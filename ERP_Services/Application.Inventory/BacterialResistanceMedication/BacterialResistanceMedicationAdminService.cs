//'************************************************************
//' Assembly         : Application.Inventory.BacterialResistanceMedication
//' Author           : Hector Rodriguez Rubiano
//' Created          : 06/04/2020
//'
//' Copyright        : (c) . All rights reserved.
//' About            : PBI8934
//'************************************************************

using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Transactions;

namespace Application.Inventory.BacterialResistanceMedication
{
    public class BacterialResistanceMedicationAdminService : IBacterialResistanceMedicationAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmBacterialResistanceMedication";
        private IBacterialResistanceMedicationRepository _BacterialResistanceMedicationRepository;
        #endregion

        #region Builder
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BacterialResistanceMedicationRepository"></param>
        /// <param name="sequenseRepository"></param>
        public BacterialResistanceMedicationAdminService(IBacterialResistanceMedicationRepository BacterialResistanceMedicationRepository)
        {
            if (BacterialResistanceMedicationRepository == null)
            {
                throw new ArgumentNullException("Repositorio de BacterialResistanceMedicationRepository vacio");
            }

            _BacterialResistanceMedicationRepository = BacterialResistanceMedicationRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaBRM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.BacterialResistanceMedication> SaveBacterialResistanceMedication(List<Domain.Entities.BacterialResistanceMedication> listaBRM, AuditMessage audit)
        {
            if (listaBRM == null)
            {
                throw new ArgumentNullException("BacterialResistanceMedication");
            }
            string xml = ConvertToXml(listaBRM);

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    var MessageResult = string.Empty;
                    MessageResult = ResourceManager.get_GetString("SaveMessage");
                    SP_SaveBacterialResistanceMedication_Result result = _BacterialResistanceMedicationRepository.SP_SaveBacterialResistanceMedication(xml, audit.CodeUser);

                    if (result.CodeResult == 999)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.BacterialResistanceMedication> { StateResult = false, Message = result.MessageResult, StatusCode = eStatusResult.WARNING };
                    }
                    scope.Complete();
                    return new ActionResult<Domain.Entities.BacterialResistanceMedication> { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = MessageResult };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.BacterialResistanceMedication> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence"), StatusCode = eStatusResult.EXCEPTION };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.BacterialResistanceMedication> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown"), StatusCode = eStatusResult.EXCEPTION };
                }
                catch (DbUpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    string message = ex.InnerException == null ? "" : ex.InnerException.InnerException.Message;
                    return new ActionResult<Domain.Entities.BacterialResistanceMedication> { StateResult = false, Message = "Revisar el Trigger de la Tabla. Error del Sistema: " + message, StatusCode = eStatusResult.EXCEPTION };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.BacterialResistanceMedication> { StateResult = false, Message = ex.Message, StatusCode = eStatusResult.EXCEPTION };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaBRM"></param>
        /// <returns></returns>
        private string ConvertToXml(List<Domain.Entities.BacterialResistanceMedication> listaBRM)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<Data>");

            foreach (var brm in listaBRM)
            {
                result.Append("<BRM>");
                result.Append(String.Format("<{0}>{1}</{0}>", "Id", brm.Id));
                result.Append(String.Format("<{0}>{1}</{0}>", "AtcId", brm.AtcId));
                result.Append(String.Format("<{0}>{1:dd/MM/yyyy hh:mm:ss}</{0}>", "StartDate", brm.StartDate));
                result.Append(String.Format("<{0}>{1:dd/MM/yyyy hh:mm:ss}</{0}>", "EndDate", brm.EndDate));
                result.Append(String.Format("<{0}>{1}</{0}>", "Observation", brm.Observation));
                if (brm.ChangeTracker.State == ObjectState.Added)
                    result.Append(String.Format("<{0}>{1}</{0}>", "StateBRM", 1));
                else if(brm.ChangeTracker.State == ObjectState.Deleted)
                    result.Append(String.Format("<{0}>{1}</{0}>", "StateBRM", 0));
                else if (brm.ChangeTracker.State == ObjectState.Modified)
                    result.Append(String.Format("<{0}>{1}</{0}>", "StateBRM", 2));
                result.Append("</BRM>");
            }
            result.Append("</Data>");

            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statesBRM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.BacterialResistanceMedication>> GetBacterialResistanceMedicationByStates(string statesBRM, AuditMessage audit)
        {
            if (statesBRM == string.Empty)
            {
                throw new ArgumentNullException("states");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                List<Domain.Entities.BacterialResistanceMedication> listaBRM = _BacterialResistanceMedicationRepository.GetBacterialResistanceMedicationByStates(statesBRM);
                return new ActionResult<List<Domain.Entities.BacterialResistanceMedication>> { StateResult = true, ObjectEmbbeded = listaBRM };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.BacterialResistanceMedication>> { StateResult = false, MessageResult = { ex.Message } };
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
                _BacterialResistanceMedicationRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}

