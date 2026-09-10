///************************************************************
/// Assembly         : DistributedServices.Inventory
/// Author           : Diego Andrés Roldán Lozano
/// Created          : 2023-03-24
///
/// Copyright        : (c) . All rights reserved.
///************************************************************
///
using Application.Inventory.RequestParam;
using DistributedServices.Inventory.Contracts;
using DistributedServices.Inventory.Unity;
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace DistributedServices.Inventory
{
    public partial class InventoryService : IInventoryRequestParam
    {
        public ActionResult DeleteRequestParam(RequestParam requestParam, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IRequestParamAdminService>())
            {
                return service.DeleteRequestParam(requestParam, audit);
            }
        }

        public RequestParam GetRequestParamByCode(string code)
        {
            using (var service = Container.Current.Resolve<IRequestParamAdminService>())
            {
                return service.GetRequestParamByCode(code);
            }
        }

        public RequestParam GetRequestParamById(int id)
        {
            using (var service = Container.Current.Resolve<IRequestParamAdminService>())
            {
                return service.GetRequestParamById(id);
            }
        }

        public ActionResult<List<RequestParamProduct>> LoadRequestParamProductByImportData(List<List<object>> data)
        {
            using (var service = Container.Current.Resolve<IRequestParamAdminService>())
            {
                return service.LoadRequestParamProductByImportData(data);
            }
        }

        public ActionResult<RequestParam> SaveRequestParam(RequestParam requestParam, AuditMessage audit, long idSecuence = 0)
        {
            using (var service = Container.Current.Resolve<IRequestParamAdminService>())
            {
                return service.SaveRequestParam(requestParam, audit, idSecuence);
            }
        }

        public ActionResult<RequestParam> UpdateStateRequestParam(string code, bool state, AuditMessage audit)
        {
            using (var service = Container.Current.Resolve<IRequestParamAdminService>())
            {
                return service.UpdateStateRequestParam(code, state, audit);
            }
        }
    }
}
