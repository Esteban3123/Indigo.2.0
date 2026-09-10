'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 07-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class LoanMerchandiseDevolutionDetailRepository
    Inherits GenericRepository(Of LoanMerchandiseDevolutionDetail)
    Implements ILoanMerchandiseDevolutionDetailRepository


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
    ''' obtiene un item de detalle de devolucion prestamo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoanMerchandiseDevolutionDetailById(Id As Integer) As LoanMerchandiseDevolutionDetail Implements ILoanMerchandiseDevolutionDetailRepository.GetLoanMerchandiseDevolutionDetailById
        Return (From red In _context.LoanMerchandiseDevolutionDetail Where red.Id = Id Select red).FirstOrDefault()
    End Function
    ''' <summary>
    ''' Obtiene una lista de detalle de devolucion de prestamo
    ''' </summary>
    ''' <param name="IdLoanMerchandiseDevolution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLoanMerchandiseDevolutionDetailByIdLoanMerchandise(IdLoanMerchandiseDevolution As Integer) As List(Of LoanMerchandiseDevolutionDetail) Implements ILoanMerchandiseDevolutionDetailRepository.ListLoanMerchandiseDevolutionDetailByIdLoanMerchandise
        Dim res = (From red In _context.LoanMerchandiseDevolutionDetail.Include("LoanMerchandiseDevolutionDetailBatchSerial") Where red.LoanMerchandiseDevolutionId = IdLoanMerchandiseDevolution Select red).ToList()
        For Each item In res
            Dim LoanMerchandiseDetail = (From p In _context.LoanMerchandiseDetail.AsNoTracking() Where p.Id = item.LoanMerchandiseDetaillId Select p).SingleOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = LoanMerchandiseDetail.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            item.PresentationProduct = product.Presentation
            item.UnitValue = LoanMerchandiseDetail.UnitValue
            item.BalanceQuantity = LoanMerchandiseDetail.Quantity
            item.ProductId = product.Id
            item.OutstandingQuantity = LoanMerchandiseDetail.OutstandingQuantity - item.Quantity
        Next
        Return res
    End Function
End Class
