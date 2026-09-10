'************************************************************
' Assembly         : Domain.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-02-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Billing.POCO
Imports System.Threading.Tasks


#End Region

Public Interface IInvoiceRepository
    Inherits IRepository(Of Invoice)

    Function GetInvoiceDetailByServiceOrderDetailId(sodId As Integer) As InvoiceDetail

    ''' <summary>
    ''' Lista los ids de las facturas anuladas
    ''' </summary>
    ''' <returns></returns>
    Function ListAnnullateInvoiceIdByAdmission(admission As String) As List(Of Integer)

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    Function GetInvoiceById(ByVal Id As Integer, Optional tracking As Boolean = True) As Invoice

    ''' <summary>
    ''' Obtiene una factura por numero de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvoiceByInvoiceNumber(invoiceNumber As String, Optional tracking As Boolean = True) As Invoice

    ''' <summary>
    ''' Obtiene una factura y el folio por el id del folio
    ''' </summary>
    ''' <param name="RevenueControlDetailId">id del folio</param>
    ''' <returns>Factura</returns>
    Function GetInvoiceAndRevenueControlByRevenueControlDetailIdNotTracking(ByVal RevenueControlDetailId As Integer) As Invoice

    ''' <summary>
    ''' Obtiene una factura por el id del folio
    ''' </summary>
    ''' <param name="RevenueControlDetailId">id del folio</param>
    ''' <returns>Factura</returns>
    Function GetInvoiceByRevenueControlDetailId(ByVal RevenueControlDetailId As Integer) As Invoice

    ''' <summary>
    ''' Obtiene información adicional de una factura
    ''' </summary>
    ''' <param name="invoiceId">id de la factura</param>
    ''' <returns>Factura</returns>
    Function GetInvoiceMoreInformationByInvoiceId(ByVal invoiceId As Integer) As SP_GetInvoiceMoreInformationByInvoiceId_Result

    ''' <summary>
    ''' Obtiene la fecha final del período de la factura de capitación anterior.
    ''' Solo aplica para facturas de período de capitación (InvoicePeriod = 2).
    ''' </summary>
    Function GetPreviousCapitationPeriodEndDateByInvoiceId(ByVal invoiceId As Integer) As Nullable(Of Date)

    ''' <summary>
    ''' Obtiene los detalles de una factura
    ''' </summary>
    ''' <param name="invoiceId">id de la factura</param>
    ''' <returns>Factura</returns>
    Function GetInvoiceDetailsByInvoiceId(ByVal invoiceId As Integer) As List(Of SP_GetInvoiceDetailsByInvoiceId_Result)

    ''' <summary>
    ''' Obtiene los pagos asociados a una factura
    ''' </summary>
    ''' <param name="invoiceId">id de la factura</param>
    ''' <returns>Factura</returns>
    Function GetPaymentMethodsByInvoiceId(ByVal invoiceId As Integer) As List(Of SP_GetPaymentMethodsByInvoiceId_Result)

    ''' <summary>
    ''' Obtiene los pagos realizados a facturas de sector salud de tipo EAPB con/sin Contrato
    ''' </summary>
    ''' <param name="invoiceId"></param>
    ''' <returns></returns>
    Function GetPrepaidPaymentHealth(invoiceId As Integer) As List(Of SP_GetPrepaidPaymentHealth)


    ''' <summary>
    ''' Lista registros de servicio paginados para facturas monto fijo.
    ''' </summary>
    Function ListFixedAmountServiceRecords(query As FixedAmountServiceRecordQuery) As Task(Of PagedResult(Of FixedAmountServiceRecordDto))

    ''' <summary>
    ''' Lista registros de servicio aptos para reconstruccion de JSON RIPS.
    ''' </summary>
    Function ListFixedAmountRebuildCandidates(query As FixedAmountServiceRecordQuery) As Task(Of List(Of FixedAmountRebuildCandidateDto))

    ''' <summary>
    ''' Consulta el avance de reconstruccion usando sendDate como frontera.
    ''' </summary>
    Function GetFixedAmountRebuildStatus(query As FixedAmountServiceRecordQuery) As Task(Of FixedAmountRebuildStatusDto)

    ''' <summary>
    ''' Obtiene la fecha actual del motor de base de datos.
    ''' </summary>
    Function GetDatabaseDate() As DateTime

End Interface
