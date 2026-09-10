#Region "Imports"

Imports DevExpress.Xpo

#End Region

Public Interface IReportInvoiceTraceability

#Region "XPO"

    ''' <summary>
    ''' Establece el datasource de los clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomerXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de las facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de las facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OperatingUnitXpo As XPInstantFeedbackSource

#End Region

End Interface
