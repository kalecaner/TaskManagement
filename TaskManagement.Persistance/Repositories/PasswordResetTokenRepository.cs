using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Persistance.Context;

namespace TaskManagement.Persistance.Repositories
{
    public class PasswordResetTokenRepository : GenericRepository<PasswordResetToken>, IPasswordResetTokenRepository
    {
        
        public PasswordResetTokenRepository(TaskManagementContext context) : base(context)
        {
        }

        public Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken ct = default)
        {
          return  _context.PasswordResetTokens.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Token == token, ct);

        }
    }
}
