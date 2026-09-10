using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributedService.Notification.SignalR.Models
{
    /// <summary>
    /// Representa un cliente de sesión windows
    /// </summary>
    public class OSClient
    {
        /// <summary>
        /// Obtiene o asigna la identificación unica del subscriptor
        /// </summary>
        public string UId { get; set; }

        /// <summary>
        /// Obtiene o asigna el nombre del usuario windows que ha iniciado sesión
        /// </summary>
        public string OSUserName { get; set; }

        /// <summary>
        /// Obtiene o asigna la invitación a escritorio remoto
        /// </summary>
        public string RDPInvitation { get; set; }

        /// <summary>
        /// Obtiene o asigna un valor que indica si este es el usuario activo
        /// </summary>
        public bool IsActive { get; set; }
    }
}