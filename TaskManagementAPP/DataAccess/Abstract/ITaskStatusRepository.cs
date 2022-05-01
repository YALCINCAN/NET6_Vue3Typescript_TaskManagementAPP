using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Entities.Concrete.TaskStatus;

namespace DataAccess.Abstract
{
    public interface ITaskStatusRepository:IEntityRepository<TaskStatus>
    {
        
    }
}
