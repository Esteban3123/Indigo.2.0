'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IInvoiceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una factura
    ''' </summary>
    Function SaveInvoice(ByVal invoice As Invoice, ByVal audit As AuditMessage) As ActionResult(Of Invoice)

    ''' <summary>
    ''' Anula una factura
    ''' </summary>
    Function AnularInvoice(ByVal invoice As Invoice, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    Function GetInvoiceById(ByVal Id As Integer) As Invoice

    ''' <summary>
    ''' Lista los ids de las facturas anuladas
    ''' </summary>
    ''' <returns></returns>
    Function ListAnnullateInvoiceIdByAdmission(admission As String) As List(Of Integer)

    ''' <summary>
    ''' Obtiene una factura por numero de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvoiceByInvoiceNumber(invoiceNumber As String, ByVal audit As AuditMessage) As Invoice

End Interface