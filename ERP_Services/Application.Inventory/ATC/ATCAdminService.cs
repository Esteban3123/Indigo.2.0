///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

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
using Infrastructure.CrossCutting.Resources;
using Domain.Entities.Service;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Transactions;
using Application.Events.Models;
using Application.Events.Serializers;
using Application.Events.Repository;
using Infrastructure.CrossCutting.Queue;

namespace Application.Inventory.ATC
{
    public class ATCAdminService : IATCAdminService
    {
        #region Variables
        private IATCRepository _atcRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        public ATCAdminService(IATCRepository atcRepository, IInventorySequenceDetailRepository sequenseRepository, IFactoryQueue FactoryQueue)
        {
            if ((atcRepository == null))
            {
                throw new ArgumentNullException("Repositorio de atcRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _atcRepository = atcRepository;
            _sequenseRepository = sequenseRepository;
            _factoryQueue = FactoryQueue;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Obtiene un atc por id
        /// </summary>
        public Domain.Entities.ATC GetATCById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException("Id");
            }
            try
            {
                return _atcRepository.GetATCById(id);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return null;
            }
        }

        /// <summary>
        /// Elimina un ATC
        /// </summary>
        public ActionResult DeleteATC(Domain.Entities.ATC atc, AuditMessage audit)
        {
            if (atc == null)
            {
                throw new ArgumentNullException("warehouse");
            }

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    Wrapper wrapperEvent = new Wrapper();
                    EventData eventData = wrapperEvent.GenerateWrapperEventData(atc, audit.CodeUser, "deleted", DittoSourceType.medicament);
                    SP_DeleteMedicament_Result result = _atcRepository.SP_DeleteMedicament(atc.Id);
                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.Message };
                    }

                    IIndigoQueue queue = _factoryQueue.CreateQueue();
                    queue.Publish(eventData);

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
        /// Actualiza el estado del atc
        /// </summary>
        public ActionResult<Domain.Entities.ATC> UpdateStateATC(string code, bool state, AuditMessage audit)
        {
            Domain.Entities.ATC atc = _atcRepository.GetATC(code);
            atc.Status = state;
            return SaveATC(atc, audit);
        }

