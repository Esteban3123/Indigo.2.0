'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Sergio Fernandez
' Created          : 02-11-2011
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.IO
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports System.Runtime.InteropServices
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base

Public Class CtrPhoto

#Region "Eventos Publicos"
    Public Event cargar()
    Public Event ImageChange()
#End Region

#Region "Enumumeraciones"

    Enum eImagen
        Usuario = 1
        Mantenimiento = 2
    End Enum

#End Region

#Region "Variables y Load"



    ' variable que contiene el id del dispositivo
    Dim codigoDispositivo As Integer = 0
    'variable que controla la ventana de vista previa
    Dim hHwnd As Integer
    Dim iHeight As Integer
    Dim iWidth As Integer

    'Contiene un listado del nombre del dispositivo de la camara
    Dim dispositivoNombre As New List(Of String)
    'Contiene un listado del IDdel dispositivo de la camara
    Dim dispositivoId As New List(Of Integer)

    WriteOnly Property SetImage As eImagen
        Set(value As eImagen)
            If value = eImagen.Mantenimiento Then
                INDfotoUsuario.Image = My.Resources.ImagenEquipos200x200
            End If
        End Set
    End Property
    ''' <summary>
    ''' Evento load del control de usuario CtrFoto
    ''' </summary>
    Private Sub CtrFoto_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'cargo los dispositivos de camara
        CargarDispositivosCamara()
        'si el listbox es vacio no hay dispositvos de camara y no se realiza ninguna accion
        BuscarDispositivos()
        LimpiarControles()
    End Sub

    'constantes
    Const WM_CAP As Short = &H400S

    Const WM_CAP_DRIVER_CONNECT As Integer = WM_CAP + 10
    Const WM_CAP_DRIVER_DISCONNECT As Integer = WM_CAP + 11
    Const WM_CAP_EDIT_COPY As Integer = WM_CAP + 30

    Const WM_CAP_SET_PREVIEW As Integer = WM_CAP + 50
    Const WM_CAP_SET_PREVIEWRATE As Integer = WM_CAP + 52
    Const WM_CAP_SET_SCALE As Integer = WM_CAP + 53
    Const WS_CHILD As Integer = &H40000000
    Const WS_VISIBLE As Integer = &H10000000
    Const SWP_NOMOVE As Short = &H2S
    Const SWP_NOSIZE As Short = 1
    Const SWP_NOZORDER As Short = &H4S
    Const HWND_BOTTOM As Short = 1




#End Region

#Region "Funciones"

    'Funcion que consulta si hay dispositivos encontrados.
    Public Function BuscarDispositivos() As Boolean
        If dispositivoNombre.Count > 0 Then
            INDBtnCapturar.Visible = True
            INDBtnIniciar.Visible = True
            INDBtnCapturar.Visible = False
            Return True
        Else
            INDBtnCapturar.Visible = False
            INDBtnIniciar.Visible = False
            Return False
        End If
    End Function

    'Funcion que Carga la imagen con un openFileDialog
    Public Function CargarImagen() As Boolean
        Dim myStream As Stream = Nothing
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                If (myStream IsNot Nothing) Then
                    INDfotoUsuario.EditValue = Image.FromFile(openFileDialog1.FileName)
                    INDfotoUsuario.Tag = 1
                    RaiseEvent ImageChange()
                    Return True
                End If
            Catch Ex As Exception
            Finally
                If (myStream IsNot Nothing) Then
                    myStream.Close()
                End If
            End Try
        Else
        End If
        Return False
    End Function

    Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.AsAny)> ByVal lParam As Object) As Integer
    'funcion que establece la posicion de la ventana de captura
    Declare Function SetWindowPos Lib "user32" Alias "SetWindowPos" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    'Funcion que destruye la ventana de captura despues de haver tomado la foto.
    Declare Function DestroyWindow Lib "user32" (ByVal hndw As Integer) As Boolean
    'Funcion que la imagen
    Declare Function capCreateCaptureWindowA Lib "avicap32.dll" (ByVal lpszWindowName As String, ByVal dwStyle As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Short, ByVal hWndParent As Integer, ByVal nID As Integer) As Integer
    'Funcion que captura la description de controlador de video
    Declare Function capGetDriverDescriptionA Lib "avicap32.dll" (ByVal wDriver As Short, ByVal lpszName As String, ByVal cbName As Integer, ByVal lpszVer As String, ByVal cbVer As Integer) As Boolean

    ''' <summary>
    ''' Funcion que abre la ventana de captura en el control INDfotoUsuario
    ''' </summary>
    Public Function OpenPreviewWindow() As Boolean
        'igualamos esta variable a la altura de nuestro INDfotoUsuario
        iHeight = INDfotoUsuario.Height
        'igualamos esta variable a la ancho de nuestro INDfotoUsuario
        iWidth = INDfotoUsuario.Width
        ' Abrimos la ventada de captura en nuestro INDfotoUsuario
        hHwnd = capCreateCaptureWindowA(CStr(codigoDispositivo), WS_VISIBLE Or WS_CHILD, 0, 0, 222, 165, INDfotoUsuario.Handle.ToInt32, 0)
        'Conectamos con el dispositivo de captura 
        Dim a As Integer = SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, codigoDispositivo, 0)
        If a = 1 Then
            'Establecemos la escala de captura
            SendMessage(hHwnd, WM_CAP_SET_SCALE, CInt(True), 0)

            'Set the preview rate in milliseconds
            SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0)

            'iniciamos la vista previa del dispositivo
            SendMessage(hHwnd, WM_CAP_SET_PREVIEW, CInt(True), 0)

            'Acoplamos el tamaño de la captura al tamaño de nuestro control INDfotoUsuario
            SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, INDfotoUsuario.Width, INDfotoUsuario.Height, SWP_NOMOVE Or SWP_NOZORDER)

            INDBtnCapturar.Visible = True
            INDBtnIniciar.Visible = False
            Return True
        Else
            'Si no se realiza la conexion con el dispositivo , cerramos la ventana de captura
            DestroyWindow(hHwnd)
            'mostramos como inactivo el boton de capturar imagen
            INDBtnCapturar.Visible = False
            Return False
        End If
    End Function

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Evento Click del boton INDBtnCargar
    ''' </summary>
    Private Sub INDBtnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDBtnCargar.Click
        CargarImagen()
        'Consumo el Evento Publico
    End Sub

    ''' <summary>
    ''' Metodo para cargar los dispositivos de video.
    ''' </summary>
    Private Sub CargarDispositivosCamara()
        '    'variable que contiene el nombre del dispositivo
        Dim strName As String = "                                                                                                    "
        '    'version del dispositivo
        Dim strVer As String = "                                                                                                    "
        Dim bReturn As Boolean
        Dim x As Integer = 0
        '    'con este ciclo logro sacar el nombre y el id del dispositivo .
        Do
            bReturn = capGetDriverDescriptionA(CShort(x), strName, 100, strVer, 100)
            If bReturn = True Then
                dispositivoNombre.Add(strName)
                dispositivoId.Add(x)
            End If
            x += 1
        Loop Until bReturn = False
    End Sub

    ''' <summary>
    ''' Evento  Click  del control INDBtnIniciar 
    ''' </summary>
    Private Sub INDBtnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDBtnIniciar.Click
        Try
            'si tenemos un dispositivo disponible podemos iniciar la captura
            codigoDispositivo = dispositivoId.Item(0)
            Do Until OpenPreviewWindow() = True
            Loop
            INDBtnCargar.Visible = False
        Catch ex As Exception
            MessageIndigo.Show("No se puede iniciar la camara", Botones.Ok, MessageType.Warning)
        End Try
    End Sub

    ''' <summary>
    ''' Cierra la ventana de captura y desconecta el controlador de video
    ''' </summary>
    Private Sub CerrarVentanadeCaptura()
        'Disconnect from device
        SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, codigoDispositivo, 0)
        'close(window)
        DestroyWindow(hHwnd)
    End Sub

    ''' <summary>
    ''' Evento Click del control INDBtnCapturar.
    ''' </summary>
    Private Sub INDBtnCapturar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDBtnCapturar.Click
        Dim data As IDataObject
        Dim bmap As Image
        '     Copy image to clipboard
        SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0)
        ' Get image from clipboard and convert it to a bitmap
        data = Clipboard.GetDataObject()
        If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
            bmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Image)
            INDfotoUsuario.Image = bmap
            CerrarVentanadeCaptura()
            INDBtnCapturar.Visible = False
            INDBtnIniciar.Visible = True
            INDBtnCargar.Visible = True
            INDfotoUsuario.Tag = 1
            RaiseEvent ImageChange()
        End If
    End Sub
    Property INDOrigen As Integer
    ''' <summary>
    ''' Evento Click del control INDBtnCancelar.
    ''' </summary>
    Private Sub INDBtnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDBtnCancelar.Click
        BuscarDispositivos()
        If INDOrigen <> 1 Then
            Me.INDfotoUsuario.EditValue = Global.Presentation.Controls.My.Resources.Resources.Usuario
        End If
        CerrarVentanadeCaptura()
    End Sub

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Metodo que obtiene o establece la foto
    ''' </summary>
    ''' <value>
    ''' The foto.
    ''' </value>
    Public Property Foto As Byte()
        Get
            If DesignMode = False Then
                Select Case PhotoType
                    Case eImagen.Mantenimiento
                        If INDfotoUsuario.Tag = 0 Then
                            Return Nothing
                        End If
                    Case eImagen.Usuario
                        If INDfotoUsuario.Tag = 0 Then
                            Return Nothing
                        End If
                End Select
                Dim FotoUser As New MemoryStream()
                INDfotoUsuario.Image.Save(FotoUser, System.Drawing.Imaging.ImageFormat.Png)
                Return FotoUser.GetBuffer()
            Else
                Return Nothing
            End If
        End Get
        Set(value As Byte())
            If value IsNot Nothing Then
                INDfotoUsuario.EditValue = value
                INDfotoUsuario.Tag = 1
            End If
        End Set
    End Property

    Private _PhotoType As eImagen
    Public Property PhotoType As eImagen
        Get
            Return _PhotoType
        End Get
        Set(value As eImagen)
            INDfotoUsuario.Tag = 0
            Select Case value
                Case eImagen.Mantenimiento
                    INDfotoUsuario.Image = Presentation.Controls.My.Resources.Resources.ImagenEquipos200x200
                Case eImagen.Usuario
                    INDfotoUsuario.Image = Presentation.Controls.My.Resources.Resources.Usuario
            End Select
            _PhotoType = value
        End Set
    End Property

    ''' <summary>
    '''  Funcion que Limpia el control.
    ''' </summary>
    ''' <returns></returns>
    Public Function LimpiarControles() As Boolean
        INDfotoUsuario.Tag = 0
        Select Case PhotoType
            Case eImagen.Mantenimiento
                INDfotoUsuario.EditValue = Global.Presentation.Controls.My.Resources.Resources.ImagenEquipos200x200
            Case eImagen.Usuario
                INDfotoUsuario.EditValue = Global.Presentation.Controls.My.Resources.Resources.Usuario
        End Select
        Return True
    End Function

#End Region

    Private Sub INDBtnCapturar_VisibleChanged(sender As Object, e As EventArgs) Handles INDBtnIniciar.VisibleChanged, INDBtnCargar.VisibleChanged, INDBtnCapturar.VisibleChanged, INDBtnCancelar.VisibleChanged
        INDBtnIniciar.Visible = True
        INDBtnCargar.Visible = True
        INDBtnCapturar.Visible = True
        INDBtnCancelar.Visible = True
    End Sub

    Private Sub INDpanelControles_VisibleChanged(sender As Object, e As EventArgs) Handles INDpanelControles.VisibleChanged
        INDpanelControles.Visible = True
    End Sub

    Private Sub INDfotoUsuario_Click(sender As Object, e As EventArgs) Handles INDfotoUsuario.Click
        INDpeImageLarge.Image = INDfotoUsuario.Image
        INDpceImageLarge.ShowPopup()
    End Sub
End Class
