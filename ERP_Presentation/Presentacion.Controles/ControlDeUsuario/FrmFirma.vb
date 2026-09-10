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

#Region "Referencias"
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
#End Region

Public Class FrmFirma
    Implements IDisposable

    '#Region "Variables"
    '    Dim CapturaPantalla As System.Drawing.Bitmap
    '    Dim WithEvents myInkOverlay As New Object
    '    Property limpiar As Boolean
    '    Dim aceptar As Boolean
    '#End Region

    '#Region "Eventos"
    '    ''' <summary>
    '    ''' Evento  FormClosing  que al dispararse envia la imagen al ctrfirmadigital.
    '    ''' </summary>
    '    Private Sub FrmFirma_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '        Dim control As New CtrFirmaDigital
    '        enviar = CapturaPantalla
    '        If aceptar <> True Then
    '            limpiar = True
    '        End If
    '    End Sub
    '    Private Sub FrmFirma_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    '        INDPictureEditFirma.EditValue = Global.Presentation.Controls.My.Resources.Resources.vacia

    '    End Sub
    '#End Region

    '#Region "Metodos"
    '    ''' <summary>
    '    ''' Metodo que captura la imagen de la firma
    '    ''' </summary>
    '    Private Sub CapturarImagen()
    '        'Captura solo un parte de la pantalla.
    '        'Crea un rectangulo y objeto bitmap.
    '        Dim Recuadro As Rectangle
    '        'Crea un objeto garfico.
    '        Dim Grafico As Graphics
    '        'Obtengo lo que tiene el control INDPictureEditFirma
    '        Recuadro = INDPictureEditFirma.Bounds
    '        'Establezco los rangos de captura

    '        Dim x As Integer
    '        x = Me.Location.X + 100
    '        Dim y As Integer
    '        y = Me.Location.Y + 30
    '        'Crea un nuevo bitmap con los datos del rectangulo.
    '        CapturaPantalla = New System.Drawing.Bitmap(Recuadro.Width - 90, Recuadro.Height - 0, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
    '        'Asigna el bitmap a graph, como lienso para cargar la imagen.
    '        Grafico = Graphics.FromImage(CapturaPantalla)
    '        Grafico.CopyFromScreen(x, y, 0, 0, Recuadro.Size, CopyPixelOperation.SourceCopy) 'hace la captura en el lienso oScreenShot.
    '    End Sub

    '    ''' <summary>
    '    ''' Inicializo los componentes del control de usuario CtrFirma.
    '    ''' </summary>
    '    Public Sub New()
    '        InitializeComponent()
    '        'Initialize myInkOverlay and associate it with basicTextBox
    '        myInkOverlay = New InkOverlay(INDPictureEditFirma)
    '        myInkOverlay.Enabled = True
    '        INDPictureEditFirma.EditValue = Global.Presentation.Controls.My.Resources.Resources.vacia
    '    End Sub
    '#End Region

    '#Region "Propiedades"
    '    ''' <summary>
    '    ''' Propiedad que envia la captura de imagen de la firma
    '    ''' </summary>
    '    ''' <value>The enviar.</value>
    '    Property enviar As Object
    '        Get
    '            Return CapturaPantalla
    '        End Get
    '        Set(value As Object)
    '            CapturaPantalla = CType(value, Bitmap)
    '        End Set
    '    End Property
    '#End Region

    '#Region "Eventos Navbar"


    '    Private Sub INDitemRecortar_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles INDitemRecortar.LinkClicked
    '        myInkOverlay.EditingMode = InkOverlayEditingMode.Select
    '    End Sub

    '    Private Sub INDitemBorrar_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles INDitemBorrar.LinkClicked
    '        myInkOverlay.EditingMode = InkOverlayEditingMode.Delete
    '    End Sub

    '    Private Sub INDitemCancelar_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles INDitemCancelar.LinkClicked
    '        Me.Close()
    '    End Sub

    '    Private Sub INDitemColor_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles INDitemColor.LinkClicked
    '        Dim dlgColor As New ColorDialog()
    '        dlgColor.Color = Me.myInkOverlay.DefaultDrawingAttributes.Color
    '        If dlgColor.ShowDialog(Me) = DialogResult.OK Then
    '            Me.myInkOverlay.DefaultDrawingAttributes.Color = dlgColor.Color
    '        End If
    '    End Sub

    '    Private Sub INDitemIniciar_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles INDitemIniciar.LinkClicked
    '        myInkOverlay.EditingMode = InkOverlayEditingMode.Ink
    '    End Sub

    '    Private Sub INDitemAceptarFirma_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles INDitemAceptarFirma.LinkClicked
    '        CapturarImagen()
    '        enviar = CapturaPantalla
    '        aceptar = True
    '        Me.Close()
    '    End Sub

    '    Private Sub INDitemAjustarTamaño_LinkClicked(ByVal sender As System.Object, ByVal e As DevExpress.XtraNavBar.NavBarLinkEventArgs) Handles INDitemAjustarTamaño.LinkClicked
    '        Dim width = myInkOverlay.DefaultDrawingAttributes.Width
    '        Dim Recuadro As New Rectangle(New Point(Me.Location.X + 2000, Me.Location.Y + 1000), New Size(Me.Size.Width * 20, Me.Size.Height * 15))
    '        myInkOverlay.Selection.ScaleToRectangle(Recuadro)
    '    End Sub
    '#End Region

    '    Private Sub INDPictureEditFirma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDPictureEditFirma.MouseDown
    '        INDitemAceptarFirma.Enabled = False
    '        INDitemBorrar.Enabled = False
    '        INDitemIniciar.Enabled = False
    '        INDitemRecortar.Enabled = False
    '        INDitemCancelar.Enabled = False
    '        INDitemColor.Enabled = False
    '        INDitemAjustarTamaño.Enabled = False
    '    End Sub

    '    Private Sub NavBarControl1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NavBarControl1.MouseHover
    '        INDitemAceptarFirma.Enabled = True
    '        INDitemBorrar.Enabled = True
    '        INDitemIniciar.Enabled = True
    '        INDitemRecortar.Enabled = True
    '        INDitemCancelar.Enabled = True
    '        INDitemColor.Enabled = True
    '        INDitemAjustarTamaño.Enabled = True
    ' End Sub

End Class