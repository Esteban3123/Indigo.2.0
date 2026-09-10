<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProgressPanel
    Inherits Presentation.Controls.FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.INDPbcProgress = New DevExpress.XtraEditors.ProgressBarControl()
        Me.INDLcGeneral = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLbcDescription = New DevExpress.XtraEditors.LabelControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciProgress = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPbcProgress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcGeneral.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcGeneral)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 5)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(582, 135)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolBars.Size = New System.Drawing.Size(582, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(582, 130)
        Me.BarraBotones.Visible = False
        '
        'INDPbcProgress
        '
        Me.INDPbcProgress.Location = New System.Drawing.Point(12, 48)
        Me.INDPbcProgress.MinimumSize = New System.Drawing.Size(0, 25)
        Me.INDPbcProgress.Name = "INDPbcProgress"
        Me.INDPbcProgress.Properties.Step = 1
        Me.INDPbcProgress.Size = New System.Drawing.Size(554, 25)
        Me.INDPbcProgress.StyleController = Me.INDLcGeneral
        Me.INDPbcProgress.TabIndex = 0
        '
        'INDLcGeneral
        '
        Me.INDLcGeneral.Controls.Add(Me.INDLbcDescription)
        Me.INDLcGeneral.Controls.Add(Me.INDPbcProgress)
        Me.INDLcGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcGeneral.Location = New System.Drawing.Point(2, 7)
        Me.INDLcGeneral.Name = "INDLcGeneral"
        Me.INDLcGeneral.Root = Me.Root
        Me.INDLcGeneral.Size = New System.Drawing.Size(578, 126)
        Me.INDLcGeneral.TabIndex = 1
        Me.INDLcGeneral.Text = "LayoutControl1"
        '
        'INDLbcDescription
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLbcDescription, True)
        Me.INDLbcDescription.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLbcDescription.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLbcDescription, False)
        Me.INDLbcDescription.Location = New System.Drawing.Point(12, 90)
        Me.INDLbcDescription.Name = "INDLbcDescription"
        Me.INDLbcDescription.Size = New System.Drawing.Size(554, 24)
        Me.INDLbcDescription.StyleController = Me.INDLcGeneral
        Me.INDLbcDescription.TabIndex = 4
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciProgress, Me.LayoutControlItem2})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(578, 126)
        Me.Root.TextVisible = False
        '
        'INDLciProgress
        '
        Me.INDLciProgress.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold)
        Me.INDLciProgress.AppearanceItemCaption.FontStyleDelta = System.Drawing.FontStyle.Bold
        Me.INDLciProgress.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciProgress.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLciProgress.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDLciProgress.Control = Me.INDPbcProgress
        Me.INDLciProgress.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProgress.MinSize = New System.Drawing.Size(1, 29)
        Me.INDLciProgress.Name = "INDLciProgress"
        Me.INDLciProgress.Size = New System.Drawing.Size(558, 78)
        Me.INDLciProgress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProgress.Text = "Progreso"
        Me.INDLciProgress.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProgress.TextSize = New System.Drawing.Size(68, 21)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDLbcDescription
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 78)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(95, 25)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(558, 28)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'FrmProgressPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(582, 140)
        Me.IconOptions.Image = Global.Presentation.Controls.My.Resources.Resources.Banner_angulo
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProgressPanel"
        Me.Opacity = 1.0R
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = ""
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPbcProgress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcGeneral.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcGeneral As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDPbcProgress As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDLciProgress As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLbcDescription As DevExpress.XtraEditors.LabelControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
