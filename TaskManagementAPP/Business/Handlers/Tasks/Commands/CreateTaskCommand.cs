using AutoMapper;
using Business.Constants;
using Business.Handlers.Tasks.Validations;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task = Entities.Concrete.Task;

namespace Business.Handlers.Tasks.Commands
{
    public class CreateTaskCommand : IRequest<IResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string StringDeadline { get; set; }
        public string[] UserIds { get; set; }

        public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, IResponse>
        {
            private ITaskRepository _taskRepository;
            private IUnitOfWork _unitOfWork;
            private IHttpContextAccessor _httpContextAccessor;
            private IMapper _mapper;

            public CreateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
                _mapper = mapper;
            }

            [ValidationAspect(typeof(CreateTaskValidator))]
            public async Task<IResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var task = _mapper.Map<Task>(request);
                task.CreatorId = userid;
                task.TaskStatusId = 1;
                task.Deadline = DateTime.ParseExact(request.StringDeadline, "dd-MM-yyyy HH:mm", null).ToUniversalTime();
                task.UserTasks = request.UserIds.Select(x => new UserTask() { UserId = x, TaskId = task.Id }).ToList();
                await _taskRepository.AddAsync(task);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.AddedSuccesfully);
            }
        }
    }


}
