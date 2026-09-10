//'************************************************************
//' Assembly         : Application.Inventory.PhysicalInventory
//' Author           : Cristhian Mauricio Salazar
//' Created          : 11/01/2015
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.PhysicalInventory
{
    public interface IPhysicalInventoryAdminService : IDisposable
    {

        /// <summary>
        /// Funcion para guardar en el inventario fisico y en el KARDEX, la funcion se encarga de calcular el promedio ponderado del producto y actualizarlo en la tabla de productos
        /// </summary>
        /// <param name="product">Producto que va hacer el movimiento</param>
        /// <param name="movement">Tipo de movimiento, Entrada o Salida</param>
        /// <param name="warehouseId">Id del almacen</param>
        /// <param name="batchSerialId">Id del lote o serial</param>
        /// <param name="quantity">Cantidad del producto</param>
        /// <param name="entityId">Id de la entidad que esta generando el movimiento</param>
        /// <param name="entityCode">Codigo de la entidad que esta generando el movimiento</param>
        /// <param name="entityName">Nombre de la entidad que esta generando el movimiento (Es el nombre de la clase o tabla)</param>
        /// <param name="creationUser">Codigo del usuario que esta generando el movimiento</param>
        /// <param name="controlCost">Confirmación de la modificación del costo promedio del producto por un valor fuera del limite establecido</param>
        /// <returns></returns>
        ActionMessageResult SavePhysicalInventory(List<Kardex> listKardex, int entityId, string entityCode, string entityName, string creationUser, bool controlCost = false);
        /// <summary>
        /// Obtiene el listado de inventario fisico de un producto en un almacen especifico
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        List<Domain.Entities.PhysicalInventory> GetListPhysicalInventory(int productId, int warehouseId, bool isInput = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientCode"></param>
        /// <param name="admissionNumber"></param>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        List<Domain.Entities.PhysicalInventoryCustody> GetListPhysicalInventoryCustody(string patientCode, string admissionNumber, int productId, int warehouseId);
        /// <summary>
        /// Obtiene el listado de inventario fisico de un producto
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<Domain.Entities.PhysicalInventory> GetListPhysicalInventoryByProduct(int productId);
        /// <summary>
        /// lista los inventarios fisicos por un listado de codigo de productos
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="listCodes"></param>
        /// <returns></returns>
        List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByCode(Dictionary<string, string> parameters, List<string> listCodes);
        /// <summary>
        /// lista los inventarios fisicos por el numero ATC del producto
        /// </summary>
        /// <param name="ATCNumber"></param>
        /// <returns></returns>
        List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByATCNumber(string ATCNumber, int type,int userId);
        /// <summary>
        /// lista los inventarios fisicos por el numero ATC del producto con información adicional
        /// </summary>
        /// <param name="ATCNumber"></param>
        /// <returns></returns>
        List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByATCNumberWithAdditionalInformation(string ATCNumber, int type, int userId, int careGroupId, decimal? TotalDose = decimal.Zero);
        ///<summary>
        /// lista los inventarios fisicos por el numero ATC del producto
        /// </summary>
        /// <param name="ACTNumber"></param>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <param name="admissionNumber"></param>
        /// <returns></returns> 
        /// <remarks>HRR PBI3410</remarks>
        List<Domain.Entities.PhysicalInventoryCustody> ListPhysicalInventoryCustodyByATCNumber(string ATCNumber, int type, int userId, string admissionNumber);
        /// <summary>
        /// retorna la cantidad total del producto por id del producto y id del almacen
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        int GetQuantityByProductWarehouse(int productId, int warehouseId);
        /// <summary>
        /// retorna un objeto de inventario fisico por id del producto y id del almacen
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        Domain.Entities.PhysicalInventory GetPhysicalInventory(int productId, int warehouseId);

        Domain.Entities.PhysicalInventory GetPhysicalInventoryByProductAndWarehouse(int productId, int warehouseId);
        /// <summary>
        /// metodo para obtener un inventario fisico cuando se hace por codigo de barras
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="batchCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        ActionResult< List< Domain.Entities.PhysicalInventory>> GetPhysicalInventoryBarCode(string productCode, string batchCode, int userId);

        /// <summary>
        /// Funcion para guardar en el inventario fisico y en el KARDEX, la funcion se encarga de calcular el promedio ponderado del producto y actualizarlo en la tabla de productos
        /// </summary>
        /// <param name="admissionNumber">Id de control de ingreso</param>
        /// <param name="product">Producto que va hacer el movimiento</param>
        /// <param name="movement">Tipo de movimiento, Entrada o Salida</param>
        /// <param name="warehouseId">Id del almacen</param>
        /// <param name="batchSerialId">Id del lote o serial</param>
        /// <param name="quantity">Cantidad del producto</param>
        /// <param name="entityId">Id de la entidad que esta generando el movimiento</param>
        /// <param name="entityCode">Codigo de la entidad que esta generando el movimiento</param>
        /// <param name="entityName">Nombre de la entidad que esta generando el movimiento (Es el nombre de la clase o tabla)</param>
        /// <param name="creationUser">Codigo del usuario que esta generando el movimiento</param>
        /// <param name="controlCost">Confirmación de la modificación del costo promedio del producto por un valor fuera del limite establecido</param>
        /// <returns></returns>
        ActionMessageResult SavePhysicalInventoryCustody(string admissionNumber, int productId, MovementType movement, int warehouseId, Nullable<int> batchSerialId, int quantity, Decimal Value, int entityId, String entityCode, String entityName, String creationUser, Domain.Entities.InventoryProduct product = null, bool AffectsAverageCost = true, bool controlCost = false);

        /// <summary>
        /// Consulta productos en custodia con saldo por paciente y numero de ingreso
        /// </summary>
        /// <param name="patientCode">codigo de paciente</param>
        /// <param name="admissionNumber">numero de ingreso</param>
        /// <returns></returns>
        /// <remarks>HRR PBI3410</remarks>
        List<Domain.Entities.SP_ProductCustody_Result> GetProductCustodyByPatientCodeAdmission(string patientCode, string admissionNumber);

        /// <summary>
        /// metodo para obtener un inventario fisico cuando se hace por codigo de barras
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="batchCode"></param>
        /// <param name="userId"></param>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        /// <remarks>HRR PBI3410</remarks>
        ActionResult<List<Domain.Entities.PhysicalInventoryCustody>> GetPhysicalInventoryCustodyBarCode(string productCode, string batchCode, int userId, string admissionNumber);

        /// <summary>
        /// Consulta el inventario fisico de custodia por control de ingreso, almacen y usuario
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="wareHouseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <remarks>HRR PBI3410</remarks>
        List<Domain.Entities.PhysicalInventoryCustody> GetPhysicalInventoryCustodyByWareHouse(string admissionNumber, int wareHouseId, int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientCode"></param>
        /// <param name="admissionNumber"></param>
        /// <param name="wareHouseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Domain.Entities.PhysicalInventoryCustody> GetPhysicalInventoryCustodyByAdmissionWareHouse(string patientCode, string admissionNumber, int wareHouseId, int userId);
        /// <summary>
        /// funcion para consultar CUM pestaña central de mezclas
        /// </summary>
        /// <param name="ATCCode"></param>
        /// <param name="AdmissionNumber"></param>
        /// <returns></returns>
        ActionResult<List<Domain.Entities.PhysicalInventory>> ListPhysicalInventoryByATCCodeToMS(string ATCCode, string AdmissionNumber, string CodeSusceptibleMixingStation);

    }
}
