'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class BillingInvoiceCategories
    Inherits GenericRepository(Of InvoiceCategories)
    Implements IBillingInvoiceCategories

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Gets the invoice category.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetInvoiceCategory(code As String) As InvoiceCategories Implements IBillingInvoiceCategories.GetInvoiceCategory
        Dim res As InvoiceCategories = (From bc In _context.InvoiceCategories.Include("InvoiceCategoriesUser") Where bc.Code.Equals(code) Select bc).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bc In _context.InvoiceCategories.AsNoTracking() Where bc.Code.Equals(code) Select bc).FirstOrDefault()
            Return res
        End If
        Return New InvoiceCategories
    End Function

    Public Function GetInvoiceCategoryPOCO(code As String) As InvoiceCategories Implements IBillingInvoiceCategories.GetInvoiceCategoryPOCO
        Return (From bc In _context.InvoiceCategories.AsNoTracking() Where bc.Code.Equals(code) Select bc).FirstOrDefault()
    End Function

    Public Function GetListInvoiceCategoryPOCO(listCode As List(Of String)) As List(Of InvoiceCategories) Implements IBillingInvoiceCategories.GetListInvoiceCategoryPOCO
        If listCode Is Nothing OrElse listCode.Count = 0 Then
            Return New List(Of InvoiceCategories)
        End If
        Return (From bc In _context.InvoiceCategories.AsNoTracking() Where listCode.Contains(bc.Code) Select bc).ToList()
    End Function

    ''' <summary>
    ''' Gets the invoice category by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInvoiceCategoryById(id As Integer) As InvoiceCategories Implements IBillingInvoiceCategories.GetInvoiceCategoryById
        Dim res As InvoiceCategories = (From bc In _context.InvoiceCategories Where bc.Id = id Select bc).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bc In _context.InvoiceCategories.AsNoTracking() Where bc.Id = id Select bc).FirstOrDefault()
            Return res
        End If
        Return New InvoiceCategories
    End Function

    ''' <summary>
    ''' Valida el copyPaste del formulario de categorias
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_CopyAndPasteCategories(xmlObject As String) As List(Of SP_CopyAndPasteCategories_Result) Implements IBillingInvoiceCategories.SP_CopyAndPasteCategories
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteCategories(xmlObject).ToList
    End Function

End Class
