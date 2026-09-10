#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

Public Class PTreasuryRevaluation

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ITreasuryRevaluation

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ITreasuryRevaluation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una revaluación por periodo
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    Public Function GetTreasuryRevaluationByMonthAndYear(Month As Integer, Year As Integer) As List(Of TreasuryRevaluationDetailXpo)
        Dim filtroConsulta As String = $"TreasuryRevaluation.TreasuryRevaluationControl.Year ={Year} and TreasuryRevaluation.TreasuryRevaluationControl.Month ={Month}"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryRevaluationDetailXpo)(Nothing, filtroConsulta)
    End Function
#End Region

End Class
