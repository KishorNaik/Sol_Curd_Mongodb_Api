using Sol_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Sol_Demo.Repository
{
    public interface IUsersRepository
    {
        Task<dynamic> AddUsersAsync(UsersModel usersModel);
        Task<dynamic> UpdateUsersAsync(UsersModel usersModel);

        Task<dynamic> DeleteUsersAsync(UsersModel usersModel);

        Task<IEnumerable<UsersModel>> GetUsersListAsync();
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly IMongoDbClientDbProvider mongoDbClientDbProvider=null;
        private readonly IMongoDatabase mongoDatabase = null;

        public UsersRepository(IMongoDbClientDbProvider mongoDbClientDbProvider)
        {
            this.mongoDbClientDbProvider = mongoDbClientDbProvider;
            this.mongoDatabase = this.mongoDbClientDbProvider.GetConnectionWithDatabase;
        }

        async Task<dynamic> IUsersRepository.AddUsersAsync(UsersModel usersModel)
        {
            try
            {
               
                await 
                    mongoDatabase
                    ?.GetCollection<UsersModel>("UserCollection")
                    ?.InsertOneAsync(usersModel);

                return true;
            }
            catch
            {
                throw;
            }
        }

        async Task<dynamic> IUsersRepository.DeleteUsersAsync(UsersModel usersModel)
        {
            try
            {
                await 
                    mongoDatabase
                    ?.GetCollection<UsersModel>("UserCollection")
                    ?.DeleteOneAsync((leFilter) => leFilter.Id == usersModel.Id);

                return true;
            }
            catch
            {
                throw;
            }
        }

        async Task<IEnumerable<UsersModel>> IUsersRepository.GetUsersListAsync()
        {
           try
            {
                return

                await mongoDatabase
                    ?.GetCollection<UsersModel>("UserCollection")
                    ?.Find((leFilter) => true)
                    ?.ToListAsync();
                
                        
            }
            catch
            {
                throw;
            }
        }

        async Task<dynamic> IUsersRepository.UpdateUsersAsync(UsersModel usersModel)
        {
            try
            {
                await
                    mongoDatabase
                    ?.GetCollection<UsersModel>("UserCollection")
                    ?.ReplaceOneAsync((leFilter) => leFilter.Id == usersModel.Id, usersModel);

                return true;
            }
            catch
            {
                throw;
            }
        }

       
    }
}
