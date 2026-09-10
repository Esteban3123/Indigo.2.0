<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrueba
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
        Me.CtrBiometricoGuardar1 = New Presentation.Controls.CTRBiometricoGuardar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CtrBiometricoGuardar1
        '
        Me.CtrBiometricoGuardar1.Location = New System.Drawing.Point(0, 0)
        Me.CtrBiometricoGuardar1.Mensaje = Nothing
        Me.CtrBiometricoGuardar1.Name = "CtrBiometricoGuardar1"
        Me.CtrBiometricoGuardar1.Size = New System.Drawing.Size(170, 134)
        Me.CtrBiometricoGuardar1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(207, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FrmPrueba
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(294, 143)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CtrBiometricoGuardar1)
        Me.Name = "FrmPrueba"
        Me.Text = "FrmPrueba"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrBiometricoGuardar1 As Presentation.Controls.CTRBiometricoGuardar
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
