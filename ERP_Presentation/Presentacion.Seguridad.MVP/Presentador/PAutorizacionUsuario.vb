'**********************************************************************
' Assembly         : Presentacion.Security.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 08-04-2021
'
' Description      : Formulario para espacio de trabajo del usuario
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.Threading.Tasks
Imports Domain.Security.Entities
Imports IndigoSingleton
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports Presentation.Base
Imports Presentation.CloudAgent

Public Class PAutorizacionUsuario
    Implements IDisposable

#Region "Variables"
    Private _IndigoApplicationSetting As Infrastructure.CrossCutting.Base.ApplicationSetting

#End Region

#Region "Constructor"
    Public Sub New()
        _IndigoApplicationSetting = ApplicationSetting.Instance
    End Sub
#End Region

#Region "Propiedades"
    Private _Usuario As MAutenticacionUsuario.Usuario
    Public Property Usuario() As MAutenticacionUsuario.Usuario
        Get
            Return _Usuario
        End Get
        Set(ByVal value As MAutenticacionUsuario.Usuario)
            _Usuario = value
        End Set
    End Property

    Private _UserConfig As UserConfiguration
    Public Property UserConfig() As UserConfiguration
        Get
            Return _UserConfig
        End Get
        Set(ByVal value As UserConfiguration)
            _UserConfig = value
        End Set
    End Property

    Private _UserLogin As UserLogin
    Public Property PUserLogin() As UserLogin
        Get
            Return _UserLogin
        End Get
        Set(ByVal value As UserLogin)
            _UserLogin = value
        End Set
    End Property

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Metodo para guardar la configuracion del area de trabajo del usuario
    ''' </summary>
    ''' <returns></returns>
    Public Async Sub GuardarConfiguracionWorkSpaceUsuario()
        Using modeloConfiguracionUsuario As New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = ConfigurationFile.Instance.AppFunctionURL, .SaveUserConfigurationKey = ConfigurationFile.Instance.SaveUserConfigurationKey})
            If UserConfig.Id > 0 Then
                UserConfig.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            Await modeloConfiguracionUsuario.SaveUserConfiguration(UserConfig)
        End Using
    End Sub


    ''' <summary>
    ''' Consulta los permisos del usuario, cuando tiene una configuracion por defecto el usuario 
    ''' </summary>
    ''' <returns></returns>
    Public Function AutorizarUsuario() As MAutenticacionUsuario.Respuesta
        Using modelo As New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = ConfigurationFile.Instance.AppFunctionURL, .LoginUserCompanyKey = ConfigurationFile.Instance.LoginUserCompanyKey})
            Dim _Respuesta = modelo.LoginUserCompany(Usuario.Id, Usuario.UserCode.Trim, SessionValues.Instance.IndigoContainerId, SessionValues.Instance.IndigoCompany, BaseClass.GetAppVersion(),
                                                     Usuario.UserType).Result
            If Not _Respuesta.Fallo Then
                PUserLogin = CType(_Respuesta.Result, MAutenticacionUsuario.RespuestaLogin).UserLogin

                UserConfig.Containers = New Containers With {.Id = SessionValues.Instance.IndigoContainerId, .Code = SessionValues.Instance.IndigoCompany,
                .Name = SessionValues.Instance.IndigoCompanyName, .CompanyType = SessionValues.Instance.IndigoCompanyType}
                LoaderConfigurationFile.Instance.SetUserConfiguration(UserConfig)
                _IndigoApplicationSetting.SetUserConfiguration(UserConfig)
                UserConfig.Containers = Nothing
                UserConfig.DefaultCompany = SessionValues.Instance.IndigoContainerId

                Dim appSet As ApplicationSettings = Nothing

                Using modeloConfiguracionAplicacion As New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = ConfigurationFile.Instance.AppFunctionURL, .GetApplicationSettingsByContainerIdKey = ConfigurationFile.Instance.GetApplicationSettingsByContainerIdKey})
                    Dim respuestaConfiguracionAplicacion = modeloConfiguracionAplicacion.GetApplicationSettingsByContainerId(SessionValues.Instance.IndigoContainerId).Result
                    If Not respuestaConfiguracionAplicacion.Fallo Then
                        appSet = respuestaConfiguracionAplicacion.Result
                    End If
                End Using

                _IndigoApplicationSetting.SetApplicationSetting(appSet)
                _IndigoApplicationSetting.LoginAzure = True

                InitSesionValues(PUserLogin)

                SetValuesUsuarioEHR()
                LoadSettingsHIS()
                Return New MAutenticacionUsuario.Respuesta() With {.Result = PUserLogin}

            Else
                Return _Respuesta
            End If
        End Using
    End Function

    ''' <summary>
    ''' Consulta los permisos del usuario, cuando tiene una configuracion por defecto el usuario 
    ''' </summary>
    ''' <returns></returns>
    Public Function AutorizarUsuarioFromMdi() As MAutenticacionUsuario.Respuesta
        UserConfig.Containers = New Containers With {.Id = SessionValues.Instance.IndigoContainerId, .Code = SessionValues.Instance.IndigoCompany,
                .Name = SessionValues.Instance.IndigoCompanyName, .CompanyType = SessionValues.Instance.IndigoCompanyType}
        LoaderConfigurationFile.Instance.SetUserConfiguration(UserConfig)
        _IndigoApplicationSetting.SetUserConfiguration(UserConfig)
        UserConfig.Containers = Nothing
        UserConfig.DefaultCompany = SessionValues.Instance.IndigoContainerId

        Dim appSet As ApplicationSettings = Nothing

        Using modeloConfiguracionAplicacion As New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = ConfigurationFile.Instance.AppFunctionURL, .GetApplicationSettingsByContainerIdKey = ConfigurationFile.Instance.GetApplicationSettingsByContainerIdKey})
            Dim respuestaConfiguracionAplicacion = modeloConfiguracionAplicacion.GetApplicationSettingsByContainerId(SessionValues.Instance.IndigoContainerId).Result
            If Not respuestaConfiguracionAplicacion.Fallo Then
                appSet = respuestaConfiguracionAplicacion.Result
            End If
        End Using

        _IndigoApplicationSetting.SetApplicationSetting(appSet)
        _IndigoApplicationSetting.LoginAzure = True

        Dim Indigo As SessionValues = SessionValues.Instance
        Dim MensajeAuditoria = New AuditMessage
        MensajeAuditoria.Company = Indigo.IndigoCompany
        MensajeAuditoria.CodeUser = Indigo.UserIndigo
        MensajeAuditoria.ComputerName = Infrastructure.CrossCutting.Base.Utils.GetHostname
        MensajeAuditoria.WindowsUser = Infrastructure.CrossCutting.Base.Utils.GetOSUsername
        MensajeAuditoria.IdUser = Indigo.UserIndigoId
        MensajeAuditoria.NameUser = Indigo.UserIndigoName
        MensajeAuditoria.CompanyType = Indigo.IndigoCompanyType
        MensajeAuditoria.DispensingIntegration = Indigo.IndigoDispensingIntegration
        Indigo.AuditMessageWcf = MensajeAuditoria

        LoadSettingsHIS()
        Return New MAutenticacionUsuario.Respuesta() With {.Result = Nothing}


    End Function





