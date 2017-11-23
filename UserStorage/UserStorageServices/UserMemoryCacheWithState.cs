using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserMemoryCacheWithState : UserMemoryCache
    {
        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override User Get(Guid user)
        {
            throw new NotImplementedException();
        }

        public override void Set(User user)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<User> Query(Predicate<User> options)
        {
            throw new NotImplementedException();
        }
    }
}