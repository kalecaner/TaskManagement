using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entities
{
    public class PasswordResetToken:BaseEntity
    {
        public string Token { get; set; } = null!;
        public DateTime ExpireDate { get; set; }
        public bool IsUsed { get; set; } = false;

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
