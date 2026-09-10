'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
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

#End Region

Public Class RefundAdminService
    Implements IRefundAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de tarjetas
    ''' </summary>
    Private _refundRepository As IRefundRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseTreasuryDRepository
    Private _cashRegisterRepository As ICashRegisterRepository
    Private _cashRegisterAdminServide As ICashRegisterAdminService
    Private _treasuryControlAdminService As ITreasuryControlAdminService

#End Region

#Region "Methods"

    Public Sub New(ByVal refundRepository As IRefundRepository, ByVal secuenseDRepository As ISequenseTreasuryDRepository, cashRegisterRepository As ICashRegisterRepository,
                   cashRegisterAdminServide As ICashRegisterAdminService, treasuryControlAdminService As ITreasuryControlAdminService)
        If refundRepository Is Nothing Then
            Throw New ArgumentNullException("refundRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _refundRepository = refundRepository
        _secuenseDRepository = secuenseDRepository
        _cashRegisterRepository = cashRegisterRepository
        _cashRegisterAdminServide = cashRegisterAdminServide
        _treasuryControlAdminService = treasuryControlAdminService
    End Sub

#End Region

    ''' <summary>
    ''' Elimina un reembolso
    ''' </summary>
    ''' <param name="refund">The refund.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">refund</exception>
    Public Function DeleteRefund(refund As Refunds, audit As AuditMessage) As ActionResult Implements IRefundAdminService.DeleteRefund
        If refund Is Nothing Then
            Throw New ArgumentNullException("refund")
        End If
        Dim unitOfWork As IUnitWork = Me._refundRepository.UnitWork
        Try
            While refund.VoucherTransaction.Count > 0
                refund.VoucherTransaction.Item(refund.VoucherTransaction.Count() - 1).IdRefund = Nothing
            End While
            refund.MarkAsDeleted()

            refund.ModificationUser = audit.CodeUser
            refund.ModificationDate = Date.Now
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
            Dim auditProcess As New IndigoAuditSimpleEntity(Of Refunds)(refund, audit, status)

            Me._refundRepository.SaveEntity(refund)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' obtiene un reembolso por codigo
    ''' </summary>
    Public Function GetRefund(code As String, audit As AuditMessage) As ActionResult(Of Refunds) Implements IRefundAdminService.GetRefund
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim refund As Refunds = Me._refundRepository.GetRefund(code.Trim())
            If refund IsNot Nothing AndAlso refund.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Refunds)(refund, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of Refunds) With {.StateResult = True, .ObjectEmbbeded = refund}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Refunds) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un reembolso por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetRefundById(id As Integer) As Refunds Implements IRefundAdminService.GetRefundById
        Try
            Return _refundRepository.GetRefundById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un reembolso
    ''' </summary>
    ''' <param name="refund">The refund.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">refund</exception>
    Public Function SaveRefund(refund As Refunds, audit As AuditMessage, withConfirm As Boolean, Optional idSequence As Long = 0, Optional ValidateRefund As Boolean = True) As ActionResult(Of Refunds) Implements IRefundAdminService.SaveRefund
        If refund Is Nothing Then
            Throw New ArgumentNullException("refund")
        End If


        Dim unitOfWork As IUnitWork = Me._refundRepository.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._secuenseDRepository.UnitWork

        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                If ValidateRefund AndAlso refund.ChangeTracker.State = ObjectState.Added Then 'Si viene desde el formulario se valida
                    'Se valida que no exista reembolso sin confirmar con la misma caja
                    Dim validateRefundRegister = _refundRepository.ValidateRefundRegister(refund.IdCashRegister)
                    If validateRefundRegister Then
                        scope.Dispose()
                        Return New ActionResult(Of Refunds) With {.StateResult = False, .Message = "No se puede guardar porque ya existe un reembolso sin confirmar con la caja seleccionada."}
                    End If

                    'Se valida que no exista reembolso confirmado con la misma caja y refunded en false
                    Dim validateRefundConfirmed = _refundRepository.ValidateRefundConfirmed(refund.IdCashRegister)
                    If validateRefundConfirmed Then
                        scope.Dispose()
                        Return New ActionResult(Of Refunds) With {.StateResult = False, .Message = "No se puede guardar porque ya existe un reembolso pendiente con la caja seleccionada."}
                    End If

                End If
                
                If String.IsNullOrEmpty(refund.Code) Then
                    Dim seq As TreasurySequenceDetail = _secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            refund.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of Refunds) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of Refunds) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Dim auxRefund As Refunds = Nothing
                Dim status As Integer
                If refund.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    refund.CreationDate = Date.Now
                    refund.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert

                    Dim treasuryControl As New TreasuryControl() With {.DocumentNumber = refund.Code, .DocumentType = 5, .DocumentUser = audit.CodeUser, .DocumentDate = refund.InitialDate}
                    Dim resultSaveControl = _treasuryControlAdminService.SaveTreasuryControl(treasuryControl, audit)
                    If resultSaveControl.StateResult = False Then
                        Return New ActionResult(Of Refunds) With {.StateResult = False, .Message = ResourceManager.GetString("SaveTreasuryControlError", "Treasury")}
                    End If

                Else
                    If refund.Status = 3 OrElse refund.Status = 255 Then
                        Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(refund.Code, 5)
                        If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                            treasuryControl.MarkAsDeleted()
                            Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit, False)
                            If resultSaveControl.StateResult = False Then
                                Return New ActionResult(Of Refunds) With {.StateResult = False, .Message = ResourceManager.GetString("DeleteTreasuryControlError", "Treasury")}
                            End If
                        End If
                        refund.AnnulmentDate = DateTime.Now
                        refund.AnnulmentUser = audit.CodeUser
                    End If
                    refund.ModificationDate = Date.Now
                    refund.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxRefund = refund.OriginalValue
                    If refund.Status = 3 Then 'Si se anula se devuelve la fecha de la caja
                        Dim cashRegister As CashRegisters = _cashRegisterRepository.GetCashRegisterById(refund.IdCashRegister)
                        cashRegister.RefundDate = refund.InitialDate
                        _cashRegisterAdminServide.SaveCashRegister(cashRegister, audit, 0, True)
                    End If
                End If

                Me._refundRepository.SaveEntity(refund)
                unitOfWork.Commit()
                unitWorkSequence.Commit()
                scope.Complete()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Refunds)(refund, audit, status, auxRefund)
                auditProcess.Execute()
                'Se marca la entidad como sin cambios
                'refund.MarkAsUnchanged()
            End Using

            If withConfirm Then
                Dim resultConfirm = ConfirmRefund(refund.Id, audit)
                If resultConfirm.StateResult = False Then
                    unitOfWork.RollbackChangesUnitOfWork()
                    Return New ActionResult(Of Refunds) With {.StateResult = True, .ObjectEmbbeded = refund, .MessageResult = resultConfirm.MessageResult, .Message = resultConfirm.Message}
                End If
                Return New ActionResult(Of Refunds) With {.StateResult = True, .ObjectEmbbeded = refund}
            Else
                Return New ActionResult(Of Refunds) With {.StateResult = True, .ObjectEmbbeded = refund}
            End If

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChangesUnitOfWork()
            Return New ActionResult(Of Refunds) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Refunds) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ConfirmRefund(IdRefund As Integer, audit As AuditMessage, Optional idSequence As Long = 0, Optional refund As Refunds = Nothing) As ActionResult(Of String) Implements IRefundAdminService.ConfirmRefund
        If IdRefund = 0 Then
            Throw New ArgumentNullException("Id")
        End If

        Dim unitOfWork As IUnitWork = Me._refundRepository.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._secuenseDRepository.UnitWork

        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})


                If refund Is Nothing Then
                    refund = Me.GetRefundById(IdRefund)
                End If

                With refund
                    .Status = 2
                End With

                Dim treasuryControl As TreasuryControl = _treasuryControlAdminService.GetTreasuryControlByDocumentNumber(refund.Code, 5)
                If treasuryControl IsNot Nothing AndAlso treasuryControl.Id > 0 Then
                    treasuryControl.MarkAsDeleted()
                    Dim resultSaveControl = _treasuryControlAdminService.DeleteTreasuryControl(treasuryControl, audit, False)
                    If resultSaveControl.StateResult = False Then
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = ResourceManager.GetString("DeleteTreasuryControlError", "Treasury")}
                    End If
                End If
                refund.ModificationDate = Date.Now
                refund.ModificationUser = audit.CodeUser
                refund.ConfirmationDate = Date.Now
                refund.ConfirmationUser = audit.CodeUser

                Dim cashRegister As CashRegisters = _cashRegisterRepository.GetCashRegisterById(refund.IdCashRegister, False)

                If cashRegister.RefundDate IsNot Nothing Then
                    If CType(cashRegister.RefundDate, DateTime).AddSeconds(1) <> CType(refund.InitialDate, DateTime) Then
                        unitOfWork.RollbackChangesUnitOfWork()
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Format("La fecha de último reembolso de la caja {0} no coincide con la fecha inicial del remmbolso", String.Concat(cashRegister.Code, " - ", cashRegister.Name))}
                    End If
                End If

                cashRegister.RefundDate = refund.FinalDate
                refund.BalanceCashRegister = cashRegister.CurrentBalance
                _cashRegisterAdminServide.SaveCashRegister(cashRegister, audit, 0, False)

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Confirm
                Dim auditProcess As New IndigoAuditSimpleEntity(Of Refunds)(refund, audit, status)

                Me._refundRepository.SaveEntity(refund)
                unitOfWork.Commit()
                unitWorkSequence.Commit()
                auditProcess.Execute()
                'Se marca la entidad como sin cambios
                refund.MarkAsUnchanged()
                scope.Complete()
            End Using
            Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = ""}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChangesUnitOfWork()
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try

    End Function

    ''' <summary>
    ''' lista los reembolsos asociados a una cuenta contable
    ''' </summary>
    ''' <param name="IdMainAccount">The identifier main account.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListRefundByAccount(IdMainAccount As Integer, audit As AuditMessage) As ActionResult(Of List(Of Refunds)) Implements IRefundAdminService.ListRefundByAccount
        Try
            Dim listRefund As List(Of Refunds) = _refundRepository.ListRefundByAccount(IdMainAccount)
            Return New ActionResult(Of List(Of Refunds)) With {.StateResult = True, .ObjectEmbbeded = listRefund}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Refunds)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Lista los reembolsos hechos a una caja
    ''' </summary>
    ''' <param name="CashRegisterId"></param>
    ''' <returns></returns>
    Public Function ListRefundByCashRegisterId(CashRegisterId As Integer, Optional getRefundWithRefunded As Boolean = False) As ActionResult(Of List(Of Refunds)) Implements IRefundAdminService.ListRefundByCashRegisterId
        Try
            Dim listRefund As List(Of Refunds) = _refundRepository.ListRefundByCashRegisterId(CashRegisterId, getRefundWithRefunded)
            Return New ActionResult(Of List(Of Refunds)) With {.StateResult = True, .ObjectEmbbeded = listRefund}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Refunds)) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Gets the refund detail by voucher transaction identifier.
    ''' </summary>
    ''' <param name="voucherTransactionId"></param>
    ''' <returns></returns>
    Public Function GetRefundDetailByVoucherTransactionId(voucherTransactionId As Integer) As RefundDetail Implements IRefundAdminService.GetRefundDetailByVoucherTransactionId
        Try
            Return _refundRepository.GetRefundDetailByVoucherTransactionId(voucherTransactionId)
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
                _cashRegisterAdminServide.Dispose()
                _treasuryControlAdminService.Dispose()
            End If
            _refundRepository = Nothing
            _secuenseDRepository = Nothing
            _cashRegisterRepository = Nothing
            _cashRegisterAdminServide = Nothing
            _treasuryControlAdminService = Nothing
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