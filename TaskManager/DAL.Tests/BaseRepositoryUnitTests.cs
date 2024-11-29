using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DAL.Tests
{
    public class BaseRepositoryUnitTests
    {
        [Fact]
        public void Create_InputUserInstance_CalledAddMethodOfDBSetWithUserInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<TaskManagerContext>().Options;
            var mockContext = new Mock<TaskManagerContext>(opt);
            var mockDbSet = new Mock<DbSet<User>>();
            mockContext.Setup(context => context.Set<User>()).Returns(mockDbSet.Object);

            var repository = new TestUserRepository(mockContext.Object);

            var expectedUser = new Mock<User>().Object;

            //Act
            repository.Create(expectedUser);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Add(expectedUser), Times.Once);
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<TaskManagerContext>().Options;
            var mockContext = new Mock<TaskManagerContext>(opt);
            var mockDbSet = new Mock<DbSet<User>>();
            mockContext.Setup(context => context.Set<User>()).Returns(mockDbSet.Object);

            var expectedUser = new User { Id = 1, Department = "department", Email = "email", Name = "name", Password = "password", Staus = Enums.UserStaus.Employee};
            mockDbSet.Setup(dbSet => dbSet.Find(expectedUser.Id)).Returns(expectedUser);

            var repository = new TestUserRepository(mockContext.Object);

            // Act
            var actualUser = repository.Get(expectedUser.Id);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Find(expectedUser.Id), Times.Once());
            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<TaskManagerContext>().Options;
            var mockContext = new Mock<TaskManagerContext>(opt);
            var mockDbSet = new Mock<DbSet<User>>();
            mockContext.Setup(context => context.Set<User>()).Returns(mockDbSet.Object);

            var expectedUser = new User { Id = 1, Department = "department", Email = "email", Name = "name", Password = "password", Staus = Enums.UserStaus.Employee };
            mockDbSet.Setup(mock => mock.Find(expectedUser.Id)).Returns(expectedUser);

            var repository = new TestUserRepository(mockContext.Object);

            // Act
            repository.Delete(expectedUser.Id);

            // Assert
            mockDbSet.Verify(dbSet => dbSet.Remove(expectedUser), Times.Once);
        }
    }
}
