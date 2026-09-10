Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

Partial Class MaintanceService

    Public Function DeletePolizaType(Empresa As String, PolizaType As Domain.Maintenance.Entities.PolizaType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IPolizaTypeService.DeletePolizaType
        Using PolizaAdmin As IPolizaTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaTypeAdminService)()
            Return PolizaAdmin.DeletePolizaType(PolizaType, audit)
        End Using
    End Function

    Public Function GetPolizaType(Empresa As String, codePolizaType As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Maintenance.Entities.PolizaType Implements IPolizaTypeService.GetPolizaType
        Using PolizaAdmin As IPolizaTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaTypeAdminService)()
            Return PolizaAdmin.GetPolizaType(codePolizaType, audit)
        End Using
    End Function

    Public Function ListAllPolizaType(Empresa As String) As List(Of Domain.Maintenance.Entities.PolizaType) Implements IPolizaTypeService.ListAllPolizaType
        Using PolizaAdmin As IPolizaTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaTypeAdminService)()
            Return PolizaAdmin.ListAllPolizaType
        End Using
    End Function

    Public Function SavePolizaType(Empresa As String, PolizaType As Domain.Maintenance.Entities.PolizaType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Maintenance.Entities.PolizaType) Implements IPolizaTypeService.SavePolizaType
        Using PolizaAdmin As IPolizaTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaTypeAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return PolizaAdmin.SavePolizaType(PolizaType, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStatePolizaType(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Maintenance.Entities.PolizaType) Implements IPolizaTypeService.ChangeStatePolizaType
        Using PolizaAdmin As IPolizaTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPolizaTypeAdminService)()
            Return PolizaAdmin.ChangeStatePoliza(code, state, audit)
        End Using
    End Function

End Class
