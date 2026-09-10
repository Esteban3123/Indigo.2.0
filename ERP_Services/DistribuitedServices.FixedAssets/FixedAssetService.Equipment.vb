Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Public Class FixedAssetService
    Public Function DeleteEquipment(Equipment As Domain.Entities.FixedAssetItem, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult Implements IFixedAssetItemService.DeleteEquipment
        Using service As IFixedAssetItemAdminService = Container.Current.Resolve(Of IFixedAssetItemAdminService)()
            Return service.DeleteEquipment(Equipment, session.AuditMessageWcf)
        End Using
        'Return Me._FixedAssetItemAdminService.DeleteEquipment(Equipment, session.AuditMessageWcf)
    End Function

    Public Function GetEquipment(codeEquipment As String) As ActionResult(Of FixedAssetItem) Implements IFixedAssetItemService.GetEquipment
        Using service As IFixedAssetItemAdminService = Container.Current.Resolve(Of IFixedAssetItemAdminService)()
            Return service.GetEquipment(codeEquipment)
        End Using
        'Return Me._FixedAssetItemAdminService.GetEquipment(codeEquipment)
    End Function

    Public Function ListAllEquipment(session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Entities.FixedAssetItem) Implements IFixedAssetItemService.ListAllEquipment
        Using service As IFixedAssetItemAdminService = Container.Current.Resolve(Of IFixedAssetItemAdminService)()
            Return service.ListAllEquipment()
        End Using
    End Function

    Public Function SaveEquipment(Equipment As Domain.Entities.FixedAssetItem, session As Infrastructure.CrossCutting.Base.SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.FixedAssetItem) Implements IFixedAssetItemService.SaveEquipment
        Using service As IFixedAssetItemAdminService = Container.Current.Resolve(Of IFixedAssetItemAdminService)()
            Return service.SaveEquipment(Equipment, audit, idSequense)
        End Using
        'Return Me._FixedAssetItemAdminService.SaveEquipment(Equipment, audit, idSequense)
    End Function


    Public Function Change_StateEquipment(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.FixedAssetItem) Implements IFixedAssetItemService.Change_StateEquipment
        Using service As IFixedAssetItemAdminService = Container.Current.Resolve(Of IFixedAssetItemAdminService)()
            Return service.ChangeState(code, state, session.AuditMessageWcf)
        End Using
        'Return _FixedAssetItemAdminService.ChangeState(code, state, session.AuditMessageWcf)
    End Function

    Public Function GetItemCostsPerCurrency(itemId As Integer) As ActionResult(Of List(Of Tuple(Of Decimal, Currency))) Implements IFixedAssetItemService.GetItemCostsPerCurrency
        Using service As IFixedAssetItemAdminService = Container.Current.Resolve(Of IFixedAssetItemAdminService)()
            Return service.GetItemCostsPerCurrency(itemId)
        End Using
    End Function
End Class
