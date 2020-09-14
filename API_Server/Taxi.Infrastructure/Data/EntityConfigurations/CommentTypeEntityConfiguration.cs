using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Taxi.Domain.Entities;

namespace Taxi.Infrastructure.Data.EntityConfigurations
{
    public class CommentTypeEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.HasKey(e => e.CommentId);
            entity.Property(e => e.CommentId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.HasOne<Review>(r => r.Review)
                .WithMany(e => e.Comments)
                .HasForeignKey(f => f.ReviewId);

            entity.HasOne<User>(u => u.User)
                .WithMany(e => e.Comments)
                .HasForeignKey(f => f.UserId);
        }
    }
}
