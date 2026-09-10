'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 13-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface IPurchaseOrderRepository
    Inherits IRepository(Of PurchaseOrder)

    ''' <summary>
    ''' Obtiene una Orden de Compra por Código
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>PurchaseOrder</returns>
    ''' <remarks></remarks>
    Function GetPurchaseOrderByCode(Code As String) As PurchaseOrder

    ''' <summary>
    ''' Obtiene una Orden de Compra por Id
    ''' </summary>
    ''' <param name="Id">Id</param>
    ''' <returns>PurchaseOrder</returns>
    ''' <remarks></remarks>
    Function GetPurchaseOrderById(Id As Integer) As PurchaseOrder

    ''' <summary>
    ''' Obtiene la cantidad de documentos asociados que tiene la orden de compra
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetQuantityAsociateDocument(code As String) As ActionResult

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="xmlPurchaseOrder"></param>
    ''' <returns></returns>
    Function ConfirmPurchaseOrder(xmlPurchaseOrder As string) As SP_ConfirmPurchaseOrder_Result

End Interface
