using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageInterfaces;

namespace UserStorageServices
{
    class LastNameValidator : IEntityValidator<User>
    {
        public void Validate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException($"Lastname of {nameof(user)} is null or empty or whitespace");
            }
        }
    }
}