using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageInterfaces;

namespace UserStorageServices
{
    public class UserStorageServiceSlave : UserStorageService
    {
        public UserStorageServiceSlave(IEntityValidator<User> validator = null, IIdGenerator generator = null)
        : base(validator, generator)
        {

        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.SlaveNode;

        public override void Add(User user)
        {
            if (OperationAllowed())
            {
                base.Add(user);
            }
            else
            {
                throw new NotSupportedException("You don't have enough access lavel");
            }
        }

        public override void Remove(User user)
        {
            if (OperationAllowed())
            {
                base.Remove(user);
            }
            else
            {
                throw new NotSupportedException("You don't have enough access lavel");
            }
        }

        private static bool OperationAllowed()
        {
            StackTrace stack = new StackTrace();
            var currentMethod = stack.GetFrame(1).GetMethod();
            var stackFramesContainsCurrentMethod = stack.GetFrames();
            var counterOfSameFrames = 0;
            foreach (var frame in stackFramesContainsCurrentMethod)
            {
                if (frame.GetMethod() == currentMethod)
                {
                    counterOfSameFrames++;
                    break;
                }
                //if (counterOfSameFrames >= 2)
                //{
                // break;
                //}
            }

            return counterOfSameFrames == 1;
        }
    }
}