<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmContractTemplate
    Inherits Presentation.Controls.FormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmContractTemplate))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCtrContractTemplate = New DevExpress.XtraLayout.LayoutControl()
        Me.INDReFullContract = New DevExpress.XtraRichEdit.RichEditControl()
        Me.INDGleContractType = New Presentation.Controls.GridLookUpMultiFilter()
        Me.GridLookUpMultiFilter1View = New Presentation.Controls.CustomGridView()
        Me.CTCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CTName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGrContractTemplate = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemContractType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGrFullContract = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemFullContract = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCtrContractTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCtrContractTemplate.SuspendLayout()
        CType(Me.INDGleContractType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpMultiFilter1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrContractTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrFullContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemFullContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCtrContractTemplate)
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
        'INDLyCtrContractTemplate
        '
        Me.INDLyCtrContractTemplate.AllowCustomizationMenu = False
        Me.INDLyCtrContractTemplate.Controls.Add(Me.INDReFullContract)
        Me.INDLyCtrContractTemplate.Controls.Add(Me.INDGleContractType)
        Me.INDLyCtrContractTemplate.Controls.Add(Me.INDTxtName)
        Me.INDLyCtrContractTemplate.Controls.Add(Me.INDBteCode)
        resources.ApplyResources(Me.INDLyCtrContractTemplate, "INDLyCtrContractTemplate")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrContractTemplate, False)
        Me.INDLyCtrContractTemplate.Name = "INDLyCtrContractTemplate"
        Me.INDLyCtrContractTemplate.Root = Me.LayoutControlGroup1
        '
        'INDReFullContract
        '
        Me.INDReFullContract.EnableToolTips = True
        resources.ApplyResources(Me.INDReFullContract, "INDReFullContract")
        Me.INDReFullContract.Name = "INDReFullContract"
        Me.INDReFullContract.Options.Comments.ShowAllAuthors = True
        Me.INDReFullContract.Options.Comments.Visibility = DevExpress.XtraRichEdit.RichEditCommentVisibility.Auto
        Me.INDReFullContract.Options.CopyPaste.MaintainDocumentSectionSettings = False
        Me.INDReFullContract.Options.Fields.UseCurrentCultureDateTimeFormat = False
        Me.INDReFullContract.Options.MailMerge.KeepLastParagraph = False
        '
        'INDGleContractType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleContractType, True)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleContractType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleContractType, True)
        Me.INDGleContractType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleContractType, False)
        resources.ApplyResources(Me.INDGleContractType, "INDGleContractType")
        Me.IndigoTextEdit1.SetMascara(Me.INDGleContractType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleContractType.Name = "INDGleContractType"
        Me.INDGleContractType.Properties.Appearance.BackColor = CType(resources.GetObject("INDGleContractType.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDGleContractType.Properties.Appearance.Font = CType(resources.GetObject("INDGleContractType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleContractType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleContractType.Properties.Appearance.Options.UseFont = True
        Me.INDGleContractType.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDGleContractType.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDGleContractType.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDGleContractType.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDGleContractType.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGleContractType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGleContractType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleContractType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleContractType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleContractType.Properties.AutoComplete = False
        Me.INDGleContractType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGleContractType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGleContractType.Properties.DisplayMember = "Name"
        Me.INDGleContractType.Properties.ImmediatePopup = True
        Me.INDGleContractType.Properties.NullText = resources.GetString("INDGleContractType.Properties.NullText")
        Me.INDGleContractType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDGleContractType.Properties.ValueMember = "Id"
        Me.INDGleContractType.Properties.View = Me.GridLookUpMultiFilter1View
        Me.INDGleContractType.StyleController = Me.INDLyCtrContractTemplate
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleContractType, "541")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleContractType, 0)
        '
        'GridLookUpMultiFilter1View
        '
        Me.GridLookUpMultiFilter1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpMultiFilter1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpMultiFilter1View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpMultiFilter1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpMultiFilter1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CTCode, Me.CTName})
        Me.GridLookUpMultiFilter1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpMultiFilter1View.Name = "GridLookUpMultiFilter1View"
        Me.GridLookUpMultiFilter1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpMultiFilter1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpMultiFilter1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpMultiFilter1View.OptionsView.ShowGroupPanel = False
        '
        'CTCode
        '
        Me.CTCode.FieldName = "Code"
        Me.CTCode.Name = "CTCode"
        resources.ApplyResources(Me.CTCode, "CTCode")
        '
        'CTName
        '
        Me.CTName.FieldName = "Name"
        Me.CTName.Name = "CTName"
        resources.ApplyResources(Me.CTName, "CTName")
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.Mask.EditMask = resources.GetString("INDTxtName.Properties.Mask.EditMask")
        Me.INDTxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtName.Properties.MaxLength = 100
        Me.INDTxtName.StyleController = Me.INDLyCtrContractTemplate
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
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
        Me.INDBteCode.StyleController = Me.INDLyCtrContractTemplate
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGrContractTemplate, Me.INDLyGrFullContract})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(986, 603)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLyGrContractTemplate
        '
        Me.INDLyGrContractTemplate.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrContractTemplate.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLyGrContractTemplate.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDLyGrContractTemplate.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGrContractTemplate.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrContractTemplate.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrContractTemplate.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrContractTemplate.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrContractTemplate.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrContractTemplate.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrContractTemplate.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrContractTemplate.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLyGrContractTemplate.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDLyGrContractTemplate.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGrContractTemplate.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrContractTemplate.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrContractTemplate.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrContractTemplate.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrContractTemplate.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLyGrContractTemplate.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDLyGrContractTemplate.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGrContractTemplate.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrContractTemplate.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrContractTemplate.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrContractTemplate, False)
        resources.ApplyResources(Me.INDLyGrContractTemplate, "INDLyGrContractTemplate")
        Me.INDLyGrContractTemplate.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemCode, Me.INDLyItemName, Me.INDlyItemContractType})
        Me.INDLyGrContractTemplate.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGrContractTemplate.Name = "INDLyGrContractTemplate"
        Me.INDLyGrContractTemplate.Size = New System.Drawing.Size(966, 168)
        '
        'INDLyItemCode
        '
        Me.INDLyItemCode.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDLyItemCode, "INDLyItemCode")
        Me.INDLyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemCode.MaxSize = New System.Drawing.Size(340, 36)
        Me.INDLyItemCode.MinSize = New System.Drawing.Size(340, 36)
        Me.INDLyItemCode.Name = "INDLyItemCode"
        Me.INDLyItemCode.Size = New System.Drawing.Size(942, 36)
        Me.INDLyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemCode.Tag = "Code"
        Me.INDLyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemCode.TextSize = New System.Drawing.Size(140, 21)
        Me.INDLyItemCode.TextToControlDistance = 12
        '
        'INDLyItemName
        '
        Me.INDLyItemName.Control = Me.INDTxtName
        resources.ApplyResources(Me.INDLyItemName, "INDLyItemName")
        Me.INDLyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemName.MaxSize = New System.Drawing.Size(430, 36)
        Me.INDLyItemName.MinSize = New System.Drawing.Size(430, 36)
        Me.INDLyItemName.Name = "INDLyItemName"
        Me.INDLyItemName.Size = New System.Drawing.Size(942, 36)
        Me.INDLyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemName.Tag = "Name"
        Me.INDLyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemName.TextSize = New System.Drawing.Size(140, 21)
        Me.INDLyItemName.TextToControlDistance = 12
        '
        'INDlyItemContractType
        '
        Me.INDlyItemContractType.Control = Me.INDGleContractType
        resources.ApplyResources(Me.INDlyItemContractType, "INDlyItemContractType")
        Me.INDlyItemContractType.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemContractType.MaxSize = New System.Drawing.Size(430, 36)
        Me.INDlyItemContractType.MinSize = New System.Drawing.Size(430, 36)
        Me.INDlyItemContractType.Name = "INDlyItemContractType"
        Me.INDlyItemContractType.Size = New System.Drawing.Size(942, 36)
        Me.INDlyItemContractType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemContractType.Tag = "ContractTypeId"
        Me.INDlyItemContractType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemContractType.TextSize = New System.Drawing.Size(140, 21)
        Me.INDlyItemContractType.TextToControlDistance = 12
        '
        'INDLyGrFullContract
        '
        Me.INDLyGrFullContract.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrFullContract.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLyGrFullContract.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDLyGrFullContract.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGrFullContract.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrFullContract.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrFullContract.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrFullContract.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrFullContract.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrFullContract.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrFullContract.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrFullContract.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLyGrFullContract.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDLyGrFullContract.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGrFullContract.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrFullContract.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrFullContract.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrFullContract.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrFullContract.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDLyGrFullContract.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDLyGrFullContract.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLyGrFullContract.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrFullContract.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrFullContract.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrFullContract, False)
        resources.ApplyResources(Me.INDLyGrFullContract, "INDLyGrFullContract")
        Me.INDLyGrFullContract.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemFullContract})
        Me.INDLyGrFullContract.Location = New System.Drawing.Point(0, 168)
        Me.INDLyGrFullContract.Name = "INDLyGrFullContract"
        Me.INDLyGrFullContract.Size = New System.Drawing.Size(966, 415)
        '
        'INDLyItemFullContract
        '
        Me.INDLyItemFullContract.Control = Me.INDReFullContract
        resources.ApplyResources(Me.INDLyItemFullContract, "INDLyItemFullContract")
        Me.INDLyItemFullContract.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemFullContract.Name = "INDLyItemFullContract"
        Me.INDLyItemFullContract.Size = New System.Drawing.Size(942, 355)
        Me.INDLyItemFullContract.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemFullContract.TextToControlDistance = 0
        Me.INDLyItemFullContract.TextVisible = False
        '
        'FrmContractTemplate
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "FrmContractTemplate"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Tag = "543"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCtrContractTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCtrContractTemplate.ResumeLayout(False)
        CType(Me.INDGleContractType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpMultiFilter1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrContractTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemContractType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrFullContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemFullContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCtrContractTemplate As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGleContractType As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents GridLookUpMultiFilter1View As Presentation.Controls.CustomGridView
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLyGrContractTemplate As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemContractType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDLyGrFullContract As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDReFullContract As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents INDLyItemFullContract As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CTCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CTName As DevExpress.XtraGrid.Columns.GridColumn
End Class
