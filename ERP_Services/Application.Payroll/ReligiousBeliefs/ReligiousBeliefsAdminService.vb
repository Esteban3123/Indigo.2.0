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

Public Class ReligiousBeliefsAdminService
    Implements IReligiousBeliefsAdminService

    Private _ReligiousBeliefsRepository As IReligiousBeliefsRepository

    Private _secuenseDRepository As IPayrollSequenceDetailRepository

    Public Sub New(ByVal religiousBeliefsRepository As IReligiousBeliefsRepository, ByVal secuenseDRepository As IPayrollSequenceDetailRepository)
        If (religiousBeliefsRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Actividades de Tiempo Libre vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _ReligiousBeliefsRepository = religiousBeliefsRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

    Public Function ChangeState(pCode As String, pStatus As Boolean, pAudit As AuditMessage) As ActionResult(Of ReligiousBeliefs) Implements IReligiousBeliefsAdminService.ChangeState
        Dim religiousBeliefs As ReligiousBeliefs = _ReligiousBeliefsRepository.GetReligiousBeliefs(pCode, True)
        religiousBeliefs.Status = pStatus
        Return SaveReligiousBeliefs(religiousBeliefs, pAudit)
    End Function

    Public Function DeleteReligiousBeliefs(pReligiousBeliefs As ReligiousBeliefs, audit As AuditMessage) As ActionResult Implements IReligiousBeliefsAdminService.DeleteReligiousBeliefs
        If pReligiousBeliefs Is Nothing Then
            Throw New ArgumentNullException("pReligiousBeliefs")
        End If
        Dim unitOfWork As IUnitWork = Me._ReligiousBeliefsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ReligiousBeliefs)
            auditProcess = New IndigoAuditSimpleEntity(Of ReligiousBeliefs)(pReligiousBeliefs, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._ReligiousBeliefsRepository.DeleteEntity(pReligiousBeliefs)
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

    Public Function GetReligiousBeliefs(pCode As String, pTracking As Boolean, pAudit As AuditMessage) As ActionResult(Of ReligiousBeliefs) Implements IReligiousBeliefsAdminService.GetReligiousBeliefs
        If String.IsNullOrEmpty(pCode) Then
            Throw New ArgumentNullException("pCode")
        End If
        If pAudit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim religiousBeliefs As ReligiousBeliefs = Me._ReligiousBeliefsRepository.GetReligiousBeliefs(pCode.Trim(), pTracking)
            If religiousBeliefs IsNot Nothing AndAlso religiousBeliefs.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ReligiousBeliefs)(religiousBeliefs, pAudit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = True, .ObjectEmbbeded = religiousBeliefs}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetReligiousBeliefsByID(pID As Integer, pTracking As Boolean, pAudit As AuditMessage) As ActionResult(Of ReligiousBeliefs) Implements IReligiousBeliefsAdminService.GetReligiousBeliefsByID
        If pID = 0 Then
            Throw New ArgumentNullException("pID")
        End If
        If pAudit Is Nothing Then
            Throw New ArgumentNullException("pAudit")
        End If
        Try
            Dim religiousBeliefs As ReligiousBeliefs = Me._ReligiousBeliefsRepository.GetReligiousBeliefsByID(pID, pTracking)
            If religiousBeliefs IsNot Nothing AndAlso religiousBeliefs.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ReligiousBeliefs)(religiousBeliefs, pAudit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = True, .ObjectEmbbeded = religiousBeliefs}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function SaveReligiousBeliefs(pReligiousBeliefs As ReligiousBeliefs, pAudit As AuditMessage, Optional pIDSequence As Long = 0) As ActionResult(Of ReligiousBeliefs) Implements IReligiousBeliefsAdminService.SaveReligiousBeliefs
        If pReligiousBeliefs Is Nothing Then
            Throw New ArgumentNullException("pReligiousBeliefs")
        End If
        Dim unitOfWork As IUnitWork = Me._ReligiousBeliefsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PayrollSequenceDetail = Nothing
            If pReligiousBeliefs.Code Is Nothing OrElse pReligiousBeliefs.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(pIDSequence)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        pReligiousBeliefs.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxReligiousBeliefs As ReligiousBeliefs = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of ReligiousBeliefs)
            Dim status As Integer

            If pReligiousBeliefs.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                pReligiousBeliefs.CreationUser = pAudit.CodeUser
                pReligiousBeliefs.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxReligiousBeliefs = _ReligiousBeliefsRepository.GetReligiousBeliefs(pReligiousBeliefs.Code, False)
                pReligiousBeliefs.ModificationUser = pAudit.CodeUser
                pReligiousBeliefs.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._ReligiousBeliefsRepository.SaveEntity(pReligiousBeliefs)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ReligiousBeliefs)(pReligiousBeliefs, pAudit, status, auxReligiousBeliefs)
            auditProcess.Execute()

            pReligiousBeliefs.MarkAsUnchanged()

            Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = True, .ObjectEmbbeded = pReligiousBeliefs}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ReligiousBeliefs) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ReligiousBeliefsRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
