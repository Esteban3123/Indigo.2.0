using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Domain.Autentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Autentication.Secrets.AZKeyVault
{
    public class KeyVaultSecretProvider : ISecretProvider
    {
        private readonly SecretClient _secretClient;

        public KeyVaultSecretProvider(string keyVaultUrl,string tenantId,string clientId, string clientSecret)
        {
            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            _secretClient = new SecretClient(new Uri(keyVaultUrl), credential);
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            var secret = await _secretClient.GetSecretAsync(secretName).ConfigureAwait(false);
            return secret.Value.Value;
        }
    }
}
