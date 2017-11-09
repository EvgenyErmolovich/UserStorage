using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UserStorageServices.NUnitTests
{
    [TestFixture]
    public class UserStorageServicesTests
    {
        [TestCase("Alex")]

        public void GetFirstUserByName_FirstNameAlex_FirstUserWithNameAlex(string name)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Blck", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(user1, service.GetFirstUserByName(name));
            Assert.AreNotEqual(user4, service.GetFirstUserByName(name));
        }

        [TestCase("John")]
        public void GetFirstUserByName_FirstNameJohn_Null(string name)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Blck", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(null, service.GetFirstUserByName(name));
        }

        public void GetFirstUserByName_Null_ArgumentNullException()
        {
            UserStorageService service = new UserStorageService();

            Assert.Catch<ArgumentNullException>(() => service.GetFirstUserByName(null));
        }

        [TestCase("Alex")]
        public void GetAllUsersByName_FirstNameAlex_AllUsersWithNameAlex(string name)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Blck", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(new[] { user1, user4 }, service.GetAllUsersByName(name));
            Assert.AreNotEqual(user4, service.GetFirstUserByName(name));
        }
    }
}