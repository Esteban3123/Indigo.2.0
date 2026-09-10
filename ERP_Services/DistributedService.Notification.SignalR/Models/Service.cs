using System;
using System.Collections.Generic;

namespace DistributedService.Notification.SignalR.Models
{
    public class Service
    {
        #region Properties

        /// <summary>
        /// Invitación para compartir escritorio remoto
        /// </summary>
        public string RDPInvitation { get; set; }

        /// <summary>
        /// Id de la conexión al cliente
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// Lista de clientes conectados
        /// </summary>
        public List<OSClient> Clients { get; set; }

        /// <summary>
        /// Version del servicio
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Ruta del cliente
        /// </summary>
        public string ClientPath { get; set; }

        /// <summary>
        /// Version del cliente
        /// </summary>
        public string ClientVersion { get; set; }

        /// <summary>
        /// Total de invitados conectados al RDP
        /// </summary>
        public int Attendees { get; set; }

        /// <summary>
        /// Fecha y hora de la ultima subscripción
        /// </summary>
        public DateTime LastSubscription { get; set; }

        #endregion
    }
}