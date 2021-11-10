using Identity.Domain.Entities;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Commands.RoleCommands
{
    public sealed class AddRoleCommand : IRequest<ResultModel<RoleDetailsModel>>
    {
        public string Name { get; set; }
    }

    public sealed class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, ResultModel<RoleDetailsModel>>
    {
        private readonly IRoleRepository _roleRepository;

        public AddRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ResultModel<RoleDetailsModel>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var checkedRole = await _roleRepository.Table.FirstOrDefaultAsync(r => r.Name == request.Name);
            
            if(checkedRole != null)
            {
                return ResultModel<RoleDetailsModel>.Failed("Role exist");
            }

            var role = new Role
            {
                Name = request.Name,
            };

            var roleId = await _roleRepository.CreateAsync(role);

            return ResultModel<RoleDetailsModel>.Done(new RoleDetailsModel()
            {
                Name = request.Name,
                Id = roleId
            });
        }
    }
}
