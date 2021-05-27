using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.DB.Entities;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming

namespace REFame.PasswordManagement.DB.Internals
{
    public class Context : DbContext
    {
        public Context()
        {
            
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        public DbSet<PASSWORDDATA> PASSWORDDATA { get; set; }

        public DbSet<USERDATA> USERDATA { get; set; }

        public DbSet<USERTHEME> USERTHEME { get; set; }

    }
}