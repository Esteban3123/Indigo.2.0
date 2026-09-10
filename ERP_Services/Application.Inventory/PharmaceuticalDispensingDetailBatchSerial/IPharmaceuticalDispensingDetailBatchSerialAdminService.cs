//***********************************************************************
// Assembly         : Domain.Inventory
// Author           : Diego Andrés Roldán Lozano
// Created          : 28-01-2015
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
using Domain.Crystal.Entities;

namespace Application.Inventory.PharmaceuticalDispensingDetailBatchSerial
{
    public interface IPharmaceuticalDispensingDetailBatchSerialAdminService : IDisposable
    {
        /// <summary>
        /// obtiene los productos para la devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="productCode"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> ListPharmaceuticalDispensingDetailBatchSerialDevolution(string admissionNumber, string functionalUnitCode, string productCode, int productType, int userId, string batchCode = "");
        /// <summary>
        /// lista los detalle de las dispensacion para hacer devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <returns></returns>
        List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(string admissionNumber);
        /// <summary>
        /// obtiene un detalla por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.Entities.PharmaceuticalDispensingDetailBatchSerial GetPharmaceuticalDispensingDetailBatchSerialById(int id);
        /// <summary>
        /// obtiene los detalle para hacer la devolucion
        /// </summary>
        /// <param name="admissionNumber"></param>
        /// <param name="productCode"></param>
        /// <param name="batchCode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Domain.Base.Entities.ActionResult<List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial>> GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(string admissionNumber, string productCode, string batchCode, int userId);

        /// <summary>
        /// Lista por el Id de la dispensación
        /// </summary>
        /// <param name="pharmaceuticalDispensingId">Id de la dispensación</param>
        /// <returns>Lista a retornar</returns>
        List<Domain.Entities.PharmaceuticalDispensingDetailBatchSerial> GetPharmaceuticalDispensingDetailBatchSerialByPharmaceuticalDispensingId(int pharmaceuticalDispensingId);
    }
}
