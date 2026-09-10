<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrTotalPayrollLiquidation
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
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDPceTotalValue = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPceInvoiceValueTitle = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.PopupContainerEdit1 = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPceAccruedValue = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.PopupContainerEdit6 = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPceDeductedValue = New DevExpress.XtraEditors.PopupContainerEdit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceTotalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceInvoiceValueTitle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceAccruedValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerEdit6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceDeductedValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 57)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 57)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDPceTotalValue
        '
        Me.INDPceTotalValue.CausesValidation = False
        Me.INDPceTotalValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceTotalValue.EditValue = "100000"
        Me.INDPceTotalValue.Location = New System.Drawing.Point(0, 5)
        Me.INDPceTotalValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceTotalValue.Name = "INDPceTotalValue"
        Me.INDPceTotalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPceTotalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 20.0!)
        Me.INDPceTotalValue.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDPceTotalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceTotalValue.Properties.Appearance.Options.UseFont = True
        Me.INDPceTotalValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceTotalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceTotalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPceTotalValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceTotalValue.Properties.DisplayFormat.FormatString = "c2"
        Me.INDPceTotalValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDPceTotalValue.Properties.Mask.IgnoreMaskBlank = False
        Me.INDPceTotalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPceTotalValue.Properties.PopupSizeable = False
        Me.INDPceTotalValue.Properties.ShowPopupCloseButton = False
        Me.INDPceTotalValue.Properties.ShowPopupShadow = False
        Me.INDPceTotalValue.Size = New System.Drawing.Size(292, 42)
        Me.INDPceTotalValue.StyleController = Me.LayoutControl1
        Me.INDPceTotalValue.TabIndex = 11
        '
        'INDPceInvoiceValueTitle
        '
        Me.INDPceInvoiceValueTitle.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceInvoiceValueTitle.EditValue = "TOTAL A PAGAR:"
        Me.INDPceInvoiceValueTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDPceInvoiceValueTitle.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceInvoiceValueTitle.Name = "INDPceInvoiceValueTitle"
        Me.INDPceInvoiceValueTitle.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDPceInvoiceValueTitle.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Options.UseFont = True
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceInvoiceValueTitle.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceInvoiceValueTitle.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDPceInvoiceValueTitle.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDPceInvoiceValueTitle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceInvoiceValueTitle.Properties.PopupSizeable = False
        Me.INDPceInvoiceValueTitle.Properties.ShowPopupCloseButton = False
        Me.INDPceInvoiceValueTitle.Properties.ShowPopupShadow = False
        Me.INDPceInvoiceValueTitle.Size = New System.Drawing.Size(292, 18)
        Me.INDPceInvoiceValueTitle.StyleController = Me.LayoutControl1
        Me.INDPceInvoiceValueTitle.TabIndex = 9
        '
        'PopupContainerEdit1
        '
        Me.PopupContainerEdit1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PopupContainerEdit1.EditValue = "T. Devengado:"
        Me.PopupContainerEdit1.Location = New System.Drawing.Point(0, 41)
        Me.PopupContainerEdit1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PopupContainerEdit1.Name = "PopupContainerEdit1"
        Me.PopupContainerEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PopupContainerEdit1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 6.0!, System.Drawing.FontStyle.Bold)
        Me.PopupContainerEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.PopupContainerEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PopupContainerEdit1.Properties.Appearance.Options.UseFont = True
        Me.PopupContainerEdit1.Properties.Appearance.Options.UseForeColor = True
        Me.PopupContainerEdit1.Properties.Appearance.Options.UseTextOptions = True
        Me.PopupContainerEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.PopupContainerEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PopupContainerEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PopupContainerEdit1.Properties.PopupSizeable = False
        Me.PopupContainerEdit1.Properties.ShowPopupCloseButton = False
        Me.PopupContainerEdit1.Properties.ShowPopupShadow = False
        Me.PopupContainerEdit1.Size = New System.Drawing.Size(75, 16)
        Me.PopupContainerEdit1.StyleController = Me.LayoutControl1
        Me.PopupContainerEdit1.TabIndex = 12
        '
        'INDPceAccruedValue
        '
        Me.INDPceAccruedValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceAccruedValue.EditValue = "100000"
        Me.INDPceAccruedValue.Location = New System.Drawing.Point(65, 41)
        Me.INDPceAccruedValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceAccruedValue.Name = "INDPceAccruedValue"
        Me.INDPceAccruedValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPceAccruedValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPceAccruedValue.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDPceAccruedValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceAccruedValue.Properties.Appearance.Options.UseFont = True
        Me.INDPceAccruedValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceAccruedValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceAccruedValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPceAccruedValue.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDPceAccruedValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceAccruedValue.Properties.DisplayFormat.FormatString = "c2"
        Me.INDPceAccruedValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDPceAccruedValue.Properties.PopupSizeable = False
        Me.INDPceAccruedValue.Properties.ShowPopupCloseButton = False
        Me.INDPceAccruedValue.Properties.ShowPopupShadow = False
        Me.INDPceAccruedValue.Size = New System.Drawing.Size(77, 18)
        Me.INDPceAccruedValue.StyleController = Me.LayoutControl1
        Me.INDPceAccruedValue.TabIndex = 13
        '
        'PopupContainerEdit6
        '
        Me.PopupContainerEdit6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PopupContainerEdit6.EditValue = "Total Deducido"
        Me.PopupContainerEdit6.Location = New System.Drawing.Point(146, 41)
        Me.PopupContainerEdit6.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PopupContainerEdit6.Name = "PopupContainerEdit6"
        Me.PopupContainerEdit6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PopupContainerEdit6.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 6.0!, System.Drawing.FontStyle.Bold)
        Me.PopupContainerEdit6.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.PopupContainerEdit6.Properties.Appearance.Options.UseBackColor = True
        Me.PopupContainerEdit6.Properties.Appearance.Options.UseFont = True
        Me.PopupContainerEdit6.Properties.Appearance.Options.UseForeColor = True
        Me.PopupContainerEdit6.Properties.Appearance.Options.UseTextOptions = True
        Me.PopupContainerEdit6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.PopupContainerEdit6.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PopupContainerEdit6.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PopupContainerEdit6.Properties.PopupSizeable = False
        Me.PopupContainerEdit6.Properties.ShowPopupCloseButton = False
        Me.PopupContainerEdit6.Properties.ShowPopupShadow = False
        Me.PopupContainerEdit6.Size = New System.Drawing.Size(82, 16)
        Me.PopupContainerEdit6.StyleController = Me.LayoutControl1
        Me.PopupContainerEdit6.TabIndex = 16
        '
        'INDPceDeductedValue
        '
        Me.INDPceDeductedValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceDeductedValue.EditValue = "100000"
        Me.INDPceDeductedValue.Location = New System.Drawing.Point(222, 41)
        Me.INDPceDeductedValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceDeductedValue.Name = "INDPceDeductedValue"
        Me.INDPceDeductedValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPceDeductedValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDPceDeductedValue.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDPceDeductedValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceDeductedValue.Properties.Appearance.Options.UseFont = True
        Me.INDPceDeductedValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceDeductedValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceDeductedValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPceDeductedValue.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDPceDeductedValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceDeductedValue.Properties.DisplayFormat.FormatString = "c2"
        Me.INDPceDeductedValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDPceDeductedValue.Properties.PopupSizeable = False
        Me.INDPceDeductedValue.Properties.ShowPopupCloseButton = False
        Me.INDPceDeductedValue.Properties.ShowPopupShadow = False
        Me.INDPceDeductedValue.Size = New System.Drawing.Size(70, 18)
        Me.INDPceDeductedValue.StyleController = Me.LayoutControl1
        Me.INDPceDeductedValue.TabIndex = 17
        '
        'CtrTotalPayrollLiquidation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.INDPceDeductedValue)
        Me.Controls.Add(Me.PopupContainerEdit6)
        Me.Controls.Add(Me.INDPceAccruedValue)
        Me.Controls.Add(Me.PopupContainerEdit1)
        Me.Controls.Add(Me.INDPceInvoiceValueTitle)
        Me.Controls.Add(Me.INDPceTotalValue)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximumSize = New System.Drawing.Size(292, 57)
        Me.MinimumSize = New System.Drawing.Size(292, 57)
        Me.Name = "CtrTotalPayrollLiquidation"
        Me.Size = New System.Drawing.Size(292, 57)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceTotalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceInvoiceValueTitle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceAccruedValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerEdit6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceDeductedValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDPceTotalValue As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDPceInvoiceValueTitle As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents PopupContainerEdit1 As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDPceAccruedValue As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents PopupContainerEdit6 As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDPceDeductedValue As DevExpress.XtraEditors.PopupContainerEdit
End Class
