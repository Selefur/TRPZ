using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using CCL.Security;
using CCL.Security.Identity;
using DAL.UnitOfWork;
using Task = DAL.Entities.Task;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Impl
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public TaskService (IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<TaskDTO> GetTasks(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Employee) && userType != typeof(Employer))
            {
                throw new MethodAccessException();
            }
            var userId = user.UserId;
            var taskEntities = 
                _database.Tasks
                .Find(z => z.UserId == userId, pageNumber, pageSize);
            var mapper = 
                new MapperConfiguration(
                    cfg => cfg.CreateMap<Task, TaskDTO>()
                    ).CreateMapper();
            var taskDTO = 
                mapper
                    .Map<IEnumerable<Task>, List<TaskDTO>>(taskEntities);
            return taskDTO;
        }

    }
}
