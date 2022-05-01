using Business.Handlers.TaskStatuses.Queries;
using Core.Utilities.Responses.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusesController : ControllerBase
    {
        private IMediator _mediator;

        public TaskStatusesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IResponse> GetAll()
        {
            return await _mediator.Send(new GetAllTaskStatusesQuery());
        }
    }
}
