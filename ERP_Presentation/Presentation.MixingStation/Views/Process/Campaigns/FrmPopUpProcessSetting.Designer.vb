<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProcessSetting
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.INDGvDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColMovementDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUserMovement = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnitMeasurementd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColMovementTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDviewMaster = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSelItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRpCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDColItemType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductCodeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBatchCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnitMeasurement = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityUnitMeasurement = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityHarnessed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityDevolution = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDirectMPQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColIndirectMPQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityRemaining = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColRawMaterialQuantityBalance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPpceInfo = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemMemoExEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemProgress = New DevExpress.XtraLayout.LayoutControlItem()
        Me.MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.INDlyProgress = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDpanelProgressbar = New DevExpress.XtraEditors.PanelControl()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPopUpMenuAction = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDbtnHarnessed = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBtnQuantityRemaining = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBtnIndirectRawMaterial = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        CType(Me.INDGvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRpCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPpceInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyProgress.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelProgressbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelProgressbar.SuspendLayout()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDPopUpMenuAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDGvDetails
        '
        Me.INDGvDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDetails.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDetails.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDetails.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDetails.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDetails.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDetails.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDetails.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDetails.Appearance.Row.Options.UseFont = True
        Me.INDGvDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColMovementDate, Me.INDColUserMovement, Me.INDColQuantity, Me.INDColUnitMeasurementd, Me.INDColMovementTypeName})
        Me.INDGvDetails.GridControl = Me.INDgcDetails
        Me.INDGvDetails.Name = "INDGvDetails"
        Me.INDGvDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDetails.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDetails.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDetails.OptionsView.ShowDetailButtons = False
        Me.INDGvDetails.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDetails, False)
        '
        'INDColMovementDate
        '
        Me.INDColMovementDate.Caption = "Fecha Movimiento"
        Me.INDColMovementDate.DisplayFormat.FormatString = "d"
        Me.INDColMovementDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColMovementDate.FieldName = "MovementDate"
        Me.INDColMovementDate.Name = "INDColMovementDate"
        Me.INDColMovementDate.OptionsColumn.AllowEdit = False
        Me.INDColMovementDate.OptionsColumn.AllowFocus = False
        Me.INDColMovementDate.Visible = True
        Me.INDColMovementDate.VisibleIndex = 0
        '
        'INDColUserMovement
        '
        Me.INDColUserMovement.Caption = "Movimiento"
        Me.INDColUserMovement.FieldName = "UserMovementDescription"
        Me.INDColUserMovement.Name = "INDColUserMovement"
        Me.INDColUserMovement.OptionsColumn.AllowEdit = False
        Me.INDColUserMovement.OptionsColumn.AllowFocus = False
        Me.INDColUserMovement.Visible = True
        Me.INDColUserMovement.VisibleIndex = 4
        '
        'INDColQuantity
        '
        Me.INDColQuantity.Caption = "Cantidad"
        Me.INDColQuantity.DisplayFormat.FormatString = "n2"
        Me.INDColQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantity.FieldName = "CKQuantity"
        Me.INDColQuantity.Name = "INDColQuantity"
        Me.INDColQuantity.OptionsColumn.AllowEdit = False
        Me.INDColQuantity.OptionsColumn.AllowFocus = False
        Me.INDColQuantity.Visible = True
        Me.INDColQuantity.VisibleIndex = 1
        '
        'INDColUnitMeasurementd
        '
        Me.INDColUnitMeasurementd.Caption = "U. Medida"
        Me.INDColUnitMeasurementd.FieldName = "UnitMeasurement"
        Me.INDColUnitMeasurementd.Name = "INDColUnitMeasurementd"
        Me.INDColUnitMeasurementd.OptionsColumn.AllowEdit = False
        Me.INDColUnitMeasurementd.OptionsColumn.AllowFocus = False
        Me.INDColUnitMeasurementd.Visible = True
        Me.INDColUnitMeasurementd.VisibleIndex = 2
        '
        'INDColMovementTypeName
        '
        Me.INDColMovementTypeName.Caption = "Tipo de Movimiento"
        Me.INDColMovementTypeName.FieldName = "MovementTypeName"
        Me.INDColMovementTypeName.Name = "INDColMovementTypeName"
        Me.INDColMovementTypeName.OptionsColumn.AllowEdit = False
        Me.INDColMovementTypeName.OptionsColumn.AllowFocus = False
        Me.INDColMovementTypeName.Visible = True
        Me.INDColMovementTypeName.VisibleIndex = 3
        '
        'INDgcDetails
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetails, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetails, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetails, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetails, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetails, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetails, False)
        GridLevelNode1.LevelTemplate = Me.INDGvDetails
        GridLevelNode1.RelationName = "Movimientos"
        Me.INDgcDetails.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.INDgcDetails.Location = New System.Drawing.Point(24, 24)
        Me.INDgcDetails.MainView = Me.INDviewMaster
        Me.INDgcDetails.Name = "INDgcDetails"
        Me.INDgcDetails.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDPpceInfo, Me.RepositoryItemMemoExEdit1, Me.INDRpCheckEdit})
        Me.INDgcDetails.Size = New System.Drawing.Size(946, 605)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetails, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDetails.TabIndex = 4
        Me.INDgcDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewMaster, Me.INDGvDetails})
        '
        'INDviewMaster
        '
        Me.INDviewMaster.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewMaster.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewMaster.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewMaster.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewMaster.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewMaster.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewMaster.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewMaster.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewMaster.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewMaster.Appearance.Row.Options.UseFont = True
        Me.INDviewMaster.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewMaster.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewMaster.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSelItem, Me.INDColItemType, Me.INDColProductCodeName, Me.INDColBatchCode, Me.INDColUnitMeasurement, Me.INDColQuantityUnitMeasurement, Me.INDColQuantityHarnessed, Me.INDColQuantityDevolution, Me.INDColDirectMPQuantity, Me.INDColIndirectMPQuantity, Me.INDColQuantityRemaining, Me.INDColRawMaterialQuantityBalance})
        Me.INDviewMaster.GridControl = Me.INDgcDetails
        Me.INDviewMaster.GroupCount = 1
        Me.INDviewMaster.Name = "INDviewMaster"
        Me.INDviewMaster.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewMaster.OptionsDetail.AllowExpandEmptyDetails = True
        Me.INDviewMaster.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewMaster.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewMaster.OptionsView.ShowAutoFilterRow = True
        Me.INDviewMaster.OptionsView.ShowGroupPanel = False
        Me.INDviewMaster.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColItemType, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewMaster, False)
        '
        'INDColSelItem
        '
        Me.INDColSelItem.Caption = "Sel."
        Me.INDColSelItem.ColumnEdit = Me.INDRpCheckEdit
        Me.INDColSelItem.FieldName = "IsSelected"
        Me.INDColSelItem.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDColSelItem.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.undcheck
        Me.INDColSelItem.Name = "INDColSelItem"
        Me.INDColSelItem.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelItem.OptionsColumn.AllowMove = False
        Me.INDColSelItem.OptionsColumn.AllowShowHide = False
        Me.INDColSelItem.OptionsColumn.AllowSize = False
        Me.INDColSelItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColSelItem.OptionsColumn.FixedWidth = True
        Me.INDColSelItem.Visible = True
        Me.INDColSelItem.VisibleIndex = 0
        Me.INDColSelItem.Width = 50
        '
        'INDRpCheckEdit
        '
        Me.INDRpCheckEdit.AutoHeight = False
        Me.INDRpCheckEdit.Name = "INDRpCheckEdit"
        '
        'INDColItemType
        '
        Me.INDColItemType.Caption = "Tipo"
        Me.INDColItemType.FieldName = "ItemTypeName"
        Me.INDColItemType.Name = "INDColItemType"
        Me.INDColItemType.Visible = True
        Me.INDColItemType.VisibleIndex = 1
        '
        'INDColProductCodeName
        '
        Me.INDColProductCodeName.Caption = "Producto"
        Me.INDColProductCodeName.FieldName = "ProductCodeName"
        Me.INDColProductCodeName.Name = "INDColProductCodeName"
        Me.INDColProductCodeName.OptionsColumn.AllowEdit = False
        Me.INDColProductCodeName.OptionsColumn.AllowFocus = False
        Me.INDColProductCodeName.ShowUnboundExpressionMenu = True
        Me.INDColProductCodeName.Visible = True
        Me.INDColProductCodeName.VisibleIndex = 1
        Me.INDColProductCodeName.Width = 128
        '
        'INDColBatchCode
        '
        Me.INDColBatchCode.Caption = "Lote"
        Me.INDColBatchCode.FieldName = "BatchCode"
        Me.INDColBatchCode.Name = "INDColBatchCode"
        Me.INDColBatchCode.OptionsColumn.AllowEdit = False
        Me.INDColBatchCode.OptionsColumn.AllowFocus = False
        Me.INDColBatchCode.Visible = True
        Me.INDColBatchCode.VisibleIndex = 2
        Me.INDColBatchCode.Width = 60
        '
        'INDColUnitMeasurement
        '
        Me.INDColUnitMeasurement.Caption = "U. Medida"
        Me.INDColUnitMeasurement.FieldName = "UnitMeasurement"
        Me.INDColUnitMeasurement.Name = "INDColUnitMeasurement"
        Me.INDColUnitMeasurement.OptionsColumn.AllowEdit = False
        Me.INDColUnitMeasurement.OptionsColumn.AllowFocus = False
        Me.INDColUnitMeasurement.Visible = True
        Me.INDColUnitMeasurement.VisibleIndex = 3
        Me.INDColUnitMeasurement.Width = 60
        '
        'INDColQuantityUnitMeasurement
        '
        Me.INDColQuantityUnitMeasurement.Caption = "Cant. Entregada"
        Me.INDColQuantityUnitMeasurement.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityUnitMeasurement.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityUnitMeasurement.FieldName = "QuantityUnitMeasurement"
        Me.INDColQuantityUnitMeasurement.Name = "INDColQuantityUnitMeasurement"
        Me.INDColQuantityUnitMeasurement.OptionsColumn.AllowEdit = False
        Me.INDColQuantityUnitMeasurement.OptionsColumn.AllowFocus = False
        Me.INDColQuantityUnitMeasurement.ToolTip = "Cantidad validada como tipo entrada del producto/Lote"
        Me.INDColQuantityUnitMeasurement.Visible = True
        Me.INDColQuantityUnitMeasurement.VisibleIndex = 4
        Me.INDColQuantityUnitMeasurement.Width = 125
        '
        'INDColQuantityHarnessed
        '
        Me.INDColQuantityHarnessed.Caption = "Remanente utilizado"
        Me.INDColQuantityHarnessed.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityHarnessed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityHarnessed.FieldName = "QuantityHarnessed"
        Me.INDColQuantityHarnessed.Name = "INDColQuantityHarnessed"
        Me.INDColQuantityHarnessed.OptionsColumn.AllowEdit = False
        Me.INDColQuantityHarnessed.OptionsColumn.AllowFocus = False
        Me.INDColQuantityHarnessed.ToolTip = "Sumatoria de aprovechamientos del producto/Lote"
        Me.INDColQuantityHarnessed.Visible = True
        Me.INDColQuantityHarnessed.VisibleIndex = 5
        Me.INDColQuantityHarnessed.Width = 80
        '
        'INDColQuantityDevolution
        '
        Me.INDColQuantityDevolution.Caption = "Devolución"
        Me.INDColQuantityDevolution.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityDevolution.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityDevolution.FieldName = "QuantityDevolutionUnitMeasurement"
        Me.INDColQuantityDevolution.Name = "INDColQuantityDevolution"
        Me.INDColQuantityDevolution.OptionsColumn.AllowEdit = False
        Me.INDColQuantityDevolution.OptionsColumn.AllowFocus = False
        Me.INDColQuantityDevolution.ToolTip = "Sumatoria de devoluciones del producto/lote"
        Me.INDColQuantityDevolution.Visible = True
        Me.INDColQuantityDevolution.VisibleIndex = 6
        Me.INDColQuantityDevolution.Width = 80
        '
        'INDColDirectMPQuantity
        '
        Me.INDColDirectMPQuantity.Caption = "MP Directa"
        Me.INDColDirectMPQuantity.DisplayFormat.FormatString = "n2"
        Me.INDColDirectMPQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColDirectMPQuantity.FieldName = "DirectMPQuantity"
        Me.INDColDirectMPQuantity.Name = "INDColDirectMPQuantity"
        Me.INDColDirectMPQuantity.OptionsColumn.AllowEdit = False
        Me.INDColDirectMPQuantity.OptionsColumn.AllowFocus = False
        Me.INDColDirectMPQuantity.ToolTip = "MP Utilizada en Gestion de MP"
        Me.INDColDirectMPQuantity.Visible = True
        Me.INDColDirectMPQuantity.VisibleIndex = 7
        Me.INDColDirectMPQuantity.Width = 80
        '
        'INDColIndirectMPQuantity
        '
        Me.INDColIndirectMPQuantity.Caption = "MP Indirecta"
        Me.INDColIndirectMPQuantity.DisplayFormat.FormatString = "n2"
        Me.INDColIndirectMPQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColIndirectMPQuantity.FieldName = "IndirectMPQuantity"
        Me.INDColIndirectMPQuantity.Name = "INDColIndirectMPQuantity"
        Me.INDColIndirectMPQuantity.OptionsColumn.AllowEdit = False
        Me.INDColIndirectMPQuantity.OptionsColumn.AllowFocus = False
        Me.INDColIndirectMPQuantity.ToolTip = "MP Procesada como MP Indirecta"
        Me.INDColIndirectMPQuantity.Visible = True
        Me.INDColIndirectMPQuantity.VisibleIndex = 8
        Me.INDColIndirectMPQuantity.Width = 80
        '
        'INDColQuantityRemaining
        '
        Me.INDColQuantityRemaining.Caption = "Remanentes"
        Me.INDColQuantityRemaining.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityRemaining.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityRemaining.FieldName = "QuantityRemaining"
        Me.INDColQuantityRemaining.Name = "INDColQuantityRemaining"
        Me.INDColQuantityRemaining.OptionsColumn.AllowEdit = False
        Me.INDColQuantityRemaining.OptionsColumn.AllowFocus = False
        Me.INDColQuantityRemaining.ToolTip = "Sumatoria de sobrantes del producto/lote"
        Me.INDColQuantityRemaining.Visible = True
        Me.INDColQuantityRemaining.VisibleIndex = 9
        Me.INDColQuantityRemaining.Width = 80
        '
        'INDColRawMaterialQuantityBalance
        '
        Me.INDColRawMaterialQuantityBalance.Caption = "Saldo"
        Me.INDColRawMaterialQuantityBalance.DisplayFormat.FormatString = "n2"
        Me.INDColRawMaterialQuantityBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColRawMaterialQuantityBalance.FieldName = "RawMaterialQuantityBalance"
        Me.INDColRawMaterialQuantityBalance.Name = "INDColRawMaterialQuantityBalance"
        Me.INDColRawMaterialQuantityBalance.OptionsColumn.AllowEdit = False
        Me.INDColRawMaterialQuantityBalance.OptionsColumn.AllowFocus = False
        Me.INDColRawMaterialQuantityBalance.Visible = True
        Me.INDColRawMaterialQuantityBalance.VisibleIndex = 10
        Me.INDColRawMaterialQuantityBalance.Width = 98
        '
        'INDPpceInfo
        '
        Me.INDPpceInfo.AutoHeight = False
        Me.INDPpceInfo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPpceInfo.Name = "INDPpceInfo"
        Me.INDPpceInfo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'RepositoryItemMemoExEdit1
        '
        Me.RepositoryItemMemoExEdit1.AutoHeight = False
        Me.RepositoryItemMemoExEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExEdit1.Name = "RepositoryItemMemoExEdit1"
        Me.RepositoryItemMemoExEdit1.ReadOnly = True
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemProgress})
        Me.Root.Name = "Root"
        Me.Root.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.Root.Size = New System.Drawing.Size(990, 18)
        Me.Root.TextVisible = False
        '
        'INDlyItemProgress
        '
        Me.INDlyItemProgress.Control = Me.MarqueeProgressBarControl1
        Me.INDlyItemProgress.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemProgress.MaxSize = New System.Drawing.Size(0, 10)
        Me.INDlyItemProgress.MinSize = New System.Drawing.Size(1, 10)
        Me.INDlyItemProgress.Name = "INDlyItemProgress"
        Me.INDlyItemProgress.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlyItemProgress.Size = New System.Drawing.Size(990, 18)
        Me.INDlyItemProgress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemProgress.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemProgress.TextVisible = False
        Me.INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'MarqueeProgressBarControl1
        '
        Me.MarqueeProgressBarControl1.EditValue = 0
        Me.MarqueeProgressBarControl1.Location = New System.Drawing.Point(0, 0)
        Me.MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
        Me.MarqueeProgressBarControl1.Size = New System.Drawing.Size(990, 10)
        Me.MarqueeProgressBarControl1.StyleController = Me.INDlyProgress
        Me.MarqueeProgressBarControl1.TabIndex = 4
        '
        'INDlyProgress
        '
        Me.INDlyProgress.Controls.Add(Me.MarqueeProgressBarControl1)
        Me.INDlyProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyProgress.Location = New System.Drawing.Point(2, 2)
        Me.INDlyProgress.Name = "INDlyProgress"
        Me.INDlyProgress.Root = Me.Root
        Me.INDlyProgress.Size = New System.Drawing.Size(990, 18)
        Me.INDlyProgress.TabIndex = 0
        Me.INDlyProgress.Text = "LayoutControl1"
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygDetails})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(994, 653)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygDetails
        '
        Me.INDlygDetails.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetails.AppearanceGroup.Options.UseFont = True
        Me.INDlygDetails.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetails.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDetails, False)
        Me.INDlygDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDetails})
        Me.INDlygDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDlygDetails.Name = "INDlygDetails"
        Me.INDlygDetails.Size = New System.Drawing.Size(974, 633)
        Me.INDlygDetails.Text = "Código - Nombre"
        Me.INDlygDetails.TextVisible = False
        '
        'INDlyItemDetails
        '
        Me.INDlyItemDetails.Control = Me.INDgcDetails
        Me.INDlyItemDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemDetails.Name = "INDlyItemDetails"
        Me.INDlyItemDetails.Size = New System.Drawing.Size(950, 609)
        Me.INDlyItemDetails.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemDetails.TextVisible = False
        '
        'INDpanelProgressbar
        '
        Me.INDpanelProgressbar.Controls.Add(Me.INDlyProgress)
        Me.INDpanelProgressbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpanelProgressbar.Location = New System.Drawing.Point(0, 0)
        Me.INDpanelProgressbar.Name = "INDpanelProgressbar"
        Me.INDpanelProgressbar.Size = New System.Drawing.Size(994, 22)
        Me.INDpanelProgressbar.TabIndex = 0
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDgcDetails)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(0, 22)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(994, 653)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDPopUpMenuAction
        '
        Me.INDPopUpMenuAction.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbtnHarnessed), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBtnQuantityRemaining), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBtnIndirectRawMaterial)})
        Me.INDPopUpMenuAction.Manager = Me.BarManager1
        Me.INDPopUpMenuAction.Name = "INDPopUpMenuAction"
        '
        'INDbtnHarnessed
        '
        Me.INDbtnHarnessed.Caption = "Remanente utilizado en campaña"
        Me.INDbtnHarnessed.Id = 1
        Me.INDbtnHarnessed.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Add_16x16_blue
        Me.INDbtnHarnessed.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDbtnHarnessed.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbtnHarnessed.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDbtnHarnessed.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbtnHarnessed.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDbtnHarnessed.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbtnHarnessed.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDbtnHarnessed.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbtnHarnessed.Name = "INDbtnHarnessed"
        '
        'INDBtnQuantityRemaining
        '
        Me.INDBtnQuantityRemaining.Caption = "Ingreso de remanentes"
        Me.INDBtnQuantityRemaining.Id = 0
        Me.INDBtnQuantityRemaining.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Add_16x16_blue
        Me.INDBtnQuantityRemaining.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDBtnQuantityRemaining.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBtnQuantityRemaining.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDBtnQuantityRemaining.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBtnQuantityRemaining.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDBtnQuantityRemaining.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBtnQuantityRemaining.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBtnQuantityRemaining.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBtnQuantityRemaining.Name = "INDBtnQuantityRemaining"
        '
        'INDBtnIndirectRawMaterial
        '
        Me.INDBtnIndirectRawMaterial.Caption = "Materia Prima Indirecta"
        Me.INDBtnIndirectRawMaterial.Id = 2
        Me.INDBtnIndirectRawMaterial.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Add_16x16_blue
        Me.INDBtnIndirectRawMaterial.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDBtnIndirectRawMaterial.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBtnIndirectRawMaterial.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDBtnIndirectRawMaterial.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBtnIndirectRawMaterial.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDBtnIndirectRawMaterial.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBtnIndirectRawMaterial.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDBtnIndirectRawMaterial.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBtnIndirectRawMaterial.Name = "INDBtnIndirectRawMaterial"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBtnQuantityRemaining, Me.INDbtnHarnessed, Me.INDBtnIndirectRawMaterial})
        Me.BarManager1.MaxItemId = 3
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(994, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 675)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(994, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 675)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(994, 0)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 675)
        '
        'FrmProcessSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 675)
        Me.Controls.Add(Me.INDlyRoot)
        Me.Controls.Add(Me.INDpanelProgressbar)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProcessSetting"
        Me.Text = "Campaña # 0"
        CType(Me.INDGvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRpCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPpceInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyProgress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyProgress.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelProgressbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelProgressbar.ResumeLayout(False)
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDPopUpMenuAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents INDpanelProgressbar As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyProgress As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemProgress As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewMaster As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProductCodeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantityUnitMeasurement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantityHarnessed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantityDevolution As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlygDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDColIndirectMPQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPpceInfo As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColDirectMPQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantityRemaining As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColRawMaterialQuantityBalance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoExEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents INDGvDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColMovementDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUserMovement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColMovementTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPopUpMenuAction As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBtnQuantityRemaining As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbtnHarnessed As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBtnIndirectRawMaterial As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDColUnitMeasurementd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBatchCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUnitMeasurement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColItemType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSelItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRpCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
