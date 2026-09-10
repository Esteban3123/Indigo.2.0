using Application.Events.Models;
using Application.Events.Models.MedicalFeesContract;
using System;
using System.Linq;

namespace Application.Events.Serializers
{
    public class CausationPending : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : MedicalFeesContract
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.MedicalFeesContract medicalFeesContractEntity = obj as Domain.Entities.MedicalFeesContract;

            MMedicalFeesContract mMedicalFeesContract = new MMedicalFeesContract();
            MethodsMedicalFeesContract methods = new MethodsMedicalFeesContract();
            
            mMedicalFeesContract.Code = medicalFeesContractEntity.Code;
            mMedicalFeesContract.Status = Convert.ToInt16(medicalFeesContractEntity.Status);
            mMedicalFeesContract.CreationUser = medicalFeesContractEntity.CreationUser;
            mMedicalFeesContract.CreationDate = Convert.ToString(medicalFeesContractEntity.CreationDate);
            mMedicalFeesContract.ModificationUser = medicalFeesContractEntity.ModificationUser;
            mMedicalFeesContract.ModificationDate = Convert.ToString(medicalFeesContractEntity.ModificationDate);

            // Obtener profesionales de salud asociados (solo los nuevos)
            mMedicalFeesContract.HealthProfessionalsCodes = methods.GetHealthProfessionalsCodes(medicalFeesContractEntity.Id);

            // Obtener Ids de los Servicios IPS del contrato (excepciones)
            var IPSServicesIds = medicalFeesContractEntity.MedicalFeesContractException.Where(x => x.ExceptionType == 1 && x.ChangeTracker.State == Domain.Base.Entities.ObjectState.Added).Select(d => d.IPSServiceId.Value).ToList();
            mMedicalFeesContract.IpsServicesIds = IPSServicesIds;
            return mMedicalFeesContract;
        }
    }
}
