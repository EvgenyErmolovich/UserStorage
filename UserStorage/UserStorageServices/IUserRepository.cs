using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserRepository
    {
        void Start();
        void Stop();
        User Get(Guid userId);
        void Set(User user);
        IEnumerable<User> Query(Predicate<User> options);
    }
}