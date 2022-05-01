using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using System.Linq.Expressions;
using Task = Entities.Concrete.Task;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface ITaskRepository : IEntityRepository<Task>
    {
        Task<Task> GetTaskWithUserTasksByIdAsync(int id);
        Task<TaskDetailDTO> GetTaskDetailByIdAsync(int id);
        Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(string userid);
    }
}
