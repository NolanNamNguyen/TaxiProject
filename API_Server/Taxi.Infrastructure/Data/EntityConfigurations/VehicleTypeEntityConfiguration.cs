using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class VehicleTypeEntityConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> entity)
        {
            entity.HasKey(e => e.VehicleId);
            entity.Property(e => e.VehicleId)
            .ValueGeneratedOnAdd()
            .IsRequired();
            entity.Property(e => e.License)
            .HasMaxLength(20)
            .IsRequired();
            entity.Property(e => e.VehicleName)
            .HasMaxLength(100)
            .IsRequired();
            entity.Property(e => e.Seater)
            .IsRequired();
        }
    }
}
