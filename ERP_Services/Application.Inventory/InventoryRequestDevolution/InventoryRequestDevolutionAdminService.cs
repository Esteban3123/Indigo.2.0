///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Faiber Julian Mora D.
/// Created          : 28-09-2016
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using System;
using System.Collections.Generic;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Application.Base;

using System.Data;
using System.Transactions;
using System.Linq;
using System.Text;

namespace Application.Inventory.InventoryRequestDevolution
{
    public class InventoryRequestDevolutionAdminService : IInventoryRequestDevolutionAdminService
    {
        #region Variables

        private IInventoryRequestDevolutionRepository _InventoryRequestDevolution;
        private IInventorySequenceDetailRepository _SecuenseRepository;
        private IInventoryRequestDetailRepository _inventoryRequestDetailRepository;

        #endregion

        #region Builder

        public InventoryRequestDevolutionAdminService(IInventoryRequestDevolutionRepository InventoryRequestDevolutionRepository, IInventorySequenceDetailRepository SequenceRepository,
            IInventoryRequestDetailRepository inventoryRequestDetailRepository)
        {
            if (InventoryRequestDevolutionRepository == null)
            {
                throw new ArgumentException("Repositorio de InventoryRequestdevolutionRepository vacio");
            }
            if (SequenceRepository == null)
            {
                throw new ArgumentException("Repositorio de SequeceRepository vacio");
            }
            if (inventoryRequestDetailRepository == null)
            {
                throw new ArgumentException("Repositorio de inventoryRequestDetailRepository vacio");
            }
            _InventoryRequestDevolution = InventoryRequestDevolutionRepository;
            _SecuenseRepository = SequenceRepository;
            _inventoryRequestDetailRepository = inventoryRequestDetailRepository;
        }

        #endregion

        #region Methods


        #endregion
        
