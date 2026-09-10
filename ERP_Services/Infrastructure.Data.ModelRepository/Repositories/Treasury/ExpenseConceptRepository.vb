'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ExpenseConceptRepository
    Inherits GenericRepository(Of ExpenseConcepts)
    Implements IExpenseConceptRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un concepto de egreso
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetExpenseConcept(code As String) As ExpenseConcepts Implements IExpenseConceptRepository.GetExpenseConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As ExpenseConcepts In Me._context.ExpenseConcepts.Include("ExpenseConceptCashRegisters") Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As ExpenseConcepts In Me._context.ExpenseConcepts.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New ExpenseConcepts()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetExpenseConceptById(Id As Integer, Optional tracking As Boolean = True) As ExpenseConcepts Implements IExpenseConceptRepository.GetExpenseConceptById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If

        Dim res As ExpenseConcepts = Nothing

        If tracking Then
            res = (From d As ExpenseConcepts In Me._context.ExpenseConcepts.Include("ExpenseConceptCashRegisters").Include("CashFlowConcept").AsNoTracking Where d.Id.Equals(Id) Select d).FirstOrDefault()
        Else
            res = (From d As ExpenseConcepts In Me._context.ExpenseConcepts.AsNoTracking().Include("CashFlowConcept").AsNoTracking Where d.Id.Equals(Id) Select d).FirstOrDefault()
        End If

        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From d As ExpenseConcepts In Me._context.ExpenseConcepts.AsNoTracking() Where d.Id.Equals(Id) Select d).SingleOrDefault()
            Return res
        Else
            Return New ExpenseConcepts()
        End If
    End Function

    ''' <summary>
    ''' Obtiene los conceptos de egreso que estan relacionados a un concepto de flujo de caja
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetExpenseConceptByFlowConcept(id As Integer) As List(Of ExpenseConcepts) Implements IExpenseConceptRepository.GetExpenseConceptByFlowConcept
        Return (From d As ExpenseConcepts In Me._context.ExpenseConcepts.AsNoTracking Where d.IdCashFlowConcept = id Select d).ToList()
    End Function

End Class
