using DrinkVendingMachineSite.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachineSite.DatabaseContext
{
    public class DrinkVendingMachineDBContext : DbContext
    {
        public DrinkVendingMachineDBContext
            (DbContextOptions<DrinkVendingMachineDBContext> options) 
        : base(options) { }

        public DbSet<DrinkEntity> Drinks { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(optionsBuilder.GetConnectionString("DatabaseConnection"));
        }
        */
    }
}
