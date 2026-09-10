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
Imports Domain.Entities.Service

#End Region

Public Class AvailabilityAdminService
    Implements IAvailabilityAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _availabilityRepository As IAvailabilityRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseBudgetDRepository
    ''' <summary>
    ''' Repositorio de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetHeaderRepository As IBudgetHeaderRepository
    ''' <summary>
    ''' Variable tipo repositorio para presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetRepository As IBudgetRepository

    Private _budgetService As IBudgetService

    ''' <summary>
    ''' Repositorio del control de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _BudgetControlRepository As IBudgetControlRepository

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal availabilityRepository As IAvailabilityRepository, ByVal secuenseDRepository As ISequenseBudgetDRepository, budgetHeaderRepository As IBudgetHeaderRepository, budgetRepository As IBudgetRepository, _
                   budgetService As IBudgetService, budgetControlRepository As IBudgetControlRepository)
        If availabilityRepository Is Nothing Then
            Throw New ArgumentNullException("recognitionRepository Vacío")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If budgetHeaderRepository Is Nothing Then
            Throw New ArgumentNullException("budgetHeaderRepository")
        End If
        If budgetRepository Is Nothing Then
            Throw New ArgumentNullException("budgetRepository vacío")
        End If
        _availabilityRepository = availabilityRepository
        _secuenseDRepository = secuenseDRepository
        _budgetHeaderRepository = budgetHeaderRepository
        _budgetRepository = budgetRepository
        _budgetService = budgetService
        _BudgetControlRepository = budgetControlRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina una disponibilidad
    ''' </summary>
    ''' <param name="availability"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAvailability(availability As Domain.Entities.Availability, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult Implements IAvailabilityAdminService.DeleteAvailability
        If availability Is Nothing Then
            Throw New ArgumentNullException("availability")
        End If
        Dim unitOfWork As IUnitWork = Me._availabilityRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of Availability)
            auditProcess = New IndigoAuditSimpleEntity(Of Availability)(availability, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._availabilityRepository.DeleteEntity(availability)
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
    ''' Obtiene una disponibilidad por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="ItemType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailability(Code As String, ItemType As Byte, BudgetaryValidityId As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Availability) Implements IAvailabilityAdminService.GetAvailability
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim Availability As Availability = Me._availabilityRepository.GetAvailability(Code.Trim(), ItemType, BudgetaryValidityId)
            If Availability IsNot Nothing AndAlso Availability.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Availability)(Availability, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of Availability) With {.StateResult = True, .ObjectEmbbeded = Availability}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Availability) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una disponibilidad por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAvailabilityById(Id As Integer) As Domain.Entities.Availability Implements IAvailabilityAdminService.GetAvailabilityById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return Me._availabilityRepository.GetAvailabilityById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Availability
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una disponibilidad
    ''' </summary>
    ''' <param name="availability"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAvailability(availability As Domain.Entities.Availability, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Availability) Implements IAvailabilityAdminService.SaveAvailability
        If availability Is Nothing Then
            Throw New ArgumentNullException("availability")
        End If
        Dim unitOfWork As IUnitWork = Me._availabilityRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Dim budgetUnitOfWork As IUnitWork = Me._budgetRepository.UnitWork
        Dim unitOfWorkControlDocument As IUnitWork = Me._BudgetControlRepository.UnitWork
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim seq As BudgetSequenceDetail = Nothing
                If availability.Code Is Nothing OrElse availability.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            availability.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of Availability) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of Availability) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                'valido que el mes y el año coincidan con el de la vigencia
                Dim resultValidateMonth = _budgetService.ValidateBudgetPeriod(availability.BudgetaryValidityId, availability.DocumentDate, EBudgetType.Expense)
                If resultValidateMonth.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of Availability) With {.StateResult = False, .StateResultAux = True, .Message = resultValidateMonth.Message}
                End If

                'For Each item In availability.AvailabilityDetail.ToList.FindAll(Function(x) x.Budget.Id = 0)
                '    item.Budget.CreationUser = audit.CodeUser
                '    item.Budget.CreationDate = DateTime.Now
                'Next

                Dim auxBudget As Availability = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of Availability)
                Dim status As Integer

                If availability.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    availability.CreationUser = audit.CodeUser
                    availability.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxBudget = availability.OriginalValue
                    availability.ModificationUser = audit.CodeUser
                    availability.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                If availability.Status = 2 Then
                    For Each item In availability.AvailabilityDetail
                        Dim bt As Domain.Entities.Budget = _budgetRepository.GetBudgetById(item.BudgetId)
                        bt.StartTracking()
                        bt.ExecutedValue += item.InitialValue
                        bt.Balance = bt.TotalBudget - bt.ExecutedValue - bt.SuspendedValue
                        _budgetRepository.SaveEntity(bt)
                        budgetUnitOfWork.Commit()
                        budgetUnitOfWork.Detach(bt)
                    Next
                End If

                If availability.Status = 1 Then
                    If availability.ChangeTracker.State = ObjectState.Added Then
                        Dim pc = _BudgetControlRepository.GetBudgetControl(availability.Code, 9)
                        If pc.Id = 0 Then
                            Dim BudgetControlDocument As BudgetControl = New BudgetControl()
                            BudgetControlDocument.DocumentNumber = availability.Code
                            BudgetControlDocument.DocumentType = 9
                            BudgetControlDocument.DocumentUser = audit.CodeUser
                            BudgetControlDocument.DocumentDate = availability.CreationDate
                            _BudgetControlRepository.SaveEntity(BudgetControlDocument)
                        End If
                    End If
                ElseIf (availability.Status = 2 AndAlso availability.Id > 0) OrElse availability.Status = 3 Then
                    Dim BudgetControl = _BudgetControlRepository.GetBudgetControl(availability.Code, 9)
                    If BudgetControl.Id > 0 Then
                        BudgetControl.MarkAsDeleted()
                        _BudgetControlRepository.DeleteEntity(BudgetControl)
                    End If
                End If

                Me._availabilityRepository.SaveEntity(availability)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Availability)(availability, audit, status, auxBudget)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                availability.MarkAsUnchanged()

                Transaction.Complete()
                Return New ActionResult(Of Availability) With {.StateResult = True, .ObjectEmbbeded = availability}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                budgetUnitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of Availability) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                budgetUnitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of Availability) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
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
            _availabilityRepository = Nothing
            _secuenseDRepository = Nothing
            _budgetHeaderRepository = Nothing
            _budgetRepository = Nothing
            _budgetService = Nothing
            _BudgetControlRepository = Nothing
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
