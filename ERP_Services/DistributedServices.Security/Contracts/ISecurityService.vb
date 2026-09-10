'***********************************************************************
' Assembly         : DistributedService.Security
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Jhon Tovar
' Last Modified On : 2022-03-22
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Interfaz con los metodos necesarios para el manejo de la seguridad de la aplicaciom
''' </summary>
<ServiceContract(), ServiceKnownType(GetType(PermissionUserToolbar)), ServiceKnownType(GetType(PermissionCompany))>
Public Interface ISecurityService

#Region "Login"
    ''' <summary>
    ''' Funcion para guardar los permisos del usuario
    ''' </summary>
    ''' <param name="PermissionCompanies">The permission companies.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePermissionsCompanies(ByVal PermissionCompanies As List(Of PermissionCompany), session As SessionValues) As Boolean


    ''' <summary>
    ''' Lista los permisos de las empresas.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListCompaniesPermission(ByVal UserCode As String, session As SessionValues) As IEnumerable(Of PermissionCompany)

    ''' <summary>
    ''' Metodo para obtener si el usuario tiene permisos para la empresa seleccionada
    ''' </summary>
    ''' <param name="UserCode">The user code.</param>
    ''' <param name="CompanyCode">The company code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPermissionUserCompany(ByVal UserCode As String, ByVal UserPass As String, CompanyCode As String, session As SessionValues) As ActionResult(Of User)

    <OperationContract()>
    Function LoginUserCompany(userCode As String, userPasswd As String, companyCode As String, ByVal appVersion As Version) As ActionResult(Of UserLogin)

    ''' <summary>
    ''' Guarda las preferencias del Funcional Login
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="role">el perfil (1.Administrativo - 2.Asistencial).</param>
    ''' <param name="CareCenter">el codigo del centro de atencion.</param>
    ''' <param name="unitFunctional">el codigo de la unidad funcional.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveLoginLocation(ByVal codeUser As String, role As Integer, ByVal CareCenter As String, ByVal unitFunctional As String, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta Perfil Usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUserRole(ByVal codeUser As String, session As SessionValues) As String

    ''' <summary>
    ''' Lista huella digital del usuario logeado en genesis
    ''' </summary>
    ''' <returns></returns>
    ''' 
    <OperationContract()>
    Function GetFingerPrint(session As SessionValues) As List(Of Domain.Security.Entities.Person)

    ''' <summary>
    ''' Función para obtener una lista de contenedores
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns></returns>
    <OperationContract()>
    Function getContainers(session As SessionValues) As List(Of Containers)

    <OperationContract()>
    Function ListCompanies() As ActionResult(Of List(Of Company))

    ''' <summary>
    ''' Lista las compañias que tiene permiso el usuario
    ''' </summary>
    ''' <param name="idUser"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function getCompaniesByUser(ByVal idUser As Integer) As ActionResult(Of List(Of Company))

    ''' <summary>
    ''' Obtiene el nombre del contenedor de seguridad
    ''' </summary>
    ''' <returns>Nombre del contenedor de seguridad</returns>
    <OperationContract()>
    Function getSecurityContainerName() As String

    ' ''' <summary>
    ' ''' Obtiene el nombre del contenedor de la interaccion con costos
    ' ''' </summary>
    ' ''' <returns>Nombre del contenedor de Indigo Vie Cloud Platform</returns>
    '<OperationContract()>
    'Function getInteropCostContainerName() As String

    ''' <summary>
    ''' Obtiene la cadena de conexión
    ''' </summary>
    ''' <returns>Cadena de conexión vacía por seguridad </returns>
    <OperationContract()>
    Function getIndigoConnectionString() As String

    ''' <summary>
    ''' Función para obtener un contenedor
    ''' </summary>
    ''' <param name="Name">Nombre del contenedor</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Objeto Contenedor</returns>
    <OperationContract()>
    Function getContainersByName(Name As String, session As SessionValues) As Containers

    <OperationContract()>
    Function getContainersByCode(Code As String, session As SessionValues) As Containers

    <OperationContract()>
    Function getEndpointsByIdContainer(idContainer As Integer, session As SessionValues) As IEnumerable(Of Endpoints)

    ''' <summary>
    ''' Función para obtener una lista de zonas horarias
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns></returns>
    <OperationContract()>
    Function getTimezone(session As SessionValues) As List(Of Timezone)

    <OperationContract()>
    Function getTimezoneById(IdTimeZone As Integer) As Timezone

    ''' <summary>
    ''' Actualizar zona horia y formatos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateUserConfiguration(UserConfigurationCulture As UserConfigurationCulture) As Boolean
#End Region

#Region "Audit"

    ''' <summary>
    ''' Obtiene el numero total de impresiones y exportación de una entidad
    ''' </summary>
    ''' <param name="entityName">Nombre de la entidad</param>
    ''' <param name="entityKey">Id de la entidad</param>
    ''' <returns>Número total de impresiones</returns>
    <OperationContract()>
    Function GetTotalPrint(ByVal entityName As String, ByVal entityKey As Integer, session As SessionValues) As Integer

#End Region

#Region "Users"
    ''' <summary>
    ''' Cambia el estado del paciente
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function UpdateStatusUser(Id As String, Status As Boolean, audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Funcion para restaurar la contraseña de un usuario
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ResetPassword(ByVal UserCode As String) As String
    ''' <summary>
    ''' Funcion para cambiar el modo de visuzalizacion de los frontales
    ''' True=Busqueda
    ''' False=Edicion
    ''' </summary>
    ''' <param name="ViewMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeUserViewMode(ByVal UserCode As String, ByVal ViewMode As Boolean) As Boolean

    ''' <summary>
    ''' funcion para listar si el usuario tiene o no permisos para abrir el formulario como emergente
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPermissionsUserForm(CodeUser As String, CodeMenu As String, CodeOpcion As String, session As SessionValues) As Boolean

    ''' <summary>
    ''' Lista todos los ids de los formularios con permiso de visibilidad,
    ''' asignados al usuario y al rol que tiene el usuario
    ''' </summary>
    ''' <param name="tenantId">Id de tenant</param>
    ''' <param name="rollCode">Código de rol</param>
    ''' <param name="codeUser">Código del usuario</param>
    ''' <returns>Lista de ids de formularios</returns>
    <OperationContract()>
    Function ListPermissionFormsUser(tenantId As Short, rollCode As String, codeUser As String, action As String) As List(Of String)

    ''' <summary>
    ''' Lists the rol all.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListRolAll(session As SessionValues) As List(Of RolAll)


    ''' <summary>
    ''' Lists the groups all.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListGroupsAll(session As SessionValues) As List(Of GroupAll)

    ''' <summary>
    ''' Lista los tipos de telefono
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllPhoneType(session As SessionValues) As List(Of PhoneType)

    ''' <summary>
    ''' consulta los campos NULL para customizacion
    ''' </summary>
    ''' <returns></returns>

    <OperationContract()>
    Function GetFieldsNULLUsers(session As SessionValues) As DataSet


    ''' <summary>
    ''' Listar un usuario especifico
    ''' </summary>
    ''' <param name="codeUser">el Codigo del usuario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUserByCodeUser(ByVal codeUser As String, session As SessionValues, Optional email As String = Nothing) As User

    ''' <summary>
    ''' Listar un usuario especifico por codigo y container
    ''' </summary>
    ''' <param name="codeUser">el Codigo del usuario.</param>
    ''' <param name="session">session.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUserByCodeUserContainer(ByVal codeUser As String, session As SessionValues) As User

    ''' <summary>
    ''' Listar un usuario especifico con SQLCommand
    ''' </summary>
    ''' <param name="codeUser">el Codigo del usuario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUserCommand(ByVal codeUser As String, session As SessionValues) As User

    ''' <summary>
    ''' Listar un usuario especifico
    ''' </summary>
    ''' <param name="codeUser">el Codigo del usuario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUserByCodeUseObjectOnly(ByVal codeUser As String, session As SessionValues) As User

    ''' <summary>
    ''' Buscar los usuarios por nombre.
    ''' </summary>
    ''' <param name="name">el nombre.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListUsersByName(ByVal name As String, session As SessionValues) As List(Of User)

    ''' <summary>
    ''' Cambiar la contraseña de un usuario
    ''' </summary>
    ''' <param name="codeUser">se envia id de usario.</param>
    ''' <param name="lastPasswd">la contraseña anterior.</param>
    ''' <param name="newPasswd">la contraseña nueva.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangePassword(ByVal codeUser As String, ByVal lastPasswd As String, ByVal newPasswd As String, session As SessionValues) As Boolean

    ''' <summary>
    ''' Bloquear el usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UnlockUser(ByVal codeUser As String, session As SessionValues) As Boolean

    ''' <summary>
    ''' Validar el usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario</param>
    ''' <param name="passwd">la contraseña</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ValidateUser(ByVal codeUser As String, ByVal passwd As String, session As SessionValues) As Boolean

    ''' <summary>
    ''' Lista todos Los Permisos por Usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="codeMenu">el codigo del menu.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPermissionsUserByUserAndMenu(ByVal codeUser As String, codeMenu As String, session As SessionValues) As List(Of PermissionUser)

    ''' <summary>
    ''' Lista Los Permisos autorizados por Usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="codeMenu">el codigo del menu.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPermissionsUserAuthorized(ByVal codeUser As String, codeMenu As String, session As SessionValues) As List(Of PermissionUser)

    ''' <summary>
    ''' Lista Los Permisos sin autorizar por Usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="codeMenu">el codigo del menu.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPermissionsUserNoAuthorized(ByVal codeUser As String, codeMenu As String, session As SessionValues) As List(Of PermissionUser)

    ''' <summary>
    ''' Graba los Permisos para el Usuario.
    ''' </summary>
    ''' <param name="user">el usuario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePermissionsUser(ByVal user As User, session As SessionValues, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Elimina un usuario
    ''' </summary>
    ''' <param name="user">el usuario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteUsers(ByVal user As User, session As SessionValues, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista todos los permisos Usuarios.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPermissionsUserByCodeUser(ByVal codeUser As String, session As SessionValues) As List(Of PermissionUser)

    ''' <summary>
    ''' Lista El correo del Usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetEmailUser(ByVal codeUser As String, session As SessionValues) As String

    ''' <summary>
    ''' Obtiene la contraseña del usuario
    ''' </summary>
    ''' <param name="codeUser">se envia id de usario.</param>
    ''' <returns>Cadena vacía por seguridad (ISSUE-36107)</returns>
    <OperationContract()>
    Function GetPasswordUser(ByVal codeUser As String, session As SessionValues) As String


    ''' <summary>
    ''' Lista tdos los usuarios del sistema
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllUsers(session As SessionValues) As IEnumerable(Of User)

    ''' <summary>
    ''' consultar persona especifica
    ''' </summary>
    ''' <param name="identification">The identificacion.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPerson(ByVal identification As String, session As SessionValues) As Domain.Security.Entities.Person

    ''' <summary>
    ''' consultar persona especifica
    ''' </summary>
    ''' <param name="identification">The identificacion.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUserByIdentification(ByVal identification As Integer, session As SessionValues) As User


    ''' <summary>
    ''' Funcion para guardar info de usaurio
    ''' </summary>
    ''' <param name="_ArchivosProfesionales"></param>
    ''' <param name="session"></param>
    <OperationContract()>
    Function SaveFileUserRepository(_ArchivosProfesionales As FilePerson, session As SessionValues) As Boolean

    ''' <summary>
    ''' Gets the user by Id	
    ''' </summary>
    ''' <param name="Id">Id.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetUserById(Id As String, session As SessionValues) As User

    ''' <summary>
    ''' Lista todos los permisos de las unidades operativos
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <param name="idContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListPermissionOperatingUnit(ByVal codeUser As String, idContainer As Integer, session As SessionValues) As List(Of UserOperatingUnit)

    <OperationContract()>
    Function ListPermissionOperatingUnitCommand(ByVal codeUser As String, idContainer As Integer, session As SessionValues) As List(Of UserOperatingUnit)

    ''' <summary>
    ''' Guarda una permiso de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePermissionUser(permissionCompany As Company, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta todos los numeros telefonocos de una persona
    ''' </summary>
    ''' <param name="personId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPhoneByPersonId(personId As Integer) As List(Of Phone)

    ''' <summary>
    ''' Consulta todos los correos de una person
    ''' </summary>
    ''' <param name="personId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMailsByPersonId(personId As Integer) As List(Of Email)

    <OperationContract()>
    Function GetAddressByPersonId(personId As Integer) As List(Of Address)

    <OperationContract()>
    Function GetRollById(Id As Integer) As Roll

    <OperationContract()>
    Function GetPermissionUserByUserId(userId As Integer) As List(Of PermissionUser)

    ''' <summary>
    ''' Consulta el usuario por el correo electronico
    ''' </summary>
    ''' <param name="email"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUserByEmail(email As String, session As SessionValues) As User

    ''' <summary>
    ''' Obtiene el token de la firma electronica por ID de usuario
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetElectronicSignatureToken(userId As String) As String

    ''' <summary>
    ''' Guarda el token de la firma electronica luego de ser validada.
    ''' </summary>
    ''' <param name="token"></param>
    ''' <param name="userId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveElectronicSignatureToken(token As String, userId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene la ruta del informe para un usuario específico y un contenedor dado.
    ''' </summary>
    ''' <param name="IdUser"></param>
    ''' <param name="IdContainer"></param>
    ''' <param name="IdOperatingUnit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportPathByUser(IdUser As Integer, Optional IdContainer As Integer = 0, Optional IdOperatingUnit As Integer = 0) As String

#End Region

#Region "Groups"

    ''' <summary>
    ''' Elimina un Grupo
    ''' </summary>
    ''' <param name="group">El Grupo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteGroup(ByVal group As Group, session As SessionValues) As Boolean

    ''' <summary>
    ''' Listar todos los grupos.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListGroups(session As SessionValues) As IEnumerable(Of Group)

    ''' <summary>
    ''' Consultar un grupo
    ''' </summary>
    ''' <param name="codeGroup">el codigo del grupo.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGroup(ByVal codeGroup As String, session As SessionValues) As Group

    ''' <summary>
    ''' Graba un grupo
    ''' </summary>
    ''' <param name="group">el grupo.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveGroup(ByVal group As Group, dtDetails As DataTable, eliminados As List(Of Integer), ByVal session As SessionValues) As Boolean

#End Region

#Region "Roles"
    ''' <summary>
    ''' obtiene un rol y sus permisos dependiendo del tag del formulario
    ''' </summary>
    ''' <param name="rolId"></param>
    ''' <param name="IdForm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetRolByIdAndPermissionRollByIdForm(rolId As Integer, IdForm As String) As Roll

    ''' <summary>
    ''' Lista todos los roles
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListRoles(session As SessionValues) As IEnumerable(Of Roll)

    ''' <summary>
    ''' Consulta un Rol
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRoleByCodeRole(ByVal codeRole As String, session As SessionValues) As Roll

    ''' <summary>
    ''' elimina el rol y los Permisos para el Rol.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteRole(ByVal role As Roll, session As SessionValues) As Boolean

    ''' <summary>
    ''' Graba los Permisos para el Rol.
    ''' </summary>
    ''' <param name="permissionsRole">el listado de permisos.</param>
    ''' <param name="role">el rol.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePermissionsRole(ByVal role As Roll, session As SessionValues) As Boolean

    ''' <summary>
    ''' Lista todos Los Permisos por Rol.
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol.</param>
    ''' <param name="codeMenu">el codigo del menu.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPermissionsRoleByRoleAndMenu(ByVal codeRole As String, codeMenu As String, session As SessionValues) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista Los Permisos autorizados para ese Rol.
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol.</param>
    ''' <param name="codeMenu">el codigo del menu.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPermissionsRoleAuthorized(ByVal codeRole As String, codeMenu As String, session As SessionValues) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista Los Permisos sin autorizar para ese rol
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol.</param>
    ''' <param name="codeMenu">el codigo del menu.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPermissionsRoleNoAuthorized(ByVal codeRole As String, codeMenu As String, session As SessionValues) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista todos los permisos del rol.
    ''' </summary>
    ''' <param name="codeRole">el codigo del rol.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPermissionsRoleByCodeRole(codeRole As String, session As SessionValues) As List(Of PermissionRoll)

    ''' <summary>
    ''' Lista todos los formularios y acciones
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListForms(session As SessionValues) As List(Of VieForm)

    ''' <summary>
    ''' Lista todos los modulos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListModules(session As SessionValues) As List(Of VieModule)

#End Region

#Region "Toolbar"

    ''' <summary>
    ''' Lista Permisos Barra Usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <param name="codeRole">el codigo del rol.</param>
    ''' <param name="codeMenu">el codigo del menu.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListPermissionsUserToolbar(ByVal codeUser As String, codeRole As String, codeMenu As String, session As SessionValues) As List(Of PermissionUserToolbar)


    ''' <summary>
    ''' Lists the permissions user toolbar.	
    ''' </summary>
    ''' <param name="codeUser">The code user.</param>
    ''' <param name="codeRole">The code role.</param>
    ''' <returns>List of PermissionsFormsActive</returns>
    <OperationContract()>
    Function ListPermissionsFormsActive(codeUser As String, codeRole As String, session As SessionValues) As List(Of PermissionsFormsActive)


#End Region

#Region "UsersGroupUser"

    ''' <summary>
    ''' Guarda un usuario relacionado a un grupo en el chat
    ''' </summary>
    ''' <param name="userGroupUser">Usuario relacionado</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function SaveUsersGroupUser(ByVal userGroupUser As UsersGroupUser, ByVal audit As AuditMessage) As ActionResult(Of UsersGroupUser)

    ''' <summary>
    ''' Elimina un usuario relacionado a un grupo
    ''' </summary>
    ''' <param name="userGroupUser">Usuario relacionado</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function DeleteUsersGroupUser(ByVal userGroupUser As UsersGroupUser, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista los usuarios pertenecientes al grupo filtrandolos por el id del grupo
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>Lista de usuario del grupo</returns>
    <OperationContract()>
    Function ListByGroupId(id As Integer) As List(Of UsersGroupUser)

#End Region

#Region "GroupUser"

    ''' <summary>
    ''' Guarda un grupo del chat del usuario
    ''' </summary>
    ''' <param name="groupUser">Grupo a guardar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function SaveGroupUser(ByVal groupUser As GroupUser, ByVal audit As AuditMessage) As ActionResult(Of GroupUser)

    ''' <summary>
    ''' Elimina un grupo del chat del usuario
    ''' </summary>
    ''' <param name="groupUser">Grupo a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function DeleteGroupUser(ByVal groupUser As GroupUser, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por su id
    ''' </summary>
    ''' <param name="id">Id del grupo</param>
    ''' <returns>El grupo buscado</returns>
    <OperationContract()>
    Function GetGroupById(id As Integer) As GroupUser

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su codigo
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Lista de grupos</returns>
    <OperationContract()>
    Function ListByUsercode(userCode As String) As List(Of GroupUser)

    ''' <summary>
    ''' Lista los grupos del usuario friltrando por su id
    ''' </summary>
    ''' <param name="userId">Id del usuario</param>
    ''' <returns>Lista de grupos</returns>
    <OperationContract()>
    Function ListByUserId(userId As Integer) As List(Of GroupUser)

#End Region

#Region "FilePerson"

    ''' <summary>
    ''' Funcion  Para cargar Archivo Personales y validar 
    ''' </summary>
    ''' <param name="CodePerson">codigo de la persona</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFilePerson(CodePerson As String) As FilePerson

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userCode">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    <OperationContract()>
    Function GetFilePersonByUserCode(userCode As String) As FilePerson

    ''' <summary>
    ''' Obtiene el archivo personal del usuario
    ''' </summary>
    ''' <param name="userId">Codigo del usuario</param>
    ''' <returns>Archivo personal</returns>
    <OperationContract()>
    Function GetFilePersonByUserId(userId As Integer) As FilePerson

#End Region

#Region "Weather"

    ''' <summary>
    ''' Funcion para obtener los datos del clima
    ''' </summary>
    ''' <param name="nameCity">Nombre de la ciudad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetWeatherCity(woeidCity As String) As ActionMessageResult(Of WeatherDocument)

#End Region

#Region "ApplicationSetting"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="applicationSettings"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveApplicationSettings(ByVal applicationSettings As ApplicationSettings, ByVal audit As AuditMessage) As ActionResult(Of ApplicationSettings)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="containerId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetApplicationSettingsByContainerId(containerId As Integer) As ApplicationSettings

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGeneralConfiguration() As GeneralConfiguration
#End Region

#Region "ConfigurationFile"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="serviceConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveServiceConfiguration(ByVal serviceConfiguration As Domain.Security.Entities.ServiceConfiguration, ByVal audit As AuditMessage) As ActionResult(Of Domain.Security.Entities.ServiceConfiguration)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetServiceConfigurationById(id As Byte) As Domain.Security.Entities.ServiceConfiguration

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="userConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveUserConfiguration(ByVal userConfiguration As UserConfiguration, ByVal audit As AuditMessage) As ActionResult(Of UserConfiguration)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUserConfigurationByUserId(userId As Integer) As UserConfiguration

#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPerfilUbicacion(usuario As String) As ActionResult(Of SP_SEG_AutenticarUsuario_Result)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProfesional(usuario As String) As ActionResult(Of SP_SEG_AutenticarDatosProfesional_Result)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <param name="centroAtencion"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUnidadFuncionalAutorizado(usuario As String, grupo As String, centroAtencion As String) As ActionResult(Of List(Of SP_SEG_UnidadFuncional_Autorizado_Result))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCentroAtencionAutorizado(usuario As String, grupo As String) As ActionResult(Of List(Of SP_SEG_CentroAtencion_Autorizado_Result))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoRol"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPermisoRol(idMenu As String, codigoRol As String) As ActionResult(Of SEGpermir)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPermisoUsuario(idMenu As String, codigoUsuario As String) As ActionResult(Of SEGpermiu)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSubCupsImaging() As ActionResult(Of List(Of INCUPSSUB))
#End Region


#End Region

#Region "Menu"

#Region "frmProductCatalog"
    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="productCatalog"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveProductCatalog(ByVal productCatalog As ProductCatalog, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta un ProductCatalog
    ''' </summary>
    ''' <param name="IdProductCatalog">el codigo del rol.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProductCatalog(ByVal IdProductCatalog As Integer, session As SessionValues) As ProductCatalog

    ''' <summary>
    ''' elimina el ProductCatalog
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteProductCatalog(ByVal IdProductCatalog As Integer, session As SessionValues) As Boolean

    ''' <summary>
    ''' Cambia estado el ProductCatalog
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateProductCatalog(ByVal IdProductCatalog As Integer, ByVal state As Byte, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta listado de productos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListProductCatalog(session As SessionValues) As List(Of ProductCatalog)

#End Region

#Region "FrmModule"

    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="modules"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveModule(ByVal modules As Modules, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta un modulo
    ''' </summary>
    ''' <param name="IdModule">el codigo del modulo.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetModule(ByVal IdModule As Integer, session As SessionValues) As Modules

    ''' <summary>
    ''' elimina el modulo
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteModule(ByVal IdModule As Integer, session As SessionValues) As Boolean

    ''' <summary>
    '''  Cambia estado del modulo
    ''' </summary>
    ''' <param name="IdModule"></param>
    ''' <param name="state"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateModule(ByVal IdModule As Integer, ByVal state As Byte, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta listado de Modulos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAllModule(session As SessionValues) As List(Of Modules)

#End Region

#Region "FrmForm"

    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveForm(ByVal form As VieDBForm, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta un Formulario
    ''' </summary>
    ''' <param name="IdForm">el codigo del Formulario.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetForm(ByVal IdForm As Integer, session As SessionValues) As VieDBForm

    ''' <summary>
    ''' elimina el Formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteForm(ByVal IdForm As Integer, session As SessionValues) As Boolean

    ''' <summary>
    '''  Cambia estado del Formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="state"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateForm(ByVal IdForm As Integer, ByVal state As Byte, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta lista de FormAction por Formulario
    ''' </summary>
    ''' <param name="IdForm">el codigo del rol.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListFormActionByForm(ByVal IdForm As Integer, session As SessionValues) As List(Of FormAction)

    ''' <summary>
    ''' Lista de formularios para usar en funcionalidad de importar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListVieDBFormsImport(session As SessionValues) As List(Of VieDBForm)

    ''' <summary>
    ''' Guardar datos al Importar
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveFormImport(ByVal form As List(Of VieDBForm), session As SessionValues) As Boolean

#End Region

#Region "FrmTitle"

    ''' <summary>
    ''' Guardar o Actualizar
    ''' </summary>
    ''' <param name="title"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveTitle(ByVal title As Title, session As SessionValues) As Boolean

    ''' <summary>
    ''' Consulta un Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTitle(ByVal IdTitle As Integer, session As SessionValues) As Title

    ''' <summary>
    ''' elimina el Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteTitle(ByVal IdTitle As Integer, session As SessionValues) As Boolean

    ''' <summary>
    '''  Cambia estado del Titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="state"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateTitle(ByVal IdTitle As Integer, ByVal state As Byte, session As SessionValues) As Boolean

#End Region

#End Region

End Interface
