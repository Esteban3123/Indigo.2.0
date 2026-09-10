using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Application.Inventory.PhysicalInventory;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    partial class InventoryService
    {
        public List<Domain.Entities.PhysicalInventory> GetListPhysicalInventory(int productId, int warehouseId, bool isInput = false)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetListPhysicalInventory(productId, warehouseId, isInput);
            }
            //return _iphysicalInventoryAdminService.GetListPhysicalInventory(productId, warehouseId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientCode"></param>
        /// <param name="admissionNumber"></param>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public List<Domain.Entities.PhysicalInventoryCustody> GetListPhysicalInventoryCustody(string patientCode, string admissionNumber, int productId, int warehouseId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetListPhysicalInventoryCustody(patientCode, admissionNumber, productId, warehouseId);
            }
        }

        public List<Domain.Entities.PhysicalInventory> GetListPhysicalInventoryByProduct(int productId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetListPhysicalInventoryByProduct(productId);
            }
        }

        public List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByCode(Dictionary<string, string> parameters, List<string> listCodes)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.ListPhysicalInventoryByCode(parameters, listCodes);
            }
        }

        public List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByATCNumber(string ATCNumber, int type, int userId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.ListPhysicalInventoryByATCNumber(ATCNumber, type, userId);
            }
        }

        public List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByATCNumberWithAdditionalInformation(string ATCNumber, int type, int userId, int careGroupId, decimal? TotalDose = decimal.Zero)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.ListPhysicalInventoryByATCNumberWithAdditionalInformation(ATCNumber, type, userId, careGroupId, TotalDose);
            }
        }

        ///<summary>
        /// lista los inventarios fisicos por el numero ATC del producto
        /// </summary>
        /// <param name="ACTNumber"></param>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <param name="admissionNumber"></param>
        /// <returns></returns> 
        /// <remarks>HRR PBI3410</remarks>
        public List<Domain.Entities.PhysicalInventoryCustody> ListPhysicalInventoryCustodyByATCNumber(string ATCNumber, int type, int userId, string admissionNumber)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.ListPhysicalInventoryCustodyByATCNumber(ATCNumber, type, userId, admissionNumber);
            }
            //return _iphysicalInventoryAdminService.ListPhysicalInventoryByATCNumber(ATCNumber, type,userId );
        }

        public Domain.Entities.PhysicalInventory GetPhysicalInventoryByProductAndWarehouse(int productId, int warehouseId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetPhysicalInventoryByProductAndWarehouse(productId, warehouseId);
            }
            //return _iphysicalInventoryAdminService.GetPhysicalInventoryByProductAndWarehouse(productId, warehouseId);
        }

        public int GetQuantityByProductWarehouse(int productId, int warehouseId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetQuantityByProductWarehouse(productId, warehouseId);
            }
            //return _iphysicalInventoryAdminService.GetQuantityByProductWarehouse(productId, warehouseId);
        }

        public Domain.Entities.PhysicalInventory GetPhysicalInventory(int productId, int warehouseId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetPhysicalInventory(productId, warehouseId);
            }
            //return _iphysicalInventoryAdminService.GetPhysicalInventory(productId, warehouseId);
        }

        public Domain.Base.Entities.ActionResult<List<Domain.Entities.PhysicalInventory>> GetPhysicalInventoryBarCode(string productCode, string batchCode, int userId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetPhysicalInventoryBarCode(productCode, batchCode, userId);
            }
            //return _iphysicalInventoryAdminService.GetPhysicalInventoryBarCode(productCode, batchCode, userId);
        }

        /// <summary>
        /// Consulta productos en custodia con saldo por paciente y numero de ingreso
        /// </summary>
        /// <param name="patientCode">codigo de paciente</param>
        /// <param name="admissionNumber">numero de ingreso</param>
        /// <returns></returns>
        /// <remarks>HRR PBI3410</remarks>
        public List<Domain.Entities.SP_ProductCustody_Result> GetProductCustodyByPatientCodeAdmission(string patientCode, string admissionNumber)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetProductCustodyByPatientCodeAdmission(patientCode, admissionNumber);
            }
        }

        /// <summary>
        /// metodo para obtener un inventario fisico cuando se hace por codigo de barras
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="batchCode"></param>
        /// <param name="userId"></param>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        /// <remarks>HRR PBI3410</remarks>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.PhysicalInventoryCustody>> GetPhysicalInventoryCustodyBarCode(string productCode, string batchCode, int userId, string admissionNumber)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetPhysicalInventoryCustodyBarCode(productCode, batchCode, userId, admissionNumber);
            }
            //return _iphysicalInventoryAdminService.GetPhysicalInventoryBarCode(productCode, batchCode, userId);
        }

        /// <summary>
        /// Consulta el inventario fisico de custodia por control de ingreso, almacen y usuario
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="wareHouseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <remarks>HRR PBI3410</remarks>
        public List<Domain.Entities.PhysicalInventoryCustody> GetPhysicalInventoryCustodyByWareHouse(string admissionNumber, int wareHouseId, int userId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetPhysicalInventoryCustodyByWareHouse(admissionNumber, wareHouseId, userId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientCode"></param>
        /// <param name="admissionNumber"></param>
        /// <param name="wareHouseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Domain.Entities.PhysicalInventoryCustody> GetPhysicalInventoryCustodyByAdmissionWareHouse(string patientCode, string admissionNumber, int wareHouseId, int userId)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.GetPhysicalInventoryCustodyByAdmissionWareHouse(patientCode, admissionNumber, wareHouseId, userId);
            }
        }

        /// <summary>
        /// funcion para consultar CUM pestaña central de mezclas
        /// </summary>
        /// <param name="ATCCode"></param>
        /// <param name="AdmissionNumber"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.PhysicalInventory>> ListPhysicalInventoryByATCCodeToMS(string ATCCode, string AdmissionNumber, string CodeSusceptibleMixingStation)
        {
            using (var service = Container.Current.Resolve<IPhysicalInventoryAdminService>())
            {
                return service.ListPhysicalInventoryByATCCodeToMS(ATCCode,  AdmissionNumber, CodeSusceptibleMixingStation);
            }
        }
    }
}
