//'************************************************************
//' Assembly         : Application.Inventory.IPhysicalInventoryAdminService
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 12/09/2014
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
using System.Transactions;
using Infrastructure.CrossCutting.Exceptions;
using Domain.Base;
using Domain.Crystal;

namespace Application.Inventory.PhysicalInventory
{
    public class PhysicalInventoryAdminService : IPhysicalInventoryAdminService
    {


        #region Variables
        private IPhysicalInventoryRepository _physicalInventoryRepository;
        private IKardexRepository _kardexRepository;
        private IInventoryProductRepository _productRepository;
        private IHCFARMEPDRepository _hCFARMEPDRepository;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public PhysicalInventoryAdminService(IPhysicalInventoryRepository physicalInventoryRepository, IKardexRepository kardexRepository, IInventoryProductRepository productRepository, IHCFARMEPDRepository HCFARMEPDRepository)
        {
            if ((physicalInventoryRepository == null))
            {
                throw new ArgumentNullException("Repositorio de productGroupsRepository vacio");
            }
            if ((kardexRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if (productRepository == null)
            {
                throw new ArgumentNullException("productRepository");
            }
            _physicalInventoryRepository = physicalInventoryRepository;
            _kardexRepository = kardexRepository;
            _productRepository = productRepository;
            _hCFARMEPDRepository = HCFARMEPDRepository;
        }
        #endregion

        private String createXmlKardex(List<Kardex> listKardex) {
            StringBuilder builder = new StringBuilder();
            String formatString = "<{0}>{1}</{0}>";

            foreach (Kardex kardex in listKardex)
            {
                builder.Append("<Kardex>");
                
                builder.Append(String.Format(formatString, "ProductId", kardex.ProductId));                
                builder.Append(String.Format(formatString, "WarehouseId", kardex.WarehouseId));
                if (kardex.BatchSerialId != null)
                    builder.Append(String.Format(formatString, "BatchSerialId", kardex.BatchSerialId));
                builder.Append(String.Format(formatString, "MovementType", kardex.MovementType));
                builder.Append(String.Format(formatString, "Quantity", kardex.Quantity));
                builder.Append(String.Format(formatString, "Value", kardex.Value.ToString().Replace(",", ".")));
                builder.Append(String.Format(formatString, "AffectAverageCost", kardex.AffectInventory));

                builder.Append("</Kardex>");
            }

            return builder.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listKardex"></param>
        /// <param name="entityId"></param>
        /// <param name="entityCode"></param>
        /// <param name="entityName"></param>
        /// <param name="creationUser"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        public ActionMessageResult SavePhysicalInventory(List<Kardex> listKardex, int entityId, string entityCode, string entityName, string creationUser, bool controlCost = false)
        {
            ActionMessageResult result = new ActionMessageResult();
            String xml = createXmlKardex(listKardex);
            SP_SavePhysicalInventoryKardex_Result resultSP = _physicalInventoryRepository.SavePhysicalInventoryKardex(xml, entityId, entityCode, entityName, creationUser, controlCost);
            result.Message = resultSP.Message;
            if (resultSP.Status != 1)
            {
                result.StateResult = false;
            }
            else {
                result.StateResult = true;
            }
            return result;
        }


        /// <summary>
        /// Obtiene el listado de inventario fisico de un producto en un almacen especifico
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public List<Domain.Entities.PhysicalInventory> GetListPhysicalInventory(int productId, int warehouseId, bool isInput = false)
        {
            try
            {
                return _physicalInventoryRepository.GetListPhysicalInventory(productId, warehouseId, isInput);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PhysicalInventory>();
            }
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
            try
            {
                return _physicalInventoryRepository.GetListPhysicalInventoryCustody(patientCode, admissionNumber, productId, warehouseId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PhysicalInventoryCustody>();
            }
        }

        /// <summary>
        /// Obtiene el listado de inventario fisico de un producto 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<Domain.Entities.PhysicalInventory> GetListPhysicalInventoryByProduct(int productId)
        {
            try
            {
                return _physicalInventoryRepository.GetListPhysicalInventoryByProduct(productId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PhysicalInventory>();
            }
        }

        /// <summary>
        /// retorna la cantidad total del producto por id del producto y id del almacen
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public int GetQuantityByProductWarehouse(int productId, int warehouseId)
        {
            try
            {
                return _physicalInventoryRepository.GetQuantityByProductWarehouse(productId, warehouseId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return 0;
            }
        }

        /// <summary>
        /// retorna un objeto de inventario fisico por id del producto y id del almacen
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public Domain.Entities.PhysicalInventory GetPhysicalInventory(int productId, int warehouseId)
        {
            try
            {
                return _physicalInventoryRepository.GetPhysicalInventory(productId, warehouseId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PhysicalInventory();
            }
        }

        public List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByCode(Dictionary<string, string> parameters, List<string> listCodes)
        {
            try
            {
                var xmlParameters = Utils.DictionaryToXML(parameters).ToString();
                var xmlATCs = this.ConvertToXmlCodes(listCodes);

                return _physicalInventoryRepository.SP_ListPhysicalInventoryByCode(xmlParameters, xmlATCs);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PhysicalInventory>();
            }
        }

        private string ConvertToXmlCodes(List<string> listCodes)
        {
            var builder = new StringBuilder();
            foreach (string code in listCodes)
            {
                builder.Append("<Data>");
                builder.Append("<Code>" + code + "</Code>");
                builder.Append("</Data>");
            }
            return builder.ToString();
        }

        public List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByATCNumber(string ATCNumber, int type, int userId)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("CareGroupId", "0");
            parameters.Add("UserId", userId.ToString());

            var listCodes = new List<string>();
            listCodes.Add(ATCNumber);

            return this.ListPhysicalInventoryByCode(parameters, listCodes);
        }

        public List<Domain.Entities.PhysicalInventory> ListPhysicalInventoryByATCNumberWithAdditionalInformation(string ATCNumber, int type, int userId, int careGroupId, decimal? TotalDose = decimal.Zero)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("CareGroupId", careGroupId.ToString());
            parameters.Add("UserId", userId.ToString());
            if (TotalDose > 0) parameters.Add("TotalDose", TotalDose.ToString());

            var listCodes = new List<string>();
            listCodes.Add(ATCNumber);

            return this.ListPhysicalInventoryByCode(parameters, listCodes);
        }

        /// <summary>
        /// funcion para consultar CUM de pestaña
        /// </summary>
        /// <param name="ATCCode"></param>
        /// <param name="AdmissionNumber"></param>
        /// <returns></returns>
        public ActionResult< List<Domain.Entities.PhysicalInventory>> ListPhysicalInventoryByATCCodeToMS(string ATCCode, string AdmissionNumber, string CodeSusceptibleMixingStation)
        {
            try
            {
                if (string.IsNullOrEmpty(ATCCode) || string.IsNullOrEmpty(AdmissionNumber)) { throw new Exception("Parametros Vacios"); }

                var Result = _physicalInventoryRepository.ExecuteQuery<Domain.Entities.CUMModel>(@"SELECT DISTINCT
                                                                                                    p.Id,
                                                                                                    p.WarehouseId,
                                                                                                    p.ProductId,
                                                                                                    p.BatchSerialId,
                                                                                                    p.Quantity,
                                                                                                    b.ExpirationDate,
                                                                                                    b.BatchCode,
                                                                                                    CONCAT(w.Code,' - ',w.Name) AS CodeNameWarehouse,
                                                                                                    CONCAT(ip.Code,' - ',ip.Name) AS CodeNameProduct,
                                                                                                    CASE 
                                                                                                        WHEN ud.MSClass <> 2 THEN hcf.CODPRODUC
                                                                                                        ELSE psms.MainDrugCode
                                                                                                    END AS Code,
	                                                                                                CASE pt.Class
		                                                                                                WHEN 1 THEN 'Grupo'
		                                                                                                WHEN 2 THEN 'Item Medicamento'
		                                                                                                WHEN 3 THEN 'Item Insumo'
		                                                                                                WHEN 4 THEN 'Item Otro'  
		                                                                                                WHEN 5 THEN 'Item Producción'
	                                                                                                END AS ClassName
                                                                                                FROM HCFARMEPD hcf
                                                                                                INNER JOIN HCFARMEPC hcc ON hcc.CODCONCEC = hcf.CODCONCEC
                                                                                                INNER JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON hcf.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
                                                                                                INNER JOIN MedicalHistory.PharmaDose pd ON psms.CodeSusceptibleMixingStation = pd.CodeSusceptibleMixingStation
                                                                                                INNER JOIN MixingStation.UnitDoseType ud ON ud.Id = pd.UnitDoseTypeId
                                                                                                INNER JOIN MixingStation.RequestPackageDetailStatus rpds ON pd.GroupingCodeDose = rpds.GroupingCodeDose
                                                                                                INNER JOIN Inventory.BatchSerial b ON rpds.ProductId = b.ProductId AND rpds.BatchCode = b.BatchCode
                                                                                                INNER JOIN Inventory.PhysicalInventory p ON b.Id = p.BatchSerialId
                                                                                                INNER JOIN Inventory.Warehouse w ON p.WarehouseId = w.Id
                                                                                                INNER JOIN Inventory.InventoryProduct ip ON p.ProductId = ip.Id
                                                                                                INNER JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
                                                                                                WHERE 
                                                                                                    p.Quantity > 0
                                                                                                    AND rpds.Status <> 6
                                                                                                    AND hcf.CodeSusceptibleMixingStation = {0}
                                                                                                    AND rpds.SendTo = 1
                                                                                                    AND w.Code = hcc.CODBODEGA
                                                                                                    AND (
                                                                                                        (ud.MSClass <> 2 AND hcf.CODPRODUC = {1} AND hcf.NUMINGRES = {2})
                                                                                                        OR
                                                                                                        (ud.MSClass = 2 AND hcf.NUMINGRES = {2}))
                                                                                                            ", CodeSusceptibleMixingStation, ATCCode,AdmissionNumber).Select(x => new Domain.Entities.PhysicalInventory
                {
                    Id = x.Id,
                    WarehouseId = x.WarehouseId,
                    ProductId = x.ProductId,
                    BatchSerialId = x.BatchSerialId
                                                                                            ,
                    Quantity = x.Quantity,
                    CodeNameBatchSerial = x.BatchCode,
                    BatchSerialExpiredDate = x.ExpirationDate
                                                                                            ,
                    CodeNameWarehouse = x.CodeNameWarehouse,
                    CodeNameProduct = x.CodeNameProduct,
                    Code = x.Code,
                    ClassName = x.ClassName
                }).ToList();
                ;

                if (Result is null || Result.Count == 0) { throw new Exception("La consulta no ha producido ningún resultado"); }

                Result = Result.Distinct().ToList();
                return new ActionResult<List<Domain.Entities.PhysicalInventory>> { StateResult = true, ObjectEmbbeded = Result };
            }

            catch (Exception ex)
            {
                return new ActionResult<List<Domain.Entities.PhysicalInventory>> {StateResult=false,Message=ex.Message };
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
            try
            {
                return _physicalInventoryRepository.ListPhysicalInventoryCustodyByATCNumber(ATCNumber, type, userId, admissionNumber);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PhysicalInventoryCustody>();
            }
        }



        /// <summary>
        /// metodo para obtener un inventario fisico cuando se hace por codigo de barras
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="batchCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult<List<Domain.Entities.PhysicalInventory>> GetPhysicalInventoryBarCode(string productCode, string batchCode, int userId)
        {
            try
            {
                return _physicalInventoryRepository.GetPhysicalInventoryBarCode(productCode, batchCode, userId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.PhysicalInventory>> { StateResult = false, Message = ex.Message };
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
        public ActionResult<List<Domain.Entities.PhysicalInventoryCustody>> GetPhysicalInventoryCustodyBarCode(string productCode, string batchCode, int userId, string admissionNumber)
        {
            try
            {
                return _physicalInventoryRepository.GetPhysicalInventoryCustodyBarCode(productCode, batchCode, userId, admissionNumber);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.PhysicalInventoryCustody>> { StateResult = false, Message = ex.Message };
            }
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
            try
            {
                return _physicalInventoryRepository.GetPhysicalInventoryCustodyByWareHouse(admissionNumber, wareHouseId, userId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PhysicalInventoryCustody>();
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
            try
            {
                return _physicalInventoryRepository.GetPhysicalInventoryCustodyByAdmissionWareHouse(patientCode, admissionNumber, wareHouseId, userId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PhysicalInventoryCustody>();
            }
        }


        public Domain.Entities.PhysicalInventory GetPhysicalInventoryByProductAndWarehouse(int productId, int warehouseId)
        {
            try
            {
                return _physicalInventoryRepository.GetPhysicalInventoryByProductAndWarehouse(productId, warehouseId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PhysicalInventory();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="productId"></param>
        /// <param name="movement"></param>
        /// <param name="warehouseId"></param>
        /// <param name="batchSerialId"></param>
        /// <param name="quantity"></param>
        /// <param name="entityId"></param>
        /// <param name="entityCode"></param>
        /// <param name="entityName"></param>
        /// <param name="creationUser"></param>
        /// <param name="controlCost"></param>
        /// <returns></returns>
        public ActionMessageResult SavePhysicalInventoryCustody(string admissionNumber, int productId, MovementType movement, int warehouseId, int? batchSerialId, int quantity, Decimal value, int entityId, string entityCode, string entityName, string creationUser, Domain.Entities.InventoryProduct product = null, bool AffectsAverageCost = false, bool controlCost = false)
        {
            ActionMessageResult result = new ActionMessageResult();
            List<Kardex> listKardex = new List<Kardex>()
                                {
                                    new Kardex()
                                    {
                                        ProductId = productId,
                                        WarehouseId = warehouseId,
                                        BatchSerialId = batchSerialId,
                                        MovementType = (byte)(movement == MovementType.Input ? 1 : 2),
                                        Quantity = quantity,
                                        Value = value,
                                        AffectInventory = AffectsAverageCost
                                    }
                                };
            String xml = createXmlKardex(listKardex);
            SP_SavePhysicalInventoryCustodyKardexCustody_Result resultSP = _physicalInventoryRepository.SavePhysicalInventoryKardexCustody(xml, admissionNumber, entityId, entityCode, entityName, creationUser, controlCost);
            result.Message = resultSP.Message;
            if (resultSP.Status != 1)
            {
                result.StateResult = false;
            }
            else
            {
                result.StateResult = true;
            }
            return result;

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
            return _physicalInventoryRepository.GetProductCustodyByPatientCodeAdmission(patientCode, admissionNumber);
        }

        #region IDisposable Support
        private bool disposedValue;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                _physicalInventoryRepository = null;
                _kardexRepository = null;
                _productRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }

    public enum MovementType
    {
        Input = 1,
        OutPut = 2
    }
}
