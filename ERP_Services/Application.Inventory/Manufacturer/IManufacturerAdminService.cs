using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.Manufacturer
{
    public interface IManufacturerAdminService : IDisposable
    {
        /// <summary>
        /// Guarda o actualiza un fabricante
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Manufacturer> SaveManufacturer(Domain.Entities.Manufacturer manufacturer, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un fabricante
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult DeleteManufacturer(Domain.Entities.Manufacturer manufacturer, AuditMessage audit);

        /// <summary>
        /// Cambia el estado de la entidad de fabricante
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Manufacturer> ChangeStateManufacturer(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el fabricante por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Manufacturer> GetManufacturer(string code, AuditMessage audit);

        /// <summary>
        /// Consulta un fabricante por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.Manufacturer> GetManufacturerById(int idManufacturer, AuditMessage audit);
    }
}
