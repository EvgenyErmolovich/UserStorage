using System;

namespace UserStorageInterfaces
{
    public interface IIdGenerator
    {
        int AmountOfGeneratedId { get; }

        Guid Generate();
    }
}