<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrRss
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
        Me.INDxscConainerRssItems = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.SuspendLayout()
        '
        'INDxscConainerRssItems
        '
        Me.INDxscConainerRssItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDxscConainerRssItems.Location = New System.Drawing.Point(0, 0)
        Me.INDxscConainerRssItems.Name = "INDxscConainerRssItems"
        Me.INDxscConainerRssItems.Size = New System.Drawing.Size(400, 762)
        Me.INDxscConainerRssItems.TabIndex = 0
        '
        'CtrRss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDxscConainerRssItems)
        Me.Name = "CtrRss"
        Me.Size = New System.Drawing.Size(400, 762)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDxscConainerRssItems As DevExpress.XtraEditors.XtraScrollableControl

End Class
