
Imports Application.FixedAsset
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService

    Public Function DeleteEquipmentType(Empresa As String, EquipmentType As FixedAssetItemType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IFixedAssetItemTypeService.DeleteEquipmentType
        Using service As IFixedAssetItemTypeAdminService = Container.Current.Resolve(Of IFixedAssetItemTypeAdminService)()
            Return service.DeleteEquipmentType(EquipmentType, audit)
        End Using
        'Return _FixedAssetItemTypeAdminService.DeleteEquipmentType(EquipmentType, audit)
    End Function

    Public Function GetEquipmentType(Empresa As String, codeEquipmentType As String) As FixedAssetItemType Implements IFixedAssetItemTypeService.GetEquipmentType
        Using service As IFixedAssetItemTypeAdminService = Container.Current.Resolve(Of IFixedAssetItemTypeAdminService)()
            Return service.GetEquipmentType(codeEquipmentType)
        End Using
        'Return _FixedAssetItemTypeAdminService.GetEquipmentType(codeEquipmentType)
    End Function

    Public Function SaveFixedAssetItemType(EquipmentType As FixedAssetItemType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IFixedAssetItemTypeService.SaveFixedAssetItemType
        Using service As IFixedAssetItemTypeAdminService = Container.Current.Resolve(Of IFixedAssetItemTypeAdminService)()
            Return service.SaveFixedAssetItemType(EquipmentType, audit)
        End Using
        'Return _FixedAssetItemTypeAdminService.GetEquipmentType(codeEquipmentType)
    End Function

    Public Function ListAllEquipmentType(Empresa As String) As List(Of FixedAssetItemType) Implements IFixedAssetItemTypeService.ListAllEquipmentType
        Using service As IFixedAssetItemTypeAdminService = Container.Current.Resolve(Of IFixedAssetItemTypeAdminService)()
            Return service.ListAllEquipmentType()
        End Using
        'Return _FixedAssetItemTypeAdminService.ListAllEquipmentType()
    End Function

    Public Function SaveEquipmentType(Empresa As String, EquipmentType As List(Of FixedAssetItemType), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IFixedAssetItemTypeService.SaveEquipmentType
        Using service As IFixedAssetItemTypeAdminService = Container.Current.Resolve(Of IFixedAssetItemTypeAdminService)()
            Return service.SaveEquipmentType(EquipmentType, audit)
        End Using
        'Return _FixedAssetItemTypeAdminService.SaveEquipmentType(EquipmentType, audit)
    End Function

    Public Function ListEquipmentTypeInventoryType(Empresa As String, IdInventoryType As Integer) As List(Of FixedAssetItemType) Implements IFixedAssetItemTypeService.ListEquipmentTypeInventoryType
        Using service As IFixedAssetItemTypeAdminService = Container.Current.Resolve(Of IFixedAssetItemTypeAdminService)()
            Return service.ListEquipmentTypeInventoryType(IdInventoryType)
        End Using
        'Return _FixedAssetItemTypeAdminService.ListEquipmentTypeInventoryType(IdInventoryType)
    End Function

    Public Function GetEquipmentTypeById(Empresa As String, IdEquipmentType As Integer) As FixedAssetItemType Implements IFixedAssetItemTypeService.GetEquipmentTypeById
        Using service As IFixedAssetItemTypeAdminService = Container.Current.Resolve(Of IFixedAssetItemTypeAdminService)()
            Return service.GetEquipmentTypeById(IdEquipmentType)
        End Using
        'Return _FixedAssetItemTypeAdminService.GetEquipmentTypeById(IdEquipmentType)
    End Function


End Class
