<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrAdjustmentInformation
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
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpceAdjustmentText = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDpceDebitText = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDpceCreditValue = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDpceCreditText = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDpceDebitValue = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDpceAdjustmentValue = New DevExpress.XtraEditors.PopupContainerEdit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceAdjustmentText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceDebitText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceCreditValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceCreditText.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceDebitValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceAdjustmentValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDpceDebitText)
        Me.LayoutControl1.Controls.Add(Me.INDpceCreditValue)
        Me.LayoutControl1.Controls.Add(Me.INDpceCreditText)
        Me.LayoutControl1.Controls.Add(Me.INDpceDebitValue)
        Me.LayoutControl1.Controls.Add(Me.INDpceAdjustmentValue)
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
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(50, 20)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(50, 20)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(50, 20)
        '
        'INDpceAdjustmentText
        '
        Me.INDpceAdjustmentText.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceAdjustmentText.EditValue = "DIFERENCIA:"
        Me.INDpceAdjustmentText.Location = New System.Drawing.Point(0, 0)
        Me.INDpceAdjustmentText.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDpceAdjustmentText.Name = "INDpceAdjustmentText"
        Me.INDpceAdjustmentText.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceAdjustmentText.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!, System.Drawing.FontStyle.Bold)
        Me.INDpceAdjustmentText.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpceAdjustmentText.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceAdjustmentText.Properties.Appearance.Options.UseFont = True
        Me.INDpceAdjustmentText.Properties.Appearance.Options.UseForeColor = True
        Me.INDpceAdjustmentText.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceAdjustmentText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDpceAdjustmentText.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDpceAdjustmentText.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceAdjustmentText.Properties.PopupSizeable = False
        Me.INDpceAdjustmentText.Properties.ShowPopupCloseButton = False
        Me.INDpceAdjustmentText.Properties.ShowPopupShadow = False
        Me.INDpceAdjustmentText.Size = New System.Drawing.Size(80, 18)
        Me.INDpceAdjustmentText.StyleController = Me.LayoutControl1
        Me.INDpceAdjustmentText.TabIndex = 9
        '
        'INDpceDebitText
        '
        Me.INDpceDebitText.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceDebitText.EditValue = "DEBITO:"
        Me.INDpceDebitText.Location = New System.Drawing.Point(0, 39)
        Me.INDpceDebitText.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDpceDebitText.Name = "INDpceDebitText"
        Me.INDpceDebitText.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceDebitText.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 6.0!, System.Drawing.FontStyle.Bold)
        Me.INDpceDebitText.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpceDebitText.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceDebitText.Properties.Appearance.Options.UseFont = True
        Me.INDpceDebitText.Properties.Appearance.Options.UseForeColor = True
        Me.INDpceDebitText.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceDebitText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDpceDebitText.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDpceDebitText.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceDebitText.Properties.PopupSizeable = False
        Me.INDpceDebitText.Properties.ShowPopupCloseButton = False
        Me.INDpceDebitText.Properties.ShowPopupShadow = False
        Me.INDpceDebitText.Size = New System.Drawing.Size(65, 16)
        Me.INDpceDebitText.StyleController = Me.LayoutControl1
        Me.INDpceDebitText.TabIndex = 12
        '
        'INDpceCreditValue
        '
        Me.INDpceCreditValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceCreditValue.EditValue = "$ 1.000.000.000"
        Me.INDpceCreditValue.Location = New System.Drawing.Point(211, 39)
        Me.INDpceCreditValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDpceCreditValue.Name = "INDpceCreditValue"
        Me.INDpceCreditValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceCreditValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDpceCreditValue.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpceCreditValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceCreditValue.Properties.Appearance.Options.UseFont = True
        Me.INDpceCreditValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDpceCreditValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceCreditValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDpceCreditValue.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDpceCreditValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceCreditValue.Properties.DisplayFormat.FormatString = "c2"
        Me.INDpceCreditValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDpceCreditValue.Properties.PopupSizeable = False
        Me.INDpceCreditValue.Properties.ShowPopupCloseButton = False
        Me.INDpceCreditValue.Properties.ShowPopupShadow = False
        Me.INDpceCreditValue.Size = New System.Drawing.Size(80, 18)
        Me.INDpceCreditValue.StyleController = Me.LayoutControl1
        Me.INDpceCreditValue.TabIndex = 17
        '
        'INDpceCreditText
        '
        Me.INDpceCreditText.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceCreditText.EditValue = "CREDITO:"
        Me.INDpceCreditText.Location = New System.Drawing.Point(145, 39)
        Me.INDpceCreditText.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDpceCreditText.Name = "INDpceCreditText"
        Me.INDpceCreditText.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceCreditText.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 6.0!, System.Drawing.FontStyle.Bold)
        Me.INDpceCreditText.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpceCreditText.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceCreditText.Properties.Appearance.Options.UseFont = True
        Me.INDpceCreditText.Properties.Appearance.Options.UseForeColor = True
        Me.INDpceCreditText.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceCreditText.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDpceCreditText.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDpceCreditText.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceCreditText.Properties.DisplayFormat.FormatString = "c2"
        Me.INDpceCreditText.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDpceCreditText.Properties.PopupSizeable = False
        Me.INDpceCreditText.Properties.ShowPopupCloseButton = False
        Me.INDpceCreditText.Properties.ShowPopupShadow = False
        Me.INDpceCreditText.Size = New System.Drawing.Size(65, 16)
        Me.INDpceCreditText.StyleController = Me.LayoutControl1
        Me.INDpceCreditText.TabIndex = 15
        '
        'INDpceDebitValue
        '
        Me.INDpceDebitValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceDebitValue.EditValue = "$ 1.000.000.000"
        Me.INDpceDebitValue.Location = New System.Drawing.Point(66, 39)
        Me.INDpceDebitValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDpceDebitValue.Name = "INDpceDebitValue"
        Me.INDpceDebitValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceDebitValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 8.0!)
        Me.INDpceDebitValue.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpceDebitValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceDebitValue.Properties.Appearance.Options.UseFont = True
        Me.INDpceDebitValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDpceDebitValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceDebitValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDpceDebitValue.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDpceDebitValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceDebitValue.Properties.DisplayFormat.FormatString = "c2"
        Me.INDpceDebitValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDpceDebitValue.Properties.PopupSizeable = False
        Me.INDpceDebitValue.Properties.ShowPopupCloseButton = False
        Me.INDpceDebitValue.Properties.ShowPopupShadow = False
        Me.INDpceDebitValue.Size = New System.Drawing.Size(80, 18)
        Me.INDpceDebitValue.StyleController = Me.LayoutControl1
        Me.INDpceDebitValue.TabIndex = 13
        '
        'INDpceAdjustmentValue
        '
        Me.INDpceAdjustmentValue.CausesValidation = False
        Me.INDpceAdjustmentValue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDpceAdjustmentValue.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpceAdjustmentValue.EditValue = "$ 1.000.000"
        Me.INDpceAdjustmentValue.Location = New System.Drawing.Point(2, 0)
        Me.INDpceAdjustmentValue.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDpceAdjustmentValue.Name = "INDpceAdjustmentValue"
        Me.INDpceAdjustmentValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceAdjustmentValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 20.0!)
        Me.INDpceAdjustmentValue.Properties.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDpceAdjustmentValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceAdjustmentValue.Properties.Appearance.Options.UseFont = True
        Me.INDpceAdjustmentValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDpceAdjustmentValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDpceAdjustmentValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDpceAdjustmentValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceAdjustmentValue.Properties.Mask.IgnoreMaskBlank = False
        Me.INDpceAdjustmentValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDpceAdjustmentValue.Properties.PopupSizeable = False
        Me.INDpceAdjustmentValue.Properties.ShowPopupCloseButton = False
        Me.INDpceAdjustmentValue.Properties.ShowPopupShadow = False
        Me.INDpceAdjustmentValue.Size = New System.Drawing.Size(290, 42)
        Me.INDpceAdjustmentValue.StyleController = Me.LayoutControl1
        Me.INDpceAdjustmentValue.TabIndex = 18
        '
        'CtrAdjustmentInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.INDpceAdjustmentText)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximumSize = New System.Drawing.Size(292, 57)
        Me.MinimumSize = New System.Drawing.Size(292, 57)
        Me.Name = "CtrAdjustmentInformation"
        Me.Size = New System.Drawing.Size(292, 57)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceAdjustmentText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceDebitText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceCreditValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceCreditText.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceDebitValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceAdjustmentValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDpceAdjustmentText As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDpceDebitText As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDpceDebitValue As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDpceCreditValue As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDpceCreditText As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDpceAdjustmentValue As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
