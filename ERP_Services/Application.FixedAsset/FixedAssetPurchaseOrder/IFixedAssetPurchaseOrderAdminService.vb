#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region


Public Interface IFixedAssetPurchaseOrderAdminService
    Inherits IDisposable

    '' <summary>
    ''' funcion que sirve para eliminar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteFixedAssetPurchaseOrder(ByVal FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="FixedAssetPurchaseOrder">FixedAssetPurchaseOrder</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetPurchaseOrder(ByVal FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetPurchaseOrder)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetPurchaseOrderByCode(ByVal code As String) As ActionResult(Of FixedAssetPurchaseOrder)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks><code>HRR PBI4547</code></remarks>
    Function ListPurchaseRequestToOrder() As List(Of Domain.Entities.SP_PurchaseRequestToOrderFixedAsset_Result)

    ''' <summary>
    ''' Desconfirma la orden de compra
    ''' </summary>
    ''' <param name="FixedAssetPurchaseOrder"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Function UnconfirmPurchaseOrder(ByVal FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetPurchaseOrder)


End Interface
