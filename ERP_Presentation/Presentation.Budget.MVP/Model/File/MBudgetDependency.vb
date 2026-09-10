'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
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

''' <summary>
''' Clase que expone los metodos de servicios
''' </summary>
''' <remarks></remarks>
Public Class MBudgetDependency
    Inherits ModelBaseBudget
    Implements IDisposable
    Public Shared TAG As String = "204"

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(TAG As String)
        MyBase.New(TAG)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una dependencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetDependencyAsync(code As String, validityId As Integer) As Task(Of Dependency)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetDependencyAsync(code, validityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una dependencia by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetDependencyByValidityAsync(code As String, ValidityId As String) As Task(Of Dependency)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetDependencyByValidityAsync(code, ValidityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una dependencia
    ''' </summary>
    ''' <param name="Dependency">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Async Function DeleteBudgetDependency(Dependency As Dependency) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteBudgetDependencyAsync(Dependency, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una dependencia
    ''' </summary>
    ''' <param name="Dependency">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SaveBudgetDependency(Dependency As Dependency, ByVal idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.Dependency))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveBudgetDependencyAsync(Dependency, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ChangeState(ByVal code As String, validityId As Integer, ByVal state As Boolean) As Task(Of ActionResult(Of Dependency))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStateBudgetDependencyAsync(code, validityId, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una dependencia por el Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetDependencyById(id As Integer) As Task(Of Dependency)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetDependencyByIdAsync(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
