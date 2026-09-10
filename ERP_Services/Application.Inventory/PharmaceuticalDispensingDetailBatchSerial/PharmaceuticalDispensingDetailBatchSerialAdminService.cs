///************************************************************
/// Assembly         : Domain.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 20-11-2014
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

namespace Application.Inventory.PharmaceuticalDispensingDetailBatchSerial
{
    public class PharmaceuticalDispensingDetailBatchSerialAdminService : IPharmaceuticalDispensingDetailBatchSerialAdminService
    {
        IPharmaceuticalDispensingDetailBatchSerialRepository _pharmaceuticalDispensingDetailBatchSerialRepository;


        public PharmaceuticalDispensingDetailBatchSerialAdminService(IPharmaceuticalDispensingDetailBatchSerialRepository pharmaceuticalDispensingDetailBatchSerialRepository)
        {
            if (pharmaceuticalDispensingDetailBatchSerialRepository == null )
            {
                throw new ArgumentNullException("pharmaceuticalDispensingDetailBatchSerialRepository");
            }
            _pharmaceuticalDispensingDetailBatchSerialRepository = pharmaceuticalDispensingDetailBatchSerialRepository;
        }



        /// <summary>
        /// obtiene los productos para la devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="productCode"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        public List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> ListPharmaceuticalDispensingDetailBatchSerialDevolution(string admissionNumber, string functionalUnitCode, string productCode, int productType, int userId, string batchCode = "")
        {
            try
            {
                return _pharmaceuticalDispensingDetailBatchSerialRepository.ListPharmaceuticalDispensingDetailBatchSerialDevolution(admissionNumber,functionalUnitCode, productCode, productType, userId, batchCode);
            }
            catch (Exception ex)
            {                
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial>() ;
            }
        }


        /// <summary>
        /// lista los detalle de las dispensacion para hacer devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        public List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(string admissionNumber)
        {
            try
            {
                return _pharmaceuticalDispensingDetailBatchSerialRepository.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber);
            }
            catch (Exception ex)
            {
                
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial>() ;
            }
        }


        /// <summary>
        /// obtiene un detalla por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.Entities.PharmaceuticalDispensingDetailBatchSerial GetPharmaceuticalDispensingDetailBatchSerialById(int id)
        {
            try
            {
                return _pharmaceuticalDispensingDetailBatchSerialRepository.GetPharmaceuticalDispensingDetailBatchSerialById(id);
            }
            catch (Exception ex)
            {
                
               IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Entities.PharmaceuticalDispensingDetailBatchSerial() ;
            } 

        }


        /// <summary>
        /// obtiene los detalle para hacer la devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="productCode"></param>
        /// <param name="batchCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Domain.Base.Entities.ActionResult<List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial>> GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(string admissionNumber, string productCode, string batchCode, int userId)
        {
            try
            {
                return _pharmaceuticalDispensingDetailBatchSerialRepository.GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(admissionNumber, productCode, batchCode, userId);
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new Domain.Base.Entities.ActionResult<List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial>> { StateResult = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// Lista por el Id de la dispensación
        /// </summary>
        /// <param name="pharmaceuticalDispensingId">Id de la dispensación</param>
        /// <returns>Lista a retornar</returns>
        public List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(int pharmaceuticalDispensingId)
        {
            try
            {
                var result = _pharmaceuticalDispensingDetailBatchSerialRepository.GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(pharmaceuticalDispensingId);
                return result;
            }
            catch (Exception ex)
            {

                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial>();
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
                _pharmaceuticalDispensingDetailBatchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion

    }
}
