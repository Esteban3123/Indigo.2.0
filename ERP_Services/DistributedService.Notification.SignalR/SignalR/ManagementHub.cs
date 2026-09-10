using DistributedService.Notification.SignalR.AdminServices;
using Domain.Security.Entities;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DistributedService.Notification.SignalR.SignalR
{
    public class ManagementHub : Hub
    {
        #region Base Methods

        /// <summary>
        /// Ocurre cuando se conecta un cliente
        /// </summary>
        public override System.Threading.Tasks.Task OnConnected()
        {
            Models.Subscriber sub = JsonConvert.DeserializeObject<Models.Subscriber>(Context.Headers["Subscriber"]);
            if (sub != null)
            {
                ManagementAdminService.Instance.Subscribe(sub, Context.ConnectionId);
            }
            return base.OnConnected();
        }

        /// <summary>
        /// Ocurre cuando se desconecta un cliente
        /// </summary>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            ManagementAdminService.Instance.Unsubscribe(Context.ConnectionId);
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
        /// Refresca los datos de un subscriptor
        /// </summary>
        /// <param name="subscriber"></param>
        public void RefreshSubscriber(Models.Subscriber subscriber)
        {
            ManagementAdminService.Instance.RefreshSubscriber(subscriber);
        }

        #endregion

        #region Security

        /// <summary>
        /// Lista los subscriptores registrados y conectados
        /// </summary>
        /// <returns>Lista de suscriptores</returns>
        public List<Models.Subscriber> ListSubscribers()
        {
            return ManagementAdminService.Instance.ListSubscribers();
        }

        /// <summary>
        /// Cambia el nombre de la máquina
        /// </summary>
        public bool ChangeNickNameMachine(string uidChanger, string uid, string newNickName)
        {
            return ManagementAdminService.Instance.ChangeNickNameMachine(uidChanger, uid, newNickName);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Ordena la optimización de emsamblados a una lista de
        /// máquinas
        /// </summary>
        /// <param name="uidMachines">Lista de máquinas a optimizar</param>
        public void OptimizeAssemblies(List<string> uidMachines)
        {
            ManagementAdminService.Instance.OptimizeAssemblies(uidMachines);
        }

        /// <summary>
        /// Notifica a un listado de máquinas que deben apagarse
        /// </summary>
        /// <param name="uids">Listado de máquinas a apagar</param>
        /// <param name="t">Tiempo en el que se deben apagar</param>
        /// <param name="comment">Comentario de apagado</param>
        public void Shutdown(List<string> uids, int t, string comment = "")
        {
            ManagementAdminService.Instance.Shutdown(uids, t, comment);
        }

        /// <summary>
        /// Notifica a un listado de máquinas que deben reiniciarse
        /// </summary>
        /// <param name="uids">Listado de máquinas a reiniciar</param>
        /// <param name="t">Tiempo en el que se deben reiniciar</param>
        /// <param name="comment">Comentario de reiniciado</param>
        public void Restart(List<string> uids, int t, string comment = "")
        {
            ManagementAdminService.Instance.Restart(uids, t, comment);
        }

        /// <summary>
        /// Notifica a una máquina que debe realizar una captura de pantalla
        /// </summary>
        /// <param name="uid">Identificación de la máquina a quien se notifica</param>
        /// <param name="uidClient">Identificación del cliente</param>
        public void Screenshot(string uid, string uidClient)
        {
            ManagementAdminService.Instance.Screenshot(uid, uidClient);
        }

        /// <summary>
        /// Obtiene un token para la carga de un archivo
        /// </summary>
        /// <param name="finfo">Información del archivo</param>
        /// <returns>Token generado para el archivo</returns>
        public string GetToken(FileInfo finfo)
        {
            return ManagementAdminService.Instance.GetToken(finfo);
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
            return ManagementAdminService.Instance.UploadFile(token, piece, isEndPiece);
        }

        /// <summary>
        /// Obtiene un token para la carga de un archivo
        /// </summary>
        /// <param name="finfo">Información del archivo</param>
        /// <returns>Token generado para el archivo</returns>
        public string GetToken2(FileInfo finfo)
        {
            return ManagementAdminService.Instance.GetToken2(finfo);
        }

        /// <summary>
        /// Cancela la carga de un archivo
        /// </summary>
        /// <param name="token">Token del archivo a cancelar</param>
        public void CancelUpload(string token)
        {
            ManagementAdminService.Instance.CancelUpload(token);
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
            return ManagementAdminService.Instance.UploadFile2(token, buffer, isEndPiece);
        }

        /// <summary>
        /// Mueve una imagen instantánea del directorio de archivos
        /// compartidos al directorio de imágenes instantáneas
        /// </summary>
        /// <param name="fileName">Nombre del archivo a mover</param>
        /// <param name="uid">Identificación de la máquina</param>
        public void MoveFileToScreenshot(string fileName, string uid)
        {
            ManagementAdminService.Instance.MoveFileToScreenshot(fileName, uid);
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
            ManagementAdminService.Instance.SendMessage(uids, author, title, message);
        }

        /// <summary>
        /// Cierra la sesión
        /// </summary>
        /// <param name="uidMachine">Identificación de la máquina</param>
        /// <param name="uidClient">Identificación del cliente</param>
        public void CloseSession(string uidMachine, string uidClient)
        {
            ManagementAdminService.Instance.CloseSession(uidMachine, uidClient);
        }

        /// <summary>
        /// Bloquea la sesión del cliente
        /// </summary>
        /// <param name="uidMachine">Identificación de la máquina</param>
        /// <param name="uidClient">Identificación del cliente</param>
        public void LockSession(string uidMachine, string uidClient)
        {
            ManagementAdminService.Instance.LockSession(uidMachine, uidClient);
        }

        #endregion

        #region OSClients

        /// <summary>
        /// Notifia la subscripción de un nuevo cliente a la máquina
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="client">Cliente en la máquina</param>
        public void ClientSubscribed(string uid, Models.OSClient client)
        {
            ManagementAdminService.Instance.ClientSubscribed(uid, client);
        }

        /// <summary>
        /// Notifia la desubscripción de un nuevo cliente a la máquina
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="client">Cliente en la máquina</param>
        public void ClientUnsubscribed(string uid, Models.OSClient client)
        {
            ManagementAdminService.Instance.ClientUnsubscribed(uid, client);
        }

        /// <summary>
        /// Refresca la lista de clientes
        /// </summary>
        /// <param name="uid">Identificación de la máquina</param>
        /// <param name="clients">Lista de clientes a refrescar</param>
        public void RefreshClientList(string uid, List<Models.OSClient> clients)
        {
            ManagementAdminService.Instance.RefreshClientList(uid, clients);
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
            ManagementAdminService.Instance.SetDateOnUpdate(uidMachine, idVersion);
        }

        /// <summary>
        /// Mueve un subscriptor a la lista de actualizaciones en curso
        /// </summary>
        /// <param name="uid">Identificación unica del subscriptor</param>
        /// <param name="isUpgrading">Valor que indica si el subscriptor inicia o finaliza actualización</param>
        /// <param name="toVersion">Versión a la que se va a actualizar</param>
        public void Upgrading(string uid, bool isUpgrading, string toVersion)
        {
            ManagementAdminService.Instance.Upgrading(uid, isUpgrading, toVersion);
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
            ManagementAdminService.Instance.SetUpgradeStatus(uid, status, details);
        }

        /// <summary>
        /// Obtiene una lista de los subscriptores que se encuentran en proceso
        /// de actualización
        /// </summary>
        /// <returns>Lista de subscriptores actualizando</returns>
        public List<Models.Subscriber> GetCurrentUpdates()
        {
            return ManagementAdminService.Instance.GetCurrentUpdates();
        }

        /// <summary>
        /// Lanza un paquete de actualización
        /// </summary>
        /// <param name="idVersion">Id de la versión</param>
        /// <param name="uidMachines">Lista de identificación de las máquinas a actualizar</param>
        public void RaiseUpdatePackage(int idVersion, List<string> uidMachines)
        {
            ManagementAdminService.Instance.RaiseUpdatePackage(idVersion, uidMachines);
        }

        /// <summary>
        /// Obtiene un valor que indica si se puede registrar
        /// la version de paquete
        /// </summary>
        /// <param name="version">Versión a verificar</param>
        /// <returns>Valor que indica si se puede registrar</returns>
        public bool CanRegisterPackage(int version)
        {
            return ManagementAdminService.Instance.CanRegister(version);
        }

        /// <summary>
        /// Mueve un paquete de actualización del directorio de archivos
        /// compartidos al directorio de paquetes de actualización disponibles
        /// </summary>
        /// <param name="fileName">Nombre del archivo a mover</param>
        /// <param name="packageInfo">Información de la versión</param>
        public void MoveFileToUpgradePackages(string fileName, Models.PackageInfo packageInfo)
        {
            ManagementAdminService.Instance.MoveFileToUpgradePackages(fileName, packageInfo);
        }

        /// <summary>
        /// Registra un paquete que ya ha subido
        /// </summary>
        /// <param name="packageInfo">Información del paquete</param>
        public void RegisterPackage(Models.PackageInfo packageInfo)
        {
            ManagementAdminService.Instance.RegisterPackage(packageInfo);
        }

        /// <summary>
        /// Lista todos los paquetes de actualización donde su versión
        /// </summary>
        /// <param name="version">Versión a comparar</param>
        /// <returns>Lista de paquetes</returns>
        public List<UpdatePackage> ListUpdatePackagesGreaterOrEqualsThanVersion(int version)
        {
            return ManagementAdminService.Instance.ListUpdatePackagesGreaterOrEqualsThanVersion(version);
        }

        /// <summary>
        /// Obtiene el paquete de actualización por Id de registro
        /// </summary>
        /// <param name="idVersion">Id de la versión a descargar</param>
        /// <returns>Paquete de actualización</returns>
        public UpdatePackage GetUpdatePackageById(int idVersion)
        {
            return ManagementAdminService.Instance.GetUpdatePackageById(idVersion);
        }

        /// <summary>
        /// Obtiene la URL de la carpeta para descargar los paquetes
        /// </summary>
        /// <returns>URL de los paquetes de actualización</returns>
        public string GetUrlPackages()
        {
            return ManagementAdminService.Instance.GetUrlPackages();
        }

        /// <summary>
        /// Obtiene un valor que indica si la versión actual del agente Ghost
        /// es menor a la disponible en el servicio
        /// </summary>
        /// <param name="ver">Versión del agente ghost</param>
        /// <returns>Valor que indica si se debe actualizar el agente ghost</returns>
        public bool HasNewGhostUpdate(System.Version ver)
        {
            return ManagementAdminService.Instance.HasNewGhostUpdate(ver);
        }

        /// <summary>
        /// Obtiene el nombre del paquete de actualización Ghost
        /// </summary>
        /// <returns>Obtiene el nombre del archivo ejecutable de actualización ghost</returns>
        public string GetUpgradeFileGhostService()
        {
            return ManagementAdminService.Instance.GetUpgradeFileGhostService();
        }

        /// <summary>
        /// Envia la orden de comprobar actualización a todos los agentes ghost
        /// </summary>
        public void GetNewGhostUpdate()
        {

        }

        #endregion
    }
}