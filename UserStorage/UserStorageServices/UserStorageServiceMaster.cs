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
        private List<UserStorageServiceSlave> slaveServices = new List<UserStorageServiceSlave>();
        private List<INotificationSubscriber> subscribers = new List<INotificationSubscriber>();

        public UserStorageServiceMaster(IUserRepository rep) : base(rep)
        {
        }

        public UserStorageServiceMaster(IUserRepository rep, IEnumerable<UserStorageServiceSlave> slaves, IEntityValidator<User> validator = null, IIdGenerator generator = null) : this(rep)
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

        private event Action<User> UserAdded;

        private event Action<User> UserRemoved;

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        public override void Add(User user)
        {
            base.Add(user);
            foreach (var ss in this.slaveServices)
            {
                ss.Add(user);
            }

            this.OnUserAdded(user);
        }

        public override void Remove(User user)
        {
            base.Remove(user);
            foreach (var ss in this.slaveServices)
            {
                ss.Remove(user);
            }

            this.OnUserRemoved(user);
        }

        public override IEnumerable<User> Search(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"Argument {nameof(predicate)} is null");
            }

            List<User> result = new List<User>();
            foreach (var service in this.slaveServices)
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
            if (sub == null)
            {
                throw new ArgumentNullException($"{nameof(sub)} is null");
            }

            this.subscribers.Add(sub);
            this.UserAdded += sub.UserAdded;
            this.UserRemoved += sub.UserRemoved;
        }

        public void RemoveSubscriber(INotificationSubscriber sub)
        {
            if (sub == null)
            {
                throw new ArgumentNullException($"{nameof(sub)} is null");
            }

            if (!this.subscribers.Contains(sub))
            {
                throw new InvalidOperationException("No such subscruber was found");
            }

            this.subscribers.Remove(sub);
            this.UserAdded -= sub.UserAdded;
            this.UserRemoved -= sub.UserRemoved;
        }

        private void OnUserAdded(User user)
        {
            var x = this.UserAdded;
            this.UserAdded?.Invoke(user);
        }

        private void OnUserRemoved(User user)
        {
            this.UserRemoved?.Invoke(user);
        }
    }
}