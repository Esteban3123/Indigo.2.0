'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BatchSerialRepository
    Inherits GenericRepository(Of BatchSerial)
    Implements IBatchSerialRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Indica si existe un lote para un producto, con el codigo del lote y la fecha de vencimiento
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <param name="BatchCode"></param>
    ''' <param name="ExpirationDate"></param>
    ''' <returns></returns>
    Public Function ValidateIfExistsBatchSerial(ProductId As Integer, BatchCode As String, ExpirationDate As Date?) As Boolean Implements IBatchSerialRepository.ValidateIfExistsBatchSerial
        Dim res = (From bs In _context.BatchSerial.AsNoTracking() Where bs.ProductId = ProductId AndAlso bs.BatchCode = BatchCode AndAlso bs.ExpirationDate.Value = ExpirationDate.Value).FirstOrDefault()
        If res IsNot Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' obtiene un listado de lotes por el id del producto
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    Public Function GetBatchSerialByProductId(ProductId As Integer, DateBatchSerial As Date?, WarehouseId As Integer?, RemissionType As Integer?) As List(Of BatchSerial) Implements IBatchSerialRepository.GetBatchSerialByProductId
        Dim res As List(Of BatchSerial)

        If RemissionType IsNot Nothing AndAlso RemissionType = 2 AndAlso WarehouseId IsNot Nothing AndAlso WarehouseId > 0 Then
            If DateBatchSerial IsNot Nothing Then
                Dim _date As Date = DateBatchSerial.Value.Date
                res = (From bs In _context.BatchSerial.Include("PhysicalInventory").AsNoTracking()
                       Where bs.ProductId = ProductId AndAlso bs.ExpirationDate >= _date AndAlso bs.PhysicalInventory.Where(Function(ph) ph.WarehouseId = WarehouseId AndAlso ph.Quantity > 0).Count() > 0
                       Select bs).ToList()
            Else
                res = (From bs In _context.BatchSerial.Include("PhysicalInventory").AsNoTracking()
                       Where bs.ProductId = ProductId AndAlso bs.PhysicalInventory.Where(Function(ph) ph.WarehouseId = WarehouseId AndAlso ph.Quantity > 0).Count() > 0
                       Select bs).ToList()
            End If

            For Each bs In res
                bs.OutstandingQuantity = bs.PhysicalInventory.Where(Function(ph) ph.WarehouseId = WarehouseId AndAlso ph.Quantity > 0).Sum(Function(ph) ph.Quantity)
            Next
        Else
            If DateBatchSerial IsNot Nothing Then
                Dim _date As Date = DateBatchSerial.Value.Date
                res = (From bs In _context.BatchSerial.Include("PhysicalInventory").AsNoTracking() Where bs.ProductId = ProductId AndAlso bs.ExpirationDate >= _date Select bs).ToList()
            Else
                res = (From bs In _context.BatchSerial.Include("PhysicalInventory").AsNoTracking() Where bs.ProductId = ProductId Select bs).ToList()
            End If

            For Each bs In res
                bs.OutstandingQuantity = bs.PhysicalInventory.Where(Function(ph) ph.WarehouseId = WarehouseId AndAlso ph.Quantity > 0).Sum(Function(ph) ph.Quantity)
            Next
        End If

        Return res
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="DateBatchSerial"></param>
    ''' <param name="WarehouseId"></param>
    ''' <param name="RemissionType"></param>
    ''' <returns></returns>
    Public Function GetBatchSerialCustodyByProductId(AdmissionNumber As String, ProductId As Integer, DateBatchSerial As Date?, WarehouseId As Integer?, RemissionType As Integer?) As List(Of BatchSerial) Implements IBatchSerialRepository.GetBatchSerialCustodyByProductId
        Dim res As List(Of BatchSerial)

        If RemissionType IsNot Nothing AndAlso RemissionType = 2 AndAlso WarehouseId IsNot Nothing AndAlso WarehouseId > 0 Then
            If DateBatchSerial IsNot Nothing Then
                Dim _date As Date = DateBatchSerial.Value.Date
                res = (From bs In _context.BatchSerial.Include("PhysicalInventoryCustody").AsNoTracking()
                       Where bs.ProductId = ProductId AndAlso bs.ExpirationDate >= _date AndAlso bs.PhysicalInventoryCustody.Where(Function(ph) ph.AdmissionNumber = AdmissionNumber AndAlso ph.WarehouseId = WarehouseId AndAlso ph.Quantity > 0).Count() > 0
                       Select bs).ToList()
            Else
                res = (From bs In _context.BatchSerial.Include("PhysicalInventoryCustody").AsNoTracking()
                       Where bs.ProductId = ProductId AndAlso bs.PhysicalInventoryCustody.Where(Function(ph) ph.AdmissionNumber = AdmissionNumber AndAlso ph.WarehouseId = WarehouseId AndAlso ph.Quantity > 0).Count() > 0
                       Select bs).ToList()
            End If

            For Each bs In res
                bs.OutstandingQuantity = bs.PhysicalInventoryCustody.Where(Function(ph) ph.AdmissionNumber.Trim() = AdmissionNumber.Trim() AndAlso ph.WarehouseId = WarehouseId AndAlso ph.Quantity > 0).Sum(Function(g) g.Quantity)
            Next
        Else
            If DateBatchSerial IsNot Nothing Then
                Dim _date As Date = DateBatchSerial.Value.Date
                res = (From bs In _context.BatchSerial Where bs.ProductId = ProductId AndAlso bs.ExpirationDate >= _date Select bs).ToList()
            Else
                res = (From bs In _context.BatchSerial Where bs.ProductId = ProductId Select bs).ToList()
            End If
        End If

        Return res
    End Function

    ''' <summary>
    ''' obtiene un lote por codigo
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BatchSerialByCode(productId As Integer, code As String) As BatchSerial Implements IBatchSerialRepository.BatchSerialByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From bs In _context.BatchSerial Where bs.ProductId = productId AndAlso bs.BatchCode = code Select bs).FirstOrDefault

        If res IsNot Nothing Then
            Return res
        Else
            Return New BatchSerial
        End If
    End Function


    ''' <summary>
    ''' obtiene un listado de lotes por el id del producto
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    Public Function GetBatchSerialByProductIdIncludeExpirationDate(ProductId As Integer) As List(Of BatchSerial) Implements IBatchSerialRepository.GetBatchSerialByProductIdIncludeExpirationDate
        Dim res As List(Of BatchSerial)
        res = (From bs In _context.BatchSerial.Include("UpdateExpirationDate") Where bs.ProductId = ProductId Select bs).ToList()
        Return res
    End Function

End Class
