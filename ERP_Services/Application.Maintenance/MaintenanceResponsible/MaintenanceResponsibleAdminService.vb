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

Public Class MaintenanceResponsibleAdminService

    Implements IMaintenanceResponsibleAdminService


    Private Const FORM_NAME As String = "FrmMaintenanceResponsible"
    'Repositorio de tipo de ubicacion
    Private _responsibleRespository As IMaintenanceResponsibleRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="reponsibleRespository">Repositorio de Responsable</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As IMaintenanceSequenceDetailRepository, ByVal reponsibleRespository As IMaintenanceResponsibleRepository)
        If (reponsibleRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de responsibleTypeRespository")
        End If
        _responsibleRespository = reponsibleRespository
        _secuenseDRepository = secuenceDRepository
    End Sub

    Public Function DeleteResponsible(Responsible As MaintenanceResponsible, audit As AuditMessage) As ActionResult Implements IMaintenanceResponsibleAdminService.DeleteResponsible
        'If Responsible Is Nothing Then
        '    Throw New ArgumentNullException("Trademark vacio")
        'End If
        'Dim unitWork As IUnitWork = _responsibleRespository.UnitWork
        'Try
        '    _responsibleRespository.DeleteEntity(Responsible)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of MaintenanceResponsible)(Responsible, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If Responsible Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._responsibleRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Responsible.ModificationUser = audit.CodeUser
                Responsible.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of MaintenanceResponsible)(Responsible, audit, status)

                While Responsible.ResponsibleCatalogOfArticles.Count > 0
                    Responsible.ResponsibleCatalogOfArticles(Responsible.ResponsibleCatalogOfArticles.Count - 1).MarkAsDeleted()
                End While
                Responsible.MarkAsDeleted()
                Me._responsibleRespository.SaveEntity(Responsible)
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

    Public Function GetResponsibleTypeByCode(Code As String) As MaintenanceResponsible Implements IMaintenanceResponsibleAdminService.GetResponsibleByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de Responsable vacio")
        End If
        Try
            Return _responsibleRespository.GetResponsibleByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New MaintenanceResponsible()
        End Try
    End Function

    Public Function ListAllResponsible() As List(Of MaintenanceResponsible) Implements IMaintenanceResponsibleAdminService.ListAllResponsible
        Try
            Return _responsibleRespository.ListAllResponsible()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveResponsibleType(Responsible As MaintenanceResponsible, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MaintenanceResponsible) Implements IMaintenanceResponsibleAdminService.SaveResponsible
        'If Responsible Is Nothing Then
        '    Throw New ArgumentNullException("Responsible")
        'End If
        'Dim unitOfWork As IUnitWork = Me._responsibleRespository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        'Try
        '    Dim seq As MaintenanceSequenceDetail = Nothing
        '    If Responsible.Code Is Nothing OrElse Responsible.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                Responsible.Code = res
        '                seq.Next += 1
        '                Me._secuenseDRepository.SaveEntity(seq)
        '            Else
        '                Return False
        '            End If
        '        Else
        '            Return False
        '        End If
        '    End If

        '    Dim auxResponsible As MaintenanceResponsible = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of MaintenanceResponsible)
        '    Dim status As Integer

        '    If Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        Responsible.CreationUser = audit.CodeUser
        '        Responsible.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxResponsible = Responsible.OriginalValue
        '        Responsible.ModificationUser = audit.CodeUser
        '        Responsible.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._responsibleRespository.SaveEntity(Responsible)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of MaintenanceResponsible)(Responsible, audit, status, auxResponsible)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    Responsible.MarkAsUnchanged()

        '    Return True
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return False
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try



        If Responsible Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._responsibleRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Responsible.Code) Then
                    Dim seq As MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Responsible.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MaintenanceResponsible) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Responsible.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MaintenanceResponsible) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As MaintenanceResponsible = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MaintenanceResponsible)
                Dim status As Integer

                If Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Responsible.CreationUser = audit.CodeUser
                    Responsible.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Responsible.OriginalValue
                    Responsible.ModificationUser = audit.CodeUser
                    Responsible.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._responsibleRespository.SaveEntity(Responsible)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MaintenanceResponsible)(Responsible, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Responsible.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MaintenanceResponsible) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Responsible, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MaintenanceResponsible) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceResponsible) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ChangeStateMaintenanceResponsible(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MaintenanceResponsible) Implements IMaintenanceResponsibleAdminService.ChangeStateMaintenanceResponsible
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
            Dim Responsible As MaintenanceResponsible = Me.GetResponsibleTypeByCode(code.Trim())
            If Responsible IsNot Nothing AndAlso Responsible.Id > 0 Then
                Responsible.Status = state
            End If
            Dim result = Me.SaveResponsibleType(Responsible, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceResponsible) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetItemCatalogByCode(Code As String) As FixedAssetItemCatalog Implements IMaintenanceResponsibleAdminService.GetItemCatalogByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de Responsable vacio")
        End If
        Try
            Return _responsibleRespository.GetItemCatalogByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetItemCatalogByIdUser(IdResponsible As Integer) As List(Of ResponsibleCatalogOfArticles) Implements IMaintenanceResponsibleAdminService.GetItemCatalogByIdUser
        If String.IsNullOrEmpty(IdResponsible) Then
            Throw New ArgumentNullException("Codigo de Responsable vacio")
        End If
        Try
            Return _responsibleRespository.GetItemCatalogByIdUser(IdResponsible)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _responsibleRespository = Nothing
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
