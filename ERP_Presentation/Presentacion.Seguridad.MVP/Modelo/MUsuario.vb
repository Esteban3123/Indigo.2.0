'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 27-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Security
'Imports DevExpress.Xpo
Imports System.Data
Imports Presentation.Base
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Domain.Crystal.Entities
Imports System.Threading.Tasks
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports DevExpress.Data.Linq

#End Region
''' <summary>
''' 	Modelo que sirve para establecer los servicios que se van a consumir en el funcional de usuarios
''' </summary>
Public Class MUsuario
    Implements IDisposable

    Private _SecurityDefault As Boolean
    Private _dsDatos As DataSet
    Property dsDatos As DataSet
        Get
            Return _dsDatos
        End Get
        Set(ByVal value As DataSet)
            _dsDatos = value
        End Set
    End Property

    ''' <summary>
    ''' Inicializa una nueva instancia de MfrmUnidadFuncional.
    ''' </summary>
    Sub New()
        'igualamos a los valores de sesion el nombre del formulario para poder auditar.
        If Indigo.AuditMessageWcf IsNot Nothing Then
            Indigo.AuditMessageWcf.Functional = Base.Eform.Usuario.ToString
        End If
    End Sub

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Public Indigo As SessionValues = SessionValues.Instance

#Region "Procesos"
    Public Sub IndigoConectaReset()
        IndigoConecta.Reset()
    End Sub
