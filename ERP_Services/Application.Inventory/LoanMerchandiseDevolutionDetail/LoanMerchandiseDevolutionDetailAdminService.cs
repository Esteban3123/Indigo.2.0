///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Rafael Eduardo Patiño cabrera
/// Created          : 07-05-2015
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

namespace Application.Inventory.LoanMerchandiseDevolutionDetail
{
    public class LoanMerchandiseDevolutionDetailAdminService : ILoanMerchandiseDevolutionDetailAdminService
    {

        #region Fields
        ILoanMerchandiseDevolutionDetailRepository _LoanMerchandiseDevolutionDetailRepository;
        #endregion

        #region Builder
        public LoanMerchandiseDevolutionDetailAdminService(ILoanMerchandiseDevolutionDetailRepository LoanMerchandiseDevolutionDetailRepository)
        {
            if (LoanMerchandiseDevolutionDetailRepository == null)
            {
                throw new ArgumentNullException("LoanMerchandiseDevolutionDetailRepository");
            }
            _LoanMerchandiseDevolutionDetailRepository = LoanMerchandiseDevolutionDetailRepository;
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Lista los detalles de devolcuion de un prestamo
        /// </summary>
        /// <param name="IdLoanMerchandise"></param>
        /// <returns></returns>
        public List<Domain.Entities.LoanMerchandiseDevolutionDetail> ListLoanMerchandiseDevolutionDetailByIdLoanMerchandise(int IdLoanMerchandiseDevolution)
        {
            try
            {
                return _LoanMerchandiseDevolutionDetailRepository.ListLoanMerchandiseDevolutionDetailByIdLoanMerchandise(IdLoanMerchandiseDevolution);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.LoanMerchandiseDevolutionDetail>();
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
                _LoanMerchandiseDevolutionDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion
    }
}
