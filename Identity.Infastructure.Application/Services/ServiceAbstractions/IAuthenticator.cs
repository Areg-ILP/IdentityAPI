using Identity.Infastructure.Application.Commands.IdentityCommands;
using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.DetailsModels;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Services.ServiceAbstractions
{
    public interface IAuthenticator
    {
        Task<ResultModel<UserDetailsModel>> Authenticate(LoginCommand model);
    }
}
