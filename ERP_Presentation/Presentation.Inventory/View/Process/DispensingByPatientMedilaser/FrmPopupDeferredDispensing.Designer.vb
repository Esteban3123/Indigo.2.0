<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupDeferredDispensing
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
        Me.INDseTotalQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDbtnDeferred = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsePeriodicity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseQuantityDelivery = New DevExpress.XtraEditors.SpinEdit()
        Me.INDdteDateFirstDelivery = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDBtnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcDeferred = New DevExpress.XtraGrid.GridControl()
        Me.INDviewDeferred = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSeQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDeferred = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDateFirstDelivery = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemQuantityDelivery = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPeriodicity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBtnDeferred = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTotalQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDseTotalQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsePeriodicity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseQuantityDelivery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDateFirstDelivery.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDateFirstDelivery.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDeferred, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewDeferred, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDeferred, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDateFirstDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemQuantityDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPeriodicity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBtnDeferred, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDseTotalQuantity)
        Me.LayoutControl1.Controls.Add(Me.INDbtnDeferred)
        Me.LayoutControl1.Controls.Add(Me.INDsePeriodicity)
        Me.LayoutControl1.Controls.Add(Me.INDseQuantityDelivery)
        Me.LayoutControl1.Controls.Add(Me.INDdteDateFirstDelivery)
        Me.LayoutControl1.Controls.Add(Me.LabelControl1)
        Me.LayoutControl1.Controls.Add(Me.INDBtnOk)
        Me.LayoutControl1.Controls.Add(Me.INDgcDeferred)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(370, 75, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(930, 492)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDseTotalQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseTotalQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseTotalQuantity, False)
        Me.INDseTotalQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseTotalQuantity.Enabled = False
        Me.INDseTotalQuantity.Location = New System.Drawing.Point(659, 74)
        Me.IndigoTextEdit1.SetMascara(Me.INDseTotalQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDseTotalQuantity.Name = "INDseTotalQuantity"
        Me.INDseTotalQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseTotalQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseTotalQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseTotalQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseTotalQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseTotalQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseTotalQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseTotalQuantity.Size = New System.Drawing.Size(251, 28)
        Me.INDseTotalQuantity.StyleController = Me.LayoutControl1
        Me.INDseTotalQuantity.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseTotalQuantity, 0)
        '
        'INDbtnDeferred
        '
        Me.INDbtnDeferred.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnDeferred.Appearance.Options.UseFont = True
        Me.INDbtnDeferred.Location = New System.Drawing.Point(14, 146)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnDeferred, True)
        Me.INDbtnDeferred.Name = "INDbtnDeferred"
        Me.INDbtnDeferred.Size = New System.Drawing.Size(902, 32)
        Me.INDbtnDeferred.StyleController = Me.LayoutControl1
        Me.INDbtnDeferred.TabIndex = 4
        Me.INDbtnDeferred.Text = "Diferir"
        '
        'INDsePeriodicity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsePeriodicity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsePeriodicity, False)
        Me.INDsePeriodicity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDsePeriodicity.EnterMoveNextControl = True
        Me.INDsePeriodicity.Location = New System.Drawing.Point(659, 110)
        Me.IndigoTextEdit1.SetMascara(Me.INDsePeriodicity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDsePeriodicity.Name = "INDsePeriodicity"
        Me.INDsePeriodicity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsePeriodicity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsePeriodicity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsePeriodicity.Properties.Appearance.Options.UseFont = True
        Me.INDsePeriodicity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsePeriodicity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsePeriodicity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsePeriodicity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDsePeriodicity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDsePeriodicity.Size = New System.Drawing.Size(251, 28)
        Me.INDsePeriodicity.StyleController = Me.LayoutControl1
        Me.INDsePeriodicity.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsePeriodicity, 0)
        '
        'INDseQuantityDelivery
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseQuantityDelivery, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseQuantityDelivery, False)
        Me.INDseQuantityDelivery.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseQuantityDelivery.EnterMoveNextControl = True
        Me.INDseQuantityDelivery.Location = New System.Drawing.Point(209, 110)
        Me.IndigoTextEdit1.SetMascara(Me.INDseQuantityDelivery, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseQuantityDelivery.Name = "INDseQuantityDelivery"
        Me.INDseQuantityDelivery.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseQuantityDelivery.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseQuantityDelivery.Properties.Appearance.Options.UseBackColor = True
        Me.INDseQuantityDelivery.Properties.Appearance.Options.UseFont = True
        Me.INDseQuantityDelivery.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseQuantityDelivery.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseQuantityDelivery.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseQuantityDelivery.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseQuantityDelivery.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseQuantityDelivery.Size = New System.Drawing.Size(251, 28)
        Me.INDseQuantityDelivery.StyleController = Me.LayoutControl1
        Me.INDseQuantityDelivery.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseQuantityDelivery, 0)
        '
        'INDdteDateFirstDelivery
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDateFirstDelivery, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDateFirstDelivery, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteDateFirstDelivery, False)
        Me.INDdteDateFirstDelivery.EditValue = Nothing
        Me.INDdteDateFirstDelivery.Enabled = False
        Me.INDdteDateFirstDelivery.Location = New System.Drawing.Point(209, 74)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDateFirstDelivery, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteDateFirstDelivery, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteDateFirstDelivery.Name = "INDdteDateFirstDelivery"
        Me.INDdteDateFirstDelivery.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteDateFirstDelivery.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDateFirstDelivery.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDateFirstDelivery.Properties.Appearance.Options.UseFont = True
        Me.INDdteDateFirstDelivery.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDateFirstDelivery.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDateFirstDelivery.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDateFirstDelivery.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDateFirstDelivery.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteDateFirstDelivery.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDateFirstDelivery.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDateFirstDelivery.Size = New System.Drawing.Size(251, 28)
        Me.INDdteDateFirstDelivery.StyleController = Me.LayoutControl1
        Me.INDdteDateFirstDelivery.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDateFirstDelivery, 0)
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.LabelControl1.Location = New System.Drawing.Point(15, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(96, 21)
        Me.LabelControl1.StyleController = Me.LayoutControl1
        Me.LabelControl1.TabIndex = 7
        Me.LabelControl1.Text = "Medicamento:"
        '
        'INDBtnOk
        '
        Me.INDBtnOk.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnOk.Appearance.Options.UseFont = True
        Me.INDBtnOk.Location = New System.Drawing.Point(14, 446)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnOk, True)
        Me.INDBtnOk.Name = "INDBtnOk"
        Me.INDBtnOk.Size = New System.Drawing.Size(902, 32)
        Me.INDBtnOk.StyleController = Me.LayoutControl1
        Me.INDBtnOk.TabIndex = 6
        Me.INDBtnOk.Text = "Aceptar"
        '
        'INDgcDeferred
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDeferred, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDeferred, Nothing)
        Me.INDgcDeferred.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDeferred, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDeferred, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDeferred, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDeferred, False)
        Me.INDgcDeferred.Location = New System.Drawing.Point(14, 182)
        Me.INDgcDeferred.MainView = Me.INDviewDeferred
        Me.INDgcDeferred.Name = "INDgcDeferred"
        Me.INDgcDeferred.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptSeQuantity})
        Me.INDgcDeferred.Size = New System.Drawing.Size(902, 260)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDeferred, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDeferred.TabIndex = 5
        Me.INDgcDeferred.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewDeferred})
        '
        'INDviewDeferred
        '
        Me.INDviewDeferred.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewDeferred.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewDeferred.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDviewDeferred.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewDeferred.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewDeferred.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDeferred.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewDeferred.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDeferred.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewDeferred.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewDeferred.Appearance.Row.Options.UseFont = True
        Me.INDviewDeferred.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewDeferred.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewDeferred.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.INDviewDeferred.GridControl = Me.INDgcDeferred
        Me.INDviewDeferred.Name = "INDviewDeferred"
        Me.INDviewDeferred.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewDeferred.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewDeferred.OptionsView.ShowAutoFilterRow = True
        Me.INDviewDeferred.OptionsView.ShowDetailButtons = False
        Me.INDviewDeferred.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewDeferred, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "No."
        Me.GridColumn1.FieldName = "Number"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 71
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Fecha Entrega"
        Me.GridColumn2.FieldName = "DeliveryDate"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 258
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cantidad a Entregar"
        Me.GridColumn3.FieldName = "DeliveryQuantity"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 275
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cantidad Pendiente"
        Me.GridColumn4.FieldName = "PendingQuantity"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 280
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
        Me.LayoutControlGroup1.CustomizationFormText = "Homologaciones"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMain, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(930, 492)
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
        Me.INDLcgMain.CustomizationFormText = "Homologación"
        Me.INDLcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDeferred, Me.INDLciAdd, Me.INDlyItemDateFirstDelivery, Me.INDlyItemQuantityDelivery, Me.INDlyItemPeriodicity, Me.INDlyItemBtnDeferred, Me.INDlyItemTotalQuantity})
        Me.INDLcgMain.Location = New System.Drawing.Point(0, 25)
        Me.INDLcgMain.Name = "INDLcgMain"
        Me.INDLcgMain.ShowInCustomizationForm = False
        Me.INDLcgMain.Size = New System.Drawing.Size(930, 467)
        Me.INDLcgMain.Text = "Medicamento"
        '
        'INDlyItemDeferred
        '
        Me.INDlyItemDeferred.Control = Me.INDgcDeferred
        Me.INDlyItemDeferred.CustomizationFormText = "LayoutControlItem1"
        Me.INDlyItemDeferred.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemDeferred.Name = "INDlyItemDeferred"
        Me.INDlyItemDeferred.Size = New System.Drawing.Size(906, 264)
        Me.INDlyItemDeferred.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemDeferred.TextVisible = False
        '
        'INDLciAdd
        '
        Me.INDLciAdd.Control = Me.INDBtnOk
        Me.INDLciAdd.CustomizationFormText = "LayoutControlItem2"
        Me.INDLciAdd.Location = New System.Drawing.Point(0, 372)
        Me.INDLciAdd.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLciAdd.MinSize = New System.Drawing.Size(83, 36)
        Me.INDLciAdd.Name = "INDLciAdd"
        Me.INDLciAdd.Size = New System.Drawing.Size(906, 36)
        Me.INDLciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdd.TextToControlDistance = 0
        Me.INDLciAdd.TextVisible = False
        '
        'INDlyItemDateFirstDelivery
        '
        Me.INDlyItemDateFirstDelivery.Control = Me.INDdteDateFirstDelivery
        Me.INDlyItemDateFirstDelivery.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemDateFirstDelivery.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemDateFirstDelivery.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemDateFirstDelivery.Name = "INDlyItemDateFirstDelivery"
        Me.INDlyItemDateFirstDelivery.Size = New System.Drawing.Size(450, 36)
        Me.INDlyItemDateFirstDelivery.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDateFirstDelivery.Text = "Fecha Primera Entrega"
        Me.INDlyItemDateFirstDelivery.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDateFirstDelivery.TextSize = New System.Drawing.Size(180, 21)
        Me.INDlyItemDateFirstDelivery.TextToControlDistance = 15
        '
        'INDlyItemQuantityDelivery
        '
        Me.INDlyItemQuantityDelivery.Control = Me.INDseQuantityDelivery
        Me.INDlyItemQuantityDelivery.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemQuantityDelivery.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemQuantityDelivery.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemQuantityDelivery.Name = "INDlyItemQuantityDelivery"
        Me.INDlyItemQuantityDelivery.Size = New System.Drawing.Size(450, 36)
        Me.INDlyItemQuantityDelivery.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemQuantityDelivery.Text = "Cantidad a Entregar"
        Me.INDlyItemQuantityDelivery.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemQuantityDelivery.TextSize = New System.Drawing.Size(180, 21)
        Me.INDlyItemQuantityDelivery.TextToControlDistance = 15
        '
        'INDlyItemPeriodicity
        '
        Me.INDlyItemPeriodicity.Control = Me.INDsePeriodicity
        Me.INDlyItemPeriodicity.Location = New System.Drawing.Point(450, 36)
        Me.INDlyItemPeriodicity.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemPeriodicity.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemPeriodicity.Name = "INDlyItemPeriodicity"
        Me.INDlyItemPeriodicity.Size = New System.Drawing.Size(456, 36)
        Me.INDlyItemPeriodicity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPeriodicity.Text = "Periodicidad Entrega (Días)"
        Me.INDlyItemPeriodicity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPeriodicity.TextSize = New System.Drawing.Size(180, 21)
        Me.INDlyItemPeriodicity.TextToControlDistance = 15
        '
        'INDlyItemBtnDeferred
        '
        Me.INDlyItemBtnDeferred.Control = Me.INDbtnDeferred
        Me.INDlyItemBtnDeferred.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemBtnDeferred.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemBtnDeferred.MinSize = New System.Drawing.Size(83, 36)
        Me.INDlyItemBtnDeferred.Name = "INDlyItemBtnDeferred"
        Me.INDlyItemBtnDeferred.Size = New System.Drawing.Size(906, 36)
        Me.INDlyItemBtnDeferred.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBtnDeferred.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemBtnDeferred.TextVisible = False
        '
        'INDlyItemTotalQuantity
        '
        Me.INDlyItemTotalQuantity.Control = Me.INDseTotalQuantity
        Me.INDlyItemTotalQuantity.Location = New System.Drawing.Point(450, 0)
        Me.INDlyItemTotalQuantity.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemTotalQuantity.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemTotalQuantity.Name = "INDlyItemTotalQuantity"
        Me.INDlyItemTotalQuantity.Size = New System.Drawing.Size(456, 36)
        Me.INDlyItemTotalQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalQuantity.Text = "Cantidad Total"
        Me.INDlyItemTotalQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTotalQuantity.TextSize = New System.Drawing.Size(180, 21)
        Me.INDlyItemTotalQuantity.TextToControlDistance = 15
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.LabelControl1
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(15, 2, 2, 2)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(930, 25)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmPopupDeferredDispensing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(930, 492)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupDeferredDispensing"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Diferir Cantidades"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDseTotalQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsePeriodicity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseQuantityDelivery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDateFirstDelivery.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDateFirstDelivery.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDeferred, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewDeferred, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptSeQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDeferred, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDateFirstDelivery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemQuantityDelivery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPeriodicity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBtnDeferred, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDBtnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgcDeferred As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewDeferred As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDRptSeQuantity As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemDeferred As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsePeriodicity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseQuantityDelivery As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDdteDateFirstDelivery As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemDateFirstDelivery As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemQuantityDelivery As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPeriodicity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Controls.IndigoDate
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbtnDeferred As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemBtnDeferred As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseTotalQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemTotalQuantity As DevExpress.XtraLayout.LayoutControlItem
End Class
