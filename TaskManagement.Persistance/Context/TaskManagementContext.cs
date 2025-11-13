using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistance.Configuration;

namespace TaskManagement.Persistance.Context
{
	public class TaskManagementContext : DbContext
	{
		public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options)
		{
		}
		public DbSet<AppRole> Roles { get; set; }
		public DbSet<AppUser> Users { get; set; }
		public DbSet<AppTask> Tasks { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Priority> Priorities { get; set; }
		public DbSet<TaskReport> TaskReports { get; set; }
		public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
			modelBuilder.ApplyConfiguration(new AppUserConfiguration());
			modelBuilder.ApplyConfiguration(new AppTaskConfiguration());
			modelBuilder.ApplyConfiguration(new NotificationConfiguration());
			modelBuilder.ApplyConfiguration(new PriorityConfiguration());
			modelBuilder.ApplyConfiguration(new TaskReportConfiguration());
			modelBuilder.ApplyConfiguration(new PasswordResetTokenConfiguration());

            #region Normalde bu şekilde  entity configurasyonlarını yazabilirsin ancak  Bu SRP ye aykırı bir yaklaşımdır ve yazılmamalıdır.
            //modelBuilder.Entity<AppRole>(entity =>
            //	{
            //		entity.Property(x => x.Definition).HasMaxLength(100);

            //	});

            //modelBuilder.Entity<AppUser>().ToTable("Users");
            //modelBuilder.Entity<AppRole>().ToTable("Roles");
            //modelBuilder.Entity<AppTask>().ToTable("Tasks");
            //modelBuilder.Entity<Notification>().ToTable("Notifications");
            //modelBuilder.Entity<Priority>().ToTable("Priorities");
            //modelBuilder.Entity<TaskReport>().ToTable("TaskReports");
            //base.OnModelCreating(modelBuilder); 
            #endregion
            //Bu yaklaşım yerine fluent api ile entity configurasyonlarını ayrı bir class içinde yazmak daha doğrudur.


        }


		


	}
}
