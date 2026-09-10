Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

Public Class PReportRadicateInvoice

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IReportRadicateInvoice

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
    Public Sub New(ByRef iview As IReportRadicateInvoice)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para Cargar el data source Del clientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionCustomerReportPortfolio()
        View.CustomerXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListCollectionCustomerReportPortfolio()
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del radicados
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionRadicateInvoice()
        View.RadicateInvoiceXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListCollectionRadicateInvoice(Nothing)
    End Sub

#End Region

End Class
