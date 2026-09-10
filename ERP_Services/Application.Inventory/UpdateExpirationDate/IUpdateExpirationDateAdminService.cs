//***********************************************************************
// Assembly         : Application.Inventory
// Author           : Rafael Eduardo Patiño Cabrera
// Created          : 27-05-2015
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

namespace Application.Inventory.UpdateExpirationDate
{
    public interface IUpdateExpirationDateAdminService : IDisposable
    {
               /// <summary>
        /// guarda una solicitud de prestamo
        /// </summary>
        /// <param name="loadmerchadise"></param>
        /// <param name="audit"></param>
        /// <param name="idSequence"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.LoanMerchandise> SaveUpdateExpirationDate(Domain.Entities.UpdateExpirationDate UpdateExpirationDate, AuditMessage audit);
        
    }
}
