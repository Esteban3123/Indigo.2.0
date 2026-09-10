using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Autentication.JwtService
{
    public interface IJwtTokenService:IDisposable
    {
        Task<string> GenerateToken(string username, string container);
        Task ValidateToken(string token);
    }
}
