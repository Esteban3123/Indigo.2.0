'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez
' Created          : 02-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class TransferOrderDetailBatchSerialRepository
    Inherits GenericRepository(Of TransferOrderDetailBatchSerial)
    Implements ITransferOrderDetailBatchSerialRepository

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
    '''obtiene un detalle del detalle de orden de traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTransferOrderDetailBatchSerialById(id As Integer) As TransferOrderDetailBatchSerial Implements ITransferOrderDetailBatchSerialRepository.GetTransferOrderDetailBatchSerialById
        Return (From todb In _context.TransferOrderDetailBatchSerial Where todb.Id = id Select todb).FirstOrDefault()
    End Function

    ''' <summary>
    '''  lista los detalles del detalle de la orden de traslado por id de la orden de traslado
    ''' </summary>
    ''' <param name="transferOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTransferOrderDetailBatchSerialByTransferOrderId(transferOrderId As Integer, flagQuantiyZero As Boolean) As List(Of TransferOrderDetailBatchSerial) Implements ITransferOrderDetailBatchSerialRepository.ListTransferOrderDetailBatchSerialByTransferOrderId
        Dim status As Integer = 2
        Dim res = New List(Of TransferOrderDetailBatchSerial)
        If flagQuantiyZero Then
            Dim quantity As Integer = 0
            res = (From todb In _context.TransferOrderDetailBatchSerial
                       Join tod In _context.TransferOrderDetail On todb.TransferOrderDetailId Equals tod.Id
                       Join tro In _context.TransferOrder On tod.TransferOrderId Equals tro.Id
                       Where tro.Status = status And tro.Id = transferOrderId And todb.OutstandingQuantity > quantity Select todb).ToList()
        Else
            res = (From todb In _context.TransferOrderDetailBatchSerial
                       Join tod In _context.TransferOrderDetail On todb.TransferOrderDetailId Equals tod.Id
                       Join tro In _context.TransferOrder On tod.TransferOrderId Equals tro.Id
                       Where tro.Status = status And tro.Id = transferOrderId Select todb).ToList()
        End If
        
        For Each item In res
            Dim transferOrderDetail = (From tod In _context.TransferOrderDetail.AsNoTracking() Where tod.Id = item.TransferOrderDetailId Select tod).FirstOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = transferOrderDetail.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            item.ProductId = product.Id
            'item.QuantityDeliver = item.OutstandingQuantity
            Dim physicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking() Where pi.Id = item.PhysicalInventoryId Select pi).FirstOrDefault()
            If physicalInventory.BatchSerialId IsNot Nothing Then
                item.CodeBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = physicalInventory.BatchSerialId Select bs.BatchCode).FirstOrDefault()
            End If
        Next
        Return res
    End Function

End Class
