using PersonalAccount.Domain.Core.Model;
using System.Data.Entity;

namespace PersonalAccount.Domain.Core.Context
{
    public class PersonalContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}