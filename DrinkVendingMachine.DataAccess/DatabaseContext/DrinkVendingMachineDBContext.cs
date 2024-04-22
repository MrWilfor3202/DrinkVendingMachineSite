using DrinkVendingMachine.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachine.DataAccess.DatabaseContext
{
    public class DrinkVendingMachineDBContext : DbContext
    {
        public DrinkVendingMachineDBContext
            (DbContextOptions<DrinkVendingMachineDBContext> options) 
        : base(options) { }

        public DbSet<DrinkEntity> Drinks { get; set; }
       
        public DbSet<CoinEntity> Coins { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(optionsBuilder.GetConnectionString("DatabaseConnection"));
        }
        */
    }
}
