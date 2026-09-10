<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrStatusInfo
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'LblStatus
        '
        Me.LblStatus.Appearance.Font = New System.Drawing.Font("Segoe UI", 26.0!)
        Me.LblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblStatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LblStatus.AutoEllipsis = True
        Me.LblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblStatus.Location = New System.Drawing.Point(0, 0)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(292, 62)
        Me.LblStatus.TabIndex = 0
        Me.LblStatus.Text = "Estado..."
        '
        'CtrStatusInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LblStatus)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximumSize = New System.Drawing.Size(292, 62)
        Me.MinimumSize = New System.Drawing.Size(292, 62)
        Me.Name = "CtrStatusInfo"
        Me.Size = New System.Drawing.Size(292, 62)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LblStatus As DevExpress.XtraEditors.LabelControl
End Class
