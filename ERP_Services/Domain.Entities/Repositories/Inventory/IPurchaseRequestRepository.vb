'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Hector Rodriguez R
' Created          : 11-04-2019
'
' Copyright        : (c) . All rights reserved.
' About            : PBI3499
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IPurchaseRequestRepository
    Inherits IRepository(Of PurchaseRequest)

    ''' <summary>
    ''' Obtiene una solicitud de compra de inventario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPurchaseRequestByCode(code As String) As PurchaseRequest

    ''' <summary>
    ''' Obtiene una solicitud de compra de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPurchaseRequestById(id As Integer) As PurchaseRequest


End Interface
