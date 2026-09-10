<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrDistributionLabor
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.lblHorasLaboradas = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalItemInferior = New DevExpress.XtraEditors.LabelControl()
        Me.lblTotalItemSuperior = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LciItemSuperior = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LciInferior = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LciItemSuperior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LciInferior, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.BackColor = System.Drawing.Color.Transparent
        Me.LayoutControl1.Controls.Add(Me.lblHorasLaboradas)
        Me.LayoutControl1.Controls.Add(Me.lblTotalItemInferior)
        Me.LayoutControl1.Controls.Add(Me.lblTotalItemSuperior)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'lblHorasLaboradas
        '
        Me.lblHorasLaboradas.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHorasLaboradas.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblHorasLaboradas.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblHorasLaboradas.Location = New System.Drawing.Point(125, 44)
        Me.lblHorasLaboradas.Name = "lblHorasLaboradas"
        Me.lblHorasLaboradas.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.lblHorasLaboradas.Size = New System.Drawing.Size(167, 16)
        Me.lblHorasLaboradas.StyleController = Me.LayoutControl1
        Me.lblHorasLaboradas.TabIndex = 6
        Me.lblHorasLaboradas.Text = "0"
        '
        'lblTotalItemInferior
        '
        Me.lblTotalItemInferior.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalItemInferior.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblTotalItemInferior.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblTotalItemInferior.Location = New System.Drawing.Point(135, 28)
        Me.lblTotalItemInferior.Name = "lblTotalItemInferior"
        Me.lblTotalItemInferior.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.lblTotalItemInferior.Size = New System.Drawing.Size(157, 16)
        Me.lblTotalItemInferior.StyleController = Me.LayoutControl1
        Me.lblTotalItemInferior.TabIndex = 5
        Me.lblTotalItemInferior.Text = "$0"
        '
        'lblTotalItemSuperior
        '
        Me.lblTotalItemSuperior.Appearance.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.lblTotalItemSuperior.Appearance.ForeColor = System.Drawing.Color.White
        Me.lblTotalItemSuperior.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lblTotalItemSuperior.Location = New System.Drawing.Point(150, 2)
        Me.lblTotalItemSuperior.Name = "lblTotalItemSuperior"
        Me.lblTotalItemSuperior.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.lblTotalItemSuperior.Size = New System.Drawing.Size(140, 24)
        Me.lblTotalItemSuperior.StyleController = Me.LayoutControl1
        Me.lblTotalItemSuperior.TabIndex = 4
        Me.lblTotalItemSuperior.Text = "$0"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LciItemSuperior, Me.LciInferior, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LciItemSuperior
        '
        Me.LciItemSuperior.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.LciItemSuperior.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.LciItemSuperior.AppearanceItemCaption.Options.UseFont = True
        Me.LciItemSuperior.AppearanceItemCaption.Options.UseForeColor = True
        Me.LciItemSuperior.Control = Me.lblTotalItemSuperior
        Me.LciItemSuperior.CustomizationFormText = "Total Devengado"
        Me.LciItemSuperior.Location = New System.Drawing.Point(0, 0)
        Me.LciItemSuperior.MaxSize = New System.Drawing.Size(0, 28)
        Me.LciItemSuperior.MinSize = New System.Drawing.Size(125, 28)
        Me.LciItemSuperior.Name = "LciItemSuperior"
        Me.LciItemSuperior.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2)
        Me.LciItemSuperior.Size = New System.Drawing.Size(292, 28)
        Me.LciItemSuperior.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciItemSuperior.Text = "Total Devengado"
        Me.LciItemSuperior.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LciItemSuperior.TextSize = New System.Drawing.Size(145, 20)
        Me.LciItemSuperior.TextToControlDistance = 0
        '
        'LciInferior
        '
        Me.LciInferior.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LciInferior.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.LciInferior.AppearanceItemCaption.Options.UseFont = True
        Me.LciInferior.AppearanceItemCaption.Options.UseForeColor = True
        Me.LciInferior.Control = Me.lblTotalItemInferior
        Me.LciInferior.CustomizationFormText = "Devengado + Patronales"
        Me.LciInferior.Location = New System.Drawing.Point(0, 28)
        Me.LciInferior.MaxSize = New System.Drawing.Size(0, 16)
        Me.LciInferior.MinSize = New System.Drawing.Size(250, 16)
        Me.LciInferior.Name = "LciInferior"
        Me.LciInferior.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0)
        Me.LciInferior.Size = New System.Drawing.Size(292, 16)
        Me.LciInferior.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciInferior.Text = "Devengado + Patronales"
        Me.LciInferior.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LciInferior.TextSize = New System.Drawing.Size(130, 10)
        Me.LciInferior.TextToControlDistance = 0
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem3.Control = Me.lblHorasLaboradas
        Me.LayoutControlItem3.CustomizationFormText = "Horas Laboradas"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 44)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 16)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(250, 16)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(292, 18)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Horas Laboradas"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(120, 10)
        Me.LayoutControlItem3.TextToControlDistance = 0
        '
        'CtrDistributionLabor
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 62)
        Me.MinimumSize = New System.Drawing.Size(292, 62)
        Me.Name = "CtrDistributionLabor"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LciItemSuperior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LciInferior, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lblHorasLaboradas As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalItemInferior As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTotalItemSuperior As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LciItemSuperior As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LciInferior As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem

End Class
