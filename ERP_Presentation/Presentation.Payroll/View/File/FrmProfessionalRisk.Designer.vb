Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProfessionalRisk
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProfessionalRisk))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyCtrProfessionalRisk = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDtxtPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtArlCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDlygProfessionalRisk = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrpProfessionalRisk = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemArlCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtrProfessionalRisk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtrProfessionalRisk.SuspendLayout()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtArlCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygProfessionalRisk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrpProfessionalRisk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemArlCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCtrProfessionalRisk)
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
        'INDlyCtrProfessionalRisk
        '
        Me.INDlyCtrProfessionalRisk.AllowCustomization = False
        Me.INDlyCtrProfessionalRisk.Controls.Add(Me.INDtxtName)
        Me.INDlyCtrProfessionalRisk.Controls.Add(Me.INDbteCode)
        Me.INDlyCtrProfessionalRisk.Controls.Add(Me.INDtxtPercentage)
        Me.INDlyCtrProfessionalRisk.Controls.Add(Me.INDtxtArlCode)
        resources.ApplyResources(Me.INDlyCtrProfessionalRisk, "INDlyCtrProfessionalRisk")
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCtrProfessionalRisk, False)
        Me.INDlyCtrProfessionalRisk.Name = "INDlyCtrProfessionalRisk"
        Me.INDlyCtrProfessionalRisk.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(487, 262, 250, 350)
        Me.INDlyCtrProfessionalRisk.Root = Me.INDlygProfessionalRisk
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit11.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
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
        Me.INDtxtName.Properties.Mask.EditMask = resources.GetString("INDtxtName.Properties.Mask.EditMask")
        Me.INDtxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtName.Properties.MaxLength = 20
        Me.INDtxtName.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDtxtName.StyleController = Me.INDlyCtrProfessionalRisk
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 5)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDbteCode, False)
        Me.INDbteCode.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.IndigoTextEdit11.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        EditorButtonImageOptions1.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDbteCode.Properties.Buttons6"), resources.GetString("INDbteCode.Properties.Buttons7"), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDbteCode.Properties.Mask.EditMask = resources.GetString("INDbteCode.Properties.Mask.EditMask")
        Me.INDbteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDbteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDbteCode.StyleController = Me.INDlyCtrProfessionalRisk
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 1)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDtxtPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPercentage, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtxtPercentage, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtxtPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPercentage, False)
        resources.ApplyResources(Me.INDtxtPercentage, "INDtxtPercentage")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPercentage, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDtxtPercentage, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPercentage.Name = "INDtxtPercentage"
        Me.INDtxtPercentage.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtPercentage.Properties.Appearance.Font = CType(resources.GetObject("INDtxtPercentage.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPercentage.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtPercentage.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtPercentage.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtPercentage.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtPercentage.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPercentage.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtPercentage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDtxtPercentage.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDtxtPercentage.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtPercentage.Properties.Mask.EditMask = resources.GetString("INDtxtPercentage.Properties.Mask.EditMask")
        Me.INDtxtPercentage.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDtxtPercentage.StyleController = Me.INDlyCtrProfessionalRisk
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPercentage, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtxtPercentage, 0)
        '
        'INDtxtArlCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtArlCode, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtxtArlCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtArlCode, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtxtArlCode, True)
        Me.INDtxtArlCode.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtArlCode, "INDtxtArlCode")
        Me.IndigoTextEdit11.SetMascara(Me.INDtxtArlCode, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtArlCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtArlCode.Name = "INDtxtArlCode"
        Me.INDtxtArlCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtArlCode.Properties.Appearance.Font = CType(resources.GetObject("INDtxtArlCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtArlCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtArlCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtArlCode.Properties.Appearance.Options.UseFont = True
        Me.INDtxtArlCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtArlCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtArlCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtArlCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtArlCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtArlCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtArlCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtArlCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtArlCode.Properties.Mask.EditMask = resources.GetString("INDtxtArlCode.Properties.Mask.EditMask")
        Me.INDtxtArlCode.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtArlCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtArlCode.Properties.MaxLength = 7
        Me.INDtxtArlCode.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDtxtArlCode.StyleController = Me.INDlyCtrProfessionalRisk
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtArlCode, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtxtArlCode, 5)
        '
        'INDlygProfessionalRisk
        '
        Me.INDlygProfessionalRisk.AppearanceGroup.Font = CType(resources.GetObject("INDlygProfessionalRisk.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlygProfessionalRisk.AppearanceGroup.Options.UseFont = True
        Me.INDlygProfessionalRisk.AppearanceItemCaption.Font = CType(resources.GetObject("INDlygProfessionalRisk.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlygProfessionalRisk.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDlygProfessionalRisk, "INDlygProfessionalRisk")
        Me.INDlygProfessionalRisk.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlygProfessionalRisk.GroupBordersVisible = False
        Me.INDlygProfessionalRisk.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrpProfessionalRisk, Me.EmptySpaceItem1})
        Me.INDlygProfessionalRisk.Name = "Root"
        Me.INDlygProfessionalRisk.Size = New System.Drawing.Size(680, 317)
        Me.INDlygProfessionalRisk.TextVisible = False
        '
        'INDlyGrpProfessionalRisk
        '
        resources.ApplyResources(Me.INDlyGrpProfessionalRisk, "INDlyGrpProfessionalRisk")
        Me.INDlyGrpProfessionalRisk.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemPercentage, Me.INDlyItemArlCode})
        Me.INDlyGrpProfessionalRisk.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrpProfessionalRisk.Name = "INDlyGrpProfessionalRisk"
        Me.INDlyGrpProfessionalRisk.Size = New System.Drawing.Size(660, 197)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(230, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(230, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(350, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(350, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemPercentage
        '
        Me.INDlyItemPercentage.Control = Me.INDtxtPercentage
        resources.ApplyResources(Me.INDlyItemPercentage, "INDlyItemPercentage")
        Me.INDlyItemPercentage.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemPercentage.MaxSize = New System.Drawing.Size(350, 36)
        Me.INDlyItemPercentage.MinSize = New System.Drawing.Size(350, 36)
        Me.INDlyItemPercentage.Name = "INDlyItemPercentage"
        Me.INDlyItemPercentage.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPercentage.Tag = "Percentage"
        Me.INDlyItemPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPercentage.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemPercentage.TextToControlDistance = 12
        '
        'INDlyItemArlCode
        '
        Me.INDlyItemArlCode.Control = Me.INDtxtArlCode
        resources.ApplyResources(Me.INDlyItemArlCode, "INDlyItemArlCode")
        Me.INDlyItemArlCode.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemArlCode.MaxSize = New System.Drawing.Size(350, 36)
        Me.INDlyItemArlCode.MinSize = New System.Drawing.Size(350, 36)
        Me.INDlyItemArlCode.Name = "INDlyItemArlCode"
        Me.INDlyItemArlCode.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemArlCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemArlCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemArlCode.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemArlCode.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 197)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(660, 100)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmProfessionalRisk
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmProfessionalRisk"
        Me.Opacity = 1.0R
        Me.Tag = "535"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtrProfessionalRisk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtrProfessionalRisk.ResumeLayout(False)
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtArlCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygProfessionalRisk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrpProfessionalRisk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemArlCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyCtrProfessionalRisk As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlygProfessionalRisk As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGrpProfessionalRisk As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDtxtPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents IndigoTextEdit11 As IndigoTextEdit
    Friend WithEvents INDtxtArlCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemArlCode As DevExpress.XtraLayout.LayoutControlItem
End Class
