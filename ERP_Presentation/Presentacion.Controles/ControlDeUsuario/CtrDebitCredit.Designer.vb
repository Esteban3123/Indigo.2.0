<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrDebitCredit
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.INDlbCredit = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbDebit = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbBalance = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlbAbbreviation = New DevExpress.XtraEditors.LabelControl()
        Me.INDLciAbbreviation = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAbbreviation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.BackColor = System.Drawing.Color.Transparent
        Me.LayoutControl1.Controls.Add(Me.Panel1)
        Me.LayoutControl1.Controls.Add(Me.INDlbCredit)
        Me.LayoutControl1.Controls.Add(Me.INDlbDebit)
        Me.LayoutControl1.Controls.Add(Me.LabelControl3)
        Me.LayoutControl1.Controls.Add(Me.LabelControl2)
        Me.LayoutControl1.Controls.Add(Me.INDlbBalance)
        Me.LayoutControl1.Controls.Add(Me.INDlbAbbreviation)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(294, 90)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(150, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(0, 36)
        Me.Panel1.TabIndex = 9
        '
        'INDlbCredit
        '
        Me.INDlbCredit.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlbCredit.Appearance.Options.UseFont = True
        Me.INDlbCredit.Appearance.Options.UseTextOptions = True
        Me.INDlbCredit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlbCredit.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlbCredit.Location = New System.Drawing.Point(149, 44)
        Me.INDlbCredit.Name = "INDlbCredit"
        Me.INDlbCredit.Size = New System.Drawing.Size(145, 20)
        Me.INDlbCredit.StyleController = Me.LayoutControl1
        Me.INDlbCredit.TabIndex = 8
        '
        'INDlbDebit
        '
        Me.INDlbDebit.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlbDebit.Appearance.Options.UseFont = True
        Me.INDlbDebit.Appearance.Options.UseTextOptions = True
        Me.INDlbDebit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlbDebit.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlbDebit.Location = New System.Drawing.Point(0, 44)
        Me.INDlbDebit.Name = "INDlbDebit"
        Me.INDlbDebit.Size = New System.Drawing.Size(148, 20)
        Me.INDlbDebit.StyleController = Me.LayoutControl1
        Me.INDlbDebit.TabIndex = 7
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseTextOptions = True
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl3.Location = New System.Drawing.Point(149, 24)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(145, 20)
        Me.LabelControl3.StyleController = Me.LayoutControl1
        Me.LabelControl3.TabIndex = 6
        Me.LabelControl3.Text = "Credito"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.LabelControl2.Location = New System.Drawing.Point(0, 24)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(148, 20)
        Me.LabelControl2.StyleController = Me.LayoutControl1
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Debito"
        '
        'INDlbBalance
        '
        Me.INDlbBalance.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlbBalance.Appearance.Options.UseFont = True
        Me.INDlbBalance.Appearance.Options.UseTextOptions = True
        Me.INDlbBalance.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlbBalance.Location = New System.Drawing.Point(0, 0)
        Me.INDlbBalance.Name = "INDlbBalance"
        Me.INDlbBalance.Size = New System.Drawing.Size(294, 24)
        Me.INDlbBalance.StyleController = Me.LayoutControl1
        Me.INDlbBalance.TabIndex = 4
        Me.INDlbBalance.Text = "Balance"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.INDLciAbbreviation})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(294, 90)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDlbBalance
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 24)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(49, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(294, 24)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.LabelControl2
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 20)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(56, 20)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(148, 20)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LabelControl3
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(149, 24)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 20)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(66, 20)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(145, 20)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDlbDebit
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 44)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(0, 20)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(70, 20)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem4.Size = New System.Drawing.Size(148, 20)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDlbCredit
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(149, 44)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(0, 20)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(70, 20)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem5.Size = New System.Drawing.Size(145, 20)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.Panel1
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(148, 24)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(1, 0)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(1, 24)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(1, 40)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'INDlbAbbreviation
        '
        Me.INDlbAbbreviation.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlbAbbreviation.Appearance.Options.UseFont = True
        Me.INDlbAbbreviation.Appearance.Options.UseTextOptions = True
        Me.INDlbAbbreviation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlbAbbreviation.Location = New System.Drawing.Point(0, 64)
        Me.INDlbAbbreviation.Name = "INDlbAbbreviation"
        Me.INDlbAbbreviation.Size = New System.Drawing.Size(294, 24)
        Me.INDlbAbbreviation.StyleController = Me.LayoutControl1
        Me.INDlbAbbreviation.TabIndex = 4
        Me.INDlbAbbreviation.Text = "Abreviacion"
        '
        'INDLciAbbreviation
        '
        Me.INDLciAbbreviation.Control = Me.INDlbAbbreviation
        Me.INDLciAbbreviation.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciAbbreviation.CustomizationFormText = "Abreviacion"
        Me.INDLciAbbreviation.Location = New System.Drawing.Point(0, 64)
        Me.INDLciAbbreviation.MaxSize = New System.Drawing.Size(0, 24)
        Me.INDLciAbbreviation.MinSize = New System.Drawing.Size(49, 24)
        Me.INDLciAbbreviation.Name = "INDLciAbbreviation"
        Me.INDLciAbbreviation.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLciAbbreviation.Size = New System.Drawing.Size(294, 26)
        Me.INDLciAbbreviation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAbbreviation.Text = "LayoutControlItem1"
        Me.INDLciAbbreviation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAbbreviation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAbbreviation.TextToControlDistance = 0
        Me.INDLciAbbreviation.TextVisible = False
        '
        'CtrDebitCredit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrDebitCredit"
        Me.Size = New System.Drawing.Size(294, 90)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAbbreviation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbBalance As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents INDlbCredit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbDebit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlbAbbreviation As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciAbbreviation As DevExpress.XtraLayout.LayoutControlItem
End Class
