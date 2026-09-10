//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Hector Rodriguez R
// Created          : 06/04/2020
//
// Copyright        : (c) . All rights reserved.
// About            : PBI8934
//***********************************************************************

using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.BacterialResistanceMedication;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServiceBacterialResistanceMedication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaBRM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.BacterialResistanceMedication> SaveBacterialResistanceMedication(List<Domain.Entities.BacterialResistanceMedication> listaBRM, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IBacterialResistanceMedicationAdminService>())
            {
                return service.SaveBacterialResistanceMedication(listaBRM, audit);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statesBRM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.BacterialResistanceMedication>> GetBacterialResistanceMedicationByStates(string statesBRM, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IBacterialResistanceMedicationAdminService>())
            {
                return service.GetBacterialResistanceMedicationByStates(statesBRM, audit);
            }
        }

    }
}
