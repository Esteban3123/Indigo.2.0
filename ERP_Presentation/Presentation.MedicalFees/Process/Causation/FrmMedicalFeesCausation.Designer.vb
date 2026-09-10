Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMedicalFeesCausation
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
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMedicalFeesCausation))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDcolOnlyNoQx = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolOnly = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDgcMedicalFeesCausation = New DevExpress.XtraGrid.GridControl()
        Me.ViewNoSurgical = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectOption = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtRateManualSalesPrice = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDcolHealthProfessional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleHealthProfessionalGridNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCausedValueNoSurgical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtCausedValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotalAmountPayable = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtSubTotalSalesPriceNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtPercentageDiscountNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtGrandTotalNoQx = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotalAmountPayableRealNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtPercentageCashedNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDcolColorNoSurgical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepColorNoSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyMedicalFeesCausation = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdteCausationDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDpopupInfo = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyPopupInfo = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtTotalCopayment = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtTotalEntityPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtBillPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtHealthAdministratorPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPatientPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtAdmissionNumberPopup = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAdmissionNumberPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPatientPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemHealthAdministratorPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBillPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTotalEntityPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTotalCopaymentPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPccMoreInfoAdmission = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtStay = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtResponsiblePhone = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtResponsibleName = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAuthorizationNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtEntity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtLiquidationType = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionPlace = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionType = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtBenefitsPlan = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAdmissionDate = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtPatient = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStay = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDpopupNotes = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnAddNote = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmemoNotes = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemNotes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpceNotes = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDgcNotes = New DevExpress.XtraGrid.GridControl()
        Me.viewNotes = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcMedicalFeesCausationSurgical = New DevExpress.XtraGrid.GridControl()
        Me.ViewSurgicalAndPackage = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCheckSelectOptionSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtRateManualSalesPriceSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDcolHealthProfessionalSurgical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepSleHealthProfessionalSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolCausedValueSurgical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtCausedValueSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotalAmountPayableSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtSubTotalSalesPriceSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtPercentageDiscountSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtGrandTotalQx = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotalAmountPayableRealSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtPercentageCashedSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepColorSurgical = New DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleInvoice = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSearchInvoice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtTotal = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleAdmission = New Presentation.Controls.CtrSearchLookUpEditWithPopUp()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAdmission = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInvoice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCausationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemMedicalFeesCausation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDetailsSurgical = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemMedicalFeesCausationSurgical = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygNotes = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridNotes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPceNotes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.xpViewServiceOrderDetail = New DevExpress.Xpo.XPView(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView3 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcMedicalFeesCausation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtRateManualSalesPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleHealthProfessionalGridNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtCausedValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotalAmountPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtSubTotalSalesPriceNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtPercentageDiscountNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtGrandTotalNoQx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotalAmountPayableRealNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtPercentageCashedNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepColorNoSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyMedicalFeesCausation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyMedicalFeesCausation.SuspendLayout()
        CType(Me.INDdteCausationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteCausationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopupInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupInfo.SuspendLayout()
        CType(Me.INDlyPopupInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPopupInfo.SuspendLayout()
        CType(Me.INDtxtTotalCopayment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTotalEntityPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtBillPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtHealthAdministratorPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPatientPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtAdmissionNumberPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAdmissionNumberPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPatientPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHealthAdministratorPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBillPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalEntityPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalCopaymentPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccMoreInfoAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccMoreInfoAdmission.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDTxtStay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionPlace.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtBenefitsPlan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopupNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupNotes.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDmemoNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceNotes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcMedicalFeesCausationSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewSurgicalAndPackage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCheckSelectOptionSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtRateManualSalesPriceSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepSleHealthProfessionalSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtCausedValueSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotalAmountPayableSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtSubTotalSalesPriceSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtPercentageDiscountSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtGrandTotalQx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotalAmountPayableRealSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtPercentageCashedSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepColorSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSearchInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAdmission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCausationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemMedicalFeesCausation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDetailsSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemMedicalFeesCausationSurgical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPceNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xpViewServiceOrderDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyMedicalFeesCausation)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1465, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 130)
        '
        'INDcolOnlyNoQx
        '
        Me.INDcolOnlyNoQx.Caption = "Agre. Manu."
        Me.INDcolOnlyNoQx.FieldName = "OnlyMedicalFees"
        Me.INDcolOnlyNoQx.Name = "INDcolOnlyNoQx"
        '
        'INDcolOnly
        '
        Me.INDcolOnly.Caption = "Agre. Manu."
        Me.INDcolOnly.FieldName = "OnlyMedicalFees"
        Me.INDcolOnly.Name = "INDcolOnly"
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'INDgcMedicalFeesCausation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcMedicalFeesCausation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcMedicalFeesCausation, Nothing)
        Me.INDgcMedicalFeesCausation.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcMedicalFeesCausation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcMedicalFeesCausation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcMedicalFeesCausation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcMedicalFeesCausation, False)
        Me.INDgcMedicalFeesCausation.Location = New System.Drawing.Point(42, 53)
        Me.INDgcMedicalFeesCausation.MainView = Me.ViewNoSurgical
        Me.INDgcMedicalFeesCausation.Name = "INDgcMedicalFeesCausation"
        Me.INDgcMedicalFeesCausation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtRateManualSalesPrice, Me.INDrepCheckSelectOption, Me.INDrepTxtCausedValue, Me.INDrepTxtTotalAmountPayable, Me.INDrepSleHealthProfessionalGridNoSurgical, Me.INDrepTxtSubTotalSalesPriceNoSurgical, Me.INDrepTxtPercentageDiscountNoSurgical, Me.INDrepTxtGrandTotalNoQx, Me.INDrepTxtTotalAmountPayableRealNoSurgical, Me.INDrepTxtPercentageCashedNoSurgical, Me.INDrepColorNoSurgical})
        Me.INDgcMedicalFeesCausation.Size = New System.Drawing.Size(824, 463)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcMedicalFeesCausation, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcMedicalFeesCausation.TabIndex = 3
        Me.INDgcMedicalFeesCausation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewNoSurgical})
        '
        'ViewNoSurgical
        '
        Me.ViewNoSurgical.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.ViewNoSurgical.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ViewNoSurgical.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewNoSurgical.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.ViewNoSurgical.Appearance.FocusedRow.Options.UseFont = True
        Me.ViewNoSurgical.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewNoSurgical.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewNoSurgical.Appearance.GroupRow.Options.UseFont = True
        Me.ViewNoSurgical.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewNoSurgical.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewNoSurgical.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ViewNoSurgical.Appearance.Row.Options.UseFont = True
        Me.ViewNoSurgical.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.ViewNoSurgical.Appearance.ViewCaption.Options.UseFont = True
        Me.ViewNoSurgical.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.INDcolHealthProfessional, Me.GridColumn38, Me.INDcolCausedValueNoSurgical, Me.GridColumn21, Me.GridColumn40, Me.GridColumn41, Me.GridColumn13, Me.GridColumn45, Me.GridColumn20, Me.GridColumn47, Me.GridColumn23, Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27, Me.GridColumn28, Me.INDcolOnlyNoQx, Me.GridColumn49, Me.GridColumn50, Me.INDcolColorNoSurgical, Me.GridColumn55})
        GridFormatRule2.ApplyToRow = True
        GridFormatRule2.Column = Me.INDcolOnlyNoQx
        GridFormatRule2.Name = "Format0"
        FormatConditionRuleValue2.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!)
        FormatConditionRuleValue2.Appearance.Options.UseFont = True
        FormatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue2.Value1 = True
        GridFormatRule2.Rule = FormatConditionRuleValue2
        Me.ViewNoSurgical.FormatRules.Add(GridFormatRule2)
        Me.ViewNoSurgical.GridControl = Me.INDgcMedicalFeesCausation
        Me.ViewNoSurgical.GroupCount = 1
        Me.ViewNoSurgical.Name = "ViewNoSurgical"
        Me.ViewNoSurgical.OptionsSelection.MultiSelect = True
        Me.ViewNoSurgical.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewNoSurgical.OptionsView.EnableAppearanceOddRow = True
        Me.ViewNoSurgical.OptionsView.ShowAutoFilterRow = True
        Me.ViewNoSurgical.OptionsView.ShowDetailButtons = False
        Me.ViewNoSurgical.OptionsView.ShowGroupPanel = False
        Me.ViewNoSurgical.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn7, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.ViewNoSurgical, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.ViewNoSurgical, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.ViewNoSurgical, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Seleccione"
        Me.GridColumn11.ColumnEdit = Me.INDrepCheckSelectOption
        Me.GridColumn11.FieldName = "SelectOption"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        Me.GridColumn11.Width = 41
        '
        'INDrepCheckSelectOption
        '
        Me.INDrepCheckSelectOption.AutoHeight = False
        Me.INDrepCheckSelectOption.Name = "INDrepCheckSelectOption"
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Concepto Facturación"
        Me.GridColumn7.FieldName = "BillingGroupDescription"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 250
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Servicio IPS"
        Me.GridColumn8.FieldName = "IPSServiceDescription"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 219
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Cant."
        Me.GridColumn9.FieldName = "InvoicedQuantity"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 2
        Me.GridColumn9.Width = 56
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Val. Unit."
        Me.GridColumn10.ColumnEdit = Me.INDrepTxtRateManualSalesPrice
        Me.GridColumn10.DisplayFormat.FormatString = "c0"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn10.FieldName = "TotalSalesPrice"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 3
        Me.GridColumn10.Width = 72
        '
        'INDrepTxtRateManualSalesPrice
        '
        Me.INDrepTxtRateManualSalesPrice.AutoHeight = False
        Me.INDrepTxtRateManualSalesPrice.Mask.EditMask = "C2"
        Me.INDrepTxtRateManualSalesPrice.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtRateManualSalesPrice.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtRateManualSalesPrice.Name = "INDrepTxtRateManualSalesPrice"
        '
        'INDcolHealthProfessional
        '
        Me.INDcolHealthProfessional.Caption = "Médico"
        Me.INDcolHealthProfessional.ColumnEdit = Me.INDrepSleHealthProfessionalGridNoSurgical
        Me.INDcolHealthProfessional.FieldName = "PerformsHealthProfessionalCode"
        Me.INDcolHealthProfessional.Name = "INDcolHealthProfessional"
        Me.INDcolHealthProfessional.Visible = True
        Me.INDcolHealthProfessional.VisibleIndex = 4
        Me.INDcolHealthProfessional.Width = 237
        '
        'INDrepSleHealthProfessionalGridNoSurgical
        '
        Me.INDrepSleHealthProfessionalGridNoSurgical.AutoHeight = False
        Me.INDrepSleHealthProfessionalGridNoSurgical.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleHealthProfessionalGridNoSurgical.DisplayMember = "CodeName"
        Me.INDrepSleHealthProfessionalGridNoSurgical.Name = "INDrepSleHealthProfessionalGridNoSurgical"
        Me.INDrepSleHealthProfessionalGridNoSurgical.NullText = ""
        Me.INDrepSleHealthProfessionalGridNoSurgical.PopupView = Me.RepositoryItemSearchLookUpEdit1View
        Me.INDrepSleHealthProfessionalGridNoSurgical.ValueMember = "CODPROSAL"
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn36, Me.GridColumn37})
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "Código"
        Me.GridColumn36.FieldName = "CODPROSAL"
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 0
        Me.GridColumn36.Width = 289
        '
        'GridColumn37
        '
        Me.GridColumn37.Caption = "Nombre"
        Me.GridColumn37.FieldName = "NOMMEDICO"
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.Visible = True
        Me.GridColumn37.VisibleIndex = 1
        Me.GridColumn37.Width = 1343
        '
        'GridColumn38
        '
        Me.GridColumn38.Caption = "Contrato"
        Me.GridColumn38.FieldName = "MedicalFeesContractCodeName"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.OptionsColumn.AllowEdit = False
        Me.GridColumn38.OptionsColumn.AllowFocus = False
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 5
        Me.GridColumn38.Width = 80
        '
        'INDcolCausedValueNoSurgical
        '
        Me.INDcolCausedValueNoSurgical.Caption = "Valor Causado"
        Me.INDcolCausedValueNoSurgical.ColumnEdit = Me.INDrepTxtCausedValue
        Me.INDcolCausedValueNoSurgical.DisplayFormat.FormatString = "c0"
        Me.INDcolCausedValueNoSurgical.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolCausedValueNoSurgical.FieldName = "AmountPayable"
        Me.INDcolCausedValueNoSurgical.Name = "INDcolCausedValueNoSurgical"
        Me.INDcolCausedValueNoSurgical.Visible = True
        Me.INDcolCausedValueNoSurgical.VisibleIndex = 6
        Me.INDcolCausedValueNoSurgical.Width = 184
        '
        'INDrepTxtCausedValue
        '
        Me.INDrepTxtCausedValue.AutoHeight = False
        Me.INDrepTxtCausedValue.Mask.EditMask = "C2"
        Me.INDrepTxtCausedValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtCausedValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtCausedValue.Name = "INDrepTxtCausedValue"
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Val. Total"
        Me.GridColumn21.ColumnEdit = Me.INDrepTxtTotalAmountPayable
        Me.GridColumn21.DisplayFormat.FormatString = "c0"
        Me.GridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn21.FieldName = "TotalAmountPayable"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 7
        Me.GridColumn21.Width = 101
        '
        'INDrepTxtTotalAmountPayable
        '
        Me.INDrepTxtTotalAmountPayable.AutoHeight = False
        Me.INDrepTxtTotalAmountPayable.Mask.EditMask = "C2"
        Me.INDrepTxtTotalAmountPayable.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotalAmountPayable.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotalAmountPayable.Name = "INDrepTxtTotalAmountPayable"
        '
        'GridColumn40
        '
        Me.GridColumn40.Caption = "Sub Val. Unit."
        Me.GridColumn40.ColumnEdit = Me.INDrepTxtSubTotalSalesPriceNoSurgical
        Me.GridColumn40.FieldName = "SubTotalSalesPrice"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.OptionsColumn.AllowEdit = False
        Me.GridColumn40.OptionsColumn.AllowFocus = False
        '
        'INDrepTxtSubTotalSalesPriceNoSurgical
        '
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.AutoHeight = False
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.Mask.EditMask = "C2"
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtSubTotalSalesPriceNoSurgical.Name = "INDrepTxtSubTotalSalesPriceNoSurgical"
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "% Descuento"
        Me.GridColumn41.ColumnEdit = Me.INDrepTxtPercentageDiscountNoSurgical
        Me.GridColumn41.FieldName = "ThirdPartyDiscountPercentage"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.OptionsColumn.AllowEdit = False
        Me.GridColumn41.OptionsColumn.AllowFocus = False
        '
        'INDrepTxtPercentageDiscountNoSurgical
        '
        Me.INDrepTxtPercentageDiscountNoSurgical.AutoHeight = False
        Me.INDrepTxtPercentageDiscountNoSurgical.Mask.EditMask = "P2"
        Me.INDrepTxtPercentageDiscountNoSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtPercentageDiscountNoSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtPercentageDiscountNoSurgical.Name = "INDrepTxtPercentageDiscountNoSurgical"
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Total Cobrado a Entidad"
        Me.GridColumn13.ColumnEdit = Me.INDrepTxtGrandTotalNoQx
        Me.GridColumn13.DisplayFormat.FormatString = "C0"
        Me.GridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn13.FieldName = "GrandTotalSalesPrice"
        Me.GridColumn13.Name = "GridColumn13"
        '
        'INDrepTxtGrandTotalNoQx
        '
        Me.INDrepTxtGrandTotalNoQx.AutoHeight = False
        Me.INDrepTxtGrandTotalNoQx.Mask.EditMask = "C0"
        Me.INDrepTxtGrandTotalNoQx.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtGrandTotalNoQx.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtGrandTotalNoQx.Name = "INDrepTxtGrandTotalNoQx"
        '
        'GridColumn45
        '
        Me.GridColumn45.Caption = "Unidad Funcional"
        Me.GridColumn45.FieldName = "FunctionalUnitDescription"
        Me.GridColumn45.Name = "GridColumn45"
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Fecha Servicio"
        Me.GridColumn20.FieldName = "ServiceDate"
        Me.GridColumn20.Name = "GridColumn20"
        '
        'GridColumn47
        '
        Me.GridColumn47.Caption = "Usuario Orden"
        Me.GridColumn47.FieldName = "CreationUserServiceOrder"
        Me.GridColumn47.Name = "GridColumn47"
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Usuario Creación"
        Me.GridColumn23.FieldName = "CreationUser"
        Me.GridColumn23.Name = "GridColumn23"
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Fecha Creación"
        Me.GridColumn24.FieldName = "CreationDate"
        Me.GridColumn24.Name = "GridColumn24"
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Usuario Modificación"
        Me.GridColumn25.FieldName = "ModificationUser"
        Me.GridColumn25.Name = "GridColumn25"
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Fecha Modificación"
        Me.GridColumn26.FieldName = "ModificationDate"
        Me.GridColumn26.Name = "GridColumn26"
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Usuario Confirmación"
        Me.GridColumn27.FieldName = "ConfirmationUser"
        Me.GridColumn27.Name = "GridColumn27"
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Fecha Confirmación"
        Me.GridColumn28.FieldName = "ConfirmationDate"
        Me.GridColumn28.Name = "GridColumn28"
        '
        'GridColumn49
        '
        Me.GridColumn49.Caption = "Valor Causado Real"
        Me.GridColumn49.ColumnEdit = Me.INDrepTxtTotalAmountPayableRealNoSurgical
        Me.GridColumn49.DisplayFormat.FormatString = "C0"
        Me.GridColumn49.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn49.FieldName = "TotalAmountPayableReal"
        Me.GridColumn49.Name = "GridColumn49"
        '
        'INDrepTxtTotalAmountPayableRealNoSurgical
        '
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.AutoHeight = False
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.Mask.EditMask = "C0"
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotalAmountPayableRealNoSurgical.Name = "INDrepTxtTotalAmountPayableRealNoSurgical"
        '
        'GridColumn50
        '
        Me.GridColumn50.Caption = "Porcentaje"
        Me.GridColumn50.ColumnEdit = Me.INDrepTxtPercentageCashedNoSurgical
        Me.GridColumn50.DisplayFormat.FormatString = "P2"
        Me.GridColumn50.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn50.FieldName = "PercentageCashed"
        Me.GridColumn50.Name = "GridColumn50"
        '
        'INDrepTxtPercentageCashedNoSurgical
        '
        Me.INDrepTxtPercentageCashedNoSurgical.AutoHeight = False
        Me.INDrepTxtPercentageCashedNoSurgical.Mask.EditMask = "P2"
        Me.INDrepTxtPercentageCashedNoSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtPercentageCashedNoSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtPercentageCashedNoSurgical.Name = "INDrepTxtPercentageCashedNoSurgical"
        '
        'INDcolColorNoSurgical
        '
        Me.INDcolColorNoSurgical.Caption = "Estado"
        Me.INDcolColorNoSurgical.ColumnEdit = Me.INDrepColorNoSurgical
        Me.INDcolColorNoSurgical.FieldName = "Color"
        Me.INDcolColorNoSurgical.Name = "INDcolColorNoSurgical"
        Me.INDcolColorNoSurgical.OptionsColumn.AllowEdit = False
        Me.INDcolColorNoSurgical.OptionsColumn.AllowFocus = False
        '
        'INDrepColorNoSurgical
        '
        Me.INDrepColorNoSurgical.AutoHeight = False
        Me.INDrepColorNoSurgical.AutomaticColor = System.Drawing.Color.Black
        Me.INDrepColorNoSurgical.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepColorNoSurgical.ColorAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepColorNoSurgical.Name = "INDrepColorNoSurgical"
        '
        'GridColumn55
        '
        Me.GridColumn55.Caption = "Fecha Causación"
        Me.GridColumn55.FieldName = "CausationDate"
        Me.GridColumn55.Name = "GridColumn55"
        Me.GridColumn55.OptionsColumn.AllowEdit = False
        Me.GridColumn55.OptionsColumn.AllowFocus = False
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyMedicalFeesCausation
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyMedicalFeesCausation
        '
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDdteCausationDate)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDpopupInfo)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDPccMoreInfoAdmission)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDpopupNotes)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDpceNotes)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDgcNotes)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDgcMedicalFeesCausationSurgical)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDsleInvoice)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDsleAdmission)
        Me.INDlyMedicalFeesCausation.Controls.Add(Me.INDgcMedicalFeesCausation)
        Me.INDlyMedicalFeesCausation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyMedicalFeesCausation.Location = New System.Drawing.Point(202, 7)
        Me.INDlyMedicalFeesCausation.Name = "INDlyMedicalFeesCausation"
        Me.INDlyMedicalFeesCausation.Root = Me.LayoutControlGroup1
        Me.INDlyMedicalFeesCausation.Size = New System.Drawing.Size(1261, 557)
        Me.INDlyMedicalFeesCausation.TabIndex = 1
        Me.INDlyMedicalFeesCausation.Text = "LayoutControl1"
        '
        'INDdteCausationDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteCausationDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteCausationDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteCausationDate, True)
        Me.INDdteCausationDate.EditValue = Nothing
        Me.INDdteCausationDate.Location = New System.Drawing.Point(-372, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteCausationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteCausationDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteCausationDate.Name = "INDdteCausationDate"
        Me.INDdteCausationDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdteCausationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteCausationDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdteCausationDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteCausationDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteCausationDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdteCausationDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteCausationDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteCausationDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteCausationDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdteCausationDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteCausationDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteCausationDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteCausationDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDdteCausationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteCausationDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteCausationDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteCausationDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteCausationDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteCausationDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteCausationDate.StyleController = Me.INDlyMedicalFeesCausation
        Me.INDdteCausationDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteCausationDate, 0)
        Me.INDdteCausationDate.ToolTip = "Este Campo es Necesario"
        '
        'INDpopupInfo
        '
        Me.INDpopupInfo.Controls.Add(Me.INDlyPopupInfo)
        Me.INDpopupInfo.Location = New System.Drawing.Point(1304, 425)
        Me.INDpopupInfo.Name = "INDpopupInfo"
        Me.INDpopupInfo.Size = New System.Drawing.Size(299, 338)
        Me.INDpopupInfo.TabIndex = 31
        '
        'INDlyPopupInfo
        '
        Me.INDlyPopupInfo.Controls.Add(Me.INDtxtTotalCopayment)
        Me.INDlyPopupInfo.Controls.Add(Me.INDtxtTotalEntityPopup)
        Me.INDlyPopupInfo.Controls.Add(Me.INDtxtBillPopup)
        Me.INDlyPopupInfo.Controls.Add(Me.INDtxtHealthAdministratorPopup)
        Me.INDlyPopupInfo.Controls.Add(Me.INDtxtPatientPopup)
        Me.INDlyPopupInfo.Controls.Add(Me.INDtxtAdmissionNumberPopup)
        Me.INDlyPopupInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyPopupInfo.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyPopupInfo.Location = New System.Drawing.Point(0, 0)
        Me.INDlyPopupInfo.Name = "INDlyPopupInfo"
        Me.INDlyPopupInfo.Root = Me.LayoutControlGroup3
        Me.INDlyPopupInfo.Size = New System.Drawing.Size(299, 338)
        Me.INDlyPopupInfo.TabIndex = 0
        Me.INDlyPopupInfo.Text = "LayoutControl3"
        '
        'INDtxtTotalCopayment
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalCopayment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalCopayment, False)
        Me.INDtxtTotalCopayment.Location = New System.Drawing.Point(12, 288)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalCopayment, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtTotalCopayment.Name = "INDtxtTotalCopayment"
        Me.INDtxtTotalCopayment.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalCopayment.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtTotalCopayment.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalCopayment.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtTotalCopayment.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalCopayment.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalCopayment.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalCopayment.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtTotalCopayment.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalCopayment.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtTotalCopayment.Properties.Mask.EditMask = "c0"
        Me.INDtxtTotalCopayment.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalCopayment.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalCopayment.Properties.NullText = "$0"
        Me.INDtxtTotalCopayment.Properties.ReadOnly = True
        Me.INDtxtTotalCopayment.Size = New System.Drawing.Size(275, 20)
        Me.INDtxtTotalCopayment.StyleController = Me.INDlyPopupInfo
        Me.INDtxtTotalCopayment.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalCopayment, 0)
        '
        'INDtxtTotalEntityPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalEntityPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalEntityPopup, False)
        Me.INDtxtTotalEntityPopup.Location = New System.Drawing.Point(12, 238)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalEntityPopup, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDtxtTotalEntityPopup.Name = "INDtxtTotalEntityPopup"
        Me.INDtxtTotalEntityPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalEntityPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtTotalEntityPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalEntityPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtTotalEntityPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalEntityPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalEntityPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalEntityPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtTotalEntityPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalEntityPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtTotalEntityPopup.Properties.Mask.EditMask = "c0"
        Me.INDtxtTotalEntityPopup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalEntityPopup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalEntityPopup.Properties.NullText = "$0"
        Me.INDtxtTotalEntityPopup.Properties.ReadOnly = True
        Me.INDtxtTotalEntityPopup.Size = New System.Drawing.Size(275, 20)
        Me.INDtxtTotalEntityPopup.StyleController = Me.INDlyPopupInfo
        Me.INDtxtTotalEntityPopup.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalEntityPopup, 0)
        '
        'INDtxtBillPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtBillPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtBillPopup, False)
        Me.INDtxtBillPopup.Location = New System.Drawing.Point(12, 188)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtBillPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtBillPopup.Name = "INDtxtBillPopup"
        Me.INDtxtBillPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtBillPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtBillPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtBillPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtBillPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtBillPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtBillPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtBillPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtBillPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtBillPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtBillPopup.Properties.ReadOnly = True
        Me.INDtxtBillPopup.Size = New System.Drawing.Size(275, 20)
        Me.INDtxtBillPopup.StyleController = Me.INDlyPopupInfo
        Me.INDtxtBillPopup.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtBillPopup, 0)
        '
        'INDtxtHealthAdministratorPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtHealthAdministratorPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtHealthAdministratorPopup, False)
        Me.INDtxtHealthAdministratorPopup.Location = New System.Drawing.Point(12, 138)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtHealthAdministratorPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtHealthAdministratorPopup.Name = "INDtxtHealthAdministratorPopup"
        Me.INDtxtHealthAdministratorPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtHealthAdministratorPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtHealthAdministratorPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtHealthAdministratorPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtHealthAdministratorPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtHealthAdministratorPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtHealthAdministratorPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtHealthAdministratorPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtHealthAdministratorPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtHealthAdministratorPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtHealthAdministratorPopup.Properties.ReadOnly = True
        Me.INDtxtHealthAdministratorPopup.Size = New System.Drawing.Size(275, 20)
        Me.INDtxtHealthAdministratorPopup.StyleController = Me.INDlyPopupInfo
        Me.INDtxtHealthAdministratorPopup.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtHealthAdministratorPopup, 0)
        '
        'INDtxtPatientPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPatientPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPatientPopup, False)
        Me.INDtxtPatientPopup.Location = New System.Drawing.Point(12, 88)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPatientPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPatientPopup.Name = "INDtxtPatientPopup"
        Me.INDtxtPatientPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtPatientPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtPatientPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPatientPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtPatientPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtPatientPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtPatientPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtPatientPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtPatientPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPatientPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtPatientPopup.Properties.ReadOnly = True
        Me.INDtxtPatientPopup.Size = New System.Drawing.Size(275, 20)
        Me.INDtxtPatientPopup.StyleController = Me.INDlyPopupInfo
        Me.INDtxtPatientPopup.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPatientPopup, 0)
        '
        'INDtxtAdmissionNumberPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtAdmissionNumberPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtAdmissionNumberPopup, False)
        Me.INDtxtAdmissionNumberPopup.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtAdmissionNumberPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtAdmissionNumberPopup.Name = "INDtxtAdmissionNumberPopup"
        Me.INDtxtAdmissionNumberPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtAdmissionNumberPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtAdmissionNumberPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtAdmissionNumberPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtAdmissionNumberPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtAdmissionNumberPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtAdmissionNumberPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtAdmissionNumberPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtxtAdmissionNumberPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtAdmissionNumberPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtAdmissionNumberPopup.Properties.ReadOnly = True
        Me.INDtxtAdmissionNumberPopup.Size = New System.Drawing.Size(275, 20)
        Me.INDtxtAdmissionNumberPopup.StyleController = Me.INDlyPopupInfo
        Me.INDtxtAdmissionNumberPopup.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtAdmissionNumberPopup, 0)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAdmissionNumberPopup, Me.INDlyItemPatientPopup, Me.INDlyItemHealthAdministratorPopup, Me.INDlyItemBillPopup, Me.INDlyItemTotalEntityPopup, Me.INDlyItemTotalCopaymentPopup})
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(299, 338)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'INDlyItemAdmissionNumberPopup
        '
        Me.INDlyItemAdmissionNumberPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemAdmissionNumberPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemAdmissionNumberPopup.Control = Me.INDtxtAdmissionNumberPopup
        Me.INDlyItemAdmissionNumberPopup.CustomizationFormText = "No. Ingreso"
        Me.INDlyItemAdmissionNumberPopup.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAdmissionNumberPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemAdmissionNumberPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemAdmissionNumberPopup.Name = "INDlyItemAdmissionNumberPopup"
        Me.INDlyItemAdmissionNumberPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemAdmissionNumberPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAdmissionNumberPopup.Text = "No. Ingreso"
        Me.INDlyItemAdmissionNumberPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAdmissionNumberPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAdmissionNumberPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAdmissionNumberPopup.TextToControlDistance = 5
        '
        'INDlyItemPatientPopup
        '
        Me.INDlyItemPatientPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemPatientPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemPatientPopup.Control = Me.INDtxtPatientPopup
        Me.INDlyItemPatientPopup.CustomizationFormText = "Paciente"
        Me.INDlyItemPatientPopup.Location = New System.Drawing.Point(0, 50)
        Me.INDlyItemPatientPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemPatientPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemPatientPopup.Name = "INDlyItemPatientPopup"
        Me.INDlyItemPatientPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemPatientPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPatientPopup.Text = "Paciente"
        Me.INDlyItemPatientPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPatientPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPatientPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPatientPopup.TextToControlDistance = 5
        '
        'INDlyItemHealthAdministratorPopup
        '
        Me.INDlyItemHealthAdministratorPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemHealthAdministratorPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemHealthAdministratorPopup.Control = Me.INDtxtHealthAdministratorPopup
        Me.INDlyItemHealthAdministratorPopup.CustomizationFormText = "Entidad Paciente"
        Me.INDlyItemHealthAdministratorPopup.Location = New System.Drawing.Point(0, 100)
        Me.INDlyItemHealthAdministratorPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemHealthAdministratorPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemHealthAdministratorPopup.Name = "INDlyItemHealthAdministratorPopup"
        Me.INDlyItemHealthAdministratorPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemHealthAdministratorPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemHealthAdministratorPopup.Text = "Entidad Paciente"
        Me.INDlyItemHealthAdministratorPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemHealthAdministratorPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemHealthAdministratorPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemHealthAdministratorPopup.TextToControlDistance = 5
        '
        'INDlyItemBillPopup
        '
        Me.INDlyItemBillPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemBillPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemBillPopup.Control = Me.INDtxtBillPopup
        Me.INDlyItemBillPopup.CustomizationFormText = "Factura"
        Me.INDlyItemBillPopup.Location = New System.Drawing.Point(0, 150)
        Me.INDlyItemBillPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemBillPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemBillPopup.Name = "INDlyItemBillPopup"
        Me.INDlyItemBillPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemBillPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBillPopup.Text = "Factura"
        Me.INDlyItemBillPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemBillPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemBillPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemBillPopup.TextToControlDistance = 5
        '
        'INDlyItemTotalEntityPopup
        '
        Me.INDlyItemTotalEntityPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemTotalEntityPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemTotalEntityPopup.Control = Me.INDtxtTotalEntityPopup
        Me.INDlyItemTotalEntityPopup.CustomizationFormText = "Total a Entidad"
        Me.INDlyItemTotalEntityPopup.Location = New System.Drawing.Point(0, 200)
        Me.INDlyItemTotalEntityPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemTotalEntityPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemTotalEntityPopup.Name = "INDlyItemTotalEntityPopup"
        Me.INDlyItemTotalEntityPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemTotalEntityPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalEntityPopup.Text = "Total a Entidad"
        Me.INDlyItemTotalEntityPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTotalEntityPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTotalEntityPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTotalEntityPopup.TextToControlDistance = 5
        '
        'INDlyItemTotalCopaymentPopup
        '
        Me.INDlyItemTotalCopaymentPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemTotalCopaymentPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemTotalCopaymentPopup.Control = Me.INDtxtTotalCopayment
        Me.INDlyItemTotalCopaymentPopup.CustomizationFormText = "Total a Copago"
        Me.INDlyItemTotalCopaymentPopup.Location = New System.Drawing.Point(0, 250)
        Me.INDlyItemTotalCopaymentPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemTotalCopaymentPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemTotalCopaymentPopup.Name = "INDlyItemTotalCopaymentPopup"
        Me.INDlyItemTotalCopaymentPopup.Size = New System.Drawing.Size(279, 68)
        Me.INDlyItemTotalCopaymentPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalCopaymentPopup.Text = "Total a Copago"
        Me.INDlyItemTotalCopaymentPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTotalCopaymentPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTotalCopaymentPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTotalCopaymentPopup.TextToControlDistance = 5
        '
        'INDPccMoreInfoAdmission
        '
        Me.INDPccMoreInfoAdmission.Controls.Add(Me.LayoutControl1)
        Me.INDPccMoreInfoAdmission.Location = New System.Drawing.Point(14, 524)
        Me.INDPccMoreInfoAdmission.Name = "INDPccMoreInfoAdmission"
        Me.INDPccMoreInfoAdmission.Size = New System.Drawing.Size(714, 305)
        Me.INDPccMoreInfoAdmission.TabIndex = 23
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDTxtStay)
        Me.LayoutControl1.Controls.Add(Me.INDTxtAdmissionCode)
        Me.LayoutControl1.Controls.Add(Me.INDTxtResponsiblePhone)
        Me.LayoutControl1.Controls.Add(Me.INDTxtResponsibleName)
        Me.LayoutControl1.Controls.Add(Me.INDTxtAuthorizationNumber)
        Me.LayoutControl1.Controls.Add(Me.INDTxtEntity)
        Me.LayoutControl1.Controls.Add(Me.INDTxtLiquidationType)
        Me.LayoutControl1.Controls.Add(Me.INDTxtAdmissionPlace)
        Me.LayoutControl1.Controls.Add(Me.INDTxtAdmissionType)
        Me.LayoutControl1.Controls.Add(Me.INDTxtBenefitsPlan)
        Me.LayoutControl1.Controls.Add(Me.INDTxtAdmissionDate)
        Me.LayoutControl1.Controls.Add(Me.INDTxtPatient)
        Me.LayoutControl1.Controls.Add(Me.LabelControl1)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup5
        Me.LayoutControl1.Size = New System.Drawing.Size(714, 305)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDTxtStay
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtStay, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtStay, False)
        Me.INDTxtStay.Location = New System.Drawing.Point(144, 115)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtStay, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtStay.Name = "INDTxtStay"
        Me.INDTxtStay.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtStay.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtStay.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtStay.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtStay.Properties.Appearance.Options.UseFont = True
        Me.INDTxtStay.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtStay.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtStay.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtStay.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtStay.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtStay.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtStay.Properties.ReadOnly = True
        Me.INDTxtStay.Size = New System.Drawing.Size(207, 24)
        Me.INDTxtStay.StyleController = Me.LayoutControl1
        Me.INDTxtStay.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtStay, 0)
        '
        'INDTxtAdmissionCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionCode, False)
        Me.INDTxtAdmissionCode.Location = New System.Drawing.Point(485, 115)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionCode.Name = "INDTxtAdmissionCode"
        Me.INDTxtAdmissionCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionCode.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionCode.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtAdmissionCode.Properties.ReadOnly = True
        Me.INDTxtAdmissionCode.Size = New System.Drawing.Size(215, 24)
        Me.INDTxtAdmissionCode.StyleController = Me.LayoutControl1
        Me.INDTxtAdmissionCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionCode, 0)
        '
        'INDTxtResponsiblePhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtResponsiblePhone, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtResponsiblePhone, False)
        Me.INDTxtResponsiblePhone.Location = New System.Drawing.Point(144, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtResponsiblePhone, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtResponsiblePhone.Name = "INDTxtResponsiblePhone"
        Me.INDTxtResponsiblePhone.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsiblePhone.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtResponsiblePhone.Properties.Appearance.Options.UseFont = True
        Me.INDTxtResponsiblePhone.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtResponsiblePhone.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtResponsiblePhone.Properties.ReadOnly = True
        Me.INDTxtResponsiblePhone.Size = New System.Drawing.Size(210, 24)
        Me.INDTxtResponsiblePhone.StyleController = Me.LayoutControl1
        Me.INDTxtResponsiblePhone.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtResponsiblePhone, 0)
        '
        'INDTxtResponsibleName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtResponsibleName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtResponsibleName, False)
        Me.INDTxtResponsibleName.Location = New System.Drawing.Point(488, 235)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtResponsibleName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtResponsibleName.Name = "INDTxtResponsibleName"
        Me.INDTxtResponsibleName.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsibleName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtResponsibleName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtResponsibleName.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtResponsibleName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtResponsibleName.Properties.ReadOnly = True
        Me.INDTxtResponsibleName.Size = New System.Drawing.Size(212, 24)
        Me.INDTxtResponsibleName.StyleController = Me.LayoutControl1
        Me.INDTxtResponsibleName.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtResponsibleName, 0)
        '
        'INDTxtAuthorizationNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAuthorizationNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAuthorizationNumber, False)
        Me.INDTxtAuthorizationNumber.Location = New System.Drawing.Point(144, 235)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAuthorizationNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAuthorizationNumber.Name = "INDTxtAuthorizationNumber"
        Me.INDTxtAuthorizationNumber.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAuthorizationNumber.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAuthorizationNumber.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtAuthorizationNumber.Properties.ReadOnly = True
        Me.INDTxtAuthorizationNumber.Size = New System.Drawing.Size(210, 24)
        Me.INDTxtAuthorizationNumber.StyleController = Me.LayoutControl1
        Me.INDTxtAuthorizationNumber.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAuthorizationNumber, 0)
        '
        'INDTxtEntity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtEntity, False)
        Me.INDTxtEntity.Location = New System.Drawing.Point(144, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtEntity.Name = "INDTxtEntity"
        Me.INDTxtEntity.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtEntity.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtEntity.Properties.Appearance.Options.UseFont = True
        Me.INDTxtEntity.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtEntity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtEntity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtEntity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtEntity.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtEntity.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtEntity.Properties.ReadOnly = True
        Me.INDTxtEntity.Size = New System.Drawing.Size(210, 24)
        Me.INDTxtEntity.StyleController = Me.LayoutControl1
        Me.INDTxtEntity.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtEntity, 0)
        '
        'INDTxtLiquidationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtLiquidationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtLiquidationType, False)
        Me.INDTxtLiquidationType.Location = New System.Drawing.Point(485, 175)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtLiquidationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtLiquidationType.Name = "INDTxtLiquidationType"
        Me.INDTxtLiquidationType.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtLiquidationType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtLiquidationType.Properties.Appearance.Options.UseFont = True
        Me.INDTxtLiquidationType.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtLiquidationType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtLiquidationType.Properties.ReadOnly = True
        Me.INDTxtLiquidationType.Size = New System.Drawing.Size(215, 24)
        Me.INDTxtLiquidationType.StyleController = Me.LayoutControl1
        Me.INDTxtLiquidationType.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtLiquidationType, 0)
        '
        'INDTxtAdmissionPlace
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionPlace, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionPlace, False)
        Me.INDTxtAdmissionPlace.Location = New System.Drawing.Point(144, 175)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionPlace, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionPlace.Name = "INDTxtAdmissionPlace"
        Me.INDTxtAdmissionPlace.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionPlace.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionPlace.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionPlace.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionPlace.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtAdmissionPlace.Properties.ReadOnly = True
        Me.INDTxtAdmissionPlace.Size = New System.Drawing.Size(207, 24)
        Me.INDTxtAdmissionPlace.StyleController = Me.LayoutControl1
        Me.INDTxtAdmissionPlace.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionPlace, 0)
        '
        'INDTxtAdmissionType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionType, False)
        Me.INDTxtAdmissionType.Location = New System.Drawing.Point(485, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionType.Name = "INDTxtAdmissionType"
        Me.INDTxtAdmissionType.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionType.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionType.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtAdmissionType.Properties.ReadOnly = True
        Me.INDTxtAdmissionType.Size = New System.Drawing.Size(215, 24)
        Me.INDTxtAdmissionType.StyleController = Me.LayoutControl1
        Me.INDTxtAdmissionType.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionType, 0)
        '
        'INDTxtBenefitsPlan
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtBenefitsPlan, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtBenefitsPlan, False)
        Me.INDTxtBenefitsPlan.Location = New System.Drawing.Point(488, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtBenefitsPlan, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtBenefitsPlan.Name = "INDTxtBenefitsPlan"
        Me.INDTxtBenefitsPlan.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtBenefitsPlan.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtBenefitsPlan.Properties.Appearance.Options.UseFont = True
        Me.INDTxtBenefitsPlan.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtBenefitsPlan.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtBenefitsPlan.Properties.ReadOnly = True
        Me.INDTxtBenefitsPlan.Size = New System.Drawing.Size(212, 24)
        Me.INDTxtBenefitsPlan.StyleController = Me.LayoutControl1
        Me.INDTxtBenefitsPlan.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtBenefitsPlan, 0)
        '
        'INDTxtAdmissionDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAdmissionDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAdmissionDate, False)
        Me.INDTxtAdmissionDate.Location = New System.Drawing.Point(144, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAdmissionDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtAdmissionDate.Name = "INDTxtAdmissionDate"
        Me.INDTxtAdmissionDate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionDate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAdmissionDate.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAdmissionDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAdmissionDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtAdmissionDate.Properties.ReadOnly = True
        Me.INDTxtAdmissionDate.Size = New System.Drawing.Size(207, 24)
        Me.INDTxtAdmissionDate.StyleController = Me.LayoutControl1
        Me.INDTxtAdmissionDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAdmissionDate, 0)
        '
        'INDTxtPatient
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPatient, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPatient, False)
        Me.INDTxtPatient.Location = New System.Drawing.Point(144, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPatient, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtPatient.Name = "INDTxtPatient"
        Me.INDTxtPatient.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtPatient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtPatient.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtPatient.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPatient.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPatient.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtPatient.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtPatient.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtPatient.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDTxtPatient.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPatient.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtPatient.Properties.ReadOnly = True
        Me.INDTxtPatient.Size = New System.Drawing.Size(556, 24)
        Me.INDTxtPatient.StyleController = Me.LayoutControl1
        Me.INDTxtPatient.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPatient, 0)
        '
        'LabelControl1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl1, True)
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl1.Appearance.Options.UseBackColor = True
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl1, False)
        Me.LabelControl1.Location = New System.Drawing.Point(0, 0)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Padding = New System.Windows.Forms.Padding(14, 0, 0, 0)
        Me.LabelControl1.Size = New System.Drawing.Size(714, 40)
        Me.LabelControl1.StyleController = Me.LayoutControl1
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Más Información"
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup5, False)
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup5.GroupBordersVisible = False
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.TabbedControlGroup1})
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(714, 305)
        Me.LayoutControlGroup5.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.LabelControl1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(1, 40)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(714, 40)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.CustomizationFormText = "TabbedControlGroup1"
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 40)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LayoutControlGroup6
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(714, 265)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6})
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup6, False)
        Me.LayoutControlGroup6.CustomizationFormText = "Datos del Ingreso"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem14, Me.LayoutControlItem11, Me.LayoutControlItem13, Me.LayoutControlItem9, Me.LayoutControlItem16, Me.LayoutControlItem5, Me.LayoutControlItem10, Me.INDLciStay, Me.LayoutControlItem2, Me.LayoutControlItem19, Me.LayoutControlItem8, Me.LayoutControlItem15, Me.EmptySpaceItem1})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(690, 210)
        Me.LayoutControlGroup6.Text = "Datos del Ingreso"
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem14.Control = Me.INDTxtAuthorizationNumber
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 150)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(344, 30)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.Text = "Nº Autorización"
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem14.TextToControlDistance = 12
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem11.Control = Me.INDTxtLiquidationType
        Me.LayoutControlItem11.CustomizationFormText = "Tipo de Liquidación"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(341, 90)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.Text = "Tipo de Liquidación"
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem11.TextToControlDistance = 12
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem13.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem13.Control = Me.INDTxtEntity
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(344, 30)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.Text = "Entidad Admin. Salud"
        Me.LayoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem13.TextToControlDistance = 12
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem9.Control = Me.INDTxtAdmissionType
        Me.LayoutControlItem9.CustomizationFormText = "Tipo de Ingreso"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(341, 60)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "Tipo de Ingreso"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem9.TextToControlDistance = 12
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem16.Control = Me.INDTxtResponsiblePhone
        Me.LayoutControlItem16.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(344, 30)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.Text = "Telefono Acudiente"
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem16.TextToControlDistance = 12
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.Control = Me.INDTxtAdmissionDate
        Me.LayoutControlItem5.CustomizationFormText = "Fecha Ingreso"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(341, 30)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Fecha Ingreso"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem5.TextToControlDistance = 12
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem10.Control = Me.INDTxtAdmissionPlace
        Me.LayoutControlItem10.CustomizationFormText = "Lugar Ingreso"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(341, 30)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.Text = "Lugar Ingreso"
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem10.TextToControlDistance = 12
        '
        'INDLciStay
        '
        Me.INDLciStay.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLciStay.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciStay.Control = Me.INDTxtStay
        Me.INDLciStay.CustomizationFormText = "Estancia (Cama)"
        Me.INDLciStay.Location = New System.Drawing.Point(0, 30)
        Me.INDLciStay.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDLciStay.MinSize = New System.Drawing.Size(187, 30)
        Me.INDLciStay.Name = "INDLciStay"
        Me.INDLciStay.Size = New System.Drawing.Size(341, 30)
        Me.INDLciStay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStay.Text = "Estancia (Cama)"
        Me.INDLciStay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciStay.TextSize = New System.Drawing.Size(118, 21)
        Me.INDLciStay.TextToControlDistance = 12
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.Control = Me.INDTxtPatient
        Me.LayoutControlItem2.CustomizationFormText = "Paciente"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(690, 30)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Paciente"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem2.TextToControlDistance = 12
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem19.Control = Me.INDTxtAdmissionCode
        Me.LayoutControlItem19.CustomizationFormText = "Nº Ingreso"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(341, 30)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(349, 30)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.Text = "Nº Ingreso"
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem19.TextToControlDistance = 12
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.INDTxtBenefitsPlan
        Me.LayoutControlItem8.CustomizationFormText = "Plan de Beneficios"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(344, 120)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(346, 30)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Grupo Atención"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem8.TextToControlDistance = 12
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.LayoutControlItem15.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem15.Control = Me.INDTxtResponsibleName
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(344, 150)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(0, 30)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(187, 30)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(346, 30)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.Text = "Nombre Acudiente"
        Me.LayoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(118, 21)
        Me.LayoutControlItem15.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(344, 180)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(346, 30)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDpopupNotes
        '
        Me.INDpopupNotes.Controls.Add(Me.LayoutControl2)
        Me.INDpopupNotes.Location = New System.Drawing.Point(739, 496)
        Me.INDpopupNotes.Name = "INDpopupNotes"
        Me.INDpopupNotes.Size = New System.Drawing.Size(522, 143)
        Me.INDpopupNotes.TabIndex = 30
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDbtnAddNote)
        Me.LayoutControl2.Controls.Add(Me.INDmemoNotes)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(522, 143)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDbtnAddNote
        '
        Me.INDbtnAddNote.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddNote.Appearance.Options.UseFont = True
        Me.INDbtnAddNote.Location = New System.Drawing.Point(12, 92)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddNote, True)
        Me.INDbtnAddNote.Name = "INDbtnAddNote"
        Me.INDbtnAddNote.Size = New System.Drawing.Size(496, 32)
        Me.INDbtnAddNote.StyleController = Me.LayoutControl2
        Me.INDbtnAddNote.TabIndex = 1
        Me.INDbtnAddNote.Text = "Agregar"
        '
        'INDmemoNotes
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoNotes, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoNotes, False)
        Me.INDmemoNotes.EnterMoveNextControl = True
        Me.INDmemoNotes.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoNotes, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoNotes.Name = "INDmemoNotes"
        Me.INDmemoNotes.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmemoNotes.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoNotes.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDmemoNotes.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoNotes.Properties.Appearance.Options.UseFont = True
        Me.INDmemoNotes.Properties.Appearance.Options.UseForeColor = True
        Me.INDmemoNotes.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmemoNotes.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmemoNotes.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoNotes.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDmemoNotes.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmemoNotes.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmemoNotes.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoNotes.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDmemoNotes.Size = New System.Drawing.Size(496, 50)
        Me.INDmemoNotes.StyleController = Me.LayoutControl2
        Me.INDmemoNotes.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoNotes, 0)
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemNotes, Me.LayoutControlItem3})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(522, 143)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemNotes
        '
        Me.INDlyItemNotes.Control = Me.INDmemoNotes
        Me.INDlyItemNotes.CustomizationFormText = "Nota"
        Me.INDlyItemNotes.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemNotes.MaxSize = New System.Drawing.Size(500, 80)
        Me.INDlyItemNotes.MinSize = New System.Drawing.Size(500, 80)
        Me.INDlyItemNotes.Name = "INDlyItemNotes"
        Me.INDlyItemNotes.Size = New System.Drawing.Size(502, 80)
        Me.INDlyItemNotes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemNotes.Text = "Nota"
        Me.INDlyItemNotes.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemNotes.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemNotes.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemNotes.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDbtnAddNote
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 80)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(500, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(500, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(502, 43)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDpceNotes
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceNotes, True)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceNotes, Nothing)
        Me.INDpceNotes.Location = New System.Drawing.Point(1746, 53)
        Me.INDpceNotes.MinimumSize = New System.Drawing.Size(518, 32)
        Me.INDpceNotes.Name = "INDpceNotes"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceNotes, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceNotes, False)
        Me.INDpceNotes.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceNotes.Properties.Appearance.Options.UseFont = True
        Me.INDpceNotes.Properties.AutoHeight = False
        Me.INDpceNotes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDpceNotes.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDpceNotes.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceNotes.Properties.PopupControl = Me.INDpopupNotes
        Me.INDpceNotes.Properties.PopupSizeable = False
        Me.INDpceNotes.Properties.ShowPopupCloseButton = False
        Me.INDpceNotes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceNotes.Size = New System.Drawing.Size(518, 32)
        Me.INDpceNotes.StyleController = Me.INDlyMedicalFeesCausation
        Me.INDpceNotes.TabIndex = 5
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceNotes, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceNotes, Nothing)
        '
        'INDgcNotes
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcNotes, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcNotes, Nothing)
        Me.INDgcNotes.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcNotes, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcNotes, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcNotes, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcNotes, False)
        Me.INDgcNotes.Location = New System.Drawing.Point(1746, 89)
        Me.INDgcNotes.MainView = Me.viewNotes
        Me.INDgcNotes.Name = "INDgcNotes"
        Me.INDgcNotes.Size = New System.Drawing.Size(518, 427)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcNotes, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcNotes.TabIndex = 6
        Me.INDgcNotes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewNotes})
        '
        'viewNotes
        '
        Me.viewNotes.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewNotes.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewNotes.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewNotes.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewNotes.Appearance.FocusedRow.Options.UseFont = True
        Me.viewNotes.Appearance.FocusedRow.Options.UseForeColor = True
        Me.viewNotes.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewNotes.Appearance.GroupRow.Options.UseFont = True
        Me.viewNotes.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewNotes.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewNotes.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewNotes.Appearance.Row.Options.UseFont = True
        Me.viewNotes.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewNotes.Appearance.ViewCaption.Options.UseFont = True
        Me.viewNotes.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolNote})
        Me.viewNotes.GridControl = Me.INDgcNotes
        Me.viewNotes.Name = "viewNotes"
        Me.viewNotes.OptionsView.EnableAppearanceEvenRow = True
        Me.viewNotes.OptionsView.EnableAppearanceOddRow = True
        Me.viewNotes.OptionsView.RowAutoHeight = True
        Me.viewNotes.OptionsView.ShowAutoFilterRow = True
        Me.viewNotes.OptionsView.ShowDetailButtons = False
        Me.viewNotes.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.viewNotes, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.viewNotes, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewNotes, False)
        '
        'INDcolNote
        '
        Me.INDcolNote.Caption = "Nota"
        Me.INDcolNote.FieldName = "Note"
        Me.INDcolNote.Name = "INDcolNote"
        Me.INDcolNote.OptionsColumn.AllowEdit = False
        Me.INDcolNote.OptionsColumn.AllowFocus = False
        Me.INDcolNote.Visible = True
        Me.INDcolNote.VisibleIndex = 0
        '
        'INDgcMedicalFeesCausationSurgical
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcMedicalFeesCausationSurgical, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcMedicalFeesCausationSurgical, Nothing)
        Me.INDgcMedicalFeesCausationSurgical.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcMedicalFeesCausationSurgical, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcMedicalFeesCausationSurgical, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcMedicalFeesCausationSurgical, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcMedicalFeesCausationSurgical, False)
        Me.INDgcMedicalFeesCausationSurgical.Location = New System.Drawing.Point(894, 53)
        Me.INDgcMedicalFeesCausationSurgical.MainView = Me.ViewSurgicalAndPackage
        Me.INDgcMedicalFeesCausationSurgical.Name = "INDgcMedicalFeesCausationSurgical"
        Me.INDgcMedicalFeesCausationSurgical.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtRateManualSalesPriceSurgical, Me.INDrepTxtCausedValueSurgical, Me.INDrepCheckSelectOptionSurgical, Me.INDrepTxtTotalAmountPayableSurgical, Me.INDrepSleHealthProfessionalSurgical, Me.INDrepTxtSubTotalSalesPriceSurgical, Me.INDrepTxtPercentageDiscountSurgical, Me.INDrepTxtGrandTotalQx, Me.INDrepTxtTotalAmountPayableRealSurgical, Me.INDrepTxtPercentageCashedSurgical, Me.INDrepColorSurgical})
        Me.INDgcMedicalFeesCausationSurgical.Size = New System.Drawing.Size(824, 463)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcMedicalFeesCausationSurgical, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcMedicalFeesCausationSurgical.TabIndex = 4
        Me.INDgcMedicalFeesCausationSurgical.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ViewSurgicalAndPackage})
        '
        'ViewSurgicalAndPackage
        '
        Me.ViewSurgicalAndPackage.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.ViewSurgicalAndPackage.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ViewSurgicalAndPackage.Appearance.FocusedRow.Options.UseBackColor = True
        Me.ViewSurgicalAndPackage.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.ViewSurgicalAndPackage.Appearance.FocusedRow.Options.UseFont = True
        Me.ViewSurgicalAndPackage.Appearance.FocusedRow.Options.UseForeColor = True
        Me.ViewSurgicalAndPackage.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewSurgicalAndPackage.Appearance.GroupRow.Options.UseFont = True
        Me.ViewSurgicalAndPackage.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewSurgicalAndPackage.Appearance.HeaderPanel.Options.UseFont = True
        Me.ViewSurgicalAndPackage.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ViewSurgicalAndPackage.Appearance.Row.Options.UseFont = True
        Me.ViewSurgicalAndPackage.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.ViewSurgicalAndPackage.Appearance.ViewCaption.Options.UseFont = True
        Me.ViewSurgicalAndPackage.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18, Me.INDcolHealthProfessionalSurgical, Me.GridColumn39, Me.INDcolCausedValueSurgical, Me.GridColumn22, Me.GridColumn42, Me.GridColumn43, Me.GridColumn44, Me.GridColumn46, Me.GridColumn35, Me.GridColumn48, Me.GridColumn29, Me.GridColumn30, Me.GridColumn31, Me.GridColumn32, Me.GridColumn33, Me.GridColumn34, Me.INDcolOnly, Me.GridColumn51, Me.GridColumn52, Me.GridColumn54, Me.GridColumn56})
        GridFormatRule1.ApplyToRow = True
        GridFormatRule1.Column = Me.INDcolOnly
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!)
        FormatConditionRuleValue1.Appearance.Options.UseFont = True
        FormatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue1.Value1 = True
        GridFormatRule1.Rule = FormatConditionRuleValue1
        Me.ViewSurgicalAndPackage.FormatRules.Add(GridFormatRule1)
        Me.ViewSurgicalAndPackage.GridControl = Me.INDgcMedicalFeesCausationSurgical
        Me.ViewSurgicalAndPackage.GroupCount = 1
        Me.ViewSurgicalAndPackage.Name = "ViewSurgicalAndPackage"
        Me.ViewSurgicalAndPackage.OptionsSelection.MultiSelect = True
        Me.ViewSurgicalAndPackage.OptionsView.EnableAppearanceEvenRow = True
        Me.ViewSurgicalAndPackage.OptionsView.EnableAppearanceOddRow = True
        Me.ViewSurgicalAndPackage.OptionsView.ShowAutoFilterRow = True
        Me.ViewSurgicalAndPackage.OptionsView.ShowDetailButtons = False
        Me.ViewSurgicalAndPackage.OptionsView.ShowGroupPanel = False
        Me.ViewSurgicalAndPackage.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn15, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.ViewSurgicalAndPackage, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.ViewSurgicalAndPackage, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.ViewSurgicalAndPackage, False)
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Seleccione"
        Me.GridColumn14.ColumnEdit = Me.INDrepCheckSelectOptionSurgical
        Me.GridColumn14.FieldName = "SelectOption"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 0
        Me.GridColumn14.Width = 41
        '
        'INDrepCheckSelectOptionSurgical
        '
        Me.INDrepCheckSelectOptionSurgical.AutoHeight = False
        Me.INDrepCheckSelectOptionSurgical.Name = "INDrepCheckSelectOptionSurgical"
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Concepto"
        Me.GridColumn15.FieldName = "IPSServiceDescriptionSOD"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 1
        Me.GridColumn15.Width = 250
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Servicio IPS"
        Me.GridColumn16.FieldName = "IPSServiceDescription"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 1
        Me.GridColumn16.Width = 220
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Cant."
        Me.GridColumn17.FieldName = "InvoicedQuantity"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 2
        Me.GridColumn17.Width = 50
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Val. Unit."
        Me.GridColumn18.ColumnEdit = Me.INDrepTxtRateManualSalesPriceSurgical
        Me.GridColumn18.DisplayFormat.FormatString = "c0"
        Me.GridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn18.FieldName = "TotalSalesPrice"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 3
        Me.GridColumn18.Width = 82
        '
        'INDrepTxtRateManualSalesPriceSurgical
        '
        Me.INDrepTxtRateManualSalesPriceSurgical.AutoHeight = False
        Me.INDrepTxtRateManualSalesPriceSurgical.Mask.EditMask = "C2"
        Me.INDrepTxtRateManualSalesPriceSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtRateManualSalesPriceSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtRateManualSalesPriceSurgical.Name = "INDrepTxtRateManualSalesPriceSurgical"
        '
        'INDcolHealthProfessionalSurgical
        '
        Me.INDcolHealthProfessionalSurgical.Caption = "Médico"
        Me.INDcolHealthProfessionalSurgical.ColumnEdit = Me.INDrepSleHealthProfessionalSurgical
        Me.INDcolHealthProfessionalSurgical.FieldName = "PerformsHealthProfessionalCode"
        Me.INDcolHealthProfessionalSurgical.Name = "INDcolHealthProfessionalSurgical"
        Me.INDcolHealthProfessionalSurgical.Visible = True
        Me.INDcolHealthProfessionalSurgical.VisibleIndex = 4
        Me.INDcolHealthProfessionalSurgical.Width = 233
        '
        'INDrepSleHealthProfessionalSurgical
        '
        Me.INDrepSleHealthProfessionalSurgical.AutoHeight = False
        Me.INDrepSleHealthProfessionalSurgical.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepSleHealthProfessionalSurgical.DisplayMember = "CodeName"
        Me.INDrepSleHealthProfessionalSurgical.Name = "INDrepSleHealthProfessionalSurgical"
        Me.INDrepSleHealthProfessionalSurgical.NullText = ""
        Me.INDrepSleHealthProfessionalSurgical.PopupView = Me.GridView1
        Me.INDrepSleHealthProfessionalSurgical.ValueMember = "CODPROSAL"
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn12, Me.GridColumn19})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Código"
        Me.GridColumn12.FieldName = "CODPROSAL"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        Me.GridColumn12.Width = 346
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Nombre"
        Me.GridColumn19.FieldName = "NOMMEDICO"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 1
        Me.GridColumn19.Width = 1286
        '
        'GridColumn39
        '
        Me.GridColumn39.Caption = "Contrato"
        Me.GridColumn39.FieldName = "MedicalFeesContractCodeName"
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.OptionsColumn.AllowEdit = False
        Me.GridColumn39.OptionsColumn.AllowFocus = False
        Me.GridColumn39.Visible = True
        Me.GridColumn39.VisibleIndex = 5
        Me.GridColumn39.Width = 78
        '
        'INDcolCausedValueSurgical
        '
        Me.INDcolCausedValueSurgical.Caption = "Valor Causado"
        Me.INDcolCausedValueSurgical.ColumnEdit = Me.INDrepTxtCausedValueSurgical
        Me.INDcolCausedValueSurgical.DisplayFormat.FormatString = "c0"
        Me.INDcolCausedValueSurgical.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcolCausedValueSurgical.FieldName = "AmountPayable"
        Me.INDcolCausedValueSurgical.Name = "INDcolCausedValueSurgical"
        Me.INDcolCausedValueSurgical.Visible = True
        Me.INDcolCausedValueSurgical.VisibleIndex = 6
        Me.INDcolCausedValueSurgical.Width = 191
        '
        'INDrepTxtCausedValueSurgical
        '
        Me.INDrepTxtCausedValueSurgical.AutoHeight = False
        Me.INDrepTxtCausedValueSurgical.Mask.EditMask = "C2"
        Me.INDrepTxtCausedValueSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtCausedValueSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtCausedValueSurgical.Name = "INDrepTxtCausedValueSurgical"
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Val. Total"
        Me.GridColumn22.ColumnEdit = Me.INDrepTxtTotalAmountPayableSurgical
        Me.GridColumn22.DisplayFormat.FormatString = "c0"
        Me.GridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn22.FieldName = "TotalAmountPayable"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.OptionsColumn.AllowFocus = False
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 7
        Me.GridColumn22.Width = 102
        '
        'INDrepTxtTotalAmountPayableSurgical
        '
        Me.INDrepTxtTotalAmountPayableSurgical.AutoHeight = False
        Me.INDrepTxtTotalAmountPayableSurgical.Mask.EditMask = "C2"
        Me.INDrepTxtTotalAmountPayableSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotalAmountPayableSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotalAmountPayableSurgical.Name = "INDrepTxtTotalAmountPayableSurgical"
        '
        'GridColumn42
        '
        Me.GridColumn42.Caption = "Sub Val. Unit."
        Me.GridColumn42.ColumnEdit = Me.INDrepTxtSubTotalSalesPriceSurgical
        Me.GridColumn42.FieldName = "SubTotalSalesPrice"
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.OptionsColumn.AllowEdit = False
        Me.GridColumn42.OptionsColumn.AllowFocus = False
        '
        'INDrepTxtSubTotalSalesPriceSurgical
        '
        Me.INDrepTxtSubTotalSalesPriceSurgical.AutoHeight = False
        Me.INDrepTxtSubTotalSalesPriceSurgical.Mask.EditMask = "C2"
        Me.INDrepTxtSubTotalSalesPriceSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtSubTotalSalesPriceSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtSubTotalSalesPriceSurgical.Name = "INDrepTxtSubTotalSalesPriceSurgical"
        '
        'GridColumn43
        '
        Me.GridColumn43.Caption = "% Descuento"
        Me.GridColumn43.ColumnEdit = Me.INDrepTxtPercentageDiscountSurgical
        Me.GridColumn43.FieldName = "ThirdPartyDiscountPercentage"
        Me.GridColumn43.Name = "GridColumn43"
        Me.GridColumn43.OptionsColumn.AllowEdit = False
        Me.GridColumn43.OptionsColumn.AllowFocus = False
        '
        'INDrepTxtPercentageDiscountSurgical
        '
        Me.INDrepTxtPercentageDiscountSurgical.AutoHeight = False
        Me.INDrepTxtPercentageDiscountSurgical.Mask.EditMask = "P2"
        Me.INDrepTxtPercentageDiscountSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtPercentageDiscountSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtPercentageDiscountSurgical.Name = "INDrepTxtPercentageDiscountSurgical"
        '
        'GridColumn44
        '
        Me.GridColumn44.Caption = "Total Cobrado a Entidad"
        Me.GridColumn44.ColumnEdit = Me.INDrepTxtGrandTotalQx
        Me.GridColumn44.DisplayFormat.FormatString = "C0"
        Me.GridColumn44.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn44.FieldName = "GrandTotalSalesPrice"
        Me.GridColumn44.Name = "GridColumn44"
        '
        'INDrepTxtGrandTotalQx
        '
        Me.INDrepTxtGrandTotalQx.AutoHeight = False
        Me.INDrepTxtGrandTotalQx.Mask.EditMask = "C0"
        Me.INDrepTxtGrandTotalQx.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtGrandTotalQx.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtGrandTotalQx.Name = "INDrepTxtGrandTotalQx"
        '
        'GridColumn46
        '
        Me.GridColumn46.Caption = "Unidad Funcional"
        Me.GridColumn46.FieldName = "FunctionalUnitDescription"
        Me.GridColumn46.Name = "GridColumn46"
        '
        'GridColumn35
        '
        Me.GridColumn35.Caption = "Fecha Servicio"
        Me.GridColumn35.FieldName = "ServiceDate"
        Me.GridColumn35.Name = "GridColumn35"
        '
        'GridColumn48
        '
        Me.GridColumn48.Caption = "Usuario Orden"
        Me.GridColumn48.FieldName = "CreationUserServiceOrder"
        Me.GridColumn48.Name = "GridColumn48"
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Usuario Creación"
        Me.GridColumn29.FieldName = "CreationUser"
        Me.GridColumn29.Name = "GridColumn29"
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Fecha Creación"
        Me.GridColumn30.FieldName = "CreationDate"
        Me.GridColumn30.Name = "GridColumn30"
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Usuario Modificación"
        Me.GridColumn31.FieldName = "ModificationUser"
        Me.GridColumn31.Name = "GridColumn31"
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "Fecha Modificación"
        Me.GridColumn32.FieldName = "ModificationDate"
        Me.GridColumn32.Name = "GridColumn32"
        '
        'GridColumn33
        '
        Me.GridColumn33.Caption = "Usuario Confirmación"
        Me.GridColumn33.FieldName = "ConfirmationUser"
        Me.GridColumn33.Name = "GridColumn33"
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "Fecha Confirmación"
        Me.GridColumn34.FieldName = "ConfirmationDate"
        Me.GridColumn34.Name = "GridColumn34"
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "Valor Causado Real"
        Me.GridColumn51.ColumnEdit = Me.INDrepTxtTotalAmountPayableRealSurgical
        Me.GridColumn51.DisplayFormat.FormatString = "C0"
        Me.GridColumn51.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn51.FieldName = "TotalAmountPayableReal"
        Me.GridColumn51.Name = "GridColumn51"
        '
        'INDrepTxtTotalAmountPayableRealSurgical
        '
        Me.INDrepTxtTotalAmountPayableRealSurgical.AutoHeight = False
        Me.INDrepTxtTotalAmountPayableRealSurgical.Mask.EditMask = "C0"
        Me.INDrepTxtTotalAmountPayableRealSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotalAmountPayableRealSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotalAmountPayableRealSurgical.Name = "INDrepTxtTotalAmountPayableRealSurgical"
        '
        'GridColumn52
        '
        Me.GridColumn52.Caption = "Porcentaje"
        Me.GridColumn52.ColumnEdit = Me.INDrepTxtPercentageCashedSurgical
        Me.GridColumn52.DisplayFormat.FormatString = "P2"
        Me.GridColumn52.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn52.FieldName = "PercentageCashed"
        Me.GridColumn52.Name = "GridColumn52"
        '
        'INDrepTxtPercentageCashedSurgical
        '
        Me.INDrepTxtPercentageCashedSurgical.AutoHeight = False
        Me.INDrepTxtPercentageCashedSurgical.Mask.EditMask = "P2"
        Me.INDrepTxtPercentageCashedSurgical.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtPercentageCashedSurgical.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtPercentageCashedSurgical.Name = "INDrepTxtPercentageCashedSurgical"
        '
        'GridColumn54
        '
        Me.GridColumn54.Caption = "Estado"
        Me.GridColumn54.ColumnEdit = Me.INDrepColorSurgical
        Me.GridColumn54.FieldName = "Color"
        Me.GridColumn54.Name = "GridColumn54"
        Me.GridColumn54.OptionsColumn.AllowEdit = False
        Me.GridColumn54.OptionsColumn.AllowFocus = False
        '
        'INDrepColorSurgical
        '
        Me.INDrepColorSurgical.AutoHeight = False
        Me.INDrepColorSurgical.AutomaticColor = System.Drawing.Color.Black
        Me.INDrepColorSurgical.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepColorSurgical.ColorAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepColorSurgical.Name = "INDrepColorSurgical"
        '
        'GridColumn56
        '
        Me.GridColumn56.Caption = "Fecha Causación"
        Me.GridColumn56.FieldName = "CausationDate"
        Me.GridColumn56.Name = "GridColumn56"
        Me.GridColumn56.OptionsColumn.AllowEdit = False
        Me.GridColumn56.OptionsColumn.AllowFocus = False
        '
        'INDsleInvoice
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleInvoice, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleInvoice, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleInvoice, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleInvoice, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleInvoice, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleInvoice, False)
        Me.INDsleInvoice.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleInvoice, False)
        Me.INDsleInvoice.Location = New System.Drawing.Point(-372, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleInvoice, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleInvoice.Name = "INDsleInvoice"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleInvoice, False)
        Me.INDsleInvoice.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleInvoice.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleInvoice.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleInvoice.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleInvoice.Properties.Appearance.Options.UseFont = True
        Me.INDsleInvoice.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleInvoice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleInvoice.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleInvoice.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleInvoice.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleInvoice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleInvoice.Properties.DisplayMember = "NumberDocument"
        Me.INDsleInvoice.Properties.NullText = ""
        Me.INDsleInvoice.Properties.PopupFormMinSize = New System.Drawing.Size(850, 0)
        Me.INDsleInvoice.Properties.PopupSizeable = False
        Me.INDsleInvoice.Properties.PopupView = Me.viewSearchInvoice
        Me.INDsleInvoice.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtTotal})
        Me.INDsleInvoice.Properties.ShowFooter = False
        Me.INDsleInvoice.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleInvoice, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleInvoice, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleInvoice, True)
        Me.INDsleInvoice.Size = New System.Drawing.Size(386, 28)
        Me.INDsleInvoice.StyleController = Me.INDlyMedicalFeesCausation
        Me.INDsleInvoice.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleInvoice, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleInvoice, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleInvoice, "{0} - {1}")
        Me.INDsleInvoice.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleInvoice, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleInvoice, False)
        '
        'viewSearchInvoice
        '
        Me.viewSearchInvoice.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSearchInvoice.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewSearchInvoice.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSearchInvoice.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSearchInvoice.Appearance.FocusedRow.Options.UseForeColor = True
        Me.viewSearchInvoice.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSearchInvoice.Appearance.GroupRow.Options.UseFont = True
        Me.viewSearchInvoice.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSearchInvoice.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSearchInvoice.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewSearchInvoice.Appearance.Row.Options.UseFont = True
        Me.viewSearchInvoice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn4, Me.GridColumn3, Me.GridColumn5, Me.GridColumn6, Me.GridColumn53})
        Me.viewSearchInvoice.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewSearchInvoice.Name = "viewSearchInvoice"
        Me.viewSearchInvoice.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSearchInvoice.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSearchInvoice.OptionsView.EnableAppearanceOddRow = True
        Me.viewSearchInvoice.OptionsView.ShowAutoFilterRow = True
        Me.viewSearchInvoice.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.viewSearchInvoice, False)
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.viewSearchInvoice, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSearchInvoice, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "No. Factura"
        Me.GridColumn1.FieldName = "InvoiceNumber"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo Documento"
        Me.GridColumn2.FieldName = "DocumentTypeName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "No. Ingreso"
        Me.GridColumn4.FieldName = "AdmissionNumber"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código Paciente"
        Me.GridColumn3.FieldName = "PatientCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Fecha Factura"
        Me.GridColumn5.FieldName = "InvoiceDate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Total Factura"
        Me.GridColumn6.ColumnEdit = Me.INDrepTxtTotal
        Me.GridColumn6.DisplayFormat.FormatString = "c0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "TotalInvoice"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        '
        'INDrepTxtTotal
        '
        Me.INDrepTxtTotal.AutoHeight = False
        Me.INDrepTxtTotal.Mask.EditMask = "C0"
        Me.INDrepTxtTotal.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtTotal.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtTotal.Name = "INDrepTxtTotal"
        '
        'GridColumn53
        '
        Me.GridColumn53.Caption = "Categoria"
        Me.GridColumn53.FieldName = "InvoiceCategoryId.CodeName"
        Me.GridColumn53.Name = "GridColumn53"
        Me.GridColumn53.Visible = True
        Me.GridColumn53.VisibleIndex = 6
        '
        'INDsleAdmission
        '
        Me.INDsleAdmission._flagLoadEditValue = False
        Me.INDsleAdmission.Datasource = Nothing
        Me.INDsleAdmission.IsReadOnly = False
        Me.INDsleAdmission.Location = New System.Drawing.Point(-372, 79)
        Me.INDsleAdmission.Margin = New System.Windows.Forms.Padding(0)
        Me.INDsleAdmission.MaximumSize = New System.Drawing.Size(0, 28)
        Me.INDsleAdmission.MinimumSize = New System.Drawing.Size(70, 28)
        Me.INDsleAdmission.Name = "INDsleAdmission"
        Me.INDsleAdmission.OpenFormAction = Nothing
        Me.INDsleAdmission.PopupContainerControl = Nothing
        Me.INDsleAdmission.Size = New System.Drawing.Size(386, 28)
        Me.INDsleAdmission.TabIndex = 0
        Me.INDsleAdmission.TagForm = ""
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation, Me.INDlygDetails, Me.INDlygDetailsSurgical, Me.INDlygNotes})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2684, 540)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.INDlygGeneralInformation.CustomizationFormText = "Información General"
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAdmission, Me.INDlyItemInvoice, Me.INDlyItemCausationDate})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(414, 520)
        Me.INDlygGeneralInformation.Text = "Información General"
        '
        'INDlyItemAdmission
        '
        Me.INDlyItemAdmission.AllowHide = False
        Me.INDlyItemAdmission.Control = Me.INDsleAdmission
        Me.INDlyItemAdmission.CustomizationFormText = "Ingresos"
        Me.INDlyItemAdmission.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAdmission.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAdmission.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemAdmission.Name = "INDlyItemAdmission"
        Me.INDlyItemAdmission.ShowInCustomizationForm = False
        Me.INDlyItemAdmission.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemAdmission.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAdmission.Text = "Ingresos"
        Me.INDlyItemAdmission.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAdmission.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAdmission.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAdmission.TextToControlDistance = 5
        '
        'INDlyItemInvoice
        '
        Me.INDlyItemInvoice.AllowHide = False
        Me.INDlyItemInvoice.Control = Me.INDsleInvoice
        Me.INDlyItemInvoice.CustomizationFormText = "Facturas"
        Me.INDlyItemInvoice.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemInvoice.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemInvoice.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemInvoice.Name = "INDlyItemInvoice"
        Me.INDlyItemInvoice.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInvoice.Text = "Facturas"
        Me.INDlyItemInvoice.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInvoice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInvoice.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInvoice.TextToControlDistance = 5
        '
        'INDlyItemCausationDate
        '
        Me.INDlyItemCausationDate.AllowHide = False
        Me.INDlyItemCausationDate.Control = Me.INDdteCausationDate
        Me.INDlyItemCausationDate.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemCausationDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemCausationDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemCausationDate.Name = "INDlyItemCausationDate"
        Me.INDlyItemCausationDate.Size = New System.Drawing.Size(390, 339)
        Me.INDlyItemCausationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCausationDate.Text = "Fecha Causación"
        Me.INDlyItemCausationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCausationDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCausationDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCausationDate.TextToControlDistance = 5
        '
        'INDlygDetails
        '
        Me.INDlygDetails.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetails.AppearanceGroup.Options.UseFont = True
        Me.INDlygDetails.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetails.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDetails.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetails.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDetails, False)
        Me.INDlygDetails.CustomizationFormText = "Detalles No Quirúrgicos"
        Me.INDlygDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemMedicalFeesCausation})
        Me.INDlygDetails.Location = New System.Drawing.Point(414, 0)
        Me.INDlygDetails.Name = "INDlygDetails"
        Me.INDlygDetails.Size = New System.Drawing.Size(852, 520)
        Me.INDlygDetails.Text = "Detalles No Quirúrgicos"
        '
        'INDlyItemMedicalFeesCausation
        '
        Me.INDlyItemMedicalFeesCausation.Control = Me.INDgcMedicalFeesCausation
        Me.INDlyItemMedicalFeesCausation.CustomizationFormText = "INDlyItemMedicalFeesCausation"
        Me.INDlyItemMedicalFeesCausation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemMedicalFeesCausation.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemMedicalFeesCausation.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemMedicalFeesCausation.Name = "INDlyItemMedicalFeesCausation"
        Me.INDlyItemMedicalFeesCausation.Size = New System.Drawing.Size(828, 467)
        Me.INDlyItemMedicalFeesCausation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemMedicalFeesCausation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemMedicalFeesCausation.TextVisible = False
        '
        'INDlygDetailsSurgical
        '
        Me.INDlygDetailsSurgical.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetailsSurgical.AppearanceGroup.Options.UseFont = True
        Me.INDlygDetailsSurgical.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDetailsSurgical.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDetailsSurgical.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetailsSurgical.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDetailsSurgical.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDetailsSurgical.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDetailsSurgical.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetailsSurgical.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDetailsSurgical.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetailsSurgical.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDetailsSurgical.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDetailsSurgical.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDetailsSurgical, False)
        Me.INDlygDetailsSurgical.CustomizationFormText = "Detalles Quirúrgicos y Paquetes"
        Me.INDlygDetailsSurgical.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemMedicalFeesCausationSurgical})
        Me.INDlygDetailsSurgical.Location = New System.Drawing.Point(1266, 0)
        Me.INDlygDetailsSurgical.Name = "INDlygDetailsSurgical"
        Me.INDlygDetailsSurgical.Size = New System.Drawing.Size(852, 520)
        Me.INDlygDetailsSurgical.Text = "Detalles Quirúrgicos"
        '
        'INDlyItemMedicalFeesCausationSurgical
        '
        Me.INDlyItemMedicalFeesCausationSurgical.Control = Me.INDgcMedicalFeesCausationSurgical
        Me.INDlyItemMedicalFeesCausationSurgical.CustomizationFormText = "INDlyItemMedicalFeesCausationSurgical"
        Me.INDlyItemMedicalFeesCausationSurgical.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemMedicalFeesCausationSurgical.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemMedicalFeesCausationSurgical.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemMedicalFeesCausationSurgical.Name = "INDlyItemMedicalFeesCausationSurgical"
        Me.INDlyItemMedicalFeesCausationSurgical.Size = New System.Drawing.Size(828, 467)
        Me.INDlyItemMedicalFeesCausationSurgical.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemMedicalFeesCausationSurgical.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemMedicalFeesCausationSurgical.TextVisible = False
        '
        'INDlygNotes
        '
        Me.INDlygNotes.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygNotes.AppearanceGroup.Options.UseFont = True
        Me.INDlygNotes.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygNotes.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygNotes.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygNotes.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygNotes.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygNotes.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygNotes.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygNotes.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygNotes.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygNotes.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygNotes.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygNotes.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygNotes, False)
        Me.INDlygNotes.CustomizationFormText = "Notas"
        Me.INDlygNotes.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridNotes, Me.INDlyItemPceNotes})
        Me.INDlygNotes.Location = New System.Drawing.Point(2118, 0)
        Me.INDlygNotes.Name = "INDlygNotes"
        Me.INDlygNotes.Size = New System.Drawing.Size(546, 520)
        Me.INDlygNotes.Text = "Notas"
        '
        'INDlyItemGridNotes
        '
        Me.INDlyItemGridNotes.Control = Me.INDgcNotes
        Me.INDlyItemGridNotes.CustomizationFormText = "INDlyItemGridNotes"
        Me.INDlyItemGridNotes.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemGridNotes.Name = "INDlyItemGridNotes"
        Me.INDlyItemGridNotes.Size = New System.Drawing.Size(522, 431)
        Me.INDlyItemGridNotes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridNotes.TextVisible = False
        '
        'INDlyItemPceNotes
        '
        Me.INDlyItemPceNotes.Control = Me.INDpceNotes
        Me.INDlyItemPceNotes.CustomizationFormText = "INDlyItemPceNotes"
        Me.INDlyItemPceNotes.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPceNotes.MaxSize = New System.Drawing.Size(522, 36)
        Me.INDlyItemPceNotes.MinSize = New System.Drawing.Size(522, 36)
        Me.INDlyItemPceNotes.Name = "INDlyItemPceNotes"
        Me.INDlyItemPceNotes.Size = New System.Drawing.Size(522, 36)
        Me.INDlyItemPceNotes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPceNotes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemPceNotes.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'xpViewServiceOrderDetail
        '
        Me.xpViewServiceOrderDetail.ObjectType = GetType(Infrastructure.Data.Xpo.BillingRepository.ViewListSurgicalAndPackageXpo)
        Me.xpViewServiceOrderDetail.Properties.AddRange(New DevExpress.Xpo.ViewProperty() {New DevExpress.Xpo.ViewProperty("CodeNameBillingGroup", DevExpress.Xpo.SortDirection.None, "[CodeNameBillingGroup]", False, True), New DevExpress.Xpo.ViewProperty("InvoiceId", DevExpress.Xpo.SortDirection.None, "[InvoiceId]", False, True)})
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        Me.IndigoGridView2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView3
        '
        Me.IndigoGridView3.RaiseMenuPopUp = True
        Me.IndigoGridView3.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'FrmMedicalFeesCausation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMedicalFeesCausation"
        Me.Opacity = 1.0R
        Me.Tag = "1301"
        Me.Text = "Causaciones de Honorarios Médicos"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcMedicalFeesCausation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtRateManualSalesPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleHealthProfessionalGridNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtCausedValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotalAmountPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtSubTotalSalesPriceNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtPercentageDiscountNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtGrandTotalNoQx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotalAmountPayableRealNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtPercentageCashedNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepColorNoSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyMedicalFeesCausation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyMedicalFeesCausation.ResumeLayout(False)
        CType(Me.INDdteCausationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteCausationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopupInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupInfo.ResumeLayout(False)
        CType(Me.INDlyPopupInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPopupInfo.ResumeLayout(False)
        CType(Me.INDtxtTotalCopayment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTotalEntityPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtBillPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtHealthAdministratorPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPatientPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtAdmissionNumberPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAdmissionNumberPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPatientPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHealthAdministratorPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBillPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalEntityPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalCopaymentPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccMoreInfoAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccMoreInfoAdmission.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDTxtStay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionPlace.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtBenefitsPlan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopupNotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupNotes.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDmemoNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceNotes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcMedicalFeesCausationSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewSurgicalAndPackage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCheckSelectOptionSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtRateManualSalesPriceSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepSleHealthProfessionalSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtCausedValueSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotalAmountPayableSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtSubTotalSalesPriceSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtPercentageDiscountSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtGrandTotalQx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotalAmountPayableRealSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtPercentageCashedSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepColorSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSearchInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAdmission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCausationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemMedicalFeesCausation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDetailsSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemMedicalFeesCausationSurgical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPceNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xpViewServiceOrderDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyMedicalFeesCausation As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcMedicalFeesCausation As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewNoSurgical As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemMedicalFeesCausation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleAdmission As Presentation.Controls.CtrSearchLookUpEditWithPopUp
    Friend WithEvents INDlyItemAdmission As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPccMoreInfoAdmission As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtStay As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtResponsiblePhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtResponsibleName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAuthorizationNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtEntity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtLiquidationType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionPlace As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtBenefitsPlan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAdmissionDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciStay As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDsleInvoice As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSearchInvoice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemInvoice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtTotal As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtRateManualSalesPrice As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectOption As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDcolHealthProfessional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCausedValueNoSurgical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtCausedValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDlygDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcMedicalFeesCausationSurgical As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewSurgicalAndPackage As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygDetailsSurgical As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemMedicalFeesCausationSurgical As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolHealthProfessionalSurgical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolCausedValueSurgical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCheckSelectOptionSurgical As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDrepTxtRateManualSalesPriceSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtCausedValueSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents xpViewServiceOrderDetail As DevExpress.Xpo.XPView
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtTotalAmountPayableSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtTotalAmountPayable As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView2 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDgcNotes As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewNotes As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygNotes As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemGridNotes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView3 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDpceNotes As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlyItemPceNotes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpopupNotes As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDmemoNotes As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemNotes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAddNote As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolOnly As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSleHealthProfessionalGridNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepSleHealthProfessionalSurgical As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtSubTotalSalesPriceNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtPercentageDiscountNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtSubTotalSalesPriceSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtPercentageDiscountSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDpopupInfo As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyPopupInfo As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtBillPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtHealthAdministratorPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtPatientPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtAdmissionNumberPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemAdmissionNumberPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPatientPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemHealthAdministratorPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemBillPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtTotalCopayment As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtTotalEntityPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemTotalEntityPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTotalCopaymentPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolOnlyNoQx As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtGrandTotalNoQx As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtGrandTotalQx As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtTotalAmountPayableRealSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtPercentageCashedSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtTotalAmountPayableRealNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepTxtPercentageCashedNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolColorNoSurgical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepColorNoSurgical As DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepColorSurgical As DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit
    Friend WithEvents INDdteCausationDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemCausationDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
