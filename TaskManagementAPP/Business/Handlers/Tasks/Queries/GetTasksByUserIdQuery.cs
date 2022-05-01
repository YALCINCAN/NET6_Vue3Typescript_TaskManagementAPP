using AutoMapper;
using Core.Utilities.Responses.Abstract;
using Core.Utilities.Responses.Concrete;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Business.Handlers.Tasks.Queries
{
    public class GetTasksByUserIdQuery:IRequest<IResponse>
    {
        public string UserId { get; set; }
        public GetTasksByUserIdQuery(string userid)
        {
            UserId = userid;
        }
        public class GetTasksByUserIdQueryHandler : IRequestHandler<GetTasksByUserIdQuery, IResponse>
        {
            private ITaskRepository _taskRepository;
            private IHttpContextAccessor _httpContextAccessor;
            private IMapper _mapper;

            public GetTasksByUserIdQueryHandler(ITaskRepository taskRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            {
                _taskRepository = taskRepository;
                _httpContextAccessor = httpContextAccessor;
                _mapper = mapper;
            }
            
            public async Task<IResponse> Handle(GetTasksByUserIdQuery request, CancellationToken cancellationToken)
            {
                var tasks = await _taskRepository.GetTasksByUserIdAsync(request.UserId);
                return new DataResponse<IEnumerable<TaskDTO>>(tasks, 200);
            }
        }
    }
}
