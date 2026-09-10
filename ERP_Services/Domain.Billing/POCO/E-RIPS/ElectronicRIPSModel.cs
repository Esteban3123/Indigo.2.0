using Domain.Billing.POCO.E_RIPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Billing.POCO
{
    public class ElectronicRIPSModel
    {

        public string numDocumentoIdObligado { get; set; }

        public string numFactura { get; set; }

        public string tipoNota { get; set; }

        public string numNota { get; set; }

        public List<UsuarioModel> usuarios { get; set; }

    }
}
