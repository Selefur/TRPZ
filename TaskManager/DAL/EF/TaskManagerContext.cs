using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class TaskManagerContext
        : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }

        public TaskManagerContext(DbContextOptions options) : base(options) { }
    }
}
