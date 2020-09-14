using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class PromotionTypeEntityConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> entity)
        {
            entity.HasKey(e => e.PromotionId);
            entity.Property(e => e.PromotionId)
            .ValueGeneratedOnAdd()
            .IsRequired();
            entity.Property(e => e.Content)
            .IsRequired();
        }
    }
}
