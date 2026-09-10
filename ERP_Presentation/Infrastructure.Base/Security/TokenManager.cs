using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Base.Security
{
    public class TokenManager
    {
        private static readonly Lazy<TokenManager> _instance = new Lazy<TokenManager>(() => new TokenManager());

        private string _accessToken;
        private string _encryptedKey;
        private bool _flagvalidateToken = true;

        // Objeto para realizar locking
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private TokenManager() { }

        public static TokenManager Instance => _instance.Value;

        public void SetToken(string token, string encryptedKey = null)
        {
            _lock.EnterWriteLock();
            try
            {
                _accessToken = token;
                _encryptedKey = encryptedKey;
                _flagvalidateToken = true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public string GetToken()
        {
            _lock.EnterReadLock();
            try
            {
                
                return _accessToken;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void SetFlagValidateToken(bool value)
        {
            _lock.EnterWriteLock();
            try
            {
                _flagvalidateToken = value;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool IsTokenExpired(int leewaySeconds = 60)
        {
            _lock.EnterReadLock();
            try
            {

                if (string.IsNullOrEmpty(_accessToken) && !_flagvalidateToken)
                    return false;

                if (string.IsNullOrEmpty(_accessToken))
                    return true;

                if (!string.IsNullOrEmpty(_encryptedKey))
                { 
                    _accessToken = GetInnerJwt(_accessToken, _encryptedKey);
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(_accessToken);

                var exp = jwtToken.Payload.Expiration;
                if (exp == null)
                    return true;

                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp.Value).UtcDateTime;
                return DateTime.UtcNow >= expirationTime.AddSeconds(-leewaySeconds);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void ClearToken()
        {
            _lock.EnterWriteLock();
            try
            {
                _accessToken = null;
                _flagvalidateToken = true;
                _encryptedKey = null;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public static string GetInnerJwt(string encryptedToken, string encryptionKey)
        {
            string signedToken = ""; 
            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                TokenDecryptionKey = new SymmetricSecurityKey(
                                              Encoding.UTF8.GetBytes(encryptionKey)
                                           ),
                SignatureValidator = (token, parameters) =>
                {
                    signedToken = token;
                    return new JwtSecurityToken(token);
                }

            };

            handler.ValidateToken(
                encryptedToken,
                validationParameters,
                out SecurityToken validatedToken
            );

            if (validatedToken is JwtSecurityToken jwt)
            {
            return signedToken;
            }

            throw new InvalidOperationException(
                "No se obtuvo un JwtSecurityToken tras desencriptar."
            );
        }

    }
}
