Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    ''' <summary>
    ''' Guarda o actualiza una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetPurchaseOrder(ByVal FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetPurchaseOrder) Implements IFixedAssetPurchaseOrderService.SaveFixedAssetPurchaseOrder
        Using service As IFixedAssetPurchaseOrderAdminService = Container.Current.Resolve(Of IFixedAssetPurchaseOrderAdminService)()
            Return service.SaveFixedAssetPurchaseOrder(FixedAssetPurchaseOrder, audit, idSequense)
        End Using
        'Return Me._fixedAssetPurchaseOrderAdminService.SaveFixedAssetPurchaseOrder(FixedAssetPurchaseOrder, audit, idSequense)
    End Function

    ''' <summary>
    ''' Elimina una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteFixedAssetPurchaseOrder(FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, audit As AuditMessage) As Boolean Implements IFixedAssetPurchaseOrderService.DeleteFixedAssetPurchaseOrder
        Using service As IFixedAssetPurchaseOrderAdminService = Container.Current.Resolve(Of IFixedAssetPurchaseOrderAdminService)()
            Return service.DeleteFixedAssetPurchaseOrder(FixedAssetPurchaseOrder, audit)
        End Using
        'Return Me._fixedAssetPurchaseOrderAdminService.DeleteFixedAssetPurchaseOrder(FixedAssetPurchaseOrder, audit)
    End Function

    ''' <summary>
    ''' Obtiene una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPurchaseOrder(ByVal codeFixedAssetPurchaseOrder As String) As ActionResult(Of FixedAssetPurchaseOrder) Implements IFixedAssetPurchaseOrderService.GetFixedAssetPurchaseOrder
        Using service As IFixedAssetPurchaseOrderAdminService = Container.Current.Resolve(Of IFixedAssetPurchaseOrderAdminService)()
            Return service.GetFixedAssetPurchaseOrderByCode(codeFixedAssetPurchaseOrder)
        End Using
        'Return Me._fixedAssetPurchaseOrderAdminService.GetFixedAssetPurchaseOrderByCode(codeFixedAssetPurchaseOrder)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks><code>HRR PBI4547</code></remarks>
    Public Function ListPurchaseRequestToOrder() As List(Of Domain.Entities.SP_PurchaseRequestToOrderFixedAsset_Result) Implements IFixedAssetPurchaseOrderService.ListPurchaseRequestToOrder
        Using service As IFixedAssetPurchaseOrderAdminService = Container.Current.Resolve(Of IFixedAssetPurchaseOrderAdminService)()
            Return service.ListPurchaseRequestToOrder
        End Using
    End Function

    ''' <summary>
    ''' Desconfirma la orden de compra
    ''' </summary>
    ''' <param name="FixedAssetPurchaseOrder"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UnconfirmPurchaseOrder(ByVal FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, audit As AuditMessage) As ActionResult(Of FixedAssetPurchaseOrder) Implements IFixedAssetPurchaseOrderService.UnconfirmPurchaseOrder
        Using service As IFixedAssetPurchaseOrderAdminService = Container.Current.Resolve(Of IFixedAssetPurchaseOrderAdminService)()
            Return service.UnconfirmPurchaseOrder(FixedAssetPurchaseOrder, audit)
        End Using
    End Function
End Class
