using Application.Events.Models;
using Application.Events.Models.ProductType;
using System;

namespace Application.Events.Serializers
{
    public class productType : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.ProductType productType = obj as Domain.Entities.ProductType;

            MProductType mProductType = new MProductType();
            mProductType.Code = productType.Code;
            mProductType.Name = productType.Name;
            mProductType.Class = Convert.ToInt16(productType.Class);
            mProductType.Status = Convert.ToInt16(productType.Status);
            mProductType.CreationUser = productType.CreationUser;
            mProductType.CreationDate = Convert.ToString(productType.CreationDate);
            mProductType.ModificationUser = productType.ModificationUser;
            mProductType.ModificationDate = Convert.ToString(productType.ModificationDate);
            mProductType.TemperatureRange = Convert.ToInt16(productType.TemperatureRange);
            return mProductType;
        }
    }
}
