Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrTotalVoucher
    Inherits XtraUserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgAdvance = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlblAdvanceValue = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlblValueIVA = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblXMilValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblValueNet = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDlcgAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 341
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cuenta"
        Me.GridColumn3.FieldName = "FullNameMainAccount"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 514
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Valor"
        Me.GridColumn4.FieldName = "Balance"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 519
        '
        'INDlcgAdvance
        '
        Me.INDlcgAdvance.AppearanceGroup.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.INDlcgAdvance.AppearanceGroup.Options.UseFont = True
        Me.INDlcgAdvance.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgAdvance.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgAdvance.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgAdvance.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgAdvance.GroupBordersVisible = False
        Me.INDlcgAdvance.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem1, Me.LayoutControlItem5})
        Me.INDlcgAdvance.Name = "Root"
        Me.INDlcgAdvance.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlcgAdvance.Size = New System.Drawing.Size(292, 82)
        Me.INDlcgAdvance.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LayoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem2.Control = Me.INDlblAdvanceValue
        Me.LayoutControlItem2.CustomizationFormText = "Valor Neto"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(272, 26)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(272, 26)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(292, 26)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Total"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(80, 13)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'INDlblAdvanceValue
        '
        Me.INDlblAdvanceValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDlblAdvanceValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblAdvanceValue.Appearance.Options.UseFont = True
        Me.INDlblAdvanceValue.Appearance.Options.UseForeColor = True
        Me.INDlblAdvanceValue.Appearance.Options.UseTextOptions = True
        Me.INDlblAdvanceValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblAdvanceValue.Location = New System.Drawing.Point(95, 0)
        Me.INDlblAdvanceValue.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblAdvanceValue.Name = "INDlblAdvanceValue"
        Me.INDlblAdvanceValue.Size = New System.Drawing.Size(177, 26)
        Me.INDlblAdvanceValue.StyleController = Me.LayoutControl1
        Me.INDlblAdvanceValue.TabIndex = 4
        Me.INDlblAdvanceValue.Text = "$0"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDlblValueIVA)
        Me.LayoutControl1.Controls.Add(Me.INDlblAdvanceValue)
        Me.LayoutControl1.Controls.Add(Me.INDlblXMilValue)
        Me.LayoutControl1.Controls.Add(Me.INDlblValueNet)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(349, 148, 1024, 817)
        Me.LayoutControl1.Root = Me.INDlcgAdvance
        Me.LayoutControl1.Size = New System.Drawing.Size(292, 82)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlblValueIVA
        '
        Me.INDlblValueIVA.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.INDlblValueIVA.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDlblValueIVA.Appearance.Options.UseFont = True
        Me.INDlblValueIVA.Appearance.Options.UseForeColor = True
        Me.INDlblValueIVA.Appearance.Options.UseTextOptions = True
        Me.INDlblValueIVA.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblValueIVA.Location = New System.Drawing.Point(95, 60)
        Me.INDlblValueIVA.Name = "INDlblValueIVA"
        Me.INDlblValueIVA.Size = New System.Drawing.Size(177, 17)
        Me.INDlblValueIVA.StyleController = Me.LayoutControl1
        Me.INDlblValueIVA.TabIndex = 7
        Me.INDlblValueIVA.Text = "$0"
        '
        'INDlblXMilValue
        '
        Me.INDlblXMilValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.INDlblXMilValue.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblXMilValue.Appearance.Options.UseFont = True
        Me.INDlblXMilValue.Appearance.Options.UseForeColor = True
        Me.INDlblXMilValue.Appearance.Options.UseTextOptions = True
        Me.INDlblXMilValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblXMilValue.Location = New System.Drawing.Point(95, 43)
        Me.INDlblXMilValue.Name = "INDlblXMilValue"
        Me.INDlblXMilValue.Size = New System.Drawing.Size(177, 17)
        Me.INDlblXMilValue.StyleController = Me.LayoutControl1
        Me.INDlblXMilValue.TabIndex = 5
        Me.INDlblXMilValue.Text = "$0"
        '
        'INDlblValueNet
        '
        Me.INDlblValueNet.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.INDlblValueNet.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDlblValueNet.Appearance.Options.UseFont = True
        Me.INDlblValueNet.Appearance.Options.UseForeColor = True
        Me.INDlblValueNet.Appearance.Options.UseTextOptions = True
        Me.INDlblValueNet.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDlblValueNet.Location = New System.Drawing.Point(95, 26)
        Me.INDlblValueNet.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblValueNet.Name = "INDlblValueNet"
        Me.INDlblValueNet.Size = New System.Drawing.Size(177, 17)
        Me.INDlblValueNet.StyleController = Me.LayoutControl1
        Me.INDlblValueNet.TabIndex = 4
        Me.INDlblValueNet.Text = "$0"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.INDlblXMilValue
        Me.LayoutControlItem3.CustomizationFormText = "Tasa X Mil"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 43)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(272, 17)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(272, 17)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(292, 17)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Tasa X Mil"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(80, 17)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem1.Control = Me.INDlblValueNet
        Me.LayoutControlItem1.CustomizationFormText = "Valor Neto"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 26)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(272, 17)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(272, 17)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(292, 17)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Valor Neto"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(80, 17)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.Control = Me.INDlblValueIVA
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(272, 17)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(272, 17)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.LayoutControlItem5.Size = New System.Drawing.Size(292, 22)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "IVA"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(80, 17)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'CtrTotalVoucher
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 62)
        Me.MinimumSize = New System.Drawing.Size(292, 82)
        Me.Name = "CtrTotalVoucher"
        Me.Size = New System.Drawing.Size(292, 82)
        CType(Me.INDlcgAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlcgAdvance As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblAdvanceValue As LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlblXMilValue As LabelControl
    Friend WithEvents INDlblValueNet As LabelControl
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblValueIVA As LabelControl
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
End Class
