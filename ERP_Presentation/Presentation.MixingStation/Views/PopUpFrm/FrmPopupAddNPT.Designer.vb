<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupAddNPT
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMpbProgress = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.INDLcDeliveryTime = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleInventoryProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvProductionLine = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDspnInitialWeight = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspnEndWeight = New DevExpress.XtraEditors.SpinEdit()
        Me.INDLcgPccDeliveryTime = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciInventoryProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliInitialWeight = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliEndWeight = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDMpbProgress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcDeliveryTime.SuspendLayout()
        CType(Me.INDSleInventoryProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProductionLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnInitialWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnEndWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInventoryProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliInitialWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliEndWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAdd.Appearance.Options.UseFont = True
        Me.INDSbAdd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDSbAdd.Location = New System.Drawing.Point(0, 225)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(452, 36)
        Me.INDSbAdd.TabIndex = 1
        Me.INDSbAdd.Text = "Agregar"
        '
        'INDMpbProgress
        '
        Me.INDMpbProgress.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDMpbProgress.EditValue = 0
        Me.INDMpbProgress.Location = New System.Drawing.Point(0, 0)
        Me.INDMpbProgress.Name = "INDMpbProgress"
        Me.INDMpbProgress.Size = New System.Drawing.Size(452, 18)
        Me.INDMpbProgress.TabIndex = 2
        Me.INDMpbProgress.Visible = False
        '
        'INDLcDeliveryTime
        '
        Me.INDLcDeliveryTime.Controls.Add(Me.INDSleInventoryProduct)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDspnInitialWeight)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDspnEndWeight)
        Me.INDLcDeliveryTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcDeliveryTime.Location = New System.Drawing.Point(0, 18)
        Me.INDLcDeliveryTime.Name = "INDLcDeliveryTime"
        Me.INDLcDeliveryTime.Root = Me.INDLcgPccDeliveryTime
        Me.INDLcDeliveryTime.Size = New System.Drawing.Size(452, 207)
        Me.INDLcDeliveryTime.TabIndex = 3
        Me.INDLcDeliveryTime.Text = "LayoutControl1"
        '
        'INDSleInventoryProduct
        '
        Me.INDSleInventoryProduct.EnterMoveNextControl = True
        Me.INDSleInventoryProduct.Location = New System.Drawing.Point(12, 32)
        Me.INDSleInventoryProduct.Name = "INDSleInventoryProduct"
        Me.INDSleInventoryProduct.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleInventoryProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleInventoryProduct.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleInventoryProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleInventoryProduct.Properties.Appearance.Options.UseFont = True
        Me.INDSleInventoryProduct.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleInventoryProduct.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleInventoryProduct.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleInventoryProduct.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleInventoryProduct.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleInventoryProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleInventoryProduct.Properties.DisplayMember = "CodeName"
        Me.INDSleInventoryProduct.Properties.NullText = ""
        Me.INDSleInventoryProduct.Properties.PopupView = Me.INDGvProductionLine
        Me.INDSleInventoryProduct.Properties.ValueMember = "Id"
        Me.INDSleInventoryProduct.Size = New System.Drawing.Size(428, 28)
        Me.INDSleInventoryProduct.StyleController = Me.INDLcDeliveryTime
        Me.INDSleInventoryProduct.TabIndex = 3
        '
        'INDGvProductionLine
        '
        Me.INDGvProductionLine.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProductionLine.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProductionLine.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProductionLine.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProductionLine.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDGvProductionLine.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProductionLine.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDGvProductionLine.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProductionLine.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProductionLine.Appearance.Row.Options.UseFont = True
        Me.INDGvProductionLine.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6, Me.GridColumn1})
        Me.INDGvProductionLine.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProductionLine.Name = "INDGvProductionLine"
        Me.INDGvProductionLine.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProductionLine.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProductionLine.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProductionLine.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProductionLine.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 152
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Nombre"
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 632
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Clase"
        Me.GridColumn1.FieldName = "ProductTypeId.ClassName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 362
        '
        'INDspnInitialWeight
        '
        Me.INDspnInitialWeight.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnInitialWeight.EnterMoveNextControl = True
        Me.INDspnInitialWeight.Location = New System.Drawing.Point(12, 90)
        Me.INDspnInitialWeight.Name = "INDspnInitialWeight"
        Me.INDspnInitialWeight.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnInitialWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnInitialWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDspnInitialWeight.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnInitialWeight.Properties.Appearance.Options.UseFont = True
        Me.INDspnInitialWeight.Properties.Appearance.Options.UseForeColor = True
        Me.INDspnInitialWeight.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnInitialWeight.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnInitialWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnInitialWeight.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDspnInitialWeight.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnInitialWeight.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnInitialWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnInitialWeight.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspnInitialWeight.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnInitialWeight.Properties.Mask.EditMask = "f"
        Me.INDspnInitialWeight.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspnInitialWeight.Properties.MaxLength = 9
        Me.INDspnInitialWeight.Size = New System.Drawing.Size(428, 28)
        Me.INDspnInitialWeight.StyleController = Me.INDLcDeliveryTime
        Me.INDspnInitialWeight.TabIndex = 23
        '
        'INDspnEndWeight
        '
        Me.INDspnEndWeight.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnEndWeight.EnterMoveNextControl = True
        Me.INDspnEndWeight.Location = New System.Drawing.Point(12, 148)
        Me.INDspnEndWeight.Name = "INDspnEndWeight"
        Me.INDspnEndWeight.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnEndWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnEndWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDspnEndWeight.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnEndWeight.Properties.Appearance.Options.UseFont = True
        Me.INDspnEndWeight.Properties.Appearance.Options.UseForeColor = True
        Me.INDspnEndWeight.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnEndWeight.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnEndWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnEndWeight.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDspnEndWeight.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnEndWeight.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnEndWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnEndWeight.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspnEndWeight.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnEndWeight.Properties.Mask.EditMask = "f"
        Me.INDspnEndWeight.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspnEndWeight.Properties.MaxLength = 9
        Me.INDspnEndWeight.Size = New System.Drawing.Size(428, 28)
        Me.INDspnEndWeight.StyleController = Me.INDLcDeliveryTime
        Me.INDspnEndWeight.TabIndex = 23
        '
        'INDLcgPccDeliveryTime
        '
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgPccDeliveryTime.GroupBordersVisible = False
        Me.INDLcgPccDeliveryTime.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciInventoryProduct, Me.INDliInitialWeight, Me.INDliEndWeight})
        Me.INDLcgPccDeliveryTime.Name = "Root"
        Me.INDLcgPccDeliveryTime.Size = New System.Drawing.Size(452, 207)
        Me.INDLcgPccDeliveryTime.TextVisible = False
        '
        'INDLciInventoryProduct
        '
        Me.INDLciInventoryProduct.Control = Me.INDSleInventoryProduct
        Me.INDLciInventoryProduct.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciInventoryProduct.CustomizationFormText = "Línea de producción"
        Me.INDLciInventoryProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLciInventoryProduct.Name = "INDLciInventoryProduct"
        Me.INDLciInventoryProduct.Size = New System.Drawing.Size(432, 52)
        Me.INDLciInventoryProduct.Text = "Producto NPT"
        Me.INDLciInventoryProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciInventoryProduct.TextSize = New System.Drawing.Size(88, 17)
        '
        'INDliInitialWeight
        '
        Me.INDliInitialWeight.Control = Me.INDspnInitialWeight
        Me.INDliInitialWeight.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliInitialWeight.CustomizationFormText = "Peso Inicial"
        Me.INDliInitialWeight.Location = New System.Drawing.Point(0, 52)
        Me.INDliInitialWeight.Name = "INDliInitialWeight"
        Me.INDliInitialWeight.Size = New System.Drawing.Size(432, 58)
        Me.INDliInitialWeight.Text = "Peso Inicial"
        Me.INDliInitialWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliInitialWeight.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliInitialWeight.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliInitialWeight.TextToControlDistance = 5
        '
        'INDliEndWeight
        '
        Me.INDliEndWeight.Control = Me.INDspnEndWeight
        Me.INDliEndWeight.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliEndWeight.CustomizationFormText = "Peso Final"
        Me.INDliEndWeight.Location = New System.Drawing.Point(0, 110)
        Me.INDliEndWeight.Name = "INDliEndWeight"
        Me.INDliEndWeight.Size = New System.Drawing.Size(432, 77)
        Me.INDliEndWeight.Text = "Peso Final"
        Me.INDliEndWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliEndWeight.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliEndWeight.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliEndWeight.TextToControlDistance = 5
        '
        'FrmPopupAddNPT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(452, 261)
        Me.Controls.Add(Me.INDLcDeliveryTime)
        Me.Controls.Add(Me.INDMpbProgress)
        Me.Controls.Add(Me.INDSbAdd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupAddNPT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Agregar NPT"
        CType(Me.INDMpbProgress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcDeliveryTime.ResumeLayout(False)
        CType(Me.INDSleInventoryProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProductionLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnInitialWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnEndWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInventoryProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliInitialWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliEndWeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDMpbProgress As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents INDLcDeliveryTime As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSleInventoryProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProductionLine As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDspnInitialWeight As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDspnEndWeight As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLcgPccDeliveryTime As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciInventoryProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliInitialWeight As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliEndWeight As DevExpress.XtraLayout.LayoutControlItem
End Class
