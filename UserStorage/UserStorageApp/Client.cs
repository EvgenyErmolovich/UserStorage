using System.Collections.Generic;
using System.Configuration;
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
                _userStorageService = new UserStorageLog(new UserStorageServiceMaster(new UserRepositoryWithState()));
            }
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageService"/> class.
        /// </summary>
        public void Run()
        {
            var filePath = ConfigurationManager.AppSettings["FilePath"];
            UserRepositoryWithState repository = new UserRepositoryWithState(filePath);

            repository.Start();

            UserStorageServiceMaster m = new UserStorageServiceMaster(repository, new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(new UserRepositoryWithState()), new UserStorageServiceSlave(new UserRepositoryWithState()), }));

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