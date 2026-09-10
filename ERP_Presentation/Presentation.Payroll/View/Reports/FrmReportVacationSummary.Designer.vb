Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportVacationSummary
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.INDSleemployee = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.root = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDdnMontClose = New Presentation.Controls.CtrDateNavigator()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgCriteria = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciMontClose = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcVacationSummary = New DevExpress.XtraGrid.GridControl()
        Me.INDgvVacationSummary = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl11 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton11 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDPanelControlReport = New DevExpress.XtraEditors.PanelControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDColEmployeeNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEmployeeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPeriod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColInitialAccrualDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFinalAccrualDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAccumulatedDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTakenDaysRegistered = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingDaysRegistered = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPendingDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEnjoyedDaysDF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVacationStartDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVacationEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColIncorporationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColRealIncorporationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPayrollLiquidationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLiquidationYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLiquidationMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVacationDaysInLiquidationMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBaseLiquidationValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVacationValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColHealthValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPensionValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNetVacationValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLastLiquidationIBC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLiquidationType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVacationType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPaymentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColVacationId = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleemployee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.root, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.root.SuspendLayout()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciMontClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgcVacationSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvVacationSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPanelControlReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlReport.SuspendLayout()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.root)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Dock = System.Windows.Forms.DockStyle.None
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1219, 321)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1219, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1219, 130)
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'INDSleemployee
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleemployee, AppearanceObject1)
        Me.IndigoSearchLookUpControl11.SetAppearanceEmbeddedNavigator(Me.INDSleemployee, AppearanceObject2)
        Me.IndigoSearchLookUpControl11.SetAppearanceTextFindControl(Me.INDSleemployee, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleemployee, AppearanceObject4)
        Me.IndigoSearchLookUpControl11.SetAppendButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDSleemployee, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetAutomaticOpenForm(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleemployee, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleemployee, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetCancelEditButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetEditButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleemployee, False)
        Me.INDSleemployee.EditValue = ""
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetEndEditButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetExportButton(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetFirstButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetLastButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleemployee, False)
        Me.INDSleemployee.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleemployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDSleemployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleemployee.MaximumSize = New System.Drawing.Size(385, 0)
        Me.INDSleemployee.Name = "INDSleemployee"
        Me.IndigoSearchLookUpControl11.SetNextButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetNextPageButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetOpenForm(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetPopupBestFitHeight(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetPopupSizeable(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetPrevButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetPrevPageButtonNavigator(Me.INDSleemployee, False)
        Me.INDSleemployee.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleemployee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleemployee.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleemployee.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleemployee.Properties.Appearance.Options.UseFont = True
        Me.INDSleemployee.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleemployee.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleemployee.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleemployee.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleemployee.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleemployee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleemployee.Properties.DisplayMember = "ThirdPartyNitName"
        Me.INDSleemployee.Properties.NullText = ""
        Me.INDSleemployee.Properties.PopupSizeable = False
        Me.INDSleemployee.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleemployee.Properties.ShowFooter = False
        Me.INDSleemployee.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetRemoveButtonNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleemployee, True)
        Me.IndigoSearchLookUpControl11.SetSaveXmlGrid(Me.INDSleemployee, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleemployee, True)
        Me.IndigoSearchLookUpControl11.SetShowDeleteButton(Me.INDSleemployee, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleemployee, True)
        Me.IndigoSearchLookUpControl11.SetShowFindButton(Me.INDSleemployee, True)
        Me.INDSleemployee.Size = New System.Drawing.Size(385, 28)
        Me.INDSleemployee.StyleController = Me.root
        Me.INDSleemployee.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleemployee, Nothing)
        Me.IndigoSearchLookUpControl11.SetTagForm(Me.INDSleemployee, Nothing)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDSleemployee, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleemployee, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleemployee, "{0} - {1}")
        Me.IndigoSearchLookUpControl11.SetTextStringFormat(Me.INDSleemployee, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetTxtFindEnterEnabled(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl11.SetUseEmbeddedNavigator(Me.INDSleemployee, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleemployee, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNit, Me.INDColName})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
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
        'root
        '
        Me.root.Controls.Add(Me.INDSleemployee)
        Me.root.Controls.Add(Me.INDSbGenerateReport)
        Me.root.Controls.Add(Me.INDdnMontClose)
        Me.root.Dock = System.Windows.Forms.DockStyle.Fill
        Me.root.Location = New System.Drawing.Point(202, 7)
        Me.root.Name = "root"
        Me.root.Root = Me.LayoutControlGroup2
        Me.root.Size = New System.Drawing.Size(1015, 312)
        Me.root.TabIndex = 5
        Me.root.Text = "LayoutControl2"
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(24, 183)
        Me.INDSbGenerateReport.MinimumSize = New System.Drawing.Size(0, 30)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.IndigoSimpleButton11.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(386, 30)
        Me.INDSbGenerateReport.StyleController = Me.root
        Me.INDSbGenerateReport.TabIndex = 6
        Me.INDSbGenerateReport.Text = "Generar reporte"
        '
        'INDdnMontClose
        '
        Me.INDdnMontClose.CtrCalendar = Nothing
        Me.INDdnMontClose.Enabled = False
        Me.INDdnMontClose.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.INDdnMontClose.Location = New System.Drawing.Point(24, 53)
        Me.INDdnMontClose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDdnMontClose.Name = "INDdnMontClose"
        Me.INDdnMontClose.Size = New System.Drawing.Size(386, 66)
        Me.INDdnMontClose.TabIndex = 7
        Me.INDdnMontClose.WithEvent = True
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgCriteria})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1015, 312)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLcgCriteria
        '
        Me.INDLcgCriteria.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCriteria, False)
        Me.INDLcgCriteria.CustomizationFormText = "Criterios"
        Me.INDLcgCriteria.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciEmployee, Me.INDLciGenerateReport, Me.INDlciMontClose})
        Me.INDLcgCriteria.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgCriteria.Name = "INDLcgCriteria"
        Me.INDLcgCriteria.Size = New System.Drawing.Size(995, 292)
        Me.INDLcgCriteria.Text = "Criterios"
        '
        'INDLciEmployee
        '
        Me.INDLciEmployee.Control = Me.INDSleemployee
        Me.INDLciEmployee.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciEmployee.CustomizationFormText = "Empleado"
        Me.INDLciEmployee.Location = New System.Drawing.Point(0, 70)
        Me.INDLciEmployee.MaxSize = New System.Drawing.Size(500, 60)
        Me.INDLciEmployee.MinSize = New System.Drawing.Size(390, 50)
        Me.INDLciEmployee.Name = "INDLciEmployee"
        Me.INDLciEmployee.Size = New System.Drawing.Size(971, 60)
        Me.INDLciEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEmployee.Text = "Empleado"
        Me.INDLciEmployee.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEmployee.TextSize = New System.Drawing.Size(63, 17)
        '
        'INDLciGenerateReport
        '
        Me.INDLciGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciGenerateReport.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciGenerateReport.CustomizationFormText = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Location = New System.Drawing.Point(0, 130)
        Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(390, 26)
        Me.INDLciGenerateReport.MinSize = New System.Drawing.Size(390, 26)
        Me.INDLciGenerateReport.Name = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Size = New System.Drawing.Size(971, 109)
        Me.INDLciGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateReport.TextVisible = False
        '
        'INDlciMontClose
        '
        Me.INDlciMontClose.Control = Me.INDdnMontClose
        Me.INDlciMontClose.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciMontClose.CustomizationFormText = "INDlciMontClose"
        Me.INDlciMontClose.Location = New System.Drawing.Point(0, 0)
        Me.INDlciMontClose.MaxSize = New System.Drawing.Size(390, 70)
        Me.INDlciMontClose.MinSize = New System.Drawing.Size(390, 70)
        Me.INDlciMontClose.Name = "INDlciMontClose"
        Me.INDlciMontClose.Size = New System.Drawing.Size(971, 70)
        Me.INDlciMontClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciMontClose.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciMontClose.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciMontClose.TextToControlDistance = 0
        Me.INDlciMontClose.TextVisible = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDgcVacationSummary)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1215, 274)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDgcVacationSummary
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcVacationSummary, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcVacationSummary, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcVacationSummary, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcVacationSummary, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcVacationSummary, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcVacationSummary, False)
        Me.INDgcVacationSummary.Location = New System.Drawing.Point(12, 12)
        Me.INDgcVacationSummary.MainView = Me.INDgvVacationSummary
        Me.INDgcVacationSummary.Name = "INDgcVacationSummary"
        Me.INDgcVacationSummary.Size = New System.Drawing.Size(1191, 250)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcVacationSummary, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcVacationSummary.TabIndex = 6
        Me.INDgcVacationSummary.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvVacationSummary})
        '
        'INDgvVacationSummary
        '
        Me.INDgvVacationSummary.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvVacationSummary.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvVacationSummary.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvVacationSummary.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvVacationSummary.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvVacationSummary.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvVacationSummary.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvVacationSummary.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvVacationSummary.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvVacationSummary.Appearance.Row.Options.UseFont = True
        Me.INDgvVacationSummary.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvVacationSummary.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvVacationSummary.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColEmployeeNit, Me.INDColEmployeeName, Me.INDColPeriod, Me.INDColInitialAccrualDate, Me.INDColFinalAccrualDate, Me.INDColAccumulatedDays, Me.INDColTakenDaysRegistered, Me.INDColPendingDaysRegistered, Me.INDColPendingDays, Me.INDColEnjoyedDaysDF, Me.INDColVacationStartDate, Me.INDColVacationEndDate, Me.INDColIncorporationDate, Me.INDColRealIncorporationDate, Me.INDColPayrollLiquidationDate, Me.INDColLiquidationYear, Me.INDColLiquidationMonth, Me.INDColVacationDaysInLiquidationMonth, Me.INDColBaseLiquidationValue, Me.INDColVacationValue, Me.INDColHealthValue, Me.INDColPensionValue, Me.INDColNetVacationValue, Me.INDColLastLiquidationIBC, Me.INDColLiquidationType, Me.INDColVacationType, Me.INDColPaymentType, Me.INDColVacationId})
        Me.INDgvVacationSummary.GridControl = Me.INDgcVacationSummary
        Me.INDgvVacationSummary.Name = "INDgvVacationSummary"
        Me.INDgvVacationSummary.OptionsBehavior.AutoPopulateColumns = False
        Me.INDgvVacationSummary.OptionsBehavior.Editable = False
        Me.INDgvVacationSummary.OptionsBehavior.ReadOnly = True
        Me.INDgvVacationSummary.OptionsMenu.EnableColumnMenu = True
        Me.INDgvVacationSummary.OptionsMenu.EnableFooterMenu = True
        Me.INDgvVacationSummary.OptionsView.ColumnAutoWidth = False
        Me.INDgvVacationSummary.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvVacationSummary.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvVacationSummary.OptionsView.ShowAutoFilterRow = True
        Me.INDgvVacationSummary.OptionsView.ShowFooter = True
        Me.INDgvVacationSummary.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvVacationSummary, False)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1215, 274)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcVacationSummary
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1195, 254)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.root
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 312)
        Me.CtrNavigationControlPanel1.TabIndex = 4
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDPanelControlReport
        '
        Me.INDPanelControlReport.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlReport.Location = New System.Drawing.Point(0, 480)
        Me.INDPanelControlReport.Name = "INDPanelControlReport"
        Me.INDPanelControlReport.Size = New System.Drawing.Size(1219, 278)
        Me.INDPanelControlReport.TabIndex = 1
        Me.INDPanelControlReport.Visible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'INDColEmployeeNit
        '
        Me.INDColEmployeeNit.Caption = "Nit empleado"
        Me.INDColEmployeeNit.FieldName = "EmployeeNit"
        Me.INDColEmployeeNit.Name = "INDColEmployeeNit"
        Me.INDColEmployeeNit.Visible = True
        Me.INDColEmployeeNit.VisibleIndex = 0
        Me.INDColEmployeeNit.Width = 120
        '
        'INDColEmployeeName
        '
        Me.INDColEmployeeName.Caption = "Nombre Empleado"
        Me.INDColEmployeeName.FieldName = "EmployeeName"
        Me.INDColEmployeeName.Name = "INDColEmployeeName"
        Me.INDColEmployeeName.Visible = True
        Me.INDColEmployeeName.VisibleIndex = 1

        '
        'INDColPeriod
        '
        Me.INDColPeriod.Caption = "Período"
        Me.INDColPeriod.FieldName = "Period"
        Me.INDColPeriod.Name = "INDColPeriod"
        Me.INDColPeriod.Visible = True
        Me.INDColPeriod.VisibleIndex = 2

        '
        'INDColInitialAccrualDate
        '
        Me.INDColInitialAccrualDate.Caption = "Fecha Inicial Causación"
        Me.INDColInitialAccrualDate.FieldName = "InitialAccrualDate"
        Me.INDColInitialAccrualDate.Name = "INDColInitialAccrualDate"
        Me.INDColInitialAccrualDate.Visible = True
        Me.INDColInitialAccrualDate.VisibleIndex = 3

        '
        'INDColFinalAccrualDate
        '
        Me.INDColFinalAccrualDate.Caption = "Fecha Final Causación"
        Me.INDColFinalAccrualDate.FieldName = "FinalAccrualDate"
        Me.INDColFinalAccrualDate.Name = "INDColFinalAccrualDate"
        Me.INDColFinalAccrualDate.Visible = True
        Me.INDColFinalAccrualDate.VisibleIndex = 4

        '
        'INDColAccumulatedDays
        '
        Me.INDColAccumulatedDays.Caption = "Días Acumulados"
        Me.INDColAccumulatedDays.FieldName = "AccumulatedDays"
        Me.INDColAccumulatedDays.Name = "INDColAccumulatedDays"
        Me.INDColAccumulatedDays.Visible = True
        Me.INDColAccumulatedDays.VisibleIndex = 5

        '
        'INDColTakenDaysRegistered
        '
        Me.INDColTakenDaysRegistered.Caption = "Días Tomados Registrados"
        Me.INDColTakenDaysRegistered.FieldName = "TakenDaysRegistered"
        Me.INDColTakenDaysRegistered.Name = "INDColTakenDaysRegistered"
        Me.INDColTakenDaysRegistered.Visible = True
        Me.INDColTakenDaysRegistered.VisibleIndex = 6

        '
        'INDColPendingDaysRegistered
        '
        Me.INDColPendingDaysRegistered.Caption = "Días Pendientes Registrados"
        Me.INDColPendingDaysRegistered.FieldName = "PendingDaysRegistered"
        Me.INDColPendingDaysRegistered.Name = "INDColPendingDaysRegistered"
        Me.INDColPendingDaysRegistered.Visible = True
        Me.INDColPendingDaysRegistered.VisibleIndex = 7

        '
        'INDColPendingDays
        '
        Me.INDColPendingDays.Caption = "Días Pendientes"
        Me.INDColPendingDays.FieldName = "PendingDays"
        Me.INDColPendingDays.Name = "INDColPendingDays"
        Me.INDColPendingDays.Visible = True
        Me.INDColPendingDays.VisibleIndex = 8

        '
        'INDColEnjoyedDaysDF
        '
        Me.INDColEnjoyedDaysDF.Caption = "Días Disfrutados DF"
        Me.INDColEnjoyedDaysDF.FieldName = "EnjoyedDaysDF"
        Me.INDColEnjoyedDaysDF.Name = "INDColEnjoyedDaysDF"
        Me.INDColEnjoyedDaysDF.Visible = True
        Me.INDColEnjoyedDaysDF.VisibleIndex = 9

        '
        'INDColVacationStartDate
        '
        Me.INDColVacationStartDate.Caption = "Fecha Inicio Vacaciones"
        Me.INDColVacationStartDate.FieldName = "VacationStartDate"
        Me.INDColVacationStartDate.Name = "INDColVacationStartDate"
        Me.INDColVacationStartDate.Visible = True
        Me.INDColVacationStartDate.VisibleIndex = 10

        '
        'INDColVacationEndDate
        '
        Me.INDColVacationEndDate.Caption = "Fecha Fin Vacaciones"
        Me.INDColVacationEndDate.FieldName = "VacationEndDate"
        Me.INDColVacationEndDate.Name = "INDColVacationEndDate"
        Me.INDColVacationEndDate.Visible = True
        Me.INDColVacationEndDate.VisibleIndex = 11

        '
        'INDColIncorporationDate
        '
        Me.INDColIncorporationDate.Caption = "Fecha Incorporación"
        Me.INDColIncorporationDate.FieldName = "IncorporationDate"
        Me.INDColIncorporationDate.Name = "INDColIncorporationDate"
        Me.INDColIncorporationDate.Visible = True
        Me.INDColIncorporationDate.VisibleIndex = 12

        '
        'INDColRealIncorporationDate
        '
        Me.INDColRealIncorporationDate.Caption = "Fecha Real Incorporación"
        Me.INDColRealIncorporationDate.FieldName = "RealIncorporationDate"
        Me.INDColRealIncorporationDate.Name = "INDColRealIncorporationDate"
        Me.INDColRealIncorporationDate.Visible = True
        Me.INDColRealIncorporationDate.VisibleIndex = 13

        '
        'INDColPayrollLiquidationDate
        '
        Me.INDColPayrollLiquidationDate.Caption = "Fecha Liquidación Nómina"
        Me.INDColPayrollLiquidationDate.FieldName = "PayrollLiquidationDate"
        Me.INDColPayrollLiquidationDate.Name = "INDColPayrollLiquidationDate"
        Me.INDColPayrollLiquidationDate.Visible = True
        Me.INDColPayrollLiquidationDate.VisibleIndex = 14

        '
        'INDColLiquidationYear
        '
        Me.INDColLiquidationYear.Caption = "Año Liquidación"
        Me.INDColLiquidationYear.FieldName = "LiquidationYear"
        Me.INDColLiquidationYear.Name = "INDColLiquidationYear"
        Me.INDColLiquidationYear.Visible = True
        Me.INDColLiquidationYear.VisibleIndex = 15

        '
        'INDColLiquidationMonth
        '
        Me.INDColLiquidationMonth.Caption = "Mes Liquidación"
        Me.INDColLiquidationMonth.FieldName = "LiquidationMonth"
        Me.INDColLiquidationMonth.Name = "INDColLiquidationMonth"
        Me.INDColLiquidationMonth.Visible = True
        Me.INDColLiquidationMonth.VisibleIndex = 16

        '
        'INDColVacationDaysInLiquidationMonth
        '
        Me.INDColVacationDaysInLiquidationMonth.Caption = "Días Vacaciones en Mes Liquidación"
        Me.INDColVacationDaysInLiquidationMonth.FieldName = "VacationDaysInLiquidationMonth"
        Me.INDColVacationDaysInLiquidationMonth.Name = "INDColVacationDaysInLiquidationMonth"
        Me.INDColVacationDaysInLiquidationMonth.Visible = True
        Me.INDColVacationDaysInLiquidationMonth.VisibleIndex = 17

        '
        'INDColBaseLiquidationValue
        '
        Me.INDColBaseLiquidationValue.Caption = "Valor Base Liquidación"
        Me.INDColBaseLiquidationValue.DisplayFormat.FormatString = "c2"
        Me.INDColBaseLiquidationValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColBaseLiquidationValue.FieldName = "BaseLiquidationValue"
        Me.INDColBaseLiquidationValue.Name = "INDColBaseLiquidationValue"
        Me.INDColBaseLiquidationValue.Visible = True
        Me.INDColBaseLiquidationValue.VisibleIndex = 18

        '
        'INDColVacationValue
        '
        Me.INDColVacationValue.Caption = "Valor Vacaciones"
        Me.INDColVacationValue.DisplayFormat.FormatString = "c2"
        Me.INDColVacationValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColVacationValue.FieldName = "VacationValue"
        Me.INDColVacationValue.Name = "INDColVacationValue"
        Me.INDColVacationValue.Visible = True
        Me.INDColVacationValue.VisibleIndex = 19

        '
        'INDColHealthValue
        '
        Me.INDColHealthValue.Caption = "Valor Salud"
        Me.INDColHealthValue.DisplayFormat.FormatString = "c2"
        Me.INDColHealthValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColHealthValue.FieldName = "HealthValue"
        Me.INDColHealthValue.Name = "INDColHealthValue"
        Me.INDColHealthValue.Visible = True
        Me.INDColHealthValue.VisibleIndex = 20

        '
        'INDColPensionValue
        '
        Me.INDColPensionValue.Caption = "Valor Pensión"
        Me.INDColPensionValue.DisplayFormat.FormatString = "c2"
        Me.INDColPensionValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColPensionValue.FieldName = "PensionValue"
        Me.INDColPensionValue.Name = "INDColPensionValue"
        Me.INDColPensionValue.Visible = True
        Me.INDColPensionValue.VisibleIndex = 21

        '
        'INDColNetVacationValue
        '
        Me.INDColNetVacationValue.Caption = "Valor Neto Vacaciones"
        Me.INDColNetVacationValue.DisplayFormat.FormatString = "c2"
        Me.INDColNetVacationValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColNetVacationValue.FieldName = "NetVacationValue"
        Me.INDColNetVacationValue.Name = "INDColNetVacationValue"
        Me.INDColNetVacationValue.Visible = True
        Me.INDColNetVacationValue.VisibleIndex = 22

        '
        'INDColLastLiquidationIBC
        '
        Me.INDColLastLiquidationIBC.Caption = "Último IBC Liquidación"
        Me.INDColLastLiquidationIBC.DisplayFormat.FormatString = "c2"
        Me.INDColLastLiquidationIBC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColLastLiquidationIBC.FieldName = "LastLiquidationIBC"
        Me.INDColLastLiquidationIBC.Name = "INDColLastLiquidationIBC"
        Me.INDColLastLiquidationIBC.Visible = True
        Me.INDColLastLiquidationIBC.VisibleIndex = 23

        '
        'INDColLiquidationType
        '
        Me.INDColLiquidationType.Caption = "Tipo Liquidación"
        Me.INDColLiquidationType.FieldName = "LiquidationType"
        Me.INDColLiquidationType.Name = "INDColLiquidationType"
        Me.INDColLiquidationType.Visible = True
        Me.INDColLiquidationType.VisibleIndex = 24

        '
        'INDColVacationType
        '
        Me.INDColVacationType.Caption = "Tipo Vacaciones"
        Me.INDColVacationType.FieldName = "VacationType"
        Me.INDColVacationType.Name = "INDColVacationType"
        Me.INDColVacationType.Visible = True
        Me.INDColVacationType.VisibleIndex = 25

        '
        'INDColPaymentType
        '
        Me.INDColPaymentType.Caption = "Tipo Pago"
        Me.INDColPaymentType.FieldName = "PaymentType"
        Me.INDColPaymentType.Name = "INDColPaymentType"
        Me.INDColPaymentType.Visible = True
        Me.INDColPaymentType.VisibleIndex = 26

        '
        'INDColVacationId
        '
        Me.INDColVacationId.Caption = "ID Vacaciones"
        Me.INDColVacationId.FieldName = "VacationId"
        Me.INDColVacationId.Name = "INDColVacationId"
        Me.INDColVacationId.Visible = True
        Me.INDColVacationId.VisibleIndex = 27

        '
        'FrmReportVacationSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1219, 758)
        Me.Controls.Add(Me.INDPanelControlReport)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmReportVacationSummary"
        Me.Opacity = 1.0R
        Me.Tag = "89036"
        Me.Text = "Reporte vacaciones "
        Me.Controls.SetChildIndex(Me.INDPanelControlReport, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleemployee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.root, System.ComponentModel.ISupportInitialize).EndInit()
        Me.root.ResumeLayout(False)
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciMontClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgcVacationSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvVacationSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPanelControlReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlReport.ResumeLayout(False)
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents root As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSleemployee As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl11 As IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit11 As IndigoTextEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton11 As IndigoSimpleButton
    Friend WithEvents INDdnMontClose As CtrDateNavigator
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgCriteria As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciMontClose As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPanelControlReport As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDgcVacationSummary As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvVacationSummary As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColEmployeeNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEmployeeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPeriod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColInitialAccrualDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFinalAccrualDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAccumulatedDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTakenDaysRegistered As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPendingDaysRegistered As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPendingDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEnjoyedDaysDF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVacationStartDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVacationEndDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColIncorporationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColRealIncorporationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPayrollLiquidationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLiquidationYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLiquidationMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVacationDaysInLiquidationMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBaseLiquidationValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVacationValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColHealthValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPensionValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNetVacationValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLastLiquidationIBC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLiquidationType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVacationType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPaymentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColVacationId As DevExpress.XtraGrid.Columns.GridColumn
End Class
