using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistributedService.Notification.SignalR.Models
{
    public class PackageInfo
    {
        #region Properties

        /// <summary>
        /// Obtiene o asigna la identificación unica del paquete
        /// </summary>
        public string UId { get; set; }
        /// <summary>
        /// Obtiene o asigna el numero de versión
        /// </summary>
        public int NumberVersion { get; set; }
        /// <summary>
        /// Obtiene o asigna la versión de la actualización
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Obtiene o asigna la fecha de compilación
        /// </summary>
        public DateTime BuildDate { get; set; }
        /// <summary>
        /// Obtiene o asigna la descripción de la actualización
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}