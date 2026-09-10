'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 23/09/2015
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
Imports Domain.Entities.Service

#End Region

Public Class AvailabilityExtensionAdminService
    Implements IAvailabilityExtensionAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para prorroga de disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityExtensionRepository As IAvailabilityExtensionRepository
    ''' <summary>
    ''' Variable tipo repositorio para disponibilidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityRepository As IAvailabilityRepository

    Private _budgetService As IBudgetService

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal availabilityExtensionRepository As IAvailabilityExtensionRepository, ByVal availabilityRepository As IAvailabilityRepository, budgetService As IBudgetService)
        If availabilityExtensionRepository Is Nothing Then
            Throw New ArgumentNullException("availabilityExtensionRepository Vacío")
        End If
        If availabilityRepository Is Nothing Then
            Throw New ArgumentNullException("availabilityRepository")
        End If
        _availabilityExtensionRepository = availabilityExtensionRepository
        _availabilityRepository = availabilityRepository
        _budgetService = BudgetService
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una prorroga de disponibilidad por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityExtensionById(Id As Integer) As AvailabilityExtension Implements IAvailabilityExtensionAdminService.GetAvailabilityExtensionById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return Me._availabilityExtensionRepository.GetAvailabilityExtensionById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AvailabilityExtension
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una prorroga de disponibilidad
    ''' </summary>
    ''' <param name="availabilityExtension"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAvailabilityExtension(availabilityExtension As AvailabilityExtension, audit As AuditMessage) As ActionResult(Of AvailabilityExtension) Implements IAvailabilityExtensionAdminService.SaveAvailabilityExtension
        If availabilityExtension Is Nothing Then
            Throw New ArgumentNullException("availabilityExtension")
        End If
        Dim unitOfWork As IUnitWork = Me._availabilityExtensionRepository.UnitWork
        Dim unitOfWorkAvailability As IUnitWork = Me._availabilityRepository.UnitWork
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try

                'valido que el mes y el año coincidan con el de la vigencia
                Dim resultValidateMonth = _budgetService.ValidateBudgetPeriod(availabilityExtension.BudgetaryValidityId, availabilityExtension.DocumentDate, EBudgetType.Expense)
                If resultValidateMonth.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of AvailabilityExtension) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateMonth.Message}
                End If

                'Dim auxBudget As Availability = Nothing
                'Dim auditProcess As IndigoAuditSimpleEntity(Of Availability)
                Dim status As Integer

                If availabilityExtension.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    availabilityExtension.CreationUser = audit.CodeUser
                    availabilityExtension.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                End If

                For Each item In availabilityExtension.AvailabilityExtensionDetail
                    Dim availability As Domain.Entities.Availability = _availabilityRepository.GetAvailabilityById(item.AvailabilityId, True)
                    availability.ExpirationDate = item.ExpirationDate
                    availability.ExpirationDays += item.ExtensionDay
                    _availabilityRepository.SaveEntity(availability)
                Next

                Me._availabilityExtensionRepository.SaveEntity(availabilityExtension)
                unitOfWork.Commit()
                unitOfWorkAvailability.Commit()
                'auditProcess = New IndigoAuditSimpleEntity(Of AvailabilityExtension)(availabilityExtension, audit, status, audit.Company, auxBudget)
                'auditProcess.Execute()

                'Se marca la entidad como sin cambios
                availabilityExtension.MarkAsUnchanged()
                Transaction.Complete()
                Return New ActionResult(Of AvailabilityExtension) With {.StateResult = True, .ObjectEmbbeded = availabilityExtension}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                unitOfWorkAvailability.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of AvailabilityExtension) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                unitOfWorkAvailability.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AvailabilityExtension) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _budgetService.Dispose()
            End If
            _availabilityExtensionRepository = Nothing
            _availabilityRepository = Nothing
            _budgetService = Nothing
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
