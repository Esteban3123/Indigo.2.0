'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Cristhian Mauricio Salazar
' Created          : 11/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class KardexRepository
    Inherits GenericRepository(Of Kardex)
    Implements IKardexRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un Kardex a través del ID
    ''' </summary>
    ''' <param name="id">Id del Kardex</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKardexById(id As Integer) As Kardex Implements IKardexRepository.GetKardexById
        Dim query = (From e In _context.Kardex
                     Where e.Id = id
                     Select e)
        If query.Count > 0 Then
            Return query.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene las cantidades restantes del productoId segun el almacén 
    ''' </summary>
    ''' <param name="WarehouseId"></param>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    Public Function GetQuantityKardexPoductIdAndWareHouseId(WarehouseId As List(Of Integer), ProductId As Integer) As Integer Implements IKardexRepository.GetQuantityKardexPoductIdAndWareHouseId
        Dim query = (From e In _context.Kardex
                     Where WarehouseId.Contains(e.WarehouseId) And e.ProductId = ProductId
                     Select e).ToList
        If query.Any Then
            Dim QuantityEntry As Integer = 0, QuantityExit As Integer = 0
            QuantityEntry = query.Where(Function(y) y.MovementType = 1).Sum(Function(x) x.Quantity)
            QuantityExit = query.Where(Function(y) y.MovementType = 2).Sum(Function(x) x.Quantity)

            Return QuantityEntry - QuantityExit
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Obtiene la cantidad total de productos en el kardex de una bodega
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    Public Function GetQuantityKardex(warehouseId As Integer) As Integer Implements IKardexRepository.GetQuantityKardex
        Dim count = (From e In _context.Kardex
                     Where e.WarehouseId = warehouseId
                     Select e).Count()
        Return count
    End Function
End Class
