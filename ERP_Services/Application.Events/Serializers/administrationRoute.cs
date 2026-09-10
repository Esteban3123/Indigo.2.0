using Application.Events.Models;
using Application.Events.Models.AdministrationRoute;
using System;

namespace Application.Events.Serializers
{
    public class administrationRoute : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.AdministrationRoute administrationRouteEntity = obj as Domain.Entities.AdministrationRoute;

            MadministrationRoute madministrationRoute = new MadministrationRoute();
            MethodsMedicament MethodsMedicament = new MethodsMedicament();
            madministrationRoute.Code = administrationRouteEntity.Code;
            madministrationRoute.Name = administrationRouteEntity.Name;
            madministrationRoute.Status = Convert.ToInt16(administrationRouteEntity.Status);
            madministrationRoute.CreationUser = administrationRouteEntity.CreationUser;
            madministrationRoute.CreationDate = Convert.ToString(administrationRouteEntity.CreationDate);
            madministrationRoute.ModificationUser = administrationRouteEntity.ModificationUser;
            madministrationRoute.ModificationDate = Convert.ToString(administrationRouteEntity.ModificationDate);
            madministrationRoute.CrystalAdministrationRoute = administrationRouteEntity.CrystalAdministrationRoute;
            madministrationRoute.PharmaceuticalForm = MethodsMedicament.GeneratePharmaceuticalForm(administrationRouteEntity.PharmaceuticalFormId);
            return madministrationRoute;
        }
    }
}
