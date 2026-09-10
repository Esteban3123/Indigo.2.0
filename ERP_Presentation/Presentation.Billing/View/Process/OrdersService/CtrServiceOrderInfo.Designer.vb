<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrServiceOrderInfo
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
        Me.INDLbTaxValue = New DevExpress.XtraEditors.LabelControl()
        Me.PcePopUpEdit = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPceValue = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControl1.Controls.Add(Me.INDLbTaxValue)
        Me.LayoutControl1.Controls.Add(Me.PcePopUpEdit)
        Me.LayoutControl1.Controls.Add(Me.INDPceValue)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 90)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLbTaxValue
        '
        Me.INDLbTaxValue.Appearance.BackColor = System.Drawing.Color.White
        Me.INDLbTaxValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLbTaxValue.Appearance.Options.UseBackColor = True
        Me.INDLbTaxValue.Appearance.Options.UseFont = True
        Me.INDLbTaxValue.Appearance.Options.UseTextOptions = True
        Me.INDLbTaxValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDLbTaxValue.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDLbTaxValue.Location = New System.Drawing.Point(2, 47)
        Me.INDLbTaxValue.Name = "INDLbTaxValue"
        Me.INDLbTaxValue.Padding = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.INDLbTaxValue.Size = New System.Drawing.Size(288, 18)
        Me.INDLbTaxValue.StyleController = Me.LayoutControl1
        Me.INDLbTaxValue.TabIndex = 8
        Me.INDLbTaxValue.Text = "$0"
        '
        'PcePopUpEdit
        '
        Me.PcePopUpEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PcePopUpEdit.EditValue = "PACIENTE"
        Me.PcePopUpEdit.Location = New System.Drawing.Point(0, 67)
        Me.PcePopUpEdit.Margin = New System.Windows.Forms.Padding(0)
        Me.PcePopUpEdit.Name = "PcePopUpEdit"
        Me.PcePopUpEdit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.PcePopUpEdit.Properties.Appearance.Options.UseFont = True
        Me.PcePopUpEdit.Properties.Appearance.Options.UseTextOptions = True
        Me.PcePopUpEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PcePopUpEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PcePopUpEdit.Properties.PopupSizeable = False
        Me.PcePopUpEdit.Properties.ShowPopupCloseButton = False
        Me.PcePopUpEdit.Properties.ShowPopupShadow = False
        Me.PcePopUpEdit.Size = New System.Drawing.Size(292, 22)
        Me.PcePopUpEdit.StyleController = Me.LayoutControl1
        Me.PcePopUpEdit.TabIndex = 6
        '
        'INDPceValue
        '
        Me.INDPceValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceValue.EditValue = "10000000"
        Me.INDPceValue.Location = New System.Drawing.Point(0, -12)
        Me.INDPceValue.Margin = New System.Windows.Forms.Padding(0)
        Me.INDPceValue.Name = "INDPceValue"
        Me.INDPceValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 30.0!)
        Me.INDPceValue.Properties.Appearance.Options.UseFont = True
        Me.INDPceValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPceValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceValue.Properties.Mask.EditMask = "c2"
        Me.INDPceValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDPceValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPceValue.Properties.PopupSizeable = False
        Me.INDPceValue.Properties.ShowPopupCloseButton = False
        Me.INDPceValue.Size = New System.Drawing.Size(292, 58)
        Me.INDPceValue.StyleController = Me.LayoutControl1
        Me.INDPceValue.TabIndex = 7
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 90)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.PcePopUpEdit
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 67)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 12)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(54, 12)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(292, 23)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDPceValue
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 45)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(282, 45)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, -12, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(292, 45)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.INDLbTaxValue
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 45)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 22)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(1, 22)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.OptionsPrint.AppearanceItem.BackColor = System.Drawing.Color.White
        Me.LayoutControlItem3.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem3.OptionsPrint.AppearanceItem.Options.UseBackColor = True
        Me.LayoutControlItem3.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.LayoutControlItem3.OptionsPrint.AppearanceItemControl.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem3.OptionsPrint.AppearanceItemControl.Options.UseFont = True
        Me.LayoutControlItem3.OptionsPrint.AppearanceItemText.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem3.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.LayoutControlItem3.OptionsPrint.AppearanceItemText.Options.UseTextOptions = True
        Me.LayoutControlItem3.OptionsPrint.AppearanceItemText.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.OptionsPrint.AppearanceItemText.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem3.Size = New System.Drawing.Size(292, 22)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "IVA"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'CtrServiceOrderInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximumSize = New System.Drawing.Size(292, 90)
        Me.MinimumSize = New System.Drawing.Size(292, 90)
        Me.Name = "CtrServiceOrderInfo"
        Me.Size = New System.Drawing.Size(292, 90)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PcePopUpEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PcePopUpEdit As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPceValue As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLbTaxValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
