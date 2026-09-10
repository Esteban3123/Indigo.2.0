'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
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

Public Class UVRRangeAdminService
    Implements IUVRRangeAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _uvrRangeRepository As IUVRRangeRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal uvrRangeRepository As IUVRRangeRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If uvrRangeRepository Is Nothing Then
            Throw New ArgumentNullException("uvrRangeRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _uvrRangeRepository = uvrRangeRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateUVRRange(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of UVRRange) Implements IUVRRangeAdminService.ChangeStateUVRRange
        Dim UVRRange As UVRRange = _uvrRangeRepository.GetUVRRange(code)
        UVRRange.Status = state
        Return SaveUVRRange(UVRRange, audit)
    End Function

    ''' <summary>
    ''' Elimina un rango uvr
    ''' </summary>
    ''' <param name="UVRRange"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteUVRRange(UVRRange As UVRRange, audit As AuditMessage) As ActionResult Implements IUVRRangeAdminService.DeleteUVRRange
        If UVRRange Is Nothing Then
            Throw New ArgumentNullException("UVRRange")
        End If
        Dim unitOfWork As IUnitWork = Me._uvrRangeRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of UVRRange)
            auditProcess = New IndigoAuditSimpleEntity(Of UVRRange)(UVRRange, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._uvrRangeRepository.DeleteEntity(UVRRange)
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
    ''' Obtiene un rango uvr por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUVRRange(code As String, audit As AuditMessage) As ActionResult(Of UVRRange) Implements IUVRRangeAdminService.GetUVRRange
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim UVRRange As UVRRange = Me._uvrRangeRepository.GetUVRRange(code.Trim())
            If UVRRange IsNot Nothing AndAlso UVRRange.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of UVRRange)(UVRRange, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of UVRRange) With {.StateResult = True, .ObjectEmbbeded = UVRRange}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of UVRRange) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un rango uvr por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUVRRangeById(id As Integer, audit As AuditMessage) As ActionResult(Of UVRRange) Implements IUVRRangeAdminService.GetUVRRangeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim UVRRange As UVRRange = Me._uvrRangeRepository.GetUVRRangeById(id)
            If UVRRange IsNot Nothing AndAlso UVRRange.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of UVRRange)(UVRRange, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of UVRRange) With {.StateResult = True, .ObjectEmbbeded = UVRRange}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of UVRRange) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un rango uvr
    ''' </summary>
    ''' <param name="UVRRange"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveUVRRange(UVRRange As UVRRange, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of UVRRange) Implements IUVRRangeAdminService.SaveUVRRange
        If UVRRange Is Nothing Then
            Throw New ArgumentNullException("UVRRange")
        End If
        Dim unitOfWork As IUnitWork = Me._uvrRangeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If UVRRange.Code Is Nothing OrElse UVRRange.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        UVRRange.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of UVRRange) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of UVRRange) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxUVRRange As UVRRange = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of UVRRange)
            Dim status As Integer

            If UVRRange.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                UVRRange.CreationUser = audit.CodeUser
                UVRRange.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxUVRRange = UVRRange.OriginalValue
                UVRRange.ModificationUser = audit.CodeUser
                UVRRange.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._uvrRangeRepository.SaveEntity(UVRRange)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of UVRRange)(UVRRange, audit, status, auxUVRRange)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            UVRRange.MarkAsUnchanged()

            Return New ActionResult(Of UVRRange) With {.StateResult = True, .ObjectEmbbeded = UVRRange}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of UVRRange) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of UVRRange) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _uvrRangeRepository = Nothing
            _secuenseDRepository = Nothing
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
