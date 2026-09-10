Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBudgetAmendmentsExpense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBudgetAmendmentsExpense))
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
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
        Me.INDlcBudgetM = New DevExpress.XtraLayout.LayoutControl()
        Me.INDmeObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsbAddCategory = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColResource = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTipe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColResidue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCharacter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIceNature = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDColValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDtxtDocument = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDteDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDpccChangeEntity = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleValidityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvValidityPopUp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidityPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonthPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleEntityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciEntityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrBudgetAmendmentsRevenue = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemConsecutive = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrRubro = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcBudgetM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcBudgetM.SuspendLayout()
        CType(Me.INDmeObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIceNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccChangeEntity.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrBudgetAmendmentsRevenue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemConsecutive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrRubro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcBudgetM)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1157, 548)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1157, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1157, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcBudgetM
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 539)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcBudgetM
        '
        Me.INDlcBudgetM.AllowCustomization = False
        Me.INDlcBudgetM.Controls.Add(Me.INDmeObservations)
        Me.INDlcBudgetM.Controls.Add(Me.INDsleValidity)
        Me.INDlcBudgetM.Controls.Add(Me.INDsbAddCategory)
        Me.INDlcBudgetM.Controls.Add(Me.INDgcDetail)
        Me.INDlcBudgetM.Controls.Add(Me.INDtxtDocument)
        Me.INDlcBudgetM.Controls.Add(Me.INDsleEntity)
        Me.INDlcBudgetM.Controls.Add(Me.INDteDate)
        Me.INDlcBudgetM.Controls.Add(Me.INDbtnCode)
        Me.INDlcBudgetM.Controls.Add(Me.INDpccChangeEntity)
        Me.INDlcBudgetM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlcBudgetM, False)
        Me.INDlcBudgetM.Location = New System.Drawing.Point(202, 7)
        Me.INDlcBudgetM.Name = "INDlcBudgetM"
        Me.INDlcBudgetM.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(562, 210, 835, 571)
        Me.INDlcBudgetM.Root = Me.LayoutControlGroup1
        Me.INDlcBudgetM.Size = New System.Drawing.Size(953, 539)
        Me.INDlcBudgetM.TabIndex = 1
        Me.INDlcBudgetM.Text = "LayoutControl1"
        '
        'INDmeObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeObservations, False)
        Me.INDmeObservations.EnterMoveNextControl = True
        Me.INDmeObservations.Location = New System.Drawing.Point(438, 276)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeObservations.Name = "INDmeObservations"
        Me.INDmeObservations.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeObservations.Properties.Appearance.Options.UseFont = True
        Me.INDmeObservations.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeObservations.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeObservations.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeObservations.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeObservations.Properties.MaxLength = 500
        Me.INDmeObservations.Size = New System.Drawing.Size(386, 71)
        Me.INDmeObservations.StyleController = Me.INDlcBudgetM
        Me.INDmeObservations.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeObservations, 0)
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject2)
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
        Me.INDsleValidity.Location = New System.Drawing.Point(24, 149)
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
        Me.INDsleValidity.StyleController = Me.INDlcBudgetM
        Me.INDsleValidity.TabIndex = 2
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
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseForeColor = True
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
        'INDsbAddCategory
        '
        Me.INDsbAddCategory.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAddCategory.Appearance.Options.UseFont = True
        Me.INDsbAddCategory.Location = New System.Drawing.Point(852, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAddCategory, True)
        Me.INDsbAddCategory.Name = "INDsbAddCategory"
        Me.INDsbAddCategory.Size = New System.Drawing.Size(824, 32)
        Me.INDsbAddCategory.StyleController = Me.INDlcBudgetM
        Me.INDsbAddCategory.TabIndex = 7
        Me.INDsbAddCategory.Text = "Agregar Rubro a Modificación"
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
        Me.INDgcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepIceNature, Me.INDrepTxtValue})
        Me.INDgcDetail.Size = New System.Drawing.Size(824, 403)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDetail.TabIndex = 8
        Me.INDgcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetail})
        '
        'INDgvDetail
        '
        Me.INDgvDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCode, Me.INDColName, Me.INDColResource, Me.INDColTipe, Me.INDColResidue, Me.INDColCharacter, Me.INDColValue})
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
        Me.INDColCode.Caption = "Código"
        Me.INDColCode.FieldName = "CodeCategory"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 0
        Me.INDColCode.Width = 68
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "NameCategory"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.OptionsColumn.AllowFocus = False
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 1
        Me.INDColName.Width = 172
        '
        'INDColResource
        '
        Me.INDColResource.Caption = "Recurso"
        Me.INDColResource.FieldName = "CodeNameFinancialSource"
        Me.INDColResource.Name = "INDColResource"
        Me.INDColResource.OptionsColumn.AllowEdit = False
        Me.INDColResource.OptionsColumn.AllowFocus = False
        Me.INDColResource.Visible = True
        Me.INDColResource.VisibleIndex = 2
        Me.INDColResource.Width = 113
        '
        'INDColTipe
        '
        Me.INDColTipe.Caption = "Tipo"
        Me.INDColTipe.FieldName = "CodeNameRevenueType"
        Me.INDColTipe.Name = "INDColTipe"
        Me.INDColTipe.OptionsColumn.AllowEdit = False
        Me.INDColTipe.OptionsColumn.AllowFocus = False
        Me.INDColTipe.Visible = True
        Me.INDColTipe.VisibleIndex = 3
        Me.INDColTipe.Width = 130
        '
        'INDColResidue
        '
        Me.INDColResidue.Caption = "Saldo"
        Me.INDColResidue.DisplayFormat.FormatString = "C0"
        Me.INDColResidue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColResidue.FieldName = "BalanceBudget"
        Me.INDColResidue.Name = "INDColResidue"
        Me.INDColResidue.OptionsColumn.AllowEdit = False
        Me.INDColResidue.OptionsColumn.AllowFocus = False
        Me.INDColResidue.Visible = True
        Me.INDColResidue.VisibleIndex = 4
        Me.INDColResidue.Width = 155
        '
        'INDColCharacter
        '
        Me.INDColCharacter.Caption = "Naturaleza"
        Me.INDColCharacter.ColumnEdit = Me.INDrepIceNature
        Me.INDColCharacter.FieldName = "Nature"
        Me.INDColCharacter.Name = "INDColCharacter"
        Me.INDColCharacter.Visible = True
        Me.INDColCharacter.VisibleIndex = 5
        Me.INDColCharacter.Width = 103
        '
        'INDrepIceNature
        '
        Me.INDrepIceNature.AutoHeight = False
        Me.INDrepIceNature.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepIceNature.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguno", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Débito", CType(1, Byte), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Crédito", CType(2, Byte), 1)})
        Me.INDrepIceNature.LargeImages = Me.ImageCollection1
        Me.INDrepIceNature.Name = "INDrepIceNature"
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
        Me.INDColValue.ColumnEdit = Me.INDrepTxtValue
        Me.INDColValue.DisplayFormat.FormatString = "c0"
        Me.INDColValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColValue.FieldName = "Value"
        Me.INDColValue.Name = "INDColValue"
        Me.INDColValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", "{0:c0}")})
        Me.INDColValue.Visible = True
        Me.INDColValue.VisibleIndex = 6
        Me.INDColValue.Width = 135
        '
        'INDrepTxtValue
        '
        Me.INDrepTxtValue.AutoHeight = False
        Me.INDrepTxtValue.Mask.EditMask = "c0"
        Me.INDrepTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValue.Name = "INDrepTxtValue"
        '
        'INDtxtDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDocument, False)
        Me.INDtxtDocument.EnterMoveNextControl = True
        Me.INDtxtDocument.Location = New System.Drawing.Point(438, 213)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDocument.Name = "INDtxtDocument"
        Me.INDtxtDocument.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.INDtxtDocument.StyleController = Me.INDlcBudgetM
        Me.INDtxtDocument.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDocument, 0)
        '
        'INDsleEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntity, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntity, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntity.Name = "INDsleEntity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleEntity.Properties.NullText = ""
        Me.INDsleEntity.Properties.PopupSizeable = False
        Me.INDsleEntity.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleEntity.Properties.ShowFooter = False
        Me.INDsleEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntity, True)
        Me.INDsleEntity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleEntity.StyleController = Me.INDlcBudgetM
        Me.INDsleEntity.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntity, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntity, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
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
        'INDteDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDteDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteDate, False)
        Me.INDteDate.EditValue = Nothing
        Me.INDteDate.EnterMoveNextControl = True
        Me.INDteDate.Location = New System.Drawing.Point(438, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDteDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDteDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDteDate.Name = "INDteDate"
        Me.INDteDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDteDate.Properties.Appearance.Options.UseFont = True
        Me.INDteDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDteDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDteDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDteDate.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDteDate.Properties.Mask.BeepOnError = True
        Me.INDteDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDteDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDteDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteDate.Size = New System.Drawing.Size(386, 28)
        Me.INDteDate.StyleController = Me.INDlcBudgetM
        Me.INDteDate.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteDate, 0)
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, False)
        Me.INDbtnCode.EditValue = ""
        Me.INDbtnCode.Location = New System.Drawing.Point(438, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Budget.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlcBudgetM
        Me.INDbtnCode.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        '
        'INDpccChangeEntity
        '
        Me.INDpccChangeEntity.Controls.Add(Me.LayoutControl2)
        Me.INDpccChangeEntity.Location = New System.Drawing.Point(130, 291)
        Me.INDpccChangeEntity.Name = "INDpccChangeEntity"
        Me.INDpccChangeEntity.Size = New System.Drawing.Size(299, 150)
        Me.INDpccChangeEntity.TabIndex = 9
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDsleValidityPopUp)
        Me.LayoutControl2.Controls.Add(Me.INDsleEntityPopUp)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDsleValidityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidityPopUp, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidityPopUp, AppearanceObject6)
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
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Options.UseForeColor = True
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
        'INDsleEntityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityPopUp, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityPopUp, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.INDsleEntityPopUp.Location = New System.Drawing.Point(12, 32)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntityPopUp.Name = "INDsleEntityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.INDsleEntityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntityPopUp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntityPopUp.Properties.DisplayMember = "NameCode"
        Me.INDsleEntityPopUp.Properties.NullText = ""
        Me.INDsleEntityPopUp.Properties.PopupSizeable = False
        Me.INDsleEntityPopUp.Properties.PopupView = Me.GridView2
        Me.INDsleEntityPopUp.Properties.ShowFooter = False
        Me.INDsleEntityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityPopUp, True)
        Me.INDsleEntityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleEntityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleEntityPopUp.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityPopUp, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityPopUp, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
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
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEntityPopUp, Me.INDlciValidityPopUp})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlciEntityPopUp
        '
        Me.INDlciEntityPopUp.Control = Me.INDsleEntityPopUp
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
        Me.LayoutControlGroup1.CustomizationFormText = "Presupuesto Modificaciones"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrBudgetAmendmentsRevenue, Me.INDlcgMainData, Me.INDlyGrRubro})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1700, 522)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrBudgetAmendmentsRevenue
        '
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrBudgetAmendmentsRevenue.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrBudgetAmendmentsRevenue, False)
        Me.INDlyGrBudgetAmendmentsRevenue.CustomizationFormText = "Modificación del Presupuesto"
        Me.INDlyGrBudgetAmendmentsRevenue.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEntity, Me.INDlciValidity})
        Me.INDlyGrBudgetAmendmentsRevenue.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrBudgetAmendmentsRevenue.Name = "INDlyGrBudgetAmendmentsRevenue"
        Me.INDlyGrBudgetAmendmentsRevenue.Size = New System.Drawing.Size(414, 502)
        Me.INDlyGrBudgetAmendmentsRevenue.Text = "Modificación del Presupuesto"
        '
        'INDlyItemEntity
        '
        Me.INDlyItemEntity.Control = Me.INDsleEntity
        Me.INDlyItemEntity.CustomizationFormText = "Entidad"
        Me.INDlyItemEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemEntity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.Name = "INDlyItemEntity"
        Me.INDlyItemEntity.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEntity.Text = "Entidad Presupuestal"
        Me.INDlyItemEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEntity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEntity.TextToControlDistance = 5
        '
        'INDlciValidity
        '
        Me.INDlciValidity.Control = Me.INDsleValidity
        Me.INDlciValidity.CustomizationFormText = "Vigencia"
        Me.INDlciValidity.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.Name = "INDlciValidity"
        Me.INDlciValidity.Size = New System.Drawing.Size(390, 379)
        Me.INDlciValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidity.Text = "Vigencia"
        Me.INDlciValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciValidity.TextToControlDistance = 5
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.CustomizationFormText = "Datos Principales"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemConsecutive, Me.INDlyItemDate, Me.INDlyItemDocument, Me.INDlciObservations})
        Me.INDlcgMainData.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 502)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDlyItemConsecutive
        '
        Me.INDlyItemConsecutive.Control = Me.INDbtnCode
        Me.INDlyItemConsecutive.CustomizationFormText = "Consecutivo"
        Me.INDlyItemConsecutive.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemConsecutive.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemConsecutive.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemConsecutive.Name = "INDlyItemConsecutive"
        Me.INDlyItemConsecutive.ShowInCustomizationForm = False
        Me.INDlyItemConsecutive.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemConsecutive.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemConsecutive.Text = "Código"
        Me.INDlyItemConsecutive.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemConsecutive.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemConsecutive.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemConsecutive.TextToControlDistance = 5
        '
        'INDlyItemDate
        '
        Me.INDlyItemDate.Control = Me.INDteDate
        Me.INDlyItemDate.CustomizationFormText = "Fecha"
        Me.INDlyItemDate.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDate.Name = "INDlyItemDate"
        Me.INDlyItemDate.ShowInCustomizationForm = False
        Me.INDlyItemDate.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDate.Text = "Fecha"
        Me.INDlyItemDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDate.TextToControlDistance = 5
        '
        'INDlyItemDocument
        '
        Me.INDlyItemDocument.Control = Me.INDtxtDocument
        Me.INDlyItemDocument.CustomizationFormText = "Documento"
        Me.INDlyItemDocument.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemDocument.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDocument.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDocument.Name = "INDlyItemDocument"
        Me.INDlyItemDocument.ShowInCustomizationForm = False
        Me.INDlyItemDocument.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDocument.Text = "Documento"
        Me.INDlyItemDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDocument.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemDocument.TextToControlDistance = 5
        '
        'INDlciObservations
        '
        Me.INDlciObservations.Control = Me.INDmeObservations
        Me.INDlciObservations.CustomizationFormText = "Observación"
        Me.INDlciObservations.Location = New System.Drawing.Point(0, 192)
        Me.INDlciObservations.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlciObservations.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlciObservations.Name = "INDlciObservations"
        Me.INDlciObservations.ShowInCustomizationForm = False
        Me.INDlciObservations.Size = New System.Drawing.Size(390, 251)
        Me.INDlciObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciObservations.Text = "Observación"
        Me.INDlciObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciObservations.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciObservations.TextToControlDistance = 5
        '
        'INDlyGrRubro
        '
        Me.INDlyGrRubro.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrRubro.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrRubro.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrRubro.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrRubro.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrRubro.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrRubro.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrRubro.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrRubro.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrRubro.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrRubro.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrRubro.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrRubro.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrRubro.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrRubro, False)
        Me.INDlyGrRubro.CustomizationFormText = "Listado de Rubros"
        Me.INDlyGrRubro.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.INDlyGrRubro.Location = New System.Drawing.Point(828, 0)
        Me.INDlyGrRubro.Name = "INDlyGrRubro"
        Me.INDlyGrRubro.Size = New System.Drawing.Size(852, 502)
        Me.INDlyGrRubro.Text = "Listado de Rubros"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcDetail
        Me.LayoutControlItem1.CustomizationFormText = "Rubros"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(828, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(828, 1)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 407)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsbAddCategory
        Me.LayoutControlItem2.CustomizationFormText = "Agregar Rubro a Modificación"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmBudgetAmendmentsExpense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1157, 670)
        Me.Name = "FrmBudgetAmendmentsExpense"
        Me.Opacity = 1.0R
        Me.Tag = "221"
        Me.Text = "Presupuesto Modificaciones - Gastos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcBudgetM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcBudgetM.ResumeLayout(False)
        CType(Me.INDmeObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIceNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccChangeEntity.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrBudgetAmendmentsRevenue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemConsecutive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrRubro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcBudgetM As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDtxtDocument As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDsleEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDlyGrBudgetAmendmentsRevenue As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGrRubro As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDlyItemDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDteDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDsbAddCategory As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Private WithEvents INDlyItemConsecutive As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrepIceNature As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpccChangeEntity As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleValidityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvValidityPopUp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleEntityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciEntityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciValidityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidityPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonthPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDmeObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrepTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDgvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColResource As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTipe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColResidue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCharacter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
End Class
