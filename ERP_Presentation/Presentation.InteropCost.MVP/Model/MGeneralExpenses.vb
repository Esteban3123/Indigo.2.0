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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Class MGeneralExpenses
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
    Public Async Function DeleteGeneralExpense(generalExpense As GeneralExpense) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteGeneralExpenseAsync(generalExpense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the general expense by main account identifier.
    ''' </summary>
    Public Async Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As Task(Of GeneralExpense)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetGeneralExpenseByMainAccountIdAsync(MainAccountId)
    End Function

    Public Async Function ListGeneralExpenseByStatus(status As Boolean) As Task(Of List(Of GeneralExpense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListGeneralExpenseByStatusAsync(status)
    End Function

    Public Async Function ListGeneralExpenseByPeriod(year As Integer, month As Integer) As Task(Of List(Of GeneralExpense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.ListGeneralExpenseByPeriodAsync(year, month)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    Public Async Function GetGeneralExpense(code As String) As Task(Of ActionResult(Of GeneralExpense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetGeneralExpenseAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Async Function GetGeneralExpenseById(id As Integer) As Task(Of GeneralExpense)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetGeneralExpenseByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Public Async Function SaveGeneralExpense(generalExpense As GeneralExpense, ByVal idSequence As Long) As Task(Of ActionResult(Of GeneralExpense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveGeneralExpenseAsync(generalExpense, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function UpdateStateGeneralExpense(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of GeneralExpense))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.UpdateStateGeneralExpenseAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    Public Function ListAccounts() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListMainAccountErpByNivel({5}.ToList)
    End Function

    Public Function GetCTNCUENTAById(id As Integer) As CTNCUENTAXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.GetCTNCUENTAById(id)
    End Function

    Public Function ListMeasureUnitByType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListMeasureUnitByType(4)
    End Function

    Public Function ListProductionCenterCostCenterByProductionCenterId(id As Integer) As XPCollection(Of ProductionCenterCostCenterXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InteropCostService.ListProductionCenterCostCenterByProductionCenterId(id)
    End Function

    Public Function ListCostCenterDinamicByListId(listId As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListCostCenterDinamicByListId(listId)
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