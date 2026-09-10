//'************************************************************
//' Assembly         : Application.Inventory.PurchaseRequest
//' Author           : Hector Rodriguez Rubiano
//' Created          : 10/04/2019
//'
//' Copyright        : (c) . All rights reserved.
//' About            : PBI3499
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

namespace Application.Inventory.PurchaseRequest
{
    public class PurchaseRequestAdminService : IPurchaseRequestAdminService
    {
        #region Variables
        private IPurchaseRequestRepository _PurchaseRequestRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        #endregion

        #region Builder

        public PurchaseRequestAdminService(IPurchaseRequestRepository PurchaseRequestRepository, IInventorySequenceDetailRepository sequenseRepository)
        {
            if (PurchaseRequestRepository == null)
            {
                throw new ArgumentNullException("Repositorio de PurchaseRequestRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }

            _PurchaseRequestRepository = PurchaseRequestRepository;
            _sequenseRepository = sequenseRepository;

        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda una solicitud de compra de inventario
        /// </summary>
        /// <param name="oPurchaseRequest"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <param name="sequenceC"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PurchaseRequest> SavePurchaseRequest(Domain.Entities.PurchaseRequest oPurchaseRequest, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null)
        {
            if (oPurchaseRequest == null)
            {
                throw new ArgumentNullException("PurchaseRequest");
            }
            IUnitWork unitOfWork = _PurchaseRequestRepository.UnitWork;
            IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;

            try
            {
                InventorySequenceDetail seq = (idSecuence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSecuence)));
                if (oPurchaseRequest.Code == null || oPurchaseRequest.Code.Trim().Equals(string.Empty))
                {
                    if (seq != null)
                    {
                        if (seq.Id == 0)
                        {
                            seq.IdSequense = sequenceC.IdSequence.Value;
                            seq.InventorySequenceId = sequenceC.Id;
                            seq.Next = 1;
                            seq.Prefix = oPurchaseRequest.Prefix;
                        }
                        var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(oPurchaseRequest.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                        if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                        {
                            oPurchaseRequest.Code = res;
                            seq.Next += 1;
                            this._sequenseRepository.SaveEntity(seq);
                            unitOfWorkSequense.Commit();
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.PurchaseRequest> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.PurchaseRequest> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                    }
                }

                Domain.Entities.PurchaseRequest auxPurchaseRequest = null;
                IndigoAuditSimpleEntity<Domain.Entities.PurchaseRequest> auditProcess;
                Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Insert;

                if (oPurchaseRequest.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                {
                    oPurchaseRequest.CreationUser = audit.CodeUser;
                    oPurchaseRequest.CreationDate = DateTime.Now;
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert;

                    if (oPurchaseRequest.Status == 2)
                    {
                        oPurchaseRequest.ConfirmationUser = audit.CodeUser;
                        oPurchaseRequest.ConfirmationDate = DateTime.Now;
                    }
                }
                else
                {
                    auxPurchaseRequest = oPurchaseRequest.OriginalValue;
                    switch (oPurchaseRequest.Status)
                    {
                        case 1:
                            oPurchaseRequest.ModificationUser = audit.CodeUser;
                            oPurchaseRequest.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                            break;
                        case 2:
                            oPurchaseRequest.ModificationUser = audit.CodeUser;
                            oPurchaseRequest.ModificationDate = DateTime.Now;
                            oPurchaseRequest.ConfirmationUser = audit.CodeUser;
                            oPurchaseRequest.ConfirmationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Confirm;
                            break;
                        case 3:
                            oPurchaseRequest.ModificationUser = audit.CodeUser;
                            oPurchaseRequest.ModificationDate = DateTime.Now;
                            oPurchaseRequest.AnnulmentUser = audit.CodeUser;
                            oPurchaseRequest.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Annular;
                            break;
                    }
                }
                foreach (Domain.Entities.PurchaseRequestDetail detail in oPurchaseRequest.PurchaseRequestDetail)
                {
                    if (detail.InventoryProduct != null)
                    {
                        if (detail.InventoryProduct.ProductGroup != null)
                        {
                            var groupId = detail.InventoryProduct.ProductGroup.Id;
                            detail.InventoryProduct.ProductGroup = null;
                            detail.InventoryProduct.ProductGroupId = groupId;
                        }
                        if (detail.InventoryProduct.ProductSubGroup != null)
                        {
                            var subGroupId = detail.InventoryProduct.ProductSubGroup.Id;
                            detail.InventoryProduct.ProductSubGroup = null;
                            detail.InventoryProduct.ProductSubGroupId = subGroupId;
                        }
                    }
                }
                _PurchaseRequestRepository.SaveEntity(oPurchaseRequest);
                unitOfWork.Commit();
                
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PurchaseRequest>(oPurchaseRequest, audit, status, auxPurchaseRequest);
                auditProcess.Execute();

                return new ActionResult<Domain.Entities.PurchaseRequest> { StateResult = true, ObjectEmbbeded = oPurchaseRequest };
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PurchaseRequest> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PurchaseRequest> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PurchaseRequest> { StateResult = false, MessageResult = { ex.Message } };
            }

        }

        /// <summary>
        /// Eliminar Una solicitud de compra de inventario
        /// </summary>
        /// <param name="oPurchaseRequest"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeletePurchaseRequest(Domain.Entities.PurchaseRequest oPurchaseRequest, AuditMessage audit)
        {
            if (oPurchaseRequest == null)
            {
                throw new ArgumentNullException("product");
            }

            IUnitWork unitOfWork = _PurchaseRequestRepository.UnitWork;
            try
            {
                oPurchaseRequest.MarkAsDeleted();
                IndigoAuditSimpleEntity<Domain.Entities.PurchaseRequest> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PurchaseRequest>(oPurchaseRequest, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                _PurchaseRequestRepository.DeleteEntity(oPurchaseRequest);
                unitOfWork.Commit();
                auditProcess.Execute();

                return new ActionResult { StateResult = true };

            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (DbUpdateException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, Message = ResourceManager.get_GetString("ErrorDependence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Cambiar el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PurchaseRequest> ChangeStatePurchaseRequest(string code, byte state, AuditMessage audit)
        {
            Domain.Entities.PurchaseRequest PurchaseRequest = _PurchaseRequestRepository.GetPurchaseRequestByCode(code);
            PurchaseRequest.Status = state;
            return SavePurchaseRequest(PurchaseRequest, audit);
        }

        /// <summary>
        /// Obtiene Una solicitud de compra de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.PurchaseRequest> GetPurchaseRequestByCode(string code, AuditMessage audit)
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
                Domain.Entities.PurchaseRequest PurchaseRequest = _PurchaseRequestRepository.GetPurchaseRequestByCode(code);
                if (PurchaseRequest != null && PurchaseRequest.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.PurchaseRequest> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.PurchaseRequest>(PurchaseRequest, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.PurchaseRequest> { StateResult = true, ObjectEmbbeded = PurchaseRequest };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.PurchaseRequest> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene Una solicitud de compra de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.PurchaseRequest GetPurchaseRequestById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _PurchaseRequestRepository.GetPurchaseRequestById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
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
                _PurchaseRequestRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
