Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReferralEntry
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcReferralEntry = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccBatchSerial = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcBatchSerial = New DevExpress.XtraGrid.GridControl()
        Me.INDGvBatchSerial = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSleSupplierDistributionLine = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvSleSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMeDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDGcReferralEntry = New DevExpress.XtraGrid.GridControl()
        Me.INDGvReferralEntry = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolProduct = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptIcbRemissionSource = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDcolAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolUnitValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDecountPercentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGrossUnitValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSubtotalNet = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolIVA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolLot = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptPceBatchSerial = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleWareHouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvWareHouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtReferalNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDDteDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleCurrency = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCurrencyAdvance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1179 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1180 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1181 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlygReferralEntry = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSupplierDistributionLine = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReferal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciWareHouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCurrency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgProducts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemReferralEntry = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcReferralEntry.SuspendLayout()
        CType(Me.INDPccBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccBatchSerial.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSupplierDistributionLine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSleSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIcbRemissionSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptPceBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWareHouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtReferalNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCurrencyAdvance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSupplierDistributionLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReferal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProducts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcReferralEntry)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1737, 811)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.ToolBars.Size = New System.Drawing.Size(1737, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1737, 130)
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
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcReferralEntry
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 9)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 800)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcReferralEntry
        '
        Me.INDlcReferralEntry.AllowCustomization = False
        Me.INDlcReferralEntry.Controls.Add(Me.INDPccBatchSerial)
        Me.INDlcReferralEntry.Controls.Add(Me.INDSleSupplierDistributionLine)
        Me.INDlcReferralEntry.Controls.Add(Me.INDBtnAdd)
        Me.INDlcReferralEntry.Controls.Add(Me.INDMeDetail)
        Me.INDlcReferralEntry.Controls.Add(Me.INDGcReferralEntry)
        Me.INDlcReferralEntry.Controls.Add(Me.INDSleWareHouse)
        Me.INDlcReferralEntry.Controls.Add(Me.INDTxtReferalNumber)
        Me.INDlcReferralEntry.Controls.Add(Me.INDDteDate)
        Me.INDlcReferralEntry.Controls.Add(Me.INDBteCode)
        Me.INDlcReferralEntry.Controls.Add(Me.INDsleCurrency)
        Me.INDlcReferralEntry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlcReferralEntry, False)
        Me.INDlcReferralEntry.Location = New System.Drawing.Point(202, 9)
        Me.INDlcReferralEntry.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlcReferralEntry.Name = "INDlcReferralEntry"
        Me.INDlcReferralEntry.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(333, 284, 670, 521)
        Me.INDlcReferralEntry.Root = Me.INDlygReferralEntry
        Me.INDlcReferralEntry.Size = New System.Drawing.Size(1533, 800)
        Me.INDlcReferralEntry.TabIndex = 1
        Me.INDlcReferralEntry.Text = "LayoutControl1"
        '
        'INDPccBatchSerial
        '
        Me.INDPccBatchSerial.Controls.Add(Me.LayoutControl1)
        Me.INDPccBatchSerial.Location = New System.Drawing.Point(653, 417)
        Me.INDPccBatchSerial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPccBatchSerial.Name = "INDPccBatchSerial"
        Me.INDPccBatchSerial.Size = New System.Drawing.Size(418, 235)
        Me.INDPccBatchSerial.TabIndex = 17
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcBatchSerial)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(418, 235)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcBatchSerial
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcBatchSerial, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcBatchSerial, Nothing)
        Me.INDGcBatchSerial.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDGcBatchSerial.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcBatchSerial, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcBatchSerial, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcBatchSerial, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcBatchSerial, False)
        Me.INDGcBatchSerial.Location = New System.Drawing.Point(14, 14)
        Me.INDGcBatchSerial.MainView = Me.INDGvBatchSerial
        Me.INDGcBatchSerial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGcBatchSerial.Name = "INDGcBatchSerial"
        Me.INDGcBatchSerial.Size = New System.Drawing.Size(390, 207)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcBatchSerial, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcBatchSerial.TabIndex = 0
        Me.INDGcBatchSerial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvBatchSerial})
        '
        'INDGvBatchSerial
        '
        Me.INDGvBatchSerial.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvBatchSerial.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvBatchSerial.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvBatchSerial.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvBatchSerial.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvBatchSerial.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvBatchSerial.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBatchSerial.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvBatchSerial.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBatchSerial.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvBatchSerial.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvBatchSerial.Appearance.Row.Options.UseFont = True
        Me.INDGvBatchSerial.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvBatchSerial.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvBatchSerial.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn7})
        Me.INDGvBatchSerial.DetailHeight = 431
        Me.INDGvBatchSerial.GridControl = Me.INDGcBatchSerial
        Me.INDGvBatchSerial.Name = "INDGvBatchSerial"
        Me.INDGvBatchSerial.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvBatchSerial.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvBatchSerial.OptionsView.ShowAutoFilterRow = True
        Me.INDGvBatchSerial.OptionsView.ShowDetailButtons = False
        Me.INDGvBatchSerial.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvBatchSerial, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Lote/Serial"
        Me.GridColumn6.FieldName = "CodeBatchSerial"
        Me.GridColumn6.MinWidth = 23
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        Me.GridColumn6.Width = 1100
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Cantidad"
        Me.GridColumn7.FieldName = "Quantity"
        Me.GridColumn7.MinWidth = 23
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 524
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(418, 235)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcBatchSerial
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(394, 211)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDSleSupplierDistributionLine
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleSupplierDistributionLine, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleSupplierDistributionLine, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleSupplierDistributionLine, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleSupplierDistributionLine, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.INDSleSupplierDistributionLine.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.INDSleSupplierDistributionLine.Location = New System.Drawing.Point(27, 237)
        Me.INDSleSupplierDistributionLine.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleSupplierDistributionLine, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleSupplierDistributionLine.Name = "INDSleSupplierDistributionLine"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleSupplierDistributionLine, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.INDSleSupplierDistributionLine.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleSupplierDistributionLine.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleSupplierDistributionLine.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleSupplierDistributionLine.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleSupplierDistributionLine.Properties.Appearance.Options.UseFont = True
        Me.INDSleSupplierDistributionLine.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleSupplierDistributionLine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSupplierDistributionLine.Properties.DisplayMember = "DisplaySupplier"
        Me.INDSleSupplierDistributionLine.Properties.NullText = ""
        Me.INDSleSupplierDistributionLine.Properties.PopupFormMinSize = New System.Drawing.Size(700, 0)
        Me.INDSleSupplierDistributionLine.Properties.PopupSizeable = False
        Me.INDSleSupplierDistributionLine.Properties.PopupView = Me.INDGvSleSupplier
        Me.INDSleSupplierDistributionLine.Properties.ShowClearButton = False
        Me.INDSleSupplierDistributionLine.Properties.ShowFooter = False
        Me.INDSleSupplierDistributionLine.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleSupplierDistributionLine, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleSupplierDistributionLine, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleSupplierDistributionLine, True)
        Me.INDSleSupplierDistributionLine.Size = New System.Drawing.Size(451, 34)
        Me.INDSleSupplierDistributionLine.StyleController = Me.INDlcReferralEntry
        Me.INDSleSupplierDistributionLine.TabIndex = 16
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleSupplierDistributionLine, "558")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleSupplierDistributionLine, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleSupplierDistributionLine, "{0} - {1}")
        Me.INDSleSupplierDistributionLine.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleSupplierDistributionLine, False)
        '
        'INDGvSleSupplier
        '
        Me.INDGvSleSupplier.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSleSupplier.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSleSupplier.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSleSupplier.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSleSupplier.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvSleSupplier.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSleSupplier.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSleSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSleSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSleSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSleSupplier.Appearance.Row.Options.UseFont = True
        Me.INDGvSleSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn2, Me.GridColumn3})
        Me.INDGvSleSupplier.DetailHeight = 431
        Me.INDGvSleSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvSleSupplier.GroupCount = 1
        Me.INDGvSleSupplier.Name = "INDGvSleSupplier"
        Me.INDGvSleSupplier.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvSleSupplier.OptionsFind.FindFilterColumns = "IdSupplier.IdThirdParty.Nit"
        Me.INDGvSleSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvSleSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSleSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSleSupplier.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSleSupplier.OptionsView.ShowGroupPanel = False
        Me.INDGvSleSupplier.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSleSupplier, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nit"
        Me.GridColumn4.FieldName = "IdSupplier.IdThirdParty.Nit"
        Me.GridColumn4.MinWidth = 23
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 411
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Proveedor"
        Me.GridColumn2.FieldName = "IdSupplier.CodeName"
        Me.GridColumn2.MinWidth = 23
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 766
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Línea Distribución"
        Me.GridColumn3.FieldName = "IdDistributionLine.CodeName"
        Me.GridColumn3.MinWidth = 23
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 727
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Location = New System.Drawing.Point(508, 63)
        Me.INDBtnAdd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(962, 40)
        Me.INDBtnAdd.StyleController = Me.INDlcReferralEntry
        Me.INDBtnAdd.TabIndex = 15
        Me.INDBtnAdd.Text = "Agregar Producto"
        '
        'INDMeDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDetail, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDetail, False)
        Me.INDMeDetail.Location = New System.Drawing.Point(27, 538)
        Me.INDMeDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDetail.Name = "INDMeDetail"
        Me.INDMeDetail.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMeDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeDetail.Properties.Appearance.Options.UseFont = True
        Me.INDMeDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMeDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeDetail.Properties.MaxLength = 200
        Me.INDMeDetail.Size = New System.Drawing.Size(451, 113)
        Me.INDMeDetail.StyleController = Me.INDlcReferralEntry
        Me.INDMeDetail.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDetail, 0)
        '
        'INDGcReferralEntry
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcReferralEntry, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcReferralEntry, Nothing)
        Me.INDGcReferralEntry.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDGcReferralEntry.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcReferralEntry, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcReferralEntry, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcReferralEntry, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcReferralEntry, False)
        Me.INDGcReferralEntry.Location = New System.Drawing.Point(508, 107)
        Me.INDGcReferralEntry.MainView = Me.INDGvReferralEntry
        Me.INDGcReferralEntry.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGcReferralEntry.Name = "INDGcReferralEntry"
        Me.INDGcReferralEntry.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptPceBatchSerial, Me.INDRptIcbRemissionSource})
        Me.INDGcReferralEntry.Size = New System.Drawing.Size(962, 665)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcReferralEntry, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcReferralEntry.TabIndex = 13
        Me.INDGcReferralEntry.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvReferralEntry})
        '
        'INDGvReferralEntry
        '
        Me.INDGvReferralEntry.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvReferralEntry.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvReferralEntry.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvReferralEntry.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvReferralEntry.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvReferralEntry.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvReferralEntry.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvReferralEntry.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvReferralEntry.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvReferralEntry.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvReferralEntry.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvReferralEntry.Appearance.Row.Options.UseFont = True
        Me.INDGvReferralEntry.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvReferralEntry.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvReferralEntry.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolProduct, Me.GridColumn8, Me.INDcolAmount, Me.INDcolUnitValue, Me.INDColDecountPercentage, Me.INDColGrossUnitValue, Me.INDColSubtotalNet, Me.INDcolIVA, Me.INDColTotal, Me.INDcolLot, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11})
        Me.INDGvReferralEntry.DetailHeight = 431
        Me.INDGvReferralEntry.GridControl = Me.INDGcReferralEntry
        Me.INDGvReferralEntry.Name = "INDGvReferralEntry"
        Me.INDGvReferralEntry.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvReferralEntry.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvReferralEntry.OptionsView.ShowAutoFilterRow = True
        Me.INDGvReferralEntry.OptionsView.ShowDetailButtons = False
        Me.INDGvReferralEntry.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvReferralEntry, False)
        '
        'INDcolProduct
        '
        Me.INDcolProduct.Caption = "Producto"
        Me.INDcolProduct.FieldName = "CodeNameProduct"
        Me.INDcolProduct.MinWidth = 23
        Me.INDcolProduct.Name = "INDcolProduct"
        Me.INDcolProduct.OptionsColumn.AllowEdit = False
        Me.INDcolProduct.OptionsColumn.AllowFocus = False
        Me.INDcolProduct.Visible = True
        Me.INDcolProduct.VisibleIndex = 0
        Me.INDcolProduct.Width = 87
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Origen"
        Me.GridColumn8.ColumnEdit = Me.INDRptIcbRemissionSource
        Me.GridColumn8.FieldName = "RemissionSource"
        Me.GridColumn8.MinWidth = 23
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 87
        '
        'INDRptIcbRemissionSource
        '
        Me.INDRptIcbRemissionSource.AutoHeight = False
        Me.INDRptIcbRemissionSource.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguno", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Orden de Compra", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Contrato", CType(3, Byte), -1)})
        Me.INDRptIcbRemissionSource.Name = "INDRptIcbRemissionSource"
        '
        'INDcolAmount
        '
        Me.INDcolAmount.Caption = "Cantidad"
        Me.INDcolAmount.FieldName = "Quantity"
        Me.INDcolAmount.MinWidth = 23
        Me.INDcolAmount.Name = "INDcolAmount"
        Me.INDcolAmount.OptionsColumn.AllowEdit = False
        Me.INDcolAmount.OptionsColumn.AllowFocus = False
        Me.INDcolAmount.Visible = True
        Me.INDcolAmount.VisibleIndex = 2
        Me.INDcolAmount.Width = 87
        '
        'INDcolUnitValue
        '
        Me.INDcolUnitValue.Caption = "Valor Unitario"
        Me.INDcolUnitValue.DisplayFormat.FormatString = "c2"
        Me.INDcolUnitValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolUnitValue.FieldName = "GrossUnitValue"
        Me.INDcolUnitValue.MinWidth = 23
        Me.INDcolUnitValue.Name = "INDcolUnitValue"
        Me.INDcolUnitValue.OptionsColumn.AllowEdit = False
        Me.INDcolUnitValue.OptionsColumn.AllowFocus = False
        Me.INDcolUnitValue.Visible = True
        Me.INDcolUnitValue.VisibleIndex = 3
        Me.INDcolUnitValue.Width = 87
        '
        'INDColDecountPercentage
        '
        Me.INDColDecountPercentage.Caption = "% Descuento"
        Me.INDColDecountPercentage.DisplayFormat.FormatString = "p"
        Me.INDColDecountPercentage.FieldName = "DiscountPercentage"
        Me.INDColDecountPercentage.MinWidth = 23
        Me.INDColDecountPercentage.Name = "INDColDecountPercentage"
        Me.INDColDecountPercentage.Visible = True
        Me.INDColDecountPercentage.VisibleIndex = 4
        Me.INDColDecountPercentage.Width = 87
        '
        'INDColGrossUnitValue
        '
        Me.INDColGrossUnitValue.Caption = "Costo Producto"
        Me.INDColGrossUnitValue.DisplayFormat.FormatString = "c2"
        Me.INDColGrossUnitValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColGrossUnitValue.FieldName = "UnitValue"
        Me.INDColGrossUnitValue.MinWidth = 23
        Me.INDColGrossUnitValue.Name = "INDColGrossUnitValue"
        Me.INDColGrossUnitValue.Visible = True
        Me.INDColGrossUnitValue.VisibleIndex = 5
        Me.INDColGrossUnitValue.Width = 87
        '
        'INDColSubtotalNet
        '
        Me.INDColSubtotalNet.Caption = "Subtotal"
        Me.INDColSubtotalNet.DisplayFormat.FormatString = "c2"
        Me.INDColSubtotalNet.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSubtotalNet.FieldName = "NetDiscount"
        Me.INDColSubtotalNet.MinWidth = 23
        Me.INDColSubtotalNet.Name = "INDColSubtotalNet"
        Me.INDColSubtotalNet.Visible = True
        Me.INDColSubtotalNet.VisibleIndex = 6
        Me.INDColSubtotalNet.Width = 87
        '
        'INDcolIVA
        '
        Me.INDcolIVA.Caption = "Valor IVA"
        Me.INDcolIVA.DisplayFormat.FormatString = "c2"
        Me.INDcolIVA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolIVA.FieldName = "IvaValue"
        Me.INDcolIVA.MinWidth = 23
        Me.INDcolIVA.Name = "INDcolIVA"
        Me.INDcolIVA.OptionsColumn.AllowEdit = False
        Me.INDcolIVA.OptionsColumn.AllowFocus = False
        Me.INDcolIVA.Visible = True
        Me.INDcolIVA.VisibleIndex = 7
        Me.INDcolIVA.Width = 87
        '
        'INDColTotal
        '
        Me.INDColTotal.Caption = "Valor Total"
        Me.INDColTotal.DisplayFormat.FormatString = "c2"
        Me.INDColTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColTotal.FieldName = "TotalValue"
        Me.INDColTotal.MinWidth = 23
        Me.INDColTotal.Name = "INDColTotal"
        Me.INDColTotal.Visible = True
        Me.INDColTotal.VisibleIndex = 8
        Me.INDColTotal.Width = 87
        '
        'INDcolLot
        '
        Me.INDcolLot.Caption = "Lote/Serial"
        Me.INDcolLot.ColumnEdit = Me.INDRptPceBatchSerial
        Me.INDcolLot.MinWidth = 23
        Me.INDcolLot.Name = "INDcolLot"
        Me.INDcolLot.Visible = True
        Me.INDcolLot.VisibleIndex = 9
        Me.INDcolLot.Width = 87
        '
        'INDRptPceBatchSerial
        '
        Me.INDRptPceBatchSerial.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptPceBatchSerial.AutoHeight = False
        Me.INDRptPceBatchSerial.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptPceBatchSerial.Name = "INDRptPceBatchSerial"
        Me.INDRptPceBatchSerial.PopupControl = Me.INDPccBatchSerial
        Me.INDRptPceBatchSerial.PopupSizeable = False
        Me.INDRptPceBatchSerial.ShowPopupCloseButton = False
        Me.INDRptPceBatchSerial.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Fabricante"
        Me.GridColumn9.FieldName = "ManufacturerName"
        Me.GridColumn9.MinWidth = 23
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Width = 87
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Registro Sanitario"
        Me.GridColumn10.FieldName = "HealthRegistration"
        Me.GridColumn10.MinWidth = 23
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Width = 87
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Presentación"
        Me.GridColumn11.FieldName = "Presentation"
        Me.GridColumn11.MinWidth = 23
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Width = 87
        '
        'INDSleWareHouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWareHouse, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWareHouse, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWareHouse, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleWareHouse, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleWareHouse, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleWareHouse, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWareHouse, False)
        Me.INDSleWareHouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWareHouse, False)
        Me.INDSleWareHouse.Location = New System.Drawing.Point(27, 389)
        Me.INDSleWareHouse.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleWareHouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleWareHouse.Name = "INDSleWareHouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWareHouse, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWareHouse, False)
        Me.INDSleWareHouse.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleWareHouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleWareHouse.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleWareHouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleWareHouse.Properties.Appearance.Options.UseFont = True
        Me.INDSleWareHouse.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleWareHouse.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleWareHouse.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleWareHouse.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleWareHouse.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleWareHouse.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleWareHouse.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleWareHouse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleWareHouse.Properties.DisplayMember = "CodeName"
        Me.INDSleWareHouse.Properties.NullText = ""
        Me.INDSleWareHouse.Properties.PopupSizeable = False
        Me.INDSleWareHouse.Properties.PopupView = Me.INDGdvWareHouse
        Me.INDSleWareHouse.Properties.ShowClearButton = False
        Me.INDSleWareHouse.Properties.ShowFooter = False
        Me.INDSleWareHouse.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWareHouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWareHouse, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWareHouse, True)
        Me.INDSleWareHouse.Size = New System.Drawing.Size(451, 34)
        Me.INDSleWareHouse.StyleController = Me.INDlcReferralEntry
        Me.INDSleWareHouse.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWareHouse, "302")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleWareHouse, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWareHouse, "{0} - {1}")
        Me.INDSleWareHouse.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWareHouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWareHouse, False)
        '
        'INDGdvWareHouse
        '
        Me.INDGdvWareHouse.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGdvWareHouse.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGdvWareHouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGdvWareHouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGdvWareHouse.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGdvWareHouse.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvWareHouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDGdvWareHouse.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGdvWareHouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGdvWareHouse.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGdvWareHouse.Appearance.Row.Options.UseFont = True
        Me.INDGdvWareHouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn5})
        Me.INDGdvWareHouse.DetailHeight = 431
        Me.INDGdvWareHouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGdvWareHouse.Name = "INDGdvWareHouse"
        Me.INDGdvWareHouse.OptionsFind.FindFilterColumns = "Code"
        Me.INDGdvWareHouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGdvWareHouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGdvWareHouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGdvWareHouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGdvWareHouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGdvWareHouse, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.MinWidth = 23
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 472
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nombre"
        Me.GridColumn5.FieldName = "Name"
        Me.GridColumn5.MinWidth = 23
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 1151
        '
        'INDTxtReferalNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtReferalNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtReferalNumber, True)
        Me.INDTxtReferalNumber.EnterMoveNextControl = True
        Me.INDTxtReferalNumber.Location = New System.Drawing.Point(27, 315)
        Me.INDTxtReferalNumber.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtReferalNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtReferalNumber.Name = "INDTxtReferalNumber"
        Me.INDTxtReferalNumber.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtReferalNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtReferalNumber.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtReferalNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtReferalNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxtReferalNumber.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtReferalNumber.Properties.MaxLength = 50
        Me.INDTxtReferalNumber.Size = New System.Drawing.Size(451, 34)
        Me.INDTxtReferalNumber.StyleController = Me.INDlcReferralEntry
        Me.INDTxtReferalNumber.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtReferalNumber, 0)
        Me.INDTxtReferalNumber.ToolTip = "Este Campo es Necesario"
        '
        'INDDteDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDate, True)
        Me.INDDteDate.EditValue = Nothing
        Me.INDDteDate.EnterMoveNextControl = True
        Me.INDDteDate.Location = New System.Drawing.Point(27, 167)
        Me.INDDteDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteDate.Name = "INDDteDate"
        Me.INDDteDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDDteDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDDteDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDteDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteDate.Size = New System.Drawing.Size(451, 34)
        Me.INDDteDate.StyleController = Me.INDlcReferralEntry
        Me.INDDteDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDate, 0)
        Me.INDDteDate.ToolTip = "Este Campo es Necesario"
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        Me.INDBteCode.Location = New System.Drawing.Point(27, 94)
        Me.INDBteCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Size = New System.Drawing.Size(451, 34)
        Me.INDBteCode.StyleController = Me.INDlcReferralEntry
        Me.INDBteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        Me.INDBteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDsleCurrency
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCurrency, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCurrency, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCurrency, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Location = New System.Drawing.Point(27, 463)
        Me.INDsleCurrency.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCurrency, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCurrency.Name = "INDsleCurrency"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCurrency.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCurrency.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCurrency.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseFont = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCurrency.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCurrency.Properties.DisplayMember = "Abbreviation"
        Me.INDsleCurrency.Properties.NullText = ""
        Me.INDsleCurrency.Properties.PopupFormMinSize = New System.Drawing.Size(700, 0)
        Me.INDsleCurrency.Properties.PopupSizeable = False
        Me.INDsleCurrency.Properties.PopupView = Me.INDGvCurrencyAdvance
        Me.INDsleCurrency.Properties.ShowFooter = False
        Me.INDsleCurrency.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCurrency, True)
        Me.INDsleCurrency.Size = New System.Drawing.Size(451, 34)
        Me.INDsleCurrency.StyleController = Me.INDlcReferralEntry
        Me.INDsleCurrency.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCurrency, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCurrency, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCurrency, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCurrency, False)
        '
        'INDGvCurrencyAdvance
        '
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCurrencyAdvance.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvCurrencyAdvance.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCurrencyAdvance.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCurrencyAdvance.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCurrencyAdvance.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCurrencyAdvance.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCurrencyAdvance.Appearance.Row.Options.UseFont = True
        Me.INDGvCurrencyAdvance.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1179, Me.GridColumn1180, Me.GridColumn1181})
        Me.INDGvCurrencyAdvance.DetailHeight = 431
        Me.INDGvCurrencyAdvance.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCurrencyAdvance.Name = "INDGvCurrencyAdvance"
        Me.INDGvCurrencyAdvance.OptionsFind.FindFilterColumns = ""
        Me.INDGvCurrencyAdvance.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCurrencyAdvance.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCurrencyAdvance.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCurrencyAdvance.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCurrencyAdvance.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCurrencyAdvance, False)
        '
        'GridColumn1179
        '
        Me.GridColumn1179.Caption = "Codígo"
        Me.GridColumn1179.FieldName = "Codigo"
        Me.GridColumn1179.MinWidth = 23
        Me.GridColumn1179.Name = "GridColumn1179"
        Me.GridColumn1179.Visible = True
        Me.GridColumn1179.VisibleIndex = 0
        Me.GridColumn1179.Width = 87
        '
        'GridColumn1180
        '
        Me.GridColumn1180.Caption = "Nombre"
        Me.GridColumn1180.FieldName = "CurrencyName"
        Me.GridColumn1180.MinWidth = 23
        Me.GridColumn1180.Name = "GridColumn1180"
        Me.GridColumn1180.Visible = True
        Me.GridColumn1180.VisibleIndex = 1
        Me.GridColumn1180.Width = 87
        '
        'GridColumn1181
        '
        Me.GridColumn1181.Caption = "Abreviación"
        Me.GridColumn1181.FieldName = "Abbreviation"
        Me.GridColumn1181.MinWidth = 23
        Me.GridColumn1181.Name = "GridColumn1181"
        Me.GridColumn1181.Visible = True
        Me.GridColumn1181.VisibleIndex = 2
        Me.GridColumn1181.Width = 87
        '
        'INDlygReferralEntry
        '
        Me.INDlygReferralEntry.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygReferralEntry.AppearanceGroup.Options.UseFont = True
        Me.INDlygReferralEntry.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygReferralEntry.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygReferralEntry.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReferralEntry.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygReferralEntry.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygReferralEntry.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygReferralEntry.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReferralEntry.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygReferralEntry.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReferralEntry.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygReferralEntry.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReferralEntry.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygReferralEntry, False)
        Me.INDlygReferralEntry.CustomizationFormText = "Remisión de Entrada"
        Me.INDlygReferralEntry.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlygReferralEntry.GroupBordersVisible = False
        Me.INDlygReferralEntry.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMainData, Me.INDLcgProducts})
        Me.INDlygReferralEntry.Name = "Root"
        Me.INDlygReferralEntry.Size = New System.Drawing.Size(1533, 800)
        Me.INDlygReferralEntry.TextVisible = False
        '
        'INDLcgMainData
        '
        Me.INDLcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMainData, False)
        Me.INDLcgMainData.CustomizationFormText = "Datos Principales"
        Me.INDLcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciDate, Me.INDLciDetail, Me.INDLciSupplierDistributionLine, Me.INDLciReferal, Me.INDLciWareHouse, Me.INDLciCurrency})
        Me.INDLcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMainData.Name = "INDLcgMainData"
        Me.INDLcgMainData.Size = New System.Drawing.Size(481, 776)
        Me.INDLcgMainData.Text = "Datos Principales"
        '
        'INDLciCode
        '
        Me.INDLciCode.AllowHide = False
        Me.INDLciCode.Control = Me.INDBteCode
        Me.INDLciCode.CustomizationFormText = "Código"
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciCode.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(455, 74)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(157, 26)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciDate
        '
        Me.INDLciDate.AllowHide = False
        Me.INDLciDate.Control = Me.INDDteDate
        Me.INDLciDate.CustomizationFormText = "Fecha"
        Me.INDLciDate.Location = New System.Drawing.Point(0, 74)
        Me.INDLciDate.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciDate.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciDate.Name = "INDLciDate"
        Me.INDLciDate.ShowInCustomizationForm = False
        Me.INDLciDate.Size = New System.Drawing.Size(455, 74)
        Me.INDLciDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDate.Text = "Fecha"
        Me.INDLciDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDate.TextSize = New System.Drawing.Size(157, 25)
        Me.INDLciDate.TextToControlDistance = 5
        '
        'INDLciDetail
        '
        Me.INDLciDetail.Control = Me.INDMeDetail
        Me.INDLciDetail.CustomizationFormText = "Detalle"
        Me.INDLciDetail.Location = New System.Drawing.Point(0, 444)
        Me.INDLciDetail.MaxSize = New System.Drawing.Size(455, 148)
        Me.INDLciDetail.MinSize = New System.Drawing.Size(455, 148)
        Me.INDLciDetail.Name = "INDLciDetail"
        Me.INDLciDetail.Size = New System.Drawing.Size(455, 269)
        Me.INDLciDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDetail.Text = "Detalle"
        Me.INDLciDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDetail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDetail.TextSize = New System.Drawing.Size(153, 26)
        Me.INDLciDetail.TextToControlDistance = 5
        '
        'INDLciSupplierDistributionLine
        '
        Me.INDLciSupplierDistributionLine.AllowHide = False
        Me.INDLciSupplierDistributionLine.Control = Me.INDSleSupplierDistributionLine
        Me.INDLciSupplierDistributionLine.CustomizationFormText = "Proveedor"
        Me.INDLciSupplierDistributionLine.Location = New System.Drawing.Point(0, 148)
        Me.INDLciSupplierDistributionLine.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciSupplierDistributionLine.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciSupplierDistributionLine.Name = "INDLciSupplierDistributionLine"
        Me.INDLciSupplierDistributionLine.ShowInCustomizationForm = False
        Me.INDLciSupplierDistributionLine.Size = New System.Drawing.Size(455, 74)
        Me.INDLciSupplierDistributionLine.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSupplierDistributionLine.Text = "Proveedor"
        Me.INDLciSupplierDistributionLine.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSupplierDistributionLine.TextSize = New System.Drawing.Size(86, 23)
        '
        'INDLciReferal
        '
        Me.INDLciReferal.AllowHide = False
        Me.INDLciReferal.Control = Me.INDTxtReferalNumber
        Me.INDLciReferal.CustomizationFormText = "Nº Remision"
        Me.INDLciReferal.Location = New System.Drawing.Point(0, 222)
        Me.INDLciReferal.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciReferal.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciReferal.Name = "INDLciReferal"
        Me.INDLciReferal.ShowInCustomizationForm = False
        Me.INDLciReferal.Size = New System.Drawing.Size(455, 74)
        Me.INDLciReferal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReferal.Text = "Nº Remisión"
        Me.INDLciReferal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciReferal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReferal.TextSize = New System.Drawing.Size(157, 25)
        Me.INDLciReferal.TextToControlDistance = 5
        '
        'INDLciWareHouse
        '
        Me.INDLciWareHouse.AllowHide = False
        Me.INDLciWareHouse.Control = Me.INDSleWareHouse
        Me.INDLciWareHouse.CustomizationFormText = "Almacén"
        Me.INDLciWareHouse.Location = New System.Drawing.Point(0, 296)
        Me.INDLciWareHouse.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciWareHouse.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciWareHouse.Name = "INDLciWareHouse"
        Me.INDLciWareHouse.ShowInCustomizationForm = False
        Me.INDLciWareHouse.Size = New System.Drawing.Size(455, 74)
        Me.INDLciWareHouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciWareHouse.Text = "Almacén"
        Me.INDLciWareHouse.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciWareHouse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciWareHouse.TextSize = New System.Drawing.Size(157, 25)
        Me.INDLciWareHouse.TextToControlDistance = 5
        '
        'INDLciCurrency
        '
        Me.INDLciCurrency.Control = Me.INDsleCurrency
        Me.INDLciCurrency.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciCurrency.CustomizationFormText = "Moneda"
        Me.INDLciCurrency.Location = New System.Drawing.Point(0, 370)
        Me.INDLciCurrency.MaxSize = New System.Drawing.Size(455, 74)
        Me.INDLciCurrency.MinSize = New System.Drawing.Size(455, 74)
        Me.INDLciCurrency.Name = "INDLciCurrency"
        Me.INDLciCurrency.Size = New System.Drawing.Size(455, 74)
        Me.INDLciCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCurrency.Text = "Moneda"
        Me.INDLciCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCurrency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCurrency.TextSize = New System.Drawing.Size(157, 25)
        Me.INDLciCurrency.TextToControlDistance = 5
        '
        'INDLcgProducts
        '
        Me.INDLcgProducts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProducts.AppearanceGroup.Options.UseFont = True
        Me.INDLcgProducts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProducts.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgProducts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProducts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgProducts, False)
        Me.INDLcgProducts.CustomizationFormText = "Listado de Productos"
        Me.INDLcgProducts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemReferralEntry, Me.LayoutControlItem2})
        Me.INDLcgProducts.Location = New System.Drawing.Point(481, 0)
        Me.INDLcgProducts.Name = "INDLcgProducts"
        Me.INDLcgProducts.Size = New System.Drawing.Size(1028, 776)
        Me.INDLcgProducts.Text = "Listado de Productos"
        '
        'INDlyItemReferralEntry
        '
        Me.INDlyItemReferralEntry.Control = Me.INDGcReferralEntry
        Me.INDlyItemReferralEntry.CustomizationFormText = "Productos"
        Me.INDlyItemReferralEntry.Location = New System.Drawing.Point(0, 44)
        Me.INDlyItemReferralEntry.MaxSize = New System.Drawing.Size(966, 0)
        Me.INDlyItemReferralEntry.MinSize = New System.Drawing.Size(966, 1)
        Me.INDlyItemReferralEntry.Name = "INDlyItemReferralEntry"
        Me.INDlyItemReferralEntry.Size = New System.Drawing.Size(1002, 669)
        Me.INDlyItemReferralEntry.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemReferralEntry.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemReferralEntry.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemReferralEntry.TextToControlDistance = 0
        Me.INDlyItemReferralEntry.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDBtnAdd
        Me.LayoutControlItem2.CustomizationFormText = "Agregar Producto"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(966, 44)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(966, 44)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1002, 44)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmReferralEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1737, 948)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "FrmReferralEntry"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "317"
        Me.Text = "Remisión de Entrada"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcReferralEntry.ResumeLayout(False)
        CType(Me.INDPccBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccBatchSerial.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSupplierDistributionLine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSleSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIcbRemissionSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptPceBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWareHouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtReferalNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCurrencyAdvance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSupplierDistributionLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReferal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProducts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcReferralEntry As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlygReferralEntry As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDGcReferralEntry As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvReferralEntry As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleWareHouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGdvWareHouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTxtReferalNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDDteDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciReferal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciWareHouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemReferralEntry As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgProducts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDcolProduct As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolLot As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolAmount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolUnitValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolIVA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDMeDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleSupplierDistributionLine As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvSleSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciSupplierDistributionLine As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPccBatchSerial As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcBatchSerial As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvBatchSerial As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRptPceBatchSerial As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptIcbRemissionSource As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleCurrency As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCurrencyAdvance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1179 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1180 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1181 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciCurrency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColDecountPercentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColGrossUnitValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSubtotalNet As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTotal As DevExpress.XtraGrid.Columns.GridColumn
End Class
