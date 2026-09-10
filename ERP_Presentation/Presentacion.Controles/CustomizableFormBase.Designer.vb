<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomizableFormBase
    Inherits FormBase

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
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlycgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(651, 296)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(651, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(651, 94)
        '
        'INDlycRoot
        '
        Me.INDlycRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlycRoot.Name = "INDlycRoot"
        Me.INDlycRoot.Root = Me.INDlycgRoot
        Me.INDlycRoot.Size = New System.Drawing.Size(647, 287)
        Me.INDlycRoot.TabIndex = 0
        Me.INDlycRoot.Text = "LayoutControl1"
        '
        'INDlycgRoot
        '
        Me.INDlycgRoot.AllowHide = False
        Me.INDlycgRoot.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlycgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycgRoot.GroupBordersVisible = False
        Me.INDlycgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgRoot.Name = "INDlycgRoot"
        Me.INDlycgRoot.Size = New System.Drawing.Size(647, 287)
        Me.INDlycgRoot.Text = "Root"
        Me.INDlycgRoot.TextVisible = False
        '
        'CustomizableFormBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(651, 413)
        Me.Name = "CustomizableFormBase"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "CustomizableFormBase"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents INDlycgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Public WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControl
End Class
