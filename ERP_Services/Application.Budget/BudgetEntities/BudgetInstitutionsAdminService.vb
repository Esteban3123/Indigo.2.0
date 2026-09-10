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

#End Region

Public Class BudgetInstitutionAdminService
    Implements IBudgetInstitutionAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de las entidades presupuestales
    ''' </summary>
    Private _BudgetInstitutionRepository As IBudgetInstitutionRepository

    ''' <summary>
    ''' Repositorio de las entidades presupuestales
    ''' </summary>
    Private _BudgetInstitutionSaveRepository As IBudgetInstitutionRepository

    ''' <summary>
    ''' Repositorio de las vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Private _validityRepository As IValidityRepository

    ''' <summary>
    ''' Repositorio de consecutivo
    ''' </summary>
    ''' <remarks></remarks>
    Private _consecutiveRepository As IConsecutiveBudgetRepository
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal BudgetInstitutionRepository As IBudgetInstitutionRepository, ByVal BudgetInstitutionSaveRepository As IBudgetInstitutionRepository, ByVal validityRepository As IValidityRepository, consecutiveRepository As IConsecutiveBudgetRepository)
        If BudgetInstitutionRepository Is Nothing Then
            Throw New ArgumentNullException("repositorybudgetEntitiesRepository")
        End If
        If validityRepository Is Nothing Then
            Throw New ArgumentNullException("repositoryvalidityRepository")
        End If
        Me._BudgetInstitutionRepository = BudgetInstitutionRepository
        Me._BudgetInstitutionSaveRepository = BudgetInstitutionSaveRepository
        Me._validityRepository = validityRepository
        Me._consecutiveRepository = consecutiveRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una entidad presupuestal por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">objeto de auditoria</param>
    ''' <returns></returns>
    Public Function GetBudgetInstitution(code As String, audit As AuditMessage) As BudgetaryEntity Implements IBudgetInstitutionAdminService.GetBudgetInstitution
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim budgetEntity As BudgetaryEntity = Me._BudgetInstitutionRepository.GetBudgetInstitution(code.Trim())
            If budgetEntity IsNot Nothing AndAlso budgetEntity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetaryEntity)(budgetEntity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return budgetEntity
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una entidad presupuestal
    ''' </summary>
    ''' <param name="BudgetEntity">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Function DeleteBudgetInstitution(BudgetaryEntity As BudgetaryEntity, audit As AuditMessage) As ActionResult Implements IBudgetInstitutionAdminService.DeleteBudgetInstitution
        If BudgetaryEntity Is Nothing Then
            Throw New ArgumentNullException("BudgetaryEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._BudgetInstitutionRepository.UnitWork
        Try
            BudgetaryEntity.StartTracking()
            If BudgetaryEntity.BudgetaryValidity1 IsNot Nothing AndAlso BudgetaryEntity.BudgetaryValidity1.Count > 0 Then
                While BudgetaryEntity.BudgetaryValidity1.Count > 0
                    BudgetaryEntity.BudgetaryValidity1.Item(0).MarkAsDeleted()
                End While
            End If
            BudgetaryEntity.MarkAsDeleted()
            Dim auditProcess As IndigoAuditSimpleEntity(Of BudgetaryEntity)
            auditProcess = New IndigoAuditSimpleEntity(Of BudgetaryEntity)(BudgetaryEntity, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._BudgetInstitutionRepository.SaveEntity(BudgetaryEntity)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una Entidad Presupuestal
    ''' </summary>
    ''' <param name="BudgetaryEntity">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Function SaveBudgetInstitution(BudgetaryEntity As BudgetaryEntity, audit As AuditMessage) As ActionResult(Of BudgetaryEntity) Implements IBudgetInstitutionAdminService.SaveBudgetInstitution
        If BudgetaryEntity Is Nothing Then
            Throw New ArgumentNullException("BudgetaryEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._BudgetInstitutionRepository.UnitWork
        Try
            Dim auxBudgetary As BudgetaryEntity = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of BudgetaryEntity)
            Dim status As Integer

            If BudgetaryEntity.BudgetaryValidity1.Count > 0 Then
                For Each itemvalidity In BudgetaryEntity.BudgetaryValidity1.Where(Function(x) x.ChangeTracker.State = ObjectState.Added Or x.ChangeTracker.State = ObjectState.Modified)
                    With itemvalidity
                        If .ChangeTracker.State = ObjectState.Added Then
                            .CreationUser = audit.IdUser
                            .CreationDate = DateTime.Now
                        ElseIf .ChangeTracker.State = ObjectState.Modified Then
                            .ModificationUser = audit.IdUser
                            .ModificationDate = DateTime.Now
                        End If
                    End With
                Next
            End If

            If BudgetaryEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                BudgetaryEntity.CreationUser = audit.CodeUser
                BudgetaryEntity.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxBudgetary = BudgetaryEntity.OriginalValue
                BudgetaryEntity.ModificationUser = audit.CodeUser
                BudgetaryEntity.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._BudgetInstitutionRepository.SaveEntity(BudgetaryEntity)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of BudgetaryEntity)(BudgetaryEntity, audit, status, auxBudgetary)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            BudgetaryEntity.MarkAsUnchanged()

            Return New ActionResult(Of BudgetaryEntity) With {.StateResult = True, .ObjectEmbbeded = BudgetaryEntity}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BudgetaryEntity) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BudgetaryEntity) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStateBudgetInstitution(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BudgetaryEntity) Implements IBudgetInstitutionAdminService.ChangeStateBudgetInstitution
        Dim BudgetaryEntity As BudgetaryEntity = Me._BudgetInstitutionRepository.GetBudgetInstitution(code.Trim())
        BudgetaryEntity.Status = state
        BudgetaryEntity.MarkAsModified()
        Return SaveBudgetInstitution(BudgetaryEntity, audit)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            Me._BudgetInstitutionRepository = Nothing
            Me._BudgetInstitutionSaveRepository = Nothing
            Me._validityRepository = Nothing
            Me._consecutiveRepository = Nothing
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
