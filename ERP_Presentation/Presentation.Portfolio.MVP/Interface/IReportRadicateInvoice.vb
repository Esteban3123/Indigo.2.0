Imports DevExpress.Xpo

Public Interface IReportRadicateInvoice

#Region "XPO"

    ''' <summary>
    ''' Establece el datasource de los clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomerXpo As XPCollection(Of Infrastructure.Data.Xpo.PortfolioRepository.CommonCustomerReportXpo)

    ''' <summary>
    ''' Establece el datasource de los radicados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RadicateInvoiceXpo As XPCollection(Of Infrastructure.Data.Xpo.PortfolioRepository.PortfolioRadicateInvoiceCReportXpo)

#End Region

End Interface
