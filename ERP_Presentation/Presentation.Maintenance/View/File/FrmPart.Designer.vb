<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPart))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCtrPart = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnAgregar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcListEquipmentType = New DevExpress.XtraGrid.GridControl()
        Me.INDgcListEquipmentTypeView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDglEquipmentType = New DevExpress.XtraEditors.TreeListLookUpEdit()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.INDColInventoryType = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDColCode = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColName = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDlyPart = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrPart = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCodePart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemNamePart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIdEquipment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCtrPart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCtrPart.SuspendLayout()
        CType(Me.INDgcListEquipmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcListEquipmentTypeView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglEquipmentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyPart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrPart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCodePart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemNamePart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIdEquipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCtrPart)
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
        'INDLyCtrPart
        '
        Me.INDLyCtrPart.AllowCustomization = False
        Me.INDLyCtrPart.Controls.Add(Me.INDbtnAgregar)
        Me.INDLyCtrPart.Controls.Add(Me.INDgcListEquipmentType)
        Me.INDLyCtrPart.Controls.Add(Me.INDTxtName)
        Me.INDLyCtrPart.Controls.Add(Me.INDBteCode)
        Me.INDLyCtrPart.Controls.Add(Me.INDglEquipmentType)
        resources.ApplyResources(Me.INDLyCtrPart, "INDLyCtrPart")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrPart, False)
        Me.INDLyCtrPart.Name = "INDLyCtrPart"
        Me.INDLyCtrPart.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 221, 250, 350)
        Me.INDLyCtrPart.Root = Me.INDlyPart
        '
        'INDbtnAgregar
        '
        Me.INDbtnAgregar.Appearance.Font = CType(resources.GetObject("INDbtnAgregar.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnAgregar.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDbtnAgregar, "INDbtnAgregar")
        Me.INDbtnAgregar.Name = "INDbtnAgregar"
        Me.INDbtnAgregar.StyleController = Me.INDLyCtrPart
        '
        'INDgcListEquipmentType
        '
        Me.INDgcListEquipmentType.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcListEquipmentType.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.INDgcListEquipmentType.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.INDgcListEquipmentType.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDgcListEquipmentType.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        resources.ApplyResources(Me.INDgcListEquipmentType, "INDgcListEquipmentType")
        Me.INDgcListEquipmentType.MainView = Me.INDgcListEquipmentTypeView
        Me.INDgcListEquipmentType.Name = "INDgcListEquipmentType"
        Me.INDgcListEquipmentType.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcListEquipmentTypeView})
        '
        'INDgcListEquipmentTypeView
        '
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("INDgcListEquipmentTypeView.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgcListEquipmentTypeView.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgcListEquipmentTypeView.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Appearance.GroupRow.Font = CType(resources.GetObject("INDgcListEquipmentTypeView.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgcListEquipmentTypeView.Appearance.GroupRow.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgcListEquipmentTypeView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgcListEquipmentTypeView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Appearance.Row.Font = CType(resources.GetObject("INDgcListEquipmentTypeView.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgcListEquipmentTypeView.Appearance.Row.Options.UseFont = True
        Me.INDgcListEquipmentTypeView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.INDgcListEquipmentTypeView.GridControl = Me.INDgcListEquipmentType
        Me.INDgcListEquipmentTypeView.Name = "INDgcListEquipmentTypeView"
        Me.INDgcListEquipmentTypeView.OptionsDetail.EnableMasterViewMode = False
        Me.INDgcListEquipmentTypeView.OptionsDetail.ShowDetailTabs = False
        Me.INDgcListEquipmentTypeView.OptionsDetail.SmartDetailExpand = False
        Me.INDgcListEquipmentTypeView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcListEquipmentTypeView.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcListEquipmentTypeView.OptionsView.ShowAutoFilterRow = True
        Me.INDgcListEquipmentTypeView.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgcListEquipmentTypeView, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        '
        'GridColumn4
        '
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.INDTxtName.Properties.Mask.EditMask = resources.GetString("INDTxtName.Properties.Mask.EditMask")
        Me.INDTxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtName.Properties.MaxLength = 50
        Me.INDTxtName.StyleController = Me.INDLyCtrPart
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
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
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDBteCode.Properties.Buttons7"), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons10"), Boolean))})
        Me.INDBteCode.Properties.Mask.EditMask = resources.GetString("INDBteCode.Properties.Mask.EditMask")
        Me.INDBteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDBteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.StyleController = Me.INDLyCtrPart
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDglEquipmentType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglEquipmentType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglEquipmentType, False)
        resources.ApplyResources(Me.INDglEquipmentType, "INDglEquipmentType")
        Me.IndigoTextEdit1.SetMascara(Me.INDglEquipmentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglEquipmentType.Name = "INDglEquipmentType"
        Me.INDglEquipmentType.Properties.Appearance.BackColor = CType(resources.GetObject("INDglEquipmentType.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDglEquipmentType.Properties.Appearance.Font = CType(resources.GetObject("INDglEquipmentType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglEquipmentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDglEquipmentType.Properties.Appearance.Options.UseFont = True
        Me.INDglEquipmentType.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDglEquipmentType.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDglEquipmentType.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDglEquipmentType.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
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
        Me.INDglEquipmentType.StyleController = Me.INDLyCtrPart
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglEquipmentType, 0)
        '
        'TreeListLookUpEdit1TreeList
        '
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2 = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.Row.Font"), System.Drawing.Font)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.INDColInventoryType, Me.INDColCode, Me.INDColName})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.TreeListLookUpEdit1TreeList, False)
        Me.TreeListLookUpEdit1TreeList.KeyFieldName = "Id"
        resources.ApplyResources(Me.TreeListLookUpEdit1TreeList, "TreeListLookUpEdit1TreeList")
        Me.TreeListLookUpEdit1TreeList.Name = "TreeListLookUpEdit1TreeList"
        Me.TreeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = True
        Me.TreeListLookUpEdit1TreeList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceOddRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeListLookUpEdit1TreeList.ParentFieldName = "IdParent"
        Me.TreeListLookUpEdit1TreeList.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        '
        'INDColInventoryType
        '
        resources.ApplyResources(Me.INDColInventoryType, "INDColInventoryType")
        Me.INDColInventoryType.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.INDColInventoryType.FieldName = "InventoryType"
        Me.INDColInventoryType.Name = "INDColInventoryType"
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemImageComboBox1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox1.Items"), resources.GetString("RepositoryItemImageComboBox1.Items1"), CType(resources.GetObject("RepositoryItemImageComboBox1.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox1.Items3"), resources.GetString("RepositoryItemImageComboBox1.Items4"), CType(resources.GetObject("RepositoryItemImageComboBox1.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox1.Items6"), resources.GetString("RepositoryItemImageComboBox1.Items7"), CType(resources.GetObject("RepositoryItemImageComboBox1.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox1.Items9"), resources.GetString("RepositoryItemImageComboBox1.Items10"), CType(resources.GetObject("RepositoryItemImageComboBox1.Items11"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox1.Items12"), resources.GetString("RepositoryItemImageComboBox1.Items13"), CType(resources.GetObject("RepositoryItemImageComboBox1.Items14"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox1.Items15"), resources.GetString("RepositoryItemImageComboBox1.Items16"), CType(resources.GetObject("RepositoryItemImageComboBox1.Items17"), Integer))})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'INDColCode
        '
        resources.ApplyResources(Me.INDColCode, "INDColCode")
        Me.INDColCode.FieldName = "Code"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        '
        'INDColName
        '
        resources.ApplyResources(Me.INDColName, "INDColName")
        Me.INDColName.FieldName = "Name"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        '
        'INDlyPart
        '
        resources.ApplyResources(Me.INDlyPart, "INDlyPart")
        Me.INDlyPart.GroupBordersVisible = False
        Me.INDlyPart.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrPart})
        Me.INDlyPart.Location = New System.Drawing.Point(0, 0)
        Me.INDlyPart.Name = "INDlyPart"
        Me.INDlyPart.Size = New System.Drawing.Size(543, 330)
        Me.INDlyPart.TextVisible = False
        '
        'INDGrPart
        '
        Me.INDGrPart.AppearanceGroup.Font = CType(resources.GetObject("INDGrPart.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDGrPart.AppearanceGroup.Options.UseFont = True
        Me.INDGrPart.AppearanceItemCaption.Font = CType(resources.GetObject("INDGrPart.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDGrPart.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDGrPart, "INDGrPart")
        Me.INDGrPart.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCodePart, Me.INDLyItemNamePart, Me.INDlyItemIdEquipment, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.INDGrPart.Location = New System.Drawing.Point(0, 0)
        Me.INDGrPart.Name = "INDGrPart"
        Me.INDGrPart.Size = New System.Drawing.Size(543, 330)
        '
        'INDlyItemCodePart
        '
        Me.INDlyItemCodePart.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDlyItemCodePart, "INDlyItemCodePart")
        Me.INDlyItemCodePart.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCodePart.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodePart.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodePart.Name = "INDlyItemCodePart"
        Me.INDlyItemCodePart.ShowInCustomizationForm = False
        Me.INDlyItemCodePart.Size = New System.Drawing.Size(519, 36)
        Me.INDlyItemCodePart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCodePart.Tag = "Code"
        Me.INDlyItemCodePart.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCodePart.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCodePart.TextToControlDistance = 12
        '
        'INDLyItemNamePart
        '
        Me.INDLyItemNamePart.Control = Me.INDTxtName
        resources.ApplyResources(Me.INDLyItemNamePart, "INDLyItemNamePart")
        Me.INDLyItemNamePart.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemNamePart.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyItemNamePart.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyItemNamePart.Name = "INDLyItemNamePart"
        Me.INDLyItemNamePart.ShowInCustomizationForm = False
        Me.INDLyItemNamePart.Size = New System.Drawing.Size(519, 36)
        Me.INDLyItemNamePart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemNamePart.Tag = "Name"
        Me.INDLyItemNamePart.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemNamePart.TextSize = New System.Drawing.Size(160, 21)
        Me.INDLyItemNamePart.TextToControlDistance = 12
        '
        'INDlyItemIdEquipment
        '
        Me.INDlyItemIdEquipment.Control = Me.INDglEquipmentType
        resources.ApplyResources(Me.INDlyItemIdEquipment, "INDlyItemIdEquipment")
        Me.INDlyItemIdEquipment.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemIdEquipment.MaxSize = New System.Drawing.Size(519, 32)
        Me.INDlyItemIdEquipment.MinSize = New System.Drawing.Size(519, 32)
        Me.INDlyItemIdEquipment.Name = "INDlyItemIdEquipment"
        Me.INDlyItemIdEquipment.Size = New System.Drawing.Size(519, 32)
        Me.INDlyItemIdEquipment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIdEquipment.Tag = "IdEquipment"
        Me.INDlyItemIdEquipment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemIdEquipment.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemIdEquipment.TextToControlDistance = 12
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcListEquipmentType
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 140)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(519, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(519, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(519, 131)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDbtnAgregar
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 104)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(420, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(420, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(519, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyCtrPart
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmPart
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmPart"
        Me.Opacity = 1.0R
        Me.Tag = "573"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCtrPart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCtrPart.ResumeLayout(False)
        CType(Me.INDgcListEquipmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcListEquipmentTypeView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglEquipmentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyPart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrPart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCodePart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemNamePart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIdEquipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCtrPart As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyPart As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGrPart As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemNamePart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlyItemCodePart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemIdEquipment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcListEquipmentType As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcListEquipmentTypeView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAgregar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDglEquipmentType As DevExpress.XtraEditors.TreeListLookUpEdit
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDColInventoryType As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColCode As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColName As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
End Class
