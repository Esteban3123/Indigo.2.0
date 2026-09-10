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

Public Class SportPracticeAdminService
    Implements ISportPracticeAdminService

#Region "Variables"

    ''' <summary>
    ''' Repositorio de Practicas Deportivas
    ''' </summary>
    Private _SportPracticeRepository As ISportPracticeRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IPayrollSequenceDetailRepository

#End Region

    ''' <summary>
    ''' inicia el repositorio de Practicas Deportivas
    ''' </summary>
    ''' <param name="sportPracticeRepository">Repositorio de Practicas Deportivas</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal sportPracticeRepository As ISportPracticeRepository, ByVal secuenseDRepository As IPayrollSequenceDetailRepository)
        If (sportPracticeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Practicas Deportivas vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _SportPracticeRepository = sportPracticeRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of SportPractice) Implements ISportPracticeAdminService.ChangeState
        Dim sportPractice As SportPractice = _SportPracticeRepository.GetSportPractice(code, True)
        sportPractice.State = state
        Return SaveSportPractice(sportPractice, audit)
    End Function

    ''' <summary>
    ''' Elimina un deporte practicado
    ''' </summary>
    ''' <param name="sportPractice"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSportPractice(sportPractice As SportPractice, audit As AuditMessage) As ActionResult Implements ISportPracticeAdminService.DeleteSportPractice
        If sportPractice Is Nothing Then
            Throw New ArgumentNullException("sportPractice")
        End If
        Dim unitOfWork As IUnitWork = Me._SportPracticeRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of SportPractice)
            auditProcess = New IndigoAuditSimpleEntity(Of SportPractice)(sportPractice, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._SportPracticeRepository.DeleteEntity(sportPractice)
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
    ''' Obtiene una practica deportiva por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSportPractice(code As String, tracking As Boolean, audit As AuditMessage) As ActionResult(Of SportPractice) Implements ISportPracticeAdminService.GetSportPractice
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim sportPractice As SportPractice = Me._SportPracticeRepository.GetSportPractice(code.Trim(), tracking)
            If sportPractice IsNot Nothing AndAlso sportPractice.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SportPractice)(sportPractice, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SportPractice) With {.StateResult = True, .ObjectEmbbeded = sportPractice}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SportPractice) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    '''  Obtiene una practica deportiva por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSportPracticeById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of SportPractice) Implements ISportPracticeAdminService.GetSportPracticeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim sportPractice As SportPractice = Me._SportPracticeRepository.GetSportPracticeById(id, tracking)
            If sportPractice IsNot Nothing AndAlso sportPractice.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SportPractice)(sportPractice, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SportPractice) With {.StateResult = True, .ObjectEmbbeded = sportPractice}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SportPractice) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function


    ''' <summary>
    ''' Guarda o actualiza un deporte practicado
    ''' </summary>
    ''' <param name="sportPractice"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSportPractice(sportPractice As SportPractice, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of SportPractice) Implements ISportPracticeAdminService.SaveSportPractice
        If sportPractice Is Nothing Then
            Throw New ArgumentNullException("sportPractice")
        End If
        Dim unitOfWork As IUnitWork = Me._SportPracticeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As PayrollSequenceDetail = Nothing
            If sportPractice.Code Is Nothing OrElse sportPractice.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        sportPractice.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of SportPractice) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of SportPractice) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxSportPractice As SportPractice = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SportPractice)
            Dim status As Integer

            If sportPractice.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                sportPractice.CreationUser = audit.CodeUser
                sportPractice.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSportPractice = _SportPracticeRepository.GetSportPractice(sportPractice.Code, False)
                sportPractice.ModificationUser = audit.CodeUser
                sportPractice.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._SportPracticeRepository.SaveEntity(sportPractice)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SportPractice)(sportPractice, audit, status, auxSportPractice)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            sportPractice.MarkAsUnchanged()

            Return New ActionResult(Of SportPractice) With {.StateResult = True, .ObjectEmbbeded = sportPractice}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SportPractice) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SportPractice) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _SportPracticeRepository = Nothing
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
