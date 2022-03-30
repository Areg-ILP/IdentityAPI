using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Domain.Entities;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using Identity.Infastructure.Application.Utilities.Extentions;
using Identity.Infastructure.Application.Utilities.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Commands.IdentityCommands
{
    public sealed class RegistrationCommand : IRequest<ResultModel<UserDetailsModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, ResultModel<UserDetailsModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegistrationCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<UserDetailsModel>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _userRepository.Table.ProjectTo<UserDetailsModel>(_mapper.ConfigurationProvider)
                                                           .FirstOrDefaultAsync(u => u.Email == request.Email);
            
            if (checkUser == null)
            {
                var passHash = HashHelper.GetSoltedHash(request.Password,request.Email);

                var user = await _userRepository.CreateAsync(new User()
                             {
                                Email = request.Email,
                                PasswordHash = passHash,
                                UserName = request.Email.Split("@").First(),
                                AccessFailedCount = 0,
                                EmailConfirmed = false,
                                PhoneNumberConfirmed = false,
                                TwoFactorEnabled = false,
                                RoleId = 2
                             });;

                var userDetailModel = _mapper.Map<UserDetailsModel>(user);

                return ResultModel<UserDetailsModel>.Done(userDetailModel);
            }
            
            return ResultModel<UserDetailsModel>.Failed(CustomErrorMessage.RegistrationFailed);
        }
    }
}
