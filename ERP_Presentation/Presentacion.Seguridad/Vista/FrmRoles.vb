'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Jhon Tovar
' Last Modified On : 28-02-2022
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"

Imports DevExpress.XtraEditors
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Security.MVP
Imports Presentation.Controls
Imports Presentation.CloudAgent.IndigoReference.Security
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Security.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Threading.Tasks
Imports System.Text
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Clase que controla los comportamientos del formulario FrmRoles..
''' </summary>
Public Class FrmRoles
    Inherits Presentation.Controls.FormBase
    Implements IRoles

#Region "variables y Load"

    ''' <summary>
    ''' lista de los permisos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListPermission As New List(Of PermissionUserModuleForm)
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim presenter As PRoles
    ''' <summary>
    ''' Variable para instanciar un listado de tipo SeguridadPermisosRoles - Retorna TAG del permiso del ROL
    ''' </summary>
    Dim ListadoRoles As New List(Of PermissionRoll)
    Dim Indigo As SessionValues = SessionValues.Instance


    ''' <summary>
    ''' variable para conocer si el boton de conceder todos los permisos fue presionado
    ''' </summary>
    Dim INDConcederTodosLosPermisos As Boolean = False
    ''' <summary>
    ''' variable para quitarle todos los permisos para todos lo funcionales al rol
    ''' </summary>
    Dim INDDeclinarTodosLosPermisos As Boolean = False
    ''' <summary>
    ''' vairable para conocer el estado de la consulta de los formularios de DB.
    ''' </summary>
    Dim TaskloadListForms As Task


    ''' <summary>
    ''' Evento Load, establecer TAG para el permiso de la Barra de Botones
    ''' </summary>
    Private Sub FrmRoles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BarraBotones.StatusRecordVisible = False
        presenter = New PRoles(Me)
        INDbtnConcederTodosLosPermisos.Enabled = False
        INDbtnDeclinarTodosLosPermisos.Enabled = False
        Deshacer()
        INDGleRoleType_EditValueChanged(Nothing, Nothing)
        If Indigo.UserViewMode = True Then
            BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
        TaskloadListForms = presenter.ConsultarTodosForms()
    End Sub

    Dim _listModulesForms As List(Of VieForm)

    ''' <summary>
    ''' Listado de tipos de rol
    ''' </summary>
    Dim ListRoleType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Modo de visualizacion del formulario
    ''' </summary>
    Dim _ViewForm As Boolean
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Metodo Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _ViewForm = Indigo.UserViewMode
        Indigo.UserViewMode = True
    End Sub
#End Region

#Region "Propiedades de la Interfaz"

    ''' <summary>
    ''' Esta propiedad contiene todos los mensajes.
    ''' </summary>
    ''' <value></value>
    Public WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad sirve para activar o desactivar controles del frontal
    ''' </summary>
    Public WriteOnly Property ActivarDesactivar As Boolean Implements IRoles.ActivarControles
        Set(ByVal value As Boolean)
            LayoutControl1.BeginUpdate()
            'INDBtnEditCodigoRol.Enabled = Not value
            INDTxtNombreRol.Enabled = value
            INDgrupoPermisos.Enabled = value
            INDgcPermission.Enabled = value
            INDBtnEditCodigoRol.Enabled = False
            INDGleRollType.Enabled = value
            LayoutControl1.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo  del rol
    ''' </summary>
    ''' <value></value>
    Public Property Codigo As String Implements IRoles.Codigo
        Get
            Return INDBtnEditCodigoRol.Text
        End Get
        Set(ByVal value As String)
            INDBtnEditCodigoRol.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene en nombre del rol
    ''' </summary>
    ''' <value></value>
    Public Property Nombre As String Implements IRoles.Nombre
        Get
            Return INDTxtNombreRol.Text
        End Get
        Set(ByVal value As String)
            INDTxtNombreRol.Text = value
        End Set
    End Property

    Dim Tabla As New List(Of PermissionUserModuleForm)
    ''' <summary>
    ''' Esta propiedad hace el data source a la rejilla de funcionales
    ''' </summary>
    ''' <value></value>
    Public WriteOnly Property DataSourceRoles As Object Implements IRoles.DataSourceRoles
        Set(ByVal value As Object)
            INDgcPermission.DataSource = value
            Tabla = CType(value, List(Of PermissionUserModuleForm))
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el tipo de rol
    ''' </summary>
    ''' <value></value>
    Public Property PRollType As Byte? Implements IRoles.PRollType
        Get
            Return INDGleRollType.EditValue
        End Get
        Set(ByVal value As Byte?)
            INDGleRollType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad hace el data source al control de tenant
    ''' </summary>
    ''' <value></value>
    Public Property TenantDataSource As DevExpress.Xpo.XPServerCollectionSource Implements IRoles.TenantDataSource
        Get
            Return INDSleTenant.Properties.DataSource
        End Get
        Set(ByVal value As DevExpress.Xpo.XPServerCollectionSource)
            INDSleTenant.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el id de tenant
    ''' </summary>
    ''' <value></value>
    Public Property TenantId As Short? Implements IRoles.TenantId
        Get
            Return INDSleTenant.EditValue
        End Get
        Set(ByVal value As Short?)
            INDSleTenant.EditValue = value
        End Set
    End Property

#End Region

#Region "Permisos"

    ''' <summary>
    ''' Variable del Objeto SeguridadPermisosFormularios
    ''' </summary>
    Private _ListadTodosPermisosRol As List(Of PermissionRoll)
    ''' <summary>
    ''' lista todos los permisos rol.	
    ''' </summary>
    ''' <value>lista todos los permisos rol.</value>
    ''' <remarks></remarks>
    Public Property ListarTodosPermisosRol As System.Collections.Generic.List(Of PermissionRoll) Implements MVP.IRoles.ListarTodosPermisosRol
        Get
            Return _ListadTodosPermisosRol
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of PermissionRoll))
            _ListadTodosPermisosRol = value
        End Set
    End Property

    ''' <summary>
    ''' Variable del Objeto SeguridadPermisosRoles
    ''' </summary>
    Private _ListaPermisoRol As List(Of PermissionRoll)
    ''' <summary>
    ''' Esta propiedad establece las acciones de cada funcional con sus respectivos valores de permisos, se ejecuta en el evento click del panel de acciones
    ''' </summary>
    ''' <value></value>
    Property ListaPermisoRol As List(Of PermissionRoll) Implements MVP.IRoles.ListaPermisoRol
        Get
            Return _ListaPermisoRol
        End Get
        Set(ByVal value As List(Of PermissionRoll))
            '_ListaPermisoRol = value
            'Dim valor As String = String.Empty

            'For i As Integer = 0 To value.Count - 1

            '    For j As Integer = 0 To INDCheckedListBoxControl.Items.Count - 1
            '        Select Case INDCheckedListBoxControl.Items(j).Description.Trim
            '            'Eliminar Guardar Actualizar, etc.. deben tener el mismo nombre en la base de datos.
            '            Case Is = obtenerRecurso(PermisosEliminar)
            '                valor = "1"
            '            Case Is = obtenerRecurso(PermisosGuardar)
            '                valor = "2"
            '            Case Is = obtenerRecurso(PermisosActualizar)
            '                valor = "3"
            '            Case Is = obtenerRecurso(PermisosEliminarRejilla)
            '                valor = "6"
            '            Case Is = obtenerRecurso(PermisosCustomizar)
            '                valor = "11"
            '            Case Is = obtenerRecurso(PermisosRedesSociales)
            '                valor = "12"
            '            Case Is = obtenerRecurso(PermisosDocumentos)
            '                valor = "13"
            '            Case Is = obtenerRecurso(PermisosComunicacion)
            '                valor = "14"
            '            Case Is = obtenerRecurso(PermisosOtrosPlugins)
            '                valor = "15"
            '            Case Is = obtenerRecurso(PermisosConsultar)
            '                valor = "40"
            '            Case Is = obtenerRecurso(PermisosVisible)
            '                valor = "41"
            '        End Select
            '        If value.Item(i).Action.ToString.Trim = valor.ToString Then
            '            If value.Item(i).ActionValue = True Then
            '                INDCheckedListBoxControl.Items(j).CheckState = CheckState.Checked
            '            End If
            '        End If
            '    Next

            'Next
        End Set
    End Property

