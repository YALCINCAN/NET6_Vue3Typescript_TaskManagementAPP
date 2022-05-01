using AutoMapper;
using Business.Constants;
using Business.Handlers.Users.Validations;
using Core.Aspects.Autofac.Validation;
using Core.Exceptions;
using Core.Utilities;
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
    public class RegisterCommand:IRequest<IResponse>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IResponse>
        {
            private UserManager<User> _userManager;
            private IMapper _mapper;
            private IEmailService _emailService;
            public RegisterCommandHandler(UserManager<User> userManager, IMapper mapper, IEmailService emailService)
            {
                _userManager = userManager;
                _mapper = mapper;
                _emailService = emailService;
            }

            [ValidationAspect(typeof(RegisterValidator))]
            public async Task<IResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var email = await _userManager.FindByEmailAsync(request.Email);
                if (email != null)
                {
                    throw new ApiException(400, Messages.EmailIsAlreadyExist);
                }
                var username = await _userManager.FindByNameAsync(request.UserName);
                if (username != null)
                {
                    throw new ApiException(400, Messages.UsernameIsAlreadyExist);
                }
                if (request.Password != request.ConfirmPassword)
                {
                    throw new ApiException(400, Messages.PasswordDontMatchWithConfirmation);
                }
                var user = _mapper.Map<User>(request);
                var IdentityResult = await _userManager.CreateAsync(user, request.Password);
                if (IdentityResult.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                    var tokenEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                    string link = "http://localhost:8080/confirmemail/" + user.Id + "/" + tokenEncoded;
                    await _emailService.ConfirmationMailAsync(link, request.Email);
                    return new SuccessResponse(200, Messages.RegisterSuccessfully);
                }
                else
                {
                    throw new ApiException(400, IdentityResult.Errors.Select(e => e.Description).ToList());
                }
            }
        }
    }
}
