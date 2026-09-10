
namespace DistributedService.Notification.SignalR.Models
{
    public class AppUser
    {
        #region Properties

        /// <summary>
        /// Código del usuario
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cargo del usuario
        /// </summary>
        public string Position { get; set; }

        #endregion
    }
}