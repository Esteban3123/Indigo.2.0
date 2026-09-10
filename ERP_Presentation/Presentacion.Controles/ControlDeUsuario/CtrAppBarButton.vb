'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 29-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Clase con toda la funcionalidad del control app bar
''' </summary>
Public Class CtrAppBarButton

#Region "Variables y eventos"
    ''' <summary>
    ''' Evento que se dispara al hacer clic sobre el control
    ''' </summary>
    Public Event Accion()
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Propiedad que obtiene o establece el valor del texto a mostrar en el control
    ''' </summary>
    ''' <value>
    ''' el texto.
    ''' </value>
    Public Property INDTexto As String
        Get
            Return INDTextLabel.Text
        End Get
        Set(value As String)
            INDTextLabel.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el icono a mostrar en el control.
    ''' </summary>
    ''' <value>
    ''' el icono de Segoe UI Symbol.
    ''' </value>
    Public Property INDIcono As String
        Get
            Return INDContent.Text
        End Get
        Set(value As String)
            INDContent.Text = value
        End Set
    End Property
#End Region

#Region "Metodos de la apariencia del control"
    ''' <summary>
    ''' Metodo para cambiar la apariencia cuando el mouse esta presionado sobre el control
    ''' </summary>
    Private Sub PointerPress()
        '
        'INDContent
        '
        Me.INDContent.Appearance.BackColor = System.Drawing.Color.White
        Me.INDContent.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 18.0!)
        Me.INDContent.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.INDContent.Location = New System.Drawing.Point(38, 15)
        Me.INDContent.Name = "INDContent"
        Me.INDContent.Size = New System.Drawing.Size(32, 32)
        Me.INDContent.TabIndex = 2
        '
        'INDTextLabel
        '
        Me.INDTextLabel.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDTextLabel.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDTextLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDTextLabel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDTextLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDTextLabel.Location = New System.Drawing.Point(6, 58)
        Me.INDTextLabel.Name = "INDTextLabel"
        Me.INDTextLabel.Size = New System.Drawing.Size(94, 41)
        Me.INDTextLabel.TabIndex = 3
        '
        'INDBackgroundGlyph
        '
        Me.INDBackgroundGlyph.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 53.333!)
        Me.INDBackgroundGlyph.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDBackgroundGlyph.Location = New System.Drawing.Point(22, -19)
        Me.INDBackgroundGlyph.Name = "INDBackgroundGlyph"
        Me.INDBackgroundGlyph.Size = New System.Drawing.Size(57, 94)
        Me.INDBackgroundGlyph.TabIndex = 1
        Me.INDBackgroundGlyph.Text = ""
        Me.INDBackgroundGlyph.Visible = True
        '
        'INDBackgroundGlyphP
        '
        Me.INDBackgroundGlyphP.EditValue = My.Resources.Resources.icon2
        Me.INDBackgroundGlyphP.Location = New System.Drawing.Point(24, -1)
        Me.INDBackgroundGlyphP.Name = "INDBackgroundGlyphP"
        Me.INDBackgroundGlyphP.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBackgroundGlyphP.Properties.Appearance.Options.UseBackColor = True
        Me.INDBackgroundGlyphP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDBackgroundGlyphP.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDBackgroundGlyphP.Size = New System.Drawing.Size(55, 67)
        Me.INDBackgroundGlyphP.TabIndex = 4
        Me.INDBackgroundGlyphP.Visible = False
    End Sub

    ''' <summary>
    ''' Metodo para cambiar la apariencia del control a el estado normal
    ''' </summary>
    Private Sub Normal()
        'INDBackgroundGlyph
        '
        Me.INDBackgroundGlyph.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 53.333!)
        Me.INDBackgroundGlyph.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDBackgroundGlyph.Location = New System.Drawing.Point(22, -19)
        Me.INDBackgroundGlyph.Name = "INDBackgroundGlyph"
        Me.INDBackgroundGlyph.Size = New System.Drawing.Size(57, 94)
        Me.INDBackgroundGlyph.TabIndex = 1
        Me.INDBackgroundGlyph.Visible = True
        Me.INDBackgroundGlyph.Text = ""
        'INDContent
        '
        Me.INDContent.Appearance.Font = New System.Drawing.Font("Segoe UI Symbol", 18.0!)
        Me.INDContent.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDContent.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDContent.Location = New System.Drawing.Point(38, 15)
        Me.INDContent.Name = "INDContent"
        Me.INDContent.Size = New System.Drawing.Size(32, 32)
        Me.INDContent.TabIndex = 2
        'INDTextLabel
        '
        Me.INDTextLabel.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDTextLabel.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDTextLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDTextLabel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDTextLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDTextLabel.Location = New System.Drawing.Point(6, 58)
        Me.INDTextLabel.Name = "INDTextLabel"
        Me.INDTextLabel.Size = New System.Drawing.Size(94, 41)
        Me.INDTextLabel.TabIndex = 3
        'INDBackgroundGlyphP
        '
        Me.INDBackgroundGlyphP.EditValue = My.Resources.Resources.icon2
        Me.INDBackgroundGlyphP.Location = New System.Drawing.Point(24, -1)
        Me.INDBackgroundGlyphP.Name = "INDBackgroundGlyphP"
        Me.INDBackgroundGlyphP.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBackgroundGlyphP.Properties.Appearance.Options.UseBackColor = True
        Me.INDBackgroundGlyphP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDBackgroundGlyphP.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDBackgroundGlyphP.Size = New System.Drawing.Size(55, 67)
        Me.INDBackgroundGlyphP.TabIndex = 4
        Me.INDBackgroundGlyphP.Visible = False
    End Sub
