'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-12-2014
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

Public Class MDistributionDirectCost
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

    Public Function ListDistributionDirectCostToReport(containerCost As String, distributionDirectCostId As Integer) As List(Of SP_ReportDistributionDirectCost_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListDistributionDirectCostToReport(containerCost, distributionDirectCostId)
    End Function

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    Public Async Function DeleteDistributionDirectCost(distributionDirectCost As DistributionDirectCost) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteDistributionDirectCostAsync(distributionDirectCost, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Calcula la distribución para cada centro de producción
    ''' </summary>
    ''' <param name="GeneralExpenseId">Elemento del costo</param>
    ''' <param name="value">Valor a distribuir</param>
    ''' <returns>Lista de costos</returns>
    Public Async Function CalcDistribution(GeneralExpenseId As Integer, ByVal value As Decimal, ByVal year As Integer, ByVal month As Integer, ByVal containerName As String) As Task(Of ActionResult(Of List(Of DistributionDirectCostDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.CalculateDistributionAsync(GeneralExpenseId, value, year, month, containerName)
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Public Async Function GetDistributionDirectCost(code As String) As Task(Of ActionResult(Of DistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionDirectCostAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Public Async Function GetDistributionDirectCostById(id As Integer) As Task(Of DistributionDirectCost)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetDistributionDirectCostByIdAsync(id)
    End Function

    ''' <summary>
    ''' Lists the distribution direct cost by year month.
    ''' </summary>
    Public Async Function ListDistributionDirectCostByYearMonth(ByVal year As String, ByVal month As Integer) As Task(Of List(Of DistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListDistributionDirectCostByYearMonthAsync(year, month)
    End Function

    ''' <summary>
    ''' Gets the main account value by container number account year and motn.
    ''' </summary>
    Public Async Function GetMainAccountValueByContainerNumberAccountYearAndMotn(ByVal container As String, ByVal accountId As String, ByVal year As String, ByVal month As Integer) As Task(Of ActionResult(Of SP_MainAccountValue_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetMainAccountValueByContainerNumberAccountYearAndMotnAsync(container, accountId, year, month)
    End Function

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    Public Async Function SaveDistributionDirectCost(distributionDirectCost As DistributionDirectCost, ByVal idSequence As Long) As Task(Of ActionResult(Of DistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveDistributionDirectCostAsync(distributionDirectCost, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Updates the state distribution direct cost.
    ''' </summary>
    Public Async Function UpdateStateDistributionDirectCost(code As String, state As Boolean) As Task(Of ActionResult(Of DistributionDirectCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.UpdateStateDistributionDirectCostAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
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
