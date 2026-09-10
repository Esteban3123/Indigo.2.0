//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Diego Andrés Roldán Lozano
// Created          : 02-12-2014
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Domain.Base.Entities;
using Infrastructure.CrossCutting.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Inventory.ATC
{
    public interface IATCAdminService : IDisposable
    {
        /// <summary>
        /// Guarda un ATC
        /// </summary>
        ActionResult<Domain.Entities.ATC> SaveATC(Domain.Entities.ATC atc, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un ATC
        /// </summary>
        ActionResult DeleteATC(Domain.Entities.ATC atc, AuditMessage audit);

        /// <summary>
        /// Actualiza el estado del atc
        /// </summary>
        ActionResult<Domain.Entities.ATC> UpdateStateATC(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene un atc por codigo
        /// </summary>
        ActionResult<Domain.Entities.ATC> GetATC(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un atc por id
        /// </summary>
        Domain.Entities.ATC GetATCById(int id);
    }
}