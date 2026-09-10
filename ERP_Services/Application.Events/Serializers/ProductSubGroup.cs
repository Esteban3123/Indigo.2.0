using Application.Events.Models;
using Application.Events.Models.ProductSubGroup;
using System;

namespace Application.Events.Serializers
{
    public class ProductSubGroup : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.ProductSubGroup productSubGroup = obj as Domain.Entities.ProductSubGroup;

            MProductSubGroup mProductSubGroup = new MProductSubGroup();
            mProductSubGroup.Code = productSubGroup.Code;
            mProductSubGroup.Name = productSubGroup.Name;
            mProductSubGroup.HandlesBatch = Convert.ToInt16(productSubGroup.HandlesBatch);
            mProductSubGroup.HandlesExpiry = Convert.ToInt16(productSubGroup.HandlesExpiry);
            mProductSubGroup.Status = Convert.ToInt16(productSubGroup.Status);
            mProductSubGroup.CreationUser = productSubGroup.CreationUser;
            mProductSubGroup.CreationDate = Convert.ToString(productSubGroup.CreationDate);
            mProductSubGroup.ModificationUser = productSubGroup.ModificationUser;
            mProductSubGroup.ModificationDate = Convert.ToString(productSubGroup.ModificationDate);
            return mProductSubGroup;
        }
    }
}
