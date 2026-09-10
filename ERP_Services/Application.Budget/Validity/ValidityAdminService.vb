'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
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
'Imports System.Data.Entity.Core
'Imports System.Data.Entity.Infrastructure
'Imports System.Data.Entity.Core
#End Region

Public Class ValidityAdminService
    Implements IValidityAdminService
#Region "Fields"

    ''' <summary>
    ''' Repositorio de las entidades presupuestales
    ''' </summary>
    Private _budgetEntitiesRepository As IBudgetInstitutionRepository

    ''' <summary>
    ''' Repositorio de las vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Private _validityRepository As IValidityRepository
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="budgetEntitiesRepository">repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal budgetEntitiesRepository As IBudgetInstitutionRepository, ByVal validityRepository As IValidityRepository)
        If budgetEntitiesRepository Is Nothing Then
            Throw New ArgumentNullException("repositorybudgetEntitiesRepository")
        End If
        If validityRepository Is Nothing Then
            Throw New ArgumentNullException("repositoryvalidityRepository")
        End If
        _budgetEntitiesRepository = budgetEntitiesRepository
        _validityRepository = validityRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene las validaciones de una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetEntityId">Id de la entidad presupuestal.</param>
    ''' <returns></returns>
    Function GetValidityByBudgetEntity(BudgetEntityId As String, audit As AuditMessage) As Object Implements IValidityAdminService.GetValidityByBudgetEntity
        If String.IsNullOrEmpty(BudgetEntityId) Then
            Throw New ArgumentNullException("BudgetEntityId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BudgetaryValidity As List(Of BudgetaryValidity) = Me._validityRepository.GetValidityByBudgetInstitutions(BudgetEntityId.Trim())
            If BudgetaryValidity IsNot Nothing AndAlso BudgetaryValidity.Count > 0 Then
                For Each item In BudgetaryValidity
                    Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetaryValidity)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                    auditObject.Execute()
                Next
            End If
            Return BudgetaryValidity
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una validacion por Id
    ''' </summary>
    ''' <param name="Id">Id de la validacion.</param>
    ''' <returns></returns>
    Public Function GetValidity(Id As String, audit As AuditMessage) As BudgetaryValidity Implements IValidityAdminService.GetValidity
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BudgetaryValidity As BudgetaryValidity = Me._validityRepository.GetValidity(Id.Trim())
            If BudgetaryValidity IsNot Nothing AndAlso BudgetaryValidity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetaryValidity)(BudgetaryValidity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return BudgetaryValidity
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Function SaveValidity(BudgetaryValidity As BudgetaryValidity, audit As AuditMessage) As ActionResult(Of BudgetaryValidity) Implements IValidityAdminService.SaveValidity
        If BudgetaryValidity Is Nothing Then
            Throw New ArgumentNullException("BudgetaryValidity")
        End If
        Dim unitOfWork As IUnitWork = Me._validityRepository.UnitWork
        Try
            Dim auxValidity As BudgetaryValidity = BudgetaryValidity.OriginalValue
            If BudgetaryValidity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse BudgetaryValidity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._validityRepository.SaveEntity(BudgetaryValidity)
            End If
            unitOfWork.Commit()
            If BudgetaryValidity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(BudgetaryValidity).Name, audit.Functional, BudgetaryValidity.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetaryValidity)(BudgetaryValidity, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf BudgetaryValidity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(BudgetaryValidity).Name, audit.Functional, BudgetaryValidity.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetaryValidity)(BudgetaryValidity, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxValidity)
                auditObject.Execute()
            End If

            'Se marca la entidad como sin cambios
            'financialSource.MarkAsUnchanged()

            Return New ActionResult(Of BudgetaryValidity) With {.StateResult = True, .ObjectEmbbeded = BudgetaryValidity}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una Vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="type">Tipo 1. Ingresos 2. Gastos</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Function CloseValidity(BudgetaryValidity As BudgetaryValidity, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetaryValidity) Implements IValidityAdminService.CloseValidity
        If BudgetaryValidity Is Nothing Then
            Throw New ArgumentNullException("BudgetaryValidity")
        End If

        Dim unitOfWork As IUnitWork = Me._validityRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim message As String = String.Empty

                If type = 1 Then
                    Dim resultStore = Me._validityRepository.SP_ClosingValidityIncome(BudgetaryValidity.Id, audit.CodeUser)
                    If resultStore.CodeResult <> 0 Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .Message = resultStore.MessageResult}
                    End If

                    message = resultStore.MessageResult
                ElseIf type = 2 Then
                    Dim resultStore = Me._validityRepository.SP_ClosingValidityExpense(BudgetaryValidity.Id, audit.CodeUser)
                    If resultStore.CodeResult <> 0 Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .Message = resultStore.MessageResult}
                    End If

                    message = resultStore.MessageResult
                Else
                    Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .Message = "El tipo de vigencia no es válido"}
                End If

                Dim auditProcess = New IndigoAuditSimpleEntity(Of BudgetaryValidity)(BudgetaryValidity, audit, Infrastructure.CrossCutting.Audit.Actions.Update, BudgetaryValidity.OriginalValue)
                auditProcess.Execute()

                BudgetaryValidity.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of BudgetaryValidity) With {.StateResult = True, .ObjectEmbbeded = BudgetaryValidity, .Message = message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Recalcular los valores de la vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">la entidad</param>
    ''' <param name="type">Tipo 1. Ingresos 2. Gastos</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Function RecalculateBalances(BudgetaryValidity As BudgetaryValidity, type As Integer, audit As AuditMessage) As ActionResult(Of BudgetaryValidity) Implements IValidityAdminService.RecalculateBalances
        If BudgetaryValidity Is Nothing Then
            Throw New ArgumentNullException("BudgetaryValidity")
        End If

        Dim unitOfWork As IUnitWork = Me._validityRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim message As String = String.Empty

                If type = 1 Then
                    Dim resultStore = Me._validityRepository.SP_RecalculateBalancesIncome(BudgetaryValidity.Id, audit.CodeUser)
                    If resultStore.CodeResult <> 0 Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .message = resultStore.MessageResult}
                    End If

                    message = resultStore.MessageResult
                ElseIf type = 2 Then
                    Dim resultStore = Me._validityRepository.SP_RecalculateBalancesExpense(BudgetaryValidity.Id, audit.CodeUser)
                    If resultStore.CodeResult <> 0 Then
                        unitOfWork.RollbackChanges()
                        transaction.Dispose()
                        Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .message = resultStore.MessageResult}
                    End If

                    message = resultStore.MessageResult
                Else
                    Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .message = "El tipo de vigencia no es válido"}
                End If

                Dim auditProcess = New IndigoAuditSimpleEntity(Of BudgetaryValidity)(BudgetaryValidity, audit, Infrastructure.CrossCutting.Audit.Actions.Update, BudgetaryValidity.OriginalValue)
                auditProcess.Execute()

                BudgetaryValidity.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of BudgetaryValidity) With {.StateResult = True, .ObjectEmbbeded = BudgetaryValidity, .message = message}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BudgetaryValidity) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Elimina una vigencia
    ''' </summary>
    ''' <param name="BudgetaryValidity">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Function DeleteValidity(BudgetaryValidity As BudgetaryValidity, audit As AuditMessage) As ActionResult Implements IValidityAdminService.DeleteValidity
        If BudgetaryValidity Is Nothing Then
            Throw New ArgumentNullException("BudgetaryValidity vacio")
        End If
        Dim unitOfWork As IUnitWork = Me._validityRepository.UnitWork
        Try
            If BudgetaryValidity.ChangeTracker.State = ObjectState.Deleted Then
                _validityRepository.DeleteEntity(BudgetaryValidity)

                unitOfWork.Commit()

                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(BudgetaryValidity).Name, audit.Functional, BudgetaryValidity.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetaryValidity)(BudgetaryValidity, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})} 'error genérico
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})} 'error de concurrencia
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"c-0000"})} ' error de dependencia
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})} ' error generico
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
            _budgetEntitiesRepository = Nothing
            _validityRepository = Nothing
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
