using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    class LikeTypeEntityConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> entity)
        {
            entity.HasKey(e => e.LikeId);
            entity.Property(e => e.LikeId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.HasOne<Review>(r => r.Review)
                .WithMany(l => l.Likes)
                .HasForeignKey(f => f.ReviewId);

            entity.HasOne<User>(u => u.User)
                .WithMany(l => l.Likes)
                .HasForeignKey(f => f.UserId);
        }
    }
}
