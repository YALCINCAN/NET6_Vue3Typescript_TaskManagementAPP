using AutoMapper;
using Business.Handlers.Tasks.Commands;
using Business.Handlers.Users.Commands;
using Entities.Concrete;
using Entities.Dtos;
using TaskStatus = Entities.Concrete.TaskStatus;
using Task = Entities.Concrete.Task;
using Business.Handlers.Comments.Commands;

namespace Business.Mappings
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<TaskStatus, TaskStatusDTO>().ReverseMap();
            CreateMap<Task, CreateTaskCommand>().ReverseMap();
            CreateMap<Task, UpdateTaskCommand>().ReverseMap();
            CreateMap<Comment, CreateCommentCommand>().ReverseMap();
            CreateMap<Comment, UpdateCommentCommand>().ReverseMap();
        }
    }
}
