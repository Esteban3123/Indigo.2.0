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

        /// <summary>
        /// Calcula el precio de venta de un ítem de producción (producto terminado de central de mezclas)
        /// sumando las tarifas de cada componente real usado como materia prima para el lote.
        /// </summary>
        /// <param name="CareGroupId">Id del grupo de atención</param>
        /// <param name="ProductId">Id del producto terminado (ProductType.Class = 5)</param>
        /// <param name="ServiceDate">Fecha del servicio</param>
        /// <param name="BatchCode">Lote del producto terminado dispensado</param>
        /// <returns>Precio de venta total calculado desde los componentes reales del paquete</returns>
        [OperationContract]
        ActionResult<decimal> GetPackageSalePriceByProductId(int CareGroupId, int ProductId, DateTime ServiceDate, string BatchCode);

        /// <summary>
        /// Retorna los componentes tarifados del paquete estandar asociado a un producto terminado de dosis estandar.
        /// Uso exclusivo del flujo de dispensacion farmaceutica para generar orden de servicio con DatasourceType = 11.
        /// No debe usarse para paquetes personalizados ni para el flujo de control de cuentas hospitalario.
        /// </summary>
        /// <param name="CareGroupId">Id del grupo de atención</param>
        /// <param name="ProductId">Id del producto terminado (ProductType.Class = 5)</param>
        /// <param name="DispensedQuantity">Cantidad dispensada del producto terminado</param>
        /// <param name="ServiceDate">Fecha del servicio</param>
        /// <param name="BatchCode">Lote del producto terminado dispensado</param>
        [OperationContract]
        ActionResult<List<Domain.Entities.ProductRateDetailPackage>> GetPackageRateDetailListByProductId(int CareGroupId, int ProductId, int DispensedQuantity, DateTime ServiceDate, string BatchCode);
    }
}
