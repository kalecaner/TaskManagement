using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Persistance.Configuration
{
    public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.Property(x => x.Token)
                .IsRequired(true)
                .HasMaxLength(200);
            builder.HasIndex(x => x.Token)
                .IsUnique(true);
            builder.HasOne(x=>x.AppUser)
                .WithMany()
                .HasForeignKey(x=>x.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
