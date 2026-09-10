Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Microsoft.Practices.Unity
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

Partial Class MaintanceService

    Function GetEquipmentFunctionByCode(code As String, audit As AuditMessage) As ActionResult(Of EquipmentFunction) Implements IEquipmentFunctionService.GetEquipmentFunctionByCode
        Using service As IEquipmentFunctionAdminService = Container.Current.Resolve(Of IEquipmentFunctionAdminService)()
            Return service.GetEquipmentFunctionByCode(code, audit)
        End Using
    End Function

    Function SaveEquipmentFunction(EquipmentFunction As EquipmentFunction, audit As AuditMessage, Optional idSequense As Int64 = Nothing) As ActionResult(Of EquipmentFunction) Implements IEquipmentFunctionService.SaveEquipmentFunction
        Using service As IEquipmentFunctionAdminService = Container.Current.Resolve(Of IEquipmentFunctionAdminService)()
            Return service.SaveEquipmentFunction(EquipmentFunction, audit, idSequense)
        End Using
    End Function

    Public Function ListAllEquipmentFunction(Empresa As String) As List(Of Domain.Entities.EquipmentFunction) Implements IEquipmentFunctionService.ListAllEquipmentFunction
        Using service As IEquipmentFunctionAdminService = Container.Current.Resolve(Of IEquipmentFunctionAdminService)()
            Return service.ListAllEquipmentFunction
        End Using
    End Function

    Function ChangeStateEquipmentFunction(id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of EquipmentFunction) Implements IEquipmentFunctionService.ChangeStateEquipmentFunction
        Using service As IEquipmentFunctionAdminService = Container.Current.Resolve(Of IEquipmentFunctionAdminService)()
            Return service.ChangeStateEquipmentFunction(id, state, audit)
        End Using
    End Function

    Function DeleteEquipmentFunction(id As Integer, audit As AuditMessage) As ActionResult(Of EquipmentFunction) Implements IEquipmentFunctionService.DeleteEquipmentFunction
        Using service As IEquipmentFunctionAdminService = Container.Current.Resolve(Of IEquipmentFunctionAdminService)()
            Return service.DeleteEquipmentFunction(id, audit)
        End Using
    End Function

End Class
