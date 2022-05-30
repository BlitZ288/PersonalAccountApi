using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Domain.Core.Model;
using PersonalAccountApi.Infrastructure.ResultService;
using PersonalAccountApi.Services.UserService.Interfaces;
using System.Security.Claims;

namespace PersonalAccountApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        IUserService userService;
        private readonly IWebHostEnvironment hostEnvironment;
        public LoginController(IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            this.userService = userService;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public Result<User> GetMe()
        {
            if (User.Identity.IsAuthenticated)
            {

                return userService.GetUserByName(User.Identity.Name, Request);
            }
            return new Result<User>() { Data = null, Error = "Пользователь не аутифицирован" };
        }
        [HttpGet]
        public async void Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }

        [HttpGet]
        public async Task<Result<User>> LoginUser(string login, string password, bool remember)
        {
            login = login.Trim();
            password = password.Trim();
            var result = userService.LoginUser(login, password, Request);

            if (result.Data != null && remember)
            {
                await Authenticate(login);
            }

            return result;
        }

        [HttpPost]
        public async Task<Result<User>> RegisterUser([FromForm] User user)
        {
            user.ImageName = await SaveImage(user.ImageFile);

            var result = userService.RegisterUser(user, Request);

            return result;
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imgaePath = Path.Combine(hostEnvironment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imgaePath, FileMode.Create))
            {

                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }

}

