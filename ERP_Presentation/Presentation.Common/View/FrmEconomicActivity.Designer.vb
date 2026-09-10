Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEconomicActivity
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyEconomicActivity = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleIncomeGenerating = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIncomeGenerating = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyEconomicActivity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyEconomicActivity.SuspendLayout()
        CType(Me.INDSleIncomeGenerating.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIncomeGenerating, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyEconomicActivity)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1072, 490)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1072, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1072, 130)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyEconomicActivity
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 481)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyEconomicActivity
        '
        Me.INDlyEconomicActivity.AllowCustomization = False
        Me.INDlyEconomicActivity.Controls.Add(Me.INDSleIncomeGenerating)
        Me.INDlyEconomicActivity.Controls.Add(Me.INDtxtName)
        Me.INDlyEconomicActivity.Controls.Add(Me.INDbtnCode)
        Me.INDlyEconomicActivity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyEconomicActivity, False)
        Me.INDlyEconomicActivity.Location = New System.Drawing.Point(202, 7)
        Me.INDlyEconomicActivity.Name = "INDlyEconomicActivity"
        Me.INDlyEconomicActivity.Root = Me.LayoutControlGroup1
        Me.INDlyEconomicActivity.Size = New System.Drawing.Size(868, 481)
        Me.INDlyEconomicActivity.TabIndex = 1
        Me.INDlyEconomicActivity.Text = "LayoutControl1"
        '
        'INDSleIncomeGenerating
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleIncomeGenerating, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleIncomeGenerating, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleIncomeGenerating, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleIncomeGenerating, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleIncomeGenerating, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.INDSleIncomeGenerating.EditValue = ""
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.INDSleIncomeGenerating.Location = New System.Drawing.Point(24, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleIncomeGenerating, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleIncomeGenerating.MaximumSize = New System.Drawing.Size(390, 60)
        Me.INDSleIncomeGenerating.MinimumSize = New System.Drawing.Size(390, 28)
        Me.INDSleIncomeGenerating.Name = "INDSleIncomeGenerating"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.INDSleIncomeGenerating.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleIncomeGenerating.Properties.Appearance.Options.UseFont = True
        Me.INDSleIncomeGenerating.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleIncomeGenerating.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleIncomeGenerating.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleIncomeGenerating.Properties.DisplayMember = "Item2"
        Me.INDSleIncomeGenerating.Properties.NullText = ""
        Me.INDSleIncomeGenerating.Properties.PopupSizeable = False
        Me.INDSleIncomeGenerating.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleIncomeGenerating.Properties.ShowFooter = False
        Me.INDSleIncomeGenerating.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleIncomeGenerating, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleIncomeGenerating, True)
        Me.INDSleIncomeGenerating.Size = New System.Drawing.Size(390, 28)
        Me.INDSleIncomeGenerating.StyleController = Me.INDlyEconomicActivity
        Me.INDSleIncomeGenerating.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleIncomeGenerating, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleIncomeGenerating, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleIncomeGenerating, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleIncomeGenerating, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleIncomeGenerating, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDescription})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'INDColDescription
        '
        Me.INDColDescription.Caption = "Selección"
        Me.INDColDescription.FieldName = "Item2"
        Me.INDColDescription.Name = "INDColDescription"
        Me.INDColDescription.OptionsColumn.AllowEdit = False
        Me.INDColDescription.Visible = True
        Me.INDColDescription.VisibleIndex = 0
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.MaximumSize = New System.Drawing.Size(390, 0)
        Me.INDtxtName.MinimumSize = New System.Drawing.Size(390, 0)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 200
        Me.INDtxtName.Size = New System.Drawing.Size(390, 28)
        Me.INDtxtName.StyleController = Me.INDlyEconomicActivity
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.MaximumSize = New System.Drawing.Size(390, 0)
        Me.INDbtnCode.MinimumSize = New System.Drawing.Size(390, 0)
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
        EditorButtonImageOptions1.Image = Global.Presentation.Common.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(390, 28)
        Me.INDbtnCode.StyleController = Me.INDlyEconomicActivity
        Me.INDbtnCode.TabIndex = 4
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
        Me.LayoutControlGroup1.CustomizationFormText = "Actividad Económica"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(868, 481)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        Me.INDlygGeneralInformation.CustomizationFormText = "Información Principal"
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDLciIncomeGenerating})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(848, 461)
        Me.INDlygGeneralInformation.Text = "Información Principal"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(824, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.CustomizationFormText = "Nombre"
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(824, 60)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDLciIncomeGenerating
        '
        Me.INDLciIncomeGenerating.Control = Me.INDSleIncomeGenerating
        Me.INDLciIncomeGenerating.CustomizationFormText = "Actividad propia generadora de ingreso"
        Me.INDLciIncomeGenerating.Location = New System.Drawing.Point(0, 120)
        Me.INDLciIncomeGenerating.MaxSize = New System.Drawing.Size(0, 60)
        Me.INDLciIncomeGenerating.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciIncomeGenerating.Name = "INDLciIncomeGenerating"
        Me.INDLciIncomeGenerating.ShowInCustomizationForm = False
        Me.INDLciIncomeGenerating.Size = New System.Drawing.Size(824, 288)
        Me.INDLciIncomeGenerating.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIncomeGenerating.Text = "Actividad propia generadora de ingreso"
        Me.INDLciIncomeGenerating.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciIncomeGenerating.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIncomeGenerating.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciIncomeGenerating.TextToControlDistance = 5
        '
        'FrmEconomicActivity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1072, 625)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmEconomicActivity"
        Me.Opacity = 1.0R
        Me.Tag = "1845"
        Me.Text = "Actividad Económica"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyEconomicActivity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyEconomicActivity.ResumeLayout(False)
        CType(Me.INDSleIncomeGenerating.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIncomeGenerating, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyEconomicActivity As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciIncomeGenerating As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDSleIncomeGenerating As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
End Class
