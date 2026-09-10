Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTemplateProduct
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTemplateProduct))
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MemoEdit1 = New DevExpress.XtraEditors.MemoEdit()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.ButtonEdit1 = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
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
        'CtrNavigationControl1
        '
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
        Me.LayoutControl1.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
        Me.LayoutControl1.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControl1.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("LayoutControl1.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
        Me.LayoutControl1.Controls.Add(Me.GridControl1)
        Me.LayoutControl1.Controls.Add(Me.MemoEdit1)
        Me.LayoutControl1.Controls.Add(Me.TextEdit1)
        Me.LayoutControl1.Controls.Add(Me.ButtonEdit1)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsPrint.AppearanceGroupCaption.FontSizeDelta = CType(resources.GetObject("LayoutControl1.OptionsPrint.AppearanceGroupCaption.FontSizeDelta"), Integer)
        Me.LayoutControl1.OptionsPrint.AppearanceGroupCaption.FontStyleDelta = CType(resources.GetObject("LayoutControl1.OptionsPrint.AppearanceGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControl1.OptionsPrint.AppearanceGroupCaption.GradientMode = CType(resources.GetObject("LayoutControl1.OptionsPrint.AppearanceGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControl1.OptionsPrint.AppearanceGroupCaption.Image = CType(resources.GetObject("LayoutControl1.OptionsPrint.AppearanceGroupCaption.Image"), System.Drawing.Image)
        Me.LayoutControl1.OptionsPrint.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("LayoutControl1.OptionsPrint.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.LayoutControl1.OptionsPrint.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("LayoutControl1.OptionsPrint.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControl1.OptionsPrint.AppearanceItemCaption.GradientMode = CType(resources.GetObject("LayoutControl1.OptionsPrint.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControl1.OptionsPrint.AppearanceItemCaption.Image = CType(resources.GetObject("LayoutControl1.OptionsPrint.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'GridControl1
        '
        resources.ApplyResources(Me.GridControl1, "GridControl1")
        Me.IndigoGridControl1.SetAddActions(Me.GridControl1, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.GridControl1, Nothing)
        Me.GridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("GridControl1.EmbeddedNavigator.AccessibleDescription")
        Me.GridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("GridControl1.EmbeddedNavigator.AccessibleName")
        Me.GridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = CType(resources.GetObject("GridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip"), DevExpress.Utils.DefaultBoolean)
        Me.GridControl1.EmbeddedNavigator.Anchor = CType(resources.GetObject("GridControl1.EmbeddedNavigator.Anchor"), System.Windows.Forms.AnchorStyles)
        Me.GridControl1.EmbeddedNavigator.BackgroundImage = CType(resources.GetObject("GridControl1.EmbeddedNavigator.BackgroundImage"), System.Drawing.Image)
        Me.GridControl1.EmbeddedNavigator.BackgroundImageLayout = CType(resources.GetObject("GridControl1.EmbeddedNavigator.BackgroundImageLayout"), System.Windows.Forms.ImageLayout)
        Me.GridControl1.EmbeddedNavigator.ImeMode = CType(resources.GetObject("GridControl1.EmbeddedNavigator.ImeMode"), System.Windows.Forms.ImeMode)
        Me.GridControl1.EmbeddedNavigator.MaximumSize = CType(resources.GetObject("GridControl1.EmbeddedNavigator.MaximumSize"), System.Drawing.Size)
        Me.GridControl1.EmbeddedNavigator.TextLocation = CType(resources.GetObject("GridControl1.EmbeddedNavigator.TextLocation"), DevExpress.XtraEditors.NavigatorButtonsTextLocation)
        Me.GridControl1.EmbeddedNavigator.ToolTip = resources.GetString("GridControl1.EmbeddedNavigator.ToolTip")
        Me.GridControl1.EmbeddedNavigator.ToolTipIconType = CType(resources.GetObject("GridControl1.EmbeddedNavigator.ToolTipIconType"), DevExpress.Utils.ToolTipIconType)
        Me.GridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("GridControl1.EmbeddedNavigator.ToolTipTitle")
        Me.IndigoGridControl1.SetExportButton(Me.GridControl1, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.GridControl1, True)
        Me.IndigoGridControl1.SetHoldSize(Me.GridControl1, False)
        Me.IndigoGridControl1.SetHotTrack(Me.GridControl1, False)
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.Name = "GridControl1"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.GridControl1, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.GridControl1, New System.Drawing.Size(310, 0))
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        'Cadena reemplazada... CType(resources.GetObject("GridView1.Appearance.FocusedRow.BackColor"), System.Drawing.Color)
        'Cadena reemplazada... CType(resources.GetObject("GridView1.Appearance.FocusedRow.BackColor2"), System.Drawing.Color)
        Me.GridView1.Appearance.FocusedRow.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.FocusedRow.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.FocusedRow.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.FocusedRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridView1.Appearance.FocusedRow.GradientMode = CType(resources.GetObject("GridView1.Appearance.FocusedRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.FocusedRow.Image = CType(resources.GetObject("GridView1.Appearance.FocusedRow.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("GridView1.Appearance.GroupRow.ForeColor"), System.Drawing.Color)
        Me.GridView1.Appearance.GroupRow.GradientMode = CType(resources.GetObject("GridView1.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.GroupRow.Image = CType(resources.GetObject("GridView1.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("GridView1.Appearance.HeaderPanel.ForeColor"), System.Drawing.Color)
        Me.GridView1.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.HeaderPanel.Image = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.Name = "GridColumn4"
        '
        'MemoEdit1
        '
        resources.ApplyResources(Me.MemoEdit1, "MemoEdit1")
        Me.IndigoTextEdit1.SetApplyStyle(Me.MemoEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.MemoEdit1, False)
        Me.IndigoTextEdit1.SetMascara(Me.MemoEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.MemoEdit1.Name = "MemoEdit1"
        Me.MemoEdit1.Properties.AccessibleDescription = resources.GetString("MemoEdit1.Properties.AccessibleDescription")
        Me.MemoEdit1.Properties.AccessibleName = resources.GetString("MemoEdit1.Properties.AccessibleName")
        Me.MemoEdit1.Properties.Appearance.BackColor = CType(resources.GetObject("MemoEdit1.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.MemoEdit1.Properties.Appearance.Font = CType(resources.GetObject("MemoEdit1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.MemoEdit1.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("MemoEdit1.Properties.Appearance.FontSizeDelta"), Integer)
        Me.MemoEdit1.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("MemoEdit1.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.MemoEdit1.Properties.Appearance.GradientMode = CType(resources.GetObject("MemoEdit1.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.MemoEdit1.Properties.Appearance.Image = CType(resources.GetObject("MemoEdit1.Properties.Appearance.Image"), System.Drawing.Image)
        Me.MemoEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.MemoEdit1.Properties.Appearance.Options.UseFont = True
        Me.MemoEdit1.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("MemoEdit1.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.MemoEdit1.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("MemoEdit1.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.MemoEdit1.Properties.AppearanceFocused.Font = CType(resources.GetObject("MemoEdit1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.MemoEdit1.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("MemoEdit1.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.MemoEdit1.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("MemoEdit1.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.MemoEdit1.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("MemoEdit1.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.MemoEdit1.Properties.AppearanceFocused.Image = CType(resources.GetObject("MemoEdit1.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.MemoEdit1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.MemoEdit1.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.MemoEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.MemoEdit1.Properties.NullValuePrompt = resources.GetString("MemoEdit1.Properties.NullValuePrompt")
        Me.MemoEdit1.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("MemoEdit1.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.MemoEdit1.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.MemoEdit1, 0)
        '
        'TextEdit1
        '
        resources.ApplyResources(Me.TextEdit1, "TextEdit1")
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit1, False)
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Properties.AccessibleDescription = resources.GetString("TextEdit1.Properties.AccessibleDescription")
        Me.TextEdit1.Properties.AccessibleName = resources.GetString("TextEdit1.Properties.AccessibleName")
        Me.TextEdit1.Properties.Appearance.BackColor = CType(resources.GetObject("TextEdit1.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.TextEdit1.Properties.Appearance.Font = CType(resources.GetObject("TextEdit1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit1.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("TextEdit1.Properties.Appearance.FontSizeDelta"), Integer)
        Me.TextEdit1.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("TextEdit1.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.TextEdit1.Properties.Appearance.GradientMode = CType(resources.GetObject("TextEdit1.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.TextEdit1.Properties.Appearance.Image = CType(resources.GetObject("TextEdit1.Properties.Appearance.Image"), System.Drawing.Image)
        Me.TextEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit1.Properties.Appearance.Options.UseFont = True
        Me.TextEdit1.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("TextEdit1.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.TextEdit1.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("TextEdit1.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.TextEdit1.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit1.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("TextEdit1.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.TextEdit1.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("TextEdit1.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.TextEdit1.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("TextEdit1.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.TextEdit1.Properties.AppearanceFocused.Image = CType(resources.GetObject("TextEdit1.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.TextEdit1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit1.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit1.Properties.AutoHeight = CType(resources.GetObject("TextEdit1.Properties.AutoHeight"), Boolean)
        Me.TextEdit1.Properties.Mask.AutoComplete = CType(resources.GetObject("TextEdit1.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.TextEdit1.Properties.Mask.BeepOnError = CType(resources.GetObject("TextEdit1.Properties.Mask.BeepOnError"), Boolean)
        Me.TextEdit1.Properties.Mask.EditMask = resources.GetString("TextEdit1.Properties.Mask.EditMask")
        Me.TextEdit1.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("TextEdit1.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.TextEdit1.Properties.Mask.MaskType = CType(resources.GetObject("TextEdit1.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.TextEdit1.Properties.Mask.PlaceHolder = CType(resources.GetObject("TextEdit1.Properties.Mask.PlaceHolder"), Char)
        Me.TextEdit1.Properties.Mask.SaveLiteral = CType(resources.GetObject("TextEdit1.Properties.Mask.SaveLiteral"), Boolean)
        Me.TextEdit1.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("TextEdit1.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.TextEdit1.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("TextEdit1.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.TextEdit1.Properties.NullValuePrompt = resources.GetString("TextEdit1.Properties.NullValuePrompt")
        Me.TextEdit1.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("TextEdit1.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.TextEdit1.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit1, 0)
        '
        'ButtonEdit1
        '
        resources.ApplyResources(Me.ButtonEdit1, "ButtonEdit1")
        Me.IndigoTextEdit1.SetApplyStyle(Me.ButtonEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ButtonEdit1, False)
        Me.IndigoTextEdit1.SetMascara(Me.ButtonEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ButtonEdit1.Name = "ButtonEdit1"
        Me.ButtonEdit1.Properties.AccessibleDescription = resources.GetString("ButtonEdit1.Properties.AccessibleDescription")
        Me.ButtonEdit1.Properties.AccessibleName = resources.GetString("ButtonEdit1.Properties.AccessibleName")
        Me.ButtonEdit1.Properties.Appearance.BackColor = CType(resources.GetObject("ButtonEdit1.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.ButtonEdit1.Properties.Appearance.Font = CType(resources.GetObject("ButtonEdit1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ButtonEdit1.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("ButtonEdit1.Properties.Appearance.FontSizeDelta"), Integer)
        Me.ButtonEdit1.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("ButtonEdit1.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.ButtonEdit1.Properties.Appearance.GradientMode = CType(resources.GetObject("ButtonEdit1.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.ButtonEdit1.Properties.Appearance.Image = CType(resources.GetObject("ButtonEdit1.Properties.Appearance.Image"), System.Drawing.Image)
        Me.ButtonEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.ButtonEdit1.Properties.Appearance.Options.UseFont = True
        Me.ButtonEdit1.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("ButtonEdit1.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.ButtonEdit1.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("ButtonEdit1.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.ButtonEdit1.Properties.AppearanceFocused.Font = CType(resources.GetObject("ButtonEdit1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.ButtonEdit1.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("ButtonEdit1.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.ButtonEdit1.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("ButtonEdit1.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.ButtonEdit1.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("ButtonEdit1.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.ButtonEdit1.Properties.AppearanceFocused.Image = CType(resources.GetObject("ButtonEdit1.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.ButtonEdit1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.ButtonEdit1.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.ButtonEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.ButtonEdit1.Properties.AutoHeight = CType(resources.GetObject("ButtonEdit1.Properties.AutoHeight"), Boolean)
        Me.ButtonEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ButtonEdit1.Properties.Mask.AutoComplete = CType(resources.GetObject("ButtonEdit1.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.ButtonEdit1.Properties.Mask.BeepOnError = CType(resources.GetObject("ButtonEdit1.Properties.Mask.BeepOnError"), Boolean)
        Me.ButtonEdit1.Properties.Mask.EditMask = resources.GetString("ButtonEdit1.Properties.Mask.EditMask")
        Me.ButtonEdit1.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("ButtonEdit1.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.ButtonEdit1.Properties.Mask.MaskType = CType(resources.GetObject("ButtonEdit1.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.ButtonEdit1.Properties.Mask.PlaceHolder = CType(resources.GetObject("ButtonEdit1.Properties.Mask.PlaceHolder"), Char)
        Me.ButtonEdit1.Properties.Mask.SaveLiteral = CType(resources.GetObject("ButtonEdit1.Properties.Mask.SaveLiteral"), Boolean)
        Me.ButtonEdit1.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("ButtonEdit1.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.ButtonEdit1.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("ButtonEdit1.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.ButtonEdit1.Properties.NullValuePrompt = resources.GetString("ButtonEdit1.Properties.NullValuePrompt")
        Me.ButtonEdit1.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("ButtonEdit1.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.ButtonEdit1.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ButtonEdit1, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceGroup.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceGroup.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceItemCaption.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceItemCaption.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(768, 413)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.FontSizeDelta"), Integer)
        Me.LayoutControlGroup2.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup2.AppearanceGroup.GradientMode = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup2.AppearanceGroup.Image = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Image"), System.Drawing.Image)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.LayoutControlGroup2.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup2.AppearanceItemCaption.GradientMode = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup2.AppearanceItemCaption.Image = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Image = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 393)
        '
        'LayoutControlItem1
        '
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Control = Me.ButtonEdit1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(300, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(300, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 21)
        Me.LayoutControlItem1.TextToControlDistance = 12
        '
        'LayoutControlItem2
        '
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Control = Me.TextEdit1
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 21)
        Me.LayoutControlItem2.TextToControlDistance = 12
        '
        'LayoutControlItem3
        '
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Control = Me.MemoEdit1
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 200)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 200)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 262)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(100, 21)
        Me.LayoutControlItem3.TextToControlDistance = 12
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.FontSizeDelta"), Integer)
        Me.LayoutControlGroup3.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup3.AppearanceGroup.GradientMode = CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup3.AppearanceGroup.Image = CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.Image"), System.Drawing.Image)
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.LayoutControlGroup3.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup3.AppearanceItemCaption.GradientMode = CType(resources.GetObject("LayoutControlGroup3.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup3.AppearanceItemCaption.Image = CType(resources.GetObject("LayoutControlGroup3.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Image = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        resources.ApplyResources(Me.LayoutControlGroup3, "LayoutControlGroup3")
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(334, 393)
        '
        'LayoutControlItem4
        '
        resources.ApplyResources(Me.LayoutControlItem4, "LayoutControlItem4")
        Me.LayoutControlItem4.Control = Me.GridControl1
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(310, 334)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'FrmTemplateProduct
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "FrmTemplateProduct"
        Me.Opacity = 1.0R
        Me.Tag = "315"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents MemoEdit1 As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ButtonEdit1 As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
