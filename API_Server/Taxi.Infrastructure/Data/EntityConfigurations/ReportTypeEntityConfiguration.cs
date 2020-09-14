using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class ReportTypeEntityConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> entity)
        {
            entity.HasKey(e => e.ReportId);
            entity.Property(e => e.ReportId)
            .ValueGeneratedOnAdd()
            .IsRequired();
            entity.Property(e => e.Content)
            .IsRequired();

            entity.HasOne<Driver>(d => d.Driver)
            .WithMany(r => r.Reports)
            .HasForeignKey(dr => dr.DriverId);

            entity.HasOne<Customer>(c => c.Customer)
            .WithMany(r => r.Reports)
            .HasForeignKey(ct => ct.CustomerId);
        }
    }
}
