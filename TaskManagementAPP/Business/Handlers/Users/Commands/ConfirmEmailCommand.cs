using Business.Constants;
using Core.Exceptions;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Commands
{
    public class ConfirmEmailCommand : IRequest<IResponse>
    {
        public string UserId { get; set; }
        public string Token { get; set; }


        public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, IResponse>
        {
            private UserManager<User> _userManager;

            public ConfirmEmailCommandHandler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
            {
                if (request.UserId == null || request.Token == null)
                {
                    throw new ApiException(404, Messages.TokenOrUserNotFound);
                }
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new ApiException(404, Messages.UserNotFound);
                }
                if (user.EmailConfirmed)
                {
                    throw new ApiException(400, Messages.AlreadyAccountConfirmed);
                }
                var tokenDecodedBytes = WebEncoders.Base64UrlDecode(request.Token);
                var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
                var result = await _userManager.ConfirmEmailAsync(user, tokenDecoded);
                if (result.Succeeded)
                {
                    return new SuccessResponse(200, Messages.SuccessfullyAccountConfirmed);
                }
                throw new ApiException(400, Messages.AccountDontConfirmed);
            }
        }
    }

    
}
