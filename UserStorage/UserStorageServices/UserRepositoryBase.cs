using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UserStorageInterfaces;

namespace UserStorageServices
{
    public abstract class UserRepositoryBase : IUserRepository
    {
        protected List<User> users = new List<User>();
        private readonly IEntityValidator<User> validator;
        private readonly IIdGenerator generator;
        private string filePath = "repository.bin";

        protected UserRepositoryBase(IIdGenerator gen = null, IEntityValidator<User> val = null)
        {
            generator = gen ?? new DefaultIdGenerator();
            validator = val ?? new CompositeValidator();
        }

        public int Count => users.Count;

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

        public void Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"User entity {nameof(user)} is null");
            }

            if (user.Id == Guid.Empty || string.IsNullOrWhiteSpace(user.FirstName) ||
            string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException($"User {nameof(user)} is not defined");
            }

            if (!this.Contains(user))
            {
                throw new ArgumentException("No user with such Id was found");
            }

            this.users.Remove(user);
        }

        public virtual IEnumerable<User> Query(Predicate<User> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException($"Argument {nameof(options)} is null");
            }

            return this.users.FindAll(options);
        }

        private bool Contains(User user) => this.users.Any(u => u.Id == user.Id);
    }
}