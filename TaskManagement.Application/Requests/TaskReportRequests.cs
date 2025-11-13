using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Requests
{
   public record TaskReportListGetByAppTaskIdRequest(int AppTaskId):IRequest<Result<List<TaskReportListDto>>>;
}
