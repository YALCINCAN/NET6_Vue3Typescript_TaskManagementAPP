using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Core.Utilities.Responses.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthController(IMediator mediator,IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize]
        public async Task<IResponse> GetAuthenticatedUser()
        {
            var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return await _mediator.Send(new GetAuthenticatedUserQuery(userid));
        }

        [HttpGet("{email}")]
        [Authorize]
        public async Task<IResponse> GetUserByEmail(string email)
        {
            return await _mediator.Send(new GetUserByEmailQuery(email));
        }


        [HttpPost("login")]
        public async Task<IResponse>Login(LoginCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("register")]
        public async Task<IResponse>Register(RegisterCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpPost("confirmemail")]
        public async Task<IResponse> ConfirmEmail(ConfirmEmailCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
