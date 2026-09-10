using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DistributedService.Notification.SignalR.AdminServices;
using Domain.Base.Entities;
using Newtonsoft.Json;

namespace DistributedService.Notification.SignalR.SignalR
{
    public class ChatHub : Hub
    {
        #region Base Methods

        /// <summary>
        /// Ocurre cuando se conecta un cliente
        /// </summary>
        public override System.Threading.Tasks.Task OnConnected()
        {
            Subscriber sub = JsonConvert.DeserializeObject<ChatSubscriber>(Context.Headers["Subscriber"]);
            if (sub != null)
            {
                ChatAdminService.Instance.Subscribe(sub, Context.ConnectionId);
            }
            return base.OnConnected();
        }

        /// <summary>
        /// Ocurre cuando se desconecta un cliente
        /// </summary>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            ChatAdminService.Instance.Unsubscribe(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// Ocurre cuando se reconecta un cliente
        /// </summary>
        public override System.Threading.Tasks.Task OnReconnected()
        {
            return base.OnReconnected();
        }

        /// <summary>
        /// Cambia el estado del usuario
        /// </summary>
        /// <param name="idAppUser">Id del usuario quien cambia de estado</param>
        /// <param name="newStatus">Nuevo estado del usuario</param>
        public void ChangeStatus(string idAppUser, AppUserStatus newStatus)
        {
            ChatAdminService.Instance.ChangeStatus(idAppUser, newStatus);
        }

        /// <summary>
        /// Obtiene el estado de un usuario
        /// </summary>
        /// <param name="idAppUser">Id del usuario a consultar</param>
        /// <returns>Estado del usuario</returns>
        public AppUserStatus GetUserStatus(string idAppUser)
        {
            return ChatAdminService.Instance.GetUserStatus(idAppUser);
        }

        /// <summary>
        /// Obtiene un usuario del diccionario de usuarios en linea
        /// </summary>
        /// <param name="idAppUser">Id del usuario a consultar</param>
        /// <returns>Usuario consultado</returns>
        public AppUser GetUser(string idAppUser)
        {
            return ChatAdminService.Instance.GetUser(idAppUser);
        }

        #endregion

        #region Simple Message Methods

        /// <summary>
        /// Notifica a un usuario que los mensajes enviados a otro, ya fueron revisados
        /// </summary>
        /// <param name="idAppUserFrom">Usuario origen</param>
        /// <param name="idAppUserTo">Usuario destino</param>
        public void CheckMessages(string idAppUserFrom, string idAppUserTo)
        {
            ChatAdminService.Instance.CheckMessages(idAppUserFrom, idAppUserTo);
        }

        /// <summary>
        /// Envia un mensaje de un usuario a otro
        /// </summary>
        /// <param name="idAppUserFrom">Usuario origen</param>
        /// <param name="idAppUserTo">Usuario destino</param>
        /// <param name="message">Mensaje</param>
        public void SendMessage(string idAppUserFrom, string idAppUserTo, ChatMessage message)
        {
            ChatAdminService.Instance.SendMessage(idAppUserFrom, idAppUserTo, message);
        }

        /// <summary>
        /// Envia un valor que indica si el usuario se encuentra escribiendo
        /// </summary>
        /// <param name="idAppUserFrom">Usuario origen</param>
        /// <param name="idAppUserTo">Usuario destino</param>
        /// <param name="isWriting">Valor que indica si se esta escribiendo</param>
        public void Writing(string idAppUserFrom, string idAppUserTo, bool isWriting)
        {
            ChatAdminService.Instance.Writing(idAppUserFrom, idAppUserTo, isWriting);
        }

        #endregion
    }
}