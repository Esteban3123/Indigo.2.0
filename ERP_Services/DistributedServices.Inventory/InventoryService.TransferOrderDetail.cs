
using Application.Inventory.TransferOrderDetail;
using DistributedServices.Inventory.Unity;
///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Juan Carlos Bermudez
/// Created          : 25-05-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {

        /// <summary>
        /// obtiene una lista de detalles de orden de traslado por unidad funcional o almacen de destino
        /// </summary>
        /// <param name="idFilter"></param>
        /// <param name="orderType"></param>
        /// <param name="dispatchTo"></param>
        /// <returns></returns>
        public List<Domain.Entities.TransferOrderDetail> ListTransferOrderDetailByTarget(int idFilter, int orderType, int dispatchTo)
        {
            using (var service = Container.Current.Resolve<ITransferOrderDetailAdminService>())
            {
                return service.ListTrasnferOrderDetailByTarget(idFilter, orderType, dispatchTo);
            }
            //return _transferOrderDetailAdminService.ListTrasnferOrderDetailByTarget(idFilter, orderType, dispatchTo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet ListAverageConsumptionTransfer(string parameters, Infrastructure.CrossCutting.Base.SessionValues session)
        {
            using (var service = Container.Current.Resolve<ITransferOrderDetailAdminService>())
            {
                return service.ListAverageConsumptionTransfer(parameters, session);
            }
        }
        /// <summary>
        /// Metodo que lista el reporte de entradas de inventario.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet ListAverageEntranceOrder(string parameters, Infrastructure.CrossCutting.Base.SessionValues session)
        {
            using (var service = Container.Current.Resolve<ITransferOrderDetailAdminService>())
            {
                return service.ListAverageEntranceOrder(parameters, session);
            }
        }

    }
}
