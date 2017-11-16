using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageInterfaces;

namespace UserStorageServices
{
    public class UserStorageServiceMaster : UserStorageService
    {
        private List<UserStorageServiceSlave> slaveServices;

        private List<INotificationSubscriber> subscribers = new List<INotificationSubscriber>();

        private event Action<User> UserAdded;

        private event Action<User> UserRemoved;

        public UserStorageServiceMaster(IEnumerable<UserStorageServiceSlave> slaves, IEntityValidator<User> validator = null, IIdGenerator generator = null)
            : base(validator, generator)
        {
            if (slaves != null)
            {
                this.slaveServices = slaves.ToList();
            }
            else
            {
                this.slaveServices = new List<UserStorageServiceSlave>();
            }
        }
        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;
        public override void Add(User user)
        {
            base.Add(user);
            foreach (var ss in slaveServices)
            {
                ss.Add(user);
            }

            OnUserAdded(user);

            //foreach (var sub in subscribers)
            //{
            // sub.UserAdded(user);   
            //}
        }

        public override void Remove(User user)
        {
            base.Remove(user);
            foreach (var ss in slaveServices)
            {
                ss.Remove(user);
            }

            OnUserRemoved(user);

            //foreach (var sub in subscribers)
            //{
            //    sub.UserRemoved(user);
            //}
        }

        public override IEnumerable<User> Search(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"Argument {nameof(predicate)} is null");
            }

            List<User> result = new List<User>();

            foreach (var service in slaveServices)
            {
                if (service.Search(predicate) != null)
                {
                    result.AddRange(service.Search(predicate));
                }
            }
            return result;
        }

        public void AddSubscriber(INotificationSubscriber sub)
        {
            if (sub == null) throw new ArgumentNullException($"{nameof(sub)} is null");
            subscribers.Add(sub);
            UserAdded += sub.UserAdded;
            UserRemoved += sub.UserRemoved;
        }

        public void RemoveSubscriber(INotificationSubscriber sub)
        {
            if (sub == null) throw new ArgumentNullException($"{nameof(sub)} is null");
            if (!subscribers.Contains(sub)) throw new InvalidOperationException("No such subscruber was found");
            subscribers.Remove(sub);
            UserAdded -= sub.UserAdded;
            UserRemoved -= sub.UserRemoved;
        }

        private void OnUserAdded(User user)
        {
            var x = UserAdded;
            UserAdded?.Invoke(user);
        }
        private void OnUserRemoved(User user)
        {
            UserRemoved?.Invoke(user);
        }
    }
}