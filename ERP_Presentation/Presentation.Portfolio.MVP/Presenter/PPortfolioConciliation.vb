#Region "Librerias Importadas"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal
''' </summary>
Public Class PPortfolioConciliation

#Region "variables y constructor "

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IPortfolioConciliation

    ''' <summary>
    ''' Variable que se utilizapa para tratar a la cabecera como un Objeto
    ''' </summary>
    Dim PortfolioConciliationC As Object

    ''' <summary>
    ''' Variable que se utilizapa para tratar al detalle como un Objeto
    ''' </summary>
    Dim GlosaPortfolioConciliationD As List(Of Object)

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase MConciliationReception
    ''' </summary>
    Dim MConciliation As MPortfolioConciliation

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IPortfolioConciliation)
        If iview Is Nothing Then
            Throw New ArgumentNullException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
            Me.Indigo = SessionValues.Instance
            Me.MConciliation = New MPortfolioConciliation("2221")
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para Cargar el GridLookUpEdit
    ''' </summary>
    Public Async Sub Initializes()
        'View.DataSourceBranch = Await MPortfolioConciliation.GetBranchAll()
    End Sub

    ''' <summary>
    ''' obtiene la secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MPortfolioConciliation(CStr("2221")) 'tag de radicacion de cuentas, con el que quedo registrado la secuencia numerica para Doc. de reclasificacion
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene una lista de facturas por nit, de la cartera de la glosa
    ''' </summary>
    ''' <param name="nit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListXpoInvoicesByNitGlosaPortfolio(ByVal nit As String) As Object
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).GlosasService.GetXPOPortfolioGlosa(nit.Trim())
    End Function

#End Region

End Class