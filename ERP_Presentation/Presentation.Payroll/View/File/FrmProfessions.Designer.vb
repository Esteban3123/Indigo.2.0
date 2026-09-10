<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProfessions
    Inherits Presentation.Controls.FormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProfessions))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyProfessions = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgleStudyLevel = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDgrvGleStudyLevel = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcStudyLevel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlygProfessions = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDgrProfessions = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlyItemStudyLevel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyProfessions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyProfessions.SuspendLayout()
        CType(Me.INDgleStudyLevel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvGleStudyLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygProfessions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrProfessions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStudyLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyProfessions)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDlyProfessions
        '
        Me.INDlyProfessions.AllowCustomization = False
        Me.INDlyProfessions.Controls.Add(Me.INDgleStudyLevel)
        Me.INDlyProfessions.Controls.Add(Me.INDtxtName)
        Me.INDlyProfessions.Controls.Add(Me.INDBteCode)
        resources.ApplyResources(Me.INDlyProfessions, "INDlyProfessions")
        Me.LayoutControls.SetIsCustomizable(Me.INDlyProfessions, False)
        Me.INDlyProfessions.Name = "INDlyProfessions"
        Me.INDlyProfessions.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 239, 509, 350)
        Me.INDlyProfessions.Root = Me.INDlygProfessions
        '
        'INDgleStudyLevel
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleStudyLevel, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleStudyLevel, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleStudyLevel, True)
        Me.INDgleStudyLevel.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleStudyLevel, False)
        resources.ApplyResources(Me.INDgleStudyLevel, "INDgleStudyLevel")
        Me.IndigoTextEdit1.SetMascara(Me.INDgleStudyLevel, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleStudyLevel.Name = "INDgleStudyLevel"
        Me.INDgleStudyLevel.Properties.Appearance.BackColor = CType(resources.GetObject("INDgleStudyLevel.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDgleStudyLevel.Properties.Appearance.Font = CType(resources.GetObject("INDgleStudyLevel.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDgleStudyLevel.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleStudyLevel.Properties.Appearance.Options.UseFont = True
        Me.INDgleStudyLevel.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDgleStudyLevel.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDgleStudyLevel.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDgleStudyLevel.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDgleStudyLevel.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDgleStudyLevel.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDgleStudyLevel.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleStudyLevel.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleStudyLevel.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleStudyLevel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDgleStudyLevel.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDgleStudyLevel.Properties.DisplayMember = "Item3"
        Me.INDgleStudyLevel.Properties.ImmediatePopup = True
        Me.INDgleStudyLevel.Properties.NullText = resources.GetString("INDgleStudyLevel.Properties.NullText")
        Me.INDgleStudyLevel.Properties.ValueMember = "Item2"
        Me.INDgleStudyLevel.Properties.View = Me.INDgrvGleStudyLevel
        Me.INDgleStudyLevel.StyleController = Me.INDlyProfessions
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleStudyLevel, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleStudyLevel, 0)
        '
        'INDgrvGleStudyLevel
        '
        Me.INDgrvGleStudyLevel.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("INDgrvGleStudyLevel.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.INDgrvGleStudyLevel.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgrvGleStudyLevel.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgrvGleStudyLevel.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvGleStudyLevel.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvGleStudyLevel.Appearance.GroupRow.Font = CType(resources.GetObject("INDgrvGleStudyLevel.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgrvGleStudyLevel.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvGleStudyLevel.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgrvGleStudyLevel.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgrvGleStudyLevel.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvGleStudyLevel.Appearance.Row.Font = CType(resources.GetObject("INDgrvGleStudyLevel.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgrvGleStudyLevel.Appearance.Row.Options.UseFont = True
        Me.INDgrvGleStudyLevel.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcStudyLevel})
        Me.INDgrvGleStudyLevel.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgrvGleStudyLevel.Name = "INDgrvGleStudyLevel"
        Me.INDgrvGleStudyLevel.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgrvGleStudyLevel.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvGleStudyLevel.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvGleStudyLevel.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvGleStudyLevel.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvGleStudyLevel, False)
        '
        'INDgrcStudyLevel
        '
        resources.ApplyResources(Me.INDgrcStudyLevel, "INDgrcStudyLevel")
        Me.INDgrcStudyLevel.FieldName = "Item3"
        Me.INDgrcStudyLevel.Name = "INDgrcStudyLevel"
        Me.INDgrcStudyLevel.OptionsColumn.AllowEdit = False
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.SoloLetraMayusculaMinuscula)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.Mask.EditMask = resources.GetString("INDtxtName.Properties.Mask.EditMask")
        Me.INDtxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.StyleController = Me.INDlyProfessions
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDBteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.Appearance.Font = CType(resources.GetObject("INDBteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDBteCode.Properties.Buttons7"), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons10"), Boolean))})
        Me.INDBteCode.Properties.Mask.EditMask = resources.GetString("INDBteCode.Properties.Mask.EditMask")
        Me.INDBteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDBteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.StyleController = Me.INDlyProfessions
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDlygProfessions
        '
        Me.INDlygProfessions.AppearanceGroup.Font = CType(resources.GetObject("INDlygProfessions.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlygProfessions.AppearanceGroup.Options.UseFont = True
        Me.INDlygProfessions.AppearanceItemCaption.Font = CType(resources.GetObject("INDlygProfessions.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlygProfessions.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygProfessions.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlygProfessions.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlygProfessions.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygProfessions.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlygProfessions.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlygProfessions.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygProfessions.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlygProfessions.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlygProfessions.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygProfessions.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlygProfessions.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlygProfessions.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygProfessions.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlygProfessions.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlygProfessions.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygProfessions, False)
        resources.ApplyResources(Me.INDlygProfessions, "INDlygProfessions")
        Me.INDlygProfessions.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlygProfessions.GroupBordersVisible = False
        Me.INDlygProfessions.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDgrProfessions})
        Me.INDlygProfessions.Location = New System.Drawing.Point(0, 0)
        Me.INDlygProfessions.Name = "Root"
        Me.INDlygProfessions.Size = New System.Drawing.Size(730, 330)
        Me.INDlygProfessions.TextVisible = False
        '
        'INDgrProfessions
        '
        Me.INDgrProfessions.AppearanceGroup.Font = CType(resources.GetObject("INDgrProfessions.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDgrProfessions.AppearanceGroup.Options.UseFont = True
        Me.INDgrProfessions.AppearanceItemCaption.Font = CType(resources.GetObject("INDgrProfessions.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDgrProfessions.AppearanceItemCaption.Options.UseFont = True
        Me.INDgrProfessions.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDgrProfessions.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDgrProfessions.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDgrProfessions.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDgrProfessions.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDgrProfessions.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDgrProfessions.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDgrProfessions.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDgrProfessions.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDgrProfessions.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDgrProfessions.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDgrProfessions.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDgrProfessions.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDgrProfessions.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDgrProfessions.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDgrProfessions, False)
        resources.ApplyResources(Me.INDgrProfessions, "INDgrProfessions")
        Me.INDgrProfessions.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.EmptySpaceItem1, Me.INDlyItemStudyLevel})
        Me.INDgrProfessions.Location = New System.Drawing.Point(0, 0)
        Me.INDgrProfessions.Name = "INDgrProfessions"
        Me.INDgrProfessions.Size = New System.Drawing.Size(710, 310)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyItemCode.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyItemCode.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemCode.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(686, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyItemName.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyItemName.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(686, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 108)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.ShowInCustomizationForm = False
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(686, 143)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlyItemStudyLevel
        '
        Me.INDlyItemStudyLevel.AppearanceItemCaption.Options.UseTextOptions = True
        Me.INDlyItemStudyLevel.AppearanceItemCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlyItemStudyLevel.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlyItemStudyLevel.Control = Me.INDgleStudyLevel
        resources.ApplyResources(Me.INDlyItemStudyLevel, "INDlyItemStudyLevel")
        Me.INDlyItemStudyLevel.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemStudyLevel.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemStudyLevel.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemStudyLevel.Name = "INDlyItemStudyLevel"
        Me.INDlyItemStudyLevel.OptionsToolTip.ToolTip = resources.GetString("resource.ToolTip")
        Me.INDlyItemStudyLevel.Size = New System.Drawing.Size(686, 36)
        Me.INDlyItemStudyLevel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemStudyLevel.Tag = "StudyLevelId"
        Me.INDlyItemStudyLevel.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemStudyLevel.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemStudyLevel.TextToControlDistance = 12
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmProfessions
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "FrmProfessions"
        Me.Opacity = 1.0R
        Me.Tag = "516"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyProfessions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyProfessions.ResumeLayout(False)
        CType(Me.INDgleStudyLevel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvGleStudyLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygProfessions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrProfessions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStudyLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyProfessions As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlygProfessions As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDgrProfessions As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDgleStudyLevel As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDgrvGleStudyLevel As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemStudyLevel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDgrcStudyLevel As DevExpress.XtraGrid.Columns.GridColumn
End Class
