using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Entities.Concrete.TaskStatus;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTaskStatusRepository : EfEntityRepositoryBase<TaskStatus, TaskContext>, ITaskStatusRepository
    {
        public EfTaskStatusRepository(TaskContext context) : base(context)
        {
            
        }
    }
}
