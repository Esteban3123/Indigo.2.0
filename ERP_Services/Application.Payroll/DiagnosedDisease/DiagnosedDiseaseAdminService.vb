'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-08-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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

Public Class DiagnosedDiseaseAdminService
    Implements IDiagnosedDiseaseAdminService

#Region "Variables"

    ''' <summary>
    ''' Repositorio de Enfermedades Diagnosticadas
    ''' </summary>
    Private _DiagnosedDiseaseRepository As IDiagnosedDiseaseRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IPayrollSequenceDetailRepository

#End Region

    ''' <summary>
    ''' inicia el repositorio de la Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDiseaseRepository">Repositorio de Enfermedades Diagnosticadas</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal diagnosedDiseaseRepository As IDiagnosedDiseaseRepository, ByVal secuenseDRepository As IPayrollSequenceDetailRepository)
        If (diagnosedDiseaseRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Enfermedades Diagnosticadas vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _DiagnosedDiseaseRepository = diagnosedDiseaseRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DiagnosedDisease) Implements IDiagnosedDiseaseAdminService.ChangeState
        Dim diagnosedDisease As DiagnosedDisease = _DiagnosedDiseaseRepository.GetDiagnosedDisease(code, True)
        diagnosedDisease.State = state
        Return SaveDiagnosedDisease(diagnosedDisease, audit)
    End Function

    ''' <summary>
    ''' Elimina una enfermedad diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDisease"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDiagnosedDisease(diagnosedDisease As DiagnosedDisease, audit As AuditMessage) As ActionResult Implements IDiagnosedDiseaseAdminService.DeleteDiagnosedDisease
        If diagnosedDisease Is Nothing Then
            Throw New ArgumentNullException("diagnosedDisease")
        End If
        Dim unitOfWork As IUnitWork = Me._DiagnosedDiseaseRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of DiagnosedDisease)
            auditProcess = New IndigoAuditSimpleEntity(Of DiagnosedDisease)(diagnosedDisease, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._DiagnosedDiseaseRepository.DeleteEntity(diagnosedDisease)
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
    ''' Obtiene una enfermedad diagnosticada por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDiagnosedDisease(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of DiagnosedDisease) Implements IDiagnosedDiseaseAdminService.GetDiagnosedDisease
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim diagnosedDisease As DiagnosedDisease = Me._DiagnosedDiseaseRepository.GetDiagnosedDisease(code.Trim(), tracking)
            If diagnosedDisease IsNot Nothing AndAlso diagnosedDisease.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DiagnosedDisease)(diagnosedDisease, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DiagnosedDisease) With {.StateResult = True, .ObjectEmbbeded = diagnosedDisease}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DiagnosedDisease) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    '''  Obtiene una enfermedad diagnosticada por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDiagnosedDiseaseById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of DiagnosedDisease) Implements IDiagnosedDiseaseAdminService.GetDiagnosedDiseaseById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim diagnosedDisease As DiagnosedDisease = Me._DiagnosedDiseaseRepository.GetDiagnosedDiseaseById(id, tracking)
            If diagnosedDisease IsNot Nothing AndAlso diagnosedDisease.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DiagnosedDisease)(diagnosedDisease, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DiagnosedDisease) With {.StateResult = True, .ObjectEmbbeded = diagnosedDisease}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DiagnosedDisease) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function


    ''' <summary>
    ''' Guarda o actualiza una enfermedad diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDisease"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDiagnosedDisease(diagnosedDisease As DiagnosedDisease, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DiagnosedDisease) Implements IDiagnosedDiseaseAdminService.SaveDiagnosedDisease
        If diagnosedDisease Is Nothing Then
            Throw New ArgumentNullException("diagnosedDisease")
        End If
        Dim unitOfWork As IUnitWork = Me._DiagnosedDiseaseRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PayrollSequenceDetail = Nothing
            If diagnosedDisease.Code Is Nothing OrElse diagnosedDisease.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        diagnosedDisease.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of DiagnosedDisease) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of DiagnosedDisease) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxDiagnosedDisease As DiagnosedDisease = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of DiagnosedDisease)
            Dim status As Integer

            If diagnosedDisease.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                diagnosedDisease.CreationUser = audit.CodeUser
                diagnosedDisease.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxDiagnosedDisease = _DiagnosedDiseaseRepository.GetDiagnosedDisease(diagnosedDisease.Code, False)
                diagnosedDisease.ModificationUser = audit.CodeUser
                diagnosedDisease.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._DiagnosedDiseaseRepository.SaveEntity(diagnosedDisease)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of DiagnosedDisease)(diagnosedDisease, audit, status, auxDiagnosedDisease)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            diagnosedDisease.MarkAsUnchanged()

            Return New ActionResult(Of DiagnosedDisease) With {.StateResult = True, .ObjectEmbbeded = diagnosedDisease}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DiagnosedDisease) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DiagnosedDisease) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _DiagnosedDiseaseRepository = Nothing
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
