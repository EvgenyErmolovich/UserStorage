using System;

namespace UserStorageInterfaces
{
    public interface IIdGenerator
    {
        Guid Generate();
    }
}