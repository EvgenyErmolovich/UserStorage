using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UserStorageInterfaces;

namespace UserStorageServices
{
    public class DefaultIdGenerator : IIdGenerator
    {
        private string filePathToSaveAmount = "AmountOfGeneratedId.bin";

        public DefaultIdGenerator()
        {
            AmountOfGeneratedId = LoadAmountOfId();
        }

        public int AmountOfGeneratedId { get; private set; }

        public Guid Generate()
        {
            ++AmountOfGeneratedId;
            SaveAmountofId();
            return new Guid();
        }

        private void SaveAmountofId()
        {
            FileStream str = new FileStream(filePathToSaveAmount, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(str, AmountOfGeneratedId);
            }
            catch (SerializationException e)
            {
                Trace.WriteLine("Amount wasn't save because of " + e.Message);
            }
            finally
            {
                str.Close();
            }
        }

        private int LoadAmountOfId()
        {
            if (!File.Exists(filePathToSaveAmount))
            {
                Trace.WriteLine("Can't find file to load amount of generated id. Amount is set to 0");
                return 0;
            }

            FileStream str = new FileStream(filePathToSaveAmount, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                int res;
                return int.TryParse(formatter.Deserialize(str).ToString(), out res) ? res : 0;
            }
            catch (SerializationException e)
            {
                Trace.WriteLine("Impossible to deserialize amdount of id because of " + e.Message);
                return 0;
            }
            finally
            {
                str.Close();
            }
        }
    }
}