Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function DeleteEquipment(Equipment As Domain.Entities.Equipment, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IEquipmentService.DeleteEquipment
        Using EquipmentAdmin As IEquipmentAdminService = Container.Current.Resolve(Of IEquipmentAdminService)()
            Return EquipmentAdmin.DeleteEquipment(Equipment, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetEquipment(codeEquipment As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.Equipment Implements IEquipmentService.GetEquipment
        Using EquipmentAdmin As IEquipmentAdminService = Container.Current.Resolve(Of IEquipmentAdminService)()
            Return EquipmentAdmin.GetEquipment(codeEquipment)
        End Using
    End Function

    Public Function ListAllEquipment(session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Entities.Equipment) Implements IEquipmentService.ListAllEquipment
        Using EquipmentAdmin As IEquipmentAdminService = Container.Current.Resolve(Of IEquipmentAdminService)()
            Return EquipmentAdmin.ListAllEquipment()
        End Using
    End Function

    Public Function SaveEquipment(Equipment As Domain.Entities.Equipment, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Entities.Equipment) Implements IEquipmentService.SaveEquipment
        Using EquipmentAdmin As IEquipmentAdminService = Container.Current.Resolve(Of IEquipmentAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return EquipmentAdmin.SaveEquipment(Equipment, audit, idSequense)
        End Using
    End Function

    Public Function Change_StateEquipment(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.Equipment) Implements IEquipmentService.Change_StateEquipment
        Using EquipmentAdmin As IEquipmentAdminService = Container.Current.Resolve(Of IEquipmentAdminService)()
            Return EquipmentAdmin.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class
