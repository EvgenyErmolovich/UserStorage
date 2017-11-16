using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public abstract class UserStorageDecorator : IUserStorageService
    {
        protected IUserStorageService Service;

        protected UserStorageDecorator(IUserStorageService _service)
        {
            Service = _service;
        }

        public UserStorageServiceMode ServiceMode { get; }

        public abstract int Count { get; }

        public abstract void Add(User user);

        public abstract void Remove(User user);

        public abstract IEnumerable<User> Search(Predicate<User> predicate);
    }
}
