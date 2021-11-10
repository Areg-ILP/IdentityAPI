using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Commands.RoleCommands
{
    public sealed class UpdateRoleCommand : IRequest<ResultModel<RoleDetailsModel>>
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
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
            var checkedRole = await _roleRepository.Table.FirstOrDefaultAsync(r => r.Name == request.NewName);
            if (checkedRole != null)
            {
                return ResultModel<RoleDetailsModel>.Failed("Role exist");
            }

            var role = await _roleRepository.Table.FirstOrDefaultAsync(r => r.Name == request.OldName);
            if (role == null)
            {
                return ResultModel<RoleDetailsModel>.Failed("Role dosent exists");
            }

            role.Name = request.NewName;
            var roleId = await _roleRepository.UpdateAsync(role);

            return ResultModel<RoleDetailsModel>.Done(new RoleDetailsModel()
            {
                Name = request.NewName,
                Id = roleId
            });
        }
    }
}
