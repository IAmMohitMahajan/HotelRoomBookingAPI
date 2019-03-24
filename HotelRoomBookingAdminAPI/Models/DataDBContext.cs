using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelRoomBookingAdminAPI.Models
{
    public class DataDBContext :DbContext
    {
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<HotelType> HotelTypes { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-CVPECQF\\SQLEXPRESS;Initial Catalog=DBAPIHotelRoomBookingDb;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasOne(h => h.HotelType).WithMany(b => b.Hotels).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<HotelType>().HasOne(h => h.UserDetail).WithMany(b => b.HotelTypes).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Booking>().HasOne(h => h.Customer).WithMany(b => b.Bookings).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<HotelRoom>().HasOne(h => h.Hotel).WithMany(b => b.HotelRooms).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<HotelRoom>().HasOne(h => h.Booking).WithMany(b => b.HotelRooms).OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Customer>().HasOne(h => h.Payment).WithOne(b => b.Customer).OnDelete(DeleteBehavior.SetNull);
            //modelBuilder.Entity<HotelRoom>().HasOne(h => h.RoomFacility).WithOne(b => b.HotelRoom).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
