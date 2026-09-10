'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Application.MixingStation
Imports Application.Security
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class ExternalCareCenterAdminService
    Implements IExternalCareCenterAdminService, Inject

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _ExternalCareCenterRepository As IExternalCareCenterRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

    ''' <summary>
    ''' Repositorio de usuarios
    ''' </summary>
    Private _IUserAdminService As IUserAdminService

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ExternalCareCenterRepository As IExternalCareCenterRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository, IUserAdminService As IUserAdminService)
        If ExternalCareCenterRepository Is Nothing Then
            Throw New ArgumentNullException("ExternalCareCenterRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        _ExternalCareCenterRepository = ExternalCareCenterRepository
        _secuenseDetailRepository = secuenseDetailRepository
        Me._IUserAdminService = IUserAdminService
    End Sub

    Public Function SaveExternalCareCenter(ExternalCareCenter As ExternalCareCenter, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of ExternalCareCenter) Implements IExternalCareCenterAdminService.SaveExternalCareCenter
        If ExternalCareCenter Is Nothing Then
            Throw New ArgumentNullException("ExternalCareCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._ExternalCareCenterRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As MixingStationSequenceDetail = Nothing
                If ExternalCareCenter.Code Is Nothing OrElse ExternalCareCenter.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDetailRepository.GetSequenceDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ExternalCareCenter.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ExternalCareCenter) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ExternalCareCenter) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxExternalCareCenter As ExternalCareCenter = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ExternalCareCenter)
                Dim status As Integer

                If ExternalCareCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ExternalCareCenter.CreationUser = audit.CodeUser
                    ExternalCareCenter.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxExternalCareCenter = ExternalCareCenter.OriginalValue
                    ExternalCareCenter.ModificationUser = audit.CodeUser
                    ExternalCareCenter.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._ExternalCareCenterRepository.SaveEntity(ExternalCareCenter)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ExternalCareCenter)(ExternalCareCenter, audit, status, auxExternalCareCenter)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ExternalCareCenter.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ExternalCareCenter) With {.StateResult = True, .ObjectEmbbeded = ExternalCareCenter}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ExternalCareCenter) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExternalCareCenter) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function DeleteExternalCareCenter(externalCareCenter As ExternalCareCenter, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IExternalCareCenterAdminService.DeleteExternalCareCenter
        If externalCareCenter Is Nothing Then
            Throw New ArgumentNullException("ExternalCareCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._ExternalCareCenterRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                If externalCareCenter.ExternalCareCenterUsers IsNot Nothing AndAlso externalCareCenter.ExternalCareCenterUsers.Any() Then
                    For i As Integer = 0 To externalCareCenter.ExternalCareCenterUsers.Count - 1
                        externalCareCenter.ExternalCareCenterUsers(i).MarkAsDeleted()
                    Next
                End If

                externalCareCenter.MarkAsDeleted()

                Me._ExternalCareCenterRepository.SaveEntity(externalCareCenter)
                unitOfWork.Commit()
                scope.Complete()
            End Using

            Dim auditProcess As New IndigoAuditSimpleEntity(Of ExternalCareCenter)(externalCareCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
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

    Public Function GetExternalCareCenter(code As String, audit As AuditMessage) As ActionResult(Of ExternalCareCenter) Implements IExternalCareCenterAdminService.GetExternalCareCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ExternalCareCenter As ExternalCareCenter = Me._ExternalCareCenterRepository.GetExternalCareCenter(code)
            If ExternalCareCenter IsNot Nothing AndAlso ExternalCareCenter.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ExternalCareCenter)(ExternalCareCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If ExternalCareCenter IsNot Nothing AndAlso ExternalCareCenter.Id > 0 AndAlso ExternalCareCenter.ExternalCareCenterUsers IsNot Nothing AndAlso ExternalCareCenter.ExternalCareCenterUsers.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each u In ExternalCareCenter.ExternalCareCenterUsers
                    ListUserIds.Add(u.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In ExternalCareCenter.ExternalCareCenterUsers
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of ExternalCareCenter) With {.StateResult = True, .ObjectEmbbeded = ExternalCareCenter}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExternalCareCenter) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetExternalCareCenterById(id As Integer) As ActionResult(Of ExternalCareCenter) Implements IExternalCareCenterAdminService.GetExternalCareCenterById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim ExternalCareCenter As ExternalCareCenter = Me._ExternalCareCenterRepository.GetExternalCareCenterById(id)
            Return New ActionResult(Of ExternalCareCenter) With {.StateResult = True, .ObjectEmbbeded = ExternalCareCenter}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ExternalCareCenter) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ChangeStateExternalCareCenter(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of ExternalCareCenter) Implements IExternalCareCenterAdminService.ChangeStateExternalCareCenter
        Dim ExternalCareCenter As ExternalCareCenter = _ExternalCareCenterRepository.GetExternalCareCenter(code)
        ExternalCareCenter.Status = state
        Return SaveExternalCareCenter(ExternalCareCenter, audit, operatingUnitId)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _ExternalCareCenterRepository = Nothing
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
