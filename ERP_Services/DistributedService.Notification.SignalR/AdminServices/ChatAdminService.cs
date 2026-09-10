#region Using

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using DistributedService.Notification.SignalR.Helpers;
using Domain.Base.Entities;
using Infrastructure.CrossCutting.IOC;
using Infrastructure.CrossCutting.Base;
using Application.Security;
using Infrastructure.CrossCutting.Exceptions;
using DistributedService.Notification.SignalR.SignalR;
using Microsoft.AspNet.SignalR;

#endregion

namespace DistributedService.Notification.SignalR.AdminServices
{
    /// <summary>
    /// Clase encargada de administrar las conexiones y llamados
    /// al servicio de chat Indigo Messenger
    /// </summary>
    public sealed class ChatAdminService
    {
        #region Singleton

        /// <summary>
        /// Única instancia de la clase
        /// </summary>
        private static ChatAdminService instance;

        /// <summary>
        /// Obtiene la única instancia de la clase
        /// </summary>
        public static ChatAdminService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChatAdminService();
                return instance;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Lista de subscriptores del chat
        /// </summary>
        private ConcurrentDictionary<string,Subscriber> _clients;

        /// <summary>
        /// Servicio de aplicacion de grupos
        /// </summary>
        private IGroupUserAdminService _groupService;

        /// <summary>
        /// Servicio de aplicación de grupos de usuarios
        /// </summary>
        private IUsersGroupUserAdminService _userService;

        #endregion

        #region Builders

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// </summary>
        private ChatAdminService()
        {
            var container = System.Configuration.ConfigurationManager.AppSettings[ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME];
            if (container == null || container.ToString().Trim().Equals(String.Empty))
                throw new ArgumentException("containerSecurity", "El parametro no existe en el archivo de configuración o se encuentra vacío");
            this._clients = new ConcurrentDictionary<string, Subscriber>();
            this._groupService = IocFactory.get_Instance(container).CurrentContainer.Resolve<IGroupUserAdminService>();
            this._userService = IocFactory.get_Instance(container).CurrentContainer.Resolve<IUsersGroupUserAdminService>();
        }

        #endregion

        #region Base Methods

        /// <summary>
        /// Obtiene el contexto actual para la conexion
        /// </summary>
        /// <returns>Contexto de la actual conexion</returns>
        private Microsoft.AspNet.SignalR.IHubContext GetHubContext()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            if (context == null)
                IndigoManagementExceptions.HandleException(new InvalidOperationException("No existe un contexto de tipo <ChatHub> por el cual comunicarse con los clientes"), "ApplicationPolicy");
            return context;
        }

