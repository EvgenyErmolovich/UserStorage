﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageLog : UserStorageDecorator
    {

        private static BooleanSwitch boolSwitch = new BooleanSwitch("enabledLogging", "Check if logging is on or off");

        public UserStorageLog(IUserStorageService _service) : base(_service)
        {
        }

        public override int Count { get; }
        public override void Add(User user)
        {
            if (boolSwitch.Enabled) Console.WriteLine("Add() method is used");
            Service.Add(user);
        }

        public override void Remove(User user)
        {
            if (boolSwitch.Enabled) Console.WriteLine("Remove() method is used");
            Service.Remove(user);
        }

        public override IEnumerable<User> Search(Predicate<User> predicate)
        {
            if (boolSwitch.Enabled) Console.WriteLine("Search() method is userd");
            return Service.Search(predicate);
        }
    }
}