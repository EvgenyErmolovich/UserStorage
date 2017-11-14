using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UserStorageServices.NUnitTests
{
    [TestFixture]
    public class UserStorageServiceTests
    {
        //[TestCase("Alex")] 
        public void GetFirstUserByName_FirstNameAlex_FirstUserWithNameAlex(string name)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Blck", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            //UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);
            UserStorageServiceMaster master = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(), new UserStorageServiceSlave() }));
            master.Add(user1);
            master.Add(user2);
            master.Add(user3);
            master.Add(user4);

            Assert.AreEqual(user1, master.GetFirstUserByName(name));
            Assert.AreNotEqual(user4, master.GetFirstUserByName(name));
        }

        [TestCase("John")]
        public void GetFirstUserByName_FirstNameJohn_Null(string name)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Blck", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageServiceMaster service = new UserStorageServiceMaster(null);
            service.Add(user1);
            service.Add(user2);
            service.Add(user3);
            service.Add(user4);
            Assert.AreEqual(null, service.GetFirstUserByName(name));
        }

        public void GetFirstUserByName_Null_ArgumentNullException()
        {
            UserStorageServiceMaster service = new UserStorageServiceMaster(null);

            Assert.Catch<ArgumentNullException>(() => service.GetFirstUserByName(null));
        }

        [TestCase("Alex")]
        public void GetAllUsersByName_FirstNameAlex_AllUsersWithNameAlex(string name)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Blck", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            //UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);
            UserStorageServiceMaster master = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(), new UserStorageServiceSlave() }));
            master.Add(user1);
            master.Add(user2);
            master.Add(user3);
            master.Add(user4);

            Assert.AreEqual(new[] { user1, user4 }, master.GetAllUsersByName(name).Take(2));
        }

        // [TestCase("Alex", "Black")]
        public void GetFirstUserByFirstAndLastName_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, string last)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageServiceMaster service = new UserStorageServiceMaster(null);
            service.Add(user1);
            service.Add(user2);
            service.Add(user3);
            service.Add(user4);

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
            User

            user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            //UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);
            UserStorageServiceMaster master = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(), new UserStorageServiceSlave() }));
            master.Add(user1);
            master.Add(user2);
            master.Add(user3);
            master.Add(user4);

            Assert.AreEqual(new[] { user1, user4 }, master.GetAllUsersByFirstAndLastName(first, last).Take(2));
        }

        //[TestCase("Alex", 22)]
        public void GetFirstUserByFirstNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(string first, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageServiceMaster service = new UserStorageServiceMaster(null);

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

            //UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4, user5);
            UserStorageServiceMaster master = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(), new UserStorageServiceSlave() }));
            master.Add(user1);
            master.Add(user2);
            master.Add(user3);
            master.Add(user4);
            master.Add(user5);

            Assert.AreEqual(new[] { user1, user4 }, master.GetAllUsersByFirstNameAndAge(first, age).Take(2));
        }

        //[TestCase("Black", 22)]
        public void GetFirstUserByLastNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "Black", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };

            //UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);
            UserStorageServiceMaster master = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(), new UserStorageServiceSlave() }));
            master.Add(user1);
            master.Add(user2);
            master.Add(user3);
            master.Add(user4);

            Assert.AreEqual(user1, master.GetFirstUserByLastNameAndAge(first, age));
            // Assert.AreNotEqual(user4, master.GetFirstUserByLastNameAndAge(first, age));
        }

        [TestCase("Black", 22)]
        public void GetAllUsersByLastNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "Black", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };

            //UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);
            UserStorageServiceMaster master = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(), new UserStorageServiceSlave() }));
            master.Add(user1);
            master.Add(user2);
            master.Add(user3);
            master.Add(user4);



            Assert.AreEqual(new[] { user1, user4 }, master.GetAllUsersByLastNameAndAge(first, age).Take(2));
        }

        //[TestCase("Alex", "Black", 22)]
        public void GetFirstUserByFirstAndLastNameAndAge_FirstNameAlexLastNameBlack_FirstUserWithFirstNameAlexLastNameBlack(
        string first, string lastname, int age)
        {
            User user1 = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            User user2 = new User() { FirstName = "Mike", LastName = "red", Age = 123 };
            User user3 = new User() { FirstName = "Sue", LastName = "gen", Age = 42 };
            User user4 = new User() { FirstName = "Alex", LastName = "Black", Age = 111 };

            UserStorageServiceMaster service = new UserStorageServiceMaster(null);

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

            //UserStorageService service = new UserStorageService(null, null, user1, user2, user3, user4);
            UserStorageServiceMaster master = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(), new UserStorageServiceSlave() }));
            master.Add(user1);
            master.Add(user2);
            master.Add(user3);
            master.Add(user4);

            Assert.AreEqual(new[] { user1, user4 }, master.GetAllUsersByFirstAndLastNameAndAge(first, last, age).Take(2));
        }

        [TestCase()]
        public void MasterMethodAdd_AnyUser_UserAddedToSlaveNodes()
        {
            User user = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            UserStorageServiceSlave slaveService1 = new UserStorageServiceSlave();
            UserStorageServiceSlave slaveService2 = new UserStorageServiceSlave();
            UserStorageServiceMaster masterService = new UserStorageServiceMaster(new List<UserStorageServiceSlave>(new[] { slaveService1, slaveService2 }));

            masterService.Add(user);

            Assert.AreEqual(1, slaveService1.Count);
            Assert.AreEqual(1, slaveService2.Count);
        }

        [Test]
        public void SlaveMethodAdd_AnyUser_NotSupportedException()
        {
            User user = new User() { FirstName = "Alex", LastName = "Black", Age = 22 };
            UserStorageServiceSlave slaveService1 = new UserStorageServiceSlave();

            Assert.Catch<NotSupportedException>(() => slaveService1.Add(user));
        }
    }
}