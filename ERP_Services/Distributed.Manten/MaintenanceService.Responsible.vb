Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

Partial Class MaintanceService

    Public Function DeleteResponsible(Empresa As String, Responsible As Domain.Maintenance.Entities.Responsible, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IResponsibleService.DeleteResponsible
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.DeleteResponsible(Responsible, audit)
        End Using
    End Function

    Public Function GetResponsible(Empresa As String, codeAResponsible As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Maintenance.Entities.Responsible Implements IResponsibleService.GetResponsible
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.GetResponsible(codeAResponsible, audit)
        End Using
    End Function

    Public Function ListAllResponsible(Empresa As String) As List(Of Domain.Maintenance.Entities.Responsible) Implements IResponsibleService.ListAllResponsible
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return ResponsibleAdmin.ListAllResponsible()
        End Using
    End Function

    Public Function SaveResponsible(Empresa As String, Responsible As Domain.Maintenance.Entities.Responsible, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Maintenance.Entities.Responsible) Implements IResponsibleService.SaveResponsible
        Using ResponsibleAdmin As IResponsibleAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return ResponsibleAdmin.SaveResponsible(Responsible, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStateResponsible(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Maintenance.Entities.Responsible) Implements IResponsibleService.ChangeStateResponsible
        Using PartAdmin As IResponsibleAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IResponsibleAdminService)()
            Return PartAdmin.ChangeStateResponsible(code, state, audit)
        End Using
    End Function

End Class
