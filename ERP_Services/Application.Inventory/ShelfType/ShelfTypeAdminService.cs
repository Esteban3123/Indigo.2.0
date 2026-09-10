//'************************************************************
//' Assembly         : Domain.Inventory.ShelfType
//' Author           : Judy Andrea Díaz Reyes
//' Created          : 27/05/2019
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Base;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using Application.Inventory.ShelfType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Inventory.ShelfType
{
    public class ShelfTypeAdminService : IShelfTypeAdminService
	{
        #region Variables
        private IShelfTypeRepository _shelfTypeRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private const string FORM_NAME = "FrmShelfType";
		#endregion

		#region Builder
		/// <summary>
		/// inicia el repositorio de tipo de estante
		/// </summary>
		/// <param name="shelfTypeRepository">Repositorio de tipo de estante</param>
		/// <remarks></remarks>
		public ShelfTypeAdminService(IShelfTypeRepository shelfTypeRepository, IInventorySequenceDetailRepository sequenseRepository)
        {
            if ((shelfTypeRepository == null))
            {
                throw new ArgumentNullException("Repositorio de shelfTypeRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
			_shelfTypeRepository = shelfTypeRepository;
            _sequenseRepository = sequenseRepository;
        }
		#endregion

		#region Methods

		/// <summary>
		/// Obtiene todos los tipos de estante
		/// </summary>
		/// <param name="audit"></param>
		/// <returns></returns>
		public List<Domain.Entities.ShelfType> ListAllShelfType(AuditMessage audit)
		{
			if (audit == null)
			{
				throw new ArgumentNullException("audit");
			}
			try
			{
				List<Domain.Entities.ShelfType> shelfType = _shelfTypeRepository.ListAllShelfType();
				return shelfType;
			}
			catch (Exception ex)
			{
				IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
				return null;
			}
		}

		/// <summary>
		/// Guarda o actualiza un tipo de estante
		/// </summary>
		/// <param name="shelfType"></param>
		/// <param name="audit"></param>
		/// <param name="idSecuence"></param>
		/// <returns></returns>
		public ActionResult<Domain.Entities.ShelfType> SaveShelfType(Domain.Entities.ShelfType shelfType, AuditMessage audit, long idSecuence = 0)
        {
            if (shelfType == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._shelfTypeRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(shelfType.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
								shelfType.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.ShelfType> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), shelfType.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.ShelfType> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }

                    Domain.Entities.ShelfType auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.ShelfType> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;

                    if (shelfType.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        auxObjEntity = shelfType.OriginalValue;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }

                    var resultSave = this._shelfTypeRepository.SaveShelfType(shelfType.Code, shelfType.Description, (Decimal)shelfType.Large, (Decimal)shelfType.Wide, (Decimal)shelfType.Deep, (Decimal)shelfType.PartitionXDeep, (Decimal)shelfType.Partitions, (Decimal)shelfType.LocationXPartition, shelfType.State, audit.CodeUser);

                    if (resultSave.CodeMessage == 0)
                    {
                        sequenseUnitOfWork.Commit();
                        auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ShelfType>(shelfType, audit, status, auxObjEntity);
                        auditProcess.Execute();
						//Se marca la entidad como sin cambios
						shelfType.MarkAsUnchanged();
						scope.Complete();
                        return new ActionResult<Domain.Entities.ShelfType> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = shelfType, Message = resultSave.Message };
                    }
                    else
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.ShelfType> { StateResult = false, StatusCode = eStatusResult.WARNING, ObjectEmbbeded = shelfType, Message = resultSave.Message };
                    }

                       
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.ShelfType> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ShelfType> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Elimina un
        /// </summary>
        /// <param name="measureUnit"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteShelfType(Domain.Entities.ShelfType shelfType, AuditMessage audit)
        {
            if (shelfType == null)
            {
                throw new ArgumentNullException("measureUnit");
            }

            IUnitWork unitOfWork = this._shelfTypeRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
					shelfType.ModificationUser = audit.CodeUser;
					shelfType.ModificationDate = DateTime.Now;
                    Infrastructure.CrossCutting.Audit.Actions status = Infrastructure.CrossCutting.Audit.Actions.Delete;
                    var auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ShelfType>(shelfType, audit, status);

					shelfType.MarkAsDeleted();
                    this._shelfTypeRepository.SaveEntity(shelfType);
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
        public ActionResult<Domain.Entities.ShelfType> UpdateStateShelfType(string code, bool state, AuditMessage audit)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.ShelfType shelfType = this._shelfTypeRepository.GetShelfType(code.Trim());
                if (shelfType != null && shelfType.Id > 0)
                {
					shelfType.State = state;
                }
                var result = this.SaveShelfType(shelfType, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ShelfType> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }


        }

        /// <summary>
        /// Obtiene un tipo de estante por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ShelfType> GetShelfType(string code, AuditMessage audit)
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
                Domain.Entities.ShelfType shelfType = _shelfTypeRepository.GetShelfType(code);
                return new ActionResult<Domain.Entities.ShelfType> { StateResult = true, ObjectEmbbeded = shelfType };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ShelfType> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un tipo de estante por id
        /// </summary>
        /// <param name="idProductGroup"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.ShelfType> GetShelfTypeById(int id, AuditMessage audit)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Code");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.ShelfType shelfType = _shelfTypeRepository.GetShelfTypeById(id);
                return new ActionResult<Domain.Entities.ShelfType> { StateResult = true, ObjectEmbbeded = null };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ShelfType> { StateResult = false, MessageResult = { ex.Message } };
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
				_shelfTypeRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
