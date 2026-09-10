//'************************************************************
//' Assembly         : Application.Inventory.BacterialResistanceMedication
//' Author           : Hector Rodriguez Rubiano
//' Created          : 06/04/2020
//'
//' Copyright        : (c) . All rights reserved.
//' About            : PBI8934
//'************************************************************

using System;
using System.Collections.Generic;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;

namespace Application.Inventory.BacterialResistanceMedication
{
    public interface IBacterialResistanceMedicationAdminService : IDisposable
    {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="listaBRM"></param>
            /// <param name="audit"></param>
            /// <returns></returns>
            ActionResult<Domain.Entities.BacterialResistanceMedication> SaveBacterialResistanceMedication(List<Domain.Entities.BacterialResistanceMedication> listaBRM, AuditMessage audit);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="statesBRM"></param>
            /// <param name="audit"></param>
            /// <returns></returns>
            Domain.Base.Entities.ActionResult<List<Domain.Entities.BacterialResistanceMedication>> GetBacterialResistanceMedicationByStates(string statesBRM, AuditMessage audit);
    }
}
