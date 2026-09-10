#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IInvoiceEntityCapitatedDistributionDetailRepository
    Inherits IRepository(Of InvoiceEntityCapitatedDistributionDetail)

    ''' <summary>
    ''' Obtiene los Controles de una Distribución a partir de Factura Monto Fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedId"></param>
    ''' <param name="invoiceEntityCapitatedDistributionId"></param>
    Function SP_GetInvoiceEntityCapitatedDistributionDetails(invoiceEntityCapitatedId As Integer, invoiceEntityCapitatedDistributionId As Integer) As List(Of SP_GetInvoiceEntityCapitatedDistributionDetails_Result)

End Interface