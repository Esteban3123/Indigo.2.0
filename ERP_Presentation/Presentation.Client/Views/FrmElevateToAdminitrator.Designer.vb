<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmElevateToAdminitrator
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmElevateToAdminitrator))
        Me.BtnElevateTo = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LblMessage = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'BtnElevateTo
        '
        Me.BtnElevateTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnElevateTo.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnElevateTo.Appearance.Options.UseFont = True
        Me.BtnElevateTo.Location = New System.Drawing.Point(12, 193)
        Me.BtnElevateTo.Name = "BtnElevateTo"
        Me.BtnElevateTo.Size = New System.Drawing.Size(173, 32)
        Me.BtnElevateTo.TabIndex = 0
        Me.BtnElevateTo.Text = "Elevar Privilegios!"
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Appearance.Options.UseFont = True
        Me.BtnCancel.Location = New System.Drawing.Point(191, 193)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(173, 32)
        Me.BtnCancel.TabIndex = 1
        Me.BtnCancel.Text = "Cancelar"
        '
        'LblMessage
        '
        Me.LblMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblMessage.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblMessage.Location = New System.Drawing.Point(12, 12)
        Me.LblMessage.Name = "LblMessage"
        Me.LblMessage.Size = New System.Drawing.Size(352, 175)
        Me.LblMessage.TabIndex = 2
        Me.LblMessage.Text = resources.GetString("LblMessage.Text")
        '
        'FrmElevateToAdminitrator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(378, 237)
        Me.Controls.Add(Me.LblMessage)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnElevateTo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmElevateToAdminitrator"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Elevación de privilegios"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnElevateTo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblMessage As DevExpress.XtraEditors.LabelControl
End Class
