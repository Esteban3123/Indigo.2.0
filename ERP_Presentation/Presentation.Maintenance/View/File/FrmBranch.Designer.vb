<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBranch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBranch))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDLyCtrBranch = New DevExpress.XtraLayout.LayoutControl()
        Me.INDglCity = New Presentation.Controls.GridLookUpMultiFilter()
        Me.GridLookUpMultiFilter2View = New Presentation.Controls.CustomGridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDglDepartamentos = New Presentation.Controls.GridLookUpMultiFilter()
        Me.CustomGridView1 = New Presentation.Controls.CustomGridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyKinship = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrBranch = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCodeBranch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemNameBranch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDltItemDeparment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.DepartmentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DepartmentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCtrBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCtrBranch.SuspendLayout()
        CType(Me.INDglCity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpMultiFilter2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglDepartamentos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyKinship, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCodeBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemNameBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDltItemDeparment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCtrBranch)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        '
        'ToolBars
        '
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.FontSizeDelta = CType(resources.GetObject("ToolBars.Appearance.FontSizeDelta"), Integer)
        Me.ToolBars.Appearance.FontStyleDelta = CType(resources.GetObject("ToolBars.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.ToolBars.Appearance.GradientMode = CType(resources.GetObject("ToolBars.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.ToolBars.Appearance.Image = CType(resources.GetObject("ToolBars.Appearance.Image"), System.Drawing.Image)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        '
        'INDLyCtrBranch
        '
        resources.ApplyResources(Me.INDLyCtrBranch, "INDLyCtrBranch")
        Me.INDLyCtrBranch.AllowCustomization = False
        Me.INDLyCtrBranch.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("INDLyCtrBranch.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
        Me.INDLyCtrBranch.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("INDLyCtrBranch.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDLyCtrBranch.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("INDLyCtrBranch.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDLyCtrBranch.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("INDLyCtrBranch.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
        Me.INDLyCtrBranch.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("INDLyCtrBranch.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
        Me.INDLyCtrBranch.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("INDLyCtrBranch.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDLyCtrBranch.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("INDLyCtrBranch.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDLyCtrBranch.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("INDLyCtrBranch.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
        Me.INDLyCtrBranch.Controls.Add(Me.INDglCity)
        Me.INDLyCtrBranch.Controls.Add(Me.INDglDepartamentos)
        Me.INDLyCtrBranch.Controls.Add(Me.INDTxtName)
        Me.INDLyCtrBranch.Controls.Add(Me.INDBteCode)
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCtrBranch, False)
        Me.INDLyCtrBranch.Name = "INDLyCtrBranch"
        Me.INDLyCtrBranch.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 221, 250, 350)
        Me.INDLyCtrBranch.OptionsPrint.AppearanceGroupCaption.FontSizeDelta = CType(resources.GetObject("INDLyCtrBranch.OptionsPrint.AppearanceGroupCaption.FontSizeDelta"), Integer)
        Me.INDLyCtrBranch.OptionsPrint.AppearanceGroupCaption.FontStyleDelta = CType(resources.GetObject("INDLyCtrBranch.OptionsPrint.AppearanceGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDLyCtrBranch.OptionsPrint.AppearanceGroupCaption.GradientMode = CType(resources.GetObject("INDLyCtrBranch.OptionsPrint.AppearanceGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDLyCtrBranch.OptionsPrint.AppearanceGroupCaption.Image = CType(resources.GetObject("INDLyCtrBranch.OptionsPrint.AppearanceGroupCaption.Image"), System.Drawing.Image)
        Me.INDLyCtrBranch.OptionsPrint.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDLyCtrBranch.OptionsPrint.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDLyCtrBranch.OptionsPrint.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDLyCtrBranch.OptionsPrint.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDLyCtrBranch.OptionsPrint.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDLyCtrBranch.OptionsPrint.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDLyCtrBranch.OptionsPrint.AppearanceItemCaption.Image = CType(resources.GetObject("INDLyCtrBranch.OptionsPrint.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDLyCtrBranch.Root = Me.INDlyKinship
        '
        'INDglCity
        '
        resources.ApplyResources(Me.INDglCity, "INDglCity")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglCity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglCity, True)
        Me.INDglCity.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDglCity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglCity.Name = "INDglCity"
        Me.INDglCity.Properties.AccessibleDescription = resources.GetString("INDglCity.Properties.AccessibleDescription")
        Me.INDglCity.Properties.AccessibleName = resources.GetString("INDglCity.Properties.AccessibleName")
        Me.INDglCity.Properties.Appearance.BackColor = CType(resources.GetObject("INDglCity.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDglCity.Properties.Appearance.Font = CType(resources.GetObject("INDglCity.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglCity.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDglCity.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDglCity.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDglCity.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglCity.Properties.Appearance.GradientMode = CType(resources.GetObject("INDglCity.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglCity.Properties.Appearance.Image = CType(resources.GetObject("INDglCity.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDglCity.Properties.Appearance.Options.UseBackColor = True
        Me.INDglCity.Properties.Appearance.Options.UseFont = True
        Me.INDglCity.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDglCity.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDglCity.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDglCity.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDglCity.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglCity.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglCity.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDglCity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglCity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglCity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglCity.Properties.AutoComplete = False
        Me.INDglCity.Properties.AutoHeight = CType(resources.GetObject("INDglCity.Properties.AutoHeight"), Boolean)
        Me.INDglCity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglCity.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDglCity.Properties.DisplayMember = "Name"
        Me.INDglCity.Properties.NullText = resources.GetString("INDglCity.Properties.NullText")
        Me.INDglCity.Properties.NullValuePrompt = resources.GetString("INDglCity.Properties.NullValuePrompt")
        Me.INDglCity.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDglCity.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDglCity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDglCity.Properties.ValueMember = "Id"
        Me.INDglCity.Properties.View = Me.GridLookUpMultiFilter2View
        Me.INDglCity.StyleController = Me.INDLyCtrBranch
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglCity, 0)
        '
        'GridLookUpMultiFilter2View
        '
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.FontSizeDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.FontSizeDelta"), Integer)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.FontStyleDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.GradientMode = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Image = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.Image"), System.Drawing.Image)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.GradientMode = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Image = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Image = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.Row.FontSizeDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.FontSizeDelta"), Integer)
        Me.GridLookUpMultiFilter2View.Appearance.Row.FontStyleDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpMultiFilter2View.Appearance.Row.GradientMode = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpMultiFilter2View.Appearance.Row.Image = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.Image"), System.Drawing.Image)
        Me.GridLookUpMultiFilter2View.Appearance.Row.Options.UseFont = True
        resources.ApplyResources(Me.GridLookUpMultiFilter2View, "GridLookUpMultiFilter2View")
        Me.GridLookUpMultiFilter2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridLookUpMultiFilter2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpMultiFilter2View.Name = "GridLookUpMultiFilter2View"
        Me.GridLookUpMultiFilter2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpMultiFilter2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpMultiFilter2View, False)
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'INDglDepartamentos
        '
        resources.ApplyResources(Me.INDglDepartamentos, "INDglDepartamentos")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglDepartamentos, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglDepartamentos, True)
        Me.INDglDepartamentos.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDglDepartamentos, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglDepartamentos.Name = "INDglDepartamentos"
        Me.INDglDepartamentos.Properties.AccessibleDescription = resources.GetString("INDglDepartamentos.Properties.AccessibleDescription")
        Me.INDglDepartamentos.Properties.AccessibleName = resources.GetString("INDglDepartamentos.Properties.AccessibleName")
        Me.INDglDepartamentos.Properties.Appearance.BackColor = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDglDepartamentos.Properties.Appearance.Font = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglDepartamentos.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDglDepartamentos.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglDepartamentos.Properties.Appearance.GradientMode = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglDepartamentos.Properties.Appearance.Image = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDglDepartamentos.Properties.Appearance.Options.UseBackColor = True
        Me.INDglDepartamentos.Properties.Appearance.Options.UseFont = True
        Me.INDglDepartamentos.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDglDepartamentos.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDglDepartamentos.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDglDepartamentos.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDglDepartamentos.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglDepartamentos.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglDepartamentos.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDglDepartamentos.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglDepartamentos.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglDepartamentos.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglDepartamentos.Properties.AutoComplete = False
        Me.INDglDepartamentos.Properties.AutoHeight = CType(resources.GetObject("INDglDepartamentos.Properties.AutoHeight"), Boolean)
        Me.INDglDepartamentos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglDepartamentos.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDglDepartamentos.Properties.DisplayMember = "Name"
        Me.INDglDepartamentos.Properties.NullText = resources.GetString("INDglDepartamentos.Properties.NullText")
        Me.INDglDepartamentos.Properties.NullValuePrompt = resources.GetString("INDglDepartamentos.Properties.NullValuePrompt")
        Me.INDglDepartamentos.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDglDepartamentos.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDglDepartamentos.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDglDepartamentos.Properties.ValueMember = "Id"
        Me.INDglDepartamentos.Properties.View = Me.CustomGridView1
        Me.INDglDepartamentos.StyleController = Me.INDLyCtrBranch
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglDepartamentos, 0)
        '
        'CustomGridView1
        '
        Me.CustomGridView1.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.CustomGridView1.Appearance.FocusedRow.Font = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.FocusedRow.FontSizeDelta = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.FontSizeDelta"), Integer)
        Me.CustomGridView1.Appearance.FocusedRow.FontStyleDelta = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.CustomGridView1.Appearance.FocusedRow.GradientMode = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.CustomGridView1.Appearance.FocusedRow.Image = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.Image"), System.Drawing.Image)
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.CustomGridView1.Appearance.GroupRow.Font = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.CustomGridView1.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.CustomGridView1.Appearance.GroupRow.GradientMode = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.CustomGridView1.Appearance.GroupRow.Image = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.CustomGridView1.Appearance.GroupRow.Options.UseFont = True
        Me.CustomGridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.CustomGridView1.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.CustomGridView1.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.CustomGridView1.Appearance.HeaderPanel.Image = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.CustomGridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.CustomGridView1.Appearance.Row.Font = CType(resources.GetObject("CustomGridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.Row.FontSizeDelta = CType(resources.GetObject("CustomGridView1.Appearance.Row.FontSizeDelta"), Integer)
        Me.CustomGridView1.Appearance.Row.FontStyleDelta = CType(resources.GetObject("CustomGridView1.Appearance.Row.FontStyleDelta"), System.Drawing.FontStyle)
        Me.CustomGridView1.Appearance.Row.GradientMode = CType(resources.GetObject("CustomGridView1.Appearance.Row.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.CustomGridView1.Appearance.Row.Image = CType(resources.GetObject("CustomGridView1.Appearance.Row.Image"), System.Drawing.Image)
        Me.CustomGridView1.Appearance.Row.Options.UseFont = True
        resources.ApplyResources(Me.CustomGridView1, "CustomGridView1")
        Me.CustomGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6})
        Me.CustomGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CustomGridView1.Name = "CustomGridView1"
        Me.CustomGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CustomGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.CustomGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.CustomGridView1.OptionsView.ShowAutoFilterRow = True
        Me.CustomGridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CustomGridView1, False)
        '
        'GridColumn5
        '
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'GridColumn6
        '
        resources.ApplyResources(Me.GridColumn6, "GridColumn6")
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'INDTxtName
        '
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.AccessibleDescription = resources.GetString("INDTxtName.Properties.AccessibleDescription")
        Me.INDTxtName.Properties.AccessibleName = resources.GetString("INDTxtName.Properties.AccessibleName")
        Me.INDTxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDTxtName.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDTxtName.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDTxtName.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDTxtName.Properties.Appearance.GradientMode = CType(resources.GetObject("INDTxtName.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDTxtName.Properties.Appearance.Image = CType(resources.GetObject("INDTxtName.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDTxtName.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDTxtName.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDTxtName.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.AutoHeight = CType(resources.GetObject("INDTxtName.Properties.AutoHeight"), Boolean)
        Me.INDTxtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.INDTxtName.Properties.Mask.AutoComplete = CType(resources.GetObject("INDTxtName.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDTxtName.Properties.Mask.BeepOnError = CType(resources.GetObject("INDTxtName.Properties.Mask.BeepOnError"), Boolean)
        Me.INDTxtName.Properties.Mask.EditMask = resources.GetString("INDTxtName.Properties.Mask.EditMask")
        Me.INDTxtName.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDTxtName.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDTxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtName.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDTxtName.Properties.Mask.PlaceHolder"), Char)
        Me.INDTxtName.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDTxtName.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDTxtName.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDTxtName.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDTxtName.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDTxtName.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDTxtName.Properties.MaxLength = 50
        Me.INDTxtName.Properties.NullValuePrompt = resources.GetString("INDTxtName.Properties.NullValuePrompt")
        Me.INDTxtName.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDTxtName.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDTxtName.StyleController = Me.INDLyCtrBranch
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        '
        'INDBteCode
        '
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.AccessibleDescription = resources.GetString("INDBteCode.Properties.AccessibleDescription")
        Me.INDBteCode.Properties.AccessibleName = resources.GetString("INDBteCode.Properties.AccessibleName")
        Me.INDBteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDBteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.Appearance.Font = CType(resources.GetObject("INDBteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDBteCode.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDBteCode.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDBteCode.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDBteCode.Properties.Appearance.GradientMode = CType(resources.GetObject("INDBteCode.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDBteCode.Properties.Appearance.Image = CType(resources.GetObject("INDBteCode.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDBteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDBteCode.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDBteCode.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDBteCode.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBteCode.Properties.AutoHeight = CType(resources.GetObject("INDBteCode.Properties.AutoHeight"), Boolean)
        resources.ApplyResources(SerializableAppearanceObject1, "SerializableAppearanceObject1")
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDBteCode.Properties.Buttons7"), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons10"), Boolean))})
        Me.INDBteCode.Properties.Mask.AutoComplete = CType(resources.GetObject("INDBteCode.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDBteCode.Properties.Mask.BeepOnError = CType(resources.GetObject("INDBteCode.Properties.Mask.BeepOnError"), Boolean)
        Me.INDBteCode.Properties.Mask.EditMask = resources.GetString("INDBteCode.Properties.Mask.EditMask")
        Me.INDBteCode.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDBteCode.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDBteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDBteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteCode.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDBteCode.Properties.Mask.PlaceHolder"), Char)
        Me.INDBteCode.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDBteCode.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDBteCode.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDBteCode.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDBteCode.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDBteCode.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.Properties.NullValuePrompt = resources.GetString("INDBteCode.Properties.NullValuePrompt")
        Me.INDBteCode.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDBteCode.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDBteCode.StyleController = Me.INDLyCtrBranch
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDlyKinship
        '
        resources.ApplyResources(Me.INDlyKinship, "INDlyKinship")
        Me.INDlyKinship.GroupBordersVisible = False
        Me.INDlyKinship.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrBranch})
        Me.INDlyKinship.Location = New System.Drawing.Point(0, 0)
        Me.INDlyKinship.Name = "INDlyKinship"
        Me.INDlyKinship.Size = New System.Drawing.Size(804, 598)
        Me.INDlyKinship.TextVisible = False
        '
        'INDGrBranch
        '
        Me.INDGrBranch.AppearanceGroup.Font = CType(resources.GetObject("INDGrBranch.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDGrBranch.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("INDGrBranch.AppearanceGroup.FontSizeDelta"), Integer)
        Me.INDGrBranch.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("INDGrBranch.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDGrBranch.AppearanceGroup.GradientMode = CType(resources.GetObject("INDGrBranch.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDGrBranch.AppearanceGroup.Image = CType(resources.GetObject("INDGrBranch.AppearanceGroup.Image"), System.Drawing.Image)
        Me.INDGrBranch.AppearanceGroup.Options.UseFont = True
        Me.INDGrBranch.AppearanceItemCaption.Font = CType(resources.GetObject("INDGrBranch.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDGrBranch.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDGrBranch.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDGrBranch.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDGrBranch.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDGrBranch.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDGrBranch.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDGrBranch.AppearanceItemCaption.Image = CType(resources.GetObject("INDGrBranch.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDGrBranch.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDGrBranch, "INDGrBranch")
        Me.INDGrBranch.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCodeBranch, Me.INDLyItemNameBranch, Me.INDltItemDeparment, Me.INDlyItemCity})
        Me.INDGrBranch.Location = New System.Drawing.Point(0, 0)
        Me.INDGrBranch.Name = "INDGrBranch"
        Me.INDGrBranch.Size = New System.Drawing.Size(804, 598)
        '
        'INDlyItemCodeBranch
        '
        resources.ApplyResources(Me.INDlyItemCodeBranch, "INDlyItemCodeBranch")
        Me.INDlyItemCodeBranch.Control = Me.INDBteCode
        Me.INDlyItemCodeBranch.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCodeBranch.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodeBranch.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCodeBranch.Name = "INDlyItemCodeBranch"
        Me.INDlyItemCodeBranch.ShowInCustomizationForm = False
        Me.INDlyItemCodeBranch.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemCodeBranch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCodeBranch.Tag = "Code"
        Me.INDlyItemCodeBranch.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCodeBranch.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCodeBranch.TextToControlDistance = 12
        '
        'INDLyItemNameBranch
        '
        resources.ApplyResources(Me.INDLyItemNameBranch, "INDLyItemNameBranch")
        Me.INDLyItemNameBranch.Control = Me.INDTxtName
        Me.INDLyItemNameBranch.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemNameBranch.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDLyItemNameBranch.MinSize = New System.Drawing.Size(420, 36)
        Me.INDLyItemNameBranch.Name = "INDLyItemNameBranch"
        Me.INDLyItemNameBranch.ShowInCustomizationForm = False
        Me.INDLyItemNameBranch.Size = New System.Drawing.Size(780, 36)
        Me.INDLyItemNameBranch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemNameBranch.Tag = "Name"
        Me.INDLyItemNameBranch.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemNameBranch.TextSize = New System.Drawing.Size(160, 21)
        Me.INDLyItemNameBranch.TextToControlDistance = 12
        '
        'INDltItemDeparment
        '
        resources.ApplyResources(Me.INDltItemDeparment, "INDltItemDeparment")
        Me.INDltItemDeparment.Control = Me.INDglDepartamentos
        Me.INDltItemDeparment.Location = New System.Drawing.Point(0, 72)
        Me.INDltItemDeparment.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDltItemDeparment.MinSize = New System.Drawing.Size(420, 36)
        Me.INDltItemDeparment.Name = "INDltItemDeparment"
        Me.INDltItemDeparment.ShowInCustomizationForm = False
        Me.INDltItemDeparment.Size = New System.Drawing.Size(780, 36)
        Me.INDltItemDeparment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDltItemDeparment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDltItemDeparment.TextSize = New System.Drawing.Size(160, 21)
        Me.INDltItemDeparment.TextToControlDistance = 12
        '
        'INDlyItemCity
        '
        resources.ApplyResources(Me.INDlyItemCity, "INDlyItemCity")
        Me.INDlyItemCity.Control = Me.INDglCity
        Me.INDlyItemCity.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemCity.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCity.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCity.Name = "INDlyItemCity"
        Me.INDlyItemCity.ShowInCustomizationForm = False
        Me.INDlyItemCity.Size = New System.Drawing.Size(780, 431)
        Me.INDlyItemCity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCity.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCity.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyCtrBranch
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'DepartmentCode
        '
        resources.ApplyResources(Me.DepartmentCode, "DepartmentCode")
        Me.DepartmentCode.FieldName = "Code"
        Me.DepartmentCode.Name = "DepartmentCode"
        Me.DepartmentCode.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'DepartmentName
        '
        resources.ApplyResources(Me.DepartmentName, "DepartmentName")
        Me.DepartmentName.FieldName = "Name"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'FrmBranch
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "FrmBranch"
        Me.Opacity = 1.0R
        Me.Tag = "563"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCtrBranch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCtrBranch.ResumeLayout(False)
        CType(Me.INDglCity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpMultiFilter2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglDepartamentos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyKinship, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCodeBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemNameBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDltItemDeparment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCtrBranch As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyKinship As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGrBranch As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCodeBranch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemNameBranch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents INDglDepartamentos As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents CustomGridView1 As Presentation.Controls.CustomGridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDltItemDeparment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDglCity As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents GridLookUpMultiFilter2View As Presentation.Controls.CustomGridView
    Friend WithEvents DepartmentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DepartmentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemCity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
End Class
