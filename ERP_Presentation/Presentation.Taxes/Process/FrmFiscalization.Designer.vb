Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFiscalization
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
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyFiscalization = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcReteICA = New DevExpress.XtraGrid.GridControl()
        Me.viewReteIca = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcOmissive = New DevExpress.XtraGrid.GridControl()
        Me.viewOmissive = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDpopupLoad = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyLoad = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcLoad = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridLoad = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcDian = New DevExpress.XtraGrid.GridControl()
        Me.viewDian = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPbcProcess = New DevExpress.XtraEditors.ProgressBarControl()
        Me.INDgcPrivatePublic = New DevExpress.XtraGrid.GridControl()
        Me.viewPrivatePublic = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CtrDateNavigator1 = New Presentation.Controls.CtrDateNavigator()
        Me.INDbtnGenerate = New DevExpress.XtraEditors.SimpleButton()
        Me.INDpceLoad = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGenerate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLoad = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemProgress = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygConsult = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlygDifDIAN = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDifPrivateAndPublic = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemConsult = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygOmissive = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygReteICA = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyFiscalization, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyFiscalization.SuspendLayout()
        CType(Me.INDgcReteICA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewReteIca, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcOmissive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewOmissive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopupLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupLoad.SuspendLayout()
        CType(Me.INDlyLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyLoad.SuspendLayout()
        CType(Me.INDgcLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewDian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPbcProcess.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcPrivatePublic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewPrivatePublic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceLoad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGenerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLoad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygConsult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDifDIAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDifPrivateAndPublic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemConsult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygOmissive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygReteICA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyFiscalization)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyFiscalization
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 574)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyFiscalization
        '
        Me.INDlyFiscalization.Controls.Add(Me.INDgcReteICA)
        Me.INDlyFiscalization.Controls.Add(Me.INDgcOmissive)
        Me.INDlyFiscalization.Controls.Add(Me.INDpopupLoad)
        Me.INDlyFiscalization.Controls.Add(Me.INDgcDian)
        Me.INDlyFiscalization.Controls.Add(Me.INDPbcProcess)
        Me.INDlyFiscalization.Controls.Add(Me.INDgcPrivatePublic)
        Me.INDlyFiscalization.Controls.Add(Me.CtrDateNavigator1)
        Me.INDlyFiscalization.Controls.Add(Me.INDbtnGenerate)
        Me.INDlyFiscalization.Controls.Add(Me.INDpceLoad)
        Me.INDlyFiscalization.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyFiscalization.Location = New System.Drawing.Point(202, 7)
        Me.INDlyFiscalization.Name = "INDlyFiscalization"
        Me.INDlyFiscalization.Root = Me.LayoutControlGroup1
        Me.INDlyFiscalization.Size = New System.Drawing.Size(1261, 574)
        Me.INDlyFiscalization.TabIndex = 1
        Me.INDlyFiscalization.Text = "LayoutControl1"
        '
        'INDgcReteICA
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcReteICA, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcReteICA, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcReteICA, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcReteICA, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcReteICA, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcReteICA, False)
        Me.INDgcReteICA.Location = New System.Drawing.Point(437, 101)
        Me.INDgcReteICA.MainView = Me.viewReteIca
        Me.INDgcReteICA.Name = "INDgcReteICA"
        Me.INDgcReteICA.Size = New System.Drawing.Size(824, 420)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcReteICA, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcReteICA, New System.Drawing.Size(828, 0))
        Me.INDgcReteICA.TabIndex = 12
        Me.INDgcReteICA.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewReteIca})
        '
        'viewReteIca
        '
        Me.viewReteIca.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewReteIca.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewReteIca.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewReteIca.Appearance.FocusedRow.Options.UseFont = True
        Me.viewReteIca.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewReteIca.Appearance.GroupRow.Options.UseFont = True
        Me.viewReteIca.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewReteIca.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewReteIca.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewReteIca.Appearance.Row.Options.UseFont = True
        Me.viewReteIca.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewReteIca.Appearance.ViewCaption.Options.UseFont = True
        Me.viewReteIca.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn22, Me.GridColumn23, Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27, Me.GridColumn28})
        Me.viewReteIca.GridControl = Me.INDgcReteICA
        Me.viewReteIca.Name = "viewReteIca"
        Me.viewReteIca.OptionsView.EnableAppearanceEvenRow = True
        Me.viewReteIca.OptionsView.EnableAppearanceOddRow = True
        Me.viewReteIca.OptionsView.ShowAutoFilterRow = True
        Me.viewReteIca.OptionsView.ShowDetailButtons = False
        Me.viewReteIca.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewReteIca, False)
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Nit"
        Me.GridColumn22.FieldName = "Nit"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.OptionsColumn.AllowFocus = False
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 0
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Razón Social"
        Me.GridColumn23.FieldName = "ReasonSocial"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.OptionsColumn.AllowFocus = False
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 1
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Dirección"
        Me.GridColumn24.FieldName = "Addresss"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsColumn.AllowFocus = False
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 2
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Teléfono"
        Me.GridColumn25.FieldName = "Phone"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.OptionsColumn.AllowFocus = False
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 3
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Vr. Columna 1"
        Me.GridColumn26.DisplayFormat.FormatString = "C0"
        Me.GridColumn26.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn26.FieldName = "ValueOne"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.OptionsColumn.AllowEdit = False
        Me.GridColumn26.OptionsColumn.AllowFocus = False
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 4
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Vr. Columna 2"
        Me.GridColumn27.DisplayFormat.FormatString = "C0"
        Me.GridColumn27.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn27.FieldName = "ValueTwo"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.OptionsColumn.AllowEdit = False
        Me.GridColumn27.OptionsColumn.AllowFocus = False
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 5
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Diferencia"
        Me.GridColumn28.DisplayFormat.FormatString = "C0"
        Me.GridColumn28.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn28.FieldName = "DifferenceValue"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.OptionsColumn.AllowEdit = False
        Me.GridColumn28.OptionsColumn.AllowFocus = False
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 6
        '
        'INDgcOmissive
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcOmissive, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcOmissive, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcOmissive, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcOmissive, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcOmissive, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcOmissive, False)
        Me.INDgcOmissive.Location = New System.Drawing.Point(437, 101)
        Me.INDgcOmissive.MainView = Me.viewOmissive
        Me.INDgcOmissive.Name = "INDgcOmissive"
        Me.INDgcOmissive.Size = New System.Drawing.Size(824, 420)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcOmissive, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcOmissive, New System.Drawing.Size(828, 0))
        Me.INDgcOmissive.TabIndex = 11
        Me.INDgcOmissive.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewOmissive})
        '
        'viewOmissive
        '
        Me.viewOmissive.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewOmissive.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewOmissive.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewOmissive.Appearance.FocusedRow.Options.UseFont = True
        Me.viewOmissive.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewOmissive.Appearance.GroupRow.Options.UseFont = True
        Me.viewOmissive.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewOmissive.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewOmissive.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewOmissive.Appearance.Row.Options.UseFont = True
        Me.viewOmissive.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewOmissive.Appearance.ViewCaption.Options.UseFont = True
        Me.viewOmissive.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn18, Me.GridColumn19, Me.GridColumn20, Me.GridColumn21})
        Me.viewOmissive.GridControl = Me.INDgcOmissive
        Me.viewOmissive.Name = "viewOmissive"
        Me.viewOmissive.OptionsView.EnableAppearanceEvenRow = True
        Me.viewOmissive.OptionsView.EnableAppearanceOddRow = True
        Me.viewOmissive.OptionsView.ShowAutoFilterRow = True
        Me.viewOmissive.OptionsView.ShowDetailButtons = False
        Me.viewOmissive.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewOmissive, False)
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Nit"
        Me.GridColumn18.FieldName = "Nit"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 0
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Razón Social"
        Me.GridColumn19.FieldName = "ReasonSocial"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 1
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Dirección"
        Me.GridColumn20.FieldName = "Addresss"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.OptionsColumn.AllowEdit = False
        Me.GridColumn20.OptionsColumn.AllowFocus = False
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 2
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Teléfono"
        Me.GridColumn21.FieldName = "Phone"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 3
        '
        'INDpopupLoad
        '
        Me.INDpopupLoad.Controls.Add(Me.INDlyLoad)
        Me.INDpopupLoad.Location = New System.Drawing.Point(65, 509)
        Me.INDpopupLoad.Name = "INDpopupLoad"
        Me.INDpopupLoad.Size = New System.Drawing.Size(599, 343)
        Me.INDpopupLoad.TabIndex = 8
        '
        'INDlyLoad
        '
        Me.INDlyLoad.Controls.Add(Me.INDgcLoad)
        Me.INDlyLoad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyLoad.Location = New System.Drawing.Point(0, 0)
        Me.INDlyLoad.Name = "INDlyLoad"
        Me.INDlyLoad.Root = Me.LayoutControlGroup2
        Me.INDlyLoad.Size = New System.Drawing.Size(599, 343)
        Me.INDlyLoad.TabIndex = 0
        Me.INDlyLoad.Text = "LayoutControl1"
        '
        'INDgcLoad
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLoad, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLoad, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLoad, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLoad, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLoad, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLoad, False)
        Me.INDgcLoad.Location = New System.Drawing.Point(12, 12)
        Me.INDgcLoad.MainView = Me.GridView2
        Me.INDgcLoad.Name = "INDgcLoad"
        Me.INDgcLoad.Size = New System.Drawing.Size(575, 319)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLoad, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcLoad, New System.Drawing.Size(579, 0))
        Me.INDgcLoad.TabIndex = 4
        Me.INDgcLoad.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView2.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8, Me.GridColumn9, Me.GridColumn10})
        Me.GridView2.GridControl = Me.INDgcLoad
        Me.GridView2.GroupCount = 1
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowDetailButtons = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn8, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Estado"
        Me.GridColumn8.FieldName = "Message"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Nit"
        Me.GridColumn9.FieldName = "Nit"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Valor"
        Me.GridColumn10.DisplayFormat.FormatString = "C0"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn10.FieldName = "Value"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridLoad})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(599, 343)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemGridLoad
        '
        Me.INDlyItemGridLoad.Control = Me.INDgcLoad
        Me.INDlyItemGridLoad.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemGridLoad.Name = "INDlyItemGridLoad"
        Me.INDlyItemGridLoad.Size = New System.Drawing.Size(579, 323)
        Me.INDlyItemGridLoad.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridLoad.TextVisible = False
        '
        'INDgcDian
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDian, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDian, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDian, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDian, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDian, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDian, False)
        Me.INDgcDian.Location = New System.Drawing.Point(437, 101)
        Me.INDgcDian.MainView = Me.viewDian
        Me.INDgcDian.Name = "INDgcDian"
        Me.INDgcDian.Size = New System.Drawing.Size(824, 420)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDian, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcDian, New System.Drawing.Size(828, 0))
        Me.INDgcDian.TabIndex = 10
        Me.INDgcDian.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewDian})
        '
        'viewDian
        '
        Me.viewDian.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewDian.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewDian.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewDian.Appearance.FocusedRow.Options.UseFont = True
        Me.viewDian.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewDian.Appearance.GroupRow.Options.UseFont = True
        Me.viewDian.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewDian.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewDian.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewDian.Appearance.Row.Options.UseFont = True
        Me.viewDian.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewDian.Appearance.ViewCaption.Options.UseFont = True
        Me.viewDian.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17})
        Me.viewDian.GridControl = Me.INDgcDian
        Me.viewDian.Name = "viewDian"
        Me.viewDian.OptionsView.EnableAppearanceEvenRow = True
        Me.viewDian.OptionsView.EnableAppearanceOddRow = True
        Me.viewDian.OptionsView.ShowAutoFilterRow = True
        Me.viewDian.OptionsView.ShowDetailButtons = False
        Me.viewDian.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewDian, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Nit"
        Me.GridColumn11.FieldName = "Nit"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Razón Social"
        Me.GridColumn12.FieldName = "ReasonSocial"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Dirección"
        Me.GridColumn13.FieldName = "Addresss"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 2
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Teléfono"
        Me.GridColumn14.FieldName = "Phone"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 3
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Vr. Columna 1"
        Me.GridColumn15.DisplayFormat.FormatString = "C0"
        Me.GridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn15.FieldName = "ValueOne"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 4
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Vr. Columna 2"
        Me.GridColumn16.DisplayFormat.FormatString = "C0"
        Me.GridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn16.FieldName = "ValueTwo"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 5
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Diferencia"
        Me.GridColumn17.DisplayFormat.FormatString = "C0"
        Me.GridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn17.FieldName = "DifferenceValue"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 6
        '
        'INDPbcProcess
        '
        Me.INDPbcProcess.Location = New System.Drawing.Point(11, 192)
        Me.INDPbcProcess.Name = "INDPbcProcess"
        Me.INDPbcProcess.Properties.ShowTitle = True
        Me.INDPbcProcess.Size = New System.Drawing.Size(386, 18)
        Me.INDPbcProcess.StyleController = Me.INDlyFiscalization
        Me.INDPbcProcess.TabIndex = 9
        '
        'INDgcPrivatePublic
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcPrivatePublic, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcPrivatePublic, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcPrivatePublic, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcPrivatePublic, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcPrivatePublic, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcPrivatePublic, False)
        Me.INDgcPrivatePublic.Location = New System.Drawing.Point(437, 101)
        Me.INDgcPrivatePublic.MainView = Me.viewPrivatePublic
        Me.INDgcPrivatePublic.Name = "INDgcPrivatePublic"
        Me.INDgcPrivatePublic.Size = New System.Drawing.Size(824, 420)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcPrivatePublic, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDgcPrivatePublic, New System.Drawing.Size(828, 0))
        Me.INDgcPrivatePublic.TabIndex = 6
        Me.INDgcPrivatePublic.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewPrivatePublic})
        '
        'viewPrivatePublic
        '
        Me.viewPrivatePublic.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewPrivatePublic.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewPrivatePublic.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewPrivatePublic.Appearance.FocusedRow.Options.UseFont = True
        Me.viewPrivatePublic.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewPrivatePublic.Appearance.GroupRow.Options.UseFont = True
        Me.viewPrivatePublic.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewPrivatePublic.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewPrivatePublic.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewPrivatePublic.Appearance.Row.Options.UseFont = True
        Me.viewPrivatePublic.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewPrivatePublic.Appearance.ViewCaption.Options.UseFont = True
        Me.viewPrivatePublic.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.viewPrivatePublic.GridControl = Me.INDgcPrivatePublic
        Me.viewPrivatePublic.Name = "viewPrivatePublic"
        Me.viewPrivatePublic.OptionsView.EnableAppearanceEvenRow = True
        Me.viewPrivatePublic.OptionsView.EnableAppearanceOddRow = True
        Me.viewPrivatePublic.OptionsView.ShowAutoFilterRow = True
        Me.viewPrivatePublic.OptionsView.ShowDetailButtons = False
        Me.viewPrivatePublic.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewPrivatePublic, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nit"
        Me.GridColumn1.FieldName = "Nit"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Razón Social"
        Me.GridColumn2.FieldName = "ReasonSocial"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Dirección"
        Me.GridColumn3.FieldName = "Addresss"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Teléfono"
        Me.GridColumn4.FieldName = "Phone"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Vr. Columna 1"
        Me.GridColumn5.DisplayFormat.FormatString = "C0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "ValueOne"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Vr. Columna 2"
        Me.GridColumn6.DisplayFormat.FormatString = "C0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "ValueTwo"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Diferencia"
        Me.GridColumn7.DisplayFormat.FormatString = "C0"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "DifferenceValue"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        '
        'CtrDateNavigator1
        '
        Me.CtrDateNavigator1.CtrCalendar = Nothing
        Me.CtrDateNavigator1.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.CtrDateNavigator1.Location = New System.Drawing.Point(11, 92)
        Me.CtrDateNavigator1.Name = "CtrDateNavigator1"
        Me.CtrDateNavigator1.Size = New System.Drawing.Size(386, 32)
        Me.CtrDateNavigator1.TabIndex = 5
        Me.CtrDateNavigator1.WithEvent = True
        '
        'INDbtnGenerate
        '
        Me.INDbtnGenerate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnGenerate.Appearance.Options.UseFont = True
        Me.INDbtnGenerate.Location = New System.Drawing.Point(11, 214)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnGenerate, True)
        Me.INDbtnGenerate.Name = "INDbtnGenerate"
        Me.INDbtnGenerate.Size = New System.Drawing.Size(386, 32)
        Me.INDbtnGenerate.StyleController = Me.INDlyFiscalization
        Me.INDbtnGenerate.TabIndex = 4
        Me.INDbtnGenerate.Text = "Generar Consulta"
        '
        'INDpceLoad
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceLoad, False)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceLoad, Nothing)
        Me.INDpceLoad.Location = New System.Drawing.Point(11, 154)
        Me.INDpceLoad.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDpceLoad.Name = "INDpceLoad"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceLoad, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceLoad, False)
        Me.INDpceLoad.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpceLoad.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceLoad.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceLoad.Properties.Appearance.Options.UseFont = True
        Me.INDpceLoad.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceLoad.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDpceLoad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDpceLoad.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceLoad.Properties.PopupControl = Me.INDpopupLoad
        Me.INDpceLoad.Size = New System.Drawing.Size(386, 32)
        Me.INDpceLoad.StyleController = Me.INDlyFiscalization
        Me.INDpceLoad.TabIndex = 7
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceLoad, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceLoad, Nothing)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygConsult})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(-13, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1310, 557)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygPrincipalData
        '
        Me.INDlygPrincipalData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalData, False)
        Me.INDlygPrincipalData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGenerate, Me.INDlyItemPeriod, Me.INDlyItemLoad, Me.INDlyItemProgress})
        Me.INDlygPrincipalData.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalData.Name = "INDlygPrincipalData"
        Me.INDlygPrincipalData.Size = New System.Drawing.Size(414, 537)
        Me.INDlygPrincipalData.Text = "Datos Principales"
        '
        'INDlyItemGenerate
        '
        Me.INDlyItemGenerate.Control = Me.INDbtnGenerate
        Me.INDlyItemGenerate.Location = New System.Drawing.Point(0, 155)
        Me.INDlyItemGenerate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemGenerate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemGenerate.Name = "INDlyItemGenerate"
        Me.INDlyItemGenerate.Size = New System.Drawing.Size(390, 323)
        Me.INDlyItemGenerate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGenerate.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGenerate.TextVisible = False
        '
        'INDlyItemPeriod
        '
        Me.INDlyItemPeriod.Control = Me.CtrDateNavigator1
        Me.INDlyItemPeriod.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPeriod.MaxSize = New System.Drawing.Size(390, 69)
        Me.INDlyItemPeriod.MinSize = New System.Drawing.Size(390, 69)
        Me.INDlyItemPeriod.Name = "INDlyItemPeriod"
        Me.INDlyItemPeriod.Size = New System.Drawing.Size(390, 69)
        Me.INDlyItemPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPeriod.Text = "Periodo Gravable"
        Me.INDlyItemPeriod.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPeriod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPeriod.TextSize = New System.Drawing.Size(135, 28)
        Me.INDlyItemPeriod.TextToControlDistance = 5
        '
        'INDlyItemLoad
        '
        Me.INDlyItemLoad.Control = Me.INDpceLoad
        Me.INDlyItemLoad.Location = New System.Drawing.Point(0, 69)
        Me.INDlyItemLoad.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemLoad.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemLoad.Name = "INDlyItemLoad"
        Me.INDlyItemLoad.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemLoad.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLoad.Text = "Cargue Base Gravable DIAN"
        Me.INDlyItemLoad.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLoad.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemLoad.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemLoad.TextToControlDistance = 5
        '
        'INDlyItemProgress
        '
        Me.INDlyItemProgress.Control = Me.INDPbcProcess
        Me.INDlyItemProgress.Location = New System.Drawing.Point(0, 133)
        Me.INDlyItemProgress.Name = "INDlyItemProgress"
        Me.INDlyItemProgress.Size = New System.Drawing.Size(390, 22)
        Me.INDlyItemProgress.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemProgress.TextVisible = False
        Me.INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlygConsult
        '
        Me.INDlygConsult.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygConsult.AppearanceGroup.Options.UseFont = True
        Me.INDlygConsult.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygConsult.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygConsult.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygConsult.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygConsult.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygConsult.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygConsult.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygConsult.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygConsult.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygConsult.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygConsult.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygConsult.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygConsult, False)
        Me.INDlygConsult.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup1})
        Me.INDlygConsult.Location = New System.Drawing.Point(414, 0)
        Me.INDlygConsult.Name = "INDlygConsult"
        Me.INDlygConsult.Size = New System.Drawing.Size(876, 537)
        Me.INDlygConsult.Text = "Datos Consulta"
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.INDlygReteICA
        Me.TabbedControlGroup1.SelectedTabPageIndex = 3
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(852, 478)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygDifPrivateAndPublic, Me.INDlygDifDIAN, Me.INDlygOmissive, Me.INDlygReteICA})
        '
        'INDlygDifDIAN
        '
        Me.INDlygDifDIAN.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDifDIAN.AppearanceGroup.Options.UseFont = True
        Me.INDlygDifDIAN.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDifDIAN.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDifDIAN.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDifDIAN.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDifDIAN.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDifDIAN.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDifDIAN.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDifDIAN.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDifDIAN.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDifDIAN.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDifDIAN.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDifDIAN.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDifDIAN, False)
        Me.INDlygDifDIAN.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDlygDifDIAN.Location = New System.Drawing.Point(0, 0)
        Me.INDlygDifDIAN.Name = "INDlygDifDIAN"
        Me.INDlygDifDIAN.Size = New System.Drawing.Size(828, 424)
        Me.INDlygDifDIAN.Text = "Dif.  Base Tributaria y Liq. Privada"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcDian
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 424)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDlygDifPrivateAndPublic
        '
        Me.INDlygDifPrivateAndPublic.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDifPrivateAndPublic.AppearanceGroup.Options.UseFont = True
        Me.INDlygDifPrivateAndPublic.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDifPrivateAndPublic.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDifPrivateAndPublic.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDifPrivateAndPublic, False)
        Me.INDlygDifPrivateAndPublic.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemConsult})
        Me.INDlygDifPrivateAndPublic.Location = New System.Drawing.Point(0, 0)
        Me.INDlygDifPrivateAndPublic.Name = "INDlygDifPrivateAndPublic"
        Me.INDlygDifPrivateAndPublic.Size = New System.Drawing.Size(828, 424)
        Me.INDlygDifPrivateAndPublic.Text = "Dif. Liq. Oficial y Privada"
        '
        'INDlyItemConsult
        '
        Me.INDlyItemConsult.Control = Me.INDgcPrivatePublic
        Me.INDlyItemConsult.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemConsult.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemConsult.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemConsult.Name = "INDlyItemConsult"
        Me.INDlyItemConsult.Size = New System.Drawing.Size(828, 424)
        Me.INDlyItemConsult.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemConsult.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemConsult.TextVisible = False
        '
        'INDlygOmissive
        '
        Me.INDlygOmissive.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygOmissive.AppearanceGroup.Options.UseFont = True
        Me.INDlygOmissive.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygOmissive.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygOmissive.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygOmissive.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygOmissive.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygOmissive.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygOmissive.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygOmissive.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygOmissive.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygOmissive.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygOmissive.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygOmissive.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygOmissive, False)
        Me.INDlygOmissive.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.INDlygOmissive.Location = New System.Drawing.Point(0, 0)
        Me.INDlygOmissive.Name = "INDlygOmissive"
        Me.INDlygOmissive.Size = New System.Drawing.Size(828, 424)
        Me.INDlygOmissive.Text = "Omisos"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDgcOmissive
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(828, 424)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDlygReteICA
        '
        Me.INDlygReteICA.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygReteICA.AppearanceGroup.Options.UseFont = True
        Me.INDlygReteICA.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygReteICA.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygReteICA.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReteICA.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygReteICA.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygReteICA.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygReteICA.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReteICA.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygReteICA.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReteICA.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygReteICA.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygReteICA.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygReteICA, False)
        Me.INDlygReteICA.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.INDlygReteICA.Location = New System.Drawing.Point(0, 0)
        Me.INDlygReteICA.Name = "INDlygReteICA"
        Me.INDlygReteICA.Size = New System.Drawing.Size(828, 424)
        Me.INDlygReteICA.Text = "Desc. ReteICA <> Decla. Privada"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDgcReteICA
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(828, 424)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'FrmFiscalization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFiscalization"
        Me.Opacity = 1.0R
        Me.Tag = "1879"
        Me.Text = "Fiscalización"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyFiscalization, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyFiscalization.ResumeLayout(False)
        CType(Me.INDgcReteICA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewReteIca, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcOmissive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewOmissive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopupLoad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupLoad.ResumeLayout(False)
        CType(Me.INDlyLoad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyLoad.ResumeLayout(False)
        CType(Me.INDgcLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewDian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPbcProcess.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcPrivatePublic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewPrivatePublic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceLoad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGenerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLoad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygConsult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDifDIAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDifPrivateAndPublic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemConsult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygOmissive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygReteICA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyFiscalization As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDbtnGenerate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemGenerate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrDateNavigator1 As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDlyItemPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcPrivatePublic As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewPrivatePublic As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygConsult As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemLoad As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpceLoad As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDpopupLoad As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyLoad As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcLoad As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemGridLoad As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPbcProcess As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents INDlyItemProgress As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlygDifPrivateAndPublic As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemConsult As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygDifDIAN As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlygOmissive As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlygReteICA As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcDian As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewDian As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcReteICA As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewReteIca As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgcOmissive As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewOmissive As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
End Class
