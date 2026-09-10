Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports System.Transactions

Public Class ExogenousFormatAdminService
    Implements IExogenousFormatAdminService

#Region "Properties"

    Private Const FORM_NAME As String = "FrmExogenousFormat"

    Private _sequenseDRepository As ISequenseAccountingDRepository
    Private _exogenousFormatRepository As IExogenousFormatRepository

#End Region

#Region "Builder"

    Public Sub New(sequenseDRepository As ISequenseAccountingDRepository, exogenousFormatRepository As IExogenousFormatRepository)
        If sequenseDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenseDRepository Vacio")
        End If
        If exogenousFormatRepository Is Nothing Then
            Throw New ArgumentNullException("exogenousFormatRepository Vacio")
        End If

        _sequenseDRepository = sequenseDRepository
        _exogenousFormatRepository = exogenousFormatRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetExogenousFormatById(id As Integer, audit As AuditMessage) As ActionResult(Of ExogenousFormat) Implements IExogenousFormatAdminService.GetExogenousFormatById
        Try
            Dim exogenousFormat As ExogenousFormat = Me._exogenousFormatRepository.GetExogenousFormatById(id)
            If exogenousFormat IsNot Nothing AndAlso exogenousFormat.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ExogenousFormat)(exogenousFormat, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ExogenousFormat) With {.StateResult = True, .ObjectEmbbeded = exogenousFormat}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExogenousFormat) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetExogenousFormatByCode(code As String, audit As AuditMessage) As ActionResult(Of ExogenousFormat) Implements IExogenousFormatAdminService.GetExogenousFormatByCode
        Try
            Dim exogenousFormat As ExogenousFormat = Me._exogenousFormatRepository.GetExogenousFormatByCode(code)
            If exogenousFormat IsNot Nothing AndAlso exogenousFormat.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ExogenousFormat)(exogenousFormat, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ExogenousFormat) With {.StateResult = True, .ObjectEmbbeded = exogenousFormat}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExogenousFormat) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveExogenousFormat(exogenousFormat As ExogenousFormat, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ExogenousFormat) Implements IExogenousFormatAdminService.SaveExogenousFormat
        Dim unitOfWork As IUnitWork = Me._exogenousFormatRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseDRepository.UnitWork
        Try
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(exogenousFormat.Code) Then
                    Dim seq As GeneralLedgerSequenceDetail = Me._sequenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            exogenousFormat.Code = res
                            seq.Next += 1
                            Me._sequenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ExogenousFormat) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), exogenousFormat.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ExogenousFormat) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ExogenousFormat = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ExogenousFormat)
                Dim status As Integer

                If exogenousFormat.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    exogenousFormat.CreationUser = audit.CodeUser
                    exogenousFormat.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _exogenousFormatRepository.GetExogenousFormatByCode(exogenousFormat.Code)
                    exogenousFormat.ModificationUser = audit.CodeUser
                    exogenousFormat.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._exogenousFormatRepository.SaveEntity(exogenousFormat)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ExogenousFormat)(exogenousFormat, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                exogenousFormat.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ExogenousFormat) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = exogenousFormat, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ExogenousFormat) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExogenousFormat) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ChangeStateExogenousFormat(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ExogenousFormat) Implements IExogenousFormatAdminService.ChangeStateExogenousFormat
        Try
            Dim exogenousFormat As ExogenousFormat = Me._exogenousFormatRepository.GetExogenousFormatByCode(code.Trim())
            If exogenousFormat IsNot Nothing AndAlso exogenousFormat.Id > 0 Then
                exogenousFormat.Status = state
            End If
            Dim result = Me.SaveExogenousFormat(exogenousFormat, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExogenousFormat) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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

            _sequenseDRepository = Nothing
            _exogenousFormatRepository = Nothing
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
