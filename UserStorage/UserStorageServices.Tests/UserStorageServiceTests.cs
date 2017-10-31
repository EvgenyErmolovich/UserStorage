using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            
            // Act
            userStorageService.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_WithoutArguments_NothingHappen()
        {
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserLastNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Evgeny",
                LastName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserAgeLessThen10_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Evgeny",
                LastName = "Ermolovich",
                Age = 4
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserAgeGreaterThen100_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(new User
            {
                FirstName = "Alex",
                LastName = "Ermolovich",
                Age = 101
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_UserIsNull_ExceptionThrown()
        {
            UserStorageService userStorageService = new UserStorageService();

            userStorageService.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_UserIdIsNotDefined_ExceptionThrown()
        {
            UserStorageService userStorageService = new UserStorageService();

            userStorageService.Remove(new User() { Id = Guid.Empty });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_UserIsNotInStorage_ExceptionThrown()
        {
            UserStorageService userStorageService = new UserStorageService();

            userStorageService.Remove(new User() { Id = Guid.NewGuid(), FirstName = "alex", LastName = "black", Age = 24 });
        }

        [TestMethod]
        public void Remove_User_UserIsRemoved()
        {
            User user = new User() { Id = Guid.NewGuid(), FirstName = "Alex", LastName = "Black", Age = 25 };
            UserStorageService userStorageService = new UserStorageService(user);

            userStorageService.Remove(user);

            Assert.AreEqual(0, userStorageService.Count);
        }
    }
}
