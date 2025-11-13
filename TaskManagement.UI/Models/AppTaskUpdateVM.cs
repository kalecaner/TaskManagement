using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManagement.UI.Models
{
    public class AppTaskUpdateVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PriorityId { get; set; }
        public int? AppUserId { get; set; }
        public List<SelectListItem> Priorities { get; set; } = new();
        public List<SelectListItem> Users { get; set; } = new();
    }
}
