<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PopupCUMTransferOrder
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
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDseRequestQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemRequestQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblQuantity = New DevExpress.XtraEditors.LabelControl()
        Me.INDBtnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcCUM = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCUM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSeQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLabel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTableView = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDseRequestQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRequestQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcCUM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCUM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTableView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.TableLayoutPanel1)
        Me.LayoutControl1.Controls.Add(Me.INDLblQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDBtnOk)
        Me.LayoutControl1.Controls.Add(Me.INDGcCUM)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(370, 75, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(918, 366)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LayoutControl2, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(14, 79)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(890, 50)
        Me.TableLayoutPanel1.TabIndex = 9
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDseRequestQuantity)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(314, 3)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(261, 44)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDseRequestQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseRequestQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseRequestQuantity, False)
        Me.INDseRequestQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseRequestQuantity.Location = New System.Drawing.Point(142, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDseRequestQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseRequestQuantity.Name = "INDseRequestQuantity"
        Me.INDseRequestQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseRequestQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseRequestQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseRequestQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseRequestQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseRequestQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseRequestQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseRequestQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseRequestQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseRequestQuantity.Properties.MaxLength = 3
        Me.INDseRequestQuantity.Properties.MaxValue = New Decimal(New Integer() {999, 0, 0, 0})
        Me.INDseRequestQuantity.Size = New System.Drawing.Size(66, 28)
        Me.INDseRequestQuantity.StyleController = Me.LayoutControl2
        Me.INDseRequestQuantity.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseRequestQuantity, 0)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemRequestQuantity})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(261, 44)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemRequestQuantity
        '
        Me.INDlyItemRequestQuantity.Control = Me.INDseRequestQuantity
        Me.INDlyItemRequestQuantity.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemRequestQuantity.MaxSize = New System.Drawing.Size(210, 34)
        Me.INDlyItemRequestQuantity.MinSize = New System.Drawing.Size(210, 34)
        Me.INDlyItemRequestQuantity.Name = "INDlyItemRequestQuantity"
        Me.INDlyItemRequestQuantity.Size = New System.Drawing.Size(261, 44)
        Me.INDlyItemRequestQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRequestQuantity.Text = "Cantidad Solicitada"
        Me.INDlyItemRequestQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRequestQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRequestQuantity.TextToControlDistance = 5
        '
        'INDLblQuantity
        '
        Me.INDLblQuantity.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Underline)
        Me.INDLblQuantity.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDLblQuantity.Appearance.Options.UseFont = True
        Me.INDLblQuantity.Appearance.Options.UseForeColor = True
        Me.INDLblQuantity.Appearance.Options.UseTextOptions = True
        Me.INDLblQuantity.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLblQuantity.Location = New System.Drawing.Point(14, 43)
        Me.INDLblQuantity.Name = "INDLblQuantity"
        Me.INDLblQuantity.Size = New System.Drawing.Size(890, 32)
        Me.INDLblQuantity.StyleController = Me.LayoutControl1
        Me.INDLblQuantity.TabIndex = 6
        Me.INDLblQuantity.Text = "Cantidad Solicitada:"
        '
        'INDBtnOk
        '
        Me.INDBtnOk.Location = New System.Drawing.Point(14, 320)
        Me.INDBtnOk.Name = "INDBtnOk"
        Me.INDBtnOk.Size = New System.Drawing.Size(890, 32)
        Me.INDBtnOk.StyleController = Me.LayoutControl1
        Me.INDBtnOk.TabIndex = 5
        Me.INDBtnOk.Text = "Aceptar"
        '
        'INDGcCUM
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCUM, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCUM, Nothing)
        Me.INDGcCUM.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCUM, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCUM, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCUM, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCUM, False)
        Me.INDGcCUM.Location = New System.Drawing.Point(14, 133)
        Me.INDGcCUM.MainView = Me.INDGvCUM
        Me.INDGcCUM.Name = "INDGcCUM"
        Me.INDGcCUM.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptSeQuantity})
        Me.INDGcCUM.Size = New System.Drawing.Size(890, 183)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCUM, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcCUM.TabIndex = 4
        Me.INDGcCUM.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCUM})
        '
        'INDGvCUM
        '
        Me.INDGvCUM.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCUM.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCUM.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvCUM.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCUM.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCUM.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCUM.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCUM.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCUM.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCUM.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCUM.Appearance.Row.Options.UseFont = True
        Me.INDGvCUM.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvCUM.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvCUM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn3, Me.GridColumn5, Me.GridColumn2, Me.GridColumn4, Me.GridColumn6})
        Me.INDGvCUM.GridControl = Me.INDGcCUM
        Me.INDGvCUM.Name = "INDGvCUM"
        Me.INDGvCUM.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCUM.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCUM.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCUM.OptionsView.ShowDetailButtons = False
        Me.INDGvCUM.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCUM, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Producto"
        Me.GridColumn1.FieldName = "CodeNameProduct"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 136
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Vencimiento"
        Me.GridColumn2.FieldName = "BatchSerialExpiredDate"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        Me.GridColumn2.Width = 136
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Lote"
        Me.GridColumn3.FieldName = "CodeNameBatchSerial"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 135
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Almacen"
        Me.GridColumn5.FieldName = "CodeNameWarehouse"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 182
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "C. Disponible"
        Me.GridColumn4.FieldName = "Quantity"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 90
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "C. Entregada"
        Me.GridColumn6.ColumnEdit = Me.INDRptSeQuantity
        Me.GridColumn6.FieldName = "QuantityDeliver"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 92
        '
        'INDRptSeQuantity
        '
        Me.INDRptSeQuantity.AutoHeight = False
        Me.INDRptSeQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptSeQuantity.Mask.EditMask = "[0-9]+"
        Me.INDRptSeQuantity.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDRptSeQuantity.Mask.UseMaskAsDisplayFormat = True
        Me.INDRptSeQuantity.MaxLength = 5
        Me.INDRptSeQuantity.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDRptSeQuantity.Name = "INDRptSeQuantity"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Homologaciones"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMain})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(918, 366)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgMain
        '
        Me.INDLcgMain.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMain.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMain, False)
        Me.INDLcgMain.CustomizationFormText = "Homologación"
        Me.INDLcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLciAdd, Me.INDlyItemLabel, Me.INDlyItemTableView})
        Me.INDLcgMain.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMain.Name = "INDLcgMain"
        Me.INDLcgMain.ShowInCustomizationForm = False
        Me.INDLcgMain.Size = New System.Drawing.Size(918, 366)
        Me.INDLcgMain.Text = "Medicamento"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcCUM
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(894, 187)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDLciAdd
        '
        Me.INDLciAdd.Control = Me.INDBtnOk
        Me.INDLciAdd.CustomizationFormText = "LayoutControlItem2"
        Me.INDLciAdd.Location = New System.Drawing.Point(0, 277)
        Me.INDLciAdd.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLciAdd.MinSize = New System.Drawing.Size(83, 36)
        Me.INDLciAdd.Name = "INDLciAdd"
        Me.INDLciAdd.Size = New System.Drawing.Size(894, 36)
        Me.INDLciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdd.TextToControlDistance = 0
        Me.INDLciAdd.TextVisible = False
        '
        'INDlyItemLabel
        '
        Me.INDlyItemLabel.Control = Me.INDLblQuantity
        Me.INDlyItemLabel.CustomizationFormText = "LayoutControlItem3"
        Me.INDlyItemLabel.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemLabel.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemLabel.MinSize = New System.Drawing.Size(70, 36)
        Me.INDlyItemLabel.Name = "INDlyItemLabel"
        Me.INDlyItemLabel.Size = New System.Drawing.Size(894, 36)
        Me.INDlyItemLabel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLabel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemLabel.TextVisible = False
        '
        'INDlyItemTableView
        '
        Me.INDlyItemTableView.Control = Me.TableLayoutPanel1
        Me.INDlyItemTableView.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemTableView.Name = "INDlyItemTableView"
        Me.INDlyItemTableView.Size = New System.Drawing.Size(894, 54)
        Me.INDlyItemTableView.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemTableView.TextVisible = False
        Me.INDlyItemTableView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PopupCUMTransferOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(918, 366)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopupCUMTransferOrder"
        Me.ShowInTaskbar = False
        Me.Text = "C.U.M"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDseRequestQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRequestQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcCUM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCUM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTableView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcCUM As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvCUM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBtnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLblQuantity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptSeQuantity As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDlyItemLabel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents INDlyItemTableView As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDseRequestQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemRequestQuantity As DevExpress.XtraLayout.LayoutControlItem
End Class
