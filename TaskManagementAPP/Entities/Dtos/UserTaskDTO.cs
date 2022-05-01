using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserTaskDTO
    {
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
