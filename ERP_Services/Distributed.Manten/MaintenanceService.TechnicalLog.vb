Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function DeleteTechnicalLog(Empresa As String, TechnicalLog As Domain.Entities.TechnicalLog, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements ITechnicalLogService.DeleteTechnicalLog
        Using TechnicalLogAdmin As ITechnicalLogAdminService = Container.Current.Resolve(Of ITechnicalLogAdminService)()
            Return TechnicalLogAdmin.DeleteTechnicalLog(TechnicalLog, audit)
        End Using
    End Function

    Public Function GetTechnicalLog(Empresa As String, codeTechnicalLog As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.TechnicalLog Implements ITechnicalLogService.GetTechnicalLog
        Using TechnicalLogAdmin As ITechnicalLogAdminService = Container.Current.Resolve(Of ITechnicalLogAdminService)()
            Return TechnicalLogAdmin.GetTechnicalLog(codeTechnicalLog, audit)
        End Using
    End Function

    Public Function ListAllTechnicalLog(Empresa As String) As List(Of Domain.Entities.TechnicalLog) Implements ITechnicalLogService.ListAllTechnicalLog
        Using TechnicalLogAdmin As ITechnicalLogAdminService = Container.Current.Resolve(Of ITechnicalLogAdminService)()
            Return TechnicalLogAdmin.ListAllTechnicalLog
        End Using
    End Function

    Public Function SaveTechnicalLog(Empresa As String, TechnicalLog As Domain.Entities.TechnicalLog, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Entities.TechnicalLog) Implements ITechnicalLogService.SaveTechnicalLog
        Using TechnicalLogAdmin As ITechnicalLogAdminService = Container.Current.Resolve(Of ITechnicalLogAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return TechnicalLogAdmin.SaveTechnicalLog(TechnicalLog, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStateTechnicalLog(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TechnicalLog) Implements ITechnicalLogService.ChangeStateTechnicalLog
        Using TechnicalLogAdmin As ITechnicalLogAdminService = Container.Current.Resolve(Of ITechnicalLogAdminService)()
            Return TechnicalLogAdmin.ChangeStateTechnicalLog(code, state, audit)
        End Using
    End Function
End Class
