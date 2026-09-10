using Domain.Base.Entities;
using Domain.Inventory.POCO;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.SurgicalPackageProcess
{
    public interface ISurgicalPackageProcessAdminService : IDisposable
    {
        //Metodo que se encarga de realizar el proceso de solicitud de paquete qx, dispensación y confirmación hoja de gasto qx, para la integración
        ActionResult<string> AllSurgicalPackageTransaction(AllSurgicalPackageProcessWrapper objParams, AuditMessage audit, DateTime serverDate, string container);
    }
}
