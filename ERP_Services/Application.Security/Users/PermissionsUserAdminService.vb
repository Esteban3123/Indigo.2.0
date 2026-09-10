'***********************************************************************
' Assembly         : Application.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Security

#End Region

''' <summary>
''' 	
''' </summary>
Public Class PermissionsUserAdminService
    Implements IPermissionsUserAdminService

    Dim _userPermissionsRepository As IRepository(Of PermissionUser)
    Dim _userRepository As IUserRepository
    Dim _userMembershipRepository As IUserMembershipRepository
    Dim _personRepository As IPersonRepository
    Dim _usersGroupUserRepository As IUsersGroupUserRepository
    Dim _roleRepository As IRoleRepository
    Dim _groupRepository As IGroupRepository
    Dim _groupUserRepository As IGroupUserRepository
    Dim _PermissionCompanyRepository As IPermissionCompanyRepository
    Dim _TenantUsersRepository As IRepository(Of TenantUsers)
    Dim _PermissionUserRepository As IRepository(Of PermissionUser)
    Dim _UserOperatingUnitRepository As IRepository(Of UserOperatingUnit)
    Dim _UserConfigurationRepository As IUserConfigurationRepository

    'EHR
    Dim _sEGroleRepository As ISEGrolesuRepository
    Dim _sEGgruusuRepository As ISEGgruusuRepository
    Dim _sEGusuaruRepository As ISEGusuaruRepository
    Dim _SEGpermiuRepository As ISEGpermiuRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="PermissionsUserAdminService" />.
    ''' </summary>
    ''' <param name="userPermissionsRepository">el repositorio para el manejo de los permisos de usuarios.</param>
    ''' <param name="userRepository">el repositorio para el manejo de los usuarios</param>
    ''' <param name="personRepository"></param>
    ''' <param name="userMembershipRepository">el repositorio para el manejo de los usuarios en membership</param>
    ''' <param name="usersGroupUserRepository"></param>
    ''' <param name="roleRepository"></param>
    ''' <param name="groupRepository"></param>
    ''' <param name="groupUserRepository"></param>
    ''' <param name="sEGroleRepository"></param>
    ''' <param name="sEGgruusuRepository"></param>
    ''' <param name="sEGusuaruRepository"></param>
    ''' <param name="SEGpermiuRepository"></param>
    Public Sub New(ByVal userPermissionsRepository As IRepository(Of PermissionUser), ByVal userRepository As IUserRepository, personRepository As IPersonRepository, ByVal userMembershipRepository As IUserMembershipRepository, ByVal usersGroupUserRepository As IUsersGroupUserRepository,
                   ByVal roleRepository As IRoleRepository, groupRepository As IGroupRepository, ByVal groupUserRepository As IGroupUserRepository,
                   ByVal sEGroleRepository As ISEGrolesuRepository, ByVal sEGgruusuRepository As ISEGgruusuRepository, ByVal sEGusuaruRepository As ISEGusuaruRepository,
                   SEGpermiuRepository As ISEGpermiuRepository, PermissionCompanyRepository As IPermissionCompanyRepository, TenantUsersRepository As IRepository(Of TenantUsers),
                   PermissionUserRepository As IRepository(Of PermissionUser), UserOperatingUnitRepository As IRepository(Of UserOperatingUnit),
                   UserConfigurationRepository As IUserConfigurationRepository)
        If userPermissionsRepository Is Nothing Then
            Throw New ArgumentNullException("userPermissionsRepository Vacio")
        End If
        If userRepository Is Nothing Then
            Throw New ArgumentNullException("userRepository Vacio")
        End If
        If userMembershipRepository Is Nothing Then
            Throw New ArgumentNullException("userMembershipRepository Vacio")
        End If

        _userRepository = userRepository
        _userPermissionsRepository = userPermissionsRepository
        _userMembershipRepository = userMembershipRepository
        _personRepository = personRepository
        _usersGroupUserRepository = usersGroupUserRepository
        _roleRepository = roleRepository
        _groupRepository = groupRepository
        _groupUserRepository = groupUserRepository
        _PermissionCompanyRepository = PermissionCompanyRepository
        _TenantUsersRepository = TenantUsersRepository
        _PermissionUserRepository = PermissionUserRepository
        _UserOperatingUnitRepository = UserOperatingUnitRepository
        _UserConfigurationRepository = UserConfigurationRepository

        _sEGroleRepository = sEGroleRepository
        _sEGgruusuRepository = sEGgruusuRepository
        _sEGusuaruRepository = sEGusuaruRepository
        _SEGpermiuRepository = SEGpermiuRepository
    End Sub

    ''' <summary>
    ''' Deletes the user.	
    ''' </summary>
    ''' <param name="user">The user.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteUser(user As User, session As SessionValues, audit As AuditMessage) As ActionResult Implements IPermissionsUserAdminService.DeleteUsers
        'If user.PermissionUser Is Nothing Then
        '    Throw New ArgumentNullException("permissionUser Vacio")
        'End If
        If user Is Nothing Then
            Throw New ArgumentNullException("user Vacio")
        End If
        'If user.PermissionCompany Is Nothing Then
        '    Throw New ArgumentNullException("PermissionCompany Vacio")
        'End If
        'Dim _ListUsersGroupUser As List(Of UsersGroupUser) = Nothing

        Dim unitWorkUser As IUnitWork = TryCast(_userRepository.UnitWork, IUnitWork)
        'Dim unitWorkUserGroupUser As IUnitWork = TryCast(_usersGroupUserRepository.UnitWork, IUnitWork)
        'Dim unitWorkgroupUser As IUnitWork = TryCast(_groupUserRepository.UnitWork, IUnitWork)

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required)


                'Dim UserGroupUser As List(Of UsersGroupUser) = _usersGroupUserRepository.ListUserInGroupsById(user.Id)
                'If user.GroupUser IsNot Nothing AndAlso user.GroupUser.Count > 0 AndAlso user.GroupUser.Any(Function(_GroupUser) _GroupUser.UsersGroupUser IsNot Nothing AndAlso _GroupUser.UsersGroupUser.Count > 0) Then
                '    _ListUsersGroupUser = From _GroupUser In user.GroupUser.Where(Function(_GroupUser) _GroupUser.UsersGroupUser IsNot Nothing AndAlso _GroupUser.UsersGroupUser.Count > 0)
                '                          From _UsersGroupUser In _GroupUser.UsersGroupUser
                '                          Select _UsersGroupUser
                'End If
                Dim UserConfig As UserConfiguration = _UserConfigurationRepository.GetUserConfigurationByUserId(user.Id)

                'For I = 0 To UserGroupUser.Count - 1
                '    _usersGroupUserRepository.DeleteEntity(UserGroupUser(I))
                'Next

                'unitWorkUserGroupUser.Commit()

                'elimino los permisos de usuario
                'For I = 0 To user.PermissionUser.Count - 1
                '    user.PermissionUser(0).ChangeTracker.State = ObjectState.Deleted
                'Next

                'For I = 0 To user.PermissionCompany.Count - 1
                '    user.PermissionCompany(0).ChangeTracker.State = ObjectState.Deleted
                'Next

                'For I = 0 To user.GroupUser.Count - 1
                '    For j = 0 To user.GroupUser(I).UsersGroupUser.Count - 1
                '        user.GroupUser(I).UsersGroupUser(0).ChangeTracker.State = ObjectState.Deleted
                '    Next
                'Next

                'For I = 0 To user.GroupUser.Count - 1
                '    user.GroupUser(0).ChangeTracker.State = ObjectState.Deleted
                'Next

                'user.ChangeTracker.State = ObjectState.Deleted
                '_userRepository.SaveEntity(user)
                '_usersGroupUserRepository.DeleteList(UserGroupUser)
                _PermissionUserRepository.DeleteList(user.PermissionUser.ToList)
                _PermissionCompanyRepository.DeleteList(user.PermissionCompany.ToList)
                'If _ListUsersGroupUser IsNot Nothing Then
                '    _usersGroupUserRepository.DeleteList(_ListUsersGroupUser)
                'End If
                '_usersGroupUserRepository.DeleteList(UserGroupUser)
                '_groupUserRepository.DeleteList(user.GroupUser.ToList)
                _TenantUsersRepository.DeleteList(user.TenantUsers.ToList)
                _UserOperatingUnitRepository.DeleteList(user.UserOperatingUnit.ToList)
                If UserConfig IsNot Nothing AndAlso UserConfig.Id > 0 Then
                    _UserConfigurationRepository.DeleteEntity(UserConfig)
                End If
                user.State = False

                '_userRepository.DeleteEntity(user)

                'elimino el usuario en membership
                'If _userMembershipRepository.DeleteUser(user.UserCode.Trim) = False Then
                'devuelvo cambios permisos
                'unitWorkUser.RollbackChanges()
                'Return False
                'End If
                'confirmo cambios usuarios

                Dim auxuser As User = Nothing
                Dim auditprocess As IndigoAuditSimpleEntity(Of User)
                Dim status As Integer

                user.ModificationUser = audit.CodeUser
                user.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Delete

                _userRepository.SaveEntity(user)
                unitWorkUser.Commit()
                auditprocess = New IndigoAuditSimpleEntity(Of User)(user, audit, status, auxuser)
                auditprocess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            'devuelvo cambios permisos
            unitWorkUser.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Saves the permissions user.	
    ''' </summary>
    ''' <param name="user">The user.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePermissionsUser(user As User, SessionValues As SessionValues, audit As AuditMessage) As ActionResult Implements IPermissionsUserAdminService.SavePermissionsUser
        If user Is Nothing Then
            Throw New ArgumentNullException("user Vacio")
        End If

        Dim unitWorkUser As IUnitWork = TryCast(_userRepository.UnitWork, IUnitWork)
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted
            Dim _ListPermissionUserDelete As List(Of PermissionUser) = Nothing
            If user.PermissionUser IsNot Nothing AndAlso user.PermissionUser.Any(Function(_PermissionUser) Not _PermissionUser.ActionValue) Then
                Dim linq = From _PermissionUser In user.PermissionUser.Where(Function(_PermissionUser) Not _PermissionUser.ActionValue)
                _ListPermissionUserDelete = linq.ToList()
                _ListPermissionUserDelete.ForEach(Sub(_PermissionUser) _PermissionUser.MarkAsDeleted())
            End If
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                user.Roll = Nothing
                If user.ChangeTracker.State = ObjectState.Added Then
                    'encripto la contraseña del usuario
                    user.Password = IndigoRijndael.Encrypt(user.Password)
                End If
                'If user.TenantId = 0 Then
                '    user.TenantId = Nothing
                'End If
                If _ListPermissionUserDelete IsNot Nothing Then
                    _PermissionUserRepository.DeleteList(_ListPermissionUserDelete)
                End If

                Dim auxuser As User = Nothing
                Dim auditprocess As IndigoAuditSimpleEntity(Of User)
                Dim status As Integer

                Dim isNewUser As Boolean = user.ChangeTracker.State = ObjectState.Added

                If isNewUser Then
                    If SessionValues.ArchitectureType <> 2 Then ' ON PREMISE: el UserCode se asigna con la cédula antes del INSERT
                        Dim query = _userRepository.FirstOrDefault(Function(item) item.UserCode = user.Person.Identification, False)

                        If query IsNot Nothing AndAlso query.Id > 0 Then
                            Throw New Exception("Código de usuario duplicado")
                        End If

                        user.UserCode = user.Person.Identification
                    End If

                    user.CreationUser = audit.CodeUser
                    user.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    'auxUser = user.ori
                    user.ModificationUser = audit.CodeUser
                    user.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                _userRepository.SaveEntity(user)
                unitWorkUser.Commit()
                auditprocess = New IndigoAuditSimpleEntity(Of User)(user, audit, status, auxuser)
                auditprocess.Execute()

                ' PAAS: el UserCode se asigna después del Commit() porque solo entonces la BD genera el Id autonumérico real
                If SessionValues.ArchitectureType = 2 AndAlso isNewUser Then
                    user.UserCode = user.Id.ToString()
                    _userRepository.SaveEntity(user)
                    unitWorkUser.Commit()
                End If

                'EHR
                Dim rol = _roleRepository.GetRoleId(user.RollCode)
                Dim rolCrystal = _sEGroleRepository.GetRolCrystal(rol.RollCode)
                If rolCrystal Is Nothing Then
                    rolCrystal = New Domain.Crystal.Entities.SEGrolesu()
                    rolCrystal.codigorol = rol.RollCode
                    rolCrystal.descrirol = rol.Description
                    _sEGroleRepository.SaveEntity(rolCrystal)
                    _sEGroleRepository.UnitWork.Commit()
                End If

                Dim group = _groupRepository.GetGroupById(user.GroupCode)
                Dim groupCrystal = _sEGgruusuRepository.GetGrupoCrystal(group.Code)
                If groupCrystal Is Nothing Then
                    groupCrystal = New Domain.Crystal.Entities.SEGgruusu()
                    groupCrystal.codgrupou = group.Code
                    groupCrystal.descrigru = group.Description
                    _sEGgruusuRepository.SaveEntity(groupCrystal)
                    _sEGgruusuRepository.UnitWork.Commit()
                End If

                Dim newPasswd As String = IndigoRijndael.Decrypt(user.Password)
                Dim userCrystal = _sEGusuaruRepository.GetusuarioCrystal(user.UserCode)
                If userCrystal Is Nothing Then
                    userCrystal = New Domain.Crystal.Entities.SEGusuaru
                    userCrystal.CODUSUARI = user.UserCode
                    userCrystal.PASSUSUAR = IndigoEncriptar.EncriptarPassword(newPasswd)
                    userCrystal.CODUSUDGH = user.CodeInterface
                    userCrystal.USUACTIVO = user.State
                    userCrystal.DESCARUSU = user.Position
                    userCrystal.SOLCAMCON = 0
                    userCrystal.FECULTCAM = DateTime.Now
                Else
                    _sEGusuaruRepository.ChangePassword(user.UserCode, IndigoEncriptar.EncriptarPassword(newPasswd))
                End If

                userCrystal.CODIGOROL = rol.RollCode
                userCrystal.CODGRUPOU = group.Code
                If user.Person IsNot Nothing Then
                    userCrystal.NOMUSUARI = user.Person.Fullname
                    'If user.Person.Email IsNot Nothing AndAlso user.Person.Email.Any() Then
                    '    userCrystal.USUEMAILE = user.Person.Email.FirstOrDefault().Email1
                    'End If
                End If
                userCrystal.USUEMAILE = user.Email
                userCrystal.TIPPERUSU = user.ProfileType
                userCrystal.USUADMINI = If(user.UserType = "0", False, True)
                _sEGusuaruRepository.SaveEntity(userCrystal)
                _sEGusuaruRepository.UnitWork.Commit()

                'confirmo la transaccion
                scope.Complete()
            End Using

            Return New ActionResult With {.StateResult = True, .MessageResult = New List(Of String)(New String() {"USERID:" + user.Id.ToString()})}
        Catch ex As Exception
            'devuelvo cambios usuario
            unitWorkUser.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String) From {Utils.GetInnerExceptionMessageToString(ex)}}
        End Try
    End Function

#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <returns></returns>
    Public Function GetPermisoUsuario(idMenu As String, codigoUsuario As String) As ActionResult(Of SEGpermiu) Implements IPermissionsUserAdminService.GetPermisoUsuario
        Try
            Return _SEGpermiuRepository.GetPermisoUsuario(idMenu, codigoUsuario)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SEGpermiu) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = ex.Message}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _userRepository = Nothing
            _userPermissionsRepository = Nothing
            _userMembershipRepository = Nothing
            _personRepository = Nothing
            _usersGroupUserRepository = Nothing
            _groupUserRepository = Nothing
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
