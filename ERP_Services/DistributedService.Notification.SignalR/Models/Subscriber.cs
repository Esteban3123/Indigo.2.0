using System;
using System.Collections.Generic;

namespace DistributedService.Notification.SignalR.Models
{
    public class Subscriber
    {
        #region Properties

        /// <summary>
        /// Indice del subscripto
        /// </summary>
        public int NumberRow { get; set; }

        /// <summary>
        /// Identificación unica de la máquina
        /// </summary>
        public string UId { get; set; }

        /// <summary>
        /// Id de la conexión al cliente
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// Nombre de la máquina
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Alias puesto a la máquina por el administrador
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Version del cliente
        /// </summary>
        public string ClientVersion { get; set; }

        /// <summary>
        /// Nombre del sistema operativo
        /// </summary>
        public string OSName { get; set; }

        /// <summary>
        /// Arquitectura de la máquina
        /// </summary>
        public string Architecture { get; set; }

        /// <summary>
        /// Lista de direcciones IP
        /// </summary>
        public string IPs { get; set; }

        /// <summary>
        /// Lista de direcciones MAC
        /// </summary>
        public string MACs { get; set; }

        /// <summary>
        /// Servicio ghost en la máquina
        /// </summary>
        public Service Service { get; set; }

        /// <summary>
        /// Lista de clientes genesis conectados
        /// </summary>
        public List<Client> Clients { get; set; }

        /// <summary>
        /// Lista de clientes messenger conectados
        /// </summary>
        public List<Messenger> Messengers { get; set; }

        /// <summary>
        /// Fecha y hora de registro de la máquina
        /// </summary>
        public DateTime RegMachineDate { get; set; }

        /// <summary>
        /// Fecha y hora de la ultima subscripción
        /// </summary>
        public DateTime LastSubscriptionDate { get; set; }

        /// <summary>
        /// Tipo de subscriptor. Consola: -1, Servicio: 0, Cliente: 1, Messenger: 2
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Obtiene o asigna el estado de la actualización en el subscriptor
        /// </summary>
        public int UpgradeStatus { get; set; }

        /// <summary>
        /// Obtiene o asigna los detalles del estado en la actualización
        /// </summary>
        public string UpgradeDetails { get; set; }

        /// <summary>
        /// Obtiene o asigna la versión a la que se va a actualizar el cliente
        /// </summary>
        public string UpgradeToVersion { get; set; }

        #endregion
    }
}