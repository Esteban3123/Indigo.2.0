'**********************************************************************
' Assembly         : Presentacion.Security
' Author           : Hector Rodriguez Rubiano
' Created          : 08-04-2021
'
' Description      : Formulario para espacio de trabajo del usuario
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading.Tasks
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Crystal.Entities
Imports Domain.Security.Entities
Imports IndigoSingleton
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Window
Imports Presentation.Base
Imports Presentation.Client.MVP
Imports Presentation.CloudAgent
Imports Presentation.Security.MVP

#End Region

Public Class WorkspaceUser


#Region "Variables"
    Private ValoresSesion As IndigoValoresSesion
    Private _CargandoInfo As Boolean
    Private _ParametrosAzureFunctions As MAutenticacionUsuario.ParametrosAzureFunctions
    Private _UserConfig As New UserConfiguration
    Private _ServiceConfiguration As ServiceConfiguration
    Private _UserLogin As UserLogin
    Private _UsuarioEHR As SP_SEG_AutenticarUsuario_Result
    Private _Profesional As SP_SEG_AutenticarDatosProfesional_Result
    Private _CentroAtencion As String
    Private _UnidadFuncional As String
    Private _PerfilCirugia As EIndigoTipoPerfilCirugia
    Private _DashboardDefault As EIndigoTipoDashboardDefault
    Private _TipoProfesional As eIndigoTipoProfesional
    Private _CargandoCompañias As Boolean
    Private _RowNumber As Integer 'Variable para almacenar la fila previa seleccionada en el grid control de empresas
    Private _Source As String
#End Region

#Region "Propiedades"

    Public Property UserConfig() As UserConfiguration
        Get
            Return _UserConfig
        End Get
        Set(ByVal value As UserConfiguration)
            _UserConfig = value
        End Set
    End Property


    Public Property PServiceConfiguration() As ServiceConfiguration
        Get
            Return _ServiceConfiguration
        End Get
        Set(ByVal value As ServiceConfiguration)
            _ServiceConfiguration = value
        End Set
    End Property


    Public Property PUserLogin() As UserLogin
        Get
            Return _UserLogin
        End Get
        Set(ByVal value As UserLogin)
            _UserLogin = value
        End Set
    End Property

    Public Property UsuarioEHR() As SP_SEG_AutenticarUsuario_Result
        Get
            Return _UsuarioEHR
        End Get
        Set(ByVal value As SP_SEG_AutenticarUsuario_Result)
            _UsuarioEHR = value
        End Set
    End Property

    Public Property Profesional() As SP_SEG_AutenticarDatosProfesional_Result
        Get
            Return _Profesional
        End Get
        Set(ByVal value As SP_SEG_AutenticarDatosProfesional_Result)
            _Profesional = value
        End Set
    End Property

    Private Property CentroAtencion() As String
        Get
            Return _CentroAtencion
        End Get
        Set(ByVal value As String)
            _CentroAtencion = value
        End Set
    End Property

    Private Property UnidadFuncional() As String
        Get
            Return _UnidadFuncional
        End Get
        Set(ByVal value As String)
            _UnidadFuncional = value
        End Set
    End Property

    Private Property PerfilCirugia() As EIndigoTipoPerfilCirugia
        Get
            Return _PerfilCirugia
        End Get
        Set(ByVal value As EIndigoTipoPerfilCirugia)
            _PerfilCirugia = value
        End Set
    End Property

    Private Property DashboardDefault() As EIndigoTipoDashboardDefault
        Get
            Return _DashboardDefault
        End Get
        Set(ByVal value As EIndigoTipoDashboardDefault)
            _DashboardDefault = value
        End Set
    End Property

    Public Property TipoProfesional() As eIndigoTipoProfesional
        Get
            Return _TipoProfesional
        End Get
        Set(ByVal value As eIndigoTipoProfesional)
            _TipoProfesional = value
        End Set
    End Property

    Private Property CentroAtencionCode() As String
        Get
            Return INDSleCentroAtencion.EditValue
        End Get
        Set(ByVal value As String)
            INDSleCentroAtencion.EditValue = value
        End Set
    End Property

    Private Property UnidadFuncionalCode() As String
        Get
            Return INDSleUnidadFuncional.EditValue
        End Get
        Set(ByVal value As String)
            INDSleUnidadFuncional.EditValue = value
        End Set
    End Property

    Private Property DefaultConfiguration() As Boolean
        Get
            Return INDCheConfigDefault.Checked
        End Get
        Set(ByVal value As Boolean)
            INDCheConfigDefault.Checked = value
        End Set
    End Property

    Private Property TipoPerfil() As Integer
        Get
            Return INDIlbcPerfil.SelectedIndex
        End Get
        Set(ByVal value As Integer)
            INDIlbcPerfil.SelectedIndex = value
        End Set
    End Property

    Private _CompanyList As List(Of Company)
    Public Property CompanyList() As List(Of Company)
        Get
            Return _CompanyList
        End Get
        Set(ByVal value As List(Of Company))
            _CompanyList = value
            INDGcCompanias.DataSource = _CompanyList
        End Set
    End Property

    Private _CompanySelected As Company
    Public Property CompanySelected() As Company
        Get
            Return _CompanySelected
        End Get
        Set(ByVal value As Company)
            _CompanySelected = value
        End Set
    End Property

    Private _Usuario As MAutenticacionUsuario.Usuario
    Public Property Usuario() As MAutenticacionUsuario.Usuario
        Get
            Return _Usuario
        End Get
        Set(ByVal value As MAutenticacionUsuario.Usuario)
            _Usuario = value
        End Set
    End Property

    Private Property OperatingUnitId() As Integer?
        Get
            Return INDGleOperatingUnit.EditValue
        End Get
        Set(ByVal value As Integer?)
            INDGleOperatingUnit.EditValue = value
        End Set
    End Property

#End Region

#Region "Constructor"
    Sub New(Source As String)
        InitializeComponent()
        _Source = Source
        ValoresSesion = IndigoValoresSesion.Instancia
        labelCopyright.Text = "Indigo International LLC Copyright © 2012 -" & DateTime.Now.Year.ToString()
        _RowNumber = -1
    End Sub
#End Region

