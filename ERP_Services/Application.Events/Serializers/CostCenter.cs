using Application.Events.Models;
using Application.Events.Models.CostCenter;
using Application.Events.Repository.CostCenter;
using System;

namespace Application.Events.Serializers
{
    public class CostCenter : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Payroll.Entities.CostCenter costCenterEntity = obj as Domain.Payroll.Entities.CostCenter;

            MCostCenter costCenter = new MCostCenter();
            MethodsCostCenter methodsCostCenter = new MethodsCostCenter();
            costCenter.Code = costCenterEntity.Code;
            costCenter.Name = costCenterEntity.Name;
            costCenter.State = Convert.ToInt16(costCenterEntity.State);
            costCenter.BranchOffice = methodsCostCenter.GetBranchOffice(costCenterEntity.BranchOfficeId);
            costCenter.CreationUser = costCenterEntity.CreationUser;
            costCenter.CreationDate = Convert.ToString(costCenterEntity.CreationDate);
            costCenter.ModificationUser = costCenterEntity.ModificationUser;
            costCenter.ModificationDate = Convert.ToString(costCenterEntity.ModificationDate);

            return costCenter;
        }
    }
}
