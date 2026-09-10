<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmKardexCampaign
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
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcDetails1 = New DevExpress.XtraGrid.GridControl()
        Me.INDviewMaster = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProductCodeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnitMeasurement = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityUnitMeasurement = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityHarnessed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityUsed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCurrentQuantity1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDeliveredQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityUsedCampaign1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantityDevolution1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCampaignBalance = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDpanelProgressbar = New DevExpress.XtraEditors.PanelControl()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDColUnitMeasurementd = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDGvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetails1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewMaster, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpanelProgressbar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpanelProgressbar.SuspendLayout()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
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
        Me.INDGvDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColMovementDate, Me.INDColUserMovement, Me.INDColQuantity, Me.INDColUnitMeasurementd, Me.GridColumn1})
        Me.INDGvDetails.GridControl = Me.INDgcDetails1
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
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo de Movimiento"
        Me.GridColumn1.FieldName = "MovementTypeName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 3
        '
        'INDgcDetails1
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetails1, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetails1, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetails1, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetails1, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetails1, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetails1, False)
        GridLevelNode1.LevelTemplate = Me.INDGvDetails
        GridLevelNode1.RelationName = "Movimientos"
        Me.INDgcDetails1.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.INDgcDetails1.Location = New System.Drawing.Point(24, 24)
        Me.INDgcDetails1.MainView = Me.INDviewMaster
        Me.INDgcDetails1.Name = "INDgcDetails1"
        Me.INDgcDetails1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDPpceInfo, Me.RepositoryItemMemoExEdit1})
        Me.IndigoGridControl1.SetShowNewRecordButton(Me.INDgcDetails1, False)
        Me.INDgcDetails1.Size = New System.Drawing.Size(946, 605)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetails1, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDetails1.TabIndex = 17
        Me.INDgcDetails1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewMaster, Me.INDGvDetails})
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
        Me.INDviewMaster.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProductCodeName, Me.INDColUnitMeasurement, Me.INDColQuantityUnitMeasurement, Me.INDColQuantityHarnessed, Me.INDColQuantityUsed, Me.INDColCurrentQuantity1, Me.INDColDeliveredQuantity, Me.INDColQuantityUsedCampaign1, Me.INDColQuantityDevolution1, Me.INDColCampaignBalance})
        Me.INDviewMaster.GridControl = Me.INDgcDetails1
        Me.INDviewMaster.Name = "INDviewMaster"
        Me.INDviewMaster.OptionsDetail.AllowExpandEmptyDetails = True
        Me.INDviewMaster.OptionsSelection.MultiSelect = True
        Me.INDviewMaster.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewMaster.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewMaster.OptionsView.ShowAutoFilterRow = True
        Me.INDviewMaster.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewMaster, False)
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
        Me.INDColProductCodeName.VisibleIndex = 0
        Me.INDColProductCodeName.Width = 398
        '
        'INDColUnitMeasurement
        '
        Me.INDColUnitMeasurement.Caption = "U. Medida"
        Me.INDColUnitMeasurement.FieldName = "UnitMeasurement"
        Me.INDColUnitMeasurement.Name = "INDColUnitMeasurement"
        Me.INDColUnitMeasurement.OptionsColumn.AllowEdit = False
        Me.INDColUnitMeasurement.OptionsColumn.AllowFocus = False
        Me.INDColUnitMeasurement.ToolTip = "Unidad de Medida"
        Me.INDColUnitMeasurement.Visible = True
        Me.INDColUnitMeasurement.VisibleIndex = 1
        Me.INDColUnitMeasurement.Width = 106
        '
        'INDColQuantityUnitMeasurement
        '
        Me.INDColQuantityUnitMeasurement.Caption = "Cant. U. Medida"
        Me.INDColQuantityUnitMeasurement.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityUnitMeasurement.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityUnitMeasurement.FieldName = "QuantityUnitMeasurement"
        Me.INDColQuantityUnitMeasurement.Name = "INDColQuantityUnitMeasurement"
        Me.INDColQuantityUnitMeasurement.OptionsColumn.AllowEdit = False
        Me.INDColQuantityUnitMeasurement.OptionsColumn.AllowFocus = False
        Me.INDColQuantityUnitMeasurement.ToolTip = "Cantidad Unidad de Medida"
        Me.INDColQuantityUnitMeasurement.Visible = True
        Me.INDColQuantityUnitMeasurement.VisibleIndex = 2
        Me.INDColQuantityUnitMeasurement.Width = 118
        '
        'INDColQuantityHarnessed
        '
        Me.INDColQuantityHarnessed.Caption = "Aprovechada"
        Me.INDColQuantityHarnessed.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityHarnessed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityHarnessed.FieldName = "QuantityHarnessed"
        Me.INDColQuantityHarnessed.Name = "INDColQuantityHarnessed"
        Me.INDColQuantityHarnessed.ToolTip = "Cantidad Aprovechada"
        Me.INDColQuantityHarnessed.Visible = True
        Me.INDColQuantityHarnessed.VisibleIndex = 3
        Me.INDColQuantityHarnessed.Width = 97
        '
        'INDColQuantityUsed
        '
        Me.INDColQuantityUsed.Caption = "Utilizada"
        Me.INDColQuantityUsed.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityUsed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityUsed.FieldName = "QuantityUsed"
        Me.INDColQuantityUsed.Name = "INDColQuantityUsed"
        Me.INDColQuantityUsed.OptionsColumn.AllowEdit = False
        Me.INDColQuantityUsed.OptionsColumn.AllowFocus = False
        Me.INDColQuantityUsed.ToolTip = "Cantidad Utilizada"
        Me.INDColQuantityUsed.Visible = True
        Me.INDColQuantityUsed.VisibleIndex = 4
        Me.INDColQuantityUsed.Width = 98
        '
        'INDColCurrentQuantity1
        '
        Me.INDColCurrentQuantity1.Caption = "Actual"
        Me.INDColCurrentQuantity1.DisplayFormat.FormatString = "n2"
        Me.INDColCurrentQuantity1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColCurrentQuantity1.FieldName = "CurrentQuantity"
        Me.INDColCurrentQuantity1.Name = "INDColCurrentQuantity1"
        Me.INDColCurrentQuantity1.OptionsColumn.AllowEdit = False
        Me.INDColCurrentQuantity1.OptionsColumn.AllowFocus = False
        Me.INDColCurrentQuantity1.ToolTip = "Cantidad Actual"
        Me.INDColCurrentQuantity1.Visible = True
        Me.INDColCurrentQuantity1.VisibleIndex = 5
        Me.INDColCurrentQuantity1.Width = 99
        '
        'INDColDeliveredQuantity
        '
        Me.INDColDeliveredQuantity.Caption = "Entregada"
        Me.INDColDeliveredQuantity.DisplayFormat.FormatString = "n2"
        Me.INDColDeliveredQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColDeliveredQuantity.FieldName = "DeliveredQuantity"
        Me.INDColDeliveredQuantity.Name = "INDColDeliveredQuantity"
        Me.INDColDeliveredQuantity.OptionsColumn.AllowEdit = False
        Me.INDColDeliveredQuantity.OptionsColumn.AllowFocus = False
        Me.INDColDeliveredQuantity.ToolTip = "Cantidad Entregada"
        Me.INDColDeliveredQuantity.Visible = True
        Me.INDColDeliveredQuantity.VisibleIndex = 6
        Me.INDColDeliveredQuantity.Width = 112
        '
        'INDColQuantityUsedCampaign1
        '
        Me.INDColQuantityUsedCampaign1.Caption = "Utilizada"
        Me.INDColQuantityUsedCampaign1.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityUsedCampaign1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityUsedCampaign1.FieldName = "QuantityUsedCampaign"
        Me.INDColQuantityUsedCampaign1.Name = "INDColQuantityUsedCampaign1"
        Me.INDColQuantityUsedCampaign1.OptionsColumn.AllowEdit = False
        Me.INDColQuantityUsedCampaign1.OptionsColumn.AllowFocus = False
        Me.INDColQuantityUsedCampaign1.ToolTip = "Cantidad Utilizada"
        Me.INDColQuantityUsedCampaign1.Visible = True
        Me.INDColQuantityUsedCampaign1.VisibleIndex = 7
        Me.INDColQuantityUsedCampaign1.Width = 103
        '
        'INDColQuantityDevolution1
        '
        Me.INDColQuantityDevolution1.Caption = "Devolutivos"
        Me.INDColQuantityDevolution1.DisplayFormat.FormatString = "n2"
        Me.INDColQuantityDevolution1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantityDevolution1.FieldName = "QuantityDevolution"
        Me.INDColQuantityDevolution1.Name = "INDColQuantityDevolution1"
        Me.INDColQuantityDevolution1.OptionsColumn.AllowEdit = False
        Me.INDColQuantityDevolution1.OptionsColumn.AllowFocus = False
        Me.INDColQuantityDevolution1.ToolTip = "Cantidad Devolutivos"
        Me.INDColQuantityDevolution1.Visible = True
        Me.INDColQuantityDevolution1.VisibleIndex = 8
        Me.INDColQuantityDevolution1.Width = 86
        '
        'INDColCampaignBalance
        '
        Me.INDColCampaignBalance.Caption = "Saldo Actual Campaña"
        Me.INDColCampaignBalance.DisplayFormat.FormatString = "n2"
        Me.INDColCampaignBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColCampaignBalance.FieldName = "CampaignBalance"
        Me.INDColCampaignBalance.Name = "INDColCampaignBalance"
        Me.INDColCampaignBalance.OptionsColumn.AllowEdit = False
        Me.INDColCampaignBalance.OptionsColumn.AllowFocus = False
        Me.INDColCampaignBalance.ToolTip = "Saldo Actual Campaña"
        Me.INDColCampaignBalance.Visible = True
        Me.INDColCampaignBalance.VisibleIndex = 9
        Me.INDColCampaignBalance.Width = 158
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
        Me.INDlygDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDlygDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDlygDetails.Name = "INDlygDetails"
        Me.INDlygDetails.Size = New System.Drawing.Size(974, 633)
        Me.INDlygDetails.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcDetails1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(950, 609)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
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
        Me.INDlyRoot.Controls.Add(Me.INDgcDetails1)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(0, 22)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(994, 653)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
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
        'FrmKardexCampaign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 675)
        Me.Controls.Add(Me.INDlyRoot)
        Me.Controls.Add(Me.INDpanelProgressbar)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmKardexCampaign"
        Me.Text = "Campaña # 0"
        CType(Me.INDGvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetails1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewMaster, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpanelProgressbar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpanelProgressbar.ResumeLayout(False)
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents INDpanelProgressbar As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyProgress As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemProgress As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlygDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcDetails1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColMovementDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUserMovement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewMaster As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProductCodeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUnitMeasurement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantityUnitMeasurement As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantityUsed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCurrentQuantity1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDeliveredQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantityUsedCampaign1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantityDevolution1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCampaignBalance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPpceInfo As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemMemoExEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents INDColQuantityHarnessed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColUnitMeasurementd As DevExpress.XtraGrid.Columns.GridColumn
End Class
