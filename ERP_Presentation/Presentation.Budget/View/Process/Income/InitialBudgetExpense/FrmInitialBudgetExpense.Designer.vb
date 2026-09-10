Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInitialBudgetExpense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInitialBudgetExpense))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLyAnnualizedCashFlowModification = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccChangeEntity = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleValidityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvValidityPopUp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidityPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonthPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleBudgetEntityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciEntityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColResource = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTipe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptIcbMonth = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDColResidue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCharacter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptIcbNature = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDColValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmemoObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtDocument = New DevExpress.XtraEditors.TextEdit()
        Me.INDdteDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnConsecutive = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleBudgetEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygCategory = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyAnnualizedCashFlowModification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyAnnualizedCashFlowModification.SuspendLayout()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccChangeEntity.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBudgetEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIcbMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIcbNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnConsecutive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyAnnualizedCashFlowModification)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1215, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1215, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1215, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyAnnualizedCashFlowModification
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLyAnnualizedCashFlowModification
        '
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDpccChangeEntity)
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDgcDetail)
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDBtnAdd)
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDmemoObservations)
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDtxtDocument)
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDdteDate)
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDbtnConsecutive)
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDsleValidity)
        Me.INDLyAnnualizedCashFlowModification.Controls.Add(Me.INDsleBudgetEntity)
        Me.INDLyAnnualizedCashFlowModification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLyAnnualizedCashFlowModification.Location = New System.Drawing.Point(202, 7)
        Me.INDLyAnnualizedCashFlowModification.Name = "INDLyAnnualizedCashFlowModification"
        Me.INDLyAnnualizedCashFlowModification.Root = Me.LayoutControlGroup1
        Me.INDLyAnnualizedCashFlowModification.Size = New System.Drawing.Size(1011, 557)
        Me.INDLyAnnualizedCashFlowModification.TabIndex = 1
        Me.INDLyAnnualizedCashFlowModification.Text = "LayoutControl1"
        '
        'INDpccChangeEntity
        '
        Me.INDpccChangeEntity.Controls.Add(Me.LayoutControl2)
        Me.INDpccChangeEntity.Location = New System.Drawing.Point(75, 341)
        Me.INDpccChangeEntity.Name = "INDpccChangeEntity"
        Me.INDpccChangeEntity.Size = New System.Drawing.Size(299, 150)
        Me.INDpccChangeEntity.TabIndex = 10
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDsleValidityPopUp)
        Me.LayoutControl2.Controls.Add(Me.INDsleBudgetEntityPopUp)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup4
        Me.LayoutControl2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDsleValidityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidityPopUp, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidityPopUp, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.INDsleValidityPopUp.Location = New System.Drawing.Point(12, 96)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidityPopUp.Name = "INDsleValidityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.INDsleValidityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidityPopUp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleValidityPopUp.Properties.DisplayMember = "Year"
        Me.INDsleValidityPopUp.Properties.NullText = ""
        Me.INDsleValidityPopUp.Properties.PopupSizeable = False
        Me.INDsleValidityPopUp.Properties.PopupView = Me.INDGvValidityPopUp
        Me.INDsleValidityPopUp.Properties.ShowFooter = False
        Me.INDsleValidityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidityPopUp, True)
        Me.INDsleValidityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleValidityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleValidityPopUp.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidityPopUp, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidityPopUp, False)
        '
        'INDGvValidityPopUp
        '
        Me.INDGvValidityPopUp.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvValidityPopUp.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvValidityPopUp.Appearance.Row.Options.UseFont = True
        Me.INDGvValidityPopUp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.INDgcStatusValidityPopUp, Me.GridColumn7, Me.GridColumn8, Me.INDgcIncomeMonthPopUp})
        Me.INDGvValidityPopUp.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvValidityPopUp.Name = "INDGvValidityPopUp"
        Me.INDGvValidityPopUp.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvValidityPopUp.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvValidityPopUp.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvValidityPopUp.OptionsView.ShowAutoFilterRow = True
        Me.INDGvValidityPopUp.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvValidityPopUp, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Año"
        Me.GridColumn5.FieldName = "Year"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'INDgcStatusValidityPopUp
        '
        Me.INDgcStatusValidityPopUp.Caption = "Estado"
        Me.INDgcStatusValidityPopUp.FieldName = "StatusText"
        Me.INDgcStatusValidityPopUp.Name = "INDgcStatusValidityPopUp"
        Me.INDgcStatusValidityPopUp.Visible = True
        Me.INDgcStatusValidityPopUp.VisibleIndex = 1
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Resolución"
        Me.GridColumn7.FieldName = "ResolutionNumber"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Valor"
        Me.GridColumn8.DisplayFormat.FormatString = "c0"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "ResolutionValue"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        '
        'INDgcIncomeMonthPopUp
        '
        Me.INDgcIncomeMonthPopUp.Caption = "Mes Ingreso"
        Me.INDgcIncomeMonthPopUp.FieldName = "IncomeMonth"
        Me.INDgcIncomeMonthPopUp.Name = "INDgcIncomeMonthPopUp"
        '
        'INDsleBudgetEntityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBudgetEntityPopUp, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBudgetEntityPopUp, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.INDsleBudgetEntityPopUp.Location = New System.Drawing.Point(12, 32)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleBudgetEntityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBudgetEntityPopUp.Name = "INDsleBudgetEntityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleBudgetEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.INDsleBudgetEntityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleBudgetEntityPopUp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleBudgetEntityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleBudgetEntityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleBudgetEntityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleBudgetEntityPopUp.Properties.DisplayMember = "NameCode"
        Me.INDsleBudgetEntityPopUp.Properties.NullText = ""
        Me.INDsleBudgetEntityPopUp.Properties.PopupSizeable = False
        Me.INDsleBudgetEntityPopUp.Properties.PopupView = Me.GridView2
        Me.INDsleBudgetEntityPopUp.Properties.ShowFooter = False
        Me.INDsleBudgetEntityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleBudgetEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleBudgetEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleBudgetEntityPopUp, True)
        Me.INDsleBudgetEntityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleBudgetEntityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleBudgetEntityPopUp.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleBudgetEntityPopUp, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleBudgetEntityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleBudgetEntityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleBudgetEntityPopUp, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsCustomization.AllowGroup = False
        Me.GridView2.OptionsDetail.EnableMasterViewMode = False
        Me.GridView2.OptionsDetail.ShowDetailTabs = False
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowDetailButtons = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup4, False)
        Me.LayoutControlGroup4.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEntityPopUp, Me.INDlciValidityPopUp})
        Me.LayoutControlGroup4.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'INDlciEntityPopUp
        '
        Me.INDlciEntityPopUp.Control = Me.INDsleBudgetEntityPopUp
        Me.INDlciEntityPopUp.CustomizationFormText = "LayoutControlItem3"
        Me.INDlciEntityPopUp.Location = New System.Drawing.Point(0, 0)
        Me.INDlciEntityPopUp.MaxSize = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.MinSize = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.Name = "INDlciEntityPopUp"
        Me.INDlciEntityPopUp.Size = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEntityPopUp.Text = "Entidad Presupuestal"
        Me.INDlciEntityPopUp.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciEntityPopUp.TextSize = New System.Drawing.Size(134, 17)
        '
        'INDlciValidityPopUp
        '
        Me.INDlciValidityPopUp.Control = Me.INDsleValidityPopUp
        Me.INDlciValidityPopUp.CustomizationFormText = "LayoutControlItem4"
        Me.INDlciValidityPopUp.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidityPopUp.MaxSize = New System.Drawing.Size(279, 64)
        Me.INDlciValidityPopUp.MinSize = New System.Drawing.Size(279, 64)
        Me.INDlciValidityPopUp.Name = "INDlciValidityPopUp"
        Me.INDlciValidityPopUp.Size = New System.Drawing.Size(279, 66)
        Me.INDlciValidityPopUp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidityPopUp.Text = "Vigencia"
        Me.INDlciValidityPopUp.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidityPopUp.TextSize = New System.Drawing.Size(134, 17)
        '
        'INDgcDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetail, Nothing)
        Me.INDgcDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.INDgcDetail.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDgcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetail, False)
        Me.INDgcDetail.Location = New System.Drawing.Point(852, 95)
        Me.INDgcDetail.MainView = Me.INDgvDetail
        Me.INDgcDetail.Name = "INDgcDetail"
        Me.INDgcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptIcbNature, Me.INDRptIcbMonth, Me.INDRptTxtValue})
        Me.INDgcDetail.Size = New System.Drawing.Size(896, 421)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDetail.TabIndex = 12
        Me.INDgcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetail})
        '
        'INDgvDetail
        '
        Me.INDgvDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCode, Me.INDColResource, Me.INDColTipe, Me.INDColResidue, Me.INDColCharacter, Me.INDColValue})
        Me.INDgvDetail.GridControl = Me.INDgcDetail
        Me.INDgvDetail.Name = "INDgvDetail"
        Me.INDgvDetail.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvDetail.OptionsDetail.SmartDetailExpand = False
        Me.INDgvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvDetail.OptionsView.ShowDetailButtons = False
        Me.INDgvDetail.OptionsView.ShowFooter = True
        Me.INDgvDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDetail, False)
        Me.INDgvDetail.ViewCaption = "Identificación del Rubro"
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Rubro"
        Me.INDColCode.FieldName = "CodeNameCategory"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.OptionsColumn.ReadOnly = True
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 0
        Me.INDColCode.Width = 232
        '
        'INDColResource
        '
        Me.INDColResource.Caption = "Recurso"
        Me.INDColResource.FieldName = "CategoryResource"
        Me.INDColResource.Name = "INDColResource"
        Me.INDColResource.OptionsColumn.AllowEdit = False
        Me.INDColResource.OptionsColumn.AllowFocus = False
        Me.INDColResource.OptionsColumn.ReadOnly = True
        Me.INDColResource.Visible = True
        Me.INDColResource.VisibleIndex = 1
        Me.INDColResource.Width = 166
        '
        'INDColTipe
        '
        Me.INDColTipe.Caption = "Mes"
        Me.INDColTipe.ColumnEdit = Me.INDRptIcbMonth
        Me.INDColTipe.FieldName = "Month"
        Me.INDColTipe.Name = "INDColTipe"
        Me.INDColTipe.OptionsColumn.AllowEdit = False
        Me.INDColTipe.OptionsColumn.AllowFocus = False
        Me.INDColTipe.OptionsColumn.ReadOnly = True
        Me.INDColTipe.Visible = True
        Me.INDColTipe.VisibleIndex = 2
        Me.INDColTipe.Width = 170
        '
        'INDRptIcbMonth
        '
        Me.INDRptIcbMonth.AutoHeight = False
        Me.INDRptIcbMonth.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Enero", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Febrero", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Marzo", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Abril", CType(4, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Mayo", CType(5, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Junio", CType(6, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Julio", CType(7, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Agosto", CType(8, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Septiembre", CType(9, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Octubre", CType(10, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Noviembre", CType(11, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Diciembre", CType(12, Byte), -1)})
        Me.INDRptIcbMonth.Name = "INDRptIcbMonth"
        '
        'INDColResidue
        '
        Me.INDColResidue.Caption = "Saldo"
        Me.INDColResidue.DisplayFormat.FormatString = "c0"
        Me.INDColResidue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColResidue.FieldName = "Balance"
        Me.INDColResidue.Name = "INDColResidue"
        Me.INDColResidue.OptionsColumn.AllowEdit = False
        Me.INDColResidue.OptionsColumn.AllowFocus = False
        Me.INDColResidue.OptionsColumn.ReadOnly = True
        Me.INDColResidue.Visible = True
        Me.INDColResidue.VisibleIndex = 3
        Me.INDColResidue.Width = 161
        '
        'INDColCharacter
        '
        Me.INDColCharacter.Caption = "Naturaleza"
        Me.INDColCharacter.ColumnEdit = Me.INDRptIcbNature
        Me.INDColCharacter.FieldName = "Nature"
        Me.INDColCharacter.Name = "INDColCharacter"
        Me.INDColCharacter.Visible = True
        Me.INDColCharacter.VisibleIndex = 4
        Me.INDColCharacter.Width = 129
        '
        'INDRptIcbNature
        '
        Me.INDRptIcbNature.AutoHeight = False
        Me.INDRptIcbNature.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptIcbNature.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguna", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Débito", CType(1, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Crédito", CType(2, Byte), 1)})
        Me.INDRptIcbNature.LargeImages = Me.ImageCollection1
        Me.INDRptIcbNature.Name = "INDRptIcbNature"
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "-.png")
        Me.ImageCollection1.Images.SetKeyName(1, "+.png")
        '
        'INDColValue
        '
        Me.INDColValue.Caption = "Valor"
        Me.INDColValue.ColumnEdit = Me.INDRptTxtValue
        Me.INDColValue.DisplayFormat.FormatString = "c0"
        Me.INDColValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColValue.FieldName = "Value"
        Me.INDColValue.Name = "INDColValue"
        Me.INDColValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", "{0:c0}")})
        Me.INDColValue.Visible = True
        Me.INDColValue.VisibleIndex = 5
        Me.INDColValue.Width = 202
        '
        'INDRptTxtValue
        '
        Me.INDRptTxtValue.AutoHeight = False
        Me.INDRptTxtValue.Mask.EditMask = "c0"
        Me.INDRptTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDRptTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDRptTxtValue.MaxLength = 16
        Me.INDRptTxtValue.Name = "INDRptTxtValue"
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.Location = New System.Drawing.Point(852, 59)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(896, 32)
        Me.INDBtnAdd.StyleController = Me.INDLyAnnualizedCashFlowModification
        Me.INDBtnAdd.TabIndex = 11
        Me.INDBtnAdd.Text = "Agregar"
        '
        'INDmemoObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoObservations, True)
        Me.INDmemoObservations.EnterMoveNextControl = True
        Me.INDmemoObservations.Location = New System.Drawing.Point(438, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoObservations.Name = "INDmemoObservations"
        Me.INDmemoObservations.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmemoObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoObservations.Properties.Appearance.Options.UseFont = True
        Me.INDmemoObservations.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmemoObservations.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmemoObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservations.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmemoObservations.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmemoObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoObservations.Properties.MaxLength = 500
        Me.INDmemoObservations.Size = New System.Drawing.Size(386, 70)
        Me.INDmemoObservations.StyleController = Me.INDLyAnnualizedCashFlowModification
        Me.INDmemoObservations.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoObservations, 0)
        Me.INDmemoObservations.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDocument, True)
        Me.INDtxtDocument.EnterMoveNextControl = True
        Me.INDtxtDocument.Location = New System.Drawing.Point(438, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDocument.Name = "INDtxtDocument"
        Me.INDtxtDocument.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDocument.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDocument.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtDocument.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtDocument.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDocument.Properties.MaxLength = 100
        Me.INDtxtDocument.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtDocument.StyleController = Me.INDLyAnnualizedCashFlowModification
        Me.INDtxtDocument.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDocument, 0)
        Me.INDtxtDocument.ToolTip = "Este Campo es Necesario"
        '
        'INDdteDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteDate, True)
        Me.INDdteDate.EditValue = Nothing
        Me.INDdteDate.EnterMoveNextControl = True
        Me.INDdteDate.Location = New System.Drawing.Point(438, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDdteDate.Name = "INDdteDate"
        Me.INDdteDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdteDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDate.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDdteDate.Properties.Mask.BeepOnError = True
        Me.INDdteDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteDate.StyleController = Me.INDLyAnnualizedCashFlowModification
        Me.INDdteDate.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteDate, 0)
        Me.INDdteDate.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnConsecutive
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnConsecutive, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnConsecutive, True)
        Me.INDbtnConsecutive.EditValue = ""
        Me.INDbtnConsecutive.Location = New System.Drawing.Point(438, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnConsecutive, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnConsecutive.Name = "INDbtnConsecutive"
        Me.INDbtnConsecutive.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnConsecutive.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnConsecutive.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnConsecutive.Properties.Appearance.Options.UseFont = True
        Me.INDbtnConsecutive.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnConsecutive.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnConsecutive.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnConsecutive.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnConsecutive.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnConsecutive.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Budget.My.Resources.Resources.BuscarMetro
        Me.INDbtnConsecutive.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnConsecutive.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnConsecutive.StyleController = Me.INDLyAnnualizedCashFlowModification
        Me.INDbtnConsecutive.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnConsecutive, 0)
        Me.INDbtnConsecutive.ToolTip = "Este Campo es Necesario"
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidity.Name = "INDsleValidity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleValidity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidity.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleValidity.Properties.DisplayMember = "Year"
        Me.INDsleValidity.Properties.NullText = ""
        Me.INDsleValidity.Properties.PopupSizeable = False
        Me.INDsleValidity.Properties.PopupView = Me.INDgvValidity
        Me.INDsleValidity.Properties.ShowFooter = False
        Me.INDsleValidity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidity, True)
        Me.INDsleValidity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleValidity.StyleController = Me.INDLyAnnualizedCashFlowModification
        Me.INDsleValidity.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidity, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidity, False)
        '
        'INDgvValidity
        '
        Me.INDgvValidity.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvValidity.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvValidity.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvValidity.Appearance.Row.Options.UseFont = True
        Me.INDgvValidity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.INDgcStatusValidity, Me.GridColumn13, Me.GridColumn14, Me.INDgcIncomeMonth})
        Me.INDgvValidity.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvValidity.Name = "INDgvValidity"
        Me.INDgvValidity.OptionsCustomization.AllowGroup = False
        Me.INDgvValidity.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvValidity.OptionsDetail.ShowDetailTabs = False
        Me.INDgvValidity.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvValidity.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvValidity.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvValidity.OptionsView.ShowAutoFilterRow = True
        Me.INDgvValidity.OptionsView.ShowDetailButtons = False
        Me.INDgvValidity.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvValidity, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Año"
        Me.GridColumn11.FieldName = "Year"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'INDgcStatusValidity
        '
        Me.INDgcStatusValidity.Caption = "Estado"
        Me.INDgcStatusValidity.FieldName = "StatusText"
        Me.INDgcStatusValidity.Name = "INDgcStatusValidity"
        Me.INDgcStatusValidity.OptionsColumn.AllowEdit = False
        Me.INDgcStatusValidity.OptionsColumn.AllowFocus = False
        Me.INDgcStatusValidity.Visible = True
        Me.INDgcStatusValidity.VisibleIndex = 1
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Resolución"
        Me.GridColumn13.FieldName = "ResolutionNumber"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 2
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Valor"
        Me.GridColumn14.DisplayFormat.FormatString = "c0"
        Me.GridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn14.FieldName = "ResolutionValue"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 3
        '
        'INDgcIncomeMonth
        '
        Me.INDgcIncomeMonth.Caption = "Mes Ingreso"
        Me.INDgcIncomeMonth.FieldName = "IncomeMonth"
        Me.INDgcIncomeMonth.Name = "INDgcIncomeMonth"
        '
        'INDsleBudgetEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBudgetEntity, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBudgetEntity, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleBudgetEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleBudgetEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBudgetEntity.Name = "INDsleBudgetEntity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleBudgetEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleBudgetEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleBudgetEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleBudgetEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleBudgetEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleBudgetEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleBudgetEntity.Properties.NullText = ""
        Me.INDsleBudgetEntity.Properties.PopupSizeable = False
        Me.INDsleBudgetEntity.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleBudgetEntity.Properties.ShowFooter = False
        Me.INDsleBudgetEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleBudgetEntity, True)
        Me.INDsleBudgetEntity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleBudgetEntity.StyleController = Me.INDLyAnnualizedCashFlowModification
        Me.INDsleBudgetEntity.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleBudgetEntity, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleBudgetEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleBudgetEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleBudgetEntity, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEdit1View.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEdit1View.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygGeneralInformation, Me.INDlygCategory})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1772, 540)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygPrincipalData
        '
        Me.INDlygPrincipalData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.INDlygPrincipalData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.INDlygPrincipalData.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalData.Name = "INDlygPrincipalData"
        Me.INDlygPrincipalData.Size = New System.Drawing.Size(414, 520)
        Me.INDlygPrincipalData.Text = "Datos principales"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDsleBudgetEntity
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Entidad Presupuestal"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsleValidity
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 401)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Vigencia"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(414, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(414, 520)
        Me.INDlygGeneralInformation.Text = "Información General"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDbtnConsecutive
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.ShowInCustomizationForm = False
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Código"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDdteDate
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(360, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.ShowInCustomizationForm = False
        Me.LayoutControlItem4.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Fecha"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDtxtDocument
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.ShowInCustomizationForm = False
        Me.LayoutControlItem5.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Documento"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(74, 17)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDmemoObservations
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(390, 100)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(390, 100)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.ShowInCustomizationForm = False
        Me.LayoutControlItem6.Size = New System.Drawing.Size(390, 281)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "Observaciones"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem6.TextToControlDistance = 5
        '
        'INDlygCategory
        '
        Me.INDlygCategory.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygCategory.AppearanceGroup.Options.UseFont = True
        Me.INDlygCategory.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygCategory.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygCategory.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCategory.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygCategory.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygCategory.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygCategory.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCategory.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygCategory.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCategory.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygCategory.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCategory.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygCategory, False)
        Me.INDlygCategory.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem8})
        Me.INDlygCategory.Location = New System.Drawing.Point(828, 0)
        Me.INDlygCategory.Name = "INDlygCategory"
        Me.INDlygCategory.Size = New System.Drawing.Size(924, 520)
        Me.INDlygCategory.Text = "Listado de Rubros"
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDBtnAdd
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(900, 36)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(900, 36)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(900, 36)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDgcDetail
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(900, 0)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(900, 1)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(900, 425)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmInitialBudgetExpense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 688)
        Me.Name = "FrmInitialBudgetExpense"
        Me.Opacity = 1.0R
        Me.Tag = "211"
        Me.Text = "Modificaciones del P.A.C. - Ingresos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyAnnualizedCashFlowModification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyAnnualizedCashFlowModification.ResumeLayout(False)
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccChangeEntity.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBudgetEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIcbMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIcbNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnConsecutive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLyAnnualizedCashFlowModification As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDsleBudgetEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnConsecutive As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDocument As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpccChangeEntity As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsleValidityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvValidityPopUp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidityPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonthPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleBudgetEntityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciEntityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciValidityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlygCategory As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDRptIcbNature As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRptIcbMonth As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents INDRptTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDgvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColResource As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTipe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColResidue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCharacter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColValue As DevExpress.XtraGrid.Columns.GridColumn
End Class
