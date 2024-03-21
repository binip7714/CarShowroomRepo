using BMCar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMCar.Data
{
    public class CarApplicationDbContext : IdentityDbContext
    {
        public CarApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CarLeadEntity> CarLead { get; set; }
    }
}
