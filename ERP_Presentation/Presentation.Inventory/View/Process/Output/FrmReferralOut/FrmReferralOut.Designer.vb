Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReferralOut
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReferralOut))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcRemissionOutput = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcProduct = New DevExpress.XtraGrid.GridControl()
        Me.INDGvProduct = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMeDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvWarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCustomer = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDteDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCustomer = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRemissionOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRemissionOutput.SuspendLayout()
        CType(Me.INDGcProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCustomer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRemissionOutput)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDLcRemissionOutput
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLcRemissionOutput
        '
        Me.INDLcRemissionOutput.AllowCustomization = False
        Me.INDLcRemissionOutput.Controls.Add(Me.INDBtnAdd)
        Me.INDLcRemissionOutput.Controls.Add(Me.INDGcProduct)
        Me.INDLcRemissionOutput.Controls.Add(Me.INDMeDetail)
        Me.INDLcRemissionOutput.Controls.Add(Me.INDSleWarehouse)
        Me.INDLcRemissionOutput.Controls.Add(Me.INDSleCustomer)
        Me.INDLcRemissionOutput.Controls.Add(Me.INDDteDate)
        Me.INDLcRemissionOutput.Controls.Add(Me.INDBteCode)
        resources.ApplyResources(Me.INDLcRemissionOutput, "INDLcRemissionOutput")
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRemissionOutput, False)
        Me.INDLcRemissionOutput.Name = "INDLcRemissionOutput"
        Me.INDLcRemissionOutput.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(384, 486, 445, 404)
        Me.INDLcRemissionOutput.Root = Me.LayoutControlGroup1
        '
        'INDBtnAdd
        '
        resources.ApplyResources(Me.INDBtnAdd, "INDBtnAdd")
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.StyleController = Me.INDLcRemissionOutput
        '
        'INDGcProduct
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcProduct, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcProduct, Nothing)
        Me.INDGcProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDGcProduct.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDGcProduct.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.IndigoGridControl1.SetExportButton(Me.INDGcProduct, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcProduct, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcProduct, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcProduct, False)
        resources.ApplyResources(Me.INDGcProduct, "INDGcProduct")
        Me.INDGcProduct.MainView = Me.INDGvProduct
        Me.INDGcProduct.Name = "INDGcProduct"
        Me.INDGcProduct.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1, Me.RepositoryItemTextEdit1})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcProduct, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcProduct.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvProduct})
        '
        'INDGvProduct
        '
        Me.INDGvProduct.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("INDGvProduct.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.INDGvProduct.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvProduct.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvProduct.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvProduct.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvProduct.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProduct.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvProduct.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvProduct.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProduct.Appearance.Row.Font = CType(resources.GetObject("INDGvProduct.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvProduct.Appearance.Row.Options.UseFont = True
        Me.INDGvProduct.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvProduct.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvProduct.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvProduct.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn9, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDGvProduct.GridControl = Me.INDGcProduct
        Me.INDGvProduct.Name = "INDGvProduct"
        Me.INDGvProduct.OptionsCustomization.AllowGroup = False
        Me.INDGvProduct.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvProduct.OptionsDetail.ShowDetailTabs = False
        Me.INDGvProduct.OptionsFind.AlwaysVisible = True
        Me.INDGvProduct.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProduct.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProduct.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProduct.OptionsView.ShowDetailButtons = False
        Me.INDGvProduct.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProduct, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "CodeNameProduct"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        '
        'GridColumn9
        '
        resources.ApplyResources(Me.GridColumn9, "GridColumn9")
        Me.GridColumn9.FieldName = "consumptionUnit"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "Quantity"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn3.FieldName = "VariationType"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemImageComboBox1
        '
        resources.ApplyResources(Me.RepositoryItemImageComboBox1, "RepositoryItemImageComboBox1")
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox1.Items"), CType(resources.GetObject("RepositoryItemImageComboBox1.Items1"), Object), CType(resources.GetObject("RepositoryItemImageComboBox1.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox1.Items3"), CType(resources.GetObject("RepositoryItemImageComboBox1.Items4"), Object), CType(resources.GetObject("RepositoryItemImageComboBox1.Items5"), Integer))})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.GridColumn4.FieldName = "PercentageVariation"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemTextEdit1
        '
        resources.ApplyResources(Me.RepositoryItemTextEdit1, "RepositoryItemTextEdit1")
        Me.RepositoryItemTextEdit1.Mask.EditMask = resources.GetString("RepositoryItemTextEdit1.Mask.EditMask")
        Me.RepositoryItemTextEdit1.Mask.MaskType = CType(resources.GetObject("RepositoryItemTextEdit1.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.RepositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("RepositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'GridColumn5
        '
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.DisplayFormat.FormatString = "c0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "SalePrice"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        '
        'GridColumn6
        '
        resources.ApplyResources(Me.GridColumn6, "GridColumn6")
        Me.GridColumn6.DisplayFormat.FormatString = "c0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "TotalPriceWithDiscount"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        '
        'INDMeDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDetail, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDetail, False)
        resources.ApplyResources(Me.INDMeDetail, "INDMeDetail")
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDetail.Name = "INDMeDetail"
        Me.INDMeDetail.Properties.Appearance.BackColor = CType(resources.GetObject("INDMeDetail.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDMeDetail.Properties.Appearance.Font = CType(resources.GetObject("INDMeDetail.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDMeDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeDetail.Properties.Appearance.Options.UseFont = True
        Me.INDMeDetail.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDMeDetail.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDMeDetail.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDMeDetail.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDMeDetail.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDMeDetail.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeDetail.Properties.MaxLength = 300
        Me.INDMeDetail.StyleController = Me.INDLcRemissionOutput
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDetail, 0)
        '
        'INDSleWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWarehouse, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWarehouse, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        resources.ApplyResources(Me.INDSleWarehouse, "INDSleWarehouse")
        Me.IndigoTextEdit1.SetMascara(Me.INDSleWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Properties.Appearance.BackColor = CType(resources.GetObject("INDSleWarehouse.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDSleWarehouse.Properties.Appearance.Font = CType(resources.GetObject("INDSleWarehouse.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseFont = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDSleWarehouse.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDSleWarehouse.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDSleWarehouse.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDSleWarehouse.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDSleWarehouse.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleWarehouse.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleWarehouse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSleWarehouse.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDSleWarehouse.Properties.DisplayMember = "CodeName"
        Me.INDSleWarehouse.Properties.NullText = resources.GetString("INDSleWarehouse.Properties.NullText")
        Me.INDSleWarehouse.Properties.PopupSizeable = False
        Me.INDSleWarehouse.Properties.ShowClearButton = False
        Me.INDSleWarehouse.Properties.ShowFooter = False
        Me.INDSleWarehouse.Properties.ValueMember = "Id"
        Me.INDSleWarehouse.Properties.View = Me.INDGdvWarehouse
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.StyleController = Me.INDLcRemissionOutput
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleWarehouse, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWarehouse, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWarehouse, False)
        '
        'INDGdvWarehouse
        '
        Me.INDGdvWarehouse.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("INDGdvWarehouse.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.INDGdvWarehouse.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGdvWarehouse.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGdvWarehouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGdvWarehouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGdvWarehouse.Appearance.GroupRow.Font = CType(resources.GetObject("INDGdvWarehouse.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGdvWarehouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGdvWarehouse.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGdvWarehouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGdvWarehouse.Appearance.Row.Font = CType(resources.GetObject("INDGdvWarehouse.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGdvWarehouse.Appearance.Row.Options.UseFont = True
        Me.INDGdvWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.INDGdvWarehouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGdvWarehouse.Name = "INDGdvWarehouse"
        Me.INDGdvWarehouse.OptionsFind.FindFilterColumns = "Code"
        Me.INDGdvWarehouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGdvWarehouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGdvWarehouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGdvWarehouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGdvWarehouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGdvWarehouse, False)
        '
        'GridColumn7
        '
        resources.ApplyResources(Me.GridColumn7, "GridColumn7")
        Me.GridColumn7.FieldName = "Code"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        resources.ApplyResources(Me.GridColumn8, "GridColumn8")
        Me.GridColumn8.FieldName = "Name"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'INDSleCustomer
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCustomer, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCustomer, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCustomer, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCustomer, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCustomer, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCustomer, False)
        Me.INDSleCustomer.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCustomer, False)
        resources.ApplyResources(Me.INDSleCustomer, "INDSleCustomer")
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCustomer, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCustomer.Name = "INDSleCustomer"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCustomer, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCustomer, False)
        Me.INDSleCustomer.Properties.Appearance.BackColor = CType(resources.GetObject("INDSleCustomer.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDSleCustomer.Properties.Appearance.Font = CType(resources.GetObject("INDSleCustomer.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleCustomer.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCustomer.Properties.Appearance.Options.UseFont = True
        Me.INDSleCustomer.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDSleCustomer.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDSleCustomer.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDSleCustomer.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDSleCustomer.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDSleCustomer.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDSleCustomer.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleCustomer.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleCustomer.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleCustomer.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSleCustomer.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDSleCustomer.Properties.DisplayMember = "NitName"
        Me.INDSleCustomer.Properties.NullText = resources.GetString("INDSleCustomer.Properties.NullText")
        Me.INDSleCustomer.Properties.PopupSizeable = False
        Me.INDSleCustomer.Properties.ShowClearButton = False
        Me.INDSleCustomer.Properties.ShowFooter = False
        Me.INDSleCustomer.Properties.ValueMember = "Id"
        Me.INDSleCustomer.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCustomer, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCustomer, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCustomer, True)
        Me.INDSleCustomer.StyleController = Me.INDLcRemissionOutput
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCustomer, "503")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCustomer, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCustomer, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCustomer, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCustomer, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn52, Me.GridColumn53})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsFind.FindFilterColumns = "Nit"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn52
        '
        resources.ApplyResources(Me.GridColumn52, "GridColumn52")
        Me.GridColumn52.FieldName = "Nit"
        Me.GridColumn52.Name = "GridColumn52"
        '
        'GridColumn53
        '
        resources.ApplyResources(Me.GridColumn53, "GridColumn53")
        Me.GridColumn53.FieldName = "Name"
        Me.GridColumn53.Name = "GridColumn53"
        '
        'INDDteDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDate, False)
        resources.ApplyResources(Me.INDDteDate, "INDDteDate")
        Me.INDDteDate.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteDate.Name = "INDDteDate"
        Me.INDDteDate.Properties.Appearance.BackColor = CType(resources.GetObject("INDDteDate.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDDteDate.Properties.Appearance.Font = CType(resources.GetObject("INDDteDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDDteDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteDate.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDDteDate.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDDteDate.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDDteDate.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDDteDate.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDDteDate.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDDteDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDDteDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDDteDate.Properties.CalendarTimeProperties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDDteDate.Properties.Mask.EditMask = resources.GetString("INDDteDate.Properties.Mask.EditMask")
        Me.INDDteDate.Properties.Mask.MaskType = CType(resources.GetObject("INDDteDate.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDDteDate.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDDteDate.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDDteDate.StyleController = Me.INDLcRemissionOutput
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDate, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDBteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.Appearance.Font = CType(resources.GetObject("INDBteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDBteCode.Properties.Buttons7"), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons10"), Boolean))})
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.StyleController = Me.INDLcRemissionOutput
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1296, 554)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciDate, Me.INDLciCustomer, Me.INDLciWarehouse, Me.INDLciDetail})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 534)
        '
        'INDLciCode
        '
        Me.INDLciCode.AllowHide = False
        Me.INDLciCode.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDLciCode, "INDLciCode")
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(390, 60)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciDate
        '
        Me.INDLciDate.AllowHide = False
        Me.INDLciDate.Control = Me.INDDteDate
        resources.ApplyResources(Me.INDLciDate, "INDLciDate")
        Me.INDLciDate.Location = New System.Drawing.Point(0, 60)
        Me.INDLciDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.Name = "INDLciDate"
        Me.INDLciDate.ShowInCustomizationForm = False
        Me.INDLciDate.Size = New System.Drawing.Size(390, 60)
        Me.INDLciDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDate.TextToControlDistance = 5
        '
        'INDLciCustomer
        '
        Me.INDLciCustomer.AllowHide = False
        Me.INDLciCustomer.Control = Me.INDSleCustomer
        resources.ApplyResources(Me.INDLciCustomer, "INDLciCustomer")
        Me.INDLciCustomer.Location = New System.Drawing.Point(0, 120)
        Me.INDLciCustomer.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciCustomer.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciCustomer.Name = "INDLciCustomer"
        Me.INDLciCustomer.ShowInCustomizationForm = False
        Me.INDLciCustomer.Size = New System.Drawing.Size(390, 60)
        Me.INDLciCustomer.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCustomer.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCustomer.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCustomer.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCustomer.TextToControlDistance = 5
        '
        'INDLciWarehouse
        '
        Me.INDLciWarehouse.AllowHide = False
        Me.INDLciWarehouse.Control = Me.INDSleWarehouse
        resources.ApplyResources(Me.INDLciWarehouse, "INDLciWarehouse")
        Me.INDLciWarehouse.Location = New System.Drawing.Point(0, 180)
        Me.INDLciWarehouse.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciWarehouse.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciWarehouse.Name = "INDLciWarehouse"
        Me.INDLciWarehouse.ShowInCustomizationForm = False
        Me.INDLciWarehouse.Size = New System.Drawing.Size(390, 60)
        Me.INDLciWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciWarehouse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciWarehouse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciWarehouse.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciWarehouse.TextToControlDistance = 5
        '
        'INDLciDetail
        '
        Me.INDLciDetail.Control = Me.INDMeDetail
        resources.ApplyResources(Me.INDLciDetail, "INDLciDetail")
        Me.INDLciDetail.Location = New System.Drawing.Point(0, 240)
        Me.INDLciDetail.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDLciDetail.MinSize = New System.Drawing.Size(390, 100)
        Me.INDLciDetail.Name = "INDLciDetail"
        Me.INDLciDetail.Size = New System.Drawing.Size(390, 235)
        Me.INDLciDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDetail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDetail.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDetail.TextToControlDistance = 5
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        resources.ApplyResources(Me.LayoutControlGroup3, "LayoutControlGroup3")
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem8, Me.LayoutControlItem5})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(862, 534)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDGcProduct
        resources.ApplyResources(Me.LayoutControlItem8, "LayoutControlItem8")
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(838, 0)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(838, 1)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(838, 439)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDBtnAdd
        resources.ApplyResources(Me.LayoutControlItem5, "LayoutControlItem5")
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(838, 36)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(838, 36)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(838, 36)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmReferralOut
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "FrmReferralOut"
        Me.Opacity = 1.0R
        Me.Tag = "323"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRemissionOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRemissionOutput.ResumeLayout(False)
        CType(Me.INDGcProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCustomer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcRemissionOutput As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcProduct As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDMeDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGdvWarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleCustomer As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDDteDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCustomer As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDGvProduct As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
End Class
