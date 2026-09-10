///***********************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Carlos Ernesto Cordoba
/// Created          : 08-01-2015
///
/// Copyright        : (c) . All rights reserved.
///***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using System.ServiceModel;

namespace DistributedServices.Inventory.Contracts
{
    [ServiceContract]
    public interface IInventoryServiceRemissionOutputDetailPhysical
    {
        /// <summary>
        /// lista los detalles del detalle de la remision de salida
        /// </summary>
        /// <param name="RemissionOutputId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Domain.Entities.RemissionOutputDetailPhysical> ListRemissionOutputDetailPhysicalByRemissionOutputId(int RemissionOutputId);
    }
}
