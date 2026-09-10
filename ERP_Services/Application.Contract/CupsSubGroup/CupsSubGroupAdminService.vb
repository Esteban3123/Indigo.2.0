Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core

Public Class CupsSubGroupAdminService
    Implements ICupsSubGroupAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _cupsSubGroupRepository As ICupsSubGroupRepository
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
    Public Sub New(ByVal cupsSubGroupRepository As ICupsSubGroupRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If cupsSubGroupRepository Is Nothing Then
            Throw New ArgumentNullException("cupsSubGroupRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _cupsSubGroupRepository = cupsSubGroupRepository
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
    Public Function ChangeStateCupsSubGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CupsSubgroup) Implements ICupsSubGroupAdminService.ChangeStateCupsSubGroup
        Dim CupsSubgroup As CupsSubgroup = _cupsSubGroupRepository.GetCupsSubgroup(code)
        CupsSubgroup.Status = state
        Return SaveCupsSubGroup(CupsSubgroup, audit)
    End Function

    ''' <summary>
    ''' Elimina un subgrupo
    ''' </summary>
    ''' <param name="CupsSubGroup"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteCupsSubGroup(CupsSubGroup As CupsSubgroup, audit As AuditMessage) As ActionResult Implements ICupsSubGroupAdminService.DeleteCupsSubGroup
        If CupsSubGroup Is Nothing Then
            Throw New ArgumentNullException("CupsSubGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._cupsSubGroupRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CupsSubgroup)
            auditProcess = New IndigoAuditSimpleEntity(Of CupsSubgroup)(CupsSubGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._cupsSubGroupRepository.DeleteEntity(CupsSubGroup)
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
    ''' Obtiene un subgrupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsSubGroup(code As String, audit As AuditMessage) As ActionResult(Of CupsSubgroup) Implements ICupsSubGroupAdminService.GetCupsSubGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CupsSubgroup As CupsSubgroup = Me._cupsSubGroupRepository.GetCupsSubgroup(code.Trim())
            If CupsSubgroup IsNot Nothing AndAlso CupsSubgroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CupsSubgroup)(CupsSubgroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CupsSubgroup) With {.StateResult = True, .ObjectEmbbeded = CupsSubgroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CupsSubgroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un subgrupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsSubGroupById(id As Integer, audit As AuditMessage) As ActionResult(Of CupsSubgroup) Implements ICupsSubGroupAdminService.GetCupsSubGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CupsSubgroup As CupsSubgroup = Me._cupsSubGroupRepository.GetCupsSubgroupById(id)
            If CupsSubgroup IsNot Nothing AndAlso CupsSubgroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CupsSubgroup)(CupsSubgroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CupsSubgroup) With {.StateResult = True, .ObjectEmbbeded = CupsSubgroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CupsSubgroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un subgrupo
    ''' </summary>
    ''' <param name="CupsSubGroup"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCupsSubGroup(CupsSubGroup As CupsSubgroup, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CupsSubgroup) Implements ICupsSubGroupAdminService.SaveCupsSubGroup
        If CupsSubGroup Is Nothing Then
            Throw New ArgumentNullException("CupsSubGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._cupsSubGroupRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim auxCupsSubGroup As CupsSubgroup = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of CupsSubgroup)
            Dim status As Integer

            If CupsSubGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                CupsSubGroup.CreationUser = audit.CodeUser
                CupsSubGroup.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxCupsSubGroup = CupsSubGroup.OriginalValue
                CupsSubGroup.ModificationUser = audit.CodeUser
                CupsSubGroup.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._cupsSubGroupRepository.SaveEntity(CupsSubGroup)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of CupsSubgroup)(CupsSubGroup, audit, status, auxCupsSubGroup)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            CupsSubGroup.MarkAsUnchanged()

            Return New ActionResult(Of CupsSubgroup) With {.StateResult = True, .ObjectEmbbeded = CupsSubGroup}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CupsSubgroup) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CupsSubgroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _cupsSubGroupRepository = Nothing
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
