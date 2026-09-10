'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class DocumentInvoiceProductSalesDevolutionRepository
    Inherits GenericRepository(Of DocumentInvoiceProductSalesDevolution)
    Implements IDocumentInvoiceProductSalesDevolutionRepository

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

    Public Function GetDocumentInvoiceProductSalesDevolutionByCode(code As String) As DocumentInvoiceProductSalesDevolution Implements IDocumentInvoiceProductSalesDevolutionRepository.GetDocumentInvoiceProductSalesDevolutionByCode
        Dim res = (From bg In _context.DocumentInvoiceProductSalesDevolution Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.WarehouseDescription = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.WarehouseId Select String.Concat(w.Code + " - ", w.Name)).FirstOrDefault()

            res.OriginalValue = (From bg In _context.DocumentInvoiceProductSalesDevolution.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New DocumentInvoiceProductSalesDevolution
        End If
    End Function

    Public Function GetDocumentInvoiceProductSalesDevolutionById(id As Integer) As DocumentInvoiceProductSalesDevolution Implements IDocumentInvoiceProductSalesDevolutionRepository.GetDocumentInvoiceProductSalesDevolutionById
        Dim res = (From bg In _context.DocumentInvoiceProductSalesDevolution Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New DocumentInvoiceProductSalesDevolution
        End If
    End Function

    ''' <summary>
    ''' Proceso de devolución parcial de ventas
    ''' </summary>
    ''' <param name="xmlData"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveDocumentInvoiceProductSalesDevolution(xmlData As String, codeUser As String) As SP_SaveDocumentInvoiceProductSalesDevolution_Result Implements IDocumentInvoiceProductSalesDevolutionRepository.SP_SaveDocumentInvoiceProductSalesDevolution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveDocumentInvoiceProductSalesDevolution(xmlData, codeUser).SingleOrDefault
    End Function

End Class
