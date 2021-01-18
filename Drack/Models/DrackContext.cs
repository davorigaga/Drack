using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Drack.Models
{
    public class DrackContext : DbContext
    {
        public DrackContext()

        : base("DefaultConnection")

        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<News> News { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        public System.Data.Entity.DbSet<Drack.Models.Garage> Garages { get; set; }

        public System.Data.Entity.DbSet<Drack.Models.GarageChairman> GarageChairmen { get; set; }

        public System.Data.Entity.DbSet<Drack.Models.DriverLicense> DriverLicenses { get; set; }

        public System.Data.Entity.DbSet<Drack.Models.DriverNextOfKin> DriverNextOfKins { get; set; }

        public System.Data.Entity.DbSet<Drack.Models.Payment> Payments { get; set; }
    }

}