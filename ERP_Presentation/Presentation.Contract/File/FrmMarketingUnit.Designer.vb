Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMarketingUnit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMarketingUnit))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyMarketingUnit = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcCupsEntity = New DevExpress.XtraGrid.GridControl()
        Me.viewCups = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleCupsEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSearchCups = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygInformationCups = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCupsEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemGridCupsEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyMarketingUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyMarketingUnit.SuspendLayout()
        CType(Me.INDgcCupsEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewCups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCupsEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSearchCups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygInformationCups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCupsEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridCupsEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.INDlyMarketingUnit)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        '
        'ToolBars
        '
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControl1
        '
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyMarketingUnit
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyMarketingUnit
        '
        resources.ApplyResources(Me.INDlyMarketingUnit, "INDlyMarketingUnit")
        Me.INDlyMarketingUnit.AllowCustomization = False
        Me.INDlyMarketingUnit.Controls.Add(Me.INDbtnAdd)
        Me.INDlyMarketingUnit.Controls.Add(Me.INDgcCupsEntity)
        Me.INDlyMarketingUnit.Controls.Add(Me.INDsleCupsEntity)
        Me.INDlyMarketingUnit.Controls.Add(Me.INDtxtName)
        Me.INDlyMarketingUnit.Controls.Add(Me.INDbtnCode)
        Me.LayoutControls.SetIsCustomizable(Me.INDlyMarketingUnit, False)
        Me.INDlyMarketingUnit.Name = "INDlyMarketingUnit"
        Me.INDlyMarketingUnit.Root = Me.LayoutControlGroup1
        '
        'INDbtnAdd
        '
        resources.ApplyResources(Me.INDbtnAdd, "INDbtnAdd")
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.StyleController = Me.INDlyMarketingUnit
        '
        'INDgcCupsEntity
        '
        resources.ApplyResources(Me.INDgcCupsEntity, "INDgcCupsEntity")
        Me.IndigoGridControl1.SetAddActions(Me.INDgcCupsEntity, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcCupsEntity, Nothing)
        Me.INDgcCupsEntity.EmbeddedNavigator.AccessibleDescription = resources.GetString("INDgcCupsEntity.EmbeddedNavigator.AccessibleDescription")
        Me.INDgcCupsEntity.EmbeddedNavigator.AccessibleName = resources.GetString("INDgcCupsEntity.EmbeddedNavigator.AccessibleName")
        Me.INDgcCupsEntity.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.INDgcCupsEntity.EmbeddedNavigator.Anchor = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.INDgcCupsEntity.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.INDgcCupsEntity.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.INDgcCupsEntity.EmbeddedNavigator.ImeMode = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.INDgcCupsEntity.EmbeddedNavigator.Margin = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.Margin"), System.Windows.Forms.Padding)
        Me.INDgcCupsEntity.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.INDgcCupsEntity.EmbeddedNavigator.TextLocation = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.INDgcCupsEntity.EmbeddedNavigator.ToolTip = resources.GetString("INDgcCupsEntity.EmbeddedNavigator.ToolTip")
        Me.INDgcCupsEntity.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("INDgcCupsEntity.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.INDgcCupsEntity.EmbeddedNavigator.ToolTipTitle = resources.GetString("INDgcCupsEntity.EmbeddedNavigator.ToolTipTitle")
        Me.IndigoGridControl1.SetExportButton(Me.INDgcCupsEntity, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcCupsEntity, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcCupsEntity, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcCupsEntity, False)
        Me.INDgcCupsEntity.MainView = Me.viewCups
        Me.INDgcCupsEntity.Name = "INDgcCupsEntity"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcCupsEntity, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcCupsEntity.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewCups})
        '
        'viewCups
        '
        Me.viewCups.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewCups.Appearance.FocusedRow.Font = CType(resources.GetObject("viewCups.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.viewCups.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewCups.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewCups.Appearance.FocusedRow.Options.UseFont = True
        Me.viewCups.Appearance.GroupRow.Font = CType(resources.GetObject("viewCups.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.viewCups.Appearance.GroupRow.Options.UseFont = True
        Me.viewCups.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewCups.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewCups.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewCups.Appearance.Row.Font = CType(resources.GetObject("viewCups.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewCups.Appearance.Row.Options.UseFont = True
        Me.viewCups.Appearance.ViewCaption.Font = CType(resources.GetObject("viewCups.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.viewCups.Appearance.ViewCaption.Options.UseFont = True
        resources.ApplyResources(Me.viewCups, "viewCups")
        Me.viewCups.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.viewCups.DetailHeight = 431
        Me.viewCups.GridControl = Me.INDgcCupsEntity
        Me.viewCups.Name = "viewCups"
        Me.viewCups.OptionsView.EnableAppearanceEvenRow = True
        Me.viewCups.OptionsView.EnableAppearanceOddRow = True
        Me.viewCups.OptionsView.ShowAutoFilterRow = True
        Me.viewCups.OptionsView.ShowDetailButtons = False
        Me.viewCups.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewCups, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "CupsEntityCode"
        Me.GridColumn1.MinWidth = 23
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "CupsEntityDescription"
        Me.GridColumn2.MinWidth = 23
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        '
        'INDsleCupsEntity
        '
        resources.ApplyResources(Me.INDsleCupsEntity, "INDsleCupsEntity")
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCupsEntity, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCupsEntity, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCupsEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCupsEntity, False)
        Me.INDsleCupsEntity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCupsEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCupsEntity.Name = "INDsleCupsEntity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCupsEntity, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCupsEntity, False)
        Me.INDsleCupsEntity.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleCupsEntity.Properties.Appearance.Font = CType(resources.GetObject("INDsleCupsEntity.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDsleCupsEntity.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCupsEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCupsEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleCupsEntity.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCupsEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCupsEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCupsEntity.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDsleCupsEntity.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDsleCupsEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCupsEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCupsEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCupsEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDsleCupsEntity.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDsleCupsEntity.Properties.DisplayMember = "CodeDescription"
        Me.INDsleCupsEntity.Properties.NullText = resources.GetString("INDsleCupsEntity.Properties.NullText")
        Me.INDsleCupsEntity.Properties.PopupSizeable = False
        Me.INDsleCupsEntity.Properties.PopupView = Me.viewSearchCups
        Me.INDsleCupsEntity.Properties.ShowFooter = False
        Me.INDsleCupsEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCupsEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCupsEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCupsEntity, True)
        Me.INDsleCupsEntity.StyleController = Me.INDlyMarketingUnit
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCupsEntity, "970")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCupsEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCupsEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCupsEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCupsEntity, False)
        '
        'viewSearchCups
        '
        Me.viewSearchCups.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSearchCups.Appearance.FocusedRow.Font = CType(resources.GetObject("viewSearchCups.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.viewSearchCups.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSearchCups.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSearchCups.Appearance.GroupRow.Font = CType(resources.GetObject("viewSearchCups.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.viewSearchCups.Appearance.GroupRow.Options.UseFont = True
        Me.viewSearchCups.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewSearchCups.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewSearchCups.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSearchCups.Appearance.Row.Font = CType(resources.GetObject("viewSearchCups.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewSearchCups.Appearance.Row.Options.UseFont = True
        resources.ApplyResources(Me.viewSearchCups, "viewSearchCups")
        Me.viewSearchCups.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.viewSearchCups.DetailHeight = 431
        Me.viewSearchCups.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewSearchCups.Name = "viewSearchCups"
        Me.viewSearchCups.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSearchCups.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSearchCups.OptionsView.EnableAppearanceOddRow = True
        Me.viewSearchCups.OptionsView.ShowAutoFilterRow = True
        Me.viewSearchCups.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSearchCups, False)
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.MinWidth = 23
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Description"
        Me.GridColumn4.MinWidth = 23
        Me.GridColumn4.Name = "GridColumn4"
        '
        'INDtxtName
        '
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.StyleController = Me.INDlyMarketingUnit
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbtnCode
        '
        resources.ApplyResources(Me.INDbtnCode, "INDbtnCode")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = CType(resources.GetObject("INDbtnCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbtnCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Contract.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbtnCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbtnCode.Properties.Buttons1"), CType(resources.GetObject("INDbtnCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbtnCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbtnCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbtnCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDbtnCode.Properties.Buttons6"), CType(resources.GetObject("INDbtnCode.Properties.Buttons7"), Object), CType(resources.GetObject("INDbtnCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbtnCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.StyleController = Me.INDlyMarketingUnit
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation, Me.INDlygInformationCups})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1318, 537)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = CType(resources.GetObject("INDlygGeneralInformation.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = CType(resources.GetObject("INDlygGeneralInformation.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlygGeneralInformation.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlygGeneralInformation.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        resources.ApplyResources(Me.INDlygGeneralInformation, "INDlygGeneralInformation")
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(479, 517)
        '
        'INDlyItemCode
        '
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(455, 44)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(455, 44)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(455, 44)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 44)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(455, 44)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(455, 44)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(455, 420)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlygInformationCups
        '
        Me.INDlygInformationCups.AppearanceGroup.Font = CType(resources.GetObject("INDlygInformationCups.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlygInformationCups.AppearanceGroup.Options.UseFont = True
        Me.INDlygInformationCups.AppearanceItemCaption.Font = CType(resources.GetObject("INDlygInformationCups.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlygInformationCups.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygInformationCups.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlygInformationCups.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlygInformationCups.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygInformationCups.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlygInformationCups.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlygInformationCups.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygInformationCups.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlygInformationCups.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlygInformationCups.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygInformationCups.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlygInformationCups.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlygInformationCups.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygInformationCups.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlygInformationCups.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlygInformationCups.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygInformationCups, False)
        resources.ApplyResources(Me.INDlygInformationCups, "INDlygInformationCups")
        Me.INDlygInformationCups.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCupsEntity, Me.INDlyItemGridCupsEntity, Me.INDlyItemAdd})
        Me.INDlygInformationCups.Location = New System.Drawing.Point(479, 0)
        Me.INDlygInformationCups.Name = "INDlygInformationCups"
        Me.INDlygInformationCups.Size = New System.Drawing.Size(819, 517)
        '
        'INDlyItemCupsEntity
        '
        resources.ApplyResources(Me.INDlyItemCupsEntity, "INDlyItemCupsEntity")
        Me.INDlyItemCupsEntity.Control = Me.INDsleCupsEntity
        Me.INDlyItemCupsEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCupsEntity.MaxSize = New System.Drawing.Size(600, 44)
        Me.INDlyItemCupsEntity.MinSize = New System.Drawing.Size(550, 44)
        Me.INDlyItemCupsEntity.Name = "INDlyItemCupsEntity"
        Me.INDlyItemCupsEntity.Size = New System.Drawing.Size(550, 44)
        Me.INDlyItemCupsEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCupsEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDlyItemCupsEntity.TextSize = New System.Drawing.Size(201, 17)
        Me.INDlyItemCupsEntity.TextToControlDistance = 0
        '
        'INDlyItemGridCupsEntity
        '
        resources.ApplyResources(Me.INDlyItemGridCupsEntity, "INDlyItemGridCupsEntity")
        Me.INDlyItemGridCupsEntity.Control = Me.INDgcCupsEntity
        Me.INDlyItemGridCupsEntity.Location = New System.Drawing.Point(0, 44)
        Me.INDlyItemGridCupsEntity.MaxSize = New System.Drawing.Size(700, 0)
        Me.INDlyItemGridCupsEntity.MinSize = New System.Drawing.Size(700, 30)
        Me.INDlyItemGridCupsEntity.Name = "INDlyItemGridCupsEntity"
        Me.INDlyItemGridCupsEntity.Size = New System.Drawing.Size(795, 420)
        Me.INDlyItemGridCupsEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridCupsEntity.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridCupsEntity.TextVisible = False
        '
        'INDlyItemAdd
        '
        resources.ApplyResources(Me.INDlyItemAdd, "INDlyItemAdd")
        Me.INDlyItemAdd.Control = Me.INDbtnAdd
        Me.INDlyItemAdd.Location = New System.Drawing.Point(550, 0)
        Me.INDlyItemAdd.MaxSize = New System.Drawing.Size(245, 39)
        Me.INDlyItemAdd.MinSize = New System.Drawing.Size(245, 39)
        Me.INDlyItemAdd.Name = "INDlyItemAdd"
        Me.INDlyItemAdd.Size = New System.Drawing.Size(245, 44)
        Me.INDlyItemAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAdd.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmMarketingUnit
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMarketingUnit"
        Me.Opacity = 1.0R
        Me.Tag = "971"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyMarketingUnit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyMarketingUnit.ResumeLayout(False)
        CType(Me.INDgcCupsEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewCups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCupsEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSearchCups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygInformationCups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCupsEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridCupsEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyMarketingUnit As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcCupsEntity As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewCups As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleCupsEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSearchCups As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygInformationCups As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCupsEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemGridCupsEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
