Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Public Class FixedAssetService

    ''' <summary>
    ''' Obtiene saldo inicial por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetInitialBalance(Code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInitialBalance) Implements IFixedAssetInitialBalanceService.GetFixedAssetInitialBalance
        Using service As IFixedAssetInitialBalanceAdminService = Container.Current.Resolve(Of IFixedAssetInitialBalanceAdminService)()
            Return service.GetFixedAssetInitialBalance(Code, audit)
        End Using
        'Return Me._fixedAssetInitialBalanceAdminService.GetFixedAssetInitialBalance(Code, audit)
    End Function

    ''' <summary>
    ''' Obtiene saldo inicial por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetInitialBalanceById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInitialBalance) Implements IFixedAssetInitialBalanceService.GetFixedAssetInitialBalanceById
        Using service As IFixedAssetInitialBalanceAdminService = Container.Current.Resolve(Of IFixedAssetInitialBalanceAdminService)()
            Return service.GetFixedAssetInitialBalanceById(Id, audit)
        End Using
        'Return Me._fixedAssetInitialBalanceAdminService.GetFixedAssetInitialBalanceById(Id, audit)
    End Function

    ''' <summary>
    ''' Guarda un saldo inicial
    ''' </summary>
    ''' <param name="FixedAssetInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetInitialBalance(FixedAssetInitialBalance As Domain.Entities.FixedAssetInitialBalance, ListDeleteFixedAssetInitialBalanceItem As List(Of FixedAssetInitialBalanceItem), ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook), ListDeleteFixedAssetInitialBalanceItemDetailBook As List(Of FixedAssetInitialBalanceItemDetailBook), ListDeleteFixedAssetInitialBalanceItemParts As List(Of FixedAssetInitialBalanceItemParts), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInitialBalance) Implements IFixedAssetInitialBalanceService.SaveFixedAssetInitialBalance
        Using service As IFixedAssetInitialBalanceAdminService = Container.Current.Resolve(Of IFixedAssetInitialBalanceAdminService)()
            Return service.SaveFixedAssetInitialBalance(FixedAssetInitialBalance, ListDeleteFixedAssetInitialBalanceItem, ListDeleteFixedAssetInitialBalanceItemPartsDetailBook, ListDeleteFixedAssetInitialBalanceItemDetailBook, ListDeleteFixedAssetInitialBalanceItemParts, audit, idSequense)
        End Using
        'Return Me._fixedAssetInitialBalanceAdminService.SaveFixedAssetInitialBalance(FixedAssetInitialBalance, ListDeleteFixedAssetInitialBalanceItem, ListDeleteFixedAssetInitialBalanceItemPartsDetailBook, ListDeleteFixedAssetInitialBalanceItemDetailBook, ListDeleteFixedAssetInitialBalanceItemParts, audit, idSequense)
    End Function

    ''' <summary>
    ''' Copiar y pegar para saldos iniciales de activos fijos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SP_CopyAndPasteFixedAssetInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of FixedAssetInitialBalanceItem), List(Of Tuple(Of String, Integer))) Implements IFixedAssetInitialBalanceService.SP_CopyAndPasteFixedAssetInitialBalance
        Using service As IFixedAssetInitialBalanceAdminService = Container.Current.Resolve(Of IFixedAssetInitialBalanceAdminService)()
            Return service.SP_CopyAndPasteFixedAssetInitialBalance(data)
        End Using
        'Return Me._fixedAssetInitialBalanceAdminService.SP_CopyAndPasteFixedAssetInitialBalance(data)
    End Function

End Class