        /// <summary>
        /// Subscribe un cliente al servicio de chat
        /// </summary>
        /// <param name="subscriber">Cliente a subscribir</param>
        /// <param name="idCallBack">Id de conexion del cliente a subscribir</param>
        public void Subscribe(Subscriber subscriber, string idCallBack)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    if (!this._clients.ContainsKey(subscriber.UID))
                    {
                        subscriber.CallBack = idCallBack;
                        this._clients.TryAdd(subscriber.UID, subscriber);
                        var usrs = this._clients.ToListOfUsers(subscriber.App.AppUser, this._userService.ListByUserCode(subscriber.App.AppUser.ID));
                        context.Clients.Client(this._clients[subscriber.UID].CallBack).OnListUsersOnline(usrs);
                    }
                    else
                    {
                        this._clients[subscriber.UID].CallBack = idCallBack;
                    }
                    this.ChangeStatus(subscriber.App.AppUser.ID, AppUserStatus.Online);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Desuscribe un cliente del servicio de notificación correspondiente
        /// </summary>
        /// <param name="connectionId">Id de la conexion del cliente a desubscribir</param>
        public void Unsubscribe(string connectionId)
        {
            try
            {
                if (this._clients.Any((kv) => kv.Value.CallBack.Equals(connectionId)))
                {
                    var usr = this._clients.Where((kv) => kv.Value.CallBack.Equals(connectionId)).ToList()[0];
                    if (this._clients.LongCount((kv) => kv.Value.App.AppUser.Equals(usr.Value.App.AppUser)) == 1)
                    {
                        Subscriber res;
                        this._clients.TryRemove(usr.Key, out res);
                        this.ChangeStatus(usr.Value.App.AppUser.ID.Trim(), AppUserStatus.Offline);
                    }
                }
            }catch(Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Cambia el estado del usuario
        /// </summary>
        /// <param name="idAppUser">Id del usuario quien cambia de estado</param>
        /// <param name="newStatus">Nuevo estado del usuario</param>
        public void ChangeStatus(string idAppUser, AppUserStatus newStatus)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Where((k) => k.Value.App.AppUser.ID.Equals(idAppUser)).ToList())
                    {
                        kv.Value.App.AppUser.Status = newStatus;
                    }
                    context.Clients.All.OnChangedStatus(idAppUser, newStatus);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Obtiene el estado de un usuario
        /// </summary>
        /// <param name="idAppUser">Id del usuario a consultar</param>
        /// <returns>Estado del usuario</returns>
        public AppUserStatus GetUserStatus(string idAppUser)
        {
            try
            {
                var results = this._clients.Where((k) => k.Value.App.AppUser.ID.Equals(idAppUser)).ToList();
                if (results != null && results.Count > 0)
                    return results[0].Value.App.AppUser.Status;
                else
                    return AppUserStatus.Offline;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return AppUserStatus.Offline;
            }
        }

        /// <summary>
        /// Obtiene un usuario del diccionario de usuarios en linea
        /// </summary>
        /// <param name="idAppUser">Id del usuario a consultar</param>
        /// <returns>Usuario consultado</returns>
        public AppUser GetUser(string idAppUser)
        {
            try
            {
                var results = this._clients.Where((k) => k.Value.App.AppUser.ID.Equals(idAppUser)).ToList();
                if (results != null && results.Count > 0)
                    return results[0].Value.App.AppUser;
                return new AppUser() { ID = "", Name = "", Position = "", Status = AppUserStatus.Offline };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new AppUser() { ID = "", Name = "", Position = "", Status = AppUserStatus.Offline };
            }
        }

        #endregion

        #region Single Message Methods

        /// <summary>
        /// Notifica a un usuario que los mensajes enviados a otro, ya fueron revisados
        /// </summary>
        /// <param name="idAppUserFrom">Usuario origen</param>
        /// <param name="idAppUserTo">Usuario destino</param>
        public void CheckMessages(string idAppUserFrom, string idAppUserTo)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Where((k) => k.Value.App.AppUser.ID.Trim().Equals(idAppUserTo.Trim())).ToList())
                    {
                        context.Clients.Client(kv.Value.CallBack).OnCheckedMessages(idAppUserFrom);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Envia un mensaje de un usuario a otro
        /// </summary>
        /// <param name="idAppUserFrom">Usuario origen</param>
        /// <param name="idAppUserTo">Usuario destino</param>
        /// <param name="message">Mensaje</param>
        public void SendMessage(string idAppUserFrom, string idAppUserTo, ChatMessage message)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Where((k) => k.Value.App.AppUser.ID.Trim().Equals(idAppUserTo.Trim())).ToList())
                    {
                        context.Clients.Client(kv.Value.CallBack).OnMessageIncoming(idAppUserFrom, message);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Envia un valor que indica si el usuario se encuentra escribiendo
        /// </summary>
        /// <param name="idAppUserFrom">Usuario origen</param>
        /// <param name="idAppUserTo">Usuario destino</param>
        /// <param name="isWriting">Valor que indica si se esta escribiendo</param>
        public void Writing(string idAppUserFrom, string idAppUserTo, bool isWriting)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Where((k) => k.Value.App.AppUser.ID.Trim().Equals(idAppUserTo.Trim())).ToList())
                    {
                        context.Clients.Client(kv.Value.CallBack).OnWriting(idAppUserFrom, isWriting);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        #endregion
    }
}