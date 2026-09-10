//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 15/09/2014
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Application.Base;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Application.Inventory.ProductSubGroups;
using Domain.Entities;
using System.Data;
using Application.Security;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using Infrastructure.CrossCutting.Resources;
using Application.Inventory.DecreaseMaximumLimit;
using Infrastructure.CrossCutting.Queue;

namespace Application.Inventory.Warehouse
{
    public class WarehouseAdminService : IWarehouseAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmStores";
        private IWarehouseRepository _warehouseRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IUserAdminService _IUserAdminService;
        private IDecreaseMaximumLimitAdminService _decreaseMaximumLimitAdminService;
        private Domain.Crystal.IADCENATENRepository _ADCENATENRepository;
        private IFactoryQueue _factoryQueue;
        private ISettingInventoryRepository _settigInventoryRepository;
        #endregion

        #region Builder
        /// <summary>
        /// inicia 
        /// </summary>
        /// <param name="warehouseRepository"></param>
        /// <param name="sequenseRepository"></param>
        /// <param name="IUserAdminService"></param>
        /// <param name="centerAttentionRepository"></param>
        public WarehouseAdminService(IWarehouseRepository warehouseRepository, IInventorySequenceDetailRepository sequenseRepository, IUserAdminService IUserAdminService,
            Domain.Crystal.IADCENATENRepository centerAttentionRepository, IDecreaseMaximumLimitRepository maximumLimitRepository, IDecreaseMaximumLimitAdminService decreaseMaximumLimitAdminService,
            IFactoryQueue factoryQueue, ISettingInventoryRepository settingInventoryRepository)
        {
            if ((warehouseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de warehouseRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            if ((IUserAdminService == null))
            {
                throw new ArgumentNullException("Repositorio de IUserAdminService vacio");
            }
            if ((settingInventoryRepository == null))
            {
                throw new ArgumentNullException("Repositorio de settingInventory vacio");
            }
            _warehouseRepository = warehouseRepository;
            _sequenseRepository = sequenseRepository;
            _IUserAdminService = IUserAdminService;
            _ADCENATENRepository = centerAttentionRepository;
            _decreaseMaximumLimitAdminService = decreaseMaximumLimitAdminService;
            _factoryQueue = factoryQueue;
            _settigInventoryRepository = settingInventoryRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda o actualiza un almacen
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>'List<DecreaseMaximumLimit> Decrease = null
        public ActionResult<Domain.Entities.Warehouse> SaveWarehouse(Domain.Entities.Warehouse warehouse, AuditMessage audit, List<Domain.Entities.DecreaseMaximumLimit> Decrease = null, long idSecuence = 0)
        {
            if (warehouse == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }

            switch (warehouse.WareHouseType)
            {
                case 1:
                    warehouse.VirtualStore = true;
                    break;
                case 2:
                    warehouse.WarehouseConsignment = true;
                    break;
                case 3:
                    warehouse.CustodyStore = true;
                    break;
                case 4:
                    warehouse.TransitStore = true;
                    break;
                case 5:
                    warehouse.ControlStore = true;
                    break;
                default:
                    warehouse.VirtualStore = false;
                    warehouse.WarehouseConsignment = false;
                    warehouse.CustodyStore = false;
                    warehouse.TransitStore = false;
                    warehouse.ControlStore = false;
                    break;
            }

            if (warehouse.VirtualStore == true && warehouse.WarehouseConsignment == true)
            {
                return new ActionResult<Domain.Entities.Warehouse> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "Un almacén de consignación no puede ser un almacén virtual." } };
            }

            
            IUnitWork unitOfWork = this._warehouseRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(warehouse.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                warehouse.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.Warehouse> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), warehouse.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.Warehouse> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.Warehouse auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.Warehouse> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (warehouse.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        warehouse.CreationUser = audit.CodeUser;
                        warehouse.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = warehouse.OriginalValue;
                        warehouse.ModificationUser = audit.CodeUser;
                        warehouse.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    //En caso de que la condición de restriccion este en no, se verfica que no existan detalles, de lo contrario se eliminan
                    if (warehouse.HandleRestrictedProducts == false)
                    {
                        if (warehouse.WarehouseRestrictedConditions.Any())
                        {
                            this._warehouseRepository.DeleteConditionsByWarehouseId(warehouse.Id);
                        }
                    }

                    this._warehouseRepository.SaveEntity(warehouse);
                    unitOfWork.Commit();

                    //Se afecta el decremento del producto
                    if (Decrease?.Count > 0)
                    {
                        var result = _decreaseMaximumLimitAdminService.SaveDecreaseProduct(Decrease, audit);
                        if (!result.StateResult)
                        {
                            return new ActionResult<Domain.Entities.Warehouse> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = result.Message };
                        }
                    }
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.Warehouse>(warehouse, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //se asegura que se hace commit y se se dispara el evento
                    TriggerEvent(warehouse, audit);
                    //Se marca la entidad como sin cambios
                    warehouse.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.Warehouse> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = warehouse, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.Warehouse> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Warehouse> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un almacen
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteWarehouse(Domain.Entities.Warehouse warehouse, AuditMessage audit)
        {
            if (warehouse == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            IUnitWork unitOfWork = this._warehouseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    warehouse.ModificationUser = audit.CodeUser;
                    warehouse.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.Warehouse>(warehouse, audit, status);

                    var warehouseUsers = warehouse.WarehouseUser;
                    var warehouseUserRequests = warehouse.WarehouseUserRequest;

                    foreach (var user in warehouseUsers.ToList())
                    {
                        user.MarkAsDeleted();
                    }

                    foreach (var userRequest in warehouseUserRequests.ToList())
                    {
                        userRequest.MarkAsDeleted();
                    }

                    warehouse.MarkAsDeleted();
                    _warehouseRepository.SaveEntity(warehouse);
                    unitOfWork.Commit();
                    auditProcess.Execute();
                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (UpdateException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<String> { "-000" }, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
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

        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.Warehouse> ChangeStateWarehouse(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.Warehouse warehouse = _warehouseRepository.GetWarehouse(code);
            //warehouse.Status = state;
            //return SaveWarehouse(warehouse, audit);



            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
            //if (Boolean .isn(state)) {
            //    throw new ArgumentNullException("state");
            //}
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.Warehouse Warehouse = this._warehouseRepository.GetWarehouse(code.Trim());
                if (Warehouse != null && Warehouse.Id > 0)
                {
                    Warehouse.Status = state;
                }
                var result = this.SaveWarehouse(Warehouse, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Warehouse> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un almacen por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.Warehouse> GetWarehouse(string code, AuditMessage audit)
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
                Domain.Entities.Warehouse warehouse = _warehouseRepository.GetWarehouse(code);
                   
                //Se saca el listado de ids de usuario para enviar
                List<Domain.Security.Entities.User> ListUsers = new List<Domain.Security.Entities.User>();
                //Se consulta los usuarios por id para agregarles la descripcion
                List<int> ListUserIds = new List<int>();
                if (warehouse != null && warehouse.Id > 0 && !string.IsNullOrEmpty(warehouse.CodeCenterAttention))
                {
                    Domain.Crystal.Entities.ADCENATEN _adcenten = _ADCENATENRepository.GetADCENATENByCode(warehouse.CodeCenterAttention, false);
                    if (_adcenten != null)
                    {
                        warehouse.CenterAttentionDescription = string.Format("{0} - {1}", _adcenten.CODCENATE.Trim(), _adcenten.NOMCENATE.Trim());
                    }
                }

                if (warehouse != null && warehouse.Id > 0 && warehouse.WarehouseUser != null && warehouse.WarehouseUser.Count > 0)
                {
                    //Se recorre el listado de usuarios que trae el almacen para sacar los ids
                    foreach (WarehouseUser wu in warehouse.WarehouseUser)
                    {
                        ListUserIds.Add(wu.UserId);
                    }

                    //Se obtiene el listado de usuarios
                    ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds);
                    if (ListUsers != null && ListUsers.Count > 0)
                    {
                        //Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                        foreach (WarehouseUser wu in warehouse.WarehouseUser)
                        {
                            Domain.Security.Entities.User user = (from e in ListUsers where e.Id == wu.UserId select e).FirstOrDefault();
                            if (user != null && user.Id > 0 && user.Person != null)
                            {
                                wu.FullNameUser = user.Person.Fullname;
                            }
                        }
                    }

                }

               if (warehouse != null && warehouse.Id > 0 && warehouse.WarehouseUserRequest != null && warehouse.WarehouseUserRequest.Count > 0)
                {
                    //Se recorre el listado de usuarios que trae el almacen para sacar los ids
                    foreach (WarehouseUserRequest wu in warehouse.WarehouseUserRequest)
                    {
                        ListUserIds.Add(wu.UserId);
                    }

                    //Se obtiene el listado de usuarios
                    ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds);
                    if (ListUsers != null && ListUsers.Count > 0)
                    {
                        //Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                        foreach (WarehouseUserRequest wu in warehouse.WarehouseUserRequest)
                        {
                            Domain.Security.Entities.User user = (from e in ListUsers where e.Id == wu.UserId select e).FirstOrDefault();
                            if (user != null && user.Id > 0 && user.Person != null)
                            {
                                wu.FullNameUser = user.Person.Fullname;
                            }
                        }
                    }

                }

                return new ActionResult<Domain.Entities.Warehouse> { StateResult = true, ObjectEmbbeded = warehouse };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Warehouse> { StateResult = false, MessageResult = new List<string> { ex.Message, ex.InnerException.Message }};
            }
        }

        /// <summary>
        /// Obtiene un almacen por id
        /// </summary>
        /// <param name="idWarehouse"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.Warehouse> GetWarehouseById(int idWarehouse, AuditMessage audit)
        {
            if (idWarehouse == 0)
            {
                throw new ArgumentNullException("id");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.Warehouse warehouse = _warehouseRepository.GetWarehouseById(idWarehouse);
                if (warehouse != null && warehouse.Id > 0 && !string.IsNullOrEmpty(warehouse.CodeCenterAttention))
                {
                    Domain.Crystal.Entities.ADCENATEN _adcenten = _ADCENATENRepository.GetADCENATENByCode(warehouse.CodeCenterAttention, false);
                    if (_adcenten != null)
                    {
                        warehouse.CenterAttentionDescription = string.Format("{0} - {1}", _adcenten.CODCENATE.Trim(), _adcenten.NOMCENATE.Trim());
                    }
                }
                return new ActionResult<Domain.Entities.Warehouse> { StateResult = true, ObjectEmbbeded = warehouse };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Warehouse> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        public List<string> ListPrefixs()
        {
            try
            {
                return this._warehouseRepository.ListPrefixs();
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene la bodega de un proveedor por tipo
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="type"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ActionResult<Domain.Entities.Warehouse> GetWarehouseSupplierByType(int supplierId, int type, AuditMessage audit)
        {
            if (supplierId == 0)
            {
                throw new ArgumentNullException("supplierId");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                var settingInventory = _settigInventoryRepository.GetFirstOrDefaultSettingInventory();
                var mainWarehouse = settingInventory.MainWarehouseId.HasValue
                    ? _warehouseRepository.GetWarehouseById(settingInventory.MainWarehouseId.Value)
                    : null;
                int? costCenterId = mainWarehouse != null ? mainWarehouse.CostCenterId : (int?)null;

                Domain.Entities.Warehouse warehouse = _warehouseRepository.GetWarehouseSupplierByType(supplierId, type, costCenterId);
                if (warehouse != null && warehouse.Id > 0 && !string.IsNullOrEmpty(warehouse.CodeCenterAttention))
                {
                    Domain.Crystal.Entities.ADCENATEN _adcenten = _ADCENATENRepository.GetADCENATENByCode(warehouse.CodeCenterAttention, false);
                    if (_adcenten != null)
                    {
                        warehouse.CenterAttentionDescription = string.Format("{0} - {1}", _adcenten.CODCENATE.Trim(), _adcenten.NOMCENATE.Trim());
                    }
                }
                return new ActionResult<Domain.Entities.Warehouse> { StateResult = true, ObjectEmbbeded = warehouse };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.Warehouse> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        #endregion

        #region Events

        public void TriggerEvent(Domain.Entities.Warehouse warehouse, AuditMessage audit)
        {
            var wrapperEvent = new Events.Serializers.Wrapper();
            String ChangeTracker = warehouse.ChangeTracker.State.ToString().ToLower();
            if (warehouse.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(warehouse, audit.CodeUser, ChangeTracker, DittoSourceType.warehouse);
            IIndigoQueue queue = _factoryQueue.CreateQueue();
            queue.Publish(eventData);
            return;
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
                    _IUserAdminService.Dispose();
                }
                _warehouseRepository = null;
                _sequenseRepository = null;
                _IUserAdminService = null;
                _decreaseMaximumLimitAdminService = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
