using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Commands.IdentityCommands;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using Identity.Infastructure.Application.Services.ServiceAbstractions;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ResultModel<UserDetailsModel>> Authenticate(LoginCommand user)
        {
            //mb Hash
            var authRes = await _userRepository.Table.ProjectTo<UserDetailsModel>(_mapper.ConfigurationProvider)
                                                     .AsQueryable()
                                                     .FirstOrDefaultAsync(u => u.Email == user.Email &&
                                                                               u.Password == user.Password);

            var res = new ResultModel<UserDetailsModel>();

            if (authRes != null)
            {
                res.Done(authRes);
            }
            else
            {
                res.Failed("Authentication Error");
            }

            return res;
        }
    }
}
