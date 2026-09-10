<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrCostProduct
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlblTotal = New DevExpress.XtraEditors.LabelControl()
        Me.INDLbcTaxesName = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblTaxValue = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTaxes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTaxes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.Location = New System.Drawing.Point(2, 0)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(98, 30)
        Me.LabelControl2.StyleController = Me.LayoutControl1
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Total"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.BackColor = System.Drawing.Color.Transparent
        Me.LayoutControl1.Controls.Add(Me.LabelControl2)
        Me.LayoutControl1.Controls.Add(Me.INDlblTotal)
        Me.LayoutControl1.Controls.Add(Me.INDLbcTaxesName)
        Me.LayoutControl1.Controls.Add(Me.INDlblTaxValue)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(306, 65)
        Me.LayoutControl1.TabIndex = 3
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlblTotal
        '
        Me.INDlblTotal.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDlblTotal.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblTotal.Appearance.Options.UseFont = True
        Me.INDlblTotal.Appearance.Options.UseForeColor = True
        Me.INDlblTotal.Appearance.Options.UseTextOptions = True
        Me.INDlblTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblTotal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblTotal.Location = New System.Drawing.Point(100, 0)
        Me.INDlblTotal.Name = "INDlblTotal"
        Me.INDlblTotal.Size = New System.Drawing.Size(170, 30)
        Me.INDlblTotal.StyleController = Me.LayoutControl1
        Me.INDlblTotal.TabIndex = 2
        Me.INDlblTotal.Text = "$0"
        '
        'INDLbcTaxesName
        '
        Me.INDLbcTaxesName.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDLbcTaxesName.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDLbcTaxesName.Appearance.Options.UseFont = True
        Me.INDLbcTaxesName.Appearance.Options.UseForeColor = True
        Me.INDLbcTaxesName.Appearance.Options.UseTextOptions = True
        Me.INDLbcTaxesName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLbcTaxesName.Location = New System.Drawing.Point(2, 30)
        Me.INDLbcTaxesName.Name = "INDLbcTaxesName"
        Me.INDLbcTaxesName.Size = New System.Drawing.Size(98, 30)
        Me.INDLbcTaxesName.StyleController = Me.LayoutControl1
        Me.INDLbcTaxesName.TabIndex = 1
        Me.INDLbcTaxesName.Text = "IVA"
        '
        'INDlblTaxValue
        '
        Me.INDlblTaxValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDlblTaxValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblTaxValue.Appearance.Options.UseFont = True
        Me.INDlblTaxValue.Appearance.Options.UseForeColor = True
        Me.INDlblTaxValue.Appearance.Options.UseTextOptions = True
        Me.INDlblTaxValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblTaxValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblTaxValue.Location = New System.Drawing.Point(100, 30)
        Me.INDlblTaxValue.Name = "INDlblTaxValue"
        Me.INDlblTaxValue.Size = New System.Drawing.Size(170, 30)
        Me.INDlblTaxValue.StyleController = Me.LayoutControl1
        Me.INDlblTaxValue.TabIndex = 2
        Me.INDlblTaxValue.Text = "$0"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.INDLciTaxes, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(306, 65)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDlblTotal
        Me.LayoutControlItem1.Location = New System.Drawing.Point(100, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(170, 30)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(170, 30)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(206, 30)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.LabelControl2
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(100, 30)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(100, 30)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(100, 30)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLciTaxes
        '
        Me.INDLciTaxes.Control = Me.INDLbcTaxesName
        Me.INDLciTaxes.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTaxes.CustomizationFormText = "LayoutControlItem2"
        Me.INDLciTaxes.Location = New System.Drawing.Point(0, 30)
        Me.INDLciTaxes.MaxSize = New System.Drawing.Size(100, 30)
        Me.INDLciTaxes.MinSize = New System.Drawing.Size(100, 30)
        Me.INDLciTaxes.Name = "INDLciTaxes"
        Me.INDLciTaxes.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.INDLciTaxes.Size = New System.Drawing.Size(100, 35)
        Me.INDLciTaxes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTaxes.Text = "LayoutControlItem2"
        Me.INDLciTaxes.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTaxes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciTaxes.TextToControlDistance = 0
        Me.INDLciTaxes.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDlblTaxValue
        Me.LayoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(100, 30)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(170, 30)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(170, 30)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem4.Size = New System.Drawing.Size(206, 35)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "LayoutControlItem1"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'CtrCostProduct
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrCostProduct"
        Me.Size = New System.Drawing.Size(306, 65)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTaxes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLbcTaxesName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciTaxes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblTaxValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