        /// <summary>
        /// Obtiene un atc por codigo
        /// </summary>
        public ActionResult<Domain.Entities.ATC> GetATC(string code, AuditMessage audit)
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
                Domain.Entities.ATC atc = _atcRepository.GetATC(code);
                if (atc != null && atc.Id > 0)
                {
                    IndigoAuditSimpleEntity<Domain.Entities.ATC> auditProcess;
                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.ATC>(atc, audit, Infrastructure.CrossCutting.Audit.Actions.Print);
                    auditProcess.Execute();
                }
                return new ActionResult<Domain.Entities.ATC> { StateResult = true, ObjectEmbbeded = atc };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.ATC> { StateResult = false, MessageResult = new List<string> { ex.Message } };
            }
        }

        /// <summary>
        /// Guarda un ATC
        /// </summary>
        public ActionResult<Domain.Entities.ATC> SaveATC(Domain.Entities.ATC atc, AuditMessage audit, long idSecuence = 0)
        {
            if (atc == null) throw new ArgumentNullException("atc");

            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    string xml = ConvertToXml(atc);
                    SP_SaveATC_Result result = _atcRepository.SP_SaveATC(xml, audit.CodeUser);

                    if (result.CodeResult == 999)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.ATC> { StateResult = false, Message = result.MessageResult, StatusCode = eStatusResult.WARNING };
                    }
                    if (atc.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added) { atc.CreationUser = audit.CodeUser; atc.CreationDate = DateTime.Now; atc.ModificationUser = audit.CodeUser; atc.ModificationDate = DateTime.Now; } else { atc.ModificationUser = audit.CodeUser; atc.ModificationDate = DateTime.Now; }
                    TriggerEvent(atc, audit);
                    scope.Complete();
                    return new ActionResult<Domain.Entities.ATC> { StateResult = true, ObjectEmbbeded = atc };
                }
                catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.ATC> { StateResult = false, Message = ResourceManager.get_GetString("ErrorConcurrence"), StatusCode = eStatusResult.EXCEPTION };
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.ATC> { StateResult = false, Message = ResourceManager.get_GetString("ErrorUnknown"), StatusCode = eStatusResult.EXCEPTION };
                }
                catch (DbUpdateException ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    string message = ex.InnerException == null ? "" : ex.InnerException.InnerException.Message;
                    return new ActionResult<Domain.Entities.ATC> { StateResult = false, Message = "Revisar el Trigger de la Tabla. Error del Sistema: " + message, StatusCode = eStatusResult.EXCEPTION };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult<Domain.Entities.ATC> { StateResult = false, Message = ex.Message, StatusCode = eStatusResult.EXCEPTION };
                }
            }
        }

        /// <summary>
        /// Método que convierte la entidad de atc a xml
        /// </summary>
        /// <returns></returns>
        private string ConvertToXml(Domain.Entities.ATC atc)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<ATC>");

            result.Append("<Id>" + atc.Id + "</Id>");
            result.Append("<DCIId>" + atc.DCIId + "</DCIId>");
            result.Append("<Code>" + atc.Code + "</Code>");
            result.Append("<Name>" + atc.Name + "</Name>");
            result.Append("<AbbreviationName>" + atc.AbbreviationName + "</AbbreviationName>");
            result.Append("<AdministrationRouteId>" + atc.AdministrationRouteId + "</AdministrationRouteId>");
            result.Append("<PharmacologicalGroupId>" + atc.PharmacologicalGroupId + "</PharmacologicalGroupId>");
            result.Append("<Presentations>" + atc.Presentations + "</Presentations>");
            result.Append("<Concentration>" + atc.Concentration + "</Concentration>");
            result.Append("<InventoryRiskLevelId>" + atc.InventoryRiskLevelId + "</InventoryRiskLevelId>");
            result.Append("<StabilityMinimumHours>" + atc.StabilityMinimumHours + "</StabilityMinimumHours>");
            result.Append("<StabilityMaximumHours>" + atc.StabilityMaximumHours + "</StabilityMaximumHours>");
            result.Append("<FormulationType>" + atc.FormulationType + "</FormulationType>");
            result.Append("<Weight>" + atc.Weight + "</Weight>");
            result.Append("<WeightMeasureUnit>" + atc.WeightMeasureUnit + "</WeightMeasureUnit>");
            result.Append("<Volume>" + atc.Volume + "</Volume>");
            result.Append("<VolumeMeasureUnit>" + atc.VolumeMeasureUnit + "</VolumeMeasureUnit>");
            result.Append("<AdministrationUnitId>" + atc.AdministrationUnitId + "</AdministrationUnitId>");
            result.Append("<Warning>" + atc.Warning + "</Warning>");
            result.Append("<WarningHtml>" + atc.WarningHtml.ConvertToXmlText() + "</WarningHtml>");
            result.Append("<Dosage>" + atc.Dosage + "</Dosage>");
            result.Append("<DosageHtml>" + atc.DosageHtml.ConvertToXmlText() + "</DosageHtml>");
            result.Append("<Indications>" + atc.Indications + "</Indications>");
            result.Append("<IndicationsHtml>" + atc.IndicationsHtml.ConvertToXmlText() + "</IndicationsHtml>");
            result.Append("<ContraIndications>" + atc.ContraIndications + "</ContraIndications>");
            result.Append("<ContraIndicationsHtml>" + atc.ContraIndicationsHtml.ConvertToXmlText() + "</ContraIndicationsHtml>");
            result.Append("<Precautions>" + atc.Precautions + "</Precautions>");
            result.Append("<PrecautionsHtml>" + atc.PrecautionsHtml.ConvertToXmlText() + "</PrecautionsHtml>");
            result.Append("<AdverseReactions>" + atc.AdverseReactions + "</AdverseReactions>");
            result.Append("<AdverseReactionsHtml>" + atc.AdverseReactionsHtml.ConvertToXmlText() + "</AdverseReactionsHtml>");
            result.Append("<AutomaticCalculation>" + atc.AutomaticCalculation + "</AutomaticCalculation>");
            result.Append("<TransferSurplusProduct>" + atc.TransferSurplusProduct + "</TransferSurplusProduct>");
            result.Append("<DiluentProduct>" + atc.DiluentProduct + "</DiluentProduct>");
            result.Append("<SupplieProduct>" + atc.SupplieProduct + "</SupplieProduct>");
            result.Append("<JustificationForSpecialDrugs>" + atc.JustificationForSpecialDrugs + "</JustificationForSpecialDrugs>");
            result.Append("<JustificationOfInputs>" + atc.JustificationOfInputs + "</JustificationOfInputs>");
            result.Append("<IndicatorDrug>" + atc.IndicatorDrug + "</IndicatorDrug>");
            result.Append("<Status>" + atc.Status + "</Status>");
            result.Append("<ATCEntityId>" + atc.ATCEntityId + "</ATCEntityId>");
            result.Append("<POSProduct>" + atc.POSProduct + "</POSProduct>");
            result.Append("<AllPOSPathologies>" + atc.AllPOSPathologies + "</AllPOSPathologies>");
            result.Append("<BillingGroupNoPosId>" + atc.BillingGroupNoPosId + "</BillingGroupNoPosId>");
            result.Append("<ProductNPT>" + atc.ProductNPT + "</ProductNPT>");
            result.Append("<Osmolarity>" + atc.Osmolarity + "</Osmolarity>");
            result.Append("<Density>" + atc.Density + "</Density>");
            result.Append("<Consumption>" + atc.Consumption + "</Consumption>");
            result.Append("<Conditioned>" + atc.Conditioned + "</Conditioned>");
            result.Append("<UNIRS>" + atc.UNIRS + "</UNIRS>");
            result.Append("<Stability>" + atc.Stability + "</Stability>");
            result.Append("<Multidose>" + atc.Multidose + "</Multidose>");
            result.Append("<SuitableForReconstitution>" + atc.SuitableForReconstitution + "</SuitableForReconstitution>");
            result.Append("<Combined>" + atc.Combined + "</Combined>");
            result.Append("<ConcentrationQuantity>" + atc.ConcentrationQuantity + "</ConcentrationQuantity>");
            result.Append("<ConcentrationMeasureUnitId>" + atc.ConcentrationMeasureUnitId + "</ConcentrationMeasureUnitId>");
            result.Append("<TotalSubstanceConcentration>" + atc.TotalSubstanceConcentration + "</TotalSubstanceConcentration>");

            if (atc.ComponentType != null)
            {
                result.Append("<ComponentType>" + atc.ComponentType + "</ComponentType>");
            }

            if (atc.UPRUnitsId != null)
            {
                result.Append("<UPRUnitsId>" + atc.UPRUnitsId + "</UPRUnitsId>");
            }
            if (atc.PharmaceuticalFormId != null)
            {
                result.Append("<PharmaceuticalFormId>" + atc.PharmaceuticalFormId + "</PharmaceuticalFormId>");
            }
            if (atc.HasSupplieMedicine != null)
            {
                result.Append("<HasSupplieMedicine>" + atc.HasSupplieMedicine + "</HasSupplieMedicine>");
            }
            if (atc.Antibiotic != null) result.Append("<Antibiotic>" + atc.Antibiotic + "</Antibiotic>");
            if (atc.RequireMedicalBoard != null) result.Append("<RequireMedicalBoard>" + atc.RequireMedicalBoard + "</RequireMedicalBoard>");
            if (atc.HighCost != null) result.Append("<HighCost>" + atc.HighCost + "</HighCost>");
            result.Append("<DefineProfessional>" + atc.DefineProfessional.GetValueOrDefault() + "</DefineProfessional>");
            if (atc.DefineProfessional.GetValueOrDefault())
            {
                result.Append("<ClinicalJustification>" + atc.ClinicalJustification + "</ClinicalJustification>");
            }


            if (atc.POSProduct == true && atc.ProductPathologies != null && atc.ProductPathologies.POSPathologies.Count() > 0)
            {
                byte _state = 0;
                foreach (var item in atc.ProductPathologies.POSPathologies)
                {
                    _state = 0;
                    switch (item.ChangeTracker.State)
                    {
                        case ObjectState.Unchanged:
                            break;
                        case ObjectState.Added:
                            break;
                        case ObjectState.Modified:
                            _state = 2;
                            break;
                        case ObjectState.Deleted:
                            _state = 1;
                            break;
                        default:
                            break;
                    }
                    result.Append("<ProductPathologies>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<DiagnosticId>" + item.DiagnosticId + "</DiagnosticId>");
                    result.Append("<DiagnosticCode>" + item.DiagnosticCode + "</DiagnosticCode>");
                    result.Append("<MedicamentId>" + item.MedicamentId + "</MedicamentId>");
                    result.Append("<MinimumAge>" + item.MinimumAge + "</MinimumAge>");
                    result.Append("<MaximumAge>" + item.MaximumAge + "</MaximumAge>");
                    result.Append("<AgeMeasure>" + item.AgeMeasure + "</AgeMeasure>");
                    result.Append("<IsDelete>" + _state + "</IsDelete>");
                    result.Append("</ProductPathologies>");
                }
            }

            if (atc.ATCAdministrationRoute != null && atc.ATCAdministrationRoute.Count > 0)
            {
                foreach (var d in atc.ATCAdministrationRoute)
                {
                    result.Append("<ATCAR>");
                    result.Append("<Id>" + d.Id + "</Id>");
                    result.Append("<ATCId>" + d.ATCId + "</ATCId>");
                    result.Append("<ARId>" + d.AdministrationRouteId + "</ARId>");
                    result.Append("<IsDelete>" + ((d.ChangeTracker.State == ObjectState.Deleted) ? 1 : 0) + "</IsDelete>");
                    result.Append("</ATCAR>");
                }
            }

            if (atc.ATCConcentrationByDCI != null && atc.ATCConcentrationByDCI.Count > 0)
            {
                foreach (var a in atc.ATCConcentrationByDCI)
                {
                    result.Append("<ATCConcentrationByDCI>");
                    result.Append("<Id>" + a.Id + "</Id>");
                    result.Append("<AtcId>" + a.AtcId + "</AtcId>");
                    result.Append("<DCIId>" + a.DCIId + "</DCIId>");
                    result.Append("<Concentration>" + a.Concentration + "</Concentration>");
                    result.Append("<ConcentrationMeasureUnitId>" + a.ConcentrationMeasureUnitId + "</ConcentrationMeasureUnitId>");
                    result.Append("<ChangeTracker>" + a.ChangeTracker.State.ToString() + "</ChangeTracker>");
                    result.Append("</ATCConcentrationByDCI>");
                }
            }

            if (atc.ATCClinicalData != null && atc.ATCClinicalData.Count > 0)
            {
                foreach (var item in atc.ATCClinicalData)
                {
                    result.Append("<ATCClinicalData>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<DataType>" + item.DataType + "</DataType>");
                    result.Append("<Description>" + item.Description + "</Description>");
                    result.Append("<ControlLaboratoryId>" + item.ControlLaboratoryId + "</ControlLaboratoryId>");
                    result.Append("<TimeRequest>" + item.TimeRequest + "</TimeRequest>");
                    result.Append("<Frequency>" + item.Frequency + "</Frequency>");
                    result.Append("<DiagnosisId>" + item.DiagnosisId + "</DiagnosisId>");
                    result.Append("<ATCId>" + item.ATCId + "</ATCId>");
                    result.Append("<ChangeTracker>" + item.ChangeTracker.State.ToString() + "</ChangeTracker>");
                    result.Append("</ATCClinicalData>");
                }
            }

            if (atc.HasSupplieMedicine == true && atc.RelatedSupplieMedicine != null && atc.RelatedSupplieMedicine.Count() > 0)
            {
                foreach (var item in atc.RelatedSupplieMedicine)
                {
                    result.Append("<RSM>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<ATCId>" + item.ATCId + "</ATCId>");
                    result.Append("<ItemType>" + item.ItemType + "</ItemType>");
                    result.Append("<SourceId>" + item.SourceId + "</SourceId>");
                    result.Append("<IsDelete>" + ((item.ChangeTracker.State == ObjectState.Deleted) ? 1 : 0) + "</IsDelete>");
                    result.Append("</RSM>");
                }
            }

            if (atc.TechnicalSheet != null && atc.TechnicalSheet.Count() > 0)
            {
                foreach (var item in atc.TechnicalSheet)
                {
                    result.Append("<TechnicalSheet>");
                    result.Append("<Id>" + item.Id + "</Id>");
                    result.Append("<ATCId>" + item.ATCId + "</ATCId>");
                    result.Append("<DiagnosticId>" + item.DiagnosticId + "</DiagnosticId>");
                    result.Append("<TechnicalSheetType>" + item.TechnicalSheetType + "</TechnicalSheetType>");
                    result.Append("<Comment>" + item.Comment + "</Comment>");
                    result.Append("<ChangeTracker>" + item.ChangeTracker.State.ToString() + "</ChangeTracker>");
                    result.Append("</TechnicalSheet>");
                }
            }
            result.Append("</ATC>");
            return result.ToString();
        }
        #endregion



        #region Events
        public void TriggerEvent(Domain.Entities.ATC atc, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = atc.ChangeTracker.State.ToString().ToLower();
            if (atc.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(atc, audit.CodeUser, ChangeTracker, DittoSourceType.medicament);
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
                _atcRepository = null;
                _sequenseRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}