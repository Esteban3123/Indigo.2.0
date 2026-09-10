<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrBiometricoInicio
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
        Me.LogList = New System.Windows.Forms.ListBox()
        Me.INDpeImagenBiometrico = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.INDpeImagenBiometrico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LogList
        '
        Me.LogList.FormattingEnabled = True
        Me.LogList.Location = New System.Drawing.Point(0, 0)
        Me.LogList.Name = "LogList"
        Me.LogList.Size = New System.Drawing.Size(51, 4)
        Me.LogList.TabIndex = 1
        Me.LogList.Visible = False
        '
        'INDpeImagenBiometrico
        '
        Me.INDpeImagenBiometrico.Dock = System.Windows.Forms.DockStyle.Fill
        'Me.INDpeImagenBiometrico.EditValue = Global.Presentation.Controls.My.Resources.Resources.biometrico11
        Me.INDpeImagenBiometrico.Location = New System.Drawing.Point(0, 0)
        Me.INDpeImagenBiometrico.Name = "INDpeImagenBiometrico"
        Me.INDpeImagenBiometrico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpeImagenBiometrico.Properties.Appearance.Options.UseBackColor = True
        Me.INDpeImagenBiometrico.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpeImagenBiometrico.Properties.ReadOnly = True
        Me.INDpeImagenBiometrico.Properties.ShowMenu = False
        Me.INDpeImagenBiometrico.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.INDpeImagenBiometrico.Size = New System.Drawing.Size(66, 86)
        Me.INDpeImagenBiometrico.TabIndex = 0
        '
        'CtrBiometricoInicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LogList)
        Me.Controls.Add(Me.INDpeImagenBiometrico)
        Me.Name = "CtrBiometricoInicio"
        Me.Size = New System.Drawing.Size(66, 86)
        CType(Me.INDpeImagenBiometrico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LogList As System.Windows.Forms.ListBox
    Friend WithEvents INDpeImagenBiometrico As DevExpress.XtraEditors.PictureEdit
End Class
