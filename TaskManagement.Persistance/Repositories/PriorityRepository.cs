using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistance.Context;

namespace TaskManagement.Persistance.Repositories
{
	public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
	{
		
        public PriorityRepository(TaskManagementContext context) : base(context)
        {
        }
    }
}
