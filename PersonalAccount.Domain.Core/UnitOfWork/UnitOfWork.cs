using PersonalAccount.Domain.Core.Context;
using PersonalAccount.Domain.Core.Repositories;


namespace PersonalAccount.Domain.Core.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private PersonalContext context = new PersonalContext();
        private UserRepository userRepository;


        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(context);
                return userRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
