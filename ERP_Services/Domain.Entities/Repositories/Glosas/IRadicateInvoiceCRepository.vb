'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : RafaelPatiño
' Created          : 14-06-2013
'
' Last Modified By : 
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 



Imports Domain.Entities
Imports Domain.Base
Imports System.Data


Public Interface IRadicateInvoiceCRepository
    Inherits IRepository(Of RadicateInvoiceC)

#Region "Methods"

    Function ListRadicateInvoiceMassiveConfirm(listDocuments As List(Of String)) As List(Of RadicateInvoiceC)

    ''' <summary>
    ''' Listar Oficio de Factura Radicadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListInvocieRadicate() As List(Of RadicateInvoiceC)

    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvocieRadicate(ByVal consecutive As String) As RadicateInvoiceC

    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por el id sin agregados
    ''' </summary>
    Function GetInvocieRadicateByIdSimple(ByVal Id As Integer, Optional tracking As Boolean = True) As RadicateInvoiceC
    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por medio del consecutivo
    ''' </summary>
    ''' <param name="id">id del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvocieRadicateByID(id As String) As RadicateInvoiceC

    ''' <summary>
    ''' Obtener una factura por numero de radicacion
    ''' </summary>
    ''' <param name="radicatenumber">numero de radicado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvocieDRadicate(radicatenumber As String) As RadicateInvoiceD

    ''' <summary>
    ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceability(container As String, SecurityContainer As String, invoice As String) As SP_InvoiceTraceability_Result

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceabilityConciliation(invoiceNumber As String) As List(Of SP_InvoiceTraceabilityConciliation_Result)

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceabilityDevolution(invoiceNumber As String) As List(Of SP_InvoiceTraceabilityDevolution_Result)

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceabilityRadication(invoice As String) As List(Of SP_InvoiceTraceabilityRadication_Result)

    ''' <summary>
    ''' ''' Obtener los reponsables de tramite de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceabilityResponsibles(invoice As String) As List(Of SP_InvoiceTraceabilityResponsibles_Result)

    #Region "RIPS"

    Function SP_GenerateAFFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateAFFileData_Result)

    Function SP_GenerateUSFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateUSFileData_Result)

    Function SP_GenerateACFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateACFileData_Result)

    Function SP_GenerateADFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateADFileData_Result)

    Function SP_GenerateAPFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateAPFileData_Result)

    Function SP_GenerateATFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateATFileData_Result)

    Function SP_GenerateANFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateANFileData_Result)

    Function SP_GenerateAUFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateAUFileData_Result)

    Function SP_GenerateAHFileData(RadicateInvoiceId As Integer, XmlInvoices As String) As List(Of SP_GenerateAHFileData_Result)

    Function SP_GenerateAMFileData(RadicateInvoiceId As Integer, XmlInvoices As String, PackageDetail As Boolean) As List(Of SP_GenerateAMFileData_Result)

#End Region

#End Region

End Interface
