#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetInventoryTypeAdminService

    Implements IFixedAssetInventoryTypeAdminService
    Private Const FORM_NAME As String = "FrmFixedAssetInventoryType"
    'Repositorio de la aseguradora
    Private _FixedAssetInventoryTypeRepository As IFixedAssetInventoryTypeRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="InsuranceRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal FixedAssetInventoryTypeRepository As IFixedAssetInventoryTypeRepository, sequenceRepository As IFixedAssetSequenceDetailRepository)
        If (FixedAssetInventoryTypeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de aseguradora vacio")
        End If
        _sequenceRepository = sequenceRepository
        _FixedAssetInventoryTypeRepository = FixedAssetInventoryTypeRepository
    End Sub

    Public Function DeleteFixedAssetInventoryType(FixedAssetInventoryType As FixedAssetInventoryType, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IFixedAssetInventoryTypeAdminService.DeleteFixedAssetInventoryType
        'If FixedAssetInventoryType Is Nothing Then
        '    Throw New ArgumentNullException("FixedAssetInventoryType vacio")
        'End If
        'Dim unitWork As IUnitWork = _FixedAssetInventoryTypeRepository.UnitWork
        'Try
        '    _FixedAssetInventoryTypeRepository.DeleteEntity(FixedAssetInventoryType)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetInventoryType)(FixedAssetInventoryType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
        '    Return False
        'End Try



        If FixedAssetInventoryType Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetInventoryTypeRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                FixedAssetInventoryType.ModificationUser = audit.CodeUser
                FixedAssetInventoryType.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetInventoryType)(FixedAssetInventoryType, audit, status)

                'While FixedAssetInventoryType.InvoiceCategoriesUser.Count > 0
                '    FixedAssetInventoryType.InvoiceCategoriesUser(FixedAssetInventoryType.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                FixedAssetInventoryType.MarkAsDeleted()
                Me._FixedAssetInventoryTypeRepository.SaveEntity(FixedAssetInventoryType)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetFixedAssetInventoryType(code As String) As FixedAssetInventoryType Implements IFixedAssetInventoryTypeAdminService.GetFixedAssetInventoryType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo de Marca vacio")
        End If
        Try
            Return _FixedAssetInventoryTypeRepository.GetInventoryType(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New FixedAssetInventoryType()
        End Try
    End Function

    Public Function ListAllFixedAssetInventoryType() As List(Of FixedAssetInventoryType) Implements IFixedAssetInventoryTypeAdminService.ListAllFixedAssetInventoryType
        Try
            Return _FixedAssetInventoryTypeRepository.ListAllInventoryType()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveFixedAssetInventoryType(FixedAssetInventoryType As FixedAssetInventoryType, audit As AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInventoryType) Implements IFixedAssetInventoryTypeAdminService.SaveFixedAssetInventoryType
        'If FixedAssetInventoryType Is Nothing Then
        '    Throw New ArgumentNullException("FixedAssetInventoryType")
        'End If
        'Dim unitOfWork As IUnitWork = Me._FixedAssetInventoryTypeRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        'Try
        '    Dim seq As FixedAssetSequenceDetail = Nothing
        '    If FixedAssetInventoryType.Code Is Nothing OrElse FixedAssetInventoryType.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._sequenceRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                FixedAssetInventoryType.Code = res
        '                seq.Next += 1
        '                Me._sequenceRepository.SaveEntity(seq)
        '            Else
        '                Return False
        '            End If
        '        Else
        '            Return False
        '        End If
        '    End If

        '    Dim auxFixedAssetInventoryType As FixedAssetInventoryType = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetInventoryType)
        '    Dim status As Integer

        '    If FixedAssetInventoryType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        FixedAssetInventoryType.CreationUser = audit.CodeUser
        '        FixedAssetInventoryType.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxFixedAssetInventoryType = FixedAssetInventoryType.OriginalValue
        '        FixedAssetInventoryType.ModificationUser = audit.CodeUser
        '        FixedAssetInventoryType.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._FixedAssetInventoryTypeRepository.SaveEntity(FixedAssetInventoryType)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetInventoryType)(FixedAssetInventoryType, audit, status, auxFixedAssetInventoryType)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    FixedAssetInventoryType.MarkAsUnchanged()

        '    Return True
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return False
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try



        If FixedAssetInventoryType Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetInventoryTypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(FixedAssetInventoryType.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._sequenceRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetInventoryType.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetInventoryType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), FixedAssetInventoryType.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetInventoryType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As FixedAssetInventoryType = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetInventoryType)
                Dim status As Integer

                If FixedAssetInventoryType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    FixedAssetInventoryType.CreationUser = audit.CodeUser
                    FixedAssetInventoryType.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = FixedAssetInventoryType.OriginalValue
                    FixedAssetInventoryType.ModificationUser = audit.CodeUser
                    FixedAssetInventoryType.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._FixedAssetInventoryTypeRepository.SaveEntity(FixedAssetInventoryType)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetInventoryType)(FixedAssetInventoryType, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetInventoryType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FixedAssetInventoryType) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = FixedAssetInventoryType, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetInventoryType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetInventoryType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Function ChangeFixedAssetInventoryTypeStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInventoryType) Implements IFixedAssetInventoryTypeAdminService.ChangeFixedAssetInventoryTypeStatus
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetInventoryType As FixedAssetInventoryType = Me.GetFixedAssetInventoryType(code.Trim())
            If FixedAssetInventoryType IsNot Nothing AndAlso FixedAssetInventoryType.Id > 0 Then
                FixedAssetInventoryType.Status = state
            End If
            Dim result = Me.SaveFixedAssetInventoryType(FixedAssetInventoryType, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetInventoryType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _FixedAssetInventoryTypeRepository = Nothing
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
