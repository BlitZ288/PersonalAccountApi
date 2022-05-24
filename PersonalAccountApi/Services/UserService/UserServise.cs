using PersonalAccount.Domain.Core.Model;
using PersonalAccount.Domain.Core.UnitOfWork;
using PersonalAccountApi.Infrastructure.ResultService;
using PersonalAccountApi.Services.UserService.Interfaces;

namespace PersonalAccountApi.Services.UserService
{
    public class UserServise : IUserService
    {
        private readonly UnitOfWork unitOfWork;

        public UserServise()
        {
            unitOfWork = new UnitOfWork();
        }

        public Result<IEnumerable<User>> GetAllUser()
        {
            return new Result<IEnumerable<User>>() { Data = unitOfWork.Users.GetAll() };
        }
        public Result<User> GetUserById(int idUser)
        {
            return new Result<User>() { Data = unitOfWork.Users.GetById(idUser) };
        }

        public Result<User> LoginUser(string login, string password)
        {
            try
            {
                var user = unitOfWork.Users.GetByLogin(login);

                if (user == null)
                {
                    return new Result<User>() { Data = null, Error = "Пользователя с таким логином не существует" };
                }
                if (user.Password != password)
                {
                    return new Result<User>() { Data = null, Error = "Неправильно введен пароль " };
                }

                return new Result<User>() { Data = user, Error = null };
            }
            catch (Exception ex)
            {
                return new Result<User> { Data = null, Error = ex.Message };
            }
        }

        public Result<bool> RegisterUser(User user)
        {
            var newUser = unitOfWork.Users.GetByLogin(user.Name);
            if (newUser == null)
            {
                unitOfWork.Users.Create(user);
                return new Result<bool> { Data = true, Error = null };
            }
            return new Result<bool> { Data = false, Error = "Пользователя с таким логином уже существует" };
        }

        public void RemoveUser(int idUser)
        {
            unitOfWork.Users.Delete(idUser);
        }

        public void UpdateUser(User user)
        {
            unitOfWork.Users.Update(user);
        }
    }
}
