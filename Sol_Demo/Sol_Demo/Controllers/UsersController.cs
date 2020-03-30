using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sol_Demo.Models;
using Sol_Demo.Repository;

namespace Sol_Demo.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository usersRepository = null;

        public UsersController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpPost("adduser")]
        public async  Task<IActionResult> AddUsersAsync([FromBody] UsersModel usersModel)
        {
            try
            {
                if (usersModel == null) return base.BadRequest();
                else
                {
                    var data =
                            await
                                this
                                .usersRepository
                                .AddUsersAsync(usersModel);

                    return base.Ok((Object)data);

                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateUsersAsync([FromBody] UsersModel usersModel)
        {
            try
            {
                if (usersModel == null) return base.BadRequest();
                else
                {
                    var data =
                            await
                                this
                                .usersRepository
                                .UpdateUsersAsync(usersModel);

                    return base.Ok((Object)data);

                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("deleteuser")]
        public async Task<IActionResult> DeleteUsersAsync([FromBody] UsersModel usersModel)
        {
            try
            {
                if (usersModel == null) return base.BadRequest();
                else
                {
                    var data =
                            await
                                this
                                .usersRepository
                                .DeleteUsersAsync(usersModel);

                    return base.Ok((Object)data);

                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("getuserlist")]
        public async Task<IActionResult> DeleteUsersAsync()
        {
            try
            {
                
                    var data =
                            await
                                this
                                .usersRepository
                                .GetUsersListAsync();

                    return base.Ok((Object)data);

                
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchUserAsync([FromBody] UsersModel usersModel)
        {
            try
            {
                if (usersModel == null) return base.BadRequest();
                else
                {
                    var data =
                        await
                            usersRepository
                            ?.SearchUsers(usersModel);

                    return base.Ok((Object)data);
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpPost("joinusers")]
        public async Task<IActionResult> GetUserListJoinAsync()
        {
            try
            {
                
                    var data =
                        await
                            usersRepository
                            ?.GetUserJoinListAsync();

                    return base.Ok((Object)data);
                
            }
            catch
            {
                throw;
            }
        }
    }
}