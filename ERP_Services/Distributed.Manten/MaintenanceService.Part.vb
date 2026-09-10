Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

Partial Class MaintanceService

    Public Function DeletePart(Empresa As String, Part As Domain.Maintenance.Entities.Part, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IPartService.DeletePart
        Using PartAdmin As IPartAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPartAdminService)()
            Return PartAdmin.DeletePart(Part, audit)
        End Using
    End Function

    Public Function GetPart(Empresa As String, codePart As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Maintenance.Entities.Part Implements IPartService.GetPart
        Using PartAdmin As IPartAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPartAdminService)()
            Return PartAdmin.GetPart(codePart, audit)
        End Using
    End Function

    Public Function ListAllPart(Empresa As String) As List(Of Domain.Maintenance.Entities.Part) Implements IPartService.ListAllPart
        Using PartAdmin As IPartAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPartAdminService)()
            Return PartAdmin.ListAllPart
        End Using
    End Function

    Public Function SavePart(Empresa As String, Part As Domain.Maintenance.Entities.Part, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Maintenance.Entities.Part) Implements IPartService.SavePart
        Using PartAdmin As IPartAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPartAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return PartAdmin.SavePart(Part, audit, idSequense)
        End Using
    End Function
    ''' <summary>
    ''' Changes the state1.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStatePart(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Maintenance.Entities.Part) Implements IPartService.ChangeStatePart
        Using PartAdmin As IPartAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPartAdminService)()
            Return PartAdmin.ChangeStatePart(code, state, audit)
        End Using
    End Function
End Class
