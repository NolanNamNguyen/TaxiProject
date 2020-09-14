using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class ScheduleTypeEntityConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> entity)
        {
            entity.HasKey(e => e.ScheduleId);
            entity.Property(e => e.ScheduleId)
            .ValueGeneratedOnAdd()
            .IsRequired();
            entity.Property(e => e.StartProvince)
            .IsRequired();
            entity.Property(e => e.DestinationProvince)
            .IsRequired();
        }
    }
}
