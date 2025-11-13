using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Dtos
{
	public record Result<T>(T data,bool isSuccess, string? ErrorMessage,List<ValidationError>? Errors);
	public record ValidationError(string PropertyName, string ErrorMessage);
	public record NoData();
	public record PagedResult<T>(List<T> Data, int ActivePage, int PageSize,  int TotalPages);
    public record PagedData<T> where T : class, new()
    {
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int ActivePage { get; set; }
        public PagedData(List<T> data, int activePage, int pageSize, int totalRecords)
        {
            this.Data = data;
            this.TotalRecords = totalRecords;
            this.PageSize = pageSize;
            this.ActivePage = activePage;
            this.TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
        }

    }

}
