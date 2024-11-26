using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = DAL.Entities.Task;

namespace DAL.Repository.Interfaces
{
    public interface ITaskRepository
        : IRepository<Task>
    {
    }
}
