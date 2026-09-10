'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/06/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository

#End Region

Public Class PPortfolioRevaluation

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPortfolioRevaluation

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IPortfolioRevaluation)
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
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Lista los activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetPortfolioRevaluationByMonthAndYear(DepreciateMonth As Integer, DepreciateYear As Integer) As PortfolioRevaluationXpo
        Dim filtroConsulta As String = "Month = " & DepreciateMonth & " And Year = " & DepreciateYear
        Dim xpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioRevaluationXpo)(Nothing, filtroConsulta)
        If xpo IsNot Nothing AndAlso xpo.Count > 0 Then
            Return xpo(0)
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class
