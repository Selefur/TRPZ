using BLL.Services.Impl;
using BLL.Services.Interfaces;
using CCL.Security;
using CCL.Security.Identity;
using DAL.Repository.Interfaces;
using DAL.UnitOfWork;
using Moq;
using Task = DAL.Entities.Task;
using System.IO;

namespace BLL.Tests
{
    public class TaskServiceTests
    {
        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(
                () => new TaskService(nullUnitOfWork)
            );
        }

        [Fact]
        public void GetTasks_UserIsEmployer_ThrowMethodAccessException()
        {
            // Arrange
            User user = new Employer(1, "name", "email", "password", "department");
            SecurityContext.SetUser(user);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            ITaskService taskService = new TaskService(mockUnitOfWork.Object);
            // Act
            var actualGetTaskssFunc = () => taskService.GetTasks(0);
            var exception = Record.Exception(actualGetTaskssFunc);

            // Assert
            Assert.IsNotType<MethodAccessException>(exception);
        }

        [Fact]
        public void GetTasks_TaskFromDAL_CorrectMappingToTaskDTO()
        {
            // Arrange
            User user = new Employer(1, "name", "email", "password", "department");
            SecurityContext.SetUser(user);
            var taskService = GetTaskService();
            // Act
            var actualTaskDto = taskService.GetTasks(0).First();
            // Assert
            Assert.True(
                actualTaskDto.Id == 1
                && actualTaskDto.Name == "testValue"
                && actualTaskDto.Description == "testValue"
                && actualTaskDto.Status == DAL.Enums.TaskStatus.InProgress
                && actualTaskDto.Deadline == DateTime.MaxValue
                && actualTaskDto.UserId == 1
            );
        }
        ITaskService GetTaskService()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedTask = new Task()
            {
                Id = 1,
                Name = "testValue",
                Description = "testValue",
                Status = DAL.Enums.TaskStatus.InProgress,
                Deadline = DateTime.MaxValue,
                UserId = 1
            };
            var mockDbSet = new Mock<ITaskRepository>();
            mockDbSet
                .Setup(z =>
                    z.Find(
                        It.IsAny<Func<Task, bool>>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                    .Returns(
                        new List<Task>() { expectedTask }
                    );
            mockContext
                .Setup(context =>
                    context.Tasks)
                .Returns(mockDbSet.Object);
            ITaskService taskService = new TaskService(mockContext.Object);

            return taskService;
        }
    }
}