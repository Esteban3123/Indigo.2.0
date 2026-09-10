//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Rafael Eduardo Patiño
// Created          : 24-04-2015
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
namespace Application.Inventory.LoanMerchandiseDetail
{
    public interface ILoanMerchandiseDetailAdminService : IDisposable
    {

        /// <summary>
        /// obtiene los detalles del prestamo
        /// </summary>
        /// <param name="RemissionEntranceId"></param>
        /// <returns></returns>
        List<Domain.Entities.LoanMerchandiseDetail> ListLoanMerchandiseDetailByIdLoanMerchandise(int IdLoanMerchandise, Boolean IsDevolution);

    }
}
