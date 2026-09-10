using Application.Events.Models;
using Application.Events.Models.Manufacturer;
using System;

namespace Application.Events.Serializers
{
    public class Manufacturer : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.Manufacturer manufacturer = obj as Domain.Entities.Manufacturer;

            MManufacturer mManufacturer = new MManufacturer();
            mManufacturer.Code = manufacturer.Code;
            mManufacturer.Name = manufacturer.Name;
            mManufacturer.Status = Convert.ToInt16(manufacturer.Status);
            mManufacturer.CreationUser = manufacturer.CreationUser;
            mManufacturer.CreationDate = Convert.ToString(manufacturer.CreationDate);
            mManufacturer.ModificationUser = manufacturer.ModificationUser;
            mManufacturer.ModificationDate = Convert.ToString(manufacturer.ModificationDate);
            return mManufacturer;
        }
    }
}
