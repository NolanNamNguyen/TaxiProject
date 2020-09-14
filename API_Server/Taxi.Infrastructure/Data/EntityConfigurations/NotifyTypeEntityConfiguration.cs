using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class NotifyTypeEntityConfiguration : IEntityTypeConfiguration<Notify>
    {
        public void Configure(EntityTypeBuilder<Notify> entity)
        {
            entity.HasKey(e => e.NotifyId);
            entity.Property(e => e.NotifyId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.HasOne<User>(u => u.User)
                .WithMany(n => n.Notifies)
                .HasForeignKey(e => e.UserId);
        }
    }
}
