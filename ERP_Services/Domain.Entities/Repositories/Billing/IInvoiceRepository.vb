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

End Interface