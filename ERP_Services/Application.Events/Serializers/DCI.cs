using Application.Events.Models;
using Application.Events.Models.DCI;
using System;

namespace Application.Events.Serializers
{
    public class DCI : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.DCI dCI = obj as Domain.Entities.DCI;

            MDCI mDCI = new MDCI();
            MethodsDCI methodsDCI = new MethodsDCI();
            mDCI.Code = dCI.Code;
            mDCI.Name = dCI.Name;
            mDCI.DCICrystal = dCI.Code;
            mDCI.Status = Convert.ToInt16(dCI.Status);
            mDCI.CreationUser = dCI.CreationUser;
            mDCI.CreationDate = Convert.ToString(dCI.CreationDate);
            mDCI.ModificationUser = dCI.ModificationUser;
            mDCI.ModificationDate = Convert.ToString(dCI.ModificationDate);
            mDCI.DCIATCEntity = methodsDCI.GetDCIATCEntity(dCI);
            mDCI.DrugInteraction = methodsDCI.GetDrugInteraction(dCI);
            return mDCI;
        }
    }
}
