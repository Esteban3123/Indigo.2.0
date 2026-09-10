using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Application.Autentication.JwtService;
using Domain.Autentication.Interfaces;
using DistributedService.Rest.Unity;
using Infrastructure.CrossCutting.Base;

namespace DistributedService.Rest.App_Start
{
    /// <summary>
    /// Filtro de autorización JWT para producción.
    /// Valida Bearer token en todos los endpoints excepto los marcados con [AllowAnonymous].
    /// </summary>
    public class JwtAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override async Task OnAuthorizationAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (SkipAuthorization(actionContext))
                return;

            var authHeader = actionContext.Request.Headers.Authorization;

            // Validar que existe el header
            if (authHeader == null)
            {
                actionContext.Response = CreateUnauthorizedResponse(
                    actionContext,
                    "Header Authorization no encontrado");
                return;
            }

            // Validar que el scheme sea "Bearer"
            if (authHeader.Scheme != "Bearer")
            {
                actionContext.Response = CreateUnauthorizedResponse(
                    actionContext,
                    "Scheme debe ser 'Bearer'");
                return;
            }

            // Extraer el token
            var token = authHeader.Parameter;

            if (string.IsNullOrWhiteSpace(token))
            {
                actionContext.Response = CreateUnauthorizedResponse(
                    actionContext,
                    "Token JWT vacío");
                return;
            }

            // Validar el token de forma asíncrona (sin bloquear)
            if (!await ValidateTokenAsync(actionContext, token, cancellationToken).ConfigureAwait(false))
            {
                actionContext.Response = CreateUnauthorizedResponse(
                    actionContext,
                    "Token JWT inválido o expirado");
                return;
            }

            // Si llega aquí, el token es válido y se permite continuar
        }

        /// <summary>
        /// Omite la validación JWT para acciones/controladores con [AllowAnonymous].
        /// </summary>
        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        /// <summary>
        /// Valida el token JWT usando IJwtTokenService de forma asíncrona.
        /// Obtiene container/hisContainer desde los headers del request (igual que BillingController y ElectronicRipsController).
        /// El flujo async completo evita deadlocks: JwtTokenService -> CachedSecretProvider -> KeyVaultSecretProvider.
        /// </summary>
        private async Task<bool> ValidateTokenAsync(
            HttpActionContext actionContext,
            string token,
            CancellationToken cancellationToken)
        {
            try
            {
                var headers = actionContext.Request.Headers;
                var container = string.Empty;
                var hisContainer = string.Empty;

                if (headers.Contains(ConfigurationFile.SESS_CONTAINER))
                    container = headers.GetValues(ConfigurationFile.SESS_CONTAINER).First();
                if (headers.Contains(ConfigurationFile.SESS_CONTAINER_HIS))
                    hisContainer = headers.GetValues(ConfigurationFile.SESS_CONTAINER_HIS).First();

                var containerUnity = ContainerRCM.Current(container, hisContainer);
                var jwtService = (IJwtTokenService)containerUnity.Resolve(typeof(IJwtTokenService), null);

                await jwtService.ValidateToken(token).ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Error validando JWT: {ex.GetType().Name}: {ex.Message}");

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"  InnerException: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }

                // Si es una excepción de Key Vault, mostrar el mensaje completo
                if (ex.Message.Contains("Key Vault") || (ex.InnerException?.Message?.Contains("Key Vault") ?? false))
                {
                    System.Diagnostics.Debug.WriteLine($"  Detalle completo: {ex}");
                }

                return false;
            }
        }

        /// <summary>
        /// Crea una respuesta HTTP 401 Unauthorized estándar
        /// </summary>
        private HttpResponseMessage CreateUnauthorizedResponse(
            HttpActionContext actionContext,
            string message)
        {
            return actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                new
                {
                    error = "Unauthorized",
                    message = message,
                    timestamp = DateTime.UtcNow
                }
            );
        }
    }
}