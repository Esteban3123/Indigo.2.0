using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Base.Security
{
    /// <summary>
    /// DelegatingHandler que agrega el header Authorization Bearer a las peticiones HTTP REST.
    /// Valida la expiración del token antes de enviar y dispara logout si está expirado.
    /// </summary>
    /// <remarks>
    /// Uso: New HttpClient(new BearerTokenHandler { InnerHandler = new HttpClientHandler() })
    /// </remarks>
    public class BearerTokenHandler : DelegatingHandler
    {
        /// <summary>
        /// Indica si se debe validar la expiración del token antes de enviar la petición.
        /// Por defecto es true.
        /// </summary>
        public bool ValidateTokenExpiration { get; set; } = true;

        public BearerTokenHandler()
            : base(new HttpClientHandler())
        {
        }

        public BearerTokenHandler(HttpMessageHandler innerHandler)
            : base(innerHandler ?? new HttpClientHandler())
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
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
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
