'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Daniel Eduardo Arévalo
' Created          : 04-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Application.Base
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetVinculationTypeAdminService

    Implements IFixedAssetVinculationTypeAdminService

    'Repositorio de tipo de ubicacion
    Private _vinculationTypeRespository As IFixedAssetVinculationTypeRepository

    Private Const FORM_NAME As String = "FrmFixedAssetVinculationType"

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IFixedAssetSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="vinculationTypeRespository">Repositorio de Tipos de Responsable</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As IFixedAssetSequenceDetailRepository, ByVal vinculationTypeRespository As IFixedAssetVinculationTypeRepository)
        If (vinculationTypeRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de vinculationTypeRespository")
        End If
        _vinculationTypeRespository = vinculationTypeRespository
        _secuenseDRepository = secuenceDRepository
    End Sub

    Public Function DeleteVinculationType(VinculationType As FixedAssetVinculationType, audit As AuditMessage) As ActionResult Implements IFixedAssetVinculationTypeAdminService.DeleteVinculationType
        'If VinculationType Is Nothing Then
        '    Throw New ArgumentNullException("VinculationType vacio")
        'End If
        'Dim unitWork As IUnitWork = _vinculationTypeRespository.UnitWork
        'Try
        '    _vinculationTypeRespository.DeleteEntity(VinculationType)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetVinculationType)(VinculationType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If VinculationType Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._vinculationTypeRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                VinculationType.ModificationUser = audit.CodeUser
                VinculationType.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetVinculationType)(VinculationType, audit, status)

                'While VinculationType.InvoiceCategoriesUser.Count > 0
                '    VinculationType.InvoiceCategoriesUser(VinculationType.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                VinculationType.MarkAsDeleted()
                Me._vinculationTypeRespository.SaveEntity(VinculationType)
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

    Public Function GetVinculationTypeByCode(Code As String) As FixedAssetVinculationType Implements IFixedAssetVinculationTypeAdminService.GetVinculationTypeByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de Marca vacio")
        End If
        Try
            Return _vinculationTypeRespository.GetVinculationTypeByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New FixedAssetVinculationType()
        End Try
    End Function

    Public Function ListAllVinculationType() As List(Of FixedAssetVinculationType) Implements IFixedAssetVinculationTypeAdminService.ListAllVinculationType
        Try
            Return _vinculationTypeRespository.ListAllVinculationType()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveVinculationType(VinculationType As FixedAssetVinculationType, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetVinculationType) Implements IFixedAssetVinculationTypeAdminService.SaveVinculationType
        'If VinculationType Is Nothing Then
        '    Throw New ArgumentNullException("ResponsibleType")
        'End If
        'Dim unitOfWork As IUnitWork = Me._vinculationTypeRespository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        'Try
        '    Dim seq As FixedAssetSequenceDetail = Nothing
        '    If VinculationType.Code Is Nothing OrElse VinculationType.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                VinculationType.Code = res
        '                seq.Next += 1
        '                Me._secuenseDRepository.SaveEntity(seq)
        '            Else
        '                Return False
        '            End If
        '        Else
        '            Return False
        '        End If
        '    End If

        '    Dim auxVinculationType As FixedAssetVinculationType = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetVinculationType)
        '    Dim status As Integer

        '    If VinculationType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        VinculationType.CreationUser = audit.CodeUser
        '        VinculationType.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxVinculationType = VinculationType.OriginalValue
        '        VinculationType.ModificationUser = audit.CodeUser
        '        VinculationType.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._vinculationTypeRespository.SaveEntity(VinculationType)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetVinculationType)(VinculationType, audit, status, auxVinculationType)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    VinculationType.MarkAsUnchanged()

        '    Return True
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return False
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try



        If VinculationType Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._vinculationTypeRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(VinculationType.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            VinculationType.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetVinculationType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), VinculationType.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetVinculationType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As FixedAssetVinculationType = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetVinculationType)
                Dim status As Integer

                If VinculationType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    VinculationType.CreationUser = audit.CodeUser
                    VinculationType.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = VinculationType.OriginalValue
                    VinculationType.ModificationUser = audit.CodeUser
                    VinculationType.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._vinculationTypeRespository.SaveEntity(VinculationType)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetVinculationType)(VinculationType, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                VinculationType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FixedAssetVinculationType) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = VinculationType, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetVinculationType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetVinculationType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeFixedAssetVinculationTypeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetVinculationType) Implements IFixedAssetVinculationTypeAdminService.ChangeFixedAssetVinculationTypeState
        'Dim ObjFixedAssetStatus As FixedAssetStatusAsset = _FixedAssetStatusRepository.GetFixedAssetStatusByCode(code)
        'ObjFixedAssetStatus.Status = state
        'Return SaveFixedAssetStatusAsset(ObjFixedAssetStatus, audit)


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
            Dim ObjFixedAssetStatus As FixedAssetVinculationType = Me._vinculationTypeRespository.GetVinculationTypeByCode(code.Trim())
            If ObjFixedAssetStatus IsNot Nothing AndAlso ObjFixedAssetStatus.Id > 0 Then
                ObjFixedAssetStatus.Status = state
            End If
            Dim result = Me.SaveVinculationType(ObjFixedAssetStatus, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetVinculationType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _vinculationTypeRespository = Nothing
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
