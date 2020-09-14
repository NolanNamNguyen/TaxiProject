using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class DriverTypeEntityConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> entity)
        {
            entity.HasKey(e => e.DriverId);
            entity.Property(e => e.DriverId)
            .ValueGeneratedOnAdd()
            .IsRequired();

            entity.HasOne<Schedule>(s => s.Schedule)
            .WithOne(dr => dr.Driver)
            .HasForeignKey<Schedule>(si => si.DriverId);

            entity.HasOne<Vehicle>(v => v.Vehicle)
            .WithOne(dr => dr.Driver)
            .HasForeignKey<Vehicle>(vh => vh.DriverId);
        }
    }
}
