using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Persistance.Services.Security
{
    public class PasswordResetTokenGenerator : IPasswordResetTokenGenerator
    {
        public string Generate()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=","").Replace("+","").Replace("/","");
        }
    }
}
