///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Oscar Stiven Astudillo
/// Created          : 2024-20-02
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System.ServiceModel;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.Collections.Generic;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryConsignmentCostList
    {
        /// <summary>
        /// Guarda o actualiza un EntranceVoucher
        /// </summary>
        /// <param name="productGroup"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ConsignmentCostList> SaveConsignmentCostList(Domain.Entities.ConsignmentCostList consignmentCostList, AuditMessage audit);


        /// <summary>
        /// Obtiene la informacion por proveedor
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.ConsignmentCostList GetConsignmentCostListBySupplierId(int SupplierId, int OperatingUnitId);


        /// <summary>
        /// Elimina un registro 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActionResult DeleteConsignmentCostList(Domain.Entities.ConsignmentCostList consignmentCostList, AuditMessage audit);

        /// <summary>
        /// Lee y valida archivo Excel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.ConsignmentCostListDetail>> SetConsignmentConsListDetailFromFile(List<ImportFileRow> data);


        /// <summary>
        /// lee y valida lo obtenido del copyPaste
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.ConsignmentCostListDetail>> SetProductFeeDetailFromCopyandPaste(List<List<string>> data);

        /// <summary>
        /// Actualiza estado del registro
        /// </summary>
        /// <param name="consignmentCostList"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ConsignmentCostList> UpdateStateConsignmentCostList(int SupplierId, bool state, AuditMessage audit);


    }

}
