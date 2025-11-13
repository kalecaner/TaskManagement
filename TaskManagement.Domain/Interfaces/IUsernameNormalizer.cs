using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Interfaces
{
    public interface IUsernameNormalizer
    {
        string Normalize(string input);
    }
}
