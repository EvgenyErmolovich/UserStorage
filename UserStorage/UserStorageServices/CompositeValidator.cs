using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageInterfaces;

namespace UserStorageServices
{
    class CompositeValidator : IEntityValidator<User>
    {
        private readonly List<IEntityValidator<User>> validators;
        public CompositeValidator()
        {
            validators = new List<IEntityValidator<User>>();
            validators.Add(new FirstNameValidator());
            validators.Add(new LastNameValidator());
            validators.Add(new AgeValidator());
        }
        public void Validate(User user)
        {
            if (user == null) throw new ArgumentNullException($"{nameof(user)} is null");
            foreach (var i in validators)
            {
                i.Validate(user);
            }
        }

    }
}