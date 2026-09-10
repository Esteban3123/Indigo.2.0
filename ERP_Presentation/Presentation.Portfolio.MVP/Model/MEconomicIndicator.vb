'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
#End Region

Public Class MEconomicIndicator
    Implements IDisposable

#Region "fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    

    ''' <summary>
    ''' metodo para eliminar un indicador economico
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteEconomicIndicator(ByVal economicIndicator As EconomicIndicator) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.DeleteEconomicIndicatorAsync(economicIndicator, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para guardar un indicador economico
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveEconomicIndicator(ByVal economicIndicator As EconomicIndicator, ByVal idSequense As Int64) As Task(Of ActionResult(Of EconomicIndicator))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SaveEconomicIndicatorAsync(economicIndicator, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para obtener un indicador economico por año y mes 
    ''' </summary>    
    ''' <returns></returns>
    Public Async Function GetEconomicIndicator(ByVal year As String, ByVal month As String) As Task(Of EconomicIndicator)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetEconomicIndicatorAsync(year, month, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetEconomicIndicatorByCode(ByVal code) As Task(Of EconomicIndicator)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetEconomicIndicatorByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Changes the state.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of EconomicIndicator))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ChangeStateEconomicIndicatorAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