#End Region

#Region "Eventos de los controles"



#End Region

#Region "Eventos Mouse Leave"
    Private Sub CtrAppBar_MouseLeave(sender As Object, e As EventArgs) Handles MyBase.MouseLeave
        Normal()
    End Sub
#End Region

#Region "Eventos Mouse Down"
    Private Sub CtrAppBar_Mousedown(sender As Object, e As EventArgs) Handles MyBase.MouseDown
        PointerPress()
    End Sub

    Private Sub INDContent_Mousedown(sender As Object, e As EventArgs) Handles INDContent.MouseDown
        PointerPress()
    End Sub

    Private Sub INDBackgroundGlyphP_Mousedown(sender As Object, e As EventArgs) Handles INDBackgroundGlyphP.MouseDown
        PointerPress()
    End Sub

    Private Sub INDTextLabel_MouseDown(sender As Object, e As MouseEventArgs) Handles INDTextLabel.MouseDown
        PointerPress()
    End Sub

    Private Sub INDBackgroundGlyph_Mousedown(sender As Object, e As EventArgs) Handles INDBackgroundGlyph.MouseDown
        PointerPress()
    End Sub
#End Region

#Region "Eventos Mouse Up"
    Private Sub CtrAppBar_Mouseup(sender As Object, e As EventArgs) Handles MyBase.MouseUp
        Normal()
    End Sub

    Private Sub INDTextLabel_Mouseup(sender As Object, e As MouseEventArgs) Handles INDTextLabel.MouseUp
        Normal()
    End Sub

    Private Sub INDBackgroundGlyphP_Mouseup(sender As Object, e As EventArgs) Handles INDBackgroundGlyphP.MouseUp
        Normal()
    End Sub

    Private Sub INDBackgroundGlyph_Mouseup(sender As Object, e As EventArgs) Handles INDBackgroundGlyph.MouseUp
        Normal()
    End Sub
    Private Sub INDContent_Mouseup(sender As Object, e As EventArgs) Handles INDContent.MouseUp
        Normal()
    End Sub

    Private Sub INDTextLabel_up(sender As Object, e As MouseEventArgs) Handles INDTextLabel.MouseUp
        Normal()
    End Sub
#End Region

#Region "Eventos Click"
    Private Sub CtrAppBar_click(sender As Object, e As EventArgs) Handles MyBase.Click
        RaiseEvent Accion()
    End Sub

    Private Sub INDBackgroundGlyphP_click(sender As Object, e As EventArgs) Handles INDBackgroundGlyphP.Click
        RaiseEvent Accion()
    End Sub

    Private Sub INDBackgroundGlyph_click(sender As Object, e As EventArgs) Handles INDBackgroundGlyph.Click
        RaiseEvent Accion()
    End Sub
    Private Sub INDContent_click(sender As Object, e As EventArgs) Handles INDContent.Click
        RaiseEvent Accion()
    End Sub

    Private Sub INDTextLabel_click(sender As Object, e As EventArgs) Handles INDTextLabel.Click
        RaiseEvent Accion()
    End Sub
#End Region


    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        Normal()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
End Class
