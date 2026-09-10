#Region "Imports"
Imports Presentation.Controls
#End Region

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCMConfigure
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCMConfigure))
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit14 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit15 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit8 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit10 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit9 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit12 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit13 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDcncUnitDoseType = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlycBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbAddWorkingAreas = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbAddWarehouse = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbAddExternalAttentionCenter = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbAddAttentionCenter = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcWorkingAreas = New DevExpress.XtraGrid.GridControl()
        Me.INDGvWorkingAreas = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPccDeliveryTime = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcDeliveryTime = New DevExpress.XtraLayout.LayoutControl()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTmeFourthDeliveryTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDTmeThirdDeliveryTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDTmeSecondDeliveryTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDTmeFirstDeliveryTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDLbcDeliveryTime = New DevExpress.XtraEditors.LabelControl()
        Me.INDGleEveryTimeDeliver = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvEveryTimeDeliver = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgPccDeliveryTime = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciEveryTimeDeliver = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciLabelTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFirstDeliveryTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciSecondDeliveryTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciThirdDeliveryTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFourthDeliveryTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddDeliveryTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcDeliveryTime = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDeliveryTime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRipceEditDeliveryTime = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGcExternalCenter = New DevExpress.XtraGrid.GridControl()
        Me.INDviewCMExternalCareCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepCbeStatus = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDGcLineDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvLineDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbAddLine = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleProductionLine = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvProductionLine = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcCenterAttention = New DevExpress.XtraGrid.GridControl()
        Me.INDGvCenterAttention = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleCmType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleUsers = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewUserSearch = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcUsers = New DevExpress.XtraGrid.GridControl()
        Me.viewUsersGrid = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnAddUser = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcWareHouse = New DevExpress.XtraGrid.GridControl()
        Me.INDGvWareHouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn210 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn102 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtPrefix = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleDirectPr = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewUserSearchPr = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleDirectSp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewUserSearchSp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1101 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlycBaseUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.nombre = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCmType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPrefix = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgProductionLine = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciProductionLine = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciLineDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddLine = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCenter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCenterAttention = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddAttentionCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgExternalCenter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciGcExternalCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExternalAttentionCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgDeliveryTime = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDeliveryTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygAuthorization = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemUsers = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddUser = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygTechnicaldirector = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemTecPr = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCmType2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgWareHouse = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCenterAttention1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciWarehouses = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgWorkingAreas = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciWorkingAreas = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAddWorkingAreas = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridViewCenterAttention = New Presentation.Controls.IndigoGridView(Me.components)
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridViewLineDetail = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGridViewUsers = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        Me.IndigoGridView7 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridViewWareHouse = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridViewWorkingArea = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit81 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewUsers1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit41 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewLineDetail1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit21 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewCenterAttention1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit31 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewWareHouse1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewWorkingArea1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit51 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit211 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit811 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit82 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewLineDetail2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit22 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewWareHouse2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit112 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewWorkingArea2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit52 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewCenterAttention2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit32 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewUsers2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit42 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit311 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit411 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit1111 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit511 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDcncUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycBase.SuspendLayout()
        CType(Me.INDGcWorkingAreas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvWorkingAreas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccDeliveryTime.SuspendLayout()
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcDeliveryTime.SuspendLayout()
        CType(Me.INDTmeFourthDeliveryTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTmeThirdDeliveryTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTmeSecondDeliveryTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTmeFirstDeliveryTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleEveryTimeDeliver.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvEveryTimeDeliver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEveryTimeDeliver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLabelTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFirstDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSecondDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciThirdDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFourthDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRipceEditDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcExternalCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewCMExternalCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepCbeStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcLineDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvLineDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProductionLine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProductionLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcCenterAttention, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCenterAttention, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCmType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleUsers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewUserSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewUsersGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPrefix.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleDirectPr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewUserSearchPr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleDirectSp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewUserSearchSp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nombre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCmType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPrefix, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProductionLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProductionLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciLineDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCenterAttention, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddAttentionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgExternalCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGcExternalCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExternalAttentionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDeliveryTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAuthorization, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygTechnicaldirector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTecPr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCmType2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCenterAttention1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciWarehouses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgWorkingAreas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciWorkingAreas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAddWorkingAreas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewCenterAttention, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewLineDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewWareHouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewWorkingArea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit81, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewUsers1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewLineDetail1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewCenterAttention1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewWareHouse1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewWorkingArea1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit51, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit211, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit811, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit82, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewLineDetail2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewWareHouse2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit112, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewWorkingArea2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit52, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewCenterAttention2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewUsers2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit311, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit411, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit511, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDcncUnitDoseType)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1261, 615)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1261, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1261, 130)
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemPopupContainerEdit6
        '
        Me.RepositoryItemPopupContainerEdit6.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit6.Name = "RepositoryItemPopupContainerEdit6"
        '
        'RepositoryItemPopupContainerEdit4
        '
        Me.RepositoryItemPopupContainerEdit4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit4.Name = "RepositoryItemPopupContainerEdit4"
        '
        'RepositoryItemPopupContainerEdit14
        '
        Me.RepositoryItemPopupContainerEdit14.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit14.Name = "RepositoryItemPopupContainerEdit14"
        '
        'RepositoryItemPopupContainerEdit15
        '
        Me.RepositoryItemPopupContainerEdit15.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit15.Name = "RepositoryItemPopupContainerEdit15"
        '
        'RepositoryItemPopupContainerEdit8
        '
        Me.RepositoryItemPopupContainerEdit8.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit8.Name = "RepositoryItemPopupContainerEdit8"
        '
        'RepositoryItemPopupContainerEdit7
        '
        Me.RepositoryItemPopupContainerEdit7.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit7.Name = "RepositoryItemPopupContainerEdit7"
        '
        'RepositoryItemPopupContainerEdit10
        '
        Me.RepositoryItemPopupContainerEdit10.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit10.Name = "RepositoryItemPopupContainerEdit10"
        '
        'RepositoryItemPopupContainerEdit9
        '
        Me.RepositoryItemPopupContainerEdit9.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit9.Name = "RepositoryItemPopupContainerEdit9"
        '
        'RepositoryItemPopupContainerEdit12
        '
        Me.RepositoryItemPopupContainerEdit12.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit12.Name = "RepositoryItemPopupContainerEdit12"
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'RepositoryItemPopupContainerEdit13
        '
        Me.RepositoryItemPopupContainerEdit13.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit13.Name = "RepositoryItemPopupContainerEdit13"
        '
        'RepositoryItemPopupContainerEdit5
        '
        Me.RepositoryItemPopupContainerEdit5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit5.Name = "RepositoryItemPopupContainerEdit5"
        '
        'INDcncUnitDoseType
        '
        Me.INDcncUnitDoseType.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDcncUnitDoseType.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDcncUnitDoseType.LayoutControl = Me.INDlycBase
        Me.INDcncUnitDoseType.Location = New System.Drawing.Point(2, 7)
        Me.INDcncUnitDoseType.Margin = New System.Windows.Forms.Padding(0)
        Me.INDcncUnitDoseType.Name = "INDcncUnitDoseType"
        Me.INDcncUnitDoseType.Size = New System.Drawing.Size(200, 606)
        Me.INDcncUnitDoseType.TabIndex = 2
        Me.INDcncUnitDoseType.UseDisabledStatePainter = False
        '
        'INDlycBase
        '
        Me.INDlycBase.Controls.Add(Me.INDSbAddWorkingAreas)
        Me.INDlycBase.Controls.Add(Me.INDSbAddWarehouse)
        Me.INDlycBase.Controls.Add(Me.INDSbAddExternalAttentionCenter)
        Me.INDlycBase.Controls.Add(Me.INDSbAddAttentionCenter)
        Me.INDlycBase.Controls.Add(Me.INDGcWorkingAreas)
        Me.INDlycBase.Controls.Add(Me.INDPccDeliveryTime)
        Me.INDlycBase.Controls.Add(Me.INDGcDeliveryTime)
        Me.INDlycBase.Controls.Add(Me.INDGcExternalCenter)
        Me.INDlycBase.Controls.Add(Me.INDGcLineDetail)
        Me.INDlycBase.Controls.Add(Me.INDSbAddLine)
        Me.INDlycBase.Controls.Add(Me.INDSleProductionLine)
        Me.INDlycBase.Controls.Add(Me.INDGcCenterAttention)
        Me.INDlycBase.Controls.Add(Me.INDsleCmType)
        Me.INDlycBase.Controls.Add(Me.INDtxtName)
        Me.INDlycBase.Controls.Add(Me.INDbtnCode)
        Me.INDlycBase.Controls.Add(Me.INDsleUsers)
        Me.INDlycBase.Controls.Add(Me.INDgcUsers)
        Me.INDlycBase.Controls.Add(Me.INDbtnAddUser)
        Me.INDlycBase.Controls.Add(Me.INDGcWareHouse)
        Me.INDlycBase.Controls.Add(Me.INDtxtPrefix)
        Me.INDlycBase.Controls.Add(Me.INDsleDirectPr)
        Me.INDlycBase.Controls.Add(Me.INDsleDirectSp)
        Me.INDlycBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycBase.Location = New System.Drawing.Point(202, 7)
        Me.INDlycBase.Name = "INDlycBase"
        Me.INDlycBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(434, 310, 1201, 569)
        Me.INDlycBase.Root = Me.INDlycBaseUnitDoseType
        Me.INDlycBase.Size = New System.Drawing.Size(1057, 606)
        Me.INDlycBase.TabIndex = 3
        Me.INDlycBase.Text = "LayoutControl1"
        '
        'INDSbAddWorkingAreas
        '
        Me.INDSbAddWorkingAreas.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAddWorkingAreas.Appearance.Options.UseFont = True
        Me.INDSbAddWorkingAreas.Location = New System.Drawing.Point(945, 53)
        Me.INDSbAddWorkingAreas.Name = "INDSbAddWorkingAreas"
        Me.INDSbAddWorkingAreas.Size = New System.Drawing.Size(746, 28)
        Me.INDSbAddWorkingAreas.StyleController = Me.INDlycBase
        Me.INDSbAddWorkingAreas.TabIndex = 42
        Me.INDSbAddWorkingAreas.Text = "Agregar"
        '
        'INDSbAddWarehouse
        '
        Me.INDSbAddWarehouse.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAddWarehouse.Appearance.Options.UseFont = True
        Me.INDSbAddWarehouse.Location = New System.Drawing.Point(171, 53)
        Me.INDSbAddWarehouse.Name = "INDSbAddWarehouse"
        Me.INDSbAddWarehouse.Size = New System.Drawing.Size(746, 28)
        Me.INDSbAddWarehouse.StyleController = Me.INDlycBase
        Me.INDSbAddWarehouse.TabIndex = 41
        Me.INDSbAddWarehouse.Text = "Agregar"
        '
        'INDSbAddExternalAttentionCenter
        '
        Me.INDSbAddExternalAttentionCenter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAddExternalAttentionCenter.Appearance.Options.UseFont = True
        Me.INDSbAddExternalAttentionCenter.Location = New System.Drawing.Point(-2411, 53)
        Me.INDSbAddExternalAttentionCenter.Name = "INDSbAddExternalAttentionCenter"
        Me.INDSbAddExternalAttentionCenter.Size = New System.Drawing.Size(746, 28)
        Me.INDSbAddExternalAttentionCenter.StyleController = Me.INDlycBase
        Me.INDSbAddExternalAttentionCenter.TabIndex = 40
        Me.INDSbAddExternalAttentionCenter.Text = "Agregar"
        '
        'INDSbAddAttentionCenter
        '
        Me.INDSbAddAttentionCenter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAddAttentionCenter.Appearance.Options.UseFont = True
        Me.INDSbAddAttentionCenter.Location = New System.Drawing.Point(-3185, 53)
        Me.INDSbAddAttentionCenter.Name = "INDSbAddAttentionCenter"
        Me.INDSbAddAttentionCenter.Size = New System.Drawing.Size(746, 28)
        Me.INDSbAddAttentionCenter.StyleController = Me.INDlycBase
        Me.INDSbAddAttentionCenter.TabIndex = 39
        Me.INDSbAddAttentionCenter.Text = "Agregar"
        '
        'INDGcWorkingAreas
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcWorkingAreas, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcWorkingAreas, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcWorkingAreas, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcWorkingAreas, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcWorkingAreas, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcWorkingAreas, False)
        Me.INDGcWorkingAreas.Location = New System.Drawing.Point(945, 85)
        Me.INDGcWorkingAreas.MainView = Me.INDGvWorkingAreas
        Me.INDGcWorkingAreas.Name = "INDGcWorkingAreas"
        Me.INDGcWorkingAreas.Size = New System.Drawing.Size(746, 480)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcWorkingAreas, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcWorkingAreas.TabIndex = 38
        Me.INDGcWorkingAreas.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvWorkingAreas})
        '
        'INDGvWorkingAreas
        '
        Me.INDGvWorkingAreas.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvWorkingAreas.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvWorkingAreas.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvWorkingAreas.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvWorkingAreas.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWorkingAreas.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvWorkingAreas.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWorkingAreas.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvWorkingAreas.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvWorkingAreas.Appearance.Row.Options.UseFont = True
        Me.INDGvWorkingAreas.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvWorkingAreas.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvWorkingAreas.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCode, Me.INDColDescription, Me.INDColState})
        Me.INDGvWorkingAreas.GridControl = Me.INDGcWorkingAreas
        Me.INDGvWorkingAreas.Name = "INDGvWorkingAreas"
        Me.INDGvWorkingAreas.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvWorkingAreas.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvWorkingAreas.OptionsView.ShowAutoFilterRow = True
        Me.INDGvWorkingAreas.OptionsView.ShowDetailButtons = False
        Me.INDGvWorkingAreas.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.INDGvWorkingAreas, False)
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Código"
        Me.INDColCode.FieldName = "Code"
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 0
        Me.INDColCode.Width = 191
        '
        'INDColDescription
        '
        Me.INDColDescription.Caption = "Descripción"
        Me.INDColDescription.FieldName = "Description"
        Me.INDColDescription.Name = "INDColDescription"
        Me.INDColDescription.OptionsColumn.AllowEdit = False
        Me.INDColDescription.OptionsColumn.AllowFocus = False
        Me.INDColDescription.Visible = True
        Me.INDColDescription.VisibleIndex = 1
        Me.INDColDescription.Width = 542
        '
        'INDColState
        '
        Me.INDColState.Caption = "Estado"
        Me.INDColState.FieldName = "State"
        Me.INDColState.Name = "INDColState"
        Me.INDColState.OptionsColumn.AllowEdit = False
        Me.INDColState.OptionsColumn.AllowFocus = False
        Me.INDColState.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDColState.Visible = True
        Me.INDColState.VisibleIndex = 2
        Me.INDColState.Width = 229
        '
        'INDPccDeliveryTime
        '
        Me.INDPccDeliveryTime.Controls.Add(Me.INDLcDeliveryTime)
        Me.INDPccDeliveryTime.Location = New System.Drawing.Point(46, 521)
        Me.INDPccDeliveryTime.Name = "INDPccDeliveryTime"
        Me.INDPccDeliveryTime.Size = New System.Drawing.Size(221, 215)
        Me.INDPccDeliveryTime.TabIndex = 11
        '
        'INDLcDeliveryTime
        '
        Me.INDLcDeliveryTime.Controls.Add(Me.SimpleButton1)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDTmeFourthDeliveryTime)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDTmeThirdDeliveryTime)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDTmeSecondDeliveryTime)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDTmeFirstDeliveryTime)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDLbcDeliveryTime)
        Me.INDLcDeliveryTime.Controls.Add(Me.INDGleEveryTimeDeliver)
        Me.INDLcDeliveryTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcDeliveryTime.Location = New System.Drawing.Point(0, 0)
        Me.INDLcDeliveryTime.Name = "INDLcDeliveryTime"
        Me.INDLcDeliveryTime.Root = Me.INDLcgPccDeliveryTime
        Me.INDLcDeliveryTime.Size = New System.Drawing.Size(221, 215)
        Me.INDLcDeliveryTime.TabIndex = 0
        Me.INDLcDeliveryTime.Text = "LayoutControl1"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Location = New System.Drawing.Point(12, 174)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(196, 28)
        Me.SimpleButton1.StyleController = Me.INDLcDeliveryTime
        Me.SimpleButton1.TabIndex = 5
        Me.SimpleButton1.Text = "Agregar"
        '
        'INDTmeFourthDeliveryTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTmeFourthDeliveryTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTmeFourthDeliveryTime, False)
        Me.INDTmeFourthDeliveryTime.EditValue = New Date(2020, 1, 29, 0, 0, 0, 0)
        Me.INDTmeFourthDeliveryTime.EnterMoveNextControl = True
        Me.INDTmeFourthDeliveryTime.Location = New System.Drawing.Point(127, 142)
        Me.IndigoTextEdit1.SetMascara(Me.INDTmeFourthDeliveryTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTmeFourthDeliveryTime.Name = "INDTmeFourthDeliveryTime"
        Me.INDTmeFourthDeliveryTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTmeFourthDeliveryTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTmeFourthDeliveryTime.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTmeFourthDeliveryTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDTmeFourthDeliveryTime.Properties.Appearance.Options.UseFont = True
        Me.INDTmeFourthDeliveryTime.Properties.Appearance.Options.UseForeColor = True
        Me.INDTmeFourthDeliveryTime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTmeFourthDeliveryTime.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTmeFourthDeliveryTime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTmeFourthDeliveryTime.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTmeFourthDeliveryTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTmeFourthDeliveryTime.Properties.Mask.EditMask = "HH:mm"
        Me.INDTmeFourthDeliveryTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTmeFourthDeliveryTime.Size = New System.Drawing.Size(81, 28)
        Me.INDTmeFourthDeliveryTime.StyleController = Me.INDLcDeliveryTime
        Me.INDTmeFourthDeliveryTime.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTmeFourthDeliveryTime, 0)
        '
        'INDTmeThirdDeliveryTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTmeThirdDeliveryTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTmeThirdDeliveryTime, False)
        Me.INDTmeThirdDeliveryTime.EditValue = New Date(2020, 1, 29, 0, 0, 0, 0)
        Me.INDTmeThirdDeliveryTime.EnterMoveNextControl = True
        Me.INDTmeThirdDeliveryTime.Location = New System.Drawing.Point(127, 110)
        Me.IndigoTextEdit1.SetMascara(Me.INDTmeThirdDeliveryTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTmeThirdDeliveryTime.Name = "INDTmeThirdDeliveryTime"
        Me.INDTmeThirdDeliveryTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTmeThirdDeliveryTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTmeThirdDeliveryTime.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTmeThirdDeliveryTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDTmeThirdDeliveryTime.Properties.Appearance.Options.UseFont = True
        Me.INDTmeThirdDeliveryTime.Properties.Appearance.Options.UseForeColor = True
        Me.INDTmeThirdDeliveryTime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTmeThirdDeliveryTime.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTmeThirdDeliveryTime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTmeThirdDeliveryTime.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTmeThirdDeliveryTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTmeThirdDeliveryTime.Properties.Mask.EditMask = "HH:mm"
        Me.INDTmeThirdDeliveryTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTmeThirdDeliveryTime.Size = New System.Drawing.Size(81, 28)
        Me.INDTmeThirdDeliveryTime.StyleController = Me.INDLcDeliveryTime
        Me.INDTmeThirdDeliveryTime.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTmeThirdDeliveryTime, 0)
        '
        'INDTmeSecondDeliveryTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTmeSecondDeliveryTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTmeSecondDeliveryTime, False)
        Me.INDTmeSecondDeliveryTime.EditValue = New Date(2020, 1, 29, 0, 0, 0, 0)
        Me.INDTmeSecondDeliveryTime.EnterMoveNextControl = True
        Me.INDTmeSecondDeliveryTime.Location = New System.Drawing.Point(127, 78)
        Me.IndigoTextEdit1.SetMascara(Me.INDTmeSecondDeliveryTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTmeSecondDeliveryTime.Name = "INDTmeSecondDeliveryTime"
        Me.INDTmeSecondDeliveryTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTmeSecondDeliveryTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTmeSecondDeliveryTime.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTmeSecondDeliveryTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDTmeSecondDeliveryTime.Properties.Appearance.Options.UseFont = True
        Me.INDTmeSecondDeliveryTime.Properties.Appearance.Options.UseForeColor = True
        Me.INDTmeSecondDeliveryTime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTmeSecondDeliveryTime.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTmeSecondDeliveryTime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTmeSecondDeliveryTime.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTmeSecondDeliveryTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTmeSecondDeliveryTime.Properties.Mask.EditMask = "HH:mm"
        Me.INDTmeSecondDeliveryTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTmeSecondDeliveryTime.Size = New System.Drawing.Size(81, 28)
        Me.INDTmeSecondDeliveryTime.StyleController = Me.INDLcDeliveryTime
        Me.INDTmeSecondDeliveryTime.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTmeSecondDeliveryTime, 0)
        '
        'INDTmeFirstDeliveryTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTmeFirstDeliveryTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTmeFirstDeliveryTime, False)
        Me.INDTmeFirstDeliveryTime.EditValue = New Date(2020, 1, 29, 0, 0, 0, 0)
        Me.INDTmeFirstDeliveryTime.EnterMoveNextControl = True
        Me.INDTmeFirstDeliveryTime.Location = New System.Drawing.Point(127, 46)
        Me.IndigoTextEdit1.SetMascara(Me.INDTmeFirstDeliveryTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTmeFirstDeliveryTime.Name = "INDTmeFirstDeliveryTime"
        Me.INDTmeFirstDeliveryTime.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTmeFirstDeliveryTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTmeFirstDeliveryTime.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTmeFirstDeliveryTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDTmeFirstDeliveryTime.Properties.Appearance.Options.UseFont = True
        Me.INDTmeFirstDeliveryTime.Properties.Appearance.Options.UseForeColor = True
        Me.INDTmeFirstDeliveryTime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTmeFirstDeliveryTime.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDTmeFirstDeliveryTime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTmeFirstDeliveryTime.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTmeFirstDeliveryTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTmeFirstDeliveryTime.Properties.Mask.EditMask = "HH:mm"
        Me.INDTmeFirstDeliveryTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTmeFirstDeliveryTime.Size = New System.Drawing.Size(81, 28)
        Me.INDTmeFirstDeliveryTime.StyleController = Me.INDLcDeliveryTime
        Me.INDTmeFirstDeliveryTime.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTmeFirstDeliveryTime, 0)
        '
        'INDLbcDeliveryTime
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLbcDeliveryTime, True)
        Me.INDLbcDeliveryTime.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLbcDeliveryTime.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLbcDeliveryTime, False)
        Me.INDLbcDeliveryTime.Location = New System.Drawing.Point(182, 12)
        Me.INDLbcDeliveryTime.Name = "INDLbcDeliveryTime"
        Me.INDLbcDeliveryTime.Size = New System.Drawing.Size(26, 30)
        Me.INDLbcDeliveryTime.StyleController = Me.INDLcDeliveryTime
        Me.INDLbcDeliveryTime.TabIndex = 5
        Me.INDLbcDeliveryTime.Text = "horas"
        '
        'INDGleEveryTimeDeliver
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleEveryTimeDeliver, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleEveryTimeDeliver, False)
        Me.INDGleEveryTimeDeliver.EnterMoveNextControl = True
        Me.INDGleEveryTimeDeliver.Location = New System.Drawing.Point(127, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleEveryTimeDeliver, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleEveryTimeDeliver.Name = "INDGleEveryTimeDeliver"
        Me.INDGleEveryTimeDeliver.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleEveryTimeDeliver.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleEveryTimeDeliver.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDGleEveryTimeDeliver.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleEveryTimeDeliver.Properties.Appearance.Options.UseFont = True
        Me.INDGleEveryTimeDeliver.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleEveryTimeDeliver.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleEveryTimeDeliver.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDGleEveryTimeDeliver.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleEveryTimeDeliver.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDGleEveryTimeDeliver.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleEveryTimeDeliver.Properties.DisplayMember = "Item2"
        Me.INDGleEveryTimeDeliver.Properties.NullText = ""
        Me.INDGleEveryTimeDeliver.Properties.PopupFormSize = New System.Drawing.Size(100, 0)
        Me.INDGleEveryTimeDeliver.Properties.PopupView = Me.INDGvEveryTimeDeliver
        Me.INDGleEveryTimeDeliver.Properties.ValueMember = "Item1"
        Me.INDGleEveryTimeDeliver.Size = New System.Drawing.Size(51, 28)
        Me.INDGleEveryTimeDeliver.StyleController = Me.INDLcDeliveryTime
        Me.INDGleEveryTimeDeliver.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleEveryTimeDeliver, 0)
        '
        'INDGvEveryTimeDeliver
        '
        Me.INDGvEveryTimeDeliver.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvEveryTimeDeliver.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvEveryTimeDeliver.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvEveryTimeDeliver.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvEveryTimeDeliver.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvEveryTimeDeliver.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvEveryTimeDeliver.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvEveryTimeDeliver.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvEveryTimeDeliver.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvEveryTimeDeliver.Appearance.Row.Options.UseFont = True
        Me.INDGvEveryTimeDeliver.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn22})
        Me.INDGvEveryTimeDeliver.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvEveryTimeDeliver.Name = "INDGvEveryTimeDeliver"
        Me.INDGvEveryTimeDeliver.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvEveryTimeDeliver.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvEveryTimeDeliver.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvEveryTimeDeliver.OptionsView.ShowAutoFilterRow = True
        Me.INDGvEveryTimeDeliver.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.INDGvEveryTimeDeliver, False)
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Cada"
        Me.GridColumn22.FieldName = "Item2"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 0
        '
        'INDLcgPccDeliveryTime
        '
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPccDeliveryTime.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPccDeliveryTime.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPccDeliveryTime, False)
        Me.INDLcgPccDeliveryTime.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgPccDeliveryTime.GroupBordersVisible = False
        Me.INDLcgPccDeliveryTime.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciEveryTimeDeliver, Me.INDLciLabelTime, Me.INDLciFirstDeliveryTime, Me.INDLciSecondDeliveryTime, Me.INDLciThirdDeliveryTime, Me.INDLciFourthDeliveryTime, Me.INDLciAddDeliveryTime})
        Me.INDLcgPccDeliveryTime.Name = "INDLcgPccDeliveryTime"
        Me.INDLcgPccDeliveryTime.Size = New System.Drawing.Size(221, 215)
        Me.INDLcgPccDeliveryTime.TextVisible = False
        '
        'INDLciEveryTimeDeliver
        '
        Me.INDLciEveryTimeDeliver.Control = Me.INDGleEveryTimeDeliver
        Me.INDLciEveryTimeDeliver.Location = New System.Drawing.Point(0, 0)
        Me.INDLciEveryTimeDeliver.MaxSize = New System.Drawing.Size(170, 34)
        Me.INDLciEveryTimeDeliver.MinSize = New System.Drawing.Size(170, 34)
        Me.INDLciEveryTimeDeliver.Name = "INDLciEveryTimeDeliver"
        Me.INDLciEveryTimeDeliver.Size = New System.Drawing.Size(170, 34)
        Me.INDLciEveryTimeDeliver.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEveryTimeDeliver.Text = "Entrega cada"
        Me.INDLciEveryTimeDeliver.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEveryTimeDeliver.TextSize = New System.Drawing.Size(110, 13)
        Me.INDLciEveryTimeDeliver.TextToControlDistance = 5
        '
        'INDLciLabelTime
        '
        Me.INDLciLabelTime.Control = Me.INDLbcDeliveryTime
        Me.INDLciLabelTime.Location = New System.Drawing.Point(170, 0)
        Me.INDLciLabelTime.MaxSize = New System.Drawing.Size(30, 34)
        Me.INDLciLabelTime.MinSize = New System.Drawing.Size(30, 34)
        Me.INDLciLabelTime.Name = "INDLciLabelTime"
        Me.INDLciLabelTime.Size = New System.Drawing.Size(31, 34)
        Me.INDLciLabelTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciLabelTime.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciLabelTime.TextVisible = False
        '
        'INDLciFirstDeliveryTime
        '
        Me.INDLciFirstDeliveryTime.Control = Me.INDTmeFirstDeliveryTime
        Me.INDLciFirstDeliveryTime.Location = New System.Drawing.Point(0, 34)
        Me.INDLciFirstDeliveryTime.MaxSize = New System.Drawing.Size(200, 32)
        Me.INDLciFirstDeliveryTime.MinSize = New System.Drawing.Size(200, 32)
        Me.INDLciFirstDeliveryTime.Name = "INDLciFirstDeliveryTime"
        Me.INDLciFirstDeliveryTime.Size = New System.Drawing.Size(201, 32)
        Me.INDLciFirstDeliveryTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFirstDeliveryTime.Text = "Primera Entrega"
        Me.INDLciFirstDeliveryTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFirstDeliveryTime.TextSize = New System.Drawing.Size(110, 13)
        Me.INDLciFirstDeliveryTime.TextToControlDistance = 5
        '
        'INDLciSecondDeliveryTime
        '
        Me.INDLciSecondDeliveryTime.Control = Me.INDTmeSecondDeliveryTime
        Me.INDLciSecondDeliveryTime.Enabled = False
        Me.INDLciSecondDeliveryTime.Location = New System.Drawing.Point(0, 66)
        Me.INDLciSecondDeliveryTime.MaxSize = New System.Drawing.Size(200, 32)
        Me.INDLciSecondDeliveryTime.MinSize = New System.Drawing.Size(200, 32)
        Me.INDLciSecondDeliveryTime.Name = "INDLciSecondDeliveryTime"
        Me.INDLciSecondDeliveryTime.Size = New System.Drawing.Size(201, 32)
        Me.INDLciSecondDeliveryTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSecondDeliveryTime.Text = "Segunda Entrega"
        Me.INDLciSecondDeliveryTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSecondDeliveryTime.TextSize = New System.Drawing.Size(110, 13)
        Me.INDLciSecondDeliveryTime.TextToControlDistance = 5
        '
        'INDLciThirdDeliveryTime
        '
        Me.INDLciThirdDeliveryTime.Control = Me.INDTmeThirdDeliveryTime
        Me.INDLciThirdDeliveryTime.Enabled = False
        Me.INDLciThirdDeliveryTime.Location = New System.Drawing.Point(0, 98)
        Me.INDLciThirdDeliveryTime.MaxSize = New System.Drawing.Size(200, 32)
        Me.INDLciThirdDeliveryTime.MinSize = New System.Drawing.Size(200, 32)
        Me.INDLciThirdDeliveryTime.Name = "INDLciThirdDeliveryTime"
        Me.INDLciThirdDeliveryTime.Size = New System.Drawing.Size(201, 32)
        Me.INDLciThirdDeliveryTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciThirdDeliveryTime.Text = "Tercera Entrega"
        Me.INDLciThirdDeliveryTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciThirdDeliveryTime.TextSize = New System.Drawing.Size(110, 13)
        Me.INDLciThirdDeliveryTime.TextToControlDistance = 5
        '
        'INDLciFourthDeliveryTime
        '
        Me.INDLciFourthDeliveryTime.Control = Me.INDTmeFourthDeliveryTime
        Me.INDLciFourthDeliveryTime.Enabled = False
        Me.INDLciFourthDeliveryTime.Location = New System.Drawing.Point(0, 130)
        Me.INDLciFourthDeliveryTime.MaxSize = New System.Drawing.Size(200, 32)
        Me.INDLciFourthDeliveryTime.MinSize = New System.Drawing.Size(200, 32)
        Me.INDLciFourthDeliveryTime.Name = "INDLciFourthDeliveryTime"
        Me.INDLciFourthDeliveryTime.Size = New System.Drawing.Size(201, 32)
        Me.INDLciFourthDeliveryTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFourthDeliveryTime.Text = "Cuarta Entrega"
        Me.INDLciFourthDeliveryTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFourthDeliveryTime.TextSize = New System.Drawing.Size(110, 13)
        Me.INDLciFourthDeliveryTime.TextToControlDistance = 5
        '
        'INDLciAddDeliveryTime
        '
        Me.INDLciAddDeliveryTime.Control = Me.SimpleButton1
        Me.INDLciAddDeliveryTime.Location = New System.Drawing.Point(0, 162)
        Me.INDLciAddDeliveryTime.MaxSize = New System.Drawing.Size(200, 32)
        Me.INDLciAddDeliveryTime.MinSize = New System.Drawing.Size(200, 32)
        Me.INDLciAddDeliveryTime.Name = "INDLciAddDeliveryTime"
        Me.INDLciAddDeliveryTime.Size = New System.Drawing.Size(201, 33)
        Me.INDLciAddDeliveryTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAddDeliveryTime.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddDeliveryTime.TextVisible = False
        '
        'INDGcDeliveryTime
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDeliveryTime, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDeliveryTime, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDeliveryTime, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDeliveryTime, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDeliveryTime, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDeliveryTime, False)
        Me.INDGcDeliveryTime.Location = New System.Drawing.Point(-1637, 53)
        Me.INDGcDeliveryTime.MainView = Me.INDGvDeliveryTime
        Me.INDGcDeliveryTime.Name = "INDGcDeliveryTime"
        Me.INDGcDeliveryTime.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRipceEditDeliveryTime})
        Me.INDGcDeliveryTime.Size = New System.Drawing.Size(696, 512)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDeliveryTime, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcDeliveryTime.TabIndex = 10
        Me.INDGcDeliveryTime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDeliveryTime})
        '
        'INDGvDeliveryTime
        '
        Me.INDGvDeliveryTime.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDeliveryTime.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvDeliveryTime.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDeliveryTime.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDeliveryTime.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDeliveryTime.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDeliveryTime.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvDeliveryTime.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDeliveryTime.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvDeliveryTime.Appearance.Row.Options.UseFont = True
        Me.INDGvDeliveryTime.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvDeliveryTime.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDeliveryTime.ColumnPanelRowHeight = 40
        Me.INDGvDeliveryTime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18, Me.GridColumn19, Me.GridColumn20, Me.GridColumn21})
        Me.INDGvDeliveryTime.GridControl = Me.INDGcDeliveryTime
        Me.INDGvDeliveryTime.GroupCount = 1
        Me.INDGvDeliveryTime.Name = "INDGvDeliveryTime"
        Me.INDGvDeliveryTime.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvDeliveryTime.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDeliveryTime.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDeliveryTime.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDeliveryTime.OptionsView.ShowGroupPanel = False
        Me.INDGvDeliveryTime.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn14, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.INDGvDeliveryTime, False)
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Línea Producción"
        Me.GridColumn14.FieldName = "NameProductionLine"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.OptionsColumn.AllowMove = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 0
        Me.GridColumn14.Width = 141
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Unidad Funcional"
        Me.GridColumn15.FieldName = "NameFunctionalUnit"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.OptionsColumn.AllowMove = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 0
        Me.GridColumn15.Width = 176
        '
        'GridColumn16
        '
        Me.GridColumn16.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn16.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn16.Caption = "Entrega cada"
        Me.GridColumn16.FieldName = "EveryTimeDeliverName"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        Me.GridColumn16.OptionsColumn.AllowMove = False
        Me.GridColumn16.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 1
        Me.GridColumn16.Width = 59
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn17.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn17.Caption = "Primera Entrega"
        Me.GridColumn17.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn17.FieldName = "FirstDeliveryTime"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.OptionsColumn.AllowMove = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 2
        Me.GridColumn17.Width = 63
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn18.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn18.Caption = "Segunda Entrega"
        Me.GridColumn18.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn18.FieldName = "SecondDeliveryTime"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.OptionsColumn.AllowMove = False
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 3
        Me.GridColumn18.Width = 65
        '
        'GridColumn19
        '
        Me.GridColumn19.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn19.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn19.Caption = "Tercera Entrega"
        Me.GridColumn19.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn19.FieldName = "ThirdDeliveryTime"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        Me.GridColumn19.OptionsColumn.AllowMove = False
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 4
        Me.GridColumn19.Width = 59
        '
        'GridColumn20
        '
        Me.GridColumn20.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn20.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn20.Caption = "Cuarta Entrega"
        Me.GridColumn20.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn20.FieldName = "FourthDeliveryTime"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.OptionsColumn.AllowEdit = False
        Me.GridColumn20.OptionsColumn.AllowFocus = False
        Me.GridColumn20.OptionsColumn.AllowMove = False
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 5
        Me.GridColumn20.Width = 56
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Acción"
        Me.GridColumn21.ColumnEdit = Me.INDRipceEditDeliveryTime
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 6
        Me.GridColumn21.Width = 59
        '
        'INDRipceEditDeliveryTime
        '
        Me.INDRipceEditDeliveryTime.AutoHeight = False
        Me.INDRipceEditDeliveryTime.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Editar", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDRipceEditDeliveryTime.Name = "INDRipceEditDeliveryTime"
        Me.INDRipceEditDeliveryTime.PopupControl = Me.INDPccDeliveryTime
        '
        'INDGcExternalCenter
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcExternalCenter, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcExternalCenter, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcExternalCenter, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcExternalCenter, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcExternalCenter, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcExternalCenter, False)
        Me.INDGcExternalCenter.Location = New System.Drawing.Point(-2411, 85)
        Me.INDGcExternalCenter.MainView = Me.INDviewCMExternalCareCenter
        Me.INDGcExternalCenter.Name = "INDGcExternalCenter"
        Me.INDGcExternalCenter.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepCbeStatus})
        Me.INDGcExternalCenter.Size = New System.Drawing.Size(746, 480)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcExternalCenter, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcExternalCenter.TabIndex = 9
        Me.INDGcExternalCenter.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewCMExternalCareCenter})
        '
        'INDviewCMExternalCareCenter
        '
        Me.INDviewCMExternalCareCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewCMExternalCareCenter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewCMExternalCareCenter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewCMExternalCareCenter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewCMExternalCareCenter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewCMExternalCareCenter.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewCMExternalCareCenter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewCMExternalCareCenter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewCMExternalCareCenter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewCMExternalCareCenter.Appearance.Row.Options.UseFont = True
        Me.INDviewCMExternalCareCenter.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewCMExternalCareCenter.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewCMExternalCareCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn30})
        Me.INDviewCMExternalCareCenter.GridControl = Me.INDGcExternalCenter
        Me.INDviewCMExternalCareCenter.Name = "INDviewCMExternalCareCenter"
        Me.INDviewCMExternalCareCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewCMExternalCareCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewCMExternalCareCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDviewCMExternalCareCenter.OptionsView.ShowFooter = True
        Me.INDviewCMExternalCareCenter.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.INDviewCMExternalCareCenter, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Centro Atención Externo"
        Me.GridColumn11.FieldName = "ExternalCareCenterDescription"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.OptionsColumn.AllowMove = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Cliente"
        Me.GridColumn12.FieldName = "CustomerDescription"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.OptionsColumn.AllowMove = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Línea Producción"
        Me.GridColumn13.FieldName = "ProductionLineDescription"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.OptionsColumn.AllowMove = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 2
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Estado"
        Me.GridColumn30.ColumnEdit = Me.INDrepCbeStatus
        Me.GridColumn30.FieldName = "Status"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.OptionsColumn.AllowEdit = False
        Me.GridColumn30.OptionsColumn.AllowFocus = False
        Me.GridColumn30.Visible = True
        Me.GridColumn30.VisibleIndex = 3
        '
        'INDrepCbeStatus
        '
        Me.INDrepCbeStatus.AutoHeight = False
        Me.INDrepCbeStatus.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Activo", True, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inactivo", False, -1)})
        Me.INDrepCbeStatus.Name = "INDrepCbeStatus"
        '
        'INDGcLineDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcLineDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcLineDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcLineDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcLineDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcLineDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcLineDetail, False)
        Me.INDGcLineDetail.Location = New System.Drawing.Point(-3709, 85)
        Me.INDGcLineDetail.MainView = Me.INDGvLineDetail
        Me.INDGcLineDetail.Name = "INDGcLineDetail"
        Me.INDGcLineDetail.Size = New System.Drawing.Size(496, 480)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcLineDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcLineDetail.TabIndex = 5
        Me.INDGcLineDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvLineDetail})
        '
        'INDGvLineDetail
        '
        Me.INDGvLineDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvLineDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvLineDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvLineDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvLineDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvLineDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvLineDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvLineDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvLineDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvLineDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvLineDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvLineDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvLineDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.INDGvLineDetail.GridControl = Me.INDGcLineDetail
        Me.INDGvLineDetail.Name = "INDGvLineDetail"
        Me.INDGvLineDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvLineDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvLineDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvLineDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.INDGvLineDetail, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "ProductionLineCode"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.OptionsColumn.AllowMove = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 61
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "ProductionLineName"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.OptionsColumn.AllowMove = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 292
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Estado"
        Me.GridColumn9.FieldName = "StatusName"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.OptionsColumn.AllowMove = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 2
        Me.GridColumn9.Width = 118
        '
        'INDSbAddLine
        '
        Me.INDSbAddLine.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSbAddLine.Appearance.Options.UseFont = True
        Me.INDSbAddLine.Location = New System.Drawing.Point(-3309, 53)
        Me.INDSbAddLine.Name = "INDSbAddLine"
        Me.INDSbAddLine.Size = New System.Drawing.Size(96, 28)
        Me.INDSbAddLine.StyleController = Me.INDlycBase
        Me.INDSbAddLine.TabIndex = 4
        Me.INDSbAddLine.Text = "Agregar"
        '
        'INDSleProductionLine
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProductionLine, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProductionLine, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProductionLine, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleProductionLine, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProductionLine, False)
        Me.INDSleProductionLine.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProductionLine, False)
        Me.INDSleProductionLine.Location = New System.Drawing.Point(-3709, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProductionLine, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProductionLine.Name = "INDSleProductionLine"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProductionLine, False)
        Me.INDSleProductionLine.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleProductionLine.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProductionLine.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleProductionLine.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProductionLine.Properties.Appearance.Options.UseFont = True
        Me.INDSleProductionLine.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleProductionLine.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleProductionLine.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleProductionLine.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleProductionLine.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleProductionLine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleProductionLine.Properties.DisplayMember = "CodeName"
        Me.INDSleProductionLine.Properties.NullText = ""
        Me.INDSleProductionLine.Properties.PopupSizeable = False
        Me.INDSleProductionLine.Properties.PopupView = Me.INDGvProductionLine
        Me.INDSleProductionLine.Properties.ShowFooter = False
        Me.INDSleProductionLine.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProductionLine, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProductionLine, True)
        Me.INDSleProductionLine.Size = New System.Drawing.Size(396, 28)
        Me.INDSleProductionLine.StyleController = Me.INDlycBase
        Me.INDSleProductionLine.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProductionLine, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProductionLine, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProductionLine, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProductionLine, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProductionLine, False)
        '
        'INDGvProductionLine
        '
        Me.INDGvProductionLine.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProductionLine.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProductionLine.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProductionLine.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProductionLine.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProductionLine.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProductionLine.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProductionLine.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProductionLine.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProductionLine.Appearance.Row.Options.UseFont = True
        Me.INDGvProductionLine.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6})
        Me.INDGvProductionLine.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProductionLine.Name = "INDGvProductionLine"
        Me.INDGvProductionLine.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProductionLine.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProductionLine.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProductionLine.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProductionLine.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.INDGvProductionLine, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 145
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Nombre"
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 868
        '
        'INDGcCenterAttention
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCenterAttention, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCenterAttention, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCenterAttention, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCenterAttention, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCenterAttention, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCenterAttention, False)
        Me.INDGcCenterAttention.Location = New System.Drawing.Point(-3185, 85)
        Me.INDGcCenterAttention.MainView = Me.INDGvCenterAttention
        Me.INDGcCenterAttention.Name = "INDGcCenterAttention"
        Me.INDGcCenterAttention.Size = New System.Drawing.Size(746, 480)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCenterAttention, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcCenterAttention.TabIndex = 7
        Me.INDGcCenterAttention.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvCenterAttention})
        '
        'INDGvCenterAttention
        '
        Me.INDGvCenterAttention.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCenterAttention.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCenterAttention.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCenterAttention.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCenterAttention.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCenterAttention.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCenterAttention.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCenterAttention.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCenterAttention.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCenterAttention.Appearance.Row.Options.UseFont = True
        Me.INDGvCenterAttention.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvCenterAttention.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvCenterAttention.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn10})
        Me.INDGvCenterAttention.GridControl = Me.INDGcCenterAttention
        Me.INDGvCenterAttention.Name = "INDGvCenterAttention"
        Me.INDGvCenterAttention.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCenterAttention.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCenterAttention.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCenterAttention.OptionsView.ShowFooter = True
        Me.INDGvCenterAttention.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.INDGvCenterAttention, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Centro de atención"
        Me.GridColumn2.FieldName = "CodeNameCenterAttention"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowMove = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 240
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Ubicación"
        Me.GridColumn3.FieldName = "Ubicacion"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 226
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Línea de producción"
        Me.GridColumn4.FieldName = "CodeNameProductionLine"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.OptionsColumn.AllowMove = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 732
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Estado"
        Me.GridColumn10.FieldName = "StatusName"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.OptionsColumn.AllowMove = False
        Me.GridColumn10.Width = 694
        '
        'INDsleCmType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCmType, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCmType, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCmType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCmType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCmType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCmType, False)
        Me.INDsleCmType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCmType, False)
        Me.INDsleCmType.Location = New System.Drawing.Point(-4123, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCmType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCmType.Name = "INDsleCmType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCmType, False)
        Me.INDsleCmType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleCmType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCmType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCmType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCmType.Properties.Appearance.Options.UseFont = True
        Me.INDsleCmType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCmType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCmType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCmType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCmType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleCmType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCmType.Properties.DisplayMember = "Item2"
        Me.INDsleCmType.Properties.NullText = ""
        Me.INDsleCmType.Properties.PopupSizeable = False
        Me.INDsleCmType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleCmType.Properties.ShowFooter = False
        Me.INDsleCmType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCmType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCmType, True)
        Me.INDsleCmType.Size = New System.Drawing.Size(382, 28)
        Me.INDsleCmType.StyleController = Me.INDlycBase
        Me.INDsleCmType.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCmType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCmType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCmType, "{0} - {1}")
        Me.INDsleCmType.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCmType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCmType, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Clase"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(-4123, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtName.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtName.Properties.MaxLength = 40
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlycBase
        Me.INDtxtName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(-4123, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.Appearance.Options.UseForeColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseForeColor = True
        EditorButtonImageOptions2.Image = Global.Presentation.MixingStation.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.Mask.EditMask = "[0-9]+"
        Me.INDbtnCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlycBase
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDsleUsers
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleUsers, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleUsers, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleUsers, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.Location = New System.Drawing.Point(-793, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleUsers, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleUsers.Name = "INDsleUsers"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleUsers.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleUsers.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleUsers.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleUsers.Properties.Appearance.Options.UseFont = True
        Me.INDsleUsers.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleUsers.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleUsers.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleUsers.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleUsers.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleUsers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleUsers.Properties.DisplayMember = "FullName"
        Me.INDsleUsers.Properties.NullText = ""
        Me.INDsleUsers.Properties.PopupSizeable = False
        Me.INDsleUsers.Properties.PopupView = Me.viewUserSearch
        Me.INDsleUsers.Properties.ShowFooter = False
        Me.INDsleUsers.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleUsers, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleUsers, True)
        Me.INDsleUsers.Size = New System.Drawing.Size(426, 28)
        Me.INDsleUsers.StyleController = Me.INDlycBase
        Me.INDsleUsers.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleUsers, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleUsers, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleUsers, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleUsers, False)
        '
        'viewUserSearch
        '
        Me.viewUserSearch.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewUserSearch.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewUserSearch.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewUserSearch.Appearance.FocusedRow.Options.UseFont = True
        Me.viewUserSearch.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUserSearch.Appearance.GroupRow.Options.UseFont = True
        Me.viewUserSearch.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUserSearch.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewUserSearch.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewUserSearch.Appearance.Row.Options.UseFont = True
        Me.viewUserSearch.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn111, Me.GridColumn25, Me.GridColumn28})
        Me.viewUserSearch.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewUserSearch.Name = "viewUserSearch"
        Me.viewUserSearch.OptionsFind.AlwaysVisible = True
        Me.viewUserSearch.OptionsFind.FindFilterColumns = "UserCode"
        Me.viewUserSearch.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewUserSearch.OptionsView.EnableAppearanceEvenRow = True
        Me.viewUserSearch.OptionsView.EnableAppearanceOddRow = True
        Me.viewUserSearch.OptionsView.ShowAutoFilterRow = True
        Me.viewUserSearch.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.viewUserSearch, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.viewUserSearch, False)
        '
        'GridColumn111
        '
        Me.GridColumn111.Caption = "Código"
        Me.GridColumn111.FieldName = "UserCode"
        Me.GridColumn111.Name = "GridColumn111"
        Me.GridColumn111.OptionsColumn.AllowEdit = False
        Me.GridColumn111.OptionsColumn.AllowFocus = False
        Me.GridColumn111.Visible = True
        Me.GridColumn111.VisibleIndex = 0
        Me.GridColumn111.Width = 333
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Nombre"
        Me.GridColumn25.FieldName = "FullName"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.OptionsColumn.AllowFocus = False
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 1
        Me.GridColumn25.Width = 1049
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Cargo"
        Me.GridColumn28.FieldName = "PositionName"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 2
        '
        'INDgcUsers
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcUsers, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcUsers, Nothing)
        Me.INDgcUsers.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcUsers, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcUsers, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcUsers, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcUsers, False)
        Me.INDgcUsers.Location = New System.Drawing.Point(-913, 86)
        Me.INDgcUsers.MainView = Me.viewUsersGrid
        Me.INDgcUsers.Name = "INDgcUsers"
        Me.INDgcUsers.Size = New System.Drawing.Size(646, 479)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcUsers, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcUsers.TabIndex = 3
        Me.INDgcUsers.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewUsersGrid})
        '
        'viewUsersGrid
        '
        Me.viewUsersGrid.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewUsersGrid.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewUsersGrid.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewUsersGrid.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewUsersGrid.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewUsersGrid.Appearance.FocusedRow.Options.UseFont = True
        Me.viewUsersGrid.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUsersGrid.Appearance.GroupRow.Options.UseFont = True
        Me.viewUsersGrid.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUsersGrid.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewUsersGrid.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewUsersGrid.Appearance.Row.Options.UseFont = True
        Me.viewUsersGrid.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewUsersGrid.Appearance.ViewCaption.Options.UseFont = True
        Me.viewUsersGrid.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn31, Me.GridColumn41, Me.GridColumn27})
        Me.viewUsersGrid.GridControl = Me.INDgcUsers
        Me.viewUsersGrid.Name = "viewUsersGrid"
        Me.viewUsersGrid.OptionsCustomization.AllowGroup = False
        Me.viewUsersGrid.OptionsDetail.EnableMasterViewMode = False
        Me.viewUsersGrid.OptionsDetail.ShowDetailTabs = False
        Me.viewUsersGrid.OptionsView.EnableAppearanceEvenRow = True
        Me.viewUsersGrid.OptionsView.EnableAppearanceOddRow = True
        Me.viewUsersGrid.OptionsView.ShowAutoFilterRow = True
        Me.viewUsersGrid.OptionsView.ShowDetailButtons = False
        Me.viewUsersGrid.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Código "
        Me.GridColumn31.FieldName = "UserCode"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.OptionsColumn.AllowFocus = False
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 0
        Me.GridColumn31.Width = 149
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "Descripción"
        Me.GridColumn41.FieldName = "FullNameUser"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.OptionsColumn.AllowEdit = False
        Me.GridColumn41.OptionsColumn.AllowFocus = False
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 1
        Me.GridColumn41.Width = 267
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Cargo"
        Me.GridColumn27.FieldName = "PositionName"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 2
        Me.GridColumn27.Width = 205
        '
        'INDbtnAddUser
        '
        Me.INDbtnAddUser.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDbtnAddUser.Appearance.Options.UseFont = True
        Me.INDbtnAddUser.Location = New System.Drawing.Point(-363, 53)
        Me.INDbtnAddUser.Name = "INDbtnAddUser"
        Me.INDbtnAddUser.Size = New System.Drawing.Size(96, 29)
        Me.INDbtnAddUser.StyleController = Me.INDlycBase
        Me.INDbtnAddUser.TabIndex = 4
        Me.INDbtnAddUser.Text = "Agregar"
        '
        'INDGcWareHouse
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcWareHouse, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcWareHouse, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcWareHouse, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcWareHouse, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcWareHouse, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcWareHouse, False)
        Me.INDGcWareHouse.Location = New System.Drawing.Point(171, 85)
        Me.INDGcWareHouse.MainView = Me.INDGvWareHouse
        Me.INDGcWareHouse.Name = "INDGcWareHouse"
        Me.INDGcWareHouse.Size = New System.Drawing.Size(746, 480)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcWareHouse, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcWareHouse.TabIndex = 7
        Me.INDGcWareHouse.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvWareHouse})
        '
        'INDGvWareHouse
        '
        Me.INDGvWareHouse.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvWareHouse.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvWareHouse.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvWareHouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvWareHouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvWareHouse.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWareHouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvWareHouse.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWareHouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvWareHouse.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvWareHouse.Appearance.Row.Options.UseFont = True
        Me.INDGvWareHouse.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvWareHouse.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvWareHouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn210, Me.GridColumn33, Me.GridColumn42, Me.GridColumn102})
        Me.INDGvWareHouse.GridControl = Me.INDGcWareHouse
        Me.INDGvWareHouse.Name = "INDGvWareHouse"
        Me.INDGvWareHouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvWareHouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvWareHouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGvWareHouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.INDGvWareHouse, False)
        '
        'GridColumn210
        '
        Me.GridColumn210.Caption = "Código"
        Me.GridColumn210.FieldName = "WareHouseCode"
        Me.GridColumn210.Name = "GridColumn210"
        Me.GridColumn210.OptionsColumn.AllowEdit = False
        Me.GridColumn210.OptionsColumn.AllowFocus = False
        Me.GridColumn210.OptionsColumn.AllowMove = False
        Me.GridColumn210.Visible = True
        Me.GridColumn210.VisibleIndex = 0
        Me.GridColumn210.Width = 240
        '
        'GridColumn33
        '
        Me.GridColumn33.Caption = "Descripción"
        Me.GridColumn33.FieldName = "Description"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.OptionsColumn.AllowEdit = False
        Me.GridColumn33.OptionsColumn.AllowFocus = False
        Me.GridColumn33.OptionsColumn.AllowMove = False
        Me.GridColumn33.Visible = True
        Me.GridColumn33.VisibleIndex = 1
        Me.GridColumn33.Width = 226
        '
        'GridColumn42
        '
        Me.GridColumn42.Caption = "Tipo de almacén"
        Me.GridColumn42.FieldName = "WarehouseTypeName"
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.OptionsColumn.AllowEdit = False
        Me.GridColumn42.OptionsColumn.AllowFocus = False
        Me.GridColumn42.OptionsColumn.AllowMove = False
        Me.GridColumn42.Visible = True
        Me.GridColumn42.VisibleIndex = 2
        Me.GridColumn42.Width = 732
        '
        'GridColumn102
        '
        Me.GridColumn102.Caption = "Estado"
        Me.GridColumn102.FieldName = "StatusName"
        Me.GridColumn102.Name = "GridColumn102"
        Me.GridColumn102.OptionsColumn.AllowEdit = False
        Me.GridColumn102.OptionsColumn.AllowFocus = False
        Me.GridColumn102.OptionsColumn.AllowMove = False
        Me.GridColumn102.Visible = True
        Me.GridColumn102.VisibleIndex = 3
        Me.GridColumn102.Width = 694
        '
        'INDtxtPrefix
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPrefix, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPrefix, True)
        Me.INDtxtPrefix.EnterMoveNextControl = True
        Me.INDtxtPrefix.Location = New System.Drawing.Point(-4123, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPrefix, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPrefix.Name = "INDtxtPrefix"
        Me.INDtxtPrefix.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtPrefix.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPrefix.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtPrefix.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPrefix.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPrefix.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtPrefix.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtPrefix.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDtxtPrefix.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPrefix.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtPrefix.Properties.MaxLength = 4
        Me.INDtxtPrefix.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPrefix.StyleController = Me.INDlycBase
        Me.INDtxtPrefix.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPrefix, 0)
        Me.INDtxtPrefix.ToolTip = "Este Campo es Necesario"
        '
        'INDsleDirectPr
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleDirectPr, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleDirectPr, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleDirectPr, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleDirectPr, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleDirectPr, False)
        Me.INDsleDirectPr.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleDirectPr, False)
        Me.INDsleDirectPr.Location = New System.Drawing.Point(-239, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleDirectPr, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleDirectPr.Name = "INDsleDirectPr"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleDirectPr, False)
        Me.INDsleDirectPr.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleDirectPr.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleDirectPr.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleDirectPr.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleDirectPr.Properties.Appearance.Options.UseFont = True
        Me.INDsleDirectPr.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleDirectPr.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleDirectPr.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleDirectPr.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleDirectPr.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleDirectPr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleDirectPr.Properties.DisplayMember = "CodeNameUser"
        Me.INDsleDirectPr.Properties.NullText = ""
        Me.INDsleDirectPr.Properties.PopupSizeable = False
        Me.INDsleDirectPr.Properties.PopupView = Me.viewUserSearchPr
        Me.INDsleDirectPr.Properties.ShowFooter = False
        Me.INDsleDirectPr.Properties.ValueMember = "UserId"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleDirectPr, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleDirectPr, True)
        Me.INDsleDirectPr.Size = New System.Drawing.Size(382, 28)
        Me.INDsleDirectPr.StyleController = Me.INDlycBase
        Me.INDsleDirectPr.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleDirectPr, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleDirectPr, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleDirectPr, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleDirectPr, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleDirectPr, False)
        '
        'viewUserSearchPr
        '
        Me.viewUserSearchPr.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewUserSearchPr.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewUserSearchPr.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewUserSearchPr.Appearance.FocusedRow.Options.UseFont = True
        Me.viewUserSearchPr.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUserSearchPr.Appearance.GroupRow.Options.UseFont = True
        Me.viewUserSearchPr.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUserSearchPr.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewUserSearchPr.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewUserSearchPr.Appearance.Row.Options.UseFont = True
        Me.viewUserSearchPr.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn23, Me.GridColumn24})
        Me.viewUserSearchPr.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewUserSearchPr.Name = "viewUserSearchPr"
        Me.viewUserSearchPr.OptionsFind.AlwaysVisible = True
        Me.viewUserSearchPr.OptionsFind.FindFilterColumns = "UserCode"
        Me.viewUserSearchPr.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewUserSearchPr.OptionsView.EnableAppearanceEvenRow = True
        Me.viewUserSearchPr.OptionsView.EnableAppearanceOddRow = True
        Me.viewUserSearchPr.OptionsView.ShowAutoFilterRow = True
        Me.viewUserSearchPr.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.viewUserSearchPr, False)
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Código"
        Me.GridColumn23.FieldName = "UserCode"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.OptionsColumn.AllowFocus = False
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 0
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Nombre"
        Me.GridColumn24.FieldName = "FullNameUser"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsColumn.AllowFocus = False
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 1
        '
        'INDsleDirectSp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleDirectSp, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleDirectSp, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleDirectSp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleDirectSp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleDirectSp, False)
        Me.INDsleDirectSp.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleDirectSp, False)
        Me.INDsleDirectSp.Location = New System.Drawing.Point(-239, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleDirectSp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleDirectSp.Name = "INDsleDirectSp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleDirectSp, False)
        Me.INDsleDirectSp.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleDirectSp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleDirectSp.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleDirectSp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleDirectSp.Properties.Appearance.Options.UseFont = True
        Me.INDsleDirectSp.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleDirectSp.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleDirectSp.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleDirectSp.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleDirectSp.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDsleDirectSp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleDirectSp.Properties.DisplayMember = "CodeNameUser"
        Me.INDsleDirectSp.Properties.NullText = ""
        Me.INDsleDirectSp.Properties.PopupSizeable = False
        Me.INDsleDirectSp.Properties.PopupView = Me.viewUserSearchSp
        Me.INDsleDirectSp.Properties.ShowFooter = False
        Me.INDsleDirectSp.Properties.ValueMember = "UserId"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleDirectSp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleDirectSp, True)
        Me.INDsleDirectSp.Size = New System.Drawing.Size(382, 28)
        Me.INDsleDirectSp.StyleController = Me.INDlycBase
        Me.INDsleDirectSp.TabIndex = 2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleDirectSp, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleDirectSp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleDirectSp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleDirectSp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleDirectSp, False)
        '
        'viewUserSearchSp
        '
        Me.viewUserSearchSp.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewUserSearchSp.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewUserSearchSp.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewUserSearchSp.Appearance.FocusedRow.Options.UseFont = True
        Me.viewUserSearchSp.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUserSearchSp.Appearance.GroupRow.Options.UseFont = True
        Me.viewUserSearchSp.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewUserSearchSp.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewUserSearchSp.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewUserSearchSp.Appearance.Row.Options.UseFont = True
        Me.viewUserSearchSp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1101, Me.GridColumn26})
        Me.viewUserSearchSp.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewUserSearchSp.Name = "viewUserSearchSp"
        Me.viewUserSearchSp.OptionsFind.AlwaysVisible = True
        Me.viewUserSearchSp.OptionsFind.FindFilterColumns = "UserCode"
        Me.viewUserSearchSp.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewUserSearchSp.OptionsView.EnableAppearanceEvenRow = True
        Me.viewUserSearchSp.OptionsView.EnableAppearanceOddRow = True
        Me.viewUserSearchSp.OptionsView.ShowAutoFilterRow = True
        Me.viewUserSearchSp.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.viewUserSearchSp, False)
        '
        'GridColumn1101
        '
        Me.GridColumn1101.Caption = "Código"
        Me.GridColumn1101.FieldName = "UserCode"
        Me.GridColumn1101.Name = "GridColumn1101"
        Me.GridColumn1101.Visible = True
        Me.GridColumn1101.VisibleIndex = 0
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Nombre"
        Me.GridColumn26.FieldName = "FullNameUser"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 1
        '
        'INDlycBaseUnitDoseType
        '
        Me.INDlycBaseUnitDoseType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseUnitDoseType.AppearanceGroup.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseUnitDoseType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycBaseUnitDoseType, False)
        Me.INDlycBaseUnitDoseType.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycBaseUnitDoseType.GroupBordersVisible = False
        Me.INDlycBaseUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrUnitDoseType, Me.INDLcgProductionLine, Me.INDLcgCenter, Me.INDLcgExternalCenter, Me.INDLcgDeliveryTime, Me.INDlygAuthorization, Me.INDlygTechnicaldirector, Me.INDLcgWareHouse, Me.INDLcgWorkingAreas})
        Me.INDlycBaseUnitDoseType.Name = "Root"
        Me.INDlycBaseUnitDoseType.Size = New System.Drawing.Size(5862, 589)
        Me.INDlycBaseUnitDoseType.TextVisible = False
        '
        'INDlyGrUnitDoseType
        '
        Me.INDlyGrUnitDoseType.AllowHide = False
        Me.INDlyGrUnitDoseType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrUnitDoseType.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrUnitDoseType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrUnitDoseType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrUnitDoseType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrUnitDoseType, False)
        Me.INDlyGrUnitDoseType.CustomizationFormText = "Datos Principales"
        Me.INDlyGrUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.nombre, Me.INDLciCmType, Me.INDlyItemPrefix})
        Me.INDlyGrUnitDoseType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrUnitDoseType.Name = "INDlyGrUnitDoseType"
        Me.INDlyGrUnitDoseType.Size = New System.Drawing.Size(414, 569)
        Me.INDlyGrUnitDoseType.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'nombre
        '
        Me.nombre.AllowHide = False
        Me.nombre.Control = Me.INDtxtName
        Me.nombre.Location = New System.Drawing.Point(0, 60)
        Me.nombre.MaxSize = New System.Drawing.Size(390, 60)
        Me.nombre.MinSize = New System.Drawing.Size(390, 60)
        Me.nombre.Name = "nombre"
        Me.nombre.Size = New System.Drawing.Size(390, 60)
        Me.nombre.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.nombre.Text = "Nombre"
        Me.nombre.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.nombre.TextLocation = DevExpress.Utils.Locations.Top
        Me.nombre.TextSize = New System.Drawing.Size(135, 21)
        Me.nombre.TextToControlDistance = 5
        '
        'INDLciCmType
        '
        Me.INDLciCmType.AllowHide = False
        Me.INDLciCmType.Control = Me.INDsleCmType
        Me.INDLciCmType.Location = New System.Drawing.Point(0, 180)
        Me.INDLciCmType.MaxSize = New System.Drawing.Size(386, 60)
        Me.INDLciCmType.MinSize = New System.Drawing.Size(386, 60)
        Me.INDLciCmType.Name = "INDLciCmType"
        Me.INDLciCmType.Size = New System.Drawing.Size(390, 336)
        Me.INDLciCmType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCmType.Text = "Tipo de central de mezcla"
        Me.INDLciCmType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCmType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCmType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCmType.TextToControlDistance = 5
        '
        'INDlyItemPrefix
        '
        Me.INDlyItemPrefix.AllowHide = False
        Me.INDlyItemPrefix.Control = Me.INDtxtPrefix
        Me.INDlyItemPrefix.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemPrefix.CustomizationFormText = "Prefijo"
        Me.INDlyItemPrefix.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemPrefix.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPrefix.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPrefix.Name = "INDlyItemPrefix"
        Me.INDlyItemPrefix.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemPrefix.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPrefix.Text = "Prefijo"
        Me.INDlyItemPrefix.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPrefix.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPrefix.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPrefix.TextToControlDistance = 5
        '
        'INDLcgProductionLine
        '
        Me.INDLcgProductionLine.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProductionLine.AppearanceGroup.Options.UseFont = True
        Me.INDLcgProductionLine.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProductionLine.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgProductionLine.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProductionLine.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgProductionLine.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgProductionLine.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgProductionLine.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProductionLine.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgProductionLine.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProductionLine.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgProductionLine.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProductionLine.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgProductionLine, False)
        Me.INDLcgProductionLine.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciProductionLine, Me.INDLciLineDetail, Me.INDLciAddLine})
        Me.INDLcgProductionLine.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgProductionLine.Name = "INDLcgProductionLine"
        Me.INDLcgProductionLine.OptionsItemText.TextToControlDistance = 5
        Me.INDLcgProductionLine.Size = New System.Drawing.Size(524, 569)
        Me.INDLcgProductionLine.Text = "Líneas de producción"
        '
        'INDLciProductionLine
        '
        Me.INDLciProductionLine.Control = Me.INDSleProductionLine
        Me.INDLciProductionLine.Location = New System.Drawing.Point(0, 0)
        Me.INDLciProductionLine.MaxSize = New System.Drawing.Size(400, 32)
        Me.INDLciProductionLine.MinSize = New System.Drawing.Size(400, 32)
        Me.INDLciProductionLine.Name = "INDLciProductionLine"
        Me.INDLciProductionLine.Size = New System.Drawing.Size(400, 32)
        Me.INDLciProductionLine.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProductionLine.Text = "Línea de producción"
        Me.INDLciProductionLine.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciProductionLine.TextVisible = False
        '
        'INDLciLineDetail
        '
        Me.INDLciLineDetail.AllowHide = False
        Me.INDLciLineDetail.Control = Me.INDGcLineDetail
        Me.INDLciLineDetail.Location = New System.Drawing.Point(0, 32)
        Me.INDLciLineDetail.MinSize = New System.Drawing.Size(110, 30)
        Me.INDLciLineDetail.Name = "INDLciLineDetail"
        Me.INDLciLineDetail.Size = New System.Drawing.Size(500, 484)
        Me.INDLciLineDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciLineDetail.Text = "Líneas de produccion agregadas"
        Me.INDLciLineDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciLineDetail.TextVisible = False
        '
        'INDLciAddLine
        '
        Me.INDLciAddLine.AllowHide = False
        Me.INDLciAddLine.Control = Me.INDSbAddLine
        Me.INDLciAddLine.Location = New System.Drawing.Point(400, 0)
        Me.INDLciAddLine.MaxSize = New System.Drawing.Size(100, 32)
        Me.INDLciAddLine.MinSize = New System.Drawing.Size(100, 32)
        Me.INDLciAddLine.Name = "INDLciAddLine"
        Me.INDLciAddLine.Size = New System.Drawing.Size(100, 32)
        Me.INDLciAddLine.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAddLine.Text = "Agregar Línea"
        Me.INDLciAddLine.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddLine.TextVisible = False
        '
        'INDLcgCenter
        '
        Me.INDLcgCenter.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCenter.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCenter.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCenter.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCenter.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCenter.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCenter.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCenter.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCenter.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCenter.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCenter.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCenter.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCenter.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCenter.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCenter, False)
        Me.INDLcgCenter.CustomizationFormText = "Centros de Atención (Propios)"
        Me.INDLcgCenter.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.INDLcgCenter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCenterAttention, Me.INDLciAddAttentionCenter})
        Me.INDLcgCenter.Location = New System.Drawing.Point(938, 0)
        Me.INDLcgCenter.Name = "INDLcgCenter"
        Me.INDLcgCenter.Size = New System.Drawing.Size(774, 569)
        Me.INDLcgCenter.Text = "Centros de Atención (Propios)"
        '
        'INDLciCenterAttention
        '
        Me.INDLciCenterAttention.Control = Me.INDGcCenterAttention
        Me.INDLciCenterAttention.Location = New System.Drawing.Point(0, 32)
        Me.INDLciCenterAttention.MaxSize = New System.Drawing.Size(750, 0)
        Me.INDLciCenterAttention.MinSize = New System.Drawing.Size(750, 24)
        Me.INDLciCenterAttention.Name = "INDLciCenterAttention"
        Me.INDLciCenterAttention.Size = New System.Drawing.Size(750, 484)
        Me.INDLciCenterAttention.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCenterAttention.Text = "Centros de Atención"
        Me.INDLciCenterAttention.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciCenterAttention.TextVisible = False
        '
        'INDLciAddAttentionCenter
        '
        Me.INDLciAddAttentionCenter.Control = Me.INDSbAddAttentionCenter
        Me.INDLciAddAttentionCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAddAttentionCenter.MaxSize = New System.Drawing.Size(750, 32)
        Me.INDLciAddAttentionCenter.MinSize = New System.Drawing.Size(750, 32)
        Me.INDLciAddAttentionCenter.Name = "INDLciAddAttentionCenter"
        Me.INDLciAddAttentionCenter.Size = New System.Drawing.Size(750, 32)
        Me.INDLciAddAttentionCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAddAttentionCenter.Text = "Centros de atención"
        Me.INDLciAddAttentionCenter.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddAttentionCenter.TextVisible = False
        '
        'INDLcgExternalCenter
        '
        Me.INDLcgExternalCenter.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgExternalCenter.AppearanceGroup.Options.UseFont = True
        Me.INDLcgExternalCenter.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgExternalCenter.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgExternalCenter.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgExternalCenter.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgExternalCenter.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgExternalCenter.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgExternalCenter.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgExternalCenter.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgExternalCenter.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgExternalCenter.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgExternalCenter.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgExternalCenter.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgExternalCenter, False)
        Me.INDLcgExternalCenter.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.INDLcgExternalCenter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciGcExternalCenter, Me.INDLciExternalAttentionCenter})
        Me.INDLcgExternalCenter.Location = New System.Drawing.Point(1712, 0)
        Me.INDLcgExternalCenter.Name = "INDLcgExternalCenter"
        Me.INDLcgExternalCenter.Size = New System.Drawing.Size(774, 569)
        Me.INDLcgExternalCenter.Text = "Centros de Atención (Externos)"
        '
        'INDLciGcExternalCenter
        '
        Me.INDLciGcExternalCenter.Control = Me.INDGcExternalCenter
        Me.INDLciGcExternalCenter.Location = New System.Drawing.Point(0, 32)
        Me.INDLciGcExternalCenter.MaxSize = New System.Drawing.Size(750, 0)
        Me.INDLciGcExternalCenter.MinSize = New System.Drawing.Size(750, 24)
        Me.INDLciGcExternalCenter.Name = "INDLciGcExternalCenter"
        Me.INDLciGcExternalCenter.Size = New System.Drawing.Size(750, 484)
        Me.INDLciGcExternalCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGcExternalCenter.Text = "Centros de Atención Externos"
        Me.INDLciGcExternalCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciGcExternalCenter.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGcExternalCenter.TextToControlDistance = 0
        Me.INDLciGcExternalCenter.TextVisible = False
        '
        'INDLciExternalAttentionCenter
        '
        Me.INDLciExternalAttentionCenter.Control = Me.INDSbAddExternalAttentionCenter
        Me.INDLciExternalAttentionCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDLciExternalAttentionCenter.MaxSize = New System.Drawing.Size(750, 32)
        Me.INDLciExternalAttentionCenter.MinSize = New System.Drawing.Size(750, 32)
        Me.INDLciExternalAttentionCenter.Name = "INDLciExternalAttentionCenter"
        Me.INDLciExternalAttentionCenter.Size = New System.Drawing.Size(750, 32)
        Me.INDLciExternalAttentionCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExternalAttentionCenter.Text = "Agregar centros de atencion externos"
        Me.INDLciExternalAttentionCenter.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciExternalAttentionCenter.TextVisible = False
        '
        'INDLcgDeliveryTime
        '
        Me.INDLcgDeliveryTime.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDeliveryTime.AppearanceGroup.Options.UseFont = True
        Me.INDLcgDeliveryTime.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDeliveryTime.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgDeliveryTime.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDeliveryTime.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgDeliveryTime.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgDeliveryTime.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgDeliveryTime.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDeliveryTime.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgDeliveryTime.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDeliveryTime.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgDeliveryTime.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDeliveryTime.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgDeliveryTime, False)
        Me.INDLcgDeliveryTime.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDeliveryTime})
        Me.INDLcgDeliveryTime.Location = New System.Drawing.Point(2486, 0)
        Me.INDLcgDeliveryTime.Name = "INDLcgDeliveryTime"
        Me.INDLcgDeliveryTime.Size = New System.Drawing.Size(724, 569)
        Me.INDLcgDeliveryTime.Text = "Horarios de Entrega a Enfermería"
        '
        'INDLciDeliveryTime
        '
        Me.INDLciDeliveryTime.Control = Me.INDGcDeliveryTime
        Me.INDLciDeliveryTime.CustomizationFormText = "Horarios de Entrega a Enfermería"
        Me.INDLciDeliveryTime.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDeliveryTime.MaxSize = New System.Drawing.Size(700, 0)
        Me.INDLciDeliveryTime.MinSize = New System.Drawing.Size(700, 509)
        Me.INDLciDeliveryTime.Name = "INDLciDeliveryTime"
        Me.INDLciDeliveryTime.Size = New System.Drawing.Size(700, 516)
        Me.INDLciDeliveryTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDeliveryTime.Text = "Horarios de Entrega a Enfermería"
        Me.INDLciDeliveryTime.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciDeliveryTime.TextVisible = False
        '
        'INDlygAuthorization
        '
        Me.INDlygAuthorization.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAuthorization.AppearanceGroup.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAuthorization.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygAuthorization.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorization.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygAuthorization, False)
        Me.INDlygAuthorization.CustomizationFormText = "Autorización"
        Me.INDlygAuthorization.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemUsers, Me.LayoutControlItem2, Me.INDlyItemAddUser})
        Me.INDlygAuthorization.Location = New System.Drawing.Point(3210, 0)
        Me.INDlygAuthorization.Name = "INDlygAuthorization"
        Me.INDlygAuthorization.Size = New System.Drawing.Size(674, 569)
        Me.INDlygAuthorization.Text = "Autorización"
        '
        'INDlyItemUsers
        '
        Me.INDlyItemUsers.Control = Me.INDsleUsers
        Me.INDlyItemUsers.CustomizationFormText = "Usuarios"
        Me.INDlyItemUsers.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemUsers.MaxSize = New System.Drawing.Size(550, 33)
        Me.INDlyItemUsers.MinSize = New System.Drawing.Size(550, 33)
        Me.INDlyItemUsers.Name = "INDlyItemUsers"
        Me.INDlyItemUsers.Size = New System.Drawing.Size(550, 33)
        Me.INDlyItemUsers.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemUsers.Text = "Usuarios"
        Me.INDlyItemUsers.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemUsers.TextSize = New System.Drawing.Size(120, 21)
        Me.INDlyItemUsers.TextToControlDistance = 0
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDgcUsers
        Me.LayoutControlItem2.CustomizationFormText = "Usuarios"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 33)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(650, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(650, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(650, 483)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "LayoutControlItem1"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDlyItemAddUser
        '
        Me.INDlyItemAddUser.Control = Me.INDbtnAddUser
        Me.INDlyItemAddUser.CustomizationFormText = "Agregar"
        Me.INDlyItemAddUser.Location = New System.Drawing.Point(550, 0)
        Me.INDlyItemAddUser.MaxSize = New System.Drawing.Size(100, 33)
        Me.INDlyItemAddUser.MinSize = New System.Drawing.Size(100, 33)
        Me.INDlyItemAddUser.Name = "INDlyItemAddUser"
        Me.INDlyItemAddUser.Size = New System.Drawing.Size(100, 33)
        Me.INDlyItemAddUser.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddUser.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddUser.TextVisible = False
        '
        'INDlygTechnicaldirector
        '
        Me.INDlygTechnicaldirector.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygTechnicaldirector.AppearanceGroup.Options.UseFont = True
        Me.INDlygTechnicaldirector.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygTechnicaldirector.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygTechnicaldirector.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygTechnicaldirector.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygTechnicaldirector.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygTechnicaldirector.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygTechnicaldirector.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygTechnicaldirector.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygTechnicaldirector.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygTechnicaldirector.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygTechnicaldirector.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygTechnicaldirector.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygTechnicaldirector, False)
        Me.INDlygTechnicaldirector.CustomizationFormText = "Dirección Técnica"
        Me.INDlygTechnicaldirector.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemTecPr, Me.INDLciCmType2})
        Me.INDlygTechnicaldirector.Location = New System.Drawing.Point(3884, 0)
        Me.INDlygTechnicaldirector.Name = "INDlygTechnicaldirector"
        Me.INDlygTechnicaldirector.Size = New System.Drawing.Size(410, 569)
        Me.INDlygTechnicaldirector.Text = "Dirección Técnica"
        '
        'INDlyItemTecPr
        '
        Me.INDlyItemTecPr.Control = Me.INDsleDirectPr
        Me.INDlyItemTecPr.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemTecPr.CustomizationFormText = "Director técnico principal"
        Me.INDlyItemTecPr.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemTecPr.MaxSize = New System.Drawing.Size(386, 60)
        Me.INDlyItemTecPr.MinSize = New System.Drawing.Size(386, 60)
        Me.INDlyItemTecPr.Name = "INDlyItemTecPr"
        Me.INDlyItemTecPr.Size = New System.Drawing.Size(386, 60)
        Me.INDlyItemTecPr.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTecPr.Text = "Director técnico principal"
        Me.INDlyItemTecPr.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTecPr.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTecPr.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTecPr.TextToControlDistance = 5
        '
        'INDLciCmType2
        '
        Me.INDLciCmType2.Control = Me.INDsleDirectSp
        Me.INDLciCmType2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciCmType2.CustomizationFormText = "Director técnico suplente"
        Me.INDLciCmType2.Location = New System.Drawing.Point(0, 60)
        Me.INDLciCmType2.MaxSize = New System.Drawing.Size(386, 60)
        Me.INDLciCmType2.MinSize = New System.Drawing.Size(386, 60)
        Me.INDLciCmType2.Name = "INDLciCmType2"
        Me.INDLciCmType2.Size = New System.Drawing.Size(386, 456)
        Me.INDLciCmType2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCmType2.Text = "Director técnico suplente"
        Me.INDLciCmType2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCmType2.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCmType2.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCmType2.TextToControlDistance = 5
        '
        'INDLcgWareHouse
        '
        Me.INDLcgWareHouse.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWareHouse.AppearanceGroup.Options.UseFont = True
        Me.INDLcgWareHouse.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWareHouse.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgWareHouse.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWareHouse.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgWareHouse.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgWareHouse.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgWareHouse.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWareHouse.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgWareHouse.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWareHouse.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgWareHouse.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWareHouse.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgWareHouse, False)
        Me.INDLcgWareHouse.CustomizationFormText = "Almacenes"
        Me.INDLcgWareHouse.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCenterAttention1, Me.INDLciWarehouses})
        Me.INDLcgWareHouse.Location = New System.Drawing.Point(4294, 0)
        Me.INDLcgWareHouse.Name = "INDLcgWareHouse"
        Me.INDLcgWareHouse.Size = New System.Drawing.Size(774, 569)
        Me.INDLcgWareHouse.Text = "Almacenes"
        '
        'INDLciCenterAttention1
        '
        Me.INDLciCenterAttention1.Control = Me.INDGcWareHouse
        Me.INDLciCenterAttention1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciCenterAttention1.CustomizationFormText = "Centros de Atención"
        Me.INDLciCenterAttention1.Location = New System.Drawing.Point(0, 32)
        Me.INDLciCenterAttention1.MaxSize = New System.Drawing.Size(750, 0)
        Me.INDLciCenterAttention1.MinSize = New System.Drawing.Size(750, 24)
        Me.INDLciCenterAttention1.Name = "INDLciCenterAttention1"
        Me.INDLciCenterAttention1.Size = New System.Drawing.Size(750, 484)
        Me.INDLciCenterAttention1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCenterAttention1.Text = "Centros de Atención"
        Me.INDLciCenterAttention1.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciCenterAttention1.TextVisible = False
        '
        'INDLciWarehouses
        '
        Me.INDLciWarehouses.Control = Me.INDSbAddWarehouse
        Me.INDLciWarehouses.Location = New System.Drawing.Point(0, 0)
        Me.INDLciWarehouses.MaxSize = New System.Drawing.Size(750, 32)
        Me.INDLciWarehouses.MinSize = New System.Drawing.Size(750, 32)
        Me.INDLciWarehouses.Name = "INDLciWarehouses"
        Me.INDLciWarehouses.Size = New System.Drawing.Size(750, 32)
        Me.INDLciWarehouses.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciWarehouses.Text = "Almacenes"
        Me.INDLciWarehouses.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciWarehouses.TextVisible = False
        '
        'INDLcgWorkingAreas
        '
        Me.INDLcgWorkingAreas.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWorkingAreas.AppearanceGroup.Options.UseFont = True
        Me.INDLcgWorkingAreas.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgWorkingAreas.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgWorkingAreas.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWorkingAreas.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgWorkingAreas.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgWorkingAreas.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgWorkingAreas.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWorkingAreas.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgWorkingAreas.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWorkingAreas.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgWorkingAreas.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgWorkingAreas.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgWorkingAreas, False)
        Me.INDLcgWorkingAreas.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.INDLcgWorkingAreas.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciWorkingAreas, Me.INDLciAddWorkingAreas})
        Me.INDLcgWorkingAreas.Location = New System.Drawing.Point(5068, 0)
        Me.INDLcgWorkingAreas.Name = "INDLcgWorkingAreas"
        Me.INDLcgWorkingAreas.Size = New System.Drawing.Size(774, 569)
        Me.INDLcgWorkingAreas.Text = "Áreas de Trabajo"
        '
        'INDLciWorkingAreas
        '
        Me.INDLciWorkingAreas.Control = Me.INDGcWorkingAreas
        Me.INDLciWorkingAreas.CustomizationFormText = "Áreas de Trabajo"
        Me.INDLciWorkingAreas.Location = New System.Drawing.Point(0, 32)
        Me.INDLciWorkingAreas.MaxSize = New System.Drawing.Size(750, 0)
        Me.INDLciWorkingAreas.MinSize = New System.Drawing.Size(750, 24)
        Me.INDLciWorkingAreas.Name = "INDLciWorkingAreas"
        Me.INDLciWorkingAreas.Size = New System.Drawing.Size(750, 484)
        Me.INDLciWorkingAreas.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciWorkingAreas.Text = "Áreas de Trabajo"
        Me.INDLciWorkingAreas.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciWorkingAreas.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciWorkingAreas.TextToControlDistance = 0
        Me.INDLciWorkingAreas.TextVisible = False
        '
        'INDLciAddWorkingAreas
        '
        Me.INDLciAddWorkingAreas.Control = Me.INDSbAddWorkingAreas
        Me.INDLciAddWorkingAreas.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAddWorkingAreas.MaxSize = New System.Drawing.Size(750, 32)
        Me.INDLciAddWorkingAreas.MinSize = New System.Drawing.Size(750, 32)
        Me.INDLciAddWorkingAreas.Name = "INDLciAddWorkingAreas"
        Me.INDLciAddWorkingAreas.Size = New System.Drawing.Size(750, 32)
        Me.INDLciAddWorkingAreas.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAddWorkingAreas.Text = "Agregar areas de trabajo"
        Me.INDLciAddWorkingAreas.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAddWorkingAreas.TextVisible = False
        '
        'IndigoGridViewCenterAttention
        '
        Me.IndigoGridViewCenterAttention.RaiseMenuPopUp = True
        Me.IndigoGridViewCenterAttention.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
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
        Me.GridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView7.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewLineDetail1.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewCenterAttention.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewUsers.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewLineDetail.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewCenterAttention2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewWareHouse1.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewUsers2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewWorkingArea.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewCenterAttention1.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewLineDetail2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewWorkingArea1.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewWorkingArea2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewWareHouse2.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewUsers1.SetTemaIndigoMetro(Me.GridView1, False)
        Me.IndigoGridViewWareHouse.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'IndigoGridControl1
        '
        '
        'IndigoGridViewLineDetail
        '
        Me.IndigoGridViewLineDetail.RaiseMenuPopUp = True
        Me.IndigoGridViewLineDetail.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'IndigoGridViewUsers
        '
        Me.IndigoGridViewUsers.RaiseMenuPopUp = True
        Me.IndigoGridViewUsers.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit4
        '
        'IndigoGridView7
        '
        Me.IndigoGridView7.RaiseMenuPopUp = True
        Me.IndigoGridView7.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit8
        '
        'IndigoGridViewWareHouse
        '
        Me.IndigoGridViewWareHouse.RaiseMenuPopUp = True
        Me.IndigoGridViewWareHouse.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit11
        '
        'IndigoGridViewWorkingArea
        '
        Me.IndigoGridViewWorkingArea.RaiseMenuPopUp = True
        Me.IndigoGridViewWorkingArea.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit5
        '
        'RepositoryItemPopupContainerEdit81
        '
        Me.RepositoryItemPopupContainerEdit81.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit81.Name = "RepositoryItemPopupContainerEdit81"
        '
        'IndigoGridViewUsers1
        '
        Me.IndigoGridViewUsers1.RaiseMenuPopUp = True
        Me.IndigoGridViewUsers1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit41
        '
        'RepositoryItemPopupContainerEdit41
        '
        Me.RepositoryItemPopupContainerEdit41.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit41.Name = "RepositoryItemPopupContainerEdit41"
        '
        'IndigoGridViewLineDetail1
        '
        Me.IndigoGridViewLineDetail1.RaiseMenuPopUp = True
        Me.IndigoGridViewLineDetail1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit21
        '
        'RepositoryItemPopupContainerEdit21
        '
        Me.RepositoryItemPopupContainerEdit21.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit21.Name = "RepositoryItemPopupContainerEdit21"
        '
        'IndigoGridViewCenterAttention1
        '
        Me.IndigoGridViewCenterAttention1.RaiseMenuPopUp = True
        Me.IndigoGridViewCenterAttention1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit31
        '
        'RepositoryItemPopupContainerEdit31
        '
        Me.RepositoryItemPopupContainerEdit31.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit31.Name = "RepositoryItemPopupContainerEdit31"
        '
        'IndigoGridViewWareHouse1
        '
        Me.IndigoGridViewWareHouse1.RaiseMenuPopUp = True
        Me.IndigoGridViewWareHouse1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit111
        '
        'RepositoryItemPopupContainerEdit111
        '
        Me.RepositoryItemPopupContainerEdit111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit111.Name = "RepositoryItemPopupContainerEdit111"
        '
        'IndigoGridViewWorkingArea1
        '
        Me.IndigoGridViewWorkingArea1.RaiseMenuPopUp = True
        Me.IndigoGridViewWorkingArea1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit51
        '
        'RepositoryItemPopupContainerEdit51
        '
        Me.RepositoryItemPopupContainerEdit51.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit51.Name = "RepositoryItemPopupContainerEdit51"
        '
        'RepositoryItemPopupContainerEdit211
        '
        Me.RepositoryItemPopupContainerEdit211.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit211.Name = "RepositoryItemPopupContainerEdit211"
        '
        'RepositoryItemPopupContainerEdit811
        '
        Me.RepositoryItemPopupContainerEdit811.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit811.Name = "RepositoryItemPopupContainerEdit811"
        '
        'RepositoryItemPopupContainerEdit82
        '
        Me.RepositoryItemPopupContainerEdit82.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit82.Name = "RepositoryItemPopupContainerEdit82"
        '
        'IndigoGridViewLineDetail2
        '
        Me.IndigoGridViewLineDetail2.RaiseMenuPopUp = True
        Me.IndigoGridViewLineDetail2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit22
        '
        'RepositoryItemPopupContainerEdit22
        '
        Me.RepositoryItemPopupContainerEdit22.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit22.Name = "RepositoryItemPopupContainerEdit22"
        '
        'IndigoGridViewWareHouse2
        '
        Me.IndigoGridViewWareHouse2.RaiseMenuPopUp = True
        Me.IndigoGridViewWareHouse2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit112
        '
        'RepositoryItemPopupContainerEdit112
        '
        Me.RepositoryItemPopupContainerEdit112.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit112.Name = "RepositoryItemPopupContainerEdit112"
        '
        'IndigoGridViewWorkingArea2
        '
        Me.IndigoGridViewWorkingArea2.RaiseMenuPopUp = True
        Me.IndigoGridViewWorkingArea2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit52
        '
        'RepositoryItemPopupContainerEdit52
        '
        Me.RepositoryItemPopupContainerEdit52.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit52.Name = "RepositoryItemPopupContainerEdit52"
        '
        'IndigoGridViewCenterAttention2
        '
        Me.IndigoGridViewCenterAttention2.RaiseMenuPopUp = True
        Me.IndigoGridViewCenterAttention2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit32
        '
        'RepositoryItemPopupContainerEdit32
        '
        Me.RepositoryItemPopupContainerEdit32.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit32.Name = "RepositoryItemPopupContainerEdit32"
        '
        'IndigoGridViewUsers2
        '
        Me.IndigoGridViewUsers2.RaiseMenuPopUp = True
        Me.IndigoGridViewUsers2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit42
        '
        'RepositoryItemPopupContainerEdit42
        '
        Me.RepositoryItemPopupContainerEdit42.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit42.Name = "RepositoryItemPopupContainerEdit42"
        '
        'RepositoryItemPopupContainerEdit311
        '
        Me.RepositoryItemPopupContainerEdit311.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit311.Name = "RepositoryItemPopupContainerEdit311"
        '
        'RepositoryItemPopupContainerEdit411
        '
        Me.RepositoryItemPopupContainerEdit411.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit411.Name = "RepositoryItemPopupContainerEdit411"
        '
        'RepositoryItemPopupContainerEdit1111
        '
        Me.RepositoryItemPopupContainerEdit1111.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1111.Name = "RepositoryItemPopupContainerEdit1111"
        '
        'RepositoryItemPopupContainerEdit511
        '
        Me.RepositoryItemPopupContainerEdit511.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit511.Name = "RepositoryItemPopupContainerEdit511"
        '
        'FrmCMConfigure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1261, 750)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmCMConfigure.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmCMConfigure"
        Me.Opacity = 1.0R
        Me.Tag = "2063"
        Me.Text = "Central de Mezclas"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDcncUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycBase.ResumeLayout(False)
        CType(Me.INDGcWorkingAreas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvWorkingAreas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccDeliveryTime.ResumeLayout(False)
        CType(Me.INDLcDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcDeliveryTime.ResumeLayout(False)
        CType(Me.INDTmeFourthDeliveryTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTmeThirdDeliveryTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTmeSecondDeliveryTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTmeFirstDeliveryTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleEveryTimeDeliver.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvEveryTimeDeliver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPccDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEveryTimeDeliver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLabelTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFirstDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSecondDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciThirdDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFourthDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRipceEditDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcExternalCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewCMExternalCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepCbeStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcLineDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvLineDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProductionLine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProductionLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcCenterAttention, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCenterAttention, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCmType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleUsers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewUserSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewUsersGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPrefix.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleDirectPr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewUserSearchPr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleDirectSp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewUserSearchSp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nombre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCmType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPrefix, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProductionLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProductionLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciLineDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCenterAttention, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddAttentionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgExternalCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGcExternalCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExternalAttentionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDeliveryTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAuthorization, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygTechnicaldirector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTecPr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCmType2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCenterAttention1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciWarehouses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgWorkingAreas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciWorkingAreas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAddWorkingAreas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewCenterAttention, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewLineDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewWareHouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewWorkingArea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit81, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewUsers1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewLineDetail1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewCenterAttention1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewWareHouse1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewWorkingArea1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit51, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit211, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit811, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit82, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewLineDetail2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewWareHouse2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit112, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewWorkingArea2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit52, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewCenterAttention2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewUsers2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit311, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit411, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit511, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDcncUnitDoseType As CtrNavigationControlPanel
    Friend WithEvents INDlycBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycBaseUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyGrUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents nombre As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGridViewCenterAttention As IndigoGridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDLcgCenter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDsleCmType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciCmType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcCenterAttention As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvCenterAttention As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciCenterAttention As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcLineDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvLineDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSbAddLine As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSleProductionLine As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProductionLine As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgProductionLine As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciProductionLine As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciLineDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAddLine As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridViewLineDetail As IndigoGridView
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcExternalCenter As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewCMExternalCareCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciGcExternalCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgExternalCenter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcDeliveryTime As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDeliveryTime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgDeliveryTime As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDeliveryTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPccDeliveryTime As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcDeliveryTime As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDTmeFourthDeliveryTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDTmeThirdDeliveryTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDTmeSecondDeliveryTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDTmeFirstDeliveryTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDLbcDeliveryTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDGleEveryTimeDeliver As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvEveryTimeDeliver As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgPccDeliveryTime As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciEveryTimeDeliver As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciLabelTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFirstDeliveryTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciSecondDeliveryTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciThirdDeliveryTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFourthDeliveryTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAddDeliveryTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRipceEditDeliveryTime As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents INDsleUsers As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewUserSearch As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcUsers As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewUsersGrid As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbtnAddUser As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlygAuthorization As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemUsers As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAddUser As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridViewUsers As IndigoGridView
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents IndigoCheckEdit1 As IndigoCheckEdit
    Friend WithEvents IndigoGridView7 As IndigoGridView
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepCbeStatus As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoGridViewWareHouse As IndigoGridView
    Friend WithEvents INDGcWareHouse As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvWareHouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn210 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn102 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgWareHouse As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCenterAttention1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcWorkingAreas As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvWorkingAreas As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgWorkingAreas As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciWorkingAreas As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridViewWorkingArea As IndigoGridView
    Friend WithEvents INDtxtPrefix As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemPrefix As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit8 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit9 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit10 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit12 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit13 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit14 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit15 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSbAddAttentionCenter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciAddAttentionCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbAddWorkingAreas As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbAddWarehouse As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbAddExternalAttentionCenter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciExternalAttentionCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciWarehouses As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAddWorkingAreas As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygTechnicaldirector As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridViewLineDetail1 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit21 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit81 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridViewCenterAttention1 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit31 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridViewUsers1 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit41 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridViewWareHouse1 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridViewWorkingArea1 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit51 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleDirectPr As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewUserSearchPr As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemTecPr As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridViewCenterAttention2 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit32 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridViewUsers2 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit42 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridViewLineDetail2 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit22 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridViewWorkingArea2 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit52 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridViewWareHouse2 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit112 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleDirectSp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewUserSearchSp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1101 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciCmType2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit211 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit811 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit82 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit311 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit411 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1111 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit511 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
End Class
