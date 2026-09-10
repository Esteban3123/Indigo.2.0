'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/10/2014
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

Public Class SurgicalGroupAdminService
    Implements ISurgicalGroupAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _surgicalGroupRepository As ISurgicalGroupRepository
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
    Public Sub New(ByVal surgicalGroupRepository As ISurgicalGroupRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If surgicalGroupRepository Is Nothing Then
            Throw New ArgumentNullException("surgicalGroupRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _surgicalGroupRepository = surgicalGroupRepository
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
    Public Function ChangeStateSurgicalGroup(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of SurgicalGroup) Implements ISurgicalGroupAdminService.ChangeStateSurgicalGroup
        Dim SurgicalGroup As SurgicalGroup = _surgicalGroupRepository.GetSurgicalGroup(code)
        SurgicalGroup.Status = state
        Return SaveSurgicalGroup(SurgicalGroup, audit)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="SurgicalGroup"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSurgicalGroup(SurgicalGroup As SurgicalGroup, audit As AuditMessage) As ActionResult Implements ISurgicalGroupAdminService.DeleteSurgicalGroup
        If SurgicalGroup Is Nothing Then
            Throw New ArgumentNullException("SurgicalGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._surgicalGroupRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of SurgicalGroup)
            auditProcess = New IndigoAuditSimpleEntity(Of SurgicalGroup)(SurgicalGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._surgicalGroupRepository.DeleteEntity(SurgicalGroup)
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
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSurgicalGroup(code As String, audit As AuditMessage) As ActionResult(Of SurgicalGroup) Implements ISurgicalGroupAdminService.GetSurgicalGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim SurgicalGroup As SurgicalGroup = Me._surgicalGroupRepository.GetSurgicalGroup(code.Trim())
            If SurgicalGroup IsNot Nothing AndAlso SurgicalGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SurgicalGroup)(SurgicalGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SurgicalGroup) With {.StateResult = True, .ObjectEmbbeded = SurgicalGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SurgicalGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSurgicalGroupById(id As Integer, audit As AuditMessage) As ActionResult(Of SurgicalGroup) Implements ISurgicalGroupAdminService.GetSurgicalGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim SurgicalGroup As SurgicalGroup = Me._surgicalGroupRepository.GetSurgicalGroupById(id)
            If SurgicalGroup IsNot Nothing AndAlso SurgicalGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SurgicalGroup)(SurgicalGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SurgicalGroup) With {.StateResult = True, .ObjectEmbbeded = SurgicalGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SurgicalGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="SurgicalGroup"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSurgicalGroup(SurgicalGroup As SurgicalGroup, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of SurgicalGroup) Implements ISurgicalGroupAdminService.SaveSurgicalGroup
        If SurgicalGroup Is Nothing Then
            Throw New ArgumentNullException("SurgicalGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._surgicalGroupRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If SurgicalGroup.Code Is Nothing OrElse SurgicalGroup.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        SurgicalGroup.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of SurgicalGroup) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of SurgicalGroup) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxSurgicalGroup As SurgicalGroup = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SurgicalGroup)
            Dim status As Integer

            If SurgicalGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                SurgicalGroup.CreationUser = audit.CodeUser
                SurgicalGroup.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSurgicalGroup = SurgicalGroup.OriginalValue
                SurgicalGroup.ModificationUser = audit.CodeUser
                SurgicalGroup.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._surgicalGroupRepository.SaveEntity(SurgicalGroup)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SurgicalGroup)(SurgicalGroup, audit, status, auxSurgicalGroup)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            SurgicalGroup.MarkAsUnchanged()

            Return New ActionResult(Of SurgicalGroup) With {.StateResult = True, .ObjectEmbbeded = SurgicalGroup}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SurgicalGroup) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SurgicalGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _surgicalGroupRepository = Nothing
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
