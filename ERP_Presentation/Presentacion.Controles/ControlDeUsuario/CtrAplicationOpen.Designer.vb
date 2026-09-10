<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrAplicationOpen
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
        Me.INDlvwAplications = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'INDlvwAplications
        '
        Me.INDlvwAplications.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.INDlvwAplications.BackColor = System.Drawing.Color.White
        Me.INDlvwAplications.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.INDlvwAplications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlvwAplications.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlvwAplications.Location = New System.Drawing.Point(0, 0)
        Me.INDlvwAplications.Name = "INDlvwAplications"
        Me.INDlvwAplications.Size = New System.Drawing.Size(408, 323)
        Me.INDlvwAplications.TabIndex = 0
        Me.INDlvwAplications.UseCompatibleStateImageBehavior = False
        '
        'CtrAplicationOpen
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlvwAplications)
        Me.Name = "CtrAplicationOpen"
        Me.Size = New System.Drawing.Size(408, 323)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlvwAplications As System.Windows.Forms.ListView

End Class
