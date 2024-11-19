using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shahkar.UserManagement.AppService.Common.QueryHandlers;
using Shahkar.UserManagement.AppService.UserInfo.Queries;
using Shahkar.UserManagement.AppService.UserInfo.Views;

namespace Shahkar.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IQueryDispatcher queryDispatcher;

        public UserInfoController(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet("GetUserInfo")]
        public async Task<ActionResult> GetUserInfo([FromQuery]GetUserInfoByPhoneQuery query)
        {
           return Ok(await queryDispatcher.ExexcuteAsync<GetUserInfoByPhoneQuery, UserInfoView>(query));
        }

    }
}
