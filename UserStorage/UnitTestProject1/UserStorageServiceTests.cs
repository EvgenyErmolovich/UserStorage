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

        [TestCase("Alex", "Black")]
        public void GetFirstUserByFirstAndLastName_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
string first, string last)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(user1, service.GetAllUsersByFirstAndLastName(first, last));
            Assert.AreNotEqual(user4, service.GetAllUsersByFirstAndLastName(first, last));
        }

        [TestCase("Alex", "Black")]
        public void GetAllUsersByFirstAndLastName_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, string last)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(new[] { user1, user4 }, service.GetAllUsersByFirstAndLastName(first, last));
        }

        [TestCase("Alex", 22)]
        public void GetFirstUserByFirstNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(user1, service.GetAllUsersByFirstNameAndAge(first, age));
            Assert.AreNotEqual(user4, service.GetAllUsersByFirstNameAndAge(first, age));
        }

        [TestCase("Alex", 22)]
        public void GetAllUsersByFirstNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user5 = new User() { FirstName = "Alex", LastName = "Bra", Age = 32 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4, user5);

            Assert.AreEqual(new[] { user1, user4 }, service.GetAllUsersByFirstNameAndAge(first, age));
        }

        [TestCase("Black", 22)]
        public void GetFirstUserByLastNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "Black", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(user1, service.GetFirstUserByLastNameAndAge(first, age));
            Assert.AreNotEqual(user4, service.GetFirstUserByLastNameAndAge(first, age));
        }

        [TestCase("Black", 22)]
        public void GetAllUsersByLastNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "Black", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(new[] { user1, user4 }, service.GetAllUsersByLastNameAndAge(first, age));
        }

        [TestCase("Alex", "Black", 22)]
        public void GetFirstUserByFirstAndLastNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, string lastname, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(user1, service.GetFirstUserByFirstAndLastNameAndAge(first, lastname, age));
            Assert.AreNotEqual(user4, service.GetFirstUserByFirstAndLastNameAndAge(first, lastname, age));
        }

        [TestCase("Alex", "Black", 22)]
        public void GetAlltUsersByFirstAndLastNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, string last, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };

            UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);

            Assert.AreEqual(new[] { user1, user4 }, service.GetAllUsersByFirstAndLastNameAndAge(first, last, age));
        }
    }
}