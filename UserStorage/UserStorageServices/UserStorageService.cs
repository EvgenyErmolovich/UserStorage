using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using UserStorageInterfaces;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public abstract class UserStorageService : Switch, IUserStorageService
    {
        private readonly IIdGenerator generator;
        private readonly IEntityValidator<User> validator;
        //private List<IUserStorageService> slaveServices = new List<IUserStorageService>();
        private List<User> users;
       
        protected UserStorageService(IEntityValidator<User> _validator = null, IIdGenerator _generator = null) : base("enableLogging", "If logging enabled")
        {
            this.validator = _validator;
            this.generator = _generator;
            if (_validator == null)
            {
                this.validator = new CompositeValidator();
            }

            if (this.generator == null)
            {
                this.generator = new DefaultIdGenerator();
            }

            this.users = new List<User>();
        }

        protected UserStorageService(IEnumerable<User> users, IEntityValidator<User> _validator = null, IIdGenerator _generator = null) : this(_validator, _generator)
        {
            foreach (User u in users)
            {
                this.Add(u);
            }
        }

        protected UserStorageService(User user, IEntityValidator<User> _validator = null, IIdGenerator _generator = null) : this(_validator, _generator)
        {
            this.Add(user);
        }

        protected UserStorageService(IEntityValidator<User> _validator = null, IIdGenerator _generator = null, params User[] users) : this(_validator, _generator)
        {
            foreach (User u in users)
            {
                this.Add(u);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count
        {
            get
            {
                return this.users.Count;
            }
        }

        public abstract UserStorageServiceMode ServiceMode { get; }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            // TODO: Implement Add() method and all other validation rules.
            this.validator.Validate(user);
            if (user.Id == Guid.Empty)
            {
                user.Id = this.generator.Generate();
            }
            this.users.Add(user);

            //if (mode == UserStorageServiceMode.MasterNode && slaveServices != null)
            //{
            //    foreach (var service in slaveServices)
            //    {
            //        service.Add(user);
            //    }
            //}
            //else
            //{
            //    this.users.Add(user);
            //}
            //if (mode == UserStorageServiceMode.MasterNode)
            //{
            //    foreach (var sub in subscribers)
            //    {
            //        sub.UserAdded(user);
            //    }
            //}
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public virtual void Remove(User user)
        {
            // TODO: Implement Remove() method.
            if (user == null)
            {
                throw new ArgumentNullException("User entity {nameof(user)} is null");
            }

            if (user.Id == Guid.Empty || string.IsNullOrWhiteSpace(user.FirstName) ||
            string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("User {nameof(user)} is not defined");
            }

            if (!this.Contains(user))
            {
                throw new ArgumentException("No user with such Id was found");
            }

            this.users.Remove(user);

            //if (mode == UserStorageServiceMode.MasterNode)
            //{
            //    foreach (var sub in subscribers)
            //    {
            //        sub.UserRemoved(user);
            //    }
            //}
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public virtual IEnumerable<User> Search(Predicate<User> predicate)
        {
            // TODO: Implement Search() method.
            if (predicate == null)
            {
                throw new ArgumentNullException("Argument {nameof(predicate)} is null");
            }
            return this.users.FindAll(predicate);
        }

        public User GetFirstUserByName(string firstName)
        {
            return this.Search(delegate(User user) { return user.FirstName == firstName; }).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsersByName(string firstname)
        {
            return this.Search(delegate(User user) { return user.FirstName == firstname; });
        }

        public User GetFirstUserByLastName(string lastname)
        {
            return this.Search(delegate(User user) { return user.LastName == lastname; }).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsersByLastName(string lastname)
        {
            return this.Search(delegate(User user) { return user.LastName == lastname; });
        }

        public User GetFirstUserByAge(int age)
        {
            return this.Search(delegate(User user) { return user.Age == age; }).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsersByAge(int age)
        {
            return this.Search(delegate(User user) { return user.Age == age; });
        }

        public User GetFirstUserByFirstAndLastName(string firstname, string lastname)
        {
            return this.Search(user => user.FirstName == firstname && user.LastName == lastname).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsersByFirstAndLastName(string firstname, string lastname)
        {
            return this.Search(user => user.FirstName == firstname && user.LastName == lastname);
        }

        public User GetFirstUserByFirstNameAndAge(string firstname, int age)
        {
            return this.Search(user => user.FirstName == firstname && user.Age == age).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsersByFirstNameAndAge(string firstname, int age)
        {
            return this.Search(user => user.FirstName == firstname && user.Age == age);
        }

        public User GetFirstUserByLastNameAndAge(string lastname, int age)
        {
            return this.Search(user => user.LastName == lastname && user.Age == age).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsersByLastNameAndAge(string lastname, int age)
        {
            return this.Search(user => user.LastName == lastname && user.Age == age);
        }

        public User GetFirstUserByFirstAndLastNameAndAge(string firstname, string lastname, int age)
        {
            return this.Search(user => user.FirstName == firstname && user.LastName == lastname && user.Age == age).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsersByFirstAndLastNameAndAge(string firstname, string lastname, int age)
        {
            return this.Search(user => user.FirstName == firstname && user.LastName == lastname && user.Age == age);
        }

        private bool Contains(User user) => this.users.Any(u => u.Id == user.Id);
    }

    public enum UserStorageServiceMode
    {
        MasterNode,
        SlaveNode
    }
}

