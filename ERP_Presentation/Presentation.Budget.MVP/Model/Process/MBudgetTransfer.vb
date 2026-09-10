'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/08/2015
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

Public Class MBudgetTransfer
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
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetTransfer(Code As String, ItemType As Byte, yearValidity As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of BudgetTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetTransferAsync(Code, ItemType, yearValidity, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener un traslado por id
    ''' </summary>
    ''' <returns>La Profesion</returns>
    Public Async Function GetBudgetTransferById(Id As Integer) As Task(Of ActionResult(Of BudgetTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetTransferByIdAsync(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Async Function SaveBudgetTransfer(ByVal budgetTransfer As BudgetTransfer, ByVal idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of BudgetTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveBudgetTransferAsync(budgetTransfer, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBudgetTransfer(ByVal budgetTransfer As BudgetTransfer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteBudgetTransferAsync(budgetTransfer, Me._indigoSessionValues.AuditMessageWcf)
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
