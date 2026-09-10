<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCostDistributionFixedAsset
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
        Me.LblName = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LblDeprecationValue = New DevExpress.XtraEditors.LabelControl()
        Me.LblMonth = New DevExpress.XtraEditors.LabelControl()
        Me.LblYear = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblName
        '
        Me.LblName.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 13.0!, System.Drawing.FontStyle.Bold)
        Me.LblName.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblName.Location = New System.Drawing.Point(2, 0)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(138, 25)
        Me.LblName.StyleController = Me.LayoutControl1
        Me.LblName.TabIndex = 0
        Me.LblName.Text = "V. Depreciación"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.LblName)
        Me.LayoutControl1.Controls.Add(Me.LblDeprecationValue)
        Me.LayoutControl1.Controls.Add(Me.LblMonth)
        Me.LayoutControl1.Controls.Add(Me.LblYear)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 4
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LblDeprecationValue
        '
        Me.LblDeprecationValue.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.LblDeprecationValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblDeprecationValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LblDeprecationValue.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LblDeprecationValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblDeprecationValue.Location = New System.Drawing.Point(140, 0)
        Me.LblDeprecationValue.Name = "LblDeprecationValue"
        Me.LblDeprecationValue.Size = New System.Drawing.Size(147, 25)
        Me.LblDeprecationValue.StyleController = Me.LayoutControl1
        Me.LblDeprecationValue.TabIndex = 1
        Me.LblDeprecationValue.Text = "$0"
        '
        'LblMonth
        '
        Me.LblMonth.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.5!)
        Me.LblMonth.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblMonth.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblMonth.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblMonth.Location = New System.Drawing.Point(0, 25)
        Me.LblMonth.Name = "LblMonth"
        Me.LblMonth.Size = New System.Drawing.Size(146, 37)
        Me.LblMonth.StyleController = Me.LayoutControl1
        Me.LblMonth.TabIndex = 2
        Me.LblMonth.Text = "Enero"
        '
        'LblYear
        '
        Me.LblYear.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.5!)
        Me.LblYear.Appearance.ForeColor = System.Drawing.Color.White
        Me.LblYear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblYear.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LblYear.Location = New System.Drawing.Point(146, 25)
        Me.LblYear.Name = "LblYear"
        Me.LblYear.Size = New System.Drawing.Size(146, 37)
        Me.LblYear.StyleController = Me.LayoutControl1
        Me.LblYear.TabIndex = 3
        Me.LblYear.Text = "2016"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem4, Me.LayoutControlItem3, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.LblYear
        Me.LayoutControlItem1.Location = New System.Drawing.Point(146, 25)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(146, 37)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(146, 37)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(146, 37)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.LblName
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(140, 25)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(140, 25)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem4.Size = New System.Drawing.Size(140, 25)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LblDeprecationValue
        Me.LayoutControlItem3.Location = New System.Drawing.Point(140, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(152, 25)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(152, 25)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 5, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(152, 25)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.LblMonth
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 25)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(146, 37)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(146, 37)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(146, 37)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'CtrCostDistributionFixedAsset
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 62)
        Me.MinimumSize = New System.Drawing.Size(292, 62)
        Me.Name = "CtrCostDistributionFixedAsset"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblDeprecationValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblMonth As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblYear As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Public WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
