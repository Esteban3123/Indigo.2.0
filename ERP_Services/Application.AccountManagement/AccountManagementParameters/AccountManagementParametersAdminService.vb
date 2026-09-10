'***********************************************************************
' Assembly         : Presentacion.AccoungManagement
' Author           : Andres Felipe Quintero Garcia 
' Created          : 09-01-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Data.Entity.Core
#End Region

Public Class AccountManagementParametersAdminService
    Implements IAccountManagementParametersAdminService, Inject

#Region "Fields"
    Private ReadOnly _accountManagementParametersRepository As IAccountManagementParametersRepository
    Private ReadOnly _usersAssignmentRepository As IUsersAssignmentRepository
    Private ReadOnly _userNoveltiesRepository As IUserNoveltiesRepository
#End Region

#Region "Builder"
    Public Sub New(ByVal repository As Domain.Entities.IAccountManagementParametersRepository, ByVal userAssignmentRepository As IUsersAssignmentRepository, ByVal userNovelties As IUserNoveltiesRepository)
        If repository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _accountManagementParametersRepository = repository
        _usersAssignmentRepository = userAssignmentRepository
        _userNoveltiesRepository = userNovelties
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una autorización del area de gestion id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountManagementParametersById(id As Integer) As ActionResult(Of AccountManagementParameters) Implements IAccountManagementParametersAdminService.GetAccountManagementParametersById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim accountManagementParameters As Domain.Entities.AccountManagementParameters = Me._accountManagementParametersRepository.GetAccountManagementParametersById(id)
            Return New ActionResult(Of Domain.Entities.AccountManagementParameters) With {.StateResult = True, .ObjectEmbbeded = accountManagementParameters}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountManagementParameters) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorización
    ''' </summary>
    ''' <param name="accountManagementParameters"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAccountManagementParameters(accountManagementParameters As AccountManagementParameters, audit As AuditMessage) As Task(Of ActionResult(Of AccountManagementParameters)) Implements IAccountManagementParametersAdminService.SaveAccountManagementParameters
        Dim UnitOfWork As IUnitWork = _accountManagementParametersRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of AccountManagementParameters)
            Dim status As Integer
            Dim informativeMessages As New List(Of String)

            'Guarda UsersAssignment separadamente
            If accountManagementParameters.usersAssignment IsNot Nothing Then
                Dim validationMessage = Await SaveUsersAssignment(accountManagementParameters)
                If Not String.IsNullOrEmpty(validationMessage) Then
                    informativeMessages.Add(validationMessage)
                End If
            End If

            'Guarda UserNovelties separadamente  
            If accountManagementParameters.userNovelties IsNot Nothing Then
                SaveUserNovelties(accountManagementParameters)
            End If


            If accountManagementParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                accountManagementParameters.CreationDate = DateTime.Now
                accountManagementParameters.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                accountManagementParameters.ModificationDate = DateTime.Now
                accountManagementParameters.ModificationUser = audit.CodeUser


                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._accountManagementParametersRepository.SaveEntity(accountManagementParameters)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of AccountManagementParameters)(accountManagementParameters, audit, status)
            auditProcess.Execute()
            accountManagementParameters.MarkAsUnchanged()

            ' Devolver resultado exitoso con mensajes informativos si los hay
            If informativeMessages.Count > 0 Then
                Return New ActionResult(Of AccountManagementParameters) With {.StateResult = True, .ObjectEmbbeded = accountManagementParameters, .MessageResult = informativeMessages}
            Else
                Return New ActionResult(Of AccountManagementParameters) With {.StateResult = True, .ObjectEmbbeded = accountManagementParameters}
            End If
        Catch ex As OptimisticConcurrencyException
            If UnitOfWork IsNot Nothing Then UnitOfWork.RollbackChanges()
            Return New ActionResult(Of AccountManagementParameters) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            If UnitOfWork IsNot Nothing Then UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountManagementParameters) With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Guarda la lista de UsersAssignment ligada a accountManagementParameters 
    ''' </summary>
    ''' <param name="accountManagementParameters"></param>
    ''' <returns>Mensaje informativo si hay usuarios con distribuciones que no se pueden eliminar, String.Empty si todo está bien</returns>
    Private Async Function SaveUsersAssignment(accountManagementParameters As AccountManagementParameters) As Task(Of String)


        ' Obtener los IDs de los usuarios con ingresos distribuidos
        Dim userWithDistributedAccounts = Await _accountManagementParametersRepository.GetUsersWithDistributedAccounts()
        Dim userIdsWithDistributedAccounts = userWithDistributedAccounts.Select(Function(u) u.Id).ToList()


        ' Verificar si hay usuarios con distribuciones que se están intentando eliminar (soft delete)
        Dim usersToDeleteWithDistributions = accountManagementParameters.usersAssignment _
                                            .Where(Function(u) u.IsRemoved = True AndAlso u.ChangeTracker.State = ObjectState.Modified AndAlso userIdsWithDistributedAccounts.Contains(u.Id)) _
                                            .ToList()

        ' Revertir el IsRemoved de los usuarios que NO se pueden eliminar porque tienen distribuciones
        If usersToDeleteWithDistributions.Count > 0 Then
            For Each userAssign As UsersAssignment In usersToDeleteWithDistributions
                userAssign.IsRemoved = False
            Next
        End If

        ' Guardar todos los registros que tienen cambios (Added, Modified)
        ' Esto incluye tanto los que tienen IsRemoved = True como los que tienen IsRemoved = False
        Dim toSave = accountManagementParameters.usersAssignment _
                        .Where(Function(u) u.ChangeTracker.State <> ObjectState.Deleted AndAlso u.ChangeTracker.State <> ObjectState.Unchanged) _
                        .ToList()

        If toSave IsNot Nothing AndAlso toSave.Count > 0 Then
            For Each userAssign As UsersAssignment In toSave
                _usersAssignmentRepository.SaveEntity(userAssign)
            Next
        End If

        ' Si hay usuarios que NO se pudieron eliminar, devolver mensaje informativo
        If usersToDeleteWithDistributions.Count > 0 Then
            Dim userNames = String.Join(", ", usersToDeleteWithDistributions.Select(Function(u) u.FullName))
            Return $"Los usuarios: {userNames} no pudieron ser eliminados porque cuentan con ingresos o folios distribuidos."
        End If

        ' Si llegamos aquí, todo salió bien
        Return String.Empty
    End Function

    ''' <summary>
    ''' Guarda la lista de UserNovelties ligada a accountManagementParameters 
    ''' </summary>
    ''' <param name="accountManagementParameters"></param>
    Private Sub SaveUserNovelties(accountManagementParameters As AccountManagementParameters)
        If accountManagementParameters.userNovelties IsNot Nothing AndAlso accountManagementParameters.userNovelties.Count > 0 Then

            For Each novelty As UserNovelties In accountManagementParameters.userNovelties
                ' Solo guardar si tiene cambios
                If novelty.ChangeTracker.State <> ObjectState.Unchanged Then
                    _userNoveltiesRepository.SaveEntity(novelty)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la lista de usuarios activos para asignación de ingresos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAvailableUsers() As ActionResult(Of List(Of UsersAssignment)) Implements IAccountManagementParametersAdminService.GetAvailableUsers
        Dim query = _accountManagementParametersRepository.GetAvailableUsers()
        If (Not query.Any()) Then
            Return New ActionResult(Of List(Of UsersAssignment)) With {.StateResult = False, .MessageResult = {"No se encontraron usuarios activos"}.ToList()}
        End If
        Return New ActionResult(Of List(Of UsersAssignment)) With {.StateResult = True, .ObjectEmbbeded = query}
    End Function

    ''' <summary>
    ''' Obtiene la lista de usuarios con ingresos y/o folios distribuidos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetUsersWithDistributedAccounts() As Task(Of ActionResult(Of List(Of UsersAssignment))) Implements IAccountManagementParametersAdminService.GetUsersWithDistributedAccounts
        Dim query = Await _accountManagementParametersRepository.GetUsersWithDistributedAccounts()
        If (Not query.Any()) Then
            Return New ActionResult(Of List(Of UsersAssignment)) With {.StateResult = False, .MessageResult = {"No se encontraron usuarios con ingresos distribuidos"}.ToList()}
        End If
        Return New ActionResult(Of List(Of UsersAssignment)) With {.StateResult = True, .ObjectEmbbeded = query}
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' Liberar recursos administrados

            End If

            ' TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            ' TODO: establecer los campos grandes como NULL
            disposedValue = True
        End If
    End Sub

    ' ' TODO: reemplazar el finalizador solo si "Dispose(disposing As Boolean)" tiene código para liberar los recursos no administrados
    ' Protected Overrides Sub Finalize()
    '     ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
