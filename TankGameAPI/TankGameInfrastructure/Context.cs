using Microsoft.EntityFrameworkCore;
using TankGameDomain;

namespace TankGameInfrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public Field Field { get; set; }
    }
}
