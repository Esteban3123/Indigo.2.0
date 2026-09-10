'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Kevin Garay    
' Created          : 05-08-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

Public Class MBudgetModification
    Inherits ModelBaseBudget
    Implements IDisposable

#Region "fields"
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
        MyBase.New(tag)
        _tagForm = tag
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtener el presupuesto incial de una vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id asociado a la vigencia.</param>
    ''' <param name="itemType">Si es 1=ingreso  2=gasto</param>
    ''' <returns>La Profesion</returns>
    Public Async Function GetBudgetBudgetAsync(ByVal ValidityId As Integer, itemType As Integer) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetBudgetAsync(ValidityId, itemType, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener el presupuesto incial de una vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id asociado a la vigencia.</param>
    ''' <param name="itemType">Si es 1=ingreso  2=gasto</param>
    ''' <returns>La Profesion</returns>
    Public Async Function GetBudgetBudgetInitialValueZero(ByVal ValidityId As Integer, itemType As Integer, FlagInitialValue As Boolean) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetBudgetInitialValueZeroAsync(ValidityId, itemType, FlagInitialValue, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una modificacion de presupuesto por su código
    ''' </summary>
    ''' <param name="code">codigo de la modificacion de presupuesto</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Async Function GetBudgetModificationAsync(code As String, type As Integer, budgetaryValidityId As Integer) As Task(Of BudgetModification)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetModificationAsync(code, type, budgetaryValidityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveBudgetMoficationAsync(budgetModification As BudgetModification, ByVal listDetailsForDelete As System.Collections.Generic.List(Of Integer)) As Task(Of ActionResult(Of BudgetModification))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveBudgetMoficationAsync(budgetModification, listDetailsForDelete, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ChangeState(code As String, state As Integer, itemType As Integer, budgetaryValidityId As Integer) As Task(Of ActionResult(Of BudgetModification))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStateBudgetModificationAsync(code, itemType, state, budgetaryValidityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    Public Async Function DeleteBudgetModificationAsync(budgetModification As BudgetModification) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteBudgetModificationAsync(budgetModification, Me._indigoSessionValues.AuditMessageWcf)
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
