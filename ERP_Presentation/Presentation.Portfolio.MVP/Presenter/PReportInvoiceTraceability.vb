Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Public Class PReportInvoiceTraceability
#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IReportInvoiceTraceability

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IReportInvoiceTraceability)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para Cargar el data source de las Unidades Operativas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionOperatingUnit()
        If View.OperatingUnitXpo Is Nothing Then
            View.OperatingUnitXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListOperatingUnit()
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source de las facturas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListAccountReceivable()
        If View.InvoiceXpo Is Nothing Then
            View.InvoiceXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListAccountReceivable()
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del clientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionCustomerReportPortfolio()
        If View.CustomerXpo Is Nothing Then
            View.CustomerXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListCustomerReportPortfolio()
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source del gridcontrol
    ''' </summary>
    ''' <remarks></remarks>
    Public Function LoadDatasourceInvoiceTraceability(cutoffDate As DateTime, opertatingUnitIds As String, customerIds As String, invoiceIds As String, typeReport As Byte) As XPCollection(Of PortfolioRepository.PortfolioViewReportInvoiceTraceability)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.LoadDatasourceInvoiceTraceability(cutoffDate, opertatingUnitIds, customerIds, invoiceIds, typeReport)
    End Function

#End Region
End Class
