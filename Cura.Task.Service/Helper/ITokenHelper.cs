using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cura.Task.Service.Helper
{
    public interface ITokenHelper
    {
        Task<string> GenerateToken(List<Claim> claims);
    }
}
