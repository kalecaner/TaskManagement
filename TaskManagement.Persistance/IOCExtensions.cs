
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Persistance.Configuration;
using TaskManagement.Persistance.Context;
using TaskManagement.Persistance.Repositories;
using TaskManagement.Persistance.Services.Email;
using TaskManagement.Persistance.Services.Identity;
using TaskManagement.Persistance.Services.Link;
using TaskManagement.Persistance.Services.Security;


namespace TaskManagement.Persistance
{
	public static class IOCExtensions
	{
		public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<TaskManagementContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

			});
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IPriorityRepository, PriorityRepository>();
			services.AddScoped<IAppTaskRepository, AppTaskRepository>();
			services.AddScoped<ITaskReportRepository, TaskReportRepository>();
			services.AddScoped<IUsernameNormalizer, InvariantUsernameNormalizer>();
			services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
			services.AddScoped<IPasswordResetTokenGenerator, PasswordResetTokenGenerator>();
            services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
            services.AddScoped<IResetLinkBuilder, ResetLinkBuilder>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();
			services.AddScoped<INotificationRepository, NotificationRepository>();


        }
    }
}
