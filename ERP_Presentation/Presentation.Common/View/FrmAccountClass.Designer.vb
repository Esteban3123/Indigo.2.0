Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountClass
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccountClass))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDrgbPatrimony = New DevExpress.XtraEditors.RadioGroup()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDgleNature = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgleType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlycgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlycgUnique = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyciPatrimony = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyNature = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycRoot.SuspendLayout()
        CType(Me.INDrgbPatrimony.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleNature.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgUnique, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciPatrimony, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
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
        'INDlycRoot
        '
        Me.INDlycRoot.AllowCustomization = False
        Me.INDlycRoot.Controls.Add(Me.INDrgbPatrimony)
        Me.INDlycRoot.Controls.Add(Me.INDtxtName)
        Me.INDlycRoot.Controls.Add(Me.INDbteCode)
        Me.INDlycRoot.Controls.Add(Me.INDgleNature)
        Me.INDlycRoot.Controls.Add(Me.INDgleType)
        resources.ApplyResources(Me.INDlycRoot, "INDlycRoot")
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, False)
        Me.INDlycRoot.Name = "INDlycRoot"
        Me.INDlycRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(960, 158, 775, 571)
        Me.INDlycRoot.Root = Me.INDlycgRoot
        '
        'INDrgbPatrimony
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgbPatrimony, False)
        Me.INDrgbPatrimony.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDrgbPatrimony, "INDrgbPatrimony")
        Me.INDrgbPatrimony.Name = "INDrgbPatrimony"
        Me.INDrgbPatrimony.Properties.Appearance.BackColor = CType(resources.GetObject("INDrgbPatrimony.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDrgbPatrimony.Properties.Appearance.Font = CType(resources.GetObject("INDrgbPatrimony.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDrgbPatrimony.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgbPatrimony.Properties.Appearance.Options.UseFont = True
        Me.INDrgbPatrimony.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDrgbPatrimony.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDrgbPatrimony.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDrgbPatrimony.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDrgbPatrimony.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDrgbPatrimony.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDrgbPatrimony.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgbPatrimony.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDrgbPatrimony.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgbPatrimony.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDrgbPatrimony.Properties.Items"), Object), resources.GetString("INDrgbPatrimony.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDrgbPatrimony.Properties.Items2"), Object), resources.GetString("INDrgbPatrimony.Properties.Items3"))})
        Me.INDrgbPatrimony.StyleController = Me.INDlycRoot
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.StyleController = Me.INDlycRoot
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Common.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteCode.Properties.Buttons7"), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons10"), Boolean))})
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.StyleController = Me.INDlycRoot
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDgleNature
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleNature, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleNature, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleNature, False)
        Me.INDgleNature.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleNature, False)
        resources.ApplyResources(Me.INDgleNature, "INDgleNature")
        Me.IndigoTextEdit1.SetMascara(Me.INDgleNature, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleNature.Name = "INDgleNature"
        Me.INDgleNature.Properties.Appearance.BackColor = CType(resources.GetObject("INDgleNature.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDgleNature.Properties.Appearance.Font = CType(resources.GetObject("INDgleNature.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDgleNature.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleNature.Properties.Appearance.Options.UseFont = True
        Me.INDgleNature.Properties.AppearanceDropDown.Font = CType(resources.GetObject("INDgleNature.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.INDgleNature.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDgleNature.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDgleNature.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDgleNature.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDgleNature.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDgleNature.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDgleNature.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDgleNature.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleNature.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleNature.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleNature.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDgleNature.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDgleNature.Properties.DisplayMember = "Item2"
        Me.INDgleNature.Properties.ImmediatePopup = True
        Me.INDgleNature.Properties.NullText = resources.GetString("INDgleNature.Properties.NullText")
        Me.INDgleNature.Properties.PopupSizeable = False
        Me.INDgleNature.Properties.ValueMember = "Item1"
        Me.INDgleNature.Properties.View = Me.GridLookUpEdit1View
        Me.INDgleNature.StyleController = Me.INDlycRoot
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleNature, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleNature, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "Item1"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.OptionsColumn.ShowInCustomizationForm = False
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'INDgleType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleType, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleType, False)
        resources.ApplyResources(Me.INDgleType, "INDgleType")
        Me.IndigoTextEdit1.SetMascara(Me.INDgleType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleType.Name = "INDgleType"
        Me.INDgleType.Properties.Appearance.BackColor = CType(resources.GetObject("INDgleType.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDgleType.Properties.Appearance.Font = CType(resources.GetObject("INDgleType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDgleType.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleType.Properties.Appearance.Options.UseFont = True
        Me.INDgleType.Properties.AppearanceDropDown.Font = CType(resources.GetObject("INDgleType.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.INDgleType.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDgleType.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDgleType.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDgleType.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDgleType.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDgleType.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDgleType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDgleType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDgleType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDgleType.Properties.DisplayMember = "Item2"
        Me.INDgleType.Properties.ImmediatePopup = True
        Me.INDgleType.Properties.NullText = resources.GetString("INDgleType.Properties.NullText")
        Me.INDgleType.Properties.PopupSizeable = False
        Me.INDgleType.Properties.ValueMember = "Item1"
        Me.INDgleType.Properties.View = Me.GridView1
        Me.INDgleType.StyleController = Me.INDlycRoot
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleType, 0)
        '
        'GridView1
        '
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.FieldName = "Item1"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn6
        '
        resources.ApplyResources(Me.GridColumn6, "GridColumn6")
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        '
        'INDlycgRoot
        '
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
        Me.INDlycgRoot.Size = New System.Drawing.Size(560, 602)
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
        Me.INDlycgUnique.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyciCode, Me.INDlyciName, Me.INDlyciPatrimony, Me.INDlyNature, Me.INDlyType})
        Me.INDlycgUnique.Location = New System.Drawing.Point(0, 0)
        Me.INDlycgUnique.Name = "INDlycgUnique"
        Me.INDlycgUnique.Size = New System.Drawing.Size(540, 582)
        '
        'INDlyciCode
        '
        Me.INDlyciCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyciCode, "INDlyciCode")
        Me.INDlyciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyciCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyciCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyciCode.Name = "INDlyciCode"
        Me.INDlyciCode.ShowInCustomizationForm = False
        Me.INDlyciCode.Size = New System.Drawing.Size(516, 36)
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
        Me.INDlyciName.Size = New System.Drawing.Size(516, 36)
        Me.INDlyciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyciName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyciName.TextToControlDistance = 12
        '
        'INDlyciPatrimony
        '
        Me.INDlyciPatrimony.Control = Me.INDrgbPatrimony
        resources.ApplyResources(Me.INDlyciPatrimony, "INDlyciPatrimony")
        Me.INDlyciPatrimony.Location = New System.Drawing.Point(0, 72)
        Me.INDlyciPatrimony.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyciPatrimony.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyciPatrimony.Name = "INDlyciPatrimony"
        Me.INDlyciPatrimony.Size = New System.Drawing.Size(516, 36)
        Me.INDlyciPatrimony.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciPatrimony.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyciPatrimony.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyciPatrimony.TextToControlDistance = 12
        '
        'INDlyNature
        '
        Me.INDlyNature.Control = Me.INDgleNature
        resources.ApplyResources(Me.INDlyNature, "INDlyNature")
        Me.INDlyNature.Location = New System.Drawing.Point(0, 108)
        Me.INDlyNature.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyNature.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyNature.Name = "INDlyNature"
        Me.INDlyNature.Size = New System.Drawing.Size(516, 36)
        Me.INDlyNature.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyNature.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyNature.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyNature.TextToControlDistance = 12
        '
        'INDlyType
        '
        Me.INDlyType.Control = Me.INDgleType
        resources.ApplyResources(Me.INDlyType, "INDlyType")
        Me.INDlyType.Location = New System.Drawing.Point(0, 144)
        Me.INDlyType.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyType.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyType.Name = "INDlyType"
        Me.INDlyType.Size = New System.Drawing.Size(516, 379)
        Me.INDlyType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyType.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDlycRoot
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "Item1"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.ShowInCustomizationForm = False
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'FrmAccountClass
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAccountClass"
        Me.Opacity = 1.0R
        Me.Tag = "601"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycRoot.ResumeLayout(False)
        CType(Me.INDrgbPatrimony.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleNature.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgUnique, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciPatrimony, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDrgbPatrimony As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyciPatrimony As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlycgUnique As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyNature As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDgleNature As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDgleType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
End Class
