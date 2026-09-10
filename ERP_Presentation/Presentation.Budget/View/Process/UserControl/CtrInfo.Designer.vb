<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrInfo
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
        Me.INDlbBudgetaryEntity = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlbItemBudgetaryEntity = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbItemValidity = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbValidity = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbItemStatus = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbStatus = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlbBudgetaryEntity
        '
        Me.INDlbBudgetaryEntity.Appearance.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.INDlbBudgetaryEntity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbBudgetaryEntity.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlbBudgetaryEntity.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbBudgetaryEntity.Location = New System.Drawing.Point(2, 16)
        Me.INDlbBudgetaryEntity.Name = "INDlbBudgetaryEntity"
        Me.INDlbBudgetaryEntity.Size = New System.Drawing.Size(290, 28)
        Me.INDlbBudgetaryEntity.StyleController = Me.LayoutControl1
        Me.INDlbBudgetaryEntity.TabIndex = 1
        Me.INDlbBudgetaryEntity.Text = "Entidad Presupuestal"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDlbItemBudgetaryEntity)
        Me.LayoutControl1.Controls.Add(Me.INDlbBudgetaryEntity)
        Me.LayoutControl1.Controls.Add(Me.INDlbItemValidity)
        Me.LayoutControl1.Controls.Add(Me.INDlbValidity)
        Me.LayoutControl1.Controls.Add(Me.INDlbItemStatus)
        Me.LayoutControl1.Controls.Add(Me.INDlbStatus)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 4
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlbItemBudgetaryEntity
        '
        Me.INDlbItemBudgetaryEntity.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.INDlbItemBudgetaryEntity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbItemBudgetaryEntity.Location = New System.Drawing.Point(2, 0)
        Me.INDlbItemBudgetaryEntity.Name = "INDlbItemBudgetaryEntity"
        Me.INDlbItemBudgetaryEntity.Size = New System.Drawing.Size(290, 16)
        Me.INDlbItemBudgetaryEntity.StyleController = Me.LayoutControl1
        Me.INDlbItemBudgetaryEntity.TabIndex = 0
        Me.INDlbItemBudgetaryEntity.Text = "Entidad Presupuestal"
        '
        'INDlbItemValidity
        '
        Me.INDlbItemValidity.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.INDlbItemValidity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbItemValidity.Location = New System.Drawing.Point(2, 44)
        Me.INDlbItemValidity.Name = "INDlbItemValidity"
        Me.INDlbItemValidity.Size = New System.Drawing.Size(48, 18)
        Me.INDlbItemValidity.StyleController = Me.LayoutControl1
        Me.INDlbItemValidity.TabIndex = 0
        Me.INDlbItemValidity.Text = "Vigencia"
        '
        'INDlbValidity
        '
        Me.INDlbValidity.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.INDlbValidity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbValidity.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbValidity.Location = New System.Drawing.Point(55, 44)
        Me.INDlbValidity.Name = "INDlbValidity"
        Me.INDlbValidity.Size = New System.Drawing.Size(91, 18)
        Me.INDlbValidity.StyleController = Me.LayoutControl1
        Me.INDlbValidity.TabIndex = 1
        Me.INDlbValidity.Text = "Vigencia"
        '
        'INDlbItemStatus
        '
        Me.INDlbItemStatus.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.INDlbItemStatus.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbItemStatus.Location = New System.Drawing.Point(146, 44)
        Me.INDlbItemStatus.Name = "INDlbItemStatus"
        Me.INDlbItemStatus.Size = New System.Drawing.Size(40, 18)
        Me.INDlbItemStatus.StyleController = Me.LayoutControl1
        Me.INDlbItemStatus.TabIndex = 2
        Me.INDlbItemStatus.Text = "Estado"
        '
        'INDlbStatus
        '
        Me.INDlbStatus.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.INDlbStatus.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbStatus.Location = New System.Drawing.Point(191, 44)
        Me.INDlbStatus.Name = "INDlbStatus"
        Me.INDlbStatus.Size = New System.Drawing.Size(101, 18)
        Me.INDlbStatus.StyleController = Me.LayoutControl1
        Me.INDlbStatus.TabIndex = 3
        Me.INDlbStatus.Text = "Estado"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem5, Me.LayoutControlItem3, Me.LayoutControlItem6, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDlbItemBudgetaryEntity
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(292, 16)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(292, 16)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(292, 16)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDlbBudgetaryEntity
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 16)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(292, 28)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(292, 28)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(292, 28)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDlbValidity
        Me.LayoutControlItem5.Location = New System.Drawing.Point(50, 44)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(96, 18)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(96, 18)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0)
        Me.LayoutControlItem5.Size = New System.Drawing.Size(96, 18)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDlbStatus
        Me.LayoutControlItem3.Location = New System.Drawing.Point(186, 44)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(106, 18)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(106, 18)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(106, 18)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDlbItemValidity
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 44)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(50, 18)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(50, 18)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem6.Size = New System.Drawing.Size(50, 18)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDlbItemStatus
        Me.LayoutControlItem4.Location = New System.Drawing.Point(146, 44)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(40, 18)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(40, 18)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem4.Size = New System.Drawing.Size(40, 18)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'CtrInfo
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrInfo"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlbItemBudgetaryEntity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbItemValidity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbBudgetaryEntity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbValidity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbItemStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
