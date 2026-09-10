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
Imports System.Threading
Imports Domain.Billing.POCO
Imports Domain.Billing.POCO.E_RIPS

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
    ''' Genera el archivo FUR SERVICIOS de la Circular Externa 003 de 2026 de
    ''' ADRES. Devuelve un único <see cref="AdresClaimFile"/> con el JSON ya
    ''' serializado y un DataSet plano listo para exportarse a XLSX desde la UI.
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id del radicado de cuentas confirmado (0 si se envía solo lista de facturas).</param>
    ''' <param name="Session">Variable de sesión.</param>
    ''' <param name="InvoicesList">Facturas seleccionadas en el grid del radicado.</param>
    Function GenerateAdresFurServiciosPlane(IdRadicateInvoice As Integer,
                                            Session As SessionValues,
                                            Optional InvoicesList As List(Of RIPSBilling) = Nothing) As ActionMessageResult(Of AdresClaimFile)

    ''' <summary>
    ''' Genera el archivo FUR (Formulario Único de Reclamaciones) de la Circular Externa 003 de 2026 de ADRES. Devuelve un único
    ''' <see cref="AdresClaimFile"/> con el JSON ya serializado y un DataSet
    ''' plano listo para exportarse a XLSX desde la UI.
    ''' </summary>
    Function GenerateAdresFurPlane(IdRadicateInvoice As Integer,
                                    Session As SessionValues,
                                    Optional InvoicesList As List(Of RIPSBilling) = Nothing) As ActionMessageResult(Of AdresClaimFile)


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

    ''' <summary>
    ''' Valida y encola registros de servicio para reconstruir JSON RIPS.
    ''' </summary>
    Function RebuildFixedAmountRIPSToQueue(listDocumentNumber As List(Of String), audit As AuditMessage) As ActionResult

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

    ''' <summary>
    ''' Cargue pequeño de RIPS (≤ threshold). Procesa síncronamente y retorna resultado por item.
    ''' </summary>
    Function UploadRipsSmallAsync(items As List(Of RipsUploadRequest),
                                  audit As AuditMessage) As Task(Of ActionResult(Of RipsBulkResponse))

    ''' <summary>
    ''' Cargue masivo de RIPS. Usa Cosmos AllowBulkExecution. Aplica política de duplicados por _ts.
    ''' </summary>
    Function UploadRipsBulkAsync(batchId As String,
                                 items As List(Of RipsUploadRequest),
                                 audit As AuditMessage,
                                 Optional cancellationToken As CancellationToken = Nothing) As Task(Of ActionResult(Of RipsBulkResponse))

    ''' <summary>
    ''' Hidrata Cosmos por número de factura y carga InitialBalanceInvoiceDetail (snapshot 9 cols por ServiceType,
    ''' ADR-005 / D26). Idempotente: DELETE detail rows existentes antes de INSERT. Actualiza
    ''' InitialBalanceInvoice.CosmosId + ObligatedPartyDocument + Status=2 (Confirmado).
    ''' </summary>
    ''' <param name="invoiceNumbers">Lista de números de factura (saldos iniciales con CUV).</param>
    ''' <param name="audit">Audit user/company.</param>
    ''' <returns>Resumen por factura: OK / Skipped / Failed + error message.</returns>
    Function PopulateInitialBalanceDetail(invoiceNumbers As List(Of String), audit As AuditMessage) As Task(Of ActionResult(Of List(Of RipsUploadResult)))

    ''' <summary>
    ''' Pre-check de existencia de RIPS en CosmosDB antes de confirmar saldo inicial.
    ''' Recibe lista de numFactura y devuelve Existing/Missing. Usado por Portfolio Confirm + frontend
    ''' SaveAndConfirm para bloquear confirmación si faltan JSON RIPS para alguna factura con CUV.
    ''' </summary>
    Function CheckRipsExistAsync(invoiceNumbers As List(Of String), audit As AuditMessage) As Task(Of ActionResult(Of RipsCheckExistResponse))
End Interface