#End Region

#Region "Metodos de Funcionalidad del Formulario"

    ' ''' <summary>
    ' ''' Conceders the todos los permisos.
    ' ''' </summary>
    'Public Async Sub ConcederTodosLosPermisos() Handles INDbtnConcederTodosLosPermisos.Click
    '    Dim Modelo As New MRoles
    '    AsyncLoader(True)
    '    Dim ListadoPermisosMenu As List(Of GenesisPermissionForm) = Modelo.listarTodosLosPermisosFormularios()
    '    AsyncLoader(False)
    '    Await presenter.ConsultarTodosPermisosRol()
    '    Dim valor As String = String.Empty
    '    Dim Objeto As PermissionRoll = Nothing
    '    Dim NuevoObjeto As Boolean = True
    '    For i As Integer = 0 To ListadoPermisosMenu.Count - 1
    '        If Object.Equals(ListarTodosPermisosRol, Nothing) = False Then
    '            If ListarTodosPermisosRol.Where(Function(e) e.IdRoll = CInt(presenter.Rol.Id) And e.IdForm.Trim = ListadoPermisosMenu.Item(i).IdForm.ToString.Trim And e.Action.Trim = ListadoPermisosMenu.Item(i).Action.ToString.Trim).Count > 0 Then
    '                NuevoObjeto = False
    '                For j As Integer = 0 To ListarTodosPermisosRol.Count - 1
    '                    If ListarTodosPermisosRol.Item(j).IdRoll = CInt(presenter.Rol.Id) And ListarTodosPermisosRol.Item(j).IdForm.Trim = ListadoPermisosMenu.Item(i).IdForm.ToString.Trim And ListarTodosPermisosRol.Item(j).Action.Trim = ListadoPermisosMenu.Item(i).Action.ToString.Trim Then
    '                        Objeto = ListarTodosPermisosRol.Item(j)
    '                        Objeto.ActionValue = True
    '                        'Objeto.ChangeTracker.State = Dominio.Entidades.ObjectState.Modified
    '                        Exit For
    '                    End If
    '                Next
    '            Else
    '                NuevoObjeto = True
    '            End If
    '        Else
    '            NuevoObjeto = True
    '        End If
    '        If NuevoObjeto = True Then
    '            Objeto = New PermissionRoll
    '            valor = ListadoPermisosMenu.Item(i).Action.ToString.Trim
    '            If Object.Equals(ListarTodosPermisosRol, Nothing) = False Then
    '                Objeto.IdRoll = CInt((presenter.Rol.Id))
    '            End If
    '            Objeto.IdForm = ListadoPermisosMenu.Item(i).IdForm.Trim
    '            Objeto.ActionValue = True
    '            Objeto.Action = valor.ToString.Trim

    '        End If
    '        ListadoRoles.Add(Objeto)
    '    Next
    'End Sub


#End Region

