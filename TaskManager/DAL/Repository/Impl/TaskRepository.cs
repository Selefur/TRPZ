using DAL.EF;
using DAL.Entities;
using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = DAL.Entities.Task;

namespace DAL.Repository.Impl
{
    public class TaskRepository
        : BaseRepository<Task>, ITaskRepository
    {
        internal TaskRepository(TaskManagerContext context)
            : base(context)
        {
        }
    }
}
