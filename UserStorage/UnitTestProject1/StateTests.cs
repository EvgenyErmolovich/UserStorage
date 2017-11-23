using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NUnit.Framework;

namespace UserStorageServices.NUnitTests
{
    [TestFixture]
    class StateTests
    {
        [Test]
        public void UserMemoryCacheWithStatedotStartBinarySerializer_noParametr_SerializationUsersToBinaryFileSucceed()
        {
            var rep = new UserRepositoryWithState(@"C:\Users\Desktop\repository.bin");
            User user1 = new User() { Id = Guid.NewGuid(), FirstName = "A", LastName = "A", Age = 22 };
            User user2 = new User() { Id = Guid.NewGuid(), FirstName = "B", LastName = "B", Age = 24 };
            User user3 = new User() { Id = Guid.NewGuid(), FirstName = "C", LastName = "C", Age = 44 };

            rep.Start();
            rep.Set(user1);
            rep.Set(user2);
            rep.Set(user3);
            rep.Stop();
            List<User> listOfUsers = new List<User>(new[] { user1, user2, user3 });

            BinaryFormatter form = new BinaryFormatter();
            FileStream strm = new FileStream(@"C:\Users\Desktop\repository.bin", FileMode.Open);
            List<User> listOfDeserialisedUsers = (List<User>)form.Deserialize(strm);
            strm.Close();
            for (int i = 0; i < listOfUsers.Count; i++)
            {
                Assert.AreEqual(listOfDeserialisedUsers.ElementAt(i).Id, listOfUsers.ElementAt(i).Id);
            }
        }

        [Test]
        public void UserMemoryCacheWithStatedotStartXMLSerializer_noParametr_SerializationUsersToBinaryFileSucceed()
        {
            var rep = new UserRepositoryWithState(@"C:\Users\Desktop\repository.xml", new XMLUserSerializationStrategy());
            User user1 = new User() { Id = Guid.NewGuid(), FirstName = "A", LastName = "A", Age = 22 };
            User user2 = new User() { Id = Guid.NewGuid(), FirstName = "B", LastName = "B", Age = 24 };
            User user3 = new User() { Id = Guid.NewGuid(), FirstName = "C", LastName = "C", Age = 44 };

            rep.Start();
            rep.Set(user1);
            rep.Set(user2);
            rep.Set(user3);
            rep.Stop();
            List<User> listOfUsers = new List<User>(new[] { user1, user2, user3 });

            XmlSerializer form = new XmlSerializer(typeof(List<User>));
            FileStream strm = new FileStream(@"C:\Users\Desktop\repository.xml", FileMode.Open);
            List<User> listOfDeserialisedUsers = (List<User>)form.Deserialize(strm);
            strm.Close();
            for (int i = 0; i < listOfUsers.Count; i++)
            {
                Assert.AreEqual(listOfDeserialisedUsers.ElementAt(i).Id, listOfUsers.ElementAt(i).Id);
            }
        }

    }
}