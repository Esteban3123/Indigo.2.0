<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrConfirmado
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrConfirmado))
        Me.INDlblText = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'INDlblText
        '
        resources.ApplyResources(Me.INDlblText, "INDlblText")
        Me.INDlblText.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.INDlblText.Appearance.ForeColor = CType(resources.GetObject("LabelControl1.Appearance.ForeColor"), System.Drawing.Color)
        Me.INDlblText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblText.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblText.Name = "INDlblText"
        '
        'CtrConfirmado
        '
        Me.Appearance.BackColor = CType(resources.GetObject("CtrConfirmado.Appearance.BackColor"), System.Drawing.Color)
        Me.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlblText)
        Me.Name = "CtrConfirmado"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlblText As DevExpress.XtraEditors.LabelControl

End Class
