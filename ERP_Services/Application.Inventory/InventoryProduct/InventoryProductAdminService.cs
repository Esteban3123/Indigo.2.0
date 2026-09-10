///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using Application.Base;
using Application.Events.Models;
using Application.Events.Serializers;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Crystal;
using Domain.Crystal.Entities;
using Domain.Entities;
using Domain.Entities.Service;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Queue;
using Infrastructure.CrossCutting.Resources;
using Infrastructure.Data.CrystalRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Inventory.InventoryProduct
{
    public class InventoryProductAdminService : IInventoryProductAdminService
    {

        #region Variables

        private IInventoryProductRepository _productRepository;
        private IProductTypeRepository _productTypeRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IProductHierarchyRepository _hierarchyRepository;
        private IINPRODPATRepository _INPRODPATRepository;
        private IIHLISTPRORepository _IHLISTPRORepository;
        private IATCRepository _ATCRepository;
        private IPOSPathologiesRepository _POSPathologiesRepository;
        private IThirdPartyRepository _ThirdPartyRepository;
        private IPhysicalInventoryRepository _PhysicalInventoryRepository;
        private IFactoryQueue _factoryQueue;
        private IMedicationTypeRepository _medicationTypeRepository;
        #endregion

        #region Builder

        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public InventoryProductAdminService(IInventoryProductRepository productRepository, IInventorySequenceDetailRepository sequenseRepository,
            IProductHierarchyRepository hierarchyRepository, IProductTypeRepository productTypeRepository, IINPRODPATRepository INPRODPATRepository,
            IIHLISTPRORepository IHLISTPRORepository, IATCRepository ATCRepository, IPOSPathologiesRepository POSPathologiesRepository,
            IThirdPartyRepository ThirdPartyRepository, IPhysicalInventoryRepository PhysicalInventoryRepository, IFactoryQueue FactoryQueue,
            IMedicationTypeRepository MedicationTypeRepository)
        {
            if ((productRepository == null))
            {
                throw new ArgumentNullException("Repositorio de productRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if (productTypeRepository == null)
            {
                throw new ArgumentNullException("productTypeRepository");
            }
            if (PhysicalInventoryRepository == null)
            {
                throw new ArgumentNullException("PhysicalInventoryRepository");
            }
            _productRepository = productRepository;
            _sequenseRepository = sequenseRepository;
            _hierarchyRepository = hierarchyRepository;
            _productTypeRepository = productTypeRepository;
            _INPRODPATRepository = INPRODPATRepository;
            _IHLISTPRORepository = IHLISTPRORepository;
            _ATCRepository = ATCRepository;
            _POSPathologiesRepository = POSPathologiesRepository;
            _ThirdPartyRepository = ThirdPartyRepository;
            _PhysicalInventoryRepository = PhysicalInventoryRepository;
            _factoryQueue = FactoryQueue;
            _medicationTypeRepository = MedicationTypeRepository;
        }

        #endregion

        #region Const
        private const string MEDICATION_TYPE_1 = "01";
        private const string MEDICATION_TYPE_4 = "04";
        #endregion

        #region Methods

        /// <summary>
        /// Obtiene un producto por id
        /// </summary>
        public Domain.Entities.InventoryProduct GetInventoryProductById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _productRepository.GetInventoryProductById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene registro de la tabla inventoryProduct filtrando por una lista de Ids
        /// </summary>
        /// <param name="ListIds"></param>
        /// <returns></returns>
        public List<Domain.Entities.InventoryProduct> GetInventoryProductByIds(List<int> ListIds)
        {
            if (ListIds.Count == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _productRepository.GetByFilter(x=> ListIds.Contains(x.Id),false, new List<string> { "ProductType" }).ToList();
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene un producto por id con su grupo y subgrupo
        /// </summary>
        public Domain.Entities.InventoryProduct GetInventoryProductByIdSimple(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _productRepository.GetInventoryProductByIdSimple(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        public Domain.Entities.InventoryProduct GetInventoryProductByCodeWithProducGroup(string code)
        {
            if (code == string.Empty)
            {
                throw new ArgumentNullException("code");
            }
            try
            {
                return _productRepository.GetInventoryProductByCodeWithProducGroup(code);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene un producto por codigo
        /// </summary>
        public ActionResult<Domain.Entities.InventoryProduct> GetInventoryProduct(string code, AuditMessage audit)
        {
            if (code == string.Empty)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.InventoryProduct product = _productRepository.GetInventoryProduct(code);
                if (product != null && product.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryProduct> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryProduct>(product, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryProduct> { StateResult = true, ObjectEmbbeded = product };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryProduct> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un producto por codigo de forma asincrona
        /// </summary>
        public async Task<ActionResult<Domain.Entities.InventoryProduct>> GetInventoryProductAsync(string code, AuditMessage audit)
        {
            if (code == string.Empty)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.InventoryProduct product = await _productRepository.GetInventoryProductAsync(code);
                if (product != null && product.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryProduct> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryProduct>(product, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryProduct> { StateResult = true, ObjectEmbbeded = product };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryProduct> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un producto por id sin agregados
        /// </summary>
        public Domain.Entities.InventoryProduct GetInventoryProductByIdWithoutAggregates(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _productRepository.GetInventoryProductByIdWithoutAggregates(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene un producto por codigo sin agregados
        /// </summary>
        public Domain.Entities.InventoryProduct GetInventoryProductByCodeWithoutAggregates(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
            try
            {
                return _productRepository.GetInventoryProductByCodeWithoutAggregates(code);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Llena el listado de Jerarquías de un producto
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public ActionResult<System.Collections.Generic.List<Domain.Entities.ProductHierarchy>> LoadListHierarchyProduct(Domain.Entities.InventoryProduct product)
        {
            try
            {
                InventoryServices service = new InventoryServices(_productRepository);
                List<Domain.Entities.ProductHierarchy> _listReturn = service.LoadListHierarchyProduct(product);

                return new ActionResult<System.Collections.Generic.List<Domain.Entities.ProductHierarchy>>() { StateResult = true, ObjectEmbbeded = _listReturn };
            }
            catch (System.Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<System.Collections.Generic.List<Domain.Entities.ProductHierarchy>>() { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        public async Task<ActionResult<Domain.Entities.InventoryProduct>> SaveInventoryProductAsync(Domain.Entities.InventoryProduct product, AuditMessage audit, long idSecuence = 0)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            var txSettings = new TransactionOptions
            {
                Timeout = TransactionManager.MaximumTimeout,
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    string xml = ConvertProductToXml(product);

                    SP_SaveInventoryProduct_Result resultStore = await _productRepository.SP_SaveInventoryProductAsync(xml, audit.CodeUser, 0);
                    if (resultStore.CodeResult == 999)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = resultStore.MessageResult };
                    }

                    if (product.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added) {
                        product.CreationUser = audit.CodeUser; product.CreationDate = DateTime.Now;
                        product.ModificationUser = audit.CodeUser; product.ModificationDate = DateTime.Now;
                    } else {
                        product.ModificationUser = audit.CodeUser;
                        product.ModificationDate = DateTime.Now;
                    }

                    List<ProductBarcode> listBarCode = new List<ProductBarcode>();
                    if (product.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ProductBarcode"))
                    {
                        listBarCode = (from Domain.Entities.ProductBarcode e in product.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "ProductBarcode").ToList()[0].Value select e).ToList();
                    }

                    if (product.ProductBarcode != null && product.ProductBarcode.Count > 0)
                    {
                        (from ProductBarcode e in product.ProductBarcode select e).ToList().ForEach(x => { listBarCode.Add(x); });
                    }

                    if (listBarCode.Count > 0) {
                        (from x in listBarCode select x).ToList().ForEach(x => { product.ProductBarcode.Add(x); });
                    }

                    product.Code = resultStore?.Code;

                    TriggerEvent(product, audit);
                    scope.Complete();

                    return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.SUCCESS, StateResult = true, ObjectEmbbeded = product };
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    if (ex.InnerException.InnerException.Message.Contains("CODUSUARI") || ex.InnerException.InnerException.Message.Contains("CODUSUMOD"))
                    {
                        return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = "El usuario " + audit.CodeUser + " no esta creado en Crystal" };
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                    }

                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryProduct>
                    {
                        StatusCode = eStatusResult.EXCEPTION,
                        StateResult = false,
                        Message = IndigoManagementExceptions.GetExceptionDetails(ex),
                        MessageResult = new List<String> { IndigoManagementExceptions.GetExceptionDetails(ex) }
                    };
                }
            }
        }

        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        [Obsolete("Este método es obsoleto. Utilice SaveInventoryProductAsync en su lugar.")]
        public ActionResult<Domain.Entities.InventoryProduct> SaveInventoryProduct(Domain.Entities.InventoryProduct product, AuditMessage audit, long idSecuence = 0)
        {
            // Se llama a la implementación asíncrona de forma síncrona para reutilizar el código.
            // Task.Run evita posibles deadlocks en contextos de sincronización.
            return Task.Run(() => SaveInventoryProductAsync(product, audit, idSecuence)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Guarda o actualiza un tipo de producto
        /// </summary>
        public ActionResult<Domain.Entities.InventoryProduct> SaveProductsCharge(Domain.Entities.InventoryProduct product, AuditMessage audit, long idSecuence = 0)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    //se valida que exista el producto segun el codigo de la lista

                    string xml = ConvertProductToXml(product);
                    List<ProductBarcode> listBarCode = new List<ProductBarcode>();
                    if (product.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ProductBarcode")) { listBarCode = (from Domain.Entities.ProductBarcode e in product.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "ProductBarcode").ToList()[0].Value select e).ToList(); }
                    if (product.ProductBarcode != null || product.ProductBarcode.Count > 0) { (from ProductBarcode e in product.ProductBarcode select e).ToList().ForEach(x => { listBarCode.Add(x); }); }
                    SP_SaveInventoryProduct_Result resultStore = _productRepository.SP_SaveInventoryProduct(xml, audit.CodeUser, 0);
                    if (resultStore.CodeResult == 999)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = resultStore.MessageResult };
                    }
                    if (product.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added) { product.CreationUser = audit.CodeUser; product.CreationDate = DateTime.Now; product.ModificationUser = audit.CodeUser; product.ModificationDate = DateTime.Now; } else { product.ModificationUser = audit.CodeUser; product.ModificationDate = DateTime.Now; }
                    if (listBarCode.Count > 0) { (from x in listBarCode select x).ToList().ForEach(x => { product.ProductBarcode.Add(x); }); }
                    product.Code = resultStore?.Code;
                    TriggerEvent(product, audit);
                    scope.Complete();

                    return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.SUCCESS, StateResult = true, ObjectEmbbeded = product };
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    if (ex.InnerException.InnerException.Message.Contains("CODUSUARI") || ex.InnerException.InnerException.Message.Contains("CODUSUMOD"))
                    {
                        return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = "El usuario " + audit.CodeUser + " no esta creado en Crystal" };
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                    }

                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryProduct> { StatusCode = eStatusResult.WARNING, StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryProduct>
                    {
                        StatusCode = eStatusResult.EXCEPTION,
                        StateResult = false,
                        Message = IndigoManagementExceptions.GetExceptionDetails(ex),
                        MessageResult = new List<String> { IndigoManagementExceptions.GetExceptionDetails(ex) }
                    };
                }
            }
        }

        /// <summary>
        /// VERSIÓN OPTIMIZADA - Actualiza el estado de un producto de forma asíncrona
        /// </summary>
        /// <param name="code">Código del producto</param>
        /// <param name="state">Nuevo estado del producto</param>
        /// <param name="audit">Información de auditoría</param>
        /// <returns>Resultado de la operación</returns>
        public async Task<ActionResult<Domain.Entities.InventoryProduct>> UpdateStateProductAsync(string code, bool state, AuditMessage audit)
        {
            try
            {
                // ✅ MEJORA 1: Validación de parámetros temprana
                if (string.IsNullOrWhiteSpace(code))
                {
                    return new ActionResult<Domain.Entities.InventoryProduct>
                    {
                        StatusCode = eStatusResult.WARNING,
                        StateResult = false,
                        Message = "El código del producto es requerido y no puede estar vacío"
                    };
                }

                if (audit == null)
                {
                    return new ActionResult<Domain.Entities.InventoryProduct>
                    {
                        StatusCode = eStatusResult.WARNING,
                        StateResult = false,
                        Message = "La información de auditoría es requerida"
                    };
                }

                if (string.IsNullOrWhiteSpace(audit.CodeUser))
                {
                    return new ActionResult<Domain.Entities.InventoryProduct>
                    {
                        StatusCode = eStatusResult.WARNING,
                        StateResult = false,
                        Message = "El código de usuario en la auditoría es requerido"
                    };
                }

                // ✅ MEJORA 2: Obtención asíncrona optimizada del producto (sin datos relacionados para mejor rendimiento)
                Domain.Entities.InventoryProduct product;
                try
                {
                    product = await _productRepository.GetInventoryProductLightAsync(code.Trim());
                }
                catch (Exception ex)
                {
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventoryProduct>
                    {
                        StatusCode = eStatusResult.EXCEPTION,
                        StateResult = false,
                        Message = $"Error al obtener el producto: {Utils.GetInnerExceptionMessageToString(ex)}"
                    };
                }

                // ✅ MEJORA 3: Validación de existencia del producto
                if (product == null || product.Id <= 0)
                {
                    return new ActionResult<Domain.Entities.InventoryProduct>
                    {
                        StatusCode = eStatusResult.WARNING,
                        StateResult = false,
                        Message = $"No se encontró un producto con el código '{code}'"
                    };
                }

                // ✅ MEJORA 4: Verificación de cambio de estado para evitar procesamiento innecesario
                if (product.Status == state)
                {
                    return new ActionResult<Domain.Entities.InventoryProduct>
                    {
                        StatusCode = eStatusResult.SUCCESS,
                        StateResult = true,
                        ObjectEmbbeded = product,
                        Message = $"El producto ya tiene el estado solicitado ({(state ? "activo" : "inactivo")})"
                    };
                }

                // ✅ MEJORA 5: Actualización del estado
                product.Status = state;
                
                // ✅ MEJORA 6: Usar SaveInventoryProductAsync optimizado
                var result = await SaveInventoryProductAsync(product, audit);
                
                if (result.StateResult)
                {
                    result.Message = $"Estado del producto actualizado a '{(state ? "activo" : "inactivo")}' correctamente";
                }

                return result;

            }
            catch (Exception ex)
            {
                // ✅ MEJORA 7: Manejo robusto de excepciones
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryProduct>
                {
                    StatusCode = eStatusResult.EXCEPTION,
                    StateResult = false,
                    Message = $"Error inesperado al actualizar el estado del producto: {Utils.GetInnerExceptionMessageToString(ex)}"
                };
            }
        }

        /// <summary>
        /// Elimina un tipo de producto
        /// </summary>
        /// <param name="InventoryProduct"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">product</exception>
        public ActionResult DeleteInventoryProduct(Domain.Entities.InventoryProduct InventoryProduct, AuditMessage audit)
        {
            if (InventoryProduct == null)
            {
                throw new ArgumentNullException("product");
            }

            IUnitWork unitOfWork = _productRepository.UnitWork;
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    Wrapper Event = new Wrapper();
                    DittoQueue ditto = Event.BuildEvent(InventoryProduct, audit, "deleted", DittoSourceType.product);
                    List<ProductHierarchy> _listHierarchy = _hierarchyRepository.ListProductHierarchyByHierarchyProductFinalId(InventoryProduct.Id);
                    foreach (ProductHierarchy ph in _listHierarchy)
                    {
                        _hierarchyRepository.DeleteEntity(ph);
                    }

                    //eliminamos el producto de crystal
                    var crystalProduct = _IHLISTPRORepository.GetIHLISTPROByCode(InventoryProduct.Code);
                    if (crystalProduct != null)
                    {
                        crystalProduct.MarkAsDeleted();
                        _IHLISTPRORepository.SaveEntity(crystalProduct);
                        _IHLISTPRORepository.UnitWork.Commit();
                    }

                    if (InventoryProduct.POSProduct == true)
                    {
                        while (InventoryProduct.POSPathologies.Count > 0)
                        {
                            _POSPathologiesRepository.DeleteEntity(InventoryProduct.POSPathologies[0]);
                            _POSPathologiesRepository.UnitWork.Commit();
                        }
                        //consulto las patologias que tiene el producto
                        var listPathologies = _INPRODPATRepository.GetINPRODPATByCodeProduct(InventoryProduct.Code);
                        foreach (var item in listPathologies)
                        {
                            item.MarkAsDeleted();
                            _INPRODPATRepository.SaveEntity(item);
                            _INPRODPATRepository.UnitWork.Commit();
                        }

                    }

                    if (InventoryProduct.InventoryProductAttribute != null && InventoryProduct.InventoryProductAttribute.Count() > 0)
                    {
                        while (InventoryProduct.InventoryProductAttribute.Count > 0)
                        {
                            InventoryProduct.InventoryProductAttribute[InventoryProduct.InventoryProductAttribute.Count - 1].MarkAsDeleted();
                        }
                    }

                    InventoryProduct.MarkAsDeleted();
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryProduct> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryProduct>(InventoryProduct, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                    _productRepository.SaveEntity(InventoryProduct);
                    unitOfWork.Commit();
                    auditProcess.Execute();
                    if (ditto.data != null) { Event.PublishEventObjectDeleted(ditto, Event.queueParameter.UrlQueue); }
                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };

                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    unitOfWork.RollbackChanges();
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    unitOfWork.RollbackChanges();
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-000" }, Message = ResourceManager.get_GetString("ErrorDependence") };
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    scope.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-000" }, Message = ResourceManager.get_GetString("ErrorDependence") };
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackChanges();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                }
            }
        }

       /// <summary>
       /// Valida el tipo medicamento segun el producto/medicamento
       /// </summary>
       /// <param name="InventoryProduct"></param>
       /// <returns></returns>
        public ActionResult ValidateMedicationTypeByProduct(Domain.Entities.InventoryProduct InventoryProduct)
        {
            if(InventoryProduct is null)
            {
                throw new  ArgumentNullException(nameof(InventoryProduct));
            }
            try
            {
                if(InventoryProduct.MedicationTypeId is null || InventoryProduct.ATCId is null)
                {
                    return new ActionResult { StateResult = false, Message = "No se ha seleccionado ningún tipo de medicamento" };
                }

                string medicationTypeCode = _medicationTypeRepository.FirstOrDefault(x => x.Id == InventoryProduct.MedicationTypeId, false)?.Code;

                bool uNIRS = _ATCRepository.FirstOrDefault(x => x.Id == InventoryProduct.ATCId, false).UNIRS;

                if(medicationTypeCode == MEDICATION_TYPE_1 && ( InventoryProduct.HandlesHealthRegistration is null || !InventoryProduct.HandlesHealthRegistration.Value || string.IsNullOrEmpty( InventoryProduct.HealthRegistration)))
                {
                    return new ActionResult { StateResult = true, Message = "El tipo de medicamento requiere un registro sanitario, pero actualmente el campo correspondiente se encuentra vacio o marcado en no." };
                }

                if (medicationTypeCode == MEDICATION_TYPE_4 && !uNIRS)
                {
                    return new ActionResult { StateResult = true, Message = "El tipo de medicamento requiere que el medicamento maneje la opción UNIRS" };
                }

                return new ActionResult { StateResult = true, Message = string.Empty };

            }
            catch(Exception ex)
            {
                return new ActionResult { StateResult = false,Message = Utils.GetInnerExceptionMessageToString(ex) };
            }
        }
        #endregion

        #region Events
        public void TriggerEvent(Domain.Entities.InventoryProduct product, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = product.ChangeTracker.State.ToString().ToLower();
            if (product.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(product, audit.CodeUser, ChangeTracker, DittoSourceType.product);
            IIndigoQueue queue = _factoryQueue.CreateQueue();
            queue.Publish(eventData);
            return;
        }


        #endregion

        #region "Report"

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISMEDVentas]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportSismedVentas(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            if (dateStart == null)
            {
                throw new ArgumentNullException("dateStart");
            }
            if (dateEnd == null)
            {
                throw new ArgumentNullException("dateEnd");
            }
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                string query1 = string.Empty;
                query1 = "exec [Inventory].[SP_ReportSISMEDVentas] '" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateStart) + "','" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateEnd) + "'";
                System.Data.DataTable dt1 = GetDatatable(query1, session, "ReportSISMEDVentas");
                ds.Tables.Add(dt1.Copy());
                return ds;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [Inventory].[SP_ReportSISDIS002]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportSISDIS002(DateTime dateStart, DateTime dateEnd, SessionValues session, List<int> ProductSupplieId = null)
        {
            String SupplieId = "";
            if (dateStart == null)
            {
                throw new ArgumentNullException("dateStart");
            }
            if (dateEnd == null)
            {
                throw new ArgumentNullException("dateEnd");
            }
            if (ProductSupplieId != null)
            {
                SupplieId = string.Join(",", ProductSupplieId);
            }
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                string query1 = string.Empty;
                query1 = "exec [Inventory].[SP_ReportSISDIS002] '" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateStart) + "','" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateEnd) + "','"+SupplieId+"'";
                System.Data.DataTable dt1 = GetDatatable(query1, session, "ReportSISDIS002");
                ds.Tables.Add(dt1.Copy());
                return ds;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISMEDCompras]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportSismedCompras(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            if (dateStart == null)
            {
                throw new ArgumentNullException("dateStart");
            }
            if (dateEnd == null)
            {
                throw new ArgumentNullException("dateEnd");
            }
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                string query1 = string.Empty;
                query1 = "exec [Inventory].[SP_ReportSISMEDCompras] '" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateStart) + "','" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateEnd) + "'";
                System.Data.DataTable dt1 = GetDatatable(query1, session, "ReportSISMEDCompras");
                ds.Tables.Add(dt1.Copy());
                return ds;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportSISDISCircular015]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public DataSet GetReportSISDISCircular015(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            if (dateStart == null)
            {
                throw new ArgumentNullException("dateStart");
            }
            if (dateEnd == null)
            {
                throw new ArgumentNullException("dateEnd");
            }
            try
            {
                DataSet ds = new DataSet();
                string query1 = string.Empty;
                query1 = "exec [Inventory].[SP_ReportSISDISCircular015] '" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateStart) + "','" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateEnd) + "'";
                DataTable dt1 = GetDatatable(query1, session, "ReportSISDISCircular015");
                ds.Tables.Add(dt1.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        /// <summary>
        /// Genera el archivo plano de sismed de ventas
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public ActionResult<System.Text.StringBuilder> GenerateFileSismedVentas(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            try
            {
                //Variable que se devuelve para generar el archivo plano
                ActionResult<System.Text.StringBuilder> result = new ActionResult<System.Text.StringBuilder>();
                System.Text.StringBuilder file = new System.Text.StringBuilder();
                //se obtiene el listado por medio de un dataset
                System.Data.DataSet data = GetReportSismedVentas(dateStart, dateEnd, session);
                System.Data.DataTable dtSismedVentas = new System.Data.DataTable();
                if (data != null)
                {
                    dtSismedVentas = data.Tables["ReportSISMEDVentas"];
                }
                if (dtSismedVentas.Rows.Count > 0)
                {
                    int cantProduct = dtSismedVentas.AsEnumerable().GroupBy(r => new { Col1 = r["CUM"] }).Count();
                    decimal valueTotalFile = Convert.ToDecimal(dtSismedVentas.Compute("Sum(TotalVentas)", ""));
                    Domain.Entities.ThirdParty thirdParty = _ThirdPartyRepository.GetThirdPartyByNit(session.IndigoCompanyNit);
                    if (thirdParty == null || thirdParty.Id == 0)
                    {
                        return new ActionResult<System.Text.StringBuilder>() { StateResult = false, Message = "La Compañia No se Encuentra Creada Como Tercero." };
                    }
                    //se arma la cabecera del archivo plano
                    string lineHead = Utils.StringPad("1", 1, "", Utils.PadType.STR_PAD_RIGHT).ToString();
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("1", 1, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("NI", 2, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(session.IndigoCompanyNit, maxSize(session.IndigoCompanyNit, 12), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(thirdParty.DigitVerification, 1, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(cantProduct.ToString().Replace(",", "."), maxSize(cantProduct.ToString(), 6), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += "";
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += "";
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += "";
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateStart.Year.ToString(), 4, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateStart.Month.ToString(), 2, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateEnd.Month.ToString(), 2, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dtSismedVentas.Rows.Count.ToString(), maxSize(dtSismedVentas.Rows.Count.ToString(), 6), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(valueTotalFile.ToString().Replace(",", "."), maxSize(valueTotalFile.ToString(), 16), " ", Utils.PadType.STR_PAD_LEFT);
                    file.Append(lineHead);

                    //Se recorre los rows del datarow para armar el archivo plano
                    foreach (DataRow item in dtSismedVentas.Rows)
                    {
                        string typeRegister = Convert.ToString(item["Tipo"]);//tipo de registro
                        string consecutive = Convert.ToString(item["Consecutivo"]);//consecutivo del registro
                        string month = Convert.ToString(item["Mes"]);//Mes de Reporte
                        string channel = Convert.ToString(item["canal"]);//canal
                        string codeCum = Convert.ToString(item["CUM"]);//Codigo Unico de Medicamento
                        string markets = Convert.ToString(item["Comercializa"]);// Se comercializa
                        string valueMin = Convert.ToString(item["ValorMinimo"]);//Precio unitano Minimo de venta
                        string valueMax = Convert.ToString(item["ValorMaximo"]);//Precio unitario Maximo de venta
                        string valueTotal = Convert.ToString(item["TotalVentas"]);//Valor total de ventas netas
                        string quantity = Convert.ToString(item["Cantidad"]);//Total unidades vendidas
                        string numberInvoiceMin = Convert.ToString(item["FacturaVentaMin"]);//Número de factura del precio mimo de venta del medicamento
                        string numberInvoiceMax = Convert.ToString(item["FacturaVentaMax"]);//Número de factura del precio maximo de venta del medicamento

                        string lineDet = System.Environment.NewLine;

                        lineDet += Utils.StringPad(typeRegister, 1, "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(consecutive, maxSize(consecutive, 6), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(month, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(channel, 3, "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(codeCum, maxSize(codeCum, 18), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(markets, 2, "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(valueMin.Replace(",", "."), maxSize(valueMin, 14), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(valueMax.Replace(",", "."), maxSize(valueMax, 14), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(valueTotal.Replace(",", "."), maxSize(valueTotal, 16), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(quantity.Replace(",", "."), maxSize(quantity, 16), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(numberInvoiceMin, maxSize(numberInvoiceMin, 20), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(numberInvoiceMax, maxSize(numberInvoiceMax, 20), " ", Utils.PadType.STR_PAD_LEFT);
                        file.Append(lineDet);
                    }
                }
                result.Message = "MED113MVEN" + dateEnd.ToString("yyyyMMdd") + "NI" + Utils.StringPad(session.IndigoCompanyNit, 12, "0", Utils.PadType.STR_PAD_LEFT);
                result.ObjectEmbbeded = file;
                result.StateResult = true;
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return new ActionResult<System.Text.StringBuilder>() { StateResult = false, Message = ex.Message.ToString() };
            }

        }

        /// <summary>
        /// Genera el archivo plano de sismed de compras
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public ActionResult<System.Text.StringBuilder> GenerateFileSismedCompras(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            try
            {
                //Variable que se devuelve para generar el archivo plano
                ActionResult<System.Text.StringBuilder> result = new ActionResult<System.Text.StringBuilder>();
                System.Text.StringBuilder file = new System.Text.StringBuilder();
                //se obtiene el listado por medio de un dataset
                System.Data.DataSet data = GetReportSismedCompras(dateStart, dateEnd, session);
                System.Data.DataTable dtSismedCompras = new System.Data.DataTable();
                if (data != null)
                {
                    dtSismedCompras = data.Tables["ReportSISMEDCompras"];
                }
                if (dtSismedCompras.Rows.Count > 0)
                {
                    decimal valueTotalFile = Convert.ToDecimal(dtSismedCompras.Compute("Sum(TotalVentas)", ""));
                    Domain.Entities.ThirdParty thirdParty = _ThirdPartyRepository.GetThirdPartyByNit(session.IndigoCompanyNit);
                    if (thirdParty == null || thirdParty.Id == 0)
                    {
                        return new ActionResult<System.Text.StringBuilder>() { StateResult = false, Message = "La Compañia No se Encuentra Creada Como Tercero." };
                    }
                    //se arma la cabecera del archivo plano
                    string lineHead = Utils.StringPad("1", 1, "", Utils.PadType.STR_PAD_RIGHT).ToString();
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("2", 1, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("NI", 2, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(session.IndigoCompanyNit, maxSize(session.IndigoCompanyNit, 12), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(thirdParty.DigitVerification, 1, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += "";
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateStart.Year.ToString(), 4, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateStart.Month.ToString(), 2, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateEnd.Month.ToString(), 2, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dtSismedCompras.Rows.Count.ToString(), maxSize(dtSismedCompras.Rows.Count.ToString(), 6), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(valueTotalFile.ToString().Replace(",", "."), maxSize(valueTotalFile.ToString(), 16), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += "0";
                    file.Append(lineHead);

                    //Se recorre los rows del datarow para armar el archivo plano
                    foreach (DataRow item in dtSismedCompras.Rows)
                    {
                        string typeRegister = Convert.ToString(item["Tipo"]);//tipo de registro
                        string consecutive = Convert.ToString(item["Consecutivo"]);//consecutivo del registro
                        string month = Convert.ToString(item["Mes"]);//Mes de Reporte
                        string codeCum = Convert.ToString(item["CUM"]);//Codigo Unico de Medicamento
                        string valueMin = Convert.ToString(item["ValorMinimo"]);//Precio unitano Minimo de venta
                        string valueMax = Convert.ToString(item["ValorMaximo"]);//Precio unitario Maximo de venta
                        string valueTotal = Convert.ToString(item["TotalVentas"]);//Valor total de ventas netas
                        string quantity = Convert.ToString(item["Cantidad"]);//Total unidades vendidas
                        string numberInvoiceMin = Convert.ToString(item["FacturaVentaMin"]);//Número de factura del precio mimo de venta del medicamento
                        string numberInvoiceMax = Convert.ToString(item["FacturaVentaMax"]);//Número de factura del precio maximo de venta del medicamento

                        string lineDet = System.Environment.NewLine;

                        lineDet += Utils.StringPad(typeRegister, 1, "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(consecutive, maxSize(consecutive, 6), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(month, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(codeCum, maxSize(codeCum, 18), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(valueMin.Replace(",", "."), maxSize(valueMin, 14), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(valueMax.Replace(",", "."), maxSize(valueMax, 14), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(valueTotal.Replace(",", "."), maxSize(valueTotal, 16), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(quantity.Replace(",", "."), maxSize(quantity, 16), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(numberInvoiceMin, maxSize(numberInvoiceMin, 20), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(",", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(numberInvoiceMax, maxSize(numberInvoiceMax, 20), " ", Utils.PadType.STR_PAD_LEFT);
                        file.Append(lineDet);
                    }
                }
                result.Message = "MED114MCOM" + dateEnd.ToString("yyyyMMdd") + "NI" + Utils.StringPad(session.IndigoCompanyNit, 12, "0", Utils.PadType.STR_PAD_LEFT);
                result.ObjectEmbbeded = file;
                result.StateResult = true;
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return new ActionResult<System.Text.StringBuilder>() { StateResult = false, Message = ex.Message.ToString() };
            }
        }

        /// <summary>
        /// Genera el archivo plano de sismed de compras y ventas res 006
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public ActionResult<System.Text.StringBuilder> GenerateFileSismedRes006(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            try
            {
                //Variable que se devuelve para generar el archivo plano
                ActionResult<System.Text.StringBuilder> result = new ActionResult<System.Text.StringBuilder>();
                System.Text.StringBuilder file = new System.Text.StringBuilder();
                //se obtiene el listado por medio de un dataset COMPRAS
                System.Data.DataSet data = GetReportSismedCompras(dateStart, dateEnd, session);
                System.Data.DataTable dtSismedCompras = new System.Data.DataTable();
                if (data != null)
                {
                    dtSismedCompras = data.Tables["ReportSISMEDCompras"];
                }

                //se obtiene el listado por medio de un dataset VENTAS
                System.Data.DataSet dataVentas = GetReportSismedVentas(dateStart, dateEnd, session);
                System.Data.DataTable dtSismedVentas = new System.Data.DataTable();
                if (dataVentas != null)
                {
                    dtSismedVentas = dataVentas.Tables["ReportSISMEDVentas"];
                }

                if (dtSismedCompras.Rows.Count == 0 && dtSismedVentas.Rows.Count == 0)
                {
                    result.Message = "No se encontraton datos para generar el reporte.";
                    result.ObjectEmbbeded = null;
                    result.StateResult = false;
                    return result;
                }

                //Encontramos el numero de medicamentos. 
                var Results = dtSismedCompras.AsEnumerable().Select(row => row.Field<String>("CUM"))
                    .Union(dtSismedVentas.AsEnumerable().Select(row => row.Field<String>("CUM")));
                int NumeroCUMUnitario = Results.Count();

                if (dtSismedCompras.Rows.Count > 0 || dtSismedVentas.Rows.Count > 0)
                {
                    Domain.Entities.ThirdParty thirdParty =
                        _ThirdPartyRepository.GetThirdPartyByNit(session.IndigoCompanyNit);
                    if (thirdParty == null || thirdParty.Id == 0)
                    {
                        return new ActionResult<System.Text.StringBuilder>()
                        { StateResult = false, Message = "La Compañia No se Encuentra Creada Como Tercero." };
                    }

                    //se arma la cabecera del archivo plano
                    string lineHead = Utils.StringPad("1", 1, "", Utils.PadType.STR_PAD_RIGHT).ToString();
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("NI", 2, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(session.IndigoCompanyNit, maxSize(session.IndigoCompanyNit, 12), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateStart.ToString("yyyy-MM-dd"), 10, " ", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateEnd.ToString("yyyy-MM-dd"), 10, " ", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad((dtSismedCompras.Rows.Count + dtSismedVentas.Rows.Count).ToString(), maxSize((dtSismedCompras.Rows.Count + dtSismedVentas.Rows.Count).ToString(), 6), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(NumeroCUMUnitario.ToString(), 6, " ", Utils.PadType.STR_PAD_RIGHT);
                    file.Append(lineHead);
                }

                string habilitationcode = "0";
                string TipoTrans = "05";
                //Logica para encontrar el codigo de habilitacion con el primer centro de atencion.
                if (session.HisContainer != null || session.HisContainer != "")
                {
                    ICrystalModelUnitOfWork crystalContext = new CrystalModelUnitOfWork(session.HisContainer);
                    ADCENATENRepository _ADCENATENRepository = new ADCENATENRepository(crystalContext);
                    ActionResult<ADCENATEN> CentroAtencion = _ADCENATENRepository.GetFirstADCENATEN();
                    if (CentroAtencion.StateResult == true)
                    { habilitationcode = CentroAtencion.ObjectEmbbeded.CODIPSSEC; TipoTrans = "05"; }
                    else { habilitationcode = "0"; TipoTrans = "04"; }
                }

                int consecutiveNum = 0;
                string RolActor = "2";
                string TipoOperacion = "0";

                if (dtSismedCompras.Rows.Count > 0)
                {
                    TipoOperacion = "CM";//Tipo operacion (Compras)
                                         //Se recorre los rows del datarow para armar el archivo plano

                    foreach (DataRow item in dtSismedCompras.Rows)
                    {
                        string typeRegister = Convert.ToString(item["Tipo"]);//tipo de registro
                        consecutiveNum = consecutiveNum + 1;
                        string consecutive = String.Format("{0:D1}", consecutiveNum);
                        string month = Convert.ToString(item["Mes"]);//Mes de Reporte

                        string codeCum = Convert.ToString(item["CUM"]);//Codigo Unico de Medicamento
                        string CodeCumCom1 = "";
                        string CodeCumCom2 = "";
                        if (codeCum.Contains("-"))
                        {
                            CodeCumCom1 = codeCum.Remove(codeCum.IndexOf("-"));
                            CodeCumCom2 = codeCum.Remove(0, codeCum.IndexOf("-") + 1);
                        }
                        else
                        {
                            CodeCumCom1 = codeCum;
                            CodeCumCom2 = "error";
                        }

                        int length = 0;
                        string ium = Convert.ToString(item["IUM"]);//Codigo Unico de Medicamento
                        string IUMFirst = "0";
                        string IUMSecond = "0";
                        string IUMThird = "0";
                        if (ium.Length > 0)
                        {
                            length = (ium.Length > 8 ? 8: ium.Length);
                            IUMFirst = ium.Substring(0, length);
                        }
                        if (ium.Length > 8)
                        {
                            length = (ium.Length > 12 ? 4 : ium.Length - 8);
                            IUMSecond = ium.Substring(8, length);
                        }
                        if (ium.Length > 12)
                        {
                            length = (ium.Length > 15 ? 3 : ium.Length - 12);
                            IUMThird = ium.Substring(12, length);
                        }

                        decimal valueMin = Convert.ToDecimal(item["ValorMinimo"]);
                        decimal valueMax = Convert.ToDecimal(item["ValorMaximo"]);
                        decimal valueTotal = Convert.ToDecimal(item["TotalVentas"]);//Valor total de ventas netas
                        string quantity = String.Format("{0:N0}", item["Cantidad"]);
                        string numberInvoiceMin = Convert.ToString(item["FacturaVentaMin"]);//Número de factura del precio mimo de venta del medicamento
                        string numberInvoiceMax = Convert.ToString(item["FacturaVentaMax"]);//Número de factura del precio maximo de venta del medicamento

                        string InvoiceMin = Regex.Replace(numberInvoiceMin.Trim(), @"[^\w]+", "");
                        string InvoiceMax = Regex.Replace(numberInvoiceMax.Trim(), @"[^\w]+", "");

                        string lineDet = System.Environment.NewLine;

                        lineDet += Utils.StringPad(typeRegister, 1, "", Utils.PadType.STR_PAD_RIGHT); //col0
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(consecutive, maxSize(consecutive, 6), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(habilitationcode, maxSize(habilitationcode, 10), "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(month, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(RolActor, 1, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TipoOperacion, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TipoTrans, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IUMFirst, maxSize(IUMFirst, 8), "0", Utils.PadType.STR_PAD_LEFT); // Valor en 0 (Col7)
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IUMSecond, maxSize(IUMSecond, 4), "0", Utils.PadType.STR_PAD_LEFT); // Valor en 0 (col8)
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IUMThird, maxSize(IUMThird, 3), "0", Utils.PadType.STR_PAD_LEFT); // Valor en 0 (col9)
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(CodeCumCom1, maxSize(CodeCumCom1, 8), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(CodeCumCom2, maxSize(CodeCumCom2, 3), " ", Utils.PadType.STR_PAD_LEFT); //debe ser igual al parametrizado en el producto (no debe rellenar)
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("C", 1, "0", Utils.PadType.STR_PAD_LEFT); // Valor en C (col12) unidad en que se factura.
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(decimal.Round(valueMin, 2).ToString().Replace(",", "."), maxSize(decimal.Round(valueMin, 2).ToString(), 14), " ", Utils.PadType.STR_PAD_LEFT); //ValueMin
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(decimal.Round(valueMax, 2).ToString().Replace(",", "."), maxSize(decimal.Round(valueMax, 2).ToString(), 14), " ", Utils.PadType.STR_PAD_LEFT); //ValueMax
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(decimal.Round(valueTotal, 2).ToString().Replace(",", "."), maxSize(decimal.Round(valueTotal, 2).ToString(), 14), " ", Utils.PadType.STR_PAD_LEFT); //ValueMax
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(quantity.Replace(".", "").TrimStart(), maxSize(quantity.Replace(".", ""), 16), " ", Utils.PadType.STR_PAD_LEFT); //Quantity
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(InvoiceMin, maxSize(InvoiceMin, 20), " ", Utils.PadType.STR_PAD_LEFT); // MAxNumberInvoice
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(InvoiceMax, maxSize(InvoiceMax, 20), " ", Utils.PadType.STR_PAD_LEFT); // MAxNumberInvoice
                        file.Append(lineDet);
                    }
                }

                if (dtSismedVentas.Rows.Count > 0)
                {
                    int cantProduct = dtSismedVentas.AsEnumerable().GroupBy(r => new { Col1 = r["CUM"] }).Count();
                    decimal valueTotalFile = Convert.ToDecimal(dtSismedVentas.Compute("Sum(TotalVentas)", ""));
                    Domain.Entities.ThirdParty thirdParty = _ThirdPartyRepository.GetThirdPartyByNit(session.IndigoCompanyNit);
                    if (thirdParty == null || thirdParty.Id == 0)
                    {
                        return new ActionResult<System.Text.StringBuilder>() { StateResult = false, Message = "La Compañia No se Encuentra Creada Como Tercero." };
                    }

                    TipoOperacion = "VN";//Tipo operacion (Compras)
                    //Se recorre los rows del datarow para armar el archivo plano
                    foreach (DataRow item in dtSismedVentas.Rows)
                    {
                        string typeRegister = Convert.ToString(item["Tipo"]);//tipo de registro
                        consecutiveNum = consecutiveNum + 1;
                        string consecutive = String.Format("{0:D1}", consecutiveNum);
                        string month = Convert.ToString(item["Mes"]);//Mes de Reporte
                        string codeCum = Convert.ToString(item["CUM"]);//Codigo Unico de Medicamento
                        string CodeCumCom1 = "";
                        string CodeCumCom2 = "";
                        if (codeCum.Contains("-"))
                        {
                            CodeCumCom1 = codeCum.Remove(codeCum.IndexOf("-"));
                            CodeCumCom2 = codeCum.Remove(0, codeCum.IndexOf("-") + 1);
                        }
                        else
                        {
                            CodeCumCom1 = codeCum;
                            CodeCumCom2 = "error";
                        }

                        int length = 0;
                        string ium = Convert.ToString(item["IUM"]);//Codigo Unico de Medicamento
                        string IUMFirst = "0";
                        string IUMSecond = "0";
                        string IUMThird = "0";
                        if (ium.Length > 0)
                        {
                            length = (ium.Length > 8 ? 8 : ium.Length);
                            IUMFirst = ium.Substring(0, length);
                        }
                        if (ium.Length > 8)
                        {
                            length = (ium.Length > 12 ? 4 : ium.Length - 8);
                            IUMSecond = ium.Substring(8, length);
                        }
                        if (ium.Length > 12)
                        {
                            length = (ium.Length > 15 ? 3 : ium.Length - 12);
                            IUMThird = ium.Substring(12, length);
                        }

                        string markets = Convert.ToString(item["Comercializa"]);// Se comercializa
                        decimal valueMin = Convert.ToDecimal(item["ValorMinimo"]);
                        decimal valueMax = Convert.ToDecimal(item["ValorMaximo"]);
                        decimal valueTotal = Convert.ToDecimal(item["TotalVentas"]);
                        string quantity = String.Format("{0:N0}", item["Cantidad"]);
                        string numberInvoiceMin = Convert.ToString(item["FacturaVentaMin"]);//Número de factura del precio mimo de venta del medicamento
                        string numberInvoiceMax = Convert.ToString(item["FacturaVentaMax"]);//Número de factura del precio maximo de venta del medicamento

                        string InvoiceMin = Regex.Replace(numberInvoiceMin.Trim(), @"[^\w]+", "");
                        string InvoiceMax = Regex.Replace(numberInvoiceMax.Trim(), @"[^\w]+", "");

                        string lineDet = System.Environment.NewLine;

                        lineDet += Utils.StringPad(typeRegister, 1, "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(consecutive, maxSize(consecutive, 6), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(habilitationcode, maxSize(habilitationcode, 10), "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(month, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(RolActor, 1, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TipoOperacion, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TipoTrans, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IUMFirst, maxSize(IUMFirst, 8), "0", Utils.PadType.STR_PAD_LEFT); // Valor en 0 (Col7)
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IUMSecond, maxSize(IUMSecond, 4), "0", Utils.PadType.STR_PAD_LEFT); // Valor en 0 (col8)
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IUMThird, maxSize(IUMThird, 3), "0", Utils.PadType.STR_PAD_LEFT); // Valor en 0 (col9)
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(CodeCumCom1, maxSize(CodeCumCom1, 8), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(CodeCumCom2, maxSize(CodeCumCom2, 3), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("C", 1, "0", Utils.PadType.STR_PAD_LEFT); // Valor en C (col12) unidad en que se factura.
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(decimal.Round(valueMin, 2).ToString().Replace(",", "."), maxSize(decimal.Round(valueMin, 2).ToString(), 14), " ", Utils.PadType.STR_PAD_LEFT); //ValueMin
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(decimal.Round(valueMax, 2).ToString().Replace(",", "."), maxSize(decimal.Round(valueMax, 2).ToString(), 14), " ", Utils.PadType.STR_PAD_LEFT); //ValueMax
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(decimal.Round(valueTotal, 2).ToString().Replace(",", "."), maxSize(decimal.Round(valueTotal, 2).ToString(), 14), " ", Utils.PadType.STR_PAD_LEFT); //ValueMax
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(quantity.Replace(".", "").TrimStart(), maxSize(quantity.Replace(".", ""), 16), " ", Utils.PadType.STR_PAD_LEFT); //Quantity
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(InvoiceMin, maxSize(InvoiceMin, 20), " ", Utils.PadType.STR_PAD_LEFT); // MAxNumberInvoice
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(InvoiceMax, maxSize(InvoiceMax, 20), " ", Utils.PadType.STR_PAD_LEFT); // MAxNumberInvoice
                        file.Append(lineDet);
                    }
                }
                //Nombre del archivo. 
                result.Message = "MED100MPRE" + dateEnd.ToString("yyyyMMdd") + "NI" + Utils.StringPad(session.IndigoCompanyNit, 12, "0", Utils.PadType.STR_PAD_LEFT);
                result.ObjectEmbbeded = file;
                result.StateResult = true;
                return result;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return new ActionResult<System.Text.StringBuilder>() { StateResult = false, Message = ex.Message.ToString() };
            }
        }

        /// <summary>
        /// Genera el archivo plano de SISDIS002
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public ActionResult<System.Text.StringBuilder> GenerateSISDIS002(DateTime dateStart, DateTime dateEnd, SessionValues session, List<int> ProductSupplieId = null)
        {
            try
            {
                //Variable que se devuelve para generar el archivo plano
                ActionResult<System.Text.StringBuilder> result = new ActionResult<System.Text.StringBuilder>();
                System.Text.StringBuilder file = new System.Text.StringBuilder();
                //se obtiene el listado por medio de un dataset
                System.Data.DataSet data = GetReportSISDIS002(dateStart, dateEnd, session, ProductSupplieId);
                System.Data.DataTable dtSISDIS002 = new System.Data.DataTable();
                if (data != null)
                {
                    dtSISDIS002 = data.Tables["ReportSISDIS002"];
                }
                if (dtSISDIS002.Rows.Count > 0)
                {
                    Domain.Entities.ThirdParty thirdParty = _ThirdPartyRepository.GetThirdPartyByNit(session.IndigoCompanyNit);
                    if (thirdParty == null || thirdParty.Id == 0)
                    {
                        return new ActionResult<System.Text.StringBuilder>() { StateResult = false, Message = "La Compañia No se Encuentra Creada Como Tercero." };
                    }
                    //se arma la cabecera del archivo plano
                    string lineHead = Utils.StringPad("1", 1, "", Utils.PadType.STR_PAD_RIGHT).ToString();
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("NI", 2, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(session.IndigoCompanyNit, maxSize(session.IndigoCompanyNit, 12), " ", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateStart.Year.ToString(), 4, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad("-", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateStart.Month.ToString(), 2, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("-", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateStart.Day.ToString(), 2, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateEnd.Year.ToString(), 4, "", Utils.PadType.STR_PAD_RIGHT);
                    lineHead += Utils.StringPad("-", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateEnd.Month.ToString(), 2, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("-", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dateEnd.Day.ToString(), 2, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                    lineHead += Utils.StringPad(dtSISDIS002.Rows.Count.ToString(), maxSize(dtSISDIS002.Rows.Count.ToString(), 10), " ", Utils.PadType.STR_PAD_LEFT);
                    file.Append(lineHead);

                    //Se recorre los rows del datarow para armar el archivo plano
                    Parallel.ForEach(dtSISDIS002.AsEnumerable(), fila =>
                    {
                        string TypeRegister = Convert.ToString(fila["TypeRegister"]);//tipo de registro
                        string consecutive = Convert.ToString(fila["Consecutivo"]);//consecutivo del registro
                        string MonthReport = Convert.ToString(fila["MonthReport"]);//Mes de Reporte
                        string Channel = Convert.ToString(fila["Channel"]);//canal
                        string IDM = Convert.ToString(fila["IDM"]);//Identificación del dispositivo médico
                        string UnitPriceMinimunSale = Convert.ToString(fila["UnitPriceMinimunSale"]);// Precio unitario mínimo de venta
                        string UnitPriceMaximumSale = Convert.ToString(fila["UnitPriceMaximumSale"]);//Precio unitario Maximo de venta
                        string TotalSales = Convert.ToString(fila["TotalSales"]);//Valor total de ventas netas
                        string TotalUnitsSold = Convert.ToString(fila["TotalUnitsSold"]);//Total de unidades vendidas
                        string MinInvoice = Convert.ToString(fila["MinInvoice"]);//Número de factura del precio mínimo de venta del dispositivo
                        string TypeEntityMinInvoice = Convert.ToString(fila["TypeEntityMinInvoice"]);//Tipo Identificación de la entidad compradora en la factura del precio mínimo de venta
                        string IdNumberMinInvoice = Convert.ToString(fila["IdNumberMinInvoice"]);//Número de identificación de la entidad compradora en la factura del precio mínimo de venta
                        string TotalUnitsRegisterMin = Convert.ToString(fila["TotalUnitsRegisterMin"]);//Total de unidades registradas del dispositivo en la factura del precio mínimo de venta 
                        string MaxInovice = Convert.ToString(fila["MaxInovice"]);//Número de factura del precio maximo de venta del dispositivo
                        string TypeEntityMaxInvoice = Convert.ToString(fila["TypeEntityMaxInvoice"]);//Tipo Identificación de la entidad compradora en la factura del precio maximo de venta
                        string IdNumberMaxInvoice = Convert.ToString(fila["IdNumberMaxInvoice"]);//Número de identificación de la entidad compradora en la factura del precio maximo de venta
                        string TotalUnitsRegisterMax = Convert.ToString(fila["TotalUnitsRegisterMax"]);//Total de unidades registradas del dispositivo en la factura del precio maximo de venta 

                        string lineDet = System.Environment.NewLine;

                        lineDet += Utils.StringPad(TypeRegister, 1, "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(consecutive, maxSize(consecutive, 10), "", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(MonthReport, 2, "0", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(Channel, 3, "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IDM, maxSize(IDM, 19), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(UnitPriceMinimunSale.Replace(",", "."), maxSize(UnitPriceMinimunSale, 14), "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(UnitPriceMaximumSale.Replace(",", "."), maxSize(UnitPriceMaximumSale, 14), "", Utils.PadType.STR_PAD_RIGHT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TotalSales.Replace(",", "."), maxSize(TotalSales, 16), "", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TotalUnitsSold, maxSize(TotalUnitsSold, 16), "", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(MinInvoice, maxSize(MinInvoice, 30), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TypeEntityMinInvoice, 2, "", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IdNumberMinInvoice, maxSize(IdNumberMinInvoice, 12), "", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TotalUnitsRegisterMin, maxSize(TotalUnitsRegisterMin, 10), "", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(MaxInovice, maxSize(MaxInovice, 30), " ", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TypeEntityMaxInvoice, 2, "", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(IdNumberMaxInvoice, maxSize(IdNumberMaxInvoice, 12), "", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad("|", 1, "0", Utils.PadType.STR_PAD_LEFT);
                        lineDet += Utils.StringPad(TotalUnitsRegisterMax, maxSize(TotalUnitsRegisterMax, 10), "", Utils.PadType.STR_PAD_LEFT);

                        file.Append(lineDet);
                    });
                }
                result.Message = "DIS113DVEN" + dateEnd.ToString("yyyyMMdd") + "NI" + Utils.StringPad(session.IndigoCompanyNit, 12, "0", Utils.PadType.STR_PAD_LEFT);
                result.ObjectEmbbeded = file;
                result.StateResult = true;
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return new ActionResult<System.Text.StringBuilder>() { StateResult = false, Message = ex.Message.ToString() };
            }

        }

        /// <summary>
        /// Genera el archivo plano de SISDIS015
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public ActionResult<StringBuilder> GenerateSISDIS015(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            const string SEP = "|";

            // Función que NO aplica padding - retorna el valor tal como está
            string NoPad(string value)
            {
                return string.IsNullOrEmpty(value) ? string.Empty : value.Trim();
            }

            string FormatDate(DateTime d) => d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            string NormalizeDecimal(object val)
            {
                if (val == null || val == DBNull.Value) return string.Empty;
                // Sin separadores de miles, con punto decimal invariable
                if (val is IFormattable f) return f.ToString(null, CultureInfo.InvariantCulture);
                return Convert.ToString(val, CultureInfo.InvariantCulture)
                       ?.Replace(",", "."); // por si viene como string con coma
            }

            string NormalizeIntegerLike(object val)
            {
                if (val == null || val == DBNull.Value) return string.Empty;
                // Cantidad sin separadores (equivalente a N0 pero invariable)
                if (decimal.TryParse(Convert.ToString(val, CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out var dec))
                    return Math.Truncate(dec).ToString("0", CultureInfo.InvariantCulture);
                return Convert.ToString(val, CultureInfo.InvariantCulture);
            }

            // Función para limpiar valores de texto del DataTable
            string CleanValue(object val)
            {
                if (val == null || val == DBNull.Value) return string.Empty;
                var str = Convert.ToString(val)?.Trim();
                return string.IsNullOrEmpty(str) ? string.Empty : str;
            }

            try
            {
                var result = new ActionResult<StringBuilder>();
                var file = new StringBuilder();

                DataSet data = GetReportSISDISCircular015(dateStart, dateEnd, session);
                DataTable dt = data?.Tables["ReportSISDISCircular015"];

                if (dt == null || dt.Rows.Count == 0)
                {
                    result.Message = "No se encontraron registros para generar el archivo";
                    result.StateResult = false;
                    return result;
                }

                // Validación de tercero
                ThirdParty thirdParty = _ThirdPartyRepository.GetThirdPartyByNit(session.IndigoCompanyNit);
                if (thirdParty == null || thirdParty.Id == 0)
                {
                    return new ActionResult<StringBuilder>
                    {
                        StateResult = false,
                        Message = "La Compañia No se Encuentra Creada Como Tercero."
                    };
                }

                // ---------- Cabecera (tipo 1) ----------
                var headFields = new[]
                {
                    NoPad("1"),
                    NoPad("NI"),
                    NoPad(session.IndigoCompanyNit),
                    NoPad(FormatDate(dateStart)),
                    NoPad(FormatDate(dateEnd)),
                    NoPad(dt.Rows.Count.ToString())
                };

                file.Append(string.Join(SEP, headFields));

                // ---------- Detalle (tipo 2) ----------
                foreach (DataRow item in dt.Rows)
                {
                    string Consecutive = CleanValue(item["Consecutive"]);
                    string Qualification = CleanValue(item["Qualification"]);
                    string Month = CleanValue(item["Month"]);
                    string Channel = CleanValue(item["Channel"]);
                    string Role = CleanValue(item["Role"]);
                    string TypeOperation = CleanValue(item["TypeOperation"]);
                    string TransactionType = CleanValue(item["TransactionType"]);
                    string IUM = CleanValue(item["IUM"]);
                    string UnitType = CleanValue(item["UnitType"]);

                    string Quantity = NormalizeIntegerLike(item["Quantity"]);
                    string TotalSales = NormalizeDecimal(item["TotalSales"]);
                    string MinimumValue = NormalizeDecimal(item["MinimumValue"]);
                    string MaximumValue = NormalizeDecimal(item["MaximumValue"]);

                    string TypePersonMin = CleanValue(item["TypePersonMin"]);
                    string NitMin = CleanValue(item["NitMin"]);
                    string InvoiceSaleMin = CleanValue(item["InvoiceSaleMin"]);

                    string TypePersonMax = CleanValue(item["TypePersonMax"]);
                    string NitMax = CleanValue(item["NitMax"]);
                    string InvoiceSaleMax = CleanValue(item["InvoiceSaleMax"]);

                    var detFields = new[]
                    {
                        NoPad("2"), /*0- Tipo de Registro*/
                        NoPad(Consecutive), /*1- Consecutivo de registro*/
                        NoPad(Qualification),/*2- Código de habilitación*/
                        NoPad(Month), /*3- Mes de la información a reportar*/
                        NoPad(Channel),/*4- Canal */
                        NoPad(Role),/*5- Rol del actor que reporta frente a la operación de reporte del dispositivo médico*/
                        NoPad(TypeOperation),/*6- Tipo de operación*/
                        NoPad(TransactionType),/*7- Tipo de transacción*/
                        NoPad(IUM),/*8- Identificador del dispositivo médico*/
                        NoPad(UnitType),/*9- Unidad en la que se factura el dispositivo médico en la operación reportada*/

                        // Numéricos
                        NoPad(Quantity),/*10- Total de unidades de reporte en el mes para la operación y la transacción*/
                        NoPad(TotalSales),/*11- Valor del total facturado del dispositivo médico durante el mes para la operación y transacción del reporte*/
                        NoPad(MinimumValue),/*12- Precio unitario mínimo del dispositivo médico durante el mes para la operación y la transacción del reporte*/
                        NoPad(MaximumValue),/*13- Precio unitario máximo del dispositivo médico durante el mes para la operación y transacción del reporte*/

                        // Mínimos
                        NoPad(TypePersonMin),/*14- Tipo de identificación del actor con quien se realizó la operación y la transacción de reporte, al precio mínimo*/
                        NoPad(NitMin),/*15- Número de identificación del actor con quien se realizó la operación y la transacción de reporte, al precio mínimo*/
                        NoPad(InvoiceSaleMin),/*16- Número de factura o documento equivalente del precio mínimo unitario de  la operación y transacción del reporte*/

                        // Máximos
                        NoPad(TypePersonMax),/*17- Tipo de identificación del actor con quien se realizó la operación y la transacción de reporte, al precio máximo*/
                        NoPad(NitMax),/*18- Número de identificación del actor con quien se realizó la operación y transacción de reporte, al precio máximo. */
                        NoPad(InvoiceSaleMax)/*19- Número de factura o documento equivalente del precio máximo unitario de la operación y transacción del reporte*/
                    };

                    file.AppendLine();
                    file.Append(string.Join(SEP, detFields));
                }

                result.Message = "DIS123DVEN" + dateEnd.ToString("yyyyMMdd") + "NI" +
                                 Utils.StringPad(session.IndigoCompanyNit, 12, "0", Utils.PadType.STR_PAD_LEFT);

                result.ObjectEmbbeded = file;
                result.StateResult = true;
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return new ActionResult<StringBuilder>
                {
                    StateResult = false,
                    Message = Utils.GetInnerExceptionMessageToString(ex)
                };
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportDocumentsDisorganizedInventoryVsAccounting]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>        
        public System.Data.DataSet GetReportDocumentsDisorganizedInventoryVsAccounting(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            if (dateStart == null)
            {
                throw new ArgumentNullException("dateStart");
            }
            if (dateEnd == null)
            {
                throw new ArgumentNullException("dateEnd");
            }
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                string query1 = "exec [Inventory].[SP_ReportDocumentsDisorganizedInventoryVsAccounting] '" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateStart) + "','" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateEnd) + "'";
                System.Data.DataTable dt1 = GetDatatable(query1, session, "ReportDocumentsDisorganizedInventoryVsAccounting");
                ds.Tables.Add(dt1.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportAccountingSummaryA]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportReportAccountingSummaryA(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            if (dateStart == null)
            {
                throw new ArgumentNullException("dateStart");
            }
            if (dateEnd == null)
            {
                throw new ArgumentNullException("dateEnd");
            }
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                string query1 = string.Empty;
                query1 = "exec [Inventory].[SP_ReportAccountingSummaryA] '" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateStart) + "','" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateEnd) + "'";
                System.Data.DataTable dt1 = GetDatatable(query1, session, "ReportAccountingSummaryA");
                ds.Tables.Add(dt1.Copy());
                return ds;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [SP_ReportAccountingSummaryB]
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="session"></param>
        /// <returns></returns>        
        public System.Data.DataSet GetReportReportAccountingSummaryB(DateTime dateStart, DateTime dateEnd, SessionValues session)
        {
            if (dateStart == null)
            {
                throw new ArgumentNullException("dateStart");
            }
            if (dateEnd == null)
            {
                throw new ArgumentNullException("dateEnd");
            }
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                string query1 = string.Empty;
                query1 = "exec [Inventory].[SP_ReportAccountingSummaryB] '" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateStart) + "','" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dateEnd) + "'";
                System.Data.DataTable dt1 = GetDatatable(query1, session, "ReportAccountingSummaryB");
                ds.Tables.Add(dt1.Copy());
                return ds;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        /// <summary>
        /// Metodo que realiza el llamado al storeProcedure [Inventory.SP_ReportDeterioration]
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public System.Data.DataSet GetReportDeterioration(int Year, int Month, SessionValues session)
        {
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                string query1 = string.Empty;
                query1 = "exec [Inventory].[SP_ReportDeterioration] " + Year + ", " + Month;
                System.Data.DataTable dt1 = GetDatatable(query1, session, "ReportDeterioration");
                ds.Tables.Add(dt1.Copy());
                return ds;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
        }

        #endregion

        #region "Private Methods"

        public string ConvertProductToXml(Domain.Entities.InventoryProduct product)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<InventoryProduct>");

            builder.Append("<Id>" + product.Id + "</Id>");
            builder.Append("<Code>" + product.Code + "</Code>");
            builder.Append("<Name>" + product.Name.ConvertToXmlText() + "</Name>");
            builder.Append("<ProductTypeId>" + product.ProductTypeId + "</ProductTypeId>");
            builder.Append("<ATCId>" + product.ATCId + "</ATCId>");
            builder.Append("<CodeCUM>" + product.CodeCUM + "</CodeCUM>");
            builder.Append("<CodeAlternative>" + product.CodeAlternative + "</CodeAlternative>");
            builder.Append("<CodeAlternativeTwo>" + product.CodeAlternativeTwo + "</CodeAlternativeTwo>");
            builder.Append("<Description>" + product.Description.ConvertToXmlText() + "</Description>");
            builder.Append("<ProductGroupId>" + product.ProductGroupId + "</ProductGroupId>");
            builder.Append("<ProductSubGroupId>" + product.ProductSubGroupId + "</ProductSubGroupId>");
            builder.Append("<MeasurementUnitId>" + product.MeasurementUnitId + "</MeasurementUnitId>");
            builder.Append("<PackagingUnitId>" + product.PackagingUnitId + "</PackagingUnitId>");
            builder.Append("<ManufacturerId>" + product.ManufacturerId + "</ManufacturerId>");
            builder.Append("<Osmolarity>" + product.Osmolarity + "</Osmolarity>");
            builder.Append("<IVAId>" + product.IVAId + "</IVAId>");
            builder.Append("<Presentation>" + product.Presentation + "</Presentation>");
            builder.Append("<CodeSICE>" + product.CodeSICE + "</CodeSICE>");
            builder.Append("<HandlesSerial>" + product.HandlesSerial + "</HandlesSerial>");
            builder.Append("<HandlesHealthRegistration>" + product.HandlesHealthRegistration + "</HandlesHealthRegistration>");
            builder.Append("<HealthRegistration>" + product.HealthRegistration + "</HealthRegistration>");
            builder.Append("<ExpirationDate>" + (product.ExpirationDate != null ? product.ExpirationDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "") + "</ExpirationDate>");
            builder.Append("<BillingGroupId>" + product.BillingGroupId + "</BillingGroupId>");
            builder.Append("<ProductControl>" + product.ProductControl + "</ProductControl>");
            builder.Append("<ProductWithPriceControl>" + product.ProductWithPriceControl + "</ProductWithPriceControl>");
            builder.Append("<POSProduct>" + product.POSProduct + "</POSProduct>");
            builder.Append("<AuthorizationByOrderNumber>" + product.AuthorizationByOrderNumber + "</AuthorizationByOrderNumber>");
            builder.Append("<ExpirationDay>" + product.ExpirationDay + "</ExpirationDay>");
            builder.Append("<MaximumControlPeriod>" + product.MaximumControlPeriod + "</MaximumControlPeriod>");
            builder.Append("<ControlDays>" + product.ControlDays + "</ControlDays>");
            builder.Append("<ControlOrderQuantity>" + product.ControlOrderQuantity + "</ControlOrderQuantity>");
            builder.Append("<ProductOrderAmount>" + product.ProductOrderAmount + "</ProductOrderAmount>");
            builder.Append("<LastPurchase>" + (product.LastPurchase != null ? product.LastPurchase.Value.ToString("dd/MM/yyyy HH:mm:ss") : "") + "</LastPurchase>");
            builder.Append("<LastSale>" + (product.LastSale != null ? product.LastSale.Value.ToString("dd/MM/yyyy HH:mm:ss") : "") + "</LastSale>");
            builder.Append("<ProductOrigin>" + product.ProductOrigin + "</ProductOrigin>");
            builder.Append("<MinimumStock>" + product.MinimumStock + "</MinimumStock>");
            builder.Append("<MaximumStock>" + product.MaximumStock + "</MaximumStock>");
            builder.Append("<CommissionPercentage>" + product.CommissionPercentage + "</CommissionPercentage>");
            builder.Append("<RepositionPoint>" + product.RepositionPoint + "</RepositionPoint>");
            builder.Append("<ResetTime>" + product.ResetTime + "</ResetTime>");
            builder.Append("<CurrencyType>" + product.CurrencyType + "</CurrencyType>");
            builder.Append("<ProductCost>" + product.ProductCost + "</ProductCost>");
            builder.Append("<FinalProductCost>" + product.FinalProductCost + "</FinalProductCost>");
            builder.Append("<SellingPrice>" + product.SellingPrice + "</SellingPrice>");
            builder.Append("<AllPOSPathologies>" + product.AllPOSPathologies + "</AllPOSPathologies>");
            builder.Append("<Status>" + product.Status + "</Status>");
            builder.Append("<BillingGroupNoPosId>" + product.BillingGroupNoPosId + "</BillingGroupNoPosId>");
            builder.Append("<ControlCostPercentage>" + product.ControlCostPercentage + "</ControlCostPercentage>");
            builder.Append("<InventoryRiskLevelId>" + product.InventoryRiskLevelId + "</InventoryRiskLevelId>");
            builder.Append("<SerialNumber>" + product.SerialNumber + "</SerialNumber>");
            builder.Append("<DriveUnit>" + product.DriveUnit + "</DriveUnit>");
            builder.Append("<MinimumTemperature>" + product.MinimumTemperature + "</MinimumTemperature>");
            builder.Append("<MaximumTemperature>" + product.MaximumTemperature + "</MaximumTemperature>");
            builder.Append("<SanitaryRegistration>" + product.SanitaryRegistration + "</SanitaryRegistration>");
            builder.Append("<Consumption>" + product.Consumption + "</Consumption>");
            builder.Append("<JustificationSuppliesDispositives>" + product.JustificationSuppliesDispositives + "</JustificationSuppliesDispositives>");
            builder.Append("<OsteosynthesisMaterial>" + product.OsteosynthesisMaterial + "</OsteosynthesisMaterial>");
            builder.Append("<Abbreviation>" + product.Abbreviation.ConvertToXmlText() + "</Abbreviation>");
            builder.Append("<SupplieId>" + product.SupplieId + "</SupplieId>");
            builder.Append("<IUM>" + product.IUM + "</IUM>");
            builder.Append("<Storage>" + product.Storage + "</Storage>");
            builder.Append("<TaxedProduct>" + product.TaxedProduct + "</TaxedProduct>");
            builder.Append("<LiquidateSalesTaxes>" + product.LiquidateSalesTaxes + "</LiquidateSalesTaxes>");
            builder.Append("<SismedReport>" + product.SismedReport + "</SismedReport>");
			builder.Append("<DairyComponent>" + product.DairyComponent + "</DairyComponent>");
			builder.Append("<DairyComponentType>" + product.DairyComponentType + "</DairyComponentType>");
            builder.Append("<MedicationTypeId>" + product.MedicationTypeId + "</MedicationTypeId>");
            builder.Append("<WeightParenteralNutritionSupply>" + product.WeightParenteralNutritionSupply + "</WeightParenteralNutritionSupply>");

            if (product.InventoryProductAttribute != null && product.InventoryProductAttribute.Count > 0)
            {
                foreach (var item in product.InventoryProductAttribute)
                {
                    builder.Append("<InventoryProductAttribute>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("<AttributeProductTypeId>" + item.AttributeProductTypeId + "</AttributeProductTypeId>");
                    builder.Append("<Value>" + item.Value + "</Value>");
                    builder.Append("<IsDelete>" + (item.ChangeTracker.State == ObjectState.Deleted ? 1 : 0) + "</IsDelete>");
                    builder.Append("</InventoryProductAttribute>");
                }
            }

            if (product.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("InventoryProductAttribute"))
            {
                var listDelete = (from InventoryProductAttribute e in product.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "InventoryProductAttribute").ToList()[0].Value select e).ToList();

                foreach (var item in listDelete)
                {
                    builder.Append("<InventoryProductAttribute>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("<AttributeProductTypeId>" + item.AttributeProductTypeId + "</AttributeProductTypeId>");
                    builder.Append("<Value>" + item.Value + "</Value>");
                    builder.Append("<IsDelete>" + 1 + "</IsDelete>");
                    builder.Append("</InventoryProductAttribute>");
                }
            }

            if (product.ProductBarcode != null && product.ProductBarcode.Count > 0)
            {
                foreach (var item in product.ProductBarcode)
                {
                    if (item.ChangeTracker.State == ObjectState.Added || item.ChangeTracker.State == ObjectState.Deleted)
                    {
                        builder.Append("<ProductBarcode>");
                        builder.Append("<Id>" + item.Id + "</Id>");
                        builder.Append("<Barcode>" + item.Barcode + "</Barcode>");
                        builder.Append("<IsDelete>" + (item.ChangeTracker.State == ObjectState.Deleted ? 1 : 0) + "</IsDelete>");
                        builder.Append("</ProductBarcode>");
                    }
                }
            }

            if (product.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ProductBarcode"))
            {
                var listDelete = (from ProductBarcode e in product.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "ProductBarcode").ToList()[0].Value select e).ToList();

                foreach (var item in listDelete)
                {
                    builder.Append("<ProductBarcode>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("<Barcode>" + item.Barcode + "</Barcode>");
                    builder.Append("<IsDelete>" + 1 + "</IsDelete>");
                    builder.Append("</ProductBarcode>");
                }
            }

            if (product.POSPathologies != null && product.POSPathologies.Count > 0)
            {
                foreach (var item in product.POSPathologies)
                {
                    builder.Append("<POSPathologies>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("<DiagnosticId>" + item.DiagnosticId + "</DiagnosticId>");
                    builder.Append("<DiagnosticCode>" + item.DiagnosticCode + "</DiagnosticCode>");
                    builder.Append("<MinimumAge>" + item.MinimumAge + "</MinimumAge>");
                    builder.Append("<MaximumAge>" + item.MaximumAge + "</MaximumAge>");
                    builder.Append("<AgeMeasure>" + item.AgeMeasure + "</AgeMeasure>");
                    builder.Append("<ProductId>" + item.ProductId + "</ProductId>");
                    builder.Append("<IsDelete>" + (item.ChangeTracker.State == ObjectState.Deleted ? 1 : 0) + "</IsDelete>");
                    builder.Append("</POSPathologies>");
                }
            }

            if (product.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("POSPathologies"))
            {
                var listDelete = (from POSPathologies e in product.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "POSPathologies").ToList()[0].Value select e).ToList();

                foreach (var item in listDelete)
                {
                    builder.Append("<POSPathologies>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("<DiagnosticId>" + item.DiagnosticId + "</DiagnosticId>");
                    builder.Append("<DiagnosticCode>" + item.DiagnosticCode + "</DiagnosticCode>");
                    builder.Append("<MinimumAge>" + item.MinimumAge + "</MinimumAge>");
                    builder.Append("<MaximumAge>" + item.MaximumAge + "</MaximumAge>");
                    builder.Append("<AgeMeasure>" + item.AgeMeasure + "</AgeMeasure>");
                    builder.Append("<ProductId>" + item.ProductId + "</ProductId>");
                    builder.Append("<IsDelete>" + 1 + "</IsDelete>");
                    builder.Append("</POSPathologies>");
                }
            }

            if (product?.ProductHierarchy2 != null && product.ProductHierarchy2.ToList().Any())
            {
                foreach (var item in product.ProductHierarchy2.ToList())
                {
                    builder.Append("<ProductHierarchy>");
                    builder.Append($"<Id>{item.Id}</Id>");
                    builder.Append($"<HierarchyProductFinalId>{item.HierarchyProductFinalId}</HierarchyProductFinalId>");
                    builder.Append($"<ProductId>{item.ProductId}</ProductId>");
                    builder.Append($"<ParentProductId>{item.ParentProductId}</ParentProductId>");
                    builder.Append($"<ConversionUnit>{item.ConversionUnit}</ConversionUnit>");
                    builder.Append($"<IsDelete>{(item.ChangeTracker.State == ObjectState.Deleted ? 1 : 0)}</IsDelete>");
                    builder.Append("</ProductHierarchy>");
                }
            }

            builder.Append("</InventoryProduct>");

            return builder.ToString();
        }

        public int maxSize(string VarField, int MaxLength)
        {
            int Size = 0;
            if (VarField.Length < MaxLength)
            {
                Size = VarField.Length;
            }
            else
            {
                Size = MaxLength;
            }
            return Size;
        }

        public System.Data.DataTable GetDatatable(string Comando, SessionValues session, string nameDt)
        {
            System.Data.DataTable functionReturnValue = default(System.Data.DataTable);
            System.Data.SqlClient.SqlConnection conexion = new System.Data.SqlClient.SqlConnection();


            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion = new System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, string.Empty, session.TransactionalContainer, false));
                    conexion.Open();
                }
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(Comando, conexion);
                da.SelectCommand.CommandTimeout = 30000;
                DataSet ds = new DataSet();
                da.Fill(ds, nameDt);
                functionReturnValue = ds.Tables[nameDt];
                da = null;
                ds = null;
                conexion.Close();
                return functionReturnValue;

            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session);
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

        #endregion

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
                _productRepository = null;
                _sequenseRepository = null;
                _hierarchyRepository = null;
                _productTypeRepository = null;
                _INPRODPATRepository = null;
                _IHLISTPRORepository = null;
                _ATCRepository = null;
                _POSPathologiesRepository = null;
                _ThirdPartyRepository = null;
                _PhysicalInventoryRepository = null;
                _medicationTypeRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }

        #endregion

    }
}