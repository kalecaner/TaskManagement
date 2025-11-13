using MediatR;
using TaskManagement.Application.Dtos;
using static TaskManagement.Application.Requests.ApplicationRequests;

namespace TaskManagement.Application.Requests
{
	public record LoginRequest(string? Username, string? Password,bool RememberMe=false):IRequest<Result<LoginResponseDto?>>;
	public record RegisterRequest(string? Username, string? Password,string? ConfirmPassword, string? Name,string? Surname, string Email) :IRequest<Result<NoData>>;

   public record MemberPageListRequest: PagedRequest, IRequest<PagedResult<MemberDto>>
	   {
        public string? SearchString { get; set; }
        public MemberPageListRequest(int activePage, string? searchString) : base(activePage)
		 {
			  SearchString = searchString;
        }
    }

   public  record CreateUserRequest(string? Username, string? Password, string? ConfirmPassword, string Email, string? Name, string? Surname, int RoleId) : IRequest<Result<NoData>>;

	public record SendPasswordResetMailRequest(string Username):IRequest<Result<NoData>>;
	public record ResetPasswordRequest(string Token,string NewPassword):IRequest<Result<NoData>>;
	public record GetUserByIdRequest(int UserId):IRequest<Result<UpdateDto?>>;
	public record UpdateUserRequest(int Id,string? Username,string? Email,string? Name, string? Surname, int RoleId):IRequest<Result<NoData>>;
	 public record DeleteUserRequest(int UserId):IRequest<Result<NoData>>;


}
