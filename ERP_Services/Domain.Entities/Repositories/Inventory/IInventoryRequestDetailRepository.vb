'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 25-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IInventoryRequestDetailRepository
    Inherits IRepository(Of InventoryRequestDetail)

    ''' <summary>
    ''' Obtiene una lista de detalles de solicitudes de inventario por almacen o unidad funcional de destino
    ''' </summary>
    ''' <param name="idFilter"></param>
    ''' <param name="orderType"></param>
    ''' <param name="dispatchTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListInventoryRequestDetailByTarget(idFilter As Integer, orderType As Integer, dispatchTo As Integer) As List(Of InventoryRequestDetail)

    ''' <summary>
    ''' Obtiene un detalle de solicitud de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryRequestDetailById(id As Integer) As InventoryRequestDetail

End Interface
