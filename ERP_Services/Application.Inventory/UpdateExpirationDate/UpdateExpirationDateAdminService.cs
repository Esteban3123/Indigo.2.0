///************************************************************
/// Assembly         : Application.Inventory
/// Author           : Rafel Eduardo Patiño Cabrera
/// Created          : 27-05-2015
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

using System;
using System.Collections.Generic;
using Domain.Base;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Resources;
using System.Data;
using Application.Base;
using Application.Inventory.PhysicalInventory;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Text;
using Domain.Entities.Service;
using Application.Accounting; 

namespace Application.Inventory.UpdateExpirationDate
{
   public class UpdateExpirationDateAdminService : IUpdateExpirationDateAdminService
   {

       #region fields
       private IUpdateExpirationDateRepository  _UpdateExpirationDateRepository;
       #endregion

       #region builder
       public UpdateExpirationDateAdminService(IUpdateExpirationDateRepository UpdateExpirationDateRepository)
       {           
           if (UpdateExpirationDateRepository == null) {
               throw new ArgumentNullException("UpdateExpirationDateRepository");
           }
            _UpdateExpirationDateRepository = UpdateExpirationDateRepository;
       }
       #endregion

       #region Metodos


        #endregion

       public ActionResult<Domain.Entities.LoanMerchandise> SaveUpdateExpirationDate(Domain.Entities.UpdateExpirationDate UpdateExpirationDate, AuditMessage audit)
       {
           throw new NotImplementedException();
       }

        #region IDisposable Support
        private bool disposedValue;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                _UpdateExpirationDateRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
