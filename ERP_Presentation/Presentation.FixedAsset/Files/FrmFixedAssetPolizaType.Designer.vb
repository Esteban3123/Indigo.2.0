<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetPolizaType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetPolizaType))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCtrPolizaTypes = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyKinship = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrPolizaType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCodePolizaType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemNamePolizaType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCtrPolizaTypes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCtrPolizaTypes.SuspendLayout()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyKinship, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrPolizaType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCodePolizaType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemNamePolizaType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCtrPolizaTypes)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
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
        'INDLyCtrPolizaTypes
        '
        Me.INDLyCtrPolizaTypes.AllowCustomization = False
        Me.INDLyCtrPolizaTypes.Controls.Add(Me.INDTxtName)
        Me.INDLyCtrPolizaTypes.Controls.Add(Me.INDBteCode)
        resources.ApplyResources(Me.INDLyCtrPolizaTypes, "INDLyCtrPolizaTypes")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrPolizaTypes, False)
        Me.INDLyCtrPolizaTypes.Name = "INDLyCtrPolizaTypes"
        Me.INDLyCtrPolizaTypes.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 221, 250, 350)
        Me.INDLyCtrPolizaTypes.Root = Me.INDlyKinship
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, False)
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDTxtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.INDTxtName.Properties.MaxLength = 50
        Me.INDTxtName.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDTxtName.StyleController = Me.INDLyCtrPolizaTypes
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, False)
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDBteCode.Properties.Appearance.Font = CType(resources.GetObject("INDBteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.FixedAsset.My.Resources.Resources.BuscarMetro
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDBteCode.Properties.Buttons6"), resources.GetString("INDBteCode.Properties.Buttons7"), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDBteCode.Properties.Mask.EditMask = resources.GetString("INDBteCode.Properties.Mask.EditMask")
        Me.INDBteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDBteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Properties.ShowNullValuePrompt = CType((DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorFocused Or DevExpress.XtraEditors.ShowNullValuePromptOptions.EditorReadOnly), DevExpress.XtraEditors.ShowNullValuePromptOptions)
        Me.INDBteCode.StyleController = Me.INDLyCtrPolizaTypes
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDlyKinship
        '
        resources.ApplyResources(Me.INDlyKinship, "INDlyKinship")
        Me.INDlyKinship.GroupBordersVisible = False
        Me.INDlyKinship.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrPolizaType})
        Me.INDlyKinship.Name = "INDlyKinship"
        Me.INDlyKinship.Size = New System.Drawing.Size(543, 317)
        Me.INDlyKinship.TextVisible = False
        '
        'INDGrPolizaType
        '
        Me.INDGrPolizaType.AppearanceGroup.Font = CType(resources.GetObject("INDGrPolizaType.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDGrPolizaType.AppearanceGroup.Options.UseFont = True
        Me.INDGrPolizaType.AppearanceItemCaption.Font = CType(resources.GetObject("INDGrPolizaType.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDGrPolizaType.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDGrPolizaType, "INDGrPolizaType")
        Me.INDGrPolizaType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCodePolizaType, Me.INDLyItemNamePolizaType})
        Me.INDGrPolizaType.Location = New System.Drawing.Point(0, 0)
        Me.INDGrPolizaType.Name = "INDGrPolizaType"
        Me.INDGrPolizaType.Size = New System.Drawing.Size(543, 317)
        '
        'INDlyItemCodePolizaType
        '
        Me.INDlyItemCodePolizaType.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDlyItemCodePolizaType, "INDlyItemCodePolizaType")
        Me.INDlyItemCodePolizaType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCodePolizaType.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodePolizaType.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodePolizaType.Name = "INDlyItemCodePolizaType"
        Me.INDlyItemCodePolizaType.ShowInCustomizationForm = False
        Me.INDlyItemCodePolizaType.Size = New System.Drawing.Size(519, 36)
        Me.INDlyItemCodePolizaType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCodePolizaType.Tag = "Code"
        Me.INDlyItemCodePolizaType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCodePolizaType.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCodePolizaType.TextToControlDistance = 12
        '
        'INDLyItemNamePolizaType
        '
        Me.INDLyItemNamePolizaType.Control = Me.INDTxtName
        resources.ApplyResources(Me.INDLyItemNamePolizaType, "INDLyItemNamePolizaType")
        Me.INDLyItemNamePolizaType.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemNamePolizaType.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyItemNamePolizaType.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyItemNamePolizaType.Name = "INDLyItemNamePolizaType"
        Me.INDLyItemNamePolizaType.ShowInCustomizationForm = False
        Me.INDLyItemNamePolizaType.Size = New System.Drawing.Size(519, 228)
        Me.INDLyItemNamePolizaType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemNamePolizaType.Tag = "Name"
        Me.INDLyItemNamePolizaType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemNamePolizaType.TextSize = New System.Drawing.Size(160, 21)
        Me.INDLyItemNamePolizaType.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.Appearance.BackColor = System.Drawing.Color.White
        Me.CtrNavigationControl1.Appearance.Image = CType(resources.GetObject("CtrNavigationControl1.Appearance.Image"), System.Drawing.Image)
        Me.CtrNavigationControl1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControl1.Appearance.Options.UseImage = True
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyCtrPolizaTypes
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmFixedAssetPolizaType
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetPolizaType"
        Me.Opacity = 1.0R
        Me.Tag = "1701"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCtrPolizaTypes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCtrPolizaTypes.ResumeLayout(False)
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyKinship, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrPolizaType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCodePolizaType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemNamePolizaType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCtrPolizaTypes As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyKinship As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGrPolizaType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCodePolizaType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemNamePolizaType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
End Class
