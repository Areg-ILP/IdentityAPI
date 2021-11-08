using Identity.Domain.Entities;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var role = new Role
            {
                Name = request.Name
            };

            await _roleRepository.CreateAsync(role);

            var res = new ResultModel<RoleDetailsModel>();
            res.Done(new RoleDetailsModel()
            {
                Name = request.Name
            });

            return res;
        }
    }
}
