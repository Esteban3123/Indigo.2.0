#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PCustomers

#Region "Builder"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As ICustomers

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ICustomers)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Carga DataSource del Campo Clasificación Factura Básica
    ''' </summary>
    Public Sub InitializeBasicBillingClassification()
        View.BasicBillingClassificationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListActivePortfolioDeteriorationClassification()
    End Sub

    ''' <summary>
    ''' Carga DataSource del Campo Clasificación Factura Salud
    ''' </summary>
    Public Sub InitializeHealthInvoiceClassification()
        View.HealthInvoiceClassificationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListActivePortfolioDeteriorationClassification()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga el datasource de los bancos
    ''' </summary>
    Public Sub InitializePortfolioNoteConcept()
        Me.View.PortfolioNoteConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetAllPortfolioNoteConceptByStatus(True)
    End Sub

    ''' <summary>
    ''' Carga el datasource de los bancos
    ''' </summary>
    Public Sub InitializeRetentionConcept()
        Me.View.RetentionConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListRetentionConceptByTypeRetention("1,3", True)
    End Sub

#End Region

End Class
