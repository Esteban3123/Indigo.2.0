using Infrastructure.CrossCutting.Base;
using Application.Inventory.ConsignmentCostList;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;
using DistributedServices.Inventory.Contracts;
using System.Collections.Generic;
using Domain.Base.Entities;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryConsignmentCostList

    {
        /// <summary>
        /// Guarda el registro de costos de consignacion
        /// </summary>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ConsignmentCostList> SaveConsignmentCostList(Domain.Entities.ConsignmentCostList consignmentCostList,  AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IConsignmentCostListAdminService>())
            {
                return service.SaveConsignmentCostList(consignmentCostList, audit);
            }
        }

        /// <summary>
        /// Obtiene informacion por proveedor
        /// </summary>
        /// <param name="idEntranceVoucher"></param>
        /// <returns></returns>
        public Domain.Entities.ConsignmentCostList GetConsignmentCostListBySupplierId(int SupplierId, int OperatingUnitId)
        {
            using (var service = Container.Current.Resolve<IConsignmentCostListAdminService>())
            {
                return service.GetConsignmentCostListBySupplierId(SupplierId, OperatingUnitId);
            }
        }

        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="consignmentCostList"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult DeleteConsignmentCostList(Domain.Entities.ConsignmentCostList consignmentCostList, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IConsignmentCostListAdminService>())
            {
                return service.DeleteConsignmentCostList(consignmentCostList, audit);
            }
        }

        /// <summary>
        /// Lee y valida archivo excel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.ConsignmentCostListDetail>> SetConsignmentConsListDetailFromFile(List<ImportFileRow> data)
        {
            using (var service = Container.Current.Resolve<IConsignmentCostListAdminService>())
            {
                return service.SetConsignmentConsListDetailFromFile(data, null);
            }
        }

        /// <summary>
        /// lee y validac lo que se obtuvo del copyPaste
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.ConsignmentCostListDetail>> SetProductFeeDetailFromCopyandPaste(List<List<string>> data)
        {
            using (var service = Container.Current.Resolve<IConsignmentCostListAdminService>())
            {
                return service.SetConsignmentConsListDetailFromFile(null, data);
            }
        }


        /// <summary>
        /// Actualiza estado del registro
        /// </summary>
        /// <param name="consignmentCostList"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.ConsignmentCostList> UpdateStateConsignmentCostList(int SupplierId, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IConsignmentCostListAdminService>())
            {
                return service.UpdateStateConsignmentCostList(SupplierId, state,audit);
            }
        }



    }
}
