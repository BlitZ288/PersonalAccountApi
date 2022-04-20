using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Domain.Core.Model;
using PersonalAccountApi.Infrastructure.ResultService;
using PersonalAccountApi.Services.UserService.Interfaces;

namespace PersonalAccountApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public Result<IEnumerable<User>> GetAll()
        {
            var result = userService.GetAllUser();

            return result;
        }
        [HttpPost]
        public void RemoveUserById(int idUser)
        {
            userService.RemoveUser(idUser);
        }
        [HttpPost]
        public void UpdateUser(User user)
        {
            userService.UpdateUser(user);
        }
    }
}
