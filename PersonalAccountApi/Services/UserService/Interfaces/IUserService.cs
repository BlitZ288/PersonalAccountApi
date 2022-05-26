using PersonalAccount.Domain.Core.Model;
using PersonalAccountApi.Infrastructure.ResultService;

namespace PersonalAccountApi.Services.UserService.Interfaces
{
    public interface IUserService
    {
        Result<User> LoginUser(string login, string password, HttpRequest request);
        Result<User> RegisterUser(User user, HttpRequest request);
        void RemoveUser(int idUser);
        void UpdateUser(User user, HttpRequest request);
        Result<IEnumerable<User>> GetAllUser(HttpRequest request);
        Result<User> GetUserById(int idUser);


    }
}
