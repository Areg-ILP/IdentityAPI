using Identity.Infastructure.Application.Models.DetailsModels;

namespace Identity.Infastructure.Application.Services.ServiceAbstractions
{
    public interface ITokenGenerator
    {
        string GenerateToken(UserDetailsModel user);
    }
}
