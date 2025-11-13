using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Extensions
{
	public static class IOCExtension
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddMediatR(configuration=>configuration.RegisterServicesFromAssembly(typeof(LoginRequest).Assembly));
		}
	}
}
