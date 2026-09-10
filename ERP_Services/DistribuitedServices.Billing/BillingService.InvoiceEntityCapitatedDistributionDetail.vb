#Region "Imports"

Imports Application.Billing
Imports DistribuitedServices.Billing
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BillingService
    Implements IBillingServiceInvoiceEntityCapitatedDistributionDetail

    ''' <summary>
    ''' Obtiene los Controles de una Distribución a partir de Factura Monto Fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedId"></param>
    ''' <param name="invoiceEntityCapitatedDistributionId"></param>
    ''' <returns></returns>
    Public Function GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId(invoiceEntityCapitatedId As Integer, invoiceEntityCapitatedDistributionId As Integer) As ActionResult(Of List(Of InvoiceEntityCapitatedDistributionDetail)) Implements IBillingServiceInvoiceEntityCapitatedDistributionDetail.GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId
        Using service As IInvoiceEntityCapitatedDistributionDetailAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedDistributionDetailAdminService)()
            Return service.GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId(invoiceEntityCapitatedId, invoiceEntityCapitatedDistributionId)
        End Using
    End Function

End Class