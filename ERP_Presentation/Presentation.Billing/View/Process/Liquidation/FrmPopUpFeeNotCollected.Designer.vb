<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopUpFeeNotCollected
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMeObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSbSaveFeeNotCollected = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleReportType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSleTypeReport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTeValue = New DevExpress.XtraEditors.TextEdit()
        Me.LcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenFeeNotCollected = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReportType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LcRoot.SuspendLayout()
        CType(Me.INDMeObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleReportType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleTypeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenFeeNotCollected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'LcRoot
        '
        Me.LcRoot.Controls.Add(Me.INDMeObservations)
        Me.LcRoot.Controls.Add(Me.INDSbSaveFeeNotCollected)
        Me.LcRoot.Controls.Add(Me.INDSleReportType)
        Me.LcRoot.Controls.Add(Me.INDTeValue)
        Me.LcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LcRoot.Location = New System.Drawing.Point(0, 0)
        Me.LcRoot.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LcRoot.Name = "LcRoot"
        Me.LcRoot.Root = Me.LcgRoot
        Me.LcRoot.Size = New System.Drawing.Size(395, 366)
        Me.LcRoot.TabIndex = 0
        Me.LcRoot.Text = "LayoutControl1"
        '
        'INDMeObservations
        '
        Me.INDMeObservations.EditValue = ""
        Me.INDMeObservations.EnterMoveNextControl = True
        Me.INDMeObservations.Location = New System.Drawing.Point(12, 184)
        Me.INDMeObservations.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDMeObservations.Name = "INDMeObservations"
        Me.INDMeObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDMeObservations.Properties.Appearance.Options.UseFont = True
        Me.INDMeObservations.Properties.LinesCount = 2
        Me.INDMeObservations.Properties.MaxLength = 200
        Me.INDMeObservations.Size = New System.Drawing.Size(371, 125)
        Me.INDMeObservations.StyleController = Me.LcRoot
        Me.INDMeObservations.TabIndex = 8
        '
        'INDSbSaveFeeNotCollected
        '
        Me.INDSbSaveFeeNotCollected.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbSaveFeeNotCollected.Appearance.Options.UseFont = True
        Me.INDSbSaveFeeNotCollected.Location = New System.Drawing.Point(12, 313)
        Me.INDSbSaveFeeNotCollected.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbSaveFeeNotCollected, True)
        Me.INDSbSaveFeeNotCollected.Name = "INDSbSaveFeeNotCollected"
        Me.INDSbSaveFeeNotCollected.Size = New System.Drawing.Size(371, 41)
        Me.INDSbSaveFeeNotCollected.StyleController = Me.LcRoot
        Me.INDSbSaveFeeNotCollected.TabIndex = 6
        Me.INDSbSaveFeeNotCollected.Text = "Agregar"
        '
        'INDSleReportType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleReportType, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleReportType, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleReportType, False)
        Me.INDSleReportType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleReportType, False)
        Me.INDSleReportType.Location = New System.Drawing.Point(12, 44)
        Me.INDSleReportType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleReportType.Name = "INDSleReportType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleReportType, False)
        Me.INDSleReportType.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleReportType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleReportType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleReportType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleReportType.Properties.Appearance.Options.UseFont = True
        Me.INDSleReportType.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleReportType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleReportType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleReportType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleReportType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleReportType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleReportType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleReportType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleReportType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleReportType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleReportType.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleReportType.Properties.DisplayMember = "Item2"
        Me.INDSleReportType.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDSleReportType.Properties.NullText = ""
        Me.INDSleReportType.Properties.PopupSizeable = False
        Me.INDSleReportType.Properties.PopupView = Me.INDSleTypeReport
        Me.INDSleReportType.Properties.ShowClearButton = False
        Me.INDSleReportType.Properties.ShowFooter = False
        Me.INDSleReportType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleReportType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleReportType, True)
        Me.INDSleReportType.Size = New System.Drawing.Size(371, 28)
        Me.INDSleReportType.StyleController = Me.LcRoot
        Me.INDSleReportType.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleReportType, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleReportType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleReportType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleReportType, False)
        '
        'INDSleTypeReport
        '
        Me.INDSleTypeReport.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDSleTypeReport.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDSleTypeReport.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDSleTypeReport.Appearance.FocusedRow.Options.UseFont = True
        Me.INDSleTypeReport.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleTypeReport.Appearance.GroupRow.Options.UseFont = True
        Me.INDSleTypeReport.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleTypeReport.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDSleTypeReport.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDSleTypeReport.Appearance.Row.Options.UseFont = True
        Me.INDSleTypeReport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.INDSleTypeReport.DetailHeight = 458
        Me.INDSleTypeReport.FixedLineWidth = 3
        Me.INDSleTypeReport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDSleTypeReport.Name = "INDSleTypeReport"
        Me.INDSleTypeReport.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDSleTypeReport.OptionsView.EnableAppearanceEvenRow = True
        Me.INDSleTypeReport.OptionsView.EnableAppearanceOddRow = True
        Me.INDSleTypeReport.OptionsView.ShowAutoFilterRow = True
        Me.INDSleTypeReport.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.MinWidth = 23
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDTeValue
        '
        Me.INDTeValue.EditValue = "0"
        Me.INDTeValue.EnterMoveNextControl = True
        Me.INDTeValue.Location = New System.Drawing.Point(12, 117)
        Me.INDTeValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDTeValue.Name = "INDTeValue"
        Me.INDTeValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeValue.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTeValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeValue.Properties.Appearance.Options.UseFont = True
        Me.INDTeValue.Properties.Appearance.Options.UseForeColor = True
        Me.INDTeValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTeValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTeValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTeValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeValue.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTeValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTeValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeValue.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTeValue.Properties.DisplayFormat.FormatString = "d"
        Me.INDTeValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDTeValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDTeValue.Properties.Mask.EditMask = "c2"
        Me.INDTeValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTeValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTeValue.Size = New System.Drawing.Size(371, 28)
        Me.INDTeValue.StyleController = Me.LcRoot
        Me.INDTeValue.TabIndex = 4
        '
        'LcgRoot
        '
        Me.LcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgRoot.AppearanceGroup.Options.UseFont = True
        Me.LcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LcgRoot, False)
        Me.LcgRoot.CustomizationFormText = "LcgRoot"
        Me.LcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LcgRoot.GroupBordersVisible = False
        Me.LcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciValue, Me.INDLciGenFeeNotCollected, Me.INDLciReportType, Me.INDLciObservations})
        Me.LcgRoot.Name = "Root"
        Me.LcgRoot.Size = New System.Drawing.Size(395, 366)
        Me.LcgRoot.TextVisible = False
        '
        'INDLciValue
        '
        Me.INDLciValue.Control = Me.INDTeValue
        Me.INDLciValue.CustomizationFormText = "Valor"
        Me.INDLciValue.Location = New System.Drawing.Point(0, 73)
        Me.INDLciValue.MinSize = New System.Drawing.Size(40, 33)
        Me.INDLciValue.Name = "INDLciValue"
        Me.INDLciValue.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciValue.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.INDLciValue.OptionsPrint.AppearanceItemControl.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciValue.OptionsPrint.AppearanceItemControl.Options.UseFont = True
        Me.INDLciValue.OptionsPrint.AppearanceItemText.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciValue.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.INDLciValue.Size = New System.Drawing.Size(375, 72)
        Me.INDLciValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValue.Text = "Valor"
        Me.INDLciValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValue.TextSize = New System.Drawing.Size(112, 27)
        Me.INDLciValue.TextToControlDistance = 5
        Me.INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciGenFeeNotCollected
        '
        Me.INDLciGenFeeNotCollected.Control = Me.INDSbSaveFeeNotCollected
        Me.INDLciGenFeeNotCollected.CustomizationFormText = "Agregar"
        Me.INDLciGenFeeNotCollected.Location = New System.Drawing.Point(0, 301)
        Me.INDLciGenFeeNotCollected.MaxSize = New System.Drawing.Size(0, 45)
        Me.INDLciGenFeeNotCollected.MinSize = New System.Drawing.Size(65, 45)
        Me.INDLciGenFeeNotCollected.Name = "INDLciGenFeeNotCollected"
        Me.INDLciGenFeeNotCollected.Size = New System.Drawing.Size(375, 45)
        Me.INDLciGenFeeNotCollected.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenFeeNotCollected.Text = "Agregar"
        Me.INDLciGenFeeNotCollected.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciGenFeeNotCollected.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenFeeNotCollected.TextToControlDistance = 0
        Me.INDLciGenFeeNotCollected.TextVisible = False
        '
        'INDLciReportType
        '
        Me.INDLciReportType.Control = Me.INDSleReportType
        Me.INDLciReportType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciReportType.CustomizationFormText = "Tipo de reporte"
        Me.INDLciReportType.Location = New System.Drawing.Point(0, 0)
        Me.INDLciReportType.MinSize = New System.Drawing.Size(40, 33)
        Me.INDLciReportType.Name = "INDLciReportType"
        Me.INDLciReportType.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciReportType.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.INDLciReportType.OptionsPrint.AppearanceItemControl.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciReportType.OptionsPrint.AppearanceItemControl.Options.UseFont = True
        Me.INDLciReportType.OptionsPrint.AppearanceItemText.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciReportType.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.INDLciReportType.Size = New System.Drawing.Size(375, 73)
        Me.INDLciReportType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReportType.Text = "Tipo de reporte"
        Me.INDLciReportType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciReportType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReportType.TextSize = New System.Drawing.Size(112, 27)
        Me.INDLciReportType.TextToControlDistance = 5
        '
        'INDLciObservations
        '
        Me.INDLciObservations.Control = Me.INDMeObservations
        Me.INDLciObservations.Location = New System.Drawing.Point(0, 145)
        Me.INDLciObservations.MinSize = New System.Drawing.Size(40, 42)
        Me.INDLciObservations.Name = "INDLciObservations"
        Me.INDLciObservations.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciObservations.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.INDLciObservations.OptionsPrint.AppearanceItemControl.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciObservations.OptionsPrint.AppearanceItemControl.Options.UseFont = True
        Me.INDLciObservations.OptionsPrint.AppearanceItemText.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDLciObservations.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.INDLciObservations.Size = New System.Drawing.Size(375, 156)
        Me.INDLciObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservations.Text = "Observaciones"
        Me.INDLciObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservations.TextSize = New System.Drawing.Size(108, 22)
        Me.INDLciObservations.TextToControlDistance = 5
        '
        'FrmPopUpFeeNotCollected
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 366)
        Me.Controls.Add(Me.LcRoot)
        Me.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.Image = Global.Presentation.Billing.My.Resources.Resources.login
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopUpFeeNotCollected"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Información"
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LcRoot.ResumeLayout(False)
        CType(Me.INDMeObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleReportType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleTypeReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenFeeNotCollected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDSbSaveFeeNotCollected As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGenFeeNotCollected As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDLciReportType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleReportType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDSleTypeReport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDMeObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents INDTeValue As DevExpress.XtraEditors.TextEdit
End Class
