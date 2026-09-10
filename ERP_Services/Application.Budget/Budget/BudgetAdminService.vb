'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions

#End Region

Public Class BudgetAdminService
    Implements IBudgetAdminService

#Region "Variables"
    ''' <summary>
    ''' Variable tipo repositorio para presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal budgetRepository As IBudgetRepository)
        If budgetRepository Is Nothing Then
            Throw New ArgumentNullException("budgetRepository Vacío")
        End If
        _budgetRepository = budgetRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un presupuesto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetById(Id As Integer, audit As AuditMessage) As ActionResult(Of Domain.Entities.Budget) Implements IBudgetAdminService.GetBudgetById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Budget As Domain.Entities.Budget = Me._budgetRepository.GetBudgetById(Id)
            If Budget IsNot Nothing AndAlso Budget.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Budget)(Budget, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Domain.Entities.Budget) With {.StateResult = True, .ObjectEmbbeded = Budget}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.Budget) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function
    ''' <summary>
    ''' Obtiene el saldo del presupuesto por id del rubro y del tipo
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="revenueTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId As Integer, revenueTypeId As Integer) As Decimal Implements IBudgetAdminService.GetBalanceBudgetByCategoryIdAndRevenueTypeId
        If categoryId = 0 Then
            Throw New ArgumentNullException("categoryId")
        End If
        If revenueTypeId = 0 Then
            Throw New ArgumentNullException("revenueTypeId")
        End If
        Try
            Return _budgetRepository.GetBalanceBudgetByCategoryIdAndRevenueTypeId(categoryId, revenueTypeId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Decimal
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _budgetRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
