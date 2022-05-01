using Business.Constants;
using Core.DataAccess;
using Core.Exceptions;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Tasks.Commands
{
    public class RemoveTaskCommand : IRequest<IResponse>
    {
        public int Id { get; set; }
        public RemoveTaskCommand(int id)
        {
            Id = id;
        }
        public class RemoveTaskCommandHandler : IRequestHandler<RemoveTaskCommand, IResponse>
        {
            private IUnitOfWork _unitOfWork;
            private ITaskRepository _taskRepository;

            public RemoveTaskCommandHandler(IUnitOfWork unitOfWork, ITaskRepository taskRepository)
            {
                _unitOfWork = unitOfWork;
                _taskRepository = taskRepository;
            }

            public async Task<IResponse> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await _taskRepository.GetByIdAsync(request.Id);
                if (task == null)
                {
                    throw new ApiException(404, Messages.NotFound);
                }
                _taskRepository.Remove(task);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
        }
    }
}
