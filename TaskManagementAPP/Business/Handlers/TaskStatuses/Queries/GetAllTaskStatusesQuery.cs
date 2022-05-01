using AutoMapper;
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

namespace Business.Handlers.TaskStatuses.Queries
{
    public class GetAllTaskStatusesQuery : IRequest<IResponse>
    {
        public class GetAllTaskStatusesQueryHandler : IRequestHandler<GetAllTaskStatusesQuery, IResponse>
        {
            private ITaskStatusRepository _taskStatusRepository;
            private IMapper _mapper;

            public GetAllTaskStatusesQueryHandler(ITaskStatusRepository taskStatusRepository, IMapper mapper)
            {
                _taskStatusRepository = taskStatusRepository;
                _mapper = mapper;
            }
            
            public async Task<IResponse> Handle(GetAllTaskStatusesQuery request, CancellationToken cancellationToken)
            {
                var taskStatuses = await _taskStatusRepository.GetAllAsync();
                var mappedtaskStatuses = _mapper.Map<IEnumerable<TaskStatusDTO>>(taskStatuses);
                return new DataResponse<IEnumerable<TaskStatusDTO>>(mappedtaskStatuses,200);
            }
        }
    }
    
}
