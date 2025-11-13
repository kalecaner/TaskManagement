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
	public record PriorityListRequest():IRequest<Result<List<PriorityListDto>>>;
	
	public record PriorityUpdateRequest(int Id, string? Definition):IRequest<Result<NoData>>;
	
	public record PriorityGetByIdRequest(int Id):IRequest<Result<PriorityListDto>>;

	public record PriorityCreateRequest(string? Definition):IRequest<Result<NoData>>;
	public record PriorityDeleteRequest(int Id):IRequest<Result<NoData>>;



}
