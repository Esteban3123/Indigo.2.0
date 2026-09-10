'***********************************************************************
' Assembly         : DistributedService.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.IOC
Imports Application.Security
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Configuration
Imports Domain.Crystal.Entities
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports Microsoft.IdentityModel.Tokens
Imports DistributedServices.Authentication
#End Region

Partial Public Class SecurityService

    ''' <summary>
    ''' Lista los tipos de telefono
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ListAllPhoneType(session As SessionValues) As List(Of PhoneType) Implements ISecurityService.ListAllPhoneType
        Using AdminSecurity As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return AdminSecurity.ListAllPhoneType()
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado del paciente
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado</param>
    ''' <returns></returns>
    Public Function UpdateStatusUser(Id As String, Status As Boolean, audit As AuditMessage) As ActionResult Implements ISecurityService.UpdateStatusUser
        Using AdminSecurity As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return AdminSecurity.UpdateStatusUser(Id, Status, audit)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para restaurar la contraseña de un usuario
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResetPassword(ByVal UserCode As String) As String Implements Security.ISecurityService.ResetPassword
        Using AdminSecurity As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return AdminSecurity.ResetPassword(UserCode)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para cambier al modo de visualizacion de los frontales
    ''' True=Modo Busqueda
    ''' False=Modo Edicion
    ''' </summary>
    ''' <param name="ViewMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeUserViewMode(ByVal UserCode As String, ByVal ViewMode As Boolean) As Boolean Implements ISecurityService.ChangeUserViewMode
        Using AdminSecurity As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return AdminSecurity.ChangeUserViewMode(UserCode, ViewMode)
        End Using
    End Function

    ''' <summary>
    ''' Saves the permissions companies.
    ''' </summary>
    ''' <param name="PermissionCompanies">The permission companies.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Public Function SavePermissionsCompanies(PermissionCompanies As List(Of PermissionCompany), session As SessionValues) As Boolean Implements ISecurityService.SavePermissionsCompanies
        Using permissionsComapanies As IPermissionAdminCompany = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionAdminCompany)()
            Return permissionsComapanies.SavePermissionCompany(PermissionCompanies, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista los permisos de las empresas.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCompaniesPermission(ByVal UserCode As String, session As SessionValues) As IEnumerable(Of PermissionCompany) Implements ISecurityService.ListCompaniesPermission
        Using AdminSecurity As IPermissionAdminCompany = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionAdminCompany)()
            Return AdminSecurity.ListPermissionsCompanies(UserCode)
        End Using
    End Function

    ''' <summary>
    ''' Metodo para obtener si el usuario tiene permisos para la empresa seleccionada
    ''' </summary>
    ''' <param name="UserCode">The user code.</param>
    ''' <param name="CompanyCode">The company code.</param>
    ''' <returns></returns>
    Public Function GetPermissionUserCompany(ByVal UserCode As String, ByVal UserPass As String, CompanyCode As String, session As SessionValues) As ActionResult(Of User) Implements ISecurityService.GetPermissionUserCompany
        Using permissionsComapanies As IPermissionAdminCompany = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionAdminCompany)()
            Return permissionsComapanies.GetPermissionUserCompany(UserCode, UserPass, CompanyCode)
        End Using
    End Function

    Public Function LoginUserCompany(userCode As String, userPasswd As String, companyCode As String, ByVal appVersion As Version) As Domain.Base.Entities.ActionResult(Of UserLogin) Implements ISecurityService.LoginUserCompany
        Using permissionsComapanies As IPermissionAdminCompany = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionAdminCompany)()
            Return permissionsComapanies.LoginUserCompany(userCode, userPasswd, companyCode, appVersion)
        End Using
    End Function

    ''' <summary>
    ''' funcion para listar si el usuario tiene o no permisos para abrir el formulario como emergente
    ''' </summary>
    ''' <returns></returns>
    Function ListPermissionsUserForm(CodeUser As String, CodeMenu As String, CodeOpcion As String, session As SessionValues) As Boolean Implements ISecurityService.ListPermissionsUserForm
        Using AdminSecurity As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return AdminSecurity.ListPermissionsUserForm(CodeUser, CodeMenu, CodeOpcion, session.TenantId)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los ids de los formularios con permiso de visibilidad,
    ''' asignados al usuario y al rol que tiene el usuario
    ''' </summary>
    ''' <param name="tenantId">Id de tenant</param>
    ''' <param name="rollCode">Código de rol</param>
    ''' <param name="codeUser">Código del usuario</param>
    ''' <returns>Lista de ids de formularios</returns>
    Public Function ListPermissionFormsUser(tenantId As Short, rollCode As String, codeUser As String, action As String) As List(Of String) Implements ISecurityService.ListPermissionFormsUser
        Using AdminSecurity As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return AdminSecurity.ListPermissionFormsUser(tenantId, rollCode, codeUser, action)
        End Using
    End Function

    Public Function GetFieldsNULLUsers(session As SessionValues) As DataSet Implements ISecurityService.GetFieldsNULLUsers
        Using AdminSecurity As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return AdminSecurity.ConsultarCamposNULL
        End Using
    End Function

    ''' <summary>
    ''' Unlocks the user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UnlockUser(codeUser As String, session As SessionValues) As Boolean Implements ISecurityService.UnlockUser
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.UnlockUser(codeUser)
        End Using
    End Function

    ''' <summary>
    ''' Finds the name of the user by.	
    ''' </summary>
    ''' <param name="name">The name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindUserByName(name As String, session As SessionValues) As List(Of User) Implements ISecurityService.ListUsersByName
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.FindUserByName(name)
        End Using
    End Function

    ''' <summary>
    ''' Changes the password.	
    ''' </summary>
    ''' <param name="codeUser">se envia id de usario.</param>
    ''' <param name="lastPasswd">The last passwd.</param>
    ''' <param name="newPasswd">The new passwd.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangePassword(codeUser As String, lastPasswd As String, newPasswd As String, session As SessionValues) As Boolean Implements ISecurityService.ChangePassword
        ServerSessionValues.Current.CurrentHISContainer = session.HisContainer
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.ChangePassword(codeUser, lastPasswd, newPasswd)
        End Using
    End Function

    ''' <summary>
    ''' Gets the user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUser(codeUser As String, session As SessionValues, Optional email As String = Nothing) As User Implements ISecurityService.GetUserByCodeUser
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetUser(codeUser, session.IndigoCompany, email)
        End Using
    End Function

    ''' <summary>
    ''' Gets the user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="session">session.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserByCodeUserContainer(codeUser As String, session As SessionValues) As User Implements ISecurityService.GetUserByCodeUserContainer
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetUser(codeUser, session.IndigoContainerId)
        End Using
    End Function

    ''' <summary>
    ''' Consulta el usuario por el correo electronico
    ''' </summary>
    ''' <param name="email"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetUserByEmail(email As String, session As SessionValues) As User Implements ISecurityService.GetUserByEmail
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetUserByEmail(email)
        End Using
    End Function


    ''' <summary>
    ''' Gets the user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserCommand(codeUser As String, session As SessionValues) As User Implements ISecurityService.GetUserCommand
        Dim sql As New Text.StringBuilder()

        sql.AppendLine(" SELECT * FROM Security.[User] SU                                  ")
        sql.AppendLine(" INNER Join Security.Person PE WITH(NOLOCK) ON SU.IdPerson = PE.Id ")
        sql.AppendLine(" WHERE SU.UserCode = '" & codeUser & "'                            ")

        Dim dtDatos As New DataTable("User")
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", session.SecurityContainer)
        Dim conexion As New SqlClient.SqlConnection(conx)
        Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(sql.ToString(), conexion)
        da.SelectCommand.CommandTimeout = 90
        Dim ds As New DataSet
        da.Fill(ds, "User")
        dtDatos = ds.Tables("User")
        conexion.Close()

        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim row As DataRow = ds.Tables(0).Rows(0)
            Return New User() With {
                .Id = Convert.ToInt32(row("Id")),
                .IdPerson = Convert.ToInt32(row("IdPerson")),
                .UserCode = row("UserCode").ToString(),
                .RollCode = row("RollCode").ToString(),
                .GroupCode = row("GroupCode").ToString(),
                .Position = row("Position").ToString(),
                .UserType = row("UserType").ToString(),
                .ChangePassword = Convert.ToBoolean(row("ChangePassword")),
                .Person = New Person() With {
                    .Id = Convert.ToInt32(row("IdPerson")),
                    .Identification = row("Identification").ToString(),
                    .IdentificationType = Convert.ToInt16(row("IdentificationType")),
                    .FirstName = row("FirstName").ToString(),
                    .SecondName = row("SecondName").ToString(),
                    .FirstLastName = row("FirstLastName").ToString(),
                    .SecondLastName = row("SecondLastName").ToString(),
                    .Fullname = row("Fullname").ToString(),
                    .State = Convert.ToBoolean(row("State"))
                }
            }
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Gets the user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserByCodeUseObjectOnly(codeUser As String, session As SessionValues) As User Implements ISecurityService.GetUserByCodeUseObjectOnly
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetUserObjectOnly(codeUser)
        End Using
    End Function

    ''' <summary>
    ''' Validates the user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="passwd">The passwd.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateUser(codeUser As String, passwd As String, session As SessionValues) As Boolean Implements ISecurityService.ValidateUser
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of UserAdminService)()
            Return userAdmin.ValidateUser(codeUser, passwd)
        End Using
    End Function

    ''' <summary>
    ''' Saves the permissions user.	
    ''' </summary>
    ''' <param name="user">The user.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePermissionsUser(user As User, session As SessionValues, audit As AuditMessage) As ActionResult Implements ISecurityService.SavePermissionsUser
        ServerSessionValues.Current.CurrentHISContainer = session.HisContainer
        Using permissionsUser As IPermissionsUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionsUserAdminService)()
            Return permissionsUser.SavePermissionsUser(user, session, audit)
        End Using
    End Function

    ''' <summary>
    ''' Deletes the users.	
    ''' </summary>
    ''' <param name="user">The user.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteUsers(ByVal user As User, session As SessionValues, audit As AuditMessage) As ActionResult Implements ISecurityService.DeleteUsers
        Using permissionsUser As IPermissionsUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionsUserAdminService)()
            Return permissionsUser.DeleteUsers(user, session, audit)
        End Using
    End Function

    ''' <summary>
    ''' Lists the permissions user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUser(codeUser As String, session As SessionValues) As List(Of PermissionUser) Implements ISecurityService.ListPermissionsUserByCodeUser
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.ListPermissionsUser(codeUser, "", "")
        End Using
    End Function

    ''' <summary>
    ''' Gets the email user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmailUser(codeUser As String, session As SessionValues) As String Implements ISecurityService.GetEmailUser
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetEmailUser(codeUser)
        End Using
    End Function

    ''' <summary>
    ''' Gets the password user.	
    ''' </summary>
    ''' <param name="codeUser">se envia id de usario.</param>
    ''' <returns>Cadena vacía por seguridad</returns>
    Public Function GetPasswordUser(codeUser As String, session As SessionValues) As String Implements ISecurityService.GetPasswordUser
        Return String.Empty
    End Function

    ''' <summary>
    ''' Lists the permissions user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUser(codeUser As String, codeMenu As String, session As SessionValues) As List(Of PermissionUser) Implements ISecurityService.ListPermissionsUserByUserAndMenu
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.ListPermissionsUser(codeMenu, codeMenu)
        End Using
    End Function

    ''' <summary>
    ''' Lists the permissions user authorized.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUserAuthorized(codeUser As String, codeMenu As String, session As SessionValues) As List(Of PermissionUser) Implements ISecurityService.ListPermissionsUserAuthorized
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.ListPermissionsUser(codeMenu, codeMenu, True)
        End Using
    End Function

    ''' <summary>
    ''' Lists the permissions user no authorized.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUserNoAuthorized(codeUser As String, codeMenu As String, session As SessionValues) As List(Of PermissionUser) Implements ISecurityService.ListPermissionsUserNoAuthorized
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.ListPermissionsUser(codeMenu, codeMenu, False)
        End Using
    End Function

    ''' <summary>
    ''' Lists all users.	
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllUsers(session As SessionValues) As IEnumerable(Of User) Implements ISecurityService.ListAllUsers
        Try
            Dim authorizationHeader As String = Nothing
            Dim raw As Object = Nothing

            If OperationContext.Current IsNot Nothing AndAlso
               OperationContext.Current.IncomingMessageProperties.TryGetValue(HttpRequestMessageProperty.Name, raw) Then
                Dim httpProp As HttpRequestMessageProperty = TryCast(raw, HttpRequestMessageProperty)
                If httpProp IsNot Nothing Then
                    authorizationHeader = httpProp.Headers("Authorization")
                End If
            End If

            If String.IsNullOrEmpty(authorizationHeader) OrElse Not authorizationHeader.StartsWith("Bearer ") Then
                Throw New FaultException(String.Format("Acceso no autorizado. Código de incidencia: {0}", Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()))
            End If

            Dim token As String = authorizationHeader.Substring("Bearer ".Length).Trim()
            Task.Run(Function() JwtFactory.ValidateToken(token)).Wait()

        Catch ex As FaultException
            Throw

        Catch ex As AggregateException
            Throw New FaultException(String.Format("Acceso no autorizado. Código de incidencia: {0}", Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()))

        Catch ex As Exception
            Throw New FaultException(String.Format("Error interno del servicio. Código de incidencia: {0}", Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()))
        End Try

        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Dim users = userAdmin.ListAllUsers()
            If users IsNot Nothing Then
                For Each u In users
                    u.Password = Nothing
                    u.PasswordLync = Nothing
                Next
            End If
            Return users
        End Using
    End Function

    ''' <summary>
    ''' Gets the person.	
    ''' </summary>
    ''' <param name="identification">The identification.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPerson(identification As String, session As SessionValues) As Person Implements ISecurityService.GetPerson
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetPerson(identification)
        End Using
    End Function

    ''' <summary>
    ''' Gets the user.	
    ''' </summary>
    ''' <param name="identification">The identification.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUser(identification As Integer, session As SessionValues) As User Implements ISecurityService.GetUserByIdentification
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetUser(identification)
        End Using
    End Function


    ''' <summary>
    ''' Funcion para guardar archivo de usaurio
    ''' </summary>
    ''' <param name="_ArchivosProfesionales"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveFileUserRepository(_ArchivosProfesionales As FilePerson, session As SessionValues) As Boolean Implements ISecurityService.SaveFileUserRepository
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.SaveFileUserRepository(_ArchivosProfesionales, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Gets the user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserById(Id As String, session As SessionValues) As User Implements ISecurityService.GetUserById
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetUserById(Id, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los permisos de las unidades operativos
    ''' Este servicio se convierte a SqlCommand debido a que se necesita que se carguen las unidades operativas lo más rápido posible una vez se inicie sesión
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <param name="idContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPermissionOperatingUnit(ByVal codeUser As String, idContainer As Integer, session As SessionValues) As List(Of UserOperatingUnit) Implements ISecurityService.ListPermissionOperatingUnit
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.ListPermissionOperatingUnit(codeUser, idContainer)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los permisos de las unidades operativos
    ''' Este servicio se crea con SqlCommand debido a que se necesita que se carguen las unidades operativas lo más rápido posible una vez se inicie sesión debido
    ''' a la demora instanciando el contenedor de seguridad
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <param name="idContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPermissionOperatingUnitCommand(ByVal codeUser As String, idContainer As Integer, session As SessionValues) As List(Of UserOperatingUnit) Implements ISecurityService.ListPermissionOperatingUnitCommand

        Dim sql As New Text.StringBuilder()

        sql.AppendLine("SELECT UOU.Id, UOU.IdUser, UOU.IdContainer, UOU.IdOperatingUnit, [Status] = cast(1 as bit) FROM Security.[User] SU " &
"INNER JOIN Security.UserOperatingUnit UOU On UOU.IdUser = SU.Id " &
"WHERE SU.UserCode = '" & codeUser & "' AND UOU.idContainer = " & idContainer & " AND UOU.[Status] = 1 ")


        Dim dtDatos As New DataTable("UserOperatingUnit")
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", session.SecurityContainer)
        Dim conexion As New SqlClient.SqlConnection(conx)
        Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(sql.ToString(), conexion)
        da.SelectCommand.CommandTimeout = 90
        Dim ds As New DataSet
        da.Fill(ds, "UserOperatingUnit")
        dtDatos = ds.Tables("UserOperatingUnit")
        conexion.Close()

        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim userOperatingUnit As New List(Of UserOperatingUnit)()
            For Each op As DataRow In ds.Tables(0).Rows
                Dim ou = New UserOperatingUnit()
                With ou
                    .Id = Convert.ToInt32(op("Id"))
                    .IdUser = Convert.ToInt32(op("IdUser"))
                    .IdContainer = Convert.ToInt32(op("IdContainer"))
                    .IdOperatingUnit = Convert.ToInt32(op("IdOperatingUnit"))
                    .Status = Convert.ToBoolean(op("Status"))
                End With
                userOperatingUnit.Add(ou)
            Next
            Return userOperatingUnit
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Guarda una permiso de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePermissionUser(permissionCompany As Company, session As SessionValues) As Boolean Implements ISecurityService.SavePermissionUser
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.SavePermissionUser(permissionCompany, session)
        End Using
    End Function

    Public Function GetPhoneByPersonId(personId As Integer) As List(Of Phone) Implements ISecurityService.GetPhoneByPersonId
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetPhoneByPersonId(personId)
        End Using
    End Function

    Public Function GetMailsByPersonId(personId As Integer) As List(Of Email) Implements ISecurityService.GetMailsByPersonId
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetMailsByPersonId(personId)
        End Using
    End Function

    Public Function GetAddressByPersonId(personId As Integer) As List(Of Address) Implements ISecurityService.GetAddressByPersonId
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetAddressByPersonId(personId)
        End Using
    End Function

    Public Function GetRollById(Id As Integer) As Roll Implements ISecurityService.GetRollById
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetRollById(Id)
        End Using
    End Function

    Public Function GetPermissionUserByUserId(userId As Integer) As List(Of PermissionUser) Implements ISecurityService.GetPermissionUserByUserId
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetPermissionUserByUserId(userId)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el token de la firma electronica de un usuario por su ID
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Function GetElectronicSignatureToken(userId As String) As String Implements ISecurityService.GetElectronicSignatureToken
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetElectronicSignatureTokenByUserId(userId)
        End Using
    End Function

    ''' <summary>
    ''' Guarda el token de la firma electronica de un usuario luego de validarlo
    ''' </summary>
    ''' <param name="token"></param>
    ''' <param name="userId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveElectronicSignatureToken(token As String, userId As Integer, audit As AuditMessage) As ActionResult Implements ISecurityService.SaveElectronicSignatureToken
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.SaveElectronicSignatureToken(token, userId, audit)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene la ruta del informe para un usuario específico y un contenedor dado.
    ''' </summary>
    ''' <param name="IdUser"></param>
    ''' <param name="IdContainer"></param>
    ''' <param name="IdOperatingUnit"></param>
    ''' <returns></returns>
    Public Function GetReportPathByUser(IdUser As Integer, Optional IdContainer As Integer = 0, Optional IdOperatingUnit As Integer = 0) As String Implements ISecurityService.GetReportPathByUser
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetReportPathByUser(IdUser, IdContainer, IdOperatingUnit)
        End Using
    End Function

#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    Public Function GetPerfilUbicacion(usuario As String) As ActionResult(Of SP_SEG_AutenticarUsuario_Result) Implements ISecurityService.GetPerfilUbicacion
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetPerfilUbicacion(usuario)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    Public Function GetProfesional(usuario As String) As ActionResult(Of SP_SEG_AutenticarDatosProfesional_Result) Implements ISecurityService.GetProfesional
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetProfesional(usuario)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <param name="centroAtencion"></param>
    ''' <returns></returns>
    Public Function GetUnidadFuncionalAutorizado(usuario As String, grupo As String, centroAtencion As String) As ActionResult(Of List(Of SP_SEG_UnidadFuncional_Autorizado_Result)) Implements ISecurityService.GetUnidadFuncionalAutorizado
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetUnidadFuncionalAutorizado(usuario, grupo, centroAtencion)
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <returns></returns>
    Public Function GetCentroAtencionAutorizado(usuario As String, grupo As String) As ActionResult(Of List(Of SP_SEG_CentroAtencion_Autorizado_Result)) Implements ISecurityService.GetCentroAtencionAutorizado
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetCentroAtencionAutorizado(usuario, grupo)
        End Using
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <returns></returns>
    Public Function GetPermisoUsuario(idMenu As String, codigoUsuario As String) As ActionResult(Of SEGpermiu) Implements ISecurityService.GetPermisoUsuario
        Using userAdmin As IPermissionsUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IPermissionsUserAdminService)()
            Return userAdmin.GetPermisoUsuario(idMenu, codigoUsuario)
        End Using
    End Function

    Public Function GetSubCupsImaging() As ActionResult(Of List(Of INCUPSSUB)) Implements ISecurityService.GetSubCupsImaging
        Using userAdmin As IUserAdminService = IocFactory.Instance().CurrentContainer.Resolve(Of IUserAdminService)()
            Return userAdmin.GetSubCupsImaging()
        End Using
    End Function
#End Region

End Class
