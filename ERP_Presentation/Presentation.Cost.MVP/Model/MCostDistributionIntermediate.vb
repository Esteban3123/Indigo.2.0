'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-01-2015
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

Public Class MCostDistributionIntermediate
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina una distribución intermedia
    ''' </summary>
    Public Async Function DeleteDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteDistributionIntermediateAsync(distributionIntermediate, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Public Async Function GetDistributionIntermediate(code As String) As Task(Of ActionResult(Of CostDistributionIntermediate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetDistributionIntermediateAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Public Async Function GetDistributionIntermediateById(id As Integer) As Task(Of CostDistributionIntermediate)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetDistributionIntermediateByIdAsync(id)
    End Function

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Public Async Function ListDistributionIntermediateByYearMonth(year As Integer, month As Integer) As Task(Of List(Of CostDistributionIntermediate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListDistributionIntermediateByYearMonthAsync(year, month)
    End Function

    ''' <summary>
    ''' Lists the period with data by maximum period simple.
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionIntermediateSimple(ByVal year As Integer, ByVal month As Integer) As List(Of String)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Async Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year As Integer, month As Integer) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListPeriodWithDataByMaximumPeriodDistributionIntermediateAsync(year, month)
    End Function

    ''' <summary>
    ''' Guarda una distribución intermedia
    ''' </summary>
    Public Async Function SaveDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, idSequence As Long) As Task(Of ActionResult(Of CostDistributionIntermediate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveDistributionIntermediateAsync(distributionIntermediate, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Updates the state distribution intermediate.
    ''' </summary>
    Public Async Function UpdateStateDistributionIntermediate(code As String, state As Boolean) As Task(Of ActionResult(Of CostDistributionIntermediate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.UpdateStateDistributionIntermediateAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
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