'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : RafaelPatiño
' Created          : 11-04-2014
'
' Last Modified By : 
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'**********************************************************************
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class RadicateInvoiceCRepository
    Inherits GenericRepository(Of RadicateInvoiceC)
    Implements IRadicateInvoiceCRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la nueva instancia de <see cref="ObjectionsReceptionCRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRadicateInvoiceMassiveConfirm(listDocuments As List(Of String)) As List(Of RadicateInvoiceC) Implements IRadicateInvoiceCRepository.ListRadicateInvoiceMassiveConfirm
        Return (From cr In _context.RadicateInvoiceC Where listDocuments.Contains(cr.RadicatedConsecutive) Select cr).ToList()
    End Function

    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvocieRadicate(consecutive As String) As RadicateInvoiceC Implements IRadicateInvoiceCRepository.GetInvocieRadicate
        Dim tmpRadicateInvoiceC As IQueryable(Of RadicateInvoiceC) = From e In _context.RadicateInvoiceC.Include("Customer")
                                                                     Where e.RadicatedConsecutive = consecutive
                                                                     Select e
        If tmpRadicateInvoiceC.Count > 0 Then
            Dim ResponsibleData = tmpRadicateInvoiceC.SingleOrDefault
            ResponsibleData.OriginalValue = (From e In _context.RadicateInvoiceC.AsNoTracking Where e.RadicatedConsecutive = consecutive Select e).SingleOrDefault
            Return ResponsibleData
        End If
        Return New RadicateInvoiceC
    End Function
    
    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por medio del consecutivo
    ''' </summary>
    ''' <param name="id">id del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvocieRadicateByID(id As String) As RadicateInvoiceC Implements IRadicateInvoiceCRepository.GetInvocieRadicateByID
        Dim tmpRadicateInvoiceC = From e In _context.RadicateInvoiceC.Include("Customer").Include("RadicateInvoiceD")
                                  Where e.Id = id
                                  Select e
        If tmpRadicateInvoiceC.Count > 0 Then
            Dim ResponsibleData = tmpRadicateInvoiceC.SingleOrDefault
            ResponsibleData.OriginalValue = (From e In _context.RadicateInvoiceC.AsNoTracking
                                             Where e.Id = id
                                             Select e).SingleOrDefault
            Return ResponsibleData
        End If
        Return New RadicateInvoiceC
    End Function

    ''' <summary>
    ''' Listar Oficio de Factura Radicadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvocieRadicate() As List(Of RadicateInvoiceC) Implements IRadicateInvoiceCRepository.ListInvocieRadicate
        Dim Busqueda = From e In _context.RadicateInvoiceC
                       Select e
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Obtener una factura por numero de radicacion
    ''' </summary>
    ''' <param name="radicatenumber">numero de radicado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvocieDRadicate(radicatenumber As String) As RadicateInvoiceD Implements IRadicateInvoiceCRepository.GetInvocieDRadicate
        Dim tmpRadicateInvoiceC = From e In _context.RadicateInvoiceD.Include("RadicateInvoiceC")
                                  Where e.RadicatedNumber = radicatenumber
                                  Select e
        If tmpRadicateInvoiceC.Count > 0 Then
            Dim ResponsibleData = tmpRadicateInvoiceC.ToList().First
            Return ResponsibleData
        End If
        Return New RadicateInvoiceD
    End Function
    
    Public Function SP_InvoiceTraceability(container As String, SecurityContainer As String, invoice As String) As SP_InvoiceTraceability_Result Implements IRadicateInvoiceCRepository.SP_InvoiceTraceability
        Dim traceability = (From e In _context.SP_InvoiceTraceability(container, SecurityContainer, invoice)
                            Select e).ToList()
        If traceability.Count > 0 Then
            Return traceability.ToList.Item(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function SP_InvoiceTraceabilityConciliation(invoiceNumber As String) As List(Of SP_InvoiceTraceabilityConciliation_Result) Implements IRadicateInvoiceCRepository.SP_InvoiceTraceabilityConciliation
        Dim traceability = (From e In _context.SP_InvoiceTraceabilityConciliation(invoiceNumber)
                            Select e).ToList
        Return traceability
    End Function

    Public Function SP_InvoiceTraceabilityDevolution(invoiceNumber As String) As List(Of SP_InvoiceTraceabilityDevolution_Result) Implements IRadicateInvoiceCRepository.SP_InvoiceTraceabilityDevolution
        Dim traceability = (From e In _context.SP_InvoiceTraceabilityDevolution(invoiceNumber)
                            Select e).ToList
        Return traceability
    End Function

    Public Function SP_InvoiceTraceabilityRadication(invoice As String) As List(Of SP_InvoiceTraceabilityRadication_Result) Implements IRadicateInvoiceCRepository.SP_InvoiceTraceabilityRadication
        Dim traceability = (From e In _context.SP_InvoiceTraceabilityRadication(invoice)
                            Select e).ToList
        Return traceability
    End Function

    Public Function SP_InvoiceTraceabilityResponsibles(invoice As String) As List(Of SP_InvoiceTraceabilityResponsibles_Result) Implements IRadicateInvoiceCRepository.SP_InvoiceTraceabilityResponsibles
        Dim traceability = (From e In _context.SP_InvoiceTraceabilityResponsibles(invoice)
                            Select e).ToList
        Return traceability
    End Function

    Public Function GetInvocieRadicateByIdSimple(Id As Integer, Optional tracking As Boolean = True) As RadicateInvoiceC Implements IRadicateInvoiceCRepository.GetInvocieRadicateByIdSimple
        If tracking Then
            Return (From ir In _context.RadicateInvoiceC Where ir.Id = Id Select ir).FirstOrDefault()
        Else
            Return (From ir In _context.RadicateInvoiceC.AsNoTracking Where ir.Id = Id Select ir).FirstOrDefault()
        End If
    End Function

#Region "RIPS"

    Public Function SP_GenerateAFFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateAFFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateAFFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateAFFileData(RadicateInvoiceId, XmlInvoices).ToList
    End Function

    Function SP_GenerateUSFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateUSFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateUSFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateUSFileData(RadicateInvoiceId, XmlInvoices).ToList
    End Function

    Function SP_GenerateACFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateACFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateACFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateACFileData(RadicateInvoiceId, XmlInvoices, PackageDetail).ToList
    End Function

    Function SP_GenerateADFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateADFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateADFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateADFileData(RadicateInvoiceId, XmlInvoices, PackageDetail).ToList
    End Function

    Function SP_GenerateAPFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateAPFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateAPFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateAPFileData(RadicateInvoiceId, XmlInvoices, PackageDetail).ToList
    End Function

    Function SP_GenerateATFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateATFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateATFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateATFileData(RadicateInvoiceId, XmlInvoices, PackageDetail).ToList
    End Function

    Function SP_GenerateANFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateANFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateANFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateANFileData(RadicateInvoiceId, XmlInvoices).ToList
    End Function

    Function SP_GenerateAUFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateAUFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateAUFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateAUFileData(RadicateInvoiceId, XmlInvoices).ToList
    End Function

    Function SP_GenerateAHFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateAHFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateAHFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateAHFileData(RadicateInvoiceId, XmlInvoices).ToList
    End Function

    Function SP_GenerateAMFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateAMFileData_Result) Implements IRadicateInvoiceCRepository.SP_GenerateAMFileData
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateAMFileData(RadicateInvoiceId, XmlInvoices, PackageDetail).ToList
    End Function

#End Region

End Class
