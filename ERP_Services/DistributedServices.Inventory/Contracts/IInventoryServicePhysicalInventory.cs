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
    public interface IInventoryServicePhysicalInventory
    {

        /// <summary>
        /// metodo para obtener un inventario fisico cuando se hace por codigo de barras
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="batchCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.PhysicalInventory>> GetPhysicalInventoryBarCode(string productCode, string batchCode, int userId);
        /// <summary>
        /// Obtiene el listado de inventario fisico de un producto en un almacen especifico
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PhysicalInventory> GetListPhysicalInventory(int productId, int warehouseId, bool isInput = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientCode"></param>
        /// <param name="admissionNumber"></param>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PhysicalInventoryCustody> GetListPhysicalInventoryCustody(string patientCode, string admissionNumber, int productId, int warehouseId);

        [OperationContract]
        Domain.Entities.PhysicalInventory GetPhysicalInventoryByProductAndWarehouse(int productId, int warehouseId);

        /// <summary>
        /// Obtiene el listado de inventario fisico de un producto
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PhysicalInventory> GetListPhysicalInventoryByProduct(int productId);

        /// <summary>
        /// lista los inventarios fisicos por un listado de codigo de productos
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="listCodes"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByCode(Dictionary<string, string> parameters, List<string> listCodes);

        /// <summary>
        /// lista los inventarios fisicos por el numero ATC del producto
        /// </summary>
        /// <param name="ATCNumber"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByATCNumber(string ATCNumber, int type, int userId);

        /// <summary>
        /// lista los inventarios fisicos por el numero ATC del producto con información adicional
        /// </summary>
        /// <param name="ATCNumber"></param>
        /// <returns></returns>
        [OperationContract]
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
        [OperationContract]
        List<Domain.Entities.PhysicalInventoryCustody> ListPhysicalInventoryCustodyByATCNumber(string ATCNumber, int type, int userId, string admissionNumber);

        /// <summary>
        /// retorna la cantidad total del producto por id del producto y id del almacen
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        [OperationContract]
        int GetQuantityByProductWarehouse(int productId, int warehouseId);

        /// <summary>
        /// retorna un objeto de inventario fisico por id del producto y id del almacen
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        [OperationContract]
        Domain.Entities.PhysicalInventory GetPhysicalInventory(int productId, int warehouseId);

        /// <summary>
        /// Consulta productos en custodia con saldo por paciente y numero de ingreso
        /// </summary>
        /// <param name="patientCode">codigo de paciente</param>
        /// <param name="admissionNumber">numero de ingreso</param>
        /// <returns></returns>
        /// <remarks>HRR PBI3410</remarks>
        [OperationContract]
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
        [OperationContract]
        ActionResult<List<Domain.Entities.PhysicalInventoryCustody>> GetPhysicalInventoryCustodyBarCode(string productCode, string batchCode, int userId, string admissionNumber);

        /// <summary>
        /// Consulta el inventario fisico de custodia por control de ingreso, almacen y usuario
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="wareHouseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <remarks>HRR PBI3410</remarks>
        [OperationContract]
        List<Domain.Entities.PhysicalInventoryCustody> GetPhysicalInventoryCustodyByWareHouse(string admissionNumber, int wareHouseId, int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientCode"></param>
        /// <param name="admissionNumber"></param>
        /// <param name="wareHouseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.PhysicalInventoryCustody> GetPhysicalInventoryCustodyByAdmissionWareHouse(string patientCode, string admissionNumber, int wareHouseId, int userId);

        /// <summary>
        /// contrato para servicio de consulta CUM pestaña central de mezclas
        /// </summary>
        /// <param name="ATCCode"></param>
        /// <param name="AdmissionNumber"></param>
        /// <returns></returns>
        [OperationContract]
        ActionResult<List<Domain.Entities.PhysicalInventory>> ListPhysicalInventoryByATCCodeToMS(string ATCCode, string AdmissionNumber, string CodeSusceptibleMixingStation);
    }
}
