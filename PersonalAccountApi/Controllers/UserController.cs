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
        private readonly IWebHostEnvironment hostEnvironment;

        public UserController(IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            this.userService = userService;
            this.hostEnvironment = hostEnvironment;

        }

        [HttpGet]
        public Result<IEnumerable<User>> GetAll()
        {

            var result = userService.GetAllUser(Request);

            return result;
        }
        [HttpGet]
        public Result<User> GetUserById(int idUser)
        {
            var resutl = userService.GetUserById(idUser);

            return resutl;
        }
        [HttpGet]
        public void RemoveUserById(int idUser)
        {
            userService.RemoveUser(idUser);
        }
        [HttpPost]
        public async Task UpdateUser([FromForm] User user)
        {
            user.ImageName = await SaveImage(user.ImageFile);

            userService.UpdateUser(user, Request);
        }
        [HttpPost]
        public void CreateUser(User user)
        {
            userService.RegisterUser(user, Request);
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
