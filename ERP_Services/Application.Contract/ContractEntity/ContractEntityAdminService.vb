'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2014
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

Public Class ContractEntityAdminService
    Implements IContractEntityAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractEntityRepository As IContractEntityRepository
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
    Public Sub New(ByVal contractEntityRepository As IContractEntityRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If contractEntityRepository Is Nothing Then
            Throw New ArgumentNullException("contractEntityRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _contractEntityRepository = contractEntityRepository
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
    Public Function ChangeStateContractEntity(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ContractEntity) Implements IContractEntityAdminService.ChangeStateContractEntity
        Dim ContractEntity As ContractEntity = _contractEntityRepository.GetContractEntity(code)
        ContractEntity.Status = state
        Return SaveContractEntity(ContractEntity, audit)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="ContractEntity"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteContractEntity(ContractEntity As ContractEntity, audit As AuditMessage) As ActionResult Implements IContractEntityAdminService.DeleteContractEntity
        If ContractEntity Is Nothing Then
            Throw New ArgumentNullException("ContractEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._contractEntityRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractEntity)
            auditProcess = New IndigoAuditSimpleEntity(Of ContractEntity)(ContractEntity, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._contractEntityRepository.DeleteEntity(ContractEntity)
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
    Public Function GetContractEntity(code As String, audit As AuditMessage) As ActionResult(Of ContractEntity) Implements IContractEntityAdminService.GetContractEntity
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractEntity As ContractEntity = Me._contractEntityRepository.GetContractEntity(code.Trim())
            If ContractEntity IsNot Nothing AndAlso ContractEntity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractEntity)(ContractEntity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractEntity) With {.StateResult = True, .ObjectEmbbeded = ContractEntity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractEntity) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractEntityById(id As Integer, audit As AuditMessage) As ActionResult(Of ContractEntity) Implements IContractEntityAdminService.GetContractEntityById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractEntity As ContractEntity = Me._contractEntityRepository.GetContractEntityById(id)
            If ContractEntity IsNot Nothing AndAlso ContractEntity.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractEntity)(ContractEntity, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractEntity) With {.StateResult = True, .ObjectEmbbeded = ContractEntity}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractEntity) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="ContractEntity"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveContractEntity(ContractEntity As ContractEntity, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ContractEntity) Implements IContractEntityAdminService.SaveContractEntity
        If ContractEntity Is Nothing Then
            Throw New ArgumentNullException("ContractEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._contractEntityRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If ContractEntity.Code Is Nothing OrElse ContractEntity.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        ContractEntity.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of ContractEntity) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of ContractEntity) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxContractEntity As ContractEntity = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractEntity)
            Dim status As Integer

            If ContractEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                ContractEntity.CreationUser = audit.CodeUser
                ContractEntity.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxContractEntity = ContractEntity.OriginalValue
                ContractEntity.ModificationUser = audit.CodeUser
                ContractEntity.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._contractEntityRepository.SaveEntity(ContractEntity)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContractEntity)(ContractEntity, audit, status, auxContractEntity)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            ContractEntity.MarkAsUnchanged()

            Return New ActionResult(Of ContractEntity) With {.StateResult = True, .ObjectEmbbeded = ContractEntity}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ContractEntity) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractEntity) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _contractEntityRepository = Nothing
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
