using PersonalAccount.Domain.Core.Context;
using PersonalAccount.Domain.Core.Interfaces;
using PersonalAccount.Domain.Core.Model;

namespace PersonalAccount.Domain.Core.Repositories
{
    public class UserRepository : IRepositiory<User>
    {
        private readonly PersonalContext context;

        public UserRepository(PersonalContext context)
        {
            this.context = context;
        }
        public User Get(int id)
        {
            return this.context.Users.Find(id);
        }
        public User? GetByLogin(string login)
        {
            var user = this.context.Users.FirstOrDefault(user => user.Name == login);
            return user;
        }

        public object Select()
        {
            throw new NotImplementedException();
        }

        public User GetById(int idUser)
        {
            var user = this.context.Users.Find(idUser);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users;
        }

        public void Create(User item)
        {
            this.context.Users.Add(item);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = this.context.Users.Find(id);
            if (user != null)
            {
                this.context.Users.Remove(user);
            }
            this.context.SaveChanges();
        }

        public void Update(User item)
        {
            this.context.Update(item);
            this.context.SaveChanges();
        }


    }
}
