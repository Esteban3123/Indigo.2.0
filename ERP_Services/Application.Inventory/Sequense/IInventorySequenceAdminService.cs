//***********************************************************************
// Assembly         : Application.FixedAsset
// Author           : Juan F. Tamayo
// Created          : 2014-01-15
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

using Domain.Base.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Inventory.Sequense
{
    public interface IInventorySequenceAdminService : IDisposable
    {
        /// <summary>
        /// Obtiene secuencia numerica para el formulario
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
        InventorySequence GetSequenseByIdForm(string idForm, bool readUnCommit = false);

        /// <summary>
        /// Obtiene el id de la secuencia numérica por el tag del formulario
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
        int GetCurrentSequenceByIdForm(string idForm, int operativeUnitId);

        /// <summary>
        /// Obtiene un grupo de secuencias numericas por su id de configuracion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<string> GetNumericSequenseGroupById(Int32 id);

        ActionResult SaveSequence(InventorySequence seq);

        InventorySequenceDetail GetSequenseByPrefix(string prefix, int idsequence);
    }
}
