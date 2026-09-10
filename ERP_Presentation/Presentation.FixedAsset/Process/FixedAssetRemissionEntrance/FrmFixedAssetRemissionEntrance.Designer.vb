Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetRemissionEntrance
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
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetRemissionEntrance))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcReferralEntry = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSlResponsible = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtreeLocation = New DevExpress.XtraEditors.TreeListLookUpEdit()
        Me.TreeListLookUpEdit1TreeList = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.INDSlLocationResponsible = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleSupplierDistributionLine = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvSleSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMeDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDGcReferralEntry = New DevExpress.XtraGrid.GridControl()
        Me.INDGvReferralEntry = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolEquipment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolAmount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolUnitValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolSubtotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolIVA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolTrademark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptPceBatchSerial = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDRptIcbRemissionSource = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDSleAdquisitionType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGdvWareHouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtReferalNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDDteDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlygReferralEntry = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSupplierDistributionLine = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAdquisitionType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReferal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciLocationResponsible = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciLocation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciResponsible = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygItem = New DevExpress.XtraLayout.LayoutControlGroup()
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
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTreeList1 = New Presentation.Controls.IndigoTreeList(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcReferralEntry.SuspendLayout()
        CType(Me.INDSlResponsible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtreeLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlLocationResponsible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSupplierDistributionLine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSleSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptPceBatchSerial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIcbRemissionSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleAdquisitionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGdvWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtReferalNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygReferralEntry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSupplierDistributionLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdquisitionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReferal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLocationResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygItem, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcReferralEntry)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1362, 619)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1362, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1362, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.CtrNavigationControl1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcReferralEntry
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 610)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcReferralEntry
        '
        Me.INDlcReferralEntry.AllowCustomization = False
        Me.INDlcReferralEntry.Controls.Add(Me.INDSlResponsible)
        Me.INDlcReferralEntry.Controls.Add(Me.INDtreeLocation)
        Me.INDlcReferralEntry.Controls.Add(Me.INDSlLocationResponsible)
        Me.INDlcReferralEntry.Controls.Add(Me.INDSleSupplierDistributionLine)
        Me.INDlcReferralEntry.Controls.Add(Me.INDBtnAdd)
        Me.INDlcReferralEntry.Controls.Add(Me.INDMeDetail)
        Me.INDlcReferralEntry.Controls.Add(Me.INDGcReferralEntry)
        Me.INDlcReferralEntry.Controls.Add(Me.INDSleAdquisitionType)
        Me.INDlcReferralEntry.Controls.Add(Me.INDTxtReferalNumber)
        Me.INDlcReferralEntry.Controls.Add(Me.INDDteDate)
        Me.INDlcReferralEntry.Controls.Add(Me.INDBteCode)
        Me.INDlcReferralEntry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlcReferralEntry, False)
        Me.INDlcReferralEntry.Location = New System.Drawing.Point(202, 7)
        Me.INDlcReferralEntry.Name = "INDlcReferralEntry"
        Me.INDlcReferralEntry.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(333, 284, 670, 521)
        Me.INDlcReferralEntry.Root = Me.INDlygReferralEntry
        Me.INDlcReferralEntry.Size = New System.Drawing.Size(1158, 610)
        Me.INDlcReferralEntry.TabIndex = 1
        Me.INDlcReferralEntry.Text = "LayoutControl1"
        '
        'INDSlResponsible
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlResponsible, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlResponsible, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlResponsible, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlResponsible, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlResponsible, False)
        Me.INDSlResponsible.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlResponsible, False)
        Me.INDSlResponsible.Location = New System.Drawing.Point(438, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDSlResponsible, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlResponsible.Name = "INDSlResponsible"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlResponsible, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlResponsible, False)
        Me.INDSlResponsible.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlResponsible.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlResponsible.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlResponsible.Properties.Appearance.Options.UseFont = True
        Me.INDSlResponsible.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlResponsible.Properties.DisplayMember = "ThirdPartyId.Name"
        Me.INDSlResponsible.Properties.NullText = ""
        Me.INDSlResponsible.Properties.PopupSizeable = False
        Me.INDSlResponsible.Properties.ShowFooter = False
        Me.INDSlResponsible.Properties.ValueMember = "Id"
        Me.INDSlResponsible.Properties.View = Me.SearchLookUpEdit2View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlResponsible, True)
        Me.INDSlResponsible.Size = New System.Drawing.Size(386, 28)
        Me.INDSlResponsible.StyleController = Me.INDlcReferralEntry
        Me.INDSlResponsible.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlResponsible, "1711")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlResponsible, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlResponsible, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlResponsible, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlResponsible, False)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn7})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Código"
        Me.GridColumn6.FieldName = "Code"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.ReadOnly = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        Me.GridColumn6.Width = 60
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Descripción"
        Me.GridColumn7.FieldName = "ThirdPartyId.Name"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        '
        'INDtreeLocation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtreeLocation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtreeLocation, False)
        Me.INDtreeLocation.EnterMoveNextControl = True
        Me.INDtreeLocation.Location = New System.Drawing.Point(438, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDtreeLocation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtreeLocation.Name = "INDtreeLocation"
        Me.INDtreeLocation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtreeLocation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtreeLocation.Properties.Appearance.Options.UseBackColor = True
        Me.INDtreeLocation.Properties.Appearance.Options.UseFont = True
        Me.INDtreeLocation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDtreeLocation.Properties.DisplayMember = "CodeName"
        Me.INDtreeLocation.Properties.NullText = ""
        Me.INDtreeLocation.Properties.TreeList = Me.TreeListLookUpEdit1TreeList
        Me.INDtreeLocation.Properties.ValueMember = "Id"
        Me.INDtreeLocation.Size = New System.Drawing.Size(386, 28)
        Me.INDtreeLocation.StyleController = Me.INDlcReferralEntry
        Me.INDtreeLocation.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtreeLocation, 0)
        '
        'TreeListLookUpEdit1TreeList
        '
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.FocusedRow.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.TreeListLookUpEdit1TreeList.Appearance.HeaderPanel.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseFont = True
        Me.TreeListLookUpEdit1TreeList.Appearance.Row.Options.UseForeColor = True
        Me.TreeListLookUpEdit1TreeList.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1})
        Me.IndigoTreeList1.SetExpandedAllNodes(Me.TreeListLookUpEdit1TreeList, False)
        Me.TreeListLookUpEdit1TreeList.KeyFieldName = "Id"
        Me.TreeListLookUpEdit1TreeList.Location = New System.Drawing.Point(0, 0)
        Me.TreeListLookUpEdit1TreeList.Name = "TreeListLookUpEdit1TreeList"
        Me.TreeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = True
        Me.TreeListLookUpEdit1TreeList.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.EnableAppearanceOddRow = True
        Me.TreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = True
        Me.TreeListLookUpEdit1TreeList.ParentFieldName = "PadreId"
        Me.TreeListLookUpEdit1TreeList.Size = New System.Drawing.Size(400, 200)
        Me.TreeListLookUpEdit1TreeList.TabIndex = 0
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Descripción"
        Me.TreeListColumn1.FieldName = "CodeName"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        '
        'INDSlLocationResponsible
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlLocationResponsible, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlLocationResponsible, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlLocationResponsible, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.INDSlLocationResponsible.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.INDSlLocationResponsible.Location = New System.Drawing.Point(438, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDSlLocationResponsible, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlLocationResponsible.Name = "INDSlLocationResponsible"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.INDSlLocationResponsible.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlLocationResponsible.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlLocationResponsible.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlLocationResponsible.Properties.Appearance.Options.UseFont = True
        Me.INDSlLocationResponsible.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlLocationResponsible.Properties.DisplayMember = "Item2"
        Me.INDSlLocationResponsible.Properties.NullText = ""
        Me.INDSlLocationResponsible.Properties.PopupSizeable = False
        Me.INDSlLocationResponsible.Properties.ShowFooter = False
        Me.INDSlLocationResponsible.Properties.ValueMember = "Item1"
        Me.INDSlLocationResponsible.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlLocationResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlLocationResponsible, True)
        Me.INDSlLocationResponsible.Size = New System.Drawing.Size(386, 28)
        Me.INDSlLocationResponsible.StyleController = Me.INDlcReferralEntry
        Me.INDSlLocationResponsible.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlLocationResponsible, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlLocationResponsible, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlLocationResponsible, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlLocationResponsible, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlLocationResponsible, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Descripción"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDSleSupplierDistributionLine
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleSupplierDistributionLine, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleSupplierDistributionLine, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.INDSleSupplierDistributionLine.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.INDSleSupplierDistributionLine.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleSupplierDistributionLine, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleSupplierDistributionLine.Name = "INDSleSupplierDistributionLine"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleSupplierDistributionLine, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.INDSleSupplierDistributionLine.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleSupplierDistributionLine.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleSupplierDistributionLine.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleSupplierDistributionLine.Properties.Appearance.Options.UseFont = True
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleSupplierDistributionLine.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleSupplierDistributionLine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSupplierDistributionLine.Properties.DisplayMember = "DisplaySupplier"
        Me.INDSleSupplierDistributionLine.Properties.NullText = ""
        Me.INDSleSupplierDistributionLine.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDSleSupplierDistributionLine.Properties.PopupSizeable = False
        Me.INDSleSupplierDistributionLine.Properties.ShowClearButton = False
        Me.INDSleSupplierDistributionLine.Properties.ShowFooter = False
        Me.INDSleSupplierDistributionLine.Properties.ValueMember = "Id"
        Me.INDSleSupplierDistributionLine.Properties.View = Me.INDGvSleSupplier
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleSupplierDistributionLine, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleSupplierDistributionLine, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleSupplierDistributionLine, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleSupplierDistributionLine, True)
        Me.INDSleSupplierDistributionLine.Size = New System.Drawing.Size(386, 28)
        Me.INDSleSupplierDistributionLine.StyleController = Me.INDlcReferralEntry
        Me.INDSleSupplierDistributionLine.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleSupplierDistributionLine, "558")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleSupplierDistributionLine, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleSupplierDistributionLine, "{0} - {1}")
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
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 352
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Proveedor"
        Me.GridColumn2.FieldName = "IdSupplier.CodeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 657
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Línea Distribución"
        Me.GridColumn3.FieldName = "IdDistributionLine.CodeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 623
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnAdd.Appearance.Options.UseFont = True
        Me.INDBtnAdd.Location = New System.Drawing.Point(852, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAdd, True)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(824, 32)
        Me.INDBtnAdd.StyleController = Me.INDlcReferralEntry
        Me.INDBtnAdd.TabIndex = 9
        Me.INDBtnAdd.Text = "Agregar Artículo"
        '
        'INDMeDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDetail, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDetail, False)
        Me.INDMeDetail.EnterMoveNextControl = True
        Me.INDMeDetail.Location = New System.Drawing.Point(24, 265)
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
        Me.INDMeDetail.Properties.MaxLength = 300
        Me.INDMeDetail.Size = New System.Drawing.Size(386, 70)
        Me.INDMeDetail.StyleController = Me.INDlcReferralEntry
        Me.INDMeDetail.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDetail, 0)
        '
        'INDGcReferralEntry
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcReferralEntry, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcReferralEntry, Nothing)
        Me.INDGcReferralEntry.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcReferralEntry, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcReferralEntry, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcReferralEntry, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcReferralEntry, False)
        Me.INDGcReferralEntry.Location = New System.Drawing.Point(852, 95)
        Me.INDGcReferralEntry.MainView = Me.INDGvReferralEntry
        Me.INDGcReferralEntry.MaximumSize = New System.Drawing.Size(824, 0)
        Me.INDGcReferralEntry.MinimumSize = New System.Drawing.Size(824, 0)
        Me.INDGcReferralEntry.Name = "INDGcReferralEntry"
        Me.INDGcReferralEntry.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptPceBatchSerial, Me.INDRptIcbRemissionSource})
        Me.INDGcReferralEntry.Size = New System.Drawing.Size(824, 474)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcReferralEntry, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcReferralEntry.TabIndex = 10
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
        Me.INDGvReferralEntry.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolEquipment, Me.INDcolAmount, Me.INDcolUnitValue, Me.INDcolSubtotal, Me.INDcolIVA, Me.INDcolTrademark})
        Me.INDGvReferralEntry.GridControl = Me.INDGcReferralEntry
        Me.INDGvReferralEntry.Name = "INDGvReferralEntry"
        Me.INDGvReferralEntry.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvReferralEntry.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvReferralEntry.OptionsView.ShowAutoFilterRow = True
        Me.INDGvReferralEntry.OptionsView.ShowDetailButtons = False
        Me.INDGvReferralEntry.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvReferralEntry, False)
        '
        'INDcolEquipment
        '
        Me.INDcolEquipment.Caption = "Equipo"
        Me.INDcolEquipment.FieldName = "NameEquipment"
        Me.INDcolEquipment.Name = "INDcolEquipment"
        Me.INDcolEquipment.OptionsColumn.AllowEdit = False
        Me.INDcolEquipment.OptionsColumn.AllowFocus = False
        Me.INDcolEquipment.OptionsColumn.ReadOnly = True
        Me.INDcolEquipment.Visible = True
        Me.INDcolEquipment.VisibleIndex = 0
        '
        'INDcolAmount
        '
        Me.INDcolAmount.Caption = "Cantidad"
        Me.INDcolAmount.FieldName = "Quantity"
        Me.INDcolAmount.Name = "INDcolAmount"
        Me.INDcolAmount.OptionsColumn.AllowEdit = False
        Me.INDcolAmount.OptionsColumn.AllowFocus = False
        Me.INDcolAmount.OptionsColumn.ReadOnly = True
        Me.INDcolAmount.Visible = True
        Me.INDcolAmount.VisibleIndex = 1
        '
        'INDcolUnitValue
        '
        Me.INDcolUnitValue.Caption = "Valor Unitario"
        Me.INDcolUnitValue.DisplayFormat.FormatString = "c0"
        Me.INDcolUnitValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolUnitValue.FieldName = "UnitValue"
        Me.INDcolUnitValue.Name = "INDcolUnitValue"
        Me.INDcolUnitValue.OptionsColumn.AllowEdit = False
        Me.INDcolUnitValue.OptionsColumn.AllowFocus = False
        Me.INDcolUnitValue.OptionsColumn.ReadOnly = True
        Me.INDcolUnitValue.Visible = True
        Me.INDcolUnitValue.VisibleIndex = 2
        '
        'INDcolSubtotal
        '
        Me.INDcolSubtotal.Caption = "Total"
        Me.INDcolSubtotal.DisplayFormat.FormatString = "c0"
        Me.INDcolSubtotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolSubtotal.FieldName = "TotalValue"
        Me.INDcolSubtotal.Name = "INDcolSubtotal"
        Me.INDcolSubtotal.OptionsColumn.AllowEdit = False
        Me.INDcolSubtotal.OptionsColumn.AllowFocus = False
        Me.INDcolSubtotal.OptionsColumn.ReadOnly = True
        Me.INDcolSubtotal.Visible = True
        Me.INDcolSubtotal.VisibleIndex = 4
        '
        'INDcolIVA
        '
        Me.INDcolIVA.Caption = "Valor IVA"
        Me.INDcolIVA.DisplayFormat.FormatString = "c0"
        Me.INDcolIVA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolIVA.FieldName = "IvaValue"
        Me.INDcolIVA.Name = "INDcolIVA"
        Me.INDcolIVA.OptionsColumn.AllowEdit = False
        Me.INDcolIVA.OptionsColumn.AllowFocus = False
        Me.INDcolIVA.Visible = True
        Me.INDcolIVA.VisibleIndex = 3
        '
        'INDcolTrademark
        '
        Me.INDcolTrademark.Caption = "Marca"
        Me.INDcolTrademark.FieldName = "NameTrademark"
        Me.INDcolTrademark.Name = "INDcolTrademark"
        Me.INDcolTrademark.OptionsColumn.AllowEdit = False
        Me.INDcolTrademark.OptionsColumn.AllowFocus = False
        Me.INDcolTrademark.Visible = True
        Me.INDcolTrademark.VisibleIndex = 5
        '
        'INDRptPceBatchSerial
        '
        Me.INDRptPceBatchSerial.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptPceBatchSerial.AutoHeight = False
        Me.INDRptPceBatchSerial.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptPceBatchSerial.Name = "INDRptPceBatchSerial"
        Me.INDRptPceBatchSerial.PopupSizeable = False
        Me.INDRptPceBatchSerial.ShowPopupCloseButton = False
        Me.INDRptPceBatchSerial.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDRptIcbRemissionSource
        '
        Me.INDRptIcbRemissionSource.AutoHeight = False
        Me.INDRptIcbRemissionSource.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguno", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Orden de Compra", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Contrato", CType(3, Byte), -1)})
        Me.INDRptIcbRemissionSource.Name = "INDRptIcbRemissionSource"
        '
        'INDSleAdquisitionType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleAdquisitionType, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleAdquisitionType, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleAdquisitionType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.INDSleAdquisitionType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.INDSleAdquisitionType.Location = New System.Drawing.Point(438, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleAdquisitionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleAdquisitionType.Name = "INDSleAdquisitionType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.INDSleAdquisitionType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleAdquisitionType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleAdquisitionType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleAdquisitionType.Properties.Appearance.Options.UseFont = True
        Me.INDSleAdquisitionType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleAdquisitionType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleAdquisitionType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleAdquisitionType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleAdquisitionType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleAdquisitionType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleAdquisitionType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleAdquisitionType.Properties.DisplayMember = "Item2"
        Me.INDSleAdquisitionType.Properties.NullText = ""
        Me.INDSleAdquisitionType.Properties.PopupSizeable = False
        Me.INDSleAdquisitionType.Properties.ShowClearButton = False
        Me.INDSleAdquisitionType.Properties.ShowFooter = False
        Me.INDSleAdquisitionType.Properties.ValueMember = "Item1"
        Me.INDSleAdquisitionType.Properties.View = Me.INDGdvWareHouse
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleAdquisitionType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleAdquisitionType, True)
        Me.INDSleAdquisitionType.Size = New System.Drawing.Size(386, 28)
        Me.INDSleAdquisitionType.StyleController = Me.INDlcReferralEntry
        Me.INDSleAdquisitionType.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleAdquisitionType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleAdquisitionType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleAdquisitionType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleAdquisitionType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleAdquisitionType, False)
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
        Me.INDGdvWareHouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
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
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nombre"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 324
        '
        'INDTxtReferalNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtReferalNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtReferalNumber, False)
        Me.INDTxtReferalNumber.EnterMoveNextControl = True
        Me.INDTxtReferalNumber.Location = New System.Drawing.Point(438, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtReferalNumber, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtReferalNumber.Name = "INDTxtReferalNumber"
        Me.INDTxtReferalNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtReferalNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtReferalNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtReferalNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtReferalNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtReferalNumber.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTxtReferalNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtReferalNumber.Properties.MaxLength = 20
        Me.INDTxtReferalNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtReferalNumber.StyleController = Me.INDlcReferralEntry
        Me.INDTxtReferalNumber.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtReferalNumber, 0)
        '
        'INDDteDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDate, False)
        Me.INDDteDate.EditValue = Nothing
        Me.INDDteDate.EnterMoveNextControl = True
        Me.INDDteDate.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteDate.Name = "INDDteDate"
        Me.INDDteDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDDteDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDteDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDteDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDteDate.StyleController = Me.INDlcReferralEntry
        Me.INDDteDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDate, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, False)
        Me.INDBteCode.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("INDBteCode.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBteCode.StyleController = Me.INDlcReferralEntry
        Me.INDBteCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
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
        Me.INDlygReferralEntry.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMainData, Me.INDlygInformation, Me.INDlygItem})
        Me.INDlygReferralEntry.Location = New System.Drawing.Point(0, 0)
        Me.INDlygReferralEntry.Name = "Root"
        Me.INDlygReferralEntry.Size = New System.Drawing.Size(1700, 593)
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
        Me.INDLcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciDate, Me.INDLciSupplierDistributionLine, Me.INDLciDetail})
        Me.INDLcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMainData.Name = "INDLcgMainData"
        Me.INDLcgMainData.Size = New System.Drawing.Size(414, 573)
        Me.INDLcgMainData.Text = "Datos Principales"
        '
        'INDLciCode
        '
        Me.INDLciCode.AllowHide = False
        Me.INDLciCode.Control = Me.INDBteCode
        Me.INDLciCode.CustomizationFormText = "Código"
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(390, 60)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciDate
        '
        Me.INDLciDate.AllowHide = False
        Me.INDLciDate.Control = Me.INDDteDate
        Me.INDLciDate.CustomizationFormText = "Fecha"
        Me.INDLciDate.Location = New System.Drawing.Point(0, 120)
        Me.INDLciDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDate.Name = "INDLciDate"
        Me.INDLciDate.ShowInCustomizationForm = False
        Me.INDLciDate.Size = New System.Drawing.Size(390, 60)
        Me.INDLciDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDate.Text = "Fecha"
        Me.INDLciDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDate.TextToControlDistance = 5
        '
        'INDLciSupplierDistributionLine
        '
        Me.INDLciSupplierDistributionLine.AllowHide = False
        Me.INDLciSupplierDistributionLine.Control = Me.INDSleSupplierDistributionLine
        Me.INDLciSupplierDistributionLine.CustomizationFormText = "Proveedor"
        Me.INDLciSupplierDistributionLine.Location = New System.Drawing.Point(0, 60)
        Me.INDLciSupplierDistributionLine.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciSupplierDistributionLine.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciSupplierDistributionLine.Name = "INDLciSupplierDistributionLine"
        Me.INDLciSupplierDistributionLine.ShowInCustomizationForm = False
        Me.INDLciSupplierDistributionLine.Size = New System.Drawing.Size(390, 60)
        Me.INDLciSupplierDistributionLine.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSupplierDistributionLine.Text = "Proveedor"
        Me.INDLciSupplierDistributionLine.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSupplierDistributionLine.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciSupplierDistributionLine.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciSupplierDistributionLine.TextToControlDistance = 5
        '
        'INDLciDetail
        '
        Me.INDLciDetail.Control = Me.INDMeDetail
        Me.INDLciDetail.CustomizationFormText = "Detalle"
        Me.INDLciDetail.Location = New System.Drawing.Point(0, 180)
        Me.INDLciDetail.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDLciDetail.MinSize = New System.Drawing.Size(390, 100)
        Me.INDLciDetail.Name = "INDLciDetail"
        Me.INDLciDetail.Size = New System.Drawing.Size(390, 334)
        Me.INDLciDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDetail.Text = "Detalle"
        Me.INDLciDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDetail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDetail.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDetail.TextToControlDistance = 5
        '
        'INDlygInformation
        '
        Me.INDlygInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygInformation, False)
        Me.INDlygInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAdquisitionType, Me.INDLciReferal, Me.INDLciLocationResponsible, Me.INDLciLocation, Me.INDLciResponsible})
        Me.INDlygInformation.Location = New System.Drawing.Point(414, 0)
        Me.INDlygInformation.Name = "INDlygInformation"
        Me.INDlygInformation.Size = New System.Drawing.Size(414, 573)
        Me.INDlygInformation.Text = "Información"
        '
        'INDLciAdquisitionType
        '
        Me.INDLciAdquisitionType.AllowHide = False
        Me.INDLciAdquisitionType.Control = Me.INDSleAdquisitionType
        Me.INDLciAdquisitionType.CustomizationFormText = "Almacén"
        Me.INDLciAdquisitionType.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAdquisitionType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciAdquisitionType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciAdquisitionType.Name = "INDLciAdquisitionType"
        Me.INDLciAdquisitionType.ShowInCustomizationForm = False
        Me.INDLciAdquisitionType.Size = New System.Drawing.Size(390, 60)
        Me.INDLciAdquisitionType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdquisitionType.Text = "Tipo de Adquisición"
        Me.INDLciAdquisitionType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdquisitionType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAdquisitionType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAdquisitionType.TextToControlDistance = 5
        '
        'INDLciReferal
        '
        Me.INDLciReferal.AllowHide = False
        Me.INDLciReferal.Control = Me.INDTxtReferalNumber
        Me.INDLciReferal.CustomizationFormText = "Nº Remision"
        Me.INDLciReferal.Location = New System.Drawing.Point(0, 60)
        Me.INDLciReferal.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciReferal.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciReferal.Name = "INDLciReferal"
        Me.INDLciReferal.ShowInCustomizationForm = False
        Me.INDLciReferal.Size = New System.Drawing.Size(390, 60)
        Me.INDLciReferal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReferal.Text = "Nº Remisión"
        Me.INDLciReferal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciReferal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReferal.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciReferal.TextToControlDistance = 5
        '
        'INDLciLocationResponsible
        '
        Me.INDLciLocationResponsible.Control = Me.INDSlLocationResponsible
        Me.INDLciLocationResponsible.Location = New System.Drawing.Point(0, 120)
        Me.INDLciLocationResponsible.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciLocationResponsible.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciLocationResponsible.Name = "INDLciLocationResponsible"
        Me.INDLciLocationResponsible.Size = New System.Drawing.Size(390, 60)
        Me.INDLciLocationResponsible.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciLocationResponsible.Text = "Ubicación y Responsable"
        Me.INDLciLocationResponsible.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciLocationResponsible.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciLocationResponsible.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciLocationResponsible.TextToControlDistance = 5
        '
        'INDLciLocation
        '
        Me.INDLciLocation.Control = Me.INDtreeLocation
        Me.INDLciLocation.Location = New System.Drawing.Point(0, 180)
        Me.INDLciLocation.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciLocation.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciLocation.Name = "INDLciLocation"
        Me.INDLciLocation.Size = New System.Drawing.Size(390, 60)
        Me.INDLciLocation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciLocation.Text = "Ubicación"
        Me.INDLciLocation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciLocation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciLocation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciLocation.TextToControlDistance = 5
        Me.INDLciLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciResponsible
        '
        Me.INDLciResponsible.Control = Me.INDSlResponsible
        Me.INDLciResponsible.Location = New System.Drawing.Point(0, 240)
        Me.INDLciResponsible.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciResponsible.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciResponsible.Name = "INDLciResponsible"
        Me.INDLciResponsible.Size = New System.Drawing.Size(390, 274)
        Me.INDLciResponsible.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciResponsible.Text = "Responsable"
        Me.INDLciResponsible.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciResponsible.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciResponsible.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciResponsible.TextToControlDistance = 5
        Me.INDLciResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlygItem
        '
        Me.INDlygItem.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygItem.AppearanceGroup.Options.UseFont = True
        Me.INDlygItem.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygItem.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygItem.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygItem.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygItem, False)
        Me.INDlygItem.CustomizationFormText = "Listado de Equipos"
        Me.INDlygItem.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemReferralEntry, Me.LayoutControlItem2})
        Me.INDlygItem.Location = New System.Drawing.Point(828, 0)
        Me.INDlygItem.Name = "INDlygItem"
        Me.INDlygItem.Size = New System.Drawing.Size(852, 573)
        Me.INDlygItem.Text = "Artículos"
        '
        'INDlyItemReferralEntry
        '
        Me.INDlyItemReferralEntry.Control = Me.INDGcReferralEntry
        Me.INDlyItemReferralEntry.CustomizationFormText = "Productos"
        Me.INDlyItemReferralEntry.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemReferralEntry.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemReferralEntry.MinSize = New System.Drawing.Size(828, 1)
        Me.INDlyItemReferralEntry.Name = "INDlyItemReferralEntry"
        Me.INDlyItemReferralEntry.Size = New System.Drawing.Size(828, 478)
        Me.INDlyItemReferralEntry.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemReferralEntry.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemReferralEntry.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemReferralEntry.TextToControlDistance = 0
        Me.INDlyItemReferralEntry.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDBtnAdd
        Me.LayoutControlItem2.CustomizationFormText = "Agregar Artículo"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmFixedAssetRemissionEntrance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1362, 741)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetRemissionEntrance"
        Me.Opacity = 1.0R
        Me.Tag = "1696"
        Me.Text = "Remisión de Entrada"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcReferralEntry.ResumeLayout(False)
        CType(Me.INDSlResponsible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtreeLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeListLookUpEdit1TreeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlLocationResponsible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSupplierDistributionLine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSleSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptPceBatchSerial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIcbRemissionSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleAdquisitionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGdvWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtReferalNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygReferralEntry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSupplierDistributionLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdquisitionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReferal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLocationResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygItem, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcReferralEntry As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlygReferralEntry As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDGcReferralEntry As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvReferralEntry As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleAdquisitionType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGdvWareHouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTxtReferalNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDDteDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciReferal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAdquisitionType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemReferralEntry As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlygItem As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDcolEquipment As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptPceBatchSerial As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDRptIcbRemissionSource As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDcolSubtotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolTrademark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDSlLocationResponsible As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciLocationResponsible As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlResponsible As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtreeLocation As DevExpress.XtraEditors.TreeListLookUpEdit
    Friend WithEvents TreeListLookUpEdit1TreeList As DevExpress.XtraTreeList.TreeList
    Friend WithEvents INDLciLocation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciResponsible As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoTreeList1 As Presentation.Controls.IndigoTreeList
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDlygInformation As DevExpress.XtraLayout.LayoutControlGroup
End Class
