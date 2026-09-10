<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrValueAccountReceivableDocuments
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
        Me.INDlblAdvanceValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblAdvanceTitle = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDlblAdvanceValue)
        Me.LayoutControl1.Controls.Add(Me.INDlblAdvanceTitle)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1062, 382, 650, 400)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(277, 77)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlblAdvanceValue
        '
        Me.INDlblAdvanceValue.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 20.0!, System.Drawing.FontStyle.Bold)
        Me.INDlblAdvanceValue.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
        Me.INDlblAdvanceValue.Appearance.Options.UseFont = True
        Me.INDlblAdvanceValue.Appearance.Options.UseForeColor = True
        Me.INDlblAdvanceValue.Appearance.Options.UseTextOptions = True
        Me.INDlblAdvanceValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblAdvanceValue.Location = New System.Drawing.Point(2, 2)
        Me.INDlblAdvanceValue.MinimumSize = New System.Drawing.Size(250, 30)
        Me.INDlblAdvanceValue.Name = "INDlblAdvanceValue"
        Me.INDlblAdvanceValue.Size = New System.Drawing.Size(273, 39)
        Me.INDlblAdvanceValue.StyleController = Me.LayoutControl1
        Me.INDlblAdvanceValue.TabIndex = 9
        Me.INDlblAdvanceValue.Text = "$000000000000000"
        '
        'INDlblAdvanceTitle
        '
        Me.INDlblAdvanceTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlblAdvanceTitle.Appearance.ForeColor = System.Drawing.SystemColors.WindowText
        Me.INDlblAdvanceTitle.Appearance.Options.UseFont = True
        Me.INDlblAdvanceTitle.Appearance.Options.UseForeColor = True
        Me.INDlblAdvanceTitle.Appearance.Options.UseTextOptions = True
        Me.INDlblAdvanceTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblAdvanceTitle.Location = New System.Drawing.Point(4, 43)
        Me.INDlblAdvanceTitle.MinimumSize = New System.Drawing.Size(250, 30)
        Me.INDlblAdvanceTitle.Name = "INDlblAdvanceTitle"
        Me.INDlblAdvanceTitle.Size = New System.Drawing.Size(269, 30)
        Me.INDlblAdvanceTitle.StyleController = Me.LayoutControl1
        Me.INDlblAdvanceTitle.TabIndex = 8
        Me.INDlblAdvanceTitle.Text = "Valor Total"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(277, 77)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDlblAdvanceValue
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(25, 37)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(273, 39)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDlblAdvanceTitle
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 39)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(250, 34)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(273, 34)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'CtrValueAccountReceivableDocuments
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrValueAccountReceivableDocuments"
        Me.Size = New System.Drawing.Size(277, 77)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlblAdvanceValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblAdvanceTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
