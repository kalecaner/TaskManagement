using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Persistance.Services.Email
{
    public class SmtpSettings
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string From { get; set; } = default!;
    }
}
