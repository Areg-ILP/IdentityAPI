using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.JWTModels;
using Identity.Infastructure.Application.Services.ServiceAbstractions;
using Identity.Infastructure.Application.Utilities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Commands.IdentityCommands
{
    public sealed class LoginCommand : IRequest<ResultModel<TokenResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ResultModel<TokenResult>>
    {
        private readonly IAuthenticator _authenticator;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginCommandHandler(IAuthenticator authenticator,
                                   ITokenGenerator tokenGenerator)
        {
            _authenticator = authenticator;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResultModel<TokenResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var res = await _authenticator.Authenticate(request);

            if (res.IsSuccessed)
            {
                var token = _tokenGenerator.GenerateToken(res.Data);
                if (token != null)
                {
                    return ResultModel<TokenResult>.Done(new TokenResult()
                    {
                        Email = request.Email,
                        Token = token
                    });
                }
            }

            return ResultModel<TokenResult>.Failed(CustomErrorMessage.LoginFailed);
        }
    }
}
