//'************************************************************
//' Assembly         : Application.Inventory.InventorySupplie
//' Author           : Hector Rodriguez Rubiano
//' Created          : 08/01/2020
//'
//' Copyright        : (c) . All rights reserved.
//' About            : PBI7129
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
using Domain.Entities;
using System.Data;
using Infrastructure.CrossCutting.Resources;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using System.Data.Entity.Validation;
using Domain.Entities.Service;
using Infrastructure.CrossCutting.Queue;
using Application.Events.Serializers;

namespace Application.Inventory.InventorySupplie
{
    public class InventorySupplieAdminService : IInventorySupplieAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmInventorySupplie";
        private IInventorySupplieRepository _InventorySupplieRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// 
        /// </summary>
        /// <param name="InventorySupplieRepository"></param>
        /// <param name="sequenseRepository"></param>
        public InventorySupplieAdminService(IInventorySupplieRepository InventorySupplieRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue factoryQueue)
        {
            if (InventorySupplieRepository == null)
            {
                throw new ArgumentNullException("Repositorio de InventorySupplieRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }

            _InventorySupplieRepository = InventorySupplieRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda un insumo de inventario
        /// </summary>
        /// <param name="oInventorySupplie"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventorySupplie> SaveInventorySupplie(Domain.Entities.InventorySupplie oInventorySupplie, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null)
        {
            if (oInventorySupplie == null)
            {
                throw new ArgumentNullException("InventorySupplie");
            }
            //IUnitWork unitOfWork = _InventorySupplieRepository.UnitWork;
            IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;


            string xml = ConvertToXml(oInventorySupplie);

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(oInventorySupplie.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                oInventorySupplie.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                unitOfWorkSequense.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.InventorySupplie> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), oInventorySupplie.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.InventorySupplie> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }

                    Domain.Entities.InventorySupplie auxInventorySupplie = null;
                    IndigoAuditSimpleEntity<Domain.Entities.InventorySupplie> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Insert;

                    if (oInventorySupplie.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        oInventorySupplie.CreationUser = audit.CodeUser;
                        oInventorySupplie.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxInventorySupplie = oInventorySupplie.OriginalValue;
                        oInventorySupplie.ModificationUser = audit.CodeUser;
                        oInventorySupplie.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    xml = xml.Replace("[Code]", oInventorySupplie.Code);

                    if (oInventorySupplie.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added) { oInventorySupplie.CreationUser = audit.CodeUser; } else { oInventorySupplie.ModificationUser = audit.CodeUser; oInventorySupplie.ModificationDate = DateTime.Now; }
                    SP_SaveSupplie_Result result = _InventorySupplieRepository.SP_SaveSupplie(xml, audit.CodeUser);
                    if (result.CodeResult == 999)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = false, Message = result.MessageResult, StatusCode = eStatusResult.WARNING };
                    }
                    TriggerEvent(oInventorySupplie, audit);

                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventorySupplie>(oInventorySupplie, audit, status, auxInventorySupplie);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    oInventorySupplie.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = oInventorySupplie, Message = MessageResult };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    //unitOfWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence"), StatusCode = eStatusResult.EXCEPTION };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    //unitOfWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown"), StatusCode = eStatusResult.EXCEPTION };
                }
                catch (DbUpdateException ex)
                {
                    scope.Dispose();
                    //unitOfWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    string message = ex.InnerException == null ? "" : ex.InnerException.InnerException.Message;
                    return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = false, Message = "Revisar el Trigger de la Tabla. Error del Sistema: " + message, StatusCode = eStatusResult.EXCEPTION };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    //unitOfWork.RollbackChangesUnitOfWork();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = false, Message = ex.Message, StatusCode = eStatusResult.EXCEPTION };
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inventorySupplie"></param>
        /// <returns></returns>
        private string ConvertToXml(Domain.Entities.InventorySupplie inventorySupplie)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<Supplie>");

            result.Append("<Id>" + inventorySupplie.Id + "</Id>");
            result.Append("<Code>[Code]</Code>");
            result.Append("<SupplieName>" + inventorySupplie.SupplieName + "</SupplieName>");
            result.Append("<RiskLevelId>" + inventorySupplie.RiskLevelId + "</RiskLevelId>");
            result.Append("<PBSProduct>" + inventorySupplie.PBSProduct + "</PBSProduct>");
            result.Append("<SupplieStatus>" + inventorySupplie.SupplieStatus + "</SupplieStatus>");
            result.Append("<JustificationOfInputs>" + inventorySupplie.JustificationOfInputs + "</JustificationOfInputs>");
            result.Append("<OsteosynthesisMaterial>" + inventorySupplie.OsteosynthesisMaterial + "</OsteosynthesisMaterial>");
            result.Append("<Consumption>" + inventorySupplie.Consumption + "</Consumption>");
            result.Append("<OptometryDevice>" + inventorySupplie.OptometryDevice + "</OptometryDevice>");
            result.Append("<MedicalDevice>" + inventorySupplie.MedicalDevice + "</MedicalDevice>");
            result.Append("<IsParenteralNutritionSupply>" + inventorySupplie.IsParenteralNutritionSupply + "</IsParenteralNutritionSupply>");

            result.Append("</Supplie>");

            return result.ToString();
        }

        /// <summary>
        /// Eliminar un insumo de inventario
        /// </summary>
        /// <param name="oInventorySupplie"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteInventorySupplie(Domain.Entities.InventorySupplie oInventorySupplie, AuditMessage audit)
        {
            if (oInventorySupplie == null)
            {
                throw new ArgumentNullException("InventorySupplie");
            }

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    SP_DeleteSupplie_Result result = _InventorySupplieRepository.SP_DeleteSupplie(oInventorySupplie.Id);
                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.Message };
                    }

                    oInventorySupplie.MarkAsDeleted();
                    TriggerEvent(oInventorySupplie, audit);

                    scope.Complete();
                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = ResourceManager.get_GetString("RecordDeleted") };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
                }
            }
        }

        /// <summary>
        /// Cambiar el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventorySupplie> ChangeStateInventorySupplie(string code, bool state, AuditMessage audit)
        {
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
                Domain.Entities.InventorySupplie InventorySupplie = _InventorySupplieRepository.GetInventorySupplieByCode(code);
                if (InventorySupplie != null && InventorySupplie.Id > 0)
                {
                    InventorySupplie.SupplieStatus = state;
                }
                var result = SaveInventorySupplie(InventorySupplie, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene un insumo de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventorySupplie> GetInventorySupplieByCode(string code, AuditMessage audit)
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
                Domain.Entities.InventorySupplie InventorySupplie = _InventorySupplieRepository.GetInventorySupplieByCode(code);
                if (InventorySupplie != null && InventorySupplie.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventorySupplie> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventorySupplie>(InventorySupplie, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = true, ObjectEmbbeded = InventorySupplie };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventorySupplie> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un insumo de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.InventorySupplie GetInventorySupplieById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _InventorySupplieRepository.GetInventorySupplieById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }


        #endregion

        #region Events
        public void TriggerEvent(Domain.Entities.InventorySupplie inventorySupplie, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = inventorySupplie.ChangeTracker.State.ToString().ToLower();
            if (inventorySupplie.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(inventorySupplie, audit.CodeUser, ChangeTracker, DittoSourceType.inventorySupplie);
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

                }
                _InventorySupplieRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}

