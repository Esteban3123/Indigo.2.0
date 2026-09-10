'***********************************************************************
' Assembly         : Application.Budget
' Author           : Oscar Ivan Sierra  Jaramillo
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions

#End Region

Public Class BudgetEntryAdminService
    Implements IBudgetEntryAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de las entidades de categorias
    ''' </summary>
    Private _BBudgetEntryRepository As IBudgetEntryRepository

    ''' <summary>
    ''' Repositorio de la cabecera del presupuesto incial
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetHeaderRepository As IBudgetHeaderRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal BudgetEntryRepository As IBudgetEntryRepository, budgetHeaderRepository As IBudgetHeaderRepository)
        If BudgetEntryRepository Is Nothing Then
            Throw New ArgumentNullException("repositorybudgetEntryRepository")
        End If
        If budgetHeaderRepository Is Nothing Then
            Throw New ArgumentNullException("budgetHeaderRepository")
        End If
        Me._BBudgetEntryRepository = BudgetEntryRepository
        _budgetHeaderRepository = budgetHeaderRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene las categorias teniendo en cuenta el id de la vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetCategory(ValidityId As Integer, audit As AuditMessage, RevenueType As String) As List(Of BudgetEntry) Implements IBudgetEntryAdminService.GetBudgetCategory
        If String.IsNullOrEmpty(ValidityId) Then
            Throw New ArgumentNullException("ValidityId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ListbudgeEntry As List(Of BudgetEntry) = Me._BBudgetEntryRepository.GetBudgetCategory(ValidityId, RevenueType)

            Return ListbudgeEntry
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveBudget(BudgetHeader As BudgetHeader, state As Integer, audit As AuditMessage) As ActionResult(Of BudgetHeader) Implements IBudgetEntryAdminService.SaveBudget
        If BudgetHeader Is Nothing Then
            Throw New ArgumentNullException("BudgetHeader")
        End If
        Dim unitOfWork As IUnitWork = Me._budgetHeaderRepository.UnitWork
        Try
            Dim auxBudgetHeader As BudgetHeader = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of BudgetHeader)
            Dim status As Integer

            If BudgetHeader.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                BudgetHeader.CreationUser = audit.CodeUser
                BudgetHeader.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxBudgetHeader = BudgetHeader.OriginalValue
                BudgetHeader.ModificationUser = audit.CodeUser
                BudgetHeader.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            If BudgetHeader.Budget IsNot Nothing AndAlso BudgetHeader.Budget.Count > 0 Then
                BudgetHeader.Budget.ToList.ForEach(Sub(item)
                                                       If item.ChangeTracker.State = ObjectState.Added Then
                                                           item.CreationUser = audit.CodeUser
                                                           item.CreationDate = DateTime.Now
                                                       ElseIf item.ChangeTracker.State = ObjectState.Modified Then
                                                           item.ModificationUser = audit.CodeUser
                                                           item.ModificationDate = DateTime.Now
                                                       End If
                                                   End Sub)
            End If


            Me._budgetHeaderRepository.SaveEntity(BudgetHeader)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of BudgetHeader)(BudgetHeader, audit, status, auxBudgetHeader)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            BudgetHeader.MarkAsUnchanged()

            Return New ActionResult(Of BudgetHeader) With {.StateResult = True, .ObjectEmbbeded = BudgetHeader}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BudgetHeader) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BudgetHeader) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetBudgetBudget(ValidityId As Integer, itemType As Integer, audit As AuditMessage) As List(Of Domain.Entities.Budget) Implements IBudgetEntryAdminService.GetBudgetBudget
        Try
            Return _BBudgetEntryRepository.GetBudgetBudget(ValidityId, itemType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetBudgetBudgetInitialValueZero(ValidityId As Integer, itemType As Integer, audit As AuditMessage, FlagInitialValue As Boolean) As List(Of Domain.Entities.Budget) Implements IBudgetEntryAdminService.GetBudgetBudgetInitialValueZero
        Try
            Return _BBudgetEntryRepository.GetBudgetBudgetInitialValueZero(ValidityId, itemType, FlagInitialValue)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la cabecera con sus detalles del presupuesto inicial
    ''' </summary>
    ''' <param name="budgetaryValidityId"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetHeader(budgetaryValidityId As Integer, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetHeader) Implements IBudgetEntryAdminService.GetBudgetHeader
        If budgetaryValidityId = 0 Then
            Throw New ArgumentNullException("budgetaryValidityId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BudgetHeader As BudgetHeader = Me._BBudgetEntryRepository.GetBudgetHeader(budgetaryValidityId, type)
            If BudgetHeader IsNot Nothing AndAlso BudgetHeader.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetHeader)(BudgetHeader, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BudgetHeader) With {.StateResult = True, .ObjectEmbbeded = BudgetHeader}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BudgetHeader) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            Me._BBudgetEntryRepository = Nothing
            Me._budgetHeaderRepository = Nothing
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
