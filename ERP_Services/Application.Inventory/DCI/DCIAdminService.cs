//'************************************************************
//' Assembly         : Domain.Inventory.IGroupRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 22/09/2014
//'
//' Copyright        : (c) . All rights reserved.
//'************************************************************

using Application.Base;
using Application.Events.Serializers;
using Domain.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Queue;
using Infrastructure.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Application.Inventory.DCI
{
    public class DCIAdminService : IDCIAdminService
    {
        #region Variables
        private const string FORM_NAME = "FrmDCI";
        private IDCIRepository _dciRepository;
        private IInventorySequenceDetailRepository _sequenseRepository;
        private IDrugInteractionRepository _drugInteractionRepository;
        private readonly IHighRiskDrugsRepository _highRiskDrugsRepository;
        private IFactoryQueue _factoryQueue;
        #endregion

        #region Builder
        /// <summary>
        /// inicia el repositorio de bancos
        /// </summary>
        /// <param name="bankRepository">Repositorio de bancos</param>
        /// <remarks></remarks>
        public DCIAdminService(IDCIRepository dciRepository, IInventorySequenceDetailRepository sequenseRepository, IDrugInteractionRepository drugInteractionRepository,
            IHighRiskDrugsRepository highRiskDrugsRepository, IFactoryQueue factoryQueue)
        {
            if ((dciRepository == null))
            {
                throw new ArgumentNullException("Repositorio de dciRepository vacio");
            }
            if ((sequenseRepository == null))
            {
                throw new ArgumentNullException("Repositorio de sequenseRepository vacio");
            }
            _dciRepository = dciRepository;
            _sequenseRepository = sequenseRepository;
            _drugInteractionRepository = drugInteractionRepository;
            _highRiskDrugsRepository = highRiskDrugsRepository;
            _factoryQueue = factoryQueue;
        }
        #endregion

        #region Methods


        public IEnumerable<WarningHighRiskDrugModel> GetHighRiskDrugsByAtcCode(List<string> atcCodes)
        {
            try
            {

                IEnumerable<WarningHighRiskDrugModel> data =  Enumerable.Empty<WarningHighRiskDrugModel>();
                if (atcCodes.Count>0)
                {

                     data = _highRiskDrugsRepository.ExecuteQuery<WarningHighRiskDrugModel>($@"SELECT DISTINCT hd.DCIId
	                    , a.Id AS ATCId
	                    , a.Code AS ATCCode
	                    , a.[Name] AS ATCName
	                    , r.[Name] AS RiskName
	                    , hd.Observation AS Alert
	                    , dci.TypeWarning AS IsRestrictive
                    FROM Inventory.HighRiskDrugs hd
                    JOIN Inventory.DCI dci ON hd.DCIId = dci.Id
                    JOIN Inventory.InventoryRiskLevel r ON hd.InventoryRiskLevelId = r.Id
                    JOIN Inventory.ATC a ON a.DCIId = hd.DCIId
                    WHERE a.Code IN ({string.Join(",", atcCodes.Select(m => $"'{m}'").ToArray())})");

                }
                   

                return data;
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                throw ex;
            }
        }

        /// <summary>
        /// Guarda o actualiza un DCI
        /// </summary>
        /// <param name="DCI"></param>
        /// <param name="audit"></param>
        /// <param name="idSecuence"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DCI> SaveDCI(Domain.Entities.DCI DCI, List<DrugInteraction> ListDeleteDrugInteraction, List<DrugActive> ListDeleteDrugActive, List<LethalDoseLimits> ListDeleteLethalDoseLimits, List<RisksDescription> ListDeleteRisksDescription, List<DCIRiskFactors> ListDeleteDCIRiskFactors, SessionValues audit, long idSecuence = 0)
        {

            if (DCI == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._dciRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    Events.Serializers.Wrapper Event = new Events.Serializers.Wrapper();
                    AuditMessage auditMessage = new AuditMessage();
                    auditMessage.CodeUser = audit.AuditMessageWcf.CodeUser;

                    if (DCI.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        DCI.CreationUser = audit.AuditMessageWcf.CodeUser;
                        DCI.CreationDate = DateTime.Now;
                    }
                    else
                    {
                        DCI.ModificationUser = audit.AuditMessageWcf.CodeUser;
                        DCI.ModificationDate = DateTime.Now;
                        DCI.ChangeTracker.State = ObjectState.Modified;
                    }


                    List<String> ListString = ConvertToXmlListDelete(ListDeleteDrugInteraction);
                    List<String> ListStringDrugActive = ConvertToXmlListDeleteDrugActive(ListDeleteDrugActive);
                    List<String> ListStringLethalDoseLimits = ConvertToXmlListDeleteLethalDoseLimits(ListDeleteLethalDoseLimits);
                    List<String> ListStringRisksDescription = ConvertToXmlListDeleteRisksDescription(ListDeleteRisksDescription);
                    List<String> ListStringDCIRiskFactors = ConvertToXmlListDeleteDCIRiskFactors(ListDeleteDCIRiskFactors);
                    String EntityXml = ConvertToXmlDCI(DCI);

                    //DCIATCEntityList
                    List<DCIATCEntity> DCIATCEntityList = new List<DCIATCEntity>();
                    if (DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("DCIATCEntity")) { DCIATCEntityList = (from Domain.Entities.DCIATCEntity e in DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "DCIATCEntity").ToList()[0].Value select e).ToList(); }
                    if (DCI.DCIATCEntity != null || DCI.DCIATCEntity.Count > 0) { (from DCIATCEntity e in DCI.DCIATCEntity select e).ToList().ForEach(x => { DCIATCEntityList.Add(x); }); }

                    //DrugInteraction1List
                    List<DrugInteraction> DrugInteraction1List = new List<DrugInteraction>();
                    if (DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("DrugInteraction1")) { DrugInteraction1List = (from Domain.Entities.DrugInteraction e in DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(x => x.Key == "DrugInteraction1").ToList()[0].Value select e).ToList(); }

                    var resultSave = this._dciRepository.SaveDCI(EntityXml, ListString, ListStringDrugActive, ListStringLethalDoseLimits, ListStringRisksDescription, ListStringDCIRiskFactors, audit.IndigoOperatingUnitId, audit.AuditMessageWcf.CodeUser);
                    if (DCIATCEntityList.Count > 0) { (from x in DCIATCEntityList select x).ToList().ForEach(x => { DCI.DCIATCEntity.Add(x); }); }

                    if ( resultSave?.CodeMessage != 0)
                    {
                        scope.Dispose();
                        return new ActionResult<Domain.Entities.DCI> { StateResult = false, StatusCode = eStatusResult.WARNING, ObjectEmbbeded = DCI, Message = resultSave?.Message };
                    }

                    TriggerEvent(DCI, auditMessage);

                    scope.Complete();
                    return new ActionResult<Domain.Entities.DCI> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = DCI, Message = resultSave.Message };
                   
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.DCI> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", audit);
                return new ActionResult<Domain.Entities.DCI> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }

        }

        /// <summary>
        /// Elimina un DCI
        /// </summary>
        /// <param name="DCI"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult DeleteDCI(Domain.Entities.DCI DCI, AuditMessage audit)
        {
            if (DCI == null)
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
                    SP_DeleteDCI_Result result = _dciRepository.SP_DeleteDCI(DCI.Id);
                    if (result.CodeMessage == 999)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.Message };
                    }

                    DCI.MarkAsDeleted();
                    TriggerEvent(DCI, audit);

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
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DCI> ChangeStateDCI(string code, bool state, AuditMessage audit)
        {
            //Domain.Entities.DCI dci = _dciRepository.GetDCI(code);
            //dci.Status = state;
            //return SaveDCI(dci, audit);


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
                Domain.Entities.DCI dci = this._dciRepository.GetDCI(code.Trim());
                if (dci != null && dci.Id > 0)
                {
                    dci.Status = state;
                }
                var result = this.SaveStateDCI(dci, audit);
                if (result.StatusCode == eStatusResult.SUCCESS)
                {
                    result.Message = Infrastructure.CrossCutting.Resources.ResourceManager.get_GetString("UpdateState");
                }
                return result;
                throw new ArgumentNullException("audit");
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.DCI> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        /// <summary>
        /// Obtiene una interacción de medicamento por el Id del DCI padre
        /// </summary>
        /// <param name="ParentDCIid"></param>
        /// <returns></returns>
        public List<Domain.Entities.DrugInteraction> GetDrugInteractionByDCIParentId(int ParentDCIid)
        {
            if (ParentDCIid == 0)
            {
                throw new ArgumentNullException("ParentDCIId");
            }
            try
            {
                return _drugInteractionRepository.GetDrugInteractionByDCIParentId(ParentDCIid);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<DrugInteraction>();
            }
        }

        /// <summary>
        /// Obtiene un dci por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DCI> GetDCI(string code, AuditMessage audit)
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
                Domain.Entities.DCI dci = _dciRepository.GetDCI(code);
                return new ActionResult<Domain.Entities.DCI> { StateResult = true, ObjectEmbbeded = dci };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.DCI> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        /// <summary>
        /// Obtiene un dci por id
        /// </summary>
        /// <param name="idDCI"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public ActionResult<Domain.Entities.DCI> GetDCIById(int idDCI, AuditMessage audit)
        {
            if (idDCI == 0)
            {
                throw new ArgumentNullException("idDCI");
            }
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }
            try
            {
                Domain.Entities.DCI dCI = _dciRepository.GetDCIById(idDCI);
                return new ActionResult<Domain.Entities.DCI> { StateResult = true, ObjectEmbbeded = dCI };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.DCI> { StateResult = false, MessageResult = { ex.Message } };
            }
        }

        public List<String> ConvertToXmlListDelete(List<Domain.Entities.DrugInteraction> ListDrugInteraction)
        {
            StringBuilder builder = new StringBuilder();
            List<String> ListString = new List<String>();

            if (ListDrugInteraction != null && ListDrugInteraction.Count > 0)
            {
                foreach (Domain.Entities.DrugInteraction item in ListDrugInteraction)
                {
                    builder.Append("<ListDeleteMedicaments>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("</ListDeleteMedicaments>");

                }
                ListString.Add(builder.ToString());
                return ListString;
            }
            else
            {
                return null;
            }

        }

        public List<String> ConvertToXmlListDeleteDrugActive(List<Domain.Entities.DrugActive> ListDeleteDrugActive)
        {
            StringBuilder builder = new StringBuilder();
            List<String> ListString = new List<String>();

            if (ListDeleteDrugActive != null && ListDeleteDrugActive.Count > 0)
            {
                foreach (DrugActive item in ListDeleteDrugActive)
                {
                    builder.Append("<ListDeleteDrugActive>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("</ListDeleteDrugActive>");

                }

                ListString.Add(builder.ToString());
                return ListString;
            }
            else
            {
                return null;
            }
        }

        public List<String> ConvertToXmlListDeleteLethalDoseLimits(List<LethalDoseLimits> ListDeleteLethalDoseLimits)
        {
            StringBuilder builder = new StringBuilder();
            List<String> ListString = new List<String>();

            if (ListDeleteLethalDoseLimits != null && ListDeleteLethalDoseLimits.Count > 0)
            {
                foreach (LethalDoseLimits item in ListDeleteLethalDoseLimits)
                {
                    builder.Append("<ListDeleteLethalDoseLimits>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("</ListDeleteLethalDoseLimits>");

                }

                ListString.Add(builder.ToString());
                return ListString;
            }
            else
            {
                return null;
            }
        }

        public List<String> ConvertToXmlListDeleteRisksDescription(List<RisksDescription> ListDeleteRisksDescription)
        {
            StringBuilder builder = new StringBuilder();
            List<String> ListString = new List<String>();

            if (ListDeleteRisksDescription != null && ListDeleteRisksDescription.Count > 0)
            {
                foreach (RisksDescription item in ListDeleteRisksDescription)
                {
                    builder.Append("<ListDeleteRisksDescription>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("</ListDeleteRisksDescription>");

                }

                ListString.Add(builder.ToString());
                return ListString;
            }
            else
            {
                return null;
            }
        }

        public List<String> ConvertToXmlListDeleteDCIRiskFactors(List<DCIRiskFactors> ListDeleteDCIRiskFactors)
        {
            StringBuilder builder = new StringBuilder();
            List<String> ListString = new List<String>();

            if (ListDeleteDCIRiskFactors != null && ListDeleteDCIRiskFactors.Count > 0)
            {
                foreach (DCIRiskFactors item in ListDeleteDCIRiskFactors)
                {
                    builder.Append("<ListDeleteDCIRiskFactors>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("</ListDeleteDCIRiskFactors>");

                }

                ListString.Add(builder.ToString());
                return ListString;
            }
            else
            {
                return null;
            }
        }

        public String ConvertToXmlDCI(Domain.Entities.DCI DCI)
        {
            StringBuilder builder = new StringBuilder();
            Int32 contTempId = 1;

            builder.Append("<DCI>");
            builder.Append("<Id>" + DCI.Id + "</Id>");
            builder.Append("<Code>" + DCI.Code + "</Code>");
            builder.Append("<Name>" + DCI.Name + "</Name>");
            builder.Append("<Status>" + DCI.Status + "</Status>");
            builder.Append("<Combined>" + DCI.Combined + "</Combined>");            
            builder.Append("<TypeWarning>" + DCI.TypeWarning + "</TypeWarning>");

            if (DCI.DrugInteraction1 != null && DCI.DrugInteraction1.Any())
            {
                foreach (DrugInteraction item in DCI.DrugInteraction1)
                {
                    if (!item.isInherited)
                    {
                        builder.Append("<DrugInteraction>");
                        builder.Append($"<Id>{item.Id}</Id>");
                        builder.Append($"<ATCEntityId>{item.ATCEntityId}</ATCEntityId>");
                        builder.Append($"<RiskLevel>{item.RiskLevel}</RiskLevel>");
                        builder.Append($"<Description>{item.Description}</Description>");
                        builder.Append($"<TempId>{contTempId}</TempId>");
                        builder.Append("</DrugInteraction>");

                        contTempId += 1;
                    }
                }
            }

            if (DCI.DCIATCEntity != null && DCI.DCIATCEntity.Any())
            {
                foreach (DCIATCEntity item in DCI.DCIATCEntity)
                {
                    builder.AppendFormat("<DCIATCEntity>");
                    builder.AppendFormat("<Id>" + item.Id + "</Id>");
                    builder.AppendFormat("<DCIId>" + item.IdDCI + "</DCIId>");
                    builder.AppendFormat("<ATCEntityId>" + item.IdATCEntity + "</ATCEntityId>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</DCIATCEntity>");
                }
            }

            if (DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("DCIATCEntity"))
            {
                var deletes = DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties["DCIATCEntity"].ToList();

                foreach (DCIATCEntity item in deletes)
                {
                    builder.AppendFormat("<DCIATCEntity>");
                    builder.AppendFormat("<Id>" + item.Id + "</Id>");
                    builder.AppendFormat("<DCIId>" + item.IdDCI + "</DCIId>");
                    builder.AppendFormat("<ATCEntityId>" + item.IdATCEntity + "</ATCEntityId>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</DCIATCEntity>");
                }
            }

            if (DCI.DrugActive1 != null && DCI.DrugActive1.Any())
            {
                foreach (DrugActive item in DCI.DrugActive1)
                {
                    builder.Append("<DrugActive>");
                    builder.Append($"<Id>{item.Id}</Id>");
                    builder.Append($"<ATCEntityId>{item.ATCEntityId}</ATCEntityId>");
                    builder.Append($"<ChangeTracker>{item.ChangeTracker.State}</ChangeTracker>");
                    builder.Append("</DrugActive>");
                }
            }

            if (DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("DrugActive1"))
            {
                var deletes = DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties["DrugActive1"].ToList();

                foreach (DrugActive item in deletes)
                {
                    builder.Append("<DrugActive>");
                    builder.Append($"<Id>{item.Id}</Id>");
                    builder.Append($"<ATCEntityId>{item.ATCEntityId}</ATCEntityId>");
                    builder.Append($"<ChangeTracker>{item.ChangeTracker.State}</ChangeTracker>");
                    builder.Append("</DrugActive>");
                }
            }

            if (DCI.HighRiskDrugs != null && DCI.HighRiskDrugs.Count > 0)
            {
                foreach (Domain.Entities.HighRiskDrugs item in DCI.HighRiskDrugs)
                {
                    builder.AppendFormat("<HighRiskDrugs>");
                    builder.AppendFormat("<Id>" + item.Id + "</Id>");
                    builder.AppendFormat("<InventoryRiskLevelId>" + item.InventoryRiskLevelId + "</InventoryRiskLevelId>");
                    builder.AppendFormat("<Observation>" + item.Observation + "</Observation>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</HighRiskDrugs>");
                }
            }

            if (DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("HighRiskDrugs"))
            {
                var deletes = DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties["HighRiskDrugs"].ToList();

                foreach (Domain.Entities.HighRiskDrugs item in deletes)
                {
                    builder.AppendFormat("<HighRiskDrugs>");
                    builder.AppendFormat("<Id>" + item.Id + "</Id>");
                    builder.AppendFormat("<InventoryRiskLevelId>" + item.InventoryRiskLevelId + "</InventoryRiskLevelId>");
                    builder.AppendFormat("<Observation>" + item.Observation + "</Observation>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</HighRiskDrugs>");
                }
            }

            if (DCI.IHPARAMDCIs != null && DCI.IHPARAMDCIs.Any())
            {
                foreach (IHPARAMDCI item in DCI.IHPARAMDCIs)
                {
                    builder.AppendFormat("<IHPARAMDCIs>");
                    builder.AppendFormat("<ID>" + item.ID + "</ID>");
                    builder.AppendFormat("<CODDCIMED>" + item.CODDCIMED + "</CODDCIMED>");
                    builder.AppendFormat("<TIPO>" + item.TIPO + "</TIPO>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</IHPARAMDCIs>");
                }
            }

            if (DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("IHPARAMDCIs"))
            {
                var deletes = DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties["IHPARAMDCIs"].ToList();

                foreach (IHPARAMDCI item in deletes)
                {
                    builder.AppendFormat("<IHPARAMDCIs>");
                    builder.AppendFormat("<ID>" + item.ID + "</ID>");
                    builder.AppendFormat("<CODDCIMED>" + item.CODDCIMED + "</CODDCIMED>");
                    builder.AppendFormat("<TIPO>" + item.TIPO + "</TIPO>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</IHPARAMDCIs>");
                }
            }

            if (DCI.LethalDoseLimits != null && DCI.LethalDoseLimits.Any())
            {
                foreach (LethalDoseLimits item in DCI.LethalDoseLimits)
                {
                    builder.Append("<LethalDoseLimits>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("<StartAge>" + item.StartAge + "</StartAge>");
                    builder.Append("<StartAgeUnit>" + item.StartAgeUnit + "</StartAgeUnit>");
                    builder.Append("<EndAge>" + item.EndAge+ "</EndAge>");
                    builder.Append("<EndAgeUnit>" + item.EndAgeUnit+ "</EndAgeUnit>");
                    builder.Append("<StartWeight>" + item.StartWeight+ "</StartWeight>");
                    builder.Append("<StartWeightUnit>" + item.StartWeightUnit + "</StartWeightUnit>");
                    builder.Append("<EndWeight>" + item.EndWeight + "</EndWeight>");
                    builder.Append("<EndWeightUnit>" + item.EndWeightUnit+ "</EndWeightUnit>");
                    builder.Append("<MaxDoseConcentration>" + item.MaxDoseConcentration + "</MaxDoseConcentration>");
                    builder.Append("<MaxDoseConcentrationUnitId>" + item.MaxDoseConcentrationUnitId + "</MaxDoseConcentrationUnitId>");
                    builder.Append("<Max24HourConcentration>" + item.Max24HourConcentration+ "</Max24HourConcentration>");
                    builder.Append("<Max24HourConcentrationUnitId>" + item.Max24HourConcentrationUnitId + "</Max24HourConcentrationUnitId>");
                    builder.Append("<LethalDoseConcentration>" + item.LethalDoseConcentration + "</LethalDoseConcentration>");
                    builder.Append("<LethalDoseConcentrationUnitId>" + item.LethalDoseConcentrationUnitId + "</LethalDoseConcentrationUnitId>");
                    builder.Append("<Lethal24HourConcentration>" + item.Lethal24HourConcentration + "</Lethal24HourConcentration>");
                    builder.Append("<Lethal24HourConcentrationUnitId>" + item.Lethal24HourConcentrationUnitId + "</Lethal24HourConcentrationUnitId>");
                    builder.Append("<DCIId>" + item.DCIId + "</DCIId>");
                    builder.Append("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.Append("</LethalDoseLimits>");
                }
            }

            if(DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("LethalDoseLimits"))
            {
                var deletes = DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties["LethalDoseLimits"].ToList();

                foreach (LethalDoseLimits item in deletes)
                {
                    builder.Append("<LethalDoseLimits>");
                    builder.Append("<Id>" + item.Id + "</Id>");
                    builder.Append("<StartAge>" + item.StartAge + "</StartAge>");
                    builder.Append("<StartAgeUnit>" + item.StartAgeUnit + "</StartAgeUnit>");
                    builder.Append("<EndAge>" + item.EndAge + "</EndAge>");
                    builder.Append("<EndAgeUnit>" + item.EndAgeUnit + "</EndAgeUnit>");
                    builder.Append("<StartWeight>" + item.StartWeight + "</StartWeight>");
                    builder.Append("<StartWeightUnit>" + item.StartWeightUnit + "</StartWeightUnit>");
                    builder.Append("<EndWeight>" + item.EndWeight + "</EndWeight>");
                    builder.Append("<EndWeightUnit>" + item.EndWeightUnit + "</EndWeightUnit>");
                    builder.Append("<MaxDoseConcentration>" + item.MaxDoseConcentration + "</MaxDoseConcentration>");
                    builder.Append("<MaxDoseConcentrationUnitId>" + item.MaxDoseConcentrationUnitId + "</MaxDoseConcentrationUnitId>");
                    builder.Append("<Max24HourConcentration>" + item.Max24HourConcentration + "</Max24HourConcentration>");
                    builder.Append("<Max24HourConcentrationUnitId>" + item.Max24HourConcentrationUnitId + "</Max24HourConcentrationUnitId>");
                    builder.Append("<LethalDoseConcentration>" + item.LethalDoseConcentration + "</LethalDoseConcentration>");
                    builder.Append("<LethalDoseConcentrationUnitId>" + item.LethalDoseConcentrationUnitId + "</LethalDoseConcentrationUnitId>");
                    builder.Append("<Lethal24HourConcentration>" + item.Lethal24HourConcentration + "</Lethal24HourConcentration>");
                    builder.Append("<Lethal24HourConcentrationUnitId>" + item.Lethal24HourConcentrationUnitId + "</Lethal24HourConcentrationUnitId>");
                    builder.Append("<DCIId>" + item.DCIId + "</DCIId>");
                    builder.Append("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.Append("</LethalDoseLimits>");
                }
            }

            if (DCI.DCIRiskFactors != null && DCI.DCIRiskFactors.Any())
            {
                foreach (DCIRiskFactors item in DCI.DCIRiskFactors)
                {
                    builder.AppendFormat("<DCIRiskFactor>");
                    builder.AppendFormat("<Id>" + item.Id + "</Id>");
                    builder.AppendFormat("<DCIId>" + item.DciId + "</DCIId>");
                    builder.AppendFormat("<RiskFactorId>" + item.RiskFactorId + "</RiskFactorId>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</DCIRiskFactor>");
                }
            }

            if (DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("DCIRiskFactors"))
            {
                var deletes = DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties["DCIRiskFactors"].ToList();

                foreach (DCIRiskFactors item in deletes)
                {
                    builder.AppendFormat("<DCIRiskFactor>");
                    builder.AppendFormat("<Id>" + item.Id + "</Id>");
                    builder.AppendFormat("<DCIId>" + item.DciId + "</DCIId>");
                    builder.AppendFormat("<RiskFactorId>" + item.RiskFactorId + "</RiskFactorId>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</DCIRiskFactor>");
                }
            }

            if (DCI.RisksDescription != null && DCI.RisksDescription.Any())
            {
                foreach (RisksDescription item in DCI.RisksDescription)
                {
                    builder.AppendFormat("<RisksDescription>");
                    builder.AppendFormat("<Id>" + item.Id + "</Id>");
                    builder.AppendFormat("<DCIId>" + item.DciId + "</DCIId>");
                    builder.AppendFormat("<TagRiskType>" + item.TagRiskType + "</TagRiskType>");
                    builder.AppendFormat("<TagIncludedDescription>" + item.TagIncludedDescription + "</TagIncludedDescription>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</RisksDescription>");
                }
            }

            if (DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("RisksDescription"))
            {
                var deletes = DCI.ChangeTracker.ObjectsRemovedFromCollectionProperties["RisksDescription"].ToList();

                foreach (RisksDescription item in deletes)
                {
                    builder.AppendFormat("<RisksDescription>");
                    builder.AppendFormat("<Id>" + item.Id + "</Id>");
                    builder.AppendFormat("<DCIId>" + item.DciId + "</DCIId>");
                    builder.AppendFormat("<TagRiskType>" + item.TagRiskType + "</TagRiskType>");
                    builder.AppendFormat("<TagIncludedDescription>" + item.TagIncludedDescription + "</TagIncludedDescription>");
                    builder.AppendFormat("<ChangeTracker>" + item.ChangeTracker.State + "</ChangeTracker>");
                    builder.AppendFormat("</RisksDescription>");
                }
            }
            builder.Append("</DCI>");

            return builder.ToString();
        }

        public ActionResult<Domain.Entities.DCI> SaveStateDCI(Domain.Entities.DCI DCI, AuditMessage audit, long idSecuence = 0)
        {
            if (DCI == null)
            {
                throw new ArgumentNullException("ObjEntity");
            }
            IUnitWork unitOfWork = this._dciRepository.UnitWork;
            IUnitWork sequenseUnitOfWork = this._sequenseRepository.UnitWork;
            try
            {
                TransactionOptions txSettings = new TransactionOptions();
                txSettings.Timeout = TransactionManager.MaximumTimeout;
                txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
                {
                    var MessageResult = string.Empty;
                    if (string.IsNullOrEmpty(DCI.Code))
                    {
                        InventorySequenceDetail seq = this._sequenseRepository.GetSequenseDById((int)idSecuence);
                        if ((seq != null) && (seq.Id > 0) && (seq.InventorySequence.Sequential))
                        {
                            var res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next);
                            if ((res != null) && !(res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE)))
                            {
                                DCI.Code = res;
                                seq.Next += 1;
                                this._sequenseRepository.SaveEntity(seq);
                                sequenseUnitOfWork.Commit();
                            }
                            else
                            {
                                scope.Dispose();
                                return new ActionResult<Domain.Entities.DCI> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }, Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                            }
                            MessageResult = seq.InventorySequence.Sequential ? string.Format(ResourceManager.get_GetString("SavedWithCode"), DCI.Code) : ResourceManager.get_GetString("SaveMessage");
                        }
                        else
                        {
                            scope.Dispose();
                            return new ActionResult<Domain.Entities.DCI> { StatusCode = eStatusResult.WARNING, StateResult = false, MessageResult = new List<string> { "_Seq02_" }.ToList(), Message = String.Format(ResourceManager.get_GetString("SequenceFormNotFound"), FORM_NAME) };
                        }
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("SaveMessage");
                    }
                    Domain.Entities.DCI auxObjEntity = null;
                    IndigoAuditSimpleEntity<Domain.Entities.DCI> auditProcess;
                    Infrastructure.CrossCutting.Audit.Actions status;
                    if (DCI.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added)
                    {
                        DCI.CreationUser = audit.CodeUser;
                        DCI.CreationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Insert;
                    }
                    else
                    {
                        MessageResult = ResourceManager.get_GetString("UpdateMessage");
                        auxObjEntity = DCI.OriginalValue;
                        DCI.ModificationUser = audit.CodeUser;
                        DCI.ModificationDate = DateTime.Now;
                        status = Infrastructure.CrossCutting.Audit.Actions.Update;
                    }
                    this._dciRepository.SaveEntity(DCI);
                    unitOfWork.Commit();

                    auditProcess = new IndigoAuditSimpleEntity<Domain.Entities.DCI>(DCI, audit, status, auxObjEntity);
                    auditProcess.Execute();
                    //Se marca la entidad como sin cambios
                    DCI.MarkAsUnchanged();
                    scope.Complete();
                    return new ActionResult<Domain.Entities.DCI> { StateResult = true, StatusCode = eStatusResult.SUCCESS, ObjectEmbbeded = DCI, Message = MessageResult };
                }
            }
            catch (OptimisticConcurrencyException ex)
            {
                unitOfWork.RollbackChanges();
                return new ActionResult<Domain.Entities.DCI> { StateResult = false, StatusCode = eStatusResult.WARNING, MessageResult = new List<string> { "-999" }, Message = ResourceManager.get_GetString("ErrorConcurrence") };
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackChanges();
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<Domain.Entities.DCI> { StateResult = false, StatusCode = eStatusResult.EXCEPTION, MessageResult = { ex.Message }, Message = IndigoManagementExceptions.GetExceptionDetails(ex) };
            }
        }

        #endregion

        #region Events

        public void TriggerEvent(Domain.Entities.DCI dCI, AuditMessage audit)
        {
            Wrapper wrapperEvent = new Wrapper();
            String ChangeTracker = dCI.ChangeTracker.State.ToString().ToLower();
            if (dCI.ChangeTracker.State == ObjectState.Unchanged) { ChangeTracker = "modified"; }
            EventData eventData = wrapperEvent.GenerateWrapperEventData(dCI, audit.CodeUser, ChangeTracker, DittoSourceType.dCI);
            IIndigoQueue queue = _factoryQueue.CreateQueue();
            queue.Publish(eventData);
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
                _dciRepository = null;
                _sequenseRepository = null;
                _drugInteractionRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion 
    }
}
