<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrPhysicalInventory
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
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcPhysicalInventory = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPhysicalInventory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSeQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDTxtManufacturer = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtProduct = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPhysicalInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtManufacturer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcPhysicalInventory)
        Me.LayoutControl1.Controls.Add(Me.INDTxtManufacturer)
        Me.LayoutControl1.Controls.Add(Me.INDTxtProduct)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(560, 468)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcPhysicalInventory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcPhysicalInventory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcPhysicalInventory, Nothing)
        Me.INDGcPhysicalInventory.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcPhysicalInventory, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcPhysicalInventory, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcPhysicalInventory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcPhysicalInventory, False)
        Me.INDGcPhysicalInventory.Location = New System.Drawing.Point(12, 84)
        Me.INDGcPhysicalInventory.MainView = Me.INDGvPhysicalInventory
        Me.INDGcPhysicalInventory.Name = "INDGcPhysicalInventory"
        Me.INDGcPhysicalInventory.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptSeQuantity})
        Me.INDGcPhysicalInventory.Size = New System.Drawing.Size(536, 372)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcPhysicalInventory, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcPhysicalInventory.TabIndex = 7
        Me.INDGcPhysicalInventory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPhysicalInventory})
        '
        'INDGvPhysicalInventory
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDGvPhysicalInventory.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvPhysicalInventory.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvPhysicalInventory.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvPhysicalInventory.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGvPhysicalInventory.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGvPhysicalInventory.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPhysicalInventory.Appearance.Row.Options.UseFont = True
        Me.INDGvPhysicalInventory.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPhysicalInventory.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvPhysicalInventory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.INDGvPhysicalInventory.GridControl = Me.INDGcPhysicalInventory
        Me.INDGvPhysicalInventory.Name = "INDGvPhysicalInventory"
        Me.INDGvPhysicalInventory.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPhysicalInventory.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPhysicalInventory.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPhysicalInventory.OptionsView.ShowDetailButtons = False
        Me.INDGvPhysicalInventory.OptionsView.ShowFooter = True
        Me.INDGvPhysicalInventory.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPhysicalInventory, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Almacen"
        Me.GridColumn1.FieldName = "CodeNameWarehouse"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Lote"
        Me.GridColumn2.FieldName = "CodeNameBatchSerial"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Vencimiento"
        Me.GridColumn3.FieldName = "BatchSerialExpiredDate"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Existencia"
        Me.GridColumn4.FieldName = "Quantity"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Cantidad"
        Me.GridColumn5.ColumnEdit = Me.INDRptSeQuantity
        Me.GridColumn5.FieldName = "QuantityDeliver"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QuantityDeliver", "Total: {0}")})
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'INDRptSeQuantity
        '
        Me.INDRptSeQuantity.AutoHeight = False
        Me.INDRptSeQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptSeQuantity.Mask.EditMask = "[0-9]+"
        Me.INDRptSeQuantity.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDRptSeQuantity.MaxLength = 5
        Me.INDRptSeQuantity.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDRptSeQuantity.Name = "INDRptSeQuantity"
        '
        'INDTxtManufacturer
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtManufacturer, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtManufacturer, False)
        Me.INDTxtManufacturer.Location = New System.Drawing.Point(117, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtManufacturer, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtManufacturer.Name = "INDTxtManufacturer"
        Me.INDTxtManufacturer.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtManufacturer.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtManufacturer.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtManufacturer.Properties.Appearance.Options.UseFont = True
        Me.INDTxtManufacturer.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtManufacturer.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtManufacturer.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtManufacturer.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtManufacturer.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtManufacturer.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtManufacturer.Properties.ReadOnly = True
        Me.INDTxtManufacturer.Size = New System.Drawing.Size(431, 28)
        Me.INDTxtManufacturer.StyleController = Me.LayoutControl1
        Me.INDTxtManufacturer.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtManufacturer, 0)
        '
        'INDTxtProduct
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtProduct, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtProduct, False)
        Me.INDTxtProduct.Location = New System.Drawing.Point(117, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtProduct.Name = "INDTxtProduct"
        Me.INDTxtProduct.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtProduct.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProduct.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtProduct.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtProduct.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProduct.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtProduct.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtProduct.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtProduct.Properties.ReadOnly = True
        Me.INDTxtProduct.Size = New System.Drawing.Size(431, 28)
        Me.INDTxtProduct.StyleController = Me.LayoutControl1
        Me.INDTxtProduct.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtProduct, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(560, 468)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDTxtProduct
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(126, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(540, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Producto"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.[Default]
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDTxtManufacturer
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(126, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(540, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Fabricante"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.[Default]
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 21)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDGcPhysicalInventory
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(540, 376)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20141229"

        '
        'CtrPhysicalInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrPhysicalInventory"
        Me.Size = New System.Drawing.Size(560, 468)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcPhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPhysicalInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtManufacturer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtProduct As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDGcPhysicalInventory As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvPhysicalInventory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTxtManufacturer As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptSeQuantity As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit

End Class
