'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 31-03-2014
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

Public Class BlockRecordTreasuryAdminService
    Implements IBlockRecordTreasuryAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad ciudades bancarias
    ''' </summary>
    Private _Repository As IBlockRecordTreasuryRepository

#End Region

    Public Sub New(ByVal Repository As IBlockRecordTreasuryRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = Repository
    End Sub

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordTreasury"></param>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordTreasury</exception>
    Public Function DeleteBlockRecordTreasury(blockRecordTreasury As BlockRecordTreasury) As ActionResult Implements IBlockRecordTreasuryAdminService.DeleteBlockRecordTreasury
        If blockRecordTreasury Is Nothing Then
            Throw New ArgumentNullException("blockRecordTreasury")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Me._Repository.DeleteEntity(blockRecordTreasury)
            unitOfWork.Commit()
            'Auditoria básica
            'IndigoAuditBasic.Execute(GetType(BlockRecordTreasury).Name, audit.Functional, blockRecordTreasury.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            ''Ausitoria avanzada
            'IndigoAuditSimpleEntity(Of BlockRecordTreasury).Execute(blockRecordTreasury, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, audit.Company)
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdForm IdRecord</exception>
    Public Function GetBlockRecordTreasuryByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordTreasury Implements IBlockRecordTreasuryAdminService.GetBlockRecordTreasuryByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim blockRecordTreasury As BlockRecordTreasury = Me._Repository.GetBlockRecordTreasuryByIdformAndIdRecord(IdForm, IdRecord)
            Return blockRecordTreasury
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordTreasury"></param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordTreasury Vacio</exception>
    Public Function SaveBlockRecordTreasury(blockRecordTreasury As BlockRecordTreasury) As ActionResult(Of BlockRecordTreasury) Implements IBlockRecordTreasuryAdminService.SaveBlockRecordTreasury
        If blockRecordTreasury Is Nothing Then
            Throw New ArgumentNullException("blockRecordTreasury Vacio")
        End If
        'Dim AuxblockRecordTreasury As BlockRecordTreasury = Nothing
        'If blockRecordTreasury.ChangeTracker.State = ObjectState.Modified Then
        '    'AuxBlockRecord = _blockRecordRepository.GetBlockRecordByIdformAndIdRecord(blockRecordTreasury.IdForm, blockRecordTreasury.IdRecord, False)
        'End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _Repository.SaveEntity(blockRecordTreasury)
            UnitOfWork.Commit()
            'Agrego la auditoria
            'If blockRecordTreasury.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            '    IndigoAuditSimpleEntity(Of blockRecordTreasury).Execute(blockRecordTreasury, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
            'ElseIf blockRecordTreasury.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            '    IndigoAuditSimpleEntity(Of blockRecordTreasury).Execute(blockRecordTreasury, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxBlockRecord)
            'End If
            Return New ActionResult(Of BlockRecordTreasury) With {.StateResult = True, .ObjectEmbbeded = blockRecordTreasury}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordTreasury) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _Repository = Nothing
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
