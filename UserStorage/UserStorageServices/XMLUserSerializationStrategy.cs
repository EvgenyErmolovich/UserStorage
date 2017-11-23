#define TRACE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserStorageServices
{
    public class XMLUserSerializationStrategy : IUserSerializationStrategy
    {
        public void SerializeUsers(List<User> users, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
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
                XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
                return (List<User>)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
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
