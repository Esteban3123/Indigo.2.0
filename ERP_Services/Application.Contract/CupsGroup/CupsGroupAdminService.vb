'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
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

Public Class CupsGroupAdminService
    Implements ICupsGroupAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsGroupRepository As ICupsGroupRepository
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
    Public Sub New(ByVal cupsGroupRepository As ICupsGroupRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If cupsGroupRepository Is Nothing Then
            Throw New ArgumentNullException("cupsGroupRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _cupsGroupRepository = cupsGroupRepository
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
    Public Function ChangeStateCupsGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CupsGroup) Implements ICupsGroupAdminService.ChangeStateCupsGroup
        Dim CupsGroup As CupsGroup = _cupsGroupRepository.GetCupsGroup(code)
        CupsGroup.Status = state
        Return SaveCupsGroup(CupsGroup, audit)
    End Function

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="CupsGroup"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteCupsGroup(CupsGroup As CupsGroup, audit As AuditMessage) As ActionResult Implements ICupsGroupAdminService.DeleteCupsGroup
        If CupsGroup Is Nothing Then
            Throw New ArgumentNullException("CupsGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._cupsGroupRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CupsGroup)
            auditProcess = New IndigoAuditSimpleEntity(Of CupsGroup)(CupsGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._cupsGroupRepository.DeleteEntity(CupsGroup)
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
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsGroup(code As String, audit As AuditMessage) As ActionResult(Of CupsGroup) Implements ICupsGroupAdminService.GetCupsGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CupsGroup As CupsGroup = Me._cupsGroupRepository.GetCupsGroup(code.Trim())
            If CupsGroup IsNot Nothing AndAlso CupsGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CupsGroup)(CupsGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CupsGroup) With {.StateResult = True, .ObjectEmbbeded = CupsGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CupsGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsGroupById(id As Integer, audit As AuditMessage) As ActionResult(Of CupsGroup) Implements ICupsGroupAdminService.GetCupsGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CupsGroup As CupsGroup = Me._cupsGroupRepository.GetCupsGroupById(id)
            If CupsGroup IsNot Nothing AndAlso CupsGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CupsGroup)(CupsGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CupsGroup) With {.StateResult = True, .ObjectEmbbeded = CupsGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CupsGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un grupo
    ''' </summary>
    ''' <param name="CupsGroup"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCupsGroup(CupsGroup As CupsGroup, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CupsGroup) Implements ICupsGroupAdminService.SaveCupsGroup
        If CupsGroup Is Nothing Then
            Throw New ArgumentNullException("CupsGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._cupsGroupRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim auxCupsGroup As CupsGroup = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of CupsGroup)
            Dim status As Integer

            If CupsGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                CupsGroup.CreationUser = audit.CodeUser
                CupsGroup.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxCupsGroup = CupsGroup.OriginalValue
                CupsGroup.ModificationUser = audit.CodeUser
                CupsGroup.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._cupsGroupRepository.SaveEntity(CupsGroup)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of CupsGroup)(CupsGroup, audit, status, auxCupsGroup)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            CupsGroup.MarkAsUnchanged()

            Return New ActionResult(Of CupsGroup) With {.StateResult = True, .ObjectEmbbeded = CupsGroup}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CupsGroup) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CupsGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _cupsGroupRepository = Nothing
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
