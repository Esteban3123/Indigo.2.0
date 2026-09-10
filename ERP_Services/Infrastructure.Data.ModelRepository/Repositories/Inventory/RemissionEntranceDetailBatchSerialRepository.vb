'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Henry Alejandro Vargas Polania
' Created          : 28-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class RemissionEntranceDetailBatchSerialRepository
    Inherits GenericRepository(Of RemissionEntranceDetailBatchSerial)
    Implements IRemissionEntranceDetailBatchSerialRepository



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
    ''' Lista los productos de la remision de entrada por lote
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="idSupplierDistributionLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine(idSupplier As Integer, idSupplierDistributionLine As Integer) As List(Of RemissionEntranceDetailBatchSerial) Implements IRemissionEntranceDetailBatchSerialRepository.ListRemissionEntranceDetailBatchSerialBySupplierAndSupplierDistributionLine
        Dim status As Integer = 2
        Dim res = (From redb In _context.RemissionEntranceDetailBatchSerial
                   Join red In _context.RemissionEntranceDetail On redb.RemissionEntranceDetailId Equals red.Id
                   Join re In _context.RemissionEntrance On red.RemissionEntranceId Equals re.Id
                   Where re.SupplierId = idSupplier And re.SupplierDistributionLineId = idSupplierDistributionLine And re.Status = status And redb.OutstandingQuantity > 0 Select redb).ToList()

        If res.Count > 0 Then
            For Each item In res
                Dim remissionEntranceDetail = (From red In _context.RemissionEntranceDetail.AsNoTracking() Where red.Id = item.RemissionEntranceDetailId Select red).FirstOrDefault()
                Dim remissionEntrance = (From re In _context.RemissionEntrance.AsNoTracking() Where re.Id = remissionEntranceDetail.RemissionEntranceId Select re).FirstOrDefault()
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = remissionEntranceDetail.ProductId Select p).FirstOrDefault()
                Dim batch = IIf(item.BatchSerialId IsNot Nothing, (From b In _context.BatchSerial.AsNoTracking() Where b.Id = item.BatchSerialId Select b).FirstOrDefault(), Nothing)

                item.Code = remissionEntrance.Code
                item.CodeNameProduct = product.Code + " - " + product.Name
                item.ProductId = product.Id
                If batch IsNot Nothing Then
                    item.CodeBatchSerial = batch.BatchCode
                End If

            Next
        End If
        Return res
    End Function


    ''' <summary>
    ''' lista los detalles del detalle de la remision de entrada
    ''' </summary>
    ''' <param name="RemissionEntranceId"></param>
    ''' <returns></returns>
    Public Function ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(RemissionEntranceId As Integer) As List(Of RemissionEntranceDetailBatchSerial) Implements IRemissionEntranceDetailBatchSerialRepository.ListRemissionEntranceDetailBatchSerialByRemissionEntranceId
        Dim quantity As Integer = 0
        Dim status As Integer = 2
        Dim res = (From redbs In _context.RemissionEntranceDetailBatchSerial
                  Join red In _context.RemissionEntranceDetail On redbs.RemissionEntranceDetailId Equals red.Id
                  Join re In _context.RemissionEntrance On red.RemissionEntranceId Equals re.Id
                  Where re.Id = RemissionEntranceId Select redbs).ToList()
        'Where re.Status = status And re.Id = RemissionEntranceId And redbs.OutstandingQuantity > quantity Select redbs).ToList()
        For Each item In res
            Dim remissionDetail = (From rd In _context.RemissionEntranceDetail.AsNoTracking() Where rd.Id = item.RemissionEntranceDetailId Select rd).FirstOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = remissionDetail.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            item.ProductId = product.Id
            item.QuantityDeliver = item.OutstandingQuantity
            If item.BatchSerialId IsNot Nothing Then
                item.CodeBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs.BatchCode).FirstOrDefault()
            End If
        Next
        Return res
    End Function


    ''' <summary>
    ''' obtiene una detalla del detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetRemissionEntranceDetailBatchSerialById(Id As Integer) As RemissionEntranceDetailBatchSerial Implements IRemissionEntranceDetailBatchSerialRepository.GetRemissionEntranceDetailBatchSerialById
        Return (From redb In _context.RemissionEntranceDetailBatchSerial Where redb.Id = Id Select redb).FirstOrDefault()
    End Function

    ''' <summary>
    ''' lista los detalles del detalle de la remision de entrada por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode(code As String) As List(Of RemissionEntranceDetailBatchSerial) Implements IRemissionEntranceDetailBatchSerialRepository.ListRemissionEntranceDetailBatchSerialByRemissionEntranceCode
        Dim quantity As Integer = 0
        Dim status As Integer = 2
        Dim res = (From redbs In _context.RemissionEntranceDetailBatchSerial
                   Join red In _context.RemissionEntranceDetail On redbs.RemissionEntranceDetailId Equals red.Id
                   Join re In _context.RemissionEntrance On red.RemissionEntranceId Equals re.Id
                   Where redbs.OutstandingQuantity > quantity AndAlso re.Status = status AndAlso re.Code.Equals(code) Select redbs).ToList()

        For Each item In res
            Dim remissionDetail = (From rd In _context.RemissionEntranceDetail.AsNoTracking() Where rd.Id = item.RemissionEntranceDetailId Select rd).FirstOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = remissionDetail.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            item.ProductId = product.Id
            item.QuantityDeliver = item.OutstandingQuantity
            If item.BatchSerialId IsNot Nothing Then
                item.CodeBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = item.BatchSerialId Select bs.BatchCode).FirstOrDefault()
            End If
        Next
        Return res
    End Function
End Class
