<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEquipmentTypePartsAccesoriesConsumables
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEquipmentTypePartsAccesoriesConsumables))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCtrTechnicalLog = New DevExpress.XtraLayout.LayoutControl()
        Me.INDglTechnicalLog = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCodeTechnicalLog = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameTechnicalLog = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnAgregar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcListUnitMeasure = New DevExpress.XtraGrid.GridControl()
        Me.INDgcListUnitMeasureView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDglEquipmentType = New DevExpress.XtraEditors.TreeListLookUpEdit()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.INDColTreeListCode = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDColTreeListName = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDlyTechnicalLog = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrTechnicalLog = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemListUnitMeasure = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.INDLyCtrTechnicalLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCtrTechnicalLog.SuspendLayout()
        CType(Me.INDglTechnicalLog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcListUnitMeasure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcListUnitMeasureView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglEquipmentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyTechnicalLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrTechnicalLog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemListUnitMeasure, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'INDLyCtrTechnicalLog
        '
        Me.INDLyCtrTechnicalLog.AllowCustomization = False
        Me.INDLyCtrTechnicalLog.Controls.Add(Me.INDglTechnicalLog)
        Me.INDLyCtrTechnicalLog.Controls.Add(Me.INDbtnAgregar)
        Me.INDLyCtrTechnicalLog.Controls.Add(Me.INDgcListUnitMeasure)
        Me.INDLyCtrTechnicalLog.Controls.Add(Me.INDglEquipmentType)
        resources.ApplyResources(Me.INDLyCtrTechnicalLog, "INDLyCtrTechnicalLog")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrTechnicalLog, False)
        Me.INDLyCtrTechnicalLog.Name = "INDLyCtrTechnicalLog"
        Me.INDLyCtrTechnicalLog.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 221, 250, 350)
        Me.INDLyCtrTechnicalLog.Root = Me.INDlyTechnicalLog
        '
        'INDglTechnicalLog
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDglTechnicalLog, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDglTechnicalLog, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDglTechnicalLog, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglTechnicalLog, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglTechnicalLog, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDglTechnicalLog, True)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDglTechnicalLog, False)
        resources.ApplyResources(Me.INDglTechnicalLog, "INDglTechnicalLog")
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDglTechnicalLog, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDglTechnicalLog, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglTechnicalLog.Name = "INDglTechnicalLog"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDglTechnicalLog, False)
        Me.INDglTechnicalLog.Properties.Appearance.BackColor = CType(resources.GetObject("INDglTechnicalLog.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDglTechnicalLog.Properties.Appearance.Font = CType(resources.GetObject("INDglTechnicalLog.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglTechnicalLog.Properties.Appearance.Options.UseBackColor = True
        Me.INDglTechnicalLog.Properties.Appearance.Options.UseFont = True
        Me.INDglTechnicalLog.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)), New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons1"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDglTechnicalLog.Properties.Buttons2"), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons3"), Integer), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons6"), Boolean), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons7"), DevExpress.XtraEditors.ImageLocation), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons8"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDglTechnicalLog.Properties.Buttons9"), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons10"), Object), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons11"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDglTechnicalLog.Properties.Buttons12"), Boolean))})
        Me.INDglTechnicalLog.Properties.DisplayMember = "Name"
        Me.INDglTechnicalLog.Properties.NullText = resources.GetString("INDglTechnicalLog.Properties.NullText")
        Me.INDglTechnicalLog.Properties.PopupFormMinSize = New System.Drawing.Size(500, 0)
        Me.INDglTechnicalLog.Properties.PopupSizeable = False
        Me.INDglTechnicalLog.Properties.ShowFooter = False
        Me.INDglTechnicalLog.Properties.ValueMember = "Id"
        Me.INDglTechnicalLog.Properties.View = Me.GridView1
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDglTechnicalLog, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDglTechnicalLog, True)
        Me.INDglTechnicalLog.StyleController = Me.INDLyCtrTechnicalLog
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDglTechnicalLog, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglTechnicalLog, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDglTechnicalLog, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDglTechnicalLog, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDglTechnicalLog, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("GridView1.Appearance.GroupRow.ForeColor"), System.Drawing.Color)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("GridView1.Appearance.HeaderPanel.ForeColor"), System.Drawing.Color)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
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
        resources.ApplyResources(Me.INDbtnAgregar, "INDbtnAgregar")
        Me.INDbtnAgregar.Name = "INDbtnAgregar"
        Me.INDbtnAgregar.StyleController = Me.INDLyCtrTechnicalLog
        '
        'INDgcListUnitMeasure
        '
        Me.INDgcListUnitMeasure.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcListUnitMeasure.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.INDgcListUnitMeasure.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.INDgcListUnitMeasure.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDgcListUnitMeasure.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        resources.ApplyResources(Me.INDgcListUnitMeasure, "INDgcListUnitMeasure")
        Me.INDgcListUnitMeasure.MainView = Me.INDgcListUnitMeasureView
        Me.INDgcListUnitMeasure.Name = "INDgcListUnitMeasure"
        Me.INDgcListUnitMeasure.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcListUnitMeasureView})
        '
        'INDgcListUnitMeasureView
        '
        Me.INDgcListUnitMeasureView.Appearance.GroupRow.Font = CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.GroupRow.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.GroupRow.ForeColor"), System.Drawing.Color)
        Me.INDgcListUnitMeasureView.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgcListUnitMeasureView.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.HeaderPanel.ForeColor"), System.Drawing.Color)
        Me.INDgcListUnitMeasureView.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgcListUnitMeasureView.Appearance.Row.Font = CType(resources.GetObject("INDgcListUnitMeasureView.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgcListUnitMeasureView.Appearance.Row.Options.UseFont = True
        Me.INDgcListUnitMeasureView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.INDgcListUnitMeasureView.GridControl = Me.INDgcListUnitMeasure
        Me.INDgcListUnitMeasureView.Name = "INDgcListUnitMeasureView"
        Me.INDgcListUnitMeasureView.OptionsDetail.EnableMasterViewMode = False
        Me.INDgcListUnitMeasureView.OptionsDetail.ShowDetailTabs = False
        Me.INDgcListUnitMeasureView.OptionsDetail.SmartDetailExpand = False
        Me.INDgcListUnitMeasureView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcListUnitMeasureView.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcListUnitMeasureView.OptionsView.ShowAutoFilterRow = True
        Me.INDgcListUnitMeasureView.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgcListUnitMeasureView, False)
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Name"
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
        Me.INDglEquipmentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglEquipmentType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)), New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglEquipmentType.Properties.Buttons1"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDglEquipmentType.Properties.Buttons2"), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons3"), Integer), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons6"), Boolean), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons7"), DevExpress.XtraEditors.ImageLocation), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons8"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, resources.GetString("INDglEquipmentType.Properties.Buttons9"), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons10"), Object), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons11"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDglEquipmentType.Properties.Buttons12"), Boolean))})
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
        'Cadena reemplazada... CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.FocusedRow.BackColor"), System.Drawing.Color)
        'Cadena reemplazada... CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.FocusedRow.BackColor2"), System.Drawing.Color)
        'Cadena reemplazada... CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.FocusedRow.ForeColor"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2 = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.ForeColor"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.Row.Font"), System.Drawing.Font)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor = CType(resources.GetObject("TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor"), System.Drawing.Color)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.INDColTreeListCode, Me.INDColTreeListName})
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
        Me.INDlyTechnicalLog.Location = New System.Drawing.Point(0, 0)
        Me.INDlyTechnicalLog.Name = "INDlyTechnicalLog"
        Me.INDlyTechnicalLog.Size = New System.Drawing.Size(574, 537)
        Me.INDlyTechnicalLog.TextVisible = False
        '
        'INDGrTechnicalLog
        '
        Me.INDGrTechnicalLog.AppearanceGroup.Font = CType(resources.GetObject("INDGrTechnicalLog.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDGrTechnicalLog.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDGrTechnicalLog.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGrTechnicalLog.AppearanceItemCaption.Font = CType(resources.GetObject("INDGrTechnicalLog.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDGrTechnicalLog.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDGrTechnicalLog, "INDGrTechnicalLog")
        Me.INDGrTechnicalLog.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemListUnitMeasure, Me.LayoutControlItem2, Me.INDlyItemEquipmentType, Me.INDlyItemTechnicalLog})
        Me.INDGrTechnicalLog.Location = New System.Drawing.Point(0, 0)
        Me.INDGrTechnicalLog.Name = "INDGrTechnicalLog"
        Me.INDGrTechnicalLog.Size = New System.Drawing.Size(574, 537)
        '
        'INDlyItemListUnitMeasure
        '
        Me.INDlyItemListUnitMeasure.Control = Me.INDgcListUnitMeasure
        resources.ApplyResources(Me.INDlyItemListUnitMeasure, "INDlyItemListUnitMeasure")
        Me.INDlyItemListUnitMeasure.Location = New System.Drawing.Point(0, 110)
        Me.INDlyItemListUnitMeasure.Name = "INDlyItemListUnitMeasure"
        Me.INDlyItemListUnitMeasure.Size = New System.Drawing.Size(550, 368)
        Me.INDlyItemListUnitMeasure.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemListUnitMeasure.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemListUnitMeasure.TextToControlDistance = 0
        Me.INDlyItemListUnitMeasure.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDbtnAgregar
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 78)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(519, 32)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(519, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(550, 32)
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
        Me.INDlyItemEquipmentType.Size = New System.Drawing.Size(550, 39)
        Me.INDlyItemEquipmentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEquipmentType.Tag = "IdMeasurementUnit"
        Me.INDlyItemEquipmentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEquipmentType.TextSize = New System.Drawing.Size(220, 21)
        Me.INDlyItemEquipmentType.TextToControlDistance = 12
        '
        'INDlyItemTechnicalLog
        '
        Me.INDlyItemTechnicalLog.Control = Me.INDglTechnicalLog
        resources.ApplyResources(Me.INDlyItemTechnicalLog, "INDlyItemTechnicalLog")
        Me.INDlyItemTechnicalLog.Location = New System.Drawing.Point(0, 39)
        Me.INDlyItemTechnicalLog.MaxSize = New System.Drawing.Size(550, 39)
        Me.INDlyItemTechnicalLog.MinSize = New System.Drawing.Size(550, 39)
        Me.INDlyItemTechnicalLog.Name = "INDlyItemTechnicalLog"
        Me.INDlyItemTechnicalLog.ShowInCustomizationForm = False
        Me.INDlyItemTechnicalLog.Size = New System.Drawing.Size(550, 39)
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
        'FrmEquipmentTypePartsAccesoriesConsumables
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmEquipmentTypePartsAccesoriesConsumables"
        Me.Opacity = 1.0R
        Me.Tag = "1532"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCtrTechnicalLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCtrTechnicalLog.ResumeLayout(False)
        CType(Me.INDglTechnicalLog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcListUnitMeasure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcListUnitMeasureView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglEquipmentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyTechnicalLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrTechnicalLog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemListUnitMeasure, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents INDgcListUnitMeasure As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcListUnitMeasureView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemListUnitMeasure As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAgregar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDlyItemEquipmentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDglTechnicalLog As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemTechnicalLog As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColCodeTechnicalLog As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameTechnicalLog As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDglEquipmentType As DevExpress.XtraEditors.TreeListLookUpEdit
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDColTreeListCode As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDColTreeListName As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
End Class
