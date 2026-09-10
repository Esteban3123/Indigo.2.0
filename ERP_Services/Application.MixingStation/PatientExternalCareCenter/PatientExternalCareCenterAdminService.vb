'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/07/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.Security
Imports Application.MixingStation
Imports System.Text

Public Class PatientExternalCareCenterAdminService
    Implements IPatientExternalCareCenterAdminService, Inject

    Private _patientExternalCareCenterRepository As IPatientExternalCareCenterRepository

    Public Sub New(patientExternalCareCenterRepository As IPatientExternalCareCenterRepository)
        If patientExternalCareCenterRepository Is Nothing Then
            Throw New ArgumentNullException("patientExternalCareCenterRepository")
        End If
        _patientExternalCareCenterRepository = patientExternalCareCenterRepository
    End Sub

    Public Function SavePatientExternalCareCenter(PatientExternalCareCenter As PatientExternalCareCenter, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PatientExternalCareCenter) Implements IPatientExternalCareCenterAdminService.SavePatientExternalCareCenter
        If PatientExternalCareCenter Is Nothing Then
            Throw New ArgumentNullException("PatientExternalCareCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._patientExternalCareCenterRepository.UnitWork
        Dim message As String = String.Empty
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim auxPatientExternalCareCenter As PatientExternalCareCenter = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PatientExternalCareCenter)
                Dim status As Integer

                If PatientExternalCareCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    PatientExternalCareCenter.CreationUser = audit.CodeUser
                    PatientExternalCareCenter.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxPatientExternalCareCenter = PatientExternalCareCenter.OriginalValue
                    PatientExternalCareCenter.ModificationUser = audit.CodeUser
                    PatientExternalCareCenter.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Dim Xml As String = ConvertToXml(PatientExternalCareCenter)
                Dim resultStore = Me._patientExternalCareCenterRepository.SP_SavePatientExternalCareCenter(Xml, audit.CodeUser)
                If resultStore Is Nothing OrElse resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    scope.Dispose()

                    If (resultStore IsNot Nothing) Then
                        message = resultStore.CodeMessage + vbCrLf
                    Else
                        message = "Ha ocurrido un error no identificado."
                    End If

                End If
                auditProcess = New IndigoAuditSimpleEntity(Of PatientExternalCareCenter)(PatientExternalCareCenter, audit, status, auxPatientExternalCareCenter)

                auditProcess.Execute()

                scope.Complete()
                Return New ActionResult(Of PatientExternalCareCenter) With {.StateResult = True, .ObjectEmbbeded = PatientExternalCareCenter}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PatientExternalCareCenter) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PatientExternalCareCenter) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Private Function ConvertToXml(PatientExternalCareCenter As PatientExternalCareCenter)
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Data>")
        builder.Append("<IdentificationNumber>" & PatientExternalCareCenter.IdentificationNumber & "</IdentificationNumber>")
        builder.Append("<IdentificationTypeId>" & PatientExternalCareCenter.IdentificationTypeId & "</IdentificationTypeId>")
        builder.Append("<PatientName>" & PatientExternalCareCenter.Name & "</PatientName>")
        builder.Append("<LastName>" & PatientExternalCareCenter.LastName & "</LastName>")
        builder.Append("<GenderTypeId>" & PatientExternalCareCenter.GenderTypeId & "</GenderTypeId>")
        builder.Append("<Status>" & PatientExternalCareCenter.Status & "</Status>")
        builder.Append("<CreationUser>" & PatientExternalCareCenter.CreationUser & "</CreationUser>")
        builder.Append("<CreationDate>" & PatientExternalCareCenter.CreationDate.Date & "</CreationDate>")
        builder.Append("</Data>")

        Return builder.ToString()
    End Function

    Public Function DeletePatientExternalCareCenter(PatientExternalCareCenter As PatientExternalCareCenter, audit As AuditMessage) As ActionResult Implements IPatientExternalCareCenterAdminService.DeletePatientExternalCareCenter
        If PatientExternalCareCenter Is Nothing Then
            Throw New ArgumentNullException("PatientExternalCareCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._patientExternalCareCenterRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of PatientExternalCareCenter)
            auditProcess = New IndigoAuditSimpleEntity(Of PatientExternalCareCenter)(PatientExternalCareCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            PatientExternalCareCenter.MarkAsDeleted()

            Me._patientExternalCareCenterRepository.SaveEntity(PatientExternalCareCenter)
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

    Public Function GetPatientExternalCareCenter(code As String, audit As AuditMessage) As PatientExternalCareCenter Implements IPatientExternalCareCenterAdminService.GetPatientExternalCareCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim PatientExternalCareCenter As PatientExternalCareCenter = Me._patientExternalCareCenterRepository.GetPatientExternalCareCenterByCode(code.Trim())
            If PatientExternalCareCenter IsNot Nothing AndAlso PatientExternalCareCenter.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PatientExternalCareCenter)(PatientExternalCareCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return PatientExternalCareCenter
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PatientExternalCareCenter
        End Try
    End Function

    Public Function GetPatientExternalCareCenterById(id As Integer) As PatientExternalCareCenter Implements IPatientExternalCareCenterAdminService.GetPatientExternalCareCenterById
        Try
            Return _patientExternalCareCenterRepository.GetPatientExternalCareCenterById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PatientExternalCareCenter
        End Try
    End Function

    Public Function ChangeStatePatientExternalCareCenter(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PatientExternalCareCenter) Implements IPatientExternalCareCenterAdminService.ChangeStatePatientExternalCareCenter
        Dim PatientExternalCareCenter As PatientExternalCareCenter = _patientExternalCareCenterRepository.GetPatientExternalCareCenterByCode(code)
        PatientExternalCareCenter.Status = state
        Return SavePatientExternalCareCenter(PatientExternalCareCenter, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _patientExternalCareCenterRepository = Nothing
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
