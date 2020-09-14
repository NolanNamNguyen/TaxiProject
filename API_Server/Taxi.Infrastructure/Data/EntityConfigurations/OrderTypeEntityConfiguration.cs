using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class OrderTypeEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.HasKey(e => e.OrderId);
            entity.Property(e => e.OrderId)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasOne<Driver>(d => d.Driver)
            .WithMany(o => o.Orders)
            .HasForeignKey(dr => dr.DriverId);

            entity.HasOne<Customer>(c => c.Customer)
            .WithMany(o => o.Orders)
            .HasForeignKey(ct => ct.CustomerId);
        }
    }
}
