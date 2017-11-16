using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface INotificationSubscriber
    {
        void UserAdded(User user);

        void UserRemoved(User user);
    }
}
