Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance

Partial Class MaintanceService

    Public Function DeleteEquipmentType(Empresa As String, EquipmentType As Domain.Entities.EquipmentType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IEquipmentTypeService.DeleteEquipmentType
        Using EquipmentServiceAdmin As IEquipmentTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IEquipmentTypeAdminService)()
            Return EquipmentServiceAdmin.DeleteEquipmentType(EquipmentType, audit)
        End Using
    End Function

    Public Function GetEquipmentType(Empresa As String, codeEquipmentType As String) As Domain.Entities.EquipmentType Implements IEquipmentTypeService.GetEquipmentType
        Using EquipmentServiceAdmin As IEquipmentTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IEquipmentTypeAdminService)()
            Return EquipmentServiceAdmin.GetEquipmentType(codeEquipmentType)
        End Using
    End Function

    Public Function ListAllEquipmentType(Empresa As String) As List(Of Domain.Entities.EquipmentType) Implements IEquipmentTypeService.ListAllEquipmentType
        Using EquipmentServiceAdmin As IEquipmentTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IEquipmentTypeAdminService)()
            Return EquipmentServiceAdmin.ListAllEquipmentType()
        End Using
    End Function

    Public Function SaveEquipmentType(Empresa As String, EquipmentType As List(Of Domain.Entities.EquipmentType), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IEquipmentTypeService.SaveEquipmentType
        Using EquipmentServiceAdmin As IEquipmentTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IEquipmentTypeAdminService)()
            Return EquipmentServiceAdmin.SaveEquipmentType(EquipmentType, audit)
        End Using
    End Function

    Public Function ListEquipmentTypeInventoryType(Empresa As String, IdInventoryType As Integer) As List(Of Domain.Entities.EquipmentType) Implements IEquipmentTypeService.ListEquipmentTypeInventoryType
        Using EquipmentServiceAdmin As IEquipmentTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IEquipmentTypeAdminService)()
            Return EquipmentServiceAdmin.ListEquipmentTypeInventoryType(IdInventoryType)
        End Using
    End Function

    Public Function GetEquipmentTypeById(Empresa As String, IdEquipmentType As Integer) As Domain.Entities.EquipmentType Implements IEquipmentTypeService.GetEquipmentTypeById
        Using EquipmentServiceAdmin As IEquipmentTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IEquipmentTypeAdminService)()
            Return EquipmentServiceAdmin.GetEquipmentTypeById(IdEquipmentType)
        End Using
    End Function

End Class
