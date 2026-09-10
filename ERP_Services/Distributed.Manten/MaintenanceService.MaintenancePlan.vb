Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Microsoft.Practices.Unity

Partial Class MaintanceService
    Implements IMaintenancePlanService

    Public Function DeleteMaintenancePlan(MaintenancePlan As Domain.Entities.MaintenancePlan, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IMaintenancePlanService.DeleteMaintenancePlan
        Using MaintenancePlanAdmin As IMaintenancePlanAdminService = Container.Current.Resolve(Of IMaintenancePlanAdminService)()
            Return MaintenancePlanAdmin.DeleteMaintenancePlan(MaintenancePlan, audit)
        End Using
    End Function

    Public Function GetMaintenancePlan(CodeMaintenancePlan As String, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional tracking As Boolean = False) As Domain.Entities.MaintenancePlan Implements IMaintenancePlanService.GetMaintenancePlan
        Using MaintenancePlanAdmin As IMaintenancePlanAdminService = Container.Current.Resolve(Of IMaintenancePlanAdminService)()
            Return MaintenancePlanAdmin.GetMaintenancePlan(CodeMaintenancePlan, tracking)
        End Using
    End Function

    Public Function SaveMaintenancePlan(MaintenancePlan As Domain.Entities.MaintenancePlan) As ActionResult(Of MaintenancePlan) Implements IMaintenancePlanService.SaveMaintenancePlan
        Using MaintenancePlanAdmin As IMaintenancePlanAdminService = Container.Current.Resolve(Of IMaintenancePlanAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return MaintenancePlanAdmin.SaveMaintenancePlan(MaintenancePlan, idSequense, audit)
        End Using
    End Function

End Class
