'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 13-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Class PurchaseOrderDevolutionRepository
    Inherits GenericRepository(Of PurchaseOrderDevolution)
    Implements IPurchaseOrderDevolutionRepository


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

    
    Public Function GetPurchaseOrderDevolutionByCode(Code As String) As PurchaseOrderDevolution Implements IPurchaseOrderDevolutionRepository.GetPurchaseOrderDevolutionByCode
        Dim res = (From pod In _context.PurchaseOrderDevolution Where pod.Code = Code Select pod).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From pod In _context.PurchaseOrderDevolution.AsNoTracking() Where pod.Code = Code Select pod).FirstOrDefault()
            Return res
        End If
        Return New PurchaseOrderDevolution
    End Function

    Public Function GetPurchaseOrderDevolutionById(Id As Integer) As PurchaseOrderDevolution Implements IPurchaseOrderDevolutionRepository.GetPurchaseOrderDevolutionById
        Dim res = (From pod In _context.PurchaseOrderDevolution Where pod.Id = Id Select pod).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From pod In _context.PurchaseOrderDevolution.AsNoTracking() Where pod.Id = Id Select pod).FirstOrDefault()
            Return res
        End If
        Return New PurchaseOrderDevolution
    End Function

    Public Function GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(PurchaseOrderDevolutionId As Integer) As List(Of PurchaseOrderDevolutionDetail) Implements IPurchaseOrderDevolutionRepository.GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId
        Dim res = (From podd In _context.PurchaseOrderDevolutionDetail Where podd.PurchaseOrderDevolutionId = PurchaseOrderDevolutionId Select podd).ToList()
        For Each item In res
            Dim detailTmp = (From d In _context.PurchaseOrderDetail.AsNoTracking() Where d.Id = item.PurchaseOrderDetailId Select d).FirstOrDefault()
            item.PurchaseCode = (From p In _context.PurchaseOrder.AsNoTracking Where p.Id = detailTmp.PurchaseOrderId Select p.Code).FirstOrDefault()
            item.ProductCode = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = detailTmp.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
            item.OutstandingQuantity = detailTmp.OutstandingQuantity
        Next
        Return res
    End Function
End Class
