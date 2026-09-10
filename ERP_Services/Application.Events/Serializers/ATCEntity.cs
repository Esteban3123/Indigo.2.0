using Application.Events.Models;
using Application.Events.Models.ATCEntity;
using System;

namespace Application.Events.Serializers
{
    public class ATCEntity : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.ATCEntity aTCEntity = obj as Domain.Entities.ATCEntity;

            MATCEntity mATCEntity = new MATCEntity();
            MethodsATCEntity methodsATCEntity = new MethodsATCEntity();
            mATCEntity.Code = aTCEntity.Code;
            mATCEntity.Name = aTCEntity.Name;
            mATCEntity.PharmacologicalGroup = methodsATCEntity.GetPharmacologicalGroup(aTCEntity.IdPharmacologicalGroup);
            mATCEntity.Status = Convert.ToInt16(aTCEntity.State);
            mATCEntity.CreationUser = aTCEntity.CreationUser;
            mATCEntity.CreationDate = Convert.ToString(aTCEntity.CreationDate);
            mATCEntity.ModificationUser = aTCEntity.ModificationUser;
            mATCEntity.ModificationDate = Convert.ToString(aTCEntity.ModificationDate);
            return mATCEntity;
        }
    }
}
