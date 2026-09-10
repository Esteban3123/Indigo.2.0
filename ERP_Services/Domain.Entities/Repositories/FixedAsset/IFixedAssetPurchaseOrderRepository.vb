'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05/02/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetPurchaseOrderRepository

    Inherits IRepository(Of FixedAssetPurchaseOrder)

    ''' <summary>
    ''' Obtiene una Ubicación por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetPurchaseOrderByCode(code As String, Optional tracking As Boolean = True) As FixedAssetPurchaseOrder

    Function GetFixedAssetPurchaseOrderItemById(IdPurchaseOrder As Integer, Optional tracking As Boolean = True) As FixedAssetPurchaseOrderItem

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks><code>HRR PBI4547</code></remarks>
    Function ListPurchaseRequestToOrder() AS List(OF Domain.Entities.SP_PurchaseRequestToOrderFixedAsset_Result)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="xmlPurchaseOrder"></param>
    ''' <returns></returns>
    ''' <remarks><code>HRR PBI4547</code></remarks>
    Function ConfirmPurchaseOrder(xmlPurchaseOrder As string) As SP_ConfirmPurchaseOrderFixedAsset_Result

End Interface
