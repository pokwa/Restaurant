using IDataInterface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class RestaurantContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Restaurant;Trusted_connection=true");
        }

        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Table> Tables { get; set; }

        public DbSet<Dish> Menu { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