        public ActionResult<Domain.Entities.InventoryRequestDevolution> SaveInventoryRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestdevolution, AuditMessage audit, long idSecuence = 0, InventorySequence secuenceC = null)
        {
            if (inventoryRequestdevolution == null)
            {
                throw new ArgumentNullException("adjustmentConcept");
            }
            IUnitWork unitOfWork = _InventoryRequestDevolution.UnitWork;
            IUnitWork unitOfWorkSequense = _SecuenseRepository.UnitWork;
            IUnitWork unitOfWorkRquestDetail = _inventoryRequestDetailRepository.UnitWork;

             TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    InventorySequenceDetail seq = null;
                    if (inventoryRequestdevolution.Code == null || inventoryRequestdevolution.Code.Trim().Equals(string.Empty))
                    {
                        seq = this._SecuenseRepository.GetSequenseDById(Convert.ToInt32(idSecuence));
                        if (seq != null && seq.Id > 0 && seq.InventorySequence.Sequential)
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE))
                            {
                                inventoryRequestdevolution.Code = res;
                                seq.Next += 1;
                                this._SecuenseRepository.SaveEntity(seq);
                                unitOfWorkSequense.Commit();
                            }
                            else
                            {
                                transaction.Dispose();
                                return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.WARNING, Message =  "_Seq02_"  };
                            }
                        }
                        else
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.WARNING, Message =  "_Seq01_"  };
                        }
                    }

                    Domain.Entities.InventoryRequestDevolution auxInventoryRequestDevolution = null;
                    IndigoAuditSimpleEntity<Domain.Entities.InventoryRequestDevolution> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (inventoryRequestdevolution.Status == 1 && inventoryRequestdevolution.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        inventoryRequestdevolution.CreationUser = audit.CodeUser;
                        inventoryRequestdevolution.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else if (inventoryRequestdevolution.Status == 1 && inventoryRequestdevolution.ChangeTracker.State == Domain.Base.Entities.ObjectState.Modified)
                    {
                        auxInventoryRequestDevolution = inventoryRequestdevolution.OriginalValue;
                        inventoryRequestdevolution.ModificationUser = audit.CodeUser;
                        inventoryRequestdevolution.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    else if (inventoryRequestdevolution.ChangeTracker.State == Domain.Base.Entities.ObjectState.Unchanged && inventoryRequestdevolution.Status == 3)
                    {
                        auxInventoryRequestDevolution = inventoryRequestdevolution.OriginalValue;
                        inventoryRequestdevolution.AnnulmentUser = audit.CodeUser;
                        inventoryRequestdevolution.AnnulmentDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular;
                    }
                    else
                    {
                        if (inventoryRequestdevolution.Id > 0)
                        {
                            auxInventoryRequestDevolution = inventoryRequestdevolution.OriginalValue;
                            inventoryRequestdevolution.ModificationUser = audit.CodeUser;
                            inventoryRequestdevolution.ModificationDate = DateTime.Now;
                            inventoryRequestdevolution.ConfirmationUser = audit.CodeUser;
                            inventoryRequestdevolution.ConfirmationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Confirm;
                        }
                        else
                        {
                            inventoryRequestdevolution.CreationUser = audit.CodeUser;
                            inventoryRequestdevolution.CreationDate = DateTime.Now;
                            inventoryRequestdevolution.ConfirmationUser = audit.CodeUser;
                            inventoryRequestdevolution.ConfirmationDate = DateTime.Now;
                            status = Infrastructure.CrossCutting.Audit.Actions.Confirm;
                        }
                    }

                    _InventoryRequestDevolution.SaveEntity(inventoryRequestdevolution);
                    unitOfWork.Commit();
                    
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.InventoryRequestDevolution>(inventoryRequestdevolution, audit, status, auxInventoryRequestDevolution);
                    auditProcess.Execute();

                    if (inventoryRequestdevolution.Status == 2) ///Si se esta confirmando
                    {
                        if (inventoryRequestdevolution.InventoryRequestDevolutionDetail == null || inventoryRequestdevolution.InventoryRequestDevolutionDetail.Count == 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.WARNING, Message =  "No hay detalles"  };
                        }
                        if (inventoryRequestdevolution.InventoryRequestDevolutionDetail.Where(x => x.Quantity == 0).Count() > 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.WARNING, Message =  "Hay detalles con valores a devolver en cero"  };
                        }

                        var listErrors = new StringBuilder();
                        foreach (Domain.Entities.InventoryRequestDevolutionDetail devolutionDetail in inventoryRequestdevolution.InventoryRequestDevolutionDetail)
                        {
                            var requestDetail = _inventoryRequestDetailRepository.GetInventoryRequestDetailById(devolutionDetail.InventoryRequestDetailId);
                            if (requestDetail.OutstandingQuantity == 0)
                            {
                                listErrors.AppendLine("El código de la devolución " + devolutionDetail.RequestCode + " tiene cantidad pendiente en cero");
                                continue;
                            }
                            if (requestDetail.OutstandingQuantity < devolutionDetail.Quantity)
                            {
                                listErrors.AppendLine("La cantidad del código de la devolución " + devolutionDetail.RequestCode + " es mayor a la cantidad pendiente");
                                continue;
                            }
                            requestDetail.OutstandingQuantity -= devolutionDetail.Quantity;
                            requestDetail.MarkAsModified();
                            _inventoryRequestDetailRepository.SaveEntity(requestDetail);
                            unitOfWorkRquestDetail.Commit();

                        }

                        if (listErrors.Length > 0)
                        {
                            transaction.Dispose();
                            return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.WARNING, Message =  listErrors.ToString()  };
                        }

                    }

                    transaction.Complete();
                    return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = inventoryRequestdevolution };

                }
                catch (OptimisticConcurrencyException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.WARNING, Message =  "-999"  };
                }
                catch (UpdateException ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.WARNING, Message =  "-000"  };
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    unitOfWork.RollbackChanges();
                    return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.EXCEPTION, Message =  ex.Message  };
                }
            }

        }

        public ActionResult<Domain.Entities.InventoryRequestDevolution> SaveAndConfirmRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestdevolution, AuditMessage audit, long idSequence = 0, Infrastructure.CrossCutting.Audit.Actions action = Infrastructure.CrossCutting.Audit.Actions.Insert, InventorySequence sequenceC = null)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteInventoryRequestDevolution(Domain.Entities.InventoryRequestDevolution inventoryRequestDevolution, AuditMessage audit)
        {
            throw new NotImplementedException();
        }

        public ActionResult<Domain.Entities.InventoryRequestDevolution> ChangeStatusInventoryRequestDevolution(string code, byte status, AuditMessage audit)
        {
            throw new NotImplementedException();
        }

        public ActionResult<Domain.Entities.InventoryRequestDevolution> GetRequestDevolutionByCode(string code, AuditMessage audit)
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
                Domain.Entities.InventoryRequestDevolution inventoryRequestDevolution = _InventoryRequestDevolution.GetInventoryRequestDevolutionByCode(code);
                return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = inventoryRequestDevolution };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.InventoryRequestDevolution> { StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message } };
            }
        }

        public ActionResult<Domain.Entities.InventoryRequestDevolution> GetInventoryRequestDevolutionById(int id, AuditMessage audit)
        {
            throw new NotImplementedException();
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
                _InventoryRequestDevolution = null;
                _SecuenseRepository = null;
                _inventoryRequestDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
