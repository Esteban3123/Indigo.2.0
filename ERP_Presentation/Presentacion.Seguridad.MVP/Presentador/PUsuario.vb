'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 03-05-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias importadas"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Domain.Security.Entities
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal FrmUsuario
''' </summary>
Public Class PUsuario
#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IUsuario
    ''' </summary>
    Dim vista As IUsuario

    ''' <summary>
    ''' Variable que se Utiliza para instanciar la entidad SeguridadUsuario
    ''' </summary>
    Dim UsuarioInsert As New User
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Varible que se utiliza para instanciar la entidades SeguridadGrupoUsuario 
    ''' </summary>
    Dim grupo As New Group
    ''' <summary>
    ''' Variable que se utiliza  para instanciar la entidad SeguridadRolesUsuario
    ''' </summary>
    Dim rol As New Roll
    ''' <summary>
    ''' Variable que instancia le entidad persona.
    ''' </summary>
    Dim personas As Person
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' 
    Public Property Usuario As User
        Get
            Return vista.User
        End Get
        Set(value As User)
            vista.User = value
        End Set
    End Property

    Private _Form As List(Of VieForm)
    Property ListForm As List(Of VieForm)
        Get
            Return _Form
        End Get
        Set(value As List(Of VieForm))
            _Form = value
        End Set
    End Property

    Public Sub New(ByRef iview As IUsuario)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.vista = iview
        End If
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo que me consulta la hora del servidor .
    ''' </summary>
    Public Async Sub ConsultarHoraServidor()
        Using modelo As New MUsuario
            vista.FechaServidor = Await modelo.ConsultarHoraServidor
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para bloquear los controles del funcional al realizar una accion
    ''' </summary>
    Public Sub BloquearControles()
        With vista
            .AccionesSobreLosControles = False
            .CodigoDelUsuario = String.Empty
            .NombreDelUsuario = String.Empty
            .RolValueMember = Nothing
            .ContraseñaDelUsuario = String.Empty
            .ContraseñaConfirmada = String.Empty
            .GrupoValueMember = String.Empty
            .Cargo = String.Empty
            .FechaCaducidadContraseña = Nothing
            .TiempoCaducidadContraseña = Nothing
            .ExigirCambioContraseña = Nothing
        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utilizar para inicializar componentes necesarios al cargar el formulario 
    ''' </summary>
    Async Function Inicializar() As Threading.Tasks.Task
        vista.AsyncLoader(True)
        Using modelo As New MUsuario
            '   vista.DatasourcePermisosUsuarios = Await modelo.ConsultarModuloRoles
            vista.FuenteDEDatosGrupos = Await modelo.BusquedaListadoGrupos
            'vista.RolDatasource = Await modelo.BusquedaListaTodosRoles
        End Using
        vista.AsyncLoader(False)
    End Function



    ''' <summary>
    ''' Propiedad que se utiliza para verificar en la vista si se guardo correctamente el USUARIO 
    ''' </summary>
    Private _fallo As Boolean
    ''' <summary>
    ''' Obtiene o establece si el usuario fue guardado correctamente
    ''' </summary>
    Property Aceptar As Boolean
        Get
            Return _fallo
        End Get
        Set(ByVal value As Boolean)
            _fallo = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene la lista de tenant autorizados segun el tipo de usuario
    ''' </summary>
    Public Sub InitializeTenant()
        Using modelo As New MUsuario
            vista.Tenants = modelo.GetAllTenant(Indigo.UserIndigoId, Indigo.UserType)
        End Using
    End Sub

    ''' <summary>
    ''' lista todos los roles por tenant
    ''' </summary>
    ''' <param name="tenantId"></param>
    Public Sub ListRolls(ByVal tenantId As Short)
        Using modelo As New MUsuario
            vista.TenantRolls = modelo.ListTenantRoll(tenantId)
        End Using
    End Sub

    ''' <summary>
    ''' lista todos los grupos por tenant y tipo de usuario, si el usuario es global consulta grupos globales, si el usuario no es global consulta grupos por tenant
    ''' </summary>
    ''' <param name="tenantId"></param>
    Public Sub ListGroups(ByVal tenantId As Short)
        Using modelo As New MUsuario
            vista.TenantGroups = modelo.ListTenantGroup(tenantId)
        End Using
    End Sub

    ''' <summary>
    ''' Este metodo consulta el listado de formularios con acciones.
    ''' </summary>
    Public Async Function ConsultarTodosForms() As Threading.Tasks.Task
        Dim modelo As New MUsuario
        ListForm = Await modelo.ConsultarTodosForms()
    End Function

#End Region

#Region "metodos de CRUD"

    ''' <summary>
    ''' Variable privada para la propiedad UsuarioExiste
    ''' </summary>
    Private _UsarioExiste As Boolean
    ''' <summary>
    ''' Obtiene o establece si el usuario existe.	
    ''' </summary>
    ''' <value>el usuario existe.</value>
    Property UsuarioExiste As Boolean
        Get
            Return _UsarioExiste
        End Get
        Set(ByVal value As Boolean)
            _UsarioExiste = value
        End Set
    End Property

    '''' <summary>
    '''' Metodo que se utiliza para COnsultar el usuario por codigo y cargarme los campos correspondientes.
    '''' </summary>
    '''' <param name="codigo">El codigo del usuario</param>
    'Public Async Function ConsultarUsuario(ByVal codigo As String) As Threading.Tasks.Task
    '    'Habilitamos controles de la vista para cargar datos consultados
    '    vista.AccionesSobreLosControles = True
    '    Using modelo As New MUsuario
    '        Usuario = Await modelo.ConsultarUsuarioCodigo(codigo, True)
    '    End Using
    '    With vista
    '        ' si el usuario no es vacio entonces realizar 
    '        If Not Usuario Is Nothing Then
    '            If Usuario.Id > 0 Then
    '                'como el usuario existe, entonces bloqueamos los controles de seguridad.
    '                .ActivarGrupoSeguridad = False
    '                'UsuarioExiste = True
    '                .LogicaBotonActualizar(True)
    '                'Rol
    '                .RolValueMember = Usuario.RollCode.ToString.Trim
    '                'Correo usuario
    '                ' .CorreoElectronicoDelUsuario = modelo.ConsultarCorreoElectronicoUsuario(vista.CodigoDelUsuario)
    '                'Grupo Usuario
    '                .GrupoValueMember = Usuario.GroupCode.ToString.Trim
    '                'Cargo
    '                .Cargo = Usuario.Position.ToString.Trim
    '                'Solicitar cambio contraseña
    '                .ExigirCambioContraseña = CBool(Usuario.ChangePassword)
    '                'Periodo de caducidad
    '                If Usuario.DaysChangePassword Is Nothing Then
    '                    .TiempoCaducidadContraseña = String.Empty
    '                Else
    '                    .TiempoCaducidadContraseña = CStr(Usuario.DaysChangePassword)
    '                End If
    '                'Fecha de caducidad
    '                If Usuario.DateExpiryAccount IsNot Nothing Then
    '                    .FechaCaducidadContraseña = Usuario.DateExpiryAccount.Value
    '                Else
    '                    .FechaCaducidadContraseña = CDate(Nothing)
    '                End If
    '                .HabilitarGrupoContraseña = False
    '                .ActivarGrupoSeguridad = True
    '                GetPermissionUser()
    '            Else
    '                .ActivarGrupoSeguridad = True
    '                vista.LogicaBotonActualizar(False)
    '                vista.HabilitarGrupoContraseña = True
    '            End If
    '        Else
    '            .ActivarGrupoSeguridad = True
    '            vista.LogicaBotonActualizar(False)
    '            vista.HabilitarGrupoContraseña = True
    '        End If
    '    End With
    'End Function

    ''' <summary>
    ''' Variable privada para la propiedad SeValidoCorrectamente
    ''' </summary>
    Private _SeValidoCorrectamente As Boolean
    ''' <summary>
    ''' Obtiene o establece si se valido correctamente.	
    ''' </summary>
    ''' <remarks></remarks>
    Property SEValidoCorrectamente As Boolean
        Get
            Return _SeValidoCorrectamente
        End Get
        Set(ByVal value As Boolean)
            _SeValidoCorrectamente = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo que se utiliza para validar el usuario antes de disparar el metodo guardar.
    ''' </summary>
    Sub ValidarUsuarioParaGuardar(ByVal guardarUsuario As List(Of PermissionUser))
        With vista
            ''validamos que los campo esten completamente diligenciados
            'If Not .TipoPerfilSeleccionado = Nothing Then
            '    If CDbl(.TipoPerfilSeleccionado) = 2 Then
            '        'validamos campos cion respecto al perfil asistencia
            '        If vista.CodigoDelUsuario = String.Empty Or vista.NombreDelUsuario = String.Empty Or vista.DefinirActivoInactivo Is Nothing Or _
            '           vista.RolValueMember = String.Empty Or vista.CorreoElectronicoDelUsuario = String.Empty Or
            '         vista.Cargo = String.Empty Or vista.Sip = String.Empty Then
            '            .Mensaje(EeventViewerImages.Advertencia) = "Se Requieren Datos Adicionales al Perfil Asistencial"
            '            SEValidoCorrectamente = False
            '        Else
            '            SEValidoCorrectamente = True
            '            Guardar(guardarUsuario)
            '            Exit Sub
            '        End If
            '    Else
            '        'validando los campos con respecto al perfil administrativo
            '        If .CodigoDelUsuario = String.Empty Or .NombreDelUsuario = String.Empty Or _
            '          .DefinirActivoInactivo Is Nothing Or .RolValueMember = String.Empty Or .CorreoElectronicoDelUsuario = String.Empty Or _
            '           .Cargo = String.Empty Or .Sip = String.Empty Then
            '            .Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesDigiteDatos)
            '            SEValidoCorrectamente = False
            '        Else
            '            SEValidoCorrectamente = True
            '            Guardar(guardarUsuario)
            '            Exit Sub
            '        End If
            '    End If
            'Else
            '    .Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesDigiteDatos)
            'End If
        End With
        If Not vista.FechaCaducidadContraseña = Nothing Then
            If vista.FechaCaducidadContraseña < vista.FechaServidor Then
                vista.Mensaje(EeventViewerImages.Advertencia) = "La Fecha de Caducacion de la Cuenta no Puede ser Menor a la Actual"
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para Guardar el USUARIO
    ''' </summary>
    Public Async Sub Guardar(ByVal guardarUsuario As List(Of PermissionUser))
        Dim resultado As ActionResult
        If Not Usuario Is Nothing Then
            'si el usuario existe se envian los datos y se modifican los datos correspodientes
            If vista.ContraseñaDelUsuario.Length > 0 Then
                If vista.ContraseñaDelUsuario = vista.ContraseñaConfirmada And vista.ContraseñaDelUsuario.Length >= 7 Then

                    Usuario.Person.Fullname = vista.NombreDelUsuario.ToString.Trim
                    Usuario.RollCode = Integer.Parse(vista.RolValueMember)
                    Usuario.Password = vista.ContraseñaDelUsuario
                    Usuario.GroupCode = Integer.Parse(vista.GrupoValueMember.Trim)
                    Usuario.Position = vista.Cargo
                    Usuario.Person.Fingerprint = vista.Huella
                    'Tipo de Perfil (Administrativo,Asistencial)
                    Usuario.UserType = "1"

                    Usuario.ChangePassword = vista.ExigirCambioContraseña

                    If vista.TiempoCaducidadContraseña = String.Empty Then
                        Usuario.DaysChangePassword = Nothing
                    Else
                        Usuario.DaysChangePassword = CType(vista.TiempoCaducidadContraseña, Integer?)
                    End If

                    'Fecha caducidad
                    If Not vista.FechaCaducidadContraseña = Nothing Then
                        Usuario.DateExpiryAccount = CType(vista.FechaCaducidadContraseña, DateTime)
                    End If
                    vista.AsyncLoader(True)
                    Using modelo As New MUsuario
                        resultado = Await modelo.GuardarListadoUsuarios(Usuario)
                    End Using
                    vista.AsyncLoader(False)
                    If resultado.StateResult = False Then
                        vista.Mensaje(EeventViewerImages.MensajeError) = BaseClass.obtenerExcepcion(EexceptionsResources.LoginUsuarioSinDatos)
                        Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.LoginUsuarioSinDatos))
                        'FALTA CAPA EXCEPCIONES
                        _fallo = True
                    Else
                        vista.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                        BloquearControles()
                    End If
                Else
                    vista.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ContrasenasNoCoinciden, CambiarContrasena)
                    Exit Sub
                End If
            Else

                vista.ContraseñaDelUsuario = vista.ContraseñaConfirmada
                Usuario.Person.Fullname = vista.NombreDelUsuario.ToString.Trim
                Usuario.RollCode = Integer.Parse(vista.RolValueMember)
                Usuario.Password = vista.ContraseñaDelUsuario
                Usuario.GroupCode = Integer.Parse(vista.GrupoValueMember)
                Usuario.Position = vista.Cargo
                Usuario.Person.Fingerprint = vista.Huella
                'Tipo de Perfil (Administrativo,Asistencial)
                Usuario.UserType = "1"


                Usuario.ChangePassword = vista.ExigirCambioContraseña

                If vista.TiempoCaducidadContraseña = String.Empty Then
                    Usuario.DaysChangePassword = Nothing
                Else
                    Usuario.DaysChangePassword = CType(vista.TiempoCaducidadContraseña, Integer?)
                End If

                'Fecha caducidad
                If vista.FechaCaducidadContraseña = Nothing Then
                    Usuario.DateExpiryAccount = Nothing
                Else
                    Usuario.DateExpiryAccount = vista.FechaCaducidadContraseña
                End If
                vista.AsyncLoader(True)
                Using modelo As New MUsuario
                    resultado = Await modelo.GuardarListadoUsuarios(Usuario)
                End Using
                vista.AsyncLoader(False)
                If resultado.StateResult = False Then
                    vista.Mensaje(EeventViewerImages.MensajeError) = BaseClass.obtenerExcepcion(EexceptionsResources.LoginUsuarioSinDatos)
                    Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.LoginUsuarioSinDatos))
                    _fallo = True
                Else
                    vista.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    BloquearControles()
                End If
            End If
        Else
            'si el usuario no existe  hace el respctivo insert y sus validaciones
            If vista.ContraseñaDelUsuario = String.Empty Or vista.ContraseñaDelUsuario <> vista.ContraseñaConfirmada Or vista.ContraseñaDelUsuario.Length < 7 Then
                vista.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ContrasenasNoCoinciden, CambiarContrasena)
                Exit Sub

            Else
                UsuarioInsert.UserCode = vista.CodigoDelUsuario.ToString.Trim
                UsuarioInsert.Person.Fullname = vista.NombreDelUsuario.ToString.Trim
                UsuarioInsert.RollCode = Integer.Parse(vista.RolValueMember)
                UsuarioInsert.GroupCode = Integer.Parse(vista.GrupoValueMember)
                UsuarioInsert.Position = vista.Cargo
                'Tipo de Perfil (Administrativo,Asistencial)
                UsuarioInsert.UserType = "1"

                UsuarioInsert.ChangePassword = vista.ExigirCambioContraseña
                'Dias cambio contraseña
                If vista.TiempoCaducidadContraseña = String.Empty Then

                    UsuarioInsert.DaysChangePassword = Nothing
                Else
                    UsuarioInsert.DaysChangePassword = CType(vista.TiempoCaducidadContraseña, Integer?)
                End If

                'Fecha caducidad
                If Not vista.FechaCaducidadContraseña = Nothing Then
                    UsuarioInsert.DateExpiryAccount = CType(vista.FechaCaducidadContraseña, DateTime)
                End If
                UsuarioInsert.Password = vista.ContraseñaDelUsuario
                'Resultado de Guardar el objeto
                ' resultado = modelo.GuardarListadoUsuarios(guardarUsuario, UsuarioInsert, vista.CorreoElectronicoDelUsuario, vista.ContraseñaDelUsuario)

                If resultado.StateResult = False Then
                    vista.Mensaje(EeventViewerImages.MensajeError) = BaseClass.obtenerExcepcion(EexceptionsResources.LoginUsuarioSinDatos)
                    Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.LoginUsuarioSinDatos))
                    _fallo = True
                Else
                    vista.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    BloquearControles()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para eliminar el USUARIO seleccionado.
    ''' </summary>
    Public Async Sub EliminarUsuarioSeleccionado()
        Try
            If Usuario Is Nothing Then
                vista.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesDigiteDatos)
            Else
                vista.AsyncLoader(True)
                Using modelo As New MUsuario
                    Dim resultado = Await modelo.EliminarUsuarios(Usuario)
                    vista.AsyncLoader(False)
                    If resultado.StateResult = True Then
                        vista.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        BloquearControles()
                    End If
                End Using

            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

End Class
