'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 16/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core

Public Class ContractMinimumWageAdminService
    Implements IContractMinimumWageAdminService

    Dim _contractMinimumWageRepository As IContractMinimumWageRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

    Public Sub New(contractMinimumWageRepository As IContractMinimumWageRepository, secuenseDRepository As ISequenseContractDRepository)
        If contractMinimumWageRepository Is Nothing Then
            Throw New ArgumentNullException("contractMinimumWageRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _contractMinimumWageRepository = contractMinimumWageRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateContractMinimumWage(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ContractMinimumWage) Implements IContractMinimumWageAdminService.ChangeStateContractMinimumWage
        Dim ContractMinimumWage As ContractMinimumWage = _contractMinimumWageRepository.GetContractMinimumWage(code)
        ContractMinimumWage.Status = state
        ContractMinimumWage.MarkAsModified()
        Return SaveContractMinimumWage(ContractMinimumWage, audit)
    End Function

    ''' <summary>
    ''' Elimina un salario
    ''' </summary>
    ''' <param name="ContractMinimumWage"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ContractMinimumWage</exception>
    Public Function DeleteContractMinimumWage(ContractMinimumWage As ContractMinimumWage, audit As AuditMessage) As ActionResult Implements IContractMinimumWageAdminService.DeleteContractMinimumWage
        If ContractMinimumWage Is Nothing Then
            Throw New ArgumentNullException("ContractMinimumWage")
        End If
        Dim unitOfWork As IUnitWork = Me._contractMinimumWageRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractMinimumWage)
            auditProcess = New IndigoAuditSimpleEntity(Of ContractMinimumWage)(ContractMinimumWage, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._contractMinimumWageRepository.SaveEntity(ContractMinimumWage)
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
    ''' Obtiene un salario por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetContractMinimumWage(code As String, audit As AuditMessage) As ContractMinimumWage Implements IContractMinimumWageAdminService.GetContractMinimumWage
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractMinimumWage As ContractMinimumWage = Me._contractMinimumWageRepository.GetContractMinimumWage(code.Trim())
            If ContractMinimumWage IsNot Nothing AndAlso ContractMinimumWage.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractMinimumWage)(ContractMinimumWage, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return ContractMinimumWage
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ContractMinimumWage
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un salario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">id</exception>
    Public Function GetContractMinimumWageById(id As Integer) As ContractMinimumWage Implements IContractMinimumWageAdminService.GetContractMinimumWageById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return Me._contractMinimumWageRepository.GetContractMinimumWageById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ContractMinimumWage
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un salario
    ''' </summary>
    ''' <param name="ContractMinimumWage"></param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ContractMinimumWage</exception>
    Public Function SaveContractMinimumWage(ContractMinimumWage As ContractMinimumWage, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ContractMinimumWage) Implements IContractMinimumWageAdminService.SaveContractMinimumWage
        If ContractMinimumWage Is Nothing Then
            Throw New ArgumentNullException("ContractMinimumWage")
        End If
        Dim unitOfWork As IUnitWork = Me._contractMinimumWageRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If ContractMinimumWage.Code Is Nothing OrElse ContractMinimumWage.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        ContractMinimumWage.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of ContractMinimumWage) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of ContractMinimumWage) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxContractMinimumWage As ContractMinimumWage = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractMinimumWage)
            Dim status As Integer

            If ContractMinimumWage.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                ContractMinimumWage.CreationUser = audit.CodeUser
                ContractMinimumWage.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxContractMinimumWage = ContractMinimumWage.OriginalValue
                ContractMinimumWage.ModificationUser = audit.CodeUser
                ContractMinimumWage.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._contractMinimumWageRepository.SaveEntity(ContractMinimumWage)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContractMinimumWage)(ContractMinimumWage, audit, status, auxContractMinimumWage)
            auditProcess.Execute()

            Return New ActionResult(Of ContractMinimumWage) With {.StateResult = True, .ObjectEmbbeded = ContractMinimumWage}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ContractMinimumWage) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractMinimumWage) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _contractMinimumWageRepository = Nothing
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
