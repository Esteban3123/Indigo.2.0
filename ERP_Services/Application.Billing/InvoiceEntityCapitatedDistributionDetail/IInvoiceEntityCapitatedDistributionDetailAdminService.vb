Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IInvoiceEntityCapitatedDistributionDetailAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene los Controles de una Distribución a partir de Factura Monto Fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedId"></param>
    ''' <param name="invoiceEntityCapitatedDistributionId"></param>
    ''' <returns></returns>
    Function GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId(ByVal invoiceEntityCapitatedId As Integer, ByVal invoiceEntityCapitatedDistributionId As Integer) As ActionResult(Of List(Of InvoiceEntityCapitatedDistributionDetail))

End Interface