using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Persistance.Services.Identity
{
    public class InvariantUsernameNormalizer : IUsernameNormalizer
    {
        public string Normalize(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim().ToLowerInvariant();
        }
    }
}
