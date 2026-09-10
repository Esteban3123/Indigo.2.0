<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrVoucherTransactionCrossing
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
        Me.INDlbCxCValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbCxPValue = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlbDifference = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDlbCxCValue)
        Me.LayoutControl1.Controls.Add(Me.INDlbCxPValue)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlbCxCValue
        '
        Me.INDlbCxCValue.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlbCxCValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbCxCValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDlbCxCValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbCxCValue.Location = New System.Drawing.Point(12, 20)
        Me.INDlbCxCValue.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlbCxCValue.MaximumSize = New System.Drawing.Size(0, 18)
        Me.INDlbCxCValue.MinimumSize = New System.Drawing.Size(0, 18)
        Me.INDlbCxCValue.Name = "INDlbCxCValue"
        Me.INDlbCxCValue.Padding = New System.Windows.Forms.Padding(0, 0, 8, 0)
        Me.INDlbCxCValue.Size = New System.Drawing.Size(133, 18)
        Me.INDlbCxCValue.StyleController = Me.LayoutControl1
        Me.INDlbCxCValue.TabIndex = 5
        Me.INDlbCxCValue.Text = "$0"
        '
        'INDlbCxPValue
        '
        Me.INDlbCxPValue.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlbCxPValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbCxPValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDlbCxPValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbCxPValue.Location = New System.Drawing.Point(145, 20)
        Me.INDlbCxPValue.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlbCxPValue.MaximumSize = New System.Drawing.Size(0, 18)
        Me.INDlbCxPValue.MinimumSize = New System.Drawing.Size(0, 18)
        Me.INDlbCxPValue.Name = "INDlbCxPValue"
        Me.INDlbCxPValue.Padding = New System.Windows.Forms.Padding(0, 0, 8, 0)
        Me.INDlbCxPValue.Size = New System.Drawing.Size(147, 18)
        Me.INDlbCxPValue.StyleController = Me.LayoutControl1
        Me.INDlbCxPValue.TabIndex = 5
        Me.INDlbCxPValue.Text = "$0"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDlbCxCValue
        Me.LayoutControlItem2.CustomizationFormText = "CxP"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(125, 40)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(145, 62)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Facturas CxC"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem2.TextToControlDistance = 0
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDlbCxPValue
        Me.LayoutControlItem1.CustomizationFormText = "CxP"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(145, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(125, 40)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(147, 62)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Facturas CxP"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem1.TextToControlDistance = 0
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDlbDifference)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 41)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(292, 21)
        Me.LayoutControl2.TabIndex = 1
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDlbDifference
        '
        Me.INDlbDifference.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlbDifference.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbDifference.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlbDifference.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbDifference.Location = New System.Drawing.Point(147, 0)
        Me.INDlbDifference.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlbDifference.MaximumSize = New System.Drawing.Size(0, 18)
        Me.INDlbDifference.MinimumSize = New System.Drawing.Size(0, 18)
        Me.INDlbDifference.Name = "INDlbDifference"
        Me.INDlbDifference.Padding = New System.Windows.Forms.Padding(0, 0, 8, 0)
        Me.INDlbDifference.Size = New System.Drawing.Size(145, 18)
        Me.INDlbDifference.StyleController = Me.LayoutControl2
        Me.INDlbDifference.TabIndex = 5
        Me.INDlbDifference.Text = "$0"
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(292, 21)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem3.Control = Me.INDlbDifference
        Me.LayoutControlItem3.CustomizationFormText = "CxP"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 20)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(237, 20)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(12, 0, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(292, 21)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Diferencia"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(135, 20)
        Me.LayoutControlItem3.TextToControlDistance = 0
        '
        'CtrVoucherTransactionCrossing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl2)
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximumSize = New System.Drawing.Size(292, 62)
        Me.MinimumSize = New System.Drawing.Size(292, 62)
        Me.Name = "CtrVoucherTransactionCrossing"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlbCxCValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlbCxPValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlbDifference As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
