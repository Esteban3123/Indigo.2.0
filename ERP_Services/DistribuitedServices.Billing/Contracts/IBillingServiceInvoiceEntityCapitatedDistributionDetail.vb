#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IBillingServiceInvoiceEntityCapitatedDistributionDetail

    ''' <summary>
    ''' Obtiene los Controles de una Distribución a partir de Factura Monto Fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedId"></param>
    ''' <param name="invoiceEntityCapitatedDistributionId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId(ByVal invoiceEntityCapitatedId As Integer, ByVal invoiceEntityCapitatedDistributionId As Integer) As ActionResult(Of List(Of InvoiceEntityCapitatedDistributionDetail))

End Interface