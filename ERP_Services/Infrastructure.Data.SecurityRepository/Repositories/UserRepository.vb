'***********************************************************************
' Assembly         : Infraestructura.Datos.RepositorioSeguridad
' Author           : OscarSierra
' Created          : 03-11-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Security.Entities
Imports Domain.Security
Imports System.Globalization
Imports System.Data.Entity
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Security
Imports Infrastructure.CrossCutting.Interface
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Esta clase contiene cada uno de los metodos y funciones que no son comunes dentro del repositorio generico ubicado
''' en Infraestructura.Base	 ademas implementa de la interfaz ubicada en la capa de Dominio.Seguridad. Esta clase es la encargada 
''' realizar las respectivas consultas y operaciones del CRUD para la entidad de Usuarios.
''' </summary>
Public Class UserRepository
    Inherits GenericRepository(Of User)
    Implements IUserRepository

    ''' <summary>
    ''' Esta variable  contiene el contexto de nuestro modelo.
    ''' </summary>
    Private _context As ISeguridadUnitOfWork
    ''' <summary>
    ''' Inicializa una nueva instancia <see cref="UserRepository" /> class.	
    ''' </summary>
    ''' <param name="contex">nThe contex.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Changes the password.	
    ''' </summary>
    ''' <param name="codeUser">se envia id de usario.</param>
    ''' <param name="newPassword">The new password.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangePassword(codeUser As String, newPassword As String) As Boolean Implements IUserRepository.ChangePassword
        Dim user = (From e In _context.User
                    Where e.Id = CInt(codeUser)
                    Select e).Single

        user.Password = newPassword
        user.DateLastChangePassword = Date.Now
        user.ChangePassword = False

        SaveEntity(user)
        Return True
    End Function

    ''' <summary>
    ''' Finds the name of the user by.	
    ''' </summary>
    ''' <param name="nameUser">The name user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindUserByName(nameUser As String) As List(Of User) Implements IUserRepository.FindUserByName
        'If Not _context Is Nothing Then
        '    Dim usuarios = From e In _context.SeguridadUsuario
        '                          Where e.i = nombre.Trim And e.EstadoEliminado = False
        '                          Select e
        '    Return usuarios.ToList
        'Else
        '    Throw New ArgumentNullException("Context")
        'End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Gets the person by key.	
    ''' </summary>
    ''' <param name="identification">The identification.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPersonByKey(identification As String) As Person Implements IUserRepository.GetPerson
        Dim person = From e In _context.Person.Include("Address").Include("Email").Include("Phone").Include("FilePerson")
                     Where (e.Identification = identification)
                     Select e

        If person.Count > 0 Then
            Return person.Single
        Else
            Return New Person
        End If
    End Function

    ''' <summary>
    ''' Gets the user by code.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserByCode(codeUser As String, Optional email As String = Nothing) As User Implements IUserRepository.GetUser
        If codeUser = System.String.Empty Then
            Throw New ArgumentNullException("codeUser")
        End If
        Dim usuario As User
        Dim usuarioGeneral = (From e In _context.User
                              Join p In _context.Person On p.Id Equals e.IdPerson
                              Where e.UserCode = codeUser
                              Select e).Include("Person")
        If email Is Nothing Then
            usuario = usuarioGeneral.FirstOrDefault()
        Else
            usuario = usuarioGeneral.Where(Function(x) x.Email = email).FirstOrDefault()
        End If

        If usuario IsNot Nothing AndAlso usuario.Id > 0 Then
            usuario.Roll = (From r In _context.Roll Where r.Id = usuario.RollCode Select r).FirstOrDefault
            usuario.Group = (From g In _context.Group Where g.Id = usuario.GroupCode Select g).FirstOrDefault
            'usuario.Person = (From p In _context.Person Where p.Id = usuario.IdPerson Select p).FirstOrDefault
            Dim permissionsCompany = (From p In _context.PermissionCompany.Where(Function(p) p.IdUser = usuario.Id)
                                      Join c In _context.Containers On c.Id Equals p.IdContainer
                                      Select p, c.Name)
            For Each pc In permissionsCompany
                pc.p.ContainerName = pc.Name
                usuario.PermissionCompany.Add(pc.p)
            Next

            Dim userOperatingUnit = (From u In _context.UserOperatingUnit Where u.IdUser = usuario.Id Select u).ToList()
            userOperatingUnit.ForEach(Sub(u)
                                          usuario.UserOperatingUnit.Add(u)
                                      End Sub)
            Dim tenantUser = (From tu In _context.TenantUsers.Where(Function(tu) tu.UserId = usuario.Id)
                              Join t In _context.Tenant On t.Id Equals tu.TenantId
                              Group Join r In _context.Roll On r.Id Equals tu.RollId Into gr = Group
                              From r In gr.DefaultIfEmpty()
                              Group Join g In _context.Group On g.Id Equals tu.GroupId Into gg = Group
                              From g In gg.DefaultIfEmpty()
                              Select tu, t.Name, RoleName = If(r Is Nothing, "", r.Description), GroupName = If(g Is Nothing, "", g.Description))

            For Each ts In tenantUser
                ts.tu.TenantName = ts.Name
                ts.tu.RoleName = ts.RoleName
                ts.tu.GroupName = ts.GroupName
                usuario.TenantUsers.Add(ts.tu)
            Next

            Return usuario
        Else
            Return New User()
        End If
    End Function

    ''' <summary>
    ''' Gets the user by code.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="containerId">id de container.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserByCode(codeUser As String, containerId As Integer) As User Implements IUserRepository.GetUser
        If codeUser = System.String.Empty Then
            Throw New ArgumentNullException("codeUser")
        End If
        If containerId = 0 Then
            Throw New ArgumentNullException("containerId")
        End If
        Dim usuario As User
        Dim usuarios = (From e In _context.User
                        Where (e.UserCode = codeUser)
                        Select e)

        If usuarios.Count > 0 Then

            Dim permissionsCompany = (From p In _context.PermissionCompany Where p.IdContainer = containerId Select p)
            If permissionsCompany.Count > 0 AndAlso usuarios.Any(Function(x) x.UserType <> 3) Then
                usuario = (From u In usuarios
                           Join pc In permissionsCompany
                                 On u.Id Equals pc.IdUser
                           Select u).FirstOrDefault
            Else
                usuario = usuarios.FirstOrDefault
            End If

            If usuario IsNot Nothing Then
                usuario.PersonFullName = (From list In _context.Person.AsNoTracking() Where usuario.IdPerson = list.Id Select list.Fullname).FirstOrDefault()
                Dim linq = (From p In _context.PermissionCompany.Where(Function(p) p.IdUser = usuario.Id)
                            Join c In _context.Containers On c.Id Equals p.IdContainer
                            Select p, c.Name)
                For Each pc In linq
                    pc.p.ContainerName = pc.Name
                    usuario.PermissionCompany.Add(pc.p)
                Next
                Dim userOperatingUnit = (From u In _context.UserOperatingUnit Where u.IdUser = usuario.Id Select u).ToList()
                userOperatingUnit.ForEach(Sub(u)
                                              usuario.UserOperatingUnit.Add(u)
                                          End Sub)
                Dim tenantUser = (From tu In _context.TenantUsers.Where(Function(tu) tu.UserId = usuario.Id)
                                  Join t In _context.Tenant On t.Id Equals tu.TenantId
                                  Group Join r In _context.Roll On r.Id Equals tu.RollId Into gr = Group
                                  From r In gr.DefaultIfEmpty()
                                  Group Join g In _context.Group On g.Id Equals tu.GroupId Into gg = Group
                                  From g In gg.DefaultIfEmpty()
                                  Select tu, t.Name, RoleName = If(r Is Nothing, "", r.Description), GroupName = If(g Is Nothing, "", g.Description))
                For Each ts In tenantUser
                    ts.tu.TenantName = ts.Name
                    ts.tu.RoleName = ts.RoleName
                    ts.tu.GroupName = ts.GroupName
                    usuario.TenantUsers.Add(ts.tu)
                Next
                Return usuario
            Else
                Return New User
            End If

        Else
            Return New User()
        End If
    End Function

    Public Function GetPhoneByPersonId(personId As Integer) As List(Of Phone) Implements IUserRepository.GetPhoneByPersonId
        Return (From p In _context.Phone Where p.IdPerson = personId Select p).ToList()
    End Function

    Public Function GetMailsByPersonId(personId As Integer) As List(Of Email) Implements IUserRepository.GetMailsByPersonId
        Return (From e In _context.Email Where e.IdPerson = personId Select e).ToList()
    End Function

    Public Function GetUserByEmail(email As String) As User Implements IUserRepository.GetUserByEmail
        Return (From u In _context.User Where u.Email = email Select u).FirstOrDefault
    End Function

    Public Function GetAddressByPersonId(personId As Integer) As List(Of Address) Implements IUserRepository.GetAddressByPersonId
        Return (From e In _context.Address Where e.IdPerson = personId Select e).ToList()
    End Function

    Public Function GetRollById(Id As Integer) As Roll Implements IUserRepository.GetRollById
        Return (From e In _context.Roll Where e.Id = Id Select e).FirstOrDefault()
    End Function

    Public Function GetPermissionUserByUserId(userId As Integer) As List(Of PermissionUser) Implements IUserRepository.GetPermissionUserByUserId
        Return (From e In _context.PermissionUser Where e.IdUser = userId Select e).ToList()
    End Function

    ''' <summary>
    ''' Gets the user by code.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUser(codeUser As String) As User Implements IUserRepository.GetUserObjectOnly
        If codeUser = System.String.Empty Then
            Throw New ArgumentNullException("codeUser")
        End If
        Dim usuario = From e In _context.User
                      Where (e.UserCode = codeUser) AndAlso (e.State = True)
                      Select e
        If usuario.Count > 0 Then
            Return usuario.FirstOrDefault()
        Else
            Return New User
        End If
    End Function

    Public Function ListUsersByCodes(ByVal listCodes As List(Of String)) As List(Of User) Implements IUserRepository.ListUsersByCodes
        If listCodes Is Nothing Then
            Throw New ArgumentNullException("listCodes")
        End If
        Dim res = (From e In _context.User.Include("Person") Where (listCodes.Contains(e.UserCode)) Select e).ToList
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        End If
        Return New List(Of User)()
    End Function

    Public Function ListUsersByIds(listIds As List(Of Integer)) As List(Of User) Implements IUserRepository.ListUsersByIds
        If listIds Is Nothing Then
            Throw New ArgumentNullException("listIds")
        End If
        Dim res = (From e In _context.User.Include("Person") Where (listIds.Contains(e.Id)) Select e).ToList
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        End If
        Return New List(Of User)()
    End Function

    ''' <summary>
    ''' Obtiene el usuario a traves del codigo
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserPersonFileByCode(codeUser As String) As User Implements IUserRepository.GetUserPersonFileByCode
        If codeUser = System.String.Empty Then
            Throw New ArgumentNullException("codeUser")
        End If
        Dim usuario = From e In _context.User.Include("Person").Include("Person.FilePerson").Include("Person.Email")
                      Where (e.UserCode = codeUser)
                      Select e
        If usuario.Count > 0 Then
            Return usuario.Single
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtengo los datos del usuario y persona
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserPersonFile(codeUser As String, password As String) As User Implements IUserRepository.GetUserPersonFile
        If codeUser = System.String.Empty Then
            Throw New ArgumentNullException("codeUser")
        End If
        Dim passwordEncript = IndigoRijndael.Encrypt(password)
        Dim usuario = From e In _context.User.Include("Person").Include("Person.FilePerson").Include("Person.Email")
                      Where (e.UserCode = codeUser And e.Password = passwordEncript)
                      Select e
        If usuario.Count > 0 Then
            Return usuario.Single
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Gets the user by key.	
    ''' </summary>
    ''' <param name="identification">The identification.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserByKey(identification As Integer) As User Implements IUserRepository.GetUser
        Dim result = From e In _context.User
                     Where e.UserCode = identification
                     Select e

        If result.Count > 0 Then
            Return result.Single
        Else
            Return New User
        End If
    End Function

    ''' <summary>
    ''' Lists the permissions user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUser(codeUser As String) As List(Of PermissionUser) Implements IUserRepository.ListPermissionsUser
        Dim permissionsUser = (From p In _context.PermissionUser
                               Join u In _context.User On u.Id Equals p.IdUser
                               Where u.UserCode = codeUser
                               Select p).ToList()
        'Dim permissionsUser = (From e In _context.PermissionUser.Include("User").AsNoTracking()
        '                       Where e.User.UserCode = codeUser
        '                       Select e).ToList()
        Return If(permissionsUser IsNot Nothing, permissionsUser, New List(Of PermissionUser)())
    End Function


    ''' <summary>
    ''' Lists the permissions user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="companyCode">The company code.</param>
    ''' <param name="authorized">Authorized.</param>
    ''' <returns>List of user permissions</returns>
    Public Function ListPermissionsUserAuthorized(codeUser As String, authorized As Boolean) As List(Of PermissionUser) Implements IUserRepository.ListPermissionsUserAuthorized
        Dim permissionsUser = (From e In _context.PermissionUser.Include("User")
                               Where e.User.UserCode = codeUser And e.ActionValue = authorized
                               Select e).ToList()
        Return If(permissionsUser IsNot Nothing, permissionsUser, New List(Of PermissionUser)())

    End Function

    ''' <summary>
    ''' Lists the permissions user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUser(codeUser As String, codeMenu As String) As List(Of PermissionUser) Implements IUserRepository.ListPermissionsUser
        Dim permissionsUser = (From e In _context.PermissionUser.Include("User")
                               Where e.User.UserCode = codeUser And e.IdForm = codeMenu
                               Select e).ToList()
        Return If(permissionsUser IsNot Nothing, permissionsUser, New List(Of PermissionUser)())
    End Function

    ''' <summary>
    ''' Lists the permissions user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeMenu">The code menu.</param>
    ''' <param name="authorized">The authorized.</param>
    ''' <param name="tenantId">id de tenant.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPermissionsUser(codeUser As String, codeMenu As String, authorized As Boolean, tenantId As Short) As List(Of PermissionUser) Implements IUserRepository.ListPermissionsUser
        Dim permissionsUser = (From e In _context.PermissionUser.Include("User")
                               Where e.User.UserCode = codeUser And e.TenantId = tenantId And e.IdForm = codeMenu And e.ActionValue = authorized
                               Select e).ToList()
        Return If(permissionsUser IsNot Nothing, permissionsUser, New List(Of PermissionUser)())
    End Function

    ''' <summary>
    ''' Locks the user.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LockUser(codeUser As String) As Boolean Implements IUserRepository.LockUser
        Dim usuario As User
        If codeUser = System.String.Empty Then
            Throw New ArgumentNullException("codeUser")
        End If

        If Not _context Is Nothing Then
            usuario = (From e In _context.User
                       Where (e.UserCode = codeUser.Trim)
                       Select e).Single
            usuario.State = False
            'todo: AGREGAR PROPIEDAD

            UpdateEntity(usuario)
        End If

        Return True
    End Function

    ''' <summary>
    ''' Saves the location login.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="role">The role.</param>
    ''' <param name="CareCenter">The health care.</param>
    ''' <param name="unitFunctional">The unit functional.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveLocationLogin(codeUser As String, role As Integer, CareCenter As String, unitFunctional As String) As Boolean Implements IUserRepository.SaveLoginLocation
        Dim user = (From e In _context.User
                    Where e.UserCode = codeUser.Trim
                    Select e).Single
        UpdateEntity(user)
        Return True
    End Function

    ''' <summary>
    ''' metodo que develve los valores originales de la entidad {T}
    ''' </summary>
    ''' <typeparam name="TEntity">el tipo de la entidad.</typeparam>
    ''' <param name="entity">el objeto de la entidad.</param>
    ''' <returns></returns>
    Public Function GetSourceValues(Of TEntity)(ByVal entity As TEntity) As TEntity
        Return IndigoContext.GetSourceValues(entity, CType(_context, DbContext))
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="CodeUser"></param>
    ''' <param name="CodeMenu"></param>
    ''' <param name="CodeOpcion"></param>
    ''' <param name="tenantId"></param>
    ''' <returns></returns>
    Public Function ListPermissionsUserForm(CodeUser As String, CodeMenu As String, CodeOpcion As String, tenantId As Short) As Boolean Implements IUserRepository.ListPermissionsUserForm

        Dim _User = (From u In _context.User Where u.UserCode = CodeUser).FirstOrDefault
        If _User IsNot Nothing Then
            Select Case CType(_User.UserType, UserType)
                Case UserType.StandardUser, UserType.CompanyAdmin, UserType.TenantAdmin
                    Dim _tenantUser = (From tu In _context.TenantUsers Where tu.UserId = _User.Id AndAlso tu.TenantId = tenantId).FirstOrDefault
                    If _tenantUser IsNot Nothing Then
                        Dim permission = (From e In _context.PermissionRoll Where e.IdRoll = _tenantUser.RollId AndAlso e.IdForm = CodeMenu.Trim AndAlso e.Action = CodeOpcion AndAlso e.ActionValue
                                          Select e).ToList()
                        If permission IsNot Nothing AndAlso permission.Count > 0 Then
                            Return True
                        Else
                            Dim _permissionUser = (From e In _context.PermissionUser
                                                   Where e.IdUser = _User.Id AndAlso e.TenantId = tenantId AndAlso e.IdForm = CodeMenu.Trim AndAlso e.Action = CodeOpcion AndAlso e.ActionValue
                                                   Select e).ToList()
                            If _permissionUser IsNot Nothing AndAlso _permissionUser.Count > 0 Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    Else
                        Return False
                    End If
                Case UserType.GlobalAdmin, UserType.GlobalQa
                    Return True
                Case Else
                    Return False
            End Select
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Gets the user by Id	
    ''' </summary>
    ''' <param name="Id">Id.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserById(Id As String) As User Implements IUserRepository.GetUserById
        Dim result = From e In _context.User.Include("Person")
                     Where e.Id = CInt(Id)
                     Select e

        If result.Count > 0 Then
            Return result.Single
        Else
            Return New User
        End If
    End Function

    ''' <summary>
    ''' Obtiene el usuario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetUserByCodeSimple(code As String) As User Implements IUserRepository.GetUserByCodeSimple
        Return (From e In _context.User.Include("Person")
                Where e.UserCode = code
                Select e).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista todos los usuarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllUser() As List(Of User) Implements IUserRepository.ListAllUser
        Dim user = From e In _context.User.Include("Person")
                   Select e
        Return user.ToList()
    End Function

    Public Function ListPermissionsOperatingUnit(codeUser As String, idCompany As Integer) As List(Of UserOperatingUnit) Implements IUserRepository.ListPermissionsOperatingUnit
        Dim queryUserOperating = From e In _context.UserOperatingUnit.Include("User").Include("Containers")
                                 Where e.IdContainer = idCompany And e.User.UserCode = codeUser
        Return queryUserOperating.ToList()
    End Function

    ''' <summary>
    ''' Valida que el usuario y la contraseña existan
    ''' </summary>
    ''' <param name="codeUser">se envia id del usuario</param>
    ''' <param name="password"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateUser(codeUser As String, password As String) As Boolean Implements IUserRepository.ValidateUser
        'Dim query = From e In _context.User Where e.UserCode = codeUser And e.Password = password Select e
        Dim query = From e In _context.User Where e.Id = CInt(codeUser) And e.Password = password Select e
        If query.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Lista todos los ids de los formularios con permiso de solicitado,
    ''' asignados al usuario y al rol que tiene el usuario
    ''' </summary>
    ''' <param name="tenantId">tenant id</param>
    ''' <param name="rollCode">roll code</param>
    ''' <param name="codeUser">Código del usuario</param>
    ''' <param name="action">Acción</param>
    ''' <returns>Lista de ids de formularios</returns>
    Public Function ListPermissionFormsUser(tenantId As Short, rollCode As String, codeUser As String, action As String) As List(Of String) Implements IUserRepository.ListPermissionFormsUser
        Dim list As New List(Of String)()
        Dim pRol = (From t In _context.PermissionRoll.Include("Roll") Where t.Roll.RollCode = rollCode And t.Action = action And t.ActionValue = True Select t.IdForm).ToList()
        Dim pUser = (From r In _context.PermissionUser.Include("User") Where r.User.UserCode = codeUser And r.TenantId = tenantId And r.Action = action And r.ActionValue = True Select r.IdForm).ToList()
        list = pRol.Union(pUser).Distinct().ToList()
        Return list
    End Function

    Public Function ListPermissionFormsUserV2(tenantId As Short, rollCode As String, codeUser As String, action As String) As List(Of ProductCatalog) Implements IUserRepository.ListPermissionFormsUserV2
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim listProducts As List(Of ProductCatalog) = New List(Of ProductCatalog)()
            Dim dt1 As DataTable = Nothing

            Dim query = String.Format("SELECT DISTINCT * FROM (
            SELECT PC.Id IdProduct,PC.ProductName,PC.State ProductState,PC.Visible ProductVisible,
	            M.Id IdModule, M.Name ModuleName, 
	            T.Id IdTitle, T.Name TitleName, ISNULL(Mt.[Order],99) TitleOrder,
	            F.Id IdForm,F.Name FormName, MF.FormOrder, ISNULL(F.PrintEvents,'') PrintEvents, F.HasSequence, F.IsNativeForm, F.HasForm,ISNULL(F.ClassName,'') ClassName,ISNULL(f.AssemblyName,'') AssemblyName,f.HandlesMassiveConfirm,
	            PR.Action IdAction,A.Name ActionName
            FROM 
            Security.ProductCatalog PC
            INNER JOIN [Security].[ProductModule] PM ON PC.Id = PM.IdProduct
            INNER JOIN [Security].[Module] M ON PM.IdModule = M.Id AND M.State = 1
            INNER JOIN [Security].[ModuleForm] MF ON M.Id = MF.IdModule 
            INNER JOIN [Security].[Title] T ON MF.IdTitle = T.Id
            INNER JOIN [Security].[Form] F ON MF.IdForm = F.Id AND F.State = 1
            INNER JOIN [Security].[PermissionRoll] PR ON PR.IdForm = F.Id AND PR.ActionValue = 1
            INNER JOIN [Security].[Roll] R ON PR.IdRoll = R.Id
            INNER JOIN [Security].[Action] A ON PR.Action = A.Id AND A.State = 1
            LEFT JOIN [Security].[ModuleTitle] MT ON MF.IdModule = MT.IdModule AND MF.IdTitle = MT.IdTitle
            WHERE R.RollCode = '{0}'  AND PR.Action = '{3}'
            UNION ALL
            SELECT PC.Id IdProduct,PC.ProductName,PC.State ProductState,PC.Visible ProductVisible,
	            M.Id IdModule, M.Name ModuleName, 
	            T.Id IdTitle, T.Name TitleName, ISNULL(Mt.[Order],99) TitleOrder,
	            F.Id IdForm,F.Name FormName, MF.FormOrder, ISNULL(F.PrintEvents,'') PrintEvents, F.HasSequence, F.IsNativeForm, F.HasForm,ISNULL(F.ClassName,'') ClassName,ISNULL(f.AssemblyName,'') AssemblyName,f.HandlesMassiveConfirm,
	            PU.Action,A.Name ActionName
            FROM 
            Security.ProductCatalog PC
            INNER JOIN [Security].[ProductModule] PM ON PC.Id = PM.IdProduct
            INNER JOIN [Security].[Module] M ON PM.IdModule = M.Id AND M.State = 1
            INNER JOIN [Security].[ModuleForm] MF ON M.Id = MF.IdModule 
            INNER JOIN [Security].[Title] T ON MF.IdTitle = T.Id
            INNER JOIN [Security].[Form] F ON MF.IdForm = F.Id AND F.State = 1
            INNER JOIN [Security].[PermissionUser] PU ON PU.IdForm = F.Id AND PU.ActionValue = 1
            INNER JOIN [Security].[Action] A ON PU.Action = A.Id AND A.State = 1
            INNER JOIN [Security].[User] U ON U.Id = PU.IdUser
            LEFT JOIN [Security].[ModuleTitle] MT ON MF.IdModule = MT.IdModule AND MF.IdTitle = MT.IdTitle
            WHERE U.UserCode = '{1}' AND PU.TenantId = {2} AND PU.Action = '{3}' ) G
            ORDER BY  IdProduct,IdModule,IdForm", rollCode, codeUser, tenantId, action)
            dt1 = conx.ExecuteCommand_Data(query)


            Dim lastProduct As ProductCatalog = Nothing
            Dim lastModule As Modules = Nothing
            Dim lastForm As VieDBForm = Nothing
            Dim idProduct As String
            Dim idModule As String
            Dim idForm As String


            For index As Integer = 0 To dt1.Rows.Count() - 1
                idProduct = dt1.Rows(index)("IdProduct")
                idModule = dt1.Rows(index)("IdModule")
                idForm = dt1.Rows(index)("IdForm")

                If lastForm IsNot Nothing AndAlso idForm <> lastForm.IdForm Then
                    lastModule.ListForm.Add(lastForm)
                End If

                If lastModule IsNot Nothing AndAlso idModule <> lastModule.IdModule Then
                    lastProduct.ListModules.Add(lastModule)
                End If

                If lastProduct IsNot Nothing AndAlso idProduct <> lastProduct.IdProduct Then
                    listProducts.Add(lastProduct)
                End If

                If lastProduct Is Nothing OrElse idProduct <> lastProduct.IdProduct Then
                    lastProduct = New ProductCatalog() With {.IdProduct = idProduct, .ProductName = dt1.Rows(index)("ProductName"), .State = Convert.ToByte(dt1.Rows(index)("ProductState")), .Visible = Convert.ToByte(dt1.Rows(index)("ProductVisible"))}
                End If

                If lastModule Is Nothing OrElse idModule <> lastModule.IdModule Then
                    lastModule = New Modules() With {.IdModule = idModule, .ModuleName = dt1.Rows(index)("ModuleName")}
                End If

                If lastForm Is Nothing OrElse idForm <> lastForm.IdForm Then

                    Dim title = New Title() With {.IdTitle = dt1.Rows(index)("IdTitle"), .TitleName = dt1.Rows(index)("TitleName"), .TitleOrder = dt1.Rows(index)("TitleOrder")}

                    lastForm = New VieDBForm() With {
                    .IdForm = idForm,
                    .AssemblyName = dt1.Rows(index)("AssemblyName"),
                    .ClassName = dt1.Rows(index)("ClassName"),
                    .FormName = dt1.Rows(index)("FormName"),
                    .FormOrder = dt1.Rows(index)("FormOrder"),
                    .HandlesMassiveConfirm = dt1.Rows(index)("HandlesMassiveConfirm"),
                    .HasForm = dt1.Rows(index)("HasForm"),
                    .HasSequence = dt1.Rows(index)("HasSequence"),
                    .IsNativeForm = dt1.Rows(index)("IsNativeForm"),
                    .PrintEvents = dt1.Rows(index)("PrintEvents"),
                    .Title = title
                }
                End If
                lastForm.ListAction.Add(New Action() With {.IdAction = dt1.Rows(index)("IdAction"), .ActionName = dt1.Rows(index)("ActionName")})
            Next

            If lastForm IsNot Nothing Then
                lastModule.ListForm.Add(lastForm)
            End If

            If lastModule IsNot Nothing Then
                lastProduct.ListModules.Add(lastModule)
            End If

            If lastProduct IsNot Nothing Then
                listProducts.Add(lastProduct)
            End If

            Return listProducts
        End Using
    End Function


    ''' <summary>
    ''' Actualiza los campos de bloqueo y de intentos fallidos de un usuario especifico
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="lockUser"></param>
    ''' <param name="failedAccount"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateUserLockFailedCount(userId As Integer, lockUser As Boolean, failedAccount As Integer) As Boolean Implements IUserRepository.UpdateUserLockFailedCount
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            conx.ExecuteCommand("update Security.[User] set FailedPasswordCount = " & failedAccount.ToString & ", IsLockedOut = " & lockUser.ToString & " where Id = " & userId.ToString)
            Return True
        End Using
    End Function

    ''' <summary>
    ''' Lista los tipos de telefono
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllPhoneType() As List(Of PhoneType) Implements IUserRepository.ListAllPhoneType
        Dim phoneType = From e In _context.PhoneType
                        Select e
        Return phoneType.ToList()
    End Function

    ''' <summary>
    ''' Obtiene el token de la firma electronica para un usuario especifico por su codigo
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Function GetUserElectronicSignatureToken(userId As Integer) As String Implements IUserRepository.GetUserElectronicSignatureToken
        Dim token = (From e In _context.User
                     Where e.Id = userId
                     Select e.ElectronicSignatureToken).FirstOrDefault
        Return token
    End Function


    ''' <summary>
    ''' Obtiene la ruta del informe para un usuario específico.
    ''' Esta función verifica la configuración del usuario y determina la ruta del informe 
    ''' en función del tipo de usuario y la unidad operativa.
    ''' </summary>
    ''' <param name="IdUser">ID del usuario para el que se busca la ruta del informe.</param>
    ''' <param name="IdContainer">ID del contenedor (opcional, por defecto es 0).</param>
    ''' <param name="IdOperatingUnit">ID de la unidad operativa (opcional, por defecto es 0).</param>
    ''' <returns>Ruta del informe como cadena, o una cadena vacía si no se encuentra.</returns>
    Public Function GetReportPathByUser(IdUser As Integer, Optional IdContainer As Integer = 0, Optional IdOperatingUnit As Integer = 0) As String Implements IUserRepository.GetReportPathByUser
        If IdUser = 0 Then
            Throw New ArgumentNullException("IdUser", "El ID de usuario no puede ser cero.")
        End If
        Dim userConfig = (From uc As UserConfiguration In _context.UserConfiguration.Include("User")
                          Where uc.UserId = IdUser
                          Select uc).FirstOrDefault()

        If userConfig Is Nothing Then
            Return String.Empty
        End If
        ' Verifica si el usuario es de tipo Administrador/empresa
        If userConfig.User.UserType = "0" OrElse userConfig.User.UserType = "1" Then
            If userConfig.ReportPathType = 2 AndAlso IdOperatingUnit <> 0 Then
                Dim userOperatingUnits = (From u In _context.UserOperatingUnit Where u.IdUser = IdUser AndAlso u.IdContainer = IdContainer Select u).ToList()
                If userOperatingUnits.Any() Then
                    Dim matchingUnit = userOperatingUnits.FirstOrDefault(Function(x) x.IdOperatingUnit = IdOperatingUnit AndAlso x.Status = True)
                    If matchingUnit IsNot Nothing Then
                        Return matchingUnit.ReportPath
                    Else
                        Return String.Empty
                    End If
                End If
            End If
            Return userConfig.CustomReportPath
        End If
        Return userConfig.CustomReportPath
    End Function
End Class