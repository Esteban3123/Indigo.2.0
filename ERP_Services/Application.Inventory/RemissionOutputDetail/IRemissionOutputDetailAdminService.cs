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

namespace Application.Inventory.RemissionOutputDetail
{
    public interface IRemissionOutputDetailAdminService : IDisposable
    {
        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="RemissionOutputId"></param>
        /// <returns></returns>
        List<Domain.Entities.RemissionOutputDetail> GetRemissionOutputDetailByRemissionOutputId(int RemissionOutputId);
    }
}
