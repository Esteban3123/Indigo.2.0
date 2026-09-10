'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Application.Security

Public Class CashReceiptConceptAdminService
    Implements ICashReceiptConceptAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de conceptos de recibos de caja
    ''' </summary>
    Private _cashReceiptConceptRepository As ICashReceiptConceptRepository
    Private _userAdminService As IUserAdminService
    ''' <summary>
    ''' contiene el repositorio de las secuencia numérica
    ''' </summary>
    Private _sequenceRepository As ISequenseTreasuryDRepository

    Private Const FORM_NAME As String = "Conceptos de Recibos de Caja"

#End Region

    Public Sub New(ByVal cashReceiptConceptRepository As ICashReceiptConceptRepository, ByVal sequenceRepository As ISequenseTreasuryDRepository,
                   userAdminService As IUserAdminService)
        If cashReceiptConceptRepository Is Nothing Then
            Throw New ArgumentNullException("cashReceiptConceptRepository")
        End If
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        _userAdminService = userAdminService
        _cashReceiptConceptRepository = cashReceiptConceptRepository
        _sequenceRepository = sequenceRepository
    End Sub

    ''' <summary>
    ''' Elimina un concepto de recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">cashReceiptConcept</exception>
    Public Function DeleteCashReceiptConcept(cashReceiptConcept As CashReceiptConcepts, audit As AuditMessage) As ActionResult Implements ICashReceiptConceptAdminService.DeleteCashReceiptConcept
        If cashReceiptConcept Is Nothing Then
            Throw New ArgumentNullException("cashReceiptConcept")
        End If
        Dim unitOfWork As IUnitWork = Me._cashReceiptConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                cashReceiptConcept.ModificationUser = audit.CodeUser
                cashReceiptConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CashReceiptConcepts)(cashReceiptConcept, audit, status)

                While cashReceiptConcept.CashReceiptConceptUser.Any()
                    cashReceiptConcept.CashReceiptConceptUser.FirstOrDefault().MarkAsDeleted()
                End While

                cashReceiptConcept.MarkAsDeleted()
                Me._cashReceiptConceptRepository.SaveEntity(cashReceiptConcept)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de recibo de caja
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetCashReceiptConcept(code As String, ByVal audit As AuditMessage) As CashReceiptConcepts Implements ICashReceiptConceptAdminService.GetCashReceiptConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cashReceiptConcept As CashReceiptConcepts = Me._cashReceiptConceptRepository.GetCashReceiptConcept(code.Trim())
            If cashReceiptConcept IsNot Nothing AndAlso cashReceiptConcept.Id > 0 Then
                If cashReceiptConcept.CashReceiptConceptUser.Any() Then
                    Dim listUserIds = cashReceiptConcept.CashReceiptConceptUser.Select(Function(m) m.UserId).ToList()
                    Dim listUsers = _userAdminService.ListUsersByIds(listUserIds)
                    If listUsers IsNot Nothing AndAlso listUsers.Count > 0 Then
                        For Each bu In cashReceiptConcept.CashReceiptConceptUser
                            Dim user = listUsers.FirstOrDefault(Function(m) m.Id = bu.UserId)
                            If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                                bu.FullNameUser = user.Person.Fullname
                            End If
                        Next
                    End If
                End If

                Dim auditProcess As New IndigoAuditSimpleEntity(Of CashReceiptConcepts)(cashReceiptConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return cashReceiptConcept
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un concepto de recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptConcept">The cash receipt concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">bankCity</exception>
    Public Function SaveCashReceiptConcept(cashReceiptConcept As CashReceiptConcepts, audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CashReceiptConcepts) Implements ICashReceiptConceptAdminService.SaveCashReceiptConcept
        If cashReceiptConcept Is Nothing Then
            Throw New ArgumentNullException("bankCity")
        End If

        Dim unitOfWork As IUnitWork = Me._cashReceiptConceptRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(cashReceiptConcept.Code) Then
                    Dim seq As TreasurySequenceDetail = _sequenceRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            cashReceiptConcept.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CashReceiptConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), cashReceiptConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CashReceiptConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auditProcess As IndigoAuditSimpleEntity(Of CashReceiptConcepts)
                Dim status As Integer
                Dim auxCashReceiptConcept As CashReceiptConcepts = Nothing

                If cashReceiptConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    cashReceiptConcept.CreationDate = DateTime.Now
                    cashReceiptConcept.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    cashReceiptConcept.ModificationDate = DateTime.Now
                    cashReceiptConcept.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxCashReceiptConcept = cashReceiptConcept.OriginalValue
                End If

                Me._cashReceiptConceptRepository.SaveEntity(cashReceiptConcept)
                unitOfWork.Commit()
                sequenceUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CashReceiptConcepts)(cashReceiptConcept, audit, status, auxCashReceiptConcept)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                cashReceiptConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CashReceiptConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = cashReceiptConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CashReceiptConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashReceiptConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStateCashReceiptConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CashReceiptConcepts) Implements ICashReceiptConceptAdminService.UpdateStateCashReceiptConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim cashReceiptConcept As CashReceiptConcepts = Me._cashReceiptConceptRepository.GetCashReceiptConcept(code.Trim())
            If cashReceiptConcept IsNot Nothing AndAlso cashReceiptConcept.Id > 0 Then
                cashReceiptConcept.Status = state
            End If
            Dim result = Me.SaveCashReceiptConcept(cashReceiptConcept, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashReceiptConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' obtiene un concepto de recibo de caja por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.InvalidOperationException">id</exception>
    Public Function GetCashReceiptConceptById(id As Object) As CashReceiptConcepts Implements ICashReceiptConceptAdminService.GetCashReceiptConceptById
        If id = 0 Then
            Throw New InvalidOperationException("id")
        End If
        Try
            Return _cashReceiptConceptRepository.GetCashReceiptConceptById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los conceptos de recibo de caja que estan relacionados a un concepto de flujo de caja
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCashReceiptConceptByFlowConcept(id As Integer) As List(Of CashReceiptConcepts) Implements ICashReceiptConceptAdminService.GetCashReceiptConceptByFlowConcept
        If id = 0 Then
            Throw New InvalidOperationException("id")
        End If
        Try
            Return _cashReceiptConceptRepository.GetCashReceiptConceptByFlowConcept(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _cashReceiptConceptRepository = Nothing
            _sequenceRepository = Nothing
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
