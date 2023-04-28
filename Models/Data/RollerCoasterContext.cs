using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RollerCoasterAPI.Models.Classes;
using RollerCoasterAPI.Models.Enums;

namespace RollerCoasterAPI.Models.Data
{
    public class RollerCoasterContext : DbContext
    {
        public RollerCoasterContext()
        {
            
        }

        public RollerCoasterContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Attraction> Attractions { get; set; } = null!;
        public DbSet<Error> Errors { get; set; } = null!;
        public DbSet<Manufacturer> Manufacturerers { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<Classes.OperatingStatus> OperatingStatuses { get; set; } = null!;
        public DbSet<Owner> Owners { get; set; } = null!;     
        public DbSet<Ride> Rides { get; set; } = null!;
        public DbSet<RollerCoaster> RollerCoasters { get; set; } = null!;
        public DbSet<ThemePark> ThemeParks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            Console.WriteLine("CS - " + configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Classes.NoteType>()
            .HasData(Enum.GetValues(typeof(Enums.NoteType))
                .Cast<Enums.NoteType>()
                .Select(e => new Classes.NoteType
                {
                    Id = (short)e,
                    Type = e.ToString()
                })
            );

            modelBuilder
            .Entity<Classes.RollerCoasterType>()
            .HasData(Enum.GetValues(typeof(Enums.RollerCoasterType))
                .Cast<Enums.RollerCoasterType>()
                .Select(e => new Classes.RollerCoasterType
                {
                    Id = (short)e,
                    Type = e.ToString()
                })
            );

            modelBuilder
            .Entity<Classes.RideType>()
            .HasData(Enum.GetValues(typeof(Enums.RideType))
                .Cast<Enums.RideType>()
                .Select(e => new Classes.RideType
                {
                    Id = (short)e,
                    Type = e.ToString()
                })
            );

            modelBuilder
            .Entity<Classes.RollerCoasterDesign>()
            .HasData(Enum.GetValues(typeof(Enums.RollerCoasterDesign))
                .Cast<Enums.RollerCoasterDesign>()
                .Select(e => new Classes.RollerCoasterDesign
                {
                    Id = (short)e,
                    Design = e.ToString()
                })
            );

            modelBuilder
            .Entity<Classes.OperatingStatus>()
            .HasData(Enum.GetValues(typeof(Enums.OperatingStatus))
                .Cast<Enums.OperatingStatus>()
                .Select(e => new Classes.OperatingStatus
                {
                    Id = (short)e,
                    Status = e.ToString()
                })
            );     
        }
    }
}