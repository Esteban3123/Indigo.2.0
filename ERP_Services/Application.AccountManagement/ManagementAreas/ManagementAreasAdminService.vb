'***********************************************************************
' Assembly         : Application.AccountManagement
' Author           : Felix Camilo Salazar Roldan
' Created          : 14-11-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Security
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region


Public Class ManagementAreasAdminService
    Implements IManagementAreasAdminService

    Private Const FORM_NAME As String = "FrmManagementAreas"

    Private _secuenseDRepository As IAccountManagementSequenceDetailRepository
    Private _managementAreasRepository As IManagementAreasRepository
    Private _IUserAdminService As IUserAdminService

    Public Sub New(secuenceDRepository As IAccountManagementSequenceDetailRepository, ByVal ManagementAreas As IManagementAreasRepository, IUserAdminService As IUserAdminService)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If ManagementAreas Is Nothing Then
            Throw New ArgumentNullException("ManagementAreas vacío")
        End If
        If IUserAdminService Is Nothing Then
            Throw New ArgumentNullException("IUserAdminService vacío")
        End If

        Me._secuenseDRepository = secuenceDRepository
        Me._managementAreasRepository = ManagementAreas
        Me._IUserAdminService = IUserAdminService
    End Sub

    ''' <summary>
    ''' Lista todas las areas de gestion autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Function ListManagementAreasByUserCode(userCode As String) As List(Of ManagementAreas) Implements IManagementAreasAdminService.ListManagementAreasByUserCode
        If String.IsNullOrEmpty(userCode) Then
            Throw New ArgumentNullException("userCode Vacio")
        End If

        Try
            Return _ManagementAreasRepository.ListManagementAreasByUserCode(userCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una autorizacion  del area de gestion
    ''' </summary>
    ''' <param name="ManagementAreas"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteManagementAreas(ManagementAreas As ManagementAreas, audit As AuditMessage) As ActionResult Implements IManagementAreasAdminService.DeleteManagementAreas
        If ManagementAreas Is Nothing Then
            Throw New ArgumentNullException("ManagementAreas")
        End If

        Dim unitOfWork As IUnitWork = Me._managementAreasRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                ManagementAreas.ModificationUser = audit.CodeUser
                ManagementAreas.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ManagementAreas)(ManagementAreas, audit, status)

                While ManagementAreas.ManagementAreasUser.Count > 0
                    ManagementAreas.ManagementAreasUser(ManagementAreas.ManagementAreasUser.Count - 1).MarkAsDeleted()
                End While

                ManagementAreas.MarkAsDeleted()
                Me._managementAreasRepository.SaveEntity(ManagementAreas)
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
    ''' Guarda o actualiza una autorización
    ''' </summary>
    ''' <param name="ManagementAreas">The ManagementAreas authorization.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="ArgumentNullException">distributionManpower</exception>
    Public Function SaveManagementAreas(ManagementAreas As ManagementAreas, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of ManagementAreas) Implements IManagementAreasAdminService.SaveManagementAreas
        If ManagementAreas Is Nothing Then
            Throw New ArgumentNullException("ManagementAreas")
        End If

        Dim unitOfWork As IUnitWork = Me._managementAreasRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(ManagementAreas.Code) Then
                    Dim seq As AccountManagementSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSecuence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.AccountManagementSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ManagementAreas.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ManagementAreas) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.AccountManagementSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), ManagementAreas.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ManagementAreas) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ManagementAreas = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ManagementAreas)
                Dim status As Integer

                If ManagementAreas.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ManagementAreas.CreationUser = audit.CodeUser
                    ManagementAreas.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = ManagementAreas.OriginalValue
                    ManagementAreas.ModificationUser = audit.CodeUser
                    ManagementAreas.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._managementAreasRepository.SaveEntity(ManagementAreas)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ManagementAreas)(ManagementAreas, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ManagementAreas.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ManagementAreas) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ManagementAreas, .Message = MessageResult}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ManagementAreas) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ManagementAreas) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una autorización del area de gestion id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManagementAreasById(id As Integer, audit As AuditMessage) As ActionResult(Of ManagementAreas) Implements IManagementAreasAdminService.GetManagementAreasById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim ManagementAreas As ManagementAreas = _managementAreasRepository.GetManagementAreasById(id)
            Return New ActionResult(Of ManagementAreas) With {.StateResult = True, .ObjectEmbbeded = ManagementAreas}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ManagementAreas) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una autorización de area de gestion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManagementAreasByCode(code As String, audit As AuditMessage) As ActionResult(Of ManagementAreas) Implements IManagementAreasAdminService.GetManagementAreasByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim ManagementAreas As ManagementAreas = _managementAreasRepository.GetManagementAreasByCode(code)

            'Se saca el listado de ids de usuario para enviar
            Dim ListUsers As New List(Of Domain.Security.Entities.User)
            'Se consulta los usuarios por id para agregarles la descripcion
            Dim ListUserIds As New List(Of Integer)
            If ManagementAreas IsNot Nothing AndAlso ManagementAreas.Id > 0 AndAlso ManagementAreas.ManagementAreasUser IsNot Nothing AndAlso ManagementAreas.ManagementAreasUser.Count > 0 Then
                'Se recorre el listado de usuarios que trae la autorizacion para sacar los ids
                For Each bu In ManagementAreas.ManagementAreasUser
                    ListUserIds.Add(bu.UserId)
                Next

                'Se obtiene el listado de usuarios
                ListUsers = _IUserAdminService.ListUsersByIds(ListUserIds)
                If ListUsers IsNot Nothing AndAlso ListUsers.Count > 0 Then
                    'Se asigna el nombre completo del usuario al listado que se va a asignar al datasource de la rejilla
                    For Each bu In ManagementAreas.ManagementAreasUser
                        Dim user = (From e In ListUsers Where e.Id = bu.UserId Select e).FirstOrDefault
                        If user IsNot Nothing AndAlso user.Id > 0 AndAlso user.Person IsNot Nothing Then
                            bu.FullNameUser = user.Person.Fullname
                        End If
                    Next
                End If
            End If

            Return New ActionResult(Of ManagementAreas) With {.StateResult = True, .ObjectEmbbeded = ManagementAreas}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ManagementAreas) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateManagementAreas(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ManagementAreas) Implements IManagementAreasAdminService.ChangeStateManagementAreas

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
            Dim ManagementAreas As ManagementAreas = Me._ManagementAreasRepository.GetManagementAreasByCode(code.Trim())
            If ManagementAreas IsNot Nothing AndAlso ManagementAreas.Id > 0 Then
                ManagementAreas.Status = state
            End If

            Dim result = Me.SaveManagementAreas(ManagementAreas, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If

            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ManagementAreas) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todas las Areas de Gestión
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllManagementAreas() As ActionResult(Of List(Of ManagementAreas)) Implements IManagementAreasAdminService.GetAllManagementAreas
        Dim managementAreasList = _managementAreasRepository.GetAllWithUsers()
        Return New ActionResult(Of List(Of ManagementAreas)) With {.StateResult = True, .ObjectEmbbeded = managementAreasList}
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="assignmentDate"></param>
    ''' <param name="managementAreaCode"></param>
    ''' <returns></returns>
    Public Function GetTimeStatus(assignmentDate As DateTime?, managementAreaCode As String) As Integer? Implements IManagementAreasAdminService.GetTimeStatus
        If Not assignmentDate.HasValue OrElse String.IsNullOrWhiteSpace(managementAreaCode) Then
            Return Nothing
        End If

        ' Parametrización del área
        Dim area = _managementAreasRepository _
        .GetByFilter(Function(ma) ma.Code = managementAreaCode) _
        .FirstOrDefault()
        If area Is Nothing Then
            Return Nothing
        End If


        Dim unit As Byte = area.Unit
        Dim spanCount As Integer = area.Time

        ' Calcular fecha límite
        Dim deadline As DateTime
        Select Case unit
            Case 1 ' horas
                deadline = assignmentDate.Value.AddHours(spanCount)
            Case 2 ' días
                deadline = assignmentDate.Value.AddDays(spanCount)
            Case 3 ' semanas
                deadline = assignmentDate.Value.AddDays(spanCount * 7)
            Case Else
                Return Nothing
        End Select

        Dim nowUtc = DateTime.UtcNow

        ' Si ya expiró
        If nowUtc > deadline Then
            Return 3
        End If

        ' ¿Cuánto tiempo queda en segundos?
        Dim remainingSec = (deadline - nowUtc).TotalSeconds

        ' Tiempo total en segundos
        Dim totalSec As Double = spanCount *
        If(unit = 1, 3600,
           If(unit = 2, 86400,
              spanCount * 7 * 86400))

        ' Si entra en el último 10%
        If remainingSec <= totalSec * 0.1 Then
            Return 2
        End If

        ' Aún le queda más del 10%
        Return 1
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _IUserAdminService.Dispose()
            End If
            _secuenseDRepository = Nothing
            _managementAreasRepository = Nothing
            _IUserAdminService = Nothing
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

