<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMensajeIndigo
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMensajeIndigo))
        Me.Boton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.Boton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.INDlblMensaje = New DevExpress.XtraEditors.LabelControl()
        Me.INDpeIcono = New DevExpress.XtraEditors.PictureEdit()
        Me.INDicIconosMensaje = New DevExpress.Utils.ImageCollection(Me.components)
        CType(Me.INDpeIcono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDicIconosMensaje, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Boton2
        '
        Me.Boton2.AllowFocus = False
        Me.Boton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Boton2.Appearance.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.Boton2.Appearance.Options.UseFont = True
        Me.Boton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Boton2.Location = New System.Drawing.Point(83, 86)
        Me.Boton2.Name = "Boton2"
        Me.Boton2.Size = New System.Drawing.Size(80, 24)
        Me.Boton2.TabIndex = 0
        Me.Boton2.Visible = False
        '
        'Boton1
        '
        Me.Boton1.AllowFocus = False
        Me.Boton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Boton1.Appearance.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.Boton1.Appearance.Options.UseFont = True
        Me.Boton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Boton1.Location = New System.Drawing.Point(173, 86)
        Me.Boton1.Name = "Boton1"
        Me.Boton1.Size = New System.Drawing.Size(80, 24)
        Me.Boton1.TabIndex = 1
        Me.Boton1.Visible = False
        '
        'INDlblMensaje
        '
        Me.INDlblMensaje.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDlblMensaje.Appearance.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.INDlblMensaje.Appearance.Options.UseFont = True
        Me.INDlblMensaje.Appearance.Options.UseTextOptions = True
        Me.INDlblMensaje.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblMensaje.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblMensaje.AutoEllipsis = True
        Me.INDlblMensaje.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        Me.INDlblMensaje.Location = New System.Drawing.Point(80, 12)
        Me.INDlblMensaje.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.INDlblMensaje.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDlblMensaje.MaximumSize = New System.Drawing.Size(310, 350)
        Me.INDlblMensaje.MinimumSize = New System.Drawing.Size(310, 0)
        Me.INDlblMensaje.Name = "INDlblMensaje"
        Me.INDlblMensaje.Size = New System.Drawing.Size(310, 0)
        Me.INDlblMensaje.TabIndex = 2
        '
        'INDpeIcono
        '
        Me.INDpeIcono.Location = New System.Drawing.Point(12, 12)
        Me.INDpeIcono.Name = "INDpeIcono"
        Me.INDpeIcono.Properties.AllowFocused = False
        Me.INDpeIcono.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpeIcono.Properties.Appearance.Options.UseBackColor = True
        Me.INDpeIcono.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeIcono.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.INDpeIcono.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDpeIcono.Properties.ReadOnly = True
        Me.INDpeIcono.Properties.ShowMenu = False
        Me.INDpeIcono.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDpeIcono.Size = New System.Drawing.Size(60, 60)
        Me.INDpeIcono.TabIndex = 3
        '
        'INDicIconosMensaje
        '
        Me.INDicIconosMensaje.ImageSize = New System.Drawing.Size(128, 128)
        Me.INDicIconosMensaje.ImageStream = CType(resources.GetObject("INDicIconosMensaje.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicIconosMensaje.Images.SetKeyName(0, "info.png")
        Me.INDicIconosMensaje.Images.SetKeyName(1, "cerrar.png")
        Me.INDicIconosMensaje.Images.SetKeyName(2, "advertencia.png")
        Me.INDicIconosMensaje.Images.SetKeyName(3, "pregunta.png")
        '
        'FrmMensajeIndigo
        '
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(260, 122)
        Me.Controls.Add(Me.INDpeIcono)
        Me.Controls.Add(Me.INDlblMensaje)
        Me.Controls.Add(Me.Boton1)
        Me.Controls.Add(Me.Boton2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximumSize = New System.Drawing.Size(400, 400)
        Me.MinimumSize = New System.Drawing.Size(260, 122)
        Me.Name = "FrmMensajeIndigo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IndigoMensajes"
        Me.TopMost = True
        CType(Me.INDpeIcono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDicIconosMensaje, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Boton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Boton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlblMensaje As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpeIcono As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDicIconosMensaje As DevExpress.Utils.ImageCollection
End Class