//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Hector Rodriguez R
// Created          : 04/08/2020
//
// Copyright        : (c) . All rights reserved.
// About            : PBI10044
//***********************************************************************

using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using DistributedServices.Inventory.Contracts;
using Application.Inventory.AntineoplasicoMedication;
using DistributedServices.Inventory.Unity;
using Microsoft.Practices.Unity;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryServiceAntineoplasicoMedication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaAPM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<Domain.Entities.AntineoplasicoMedication> SaveAntineoplasicoMedication(List<Domain.Entities.AntineoplasicoMedication> listaAPM, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAntineoplasicoMedicationAdminService>())
            {
                return service.SaveAntineoplasicoMedication(listaAPM, audit);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statesAPM"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.AntineoplasicoMedication>> GetAntineoplasicoMedicationByStates(string statesAPM, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IAntineoplasicoMedicationAdminService>())
            {
                return service.GetAntineoplasicoMedicationByStates(statesAPM, audit);
            }
        }

    }
}
