#define TRACE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        private string filePath;

        public UserMemoryCacheWithState(string filePath = null)
        {
            this.filePath = filePath ?? "repository.bin";
        }

        public override void Start()
        {
            if (File.Exists(filePath))
            {
                FileStream stream = new FileStream(filePath, FileMode.Open);

                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    this.users = (List<User>)formatter.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    Trace.WriteLine("Exception description" + e.Message);
                    throw;
                }
                finally
                {
                    stream.Close();
                }
            }
            else
            {
                Trace.WriteLine("Warning : File not found. It will be created");
            }
        }

        public override void Stop()
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(stream, this.users);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception during serialization " + e.Message);
                    throw;
                }
            }
        }

    }
}