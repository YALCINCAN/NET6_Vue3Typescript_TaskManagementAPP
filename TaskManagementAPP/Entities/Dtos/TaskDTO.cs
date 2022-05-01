using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TaskDTO
    {
        public  int Id { get; set; }
        public string CreatorId { get; set; }
        public string Deadline { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public List<UserTaskDTO>  AssignedUsers { get; set; }
    }
}
