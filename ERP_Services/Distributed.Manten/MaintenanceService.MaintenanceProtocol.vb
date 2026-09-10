Imports DistributedServices.Maintenance
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class MaintanceService
    Implements IMaintenanceProtocolService

    Public Function SaveMaintenanceProtocol(maintenanceProtocol As MaintenanceProtocol, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceProtocol) Implements IMaintenanceProtocolService.SaveMaintenanceProtocol
        Using service As Application.Maintenance.IMaintenanceProtocolAdminService = Container.Current.Resolve(Of Application.Maintenance.IMaintenanceProtocolAdminService)()
            Return service.SaveMaintenanceProtocol(maintenanceProtocol, audit, idSequense)
        End Using
    End Function

    Public Function DeleteMaintenanceProtocol(maintenanceProtocol As MaintenanceProtocol, audit As AuditMessage) As ActionResult Implements IMaintenanceProtocolService.DeleteMaintenanceProtocol
        Using service As Application.Maintenance.IMaintenanceProtocolAdminService = Container.Current.Resolve(Of Application.Maintenance.IMaintenanceProtocolAdminService)()
            Return service.DeleteMaintenanceProtocol(maintenanceProtocol, audit)
        End Using
    End Function

    Public Function GetMaintenanceProtocol(code As String, audit As AuditMessage) As MaintenanceProtocol Implements IMaintenanceProtocolService.GetMaintenanceProtocol
        Using service As Application.Maintenance.IMaintenanceProtocolAdminService = Container.Current.Resolve(Of Application.Maintenance.IMaintenanceProtocolAdminService)()
            Return service.GetMaintenanceProtocol(code, audit)
        End Using
    End Function

    Public Function GetMaintenanceProtocolById(id As Integer) As MaintenanceProtocol Implements IMaintenanceProtocolService.GetMaintenanceProtocolById
        Using service As Application.Maintenance.IMaintenanceProtocolAdminService = Container.Current.Resolve(Of Application.Maintenance.IMaintenanceProtocolAdminService)()
            Return service.GetMaintenanceProtocolById(id)
        End Using
    End Function

    Public Function ChangeStateMaintenanceProtocol(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of MaintenanceProtocol) Implements IMaintenanceProtocolService.ChangeStateMaintenanceProtocol
        Using service As Application.Maintenance.IMaintenanceProtocolAdminService = Container.Current.Resolve(Of Application.Maintenance.IMaintenanceProtocolAdminService)()
            Return service.ChangeStateMaintenanceProtocol(code, state, audit)
        End Using
    End Function
End Class
