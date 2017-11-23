#define TRACE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class BinaryUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(List<User> users, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, users);
            }
            catch (SerializationException e)
            {
                Trace.WriteLine("Serialization has been stoped because of " + e.Message);
                throw;
            }
            finally
            {
                stream.Close();
            }
        }

        public List<User> DeserializeUsers(string path)
        {
            if (!File.Exists(path))
            {
                Trace.WriteLine("Warning : file not found. It will be created");
                return new List<User>();
            }

            FileStream stream = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<User>)formatter.Deserialize(stream);
            }
            catch (ServerException e)
            {
                Trace.WriteLine("Warning : deserialization failed because of " + e.Message);
                throw;
            }
            finally
            {
                stream.Close();
            }
        }
    }
}