#Region "Procesos"
    ''' <summary>
    ''' Metodo para asignar los parametros de consulta por medio de las Azure Functions
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AsignarParametrosAzureFunctions(ByVal sc As ServiceConfiguration)
        _ParametrosAzureFunctions = New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = sc.AppFunctionURL, .GetPerfilUbicacionKey = sc.FunctionKey1, .GetProfesionalKey = sc.FunctionKey2, .SaveUserConfigurationKey = sc.FunctionKey3,
            .GetApplicationSettingsByContainerIdKey = sc.FunctionKey4, .LoginUserCompanyKey = sc.FunctionKey5}
    End Sub

    ''' <summary>
    ''' Lista unidades operativas dependiendo del tipo de usuario
    ''' </summary>
    Private Async Function ListAllOperatingUnit(IndigoContainerId As Integer, UserType As Byte, UserCode As String, AdminCompany As Boolean) As Threading.Tasks.Task
        Using model = New MUsuario
            Dim _ListOperatingUnit = Await model.GetOperatingUnitByContainerPermission(IndigoContainerId, UserType, UserCode, AdminCompany)
            If _ListOperatingUnit IsNot Nothing Then
                INDGleOperatingUnit.Properties.DataSource = _ListOperatingUnit
                'INDGleOperatingUnit.Properties.ReadOnly = False
                Dim _OperatingUnitId As Integer = UserConfig.OperatingUnitId.GetValueOrDefault()
                If _OperatingUnitId <> 0 AndAlso _ListOperatingUnit.Where(Function(ou) ou.Id = _OperatingUnitId).FirstOrDefault IsNot Nothing Then
                    OperatingUnitId = _OperatingUnitId
                ElseIf SessionValues.Instance.IndigoOperatingUnitId <> 0 AndAlso _ListOperatingUnit.Where(Function(ou) ou.Id = SessionValues.Instance.IndigoOperatingUnitId).FirstOrDefault IsNot Nothing Then
                    OperatingUnitId = SessionValues.Instance.IndigoOperatingUnitId
                ElseIf _ListOperatingUnit.FirstOrDefault IsNot Nothing Then
                    OperatingUnitId = _ListOperatingUnit.FirstOrDefault.Id
                End If
            End If

        End Using
    End Function

    ''' <summary>
    ''' Metodo para consultar perfil y profesional
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CargarPerfilUbicacionProfesionalUnidades() As Task
        Await CargarPerfilUbicacion()

        Await CargarProfesional()
    End Function

    Private Async Function CargarProfesional() As Task
        Using modelo1 = New PAutenticacionUsuario(_ParametrosAzureFunctions)
            If CType(UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter OrElse UsuarioEHR.ADMINISTRADOR.GetValueOrDefault Then
                Dim _RespuestaProfesional = Await modelo1.GetProfesional(_Usuario.UserCode)
                If Not _RespuestaProfesional.Fallo Then
                    Profesional = _RespuestaProfesional.Result
                    SetValuesProfesional()
                    CargarProfesionalUbicacion()
                ElseIf CType(UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter Then
                    DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la información del profesional")
                    'MessageIndigo.Show("No se encontró la información del profesional", MessageType.Warning, "Advertencia de ingreso", Me)()
                End If
            End If
        End Using
    End Function

    Private Async Function CargarPerfilUbicacion() As Task
        Using modelo = New PAutenticacionUsuario(_ParametrosAzureFunctions)
            Dim _Respuesta = Await modelo.GetPerfilUbicacion(_Usuario.UserCode)
            If Not _Respuesta.Fallo Then
                Dim _UsuarioEHRAux As SP_SEG_AutenticarUsuario_Result = _Respuesta.Result
                If _UsuarioEHRAux IsNot Nothing Then
                    UsuarioEHR.CARGO = _UsuarioEHRAux.CARGO
                    UsuarioEHR.NOMBREUSUARIO = _UsuarioEHRAux.NOMBREUSUARIO
                    If CType(_UsuarioEHRAux.TIPO, eProfileType) = eProfileType.CareCenter OrElse _UsuarioEHRAux.ADMINISTRADOR.GetValueOrDefault Then

                        If _CargandoInfo Then
                            CentroAtencion = ConfigurationFile.Instance.CenterAttention
                            UnidadFuncional = ConfigurationFile.Instance.FunctionalUnit
                            _CargandoInfo = False
                        Else
                            CentroAtencion = _UsuarioEHRAux.CENTROATENCION
                            UnidadFuncional = _UsuarioEHRAux.UNIDADFUNCIONAL
                        End If
                        If (Not String.IsNullOrEmpty(CentroAtencion)) AndAlso INDSleCentroAtencion.Properties.DataSource IsNot Nothing Then
                            CentroAtencionCode = CentroAtencion
                        End If
                    End If
                End If
            Else
                DoSomething(Of String)(AddressOf MostrarNotificacion, _Respuesta.Mensaje)
                'MessageIndigo.Show(_Respuesta.Mensaje, MessageType.Warning, "Advertencia de ingreso", Me)
            End If
        End Using
    End Function

    '''' <summary>
    '''' Metodo para consultar perfil y profesional
    '''' </summary>
    '''' <remarks></remarks>
    'Private Async Sub CargarPerfilUbicacion()
    '    Using modelo = New PAutenticacionUsuario(_ParametrosAzureFunctions)
    '        Dim _Respuesta = Await modelo.GetPerfilUbicacion(_Usuario.UserCode)
    '        If Not _Respuesta.Fallo Then
    '            UsuarioEHR = _Respuesta.Result
    '            If UsuarioEHR IsNot Nothing Then
    '                If CType(UsuarioEHR.TIPO, ProfileType) = ProfileType.CareCenter OrElse UsuarioEHR.ADMINISTRADOR.GetValueOrDefault Then
    '                    If _CargandoInfo Then
    '                        CentroAtencion = ConfigurationFile.Instance.CenterAttention
    '                        UnidadFuncional = ConfigurationFile.Instance.FunctionalUnit
    '                        _CargandoInfo = False
    '                    Else
    '                        CentroAtencion = UsuarioEHR.CENTROATENCION
    '                        UnidadFuncional = UsuarioEHR.UNIDADFUNCIONAL
    '                    End If
    '                    _Respuesta = modelo.GetProfesional(_Usuario.UserCode).Result
    '                    If Not _Respuesta.Fallo Then
    '                        Profesional = _Respuesta.Result
    '                        SetValuesProfesional()
    '                        CargarProfesionalUbicacion()
    '                    ElseIf CType(UsuarioEHR.TIPO, ProfileType) = ProfileType.CareCenter Then
    '                        DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la información del profesional")
    '                        'MessageIndigo.Show("No se encontró la información del profesional", MessageType.Warning, "Advertencia de ingreso", Me)()
    '                    End If
    '                End If
    '            End If
    '        Else
    '            DoSomething(Of String)(AddressOf MostrarNotificacion, _Respuesta.Mensaje)
    '            'MessageIndigo.Show(_Respuesta.Mensaje, MessageType.Warning, "Advertencia de ingreso", Me)
    '        End If
    '    End Using
    'End Sub

    ''' <summary>
    ''' Metodo para cargar el dashboard
    ''' </summary>
    Private Async Sub CargarProfesionalUbicacion()
        If _Profesional IsNot Nothing AndAlso _Profesional.ESTADO = 1 Then
            PerfilCirugia = CType(_Profesional.PERFILCIRUGIA, EIndigoTipoPerfilCirugia)
            TipoProfesional = CType(_Profesional.TIPOPROFESIONAL, eIndigoTipoProfesional)
            Select Case TipoProfesional
                Case eIndigoTipoProfesional.Medico_General
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Medico
                Case eIndigoTipoProfesional.Medico_Especialista
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Medico
                Case eIndigoTipoProfesional.Enfermera
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Enfermeria
                Case eIndigoTipoProfesional.Auxiliar_Enfermeria
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Enfermeria
                Case eIndigoTipoProfesional.Terapeuta
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Terapias
                Case eIndigoTipoProfesional.Auxiliar_Bacteriologo
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Laboratorio
                Case eIndigoTipoProfesional.Bacteriologo
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Laboratorio
                Case eIndigoTipoProfesional.Radiologo
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Imagenologia
                Case eIndigoTipoProfesional.Tecnologo_Radiologo
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Imagenologia
                Case eIndigoTipoProfesional.Auxiliar_Patologia
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_Patologias
                Case eIndigoTipoProfesional.Psicologo, eIndigoTipoProfesional.Nutricionista, eIndigoTipoProfesional.Trabajadora_Social
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_ServiciosApoyo
                Case eIndigoTipoProfesional.Medico_Interno
                    DashboardDefault = EIndigoTipoDashboardDefault.DashBoard_MedicoInternos
            End Select
            'If CType(UsuarioEHR.TIPO, ProfileType) = ProfileType.CareCenter OrElse UsuarioEHR.ADMINISTRADOR.GetValueOrDefault Then
            INDIlbcPerfil.Enabled = True
            INDSleUnidadFuncional.ReadOnly = False
            INDSleCentroAtencion.ReadOnly = False
            ActivarDashboardProfesional()
            '    CargarCentrosUnidades()
            'Else
            '    INDIlbcPerfil.Enabled = False
            '    INDSleUnidadFuncional.ReadOnly = True
            '    INDSleCentroAtencion.ReadOnly = True
            'End If
            'ElseIf _UsuarioEHR IsNot Nothing AndAlso CType(_UsuarioEHR.TIPO, ProfileType) = ProfileType.CareCenter Then
        ElseIf _Profesional IsNot Nothing Then
            DoSomething(Of String)(AddressOf MostrarNotificacion, "El profesional asociado a este usuario esta inactivo")
            'MessageIndigo.Show("El profesional asociado a este usuario esta inactivo", MessageType.Warning, "Advertencia de ingreso", Me)
        Else
            INDIlbcPerfil.Enabled = False
            INDSleUnidadFuncional.ReadOnly = True
            INDSleCentroAtencion.ReadOnly = True
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar perfiles en el control INDIlbcPerfil
    ''' </summary>
    Private Async Sub ActivarDashboardProfesional()
        INDIlbcPerfil.Enabled = True

        Select Case TipoProfesional
            Case eIndigoTipoProfesional.Medico_General, eIndigoTipoProfesional.Odontologo_General, eIndigoTipoProfesional.Odontologo_Especialista
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Medicos", 0)})
                INDSleUnidadFuncional.Enabled = True

                'Dim ItemAgregado As Boolean
                Using model = New MUsuario
                    'Dim resultado = Await model.GetPermisoRol("239", _UsuarioEHR.ROL)
                    'If resultado.StateResult Then
                    '    If resultado.ObjectEmbbeded IsNot Nothing AndAlso resultado.ObjectEmbbeded.ioptconsu Then
                    '        ItemAgregado = True
                    '        Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Academico", 7)})
                    '    End If
                    'End If
                    'If Not ItemAgregado Then
                    '    Dim result = Await model.GetPermisoUsuario("239", _UsuarioEHR.CODIGO)
                    '    If result.StateResult Then
                    '        If resultado.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.ioptconsu Then
                    '            Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Academico", 7)})
                    '        End If
                    '    End If
                    'End If
                    Dim resultado = Await model.ConsultarPermisosUsuario(_UsuarioEHR.CODIGO, _UsuarioEHR.ROL, "1568")
                    If resultado IsNot Nothing AndAlso resultado.Count > 0 AndAlso resultado.Where(Function(pu) pu.TagButton = 40).FirstOrDefault() IsNot Nothing Then
                        Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Academico", 7)})
                    End If
                End Using

            Case eIndigoTipoProfesional.Medico_Especialista
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Medicos", 0),
                                                                                            New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Interconsultas", 2)})
                INDSleUnidadFuncional.Enabled = True
            Case eIndigoTipoProfesional.Enfermera
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Enfermeria", 1)})
                INDSleUnidadFuncional.Enabled = True

            Case eIndigoTipoProfesional.Auxiliar_Enfermeria
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Enfermeria", 1)})
                INDSleUnidadFuncional.Enabled = True

            Case eIndigoTipoProfesional.Radiologo
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Imagenologia", 3)})

                INDSleUnidadFuncional.Enabled = True

            Case eIndigoTipoProfesional.Tecnologo_Radiologo
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Imagenologia", 3)})

                INDSleUnidadFuncional.Enabled = True

            Case eIndigoTipoProfesional.Auxiliar_Bacteriologo
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("Dashboard Laboratorio", 4),
                                                                                                      New DevExpress.XtraEditors.Controls.ImageListBoxItem("Dashboard Hemocomponentes", 8)})
                INDSleUnidadFuncional.Enabled = False
            Case eIndigoTipoProfesional.Bacteriologo
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("Dashboard Laboratorio", 4),
                                                                                                     New DevExpress.XtraEditors.Controls.ImageListBoxItem("Dashboard Hemocomponentes", 8)})
                INDSleUnidadFuncional.Enabled = False
            Case eIndigoTipoProfesional.Terapeuta
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Terapias", 5)})
                INDSleUnidadFuncional.Enabled = True

            Case eIndigoTipoProfesional.Auxiliar_Patologia
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Patologia", 1)})
                INDSleUnidadFuncional.Enabled = False

            Case eIndigoTipoProfesional.Psicologo, eIndigoTipoProfesional.Nutricionista, eIndigoTipoProfesional.Trabajadora_Social
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Servicios de Apoyo", 6)})
                INDSleUnidadFuncional.Enabled = True
            Case eIndigoTipoProfesional.Medico_Interno
                Me.INDIlbcPerfil.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageListBoxItem() {New DevExpress.XtraEditors.Controls.ImageListBoxItem("DashBoard Academico", 7)})
                INDSleUnidadFuncional.Enabled = True
        End Select
    End Sub

    Private Async Function CargarEndpoints(CompanyId As Integer) As Threading.Tasks.Task
        Using model As New Presentation.Client.MVP.MLogin
            Dim endpoints = Await model.GetEndPointsByIdContainer(CompanyId)
            SessionValues.Instance.EndPoints = endpoints
            ValoresSesion.EndPoints = SessionValues.Instance.GetEndpointsAsDic()
        End Using
    End Function

    ''' <summary>
    ''' Carga los Centros de Atencion y Unidades Funcionales, asignando los valores de la sesion del usuario
    ''' </summary>
    Private Async Function CargarCentrosUnidades(CodigoUsuario As String, Grupo As String) As Threading.Tasks.Task
        'Si no exiten Valores de Sesion no realizo la consulta, ya que esta consulta se realiza en el popup del control
        If UsuarioEHR Is Nothing Then
            Return
        End If

        Using modelo = New MUsuario
            Dim resultado = Await modelo.GetCentroAtencionAutorizado(CodigoUsuario, Grupo)
            If resultado.StateResult Then
                INDSleCentroAtencion.Properties.DataSource = resultado.ObjectEmbbeded
                If resultado.ObjectEmbbeded.Count > 0 AndAlso Not String.IsNullOrEmpty(CentroAtencion) Then
                    CentroAtencionCode = CentroAtencion.Trim
                ElseIf resultado.ObjectEmbbeded.Count > 0 Then
                    CentroAtencionCode = resultado.ObjectEmbbeded.FirstOrDefault().Codigo.Trim
                End If
            End If
        End Using
    End Function



    ''' <summary>
    ''' Metodo para cargar unidades funcionales
    ''' </summary>
    Private Async Sub CargarUnidadFuncional()
        If String.IsNullOrEmpty(CentroAtencionCode) Then
            Exit Sub
        End If
        Using model = New MUsuario

            Dim result = Await model.GetUnidadFuncionalAutorizado(UsuarioEHR.CODIGO, UsuarioEHR.GRUPO, CentroAtencionCode)
            If result.StateResult Then
                INDSleUnidadFuncional.Properties.DataSource = result.ObjectEmbbeded
                INDSleUnidadFuncional.Properties.ValueMember = "Codigo"
                INDSleUnidadFuncional.Properties.DisplayMember = "UnidadFuncional"
                If result.ObjectEmbbeded.Count > 0 AndAlso Not String.IsNullOrEmpty(UnidadFuncional) Then
                    UnidadFuncionalCode = UnidadFuncional.Trim
                ElseIf result.ObjectEmbbeded.Count > 0 Then
                    UnidadFuncionalCode = result.ObjectEmbbeded.FirstOrDefault().Codigo.Trim
                End If
            End If

        End Using
    End Sub

    ''' <summary>
    ''' Mostrar mensajes
    ''' </summary>
    ''' <param name="_mensaje"></param>
    Private Sub MostrarNotificacion(ByVal _mensaje As String)
        'Dim frm As New Presentation.Controls.FrmNotificationItemDetail
        'frm.TxtMessage.Text = _mensaje
        'Using transparent As New FrmTransparent(frm, False)
        '    transparent.ShowDialog(Me)
        'End Using
        MessageIndigo.Show(_mensaje, MessageType.Warning, "Advertencia de ingreso", Me)
    End Sub

    ''' <summary>
    ''' Metodo para establecer los valores de la Singleton del ERP en el proceso de login cuando se carga la empresa por default o cuando se dispara el evento change del Popup Empresas
    ''' </summary>
    ''' <param name="companySelected"></param>
    ''' <remarks></remarks>
    Private Async Function SetERPSingletonValues(ByVal companySelected As Company) As Task

        PServiceConfiguration = companySelected.ServiceConfiguration
        If PServiceConfiguration Is Nothing OrElse PServiceConfiguration.Id = 0 Then
            DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la configuración de servicios de la compañía seleccionada. Por favor comuníquese con el administrador del sistema.")
            Return
        End If

        LoaderConfigurationFile.Instance.SetServiceConfiguration(PServiceConfiguration)
        IndigoConecta.Reset()
        AsignarParametrosAzureFunctions(PServiceConfiguration)

        If companySelected.ClientId Is Nothing Then
            Using model As New MmdiPrincipal
                Dim container = Await model.GetContainersByCode(companySelected.Code)
                companySelected.ClientId = container.ClientId.ToString()
            End Using
        End If

        CargarInformacionEmpresa(companySelected)

        ''Se disparan las tareas de forma asincorna 
        Dim taskEndpoints = CargarEndpoints(companySelected.Id)
        Dim taskOperating = ListAllOperatingUnit(companySelected.Id, Usuario.UserType, Usuario.UserCode, companySelected.Administrator)

        Dim taskCentros As Task = Task.CompletedTask
        If CType(UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter OrElse UsuarioEHR.ADMINISTRADOR.GetValueOrDefault Then
            taskCentros = CargarCentrosUnidades(UsuarioEHR.CODIGO, UsuarioEHR.GRUPO)
        Else
            INDIlbcPerfil.Enabled = False
            INDSleUnidadFuncional.ReadOnly = True
            INDSleCentroAtencion.ReadOnly = True
        End If

        Dim taskPerfil = CargarPerfilUbicacionProfesionalUnidades()

        Await taskEndpoints
        Await taskOperating
        Await taskCentros
        Await taskPerfil

        'Task.WaitAll({taskEndpoints, taskOperating, taskCentros, taskPerfil})
    End Function

    ''' <summary>
    ''' Cargo variables de sesion para la empresa
    ''' </summary>
    ''' <param name="companySelected"></param>
    Private Sub CargarInformacionEmpresa(ByVal companySelected As Company)
        If companySelected IsNot Nothing Then
            Dim ips = GetHostAndNetworkIP()
            With SessionValues.Instance
                .IndigoContainerId = companySelected.Id
                .HisContainer = companySelected.HISContainer
                .IndigoCompany = companySelected.Code
                .DocumentalContainer = companySelected.DocumentalContainer
                .TransactionalContainer = companySelected.TransactionalContainer
                .FoundationalContainer = companySelected.FoundationalContainer
                .InteropCostContainer = companySelected.InteropCostContainer
                .IndigoCompanyType = companySelected.CompanyType
                .IndigoContainer = companySelected.Code
                .IndigoGlossesIntegration = companySelected.GlossesIntegration
                .IntergrationHisStatus = If(companySelected.HISIntegration = 0, IntegrationStatus.NonIntegrated, IntegrationStatus.InProgress)
                .IndigoPayrollIntegration = companySelected.PayrollIntegration
                .IndigoHumanTalentIntegration = companySelected.HumanTalentIntegration
                .IndigoDispensingIntegration = companySelected.DispensingIntegration
                .VituelContainer = companySelected.VituelContainer
                .IndigoCompanyAddress = companySelected.Address
                .IndigoCompanyName = companySelected.Name
                .IndigoCompanyNit = companySelected.CompanyNit
                .IndigoVerificationDigitNit = companySelected.VerificationDigitNit
                .IndigoCompanyPhoneNumber = companySelected.Telephone
                .TenantId = companySelected.TenantId
                .ServiceConfigurationId = companySelected.ServiceConfigurationId
                .ArchitectureType = companySelected.ArchitectureType
                .DecimalSeparator = companySelected.DecimalSeparator
                .ClientId = companySelected.ClientId
                .IndigoVersion = companySelected.Version
                .HostName = ips.HostName
                .NetworkIP = ips.NetworkIp
                ValoresSesion.EmpresaDGH = companySelected.TransactionalContainer 'obsoleto, si lo utiliza el his
                ValoresSesion.EmpresaIndigoNit = companySelected.CompanyNit + "-" + companySelected.VerificationDigitNit
                ValoresSesion.EmpresaIndigoFundamentales = companySelected.FoundationalContainer.Replace("INDIGO", "")
                ValoresSesion.EmpresaIndigo = companySelected.HISContainer.Trim.Replace("INDIGO", "")
                ValoresSesion.EmpresaIndigoNombre = companySelected.Name.Trim
                ValoresSesion.ArchitectureType = companySelected.ArchitectureType
                If .SecurityContainer Is Nothing OrElse .SecurityContainer.Trim().Equals(String.Empty) Then
                    .SecurityContainer = companySelected.SecurityContainer
                End If
                .IndigoOperatingUnitId = companySelected.IdOperatingUnitDefault
                .ProductionCompany = companySelected.ProductionCompany
            End With

            UsuarioEHR = New SP_SEG_AutenticarUsuario_Result() With {
                .ADMINISTRADOR = Not (CType(Usuario.UserType, UserType) = UserType.StandardUser),
                .CODIGO = Usuario.UserCode,
                .EMAIL = Usuario.Email,
                .GRUPO = companySelected.GroupCode,
                .ROL = companySelected.RollCode,
                .TIPO = Usuario.ProfileType
                }
            SetValuesUsuarioEHR()
        End If
    End Sub


    Private Sub DoSomething(Of T1)(a As Action(Of T1), p As T1)
        If InvokeRequired Then
            Me.BeginInvoke(Sub()
                               a.Invoke(p)
                           End Sub)
        Else
            a.Invoke(p)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar Singleton  del EHR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValuesUsuarioEHR()
        Dim ValoresSesion As IndigoValoresSesion = IndigoValoresSesion.Instancia
        If UsuarioEHR IsNot Nothing Then
            ValoresSesion.UsuarioIndigo = UsuarioEHR.CODIGO.Trim
            ValoresSesion.UsuarioIndigoNombre = UsuarioEHR.NOMBREUSUARIO?.Trim
            ValoresSesion.UsuarioRol = UsuarioEHR.ROL.Trim
            ValoresSesion.UsuarioGrupo = UsuarioEHR.GRUPO.Trim
            ValoresSesion.UsuarioEmail = UsuarioEHR.EMAIL.Trim
            ValoresSesion.UsuarioCargo = UsuarioEHR.CARGO?.Trim
            ValoresSesion.UsuarioTipo = CType(UsuarioEHR.TIPO.Trim, IndigoValoresSesion.EIndigoTipoUsuario)
            ValoresSesion.UsuarioAdministrador = UsuarioEHR.ADMINISTRADOR.GetValueOrDefault
            ValoresSesion.VersionIndigoCrystal = Infrastructure.CrossCutting.Base.Utils.GetAppVersion().ToString
            ValoresSesion.IdTimeZone = SessionValues.Instance.IdTimeZone
            ValoresSesion.dateFormat = SessionValues.Instance.dateFormat
            ValoresSesion.timeFormat = SessionValues.Instance.timeFormat
            ValoresSesion.ClientId = SessionValues.Instance.ClientId

            ValoresSesion.dateFormatValue = Infrastructure.CrossCutting.Base.Utils.GetCustomDateFormat(SessionValues.Instance.dateFormat)
            ValoresSesion.timeFormatValue = Infrastructure.CrossCutting.Base.Utils.GetCustomTimeFormat(SessionValues.Instance.timeFormat)

            Dim dateFormatCulture = System.Globalization.CultureInfo.CurrentCulture
            If ValoresSesion.dateFormat > 0 Then
                dateFormatCulture.DateTimeFormat.LongTimePattern = ValoresSesion.timeFormatValue
                dateFormatCulture.DateTimeFormat.LongDatePattern = ValoresSesion.dateFormatValue
                dateFormatCulture.DateTimeFormat.ShortTimePattern = ValoresSesion.timeFormatValue
                dateFormatCulture.DateTimeFormat.ShortDatePattern = ValoresSesion.dateFormatValue
            End If
        End If
    End Sub

    ''' <summary>
    ''' Retorna el host y la networkIp del cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetHostAndNetworkIP() As (HostName As String, NetworkIp As String)
        Dim hostName As String = ""
        Dim networkIp As String = ""

        Try
            hostName = Dns.GetHostName()
            Dim addresses() As IPAddress = Dns.GetHostAddresses(hostName)
            networkIp = addresses.FirstOrDefault(Function(ip) ip.AddressFamily = AddressFamily.InterNetwork AndAlso Not IPAddress.IsLoopback(ip))?.ToString()
            If String.IsNullOrEmpty(networkIp) Then networkIp = ""
        Catch ex As Exception
            hostName = "Error: " & ex.Message
            networkIp = "Error: " & ex.Message
        End Try

        Return (hostName, networkIp)
    End Function

    ''' <summary>
    ''' Metodo para Singleton del EHR cuando el usuario es profesional de salud
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValuesProfesional()
        Dim ValoresSesion As IndigoValoresSesion = IndigoValoresSesion.Instancia
        If Profesional Is Nothing Then
            ValoresSesion.Profesional = Nothing
            ValoresSesion.ProfesionalNombre = Nothing
            ValoresSesion.ProfesionalTipo = IndigoValoresSesion.eIndigoTipoProfesional.Otros
            ValoresSesion.ProfesionalTarjetaProfesional = Nothing
            ValoresSesion.ProfesionalPerfilCirugia = IndigoValoresSesion.EIndigoTipoPerfilCirugia.Ninguno
            ValoresSesion.EspecialidadMedico1 = Nothing
            ValoresSesion.EspecialidadMedico2 = Nothing
            ValoresSesion.EspecialidadMedico3 = Nothing
            ValoresSesion.ProfesionalDashboardDefault = IndigoValoresSesion.EIndigoTipoDashboardDefault.Ninguno
        Else
            ValoresSesion.Profesional = Profesional.CODIGO.Trim
            ValoresSesion.ProfesionalNombre = Profesional.NOMBREPROFESIONAL.Trim
            ValoresSesion.ProfesionalTipo = CType(Profesional.TIPOPROFESIONAL, IndigoValoresSesion.eIndigoTipoProfesional)
            ValoresSesion.ProfesionalTarjetaProfesional = Profesional.TARJETAPROFESIONAL.Trim
            If String.IsNullOrEmpty(Profesional.PERFILCIRUGIA) Then
                Profesional.PERFILCIRUGIA = 0
            End If
            ValoresSesion.ProfesionalPerfilCirugia = CType(Profesional.PERFILCIRUGIA, IndigoValoresSesion.EIndigoTipoPerfilCirugia)
            ValoresSesion.EspecialidadMedico1 = Profesional.ESPECIALIDAD1.Trim
            ValoresSesion.EspecialidadMedico2 = Profesional.ESPECIALIDAD2.Trim
            ValoresSesion.EspecialidadMedico3 = Profesional.ESPECIALIDAD3.Trim

        End If
    End Sub

    ''' <summary>
    ''' Selecciona la empresa actual para que cargue la informacion
    ''' </summary>
    Public Sub CargarEspacioTrabajoActual()
        _RowNumber = INDGvCompanias.LocateByValue("Id", SessionValues.Instance.IndigoContainerId)
        INDGvCompanias.SelectRow(_RowNumber)
        INDGvCompanias.FocusedRowHandle = _RowNumber
        Dim row = INDGvCompanias.GetFocusedRow
        CompanySelected = CType(row, Company)
        DefaultConfiguration = ConfigurationFile.Instance.DefaultConfiguration
        UsuarioEHR = UnifiedConfiguration.Instance.UsuarioEHR
        Profesional = UnifiedConfiguration.Instance.Profesional
        PServiceConfiguration = UnifiedConfiguration.Instance.PServiceConfiguration
        AsignarParametrosAzureFunctions(PServiceConfiguration)
        CentroAtencion = ConfigurationFile.Instance.CenterAttention
        UnidadFuncional = ConfigurationFile.Instance.FunctionalUnit
        INDGleOperatingUnit.Properties.DataSource = SessionValues.Instance.ListOperatingUnitPermission
        OperatingUnitId = UserConfig.OperatingUnitId
        If CType(UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter OrElse UsuarioEHR.ADMINISTRADOR.GetValueOrDefault Then
            CargarProfesionalUbicacion()
            CargarCentrosUnidades(UsuarioEHR.CODIGO, UsuarioEHR.GRUPO)
        End If
    End Sub
#End Region

#Region "Funciones"
    ''' <summary>
    ''' Verifica el perfil del usuario y los estados del profesional 
    ''' </summary>
    ''' <param name="uc"></param>
    ''' <returns></returns>
    Public Async Function VerificarPerfil() As Threading.Tasks.Task(Of EValidacion)
        Dim _VerificarPerfil As EValidacion = EValidacion.UsuarioNoAsistencial
        Dim list = CType(INDGcCompanias.DataSource, List(Of Company))
        Dim compania = list.Where(Function(c) c.Id = UserConfig.DefaultCompany).FirstOrDefault
        If compania IsNot Nothing Then


            '' Pendiente revisar 
            CompanySelected = compania
            Await SetERPSingletonValues(compania)


            '0 Cuando no existe configuracion en la base de datos
            If SessionValues.Instance.ServiceConfigurationId = 0 Then
                Return EValidacion.SinServiceConfiguration
            End If


            Try
                If _ServiceConfiguration IsNot Nothing AndAlso _ServiceConfiguration.Id > 0 Then

                    'Se instancia el Presenter que contiene las funciones de validacion
                    Using modelo = New PAutenticacionUsuario(_ParametrosAzureFunctions)
                        'Se obtiene el perfil administrativo o asistencial
                        Dim respuesta As MAutenticacionUsuario.Respuesta = Await modelo.GetPerfilUbicacion(_Usuario.UserCode)

                        If Not respuesta.Fallo Then
                            Dim _UsuarioEHRAux As SP_SEG_AutenticarUsuario_Result = respuesta.Result
                            If _UsuarioEHRAux IsNot Nothing Then
                                UsuarioEHR.CARGO = _UsuarioEHRAux.CARGO
                                UsuarioEHR.NOMBREUSUARIO = _UsuarioEHRAux.NOMBREUSUARIO
                                If CType(_UsuarioEHRAux.TIPO, eProfileType) = eProfileType.CareCenter OrElse _UsuarioEHRAux.ADMINISTRADOR.GetValueOrDefault Then
                                    CentroAtencion = _UsuarioEHRAux.CENTROATENCION
                                    UnidadFuncional = _UsuarioEHRAux.UNIDADFUNCIONAL
                                End If
                            End If
                            SetValuesUsuarioEHR()
                            _VerificarPerfil = EValidacion.UsuarioNoAsistencial
                        End If
                        'Se valida si es usuario asistencial los parametros del profesional
                        If CType(UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter OrElse UsuarioEHR.ADMINISTRADOR.GetValueOrDefault Then

                            Dim respuestaProfesional = Await modelo.GetProfesional(_Usuario.UserCode)
                            If Not respuestaProfesional.Fallo Then
                                _Profesional = respuestaProfesional.Result
                                If _Profesional IsNot Nothing AndAlso _Profesional.ESTADO = 1 Then
                                    If UsuarioEHR.GRUPO <> UserConfig.GroupCode OrElse UsuarioEHR.ROL <> UserConfig.RoleCode Then
                                        _VerificarPerfil = EValidacion.CambioGrupoRol
                                    Else
                                        SetValuesProfesional()
                                        _VerificarPerfil = EValidacion.ProfesionalActivo
                                    End If
                                ElseIf CType(UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter Then
                                    _VerificarPerfil = EValidacion.ProfesionalInactivo
                                Else
                                    _VerificarPerfil = EValidacion.UsuarioNoAsistencial
                                End If
                            ElseIf CType(_UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter Then
                                _VerificarPerfil = EValidacion.NoEncontroProfesional
                            Else
                                _VerificarPerfil = EValidacion.UsuarioNoAsistencial
                            End If
                        End If
                    End Using
                Else
                    _VerificarPerfil = EValidacion.SinServiceConfiguration
                End If
            Catch ex As Exception
                _VerificarPerfil = EValidacion.ErrorInterno
            End Try
        Else
            _VerificarPerfil = EValidacion.SinCompaniaDefault
        End If
        Return _VerificarPerfil
    End Function

    Private Async Sub ExecuteBatCredentials()

        Dim commandAzureFileShare As String

        Using modelo As New MmdiPrincipal
            Dim container = Await modelo.GetContainersByCode(SessionValues.Instance.IndigoCompany)

            If container.AzureFileShareRoute Is Nothing Then
                Exit Sub
            End If

            commandAzureFileShare = container.AzureFileShareRoute

        End Using

        Dim proceso As New Process()
        proceso.StartInfo.FileName = "cmd.exe"
        proceso.StartInfo.Arguments = "/c " & commandAzureFileShare
        proceso.StartInfo.RedirectStandardOutput = True
        proceso.StartInfo.UseShellExecute = False
        proceso.StartInfo.CreateNoWindow = True

        ' Iniciar el proceso
        proceso.Start()

        ' Esperar a que el proceso termine
        proceso.WaitForExit()

    End Sub
#End Region

#Region "Eventos"


    ''' <summary>
    ''' Evento de consulta centros de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCentroAtencion_QueryPopUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDSleCentroAtencion.QueryPopUp
        If INDSleCentroAtencion.Properties.DataSource Is Nothing Then
            CargarCentrosUnidades(UsuarioEHR.CODIGO, UsuarioEHR.GRUPO)
        End If
    End Sub

    ''' <summary>
    ''' Evento de consulta unidades funcionales por cambio de centro de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCentroAtencion_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles INDSleCentroAtencion.EditValueChanged
        If INDSleCentroAtencion.EditValue Is Nothing Then
            UnidadFuncionalCode = Nothing
            INDSleUnidadFuncional.Properties.DataSource = Nothing
        Else
            CargarUnidadFuncional()
        End If
    End Sub

    ''' <summary>
    ''' Evento de consulta de unidades funcionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleUnidadFuncional_QueryPopUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDSleUnidadFuncional.QueryPopUp
        If INDSleUnidadFuncional.Properties.DataSource Is Nothing Then
            CargarUnidadFuncional()
        End If
    End Sub
    '''' <summary>
    '''' Handles the Click event of the cmdAceptar control.
    '''' </summary>
    '''' <param name="sender">The source of the event.</param>
    '''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDSmbAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDSmbAceptar.Click
        Dim _UnidadFuncionalName As String = String.Empty
        Dim _CentroAtencionName As String = String.Empty
        Dim _TipoUnidadFuncional As Integer?
        Dim _TipoPerfil As Byte?
        Dim _Dashboard As Byte?
        Dim _Grupo As String = String.Empty
        Dim _Rol As String = String.Empty
        If CompanySelected Is Nothing Then
            DoSomething(Of String)(AddressOf MostrarNotificacion, "Seleccione una compañia")
            'MessageIndigo.Show("Seleccione una compañia", MessageType.Warning, "Advertencia de ingreso", Me)
            Exit Sub
        End If
        If OperatingUnitId Is Nothing OrElse OperatingUnitId = 0 Then
            DoSomething(Of String)(AddressOf MostrarNotificacion, "Seleccione la unidad operativa")
            'MessageIndigo.Show("Seleccione la unidad operativa", MessageType.Warning, "Advertencia de ingreso", Me)
            Exit Sub
        End If
        If UsuarioEHR IsNot Nothing Then
            If CType(UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter OrElse UsuarioEHR.ADMINISTRADOR.GetValueOrDefault Then
                If CType(UsuarioEHR.TIPO, eProfileType) = eProfileType.CareCenter Then
                    If _Profesional Is Nothing Then
                        'MessageIndigo.Show("No se encontró la información del profesional", MessageType.Warning, "Advertencia de ingreso", Me)
                        DoSomething(Of String)(AddressOf MostrarNotificacion, "No se encontró la información del profesional.")
                        Exit Sub
                    ElseIf _Profesional.ESTADO <> 1 Then
                        'MessageIndigo.Show("El profesional asociado a este usuario esta inactivo", MessageType.Warning, "Advertencia de ingreso", Me)
                        DoSomething(Of String)(AddressOf MostrarNotificacion, "El profesional asociado a este usuario esta inactivo.")
                        Exit Sub
                    End If
                    If INDSleUnidadFuncional.Enabled = True AndAlso String.IsNullOrEmpty(UnidadFuncionalCode) Then
                        'MessageIndigo.Show("Debe escoger una unidad funcional para poder continuar", MessageType.Warning, "Advertencia de ingreso", Me)
                        DoSomething(Of String)(AddressOf MostrarNotificacion, "Debe escoger una unidad funcional para poder continuar.")
                        Exit Sub
                    ElseIf String.IsNullOrEmpty(CentroAtencionCode) Then
                        'MessageIndigo.Show("Debe escoger un centro de atención para poder continuar", MessageType.Warning, "Advertencia de ingreso", Me)
                        DoSomething(Of String)(AddressOf MostrarNotificacion, "Debe escoger un centro de atención para poder continuar.")
                        Exit Sub
                    End If
                End If

                Dim _CentroAtencion = CType(INDSleCentroAtencion.GetSelectedDataRow, SP_SEG_CentroAtencion_Autorizado_Result)
                If _CentroAtencion IsNot Nothing Then
                    _CentroAtencionName = _CentroAtencion.CentroAtencion
                End If
                If INDSleUnidadFuncional.Enabled = True AndAlso TipoProfesional <> IndigoValoresSesion.eIndigoTipoProfesional.Tecnologo_Radiologo AndAlso TipoProfesional <> IndigoValoresSesion.eIndigoTipoProfesional.Radiologo Then
                    Dim _UnidadFuncional = CType(INDSleUnidadFuncional.GetSelectedDataRow, SP_SEG_UnidadFuncional_Autorizado_Result)
                    If _UnidadFuncional IsNot Nothing Then
                        _TipoUnidadFuncional = _UnidadFuncional.TipoUnidadFuncional
                        _UnidadFuncionalName = _UnidadFuncional.UnidadFuncional
                    End If
                ElseIf INDSleUnidadFuncional.Enabled = True AndAlso (TipoProfesional = IndigoValoresSesion.eIndigoTipoProfesional.Tecnologo_Radiologo OrElse TipoProfesional = IndigoValoresSesion.eIndigoTipoProfesional.Radiologo) Then
                    Dim _UnidadFuncional = CType(INDSleUnidadFuncional.GetSelectedDataRow, SP_SEG_UnidadFuncional_Autorizado_Result)
                    If _UnidadFuncional IsNot Nothing Then
                        _UnidadFuncionalName = _UnidadFuncional.UnidadFuncional
                        _TipoUnidadFuncional = _UnidadFuncional.TipoUnidadFuncional
                    End If
                End If
                If TipoPerfil >= 0 Then
                    _TipoPerfil = CByte(TipoPerfil)
                    If INDIlbcPerfil.Items(INDIlbcPerfil.SelectedIndex).Value.ToString = "DashBoard Academico" Then
                        _TipoPerfil = CByte(10)
                    End If
                End If

                If DashboardDefault <> EIndigoTipoDashboardDefault.Ninguno Then
                    _Dashboard = CByte(DashboardDefault.GetHashCode)
                End If
            End If
            _Grupo = _UsuarioEHR.GRUPO
            _Rol = _UsuarioEHR.ROL
        End If

        With UserConfig
            .DefaultCompany = SessionValues.Instance.IndigoContainerId
            .CenterAttention = CentroAtencionCode
            .DefaultConfiguration = DefaultConfiguration
            .Dashboard = _Dashboard
            .FunctionalUnit = UnidadFuncionalCode
            .FunctionalUnitName = _UnidadFuncionalName
            .GroupCode = _Grupo
            .NameCareCenter = _CentroAtencionName
            .RoleCode = _Rol
            .SideFace = _TipoPerfil
            .TypeFunctionalUnit = _TipoUnidadFuncional
            .BorrarConfig = False
            .OperatingUnitId = OperatingUnitId
        End With
        If _Source.Equals("FrmLoginAzure") Then
            Using _PAutorizacionUsuario As New PAutorizacionUsuario()
                _PAutorizacionUsuario.UserConfig = UserConfig
                _PAutorizacionUsuario.Usuario = Usuario
                Dim _Respuesta As MAutenticacionUsuario.Respuesta = _PAutorizacionUsuario.AutorizarUsuario
                If _Respuesta.Fallo Then
                    'MessageIndigo.Show(_Respuesta.Mensaje, MessageType.Warning, "Advertencia de ingreso", Me)
                    DoSomething(Of String)(AddressOf MostrarNotificacion, _Respuesta.Mensaje)
                    Me.DialogResult = DialogResult.Cancel
                Else
                    CompanySelected.IdOperatingUnitDefault = OperatingUnitId
                    SessionValues.Instance.IndigoOperatingUnitId = OperatingUnitId
                    UnifiedConfiguration.Instance.UsuarioEHR = UsuarioEHR
                    UnifiedConfiguration.Instance.Profesional = Profesional
                    UnifiedConfiguration.Instance.PServiceConfiguration = PServiceConfiguration
                    UnifiedConfiguration.Instance.CompanySelected = CompanySelected
                    PUserLogin = CType(_Respuesta.Result, UserLogin)
                    If INDCheConfigDefault.EditValue Then
                        _PAutorizacionUsuario.GuardarConfiguracionWorkSpaceUsuario()
                    End If
                    UpdateReportPath(Usuario.Id, CompanySelected.Id, OperatingUnitId)
                    ExecuteBatCredentials()

                    Me.DialogResult = DialogResult.OK
                End If
            End Using
        Else
            Using _PAutorizacionUsuario As New PAutorizacionUsuario()
                _PAutorizacionUsuario.UserConfig = UserConfig
                _PAutorizacionUsuario.AutorizarUsuarioFromMdi()
                CompanySelected.IdOperatingUnitDefault = OperatingUnitId
                SessionValues.Instance.IndigoOperatingUnitId = OperatingUnitId
                UnifiedConfiguration.Instance.UsuarioEHR = UsuarioEHR
                UnifiedConfiguration.Instance.Profesional = Profesional
                UnifiedConfiguration.Instance.PServiceConfiguration = PServiceConfiguration
                UnifiedConfiguration.Instance.CompanySelected = CompanySelected
                _PAutorizacionUsuario.GuardarConfiguracionWorkSpaceUsuario()
                UpdateReportPath(Usuario.Id, CompanySelected.Id, OperatingUnitId)
                Me.DialogResult = DialogResult.OK
                'End If
            End Using

        End If

    End Sub

    ''' <summary>
    ''' Actualiza la ruta de reportes por usuario 
    ''' </summary>
    ''' <param name="IdUser"></param>
    ''' <param name="IdContainer"></param>
    ''' <param name="IdOperatingUnit"></param>
    Private Sub UpdateReportPath(idUser As Integer, idContainer As Integer, idOperatingUnit As Integer)
        If ApplicationSetting.Instance.LoginAzure Then
            Dim cache As ReportCache = ReportCache.GetInstance()
            Dim cacheKey As String = $"{idUser}-{idContainer}-{idOperatingUnit}"
            Dim cachedReportPath As String = cache.GetValue(cacheKey)
            If String.IsNullOrEmpty(cachedReportPath) Then
                Using modelo = New PAutenticacionUsuario(Nothing)
                    Dim reportPath = modelo.GetReportPathByUser(idUser, idContainer, idOperatingUnit)
                    cache.SetValue(cacheKey, reportPath)
                    ConfigurationFile.Instance.ReportsPath = reportPath
                End Using
            Else
                ConfigurationFile.Instance.ReportsPath = cachedReportPath
            End If
        End If
    End Sub

    '''' <summary>
    '''' Handles the Click event of the cmdCancelar control.
    '''' </summary>
    '''' <param name="sender">The source of the event.</param>
    '''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDSmbCancelar_ClickAsync(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDSmbCancelar.Click
        'Sin este archivo no se puede continuar
        DialogResult = DialogResult.Abort

        If _Source.Equals("FormMdi") Then
            If UnifiedConfiguration.Instance.UsuarioEHR IsNot Nothing AndAlso Not UnifiedConfiguration.Instance.UsuarioEHR.Equals(UsuarioEHR) Then
                UsuarioEHR = UnifiedConfiguration.Instance.UsuarioEHR
                SetValuesUsuarioEHR()
            End If
            If UnifiedConfiguration.Instance.Profesional IsNot Nothing AndAlso Not UnifiedConfiguration.Instance.Profesional.Equals(Profesional) Then
                Profesional = UnifiedConfiguration.Instance.Profesional
                SetValuesProfesional()
            End If
            If UnifiedConfiguration.Instance.CompanySelected IsNot Nothing AndAlso Not UnifiedConfiguration.Instance.CompanySelected.Equals(CompanySelected) Then
                CompanySelected = UnifiedConfiguration.Instance.CompanySelected
                CargarInformacionEmpresa(CompanySelected)
            End If
        End If

        Me.Close()
    End Sub

    ''' <summary>
    ''' Evento para cargar variables de compañia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDGvCompanias_RowClick(sender As Object, e As RowClickEventArgs) Handles INDGvCompanias.RowClick
        If (Not INDGvCompanias.IsGroupRow(e.RowHandle)) AndAlso _RowNumber <> e.RowHandle Then
            _RowNumber = e.RowHandle
            Dim row = INDGvCompanias.GetFocusedRow
            If row IsNot Nothing Then
                Me.INDIlbcPerfil.Items.Clear()
                TipoPerfil = -1
                DefaultConfiguration = False
                _UserLogin = Nothing
                _UsuarioEHR = Nothing
                _Profesional = Nothing
                PerfilCirugia = EIndigoTipoPerfilCirugia.Ninguno
                DashboardDefault = EIndigoTipoDashboardDefault.Ninguno
                TipoProfesional = eIndigoTipoProfesional.Ninguno
                CentroAtencion = Nothing
                UnidadFuncional = Nothing
                CentroAtencionCode = Nothing
                INDSleCentroAtencion.Properties.DataSource = Nothing
                UnidadFuncionalCode = Nothing
                INDSleUnidadFuncional.Properties.DataSource = Nothing
                INDIlbcPerfil.Enabled = False
                INDSleUnidadFuncional.ReadOnly = True
                INDSleCentroAtencion.ReadOnly = True
                OperatingUnitId = Nothing
                CompanySelected = CType(row, Company)
                Await SetERPSingletonValues(CompanySelected)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna la bandera del pais de la compañia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvCompanias_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles INDGvCompanias.CustomDrawCell
        If e.Column.FieldName = "Flagcode" Then
            e.DefaultDraw()
            If Not String.IsNullOrEmpty(e.CellValue) Then
                Dim _Index = INDImgCountries.Images.Keys.IndexOf(String.Format("{0}.png", e.CellValue.ToString()))
                If _Index >= 0 Then
                    e.Cache.DrawImage(INDImgCountries.Images(_Index), e.Bounds.Location)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Enumeradores"
    ''' <summary>
    ''' Enumeracion para establecer el tipo de profesional y realizar las respectivas validaciones en los
    ''' Dashboar's
    ''' </summary>
    Public Enum eIndigoTipoProfesional
        Ninguno = 0
        Medico_General = 1
        Medico_Especialista = 2
        Enfermera = 3
        Auxiliar_Enfermeria = 4
        Odontologo_General = 5
        Odontologo_Especialista = 6
        Nutricionista = 7
        Higienista = 8
        Psicologo = 9
        Trabajadora_Social = 10
        Promotor_de_Saneamiento = 11
        Ingeniero_Sanitario = 12
        Medico_Veterinario = 13
        Ingeniero_Alimento = 14
        Auxiliar_Bacteriologo = 15
        Terapeuta = 16
        Optometra = 17
        Quimico_Farmaceutico = 18
        Radiologo = 19
        Tecnologo_Radiologo = 20
        Instrumentador_Qx = 21
        Auxiliar_Patologia = 22
        Otros = 23
        Medico_Interno = 24
        Bacteriologo = 25
        Patologo = 26
    End Enum

    ''' <summary>
    ''' Enumeracion para los perfiles de Cirugia
    ''' </summary>
    Public Enum EIndigoTipoPerfilCirugia
        Ninguno = 0
        Cirujano = 1
        Anestesiologos = 2
        Ayudantes = 3
        AnestesiologosCirujano = 4
    End Enum

    ''' <summary>
    ''' Enumeracion para establecer el Dashboard que se selecciona por defecto cuando el usuario ingresa
    ''' </summary>
    Public Enum EIndigoTipoDashboardDefault
        Ninguno = 0
        DashBoard_Medico = 1
        DashBoard_Enfermeria = 2
        DashBoard_Interconsultas = 3
        DashBoard_Terapias = 4
        DashBoard_Laboratorio = 5
        DashBoard_Imagenologia = 6
        DashBoard_Patologias = 7
        DashBoard_ServiciosApoyo = 8
        DashBoard_MedicoInternos = 9
    End Enum

    ''' <summary>
    ''' Indica el estado de validacion de perfil y ubicacion
    ''' </summary>
    Public Enum EValidacion
        SinCompaniaDefault
        SinServiceConfiguration
        CambioGrupoRol
        NoEncontroProfesional
        ProfesionalActivo
        ProfesionalInactivo
        UsuarioNoAsistencial
        ErrorInterno
    End Enum
#End Region

End Class

