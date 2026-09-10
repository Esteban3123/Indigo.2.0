Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountReceivableDocument
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
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcAccountReceivableDocument = New DevExpress.XtraLayout.LayoutControl()
        Me.INDspnValueCredit = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspnValueDebit = New DevExpress.XtraEditors.SpinEdit()
        Me.INDsleMainAccountId = New Presentation.Controls.CtrPUC()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn165 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn166 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn167 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn168 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn169 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn170 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn171 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn172 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn173 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn174 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDspnShare = New DevExpress.XtraEditors.SpinEdit()
        Me.INDGcConcepts = New DevExpress.XtraGrid.GridControl()
        Me.INDGvConcepts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcbeNature = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDcValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDmeObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdeExpiredDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDspnTerm = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtInvoiceNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleCostCenterId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleCustomerId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdeDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleCurrency = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCurrency = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColAbbreviation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgGeneralData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCustomerId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciInvoiceNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCurrency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgDataOptional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCostCenterId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciTerm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciExpiredDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciShare = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciMainAccountId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDebitValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValueCredit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgConcepts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn160 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn161 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn162 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn163 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn164 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn155 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn156 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn157 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn158 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn159 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn150 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn151 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn152 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn153 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn154 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn145 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn146 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn147 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn148 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn149 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn140 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn141 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn142 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn143 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn144 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn135 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn136 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn137 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn138 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn139 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn130 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn131 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn132 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn133 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn134 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn125 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn126 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn127 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn129 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn120 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn121 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn122 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn123 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn124 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn116 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn117 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn118 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn119 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn114 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn115 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn106 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn107 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn108 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn109 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn110 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn101 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn102 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn103 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn104 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn105 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn96 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn97 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn98 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn99 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn100 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn91 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn92 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn93 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn94 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn95 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn86 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn87 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn90 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn81 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn82 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn83 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn84 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn76 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn77 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn78 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn79 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn80 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn72 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn66 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn67 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn70 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn62 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn63 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn64 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn65 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn59 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn60 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GridColumn561 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn562 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn563 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn564 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn565 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridView4 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridView21 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit21 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridView3 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcAccountReceivableDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcAccountReceivableDocument.SuspendLayout()
        CType(Me.INDspnValueCredit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnValueDebit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleMainAccountId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleMainAccountId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnShare.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcConcepts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvConcepts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDcbeNature, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeExpiredDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeExpiredDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspnTerm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtInvoiceNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCostCenterId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCustomerId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgGeneralData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCustomerId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciInvoiceNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgDataOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCostCenterId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciTerm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciExpiredDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciShare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciMainAccountId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDebitValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValueCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgConcepts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcAccountReceivableDocument)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 594)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLcAccountReceivableDocument
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 585)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLcAccountReceivableDocument
        '
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDspnValueCredit)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDspnValueDebit)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDsleMainAccountId)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDspnShare)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDGcConcepts)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDbtnAdd)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDmeObservation)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDdeExpiredDate)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDspnTerm)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDtxtInvoiceNumber)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDsleCostCenterId)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDsleCustomerId)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDdeDocumentDate)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDbtnCode)
        Me.INDLcAccountReceivableDocument.Controls.Add(Me.INDsleCurrency)
        Me.INDLcAccountReceivableDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcAccountReceivableDocument.Location = New System.Drawing.Point(202, 7)
        Me.INDLcAccountReceivableDocument.Name = "INDLcAccountReceivableDocument"
        Me.INDLcAccountReceivableDocument.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2383, 292, 250, 350)
        Me.INDLcAccountReceivableDocument.Root = Me.LayoutControlGroup1
        Me.INDLcAccountReceivableDocument.Size = New System.Drawing.Size(804, 585)
        Me.INDLcAccountReceivableDocument.TabIndex = 1
        Me.INDLcAccountReceivableDocument.Text = "LayoutControl1"
        '
        'INDspnValueCredit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnValueCredit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnValueCredit, True)
        Me.INDspnValueCredit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnValueCredit.EnterMoveNextControl = True
        Me.INDspnValueCredit.Location = New System.Drawing.Point(438, 463)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnValueCredit, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDspnValueCredit.Name = "INDspnValueCredit"
        Me.INDspnValueCredit.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnValueCredit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnValueCredit.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnValueCredit.Properties.Appearance.Options.UseFont = True
        Me.INDspnValueCredit.Properties.Appearance.Options.UseTextOptions = True
        Me.INDspnValueCredit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDspnValueCredit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnValueCredit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnValueCredit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnValueCredit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnValueCredit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnValueCredit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnValueCredit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnValueCredit.Properties.Mask.EditMask = "c0"
        Me.INDspnValueCredit.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspnValueCredit.Size = New System.Drawing.Size(386, 28)
        Me.INDspnValueCredit.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDspnValueCredit.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnValueCredit, 0)
        '
        'INDspnValueDebit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnValueDebit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnValueDebit, False)
        Me.INDspnValueDebit.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnValueDebit.EnterMoveNextControl = True
        Me.INDspnValueDebit.Location = New System.Drawing.Point(438, 399)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnValueDebit, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDspnValueDebit.Name = "INDspnValueDebit"
        Me.INDspnValueDebit.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnValueDebit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnValueDebit.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnValueDebit.Properties.Appearance.Options.UseFont = True
        Me.INDspnValueDebit.Properties.Appearance.Options.UseTextOptions = True
        Me.INDspnValueDebit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDspnValueDebit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnValueDebit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnValueDebit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnValueDebit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnValueDebit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnValueDebit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnValueDebit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnValueDebit.Properties.Mask.EditMask = "c0"
        Me.INDspnValueDebit.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspnValueDebit.Size = New System.Drawing.Size(386, 28)
        Me.INDspnValueDebit.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDspnValueDebit.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnValueDebit, 0)
        '
        'INDsleMainAccountId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleMainAccountId, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleMainAccountId, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleMainAccountId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleMainAccountId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleMainAccountId, False)
        Me.INDsleMainAccountId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleMainAccountId, False)
        Me.INDsleMainAccountId.Location = New System.Drawing.Point(438, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleMainAccountId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleMainAccountId.Name = "INDsleMainAccountId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleMainAccountId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleMainAccountId, False)
        Me.INDsleMainAccountId.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleMainAccountId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleMainAccountId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleMainAccountId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleMainAccountId.Properties.Appearance.Options.UseFont = True
        Me.INDsleMainAccountId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleMainAccountId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleMainAccountId.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleMainAccountId.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleMainAccountId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleMainAccountId.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleMainAccountId.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleMainAccountId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleMainAccountId.Properties.DisplayMember = "NumberName"
        Me.INDsleMainAccountId.Properties.NullText = ""
        Me.INDsleMainAccountId.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.INDsleMainAccountId.Properties.PopupFormMinSize = New System.Drawing.Size(800, 0)
        Me.INDsleMainAccountId.Properties.PopupSizeable = False
        Me.INDsleMainAccountId.Properties.PopupView = Me.GridView3
        Me.INDsleMainAccountId.Properties.ShowClearButton = False
        Me.INDsleMainAccountId.Properties.ShowFooter = False
        Me.INDsleMainAccountId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleMainAccountId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleMainAccountId, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleMainAccountId, True)
        Me.INDsleMainAccountId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleMainAccountId.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDsleMainAccountId.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleMainAccountId, "602")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleMainAccountId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleMainAccountId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleMainAccountId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleMainAccountId, False)
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn170, Me.GridColumn171, Me.GridColumn172, Me.GridColumn173, Me.GridColumn174})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsFind.FindFilterColumns = "Number"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowDetailButtons = False
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridView3, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.GridView3, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        Me.IndigoGridView21.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn165
        '
        Me.GridColumn165.Caption = "Id"
        Me.GridColumn165.FieldName = "Id"
        Me.GridColumn165.Name = "GridColumn165"
        '
        'GridColumn166
        '
        Me.GridColumn166.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn166.Caption = "Código"
        Me.GridColumn166.FieldName = "Number"
        Me.GridColumn166.Name = "GridColumn166"
        Me.GridColumn166.Width = 77
        '
        'GridColumn167
        '
        Me.GridColumn167.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn167.Caption = "Nombre"
        Me.GridColumn167.FieldName = "Name"
        Me.GridColumn167.Name = "GridColumn167"
        Me.GridColumn167.Width = 89
        '
        'GridColumn168
        '
        Me.GridColumn168.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn168.Caption = "Maneja Tercero"
        Me.GridColumn168.FieldName = "HandlesThirdParty"
        Me.GridColumn168.Name = "GridColumn168"
        Me.GridColumn168.Width = 173
        '
        'GridColumn169
        '
        Me.GridColumn169.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn169.Caption = "Maneja Centro de Costo"
        Me.GridColumn169.FieldName = "HandlesCostCenter"
        Me.GridColumn169.Name = "GridColumn169"
        Me.GridColumn169.Width = 350
        '
        'GridColumn170
        '
        Me.GridColumn170.Caption = "Id"
        Me.GridColumn170.FieldName = "Id"
        Me.GridColumn170.Name = "GridColumn170"
        '
        'GridColumn171
        '
        Me.GridColumn171.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn171.Caption = "Código"
        Me.GridColumn171.FieldName = "Number"
        Me.GridColumn171.Name = "GridColumn171"
        Me.GridColumn171.Visible = True
        Me.GridColumn171.VisibleIndex = 0
        Me.GridColumn171.Width = 77
        '
        'GridColumn172
        '
        Me.GridColumn172.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn172.Caption = "Nombre"
        Me.GridColumn172.FieldName = "Name"
        Me.GridColumn172.Name = "GridColumn172"
        Me.GridColumn172.Visible = True
        Me.GridColumn172.VisibleIndex = 1
        Me.GridColumn172.Width = 89
        '
        'GridColumn173
        '
        Me.GridColumn173.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn173.Caption = "Maneja Tercero"
        Me.GridColumn173.FieldName = "HandlesThirdParty"
        Me.GridColumn173.Name = "GridColumn173"
        Me.GridColumn173.Visible = True
        Me.GridColumn173.VisibleIndex = 2
        Me.GridColumn173.Width = 173
        '
        'GridColumn174
        '
        Me.GridColumn174.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn174.Caption = "Maneja Centro de Costo"
        Me.GridColumn174.FieldName = "HandlesCostCenter"
        Me.GridColumn174.Name = "GridColumn174"
        Me.GridColumn174.Visible = True
        Me.GridColumn174.VisibleIndex = 3
        Me.GridColumn174.Width = 350
        '
        'INDspnShare
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnShare, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnShare, True)
        Me.INDspnShare.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDspnShare.EnterMoveNextControl = True
        Me.INDspnShare.Location = New System.Drawing.Point(438, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnShare, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspnShare.Name = "INDspnShare"
        Me.INDspnShare.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnShare.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnShare.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnShare.Properties.Appearance.Options.UseFont = True
        Me.INDspnShare.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnShare.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnShare.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnShare.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnShare.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnShare.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnShare.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnShare.Properties.Mask.EditMask = "n0"
        Me.INDspnShare.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspnShare.Properties.MaxValue = New Decimal(New Integer() {999, 0, 0, 0})
        Me.INDspnShare.Size = New System.Drawing.Size(386, 28)
        Me.INDspnShare.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDspnShare.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnShare, 0)
        '
        'INDGcConcepts
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcConcepts, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcConcepts, Nothing)
        Me.INDGcConcepts.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcConcepts, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcConcepts, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcConcepts, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcConcepts, False)
        Me.INDGcConcepts.Location = New System.Drawing.Point(852, 89)
        Me.INDGcConcepts.MainView = Me.INDGvConcepts
        Me.INDGcConcepts.Name = "INDGcConcepts"
        Me.INDGcConcepts.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDcbeNature})
        Me.INDGcConcepts.Size = New System.Drawing.Size(824, 455)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcConcepts, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcConcepts.TabIndex = 13
        Me.INDGcConcepts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvConcepts})
        '
        'INDGvConcepts
        '
        Me.INDGvConcepts.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvConcepts.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvConcepts.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvConcepts.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvConcepts.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvConcepts.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvConcepts.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvConcepts.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvConcepts.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvConcepts.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvConcepts.Appearance.Row.Options.UseFont = True
        Me.INDGvConcepts.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvConcepts.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvConcepts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.INDcValue})
        Me.INDGvConcepts.GridControl = Me.INDGcConcepts
        Me.INDGvConcepts.Name = "INDGvConcepts"
        Me.INDGvConcepts.OptionsCustomization.AllowGroup = False
        Me.INDGvConcepts.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvConcepts.OptionsDetail.ShowDetailTabs = False
        Me.INDGvConcepts.OptionsFind.AlwaysVisible = True
        Me.INDGvConcepts.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvConcepts.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvConcepts.OptionsView.ShowAutoFilterRow = True
        Me.INDGvConcepts.OptionsView.ShowDetailButtons = False
        Me.INDGvConcepts.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvConcepts, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvConcepts, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvConcepts, False)
        Me.IndigoGridView21.SetTemaIndigoMetro(Me.INDGvConcepts, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Concepto"
        Me.GridColumn1.FieldName = "DescriptionAccountReceivableConcept"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Cuenta Contable"
        Me.GridColumn2.FieldName = "DescriptionAccount"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Tercero"
        Me.GridColumn3.FieldName = "DescriptionThirdParty"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Centro Costo"
        Me.GridColumn4.FieldName = "DescriptionCostCenter"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Naturaleza"
        Me.GridColumn5.ColumnEdit = Me.INDcbeNature
        Me.GridColumn5.FieldName = "Nature"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'INDcbeNature
        '
        Me.INDcbeNature.AutoHeight = False
        Me.INDcbeNature.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDcbeNature.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Debito", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Credito", CType(2, Byte), -1)})
        Me.INDcbeNature.Name = "INDcbeNature"
        '
        'INDcValue
        '
        Me.INDcValue.Caption = "Valor"
        Me.INDcValue.DisplayFormat.FormatString = "c0"
        Me.INDcValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDcValue.FieldName = "Value"
        Me.INDcValue.Name = "INDcValue"
        Me.INDcValue.OptionsColumn.AllowEdit = False
        Me.INDcValue.OptionsColumn.AllowFocus = False
        Me.INDcValue.Visible = True
        Me.INDcValue.VisibleIndex = 5
        '
        'INDbtnAdd
        '
        Me.INDbtnAdd.Location = New System.Drawing.Point(852, 53)
        Me.INDbtnAdd.Name = "INDbtnAdd"
        Me.INDbtnAdd.Size = New System.Drawing.Size(824, 32)
        Me.INDbtnAdd.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDbtnAdd.TabIndex = 12
        Me.INDbtnAdd.Text = "Agregar"
        '
        'INDmeObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeObservation, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeObservation, False)
        Me.INDmeObservation.EnterMoveNextControl = True
        Me.INDmeObservation.Location = New System.Drawing.Point(24, 395)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeObservation.Name = "INDmeObservation"
        Me.INDmeObservation.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeObservation.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDmeObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeObservation.Properties.Appearance.Options.UseFont = True
        Me.INDmeObservation.Properties.Appearance.Options.UseForeColor = True
        Me.INDmeObservation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeObservation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeObservation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeObservation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeObservation.Properties.MaxLength = 300
        Me.INDmeObservation.Size = New System.Drawing.Size(386, 50)
        Me.INDmeObservation.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDmeObservation.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeObservation, 0)
        '
        'INDdeExpiredDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeExpiredDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeExpiredDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeExpiredDate, True)
        Me.INDdeExpiredDate.EditValue = Nothing
        Me.INDdeExpiredDate.EnterMoveNextControl = True
        Me.INDdeExpiredDate.Location = New System.Drawing.Point(438, 342)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeExpiredDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeExpiredDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeExpiredDate.Name = "INDdeExpiredDate"
        Me.INDdeExpiredDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeExpiredDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeExpiredDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdeExpiredDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeExpiredDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeExpiredDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeExpiredDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeExpiredDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeExpiredDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeExpiredDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeExpiredDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeExpiredDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeExpiredDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeExpiredDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeExpiredDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeExpiredDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeExpiredDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeExpiredDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeExpiredDate.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDdeExpiredDate.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeExpiredDate, 0)
        Me.INDdeExpiredDate.ToolTip = "Este Campo es Necesario"
        '
        'INDspnTerm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspnTerm, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspnTerm, True)
        Me.INDspnTerm.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspnTerm.EnterMoveNextControl = True
        Me.INDspnTerm.Location = New System.Drawing.Point(438, 278)
        Me.IndigoTextEdit1.SetMascara(Me.INDspnTerm, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDspnTerm.Name = "INDspnTerm"
        Me.INDspnTerm.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnTerm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnTerm.Properties.Appearance.Options.UseBackColor = True
        Me.INDspnTerm.Properties.Appearance.Options.UseFont = True
        Me.INDspnTerm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDspnTerm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDspnTerm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspnTerm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspnTerm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspnTerm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspnTerm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDspnTerm.Properties.Mask.EditMask = "[0-9]+"
        Me.INDspnTerm.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDspnTerm.Size = New System.Drawing.Size(386, 28)
        Me.INDspnTerm.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDspnTerm.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspnTerm, 0)
        '
        'INDtxtInvoiceNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtInvoiceNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtInvoiceNumber, True)
        Me.INDtxtInvoiceNumber.EnterMoveNextControl = True
        Me.INDtxtInvoiceNumber.Location = New System.Drawing.Point(24, 331)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtInvoiceNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtInvoiceNumber.Name = "INDtxtInvoiceNumber"
        Me.INDtxtInvoiceNumber.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtInvoiceNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtInvoiceNumber.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtInvoiceNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtInvoiceNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtInvoiceNumber.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtInvoiceNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtInvoiceNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtInvoiceNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtInvoiceNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtInvoiceNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtInvoiceNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtInvoiceNumber.Properties.MaxLength = 20
        Me.INDtxtInvoiceNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtInvoiceNumber.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDtxtInvoiceNumber.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtInvoiceNumber, 0)
        Me.INDtxtInvoiceNumber.ToolTip = "Este Campo es Necesario"
        '
        'INDsleCostCenterId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCostCenterId, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCostCenterId, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCostCenterId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCostCenterId, True)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCostCenterId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCostCenterId, False)
        Me.INDsleCostCenterId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCostCenterId, False)
        Me.INDsleCostCenterId.Location = New System.Drawing.Point(438, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCostCenterId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCostCenterId.Name = "INDsleCostCenterId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCostCenterId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCostCenterId, False)
        Me.INDsleCostCenterId.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCostCenterId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCostCenterId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCostCenterId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCostCenterId.Properties.Appearance.Options.UseFont = True
        Me.INDsleCostCenterId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCostCenterId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCostCenterId.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCostCenterId.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCostCenterId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCostCenterId.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCostCenterId.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCostCenterId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCostCenterId.Properties.DisplayMember = "CodeName"
        Me.INDsleCostCenterId.Properties.NullText = ""
        Me.INDsleCostCenterId.Properties.PopupSizeable = False
        Me.INDsleCostCenterId.Properties.PopupView = Me.GridView2
        Me.INDsleCostCenterId.Properties.ShowFooter = False
        Me.INDsleCostCenterId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCostCenterId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCostCenterId, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCostCenterId, True)
        Me.INDsleCostCenterId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCostCenterId.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDsleCostCenterId.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCostCenterId, "517")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCostCenterId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCostCenterId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCostCenterId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCostCenterId, False)
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
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsCustomization.AllowGroup = False
        Me.GridView2.OptionsDetail.EnableMasterViewMode = False
        Me.GridView2.OptionsDetail.ShowDetailTabs = False
        Me.GridView2.OptionsFind.AlwaysVisible = True
        Me.GridView2.OptionsFind.FindFilterColumns = "Code"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowDetailButtons = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.GridView2, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.GridView2, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        Me.IndigoGridView21.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "Codigo"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "Descripcion"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        '
        'INDsleCustomerId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCustomerId, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCustomerId, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCustomerId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCustomerId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCustomerId, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCustomerId, False)
        Me.INDsleCustomerId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCustomerId, False)
        Me.INDsleCustomerId.Location = New System.Drawing.Point(24, 206)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCustomerId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCustomerId.Name = "INDsleCustomerId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCustomerId, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCustomerId, False)
        Me.INDsleCustomerId.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleCustomerId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCustomerId.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCustomerId.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCustomerId.Properties.Appearance.Options.UseFont = True
        Me.INDsleCustomerId.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCustomerId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCustomerId.Properties.DisplayMember = "NitName"
        Me.INDsleCustomerId.Properties.NullText = ""
        Me.INDsleCustomerId.Properties.PopupSizeable = False
        Me.INDsleCustomerId.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleCustomerId.Properties.ShowFooter = False
        Me.INDsleCustomerId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCustomerId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCustomerId, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCustomerId, True)
        Me.INDsleCustomerId.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCustomerId.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDsleCustomerId.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCustomerId, "503")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCustomerId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCustomerId, "{0} - {1}")
        Me.INDsleCustomerId.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCustomerId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCustomerId, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEdit1View.OptionsFind.FindFilterColumns = "Nit"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView21.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Nit"
        Me.GridColumn9.FieldName = "Nit"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Nombre"
        Me.GridColumn10.FieldName = "Name"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        '
        'INDdeDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDocumentDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDocumentDate, True)
        Me.INDdeDocumentDate.EditValue = Nothing
        Me.INDdeDocumentDate.EnterMoveNextControl = True
        Me.INDdeDocumentDate.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDocumentDate.Name = "INDdeDocumentDate"
        Me.INDdeDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDocumentDate.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDdeDocumentDate.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDocumentDate, 0)
        Me.INDdeDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, False)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions3.Image = Global.Presentation.Portfolio.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        '
        'INDsleCurrency
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCurrency, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCurrency, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCurrency, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Location = New System.Drawing.Point(24, 270)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCurrency, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCurrency.Name = "INDsleCurrency"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCurrency.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCurrency.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCurrency.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseFont = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCurrency.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleCurrency.Properties.DisplayMember = "Abbreviation"
        Me.INDsleCurrency.Properties.NullText = ""
        Me.INDsleCurrency.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDsleCurrency.Properties.PopupSizeable = False
        Me.INDsleCurrency.Properties.PopupView = Me.INDGvCurrency
        Me.INDsleCurrency.Properties.ShowClearButton = False
        Me.INDsleCurrency.Properties.ShowFooter = False
        Me.INDsleCurrency.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCurrency, True)
        Me.INDsleCurrency.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCurrency.StyleController = Me.INDLcAccountReceivableDocument
        Me.INDsleCurrency.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCurrency, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCurrency, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCurrency, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCurrency, False)
        '
        'INDGvCurrency
        '
        Me.INDGvCurrency.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCurrency.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCurrency.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCurrency.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCurrency.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvCurrency.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCurrency.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCurrency.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCurrency.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCurrency.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCurrency.Appearance.Row.Options.UseFont = True
        Me.INDGvCurrency.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColAbbreviation, Me.INDColName})
        Me.INDGvCurrency.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCurrency.Name = "INDGvCurrency"
        Me.INDGvCurrency.OptionsFind.FindFilterColumns = "Code"
        Me.INDGvCurrency.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCurrency.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCurrency.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCurrency.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCurrency.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView3.SetTemaIndigoMetro(Me.INDGvCurrency, False)
        Me.IndigoGridView4.SetTemaIndigoMetro(Me.INDGvCurrency, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCurrency, False)
        Me.IndigoGridView21.SetTemaIndigoMetro(Me.INDGvCurrency, False)
        '
        'INDColAbbreviation
        '
        Me.INDColAbbreviation.Caption = "Abreviación"
        Me.INDColAbbreviation.FieldName = "Abbreviation"
        Me.INDColAbbreviation.Name = "INDColAbbreviation"
        Me.INDColAbbreviation.OptionsColumn.AllowEdit = False
        Me.INDColAbbreviation.OptionsColumn.AllowFocus = False
        Me.INDColAbbreviation.Visible = True
        Me.INDColAbbreviation.VisibleIndex = 0
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "CurrencyName"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.OptionsColumn.AllowFocus = False
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 1
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
        Me.LayoutControlGroup1.CustomizationFormText = "Documento de Cuenta x Cobrar"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgGeneralData, Me.INDlcgDataOptional, Me.INDlcgConcepts})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1700, 568)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlcgGeneralData
        '
        Me.INDlcgGeneralData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgGeneralData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgGeneralData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgGeneralData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgGeneralData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgGeneralData, False)
        Me.INDlcgGeneralData.CustomizationFormText = "Datos Principales"
        Me.INDlcgGeneralData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCode, Me.INDlciDocumentDate, Me.INDlciCustomerId, Me.INDlciInvoiceNumber, Me.INDlciObservation, Me.INDLciCurrency})
        Me.INDlcgGeneralData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgGeneralData.Name = "INDlcgGeneralData"
        Me.INDlcgGeneralData.Size = New System.Drawing.Size(414, 548)
        Me.INDlcgGeneralData.Text = "Datos Principales"
        '
        'INDlciCode
        '
        Me.INDlciCode.Control = Me.INDbtnCode
        Me.INDlciCode.CustomizationFormText = "Código"
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.Size = New System.Drawing.Size(390, 64)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'INDlciDocumentDate
        '
        Me.INDlciDocumentDate.Control = Me.INDdeDocumentDate
        Me.INDlciDocumentDate.CustomizationFormText = "Fecha Documento"
        Me.INDlciDocumentDate.Location = New System.Drawing.Point(0, 64)
        Me.INDlciDocumentDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.Name = "INDlciDocumentDate"
        Me.INDlciDocumentDate.ShowInCustomizationForm = False
        Me.INDlciDocumentDate.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDocumentDate.Text = "Fecha Documento"
        Me.INDlciDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDocumentDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciDocumentDate.TextToControlDistance = 5
        '
        'INDlciCustomerId
        '
        Me.INDlciCustomerId.Control = Me.INDsleCustomerId
        Me.INDlciCustomerId.CustomizationFormText = "Cliente"
        Me.INDlciCustomerId.Location = New System.Drawing.Point(0, 128)
        Me.INDlciCustomerId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciCustomerId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciCustomerId.Name = "INDlciCustomerId"
        Me.INDlciCustomerId.ShowInCustomizationForm = False
        Me.INDlciCustomerId.Size = New System.Drawing.Size(390, 64)
        Me.INDlciCustomerId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCustomerId.Text = "Cliente"
        Me.INDlciCustomerId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCustomerId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCustomerId.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciCustomerId.TextToControlDistance = 5
        '
        'INDlciInvoiceNumber
        '
        Me.INDlciInvoiceNumber.Control = Me.INDtxtInvoiceNumber
        Me.INDlciInvoiceNumber.CustomizationFormText = "Numero de Factura"
        Me.INDlciInvoiceNumber.Location = New System.Drawing.Point(0, 252)
        Me.INDlciInvoiceNumber.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciInvoiceNumber.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciInvoiceNumber.Name = "INDlciInvoiceNumber"
        Me.INDlciInvoiceNumber.ShowInCustomizationForm = False
        Me.INDlciInvoiceNumber.Size = New System.Drawing.Size(390, 64)
        Me.INDlciInvoiceNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciInvoiceNumber.Text = "Numero de Factura"
        Me.INDlciInvoiceNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciInvoiceNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciInvoiceNumber.TextSize = New System.Drawing.Size(131, 21)
        Me.INDlciInvoiceNumber.TextToControlDistance = 5
        '
        'INDlciObservation
        '
        Me.INDlciObservation.Control = Me.INDmeObservation
        Me.INDlciObservation.CustomizationFormText = "Observación"
        Me.INDlciObservation.Location = New System.Drawing.Point(0, 316)
        Me.INDlciObservation.MaxSize = New System.Drawing.Size(390, 80)
        Me.INDlciObservation.MinSize = New System.Drawing.Size(390, 80)
        Me.INDlciObservation.Name = "INDlciObservation"
        Me.INDlciObservation.Size = New System.Drawing.Size(390, 179)
        Me.INDlciObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciObservation.Text = "Observación"
        Me.INDlciObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciObservation.TextSize = New System.Drawing.Size(133, 21)
        Me.INDlciObservation.TextToControlDistance = 5
        '
        'INDLciCurrency
        '
        Me.INDLciCurrency.Control = Me.INDsleCurrency
        Me.INDLciCurrency.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciCurrency.CustomizationFormText = "Moneda"
        Me.INDLciCurrency.Location = New System.Drawing.Point(0, 192)
        Me.INDLciCurrency.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciCurrency.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciCurrency.Name = "INDLciCurrency"
        Me.INDLciCurrency.ShowInCustomizationForm = False
        Me.INDLciCurrency.Size = New System.Drawing.Size(390, 60)
        Me.INDLciCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCurrency.Text = "Moneda"
        Me.INDLciCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCurrency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCurrency.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciCurrency.TextToControlDistance = 5
        '
        'INDlcgDataOptional
        '
        Me.INDlcgDataOptional.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDataOptional.AppearanceGroup.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgDataOptional.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgDataOptional.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgDataOptional.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgDataOptional, False)
        Me.INDlcgDataOptional.CustomizationFormText = "Información Relacionada"
        Me.INDlcgDataOptional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCostCenterId, Me.INDlciTerm, Me.INDlciExpiredDate, Me.INDlciShare, Me.INDlciMainAccountId, Me.INDlciDebitValue, Me.INDlciValueCredit})
        Me.INDlcgDataOptional.Location = New System.Drawing.Point(414, 0)
        Me.INDlcgDataOptional.Name = "INDlcgDataOptional"
        Me.INDlcgDataOptional.Size = New System.Drawing.Size(414, 548)
        Me.INDlcgDataOptional.Text = "Información Relacionada"
        '
        'INDlciCostCenterId
        '
        Me.INDlciCostCenterId.Control = Me.INDsleCostCenterId
        Me.INDlciCostCenterId.CustomizationFormText = "Centro de Costo"
        Me.INDlciCostCenterId.Location = New System.Drawing.Point(0, 64)
        Me.INDlciCostCenterId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciCostCenterId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciCostCenterId.Name = "INDlciCostCenterId"
        Me.INDlciCostCenterId.Size = New System.Drawing.Size(390, 64)
        Me.INDlciCostCenterId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCostCenterId.Text = "Centro de Costo"
        Me.INDlciCostCenterId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCostCenterId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCostCenterId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciCostCenterId.TextToControlDistance = 5
        Me.INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciTerm
        '
        Me.INDlciTerm.Control = Me.INDspnTerm
        Me.INDlciTerm.CustomizationFormText = "Plazo (Días)"
        Me.INDlciTerm.Location = New System.Drawing.Point(0, 192)
        Me.INDlciTerm.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciTerm.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciTerm.Name = "INDlciTerm"
        Me.INDlciTerm.ShowInCustomizationForm = False
        Me.INDlciTerm.Size = New System.Drawing.Size(390, 64)
        Me.INDlciTerm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciTerm.Text = "Plazo (Días)"
        Me.INDlciTerm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciTerm.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciTerm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciTerm.TextToControlDistance = 12
        '
        'INDlciExpiredDate
        '
        Me.INDlciExpiredDate.Control = Me.INDdeExpiredDate
        Me.INDlciExpiredDate.CustomizationFormText = "Fecha Vencimiento"
        Me.INDlciExpiredDate.Location = New System.Drawing.Point(0, 256)
        Me.INDlciExpiredDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciExpiredDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciExpiredDate.Name = "INDlciExpiredDate"
        Me.INDlciExpiredDate.ShowInCustomizationForm = False
        Me.INDlciExpiredDate.Size = New System.Drawing.Size(390, 64)
        Me.INDlciExpiredDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciExpiredDate.Text = "Fecha Vencimiento"
        Me.INDlciExpiredDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciExpiredDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciExpiredDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciExpiredDate.TextToControlDistance = 12
        '
        'INDlciShare
        '
        Me.INDlciShare.Control = Me.INDspnShare
        Me.INDlciShare.CustomizationFormText = "Numero de Cuotas"
        Me.INDlciShare.Location = New System.Drawing.Point(0, 128)
        Me.INDlciShare.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciShare.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciShare.Name = "INDlciShare"
        Me.INDlciShare.ShowInCustomizationForm = False
        Me.INDlciShare.Size = New System.Drawing.Size(390, 64)
        Me.INDlciShare.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciShare.Text = "Numero de Cuotas"
        Me.INDlciShare.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciShare.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciShare.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciShare.TextToControlDistance = 5
        '
        'INDlciMainAccountId
        '
        Me.INDlciMainAccountId.Control = Me.INDsleMainAccountId
        Me.INDlciMainAccountId.CustomizationFormText = "Cuenta Contable"
        Me.INDlciMainAccountId.Location = New System.Drawing.Point(0, 0)
        Me.INDlciMainAccountId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciMainAccountId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciMainAccountId.Name = "INDlciMainAccountId"
        Me.INDlciMainAccountId.Size = New System.Drawing.Size(390, 64)
        Me.INDlciMainAccountId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciMainAccountId.Text = "Cuenta Contable"
        Me.INDlciMainAccountId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciMainAccountId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciMainAccountId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciMainAccountId.TextToControlDistance = 5
        '
        'INDlciDebitValue
        '
        Me.INDlciDebitValue.Control = Me.INDspnValueDebit
        Me.INDlciDebitValue.CustomizationFormText = "Valor Debito"
        Me.INDlciDebitValue.Location = New System.Drawing.Point(0, 320)
        Me.INDlciDebitValue.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciDebitValue.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciDebitValue.Name = "INDlciDebitValue"
        Me.INDlciDebitValue.Size = New System.Drawing.Size(390, 64)
        Me.INDlciDebitValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDebitValue.Text = "Valor Debito"
        Me.INDlciDebitValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDebitValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDebitValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciDebitValue.TextToControlDistance = 5
        '
        'INDlciValueCredit
        '
        Me.INDlciValueCredit.Control = Me.INDspnValueCredit
        Me.INDlciValueCredit.CustomizationFormText = "Valor Credito"
        Me.INDlciValueCredit.Location = New System.Drawing.Point(0, 384)
        Me.INDlciValueCredit.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciValueCredit.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciValueCredit.Name = "INDlciValueCredit"
        Me.INDlciValueCredit.Size = New System.Drawing.Size(390, 111)
        Me.INDlciValueCredit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValueCredit.Text = "Valor Credito"
        Me.INDlciValueCredit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValueCredit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValueCredit.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciValueCredit.TextToControlDistance = 5
        '
        'INDlcgConcepts
        '
        Me.INDlcgConcepts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgConcepts.AppearanceGroup.Options.UseFont = True
        Me.INDlcgConcepts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgConcepts.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgConcepts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgConcepts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgConcepts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgConcepts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgConcepts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgConcepts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgConcepts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgConcepts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgConcepts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgConcepts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgConcepts, False)
        Me.INDlcgConcepts.CustomizationFormText = "Conceptos"
        Me.INDlcgConcepts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.INDlcgConcepts.Location = New System.Drawing.Point(828, 0)
        Me.INDlcgConcepts.Name = "INDlcgConcepts"
        Me.INDlcgConcepts.Size = New System.Drawing.Size(852, 548)
        Me.INDlcgConcepts.Text = "Conceptos"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDbtnAdd
        Me.LayoutControlItem2.CustomizationFormText = "Agregar Conceptos"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(828, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDGcConcepts
        Me.LayoutControlItem3.CustomizationFormText = "Listado de Conceptos"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(828, 459)
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'GridColumn160
        '
        Me.GridColumn160.Caption = "Id"
        Me.GridColumn160.FieldName = "Id"
        Me.GridColumn160.Name = "GridColumn160"
        '
        'GridColumn161
        '
        Me.GridColumn161.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn161.Caption = "Código"
        Me.GridColumn161.FieldName = "Number"
        Me.GridColumn161.Name = "GridColumn161"
        Me.GridColumn161.Width = 77
        '
        'GridColumn162
        '
        Me.GridColumn162.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn162.Caption = "Nombre"
        Me.GridColumn162.FieldName = "Name"
        Me.GridColumn162.Name = "GridColumn162"
        Me.GridColumn162.Width = 89
        '
        'GridColumn163
        '
        Me.GridColumn163.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn163.Caption = "Maneja Tercero"
        Me.GridColumn163.FieldName = "HandlesThirdParty"
        Me.GridColumn163.Name = "GridColumn163"
        Me.GridColumn163.Width = 173
        '
        'GridColumn164
        '
        Me.GridColumn164.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn164.Caption = "Maneja Centro de Costo"
        Me.GridColumn164.FieldName = "HandlesCostCenter"
        Me.GridColumn164.Name = "GridColumn164"
        Me.GridColumn164.Width = 350
        '
        'GridColumn155
        '
        Me.GridColumn155.Caption = "Id"
        Me.GridColumn155.FieldName = "Id"
        Me.GridColumn155.Name = "GridColumn155"
        '
        'GridColumn156
        '
        Me.GridColumn156.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn156.Caption = "Código"
        Me.GridColumn156.FieldName = "Number"
        Me.GridColumn156.Name = "GridColumn156"
        Me.GridColumn156.Width = 77
        '
        'GridColumn157
        '
        Me.GridColumn157.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn157.Caption = "Nombre"
        Me.GridColumn157.FieldName = "Name"
        Me.GridColumn157.Name = "GridColumn157"
        Me.GridColumn157.Width = 89
        '
        'GridColumn158
        '
        Me.GridColumn158.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn158.Caption = "Maneja Tercero"
        Me.GridColumn158.FieldName = "HandlesThirdParty"
        Me.GridColumn158.Name = "GridColumn158"
        Me.GridColumn158.Width = 173
        '
        'GridColumn159
        '
        Me.GridColumn159.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn159.Caption = "Maneja Centro de Costo"
        Me.GridColumn159.FieldName = "HandlesCostCenter"
        Me.GridColumn159.Name = "GridColumn159"
        Me.GridColumn159.Width = 350
        '
        'GridColumn150
        '
        Me.GridColumn150.Caption = "Id"
        Me.GridColumn150.FieldName = "Id"
        Me.GridColumn150.Name = "GridColumn150"
        '
        'GridColumn151
        '
        Me.GridColumn151.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn151.Caption = "Código"
        Me.GridColumn151.FieldName = "Number"
        Me.GridColumn151.Name = "GridColumn151"
        Me.GridColumn151.Width = 77
        '
        'GridColumn152
        '
        Me.GridColumn152.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn152.Caption = "Nombre"
        Me.GridColumn152.FieldName = "Name"
        Me.GridColumn152.Name = "GridColumn152"
        Me.GridColumn152.Width = 89
        '
        'GridColumn153
        '
        Me.GridColumn153.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn153.Caption = "Maneja Tercero"
        Me.GridColumn153.FieldName = "HandlesThirdParty"
        Me.GridColumn153.Name = "GridColumn153"
        Me.GridColumn153.Width = 173
        '
        'GridColumn154
        '
        Me.GridColumn154.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn154.Caption = "Maneja Centro de Costo"
        Me.GridColumn154.FieldName = "HandlesCostCenter"
        Me.GridColumn154.Name = "GridColumn154"
        Me.GridColumn154.Width = 350
        '
        'GridColumn145
        '
        Me.GridColumn145.Caption = "Id"
        Me.GridColumn145.FieldName = "Id"
        Me.GridColumn145.Name = "GridColumn145"
        '
        'GridColumn146
        '
        Me.GridColumn146.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn146.Caption = "Código"
        Me.GridColumn146.FieldName = "Number"
        Me.GridColumn146.Name = "GridColumn146"
        Me.GridColumn146.Width = 77
        '
        'GridColumn147
        '
        Me.GridColumn147.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn147.Caption = "Nombre"
        Me.GridColumn147.FieldName = "Name"
        Me.GridColumn147.Name = "GridColumn147"
        Me.GridColumn147.Width = 89
        '
        'GridColumn148
        '
        Me.GridColumn148.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn148.Caption = "Maneja Tercero"
        Me.GridColumn148.FieldName = "HandlesThirdParty"
        Me.GridColumn148.Name = "GridColumn148"
        Me.GridColumn148.Width = 173
        '
        'GridColumn149
        '
        Me.GridColumn149.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn149.Caption = "Maneja Centro de Costo"
        Me.GridColumn149.FieldName = "HandlesCostCenter"
        Me.GridColumn149.Name = "GridColumn149"
        Me.GridColumn149.Width = 350
        '
        'GridColumn140
        '
        Me.GridColumn140.Caption = "Id"
        Me.GridColumn140.FieldName = "Id"
        Me.GridColumn140.Name = "GridColumn140"
        '
        'GridColumn141
        '
        Me.GridColumn141.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn141.Caption = "Código"
        Me.GridColumn141.FieldName = "Number"
        Me.GridColumn141.Name = "GridColumn141"
        Me.GridColumn141.Width = 77
        '
        'GridColumn142
        '
        Me.GridColumn142.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn142.Caption = "Nombre"
        Me.GridColumn142.FieldName = "Name"
        Me.GridColumn142.Name = "GridColumn142"
        Me.GridColumn142.Width = 89
        '
        'GridColumn143
        '
        Me.GridColumn143.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn143.Caption = "Maneja Tercero"
        Me.GridColumn143.FieldName = "HandlesThirdParty"
        Me.GridColumn143.Name = "GridColumn143"
        Me.GridColumn143.Width = 173
        '
        'GridColumn144
        '
        Me.GridColumn144.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn144.Caption = "Maneja Centro de Costo"
        Me.GridColumn144.FieldName = "HandlesCostCenter"
        Me.GridColumn144.Name = "GridColumn144"
        Me.GridColumn144.Width = 350
        '
        'GridColumn135
        '
        Me.GridColumn135.Caption = "Id"
        Me.GridColumn135.FieldName = "Id"
        Me.GridColumn135.Name = "GridColumn135"
        '
        'GridColumn136
        '
        Me.GridColumn136.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn136.Caption = "Código"
        Me.GridColumn136.FieldName = "Number"
        Me.GridColumn136.Name = "GridColumn136"
        Me.GridColumn136.Width = 77
        '
        'GridColumn137
        '
        Me.GridColumn137.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn137.Caption = "Nombre"
        Me.GridColumn137.FieldName = "Name"
        Me.GridColumn137.Name = "GridColumn137"
        Me.GridColumn137.Width = 89
        '
        'GridColumn138
        '
        Me.GridColumn138.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn138.Caption = "Maneja Tercero"
        Me.GridColumn138.FieldName = "HandlesThirdParty"
        Me.GridColumn138.Name = "GridColumn138"
        Me.GridColumn138.Width = 173
        '
        'GridColumn139
        '
        Me.GridColumn139.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn139.Caption = "Maneja Centro de Costo"
        Me.GridColumn139.FieldName = "HandlesCostCenter"
        Me.GridColumn139.Name = "GridColumn139"
        Me.GridColumn139.Width = 350
        '
        'GridColumn130
        '
        Me.GridColumn130.Caption = "Id"
        Me.GridColumn130.FieldName = "Id"
        Me.GridColumn130.Name = "GridColumn130"
        '
        'GridColumn131
        '
        Me.GridColumn131.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn131.Caption = "Código"
        Me.GridColumn131.FieldName = "Number"
        Me.GridColumn131.Name = "GridColumn131"
        Me.GridColumn131.Width = 77
        '
        'GridColumn132
        '
        Me.GridColumn132.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn132.Caption = "Nombre"
        Me.GridColumn132.FieldName = "Name"
        Me.GridColumn132.Name = "GridColumn132"
        Me.GridColumn132.Width = 89
        '
        'GridColumn133
        '
        Me.GridColumn133.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn133.Caption = "Maneja Tercero"
        Me.GridColumn133.FieldName = "HandlesThirdParty"
        Me.GridColumn133.Name = "GridColumn133"
        Me.GridColumn133.Width = 173
        '
        'GridColumn134
        '
        Me.GridColumn134.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn134.Caption = "Maneja Centro de Costo"
        Me.GridColumn134.FieldName = "HandlesCostCenter"
        Me.GridColumn134.Name = "GridColumn134"
        Me.GridColumn134.Width = 350
        '
        'GridColumn125
        '
        Me.GridColumn125.Caption = "Id"
        Me.GridColumn125.FieldName = "Id"
        Me.GridColumn125.Name = "GridColumn125"
        '
        'GridColumn126
        '
        Me.GridColumn126.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn126.Caption = "Código"
        Me.GridColumn126.FieldName = "Number"
        Me.GridColumn126.Name = "GridColumn126"
        Me.GridColumn126.Width = 77
        '
        'GridColumn127
        '
        Me.GridColumn127.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn127.Caption = "Nombre"
        Me.GridColumn127.FieldName = "Name"
        Me.GridColumn127.Name = "GridColumn127"
        Me.GridColumn127.Width = 89
        '
        'GridColumn128
        '
        Me.GridColumn128.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn128.Caption = "Maneja Tercero"
        Me.GridColumn128.FieldName = "HandlesThirdParty"
        Me.GridColumn128.Name = "GridColumn128"
        Me.GridColumn128.Width = 173
        '
        'GridColumn129
        '
        Me.GridColumn129.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn129.Caption = "Maneja Centro de Costo"
        Me.GridColumn129.FieldName = "HandlesCostCenter"
        Me.GridColumn129.Name = "GridColumn129"
        Me.GridColumn129.Width = 350
        '
        'GridColumn120
        '
        Me.GridColumn120.Caption = "Id"
        Me.GridColumn120.FieldName = "Id"
        Me.GridColumn120.Name = "GridColumn120"
        '
        'GridColumn121
        '
        Me.GridColumn121.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn121.Caption = "Código"
        Me.GridColumn121.FieldName = "Number"
        Me.GridColumn121.Name = "GridColumn121"
        Me.GridColumn121.Width = 77
        '
        'GridColumn122
        '
        Me.GridColumn122.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn122.Caption = "Nombre"
        Me.GridColumn122.FieldName = "Name"
        Me.GridColumn122.Name = "GridColumn122"
        Me.GridColumn122.Width = 89
        '
        'GridColumn123
        '
        Me.GridColumn123.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn123.Caption = "Maneja Tercero"
        Me.GridColumn123.FieldName = "HandlesThirdParty"
        Me.GridColumn123.Name = "GridColumn123"
        Me.GridColumn123.Width = 173
        '
        'GridColumn124
        '
        Me.GridColumn124.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn124.Caption = "Maneja Centro de Costo"
        Me.GridColumn124.FieldName = "HandlesCostCenter"
        Me.GridColumn124.Name = "GridColumn124"
        Me.GridColumn124.Width = 350
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Id"
        Me.GridColumn6.FieldName = "Id"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'GridColumn116
        '
        Me.GridColumn116.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn116.Caption = "Código"
        Me.GridColumn116.FieldName = "Number"
        Me.GridColumn116.Name = "GridColumn116"
        Me.GridColumn116.Width = 77
        '
        'GridColumn117
        '
        Me.GridColumn117.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn117.Caption = "Nombre"
        Me.GridColumn117.FieldName = "Name"
        Me.GridColumn117.Name = "GridColumn117"
        Me.GridColumn117.Width = 89
        '
        'GridColumn118
        '
        Me.GridColumn118.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn118.Caption = "Maneja Tercero"
        Me.GridColumn118.FieldName = "HandlesThirdParty"
        Me.GridColumn118.Name = "GridColumn118"
        Me.GridColumn118.Width = 173
        '
        'GridColumn119
        '
        Me.GridColumn119.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn119.Caption = "Maneja Centro de Costo"
        Me.GridColumn119.FieldName = "HandlesCostCenter"
        Me.GridColumn119.Name = "GridColumn119"
        Me.GridColumn119.Width = 350
        '
        'GridColumn111
        '
        Me.GridColumn111.Caption = "Id"
        Me.GridColumn111.FieldName = "Id"
        Me.GridColumn111.Name = "GridColumn111"
        '
        'GridColumn112
        '
        Me.GridColumn112.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn112.Caption = "Código"
        Me.GridColumn112.FieldName = "Number"
        Me.GridColumn112.Name = "GridColumn112"
        Me.GridColumn112.Width = 77
        '
        'GridColumn113
        '
        Me.GridColumn113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn113.Caption = "Nombre"
        Me.GridColumn113.FieldName = "Name"
        Me.GridColumn113.Name = "GridColumn113"
        Me.GridColumn113.Width = 89
        '
        'GridColumn114
        '
        Me.GridColumn114.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn114.Caption = "Maneja Tercero"
        Me.GridColumn114.FieldName = "HandlesThirdParty"
        Me.GridColumn114.Name = "GridColumn114"
        Me.GridColumn114.Width = 173
        '
        'GridColumn115
        '
        Me.GridColumn115.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn115.Caption = "Maneja Centro de Costo"
        Me.GridColumn115.FieldName = "HandlesCostCenter"
        Me.GridColumn115.Name = "GridColumn115"
        Me.GridColumn115.Width = 350
        '
        'GridColumn106
        '
        Me.GridColumn106.Caption = "Id"
        Me.GridColumn106.FieldName = "Id"
        Me.GridColumn106.Name = "GridColumn106"
        '
        'GridColumn107
        '
        Me.GridColumn107.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn107.Caption = "Código"
        Me.GridColumn107.FieldName = "Number"
        Me.GridColumn107.Name = "GridColumn107"
        Me.GridColumn107.Width = 77
        '
        'GridColumn108
        '
        Me.GridColumn108.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn108.Caption = "Nombre"
        Me.GridColumn108.FieldName = "Name"
        Me.GridColumn108.Name = "GridColumn108"
        Me.GridColumn108.Width = 89
        '
        'GridColumn109
        '
        Me.GridColumn109.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn109.Caption = "Maneja Tercero"
        Me.GridColumn109.FieldName = "HandlesThirdParty"
        Me.GridColumn109.Name = "GridColumn109"
        Me.GridColumn109.Width = 173
        '
        'GridColumn110
        '
        Me.GridColumn110.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn110.Caption = "Maneja Centro de Costo"
        Me.GridColumn110.FieldName = "HandlesCostCenter"
        Me.GridColumn110.Name = "GridColumn110"
        Me.GridColumn110.Width = 350
        '
        'GridColumn101
        '
        Me.GridColumn101.Caption = "Id"
        Me.GridColumn101.FieldName = "Id"
        Me.GridColumn101.Name = "GridColumn101"
        '
        'GridColumn102
        '
        Me.GridColumn102.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn102.Caption = "Código"
        Me.GridColumn102.FieldName = "Number"
        Me.GridColumn102.Name = "GridColumn102"
        Me.GridColumn102.Width = 77
        '
        'GridColumn103
        '
        Me.GridColumn103.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn103.Caption = "Nombre"
        Me.GridColumn103.FieldName = "Name"
        Me.GridColumn103.Name = "GridColumn103"
        Me.GridColumn103.Width = 89
        '
        'GridColumn104
        '
        Me.GridColumn104.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn104.Caption = "Maneja Tercero"
        Me.GridColumn104.FieldName = "HandlesThirdParty"
        Me.GridColumn104.Name = "GridColumn104"
        Me.GridColumn104.Width = 173
        '
        'GridColumn105
        '
        Me.GridColumn105.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn105.Caption = "Maneja Centro de Costo"
        Me.GridColumn105.FieldName = "HandlesCostCenter"
        Me.GridColumn105.Name = "GridColumn105"
        Me.GridColumn105.Width = 350
        '
        'GridColumn96
        '
        Me.GridColumn96.Caption = "Id"
        Me.GridColumn96.FieldName = "Id"
        Me.GridColumn96.Name = "GridColumn96"
        '
        'GridColumn97
        '
        Me.GridColumn97.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn97.Caption = "Código"
        Me.GridColumn97.FieldName = "Number"
        Me.GridColumn97.Name = "GridColumn97"
        Me.GridColumn97.Width = 77
        '
        'GridColumn98
        '
        Me.GridColumn98.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn98.Caption = "Nombre"
        Me.GridColumn98.FieldName = "Name"
        Me.GridColumn98.Name = "GridColumn98"
        Me.GridColumn98.Width = 89
        '
        'GridColumn99
        '
        Me.GridColumn99.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn99.Caption = "Maneja Tercero"
        Me.GridColumn99.FieldName = "HandlesThirdParty"
        Me.GridColumn99.Name = "GridColumn99"
        Me.GridColumn99.Width = 173
        '
        'GridColumn100
        '
        Me.GridColumn100.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn100.Caption = "Maneja Centro de Costo"
        Me.GridColumn100.FieldName = "HandlesCostCenter"
        Me.GridColumn100.Name = "GridColumn100"
        Me.GridColumn100.Width = 350
        '
        'GridColumn91
        '
        Me.GridColumn91.Caption = "Id"
        Me.GridColumn91.FieldName = "Id"
        Me.GridColumn91.Name = "GridColumn91"
        '
        'GridColumn92
        '
        Me.GridColumn92.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn92.Caption = "Código"
        Me.GridColumn92.FieldName = "Number"
        Me.GridColumn92.Name = "GridColumn92"
        Me.GridColumn92.Width = 77
        '
        'GridColumn93
        '
        Me.GridColumn93.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn93.Caption = "Nombre"
        Me.GridColumn93.FieldName = "Name"
        Me.GridColumn93.Name = "GridColumn93"
        Me.GridColumn93.Width = 89
        '
        'GridColumn94
        '
        Me.GridColumn94.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn94.Caption = "Maneja Tercero"
        Me.GridColumn94.FieldName = "HandlesThirdParty"
        Me.GridColumn94.Name = "GridColumn94"
        Me.GridColumn94.Width = 173
        '
        'GridColumn95
        '
        Me.GridColumn95.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn95.Caption = "Maneja Centro de Costo"
        Me.GridColumn95.FieldName = "HandlesCostCenter"
        Me.GridColumn95.Name = "GridColumn95"
        Me.GridColumn95.Width = 350
        '
        'GridColumn86
        '
        Me.GridColumn86.Caption = "Id"
        Me.GridColumn86.FieldName = "Id"
        Me.GridColumn86.Name = "GridColumn86"
        '
        'GridColumn87
        '
        Me.GridColumn87.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn87.Caption = "Código"
        Me.GridColumn87.FieldName = "Number"
        Me.GridColumn87.Name = "GridColumn87"
        Me.GridColumn87.Width = 80
        '
        'GridColumn88
        '
        Me.GridColumn88.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn88.Caption = "Nombre"
        Me.GridColumn88.FieldName = "Name"
        Me.GridColumn88.Name = "GridColumn88"
        Me.GridColumn88.Width = 91
        '
        'GridColumn89
        '
        Me.GridColumn89.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn89.Caption = "Maneja Tercero"
        Me.GridColumn89.FieldName = "HandlesThirdParty"
        Me.GridColumn89.Name = "GridColumn89"
        Me.GridColumn89.Width = 174
        '
        'GridColumn90
        '
        Me.GridColumn90.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn90.Caption = "Maneja Centro de Costo"
        Me.GridColumn90.FieldName = "HandlesCostCenter"
        Me.GridColumn90.Name = "GridColumn90"
        Me.GridColumn90.Width = 350
        '
        'GridColumn81
        '
        Me.GridColumn81.Caption = "Id"
        Me.GridColumn81.FieldName = "Id"
        Me.GridColumn81.Name = "GridColumn81"
        '
        'GridColumn82
        '
        Me.GridColumn82.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn82.Caption = "Código"
        Me.GridColumn82.FieldName = "Number"
        Me.GridColumn82.Name = "GridColumn82"
        Me.GridColumn82.Width = 77
        '
        'GridColumn83
        '
        Me.GridColumn83.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn83.Caption = "Nombre"
        Me.GridColumn83.FieldName = "Name"
        Me.GridColumn83.Name = "GridColumn83"
        Me.GridColumn83.Width = 89
        '
        'GridColumn84
        '
        Me.GridColumn84.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn84.Caption = "Maneja Tercero"
        Me.GridColumn84.FieldName = "HandlesThirdParty"
        Me.GridColumn84.Name = "GridColumn84"
        Me.GridColumn84.Width = 173
        '
        'GridColumn85
        '
        Me.GridColumn85.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn85.Caption = "Maneja Centro de Costo"
        Me.GridColumn85.FieldName = "HandlesCostCenter"
        Me.GridColumn85.Name = "GridColumn85"
        Me.GridColumn85.Width = 350
        '
        'GridColumn76
        '
        Me.GridColumn76.Caption = "Id"
        Me.GridColumn76.FieldName = "Id"
        Me.GridColumn76.Name = "GridColumn76"
        '
        'GridColumn77
        '
        Me.GridColumn77.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn77.Caption = "Código"
        Me.GridColumn77.FieldName = "Number"
        Me.GridColumn77.Name = "GridColumn77"
        Me.GridColumn77.Width = 79
        '
        'GridColumn78
        '
        Me.GridColumn78.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn78.Caption = "Nombre"
        Me.GridColumn78.FieldName = "Name"
        Me.GridColumn78.Name = "GridColumn78"
        Me.GridColumn78.Width = 91
        '
        'GridColumn79
        '
        Me.GridColumn79.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn79.Caption = "Maneja Tercero"
        Me.GridColumn79.FieldName = "HandlesThirdParty"
        Me.GridColumn79.Name = "GridColumn79"
        Me.GridColumn79.Width = 174
        '
        'GridColumn80
        '
        Me.GridColumn80.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn80.Caption = "Maneja Centro de Costo"
        Me.GridColumn80.FieldName = "HandlesCostCenter"
        Me.GridColumn80.Name = "GridColumn80"
        Me.GridColumn80.Width = 350
        '
        'GridColumn71
        '
        Me.GridColumn71.Caption = "Id"
        Me.GridColumn71.FieldName = "Id"
        Me.GridColumn71.Name = "GridColumn71"
        '
        'GridColumn72
        '
        Me.GridColumn72.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn72.Caption = "Código"
        Me.GridColumn72.FieldName = "Number"
        Me.GridColumn72.Name = "GridColumn72"
        Me.GridColumn72.Width = 80
        '
        'GridColumn73
        '
        Me.GridColumn73.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn73.Caption = "Nombre"
        Me.GridColumn73.FieldName = "Name"
        Me.GridColumn73.Name = "GridColumn73"
        Me.GridColumn73.Width = 91
        '
        'GridColumn74
        '
        Me.GridColumn74.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn74.Caption = "Maneja Tercero"
        Me.GridColumn74.FieldName = "HandlesThirdParty"
        Me.GridColumn74.Name = "GridColumn74"
        Me.GridColumn74.Width = 174
        '
        'GridColumn75
        '
        Me.GridColumn75.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn75.Caption = "Maneja Centro de Costo"
        Me.GridColumn75.FieldName = "HandlesCostCenter"
        Me.GridColumn75.Name = "GridColumn75"
        Me.GridColumn75.Width = 350
        '
        'GridColumn66
        '
        Me.GridColumn66.Caption = "Id"
        Me.GridColumn66.FieldName = "Id"
        Me.GridColumn66.Name = "GridColumn66"
        '
        'GridColumn67
        '
        Me.GridColumn67.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn67.Caption = "Código"
        Me.GridColumn67.FieldName = "Number"
        Me.GridColumn67.Name = "GridColumn67"
        Me.GridColumn67.Width = 300
        '
        'GridColumn68
        '
        Me.GridColumn68.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn68.Caption = "Nombre"
        Me.GridColumn68.FieldName = "Name"
        Me.GridColumn68.Name = "GridColumn68"
        Me.GridColumn68.Width = 350
        '
        'GridColumn69
        '
        Me.GridColumn69.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn69.Caption = "Maneja Tercero"
        Me.GridColumn69.FieldName = "HandlesThirdParty"
        Me.GridColumn69.Name = "GridColumn69"
        Me.GridColumn69.Width = 350
        '
        'GridColumn70
        '
        Me.GridColumn70.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn70.Caption = "Maneja Centro de Costo"
        Me.GridColumn70.FieldName = "HandlesCostCenter"
        Me.GridColumn70.Name = "GridColumn70"
        Me.GridColumn70.Width = 350
        '
        'GridColumn61
        '
        Me.GridColumn61.Caption = "Id"
        Me.GridColumn61.FieldName = "Id"
        Me.GridColumn61.Name = "GridColumn61"
        '
        'GridColumn62
        '
        Me.GridColumn62.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn62.Caption = "Código"
        Me.GridColumn62.FieldName = "Number"
        Me.GridColumn62.Name = "GridColumn62"
        Me.GridColumn62.Width = 300
        '
        'GridColumn63
        '
        Me.GridColumn63.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn63.Caption = "Nombre"
        Me.GridColumn63.FieldName = "Name"
        Me.GridColumn63.Name = "GridColumn63"
        Me.GridColumn63.Width = 350
        '
        'GridColumn64
        '
        Me.GridColumn64.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn64.Caption = "Maneja Tercero"
        Me.GridColumn64.FieldName = "HandlesThirdParty"
        Me.GridColumn64.Name = "GridColumn64"
        Me.GridColumn64.Width = 350
        '
        'GridColumn65
        '
        Me.GridColumn65.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn65.Caption = "Maneja Centro de Costo"
        Me.GridColumn65.FieldName = "HandlesCostCenter"
        Me.GridColumn65.Name = "GridColumn65"
        Me.GridColumn65.Width = 350
        '
        'GridColumn56
        '
        Me.GridColumn56.Caption = "Id"
        Me.GridColumn56.FieldName = "Id"
        Me.GridColumn56.Name = "GridColumn56"
        '
        'GridColumn57
        '
        Me.GridColumn57.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn57.Caption = "Código"
        Me.GridColumn57.FieldName = "Number"
        Me.GridColumn57.Name = "GridColumn57"
        Me.GridColumn57.Width = 300
        '
        'GridColumn58
        '
        Me.GridColumn58.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn58.Caption = "Nombre"
        Me.GridColumn58.FieldName = "Name"
        Me.GridColumn58.Name = "GridColumn58"
        Me.GridColumn58.Width = 350
        '
        'GridColumn59
        '
        Me.GridColumn59.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn59.Caption = "Maneja Tercero"
        Me.GridColumn59.FieldName = "HandlesThirdParty"
        Me.GridColumn59.Name = "GridColumn59"
        Me.GridColumn59.Width = 350
        '
        'GridColumn60
        '
        Me.GridColumn60.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn60.Caption = "Maneja Centro de Costo"
        Me.GridColumn60.FieldName = "HandlesCostCenter"
        Me.GridColumn60.Name = "GridColumn60"
        Me.GridColumn60.Width = 350
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "Id"
        Me.GridColumn51.FieldName = "Id"
        Me.GridColumn51.Name = "GridColumn51"
        '
        'GridColumn52
        '
        Me.GridColumn52.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn52.Caption = "Código"
        Me.GridColumn52.FieldName = "Number"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.Width = 300
        '
        'GridColumn53
        '
        Me.GridColumn53.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn53.Caption = "Nombre"
        Me.GridColumn53.FieldName = "Name"
        Me.GridColumn53.Name = "GridColumn53"
        Me.GridColumn53.Width = 350
        '
        'GridColumn54
        '
        Me.GridColumn54.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn54.Caption = "Maneja Tercero"
        Me.GridColumn54.FieldName = "HandlesThirdParty"
        Me.GridColumn54.Name = "GridColumn54"
        Me.GridColumn54.Width = 350
        '
        'GridColumn55
        '
        Me.GridColumn55.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn55.Caption = "Maneja Centro de Costo"
        Me.GridColumn55.FieldName = "HandlesCostCenter"
        Me.GridColumn55.Name = "GridColumn55"
        Me.GridColumn55.Width = 350
        '
        'GridColumn46
        '
        Me.GridColumn46.Caption = "Id"
        Me.GridColumn46.FieldName = "Id"
        Me.GridColumn46.Name = "GridColumn46"
        '
        'GridColumn47
        '
        Me.GridColumn47.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn47.Caption = "Código"
        Me.GridColumn47.FieldName = "Number"
        Me.GridColumn47.Name = "GridColumn47"
        Me.GridColumn47.Width = 300
        '
        'GridColumn48
        '
        Me.GridColumn48.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn48.Caption = "Nombre"
        Me.GridColumn48.FieldName = "Name"
        Me.GridColumn48.Name = "GridColumn48"
        Me.GridColumn48.Width = 350
        '
        'GridColumn49
        '
        Me.GridColumn49.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn49.Caption = "Maneja Tercero"
        Me.GridColumn49.FieldName = "HandlesThirdParty"
        Me.GridColumn49.Name = "GridColumn49"
        Me.GridColumn49.Width = 350
        '
        'GridColumn50
        '
        Me.GridColumn50.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn50.Caption = "Maneja Centro de Costo"
        Me.GridColumn50.FieldName = "HandlesCostCenter"
        Me.GridColumn50.Name = "GridColumn50"
        Me.GridColumn50.Width = 350
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "Id"
        Me.GridColumn41.FieldName = "Id"
        Me.GridColumn41.Name = "GridColumn41"
        '
        'GridColumn42
        '
        Me.GridColumn42.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn42.Caption = "Código"
        Me.GridColumn42.FieldName = "Number"
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.Width = 300
        '
        'GridColumn43
        '
        Me.GridColumn43.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn43.Caption = "Nombre"
        Me.GridColumn43.FieldName = "Name"
        Me.GridColumn43.Name = "GridColumn43"
        Me.GridColumn43.Width = 350
        '
        'GridColumn44
        '
        Me.GridColumn44.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn44.Caption = "Maneja Tercero"
        Me.GridColumn44.FieldName = "HandlesThirdParty"
        Me.GridColumn44.Name = "GridColumn44"
        Me.GridColumn44.Width = 350
        '
        'GridColumn45
        '
        Me.GridColumn45.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn45.Caption = "Maneja Centro de Costo"
        Me.GridColumn45.FieldName = "HandlesCostCenter"
        Me.GridColumn45.Name = "GridColumn45"
        Me.GridColumn45.Width = 350
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "Id"
        Me.GridColumn36.FieldName = "Id"
        Me.GridColumn36.Name = "GridColumn36"
        '
        'GridColumn37
        '
        Me.GridColumn37.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn37.Caption = "Código"
        Me.GridColumn37.FieldName = "Number"
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.Width = 300
        '
        'GridColumn38
        '
        Me.GridColumn38.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn38.Caption = "Nombre"
        Me.GridColumn38.FieldName = "Name"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.Width = 350
        '
        'GridColumn39
        '
        Me.GridColumn39.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn39.Caption = "Maneja Tercero"
        Me.GridColumn39.FieldName = "HandlesThirdParty"
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.Width = 350
        '
        'GridColumn40
        '
        Me.GridColumn40.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn40.Caption = "Maneja Centro de Costo"
        Me.GridColumn40.FieldName = "HandlesCostCenter"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.Width = 350
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Id"
        Me.GridColumn31.FieldName = "Id"
        Me.GridColumn31.Name = "GridColumn31"
        '
        'GridColumn32
        '
        Me.GridColumn32.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn32.Caption = "Código"
        Me.GridColumn32.FieldName = "Number"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.Width = 300
        '
        'GridColumn33
        '
        Me.GridColumn33.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn33.Caption = "Nombre"
        Me.GridColumn33.FieldName = "Name"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.Width = 350
        '
        'GridColumn34
        '
        Me.GridColumn34.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn34.Caption = "Maneja Tercero"
        Me.GridColumn34.FieldName = "HandlesThirdParty"
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.Width = 350
        '
        'GridColumn35
        '
        Me.GridColumn35.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn35.Caption = "Maneja Centro de Costo"
        Me.GridColumn35.FieldName = "HandlesCostCenter"
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Width = 350
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Id"
        Me.GridColumn26.FieldName = "Id"
        Me.GridColumn26.Name = "GridColumn26"
        '
        'GridColumn27
        '
        Me.GridColumn27.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn27.Caption = "Código"
        Me.GridColumn27.FieldName = "Number"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Width = 300
        '
        'GridColumn28
        '
        Me.GridColumn28.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn28.Caption = "Nombre"
        Me.GridColumn28.FieldName = "Name"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Width = 350
        '
        'GridColumn29
        '
        Me.GridColumn29.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn29.Caption = "Maneja Tercero"
        Me.GridColumn29.FieldName = "HandlesThirdParty"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.Width = 350
        '
        'GridColumn30
        '
        Me.GridColumn30.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn30.Caption = "Maneja Centro de Costo"
        Me.GridColumn30.FieldName = "HandlesCostCenter"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.Width = 350
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Id"
        Me.GridColumn21.FieldName = "Id"
        Me.GridColumn21.Name = "GridColumn21"
        '
        'GridColumn22
        '
        Me.GridColumn22.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn22.Caption = "Código"
        Me.GridColumn22.FieldName = "Number"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Width = 300
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.Caption = "Nombre"
        Me.GridColumn23.FieldName = "Name"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.Width = 350
        '
        'GridColumn24
        '
        Me.GridColumn24.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn24.Caption = "Maneja Tercero"
        Me.GridColumn24.FieldName = "HandlesThirdParty"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Width = 350
        '
        'GridColumn25
        '
        Me.GridColumn25.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn25.Caption = "Maneja Centro de Costo"
        Me.GridColumn25.FieldName = "HandlesCostCenter"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Width = 350
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Id"
        Me.GridColumn16.FieldName = "Id"
        Me.GridColumn16.Name = "GridColumn16"
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn17.Caption = "Código"
        Me.GridColumn17.FieldName = "Number"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Width = 300
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn18.Caption = "Nombre"
        Me.GridColumn18.FieldName = "Name"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Width = 350
        '
        'GridColumn19
        '
        Me.GridColumn19.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19.Caption = "Maneja Tercero"
        Me.GridColumn19.FieldName = "HandlesThirdParty"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Width = 350
        '
        'GridColumn20
        '
        Me.GridColumn20.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn20.Caption = "Maneja Centro de Costo"
        Me.GridColumn20.FieldName = "HandlesCostCenter"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Width = 350
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Id"
        Me.GridColumn11.FieldName = "Id"
        Me.GridColumn11.Name = "GridColumn11"
        '
        'GridColumn12
        '
        Me.GridColumn12.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn12.Caption = "Código"
        Me.GridColumn12.FieldName = "Number"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Width = 300
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn13.Caption = "Nombre"
        Me.GridColumn13.FieldName = "Name"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Width = 350
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn14.Caption = "Maneja Tercero"
        Me.GridColumn14.FieldName = "HandlesThirdParty"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Width = 350
        '
        'GridColumn15
        '
        Me.GridColumn15.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn15.Caption = "Maneja Centro de Costo"
        Me.GridColumn15.FieldName = "HandlesCostCenter"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Width = 350
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'GridColumn561
        '
        Me.GridColumn561.Caption = "Id"
        Me.GridColumn561.FieldName = "Id"
        Me.GridColumn561.Name = "GridColumn561"
        '
        'GridColumn562
        '
        Me.GridColumn562.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn562.Caption = "Código"
        Me.GridColumn562.FieldName = "Number"
        Me.GridColumn562.Name = "GridColumn562"
        Me.GridColumn562.Width = 300
        '
        'GridColumn563
        '
        Me.GridColumn563.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn563.Caption = "Nombre"
        Me.GridColumn563.FieldName = "Name"
        Me.GridColumn563.Name = "GridColumn563"
        Me.GridColumn563.Width = 350
        '
        'GridColumn564
        '
        Me.GridColumn564.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn564.Caption = "Maneja Tercero"
        Me.GridColumn564.FieldName = "HandlesThirdParty"
        Me.GridColumn564.Name = "GridColumn564"
        Me.GridColumn564.Width = 350
        '
        'GridColumn565
        '
        Me.GridColumn565.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn565.Caption = "Maneja Centro de Costo"
        Me.GridColumn565.FieldName = "HandlesCostCenter"
        Me.GridColumn565.Name = "GridColumn565"
        Me.GridColumn565.Width = 350
        '
        'IndigoGridView4
        '
        Me.IndigoGridView4.RaiseMenuPopUp = True
        Me.IndigoGridView4.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit11
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'IndigoGridView21
        '
        Me.IndigoGridView21.RaiseMenuPopUp = True
        Me.IndigoGridView21.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit21
        '
        'RepositoryItemPopupContainerEdit21
        '
        Me.RepositoryItemPopupContainerEdit21.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit21.Name = "RepositoryItemPopupContainerEdit21"
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'IndigoGridView3
        '
        Me.IndigoGridView3.RaiseMenuPopUp = True
        Me.IndigoGridView3.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit4
        '
        'RepositoryItemPopupContainerEdit4
        '
        Me.RepositoryItemPopupContainerEdit4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit4.Name = "RepositoryItemPopupContainerEdit4"
        '
        'FrmAccountReceivableDocument
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmAccountReceivableDocument"
        Me.Opacity = 1.0R
        Me.Tag = "1522"
        Me.Text = "Documento de Cuenta x Cobrar"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcAccountReceivableDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcAccountReceivableDocument.ResumeLayout(False)
        CType(Me.INDspnValueCredit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnValueDebit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleMainAccountId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleMainAccountId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnShare.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcConcepts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvConcepts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDcbeNature, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeExpiredDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeExpiredDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspnTerm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtInvoiceNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCostCenterId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCustomerId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgGeneralData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCustomerId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciInvoiceNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgDataOptional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCostCenterId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciTerm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciExpiredDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciShare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciMainAccountId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDebitValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValueCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgConcepts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcAccountReceivableDocument As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlciDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCustomerId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDlciCustomerId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleCostCenterId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciCostCenterId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtInvoiceNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciInvoiceNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeExpiredDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDspnTerm As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciTerm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciExpiredDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDlcgGeneralData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlcgDataOptional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcConcepts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvConcepts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgConcepts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDspnShare As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciShare As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleMainAccountId As Presentation.Controls.CtrPUC
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlciMainAccountId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn561 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn562 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn563 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn564 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn565 As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents INDcbeNature As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDspnValueCredit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDspnValueDebit As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciDebitValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciValueCredit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn59 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn60 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn62 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn63 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn64 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn65 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn66 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn67 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn68 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn69 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn70 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn72 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn73 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn77 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn78 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn79 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn80 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn81 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn82 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn83 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn84 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn85 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn86 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn87 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn88 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn89 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn90 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn91 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn92 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn93 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn94 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn95 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn96 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn97 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn98 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn99 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn100 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn101 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn102 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn103 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn104 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn105 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridView4 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridView3 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridView21 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit21 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleCurrency As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCurrency As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColAbbreviation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciCurrency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn106 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn107 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn108 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn109 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn110 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn114 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn115 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn116 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn117 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn118 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn119 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn120 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn121 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn122 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn123 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn124 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn125 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn126 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn127 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn128 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn129 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn130 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn131 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn132 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn133 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn134 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn135 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn136 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn137 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn138 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn139 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn140 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn141 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn142 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn143 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn144 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn145 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn146 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn147 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn148 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn149 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn150 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn151 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn152 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn153 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn154 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn155 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn156 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn157 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn158 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn159 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn160 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn161 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn162 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn163 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn164 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn165 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn166 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn167 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn168 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn169 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn170 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn171 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn172 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn173 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn174 As DevExpress.XtraGrid.Columns.GridColumn
End Class
