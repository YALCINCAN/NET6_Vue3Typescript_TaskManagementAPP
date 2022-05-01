using Business.Constants;
using Core.Exceptions;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Tasks.Queries
{
    public class GetTaskDetailByIdQuery:IRequest<IResponse>
    {
        public int Id { get; set; }

        public GetTaskDetailByIdQuery(int id)
        {
            Id = id;
        }
        public class GetTaskDetailByIdQueryHandler : IRequestHandler<GetTaskDetailByIdQuery, IResponse>
        {
            private ITaskRepository _taskRepository;
            public GetTaskDetailByIdQueryHandler(ITaskRepository taskRepository)
            {
                _taskRepository = taskRepository;
            }
            
            public async Task<IResponse> Handle(GetTaskDetailByIdQuery request, CancellationToken cancellationToken)
            {
                var taskdetail = await _taskRepository.GetTaskDetailByIdAsync(request.Id);
                if (taskdetail == null)
                {
                    throw new ApiException(404, Messages.NotFound);
                }
                return new DataResponse<TaskDetailDTO>(taskdetail, 200);
            }
        }
    }
}
