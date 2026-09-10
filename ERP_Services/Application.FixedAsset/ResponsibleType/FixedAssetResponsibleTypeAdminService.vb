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

Public Class FixedAssetResponsibleTypeAdminService

    Implements IFixedAssetResponsibleTypeAdminService
    Private Const FORM_NAME As String = "FrmFixedAssetResponsibleType"
    'Repositorio de tipo de ubicacion
    Private _responsibleTypeRespository As IFixedAssetResponsibleTypeRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IFixedAssetSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="reponsibleTypeRespository">Repositorio de Tipos de Responsable</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As IFixedAssetSequenceDetailRepository, ByVal reponsibleTypeRespository As IFixedAssetResponsibleTypeRepository)
        If (reponsibleTypeRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de responsibleTypeRespository")
        End If
        _responsibleTypeRespository = reponsibleTypeRespository
        _secuenseDRepository = secuenceDRepository
    End Sub

    Public Function DeleteResponsibleType(ResponsibleType As ResponsibleType, audit As AuditMessage) As ActionResult Implements IFixedAssetResponsibleTypeAdminService.DeleteResponsibleType
        'If ResponsibleType Is Nothing Then
        '    Throw New ArgumentNullException("Trademark vacio")
        'End If
        'Dim unitWork As IUnitWork = _responsibleTypeRespository.UnitWork
        'Try
        '    _responsibleTypeRespository.DeleteEntity(ResponsibleType)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of ResponsibleType)(ResponsibleType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If ResponsibleType Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._responsibleTypeRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                ResponsibleType.ModificationUser = audit.CodeUser
                ResponsibleType.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ResponsibleType)(ResponsibleType, audit, status)

                'While ResponsibleType.InvoiceCategoriesUser.Count > 0
                '    ResponsibleType.InvoiceCategoriesUser(ResponsibleType.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                ResponsibleType.MarkAsDeleted()
                Me._responsibleTypeRespository.SaveEntity(ResponsibleType)
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

    Public Function GetResponsibleTypeByCode(Code As String) As ResponsibleType Implements IFixedAssetResponsibleTypeAdminService.GetResponsibleTypeByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de Marca vacio")
        End If
        Try
            Return _responsibleTypeRespository.GetResponsibleTypeByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New ResponsibleType()
        End Try
    End Function

    Public Function ListAllResponsibleType() As List(Of ResponsibleType) Implements IFixedAssetResponsibleTypeAdminService.ListAllResponsibleType
        Try
            Return _responsibleTypeRespository.ListAllResponsibleType()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveResponsibleType(ResponsibleType As ResponsibleType, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ResponsibleType) Implements IFixedAssetResponsibleTypeAdminService.SaveResponsibleType
        'If ResponsibleType Is Nothing Then
        '    Throw New ArgumentNullException("ResponsibleType")
        'End If
        'Dim unitOfWork As IUnitWork = Me._responsibleTypeRespository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        'Try
        '    Dim seq As FixedAssetSequenceDetail = Nothing
        '    If ResponsibleType.Code Is Nothing OrElse ResponsibleType.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                ResponsibleType.Code = res
        '                seq.Next += 1
        '                Me._secuenseDRepository.SaveEntity(seq)
        '            Else
        '                Return False
        '            End If
        '        Else
        '            Return False
        '        End If
        '    End If

        '    Dim auxResponsible As ResponsibleType = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of ResponsibleType)
        '    Dim status As Integer

        '    If ResponsibleType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        ResponsibleType.CreationUser = audit.CodeUser
        '        ResponsibleType.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxResponsible = ResponsibleType.OriginalValue
        '        ResponsibleType.ModificationUser = audit.CodeUser
        '        ResponsibleType.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._responsibleTypeRespository.SaveEntity(ResponsibleType)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of ResponsibleType)(ResponsibleType, audit, status, auxResponsible)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    ResponsibleType.MarkAsUnchanged()

        '    Return True
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return False
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try

        If ResponsibleType Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._responsibleTypeRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(ResponsibleType.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ResponsibleType.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ResponsibleType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), ResponsibleType.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ResponsibleType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ResponsibleType = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ResponsibleType)
                Dim status As Integer

                If ResponsibleType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ResponsibleType.CreationUser = audit.CodeUser
                    ResponsibleType.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = ResponsibleType.OriginalValue
                    ResponsibleType.ModificationUser = audit.CodeUser
                    ResponsibleType.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._responsibleTypeRespository.SaveEntity(ResponsibleType)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ResponsibleType)(ResponsibleType, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ResponsibleType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ResponsibleType) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ResponsibleType, .Message = MessageResult}
            End Using
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ResponsibleType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {ex.InnerException.InnerException.Message}.ToList, .Message = ex.InnerException.InnerException.Message}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ResponsibleType) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ResponsibleType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


    Function ChangeFixedAssetResponsibleTypeStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ResponsibleType) Implements IFixedAssetResponsibleTypeAdminService.ChangeFixedAssetResponsibleTypeStatus
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
            Dim ResponsibleType As ResponsibleType = Me._responsibleTypeRespository.GetResponsibleTypeByCode(code.Trim())
            If ResponsibleType IsNot Nothing AndAlso ResponsibleType.Id > 0 Then
                ResponsibleType.Status = state
            End If
            Dim result = Me.SaveResponsibleType(ResponsibleType, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ResponsibleType) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _responsibleTypeRespository = Nothing
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