#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion para consultar todos los permisos de los usuarios por el codigo.
    ''' </summary>
    ''' <param name="codigoUsuario">CodigoUsuario as string</param>
    Public Async Function ConsultarTodosPermisosUsuarios(ByVal codigoUsuario As String) As Threading.Tasks.Task(Of List(Of PermissionUser))

        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserByCodeUserAsync(codigoUsuario, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion que me trae el listado de todos los centros de atencion
    ''' </summary>
    'Public Async Function ConsultarCentroDEAtencion() As Threading.Tasks.Task(Of List(Of CentroAtencion))
    '    Dim objetoCentroAtencion As New List(Of CentroAtencion)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetCareCenterAsync
    'End Function

    ''' <summary>
    ''' Funcion para consultar las unidades funcionales por por el codigo del centro de atencion
    ''' </summary>
    ''' <returns>retorna el resultado de la consulta</returns>
    'Public Async Function ConsultarUnidadFuncional(ByVal codigoCentro As String) As Threading.Tasks.Task(Of List(Of UnidadFuncional))
    '    Dim objetoUnidadFuncional As New List(Of UnidadFuncional)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUnitsFunctionalAsync(codigoCentro)
    'End Function

    ''' <summary>
    ''' Funcion que me trae el listado de todos los GRUPOS.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function BusquedaListadoGrupos() As Threading.Tasks.Task(Of List(Of GroupAll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListGroupsAllAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion que me trae el listado de todos los ROLES 
    ''' </summary>
    ''' <returns></returns>
    Friend Async Function BusquedaListaTodosRoles() As Threading.Tasks.Task(Of List(Of RolAll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListRolAllAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Consultar la Hora del Servidor.
    ''' </summary>
    ''' <returns></returns>
    Friend Async Function ConsultarHoraServidor() As Threading.Tasks.Task(Of Date)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <returns></returns>
    Public Async Function ConsultarUsuario(ByVal codigo As String, Optional email As String = Nothing) As Threading.Tasks.Task(Of User)
        Return Await ConsultarUsuarioCodigo(codigo, email)
    End Function

    ''' <summary>
    ''' Funcion para consultar el usuario por el codigo correspondiente.
    ''' </summary>
    ''' <param name="codigo">codigo as string</param>
    Public Async Function ConsultarUsuarioCodigo(ByVal codigo As String, Optional email As String = Nothing) As Threading.Tasks.Task(Of User)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUserAsync(codigo, Me.Indigo, email)
    End Function

    ''' <summary>
    ''' Funcion para consultar el usuario por el codigo y container correspondiente.
    ''' </summary>
    ''' <param name="codigo">codigo as string</param>
    Public Async Function ConsultarUsuarioCodigoContainerId(ByVal codigo As String) As Threading.Tasks.Task(Of User)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUserContainerAsync(codigo, Me.Indigo)
    End Function

    '''' <summary>
    '''' Funcion para consultar el usuario por el codigo y tenant correspondiente.
    '''' </summary>
    '''' <param name="codigo">codigo as string</param>
    'Public Async Function ConsultarUsuarioCodigoTenantId(ByVal codigo As String) As Threading.Tasks.Task(Of User)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeTenantIdAsync(codigo, Me.Indigo)
    'End Function

    ''' <summary>
    ''' Gets the user by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetUserById(ByVal Id As String) As Threading.Tasks.Task(Of User)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByIdAsync(Id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion que me trae el listado de los permisos de los USUARIOS dependiendo del codigo del ROL y del MENU.
    ''' </summary>
    Friend Async Function ConsultarPermisosUsuarios(ByVal codigoRol As String, ByVal codigoMenu As String) As Threading.Tasks.Task(Of List(Of PermissionUser))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserByUserAndMenuAsync(codigoRol, codigoMenu, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion que me trae el listado de Permisos de los ROLES por codigorol y CodigoMenu .
    ''' </summary>
    Friend Async Function ConsultarPermisosRoles(ByVal codigoRol As String, ByVal codigoMenu As String) As Threading.Tasks.Task(Of List(Of PermissionRoll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetPermissionsRoleAuthorizedAsync(codigoRol, codigoMenu, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Guardar el listado de objectos tipo Usuarios pidiendo como parametros el Usuario,Correo, Contraseña.
    ''' </summary>
    Public Async Function GuardarListadoUsuarios(ByVal usuario As User) As Threading.Tasks.Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SavePermissionsUserAsync(usuario, Me.Indigo, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Funcion para Eliminar el usuario seleccionado.
    ''' </summary>
    ''' <param name="permisoUsuarios">The permiso usuarios.</param>
    Public Async Function EliminarUsuarios(ByVal User As User) As Threading.Tasks.Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.DeleteUsersAsync(User, Me.Indigo, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta los permisos que tiene el rol.
    ''' </summary>
    Friend Async Function ConsultaPermisosRol(ByVal codigo As String, ByVal menu As String) As Threading.Tasks.Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetPermissionsRoleAuthorizedAsync(codigo, menu, Me.Indigo)
    End Function

    '''' <summary>
    '''' Funcion que se utiliza para consultar la contraseña del usuario
    '''' </summary>
    'Friend Async Function ConsultarContraseñaUsuario(ByVal codUsuario As String) As Threading.Tasks.Task(Of String)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetPasswordUserAsync(codUsuario, Me.Indigo)
    'End Function

    ''' <summary>
    ''' Funcion que se utiliza para consultar el Email del Usuario..
    ''' </summary>
    Friend Async Function ConsultarCorreoElectronicoUsuario(ByVal codUsuario As String) As Threading.Tasks.Task(Of String)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetEmailUserAsync(codUsuario, Me.Indigo)
    End Function


    ''' <summary>
    ''' Listar los campos nulls de la base de datos y porder customizar 
    ''' </summary>
    ''' <returns></returns>
    Async Function GetFieldsNULLUsers() As Threading.Tasks.Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetFieldsNULLUsersAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para cconsultar la identificacion de la persona
    ''' </summary>
    Public Async Function ConsultarPersona(ByVal Identificacion As String) As Threading.Tasks.Task(Of Person)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetPersonAsync(Identificacion, Me.Indigo)
    End Function

    Public Function GetGenesisForms() As List(Of VieForm)
        Return Presentation.Base.BaseClass.GetXmlWithAggregates(Of VieForm)(Base.eDataXml.XMLForms)
    End Function

    Public Function GetGenesisModules() As List(Of VieModule)
        Return BaseClass.GetXmlWithAggregates(Of VieModule)(Base.eDataXml.XMLModules)
    End Function

    ''' <summary>
    ''' Funcion que retorna las empresas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAllOperatingUnitAsync(transactionContainer As String) As Threading.Tasks.Task(Of List(Of Domain.Entities.OperatingUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllOperatingUnitAsync(Indigo, transactionContainer)
    End Function

    ''' <summary>
    ''' Funcion que retorna las empresas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllOperatingUnitSimple(transactionContainer As String) As List(Of Domain.Entities.OperatingUnit)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllOperatingUnit(Indigo, transactionContainer)
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    Private Function ListAllOperatingUnit() As List(Of Domain.Entities.OperatingUnit)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllOperatingUnitCommand(Indigo, Indigo.TransactionalContainer)
    End Function

    ''' <summary>
    ''' Lista las unidades operativas que tiene permiso un usuario
    ''' </summary>
    ''' <param name="idContainer"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Private Async Function ListPermissionsOperatingUnit(idContainer As Integer, codeUser As String) As Task(Of List(Of Domain.Security.Entities.UserOperatingUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionOperatingUnitCommandAsync(codeUser, idContainer, Indigo)
    End Function

    ''' <summary>
    ''' Lista unidades operativas por tipo de usuario
    ''' </summary>
    ''' <param name="idContainer"></param>
    ''' <param name="userType"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="adminCompany"></param>
    ''' <returns></returns>
    Public Async Function GetOperatingUnitByContainerPermission(idContainer As Integer, _UserType As Byte, codeUser As String, adminCompany As Boolean) As Task(Of List(Of Domain.Entities.OperatingUnit))
        'Dim listPermissionOperating As New List(Of Domain.Entities.OperatingUnit)
        Dim listPermissionOperatingUnit As List(Of Domain.Security.Entities.UserOperatingUnit) = Nothing
        Dim listOperatingUnit = ListAllOperatingUnit()

        Select Case CType(_UserType, UserType)
            Case UserType.CompanyAdmin
                'si el usuario es administrador en la compañia se asignan todas las unidades operativas
                If Not adminCompany Then
                    'unidades operativas que tiene permiso
                    listPermissionOperatingUnit = Await ListPermissionsOperatingUnit(idContainer, codeUser)
                End If
            Case UserType.StandardUser
                'unidades operativas que tiene permiso
                listPermissionOperatingUnit = Await ListPermissionsOperatingUnit(idContainer, codeUser)
        End Select
        Select Case CType(_UserType, UserType)
            Case UserType.GlobalAdmin, UserType.GlobalQa
                'listPermissionOperating.AddRange(listOperatingUnit)
                If listOperatingUnit IsNot Nothing Then
                    Return listOperatingUnit
                Else
                    Return New List(Of Domain.Entities.OperatingUnit)
                End If

            Case UserType.TenantAdmin
                'listPermissionOperating.AddRange(listOperatingUnit)
                If listOperatingUnit IsNot Nothing Then
                    Return listOperatingUnit
                Else
                    Return New List(Of Domain.Entities.OperatingUnit)
                End If
            Case UserType.CompanyAdmin
                'si el usuario es administrador en la compañia se asignan todas las unidades operativas
                If adminCompany Then
                    'listPermissionOperating.AddRange(listOperatingUnit)
                    If listOperatingUnit IsNot Nothing Then
                        Return listOperatingUnit
                    Else
                        Return New List(Of Domain.Entities.OperatingUnit)
                    End If
                Else
                    'unidades operativas que tiene permiso
                    If listOperatingUnit IsNot Nothing AndAlso listPermissionOperatingUnit IsNot Nothing Then
                        Dim linq = From _OperatingUnit In listOperatingUnit
                                   Join _PermissionOperatingUnit In listPermissionOperatingUnit'.Where(Function(_PermissionOperatingUnit) _PermissionOperatingUnit.Status)
                                       On _PermissionOperatingUnit.IdOperatingUnit Equals _OperatingUnit.Id
                                   Select _OperatingUnit
                        'listPermissionOperating.AddRange(linq)
                        Return linq.ToList
                    Else
                        Return New List(Of Domain.Entities.OperatingUnit)
                    End If
                End If
            Case UserType.StandardUser
                'unidades operativas que tiene permiso
                If listOperatingUnit IsNot Nothing AndAlso listPermissionOperatingUnit IsNot Nothing Then
                    Dim linq = From _OperatingUnit In listOperatingUnit
                               Join _PermissionOperatingUnit In listPermissionOperatingUnit'.Where(Function(_PermissionOperatingUnit) _PermissionOperatingUnit.Status)
                                   On _PermissionOperatingUnit.IdOperatingUnit Equals _OperatingUnit.Id
                               Select _OperatingUnit
                    'listPermissionOperating.AddRange(linq)
                    Return linq.ToList
                Else
                    Return New List(Of Domain.Entities.OperatingUnit)
                End If
            Case Else
                Return New List(Of Domain.Entities.OperatingUnit)
        End Select
    End Function

    ''' <summary>
    ''' Funcion que retorna las empresas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetCompanies() As Threading.Tasks.Task(Of List(Of Domain.Security.Entities.Containers))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getContainersAsync(Indigo)
    End Function

    ''' <summary>
    ''' Funcion que retorna las empresas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCompaniesSimple() As List(Of Domain.Security.Entities.Containers)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getContainers(Indigo)
    End Function

    Public Async Function UpdateStatus(Id As String, status As Integer) As Threading.Tasks.Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.UpdateStatusUserAsync(Id, status, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function GetRoles() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.SecurityContainer).SecurityService.GetRoles(Indigo.UserType)
    End Function

    Public Function ConsultarTelefonosPersona(idPerson As Integer) As List(Of Phone)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetPhoneByPersonId(idPerson)
    End Function

    Public Function ConsultarMailsPersona(idPerson As Integer) As List(Of Email)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetMailsByPersonId(idPerson)
    End Function

    Public Function ConsultarFilePerson(idPerson As Integer) As FilePerson
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetFilePersonByUserId(idPerson)
    End Function

    Public Function ConsultarDirecciones(idPerson As Integer) As List(Of Address)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetAddressByPersonId(idPerson)
    End Function

    Public Function ConsultarRoll(rollcode As String) As Roll
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetRoleByCodeRole(rollcode, Me.Indigo)
    End Function

    Public Async Function GetUserByEmail(email As String) As Task(Of User)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByEmailAsync(email, Me.Indigo)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="UserTenantId"></param>
    ''' <param name="pUserType"></param>
    ''' <returns></returns>
    Public Function GetAllTenant(ByVal UserId As Integer, ByVal pUserType As UserType) As XPServerCollectionSource
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.GetAllTenant(UserId, pUserType)
    End Function

    ''' <summary>
    ''' lista todos los roles por tenant
    ''' </summary>
    ''' <param name="tenantId"></param>
    ''' <returns></returns>
    Public Function ListTenantRoll(ByVal tenantId As Short) As List(Of TenantRollXpo)
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.ListTenantRoll(tenantId)
    End Function

    ''' <summary>
    ''' lista todos los grupos por tenant
    ''' </summary>
    ''' <param name="tenantId"></param>
    ''' <returns></returns>
    Public Function ListTenantGroup(ByVal tenantId As Short) As List(Of TenantGroupXpo)
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.ListTenantGroup(tenantId)
    End Function

    ''' <summary>
    ''' lista roles globales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGlobalRolls() As XPServerCollectionSource
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.ListGlobalRolls()
    End Function

    ''' <summary>
    ''' lista grupos globales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGlobalGroups() As XPServerCollectionSource
        Return XpoServiceEx.Instance(Me.Indigo.SecurityContainer).SecurityService.ListGlobalGroups()
    End Function

    ''' <summary>
    ''' Este metodo consulta el listado de formularios con acciones.
    ''' </summary>
    Public Async Function ConsultarTodosForms() As Threading.Tasks.Task(Of List(Of VieForm))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListFormsAsync(Me.Indigo)
    End Function

#Region "EHR"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <param name="centroAtencion"></param>
    ''' <returns></returns>
    Public Async Function GetUnidadFuncionalAutorizado(usuario As String, grupo As String, centroAtencion As String) As Threading.Tasks.Task(Of ActionResult(Of List(Of SP_SEG_UnidadFuncional_Autorizado_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUnidadFuncionalAutorizadoAsync(usuario, grupo, centroAtencion)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="grupo"></param>
    ''' <returns></returns>
    Public Async Function GetCentroAtencionAutorizado(usuario As String, grupo As String) As Threading.Tasks.Task(Of ActionResult(Of List(Of SP_SEG_CentroAtencion_Autorizado_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetCentroAtencionAutorizadoAsync(usuario, grupo)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoRol"></param>
    ''' <returns></returns>
    Public Async Function GetPermisoRol(idMenu As String, codigoRol As String) As Threading.Tasks.Task(Of ActionResult(Of SEGpermir))
        _SecurityDefault = True
        Return Await IndigoConecta.InstanciaDefault.CurrentCloud.IndigoSeguridadDefault.GetPermisoRolAsync(idMenu, codigoRol)
    End Function

    ''' <summary>
    ''' Consultar los permisos que tiene el usuario.
    ''' </summary>
    Public Async Function ConsultarPermisosUsuario(ByVal codigoUsuario As String, ByVal codigoRol As String, ByVal codigoFormulario As String) As Task(Of List(Of PermissionUserToolbar))
        '_SecurityDefault = True
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbarAsync(codigoUsuario, codigoRol, codigoFormulario, Indigo)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="idMenu"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <returns></returns>
    Public Async Function GetPermisoUsuario(idMenu As String, codigoUsuario As String) As Threading.Tasks.Task(Of ActionResult(Of SEGpermiu))
        _SecurityDefault = True
        Return Await IndigoConecta.InstanciaDefault.CurrentCloud.IndigoSeguridadDefault.GetPermisoUsuarioAsync(idMenu, codigoUsuario)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetSubCupsImaging() As Threading.Tasks.Task(Of ActionResult(Of List(Of INCUPSSUB)))
        _SecurityDefault = True
        Return Await IndigoConecta.InstanciaDefault.CurrentCloud.IndigoSeguridadDefault.GetSubCupsImagingAsync()
    End Function

    ''' <summary>
    ''' Obtiene el token de la firma electronica del usuario si existe
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Function GetElectronicSignatureToken(userId As Integer) As String
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetElectronicSignatureToken(userId)
    End Function

    ''' <summary>
    ''' Obtiene el token de la firma electronica del usuario si existe, de manera asincrona
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Async Function GetElectronicSignatureTokenAsync(userId As Integer) As Task(Of String)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetElectronicSignatureTokenAsync(userId)
    End Function

    ''' <summary>
    ''' Guarda el token de la firma electronica del usuario
    ''' </summary>
    ''' <param name="token"></param>
    ''' <param name="userId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveElectronicSignatureToken(token As String, userId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveElectronicSignatureTokenAsync(token, userId, Me.Indigo.AuditMessageWcf)
    End Function

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If
            If _SecurityDefault Then
                IndigoConecta.Reset()
            End If
            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
