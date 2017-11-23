using System;
using System.Collections.Generic;
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
        private string filePath = "repository.bin";
        public override void Start()
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                this.users = (List<User>)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Exception description" + e.Message);
                throw;
            }
            finally
            {
                stream.Close();
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