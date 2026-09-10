'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Sergio Fernandez
' Created          : 07/04/2014
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

Public Class BlockRecordFixedAssetAdminService
    Implements IBlockRecordFixedAssetAdminService


#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad ciudades bancarias
    ''' </summary>
    Private _Repository As IBlockRecordFixedAssetRepository

#End Region

#Region "Constructor"

    Public Sub New(ByVal Repository As IBlockRecordFixedAssetRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = Repository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordFixedAsset"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBlockRecordFixedAsset(blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset, audit As AuditMessage) As ActionResult Implements IBlockRecordFixedAssetAdminService.DeleteBlockRecordFixedAsset
        If blockRecordFixedAsset Is Nothing Then
            Throw New ArgumentNullException("blockRecordFixedAsset")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Me._Repository.DeleteEntity(blockRecordFixedAsset)

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
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockRecordFixedAssetByIdformAndIdRecord(IdForm As String, IdRecord As String) As Domain.Entities.BlockRecordFixedAsset Implements IBlockRecordFixedAssetAdminService.GetBlockRecordFixedAssetByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset = Me._Repository.GetBlockRecordFixedAssetByIdformAndIdRecord(IdForm, IdRecord)
            Return blockRecordFixedAsset
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordFixedAsset"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBlockRecordFixedAsset(blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset, audit As AuditMessage) As ActionResult(Of Domain.Entities.BlockRecordFixedAsset) Implements IBlockRecordFixedAssetAdminService.SaveBlockRecordFixedAsset
        If blockRecordFixedAsset Is Nothing Then
            Throw New ArgumentNullException("blockRecordFixedAsset Vacio")
        End If
        'Dim AuxblockRecordTreasury As BlockRecordTreasury = Nothing
        'If blockRecordTreasury.ChangeTracker.State = ObjectState.Modified Then
        '    'AuxBlockRecord = _blockRecordRepository.GetBlockRecordByIdformAndIdRecord(blockRecordTreasury.IdForm, blockRecordTreasury.IdRecord, False)
        'End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _Repository.SaveEntity(blockRecordFixedAsset)
            UnitOfWork.Commit()
            'Agrego la auditoria
            'If blockRecordTreasury.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            '    IndigoAuditSimpleEntity(Of blockRecordTreasury).Execute(blockRecordTreasury, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
            'ElseIf blockRecordTreasury.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            '    IndigoAuditSimpleEntity(Of blockRecordTreasury).Execute(blockRecordTreasury, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxBlockRecord)
            'End If
            Return New ActionResult(Of Domain.Entities.BlockRecordFixedAsset) With {.StateResult = True, .ObjectEmbbeded = blockRecordFixedAsset}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of Domain.Entities.BlockRecordFixedAsset) With {.StateResult = False}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
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
