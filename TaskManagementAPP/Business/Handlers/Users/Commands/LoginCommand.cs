using Business.Constants;
using Business.Handlers.Users.Validations;
using Business.Services.Abstract;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess;
using Core.Exceptions;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Commands
{
    public class LoginCommand:IRequest<IResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, IResponse>
        {
            private UserManager<User> _userManager;
            private ITokenService _tokenService;

            public LoginCommandHandler(UserManager<User> userManager,ITokenService tokenService)
            {
                _tokenService = tokenService;
                _userManager = userManager;
            }
            
            [ValidationAspect(typeof(LoginValidator))]
            public async Task<IResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    throw new ApiException(400, Messages.UserNameOrPasswordIsIncorrect);
                }
                if (!user.EmailConfirmed)
                {
                    throw new ApiException(400, Messages.ConfirmYourAccount);
                }
                var identityResult = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!identityResult)
                {
                    throw new ApiException(400, Messages.UserNameOrPasswordIsIncorrect);
                }
                var token = await _tokenService.CreateToken(user);
                return new DataResponse<TokenDTO>(token, 200);
            }
        }
    }
}
