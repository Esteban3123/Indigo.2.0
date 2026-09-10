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

Public Class MCostGeneralExpenses
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
    ''' Elimina un centro de produccion
    ''' </summary>
    Public Async Function DeleteCostGeneralExpense(generalExpense As CostGeneralExpense) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteGeneralExpenseAsync(generalExpense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the general expense by main account identifier.
    ''' </summary>
    Public Async Function GetCostGeneralExpenseByMainAccountId(MainAccountId As Integer) As Task(Of CostGeneralExpense)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetGeneralExpenseByMainAccountIdAsync(MainAccountId)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    Public Async Function GetCostGeneralExpense(code As String) As Task(Of ActionResult(Of CostGeneralExpense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetGeneralExpenseAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Async Function GetCostGeneralExpenseById(id As Integer) As Task(Of CostGeneralExpense)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetGeneralExpenseByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Public Async Function SaveCostGeneralExpense(generalExpense As CostGeneralExpense, ByVal idSequence As Long) As Task(Of ActionResult(Of CostGeneralExpense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveGeneralExpenseAsync(generalExpense, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    Public Async Function UpdateStateCostGeneralExpense(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of CostGeneralExpense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.UpdateStateCostGeneralExpenseAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ImportDetailsToCostDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionBaseDetail As List(Of CostDistributionBaseDetail), Data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of CostDistributionBaseDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ImportDetailsToCostDistributionBaseAsync(DistributionType, MeasurementUnit, ListDistributionBaseDetail, Data)
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