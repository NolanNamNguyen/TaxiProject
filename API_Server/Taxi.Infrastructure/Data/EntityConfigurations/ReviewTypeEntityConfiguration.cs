using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class ReviewTypeEntityConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> entity)
        {
            entity.HasKey(e => e.ReviewId);
            entity.Property(e => e.ReviewId)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasOne<Driver>(d => d.Driver)
            .WithMany(r => r.Reviews)
            .HasForeignKey(dr => dr.DriverId);

            entity.HasOne<Customer>(c => c.Customer)
            .WithMany(r => r.Reviews)
            .HasForeignKey(ct => ct.CustomerId);
        }
    }
}
