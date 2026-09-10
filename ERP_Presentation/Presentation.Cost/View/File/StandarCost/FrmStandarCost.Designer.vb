Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmStandarCost
    Inherits FormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmStandarCost))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDExportStructure = New Presentation.Controls.ExportStructureButton()
        Me.INDsbAddDetail = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcCostStandarDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDgvCostStandarDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColActivity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFixedAsset = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPayroll = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInventory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAdditionalCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColStandarCost = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckContrated = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrepCheckQuoted = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDTEValidityAlert = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDbarButtonSelectContrated = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarButtonUnSelectContrated = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarButtonSelectQuoted = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarButtonUnSelectQuoted = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarButtonInactivate = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarButtonEdit = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBtnActivate = New DevExpress.XtraBars.BarButtonItem()
        Me.INDdeStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDdeEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStartDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgCostStandar = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.PopupMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoGridControl11 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLayoutControlGroup11 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton11 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDgcCostStandarDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvCostStandarDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckContrated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckQuoted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTEValidityAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgCostStandar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1510, 573)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1510, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1510, 130)
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 8)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 563)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.AllowCustomization = False
        Me.INDlcRoot.Controls.Add(Me.INDBtnImportFile)
        Me.INDlcRoot.Controls.Add(Me.INDExportStructure)
        Me.INDlcRoot.Controls.Add(Me.INDsbAddDetail)
        Me.INDlcRoot.Controls.Add(Me.INDgcCostStandarDetail)
        Me.INDlcRoot.Controls.Add(Me.INDbteCode)
        Me.INDlcRoot.Controls.Add(Me.INDtxtName)
        Me.INDlcRoot.Controls.Add(Me.INDdeStartDate)
        Me.INDlcRoot.Controls.Add(Me.INDdeEndDate)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, False)
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 8)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1907, 193, 698, 359)
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(1306, 563)
        Me.INDlcRoot.TabIndex = 2
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFile.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(1650, 53)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(34, 32)
        Me.INDBtnImportFile.StyleController = Me.INDlcRoot
        Me.INDBtnImportFile.TabIndex = 24
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDExportStructure
        '
        Me.INDExportStructure.ImageOptions.Image = CType(resources.GetObject("INDExportStructure.ImageOptions.Image"), System.Drawing.Image)
        Me.INDExportStructure.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDExportStructure.Location = New System.Drawing.Point(1612, 53)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDExportStructure, False)
        Me.INDExportStructure.Name = "INDExportStructure"
        Me.INDExportStructure.Size = New System.Drawing.Size(34, 32)
        Me.INDExportStructure.StyleController = Me.INDlcRoot
        Me.INDExportStructure.TabIndex = 23
        Me.INDExportStructure.Text = "ExportStructureButton3"
        Me.INDExportStructure.ToolTip = "Exportar Estructura"
        '
        'INDsbAddDetail
        '
        Me.INDsbAddDetail.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAddDetail.Appearance.Options.UseFont = True
        Me.INDsbAddDetail.Location = New System.Drawing.Point(438, 53)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDsbAddDetail, False)
        Me.INDsbAddDetail.Name = "INDsbAddDetail"
        Me.INDsbAddDetail.Size = New System.Drawing.Size(1170, 32)
        Me.INDsbAddDetail.StyleController = Me.INDlcRoot
        Me.INDsbAddDetail.TabIndex = 9
        Me.INDsbAddDetail.Text = "Agregar"
        '
        'INDgcCostStandarDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcCostStandarDetail, Nothing)
        Me.IndigoGridControl11.SetAddActions(Me.INDgcCostStandarDetail, Nothing)
        Me.IndigoGridControl11.SetControlNextFocus(Me.INDgcCostStandarDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcCostStandarDetail, Nothing)
        Me.INDgcCostStandarDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcCostStandarDetail, False)
        Me.IndigoGridControl11.SetExportButton(Me.INDgcCostStandarDetail, False)
        Me.IndigoGridControl11.SetGuardarXml(Me.INDgcCostStandarDetail, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcCostStandarDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcCostStandarDetail, False)
        Me.IndigoGridControl11.SetHoldSize(Me.INDgcCostStandarDetail, False)
        Me.IndigoGridControl11.SetHotTrack(Me.INDgcCostStandarDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcCostStandarDetail, False)
        Me.INDgcCostStandarDetail.Location = New System.Drawing.Point(438, 89)
        Me.INDgcCostStandarDetail.MainView = Me.INDgvCostStandarDetail
        Me.INDgcCostStandarDetail.Name = "INDgcCostStandarDetail"
        Me.INDgcCostStandarDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckContrated, Me.INDrepCheckQuoted, Me.INDTEValidityAlert})
        Me.INDgcCostStandarDetail.Size = New System.Drawing.Size(1246, 433)
        Me.IndigoGridControl11.SetSizeConstraintsType(Me.INDgcCostStandarDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcCostStandarDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcCostStandarDetail.TabIndex = 7
        Me.INDgcCostStandarDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvCostStandarDetail})
        '
        'INDgvCostStandarDetail
        '
        Me.INDgvCostStandarDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvCostStandarDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvCostStandarDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvCostStandarDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvCostStandarDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvCostStandarDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvCostStandarDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvCostStandarDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvCostStandarDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvCostStandarDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvCostStandarDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvCostStandarDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvCostStandarDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvCostStandarDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvCostStandarDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColActivity, Me.INDColFixedAsset, Me.INDColPayroll, Me.INDColInventory, Me.INDColAdditionalCost, Me.INDColStandarCost})
        Me.INDgvCostStandarDetail.GridControl = Me.INDgcCostStandarDetail
        Me.INDgvCostStandarDetail.Name = "INDgvCostStandarDetail"
        Me.INDgvCostStandarDetail.OptionsSelection.MultiSelect = True
        Me.INDgvCostStandarDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvCostStandarDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvCostStandarDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvCostStandarDetail.OptionsView.ShowDetailButtons = False
        Me.INDgvCostStandarDetail.OptionsView.ShowFooter = True
        Me.INDgvCostStandarDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDgvCostStandarDetail, False)
        '
        'INDColActivity
        '
        Me.INDColActivity.Caption = "Actividad"
        Me.INDColActivity.FieldName = "CodeNameActivity"
        Me.INDColActivity.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.INDColActivity.Name = "INDColActivity"
        Me.INDColActivity.OptionsColumn.AllowEdit = False
        Me.INDColActivity.OptionsColumn.AllowFocus = False
        Me.INDColActivity.Visible = True
        Me.INDColActivity.VisibleIndex = 0
        '
        'INDColFixedAsset
        '
        Me.INDColFixedAsset.Caption = "Activos Fijos"
        Me.INDColFixedAsset.DisplayFormat.FormatString = "c2"
        Me.INDColFixedAsset.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColFixedAsset.FieldName = "FixedAssetValue"
        Me.INDColFixedAsset.Name = "INDColFixedAsset"
        Me.INDColFixedAsset.OptionsColumn.ReadOnly = True
        Me.INDColFixedAsset.Visible = True
        Me.INDColFixedAsset.VisibleIndex = 1
        '
        'INDColPayroll
        '
        Me.INDColPayroll.Caption = "Nómina"
        Me.INDColPayroll.DisplayFormat.FormatString = "c2"
        Me.INDColPayroll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColPayroll.FieldName = "PayrollValue"
        Me.INDColPayroll.Name = "INDColPayroll"
        Me.INDColPayroll.OptionsColumn.ReadOnly = True
        Me.INDColPayroll.Visible = True
        Me.INDColPayroll.VisibleIndex = 2
        '
        'INDColInventory
        '
        Me.INDColInventory.Caption = "Inventario"
        Me.INDColInventory.DisplayFormat.FormatString = "c2"
        Me.INDColInventory.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInventory.FieldName = "InventoryValue"
        Me.INDColInventory.Name = "INDColInventory"
        Me.INDColInventory.OptionsColumn.ReadOnly = True
        Me.INDColInventory.Visible = True
        Me.INDColInventory.VisibleIndex = 3
        '
        'INDColAdditionalCost
        '
        Me.INDColAdditionalCost.Caption = "Costo Adicional"
        Me.INDColAdditionalCost.DisplayFormat.FormatString = "c2"
        Me.INDColAdditionalCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColAdditionalCost.FieldName = "AdditionalCost"
        Me.INDColAdditionalCost.Name = "INDColAdditionalCost"
        Me.INDColAdditionalCost.OptionsColumn.ReadOnly = True
        Me.INDColAdditionalCost.Visible = True
        Me.INDColAdditionalCost.VisibleIndex = 4
        '
        'INDColStandarCost
        '
        Me.INDColStandarCost.Caption = "Costo Estándar"
        Me.INDColStandarCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColStandarCost.FieldName = "StandarCostValue"
        Me.INDColStandarCost.Name = "INDColStandarCost"
        Me.INDColStandarCost.OptionsColumn.ReadOnly = True
        Me.INDColStandarCost.Visible = True
        Me.INDColStandarCost.VisibleIndex = 5
        '
        'INDrepCheckContrated
        '
        Me.INDrepCheckContrated.AutoHeight = False
        Me.INDrepCheckContrated.Name = "INDrepCheckContrated"
        '
        'INDrepCheckQuoted
        '
        Me.INDrepCheckQuoted.AutoHeight = False
        Me.INDrepCheckQuoted.Name = "INDrepCheckQuoted"
        '
        'INDTEValidityAlert
        '
        Me.INDTEValidityAlert.AutoHeight = False
        Me.INDTEValidityAlert.Name = "INDTEValidityAlert"
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, False)
        Me.INDbteCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseForeColor = True
        EditorButtonImageOptions1.Image = Global.Presentation.Cost.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbteCode.StyleController = Me.INDlcRoot
        Me.INDbteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 133)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.MenuManager = Me.BarManager
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtName.Properties.MaxLength = 20
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlcRoot
        Me.INDtxtName.TabIndex = 25
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDbarButtonSelectContrated, Me.INDbarButtonUnSelectContrated, Me.INDbarButtonSelectQuoted, Me.INDbarButtonUnSelectQuoted, Me.INDbarButtonInactivate, Me.INDbarButtonEdit, Me.INDBtnActivate})
        Me.BarManager.MaxItemId = 10
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlTop.Manager = Me.BarManager
        Me.barDockControlTop.Size = New System.Drawing.Size(1510, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 709)
        Me.barDockControlBottom.Manager = Me.BarManager
        Me.barDockControlBottom.Size = New System.Drawing.Size(1510, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlLeft.Manager = Me.BarManager
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 703)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1510, 6)
        Me.barDockControlRight.Manager = Me.BarManager
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 703)
        '
        'INDbarButtonSelectContrated
        '
        Me.INDbarButtonSelectContrated.Caption = "Seleccionar Contratados"
        Me.INDbarButtonSelectContrated.Id = 2
        Me.INDbarButtonSelectContrated.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectContrated.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonSelectContrated.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectContrated.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonSelectContrated.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectContrated.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonSelectContrated.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectContrated.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonSelectContrated.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectContrated.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonSelectContrated.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectContrated.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonSelectContrated.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectContrated.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonSelectContrated.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectContrated.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonSelectContrated.Name = "INDbarButtonSelectContrated"
        '
        'INDbarButtonUnSelectContrated
        '
        Me.INDbarButtonUnSelectContrated.Caption = "Deseleccionar Contratados"
        Me.INDbarButtonUnSelectContrated.Id = 3
        Me.INDbarButtonUnSelectContrated.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectContrated.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonUnSelectContrated.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectContrated.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonUnSelectContrated.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectContrated.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonUnSelectContrated.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectContrated.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonUnSelectContrated.Name = "INDbarButtonUnSelectContrated"
        '
        'INDbarButtonSelectQuoted
        '
        Me.INDbarButtonSelectQuoted.Caption = "Seleccionar Cotizados"
        Me.INDbarButtonSelectQuoted.Id = 4
        Me.INDbarButtonSelectQuoted.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectQuoted.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonSelectQuoted.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectQuoted.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonSelectQuoted.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectQuoted.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonSelectQuoted.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectQuoted.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonSelectQuoted.Name = "INDbarButtonSelectQuoted"
        '
        'INDbarButtonUnSelectQuoted
        '
        Me.INDbarButtonUnSelectQuoted.Caption = "Deseleccionar Cotizados"
        Me.INDbarButtonUnSelectQuoted.Id = 5
        Me.INDbarButtonUnSelectQuoted.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectQuoted.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonUnSelectQuoted.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectQuoted.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonUnSelectQuoted.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectQuoted.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonUnSelectQuoted.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectQuoted.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonUnSelectQuoted.Name = "INDbarButtonUnSelectQuoted"
        '
        'INDbarButtonInactivate
        '
        Me.INDbarButtonInactivate.Caption = "Inactivar"
        Me.INDbarButtonInactivate.Id = 6
        Me.INDbarButtonInactivate.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonInactivate.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonInactivate.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonInactivate.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonInactivate.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonInactivate.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonInactivate.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonInactivate.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonInactivate.Name = "INDbarButtonInactivate"
        '
        'INDbarButtonEdit
        '
        Me.INDbarButtonEdit.Caption = "Editar"
        Me.INDbarButtonEdit.Id = 8
        Me.INDbarButtonEdit.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonEdit.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonEdit.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonEdit.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonEdit.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonEdit.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonEdit.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonEdit.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonEdit.Name = "INDbarButtonEdit"
        '
        'INDBtnActivate
        '
        Me.INDBtnActivate.Caption = "Activar"
        Me.INDBtnActivate.Id = 9
        Me.INDBtnActivate.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDBtnActivate.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBtnActivate.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDBtnActivate.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBtnActivate.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDBtnActivate.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBtnActivate.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDBtnActivate.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBtnActivate.Name = "INDBtnActivate"
        '
        'INDdeStartDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeStartDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeStartDate, True)
        Me.INDdeStartDate.EditValue = Nothing
        Me.INDdeStartDate.EnterMoveNextControl = True
        Me.INDdeStartDate.Location = New System.Drawing.Point(24, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeStartDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdeStartDate.Name = "INDdeStartDate"
        Me.INDdeStartDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeStartDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeStartDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdeStartDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeStartDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeStartDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeStartDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeStartDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeStartDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeStartDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdeStartDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeStartDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeStartDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeStartDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDdeStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeStartDate.Properties.DisplayFormat.FormatString = ""
        Me.INDdeStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDdeStartDate.Properties.EditFormat.FormatString = ""
        Me.INDdeStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDdeStartDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeStartDate.StyleController = Me.INDlcRoot
        Me.INDdeStartDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeStartDate, 0)
        Me.INDdeStartDate.ToolTip = "Este Campo es Necesario"
        '
        'INDdeEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeEndDate, False)
        Me.INDdeEndDate.EditValue = Nothing
        Me.INDdeEndDate.EnterMoveNextControl = True
        Me.INDdeEndDate.Location = New System.Drawing.Point(24, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdeEndDate.Name = "INDdeEndDate"
        Me.INDdeEndDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeEndDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdeEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeEndDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeEndDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeEndDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeEndDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdeEndDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeEndDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeEndDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDdeEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndDate.Properties.DisplayFormat.FormatString = ""
        Me.INDdeEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDdeEndDate.Properties.EditFormat.FormatString = ""
        Me.INDdeEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDdeEndDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeEndDate.StyleController = Me.INDlcRoot
        Me.INDdeEndDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeEndDate, 0)
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup11.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "Tarifa de Productos"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData, Me.INDlcgCostStandar})
        Me.INDlcgRoot.Name = "Root"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1708, 546)
        Me.INDlcgRoot.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup11.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.CustomizationFormText = "Datos Principales"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCode, Me.INDLciStartDate, Me.INDLciName, Me.INDLciEndDate})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 526)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDliCode
        '
        Me.INDliCode.AllowHide = False
        Me.INDliCode.Control = Me.INDbteCode
        Me.INDliCode.CustomizationFormText = "Código"
        Me.INDliCode.Location = New System.Drawing.Point(0, 0)
        Me.INDliCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliCode.Name = "INDliCode"
        Me.INDliCode.ShowInCustomizationForm = False
        Me.INDliCode.Size = New System.Drawing.Size(390, 60)
        Me.INDliCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCode.Text = "Código"
        Me.INDliCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliCode.TextToControlDistance = 5
        '
        'INDLciStartDate
        '
        Me.INDLciStartDate.AllowHide = False
        Me.INDLciStartDate.Control = Me.INDdeStartDate
        Me.INDLciStartDate.CustomizationFormText = "Nombre"
        Me.INDLciStartDate.Location = New System.Drawing.Point(0, 120)
        Me.INDLciStartDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciStartDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciStartDate.Name = "INDLciStartDate"
        Me.INDLciStartDate.ShowInCustomizationForm = False
        Me.INDLciStartDate.Size = New System.Drawing.Size(390, 60)
        Me.INDLciStartDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStartDate.Text = "Fecha Inicial"
        Me.INDLciStartDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciStartDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciStartDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciStartDate.TextToControlDistance = 5
        '
        'INDLciName
        '
        Me.INDLciName.Control = Me.INDtxtName
        Me.INDLciName.Location = New System.Drawing.Point(0, 60)
        Me.INDLciName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciName.Name = "INDLciName"
        Me.INDLciName.Size = New System.Drawing.Size(390, 60)
        Me.INDLciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciName.Text = "Nombre"
        Me.INDLciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciName.TextSize = New System.Drawing.Size(51, 17)
        '
        'INDLciEndDate
        '
        Me.INDLciEndDate.Control = Me.INDdeEndDate
        Me.INDLciEndDate.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciEndDate.CustomizationFormText = "Nombre"
        Me.INDLciEndDate.Location = New System.Drawing.Point(0, 180)
        Me.INDLciEndDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciEndDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciEndDate.Name = "INDLciEndDate"
        Me.INDLciEndDate.ShowInCustomizationForm = False
        Me.INDLciEndDate.Size = New System.Drawing.Size(390, 293)
        Me.INDLciEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEndDate.Text = "Fecha Final"
        Me.INDLciEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEndDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEndDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciEndDate.TextToControlDistance = 5
        '
        'INDlcgCostStandar
        '
        Me.INDlcgCostStandar.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgCostStandar.AppearanceGroup.Options.UseFont = True
        Me.INDlcgCostStandar.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgCostStandar.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgCostStandar.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCostStandar.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgCostStandar.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgCostStandar.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgCostStandar.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCostStandar.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgCostStandar.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCostStandar.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgCostStandar.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCostStandar.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup11.SetCampoObligatorio(Me.INDlcgCostStandar, False)
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgCostStandar, False)
        Me.INDlcgCostStandar.CustomizationFormText = "Costo Estándar Promedio"
        Me.INDlcgCostStandar.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDliAddDetail, Me.LayoutControlItem1, Me.LayoutControlItem3})
        Me.INDlcgCostStandar.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgCostStandar.Name = "INDlcgCostStandar"
        Me.INDlcgCostStandar.Size = New System.Drawing.Size(1274, 526)
        Me.INDlcgCostStandar.Text = "Costo Estándar Promedio"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AllowHide = False
        Me.LayoutControlItem2.Control = Me.INDgcCostStandarDetail
        Me.LayoutControlItem2.CustomizationFormText = "Tarifas"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(1250, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(1250, 1)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1250, 437)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDliAddDetail
        '
        Me.INDliAddDetail.Control = Me.INDsbAddDetail
        Me.INDliAddDetail.CustomizationFormText = "Agregar"
        Me.INDliAddDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDliAddDetail.MaxSize = New System.Drawing.Size(1174, 36)
        Me.INDliAddDetail.MinSize = New System.Drawing.Size(1174, 36)
        Me.INDliAddDetail.Name = "INDliAddDetail"
        Me.INDliAddDetail.Size = New System.Drawing.Size(1174, 36)
        Me.INDliAddDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAddDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAddDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAddDetail.TextToControlDistance = 0
        Me.INDliAddDetail.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDExportStructure
        Me.LayoutControlItem1.Location = New System.Drawing.Point(1174, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDBtnImportFile
        Me.LayoutControlItem3.Location = New System.Drawing.Point(1212, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'PopupMenuActions
        '
        Me.PopupMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonSelectContrated), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonUnSelectContrated), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonSelectQuoted), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonUnSelectQuoted), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonInactivate), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonEdit), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBtnActivate)})
        Me.PopupMenuActions.Manager = Me.BarManager
        Me.PopupMenuActions.Name = "PopupMenuActions"
        '
        'IndigoGridControl11
        '
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        Me.IndigoGridView11.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'FrmStandarCost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1510, 709)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmStandarCost"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "89024"
        Me.Text = "Costo Estándar Promedio"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDgcCostStandarDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvCostStandarDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckContrated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckQuoted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTEValidityAlert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgCostStandar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcCostStandarDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvCostStandarDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciStartDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgCostStandar As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDsbAddDetail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDliAddDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDExportStructure As Presentation.Controls.ExportStructureButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrepCheckContrated As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrepCheckQuoted As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDbarButtonSelectContrated As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarButtonUnSelectContrated As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarButtonSelectQuoted As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarButtonUnSelectQuoted As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarButtonInactivate As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenuActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDbarButtonEdit As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDTEValidityAlert As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDBtnActivate As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColActivity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton11 As IndigoSimpleButton
    Friend WithEvents IndigoGridControl11 As IndigoGridControl
    Friend WithEvents IndigoGridView11 As IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup11 As IndigoLayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDdeStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColStandarCost As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDdeEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColFixedAsset As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPayroll As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInventory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAdditionalCost As DevExpress.XtraGrid.Columns.GridColumn
End Class
