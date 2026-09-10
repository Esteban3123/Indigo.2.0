Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IInvoiceEntityCapitatedDistributionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el registro por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvoiceEntityCapitatedDistributionById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Obtiene el registro por Code
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvoiceEntityCapitatedDistributionByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Guarde el registro
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedDistribution"></param>
    ''' <param name="ListInvoiceEntityCapitatedDistributionDetail"></param>
    ''' <param name="session"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Function SaveInvoiceEntityCapitatedDistribution(ByVal invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, ByVal ListInvoiceEntityCapitatedDistributionDetail As List(Of Integer), ByVal session As SessionValues, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Confirma el registro
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ConfirmInvoiceEntityCapitatedDistribution(ByVal id As InvoiceEntityCapitatedDistribution, ByVal session As SessionValues) As ActionResult(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Guarde el registro
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedDistribution"></param>
    ''' <param name="ListInvoiceEntityCapitatedDistributionDetail"></param>
    ''' <param name="session"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Function SaveAndConfirmInvoiceEntityCapitatedDistribution(ByVal invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, ByVal ListInvoiceEntityCapitatedDistributionDetail As List(Of Integer), ByVal session As SessionValues, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Guarde el registro
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedDistribution"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ReverseInvoiceEntityCapitatedDistribution(ByVal invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution, ByVal session As SessionValues) As ActionResult(Of InvoiceEntityCapitatedDistribution)

End Interface