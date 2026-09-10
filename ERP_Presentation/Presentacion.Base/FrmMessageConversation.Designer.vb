<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMessageConversation
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
        Me.INDehWpfConversation = New System.Windows.Forms.Integration.ElementHost()
        Me.WpfMessageConversation1 = New Presentation.Base.WPFMessageConversation()
        Me.Timer1 = New System.Windows.Forms.Timer()
        Me.SuspendLayout()
        '
        'INDehWpfConversation
        '
        Me.INDehWpfConversation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDehWpfConversation.Location = New System.Drawing.Point(1, 1)
        Me.INDehWpfConversation.Name = "INDehWpfConversation"
        Me.INDehWpfConversation.Size = New System.Drawing.Size(172, 206)
        Me.INDehWpfConversation.TabIndex = 0
        Me.INDehWpfConversation.Text = "ElementHost1"
        Me.INDehWpfConversation.Child = Me.WpfMessageConversation1
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 20000
        '
        'FrmMessageConversation
        '
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(174, 208)
        Me.Controls.Add(Me.INDehWpfConversation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmMessageConversation"
        Me.Opacity = 0.95R
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FrmMessageConversation"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDehWpfConversation As System.Windows.Forms.Integration.ElementHost
    Friend WpfMessageConversation1 As Presentation.Base.WPFMessageConversation
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
