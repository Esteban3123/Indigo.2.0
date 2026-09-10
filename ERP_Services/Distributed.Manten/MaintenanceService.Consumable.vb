Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Partial Class MaintanceService

    Public Function DeleteConsumable(Empresa As String, Consumable As Domain.Entities.Consumable, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IConsumableService.DeleteConsumable
        Using ConsumableAdmin As IConsumableAdminService = Container.Current.Resolve(Of IConsumableAdminService)()
            Return ConsumableAdmin.DeleteConsumable(Consumable, audit)
        End Using
    End Function

    Public Function GetConsumable(Empresa As String, codeConsumable As String) As Domain.Entities.Consumable Implements IConsumableService.GetConsumable
        Using ConsumableAdmin As IConsumableAdminService = Container.Current.Resolve(Of IConsumableAdminService)()
            Return ConsumableAdmin.GetConsumable(codeConsumable)
        End Using
    End Function

    Public Function ListAllConsumable(Empresa As String) As List(Of Domain.Entities.Consumable) Implements IConsumableService.ListAllConsumable
        Using ConsumableAdmin As IConsumableAdminService = Container.Current.Resolve(Of IConsumableAdminService)()
            Return ConsumableAdmin.ListAllConsumable
        End Using
    End Function

    Public Function SaveConsumable(Consumable As Domain.Entities.Consumable, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Entities.Consumable) Implements IConsumableService.SaveConsumable
        Using ConsumableAdmin As IConsumableAdminService = Container.Current.Resolve(Of IConsumableAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return ConsumableAdmin.SaveConsumable(Consumable, audit, idSequense)
        End Using
    End Function

    Public Function Change_StateConsumable(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.Consumable) Implements IConsumableService.Change_StateConsumable
        Using ConsumableAdmin As IConsumableAdminService = Container.Current.Resolve(Of IConsumableAdminService)()
            Return ConsumableAdmin.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class
