﻿using Identity.Infastructure.Application.Models;
using Identity.Infastructure.Application.Models.JWTModels;
using Identity.Infastructure.Application.Services.ServiceAbstractions;
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
            var authRes = await _authenticator.Authenticate(request);
            var res = new ResultModel<TokenResult>();

            if (authRes.IsSuccessed)
            {
                var token = _tokenGenerator.GenerateToken(authRes.Data);
                if (token != null)
                {
                    res.Done(new TokenResult()
                    {
                        Email = request.Email,
                        Token = token
                    });
                    return res;
                }
            }

            res.Failed("Authentication error");
            return res;
        }
    }
}