/************************************************************************
 Assembly         : Domain.Inventory
 Author           : Oscar stiven Astudillo
 Created          : 2024-02-15
 Copyright        : (c) . All rights reserved.
***********************************************************************
*/
using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.ConsignmentCostList
{
    public interface IConsignmentCostListAdminService : IDisposable
    {

        /// <summary>
        /// Guarda un registro en lista de costos consignacion
        /// </summary>
        ActionResult<Domain.Entities.ConsignmentCostList> SaveConsignmentCostList(Domain.Entities.ConsignmentCostList consignmentCostList, AuditMessage audit);



        /// <summary>
        /// Obtiene informacion de la tabla encabezado y detalle
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        Domain.Entities.ConsignmentCostList GetConsignmentCostListBySupplierId(int SupplierId, int OperatingUnitId);


        /// <summary>
        /// Elimina un registro
        /// </summary>
        ActionResult DeleteConsignmentCostList(Domain.Entities.ConsignmentCostList consignmentCostList, AuditMessage audit);


        /// <summary>
        /// Lee y valida archivo excel
        /// </summary>
        /// <param name="dataimport"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.ConsignmentCostListDetail>> SetConsignmentConsListDetailFromFile(List<ImportFileRow> dataimport, List<List<string>> data);


        /// <summary>
        /// Actualiza estado del registro
        /// </summary>
        ActionResult<Domain.Entities.ConsignmentCostList> UpdateStateConsignmentCostList(int SupplierId, bool state, AuditMessage audit);



    }
}
