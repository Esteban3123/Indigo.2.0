'***********************************************************************
' Assembly         : Application.Security
' Author           : Jorge Leonardo Vernaza     
' Created          : 01-08-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Security
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Security
Imports Infrastructure.CrossCutting.Base
Imports DistributedServices.Authentication
Imports System.Threading.Tasks

#End Region

Public Class PermissionAdminCompany
    Implements IPermissionAdminCompany

    Private _PermissionCompanyRepository As IPermissionCompanyRepository
    Private _userMembershipRepository As IUserMembershipRepository
    Private _userRepository As IUserRepository
    Private _containerRepository As IContainersRepository

    Public Sub New(ByVal PermissionCompanyRepository As IPermissionCompanyRepository, ByVal UserMembershipRepository As IUserMembershipRepository, ByVal UserRepository As IUserRepository, containerRepository As IContainersRepository)
        If PermissionCompanyRepository Is Nothing Then
            Throw New ArgumentNullException("PermissionCompanyRepository Vacio")
        End If
        If UserMembershipRepository Is Nothing Then
            Throw New ArgumentNullException("UserMembershipRepository Vacio")
        End If
        If UserRepository Is Nothing Then
            Throw New ArgumentNullException("UserRepository Vacio")
        End If
        If containerRepository Is Nothing Then
            Throw New ArgumentNullException("containerRepository Vacio")
        End If
        _PermissionCompanyRepository = PermissionCompanyRepository
        _userMembershipRepository = UserMembershipRepository
        _userRepository = UserRepository
        _containerRepository = containerRepository
    End Sub

    Public Function SavePermissionCompany(PermissionCompany As List(Of PermissionCompany), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IPermissionAdminCompany.SavePermissionCompany
        If PermissionCompany Is Nothing Then
            Throw New ArgumentNullException("Permission Company Vacio")
        End If

        Dim unitOfWork As IUnitWork = _PermissionCompanyRepository.UnitWork
        Try
            For Each Permission As PermissionCompany In PermissionCompany
                _PermissionCompanyRepository.SaveEntity(Permission)
            Next
            'confirmo la unidad de trabajo
            unitOfWork.Commit()
            Return True
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Lists the permissions companies.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPermissionsCompanies(ByVal UserCode As String) As List(Of PermissionCompany) Implements IPermissionAdminCompany.ListPermissionsCompanies
        Try
            Return _PermissionCompanyRepository.ListPermissionCompanyAll(UserCode)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Metodo para saber si el usuario tiene permiso para la empresa seleccionada
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <param name="CompanyCode"></param>
    ''' <returns></returns>
    Public Function GetPermissionUserCompany(UserCode As String, UserPass As String, CompanyCode As String) As ActionResult(Of User) Implements IPermissionAdminCompany.GetPermissionUserCompany
        Try
            Dim control = False
            'Dim result As New ActionResult(Of User)
            'If Not _userMembershipRepository.ValidateUser(UserCode, UserPass) Then
            '    Return New ActionResult(Of User) With {.ObjectEmbbeded = Nothing, .StateResult = False, .MessageResult = {"-1"}.ToList()}
            'End If
            Dim userAux As User = _userRepository.GetUserPersonFileByCode(UserCode)
            If userAux Is Nothing Then 'El usuario no existe
                Return New ActionResult(Of User) With {.ObjectEmbbeded = userAux, .StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioNoExiste"), UserCode)}
            ElseIf userAux.IsLockedOut = True Then 'Si el usuario esta bloqueado
                Return New ActionResult(Of User) With {.ObjectEmbbeded = userAux, .StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioBloqueado"), UserCode)}
            ElseIf userAux.Password <> IndigoRijndael.Encrypt(UserPass) Then 'Contraseña Incorrecta
                Dim result As ActionResult(Of User)
                userAux.FailedPasswordCount += 1
                If userAux.FailedPasswordCount >= 3 Then
                    userAux.IsLockedOut = True
                    result = New ActionResult(Of User) With {.ObjectEmbbeded = userAux, .StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioBloqueado"), UserCode)}
                Else
                    result = New ActionResult(Of User) With {.ObjectEmbbeded = userAux, .StateResult = False, .Message = String.Format(ResourceManager.GetString("PasswordIncorrecto"), userAux.FailedPasswordCount)}
                End If
                userAux.MarkAsModified()
                _userRepository.SaveEntity(userAux)
                _userRepository.UnitWork.Commit()
                Return result
            ElseIf userAux.State = 0 Then 'Si el usuario esta inactivo
                Return New ActionResult(Of User) With {.ObjectEmbbeded = userAux, .StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioInactivo"), UserCode)}
            ElseIf userAux.DateExpiryAccount IsNot Nothing AndAlso Date.Now > userAux.DateExpiryAccount.Value Then 'Si ya expiro la cuenta
                Return New ActionResult(Of User) With {.ObjectEmbbeded = userAux, .StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioCaducado"), UserCode)}
            ElseIf Not _PermissionCompanyRepository.GetPermissionUserCompany(UserCode, CompanyCode) Then 'Notiene permisos a la empresa
                Return New ActionResult(Of User) With {.ObjectEmbbeded = Nothing, .StateResult = False, .Message = ResourceManager.GetString("ComunesUsuarioSinPermisosEmpresa")}
            Else
                userAux.FailedPasswordCount = 0
                userAux.MarkAsModified()
                _userRepository.SaveEntity(userAux)
                _userRepository.UnitWork.Commit()
                Return New ActionResult(Of User) With {.ObjectEmbbeded = userAux, .StateResult = True}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of User) With {.StateResult = False, .Message = ResourceManager.GetString("UsuarioError")}
        End Try
    End Function

    Public Function LoginUserCompany(userCode As String, userPasswd As String, companyCode As String, ByVal appVersion As Version) As ActionResult(Of UserLogin) Implements IPermissionAdminCompany.LoginUserCompany
        Try
            Dim usr As UserLogin = _PermissionCompanyRepository.LoginUserCompany(userCode, companyCode)
            Dim comp As Containers = _containerRepository.getContainersByCode(companyCode)

            If comp Is Nothing OrElse comp.Id = 0 Then
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = "El contenedor " & companyCode & " no existe"}
#If Not DEBUG Then
            ElseIf Not comp.Version.Trim().Equals(String.Empty) Then
                Try
                    Dim vDbSchema As Version = New Version(comp.Version)
                    If Not vDbSchema.Equals(appVersion) Then
                        Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = ("La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde la versión de éste cliente (" & appVersion.ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo.")}
                    End If
                Catch ex As Exception
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = ("La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde la versión de éste cliente (" & appVersion.ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo.")}
                End Try
            Else
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = ("La versión del esquema en la empresa " & comp.Code & " - " & comp.Name & ", NO corresponde la versión de éste cliente (" & appVersion.ToString() & ")." & vbCrLf & "Porfavor actualice la versión del cliente e intente de nuevo.")}
#End If
            End If
#If Not DEBUG Then
            Dim userAux As User = _userRepository.GetUserPersonFileByCode(userCode)
#End If
            If Not usr.UserExists Then
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioNoExiste"), userCode)}
            ElseIf usr.IsLockedOut Then
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioBloqueado"), userCode)}
#If Not DEBUG Then
            ElseIf usr.Password <> IndigoRijndael.Encrypt(userPasswd) Then
                userAux.FailedPasswordCount += 1
                If userAux.FailedPasswordCount >= 3 Then
                    userAux.IsLockedOut = True
                End If
                userAux.MarkAsModified()
                _userRepository.SaveEntity(userAux)
                _userRepository.UnitWork.Commit()
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = ResourceManager.GetString("UsuarioError")}
#End If
            ElseIf Not usr.State Then
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioInactivo"), userCode)}
            ElseIf usr.DateExpiryAccount IsNot Nothing AndAlso Date.Now > usr.DateExpiryAccount Then
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioCaducado"), userCode)}
            ElseIf usr.CompanyCode Is Nothing OrElse Not usr.CompanyPermission Then
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = ResourceManager.GetString("ComunesUsuarioSinPermisosEmpresa")}
            ElseIf CType(usr.UserType, UserType) = UserType.GlobalAdmin AndAlso Not usr.Email.Contains("@indigo.tech") Then
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioGlobalNoValido"), userCode)}
            ElseIf CType(usr.UserType, UserType) = UserType.GlobalQa AndAlso Not usr.Email.Contains("@indigo.tech") Then
                Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("UsuarioGlobalNoValido"), userCode)}
            Else
                usr.ListPermission = _userRepository.ListPermissionFormsUser(usr.TenantId, usr.RollCode, userCode, "41")
                usr.ListProductCatalog = _userRepository.ListPermissionFormsUserV2(usr.TenantId, usr.RollCode, userCode, "41")

                Dim token = JwtFactory.GenerateToken(usr.UserCode, "INDIGO999")
                Task.WaitAll(token)
                usr.AccessToken = token.Result

                Return New ActionResult(Of UserLogin) With {.ObjectEmbbeded = usr, .StateResult = True}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of UserLogin) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PermissionCompanyRepository = Nothing
            _userMembershipRepository = Nothing
            _userRepository = Nothing
            _containerRepository = Nothing
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