#Region "Metodos de la Interfaz"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IRoles.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IRoles.Deshacer
        LayoutControl1.BeginUpdate()
        ListadoRoles = New List(Of PermissionRoll)
        presenter.Deshacer()
        INDConcederTodosLosPermisos = False
        INDDeclinarTodosLosPermisos = False
        'INDBtnEditCodigoRol.Focus()
        INDTxtNombreRol.Focus()
        INDgcPermission.DataSource = Nothing
        INDgcPermission.DataSource = Tabla
        INDbtnConcederTodosLosPermisos.Enabled = False
        INDbtnDeclinarTodosLosPermisos.Enabled = False
        INDGleRollType.Properties.NullText = ""
        'BarraBotones.PrepareToolbar(eAction.OnlyFind)
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements IRoles.Eliminar
        If Not String.IsNullOrEmpty(Codigo) Then
            If XtraMessageBox.Show(obtenerRecurso(ComunesEliminarRegistro), obtenerRecurso(ComunesIndigoCrystal), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                'presenter.ConsultarTodosPermisosRol()
                Try
                    presenter.EliminarRoles(Codigo)

                Catch ex As Exception
                    AsyncLoader(False)
                    ActivarDesactivar = True
                    Mensaje(EeventViewerImages.MensajeError) = ex.Message
                End Try

                'Deshacer()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesDigiteDatos)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IRoles.Guardar
        Try
            Dim errors As New StringBuilder
            If INDTxtNombreRol.Text.Trim = "" Then
                errors.AppendLine("Debe ingresar nombre de rol")
            End If
            If PRollType Is Nothing Then
                errors.AppendLine("Seleccione tipo rol")
            End If
            If PRollType IsNot Nothing AndAlso CType(PRollType, eRollType) = eRollType.ByTenant AndAlso (presenter.Rol.TenantRoll Is Nothing OrElse presenter.Rol.TenantRoll.Count = 0 OrElse
                    Not presenter.Rol.TenantRoll.Any(Function(tr) tr.ChangeTracker.State <> ObjectState.Deleted)) Then
                errors.AppendLine("Asocie el rol a un tenant")
            End If
            If presenter.Rol.PermissionRoll.Count = 0 Then
                errors.AppendLine("No se asignaron permisos al rol")
            End If
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If
            AsyncLoader(True)
            Await presenter.GuardarLosRoles()
            AsyncLoader(False)
            'BaseClass.GetActiveForms(SessionValues.Instance.UserIndigo.ToString.Trim, SessionValues.Instance.UserRol.ToString.Trim)
            Deshacer()
            AbrirBusqueda()
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements IRoles.Nuevo
        ListadoRoles = New List(Of PermissionRoll)
        presenter.Deshacer()
        'INDBtnEditCodigoRol.Focus()

        presenter.Rol = New Roll
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.LoadTreeModules()
        'INDgcPermission.DataSource = _listModulesForms.Where(Function(f) f.Modules IsNot Nothing AndAlso f.Modules.Count > 0).ToList()
        'INDgcPermission.DataSource = _listModulesForms

        ActivarDesactivar = True
        INDbtnConcederTodosLosPermisos.Enabled = True
        INDbtnDeclinarTodosLosPermisos.Enabled = True
        INDTxtNombreRol.Focus()

        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
    End Sub

    ''' <summary>
    ''' Este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements IRoles.OpenSearch
        Indigo.UserViewMode = True
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = "No tiene permisos"
            Exit Sub
        End If
        BarraBotones.PrepareToolbar(eAction.OnlyNew)
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValueSearch
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Rol", .FieldName = "Description"}, New ColumnInfo With {.Caption = "Tipo", .FieldName = "RollTypeName"}}.ToList()
            .ValorSolicitado = "RollCode"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.RolesCRUD
            .FormParent = Me
            .ShowSearch()
        End With
        Indigo.UserViewMode = _ViewForm
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValueSearch(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDBtnEditCodigoRol.Text = ReturnValue
        If INDBtnEditCodigoRol.Text <> String.Empty Then
            Await LoadControls()
            If INDBtnEditCodigoRol.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnEditCodigoRol.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(ByVal existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo que obtiene los permisos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTreeModules()
        TaskloadListForms.Wait()
        'If ApplicationSetting.Instance.LoginAzure Then
        '    Return Task.Factory.StartNew(Async Function()
        '                                     Using modelo = New PAutenticacionUsuario(New MAutenticacionUsuario.ParametrosAzureFunctions() With {.AppFunctionURL = UnifiedConfiguration.Instance.PServiceConfiguration.AppFunctionURL,
        '                                        .GetSuscriptionsKey = UnifiedConfiguration.Instance.PServiceConfiguration.FunctionKey6})
        '                                         Dim _Respuesta = Await modelo.GetSuscriptions()

        '                                         'If _Respuesta IsNot Nothing Then
        '                                         If _Respuesta.Result IsNot Nothing AndAlso Not _Respuesta.Fallo Then
        '                                             Dim _Suscriptions As List(Of MAutenticacionUsuario.Suscriptions) = _Respuesta.Result
        '                                             If _Suscriptions IsNot Nothing AndAlso _Suscriptions.Count > 0 Then
        '                                                 Dim _ProductCatalogIds = _Suscriptions.Select(Function(_suscription) _suscription.ProductCatalogId).ToList
        '                                                 Dim _modules = BaseClass.GetXmlWithAggregates(Of VieModule)(eDataXml.XMLModules).Where(Function(m) _ProductCatalogIds.Contains(m.ProductCatalogId)).Select(Function(m) m.Id).ToList
        '                                                 _listModulesForms = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms).Where(Function(f) f.Module IsNot Nothing AndAlso _modules.Contains(f.Module.Id)).ToList
        '                                             Else
        '                                                 _listModulesForms = New List(Of VieForm)
        '                                             End If
        '                                         Else
        '                                             _listModulesForms = New List(Of VieForm)
        '                                         End If
        '                                         INDgcPermission.DataSource = _listModulesForms
        '                                     End Using
        '                                 End Function)

        'Else
        '    Return Task.Factory.StartNew(Sub()
        '                                     _listModulesForms = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms).Where(Function(f) f.Module IsNot Nothing AndAlso f.Module.Id > 0 AndAlso f.Module.Id < 67).ToList
        '                                     INDgcPermission.DataSource = _listModulesForms
        '                                 End Sub)
        'End If
        If INDgcPermission.InvokeRequired Then
            INDgcPermission.BeginInvoke(Sub()
                                            '_listModulesForms = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms)
                                            'INDgcPermission.DataSource = _listModulesForms
                                            _listModulesForms = presenter.ListForm
                                            INDgcPermission.DataSource = _listModulesForms
                                        End Sub)

        Else
            '_listModulesForms = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLForms)
            'INDgcPermission.DataSource = _listModulesForms
            _listModulesForms = presenter.ListForm
            INDgcPermission.DataSource = _listModulesForms
        End If
    End Sub

    ' <summary>
    ' Metodo que obtiene los permisos del rol
    ' </summary>
    ' <remarks></remarks>
    'Private Sub GetPermissionRoll()
    '    Dim FormsERP As List(Of VieForm) = BaseClass.GetXmlWithAggregates(Of VieForm)(eDataXml.XMLFormsModuleUser)
    '    Dim ListPermission As New List(Of PermissionUserModuleForm)
    '    For Each Form As VieForm In FormsERP
    '        Dim Permission As New PermissionUserModuleForm
    '        Dim ListActions As New List(Of SecurityPermissionAction)
    '        With Permission
    '            .NameModule = Form.Module.Name
    '            .CodeForm = Form.Id.ToString()
    '            .NameForm = Form.Name
    '            .TypeForm = Form.Type.ToString()
    '            For Each Actions As ViePermission In Form.Permissions
    '                Dim ActionForm As New SecurityPermissionAction
    '                With ActionForm
    '                    .ActionId = Actions.Id.ToString()
    '                    .ActionName = Actions.Name
    '                    .ActionValue = False
    '                End With
    '                ListActions.Add(ActionForm)
    '            Next
    '            .ActionForm = ListActions
    '        End With
    '        ListPermission.Add(Permission)
    '    Next
    '    INDgcPermission.DataSource = ListPermission
    'End Sub

    ''' <summary>
    ''' Metodo que asigna todos los permisos
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetAllPermissions() As Task
        'Dim objLock As New Object()
        'Return Task.Factory.StartNew(Sub()
        '                                 Parallel.ForEach(CType(INDgcPermission.DataSource, List(Of VieForm)), Sub(Form As VieForm)
        '                                                                                                           SyncLock objLock
        '                                                                                                               For Each action In Form.Permissions.FindAll(Function(x) x.Value = False)
        '                                                                                                                   Dim actionForm = presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = Form.Id And x.Action = action.Id).FirstOrDefault()
        '                                                                                                                   If actionForm IsNot Nothing Then
        '                                                                                                                       actionForm.ActionValue = True
        '                                                                                                                   Else
        '                                                                                                                       Dim permissionRoll As New PermissionRoll
        '                                                                                                                       With permissionRoll
        '                                                                                                                           .IdRoll = presenter.Rol.Id
        '                                                                                                                           .IdForm = Form.Id
        '                                                                                                                           .Action = action.Id
        '                                                                                                                           .ActionValue = True
        '                                                                                                                       End With
        '                                                                                                                       presenter.Rol.PermissionRoll.Add(permissionRoll)

        '                                                                                                                   End If
        '                                                                                                               Next
        '                                                                                                           End SyncLock
        '                                                                                                       End Sub)
        '                             End Sub)
        _listModulesForms.ForEach(Sub(f) f.Permissions.ForEach(Sub(p) p.Value = True))
        presenter.Rol.PermissionRoll.ToList().ForEach(Sub(p) p.ActionValue = True)
        Dim linq = From form In _listModulesForms '_listModulesForms.Where(Function(f) f.Modules IsNot Nothing AndAlso f.Modules.Count > 0)
                   From action In form.Permissions
                   Group Join permissionroll In presenter.Rol.PermissionRoll On permissionroll.IdForm Equals form.Id And permissionroll.Action Equals action.Id Into Group
                   From permissionroll In Group.DefaultIfEmpty
                   Where permissionroll Is Nothing
                   Select New PermissionRoll() With {
                                   .IdRoll = presenter.Rol.Id,
                        .IdForm = form.Id,
                        .Action = action.Id,
                        .ActionValue = True}
        Return Task.Factory.StartNew(Sub()
                                         For Each reg In From _PermissionRoll In linq
                                                         Group _PermissionRoll By _PermissionRoll.IdForm, _PermissionRoll.Action Into _Group = Group
                                                         Select _Group
                                             presenter.Rol.PermissionRoll.Add(reg.FirstOrDefault)
                                         Next
                                     End Sub)

    End Function

    ''' <summary>
    ''' Metodo que quita todos los permisos
    ''' </summary>
    ''' <remarks></remarks>
    Private Function RemoveAllPermissions() As Task
        Dim listActionTmp = presenter.Rol.PermissionRoll.Where(Function(x) x.Id = 0).ToList()
        While listActionTmp.Count > 0
            presenter.Rol.PermissionRoll.Remove(listActionTmp(0))
            listActionTmp.Remove(listActionTmp(0))
        End While
        Dim objLock As New Object()



        Return Task.Factory.StartNew(Sub()
                                         Parallel.ForEach(presenter.Rol.PermissionRoll.Where(Function(x) x.ActionValue = True).ToList(), Sub(action As PermissionRoll)
                                                                                                                                             SyncLock objLock
                                                                                                                                                 action.ActionValue = False
                                                                                                                                             End SyncLock
                                                                                                                                         End Sub)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Metodo cuando se cambia un permiso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrbgActionValue_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDrbgActionValue.EditValueChanging
        Dim detailView = TryCast(INDgcvPermission.GetDetailView(INDgcvPermission.FocusedRowHandle, INDgcvPermission.GetRelationIndex(INDgcvPermission.FocusedRowHandle, "Permissions")), GridView)
        Dim form = DirectCast(INDgcvPermission.GetRow(detailView.SourceRowHandle), VieForm)
        Dim action = CType(detailView.GetFocusedRow, ViePermission)
        'busco en el agregado si existe la accion para el formulario seleccionado
        Dim permisionForm = presenter.Rol.PermissionRoll.Where(Function(x) action IsNot Nothing AndAlso x.IdForm = form.Id AndAlso x.Action = action.Id)?.FirstOrDefault()
        If permisionForm IsNot Nothing Then
            If e.NewValue = True Then
                'cambio el valor a true
                permisionForm.ActionValue = True
            Else
                'si cambia a false la accion y no esta en la BD la elimino del agreagado, si ya esta guardada la cambio a false
                If permisionForm.Id = 0 Then
                    presenter.Rol.PermissionRoll.Remove(permisionForm)
                Else
                    permisionForm.MarkAsModified
                    'presenter.Rol.PermissionRoll.Add(permisionForm)
                    permisionForm.ActionValue = False
                End If
            End If
        Else
            'si no existe la accion la creo y la agrego 
            If action IsNot Nothing Then
                Dim permissionRol = New PermissionRoll
                With permissionRol
                    .IdRoll = presenter.Rol.Id
                    .IdForm = form.Id
                    .Action = action.Id
                    .ActionValue = True
                End With
                presenter.Rol.PermissionRoll.Add(permissionRol)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Refresca la informacion de la rejilla tenant
    ''' </summary>
    Private Sub RefreshGridTenant()
        If presenter.Rol.TenantRoll Is Nothing Then
            presenter.Rol.TenantRoll = New Domain.Base.Entities.TrackableCollection(Of TenantRoll)
        End If
        INDGcTenant.Invalidate()
        INDGcTenant.DataSource = presenter.Rol.TenantRoll.Where(Function(tu) tu.ChangeTracker.State <> ObjectState.Deleted)
        INDGcTenant.RefreshDataSource()
    End Sub
#End Region

#Region "Eventos de la Barra de Botones y Eventos Controles Formulario"

    'Barra de Botones

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    Private Sub BarraBotones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra("101")
    End Sub

    ''' <summary>
    ''' Este Metodo Ejecuta el metodo Deshacer
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Deshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ eliminar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Eliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ guardar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Guardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ Nuevo
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Nuevo() Handles BarraBotones.ClickNuevo
        'Deshacer()
        If FormSearchObjects IsNot Nothing Then FormSearchObjects.Close()
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Nuevo()
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ buscar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Buscar() Handles BarraBotones.ClickBuscar
        'If INDBtnEditCodigoRol.Enabled = True Then
        AbrirBusqueda()
        'End If
    End Sub

    ''' <summary>
    ''' CTRs the barra botones_ click_ actualizar.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Actualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''Otros Controles
    '''' <summary>
    '''' METODO: Evento Boton Abrir busqueda del control Codigo ROL
    '''' </summary>
    'Private Sub INDBtnEditCodigoRol_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnEditCodigoRol.ButtonClick
    '    AbrirBusqueda()
    'End Sub

    '''' <summary>
    '''' Evento enter del Control Codigo ROL
    '''' </summary>
    'Private Async Sub INDBtnEditCodigoRol_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDBtnEditCodigoRol.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        Await LoadControls()
    '    End If
    'End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control INDTxtNombreRol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxeEmail_DoubleClick(sender As Object, e As EventArgs) Handles INDTxtNombreRol.DoubleClick
        AbrirBusqueda()
    End Sub

    Private Async Function LoadControls() As Threading.Tasks.Task
        Try
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                Exit Function
            End If
            If INDBtnEditCodigoRol.Text.Trim.Length >= 1 Then
                'LayoutControl1.BeginUpdate()
                AsyncLoader(True)
                Await presenter.ConsultarNombreRol(INDBtnEditCodigoRol.Text)
                If presenter.Rol IsNot Nothing AndAlso presenter.Rol.Id > 0 Then
                    Select Case Indigo.UserType
                        Case UserType.GlobalAdmin
                        Case UserType.TenantAdmin
                            If presenter.Rol.RollType = 1 Then
                                Mensaje(EeventViewerImages.MensajeError) = "No tiene permiso para editar el rol"
                                Deshacer()
                                AsyncLoader(False)
                                Exit Function
                            End If
                        Case UserType.CompanyAdmin, UserType.StandardUser
                            Mensaje(EeventViewerImages.MensajeError) = "No tiene permiso para editar el rol"
                            Deshacer()
                            AsyncLoader(False)
                            Exit Function
                    End Select
                End If


                If presenter.Rol.Id = 0 Then
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                Else
                    BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                End If

                Me.LoadTreeModules()
                'INDgcPermission.DataSource = _listModulesForms.Where(Function(f) f.Modules IsNot Nothing AndAlso f.Modules.Count > 0).ToList()
                'INDgcPermission.DataSource = _listModulesForms
                INDgcvActionPermission.OptionsView.ShowAutoFilterRow = False
                PRollType = presenter.Rol.RollType
                If presenter.Rol.RollType IsNot Nothing Then
                    INDGleRollType.Properties.NullText = CType(presenter.Rol.RollType, eRollType).GetEnumDescription()
                End If

                If presenter.Rol.Id > 0 Then
                    LogicaBotonActualizar(True)
                    Nombre = presenter.Rol.Description.Trim
                End If
                ActivarDesactivar = True
                If INDBtnEditCodigoRol.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                INDbtnConcederTodosLosPermisos.Enabled = True
                INDbtnDeclinarTodosLosPermisos.Enabled = True
                INDTxtNombreRol.Focus()
                'LayoutControl1.EndUpdate()
                AsyncLoader(False)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

#End Region

#Region "Eventos del Control Errores"

    ' ''' <summary>
    ' ''' Declinars the todos los permisos.
    ' ''' </summary>
    'Public Sub DeclinarTodosLosPermisos() Handles INDbtnDeclinarTodosLosPermisos.Click
    '    Dim Modelo As New MRoles
    '    AsyncLoader(True)
    '    Dim ListadoPermisosMenu As List(Of GenesisPermissionForm) = Modelo.listarTodosLosPermisosFormularios()
    '    AsyncLoader(False)
    '    presenter.ConsultarTodosPermisosRol()
    '    Dim valor As String = String.Empty
    '    Dim Objeto As PermissionRoll
    '    Dim NuevoObjeto As Boolean = True
    '    For i As Integer = 0 To ListadoPermisosMenu.Count - 1
    '        If ListarTodosPermisosRol.Where(Function(e) e.IdRoll = CInt(presenter.Rol.Id) And e.IdForm.Trim = ListadoPermisosMenu.Item(i).IdForm.ToString.Trim And e.Action.Trim = ListadoPermisosMenu.Item(i).Action.ToString.Trim).Count > 0 Then
    '            NuevoObjeto = False
    '            For j As Integer = 0 To ListarTodosPermisosRol.Count - 1
    '                If ListarTodosPermisosRol.Item(j).IdRoll = CInt(presenter.Rol.Id) And ListarTodosPermisosRol.Item(j).IdForm.Trim = ListadoPermisosMenu.Item(i).IdForm.ToString.Trim And ListarTodosPermisosRol.Item(j).Action.Trim = ListadoPermisosMenu.Item(i).Action.ToString.Trim Then
    '                    Objeto = ListarTodosPermisosRol.Item(j)
    '                    Objeto.ActionValue = False
    '                    '      Objeto.ChangeTracker.State = Dominio.Entidades.ObjectState.Modified
    '                    Exit For
    '                End If
    '            Next
    '        Else
    '            NuevoObjeto = True
    '        End If
    '        If NuevoObjeto = True Then
    '            Objeto = New PermissionRoll
    '            valor = ListadoPermisosMenu.Item(i).Action.ToString.Trim
    '            Objeto.IdRoll = CInt((presenter.Rol.Id))
    '            Objeto.IdForm = ListadoPermisosMenu.Item(i).IdForm.Trim
    '            Objeto.ActionValue = False
    '            Objeto.Action = valor.ToString.Trim

    '        End If
    '        ListadoRoles.Add(Objeto)
    '    Next
    'End Sub


    Public Sub AsyncLoader1(Value As Boolean) Implements IRoles.AsyncLoader
        AsyncLoader(Value)
    End Sub
#End Region

#Region "Eventos"
    Private Sub INDgcvPermission_MasterRowExpanding(sender As Object, e As MasterRowCanExpandEventArgs) Handles INDgcvPermission.MasterRowExpanding
        Dim form = DirectCast(INDgcvPermission.GetFocusedRow(), VieForm)
        For Each action In form.Permissions
            Dim actionForm = presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.Action = action.Id).FirstOrDefault()
            If actionForm IsNot Nothing Then
                action.Value = CBool(actionForm.ActionValue)
            Else
                action.Value = False
            End If
        Next
        INDgcPermission.RefreshDataSource()
    End Sub

    Private Async Sub INDbtnGiveAllPermissions_Click(sender As Object, e As EventArgs) Handles INDbtnConcederTodosLosPermisos.Click
        AsyncLoader(True)
        Await SetAllPermissions()
        INDgcPermission.RefreshDataSource()
        AsyncLoader(False)
    End Sub

    Private Async Sub INDbtnDeclinarTodosLosPermisos_Click(sender As Object, e As EventArgs) Handles INDbtnDeclinarTodosLosPermisos.Click
        AsyncLoader(True)
        Await RemoveAllPermissions()
        INDgcPermission.RefreshDataSource()
        AsyncLoader(False)
    End Sub

    Private Sub INDbarButtonSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectAll.ItemClick
        'si se esta seleccionando un formulario obtengo la vista del detalle
        Dim detailView = TryCast(INDgcvPermission.GetDetailView(INDgcvPermission.FocusedRowHandle, INDgcvPermission.GetRelationIndex(INDgcvPermission.FocusedRowHandle, "Permissions")), GridView)

        If detailView IsNot Nothing Then
            'valido las filas seleccionadas
            Dim form = DirectCast(INDgcvPermission.GetRow(detailView.SourceRowHandle), VieForm)
            If detailView.GetSelectedRows.Length > 0 Then
                For Each index In detailView.GetSelectedRows
                    Dim action = CType(detailView.GetRow(index), ViePermission)
                    Dim actionForm = presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.Action = action.Id).FirstOrDefault()
                    'si el rol ya tenia el permiso lo marco como true
                    If actionForm IsNot Nothing Then
                        actionForm.ActionValue = True
                    Else
                        'sino agrego el permiso
                        Dim permissionRoll As New PermissionRoll
                        With permissionRoll
                            .IdRoll = presenter.Rol.Id
                            .IdForm = form.Id
                            .Action = action.Id
                            .ActionValue = True
                        End With
                        presenter.Rol.PermissionRoll.Add(permissionRoll)

                    End If
                Next
            Else
                'si no tiene filas seleccionadas marco  o agrego todas todas las acciones
                For Each action In form.Permissions
                    Dim actionForm = presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.Action = action.Id).FirstOrDefault()
                    If actionForm IsNot Nothing Then
                        actionForm.ActionValue = True
                    Else
                        Dim permissionRoll As New PermissionRoll
                        With permissionRoll
                            .IdRoll = presenter.Rol.Id
                            .IdForm = form.Id
                            .Action = action.Id
                            .ActionValue = True
                        End With
                        presenter.Rol.PermissionRoll.Add(permissionRoll)
                    End If
                Next
            End If
        Else
            'si esta sobre los grupos los recorro para marcar las acciones
            For Each item In INDgcvPermission.GetSelectedRows()
                If INDgcvActionPermission.IsGroupRow(item) Then
                    GetChildsRowsSelect(INDgcvPermission, item)
                Else
                    Dim form = DirectCast(INDgcvPermission.GetRow(item), VieForm)
                    For Each action In form.Permissions
                        Dim actionForm = presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.Action = action.Id).FirstOrDefault()
                        If actionForm IsNot Nothing Then
                            actionForm.ActionValue = True
                        Else
                            Dim permissionRoll As New PermissionRoll
                            With permissionRoll
                                .IdRoll = presenter.Rol.Id
                                .IdForm = form.Id
                                .Action = action.Id
                                .ActionValue = True
                            End With
                            presenter.Rol.PermissionRoll.Add(permissionRoll)
                        End If
                    Next
                End If
            Next
        End If
        INDgcPermission.RefreshDataSource()
    End Sub

    ''' <summary>
    '''metodo recursivo para ir cambiando valores cuando se quieren dar los permisos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRowsSelect(view As GridView, groupRowHandle As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRowsSelect(view, childHandle)
            Else
                Dim form = DirectCast(INDgcvPermission.GetRow(childHandle), VieForm)
                For Each action In form.Permissions
                    Dim actionForm = presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.Action = action.Id).FirstOrDefault()
                    If actionForm IsNot Nothing Then
                        actionForm.ActionValue = True
                    Else
                        Dim permissionRoll As New PermissionRoll
                        With permissionRoll
                            .IdRoll = presenter.Rol.Id
                            .IdForm = form.Id
                            .Action = action.Id
                            .ActionValue = True
                        End With
                        presenter.Rol.PermissionRoll.Add(permissionRoll)
                    End If
                Next
            End If

        Next
    End Sub

    Private Sub INDgcvPermission_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDgcvPermission.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            PopupMenuActions.Manager = BarManager
            PopupMenuActions.ShowPopup(INDgcvPermission.GridControl.PointToScreen(e.Point))
        End If
    End Sub

    Private Sub INDbarButtonUnSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectAll.ItemClick
        'si se esta seleccionando un formulario obtengo la vista del detalle
        Dim detailView = TryCast(INDgcvPermission.GetDetailView(INDgcvPermission.FocusedRowHandle, INDgcvPermission.GetRelationIndex(INDgcvPermission.FocusedRowHandle, "Permissions")), GridView)

        If detailView IsNot Nothing Then
            'valido cuantas acciones tiene seleccionadas para cambiar el valor
            Dim form = DirectCast(INDgcvPermission.GetRow(detailView.SourceRowHandle), VieForm)
            If detailView.GetSelectedRows.Length > 0 Then
                For Each index In detailView.GetSelectedRows
                    Dim action = CType(detailView.GetRow(index), ViePermission)
                    Dim actionForm = presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.Action = action.Id).FirstOrDefault()
                    If actionForm IsNot Nothing AndAlso actionForm.Id > 0 Then
                        actionForm.ActionValue = False
                    Else
                        presenter.Rol.PermissionRoll.Remove(actionForm)
                    End If
                Next
            Else
                'cambio el valor a todas las acciones
                If presenter.Rol.PermissionRoll.Count > 0 Then
                    For Each action In form.Permissions
                        Dim actionForm = presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.Action = action.Id).FirstOrDefault()
                        If actionForm IsNot Nothing AndAlso actionForm.Id > 0 Then
                            actionForm.ActionValue = False
                        Else
                            presenter.Rol.PermissionRoll.Remove(actionForm)
                        End If
                    Next
                End If
            End If

        Else
            'reccorro todos los grupos para cambiar el valor de las acciones
            For Each item In INDgcvPermission.GetSelectedRows()
                If INDgcvActionPermission.IsGroupRow(item) Then
                    GetChildsRowsUnSelect(INDgcvPermission, item)
                Else
                    Dim listActionTmp = presenter.Rol.PermissionRoll.Where(Function(x) x.Id = 0).ToList()
                    While listActionTmp.Count > 0
                        presenter.Rol.PermissionRoll.Remove(listActionTmp(0))
                        listActionTmp.Remove(listActionTmp(0))
                    End While
                    If presenter.Rol.PermissionRoll.Count > 0 Then
                        Dim form = DirectCast(INDgcvPermission.GetRow(item), VieForm)
                        For Each action In presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.ActionValue = True)
                            action.ActionValue = False
                        Next
                    End If
                End If
            Next
        End If
        INDgcPermission.RefreshDataSource()
    End Sub
    ''' <summary>
    ''' metodo recursivo para ir cambiando valores a cada grupo cuado se quitan permisos
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="groupRowHandle"></param>
    ''' <remarks></remarks>
    Private Sub GetChildsRowsUnSelect(view As GridView, groupRowHandle As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRowsUnSelect(view, childHandle)
            Else
                Dim listActionTmp = presenter.Rol.PermissionRoll.Where(Function(x) x.Id = 0).ToList()
                While listActionTmp.Count > 0
                    presenter.Rol.PermissionRoll.Remove(listActionTmp(0))
                    listActionTmp.Remove(listActionTmp(0))
                End While
                If presenter.Rol.PermissionRoll.Count > 0 Then
                    Dim form = DirectCast(INDgcvPermission.GetRow(childHandle), VieForm)
                    For Each action In presenter.Rol.PermissionRoll.Where(Function(x) x.IdForm = form.Id And x.ActionValue = True)
                        action.ActionValue = False
                    Next
                End If
            End If

        Next
    End Sub

    ''' <summary>
    ''' Quita un tenant del rol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRibeDeleteTenant_Click(sender As Object, e As EventArgs) Handles INDRibeDeleteTenant.Click
        Dim _TenantRoll = TryCast(INDGvTenant.GetFocusedRow, TenantRoll)
        If _TenantRoll IsNot Nothing Then
            If _TenantRoll.Id > 0 Then
                _TenantRoll.ChangeTracker.State = ObjectState.Deleted
                presenter.Rol.TenantRoll.Add(_TenantRoll)
            Else
                presenter.Rol.TenantRoll.Remove(_TenantRoll)
            End If
            RefreshGridTenant()
        End If
    End Sub

    ''' <summary>
    ''' Agrega tenant a la rejilla de de tenant
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAddTenant_Click(sender As Object, e As EventArgs) Handles INDSmbAddTenant.Click
        Dim _Mensaje = "Seleccione: "
        If PRollType Is Nothing Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Tipo rol")
        End If
        If TenantId Is Nothing OrElse TenantId = 0 Then
            _Mensaje = String.Format("{0} {1} {2}", _Mensaje, Environment.NewLine, "Tenant")
        End If
        If Not _Mensaje.Equals("Seleccione: ") Then
            Mensaje(EeventViewerImages.MensajeError) = _Mensaje
        Else

            Dim _TenantRoll As TenantRoll = presenter.Rol.TenantRoll.Where(Function(tr) tr.TenantId = TenantId.GetValueOrDefault).FirstOrDefault
            If _TenantRoll Is Nothing Then
                _TenantRoll = New TenantRoll() With {.TenantId = TenantId.GetValueOrDefault, .RollId = presenter.Rol.Id}
                Dim _Tenant = TryCast(INDSleTenant.GetSelectedDataRow(), Object)
                If _Tenant IsNot Nothing Then
                    _TenantRoll.TenantName = _Tenant.Name
                End If
                presenter.Rol.TenantRoll.Add(_TenantRoll)
            End If
            If _TenantRoll.ChangeTracker.State = ObjectState.Deleted Then
                _TenantRoll.ChangeTracker.State = ObjectState.Modified
            End If
            RefreshGridTenant()
        End If
    End Sub

    ''' <summary>
    ''' Habilita o no los controles de tenant dependiendo del tipo de rol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleRoleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRollType.EditValueChanged
        If INDGleRollType.EditValue IsNot Nothing Then
            Select Case CType(PRollType.GetValueOrDefault, eRollType)
                Case eRollType.GlobalType
                    INDLcgTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciTenants.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSleTenant.Enabled = False
                    INDLciAddTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDSmbAddTenant.Enabled = False
                    INDLciGcTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDGcTenant.Enabled = False
                    TenantDataSource = Nothing
                Case eRollType.ByTenant
                    INDLcgTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciTenants.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDSleTenant.Enabled = True
                    INDLciAddTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDSmbAddTenant.Enabled = True
                    INDLciGcTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDGcTenant.Enabled = True
                    RefreshGridTenant()
                Case Else
            End Select
        Else
            INDLcgTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciTenants.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleTenant.Enabled = False
            INDLciAddTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSmbAddTenant.Enabled = False
            INDLciGcTenant.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGcTenant.Enabled = False
            TenantDataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Carga el control de tipos de rol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleRoleType_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleRollType.QueryPopUp
        If INDGleRollType.Properties.DataSource Is Nothing Then
            ListRoleType = New List(Of Tuple(Of Byte, String))
            If ApplicationSetting.Instance.LoginAzure Then
                Select Case Indigo.UserType
                    Case UserType.GlobalAdmin
                        ListRoleType.Add(New Tuple(Of Byte, String)(1, "Global"))
                        ListRoleType.Add(New Tuple(Of Byte, String)(2, "Por tenant"))
                    Case UserType.TenantAdmin
                        ListRoleType.Add(New Tuple(Of Byte, String)(2, "Por tenant"))
                    Case UserType.CompanyAdmin
                    Case UserType.StandardUser
                End Select
            Else
                Select Case Indigo.UserType
                    Case UserType.GlobalAdmin
                        ListRoleType.Add(New Tuple(Of Byte, String)(2, "Por tenant"))
                    Case UserType.TenantAdmin
                        ListRoleType.Add(New Tuple(Of Byte, String)(2, "Por tenant"))
                    Case UserType.CompanyAdmin
                    Case UserType.StandardUser
                End Select
            End If

            INDGleRollType.Properties.DataSource = ListRoleType
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTenant_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTenant.QueryPopUp
        If INDSleTenant.Properties.DataSource Is Nothing Then
            presenter.GetAllTenant(Indigo.UserIndigoId, Indigo.UserType)
        End If
    End Sub
#End Region
End Class