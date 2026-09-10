'***********************************************************************
' Assembly         : DistributedServices.Glosas
' Author           : Daniel Eduardo Arévalo
' Created          : 27-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Text

<ServiceContract()> _
Public Interface IGlosasRIPSPlane

    ''' <summary>
    ''' Función que Genera los Archivos de RIPS
    ''' </summary>
    ''' <param name="IdRadicateInvoice">IdRadicateInvoice</param>
    ''' <param name="ConsecutiveRadicateInvoice"></param>
    ''' <param name="CodificationType"></param>
    ''' <param name="ServiceCode"></param>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GenerateRIPSPlane(IdRadicateInvoice As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Session As SessionValues, DetailPackage As Boolean, GenerateADPlane As Boolean, Optional InvoicesList As List(Of Integer) = Nothing) As List(Of ActionMessageResult(Of StringBuilder))

    ''' <summary>
    ''' Función para generar los Archivos de FURIPS
    ''' </summary>
    ''' <param name="IdRadicateInvoice">IdRadicateInvoice</param>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns>List(Of ActionMessageResult(Of StringBuilder))</returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GenerateFURIPSPlane(IdRadicateInvoice As Integer, Session As SessionValues, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As List(Of ActionMessageResult(Of StringBuilder))

    ''' <summary>
    ''' Función para generar los Archivos de FURIPS
    ''' </summary>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns>List(Of ActionMessageResult(Of StringBuilder))</returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GenerateFURTRANPlane(InvoicesList As List(Of RIPSBilling), Session As SessionValues) As List(Of ActionMessageResult(Of StringBuilder))

    ''' <summary>
    ''' Obtiene los registros para generar el archivo plano MegaRIPS
    ''' </summary>
    ''' <param name="radicateInvoiceId">Id del radicado de la factura</param>
    ''' <returns>Lista de registros</returns>
    <OperationContract>
    Function GetMegaRIPSByRadicateInvoiceId(ByVal radicateInvoiceId As Integer, Session As SessionValues, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As ActionMessageResult(Of String)

    ''' <summary>
    ''' Función que Genera los Archivos de Mega Plano
    ''' </summary>
    ''' <param name="IdRadicateInvoice">IdRadicateInvoice</param>
    ''' <param name="ConsecutiveRadicateInvoice"></param>
    ''' <param name="CodificationType"></param>
    ''' <param name="ServiceCode"></param>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GenerateRIPSMegaPlane(IdRadicateInvoice As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Session As SessionValues, Optional InvoicesList As List(Of Integer) = Nothing) As List(Of ActionMessageResult(Of StringBuilder))

End Interface
