using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistance.Context;

namespace TaskManagement.Persistance.Repositories
{
    public class TaskReportRepository : GenericRepository<TaskReport>, ITaskReportRepository
    {
        public TaskReportRepository(TaskManagementContext context) : base(context)
        {
        }
    }
}
