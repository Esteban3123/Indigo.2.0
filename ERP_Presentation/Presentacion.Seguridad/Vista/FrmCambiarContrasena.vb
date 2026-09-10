'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 25-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Liberias Importadas"

Imports DevExpress.XtraEditors
Imports Presentation.Security.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Security.Entities

#End Region

Public Class FrmCambiarContrasena
    Inherits Presentation.Controls.FormBase
    Implements ICambiarContraseña

#Region "Variables y load"
    ''' <summary>
    ''' Variable que se utiliza para instanciar usuario
    ''' </summary>
    ''' <remarks></remarks>
    Public Property User As User Implements ICambiarContraseña.User
    ''' <summary>
    ''' Variable que instancia al presentador
    ''' </summary>
    Dim presenter As PCambiarContrasena
    ''' <summary>
    ''' Variable que instania la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Evento que se dispara despues de que se a cambiado correctamente la contraseña
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ChangePassword(context As FrmCambiarContrasena)
    ''' <summary>
    ''' Este evento se ejecuta al cargar el formulario e inicializa una nueva instancia del presentador
    ''' </summary>
    Private Sub frmCambiarContraseña_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        presenter = New PCambiarContrasena(Me)
        presenter.InicializarComponentes()
        BarraBotones.ActualizarPermisosBarra(CStr(Me.Tag))
        If ChangePasswordAllUsers = True Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            INDlyiUserCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiUserName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiPasswordBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            INDlyiUserCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiUserName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.ChangePasswordAllUsers = (Indigo.UserType <> UserType.StandardUser)
    End Sub

#End Region

#Region "propiedades"
    ''' <summary>
    ''' Propiedad que obtiene o establece si se puede cambiar la contraseña de cualquier usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChangePasswordAllUsers As Boolean

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    ''' <value></value>
    Public WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String Implements MVP.ICambiarContraseña.Mensaje
        Set(ByVal value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene la contraseña anterior del usuario.
    ''' </summary>
    Public Property ContraseñaAnterior As String Implements ICambiarContraseña.ContraseñaAnterior
        Get
            Return INDTxtCoAnterior.Text
        End Get
        Set(ByVal value As String)
            INDTxtCoAnterior.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene la nueva contraseña del usuario
    ''' </summary>
    Public Property NuevaContraseña As String Implements ICambiarContraseña.NuevaContraseña
        Get
            Return INDTxtNuContraseña.Text
        End Get
        Set(ByVal value As String)
            INDTxtNuContraseña.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene la confirmacion de la contraseña del usuario
    ''' </summary>
    Public Property ConfirmarContrasena As String Implements ICambiarContraseña.ConfirmarContraseña
        Get
            Return INDtxtConfContra.Text
        End Get
        Set(ByVal value As String)
            INDtxtConfContra.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta Propiedad establece el foco en el control que mande en el parametro
    ''' </summary>
    Public WriteOnly Property EstablecerFoco(ByVal NombreControl As String) As Boolean Implements MVP.ICambiarContraseña.EstablecerFoco
        Set(ByVal value As Boolean)


        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Habilitar los controles despues de la contraseña estar correcta
    ''' </summary>
    Public WriteOnly Property HabilitarControles As Boolean Implements MVP.ICambiarContraseña.HabilitarControles
        Set(ByVal value As Boolean)
            If ChangePasswordAllUsers = True Then
                INDbteUserCode.Enabled = Not value
                INDTxtNuContraseña.Enabled = value
                INDtxtConfContra.Enabled = value
            Else
                INDTxtNuContraseña.Enabled = value
                INDtxtConfContra.Enabled = value
                INDTxtCoAnterior.Enabled = Not value
            End If
        End Set
    End Property

