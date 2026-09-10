///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 03-02-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using Domain.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryProductRateDetail
    {
        /// <summary>
        /// Obtiene un detalle de la tarifa de productos por id del grupo de atención, id del producto y fecha de dispensación
        /// </summary>
        [OperationContract]
        ActionResult<Domain.Entities.ProductRateDetail> GetProductRateDetailByCareGroupIdProductIdServiceDate(int CareGroupId, int ProductId, DateTime ServiceDate);

        /// <summary>
        /// metodo para copiar y pergar informacion o importar un archivo de excel
        /// </summary>
        /// <param name="dataImportFile"></param>
        /// <param name="dataCopyPaste"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.ProductRateDetail>> SetCopyPasteOrImportFileProductRate(List<ImportFileRow> dataImportFile, List<List<string>> dataCopyPaste);

        /// <summary>
        /// otiene todas las tarifas de los productos enviados en el listado
        /// </summary>
        /// <param name="CareGroupId"></param>
        /// <param name="ProductId"></param>
        /// <param name="ServiceDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.ProductRateDetail> GetListProductRateDetailByCareGroupIdProductIdServiceDate(int CareGroupId, List<int> ProductId, DateTime ServiceDate);

        /// <summary>
        /// Servicio que tarifica un paquete en base a los productos que contiene
        /// </summary>
        /// <param name="CareGroupId"></param>
        /// <param name="PackageIds"></param>
        /// <param name="ServiceDate"></param>
        /// <param name="DoseQuantity"></param>
        /// <param name="MedicineQuantity"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.ProductRateDetailPackage>> GetPackageValuePerProduct(int CareGroupId, List<int> PackageIds, DateTime ServiceDate, List<Domain.Entities.ViewPharmaDoseMixingStation> ListviewPharmaDoseMixingStations);

        /// <summary>
        /// Funcion para retornar dirrectamente el valor del producto independientemente si espor tarifa fija o por porcentaje 
        /// </summary>
        /// <param name="CareGroupId"></param>
        /// <param name="ProductId"></param>
        /// <param name="ServiceDate"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<Domain.Entities.ProductRateDetail> GetProductRateDetailWithValue(int CareGroupId, int ProductId, DateTime ServiceDate);
    }
}
