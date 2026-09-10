using Application.AccountManagement;
using DistributedService.Rest.Unity;
using DistributedServices.AccountManagement;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;

namespace DistributedService.Rest.Controllers
{

    /// <summary>
    /// Controlador para la gestión de parámetros de gestión de cuentas.
    /// Proporciona endpoints para obtener configuraciones y usuarios disponibles.
    /// </summary>
    [RoutePrefix("accountmanagement")]
    public class AccountManagementParametersController : ApiController
    {
        /// <summary>
        /// Obtiene los parámetros de gestión de cuentas.
        /// </summary>
        /// <param name="httpRequestMessage">Mensaje HTTP que contiene la consulta, incluyendo el parámetro 'db'.</param>
        /// <returns>
        /// Un objeto <see cref="RequestResponse{Object}"/> con los parámetros de gestión de cuentas si existen,
        /// o un mensaje de error si no se encuentran.
        /// </returns>
        [Route("settings")]
        [HttpGet]
        public RequestResponse<Object> GetAccountManagementSettings(HttpRequestMessage httpRequestMessage)
        {
            var result = new RequestResponse<object>();
            var query = httpRequestMessage.RequestUri.ParseQueryString();
            string db = query["db"];
            SessionValues.Instance.TransactionalContainer = db;
            SessionValues.Instance.HisContainer = db;

            var service = ContainerRCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IAccountManagementParametersAdminService>();
            AccountManagementParameters managementParameters = service.GetAccountManagementParametersById(1).ObjectEmbbeded;
            managementParameters.usersAssignment = null;
            managementParameters.userNovelties = null;
            if (managementParameters.Id > 0)
            {
                result.Data = managementParameters;
                result.Status = true;
                result.Code = "1";
                result.Message = $"Se obtuvieron los parametros de gestión de cuentas exitosamente";
            }
            else
            {
                result.Status = false;
                result.Code = "0";
                result.Message = $"No se encontraron parametros de gestión de cuentas";
            }

            return result;
        }

        /// <summary>
        /// Obtiene la lista de usuarios disponibles para asignación.
        /// </summary>
        /// <param name="httpRequestMessage">Mensaje HTTP que contiene la consulta, incluyendo el parámetro 'db'.</param>
        /// <returns>
        /// Un objeto <see cref="RequestResponse{Object}"/> con la lista de usuarios disponibles,
        /// o un mensaje de error si no se encuentran usuarios.
        /// </returns>
        [Route("availableUsers")]
        [HttpGet]
        public RequestResponse<Object> GetAvailableUsers(HttpRequestMessage httpRequestMessage)
        {
            var result = new RequestResponse<object>();
            var query = httpRequestMessage.RequestUri.ParseQueryString();
            string db = query["db"];
            SessionValues.Instance.TransactionalContainer = db;
            SessionValues.Instance.HisContainer = db;

            var service = ContainerRCM.Current(SessionValues.Instance.TransactionalContainer, SessionValues.Instance.HisContainer).Resolve<IAccountManagementParametersAdminService>();
            var availableUsers = service.GetAvailableUsers().ObjectEmbbeded;
            if (availableUsers.Count > 0)
            {
                result.Data = availableUsers;
                result.Status = true;
                result.Code = "1";
                result.Message = $"Se obtuvieron los usuarios disponibles para asignación exitosamente";
            }
            else
            {
                result.Status = false;
                result.Code = "0";
                result.Message = $"No se encontraron usuarios disponibles para asignación";
            }

            return result;
        }
    }



}