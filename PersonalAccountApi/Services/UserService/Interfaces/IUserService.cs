using PersonalAccount.Domain.Core.Model;
using PersonalAccountApi.Infrastructure.ResultService;

namespace PersonalAccountApi.Services.UserService.Interfaces
{
    public interface IUserService
    {
        Result<User> LoginUser(string login, string password);
        Result<bool> RegisterUser(User user);
        void RemoveUser(int idUser);
        void UpdateUser(User user);
        Result<IEnumerable<User>> GetAllUser();


    }
}
