using Application.Events.Models;
using Application.Events.Models.ElectronicRIPS;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Serializers
{
    class ElectronicRIPSEvent : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : ElectronicRIPS
        /// </summary>
        /// <param name="MainAccountsEntity"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            var Item = obj as ElectronicRIPS;
            MElectronicRIPS electronicRIPS = new MElectronicRIPS();
            if (obj is null){return electronicRIPS;}
            electronicRIPS.EntityName = Item.EntityName;
            electronicRIPS.EntityCode = Item.ListEntityCode;
            return electronicRIPS;
        }
    }
}
