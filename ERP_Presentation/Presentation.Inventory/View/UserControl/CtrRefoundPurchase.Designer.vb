<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrRefoundPurchase
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
        Me.INDPceDevolutionValue = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPceInvoiceValueTitle = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPceDevolutionType = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPceDevolutionValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceInvoiceValueTitle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDPceDevolutionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPceDevolutionValue
        '
        Me.INDPceDevolutionValue.CausesValidation = False
        Me.INDPceDevolutionValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceDevolutionValue.EditValue = "100000"
        Me.INDPceDevolutionValue.Location = New System.Drawing.Point(0, 0)
        Me.INDPceDevolutionValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceDevolutionValue.Name = "INDPceDevolutionValue"
        Me.INDPceDevolutionValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPceDevolutionValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 24.0!)
        Me.INDPceDevolutionValue.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDPceDevolutionValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceDevolutionValue.Properties.Appearance.Options.UseFont = True
        Me.INDPceDevolutionValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceDevolutionValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceDevolutionValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPceDevolutionValue.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom
        Me.INDPceDevolutionValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceDevolutionValue.Properties.Mask.IgnoreMaskBlank = False
        Me.INDPceDevolutionValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPceDevolutionValue.Properties.PopupSizeable = False
        Me.INDPceDevolutionValue.Properties.ShowPopupCloseButton = False
        Me.INDPceDevolutionValue.Properties.ShowPopupShadow = False
        Me.INDPceDevolutionValue.Size = New System.Drawing.Size(293, 50)
        Me.INDPceDevolutionValue.StyleController = Me.LayoutControl1
        Me.INDPceDevolutionValue.TabIndex = 12
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDPceDevolutionValue)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(293, 49)
        Me.LayoutControl1.TabIndex = 16
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(293, 49)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.BorderColor = System.Drawing.Color.Transparent
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseBorderColor = True
        Me.LayoutControlItem2.Control = Me.INDPceDevolutionValue
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 48)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(50, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(293, 49)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDPceInvoiceValueTitle
        '
        Me.INDPceInvoiceValueTitle.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceInvoiceValueTitle.EditValue = "VALOR DEVOLUCIÓN"
        Me.INDPceInvoiceValueTitle.Location = New System.Drawing.Point(0, 1)
        Me.INDPceInvoiceValueTitle.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceInvoiceValueTitle.Name = "INDPceInvoiceValueTitle"
        Me.INDPceInvoiceValueTitle.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPceInvoiceValueTitle.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPceInvoiceValueTitle.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Options.UseFont = True
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceInvoiceValueTitle.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDPceInvoiceValueTitle.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.INDPceInvoiceValueTitle.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDPceInvoiceValueTitle.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDPceInvoiceValueTitle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceInvoiceValueTitle.Properties.PopupSizeable = False
        Me.INDPceInvoiceValueTitle.Properties.ShowPopupCloseButton = False
        Me.INDPceInvoiceValueTitle.Properties.ShowPopupShadow = False
        Me.INDPceInvoiceValueTitle.Size = New System.Drawing.Size(120, 18)
        Me.INDPceInvoiceValueTitle.StyleController = Me.LayoutControl1
        Me.INDPceInvoiceValueTitle.TabIndex = 17
        '
        'LayoutControl2
        '
        Me.LayoutControl2.BackColor = System.Drawing.Color.MediumTurquoise
        Me.LayoutControl2.Controls.Add(Me.INDPceDevolutionType)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 49)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(293, 23)
        Me.LayoutControl2.TabIndex = 18
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDPceDevolutionType
        '
        Me.INDPceDevolutionType.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceDevolutionType.EditValue = "Devolución Total"
        Me.INDPceDevolutionType.Location = New System.Drawing.Point(0, 0)
        Me.INDPceDevolutionType.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceDevolutionType.Name = "INDPceDevolutionType"
        Me.INDPceDevolutionType.Properties.Appearance.BackColor = System.Drawing.Color.MediumTurquoise
        Me.INDPceDevolutionType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDPceDevolutionType.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDPceDevolutionType.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceDevolutionType.Properties.Appearance.Options.UseFont = True
        Me.INDPceDevolutionType.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceDevolutionType.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceDevolutionType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDPceDevolutionType.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None
        Me.INDPceDevolutionType.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDPceDevolutionType.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.INDPceDevolutionType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceDevolutionType.Properties.PopupSizeable = False
        Me.INDPceDevolutionType.Properties.ShowPopupCloseButton = False
        Me.INDPceDevolutionType.Properties.ShowPopupShadow = False
        Me.INDPceDevolutionType.Size = New System.Drawing.Size(293, 22)
        Me.INDPceDevolutionType.StyleController = Me.LayoutControl2
        Me.INDPceDevolutionType.TabIndex = 15
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.Root.Location = New System.Drawing.Point(0, 0)
        Me.Root.Name = "Root"
        Me.Root.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.Root.Size = New System.Drawing.Size(293, 23)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.BackColor = System.Drawing.Color.MediumTurquoise
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseBackColor = True
        Me.LayoutControlItem3.Control = Me.INDPceDevolutionType
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 20)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(293, 23)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "LayoutControlItem1"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'CtrRefoundPurchase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.LayoutControl2)
        Me.Controls.Add(Me.INDPceInvoiceValueTitle)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrRefoundPurchase"
        Me.Size = New System.Drawing.Size(293, 72)
        CType(Me.INDPceDevolutionValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceInvoiceValueTitle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDPceDevolutionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDPceDevolutionValue As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPceInvoiceValueTitle As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDPceDevolutionType As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
