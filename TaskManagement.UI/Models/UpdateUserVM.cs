using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagement.Application.Enums;

namespace TaskManagement.UI.Models
{
    public class UpdateUserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }       
        public string Email { get; set; }
        public int RoleId { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public UpdateUserVM()
        {
            Roles = Enum.GetValues(typeof(RoleType))
                    .Cast<RoleType>()
                    .Select(x => new SelectListItem
                    {
                        Value = ((int)x).ToString(),
                        Text = x.ToString()
                    }).ToList();

        }
    }
}
