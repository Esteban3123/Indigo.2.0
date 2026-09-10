Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.Data.Entity.Core
Imports Application.Payroll

Public Class EthnicGroupsAdminService
    Implements IEthnicGroupsAdminService

    Private _EthnicGroupsRepository As IEthnicGroupsRepository

    Private _secuenseDRepository As IPayrollSequenceDetailRepository

    Public Sub New(ByVal pEthnicGroupsRepository As IEthnicGroupsRepository, ByVal secuenseDRepository As IPayrollSequenceDetailRepository)
        If (pEthnicGroupsRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Actividades de Tiempo Libre vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _EthnicGroupsRepository = pEthnicGroupsRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

    Public Function ChangeState(pCode As String, pStatus As Boolean, pAudit As AuditMessage) As ActionResult(Of EthnicGroups) Implements IEthnicGroupsAdminService.ChangeState
        Dim ethnicGroups As EthnicGroups = _EthnicGroupsRepository.GetEthnicGroups(pCode, True)
        ethnicGroups.Status = pStatus
        Return SaveEthnicGroups(ethnicGroups, pAudit)
    End Function

    Public Function DeleteEthnicGroups(pEthnicGroups As EthnicGroups, audit As AuditMessage) As ActionResult Implements IEthnicGroupsAdminService.DeleteEthnicGroups
        If pEthnicGroups Is Nothing Then
            Throw New ArgumentNullException("pEthnicGroups")
        End If
        Dim unitOfWork As IUnitWork = Me._EthnicGroupsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of EthnicGroups)
            auditProcess = New IndigoAuditSimpleEntity(Of EthnicGroups)(pEthnicGroups, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._EthnicGroupsRepository.DeleteEntity(pEthnicGroups)
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

    Public Function GetEthnicGroups(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of EthnicGroups) Implements IEthnicGroupsAdminService.GetEthnicGroups
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ethnicGroups As EthnicGroups = Me._EthnicGroupsRepository.GetEthnicGroups(code.Trim(), tracking)
            If ethnicGroups IsNot Nothing AndAlso ethnicGroups.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of EthnicGroups)(ethnicGroups, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of EthnicGroups) With {.StateResult = True, .ObjectEmbbeded = ethnicGroups}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EthnicGroups) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetEthnicGroupsById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of EthnicGroups) Implements IEthnicGroupsAdminService.GetEthnicGroupsById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ethnicGroups As EthnicGroups = Me._EthnicGroupsRepository.GetEthnicGroupsById(id, tracking)
            If ethnicGroups IsNot Nothing AndAlso ethnicGroups.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of EthnicGroups)(ethnicGroups, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of EthnicGroups) With {.StateResult = True, .ObjectEmbbeded = ethnicGroups}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EthnicGroups) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function SaveEthnicGroups(pEthnicGroups As EthnicGroups, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of EthnicGroups) Implements IEthnicGroupsAdminService.SaveEthnicGroups
        If pEthnicGroups Is Nothing Then
            Throw New ArgumentNullException("pEthnicGroups")
        End If
        Dim unitOfWork As IUnitWork = Me._EthnicGroupsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PayrollSequenceDetail = Nothing
            If pEthnicGroups.Code Is Nothing OrElse pEthnicGroups.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        pEthnicGroups.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of EthnicGroups) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of EthnicGroups) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxEthnicGroups As EthnicGroups = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of EthnicGroups)
            Dim status As Integer

            If pEthnicGroups.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                pEthnicGroups.CreationUser = audit.CodeUser
                pEthnicGroups.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxEthnicGroups = _EthnicGroupsRepository.GetEthnicGroups(pEthnicGroups.Code, False)
                pEthnicGroups.ModificationUser = audit.CodeUser
                pEthnicGroups.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._EthnicGroupsRepository.SaveEntity(pEthnicGroups)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of EthnicGroups)(pEthnicGroups, audit, status, auxEthnicGroups)
            auditProcess.Execute()

            pEthnicGroups.MarkAsUnchanged()

            Return New ActionResult(Of EthnicGroups) With {.StateResult = True, .ObjectEmbbeded = pEthnicGroups}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of EthnicGroups) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of EthnicGroups) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _EthnicGroupsRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
