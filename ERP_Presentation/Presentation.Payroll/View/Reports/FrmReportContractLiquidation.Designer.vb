Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportContractLiquidation
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
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbGenerarReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleEmployee = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDateEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate11 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgcContractLiquidation = New DevExpress.XtraGrid.GridControl()
        Me.IndigoGridLookUpControl11 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDColNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPanelContractLiquidation = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcContractLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPanelContractLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelContractLiquidation.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Dock = System.Windows.Forms.DockStyle.None
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 141)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(915, 375)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(915, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(915, 130)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 366)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSbGenerarReport)
        Me.LayoutControl1.Controls.Add(Me.INDDateStart)
        Me.LayoutControl1.Controls.Add(Me.INDDateEnd)
        Me.LayoutControl1.Controls.Add(Me.INDSleEmployee)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(711, 366)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDSbGenerarReport
        '
        Me.INDSbGenerarReport.Location = New System.Drawing.Point(24, 231)
        Me.INDSbGenerarReport.MaximumSize = New System.Drawing.Size(356, 36)
        Me.INDSbGenerarReport.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDSbGenerarReport.Name = "INDSbGenerarReport"
        Me.INDSbGenerarReport.Size = New System.Drawing.Size(356, 36)
        Me.INDSbGenerarReport.StyleController = Me.LayoutControl1
        Me.INDSbGenerarReport.TabIndex = 8
        Me.INDSbGenerarReport.Text = "Generar Reporte"
        '
        'INDDateStart
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDateStart, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDateStart, False)
        Me.IndigoDate11.SetCampoObligatorio(Me.INDDateStart, False)
        Me.INDDateStart.EditValue = Nothing
        Me.INDDateStart.EnterMoveNextControl = True
        Me.INDDateStart.Location = New System.Drawing.Point(24, 78)
        Me.IndigoTextEdit1.SetMascara(Me.INDDateStart, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate11.SetMascaraDate(Me.INDDateStart, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDateStart.Name = "INDDateStart"
        Me.INDDateStart.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart.Properties.Appearance.Options.UseFont = True
        Me.INDDateStart.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateStart.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateStart.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateStart.Size = New System.Drawing.Size(356, 28)
        Me.INDDateStart.StyleController = Me.LayoutControl1
        Me.INDDateStart.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDateStart, 0)
        '
        'INDDateEnd
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDateEnd, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDateEnd, False)
        Me.IndigoDate11.SetCampoObligatorio(Me.INDDateEnd, False)
        Me.INDDateEnd.EditValue = Nothing
        Me.INDDateEnd.EnterMoveNextControl = True
        Me.INDDateEnd.Location = New System.Drawing.Point(24, 134)
        Me.IndigoTextEdit1.SetMascara(Me.INDDateEnd, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate11.SetMascaraDate(Me.INDDateEnd, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDateEnd.Name = "INDDateEnd"
        Me.INDDateEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd.Properties.Appearance.Options.UseFont = True
        Me.INDDateEnd.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateEnd.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEnd.Size = New System.Drawing.Size(356, 28)
        Me.INDDateEnd.StyleController = Me.LayoutControl1
        Me.INDDateEnd.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDateEnd, 0)
        '
        'INDSleEmployee
        '
        Me.INDSleEmployee.AllowQueryOne = True
        Me.INDSleEmployee.Datasource = Nothing
        Me.INDSleEmployee.DisplayMember = "{Nit}  -  {Name}"
        Me.INDSleEmployee.DisplayNullText = ""
        Me.INDSleEmployee.EditValue = Nothing
        Me.INDSleEmployee.EnterMoveNextControl = True
        Me.INDSleEmployee.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleEmployee.IdOpenForm = 0
        Me.INDSleEmployee.IsReadOnly = False
        Me.INDSleEmployee.Location = New System.Drawing.Point(24, 190)
        Me.INDSleEmployee.MaskControl = DevExpress.XtraEditors.Mask.MaskType.None
        Me.INDSleEmployee.MaskEdit = ""
        Me.INDSleEmployee.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleEmployee.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleEmployee.Name = "INDSleEmployee"
        Me.INDSleEmployee.PopUpFormSize = New System.Drawing.Size(400, 300)
        Me.INDSleEmployee.Size = New System.Drawing.Size(356, 28)
        Me.INDSleEmployee.TabIndex = 4
        Me.INDSleEmployee.UseMaskAsDisplayFormat = False
        Me.INDSleEmployee.ValueMember = "Id"
        Me.INDSleEmployee.View = Me.SearchLookUpEditExView4
        '
        'SearchLookUpEditExView4
        '
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView4.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView4.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn2})
        Me.SearchLookUpEditExView4.Name = "SearchLookUpEditExView4"
        Me.SearchLookUpEditExView4.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView4.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView4.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView4.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView4.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView4.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView4.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEditExView4, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Documento"
        Me.GridColumn11.FieldName = "Nit"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(711, 366)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateStart, Me.INDLciDateEnd, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(691, 346)
        Me.LayoutControlGroup3.Text = "Criterios"
        '
        'INDLciDateStart
        '
        Me.INDLciDateStart.Control = Me.INDDateStart
        Me.INDLciDateStart.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciDateStart.CustomizationFormText = "Fecha Inicial:"
        Me.INDLciDateStart.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDateStart.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.Name = "INDLciDateStart"
        Me.INDLciDateStart.Size = New System.Drawing.Size(667, 56)
        Me.INDLciDateStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateStart.Text = "Fecha Inicial:"
        Me.INDLciDateStart.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDateStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateStart.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciDateStart.TextToControlDistance = 5
        '
        'INDLciDateEnd
        '
        Me.INDLciDateEnd.Control = Me.INDDateEnd
        Me.INDLciDateEnd.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciDateEnd.CustomizationFormText = "Fecha Final:"
        Me.INDLciDateEnd.Location = New System.Drawing.Point(0, 56)
        Me.INDLciDateEnd.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateEnd.Name = "INDLciDateEnd"
        Me.INDLciDateEnd.Size = New System.Drawing.Size(667, 56)
        Me.INDLciDateEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateEnd.Text = "Fecha Final:"
        Me.INDLciDateEnd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDateEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateEnd.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciDateEnd.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSleEmployee
        Me.LayoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem1.CustomizationFormText = "Empleado:"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 112)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(360, 56)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(360, 56)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(667, 56)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Empleado:"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbGenerarReport
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 168)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(667, 125)
        Me.LayoutControlItem2.Text = "+"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 10
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        Me.IndigoGridView11.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit11
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.GridControl = Me.INDgcContractLiquidation
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDgcContractLiquidation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcContractLiquidation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcContractLiquidation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcContractLiquidation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcContractLiquidation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcContractLiquidation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcContractLiquidation, False)
        Me.INDgcContractLiquidation.Location = New System.Drawing.Point(12, 12)
        Me.INDgcContractLiquidation.MainView = Me.GridView1
        Me.INDgcContractLiquidation.Name = "INDgcContractLiquidation"
        Me.INDgcContractLiquidation.Size = New System.Drawing.Size(887, 105)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcContractLiquidation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcContractLiquidation.TabIndex = 4
        Me.INDgcContractLiquidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(911, 129)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDgcContractLiquidation
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(891, 109)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDColNit
        '
        Me.INDColNit.Caption = "Nit"
        Me.INDColNit.FieldName = "Nit"
        Me.INDColNit.Name = "INDColNit"
        Me.INDColNit.Visible = True
        Me.INDColNit.VisibleIndex = 0
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "Name"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 1
        '
        'INDPanelContractLiquidation
        '
        Me.INDPanelContractLiquidation.Controls.Add(Me.LayoutControl2)
        Me.INDPanelContractLiquidation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDPanelContractLiquidation.Location = New System.Drawing.Point(0, 545)
        Me.INDPanelContractLiquidation.Name = "INDPanelContractLiquidation"
        Me.INDPanelContractLiquidation.Size = New System.Drawing.Size(915, 133)
        Me.INDPanelContractLiquidation.TabIndex = 10
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDgcContractLiquidation)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(2, 2)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(911, 129)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'FrmReportContractLiquidation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(915, 678)
        Me.Controls.Add(Me.INDPanelContractLiquidation)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmReportContractLiquidation"
        Me.Opacity = 1.0R
        Me.Text = "FrmReportContractLiquidation"
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDPanelContractLiquidation, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcContractLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPanelContractLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelContractLiquidation.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate11 As IndigoDate
    Friend WithEvents INDDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDSleEmployee As SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView11 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDateEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridLookUpControl11 As IndigoGridLookUpControl
    Friend WithEvents INDSbGenerarReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDColNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPanelContractLiquidation As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcContractLiquidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
