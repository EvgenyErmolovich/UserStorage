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
        private readonly IUserRepositoryManager repositoryManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserRepositoryManager repositoryManager = null)
        {
            this.repositoryManager = repositoryManager ?? new UserRepositoryWithState();
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageService"/> class.
        /// </summary>
        public void Run()
        {
            var filePath = ConfigurationManager.AppSettings["FilePath"];
            repositoryManager.Start();

            UserStorageServiceMaster m = new UserStorageServiceMaster((IUserRepository)repositoryManager, new List<UserStorageServiceSlave>(new[] { new UserStorageServiceSlave(new UserRepositoryWithState()), new UserStorageServiceSlave(new UserRepositoryWithState()) }));
            m.AddSubscriber(new UserStorageServiceSlave((IUserRepository)repositoryManager));
            m.Add(new User()
            {
                FirstName = "a",
                LastName = "b",
                Age = 55
            });

            repositoryManager.Stop();
            ///_userStorageService.Remove(null);

            ///_userStorageService.Search(null);
        }
    }
}