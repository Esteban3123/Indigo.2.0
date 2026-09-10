//'************************************************************
//' Assembly         : Application.Inventory.AntineoplasicoMedication
//' Author           : Hector Rodriguez Rubiano
//' Created          : 04/08/2020
//'
//' Copyright        : (c) . All rights reserved.
//' About            : PBI10044
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

namespace Application.Inventory.AntineoplasicoMedication
{
    public class AntineoplasicoMedicationAdminService : IAntineoplasicoMedicationAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmAntineoplasicoMedication";
        private IAntineoplasicoMedicationRepository _AntineoplasicoMedicationRepository;
        #endregion

        #region Builder
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AntineoplasicoMedicationRepository"></param>
        /// <param name="sequenseRepository"></param>
        public AntineoplasicoMedicationAdminService(IAntineoplasicoMedicationRepository AntineoplasicoMedicationRepository)
        {
            if (AntineoplasicoMedicationRepository == null)
            {
                throw new ArgumentNullException("Repositorio de AntineoplasicoMedicationRepository vacio");
            }

            _AntineoplasicoMedicationRepository = AntineoplasicoMedicationRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaAPM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AntineoplasicoMedication> SaveAntineoplasicoMedication(List<Domain.Entities.AntineoplasicoMedication> listaAPM, AuditMessage audit)
        {
            if (listaAPM == null)
            {
                throw new ArgumentNullException("AntineoplasicoMedication");
            }
            string xml = ConvertToXml(listaAPM);

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    var MessageResult = string.Empty;
                    MessageResult = ResourceManager.get_GetString("SaveMessage");
                    SP_SaveAntineoplasicoMedication_Result result = _AntineoplasicoMedicationRepository.SP_SaveAntineoplasicoMedication(xml, audit.CodeUser);

                    if (result.CodeResult == 999)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.AntineoplasicoMedication> { StateResult = false, Message = result.MessageResult, StatusCode = eStatusResult.WARNING };
                    }
                    scope.Complete();
                    return new ActionResult<Domain.Entities.AntineoplasicoMedication> { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = MessageResult };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.AntineoplasicoMedication> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence"), StatusCode = eStatusResult.EXCEPTION };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.AntineoplasicoMedication> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown"), StatusCode = eStatusResult.EXCEPTION };
                }
                catch (DbUpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    string message = ex.InnerException == null ? "" : ex.InnerException.InnerException.Message;
                    return new ActionResult<Domain.Entities.AntineoplasicoMedication> { StateResult = false, Message = "Revisar el Trigger de la Tabla. Error del Sistema: " + message, StatusCode = eStatusResult.EXCEPTION };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.AntineoplasicoMedication> { StateResult = false, Message = ex.Message, StatusCode = eStatusResult.EXCEPTION };
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaAPM"></param>
        /// <returns></returns>
        private string ConvertToXml(List<Domain.Entities.AntineoplasicoMedication> listaAPM)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<Data>");

            foreach (var apm in listaAPM)
            {
                result.Append("<APM>");
                result.Append(String.Format("<{0}>{1}</{0}>", "Id", apm.Id));
                result.Append(String.Format("<{0}>{1}</{0}>", "AtcId", apm.AtcId));
                result.Append(String.Format("<{0}>{1}</{0}>", "ClassificationId", apm.ClassificationId));
                if (apm.ChangeTracker.State == ObjectState.Added)
                    result.Append(String.Format("<{0}>{1}</{0}>", "StateAPM", 1));
                else if(apm.ChangeTracker.State == ObjectState.Deleted)
                    result.Append(String.Format("<{0}>{1}</{0}>", "StateAPM", 0));
                result.Append("</APM>");
            }
            result.Append("</Data>");

            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statesapm"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.AntineoplasicoMedication>> GetAntineoplasicoMedicationByStates(string statesapm, AuditMessage audit)
        {
            if (statesapm == string.Empty)
            {
                throw new ArgumentNullException("states");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                List<Domain.Entities.AntineoplasicoMedication> listaAPM = _AntineoplasicoMedicationRepository.GetAntineoplasicoMedicationByStates(statesapm);
                return new ActionResult<List<Domain.Entities.AntineoplasicoMedication>> { StateResult = true, ObjectEmbbeded = listaAPM };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<List<Domain.Entities.AntineoplasicoMedication>> { StateResult = false, MessageResult = { ex.Message } };
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
                _AntineoplasicoMedicationRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}

