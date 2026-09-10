#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IInvoiceEntityCapitatedDistributionRepository
    Inherits IRepository(Of InvoiceEntityCapitatedDistribution)

    ''' <summary>
    ''' Obtiene el registro por Id
    ''' </summary>
    Function GetInvoiceEntityCapitatedDistributionById(ByVal Id As Integer) As InvoiceEntityCapitatedDistribution

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetInvoiceEntityCapitatedDistributionByCode(code As String) As InvoiceEntityCapitatedDistribution

    ''' <summary>
    ''' Guarda el registro
    ''' </summary>
    ''' <param name="InvoiceEntityCapitatedDistributionXml"></param>
    ''' <param name="InvoiceEntityCapitatedDistributionDetailXml"></param>
    ''' <param name="codeUser"></param>
    Function SP_SaveInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionXml As String, InvoiceEntityCapitatedDistributionDetailXml As String, codeUser As String) As List(Of SP_SaveInvoiceEntityCapitatedDistribution_Result)

    ''' <summary>
    ''' Guarda el registro
    ''' </summary>
    ''' <param name="InvoiceEntityCapitatedDistributionId"></param>
    ''' <param name="codeUser"></param>
    Function SP_ConfirmInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionId As Integer, codeUser As String) As List(Of SP_ConfirmInvoiceEntityCapitatedDistribution_Result)

    ''' <summary>
    ''' Reversa el registro
    ''' </summary>
    ''' <param name="InvoiceEntityCapitatedDistributionId"></param>
    ''' <param name="codeUser"></param>
    Function SP_ReverseInvoiceEntityCapitatedDistribution(InvoiceEntityCapitatedDistributionId As Integer, codeUser As String) As List(Of SP_ReverseInvoiceEntityCapitatedDistribution_Result)

End Interface