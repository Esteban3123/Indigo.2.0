Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMaintenanceContract
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
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMaintenanceContract))
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem3 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
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
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyContract = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdeDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleSupplier = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn76 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleOnlyGuarantee = New Presentation.Controls.CtrYesNo()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn217 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn219 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleExclusivity = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn218 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn220 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMmoAttachments = New DevExpress.XtraEditors.MemoEdit()
        Me.INDMmoClauses = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxtSupervicionExecution = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtTechnicalSupervicion = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtContractNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDDteInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDteEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDGleContractType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGlvContractType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColCodeContractType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNameContractType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColTypeContracType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMmoDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDGleSourcerOrder = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGleViewSourcerOrder = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColSourceOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSlItemPlate = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn910 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1010 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn159 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcItemByPlate = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPlate = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn211 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn160 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGpMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlytxtCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGleContractType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyDteInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyDteEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyMmoDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGpDetaill = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlySleSupplier = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtContractNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGleExclusivity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGpMoreInfo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyTxtTechnicalSupervicion = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtSupervicionExecution = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGleOnlyGuarantee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGleSourcerOrder = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGpClausesAnnexes = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyMmoClauses = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyMmoAttachments = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgItemCatalog = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciFixedAssetPhysical = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGridFunctinalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn215 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn216 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn213 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn214 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn209 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn212 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn149 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn208 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn206 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn207 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn204 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn205 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn202 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn203 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn189 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn190 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn187 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn188 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn185 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn186 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn156 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn158 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn154 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn155 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn151 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn152 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn147 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn150 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn200 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn201 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn197 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn198 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn199 = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.GridColumn1691 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyContract.SuspendLayout()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleOnlyGuarantee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleOnlyGuarantee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleExclusivity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleExclusivity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMmoAttachments.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMmoClauses.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtSupervicionExecution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtTechnicalSupervicion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtContractNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleContractType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGlvContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMmoDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleSourcerOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleViewSourcerOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlItemPlate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcItemByPlate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPlate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGpMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlytxtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGleContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyDteInitialDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyDteEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyMmoDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGpDetaill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlySleSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtContractNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGleExclusivity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGpMoreInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtTechnicalSupervicion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtSupervicionExecution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGleOnlyGuarantee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGleSourcerOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGpClausesAnnexes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyMmoClauses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyMmoAttachments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgItemCatalog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFixedAssetPhysical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGridFunctinalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyContract)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 134)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1938, 653)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 4)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ToolBars.Size = New System.Drawing.Size(1938, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarraBotones.Size = New System.Drawing.Size(1938, 130)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyContract
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 6)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 645)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyContract
        '
        Me.INDlyContract.AllowCustomization = False
        Me.INDlyContract.Controls.Add(Me.INDdeDocumentDate)
        Me.INDlyContract.Controls.Add(Me.INDSleSupplier)
        Me.INDlyContract.Controls.Add(Me.INDGleOnlyGuarantee)
        Me.INDlyContract.Controls.Add(Me.INDGleExclusivity)
        Me.INDlyContract.Controls.Add(Me.INDMmoAttachments)
        Me.INDlyContract.Controls.Add(Me.INDMmoClauses)
        Me.INDlyContract.Controls.Add(Me.INDTxtSupervicionExecution)
        Me.INDlyContract.Controls.Add(Me.INDTxtTechnicalSupervicion)
        Me.INDlyContract.Controls.Add(Me.INDTxtContractNumber)
        Me.INDlyContract.Controls.Add(Me.INDDteInitialDate)
        Me.INDlyContract.Controls.Add(Me.INDDteEndDate)
        Me.INDlyContract.Controls.Add(Me.INDBtnCode)
        Me.INDlyContract.Controls.Add(Me.INDGleContractType)
        Me.INDlyContract.Controls.Add(Me.INDMmoDescription)
        Me.INDlyContract.Controls.Add(Me.INDGleSourcerOrder)
        Me.INDlyContract.Controls.Add(Me.INDSlItemPlate)
        Me.INDlyContract.Controls.Add(Me.INDGcItemByPlate)
        Me.INDlyContract.Controls.Add(Me.INDBtnAdd)
        Me.INDlyContract.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyContract, False)
        Me.INDlyContract.Location = New System.Drawing.Point(202, 6)
        Me.INDlyContract.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyContract.Name = "INDlyContract"
        Me.INDlyContract.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1275, 390, 574, 569)
        Me.INDlyContract.Root = Me.LayoutControlGroup1
        Me.INDlyContract.Size = New System.Drawing.Size(1734, 645)
        Me.INDlyContract.TabIndex = 1
        Me.INDlyContract.Text = "LayoutControl1"
        '
        'INDdeDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDocumentDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDocumentDate, True)
        Me.INDdeDocumentDate.EditValue = Nothing
        Me.INDdeDocumentDate.EnterMoveNextControl = True
        Me.INDdeDocumentDate.Location = New System.Drawing.Point(24, 201)
        Me.INDdeDocumentDate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDocumentDate.Name = "INDdeDocumentDate"
        Me.INDdeDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDocumentDate.StyleController = Me.INDlyContract
        Me.INDdeDocumentDate.TabIndex = 35
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDocumentDate, 0)
        Me.INDdeDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDSleSupplier
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleSupplier, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleSupplier, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleSupplier, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.Location = New System.Drawing.Point(438, 137)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleSupplier, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleSupplier.Name = "INDSleSupplier"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleSupplier.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleSupplier.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDSleSupplier.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleSupplier.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleSupplier.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleSupplier.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleSupplier.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleSupplier.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleSupplier.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSupplier.Properties.DisplayMember = "DisplaySupplier"
        Me.INDSleSupplier.Properties.NullText = ""
        Me.INDSleSupplier.Properties.PopupFormMinSize = New System.Drawing.Size(600, 0)
        Me.INDSleSupplier.Properties.PopupSizeable = False
        Me.INDSleSupplier.Properties.PopupView = Me.viewSupplier
        Me.INDSleSupplier.Properties.ShowClearButton = False
        Me.INDSleSupplier.Properties.ShowFooter = False
        Me.INDSleSupplier.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleSupplier, True)
        Me.INDSleSupplier.Size = New System.Drawing.Size(386, 28)
        Me.INDSleSupplier.StyleController = Me.INDlyContract
        Me.INDSleSupplier.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleSupplier, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleSupplier, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleSupplier, "{0} - {1}")
        Me.INDSleSupplier.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleSupplier, False)
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
        Me.viewSupplier.GroupCount = 1
        Me.viewSupplier.Name = "viewSupplier"
        Me.viewSupplier.OptionsBehavior.AutoExpandAllGroups = True
        Me.viewSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.viewSupplier.OptionsView.ShowAutoFilterRow = True
        Me.viewSupplier.OptionsView.ShowGroupPanel = False
        Me.viewSupplier.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn76, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewSupplier, False)
        '
        'GridColumn74
        '
        Me.GridColumn74.Caption = "Nit"
        Me.GridColumn74.FieldName = "IdSupplier.IdThirdParty.Nit"
        Me.GridColumn74.Name = "GridColumn74"
        Me.GridColumn74.Visible = True
        Me.GridColumn74.VisibleIndex = 0
        Me.GridColumn74.Width = 352
        '
        'GridColumn75
        '
        Me.GridColumn75.Caption = "Proveedor"
        Me.GridColumn75.FieldName = "IdSupplier.CodeName"
        Me.GridColumn75.Name = "GridColumn75"
        Me.GridColumn75.Visible = True
        Me.GridColumn75.VisibleIndex = 1
        Me.GridColumn75.Width = 657
        '
        'GridColumn76
        '
        Me.GridColumn76.Caption = "Línea Distribución"
        Me.GridColumn76.FieldName = "IdDistributionLine.CodeName"
        Me.GridColumn76.Name = "GridColumn76"
        Me.GridColumn76.Visible = True
        Me.GridColumn76.VisibleIndex = 2
        Me.GridColumn76.Width = 623
        '
        'INDGleOnlyGuarantee
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleOnlyGuarantee, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleOnlyGuarantee, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleOnlyGuarantee, False)
        Me.INDGleOnlyGuarantee.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleOnlyGuarantee, False)
        Me.INDGleOnlyGuarantee.Location = New System.Drawing.Point(852, 137)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleOnlyGuarantee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleOnlyGuarantee.Name = "INDGleOnlyGuarantee"
        Me.INDGleOnlyGuarantee.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleOnlyGuarantee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleOnlyGuarantee.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleOnlyGuarantee.Properties.Appearance.Options.UseFont = True
        Me.INDGleOnlyGuarantee.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleOnlyGuarantee.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleOnlyGuarantee.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleOnlyGuarantee.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleOnlyGuarantee.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleOnlyGuarantee.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleOnlyGuarantee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleOnlyGuarantee.Properties.DataSource = CType(resources.GetObject("INDGleOnlyGuarantee.Properties.DataSource"), Object)
        Me.INDGleOnlyGuarantee.Properties.DisplayMember = "Item2"
        Me.INDGleOnlyGuarantee.Properties.ImmediatePopup = True
        Me.INDGleOnlyGuarantee.Properties.NullText = ""
        Me.INDGleOnlyGuarantee.Properties.PopupView = Me.GridView2
        Me.INDGleOnlyGuarantee.Properties.ValueMember = "Item1"
        Me.INDGleOnlyGuarantee.Size = New System.Drawing.Size(386, 28)
        Me.INDGleOnlyGuarantee.StyleController = Me.INDlyContract
        ToolTipItem2.Text = "Permite crear obligación por valor de los débitos."
        SuperToolTip2.Items.Add(ToolTipItem2)
        Me.INDGleOnlyGuarantee.SuperTip = SuperToolTip2
        Me.INDGleOnlyGuarantee.TabIndex = 13
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleOnlyGuarantee, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleOnlyGuarantee, 0)
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
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn219})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowDetailButtons = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn217
        '
        Me.GridColumn217.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn217.Caption = "Selección"
        Me.GridColumn217.FieldName = "Item2"
        Me.GridColumn217.Name = "GridColumn217"
        '
        'GridColumn219
        '
        Me.GridColumn219.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn219.Caption = "Selección"
        Me.GridColumn219.FieldName = "Item2"
        Me.GridColumn219.Name = "GridColumn219"
        Me.GridColumn219.Visible = True
        Me.GridColumn219.VisibleIndex = 0
        '
        'INDGleExclusivity
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleExclusivity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleExclusivity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleExclusivity, True)
        Me.INDGleExclusivity.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleExclusivity, False)
        Me.INDGleExclusivity.Location = New System.Drawing.Point(438, 201)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleExclusivity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleExclusivity.Name = "INDGleExclusivity"
        Me.INDGleExclusivity.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleExclusivity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleExclusivity.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleExclusivity.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleExclusivity.Properties.Appearance.Options.UseFont = True
        Me.INDGleExclusivity.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleExclusivity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleExclusivity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleExclusivity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleExclusivity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleExclusivity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleExclusivity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleExclusivity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleExclusivity.Properties.DataSource = CType(resources.GetObject("INDGleExclusivity.Properties.DataSource"), Object)
        Me.INDGleExclusivity.Properties.DisplayMember = "Item2"
        Me.INDGleExclusivity.Properties.ImmediatePopup = True
        Me.INDGleExclusivity.Properties.NullText = ""
        Me.INDGleExclusivity.Properties.PopupView = Me.CtrYesNo1View
        Me.INDGleExclusivity.Properties.ValueMember = "Item1"
        Me.INDGleExclusivity.Size = New System.Drawing.Size(386, 28)
        Me.INDGleExclusivity.StyleController = Me.INDlyContract
        ToolTipItem3.Text = "Permite crear obligación por valor de los débitos."
        SuperToolTip3.Items.Add(ToolTipItem3)
        Me.INDGleExclusivity.SuperTip = SuperToolTip3
        Me.INDGleExclusivity.TabIndex = 12
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleExclusivity, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleExclusivity, 0)
        Me.INDGleExclusivity.ToolTip = "Este Campo es Necesario"
        '
        'CtrYesNo1View
        '
        Me.CtrYesNo1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CtrYesNo1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CtrYesNo1View.Appearance.FocusedRow.Options.UseFont = True
        Me.CtrYesNo1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.CtrYesNo1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo1View.Appearance.GroupRow.Options.UseFont = True
        Me.CtrYesNo1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.CtrYesNo1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo1View.Appearance.Row.Options.UseFont = True
        Me.CtrYesNo1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn220})
        Me.CtrYesNo1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo1View.Name = "CtrYesNo1View"
        Me.CtrYesNo1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo1View.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo1View.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo1View.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo1View.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CtrYesNo1View, False)
        '
        'GridColumn218
        '
        Me.GridColumn218.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn218.Caption = "Selección"
        Me.GridColumn218.FieldName = "Item2"
        Me.GridColumn218.Name = "GridColumn218"
        '
        'GridColumn220
        '
        Me.GridColumn220.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn220.Caption = "Selección"
        Me.GridColumn220.FieldName = "Item2"
        Me.GridColumn220.Name = "GridColumn220"
        Me.GridColumn220.Visible = True
        Me.GridColumn220.VisibleIndex = 0
        '
        'INDMmoAttachments
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmoAttachments, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmoAttachments, False)
        Me.INDMmoAttachments.EnterMoveNextControl = True
        Me.INDMmoAttachments.Location = New System.Drawing.Point(1266, 265)
        Me.INDMmoAttachments.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmoAttachments, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMmoAttachments.Name = "INDMmoAttachments"
        Me.INDMmoAttachments.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMmoAttachments.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoAttachments.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmoAttachments.Properties.Appearance.Options.UseFont = True
        Me.INDMmoAttachments.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMmoAttachments.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMmoAttachments.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoAttachments.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMmoAttachments.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMmoAttachments.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMmoAttachments.Size = New System.Drawing.Size(386, 168)
        Me.INDMmoAttachments.StyleController = Me.INDlyContract
        Me.INDMmoAttachments.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmoAttachments, 0)
        '
        'INDMmoClauses
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmoClauses, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmoClauses, False)
        Me.INDMmoClauses.EnterMoveNextControl = True
        Me.INDMmoClauses.Location = New System.Drawing.Point(1266, 73)
        Me.INDMmoClauses.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmoClauses, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMmoClauses.Name = "INDMmoClauses"
        Me.INDMmoClauses.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMmoClauses.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoClauses.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmoClauses.Properties.Appearance.Options.UseFont = True
        Me.INDMmoClauses.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMmoClauses.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMmoClauses.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoClauses.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMmoClauses.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMmoClauses.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMmoClauses.Size = New System.Drawing.Size(386, 168)
        Me.INDMmoClauses.StyleController = Me.INDlyContract
        Me.INDMmoClauses.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmoClauses, 0)
        '
        'INDTxtSupervicionExecution
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtSupervicionExecution, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtSupervicionExecution, False)
        Me.INDTxtSupervicionExecution.EnterMoveNextControl = True
        Me.INDTxtSupervicionExecution.Location = New System.Drawing.Point(852, 265)
        Me.INDTxtSupervicionExecution.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtSupervicionExecution, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtSupervicionExecution.Name = "INDTxtSupervicionExecution"
        Me.INDTxtSupervicionExecution.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtSupervicionExecution.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSupervicionExecution.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtSupervicionExecution.Properties.Appearance.Options.UseFont = True
        Me.INDTxtSupervicionExecution.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtSupervicionExecution.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtSupervicionExecution.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSupervicionExecution.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtSupervicionExecution.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtSupervicionExecution.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtSupervicionExecution.Properties.MaxLength = 50
        Me.INDTxtSupervicionExecution.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtSupervicionExecution.StyleController = Me.INDlyContract
        Me.INDTxtSupervicionExecution.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtSupervicionExecution, 0)
        '
        'INDTxtTechnicalSupervicion
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtTechnicalSupervicion, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtTechnicalSupervicion, False)
        Me.INDTxtTechnicalSupervicion.EnterMoveNextControl = True
        Me.INDTxtTechnicalSupervicion.Location = New System.Drawing.Point(852, 201)
        Me.INDTxtTechnicalSupervicion.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtTechnicalSupervicion, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtTechnicalSupervicion.Name = "INDTxtTechnicalSupervicion"
        Me.INDTxtTechnicalSupervicion.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtTechnicalSupervicion.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTechnicalSupervicion.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtTechnicalSupervicion.Properties.Appearance.Options.UseFont = True
        Me.INDTxtTechnicalSupervicion.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtTechnicalSupervicion.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtTechnicalSupervicion.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtTechnicalSupervicion.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtTechnicalSupervicion.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtTechnicalSupervicion.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtTechnicalSupervicion.Properties.MaxLength = 50
        Me.INDTxtTechnicalSupervicion.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtTechnicalSupervicion.StyleController = Me.INDlyContract
        Me.INDTxtTechnicalSupervicion.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtTechnicalSupervicion, 0)
        '
        'INDTxtContractNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtContractNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtContractNumber, True)
        Me.INDTxtContractNumber.EnterMoveNextControl = True
        Me.INDTxtContractNumber.Location = New System.Drawing.Point(438, 73)
        Me.INDTxtContractNumber.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtContractNumber, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtContractNumber.Name = "INDTxtContractNumber"
        Me.INDTxtContractNumber.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtContractNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtContractNumber.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtContractNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtContractNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTxtContractNumber.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtContractNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtContractNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtContractNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtContractNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtContractNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtContractNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtContractNumber.Properties.Mask.EditMask = "[0-9]+"
        Me.INDTxtContractNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtContractNumber.Properties.MaxLength = 100
        Me.INDTxtContractNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtContractNumber.StyleController = Me.INDlyContract
        Me.INDTxtContractNumber.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtContractNumber, 0)
        Me.INDTxtContractNumber.ToolTip = "Este Campo es Necesario"
        '
        'INDDteInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteInitialDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteInitialDate, True)
        Me.INDDteInitialDate.EditValue = Nothing
        Me.INDDteInitialDate.EnterMoveNextControl = True
        Me.INDDteInitialDate.Location = New System.Drawing.Point(24, 265)
        Me.INDDteInitialDate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteInitialDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteInitialDate.Name = "INDDteInitialDate"
        Me.INDDteInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteInitialDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDDteInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteInitialDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDDteInitialDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteInitialDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteInitialDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteInitialDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteInitialDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDteInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteInitialDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDteInitialDate.StyleController = Me.INDlyContract
        Me.INDDteInitialDate.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteInitialDate, 0)
        Me.INDDteInitialDate.ToolTip = "Este Campo es Necesario"
        '
        'INDDteEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteEndDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteEndDate, True)
        Me.INDDteEndDate.EditValue = Nothing
        Me.INDDteEndDate.EnterMoveNextControl = True
        Me.INDDteEndDate.Location = New System.Drawing.Point(24, 329)
        Me.INDDteEndDate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDteEndDate.Name = "INDDteEndDate"
        Me.INDDteEndDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteEndDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDDteEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteEndDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDDteEndDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteEndDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteEndDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteEndDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteEndDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDteEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteEndDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDteEndDate.StyleController = Me.INDlyContract
        Me.INDDteEndDate.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteEndDate, 0)
        Me.INDDteEndDate.ToolTip = "Este Campo es Necesario"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(24, 73)
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
        EditorButtonImageOptions2.Image = Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Properties.MaxLength = 20
        Me.INDBtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBtnCode.StyleController = Me.INDlyContract
        Me.INDBtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDGleContractType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDGleContractType, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDGleContractType, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDGleContractType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDGleContractType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleContractType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDGleContractType, False)
        Me.INDGleContractType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDGleContractType, False)
        Me.INDGleContractType.Location = New System.Drawing.Point(24, 137)
        Me.INDGleContractType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleContractType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleContractType.Name = "INDGleContractType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDGleContractType, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDGleContractType, False)
        Me.INDGleContractType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleContractType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleContractType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDGleContractType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleContractType.Properties.Appearance.Options.UseFont = True
        Me.INDGleContractType.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleContractType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleContractType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleContractType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleContractType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleContractType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleContractType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleContractType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleContractType.Properties.DisplayMember = "CodeName"
        Me.INDGleContractType.Properties.NullText = ""
        Me.INDGleContractType.Properties.PopupSizeable = False
        Me.INDGleContractType.Properties.PopupView = Me.INDGlvContractType
        Me.INDGleContractType.Properties.ShowClearButton = False
        Me.INDGleContractType.Properties.ShowFooter = False
        Me.INDGleContractType.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDGleContractType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDGleContractType, True)
        Me.INDGleContractType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleContractType.StyleController = Me.INDlyContract
        Me.INDGleContractType.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDGleContractType, "1400")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleContractType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDGleContractType, "{0} - {1}")
        Me.INDGleContractType.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDGleContractType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDGleContractType, False)
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
        Me.INDGlvContractType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColCodeContractType, Me.ColNameContractType, Me.ColTypeContracType})
        Me.INDGlvContractType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGlvContractType.Name = "INDGlvContractType"
        Me.INDGlvContractType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGlvContractType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGlvContractType.OptionsView.EnableAppearanceOddRow = True
        Me.INDGlvContractType.OptionsView.ShowAutoFilterRow = True
        Me.INDGlvContractType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGlvContractType, False)
        '
        'ColCodeContractType
        '
        Me.ColCodeContractType.Caption = "Código"
        Me.ColCodeContractType.FieldName = "Code"
        Me.ColCodeContractType.Name = "ColCodeContractType"
        Me.ColCodeContractType.Visible = True
        Me.ColCodeContractType.VisibleIndex = 0
        Me.ColCodeContractType.Width = 250
        '
        'ColNameContractType
        '
        Me.ColNameContractType.Caption = "Nombre"
        Me.ColNameContractType.FieldName = "Name"
        Me.ColNameContractType.Name = "ColNameContractType"
        Me.ColNameContractType.Visible = True
        Me.ColNameContractType.VisibleIndex = 1
        Me.ColNameContractType.Width = 446
        '
        'ColTypeContracType
        '
        Me.ColTypeContracType.Caption = "Tipo"
        Me.ColTypeContracType.FieldName = "TypenName"
        Me.ColTypeContracType.Name = "ColTypeContracType"
        Me.ColTypeContracType.Visible = True
        Me.ColTypeContracType.VisibleIndex = 2
        '
        'INDMmoDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmoDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmoDescription, True)
        Me.INDMmoDescription.EnterMoveNextControl = True
        Me.INDMmoDescription.Location = New System.Drawing.Point(24, 393)
        Me.INDMmoDescription.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmoDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMmoDescription.Name = "INDMmoDescription"
        Me.INDMmoDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMmoDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMmoDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDMmoDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDMmoDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMmoDescription.Properties.Appearance.Options.UseForeColor = True
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
        'INDGleSourcerOrder
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDGleSourcerOrder, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDGleSourcerOrder, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDGleSourcerOrder, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.INDGleSourcerOrder.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.INDGleSourcerOrder.Location = New System.Drawing.Point(852, 73)
        Me.INDGleSourcerOrder.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleSourcerOrder, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleSourcerOrder.Name = "INDGleSourcerOrder"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.INDGleSourcerOrder.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleSourcerOrder.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleSourcerOrder.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDGleSourcerOrder.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleSourcerOrder.Properties.Appearance.Options.UseFont = True
        Me.INDGleSourcerOrder.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleSourcerOrder.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleSourcerOrder.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleSourcerOrder.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleSourcerOrder.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleSourcerOrder.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleSourcerOrder.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleSourcerOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleSourcerOrder.Properties.DisplayMember = "Item2"
        Me.INDGleSourcerOrder.Properties.NullText = ""
        Me.INDGleSourcerOrder.Properties.PopupSizeable = False
        Me.INDGleSourcerOrder.Properties.PopupView = Me.INDGleViewSourcerOrder
        Me.INDGleSourcerOrder.Properties.ShowClearButton = False
        Me.INDGleSourcerOrder.Properties.ShowFooter = False
        Me.INDGleSourcerOrder.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDGleSourcerOrder, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDGleSourcerOrder, True)
        Me.INDGleSourcerOrder.Size = New System.Drawing.Size(386, 28)
        Me.INDGleSourcerOrder.StyleController = Me.INDlyContract
        Me.INDGleSourcerOrder.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDGleSourcerOrder, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleSourcerOrder, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDGleSourcerOrder, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDGleSourcerOrder, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDGleSourcerOrder, False)
        '
        'INDGleViewSourcerOrder
        '
        Me.INDGleViewSourcerOrder.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGleViewSourcerOrder.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGleViewSourcerOrder.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGleViewSourcerOrder.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGleViewSourcerOrder.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGleViewSourcerOrder.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleViewSourcerOrder.Appearance.GroupRow.Options.UseFont = True
        Me.INDGleViewSourcerOrder.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleViewSourcerOrder.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGleViewSourcerOrder.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGleViewSourcerOrder.Appearance.Row.Options.UseFont = True
        Me.INDGleViewSourcerOrder.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColSourceOrder})
        Me.INDGleViewSourcerOrder.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGleViewSourcerOrder.Name = "INDGleViewSourcerOrder"
        Me.INDGleViewSourcerOrder.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGleViewSourcerOrder.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGleViewSourcerOrder.OptionsView.EnableAppearanceOddRow = True
        Me.INDGleViewSourcerOrder.OptionsView.ShowAutoFilterRow = True
        Me.INDGleViewSourcerOrder.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGleViewSourcerOrder, False)
        '
        'ColSourceOrder
        '
        Me.ColSourceOrder.Caption = "Origen Según Cuantia"
        Me.ColSourceOrder.FieldName = "Item2"
        Me.ColSourceOrder.Name = "ColSourceOrder"
        Me.ColSourceOrder.Visible = True
        Me.ColSourceOrder.VisibleIndex = 0
        '
        'INDSlItemPlate
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlItemPlate, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlItemPlate, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSlItemPlate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlItemPlate, False)
        Me.INDSlItemPlate.Location = New System.Drawing.Point(1844, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDSlItemPlate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlItemPlate.Name = "INDSlItemPlate"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlItemPlate, False)
        Me.INDSlItemPlate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlItemPlate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlItemPlate.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlItemPlate.Properties.Appearance.Options.UseFont = True
        Me.INDSlItemPlate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSlItemPlate.Properties.DisplayMember = "Plate"
        Me.INDSlItemPlate.Properties.NullText = ""
        Me.INDSlItemPlate.Properties.PopupSizeable = False
        Me.INDSlItemPlate.Properties.PopupView = Me.SearchLookUpEdit4View
        Me.INDSlItemPlate.Properties.ShowFooter = False
        Me.INDSlItemPlate.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlItemPlate, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlItemPlate, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlItemPlate, True)
        Me.INDSlItemPlate.Size = New System.Drawing.Size(332, 28)
        Me.INDSlItemPlate.StyleController = Me.INDlyContract
        Me.INDSlItemPlate.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlItemPlate, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlItemPlate, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlItemPlate, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlItemPlate, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlItemPlate, False)
        '
        'SearchLookUpEdit4View
        '
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit4View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit4View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit4View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit4View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit4View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit4View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn910, Me.GridColumn1010, Me.GridColumn159})
        Me.SearchLookUpEdit4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit4View.Name = "SearchLookUpEdit4View"
        Me.SearchLookUpEdit4View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit4View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit4View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit4View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit4View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit4View, False)
        '
        'GridColumn910
        '
        Me.GridColumn910.Caption = "Código"
        Me.GridColumn910.FieldName = "ItemId.CodeDescription"
        Me.GridColumn910.Name = "GridColumn910"
        Me.GridColumn910.OptionsColumn.AllowEdit = False
        Me.GridColumn910.OptionsColumn.AllowFocus = False
        Me.GridColumn910.Visible = True
        Me.GridColumn910.VisibleIndex = 0
        '
        'GridColumn1010
        '
        Me.GridColumn1010.Caption = "Placa"
        Me.GridColumn1010.FieldName = "Plate"
        Me.GridColumn1010.Name = "GridColumn1010"
        Me.GridColumn1010.OptionsColumn.AllowEdit = False
        Me.GridColumn1010.OptionsColumn.AllowFocus = False
        Me.GridColumn1010.Visible = True
        Me.GridColumn1010.VisibleIndex = 1
        '
        'GridColumn159
        '
        Me.GridColumn159.Caption = "Descripción"
        Me.GridColumn159.FieldName = "AssetDescription"
        Me.GridColumn159.Name = "GridColumn159"
        Me.GridColumn159.Visible = True
        Me.GridColumn159.VisibleIndex = 2
        '
        'INDGcItemByPlate
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcItemByPlate, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcItemByPlate, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcItemByPlate, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcItemByPlate, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcItemByPlate, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcItemByPlate, False)
        Me.INDGcItemByPlate.Location = New System.Drawing.Point(1680, 89)
        Me.INDGcItemByPlate.MainView = Me.INDGvPlate
        Me.INDGcItemByPlate.Name = "INDGcItemByPlate"
        Me.INDGcItemByPlate.Size = New System.Drawing.Size(596, 515)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcItemByPlate, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcItemByPlate.TabIndex = 7
        Me.INDGcItemByPlate.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPlate})
        '
        'INDGvPlate
        '
        Me.INDGvPlate.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPlate.Appearance.Empty.Options.UseFont = True
        Me.INDGvPlate.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPlate.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvPlate.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvPlate.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPlate.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPlate.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvPlate.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPlate.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPlate.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPlate.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPlate.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPlate.Appearance.Row.Options.UseFont = True
        Me.INDGvPlate.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPlate.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvPlate.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn211, Me.GridColumn160})
        Me.INDGvPlate.GridControl = Me.INDGcItemByPlate
        Me.INDGvPlate.Name = "INDGvPlate"
        Me.INDGvPlate.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPlate.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPlate.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPlate.OptionsView.ShowDetailButtons = False
        Me.INDGvPlate.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPlate, False)
        '
        'GridColumn211
        '
        Me.GridColumn211.Caption = "Placa"
        Me.GridColumn211.FieldName = "DescriptionPlateName"
        Me.GridColumn211.Name = "GridColumn211"
        Me.GridColumn211.OptionsColumn.AllowEdit = False
        Me.GridColumn211.Visible = True
        Me.GridColumn211.VisibleIndex = 0
        Me.GridColumn211.Width = 80
        '
        'GridColumn160
        '
        Me.GridColumn160.Caption = "Descripción"
        Me.GridColumn160.FieldName = "DescriptionItem"
        Me.GridColumn160.Name = "GridColumn160"
        Me.GridColumn160.Visible = True
        Me.GridColumn160.VisibleIndex = 1
        Me.GridColumn160.Width = 200
        '
        'INDBtnAdd
        '
        Me.INDBtnAdd.ImageOptions.Image = Global.Presentation.Maintenance.My.Resources.Resources.Agregar16
        Me.INDBtnAdd.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter
        Me.INDBtnAdd.Location = New System.Drawing.Point(2180, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnAdd, False)
        Me.INDBtnAdd.Name = "INDBtnAdd"
        Me.INDBtnAdd.Size = New System.Drawing.Size(96, 32)
        Me.INDBtnAdd.StyleController = Me.INDlyContract
        Me.INDBtnAdd.TabIndex = 6
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGpMainData, Me.INDlyGpDetaill, Me.INDlyGpMoreInfo, Me.INDlyGpClausesAnnexes, Me.INDLcgItemCatalog})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2300, 628)
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
        Me.INDlyGpMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlytxtCode, Me.INDLyGleContractType, Me.INDLyDteInitialDate, Me.INDLyDteEndDate, Me.INDLyMmoDescription, Me.INDLciDocumentDate})
        Me.INDlyGpMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGpMainData.Name = "INDlyGpMainData"
        Me.INDlyGpMainData.Size = New System.Drawing.Size(414, 608)
        Me.INDlyGpMainData.Text = "Datos Principales"
        '
        'INDlytxtCode
        '
        Me.INDlytxtCode.Control = Me.INDBtnCode
        Me.INDlytxtCode.CustomizationFormText = "Código"
        Me.INDlytxtCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlytxtCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlytxtCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlytxtCode.Name = "INDlytxtCode"
        Me.INDlytxtCode.ShowInCustomizationForm = False
        Me.INDlytxtCode.Size = New System.Drawing.Size(390, 64)
        Me.INDlytxtCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlytxtCode.Text = "Código"
        Me.INDlytxtCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlytxtCode.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyGleContractType
        '
        Me.INDLyGleContractType.Control = Me.INDGleContractType
        Me.INDLyGleContractType.CustomizationFormText = "Tipo de Contrato"
        Me.INDLyGleContractType.Location = New System.Drawing.Point(0, 64)
        Me.INDLyGleContractType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyGleContractType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyGleContractType.Name = "INDLyGleContractType"
        Me.INDLyGleContractType.ShowInCustomizationForm = False
        Me.INDLyGleContractType.Size = New System.Drawing.Size(390, 64)
        Me.INDLyGleContractType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGleContractType.Text = "Tipo de Contrato"
        Me.INDLyGleContractType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyGleContractType.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyDteInitialDate
        '
        Me.INDLyDteInitialDate.Control = Me.INDDteInitialDate
        Me.INDLyDteInitialDate.CustomizationFormText = "Fecha Inicial"
        Me.INDLyDteInitialDate.Location = New System.Drawing.Point(0, 192)
        Me.INDLyDteInitialDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyDteInitialDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyDteInitialDate.Name = "INDLyDteInitialDate"
        Me.INDLyDteInitialDate.ShowInCustomizationForm = False
        Me.INDLyDteInitialDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLyDteInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyDteInitialDate.Text = "Fecha Inicial"
        Me.INDLyDteInitialDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyDteInitialDate.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyDteEndDate
        '
        Me.INDLyDteEndDate.Control = Me.INDDteEndDate
        Me.INDLyDteEndDate.CustomizationFormText = "Fecha Final"
        Me.INDLyDteEndDate.Location = New System.Drawing.Point(0, 256)
        Me.INDLyDteEndDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyDteEndDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyDteEndDate.Name = "INDLyDteEndDate"
        Me.INDLyDteEndDate.ShowInCustomizationForm = False
        Me.INDLyDteEndDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLyDteEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyDteEndDate.Text = "Fecha Final"
        Me.INDLyDteEndDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyDteEndDate.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyMmoDescription
        '
        Me.INDLyMmoDescription.Control = Me.INDMmoDescription
        Me.INDLyMmoDescription.CustomizationFormText = "Descripción"
        Me.INDLyMmoDescription.Location = New System.Drawing.Point(0, 320)
        Me.INDLyMmoDescription.MaxSize = New System.Drawing.Size(390, 128)
        Me.INDLyMmoDescription.MinSize = New System.Drawing.Size(390, 128)
        Me.INDLyMmoDescription.Name = "INDLyMmoDescription"
        Me.INDLyMmoDescription.ShowInCustomizationForm = False
        Me.INDLyMmoDescription.Size = New System.Drawing.Size(390, 235)
        Me.INDLyMmoDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyMmoDescription.Text = "Descripción"
        Me.INDLyMmoDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyMmoDescription.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLciDocumentDate
        '
        Me.INDLciDocumentDate.Control = Me.INDdeDocumentDate
        Me.INDLciDocumentDate.Location = New System.Drawing.Point(0, 128)
        Me.INDLciDocumentDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.Name = "INDLciDocumentDate"
        Me.INDLciDocumentDate.ShowInCustomizationForm = False
        Me.INDLciDocumentDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocumentDate.Text = "Fecha Documento"
        Me.INDLciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocumentDate.TextSize = New System.Drawing.Size(161, 17)
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
        Me.INDlyGpDetaill.CustomizationFormText = "Detalles del Contrato"
        Me.INDlyGpDetaill.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlySleSupplier, Me.INDLyTxtContractNumber, Me.INDLyGleExclusivity})
        Me.INDlyGpDetaill.Location = New System.Drawing.Point(414, 0)
        Me.INDlyGpDetaill.Name = "INDlyGpDetaill"
        Me.INDlyGpDetaill.Size = New System.Drawing.Size(414, 608)
        Me.INDlyGpDetaill.Text = "Detalles del Contrato"
        '
        'INDlySleSupplier
        '
        Me.INDlySleSupplier.Control = Me.INDSleSupplier
        Me.INDlySleSupplier.CustomizationFormText = "Proveedor"
        Me.INDlySleSupplier.Location = New System.Drawing.Point(0, 64)
        Me.INDlySleSupplier.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlySleSupplier.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlySleSupplier.Name = "INDlySleSupplier"
        Me.INDlySleSupplier.ShowInCustomizationForm = False
        Me.INDlySleSupplier.Size = New System.Drawing.Size(390, 64)
        Me.INDlySleSupplier.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlySleSupplier.Text = "Proveedor"
        Me.INDlySleSupplier.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlySleSupplier.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyTxtContractNumber
        '
        Me.INDLyTxtContractNumber.Control = Me.INDTxtContractNumber
        Me.INDLyTxtContractNumber.CustomizationFormText = "Número de Contrato"
        Me.INDLyTxtContractNumber.Location = New System.Drawing.Point(0, 0)
        Me.INDLyTxtContractNumber.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyTxtContractNumber.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyTxtContractNumber.Name = "INDLyTxtContractNumber"
        Me.INDLyTxtContractNumber.ShowInCustomizationForm = False
        Me.INDLyTxtContractNumber.Size = New System.Drawing.Size(390, 64)
        Me.INDLyTxtContractNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtContractNumber.Text = "Número de Contrato"
        Me.INDLyTxtContractNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtContractNumber.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyGleExclusivity
        '
        Me.INDLyGleExclusivity.Control = Me.INDGleExclusivity
        Me.INDLyGleExclusivity.CustomizationFormText = "Exclusividad"
        Me.INDLyGleExclusivity.Location = New System.Drawing.Point(0, 128)
        Me.INDLyGleExclusivity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyGleExclusivity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyGleExclusivity.Name = "INDLyGleExclusivity"
        Me.INDLyGleExclusivity.ShowInCustomizationForm = False
        Me.INDLyGleExclusivity.Size = New System.Drawing.Size(390, 427)
        Me.INDLyGleExclusivity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGleExclusivity.Text = "Exclusividad"
        Me.INDLyGleExclusivity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyGleExclusivity.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDlyGpMoreInfo
        '
        Me.INDlyGpMoreInfo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpMoreInfo.AppearanceGroup.Options.UseFont = True
        Me.INDlyGpMoreInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpMoreInfo.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGpMoreInfo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpMoreInfo.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGpMoreInfo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGpMoreInfo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGpMoreInfo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpMoreInfo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGpMoreInfo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpMoreInfo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGpMoreInfo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpMoreInfo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGpMoreInfo, False)
        Me.INDlyGpMoreInfo.CustomizationFormText = "Información Adicional"
        Me.INDlyGpMoreInfo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyTxtTechnicalSupervicion, Me.INDLyTxtSupervicionExecution, Me.INDLyGleOnlyGuarantee, Me.INDLyGleSourcerOrder})
        Me.INDlyGpMoreInfo.Location = New System.Drawing.Point(828, 0)
        Me.INDlyGpMoreInfo.Name = "INDlyGpMoreInfo"
        Me.INDlyGpMoreInfo.Size = New System.Drawing.Size(414, 608)
        Me.INDlyGpMoreInfo.Text = "Información Adicional"
        '
        'INDLyTxtTechnicalSupervicion
        '
        Me.INDLyTxtTechnicalSupervicion.Control = Me.INDTxtTechnicalSupervicion
        Me.INDLyTxtTechnicalSupervicion.CustomizationFormText = "Supervisión Técnica"
        Me.INDLyTxtTechnicalSupervicion.Location = New System.Drawing.Point(0, 128)
        Me.INDLyTxtTechnicalSupervicion.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyTxtTechnicalSupervicion.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyTxtTechnicalSupervicion.Name = "INDLyTxtTechnicalSupervicion"
        Me.INDLyTxtTechnicalSupervicion.Size = New System.Drawing.Size(390, 64)
        Me.INDLyTxtTechnicalSupervicion.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtTechnicalSupervicion.Text = "Supervisión Técnica"
        Me.INDLyTxtTechnicalSupervicion.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtTechnicalSupervicion.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyTxtSupervicionExecution
        '
        Me.INDLyTxtSupervicionExecution.Control = Me.INDTxtSupervicionExecution
        Me.INDLyTxtSupervicionExecution.CustomizationFormText = "Supervisión de Ejecución"
        Me.INDLyTxtSupervicionExecution.Location = New System.Drawing.Point(0, 192)
        Me.INDLyTxtSupervicionExecution.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyTxtSupervicionExecution.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyTxtSupervicionExecution.Name = "INDLyTxtSupervicionExecution"
        Me.INDLyTxtSupervicionExecution.Size = New System.Drawing.Size(390, 363)
        Me.INDLyTxtSupervicionExecution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtSupervicionExecution.Text = "Supervisión de Ejecución"
        Me.INDLyTxtSupervicionExecution.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyTxtSupervicionExecution.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyGleOnlyGuarantee
        '
        Me.INDLyGleOnlyGuarantee.Control = Me.INDGleOnlyGuarantee
        Me.INDLyGleOnlyGuarantee.CustomizationFormText = "Garantía Única"
        Me.INDLyGleOnlyGuarantee.Location = New System.Drawing.Point(0, 64)
        Me.INDLyGleOnlyGuarantee.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyGleOnlyGuarantee.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyGleOnlyGuarantee.Name = "INDLyGleOnlyGuarantee"
        Me.INDLyGleOnlyGuarantee.ShowInCustomizationForm = False
        Me.INDLyGleOnlyGuarantee.Size = New System.Drawing.Size(390, 64)
        Me.INDLyGleOnlyGuarantee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGleOnlyGuarantee.Text = "Garantía Única"
        Me.INDLyGleOnlyGuarantee.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyGleOnlyGuarantee.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyGleSourcerOrder
        '
        Me.INDLyGleSourcerOrder.Control = Me.INDGleSourcerOrder
        Me.INDLyGleSourcerOrder.CustomizationFormText = "Origen"
        Me.INDLyGleSourcerOrder.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGleSourcerOrder.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLyGleSourcerOrder.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLyGleSourcerOrder.Name = "INDLyGleSourcerOrder"
        Me.INDLyGleSourcerOrder.ShowInCustomizationForm = False
        Me.INDLyGleSourcerOrder.Size = New System.Drawing.Size(390, 64)
        Me.INDLyGleSourcerOrder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGleSourcerOrder.Text = "Origen"
        Me.INDLyGleSourcerOrder.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyGleSourcerOrder.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDlyGpClausesAnnexes
        '
        Me.INDlyGpClausesAnnexes.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpClausesAnnexes.AppearanceGroup.Options.UseFont = True
        Me.INDlyGpClausesAnnexes.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGpClausesAnnexes.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGpClausesAnnexes.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGpClausesAnnexes, False)
        Me.INDlyGpClausesAnnexes.CustomizationFormText = "Claúsulas y Anexos"
        Me.INDlyGpClausesAnnexes.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyMmoClauses, Me.INDLyMmoAttachments})
        Me.INDlyGpClausesAnnexes.Location = New System.Drawing.Point(1242, 0)
        Me.INDlyGpClausesAnnexes.Name = "INDlyGpClausesAnnexes"
        Me.INDlyGpClausesAnnexes.Size = New System.Drawing.Size(414, 608)
        Me.INDlyGpClausesAnnexes.Text = "Claúsulas y Anexos"
        '
        'INDLyMmoClauses
        '
        Me.INDLyMmoClauses.Control = Me.INDMmoClauses
        Me.INDLyMmoClauses.CustomizationFormText = "Claúsulas"
        Me.INDLyMmoClauses.Location = New System.Drawing.Point(0, 0)
        Me.INDLyMmoClauses.MaxSize = New System.Drawing.Size(390, 192)
        Me.INDLyMmoClauses.MinSize = New System.Drawing.Size(390, 192)
        Me.INDLyMmoClauses.Name = "INDLyMmoClauses"
        Me.INDLyMmoClauses.Size = New System.Drawing.Size(390, 192)
        Me.INDLyMmoClauses.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyMmoClauses.Text = "Claúsulas"
        Me.INDLyMmoClauses.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyMmoClauses.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLyMmoAttachments
        '
        Me.INDLyMmoAttachments.Control = Me.INDMmoAttachments
        Me.INDLyMmoAttachments.CustomizationFormText = "Anexos"
        Me.INDLyMmoAttachments.Location = New System.Drawing.Point(0, 192)
        Me.INDLyMmoAttachments.MaxSize = New System.Drawing.Size(390, 192)
        Me.INDLyMmoAttachments.MinSize = New System.Drawing.Size(390, 192)
        Me.INDLyMmoAttachments.Name = "INDLyMmoAttachments"
        Me.INDLyMmoAttachments.Size = New System.Drawing.Size(390, 363)
        Me.INDLyMmoAttachments.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyMmoAttachments.Text = "Anexos"
        Me.INDLyMmoAttachments.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyMmoAttachments.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLcgItemCatalog
        '
        Me.INDLcgItemCatalog.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgItemCatalog.AppearanceGroup.Options.UseFont = True
        Me.INDLcgItemCatalog.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgItemCatalog.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgItemCatalog.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgItemCatalog.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgItemCatalog.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgItemCatalog.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgItemCatalog.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgItemCatalog.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgItemCatalog.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgItemCatalog.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgItemCatalog.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgItemCatalog.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgItemCatalog, False)
        Me.INDLcgItemCatalog.CustomizationFormText = "Catalogo de Articulos"
        Me.INDLcgItemCatalog.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciFixedAssetPhysical, Me.INDLciGridFunctinalUnit, Me.LayoutControlItem1})
        Me.INDLcgItemCatalog.Location = New System.Drawing.Point(1656, 0)
        Me.INDLcgItemCatalog.Name = "INDLcgItemCatalog"
        Me.INDLcgItemCatalog.Size = New System.Drawing.Size(624, 608)
        Me.INDLcgItemCatalog.Text = "Listado de Activos"
        '
        'INDLciFixedAssetPhysical
        '
        Me.INDLciFixedAssetPhysical.Control = Me.INDSlItemPlate
        Me.INDLciFixedAssetPhysical.CustomizationFormText = "Unidad Funcional"
        Me.INDLciFixedAssetPhysical.Location = New System.Drawing.Point(0, 0)
        Me.INDLciFixedAssetPhysical.MaxSize = New System.Drawing.Size(500, 0)
        Me.INDLciFixedAssetPhysical.MinSize = New System.Drawing.Size(500, 32)
        Me.INDLciFixedAssetPhysical.Name = "INDLciFixedAssetPhysical"
        Me.INDLciFixedAssetPhysical.Size = New System.Drawing.Size(500, 36)
        Me.INDLciFixedAssetPhysical.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFixedAssetPhysical.Text = "Activos"
        Me.INDLciFixedAssetPhysical.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDLciFixedAssetPhysical.TextSize = New System.Drawing.Size(161, 17)
        '
        'INDLciGridFunctinalUnit
        '
        Me.INDLciGridFunctinalUnit.Control = Me.INDGcItemByPlate
        Me.INDLciGridFunctinalUnit.CustomizationFormText = "Grilla"
        Me.INDLciGridFunctinalUnit.Location = New System.Drawing.Point(0, 36)
        Me.INDLciGridFunctinalUnit.MaxSize = New System.Drawing.Size(600, 0)
        Me.INDLciGridFunctinalUnit.MinSize = New System.Drawing.Size(600, 24)
        Me.INDLciGridFunctinalUnit.Name = "INDLciGridFunctinalUnit"
        Me.INDLciGridFunctinalUnit.Size = New System.Drawing.Size(600, 519)
        Me.INDLciGridFunctinalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGridFunctinalUnit.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGridFunctinalUnit.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDBtnAdd
        Me.LayoutControlItem1.CustomizationFormText = "Add"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(500, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(100, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(100, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(100, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'GridColumn215
        '
        Me.GridColumn215.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn215.Caption = "Selección"
        Me.GridColumn215.FieldName = "Item2"
        Me.GridColumn215.Name = "GridColumn215"
        '
        'GridColumn216
        '
        Me.GridColumn216.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn216.Caption = "Selección"
        Me.GridColumn216.FieldName = "Item2"
        Me.GridColumn216.Name = "GridColumn216"
        '
        'GridColumn213
        '
        Me.GridColumn213.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn213.Caption = "Selección"
        Me.GridColumn213.FieldName = "Item2"
        Me.GridColumn213.Name = "GridColumn213"
        '
        'GridColumn214
        '
        Me.GridColumn214.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn214.Caption = "Selección"
        Me.GridColumn214.FieldName = "Item2"
        Me.GridColumn214.Name = "GridColumn214"
        '
        'GridColumn209
        '
        Me.GridColumn209.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn209.Caption = "Selección"
        Me.GridColumn209.FieldName = "Item2"
        Me.GridColumn209.Name = "GridColumn209"
        '
        'GridColumn212
        '
        Me.GridColumn212.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn212.Caption = "Selección"
        Me.GridColumn212.FieldName = "Item2"
        Me.GridColumn212.Name = "GridColumn212"
        '
        'GridColumn149
        '
        Me.GridColumn149.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn149.Caption = "Selección"
        Me.GridColumn149.FieldName = "Item2"
        Me.GridColumn149.Name = "GridColumn149"
        '
        'GridColumn208
        '
        Me.GridColumn208.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn208.Caption = "Selección"
        Me.GridColumn208.FieldName = "Item2"
        Me.GridColumn208.Name = "GridColumn208"
        '
        'GridColumn206
        '
        Me.GridColumn206.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn206.Caption = "Selección"
        Me.GridColumn206.FieldName = "Item2"
        Me.GridColumn206.Name = "GridColumn206"
        '
        'GridColumn207
        '
        Me.GridColumn207.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn207.Caption = "Selección"
        Me.GridColumn207.FieldName = "Item2"
        Me.GridColumn207.Name = "GridColumn207"
        '
        'GridColumn204
        '
        Me.GridColumn204.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn204.Caption = "Selección"
        Me.GridColumn204.FieldName = "Item2"
        Me.GridColumn204.Name = "GridColumn204"
        '
        'GridColumn205
        '
        Me.GridColumn205.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn205.Caption = "Selección"
        Me.GridColumn205.FieldName = "Item2"
        Me.GridColumn205.Name = "GridColumn205"
        '
        'GridColumn202
        '
        Me.GridColumn202.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn202.Caption = "Selección"
        Me.GridColumn202.FieldName = "Item2"
        Me.GridColumn202.Name = "GridColumn202"
        '
        'GridColumn203
        '
        Me.GridColumn203.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn203.Caption = "Selección"
        Me.GridColumn203.FieldName = "Item2"
        Me.GridColumn203.Name = "GridColumn203"
        '
        'GridColumn189
        '
        Me.GridColumn189.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn189.Caption = "Selección"
        Me.GridColumn189.FieldName = "Item2"
        Me.GridColumn189.Name = "GridColumn189"
        '
        'GridColumn190
        '
        Me.GridColumn190.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn190.Caption = "Selección"
        Me.GridColumn190.FieldName = "Item2"
        Me.GridColumn190.Name = "GridColumn190"
        '
        'GridColumn187
        '
        Me.GridColumn187.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn187.Caption = "Selección"
        Me.GridColumn187.FieldName = "Item2"
        Me.GridColumn187.Name = "GridColumn187"
        '
        'GridColumn188
        '
        Me.GridColumn188.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn188.Caption = "Selección"
        Me.GridColumn188.FieldName = "Item2"
        Me.GridColumn188.Name = "GridColumn188"
        '
        'GridColumn185
        '
        Me.GridColumn185.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn185.Caption = "Selección"
        Me.GridColumn185.FieldName = "Item2"
        Me.GridColumn185.Name = "GridColumn185"
        '
        'GridColumn186
        '
        Me.GridColumn186.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn186.Caption = "Selección"
        Me.GridColumn186.FieldName = "Item2"
        Me.GridColumn186.Name = "GridColumn186"
        '
        'GridColumn156
        '
        Me.GridColumn156.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn156.Caption = "Selección"
        Me.GridColumn156.FieldName = "Item2"
        Me.GridColumn156.Name = "GridColumn156"
        '
        'GridColumn158
        '
        Me.GridColumn158.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn158.Caption = "Selección"
        Me.GridColumn158.FieldName = "Item2"
        Me.GridColumn158.Name = "GridColumn158"
        '
        'GridColumn154
        '
        Me.GridColumn154.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn154.Caption = "Selección"
        Me.GridColumn154.FieldName = "Item2"
        Me.GridColumn154.Name = "GridColumn154"
        '
        'GridColumn155
        '
        Me.GridColumn155.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn155.Caption = "Selección"
        Me.GridColumn155.FieldName = "Item2"
        Me.GridColumn155.Name = "GridColumn155"
        '
        'GridColumn151
        '
        Me.GridColumn151.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn151.Caption = "Selección"
        Me.GridColumn151.FieldName = "Item2"
        Me.GridColumn151.Name = "GridColumn151"
        '
        'GridColumn152
        '
        Me.GridColumn152.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn152.Caption = "Selección"
        Me.GridColumn152.FieldName = "Item2"
        Me.GridColumn152.Name = "GridColumn152"
        '
        'GridColumn147
        '
        Me.GridColumn147.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn147.Caption = "Selección"
        Me.GridColumn147.FieldName = "Item2"
        Me.GridColumn147.Name = "GridColumn147"
        '
        'GridColumn150
        '
        Me.GridColumn150.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn150.Caption = "Selección"
        Me.GridColumn150.FieldName = "Item2"
        Me.GridColumn150.Name = "GridColumn150"
        '
        'GridColumn200
        '
        Me.GridColumn200.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn200.Caption = "Selección"
        Me.GridColumn200.FieldName = "Item2"
        Me.GridColumn200.Name = "GridColumn200"
        '
        'GridColumn201
        '
        Me.GridColumn201.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn201.Caption = "Selección"
        Me.GridColumn201.FieldName = "Item2"
        Me.GridColumn201.Name = "GridColumn201"
        '
        'GridColumn197
        '
        Me.GridColumn197.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn197.Caption = "Selección"
        Me.GridColumn197.FieldName = "Item2"
        Me.GridColumn197.Name = "GridColumn197"
        '
        'GridColumn198
        '
        Me.GridColumn198.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn198.Caption = "Selección"
        Me.GridColumn198.FieldName = "Item2"
        Me.GridColumn198.Name = "GridColumn198"
        '
        'GridColumn199
        '
        Me.GridColumn199.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn199.Caption = "Selección"
        Me.GridColumn199.FieldName = "Item2"
        Me.GridColumn199.Name = "GridColumn199"
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
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
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
        'GridColumn1691
        '
        Me.GridColumn1691.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1691.Caption = "Selección"
        Me.GridColumn1691.FieldName = "Item2"
        Me.GridColumn1691.Name = "GridColumn1691"
        '
        'FrmMaintenanceContract
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1938, 787)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FrmMaintenanceContract"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Tag = "2214"
        Me.Text = "Contrato de Mantenimiento"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyContract.ResumeLayout(False)
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleOnlyGuarantee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleOnlyGuarantee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleExclusivity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleExclusivity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMmoAttachments.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMmoClauses.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtSupervicionExecution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtTechnicalSupervicion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtContractNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleContractType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGlvContractType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMmoDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleSourcerOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleViewSourcerOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlItemPlate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcItemByPlate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPlate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGpMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlytxtCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGleContractType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyDteInitialDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyDteEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyMmoDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGpDetaill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlySleSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtContractNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGleExclusivity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGpMoreInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtTechnicalSupervicion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtSupervicionExecution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGleOnlyGuarantee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGleSourcerOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGpClausesAnnexes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyMmoClauses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyMmoAttachments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgItemCatalog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFixedAssetPhysical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGridFunctinalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyContract As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyGpMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDTxtContractNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlytxtCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGleContractType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyDteInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyDteEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtContractNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyMmoDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDteInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDteEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyGpDetaill As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDGleContractType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend INDGlvContractType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDTxtTechnicalSupervicion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDMmoDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLyGleSourcerOrder As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtTechnicalSupervicion As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTxtSupervicionExecution As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyGpMoreInfo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyTxtSupervicionExecution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGpClausesAnnexes As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDMmoAttachments As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDMmoClauses As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLyMmoClauses As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyMmoAttachments As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleOnlyGuarantee As Presentation.Controls.CtrYesNo
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleExclusivity As Presentation.Controls.CtrYesNo
    Friend WithEvents CtrYesNo1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLyGleOnlyGuarantee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGleExclusivity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn210 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleSourcerOrder As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGleViewSourcerOrder As DevExpress.XtraGrid.Views.Grid.GridView
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
    Friend WithEvents ColSourceOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCodeContractType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNameContractType As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents INDSleSupplier As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlySleSupplier As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents ColTypeContracType As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents GridColumn129 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn130 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn131 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn132 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn133 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn134 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
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
    Friend WithEvents GridColumn148 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn161 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn153 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn162 As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents GridColumn1691 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn170 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn171 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn172 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn173 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn174 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn175 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn176 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn177 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn178 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn179 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn180 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn181 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn182 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn183 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn184 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn191 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn192 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn193 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn194 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn195 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn196 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn197 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn198 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn199 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn200 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn201 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSlItemPlate As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn910 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1010 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcItemByPlate As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvPlate As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn211 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcgItemCatalog As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciFixedAssetPhysical As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciGridFunctinalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn147 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn150 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn151 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn152 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn154 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn155 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn156 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn158 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn159 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn160 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn185 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn186 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn187 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn188 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn189 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn190 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn202 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn203 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn204 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn205 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn206 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn207 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn149 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn208 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn209 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn212 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn213 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn214 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn215 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn216 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn217 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn218 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn219 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn220 As DevExpress.XtraGrid.Columns.GridColumn
End Class
