'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 20-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IBatchSerialRepository
    Inherits IRepository(Of BatchSerial)

    ''' <summary>
    ''' Indica si existe un lote para un producto, con el codigo del lote y la fecha de vencimiento
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <param name="BatchCode"></param>
    ''' <param name="ExpirationDate"></param>
    ''' <returns></returns>
    Function ValidateIfExistsBatchSerial(ProductId As Integer, BatchCode As String, ExpirationDate As Date?) As Boolean

    ''' <summary>
    ''' obtiene un listado de lotes por el id del producto
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBatchSerialByProductId(ProductId As Integer, DateBatchSerial As Date?, WarehouseId As Integer?, RemissionType As Integer?) As List(Of BatchSerial)

    ''' <summary>
    ''' obtiene un lote por el codigo del lote
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function BatchSerialByCode(productId As Integer, code As String) As BatchSerial

    ''' <summary>
    ''' obtiene un listado de lotes por el id del producto
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    Function GetBatchSerialByProductIdIncludeExpirationDate(ProductId As Integer) As List(Of BatchSerial)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="DateBatchSerial"></param>
    ''' <param name="WarehouseId"></param>
    ''' <param name="RemissionType"></param>
    ''' <returns></returns>
    Function GetBatchSerialCustodyByProductId(AdmissionNumber As String, ProductId As Integer, DateBatchSerial As Date?, WarehouseId As Integer?, RemissionType As Integer?) As List(Of BatchSerial)

End Interface
