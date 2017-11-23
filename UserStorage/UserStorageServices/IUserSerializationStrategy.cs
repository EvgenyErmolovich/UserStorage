using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserSerializationStrategy
    {
        void SerializeUsers(List<User> users, string path);

        List<User> DeserializeUsers(string path);
    }
}