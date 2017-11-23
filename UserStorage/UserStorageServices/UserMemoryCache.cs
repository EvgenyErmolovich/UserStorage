using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageInterfaces;

namespace UserStorageServices
{
    public abstract class UserMemoryCache : IUserRepository
    {
        protected List<User> users;
        private readonly IEntityValidator<User> validator;
        private readonly IIdGenerator generator;

        protected UserMemoryCache(IIdGenerator gen = null, IEntityValidator<User> val = null)
        {
            generator = gen ?? new DefaultIdGenerator();
            validator = val ?? new CompositeValidator();
        }

        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
        }

        public virtual User Get(Guid userId)
        {
            return users.Find(u => u.Id == userId);
        }

        public virtual void Set(User user)
        {
            this.validator.Validate(user);
            if (user.Id == Guid.Empty)
            {
                user.Id = this.generator.Generate();
            }

            this.users.Add(user);
        }

        public virtual IEnumerable<User> Query(Predicate<User> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException($"Argument {nameof(options)} is null");
            }

            return this.users.FindAll(options);
        }
    }
}


