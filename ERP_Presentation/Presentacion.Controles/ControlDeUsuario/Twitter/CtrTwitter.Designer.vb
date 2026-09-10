<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrTwitter
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
        Me.INDxscContainerTwitters = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.SuspendLayout()
        '
        'INDxscContainerTwitters
        '
        Me.INDxscContainerTwitters.AllowTouchScroll = True
        Me.INDxscContainerTwitters.AlwaysScrollActiveControlIntoView = False
        Me.INDxscContainerTwitters.AutoScroll = False
        Me.INDxscContainerTwitters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDxscContainerTwitters.FireScrollEventOnMouseWheel = True
        Me.INDxscContainerTwitters.Location = New System.Drawing.Point(0, 0)
        Me.INDxscContainerTwitters.Name = "INDxscContainerTwitters"
        Me.INDxscContainerTwitters.Size = New System.Drawing.Size(920, 72)
        Me.INDxscContainerTwitters.TabIndex = 0
        '
        'CtrTwitter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDxscContainerTwitters)
        Me.Name = "CtrTwitter"
        Me.Size = New System.Drawing.Size(920, 72)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDxscContainerTwitters As DevExpress.XtraEditors.XtraScrollableControl

End Class
