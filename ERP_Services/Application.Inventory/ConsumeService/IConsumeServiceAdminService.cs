//***********************************************************************
// Assembly         : Aplication.Inventory
// Author           : Carlos Mario Arias Rubiano
// Created          : 07/05/2018
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

#region Imports
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Application.Inventory.ConsumeService
{
    public interface IConsumeServiceAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene la dispensación por formula medica
        /// </summary>
        /// <returns></returns>
        ActionResult<WebServiceObject> GetDispensingByPatient(string parameters);

        /// <summary>
        /// Obtiene un listado de dispensaciones por rango de fechas
        /// </summary>
        /// <returns></returns>
        ActionResult<string> GetDispensingByDateRange(string parameters);

        /// <summary>
        /// Confirma la dispensación con integración a HEON
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="PharmaceuticalDispensing"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult PostConfirmIntegration(string parameters, Domain.Entities.PharmaceuticalDispensing PharmaceuticalDispensing, AuditMessage audit);
    }
}
