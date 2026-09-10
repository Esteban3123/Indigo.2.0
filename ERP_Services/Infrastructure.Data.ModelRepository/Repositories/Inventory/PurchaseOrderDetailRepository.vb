'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PurchaseOrderDetailRepository
    Inherits GenericRepository(Of PurchaseOrderDetail)
    Implements IPurchaseOrderDetailRepository


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

    Public Function GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(supplierId As Integer, supplierDistributionLineId As Integer) As List(Of PurchaseOrderDetail) Implements IPurchaseOrderDetailRepository.GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId
        Dim status As Integer = 2
        Dim outstandingQuantity = 0
        Dim res = (From pod In _context.PurchaseOrderDetail
                Join po In _context.PurchaseOrder On pod.PurchaseOrderId Equals po.Id
                Where po.SupplierId = supplierId And po.SupplierDistributionLineId = supplierDistributionLineId And po.Status = status And pod.OutstandingQuantity > outstandingQuantity Select pod).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim purcharseOrder = (From po In _context.PurchaseOrder.AsNoTracking() Where po.Id = item.PurchaseOrderId Select po).FirstOrDefault()
                item.Code = purcharseOrder.Code
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
                item.CodeNameProduct = product.Code + " - " + product.Name
            Next
        End If
        Return res
    End Function

    

    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPurchaseOrderDetailById(id As Integer) As PurchaseOrderDetail Implements IPurchaseOrderDetailRepository.GetPurchaseOrderDetailById
        Return (From pod In _context.PurchaseOrderDetail Where pod.Id = id Select pod).FirstOrDefault()
    End Function
End Class
