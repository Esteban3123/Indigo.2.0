Imports System.Threading
Imports Domain.Base.Entities
Imports Domain.Billing.POCO.E_InvoiceXml
Imports Infrastructure.CrossCutting.Base

Public Interface IInvoiceXmlBulkAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Carga pequeña (≤ threshold) de XMLs de factura DIAN al blob storage.
    ''' Cada XML debe corresponder a una factura de saldo inicial previamente guardada (shadow Invoice).
    ''' </summary>
    Function UploadInvoiceXmlSmallAsync(items As List(Of InvoiceXmlUploadRequest),
                                         audit As AuditMessage) As Task(Of ActionResult(Of InvoiceXmlBulkResponse))

    ''' <summary>
    ''' Carga masiva (&gt; threshold) de XMLs de factura DIAN al blob storage.
    ''' </summary>
    Function UploadInvoiceXmlBulkAsync(batchId As String,
                                        items As List(Of InvoiceXmlUploadRequest),
                                        audit As AuditMessage,
                                        Optional cancellationToken As CancellationToken = Nothing) As Task(Of ActionResult(Of InvoiceXmlBulkResponse))

    ''' <summary>
    ''' Pre-check de existencia de XMLs en blob storage previo a confirmar saldo inicial.
    ''' Para cada numFactura del saldo inicial verifica si el XML ya fue subido al FilePath canónico
    ''' que el shadow ElectronicDocument tendrá tras el confirm (derivado de PortfolioInitialBalance.CreationDate).
    ''' </summary>
    Function CheckInvoiceXmlExistAsync(initialBalanceId As Integer,
                                        invoiceNumbers As List(Of String),
                                        audit As AuditMessage) As Task(Of ActionResult(Of InvoiceXmlCheckExistResponse))

End Interface
