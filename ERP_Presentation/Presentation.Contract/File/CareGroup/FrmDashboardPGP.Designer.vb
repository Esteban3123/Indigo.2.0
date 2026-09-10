Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardPGP
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
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleExpression1 As DevExpress.XtraEditors.FormatConditionRuleExpression = New DevExpress.XtraEditors.FormatConditionRuleExpression()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleExpression2 As DevExpress.XtraEditors.FormatConditionRuleExpression = New DevExpress.XtraEditors.FormatConditionRuleExpression()
        Me.INDcolVariation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtVariation = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnProcess = New DevExpress.XtraEditors.SimpleButton()
        Me.CtrMonthEnd = New Presentation.Controls.CtrDateNavigator()
        Me.CtrMonthInitial = New Presentation.Controls.CtrDateNavigator()
        Me.INDgcInfo = New DevExpress.XtraGrid.GridControl()
        Me.INDviewInfo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepNumber = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCash = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemInfo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtVariation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDgcInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
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
        '
        'INDcolVariation
        '
        Me.INDcolVariation.Caption = "Variación"
        Me.INDcolVariation.ColumnEdit = Me.INDtxtVariation
        Me.INDcolVariation.FieldName = "Variation"
        Me.INDcolVariation.Name = "INDcolVariation"
        Me.INDcolVariation.OptionsColumn.AllowEdit = False
        Me.INDcolVariation.OptionsColumn.AllowFocus = False
        Me.INDcolVariation.Visible = True
        Me.INDcolVariation.VisibleIndex = 11
        Me.INDcolVariation.Width = 121
        '
        'INDtxtVariation
        '
        Me.INDtxtVariation.AutoHeight = False
        Me.INDtxtVariation.Mask.EditMask = "P2"
        Me.INDtxtVariation.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtVariation.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtVariation.Name = "INDtxtVariation"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(4, 9)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 565)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDbtnProcess)
        Me.INDlyRoot.Controls.Add(Me.CtrMonthEnd)
        Me.INDlyRoot.Controls.Add(Me.CtrMonthInitial)
        Me.INDlyRoot.Controls.Add(Me.INDgcInfo)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(204, 9)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(1257, 565)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDbtnProcess
        '
        Me.INDbtnProcess.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnProcess.Appearance.Options.UseFont = True
        Me.INDbtnProcess.Location = New System.Drawing.Point(729, 43)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnProcess, True)
        Me.INDbtnProcess.Name = "INDbtnProcess"
        Me.INDbtnProcess.Size = New System.Drawing.Size(270, 26)
        Me.INDbtnProcess.StyleController = Me.INDlyRoot
        Me.INDbtnProcess.TabIndex = 7
        Me.INDbtnProcess.Text = "Consultar"
        '
        'CtrMonthEnd
        '
        Me.CtrMonthEnd.CtrCalendar = Nothing
        Me.CtrMonthEnd.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.OnlyMonth
        Me.CtrMonthEnd.Location = New System.Drawing.Point(424, 43)
        Me.CtrMonthEnd.Name = "CtrMonthEnd"
        Me.CtrMonthEnd.Size = New System.Drawing.Size(295, 32)
        Me.CtrMonthEnd.TabIndex = 6
        Me.CtrMonthEnd.WithEvent = True
        '
        'CtrMonthInitial
        '
        Me.CtrMonthInitial.CtrCalendar = Nothing
        Me.CtrMonthInitial.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.OnlyMonth
        Me.CtrMonthInitial.Location = New System.Drawing.Point(64, 43)
        Me.CtrMonthInitial.Name = "CtrMonthInitial"
        Me.CtrMonthInitial.Size = New System.Drawing.Size(295, 32)
        Me.CtrMonthInitial.TabIndex = 5
        Me.CtrMonthInitial.WithEvent = True
        '
        'INDgcInfo
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInfo, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInfo, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInfo, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInfo, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInfo, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInfo, False)
        Me.INDgcInfo.Location = New System.Drawing.Point(9, 85)
        Me.INDgcInfo.MainView = Me.INDviewInfo
        Me.INDgcInfo.Name = "INDgcInfo"
        Me.INDgcInfo.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDtxtVariation, Me.INDrepNumber, Me.RepositoryItemTextEdit1, Me.INDrepCash})
        Me.INDgcInfo.Size = New System.Drawing.Size(990, 470)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInfo, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcInfo.TabIndex = 4
        Me.INDgcInfo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewInfo})
        '
        'INDviewInfo
        '
        Me.INDviewInfo.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewInfo.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewInfo.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewInfo.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewInfo.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewInfo.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewInfo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewInfo.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewInfo.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewInfo.Appearance.Row.Options.UseFont = True
        Me.INDviewInfo.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewInfo.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewInfo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.INDcolVariation})
        GridFormatRule1.Column = Me.INDcolVariation
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleExpression1.Expression = "[Variation] <= 100"
        FormatConditionRuleExpression1.PredefinedName = "Green Fill"
        GridFormatRule1.Rule = FormatConditionRuleExpression1
        GridFormatRule2.Column = Me.INDcolVariation
        GridFormatRule2.Name = "Format1"
        FormatConditionRuleExpression2.Expression = "[Variation] > 100"
        FormatConditionRuleExpression2.PredefinedName = "Red Fill"
        GridFormatRule2.Rule = FormatConditionRuleExpression2
        Me.INDviewInfo.FormatRules.Add(GridFormatRule1)
        Me.INDviewInfo.FormatRules.Add(GridFormatRule2)
        Me.INDviewInfo.GridControl = Me.INDgcInfo
        Me.INDviewInfo.Name = "INDviewInfo"
        Me.INDviewInfo.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewInfo.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewInfo.OptionsView.ShowAutoFilterRow = True
        Me.INDviewInfo.OptionsView.ShowDetailButtons = False
        Me.INDviewInfo.OptionsView.ShowFooter = True
        Me.INDviewInfo.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewInfo, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Agrupador"
        Me.GridColumn1.FieldName = "GrouperName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 198
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "# de Usuarios"
        Me.GridColumn2.ColumnEdit = Me.INDrepNumber
        Me.GridColumn2.DisplayFormat.FormatString = "N0"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn2.FieldName = "UserNumber"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 108
        '
        'INDrepNumber
        '
        Me.INDrepNumber.AutoHeight = False
        Me.INDrepNumber.Mask.EditMask = "n0"
        Me.INDrepNumber.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepNumber.Name = "INDrepNumber"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Min"
        Me.GridColumn3.ColumnEdit = Me.INDrepNumber
        Me.GridColumn3.DisplayFormat.FormatString = "N0"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "UserMin"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 108
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Max"
        Me.GridColumn4.ColumnEdit = Me.INDrepNumber
        Me.GridColumn4.DisplayFormat.FormatString = "N0"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.FieldName = "UserMax"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 108
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "CME Proyectado"
        Me.GridColumn5.ColumnEdit = Me.INDrepCash
        Me.GridColumn5.DisplayFormat.FormatString = "C0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "ProjectCME"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 108
        '
        'INDrepCash
        '
        Me.INDrepCash.AutoHeight = False
        Me.INDrepCash.Mask.EditMask = "C0"
        Me.INDrepCash.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepCash.Name = "INDrepCash"
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Frecuencia"
        Me.GridColumn6.FieldName = "Frequence"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 108
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Total Contratado"
        Me.GridColumn7.ColumnEdit = Me.INDrepCash
        Me.GridColumn7.DisplayFormat.FormatString = "C0"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "TotalContract"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 108
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Valor por Usuario"
        Me.GridColumn8.ColumnEdit = Me.INDrepCash
        Me.GridColumn8.DisplayFormat.FormatString = "C0"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "UserValue"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        Me.GridColumn8.Width = 108
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Eventos Ejecutados"
        Me.GridColumn9.ColumnEdit = Me.INDrepNumber
        Me.GridColumn9.DisplayFormat.FormatString = "N0"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "EjectEvent"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        Me.GridColumn9.Width = 108
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "CME Real"
        Me.GridColumn10.ColumnEdit = Me.INDrepCash
        Me.GridColumn10.DisplayFormat.FormatString = "C0"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn10.FieldName = "RealCME"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 9
        Me.GridColumn10.Width = 108
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Total Ejecutado"
        Me.GridColumn11.ColumnEdit = Me.INDrepCash
        Me.GridColumn11.DisplayFormat.FormatString = "C0"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn11.FieldName = "TotalEject"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 10
        Me.GridColumn11.Width = 108
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1257, 565)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemInfo, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1257, 565)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'INDlyItemInfo
        '
        Me.INDlyItemInfo.Control = Me.INDgcInfo
        Me.INDlyItemInfo.Location = New System.Drawing.Point(0, 42)
        Me.INDlyItemInfo.MaxSize = New System.Drawing.Size(1000, 0)
        Me.INDlyItemInfo.MinSize = New System.Drawing.Size(1000, 24)
        Me.INDlyItemInfo.Name = "INDlyItemInfo"
        Me.INDlyItemInfo.Size = New System.Drawing.Size(1249, 480)
        Me.INDlyItemInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInfo.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemInfo.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.CtrMonthInitial
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(360, 42)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(360, 42)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(360, 42)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "De: "
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.CtrMonthEnd
        Me.LayoutControlItem2.Location = New System.Drawing.Point(360, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(360, 42)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(360, 42)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(360, 42)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Hasta: "
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDbtnProcess
        Me.LayoutControlItem3.Location = New System.Drawing.Point(720, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(280, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(280, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(529, 42)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'FrmDashboardPGP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDashboardPGP"
        Me.Opacity = 1.0R
        Me.Text = "Dashboard PGP"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtVariation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDgcInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDgcInfo As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDviewInfo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDlyItemInfo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoDocumentViewer1 As IndigoDocumentViewer
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrMonthInitial As CtrDateNavigator
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrMonthEnd As CtrDateNavigator
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnProcess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolVariation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtVariation As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepNumber As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepCash As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
End Class
