'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 05-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IWarehouseStockRepository
    Inherits IRepository(Of WarehouseStock)

    ''' <summary>
    ''' Lista los stock de almacen por id de almacen
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListWarehouseStocksByWarehouseId(warehouseId As Integer) As List(Of WarehouseStock)

    ''' <summary>
    ''' Obtiene un stock de almacen por id del almacen y id del producto
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetWarehouseStockByWarehouseIdAndProductId(warehouseId As Integer, productId As Integer) As WarehouseStock

End Interface
