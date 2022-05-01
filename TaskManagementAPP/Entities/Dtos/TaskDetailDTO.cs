using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TaskDetailDTO
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatusId { get; set; }
        public string Deadline { get; set; }
        public List<UserTaskDTO> AssignedUsers { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
