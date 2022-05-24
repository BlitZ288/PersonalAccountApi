using Microsoft.EntityFrameworkCore;
using PersonalAccount.Domain.Core.Model;

namespace PersonalAccount.Domain.Core.Context
{
    public class PersonalContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public PersonalContext(DbContextOptions<PersonalContext> options) : base(options)
        {



        }
        public PersonalContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Host=localhost;Port=5432;Database=userDb;Username=postgres;Password=12345");
    }
}