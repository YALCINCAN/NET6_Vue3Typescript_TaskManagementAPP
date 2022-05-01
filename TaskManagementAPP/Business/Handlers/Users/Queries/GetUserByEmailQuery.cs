using AutoMapper;
using Business.Constants;
using Core.Exceptions;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Queries
{
    public class GetUserByEmailQuery:IRequest<IResponse>
    {
        public string Email { get; set; }
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
        public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, IResponse>
        {
            private UserManager<User> _userManager;
            private IMapper _mapper;

            public GetUserByEmailQueryHandler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<IResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new ApiException(404, Messages.UserNotFound);
                }
                var mappeduser = _mapper.Map<UserDTO>(user);
                return new DataResponse<UserDTO>(mappeduser, 200);
            }
        }
    }

    
}
