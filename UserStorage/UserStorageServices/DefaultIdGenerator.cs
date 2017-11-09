using System;
using UserStorageInterfaces;

namespace UserStorageServices
{
    public class DefaultIdGenerator : IIdGenerator
    {
        public Guid Generate() => new Guid();
    }
}
