using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;
using Taxi.Infrastructure.Data.EntityConfigurations;

namespace Taxi.Infrastructure.Data
{
    public class TaxiContext : DbContext
    {
        public TaxiContext(DbContextOptions<TaxiContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Notify> Notifies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AdminTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DriverTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReportTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LikeTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CommentTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NotifyTypeEntityConfiguration());
        }
    }
}
