<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrValue
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
        Me.INDlbValueCxP = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlbValueIVA = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbValueBill = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciBillValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValueCxP = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIVA = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBillValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValueCxP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIVA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlbValueCxP
        '
        Me.INDlbValueCxP.Appearance.Font = New System.Drawing.Font("Segoe UI", 20.0!)
        Me.INDlbValueCxP.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbValueCxP.Appearance.Options.UseFont = True
        Me.INDlbValueCxP.Appearance.Options.UseForeColor = True
        Me.INDlbValueCxP.Appearance.Options.UseTextOptions = True
        Me.INDlbValueCxP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlbValueCxP.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbValueCxP.Location = New System.Drawing.Point(98, 0)
        Me.INDlbValueCxP.Name = "INDlbValueCxP"
        Me.INDlbValueCxP.Size = New System.Drawing.Size(209, 40)
        Me.INDlbValueCxP.StyleController = Me.LayoutControl1
        Me.INDlbValueCxP.TabIndex = 1
        Me.INDlbValueCxP.Text = "$0"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.BackColor = System.Drawing.Color.Transparent
        Me.LayoutControl1.Controls.Add(Me.INDlbValueIVA)
        Me.LayoutControl1.Controls.Add(Me.INDlbValueCxP)
        Me.LayoutControl1.Controls.Add(Me.INDlbValueBill)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(307, 94)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlbValueIVA
        '
        Me.INDlbValueIVA.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlbValueIVA.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbValueIVA.Appearance.Options.UseFont = True
        Me.INDlbValueIVA.Appearance.Options.UseForeColor = True
        Me.INDlbValueIVA.Appearance.Options.UseTextOptions = True
        Me.INDlbValueIVA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlbValueIVA.Location = New System.Drawing.Point(67, 65)
        Me.INDlbValueIVA.Name = "INDlbValueIVA"
        Me.INDlbValueIVA.Size = New System.Drawing.Size(240, 29)
        Me.INDlbValueIVA.StyleController = Me.LayoutControl1
        Me.INDlbValueIVA.TabIndex = 4
        Me.INDlbValueIVA.Text = "$0"
        '
        'INDlbValueBill
        '
        Me.INDlbValueBill.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlbValueBill.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbValueBill.Appearance.Options.UseFont = True
        Me.INDlbValueBill.Appearance.Options.UseForeColor = True
        Me.INDlbValueBill.Appearance.Options.UseTextOptions = True
        Me.INDlbValueBill.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlbValueBill.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbValueBill.Location = New System.Drawing.Point(96, 40)
        Me.INDlbValueBill.Name = "INDlbValueBill"
        Me.INDlbValueBill.Size = New System.Drawing.Size(211, 25)
        Me.INDlbValueBill.StyleController = Me.LayoutControl1
        Me.INDlbValueBill.TabIndex = 1
        Me.INDlbValueBill.Text = "$0"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciBillValue, Me.INDLciValueCxP, Me.INDLciIVA})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(307, 94)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLciBillValue
        '
        Me.INDLciBillValue.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDLciBillValue.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.INDLciBillValue.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciBillValue.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLciBillValue.Control = Me.INDlbValueBill
        Me.INDLciBillValue.Location = New System.Drawing.Point(0, 40)
        Me.INDLciBillValue.MinSize = New System.Drawing.Size(36, 25)
        Me.INDLciBillValue.Name = "INDLciBillValue"
        Me.INDLciBillValue.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLciBillValue.Size = New System.Drawing.Size(307, 25)
        Me.INDLciBillValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBillValue.Text = "Valor Facturado"
        Me.INDLciBillValue.TextSize = New System.Drawing.Size(93, 17)
        '
        'INDLciValueCxP
        '
        Me.INDLciValueCxP.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLciValueCxP.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.INDLciValueCxP.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciValueCxP.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLciValueCxP.Control = Me.INDlbValueCxP
        Me.INDLciValueCxP.Location = New System.Drawing.Point(0, 0)
        Me.INDLciValueCxP.MinSize = New System.Drawing.Size(25, 37)
        Me.INDLciValueCxP.Name = "INDLciValueCxP"
        Me.INDLciValueCxP.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLciValueCxP.Size = New System.Drawing.Size(307, 40)
        Me.INDLciValueCxP.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValueCxP.Text = "Valor CxP"
        Me.INDLciValueCxP.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValueCxP.TextSize = New System.Drawing.Size(98, 0)
        Me.INDLciValueCxP.TextToControlDistance = 0
        '
        'INDLciIVA
        '
        Me.INDLciIVA.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDLciIVA.AppearanceItemCaption.ForeColor = System.Drawing.Color.White
        Me.INDLciIVA.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciIVA.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLciIVA.Control = Me.INDlbValueIVA
        Me.INDLciIVA.Location = New System.Drawing.Point(0, 65)
        Me.INDLciIVA.MinSize = New System.Drawing.Size(36, 25)
        Me.INDLciIVA.Name = "INDLciIVA"
        Me.INDLciIVA.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLciIVA.Size = New System.Drawing.Size(307, 29)
        Me.INDLciIVA.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIVA.Text = "IVA"
        Me.INDLciIVA.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciIVA.TextSize = New System.Drawing.Size(50, 0)
        Me.INDLciIVA.TextToControlDistance = 17
        '
        'CtrValue
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrValue"
        Me.Size = New System.Drawing.Size(307, 94)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBillValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValueCxP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIVA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlbValueCxP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbValueBill As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciBillValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValueCxP As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlbValueIVA As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciIVA As DevExpress.XtraLayout.LayoutControlItem
End Class
