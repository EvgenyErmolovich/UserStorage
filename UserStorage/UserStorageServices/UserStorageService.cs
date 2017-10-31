using System;
using System.Collections.Generic;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        private List<User> users;

        public UserStorageService()
        {
            users = new List<User>();
        }

        public UserStorageService(IEnumerable<User> users) : this()
        {
            foreach (User u in users)
            {
                Add(u);
            }
        }

        public UserStorageService(User user) : this()
        {
            Add(user);
        }

        public UserStorageService(params User[] users) : this()
        {
            foreach (User u in users)
            {
                Add(u);
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
                return users.Count;
            }
        }

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) 
                || user.Age <= 10 || user.Age >= 100)
            {
                throw new ArgumentException("Firstname is null or empty or whitespace", nameof(user));
            }

            // TODO: Implement Add() method and all other validation rules.
            users.Add(user);
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

            if (!Contains(user))
            {
                throw new ArgumentException("No user with such Id was found");
            }

            users.Remove(user);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public void Search()
        {
            // TODO: Implement Search() method.
        }

        private bool Contains(User user)
        {
            foreach (User u in users)
            {
                if (u.Id == user.Id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
