<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTimeUnit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTimeUnit))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyTimeUnit = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGrTimeUnit = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTimeUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyTimeUnit.SuspendLayout()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrTimeUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyTimeUnit)
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
        'INDLyTimeUnit
        '
        Me.INDLyTimeUnit.AllowCustomization = False
        Me.INDLyTimeUnit.Controls.Add(Me.INDTxtName)
        Me.INDLyTimeUnit.Controls.Add(Me.INDBteCode)
        resources.ApplyResources(Me.INDLyTimeUnit, "INDLyTimeUnit")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyTimeUnit, False)
        Me.INDLyTimeUnit.Name = "INDLyTimeUnit"
        Me.INDLyTimeUnit.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 95, 250, 350)
        Me.INDLyTimeUnit.Root = Me.LayoutControlGroup1
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, False)
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.Mask.EditMask = resources.GetString("INDTxtName.Properties.Mask.EditMask")
        Me.INDTxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtName.Properties.MaxLength = 50
        Me.INDTxtName.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDTxtName.StyleController = Me.INDLyTimeUnit
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBteCode.Properties.Appearance.Font = CType(resources.GetObject("INDBteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Common.My.Resources.Resources.BuscarMetro
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDBteCode.Properties.Buttons6"), resources.GetString("INDBteCode.Properties.Buttons7"), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDBteCode.Properties.Mask.EditMask = resources.GetString("INDBteCode.Properties.Mask.EditMask")
        Me.INDBteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDBteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDBteCode.StyleController = Me.INDLyTimeUnit
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGrTimeUnit})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(730, 317)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLyGrTimeUnit
        '
        Me.INDLyGrTimeUnit.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrTimeUnit.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLyGrTimeUnit.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrTimeUnit.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrTimeUnit.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrTimeUnit.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrTimeUnit.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrTimeUnit.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrTimeUnit.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrTimeUnit.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrTimeUnit.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLyGrTimeUnit.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrTimeUnit.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrTimeUnit.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrTimeUnit.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrTimeUnit.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrTimeUnit.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLyGrTimeUnit.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrTimeUnit.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrTimeUnit.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrTimeUnit.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrTimeUnit, False)
        resources.ApplyResources(Me.INDLyGrTimeUnit, "INDLyGrTimeUnit")
        Me.INDLyGrTimeUnit.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemCode, Me.INDLyItemName})
        Me.INDLyGrTimeUnit.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGrTimeUnit.Name = "INDLyGrTimeUnit"
        Me.INDLyGrTimeUnit.Size = New System.Drawing.Size(710, 297)
        '
        'INDLyItemCode
        '
        Me.INDLyItemCode.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDLyItemCode, "INDLyItemCode")
        Me.INDLyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemCode.Name = "INDLyItemCode"
        Me.INDLyItemCode.Size = New System.Drawing.Size(686, 36)
        Me.INDLyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemCode.Tag = "Code"
        Me.INDLyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemCode.TextSize = New System.Drawing.Size(100, 21)
        Me.INDLyItemCode.TextToControlDistance = 12
        '
        'INDLyItemName
        '
        Me.INDLyItemName.Control = Me.INDTxtName
        resources.ApplyResources(Me.INDLyItemName, "INDLyItemName")
        Me.INDLyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemName.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDLyItemName.MinSize = New System.Drawing.Size(410, 36)
        Me.INDLyItemName.Name = "INDLyItemName"
        Me.INDLyItemName.Size = New System.Drawing.Size(686, 208)
        Me.INDLyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemName.Tag = "Name"
        Me.INDLyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemName.TextSize = New System.Drawing.Size(100, 21)
        Me.INDLyItemName.TextToControlDistance = 12
        '
        'FrmTimeUnit
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmTimeUnit"
        Me.Opacity = 1.0R
        Me.Tag = "548"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTimeUnit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyTimeUnit.ResumeLayout(False)
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrTimeUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyTimeUnit As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLyGrTimeUnit As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
End Class
