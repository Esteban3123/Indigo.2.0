//***********************************************************************
// Assembly         : Application.Payments
// Author           : Juan Carlos Bermudez Gutierrez
// Created          : 30/04/2015
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

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

namespace Application.Inventory.InventoryRequest
{
    public class InventoryRequestAdminService : IInventoryRequestAdminService
    {
        #region Variables
        private IInventoryRequestRepository _InventoryRequestRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IRequestParamFunctionalUnitRepository _RequestParamFunctionalUnitRepository;
        private IRequestParamWarehouseRepository _RequestParamWarehouseRepository;
        private IRequestParamAuthUserRepository _RequestParamAuthUserRepository;
        #endregion

        #region Builder

        public InventoryRequestAdminService(IInventoryRequestRepository InventoryRequestRepository, IInventorySequenceDetailRepository sequenseRepository,
                                            IRequestParamFunctionalUnitRepository requestParamFunctionalUnitRepository, IRequestParamWarehouseRepository requestParamWarehouseRepository, IRequestParamAuthUserRepository requestParamAuthUserRepository)
        {
            if (InventoryRequestRepository == null)
            {
                throw new ArgumentNullException("Repositorio de InventoryRequestRepository vacio");
            }
            if (sequenseRepository == null)
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }

            _InventoryRequestRepository = InventoryRequestRepository;
            _sequenseRepository = sequenseRepository;
            _RequestParamFunctionalUnitRepository = requestParamFunctionalUnitRepository;
            _RequestParamWarehouseRepository = requestParamWarehouseRepository;
            _RequestParamAuthUserRepository = requestParamAuthUserRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Guarda una solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequest"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryRequest> SaveInventoryRequest(Domain.Entities.InventoryRequest inventoryRequest, AuditMessage audit, Int64 idSecuence = 0, InventorySequence sequenceC = null)
        {
            if (inventoryRequest == null)
            {
                throw new ArgumentNullException("inventoryRequest");
            }
            IUnitWork unitOfWork = _InventoryRequestRepository.UnitWork;
            IUnitWork unitOfWorkSequense = _sequenseRepository.UnitWork;

            try
            {
                InventorySequenceDetail seq = (idSecuence == 0 ? new InventorySequenceDetail() : this._sequenseRepository.GetSequenseDById(Convert.ToInt32(idSecuence)));
                if (inventoryRequest.Code == null || inventoryRequest.Code.Trim().Equals(string.Empty))
                {
                    if (seq != null)
                    {
                        if (seq.Id == 0)
                        {
                            seq.IdSequense = sequenceC.IdSequence.Value;
                            seq.InventorySequenceId = sequenceC.Id;
                            seq.Next = 1;
                            seq.Prefix = inventoryRequest.Prefix;
                        }
                        var res = (seq.Id > 0 ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Prefix, seq.Sequense.Pattern, seq.Next) : (sequenceC != null ? Infrastructure.CrossCutting.Base.Sequense.GetSequense(inventoryRequest.Prefix, sequenceC.Sequense.Pattern, seq.Next) : null));
                        if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                        {
                            inventoryRequest.Code = res;
                            seq.Next += 1;
                            this._sequenseRepository.SaveEntity(seq);
                            unitOfWorkSequense.Commit();
                        }
                        else
                        {
                            return new ActionResult<Domain.Entities.InventoryRequest> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                        }
                    }
                    else
                    {
                        return new ActionResult<Domain.Entities.InventoryRequest> { StateResult = false, Message = ResourceManager.get_GetString("SequenceNotFound") };
                    }
                }

                Domain.Entities.InventoryRequest auxInventoryRequest = null;
                IndigoAuditSimpleEntity<Domain.Entities.InventoryRequest> auditProcess;
                Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Insert;

                if (inventoryRequest.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                {
                    inventoryRequest.CreationUser = audit.CodeUser;
                    inventoryRequest.CreationDate = DateTime.Now;
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert;

                    if (inventoryRequest.Status == 2)
                    {
                        inventoryRequest.ConfirmationUser = audit.CodeUser;
                        inventoryRequest.ConfirmationDate = DateTime.Now;
                    }
                }
                else
                {
                    auxInventoryRequest = inventoryRequest.OriginalValue;
                    switch (inventoryRequest.Status)
                    {
                        case 1:
                            inventoryRequest.ModificationUser = audit.CodeUser;
                            inventoryRequest.ModificationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Update;
                            break;
                        case 2:
                            inventoryRequest.ModificationUser = audit.CodeUser;
                            inventoryRequest.ModificationDate = DateTime.Now;
                            inventoryRequest.ConfirmationUser = audit.CodeUser;
                            inventoryRequest.ConfirmationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Confirm;
                            break;
                        case 3:
                            inventoryRequest.ModificationUser = audit.CodeUser;
                            inventoryRequest.ModificationDate = DateTime.Now;
                            inventoryRequest.AnnulmentUser = audit.CodeUser;
                            inventoryRequest.AnnulmentDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Annular;
                            break;
                    }
                }

                if (inventoryRequest.Status == 2)
                {

                    if (inventoryRequest.RequestType == 1 && inventoryRequest.InventoryRequestDetailOther.Any())
                    {
                        RequestParamFuncionalUnit RequestParamFunctionalUnit = _RequestParamFunctionalUnitRepository.
                        GetByFilter(m => m.FunctionalUnitId == inventoryRequest.TargetFunctionalUnitId, false, new List<string> { "RequestParam" }).FirstOrDefault();

                        if (RequestParamFunctionalUnit != null)
                        {
                            if (RequestParamFunctionalUnit.RequestParam.RequiredAuthorization)
                            {
                                foreach (var item in inventoryRequest.InventoryRequestDetailOther)
                                {
                                    Domain.Entities.InventoryRequestDetailOther InventoryRequestDetailtmp = item.Clone();
                                    item.OriginalQuantity = InventoryRequestDetailtmp.Quantity;
                                    item.Quantity = 0;
                                    item.OutstandingQuantity = 0;
                                }
                            }

                        }
                    }

                    if (inventoryRequest.RequestType == 2 && inventoryRequest.InventoryRequestDetailOther.Any())
                    {
                        RequestParamWarehouse RequestParamWarehouse = _RequestParamWarehouseRepository.
                        GetByFilter(m => m.WarehouseId == inventoryRequest.TargetWarehouseId, false, new List<string> { "RequestParam" }).FirstOrDefault();

                        if (RequestParamWarehouse != null)
                        {
                            if (RequestParamWarehouse.RequestParam.RequiredAuthorization)
                            {
                                foreach (var item in inventoryRequest.InventoryRequestDetailOther)
                                {
                                    Domain.Entities.InventoryRequestDetailOther InventoryRequestDetailtmp = item.Clone();
                                    item.OriginalQuantity = InventoryRequestDetailtmp.Quantity;
                                    item.Quantity = 0;
                                    item.OutstandingQuantity = 0;
                                }
                            }
                        }
                    }

                    foreach (Domain.Entities.InventoryRequestDetail detail in inventoryRequest.InventoryRequestDetail)
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
                }
                _InventoryRequestRepository.SaveEntity(inventoryRequest);
                unitOfWork.Commit();
                
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryRequest>(inventoryRequest, audit, status, auxInventoryRequest);
                auditProcess.Execute();

                return new ActionResult<Domain.Entities.InventoryRequest> { StateResult = true, ObjectEmbbeded = inventoryRequest };

            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryRequest> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (System.Data.Entity.Core.UpdateException ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryRequest> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChangesUnitOfWork();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryRequest> { StateResult = false, MessageResult = { ex.Message } };
            }

        }

        /// <summary>
        /// Eliminar Una solicitud de inventario
        /// </summary>
        /// <param name="inventoryRequest"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteInventoryRequest(Domain.Entities.InventoryRequest inventoryRequest, AuditMessage audit)
        {
            if (inventoryRequest == null)
            {
                throw new ArgumentNullException("product");
            }

            IUnitWork unitOfWork = _InventoryRequestRepository.UnitWork;
            try
            {
                inventoryRequest.MarkAsDeleted();
                IndigoAuditSimpleEntity<Domain.Entities.InventoryRequest> auditProcess;
                auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryRequest>(inventoryRequest, audit, Infrastructure.CrossCutting.Audit.Actions.Delete);
                _InventoryRequestRepository.DeleteEntity(inventoryRequest);
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
        public ActionResult<Domain.Entities.InventoryRequest> ChangeStateInventoryRequest(string code, byte state, AuditMessage audit)
        {
            Domain.Entities.InventoryRequest inventoryRequest = _InventoryRequestRepository.GetInventoryRequestByCode(code);
            inventoryRequest.Status = state;
            return SaveInventoryRequest(inventoryRequest, audit);
        }

        /// <summary>
        /// Obtiene Una solicitud de inventario por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.InventoryRequest> GetInventoryRequestByCode(string code, AuditMessage audit)
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
                Domain.Entities.InventoryRequest inventoryRequest = _InventoryRequestRepository.GetInventoryRequestByCode(code);
                if (inventoryRequest != null && inventoryRequest.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryRequest> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryRequest>(inventoryRequest, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.InventoryRequest> { StateResult = true, ObjectEmbbeded = inventoryRequest };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryRequest> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene Una solicitud de inventario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.InventoryRequest GetInventoryRequestById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _InventoryRequestRepository.GetInventoryRequestById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// CopyPaste/Import solicitudes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult<List<Domain.Entities.InventoryRequestDetail>> SP_CopyPasteAndImportRequests(List<List<string>> data)
        {
            try
            {
                string xml = ConvertDataToXml(data);
                List<SP_CopyPasteAndImportRequests_Result> result = _InventoryRequestRepository.SP_CopyPasteAndImportRequests(xml);
                if((from x in result where x.CodeResult == 888 select x).Count() > 0)
                {
                    return new ActionResult<List<Domain.Entities.InventoryRequestDetail>> { StateResult = false, Message = (from x in result where x.CodeResult == 888 select x.MessageResult).FirstOrDefault() };
                }

                List<string> listErrors = new List<string>();
                List<Domain.Entities.InventoryRequestDetail> listDetails = new List<Domain.Entities.InventoryRequestDetail>();

                foreach(var item in result)
                {
                    if(item.CodeResult == 000)
                    {
                        Domain.Entities.InventoryRequestDetail detail = new Domain.Entities.InventoryRequestDetail();
                        detail.InventoryProductId = item.ProductId.Value;
                        detail.DescriptionProduct = item.ProductDescription;
                        detail.consumptionUnit = item.ConsumptionUnit;
                        detail.Quantity = Convert.ToInt32(item.Quantity);
                        detail.OutstandingQuantity = Convert.ToInt32(item.Quantity);
                        detail.Description = item.Observation;

                        listDetails.Add(detail);
                    }
                    else
                    {
                        listErrors.Add(item.MessageResult);
                    }
                }

                return new ActionResult<List<Domain.Entities.InventoryRequestDetail>> { StateResult = true, ObjectEmbbeded = listDetails, MessageResult = listErrors };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.InventoryRequestDetail>> { StateResult = false, Message = ex.Message };
            }
        }

        public string ConvertDataToXml(List<List<string>> data)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<InventoryRequest>");

            foreach(var item in data)
            {
                builder.Append("<InventoryRequestDetail>");
                builder.Append("<ProductCode>" + (item.Count() > 0 ? item[0] : "---") + "</ProductCode>");
                builder.Append("<Quantity>" + (item.Count() > 1 ? item[1] : "0") + "</Quantity>");
                builder.Append("<Observation>" + (item.Count() > 2 ? item[2] : "") + "</Observation>");
                builder.Append("</InventoryRequestDetail>");
            }

            builder.Append("</InventoryRequest>");

            return builder.ToString();
        }

        #endregion

        #region  CopyPaste/Import
        /// <summary>
        /// CopyPaste/Import solicitudes medicamentos,insumos,otros
        /// </summary>
        /// <param name = "data" ></ param >
        /// < returns ></ returns >
        public ActionResult<List<Domain.Entities.InventoryRequestDetailOther>> SP_CopyPasteAndImportRequestsOtherDetail(List<List<string>> data)
        {
            try
            {
                string xml = ConvertDataOtherDetailToXml(data);
                List<SP_CopyPasteAndImportRequestsOtherDetail_Result> result = _InventoryRequestRepository.SP_CopyPasteAndImportRequestsOtherDetail(xml);
                if ((from x in result where x.CodeResult == 888 select x).Count() > 0)
                {
                    return new ActionResult<List<Domain.Entities.InventoryRequestDetailOther>> { StateResult = false, Message = (from x in result where x.CodeResult == 888 select x.MessageResult).FirstOrDefault() };
                }

                List<string> listErrors = new List<string>();
                List<Domain.Entities.InventoryRequestDetailOther> listDetails = new List<Domain.Entities.InventoryRequestDetailOther>();

                foreach (var item in result)
                {
                    if (item.CodeResult == 000)
                    {
                        Domain.Entities.InventoryRequestDetailOther detail = new Domain.Entities.InventoryRequestDetailOther();

                        switch  (item.ComponentType)
                        {
                                case "1" :
                                    detail.ATCId = item.RequestDetailOtherId;
                                    detail.ComponentType = 1;
                                    break;                         
                               case "2" :
                                    detail.SupplieId = item.RequestDetailOtherId;
                                    detail.ComponentType = 2;
                                break;
                               case "3":
                                    detail.InventoryProductId = item.RequestDetailOtherId;
                                    detail.ComponentType = 3;
                                break;
                        }

                        detail.ComponentTypeName = item.ComponentTypeName;
                        detail.SourceCodeName = item.SourceCodeName;
                        detail.consumptionUnit = item.consumptionUnit;
                        detail.Quantity = Convert.ToInt32(item.Quantity);
                        detail.OutstandingQuantity = Convert.ToInt32(item.Quantity);
                        detail.Description = item.Description;

                        listDetails.Add(detail);
                    }
                    else
                    {
                        listErrors.Add(item.MessageResult);
                    }
                }

                return new ActionResult<List<Domain.Entities.InventoryRequestDetailOther>> { StateResult = true, ObjectEmbbeded = listDetails, MessageResult = listErrors };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.InventoryRequestDetailOther>> { StateResult = false, Message = ex.Message };
            }
        }

        public string ConvertDataOtherDetailToXml(List<List<string>> data)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<InventoryRequest>");

            foreach (var item in data)
            {
                builder.Append("<InventoryRequestDetailOther>");
                builder.Append("<ComponentType>" + (item.Count() > 0 ? item[0] : "0") + "</ComponentType>");                
                builder.Append("<ProductCode>" + (item.Count() > 1 ? item[1] : "---") + "</ProductCode>");
                builder.Append("<Quantity>" + (item.Count() > 2 ? item[2] : "0") + "</Quantity>");
                builder.Append("<Description>" + (item.Count() > 3 ? item[3] : "") + "</Description>");
                builder.Append("</InventoryRequestDetailOther>");
            }

            builder.Append("</InventoryRequest>");

            return builder.ToString();
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
                _InventoryRequestRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
