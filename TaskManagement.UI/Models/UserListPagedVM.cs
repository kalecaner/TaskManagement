using TaskManagement.Domain.Entities;

namespace TaskManagement.UI.Models
{
    public class UserListPagedVM
    {
               
        public int ActivePage { get; set; }
        public int PageSize { get; set; }

        public int TotalPages { get; set; }
        public List<UsersVM> Users { get; set; }

    }
    public sealed record UsersVM(int Id, string Name, string Surname, string Username, int RoleId);
}
