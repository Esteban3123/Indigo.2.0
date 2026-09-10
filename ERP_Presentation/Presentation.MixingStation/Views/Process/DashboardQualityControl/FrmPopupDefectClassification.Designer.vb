Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupDefectClassification
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
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMeObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTeUnitDoseType = New DevExpress.XtraEditors.TextEdit()
        Me.INDGcDefectClassification = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDefectClassification = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDefectGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCritical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLess = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDColProduction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuality = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSeTheoreticalWeight = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeInputsWeight = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeMinimumWeight = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeSumInputsTheoreticalWeight = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeMaximumWeight = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeActualWeight = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSeQuantityRequest = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeCriticalDefect = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeReleaseQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDSePerformance = New DevExpress.XtraEditors.TextEdit()
        Me.INDSeIndicationSize = New DevExpress.XtraEditors.SpinEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciUnitDoseType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgParenteralNutrition = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTheoreticalWeight = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciInputsWeight = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMinimumWeight = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSumInputsTheoreticalWeight = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMaximumWeight = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciActualWeight = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTrueValidation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValidationResult = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFalseValidation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgQualityControl = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciQuantityRequest = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCriticalDefect = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcIIndicationSize = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReleaseQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPerformance = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDMeObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeUnitDoseType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDefectClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDefectClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeTheoreticalWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeInputsWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeMinimumWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeSumInputsTheoreticalWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeMaximumWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeActualWeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeQuantityRequest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeCriticalDefect.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeReleaseQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSePerformance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeIndicationSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgParenteralNutrition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTheoreticalWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInputsWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMinimumWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSumInputsTheoreticalWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMaximumWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciActualWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTrueValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValidationResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFalseValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgQualityControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuantityRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCriticalDefect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcIIndicationSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReleaseQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPerformance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.AutoSize = True
        Me.INDPanelControlBase.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1150, 711)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1150, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1150, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDLcRoot
        '
        Me.INDLcRoot.AutoSize = True
        Me.INDLcRoot.Controls.Add(Me.INDMeObservation)
        Me.INDLcRoot.Controls.Add(Me.INDTeUnitDoseType)
        Me.INDLcRoot.Controls.Add(Me.INDGcDefectClassification)
        Me.INDLcRoot.Controls.Add(Me.INDSeTheoreticalWeight)
        Me.INDLcRoot.Controls.Add(Me.INDSeInputsWeight)
        Me.INDLcRoot.Controls.Add(Me.INDSeMinimumWeight)
        Me.INDLcRoot.Controls.Add(Me.INDSeSumInputsTheoreticalWeight)
        Me.INDLcRoot.Controls.Add(Me.INDSeMaximumWeight)
        Me.INDLcRoot.Controls.Add(Me.INDSeActualWeight)
        Me.INDLcRoot.Controls.Add(Me.INDSeQuantityRequest)
        Me.INDLcRoot.Controls.Add(Me.INDSeCriticalDefect)
        Me.INDLcRoot.Controls.Add(Me.INDSeReleaseQuantity)
        Me.INDLcRoot.Controls.Add(Me.INDSePerformance)
        Me.INDLcRoot.Controls.Add(Me.INDSeIndicationSize)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(2, 9)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(835, 132, 574, 569)
        Me.INDLcRoot.Root = Me.Root
        Me.INDLcRoot.Size = New System.Drawing.Size(1151, 746)
        Me.INDLcRoot.TabIndex = 0
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDMeObservation
        '
        Me.INDMeObservation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeObservation, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeObservation, False)
        Me.INDMeObservation.Location = New System.Drawing.Point(137, 604)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeObservation.MaximumSize = New System.Drawing.Size(1600, 160)
        Me.INDMeObservation.MinimumSize = New System.Drawing.Size(987, 130)
        Me.INDMeObservation.Name = "INDMeObservation"
        Me.INDMeObservation.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservation.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDMeObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeObservation.Properties.Appearance.Options.UseFont = True
        Me.INDMeObservation.Properties.Appearance.Options.UseForeColor = True
        Me.INDMeObservation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservation.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDMeObservation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeObservation.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDMeObservation.Properties.MaxLength = 500
        Me.INDMeObservation.Size = New System.Drawing.Size(1002, 130)
        Me.INDMeObservation.StyleController = Me.INDLcRoot
        Me.INDMeObservation.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeObservation, 0)
        '
        'INDTeUnitDoseType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeUnitDoseType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeUnitDoseType, False)
        Me.INDTeUnitDoseType.Location = New System.Drawing.Point(199, 68)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeUnitDoseType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeUnitDoseType.Name = "INDTeUnitDoseType"
        Me.INDTeUnitDoseType.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTeUnitDoseType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeUnitDoseType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTeUnitDoseType.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeUnitDoseType.Properties.Appearance.Options.UseFont = True
        Me.INDTeUnitDoseType.Properties.Appearance.Options.UseForeColor = True
        Me.INDTeUnitDoseType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTeUnitDoseType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeUnitDoseType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTeUnitDoseType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeUnitDoseType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeUnitDoseType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTeUnitDoseType.Properties.ReadOnly = True
        Me.INDTeUnitDoseType.Size = New System.Drawing.Size(921, 38)
        Me.INDTeUnitDoseType.StyleController = Me.INDLcRoot
        Me.INDTeUnitDoseType.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeUnitDoseType, 0)
        '
        'INDGcDefectClassification
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDefectClassification, Nothing)
        Me.INDGcDefectClassification.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDefectClassification, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDefectClassification, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDefectClassification, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDefectClassification, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDefectClassification, False)
        Me.INDGcDefectClassification.Location = New System.Drawing.Point(12, 374)
        Me.INDGcDefectClassification.MainView = Me.INDGvDefectClassification
        Me.INDGcDefectClassification.MaximumSize = New System.Drawing.Size(1600, 300)
        Me.INDGcDefectClassification.MinimumSize = New System.Drawing.Size(1127, 226)
        Me.INDGcDefectClassification.Name = "INDGcDefectClassification"
        Me.INDGcDefectClassification.Padding = New System.Windows.Forms.Padding(1)
        Me.INDGcDefectClassification.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSpinEdit1})
        Me.INDGcDefectClassification.Size = New System.Drawing.Size(1127, 226)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDefectClassification, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcDefectClassification.TabIndex = 4
        Me.INDGcDefectClassification.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDefectClassification})
        '
        'INDGvDefectClassification
        '
        Me.INDGvDefectClassification.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDefectClassification.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDefectClassification.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDefectClassification.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDefectClassification.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDefectClassification.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDefectClassification.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDefectClassification.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDefectClassification.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDefectClassification.Appearance.Row.Options.UseFont = True
        Me.INDGvDefectClassification.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvDefectClassification.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDefectClassification.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDefectGroup, Me.INDColItem, Me.INDColCritical, Me.INDColLess, Me.INDColTypeName, Me.INDColQuantity, Me.INDColProduction, Me.INDColQuality})
        Me.INDGvDefectClassification.GridControl = Me.INDGcDefectClassification
        Me.INDGvDefectClassification.GroupCount = 1
        Me.INDGvDefectClassification.IndicatorWidth = 30
        Me.INDGvDefectClassification.Name = "INDGvDefectClassification"
        Me.INDGvDefectClassification.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvDefectClassification.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDefectClassification.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDefectClassification.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDefectClassification.OptionsView.ShowGroupPanel = False
        Me.INDGvDefectClassification.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColDefectGroup, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDefectClassification, False)
        '
        'INDColDefectGroup
        '
        Me.INDColDefectGroup.Caption = "Grupo"
        Me.INDColDefectGroup.FieldName = "DefectClassificationGroupDescription"
        Me.INDColDefectGroup.FieldNameSortGroup = "DefectClassificationGroupWeight"
        Me.INDColDefectGroup.Name = "INDColDefectGroup"
        Me.INDColDefectGroup.OptionsColumn.AllowEdit = False
        Me.INDColDefectGroup.OptionsColumn.AllowFocus = False
        Me.INDColDefectGroup.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColDefectGroup.OptionsColumn.AllowMove = False
        Me.INDColDefectGroup.OptionsColumn.AllowShowHide = False
        Me.INDColDefectGroup.OptionsColumn.AllowSize = False
        Me.INDColDefectGroup.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColDefectGroup.Visible = True
        Me.INDColDefectGroup.VisibleIndex = 0
        '
        'INDColItem
        '
        Me.INDColItem.Caption = "DEFECTO"
        Me.INDColItem.FieldName = "DefectClassificationItemDescription"
        Me.INDColItem.Name = "INDColItem"
        Me.INDColItem.OptionsColumn.AllowEdit = False
        Me.INDColItem.OptionsColumn.AllowFocus = False
        Me.INDColItem.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.OptionsColumn.AllowMove = False
        Me.INDColItem.OptionsColumn.AllowShowHide = False
        Me.INDColItem.OptionsColumn.AllowSize = False
        Me.INDColItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColItem.Visible = True
        Me.INDColItem.VisibleIndex = 0
        Me.INDColItem.Width = 656
        '
        'INDColCritical
        '
        Me.INDColCritical.Caption = "CRÍTICO"
        Me.INDColCritical.FieldName = "Critical"
        Me.INDColCritical.Name = "INDColCritical"
        Me.INDColCritical.OptionsColumn.AllowEdit = False
        Me.INDColCritical.OptionsColumn.AllowFocus = False
        Me.INDColCritical.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColCritical.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColCritical.OptionsColumn.AllowMove = False
        Me.INDColCritical.OptionsColumn.AllowShowHide = False
        Me.INDColCritical.OptionsColumn.AllowSize = False
        Me.INDColCritical.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColCritical.OptionsColumn.ShowInCustomizationForm = False
        Me.INDColCritical.Width = 56
        '
        'INDColLess
        '
        Me.INDColLess.Caption = "MENOR"
        Me.INDColLess.FieldName = "Less"
        Me.INDColLess.Name = "INDColLess"
        Me.INDColLess.OptionsColumn.AllowEdit = False
        Me.INDColLess.OptionsColumn.AllowFocus = False
        Me.INDColLess.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColLess.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColLess.OptionsColumn.AllowMove = False
        Me.INDColLess.OptionsColumn.AllowShowHide = False
        Me.INDColLess.OptionsColumn.AllowSize = False
        Me.INDColLess.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColLess.OptionsColumn.ShowInCustomizationForm = False
        Me.INDColLess.Width = 56
        '
        'INDColTypeName
        '
        Me.INDColTypeName.AppearanceHeader.Options.UseTextOptions = True
        Me.INDColTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColTypeName.Caption = "TIPO"
        Me.INDColTypeName.FieldName = "TypeName"
        Me.INDColTypeName.Name = "INDColTypeName"
        Me.INDColTypeName.OptionsColumn.AllowEdit = False
        Me.INDColTypeName.OptionsColumn.AllowFocus = False
        Me.INDColTypeName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColTypeName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColTypeName.OptionsColumn.AllowMove = False
        Me.INDColTypeName.OptionsColumn.AllowShowHide = False
        Me.INDColTypeName.OptionsColumn.AllowSize = False
        Me.INDColTypeName.Visible = True
        Me.INDColTypeName.VisibleIndex = 1
        Me.INDColTypeName.Width = 55
        '
        'INDColQuantity
        '
        Me.INDColQuantity.Caption = "CANTIDAD"
        Me.INDColQuantity.ColumnEdit = Me.RepositoryItemSpinEdit1
        Me.INDColQuantity.DisplayFormat.FormatString = "n2"
        Me.INDColQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantity.FieldName = "Quantity"
        Me.INDColQuantity.Name = "INDColQuantity"
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSpinEdit1.IsFloatValue = False
        Me.RepositoryItemSpinEdit1.Mask.EditMask = "n0"
        Me.RepositoryItemSpinEdit1.Mask.UseMaskAsDisplayFormat = True
        Me.RepositoryItemSpinEdit1.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'INDColProduction
        '
        Me.INDColProduction.AppearanceHeader.Options.UseTextOptions = True
        Me.INDColProduction.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColProduction.Caption = "HALLAZGO"
        Me.INDColProduction.FieldName = "Production"
        Me.INDColProduction.Name = "INDColProduction"
        Me.INDColProduction.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColProduction.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColProduction.OptionsColumn.AllowMove = False
        Me.INDColProduction.OptionsColumn.AllowShowHide = False
        Me.INDColProduction.OptionsColumn.AllowSize = False
        Me.INDColProduction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColProduction.Visible = True
        Me.INDColProduction.VisibleIndex = 2
        Me.INDColProduction.Width = 157
        '
        'INDColQuality
        '
        Me.INDColQuality.AppearanceHeader.Options.UseTextOptions = True
        Me.INDColQuality.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColQuality.Caption = "HALLAZGO"
        Me.INDColQuality.FieldName = "Quality"
        Me.INDColQuality.Name = "INDColQuality"
        Me.INDColQuality.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColQuality.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColQuality.OptionsColumn.AllowMove = False
        Me.INDColQuality.OptionsColumn.AllowShowHide = False
        Me.INDColQuality.OptionsColumn.AllowSize = False
        Me.INDColQuality.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColQuality.Visible = True
        Me.INDColQuality.VisibleIndex = 3
        Me.INDColQuality.Width = 160
        '
        'INDSeTheoreticalWeight
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeTheoreticalWeight, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeTheoreticalWeight, False)
        Me.INDSeTheoreticalWeight.Location = New System.Drawing.Point(274, 173)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeTheoreticalWeight, Presentation.Controls.IndigoTextEdit.EMask.NumericoDosDecimales)
        Me.INDSeTheoreticalWeight.Name = "INDSeTheoreticalWeight"
        Me.INDSeTheoreticalWeight.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeTheoreticalWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeTheoreticalWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeTheoreticalWeight.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeTheoreticalWeight.Properties.Appearance.Options.UseFont = True
        Me.INDSeTheoreticalWeight.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeTheoreticalWeight.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeTheoreticalWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeTheoreticalWeight.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeTheoreticalWeight.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeTheoreticalWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeTheoreticalWeight.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeTheoreticalWeight.Properties.Mask.EditMask = "f"
        Me.INDSeTheoreticalWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSeTheoreticalWeight.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeTheoreticalWeight.Properties.ReadOnly = True
        Me.INDSeTheoreticalWeight.Size = New System.Drawing.Size(71, 38)
        Me.INDSeTheoreticalWeight.StyleController = Me.INDLcRoot
        Me.INDSeTheoreticalWeight.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeTheoreticalWeight, 0)
        '
        'INDSeInputsWeight
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeInputsWeight, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeInputsWeight, False)
        Me.INDSeInputsWeight.Location = New System.Drawing.Point(274, 202)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeInputsWeight, Presentation.Controls.IndigoTextEdit.EMask.NumericoDosDecimales)
        Me.INDSeInputsWeight.Name = "INDSeInputsWeight"
        Me.INDSeInputsWeight.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeInputsWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeInputsWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeInputsWeight.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeInputsWeight.Properties.Appearance.Options.UseFont = True
        Me.INDSeInputsWeight.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeInputsWeight.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeInputsWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeInputsWeight.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeInputsWeight.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeInputsWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeInputsWeight.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeInputsWeight.Properties.Mask.EditMask = "f"
        Me.INDSeInputsWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSeInputsWeight.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeInputsWeight.Properties.ReadOnly = True
        Me.INDSeInputsWeight.Size = New System.Drawing.Size(71, 38)
        Me.INDSeInputsWeight.StyleController = Me.INDLcRoot
        Me.INDSeInputsWeight.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeInputsWeight, 0)
        '
        'INDSeMinimumWeight
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeMinimumWeight, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeMinimumWeight, False)
        Me.INDSeMinimumWeight.Location = New System.Drawing.Point(504, 173)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeMinimumWeight, Presentation.Controls.IndigoTextEdit.EMask.NumericoDosDecimales)
        Me.INDSeMinimumWeight.Name = "INDSeMinimumWeight"
        Me.INDSeMinimumWeight.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeMinimumWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeMinimumWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeMinimumWeight.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeMinimumWeight.Properties.Appearance.Options.UseFont = True
        Me.INDSeMinimumWeight.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeMinimumWeight.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeMinimumWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeMinimumWeight.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeMinimumWeight.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeMinimumWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeMinimumWeight.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeMinimumWeight.Properties.Mask.EditMask = "f"
        Me.INDSeMinimumWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSeMinimumWeight.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeMinimumWeight.Properties.ReadOnly = True
        Me.INDSeMinimumWeight.Size = New System.Drawing.Size(71, 38)
        Me.INDSeMinimumWeight.StyleController = Me.INDLcRoot
        Me.INDSeMinimumWeight.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeMinimumWeight, 0)
        '
        'INDSeSumInputsTheoreticalWeight
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeSumInputsTheoreticalWeight, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeSumInputsTheoreticalWeight, False)
        Me.INDSeSumInputsTheoreticalWeight.Location = New System.Drawing.Point(274, 231)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeSumInputsTheoreticalWeight, Presentation.Controls.IndigoTextEdit.EMask.NumericoDosDecimales)
        Me.INDSeSumInputsTheoreticalWeight.Name = "INDSeSumInputsTheoreticalWeight"
        Me.INDSeSumInputsTheoreticalWeight.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeSumInputsTheoreticalWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeSumInputsTheoreticalWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeSumInputsTheoreticalWeight.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeSumInputsTheoreticalWeight.Properties.Appearance.Options.UseFont = True
        Me.INDSeSumInputsTheoreticalWeight.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeSumInputsTheoreticalWeight.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeSumInputsTheoreticalWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeSumInputsTheoreticalWeight.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeSumInputsTheoreticalWeight.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeSumInputsTheoreticalWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeSumInputsTheoreticalWeight.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeSumInputsTheoreticalWeight.Properties.Mask.EditMask = "f"
        Me.INDSeSumInputsTheoreticalWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSeSumInputsTheoreticalWeight.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeSumInputsTheoreticalWeight.Properties.ReadOnly = True
        Me.INDSeSumInputsTheoreticalWeight.Size = New System.Drawing.Size(71, 38)
        Me.INDSeSumInputsTheoreticalWeight.StyleController = Me.INDLcRoot
        Me.INDSeSumInputsTheoreticalWeight.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeSumInputsTheoreticalWeight, 0)
        '
        'INDSeMaximumWeight
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeMaximumWeight, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeMaximumWeight, False)
        Me.INDSeMaximumWeight.Location = New System.Drawing.Point(504, 202)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeMaximumWeight, Presentation.Controls.IndigoTextEdit.EMask.NumericoDosDecimales)
        Me.INDSeMaximumWeight.Name = "INDSeMaximumWeight"
        Me.INDSeMaximumWeight.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeMaximumWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeMaximumWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeMaximumWeight.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeMaximumWeight.Properties.Appearance.Options.UseFont = True
        Me.INDSeMaximumWeight.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeMaximumWeight.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeMaximumWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeMaximumWeight.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeMaximumWeight.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeMaximumWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeMaximumWeight.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeMaximumWeight.Properties.Mask.EditMask = "f"
        Me.INDSeMaximumWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSeMaximumWeight.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeMaximumWeight.Properties.ReadOnly = True
        Me.INDSeMaximumWeight.Size = New System.Drawing.Size(71, 38)
        Me.INDSeMaximumWeight.StyleController = Me.INDLcRoot
        Me.INDSeMaximumWeight.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeMaximumWeight, 0)
        '
        'INDSeActualWeight
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeActualWeight, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeActualWeight, False)
        Me.INDSeActualWeight.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeActualWeight.Location = New System.Drawing.Point(504, 231)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeActualWeight, Presentation.Controls.IndigoTextEdit.EMask.NumericoDosDecimales)
        Me.INDSeActualWeight.Name = "INDSeActualWeight"
        Me.INDSeActualWeight.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeActualWeight.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeActualWeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeActualWeight.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeActualWeight.Properties.Appearance.Options.UseFont = True
        Me.INDSeActualWeight.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeActualWeight.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeActualWeight.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeActualWeight.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSeActualWeight.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeActualWeight.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeActualWeight.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeActualWeight.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeActualWeight.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeActualWeight.Properties.Mask.EditMask = "f"
        Me.INDSeActualWeight.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeActualWeight.Properties.MaxValue = New Decimal(New Integer() {1410065407, 2, 0, 0})
        Me.INDSeActualWeight.Size = New System.Drawing.Size(71, 38)
        Me.INDSeActualWeight.StyleController = Me.INDLcRoot
        Me.INDSeActualWeight.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeActualWeight, 0)
        '
        'INDSeQuantityRequest
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeQuantityRequest, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeQuantityRequest, False)
        Me.INDSeQuantityRequest.Location = New System.Drawing.Point(242, 333)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeQuantityRequest, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeQuantityRequest.MaximumSize = New System.Drawing.Size(67, 26)
        Me.INDSeQuantityRequest.MinimumSize = New System.Drawing.Size(67, 26)
        Me.INDSeQuantityRequest.Name = "INDSeQuantityRequest"
        Me.INDSeQuantityRequest.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeQuantityRequest.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeQuantityRequest.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeQuantityRequest.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeQuantityRequest.Properties.Appearance.Options.UseFont = True
        Me.INDSeQuantityRequest.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeQuantityRequest.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeQuantityRequest.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeQuantityRequest.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeQuantityRequest.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeQuantityRequest.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeQuantityRequest.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeQuantityRequest.Properties.Mask.EditMask = "n0"
        Me.INDSeQuantityRequest.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSeQuantityRequest.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeQuantityRequest.Properties.ReadOnly = True
        Me.INDSeQuantityRequest.Size = New System.Drawing.Size(67, 26)
        Me.INDSeQuantityRequest.StyleController = Me.INDLcRoot
        Me.INDSeQuantityRequest.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeQuantityRequest, 0)
        '
        'INDSeCriticalDefect
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeCriticalDefect, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeCriticalDefect, False)
        Me.INDSeCriticalDefect.Location = New System.Drawing.Point(416, 333)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeCriticalDefect, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeCriticalDefect.MaximumSize = New System.Drawing.Size(67, 26)
        Me.INDSeCriticalDefect.MinimumSize = New System.Drawing.Size(67, 26)
        Me.INDSeCriticalDefect.Name = "INDSeCriticalDefect"
        Me.INDSeCriticalDefect.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeCriticalDefect.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeCriticalDefect.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeCriticalDefect.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeCriticalDefect.Properties.Appearance.Options.UseFont = True
        Me.INDSeCriticalDefect.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeCriticalDefect.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeCriticalDefect.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeCriticalDefect.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeCriticalDefect.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeCriticalDefect.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeCriticalDefect.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeCriticalDefect.Properties.Mask.EditMask = "n0"
        Me.INDSeCriticalDefect.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSeCriticalDefect.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeCriticalDefect.Properties.ReadOnly = True
        Me.INDSeCriticalDefect.Size = New System.Drawing.Size(67, 26)
        Me.INDSeCriticalDefect.StyleController = Me.INDLcRoot
        Me.INDSeCriticalDefect.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeCriticalDefect, 0)
        '
        'INDSeReleaseQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeReleaseQuantity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeReleaseQuantity, False)
        Me.INDSeReleaseQuantity.Location = New System.Drawing.Point(591, 333)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeReleaseQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeReleaseQuantity.MaximumSize = New System.Drawing.Size(67, 26)
        Me.INDSeReleaseQuantity.MinimumSize = New System.Drawing.Size(67, 26)
        Me.INDSeReleaseQuantity.Name = "INDSeReleaseQuantity"
        Me.INDSeReleaseQuantity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeReleaseQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeReleaseQuantity.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeReleaseQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeReleaseQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDSeReleaseQuantity.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeReleaseQuantity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeReleaseQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeReleaseQuantity.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeReleaseQuantity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeReleaseQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeReleaseQuantity.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeReleaseQuantity.Properties.Mask.EditMask = "n0"
        Me.INDSeReleaseQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSeReleaseQuantity.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeReleaseQuantity.Properties.ReadOnly = True
        Me.INDSeReleaseQuantity.Size = New System.Drawing.Size(67, 26)
        Me.INDSeReleaseQuantity.StyleController = Me.INDLcRoot
        Me.INDSeReleaseQuantity.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeReleaseQuantity, 0)
        '
        'INDSePerformance
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSePerformance, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSePerformance, False)
        Me.INDSePerformance.Location = New System.Drawing.Point(766, 333)
        Me.IndigoTextEdit1.SetMascara(Me.INDSePerformance, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSePerformance.MaximumSize = New System.Drawing.Size(67, 26)
        Me.INDSePerformance.MinimumSize = New System.Drawing.Size(67, 26)
        Me.INDSePerformance.Name = "INDSePerformance"
        Me.INDSePerformance.Properties.AllowFocused = False
        Me.INDSePerformance.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSePerformance.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSePerformance.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSePerformance.Properties.Appearance.Options.UseBackColor = True
        Me.INDSePerformance.Properties.Appearance.Options.UseFont = True
        Me.INDSePerformance.Properties.Appearance.Options.UseForeColor = True
        Me.INDSePerformance.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSePerformance.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSePerformance.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSePerformance.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSePerformance.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSePerformance.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSePerformance.Properties.Mask.EditMask = "n2"
        Me.INDSePerformance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDSePerformance.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSePerformance.Properties.ReadOnly = True
        Me.INDSePerformance.Size = New System.Drawing.Size(67, 26)
        Me.INDSePerformance.StyleController = Me.INDLcRoot
        Me.INDSePerformance.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSePerformance, 0)
        '
        'INDSeIndicationSize
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeIndicationSize, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeIndicationSize, False)
        Me.INDSeIndicationSize.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeIndicationSize.Location = New System.Drawing.Point(988, 333)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeIndicationSize, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeIndicationSize.MaximumSize = New System.Drawing.Size(80, 26)
        Me.INDSeIndicationSize.MinimumSize = New System.Drawing.Size(80, 26)
        Me.INDSeIndicationSize.Name = "INDSeIndicationSize"
        Me.INDSeIndicationSize.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeIndicationSize.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeIndicationSize.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeIndicationSize.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeIndicationSize.Properties.Appearance.Options.UseFont = True
        Me.INDSeIndicationSize.Properties.Appearance.Options.UseForeColor = True
        Me.INDSeIndicationSize.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDSeIndicationSize.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeIndicationSize.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDSeIndicationSize.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeIndicationSize.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeIndicationSize.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSeIndicationSize.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeIndicationSize.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDSeIndicationSize.Properties.IsFloatValue = False
        Me.INDSeIndicationSize.Properties.Mask.EditMask = "n0"
        Me.INDSeIndicationSize.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeIndicationSize.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDSeIndicationSize.Size = New System.Drawing.Size(80, 26)
        Me.INDSeIndicationSize.StyleController = Me.INDLcRoot
        Me.INDSeIndicationSize.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeIndicationSize, 0)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLcgMain, Me.INDLcgParenteralNutrition, Me.INDLciObservation, Me.INDLcgQualityControl})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1151, 746)
        Me.Root.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcDefectClassification
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 362)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1131, 230)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDLcgMain
        '
        Me.INDLcgMain.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMain.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMain, False)
        Me.INDLcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciUnitDoseType})
        Me.INDLcgMain.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMain.Name = "INDLcgMain"
        Me.INDLcgMain.Size = New System.Drawing.Size(1131, 100)
        Me.INDLcgMain.Text = "Información Principal"
        '
        'INDLciUnitDoseType
        '
        Me.INDLciUnitDoseType.Control = Me.INDTeUnitDoseType
        Me.INDLciUnitDoseType.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciUnitDoseType.Location = New System.Drawing.Point(0, 0)
        Me.INDLciUnitDoseType.MaxSize = New System.Drawing.Size(1100, 32)
        Me.INDLciUnitDoseType.MinSize = New System.Drawing.Size(1100, 32)
        Me.INDLciUnitDoseType.Name = "INDLciUnitDoseType"
        Me.INDLciUnitDoseType.Size = New System.Drawing.Size(1107, 32)
        Me.INDLciUnitDoseType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciUnitDoseType.Text = "Tipo de dosis unitaria"
        Me.INDLciUnitDoseType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciUnitDoseType.TextSize = New System.Drawing.Size(170, 13)
        Me.INDLciUnitDoseType.TextToControlDistance = 5
        '
        'INDLcgParenteralNutrition
        '
        Me.INDLcgParenteralNutrition.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgParenteralNutrition.AppearanceGroup.Options.UseFont = True
        Me.INDLcgParenteralNutrition.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgParenteralNutrition.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgParenteralNutrition.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgParenteralNutrition.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgParenteralNutrition.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgParenteralNutrition.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgParenteralNutrition.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgParenteralNutrition.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgParenteralNutrition.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgParenteralNutrition.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgParenteralNutrition.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgParenteralNutrition.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgParenteralNutrition, False)
        Me.INDLcgParenteralNutrition.CustomizationFormText = "Información Principal"
        Me.INDLcgParenteralNutrition.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTheoreticalWeight, Me.INDLciInputsWeight, Me.INDLciMinimumWeight, Me.INDLciSumInputsTheoreticalWeight, Me.INDLciMaximumWeight, Me.INDLciActualWeight, Me.INDLciTrueValidation, Me.INDLciValidationResult, Me.INDLciFalseValidation})
        Me.INDLcgParenteralNutrition.Location = New System.Drawing.Point(0, 100)
        Me.INDLcgParenteralNutrition.Name = "INDLcgParenteralNutrition"
        Me.INDLcgParenteralNutrition.Padding = New DevExpress.XtraLayout.Utils.Padding(13, 13, 13, 13)
        Me.INDLcgParenteralNutrition.Size = New System.Drawing.Size(1131, 165)
        Me.INDLcgParenteralNutrition.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3)
        Me.INDLcgParenteralNutrition.Text = "Validación del peso de la nutrición parenteral"
        Me.INDLcgParenteralNutrition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciTheoreticalWeight
        '
        Me.INDLciTheoreticalWeight.Control = Me.INDSeTheoreticalWeight
        Me.INDLciTheoreticalWeight.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTheoreticalWeight.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciTheoreticalWeight.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTheoreticalWeight.MaxSize = New System.Drawing.Size(320, 29)
        Me.INDLciTheoreticalWeight.MinSize = New System.Drawing.Size(320, 29)
        Me.INDLciTheoreticalWeight.Name = "INDLciTheoreticalWeight"
        Me.INDLciTheoreticalWeight.Size = New System.Drawing.Size(320, 29)
        Me.INDLciTheoreticalWeight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTheoreticalWeight.Text = "Peso teórico (g)"
        Me.INDLciTheoreticalWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTheoreticalWeight.TextSize = New System.Drawing.Size(240, 13)
        Me.INDLciTheoreticalWeight.TextToControlDistance = 5
        '
        'INDLciInputsWeight
        '
        Me.INDLciInputsWeight.Control = Me.INDSeInputsWeight
        Me.INDLciInputsWeight.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciInputsWeight.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciInputsWeight.Location = New System.Drawing.Point(0, 29)
        Me.INDLciInputsWeight.MaxSize = New System.Drawing.Size(320, 29)
        Me.INDLciInputsWeight.MinSize = New System.Drawing.Size(320, 29)
        Me.INDLciInputsWeight.Name = "INDLciInputsWeight"
        Me.INDLciInputsWeight.Size = New System.Drawing.Size(320, 29)
        Me.INDLciInputsWeight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInputsWeight.Text = "Peso insumos (g)"
        Me.INDLciInputsWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciInputsWeight.TextSize = New System.Drawing.Size(240, 13)
        Me.INDLciInputsWeight.TextToControlDistance = 5
        '
        'INDLciMinimumWeight
        '
        Me.INDLciMinimumWeight.Control = Me.INDSeMinimumWeight
        Me.INDLciMinimumWeight.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciMinimumWeight.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciMinimumWeight.Location = New System.Drawing.Point(320, 0)
        Me.INDLciMinimumWeight.MaxSize = New System.Drawing.Size(230, 29)
        Me.INDLciMinimumWeight.MinSize = New System.Drawing.Size(230, 29)
        Me.INDLciMinimumWeight.Name = "INDLciMinimumWeight"
        Me.INDLciMinimumWeight.Size = New System.Drawing.Size(230, 29)
        Me.INDLciMinimumWeight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMinimumWeight.Text = "Peso mínimo -5% (g)"
        Me.INDLciMinimumWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciMinimumWeight.TextSize = New System.Drawing.Size(150, 13)
        Me.INDLciMinimumWeight.TextToControlDistance = 5
        '
        'INDLciSumInputsTheoreticalWeight
        '
        Me.INDLciSumInputsTheoreticalWeight.Control = Me.INDSeSumInputsTheoreticalWeight
        Me.INDLciSumInputsTheoreticalWeight.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciSumInputsTheoreticalWeight.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciSumInputsTheoreticalWeight.Location = New System.Drawing.Point(0, 58)
        Me.INDLciSumInputsTheoreticalWeight.MaxSize = New System.Drawing.Size(320, 29)
        Me.INDLciSumInputsTheoreticalWeight.MinSize = New System.Drawing.Size(320, 29)
        Me.INDLciSumInputsTheoreticalWeight.Name = "INDLciSumInputsTheoreticalWeight"
        Me.INDLciSumInputsTheoreticalWeight.Size = New System.Drawing.Size(320, 29)
        Me.INDLciSumInputsTheoreticalWeight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSumInputsTheoreticalWeight.Text = "Peso teórico (g) + Peso insumos (g)"
        Me.INDLciSumInputsTheoreticalWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSumInputsTheoreticalWeight.TextSize = New System.Drawing.Size(240, 13)
        Me.INDLciSumInputsTheoreticalWeight.TextToControlDistance = 5
        '
        'INDLciMaximumWeight
        '
        Me.INDLciMaximumWeight.Control = Me.INDSeMaximumWeight
        Me.INDLciMaximumWeight.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciMaximumWeight.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciMaximumWeight.Location = New System.Drawing.Point(320, 29)
        Me.INDLciMaximumWeight.MaxSize = New System.Drawing.Size(230, 29)
        Me.INDLciMaximumWeight.MinSize = New System.Drawing.Size(230, 29)
        Me.INDLciMaximumWeight.Name = "INDLciMaximumWeight"
        Me.INDLciMaximumWeight.Size = New System.Drawing.Size(230, 29)
        Me.INDLciMaximumWeight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMaximumWeight.Text = "Peso máximo +5% (g)"
        Me.INDLciMaximumWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciMaximumWeight.TextSize = New System.Drawing.Size(150, 13)
        Me.INDLciMaximumWeight.TextToControlDistance = 5
        '
        'INDLciActualWeight
        '
        Me.INDLciActualWeight.Control = Me.INDSeActualWeight
        Me.INDLciActualWeight.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciActualWeight.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciActualWeight.Location = New System.Drawing.Point(320, 58)
        Me.INDLciActualWeight.MaxSize = New System.Drawing.Size(230, 29)
        Me.INDLciActualWeight.MinSize = New System.Drawing.Size(230, 29)
        Me.INDLciActualWeight.Name = "INDLciActualWeight"
        Me.INDLciActualWeight.Size = New System.Drawing.Size(230, 29)
        Me.INDLciActualWeight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciActualWeight.Text = "Peso real"
        Me.INDLciActualWeight.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciActualWeight.TextSize = New System.Drawing.Size(150, 13)
        Me.INDLciActualWeight.TextToControlDistance = 5
        '
        'INDLciTrueValidation
        '
        Me.INDLciTrueValidation.AllowHide = False
        Me.INDLciTrueValidation.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciTrueValidation.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.aceptar16x16
        Me.INDLciTrueValidation.Location = New System.Drawing.Point(550, 29)
        Me.INDLciTrueValidation.MaxSize = New System.Drawing.Size(480, 29)
        Me.INDLciTrueValidation.MinSize = New System.Drawing.Size(480, 29)
        Me.INDLciTrueValidation.Name = "INDLciTrueValidation"
        Me.INDLciTrueValidation.ShowInCustomizationForm = False
        Me.INDLciTrueValidation.Size = New System.Drawing.Size(547, 29)
        Me.INDLciTrueValidation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTrueValidation.Text = "El peso de la nutrición SI se encuentra dentro del rango permitido."
        Me.INDLciTrueValidation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTrueValidation.TextSize = New System.Drawing.Size(480, 17)
        Me.INDLciTrueValidation.TextToControlDistance = 2
        Me.INDLciTrueValidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciValidationResult
        '
        Me.INDLciValidationResult.AllowHide = False
        Me.INDLciValidationResult.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciValidationResult.Location = New System.Drawing.Point(550, 0)
        Me.INDLciValidationResult.MaxSize = New System.Drawing.Size(480, 29)
        Me.INDLciValidationResult.MinSize = New System.Drawing.Size(480, 29)
        Me.INDLciValidationResult.Name = "INDLciValidationResult"
        Me.INDLciValidationResult.ShowInCustomizationForm = False
        Me.INDLciValidationResult.Size = New System.Drawing.Size(547, 29)
        Me.INDLciValidationResult.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValidationResult.Text = "Resultado de la validación"
        Me.INDLciValidationResult.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValidationResult.TextSize = New System.Drawing.Size(480, 17)
        Me.INDLciValidationResult.TextToControlDistance = 5
        '
        'INDLciFalseValidation
        '
        Me.INDLciFalseValidation.AllowHide = False
        Me.INDLciFalseValidation.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciFalseValidation.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.cancel_16
        Me.INDLciFalseValidation.Location = New System.Drawing.Point(550, 58)
        Me.INDLciFalseValidation.MaxSize = New System.Drawing.Size(480, 29)
        Me.INDLciFalseValidation.MinSize = New System.Drawing.Size(480, 29)
        Me.INDLciFalseValidation.Name = "INDLciFalseValidation"
        Me.INDLciFalseValidation.ShowInCustomizationForm = False
        Me.INDLciFalseValidation.Size = New System.Drawing.Size(547, 29)
        Me.INDLciFalseValidation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFalseValidation.Text = "El peso de la nutrición NO se encuentra dentro del rango permitido."
        Me.INDLciFalseValidation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFalseValidation.TextSize = New System.Drawing.Size(480, 17)
        Me.INDLciFalseValidation.TextToControlDistance = 2
        Me.INDLciFalseValidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciObservation
        '
        Me.INDLciObservation.Control = Me.INDMeObservation
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 592)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Size = New System.Drawing.Size(1131, 134)
        Me.INDLciObservation.Text = "Observación"
        Me.INDLciObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservation.TextSize = New System.Drawing.Size(120, 10)
        Me.INDLciObservation.TextToControlDistance = 5
        '
        'INDLcgQualityControl
        '
        Me.INDLcgQualityControl.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgQualityControl.AppearanceGroup.Options.UseFont = True
        Me.INDLcgQualityControl.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgQualityControl.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgQualityControl.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgQualityControl.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgQualityControl.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgQualityControl.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgQualityControl.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgQualityControl.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgQualityControl.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgQualityControl.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgQualityControl.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgQualityControl.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgQualityControl, False)
        Me.INDLcgQualityControl.CustomizationFormText = "Información Principal"
        Me.INDLcgQualityControl.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciQuantityRequest, Me.INDLciCriticalDefect, Me.INDLcIIndicationSize, Me.INDLciReleaseQuantity, Me.INDLciPerformance})
        Me.INDLcgQualityControl.Location = New System.Drawing.Point(0, 265)
        Me.INDLcgQualityControl.Name = "INDLcgQualityControl"
        Me.INDLcgQualityControl.Size = New System.Drawing.Size(1131, 97)
        Me.INDLcgQualityControl.Text = "Control de calidad"
        Me.INDLcgQualityControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciQuantityRequest
        '
        Me.INDLciQuantityRequest.Control = Me.INDSeQuantityRequest
        Me.INDLciQuantityRequest.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciQuantityRequest.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciQuantityRequest.Location = New System.Drawing.Point(0, 0)
        Me.INDLciQuantityRequest.MaxSize = New System.Drawing.Size(287, 29)
        Me.INDLciQuantityRequest.MinSize = New System.Drawing.Size(287, 29)
        Me.INDLciQuantityRequest.Name = "INDLciQuantityRequest"
        Me.INDLciQuantityRequest.Size = New System.Drawing.Size(287, 29)
        Me.INDLciQuantityRequest.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantityRequest.Text = "Und Reempacadas / Reenvasadas"
        Me.INDLciQuantityRequest.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantityRequest.TextSize = New System.Drawing.Size(213, 13)
        Me.INDLciQuantityRequest.TextToControlDistance = 5
        '
        'INDLciCriticalDefect
        '
        Me.INDLciCriticalDefect.Control = Me.INDSeCriticalDefect
        Me.INDLciCriticalDefect.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciCriticalDefect.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciCriticalDefect.Location = New System.Drawing.Point(287, 0)
        Me.INDLciCriticalDefect.MaxSize = New System.Drawing.Size(175, 29)
        Me.INDLciCriticalDefect.MinSize = New System.Drawing.Size(175, 29)
        Me.INDLciCriticalDefect.Name = "INDLciCriticalDefect"
        Me.INDLciCriticalDefect.Size = New System.Drawing.Size(175, 29)
        Me.INDLciCriticalDefect.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCriticalDefect.Text = "Defecto crítico"
        Me.INDLciCriticalDefect.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCriticalDefect.TextSize = New System.Drawing.Size(100, 13)
        Me.INDLciCriticalDefect.TextToControlDistance = 5
        '
        'INDLcIIndicationSize
        '
        Me.INDLcIIndicationSize.Control = Me.INDSeIndicationSize
        Me.INDLcIIndicationSize.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLcIIndicationSize.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLcIIndicationSize.Location = New System.Drawing.Point(812, 0)
        Me.INDLcIIndicationSize.MinSize = New System.Drawing.Size(33, 17)
        Me.INDLcIIndicationSize.Name = "INDLcIIndicationSize"
        Me.INDLcIIndicationSize.Size = New System.Drawing.Size(295, 29)
        Me.INDLcIIndicationSize.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcIIndicationSize.Text = "Tamaño de la muestra"
        Me.INDLcIIndicationSize.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcIIndicationSize.TextSize = New System.Drawing.Size(147, 13)
        Me.INDLcIIndicationSize.TextToControlDistance = 5
        '
        'INDLciReleaseQuantity
        '
        Me.INDLciReleaseQuantity.Control = Me.INDSeReleaseQuantity
        Me.INDLciReleaseQuantity.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciReleaseQuantity.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciReleaseQuantity.Location = New System.Drawing.Point(462, 0)
        Me.INDLciReleaseQuantity.MaxSize = New System.Drawing.Size(175, 29)
        Me.INDLciReleaseQuantity.MinSize = New System.Drawing.Size(175, 29)
        Me.INDLciReleaseQuantity.Name = "INDLciReleaseQuantity"
        Me.INDLciReleaseQuantity.Size = New System.Drawing.Size(175, 29)
        Me.INDLciReleaseQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReleaseQuantity.Text = "Und a liberar"
        Me.INDLciReleaseQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciReleaseQuantity.TextSize = New System.Drawing.Size(100, 13)
        Me.INDLciReleaseQuantity.TextToControlDistance = 5
        '
        'INDLciPerformance
        '
        Me.INDLciPerformance.Control = Me.INDSePerformance
        Me.INDLciPerformance.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciPerformance.CustomizationFormText = "Tipo de Dosis Unitaria"
        Me.INDLciPerformance.Location = New System.Drawing.Point(637, 0)
        Me.INDLciPerformance.MaxSize = New System.Drawing.Size(175, 29)
        Me.INDLciPerformance.MinSize = New System.Drawing.Size(175, 29)
        Me.INDLciPerformance.Name = "INDLciPerformance"
        Me.INDLciPerformance.Size = New System.Drawing.Size(175, 29)
        Me.INDLciPerformance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPerformance.Text = "Rendimiento"
        Me.INDLciPerformance.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPerformance.TextSize = New System.Drawing.Size(100, 13)
        Me.INDLciPerformance.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmPopupDefectClassification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(144.0!, 144.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1150, 848)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupDefectClassification"
        Me.Opacity = 1.0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Clasificación de Defectos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        Me.INDPanelControlBase.PerformLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDMeObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeUnitDoseType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDefectClassification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDefectClassification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeTheoreticalWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeInputsWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeMinimumWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeSumInputsTheoreticalWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeMaximumWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeActualWeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeQuantityRequest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeCriticalDefect.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeReleaseQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSePerformance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeIndicationSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgParenteralNutrition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTheoreticalWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInputsWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMinimumWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSumInputsTheoreticalWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMaximumWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciActualWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTrueValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValidationResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFalseValidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgQualityControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuantityRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCriticalDefect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcIIndicationSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReleaseQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPerformance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcDefectClassification As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDefectClassification As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTeUnitDoseType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciUnitDoseType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents INDMeObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColDefectGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCritical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLess As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProduction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuality As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSeTheoreticalWeight As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcgParenteralNutrition As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciTheoreticalWeight As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeInputsWeight As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciInputsWeight As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeMinimumWeight As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciMinimumWeight As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeSumInputsTheoreticalWeight As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciSumInputsTheoreticalWeight As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeMaximumWeight As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciMaximumWeight As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciActualWeight As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTrueValidation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFalseValidation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValidationResult As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeActualWeight As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSeQuantityRequest As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSeCriticalDefect As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcgQualityControl As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciQuantityRequest As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCriticalDefect As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcIIndicationSize As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeReleaseQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciReleaseQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSePerformance As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciPerformance As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDSeIndicationSize As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
