<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetEquipmentTypePartsAccesoriesConsumables
    Inherits Presentation.Controls.FormBase

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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetEquipmentTypePartsAccesoriesConsumables))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLyCtrTechnicalLog = New DevExpress.XtraLayout.LayoutControl()
        Me.INDglPartsAccesories = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeTechnicalLog = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameTechnicalLog = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnAgregar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcListPartsAccesoriesConsumables = New DevExpress.XtraGrid.GridControl()
        Me.INDgcListPartsAccesoriesConsumablesView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDglEquipmentType = New DevExpress.XtraEditors.TreeListLookUpEdit()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.INDColTreeListCode = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColTreeListName = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDlyTechnicalLog = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrTechnicalLog = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemListPartsAccesoriesConsumables = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEquipmentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTechnicalLog = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCtrTechnicalLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCtrTechnicalLog.SuspendLayout()
        CType(Me.INDglPartsAccesories.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcListPartsAccesoriesConsumables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcListPartsAccesoriesConsumablesView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglEquipmentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyTechnicalLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrTechnicalLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemListPartsAccesoriesConsumables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEquipmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTechnicalLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCtrTechnicalLog)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDLyCtrTechnicalLog
        '
        Me.INDLyCtrTechnicalLog.AllowCustomization = False
        Me.INDLyCtrTechnicalLog.Controls.Add(Me.INDglPartsAccesories)
        Me.INDLyCtrTechnicalLog.Controls.Add(Me.INDbtnAgregar)
        Me.INDLyCtrTechnicalLog.Controls.Add(Me.INDgcListPartsAccesoriesConsumables)
        Me.INDLyCtrTechnicalLog.Controls.Add(Me.INDglEquipmentType)
        resources.ApplyResources(Me.INDLyCtrTechnicalLog, "INDLyCtrTechnicalLog")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrTechnicalLog, False)
        Me.INDLyCtrTechnicalLog.Name = "INDLyCtrTechnicalLog"
        Me.INDLyCtrTechnicalLog.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 221, 250, 350)
        Me.INDLyCtrTechnicalLog.Root = Me.INDlyTechnicalLog
        '
        'INDglPartsAccesories
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDglPartsAccesories, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDglPartsAccesories, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDglPartsAccesories, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDglPartsAccesories, False)
        resources.ApplyResources(Me.INDglPartsAccesories, "INDglPartsAccesories")
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDglPartsAccesories, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglPartsAccesories.Name = "INDglPartsAccesories"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDglPartsAccesories, False)
        Me.INDglPartsAccesories.Properties.Appearance.Font = CType(resources.GetObject("INDglPartsAccesories.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglPartsAccesories.Properties.Appearance.Options.UseBackColor = True
        Me.INDglPartsAccesories.Properties.Appearance.Options.UseFont = True
        Me.INDglPartsAccesories.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglPartsAccesories.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDglPartsAccesories.Properties.DisplayMember = "Name"
        Me.INDglPartsAccesories.Properties.NullText = resources.GetString("INDglPartsAccesories.Properties.NullText")
        Me.INDglPartsAccesories.Properties.PopupFormMinSize = New System.Drawing.Size(500, 0)
        Me.INDglPartsAccesories.Properties.PopupSizeable = False
        Me.INDglPartsAccesories.Properties.PopupView = Me.GridView1
        Me.INDglPartsAccesories.Properties.ShowFooter = False
        Me.INDglPartsAccesories.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDglPartsAccesories, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDglPartsAccesories, True)
        Me.INDglPartsAccesories.StyleController = Me.INDLyCtrTechnicalLog
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDglPartsAccesories, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglPartsAccesories, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDglPartsAccesories, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDglPartsAccesories, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDglPartsAccesories, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = CType(resources.GetObject("GridView1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCodeTechnicalLog, Me.INDColNameTechnicalLog})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDColCodeTechnicalLog
        '
        resources.ApplyResources(Me.INDColCodeTechnicalLog, "INDColCodeTechnicalLog")
        Me.INDColCodeTechnicalLog.FieldName = "Code"
        Me.INDColCodeTechnicalLog.Name = "INDColCodeTechnicalLog"
        '
        'INDColNameTechnicalLog
        '
        resources.ApplyResources(Me.INDColNameTechnicalLog, "INDColNameTechnicalLog")
        Me.INDColNameTechnicalLog.FieldName = "Name"
        Me.INDColNameTechnicalLog.Name = "INDColNameTechnicalLog"
        '
        'INDbtnAgregar
        '
        Me.INDbtnAgregar.Appearance.Font = CType(resources.GetObject("INDbtnAgregar.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnAgregar.Appearance.Options.UseFont = True
        Me.INDbtnAgregar.ImageOptions.Image = Global.Presentation.FixedAsset.My.Resources.Resources.Agregar16
        resources.ApplyResources(Me.INDbtnAgregar, "INDbtnAgregar")
        Me.INDbtnAgregar.Name = "INDbtnAgregar"
        Me.INDbtnAgregar.StyleController = Me.INDLyCtrTechnicalLog
        '
        'INDgcListPartsAccesoriesConsumables
        '
        Me.INDgcListPartsAccesoriesConsumables.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcListPartsAccesoriesConsumables.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.INDgcListPartsAccesoriesConsumables.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.INDgcListPartsAccesoriesConsumables.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDgcListPartsAccesoriesConsumables.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        resources.ApplyResources(Me.INDgcListPartsAccesoriesConsumables, "INDgcListPartsAccesoriesConsumables")
        Me.INDgcListPartsAccesoriesConsumables.MainView = Me.INDgcListPartsAccesoriesConsumablesView
        Me.INDgcListPartsAccesoriesConsumables.Name = "INDgcListPartsAccesoriesConsumables"
        Me.INDgcListPartsAccesoriesConsumables.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcListPartsAccesoriesConsumablesView})
        '
        'INDgcListPartsAccesoriesConsumablesView
        '
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.GroupRow.Font = CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.GroupRow.Options.UseFont = True
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.Row.Font = CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgcListPartsAccesoriesConsumablesView.Appearance.Row.Options.UseFont = True
        Me.INDgcListPartsAccesoriesConsumablesView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.INDgcListPartsAccesoriesConsumablesView.GridControl = Me.INDgcListPartsAccesoriesConsumables
        Me.INDgcListPartsAccesoriesConsumablesView.Name = "INDgcListPartsAccesoriesConsumablesView"
        Me.INDgcListPartsAccesoriesConsumablesView.OptionsDetail.EnableMasterViewMode = False
        Me.INDgcListPartsAccesoriesConsumablesView.OptionsDetail.ShowDetailTabs = False
        Me.INDgcListPartsAccesoriesConsumablesView.OptionsDetail.SmartDetailExpand = False
        Me.INDgcListPartsAccesoriesConsumablesView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcListPartsAccesoriesConsumablesView.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcListPartsAccesoriesConsumablesView.OptionsView.ShowAutoFilterRow = True
        Me.INDgcListPartsAccesoriesConsumablesView.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgcListPartsAccesoriesConsumablesView, False)
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "FixedAssetPartsAccesoriesConsumablesCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "FixedAssetPartsAccesoriesConsumablesName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        '
        'INDglEquipmentType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglEquipmentType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglEquipmentType, False)
        resources.ApplyResources(Me.INDglEquipmentType, "INDglEquipmentType")
        Me.IndigoTextEdit1.SetMascara(Me.INDglEquipmentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglEquipmentType.Name = "INDglEquipmentType"
        Me.INDglEquipmentType.Properties.Appearance.Font = CType(resources.GetObject("INDglEquipmentType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglEquipmentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDglEquipmentType.Properties.Appearance.Options.UseFont = True
        Me.INDglEquipmentType.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDglEquipmentType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDglEquipmentType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglEquipmentType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglEquipmentType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglEquipmentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglEquipmentType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDglEquipmentType.Properties.DisplayMember = "Name"
        Me.INDglEquipmentType.Properties.NullText = resources.GetString("INDglEquipmentType.Properties.NullText")
        Me.INDglEquipmentType.Properties.PopupFormMinSize = New System.Drawing.Size(500, 0)
        Me.INDglEquipmentType.Properties.PopupSizeable = False
        Me.INDglEquipmentType.Properties.ShowFooter = False
        Me.INDglEquipmentType.Properties.TreeList = Me.TreeListLookUpEdit1TreeList
        Me.INDglEquipmentType.Properties.ValueMember = "Id"
        Me.INDglEquipmentType.StyleController = Me.INDLyCtrTechnicalLog
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglEquipmentType, 0)
        '
        'TreeListLookUpEdit1TreeList
        '
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2 = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.Row.Font"), System.Drawing.Font)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.INDColTreeListCode, Me.INDColTreeListName})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.TreeListLookUpEdit1TreeList, False)
        Me.TreeListLookUpEdit1TreeList.KeyFieldName = "Id"
        resources.ApplyResources(Me.TreeListLookUpEdit1TreeList, "TreeListLookUpEdit1TreeList")
        Me.TreeListLookUpEdit1TreeList.Name = "TreeListLookUpEdit1TreeList"
        Me.TreeListLookUpEdit1TreeList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Matches
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceOddRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeListLookUpEdit1TreeList.ParentFieldName = "IdParent"
        '
        'INDColTreeListCode
        '
        resources.ApplyResources(Me.INDColTreeListCode, "INDColTreeListCode")
        Me.INDColTreeListCode.FieldName = "Code"
        Me.INDColTreeListCode.Name = "INDColTreeListCode"
        '
        'INDColTreeListName
        '
        resources.ApplyResources(Me.INDColTreeListName, "INDColTreeListName")
        Me.INDColTreeListName.FieldName = "Name"
        Me.INDColTreeListName.Name = "INDColTreeListName"
        '
        'INDlyTechnicalLog
        '
        resources.ApplyResources(Me.INDlyTechnicalLog, "INDlyTechnicalLog")
        Me.INDlyTechnicalLog.GroupBordersVisible = False
        Me.INDlyTechnicalLog.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrTechnicalLog})
        Me.INDlyTechnicalLog.Name = "INDlyTechnicalLog"
        Me.INDlyTechnicalLog.Size = New System.Drawing.Size(938, 537)
        Me.INDlyTechnicalLog.TextVisible = False
        '
        'INDGrTechnicalLog
        '
        Me.INDGrTechnicalLog.AppearanceGroup.Font = CType(resources.GetObject("INDGrTechnicalLog.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDGrTechnicalLog.AppearanceGroup.Options.UseFont = True
        Me.INDGrTechnicalLog.AppearanceItemCaption.Font = CType(resources.GetObject("INDGrTechnicalLog.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDGrTechnicalLog.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDGrTechnicalLog, "INDGrTechnicalLog")
        Me.INDGrTechnicalLog.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemListPartsAccesoriesConsumables, Me.LayoutControlItem2, Me.INDlyItemEquipmentType, Me.INDlyItemTechnicalLog})
        Me.INDGrTechnicalLog.Location = New System.Drawing.Point(0, 0)
        Me.INDGrTechnicalLog.Name = "INDGrTechnicalLog"
        Me.INDGrTechnicalLog.Size = New System.Drawing.Size(938, 537)
        '
        'INDlyItemListPartsAccesoriesConsumables
        '
        Me.INDlyItemListPartsAccesoriesConsumables.Control = Me.INDgcListPartsAccesoriesConsumables
        Me.INDlyItemListPartsAccesoriesConsumables.Location = New System.Drawing.Point(0, 110)
        Me.INDlyItemListPartsAccesoriesConsumables.MaxSize = New System.Drawing.Size(480, 0)
        Me.INDlyItemListPartsAccesoriesConsumables.MinSize = New System.Drawing.Size(480, 1)
        Me.INDlyItemListPartsAccesoriesConsumables.Name = "INDlyItemListPartsAccesoriesConsumables"
        Me.INDlyItemListPartsAccesoriesConsumables.Size = New System.Drawing.Size(914, 374)
        Me.INDlyItemListPartsAccesoriesConsumables.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemListPartsAccesoriesConsumables.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemListPartsAccesoriesConsumables.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemListPartsAccesoriesConsumables.TextToControlDistance = 0
        Me.INDlyItemListPartsAccesoriesConsumables.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDbtnAgregar
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 78)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(480, 32)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(480, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(914, 32)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDlyItemEquipmentType
        '
        Me.INDlyItemEquipmentType.Control = Me.INDglEquipmentType
        resources.ApplyResources(Me.INDlyItemEquipmentType, "INDlyItemEquipmentType")
        Me.INDlyItemEquipmentType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemEquipmentType.MaxSize = New System.Drawing.Size(550, 39)
        Me.INDlyItemEquipmentType.MinSize = New System.Drawing.Size(550, 39)
        Me.INDlyItemEquipmentType.Name = "INDlyItemEquipmentType"
        Me.INDlyItemEquipmentType.ShowInCustomizationForm = False
        Me.INDlyItemEquipmentType.Size = New System.Drawing.Size(914, 39)
        Me.INDlyItemEquipmentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEquipmentType.Tag = "IdMeasurementUnit"
        Me.INDlyItemEquipmentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEquipmentType.TextSize = New System.Drawing.Size(220, 21)
        Me.INDlyItemEquipmentType.TextToControlDistance = 12
        '
        'INDlyItemTechnicalLog
        '
        Me.INDlyItemTechnicalLog.Control = Me.INDglPartsAccesories
        resources.ApplyResources(Me.INDlyItemTechnicalLog, "INDlyItemTechnicalLog")
        Me.INDlyItemTechnicalLog.Location = New System.Drawing.Point(0, 39)
        Me.INDlyItemTechnicalLog.MaxSize = New System.Drawing.Size(550, 39)
        Me.INDlyItemTechnicalLog.MinSize = New System.Drawing.Size(550, 39)
        Me.INDlyItemTechnicalLog.Name = "INDlyItemTechnicalLog"
        Me.INDlyItemTechnicalLog.ShowInCustomizationForm = False
        Me.INDlyItemTechnicalLog.Size = New System.Drawing.Size(914, 39)
        Me.INDlyItemTechnicalLog.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTechnicalLog.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTechnicalLog.TextSize = New System.Drawing.Size(220, 21)
        Me.INDlyItemTechnicalLog.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyCtrTechnicalLog
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'GridColumn5
        '
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn6
        '
        resources.ApplyResources(Me.GridColumn6, "GridColumn6")
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'FrmFixedAssetEquipmentTypePartsAccesoriesConsumables
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmFixedAssetEquipmentTypePartsAccesoriesConsumables"
        Me.Opacity = 1.0R
        Me.Tag = "1715"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCtrTechnicalLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCtrTechnicalLog.ResumeLayout(False)
        CType(Me.INDglPartsAccesories.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcListPartsAccesoriesConsumables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcListPartsAccesoriesConsumablesView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglEquipmentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyTechnicalLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrTechnicalLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemListPartsAccesoriesConsumables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEquipmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTechnicalLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCtrTechnicalLog As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlyTechnicalLog As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGrTechnicalLog As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDgcListPartsAccesoriesConsumables As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcListPartsAccesoriesConsumablesView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemListPartsAccesoriesConsumables As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAgregar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDlyItemEquipmentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDglPartsAccesories As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemTechnicalLog As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColCodeTechnicalLog As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameTechnicalLog As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDglEquipmentType As DevExpress.XtraEditors.TreeListLookUpEdit
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDColTreeListCode As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColTreeListName As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
