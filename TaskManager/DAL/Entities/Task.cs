using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskStatus = DAL.Enums.TaskStatus;

namespace DAL.Entities
{
    public class Task
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required TaskStatus Status { get; set; }
        public DateTime Deadline { get; set; }
        public required int UserId { get; set; }
    }
}
