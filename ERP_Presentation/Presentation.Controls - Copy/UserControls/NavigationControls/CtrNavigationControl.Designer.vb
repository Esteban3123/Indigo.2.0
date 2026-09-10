<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrNavigationControl
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
        Me.INDpcNavigation = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDpcNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDpcNavigation
        '
        Me.INDpcNavigation.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(107, Byte), Integer))
        Me.INDpcNavigation.Appearance.Options.UseBackColor = True
        Me.INDpcNavigation.Appearance.Options.UseTextOptions = True
        Me.INDpcNavigation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDpcNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcNavigation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDpcNavigation.FireScrollEventOnMouseWheel = True
        Me.INDpcNavigation.Location = New System.Drawing.Point(0, 0)
        Me.INDpcNavigation.Name = "INDpcNavigation"
        Me.INDpcNavigation.Size = New System.Drawing.Size(79, 738)
        Me.INDpcNavigation.TabIndex = 0
        '
        'CtrNavigationControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpcNavigation)
        Me.Name = "CtrNavigationControl"
        Me.Size = New System.Drawing.Size(79, 738)
        CType(Me.INDpcNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDpcNavigation As DevExpress.XtraEditors.PanelControl

End Class
