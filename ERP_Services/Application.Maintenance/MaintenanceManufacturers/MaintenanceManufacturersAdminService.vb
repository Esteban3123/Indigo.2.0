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
Public Class MaintenanceManufacturersAdminService

    Implements IMaintenanceManufacturersAdminService
    Private Const FORM_NAME As String = "FrmMaintenanceManufacturers"
    'Repositorio de tipo de ubicacion
    Private _MaintenanceManufacturersRespository As IMaintenanceManufacturersRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de ubicacion
    ''' </summary>
    ''' <param name="MaintenanceManufacturersRespository">Repositorio de Fabricantes</param>
    ''' <remarks></remarks>
    Public Sub New(secuenceDRepository As IMaintenanceSequenceDetailRepository, ByVal MaintenanceManufacturersRespository As IMaintenanceManufacturersRepository)
        If (MaintenanceManufacturersRespository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de MaintenanceManufacturersRespository")
        End If
        _MaintenanceManufacturersRespository = MaintenanceManufacturersRespository
        _secuenseDRepository = secuenceDRepository
    End Sub

    ''' <summary>
    ''' Función que obtiene una Fabricantes por Código
    ''' </summary>
    ''' <param name="Code">Código de la Fabricantes</param>
    ''' <returns>MaintenanceManufacturers</returns>
    ''' <remarks></remarks>
    Public Function GetMaintenanceManufacturersByCode(Code As String) As MaintenanceManufacturers Implements IMaintenanceManufacturersAdminService.GetMaintenanceManufacturersByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de Fabricantes vacio")
        End If
        Try
            Return _MaintenanceManufacturersRespository.GetMaintenanceManufacturersByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New MaintenanceManufacturers()
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene todos los fabricantes
    ''' </summary>
    ''' <returns>Lista de Fabricantes</returns>
    ''' <remarks></remarks>
    Public Function ListAllMaintenanceManufacturers() As List(Of MaintenanceManufacturers) Implements IMaintenanceManufacturersAdminService.ListAllMaintenanceManufacturers
        Try
            Return _MaintenanceManufacturersRespository.ListAllMaintenanceManufacturers()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para Eliminar Fabricantes
    ''' </summary>
    ''' <param name="MaintenanceManufacturers">Objeto MaintenanceManufacturers</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteMaintenanceManufacturers(MaintenanceManufacturers As MaintenanceManufacturers, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IMaintenanceManufacturersAdminService.DeleteMaintenanceManufacturers
        If MaintenanceManufacturers Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._MaintenanceManufacturersRespository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                MaintenanceManufacturers.ModificationUser = audit.CodeUser
                MaintenanceManufacturers.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of MaintenanceManufacturers)(MaintenanceManufacturers, audit, status)
                MaintenanceManufacturers.MarkAsDeleted()
                Me._MaintenanceManufacturersRespository.SaveEntity(MaintenanceManufacturers)
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
    ''' Función para Almacena Fabricantes
    ''' </summary>
    ''' <param name="MaintenanceManufacturers">Objeto MaintenanceManufacturers</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveMaintenanceManufacturers(MaintenanceManufacturers As MaintenanceManufacturers, audit As AuditMessage, Optional idSequense As Long = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MaintenanceManufacturers) Implements IMaintenanceManufacturersAdminService.SaveMaintenanceManufacturers
        If MaintenanceManufacturers Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._MaintenanceManufacturersRespository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(MaintenanceManufacturers.Code) Then
                    Dim seq As MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            MaintenanceManufacturers.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MaintenanceManufacturers) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), MaintenanceManufacturers.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MaintenanceManufacturers) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As MaintenanceManufacturers = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MaintenanceManufacturers)
                Dim status As Integer

                If MaintenanceManufacturers.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    MaintenanceManufacturers.CreationUser = audit.CodeUser
                    MaintenanceManufacturers.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = MaintenanceManufacturers.OriginalValue
                    MaintenanceManufacturers.ModificationUser = audit.CodeUser
                    MaintenanceManufacturers.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._MaintenanceManufacturersRespository.SaveEntity(MaintenanceManufacturers)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MaintenanceManufacturers)(MaintenanceManufacturers, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se Fabricantes la entidad como sin cambios
                MaintenanceManufacturers.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MaintenanceManufacturers) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = MaintenanceManufacturers, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of MaintenanceManufacturers) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceManufacturers) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Function ChangeMaintenanceManufacturersStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MaintenanceManufacturers) Implements IMaintenanceManufacturersAdminService.ChangeMaintenanceManufacturersStatus
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
            Dim MaintenanceManufacturers As MaintenanceManufacturers = GetMaintenanceManufacturersByCode(code.Trim())
            If MaintenanceManufacturers IsNot Nothing AndAlso MaintenanceManufacturers.Id > 0 Then
                MaintenanceManufacturers.Status = state
            End If
            Dim result = Me.SaveMaintenanceManufacturers(MaintenanceManufacturers, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MaintenanceManufacturers) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _MaintenanceManufacturersRespository = Nothing
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
