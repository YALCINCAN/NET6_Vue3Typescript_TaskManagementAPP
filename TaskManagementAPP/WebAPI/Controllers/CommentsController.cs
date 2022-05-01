using Business.Handlers.Comments.Commands;
using Core.Utilities.Responses.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IResponse> CreateComment(CreateCommentCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [Authorize]
        [HttpPut]
        public async Task<IResponse> UpdateCommand(UpdateCommentCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IResponse> RemoveCommand(int id)
        {
            return await _mediator.Send(new RemoveCommentCommand(id));
        }
    }
}
