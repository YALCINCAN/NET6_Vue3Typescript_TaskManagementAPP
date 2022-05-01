using Business.Handlers.Tasks.Commands;
using Business.Handlers.Tasks.Queries;
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
    public class TasksController : ControllerBase
    {
        private IMediator _mediator;
        private IHttpContextAccessor _httpContextAccessor;

        public TasksController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet]
        public async Task<IResponse> GetTasks()
        {
            var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return await _mediator.Send(new GetTasksByUserIdQuery(userid));
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IResponse> GetTaskDetailById(int id)
        {
            return await _mediator.Send(new GetTaskDetailByIdQuery(id));
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IResponse> CreateTask (CreateTaskCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [Authorize]
        [HttpPut]
        public async Task<IResponse> UpdateTask(UpdateTaskCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveTask(int id)
        {
            return await _mediator.Send(new RemoveTaskCommand(id));
        }
    }
}
