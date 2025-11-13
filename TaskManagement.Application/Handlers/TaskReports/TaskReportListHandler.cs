using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Handlers.TaskReports
{
    public class TaskReportListHandler : IRequestHandler<TaskReportListGetByAppTaskIdRequest, Result<List<TaskReportListDto>>>
    {
        private readonly ITaskReportRepository _taskReportRepository;

        public TaskReportListHandler(ITaskReportRepository taskReportRepository)
        {
            _taskReportRepository = taskReportRepository;
        }

        public async Task<Result<List<TaskReportListDto>>> Handle(TaskReportListGetByAppTaskIdRequest request, CancellationToken cancellationToken)
        {
            var reports = await _taskReportRepository.GetListAsNoTrackingByFilterAsync(x => x.AppTaskId == request.AppTaskId);
            if (reports==null)
            {

                return new Result<List<TaskReportListDto>>(null, false, "No reports found for the specified task.", null);
                

            }
            else
            {
                var reportDtos = reports.ToMap();
                return new Result<List<TaskReportListDto>>(reportDtos, true, null, null);
            }



        }
    }
}
