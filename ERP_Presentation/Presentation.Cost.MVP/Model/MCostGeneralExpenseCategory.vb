'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/11/2016
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

#End Region

Public Class MCostGeneralExpenseCategory
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
    ''' Elimina una estructura Organizacional
    ''' </summary>
    Public Async Function DeleteCostGeneralExpenseCategory(record As Domain.Entities.CostGeneralExpenseCategory) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.DeleteCostGeneralExpenseCategoryAsync(record, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function GetCostGeneralExpenseCategory(code As String) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostGeneralExpenseCategoryAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateState(code As String, state As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.UpdateStateCostGeneralExpenseCategoryAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetCostGeneralExpenseCategoryById(id As Integer) As Task(Of ActionResult(Of Domain.Entities.CostGeneralExpenseCategory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetCostGeneralExpenseCategoryByIdAsync(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda una estructura Organizacional
    ''' </summary>
    Public Async Function SaveCostGeneralExpenseCategory(record As Domain.Entities.CostGeneralExpenseCategory) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.CostGeneralExpenseCategory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveCostGeneralExpenseCategoryAsync(record, Me._indigoSessionValues.AuditMessageWcf)
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