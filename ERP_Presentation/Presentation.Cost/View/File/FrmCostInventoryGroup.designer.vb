Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostInventoryGroup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCostInventoryGroup))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim RepositoryItemPopupContainerEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Dim RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Dim RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Dim RepositoryItemPopupContainerEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Dim RepositoryItemPopupContainerEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccInventory = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrInventoryProduct = New Presentation.Cost.CtrInventoryProduct()
        Me.INDpceAddInventory = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDgcInventory = New DevExpress.XtraGrid.GridControl()
        Me.INDgvInventory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolInventoryInventory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolInventoryMeasurementUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolInventoryQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDteName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbeCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDsleMeasurementUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvCUPSEntity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolCUPSEntityCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCUPSEntityDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCUPSEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgInventory = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliSupply = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAddInventory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridViewProductionCenter = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridViewStep = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridViewFixedAsset = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridViewPayroll = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridViewInventory = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridViewAddictionalCost = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDpccInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccInventory.SuspendLayout()
        CType(Me.INDpceAddInventory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleMeasurementUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvCUPSEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCUPSEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliSupply, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAddInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewStep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewFixedAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewPayroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewAddictionalCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1418, 626)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1418, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1418, 130)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 617)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDpccInventory)
        Me.INDlcRoot.Controls.Add(Me.INDpceAddInventory)
        Me.INDlcRoot.Controls.Add(Me.INDgcInventory)
        Me.INDlcRoot.Controls.Add(Me.INDteName)
        Me.INDlcRoot.Controls.Add(Me.INDbeCode)
        Me.INDlcRoot.Controls.Add(Me.INDmeDescription)
        Me.INDlcRoot.Controls.Add(Me.INDsleMeasurementUnit)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1859, 244, 744, 556)
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(1214, 617)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDpccInventory
        '
        Me.INDpccInventory.Controls.Add(Me.CtrInventoryProduct)
        Me.INDpccInventory.Location = New System.Drawing.Point(644, 270)
        Me.INDpccInventory.Name = "INDpccInventory"
        Me.INDpccInventory.Size = New System.Drawing.Size(417, 247)
        Me.INDpccInventory.TabIndex = 45
        '
        'CtrInventoryProduct
        '
        Me.CtrInventoryProduct.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrInventoryProduct.CostInventoryGroupDetail = CType(resources.GetObject("CtrInventoryProduct.CostInventoryGroupDetail"), Domain.Entities.CostInventoryGroupDetail)
        Me.CtrInventoryProduct.EditMode = False
        Me.CtrInventoryProduct.ListCostInventoryGroupDetail = Nothing
        Me.CtrInventoryProduct.Location = New System.Drawing.Point(4, 4)
        Me.CtrInventoryProduct.Name = "CtrInventoryProduct"
        Me.CtrInventoryProduct.Size = New System.Drawing.Size(410, 240)
        Me.CtrInventoryProduct.TabIndex = 0
        '
        'INDpceAddInventory
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceAddInventory, True)
        Me.INDpceAddInventory.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceAddInventory, Nothing)
        Me.INDpceAddInventory.Location = New System.Drawing.Point(438, 53)
        Me.INDpceAddInventory.MinimumSize = New System.Drawing.Size(824, 32)
        Me.INDpceAddInventory.Name = "INDpceAddInventory"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceAddInventory, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceAddInventory, False)
        Me.INDpceAddInventory.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceAddInventory.Properties.Appearance.Options.UseFont = True
        Me.INDpceAddInventory.Properties.AutoHeight = False
        Me.INDpceAddInventory.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDpceAddInventory.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDpceAddInventory.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceAddInventory.Properties.PopupControl = Me.INDpccInventory
        Me.INDpceAddInventory.Properties.PopupSizeable = False
        Me.INDpceAddInventory.Properties.ShowPopupCloseButton = False
        Me.INDpceAddInventory.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceAddInventory.Size = New System.Drawing.Size(824, 32)
        Me.INDpceAddInventory.StyleController = Me.INDlcRoot
        Me.INDpceAddInventory.TabIndex = 12
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceAddInventory, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceAddInventory, Nothing)
        '
        'INDgcInventory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInventory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInventory, Nothing)
        Me.INDgcInventory.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInventory, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInventory, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInventory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInventory, False)
        Me.INDgcInventory.Location = New System.Drawing.Point(438, 89)
        Me.INDgcInventory.MainView = Me.INDgvInventory
        Me.INDgcInventory.Name = "INDgcInventory"
        Me.INDgcInventory.Size = New System.Drawing.Size(824, 487)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInventory, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcInventory.TabIndex = 32
        Me.INDgcInventory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvInventory})
        '
        'INDgvInventory
        '
        Me.INDgvInventory.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvInventory.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvInventory.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvInventory.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvInventory.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvInventory.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvInventory.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvInventory.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvInventory.Appearance.Row.Options.UseFont = True
        Me.INDgvInventory.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvInventory.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvInventory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolInventoryInventory, Me.INDcolInventoryMeasurementUnit, Me.INDcolInventoryQuantity})
        Me.INDgvInventory.GridControl = Me.INDgcInventory
        Me.INDgvInventory.Name = "INDgvInventory"
        Me.INDgvInventory.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvInventory.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvInventory.OptionsView.ShowAutoFilterRow = True
        Me.INDgvInventory.OptionsView.ShowDetailButtons = False
        Me.INDgvInventory.OptionsView.ShowFooter = True
        Me.INDgvInventory.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewPayroll.SetTemaIndigoMetro(Me.INDgvInventory, False)
        Me.IndigoGridViewFixedAsset.SetTemaIndigoMetro(Me.INDgvInventory, False)
        Me.IndigoGridViewStep.SetTemaIndigoMetro(Me.INDgvInventory, False)
        Me.IndigoGridViewProductionCenter.SetTemaIndigoMetro(Me.INDgvInventory, False)
        Me.IndigoGridViewInventory.SetTemaIndigoMetro(Me.INDgvInventory, False)
        Me.IndigoGridViewAddictionalCost.SetTemaIndigoMetro(Me.INDgvInventory, False)
        '
        'INDcolInventoryInventory
        '
        Me.INDcolInventoryInventory.Caption = "Producto"
        Me.INDcolInventoryInventory.FieldName = "InventoryProductCodeName"
        Me.INDcolInventoryInventory.Name = "INDcolInventoryInventory"
        Me.INDcolInventoryInventory.OptionsColumn.AllowEdit = False
        Me.INDcolInventoryInventory.OptionsColumn.AllowFocus = False
        Me.INDcolInventoryInventory.Visible = True
        Me.INDcolInventoryInventory.VisibleIndex = 0
        Me.INDcolInventoryInventory.Width = 269
        '
        'INDcolInventoryMeasurementUnit
        '
        Me.INDcolInventoryMeasurementUnit.Caption = "Unidad de Medida"
        Me.INDcolInventoryMeasurementUnit.FieldName = "MeasurementUnitCodeName"
        Me.INDcolInventoryMeasurementUnit.Name = "INDcolInventoryMeasurementUnit"
        Me.INDcolInventoryMeasurementUnit.OptionsColumn.AllowEdit = False
        Me.INDcolInventoryMeasurementUnit.OptionsColumn.AllowFocus = False
        Me.INDcolInventoryMeasurementUnit.Visible = True
        Me.INDcolInventoryMeasurementUnit.VisibleIndex = 1
        Me.INDcolInventoryMeasurementUnit.Width = 216
        '
        'INDcolInventoryQuantity
        '
        Me.INDcolInventoryQuantity.Caption = "Cantidad Equivalente"
        Me.INDcolInventoryQuantity.DisplayFormat.FormatString = "n6"
        Me.INDcolInventoryQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolInventoryQuantity.FieldName = "Quantity"
        Me.INDcolInventoryQuantity.Name = "INDcolInventoryQuantity"
        Me.INDcolInventoryQuantity.OptionsColumn.AllowEdit = False
        Me.INDcolInventoryQuantity.OptionsColumn.AllowFocus = False
        Me.INDcolInventoryQuantity.Visible = True
        Me.INDcolInventoryQuantity.VisibleIndex = 2
        '
        'INDteName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteName, True)
        Me.INDteName.EnterMoveNextControl = True
        Me.INDteName.Location = New System.Drawing.Point(24, 141)
        Me.IndigoTextEdit1.SetMascara(Me.INDteName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteName.Name = "INDteName"
        Me.INDteName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDteName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDteName.Properties.Appearance.Options.UseBackColor = True
        Me.INDteName.Properties.Appearance.Options.UseFont = True
        Me.INDteName.Properties.Appearance.Options.UseForeColor = True
        Me.INDteName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDteName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteName.Properties.MaxLength = 100
        Me.INDteName.Size = New System.Drawing.Size(386, 28)
        Me.INDteName.StyleController = Me.INDlcRoot
        Me.INDteName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteName, 0)
        Me.INDteName.ToolTip = "Este Campo es Necesario"
        '
        'INDbeCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbeCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbeCode, True)
        Me.INDbeCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbeCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbeCode.Name = "INDbeCode"
        Me.INDbeCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbeCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbeCode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.INDbeCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbeCode.Properties.Appearance.Options.UseFont = True
        Me.INDbeCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDbeCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbeCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbeCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbeCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbeCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbeCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions2.Image = Global.Presentation.Cost.My.Resources.Resources.BuscarMetro
        Me.INDbeCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbeCode.Properties.MaxLength = 20
        Me.INDbeCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbeCode.StyleController = Me.INDlcRoot
        Me.INDbeCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbeCode, 0)
        Me.INDbeCode.ToolTip = "Este Campo es Necesario"
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, True)
        Me.INDmeDescription.EnterMoveNextControl = True
        Me.INDmeDescription.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmeDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDescription.Properties.MaxLength = 500
        Me.INDmeDescription.Size = New System.Drawing.Size(386, 70)
        Me.INDmeDescription.StyleController = Me.INDlcRoot
        Me.INDmeDescription.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        Me.INDmeDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDsleMeasurementUnit
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleMeasurementUnit, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleMeasurementUnit, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleMeasurementUnit, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleMeasurementUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleMeasurementUnit, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.INDsleMeasurementUnit.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.INDsleMeasurementUnit.Location = New System.Drawing.Point(24, 203)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleMeasurementUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleMeasurementUnit.Name = "INDsleMeasurementUnit"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleMeasurementUnit, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.INDsleMeasurementUnit.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleMeasurementUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleMeasurementUnit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleMeasurementUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleMeasurementUnit.Properties.Appearance.Options.UseFont = True
        Me.INDsleMeasurementUnit.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleMeasurementUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleMeasurementUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleMeasurementUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleMeasurementUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleMeasurementUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleMeasurementUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleMeasurementUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleMeasurementUnit.Properties.DisplayMember = "CodeName"
        Me.INDsleMeasurementUnit.Properties.NullText = ""
        Me.INDsleMeasurementUnit.Properties.PopupSizeable = False
        Me.INDsleMeasurementUnit.Properties.PopupView = Me.INDgvCUPSEntity
        Me.INDsleMeasurementUnit.Properties.ShowFooter = False
        Me.INDsleMeasurementUnit.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleMeasurementUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleMeasurementUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleMeasurementUnit, True)
        Me.INDsleMeasurementUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDsleMeasurementUnit.StyleController = Me.INDlcRoot
        Me.INDsleMeasurementUnit.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleMeasurementUnit, "970")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleMeasurementUnit, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleMeasurementUnit, "{0} - {1}")
        Me.INDsleMeasurementUnit.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleMeasurementUnit, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleMeasurementUnit, False)
        '
        'INDgvCUPSEntity
        '
        Me.INDgvCUPSEntity.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvCUPSEntity.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvCUPSEntity.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvCUPSEntity.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvCUPSEntity.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvCUPSEntity.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvCUPSEntity.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvCUPSEntity.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvCUPSEntity.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvCUPSEntity.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvCUPSEntity.Appearance.Row.Options.UseFont = True
        Me.INDgvCUPSEntity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolCUPSEntityCode, Me.INDcolCUPSEntityDescription})
        Me.INDgvCUPSEntity.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvCUPSEntity.Name = "INDgvCUPSEntity"
        Me.INDgvCUPSEntity.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvCUPSEntity.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvCUPSEntity.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvCUPSEntity.OptionsView.ShowAutoFilterRow = True
        Me.INDgvCUPSEntity.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewPayroll.SetTemaIndigoMetro(Me.INDgvCUPSEntity, False)
        Me.IndigoGridViewFixedAsset.SetTemaIndigoMetro(Me.INDgvCUPSEntity, False)
        Me.IndigoGridViewStep.SetTemaIndigoMetro(Me.INDgvCUPSEntity, False)
        Me.IndigoGridViewProductionCenter.SetTemaIndigoMetro(Me.INDgvCUPSEntity, False)
        Me.IndigoGridViewInventory.SetTemaIndigoMetro(Me.INDgvCUPSEntity, False)
        Me.IndigoGridViewAddictionalCost.SetTemaIndigoMetro(Me.INDgvCUPSEntity, False)
        '
        'INDcolCUPSEntityCode
        '
        Me.INDcolCUPSEntityCode.Caption = "Código"
        Me.INDcolCUPSEntityCode.FieldName = "Code"
        Me.INDcolCUPSEntityCode.Name = "INDcolCUPSEntityCode"
        Me.INDcolCUPSEntityCode.OptionsColumn.AllowEdit = False
        Me.INDcolCUPSEntityCode.OptionsColumn.AllowFocus = False
        Me.INDcolCUPSEntityCode.Visible = True
        Me.INDcolCUPSEntityCode.VisibleIndex = 0
        Me.INDcolCUPSEntityCode.Width = 262
        '
        'INDcolCUPSEntityDescription
        '
        Me.INDcolCUPSEntityDescription.Caption = "Nombre"
        Me.INDcolCUPSEntityDescription.FieldName = "Name"
        Me.INDcolCUPSEntityDescription.Name = "INDcolCUPSEntityDescription"
        Me.INDcolCUPSEntityDescription.OptionsColumn.AllowEdit = False
        Me.INDcolCUPSEntityDescription.OptionsColumn.AllowFocus = False
        Me.INDcolCUPSEntityDescription.Visible = True
        Me.INDcolCUPSEntityDescription.VisibleIndex = 1
        Me.INDcolCUPSEntityDescription.Width = 947
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
        Me.INDlcgRoot.CustomizationFormText = "Root"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData, Me.INDlcgInventory})
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1286, 600)
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
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCUPSEntity, Me.INDlciCode, Me.INDlciName, Me.INDlciDescription})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 580)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDlciCUPSEntity
        '
        Me.INDlciCUPSEntity.AllowHide = False
        Me.INDlciCUPSEntity.Control = Me.INDsleMeasurementUnit
        Me.INDlciCUPSEntity.CustomizationFormText = "Unidad de Medida"
        Me.INDlciCUPSEntity.Location = New System.Drawing.Point(0, 124)
        Me.INDlciCUPSEntity.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciCUPSEntity.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciCUPSEntity.Name = "INDlciCUPSEntity"
        Me.INDlciCUPSEntity.ShowInCustomizationForm = False
        Me.INDlciCUPSEntity.Size = New System.Drawing.Size(390, 62)
        Me.INDlciCUPSEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCUPSEntity.Text = "Unidad de Medida"
        Me.INDlciCUPSEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCUPSEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCUPSEntity.TextSize = New System.Drawing.Size(191, 21)
        Me.INDlciCUPSEntity.TextToControlDistance = 5
        '
        'INDlciCode
        '
        Me.INDlciCode.AllowHide = False
        Me.INDlciCode.Control = Me.INDbeCode
        Me.INDlciCode.CustomizationFormText = "Código"
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciCode.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.ShowInCustomizationForm = False
        Me.INDlciCode.Size = New System.Drawing.Size(390, 62)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCode.TextSize = New System.Drawing.Size(191, 21)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'INDlciName
        '
        Me.INDlciName.AllowHide = False
        Me.INDlciName.Control = Me.INDteName
        Me.INDlciName.CustomizationFormText = "Nombre"
        Me.INDlciName.Location = New System.Drawing.Point(0, 62)
        Me.INDlciName.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDlciName.MinSize = New System.Drawing.Size(390, 62)
        Me.INDlciName.Name = "INDlciName"
        Me.INDlciName.ShowInCustomizationForm = False
        Me.INDlciName.Size = New System.Drawing.Size(390, 62)
        Me.INDlciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciName.Text = "Nombre"
        Me.INDlciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciName.TextSize = New System.Drawing.Size(191, 21)
        Me.INDlciName.TextToControlDistance = 5
        '
        'INDlciDescription
        '
        Me.INDlciDescription.AllowHide = False
        Me.INDlciDescription.Control = Me.INDmeDescription
        Me.INDlciDescription.CustomizationFormText = "Descripción"
        Me.INDlciDescription.Location = New System.Drawing.Point(0, 186)
        Me.INDlciDescription.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlciDescription.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlciDescription.Name = "INDlciDescription"
        Me.INDlciDescription.ShowInCustomizationForm = False
        Me.INDlciDescription.Size = New System.Drawing.Size(390, 341)
        Me.INDlciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDescription.Text = "Descripción"
        Me.INDlciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDescription.TextSize = New System.Drawing.Size(191, 21)
        Me.INDlciDescription.TextToControlDistance = 5
        '
        'INDlcgInventory
        '
        Me.INDlcgInventory.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgInventory.AppearanceGroup.Options.UseFont = True
        Me.INDlcgInventory.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgInventory.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgInventory.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInventory.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgInventory.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgInventory.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgInventory.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInventory.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgInventory.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInventory.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgInventory.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgInventory.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgInventory, False)
        Me.INDlcgInventory.CustomizationFormText = "Inventario"
        Me.INDlcgInventory.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliSupply, Me.INDlciAddInventory})
        Me.INDlcgInventory.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgInventory.Name = "INDlcgInventory"
        Me.INDlcgInventory.ShowInCustomizationForm = False
        Me.INDlcgInventory.Size = New System.Drawing.Size(852, 580)
        Me.INDlcgInventory.Text = "Inventario"
        '
        'INDliSupply
        '
        Me.INDliSupply.Control = Me.INDgcInventory
        Me.INDliSupply.CustomizationFormText = "Listado Homologación Suministros"
        Me.INDliSupply.Location = New System.Drawing.Point(0, 36)
        Me.INDliSupply.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDliSupply.MinSize = New System.Drawing.Size(828, 24)
        Me.INDliSupply.Name = "INDliSupply"
        Me.INDliSupply.Size = New System.Drawing.Size(828, 491)
        Me.INDliSupply.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliSupply.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliSupply.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliSupply.TextToControlDistance = 0
        Me.INDliSupply.TextVisible = False
        '
        'INDlciAddInventory
        '
        Me.INDlciAddInventory.Control = Me.INDpceAddInventory
        Me.INDlciAddInventory.CustomizationFormText = "Agregar Mano Homologación Suministros"
        Me.INDlciAddInventory.Location = New System.Drawing.Point(0, 0)
        Me.INDlciAddInventory.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDlciAddInventory.MinSize = New System.Drawing.Size(828, 36)
        Me.INDlciAddInventory.Name = "INDlciAddInventory"
        Me.INDlciAddInventory.Size = New System.Drawing.Size(828, 36)
        Me.INDlciAddInventory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAddInventory.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAddInventory.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciAddInventory.TextToControlDistance = 0
        Me.INDlciAddInventory.TextVisible = False
        '
        'IndigoGridControl1
        '
        '
        'IndigoGridViewProductionCenter
        '
        Me.IndigoGridViewProductionCenter.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridViewProductionCenter.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit4
        '
        'IndigoGridViewStep
        '
        Me.IndigoGridViewStep.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridViewStep.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit3
        '
        'IndigoGridViewFixedAsset
        '
        Me.IndigoGridViewFixedAsset.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridViewFixedAsset.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit2
        '
        'IndigoGridViewPayroll
        '
        Me.IndigoGridViewPayroll.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridViewPayroll.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'IndigoGridViewInventory
        '
        Me.IndigoGridViewInventory.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridViewInventory.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit5
        '
        'IndigoGridViewAddictionalCost
        '
        Me.IndigoGridViewAddictionalCost.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit6.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridViewAddictionalCost.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit6
        '
        'FrmCostInventoryGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1418, 761)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmCostInventoryGroup"
        Me.Opacity = 1.0R
        Me.Tag = "2062"
        Me.Text = "Grupo de Productos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDpccInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccInventory.ResumeLayout(False)
        CType(Me.INDpceAddInventory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleMeasurementUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvCUPSEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCUPSEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliSupply, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAddInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewStep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewFixedAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewPayroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewInventory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewAddictionalCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDsleMeasurementUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvCUPSEntity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCUPSEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDteName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbeCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgInventory As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcInventory As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvInventory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliSupply As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolCUPSEntityCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCUPSEntityDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceAddInventory As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlciAddInventory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridViewStep As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridViewFixedAsset As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridViewProductionCenter As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridViewPayroll As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridViewInventory As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridViewAddictionalCost As Presentation.Controls.IndigoGridView
    Friend WithEvents INDcolInventoryInventory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolInventoryMeasurementUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolInventoryQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpccInventory As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrInventoryProduct As CtrInventoryProduct
End Class
