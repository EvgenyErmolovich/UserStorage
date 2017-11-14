using System;
using System.Collections.Generic;
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
        [ExpectedException(typeof(FirstNameIsNullOrEmptyException))]
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
        [ExpectedException(typeof(LastNameIsNullOrEmptyException))]
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
        [ExpectedException(typeof(AgeException))]
        public void Add_UserAgeLessThen10_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            var s = new UserStorageLog(userStorageService);

            // Act
            s.Add(new User
            {
                FirstName = "Evgeny",
                LastName = "Ermolovich",
                Age = 4
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(AgeException))]
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
            UserStorageService userStorageServiceSlave1 = new UserStorageService(UserStorageServiceMode.SlaveNode);
            UserStorageService userStorageServiceSlave2 = new UserStorageService(UserStorageServiceMode.SlaveNode);
            UserStorageService userStorageServiceMaster = new UserStorageService(UserStorageServiceMode.MasterNode, new List<IUserStorageService>(new[] { userStorageServiceSlave1, userStorageServiceSlave2 }));

            userStorageServiceMaster.Remove(new User() { Id = Guid.NewGuid(), FirstName = "alex", LastName = "black", Age = 24 });
        }

        [TestMethod]
        public void Remove_User_UserIsRemoved()
        {
            User user = new User() { Id = Guid.NewGuid(), FirstName = "Alex", LastName = "Black", Age = 25 };

            UserStorageService userStorageService = new UserStorageService(UserStorageServiceMode.MasterNode);
            UserStorageService slave1 = new UserStorageService(UserStorageServiceMode.MasterNode);
            UserStorageService slave2 = new UserStorageService(UserStorageServiceMode.MasterNode);
            userStorageService.AddSubscriber(slave1);
            userStorageService.AddSubscriber(slave2);
            userStorageService.Add(user);

            userStorageService.Remove(user);

            Assert.AreEqual(0, slave2.Count);
            Assert.AreEqual(0, slave1.Count);
        }
    }
}
