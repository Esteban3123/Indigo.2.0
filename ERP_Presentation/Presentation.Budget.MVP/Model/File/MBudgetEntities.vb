'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
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
Public Class MBudgetEntities
    Inherits ModelBaseBudget
    Implements IDisposable
    Public Shared TAG As String = "200"

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una entidad presupuestal por su codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetInstitutionAsync(ByVal code As String) As Task(Of BudgetaryEntity)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetInstitutionAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una vigencia por su id
    ''' </summary>
    ''' <param name="id">id del registro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetValidityAsync(ByVal id As String) As Task(Of BudgetaryValidity)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetValidityAsync(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene vigencias por entidad presupuestal
    ''' </summary>
    ''' <param name="budgetInstitutionId">codigo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetValidityByBudgetInstitutionAsync(ByVal budgetInstitutionId As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetValidityByBudgetEntityAsync(budgetInstitutionId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Eliminar una fuente de financiacion
    ''' </summary>
    ''' <param name="budgetInstitution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBudgetInstitutionAsync(budgetInstitution As BudgetaryEntity) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteBudgetInstitutionAsync(budgetInstitution, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gaurdar o Actualizar una fuente de financiacion
    ''' </summary>
    ''' <param name="budgetInstitution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveBudgetBudgetInstitutionAsync(budgetInstitution As BudgetaryEntity) As Task(Of Domain.Base.Entities.ActionResult(Of BudgetaryEntity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveBudgetInstitutionAsync(budgetInstitution, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Entities.BudgetaryEntity))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStateBudgetInstitutionAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
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
