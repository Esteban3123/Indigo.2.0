'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 13-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importaciones"

Imports System.Configuration
Imports DevExpress.XtraEditors
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Security.MVP
Imports Presentation.Controls
Imports System.IO
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Clase que controla los comprotamientos del formulario FrmDesbloquearUsuarios
''' </summary>
Public Class FrmDesbloquearUsuarios
    Inherits Presentation.Controls.FormBase
    'Inherits DevExpress.XtraEditors.XtraForm
    Implements IDesbloquearUsuario

#Region "Variables y Load"

    ''' <summary>
    ''' Evento que controla el load del control FrmDesbloquearUsuarios.
    ''' </summary>
    Private Sub FrmDesbloquearUsuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        presenter = New PDesbloquearUsuario(Me)
        presenter.InicializarComponentes()
        BarraBotones.ActualizarPermisosBarra(Me.Tag.ToString)
        INDBtnEditCodigoDesbloquearUsuario.Focus()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
    End Sub

    ''' <summary>
    ''' Variable que instancia al presentador
    ''' </summary>
    Dim presenter As PDesbloquearUsuario

#End Region

#Region "Propiedades de la Interfaz"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del usuario seleccionado
    ''' </summary>
    Public Property CodigoDelUsuario As String Implements IDesbloquearUsuario.CodigoDelUsuario
        Get
            Return INDBtnEditCodigoDesbloquearUsuario.EditValue.ToString.Trim
        End Get
        Set(ByVal value As String)
            INDBtnEditCodigoDesbloquearUsuario.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del usuario
    ''' </summary>
    Public Property NombreDelUsuario As String Implements IDesbloquearUsuario.NombreDelUsuario
        Get
            Return INDTxtNombreDesbloquearUsuario.Text.ToString.Trim
        End Get
        Set(ByVal value As String)
            INDTxtNombreDesbloquearUsuario.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el valor de un mensaje que se va a mostrar en el visor de eventos
    ''' </summary>
    ''' <value></value>
    Public WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String Implements MVP.IDesbloquearUsuario.Mensaje
        Set(ByVal value As String)
            
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar,"")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad sirve para permitir o no permitir algunos controles de la vista
    ''' </summary>
    Public WriteOnly Property HabilitarControles As Boolean Implements IDesbloquearUsuario.HabilitarControles
        Set(ByVal value As Boolean)
            INDTxtNombreDesbloquearUsuario.Enabled = value
        End Set
    End Property

#End Region

#Region "Eventos de la Barra de Botones eventos controles formulario"

    ''' <summary>
    ''' Metodo que se ejecuta al cargar el formularo Desbloquear usuarios
    ''' </summary>
    Private Sub CtrBarraBotones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarraBotones.Load
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Favoritos) = True
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    '''  CONTROLUSUARIO: click boton buscar llamamos el metodo Buscar() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Buscar() Handles BarraBotones.ClickBuscar
        AbrirFormularioBusqueda()
    End Sub

    ''' <summary>
    ''' CONTROLUSUARIO: click boton deshacer llamamos el metodo Deshacer() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Deshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' CONTROLUSUARIO: click boton guardar llamamos el metodo Buscar() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Guardar() Handles BarraBotones.ClickProcesar
        Guardar()
    End Sub

    ''' <summary>
    ''' CONTROLUSUARIO: click boton nuevo llamamos el metodo Nuevo() que va al presentador.
    ''' </summary>
    Private Sub CtrBarraBotones_Click_Nuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo para limpiar el nombre del usuario cuando se cambia el codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnEditCodigoDesbloquearUsuario_TextChanged(sender As Object, e As EventArgs) Handles INDBtnEditCodigoDesbloquearUsuario.TextChanged
        INDTxtNombreDesbloquearUsuario.Text = String.Empty
    End Sub

    Public Sub AsyncLoader1(Value As Boolean) Implements IDesbloquearUsuario.AsyncLoader
        AsyncLoader(Value)
    End Sub

    ''' <summary>
    ''' METODO: ejecuta el metodo del presentador para borrar el contenido de los campos del formulario.
    ''' </summary>
    Private Sub Deshacer()
        presenter.Deshacer()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
        INDBtnEditCodigoDesbloquearUsuario.Focus()
    End Sub

    ''' <summary>
    ''' METODO: ejecuta el metodo del presentador para guardar la nueva configuracion del usuario.
    ''' </summary>
    Private Sub Guardar()
        'presenter.GuardarConfiguracionUsuario() 'no se usa
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para abrir el formulario de busqueda
    ''' </summary>
    Public Sub AbrirFormularioBusqueda() Implements MVP.IDesbloquearUsuario.AbrirFormularioBusqueda
        Dim tipoCrud As Infrastructure.CrossCutting.Base.eDataSource = Infrastructure.CrossCutting.Base.eDataSource.UsersCRUD
        Dim listColumns As List(Of ColumnInfo) = {New ColumnInfo With {.Caption = "Código", .FieldName = "UserCode"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "PersonFullName"}, New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnAligment = DevExpress.Utils.HorzAlignment.Center}}.ToList()

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = listColumns
            .ValorSolicitado = "UserCode"
            .ListadoOrigenDatos = tipoCrud
            'BarraBotones.PrepareToolbar(eAction.OnlyUndo)
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
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDBtnEditCodigoDesbloquearUsuario.Text = ReturnValue
        If INDBtnEditCodigoDesbloquearUsuario.Text <> String.Empty Then
            presenter.ConsultarUsuario(INDBtnEditCodigoDesbloquearUsuario.Text)
            If INDBtnEditCodigoDesbloquearUsuario.Enabled = False Then
                'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnEditCodigoDesbloquearUsuario.Enabled = False
        End If
    End Sub

#End Region

#Region "Eventos del Frontal"

    ''' <summary>
    ''' Evento que ejecuta el metodo del presentador para abrir el frontal de busqueda.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDBtnEditCodigoDesbloquearUsuario_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnEditCodigoDesbloquearUsuario.ButtonClick
        AbrirFormularioBusqueda()
    End Sub

    ''' <summary>
    ''' Maneja el evento KeyDown del control INDButtonEditCodigoDesbloquearUsuario.
    ''' </summary>
    ''' 
    Private Sub INDBtnEditCodigoDesbloquearUsuario_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDBtnEditCodigoDesbloquearUsuario.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDBtnEditCodigoDesbloquearUsuario.Text = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = String.Concat(obtenerRecurso(ComunesCodigoVacio), " ", obtenerRecurso(UsuarioMensajeComplemento, Usuario))
                'ElseIf INDBtnEditCodigoDesbloquearUsuario.Text.Trim.Length < 3 Then
                '   Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesCodigoCaracteres)
            Else
                presenter.ConsultarUsuario(CodigoDelUsuario)
            End If
        End If
    End Sub

#End Region

End Class