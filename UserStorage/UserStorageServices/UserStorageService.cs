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
    public class UserStorageService : Switch, IUserStorageService
    {
        private static BooleanSwitch boolSwitch = new BooleanSwitch("enabledLogging", "Check if logging is on or off");
        private readonly IIdGenerator generator;
        private readonly IEntityValidator<User> validator;
        private readonly UserStorageServiceMode mode;
        private List<IUserStorageService> slaveServices;
        private List<User> users;

        public UserStorageService(UserStorageServiceMode _mode, IEnumerable<IUserStorageService> _slaves = null) : this()
        {
            this.mode = _mode;
            if (mode == UserStorageServiceMode.MasterNode && _slaves != null)
            {
                this.slaveServices = _slaves.ToList();
            }
        }

        public UserStorageService(IEntityValidator<User> _validator = null, IIdGenerator _generator = null) : base("enableLogging", "If logging enabled")
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

        public UserStorageService(IEnumerable<User> users, IEntityValidator<User> _validator = null, IIdGenerator _generator = null) : this(_validator, _generator)
        {
            foreach (User u in users)
            {
                this.Add(u);
            }
        }

        public UserStorageService(User user, IEntityValidator<User> _validator = null, IIdGenerator _generator = null) : this(_validator, _generator)
        {
            this.Add(user);
        }

        public UserStorageService(IEntityValidator<User> _validator = null, IIdGenerator _generator = null, params User[] users) : this(_validator, _generator)
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

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (!OperationAllowed())
            {
                throw new NotSupportedException();
            }

            // TODO: Implement Add() method and all other validation rules.
            this.validator.Validate(user);
            if (user.Id == Guid.Empty)
            {
                user.Id = this.generator.Generate();
            }

            if (mode == UserStorageServiceMode.MasterNode)
            {
                foreach (var service in slaveServices)
                {
                    service.Add(user);
                }
            }
            else
            {
                this.users.Add(user);
            }
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove(User user)
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

            if (!OperationAllowed())
            {
                throw new NotSupportedException();
            }

            if (mode == UserStorageServiceMode.MasterNode)
            {
                foreach (var service in slaveServices)
                {
                    service.Remove(user);
                }
            }
            else
            {
                this.users.Remove(user);
            }
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public IEnumerable<User> Search(Predicate<User> predicate)
        {
            // TODO: Implement Search() method.
            if (predicate == null)
            {
                throw new ArgumentNullException("Argument {nameof(predicate)} is null");
            }

            if (mode == UserStorageServiceMode.SlaveNode)
            {
                return this.users.FindAll(predicate);
            }
            else
            {
                List<User> result = new List<User>();
                foreach (var service in slaveServices)
                {
                    if (service.Search(predicate) != null)
                    {
                        result.AddRange(service.Search(predicate));
                    }
                }

                return result;
            }
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

        private bool Contains(User user)
        {
            foreach (User u in this.users)
            {
                if (u.Id == user.Id)
                {
                    return true;
                }
            }

            return false;
        }

        private bool OperationAllowed()
        {
            StackTrace stack = new StackTrace();
            var currentMethod = stack.GetFrame(1).GetMethod();
            var stackFramesContainsCurrentMethod = stack.GetFrames();
            var counterOfSameFrames = 0;
            foreach (var frame in stackFramesContainsCurrentMethod)
            {
                if (frame.GetMethod() == currentMethod)
                {
                    counterOfSameFrames++;
                }
                if (counterOfSameFrames >= 2)
                {
                    break;
                }
            }

            return mode == UserStorageServiceMode.MasterNode || counterOfSameFrames >= 2;
        }
    }

    public enum UserStorageServiceMode
    {
        MasterNode,
        SlaveNode
    }
}

