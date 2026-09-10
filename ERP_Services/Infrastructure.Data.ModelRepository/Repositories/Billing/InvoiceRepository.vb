'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class InvoiceRepository
    Inherits GenericRepository(Of Invoice)
    Implements IInvoiceRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    Public Function GetInvoiceById(Id As Integer, Optional tracking As Boolean = True) As Invoice Implements IInvoiceRepository.GetInvoiceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query As Invoice
        If tracking Then
            query = (From i In _context.Invoice Where i.Id = Id Select i).FirstOrDefault()
        Else
            query = (From i In _context.Invoice.AsNoTracking() Where i.Id = Id Select i).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New Invoice()
        End If
    End Function

    Public Function GetInvoiceDetailByServiceOrderDetailId(sodId As Integer) As InvoiceDetail Implements IInvoiceRepository.GetInvoiceDetailByServiceOrderDetailId
        Return _context.InvoiceDetail.AsNoTracking().Where(Function(o) o.ServiceOrderDetailId = sodId).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene una factura por numero de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceByInvoiceNumber(invoiceNumber As String, Optional tracking As Boolean = True) As Invoice Implements IInvoiceRepository.GetInvoiceByInvoiceNumber
        Dim res As Invoice = Nothing
        If tracking Then
            res = (From so In _context.Invoice Where so.InvoiceNumber = invoiceNumber Select so).FirstOrDefault()
        Else
            res = (From so In _context.Invoice.AsNoTracking() Where so.InvoiceNumber = invoiceNumber Select so).FirstOrDefault()
        End If
        If res IsNot Nothing Then
            Return res
        End If
        Return New Invoice
    End Function

    ''' <summary>
    ''' Obtiene una factura por el id del folio
    ''' </summary>
    Public Function GetInvoiceByRevenueControlDetailId(RevenueControlDetailId As Integer) As Invoice Implements IInvoiceRepository.GetInvoiceByRevenueControlDetailId
        If RevenueControlDetailId = 0 Then
            Throw New ArgumentNullException("RevenueControlDetailId")
        End If
        Dim query = (From i In _context.Invoice.Include("InvoiceDetail").Include("InvoicePortfolioAdvance") Where i.RevenueControlDetailId = RevenueControlDetailId Select i).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New Invoice()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una factura por el id del folio
    ''' </summary>
    Public Function GetInvoiceAndRevenueControlByRevenueControlDetailIdNotTracking(RevenueControlDetailId As Integer) As Invoice Implements IInvoiceRepository.GetInvoiceAndRevenueControlByRevenueControlDetailIdNotTracking
        If RevenueControlDetailId = 0 Then
            Throw New ArgumentNullException("RevenueControlDetailId")
        End If
        Dim query = (From i In _context.Invoice.AsNoTracking().Include("RevenueControlDetail").AsNoTracking() Where i.RevenueControlDetailId = RevenueControlDetailId Select i).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New Invoice()
        End If
    End Function

    ''' <summary>
    ''' Lists the annullate invoice identifier.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAnnullateInvoiceIdByAdmission(admission As String) As List(Of Integer) Implements IInvoiceRepository.ListAnnullateInvoiceIdByAdmission
        Dim DOS As Integer = 2
        Return (From i In _context.Invoice Where i.AdmissionNumber.Trim() = admission.Trim() AndAlso i.Status = DOS Select i).Select(Function(o) o.Id).Distinct().ToList()
    End Function

    ''' <summary>
    ''' Obtiene información adicional de una factura
    ''' </summary>
    Public Function GetInvoiceMoreInformationByInvoiceId(invoiceId As Integer) As SP_GetInvoiceMoreInformationByInvoiceId_Result Implements IInvoiceRepository.GetInvoiceMoreInformationByInvoiceId
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetInvoiceMoreInformationByInvoiceId(invoiceId).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los pagos asociados a una factura
    ''' </summary>
    Public Function GetInvoiceDetailsByInvoiceId(invoiceId As Integer) As List(Of SP_GetInvoiceDetailsByInvoiceId_Result) Implements IInvoiceRepository.GetInvoiceDetailsByInvoiceId
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetInvoiceDetailsByInvoiceId(invoiceId).ToList()
    End Function

    ''' <summary>
    ''' Obtiene los pagos asociados a una factura
    ''' </summary>
    Public Function GetPaymentMethodsByInvoiceId(invoiceId As Integer) As List(Of SP_GetPaymentMethodsByInvoiceId_Result) Implements IInvoiceRepository.GetPaymentMethodsByInvoiceId
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetPaymentMethodsByInvoiceId(invoiceId).ToList()
    End Function

    ''' <summary>
    ''' Obtiene los pagos realizados a facturas de sector salud de tipo EAPB con/sin Contrato
    ''' </summary>
    ''' <param name="invoiceId"></param>
    ''' <returns></returns>
    Public Function GetPrepaidPaymentHealth(invoiceId As Integer) As List(Of SP_GetPrepaidPaymentHealth) Implements IInvoiceRepository.GetPrepaidPaymentHealth
        Return Me.ExecuteStoredProcedure(Of SP_GetPrepaidPaymentHealth)("[Billing].[SP_GetPrepaidPaymentHealth]", {("@InvoiceId", invoiceId)})?.ToList()
    End Function

End Class