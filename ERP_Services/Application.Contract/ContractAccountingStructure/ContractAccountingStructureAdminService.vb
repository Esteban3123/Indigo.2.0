'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2016
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

Public Class ContractAccountingStructureAdminService
    Implements IContractAccountingStructureAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractAccountingStructureRepository As IContractAccountingStructureRepository

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
    Public Sub New(ByVal contractAccountingStructureRepository As IContractAccountingStructureRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If contractAccountingStructureRepository Is Nothing Then
            Throw New ArgumentNullException("contractAccountingStructureRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _contractAccountingStructureRepository = contractAccountingStructureRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    Public Function ChangeStateContractAccountingStructure(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ContractAccountingStructure) Implements IContractAccountingStructureAdminService.ChangeStateContractAccountingStructure
        Dim ContractAccountingStructure As ContractAccountingStructure = _contractAccountingStructureRepository.GetContractAccountingStructure(code)
        ContractAccountingStructure.Status = state
        Return SaveContractAccountingStructure(ContractAccountingStructure, audit)
    End Function

    Public Function DeleteContractAccountingStructure(ContractAccountingStructure As ContractAccountingStructure, audit As AuditMessage) As ActionResult Implements IContractAccountingStructureAdminService.DeleteContractAccountingStructure
        If ContractAccountingStructure Is Nothing Then
            Throw New ArgumentNullException("ContractAccountingStructure")
        End If
        Dim unitOfWork As IUnitWork = Me._contractAccountingStructureRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractAccountingStructure)
            auditProcess = New IndigoAuditSimpleEntity(Of ContractAccountingStructure)(ContractAccountingStructure, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._contractAccountingStructureRepository.DeleteEntity(ContractAccountingStructure)
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

    Public Function GetActiveList(audit As AuditMessage) As ActionResult(Of List(Of ContractAccountingStructure)) Implements IContractAccountingStructureAdminService.GetActiveList
        Try
            Dim list = Me._contractAccountingStructureRepository.GetActiveList()
            Return New ActionResult(Of List(Of ContractAccountingStructure)) With {.StateResult = True, .ObjectEmbbeded = list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ContractAccountingStructure)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetListByCodes(codes As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of ContractAccountingStructure)) Implements IContractAccountingStructureAdminService.GetListByCodes
        Try
            Dim list = Me._contractAccountingStructureRepository.GetListByCodes(codes)
            Return New ActionResult(Of List(Of ContractAccountingStructure)) With {.StateResult = True, .ObjectEmbbeded = list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ContractAccountingStructure)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetContractAccountingStructure(code As String, audit As AuditMessage) As ActionResult(Of ContractAccountingStructure) Implements IContractAccountingStructureAdminService.GetContractAccountingStructure
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractAccountingStructure As ContractAccountingStructure = Me._contractAccountingStructureRepository.GetContractAccountingStructure(code.Trim())
            If ContractAccountingStructure IsNot Nothing AndAlso ContractAccountingStructure.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractAccountingStructure)(ContractAccountingStructure, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = True, .ObjectEmbbeded = ContractAccountingStructure}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetContractAccountingStructureById(id As Integer, audit As AuditMessage) As ActionResult(Of ContractAccountingStructure) Implements IContractAccountingStructureAdminService.GetContractAccountingStructureById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractAccountingStructure As ContractAccountingStructure = Me._contractAccountingStructureRepository.GetContractAccountingStructureById(id)
            If ContractAccountingStructure IsNot Nothing AndAlso ContractAccountingStructure.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractAccountingStructure)(ContractAccountingStructure, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = True, .ObjectEmbbeded = ContractAccountingStructure}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function SaveContractAccountingStructure(ContractAccountingStructure As ContractAccountingStructure, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ContractAccountingStructure) Implements IContractAccountingStructureAdminService.SaveContractAccountingStructure
        If ContractAccountingStructure Is Nothing Then
            Throw New ArgumentNullException("ContractAccountingStructure")
        End If
        Dim unitOfWork As IUnitWork = Me._contractAccountingStructureRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If ContractAccountingStructure.Code Is Nothing OrElse ContractAccountingStructure.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        ContractAccountingStructure.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxContractAccountingStructure As ContractAccountingStructure = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractAccountingStructure)
            Dim status As Integer

            If ContractAccountingStructure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                ContractAccountingStructure.CreationUser = audit.CodeUser
                ContractAccountingStructure.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxContractAccountingStructure = ContractAccountingStructure.OriginalValue
                ContractAccountingStructure.ModificationUser = audit.CodeUser
                ContractAccountingStructure.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._contractAccountingStructureRepository.SaveEntity(ContractAccountingStructure)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContractAccountingStructure)(ContractAccountingStructure, audit, status, auxContractAccountingStructure)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            ContractAccountingStructure.MarkAsUnchanged()

            Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = True, .ObjectEmbbeded = ContractAccountingStructure}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractAccountingStructure) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _contractAccountingStructureRepository = Nothing
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
