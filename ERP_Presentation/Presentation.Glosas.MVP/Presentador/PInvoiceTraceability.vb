Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Public Class PInvoiceTraceability
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IInvoiceTraceability
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase MObjectionReception
    ''' </summary>
    Dim MInvoice As MInvoiceTraceability

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IInvoiceTraceability)
        If iview Is Nothing Then
            Throw New ArgumentNullException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
            Me.Indigo = SessionValues.Instance
            Me.MInvoice = New MInvoiceTraceability("567")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para Cargar el GridLookUpEdit
    ''' </summary>
    Public Async Sub Initializes()
        View.DataSourceBranch = Await MInvoice.GetBranchAll()
    End Sub

    Public Async Function LoadInvoice(container As String, Invoice As String) As Task
        View.InvoiceTraceability = Await MInvoice.GetInvoiceTraceability(container, Invoice)
        View.InvoiceTraceabilityConciliation = Await MInvoice.GetInvoiceTraceabilityConciliation(Invoice)
        View.InvoiceTraceabilityDevolution = Await MInvoice.GetInvoiceTraceabilityDevolution(Invoice)
        View.InvoiceTraceabilityRadication = Await MInvoice.GetInvoiceTraceabilityRadication(Invoice)
    End Function

End Class
