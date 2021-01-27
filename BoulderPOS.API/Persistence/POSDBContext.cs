using BoulderPOS.Model;
using Microsoft.EntityFrameworkCore;

namespace BoulderPOS.API.Persistence
{
    public class POSDBContext : DbContext
    {
        public POSDBContext(DbContextOptions<POSDBContext> options) : base(options)
        {

        }

        public DbSet<Customer> User { get; set; }


    }
}
