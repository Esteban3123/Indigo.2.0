'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2019
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
Imports Application.Contract

Public Class ContractDescriptionsAdminService
    Implements IContractDescriptionsAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractDescriptionsRepository As IContractDescriptionsRepository

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
    Public Sub New(ByVal contractDescriptionsRepository As IContractDescriptionsRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If contractDescriptionsRepository Is Nothing Then
            Throw New ArgumentNullException("contractDescriptionsRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _contractDescriptionsRepository = contractDescriptionsRepository
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
    Public Function ChangeStateContractDescriptions(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ContractDescriptions) Implements IContractDescriptionsAdminService.ChangeStateContractDescriptions
        Dim ContractDescriptions As ContractDescriptions = _contractDescriptionsRepository.GetContractDescriptions(code)
        ContractDescriptions.Status = state
        Return SaveContractDescriptions(ContractDescriptions, audit)
    End Function

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteContractDescriptions(ContractDescriptions As ContractDescriptions, audit As AuditMessage) As ActionResult Implements IContractDescriptionsAdminService.DeleteContractDescriptions
        If ContractDescriptions Is Nothing Then
            Throw New ArgumentNullException("ContractDescriptions")
        End If
        Dim unitOfWork As IUnitWork = Me._contractDescriptionsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractDescriptions)
            auditProcess = New IndigoAuditSimpleEntity(Of ContractDescriptions)(ContractDescriptions, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._contractDescriptionsRepository.DeleteEntity(ContractDescriptions)
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
    Public Function GetContractDescriptions(code As String, audit As AuditMessage) As ActionResult(Of ContractDescriptions) Implements IContractDescriptionsAdminService.GetContractDescriptions
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractDescriptions As ContractDescriptions = Me._contractDescriptionsRepository.GetContractDescriptions(code.Trim())
            If ContractDescriptions IsNot Nothing AndAlso ContractDescriptions.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractDescriptions)(ContractDescriptions, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractDescriptions) With {.StateResult = True, .ObjectEmbbeded = ContractDescriptions}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractDescriptions) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractDescriptionsById(id As Integer, audit As AuditMessage) As ActionResult(Of ContractDescriptions) Implements IContractDescriptionsAdminService.GetContractDescriptionsById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractDescriptions As ContractDescriptions = Me._contractDescriptionsRepository.GetContractDescriptionsById(id)
            If ContractDescriptions IsNot Nothing AndAlso ContractDescriptions.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractDescriptions)(ContractDescriptions, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractDescriptions) With {.StateResult = True, .ObjectEmbbeded = ContractDescriptions}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractDescriptions) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un grupo
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveContractDescriptions(ContractDescriptions As ContractDescriptions, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ContractDescriptions) Implements IContractDescriptionsAdminService.SaveContractDescriptions
        If ContractDescriptions Is Nothing Then
            Throw New ArgumentNullException("ContractDescriptions")
        End If
        Dim unitOfWork As IUnitWork = Me._contractDescriptionsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If ContractDescriptions.Code Is Nothing OrElse ContractDescriptions.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        ContractDescriptions.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of ContractDescriptions) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of ContractDescriptions) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxContractDescriptions As ContractDescriptions = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractDescriptions)
            Dim status As Integer

            If ContractDescriptions.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                ContractDescriptions.CreationUser = audit.CodeUser
                ContractDescriptions.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxContractDescriptions = ContractDescriptions.OriginalValue
                ContractDescriptions.ModificationUser = audit.CodeUser
                ContractDescriptions.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._contractDescriptionsRepository.SaveEntity(ContractDescriptions)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContractDescriptions)(ContractDescriptions, audit, status, auxContractDescriptions)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            ContractDescriptions.MarkAsUnchanged()

            Return New ActionResult(Of ContractDescriptions) With {.StateResult = True, .ObjectEmbbeded = ContractDescriptions}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ContractDescriptions) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractDescriptions) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _contractDescriptionsRepository = Nothing
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
