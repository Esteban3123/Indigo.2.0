'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-01-2015
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

Public Class MDistributionSecondary
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
    ''' Elimina una distribucion secundaria
    ''' </summary>
    ''' <param name="distributionSecondary"></param>
    ''' <returns></returns>
    Public Async Function DeleteDistributionSecondary(distributionSecondary As DistributionSecondary) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteDistributionSecondaryAsync(distributionSecondary, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetDistributionSecondary(code As String) As Task(Of ActionResult(Of DistributionSecondary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionSecondaryAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetDistributionSecondaryById(id As Integer) As Task(Of DistributionSecondary)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionSecondaryByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda una distribucion secundaria
    ''' </summary>
    ''' <param name="distributionSecondary"></param>
    ''' <returns></returns>
    Public Async Function SaveDistributionSecondary(distributionSecondary As DistributionSecondary, idSequence As Long) As Task(Of ActionResult(Of DistributionSecondary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveDistributionSecondaryAsync(distributionSecondary, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza una distribucion secundaria
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Async Function UpdateStateDistributionSecondary(code As String, state As Boolean) As Task(Of ActionResult(Of DistributionSecondary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.UpdateStateDistributionSecondaryAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Async Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As Task(Of List(Of DistributionSecondary))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListDistributionSecondaryByYearMonthAsync(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Async Function ListPeriodWithDataByMaximumPeriodDistributionSecondary(year As Integer, month As Integer) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListPeriodWithDataByMaximumPeriodDistributionSecondaryAsync(year, month)
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionSecondarySimple(year As Integer, month As Integer) As List(Of String)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListPeriodWithDataByMaximumPeriodDistributionSecondary(year, month)
    End Function

    Public Async Function SP_CopyPasteSecondaryDistribution(data As List(Of List(Of String)), DistributionSecondaryId As Integer, InitialDistribution As Decimal) As Task(Of ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SP_CopyPasteSecondaryDistributionAsync(data, DistributionSecondaryId, InitialDistribution)
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