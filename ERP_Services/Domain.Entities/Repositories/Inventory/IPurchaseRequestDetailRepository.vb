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

Public Interface IPurchaseRequestDetailRepository
    Inherits IRepository(Of PurchaseRequestDetail)

    ''' <summary>
    ''' Obtiene una lista de detalles de solicitudes de compra de inventario por unidad funcional
    ''' </summary>
    ''' <param name="idFunctionalUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPurchaseRequestDetailByFunctionalUnit(idFunctionalUnit As Integer) As List(Of PurchaseRequestDetail)

    ''' <summary>
    ''' Obtiene un detalle de solicitud de compra de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPurchaseRequestDetailById(id As Integer) As PurchaseRequestDetail

End Interface
