using Microsoft.EntityFrameworkCore;

namespace SodaMachine.Database
{
    public class SodaMachineDBContext : DbContext
    {
        public SodaMachineDBContext(DbContextOptions<SodaMachineDBContext> options) : base(options)
        {
        }
        public DbSet<Models.Machine> SodaMachines { get; set; }
    }
}
