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
    public sealed class DeleteRoleCommand : IRequest<ResultModel<RoleDetailsModel>>
    {
        public string Name { get; set; }
    }

    public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ResultModel<RoleDetailsModel>>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ResultModel<RoleDetailsModel>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.Table.FirstOrDefaultAsync(r => r.Name == request.Name);

            if(role == null)
            {
                return ResultModel<RoleDetailsModel>.Failed(CustomErrorMessage.RoleDoesntExist);
            }

            var roleEntity = await _roleRepository.DeleteAsync(role);
            
            return ResultModel<RoleDetailsModel>.Done(new RoleDetailsModel()
            {
                Name = request.Name,
                Id = roleEntity.Id
            });
        }
    }
}