#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo para cargar Singleton  del EHR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValuesUsuarioEHR()
        Dim ValoresSesion As IndigoValoresSesion = IndigoValoresSesion.Instancia
        If String.IsNullOrEmpty(ValoresSesion.UsuarioIndigo) Then ValoresSesion.UsuarioIndigo = PUserLogin.UserCode
        If String.IsNullOrEmpty(ValoresSesion.UsuarioIndigoNombre) Then ValoresSesion.UsuarioIndigoNombre = PUserLogin.Fullname
        ValoresSesion.UsuarioRol = PUserLogin.RollCode
        ValoresSesion.UsuarioGrupo = PUserLogin.GroupCode
        If String.IsNullOrEmpty(ValoresSesion.UsuarioEmail) Then ValoresSesion.UsuarioEmail = PUserLogin.Email
        If String.IsNullOrEmpty(ValoresSesion.UsuarioCargo) Then ValoresSesion.UsuarioCargo = PUserLogin.Position
        ValoresSesion.UsuarioTipo = SessionValues.Instance.ProfileType.GetHashCode.ToString
        ValoresSesion.UsuarioAdministrador = Not (SessionValues.Instance.UserType = UserType.StandardUser)
    End Sub

    ''' <summary>
    ''' Inicializa los Valores de sesion (Singleton ERP)
    ''' </summary>
    Public Sub InitSesionValues(ByVal usr As UserLogin)
        Dim Indigo As SessionValues = SessionValues.Instance
        Indigo.UserIndigo = usr.UserCode.Trim()
        Indigo.UserGroup = usr.GroupCode
        Indigo.UserEmail = usr.Email
        Indigo.UserIndigoId = usr.Id
        Indigo.ListProductCatalog = usr.ListProductCatalog
        'Indigo.IdsFormVisible = usr.ListPermission :TODO
        Dim MensajeAuditoria = New AuditMessage
        MensajeAuditoria.Company = Indigo.IndigoCompany
        MensajeAuditoria.CodeUser = usr.UserCode.Trim()
        MensajeAuditoria.ComputerName = Infrastructure.CrossCutting.Base.Utils.GetHostname
        MensajeAuditoria.WindowsUser = Infrastructure.CrossCutting.Base.Utils.GetOSUsername
        MensajeAuditoria.IdUser = usr.Id
        MensajeAuditoria.NameUser = Indigo.UserIndigoName
        MensajeAuditoria.CompanyType = Indigo.IndigoCompanyType
        MensajeAuditoria.DispensingIntegration = Indigo.IndigoDispensingIntegration
        Indigo.UserViewMode = usr.ViewForm
        Indigo.AuditMessageWcf = MensajeAuditoria
        Indigo.UserIndigoName = usr.Fullname
        Indigo.UserCharge = usr.Position
        Indigo.UserInterface = usr.CodeInterface
        Indigo.UserRol = usr.RollCode.ToString
        Indigo.AutoPersona = usr.IdPerson.ToString()
        Indigo.UserType = CType(PUserLogin.UserType, UserType)
        Indigo.ProfileType = CType(PUserLogin.ProfileType, eProfileType)

    End Sub

    ''' <summary>
    ''' Metodo para cargar Archivo de configuracion de servicios del HIS e Instanciamos la Singleton HIS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSettingsHIS()
        Dim ValoresSesion As IndigoValoresSesion = IndigoValoresSesion.Instancia
        Dim ConfigFile As Infrastructure.CrossCutting.Base.ConfigurationFile = Infrastructure.CrossCutting.Base.ConfigurationFile.Instance
        Dim ApplicationSetting As Infrastructure.CrossCutting.Base.ApplicationSetting = Infrastructure.CrossCutting.Base.ApplicationSetting.Instance
        ValoresSesion.ProtocoloWebServices = CType(ConfigFile.EHRWebServiceProtocol.GetHashCode, IndigoValoresSesion.eProtocolos)
        ValoresSesion.UriWebServices = ConfigFile.EHRWebServiceURL
        ValoresSesion.UrlWebServicesNoti = ConfigFile.UrlNotificationWebServer
        ValoresSesion.ProtocoloWebServicesXPO = CType(ConfigFile.EHREntityServiceProtocol.GetHashCode, IndigoValoresSesion.eProtocolos)
        ValoresSesion.UriWebServicesXPO = ConfigFile.EHREntityServiceURL
        'TODO:HRR Implementar funcionalidad
        'ValoresSesion.TimeBloqueoSesion
        'se usar para validar si actualiza o no la aplicación
        ValoresSesion.TipoEjecucionSesion = IndigoValoresSesion.EIndigoTipoEjecucionSesion.Local
        'ValoresSesion.RutaActualizacion Obsoleto ya no se usa
        ValoresSesion.RutadeReportes = ConfigFile.ReportsPath
        ValoresSesion.CacheLocal = If(String.IsNullOrEmpty(ApplicationSetting.CacheServerName), False, True)
        ValoresSesion.RutaArchivosComunes = String.Concat(Infrastructure.CrossCutting.Base.Window.Utils.LocalFolder(), "\HealtTech\Indigo Vie EHR")
        ValoresSesion.RutaArchivosPorUsuario = String.Concat(Infrastructure.CrossCutting.Base.Window.Utils.UserFolder(), "\HealtTech\Indigo Vie EHR")
        ValoresSesion.EmpresaAplicacionCalidad = String.Empty
        'ValoresSesion.VersionDGH = IndigoValoresSesion.eVersionDGH.Nativo Obsoleto ya no se usa
        'ValoresSesion.VersionIndigoCrystal = "1.0.0.0" 'Application.ProductVersion.ToString() Obsoleto ya no se usa
        ValoresSesion.EmpresaIndigo = SessionValues.Instance.HisContainer.Trim().Replace("INDIGO", "")
        ValoresSesion.EmpresaDGH = SessionValues.Instance.TransactionalContainer 'Obsoleto, si lo utiliza el his
        ValoresSesion.NuevoManejoMensaje = If(ApplicationSetting.ChangeMessageBox = "1", True, False)
        ValoresSesion.ReporteadorActivo = ApplicationSetting.ReporteadorActivo

        If ValoresSesion.UsuarioTipo = IndigoValoresSesion.EIndigoTipoUsuario.Asistencial Or ValoresSesion.UsuarioAdministrador Then
            If ConfigurationFile.Instance.CenterAttention IsNot Nothing Then
                ValoresSesion.ProfesionalCentroAtencion = ConfigurationFile.Instance.CenterAttention.Trim
            Else
                ValoresSesion.ProfesionalCentroAtencion = Nothing
            End If
            If ConfigurationFile.Instance.FunctionalUnit IsNot Nothing Then
                ValoresSesion.ProfesionalUnidadFuncional = ConfigurationFile.Instance.FunctionalUnit.Trim
            Else
                ValoresSesion.ProfesionalUnidadFuncional = Nothing
            End If
        End If

        If ConfigurationFile.Instance.Dashboard.HasValue Then
            ValoresSesion.ProfesionalDashboardDefault = CType(ConfigurationFile.Instance.Dashboard.GetValueOrDefault(), IndigoValoresSesion.EIndigoTipoDashboardDefault)
        End If
    End Sub







#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If


            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region


End Class
