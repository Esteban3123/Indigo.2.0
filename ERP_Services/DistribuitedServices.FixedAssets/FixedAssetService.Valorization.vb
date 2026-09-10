
Imports Application.FixedAsset
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService

    ''' <summary>
    ''' Anula
    ''' </summary>
    ''' <param name="ValorizationDevaluation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnnularValorizationDevaluation(ValorizationDevaluation As Domain.Entities.ValorizationDevaluation, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ValorizationDevaluation) Implements IFixedAssetValorizationService.AnnularValorizationDevaluation
        Using service As IFixedAssetValorizationAdminService = Container.Current.Resolve(Of IFixedAssetValorizationAdminService)()
            Return service.AnnularValorizationDevaluation(ValorizationDevaluation, audit)
        End Using
        'Return Me._fixedAssetValorizationDesvalorizationAdminService.AnnularValorizationDevaluation(ValorizationDevaluation, audit)
    End Function

    ''' <summary>
    ''' Confirma
    ''' </summary>
    ''' <param name="FixedAssetTransaction"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmValorizationDevaluation(FixedAssetTransaction As Domain.Entities.FixedAssetTransaction, ByVal ListDeleteFixedAssetTransactionDetailBook As List(Of Domain.Entities.FixedAssetTransactionDetailBook), ByVal ListDeleteFixedTransactionDetail As List(Of Domain.Entities.FixedAssetTransactionDetail), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransaction) Implements IFixedAssetValorizationService.ConfirmValorizationDevaluation
        Using service As IFixedAssetValorizationAdminService = Container.Current.Resolve(Of IFixedAssetValorizationAdminService)()
            Return service.ConfirmValorizationDevaluation(FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedTransactionDetail, audit, idSequense)
        End Using
        'Return Me._fixedAssetValorizationDesvalorizationAdminService.ConfirmValorizationDevaluation(FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedTransactionDetail, audit, idSequense)
    End Function

    ''' <summary>
    ''' Obtiene por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValorizationDevaluationByCode(Code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransaction) Implements IFixedAssetValorizationService.GetValorizationDevaluationByCode
        Using service As IFixedAssetValorizationAdminService = Container.Current.Resolve(Of IFixedAssetValorizationAdminService)()
            Return service.GetValorizationDevaluationByCode(Code, audit)
        End Using
        'Return Me._fixedAssetValorizationDesvalorizationAdminService.GetValorizationDevaluationByCode(Code, audit)
    End Function

    ''' <summary>
    ''' Obtiene por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValorizationDevaluationById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ValorizationDevaluation) Implements IFixedAssetValorizationService.GetValorizationDevaluationById
        Using service As IFixedAssetValorizationAdminService = Container.Current.Resolve(Of IFixedAssetValorizationAdminService)()
            Return service.GetValorizationDevaluationById(Id, audit)
        End Using
        'Return Me._fixedAssetValorizationDesvalorizationAdminService.GetValorizationDevaluationById(Id, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza
    ''' </summary>
    ''' <param name="FixedAssetTransaction"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveValorizationDevaluation(FixedAssetTransaction As Domain.Entities.FixedAssetTransaction, ByVal ListDeleteFixedAssetTransactionDetailBook As List(Of Domain.Entities.FixedAssetTransactionDetailBook), ByVal ListDeleteFixedTransactionDetail As List(Of Domain.Entities.FixedAssetTransactionDetail), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransaction) Implements IFixedAssetValorizationService.SaveValorizationDevaluation
        Using service As IFixedAssetValorizationAdminService = Container.Current.Resolve(Of IFixedAssetValorizationAdminService)()
            Return service.SaveValorizationDevaluation(FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedTransactionDetail, audit, idSequense)
        End Using
        'Return Me._fixedAssetValorizationDesvalorizationAdminService.SaveValorizationDevaluation(FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedTransactionDetail, audit, idSequense)
    End Function

End Class
