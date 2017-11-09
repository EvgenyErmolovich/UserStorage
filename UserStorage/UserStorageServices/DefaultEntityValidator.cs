using System;
using UserStorageInterfaces;

namespace UserStorageServices
{
    public class DefaultEntityValidator : IEntityValidator<User>
    {
        public bool Validate(User user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName) ||
            user.Age <= 10 || user.Age >= 100)
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(user));
            }

            return true;
        }
    }
}