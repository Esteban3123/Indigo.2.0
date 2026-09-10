<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrTwitterItem
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
        Me.INDlblUserName = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.INDpeUserImage = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.INDpeUserImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlblUserName
        '
        Me.INDlblUserName.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblUserName.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDlblUserName.Location = New System.Drawing.Point(60, 0)
        Me.INDlblUserName.Name = "INDlblUserName"
        Me.INDlblUserName.Padding = New System.Windows.Forms.Padding(3, 2, 3, 1)
        Me.INDlblUserName.Size = New System.Drawing.Size(79, 24)
        Me.INDlblUserName.TabIndex = 1
        Me.INDlblUserName.Text = "User Name"
        '
        'INDlblMessage
        '
        Me.INDlblMessage.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.INDlblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDlblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlblMessage.Location = New System.Drawing.Point(60, 24)
        Me.INDlblMessage.Name = "INDlblMessage"
        Me.INDlblMessage.Padding = New System.Windows.Forms.Padding(3, 0, 3, 2)
        Me.INDlblMessage.Size = New System.Drawing.Size(406, 36)
        Me.INDlblMessage.TabIndex = 2
        Me.INDlblMessage.Text = "Tweet"
        '
        'INDpeUserImage
        '
        Me.INDpeUserImage.Dock = System.Windows.Forms.DockStyle.Left
        'Me.INDpeUserImage.EditValue = Global.Presentation.Controls.My.Resources.Resources.twitter_bird_icon
        Me.INDpeUserImage.Location = New System.Drawing.Point(0, 0)
        Me.INDpeUserImage.Name = "INDpeUserImage"
        Me.INDpeUserImage.Properties.AllowFocused = False
        Me.INDpeUserImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeUserImage.Properties.ReadOnly = True
        Me.INDpeUserImage.Properties.ShowMenu = False
        Me.INDpeUserImage.Size = New System.Drawing.Size(60, 60)
        Me.INDpeUserImage.TabIndex = 0
        '
        'CtrTwitterItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlblMessage)
        Me.Controls.Add(Me.INDlblUserName)
        Me.Controls.Add(Me.INDpeUserImage)
        Me.Name = "CtrTwitterItem"
        Me.Padding = New System.Windows.Forms.Padding(0, 0, 20, 0)
        Me.Size = New System.Drawing.Size(486, 60)
        CType(Me.INDpeUserImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDpeUserImage As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents INDlblUserName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblMessage As DevExpress.XtraEditors.LabelControl

End Class
