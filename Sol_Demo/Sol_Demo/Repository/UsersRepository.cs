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

        Task<IEnumerable<UsersModel>> SearchUsers(UsersModel usersModel);

        Task<IEnumerable<UsersModel>> GetUserJoinListAsync();
    }

    public class UsersRepository : IUsersRepository
    {
      
        private readonly IMongoDatabase mongoDatabase = null;

        public UsersRepository(IMongoDbClientDbProvider mongoDbClientDbProvider)
        {
            this.mongoDatabase = mongoDbClientDbProvider.GetConnectionWithDatabase;
        }

        async Task<dynamic> IUsersRepository.AddUsersAsync(UsersModel usersModel)
        {
            try
            {

                await
                    mongoDatabase
                    ?.GetCollection<UsersModel>("UserCollection")
                    ?.InsertOneAsync(usersModel);

                // get Identity Value
                string id = usersModel?.Id;

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

        Task<IEnumerable<UsersModel>> IUsersRepository.GetUserJoinListAsync()
        {
            try
            {
                var userCollectionData = mongoDatabase.GetCollection<UsersModel>("UserCollection");
                var userLoginCollectionData = mongoDatabase.GetCollection<UserLoginModel>("UseLoginCollection");

                var joinData =
                            userCollectionData.AsQueryable()
                            ?.Join<UsersModel, UserLoginModel, String, UsersModel>(
                                    userLoginCollectionData.AsQueryable(),
                                    (leUserCollectionObj) => leUserCollectionObj.Id,
                                    (leUserLoginCollectionObj) => leUserLoginCollectionObj.UserId,
                                    (leUserCollectionObj, leUserLoginCollectionObj) => new UsersModel()
                                    {
                                        Id = leUserCollectionObj.Id,
                                        FirstName = leUserCollectionObj.FirstName,
                                        LastName = leUserCollectionObj.LastName,
                                        Age = leUserCollectionObj.Age,
                                        UserLoginModel = new UserLoginModel()
                                        {
                                            Id=leUserLoginCollectionObj.Id,
                                            UserName = leUserLoginCollectionObj.UserName,
                                            Password = leUserLoginCollectionObj.Password
                                        }
                                    }
                                    )
                                ?.ToList();
                return Task.FromResult<IEnumerable<UsersModel>>(joinData);
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

        async Task<IEnumerable<UsersModel>> IUsersRepository.SearchUsers(UsersModel usersModel)
        {
            try
            {
                var data =
                    await
                    mongoDatabase
                    ?.GetCollection<UsersModel>("UserCollection")
                    ?.Find((leFilter) => leFilter.FirstName == usersModel.FirstName)
                    ?.ToListAsync();

                return data;    
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
