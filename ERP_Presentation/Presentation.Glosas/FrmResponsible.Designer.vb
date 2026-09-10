Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmResponsible
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmResponsible))
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyResponsibles = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtCharge = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtCodeERP = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrResponsibles = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCodeERP = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCharge = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyResponsibles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyResponsibles.SuspendLayout()
        CType(Me.INDtxtCharge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtCodeERP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrResponsibles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCodeERP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyResponsibles)
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
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, True)
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
        EditorButtonImageOptions1.Image = Global.Presentation.Glosas.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDbteCode.Properties.Buttons6"), CType(resources.GetObject("INDbteCode.Properties.Buttons7"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.StyleController = Me.INDlyResponsibles
        Me.INDbteCode.Tag = "Código"
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDlyResponsibles
        '
        Me.INDlyResponsibles.AllowCustomization = False
        Me.INDlyResponsibles.Controls.Add(Me.INDtxtCharge)
        Me.INDlyResponsibles.Controls.Add(Me.INDtxtCodeERP)
        Me.INDlyResponsibles.Controls.Add(Me.INDtxtName)
        Me.INDlyResponsibles.Controls.Add(Me.INDbteCode)
        resources.ApplyResources(Me.INDlyResponsibles, "INDlyResponsibles")
        Me.LayoutControls.SetIsCustomizable(Me.INDlyResponsibles, False)
        Me.INDlyResponsibles.Name = "INDlyResponsibles"
        Me.INDlyResponsibles.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(741, 239, 250, 350)
        Me.INDlyResponsibles.Root = Me.LayoutControlGroup1
        '
        'INDtxtCharge
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCharge, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCharge, True)
        Me.INDtxtCharge.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtCharge, "INDtxtCharge")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCharge, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCharge.Name = "INDtxtCharge"
        Me.INDtxtCharge.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtCharge.Properties.Appearance.Font = CType(resources.GetObject("INDtxtCharge.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtCharge.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtCharge.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCharge.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCharge.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtCharge.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtCharge.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtCharge.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtCharge.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtCharge.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtCharge.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtCharge.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtCharge.Properties.MaxLength = 60
        Me.INDtxtCharge.StyleController = Me.INDlyResponsibles
        Me.INDtxtCharge.Tag = "Cargo"
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCharge, 0)
        '
        'INDtxtCodeERP
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCodeERP, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCodeERP, True)
        Me.INDtxtCodeERP.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtCodeERP, "INDtxtCodeERP")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCodeERP, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCodeERP.Name = "INDtxtCodeERP"
        Me.INDtxtCodeERP.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtCodeERP.Properties.Appearance.Font = CType(resources.GetObject("INDtxtCodeERP.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtCodeERP.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtCodeERP.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCodeERP.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCodeERP.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtCodeERP.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtCodeERP.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtCodeERP.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtCodeERP.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtCodeERP.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtCodeERP.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtCodeERP.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtCodeERP.Properties.MaxLength = 20
        Me.INDtxtCodeERP.StyleController = Me.INDlyResponsibles
        Me.INDtxtCodeERP.Tag = "CódigoERP"
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCodeERP, 0)
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
        Me.INDtxtName.Properties.MaxLength = 40
        Me.INDtxtName.StyleController = Me.INDlyResponsibles
        Me.INDtxtName.Tag = "Nombre"
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrResponsibles})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(530, 317)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrResponsibles
        '
        resources.ApplyResources(Me.INDlyGrResponsibles, "INDlyGrResponsibles")
        Me.INDlyGrResponsibles.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemCodeERP, Me.INDlyItemCharge})
        Me.INDlyGrResponsibles.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrResponsibles.Name = "INDlyGrResponsibles"
        Me.INDlyGrResponsibles.Size = New System.Drawing.Size(510, 297)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(486, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(486, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemCodeERP
        '
        Me.INDlyItemCodeERP.AllowHide = False
        Me.INDlyItemCodeERP.Control = Me.INDtxtCodeERP
        resources.ApplyResources(Me.INDlyItemCodeERP, "INDlyItemCodeERP")
        Me.INDlyItemCodeERP.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemCodeERP.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCodeERP.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCodeERP.Name = "INDlyItemCodeERP"
        Me.INDlyItemCodeERP.Size = New System.Drawing.Size(486, 36)
        Me.INDlyItemCodeERP.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCodeERP.Tag = "CodeERP"
        Me.INDlyItemCodeERP.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCodeERP.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCodeERP.TextToControlDistance = 12
        '
        'INDlyItemCharge
        '
        Me.INDlyItemCharge.AllowHide = False
        Me.INDlyItemCharge.Control = Me.INDtxtCharge
        resources.ApplyResources(Me.INDlyItemCharge, "INDlyItemCharge")
        Me.INDlyItemCharge.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemCharge.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCharge.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCharge.Name = "INDlyItemCharge"
        Me.INDlyItemCharge.Size = New System.Drawing.Size(486, 136)
        Me.INDlyItemCharge.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCharge.Tag = "Charge"
        Me.INDlyItemCharge.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCharge.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCharge.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyResponsibles
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmResponsible
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmResponsible"
        Me.Opacity = 1.0R
        Me.Tag = "504"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyResponsibles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyResponsibles.ResumeLayout(False)
        CType(Me.INDtxtCharge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtCodeERP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrResponsibles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCodeERP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlyResponsibles As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtCharge As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtCodeERP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCodeERP As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCharge As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGrResponsibles As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
End Class
