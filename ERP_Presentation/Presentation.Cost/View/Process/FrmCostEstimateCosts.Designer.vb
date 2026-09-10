Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostEstimateCosts
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
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcEstimationData = New DevExpress.XtraGrid.GridControl()
        Me.ViewInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProductionCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialExpenseDirect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialExpenseVariable = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialManpower = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialFixedAsset = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialDispensing = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialTransfer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialDistribution = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSecondaryExpenseDirect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSecondaryExpenseVariable = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSecondaryManPower = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSecondaryFixedAsset = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSecondaryDispensing = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSecondaryTransfer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSecondaryDistribution = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GleEstimationType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ChkListOptions = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCheckOptions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LcgDataEstimation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LciEstimateDataSimulate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoCheckedListBoxControl1 = New Presentation.Controls.IndigoCheckedListBoxControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDGcEstimationData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GleEstimationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkListOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCheckOptions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgDataEstimation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LciEstimateDataSimulate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckedListBoxControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1489, 494)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1489, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1489, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 485)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDGcEstimationData)
        Me.INDlcRoot.Controls.Add(Me.GleEstimationType)
        Me.INDlcRoot.Controls.Add(Me.ChkListOptions)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(1285, 485)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDGcEstimationData
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcEstimationData, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcEstimationData, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcEstimationData, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcEstimationData, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcEstimationData, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcEstimationData, False)
        Me.INDGcEstimationData.Location = New System.Drawing.Point(438, 59)
        Me.INDGcEstimationData.MainView = Me.ViewInfo
        Me.INDGcEstimationData.Name = "INDGcEstimationData"
        Me.INDGcEstimationData.Size = New System.Drawing.Size(824, 385)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcEstimationData, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcEstimationData.TabIndex = 9
        Me.INDGcEstimationData.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewInfo})
        '
        'ViewInfo
        '
        Me.ViewInfo.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.ViewInfo.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ViewInfo.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.ViewInfo.Appearance.FocusedRow.Options.UseFont = True
        Me.ViewInfo.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewInfo.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewInfo.Appearance.GroupRow.Options.UseFont = True
        Me.ViewInfo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewInfo.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewInfo.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ViewInfo.Appearance.Row.Options.UseFont = True
        Me.ViewInfo.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.ViewInfo.Appearance.ViewCaption.Options.UseFont = True
        Me.ViewInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProductionCenter, Me.INDColInitialExpenseDirect, Me.INDColInitialExpenseVariable, Me.INDColInitialManpower, Me.INDColInitialFixedAsset, Me.INDColInitialDispensing, Me.INDColInitialTransfer, Me.INDColInitialDistribution, Me.INDColSecondaryExpenseDirect, Me.INDColSecondaryExpenseVariable, Me.INDColSecondaryManPower, Me.INDColSecondaryFixedAsset, Me.INDColSecondaryDispensing, Me.INDColSecondaryTransfer, Me.INDColSecondaryDistribution})
        Me.ViewInfo.GridControl = Me.INDGcEstimationData
        Me.ViewInfo.Name = "ViewInfo"
        Me.ViewInfo.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewInfo.OptionsView.EnableAppearanceOddRow = True
        Me.ViewInfo.OptionsView.ShowAutoFilterRow = True
        Me.ViewInfo.OptionsView.ShowDetailButtons = False
        Me.ViewInfo.OptionsView.ShowFooter = True
        Me.ViewInfo.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.ViewInfo, False)
        '
        'INDColProductionCenter
        '
        Me.INDColProductionCenter.Caption = "Centro Producción"
        Me.INDColProductionCenter.FieldName = "ProductionCenterName"
        Me.INDColProductionCenter.Name = "INDColProductionCenter"
        Me.INDColProductionCenter.OptionsColumn.AllowEdit = False
        Me.INDColProductionCenter.OptionsColumn.AllowFocus = False
        Me.INDColProductionCenter.Visible = True
        Me.INDColProductionCenter.VisibleIndex = 0
        Me.INDColProductionCenter.Width = 101
        '
        'INDColInitialExpenseDirect
        '
        Me.INDColInitialExpenseDirect.Caption = "Gastos Directos"
        Me.INDColInitialExpenseDirect.DisplayFormat.FormatString = "C2"
        Me.INDColInitialExpenseDirect.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInitialExpenseDirect.FieldName = "DirectCostDistribution"
        Me.INDColInitialExpenseDirect.Name = "INDColInitialExpenseDirect"
        Me.INDColInitialExpenseDirect.OptionsColumn.AllowEdit = False
        Me.INDColInitialExpenseDirect.OptionsColumn.AllowFocus = False
        Me.INDColInitialExpenseDirect.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DirectCostDistribution", "{0:C2}")})
        Me.INDColInitialExpenseDirect.Visible = True
        Me.INDColInitialExpenseDirect.VisibleIndex = 1
        Me.INDColInitialExpenseDirect.Width = 101
        '
        'INDColInitialExpenseVariable
        '
        Me.INDColInitialExpenseVariable.Caption = "Gastos Variables"
        Me.INDColInitialExpenseVariable.DisplayFormat.FormatString = "C2"
        Me.INDColInitialExpenseVariable.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInitialExpenseVariable.FieldName = "AutoCostDistribution"
        Me.INDColInitialExpenseVariable.Name = "INDColInitialExpenseVariable"
        Me.INDColInitialExpenseVariable.OptionsColumn.AllowEdit = False
        Me.INDColInitialExpenseVariable.OptionsColumn.AllowFocus = False
        Me.INDColInitialExpenseVariable.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AutoCostDistribution", "{0:C2}")})
        Me.INDColInitialExpenseVariable.Visible = True
        Me.INDColInitialExpenseVariable.VisibleIndex = 2
        Me.INDColInitialExpenseVariable.Width = 101
        '
        'INDColInitialManpower
        '
        Me.INDColInitialManpower.Caption = "Mano Obra"
        Me.INDColInitialManpower.DisplayFormat.FormatString = "C2"
        Me.INDColInitialManpower.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInitialManpower.FieldName = "ManPowerDistributionTotal"
        Me.INDColInitialManpower.Name = "INDColInitialManpower"
        Me.INDColInitialManpower.OptionsColumn.AllowEdit = False
        Me.INDColInitialManpower.OptionsColumn.AllowFocus = False
        Me.INDColInitialManpower.OptionsColumn.FixedWidth = True
        Me.INDColInitialManpower.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ManPowerDistributionTotal", "{0:C2}")})
        Me.INDColInitialManpower.Visible = True
        Me.INDColInitialManpower.VisibleIndex = 3
        Me.INDColInitialManpower.Width = 110
        '
        'INDColInitialFixedAsset
        '
        Me.INDColInitialFixedAsset.Caption = "Activos Fijos"
        Me.INDColInitialFixedAsset.DisplayFormat.FormatString = "C2"
        Me.INDColInitialFixedAsset.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInitialFixedAsset.FieldName = "FixedAssetDistribution"
        Me.INDColInitialFixedAsset.Name = "INDColInitialFixedAsset"
        Me.INDColInitialFixedAsset.OptionsColumn.AllowEdit = False
        Me.INDColInitialFixedAsset.OptionsColumn.AllowFocus = False
        Me.INDColInitialFixedAsset.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FixedAssetDistribution", "{0:C2}")})
        Me.INDColInitialFixedAsset.Visible = True
        Me.INDColInitialFixedAsset.VisibleIndex = 4
        Me.INDColInitialFixedAsset.Width = 97
        '
        'INDColInitialDispensing
        '
        Me.INDColInitialDispensing.Caption = "Dispensación"
        Me.INDColInitialDispensing.DisplayFormat.FormatString = "C2"
        Me.INDColInitialDispensing.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInitialDispensing.FieldName = "DispensingDistribution"
        Me.INDColInitialDispensing.Name = "INDColInitialDispensing"
        Me.INDColInitialDispensing.OptionsColumn.AllowEdit = False
        Me.INDColInitialDispensing.OptionsColumn.AllowFocus = False
        Me.INDColInitialDispensing.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DispensingDistribution", "{0:C2}")})
        Me.INDColInitialDispensing.Visible = True
        Me.INDColInitialDispensing.VisibleIndex = 5
        Me.INDColInitialDispensing.Width = 97
        '
        'INDColInitialTransfer
        '
        Me.INDColInitialTransfer.Caption = "Consumo"
        Me.INDColInitialTransfer.DisplayFormat.FormatString = "C2"
        Me.INDColInitialTransfer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInitialTransfer.FieldName = "TransferDistribution"
        Me.INDColInitialTransfer.Name = "INDColInitialTransfer"
        Me.INDColInitialTransfer.OptionsColumn.AllowEdit = False
        Me.INDColInitialTransfer.OptionsColumn.AllowFocus = False
        Me.INDColInitialTransfer.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TransferDistribution", "{0:C2}")})
        Me.INDColInitialTransfer.Visible = True
        Me.INDColInitialTransfer.VisibleIndex = 6
        Me.INDColInitialTransfer.Width = 83
        '
        'INDColInitialDistribution
        '
        Me.INDColInitialDistribution.Caption = "Distribución Inicial"
        Me.INDColInitialDistribution.DisplayFormat.FormatString = "C2"
        Me.INDColInitialDistribution.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColInitialDistribution.FieldName = "InitialDistribution"
        Me.INDColInitialDistribution.Name = "INDColInitialDistribution"
        Me.INDColInitialDistribution.OptionsColumn.AllowEdit = False
        Me.INDColInitialDistribution.OptionsColumn.AllowFocus = False
        Me.INDColInitialDistribution.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InitialDistribution", "{0:C2}")})
        Me.INDColInitialDistribution.Visible = True
        Me.INDColInitialDistribution.VisibleIndex = 7
        Me.INDColInitialDistribution.Width = 116
        '
        'INDColSecondaryExpenseDirect
        '
        Me.INDColSecondaryExpenseDirect.Caption = "Gastos Directos"
        Me.INDColSecondaryExpenseDirect.DisplayFormat.FormatString = "C2"
        Me.INDColSecondaryExpenseDirect.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSecondaryExpenseDirect.FieldName = "SecondaryDirectCostDistribution"
        Me.INDColSecondaryExpenseDirect.Name = "INDColSecondaryExpenseDirect"
        Me.INDColSecondaryExpenseDirect.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SecondaryDirectCostDistribution", "{0:C2}")})
        Me.INDColSecondaryExpenseDirect.Width = 101
        '
        'INDColSecondaryExpenseVariable
        '
        Me.INDColSecondaryExpenseVariable.Caption = "Gastos Variables"
        Me.INDColSecondaryExpenseVariable.DisplayFormat.FormatString = "C2"
        Me.INDColSecondaryExpenseVariable.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSecondaryExpenseVariable.FieldName = "SecondaryAutoCostDistribution"
        Me.INDColSecondaryExpenseVariable.Name = "INDColSecondaryExpenseVariable"
        Me.INDColSecondaryExpenseVariable.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SecondaryAutoCostDistribution", "{0:C2}")})
        Me.INDColSecondaryExpenseVariable.Width = 101
        '
        'INDColSecondaryManPower
        '
        Me.INDColSecondaryManPower.Caption = "Mano de Obra"
        Me.INDColSecondaryManPower.DisplayFormat.FormatString = "C2"
        Me.INDColSecondaryManPower.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSecondaryManPower.FieldName = "SecondaryManPowerDistributionDirect"
        Me.INDColSecondaryManPower.Name = "INDColSecondaryManPower"
        Me.INDColSecondaryManPower.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SecondaryManPowerDistributionDirect", "{0:C2}")})
        Me.INDColSecondaryManPower.Width = 101
        '
        'INDColSecondaryFixedAsset
        '
        Me.INDColSecondaryFixedAsset.Caption = "Activos Fijos"
        Me.INDColSecondaryFixedAsset.DisplayFormat.FormatString = "C2"
        Me.INDColSecondaryFixedAsset.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSecondaryFixedAsset.FieldName = "SecondaryFixedAssetDistribution"
        Me.INDColSecondaryFixedAsset.Name = "INDColSecondaryFixedAsset"
        Me.INDColSecondaryFixedAsset.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SecondaryFixedAssetDistribution", "{0:C2}")})
        Me.INDColSecondaryFixedAsset.Width = 101
        '
        'INDColSecondaryDispensing
        '
        Me.INDColSecondaryDispensing.Caption = "Dispensación"
        Me.INDColSecondaryDispensing.DisplayFormat.FormatString = "C2"
        Me.INDColSecondaryDispensing.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSecondaryDispensing.FieldName = "SecondaryDispensingDistribution"
        Me.INDColSecondaryDispensing.Name = "INDColSecondaryDispensing"
        Me.INDColSecondaryDispensing.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SecondaryDispensingDistribution", "{0:C2}")})
        Me.INDColSecondaryDispensing.Width = 101
        '
        'INDColSecondaryTransfer
        '
        Me.INDColSecondaryTransfer.Caption = "Consumo"
        Me.INDColSecondaryTransfer.DisplayFormat.FormatString = "C2"
        Me.INDColSecondaryTransfer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSecondaryTransfer.FieldName = "SecondaryTransferDistribution"
        Me.INDColSecondaryTransfer.Name = "INDColSecondaryTransfer"
        Me.INDColSecondaryTransfer.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SecondaryTransferDistribution", "{0:C2}")})
        Me.INDColSecondaryTransfer.Width = 101
        '
        'INDColSecondaryDistribution
        '
        Me.INDColSecondaryDistribution.Caption = "Distribución Secundaria"
        Me.INDColSecondaryDistribution.DisplayFormat.FormatString = "C2"
        Me.INDColSecondaryDistribution.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSecondaryDistribution.FieldName = "SecondaryDistribution"
        Me.INDColSecondaryDistribution.Name = "INDColSecondaryDistribution"
        Me.INDColSecondaryDistribution.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SecondaryDistribution", "{0:C2}")})
        Me.INDColSecondaryDistribution.Width = 101
        '
        'GleEstimationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.GleEstimationType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.GleEstimationType, False)
        Me.GleEstimationType.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.GleEstimationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.GleEstimationType.Name = "GleEstimationType"
        Me.GleEstimationType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.GleEstimationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.GleEstimationType.Properties.Appearance.Options.UseBackColor = True
        Me.GleEstimationType.Properties.Appearance.Options.UseFont = True
        Me.GleEstimationType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GleEstimationType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GleEstimationType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.GleEstimationType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GleEstimationType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.GleEstimationType.Properties.AppearanceFocused.Options.UseFont = True
        Me.GleEstimationType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.GleEstimationType.Properties.DisplayMember = "Item2"
        Me.GleEstimationType.Properties.NullText = ""
        Me.GleEstimationType.Properties.ValueMember = "Item1"
        Me.GleEstimationType.Properties.View = Me.GridLookUpEdit1View
        Me.GleEstimationType.Size = New System.Drawing.Size(386, 28)
        Me.GleEstimationType.StyleController = Me.INDlcRoot
        Me.GleEstimationType.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.GleEstimationType, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tipo"
        Me.GridColumn7.FieldName = "Item2"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'ChkListOptions
        '
        Me.ChkListOptions.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.ChkListOptions.Appearance.Options.UseFont = True
        Me.IndigoCheckedListBoxControl1.SetCampoObligatorio(Me.ChkListOptions, False)
        Me.ChkListOptions.Items.AddRange(New DevExpress.XtraEditors.Controls.CheckedListBoxItem() {New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(1, Byte), "Gastos Generales"), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(2, Byte), "Mano de Obra"), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(3, Byte), "Activos Fijos"), New DevExpress.XtraEditors.Controls.CheckedListBoxItem(CType(4, Byte), "Suministros y Consumos")})
        Me.ChkListOptions.Location = New System.Drawing.Point(24, 119)
        Me.ChkListOptions.Name = "ChkListOptions"
        Me.ChkListOptions.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.ChkListOptions.Size = New System.Drawing.Size(386, 156)
        Me.ChkListOptions.StyleController = Me.INDlcRoot
        Me.ChkListOptions.TabIndex = 4
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
        Me.INDlcgRoot.CustomizationFormText = "INDlcgRoot"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LcgDataEstimation})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1286, 468)
        Me.INDlcgRoot.TextVisible = False
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCheckOptions, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(414, 448)
        Me.LayoutControlGroup1.Text = "Estimar Costos"
        '
        'INDLciCheckOptions
        '
        Me.INDLciCheckOptions.Control = Me.ChkListOptions
        Me.INDLciCheckOptions.CustomizationFormText = "LayoutControlItem1"
        Me.INDLciCheckOptions.Location = New System.Drawing.Point(0, 60)
        Me.INDLciCheckOptions.MaxSize = New System.Drawing.Size(390, 160)
        Me.INDLciCheckOptions.MinSize = New System.Drawing.Size(390, 160)
        Me.INDLciCheckOptions.Name = "INDLciCheckOptions"
        Me.INDLciCheckOptions.Size = New System.Drawing.Size(390, 329)
        Me.INDLciCheckOptions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCheckOptions.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCheckOptions.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciCheckOptions.TextToControlDistance = 0
        Me.INDLciCheckOptions.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.GleEstimationType
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Tipo Estimación"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'LcgDataEstimation
        '
        Me.LcgDataEstimation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgDataEstimation.AppearanceGroup.Options.UseFont = True
        Me.LcgDataEstimation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgDataEstimation.AppearanceItemCaption.Options.UseFont = True
        Me.LcgDataEstimation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgDataEstimation.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgDataEstimation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LcgDataEstimation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LcgDataEstimation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgDataEstimation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgDataEstimation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgDataEstimation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LcgDataEstimation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgDataEstimation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgDataEstimation, False)
        Me.LcgDataEstimation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LciEstimateDataSimulate})
        Me.LcgDataEstimation.Location = New System.Drawing.Point(414, 0)
        Me.LcgDataEstimation.Name = "LcgDataEstimation"
        Me.LcgDataEstimation.Size = New System.Drawing.Size(852, 448)
        Me.LcgDataEstimation.Text = "Valores De Estimación"
        '
        'LciEstimateDataSimulate
        '
        Me.LciEstimateDataSimulate.Control = Me.INDGcEstimationData
        Me.LciEstimateDataSimulate.Location = New System.Drawing.Point(0, 0)
        Me.LciEstimateDataSimulate.MinSize = New System.Drawing.Size(828, 24)
        Me.LciEstimateDataSimulate.Name = "LciEstimateDataSimulate"
        Me.LciEstimateDataSimulate.Size = New System.Drawing.Size(828, 389)
        Me.LciEstimateDataSimulate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciEstimateDataSimulate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LciEstimateDataSimulate.TextSize = New System.Drawing.Size(0, 0)
        Me.LciEstimateDataSimulate.TextToControlDistance = 0
        Me.LciEstimateDataSimulate.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmCostEstimateCosts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1489, 616)
        Me.Name = "FrmCostEstimateCosts"
        Me.Opacity = 1.0R
        Me.Tag = "1205"
        Me.Text = "Estimar Costos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDGcEstimationData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GleEstimationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkListOptions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCheckOptions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgDataEstimation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LciEstimateDataSimulate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckedListBoxControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents ChkListOptions As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents INDLciCheckOptions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckedListBoxControl1 As Presentation.Controls.IndigoCheckedListBoxControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents GleEstimationType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcEstimationData As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewInfo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LcgDataEstimation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LciEstimateDataSimulate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColProductionCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialExpenseDirect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialExpenseVariable As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialManpower As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialFixedAsset As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialDispensing As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialTransfer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialDistribution As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSecondaryExpenseDirect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSecondaryExpenseVariable As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSecondaryManPower As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSecondaryFixedAsset As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSecondaryDispensing As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSecondaryTransfer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSecondaryDistribution As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
End Class
