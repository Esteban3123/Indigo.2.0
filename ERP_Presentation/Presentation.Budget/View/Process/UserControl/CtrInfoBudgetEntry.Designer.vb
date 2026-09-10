<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrInfoBudgetEntry
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
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDpbValues = New DevExpress.XtraEditors.ProgressBarControl()
        Me.INDlbValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbStatus = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbBudgetaryEntity = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbItemStatus = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbItemValidity = New DevExpress.XtraEditors.LabelControl()
        Me.INDlbValidity = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.INDpbValues.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.PanelControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControl1.TabIndex = 6
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.INDpbValues)
        Me.PanelControl1.Controls.Add(Me.INDlbValue)
        Me.PanelControl1.Controls.Add(Me.INDlbStatus)
        Me.PanelControl1.Controls.Add(Me.INDlbBudgetaryEntity)
        Me.PanelControl1.Controls.Add(Me.INDlbItemStatus)
        Me.PanelControl1.Controls.Add(Me.INDlbItemValidity)
        Me.PanelControl1.Controls.Add(Me.INDlbValidity)
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(292, 62)
        Me.PanelControl1.TabIndex = 4
        '
        'INDpbValues
        '
        Me.INDpbValues.Location = New System.Drawing.Point(188, 39)
        Me.INDpbValues.Name = "INDpbValues"
        Me.INDpbValues.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpbValues.Properties.Step = 0
        Me.INDpbValues.Size = New System.Drawing.Size(101, 18)
        Me.INDpbValues.TabIndex = 5
        '
        'INDlbValue
        '
        Me.INDlbValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlbValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbValue.Location = New System.Drawing.Point(8, 39)
        Me.INDlbValue.Name = "INDlbValue"
        Me.INDlbValue.Size = New System.Drawing.Size(100, 21)
        Me.INDlbValue.TabIndex = 4
        Me.INDlbValue.Text = "Valor Vigencia"
        '
        'INDlbStatus
        '
        Me.INDlbStatus.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.INDlbStatus.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbStatus.Location = New System.Drawing.Point(188, 22)
        Me.INDlbStatus.Name = "INDlbStatus"
        Me.INDlbStatus.Size = New System.Drawing.Size(35, 13)
        Me.INDlbStatus.TabIndex = 3
        Me.INDlbStatus.Text = "Estado"
        '
        'INDlbBudgetaryEntity
        '
        Me.INDlbBudgetaryEntity.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.INDlbBudgetaryEntity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbBudgetaryEntity.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbBudgetaryEntity.Location = New System.Drawing.Point(8, -2)
        Me.INDlbBudgetaryEntity.Name = "INDlbBudgetaryEntity"
        Me.INDlbBudgetaryEntity.Size = New System.Drawing.Size(283, 25)
        Me.INDlbBudgetaryEntity.TabIndex = 1
        Me.INDlbBudgetaryEntity.Text = "Entidad Presupuestal"
        '
        'INDlbItemStatus
        '
        Me.INDlbItemStatus.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.INDlbItemStatus.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbItemStatus.Location = New System.Drawing.Point(146, 22)
        Me.INDlbItemStatus.Name = "INDlbItemStatus"
        Me.INDlbItemStatus.Size = New System.Drawing.Size(35, 13)
        Me.INDlbItemStatus.TabIndex = 2
        Me.INDlbItemStatus.Text = "Estado"
        '
        'INDlbItemValidity
        '
        Me.INDlbItemValidity.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.INDlbItemValidity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbItemValidity.Location = New System.Drawing.Point(8, 22)
        Me.INDlbItemValidity.Name = "INDlbItemValidity"
        Me.INDlbItemValidity.Size = New System.Drawing.Size(44, 13)
        Me.INDlbItemValidity.TabIndex = 0
        Me.INDlbItemValidity.Text = "Vigencia"
        '
        'INDlbValidity
        '
        Me.INDlbValidity.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.INDlbValidity.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlbValidity.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlbValidity.Location = New System.Drawing.Point(62, 21)
        Me.INDlbValidity.Name = "INDlbValidity"
        Me.INDlbValidity.Size = New System.Drawing.Size(56, 18)
        Me.INDlbValidity.TabIndex = 1
        Me.INDlbValidity.Text = "Vigencia"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.PanelControl1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(292, 62)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'CtrInfoBudgetEntry
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrInfoBudgetEntry"
        Me.Size = New System.Drawing.Size(292, 62)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.INDpbValues.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpbValues As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents INDlbValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbBudgetaryEntity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbItemStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbItemValidity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlbValidity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
