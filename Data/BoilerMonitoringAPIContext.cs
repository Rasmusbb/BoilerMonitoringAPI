using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoilerMonitoringAPI.Models;

namespace BoilerMonitoringAPI.Data
{
    public class BoilerMonitoringAPIContext : DbContext
    {
        public BoilerMonitoringAPIContext (DbContextOptions<BoilerMonitoringAPIContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<HomeMembers>()
                .HasKey(ur => new { ur.UserID, ur.HomeID });


            modelBuilder.Entity<HomeMembers>()
            .HasOne(hm => hm.Home)
            .WithMany(h => h.Members)
            .HasForeignKey(hm => hm.HomeID);

            modelBuilder.Entity<HomeMembers>()
                .HasOne(hm => hm.User)
                .WithMany(m => m.Homes)
                .HasForeignKey(hm => hm.UserID);


            modelBuilder.Entity<Boiler>()
                .HasOne(b => b.Devices)
                .WithOne(d => d.Boiler)
                .HasForeignKey<Boiler>(b => b.DeviceID);


        }
        public DbSet<Boiler> Boilers { get; set; } = default!;

        public DbSet<HomeMembers> HomeMembers{ get; set; } = default!; 
        public DbSet<Home> Homes { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;  
        public DbSet<Device> Devices { get; set; } = default!; 
    }
}
