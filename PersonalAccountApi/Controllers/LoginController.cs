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
        private readonly IWebHostEnvironment hostEnvironment;
        public LoginController(IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            this.userService = userService;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public Result<User> LoginUser(string login, string password)
        {
            var result = userService.LoginUser(login, password, Request);

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

    }

}

