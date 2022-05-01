using AutoMapper;
using Business.Constants;
using Business.Handlers.Tasks.Validations;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess;
using Core.Exceptions;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Globalization;

namespace Business.Handlers.Tasks.Commands
{
    public class UpdateTaskCommand:IRequest<IResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StringDeadline { get; set; }
        public int TaskStatusId { get; set; }
        public string[] UserIds { get; set; }


        public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, IResponse>
        {
            private ITaskRepository _taskRepository;
            private IUnitOfWork _unitOfWork;
            private IMapper _mapper;

            public UpdateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }


            [ValidationAspect(typeof(UpdateTaskValidator))]
            public async Task<IResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await _taskRepository.GetTaskWithUserTasksByIdAsync(request.Id);
                if (task == null)
                {
                    throw new ApiException(404, Messages.NotFound);
                }
                var mappedtask = _mapper.Map(request, task);
                mappedtask.UserTasks = request.UserIds.Select(userid => new UserTask()
                {
                    TaskId = task.Id,
                    UserId = userid
                }).ToList();
                mappedtask.Deadline = DateTime.ParseExact(request.StringDeadline, "dd-MM-yyyy HH:mm",null).ToUniversalTime();
                _taskRepository.Update(mappedtask);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.UpdatedSuccessfully);
            }
        }
    }
}
