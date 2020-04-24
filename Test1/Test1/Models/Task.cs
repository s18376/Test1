using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
    public class Task
    {
        public int IdTask { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public Project Project { get; set; }
        public TaskType TaskType { get; set; }
        public TeamMember AssignedTo { get; set; }
        public TeamMember Creator { get; set; }
      
    }
}
