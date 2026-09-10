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
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region

''' <summary>
''' Interface con los metodos necesarios 
''' para procesar la entidad usuario
''' </summary>
Public Interface IUserAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Funcion para restaurar la contraseña de un usuario
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ResetPassword(ByVal UserCode As String) As String

    ''' <summary>
    ''' Funcion para cambiar el modo de visuzalizacion de los frontales
    ''' True=Busqueda
    ''' False=Edicion
    ''' </summary>
    ''' <param name="ViewMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeUserViewMode(ByVal UserCode As String, ByVal ViewMode As Boolean) As Boolean

    ''' <summary>
    ''' Lists the permissions user form.
    ''' </summary>
    ''' <param name="CodeUser">The code user.</param>
    ''' <param name="CodeMenu">The code menu.</param>
    ''' <param name="CodeOpcion">The code opcion.</param>
    ''' <param name="tenantId">Id de tenant.</param>
    ''' <returns></returns>
    Function ListPermissionsUserForm(CodeUser As String, CodeMenu As String, CodeOpcion As String, tenantId As Short) As Boolean
    ''' <summary>
    ''' consulta los campos NULL para customizacion
    ''' </summary>
    ''' <returns></returns>
    Function ConsultarCamposNULL() As DataSet

    ''' <summary>
    ''' Listar todos los usuarios
    ''' </summary>
    ''' <returns></returns>
    Function ListAllUsers() As IEnumerable(Of User)

    ''' <summary>
    ''' Listar todos los usuarios
    ''' </summary>
    ''' <returns></returns>
    Function ListAllOnlineUser() As Integer

    ''' <summary>
    ''' Modificar un Usuario
    ''' </summary>
    ''' <param name="user">entidad usuario</param>
    Function UpdateUser(ByVal user As User, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Listar un usuario especifico
    ''' </summary>
    ''' <param name="codeUser">el Codigo del usuario.</param>
    ''' <returns></returns>
    Function GetUser(ByVal codeUser As String, ByVal CompanyCode As String, Optional email As String = Nothing) As User

    ''' <summary>
    ''' Listar un usuario especifico
    ''' </summary>
    ''' <param name="codeUser">el Codigo del usuario.</param>
    ''' ''' <param name="containerId">id de container.</param>
    ''' <returns></returns>
    Function GetUser(ByVal codeUser As String, ByVal containerId As Integer) As User

    Function ListUsersByCodes(ByVal listCodes As List(Of String)) As List(Of User)

    Function ListUsersByIds(ByVal listIds As List(Of Integer)) As List(Of User)

    ''' <summary>
    ''' Listar un usuario especifico sin agregados
    ''' </summary>
    ''' <param name="codeUser">el Codigo del usuario.</param>
    ''' <returns></returns>
    Function GetUserObjectOnly(ByVal codeUser As String) As User

    ''' <summary>
    ''' Buscar los usuarios por nombre.
    ''' </summary>
    ''' <param name="name">el nombre.</param>
    ''' <returns></returns>
    Function FindUserByName(ByVal name As String) As List(Of User)

    ''' <summary>
    ''' Cambiar la contraseña de un usuario
    ''' </summary>
    ''' <param name="codeUser">se envia id de usario.</param>
    ''' <param name="lastPasswd">la contraseña anterior.</param>
    ''' <param name="newPasswd">la contraseña nueva.</param>
    ''' <returns></returns>
    Function ChangePassword(ByVal codeUser As String, ByVal lastPasswd As String, ByVal newPasswd As String) As Boolean

    ''' <summary>
    ''' Bloquear el usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario</param>
    ''' <returns></returns>
    Function UnlockUser(ByVal codeUser As String) As Boolean

    ''' <summary>
    ''' Validar el usuario.
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario</param>
    ''' <param name="passwd">la contraseña.</param>
    ''' <returns></returns>
    Function ValidateUser(ByVal codeUser As String, ByVal passwd As String) As Boolean

    ''' <summary>
    ''' Lista Los Permisos por Usuario. autorizados y no autorizados
    ''' </summary>
    ''' <param name="codeMenu">el codigo del menu</param>
    ''' <param name="codeUser">el codigo del usuario</param>
    ''' <param name="authorized">True= Si, False=No</param>
    ''' <param name="tenantId">id de tenant</param>
    ''' <returns></returns>
    Function ListPermissionsUser(ByVal codeUser As String, ByVal codeMenu As String, ByVal authorized As Boolean, codeCompany As String, tenantId As Short) As List(Of PermissionUser)

    ''' <summary>
    ''' Lista todos Los Permisos por Usuario.
    ''' </summary>
    ''' <param name="codeMenu">el codigo del menu</param>
    ''' <param name="codeUser">el codigo del usuario</param>
    ''' <returns></returns>
    Function ListPermissionsUser(ByVal codeUser As String, ByVal codeMenu As String, codeCompany As String) As List(Of PermissionUser)

    ''' <summary>
    ''' Lista todos los permisos del rol.
    ''' </summary>
    ''' <param name="codeUsers">el codigo del usuario</param>
    ''' <returns></returns>
    Function ListPermissionsUser(ByVal codeUsers As String, codeCompany As String) As List(Of PermissionUser)

    ''' <summary>
    ''' Lista todos los ids de los formularios con permiso solicitado,
    ''' asignados al usuario y al rol que tiene el usuario
    ''' </summary>
    ''' <param name="tenantId">id de tenant</param>
    ''' <param name="rollCode">codigo de rol</param>
    ''' <param name="codeUser">Código del usuario</param>
    ''' <returns>Lista de ids de formularios</returns>
    Function ListPermissionFormsUser(tenantId As Short, rollCode As String, codeUser As String, action As String) As List(Of String)

    ''' <summary>
    ''' Lista todos los permisos de las unidades operativos
    ''' </summary>
    ''' <param name="codeUser"></param>
    ''' <param name="idContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListPermissionOperatingUnit(ByVal codeUser As String, idContainer As Integer) As List(Of UserOperatingUnit)

    ''' <summary>
    ''' Lista el email del usuario
    ''' </summary>
    ''' <param name="codeUser">el codigo del usuario.</param>
    ''' <returns></returns>
    Function GetEmailUser(ByVal codeUser As String) As String

    ''' <summary>
    ''' Lista la contraseña del Usuario.
    ''' </summary>
    ''' <param name="codeUser">se envia id de usario.</param>
    ''' <returns></returns>
    Function GetPasswordUser(ByVal codeUser As String) As String

    ''' <summary>
    ''' consultar persona especifica
    ''' </summary>
    ''' <param name="identification">The identification.</param>
    ''' <returns></returns>
    Function GetPerson(ByVal identification As String) As Person

    ''' <summary>
    ''' consultar persona especifica
    ''' </summary>
    ''' <param name="identification">The identification.</param>
    ''' <returns></returns>
    Function GetUser(ByVal identification As Integer) As User

    ''' <summary>
    ''' Funcion para guardar info de usaurio
    ''' </summary>
    ''' <param name="_ArchivosProfesionales"></param>
    ''' <param name="audit"></param>
    Function SaveFileUserRepository(_ArchivosProfesionales As FilePerson, audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Gets the user by Id	
    ''' </summary>
    ''' <param name="Id">Id.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUserById(Id As String, audit As AuditMessage) As User

    ''' <summary>
    ''' Guarda una permiso de usuario
    ''' </summary>
    ''' <param name="permissionUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePermissionUser(permissionUser As Company, session As SessionValues) As Boolean

    ''' <summary>
    ''' Cambia el estado del paciente
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateStatusUser(Id As String, Status As Boolean, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista todos los tipos de niveles
    ''' </summary>
    ''' <returns>Lista de tipos de niveles</returns>
    ''' <remarks></remarks>
    Function ListAllPhoneType() As List(Of PhoneType)

    ''' <summary>
    ''' Consulta todos los numeros telefonocos de una persona
    ''' </summary>
    ''' <param name="personId"></param>
    ''' <returns></returns>
    Function GetPhoneByPersonId(personId As Integer) As List(Of Phone)

    ''' <summary>
    ''' Consulta todos los correos de una person
    ''' </summary>
    ''' <param name="personId"></param>
    ''' <returns></returns>
    Function GetMailsByPersonId(personId As Integer) As List(Of Email)

    Function GetAddressByPersonId(personId As Integer) As List(Of Address)

    Function GetRollById(Id As Integer) As Roll

    Function GetPermissionUserByUserId(userId As Integer) As List(Of PermissionUser)

    ''' <summary>
    ''' Consulta el usuario por el correo electronico
    ''' </summary>
    ''' <param name="email"></param>
    ''' <returns></returns>
    Function GetUserByEmail(email As String) As User

    '''' <summary>
    '''' Consulta usuario por codigo y id de tenant
    '''' </summary>
    '''' <param name="codeUser">codigo de usuario.</param>
    '''' <param name="tenantId">id de tenant.</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Function GetUserByCodeTenantId(codeUser As String, ByVal tenantId As Short) As User

    ''' <summary>
    ''' Guarda el token de la firma electronica del usuario luego de ser validada
    ''' </summary>
    ''' <returns></returns>
    Function SaveElectronicSignatureToken(token As String, userId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Validación del token de la firma electronica con el tercero
    ''' </summary>
    ''' <returns></returns>
    Function ValidateElectronicSignatureToken(token As String) As ActionResult

    ''' <summary>
    ''' Obtiene el token de la firma electronica de un usuario si este existe
    ''' </summary>
    ''' <param name="userId">ID del usuario</param>
    ''' <returns></returns>
    Function GetElectronicSignatureTokenByUserId(userId As Integer) As String

    ''' <summary>
    ''' Obtiene la ruta del informe para un usuario específico, un contenedor dado y una unidad operativa.
    ''' </summary>
    ''' <param name="IdUser"></param>
    ''' <param name="IdContainer"></param>
    ''' <param name="IdOperatingUnit"></param>
    ''' <returns></returns>
    Function GetReportPathByUser(IdUser As Integer, Optional IdContainer As Integer = 0, Optional IdOperatingUnit As Integer = 0) As String
#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    Function GetPerfilUbicacion(usuario As String) As ActionResult(Of SP_SEG_AutenticarUsuario_Result)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    Function GetProfesional(usuario As String) As ActionResult(Of SP_SEG_AutenticarDatosProfesional_Result)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <param name="centroAtencion"></param>
    ''' <returns></returns>
    Function GetUnidadFuncionalAutorizado(usuario As String, grupo As String, centroAtencion As String) As ActionResult(Of List(Of SP_SEG_UnidadFuncional_Autorizado_Result))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <returns></returns>
    Function GetCentroAtencionAutorizado(usuario As String, grupo As String) As ActionResult(Of List(Of SP_SEG_CentroAtencion_Autorizado_Result))

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Function GetSubCupsImaging() As ActionResult(Of List(Of INCUPSSUB))
#End Region

End Interface
