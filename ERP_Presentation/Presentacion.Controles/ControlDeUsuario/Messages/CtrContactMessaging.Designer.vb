<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrContactMessaging
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
        Me.INDpcStatusUserPhoto = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblUserName = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblUserPosition = New DevExpress.XtraEditors.LabelControl()
        Me.INDpeUserPhoto = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.INDpcStatusUserPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.INDpcStatusUserPhoto.TabIndex = 2
        '
        'INDlblUserName
        '
        Me.INDlblUserName.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 14.0!)
        Me.INDlblUserName.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDlblUserName.Location = New System.Drawing.Point(55, 3)
        Me.INDlblUserName.Name = "INDlblUserName"
        Me.INDlblUserName.Size = New System.Drawing.Size(155, 25)
        Me.INDlblUserName.TabIndex = 4
        Me.INDlblUserName.Text = "Nombre de usuario"
        '
        'INDlblUserPosition
        '
        Me.INDlblUserPosition.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDlblUserPosition.Appearance.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlblUserPosition.Location = New System.Drawing.Point(55, 29)
        Me.INDlblUserPosition.Name = "INDlblUserPosition"
        Me.INDlblUserPosition.Size = New System.Drawing.Size(28, 13)
        Me.INDlblUserPosition.TabIndex = 5
        Me.INDlblUserPosition.Text = "Cargo"
        '
        'INDpeUserPhoto
        '
        Me.INDpeUserPhoto.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDpeUserPhoto.EditValue = Global.Presentation.Controls.My.Resources.Resources.Usuario
        Me.INDpeUserPhoto.Location = New System.Drawing.Point(5, 5)
        Me.INDpeUserPhoto.Name = "INDpeUserPhoto"
        Me.INDpeUserPhoto.Properties.AllowFocused = False
        Me.INDpeUserPhoto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeUserPhoto.Properties.ReadOnly = True
        Me.INDpeUserPhoto.Properties.ShowMenu = False
        Me.INDpeUserPhoto.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.INDpeUserPhoto.Size = New System.Drawing.Size(40, 40)
        Me.INDpeUserPhoto.TabIndex = 3
        '
        'CtrContactMessaging
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlblUserPosition)
        Me.Controls.Add(Me.INDlblUserName)
        Me.Controls.Add(Me.INDpeUserPhoto)
        Me.Controls.Add(Me.INDpcStatusUserPhoto)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Name = "CtrContactMessaging"
        Me.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Size = New System.Drawing.Size(351, 45)
        CType(Me.INDpcStatusUserPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpeUserPhoto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDpcStatusUserPhoto As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpeUserPhoto As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDlblUserName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblUserPosition As DevExpress.XtraEditors.LabelControl

End Class
