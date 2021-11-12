using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Commands.IdentityCommands;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using Identity.Infastructure.Application.Services.ServiceAbstractions;
using Identity.Infastructure.Application.Utilities.Extentions;
using Identity.Infastructure.Application.Utilities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Services.ServiceImplementations
{
    public class Authenticator : IAuthenticator
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public Authenticator(IUserRepository userRepository,
                             IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResultModel<UserDetailsModel>> AuthenticateAsync(LoginCommand user)
        {
            var passHash = HashHelper.GetSoltedHash(user.Password, user.Email);
            var userEntity = await _userRepository.Table.FirstOrDefaultAsync(u => u.Email == user.Email &&
                                                                       u.PasswordHash == passHash);

            if(userEntity == null)
            {
                return ResultModel<UserDetailsModel>.Failed(CustomErrorMessage.AuthenticationError);
            }

            var res = _mapper.Map<UserDetailsModel>(userEntity);
            return ResultModel<UserDetailsModel>.Done(res);
        }
    }
}
