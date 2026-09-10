Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportSISMED
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGleTypeReport = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDeMonthEnd = New Presentation.Controls.CtrDateNavigator()
        Me.INDDeMonthStart = New Presentation.Controls.CtrDateNavigator()
        Me.INDSpnYear = New DevExpress.XtraEditors.SpinEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilters = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciMontStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciYear = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMonthEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTypeReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMontStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMonthEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 5)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 724)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1451, 130)
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateReport)
        Me.INDLcBase.Controls.Add(Me.INDGleTypeReport)
        Me.INDLcBase.Controls.Add(Me.INDDeMonthEnd)
        Me.INDLcBase.Controls.Add(Me.INDDeMonthStart)
        Me.INDLcBase.Controls.Add(Me.INDSpnYear)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(804, 715)
        Me.INDLcBase.TabIndex = 0
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(24, 293)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(356, 32)
        Me.INDSbGenerateReport.StyleController = Me.INDLcBase
        Me.INDSbGenerateReport.TabIndex = 5
        Me.INDSbGenerateReport.Text = "Generar"
        '
        'INDGleTypeReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleTypeReport, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleTypeReport, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleTypeReport, True)
        Me.INDGleTypeReport.Location = New System.Drawing.Point(24, 257)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleTypeReport, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleTypeReport.Name = "INDGleTypeReport"
        Me.INDGleTypeReport.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDGleTypeReport.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTypeReport.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleTypeReport.Properties.Appearance.Options.UseFont = True
        Me.INDGleTypeReport.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeReport.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeReport.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleTypeReport.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleTypeReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleTypeReport.Properties.DisplayMember = "Item2"
        Me.INDGleTypeReport.Properties.ImmediatePopup = True
        Me.INDGleTypeReport.Properties.NullText = ""
        Me.INDGleTypeReport.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleTypeReport.Properties.ValueMember = "Item1"
        Me.INDGleTypeReport.Size = New System.Drawing.Size(356, 28)
        Me.INDGleTypeReport.StyleController = Me.INDLcBase
        Me.INDGleTypeReport.TabIndex = 4
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleTypeReport, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleTypeReport, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo Reporte"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDDeMonthEnd
        '
        Me.INDDeMonthEnd.CtrCalendar = Nothing
        Me.INDDeMonthEnd.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.OnlyMonth
        Me.INDDeMonthEnd.Location = New System.Drawing.Point(24, 193)
        Me.INDDeMonthEnd.Name = "INDDeMonthEnd"
        Me.INDDeMonthEnd.Size = New System.Drawing.Size(356, 40)
        Me.INDDeMonthEnd.TabIndex = 3
        Me.INDDeMonthEnd.WithEvent = True
        '
        'INDDeMonthStart
        '
        Me.INDDeMonthStart.CtrCalendar = Nothing
        Me.INDDeMonthStart.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.OnlyMonth
        Me.INDDeMonthStart.Location = New System.Drawing.Point(24, 134)
        Me.INDDeMonthStart.Name = "INDDeMonthStart"
        Me.INDDeMonthStart.Size = New System.Drawing.Size(356, 35)
        Me.INDDeMonthStart.TabIndex = 2
        Me.INDDeMonthStart.WithEvent = True
        '
        'INDSpnYear
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnYear, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnYear, False)
        Me.INDSpnYear.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnYear.EnterMoveNextControl = True
        Me.INDSpnYear.Location = New System.Drawing.Point(24, 73)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnYear, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSpnYear.Name = "INDSpnYear"
        Me.INDSpnYear.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnYear.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnYear.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnYear.Properties.Appearance.Options.UseFont = True
        Me.INDSpnYear.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnYear.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSpnYear.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnYear.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSpnYear.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSpnYear.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnYear.Size = New System.Drawing.Size(356, 28)
        Me.INDSpnYear.StyleController = Me.INDLcBase
        Me.INDSpnYear.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnYear, 0)
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilters})
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(804, 715)
        Me.INDLcgBase.TextVisible = False
        '
        'INDLcgFilters
        '
        Me.INDLcgFilters.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilters.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilters.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilters.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgFilters, False)
        Me.INDLcgFilters.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciMontStart, Me.INDLciYear, Me.INDLciMonthEnd, Me.INDLciTypeReport, Me.INDLciGenerateReport})
        Me.INDLcgFilters.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilters.Name = "INDLcgFilters"
        Me.INDLcgFilters.Size = New System.Drawing.Size(784, 695)
        Me.INDLcgFilters.Text = "Criterios"
        '
        'INDLciMontStart
        '
        Me.INDLciMontStart.Control = Me.INDDeMonthStart
        Me.INDLciMontStart.Location = New System.Drawing.Point(0, 56)
        Me.INDLciMontStart.MaxSize = New System.Drawing.Size(360, 64)
        Me.INDLciMontStart.MinSize = New System.Drawing.Size(360, 64)
        Me.INDLciMontStart.Name = "INDLciMontStart"
        Me.INDLciMontStart.Size = New System.Drawing.Size(760, 64)
        Me.INDLciMontStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMontStart.Text = "Mes Inicial:"
        Me.INDLciMontStart.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciMontStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMontStart.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciMontStart.TextToControlDistance = 5
        '
        'INDLciYear
        '
        Me.INDLciYear.Control = Me.INDSpnYear
        Me.INDLciYear.Location = New System.Drawing.Point(0, 0)
        Me.INDLciYear.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciYear.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciYear.Name = "INDLciYear"
        Me.INDLciYear.Size = New System.Drawing.Size(760, 56)
        Me.INDLciYear.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciYear.Text = "Años:"
        Me.INDLciYear.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciYear.TextSize = New System.Drawing.Size(87, 17)
        '
        'INDLciMonthEnd
        '
        Me.INDLciMonthEnd.Control = Me.INDDeMonthEnd
        Me.INDLciMonthEnd.Location = New System.Drawing.Point(0, 120)
        Me.INDLciMonthEnd.MaxSize = New System.Drawing.Size(360, 64)
        Me.INDLciMonthEnd.MinSize = New System.Drawing.Size(360, 64)
        Me.INDLciMonthEnd.Name = "INDLciMonthEnd"
        Me.INDLciMonthEnd.Size = New System.Drawing.Size(760, 64)
        Me.INDLciMonthEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMonthEnd.Text = "Mes Final:"
        Me.INDLciMonthEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMonthEnd.TextSize = New System.Drawing.Size(87, 17)
        '
        'INDLciTypeReport
        '
        Me.INDLciTypeReport.Control = Me.INDGleTypeReport
        Me.INDLciTypeReport.Location = New System.Drawing.Point(0, 184)
        Me.INDLciTypeReport.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciTypeReport.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciTypeReport.Name = "INDLciTypeReport"
        Me.INDLciTypeReport.Size = New System.Drawing.Size(760, 56)
        Me.INDLciTypeReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTypeReport.Text = "Tipo Reporte:"
        Me.INDLciTypeReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTypeReport.TextSize = New System.Drawing.Size(87, 17)
        '
        'INDLciGenerateReport
        '
        Me.INDLciGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciGenerateReport.Location = New System.Drawing.Point(0, 240)
        Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(360, 36)
        Me.INDLciGenerateReport.MinSize = New System.Drawing.Size(360, 36)
        Me.INDLciGenerateReport.Name = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Size = New System.Drawing.Size(760, 402)
        Me.INDLciGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateReport.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcBase
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 715)
        Me.CtrNavigationControlPanel1.TabIndex = 1
        Me.CtrNavigationControlPanel1.Tag = ""
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmReportSISMED
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmReportSISMED"
        Me.Opacity = 1.0R
        Me.Tag = "1764"
        Me.Text = "Informe SISMED/SISDIS"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMontStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMonthEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDDeMonthEnd As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDDeMonthStart As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDSpnYear As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciMontStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciYear As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciMonthEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgFilters As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGleTypeReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciTypeReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGenerateReport As DevExpress.XtraLayout.LayoutControlItem
End Class
