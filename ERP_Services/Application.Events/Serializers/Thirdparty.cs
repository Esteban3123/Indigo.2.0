using Application.Events.Models;
using Application.Events.Models.Thirdparty;
using System;

namespace Application.Events.Serializers
{
    public class Thirdparty : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : ThirdParty
        /// </summary>
        /// <param name="thirdpartyEntity"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.ThirdParty thirdpartyEntity = obj as Domain.Entities.ThirdParty;

            MThirdparty thirdparty = new MThirdparty();
            methodsThirdparty methodsThirdparty = new methodsThirdparty();
            if (thirdpartyEntity == null) { return thirdparty; }
            thirdparty.Person = methodsThirdparty.GeneratePerson(thirdpartyEntity);
            thirdparty.Nit = thirdpartyEntity.Nit;
            thirdparty.DigitVerification = thirdpartyEntity.DigitVerification;
            thirdparty.Name = thirdpartyEntity.Name;
            thirdparty.PersonType = thirdpartyEntity.PersonType;
            thirdparty.RetentionType = thirdpartyEntity.RetentionType;
            thirdparty.ContributionType = thirdpartyEntity.ContributionType;
            thirdparty.StateEnterpriseType = thirdpartyEntity.StateEnterpriseType;
            thirdparty.IVARetentionAccountPayableConcept = methodsThirdparty.GetIVAccountPayableConcept(thirdpartyEntity.IVARetentionAccountPayableConceptId);
            thirdparty.Ica = Convert.ToInt16(thirdpartyEntity.Ica);
            thirdparty.IcaPercentage = thirdpartyEntity.IcaPercentage;
            thirdparty.IcaTop = Convert.ToInt16(thirdpartyEntity.IcaTop);
            thirdparty.IcaTopValue = thirdpartyEntity.IcaTopValue;
            thirdparty.EntityCode = thirdpartyEntity.EntityCode;
            thirdparty.EconomicActivity = methodsThirdparty.GetEconomicActivity(thirdpartyEntity.EconomicActivityId ?? 0);
            thirdparty.Class = Convert.ToInt16(thirdpartyEntity.Class);
            thirdparty.CodeCIIU = thirdpartyEntity.CodeCIIU;
            thirdparty.State = Convert.ToInt16(thirdpartyEntity.State);
            thirdparty.CreationDate = Convert.ToString(thirdpartyEntity.CreationDate);
            thirdparty.CreationUser = thirdpartyEntity.CreationUser;
            thirdparty.HandlesBranchOffice = Convert.ToInt16(thirdpartyEntity.HandlesBranchOffice);
            thirdparty.BranchOffice = methodsThirdparty.GetBranchOffice(thirdpartyEntity);
            thirdparty.CodeDivipola = thirdpartyEntity.CodeDivipola;
            thirdparty.IVARetentionConcept = methodsThirdparty.GetIVARetentionConcept(thirdpartyEntity.IVARetentionConceptId ?? 0);
            thirdparty.FiscalResponsibility = methodsThirdparty.GetFiscalResponsibility(thirdpartyEntity);
            return thirdparty;
        }
    }
}
