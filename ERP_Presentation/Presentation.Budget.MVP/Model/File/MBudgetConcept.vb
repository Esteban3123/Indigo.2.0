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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Data.Filtering

#End Region

''' <summary>
''' Clase que expone los metodos de servicios
''' </summary>
''' <remarks></remarks>
Public Class MBudgetConcept
    Inherits ModelBaseBudget
    Implements IDisposable

    Public Shared TAG As String = "205"


#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene un concepto
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetConceptAsync(code As String, validityId As Integer) As Task(Of Concept)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetConceptAsync(code, validityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetBudgetConceptByValidityAsync(code As String, ValidityId As String) As Task(Of Concept)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetConceptByValidityAsync(code, ValidityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un concepto
    ''' </summary>
    ''' <param name="Concept">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Async Function DeleteBudgetConcept(Concept As Concept) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteBudgetConceptAsync(Concept, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un concepto
    ''' </summary>
    ''' <param name="Concept">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SaveBudgetConcept(Concept As Concept, ByVal idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.Concept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveBudgetConceptAsync(Concept, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ChangeState(ByVal code As String, validityId As Integer, ByVal state As Boolean) As Task(Of ActionResult(Of Concept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStateBudgetConceptAsync(code, validityId, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista los motivos de anulación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAnnulmentConcept(BudgetaryValidityId As Integer) As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse($"BudgetaryValidityId = {BudgetaryValidityId} And Status = 1")

        Dim session As New IndigoXPOSession(Of BudgetBudgetConceptXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(BudgetBudgetConceptXpo)), Nothing, criteria)
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
