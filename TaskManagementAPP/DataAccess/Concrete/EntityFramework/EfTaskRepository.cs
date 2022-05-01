using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Entities.Concrete.Task;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTaskRepository : EfEntityRepositoryBase<Task, TaskContext>, ITaskRepository
    {
        public EfTaskRepository(TaskContext context) : base(context)
        {

        }

        public async Task<TaskDetailDTO> GetTaskDetailByIdAsync(int id)
        {
            return await _context.Tasks.Select(task => new TaskDetailDTO
            {
                Id = task.Id,
                Title = task.Title,
                Deadline = task.Deadline.ToString("dd-MM-yyyy HH:mm",null),
                Description = task.Description,
                CreatorId = task.CreatorId,
                TaskStatusId=task.TaskStatusId,
                AssignedUsers = task.UserTasks.Select(usertask => new UserTaskDTO
                {
                    TaskId = usertask.TaskId,
                    UserId = usertask.UserId,
                    User = new UserDTO
                    {
                        FirstName = usertask.User.FirstName,
                        LastName = usertask.User.LastName,
                        Email = usertask.User.Email,
                        Id = usertask.User.Id,
                        UserName = usertask.User.UserName
                    }
                }).ToList(),
                Comments = task.Comments.Select(comment => new CommentDTO
                {
                    Id = comment.Id,
                    Description = comment.Description,
                    UserId = comment.UserId,
                    TaskId = comment.TaskId,
                    CommentDate=comment.CommentDate,
                    User = new UserDTO
                    {
                        FirstName = comment.User.FirstName,
                        LastName = comment.User.LastName,
                        Email = comment.User.Email,
                        Id = comment.User.Id,
                        UserName = comment.User.UserName
                    }
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(string userid)
        {
            return await _context.Tasks.Where(ut => ut.UserTasks.Any(u => u.UserId == userid) || ut.CreatorId == userid).Select(task => new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Deadline = task.Deadline.ToString("dd-MM-yyyy HH:mm", null),
                CreatorId = task.CreatorId,
                Status = task.TaskStatus.Status,
                AssignedUsers = task.UserTasks.Select(usertask => new UserTaskDTO
                {
                    TaskId = usertask.TaskId,
                    UserId = usertask.UserId,
                    User = new UserDTO
                    {
                        FirstName = usertask.User.FirstName,
                        LastName = usertask.User.LastName,
                        Email = usertask.User.Email,
                        Id = usertask.User.Id,
                        UserName = usertask.User.UserName
                    }
                }).ToList(),
            }).ToListAsync();
        }

        public async Task<Task> GetTaskWithUserTasksByIdAsync(int id)
        {
            return await _context.Tasks.Include(x => x.UserTasks).FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
