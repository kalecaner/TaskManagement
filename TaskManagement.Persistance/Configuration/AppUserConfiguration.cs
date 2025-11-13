using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Persistance.Configuration
{
	public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.Property(builder => builder.Name)
				.IsRequired(true)
				.HasMaxLength(200);
			builder.Property(builder => builder.Surname).IsRequired(true)
				.HasMaxLength(200);	
			builder.Property(builder => builder.Password).IsRequired(true)
				.HasMaxLength(200);
			builder.HasIndex(x => x.Username).IsUnique(true);
			builder.Property(builder => builder.Username).IsRequired(true)
				.HasMaxLength(200);
			builder.Property(x=>x.AppRoleId)
				.IsRequired(true);
			builder.HasMany(x => x.Tasks).WithOne(x => x.AppUser)
				.HasForeignKey(x => x.AppUserId);
			builder.HasMany(x => x.Notifications).WithOne(x => x.AppUser)
				.HasForeignKey(x => x.AppUserId);
			builder.Property(x => x.Email).HasMaxLength(250);
			builder.Property(x => x.NormalizedUsername).HasMaxLength(50).IsRequired();
			builder.HasIndex(x => x.NormalizedUsername).IsUnique(true);







        }
	}
}
