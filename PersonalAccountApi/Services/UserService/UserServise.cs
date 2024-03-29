﻿using PersonalAccount.Domain.Core.Model;
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

        public Result<IEnumerable<User>> GetAllUser(HttpRequest request)
        {
            var users = unitOfWork.Users.GetAll().Select(x => new User()
            {
                UserId = x.UserId,
                CountVisit = x.CountVisit,
                Discription = x.Discription,
                LastName = x.LastName,
                Name = x.Name,
                Password = x.Password,
                ImageName = x.ImageName,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, x.ImageName)
            }).ToList();

            return new Result<IEnumerable<User>>() { Data = users };
        }
        public Result<User> GetUserById(int idUser)
        {
            return new Result<User>() { Data = unitOfWork.Users.GetById(idUser) };
        }
        public Result<User> GetUserByName(string userName, HttpRequest request)
        {
            var user = unitOfWork.Users.GetByLogin(userName);
            user.CountVisit += 1;
            user.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, user.ImageName);

            return new Result<User>() { Data = user };
        }

        public Result<User> LoginUser(string login, string password, HttpRequest request)
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
                user.CountVisit += 1;
                unitOfWork.Users.Update(user);
                user.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, user.ImageName);

                return new Result<User>() { Data = user, Error = null };
            }
            catch (Exception ex)
            {
                return new Result<User> { Data = null, Error = ex.Message };
            }
        }

        public Result<User> RegisterUser(User user, HttpRequest request)
        {
            var newUser = unitOfWork.Users.GetByLogin(user.Name);
            if (newUser == null)
            {
                unitOfWork.Users.Create(user);
                user.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, user.ImageName);


                return new Result<User> { Data = user, Error = null };
            }
            return new Result<User> { Data = null, Error = "Пользователя с таким логином уже существует" };
        }

        public void RemoveUser(int idUser)
        {
            unitOfWork.Users.Delete(idUser);
        }

        public void UpdateUser(User user, HttpRequest request)
        {
            user.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, user.ImageName);
            unitOfWork.Users.Update(user);

        }
    }
}
