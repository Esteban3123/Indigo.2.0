<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrYearMonthHorizontal
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlbMonth = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbYear = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbTitle = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciYear = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMonth = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTitle = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.BackColor = System.Drawing.Color.Transparent
        Me.LayoutControl1.Controls.Add(Me.INDlbMonth)
        Me.LayoutControl1.Controls.Add(Me.INDlbYear)
        Me.LayoutControl1.Controls.Add(Me.INDlbTitle)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(240, 60)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlbMonth
        '
        Me.INDlbMonth.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlbMonth.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbMonth.Appearance.Options.UseFont = True
        Me.INDlbMonth.Appearance.Options.UseForeColor = True
        Me.INDlbMonth.Appearance.Options.UseTextOptions = True
        Me.INDlbMonth.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDlbMonth.Location = New System.Drawing.Point(166, 35)
        Me.INDlbMonth.Name = "INDlbMonth"
        Me.INDlbMonth.Size = New System.Drawing.Size(74, 25)
        Me.INDlbMonth.StyleController = Me.LayoutControl1
        Me.INDlbMonth.TabIndex = 4
        Me.INDlbMonth.Text = "Mes"
        '
        'INDlbYear
        '
        Me.INDlbYear.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDlbYear.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbYear.Appearance.Options.UseFont = True
        Me.INDlbYear.Appearance.Options.UseForeColor = True
        Me.INDlbYear.Appearance.Options.UseTextOptions = True
        Me.INDlbYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDlbYear.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbYear.Location = New System.Drawing.Point(43, 35)
        Me.INDlbYear.Name = "INDlbYear"
        Me.INDlbYear.Size = New System.Drawing.Size(61, 25)
        Me.INDlbYear.StyleController = Me.LayoutControl1
        Me.INDlbYear.TabIndex = 1
        Me.INDlbYear.Text = "Año"
        '
        'INDlbTitle
        '
        Me.INDlbTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlbTitle.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbTitle.Appearance.Options.UseFont = True
        Me.INDlbTitle.Appearance.Options.UseForeColor = True
        Me.INDlbTitle.Appearance.Options.UseTextOptions = True
        Me.INDlbTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDlbTitle.Location = New System.Drawing.Point(10, 10)
        Me.INDlbTitle.Name = "INDlbTitle"
        Me.INDlbTitle.Size = New System.Drawing.Size(230, 25)
        Me.INDlbTitle.StyleController = Me.LayoutControl1
        Me.INDlbTitle.TabIndex = 4
        Me.INDlbTitle.Text = "Depreciación y Amortización"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciYear, Me.INDLciMonth, Me.INDLciTitle})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(240, 60)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLciYear
        '
        Me.INDLciYear.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDLciYear.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.INDLciYear.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciYear.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLciYear.Control = Me.INDlbYear
        Me.INDLciYear.CustomizationFormText = "Año"
        Me.INDLciYear.Location = New System.Drawing.Point(0, 35)
        Me.INDLciYear.MinSize = New System.Drawing.Size(36, 25)
        Me.INDLciYear.Name = "INDLciYear"
        Me.INDLciYear.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.INDLciYear.Size = New System.Drawing.Size(104, 25)
        Me.INDLciYear.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciYear.Text = "Año: "
        Me.INDLciYear.TextSize = New System.Drawing.Size(30, 17)
        '
        'INDLciMonth
        '
        Me.INDLciMonth.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDLciMonth.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.INDLciMonth.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciMonth.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLciMonth.Control = Me.INDlbMonth
        Me.INDLciMonth.Location = New System.Drawing.Point(104, 35)
        Me.INDLciMonth.MinSize = New System.Drawing.Size(36, 25)
        Me.INDLciMonth.Name = "INDLciMonth"
        Me.INDLciMonth.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.INDLciMonth.Size = New System.Drawing.Size(136, 25)
        Me.INDLciMonth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMonth.Text = "Mes: "
        Me.INDLciMonth.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciMonth.TextSize = New System.Drawing.Size(35, 17)
        Me.INDLciMonth.TextToControlDistance = 17
        '
        'INDLciTitle
        '
        Me.INDLciTitle.Control = Me.INDlbTitle
        Me.INDLciTitle.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTitle.CustomizationFormText = "Mes: "
        Me.INDLciTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTitle.MinSize = New System.Drawing.Size(36, 25)
        Me.INDLciTitle.Name = "INDLciTitle"
        Me.INDLciTitle.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 10, 0)
        Me.INDLciTitle.Size = New System.Drawing.Size(240, 35)
        Me.INDLciTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTitle.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTitle.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciTitle.TextToControlDistance = 0
        Me.INDLciTitle.TextVisible = False
        '
        'CtrYearMonthHorizontal
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximumSize = New System.Drawing.Size(240, 60)
        Me.MinimumSize = New System.Drawing.Size(240, 60)
        Me.Name = "CtrYearMonthHorizontal"
        Me.Size = New System.Drawing.Size(240, 60)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTitle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlbYear As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciYear As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlbMonth As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciMonth As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlbTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciTitle As DevExpress.XtraLayout.LayoutControlItem
End Class
