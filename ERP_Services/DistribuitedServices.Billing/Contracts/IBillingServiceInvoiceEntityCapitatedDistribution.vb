#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IBillingServiceInvoiceEntityCapitatedDistribution

    ''' <summary>
    ''' Obtiene la Distribución de Factura Monto Fijo por el Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetInvoiceEntityCapitatedDistributionById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Obtiene la Distribución de Factura Monto Fijo por el Code
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetInvoiceEntityCapitatedDistributionByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Guarda, Anula o Confirma la Distribución de Factura Monto Fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedDistribution"></param>
    ''' <param name="ListInvoiceEntityCapitatedDistributionDetail"></param>
    ''' <param name="session"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveInvoiceEntityCapitatedDistribution(ByVal invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, ByVal ListInvoiceEntityCapitatedDistributionDetail As List(Of Integer), ByVal session As SessionValues, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Reversar una Distribución de Factura Monto Fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedDistribution"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ReverseInvoiceEntityCapitatedDistribution(ByVal invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, ByVal session As SessionValues) As ActionResult(Of InvoiceEntityCapitatedDistribution)

End Interface