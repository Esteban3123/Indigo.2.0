using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO
{
    /// <summary>
    /// POCO para obtener o establecer los valores por concepto de recaudo (Copagos, Cuotas moderadoras, pagos compartidos)
    /// </summary>
    public class CollectionValues
    {
        /// <summary>
        /// Monto del copago
        /// </summary>
        public decimal CopaymentAmount { get; set; }

        /// <summary>
        /// Monto de la cuota moderadora
        /// </summary>
        public decimal ModeratingFeeAmount { get; set; }

        /// <summary>
        /// Monto del pago compartido
        /// </summary>
        public decimal SharedPaymentAmount { get; set; }
    }
}
