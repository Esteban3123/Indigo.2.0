'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez
' Created          : 05-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class WarehouseStockRepository
    Inherits GenericRepository(Of WarehouseStock)
    Implements IWarehouseStockRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork


    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista los stock de alamcen por id del almacen
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouseStocksByWarehouseId(warehouseId As Integer) As List(Of WarehouseStock) Implements IWarehouseStockRepository.ListWarehouseStocksByWarehouseId
        Dim res = (From ws In _context.WarehouseStock Where ws.WarehouseId = warehouseId Select ws).ToList()
        If res.Count > 0 Then
            For Each item In res
                item.OriginalValue = (From ws In _context.WarehouseStock.AsNoTracking() Where ws.Id = item.Id Select ws).FirstOrDefault
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un stock de almacen por id del almacen y id del producto
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWarehouseStockByWarehouseIdAndProductId(warehouseId As Integer, productId As Integer) As WarehouseStock Implements IWarehouseStockRepository.GetWarehouseStockByWarehouseIdAndProductId
        Dim res = (From ws In _context.WarehouseStock.AsNoTracking() Where ws.WarehouseId = warehouseId And ws.ProductId = productId Select ws).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New WarehouseStock
        End If
    End Function

End Class
