using Application.Events.Models;
using Application.Events.Models.FunctionalUnit;
using System;

namespace Application.Events.Serializers
{
    public class FunctionalUnit : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Payroll.Entities.FunctionalUnit functionalUnitEntity = obj as Domain.Payroll.Entities.FunctionalUnit;
            MFunctionalUnit functionalUnit = new MFunctionalUnit();
            functionalUnit.Code = functionalUnitEntity.Code;
            functionalUnit.Name = functionalUnitEntity.Name;
            functionalUnit.State = Convert.ToInt16(functionalUnitEntity.State);
            functionalUnit.CreationUser = functionalUnitEntity.CreationUser;
            functionalUnit.CreationDate = Convert.ToString(functionalUnitEntity.CreationDate);
            functionalUnit.ModificationUser = functionalUnitEntity.ModificationUser;
            functionalUnit.ModificationDate = Convert.ToString(functionalUnitEntity.ModificationDate);

            return functionalUnit;
        }
    }
}
