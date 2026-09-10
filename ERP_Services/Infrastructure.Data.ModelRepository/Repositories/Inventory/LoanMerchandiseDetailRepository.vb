'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 24-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class LoanMerchandiseDetailRepository
    Inherits GenericRepository(Of LoanMerchandiseDetail)
    Implements ILoanMerchandiseDetailRepository


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
    ''' obtiene un item de detalle de una solicitud de prestamo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoanMerchandiseDetailById(Id As Integer) As LoanMerchandiseDetail Implements ILoanMerchandiseDetailRepository.GetLoanMerchandiseDetailById
        Return (From red In _context.LoanMerchandiseDetail Where red.Id = Id Select red).SingleOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene una lista de detalle de solitud de prestamo
    ''' </summary>
    ''' <param name="IdLoanMerchandise"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLoanMerchandiseDetailByIdLoanMerchandise(IdLoanMerchandise As Integer, ByVal isDevolution As Boolean) As List(Of LoanMerchandiseDetail) Implements ILoanMerchandiseDetailRepository.ListLoanMerchandiseDetailByIdLoanMerchandise
        Dim res As List(Of LoanMerchandiseDetail)
        If isDevolution = True Then
            res = (From red In _context.LoanMerchandiseDetail.Include("LoanMerchandiseDetailBatchSerial") Where red.LoanMerchandiseId = IdLoanMerchandise And red.OutstandingQuantity > 0 Select red).ToList()
        Else
            res = (From red In _context.LoanMerchandiseDetail.Include("LoanMerchandiseDetailBatchSerial") Where red.LoanMerchandiseId = IdLoanMerchandise Select red).ToList()
        End If
        For Each item In res
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            item.PresentationProduct = product.Presentation
            For Each itemBatch In item.LoanMerchandiseDetailBatchSerial
                If itemBatch.BatchSerialId IsNot Nothing Then
                    Dim batch = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = itemBatch.BatchSerialId Select bs).FirstOrDefault()
                    itemBatch.CodeBatchSerial = batch.BatchCode
                ElseIf itemBatch.PhysicalInventoryId IsNot Nothing Then
                    Dim PhysicalInventory = (From bs In _context.PhysicalInventory.AsNoTracking() Where bs.Id = itemBatch.PhysicalInventoryId Select bs).FirstOrDefault()
                    If PhysicalInventory.BatchSerialId IsNot Nothing Then
                        Dim batchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = PhysicalInventory.BatchSerialId Select bs).FirstOrDefault()
                        itemBatch.CodeBatchSerial = batchSerial.BatchCode
                    End If
                End If
            Next
        Next
        Return res
    End Function


    ''' <summary>
    ''' obtiene un item de detalle de una solicitud de prestamo con include de la cabecera
    ''' </summary>
    ''' <param name="Id">Id del item detalle de prestamo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoanMerchandiseDetailByIdWithIncludeLoan(Id As Integer) As LoanMerchandiseDetail Implements ILoanMerchandiseDetailRepository.GetLoanMerchandiseDetailByIdWithIncludeLoan
        Return (From red In _context.LoanMerchandiseDetail.Include("LoanMerchandise") Where red.Id = Id Select red).FirstOrDefault()
    End Function

End Class
