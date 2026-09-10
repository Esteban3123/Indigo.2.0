<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmStudyCenter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmStudyCenter))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCtrStudyCenter = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLyStudyCenter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGrStudyCenter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCtrStudyCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCtrStudyCenter.SuspendLayout()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyStudyCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrStudyCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCtrStudyCenter)
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
        'INDLyCtrStudyCenter
        '
        Me.INDLyCtrStudyCenter.AllowCustomization = False
        Me.INDLyCtrStudyCenter.Controls.Add(Me.INDTxtName)
        Me.INDLyCtrStudyCenter.Controls.Add(Me.INDBteCode)
        resources.ApplyResources(Me.INDLyCtrStudyCenter, "INDLyCtrStudyCenter")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrStudyCenter, False)
        Me.INDLyCtrStudyCenter.Name = "INDLyCtrStudyCenter"
        Me.INDLyCtrStudyCenter.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2467, 42, 574, 569)
        Me.INDLyCtrStudyCenter.Root = Me.INDLyStudyCenter
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.MaxLength = 80
        Me.INDTxtName.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDTxtName.StyleController = Me.INDLyCtrStudyCenter
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        Me.INDBteCode.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        EditorButtonImageOptions1.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDBteCode.Properties.Buttons6"), resources.GetString("INDBteCode.Properties.Buttons7"), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDBteCode.StyleController = Me.INDLyCtrStudyCenter
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDLyStudyCenter
        '
        Me.INDLyStudyCenter.AppearanceGroup.Font = CType(resources.GetObject("INDLyStudyCenter.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLyStudyCenter.AppearanceGroup.Options.UseFont = True
        Me.INDLyStudyCenter.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyStudyCenter.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyStudyCenter.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyStudyCenter.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyStudyCenter.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyStudyCenter.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyStudyCenter.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyStudyCenter.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLyStudyCenter.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyStudyCenter.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyStudyCenter.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyStudyCenter.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyStudyCenter.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyStudyCenter.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLyStudyCenter.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyStudyCenter.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyStudyCenter.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyStudyCenter.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyStudyCenter, False)
        resources.ApplyResources(Me.INDLyStudyCenter, "INDLyStudyCenter")
        Me.INDLyStudyCenter.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLyStudyCenter.GroupBordersVisible = False
        Me.INDLyStudyCenter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGrStudyCenter})
        Me.INDLyStudyCenter.Name = "Root"
        Me.INDLyStudyCenter.Size = New System.Drawing.Size(730, 317)
        Me.INDLyStudyCenter.TextVisible = False
        '
        'INDLyGrStudyCenter
        '
        Me.INDLyGrStudyCenter.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrStudyCenter.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLyGrStudyCenter.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrStudyCenter.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrStudyCenter.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrStudyCenter.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrStudyCenter.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrStudyCenter.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrStudyCenter.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrStudyCenter.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrStudyCenter.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLyGrStudyCenter.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrStudyCenter.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrStudyCenter.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrStudyCenter.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrStudyCenter.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrStudyCenter.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLyGrStudyCenter.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrStudyCenter.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrStudyCenter.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrStudyCenter.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrStudyCenter, False)
        resources.ApplyResources(Me.INDLyGrStudyCenter, "INDLyGrStudyCenter")
        Me.INDLyGrStudyCenter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemCode, Me.INDLyItemName})
        Me.INDLyGrStudyCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGrStudyCenter.Name = "INDLyGrStudyCenter"
        Me.INDLyGrStudyCenter.Size = New System.Drawing.Size(710, 297)
        '
        'INDLyItemCode
        '
        Me.INDLyItemCode.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDLyItemCode, "INDLyItemCode")
        Me.INDLyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemCode.MaxSize = New System.Drawing.Size(280, 36)
        Me.INDLyItemCode.MinSize = New System.Drawing.Size(280, 36)
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
        Me.INDLyItemName.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDLyItemName.MinSize = New System.Drawing.Size(450, 36)
        Me.INDLyItemName.Name = "INDLyItemName"
        Me.INDLyItemName.Size = New System.Drawing.Size(686, 208)
        Me.INDLyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemName.Tag = "Name"
        Me.INDLyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemName.TextSize = New System.Drawing.Size(100, 21)
        Me.INDLyItemName.TextToControlDistance = 12
        '
        'FrmStudyCenter
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmStudyCenter"
        Me.Opacity = 1.0R
        Me.Tag = "540"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCtrStudyCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCtrStudyCenter.ResumeLayout(False)
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyStudyCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrStudyCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCtrStudyCenter As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLyStudyCenter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLyGrStudyCenter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
End Class
