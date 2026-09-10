<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrInventoryProduct
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDteMeasurementUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDsbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleInventoryProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvInventory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgvInventoryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvInventoryATCCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvInventoryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvInventoryProductType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgvInventoryClassName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDseQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciInventoryProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciMeasurementUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDlcRoot.SuspendLayout
        CType(Me.INDteMeasurementUnit.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDsleInventoryProduct.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDgvInventory,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDseQuantity.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciInventoryProduct,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciQuantity,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDlciMeasurementUnit,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDteMeasurementUnit)
        Me.INDlcRoot.Controls.Add(Me.INDsbAdd)
        Me.INDlcRoot.Controls.Add(Me.INDsleInventoryProduct)
        Me.INDlcRoot.Controls.Add(Me.INDseQuantity)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(411, 240)
        Me.INDlcRoot.TabIndex = 0
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDteMeasurementUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteMeasurementUnit, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteMeasurementUnit, false)
        Me.INDteMeasurementUnit.Location = New System.Drawing.Point(12, 94)
        Me.IndigoTextEdit1.SetMascara(Me.INDteMeasurementUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteMeasurementUnit.Name = "INDteMeasurementUnit"
        Me.INDteMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteMeasurementUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDteMeasurementUnit.Properties.Appearance.Options.UseBackColor = true
        Me.INDteMeasurementUnit.Properties.Appearance.Options.UseFont = true
        Me.INDteMeasurementUnit.Properties.ReadOnly = true
        Me.INDteMeasurementUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDteMeasurementUnit.StyleController = Me.INDlcRoot
        Me.INDteMeasurementUnit.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteMeasurementUnit, 0)
        '
        'INDsbAdd
        '
        Me.INDsbAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10!)
        Me.INDsbAdd.Appearance.Options.UseFont = true
        Me.INDsbAdd.Location = New System.Drawing.Point(12, 196)
        Me.INDsbAdd.Name = "INDsbAdd"
        Me.INDsbAdd.Size = New System.Drawing.Size(386, 32)
        Me.INDsbAdd.StyleController = Me.INDlcRoot
        Me.INDsbAdd.TabIndex = 7
        Me.INDsbAdd.Text = "Agregar"
        '
        'INDsleInventoryProduct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleInventoryProduct, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleInventoryProduct, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleInventoryProduct, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.INDsleInventoryProduct.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.INDsleInventoryProduct.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleInventoryProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleInventoryProduct.Name = "INDsleInventoryProduct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.INDsleInventoryProduct.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleInventoryProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDsleInventoryProduct.Properties.Appearance.Options.UseBackColor = true
        Me.INDsleInventoryProduct.Properties.Appearance.Options.UseFont = true
        Me.INDsleInventoryProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleInventoryProduct.Properties.DisplayMember = "CodeName"
        Me.INDsleInventoryProduct.Properties.NullText = ""
        Me.INDsleInventoryProduct.Properties.PopupSizeable = false
        Me.INDsleInventoryProduct.Properties.ShowFooter = false
        Me.INDsleInventoryProduct.Properties.ValueMember = "Id"
        Me.INDsleInventoryProduct.Properties.View = Me.INDgvInventory
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleInventoryProduct, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleInventoryProduct, true)
        Me.INDsleInventoryProduct.Size = New System.Drawing.Size(386, 28)
        Me.INDsleInventoryProduct.StyleController = Me.INDlcRoot
        Me.INDsleInventoryProduct.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleInventoryProduct, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleInventoryProduct, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleInventoryProduct, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleInventoryProduct, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleInventoryProduct, false)
        '
        'INDgvInventory
        '
        Me.INDgvInventory.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvInventory.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseBorderColor = true
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseFont = true
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseForeColor = true
        Me.INDgvInventory.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvInventory.Appearance.GroupRow.Options.UseFont = true
        Me.INDgvInventory.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDgvInventory.Appearance.HeaderPanel.Options.UseFont = true
        Me.INDgvInventory.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvInventory.Appearance.Row.Options.UseFont = true
        Me.INDgvInventory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgvInventoryCode, Me.INDgvInventoryATCCode, Me.INDgvInventoryName, Me.INDgvInventoryProductType, Me.INDgvInventoryClassName})
        Me.INDgvInventory.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvInventory.Name = "INDgvInventory"
        Me.INDgvInventory.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDgvInventory.OptionsView.EnableAppearanceEvenRow = true
        Me.INDgvInventory.OptionsView.EnableAppearanceOddRow = true
        Me.INDgvInventory.OptionsView.ShowAutoFilterRow = true
        Me.INDgvInventory.OptionsView.ShowGroupPanel = false
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvInventory, false)
        '
        'INDgvInventoryCode
        '
        Me.INDgvInventoryCode.Caption = "Código"
        Me.INDgvInventoryCode.FieldName = "Code"
        Me.INDgvInventoryCode.Name = "INDgvInventoryCode"
        Me.INDgvInventoryCode.Visible = true
        Me.INDgvInventoryCode.VisibleIndex = 0
        '
        'INDgvInventoryATCCode
        '
        Me.INDgvInventoryATCCode.Caption = "Código ATC"
        Me.INDgvInventoryATCCode.FieldName = "ATCId.Cde"
        Me.INDgvInventoryATCCode.Name = "INDgvInventoryATCCode"
        Me.INDgvInventoryATCCode.Visible = true
        Me.INDgvInventoryATCCode.VisibleIndex = 1
        '
        'INDgvInventoryName
        '
        Me.INDgvInventoryName.Caption = "Nombre"
        Me.INDgvInventoryName.FieldName = "Name"
        Me.INDgvInventoryName.Name = "INDgvInventoryName"
        Me.INDgvInventoryName.Visible = true
        Me.INDgvInventoryName.VisibleIndex = 2
        '
        'INDgvInventoryProductType
        '
        Me.INDgvInventoryProductType.Caption = "Tipo"
        Me.INDgvInventoryProductType.FieldName = "ProductTypeId.Name"
        Me.INDgvInventoryProductType.Name = "INDgvInventoryProductType"
        Me.INDgvInventoryProductType.Visible = true
        Me.INDgvInventoryProductType.VisibleIndex = 3
        '
        'INDgvInventoryClassName
        '
        Me.INDgvInventoryClassName.Caption = "Clase"
        Me.INDgvInventoryClassName.FieldName = "ClassName"
        Me.INDgvInventoryClassName.Name = "INDgvInventoryClassName"
        Me.INDgvInventoryClassName.Visible = true
        Me.INDgvInventoryClassName.VisibleIndex = 4
        '
        'INDseQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseQuantity, false)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseQuantity, true)
        Me.INDseQuantity.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDseQuantity.EnterMoveNextControl = true
        Me.INDseQuantity.Location = New System.Drawing.Point(12, 162)
        Me.IndigoTextEdit1.SetMascara(Me.INDseQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseQuantity.Name = "INDseQuantity"
        Me.INDseQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseQuantity.Properties.Appearance.Options.UseBackColor = true
        Me.INDseQuantity.Properties.Appearance.Options.UseFont = true
        Me.INDseQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215,Byte),Integer), CType(CType(242,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.INDseQuantity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0,Byte),Integer), CType(CType(148,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.INDseQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseBackColor = true
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseBorderColor = true
        Me.INDseQuantity.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDseQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseQuantity.Properties.DisplayFormat.FormatString = "f6"
        Me.INDseQuantity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDseQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDseQuantity.Properties.Mask.EditMask = "f6"
        Me.INDseQuantity.Properties.Mask.UseMaskAsDisplayFormat = true
        Me.INDseQuantity.Properties.MaxLength = 24
        Me.INDseQuantity.Properties.MaxValue = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.INDseQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDseQuantity.StyleController = Me.INDlcRoot
        Me.INDseQuantity.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseQuantity, 0)
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = true
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, false)
        Me.INDlcgRoot.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = false
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciInventoryProduct, Me.INDlciAdd, Me.INDlciQuantity, Me.INDlciMeasurementUnit})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(411, 240)
        Me.INDlcgRoot.TextVisible = false
        '
        'INDlciInventoryProduct
        '
        Me.INDlciInventoryProduct.AllowHide = false
        Me.INDlciInventoryProduct.Control = Me.INDsleInventoryProduct
        Me.INDlciInventoryProduct.CustomizationFormText = "Producto"
        Me.INDlciInventoryProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDlciInventoryProduct.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciInventoryProduct.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciInventoryProduct.Name = "INDlciInventoryProduct"
        Me.INDlciInventoryProduct.ShowInCustomizationForm = false
        Me.INDlciInventoryProduct.Size = New System.Drawing.Size(391, 62)
        Me.INDlciInventoryProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciInventoryProduct.Text = "Producto"
        Me.INDlciInventoryProduct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciInventoryProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciInventoryProduct.TextSize = New System.Drawing.Size(96, 21)
        Me.INDlciInventoryProduct.TextToControlDistance = 5
        '
        'INDlciAdd
        '
        Me.INDlciAdd.Control = Me.INDsbAdd
        Me.INDlciAdd.CustomizationFormText = "LayoutControlItem1"
        Me.INDlciAdd.Location = New System.Drawing.Point(0, 184)
        Me.INDlciAdd.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciAdd.Name = "INDlciAdd"
        Me.INDlciAdd.Size = New System.Drawing.Size(391, 36)
        Me.INDlciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAdd.TextToControlDistance = 0
        Me.INDlciAdd.TextVisible = false
        '
        'INDlciQuantity
        '
        Me.INDlciQuantity.AllowHide = false
        Me.INDlciQuantity.Control = Me.INDseQuantity
        Me.INDlciQuantity.CustomizationFormText = "Cantidad Equivalente"
        Me.INDlciQuantity.Location = New System.Drawing.Point(0, 124)
        Me.INDlciQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlciQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlciQuantity.Name = "INDlciQuantity"
        Me.INDlciQuantity.ShowInCustomizationForm = false
        Me.INDlciQuantity.Size = New System.Drawing.Size(391, 60)
        Me.INDlciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciQuantity.Text = "Cantidad Equivalente"
        Me.INDlciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciQuantity.TextToControlDistance = 5
        '
        'INDlciMeasurementUnit
        '
        Me.INDlciMeasurementUnit.Control = Me.INDteMeasurementUnit
        Me.INDlciMeasurementUnit.Location = New System.Drawing.Point(0, 62)
        Me.INDlciMeasurementUnit.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciMeasurementUnit.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciMeasurementUnit.Name = "INDlciMeasurementUnit"
        Me.INDlciMeasurementUnit.Size = New System.Drawing.Size(391, 62)
        Me.INDlciMeasurementUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciMeasurementUnit.Text = "Unidad de Medida"
        Me.INDlciMeasurementUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciMeasurementUnit.TextSize = New System.Drawing.Size(117, 17)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = true
        '
        'CtrInventoryProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlcRoot)
        Me.Name = "CtrInventoryProduct"
        Me.Size = New System.Drawing.Size(411, 240)
        CType(Me.INDlcRoot,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDlcRoot.ResumeLayout(false)
        CType(Me.INDteMeasurementUnit.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDsleInventoryProduct.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDgvInventory,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDseQuantity.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlcgRoot,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciInventoryProduct,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciAdd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciQuantity,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDlciMeasurementUnit,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGroupControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDsleInventoryProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvInventory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciInventoryProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents INDseQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgvInventoryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvInventoryATCCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvInventoryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvInventoryProductType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgvInventoryClassName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDteMeasurementUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciMeasurementUnit As DevExpress.XtraLayout.LayoutControlItem
End Class
