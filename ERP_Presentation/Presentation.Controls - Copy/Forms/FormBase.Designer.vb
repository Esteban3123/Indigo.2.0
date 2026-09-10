Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBase
    Inherits XtraForm

    'Form overrides dispose to clean up the component list.
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
        Me.ToolBars = New DevExpress.XtraEditors.PanelControl()
        Me.INDmpbLoad = New DevExpress.XtraEditors.ProgressBarControl()
        Me.INDPanelControlBase = New DevExpress.XtraEditors.PanelControl()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmpbLoad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBars
        '
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.Top
        Me.ToolBars.Location = New System.Drawing.Point(0, 0)
        Me.ToolBars.Name = "ToolBars"
        Me.ToolBars.Size = New System.Drawing.Size(964, 100)
        Me.ToolBars.TabIndex = 0
        '
        'INDmpbLoad
        '
        Me.INDmpbLoad.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDmpbLoad.Location = New System.Drawing.Point(0, 100)
        Me.INDmpbLoad.Name = "INDmpbLoad"
        Me.INDmpbLoad.Size = New System.Drawing.Size(964, 18)
        Me.INDmpbLoad.TabIndex = 1
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Name = "INDPanelControlBase"
        Me.INDPanelControlBase.Size = New System.Drawing.Size(964, 535)
        Me.INDPanelControlBase.TabIndex = 2
        '
        'FormBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 653)
        Me.Controls.Add(Me.INDPanelControlBase)
        Me.Controls.Add(Me.INDmpbLoad)
        Me.Controls.Add(Me.ToolBars)
        Me.Name = "FormBase"
        Me.Text = "FormBase"
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmpbLoad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ToolBars As DevExpress.XtraEditors.PanelControl
    Public WithEvents INDmpbLoad As DevExpress.XtraEditors.ProgressBarControl
    Public WithEvents INDPanelControlBase As DevExpress.XtraEditors.PanelControl
End Class
