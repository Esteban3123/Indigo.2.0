'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Andres Alarcon
' Created          : 10-06-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetCatalogOfPropertyandServicesAdminService
    Implements IFixedAssetCatalogOfPropertyandServicesAdminService

#Region "Variables"

    Private Const FORM_NAME As String = "FrmCatalogOfPropertyandServices"

    'Repositorio del catalogo de bienes y servicios
    Private _FixedAssetCatalogOfPropertyandServicesRepository As IFixedAssetCatalogOfPropertyandServicesRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal FixedAssetFixedAssetCatalogOfPropertyandServicesRepository As IFixedAssetCatalogOfPropertyandServicesRepository, sequenceRepository As IFixedAssetSequenceDetailRepository)
        If FixedAssetFixedAssetCatalogOfPropertyandServicesRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetFixedAssetCatalogOfPropertyandServicesRepository")
        End If
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If

        _sequenceRepository = sequenceRepository
        _FixedAssetCatalogOfPropertyandServicesRepository = FixedAssetFixedAssetCatalogOfPropertyandServicesRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un ingreso por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetCatalogOfPropertyandServicesByCode(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices) Implements IFixedAssetCatalogOfPropertyandServicesAdminService.GetFixedAssetCatalogOfPropertyandServicesByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim FixedAssetCatalogOfPropertyandServices = Me._FixedAssetCatalogOfPropertyandServicesRepository.GetCatalogOfPropertyandServicesByCode(code.Trim())
            If FixedAssetCatalogOfPropertyandServices IsNot Nothing AndAlso FixedAssetCatalogOfPropertyandServices.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetCatalogOfPropertyandServices)(FixedAssetCatalogOfPropertyandServices, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of FixedAssetCatalogOfPropertyandServices) With {.StateResult = True, .ObjectEmbbeded = FixedAssetCatalogOfPropertyandServices}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetCatalogOfPropertyandServices) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un catalogo de bienes y servicios
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetCatalogOfPropertyandServices) Implements IFixedAssetCatalogOfPropertyandServicesAdminService.SaveFixedAssetCatalogOfPropertyandServices
        If FixedAssetCatalogOfPropertyandServices Is Nothing Then
            Throw New ArgumentNullException("FixedAssetCatalogOfPropertyandServices")
        End If

        Dim unitOfWork As IUnitWork = Me._FixedAssetCatalogOfPropertyandServicesRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(FixedAssetCatalogOfPropertyandServices.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._sequenceRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetCatalogOfPropertyandServices.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetCatalogOfPropertyandServices) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), FixedAssetCatalogOfPropertyandServices.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetCatalogOfPropertyandServices) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxSettings As FixedAssetCatalogOfPropertyandServices = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetCatalogOfPropertyandServices)
                Dim status As Integer

                If FixedAssetCatalogOfPropertyandServices.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    FixedAssetCatalogOfPropertyandServices.CreationUser = audit.CodeUser
                    FixedAssetCatalogOfPropertyandServices.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxSettings = FixedAssetCatalogOfPropertyandServices.OriginalValue
                    FixedAssetCatalogOfPropertyandServices.ModificationUser = audit.CodeUser
                    FixedAssetCatalogOfPropertyandServices.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._FixedAssetCatalogOfPropertyandServicesRepository.SaveEntity(FixedAssetCatalogOfPropertyandServices)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetCatalogOfPropertyandServices)(FixedAssetCatalogOfPropertyandServices, audit, status, auxSettings)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetCatalogOfPropertyandServices.MarkAsUnchanged()
                scope.Complete()

                Return New ActionResult(Of FixedAssetCatalogOfPropertyandServices) With {.StateResult = True, .ObjectEmbbeded = FixedAssetCatalogOfPropertyandServices, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetCatalogOfPropertyandServices) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetCatalogOfPropertyandServices) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Función para eliminar un catalogo de bienes y servicios
    ''' </summary>
    ''' <param name="FixedAssetCatalogOfPropertyandServices"></param>
    ''' <param name="audit"></param>
    ''' <remarks></remarks>
    Public Function DeleteCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IFixedAssetCatalogOfPropertyandServicesAdminService.DeleteCatalogOfPropertyandServices
        If FixedAssetCatalogOfPropertyandServices Is Nothing Then
            Throw New ArgumentNullException("FixedAssetCatalogOfPropertyandServices")
        End If

        Dim unitOfWork As IUnitWork = Me._FixedAssetCatalogOfPropertyandServicesRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetCatalogOfPropertyandServices)(FixedAssetCatalogOfPropertyandServices, audit, status)

                FixedAssetCatalogOfPropertyandServices.MarkAsDeleted()
                Me._FixedAssetCatalogOfPropertyandServicesRepository.SaveEntity(FixedAssetCatalogOfPropertyandServices)
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

    ''' <summary>
    ''' Función para cambiar el estado del catalogo de bienes y servicios
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <param name="audit"></param>
    ''' <remarks></remarks>
    Function ChangeCatalogOfPropertyandServicesStatus(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices) Implements IFixedAssetCatalogOfPropertyandServicesAdminService.ChangeCatalogOfPropertyandServicesStatus
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If

        If String.IsNullOrEmpty(State) Then
            Throw New ArgumentNullException("state")
        End If

        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim FixedAssetCatalogOfPropertyandServices = GetFixedAssetCatalogOfPropertyandServicesByCode(Code.Trim(), audit)
            If FixedAssetCatalogOfPropertyandServices IsNot Nothing AndAlso FixedAssetCatalogOfPropertyandServices.StateResult Then
                FixedAssetCatalogOfPropertyandServices.ObjectEmbbeded.State = State
            End If

            Dim result = Me.SaveFixedAssetCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices.ObjectEmbbeded, audit)
            If result.StateResult Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If

            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetCatalogOfPropertyandServices) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#End Region
#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            _sequenceRepository = Nothing
            _FixedAssetCatalogOfPropertyandServicesRepository = Nothing
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
