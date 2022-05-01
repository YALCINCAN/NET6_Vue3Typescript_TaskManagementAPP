using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime CommentDate { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
        public int TaskId { get; set; }
    }
}
