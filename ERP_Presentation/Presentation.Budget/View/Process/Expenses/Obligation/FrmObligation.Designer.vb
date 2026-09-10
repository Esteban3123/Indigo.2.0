Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmObligation
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
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmObligation))
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyObligations = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnAddCommitment = New DevExpress.XtraEditors.SimpleButton()
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
        Me.INDGcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCommitmentType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDocument = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRpDteExpiredDate = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRpTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDmemoObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtDocument = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleThirdParty = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn267 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn268 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn269 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn270 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn271 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn272 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdteDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDGleDocumentType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnConsecutive = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleBudgetEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleAutomaticPaymentOrder = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo2View1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlygObligation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAutomaticPaymentOrder = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCommitment = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl2 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit2 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyObligations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyObligations.SuspendLayout()
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
        CType(Me.INDGcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRpDteExpiredDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRpDteExpiredDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRpTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleThirdParty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleDocumentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnConsecutive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAutomaticPaymentOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAutomaticPaymentOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo2View1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAutomaticPaymentOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCommitment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyObligations)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1233, 549)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1233, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1233, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyObligations
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 540)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyObligations
        '
        Me.INDlyObligations.Controls.Add(Me.INDBtnAddCommitment)
        Me.INDlyObligations.Controls.Add(Me.INDpccChangeEntity)
        Me.INDlyObligations.Controls.Add(Me.INDGcDetail)
        Me.INDlyObligations.Controls.Add(Me.INDmemoObservations)
        Me.INDlyObligations.Controls.Add(Me.INDtxtDocument)
        Me.INDlyObligations.Controls.Add(Me.INDSleThirdParty)
        Me.INDlyObligations.Controls.Add(Me.INDdteDate)
        Me.INDlyObligations.Controls.Add(Me.INDGleDocumentType)
        Me.INDlyObligations.Controls.Add(Me.INDbtnConsecutive)
        Me.INDlyObligations.Controls.Add(Me.INDsleValidity)
        Me.INDlyObligations.Controls.Add(Me.INDsleBudgetEntity)
        Me.INDlyObligations.Controls.Add(Me.INDGleAutomaticPaymentOrder)
        Me.INDlyObligations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyObligations.Location = New System.Drawing.Point(202, 7)
        Me.INDlyObligations.Name = "INDlyObligations"
        Me.INDlyObligations.Root = Me.INDlygObligation
        Me.INDlyObligations.Size = New System.Drawing.Size(1029, 540)
        Me.INDlyObligations.TabIndex = 1
        Me.INDlyObligations.Text = "LayoutControl1"
        '
        'INDBtnAddCommitment
        '
        Me.INDBtnAddCommitment.Location = New System.Drawing.Point(852, 59)
        Me.INDBtnAddCommitment.Name = "INDBtnAddCommitment"
        Me.INDBtnAddCommitment.Size = New System.Drawing.Size(896, 32)
        Me.INDBtnAddCommitment.StyleController = Me.INDlyObligations
        Me.INDBtnAddCommitment.TabIndex = 29
        Me.INDBtnAddCommitment.Text = "Agregar"
        '
        'INDpccChangeEntity
        '
        Me.INDpccChangeEntity.Controls.Add(Me.LayoutControl2)
        Me.INDpccChangeEntity.Location = New System.Drawing.Point(85, 312)
        Me.INDpccChangeEntity.Name = "INDpccChangeEntity"
        Me.INDpccChangeEntity.Size = New System.Drawing.Size(299, 150)
        Me.INDpccChangeEntity.TabIndex = 27
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
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDsleValidityPopUp, AppearanceObject1)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDsleValidityPopUp, AppearanceObject2)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.INDsleValidityPopUp.Location = New System.Drawing.Point(12, 96)
        Me.IndigoTextEdit2.SetMascara(Me.INDsleValidityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidityPopUp.Name = "INDsleValidityPopUp"
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDsleValidityPopUp, False)
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
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDsleValidityPopUp, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDsleValidityPopUp, True)
        Me.INDsleValidityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleValidityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleValidityPopUp.TabIndex = 5
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDsleValidityPopUp, Nothing)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDsleValidityPopUp, 0)
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDsleValidityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDsleValidityPopUp, False)
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
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDsleBudgetEntityPopUp, AppearanceObject3)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDsleBudgetEntityPopUp, AppearanceObject4)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.INDsleBudgetEntityPopUp.Location = New System.Drawing.Point(12, 32)
        Me.IndigoTextEdit2.SetMascara(Me.INDsleBudgetEntityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBudgetEntityPopUp.Name = "INDsleBudgetEntityPopUp"
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDsleBudgetEntityPopUp, True)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
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
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDsleBudgetEntityPopUp, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDsleBudgetEntityPopUp, True)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDsleBudgetEntityPopUp, True)
        Me.INDsleBudgetEntityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleBudgetEntityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleBudgetEntityPopUp.TabIndex = 4
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDsleBudgetEntityPopUp, "200")
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDsleBudgetEntityPopUp, 0)
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDsleBudgetEntityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDsleBudgetEntityPopUp, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDsleBudgetEntityPopUp, False)
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
        'INDGcDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDetail, False)
        Me.INDGcDetail.Location = New System.Drawing.Point(852, 95)
        Me.INDGcDetail.MainView = Me.INDGvDetail
        Me.INDGcDetail.Name = "INDGcDetail"
        Me.INDGcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRpTxtValue, Me.INDRpDteExpiredDate, Me.RepositoryItemImageComboBox1})
        Me.INDGcDetail.Size = New System.Drawing.Size(896, 424)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcDetail.TabIndex = 28
        Me.INDGcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDetail})
        '
        'INDGvDetail
        '
        Me.INDGvDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCommitmentType, Me.INDColCode, Me.INDColDocument, Me.GridColumn12, Me.GridColumn17, Me.GridColumn19, Me.GridColumn20, Me.GridColumn9})
        Me.INDGvDetail.GridControl = Me.INDGcDetail
        Me.INDGvDetail.GroupFormat = "{1} {2}"
        Me.INDGvDetail.Name = "INDGvDetail"
        Me.INDGvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDetail.OptionsView.ShowDetailButtons = False
        Me.INDGvDetail.OptionsView.ShowFooter = True
        Me.INDGvDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDetail, False)
        '
        'INDColCommitmentType
        '
        Me.INDColCommitmentType.Caption = "Tipo de Compromiso"
        Me.INDColCommitmentType.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.INDColCommitmentType.FieldName = "CommitmentType"
        Me.INDColCommitmentType.Name = "INDColCommitmentType"
        Me.INDColCommitmentType.Visible = True
        Me.INDColCommitmentType.VisibleIndex = 0
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Compromiso", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Reserva", 2, -1)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Código"
        Me.INDColCode.FieldName = "CommitmentCode"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 1
        '
        'INDColDocument
        '
        Me.INDColDocument.Caption = "Documento"
        Me.INDColDocument.FieldName = "CommitmentDocument"
        Me.INDColDocument.Name = "INDColDocument"
        Me.INDColDocument.OptionsColumn.AllowEdit = False
        Me.INDColDocument.OptionsColumn.AllowFocus = False
        Me.INDColDocument.Visible = True
        Me.INDColDocument.VisibleIndex = 2
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Nombre"
        Me.GridColumn12.FieldName = "CodeNameBudget"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 3
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Recurso"
        Me.GridColumn17.FieldName = "CodeNameFinancialSource"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 4
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Saldo"
        Me.GridColumn19.DisplayFormat.FormatString = "c0"
        Me.GridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn19.FieldName = "BalanceCommitment"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 5
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Vencimiento"
        Me.GridColumn20.ColumnEdit = Me.INDRpDteExpiredDate
        Me.GridColumn20.FieldName = "ExpiredDate"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 6
        '
        'INDRpDteExpiredDate
        '
        Me.INDRpDteExpiredDate.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDRpDteExpiredDate.AutoHeight = False
        Me.INDRpDteExpiredDate.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRpDteExpiredDate.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRpDteExpiredDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRpDteExpiredDate.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat = True
        Me.INDRpDteExpiredDate.Mask.EditMask = "dd/MM/yyyy hh:mm:ss tt"
        Me.INDRpDteExpiredDate.Mask.UseMaskAsDisplayFormat = True
        Me.INDRpDteExpiredDate.Name = "INDRpDteExpiredDate"
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Valor"
        Me.GridColumn9.ColumnEdit = Me.INDRpTxtValue
        Me.GridColumn9.FieldName = "InitialValue"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InitialValue", "{0:c0}")})
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 7
        '
        'INDRpTxtValue
        '
        Me.INDRpTxtValue.AutoHeight = False
        Me.INDRpTxtValue.Mask.EditMask = "c0"
        Me.INDRpTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDRpTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDRpTxtValue.Name = "INDRpTxtValue"
        '
        'INDmemoObservations
        '
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDmemoObservations, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDmemoObservations, True)
        Me.INDmemoObservations.EnterMoveNextControl = True
        Me.INDmemoObservations.Location = New System.Drawing.Point(438, 449)
        Me.IndigoTextEdit2.SetMascara(Me.INDmemoObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDmemoObservations.Properties.MaxLength = 5000
        Me.INDmemoObservations.Size = New System.Drawing.Size(386, 70)
        Me.INDmemoObservations.StyleController = Me.INDlyObligations
        Me.INDmemoObservations.TabIndex = 26
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDmemoObservations, 0)
        Me.INDmemoObservations.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtDocument
        '
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDtxtDocument, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDtxtDocument, True)
        Me.INDtxtDocument.EnterMoveNextControl = True
        Me.INDtxtDocument.Location = New System.Drawing.Point(438, 325)
        Me.IndigoTextEdit2.SetMascara(Me.INDtxtDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDtxtDocument.StyleController = Me.INDlyObligations
        Me.INDtxtDocument.TabIndex = 25
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDtxtDocument, 0)
        Me.INDtxtDocument.ToolTip = "Este Campo es Necesario"
        '
        'INDSleThirdParty
        '
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDSleThirdParty, AppearanceObject5)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDSleThirdParty, AppearanceObject6)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDSleThirdParty, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDSleThirdParty, True)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDSleThirdParty, False)
        Me.INDSleThirdParty.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDSleThirdParty, False)
        Me.INDSleThirdParty.Location = New System.Drawing.Point(438, 265)
        Me.IndigoTextEdit2.SetMascara(Me.INDSleThirdParty, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleThirdParty.Name = "INDSleThirdParty"
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDSleThirdParty, True)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDSleThirdParty, False)
        Me.INDSleThirdParty.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleThirdParty.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleThirdParty.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleThirdParty.Properties.Appearance.Options.UseFont = True
        Me.INDSleThirdParty.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleThirdParty.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleThirdParty.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleThirdParty.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleThirdParty.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleThirdParty.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleThirdParty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleThirdParty.Properties.DisplayMember = "NitName"
        Me.INDSleThirdParty.Properties.NullText = ""
        Me.INDSleThirdParty.Properties.PopupFormMinSize = New System.Drawing.Size(900, 0)
        Me.INDSleThirdParty.Properties.PopupSizeable = False
        Me.INDSleThirdParty.Properties.PopupView = Me.SearchLookUpEdit3View
        Me.INDSleThirdParty.Properties.ShowClearButton = False
        Me.INDSleThirdParty.Properties.ShowFooter = False
        Me.INDSleThirdParty.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDSleThirdParty, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDSleThirdParty, True)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDSleThirdParty, True)
        Me.INDSleThirdParty.Size = New System.Drawing.Size(386, 28)
        Me.INDSleThirdParty.StyleController = Me.INDlyObligations
        Me.INDSleThirdParty.TabIndex = 24
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDSleThirdParty, "532")
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDSleThirdParty, 0)
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDSleThirdParty, "{0} - {1}")
        Me.INDSleThirdParty.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDSleThirdParty, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDSleThirdParty, False)
        '
        'SearchLookUpEdit3View
        '
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit3View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit3View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit3View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit3View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit3View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit3View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn267, Me.GridColumn268, Me.GridColumn269, Me.GridColumn270, Me.GridColumn271, Me.GridColumn272})
        Me.SearchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit3View.Name = "SearchLookUpEdit3View"
        Me.SearchLookUpEdit3View.OptionsFind.FindFilterColumns = "Nit"
        Me.SearchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit3View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit3View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit3View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit3View, False)
        '
        'GridColumn267
        '
        Me.GridColumn267.Caption = "Nit"
        Me.GridColumn267.FieldName = "Nit"
        Me.GridColumn267.Name = "GridColumn267"
        Me.GridColumn267.Visible = True
        Me.GridColumn267.VisibleIndex = 0
        Me.GridColumn267.Width = 167
        '
        'GridColumn268
        '
        Me.GridColumn268.Caption = "Tipo Documento"
        Me.GridColumn268.FieldName = "PersonId.IdentificationTypeName"
        Me.GridColumn268.Name = "GridColumn268"
        Me.GridColumn268.Visible = True
        Me.GridColumn268.VisibleIndex = 1
        Me.GridColumn268.Width = 240
        '
        'GridColumn269
        '
        Me.GridColumn269.Caption = "Nombre Completo"
        Me.GridColumn269.FieldName = "Name"
        Me.GridColumn269.Name = "GridColumn269"
        Me.GridColumn269.Visible = True
        Me.GridColumn269.VisibleIndex = 2
        Me.GridColumn269.Width = 383
        '
        'GridColumn270
        '
        Me.GridColumn270.Caption = "Ciudad"
        Me.GridColumn270.FieldName = "PersonId.IdentificacionCityId.Descripcion"
        Me.GridColumn270.Name = "GridColumn270"
        Me.GridColumn270.Visible = True
        Me.GridColumn270.VisibleIndex = 3
        Me.GridColumn270.Width = 187
        '
        'GridColumn271
        '
        Me.GridColumn271.Caption = "Tipo Ret."
        Me.GridColumn271.FieldName = "RetentionTypeName"
        Me.GridColumn271.Name = "GridColumn271"
        Me.GridColumn271.Visible = True
        Me.GridColumn271.VisibleIndex = 4
        Me.GridColumn271.Width = 203
        '
        'GridColumn272
        '
        Me.GridColumn272.Caption = "Tipo Contr"
        Me.GridColumn272.FieldName = "ContributionTypeName"
        Me.GridColumn272.Name = "GridColumn272"
        Me.GridColumn272.Visible = True
        Me.GridColumn272.VisibleIndex = 5
        Me.GridColumn272.Width = 234
        '
        'INDdteDate
        '
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDdteDate, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDdteDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteDate, True)
        Me.INDdteDate.EditValue = Nothing
        Me.INDdteDate.EnterMoveNextControl = True
        Me.INDdteDate.Location = New System.Drawing.Point(438, 205)
        Me.IndigoTextEdit2.SetMascara(Me.INDdteDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteDate, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
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
        Me.INDdteDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDdteDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteDate.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDdteDate.Properties.Mask.BeepOnError = True
        Me.INDdteDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDdteDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteDate.StyleController = Me.INDlyObligations
        Me.INDdteDate.TabIndex = 23
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDdteDate, 0)
        Me.INDdteDate.ToolTip = "Este Campo es Necesario"
        '
        'INDGleDocumentType
        '
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDGleDocumentType, True)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDGleDocumentType, True)
        Me.INDGleDocumentType.EnterMoveNextControl = True
        Me.INDGleDocumentType.Location = New System.Drawing.Point(438, 145)
        Me.IndigoTextEdit2.SetMascara(Me.INDGleDocumentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleDocumentType.Name = "INDGleDocumentType"
        Me.INDGleDocumentType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleDocumentType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleDocumentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleDocumentType.Properties.Appearance.Options.UseFont = True
        Me.INDGleDocumentType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleDocumentType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleDocumentType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleDocumentType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleDocumentType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleDocumentType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleDocumentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleDocumentType.Properties.DisplayMember = "Item2"
        Me.INDGleDocumentType.Properties.NullText = ""
        Me.INDGleDocumentType.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleDocumentType.Properties.ValueMember = "Item1"
        Me.INDGleDocumentType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleDocumentType.StyleController = Me.INDlyObligations
        Me.INDGleDocumentType.TabIndex = 22
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDGleDocumentType, 0)
        Me.INDGleDocumentType.ToolTip = "Este Campo es Necesario"
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
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
        Me.GridColumn1.Caption = "Tipo de Obligación"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDbtnConsecutive
        '
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDbtnConsecutive, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDbtnConsecutive, True)
        Me.INDbtnConsecutive.EditValue = ""
        Me.INDbtnConsecutive.Location = New System.Drawing.Point(438, 85)
        Me.IndigoTextEdit2.SetMascara(Me.INDbtnConsecutive, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDbtnConsecutive.StyleController = Me.INDlyObligations
        Me.INDbtnConsecutive.TabIndex = 21
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDbtnConsecutive, 0)
        Me.INDbtnConsecutive.ToolTip = "Este Campo es Necesario"
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject7)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject8)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDsleValidity, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit2.SetMascara(Me.INDsleValidity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidity.Name = "INDsleValidity"
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDsleValidity, False)
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
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDsleValidity, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDsleValidity, True)
        Me.INDsleValidity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleValidity.StyleController = Me.INDlyObligations
        Me.INDsleValidity.TabIndex = 20
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDsleValidity, Nothing)
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDsleValidity, 0)
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDsleValidity, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDsleValidity, False)
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
        Me.IndigoSearchLookUpControl2.SetAppearanceEmbeddedNavigator(Me.INDsleBudgetEntity, AppearanceObject9)
        Me.IndigoSearchLookUpControl2.SetAppearanceTextFindControl(Me.INDsleBudgetEntity, AppearanceObject10)
        Me.IndigoSearchLookUpControl2.SetAppendButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDsleBudgetEntity, True)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetCancelEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetEndEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl2.SetExportButton(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetFirstButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetLastButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit2.SetMascara(Me.INDsleBudgetEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBudgetEntity.Name = "INDsleBudgetEntity"
        Me.IndigoSearchLookUpControl2.SetNextButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetNextPageButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetOpenForm(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl2.SetPrevButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetPrevPageButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleBudgetEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
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
        Me.INDsleBudgetEntity.Properties.PopupView = Me.SearchLookUpEdit2View
        Me.INDsleBudgetEntity.Properties.ShowFooter = False
        Me.INDsleBudgetEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl2.SetRemoveButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetSaveXmlGrid(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl2.SetShowDeleteButton(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl2.SetShowFindButton(Me.INDsleBudgetEntity, True)
        Me.INDsleBudgetEntity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleBudgetEntity.StyleController = Me.INDlyObligations
        Me.INDsleBudgetEntity.TabIndex = 19
        Me.IndigoSearchLookUpControl2.SetTagForm(Me.INDsleBudgetEntity, "200")
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDsleBudgetEntity, 0)
        Me.IndigoSearchLookUpControl2.SetTextStringFormat(Me.INDsleBudgetEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl2.SetTxtFindEnterEnabled(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl2.SetUseEmbeddedNavigator(Me.INDsleBudgetEntity, False)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn15, Me.GridColumn16})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEdit2View.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEdit2View.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Código"
        Me.GridColumn15.FieldName = "Code"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 0
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Nombre"
        Me.GridColumn16.FieldName = "Name"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 1
        '
        'INDGleAutomaticPaymentOrder
        '
        Me.IndigoTextEdit2.SetApplyStyle(Me.INDGleAutomaticPaymentOrder, False)
        Me.IndigoTextEdit2.SetCampoObligatorio(Me.INDGleAutomaticPaymentOrder, True)
        Me.INDGleAutomaticPaymentOrder.EnterMoveNextControl = True
        Me.INDGleAutomaticPaymentOrder.Location = New System.Drawing.Point(438, 385)
        Me.IndigoTextEdit2.SetMascara(Me.INDGleAutomaticPaymentOrder, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleAutomaticPaymentOrder.Name = "INDGleAutomaticPaymentOrder"
        Me.INDGleAutomaticPaymentOrder.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleAutomaticPaymentOrder.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleAutomaticPaymentOrder.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleAutomaticPaymentOrder.Properties.Appearance.Options.UseFont = True
        Me.INDGleAutomaticPaymentOrder.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleAutomaticPaymentOrder.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleAutomaticPaymentOrder.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleAutomaticPaymentOrder.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleAutomaticPaymentOrder.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleAutomaticPaymentOrder.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleAutomaticPaymentOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleAutomaticPaymentOrder.Properties.DataSource = CType(resources.GetObject("INDGleAutomaticPaymentOrder.Properties.DataSource"), Object)
        Me.INDGleAutomaticPaymentOrder.Properties.DisplayMember = "Item2"
        Me.INDGleAutomaticPaymentOrder.Properties.ImmediatePopup = True
        Me.INDGleAutomaticPaymentOrder.Properties.NullText = ""
        Me.INDGleAutomaticPaymentOrder.Properties.PopupView = Me.CtrYesNo2View1
        Me.INDGleAutomaticPaymentOrder.Properties.ValueMember = "Item1"
        Me.INDGleAutomaticPaymentOrder.Size = New System.Drawing.Size(386, 28)
        Me.INDGleAutomaticPaymentOrder.StyleController = Me.INDlyObligations
        Me.INDGleAutomaticPaymentOrder.TabIndex = 17
        Me.IndigoTextEdit2.SetTamañoMinimoString(Me.INDGleAutomaticPaymentOrder, 0)
        Me.INDGleAutomaticPaymentOrder.ToolTip = "Este Campo es Necesario"
        '
        'CtrYesNo2View1
        '
        Me.CtrYesNo2View1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CtrYesNo2View1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo2View1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CtrYesNo2View1.Appearance.FocusedRow.Options.UseFont = True
        Me.CtrYesNo2View1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo2View1.Appearance.GroupRow.Options.UseFont = True
        Me.CtrYesNo2View1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo2View1.Appearance.HeaderPanel.Options.UseFont = True
        Me.CtrYesNo2View1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.CtrYesNo2View1.Appearance.Row.Options.UseFont = True
        Me.CtrYesNo2View1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn18})
        Me.CtrYesNo2View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo2View1.Name = "CtrYesNo2View1"
        Me.CtrYesNo2View1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo2View1.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo2View1.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo2View1.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo2View1.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo2View1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CtrYesNo2View1, False)
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.Caption = "Selección"
        Me.GridColumn23.FieldName = "Item2"
        Me.GridColumn23.Name = "GridColumn23"
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn18.Caption = "Selección"
        Me.GridColumn18.FieldName = "Item2"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 0
        '
        'INDlygObligation
        '
        Me.INDlygObligation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygObligation.AppearanceGroup.Options.UseFont = True
        Me.INDlygObligation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygObligation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygObligation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygObligation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygObligation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygObligation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygObligation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygObligation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygObligation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygObligation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygObligation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygObligation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygObligation, False)
        Me.INDlygObligation.CustomizationFormText = "INDlygObligation"
        Me.INDlygObligation.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlygObligation.GroupBordersVisible = False
        Me.INDlygObligation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygGeneralInformation, Me.INDLcgCommitment})
        Me.INDlygObligation.Name = "INDlygObligation"
        Me.INDlygObligation.Size = New System.Drawing.Size(1772, 543)
        Me.INDlygObligation.TextVisible = False
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
        Me.INDlygPrincipalData.Size = New System.Drawing.Size(414, 523)
        Me.INDlygPrincipalData.Text = "Datos Principales"
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
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 404)
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
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.INDLciAutomaticPaymentOrder})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(414, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(414, 523)
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
        Me.LayoutControlItem4.Control = Me.INDGleDocumentType
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.ShowInCustomizationForm = False
        Me.LayoutControlItem4.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Tipo de Documento"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDdteDate
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.ShowInCustomizationForm = False
        Me.LayoutControlItem5.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Fecha"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDSleThirdParty
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.ShowInCustomizationForm = False
        Me.LayoutControlItem6.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "Tercero"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem6.TextToControlDistance = 5
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDtxtDocument
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 240)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.ShowInCustomizationForm = False
        Me.LayoutControlItem7.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "Documento"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem7.TextToControlDistance = 5
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDmemoObservations
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 364)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(390, 100)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(390, 100)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.ShowInCustomizationForm = False
        Me.LayoutControlItem8.Size = New System.Drawing.Size(390, 100)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Observaciones"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem8.TextToControlDistance = 5
        '
        'INDLciAutomaticPaymentOrder
        '
        Me.INDLciAutomaticPaymentOrder.Control = Me.INDGleAutomaticPaymentOrder
        Me.INDLciAutomaticPaymentOrder.CustomizationFormText = "Orden de Pago Automática"
        Me.INDLciAutomaticPaymentOrder.Location = New System.Drawing.Point(0, 300)
        Me.INDLciAutomaticPaymentOrder.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticPaymentOrder.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticPaymentOrder.Name = "INDLciAutomaticPaymentOrder"
        Me.INDLciAutomaticPaymentOrder.Size = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticPaymentOrder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAutomaticPaymentOrder.Text = "Orden de Pago Automática"
        Me.INDLciAutomaticPaymentOrder.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAutomaticPaymentOrder.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAutomaticPaymentOrder.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAutomaticPaymentOrder.TextToControlDistance = 5
        '
        'INDLcgCommitment
        '
        Me.INDLcgCommitment.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCommitment.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCommitment.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCommitment.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCommitment.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCommitment.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCommitment.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCommitment.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCommitment.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCommitment.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCommitment.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCommitment.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCommitment.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCommitment.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCommitment, False)
        Me.INDLcgCommitment.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem9, Me.LayoutControlItem10})
        Me.INDLcgCommitment.Location = New System.Drawing.Point(828, 0)
        Me.INDLcgCommitment.Name = "INDLcgCommitment"
        Me.INDLcgCommitment.Size = New System.Drawing.Size(924, 523)
        Me.INDLcgCommitment.Text = "Listado de Compromisos"
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDGcDetail
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(900, 0)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(900, 1)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(900, 428)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.INDBtnAddCommitment
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(900, 36)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(900, 36)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(900, 36)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'GridColumn21
        '
        Me.GridColumn21.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn21.Caption = "Selección"
        Me.GridColumn21.FieldName = "Item2"
        Me.GridColumn21.Name = "GridColumn21"
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn10.Caption = "Selección"
        Me.GridColumn10.FieldName = "Item2"
        Me.GridColumn10.Name = "GridColumn10"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.Caption = "Selección"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.Caption = "Selección"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'GridColumn22
        '
        Me.GridColumn22.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn22.Caption = "Selección"
        Me.GridColumn22.FieldName = "Item2"
        Me.GridColumn22.Name = "GridColumn22"
        '
        'FrmObligation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1233, 671)
        Me.Name = "FrmObligation"
        Me.Opacity = 1.0R
        Me.Tag = "235"
        Me.Text = "Obligaciones"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyObligations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyObligations.ResumeLayout(False)
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
        CType(Me.INDGcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRpDteExpiredDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRpDteExpiredDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRpTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleThirdParty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleDocumentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnConsecutive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAutomaticPaymentOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAutomaticPaymentOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo2View1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAutomaticPaymentOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCommitment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyObligations As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlygObligation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl2 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit2 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDsleBudgetEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleDocumentType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDbtnConsecutive As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleThirdParty As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn267 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn268 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn269 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn270 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn271 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn272 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDocument As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgCommitment As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnAddCommitment As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDColDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRpTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRpDteExpiredDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents INDColCommitmentType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDGleAutomaticPaymentOrder As CtrYesNo
    Friend WithEvents CtrYesNo2View1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciAutomaticPaymentOrder As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
End Class
