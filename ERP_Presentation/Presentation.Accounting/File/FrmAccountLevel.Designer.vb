Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountLevel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccountLevel))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDrgbAux = New DevExpress.XtraEditors.RadioGroup()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlycgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlycgUnique = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyciAux = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycRoot.SuspendLayout()
        CType(Me.INDrgbAux.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgUnique, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciAux, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Tag = "600"
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
        'INDlycRoot
        '
        Me.INDlycRoot.Controls.Add(Me.INDrgbAux)
        Me.INDlycRoot.Controls.Add(Me.INDtxtName)
        Me.INDlycRoot.Controls.Add(Me.INDbteCode)
        resources.ApplyResources(Me.INDlycRoot, "INDlycRoot")
        Me.INDlycRoot.Name = "INDlycRoot"
        Me.INDlycRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(458, 284, 445, 350)
        Me.INDlycRoot.Root = Me.INDlycgRoot
        '
        'INDrgbAux
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgbAux, False)
        resources.ApplyResources(Me.INDrgbAux, "INDrgbAux")
        Me.INDrgbAux.Name = "INDrgbAux"
        Me.INDrgbAux.Properties.Appearance.BackColor = CType(resources.GetObject("INDrgbAux.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDrgbAux.Properties.Appearance.Font = CType(resources.GetObject("INDrgbAux.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDrgbAux.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgbAux.Properties.Appearance.Options.UseFont = True
        Me.INDrgbAux.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDrgbAux.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDrgbAux.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDrgbAux.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDrgbAux.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDrgbAux.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDrgbAux.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgbAux.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDrgbAux.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgbAux.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDrgbAux.Properties.Items"), Object), resources.GetString("INDrgbAux.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDrgbAux.Properties.Items2"), Object), resources.GetString("INDrgbAux.Properties.Items3"))})
        Me.INDrgbAux.StyleController = Me.INDlycRoot
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
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
        Me.INDtxtName.StyleController = Me.INDlycRoot
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDbteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.Appearance.Font = CType(resources.GetObject("INDbteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Accounting.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteCode.Properties.Buttons7"), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons10"), Boolean))})
        Me.INDbteCode.Properties.Mask.EditMask = resources.GetString("INDbteCode.Properties.Mask.EditMask")
        Me.INDbteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDbteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDbteCode.Properties.MaxLength = 3
        Me.INDbteCode.StyleController = Me.INDlycRoot
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDlycgRoot
        '
        Me.INDlycgRoot.AllowCustomizeChildren = False
        Me.INDlycgRoot.AllowHide = False
        Me.INDlycgRoot.AppearanceGroup.Font = CType(resources.GetObject("INDlycgRoot.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlycgRoot.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDlycgRoot.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgRoot.AppearanceItemCaption.Font = CType(resources.GetObject("INDlycgRoot.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgRoot.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycgRoot, False)
        resources.ApplyResources(Me.INDlycgRoot, "INDlycgRoot")
        Me.INDlycgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycgRoot.GroupBordersVisible = False
        Me.INDlycgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlycgUnique})
        Me.INDlycgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgRoot.Name = "INDlycgRoot"
        Me.INDlycgRoot.ShowInCustomizationForm = False
        Me.INDlycgRoot.Size = New System.Drawing.Size(595, 602)
        Me.INDlycgRoot.TextVisible = False
        '
        'INDlycgUnique
        '
        Me.INDlycgUnique.AppearanceGroup.Font = CType(resources.GetObject("INDlycgUnique.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlycgUnique.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDlycgUnique.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgUnique.AppearanceItemCaption.Font = CType(resources.GetObject("INDlycgUnique.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlycgUnique.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgUnique.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlycgUnique.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlycgUnique.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgUnique.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlycgUnique.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlycgUnique.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDlycgUnique.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgUnique.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlycgUnique.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlycgUnique.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgUnique.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlycgUnique.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("INDlycgUnique.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDlycgUnique.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycgUnique.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlycgUnique.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlycgUnique.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycgUnique, False)
        resources.ApplyResources(Me.INDlycgUnique, "INDlycgUnique")
        Me.INDlycgUnique.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyciCode, Me.INDlyciName, Me.INDlyciAux})
        Me.INDlycgUnique.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgUnique.Name = "INDlycgUnique"
        Me.INDlycgUnique.Size = New System.Drawing.Size(575, 582)
        '
        'INDlyciCode
        '
        Me.INDlyciCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyciCode, "INDlyciCode")
        Me.INDlyciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyciCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyciCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyciCode.Name = "INDlyciCode"
        Me.INDlyciCode.Size = New System.Drawing.Size(551, 36)
        Me.INDlyciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyciCode.TextToControlDistance = 12
        '
        'INDlyciName
        '
        Me.INDlyciName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyciName, "INDlyciName")
        Me.INDlyciName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyciName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyciName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyciName.Name = "INDlyciName"
        Me.INDlyciName.Size = New System.Drawing.Size(551, 36)
        Me.INDlyciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyciName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyciName.TextToControlDistance = 12
        '
        'INDlyciAux
        '
        Me.INDlyciAux.Control = Me.INDrgbAux
        resources.ApplyResources(Me.INDlyciAux, "INDlyciAux")
        Me.INDlyciAux.Location = New System.Drawing.Point(0, 72)
        Me.INDlyciAux.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyciAux.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyciAux.Name = "INDlyciAux"
        Me.INDlyciAux.Size = New System.Drawing.Size(551, 451)
        Me.INDlyciAux.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciAux.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyciAux.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyciAux.TextToControlDistance = 12
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControlPanel1, "CtrNavigationControlPanel1")
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlycRoot
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmAccountLevel
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmAccountLevel"
        Me.Opacity = 1.0R
        Me.Tag = "600"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycRoot.ResumeLayout(False)
        CType(Me.INDrgbAux.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgUnique, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciAux, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDrgbAux As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlycgUnique As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyciAux As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
End Class
