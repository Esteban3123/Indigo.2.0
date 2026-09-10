#Region "Imports"

Imports Application.Billing
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BillingService
    Implements IBillingServiceInvoiceEntityCapitatedDistribution

    ''' <summary>
    ''' Obtiene la Distribución de Factura Monto Fijo por el Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceEntityCapitatedDistributionById(id As Integer, audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IBillingServiceInvoiceEntityCapitatedDistribution.GetInvoiceEntityCapitatedDistributionById
        Using service As IInvoiceEntityCapitatedDistributionAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedDistributionAdminService)()
            Return service.GetInvoiceEntityCapitatedDistributionById(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la Distribución de Factura Monto Fijo por el Code
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceEntityCapitatedDistributionByCode(code As String, audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IBillingServiceInvoiceEntityCapitatedDistribution.GetInvoiceEntityCapitatedDistributionByCode
        Using service As IInvoiceEntityCapitatedDistributionAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedDistributionAdminService)()
            Return service.GetInvoiceEntityCapitatedDistributionByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda, Anula o Confirma la Distribución de Factura Monto Fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedDistribution"></param>
    ''' <param name="ListInvoiceEntityCapitatedDistributionDetail"></param>
    ''' <param name="session"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, ListInvoiceEntityCapitatedDistributionDetail As List(Of Integer), session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IBillingServiceInvoiceEntityCapitatedDistribution.SaveInvoiceEntityCapitatedDistribution
        Using service As IInvoiceEntityCapitatedDistributionAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedDistributionAdminService)()
            Return service.SaveAndConfirmInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution, ListInvoiceEntityCapitatedDistributionDetail, session, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Reversa Distribución de Factura Monto Fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedDistribution"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ReverseInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, session As SessionValues) As ActionResult(Of InvoiceEntityCapitatedDistribution) Implements IBillingServiceInvoiceEntityCapitatedDistribution.ReverseInvoiceEntityCapitatedDistribution
        Using service As IInvoiceEntityCapitatedDistributionAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedDistributionAdminService)()
            Return service.ReverseInvoiceEntityCapitatedDistribution(invoiceEntityCapitatedDistribution, session)
        End Using
    End Function

End Class