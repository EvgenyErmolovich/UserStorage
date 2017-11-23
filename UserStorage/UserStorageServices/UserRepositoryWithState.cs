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
    public class UserRepositoryWithState : UserRepositoryBase
    {
        private readonly string filePath;
        private readonly IUserSerializationStrategy serializator;

        public UserRepositoryWithState(string filePath = null, IUserSerializationStrategy serializator = null)
        {
            this.filePath = filePath ?? "repository.bin";
            this.serializator = serializator ?? new BinaryUserSerializationStrategy();
        }

        public override void Start()
        {
            this.users = this.serializator.DeserializeUsers(this.filePath);
        }

        public override void Stop()
        {
            this.serializator.SerializeUsers(this.users, this.filePath);
        }
    }
}
