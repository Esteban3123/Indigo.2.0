Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRecognition
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRecognition))
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDCnc = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcRecognition = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGleAutomaticCollection = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleDependency = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeDependency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameDependency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleThirdParty = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn267 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn268 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn269 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn270 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn271 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn272 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcCategory = New DevExpress.XtraGrid.GridControl()
        Me.viewBudget = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResource = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBalance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTxeValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDMemObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxeDocument = New DevExpress.XtraEditors.TextEdit()
        Me.INDGleRecognitionType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResolutionName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResolutionValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleBudgetEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgRecognition = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRecognitionType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciThirdParty = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDependency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgGeneralInformation2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAutomaticCollection = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCategory = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDGvValidityPopUp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidityPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciEntityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDsleEntityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleValidityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDlciValidityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDpccChangeEntity = New DevExpress.XtraEditors.PopupContainerControl()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.GridColumn306 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCnc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRecognition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRecognition.SuspendLayout()
        CType(Me.INDGleAutomaticCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAutomaticCollection.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDependency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxeValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMemObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxeDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleRecognitionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRecognition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRecognitionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDependency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgGeneralInformation2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAutomaticCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccChangeEntity.SuspendLayout()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRecognition)
        Me.INDPanelControlBase.Controls.Add(Me.INDCnc)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1316, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(1316, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1316, 98)
        '
        'INDValue
        '
        Me.INDValue.Caption = "Valor"
        Me.INDValue.ColumnEdit = Me.INDrepTxtValue
        Me.INDValue.DisplayFormat.FormatString = "C0"
        Me.INDValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDValue.FieldName = "InitialValue"
        Me.INDValue.Name = "INDValue"
        Me.INDValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InitialValue", "{0:c0}")})
        Me.INDValue.Visible = True
        Me.INDValue.VisibleIndex = 5
        '
        'INDrepTxtValue
        '
        Me.INDrepTxtValue.AutoHeight = False
        Me.INDrepTxtValue.Mask.EditMask = "c0"
        Me.INDrepTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtValue.Name = "INDrepTxtValue"
        '
        'INDCnc
        '
        Me.INDCnc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCnc.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnc.LayoutControl = Me.INDLcRecognition
        Me.INDCnc.Location = New System.Drawing.Point(2, 7)
        Me.INDCnc.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCnc.Name = "INDCnc"
        Me.INDCnc.Size = New System.Drawing.Size(200, 598)
        Me.INDCnc.TabIndex = 0
        Me.INDCnc.UseDisabledStatePainter = False
        '
        'INDLcRecognition
        '
        Me.INDLcRecognition.Controls.Add(Me.INDGleAutomaticCollection)
        Me.INDLcRecognition.Controls.Add(Me.INDSleDependency)
        Me.INDLcRecognition.Controls.Add(Me.INDSleThirdParty)
        Me.INDLcRecognition.Controls.Add(Me.INDGcCategory)
        Me.INDLcRecognition.Controls.Add(Me.INDSbAdd)
        Me.INDLcRecognition.Controls.Add(Me.INDTxeValue)
        Me.INDLcRecognition.Controls.Add(Me.INDMemObservation)
        Me.INDLcRecognition.Controls.Add(Me.INDTxeDocument)
        Me.INDLcRecognition.Controls.Add(Me.INDGleRecognitionType)
        Me.INDLcRecognition.Controls.Add(Me.INDDteDocumentDate)
        Me.INDLcRecognition.Controls.Add(Me.INDBtnCode)
        Me.INDLcRecognition.Controls.Add(Me.INDsleValidity)
        Me.INDLcRecognition.Controls.Add(Me.INDsleBudgetEntity)
        Me.INDLcRecognition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRecognition.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRecognition.Name = "INDLcRecognition"
        Me.INDLcRecognition.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(459, 288, 362, 581)
        Me.INDLcRecognition.Root = Me.INDLcgRecognition
        Me.INDLcRecognition.Size = New System.Drawing.Size(1112, 598)
        Me.INDLcRecognition.TabIndex = 1
        Me.INDLcRecognition.Text = "LayoutControl1"
        '
        'INDGleAutomaticCollection
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleAutomaticCollection, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleAutomaticCollection, False)
        Me.INDGleAutomaticCollection.EnterMoveNextControl = True
        Me.INDGleAutomaticCollection.Location = New System.Drawing.Point(852, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleAutomaticCollection, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleAutomaticCollection.Name = "INDGleAutomaticCollection"
        Me.INDGleAutomaticCollection.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleAutomaticCollection.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleAutomaticCollection.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleAutomaticCollection.Properties.Appearance.Options.UseFont = True
        Me.INDGleAutomaticCollection.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleAutomaticCollection.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleAutomaticCollection.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleAutomaticCollection.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleAutomaticCollection.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleAutomaticCollection.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleAutomaticCollection.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleAutomaticCollection.Properties.DataSource = CType(resources.GetObject("INDGleAutomaticCollection.Properties.DataSource"), Object)
        Me.INDGleAutomaticCollection.Properties.DisplayMember = "Item2"
        Me.INDGleAutomaticCollection.Properties.ImmediatePopup = True
        Me.INDGleAutomaticCollection.Properties.NullText = ""
        Me.INDGleAutomaticCollection.Properties.PopupView = Me.CtrYesNo2View
        Me.INDGleAutomaticCollection.Properties.ValueMember = "Item1"
        Me.INDGleAutomaticCollection.Size = New System.Drawing.Size(386, 28)
        Me.INDGleAutomaticCollection.StyleController = Me.INDLcRecognition
        Me.INDGleAutomaticCollection.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleAutomaticCollection, 0)
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
        Me.CtrYesNo2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn41})
        Me.CtrYesNo2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo2View.Name = "CtrYesNo2View"
        Me.CtrYesNo2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo2View.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo2View.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo2View.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo2View.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CtrYesNo2View, False)
        '
        'GridColumn40
        '
        Me.GridColumn40.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn40.Caption = "Selección"
        Me.GridColumn40.FieldName = "Item2"
        Me.GridColumn40.Name = "GridColumn40"
        '
        'GridColumn41
        '
        Me.GridColumn41.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn41.Caption = "Selección"
        Me.GridColumn41.FieldName = "Item2"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 0
        '
        'INDSleDependency
        '
        Me.INDSleDependency.AllowQueryOne = False
        Me.INDSleDependency.Datasource = Nothing
        Me.INDSleDependency.DisplayMember = "{Code} - {Name}"
        Me.INDSleDependency.DisplayNullText = ""
        Me.INDSleDependency.EditValue = Nothing
        Me.INDSleDependency.EnterMoveNextControl = True
        Me.INDSleDependency.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleDependency.IdOpenForm = 204
        Me.INDSleDependency.Location = New System.Drawing.Point(438, 405)
        Me.INDSleDependency.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleDependency.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleDependency.Name = "INDSleDependency"
        Me.INDSleDependency.PopUpFormSize = New System.Drawing.Size(500, 400)
        Me.INDSleDependency.Size = New System.Drawing.Size(386, 28)
        Me.INDSleDependency.TabIndex = 11
        Me.INDSleDependency.ValueMember = "Id"
        Me.INDSleDependency.View = Me.SearchLookUpEditExView8
        '
        'SearchLookUpEditExView8
        '
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView8.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView8.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView8.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView8.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeDependency, Me.INDNameDependency})
        Me.SearchLookUpEditExView8.Name = "SearchLookUpEditExView8"
        Me.SearchLookUpEditExView8.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView8.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView8.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEditExView8.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEditExView8.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEditExView8.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView8.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView8.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView8, False)
        '
        'INDCodeDependency
        '
        Me.INDCodeDependency.Caption = "Código"
        Me.INDCodeDependency.FieldName = "Code"
        Me.INDCodeDependency.Name = "INDCodeDependency"
        Me.INDCodeDependency.OptionsColumn.AllowEdit = False
        Me.INDCodeDependency.Visible = True
        Me.INDCodeDependency.VisibleIndex = 0
        '
        'INDNameDependency
        '
        Me.INDNameDependency.Caption = "Dependencia"
        Me.INDNameDependency.FieldName = "Name"
        Me.INDNameDependency.Name = "INDNameDependency"
        Me.INDNameDependency.OptionsColumn.AllowEdit = False
        Me.INDNameDependency.Visible = True
        Me.INDNameDependency.VisibleIndex = 1
        '
        'INDSleThirdParty
        '
        Me.INDSleThirdParty.AllowQueryOne = True
        Me.INDSleThirdParty.Datasource = Nothing
        Me.INDSleThirdParty.DisplayMember = "{Nit} - {Name}"
        Me.INDSleThirdParty.DisplayNullText = ""
        Me.INDSleThirdParty.EditValue = Nothing
        Me.INDSleThirdParty.EnterMoveNextControl = True
        Me.INDSleThirdParty.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleThirdParty.IdOpenForm = 532
        Me.INDSleThirdParty.Location = New System.Drawing.Point(438, 341)
        Me.INDSleThirdParty.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleThirdParty.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleThirdParty.Name = "INDSleThirdParty"
        Me.INDSleThirdParty.PopUpFormSize = New System.Drawing.Size(900, 400)
        Me.INDSleThirdParty.Size = New System.Drawing.Size(386, 28)
        Me.INDSleThirdParty.TabIndex = 10
        Me.INDSleThirdParty.ValueMember = "Id"
        Me.INDSleThirdParty.View = Me.SearchLookUpEditExView7
        '
        'SearchLookUpEditExView7
        '
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView7.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView7.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView7.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView7.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView7.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView7.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView7.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView7.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn267, Me.GridColumn268, Me.GridColumn269, Me.GridColumn270, Me.GridColumn271, Me.GridColumn272})
        Me.SearchLookUpEditExView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEditExView7.Name = "SearchLookUpEditExView7"
        Me.SearchLookUpEditExView7.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView7.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView7.OptionsFind.FindFilterColumns = "Nit"
        Me.SearchLookUpEditExView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEditExView7.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView7.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView7.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView7, False)
        '
        'GridColumn267
        '
        Me.GridColumn267.Caption = "Nit"
        Me.GridColumn267.FieldName = "Nit"
        Me.GridColumn267.Name = "GridColumn267"
        Me.GridColumn267.OptionsColumn.AllowEdit = False
        Me.GridColumn267.Visible = True
        Me.GridColumn267.VisibleIndex = 0
        Me.GridColumn267.Width = 167
        '
        'GridColumn268
        '
        Me.GridColumn268.Caption = "Tipo Documento"
        Me.GridColumn268.FieldName = "PersonId.IdentificationTypeName"
        Me.GridColumn268.Name = "GridColumn268"
        Me.GridColumn268.OptionsColumn.AllowEdit = False
        Me.GridColumn268.Visible = True
        Me.GridColumn268.VisibleIndex = 1
        Me.GridColumn268.Width = 240
        '
        'GridColumn269
        '
        Me.GridColumn269.Caption = "Nombre Completo"
        Me.GridColumn269.FieldName = "Name"
        Me.GridColumn269.Name = "GridColumn269"
        Me.GridColumn269.OptionsColumn.AllowEdit = False
        Me.GridColumn269.Visible = True
        Me.GridColumn269.VisibleIndex = 2
        Me.GridColumn269.Width = 383
        '
        'GridColumn270
        '
        Me.GridColumn270.Caption = "Ciudad"
        Me.GridColumn270.FieldName = "PersonId.IdentificacionCityId.Descripcion"
        Me.GridColumn270.Name = "GridColumn270"
        Me.GridColumn270.OptionsColumn.AllowEdit = False
        Me.GridColumn270.Visible = True
        Me.GridColumn270.VisibleIndex = 3
        Me.GridColumn270.Width = 187
        '
        'GridColumn271
        '
        Me.GridColumn271.Caption = "Tipo Ret."
        Me.GridColumn271.FieldName = "RetentionTypeName"
        Me.GridColumn271.Name = "GridColumn271"
        Me.GridColumn271.OptionsColumn.AllowEdit = False
        Me.GridColumn271.Visible = True
        Me.GridColumn271.VisibleIndex = 4
        Me.GridColumn271.Width = 203
        '
        'GridColumn272
        '
        Me.GridColumn272.Caption = "Tipo Contr"
        Me.GridColumn272.FieldName = "ContributionTypeName"
        Me.GridColumn272.Name = "GridColumn272"
        Me.GridColumn272.OptionsColumn.AllowEdit = False
        Me.GridColumn272.Visible = True
        Me.GridColumn272.VisibleIndex = 5
        Me.GridColumn272.Width = 234
        '
        'INDGcCategory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCategory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCategory, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCategory, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCategory, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCategory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCategory, False)
        Me.INDGcCategory.Location = New System.Drawing.Point(1266, 95)
        Me.INDGcCategory.MainView = Me.viewBudget
        Me.INDGcCategory.Name = "INDGcCategory"
        Me.INDGcCategory.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtValue})
        Me.INDGcCategory.Size = New System.Drawing.Size(824, 462)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCategory, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcCategory.TabIndex = 16
        Me.INDGcCategory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewBudget})
        '
        'viewBudget
        '
        Me.viewBudget.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewBudget.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewBudget.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewBudget.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewBudget.Appearance.FocusedRow.Options.UseFont = True
        Me.viewBudget.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewBudget.Appearance.GroupRow.Options.UseFont = True
        Me.viewBudget.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewBudget.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewBudget.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewBudget.Appearance.Row.Options.UseFont = True
        Me.viewBudget.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewBudget.Appearance.ViewCaption.Options.UseFont = True
        Me.viewBudget.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeCategory, Me.INDNameCategory, Me.INDResource, Me.INDType, Me.INDBalance, Me.INDValue})
        GridFormatRule1.Column = Me.INDValue
        GridFormatRule1.ColumnApplyTo = Me.INDValue
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
        Me.viewBudget.OptionsView.ShowFooter = True
        Me.viewBudget.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewBudget, False)
        '
        'INDCodeCategory
        '
        Me.INDCodeCategory.Caption = "Código"
        Me.INDCodeCategory.FieldName = "CodeCategory"
        Me.INDCodeCategory.Name = "INDCodeCategory"
        Me.INDCodeCategory.OptionsColumn.AllowEdit = False
        Me.INDCodeCategory.OptionsColumn.AllowFocus = False
        Me.INDCodeCategory.Visible = True
        Me.INDCodeCategory.VisibleIndex = 0
        '
        'INDNameCategory
        '
        Me.INDNameCategory.Caption = "Nombre"
        Me.INDNameCategory.FieldName = "NameCategory"
        Me.INDNameCategory.Name = "INDNameCategory"
        Me.INDNameCategory.OptionsColumn.AllowEdit = False
        Me.INDNameCategory.OptionsColumn.AllowFocus = False
        Me.INDNameCategory.Visible = True
        Me.INDNameCategory.VisibleIndex = 1
        '
        'INDResource
        '
        Me.INDResource.Caption = "Recurso"
        Me.INDResource.FieldName = "CodeNameFinancialSource"
        Me.INDResource.Name = "INDResource"
        Me.INDResource.OptionsColumn.AllowEdit = False
        Me.INDResource.OptionsColumn.AllowFocus = False
        Me.INDResource.Visible = True
        Me.INDResource.VisibleIndex = 2
        '
        'INDType
        '
        Me.INDType.Caption = "Tipo"
        Me.INDType.FieldName = "CodeNameRevenueType"
        Me.INDType.Name = "INDType"
        Me.INDType.OptionsColumn.AllowEdit = False
        Me.INDType.OptionsColumn.AllowFocus = False
        Me.INDType.Visible = True
        Me.INDType.VisibleIndex = 3
        '
        'INDBalance
        '
        Me.INDBalance.Caption = "Saldo"
        Me.INDBalance.DisplayFormat.FormatString = "c0"
        Me.INDBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDBalance.FieldName = "ValueBalance"
        Me.INDBalance.Name = "INDBalance"
        Me.INDBalance.OptionsColumn.AllowEdit = False
        Me.INDBalance.OptionsColumn.AllowFocus = False
        Me.INDBalance.Visible = True
        Me.INDBalance.VisibleIndex = 4
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Location = New System.Drawing.Point(1266, 59)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(824, 32)
        Me.INDSbAdd.StyleController = Me.INDLcRecognition
        Me.INDSbAdd.TabIndex = 15
        Me.INDSbAdd.Text = "Agregar"
        '
        'INDTxeValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeValue, False)
        Me.INDTxeValue.EnterMoveNextControl = True
        Me.INDTxeValue.Location = New System.Drawing.Point(852, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxeValue.Name = "INDTxeValue"
        Me.INDTxeValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxeValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxeValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxeValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxeValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxeValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxeValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxeValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxeValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxeValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxeValue.Properties.Mask.EditMask = "c0"
        Me.INDTxeValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxeValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxeValue.Properties.ReadOnly = True
        Me.INDTxeValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxeValue.StyleController = Me.INDLcRecognition
        Me.INDTxeValue.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeValue, 0)
        '
        'INDMemObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMemObservation, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMemObservation, True)
        Me.INDMemObservation.EnterMoveNextControl = True
        Me.INDMemObservation.Location = New System.Drawing.Point(438, 469)
        Me.IndigoTextEdit1.SetMascara(Me.INDMemObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMemObservation.Name = "INDMemObservation"
        Me.INDMemObservation.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMemObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMemObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMemObservation.Properties.Appearance.Options.UseFont = True
        Me.INDMemObservation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMemObservation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMemObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMemObservation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMemObservation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMemObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMemObservation.Properties.MaxLength = 500
        Me.INDMemObservation.Size = New System.Drawing.Size(386, 70)
        Me.INDMemObservation.StyleController = Me.INDLcRecognition
        Me.INDMemObservation.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMemObservation, 0)
        Me.INDMemObservation.ToolTip = "Este Campo es Necesario"
        '
        'INDTxeDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeDocument, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeDocument, True)
        Me.INDTxeDocument.EnterMoveNextControl = True
        Me.INDTxeDocument.Location = New System.Drawing.Point(438, 277)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxeDocument.Name = "INDTxeDocument"
        Me.INDTxeDocument.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxeDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxeDocument.Properties.Appearance.Options.UseFont = True
        Me.INDTxeDocument.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxeDocument.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxeDocument.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeDocument.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxeDocument.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxeDocument.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxeDocument.Size = New System.Drawing.Size(386, 28)
        Me.INDTxeDocument.StyleController = Me.INDLcRecognition
        Me.INDTxeDocument.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeDocument, 0)
        Me.INDTxeDocument.ToolTip = "Este Campo es Necesario"
        '
        'INDGleRecognitionType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleRecognitionType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleRecognitionType, True)
        Me.INDGleRecognitionType.EnterMoveNextControl = True
        Me.INDGleRecognitionType.Location = New System.Drawing.Point(438, 213)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleRecognitionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleRecognitionType.Name = "INDGleRecognitionType"
        Me.INDGleRecognitionType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleRecognitionType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleRecognitionType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleRecognitionType.Properties.Appearance.Options.UseFont = True
        Me.INDGleRecognitionType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleRecognitionType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleRecognitionType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleRecognitionType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleRecognitionType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleRecognitionType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleRecognitionType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleRecognitionType.Properties.DisplayMember = "Item2"
        Me.INDGleRecognitionType.Properties.NullText = ""
        Me.INDGleRecognitionType.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleRecognitionType.Properties.ValueMember = "Item1"
        Me.INDGleRecognitionType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleRecognitionType.StyleController = Me.INDLcRecognition
        Me.INDGleRecognitionType.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleRecognitionType, 0)
        Me.INDGleRecognitionType.ToolTip = "Este Campo es Necesario"
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
        Me.GridColumn1.Caption = "Tipo de Reconocimiento"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDDteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDocumentDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDocumentDate, True)
        Me.INDDteDocumentDate.EditValue = Nothing
        Me.INDDteDocumentDate.EnterMoveNextControl = True
        Me.INDDteDocumentDate.Location = New System.Drawing.Point(438, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDocumentDate, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
        Me.INDDteDocumentDate.Name = "INDDteDocumentDate"
        Me.INDDteDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDocumentDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDDteDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDocumentDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDDteDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDteDocumentDate.StyleController = Me.INDLcRecognition
        Me.INDDteDocumentDate.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDocumentDate, 0)
        Me.INDDteDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(438, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDBtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBtnCode.Name = "INDBtnCode"
        Me.INDBtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDBtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Budget.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBtnCode.StyleController = Me.INDLcRecognition
        Me.INDBtnCode.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
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
        Me.INDsleValidity.StyleController = Me.INDLcRecognition
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
        Me.INDgvValidity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDYear, Me.INDStatus, Me.INDResolutionName, Me.INDResolutionValue})
        Me.INDgvValidity.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvValidity.Name = "INDgvValidity"
        Me.INDgvValidity.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvValidity.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvValidity.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvValidity.OptionsView.ShowAutoFilterRow = True
        Me.INDgvValidity.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvValidity, False)
        '
        'INDYear
        '
        Me.INDYear.Caption = "Año"
        Me.INDYear.FieldName = "Year"
        Me.INDYear.Name = "INDYear"
        Me.INDYear.Visible = True
        Me.INDYear.VisibleIndex = 0
        '
        'INDStatus
        '
        Me.INDStatus.Caption = "Estado"
        Me.INDStatus.FieldName = "Status"
        Me.INDStatus.Name = "INDStatus"
        Me.INDStatus.Visible = True
        Me.INDStatus.VisibleIndex = 1
        '
        'INDResolutionName
        '
        Me.INDResolutionName.Caption = "Resolución"
        Me.INDResolutionName.FieldName = "ResolutionName"
        Me.INDResolutionName.Name = "INDResolutionName"
        Me.INDResolutionName.Visible = True
        Me.INDResolutionName.VisibleIndex = 2
        '
        'INDResolutionValue
        '
        Me.INDResolutionValue.Caption = "Valor"
        Me.INDResolutionValue.DisplayFormat.FormatString = "c0"
        Me.INDResolutionValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDResolutionValue.FieldName = "ResolutionValue"
        Me.INDResolutionValue.Name = "INDResolutionValue"
        Me.INDResolutionValue.Visible = True
        Me.INDResolutionValue.VisibleIndex = 3
        '
        'INDsleBudgetEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBudgetEntity, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBudgetEntity, AppearanceObject4)
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
        Me.INDsleBudgetEntity.StyleController = Me.INDLcRecognition
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCode, Me.INDName})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDCode
        '
        Me.INDCode.Caption = "Código"
        Me.INDCode.FieldName = "Code"
        Me.INDCode.Name = "INDCode"
        Me.INDCode.Visible = True
        Me.INDCode.VisibleIndex = 0
        '
        'INDName
        '
        Me.INDName.Caption = "Nombre"
        Me.INDName.FieldName = "Name"
        Me.INDName.Name = "INDName"
        Me.INDName.Visible = True
        Me.INDName.VisibleIndex = 1
        '
        'INDLcgRecognition
        '
        Me.INDLcgRecognition.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRecognition.AppearanceGroup.Options.UseFont = True
        Me.INDLcgRecognition.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRecognition.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgRecognition.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRecognition.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgRecognition.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgRecognition.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgRecognition.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRecognition.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgRecognition.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRecognition.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgRecognition.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRecognition.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgRecognition, False)
        Me.INDLcgRecognition.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgRecognition.GroupBordersVisible = False
        Me.INDLcgRecognition.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgPrincipalData, Me.INDLcgGeneralInformation, Me.INDLcgGeneralInformation2, Me.INDLcgCategory})
        Me.INDLcgRecognition.Name = "Root"
        Me.INDLcgRecognition.Size = New System.Drawing.Size(2114, 581)
        Me.INDLcgRecognition.TextVisible = False
        '
        'INDLcgPrincipalData
        '
        Me.INDLcgPrincipalData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPrincipalData.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPrincipalData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPrincipalData, False)
        Me.INDLcgPrincipalData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEntity, Me.INDlciValidity})
        Me.INDLcgPrincipalData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgPrincipalData.Name = "INDLcgPrincipalData"
        Me.INDLcgPrincipalData.Size = New System.Drawing.Size(414, 561)
        Me.INDLcgPrincipalData.Text = "Datos Principales"
        '
        'INDlyItemEntity
        '
        Me.INDlyItemEntity.Control = Me.INDsleBudgetEntity
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
        Me.INDlciValidity.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.Name = "INDlciValidity"
        Me.INDlciValidity.Size = New System.Drawing.Size(390, 438)
        Me.INDlciValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidity.Text = "Vigencia"
        Me.INDlciValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciValidity.TextToControlDistance = 5
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
        Me.INDLcgGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciDocumentDate, Me.INDLciRecognitionType, Me.INDLciDocument, Me.INDLciObservations, Me.INDLciThirdParty, Me.INDLciDependency})
        Me.INDLcgGeneralInformation.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgGeneralInformation.Name = "INDLcgGeneralInformation"
        Me.INDLcgGeneralInformation.Size = New System.Drawing.Size(414, 561)
        Me.INDLcgGeneralInformation.Text = "Información General"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBtnCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(390, 64)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciDocumentDate
        '
        Me.INDLciDocumentDate.Control = Me.INDDteDocumentDate
        Me.INDLciDocumentDate.Location = New System.Drawing.Point(0, 64)
        Me.INDLciDocumentDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.Name = "INDLciDocumentDate"
        Me.INDLciDocumentDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocumentDate.Text = "Fecha"
        Me.INDLciDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocumentDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDocumentDate.TextToControlDistance = 5
        '
        'INDLciRecognitionType
        '
        Me.INDLciRecognitionType.Control = Me.INDGleRecognitionType
        Me.INDLciRecognitionType.Location = New System.Drawing.Point(0, 128)
        Me.INDLciRecognitionType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciRecognitionType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciRecognitionType.Name = "INDLciRecognitionType"
        Me.INDLciRecognitionType.Size = New System.Drawing.Size(390, 64)
        Me.INDLciRecognitionType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRecognitionType.Text = "Tipo Reconocimiento"
        Me.INDLciRecognitionType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRecognitionType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRecognitionType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRecognitionType.TextToControlDistance = 5
        '
        'INDLciDocument
        '
        Me.INDLciDocument.Control = Me.INDTxeDocument
        Me.INDLciDocument.Location = New System.Drawing.Point(0, 192)
        Me.INDLciDocument.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocument.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocument.Name = "INDLciDocument"
        Me.INDLciDocument.Size = New System.Drawing.Size(390, 64)
        Me.INDLciDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocument.Text = "Documento"
        Me.INDLciDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocument.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDocument.TextToControlDistance = 5
        '
        'INDLciObservations
        '
        Me.INDLciObservations.Control = Me.INDMemObservation
        Me.INDLciObservations.Location = New System.Drawing.Point(0, 384)
        Me.INDLciObservations.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDLciObservations.MinSize = New System.Drawing.Size(390, 100)
        Me.INDLciObservations.Name = "INDLciObservations"
        Me.INDLciObservations.Size = New System.Drawing.Size(390, 118)
        Me.INDLciObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservations.Text = "Observaciones"
        Me.INDLciObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciObservations.TextToControlDistance = 5
        '
        'INDLciThirdParty
        '
        Me.INDLciThirdParty.Control = Me.INDSleThirdParty
        Me.INDLciThirdParty.Location = New System.Drawing.Point(0, 256)
        Me.INDLciThirdParty.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciThirdParty.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciThirdParty.Name = "INDLciThirdParty"
        Me.INDLciThirdParty.Size = New System.Drawing.Size(390, 64)
        Me.INDLciThirdParty.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciThirdParty.Text = "Tercero"
        Me.INDLciThirdParty.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciThirdParty.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciThirdParty.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciThirdParty.TextToControlDistance = 5
        '
        'INDLciDependency
        '
        Me.INDLciDependency.Control = Me.INDSleDependency
        Me.INDLciDependency.Location = New System.Drawing.Point(0, 320)
        Me.INDLciDependency.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciDependency.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciDependency.Name = "INDLciDependency"
        Me.INDLciDependency.Size = New System.Drawing.Size(390, 64)
        Me.INDLciDependency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDependency.Text = "Dependencia"
        Me.INDLciDependency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDependency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDependency.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDependency.TextToControlDistance = 5
        '
        'INDLcgGeneralInformation2
        '
        Me.INDLcgGeneralInformation2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralInformation2.AppearanceGroup.Options.UseFont = True
        Me.INDLcgGeneralInformation2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralInformation2.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgGeneralInformation2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation2.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgGeneralInformation2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgGeneralInformation2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgGeneralInformation2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgGeneralInformation2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgGeneralInformation2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgGeneralInformation2, False)
        Me.INDLcgGeneralInformation2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciValue, Me.INDLciAutomaticCollection})
        Me.INDLcgGeneralInformation2.Location = New System.Drawing.Point(828, 0)
        Me.INDLcgGeneralInformation2.Name = "INDLcgGeneralInformation2"
        Me.INDLcgGeneralInformation2.Size = New System.Drawing.Size(414, 561)
        Me.INDLcgGeneralInformation2.Text = "Información General"
        '
        'INDLciValue
        '
        Me.INDLciValue.Control = Me.INDTxeValue
        Me.INDLciValue.Location = New System.Drawing.Point(0, 64)
        Me.INDLciValue.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciValue.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciValue.Name = "INDLciValue"
        Me.INDLciValue.Size = New System.Drawing.Size(390, 438)
        Me.INDLciValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValue.Text = "Vr. Total Reconocimiento"
        Me.INDLciValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciValue.TextToControlDistance = 5
        '
        'INDLciAutomaticCollection
        '
        Me.INDLciAutomaticCollection.Control = Me.INDGleAutomaticCollection
        Me.INDLciAutomaticCollection.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAutomaticCollection.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticCollection.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticCollection.Name = "INDLciAutomaticCollection"
        Me.INDLciAutomaticCollection.Size = New System.Drawing.Size(390, 64)
        Me.INDLciAutomaticCollection.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAutomaticCollection.Text = "Recaudo Automático"
        Me.INDLciAutomaticCollection.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAutomaticCollection.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAutomaticCollection.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAutomaticCollection.TextToControlDistance = 5
        '
        'INDLcgCategory
        '
        Me.INDLcgCategory.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCategory.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCategory.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCategory.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCategory, False)
        Me.INDLcgCategory.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAdd, Me.LayoutControlItem1})
        Me.INDLcgCategory.Location = New System.Drawing.Point(1242, 0)
        Me.INDLcgCategory.Name = "INDLcgCategory"
        Me.INDLcgCategory.Size = New System.Drawing.Size(852, 561)
        Me.INDLcgCategory.Text = "Rubros"
        '
        'INDLciAdd
        '
        Me.INDLciAdd.Control = Me.INDSbAdd
        Me.INDLciAdd.CustomizationFormText = "Agregar Rubros"
        Me.INDLciAdd.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAdd.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.MinSize = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.Name = "INDLciAdd"
        Me.INDLciAdd.Size = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdd.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcCategory
        Me.LayoutControlItem1.CustomizationFormText = "Listado de Rubros"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 466)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'GridColumn39
        '
        Me.GridColumn39.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn39.Caption = "Selección"
        Me.GridColumn39.FieldName = "Item2"
        Me.GridColumn39.Name = "GridColumn39"
        '
        'GridColumn38
        '
        Me.GridColumn38.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn38.Caption = "Selección"
        Me.GridColumn38.FieldName = "Item2"
        Me.GridColumn38.Name = "GridColumn38"
        '
        'GridColumn37
        '
        Me.GridColumn37.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn37.Caption = "Selección"
        Me.GridColumn37.FieldName = "Item2"
        Me.GridColumn37.Name = "GridColumn37"
        '
        'GridColumn36
        '
        Me.GridColumn36.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn36.Caption = "Selección"
        Me.GridColumn36.FieldName = "Item2"
        Me.GridColumn36.Name = "GridColumn36"
        '
        'GridColumn35
        '
        Me.GridColumn35.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn35.Caption = "Selección"
        Me.GridColumn35.FieldName = "Item2"
        Me.GridColumn35.Name = "GridColumn35"
        '
        'GridColumn34
        '
        Me.GridColumn34.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn34.Caption = "Selección"
        Me.GridColumn34.FieldName = "Item2"
        Me.GridColumn34.Name = "GridColumn34"
        '
        'GridColumn33
        '
        Me.GridColumn33.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn33.Caption = "Selección"
        Me.GridColumn33.FieldName = "Item2"
        Me.GridColumn33.Name = "GridColumn33"
        '
        'GridColumn32
        '
        Me.GridColumn32.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn32.Caption = "Selección"
        Me.GridColumn32.FieldName = "Item2"
        Me.GridColumn32.Name = "GridColumn32"
        '
        'GridColumn31
        '
        Me.GridColumn31.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn31.Caption = "Selección"
        Me.GridColumn31.FieldName = "Item2"
        Me.GridColumn31.Name = "GridColumn31"
        '
        'GridColumn30
        '
        Me.GridColumn30.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn30.Caption = "Selección"
        Me.GridColumn30.FieldName = "Item2"
        Me.GridColumn30.Name = "GridColumn30"
        '
        'GridColumn29
        '
        Me.GridColumn29.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn29.Caption = "Selección"
        Me.GridColumn29.FieldName = "Item2"
        Me.GridColumn29.Name = "GridColumn29"
        '
        'GridColumn28
        '
        Me.GridColumn28.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn28.Caption = "Selección"
        Me.GridColumn28.FieldName = "Item2"
        Me.GridColumn28.Name = "GridColumn28"
        '
        'GridColumn27
        '
        Me.GridColumn27.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn27.Caption = "Selección"
        Me.GridColumn27.FieldName = "Item2"
        Me.GridColumn27.Name = "GridColumn27"
        '
        'GridColumn26
        '
        Me.GridColumn26.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn26.Caption = "Selección"
        Me.GridColumn26.FieldName = "Item2"
        Me.GridColumn26.Name = "GridColumn26"
        '
        'GridColumn25
        '
        Me.GridColumn25.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn25.Caption = "Selección"
        Me.GridColumn25.FieldName = "Item2"
        Me.GridColumn25.Name = "GridColumn25"
        '
        'GridColumn24
        '
        Me.GridColumn24.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn24.Caption = "Selección"
        Me.GridColumn24.FieldName = "Item2"
        Me.GridColumn24.Name = "GridColumn24"
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.Caption = "Selección"
        Me.GridColumn23.FieldName = "Item2"
        Me.GridColumn23.Name = "GridColumn23"
        '
        'GridColumn22
        '
        Me.GridColumn22.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn22.Caption = "Selección"
        Me.GridColumn22.FieldName = "Item2"
        Me.GridColumn22.Name = "GridColumn22"
        '
        'GridColumn21
        '
        Me.GridColumn21.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn21.Caption = "Selección"
        Me.GridColumn21.FieldName = "Item2"
        Me.GridColumn21.Name = "GridColumn21"
        '
        'GridColumn20
        '
        Me.GridColumn20.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn20.Caption = "Selección"
        Me.GridColumn20.FieldName = "Item2"
        Me.GridColumn20.Name = "GridColumn20"
        '
        'GridColumn19
        '
        Me.GridColumn19.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19.Caption = "Selección"
        Me.GridColumn19.FieldName = "Item2"
        Me.GridColumn19.Name = "GridColumn19"
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn18.Caption = "Selección"
        Me.GridColumn18.FieldName = "Item2"
        Me.GridColumn18.Name = "GridColumn18"
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn17.Caption = "Selección"
        Me.GridColumn17.FieldName = "Item2"
        Me.GridColumn17.Name = "GridColumn17"
        '
        'GridColumn16
        '
        Me.GridColumn16.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn16.Caption = "Selección"
        Me.GridColumn16.FieldName = "Item2"
        Me.GridColumn16.Name = "GridColumn16"
        '
        'GridColumn15
        '
        Me.GridColumn15.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn15.Caption = "Selección"
        Me.GridColumn15.FieldName = "Item2"
        Me.GridColumn15.Name = "GridColumn15"
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn14.Caption = "Selección"
        Me.GridColumn14.FieldName = "Item2"
        Me.GridColumn14.Name = "GridColumn14"
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn13.Caption = "Selección"
        Me.GridColumn13.FieldName = "Item2"
        Me.GridColumn13.Name = "GridColumn13"
        '
        'GridColumn12
        '
        Me.GridColumn12.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn12.Caption = "Selección"
        Me.GridColumn12.FieldName = "Item2"
        Me.GridColumn12.Name = "GridColumn12"
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn11.Caption = "Selección"
        Me.GridColumn11.FieldName = "Item2"
        Me.GridColumn11.Name = "GridColumn11"
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn10.Caption = "Selección"
        Me.GridColumn10.FieldName = "Item2"
        Me.GridColumn10.Name = "GridColumn10"
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn9.Caption = "Selección"
        Me.GridColumn9.FieldName = "Item2"
        Me.GridColumn9.Name = "GridColumn9"
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
        'INDGvValidityPopUp
        '
        Me.INDGvValidityPopUp.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvValidityPopUp.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvValidityPopUp.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvValidityPopUp.Appearance.Row.Options.UseFont = True
        Me.INDGvValidityPopUp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.INDgcStatusValidityPopUp, Me.GridColumn7, Me.GridColumn8})
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
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
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
        Me.LayoutControlGroup2.CustomizationFormText = "Datos Iniciales"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEntityPopUp, Me.INDlciValidityPopUp})
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlciEntityPopUp
        '
        Me.INDlciEntityPopUp.Control = Me.INDsleEntityPopUp
        Me.INDlciEntityPopUp.CustomizationFormText = "Entidad Presupuestal"
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
        Me.INDsleEntityPopUp.EnterMoveNextControl = True
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
        Me.INDsleEntityPopUp.Properties.PopupView = Me.GridView3
        Me.INDsleEntityPopUp.Properties.ShowFooter = False
        Me.INDsleEntityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityPopUp, True)
        Me.INDsleEntityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleEntityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleEntityPopUp.TabIndex = 16
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityPopUp, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityPopUp, False)
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDsleValidityPopUp)
        Me.LayoutControl2.Controls.Add(Me.INDsleEntityPopUp)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(584, 374, 250, 350)
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDsleValidityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidityPopUp, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidityPopUp, AppearanceObject8)
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
        Me.INDsleValidityPopUp.TabIndex = 17
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidityPopUp, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidityPopUp, False)
        '
        'INDlciValidityPopUp
        '
        Me.INDlciValidityPopUp.Control = Me.INDsleValidityPopUp
        Me.INDlciValidityPopUp.CustomizationFormText = "Vigencia"
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
        'INDpccChangeEntity
        '
        Me.INDpccChangeEntity.Controls.Add(Me.LayoutControl2)
        Me.INDpccChangeEntity.Location = New System.Drawing.Point(230, 422)
        Me.INDpccChangeEntity.Name = "INDpccChangeEntity"
        Me.INDpccChangeEntity.Size = New System.Drawing.Size(299, 150)
        Me.INDpccChangeEntity.TabIndex = 13
        '
        'GridColumn306
        '
        Me.GridColumn306.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn306.Caption = "Selección"
        Me.GridColumn306.FieldName = "Item2"
        Me.GridColumn306.Name = "GridColumn306"
        '
        'FrmRecognition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1316, 729)
        Me.Controls.Add(Me.INDpccChangeEntity)
        Me.Name = "FrmRecognition"
        Me.Opacity = 1.0R
        Me.Tag = "213"
        Me.Text = "Reconocimiento"
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDpccChangeEntity, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCnc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRecognition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRecognition.ResumeLayout(False)
        CType(Me.INDGleAutomaticCollection.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAutomaticCollection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDependency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxeValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMemObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxeDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleRecognitionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRecognition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRecognitionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDependency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgGeneralInformation2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAutomaticCollection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccChangeEntity.ResumeLayout(False)
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCnc As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcRecognition As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgRecognition As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleBudgetEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpccChangeEntity As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsleValidityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvValidityPopUp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidityPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciEntityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciValidityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResolutionName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResolutionValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcgGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDDteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleRecognitionType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciRecognitionType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxeDocument As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMemObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxeValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcgGeneralInformation2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcgCategory As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcCategory As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewBudget As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCodeCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResource As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBalance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleThirdParty As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciThirdParty As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleDependency As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDCodeDependency As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameDependency As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciDependency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn267 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn268 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn269 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn270 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn271 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn272 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleAutomaticCollection As Presentation.Controls.CtrYesNo
    Friend WithEvents CtrYesNo2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciAutomaticCollection As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn306 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
End Class
