using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using Identity.Infastructure.Application.Utilities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Commands.RoleCommands
{
    public sealed class UpdateRoleCommand : IRequest<ResultModel<RoleDetailsModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ResultModel<RoleDetailsModel>>
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ResultModel<RoleDetailsModel>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var checkedRole = await _roleRepository.Table.FirstOrDefaultAsync(r => r.Name == request.Name);
            if (checkedRole != null)
            {
                return ResultModel<RoleDetailsModel>.Failed(CustomErrorMessage.RoleExists);
            }

            var role = await _roleRepository.Get(request.Id);
            if (role == null)
            {
                return ResultModel<RoleDetailsModel>.Failed(CustomErrorMessage.RoleDoesntExist);
            }

            role.Name = request.Name;
            var roleEntity = await _roleRepository.UpdateAsync(role);
            
            return ResultModel<RoleDetailsModel>.Done(new RoleDetailsModel()
            {
                Name = request.Name,
                Id = roleEntity.Id
            });
        }
    }
}
