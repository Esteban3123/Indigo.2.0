//'************************************************************
//' Assembly         : Application.Inventory.AntineoplasicoMedication
//' Author           : Hector Rodriguez Rubiano
//' Created          : 04/08/2020
//'
//' Copyright        : (c) . All rights reserved.
//' About            : PBI10044
//'************************************************************

using System;
using System.Collections.Generic;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace Application.Inventory.AntineoplasicoMedication
{
    public interface IAntineoplasicoMedicationAdminService : IDisposable
    {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="listaAPM"></param>
            /// <param name="audit"></param>
            /// <returns></returns>
            ActionResult<Domain.Entities.AntineoplasicoMedication> SaveAntineoplasicoMedication(List<Domain.Entities.AntineoplasicoMedication> listaAPM, AuditMessage audit);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="statesAPM"></param>
            /// <param name="audit"></param>
            /// <returns></returns>
            Domain.Base.Entities.ActionResult<List<Domain.Entities.AntineoplasicoMedication>> GetAntineoplasicoMedicationByStates(string statesAPM, AuditMessage audit);
    }
}
