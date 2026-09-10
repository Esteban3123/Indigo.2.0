Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities

Partial Class MaintanceService

    Public Function DeletePoliza(Empresa As String, Poliza As Domain.Maintenance.Entities.Poliza, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IPolizaService.DeletePoliza
        Using PolizaAdmin As IPolizaAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaAdminService)()
            Return PolizaAdmin.DeletePoliza(Poliza, audit)
        End Using
    End Function

    Public Function GetPoliza(Empresa As String, codePoliza As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Maintenance.Entities.Poliza Implements IPolizaService.GetPoliza
        Using PolizaAdmin As IPolizaAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaAdminService)()
            Return PolizaAdmin.GetPoliza(codePoliza, audit)
        End Using
    End Function

    Public Function ListAllPoliza(Empresa As String) As List(Of Domain.Maintenance.Entities.Poliza) Implements IPolizaService.ListAllPoliza
        Using PolizaAdmin As IPolizaAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaAdminService)()
            Return PolizaAdmin.ListAllPoliza()
        End Using
    End Function

    Public Function SavePoliza(Empresa As String, Poliza As Domain.Maintenance.Entities.Poliza, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Maintenance.Entities.Poliza) Implements IPolizaService.SavePoliza
        Using PolizaAdmin As IPolizaAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return PolizaAdmin.SavePoliza(Poliza, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStatePoliza(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Maintenance.Entities.Poliza) Implements IPolizaService.ChangeStatePoliza
        Using PolizaAdmin As IPolizaAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaAdminService)()
            Return PolizaAdmin.ChangeStatePoliza(code, state, audit)
        End Using
    End Function

End Class
