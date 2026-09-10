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
using Application.Inventory.ProductGroup;
using Domain.Entities;
using System.Data;
using System.Transactions;

namespace Application.Inventory.Sequense
{
	public class InventorySequenceAdminService : IInventorySequenceAdminService
	{
		#region Variables
		private IInventorySequenceRepository _inventorySequenceRepository;
		private IInventorySequenceDetailRepository _inventorySequenceDetailRepository;
		#endregion        

		#region Builder
		/// <summary>
		/// inicia el repositorio de bancos
		/// </summary>
		/// <param name="bankRepository">Repositorio de bancos</param>
		/// <remarks></remarks>
		public InventorySequenceAdminService(IInventorySequenceRepository inventorySequenceRepository,IInventorySequenceDetailRepository inventorySequenceDetailRepository)
		{
			if ((inventorySequenceRepository == null))
			{
				throw new ArgumentNullException("Repositorio de inventorySequenceRepository vacio");
			}
			if ((inventorySequenceDetailRepository == null))
			{
				throw new ArgumentNullException("Repositorio de inventorySequenceDetailRepository vacio");
			}
			_inventorySequenceRepository = inventorySequenceRepository;
			_inventorySequenceDetailRepository = inventorySequenceDetailRepository;
		}
		#endregion

        public ActionResult SaveSequence(InventorySequence seq)
        {            
            IUnitWork UnitOfWorkC = this._inventorySequenceRepository.UnitWork;
            IUnitWork UnitOfWorkD = this._inventorySequenceDetailRepository.UnitWork;
            try {
	            if (seq.ChangeTracker.ObjectsRemovedFromCollectionProperties != null && seq.ChangeTracker.ObjectsRemovedFromCollectionProperties.Count > 0) {
		            while (seq.ChangeTracker.ObjectsRemovedFromCollectionProperties["InventorySequenceDetail"].Count > 0) {
                        this._inventorySequenceDetailRepository.DeleteEntity((InventorySequenceDetail)seq.ChangeTracker.ObjectsRemovedFromCollectionProperties["InventorySequenceDetail"][0]);
			            seq.ChangeTracker.ObjectsRemovedFromCollectionProperties["InventorySequenceDetail"].RemoveAt(0);
		            }
		            seq.MarkAsModified();
	            }
	            foreach (var d in seq.InventorySequenceDetail) {
		            if (d.ChangeTracker.State != ObjectState.Unchanged) {
			            this._inventorySequenceDetailRepository.SaveEntity(d);
		            }
	            }
	            this._inventorySequenceRepository.SaveEntity(seq);
	            UnitOfWorkD.Commit();
	            UnitOfWorkC.Commit();
	            return new ActionResult { StateResult = true };
            } catch (Exception ex) {
	            UnitOfWorkC.RollbackChanges();
	            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
	            return new ActionResult { StateResult = false };
            }
        }

		/// <summary>
		/// Obtiene la confirguracion de secuencia numerica asignada al frontal
		/// </summary>
		/// <param name="idForm"></param>
		/// <returns></returns>
		public Domain.Entities.InventorySequence GetSequenseByIdForm(string idForm, bool readUnCommit = false)
		{
			if (idForm == string .Empty)
			{
				throw new ArgumentNullException ("idForm");
			}

			try
			{
                if (readUnCommit == false)
                {
                    return this._inventorySequenceRepository.GetSequenseByIdForm(idForm);
                }
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    return this._inventorySequenceRepository.GetSequenseByIdForm(idForm);
                }
			}
			catch(Exception ex)
			{
				IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
				return null;
			}
		}

        /// <summary>
		/// Obtiene la confirguracion de secuencia numerica asignada al frontal
		/// </summary>
		/// <param name="idForm"></param>
		/// <returns></returns>
		public Domain.Entities.InventorySequenceDetail GetSequenseByPrefix(string prefix, int idsequence)
        {
            if (prefix == string.Empty)
            {
                throw new ArgumentNullException("prefix");
            }

            try
            {
                return this._inventorySequenceRepository.GetSequenseByPrefix(prefix, idsequence);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Obtiene el id de la secuencia por el tag del formulario
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
        public int GetCurrentSequenceByIdForm(string idForm, int operativeUnitId)
        {
            int _currentSequence = 0;
            var sequence = GetSequenseByIdForm(idForm);

            if (sequence.Scope.Equals("O")) _currentSequence = sequence.InventorySequenceDetail[0].Id;
            else if (sequence.Scope.Equals("OU") && sequence.InventorySequenceDetail.Any(m => m.IdOperatingUnit == operativeUnitId))
                _currentSequence = sequence.InventorySequenceDetail.FirstOrDefault(m => m.IdOperatingUnit == operativeUnitId).Id;

            return _currentSequence;
        }

        /// <summary>
        /// Obtiene un grupo de secuencias numericas por su id de configuracion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetNumericSequenseGroupById(int id)
		{
			try
			{
				IUnitWork unitOfWork = _inventorySequenceDetailRepository.UnitWork;
				InventorySequenceDetail seq = this._inventorySequenceDetailRepository.GetSequenseDById(id);
				if(seq.Id > 0)
				{
					if (!seq.InventorySequence.Sequential)
					{
						List<string> list = new List<string>();
						Int64 last = (seq.Next + seq.InventorySequence.Rate) - 1;
						for (Int64 i = seq.Next; i <= last; i++) 
						{
							var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, i);
							if (res != null && !res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)) 
							{
								list.Add(res);
							} else 
							{
								break; 
							}
						}
						if (list.Count > 0)
						{
							seq.Next = (seq.Next + list.Count);

							this._inventorySequenceDetailRepository.SaveEntity(seq);

							unitOfWork.Commit();
						}

						return list;
					}
					else 
					{
						return new List<string>();
					}
				}
				else
				{
					return new List<string>();
				}
			}
			catch(Exception ex)
			{
				IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
				return null;
			}
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
                _inventorySequenceRepository = null;
                _inventorySequenceDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
