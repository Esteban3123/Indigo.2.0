using Application.Events.Models;
using Application.Events.Models.HealthAdministrator;
using System;

namespace Application.Events.Serializers
{
    public class HealthAdministrator : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : HealthAdministrator
        /// </summary>
        /// <param name="HealthAdministratorEntity"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.HealthAdministrator HealthAdministratorEntity = obj as Domain.Entities.HealthAdministrator;

            MHealthAdministrator mHealthAdministrator = new MHealthAdministrator();
            MethodsHealthAdministrator methodsHealthAdministrator = new MethodsHealthAdministrator();
            mHealthAdministrator.Code = HealthAdministratorEntity.Code;
            mHealthAdministrator.Name = HealthAdministratorEntity.Name;
            mHealthAdministrator.Thirdparty = methodsHealthAdministrator.getThirdParty(HealthAdministratorEntity.ThirdPartyId);
            mHealthAdministrator.EntityType = HealthAdministratorEntity.EntityType;
            mHealthAdministrator.HealthEntityCode = HealthAdministratorEntity.HealthEntityCode;
            mHealthAdministrator.Regimen4505 = HealthAdministratorEntity.Regimen4505;
            mHealthAdministrator.Status = Convert.ToInt16(HealthAdministratorEntity.Status);
            mHealthAdministrator.CreationUser = HealthAdministratorEntity.CreationUser;
            mHealthAdministrator.CreationDate = Convert.ToString(HealthAdministratorEntity.CreationDate);
            mHealthAdministrator.ModificationUser = HealthAdministratorEntity.ModificationUser;
            mHealthAdministrator.ModificationDate = Convert.ToString(HealthAdministratorEntity.ModificationDate);
            return mHealthAdministrator;
        }
    }
}
