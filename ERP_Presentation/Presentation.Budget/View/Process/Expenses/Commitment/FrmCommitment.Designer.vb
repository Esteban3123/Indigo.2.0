Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCommitment
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
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject11 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject12 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCommitment))
        Me.INDValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValueCategory = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcCommitment = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcCategory = New DevExpress.XtraGrid.GridControl()
        Me.viewBudget = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResource = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateExpired = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtDateCategory = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.INDgcDetailAvailability = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColAvailability = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColResource = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTipe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolBalanceAvailability = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCharacter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtDate = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.INDColValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDsbAddCategory = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtDocument = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleDocumentSource = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleThirdParty = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdeDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDpccChangeEntity = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleValidityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvValidityPopUp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidityPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonthPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcPACControlPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleEntityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciEntityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDsleCommitmentType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcIncomeMonth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcPACControl = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleAutomaticObligation = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleAutomaticPaymentOrder = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo2View1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgCommitment = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCommitmentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciThirdParty = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDocumentSource = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAutomaticObligation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAutomaticPaymentOrder = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciGcAvailability = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciGcBudget = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn411 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValueCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcCommitment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcCommitment.SuspendLayout()
        CType(Me.INDGcCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtDateCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtDateCategory.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetailAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleDocumentSource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.INDsleCommitmentType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAutomaticObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAutomaticObligation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAutomaticPaymentOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAutomaticPaymentOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo2View1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgCommitment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCommitmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDocumentSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAutomaticObligation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAutomaticPaymentOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciGcAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciGcBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcCommitment)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'INDValue
        '
        Me.INDValue.Caption = "Valor"
        Me.INDValue.ColumnEdit = Me.INDrepTxtValueCategory
        Me.INDValue.DisplayFormat.FormatString = "C0"
        Me.INDValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDValue.FieldName = "InitialValue"
        Me.INDValue.Name = "INDValue"
        Me.INDValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InitialValue", "{0:c0}")})
        Me.INDValue.Visible = True
        Me.INDValue.VisibleIndex = 4
        '
        'INDrepTxtValueCategory
        '
        Me.INDrepTxtValueCategory.AutoHeight = False
        Me.INDrepTxtValueCategory.Mask.EditMask = "c0"
        Me.INDrepTxtValueCategory.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValueCategory.Name = "INDrepTxtValueCategory"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcCommitment
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 598)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcCommitment
        '
        Me.INDlcCommitment.Controls.Add(Me.INDGcCategory)
        Me.INDlcCommitment.Controls.Add(Me.INDgcDetailAvailability)
        Me.INDlcCommitment.Controls.Add(Me.INDsbAddCategory)
        Me.INDlcCommitment.Controls.Add(Me.INDmeObservations)
        Me.INDlcCommitment.Controls.Add(Me.INDtxtDocument)
        Me.INDlcCommitment.Controls.Add(Me.INDsleDocumentSource)
        Me.INDlcCommitment.Controls.Add(Me.INDsleThirdParty)
        Me.INDlcCommitment.Controls.Add(Me.INDdeDocumentDate)
        Me.INDlcCommitment.Controls.Add(Me.INDpccChangeEntity)
        Me.INDlcCommitment.Controls.Add(Me.INDsleCommitmentType)
        Me.INDlcCommitment.Controls.Add(Me.INDbtnCode)
        Me.INDlcCommitment.Controls.Add(Me.INDsleValidity)
        Me.INDlcCommitment.Controls.Add(Me.INDsleEntity)
        Me.INDlcCommitment.Controls.Add(Me.INDGleAutomaticObligation)
        Me.INDlcCommitment.Controls.Add(Me.INDGleAutomaticPaymentOrder)
        Me.INDlcCommitment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcCommitment.Location = New System.Drawing.Point(202, 7)
        Me.INDlcCommitment.Name = "INDlcCommitment"
        Me.INDlcCommitment.Root = Me.LayoutControlGroup1
        Me.INDlcCommitment.Size = New System.Drawing.Size(804, 598)
        Me.INDlcCommitment.TabIndex = 1
        Me.INDlcCommitment.Text = "LayoutControl1"
        '
        'INDGcCategory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCategory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCategory, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCategory, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCategory, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCategory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCategory, False)
        Me.INDGcCategory.Location = New System.Drawing.Point(1266, 299)
        Me.INDGcCategory.MainView = Me.viewBudget
        Me.INDGcCategory.Name = "INDGcCategory"
        Me.INDGcCategory.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtDateCategory, Me.INDrepTxtValueCategory})
        Me.INDGcCategory.Size = New System.Drawing.Size(820, 258)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCategory, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcCategory.TabIndex = 17
        Me.INDGcCategory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewBudget})
        '
        'viewBudget
        '
        Me.viewBudget.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewBudget.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewBudget.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewBudget.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewBudget.Appearance.FocusedRow.Options.UseFont = True
        Me.viewBudget.Appearance.FocusedRow.Options.UseForeColor = True
        Me.viewBudget.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewBudget.Appearance.GroupRow.Options.UseFont = True
        Me.viewBudget.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewBudget.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewBudget.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewBudget.Appearance.Row.Options.UseFont = True
        Me.viewBudget.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewBudget.Appearance.ViewCaption.Options.UseFont = True
        Me.viewBudget.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCategory, Me.INDResource, Me.INDType, Me.INDDateExpired, Me.INDValue})
        GridFormatRule1.Column = Me.INDValue
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.MistyRose
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Expression
        FormatConditionRuleValue1.Expression = "[ValueBalance] < [InitialValue] Or [InitialValue] = 0 Or [InitialValue] < 0"
        GridFormatRule1.Rule = FormatConditionRuleValue1
        Me.viewBudget.FormatRules.Add(GridFormatRule1)
        Me.viewBudget.GridControl = Me.INDGcCategory
        Me.viewBudget.Name = "viewBudget"
        Me.viewBudget.OptionsView.EnableAppearanceEvenRow = True
        Me.viewBudget.OptionsView.EnableAppearanceOddRow = True
        Me.viewBudget.OptionsView.ShowAutoFilterRow = True
        Me.viewBudget.OptionsView.ShowDetailButtons = False
        Me.viewBudget.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewBudget, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.viewBudget, False)
        '
        'INDCategory
        '
        Me.INDCategory.Caption = "Rubro"
        Me.INDCategory.FieldName = "CodeNameCategory"
        Me.INDCategory.Name = "INDCategory"
        Me.INDCategory.OptionsColumn.AllowEdit = False
        Me.INDCategory.OptionsColumn.AllowFocus = False
        Me.INDCategory.Visible = True
        Me.INDCategory.VisibleIndex = 0
        '
        'INDResource
        '
        Me.INDResource.Caption = "Recurso"
        Me.INDResource.FieldName = "CodeNameFinancialSource"
        Me.INDResource.Name = "INDResource"
        Me.INDResource.OptionsColumn.AllowEdit = False
        Me.INDResource.OptionsColumn.AllowFocus = False
        Me.INDResource.Visible = True
        Me.INDResource.VisibleIndex = 1
        '
        'INDType
        '
        Me.INDType.Caption = "Tipo"
        Me.INDType.FieldName = "CodeNameRevenueType"
        Me.INDType.Name = "INDType"
        Me.INDType.OptionsColumn.AllowEdit = False
        Me.INDType.OptionsColumn.AllowFocus = False
        Me.INDType.Visible = True
        Me.INDType.VisibleIndex = 2
        '
        'INDDateExpired
        '
        Me.INDDateExpired.Caption = "Vencimiento"
        Me.INDDateExpired.ColumnEdit = Me.INDrepTxtDateCategory
        Me.INDDateExpired.FieldName = "ExpiredDate"
        Me.INDDateExpired.Name = "INDDateExpired"
        Me.INDDateExpired.OptionsColumn.AllowEdit = False
        Me.INDDateExpired.OptionsColumn.AllowFocus = False
        Me.INDDateExpired.Visible = True
        Me.INDDateExpired.VisibleIndex = 3
        '
        'INDrepTxtDateCategory
        '
        Me.INDrepTxtDateCategory.AutoHeight = False
        Me.INDrepTxtDateCategory.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepTxtDateCategory.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepTxtDateCategory.Name = "INDrepTxtDateCategory"
        '
        'INDgcDetailAvailability
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetailAvailability, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetailAvailability, Nothing)
        Me.INDgcDetailAvailability.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcDetailAvailability.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.INDgcDetailAvailability.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDgcDetailAvailability.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetailAvailability, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetailAvailability, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetailAvailability, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetailAvailability, False)
        Me.INDgcDetailAvailability.Location = New System.Drawing.Point(1266, 95)
        Me.INDgcDetailAvailability.MainView = Me.INDgvDetail
        Me.INDgcDetailAvailability.Name = "INDgcDetailAvailability"
        Me.INDgcDetailAvailability.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtValue, Me.INDrepTxtDate})
        Me.INDgcDetailAvailability.Size = New System.Drawing.Size(820, 200)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetailAvailability, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcDetailAvailability.TabIndex = 11
        Me.INDgcDetailAvailability.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetail})
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
        Me.INDgvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColAvailability, Me.INDColCode, Me.INDColResource, Me.INDColTipe, Me.INDcolBalanceAvailability, Me.INDColCharacter, Me.INDColValue})
        Me.INDgvDetail.GridControl = Me.INDgcDetailAvailability
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvDetail, False)
        Me.INDgvDetail.ViewCaption = "Identificación del Rubro"
        '
        'INDColAvailability
        '
        Me.INDColAvailability.Caption = "No. Disponibilidad"
        Me.INDColAvailability.FieldName = "CodeAvailability"
        Me.INDColAvailability.Name = "INDColAvailability"
        Me.INDColAvailability.OptionsColumn.AllowEdit = False
        Me.INDColAvailability.OptionsColumn.AllowFocus = False
        Me.INDColAvailability.Visible = True
        Me.INDColAvailability.VisibleIndex = 0
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Rubro"
        Me.INDColCode.FieldName = "CodeNameCategory"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 1
        Me.INDColCode.Width = 77
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
        Me.INDColResource.Width = 74
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
        Me.INDColTipe.Width = 77
        '
        'INDcolBalanceAvailability
        '
        Me.INDcolBalanceAvailability.Caption = "Saldo Dis."
        Me.INDcolBalanceAvailability.DisplayFormat.FormatString = "c0"
        Me.INDcolBalanceAvailability.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDcolBalanceAvailability.FieldName = "BalanceAffects"
        Me.INDcolBalanceAvailability.Name = "INDcolBalanceAvailability"
        Me.INDcolBalanceAvailability.OptionsColumn.AllowEdit = False
        Me.INDcolBalanceAvailability.OptionsColumn.AllowFocus = False
        Me.INDcolBalanceAvailability.Visible = True
        Me.INDcolBalanceAvailability.VisibleIndex = 4
        Me.INDcolBalanceAvailability.Width = 92
        '
        'INDColCharacter
        '
        Me.INDColCharacter.Caption = "Vencimiento"
        Me.INDColCharacter.ColumnEdit = Me.INDrepTxtDate
        Me.INDColCharacter.FieldName = "ExpiredDate"
        Me.INDColCharacter.Name = "INDColCharacter"
        Me.INDColCharacter.Visible = True
        Me.INDColCharacter.VisibleIndex = 5
        Me.INDColCharacter.Width = 102
        '
        'INDrepTxtDate
        '
        Me.INDrepTxtDate.AutoHeight = False
        Me.INDrepTxtDate.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepTxtDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepTxtDate.Name = "INDrepTxtDate"
        '
        'INDColValue
        '
        Me.INDColValue.Caption = "Valor"
        Me.INDColValue.ColumnEdit = Me.INDrepTxtValue
        Me.INDColValue.DisplayFormat.FormatString = "c0"
        Me.INDColValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColValue.FieldName = "InitialValue"
        Me.INDColValue.Name = "INDColValue"
        Me.INDColValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InitialValue", "{0:c0}")})
        Me.INDColValue.Visible = True
        Me.INDColValue.VisibleIndex = 6
        Me.INDColValue.Width = 162
        '
        'INDrepTxtValue
        '
        Me.INDrepTxtValue.AutoHeight = False
        Me.INDrepTxtValue.Mask.EditMask = "c0"
        Me.INDrepTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValue.Name = "INDrepTxtValue"
        '
        'INDsbAddCategory
        '
        Me.INDsbAddCategory.Location = New System.Drawing.Point(1266, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAddCategory, False)
        Me.INDsbAddCategory.Name = "INDsbAddCategory"
        Me.INDsbAddCategory.Size = New System.Drawing.Size(824, 32)
        Me.INDsbAddCategory.StyleController = Me.INDlcCommitment
        Me.INDsbAddCategory.TabIndex = 10
        Me.INDsbAddCategory.Text = "Agregar"
        '
        'INDmeObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeObservations, False)
        Me.INDmeObservations.EnterMoveNextControl = True
        Me.INDmeObservations.Location = New System.Drawing.Point(438, 468)
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
        Me.INDmeObservations.Properties.MaxLength = 5000
        Me.INDmeObservations.Size = New System.Drawing.Size(386, 71)
        Me.INDmeObservations.StyleController = Me.INDlcCommitment
        Me.INDmeObservations.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeObservations, 0)
        '
        'INDtxtDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDocument, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDocument, True)
        Me.INDtxtDocument.EnterMoveNextControl = True
        Me.INDtxtDocument.Location = New System.Drawing.Point(438, 404)
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
        Me.INDtxtDocument.StyleController = Me.INDlcCommitment
        Me.INDtxtDocument.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDocument, 0)
        Me.INDtxtDocument.ToolTip = "Este Campo es Necesario"
        '
        'INDsleDocumentSource
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleDocumentSource, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleDocumentSource, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleDocumentSource, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleDocumentSource, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleDocumentSource, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleDocumentSource, False)
        Me.INDsleDocumentSource.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleDocumentSource, False)
        Me.INDsleDocumentSource.Location = New System.Drawing.Point(438, 340)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleDocumentSource, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleDocumentSource.Name = "INDsleDocumentSource"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleDocumentSource, False)
        Me.INDsleDocumentSource.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleDocumentSource.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleDocumentSource.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleDocumentSource.Properties.Appearance.Options.UseFont = True
        Me.INDsleDocumentSource.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleDocumentSource.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleDocumentSource.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleDocumentSource.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleDocumentSource.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleDocumentSource.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleDocumentSource.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleDocumentSource.Properties.DisplayMember = "Item2"
        Me.INDsleDocumentSource.Properties.NullText = ""
        Me.INDsleDocumentSource.Properties.PopupSizeable = False
        Me.INDsleDocumentSource.Properties.PopupView = Me.GridView3
        Me.INDsleDocumentSource.Properties.ShowFooter = False
        Me.INDsleDocumentSource.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleDocumentSource, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleDocumentSource, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleDocumentSource, True)
        Me.INDsleDocumentSource.Size = New System.Drawing.Size(386, 28)
        Me.INDsleDocumentSource.StyleController = Me.INDlcCommitment
        Me.INDsleDocumentSource.TabIndex = 7
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleDocumentSource, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleDocumentSource, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleDocumentSource, "{0} - {1}")
        Me.INDsleDocumentSource.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleDocumentSource, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleDocumentSource, False)
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn12})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsCustomization.AllowGroup = False
        Me.GridView3.OptionsDetail.EnableMasterViewMode = False
        Me.GridView3.OptionsDetail.ShowDetailTabs = False
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowDetailButtons = False
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Origen Documento"
        Me.GridColumn12.FieldName = "Item2"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        '
        'INDsleThirdParty
        '
        Me.INDsleThirdParty.AllowQueryOne = True
        Me.INDsleThirdParty.Datasource = Nothing
        Me.INDsleThirdParty.DisplayMember = "{Nit} - {Name}"
        Me.INDsleThirdParty.DisplayNullText = ""
        Me.INDsleThirdParty.EditValue = Nothing
        Me.INDsleThirdParty.EnterMoveNextControl = True
        Me.INDsleThirdParty.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDsleThirdParty.IdOpenForm = 532
        Me.INDsleThirdParty.Location = New System.Drawing.Point(438, 276)
        Me.INDsleThirdParty.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDsleThirdParty.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDsleThirdParty.Name = "INDsleThirdParty"
        Me.INDsleThirdParty.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDsleThirdParty.Size = New System.Drawing.Size(386, 28)
        Me.INDsleThirdParty.TabIndex = 6
        Me.INDsleThirdParty.ValueMember = "Id"
        Me.INDsleThirdParty.View = Me.SearchLookUpEditExView6
        '
        'SearchLookUpEditExView6
        '
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView6.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView6.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView6.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView6.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView6.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView6.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView6.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18})
        Me.SearchLookUpEditExView6.Name = "SearchLookUpEditExView6"
        Me.SearchLookUpEditExView6.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView6.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView6.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEditExView6.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEditExView6.OptionsFind.FindFilterColumns = "Nit"
        Me.SearchLookUpEditExView6.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView6.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView6.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView6.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView6.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView6, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEditExView6, False)
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Nit"
        Me.GridColumn13.FieldName = "Nit"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 0
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Tipo Documento"
        Me.GridColumn14.FieldName = "PersonId.IdentificationType"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 1
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Nombre Completo"
        Me.GridColumn15.FieldName = "Name"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 2
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Ciudad"
        Me.GridColumn16.FieldName = "PersonId.IdentificacionCityId.Descripcion"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 3
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Tipo Contribuyente"
        Me.GridColumn17.FieldName = "ContributionType"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 4
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Tipo Retención"
        Me.GridColumn18.FieldName = "RetentionType"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 5
        '
        'INDdeDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDocumentDate, True)
        Me.INDdeDocumentDate.EditValue = Nothing
        Me.INDdeDocumentDate.EnterMoveNextControl = True
        Me.INDdeDocumentDate.Location = New System.Drawing.Point(438, 212)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
        Me.INDdeDocumentDate.Name = "INDdeDocumentDate"
        Me.INDdeDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDdeDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDdeDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDocumentDate.StyleController = Me.INDlcCommitment
        Me.INDdeDocumentDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDocumentDate, 0)
        Me.INDdeDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDpccChangeEntity
        '
        Me.INDpccChangeEntity.Controls.Add(Me.LayoutControl2)
        Me.INDpccChangeEntity.Location = New System.Drawing.Point(56, 288)
        Me.INDpccChangeEntity.Name = "INDpccChangeEntity"
        Me.INDpccChangeEntity.Size = New System.Drawing.Size(299, 150)
        Me.INDpccChangeEntity.TabIndex = 14
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
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidityPopUp, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidityPopUp, AppearanceObject4)
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
        Me.INDGvValidityPopUp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.INDgcStatusValidityPopUp, Me.GridColumn7, Me.GridColumn8, Me.INDgcIncomeMonthPopUp, Me.INDgcPACControlPopUp})
        Me.INDGvValidityPopUp.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvValidityPopUp.Name = "INDGvValidityPopUp"
        Me.INDGvValidityPopUp.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvValidityPopUp.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvValidityPopUp.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvValidityPopUp.OptionsView.ShowAutoFilterRow = True
        Me.INDGvValidityPopUp.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvValidityPopUp, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvValidityPopUp, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Año"
        Me.GridColumn4.FieldName = "Year"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
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
        'INDgcPACControlPopUp
        '
        Me.INDgcPACControlPopUp.Caption = "P A C"
        Me.INDgcPACControlPopUp.FieldName = "PACControl"
        Me.INDgcPACControlPopUp.Name = "INDgcPACControlPopUp"
        '
        'INDsleEntityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityPopUp, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityPopUp, AppearanceObject6)
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
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10})
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Código"
        Me.GridColumn9.FieldName = "Code"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Nombre"
        Me.GridColumn10.FieldName = "Name"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
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
        'INDsleCommitmentType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCommitmentType, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCommitmentType, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCommitmentType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCommitmentType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCommitmentType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCommitmentType, False)
        Me.INDsleCommitmentType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCommitmentType, False)
        Me.INDsleCommitmentType.Location = New System.Drawing.Point(438, 148)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCommitmentType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCommitmentType.Name = "INDsleCommitmentType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCommitmentType, False)
        Me.INDsleCommitmentType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleCommitmentType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCommitmentType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCommitmentType.Properties.Appearance.Options.UseFont = True
        Me.INDsleCommitmentType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCommitmentType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCommitmentType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCommitmentType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCommitmentType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCommitmentType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCommitmentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleCommitmentType.Properties.DisplayMember = "Item2"
        Me.INDsleCommitmentType.Properties.NullText = ""
        Me.INDsleCommitmentType.Properties.PopupSizeable = False
        Me.INDsleCommitmentType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleCommitmentType.Properties.ShowFooter = False
        Me.INDsleCommitmentType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCommitmentType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCommitmentType, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCommitmentType, True)
        Me.INDsleCommitmentType.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCommitmentType.StyleController = Me.INDlcCommitment
        Me.INDsleCommitmentType.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCommitmentType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCommitmentType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCommitmentType, "{0} - {1}")
        Me.INDsleCommitmentType.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCommitmentType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCommitmentType, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11})
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Tipo Documento"
        Me.GridColumn11.FieldName = "Item2"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(438, 84)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions3.Image = Global.Presentation.Budget.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlcCommitment
        Me.INDbtnCode.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject10)
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
        Me.INDsleValidity.Location = New System.Drawing.Point(24, 148)
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
        Me.INDsleValidity.StyleController = Me.INDlcCommitment
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
        Me.INDgvValidity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.INDgcStatusValidity, Me.GridColumn5, Me.GridColumn6, Me.INDgcIncomeMonth, Me.INDgcPACControl})
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
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDgvValidity, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Año"
        Me.GridColumn3.FieldName = "Year"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'INDgcStatusValidity
        '
        Me.INDgcStatusValidity.Caption = "Estado"
        Me.INDgcStatusValidity.FieldName = "StatusText"
        Me.INDgcStatusValidity.Name = "INDgcStatusValidity"
        Me.INDgcStatusValidity.Visible = True
        Me.INDgcStatusValidity.VisibleIndex = 1
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Resolución"
        Me.GridColumn5.FieldName = "ResolutionNumber"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Valor"
        Me.GridColumn6.DisplayFormat.FormatString = "c0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "ResolutionValue"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        '
        'INDgcIncomeMonth
        '
        Me.INDgcIncomeMonth.Caption = "Mes Ingreso"
        Me.INDgcIncomeMonth.FieldName = "IncomeMonth"
        Me.INDgcIncomeMonth.Name = "INDgcIncomeMonth"
        '
        'INDgcPACControl
        '
        Me.INDgcPACControl.Caption = "P A C"
        Me.INDgcPACControl.FieldName = "PACControl"
        Me.INDgcPACControl.Name = "INDgcPACControl"
        '
        'INDsleEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntity, AppearanceObject11)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntity, AppearanceObject12)
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
        Me.INDsleEntity.Location = New System.Drawing.Point(24, 84)
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
        Me.INDsleEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleEntity.Properties.NullText = ""
        Me.INDsleEntity.Properties.PopupSizeable = False
        Me.INDsleEntity.Properties.PopupView = Me.GridView1
        Me.INDsleEntity.Properties.ShowFooter = False
        Me.INDsleEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntity, True)
        Me.INDsleEntity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleEntity.StyleController = Me.INDlcCommitment
        Me.INDsleEntity.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntity, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntity, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowGroup = False
        Me.GridView1.OptionsDetail.EnableMasterViewMode = False
        Me.GridView1.OptionsDetail.ShowDetailTabs = False
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'INDGleAutomaticObligation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleAutomaticObligation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleAutomaticObligation, True)
        Me.INDGleAutomaticObligation.EnterMoveNextControl = True
        Me.INDGleAutomaticObligation.Location = New System.Drawing.Point(852, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleAutomaticObligation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleAutomaticObligation.Name = "INDGleAutomaticObligation"
        Me.INDGleAutomaticObligation.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleAutomaticObligation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleAutomaticObligation.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleAutomaticObligation.Properties.Appearance.Options.UseFont = True
        Me.INDGleAutomaticObligation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleAutomaticObligation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleAutomaticObligation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleAutomaticObligation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleAutomaticObligation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleAutomaticObligation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleAutomaticObligation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleAutomaticObligation.Properties.DataSource = CType(resources.GetObject("INDGleAutomaticObligation.Properties.DataSource"), Object)
        Me.INDGleAutomaticObligation.Properties.DisplayMember = "Item2"
        Me.INDGleAutomaticObligation.Properties.ImmediatePopup = True
        Me.INDGleAutomaticObligation.Properties.NullText = ""
        Me.INDGleAutomaticObligation.Properties.PopupView = Me.CtrYesNo2View
        Me.INDGleAutomaticObligation.Properties.ValueMember = "Item1"
        Me.INDGleAutomaticObligation.Size = New System.Drawing.Size(386, 28)
        Me.INDGleAutomaticObligation.StyleController = Me.INDlcCommitment
        Me.INDGleAutomaticObligation.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleAutomaticObligation, 0)
        Me.INDGleAutomaticObligation.ToolTip = "Este Campo es Necesario"
        '
        'CtrYesNo2View
        '
        Me.CtrYesNo2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CtrYesNo2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CtrYesNo2View.Appearance.FocusedRow.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo2View.Appearance.GroupRow.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.CtrYesNo2View.Appearance.Row.Options.UseFont = True
        Me.CtrYesNo2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn25})
        Me.CtrYesNo2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo2View.Name = "CtrYesNo2View"
        Me.CtrYesNo2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo2View.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo2View.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo2View.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo2View.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CtrYesNo2View, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.CtrYesNo2View, False)
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.Caption = "Selección"
        Me.GridColumn23.FieldName = "Item2"
        Me.GridColumn23.Name = "GridColumn23"
        '
        'GridColumn25
        '
        Me.GridColumn25.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn25.Caption = "Selección"
        Me.GridColumn25.FieldName = "Item2"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 0
        '
        'INDGleAutomaticPaymentOrder
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleAutomaticPaymentOrder, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleAutomaticPaymentOrder, True)
        Me.INDGleAutomaticPaymentOrder.EnterMoveNextControl = True
        Me.INDGleAutomaticPaymentOrder.Location = New System.Drawing.Point(852, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleAutomaticPaymentOrder, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDGleAutomaticPaymentOrder.StyleController = Me.INDlcCommitment
        Me.INDGleAutomaticPaymentOrder.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleAutomaticPaymentOrder, 0)
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
        Me.CtrYesNo2View1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn26})
        Me.CtrYesNo2View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo2View1.Name = "CtrYesNo2View1"
        Me.CtrYesNo2View1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo2View1.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo2View1.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo2View1.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo2View1.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo2View1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CtrYesNo2View1, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.CtrYesNo2View1, False)
        '
        'GridColumn24
        '
        Me.GridColumn24.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn24.Caption = "Selección"
        Me.GridColumn24.FieldName = "Item2"
        Me.GridColumn24.Name = "GridColumn24"
        '
        'GridColumn26
        '
        Me.GridColumn26.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn26.Caption = "Selección"
        Me.GridColumn26.FieldName = "Item2"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 0
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgCommitment, Me.INDlcgMainData, Me.INDLcgGeneralInformation, Me.INDlcgDetail})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2114, 581)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlcgCommitment
        '
        Me.INDlcgCommitment.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgCommitment.AppearanceGroup.Options.UseFont = True
        Me.INDlcgCommitment.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgCommitment.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgCommitment.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCommitment.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgCommitment.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgCommitment.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgCommitment.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCommitment.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgCommitment.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCommitment.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgCommitment.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgCommitment.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgCommitment, False)
        Me.INDlcgCommitment.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDlciValidity})
        Me.INDlcgCommitment.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgCommitment.Name = "INDlcgCommitment"
        Me.INDlcgCommitment.Size = New System.Drawing.Size(414, 561)
        Me.INDlcgCommitment.Text = "Compromisos / Reservas"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsleEntity
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Entidad Presupuestal"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(135, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'INDlciValidity
        '
        Me.INDlciValidity.Control = Me.INDsleValidity
        Me.INDlciValidity.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.Name = "INDlciValidity"
        Me.INDlciValidity.Size = New System.Drawing.Size(390, 438)
        Me.INDlciValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidity.Text = "Vigencia"
        Me.INDlciValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidity.TextSize = New System.Drawing.Size(135, 20)
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
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCode, Me.INDlciCommitmentType, Me.INDlciDocumentDate, Me.INDlciThirdParty, Me.INDlciDocumentSource, Me.INDlciDocument, Me.INDlciObservations})
        Me.INDlcgMainData.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(414, 561)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDlciCode
        '
        Me.INDlciCode.Control = Me.INDbtnCode
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.ShowInCustomizationForm = False
        Me.INDlciCode.Size = New System.Drawing.Size(390, 64)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCode.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'INDlciCommitmentType
        '
        Me.INDlciCommitmentType.Control = Me.INDsleCommitmentType
        Me.INDlciCommitmentType.Location = New System.Drawing.Point(0, 64)
        Me.INDlciCommitmentType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciCommitmentType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciCommitmentType.Name = "INDlciCommitmentType"
        Me.INDlciCommitmentType.ShowInCustomizationForm = False
        Me.INDlciCommitmentType.Size = New System.Drawing.Size(390, 64)
        Me.INDlciCommitmentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCommitmentType.Text = "Tipo Documento"
        Me.INDlciCommitmentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCommitmentType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCommitmentType.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciCommitmentType.TextToControlDistance = 5
        '
        'INDlciDocumentDate
        '
        Me.INDlciDocumentDate.Control = Me.INDdeDocumentDate
        Me.INDlciDocumentDate.Location = New System.Drawing.Point(0, 128)
        Me.INDlciDocumentDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.Name = "INDlciDocumentDate"
        Me.INDlciDocumentDate.ShowInCustomizationForm = False
        Me.INDlciDocumentDate.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDocumentDate.Text = "Fecha "
        Me.INDlciDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDocumentDate.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDocumentDate.TextToControlDistance = 5
        '
        'INDlciThirdParty
        '
        Me.INDlciThirdParty.Control = Me.INDsleThirdParty
        Me.INDlciThirdParty.Location = New System.Drawing.Point(0, 192)
        Me.INDlciThirdParty.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciThirdParty.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciThirdParty.Name = "INDlciThirdParty"
        Me.INDlciThirdParty.ShowInCustomizationForm = False
        Me.INDlciThirdParty.Size = New System.Drawing.Size(390, 64)
        Me.INDlciThirdParty.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciThirdParty.Text = "Tercero"
        Me.INDlciThirdParty.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciThirdParty.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciThirdParty.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciThirdParty.TextToControlDistance = 5
        '
        'INDlciDocumentSource
        '
        Me.INDlciDocumentSource.Control = Me.INDsleDocumentSource
        Me.INDlciDocumentSource.Location = New System.Drawing.Point(0, 256)
        Me.INDlciDocumentSource.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentSource.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentSource.Name = "INDlciDocumentSource"
        Me.INDlciDocumentSource.ShowInCustomizationForm = False
        Me.INDlciDocumentSource.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentSource.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDocumentSource.Text = "Origen Documento"
        Me.INDlciDocumentSource.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDocumentSource.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDocumentSource.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDocumentSource.TextToControlDistance = 5
        '
        'INDlciDocument
        '
        Me.INDlciDocument.Control = Me.INDtxtDocument
        Me.INDlciDocument.Location = New System.Drawing.Point(0, 320)
        Me.INDlciDocument.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocument.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocument.Name = "INDlciDocument"
        Me.INDlciDocument.ShowInCustomizationForm = False
        Me.INDlciDocument.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDocument.Text = "Documento"
        Me.INDlciDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDocument.TextSize = New System.Drawing.Size(135, 20)
        Me.INDlciDocument.TextToControlDistance = 5
        '
        'INDlciObservations
        '
        Me.INDlciObservations.Control = Me.INDmeObservations
        Me.INDlciObservations.Location = New System.Drawing.Point(0, 384)
        Me.INDlciObservations.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlciObservations.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlciObservations.Name = "INDlciObservations"
        Me.INDlciObservations.Size = New System.Drawing.Size(390, 118)
        Me.INDlciObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciObservations.Text = "Observaciones"
        Me.INDlciObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciObservations.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciObservations.TextToControlDistance = 5
        '
        'INDLcgGeneralInformation
        '
        Me.INDLcgGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgGeneralInformation, False)
        Me.INDLcgGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAutomaticObligation, Me.INDLciAutomaticPaymentOrder})
        Me.INDLcgGeneralInformation.Location = New System.Drawing.Point(828, 0)
        Me.INDLcgGeneralInformation.Name = "INDLcgGeneralInformation"
        Me.INDLcgGeneralInformation.Size = New System.Drawing.Size(414, 561)
        Me.INDLcgGeneralInformation.Text = "Información General"
        '
        'INDLciAutomaticObligation
        '
        Me.INDLciAutomaticObligation.Control = Me.INDGleAutomaticObligation
        Me.INDLciAutomaticObligation.CustomizationFormText = "Obligación Automática"
        Me.INDLciAutomaticObligation.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAutomaticObligation.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticObligation.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticObligation.Name = "INDLciAutomaticObligation"
        Me.INDLciAutomaticObligation.Size = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticObligation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAutomaticObligation.Text = "Obligación Automática"
        Me.INDLciAutomaticObligation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAutomaticObligation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAutomaticObligation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAutomaticObligation.TextToControlDistance = 5
        '
        'INDLciAutomaticPaymentOrder
        '
        Me.INDLciAutomaticPaymentOrder.Control = Me.INDGleAutomaticPaymentOrder
        Me.INDLciAutomaticPaymentOrder.CustomizationFormText = "Orden de Pago Automática"
        Me.INDLciAutomaticPaymentOrder.Location = New System.Drawing.Point(0, 64)
        Me.INDLciAutomaticPaymentOrder.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticPaymentOrder.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticPaymentOrder.Name = "INDLciAutomaticPaymentOrder"
        Me.INDLciAutomaticPaymentOrder.Size = New System.Drawing.Size(390, 438)
        Me.INDLciAutomaticPaymentOrder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAutomaticPaymentOrder.Text = "Orden de Pago Automática"
        Me.INDLciAutomaticPaymentOrder.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAutomaticPaymentOrder.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAutomaticPaymentOrder.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAutomaticPaymentOrder.TextToControlDistance = 5
        Me.INDLciAutomaticPaymentOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlcgDetail
        '
        Me.INDlcgDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDetail.AppearanceGroup.Options.UseFont = True
        Me.INDlcgDetail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDetail.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgDetail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDetail.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgDetail, False)
        Me.INDlcgDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciGcAvailability, Me.LayoutControlItem1, Me.INDlciGcBudget})
        Me.INDlcgDetail.Location = New System.Drawing.Point(1242, 0)
        Me.INDlcgDetail.Name = "INDlcgDetail"
        Me.INDlcgDetail.Size = New System.Drawing.Size(852, 561)
        Me.INDlcgDetail.Text = "Rubros"
        '
        'INDlciGcAvailability
        '
        Me.INDlciGcAvailability.Control = Me.INDgcDetailAvailability
        Me.INDlciGcAvailability.Location = New System.Drawing.Point(0, 36)
        Me.INDlciGcAvailability.MaxSize = New System.Drawing.Size(824, 0)
        Me.INDlciGcAvailability.MinSize = New System.Drawing.Size(824, 1)
        Me.INDlciGcAvailability.Name = "INDlciGcAvailability"
        Me.INDlciGcAvailability.Size = New System.Drawing.Size(828, 204)
        Me.INDlciGcAvailability.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciGcAvailability.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciGcAvailability.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDsbAddCategory
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDlciGcBudget
        '
        Me.INDlciGcBudget.Control = Me.INDGcCategory
        Me.INDlciGcBudget.Location = New System.Drawing.Point(0, 240)
        Me.INDlciGcBudget.MaxSize = New System.Drawing.Size(824, 0)
        Me.INDlciGcBudget.MinSize = New System.Drawing.Size(824, 1)
        Me.INDlciGcBudget.Name = "INDlciGcBudget"
        Me.INDlciGcBudget.Size = New System.Drawing.Size(828, 262)
        Me.INDlciGcBudget.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciGcBudget.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciGcBudget.TextVisible = False
        Me.INDlciGcBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'GridColumn21
        '
        Me.GridColumn21.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn21.Caption = "Selección"
        Me.GridColumn21.FieldName = "Item2"
        Me.GridColumn21.Name = "GridColumn21"
        '
        'GridColumn22
        '
        Me.GridColumn22.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn22.Caption = "Selección"
        Me.GridColumn22.FieldName = "Item2"
        Me.GridColumn22.Name = "GridColumn22"
        '
        'GridColumn19
        '
        Me.GridColumn19.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19.Caption = "Selección"
        Me.GridColumn19.FieldName = "Item2"
        Me.GridColumn19.Name = "GridColumn19"
        '
        'GridColumn20
        '
        Me.GridColumn20.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn20.Caption = "Selección"
        Me.GridColumn20.FieldName = "Item2"
        Me.GridColumn20.Name = "GridColumn20"
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "-.png")
        Me.ImageCollection1.Images.SetKeyName(1, "+.png")
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'GridColumn41
        '
        Me.GridColumn41.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn41.Caption = "Selección"
        Me.GridColumn41.FieldName = "Item2"
        Me.GridColumn41.Name = "GridColumn41"
        '
        'GridColumn411
        '
        Me.GridColumn411.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn411.Caption = "Selección"
        Me.GridColumn411.FieldName = "Item2"
        Me.GridColumn411.Name = "GridColumn411"
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        '
        'FrmCommitment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmCommitment"
        Me.Opacity = 1.0R
        Me.Tag = "231"
        Me.Text = "Compromisos / Reservas"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValueCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcCommitment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcCommitment.ResumeLayout(False)
        CType(Me.INDGcCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtDateCategory.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtDateCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetailAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleDocumentSource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.INDsleCommitmentType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAutomaticObligation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAutomaticObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAutomaticPaymentOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAutomaticPaymentOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo2View1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgCommitment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCommitmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDocumentSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAutomaticObligation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAutomaticPaymentOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciGcAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciGcBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcCommitment As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcPACControl As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlciValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgCommitment As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCommitmentType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCommitmentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpccChangeEntity As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsleValidityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvValidityPopUp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidityPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcIncomeMonthPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcPACControlPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciEntityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciValidityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlciDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleThirdParty As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciThirdParty As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleDocumentSource As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciDocumentSource As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtDocument As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAddCategory As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcDetailAvailability As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColResource As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTipe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolBalanceAvailability As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCharacter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDlciGcAvailability As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAvailability As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDGcCategory As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewBudget As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResource As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDateExpired As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlciGcBudget As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrepTxtDateCategory As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents INDrepTxtValueCategory As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDGleAutomaticObligation As CtrYesNo
    Friend WithEvents CtrYesNo2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciAutomaticObligation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleAutomaticPaymentOrder As CtrYesNo
    Friend WithEvents CtrYesNo2View1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciAutomaticPaymentOrder As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn411 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView2 As IndigoGridView
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
End Class
