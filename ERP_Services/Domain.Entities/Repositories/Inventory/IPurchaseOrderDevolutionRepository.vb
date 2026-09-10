
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

Public Interface IPurchaseOrderDevolutionRepository
    Inherits IRepository(Of PurchaseOrderDevolution)

    Function GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(PurchaseOrderDevolutionId As Integer) As List(Of PurchaseOrderDevolutionDetail)

    ''' <summary>
    ''' Obtiene una devolucion de Orden de Compra por Código
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>PurchaseOrder</returns>
    ''' <remarks></remarks>
    Function GetPurchaseOrderDevolutionByCode(Code As String) As PurchaseOrderDevolution

    ''' <summary>
    ''' Obtiene una devolucion de Orden de Compra por Id
    ''' </summary>
    ''' <param name="Id">Id</param>
    ''' <returns>PurchaseOrder</returns>
    ''' <remarks></remarks>
    Function GetPurchaseOrderDevolutionById(Id As Integer) As PurchaseOrderDevolution

End Interface
