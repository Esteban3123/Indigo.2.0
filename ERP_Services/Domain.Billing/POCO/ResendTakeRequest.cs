using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO
{
    public class ResendTakeRequest
    {
        public int Take { get; set; } // Cantidad de mensajes a reenviar
    }
}
