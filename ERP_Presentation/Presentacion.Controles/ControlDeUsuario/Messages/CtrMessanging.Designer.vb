<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrMessanging
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrMessanging))
        Me.INDpcStatusUserPhoto = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblUserName = New DevExpress.XtraEditors.LabelControl()
        Me.INDicIconos = New DevExpress.Utils.ImageCollection()
        Me.INDbtnCerrar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDpeNotification = New DevExpress.XtraEditors.PictureEdit()
        Me.INDpeUserPhoto = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.INDpcStatusUserPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDicIconos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpeNotification.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpeUserPhoto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDpcStatusUserPhoto
        '
        Me.INDpcStatusUserPhoto.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpcStatusUserPhoto.Appearance.Options.UseBackColor = True
        Me.INDpcStatusUserPhoto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcStatusUserPhoto.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDpcStatusUserPhoto.Location = New System.Drawing.Point(0, 5)
        Me.INDpcStatusUserPhoto.Name = "INDpcStatusUserPhoto"
        Me.INDpcStatusUserPhoto.Size = New System.Drawing.Size(5, 40)
        Me.INDpcStatusUserPhoto.TabIndex = 4
        '
        'INDlblUserName
        '
        Me.INDlblUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDlblUserName.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblUserName.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.INDlblUserName.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDlblUserName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblUserName.Location = New System.Drawing.Point(57, 0)
        Me.INDlblUserName.Name = "INDlblUserName"
        Me.INDlblUserName.Size = New System.Drawing.Size(114, 42)
        Me.INDlblUserName.TabIndex = 6
        '
        'INDicIconos
        '
        Me.INDicIconos.ImageStream = CType(resources.GetObject("INDicIconos.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicIconos.Images.SetKeyName(0, "16x16 (1).png")
        '
        'INDbtnCerrar
        '
        Me.INDbtnCerrar.AllowFocus = False
        Me.INDbtnCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDbtnCerrar.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbtnCerrar.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.INDbtnCerrar.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDbtnCerrar.Appearance.Options.UseBackColor = True
        Me.INDbtnCerrar.Appearance.Options.UseBorderColor = True
        Me.INDbtnCerrar.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.INDbtnCerrar.ImageIndex = 0
        Me.INDbtnCerrar.ImageList = Me.INDicIconos
        Me.INDbtnCerrar.Location = New System.Drawing.Point(167, 0)
        Me.INDbtnCerrar.LookAndFeel.SkinName = "Metropolis"
        Me.INDbtnCerrar.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDbtnCerrar.Name = "INDbtnCerrar"
        Me.INDbtnCerrar.Size = New System.Drawing.Size(24, 24)
        Me.INDbtnCerrar.TabIndex = 7
        '
        'INDpeNotification
        '
        Me.INDpeNotification.EditValue = Global.Presentation.Controls.My.Resources.Resources.Notification
        Me.INDpeNotification.Location = New System.Drawing.Point(37, -1)
        Me.INDpeNotification.Name = "INDpeNotification"
        Me.INDpeNotification.Properties.AllowFocused = False
        Me.INDpeNotification.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpeNotification.Properties.Appearance.Options.UseBackColor = True
        Me.INDpeNotification.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeNotification.Properties.ReadOnly = True
        Me.INDpeNotification.Properties.ShowMenu = False
        Me.INDpeNotification.Size = New System.Drawing.Size(18, 18)
        Me.INDpeNotification.TabIndex = 8
        Me.INDpeNotification.Visible = False
        '
        'INDpeUserPhoto
        '
        Me.INDpeUserPhoto.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDpeUserPhoto.EditValue = Global.Presentation.Controls.My.Resources.Resources.Usuario
        Me.INDpeUserPhoto.Location = New System.Drawing.Point(5, 5)
        Me.INDpeUserPhoto.Name = "INDpeUserPhoto"
        Me.INDpeUserPhoto.Properties.AllowFocused = False
        Me.INDpeUserPhoto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpeUserPhoto.Properties.Appearance.Options.UseBackColor = True
        Me.INDpeUserPhoto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeUserPhoto.Properties.ReadOnly = True
        Me.INDpeUserPhoto.Properties.ShowMenu = False
        Me.INDpeUserPhoto.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDpeUserPhoto.Size = New System.Drawing.Size(45, 40)
        Me.INDpeUserPhoto.TabIndex = 5
        '
        'CtrMessanging
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpeNotification)
        Me.Controls.Add(Me.INDbtnCerrar)
        Me.Controls.Add(Me.INDlblUserName)
        Me.Controls.Add(Me.INDpeUserPhoto)
        Me.Controls.Add(Me.INDpcStatusUserPhoto)
        Me.Name = "CtrMessanging"
        Me.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Size = New System.Drawing.Size(191, 45)
        CType(Me.INDpcStatusUserPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDicIconos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpeNotification.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpeUserPhoto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpeUserPhoto As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDpcStatusUserPhoto As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblUserName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDicIconos As DevExpress.Utils.ImageCollection
    Friend WithEvents INDbtnCerrar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDpeNotification As DevExpress.XtraEditors.PictureEdit

End Class
