using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Base;
using Domain.Base.Entities;
using Domain.Entities;

namespace Application.Inventory.ShelfType
{
    public interface IShelfTypeAdminService : IDisposable
    {

		/// <summary>
		/// Consulta todos los tipos de estante
		/// </summary>
		/// <param name="audit"></param>
		/// <returns></returns>
		List<Domain.Entities.ShelfType> ListAllShelfType(AuditMessage audit);

		/// <summary>
		/// Guarda o actualiza un tipo de estante
		/// </summary>
		/// <param name="shelfType"></param>
		/// <param name="audit"></param>
		/// <param name="idSecuence"></param>
		/// <returns></returns>
		ActionResult<Domain.Entities.ShelfType> SaveShelfType(Domain.Entities.ShelfType shelfType, AuditMessage audit, Int64 idSecuence = 0);

		/// <summary>
		/// Elimina un tipo de estante
		/// </summary>
		/// <param name="shelfType"></param>
		/// <param name="audit"></param>
		/// <returns></returns>
		ActionResult DeleteShelfType(Domain.Entities.ShelfType shelfType, AuditMessage audit);

        /// <summary>
        /// Cambia el estado del tipo de estante
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ShelfType> UpdateStateShelfType(string code, bool state, AuditMessage audit);

        /// <summary>
        /// Consulta el tipo de estante por codigo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ShelfType> GetShelfType(string code, AuditMessage audit);

        /// <summary>
        /// Consulta el tipo de estante por id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="audit"></param>
        /// <returns></returns>
        ActionResult<Domain.Entities.ShelfType> GetShelfTypeById(int id, AuditMessage audit);
    }
}
