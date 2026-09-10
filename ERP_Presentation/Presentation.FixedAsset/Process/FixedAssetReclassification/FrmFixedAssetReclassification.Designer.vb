Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFixedAssetReclassification
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetReclassification))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyReclassification = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleCatalog = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvCatalog = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolCatalogCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCatalogDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleItem = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvItem = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolItemCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolItemDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCatalogPrevious = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvCatalogPrevious = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolCatalogPreviousCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCatalogPreviousDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleItemPrevious = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvItemPrevious = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolItemPreviousCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolItemPreviousDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleReclassificationType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvReclassificationType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolReclassificationType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmemoDetail = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemReclassificationType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygReclassificationData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemItemPrevious = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCatalogPrevious = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCatalog = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyReclassification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyReclassification.SuspendLayout()
        CType(Me.INDSleCatalog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvCatalog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCatalogPrevious.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvCatalogPrevious, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleItemPrevious.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvItemPrevious, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleReclassificationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvReclassificationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemReclassificationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygReclassificationData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemItemPrevious, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCatalogPrevious, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCatalog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyReclassification)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1262, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1262, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1262, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyReclassification
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyReclassification
        '
        Me.INDlyReclassification.Controls.Add(Me.INDSleCatalog)
        Me.INDlyReclassification.Controls.Add(Me.INDSleItem)
        Me.INDlyReclassification.Controls.Add(Me.INDSleCatalogPrevious)
        Me.INDlyReclassification.Controls.Add(Me.INDSleItemPrevious)
        Me.INDlyReclassification.Controls.Add(Me.INDGleReclassificationType)
        Me.INDlyReclassification.Controls.Add(Me.INDmemoDetail)
        Me.INDlyReclassification.Controls.Add(Me.INDdteDocumentDate)
        Me.INDlyReclassification.Controls.Add(Me.INDbtnCode)
        Me.INDlyReclassification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyReclassification.Location = New System.Drawing.Point(202, 7)
        Me.INDlyReclassification.Name = "INDlyReclassification"
        Me.INDlyReclassification.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(126, 353, 402, 428)
        Me.INDlyReclassification.Root = Me.LayoutControlGroup1
        Me.INDlyReclassification.Size = New System.Drawing.Size(1058, 557)
        Me.INDlyReclassification.TabIndex = 2
        Me.INDlyReclassification.Text = "LayoutControl1"
        '
        'INDSleCatalog
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCatalog, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCatalog, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCatalog, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCatalog, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleCatalog, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCatalog, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCatalog, False)
        Me.INDSleCatalog.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCatalog, False)
        Me.INDSleCatalog.Location = New System.Drawing.Point(438, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCatalog, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCatalog.Name = "INDSleCatalog"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCatalog, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCatalog, False)
        Me.INDSleCatalog.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleCatalog.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCatalog.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCatalog.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCatalog.Properties.Appearance.Options.UseFont = True
        Me.INDSleCatalog.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCatalog.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCatalog.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleCatalog.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleCatalog.Properties.DisplayMember = "CodeDescription"
        Me.INDSleCatalog.Properties.NullText = ""
        Me.INDSleCatalog.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDSleCatalog.Properties.PopupSizeable = False
        Me.INDSleCatalog.Properties.PopupView = Me.INDgvCatalog
        Me.INDSleCatalog.Properties.ShowClearButton = False
        Me.INDSleCatalog.Properties.ShowFooter = False
        Me.INDSleCatalog.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCatalog, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCatalog, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCatalog, True)
        Me.INDSleCatalog.Size = New System.Drawing.Size(386, 28)
        Me.INDSleCatalog.StyleController = Me.INDlyReclassification
        Me.INDSleCatalog.TabIndex = 15
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCatalog, "1697")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCatalog, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCatalog, "{0} - {1}")
        Me.INDSleCatalog.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCatalog, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCatalog, False)
        '
        'INDgvCatalog
        '
        Me.INDgvCatalog.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvCatalog.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvCatalog.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvCatalog.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvCatalog.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvCatalog.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvCatalog.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvCatalog.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvCatalog.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvCatalog.Appearance.Row.Options.UseFont = True
        Me.INDgvCatalog.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolCatalogCode, Me.INDcolCatalogDescription})
        Me.INDgvCatalog.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvCatalog.Name = "INDgvCatalog"
        Me.INDgvCatalog.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvCatalog.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvCatalog.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvCatalog.OptionsView.ShowAutoFilterRow = True
        Me.INDgvCatalog.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvCatalog, False)
        '
        'INDcolCatalogCode
        '
        Me.INDcolCatalogCode.Caption = "Código"
        Me.INDcolCatalogCode.FieldName = "Code"
        Me.INDcolCatalogCode.Name = "INDcolCatalogCode"
        Me.INDcolCatalogCode.Visible = True
        Me.INDcolCatalogCode.VisibleIndex = 0
        '
        'INDcolCatalogDescription
        '
        Me.INDcolCatalogDescription.Caption = "Descripción"
        Me.INDcolCatalogDescription.FieldName = "Description"
        Me.INDcolCatalogDescription.Name = "INDcolCatalogDescription"
        Me.INDcolCatalogDescription.Visible = True
        Me.INDcolCatalogDescription.VisibleIndex = 1
        '
        'INDSleItem
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleItem, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleItem, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleItem, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleItem, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleItem, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleItem, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleItem, False)
        Me.INDSleItem.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleItem, False)
        Me.INDSleItem.Location = New System.Drawing.Point(438, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleItem, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleItem.Name = "INDSleItem"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleItem, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleItem, False)
        Me.INDSleItem.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleItem.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleItem.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleItem.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleItem.Properties.Appearance.Options.UseFont = True
        Me.INDSleItem.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleItem.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleItem.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleItem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleItem.Properties.DisplayMember = "CodeDescription"
        Me.INDSleItem.Properties.NullText = ""
        Me.INDSleItem.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDSleItem.Properties.PopupSizeable = False
        Me.INDSleItem.Properties.PopupView = Me.INDgvItem
        Me.INDSleItem.Properties.ShowClearButton = False
        Me.INDSleItem.Properties.ShowFooter = False
        Me.INDSleItem.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleItem, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleItem, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleItem, True)
        Me.INDSleItem.Size = New System.Drawing.Size(386, 28)
        Me.INDSleItem.StyleController = Me.INDlyReclassification
        Me.INDSleItem.TabIndex = 14
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleItem, "572")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleItem, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleItem, "{0} - {1}")
        Me.INDSleItem.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleItem, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleItem, False)
        '
        'INDgvItem
        '
        Me.INDgvItem.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvItem.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvItem.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvItem.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvItem.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvItem.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvItem.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvItem.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvItem.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvItem.Appearance.Row.Options.UseFont = True
        Me.INDgvItem.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolItemCode, Me.INDcolItemDescription})
        Me.INDgvItem.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvItem.Name = "INDgvItem"
        Me.INDgvItem.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvItem.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvItem.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvItem.OptionsView.ShowAutoFilterRow = True
        Me.INDgvItem.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvItem, False)
        '
        'INDcolItemCode
        '
        Me.INDcolItemCode.Caption = "Código"
        Me.INDcolItemCode.FieldName = "Code"
        Me.INDcolItemCode.Name = "INDcolItemCode"
        Me.INDcolItemCode.Visible = True
        Me.INDcolItemCode.VisibleIndex = 0
        '
        'INDcolItemDescription
        '
        Me.INDcolItemDescription.Caption = "Descripción"
        Me.INDcolItemDescription.FieldName = "Description"
        Me.INDcolItemDescription.Name = "INDcolItemDescription"
        Me.INDcolItemDescription.Visible = True
        Me.INDcolItemDescription.VisibleIndex = 1
        '
        'INDSleCatalogPrevious
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCatalogPrevious, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCatalogPrevious, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCatalogPrevious, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleCatalogPrevious, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCatalogPrevious, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.INDSleCatalogPrevious.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.INDSleCatalogPrevious.Location = New System.Drawing.Point(438, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCatalogPrevious, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCatalogPrevious.Name = "INDSleCatalogPrevious"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCatalogPrevious, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.INDSleCatalogPrevious.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleCatalogPrevious.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCatalogPrevious.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCatalogPrevious.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCatalogPrevious.Properties.Appearance.Options.UseFont = True
        Me.INDSleCatalogPrevious.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCatalogPrevious.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCatalogPrevious.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleCatalogPrevious.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleCatalogPrevious.Properties.DisplayMember = "CodeDescription"
        Me.INDSleCatalogPrevious.Properties.NullText = ""
        Me.INDSleCatalogPrevious.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDSleCatalogPrevious.Properties.PopupSizeable = False
        Me.INDSleCatalogPrevious.Properties.PopupView = Me.INDgvCatalogPrevious
        Me.INDSleCatalogPrevious.Properties.ReadOnly = True
        Me.INDSleCatalogPrevious.Properties.ShowClearButton = False
        Me.INDSleCatalogPrevious.Properties.ShowFooter = False
        Me.INDSleCatalogPrevious.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCatalogPrevious, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCatalogPrevious, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCatalogPrevious, True)
        Me.INDSleCatalogPrevious.Size = New System.Drawing.Size(386, 28)
        Me.INDSleCatalogPrevious.StyleController = Me.INDlyReclassification
        Me.INDSleCatalogPrevious.TabIndex = 13
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCatalogPrevious, "1697")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCatalogPrevious, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCatalogPrevious, "{0} - {1}")
        Me.INDSleCatalogPrevious.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCatalogPrevious, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCatalogPrevious, False)
        '
        'INDgvCatalogPrevious
        '
        Me.INDgvCatalogPrevious.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvCatalogPrevious.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvCatalogPrevious.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvCatalogPrevious.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvCatalogPrevious.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvCatalogPrevious.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvCatalogPrevious.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvCatalogPrevious.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvCatalogPrevious.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvCatalogPrevious.Appearance.Row.Options.UseFont = True
        Me.INDgvCatalogPrevious.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolCatalogPreviousCode, Me.INDcolCatalogPreviousDescription})
        Me.INDgvCatalogPrevious.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvCatalogPrevious.Name = "INDgvCatalogPrevious"
        Me.INDgvCatalogPrevious.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvCatalogPrevious.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvCatalogPrevious.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvCatalogPrevious.OptionsView.ShowAutoFilterRow = True
        Me.INDgvCatalogPrevious.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvCatalogPrevious, False)
        '
        'INDcolCatalogPreviousCode
        '
        Me.INDcolCatalogPreviousCode.Caption = "Código"
        Me.INDcolCatalogPreviousCode.FieldName = "Code"
        Me.INDcolCatalogPreviousCode.Name = "INDcolCatalogPreviousCode"
        Me.INDcolCatalogPreviousCode.Visible = True
        Me.INDcolCatalogPreviousCode.VisibleIndex = 0
        '
        'INDcolCatalogPreviousDescription
        '
        Me.INDcolCatalogPreviousDescription.Caption = "Descripción"
        Me.INDcolCatalogPreviousDescription.FieldName = "Description"
        Me.INDcolCatalogPreviousDescription.Name = "INDcolCatalogPreviousDescription"
        Me.INDcolCatalogPreviousDescription.Visible = True
        Me.INDcolCatalogPreviousDescription.VisibleIndex = 1
        '
        'INDSleItemPrevious
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleItemPrevious, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleItemPrevious, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleItemPrevious, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleItemPrevious, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleItemPrevious, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleItemPrevious, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleItemPrevious, False)
        Me.INDSleItemPrevious.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleItemPrevious, False)
        Me.INDSleItemPrevious.Location = New System.Drawing.Point(438, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleItemPrevious, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleItemPrevious.Name = "INDSleItemPrevious"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleItemPrevious, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleItemPrevious, False)
        Me.INDSleItemPrevious.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleItemPrevious.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleItemPrevious.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleItemPrevious.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleItemPrevious.Properties.Appearance.Options.UseFont = True
        Me.INDSleItemPrevious.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleItemPrevious.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleItemPrevious.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleItemPrevious.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleItemPrevious.Properties.DisplayMember = "CodeDescription"
        Me.INDSleItemPrevious.Properties.NullText = ""
        Me.INDSleItemPrevious.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDSleItemPrevious.Properties.PopupSizeable = False
        Me.INDSleItemPrevious.Properties.PopupView = Me.INDgvItemPrevious
        Me.INDSleItemPrevious.Properties.ShowClearButton = False
        Me.INDSleItemPrevious.Properties.ShowFooter = False
        Me.INDSleItemPrevious.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleItemPrevious, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleItemPrevious, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleItemPrevious, True)
        Me.INDSleItemPrevious.Size = New System.Drawing.Size(386, 28)
        Me.INDSleItemPrevious.StyleController = Me.INDlyReclassification
        Me.INDSleItemPrevious.TabIndex = 12
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleItemPrevious, "572")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleItemPrevious, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleItemPrevious, "{0} - {1}")
        Me.INDSleItemPrevious.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleItemPrevious, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleItemPrevious, False)
        '
        'INDgvItemPrevious
        '
        Me.INDgvItemPrevious.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvItemPrevious.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvItemPrevious.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvItemPrevious.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvItemPrevious.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvItemPrevious.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvItemPrevious.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvItemPrevious.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvItemPrevious.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvItemPrevious.Appearance.Row.Options.UseFont = True
        Me.INDgvItemPrevious.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolItemPreviousCode, Me.INDcolItemPreviousDescription})
        Me.INDgvItemPrevious.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvItemPrevious.Name = "INDgvItemPrevious"
        Me.INDgvItemPrevious.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvItemPrevious.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvItemPrevious.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvItemPrevious.OptionsView.ShowAutoFilterRow = True
        Me.INDgvItemPrevious.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvItemPrevious, False)
        '
        'INDcolItemPreviousCode
        '
        Me.INDcolItemPreviousCode.Caption = "Código"
        Me.INDcolItemPreviousCode.FieldName = "Code"
        Me.INDcolItemPreviousCode.Name = "INDcolItemPreviousCode"
        Me.INDcolItemPreviousCode.Visible = True
        Me.INDcolItemPreviousCode.VisibleIndex = 0
        '
        'INDcolItemPreviousDescription
        '
        Me.INDcolItemPreviousDescription.Caption = "Descripción"
        Me.INDcolItemPreviousDescription.FieldName = "Description"
        Me.INDcolItemPreviousDescription.Name = "INDcolItemPreviousDescription"
        Me.INDcolItemPreviousDescription.Visible = True
        Me.INDcolItemPreviousDescription.VisibleIndex = 1
        '
        'INDGleReclassificationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleReclassificationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleReclassificationType, True)
        Me.INDGleReclassificationType.EnterMoveNextControl = True
        Me.INDGleReclassificationType.Location = New System.Drawing.Point(24, 198)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleReclassificationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleReclassificationType.Name = "INDGleReclassificationType"
        Me.INDGleReclassificationType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleReclassificationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleReclassificationType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleReclassificationType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleReclassificationType.Properties.Appearance.Options.UseFont = True
        Me.INDGleReclassificationType.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleReclassificationType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleReclassificationType.Properties.DisplayMember = "Item2"
        Me.INDGleReclassificationType.Properties.ImmediatePopup = True
        Me.INDGleReclassificationType.Properties.NullText = ""
        Me.INDGleReclassificationType.Properties.PopupView = Me.INDGvReclassificationType
        Me.INDGleReclassificationType.Properties.ValueMember = "Item1"
        Me.INDGleReclassificationType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleReclassificationType.StyleController = Me.INDlyReclassification
        Me.INDGleReclassificationType.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleReclassificationType, 0)
        Me.INDGleReclassificationType.ToolTip = "Este Campo es Necesario"
        '
        'INDGvReclassificationType
        '
        Me.INDGvReclassificationType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvReclassificationType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvReclassificationType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvReclassificationType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvReclassificationType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvReclassificationType.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvReclassificationType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvReclassificationType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvReclassificationType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvReclassificationType.Appearance.Row.Options.UseFont = True
        Me.INDGvReclassificationType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolReclassificationType})
        Me.INDGvReclassificationType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvReclassificationType.Name = "INDGvReclassificationType"
        Me.INDGvReclassificationType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvReclassificationType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvReclassificationType.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvReclassificationType.OptionsView.ShowAutoFilterRow = True
        Me.INDGvReclassificationType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvReclassificationType, False)
        '
        'INDcolReclassificationType
        '
        Me.INDcolReclassificationType.Caption = "Tipo de Reclasificación"
        Me.INDcolReclassificationType.FieldName = "Item2"
        Me.INDcolReclassificationType.Name = "INDcolReclassificationType"
        Me.INDcolReclassificationType.Visible = True
        Me.INDcolReclassificationType.VisibleIndex = 0
        '
        'INDmemoDetail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoDetail, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoDetail, False)
        Me.INDmemoDetail.EnterMoveNextControl = True
        Me.INDmemoDetail.Location = New System.Drawing.Point(24, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoDetail, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoDetail.Name = "INDmemoDetail"
        Me.INDmemoDetail.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoDetail.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoDetail.Properties.Appearance.Options.UseFont = True
        Me.INDmemoDetail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmemoDetail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmemoDetail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoDetail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmemoDetail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmemoDetail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoDetail.Properties.MaxLength = 300
        Me.INDmemoDetail.Size = New System.Drawing.Size(386, 70)
        Me.INDmemoDetail.StyleController = Me.INDlyReclassification
        Me.INDmemoDetail.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoDetail, 0)
        '
        'INDdteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteDocumentDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDocumentDate, True)
        Me.INDdteDocumentDate.EditValue = Nothing
        Me.INDdteDocumentDate.EnterMoveNextControl = True
        Me.INDdteDocumentDate.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteDocumentDate.Name = "INDdteDocumentDate"
        Me.INDdteDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdteDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDocumentDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdteDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteDocumentDate.StyleController = Me.INDlyReclassification
        Me.INDdteDocumentDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDocumentDate, 0)
        Me.INDdteDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlyReclassification
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygReclassificationData, Me.INDlygDetails})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1058, 557)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygPrincipalData
        '
        Me.INDlygPrincipalData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalData, False)
        Me.INDlygPrincipalData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemDocumentDate, Me.INDlyItemDetail, Me.INDlyItemReclassificationType})
        Me.INDlygPrincipalData.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalData.Name = "INDlygPrincipalData"
        Me.INDlygPrincipalData.Size = New System.Drawing.Size(414, 537)
        Me.INDlygPrincipalData.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemDocumentDate
        '
        Me.INDlyItemDocumentDate.Control = Me.INDdteDocumentDate
        Me.INDlyItemDocumentDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemDocumentDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.Name = "INDlyItemDocumentDate"
        Me.INDlyItemDocumentDate.ShowInCustomizationForm = False
        Me.INDlyItemDocumentDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDocumentDate.Text = "Fecha Documento"
        Me.INDlyItemDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDocumentDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDocumentDate.TextToControlDistance = 5
        '
        'INDlyItemDetail
        '
        Me.INDlyItemDetail.Control = Me.INDmemoDetail
        Me.INDlyItemDetail.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemDetail.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemDetail.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlyItemDetail.Name = "INDlyItemDetail"
        Me.INDlyItemDetail.Size = New System.Drawing.Size(390, 304)
        Me.INDlyItemDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDetail.Text = "Detalle"
        Me.INDlyItemDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDetail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDetail.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDetail.TextToControlDistance = 5
        '
        'INDlyItemReclassificationType
        '
        Me.INDlyItemReclassificationType.AllowHide = False
        Me.INDlyItemReclassificationType.Control = Me.INDGleReclassificationType
        Me.INDlyItemReclassificationType.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemReclassificationType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemReclassificationType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemReclassificationType.Name = "INDlyItemReclassificationType"
        Me.INDlyItemReclassificationType.ShowInCustomizationForm = False
        Me.INDlyItemReclassificationType.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemReclassificationType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemReclassificationType.Text = "Tipo de Reclasificación"
        Me.INDlyItemReclassificationType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemReclassificationType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemReclassificationType.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlyItemReclassificationType.TextToControlDistance = 5
        '
        'INDlygReclassificationData
        '
        Me.INDlygReclassificationData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygReclassificationData.AppearanceGroup.Options.UseFont = True
        Me.INDlygReclassificationData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygReclassificationData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygReclassificationData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReclassificationData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygReclassificationData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygReclassificationData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygReclassificationData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReclassificationData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygReclassificationData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReclassificationData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygReclassificationData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReclassificationData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygReclassificationData, False)
        Me.INDlygReclassificationData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemItemPrevious, Me.INDlyItemCatalogPrevious, Me.INDlyItemItem, Me.INDlyItemCatalog})
        Me.INDlygReclassificationData.Location = New System.Drawing.Point(414, 0)
        Me.INDlygReclassificationData.Name = "INDlygReclassificationData"
        Me.INDlygReclassificationData.Size = New System.Drawing.Size(414, 537)
        Me.INDlygReclassificationData.Text = "Datos de la Reclasificación"
        '
        'INDlyItemItemPrevious
        '
        Me.INDlyItemItemPrevious.Control = Me.INDSleItemPrevious
        Me.INDlyItemItemPrevious.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemItemPrevious.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemItemPrevious.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemItemPrevious.Name = "INDlyItemItemPrevious"
        Me.INDlyItemItemPrevious.ShowInCustomizationForm = False
        Me.INDlyItemItemPrevious.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemItemPrevious.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemItemPrevious.Text = "Artículo"
        Me.INDlyItemItemPrevious.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemItemPrevious.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemItemPrevious.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemItemPrevious.TextToControlDistance = 5
        '
        'INDlyItemCatalogPrevious
        '
        Me.INDlyItemCatalogPrevious.Control = Me.INDSleCatalogPrevious
        Me.INDlyItemCatalogPrevious.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemCatalogPrevious.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCatalogPrevious.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCatalogPrevious.Name = "INDlyItemCatalogPrevious"
        Me.INDlyItemCatalogPrevious.ShowInCustomizationForm = False
        Me.INDlyItemCatalogPrevious.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCatalogPrevious.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCatalogPrevious.Text = "Catálogo"
        Me.INDlyItemCatalogPrevious.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCatalogPrevious.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCatalogPrevious.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCatalogPrevious.TextToControlDistance = 5
        '
        'INDlyItemItem
        '
        Me.INDlyItemItem.Control = Me.INDSleItem
        Me.INDlyItemItem.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemItem.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemItem.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemItem.Name = "INDlyItemItem"
        Me.INDlyItemItem.ShowInCustomizationForm = False
        Me.INDlyItemItem.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemItem.Text = "Nuevo Artículo"
        Me.INDlyItemItem.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemItem.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemItem.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemItem.TextToControlDistance = 5
        '
        'INDlyItemCatalog
        '
        Me.INDlyItemCatalog.Control = Me.INDSleCatalog
        Me.INDlyItemCatalog.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemCatalog.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCatalog.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCatalog.Name = "INDlyItemCatalog"
        Me.INDlyItemCatalog.ShowInCustomizationForm = False
        Me.INDlyItemCatalog.Size = New System.Drawing.Size(390, 304)
        Me.INDlyItemCatalog.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCatalog.Text = "Nuevo Catálogo"
        Me.INDlyItemCatalog.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCatalog.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCatalog.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCatalog.TextToControlDistance = 5
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
        Me.INDlygDetails.Location = New System.Drawing.Point(828, 0)
        Me.INDlygDetails.Name = "INDlygDetails"
        Me.INDlygDetails.Size = New System.Drawing.Size(210, 537)
        Me.INDlygDetails.Text = "Detalles"
        Me.INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'FrmFixedAssetReclassification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1262, 701)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetReclassification"
        Me.Opacity = 1.0R
        Me.Tag = "1994"
        Me.Text = "Reclasificación de artículos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyReclassification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyReclassification.ResumeLayout(False)
        CType(Me.INDSleCatalog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvCatalog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCatalogPrevious.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvCatalogPrevious, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleItemPrevious.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvItemPrevious, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleReclassificationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvReclassificationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemReclassificationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygReclassificationData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemItemPrevious, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCatalogPrevious, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCatalog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyReclassification As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDmemoDetail As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDGleReclassificationType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvReclassificationType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemReclassificationType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolReclassificationType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleItemPrevious As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvItemPrevious As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygReclassificationData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemItemPrevious As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleCatalog As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvCatalog As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleItem As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvItem As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleCatalogPrevious As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvCatalogPrevious As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemCatalogPrevious As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCatalog As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolItemPreviousCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolItemPreviousDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCatalogPreviousCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCatalogPreviousDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolItemCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolItemDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCatalogCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCatalogDescription As DevExpress.XtraGrid.Columns.GridColumn
End Class
