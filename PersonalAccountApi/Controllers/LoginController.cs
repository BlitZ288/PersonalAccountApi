using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Domain.Core.Model;
using PersonalAccountApi.Infrastructure.ResultService;
using PersonalAccountApi.Services.UserService.Interfaces;

namespace PersonalAccountApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        IUserService userService;
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public Result<User> LoginUser(string login, string password)
        {
            var result = userService.LoginUser(login, password);

            return result;
        }
        [HttpPost]
        public Result<bool> RegisterUser(User user)
        {
            var result = userService.RegisterUser(user);

            return result;
        }
    }
}
