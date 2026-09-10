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

Public Class MaintenanceToolsAdminService

    Implements IMaintenanceToolsAdminService


    Private Const FORM_NAME As String = "FrmMaintenanceTools"
    'Repositorio de tipo de ubicacion
    Private _ToolsRespository As IMaintenanceToolsRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="MaintenanceToolsRespository">Repositorio de Herramientas</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As IMaintenanceSequenceDetailRepository, ByVal MaintenanceToolsRespository As IMaintenanceToolsRepository)
        If (MaintenanceToolsRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de MaintenanceTools")
        End If
        _ToolsRespository = MaintenanceToolsRespository
        _secuenseDRepository = secuenceDRepository
    End Sub

    Public Function DeleteTools(Tools As MaintenanceTools, audit As AuditMessage) As ActionResult Implements IMaintenanceToolsAdminService.DeleteTools


        If Tools Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._ToolsRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Tools.ModificationUser = audit.CodeUser
                Tools.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of MaintenanceTools)(Tools, audit, status)

                While Tools.MaintenanceToolsItemDetail.Count > 0
                    Tools.MaintenanceToolsItemDetail(Tools.MaintenanceToolsItemDetail.Count - 1).MarkAsDeleted()
                End While
                Tools.MarkAsDeleted()
                Me._ToolsRespository.SaveEntity(Tools)
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

    Public Function GetToolsTypeByCode(Code As String) As MaintenanceTools Implements IMaintenanceToolsAdminService.GetToolsByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de Responsable vacio")
        End If
        Try
            Return _ToolsRespository.GetToolsByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New MaintenanceTools()
        End Try
    End Function

    Public Function ListAllTools() As List(Of MaintenanceTools) Implements IMaintenanceToolsAdminService.ListAllTools
        Try
            Return _ToolsRespository.ListAllTools()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveToolsType(Tools As MaintenanceTools, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceTools) Implements IMaintenanceToolsAdminService.SaveTools

        If Tools Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._ToolsRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Tools.Code) Then
                    Dim seq As MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Tools.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MaintenanceTools) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Tools.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MaintenanceTools) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As MaintenanceTools = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MaintenanceTools)
                Dim status As Integer

                If Tools.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Tools.CreationUser = audit.CodeUser
                    Tools.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Tools.OriginalValue
                    Tools.ModificationUser = audit.CodeUser
                    Tools.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._ToolsRespository.SaveEntity(Tools)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MaintenanceTools)(Tools, audit, status, auxObjEntity)
                auditProcess.Execute()

                Tools.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MaintenanceTools) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Tools, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MaintenanceTools) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceTools) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetItemByCode(Code As String) As FixedAssetItem Implements IMaintenanceToolsAdminService.GetItemByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _ToolsRespository.GetItemByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetItemByIdTool(IdTools As Integer) As List(Of MaintenanceToolsItemDetail) Implements IMaintenanceToolsAdminService.GetItemDetailByIdUser
        If String.IsNullOrEmpty(IdTools) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _ToolsRespository.GetItemByIdTool(IdTools)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ToolsRespository = Nothing
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
