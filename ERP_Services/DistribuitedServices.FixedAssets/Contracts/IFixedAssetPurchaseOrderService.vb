#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetPurchaseOrderService

    <OperationContract()>
    Function DeleteFixedAssetPurchaseOrder(FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, audit As AuditMessage) As Boolean

    <OperationContract()>
    Function SaveFixedAssetPurchaseOrder(ByVal FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetPurchaseOrder)

    <OperationContract()> _
    Function GetFixedAssetPurchaseOrder(ByVal codeFixedAssetPurchaseOrder As String) As ActionResult(Of FixedAssetPurchaseOrder)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks><code>HRR PBI4547</code></remarks>
    <OperationContract()>
    Function ListPurchaseRequestToOrder() As List(Of Domain.Entities.SP_PurchaseRequestToOrderFixedAsset_Result)

    ''' <summary>
    ''' Desconfirma la orden de compra
    ''' </summary>
    ''' <param name="FixedAssetPurchaseOrder"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function UnconfirmPurchaseOrder(ByVal FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, audit As AuditMessage) As ActionResult(Of FixedAssetPurchaseOrder)


End Interface
