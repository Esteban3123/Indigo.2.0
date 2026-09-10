///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Rafael Eduardo Patiño cabrera
/// Created          : 24-04-2015
///
/// Copyright        : (c) . All rights reserved.
///************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.CrossCutting.Exceptions;
using Infrastructure.CrossCutting.Base;

namespace Application.Inventory.LoanMerchandiseDetail
{
    public class LoanMerchandiseDetailAdminService : ILoanMerchandiseDetailAdminService
    {

        #region Fields
        ILoanMerchandiseDetailRepository _LoanMerchandiseDetailRepository;
        #endregion

        #region Builder
        public LoanMerchandiseDetailAdminService(ILoanMerchandiseDetailRepository LoanMerchandiseDetailRepository)
        {
            if (LoanMerchandiseDetailRepository == null)
            {
                throw new ArgumentNullException("LoanMerchandiseDetailRepository");
            }
            _LoanMerchandiseDetailRepository = LoanMerchandiseDetailRepository;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Lista los detalles de un prestamo
        /// </summary>
        /// <param name="IdLoanMerchandise"></param>
        /// <returns></returns>
        public List<Domain.Entities.LoanMerchandiseDetail> ListLoanMerchandiseDetailByIdLoanMerchandise(int IdLoanMerchandise, Boolean IsDevolution)
        {
            try
            {
                return _LoanMerchandiseDetailRepository.ListLoanMerchandiseDetailByIdLoanMerchandise(IdLoanMerchandise, IsDevolution);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.LoanMerchandiseDetail>();
            }
        }
        #endregion

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
                _LoanMerchandiseDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
