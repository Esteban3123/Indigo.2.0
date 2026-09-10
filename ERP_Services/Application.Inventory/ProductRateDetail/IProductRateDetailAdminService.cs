//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Diego Andrés Roldán Lozano
// Created          : 03-02-2015
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Domain.Base.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.ProductRateDetail
{
    public interface IProductRateDetailAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene un detalle de la tarifa de productos por id del grupo de atención, id del producto y fecha de dispensación
        /// </summary>
        ActionResult<Domain.Entities.ProductRateDetail> GetProductRateDetailByCareGroupIdProductIdServiceDate(int CareGroupId, int ProductId, DateTime ServiceDate);

        /// <summary>
        /// metodo para copiar y pergar informacion o importar un archivo de excel
        /// </summary>
        /// <param name="dataImportFile"></param>
        /// <param name="dataCopyPaste"></param>
        /// <returns></returns>
        ActionResult<List< Domain.Entities.ProductRateDetail>>SetCopyPasteOrImportFileProductRate(List <ImportFileRow > dataImportFile,List<List<string>>dataCopyPaste);
        /// <summary>
        /// otiene todas las tarifas de los productos enviados en el listado
        /// </summary>
        /// <param name="CareGroupId"></param>
        /// <param name="ProductId"></param>
        /// <param name="ServiceDate"></param>
        /// <returns></returns>
        List<Domain.Entities.ProductRateDetail> GetListProductRateDetailByCareGroupIdProductIdServiceDate(int CareGroupId, List<int> ProductId, DateTime ServiceDate);

        /// <summary>
        /// Restorna el valor del paquete desagregado por producto
        /// </summary>
        /// <param name="CareGroupId"></param>
        /// <param name="PackageId"></param>
        /// <param name="ServiceDate"></param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.ProductRateDetailPackage>> GetPackageValuePerProduct(int CareGroupId, List<int> PackageId, DateTime ServiceDate, List<ViewPharmaDoseMixingStation> ListviewPharmaDoseMixingStations);

        /// <summary>
        /// Funcion para retornar dirrectamente el valor del producto independientemente si espor tarifa fija o por porcentaje 
        /// </summary>
        /// <param name="CareGroupId"></param>
        /// <param name="ProductId"></param>
        /// <param name="ServiceDate"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ProductRateDetail> GetProductRateDetailWithValue(int CareGroupId, int ProductId, DateTime ServiceDate);
    }
}
