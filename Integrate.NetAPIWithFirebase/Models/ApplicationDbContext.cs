using APIWithFireBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Integrate.NetAPIWithFirebase.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<BabyTemperature> BabyTemperatures { get; set; }
    }
}
