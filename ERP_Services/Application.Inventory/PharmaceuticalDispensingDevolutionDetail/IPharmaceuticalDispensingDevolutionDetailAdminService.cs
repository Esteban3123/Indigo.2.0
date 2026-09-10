//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Carlos Ernesto Cordoba
// Created          : 08-01-2015
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;


namespace Application.Inventory.PharmaceuticalDispensingDevolutionDetail
{
    public interface IPharmaceuticalDispensingDevolutionDetailAdminService : IDisposable
    {
        /// <summary>
        /// lista los detalles de la devolucion
        /// </summary>
        /// <param name="IdPharmaceuticalDispensingDevolution"></param>
        /// <returns></returns>
        List<Domain.Entities.PharmaceuticalDispensingDevolutionDetail> GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(int IdPharmaceuticalDispensingDevolution);
    }
}
