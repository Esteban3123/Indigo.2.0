Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Domain.Maintenance.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

Partial Class MaintanceService

    Public Function DeleteSupplierMaintenance(Supplier As Domain.Maintenance.Entities.SupplierMaintenance, session As SessionValues) As Domain.Base.Entities.ActionResult Implements ISupplierMaintenanceService.DeleteSupplierMaintenance
        Using SupplierAdmin As ISupplierMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISupplierMaintenanceAdminService)()
            Return SupplierAdmin.DeleteSupplierMaintenance(Supplier, session)
        End Using
    End Function

    Public Function GetSupplierMaintenance(Nit As String, session As SessionValues) As Domain.Maintenance.Entities.SupplierMaintenance Implements ISupplierMaintenanceService.GetSupplierMaintenance
        Using SupplierAdmin As ISupplierMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISupplierMaintenanceAdminService)()
            Return SupplierAdmin.GetSupplierMaintenance(Nit)
        End Using
    End Function

    Public Function GetSupplierMaintenanceById(id As Integer, session As SessionValues) As Domain.Maintenance.Entities.SupplierMaintenance Implements ISupplierMaintenanceService.GetSupplierMaintenanceById
        Using SupplierAdmin As ISupplierMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISupplierMaintenanceAdminService)()
            Return SupplierAdmin.GetSupplierMaintenanceById(id)
        End Using
    End Function

    Public Function ListAllSupplierMaintenance(session As SessionValues) As List(Of Domain.Maintenance.Entities.SupplierMaintenance) Implements ISupplierMaintenanceService.ListAllSupplierMaintenance
        Using SupplierAdmin As ISupplierMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISupplierMaintenanceAdminService)()
            Return SupplierAdmin.ListAllSupplierMaintenance()
        End Using
    End Function

    Public Function SaveSupplierMaintenance(Supplier As Domain.Maintenance.Entities.SupplierMaintenance, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Maintenance.Entities.SupplierMaintenance) Implements ISupplierMaintenanceService.SaveSupplierMaintenance
        Using SupplierAdmin As ISupplierMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISupplierMaintenanceAdminService)()
            Return SupplierAdmin.SaveSupplierMaintenance(Supplier, session)
        End Using
    End Function

    Public Function ChangeState1(code As String, state As Boolean, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Maintenance.Entities.SupplierMaintenance) Implements ISupplierMaintenanceService.ChangeStateSupplierMaintenance
        Using SupplierAdmin As ISupplierMaintenanceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ISupplierMaintenanceAdminService)()
            Return SupplierAdmin.ChangeState(code, state, session)
        End Using
    End Function

End Class
