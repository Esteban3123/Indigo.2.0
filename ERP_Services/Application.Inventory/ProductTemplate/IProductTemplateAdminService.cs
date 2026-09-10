//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Diego Andrés Roldán Lozano
// Created          : 03-12-2014
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

namespace Application.Inventory.ProductTemplate
{
    public interface IProductTemplateAdminService : IDisposable
    {

        /// <summary>
        /// Guarda un cubrimiento de producto de forma asincrona
        /// </summary>
        Task<ActionResult<Domain.Entities.ProductRate>> SaveProductTemplate(Domain.Entities.ProductRate productTemplate, AuditMessage audit, Int64 idSecuence = 0);

        /// <summary>
        /// Elimina un cubrimiento de producto
        /// </summary>
        ActionResult DeleteProductTemplate(Domain.Entities.ProductRate productTemplate, AuditMessage audit);

        /// <summary>
        /// Actualiza el estado del cubrimiento de producto
        /// </summary>
        Task <ActionResult<Domain.Entities.ProductRate>> UpdateStateProductTemplate(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Obtiene un cubrimiento de producto por codigo
        /// </summary>
        Task <ActionResult<Domain.Entities.ProductRate>> GetProductTemplate(string code, AuditMessage audit);

        /// <summary>
        /// Obtiene un cubrimiento de producto por id
        /// </summary>
        Domain.Entities.ProductRate GetProductTemplateById(int id);


        
    }
}