#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    ''' Evento click_ guardar de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_Click_Guardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento click_Actualizar de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_Click_Actualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento click_ Deshacer de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_Click_Deshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento click_ Nuevo de la barra de botones.
    ''' </summary>
    Private Sub Click_Nuevo() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

#End Region

#Region "Metodos"
    Public Sub AsyncLoader1(valor As Boolean) Implements ICambiarContraseña.AsyncLoader
        AsyncLoader(valor)
    End Sub

    ''' <summary>
    ''' Metodo que Deshace todas las operaciones realizadas y Limpia los controles utilizados
    ''' </summary>
    Public Sub Deshacer() Implements MVP.ICambiarContraseña.Deshacer
        presenter.Deshacer()
        INDtxtUserName.Text = String.Empty
        INDbteUserCode.Text = String.Empty
        If ChangePasswordAllUsers = True Then
            INDbteUserCode.Focus()
        Else
            INDTxtCoAnterior.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que sirve para guardar los cambios realizados en la contraseña
    ''' </summary>
    Private Async Sub Guardar() Implements MVP.ICambiarContraseña.Guardar
        If String.IsNullOrEmpty(INDtxtConfContra.Text) Then
            Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaConfirmacionVacia, Eform.CambiarContrasena)
            Return
        ElseIf INDtxtConfContra.Text.Length < 7 Then
            Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaMinimoCaracteres, Eform.CambiarContrasena)
            INDtxtConfContra.Text = String.Empty
            Return
        End If
        Me.AsyncLoader(True)
        If ChangePasswordAllUsers = True Then
            'Await presenter.CambiarContrasena(User.UserCode)
            Await presenter.CambiarContrasena(User.Id)
        Else
            'Await presenter.CambiarContrasena(Indigo.UserIndigo)
            Await presenter.CambiarContrasena(Indigo.UserIndigoId)
        End If
        RaiseEvent ChangePassword(Me)
        Me.AsyncLoader(False)
        Deshacer()
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(ByVal existeDatos As Boolean) Implements MVP.ICambiarContraseña.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

#End Region

#Region "Eventos que controlan las validaciones de los controles"

    ''' <summary>
    ''' Evento de tipo KeyDown que valida el campo ConfirmarContraseña
    ''' </summary>
    Private Sub INDtxtConfContra_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDtxtConfContra.KeyDown
        If e.KeyData = Keys.Enter Then
            If String.IsNullOrEmpty(INDtxtConfContra.Text) Then
                Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaConfirmacionVacia, Eform.CambiarContrasena)
            ElseIf INDtxtConfContra.Text.Length < 7 Then
                Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaMinimoCaracteres, Eform.CambiarContrasena)
                INDtxtConfContra.Text = String.Empty
            Else
                Guardar()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento de tipo KeyDown que valida el campo Contraseña anterior
    ''' </summary>
    Private Async Sub INDTxtCoAnterior_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDTxtCoAnterior.KeyDown
        If e.KeyData = Keys.Enter Then
            If String.IsNullOrEmpty(INDTxtCoAnterior.Text) Then
                Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaAnteriorVacia, Eform.CambiarContrasena)
            ElseIf INDTxtCoAnterior.Text.Length < 7 Then
                Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaMinimoCaracteres, Eform.CambiarContrasena)
            Else
                If ChangePasswordAllUsers = True Then
                    'Await presenter.CambiarContrasena(User.UserCode)
                    Await presenter.CambiarContrasena(User.Id)
                Else
                    'Await presenter.CambiarContrasena(Indigo.UserIndigo)
                    Await presenter.CambiarContrasena(Indigo.UserIndigoId)
                End If
                INDTxtNuContraseña.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento de tipo KeyDown que valida el campo Nueva Contraseña
    ''' </summary>
    Private Sub INDTxtNuContraseña_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDTxtNuContraseña.KeyDown
        If e.KeyData = Keys.Enter Then
            If String.IsNullOrEmpty(INDTxtNuContraseña.Text) Then
                Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaNuevaVacia, Eform.CambiarContrasena)
            ElseIf INDTxtNuContraseña.Text.Length < 7 Then
                Mensaje(EeventViewerImages.Advertencia) = BaseClass.obtenerRecurso(Eresources.ContrasenaMinimoCaracteres, Eform.CambiarContrasena)
                INDTxtNuContraseña.Text = String.Empty
            End If
        End If

    End Sub

    ''' <summary>
    ''' Evento clic en el boton buscar del caja de texto de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteUserCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteUserCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Metodo para consultar el usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Threading.Tasks.Task
        Using modelo = New MUsuario
            AsyncLoader(True)
            User = Await modelo.ConsultarUsuario(INDbteUserCode.Text)
            AsyncLoader(False)
        End Using
        If User Is Nothing OrElse User.Id = 0 Then
            MessageIndigo.Show(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("UserNotExist", Me.GetType()), MessageType.Warning, Me.Text)
        Else
            BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            INDtxtUserName.Text = User.Person.Fullname
            INDbteUserCode.Enabled = False
            INDTxtNuContraseña.Enabled = True
            INDtxtConfContra.Enabled = True
            INDTxtNuContraseña.Focus()
        End If
    End Function

    ''' <summary>
    ''' Evento cuando presiona una tecla en la caja de texto del codigo de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteUserCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteUserCode.KeyDown
        If e.KeyCode = Keys.Enter AndAlso INDbteUserCode.Text <> String.Empty Then
            Await LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Metodo de la barra botones para abrir el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub
#End Region

#Region "Metodos InterfaceCrudBase"
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer1() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar1() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar1(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje1(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Metodo para abrir el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Dim tipoCrud As Infrastructure.CrossCutting.Base.eDataSource = Infrastructure.CrossCutting.Base.eDataSource.UsersCRUD
        Dim listColumns As List(Of ColumnInfo) = {New ColumnInfo With {.Caption = "Código", .FieldName = "UserCode"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "PersonFullName"}, New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnAligment = DevExpress.Utils.HorzAlignment.Center}}.ToList()

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = listColumns
            .ValorSolicitado = "UserCode"
            .ListadoOrigenDatos = tipoCrud
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteUserCode.Text = ReturnValue
        If INDbteUserCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteUserCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteUserCode.Enabled = False
        End If
    End Sub

#End Region

End Class