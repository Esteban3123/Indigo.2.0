'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Daniel Eduardo Arévalo
' Created          : 27-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Domain.Billing.POCO

Public Interface IRIPSPlaneAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Generación de Plano de RIPS
    ''' </summary>
    ''' <param name="IdRadicateInvoice"></param>
    ''' <param name="ConsecutiveRadicateInvoice"></param>
    ''' <param name="CodificationType"></param>
    ''' <param name="ServiceCode"></param>
    ''' <param name="InvoicesList"></param>
    ''' <returns></returns>
    Function GenerateRIPSPlane(IdRadicateInvoice As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Session As SessionValues, DetailPackage As Boolean, GenerateADPlane As Boolean, Optional InvoicesList As List(Of Integer) = Nothing) As List(Of ActionMessageResult(Of StringBuilder))

    ''' <summary>
    ''' Generación de Plano de FURIPS
    ''' </summary>
    ''' <param name="IdRadicateInvoice">IdRadicateInvoice</param>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns>List(Of ActionMessageResult(Of StringBuilder))</returns>
    ''' <remarks></remarks>
    Function GenerateFURIPSPlane(IdRadicateInvoice As Integer, Session As SessionValues, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As List(Of ActionMessageResult(Of StringBuilder))

    ''' <summary>
    ''' Generación de Plano de FURTRAN
    ''' </summary>
    ''' <param name="Session">Variable de Sesión</param>
    Function GenerateFURTRANPlane(InvoicesList As List(Of RIPSBilling), Session As SessionValues) As List(Of ActionMessageResult(Of StringBuilder))


    ''' <summary>
    ''' Obtiene los registros para generar el archivo plano MegaRIPS
    ''' </summary>
    ''' <param name="radicateInvoiceId">Id del radicado de la factura</param>
    ''' <returns>Lista de registros</returns>
    Function GetMegaRIPSByRadicateInvoiceId(ByVal radicateInvoiceId As Integer, companyCode As String, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As ActionMessageResult(Of String)

    ''' <summary>
    ''' Función que Genera los Archivos de Mega Plano
    ''' </summary>
    ''' <param name="IdRadicateInvoice"></param>
    ''' <param name="ConsecutiveRadicateInvoice"></param>
    ''' <param name="CodificationType"></param>
    ''' <param name="ServiceCode"></param>
    ''' <param name="InvoicesList"></param>
    ''' <returns></returns>
    Function GenerateRIPSMegaPlane(IdRadicateInvoice As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Session As SessionValues, Optional InvoicesList As List(Of Integer) = Nothing) As List(Of ActionMessageResult(Of StringBuilder))

    ''' <summary>
    ''' metodo que se encarga de validar y encolar los eRIPS a generar
    ''' </summary>
    ''' <param name="entityName"></param>
    ''' <param name="listDocumentNumber"></param>
    ''' <param name="audit"></param>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Function GenerateElectronicRIPSToQueue(entityName As String, listDocumentNumber As List(Of String), audit As AuditMessage, Optional entityId As Integer? = Nothing) As ActionResult

    Function ReSendElectronicRIPSToQueue(entityName As String, listDocumentNumber As List(Of String), audit As AuditMessage, Optional entityId As Integer? = Nothing) As ActionResult
    ''' <summary>
    ''' metodo encargado de consultar y retornar el Json por Id en la cosmos en un arreglo de bytes
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetJsonRIPSById(id As String) As Task(Of ActionResult(Of String))

    ''' <summary>
    ''' consultar y retornar JSON RIPS por el numero de la factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetJsonRIPSByDocNumber(docNumber As String) As Task(Of ActionResult(Of String))

    ''' <summary>
    ''' metodo encargado de consultar y retornar el objeto Json por Id en la cosmos en un arreglo de bytes
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetObjectJsonRIPSbyId(id As String) As Task(Of ActionResult(Of ElectronicRIPSModel))

    ''' <summary>
    '''  Servicio de Re envio masivo RIPS difernetes a validados
    ''' </summary>
    ''' <param name="take"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function MassiveResendWithPolicies(take As Integer, audit As AuditMessage) As ActionResult
End Interface
