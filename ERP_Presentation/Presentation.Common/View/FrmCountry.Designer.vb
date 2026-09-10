#Region "Imports"
Imports Presentation.Controls
#End Region

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCountry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCountry))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDlyCountry = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtNationality = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleStandardCode = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGcNumeric = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcAlpha = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrCountry = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemNationality = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemStandardCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDCncCurrency = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCountry.SuspendLayout()
        CType(Me.INDtxtNationality.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleStandardCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNationality, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStandardCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCountry)
        Me.INDPanelControlBase.Controls.Add(Me.INDCncCurrency)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDlyCountry
        '
        Me.INDlyCountry.Controls.Add(Me.INDtxtNationality)
        Me.INDlyCountry.Controls.Add(Me.INDbteCode)
        Me.INDlyCountry.Controls.Add(Me.INDtxtName)
        Me.INDlyCountry.Controls.Add(Me.INDSleStandardCode)
        resources.ApplyResources(Me.INDlyCountry, "INDlyCountry")
        Me.INDlyCountry.Name = "INDlyCountry"
        Me.INDlyCountry.Root = Me.LayoutControlGroup1
        '
        'INDtxtNationality
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtNationality, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtNationality, True)
        Me.INDtxtNationality.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtNationality, "INDtxtNationality")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtNationality, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtNationality.Name = "INDtxtNationality"
        Me.INDtxtNationality.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtNationality.Properties.Appearance.Font = CType(resources.GetObject("INDtxtNationality.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtNationality.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtNationality.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtNationality.Properties.Appearance.Options.UseFont = True
        Me.INDtxtNationality.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtNationality.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtNationality.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtNationality.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtNationality.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtNationality.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtNationality.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtNationality.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtNationality.Properties.MaxLength = 50
        Me.INDtxtNationality.StyleController = Me.INDlyCountry
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtNationality, 0)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = CType(resources.GetObject("INDbteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Common.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDbteCode.Properties.Buttons6"), CType(resources.GetObject("INDbteCode.Properties.Buttons7"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDbteCode.Properties.MaxLength = 2
        Me.INDbteCode.StyleController = Me.INDlyCountry
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
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
        Me.INDtxtName.Properties.MaxLength = 80
        Me.INDtxtName.StyleController = Me.INDlyCountry
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDSleStandardCode
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleStandardCode, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleStandardCode, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleStandardCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleStandardCode, False)
        resources.ApplyResources(Me.INDSleStandardCode, "INDSleStandardCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDSleStandardCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleStandardCode.Name = "INDSleStandardCode"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleStandardCode, False)
        Me.INDSleStandardCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleStandardCode.Properties.Appearance.Font = CType(resources.GetObject("INDSleStandardCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleStandardCode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleStandardCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleStandardCode.Properties.Appearance.Options.UseFont = True
        Me.INDSleStandardCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleStandardCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDSleStandardCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDSleStandardCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleStandardCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSleStandardCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDSleStandardCode.Properties.DisplayMember = "Item2"
        Me.INDSleStandardCode.Properties.MaxLength = 3
        Me.INDSleStandardCode.Properties.NullText = resources.GetString("INDSleStandardCode.Properties.NullText")
        Me.INDSleStandardCode.Properties.PopupSizeable = False
        Me.INDSleStandardCode.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleStandardCode.Properties.ShowFooter = False
        Me.INDSleStandardCode.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleStandardCode, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleStandardCode, True)
        Me.INDSleStandardCode.StyleController = Me.INDlyCountry
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleStandardCode, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleStandardCode, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleStandardCode, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleStandardCode, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleStandardCode, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcNumeric, Me.INDGcAlpha})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'INDGcNumeric
        '
        resources.ApplyResources(Me.INDGcNumeric, "INDGcNumeric")
        Me.INDGcNumeric.FieldName = "Item1"
        Me.INDGcNumeric.Name = "INDGcNumeric"
        Me.INDGcNumeric.OptionsColumn.AllowEdit = False
        '
        'INDGcAlpha
        '
        resources.ApplyResources(Me.INDGcAlpha, "INDGcAlpha")
        Me.INDGcAlpha.FieldName = "Item2"
        Me.INDGcAlpha.Name = "INDGcAlpha"
        Me.INDGcAlpha.OptionsColumn.AllowEdit = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrCountry})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1161, 362)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrCountry
        '
        resources.ApplyResources(Me.INDlyGrCountry, "INDlyGrCountry")
        Me.INDlyGrCountry.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemName, Me.INDlyItemCode, Me.INDlyItemNationality, Me.INDlyItemStandardCode})
        Me.INDlyGrCountry.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrCountry.Name = "INDlyGrCountry"
        Me.INDlyGrCountry.Size = New System.Drawing.Size(1141, 342)
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(1117, 60)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(1117, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemNationality
        '
        Me.INDlyItemNationality.AllowHide = False
        Me.INDlyItemNationality.Control = Me.INDtxtNationality
        resources.ApplyResources(Me.INDlyItemNationality, "INDlyItemNationality")
        Me.INDlyItemNationality.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemNationality.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemNationality.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemNationality.Name = "INDlyItemNationality"
        Me.INDlyItemNationality.Size = New System.Drawing.Size(1117, 60)
        Me.INDlyItemNationality.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemNationality.Tag = "Nationality"
        Me.INDlyItemNationality.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemNationality.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemNationality.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemNationality.TextToControlDistance = 5
        '
        'INDlyItemStandardCode
        '
        Me.INDlyItemStandardCode.Control = Me.INDSleStandardCode
        Me.INDlyItemStandardCode.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemStandardCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemStandardCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemStandardCode.Name = "INDlyItemStandardCode"
        Me.INDlyItemStandardCode.ShowInCustomizationForm = False
        Me.INDlyItemStandardCode.Size = New System.Drawing.Size(1117, 109)
        Me.INDlyItemStandardCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDlyItemStandardCode, "INDlyItemStandardCode")
        Me.INDlyItemStandardCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemStandardCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemStandardCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemStandardCode.TextToControlDistance = 5
        '
        'INDCncCurrency
        '
        Me.INDCncCurrency.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.INDCncCurrency, "INDCncCurrency")
        Me.INDCncCurrency.LayoutControl = Me.INDlyCountry
        Me.INDCncCurrency.Name = "INDCncCurrency"
        Me.INDCncCurrency.UseDisabledStatePainter = False
        '
        'FrmCountry
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCountry"
        Me.Opacity = 1.0R
        Me.Tag = "511"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCountry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCountry.ResumeLayout(False)
        CType(Me.INDtxtNationality.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleStandardCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNationality, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStandardCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyCountry As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyGrCountry As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtNationality As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemNationality As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemStandardCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCncCurrency As CtrNavigationControlPanel
    Friend WithEvents INDSleStandardCode As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDGcNumeric As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcAlpha As DevExpress.XtraGrid.Columns.GridColumn
End Class
