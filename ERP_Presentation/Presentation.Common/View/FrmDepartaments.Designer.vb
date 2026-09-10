<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDepartaments
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDepartaments))
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDBteDepCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyDepartamentos = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGleCountry = New Presentation.Controls.GridLookUpMultiFilter()
        Me.CustomGridView1 = New Presentation.Controls.CustomGridView()
        Me.CountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CountryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtDepName = New DevExpress.XtraEditors.TextEdit()
        Me.INDlygDepartments = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDgrDepatamentos = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDepCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDepName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlyItemCountryCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteDepCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyDepartamentos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyDepartamentos.SuspendLayout()
        CType(Me.INDGleCountry.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDepName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDepartments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrDepatamentos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDepCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDepName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCountryCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyDepartamentos)
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
        'INDBteDepCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteDepCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteDepCode, True)
        resources.ApplyResources(Me.INDBteDepCode, "INDBteDepCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteDepCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBteDepCode.Name = "INDBteDepCode"
        Me.INDBteDepCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDBteDepCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDBteDepCode.Properties.Appearance.Font = CType(resources.GetObject("INDBteDepCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteDepCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteDepCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteDepCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDBteDepCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDBteDepCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDBteDepCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDBteDepCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteDepCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteDepCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteDepCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteDepCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBteDepCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteDepCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteDepCode.Properties.Buttons1"), CType(resources.GetObject("INDBteDepCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteDepCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteDepCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteDepCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDBteDepCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Common.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDBteDepCode.Properties.Buttons7"), CType(resources.GetObject("INDBteDepCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDBteDepCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteDepCode.Properties.Buttons10"), Boolean))})
        Me.INDBteDepCode.Properties.Mask.EditMask = resources.GetString("INDBteDepCode.Properties.Mask.EditMask")
        Me.INDBteDepCode.Properties.Mask.MaskType = CType(resources.GetObject("INDBteDepCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteDepCode.Properties.MaxLength = 2
        Me.INDBteDepCode.StyleController = Me.INDlyDepartamentos
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteDepCode, 0)
        '
        'INDlyDepartamentos
        '
        Me.INDlyDepartamentos.AllowCustomization = False
        Me.INDlyDepartamentos.Controls.Add(Me.INDGleCountry)
        Me.INDlyDepartamentos.Controls.Add(Me.INDTxtDepName)
        Me.INDlyDepartamentos.Controls.Add(Me.INDBteDepCode)
        resources.ApplyResources(Me.INDlyDepartamentos, "INDlyDepartamentos")
        Me.LayoutControls.SetIsCustomizable(Me.INDlyDepartamentos, False)
        Me.INDlyDepartamentos.Name = "INDlyDepartamentos"
        Me.INDlyDepartamentos.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(949, 208, 250, 350)
        Me.INDlyDepartamentos.Root = Me.INDlygDepartments
        '
        'INDGleCountry
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleCountry, True)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleCountry, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleCountry, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleCountry, False)
        resources.ApplyResources(Me.INDGleCountry, "INDGleCountry")
        Me.IndigoTextEdit1.SetMascara(Me.INDGleCountry, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleCountry.Name = "INDGleCountry"
        Me.INDGleCountry.Properties.Appearance.BackColor = CType(resources.GetObject("INDGleCountry.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDGleCountry.Properties.Appearance.Font = CType(resources.GetObject("INDGleCountry.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleCountry.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleCountry.Properties.Appearance.Options.UseFont = True
        Me.INDGleCountry.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDGleCountry.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDGleCountry.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDGleCountry.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDGleCountry.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGleCountry.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGleCountry.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleCountry.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleCountry.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleCountry.Properties.AutoComplete = False
        Me.INDGleCountry.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGleCountry.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGleCountry.Properties.DisplayMember = "Name"
        Me.INDGleCountry.Properties.ImmediatePopup = True
        Me.INDGleCountry.Properties.NullText = resources.GetString("INDGleCountry.Properties.NullText")
        Me.INDGleCountry.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDGleCountry.Properties.ValueMember = "Id"
        Me.INDGleCountry.Properties.View = Me.CustomGridView1
        Me.INDGleCountry.StyleController = Me.INDlyDepartamentos
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleCountry, "510")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleCountry, 0)
        '
        'CustomGridView1
        '
        Me.CustomGridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.CustomGridView1.Appearance.Row.Font = CType(resources.GetObject("CustomGridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.Row.Options.UseFont = True
        Me.CustomGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CountryCode, Me.CountryName})
        Me.CustomGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CustomGridView1.Name = "CustomGridView1"
        Me.CustomGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CustomGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.CustomGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.CustomGridView1.OptionsView.ShowGroupPanel = False
        '
        'CountryCode
        '
        resources.ApplyResources(Me.CountryCode, "CountryCode")
        Me.CountryCode.FieldName = "Code"
        Me.CountryCode.Name = "CountryCode"
        '
        'CountryName
        '
        resources.ApplyResources(Me.CountryName, "CountryName")
        Me.CountryName.FieldName = "Name"
        Me.CountryName.Name = "CountryName"
        '
        'INDTxtDepName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDepName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDepName, True)
        resources.ApplyResources(Me.INDTxtDepName, "INDTxtDepName")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDepName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtDepName.Name = "INDTxtDepName"
        Me.INDTxtDepName.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtDepName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtDepName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtDepName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtDepName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtDepName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDepName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtDepName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtDepName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtDepName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtDepName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtDepName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtDepName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtDepName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtDepName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtDepName.Properties.Mask.EditMask = resources.GetString("INDTxtDepName.Properties.Mask.EditMask")
        Me.INDTxtDepName.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtDepName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtDepName.Properties.MaxLength = 50
        Me.INDTxtDepName.StyleController = Me.INDlyDepartamentos
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDepName, 0)
        '
        'INDlygDepartments
        '
        resources.ApplyResources(Me.INDlygDepartments, "INDlygDepartments")
        Me.INDlygDepartments.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlygDepartments.GroupBordersVisible = False
        Me.INDlygDepartments.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDgrDepatamentos})
        Me.INDlygDepartments.Location = New System.Drawing.Point(0, 0)
        Me.INDlygDepartments.Name = "INDlygDepartments"
        Me.INDlygDepartments.Size = New System.Drawing.Size(714, 330)
        Me.INDlygDepartments.TextVisible = False
        '
        'INDgrDepatamentos
        '
        Me.INDgrDepatamentos.AppearanceGroup.Font = CType(resources.GetObject("INDgrDepatamentos.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDgrDepatamentos.AppearanceGroup.Options.UseFont = True
        Me.INDgrDepatamentos.AppearanceItemCaption.Font = CType(resources.GetObject("INDgrDepatamentos.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDgrDepatamentos.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDgrDepatamentos, "INDgrDepatamentos")
        Me.INDgrDepatamentos.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDepCode, Me.INDlyItemDepName, Me.EmptySpaceItem1, Me.INDlyItemCountryCode})
        Me.INDgrDepatamentos.Location = New System.Drawing.Point(0, 0)
        Me.INDgrDepatamentos.Name = "INDgrDepatamentos"
        Me.INDgrDepatamentos.Size = New System.Drawing.Size(694, 310)
        '
        'INDlyItemDepCode
        '
        Me.INDlyItemDepCode.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyItemDepCode.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyItemDepCode.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemDepCode.Control = Me.INDBteDepCode
        resources.ApplyResources(Me.INDlyItemDepCode, "INDlyItemDepCode")
        Me.INDlyItemDepCode.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemDepCode.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemDepCode.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemDepCode.Name = "INDlyItemDepCode"
        Me.INDlyItemDepCode.ShowInCustomizationForm = False
        Me.INDlyItemDepCode.Size = New System.Drawing.Size(670, 36)
        Me.INDlyItemDepCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDepCode.Tag = "Code"
        Me.INDlyItemDepCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDepCode.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemDepCode.TextToControlDistance = 12
        '
        'INDlyItemDepName
        '
        Me.INDlyItemDepName.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyItemDepName.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyItemDepName.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemDepName.Control = Me.INDTxtDepName
        resources.ApplyResources(Me.INDlyItemDepName, "INDlyItemDepName")
        Me.INDlyItemDepName.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemDepName.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemDepName.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemDepName.Name = "INDlyItemDepName"
        Me.INDlyItemDepName.Size = New System.Drawing.Size(670, 36)
        Me.INDlyItemDepName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDepName.Tag = "Name"
        Me.INDlyItemDepName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDepName.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemDepName.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 108)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.ShowInCustomizationForm = False
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(670, 143)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlyItemCountryCode
        '
        Me.INDlyItemCountryCode.Control = Me.INDGleCountry
        resources.ApplyResources(Me.INDlyItemCountryCode, "INDlyItemCountryCode")
        Me.INDlyItemCountryCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCountryCode.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemCountryCode.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemCountryCode.Name = "INDlyItemCountryCode"
        Me.INDlyItemCountryCode.Size = New System.Drawing.Size(670, 36)
        Me.INDlyItemCountryCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCountryCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCountryCode.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemCountryCode.TextToControlDistance = 12
        '
        'FrmDepartaments
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmDepartaments"
        Me.Opacity = 1.0R
        Me.Tag = "510"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteDepCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyDepartamentos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyDepartamentos.ResumeLayout(False)
        CType(Me.INDGleCountry.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDepName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDepartments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrDepatamentos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDepCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDepName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCountryCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlyDepartamentos As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtDepName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteDepCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlygDepartments As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgrDepatamentos As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemDepCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDepName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDGleCountry As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents CustomGridView1 As Presentation.Controls.CustomGridView
    Friend WithEvents INDlyItemCountryCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CountryName As DevExpress.XtraGrid.Columns.GridColumn
End Class
