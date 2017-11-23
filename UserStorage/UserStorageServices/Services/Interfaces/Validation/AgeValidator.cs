using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageInterfaces;

namespace UserStorageServices
{
    public class AgeValidator : IEntityValidator<User>
    {
        public void Validate(User user)
        {
            if (user.Age < 16 || user.Age > 150)
            {
                throw new ArgumentException($"Field Age in {nameof(user)} is incorrect");
            }
        }
    }
}