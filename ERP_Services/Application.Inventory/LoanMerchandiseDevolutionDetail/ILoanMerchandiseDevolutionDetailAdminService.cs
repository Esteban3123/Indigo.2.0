//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Rafael Eduardo Patiño
// Created          : 07-05-2015
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
namespace Application.Inventory.LoanMerchandiseDevolutionDetail
{
    public interface ILoanMerchandiseDevolutionDetailAdminService : IDisposable
    {

        /// <summary>
        /// obtiene los detalles devolución prestamo
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        List<Domain.Entities.LoanMerchandiseDevolutionDetail> ListLoanMerchandiseDevolutionDetailByIdLoanMerchandise(int IdLoanMerchandiseDevolution);

    }
}
