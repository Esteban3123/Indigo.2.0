using System;
using RestSharp;
using RestSharp.Authenticators;

namespace Infrastructure.Base.Security
{
    /// <summary>
    /// Authenticator de RestSharp que agrega el header Authorization Bearer a cada petición.
    /// Obtiene el token de TokenManager en cada request, valida expiración y dispara logout si está expirado.
    /// </summary>
    /// <remarks>
    /// Uso con RestClient: client.Authenticator = new BearerTokenAuthenticator();
    /// O por request: request.Authenticator = new BearerTokenAuthenticator();
    /// </remarks>
    public class BearerTokenAuthenticator : IAuthenticator
    {
        private const string AuthorizationHeader = "Authorization";

        /// <summary>
        /// Indica si se debe validar la expiración del token antes de enviar la petición.
        /// Por defecto es true.
        /// </summary>
        public bool ValidateTokenExpiration { get; set; } = true;

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (ValidateTokenExpiration && TokenManager.Instance.IsTokenExpired())
            {
                LogoutManager.TriggerLogoutIfNeeded();
                throw new InvalidOperationException(
                    "Token expirado. Se ha cerrado la sesión automáticamente.");
            }

            var token = TokenManager.Instance.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.AddOrUpdateHeader(AuthorizationHeader, "Bearer " + token);
            }
        }
    }
}
