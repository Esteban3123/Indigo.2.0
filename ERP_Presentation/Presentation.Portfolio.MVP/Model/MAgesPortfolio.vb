'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
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

Public Class MAgesPortfolio
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
    ''' Guarda una edad de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveAgesPortfolio(ByVal agesPortfolio As AgesPortfolio) As Task(Of ActionResult(Of AgesPortfolio))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SaveAgesPortfolioAsync(agesPortfolio, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una edad de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteAgesPortfolio(ByVal agesPortfolio As AgesPortfolio) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.DeleteAgesPortfolioAsync(agesPortfolio, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos las edades de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAgesPortfolio() As Task(Of ActionResult(Of List(Of AgesPortfolio)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ListAgesPortfolioAsync()
    End Function

    Public Function ListAgesPortfolioSimple() As ActionResult(Of List(Of AgesPortfolio))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ListAgesPortfolio()
    End Function

    Public Function ListAgesPortfolioByIdSettingPortfolio(idSettingPortfolio As Integer) As List(Of AgesPortfolio)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ListAgesPortfolioByIdSettingPortfolio(idSettingPortfolio)
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