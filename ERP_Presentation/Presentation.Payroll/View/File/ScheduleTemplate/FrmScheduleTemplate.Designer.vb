Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmScheduleTemplate
    Inherits FormBase

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmScheduleTemplate))
        Dim TimeRuler1 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler2 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDLcScheduleTemplate = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGlueEmpresa = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGcCompanyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcCompanyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDCmbSchedule = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.INDGcUnidad = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDChkTodos = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcSede = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcUnidadFuncional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtGroupName = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDScheduleControl = New DevExpress.XtraScheduler.SchedulerControl()
        Me.SchedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
        Me.INDTxtCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDTxtGroupCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.lycgScheduleTemplate = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcItemSchedule = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lycgSchedule = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcItemCodigo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcItemDescripcion = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcItemGrupo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lycItemGroupName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcItemHorario = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lycgFunctionalUnit = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcItemUnidadFuncional = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLclItemEmpresa = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcScheduleTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcScheduleTemplate.SuspendLayout()
        CType(Me.INDGlueEmpresa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCmbSchedule.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcUnidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtGroupName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDScheduleControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtGroupCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lycgScheduleTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcItemSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lycgSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcItemCodigo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcItemDescripcion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcItemGrupo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lycItemGroupName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcItemHorario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lycgFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcItemUnidadFuncional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLclItemEmpresa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.INDLcScheduleTemplate)
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
        'RepositoryItemCheckEdit1
        '
        resources.ApplyResources(Me.RepositoryItemCheckEdit1, "RepositoryItemCheckEdit1")
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDLcScheduleTemplate
        '
        resources.ApplyResources(Me.INDLcScheduleTemplate, "INDLcScheduleTemplate")
        Me.INDLcScheduleTemplate.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("INDLcScheduleTemplate.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
        Me.INDLcScheduleTemplate.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("INDLcScheduleTemplate.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDLcScheduleTemplate.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("INDLcScheduleTemplate.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDLcScheduleTemplate.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("INDLcScheduleTemplate.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
        Me.INDLcScheduleTemplate.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("INDLcScheduleTemplate.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
        Me.INDLcScheduleTemplate.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("INDLcScheduleTemplate.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDLcScheduleTemplate.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("INDLcScheduleTemplate.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDLcScheduleTemplate.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("INDLcScheduleTemplate.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
        Me.INDLcScheduleTemplate.Controls.Add(Me.INDGlueEmpresa)
        Me.INDLcScheduleTemplate.Controls.Add(Me.INDCmbSchedule)
        Me.INDLcScheduleTemplate.Controls.Add(Me.INDGcUnidad)
        Me.INDLcScheduleTemplate.Controls.Add(Me.INDTxtGroupName)
        Me.INDLcScheduleTemplate.Controls.Add(Me.INDTxtName)
        Me.INDLcScheduleTemplate.Controls.Add(Me.INDScheduleControl)
        Me.INDLcScheduleTemplate.Controls.Add(Me.INDTxtCode)
        Me.INDLcScheduleTemplate.Controls.Add(Me.INDTxtGroupCode)
        Me.INDLcScheduleTemplate.Name = "INDLcScheduleTemplate"
        Me.INDLcScheduleTemplate.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(549, 276, 416, 500)
        Me.INDLcScheduleTemplate.OptionsPrint.AppearanceGroupCaption.FontSizeDelta = CType(resources.GetObject("INDLcScheduleTemplate.OptionsPrint.AppearanceGroupCaption.FontSizeDelta"), Integer)
        Me.INDLcScheduleTemplate.OptionsPrint.AppearanceGroupCaption.FontStyleDelta = CType(resources.GetObject("INDLcScheduleTemplate.OptionsPrint.AppearanceGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDLcScheduleTemplate.OptionsPrint.AppearanceGroupCaption.GradientMode = CType(resources.GetObject("INDLcScheduleTemplate.OptionsPrint.AppearanceGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDLcScheduleTemplate.OptionsPrint.AppearanceGroupCaption.Image = CType(resources.GetObject("INDLcScheduleTemplate.OptionsPrint.AppearanceGroupCaption.Image"), System.Drawing.Image)
        Me.INDLcScheduleTemplate.OptionsPrint.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDLcScheduleTemplate.OptionsPrint.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDLcScheduleTemplate.OptionsPrint.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDLcScheduleTemplate.OptionsPrint.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDLcScheduleTemplate.OptionsPrint.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDLcScheduleTemplate.OptionsPrint.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDLcScheduleTemplate.OptionsPrint.AppearanceItemCaption.Image = CType(resources.GetObject("INDLcScheduleTemplate.OptionsPrint.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDLcScheduleTemplate.Root = Me.lycgScheduleTemplate
        '
        'INDGlueEmpresa
        '
        resources.ApplyResources(Me.INDGlueEmpresa, "INDGlueEmpresa")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGlueEmpresa, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGlueEmpresa, False)
        Me.INDGlueEmpresa.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDGlueEmpresa, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGlueEmpresa.Name = "INDGlueEmpresa"
        Me.INDGlueEmpresa.Properties.AccessibleDescription = resources.GetString("INDGlueEmpresa.Properties.AccessibleDescription")
        Me.INDGlueEmpresa.Properties.AccessibleName = resources.GetString("INDGlueEmpresa.Properties.AccessibleName")
        Me.INDGlueEmpresa.Properties.Appearance.BackColor = CType(resources.GetObject("INDGlueEmpresa.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDGlueEmpresa.Properties.Appearance.Font = CType(resources.GetObject("INDGlueEmpresa.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGlueEmpresa.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDGlueEmpresa.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDGlueEmpresa.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDGlueEmpresa.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDGlueEmpresa.Properties.Appearance.GradientMode = CType(resources.GetObject("INDGlueEmpresa.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDGlueEmpresa.Properties.Appearance.Image = CType(resources.GetObject("INDGlueEmpresa.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDGlueEmpresa.Properties.Appearance.Options.UseBackColor = True
        Me.INDGlueEmpresa.Properties.Appearance.Options.UseFont = True
        Me.INDGlueEmpresa.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDGlueEmpresa.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDGlueEmpresa.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDGlueEmpresa.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDGlueEmpresa.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGlueEmpresa.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGlueEmpresa.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDGlueEmpresa.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDGlueEmpresa.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDGlueEmpresa.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDGlueEmpresa.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDGlueEmpresa.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDGlueEmpresa.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDGlueEmpresa.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDGlueEmpresa.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGlueEmpresa.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGlueEmpresa.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGlueEmpresa.Properties.AutoHeight = CType(resources.GetObject("INDGlueEmpresa.Properties.AutoHeight"), Boolean)
        Me.INDGlueEmpresa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGlueEmpresa.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGlueEmpresa.Properties.DisplayMember = "Name"
        Me.INDGlueEmpresa.Properties.ImmediatePopup = True
        Me.INDGlueEmpresa.Properties.NullText = resources.GetString("INDGlueEmpresa.Properties.NullText")
        Me.INDGlueEmpresa.Properties.NullValuePrompt = resources.GetString("INDGlueEmpresa.Properties.NullValuePrompt")
        Me.INDGlueEmpresa.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDGlueEmpresa.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDGlueEmpresa.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.INDGlueEmpresa.Properties.ValueMember = "Code"
        Me.INDGlueEmpresa.Properties.View = Me.GridLookUpEdit1View
        Me.INDGlueEmpresa.StyleController = Me.INDLcScheduleTemplate
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGlueEmpresa, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.FontSizeDelta = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.FontSizeDelta"), Integer)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.FontStyleDelta = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.GradientMode = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Image = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.Image"), System.Drawing.Image)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("GridLookUpEdit1View.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.GridLookUpEdit1View.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("GridLookUpEdit1View.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpEdit1View.Appearance.GroupRow.GradientMode = CType(resources.GetObject("GridLookUpEdit1View.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpEdit1View.Appearance.GroupRow.Image = CType(resources.GetObject("GridLookUpEdit1View.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("GridLookUpEdit1View.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("GridLookUpEdit1View.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("GridLookUpEdit1View.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Image = CType(resources.GetObject("GridLookUpEdit1View.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.Row.FontSizeDelta = CType(resources.GetObject("GridLookUpEdit1View.Appearance.Row.FontSizeDelta"), Integer)
        Me.GridLookUpEdit1View.Appearance.Row.FontStyleDelta = CType(resources.GetObject("GridLookUpEdit1View.Appearance.Row.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpEdit1View.Appearance.Row.GradientMode = CType(resources.GetObject("GridLookUpEdit1View.Appearance.Row.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpEdit1View.Appearance.Row.Image = CType(resources.GetObject("GridLookUpEdit1View.Appearance.Row.Image"), System.Drawing.Image)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        resources.ApplyResources(Me.GridLookUpEdit1View, "GridLookUpEdit1View")
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcCompanyCode, Me.INDGcCompanyName})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDGcCompanyCode
        '
        resources.ApplyResources(Me.INDGcCompanyCode, "INDGcCompanyCode")
        Me.INDGcCompanyCode.FieldName = "Nit"
        Me.INDGcCompanyCode.Name = "INDGcCompanyCode"
        Me.INDGcCompanyCode.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'INDGcCompanyName
        '
        resources.ApplyResources(Me.INDGcCompanyName, "INDGcCompanyName")
        Me.INDGcCompanyName.FieldName = "Name"
        Me.INDGcCompanyName.Name = "INDGcCompanyName"
        Me.INDGcCompanyName.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'INDCmbSchedule
        '
        resources.ApplyResources(Me.INDCmbSchedule, "INDCmbSchedule")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCmbSchedule, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCmbSchedule, False)
        Me.INDCmbSchedule.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDCmbSchedule, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDCmbSchedule.Name = "INDCmbSchedule"
        Me.INDCmbSchedule.Properties.AccessibleDescription = resources.GetString("INDCmbSchedule.Properties.AccessibleDescription")
        Me.INDCmbSchedule.Properties.AccessibleName = resources.GetString("INDCmbSchedule.Properties.AccessibleName")
        Me.INDCmbSchedule.Properties.Appearance.BackColor = CType(resources.GetObject("INDCmbSchedule.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDCmbSchedule.Properties.Appearance.Font = CType(resources.GetObject("INDCmbSchedule.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDCmbSchedule.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDCmbSchedule.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDCmbSchedule.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDCmbSchedule.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDCmbSchedule.Properties.Appearance.GradientMode = CType(resources.GetObject("INDCmbSchedule.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDCmbSchedule.Properties.Appearance.Image = CType(resources.GetObject("INDCmbSchedule.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDCmbSchedule.Properties.Appearance.Options.UseBackColor = True
        Me.INDCmbSchedule.Properties.Appearance.Options.UseFont = True
        Me.INDCmbSchedule.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDCmbSchedule.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDCmbSchedule.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDCmbSchedule.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDCmbSchedule.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDCmbSchedule.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDCmbSchedule.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDCmbSchedule.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDCmbSchedule.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDCmbSchedule.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDCmbSchedule.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDCmbSchedule.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDCmbSchedule.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDCmbSchedule.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDCmbSchedule.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDCmbSchedule.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDCmbSchedule.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCmbSchedule.Properties.AutoHeight = CType(resources.GetObject("INDCmbSchedule.Properties.AutoHeight"), Boolean)
        Me.INDCmbSchedule.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDCmbSchedule.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDCmbSchedule.Properties.GlyphAlignment = CType(resources.GetObject("INDCmbSchedule.Properties.GlyphAlignment"), DevExpress.Utils.HorzAlignment)
        Me.INDCmbSchedule.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("INDCmbSchedule.Properties.Items"), resources.GetString("INDCmbSchedule.Properties.Items1"), CType(resources.GetObject("INDCmbSchedule.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("INDCmbSchedule.Properties.Items3"), resources.GetString("INDCmbSchedule.Properties.Items4"), CType(resources.GetObject("INDCmbSchedule.Properties.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("INDCmbSchedule.Properties.Items6"), resources.GetString("INDCmbSchedule.Properties.Items7"), CType(resources.GetObject("INDCmbSchedule.Properties.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("INDCmbSchedule.Properties.Items9"), resources.GetString("INDCmbSchedule.Properties.Items10"), CType(resources.GetObject("INDCmbSchedule.Properties.Items11"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("INDCmbSchedule.Properties.Items12"), resources.GetString("INDCmbSchedule.Properties.Items13"), CType(resources.GetObject("INDCmbSchedule.Properties.Items14"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("INDCmbSchedule.Properties.Items15"), resources.GetString("INDCmbSchedule.Properties.Items16"), CType(resources.GetObject("INDCmbSchedule.Properties.Items17"), Integer))})
        Me.INDCmbSchedule.Properties.NullValuePrompt = resources.GetString("INDCmbSchedule.Properties.NullValuePrompt")
        Me.INDCmbSchedule.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDCmbSchedule.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDCmbSchedule.Properties.SmallImages = Me.ImageList1
        Me.INDCmbSchedule.StyleController = Me.INDLcScheduleTemplate
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCmbSchedule, 0)
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "manana.png")
        Me.ImageList1.Images.SetKeyName(1, "Tarde.png")
        Me.ImageList1.Images.SetKeyName(2, "noche.png")
        Me.ImageList1.Images.SetKeyName(3, "manana-tarde.png")
        Me.ImageList1.Images.SetKeyName(4, "tarde-noche.png")
        Me.ImageList1.Images.SetKeyName(5, "manana-noche.png")
        '
        'INDGcUnidad
        '
        resources.ApplyResources(Me.INDGcUnidad, "INDGcUnidad")
        Me.INDGcUnidad.EmbeddedNavigator.AccessibleDescription = resources.GetString("INDGcUnidad.EmbeddedNavigator.AccessibleDescription")
        Me.INDGcUnidad.EmbeddedNavigator.AccessibleName = resources.GetString("INDGcUnidad.EmbeddedNavigator.AccessibleName")
        Me.INDGcUnidad.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("INDGcUnidad.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.INDGcUnidad.EmbeddedNavigator.Anchor = CType(resources.GetObject("INDGcUnidad.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.INDGcUnidad.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("INDGcUnidad.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.INDGcUnidad.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("INDGcUnidad.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.INDGcUnidad.EmbeddedNavigator.ImeMode = CType(resources.GetObject("INDGcUnidad.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.INDGcUnidad.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("INDGcUnidad.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.INDGcUnidad.EmbeddedNavigator.TextLocation = CType(resources.GetObject("INDGcUnidad.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.INDGcUnidad.EmbeddedNavigator.ToolTip = resources.GetString("INDGcUnidad.EmbeddedNavigator.ToolTip")
        Me.INDGcUnidad.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("INDGcUnidad.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.INDGcUnidad.EmbeddedNavigator.ToolTipTitle = resources.GetString("INDGcUnidad.EmbeddedNavigator.ToolTipTitle")
        Me.INDGcUnidad.MainView = Me.GridView1
        Me.INDGcUnidad.Name = "INDGcUnidad"
        Me.INDGcUnidad.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("GridView1.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.GridView1.Appearance.FocusedRow.Font = CType(resources.GetObject("GridView1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.FocusedRow.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.FocusedRow.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.FocusedRow.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.FocusedRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridView1.Appearance.FocusedRow.GradientMode = CType(resources.GetObject("GridView1.Appearance.FocusedRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.FocusedRow.Image = CType(resources.GetObject("GridView1.Appearance.FocusedRow.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridView1.Appearance.GroupRow.GradientMode = CType(resources.GetObject("GridView1.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.GroupRow.Image = CType(resources.GetObject("GridView1.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridView1.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.HeaderPanel.Image = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.Row.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.Row.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.Row.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridView1.Appearance.Row.GradientMode = CType(resources.GetObject("GridView1.Appearance.Row.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.Row.Image = CType(resources.GetObject("GridView1.Appearance.Row.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView1.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.ViewCaption.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.ViewCaption.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.ViewCaption.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.ViewCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridView1.Appearance.ViewCaption.GradientMode = CType(resources.GetObject("GridView1.Appearance.ViewCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.ViewCaption.Image = CType(resources.GetObject("GridView1.Appearance.ViewCaption.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        resources.ApplyResources(Me.GridView1, "GridView1")
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDChkTodos, Me.INDGcSede, Me.INDGcUnidadFuncional})
        Me.GridView1.GridControl = Me.INDGcUnidad
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.Tag = 192
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDChkTodos
        '
        resources.ApplyResources(Me.INDChkTodos, "INDChkTodos")
        Me.INDChkTodos.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.INDChkTodos.FieldName = "Apply"
        Me.INDChkTodos.Name = "INDChkTodos"
        Me.INDChkTodos.OptionsColumn.AllowMove = False
        Me.INDChkTodos.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'INDGcSede
        '
        resources.ApplyResources(Me.INDGcSede, "INDGcSede")
        Me.INDGcSede.FieldName = "BranchOffice.Name"
        Me.INDGcSede.Name = "INDGcSede"
        Me.INDGcSede.OptionsColumn.AllowEdit = False
        Me.INDGcSede.OptionsColumn.AllowMove = False
        Me.INDGcSede.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'INDGcUnidadFuncional
        '
        resources.ApplyResources(Me.INDGcUnidadFuncional, "INDGcUnidadFuncional")
        Me.INDGcUnidadFuncional.FieldName = "Name"
        Me.INDGcUnidadFuncional.Name = "INDGcUnidadFuncional"
        Me.INDGcUnidadFuncional.OptionsColumn.AllowEdit = False
        Me.INDGcUnidadFuncional.OptionsColumn.AllowMove = False
        Me.INDGcUnidadFuncional.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'INDTxtGroupName
        '
        resources.ApplyResources(Me.INDTxtGroupName, "INDTxtGroupName")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtGroupName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtGroupName, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtGroupName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtGroupName.Name = "INDTxtGroupName"
        Me.INDTxtGroupName.Properties.AccessibleDescription = resources.GetString("INDTxtGroupName.Properties.AccessibleDescription")
        Me.INDTxtGroupName.Properties.AccessibleName = resources.GetString("INDTxtGroupName.Properties.AccessibleName")
        Me.INDTxtGroupName.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtGroupName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtGroupName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtGroupName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtGroupName.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDTxtGroupName.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDTxtGroupName.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDTxtGroupName.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDTxtGroupName.Properties.Appearance.GradientMode = CType(resources.GetObject("INDTxtGroupName.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDTxtGroupName.Properties.Appearance.Image = CType(resources.GetObject("INDTxtGroupName.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDTxtGroupName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtGroupName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtGroupName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtGroupName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtGroupName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtGroupName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtGroupName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtGroupName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtGroupName.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDTxtGroupName.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDTxtGroupName.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDTxtGroupName.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDTxtGroupName.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDTxtGroupName.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDTxtGroupName.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDTxtGroupName.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDTxtGroupName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtGroupName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtGroupName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtGroupName.Properties.AutoHeight = CType(resources.GetObject("INDTxtGroupName.Properties.AutoHeight"), Boolean)
        Me.INDTxtGroupName.Properties.Mask.AutoComplete = CType(resources.GetObject("INDTxtGroupName.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDTxtGroupName.Properties.Mask.BeepOnError = CType(resources.GetObject("INDTxtGroupName.Properties.Mask.BeepOnError"), Boolean)
        Me.INDTxtGroupName.Properties.Mask.EditMask = resources.GetString("INDTxtGroupName.Properties.Mask.EditMask")
        Me.INDTxtGroupName.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDTxtGroupName.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDTxtGroupName.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtGroupName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtGroupName.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDTxtGroupName.Properties.Mask.PlaceHolder"), Char)
        Me.INDTxtGroupName.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDTxtGroupName.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDTxtGroupName.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDTxtGroupName.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDTxtGroupName.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDTxtGroupName.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDTxtGroupName.Properties.NullValuePrompt = resources.GetString("INDTxtGroupName.Properties.NullValuePrompt")
        Me.INDTxtGroupName.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDTxtGroupName.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDTxtGroupName.Properties.ReadOnly = True
        Me.INDTxtGroupName.StyleController = Me.INDLcScheduleTemplate
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtGroupName, 0)
        '
        'INDTxtName
        '
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, False)
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
        Me.INDTxtName.Properties.Mask.AutoComplete = CType(resources.GetObject("INDTxtName.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDTxtName.Properties.Mask.BeepOnError = CType(resources.GetObject("INDTxtName.Properties.Mask.BeepOnError"), Boolean)
        Me.INDTxtName.Properties.Mask.EditMask = resources.GetString("INDTxtName.Properties.Mask.EditMask")
        Me.INDTxtName.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDTxtName.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDTxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtName.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDTxtName.Properties.Mask.PlaceHolder"), Char)
        Me.INDTxtName.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDTxtName.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDTxtName.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDTxtName.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDTxtName.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDTxtName.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDTxtName.Properties.MaxLength = 75
        Me.INDTxtName.Properties.NullValuePrompt = resources.GetString("INDTxtName.Properties.NullValuePrompt")
        Me.INDTxtName.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDTxtName.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDTxtName.StyleController = Me.INDLcScheduleTemplate
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        '
        'INDScheduleControl
        '
        resources.ApplyResources(Me.INDScheduleControl, "INDScheduleControl")
        Me.INDScheduleControl.Name = "INDScheduleControl"
        Me.INDScheduleControl.OptionsCustomization.AllowAppointmentDrag = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDScheduleControl.OptionsCustomization.AllowAppointmentResize = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDScheduleControl.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDScheduleControl.OptionsPrint.PrintStyle = DevExpress.XtraScheduler.Printing.SchedulerPrintStyleKind.Daily
        Me.INDScheduleControl.OptionsRangeControl.AllowChangeActiveView = False
        Me.INDScheduleControl.OptionsView.NavigationButtons.Visibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never
        Me.INDScheduleControl.Start = New Date(2013, 7, 26, 0, 0, 0, 0)
        Me.INDScheduleControl.Storage = Me.SchedulerStorage1
        Me.INDScheduleControl.Views.DayView.AllDayAreaScrollBarVisible = True
        Me.INDScheduleControl.Views.DayView.DayCount = 2
        Me.INDScheduleControl.Views.DayView.DisplayName = resources.GetString("INDScheduleControl.Views.DayView.DisplayName")
        resources.ApplyResources(TimeRuler1, "TimeRuler1")
        TimeRuler1.TimeZoneId = "SA Pacific Standard Time"
        TimeRuler1.UseClientTimeZone = False
        Me.INDScheduleControl.Views.DayView.TimeRulers.Add(TimeRuler1)
        resources.ApplyResources(TimeRuler2, "TimeRuler2")
        TimeRuler2.TimeZoneId = "SA Pacific Standard Time"
        TimeRuler2.UseClientTimeZone = False
        Me.INDScheduleControl.Views.WorkWeekView.TimeRulers.Add(TimeRuler2)
        '
        'SchedulerStorage1
        '
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.SystemColors.Window, "None", "&None"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(190, Byte), Integer)), "Important", "&Important"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(168, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer)), "Business", "&Business"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(156, Byte), Integer)), "Personal", "&Personal"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(199, Byte), Integer)), "Vacation", "&Vacation"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(147, Byte), Integer)), "Must Attend", "Must &Attend"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer)), "Travel Required", "&Travel Required"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(152, Byte), Integer)), "Needs Preparation", "&Needs Preparation"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(233, Byte), Integer)), "Birthday", "&Birthday"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer)), "Anniversary", "&Anniversary"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(165, Byte), Integer)), "Telefono", "Phone &Call"))
        '
        'INDTxtCode
        '
        resources.ApplyResources(Me.INDTxtCode, "INDTxtCode")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtCode, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtCode.Name = "INDTxtCode"
        Me.INDTxtCode.Properties.AccessibleDescription = resources.GetString("INDTxtCode.Properties.AccessibleDescription")
        Me.INDTxtCode.Properties.AccessibleName = resources.GetString("INDTxtCode.Properties.AccessibleName")
        Me.INDTxtCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtCode.Properties.Appearance.Font = CType(resources.GetObject("INDTxtCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtCode.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDTxtCode.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDTxtCode.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDTxtCode.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDTxtCode.Properties.Appearance.GradientMode = CType(resources.GetObject("INDTxtCode.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDTxtCode.Properties.Appearance.Image = CType(resources.GetObject("INDTxtCode.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDTxtCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtCode.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDTxtCode.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDTxtCode.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDTxtCode.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDTxtCode.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDTxtCode.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDTxtCode.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDTxtCode.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDTxtCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtCode.Properties.AutoHeight = CType(resources.GetObject("INDTxtCode.Properties.AutoHeight"), Boolean)
        resources.ApplyResources(SerializableAppearanceObject1, "SerializableAppearanceObject1")
        Me.INDTxtCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDTxtCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDTxtCode.Properties.Buttons1"), CType(resources.GetObject("INDTxtCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDTxtCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDTxtCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDTxtCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDTxtCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDTxtCode.Properties.Buttons7"), CType(resources.GetObject("INDTxtCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDTxtCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDTxtCode.Properties.Buttons10"), Boolean))})
        Me.INDTxtCode.Properties.Mask.AutoComplete = CType(resources.GetObject("INDTxtCode.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDTxtCode.Properties.Mask.BeepOnError = CType(resources.GetObject("INDTxtCode.Properties.Mask.BeepOnError"), Boolean)
        Me.INDTxtCode.Properties.Mask.EditMask = resources.GetString("INDTxtCode.Properties.Mask.EditMask")
        Me.INDTxtCode.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDTxtCode.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDTxtCode.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtCode.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDTxtCode.Properties.Mask.PlaceHolder"), Char)
        Me.INDTxtCode.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDTxtCode.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDTxtCode.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDTxtCode.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDTxtCode.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDTxtCode.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDTxtCode.Properties.MaxLength = 3
        Me.INDTxtCode.Properties.NullValuePrompt = resources.GetString("INDTxtCode.Properties.NullValuePrompt")
        Me.INDTxtCode.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDTxtCode.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDTxtCode.StyleController = Me.INDLcScheduleTemplate
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtCode, 0)
        '
        'INDTxtGroupCode
        '
        resources.ApplyResources(Me.INDTxtGroupCode, "INDTxtGroupCode")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtGroupCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtGroupCode, False)
        Me.INDTxtGroupCode.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtGroupCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtGroupCode.Name = "INDTxtGroupCode"
        Me.INDTxtGroupCode.Properties.AccessibleDescription = resources.GetString("INDTxtGroupCode.Properties.AccessibleDescription")
        Me.INDTxtGroupCode.Properties.AccessibleName = resources.GetString("INDTxtGroupCode.Properties.AccessibleName")
        Me.INDTxtGroupCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtGroupCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtGroupCode.Properties.Appearance.Font = CType(resources.GetObject("INDTxtGroupCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtGroupCode.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDTxtGroupCode.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDTxtGroupCode.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDTxtGroupCode.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDTxtGroupCode.Properties.Appearance.GradientMode = CType(resources.GetObject("INDTxtGroupCode.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDTxtGroupCode.Properties.Appearance.Image = CType(resources.GetObject("INDTxtGroupCode.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDTxtGroupCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtGroupCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtGroupCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtGroupCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtGroupCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtGroupCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtGroupCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtGroupCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtGroupCode.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDTxtGroupCode.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDTxtGroupCode.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDTxtGroupCode.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDTxtGroupCode.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDTxtGroupCode.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDTxtGroupCode.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDTxtGroupCode.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDTxtGroupCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtGroupCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtGroupCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtGroupCode.Properties.AutoHeight = CType(resources.GetObject("INDTxtGroupCode.Properties.AutoHeight"), Boolean)
        resources.ApplyResources(SerializableAppearanceObject2, "SerializableAppearanceObject2")
        Me.INDTxtGroupCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDTxtGroupCode.Properties.Buttons1"), CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, resources.GetString("INDTxtGroupCode.Properties.Buttons7"), CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDTxtGroupCode.Properties.Buttons10"), Boolean))})
        Me.INDTxtGroupCode.Properties.Mask.AutoComplete = CType(resources.GetObject("INDTxtGroupCode.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDTxtGroupCode.Properties.Mask.BeepOnError = CType(resources.GetObject("INDTxtGroupCode.Properties.Mask.BeepOnError"), Boolean)
        Me.INDTxtGroupCode.Properties.Mask.EditMask = resources.GetString("INDTxtGroupCode.Properties.Mask.EditMask")
        Me.INDTxtGroupCode.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDTxtGroupCode.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDTxtGroupCode.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtGroupCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtGroupCode.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDTxtGroupCode.Properties.Mask.PlaceHolder"), Char)
        Me.INDTxtGroupCode.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDTxtGroupCode.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDTxtGroupCode.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDTxtGroupCode.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDTxtGroupCode.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDTxtGroupCode.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDTxtGroupCode.Properties.MaxLength = 20
        Me.INDTxtGroupCode.Properties.NullValuePrompt = resources.GetString("INDTxtGroupCode.Properties.NullValuePrompt")
        Me.INDTxtGroupCode.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDTxtGroupCode.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDTxtGroupCode.StyleController = Me.INDLcScheduleTemplate
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtGroupCode, 0)
        '
        'lycgScheduleTemplate
        '
        Me.lycgScheduleTemplate.AppearanceGroup.Font = CType(resources.GetObject("lycgScheduleTemplate.AppearanceGroup.Font"), System.Drawing.Font)
        Me.lycgScheduleTemplate.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceGroup.FontSizeDelta"), Integer)
        Me.lycgScheduleTemplate.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgScheduleTemplate.AppearanceGroup.GradientMode = CType(resources.GetObject("lycgScheduleTemplate.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgScheduleTemplate.AppearanceGroup.Image = CType(resources.GetObject("lycgScheduleTemplate.AppearanceGroup.Image"), System.Drawing.Image)
        Me.lycgScheduleTemplate.AppearanceGroup.Options.UseFont = True
        Me.lycgScheduleTemplate.AppearanceItemCaption.Font = CType(resources.GetObject("lycgScheduleTemplate.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.lycgScheduleTemplate.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.lycgScheduleTemplate.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgScheduleTemplate.AppearanceItemCaption.GradientMode = CType(resources.GetObject("lycgScheduleTemplate.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgScheduleTemplate.AppearanceItemCaption.Image = CType(resources.GetObject("lycgScheduleTemplate.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.lycgScheduleTemplate.AppearanceItemCaption.Options.UseFont = True
        Me.lycgScheduleTemplate.AppearanceTabPage.Header.Font = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.lycgScheduleTemplate.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.lycgScheduleTemplate.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgScheduleTemplate.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgScheduleTemplate.AppearanceTabPage.Header.Image = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.lycgScheduleTemplate.AppearanceTabPage.Header.Options.UseFont = True
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.lycgScheduleTemplate.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.lycgScheduleTemplate.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.lycgScheduleTemplate.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.lycgScheduleTemplate.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgScheduleTemplate.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgScheduleTemplate.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("lycgScheduleTemplate.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.lycgScheduleTemplate.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.lycgScheduleTemplate, False)
        resources.ApplyResources(Me.lycgScheduleTemplate, "lycgScheduleTemplate")
        Me.lycgScheduleTemplate.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.lycgScheduleTemplate.GroupBordersVisible = False
        Me.lycgScheduleTemplate.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcItemSchedule, Me.lycgSchedule, Me.lycgFunctionalUnit})
        Me.lycgScheduleTemplate.Location = New System.Drawing.Point(0, 0)
        Me.lycgScheduleTemplate.Name = "Root"
        Me.lycgScheduleTemplate.Size = New System.Drawing.Size(1358, 538)
        Me.lycgScheduleTemplate.TextVisible = False
        '
        'INDLcItemSchedule
        '
        resources.ApplyResources(Me.INDLcItemSchedule, "INDLcItemSchedule")
        Me.INDLcItemSchedule.Control = Me.INDScheduleControl
        Me.INDLcItemSchedule.Location = New System.Drawing.Point(0, 0)
        Me.INDLcItemSchedule.MaxSize = New System.Drawing.Size(500, 0)
        Me.INDLcItemSchedule.MinSize = New System.Drawing.Size(500, 384)
        Me.INDLcItemSchedule.Name = "INDLcItemSchedule"
        Me.INDLcItemSchedule.Size = New System.Drawing.Size(500, 518)
        Me.INDLcItemSchedule.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcItemSchedule.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLcItemSchedule.TextVisible = False
        '
        'lycgSchedule
        '
        Me.lycgSchedule.AppearanceGroup.Font = CType(resources.GetObject("lycgSchedule.AppearanceGroup.Font"), System.Drawing.Font)
        Me.lycgSchedule.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("lycgSchedule.AppearanceGroup.FontSizeDelta"), Integer)
        Me.lycgSchedule.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("lycgSchedule.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgSchedule.AppearanceGroup.GradientMode = CType(resources.GetObject("lycgSchedule.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgSchedule.AppearanceGroup.Image = CType(resources.GetObject("lycgSchedule.AppearanceGroup.Image"), System.Drawing.Image)
        Me.lycgSchedule.AppearanceGroup.Options.UseFont = True
        Me.lycgSchedule.AppearanceItemCaption.Font = CType(resources.GetObject("lycgSchedule.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.lycgSchedule.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("lycgSchedule.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.lycgSchedule.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("lycgSchedule.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgSchedule.AppearanceItemCaption.GradientMode = CType(resources.GetObject("lycgSchedule.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgSchedule.AppearanceItemCaption.Image = CType(resources.GetObject("lycgSchedule.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.lycgSchedule.AppearanceItemCaption.Options.UseFont = True
        Me.lycgSchedule.AppearanceTabPage.Header.Font = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.lycgSchedule.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.lycgSchedule.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgSchedule.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgSchedule.AppearanceTabPage.Header.Image = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.lycgSchedule.AppearanceTabPage.Header.Options.UseFont = True
        Me.lycgSchedule.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.lycgSchedule.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.lycgSchedule.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgSchedule.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgSchedule.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.lycgSchedule.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.lycgSchedule.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.lycgSchedule.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.lycgSchedule.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgSchedule.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgSchedule.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.lycgSchedule.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.lycgSchedule.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.lycgSchedule.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.lycgSchedule.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgSchedule.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgSchedule.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.lycgSchedule.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.lycgSchedule.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.lycgSchedule.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.lycgSchedule.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgSchedule.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgSchedule.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("lycgSchedule.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.lycgSchedule.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.lycgSchedule, False)
        resources.ApplyResources(Me.lycgSchedule, "lycgSchedule")
        Me.lycgSchedule.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcItemCodigo, Me.INDLcItemDescripcion, Me.INDLcItemGrupo, Me.lycItemGroupName, Me.INDLcItemHorario})
        Me.lycgSchedule.Location = New System.Drawing.Point(500, 0)
        Me.lycgSchedule.Name = "lycgSchedule"
        Me.lycgSchedule.Size = New System.Drawing.Size(838, 203)
        '
        'INDLcItemCodigo
        '
        resources.ApplyResources(Me.INDLcItemCodigo, "INDLcItemCodigo")
        Me.INDLcItemCodigo.Control = Me.INDTxtCode
        Me.INDLcItemCodigo.Location = New System.Drawing.Point(0, 0)
        Me.INDLcItemCodigo.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDLcItemCodigo.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLcItemCodigo.Name = "INDLcItemCodigo"
        Me.INDLcItemCodigo.Size = New System.Drawing.Size(814, 36)
        Me.INDLcItemCodigo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcItemCodigo.Tag = "Code"
        Me.INDLcItemCodigo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcItemCodigo.TextSize = New System.Drawing.Size(147, 21)
        Me.INDLcItemCodigo.TextToControlDistance = 12
        '
        'INDLcItemDescripcion
        '
        resources.ApplyResources(Me.INDLcItemDescripcion, "INDLcItemDescripcion")
        Me.INDLcItemDescripcion.Control = Me.INDTxtName
        Me.INDLcItemDescripcion.Location = New System.Drawing.Point(0, 36)
        Me.INDLcItemDescripcion.MaxSize = New System.Drawing.Size(650, 36)
        Me.INDLcItemDescripcion.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLcItemDescripcion.Name = "INDLcItemDescripcion"
        Me.INDLcItemDescripcion.Size = New System.Drawing.Size(814, 36)
        Me.INDLcItemDescripcion.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcItemDescripcion.Tag = "Name"
        Me.INDLcItemDescripcion.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcItemDescripcion.TextSize = New System.Drawing.Size(147, 21)
        Me.INDLcItemDescripcion.TextToControlDistance = 12
        '
        'INDLcItemGrupo
        '
        resources.ApplyResources(Me.INDLcItemGrupo, "INDLcItemGrupo")
        Me.INDLcItemGrupo.Control = Me.INDTxtGroupCode
        Me.INDLcItemGrupo.Location = New System.Drawing.Point(0, 72)
        Me.INDLcItemGrupo.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDLcItemGrupo.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLcItemGrupo.Name = "INDLcItemGrupo"
        Me.INDLcItemGrupo.Size = New System.Drawing.Size(300, 36)
        Me.INDLcItemGrupo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcItemGrupo.Tag = "GroupId"
        Me.INDLcItemGrupo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcItemGrupo.TextSize = New System.Drawing.Size(147, 21)
        Me.INDLcItemGrupo.TextToControlDistance = 12
        '
        'lycItemGroupName
        '
        resources.ApplyResources(Me.lycItemGroupName, "lycItemGroupName")
        Me.lycItemGroupName.Control = Me.INDTxtGroupName
        Me.lycItemGroupName.Location = New System.Drawing.Point(300, 72)
        Me.lycItemGroupName.MaxSize = New System.Drawing.Size(350, 36)
        Me.lycItemGroupName.MinSize = New System.Drawing.Size(300, 36)
        Me.lycItemGroupName.Name = "lycItemGroupName"
        Me.lycItemGroupName.Size = New System.Drawing.Size(514, 36)
        Me.lycItemGroupName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.lycItemGroupName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lycItemGroupName.TextSize = New System.Drawing.Size(0, 0)
        Me.lycItemGroupName.TextToControlDistance = 12
        '
        'INDLcItemHorario
        '
        resources.ApplyResources(Me.INDLcItemHorario, "INDLcItemHorario")
        Me.INDLcItemHorario.Control = Me.INDCmbSchedule
        Me.INDLcItemHorario.Location = New System.Drawing.Point(0, 108)
        Me.INDLcItemHorario.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDLcItemHorario.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLcItemHorario.Name = "INDLcItemHorario"
        Me.INDLcItemHorario.Size = New System.Drawing.Size(814, 36)
        Me.INDLcItemHorario.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcItemHorario.Tag = "Letter"
        Me.INDLcItemHorario.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcItemHorario.TextSize = New System.Drawing.Size(147, 21)
        Me.INDLcItemHorario.TextToControlDistance = 12
        '
        'lycgFunctionalUnit
        '
        Me.lycgFunctionalUnit.AppearanceGroup.Font = CType(resources.GetObject("lycgFunctionalUnit.AppearanceGroup.Font"), System.Drawing.Font)
        Me.lycgFunctionalUnit.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceGroup.FontSizeDelta"), Integer)
        Me.lycgFunctionalUnit.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgFunctionalUnit.AppearanceGroup.GradientMode = CType(resources.GetObject("lycgFunctionalUnit.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgFunctionalUnit.AppearanceGroup.Image = CType(resources.GetObject("lycgFunctionalUnit.AppearanceGroup.Image"), System.Drawing.Image)
        Me.lycgFunctionalUnit.AppearanceGroup.Options.UseFont = True
        Me.lycgFunctionalUnit.AppearanceItemCaption.Font = CType(resources.GetObject("lycgFunctionalUnit.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.lycgFunctionalUnit.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.lycgFunctionalUnit.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgFunctionalUnit.AppearanceItemCaption.GradientMode = CType(resources.GetObject("lycgFunctionalUnit.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgFunctionalUnit.AppearanceItemCaption.Image = CType(resources.GetObject("lycgFunctionalUnit.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.lycgFunctionalUnit.AppearanceItemCaption.Options.UseFont = True
        Me.lycgFunctionalUnit.AppearanceTabPage.Header.Font = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.lycgFunctionalUnit.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.lycgFunctionalUnit.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgFunctionalUnit.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgFunctionalUnit.AppearanceTabPage.Header.Image = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.lycgFunctionalUnit.AppearanceTabPage.Header.Options.UseFont = True
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.lycgFunctionalUnit.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.lycgFunctionalUnit.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.lycgFunctionalUnit.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.lycgFunctionalUnit.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.lycgFunctionalUnit.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.lycgFunctionalUnit.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("lycgFunctionalUnit.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.lycgFunctionalUnit.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.lycgFunctionalUnit, False)
        resources.ApplyResources(Me.lycgFunctionalUnit, "lycgFunctionalUnit")
        Me.lycgFunctionalUnit.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcItemUnidadFuncional, Me.INDLclItemEmpresa})
        Me.lycgFunctionalUnit.Location = New System.Drawing.Point(500, 203)
        Me.lycgFunctionalUnit.Name = "lycgFunctionalUnit"
        Me.lycgFunctionalUnit.Size = New System.Drawing.Size(838, 315)
        '
        'INDLcItemUnidadFuncional
        '
        resources.ApplyResources(Me.INDLcItemUnidadFuncional, "INDLcItemUnidadFuncional")
        Me.INDLcItemUnidadFuncional.Control = Me.INDGcUnidad
        Me.INDLcItemUnidadFuncional.Location = New System.Drawing.Point(0, 36)
        Me.INDLcItemUnidadFuncional.MinSize = New System.Drawing.Size(104, 30)
        Me.INDLcItemUnidadFuncional.Name = "INDLcItemUnidadFuncional"
        Me.INDLcItemUnidadFuncional.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 8, 2)
        Me.INDLcItemUnidadFuncional.Size = New System.Drawing.Size(814, 220)
        Me.INDLcItemUnidadFuncional.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcItemUnidadFuncional.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLcItemUnidadFuncional.TextVisible = False
        '
        'INDLclItemEmpresa
        '
        resources.ApplyResources(Me.INDLclItemEmpresa, "INDLclItemEmpresa")
        Me.INDLclItemEmpresa.Control = Me.INDGlueEmpresa
        Me.INDLclItemEmpresa.Location = New System.Drawing.Point(0, 0)
        Me.INDLclItemEmpresa.MaxSize = New System.Drawing.Size(350, 36)
        Me.INDLclItemEmpresa.MinSize = New System.Drawing.Size(350, 36)
        Me.INDLclItemEmpresa.Name = "INDLclItemEmpresa"
        Me.INDLclItemEmpresa.Size = New System.Drawing.Size(814, 36)
        Me.INDLclItemEmpresa.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLclItemEmpresa.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLclItemEmpresa.TextSize = New System.Drawing.Size(147, 21)
        Me.INDLclItemEmpresa.TextToControlDistance = 12
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmScheduleTemplate
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmScheduleTemplate"
        Me.Opacity = 1.0R
        Me.Tag = "551"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcScheduleTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcScheduleTemplate.ResumeLayout(False)
        CType(Me.INDGlueEmpresa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCmbSchedule.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcUnidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtGroupName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDScheduleControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtGroupCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lycgScheduleTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcItemSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lycgSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcItemCodigo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcItemDescripcion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcItemGrupo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lycItemGroupName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcItemHorario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lycgFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcItemUnidadFuncional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLclItemEmpresa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcScheduleTemplate As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents lycgScheduleTemplate As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents SchedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
    Friend WithEvents INDScheduleControl As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents INDLcItemSchedule As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcItemCodigo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcItemGrupo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcUnidad As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDChkTodos As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTxtGroupName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcItemDescripcion As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lycItemGroupName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDGcSede As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcUnidadFuncional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDCmbSchedule As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents lycgSchedule As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcItemHorario As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDTxtCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcItemUnidadFuncional As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtGroupCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lycgFunctionalUnit As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGlueEmpresa As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLclItemEmpresa As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcCompanyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcCompanyName As DevExpress.XtraGrid.Columns.GridColumn
End Class
