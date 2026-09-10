'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 24-06-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
#End Region

Public Class MCostIntermediateDistributionElements
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
    ''' Elimina una distribucion intermediate
    ''' </summary>
    ''' <param name="intermediateDistribution"></param>
    ''' <returns></returns>
    Public Async Function DeleteIntermediateDistribution(intermediateDistribution As CostIntermediateDistribution) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteIntermediateDistributionAsync(intermediateDistribution, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion intermediate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetIntermediateDistribution(code As String) As Task(Of ActionResult(Of CostIntermediateDistribution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetIntermediateDistributionAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion intermediate por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetIntermediateDistributionById(id As Integer) As Task(Of CostIntermediateDistribution)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetIntermediateDistributionByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda una distribucion intermediate
    ''' </summary>
    ''' <param name="distribution"></param>
    ''' <returns></returns>
    Public Async Function SaveIntermediateDistribution(distribution As CostIntermediateDistribution, idSequence As Long) As Task(Of ActionResult(Of CostIntermediateDistribution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveIntermediateDistributionAsync(distribution, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza una distribucion intermediate
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Async Function UpdateStateIntermediateDistribution(code As String, state As Boolean) As Task(Of ActionResult(Of CostIntermediateDistribution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.UpdateStateIntermediateDistributionAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista las distribuciones intermediate por año y mes
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Async Function ListIntermediateDistributionByYearMonth(year As Integer, month As Integer) As Task(Of List(Of CostIntermediateDistribution))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListDistributionByYearMonthAsync(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Async Function ListPeriodWithDataByMaximumPeriodIntermediateDistribution(year As Integer, month As Integer) As Task(Of List(Of String))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListPeriodWithDataByMaximumPeriodDistributionAsync(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function ListPeriodWithDataByMaximumPeriodIntermediateDistributionSimple(year As Integer, month As Integer) As List(Of String)
        'Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.ListPeriodWithDataByMaximumPeriodIntermediateDistribution(year, month)
    End Function

    Public Async Function SP_CopyPasteCostDistribution(data As List(Of List(Of String)), DistributionId As Integer, InitialDistribution As Decimal) As Task(Of ActionResult(Of List(Of CostIntermediateDistributionBaseDetail), List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SP_CopyPasteCostIntermediateDistributionAsync(data, DistributionId, InitialDistribution)
    End Function

    Public Async Function ImportDetailsToCostIntermediateDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionBaseDetail As List(Of CostIntermediateDistributionBaseDetail), Data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of CostIntermediateDistributionBaseDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ImportDetailsToCostIntermediateDistributionBaseAsync(DistributionType, MeasurementUnit, ListDistributionBaseDetail, Data)
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

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class