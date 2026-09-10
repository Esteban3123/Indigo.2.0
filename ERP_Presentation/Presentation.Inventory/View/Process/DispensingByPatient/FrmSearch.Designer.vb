<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSearch
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDdteInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcResult = New DevExpress.XtraGrid.GridControl()
        Me.INDviewResult = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepDteDate = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.INDbtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.INDdteEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSearch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemResult = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDlyButtons = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAccept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDgcResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepDteDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepDteDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInitialDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyButtons, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyButtons.SuspendLayout()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAccept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDdteInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteInitialDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteInitialDate, False)
        Me.INDdteInitialDate.EditValue = Nothing
        Me.INDdteInitialDate.EnterMoveNextControl = True
        Me.INDdteInitialDate.Location = New System.Drawing.Point(67, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteInitialDate, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
        Me.INDdteInitialDate.Name = "INDdteInitialDate"
        Me.INDdteInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteInitialDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDdteInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteInitialDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDdteInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteInitialDate.Size = New System.Drawing.Size(251, 28)
        Me.INDdteInitialDate.StyleController = Me.INDlyRoot
        Me.INDdteInitialDate.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteInitialDate, 0)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDgcResult)
        Me.INDlyRoot.Controls.Add(Me.INDbtnSearch)
        Me.INDlyRoot.Controls.Add(Me.INDdteEndDate)
        Me.INDlyRoot.Controls.Add(Me.INDdteInitialDate)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(766, 425)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDgcResult
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcResult, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcResult, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcResult, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcResult, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcResult, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcResult, False)
        Me.INDgcResult.Location = New System.Drawing.Point(12, 48)
        Me.INDgcResult.MainView = Me.INDviewResult
        Me.INDgcResult.Name = "INDgcResult"
        Me.INDgcResult.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepDteDate})
        Me.INDgcResult.Size = New System.Drawing.Size(742, 365)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcResult, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcResult.TabIndex = 3
        Me.INDgcResult.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewResult})
        '
        'INDviewResult
        '
        Me.INDviewResult.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewResult.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewResult.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewResult.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewResult.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewResult.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewResult.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewResult.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewResult.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewResult.Appearance.Row.Options.UseFont = True
        Me.INDviewResult.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewResult.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewResult.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.INDviewResult.GridControl = Me.INDgcResult
        Me.INDviewResult.Name = "INDviewResult"
        Me.INDviewResult.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewResult.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewResult.OptionsView.ShowAutoFilterRow = True
        Me.INDviewResult.OptionsView.ShowDetailButtons = False
        Me.INDviewResult.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewResult, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Paciente"
        Me.GridColumn1.FieldName = "identPaciente"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Fórmula"
        Me.GridColumn2.FieldName = "recetarioOMedica"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Fecha"
        Me.GridColumn3.ColumnEdit = Me.INDrepDteDate
        Me.GridColumn3.FieldName = "fechaRecetarioOMedica"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'INDrepDteDate
        '
        Me.INDrepDteDate.AutoHeight = False
        Me.INDrepDteDate.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepDteDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepDteDate.Mask.EditMask = "G"
        Me.INDrepDteDate.Name = "INDrepDteDate"
        '
        'INDbtnSearch
        '
        Me.INDbtnSearch.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnSearch.Appearance.Options.UseFont = True
        Me.INDbtnSearch.Location = New System.Drawing.Point(632, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnSearch, True)
        Me.INDbtnSearch.Name = "INDbtnSearch"
        Me.INDbtnSearch.Size = New System.Drawing.Size(122, 30)
        Me.INDbtnSearch.StyleController = Me.INDlyRoot
        Me.INDbtnSearch.TabIndex = 2
        Me.INDbtnSearch.Text = "Buscar"
        '
        'INDdteEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteEndDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteEndDate, False)
        Me.INDdteEndDate.EditValue = Nothing
        Me.INDdteEndDate.EnterMoveNextControl = True
        Me.INDdteEndDate.Location = New System.Drawing.Point(377, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteEndDate, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
        Me.INDdteEndDate.Name = "INDdteEndDate"
        Me.INDdteEndDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDdteEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDdteEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteEndDate.Size = New System.Drawing.Size(251, 28)
        Me.INDdteEndDate.StyleController = Me.INDlyRoot
        Me.INDdteEndDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteEndDate, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemInitialDate, Me.INDlyItemEndDate, Me.INDlyItemSearch, Me.INDlyItemResult})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(766, 425)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemInitialDate
        '
        Me.INDlyItemInitialDate.Control = Me.INDdteInitialDate
        Me.INDlyItemInitialDate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemInitialDate.MaxSize = New System.Drawing.Size(310, 36)
        Me.INDlyItemInitialDate.MinSize = New System.Drawing.Size(310, 36)
        Me.INDlyItemInitialDate.Name = "INDlyItemInitialDate"
        Me.INDlyItemInitialDate.Size = New System.Drawing.Size(310, 36)
        Me.INDlyItemInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitialDate.Text = "De"
        Me.INDlyItemInitialDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitialDate.TextSize = New System.Drawing.Size(50, 21)
        Me.INDlyItemInitialDate.TextToControlDistance = 5
        '
        'INDlyItemEndDate
        '
        Me.INDlyItemEndDate.Control = Me.INDdteEndDate
        Me.INDlyItemEndDate.Location = New System.Drawing.Point(310, 0)
        Me.INDlyItemEndDate.MaxSize = New System.Drawing.Size(310, 36)
        Me.INDlyItemEndDate.MinSize = New System.Drawing.Size(310, 36)
        Me.INDlyItemEndDate.Name = "INDlyItemEndDate"
        Me.INDlyItemEndDate.Size = New System.Drawing.Size(310, 36)
        Me.INDlyItemEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEndDate.Text = "Hasta"
        Me.INDlyItemEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEndDate.TextSize = New System.Drawing.Size(50, 21)
        Me.INDlyItemEndDate.TextToControlDistance = 5
        '
        'INDlyItemSearch
        '
        Me.INDlyItemSearch.Control = Me.INDbtnSearch
        Me.INDlyItemSearch.Location = New System.Drawing.Point(620, 0)
        Me.INDlyItemSearch.MaxSize = New System.Drawing.Size(0, 34)
        Me.INDlyItemSearch.MinSize = New System.Drawing.Size(1, 34)
        Me.INDlyItemSearch.Name = "INDlyItemSearch"
        Me.INDlyItemSearch.Size = New System.Drawing.Size(126, 36)
        Me.INDlyItemSearch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSearch.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSearch.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemSearch.TextToControlDistance = 0
        Me.INDlyItemSearch.TextVisible = False
        '
        'INDlyItemResult
        '
        Me.INDlyItemResult.Control = Me.INDgcResult
        Me.INDlyItemResult.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemResult.Name = "INDlyItemResult"
        Me.INDlyItemResult.Size = New System.Drawing.Size(746, 369)
        Me.INDlyItemResult.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemResult.TextVisible = False
        '
        'INDbtnAccept
        '
        Me.INDbtnAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAccept.Appearance.Options.UseFont = True
        Me.INDbtnAccept.Location = New System.Drawing.Point(12, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAccept, True)
        Me.INDbtnAccept.Name = "INDbtnAccept"
        Me.INDbtnAccept.Size = New System.Drawing.Size(367, 32)
        Me.INDbtnAccept.StyleController = Me.INDlyButtons
        Me.INDbtnAccept.TabIndex = 4
        Me.INDbtnAccept.Text = "Aceptar"
        '
        'INDlyButtons
        '
        Me.INDlyButtons.Controls.Add(Me.INDbtnCancel)
        Me.INDlyButtons.Controls.Add(Me.INDbtnAccept)
        Me.INDlyButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyButtons.Location = New System.Drawing.Point(2, 2)
        Me.INDlyButtons.Name = "INDlyButtons"
        Me.INDlyButtons.Root = Me.LayoutControlGroup2
        Me.INDlyButtons.Size = New System.Drawing.Size(762, 59)
        Me.INDlyButtons.TabIndex = 0
        Me.INDlyButtons.Text = "LayoutControl1"
        '
        'INDbtnCancel
        '
        Me.INDbtnCancel.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCancel.Appearance.Options.UseFont = True
        Me.INDbtnCancel.Location = New System.Drawing.Point(383, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnCancel, True)
        Me.INDbtnCancel.Name = "INDbtnCancel"
        Me.INDbtnCancel.Size = New System.Drawing.Size(367, 32)
        Me.INDbtnCancel.StyleController = Me.INDlyButtons
        Me.INDbtnCancel.TabIndex = 5
        Me.INDbtnCancel.Text = "Cancelar"
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
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAccept, Me.INDlyItemCancel})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(762, 59)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemAccept
        '
        Me.INDlyItemAccept.Control = Me.INDbtnAccept
        Me.INDlyItemAccept.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAccept.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemAccept.MinSize = New System.Drawing.Size(83, 36)
        Me.INDlyItemAccept.Name = "INDlyItemAccept"
        Me.INDlyItemAccept.Size = New System.Drawing.Size(371, 39)
        Me.INDlyItemAccept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAccept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAccept.TextVisible = False
        '
        'INDlyItemCancel
        '
        Me.INDlyItemCancel.Control = Me.INDbtnCancel
        Me.INDlyItemCancel.Location = New System.Drawing.Point(371, 0)
        Me.INDlyItemCancel.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlyItemCancel.MinSize = New System.Drawing.Size(73, 36)
        Me.INDlyItemCancel.Name = "INDlyItemCancel"
        Me.INDlyItemCancel.Size = New System.Drawing.Size(371, 39)
        Me.INDlyItemCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCancel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemCancel.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDlyButtons)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 425)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(766, 63)
        Me.PanelControl1.TabIndex = 1
        '
        'FrmSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(766, 488)
        Me.Controls.Add(Me.INDlyRoot)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSearch"
        Me.Text = "Búsqueda"
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDgcResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepDteDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepDteDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInitialDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyButtons, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyButtons.ResumeLayout(False)
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAccept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents IndigoDate1 As Controls.IndigoDate
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDdteEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemSearch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcResult As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewResult As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemResult As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyButtons As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemAccept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCancel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrepDteDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
End Class
