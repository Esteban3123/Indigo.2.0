'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez
' Created          : 25-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class TransferOrderDetailRepository
    Inherits GenericRepository(Of TransferOrderDetail)
    Implements ITransferOrderDetailRepository

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
    ''' lista los detalles de la orden de traslado por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTransferOrderDetailById(id As Integer) As TransferOrderDetail Implements ITransferOrderDetailRepository.GetTransferOrderDetailById
        Return (From tod In _context.TransferOrderDetail.AsNoTracking Where tod.Id = id Select tod).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un listado de detalles de orden de traslado por almacen o unidad funcional de destino
    ''' </summary>
    ''' <param name="idFilter"></param>
    ''' <param name="orderType"></param>
    ''' <param name="dispatchTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTransferOrderDetailByTarget(idFilter As Integer, orderType As Integer, dispatchTo As Integer) As List(Of TransferOrderDetail) Implements ITransferOrderDetailRepository.ListTransferOrderDetailByTarget
        Dim status As Integer = 2
        Dim res As List(Of Domain.Entities.TransferOrderDetail) = New List(Of Domain.Entities.TransferOrderDetail)
        If orderType = 1 Or orderType = 2 AndAlso dispatchTo = 1 Then
            res = (From tod In _context.TransferOrderDetail
                Join tor In _context.TransferOrder On tod.TransferOrderId Equals tor.Id
                Where tor.TargetWarehouseId = idFilter And tor.Status = status Select tod).ToList()
        ElseIf orderType = 2 AndAlso dispatchTo = 2 Then
            res = (From tod In _context.TransferOrderDetail
                Join tor In _context.TransferOrder On tod.TransferOrderId Equals tor.Id
                Where tor.TargetFunctionalUnitId = idFilter And tor.Status = status Select tod).ToList()
        End If
        
        If res.Count > 0 Then
            For Each item In res
                Dim transferOrder = (From re In _context.TransferOrder.AsNoTracking() Where re.Id = item.TransferOrderId Select re).FirstOrDefault()
                item.Code = transferOrder.Code
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
                item.DescriptionProduct = product.Code + " - " + product.Name
            Next
        End If
        Return res
    End Function

End Class
