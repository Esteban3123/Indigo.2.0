using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Autentication.Interfaces
{
   public interface ISecretProvider
    {
        Task<string> GetSecretAsync(string secretName);
    }
}
