Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProductCoverage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProductCoverage))
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
        Me.INDEsbProductRate = New Presentation.Controls.ExportStructureButton()
        Me.INDsbAddDetail = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcProduct = New DevExpress.XtraGrid.GridControl()
        Me.INDgvProducts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColClass = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColValidityAlert = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckContrated = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDrepCheckQuoted = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDTEValidityAlert = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDbtnAddRules = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcRules = New DevExpress.XtraGrid.GridControl()
        Me.viewRules = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolRule = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameEntity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgProductRange = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygRules = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAddRules = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemGridRules = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
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
        Me.PopupMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
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
        CType(Me.INDgcProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckContrated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckQuoted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTEValidityAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcRules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewRules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgProductRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygRules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddRules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridRules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(865, 348)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(865, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(865, 130)
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
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 338)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.AllowCustomization = False
        Me.INDlcRoot.Controls.Add(Me.INDBtnImportFile)
        Me.INDlcRoot.Controls.Add(Me.INDEsbProductRate)
        Me.INDlcRoot.Controls.Add(Me.INDsbAddDetail)
        Me.INDlcRoot.Controls.Add(Me.INDgcProduct)
        Me.INDlcRoot.Controls.Add(Me.INDtxtName)
        Me.INDlcRoot.Controls.Add(Me.INDbteCode)
        Me.INDlcRoot.Controls.Add(Me.INDbtnAddRules)
        Me.INDlcRoot.Controls.Add(Me.INDgcRules)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, False)
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 8)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1907, 193, 698, 359)
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(661, 338)
        Me.INDlcRoot.TabIndex = 2
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFile.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(1204, 45)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(36, 34)
        Me.INDBtnImportFile.StyleController = Me.INDlcRoot
        Me.INDBtnImportFile.TabIndex = 24
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDEsbProductRate
        '
        Me.INDEsbProductRate.ImageOptions.Image = CType(resources.GetObject("INDEsbProductRate.ImageOptions.Image"), System.Drawing.Image)
        Me.INDEsbProductRate.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbProductRate.Location = New System.Drawing.Point(1166, 45)
        Me.INDEsbProductRate.Name = "INDEsbProductRate"
        Me.INDEsbProductRate.Size = New System.Drawing.Size(36, 34)
        Me.INDEsbProductRate.StyleController = Me.INDlcRoot
        Me.INDEsbProductRate.TabIndex = 23
        Me.INDEsbProductRate.Text = "ExportStructureButton3"
        Me.INDEsbProductRate.ToolTip = "Exportar Estructura"
        '
        'INDsbAddDetail
        '
        Me.INDsbAddDetail.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAddDetail.Appearance.Options.UseFont = True
        Me.INDsbAddDetail.Location = New System.Drawing.Point(-8, 45)
        Me.INDsbAddDetail.Name = "INDsbAddDetail"
        Me.INDsbAddDetail.Size = New System.Drawing.Size(1172, 34)
        Me.INDsbAddDetail.StyleController = Me.INDlcRoot
        Me.INDsbAddDetail.TabIndex = 9
        Me.INDsbAddDetail.Text = "Agregar"
        '
        'INDgcProduct
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcProduct, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcProduct, Nothing)
        Me.INDgcProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcProduct, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcProduct, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcProduct, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcProduct, False)
        Me.INDgcProduct.Location = New System.Drawing.Point(-8, 81)
        Me.INDgcProduct.MainView = Me.INDgvProducts
        Me.INDgcProduct.Name = "INDgcProduct"
        Me.INDgcProduct.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCheckContrated, Me.INDrepCheckQuoted, Me.INDTEValidityAlert})
        Me.INDgcProduct.Size = New System.Drawing.Size(1248, 224)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcProduct, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcProduct.TabIndex = 7
        Me.INDgcProduct.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvProducts})
        '
        'INDgvProducts
        '
        Me.INDgvProducts.ActiveFilterString = "[INDColValidityAlert] = 'Vigente'"
        Me.INDgvProducts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvProducts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvProducts.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvProducts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvProducts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvProducts.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvProducts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvProducts.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvProducts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvProducts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvProducts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvProducts.Appearance.Row.Options.UseFont = True
        Me.INDgvProducts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvProducts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvProducts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColClass, Me.INDColCode, Me.INDColName, Me.GridColumn11, Me.GridColumn13, Me.GridColumn15, Me.GridColumn18, Me.GridColumn19, Me.GridColumn4, Me.GridColumn5, Me.INDColValidityAlert, Me.GridColumn20, Me.GridColumn6, Me.GridColumn9, Me.GridColumn3, Me.GridColumn17, Me.GridColumn7, Me.GridColumn21, Me.GridColumn22, Me.GridColumn8})
        Me.INDgvProducts.GridControl = Me.INDgcProduct
        Me.INDgvProducts.GroupCount = 1
        Me.INDgvProducts.Name = "INDgvProducts"
        Me.INDgvProducts.OptionsSelection.MultiSelect = True
        Me.INDgvProducts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvProducts.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvProducts.OptionsView.ShowAutoFilterRow = True
        Me.INDgvProducts.OptionsView.ShowDetailButtons = False
        Me.INDgvProducts.OptionsView.ShowFooter = True
        Me.INDgvProducts.OptionsView.ShowGroupPanel = False
        Me.INDgvProducts.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColClass, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDgvProducts, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvProducts, False)
        '
        'INDColClass
        '
        Me.INDColClass.Caption = "Clase"
        Me.INDColClass.FieldName = "RateClassName"
        Me.INDColClass.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.INDColClass.Name = "INDColClass"
        Me.INDColClass.OptionsColumn.AllowEdit = False
        Me.INDColClass.OptionsColumn.AllowFocus = False
        Me.INDColClass.Visible = True
        Me.INDColClass.VisibleIndex = 0
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Código"
        Me.INDColCode.FieldName = "INDColCode"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.UnboundExpression = "Iif(IsNullOrEmpty([ProductCode]), [PackageCode], [ProductCode])"
        Me.INDColCode.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 0
        Me.INDColCode.Width = 81
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "INDColName"
        Me.INDColName.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.OptionsColumn.AllowFocus = False
        Me.INDColName.UnboundExpression = "Iif(IsNullOrEmpty([ProductName]), [PackageName], [ProductName])"
        Me.INDColName.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 1
        Me.INDColName.Width = 184
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Tipo de Liquidación"
        Me.GridColumn11.FieldName = "GridColumn11"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.UnboundExpression = "Iif([LiquidationType] = 0, 'No Aplica', [LiquidationType] = 1, 'Tarifa', [Liquida" &
    "tionType] = 2, 'Servicio', 'Tarifa-Servicio')"
        Me.GridColumn11.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 2
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Tipo Tarifa"
        Me.GridColumn13.FieldName = "GridColumn13"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.UnboundExpression = "Iif([RateType] = 1, 'Tarifa Fija', [RateType] = 2, 'Base Porcentaje', 'N/A')"
        Me.GridColumn13.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Tipo Porcentaje"
        Me.GridColumn15.FieldName = "GridColumn15"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.UnboundExpression = "Iif([PercentageBasedOn] = 1, 'Costo Promedio Ponderado', [PercentageBasedOn] = 2," &
    " 'Ultimo Costo', 'N/A')"
        Me.GridColumn15.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "CUPS"
        Me.GridColumn18.FieldName = "CupsCodeName"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Desc. Relacionada"
        Me.GridColumn19.FieldName = "ContractDesCodeName"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Fecha Inicial"
        Me.GridColumn4.FieldName = "InitialDate"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 93
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Fecha Final"
        Me.GridColumn5.FieldName = "EndDate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 81
        '
        'INDColValidityAlert
        '
        Me.INDColValidityAlert.Caption = "Alerta de Vigencia"
        Me.INDColValidityAlert.FieldName = "INDColValidityAlert"
        Me.INDColValidityAlert.Name = "INDColValidityAlert"
        Me.INDColValidityAlert.OptionsColumn.AllowEdit = False
        Me.INDColValidityAlert.OptionsColumn.AllowFocus = False
        Me.INDColValidityAlert.UnboundExpression = "Iif(Now() > [EndDate], 'Caduco', [EndDate] > Now(), 'Vigente', 'N/A')"
        Me.INDColValidityAlert.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDColValidityAlert.Visible = True
        Me.INDColValidityAlert.VisibleIndex = 5
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Porcentaje"
        Me.GridColumn20.DisplayFormat.FormatString = "n2"
        Me.GridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn20.FieldName = "Percentage"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.OptionsColumn.AllowEdit = False
        Me.GridColumn20.OptionsColumn.AllowFocus = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Precio Venta"
        Me.GridColumn6.DisplayFormat.FormatString = "C0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "SalesValue"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 82
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Precio con Recargo"
        Me.GridColumn9.DisplayFormat.FormatString = "C0"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "SalesValueWithSurcharge"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Width = 91
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Contratado"
        Me.GridColumn3.FieldName = "Contracted"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Width = 89
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Estado"
        Me.GridColumn17.FieldName = "GridColumn17"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.UnboundExpression = "Iif([Status] = 1, 'Activo', 'Inactivo')"
        Me.GridColumn17.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 7
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Al Cotizar"
        Me.GridColumn7.FieldName = "Quoted"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Width = 105
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Producto de Control"
        Me.GridColumn21.FieldName = "ProductControl"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Control de Precios"
        Me.GridColumn22.FieldName = "ProductWithPriceControl"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Observaciones"
        Me.GridColumn8.FieldName = "Observations"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
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
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(-414, 131)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.Size = New System.Drawing.Size(388, 28)
        Me.INDtxtName.StyleController = Me.INDlcRoot
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, False)
        Me.INDbteCode.Location = New System.Drawing.Point(-414, 71)
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
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Size = New System.Drawing.Size(388, 28)
        Me.INDbteCode.StyleController = Me.INDlcRoot
        Me.INDbteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDbtnAddRules
        '
        Me.INDbtnAddRules.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddRules.Appearance.Options.UseFont = True
        Me.INDbtnAddRules.Location = New System.Drawing.Point(1262, 50)
        Me.INDbtnAddRules.Name = "INDbtnAddRules"
        Me.INDbtnAddRules.Size = New System.Drawing.Size(964, 42)
        Me.INDbtnAddRules.StyleController = Me.INDlcRoot
        Me.INDbtnAddRules.TabIndex = 3
        Me.INDbtnAddRules.Text = "Agregar"
        '
        'INDgcRules
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcRules, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcRules, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcRules, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcRules, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcRules, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcRules, False)
        Me.INDgcRules.Location = New System.Drawing.Point(1262, 94)
        Me.INDgcRules.MainView = Me.viewRules
        Me.INDgcRules.Name = "INDgcRules"
        Me.INDgcRules.Size = New System.Drawing.Size(964, 206)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcRules, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcRules.TabIndex = 4
        Me.INDgcRules.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewRules})
        '
        'viewRules
        '
        Me.viewRules.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewRules.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewRules.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewRules.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewRules.Appearance.FocusedRow.Options.UseFont = True
        Me.viewRules.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewRules.Appearance.GroupRow.Options.UseFont = True
        Me.viewRules.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewRules.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewRules.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewRules.Appearance.Row.Options.UseFont = True
        Me.viewRules.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewRules.Appearance.ViewCaption.Options.UseFont = True
        Me.viewRules.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolRule, Me.INDColNameEntity, Me.INDColInitialDate, Me.INDColEndDate})
        Me.viewRules.GridControl = Me.INDgcRules
        Me.viewRules.Name = "viewRules"
        Me.viewRules.OptionsSelection.MultiSelect = True
        Me.viewRules.OptionsView.EnableAppearanceEvenRow = True
        Me.viewRules.OptionsView.EnableAppearanceOddRow = True
        Me.viewRules.OptionsView.ShowAutoFilterRow = True
        Me.viewRules.OptionsView.ShowDetailButtons = False
        Me.viewRules.OptionsView.ShowFooter = True
        Me.viewRules.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.viewRules, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewRules, False)
        '
        'INDcolRule
        '
        Me.INDcolRule.Caption = "Clase"
        Me.INDcolRule.FieldName = "INDcolRule"
        Me.INDcolRule.Name = "INDcolRule"
        Me.INDcolRule.OptionsColumn.AllowEdit = False
        Me.INDcolRule.OptionsColumn.AllowFocus = False
        Me.INDcolRule.UnboundExpression = "Iif([RuleType] = 1, 'Tipo de Producto', [RuleType] = 2, 'Grupo de Producto', 'Sub" &
    "grupo de Producto')"
        Me.INDcolRule.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDcolRule.Visible = True
        Me.INDcolRule.VisibleIndex = 0
        Me.INDcolRule.Width = 345
        '
        'INDColNameEntity
        '
        Me.INDColNameEntity.Caption = "Nombre"
        Me.INDColNameEntity.FieldName = "EntityName"
        Me.INDColNameEntity.MinWidth = 21
        Me.INDColNameEntity.Name = "INDColNameEntity"
        Me.INDColNameEntity.OptionsColumn.AllowEdit = False
        Me.INDColNameEntity.Visible = True
        Me.INDColNameEntity.VisibleIndex = 1
        Me.INDColNameEntity.Width = 345
        '
        'INDColInitialDate
        '
        Me.INDColInitialDate.Caption = "Fecha Inicial"
        Me.INDColInitialDate.FieldName = "InitialDate"
        Me.INDColInitialDate.Name = "INDColInitialDate"
        Me.INDColInitialDate.OptionsColumn.AllowEdit = False
        Me.INDColInitialDate.OptionsColumn.AllowFocus = False
        Me.INDColInitialDate.Visible = True
        Me.INDColInitialDate.VisibleIndex = 2
        Me.INDColInitialDate.Width = 129
        '
        'INDColEndDate
        '
        Me.INDColEndDate.Caption = "Fecha Final"
        Me.INDColEndDate.FieldName = "EndDate"
        Me.INDColEndDate.Name = "INDColEndDate"
        Me.INDColEndDate.OptionsColumn.AllowEdit = False
        Me.INDColEndDate.OptionsColumn.AllowFocus = False
        Me.INDColEndDate.Visible = True
        Me.INDColEndDate.VisibleIndex = 3
        Me.INDColEndDate.Width = 129
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "Tarifa de Productos"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData, Me.INDlcgProductRange, Me.INDlygRules})
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(2676, 321)
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.CustomizationFormText = "Datos Principales"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCode, Me.INDliName})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(406, 307)
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
        'INDliName
        '
        Me.INDliName.AllowHide = False
        Me.INDliName.Control = Me.INDtxtName
        Me.INDliName.CustomizationFormText = "Nombre"
        Me.INDliName.Location = New System.Drawing.Point(0, 60)
        Me.INDliName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliName.Name = "INDliName"
        Me.INDliName.ShowInCustomizationForm = False
        Me.INDliName.Size = New System.Drawing.Size(390, 202)
        Me.INDliName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliName.Text = "Nombre"
        Me.INDliName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliName.TextToControlDistance = 5
        '
        'INDlcgProductRange
        '
        Me.INDlcgProductRange.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgProductRange.AppearanceGroup.Options.UseFont = True
        Me.INDlcgProductRange.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgProductRange.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgProductRange.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgProductRange.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgProductRange.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgProductRange.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgProductRange.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgProductRange.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgProductRange.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgProductRange.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgProductRange.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgProductRange.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgProductRange, False)
        Me.INDlcgProductRange.CustomizationFormText = "Tarifa de Productos"
        Me.INDlcgProductRange.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDliAddDetail, Me.LayoutControlItem1, Me.LayoutControlItem3})
        Me.INDlcgProductRange.Location = New System.Drawing.Point(406, 0)
        Me.INDlcgProductRange.Name = "INDlcgProductRange"
        Me.INDlcgProductRange.Size = New System.Drawing.Size(1266, 307)
        Me.INDlcgProductRange.Text = "Tarifa de Productos"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AllowHide = False
        Me.LayoutControlItem2.Control = Me.INDgcProduct
        Me.LayoutControlItem2.CustomizationFormText = "Tarifas"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(1250, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(1250, 1)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1250, 226)
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
        Me.LayoutControlItem1.Control = Me.INDEsbProductRate
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
        'INDlygRules
        '
        Me.INDlygRules.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygRules.AppearanceGroup.Options.UseFont = True
        Me.INDlygRules.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygRules.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygRules.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygRules.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygRules.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygRules.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygRules.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygRules.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygRules.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygRules.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygRules.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygRules.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygRules, False)
        Me.INDlygRules.CustomizationFormText = "Reglas"
        Me.INDlygRules.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAddRules, Me.INDlyItemGridRules})
        Me.INDlygRules.Location = New System.Drawing.Point(1672, 0)
        Me.INDlygRules.Name = "INDlygRules"
        Me.INDlygRules.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 10, 11, 11)
        Me.INDlygRules.Size = New System.Drawing.Size(990, 307)
        Me.INDlygRules.Text = "Tarifa General"
        '
        'INDlyItemAddRules
        '
        Me.INDlyItemAddRules.Control = Me.INDbtnAddRules
        Me.INDlyItemAddRules.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemAddRules.CustomizationFormText = "Agregar Reglas"
        Me.INDlyItemAddRules.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAddRules.MaxSize = New System.Drawing.Size(966, 44)
        Me.INDlyItemAddRules.MinSize = New System.Drawing.Size(966, 44)
        Me.INDlyItemAddRules.Name = "INDlyItemAddRules"
        Me.INDlyItemAddRules.Size = New System.Drawing.Size(966, 44)
        Me.INDlyItemAddRules.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddRules.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddRules.TextVisible = False
        '
        'INDlyItemGridRules
        '
        Me.INDlyItemGridRules.Control = Me.INDgcRules
        Me.INDlyItemGridRules.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemGridRules.CustomizationFormText = "Listado de Reglas"
        Me.INDlyItemGridRules.Location = New System.Drawing.Point(0, 44)
        Me.INDlyItemGridRules.MaxSize = New System.Drawing.Size(966, 0)
        Me.INDlyItemGridRules.MinSize = New System.Drawing.Size(966, 1)
        Me.INDlyItemGridRules.Name = "INDlyItemGridRules"
        Me.INDlyItemGridRules.Size = New System.Drawing.Size(966, 208)
        Me.INDlyItemGridRules.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridRules.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridRules.TextVisible = False
        '
        'IndigoGridControl1
        '
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
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
        Me.barDockControlTop.Size = New System.Drawing.Size(865, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 484)
        Me.barDockControlBottom.Manager = Me.BarManager
        Me.barDockControlBottom.Size = New System.Drawing.Size(865, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlLeft.Manager = Me.BarManager
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 478)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(865, 6)
        Me.barDockControlRight.Manager = Me.BarManager
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 478)
        '
        'INDbarButtonSelectContrated
        '
        Me.INDbarButtonSelectContrated.Caption = "Seleccionar Contratados"
        Me.INDbarButtonSelectContrated.Id = 2
        Me.INDbarButtonSelectContrated.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.Add_16x16_blue
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
        Me.INDbarButtonUnSelectContrated.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.Add_16x16_blue
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
        Me.INDbarButtonSelectQuoted.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.Add_16x16_blue
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
        Me.INDbarButtonUnSelectQuoted.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.Add_16x16_blue
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
        Me.INDbarButtonInactivate.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.Add_16x16_blue
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
        Me.INDbarButtonEdit.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.Add_16x16_blue
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
        Me.INDBtnActivate.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.Add_16x16_blue
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
        'PopupMenuActions
        '
        Me.PopupMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonSelectContrated), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonUnSelectContrated), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonSelectQuoted), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonUnSelectQuoted), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonInactivate), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonEdit), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBtnActivate)})
        Me.PopupMenuActions.Manager = Me.BarManager
        Me.PopupMenuActions.Name = "PopupMenuActions"
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        Me.IndigoGridView11.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'FrmProductCoverage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(865, 484)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProductCoverage"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "306"
        Me.Text = "Tarifa de Productos"
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
        CType(Me.INDgcProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckContrated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckQuoted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTEValidityAlert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcRules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewRules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgProductRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygRules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddRules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridRules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcProduct As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvProducts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgProductRange As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsbAddDetail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDliAddDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDEsbProductRate As Presentation.Controls.ExportStructureButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents INDColValidityAlert As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTEValidityAlert As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnActivate As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColClass As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView11 As IndigoGridView
    Friend WithEvents INDbtnAddRules As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgcRules As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewRules As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolRule As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEndDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlygRules As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemAddRules As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemGridRules As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColNameEntity As DevExpress.XtraGrid.Columns.GridColumn
End Class
