using Application.Events.Models;
using Application.Events.Models.PackagingUnit;
using System;

namespace Application.Events.Serializers
{
    public class packagingUnit : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : PackagingUnit
        /// </summary>
        /// <param name="packagingUnitEntity"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.PackagingUnit packagingUnitEntity = obj as Domain.Entities.PackagingUnit;

            MPackagingUnit mpackagingUnit = new MPackagingUnit();
            mpackagingUnit.Code = packagingUnitEntity.Code;
            mpackagingUnit.Name = packagingUnitEntity.Name;
            mpackagingUnit.Abbreviation = packagingUnitEntity.Abbreviation;
            mpackagingUnit.Status = Convert.ToInt16(packagingUnitEntity.Status);
            mpackagingUnit.CreationUser = packagingUnitEntity.CreationUser;
            mpackagingUnit.CreationDate = Convert.ToString(packagingUnitEntity.CreationDate);
            mpackagingUnit.ModificationUser = packagingUnitEntity.ModificationUser;
            mpackagingUnit.ModificationDate = Convert.ToString(packagingUnitEntity.ModificationDate);
            return mpackagingUnit;
        }
    }
}
