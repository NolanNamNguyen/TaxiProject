using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class UserTypeEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId)
            .ValueGeneratedOnAdd()
            .IsRequired();
            entity.Property(e => e.UserName)
            .HasMaxLength(20)
            .IsRequired();
            entity.Property(e => e.Role)
            .IsRequired();

            entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();
            entity.Property(e => e.Phone)
            .HasMaxLength(15)
            .IsRequired();

            //tao cac quan he 1 - 1: 
            entity.HasOne<Admin>(a => a.Admin)
            .WithOne(u => u.User)
            .HasForeignKey<Admin>(ud => ud.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Driver>(d => d.Driver)
            .WithOne(u => u.User)
            .HasForeignKey<Driver>(ud => ud.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Customer>(c => c.Customer)
            .WithOne(u => u.User)
            .HasForeignKey<Customer>(ud => ud.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
