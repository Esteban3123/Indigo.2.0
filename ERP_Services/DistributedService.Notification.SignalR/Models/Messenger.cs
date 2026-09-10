using System;

namespace DistributedService.Notification.SignalR.Models
{
    public class Messenger
    {
        #region Properties

        /// <summary>
        /// Id del proceso
        /// </summary>
        public long ProcessId { get; set; }

        /// <summary>
        /// Id de conexión al cliente
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// Nombre de la aplicación
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Versión de la aplicación
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Usuario logueado a la aplicacion
        /// </summary>
        public AppUser AppUser { get; set; }

        /// <summary>
        /// Ruta del ejecutable
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Fecha y hora de la ultima subscripción
        /// </summary>
        public DateTime LastSubscription { get; set; }

        #endregion
    }
}