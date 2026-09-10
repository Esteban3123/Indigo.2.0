using Domain.Autentication;
using Domain.Autentication.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Autentication.JwtService
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly ISecretProvider _secretProvider;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(ISecretProvider secretProvider, JwtSettings jwtSettings)
        {
            this._secretProvider = secretProvider;
            this._jwtSettings = jwtSettings;
        }

        public async Task<string> GenerateToken(string username, string container)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("container", container));
            claims.Add(new Claim("his_container", container));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(await _secretProvider.GetSecretAsync(_jwtSettings.SecretName).ConfigureAwait(false));
            string issuer = await _secretProvider.GetSecretAsync(_jwtSettings.IssuerSecretName).ConfigureAwait(false);
            string audience = await _secretProvider.GetSecretAsync(_jwtSettings.AudienceSecretName).ConfigureAwait(false);
            string timeLife = await _secretProvider.GetSecretAsync(_jwtSettings.ExpiresInMinutes).ConfigureAwait(false);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(timeLife)),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        }

        public async Task ValidateToken(string token)
        {
            string secretKey = await _secretProvider.GetSecretAsync(_jwtSettings.SecretName).ConfigureAwait(false);
            string issuer = await _secretProvider.GetSecretAsync(_jwtSettings.IssuerSecretName).ConfigureAwait(false);
            string audience = await _secretProvider.GetSecretAsync(_jwtSettings.AudienceSecretName).ConfigureAwait(false);
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
            };

            try
            {
                SecurityToken validatedToken;
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Token JWT inválido.", ex);
            }
        }


        #region IDisposable Support
        private bool disposedValue;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion 

    }
}
