using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserStorageService
    {
        bool isLoggingEnabled { get; }
        int Count { get; }
        void Add(User user);
        void Remove(User user);
        IEnumerable<User> Search(Predicate<User> predicate);
        User GetFirstUserByName(string firstName);
        IEnumerable<User> GetAllUsersByName(string firstname);
        User GetFirstUserByLastName(string lastname);
        IEnumerable<User> GetAllUsersByLastName(string lastname);
        User GetFirstUserByAge(int age);
        IEnumerable<User> GetAllUsersByAge(int age);

    }
}