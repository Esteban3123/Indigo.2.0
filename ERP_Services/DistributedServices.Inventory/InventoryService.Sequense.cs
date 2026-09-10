using Application.Inventory.Sequense;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Infrastructure.CrossCutting.Base;
using System.ServiceModel;

namespace DistributedServices.Inventory
{
    public partial class InventoryService
    {
        /// <summary>
        /// Obtiene la configuracion de secuencia numerica asignada al frontal
        /// </summary>
        /// <param name="idForm"></param>
        /// <returns></returns>
        public Domain.Entities.InventorySequence GetSequenseByIdForm(string idForm)
        {
            using (var service = Container.Current.Resolve<IInventorySequenceAdminService>())
            {
                return service.GetSequenseByIdForm(idForm, true);
            }
            //return _sequenceAdminService.GetSequenseByIdForm(idForm);
        }

        /// <summary>
        /// Obtiene un grupo de secuencias numericas por su id de configuracion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> GetNumericSequenseGroupById(int id)
        {
            using (var service = Container.Current.Resolve<IInventorySequenceAdminService>())
            {
                return service.GetNumericSequenseGroupById(id);
            }
            //return _sequenceAdminService.GetNumericSequenseGroupById(id);
        }

        public ActionResult SaveSequence(Domain.Entities.InventorySequence seq)
        {
            using (var service = Container.Current.Resolve<IInventorySequenceAdminService>())
            {
                return service.SaveSequence(seq);
            }
            //return _sequenceAdminService.SaveSequence(seq);
        }
    }
}
