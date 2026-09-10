Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmInventoryContractModification
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyContract = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleBudgetaryValidityId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn187 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn188 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn189 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn190 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleBudgetaryEntityId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn185 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn186 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdeDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDbtnAddAvailability = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleAvailability = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewSearchAvailability = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn147 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn149 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn150 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn151 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn152 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcAvailability = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAvailability = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn154 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn155 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn156 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn158 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn159 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn160 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepTxtValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDSleContract = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn76 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdeEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDGleModificationType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGlvContractType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCodeContractType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMmoDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtValue = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGpMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciContract = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGpDetaill = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciModificationType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygBudget = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridAvailability = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAvailability = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddAvailability = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBudgetaryEntityId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBudgetaryValidityId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn194 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn195 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn196 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn191 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn192 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn193 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn182 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn183 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn184 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn179 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn180 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn181 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn176 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn177 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn178 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn173 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn174 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn175 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn170 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn171 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn172 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn168 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn169 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn166 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn167 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn164 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn165 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn157 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn163 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn153 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn162 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn148 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn161 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn145 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn146 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn143 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn144 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn141 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn142 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn139 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn140 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn137 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn138 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn135 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn136 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn133 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn134 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn131 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn132 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn129 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn130 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn127 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn125 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn126 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn123 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn124 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn121 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn122 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn119 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn120 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn117 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn118 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn115 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn116 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn114 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn109 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn110 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn107 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn108 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn105 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn106 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn103 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn104 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn101 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn102 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn99 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn100 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn97 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn98 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn95 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn96 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn93 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn94 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn91 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn92 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn90 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn87 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn86 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn83 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn84 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn81 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn82 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn79 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn80 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn77 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn78 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn72 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn70 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn66 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn67 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn64 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn65 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn62 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn63 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn60 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn59 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.GridColumn210 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.GridColumn1691 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyContract.SuspendLayout()
        CType(Me.INDSleBudgetaryValidityId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleBudgetaryEntityId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAvailability.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewSearchAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleContract.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleModificationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGlvContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMmoDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGpMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGpDetaill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciModificationType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBudgetaryEntityId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBudgetaryValidityId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyContract)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 121)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1554, 666)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 4)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ToolBars.Size = New System.Drawing.Size(1554, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1554, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyContract
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 6)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 658)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyContract
        '
        Me.INDlyContract.AllowCustomization = False
        Me.INDlyContract.Controls.Add(Me.INDSleBudgetaryValidityId)
        Me.INDlyContract.Controls.Add(Me.INDSleBudgetaryEntityId)
        Me.INDlyContract.Controls.Add(Me.INDdeDocumentDate)
        Me.INDlyContract.Controls.Add(Me.INDbtnAddAvailability)
        Me.INDlyContract.Controls.Add(Me.INDsleAvailability)
        Me.INDlyContract.Controls.Add(Me.INDgcAvailability)
        Me.INDlyContract.Controls.Add(Me.INDSleContract)
        Me.INDlyContract.Controls.Add(Me.INDdeEndDate)
        Me.INDlyContract.Controls.Add(Me.INDBtnCode)
        Me.INDlyContract.Controls.Add(Me.INDGleModificationType)
        Me.INDlyContract.Controls.Add(Me.INDMmoDescription)
        Me.INDlyContract.Controls.Add(Me.INDtxtValue)
        Me.INDlyContract.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyContract, False)
        Me.INDlyContract.Location = New System.Drawing.Point(202, 6)
        Me.INDlyContract.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyContract.Name = "INDlyContract"
        Me.INDlyContract.Root = Me.LayoutControlGroup1
        Me.INDlyContract.Size = New System.Drawing.Size(1350, 658)
        Me.INDlyContract.TabIndex = 1
        Me.INDlyContract.Text = "LayoutControl1"
        '
        'INDSleBudgetaryValidityId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleBudgetaryValidityId, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleBudgetaryValidityId, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.INDSleBudgetaryValidityId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.INDSleBudgetaryValidityId.Location = New System.Drawing.Point(687, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleBudgetaryValidityId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleBudgetaryValidityId.Name = "INDSleBudgetaryValidityId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.INDSleBudgetaryValidityId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleBudgetaryValidityId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleBudgetaryValidityId.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleBudgetaryValidityId.Properties.Appearance.Options.UseFont = True
        Me.INDSleBudgetaryValidityId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleBudgetaryValidityId.Properties.DisplayMember = "Year"
        Me.INDSleBudgetaryValidityId.Properties.NullText = ""
        Me.INDSleBudgetaryValidityId.Properties.PopupSizeable = False
        Me.INDSleBudgetaryValidityId.Properties.PopupView = Me.SearchLookUpEdit2View
        Me.INDSleBudgetaryValidityId.Properties.ShowFooter = False
        Me.INDSleBudgetaryValidityId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleBudgetaryValidityId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleBudgetaryValidityId, True)
        Me.INDSleBudgetaryValidityId.Size = New System.Drawing.Size(411, 28)
        Me.INDSleBudgetaryValidityId.StyleController = Me.INDlyContract
        Me.INDSleBudgetaryValidityId.TabIndex = 38
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleBudgetaryValidityId, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleBudgetaryValidityId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleBudgetaryValidityId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleBudgetaryValidityId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleBudgetaryValidityId, False)
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
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn187, Me.GridColumn188, Me.GridColumn189, Me.GridColumn190})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn187
        '
        Me.GridColumn187.Caption = "Año"
        Me.GridColumn187.FieldName = "Year"
        Me.GridColumn187.Name = "GridColumn187"
        Me.GridColumn187.OptionsColumn.AllowEdit = False
        Me.GridColumn187.OptionsColumn.AllowFocus = False
        Me.GridColumn187.Visible = True
        Me.GridColumn187.VisibleIndex = 0
        '
        'GridColumn188
        '
        Me.GridColumn188.Caption = "Resolución"
        Me.GridColumn188.FieldName = "ResolutionNumber"
        Me.GridColumn188.Name = "GridColumn188"
        Me.GridColumn188.OptionsColumn.AllowEdit = False
        Me.GridColumn188.OptionsColumn.AllowFocus = False
        Me.GridColumn188.Visible = True
        Me.GridColumn188.VisibleIndex = 1
        '
        'GridColumn189
        '
        Me.GridColumn189.Caption = "Valor"
        Me.GridColumn189.DisplayFormat.FormatString = "c0"
        Me.GridColumn189.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn189.FieldName = "ResolutionValue"
        Me.GridColumn189.Name = "GridColumn189"
        Me.GridColumn189.OptionsColumn.AllowEdit = False
        Me.GridColumn189.OptionsColumn.AllowFocus = False
        Me.GridColumn189.Visible = True
        Me.GridColumn189.VisibleIndex = 2
        '
        'GridColumn190
        '
        Me.GridColumn190.Caption = "Estado"
        Me.GridColumn190.FieldName = "Status"
        Me.GridColumn190.Name = "GridColumn190"
        Me.GridColumn190.OptionsColumn.AllowEdit = False
        Me.GridColumn190.OptionsColumn.AllowFocus = False
        Me.GridColumn190.Visible = True
        Me.GridColumn190.VisibleIndex = 3
        '
        'INDSleBudgetaryEntityId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleBudgetaryEntityId, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleBudgetaryEntityId, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.INDSleBudgetaryEntityId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.INDSleBudgetaryEntityId.Location = New System.Drawing.Point(687, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleBudgetaryEntityId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleBudgetaryEntityId.Name = "INDSleBudgetaryEntityId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.INDSleBudgetaryEntityId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleBudgetaryEntityId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleBudgetaryEntityId.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleBudgetaryEntityId.Properties.Appearance.Options.UseFont = True
        Me.INDSleBudgetaryEntityId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleBudgetaryEntityId.Properties.DisplayMember = "NameCode"
        Me.INDSleBudgetaryEntityId.Properties.NullText = ""
        Me.INDSleBudgetaryEntityId.Properties.PopupSizeable = False
        Me.INDSleBudgetaryEntityId.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleBudgetaryEntityId.Properties.ShowFooter = False
        Me.INDSleBudgetaryEntityId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleBudgetaryEntityId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleBudgetaryEntityId, True)
        Me.INDSleBudgetaryEntityId.Size = New System.Drawing.Size(411, 28)
        Me.INDSleBudgetaryEntityId.StyleController = Me.INDlyContract
        Me.INDSleBudgetaryEntityId.TabIndex = 37
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleBudgetaryEntityId, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleBudgetaryEntityId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleBudgetaryEntityId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleBudgetaryEntityId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleBudgetaryEntityId, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn185, Me.GridColumn186})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn185
        '
        Me.GridColumn185.Caption = "Código"
        Me.GridColumn185.FieldName = "Code"
        Me.GridColumn185.Name = "GridColumn185"
        Me.GridColumn185.OptionsColumn.AllowEdit = False
        Me.GridColumn185.OptionsColumn.AllowFocus = False
        Me.GridColumn185.Visible = True
        Me.GridColumn185.VisibleIndex = 0
        Me.GridColumn185.Width = 155
        '
        'GridColumn186
        '
        Me.GridColumn186.Caption = "Nombre"
        Me.GridColumn186.FieldName = "Name"
        Me.GridColumn186.Name = "GridColumn186"
        Me.GridColumn186.OptionsColumn.AllowEdit = False
        Me.GridColumn186.OptionsColumn.AllowFocus = False
        Me.GridColumn186.Visible = True
        Me.GridColumn186.VisibleIndex = 1
        Me.GridColumn186.Width = 1227
        '
        'INDdeDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDocumentDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDocumentDate, True)
        Me.INDdeDocumentDate.EditValue = Nothing
        Me.INDdeDocumentDate.EnterMoveNextControl = True
        Me.INDdeDocumentDate.Location = New System.Drawing.Point(-326, 143)
        Me.INDdeDocumentDate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDocumentDate.Name = "INDdeDocumentDate"
        Me.INDdeDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDocumentDate.StyleController = Me.INDlyContract
        Me.INDdeDocumentDate.TabIndex = 35
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDocumentDate, 0)
        Me.INDdeDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnAddAvailability
        '
        Me.INDbtnAddAvailability.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddAvailability.Appearance.Options.UseFont = True
        Me.INDbtnAddAvailability.Location = New System.Drawing.Point(1102, 131)
        Me.INDbtnAddAvailability.MaximumSize = New System.Drawing.Size(224, 0)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddAvailability, True)
        Me.INDbtnAddAvailability.Name = "INDbtnAddAvailability"
        Me.INDbtnAddAvailability.Size = New System.Drawing.Size(224, 28)
        Me.INDbtnAddAvailability.StyleController = Me.INDlyContract
        Me.INDbtnAddAvailability.TabIndex = 34
        Me.INDbtnAddAvailability.Text = "Agregar"
        '
        'INDsleAvailability
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleAvailability, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleAvailability, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleAvailability, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleAvailability, False)
        Me.INDsleAvailability.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleAvailability, False)
        Me.INDsleAvailability.Location = New System.Drawing.Point(687, 131)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleAvailability, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleAvailability.Name = "INDsleAvailability"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleAvailability, False)
        Me.INDsleAvailability.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleAvailability.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAvailability.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAvailability.Properties.Appearance.Options.UseFont = True
        Me.INDsleAvailability.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAvailability.Properties.DisplayMember = "Description"
        Me.INDsleAvailability.Properties.NullText = ""
        Me.INDsleAvailability.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDsleAvailability.Properties.PopupFormSize = New System.Drawing.Size(600, 0)
        Me.INDsleAvailability.Properties.PopupSizeable = False
        Me.INDsleAvailability.Properties.PopupView = Me.INDviewSearchAvailability
        Me.INDsleAvailability.Properties.ShowFooter = False
        Me.INDsleAvailability.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleAvailability, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleAvailability, True)
        Me.INDsleAvailability.Size = New System.Drawing.Size(411, 28)
        Me.INDsleAvailability.StyleController = Me.INDlyContract
        Me.INDsleAvailability.TabIndex = 33
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleAvailability, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleAvailability, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleAvailability, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleAvailability, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleAvailability, False)
        '
        'INDviewSearchAvailability
        '
        Me.INDviewSearchAvailability.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewSearchAvailability.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewSearchAvailability.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewSearchAvailability.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewSearchAvailability.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchAvailability.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewSearchAvailability.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchAvailability.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewSearchAvailability.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewSearchAvailability.Appearance.Row.Options.UseFont = True
        Me.INDviewSearchAvailability.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn147, Me.GridColumn149, Me.GridColumn150, Me.GridColumn151, Me.GridColumn152})
        Me.INDviewSearchAvailability.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewSearchAvailability.Name = "INDviewSearchAvailability"
        Me.INDviewSearchAvailability.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewSearchAvailability.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewSearchAvailability.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewSearchAvailability.OptionsView.ShowAutoFilterRow = True
        Me.INDviewSearchAvailability.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewSearchAvailability, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewSearchAvailability, False)
        '
        'GridColumn147
        '
        Me.GridColumn147.Caption = "Código"
        Me.GridColumn147.FieldName = "AvailabilityCode"
        Me.GridColumn147.Name = "GridColumn147"
        Me.GridColumn147.Visible = True
        Me.GridColumn147.VisibleIndex = 0
        '
        'GridColumn149
        '
        Me.GridColumn149.Caption = "Rubro"
        Me.GridColumn149.FieldName = "CategoryCodeName"
        Me.GridColumn149.Name = "GridColumn149"
        Me.GridColumn149.Visible = True
        Me.GridColumn149.VisibleIndex = 1
        '
        'GridColumn150
        '
        Me.GridColumn150.Caption = "Recurso"
        Me.GridColumn150.FieldName = "FinancialSourceCodeName"
        Me.GridColumn150.Name = "GridColumn150"
        Me.GridColumn150.Visible = True
        Me.GridColumn150.VisibleIndex = 2
        '
        'GridColumn151
        '
        Me.GridColumn151.Caption = "Tipo"
        Me.GridColumn151.FieldName = "RevenueTypeCodeName"
        Me.GridColumn151.Name = "GridColumn151"
        Me.GridColumn151.Visible = True
        Me.GridColumn151.VisibleIndex = 3
        '
        'GridColumn152
        '
        Me.GridColumn152.Caption = "Saldo"
        Me.GridColumn152.DisplayFormat.FormatString = "C0"
        Me.GridColumn152.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn152.FieldName = "Balance"
        Me.GridColumn152.Name = "GridColumn152"
        Me.GridColumn152.Visible = True
        Me.GridColumn152.VisibleIndex = 4
        '
        'INDgcAvailability
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAvailability, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAvailability, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAvailability, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAvailability, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAvailability, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAvailability, False)
        Me.INDgcAvailability.Location = New System.Drawing.Point(502, 167)
        Me.INDgcAvailability.MainView = Me.INDviewAvailability
        Me.INDgcAvailability.MaximumSize = New System.Drawing.Size(824, 0)
        Me.INDgcAvailability.Name = "INDgcAvailability"
        Me.INDgcAvailability.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepTxtValue})
        Me.INDgcAvailability.Size = New System.Drawing.Size(824, 450)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAvailability, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcAvailability.TabIndex = 32
        Me.INDgcAvailability.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewAvailability})
        '
        'INDviewAvailability
        '
        Me.INDviewAvailability.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewAvailability.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewAvailability.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewAvailability.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewAvailability.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAvailability.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewAvailability.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAvailability.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewAvailability.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewAvailability.Appearance.Row.Options.UseFont = True
        Me.INDviewAvailability.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAvailability.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewAvailability.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn154, Me.GridColumn155, Me.GridColumn156, Me.GridColumn158, Me.GridColumn159, Me.GridColumn160})
        Me.INDviewAvailability.GridControl = Me.INDgcAvailability
        Me.INDviewAvailability.Name = "INDviewAvailability"
        Me.INDviewAvailability.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAvailability.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAvailability.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAvailability.OptionsView.ShowDetailButtons = False
        Me.INDviewAvailability.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAvailability, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDviewAvailability, False)
        '
        'GridColumn154
        '
        Me.GridColumn154.Caption = "Código"
        Me.GridColumn154.FieldName = "AvailabilityCode"
        Me.GridColumn154.Name = "GridColumn154"
        Me.GridColumn154.OptionsColumn.AllowEdit = False
        Me.GridColumn154.OptionsColumn.AllowFocus = False
        Me.GridColumn154.Visible = True
        Me.GridColumn154.VisibleIndex = 0
        '
        'GridColumn155
        '
        Me.GridColumn155.Caption = "Rubro"
        Me.GridColumn155.FieldName = "CategoryCodeName"
        Me.GridColumn155.Name = "GridColumn155"
        Me.GridColumn155.OptionsColumn.AllowEdit = False
        Me.GridColumn155.OptionsColumn.AllowFocus = False
        Me.GridColumn155.Visible = True
        Me.GridColumn155.VisibleIndex = 1
        '
        'GridColumn156
        '
        Me.GridColumn156.Caption = "Recurso"
        Me.GridColumn156.FieldName = "FinancialSourceCodeName"
        Me.GridColumn156.Name = "GridColumn156"
        Me.GridColumn156.OptionsColumn.AllowEdit = False
        Me.GridColumn156.OptionsColumn.AllowFocus = False
        Me.GridColumn156.Visible = True
        Me.GridColumn156.VisibleIndex = 2
        '
        'GridColumn158
        '
        Me.GridColumn158.Caption = "Tipo"
        Me.GridColumn158.FieldName = "RevenueTypeCodeName"
        Me.GridColumn158.Name = "GridColumn158"
        Me.GridColumn158.OptionsColumn.AllowEdit = False
        Me.GridColumn158.OptionsColumn.AllowFocus = False
        Me.GridColumn158.Visible = True
        Me.GridColumn158.VisibleIndex = 3
        '
        'GridColumn159
        '
        Me.GridColumn159.Caption = "Saldo"
        Me.GridColumn159.DisplayFormat.FormatString = "C0"
        Me.GridColumn159.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn159.FieldName = "Balance"
        Me.GridColumn159.Name = "GridColumn159"
        Me.GridColumn159.OptionsColumn.AllowEdit = False
        Me.GridColumn159.OptionsColumn.AllowFocus = False
        Me.GridColumn159.Visible = True
        Me.GridColumn159.VisibleIndex = 4
        '
        'GridColumn160
        '
        Me.GridColumn160.Caption = "Valor a Ejecutar"
        Me.GridColumn160.ColumnEdit = Me.INDrepTxtValue
        Me.GridColumn160.FieldName = "Value"
        Me.GridColumn160.Name = "GridColumn160"
        Me.GridColumn160.Visible = True
        Me.GridColumn160.VisibleIndex = 5
        '
        'INDrepTxtValue
        '
        Me.INDrepTxtValue.AutoHeight = False
        Me.INDrepTxtValue.Mask.EditMask = "C0"
        Me.INDrepTxtValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrepTxtValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepTxtValue.Name = "INDrepTxtValue"
        '
        'INDSleContract
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleContract, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleContract, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleContract, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleContract, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleContract, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleContract, False)
        Me.INDSleContract.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleContract, False)
        Me.INDSleContract.Location = New System.Drawing.Point(-326, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleContract, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleContract.Name = "INDSleContract"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleContract, False)
        Me.INDSleContract.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleContract.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleContract.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleContract.Properties.Appearance.Options.UseFont = True
        Me.INDSleContract.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleContract.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleContract.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleContract.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleContract.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleContract.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleContract.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleContract.Properties.DisplayMember = "ContractNumber"
        Me.INDSleContract.Properties.NullText = ""
        Me.INDSleContract.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDSleContract.Properties.PopupSizeable = False
        Me.INDSleContract.Properties.PopupView = Me.viewSupplier
        Me.INDSleContract.Properties.ShowFooter = False
        Me.INDSleContract.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleContract, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleContract, True)
        Me.INDSleContract.Size = New System.Drawing.Size(386, 28)
        Me.INDSleContract.StyleController = Me.INDlyContract
        Me.INDSleContract.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleContract, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleContract, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleContract, "{0} - {1}")
        Me.INDSleContract.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleContract, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleContract, False)
        '
        'viewSupplier
        '
        Me.viewSupplier.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSupplier.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewSupplier.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSupplier.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSupplier.Appearance.FocusedRow.Options.UseForeColor = True
        Me.viewSupplier.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSupplier.Appearance.GroupRow.Options.UseFont = True
        Me.viewSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewSupplier.Appearance.Row.Options.UseFont = True
        Me.viewSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn74, Me.GridColumn75, Me.GridColumn76})
        Me.viewSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewSupplier.Name = "viewSupplier"
        Me.viewSupplier.OptionsBehavior.AutoExpandAllGroups = True
        Me.viewSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.viewSupplier.OptionsView.ShowAutoFilterRow = True
        Me.viewSupplier.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSupplier, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.viewSupplier, False)
        '
        'GridColumn74
        '
        Me.GridColumn74.Caption = "Código"
        Me.GridColumn74.FieldName = "Code"
        Me.GridColumn74.Name = "GridColumn74"
        Me.GridColumn74.Visible = True
        Me.GridColumn74.VisibleIndex = 0
        Me.GridColumn74.Width = 352
        '
        'GridColumn75
        '
        Me.GridColumn75.Caption = "Tipo de Contrato"
        Me.GridColumn75.FieldName = "ContractTypeId.CodeName"
        Me.GridColumn75.Name = "GridColumn75"
        Me.GridColumn75.Visible = True
        Me.GridColumn75.VisibleIndex = 1
        Me.GridColumn75.Width = 657
        '
        'GridColumn76
        '
        Me.GridColumn76.Caption = "Número de Contrato"
        Me.GridColumn76.FieldName = "ContractNumber"
        Me.GridColumn76.Name = "GridColumn76"
        Me.GridColumn76.Visible = True
        Me.GridColumn76.VisibleIndex = 2
        Me.GridColumn76.Width = 623
        '
        'INDdeEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeEndDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeEndDate, True)
        Me.INDdeEndDate.EditValue = Nothing
        Me.INDdeEndDate.EnterMoveNextControl = True
        Me.INDdeEndDate.Location = New System.Drawing.Point(88, 143)
        Me.INDdeEndDate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeEndDate.Name = "INDdeEndDate"
        Me.INDdeEndDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeEndDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeEndDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeEndDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeEndDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeEndDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeEndDate.StyleController = Me.INDlyContract
        Me.INDdeEndDate.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeEndDate, 0)
        Me.INDdeEndDate.ToolTip = "Este Campo es Necesario"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(-326, 79)
        Me.INDBtnCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
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
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Properties.MaxLength = 20
        Me.INDBtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBtnCode.StyleController = Me.INDlyContract
        Me.INDBtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDGleModificationType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDGleModificationType, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDGleModificationType, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDGleModificationType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleModificationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleModificationType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDGleModificationType, False)
        Me.INDGleModificationType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDGleModificationType, False)
        Me.INDGleModificationType.Location = New System.Drawing.Point(88, 79)
        Me.INDGleModificationType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleModificationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleModificationType.Name = "INDGleModificationType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDGleModificationType, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDGleModificationType, False)
        Me.INDGleModificationType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleModificationType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleModificationType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleModificationType.Properties.Appearance.Options.UseFont = True
        Me.INDGleModificationType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleModificationType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleModificationType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleModificationType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleModificationType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleModificationType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleModificationType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleModificationType.Properties.DisplayMember = "Item2"
        Me.INDGleModificationType.Properties.NullText = ""
        Me.INDGleModificationType.Properties.PopupSizeable = False
        Me.INDGleModificationType.Properties.PopupView = Me.INDGlvContractType
        Me.INDGleModificationType.Properties.ShowFooter = False
        Me.INDGleModificationType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDGleModificationType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDGleModificationType, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDGleModificationType, True)
        Me.INDGleModificationType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleModificationType.StyleController = Me.INDlyContract
        Me.INDGleModificationType.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDGleModificationType, "1400")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleModificationType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDGleModificationType, "{0} - {1}")
        Me.INDGleModificationType.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDGleModificationType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDGleModificationType, False)
        '
        'INDGlvContractType
        '
        Me.INDGlvContractType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGlvContractType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGlvContractType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGlvContractType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGlvContractType.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGlvContractType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGlvContractType.Appearance.GroupRow.Options.UseFont = True
        Me.INDGlvContractType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGlvContractType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGlvContractType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGlvContractType.Appearance.Row.Options.UseFont = True
        Me.INDGlvContractType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCodeContractType})
        Me.INDGlvContractType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGlvContractType.Name = "INDGlvContractType"
        Me.INDGlvContractType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGlvContractType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGlvContractType.OptionsView.EnableAppearanceOddRow = True
        Me.INDGlvContractType.OptionsView.ShowAutoFilterRow = True
        Me.INDGlvContractType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGlvContractType, False)
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGlvContractType, False)
        '
        'ColCodeContractType
        '
        Me.ColCodeContractType.Caption = "Descripción"
        Me.ColCodeContractType.FieldName = "Item2"
        Me.ColCodeContractType.Name = "ColCodeContractType"
        Me.ColCodeContractType.Visible = True
        Me.ColCodeContractType.VisibleIndex = 0
        Me.ColCodeContractType.Width = 250
        '
        'INDMmoDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmoDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmoDescription, True)
        Me.INDMmoDescription.EnterMoveNextControl = True
        Me.INDMmoDescription.Location = New System.Drawing.Point(-326, 271)
        Me.INDMmoDescription.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmoDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMmoDescription.Name = "INDMmoDescription"
        Me.INDMmoDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMmoDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmoDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMmoDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMmoDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMmoDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMmoDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMmoDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMmoDescription.Properties.MaxLength = 300
        Me.INDMmoDescription.Size = New System.Drawing.Size(386, 104)
        Me.INDMmoDescription.StyleController = Me.INDlyContract
        Me.INDMmoDescription.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmoDescription, 0)
        Me.INDMmoDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtValue, True)
        Me.INDtxtValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtValue.EnterMoveNextControl = True
        Me.INDtxtValue.Location = New System.Drawing.Point(88, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtValue, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtValue.Name = "INDtxtValue"
        Me.INDtxtValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtValue.Properties.Appearance.Options.UseFont = True
        Me.INDtxtValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtValue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDtxtValue.Properties.Mask.EditMask = "c"
        Me.INDtxtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtValue.Properties.MaxLength = 20
        Me.INDtxtValue.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtValue.StyleController = Me.INDlyContract
        Me.INDtxtValue.TabIndex = 36
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtValue, 0)
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
        Me.LayoutControlGroup1.CustomizationFormText = "Contrato"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGpMainData, Me.INDlyGpDetaill, Me.INDlygBudget})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1700, 641)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGpMainData
        '
        Me.INDlyGpMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlyGpMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGpMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGpMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGpMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGpMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGpMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGpMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGpMainData, False)
        Me.INDlyGpMainData.CustomizationFormText = "Datos Principales"
        Me.INDlyGpMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciDescription, Me.INDLciDocumentDate, Me.INDLciContract})
        Me.INDlyGpMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGpMainData.Name = "INDlyGpMainData"
        Me.INDlyGpMainData.Size = New System.Drawing.Size(414, 621)
        Me.INDlyGpMainData.Text = "Datos Principales"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBtnCode
        Me.INDLciCode.CustomizationFormText = "Código"
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(390, 64)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(136, 17)
        '
        'INDLciDescription
        '
        Me.INDLciDescription.Control = Me.INDMmoDescription
        Me.INDLciDescription.CustomizationFormText = "Descripción"
        Me.INDLciDescription.Location = New System.Drawing.Point(0, 192)
        Me.INDLciDescription.MaxSize = New System.Drawing.Size(390, 128)
        Me.INDLciDescription.MinSize = New System.Drawing.Size(390, 128)
        Me.INDLciDescription.Name = "INDLciDescription"
        Me.INDLciDescription.ShowInCustomizationForm = False
        Me.INDLciDescription.Size = New System.Drawing.Size(390, 370)
        Me.INDLciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescription.Text = "Descripción"
        Me.INDLciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescription.TextSize = New System.Drawing.Size(136, 17)
        '
        'INDLciDocumentDate
        '
        Me.INDLciDocumentDate.Control = Me.INDdeDocumentDate
        Me.INDLciDocumentDate.Location = New System.Drawing.Point(0, 64)
        Me.INDLciDocumentDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.Name = "INDLciDocumentDate"
        Me.INDLciDocumentDate.ShowInCustomizationForm = False
        Me.INDLciDocumentDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocumentDate.Text = "Fecha Documento"
        Me.INDLciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocumentDate.TextSize = New System.Drawing.Size(136, 17)
        '
        'INDLciContract
        '
        Me.INDLciContract.Control = Me.INDSleContract
        Me.INDLciContract.CustomizationFormText = "Contrato"
        Me.INDLciContract.Location = New System.Drawing.Point(0, 128)
        Me.INDLciContract.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciContract.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciContract.Name = "INDLciContract"
        Me.INDLciContract.ShowInCustomizationForm = False
        Me.INDLciContract.Size = New System.Drawing.Size(390, 64)
        Me.INDLciContract.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciContract.Text = "Contrato"
        Me.INDLciContract.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciContract.TextSize = New System.Drawing.Size(136, 17)
        '
        'INDlyGpDetaill
        '
        Me.INDlyGpDetaill.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpDetaill.AppearanceGroup.Options.UseFont = True
        Me.INDlyGpDetaill.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpDetaill.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGpDetaill.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpDetaill.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGpDetaill.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGpDetaill.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGpDetaill.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpDetaill.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGpDetaill.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpDetaill.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGpDetaill.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpDetaill.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGpDetaill, False)
        Me.INDlyGpDetaill.CustomizationFormText = "Detalles del Otro si"
        Me.INDlyGpDetaill.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciValue, Me.INDLciModificationType, Me.INDLciEndDate})
        Me.INDlyGpDetaill.Location = New System.Drawing.Point(414, 0)
        Me.INDlyGpDetaill.Name = "INDlyGpDetaill"
        Me.INDlyGpDetaill.Size = New System.Drawing.Size(414, 621)
        Me.INDlyGpDetaill.Text = "Detalles del Otro si"
        '
        'INDLciValue
        '
        Me.INDLciValue.Control = Me.INDtxtValue
        Me.INDLciValue.Location = New System.Drawing.Point(0, 128)
        Me.INDLciValue.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciValue.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciValue.Name = "INDLciValue"
        Me.INDLciValue.Size = New System.Drawing.Size(390, 434)
        Me.INDLciValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValue.Text = "Valor"
        Me.INDLciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValue.TextSize = New System.Drawing.Size(136, 17)
        Me.INDLciValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciModificationType
        '
        Me.INDLciModificationType.Control = Me.INDGleModificationType
        Me.INDLciModificationType.CustomizationFormText = "Tipo de Modificación"
        Me.INDLciModificationType.Location = New System.Drawing.Point(0, 0)
        Me.INDLciModificationType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciModificationType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciModificationType.Name = "INDLciModificationType"
        Me.INDLciModificationType.ShowInCustomizationForm = False
        Me.INDLciModificationType.Size = New System.Drawing.Size(390, 64)
        Me.INDLciModificationType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciModificationType.Text = "Tipo de Modificación"
        Me.INDLciModificationType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciModificationType.TextSize = New System.Drawing.Size(136, 17)
        '
        'INDLciEndDate
        '
        Me.INDLciEndDate.Control = Me.INDdeEndDate
        Me.INDLciEndDate.CustomizationFormText = "Fecha Final"
        Me.INDLciEndDate.Location = New System.Drawing.Point(0, 64)
        Me.INDLciEndDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciEndDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciEndDate.Name = "INDLciEndDate"
        Me.INDLciEndDate.ShowInCustomizationForm = False
        Me.INDLciEndDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLciEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEndDate.Text = "Fecha Final"
        Me.INDLciEndDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEndDate.TextSize = New System.Drawing.Size(136, 17)
        Me.INDLciEndDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlygBudget
        '
        Me.INDlygBudget.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygBudget.AppearanceGroup.Options.UseFont = True
        Me.INDlygBudget.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygBudget.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygBudget.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygBudget.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygBudget, False)
        Me.INDlygBudget.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridAvailability, Me.INDlyItemAvailability, Me.INDlyItemAddAvailability, Me.INDLciBudgetaryEntityId, Me.INDLciBudgetaryValidityId})
        Me.INDlygBudget.Location = New System.Drawing.Point(828, 0)
        Me.INDlygBudget.Name = "INDlygBudget"
        Me.INDlygBudget.Size = New System.Drawing.Size(852, 621)
        Me.INDlygBudget.Text = "Listado Presupuestal"
        Me.INDlygBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemGridAvailability
        '
        Me.INDlyItemGridAvailability.Control = Me.INDgcAvailability
        Me.INDlyItemGridAvailability.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemGridAvailability.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDlyItemGridAvailability.MinSize = New System.Drawing.Size(828, 24)
        Me.INDlyItemGridAvailability.Name = "INDlyItemGridAvailability"
        Me.INDlyItemGridAvailability.Size = New System.Drawing.Size(828, 454)
        Me.INDlyItemGridAvailability.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridAvailability.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridAvailability.TextVisible = False
        '
        'INDlyItemAvailability
        '
        Me.INDlyItemAvailability.Control = Me.INDsleAvailability
        Me.INDlyItemAvailability.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemAvailability.MaxSize = New System.Drawing.Size(600, 36)
        Me.INDlyItemAvailability.MinSize = New System.Drawing.Size(600, 36)
        Me.INDlyItemAvailability.Name = "INDlyItemAvailability"
        Me.INDlyItemAvailability.Size = New System.Drawing.Size(600, 36)
        Me.INDlyItemAvailability.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAvailability.Text = "Rubro de Disponibilidades"
        Me.INDlyItemAvailability.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAvailability.TextSize = New System.Drawing.Size(180, 21)
        Me.INDlyItemAvailability.TextToControlDistance = 5
        '
        'INDlyItemAddAvailability
        '
        Me.INDlyItemAddAvailability.Control = Me.INDbtnAddAvailability
        Me.INDlyItemAddAvailability.Location = New System.Drawing.Point(600, 72)
        Me.INDlyItemAddAvailability.MaxSize = New System.Drawing.Size(228, 32)
        Me.INDlyItemAddAvailability.MinSize = New System.Drawing.Size(228, 32)
        Me.INDlyItemAddAvailability.Name = "INDlyItemAddAvailability"
        Me.INDlyItemAddAvailability.Size = New System.Drawing.Size(228, 36)
        Me.INDlyItemAddAvailability.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddAvailability.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddAvailability.TextVisible = False
        '
        'INDLciBudgetaryEntityId
        '
        Me.INDLciBudgetaryEntityId.Control = Me.INDSleBudgetaryEntityId
        Me.INDLciBudgetaryEntityId.Location = New System.Drawing.Point(0, 0)
        Me.INDLciBudgetaryEntityId.MaxSize = New System.Drawing.Size(600, 36)
        Me.INDLciBudgetaryEntityId.MinSize = New System.Drawing.Size(600, 36)
        Me.INDLciBudgetaryEntityId.Name = "INDLciBudgetaryEntityId"
        Me.INDLciBudgetaryEntityId.Size = New System.Drawing.Size(828, 36)
        Me.INDLciBudgetaryEntityId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBudgetaryEntityId.Text = "Entidad Presupuestal"
        Me.INDLciBudgetaryEntityId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBudgetaryEntityId.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDLciBudgetaryEntityId.TextSize = New System.Drawing.Size(180, 21)
        Me.INDLciBudgetaryEntityId.TextToControlDistance = 5
        '
        'INDLciBudgetaryValidityId
        '
        Me.INDLciBudgetaryValidityId.Control = Me.INDSleBudgetaryValidityId
        Me.INDLciBudgetaryValidityId.Location = New System.Drawing.Point(0, 36)
        Me.INDLciBudgetaryValidityId.MaxSize = New System.Drawing.Size(600, 36)
        Me.INDLciBudgetaryValidityId.MinSize = New System.Drawing.Size(600, 36)
        Me.INDLciBudgetaryValidityId.Name = "INDLciBudgetaryValidityId"
        Me.INDLciBudgetaryValidityId.Size = New System.Drawing.Size(828, 36)
        Me.INDLciBudgetaryValidityId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBudgetaryValidityId.Text = "Vigencia"
        Me.INDLciBudgetaryValidityId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBudgetaryValidityId.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDLciBudgetaryValidityId.TextSize = New System.Drawing.Size(180, 21)
        Me.INDLciBudgetaryValidityId.TextToControlDistance = 5
        '
        'GridColumn194
        '
        Me.GridColumn194.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn194.Caption = "Selección"
        Me.GridColumn194.FieldName = "Item2"
        Me.GridColumn194.Name = "GridColumn194"
        '
        'GridColumn195
        '
        Me.GridColumn195.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn195.Caption = "Selección"
        Me.GridColumn195.FieldName = "Item2"
        Me.GridColumn195.Name = "GridColumn195"
        '
        'GridColumn196
        '
        Me.GridColumn196.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn196.Caption = "Selección"
        Me.GridColumn196.FieldName = "Item2"
        Me.GridColumn196.Name = "GridColumn196"
        '
        'GridColumn191
        '
        Me.GridColumn191.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn191.Caption = "Selección"
        Me.GridColumn191.FieldName = "Item2"
        Me.GridColumn191.Name = "GridColumn191"
        '
        'GridColumn192
        '
        Me.GridColumn192.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn192.Caption = "Selección"
        Me.GridColumn192.FieldName = "Item2"
        Me.GridColumn192.Name = "GridColumn192"
        '
        'GridColumn193
        '
        Me.GridColumn193.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn193.Caption = "Selección"
        Me.GridColumn193.FieldName = "Item2"
        Me.GridColumn193.Name = "GridColumn193"
        '
        'GridColumn182
        '
        Me.GridColumn182.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn182.Caption = "Selección"
        Me.GridColumn182.FieldName = "Item2"
        Me.GridColumn182.Name = "GridColumn182"
        '
        'GridColumn183
        '
        Me.GridColumn183.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn183.Caption = "Selección"
        Me.GridColumn183.FieldName = "Item2"
        Me.GridColumn183.Name = "GridColumn183"
        '
        'GridColumn184
        '
        Me.GridColumn184.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn184.Caption = "Selección"
        Me.GridColumn184.FieldName = "Item2"
        Me.GridColumn184.Name = "GridColumn184"
        '
        'GridColumn179
        '
        Me.GridColumn179.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn179.Caption = "Selección"
        Me.GridColumn179.FieldName = "Item2"
        Me.GridColumn179.Name = "GridColumn179"
        '
        'GridColumn180
        '
        Me.GridColumn180.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn180.Caption = "Selección"
        Me.GridColumn180.FieldName = "Item2"
        Me.GridColumn180.Name = "GridColumn180"
        '
        'GridColumn181
        '
        Me.GridColumn181.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn181.Caption = "Selección"
        Me.GridColumn181.FieldName = "Item2"
        Me.GridColumn181.Name = "GridColumn181"
        '
        'GridColumn176
        '
        Me.GridColumn176.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn176.Caption = "Selección"
        Me.GridColumn176.FieldName = "Item2"
        Me.GridColumn176.Name = "GridColumn176"
        '
        'GridColumn177
        '
        Me.GridColumn177.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn177.Caption = "Selección"
        Me.GridColumn177.FieldName = "Item2"
        Me.GridColumn177.Name = "GridColumn177"
        '
        'GridColumn178
        '
        Me.GridColumn178.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn178.Caption = "Selección"
        Me.GridColumn178.FieldName = "Item2"
        Me.GridColumn178.Name = "GridColumn178"
        '
        'GridColumn173
        '
        Me.GridColumn173.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn173.Caption = "Selección"
        Me.GridColumn173.FieldName = "Item2"
        Me.GridColumn173.Name = "GridColumn173"
        '
        'GridColumn174
        '
        Me.GridColumn174.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn174.Caption = "Selección"
        Me.GridColumn174.FieldName = "Item2"
        Me.GridColumn174.Name = "GridColumn174"
        '
        'GridColumn175
        '
        Me.GridColumn175.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn175.Caption = "Selección"
        Me.GridColumn175.FieldName = "Item2"
        Me.GridColumn175.Name = "GridColumn175"
        '
        'GridColumn170
        '
        Me.GridColumn170.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn170.Caption = "Selección"
        Me.GridColumn170.FieldName = "Item2"
        Me.GridColumn170.Name = "GridColumn170"
        '
        'GridColumn171
        '
        Me.GridColumn171.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn171.Caption = "Selección"
        Me.GridColumn171.FieldName = "Item2"
        Me.GridColumn171.Name = "GridColumn171"
        '
        'GridColumn172
        '
        Me.GridColumn172.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn172.Caption = "Selección"
        Me.GridColumn172.FieldName = "Item2"
        Me.GridColumn172.Name = "GridColumn172"
        '
        'GridColumn168
        '
        Me.GridColumn168.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn168.Caption = "Selección"
        Me.GridColumn168.FieldName = "Item2"
        Me.GridColumn168.Name = "GridColumn168"
        '
        'GridColumn169
        '
        Me.GridColumn169.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn169.Caption = "Selección"
        Me.GridColumn169.FieldName = "Item2"
        Me.GridColumn169.Name = "GridColumn169"
        '
        'GridColumn166
        '
        Me.GridColumn166.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn166.Caption = "Selección"
        Me.GridColumn166.FieldName = "Item2"
        Me.GridColumn166.Name = "GridColumn166"
        '
        'GridColumn167
        '
        Me.GridColumn167.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn167.Caption = "Selección"
        Me.GridColumn167.FieldName = "Item2"
        Me.GridColumn167.Name = "GridColumn167"
        '
        'GridColumn164
        '
        Me.GridColumn164.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn164.Caption = "Selección"
        Me.GridColumn164.FieldName = "Item2"
        Me.GridColumn164.Name = "GridColumn164"
        '
        'GridColumn165
        '
        Me.GridColumn165.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn165.Caption = "Selección"
        Me.GridColumn165.FieldName = "Item2"
        Me.GridColumn165.Name = "GridColumn165"
        '
        'GridColumn157
        '
        Me.GridColumn157.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn157.Caption = "Selección"
        Me.GridColumn157.FieldName = "Item2"
        Me.GridColumn157.Name = "GridColumn157"
        '
        'GridColumn163
        '
        Me.GridColumn163.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn163.Caption = "Selección"
        Me.GridColumn163.FieldName = "Item2"
        Me.GridColumn163.Name = "GridColumn163"
        '
        'GridColumn153
        '
        Me.GridColumn153.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn153.Caption = "Selección"
        Me.GridColumn153.FieldName = "Item2"
        Me.GridColumn153.Name = "GridColumn153"
        '
        'GridColumn162
        '
        Me.GridColumn162.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn162.Caption = "Selección"
        Me.GridColumn162.FieldName = "Item2"
        Me.GridColumn162.Name = "GridColumn162"
        '
        'GridColumn148
        '
        Me.GridColumn148.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn148.Caption = "Selección"
        Me.GridColumn148.FieldName = "Item2"
        Me.GridColumn148.Name = "GridColumn148"
        '
        'GridColumn161
        '
        Me.GridColumn161.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn161.Caption = "Selección"
        Me.GridColumn161.FieldName = "Item2"
        Me.GridColumn161.Name = "GridColumn161"
        '
        'GridColumn145
        '
        Me.GridColumn145.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn145.Caption = "Selección"
        Me.GridColumn145.FieldName = "Item2"
        Me.GridColumn145.Name = "GridColumn145"
        '
        'GridColumn146
        '
        Me.GridColumn146.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn146.Caption = "Selección"
        Me.GridColumn146.FieldName = "Item2"
        Me.GridColumn146.Name = "GridColumn146"
        '
        'GridColumn143
        '
        Me.GridColumn143.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn143.Caption = "Selección"
        Me.GridColumn143.FieldName = "Item2"
        Me.GridColumn143.Name = "GridColumn143"
        '
        'GridColumn144
        '
        Me.GridColumn144.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn144.Caption = "Selección"
        Me.GridColumn144.FieldName = "Item2"
        Me.GridColumn144.Name = "GridColumn144"
        '
        'GridColumn141
        '
        Me.GridColumn141.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn141.Caption = "Selección"
        Me.GridColumn141.FieldName = "Item2"
        Me.GridColumn141.Name = "GridColumn141"
        '
        'GridColumn142
        '
        Me.GridColumn142.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn142.Caption = "Selección"
        Me.GridColumn142.FieldName = "Item2"
        Me.GridColumn142.Name = "GridColumn142"
        '
        'GridColumn139
        '
        Me.GridColumn139.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn139.Caption = "Selección"
        Me.GridColumn139.FieldName = "Item2"
        Me.GridColumn139.Name = "GridColumn139"
        '
        'GridColumn140
        '
        Me.GridColumn140.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn140.Caption = "Selección"
        Me.GridColumn140.FieldName = "Item2"
        Me.GridColumn140.Name = "GridColumn140"
        '
        'GridColumn137
        '
        Me.GridColumn137.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn137.Caption = "Selección"
        Me.GridColumn137.FieldName = "Item2"
        Me.GridColumn137.Name = "GridColumn137"
        '
        'GridColumn138
        '
        Me.GridColumn138.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn138.Caption = "Selección"
        Me.GridColumn138.FieldName = "Item2"
        Me.GridColumn138.Name = "GridColumn138"
        '
        'GridColumn135
        '
        Me.GridColumn135.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn135.Caption = "Selección"
        Me.GridColumn135.FieldName = "Item2"
        Me.GridColumn135.Name = "GridColumn135"
        '
        'GridColumn136
        '
        Me.GridColumn136.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn136.Caption = "Selección"
        Me.GridColumn136.FieldName = "Item2"
        Me.GridColumn136.Name = "GridColumn136"
        '
        'GridColumn133
        '
        Me.GridColumn133.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn133.Caption = "Selección"
        Me.GridColumn133.FieldName = "Item2"
        Me.GridColumn133.Name = "GridColumn133"
        '
        'GridColumn134
        '
        Me.GridColumn134.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn134.Caption = "Selección"
        Me.GridColumn134.FieldName = "Item2"
        Me.GridColumn134.Name = "GridColumn134"
        '
        'GridColumn131
        '
        Me.GridColumn131.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn131.Caption = "Selección"
        Me.GridColumn131.FieldName = "Item2"
        Me.GridColumn131.Name = "GridColumn131"
        '
        'GridColumn132
        '
        Me.GridColumn132.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn132.Caption = "Selección"
        Me.GridColumn132.FieldName = "Item2"
        Me.GridColumn132.Name = "GridColumn132"
        '
        'GridColumn129
        '
        Me.GridColumn129.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn129.Caption = "Selección"
        Me.GridColumn129.FieldName = "Item2"
        Me.GridColumn129.Name = "GridColumn129"
        '
        'GridColumn130
        '
        Me.GridColumn130.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn130.Caption = "Selección"
        Me.GridColumn130.FieldName = "Item2"
        Me.GridColumn130.Name = "GridColumn130"
        '
        'GridColumn127
        '
        Me.GridColumn127.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn127.Caption = "Selección"
        Me.GridColumn127.FieldName = "Item2"
        Me.GridColumn127.Name = "GridColumn127"
        '
        'GridColumn128
        '
        Me.GridColumn128.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn128.Caption = "Selección"
        Me.GridColumn128.FieldName = "Item2"
        Me.GridColumn128.Name = "GridColumn128"
        '
        'GridColumn125
        '
        Me.GridColumn125.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn125.Caption = "Selección"
        Me.GridColumn125.FieldName = "Item2"
        Me.GridColumn125.Name = "GridColumn125"
        '
        'GridColumn126
        '
        Me.GridColumn126.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn126.Caption = "Selección"
        Me.GridColumn126.FieldName = "Item2"
        Me.GridColumn126.Name = "GridColumn126"
        '
        'GridColumn123
        '
        Me.GridColumn123.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn123.Caption = "Selección"
        Me.GridColumn123.FieldName = "Item2"
        Me.GridColumn123.Name = "GridColumn123"
        '
        'GridColumn124
        '
        Me.GridColumn124.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn124.Caption = "Selección"
        Me.GridColumn124.FieldName = "Item2"
        Me.GridColumn124.Name = "GridColumn124"
        '
        'GridColumn121
        '
        Me.GridColumn121.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn121.Caption = "Selección"
        Me.GridColumn121.FieldName = "Item2"
        Me.GridColumn121.Name = "GridColumn121"
        '
        'GridColumn122
        '
        Me.GridColumn122.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn122.Caption = "Selección"
        Me.GridColumn122.FieldName = "Item2"
        Me.GridColumn122.Name = "GridColumn122"
        '
        'GridColumn119
        '
        Me.GridColumn119.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn119.Caption = "Selección"
        Me.GridColumn119.FieldName = "Item2"
        Me.GridColumn119.Name = "GridColumn119"
        '
        'GridColumn120
        '
        Me.GridColumn120.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn120.Caption = "Selección"
        Me.GridColumn120.FieldName = "Item2"
        Me.GridColumn120.Name = "GridColumn120"
        '
        'GridColumn117
        '
        Me.GridColumn117.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn117.Caption = "Selección"
        Me.GridColumn117.FieldName = "Item2"
        Me.GridColumn117.Name = "GridColumn117"
        '
        'GridColumn118
        '
        Me.GridColumn118.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn118.Caption = "Selección"
        Me.GridColumn118.FieldName = "Item2"
        Me.GridColumn118.Name = "GridColumn118"
        '
        'GridColumn115
        '
        Me.GridColumn115.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn115.Caption = "Selección"
        Me.GridColumn115.FieldName = "Item2"
        Me.GridColumn115.Name = "GridColumn115"
        '
        'GridColumn116
        '
        Me.GridColumn116.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn116.Caption = "Selección"
        Me.GridColumn116.FieldName = "Item2"
        Me.GridColumn116.Name = "GridColumn116"
        '
        'GridColumn113
        '
        Me.GridColumn113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn113.Caption = "Selección"
        Me.GridColumn113.FieldName = "Item2"
        Me.GridColumn113.Name = "GridColumn113"
        '
        'GridColumn114
        '
        Me.GridColumn114.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn114.Caption = "Selección"
        Me.GridColumn114.FieldName = "Item2"
        Me.GridColumn114.Name = "GridColumn114"
        '
        'GridColumn111
        '
        Me.GridColumn111.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn111.Caption = "Selección"
        Me.GridColumn111.FieldName = "Item2"
        Me.GridColumn111.Name = "GridColumn111"
        '
        'GridColumn112
        '
        Me.GridColumn112.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn112.Caption = "Selección"
        Me.GridColumn112.FieldName = "Item2"
        Me.GridColumn112.Name = "GridColumn112"
        '
        'GridColumn109
        '
        Me.GridColumn109.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn109.Caption = "Selección"
        Me.GridColumn109.FieldName = "Item2"
        Me.GridColumn109.Name = "GridColumn109"
        '
        'GridColumn110
        '
        Me.GridColumn110.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn110.Caption = "Selección"
        Me.GridColumn110.FieldName = "Item2"
        Me.GridColumn110.Name = "GridColumn110"
        '
        'GridColumn107
        '
        Me.GridColumn107.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn107.Caption = "Selección"
        Me.GridColumn107.FieldName = "Item2"
        Me.GridColumn107.Name = "GridColumn107"
        '
        'GridColumn108
        '
        Me.GridColumn108.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn108.Caption = "Selección"
        Me.GridColumn108.FieldName = "Item2"
        Me.GridColumn108.Name = "GridColumn108"
        '
        'GridColumn105
        '
        Me.GridColumn105.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn105.Caption = "Selección"
        Me.GridColumn105.FieldName = "Item2"
        Me.GridColumn105.Name = "GridColumn105"
        '
        'GridColumn106
        '
        Me.GridColumn106.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn106.Caption = "Selección"
        Me.GridColumn106.FieldName = "Item2"
        Me.GridColumn106.Name = "GridColumn106"
        '
        'GridColumn103
        '
        Me.GridColumn103.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn103.Caption = "Selección"
        Me.GridColumn103.FieldName = "Item2"
        Me.GridColumn103.Name = "GridColumn103"
        '
        'GridColumn104
        '
        Me.GridColumn104.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn104.Caption = "Selección"
        Me.GridColumn104.FieldName = "Item2"
        Me.GridColumn104.Name = "GridColumn104"
        '
        'GridColumn101
        '
        Me.GridColumn101.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn101.Caption = "Selección"
        Me.GridColumn101.FieldName = "Item2"
        Me.GridColumn101.Name = "GridColumn101"
        '
        'GridColumn102
        '
        Me.GridColumn102.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn102.Caption = "Selección"
        Me.GridColumn102.FieldName = "Item2"
        Me.GridColumn102.Name = "GridColumn102"
        '
        'GridColumn99
        '
        Me.GridColumn99.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn99.Caption = "Selección"
        Me.GridColumn99.FieldName = "Item2"
        Me.GridColumn99.Name = "GridColumn99"
        '
        'GridColumn100
        '
        Me.GridColumn100.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn100.Caption = "Selección"
        Me.GridColumn100.FieldName = "Item2"
        Me.GridColumn100.Name = "GridColumn100"
        '
        'GridColumn97
        '
        Me.GridColumn97.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn97.Caption = "Selección"
        Me.GridColumn97.FieldName = "Item2"
        Me.GridColumn97.Name = "GridColumn97"
        '
        'GridColumn98
        '
        Me.GridColumn98.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn98.Caption = "Selección"
        Me.GridColumn98.FieldName = "Item2"
        Me.GridColumn98.Name = "GridColumn98"
        '
        'GridColumn95
        '
        Me.GridColumn95.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn95.Caption = "Selección"
        Me.GridColumn95.FieldName = "Item2"
        Me.GridColumn95.Name = "GridColumn95"
        '
        'GridColumn96
        '
        Me.GridColumn96.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn96.Caption = "Selección"
        Me.GridColumn96.FieldName = "Item2"
        Me.GridColumn96.Name = "GridColumn96"
        '
        'GridColumn93
        '
        Me.GridColumn93.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn93.Caption = "Selección"
        Me.GridColumn93.FieldName = "Item2"
        Me.GridColumn93.Name = "GridColumn93"
        '
        'GridColumn94
        '
        Me.GridColumn94.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn94.Caption = "Selección"
        Me.GridColumn94.FieldName = "Item2"
        Me.GridColumn94.Name = "GridColumn94"
        '
        'GridColumn91
        '
        Me.GridColumn91.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn91.Caption = "Selección"
        Me.GridColumn91.FieldName = "Item2"
        Me.GridColumn91.Name = "GridColumn91"
        '
        'GridColumn92
        '
        Me.GridColumn92.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn92.Caption = "Selección"
        Me.GridColumn92.FieldName = "Item2"
        Me.GridColumn92.Name = "GridColumn92"
        '
        'GridColumn89
        '
        Me.GridColumn89.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn89.Caption = "Selección"
        Me.GridColumn89.FieldName = "Item2"
        Me.GridColumn89.Name = "GridColumn89"
        '
        'GridColumn90
        '
        Me.GridColumn90.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn90.Caption = "Selección"
        Me.GridColumn90.FieldName = "Item2"
        Me.GridColumn90.Name = "GridColumn90"
        '
        'GridColumn87
        '
        Me.GridColumn87.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn87.Caption = "Selección"
        Me.GridColumn87.FieldName = "Item2"
        Me.GridColumn87.Name = "GridColumn87"
        '
        'GridColumn88
        '
        Me.GridColumn88.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn88.Caption = "Selección"
        Me.GridColumn88.FieldName = "Item2"
        Me.GridColumn88.Name = "GridColumn88"
        '
        'GridColumn85
        '
        Me.GridColumn85.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn85.Caption = "Selección"
        Me.GridColumn85.FieldName = "Item2"
        Me.GridColumn85.Name = "GridColumn85"
        '
        'GridColumn86
        '
        Me.GridColumn86.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn86.Caption = "Selección"
        Me.GridColumn86.FieldName = "Item2"
        Me.GridColumn86.Name = "GridColumn86"
        '
        'GridColumn83
        '
        Me.GridColumn83.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn83.Caption = "Selección"
        Me.GridColumn83.FieldName = "Item2"
        Me.GridColumn83.Name = "GridColumn83"
        '
        'GridColumn84
        '
        Me.GridColumn84.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn84.Caption = "Selección"
        Me.GridColumn84.FieldName = "Item2"
        Me.GridColumn84.Name = "GridColumn84"
        '
        'GridColumn81
        '
        Me.GridColumn81.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn81.Caption = "Selección"
        Me.GridColumn81.FieldName = "Item2"
        Me.GridColumn81.Name = "GridColumn81"
        '
        'GridColumn82
        '
        Me.GridColumn82.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn82.Caption = "Selección"
        Me.GridColumn82.FieldName = "Item2"
        Me.GridColumn82.Name = "GridColumn82"
        '
        'GridColumn79
        '
        Me.GridColumn79.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn79.Caption = "Selección"
        Me.GridColumn79.FieldName = "Item2"
        Me.GridColumn79.Name = "GridColumn79"
        '
        'GridColumn80
        '
        Me.GridColumn80.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn80.Caption = "Selección"
        Me.GridColumn80.FieldName = "Item2"
        Me.GridColumn80.Name = "GridColumn80"
        '
        'GridColumn77
        '
        Me.GridColumn77.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn77.Caption = "Selección"
        Me.GridColumn77.FieldName = "Item2"
        Me.GridColumn77.Name = "GridColumn77"
        '
        'GridColumn78
        '
        Me.GridColumn78.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn78.Caption = "Selección"
        Me.GridColumn78.FieldName = "Item2"
        Me.GridColumn78.Name = "GridColumn78"
        '
        'GridColumn72
        '
        Me.GridColumn72.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn72.Caption = "Selección"
        Me.GridColumn72.FieldName = "Item2"
        Me.GridColumn72.Name = "GridColumn72"
        '
        'GridColumn73
        '
        Me.GridColumn73.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn73.Caption = "Selección"
        Me.GridColumn73.FieldName = "Item2"
        Me.GridColumn73.Name = "GridColumn73"
        '
        'GridColumn70
        '
        Me.GridColumn70.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn70.Caption = "Selección"
        Me.GridColumn70.FieldName = "Item2"
        Me.GridColumn70.Name = "GridColumn70"
        '
        'GridColumn71
        '
        Me.GridColumn71.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn71.Caption = "Selección"
        Me.GridColumn71.FieldName = "Item2"
        Me.GridColumn71.Name = "GridColumn71"
        '
        'GridColumn68
        '
        Me.GridColumn68.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn68.Caption = "Selección"
        Me.GridColumn68.FieldName = "Item2"
        Me.GridColumn68.Name = "GridColumn68"
        '
        'GridColumn69
        '
        Me.GridColumn69.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn69.Caption = "Selección"
        Me.GridColumn69.FieldName = "Item2"
        Me.GridColumn69.Name = "GridColumn69"
        '
        'GridColumn66
        '
        Me.GridColumn66.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn66.Caption = "Selección"
        Me.GridColumn66.FieldName = "Item2"
        Me.GridColumn66.Name = "GridColumn66"
        '
        'GridColumn67
        '
        Me.GridColumn67.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn67.Caption = "Selección"
        Me.GridColumn67.FieldName = "Item2"
        Me.GridColumn67.Name = "GridColumn67"
        '
        'GridColumn64
        '
        Me.GridColumn64.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn64.Caption = "Selección"
        Me.GridColumn64.FieldName = "Item2"
        Me.GridColumn64.Name = "GridColumn64"
        '
        'GridColumn65
        '
        Me.GridColumn65.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn65.Caption = "Selección"
        Me.GridColumn65.FieldName = "Item2"
        Me.GridColumn65.Name = "GridColumn65"
        '
        'GridColumn62
        '
        Me.GridColumn62.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn62.Caption = "Selección"
        Me.GridColumn62.FieldName = "Item2"
        Me.GridColumn62.Name = "GridColumn62"
        '
        'GridColumn63
        '
        Me.GridColumn63.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn63.Caption = "Selección"
        Me.GridColumn63.FieldName = "Item2"
        Me.GridColumn63.Name = "GridColumn63"
        '
        'GridColumn60
        '
        Me.GridColumn60.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn60.Caption = "Selección"
        Me.GridColumn60.FieldName = "Item2"
        Me.GridColumn60.Name = "GridColumn60"
        '
        'GridColumn61
        '
        Me.GridColumn61.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn61.Caption = "Selección"
        Me.GridColumn61.FieldName = "Item2"
        Me.GridColumn61.Name = "GridColumn61"
        '
        'GridColumn58
        '
        Me.GridColumn58.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn58.Caption = "Selección"
        Me.GridColumn58.FieldName = "Item2"
        Me.GridColumn58.Name = "GridColumn58"
        '
        'GridColumn59
        '
        Me.GridColumn59.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn59.Caption = "Selección"
        Me.GridColumn59.FieldName = "Item2"
        Me.GridColumn59.Name = "GridColumn59"
        '
        'GridColumn56
        '
        Me.GridColumn56.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn56.Caption = "Selección"
        Me.GridColumn56.FieldName = "Item2"
        Me.GridColumn56.Name = "GridColumn56"
        '
        'GridColumn57
        '
        Me.GridColumn57.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn57.Caption = "Selección"
        Me.GridColumn57.FieldName = "Item2"
        Me.GridColumn57.Name = "GridColumn57"
        '
        'GridColumn54
        '
        Me.GridColumn54.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn54.Caption = "Selección"
        Me.GridColumn54.FieldName = "Item2"
        Me.GridColumn54.Name = "GridColumn54"
        '
        'GridColumn55
        '
        Me.GridColumn55.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn55.Caption = "Selección"
        Me.GridColumn55.FieldName = "Item2"
        Me.GridColumn55.Name = "GridColumn55"
        '
        'GridColumn52
        '
        Me.GridColumn52.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn52.Caption = "Selección"
        Me.GridColumn52.FieldName = "Item2"
        Me.GridColumn52.Name = "GridColumn52"
        '
        'GridColumn53
        '
        Me.GridColumn53.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn53.Caption = "Selección"
        Me.GridColumn53.FieldName = "Item2"
        Me.GridColumn53.Name = "GridColumn53"
        '
        'GridColumn50
        '
        Me.GridColumn50.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn50.Caption = "Selección"
        Me.GridColumn50.FieldName = "Item2"
        Me.GridColumn50.Name = "GridColumn50"
        '
        'GridColumn51
        '
        Me.GridColumn51.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn51.Caption = "Selección"
        Me.GridColumn51.FieldName = "Item2"
        Me.GridColumn51.Name = "GridColumn51"
        '
        'GridColumn48
        '
        Me.GridColumn48.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn48.Caption = "Selección"
        Me.GridColumn48.FieldName = "Item2"
        Me.GridColumn48.Name = "GridColumn48"
        '
        'GridColumn49
        '
        Me.GridColumn49.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn49.Caption = "Selección"
        Me.GridColumn49.FieldName = "Item2"
        Me.GridColumn49.Name = "GridColumn49"
        '
        'GridColumn46
        '
        Me.GridColumn46.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn46.Caption = "Selección"
        Me.GridColumn46.FieldName = "Item2"
        Me.GridColumn46.Name = "GridColumn46"
        '
        'GridColumn47
        '
        Me.GridColumn47.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn47.Caption = "Selección"
        Me.GridColumn47.FieldName = "Item2"
        Me.GridColumn47.Name = "GridColumn47"
        '
        'GridColumn44
        '
        Me.GridColumn44.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn44.Caption = "Selección"
        Me.GridColumn44.FieldName = "Item2"
        Me.GridColumn44.Name = "GridColumn44"
        '
        'GridColumn45
        '
        Me.GridColumn45.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn45.Caption = "Selección"
        Me.GridColumn45.FieldName = "Item2"
        Me.GridColumn45.Name = "GridColumn45"
        '
        'GridColumn42
        '
        Me.GridColumn42.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn42.Caption = "Selección"
        Me.GridColumn42.FieldName = "Item2"
        Me.GridColumn42.Name = "GridColumn42"
        '
        'GridColumn43
        '
        Me.GridColumn43.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn43.Caption = "Selección"
        Me.GridColumn43.FieldName = "Item2"
        Me.GridColumn43.Name = "GridColumn43"
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
        '
        'GridColumn38
        '
        Me.GridColumn38.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn38.Caption = "Selección"
        Me.GridColumn38.FieldName = "Item2"
        Me.GridColumn38.Name = "GridColumn38"
        '
        'GridColumn39
        '
        Me.GridColumn39.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn39.Caption = "Selección"
        Me.GridColumn39.FieldName = "Item2"
        Me.GridColumn39.Name = "GridColumn39"
        '
        'GridColumn36
        '
        Me.GridColumn36.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn36.Caption = "Selección"
        Me.GridColumn36.FieldName = "Item2"
        Me.GridColumn36.Name = "GridColumn36"
        '
        'GridColumn37
        '
        Me.GridColumn37.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn37.Caption = "Selección"
        Me.GridColumn37.FieldName = "Item2"
        Me.GridColumn37.Name = "GridColumn37"
        '
        'GridColumn34
        '
        Me.GridColumn34.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn34.Caption = "Selección"
        Me.GridColumn34.FieldName = "Item2"
        Me.GridColumn34.Name = "GridColumn34"
        '
        'GridColumn35
        '
        Me.GridColumn35.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn35.Caption = "Selección"
        Me.GridColumn35.FieldName = "Item2"
        Me.GridColumn35.Name = "GridColumn35"
        '
        'GridColumn32
        '
        Me.GridColumn32.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn32.Caption = "Selección"
        Me.GridColumn32.FieldName = "Item2"
        Me.GridColumn32.Name = "GridColumn32"
        '
        'GridColumn33
        '
        Me.GridColumn33.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn33.Caption = "Selección"
        Me.GridColumn33.FieldName = "Item2"
        Me.GridColumn33.Name = "GridColumn33"
        '
        'GridColumn30
        '
        Me.GridColumn30.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn30.Caption = "Selección"
        Me.GridColumn30.FieldName = "Item2"
        Me.GridColumn30.Name = "GridColumn30"
        '
        'GridColumn31
        '
        Me.GridColumn31.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn31.Caption = "Selección"
        Me.GridColumn31.FieldName = "Item2"
        Me.GridColumn31.Name = "GridColumn31"
        '
        'GridColumn28
        '
        Me.GridColumn28.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn28.Caption = "Selección"
        Me.GridColumn28.FieldName = "Item2"
        Me.GridColumn28.Name = "GridColumn28"
        '
        'GridColumn29
        '
        Me.GridColumn29.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn29.Caption = "Selección"
        Me.GridColumn29.FieldName = "Item2"
        Me.GridColumn29.Name = "GridColumn29"
        '
        'GridColumn26
        '
        Me.GridColumn26.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn26.Caption = "Selección"
        Me.GridColumn26.FieldName = "Item2"
        Me.GridColumn26.Name = "GridColumn26"
        '
        'GridColumn27
        '
        Me.GridColumn27.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn27.Caption = "Selección"
        Me.GridColumn27.FieldName = "Item2"
        Me.GridColumn27.Name = "GridColumn27"
        '
        'GridColumn24
        '
        Me.GridColumn24.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn24.Caption = "Selección"
        Me.GridColumn24.FieldName = "Item2"
        Me.GridColumn24.Name = "GridColumn24"
        '
        'GridColumn25
        '
        Me.GridColumn25.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn25.Caption = "Selección"
        Me.GridColumn25.FieldName = "Item2"
        Me.GridColumn25.Name = "GridColumn25"
        '
        'GridColumn22
        '
        Me.GridColumn22.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn22.Caption = "Selección"
        Me.GridColumn22.FieldName = "Item2"
        Me.GridColumn22.Name = "GridColumn22"
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.Caption = "Selección"
        Me.GridColumn23.FieldName = "Item2"
        Me.GridColumn23.Name = "GridColumn23"
        '
        'GridColumn20
        '
        Me.GridColumn20.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn20.Caption = "Selección"
        Me.GridColumn20.FieldName = "Item2"
        Me.GridColumn20.Name = "GridColumn20"
        '
        'GridColumn21
        '
        Me.GridColumn21.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn21.Caption = "Selección"
        Me.GridColumn21.FieldName = "Item2"
        Me.GridColumn21.Name = "GridColumn21"
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn18.Caption = "Selección"
        Me.GridColumn18.FieldName = "Item2"
        Me.GridColumn18.Name = "GridColumn18"
        '
        'GridColumn19
        '
        Me.GridColumn19.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19.Caption = "Selección"
        Me.GridColumn19.FieldName = "Item2"
        Me.GridColumn19.Name = "GridColumn19"
        '
        'GridColumn16
        '
        Me.GridColumn16.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn16.Caption = "Selección"
        Me.GridColumn16.FieldName = "Item2"
        Me.GridColumn16.Name = "GridColumn16"
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn17.Caption = "Selección"
        Me.GridColumn17.FieldName = "Item2"
        Me.GridColumn17.Name = "GridColumn17"
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn14.Caption = "Selección"
        Me.GridColumn14.FieldName = "Item2"
        Me.GridColumn14.Name = "GridColumn14"
        '
        'GridColumn15
        '
        Me.GridColumn15.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn15.Caption = "Selección"
        Me.GridColumn15.FieldName = "Item2"
        Me.GridColumn15.Name = "GridColumn15"
        '
        'GridColumn12
        '
        Me.GridColumn12.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn12.Caption = "Selección"
        Me.GridColumn12.FieldName = "Item2"
        Me.GridColumn12.Name = "GridColumn12"
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn13.Caption = "Selección"
        Me.GridColumn13.FieldName = "Item2"
        Me.GridColumn13.Name = "GridColumn13"
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn10.Caption = "Selección"
        Me.GridColumn10.FieldName = "Item2"
        Me.GridColumn10.Name = "GridColumn10"
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn11.Caption = "Selección"
        Me.GridColumn11.FieldName = "Item2"
        Me.GridColumn11.Name = "GridColumn11"
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn8.Caption = "Selección"
        Me.GridColumn8.FieldName = "Item2"
        Me.GridColumn8.Name = "GridColumn8"
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
        'GridColumn7
        '
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.Caption = "Selección"
        Me.GridColumn7.FieldName = "Item2"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.Caption = "Selección"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.Caption = "Selección"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.Caption = "Selección"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.Caption = "Selección"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'GridColumn210
        '
        Me.GridColumn210.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn210.Caption = "Selección"
        Me.GridColumn210.FieldName = "Item2"
        Me.GridColumn210.Name = "GridColumn210"
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.Caption = "Selección"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        '
        'GridColumn1691
        '
        Me.GridColumn1691.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1691.Caption = "Selección"
        Me.GridColumn1691.FieldName = "Item2"
        Me.GridColumn1691.Name = "GridColumn1691"
        '
        'FrmInventoryContractModification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1554, 787)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FrmInventoryContractModification"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Tag = "2117"
        Me.Text = "Otro si de Contrato"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyContract.ResumeLayout(False)
        CType(Me.INDSleBudgetaryValidityId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleBudgetaryEntityId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAvailability.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewSearchAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepTxtValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleContract.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleModificationType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGlvContractType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMmoDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGpMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGpDetaill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciModificationType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBudgetaryEntityId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBudgetaryValidityId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyContract As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyGpMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciModificationType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyGpDetaill As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDGleModificationType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend INDGlvContractType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDMmoDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn210 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
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
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCodeContractType As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents INDSleContract As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciContract As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents GridColumn106 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn107 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn108 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents GridColumn109 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn110 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn114 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn115 As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents INDlygBudget As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents GridColumn129 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn130 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn131 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn132 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcAvailability As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAvailability As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemGridAvailability As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn133 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn134 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents GridColumn135 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn136 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn137 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn138 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleAvailability As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewSearchAvailability As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn139 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn140 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemAvailability As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAddAvailability As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn141 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn142 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemAddAvailability As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn143 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn144 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn147 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn149 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn150 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn151 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn152 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn154 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn155 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn156 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn158 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn159 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn160 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn145 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn146 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn148 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn161 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn153 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn162 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepTxtValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents IndigoGridView2 As IndigoGridView
    Friend WithEvents GridColumn157 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn163 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn164 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn165 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn166 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn167 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDdeDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents GridColumn168 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn169 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1691 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn170 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn171 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn172 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridColumn173 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn174 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn175 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn176 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn177 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn178 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleBudgetaryValidityId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleBudgetaryEntityId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn179 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn180 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn181 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciBudgetaryEntityId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciBudgetaryValidityId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn182 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn183 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn184 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn187 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn188 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn189 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn190 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn185 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn186 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn191 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn192 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn193 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn194 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn195 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn196 As DevExpress.XtraGrid.Columns.GridColumn
End Class
