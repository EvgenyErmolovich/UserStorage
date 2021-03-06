﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserStorageService
    {
        UserStorageServiceMode ServiceMode { get; }

        int Count { get; }

        void Add(User user);

        void Remove(User user);

        IEnumerable<User> Search(Predicate<User> predicate);
    }
}