#region Using

using Application.Security;
using DistributedService.Notification.SignalR.SignalR;
using Domain.Security.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.IOC;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace DistributedService.Notification.SignalR.AdminServices
{
    /// <summary>
    /// Clase encargada de administrar las conexiones y llamados
    /// al servicio de administración
    /// </summary>
    public sealed class ManagementAdminService
    {
        #region Singleton

        /// <summary>
        /// Única instancia de la clase
        /// </summary>
        private static ManagementAdminService instance;

        /// <summary>
        /// Obtiene la única instancia de la clase
        /// </summary>
        public static ManagementAdminService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ManagementAdminService();
                instance.RefreshRepositories();
                return instance;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Lista de maquinas subscriptas
        /// </summary>
        private ConcurrentDictionary<string, Models.Subscriber> _clients;

        /// <summary>
        /// Lista de maquinas subscriptas que se encuentran en proceso de actualización
        /// </summary>
        private ConcurrentDictionary<string, Models.Subscriber> _clientsUpgrading;

        /// <summary>
        /// Diccionario de archivos en proceso de carga
        /// </summary>
        private ConcurrentDictionary<string, Tuple<FileInfo, TimeSpan, StringBuilder>> _files;

        /// <summary>
        /// Diccionario de archivos en proceso de carga
        /// </summary>
        private ConcurrentDictionary<string, Tuple<FileInfo, TimeSpan, MemoryStream>> _files2;

        ///// <summary>
        ///// Repositorio de máquinas
        ///// </summary>
        private IMachineAdminService _machineService;

        #endregion

        #region Builders

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// </summary>
        private ManagementAdminService()
        {
            this._clients = new ConcurrentDictionary<string, Models.Subscriber>();
            this._clientsUpgrading = new ConcurrentDictionary<string, Models.Subscriber>();
            this._files = new ConcurrentDictionary<string, Tuple<FileInfo, TimeSpan, StringBuilder>>();
            this._files2 = new ConcurrentDictionary<string, Tuple<FileInfo, TimeSpan, MemoryStream>>();
        }

        /// <summary>
        /// Destruye y construye de nuevo los servicios de
        /// aplicación
        /// </summary>
        public void RefreshRepositories()
        {
            var container = System.Configuration.ConfigurationManager.AppSettings[ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME];
            if (container == null || container.ToString().Trim().Equals(String.Empty))
                throw new ArgumentException("containerSecurity", "El parametro no existe en el archivo de configuración o se encuentra vacío");
            this._machineService = null;
            this._machineService = IocFactory.get_Instance(container).CurrentContainer.Resolve<IMachineAdminService>();
        }

        #endregion

        #region Base Methods

        /// <summary>
        /// Obtiene el contexto actual para la conexion
        /// </summary>
        /// <returns>Contexto de la actual conexion</returns>
        private Microsoft.AspNet.SignalR.IHubContext GetHubContext()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ManagementHub>();
            if (context == null)
                IndigoManagementExceptions.HandleException(new InvalidOperationException("No existe un contexto de tipo <ManagementHub> por el cual comunicarse con los clientes"), "ApplicationPolicy");
            return context;
        }

        /// <summary>
        /// Subscribe un cliente al servicio de chat
        /// </summary>
        /// <param name="subscriber">Cliente a subscribir</param>
        /// <param name="idCallBack">Id de conexion del cliente a subscribir</param>
        public void Subscribe(Models.Subscriber subscriber, string idCallBack)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    subscriber.LastSubscriptionDate = DateTime.Now;
                    if (!this._clients.ContainsKey(subscriber.UId)) //Si no existe el subscriptor lo agrego
                    {
                        if (subscriber.Type == -1) { subscriber.Callback = idCallBack; subscriber.Service = null; subscriber.Clients = new List<Models.Client>(); subscriber.Messengers = new List<Models.Messenger>(); }
                        if (subscriber.Type == 0) { subscriber.Service.Callback = idCallBack; subscriber.Service.LastSubscription = DateTime.Now; subscriber.Callback = string.Empty; subscriber.Clients = new List<Models.Client>(); subscriber.Messengers = new List<Models.Messenger>(); }
                        if (subscriber.Type == 1) { subscriber.Clients[0].Callback = idCallBack; subscriber.Clients[0].LastSubscription = DateTime.Now; subscriber.Callback = string.Empty; subscriber.Service = null; subscriber.Messengers = new List<Models.Messenger>(); }
                        if (subscriber.Type == 2) { subscriber.Messengers[0].Callback = idCallBack; subscriber.Messengers[0].LastSubscription = DateTime.Now; subscriber.Callback = string.Empty; subscriber.Service = null; subscriber.Clients = new List<Models.Client>(); }
                        this._clients.TryAdd(subscriber.UId, subscriber);
                    }
                    else
                    {
                        if (subscriber.Type == -1) //Si es una consola
                        {
                            this._clients[subscriber.UId].Callback = idCallBack;
                            this._clients[subscriber.UId].Architecture = subscriber.Architecture;
                            this._clients[subscriber.UId].IPs = subscriber.IPs;
                            this._clients[subscriber.UId].MACs = subscriber.MACs;
                            this._clients[subscriber.UId].Name = subscriber.Name;
                            this._clients[subscriber.UId].OSName = subscriber.OSName;
                            this._clients[subscriber.UId].ClientVersion = subscriber.ClientVersion;
                            this._clients[subscriber.UId].LastSubscriptionDate = subscriber.LastSubscriptionDate;
                        }
                        if (subscriber.Type == 0) //Si es un servicio
                        {
                            subscriber.Service.Callback = idCallBack;
                            subscriber.Service.LastSubscription = DateTime.Now;
                            this._clients[subscriber.UId].ClientVersion = subscriber.ClientVersion;
                            this._clients[subscriber.UId].Service = null;
                            this._clients[subscriber.UId].Service = subscriber.Service;
                        }
                        if (subscriber.Type == 1) //Si es un cliente
                        {
                            subscriber.Clients[0].Callback = idCallBack;
                            subscriber.Clients[0].LastSubscription = DateTime.Now;
                            this._clients[subscriber.UId].Clients.Add(subscriber.Clients[0]);
                        }
                        if (subscriber.Type == 2) //Si es un mensajero
                        {
                            subscriber.Messengers[0].Callback = idCallBack;
                            subscriber.Messengers[0].LastSubscription = DateTime.Now;
                            this._clients[subscriber.UId].Messengers.Add(subscriber.Messengers[0]);
                        }
                    }
                    //Aqui se debe notificar que se conecto un nuevo subscriptor
                    this.SubscriptorConnected(subscriber);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Refresca los datos de un subscriptor
        /// </summary>
        /// <param name="subscriber"></param>
        public void RefreshSubscriber(Models.Subscriber subscriber)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    if (this._clients.ContainsKey(subscriber.UId))
                    {
                        if (subscriber.Type == -1) //Si es una consola
                        {
                            this._clients[subscriber.UId].Architecture = subscriber.Architecture;
                            this._clients[subscriber.UId].IPs = subscriber.IPs;
                            this._clients[subscriber.UId].MACs = subscriber.MACs;
                            this._clients[subscriber.UId].Name = subscriber.Name;
                            this._clients[subscriber.UId].OSName = subscriber.OSName;
                            this._clients[subscriber.UId].ClientVersion = subscriber.ClientVersion;
                            this._clients[subscriber.UId].LastSubscriptionDate = subscriber.LastSubscriptionDate;
                        }
                        if (subscriber.Type == 0) //Si es un servicio
                        {
                            subscriber.Service.LastSubscription = DateTime.Now;
                            this._clients[subscriber.UId].ClientVersion = subscriber.ClientVersion;
                            this._clients[subscriber.UId].Service = null;
                            this._clients[subscriber.UId].Service = subscriber.Service;
                        }
                        if (subscriber.Type == 1) //Si es un cliente
                        {
                            subscriber.Clients[0].LastSubscription = DateTime.Now;
                            this._clients[subscriber.UId].Clients.Add(subscriber.Clients[0]);
                        }
                        if (subscriber.Type == 2) //Si es un mensajero
                        {
                            subscriber.Messengers[0].LastSubscription = DateTime.Now;
                            this._clients[subscriber.UId].Messengers.Add(subscriber.Messengers[0]);
                        }
                        //Aqui se debe notificar que se conecto un nuevo subscriptor
                        this.SubscriptorConnected(subscriber);
                    }
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
                string uid = string.Empty;

                //Si es consola
                var sus = this._clients.Values.Where((s) => s.Callback != null).ToList();
                if (sus != null)
                {
                    if (sus.Count > 0 && sus.Any((s) => s.Callback.Equals(connectionId)))
                    {
                        uid = sus.Where((s) => s.Callback.Equals(connectionId)).SingleOrDefault().UId;
                        this._clients[uid].Callback = string.Empty;
                    }
                }

                //Si es servicio
                sus = this._clients.Values.Where((s) => s.Service != null).ToList().Where((s) => s.Service.Callback != null).ToList();
                if (sus != null)
                {
                    if (sus.Count > 0 && sus.Any((s) => s.Service.Callback.Equals(connectionId)))
                    {
                        uid = sus.Where((s) => s.Service.Callback.Equals(connectionId)).SingleOrDefault().UId;
                        //Aqui se notifica la desconección del servicio
                        this.SubscriptorDesconnected(uid, this._clients[uid].Service);
                        this._clients[uid].Service = null;
                    }
                }

                //Si es un cliente
                if (this._clients.Values.Any((s) => s.Clients != null))
                {
                    if (this._clients.Values.Any((s) => s.Clients.Count > 0))
                    {
                        sus = this._clients.Values.Where((s) => s.Clients != null).ToList();
                        if (sus.Any((s) => s.Clients.Any((c) => c.Callback.Equals(connectionId))))
                        {
                            uid = sus.Where((s) => s.Clients.Any((c) => c.Callback.Equals(connectionId))).SingleOrDefault().UId;
                            Models.Client cli = this._clients[uid].Clients.Where((c) => c.Callback.Equals(connectionId)).SingleOrDefault();
                            //Aqui se notifica la desconección del cliente
                            this.SubscriptorDesconnected(uid, cli);
                            this._clients[uid].Clients.Remove(cli);
                        }
                    }
                }

                //Si es un mensajero
                if (this._clients.Values.Any((s) => s.Messengers != null))
                {
                    if (this._clients.Values.Any((s) => s.Messengers.Count > 0))
                    {
                        sus = this._clients.Values.Where((s) => s.Messengers != null).ToList();
                        if (sus.Any((s) => s.Messengers.Any((c) => c.Callback.Equals(connectionId))))
                        {
                            uid = sus.Where((s) => s.Messengers.Any((c) => c.Callback.Equals(connectionId))).SingleOrDefault().UId;
                            Models.Messenger mess = this._clients[uid].Messengers.Where((c) => c.Callback.Equals(connectionId)).SingleOrDefault();
                            //Aqui se notifica la desconección del mensajero
                            this.SubscriptorDesconnected(uid, mess);
                            this._clients[uid].Messengers.Remove(mess);
                        }
                    }
                }

                //Eliminamos el subscriptor si ya no tiene a nadie conectado
                if (uid != null)
                {
                    if (!uid.Equals(string.Empty))
                    {
                        if (this._clients.ContainsKey(uid))
                        {
                            if (this._clients[uid].Callback != null)
                            {
                                if (this._clients[uid].Callback.Equals(string.Empty))
                                {
                                    if (this._clients[uid].Service == null)
                                    {
                                        if (this._clients[uid].Clients.Count == 0)
                                        {
                                            if (this._clients[uid].Messengers.Count == 0)
                                            {
                                                Models.Subscriber aux;
                                                this._clients.TryRemove(uid, out aux);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        #endregion

        #region Notification Console

        /// <summary>
        /// Realiza la notificación en el evento de desconeción de un subscriptor
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="serv">Servicio desconectado</param>
        private void SubscriptorDesconnected(string uid, Models.Service serv)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        context.Clients.Client(kv.Callback).OnServiceDesconnected(uid, serv);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Realiza la notificación en el evento de desconeción de un subscriptor
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="cli">Cliente desconectado</param>
        private void SubscriptorDesconnected(string uid, Models.Client cli)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        context.Clients.Client(kv.Callback).OnClientDesconnected(uid, cli);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Realiza la notificación en el evento de desconeción de un subscriptor
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="mess">Mensajero desconectado</param>
        private void SubscriptorDesconnected(string uid, Models.Messenger mess)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        context.Clients.Client(kv.Callback).OnMessengerDesconnected(uid, mess);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Realiza la notificación en el evento de coneción de un subscriptor
        /// </summary>
        /// <param name="subs">Subscriptor conectado</param>
        private void SubscriptorConnected(Models.Subscriber subs)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    //Se actualiza los datos de la máquina en la base de datos
                    int res = this.SaveMachine(subs);
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        if (res == 1) //Es una máquina nueva
                        {
                            context.Clients.Client(kv.Callback).OnSubscriberConnected(subs);
                        }
                        else
                        {
                            if (subs.Type == 0) //Servicio conectado
                                context.Clients.Client(kv.Callback).OnServiceConnected(subs.UId, subs.Service);
                            if (subs.Type == 1) //Cliente conectado
                                context.Clients.Client(kv.Callback).OnClientConnected(subs.UId, subs.Clients[0]);
                            if (subs.Type == 2) //Mensajero conectado
                                context.Clients.Client(kv.Callback).OnMessengerConnected(subs.UId, subs.Messengers[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Realiza la notificación de cambio de nombre de máquina
        /// </summary>
        /// <param name="uid">Identificación de la máquina a la que se le cambio el nombre</param>
        /// <param name="newNickName">Nuevo nombre de la máquina</param>
        private void NickNameMachineChanged(string uid, string newNickName)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        context.Clients.Client(kv.Callback).OnNickNameMachineChanged(uid, newNickName);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Notifica a las consolas que se ha conectado un invitado
        /// al escritorio de una máquina
        /// </summary>
        /// <param name="uid">Máquina que reporta la conexion</param>
        public void AttendeeConnected(string uid)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        context.Clients.Client(kv.Callback).OnAttendeeConnected(uid);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        #endregion

        #region Commnads

        /// <summary>
        /// Ordena la optimización de emsamblados a una lista de
        /// máquinas
        /// </summary>
        /// <param name="uidMachines">Lista de máquinas a optimizar</param>
        public void OptimizeAssemblies(List<string> uidMachines)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (string uid in uidMachines)
                    {
                        var cli = this._clients.Values.Where((c) => c.UId.Equals(uid)).SingleOrDefault();
                        if (cli != null)
                            if (cli.Service != null)
                                if (cli.Service.Callback != null)
                                    if (!cli.Service.Callback.Equals(string.Empty))
                                        context.Clients.Client(cli.Service.Callback).OnOptimizeAssemblies();
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Bloquea la sesión del cliente
        /// </summary>
        /// <param name="uidMachine">Identificación de la máquina</param>
        /// <param name="uidClient">Identificación del cliente</param>
        public void LockSession(string uidMachine, string uidClient)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    var cli = this._clients.Values.Where((c) => c.UId.Equals(uidMachine)).SingleOrDefault();
                    if (cli != null)
                        if (cli.Service != null)
                            if (cli.Service.Callback != null)
                                if (!cli.Service.Callback.Equals(string.Empty))
                                    context.Clients.Client(cli.Service.Callback).OnLockSession(uidClient);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Cierra la sesión
        /// </summary>
        /// <param name="uidMachine">Identificación de la máquina</param>
        /// <param name="uidClient">Identificación del cliente</param>
        public void CloseSession(string uidMachine, string uidClient)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    var cli = this._clients.Values.Where((c) => c.UId.Equals(uidMachine)).SingleOrDefault();
                    if (cli != null)
                        if (cli.Service != null)
                            if (cli.Service.Callback != null)
                                if (!cli.Service.Callback.Equals(string.Empty))
                                    context.Clients.Client(cli.Service.Callback).OnCloseSession(uidClient);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Envia un mensaje a un cliente
        /// </summary>
        /// <param name="uids">Listado de máquinas a apagar</param>
        /// <param name="author">Autor del mensaje</param>
        /// <param name="title">Titulo del mensaje</param>
        /// <param name="message">Mensaje a enviar</param>
        public void SendMessage(List<string> uids, string author, string title, string message)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (string uid in uids)
                    {
                        var cli = this._clients.Values.Where((c) => c.UId.Equals(uid)).SingleOrDefault();
                        if (cli != null)
                            if (cli.Service != null)
                                if (cli.Service.Callback != null)
                                    if (!cli.Service.Callback.Equals(string.Empty))
                                        context.Clients.Client(cli.Service.Callback).OnSendMessage(author, title, message);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Notifica a un listado de máquinas que deben apagarse
        /// </summary>
        /// <param name="uids">Listado de máquinas a apagar</param>
        /// <param name="t">Tiempo en el que se deben apagar</param>
        /// <param name="comment">Comentario de apagado</param>
        public void Shutdown(List<string> uids, int t, string comment = "")
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (string uid in uids)
                    {
                        var cli = this._clients.Values.Where((c) => c.UId.Equals(uid)).SingleOrDefault();
                        if (cli != null)
                            if (cli.Service != null)
                                if (cli.Service.Callback != null)
                                    if (!cli.Service.Callback.Equals(string.Empty))
                                        context.Clients.Client(cli.Service.Callback).OnShutdown(t, comment);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Notifica a un listado de máquinas que deben reiniciarse
        /// </summary>
        /// <param name="uids">Listado de máquinas a reiniciar</param>
        /// <param name="t">Tiempo en el que se deben reiniciar</param>
        /// <param name="comment">Comentario de reiniciado</param>
        public void Restart(List<string> uids, int t, string comment = "")
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (string uid in uids)
                    {
                        var cli = this._clients.Values.Where((c) => c.UId.Equals(uid)).SingleOrDefault();
                        if (cli != null)
                            if (cli.Service != null)
                                if (cli.Service.Callback != null)
                                    if (!cli.Service.Callback.Equals(string.Empty))
                                        context.Clients.Client(cli.Service.Callback).OnRestart(t, comment);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Cambia el nombre de la máquina
        /// </summary>
        public bool ChangeNickNameMachine(string uidChanger, string uid, string newNickName)
        {
            var res = this._machineService.ChangeNickName(uid, newNickName);
            if (!res.StateResult)
            {
                IndigoManagementExceptions.HandleException(new Exception(res.Message), "ApplicationPolicy");
                return false;
            }
            else
            {
                //Notificamos el cambio de nombre de la máquina
                this.NickNameMachineChanged(uid, newNickName);
                return true;
            }
        }

        /// <summary>
        /// Notifica a una máquina que debe realizar una captura de pantalla
        /// </summary>
        /// <param name="uid">Identificación de la máquina a quien se notifica</param>
        public void Screenshot(string uid, string uidClient)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    if (this._clients.ContainsKey(uid))
                    {
                        context.Clients.Client(this._clients[uid].Service.Callback).OnScreenshot(uidClient);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Mueve una imagen instantánea del directorio de archivos
        /// compartidos al directorio de imágenes instantáneas
        /// </summary>
        /// <param name="fileName">Nombre del archivo a mover</param>
        /// <param name="uid">Identificación de la máquina</param>
        public void MoveFileToScreenshot(string fileName, string uid)
        {
            try
            {
                string fpathTemp = Path.Combine(Helpers.Helper.GetTempFilesFolder(), fileName);
                Console.WriteLine("fpathTemp: " + fpathTemp);
                if (File.Exists(fpathTemp))
                {
                    string img = Path.Combine(Helpers.Helper.GetScreenshotFilesFolder(), (uid + (Path.GetExtension(fileName))));
                    Console.WriteLine("img: " + img);
                    if (File.Exists(img))
                        File.Delete(img);
                    File.Move(fpathTemp, img);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Mueve un paquete de actualización del directorio de archivos
        /// compartidos al directorio de paquetes de actualización disponibles
        /// </summary>
        /// <param name="fileName">Nombre del archivo a mover</param>
        /// <param name="packageInfo">Información de la versión</param>
        public void MoveFileToUpgradePackages(string fileName, Models.PackageInfo packageInfo)
        {
            try
            {
                string fpathTemp = Path.Combine(Helpers.Helper.GetTempFilesFolder(), fileName);
                Console.WriteLine("fpathTemp: " + fpathTemp);
                if (File.Exists(fpathTemp))
                {
                    string package = Path.Combine(Helpers.Helper.GetUpgradeFilesFolder(), (packageInfo.Version + (Path.GetExtension(fileName))));
                    Console.WriteLine("package: " + package);
                    if (File.Exists(package))
                        File.Delete(package);
                    File.Move(fpathTemp, package);
                    this._machineService.CreateUpdatePackage(packageInfo.NumberVersion, packageInfo.Version, packageInfo.BuildDate, packageInfo.Description);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Registra un paquete que ya ha subido
        /// </summary>
        /// <param name="packageInfo">Información del paquete</param>
        public void RegisterPackage(Models.PackageInfo packageInfo)
        {
            try
            {
                if (Directory.GetFiles(Helpers.Helper.GetUpgradeFilesFolder(), "*.update", SearchOption.TopDirectoryOnly).Any((f) => f.EndsWith(packageInfo.Version + ".update")))
                {
                    this._machineService.CreateUpdatePackage(packageInfo.NumberVersion, packageInfo.Version, packageInfo.BuildDate, packageInfo.Description);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Carga un archivo por fracciones
        /// </summary>
        /// <param name="token">Token del archivo a cargar</param>
        /// <param name="piece">Fraccion del archivo</param>
        /// <param name="isEndPiece">Valor que indica si la fracción es la ultima</param>
        /// <returns>Un valor que indica si se carlo la fracción del archivo</returns>
        public bool UploadFile(string token, string piece, bool isEndPiece)
        {
            try
            {
                if (this._files.ContainsKey(token))
                {
                    this._files[token].Item3.Append(piece);
                    this._files[token].Item2.Add(new TimeSpan(DateTime.Now.Ticks));
                    if (isEndPiece)
                    {
                        if (Directory.GetFiles(Helpers.Helper.GetTempFilesFolder()).Any((f) => f.StartsWith(token)))
                        {
                            string fil = Directory.GetFiles(Helpers.Helper.GetTempFilesFolder()).Where((f) => f.StartsWith(token)).SingleOrDefault();
                            File.Delete(fil);
                        }
                        File.WriteAllBytes(Path.Combine(Helpers.Helper.GetTempFilesFolder(), (token + Path.GetExtension(this._files[token].Item1.FullName))), Convert.FromBase64String(this._files[token].Item3.ToString()));
                        Tuple<FileInfo, TimeSpan, StringBuilder> aux;
                        this._files.TryRemove(token, out aux);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return false;
            }
        }

        /// <summary>
        /// Cancela la carga de un archivo
        /// </summary>
        /// <param name="token">Token del archivo a cancelar</param>
        public void CancelUpload(string token)
        {
            try
            {
                if (this._files2.ContainsKey(token))
                {
                    Tuple<FileInfo, TimeSpan, MemoryStream> aux;
                    this._files2.TryRemove(token, out aux);
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Carga un archivo por fracciones
        /// </summary>
        /// <param name="token">Token del archivo a cargar</param>
        /// <param name="buffer">Fraccion del archivo</param>
        /// <param name="isEndPiece">Valor que indica si la fracción es la ultima</param>
        /// <returns>Un valor que indica si se carlo la fracción del archivo</returns>
        public bool UploadFile2(string token, byte[] buffer, bool isEndPiece)
        {
            try
            {
                if (this._files2.ContainsKey(token))
                {
                    this._files2[token].Item3.Write(buffer, 0, buffer.Length);
                    this._files2[token].Item2.Add(new TimeSpan(DateTime.Now.Ticks));
                    if (isEndPiece)
                    {
                        if (Directory.GetFiles(Helpers.Helper.GetTempFilesFolder()).Any((f) => f.StartsWith(token)))
                        {
                            string fil = Directory.GetFiles(Helpers.Helper.GetTempFilesFolder()).Where((f) => f.StartsWith(token)).SingleOrDefault();
                            File.Delete(fil);
                        }
                        File.WriteAllBytes(Path.Combine(Helpers.Helper.GetTempFilesFolder(), (token + Path.GetExtension(this._files2[token].Item1.FullName))), this._files2[token].Item3.ToArray());
                        Tuple<FileInfo, TimeSpan, MemoryStream> aux;
                        this._files2.TryRemove(token, out aux);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return false;
            }
        }

        /// <summary>
        /// Obtiene un token para la carga de un archivo
        /// </summary>
        /// <param name="finfo">Información del archivo</param>
        /// <returns>Token generado para el archivo</returns>
        public string GetToken(FileInfo finfo)
        {
            try
            {
                this.RecycleFiles();
                string token = Utils.MD5(finfo.FullName + DateTime.Now.Ticks);
                if (!this._files.ContainsKey(token))
                    this._files.TryAdd(token, new Tuple<FileInfo, TimeSpan, StringBuilder>(finfo, new TimeSpan(DateTime.Now.Ticks), new StringBuilder()));
                return token;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene un token para la carga de un archivo
        /// </summary>
        /// <param name="finfo">Información del archivo</param>
        /// <returns>Token generado para el archivo</returns>
        public string GetToken2(FileInfo finfo)
        {
            try
            {
                this.RecycleFiles();
                string token = Utils.MD5(finfo.FullName + DateTime.Now.Ticks);
                if (!this._files2.ContainsKey(token))
                    this._files2.TryAdd(token, new Tuple<FileInfo, TimeSpan, MemoryStream>(finfo, new TimeSpan(DateTime.Now.Ticks), new MemoryStream()));
                return token;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return string.Empty;
            }
        }

        /// <summary>
        /// Limpia el diccionario y el directorio compartido de archivos
        /// que tengan un tiempo de modificación que exceda a 1 minuto
        /// </summary>
        private void RecycleFiles()
        {
            Tuple<FileInfo, TimeSpan, StringBuilder> aux;
            Tuple<FileInfo, TimeSpan, MemoryStream> aux2;
            //Recorremos los tokens y verificamos
            //su marca de tiempo, eliminando los que
            //superen el minuto
            foreach (var kv in this._files)
            {
                TimeSpan t = new TimeSpan(DateTime.Now.Ticks);
                if (t.Subtract(kv.Value.Item2).TotalSeconds > 60)
                {
                    this._files.TryRemove(kv.Key, out aux);
                }
            }
            foreach (var kv in this._files2)
            {
                TimeSpan t = new TimeSpan(DateTime.Now.Ticks);
                if (t.Subtract(kv.Value.Item2).TotalSeconds > 60)
                {
                    this._files2.TryRemove(kv.Key, out aux2);
                }
            }

            //Recorremos todos los archivos de la carpeta compartida
            //y verificamos la fecha de ultima modificación, eliminando
            //los que superen los 2 minutos
            if (Directory.Exists(Helpers.Helper.GetTempFilesFolder()))
            {
                foreach (var d in Directory.GetFiles(Helpers.Helper.GetTempFilesFolder()))
                {
                    try
                    {
                        DateTime finfo = File.GetLastWriteTime(d);
                        TimeSpan t = new TimeSpan(DateTime.Now.Ticks);
                        if (t.Subtract(new TimeSpan(finfo.Ticks)).TotalSeconds > 120)
                        {
                            File.Delete(d);
                        }
                    }
                    catch (Exception ex)
                    {
                        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    }
                }
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Márca la relación entre un paquete y una máquina
        /// como realizado
        /// </summary>
        /// <param name="uidMachine">Identificación unica de la máquina</param>
        /// <param name="idVersion">Id del registro de la version</param>
        public void SetDateOnUpdate(string uidMachine, int idVersion)
        {
            try
            {
                this._machineService.SetDateOnUpdate(uidMachine, idVersion);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Obtiene la URL de la carpeta para descargar los paquetes
        /// </summary>
        /// <returns>URL de los paquetes de actualización</returns>
        public string GetUrlPackages()
        {
            return Helpers.Helper.GetUpgradeFilesUrl();
        }

        /// <summary>
        /// Obtiene el paquete de actualización por Id de registro
        /// </summary>
        /// <param name="idVersion">Id de la versión a descargar</param>
        /// <returns>Paquete de actualización</returns>
        public UpdatePackage GetUpdatePackageById(int idVersion)
        {
            try
            {
                return this._machineService.GetById(idVersion);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new UpdatePackage();
            }
        }

        /// <summary>
        /// Mueve un subscriptor a la lista de actualizaciones en curso
        /// </summary>
        /// <param name="uid">Identificación unica del subscriptor</param>
        /// <param name="isUpgrading">Valor que indica si el subscriptor inicia o finaliza actualización</param>
        /// <param name="toVersion">Versión a la que se va a actualizar el cliente</param>
        public void Upgrading(string uid, bool isUpgrading, string toVersion)
        {
            try
            {
                if (isUpgrading)
                {
                    if (this._clients.ContainsKey(uid))
                    {
                        if (this._clientsUpgrading.ContainsKey(uid))
                        {
                            this._clientsUpgrading[uid].UpgradeStatus = 0;
                            this._clientsUpgrading[uid].UpgradeDetails = "Iniciando..." + Environment.NewLine + Environment.NewLine;
                            this._clientsUpgrading[uid].UpgradeToVersion = toVersion;
                        }
                        else
                        {
                            Models.Subscriber sub = new Models.Subscriber();
                            this._clients.TryGetValue(uid, out sub);
                            sub.UpgradeStatus = 0;
                            sub.UpgradeDetails = "Iniciando..." + Environment.NewLine + Environment.NewLine;
                            sub.UpgradeToVersion = toVersion;
                            this._clientsUpgrading.TryAdd(uid, sub);
                        }
                        //Notificamos el proceso de actualizacion a las consolas
                        IHubContext context = this.GetHubContext();
                        if (context != null)
                        {
                            Models.Subscriber sub = new Models.Subscriber();
                            this._clientsUpgrading.TryGetValue(uid, out sub);
                            foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                            {
                                context.Clients.Client(kv.Callback).OnNewUpgrading(sub);
                            }
                        }
                    }
                }
                else
                {
                    if (this._clientsUpgrading.ContainsKey(uid))
                    {
                        Models.Subscriber sub = new Models.Subscriber();
                        this._clientsUpgrading.TryRemove(uid, out sub);
                        sub.UpgradeStatus = sub.UpgradeStatus != 10 ? 9 : 10;
                        sub.UpgradeDetails = (sub.UpgradeStatus != 10 ? "Finalizó" : "Finalizó con Errores") + Environment.NewLine + Environment.NewLine;
                        sub.UpgradeToVersion = toVersion;
                        //Notificamos que el proceso de actualizacion a terminado
                        IHubContext context = this.GetHubContext();
                        if (context != null)
                        {
                            foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                            {
                                context.Clients.Client(kv.Callback).OnEndUpgrading(sub);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Registra el estado de la actualización en una máquina
        /// y notifica a todas las consolas
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="status"></param>
        /// <param name="details">Detalles del estado</param>
        public void SetUpgradeStatus(string uid, int status, string details)
        {
            try
            {
                if (this._clientsUpgrading.ContainsKey(uid))
                {
                    this._clientsUpgrading[uid].UpgradeStatus = status;
                    this._clientsUpgrading[uid].UpgradeDetails = details;
                    //Notificamos cambio de estado en el proceso de actualización
                    IHubContext context = this.GetHubContext();
                    if (context != null)
                    {
                        Models.Subscriber sub = new Models.Subscriber();
                        this._clientsUpgrading.TryGetValue(uid, out sub);
                        foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                        {
                            context.Clients.Client(kv.Callback).OnSetUpgradeStatus(sub);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Obtiene una lista de los subscriptores que se encuentran en proceso
        /// de actualización
        /// </summary>
        /// <returns>Lista de subscriptores actualizando</returns>
        public List<Models.Subscriber> GetCurrentUpdates()
        {
            try
            {
                return (this._clientsUpgrading.Values as List<Models.Subscriber>);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Models.Subscriber>();
            }
        }

        /// <summary>
        /// Obtiene un valor que indica si se puede registrar
        /// la version de paquete
        /// </summary>
        /// <param name="version">Versión a verificar</param>
        /// <returns>Valor que indica si se puede registrar</returns>
        public bool CanRegister(int version)
        {
            try
            {
                return this._machineService.CanRegister(version);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return false;
            }
        }

        /// <summary>
        /// Lista todos los paquetes de actualización donde su versión
        /// </summary>
        /// <param name="version">Versión a comparar</param>
        /// <returns>Lista de paquetes</returns>
        public List<UpdatePackage> ListUpdatePackagesGreaterOrEqualsThanVersion(int version)
        {
            try
            {
                return this._machineService.ListUpdatePackagesGreaterOrEqualsThanVersion(version);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<UpdatePackage>();
            }
        }

        /// <summary>
        /// Lanza un paquete de actualización
        /// </summary>
        /// <param name="idVersion">Id de la versión</param>
        /// <param name="uidMachines">Lista de identificación de las máquinas a actualizar</param>
        public int RaiseUpdatePackage(int idVersion, List<string> uidMachines)
        {
            try
            {
                var res = this._machineService.UpgradeMachines(idVersion, uidMachines);
                if (res.StateResult)
                {
                    //Notificar máquinas acerca de actualización
                    IHubContext context = this.GetHubContext();
                    try
                    {
                        if (context != null)
                        {
                            foreach (string uid in uidMachines)
                            {
                                var cli = this._clients.Values.Where((c) => c.UId.Equals(uid)).SingleOrDefault();
                                if (cli != null)
                                    if (cli.Service != null)
                                        if (cli.Service.Callback != null)
                                            if (!cli.Service.Callback.Equals(string.Empty))
                                                context.Clients.Client(cli.Service.Callback).OnNewUpdate(idVersion);
                            }
                            return 0; //Todo OK
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                        return -1;
                    }
                }
                else
                {
                    if (res.Message.Equals("{ERR1}")) //El paquete de actualización no existe
                        return 1;
                    if (res.Message.Equals("{ERR2}")) //Las máquinas a relacionar con el paquete no existen
                        return 2;
                    return -1;
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return -1; //Error desconocido
            }
        }

        /// <summary>
        /// Obtiene el nombre del paquete de actualización Ghost
        /// </summary>
        /// <returns>Obtiene el nombre del archivo ejecutable de actualización ghost</returns>
        public string GetUpgradeFileGhostService()
        {
            try
            {
                return Helpers.Helper.GetUpgradeFileGhostService();
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene un valor que indica si la versión actual del agente Ghost
        /// es menor a la disponible en el servicio
        /// </summary>
        /// <param name="ver">Versión del agente ghost</param>
        /// <returns>Valor que indica si se debe actualizar el agente ghost</returns>
        public bool HasNewGhostUpdate(System.Version ver)
        {
            try
            {
                string pathFiles = Helpers.Helper.GetUpgradeFilesFolder();
                string upgradeFile = Helpers.Helper.GetUpgradeFileGhostService();
                if (!pathFiles.Trim().Equals(string.Empty) && !upgradeFile.Trim().Equals(string.Empty))
                {
                    if (File.Exists(Path.Combine(pathFiles, upgradeFile)))
                    {
                        Assembly ass = Assembly.ReflectionOnlyLoadFrom(Path.Combine(pathFiles, upgradeFile));
                        AssemblyName asn = ass.GetName();
                        Version fver = asn.Version;
                        return (ver.CompareTo(fver) < 0 ? true : false);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return false;
            }
        }

        /// <summary>
        /// Envia la orden de comprobar actualización a todos los agentes ghost
        /// </summary>
        public void GetNewGhostUpdate()
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    foreach (KeyValuePair<string, Models.Subscriber> cli in this._clients)
                    {
                        if (cli.Value.Service != null)
                            if (cli.Value.Service.Callback != null)
                                if (!cli.Value.Service.Callback.Equals(string.Empty))
                                    context.Clients.Client(cli.Value.Service.Callback).OnGetNewGhostUpdate();
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        #endregion

        #region OSClients

        /// <summary>
        /// Refresca la lista de clientes
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="clients">Lista de clientes a refrescar</param>
        public void RefreshClientList(string uid, List<Models.OSClient> clients)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    if (this._clients.ContainsKey(uid))
                    {
                        this._clients[uid].Service.Clients.Clear();
                        this._clients[uid].Service.Clients.AddRange(clients);
                    }
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        context.Clients.Client(kv.Callback).OnRefreshClientList(uid, clients);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Notifia la subscripción de un nuevo cliente a la máquina
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="client">Cliente en la máquina</param>
        public void ClientSubscribed(string uid, Models.OSClient client)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    if (this._clients.ContainsKey(uid))
                    {
                        this._clients[uid].Service.Clients.Add(client);
                    }
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        context.Clients.Client(kv.Callback).OnClientSubscribed(uid, client);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        /// <summary>
        /// Notifia la desubscripción de un nuevo cliente a la máquina
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="client">Identificación del cliente en la máquina</param>
        public void ClientUnsubscribed(string uid, Models.OSClient client)
        {
            IHubContext context = this.GetHubContext();
            try
            {
                if (context != null)
                {
                    if (this._clients.ContainsKey(uid))
                    {
                        this._clients[uid].Service.Clients.RemoveAt(this._clients[uid].Service.Clients.FindIndex(new Predicate<Models.OSClient>((c) => c.UId.Equals(client.UId))));
                    }
                    foreach (var kv in this._clients.Values.Where((s) => !s.Callback.Equals(string.Empty)).ToList())
                    {
                        context.Clients.Client(kv.Callback).OnClientUnsubscribed(uid, client);
                    }
                }
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
            }
        }

        #endregion

        #region Security

        /// <summary>
        /// Lista todas las máquina registradas en la base de datos
        /// </summary>
        /// <returns>Lista de máquinas registradas</returns>
        public List<Models.Subscriber> ListSubscribers()
        {
            List<Machines> machines = this._machineService.ListAllMachines();
            List<Models.Subscriber> subs = new List<Models.Subscriber>();
            foreach (var m in machines)
            {
                if (this._clients.ContainsKey(m.UID)) //Si la máquina esta conectada
                {
                    this._clients[m.UID].RegMachineDate = m.RegDate;
                    this._clients[m.UID].NickName = m.NickName;
                    subs.Add(this._clients[m.UID]);
                }
                else //Si no, se agrega la máquina a un subscriptor vacío
                {
                    subs.Add(new Models.Subscriber());
                    subs[subs.Count - 1].UId = m.UID;
                    subs[subs.Count - 1].Name = m.Name;
                    subs[subs.Count - 1].NickName = m.NickName;
                    subs[subs.Count - 1].ClientVersion = m.ClientVersion;
                    subs[subs.Count - 1].OSName = m.OSName;
                    subs[subs.Count - 1].Architecture = m.Architecture;
                    subs[subs.Count - 1].IPs = m.IPs;
                    subs[subs.Count - 1].MACs = m.MACs;
                    subs[subs.Count - 1].LastSubscriptionDate = m.LastUpDate;
                    subs[subs.Count - 1].RegMachineDate = m.RegDate;
                }
            }
            return subs;
        }

        /// <summary>
        /// Graba o actualiza los datos de la máquina
        /// </summary>
        /// <param name="subs">Subscriptor quien tiene los datos de la máquina</param>
        private int SaveMachine(Models.Subscriber subs)
        {
            if (subs.Type != -1) //Se actualiza o se registra si el subscriptor no es una consola
            {
                Machines machine = new Machines();
                machine.UID = subs.UId;
                machine.Name = subs.Name;
                machine.NickName = subs.Name;
                machine.ClientVersion = subs.ClientVersion;
                machine.OSName = subs.OSName;
                machine.Architecture = subs.Architecture;
                machine.MACs = subs.MACs;
                machine.IPs = subs.IPs;
                machine.RegDate = subs.LastSubscriptionDate;
                machine.LastUpDate = subs.LastSubscriptionDate;
                var res = this._machineService.SaveMachine(machine);
                if (!res.StateResult)
                {
                    IndigoManagementExceptions.HandleException(new Exception(res.Message), "ApplicationPolicy");
                    return 0;
                }
                return res.Message.Equals("I") ? 1 : 2;
            }
            return -1;
        }

        #endregion
    }
}