using System.Collections.Generic;
using UserStorageServices;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageService"/>.
    /// </summary>
    public class Client
    {
        private readonly IUserStorageService _userStorageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null)
        {
            _userStorageService = userStorageService;
            if (_userStorageService == null)
            {
                _userStorageService = new UserStorageLog(new UserStorageServiceMaster(new UserMemoryCacheWithState()));
            }
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageService"/> class.
        /// </summary>
        public void Run()
        {
            UserMemoryCacheWithState repository = new UserMemoryCacheWithState();
            repository.Start();

            _userStorageService.Add(new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            });

            UserStorageServiceMaster m = new UserStorageServiceMaster(repository, new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(new UserMemoryCacheWithState()), new UserStorageServiceSlave(new UserMemoryCacheWithState()), }));

            m.AddSubscriber(new UserStorageServiceSlave(repository));

            m.Add(new User()
            {
                FirstName = "a",
                LastName = "b",
                Age = 55
            });

            repository.Stop();

            ///_userStorageService.Remove(null);

            ///_userStorageService.Search(null);
        }
    }
}