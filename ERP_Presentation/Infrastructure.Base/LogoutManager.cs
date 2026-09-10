using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Base
{
    public static class LogoutManager
    {
        // Delegado público que la UI puede registrar
        public static Action OnLogout { get; set; }

        private static int _logoutTriggered = 0;

        public static void TriggerLogoutIfNeeded()
        {
            // Solo el primer hilo que cambia de 0 a 1 ejecuta el logout
            if (Interlocked.CompareExchange(ref _logoutTriggered, 1, 0) == 0)
            {
                if (OnLogout != null)
                {
                    Task.Run(() => OnLogout.Invoke());
                }
            }
        }

        public static void Reset()
        {
            _logoutTriggered = 0;
        }
    }

}
