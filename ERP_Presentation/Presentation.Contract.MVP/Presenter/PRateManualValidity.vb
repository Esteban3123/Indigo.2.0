#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class PRateManualValidity

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IRateManualValidity

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
    Public Sub New(ByRef iview As IRateManualValidity)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista los manuales de tarifa por estado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRateManual()
        View.RateManualXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListRateManualByStatus(True)
    End Sub

#End Region

End Class
