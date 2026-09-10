using Application.Events.Models;
using Application.Events.Models.pharmacologicalGroup;
using System;

namespace Application.Events.Serializers
{
    public class pharmacologicalGroup : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.PharmacologicalGroup pharmacologicalGroup = obj as Domain.Entities.PharmacologicalGroup;

            MpharmacologicalGroup mpharmacologicalGroup = new MpharmacologicalGroup();
            mpharmacologicalGroup.Code = pharmacologicalGroup.Code;
            mpharmacologicalGroup.Name = pharmacologicalGroup.Name;
            mpharmacologicalGroup.Status = Convert.ToInt16(pharmacologicalGroup.Status);
            mpharmacologicalGroup.CreationUser = Convert.ToString(pharmacologicalGroup.CreationUser);
            mpharmacologicalGroup.CreationDate = Convert.ToString(pharmacologicalGroup.CreationDate);
            mpharmacologicalGroup.ModificationUser = Convert.ToString(pharmacologicalGroup.ModificationUser);
            mpharmacologicalGroup.ModificationDate = Convert.ToString(pharmacologicalGroup.ModificationDate);
            mpharmacologicalGroup.CrystalPharmacologicalGroup = pharmacologicalGroup.Code;
            return mpharmacologicalGroup;
        }
    }
}
