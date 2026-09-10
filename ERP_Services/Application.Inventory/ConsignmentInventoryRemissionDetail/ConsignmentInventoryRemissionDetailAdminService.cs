///************************************************************
/// Assembly         : Aplication.Inventory
/// Author           : Miguel Angel Fonseca
/// Created          : 2017-12-12
///
/// Copyright        : (c) . All rights reserved.
///************************************************************

#region Imports

using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;

#endregion Imports

namespace Application.Inventory.ConsignmentInventoryRemissionDetail
{
    public class ConsignmentInventoryRemissionDetailAdminService : IConsignmentInventoryRemissionDetailAdminService
    {
        #region Fields

        private IConsignmentInventoryRemissionDetailRepository _consignmentInventoryRemissionDetailRepository;

        #endregion Fields

        #region Builder

        public ConsignmentInventoryRemissionDetailAdminService(IConsignmentInventoryRemissionDetailRepository consignmentInventoryRemissionDetailRepository)
        {
            if (consignmentInventoryRemissionDetailRepository == null)
            {
                throw new ArgumentNullException("consignmentInventoryRemissionDetailRepository");
            }
            _consignmentInventoryRemissionDetailRepository = consignmentInventoryRemissionDetailRepository;
        }

        #endregion Builder

        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        public List<Domain.Entities.ConsignmentInventoryRemissionDetail> GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(int ConsignmentInventoryRemissionId)
        {
            try
            {
                return _consignmentInventoryRemissionDetailRepository.ListConsignmentInventoryRemissionDetailByIdConsignmentInventoryRemission(ConsignmentInventoryRemissionId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.ConsignmentInventoryRemissionDetail>();
            }
        }

        /// <summary>
        /// obtiene los detalles de la remision
        /// </summary>
        /// <param name="ConsignmentInventoryRemissionId"></param>
        /// <returns></returns>
        public List<Domain.Entities.ConsignmentInventoryRemissionDetail> ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(int SupplierId, int SupplierDistributionLineId)
        {
            try
            {
                return _consignmentInventoryRemissionDetailRepository.ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(SupplierId, SupplierDistributionLineId);
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.ConsignmentInventoryRemissionDetail>();
            }
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
                _consignmentInventoryRemissionDetailRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion 
    }
}