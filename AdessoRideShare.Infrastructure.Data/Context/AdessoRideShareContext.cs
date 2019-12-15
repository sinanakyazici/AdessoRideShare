using AdessoRideShare.Domain.Models;
using AdessoRideShare.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AdessoRideShare.Infrastructure.Data.Context
{
    public class AdessoRideShareContext : DbContext
    {
        private readonly IHostingEnvironment _env;

        public AdessoRideShareContext(IHostingEnvironment env)
        {
            _env = env;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<RidePlan> RidePlans { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new RidePlanMap());
            modelBuilder.ApplyConfiguration(new BookingMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
