Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportValuedInventory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportValuedInventory))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDCbeSubGroupHandlesBatchEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeSubGroupHandlesExpiryEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCncNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleAccountsZero = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn90 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleTypeReport = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit3View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGrcTypeReport = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleOrderBy = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGrcOrderBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleValorization = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGrcValorization = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleSubGroup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvSubGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSubGroup_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColExpiration = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvProduct = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProduct_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductSubGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleGroup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColGroup_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGroupCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGroupName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvWarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSourceWareHouse_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSourceWarehouseCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSourceWarehouseName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleCurrency = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn178 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn761 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn771 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbGenerateExcell = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilterRequired = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTypeReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciOrderBy = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValorization = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAmountsZero = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCurrency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgFilterOptional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLblProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblSubGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblSourceWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn87 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DocumentViewerBarManager1 = New DevExpress.XtraPrinting.Preview.DocumentViewerBarManager(Me.components)
        Me.PreviewBar1 = New DevExpress.XtraPrinting.Preview.PreviewBar()
        Me.PrintPreviewBarItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem5 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem6 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem7 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem8 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem9 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem10 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem11 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem12 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem13 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem14 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem15 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.ZoomBarEditItem1 = New DevExpress.XtraPrinting.Preview.ZoomBarEditItem()
        Me.PrintPreviewRepositoryItemComboBox1 = New DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox()
        Me.PrintPreviewBarItem16 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem17 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem18 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem19 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem20 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem21 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem22 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem23 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem24 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem25 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem26 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PreviewBar2 = New DevExpress.XtraPrinting.Preview.PreviewBar()
        Me.PrintPreviewStaticItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.ProgressBarEditItem1 = New DevExpress.XtraPrinting.Preview.ProgressBarEditItem()
        Me.RepositoryItemProgressBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.PrintPreviewBarItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.PrintPreviewStaticItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem()
        Me.ZoomTrackBarEditItem1 = New DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem()
        Me.RepositoryItemZoomTrackBar1 = New DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDDvDocumentViewer = New DevExpress.XtraPrinting.Preview.DocumentViewer()
        Me.PrintPreviewSubItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewSubItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewBarItem27 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem28 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.BarToolbarsListItem1 = New DevExpress.XtraBars.BarToolbarsListItem()
        Me.PrintPreviewSubItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewSubItem()
        Me.PrintPreviewBarCheckItem1 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem2 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem3 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem4 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem5 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem6 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem7 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem8 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem9 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem10 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem11 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem12 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem13 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem14 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem15 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem16 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.PrintPreviewBarCheckItem17 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.GridColumn82 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn81 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn80 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn79 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn78 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn77 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn76 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn72 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn70 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn67 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn66 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn65 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn64 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn63 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn62 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn60 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn59 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDGcExportExcell = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn83 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn84 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn86 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDLciAccountsZero = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPcDocumentViewer = New DevExpress.XtraEditors.PanelControl()
        Me.INDCnBack = New Presentation.Controls.CtrNavigation()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeSubGroupHandlesBatchEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeSubGroupHandlesExpiryEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDsleAccountsZero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAccountsZero.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit3View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleOrderBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleValorization.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSubGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSubGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValorization, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAmountsZero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblSubGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblSourceWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDDvDocumentViewer.SuspendLayout()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAccountsZero, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcDocumentViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcDocumentViewer.SuspendLayout()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCncNavigation)
        Me.INDPanelControlBase.Controls.Add(Me.INDPcDocumentViewer)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 7)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1348, 922)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1693, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDCbeSubGroupHandlesBatchEnd
        '
        Me.INDCbeSubGroupHandlesBatchEnd.AutoHeight = False
        Me.INDCbeSubGroupHandlesBatchEnd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeSubGroupHandlesBatchEnd.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Si", True, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No", False, -1)})
        Me.INDCbeSubGroupHandlesBatchEnd.Name = "INDCbeSubGroupHandlesBatchEnd"
        '
        'INDCbeSubGroupHandlesExpiryEnd
        '
        Me.INDCbeSubGroupHandlesExpiryEnd.AutoHeight = False
        Me.INDCbeSubGroupHandlesExpiryEnd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeSubGroupHandlesExpiryEnd.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Si", True, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No", False, -1)})
        Me.INDCbeSubGroupHandlesExpiryEnd.Name = "INDCbeSubGroupHandlesExpiryEnd"
        '
        'INDCncNavigation
        '
        Me.INDCncNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCncNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCncNavigation.LayoutControl = Me.INDLcBase
        Me.INDCncNavigation.Location = New System.Drawing.Point(2, 9)
        Me.INDCncNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCncNavigation.Name = "INDCncNavigation"
        Me.INDCncNavigation.Size = New System.Drawing.Size(200, 911)
        Me.INDCncNavigation.TabIndex = 0
        Me.INDCncNavigation.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDsleAccountsZero)
        Me.INDLcBase.Controls.Add(Me.INDGleTypeReport)
        Me.INDLcBase.Controls.Add(Me.INDGleOrderBy)
        Me.INDLcBase.Controls.Add(Me.INDGleValorization)
        Me.INDLcBase.Controls.Add(Me.INDSleSubGroup)
        Me.INDLcBase.Controls.Add(Me.INDSleProduct)
        Me.INDLcBase.Controls.Add(Me.INDSleGroup)
        Me.INDLcBase.Controls.Add(Me.INDSleWarehouse)
        Me.INDLcBase.Controls.Add(Me.INDsleCurrency)
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateReport)
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateExcell)
        Me.INDLcBase.Controls.Add(Me.INDGcExportExcell)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 9)
        Me.INDLcBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(1144, 911)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDsleAccountsZero
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDsleAccountsZero, False)
        Me.INDsleAccountsZero.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDsleAccountsZero, False)
        Me.INDsleAccountsZero.Location = New System.Drawing.Point(24, 233)
        Me.INDsleAccountsZero.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDsleAccountsZero.Name = "INDsleAccountsZero"
        Me.INDsleAccountsZero.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleAccountsZero.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleAccountsZero.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleAccountsZero.Properties.Appearance.Options.UseFont = True
        Me.INDsleAccountsZero.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDsleAccountsZero.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDsleAccountsZero.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleAccountsZero.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleAccountsZero.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleAccountsZero.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleAccountsZero.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleAccountsZero.Properties.DataSource = CType(resources.GetObject("INDsleAccountsZero.Properties.DataSource"), Object)
        Me.INDsleAccountsZero.Properties.DisplayMember = "Item2"
        Me.INDsleAccountsZero.Properties.ImmediatePopup = True
        Me.INDsleAccountsZero.Properties.NullText = ""
        Me.INDsleAccountsZero.Properties.PopupView = Me.CtrYesNo2View
        Me.INDsleAccountsZero.Properties.ValueMember = "Item1"
        Me.INDsleAccountsZero.Size = New System.Drawing.Size(416, 34)
        Me.INDsleAccountsZero.StyleController = Me.INDLcBase
        ToolTipItem1.Text = "Obligación única en grupo CxP."
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.INDsleAccountsZero.SuperTip = SuperToolTip1
        Me.INDsleAccountsZero.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDsleAccountsZero, Nothing)
        '
        'CtrYesNo2View
        '
        Me.CtrYesNo2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CtrYesNo2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CtrYesNo2View.Appearance.FocusedRow.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.CtrYesNo2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo2View.Appearance.GroupRow.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.CtrYesNo2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.CtrYesNo2View.Appearance.Row.Options.UseFont = True
        Me.CtrYesNo2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn90})
        Me.CtrYesNo2View.DetailHeight = 431
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
        'GridColumn88
        '
        Me.GridColumn88.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn88.Caption = "Selección"
        Me.GridColumn88.FieldName = "Item2"
        Me.GridColumn88.Name = "GridColumn88"
        '
        'GridColumn90
        '
        Me.GridColumn90.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn90.Caption = "Selección"
        Me.GridColumn90.FieldName = "Item2"
        Me.GridColumn90.Name = "GridColumn90"
        Me.GridColumn90.Visible = True
        Me.GridColumn90.VisibleIndex = 0
        '
        'INDGleTypeReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.Location = New System.Drawing.Point(24, 307)
        Me.INDGleTypeReport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGleTypeReport.Name = "INDGleTypeReport"
        Me.INDGleTypeReport.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleTypeReport.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTypeReport.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleTypeReport.Properties.Appearance.Options.UseFont = True
        Me.INDGleTypeReport.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeReport.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeReport.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleTypeReport.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleTypeReport.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleTypeReport.Properties.DisplayMember = "Item2"
        Me.INDGleTypeReport.Properties.ImmediatePopup = True
        Me.INDGleTypeReport.Properties.NullText = ""
        Me.INDGleTypeReport.Properties.PopupView = Me.GridLookUpEdit3View
        Me.INDGleTypeReport.Properties.ValueMember = "Item1"
        Me.INDGleTypeReport.Size = New System.Drawing.Size(416, 34)
        Me.INDGleTypeReport.StyleController = Me.INDLcBase
        Me.INDGleTypeReport.TabIndex = 4
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleTypeReport, Nothing)
        '
        'GridLookUpEdit3View
        '
        Me.GridLookUpEdit3View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit3View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit3View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit3View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit3View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit3View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit3View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit3View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit3View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit3View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit3View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit3View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGrcTypeReport})
        Me.GridLookUpEdit3View.DetailHeight = 431
        Me.GridLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit3View.Name = "GridLookUpEdit3View"
        Me.GridLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit3View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit3View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit3View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit3View, False)
        '
        'INDGrcTypeReport
        '
        Me.INDGrcTypeReport.Caption = "Tipo Reporte"
        Me.INDGrcTypeReport.FieldName = "Item2"
        Me.INDGrcTypeReport.MinWidth = 23
        Me.INDGrcTypeReport.Name = "INDGrcTypeReport"
        Me.INDGrcTypeReport.Visible = True
        Me.INDGrcTypeReport.VisibleIndex = 0
        Me.INDGrcTypeReport.Width = 87
        '
        'INDGleOrderBy
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleOrderBy, False)
        Me.INDGleOrderBy.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleOrderBy, False)
        Me.INDGleOrderBy.Location = New System.Drawing.Point(24, 159)
        Me.INDGleOrderBy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGleOrderBy.Name = "INDGleOrderBy"
        Me.INDGleOrderBy.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleOrderBy.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleOrderBy.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleOrderBy.Properties.Appearance.Options.UseFont = True
        Me.INDGleOrderBy.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleOrderBy.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleOrderBy.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleOrderBy.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleOrderBy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleOrderBy.Properties.DisplayMember = "Item2"
        Me.INDGleOrderBy.Properties.ImmediatePopup = True
        Me.INDGleOrderBy.Properties.NullText = ""
        Me.INDGleOrderBy.Properties.PopupView = Me.GridLookUpEdit2View
        Me.INDGleOrderBy.Properties.ValueMember = "Item1"
        Me.INDGleOrderBy.Size = New System.Drawing.Size(416, 34)
        Me.INDGleOrderBy.StyleController = Me.INDLcBase
        Me.INDGleOrderBy.TabIndex = 2
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleOrderBy, Nothing)
        '
        'GridLookUpEdit2View
        '
        Me.GridLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGrcOrderBy})
        Me.GridLookUpEdit2View.DetailHeight = 431
        Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
        Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit2View, False)
        '
        'INDGrcOrderBy
        '
        Me.INDGrcOrderBy.Caption = "Ordenar Por"
        Me.INDGrcOrderBy.FieldName = "Item2"
        Me.INDGrcOrderBy.MinWidth = 23
        Me.INDGrcOrderBy.Name = "INDGrcOrderBy"
        Me.INDGrcOrderBy.Visible = True
        Me.INDGrcOrderBy.VisibleIndex = 0
        Me.INDGrcOrderBy.Width = 87
        '
        'INDGleValorization
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleValorization, False)
        Me.INDGleValorization.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleValorization, False)
        Me.INDGleValorization.Location = New System.Drawing.Point(24, 85)
        Me.INDGleValorization.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGleValorization.Name = "INDGleValorization"
        Me.INDGleValorization.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleValorization.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleValorization.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleValorization.Properties.Appearance.Options.UseFont = True
        Me.INDGleValorization.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleValorization.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleValorization.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleValorization.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleValorization.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleValorization.Properties.DisplayMember = "Item2"
        Me.INDGleValorization.Properties.ImmediatePopup = True
        Me.INDGleValorization.Properties.NullText = ""
        Me.INDGleValorization.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleValorization.Properties.ValueMember = "Item1"
        Me.INDGleValorization.Size = New System.Drawing.Size(416, 34)
        Me.INDGleValorization.StyleController = Me.INDLcBase
        Me.INDGleValorization.TabIndex = 1
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleValorization, Nothing)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGrcValorization})
        Me.GridLookUpEdit1View.DetailHeight = 431
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDGrcValorization
        '
        Me.INDGrcValorization.Caption = "Valorización"
        Me.INDGrcValorization.FieldName = "Item2"
        Me.INDGrcValorization.MinWidth = 23
        Me.INDGrcValorization.Name = "INDGrcValorization"
        Me.INDGrcValorization.Visible = True
        Me.INDGrcValorization.VisibleIndex = 0
        Me.INDGrcValorization.Width = 87
        '
        'INDSleSubGroup
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleSubGroup, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleSubGroup, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleSubGroup, False)
        Me.INDSleSubGroup.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleSubGroup, False)
        Me.INDSleSubGroup.Location = New System.Drawing.Point(503, 307)
        Me.INDSleSubGroup.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleSubGroup.Name = "INDSleSubGroup"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleSubGroup, False)
        Me.INDSleSubGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleSubGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleSubGroup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleSubGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleSubGroup.Properties.Appearance.Options.UseFont = True
        Me.INDSleSubGroup.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleSubGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSubGroup.Properties.NullText = ""
        Me.INDSleSubGroup.Properties.PopupSizeable = False
        Me.INDSleSubGroup.Properties.PopupView = Me.INDGvSubGroup
        Me.INDSleSubGroup.Properties.ShowClearButton = False
        Me.INDSleSubGroup.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleSubGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleSubGroup, True)
        Me.INDSleSubGroup.Size = New System.Drawing.Size(435, 34)
        Me.INDSleSubGroup.StyleController = Me.INDLcBase
        Me.INDSleSubGroup.TabIndex = 28
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleSubGroup, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleSubGroup, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleSubGroup, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleSubGroup, False)
        '
        'INDGvSubGroup
        '
        Me.INDGvSubGroup.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSubGroup.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSubGroup.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSubGroup.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSubGroup.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSubGroup.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSubGroup.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSubGroup.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSubGroup.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSubGroup.Appearance.Row.Options.UseFont = True
        Me.INDGvSubGroup.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSubGroup_UnboundSelection, Me.INDColCode, Me.INDColName, Me.INDColLote, Me.INDColExpiration})
        Me.INDGvSubGroup.DetailHeight = 431
        Me.INDGvSubGroup.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvSubGroup.Name = "INDGvSubGroup"
        Me.INDGvSubGroup.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvSubGroup.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvSubGroup.OptionsSelection.MultiSelect = True
        Me.INDGvSubGroup.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSubGroup.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSubGroup.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSubGroup.OptionsView.ShowDetailButtons = False
        Me.INDGvSubGroup.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSubGroup, False)
        '
        'INDColSubGroup_UnboundSelection
        '
        Me.INDColSubGroup_UnboundSelection.Caption = " "
        Me.INDColSubGroup_UnboundSelection.FieldName = "INDColSubGroup_UnboundSelection"
        Me.INDColSubGroup_UnboundSelection.MinWidth = 23
        Me.INDColSubGroup_UnboundSelection.Name = "INDColSubGroup_UnboundSelection"
        Me.INDColSubGroup_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColSubGroup_UnboundSelection.Visible = True
        Me.INDColSubGroup_UnboundSelection.VisibleIndex = 0
        Me.INDColSubGroup_UnboundSelection.Width = 23
        '
        'INDColCode
        '
        Me.INDColCode.Caption = "Código"
        Me.INDColCode.FieldName = "Code"
        Me.INDColCode.MinWidth = 23
        Me.INDColCode.Name = "INDColCode"
        Me.INDColCode.OptionsColumn.AllowEdit = False
        Me.INDColCode.OptionsColumn.AllowFocus = False
        Me.INDColCode.Visible = True
        Me.INDColCode.VisibleIndex = 1
        Me.INDColCode.Width = 87
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "Name"
        Me.INDColName.MinWidth = 23
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.OptionsColumn.AllowFocus = False
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 2
        Me.INDColName.Width = 87
        '
        'INDColLote
        '
        Me.INDColLote.Caption = "Maneja Lote"
        Me.INDColLote.FieldName = "HandlesBatchType"
        Me.INDColLote.MinWidth = 23
        Me.INDColLote.Name = "INDColLote"
        Me.INDColLote.OptionsColumn.AllowEdit = False
        Me.INDColLote.OptionsColumn.AllowFocus = False
        Me.INDColLote.Visible = True
        Me.INDColLote.VisibleIndex = 3
        Me.INDColLote.Width = 87
        '
        'INDColExpiration
        '
        Me.INDColExpiration.Caption = "Maneja Vencimiento"
        Me.INDColExpiration.FieldName = "HandlesExpiryType"
        Me.INDColExpiration.MinWidth = 23
        Me.INDColExpiration.Name = "INDColExpiration"
        Me.INDColExpiration.OptionsColumn.AllowEdit = False
        Me.INDColExpiration.OptionsColumn.AllowFocus = False
        Me.INDColExpiration.Visible = True
        Me.INDColExpiration.VisibleIndex = 4
        Me.INDColExpiration.Width = 87
        '
        'INDSleProduct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Location = New System.Drawing.Point(503, 85)
        Me.INDSleProduct.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleProduct.Name = "INDSleProduct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProduct.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProduct.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleProduct.Properties.NullText = ""
        Me.INDSleProduct.Properties.PopupView = Me.INDGvProduct
        Me.INDSleProduct.Properties.ShowClearButton = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct, True)
        Me.INDSleProduct.Size = New System.Drawing.Size(435, 34)
        Me.INDSleProduct.StyleController = Me.INDLcBase
        Me.INDSleProduct.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProduct, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProduct, False)
        '
        'INDGvProduct
        '
        Me.INDGvProduct.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProduct.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProduct.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProduct.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProduct.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProduct.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProduct.Appearance.Row.Options.UseFont = True
        Me.INDGvProduct.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProduct_UnboundSelection, Me.INDColProductCode, Me.INDColProductName, Me.INDColProductType, Me.INDColProductSubGroup, Me.INDColProductGroup})
        Me.INDGvProduct.DetailHeight = 431
        Me.INDGvProduct.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProduct.Name = "INDGvProduct"
        Me.INDGvProduct.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProduct.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvProduct.OptionsSelection.MultiSelect = True
        Me.INDGvProduct.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProduct.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProduct.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProduct.OptionsView.ShowDetailButtons = False
        Me.INDGvProduct.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProduct, False)
        '
        'INDColProduct_UnboundSelection
        '
        Me.INDColProduct_UnboundSelection.Caption = " "
        Me.INDColProduct_UnboundSelection.CustomizationCaption = " "
        Me.INDColProduct_UnboundSelection.FieldName = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection.MinWidth = 23
        Me.INDColProduct_UnboundSelection.Name = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColProduct_UnboundSelection.Visible = True
        Me.INDColProduct_UnboundSelection.VisibleIndex = 0
        Me.INDColProduct_UnboundSelection.Width = 23
        '
        'INDColProductCode
        '
        Me.INDColProductCode.Caption = " Código"
        Me.INDColProductCode.FieldName = "Code"
        Me.INDColProductCode.MinWidth = 23
        Me.INDColProductCode.Name = "INDColProductCode"
        Me.INDColProductCode.OptionsColumn.AllowEdit = False
        Me.INDColProductCode.OptionsColumn.AllowFocus = False
        Me.INDColProductCode.Visible = True
        Me.INDColProductCode.VisibleIndex = 1
        Me.INDColProductCode.Width = 87
        '
        'INDColProductName
        '
        Me.INDColProductName.Caption = "Nombre"
        Me.INDColProductName.FieldName = "Name"
        Me.INDColProductName.MinWidth = 23
        Me.INDColProductName.Name = "INDColProductName"
        Me.INDColProductName.OptionsColumn.AllowEdit = False
        Me.INDColProductName.OptionsColumn.AllowFocus = False
        Me.INDColProductName.Visible = True
        Me.INDColProductName.VisibleIndex = 2
        Me.INDColProductName.Width = 87
        '
        'INDColProductType
        '
        Me.INDColProductType.Caption = "Tipo"
        Me.INDColProductType.FieldName = "ProductTypeIdName"
        Me.INDColProductType.MinWidth = 23
        Me.INDColProductType.Name = "INDColProductType"
        Me.INDColProductType.OptionsColumn.AllowEdit = False
        Me.INDColProductType.OptionsColumn.AllowFocus = False
        Me.INDColProductType.Visible = True
        Me.INDColProductType.VisibleIndex = 3
        Me.INDColProductType.Width = 87
        '
        'INDColProductSubGroup
        '
        Me.INDColProductSubGroup.Caption = "SubGrupo"
        Me.INDColProductSubGroup.FieldName = "ProductSubGroupIdName"
        Me.INDColProductSubGroup.MinWidth = 23
        Me.INDColProductSubGroup.Name = "INDColProductSubGroup"
        Me.INDColProductSubGroup.OptionsColumn.AllowEdit = False
        Me.INDColProductSubGroup.OptionsColumn.AllowFocus = False
        Me.INDColProductSubGroup.Visible = True
        Me.INDColProductSubGroup.VisibleIndex = 4
        Me.INDColProductSubGroup.Width = 87
        '
        'INDColProductGroup
        '
        Me.INDColProductGroup.Caption = "Grupo"
        Me.INDColProductGroup.FieldName = "ProductGroupIdName"
        Me.INDColProductGroup.MinWidth = 23
        Me.INDColProductGroup.Name = "INDColProductGroup"
        Me.INDColProductGroup.OptionsColumn.AllowEdit = False
        Me.INDColProductGroup.OptionsColumn.AllowFocus = False
        Me.INDColProductGroup.Visible = True
        Me.INDColProductGroup.VisibleIndex = 5
        Me.INDColProductGroup.Width = 87
        '
        'INDSleGroup
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleGroup, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleGroup, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleGroup, False)
        Me.INDSleGroup.EditValue = ""
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleGroup, False)
        Me.INDSleGroup.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleGroup, False)
        Me.INDSleGroup.Location = New System.Drawing.Point(503, 233)
        Me.INDSleGroup.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleGroup.Name = "INDSleGroup"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleGroup, False)
        Me.INDSleGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleGroup.Properties.Appearance.Options.UseFont = True
        Me.INDSleGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleGroup.Properties.NullText = ""
        Me.INDSleGroup.Properties.PopupSizeable = False
        Me.INDSleGroup.Properties.PopupView = Me.INDGvGroup
        Me.INDSleGroup.Properties.ShowClearButton = False
        Me.INDSleGroup.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleGroup, True)
        Me.INDSleGroup.Size = New System.Drawing.Size(435, 34)
        Me.INDSleGroup.StyleController = Me.INDLcBase
        Me.INDSleGroup.TabIndex = 27
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleGroup, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleGroup, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleGroup, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleGroup, False)
        '
        'INDGvGroup
        '
        Me.INDGvGroup.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvGroup.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvGroup.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvGroup.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvGroup.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvGroup.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvGroup.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvGroup.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvGroup.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvGroup.Appearance.Row.Options.UseFont = True
        Me.INDGvGroup.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColGroup_UnboundSelection, Me.INDColGroupCode, Me.INDColGroupName})
        Me.INDGvGroup.DetailHeight = 431
        Me.INDGvGroup.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvGroup.Name = "INDGvGroup"
        Me.INDGvGroup.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvGroup.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvGroup.OptionsSelection.MultiSelect = True
        Me.INDGvGroup.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvGroup.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvGroup.OptionsView.ShowAutoFilterRow = True
        Me.INDGvGroup.OptionsView.ShowDetailButtons = False
        Me.INDGvGroup.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvGroup, False)
        '
        'INDColGroup_UnboundSelection
        '
        Me.INDColGroup_UnboundSelection.Caption = " "
        Me.INDColGroup_UnboundSelection.FieldName = "INDColGroup_UnboundSelection"
        Me.INDColGroup_UnboundSelection.MinWidth = 23
        Me.INDColGroup_UnboundSelection.Name = "INDColGroup_UnboundSelection"
        Me.INDColGroup_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColGroup_UnboundSelection.Visible = True
        Me.INDColGroup_UnboundSelection.VisibleIndex = 0
        Me.INDColGroup_UnboundSelection.Width = 23
        '
        'INDColGroupCode
        '
        Me.INDColGroupCode.Caption = "Código"
        Me.INDColGroupCode.FieldName = "Code"
        Me.INDColGroupCode.MinWidth = 23
        Me.INDColGroupCode.Name = "INDColGroupCode"
        Me.INDColGroupCode.OptionsColumn.AllowEdit = False
        Me.INDColGroupCode.OptionsColumn.AllowFocus = False
        Me.INDColGroupCode.Visible = True
        Me.INDColGroupCode.VisibleIndex = 1
        Me.INDColGroupCode.Width = 212
        '
        'INDColGroupName
        '
        Me.INDColGroupName.Caption = "Nombre"
        Me.INDColGroupName.FieldName = "Name"
        Me.INDColGroupName.MinWidth = 23
        Me.INDColGroupName.Name = "INDColGroupName"
        Me.INDColGroupName.OptionsColumn.AllowEdit = False
        Me.INDColGroupName.OptionsColumn.AllowFocus = False
        Me.INDColGroupName.Visible = True
        Me.INDColGroupName.VisibleIndex = 2
        Me.INDColGroupName.Width = 212
        '
        'INDSleWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWarehouse, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWarehouse, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Location = New System.Drawing.Point(503, 159)
        Me.INDSleWarehouse.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleWarehouse.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleWarehouse.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleWarehouse.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseFont = True
        Me.INDSleWarehouse.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleWarehouse.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleWarehouse.Properties.NullText = ""
        Me.INDSleWarehouse.Properties.PopupSizeable = False
        Me.INDSleWarehouse.Properties.PopupView = Me.INDGvWarehouse
        Me.INDSleWarehouse.Properties.ShowClearButton = False
        Me.INDSleWarehouse.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.Size = New System.Drawing.Size(435, 34)
        Me.INDSleWarehouse.StyleController = Me.INDLcBase
        Me.INDSleWarehouse.TabIndex = 33
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleWarehouse, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleWarehouse, False)
        '
        'INDGvWarehouse
        '
        Me.INDGvWarehouse.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvWarehouse.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvWarehouse.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvWarehouse.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWarehouse.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvWarehouse.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvWarehouse.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvWarehouse.Appearance.Row.Options.UseFont = True
        Me.INDGvWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSourceWareHouse_UnboundSelection, Me.INDColSourceWarehouseCode, Me.INDColSourceWarehouseName})
        Me.INDGvWarehouse.DetailHeight = 431
        Me.INDGvWarehouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvWarehouse.Name = "INDGvWarehouse"
        Me.INDGvWarehouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvWarehouse.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvWarehouse.OptionsSelection.MultiSelect = True
        Me.INDGvWarehouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvWarehouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvWarehouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGvWarehouse.OptionsView.ShowDetailButtons = False
        Me.INDGvWarehouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvWarehouse, False)
        '
        'INDColSourceWareHouse_UnboundSelection
        '
        Me.INDColSourceWareHouse_UnboundSelection.Caption = " "
        Me.INDColSourceWareHouse_UnboundSelection.FieldName = "INDColSourceWareHouse_UnboundSelection"
        Me.INDColSourceWareHouse_UnboundSelection.MinWidth = 23
        Me.INDColSourceWareHouse_UnboundSelection.Name = "INDColSourceWareHouse_UnboundSelection"
        Me.INDColSourceWareHouse_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColSourceWareHouse_UnboundSelection.Visible = True
        Me.INDColSourceWareHouse_UnboundSelection.VisibleIndex = 0
        Me.INDColSourceWareHouse_UnboundSelection.Width = 23
        '
        'INDColSourceWarehouseCode
        '
        Me.INDColSourceWarehouseCode.Caption = "Código"
        Me.INDColSourceWarehouseCode.FieldName = "Code"
        Me.INDColSourceWarehouseCode.MinWidth = 23
        Me.INDColSourceWarehouseCode.Name = "INDColSourceWarehouseCode"
        Me.INDColSourceWarehouseCode.OptionsColumn.AllowEdit = False
        Me.INDColSourceWarehouseCode.OptionsColumn.AllowFocus = False
        Me.INDColSourceWarehouseCode.Visible = True
        Me.INDColSourceWarehouseCode.VisibleIndex = 1
        Me.INDColSourceWarehouseCode.Width = 87
        '
        'INDColSourceWarehouseName
        '
        Me.INDColSourceWarehouseName.Caption = "Nombre"
        Me.INDColSourceWarehouseName.FieldName = "Name"
        Me.INDColSourceWarehouseName.MinWidth = 23
        Me.INDColSourceWarehouseName.Name = "INDColSourceWarehouseName"
        Me.INDColSourceWarehouseName.OptionsColumn.AllowEdit = False
        Me.INDColSourceWarehouseName.OptionsColumn.AllowFocus = False
        Me.INDColSourceWarehouseName.Visible = True
        Me.INDColSourceWarehouseName.VisibleIndex = 2
        Me.INDColSourceWarehouseName.Width = 87
        '
        'INDsleCurrency
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCurrency, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCurrency, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Location = New System.Drawing.Point(24, 381)
        Me.INDsleCurrency.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDsleCurrency.MaximumSize = New System.Drawing.Size(415, 34)
        Me.INDsleCurrency.MinimumSize = New System.Drawing.Size(415, 34)
        Me.INDsleCurrency.Name = "INDsleCurrency"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.INDsleCurrency.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCurrency.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCurrency.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseFont = True
        Me.INDsleCurrency.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleCurrency.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleCurrency.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDsleCurrency.Properties.DisplayMember = "CurrencyName"
        Me.INDsleCurrency.Properties.NullText = ""
        Me.INDsleCurrency.Properties.PopupSizeable = False
        Me.INDsleCurrency.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleCurrency.Properties.ShowFooter = False
        Me.INDsleCurrency.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCurrency, True)
        Me.INDsleCurrency.Size = New System.Drawing.Size(415, 34)
        Me.INDsleCurrency.StyleController = Me.INDLcBase
        Me.INDsleCurrency.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCurrency, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCurrency, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCurrency, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn178, Me.GridColumn761, Me.GridColumn771})
        Me.SearchLookUpEdit1View.DetailHeight = 431
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn178
        '
        Me.GridColumn178.Caption = "Código"
        Me.GridColumn178.FieldName = "Codigo"
        Me.GridColumn178.MinWidth = 23
        Me.GridColumn178.Name = "GridColumn178"
        Me.GridColumn178.Visible = True
        Me.GridColumn178.VisibleIndex = 0
        Me.GridColumn178.Width = 87
        '
        'GridColumn761
        '
        Me.GridColumn761.Caption = "Nombre"
        Me.GridColumn761.FieldName = "CurrencyName"
        Me.GridColumn761.MinWidth = 23
        Me.GridColumn761.Name = "GridColumn761"
        Me.GridColumn761.Visible = True
        Me.GridColumn761.VisibleIndex = 1
        Me.GridColumn761.Width = 87
        '
        'GridColumn771
        '
        Me.GridColumn771.Caption = "Abreviación"
        Me.GridColumn771.FieldName = "Abbreviation"
        Me.GridColumn771.MinWidth = 23
        Me.GridColumn771.Name = "GridColumn771"
        Me.GridColumn771.Visible = True
        Me.GridColumn771.VisibleIndex = 2
        Me.GridColumn771.Width = 87
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(503, 362)
        Me.INDSbGenerateReport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSbGenerateReport.MaximumSize = New System.Drawing.Size(435, 39)
        Me.INDSbGenerateReport.MinimumSize = New System.Drawing.Size(435, 39)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(435, 39)
        Me.INDSbGenerateReport.StyleController = Me.INDLcBase
        Me.INDSbGenerateReport.TabIndex = 9
        Me.INDSbGenerateReport.Text = "Generar Reporte"
        '
        'INDSbGenerateExcell
        '
        Me.INDSbGenerateExcell.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.ICONO_EXCEL_02
        Me.INDSbGenerateExcell.Location = New System.Drawing.Point(941, 362)
        Me.INDSbGenerateExcell.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSbGenerateExcell.MaximumSize = New System.Drawing.Size(47, 39)
        Me.INDSbGenerateExcell.MinimumSize = New System.Drawing.Size(47, 39)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateExcell, False)
        Me.INDSbGenerateExcell.Name = "INDSbGenerateExcell"
        Me.INDSbGenerateExcell.Size = New System.Drawing.Size(47, 39)
        Me.INDSbGenerateExcell.StyleController = Me.INDLcBase
        Me.INDSbGenerateExcell.TabIndex = 38
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.CustomizationFormText = "INDLcgBase"
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilterRequired, Me.INDLcgFilterOptional})
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(1144, 911)
        Me.INDLcgBase.TextVisible = False
        '
        'INDLcgFilterRequired
        '
        Me.INDLcgFilterRequired.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterRequired.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterRequired.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFilterRequired.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterRequired.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgFilterRequired, False)
        Me.INDLcgFilterRequired.CustomizationFormText = "Filtros"
        Me.INDLcgFilterRequired.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTypeReport, Me.INDLciOrderBy, Me.INDLciValorization, Me.INDLciAmountsZero, Me.INDlyCurrency})
        Me.INDLcgFilterRequired.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilterRequired.Name = "INDLcgFilterRequired"
        Me.INDLcgFilterRequired.Size = New System.Drawing.Size(479, 891)
        Me.INDLcgFilterRequired.Text = "Filtros"
        '
        'INDLciTypeReport
        '
        Me.INDLciTypeReport.Control = Me.INDGleTypeReport
        Me.INDLciTypeReport.CustomizationFormText = "Tipo Reporte"
        Me.INDLciTypeReport.Location = New System.Drawing.Point(0, 217)
        Me.INDLciTypeReport.MaxSize = New System.Drawing.Size(420, 74)
        Me.INDLciTypeReport.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLciTypeReport.Name = "INDLciTypeReport"
        Me.INDLciTypeReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.INDLciTypeReport.Size = New System.Drawing.Size(455, 74)
        Me.INDLciTypeReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTypeReport.Text = "Tipo Reporte :"
        Me.INDLciTypeReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTypeReport.TextSize = New System.Drawing.Size(212, 23)
        '
        'INDLciOrderBy
        '
        Me.INDLciOrderBy.Control = Me.INDGleOrderBy
        Me.INDLciOrderBy.CustomizationFormText = "Ordenar Por"
        Me.INDLciOrderBy.Location = New System.Drawing.Point(0, 69)
        Me.INDLciOrderBy.MaxSize = New System.Drawing.Size(420, 74)
        Me.INDLciOrderBy.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLciOrderBy.Name = "INDLciOrderBy"
        Me.INDLciOrderBy.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.INDLciOrderBy.Size = New System.Drawing.Size(455, 74)
        Me.INDLciOrderBy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciOrderBy.Text = "Ordenar Por :"
        Me.INDLciOrderBy.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciOrderBy.TextSize = New System.Drawing.Size(212, 23)
        '
        'INDLciValorization
        '
        Me.INDLciValorization.Control = Me.INDGleValorization
        Me.INDLciValorization.CustomizationFormText = "Valorización"
        Me.INDLciValorization.Location = New System.Drawing.Point(0, 0)
        Me.INDLciValorization.MaxSize = New System.Drawing.Size(420, 69)
        Me.INDLciValorization.MinSize = New System.Drawing.Size(420, 69)
        Me.INDLciValorization.Name = "INDLciValorization"
        Me.INDLciValorization.Size = New System.Drawing.Size(455, 69)
        Me.INDLciValorization.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValorization.Text = "Valorización :"
        Me.INDLciValorization.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValorization.TextSize = New System.Drawing.Size(212, 23)
        '
        'INDLciAmountsZero
        '
        Me.INDLciAmountsZero.Control = Me.INDsleAccountsZero
        Me.INDLciAmountsZero.CustomizationFormText = "Con Cantidades en Cero"
        Me.INDLciAmountsZero.Location = New System.Drawing.Point(0, 143)
        Me.INDLciAmountsZero.MaxSize = New System.Drawing.Size(420, 74)
        Me.INDLciAmountsZero.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLciAmountsZero.Name = "INDLciAmountsZero"
        Me.INDLciAmountsZero.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 7, 2)
        Me.INDLciAmountsZero.Size = New System.Drawing.Size(455, 74)
        Me.INDLciAmountsZero.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAmountsZero.Text = "Con Cantidades en Cero :"
        Me.INDLciAmountsZero.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAmountsZero.TextSize = New System.Drawing.Size(212, 23)
        '
        'INDlyCurrency
        '
        Me.INDlyCurrency.Control = Me.INDsleCurrency
        Me.INDlyCurrency.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyCurrency.CustomizationFormText = "Valorizar Cartera a"
        Me.INDlyCurrency.Location = New System.Drawing.Point(0, 291)
        Me.INDlyCurrency.MaxSize = New System.Drawing.Size(455, 79)
        Me.INDlyCurrency.MinSize = New System.Drawing.Size(455, 74)
        Me.INDlyCurrency.Name = "INDlyCurrency"
        Me.INDlyCurrency.Size = New System.Drawing.Size(455, 541)
        Me.INDlyCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyCurrency.Text = "Moneda :"
        Me.INDlyCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyCurrency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyCurrency.TextSize = New System.Drawing.Size(157, 26)
        Me.INDlyCurrency.TextToControlDistance = 5
        '
        'INDLcgFilterOptional
        '
        Me.INDLcgFilterOptional.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterOptional.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilterOptional.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFilterOptional.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilterOptional.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgFilterOptional, False)
        Me.INDLcgFilterOptional.CustomizationFormText = "Rangos"
        Me.INDLcgFilterOptional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLblProduct, Me.INDLblGroup, Me.INDLblSubGroup, Me.INDLblSourceWarehouse, Me.INDLciGenerateReport, Me.LayoutControlItem2})
        Me.INDLcgFilterOptional.Location = New System.Drawing.Point(479, 0)
        Me.INDLcgFilterOptional.Name = "INDLcgFilterOptional"
        Me.INDLcgFilterOptional.Size = New System.Drawing.Size(645, 891)
        Me.INDLcgFilterOptional.Text = "Rangos"
        '
        'INDLblProduct
        '
        Me.INDLblProduct.Control = Me.INDSleProduct
        Me.INDLblProduct.CustomizationFormText = "Producto :"
        Me.INDLblProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLblProduct.MaxSize = New System.Drawing.Size(439, 74)
        Me.INDLblProduct.MinSize = New System.Drawing.Size(426, 74)
        Me.INDLblProduct.Name = "INDLblProduct"
        Me.INDLblProduct.Size = New System.Drawing.Size(621, 74)
        Me.INDLblProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblProduct.Text = "Producto :"
        Me.INDLblProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblProduct.TextSize = New System.Drawing.Size(212, 23)
        '
        'INDLblGroup
        '
        Me.INDLblGroup.Control = Me.INDSleGroup
        Me.INDLblGroup.CustomizationFormText = "Grupo"
        Me.INDLblGroup.Location = New System.Drawing.Point(0, 148)
        Me.INDLblGroup.MaxSize = New System.Drawing.Size(439, 74)
        Me.INDLblGroup.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLblGroup.Name = "INDLblGroup"
        Me.INDLblGroup.Size = New System.Drawing.Size(621, 74)
        Me.INDLblGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblGroup.Text = "Grupo :"
        Me.INDLblGroup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblGroup.TextSize = New System.Drawing.Size(212, 23)
        '
        'INDLblSubGroup
        '
        Me.INDLblSubGroup.Control = Me.INDSleSubGroup
        Me.INDLblSubGroup.CustomizationFormText = "SubGrupo :"
        Me.INDLblSubGroup.Location = New System.Drawing.Point(0, 222)
        Me.INDLblSubGroup.MaxSize = New System.Drawing.Size(439, 74)
        Me.INDLblSubGroup.MinSize = New System.Drawing.Size(420, 74)
        Me.INDLblSubGroup.Name = "INDLblSubGroup"
        Me.INDLblSubGroup.Size = New System.Drawing.Size(621, 74)
        Me.INDLblSubGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblSubGroup.Text = "SubGrupo :"
        Me.INDLblSubGroup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblSubGroup.TextSize = New System.Drawing.Size(212, 23)
        '
        'INDLblSourceWarehouse
        '
        Me.INDLblSourceWarehouse.Control = Me.INDSleWarehouse
        Me.INDLblSourceWarehouse.CustomizationFormText = "Almacen :"
        Me.INDLblSourceWarehouse.Location = New System.Drawing.Point(0, 74)
        Me.INDLblSourceWarehouse.MaxSize = New System.Drawing.Size(439, 74)
        Me.INDLblSourceWarehouse.MinSize = New System.Drawing.Size(426, 74)
        Me.INDLblSourceWarehouse.Name = "INDLblSourceWarehouse"
        Me.INDLblSourceWarehouse.Size = New System.Drawing.Size(621, 74)
        Me.INDLblSourceWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblSourceWarehouse.Text = "Almacen :"
        Me.INDLblSourceWarehouse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblSourceWarehouse.TextSize = New System.Drawing.Size(212, 23)
        '
        'INDLciGenerateReport
        '
        Me.INDLciGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciGenerateReport.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciGenerateReport.CustomizationFormText = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Location = New System.Drawing.Point(0, 296)
        Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(440, 66)
        Me.INDLciGenerateReport.MinSize = New System.Drawing.Size(400, 54)
        Me.INDLciGenerateReport.Name = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 9, 2)
        Me.INDLciGenerateReport.Size = New System.Drawing.Size(440, 536)
        Me.INDLciGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateReport.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSbGenerateExcell
        Me.LayoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(440, 296)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(57, 48)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(57, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 2, 9, 2)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(181, 536)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'GridColumn87
        '
        Me.GridColumn87.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn87.Caption = "Selección"
        Me.GridColumn87.FieldName = "Item2"
        Me.GridColumn87.Name = "GridColumn87"
        '
        'GridColumn33
        '
        Me.GridColumn33.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn33.Caption = "Selección"
        Me.GridColumn33.FieldName = "Item2"
        Me.GridColumn33.Name = "GridColumn33"
        '
        'GridColumn89
        '
        Me.GridColumn89.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn89.Caption = "Selección"
        Me.GridColumn89.FieldName = "Item2"
        Me.GridColumn89.Name = "GridColumn89"
        '
        'DocumentViewerBarManager1
        '
        Me.DocumentViewerBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.PreviewBar1, Me.PreviewBar2})
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlTop)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlRight)
        Me.DocumentViewerBarManager1.DocumentViewer = Me.INDDvDocumentViewer
        Me.DocumentViewerBarManager1.Form = Me.INDDvDocumentViewer
        Me.DocumentViewerBarManager1.ImageStream = CType(resources.GetObject("DocumentViewerBarManager1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.DocumentViewerBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.PrintPreviewStaticItem1, Me.BarStaticItem1, Me.ProgressBarEditItem1, Me.PrintPreviewBarItem1, Me.BarButtonItem1, Me.PrintPreviewStaticItem2, Me.ZoomTrackBarEditItem1, Me.PrintPreviewBarItem2, Me.PrintPreviewBarItem3, Me.PrintPreviewBarItem4, Me.PrintPreviewBarItem5, Me.PrintPreviewBarItem6, Me.PrintPreviewBarItem7, Me.PrintPreviewBarItem8, Me.PrintPreviewBarItem9, Me.PrintPreviewBarItem10, Me.PrintPreviewBarItem11, Me.PrintPreviewBarItem12, Me.PrintPreviewBarItem13, Me.PrintPreviewBarItem14, Me.PrintPreviewBarItem15, Me.ZoomBarEditItem1, Me.PrintPreviewBarItem16, Me.PrintPreviewBarItem17, Me.PrintPreviewBarItem18, Me.PrintPreviewBarItem19, Me.PrintPreviewBarItem20, Me.PrintPreviewBarItem21, Me.PrintPreviewBarItem22, Me.PrintPreviewBarItem23, Me.PrintPreviewBarItem24, Me.PrintPreviewBarItem25, Me.PrintPreviewBarItem26, Me.PrintPreviewSubItem1, Me.PrintPreviewSubItem2, Me.PrintPreviewSubItem3, Me.PrintPreviewSubItem4, Me.PrintPreviewBarItem27, Me.PrintPreviewBarItem28, Me.BarToolbarsListItem1, Me.PrintPreviewBarCheckItem1, Me.PrintPreviewBarCheckItem2, Me.PrintPreviewBarCheckItem3, Me.PrintPreviewBarCheckItem4, Me.PrintPreviewBarCheckItem5, Me.PrintPreviewBarCheckItem6, Me.PrintPreviewBarCheckItem7, Me.PrintPreviewBarCheckItem8, Me.PrintPreviewBarCheckItem9, Me.PrintPreviewBarCheckItem10, Me.PrintPreviewBarCheckItem11, Me.PrintPreviewBarCheckItem12, Me.PrintPreviewBarCheckItem13, Me.PrintPreviewBarCheckItem14, Me.PrintPreviewBarCheckItem15, Me.PrintPreviewBarCheckItem16, Me.PrintPreviewBarCheckItem17})
        Me.DocumentViewerBarManager1.MaxItemId = 57
        Me.DocumentViewerBarManager1.PreviewBar = Me.PreviewBar1
        Me.DocumentViewerBarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemProgressBar1, Me.RepositoryItemZoomTrackBar1, Me.PrintPreviewRepositoryItemComboBox1})
        Me.DocumentViewerBarManager1.StatusBar = Me.PreviewBar2
        Me.DocumentViewerBarManager1.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.[True]
        '
        'PreviewBar1
        '
        Me.PreviewBar1.BarName = "Toolbar"
        Me.PreviewBar1.DockCol = 0
        Me.PreviewBar1.DockRow = 0
        Me.PreviewBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.PreviewBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem4), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem5, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem6, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem7), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem8, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem12), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem13, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem14), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem15, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem16), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem17, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem18), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem19), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem20), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem21, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem22), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26, True)})
        Me.PreviewBar1.Text = "Toolbar"
        '
        'PrintPreviewBarItem2
        '
        Me.PrintPreviewBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem2.Caption = "Document Map"
        Me.PrintPreviewBarItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap
        Me.PrintPreviewBarItem2.Enabled = False
        Me.PrintPreviewBarItem2.Hint = "Document Map"
        Me.PrintPreviewBarItem2.Id = 7
        Me.PrintPreviewBarItem2.ImageOptions.ImageIndex = 19
        Me.PrintPreviewBarItem2.Name = "PrintPreviewBarItem2"
        '
        'PrintPreviewBarItem3
        '
        Me.PrintPreviewBarItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem3.Caption = "Parameters"
        Me.PrintPreviewBarItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Parameters
        Me.PrintPreviewBarItem3.Enabled = False
        Me.PrintPreviewBarItem3.Hint = "Parameters"
        Me.PrintPreviewBarItem3.Id = 8
        Me.PrintPreviewBarItem3.ImageOptions.ImageIndex = 22
        Me.PrintPreviewBarItem3.Name = "PrintPreviewBarItem3"
        '
        'PrintPreviewBarItem4
        '
        Me.PrintPreviewBarItem4.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem4.Caption = "Search"
        Me.PrintPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find
        Me.PrintPreviewBarItem4.Enabled = False
        Me.PrintPreviewBarItem4.Hint = "Search"
        Me.PrintPreviewBarItem4.Id = 9
        Me.PrintPreviewBarItem4.ImageOptions.ImageIndex = 20
        Me.PrintPreviewBarItem4.Name = "PrintPreviewBarItem4"
        '
        'PrintPreviewBarItem5
        '
        Me.PrintPreviewBarItem5.Caption = "Customize"
        Me.PrintPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize
        Me.PrintPreviewBarItem5.Enabled = False
        Me.PrintPreviewBarItem5.Hint = "Customize"
        Me.PrintPreviewBarItem5.Id = 10
        Me.PrintPreviewBarItem5.ImageOptions.ImageIndex = 14
        Me.PrintPreviewBarItem5.Name = "PrintPreviewBarItem5"
        '
        'PrintPreviewBarItem6
        '
        Me.PrintPreviewBarItem6.Caption = "Open"
        Me.PrintPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open
        Me.PrintPreviewBarItem6.Enabled = False
        Me.PrintPreviewBarItem6.Hint = "Open a document"
        Me.PrintPreviewBarItem6.Id = 11
        Me.PrintPreviewBarItem6.ImageOptions.ImageIndex = 23
        Me.PrintPreviewBarItem6.Name = "PrintPreviewBarItem6"
        '
        'PrintPreviewBarItem7
        '
        Me.PrintPreviewBarItem7.Caption = "Save"
        Me.PrintPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save
        Me.PrintPreviewBarItem7.Enabled = False
        Me.PrintPreviewBarItem7.Hint = "Save the document"
        Me.PrintPreviewBarItem7.Id = 12
        Me.PrintPreviewBarItem7.ImageOptions.ImageIndex = 24
        Me.PrintPreviewBarItem7.Name = "PrintPreviewBarItem7"
        '
        'PrintPreviewBarItem8
        '
        Me.PrintPreviewBarItem8.Caption = "&Print..."
        Me.PrintPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print
        Me.PrintPreviewBarItem8.Enabled = False
        Me.PrintPreviewBarItem8.Hint = "Print"
        Me.PrintPreviewBarItem8.Id = 13
        Me.PrintPreviewBarItem8.ImageOptions.ImageIndex = 0
        Me.PrintPreviewBarItem8.Name = "PrintPreviewBarItem8"
        '
        'PrintPreviewBarItem9
        '
        Me.PrintPreviewBarItem9.Caption = "P&rint"
        Me.PrintPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect
        Me.PrintPreviewBarItem9.Enabled = False
        Me.PrintPreviewBarItem9.Hint = "Quick Print"
        Me.PrintPreviewBarItem9.Id = 14
        Me.PrintPreviewBarItem9.ImageOptions.ImageIndex = 1
        Me.PrintPreviewBarItem9.Name = "PrintPreviewBarItem9"
        '
        'PrintPreviewBarItem10
        '
        Me.PrintPreviewBarItem10.Caption = "Page Set&up..."
        Me.PrintPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup
        Me.PrintPreviewBarItem10.Enabled = False
        Me.PrintPreviewBarItem10.Hint = "Page Setup"
        Me.PrintPreviewBarItem10.Id = 15
        Me.PrintPreviewBarItem10.ImageOptions.ImageIndex = 2
        Me.PrintPreviewBarItem10.Name = "PrintPreviewBarItem10"
        '
        'PrintPreviewBarItem11
        '
        Me.PrintPreviewBarItem11.Caption = "Header And Footer"
        Me.PrintPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF
        Me.PrintPreviewBarItem11.Enabled = False
        Me.PrintPreviewBarItem11.Hint = "Header And Footer"
        Me.PrintPreviewBarItem11.Id = 16
        Me.PrintPreviewBarItem11.ImageOptions.ImageIndex = 15
        Me.PrintPreviewBarItem11.Name = "PrintPreviewBarItem11"
        '
        'PrintPreviewBarItem12
        '
        Me.PrintPreviewBarItem12.ActAsDropDown = True
        Me.PrintPreviewBarItem12.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem12.Caption = "Scale"
        Me.PrintPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale
        Me.PrintPreviewBarItem12.Enabled = False
        Me.PrintPreviewBarItem12.Hint = "Scale"
        Me.PrintPreviewBarItem12.Id = 17
        Me.PrintPreviewBarItem12.ImageOptions.ImageIndex = 25
        Me.PrintPreviewBarItem12.Name = "PrintPreviewBarItem12"
        '
        'PrintPreviewBarItem13
        '
        Me.PrintPreviewBarItem13.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem13.Caption = "Hand Tool"
        Me.PrintPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool
        Me.PrintPreviewBarItem13.Enabled = False
        Me.PrintPreviewBarItem13.Hint = "Hand Tool"
        Me.PrintPreviewBarItem13.Id = 18
        Me.PrintPreviewBarItem13.ImageOptions.ImageIndex = 16
        Me.PrintPreviewBarItem13.Name = "PrintPreviewBarItem13"
        '
        'PrintPreviewBarItem14
        '
        Me.PrintPreviewBarItem14.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem14.Caption = "Magnifier"
        Me.PrintPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier
        Me.PrintPreviewBarItem14.Enabled = False
        Me.PrintPreviewBarItem14.Hint = "Magnifier"
        Me.PrintPreviewBarItem14.Id = 19
        Me.PrintPreviewBarItem14.ImageOptions.ImageIndex = 3
        Me.PrintPreviewBarItem14.Name = "PrintPreviewBarItem14"
        '
        'PrintPreviewBarItem15
        '
        Me.PrintPreviewBarItem15.Caption = "Zoom Out"
        Me.PrintPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut
        Me.PrintPreviewBarItem15.Enabled = False
        Me.PrintPreviewBarItem15.Hint = "Zoom Out"
        Me.PrintPreviewBarItem15.Id = 20
        Me.PrintPreviewBarItem15.ImageOptions.ImageIndex = 5
        Me.PrintPreviewBarItem15.Name = "PrintPreviewBarItem15"
        '
        'ZoomBarEditItem1
        '
        Me.ZoomBarEditItem1.Caption = "Zoom"
        Me.ZoomBarEditItem1.Edit = Me.PrintPreviewRepositoryItemComboBox1
        Me.ZoomBarEditItem1.EditValue = "100%"
        Me.ZoomBarEditItem1.EditWidth = 70
        Me.ZoomBarEditItem1.Enabled = False
        Me.ZoomBarEditItem1.Hint = "Zoom"
        Me.ZoomBarEditItem1.Id = 21
        Me.ZoomBarEditItem1.Name = "ZoomBarEditItem1"
        '
        'PrintPreviewRepositoryItemComboBox1
        '
        Me.PrintPreviewRepositoryItemComboBox1.AutoComplete = False
        Me.PrintPreviewRepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PrintPreviewRepositoryItemComboBox1.DropDownRows = 11
        Me.PrintPreviewRepositoryItemComboBox1.Name = "PrintPreviewRepositoryItemComboBox1"
        '
        'PrintPreviewBarItem16
        '
        Me.PrintPreviewBarItem16.Caption = "Zoom In"
        Me.PrintPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn
        Me.PrintPreviewBarItem16.Enabled = False
        Me.PrintPreviewBarItem16.Hint = "Zoom In"
        Me.PrintPreviewBarItem16.Id = 22
        Me.PrintPreviewBarItem16.ImageOptions.ImageIndex = 4
        Me.PrintPreviewBarItem16.Name = "PrintPreviewBarItem16"
        '
        'PrintPreviewBarItem17
        '
        Me.PrintPreviewBarItem17.Caption = "First Page"
        Me.PrintPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage
        Me.PrintPreviewBarItem17.Enabled = False
        Me.PrintPreviewBarItem17.Hint = "First Page"
        Me.PrintPreviewBarItem17.Id = 23
        Me.PrintPreviewBarItem17.ImageOptions.ImageIndex = 7
        Me.PrintPreviewBarItem17.Name = "PrintPreviewBarItem17"
        '
        'PrintPreviewBarItem18
        '
        Me.PrintPreviewBarItem18.Caption = "Previous Page"
        Me.PrintPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage
        Me.PrintPreviewBarItem18.Enabled = False
        Me.PrintPreviewBarItem18.Hint = "Previous Page"
        Me.PrintPreviewBarItem18.Id = 24
        Me.PrintPreviewBarItem18.ImageOptions.ImageIndex = 8
        Me.PrintPreviewBarItem18.Name = "PrintPreviewBarItem18"
        '
        'PrintPreviewBarItem19
        '
        Me.PrintPreviewBarItem19.Caption = "Next Page"
        Me.PrintPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage
        Me.PrintPreviewBarItem19.Enabled = False
        Me.PrintPreviewBarItem19.Hint = "Next Page"
        Me.PrintPreviewBarItem19.Id = 25
        Me.PrintPreviewBarItem19.ImageOptions.ImageIndex = 9
        Me.PrintPreviewBarItem19.Name = "PrintPreviewBarItem19"
        '
        'PrintPreviewBarItem20
        '
        Me.PrintPreviewBarItem20.Caption = "Last Page"
        Me.PrintPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage
        Me.PrintPreviewBarItem20.Enabled = False
        Me.PrintPreviewBarItem20.Hint = "Last Page"
        Me.PrintPreviewBarItem20.Id = 26
        Me.PrintPreviewBarItem20.ImageOptions.ImageIndex = 10
        Me.PrintPreviewBarItem20.Name = "PrintPreviewBarItem20"
        '
        'PrintPreviewBarItem21
        '
        Me.PrintPreviewBarItem21.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem21.Caption = "Multiple Pages"
        Me.PrintPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages
        Me.PrintPreviewBarItem21.Enabled = False
        Me.PrintPreviewBarItem21.Hint = "Multiple Pages"
        Me.PrintPreviewBarItem21.Id = 27
        Me.PrintPreviewBarItem21.ImageOptions.ImageIndex = 11
        Me.PrintPreviewBarItem21.Name = "PrintPreviewBarItem21"
        '
        'PrintPreviewBarItem22
        '
        Me.PrintPreviewBarItem22.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem22.Caption = "&Color..."
        Me.PrintPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground
        Me.PrintPreviewBarItem22.Enabled = False
        Me.PrintPreviewBarItem22.Hint = "Background"
        Me.PrintPreviewBarItem22.Id = 28
        Me.PrintPreviewBarItem22.ImageOptions.ImageIndex = 12
        Me.PrintPreviewBarItem22.Name = "PrintPreviewBarItem22"
        '
        'PrintPreviewBarItem23
        '
        Me.PrintPreviewBarItem23.Caption = "&Watermark..."
        Me.PrintPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark
        Me.PrintPreviewBarItem23.Enabled = False
        Me.PrintPreviewBarItem23.Hint = "Watermark"
        Me.PrintPreviewBarItem23.Id = 29
        Me.PrintPreviewBarItem23.ImageOptions.ImageIndex = 21
        Me.PrintPreviewBarItem23.Name = "PrintPreviewBarItem23"
        '
        'PrintPreviewBarItem24
        '
        Me.PrintPreviewBarItem24.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem24.Caption = "Export Document..."
        Me.PrintPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile
        Me.PrintPreviewBarItem24.Enabled = False
        Me.PrintPreviewBarItem24.Hint = "Export Document..."
        Me.PrintPreviewBarItem24.Id = 30
        Me.PrintPreviewBarItem24.ImageOptions.ImageIndex = 18
        Me.PrintPreviewBarItem24.Name = "PrintPreviewBarItem24"
        '
        'PrintPreviewBarItem25
        '
        Me.PrintPreviewBarItem25.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem25.Caption = "Send via E-Mail..."
        Me.PrintPreviewBarItem25.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile
        Me.PrintPreviewBarItem25.Enabled = False
        Me.PrintPreviewBarItem25.Hint = "Send via E-Mail..."
        Me.PrintPreviewBarItem25.Id = 31
        Me.PrintPreviewBarItem25.ImageOptions.ImageIndex = 17
        Me.PrintPreviewBarItem25.Name = "PrintPreviewBarItem25"
        '
        'PrintPreviewBarItem26
        '
        Me.PrintPreviewBarItem26.Caption = "E&xit"
        Me.PrintPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview
        Me.PrintPreviewBarItem26.Enabled = False
        Me.PrintPreviewBarItem26.Hint = "Close Preview"
        Me.PrintPreviewBarItem26.Id = 32
        Me.PrintPreviewBarItem26.ImageOptions.ImageIndex = 13
        Me.PrintPreviewBarItem26.Name = "PrintPreviewBarItem26"
        '
        'PreviewBar2
        '
        Me.PreviewBar2.BarName = "Status Bar"
        Me.PreviewBar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.PreviewBar2.DockCol = 0
        Me.PreviewBar2.DockRow = 0
        Me.PreviewBar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.PreviewBar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ProgressBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewStaticItem2, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomTrackBarEditItem1)})
        Me.PreviewBar2.OptionsBar.AllowQuickCustomization = False
        Me.PreviewBar2.OptionsBar.DrawDragBorder = False
        Me.PreviewBar2.OptionsBar.UseWholeRow = True
        Me.PreviewBar2.Text = "Status Bar"
        '
        'PrintPreviewStaticItem1
        '
        Me.PrintPreviewStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PrintPreviewStaticItem1.Caption = "Nothing"
        Me.PrintPreviewStaticItem1.Id = 0
        Me.PrintPreviewStaticItem1.LeftIndent = 1
        Me.PrintPreviewStaticItem1.Name = "PrintPreviewStaticItem1"
        Me.PrintPreviewStaticItem1.RightIndent = 1
        Me.PrintPreviewStaticItem1.Type = "PageOfPages"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.BarStaticItem1.Id = 1
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime
        '
        'ProgressBarEditItem1
        '
        Me.ProgressBarEditItem1.Edit = Me.RepositoryItemProgressBar1
        Me.ProgressBarEditItem1.EditHeight = 12
        Me.ProgressBarEditItem1.EditWidth = 150
        Me.ProgressBarEditItem1.Id = 2
        Me.ProgressBarEditItem1.Name = "ProgressBarEditItem1"
        Me.ProgressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'RepositoryItemProgressBar1
        '
        Me.RepositoryItemProgressBar1.Name = "RepositoryItemProgressBar1"
        '
        'PrintPreviewBarItem1
        '
        Me.PrintPreviewBarItem1.Caption = "Stop"
        Me.PrintPreviewBarItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.StopPageBuilding
        Me.PrintPreviewBarItem1.Enabled = False
        Me.PrintPreviewBarItem1.Hint = "Stop"
        Me.PrintPreviewBarItem1.Id = 3
        Me.PrintPreviewBarItem1.Name = "PrintPreviewBarItem1"
        Me.PrintPreviewBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left
        Me.BarButtonItem1.Enabled = False
        Me.BarButtonItem1.Id = 4
        Me.BarButtonItem1.Name = "BarButtonItem1"
        Me.BarButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime
        '
        'PrintPreviewStaticItem2
        '
        Me.PrintPreviewStaticItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.PrintPreviewStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PrintPreviewStaticItem2.Caption = "100%"
        Me.PrintPreviewStaticItem2.Id = 5
        Me.PrintPreviewStaticItem2.Name = "PrintPreviewStaticItem2"
        Me.PrintPreviewStaticItem2.Size = New System.Drawing.Size(40, 0)
        Me.PrintPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Far
        Me.PrintPreviewStaticItem2.Type = "ZoomFactor"
        Me.PrintPreviewStaticItem2.Width = 40
        '
        'ZoomTrackBarEditItem1
        '
        Me.ZoomTrackBarEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.ZoomTrackBarEditItem1.Edit = Me.RepositoryItemZoomTrackBar1
        Me.ZoomTrackBarEditItem1.EditValue = 90
        Me.ZoomTrackBarEditItem1.EditWidth = 140
        Me.ZoomTrackBarEditItem1.Enabled = False
        Me.ZoomTrackBarEditItem1.Id = 6
        Me.ZoomTrackBarEditItem1.Name = "ZoomTrackBarEditItem1"
        Me.ZoomTrackBarEditItem1.Range = New Integer() {10, 500}
        '
        'RepositoryItemZoomTrackBar1
        '
        Me.RepositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center
        Me.RepositoryItemZoomTrackBar1.AllowFocused = False
        Me.RepositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.RepositoryItemZoomTrackBar1.Maximum = 180
        Me.RepositoryItemZoomTrackBar1.Middle = 90
        Me.RepositoryItemZoomTrackBar1.Name = "RepositoryItemZoomTrackBar1"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1258, 30)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 880)
        Me.barDockControlBottom.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1258, 27)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 30)
        Me.barDockControlLeft.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 850)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1258, 30)
        Me.barDockControlRight.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 850)
        '
        'INDDvDocumentViewer
        '
        Me.INDDvDocumentViewer.Controls.Add(Me.barDockControlLeft)
        Me.INDDvDocumentViewer.Controls.Add(Me.barDockControlRight)
        Me.INDDvDocumentViewer.Controls.Add(Me.barDockControlBottom)
        Me.INDDvDocumentViewer.Controls.Add(Me.barDockControlTop)
        Me.INDDvDocumentViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoDocumentViewer1.SetExtendProperties(Me.INDDvDocumentViewer, True)
        Me.INDDvDocumentViewer.IsMetric = True
        Me.INDDvDocumentViewer.Location = New System.Drawing.Point(84, 2)
        Me.INDDvDocumentViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDDvDocumentViewer.Name = "INDDvDocumentViewer"
        Me.INDDvDocumentViewer.Size = New System.Drawing.Size(1258, 907)
        Me.INDDvDocumentViewer.TabIndex = 1
        '
        'PrintPreviewSubItem1
        '
        Me.PrintPreviewSubItem1.Caption = "&File"
        Me.PrintPreviewSubItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.File
        Me.PrintPreviewSubItem1.Id = 33
        Me.PrintPreviewSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26, True)})
        Me.PrintPreviewSubItem1.Name = "PrintPreviewSubItem1"
        '
        'PrintPreviewSubItem2
        '
        Me.PrintPreviewSubItem2.Caption = "&View"
        Me.PrintPreviewSubItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.View
        Me.PrintPreviewSubItem2.Id = 34
        Me.PrintPreviewSubItem2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewSubItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarToolbarsListItem1, True)})
        Me.PrintPreviewSubItem2.Name = "PrintPreviewSubItem2"
        '
        'PrintPreviewSubItem4
        '
        Me.PrintPreviewSubItem4.Caption = "&Page Layout"
        Me.PrintPreviewSubItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayout
        Me.PrintPreviewSubItem4.Id = 36
        Me.PrintPreviewSubItem4.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem27), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem28)})
        Me.PrintPreviewSubItem4.Name = "PrintPreviewSubItem4"
        '
        'PrintPreviewBarItem27
        '
        Me.PrintPreviewBarItem27.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem27.Caption = "&Facing"
        Me.PrintPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutFacing
        Me.PrintPreviewBarItem27.Enabled = False
        Me.PrintPreviewBarItem27.GroupIndex = 100
        Me.PrintPreviewBarItem27.Id = 37
        Me.PrintPreviewBarItem27.Name = "PrintPreviewBarItem27"
        '
        'PrintPreviewBarItem28
        '
        Me.PrintPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem28.Caption = "&Continuous"
        Me.PrintPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutContinuous
        Me.PrintPreviewBarItem28.Enabled = False
        Me.PrintPreviewBarItem28.GroupIndex = 100
        Me.PrintPreviewBarItem28.Id = 38
        Me.PrintPreviewBarItem28.Name = "PrintPreviewBarItem28"
        '
        'BarToolbarsListItem1
        '
        Me.BarToolbarsListItem1.Caption = "Bars"
        Me.BarToolbarsListItem1.Id = 39
        Me.BarToolbarsListItem1.Name = "BarToolbarsListItem1"
        '
        'PrintPreviewSubItem3
        '
        Me.PrintPreviewSubItem3.Caption = "&Background"
        Me.PrintPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background
        Me.PrintPreviewSubItem3.Id = 35
        Me.PrintPreviewSubItem3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem22), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23)})
        Me.PrintPreviewSubItem3.Name = "PrintPreviewSubItem3"
        '
        'PrintPreviewBarCheckItem1
        '
        Me.PrintPreviewBarCheckItem1.BindableChecked = True
        Me.PrintPreviewBarCheckItem1.Caption = "PDF File"
        Me.PrintPreviewBarCheckItem1.Checked = True
        Me.PrintPreviewBarCheckItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf
        Me.PrintPreviewBarCheckItem1.Enabled = False
        Me.PrintPreviewBarCheckItem1.GroupIndex = 2
        Me.PrintPreviewBarCheckItem1.Hint = "PDF File"
        Me.PrintPreviewBarCheckItem1.Id = 40
        Me.PrintPreviewBarCheckItem1.Name = "PrintPreviewBarCheckItem1"
        '
        'PrintPreviewBarCheckItem2
        '
        Me.PrintPreviewBarCheckItem2.Caption = "HTML File"
        Me.PrintPreviewBarCheckItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm
        Me.PrintPreviewBarCheckItem2.Enabled = False
        Me.PrintPreviewBarCheckItem2.GroupIndex = 2
        Me.PrintPreviewBarCheckItem2.Hint = "HTML File"
        Me.PrintPreviewBarCheckItem2.Id = 41
        Me.PrintPreviewBarCheckItem2.Name = "PrintPreviewBarCheckItem2"
        '
        'PrintPreviewBarCheckItem3
        '
        Me.PrintPreviewBarCheckItem3.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht
        Me.PrintPreviewBarCheckItem3.Enabled = False
        Me.PrintPreviewBarCheckItem3.GroupIndex = 2
        Me.PrintPreviewBarCheckItem3.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem3.Id = 42
        Me.PrintPreviewBarCheckItem3.Name = "PrintPreviewBarCheckItem3"
        '
        'PrintPreviewBarCheckItem4
        '
        Me.PrintPreviewBarCheckItem4.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf
        Me.PrintPreviewBarCheckItem4.Enabled = False
        Me.PrintPreviewBarCheckItem4.GroupIndex = 2
        Me.PrintPreviewBarCheckItem4.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem4.Id = 43
        Me.PrintPreviewBarCheckItem4.Name = "PrintPreviewBarCheckItem4"
        '
        'PrintPreviewBarCheckItem5
        '
        Me.PrintPreviewBarCheckItem5.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls
        Me.PrintPreviewBarCheckItem5.Enabled = False
        Me.PrintPreviewBarCheckItem5.GroupIndex = 2
        Me.PrintPreviewBarCheckItem5.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem5.Id = 44
        Me.PrintPreviewBarCheckItem5.Name = "PrintPreviewBarCheckItem5"
        '
        'PrintPreviewBarCheckItem6
        '
        Me.PrintPreviewBarCheckItem6.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx
        Me.PrintPreviewBarCheckItem6.Enabled = False
        Me.PrintPreviewBarCheckItem6.GroupIndex = 2
        Me.PrintPreviewBarCheckItem6.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Id = 45
        Me.PrintPreviewBarCheckItem6.Name = "PrintPreviewBarCheckItem6"
        '
        'PrintPreviewBarCheckItem7
        '
        Me.PrintPreviewBarCheckItem7.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv
        Me.PrintPreviewBarCheckItem7.Enabled = False
        Me.PrintPreviewBarCheckItem7.GroupIndex = 2
        Me.PrintPreviewBarCheckItem7.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem7.Id = 46
        Me.PrintPreviewBarCheckItem7.Name = "PrintPreviewBarCheckItem7"
        '
        'PrintPreviewBarCheckItem8
        '
        Me.PrintPreviewBarCheckItem8.Caption = "Text File"
        Me.PrintPreviewBarCheckItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt
        Me.PrintPreviewBarCheckItem8.Enabled = False
        Me.PrintPreviewBarCheckItem8.GroupIndex = 2
        Me.PrintPreviewBarCheckItem8.Hint = "Text File"
        Me.PrintPreviewBarCheckItem8.Id = 47
        Me.PrintPreviewBarCheckItem8.Name = "PrintPreviewBarCheckItem8"
        '
        'PrintPreviewBarCheckItem9
        '
        Me.PrintPreviewBarCheckItem9.Caption = "Image File"
        Me.PrintPreviewBarCheckItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic
        Me.PrintPreviewBarCheckItem9.Enabled = False
        Me.PrintPreviewBarCheckItem9.GroupIndex = 2
        Me.PrintPreviewBarCheckItem9.Hint = "Image File"
        Me.PrintPreviewBarCheckItem9.Id = 48
        Me.PrintPreviewBarCheckItem9.Name = "PrintPreviewBarCheckItem9"
        '
        'PrintPreviewBarCheckItem10
        '
        Me.PrintPreviewBarCheckItem10.BindableChecked = True
        Me.PrintPreviewBarCheckItem10.Caption = "PDF File"
        Me.PrintPreviewBarCheckItem10.Checked = True
        Me.PrintPreviewBarCheckItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendPdf
        Me.PrintPreviewBarCheckItem10.Enabled = False
        Me.PrintPreviewBarCheckItem10.GroupIndex = 1
        Me.PrintPreviewBarCheckItem10.Hint = "PDF File"
        Me.PrintPreviewBarCheckItem10.Id = 49
        Me.PrintPreviewBarCheckItem10.Name = "PrintPreviewBarCheckItem10"
        '
        'PrintPreviewBarCheckItem11
        '
        Me.PrintPreviewBarCheckItem11.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht
        Me.PrintPreviewBarCheckItem11.Enabled = False
        Me.PrintPreviewBarCheckItem11.GroupIndex = 1
        Me.PrintPreviewBarCheckItem11.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem11.Id = 50
        Me.PrintPreviewBarCheckItem11.Name = "PrintPreviewBarCheckItem11"
        '
        'PrintPreviewBarCheckItem12
        '
        Me.PrintPreviewBarCheckItem12.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf
        Me.PrintPreviewBarCheckItem12.Enabled = False
        Me.PrintPreviewBarCheckItem12.GroupIndex = 1
        Me.PrintPreviewBarCheckItem12.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem12.Id = 51
        Me.PrintPreviewBarCheckItem12.Name = "PrintPreviewBarCheckItem12"
        '
        'PrintPreviewBarCheckItem13
        '
        Me.PrintPreviewBarCheckItem13.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls
        Me.PrintPreviewBarCheckItem13.Enabled = False
        Me.PrintPreviewBarCheckItem13.GroupIndex = 1
        Me.PrintPreviewBarCheckItem13.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem13.Id = 52
        Me.PrintPreviewBarCheckItem13.Name = "PrintPreviewBarCheckItem13"
        '
        'PrintPreviewBarCheckItem14
        '
        Me.PrintPreviewBarCheckItem14.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx
        Me.PrintPreviewBarCheckItem14.Enabled = False
        Me.PrintPreviewBarCheckItem14.GroupIndex = 1
        Me.PrintPreviewBarCheckItem14.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Id = 53
        Me.PrintPreviewBarCheckItem14.Name = "PrintPreviewBarCheckItem14"
        '
        'PrintPreviewBarCheckItem15
        '
        Me.PrintPreviewBarCheckItem15.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv
        Me.PrintPreviewBarCheckItem15.Enabled = False
        Me.PrintPreviewBarCheckItem15.GroupIndex = 1
        Me.PrintPreviewBarCheckItem15.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem15.Id = 54
        Me.PrintPreviewBarCheckItem15.Name = "PrintPreviewBarCheckItem15"
        '
        'PrintPreviewBarCheckItem16
        '
        Me.PrintPreviewBarCheckItem16.Caption = "Text File"
        Me.PrintPreviewBarCheckItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt
        Me.PrintPreviewBarCheckItem16.Enabled = False
        Me.PrintPreviewBarCheckItem16.GroupIndex = 1
        Me.PrintPreviewBarCheckItem16.Hint = "Text File"
        Me.PrintPreviewBarCheckItem16.Id = 55
        Me.PrintPreviewBarCheckItem16.Name = "PrintPreviewBarCheckItem16"
        '
        'PrintPreviewBarCheckItem17
        '
        Me.PrintPreviewBarCheckItem17.Caption = "Image File"
        Me.PrintPreviewBarCheckItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic
        Me.PrintPreviewBarCheckItem17.Enabled = False
        Me.PrintPreviewBarCheckItem17.GroupIndex = 1
        Me.PrintPreviewBarCheckItem17.Hint = "Image File"
        Me.PrintPreviewBarCheckItem17.Id = 56
        Me.PrintPreviewBarCheckItem17.Name = "PrintPreviewBarCheckItem17"
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
        Me.GridColumn31.MinWidth = 23
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.Width = 87
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
        'GridColumn82
        '
        Me.GridColumn82.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn82.Caption = "Selección"
        Me.GridColumn82.FieldName = "Item2"
        Me.GridColumn82.Name = "GridColumn82"
        '
        'GridColumn81
        '
        Me.GridColumn81.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn81.Caption = "Selección"
        Me.GridColumn81.FieldName = "Item2"
        Me.GridColumn81.Name = "GridColumn81"
        '
        'GridColumn80
        '
        Me.GridColumn80.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn80.Caption = "Selección"
        Me.GridColumn80.FieldName = "Item2"
        Me.GridColumn80.Name = "GridColumn80"
        '
        'GridColumn79
        '
        Me.GridColumn79.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn79.Caption = "Selección"
        Me.GridColumn79.FieldName = "Item2"
        Me.GridColumn79.Name = "GridColumn79"
        '
        'GridColumn78
        '
        Me.GridColumn78.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn78.Caption = "Selección"
        Me.GridColumn78.FieldName = "Item2"
        Me.GridColumn78.Name = "GridColumn78"
        '
        'GridColumn77
        '
        Me.GridColumn77.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn77.Caption = "Selección"
        Me.GridColumn77.FieldName = "Item2"
        Me.GridColumn77.Name = "GridColumn77"
        '
        'GridColumn76
        '
        Me.GridColumn76.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn76.Caption = "Selección"
        Me.GridColumn76.FieldName = "Item2"
        Me.GridColumn76.Name = "GridColumn76"
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
        'GridColumn75
        '
        Me.GridColumn75.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn75.Caption = "Selección"
        Me.GridColumn75.FieldName = "Item2"
        Me.GridColumn75.Name = "GridColumn75"
        '
        'GridColumn74
        '
        Me.GridColumn74.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn74.Caption = "Selección"
        Me.GridColumn74.FieldName = "Item2"
        Me.GridColumn74.Name = "GridColumn74"
        '
        'GridColumn73
        '
        Me.GridColumn73.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn73.Caption = "Selección"
        Me.GridColumn73.FieldName = "Item2"
        Me.GridColumn73.Name = "GridColumn73"
        '
        'GridColumn72
        '
        Me.GridColumn72.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn72.Caption = "Selección"
        Me.GridColumn72.FieldName = "Item2"
        Me.GridColumn72.Name = "GridColumn72"
        '
        'GridColumn71
        '
        Me.GridColumn71.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn71.Caption = "Selección"
        Me.GridColumn71.FieldName = "Item2"
        Me.GridColumn71.Name = "GridColumn71"
        '
        'GridColumn70
        '
        Me.GridColumn70.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn70.Caption = "Selección"
        Me.GridColumn70.FieldName = "Item2"
        Me.GridColumn70.Name = "GridColumn70"
        '
        'GridColumn69
        '
        Me.GridColumn69.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn69.Caption = "Selección"
        Me.GridColumn69.FieldName = "Item2"
        Me.GridColumn69.Name = "GridColumn69"
        '
        'GridColumn68
        '
        Me.GridColumn68.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn68.Caption = "Selección"
        Me.GridColumn68.FieldName = "Item2"
        Me.GridColumn68.Name = "GridColumn68"
        '
        'GridColumn67
        '
        Me.GridColumn67.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn67.Caption = "Selección"
        Me.GridColumn67.FieldName = "Item2"
        Me.GridColumn67.Name = "GridColumn67"
        '
        'GridColumn66
        '
        Me.GridColumn66.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn66.Caption = "Selección"
        Me.GridColumn66.FieldName = "Item2"
        Me.GridColumn66.Name = "GridColumn66"
        '
        'GridColumn65
        '
        Me.GridColumn65.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn65.Caption = "Selección"
        Me.GridColumn65.FieldName = "Item2"
        Me.GridColumn65.Name = "GridColumn65"
        '
        'GridColumn64
        '
        Me.GridColumn64.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn64.Caption = "Selección"
        Me.GridColumn64.FieldName = "Item2"
        Me.GridColumn64.Name = "GridColumn64"
        '
        'GridColumn63
        '
        Me.GridColumn63.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn63.Caption = "Selección"
        Me.GridColumn63.FieldName = "Item2"
        Me.GridColumn63.Name = "GridColumn63"
        '
        'GridColumn62
        '
        Me.GridColumn62.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn62.Caption = "Selección"
        Me.GridColumn62.FieldName = "Item2"
        Me.GridColumn62.Name = "GridColumn62"
        '
        'GridColumn61
        '
        Me.GridColumn61.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn61.Caption = "Selección"
        Me.GridColumn61.FieldName = "Item2"
        Me.GridColumn61.Name = "GridColumn61"
        '
        'GridColumn60
        '
        Me.GridColumn60.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn60.Caption = "Selección"
        Me.GridColumn60.FieldName = "Item2"
        Me.GridColumn60.Name = "GridColumn60"
        '
        'GridColumn59
        '
        Me.GridColumn59.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn59.Caption = "Selección"
        Me.GridColumn59.FieldName = "Item2"
        Me.GridColumn59.Name = "GridColumn59"
        '
        'GridColumn58
        '
        Me.GridColumn58.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn58.Caption = "Selección"
        Me.GridColumn58.FieldName = "Item2"
        Me.GridColumn58.Name = "GridColumn58"
        '
        'GridColumn57
        '
        Me.GridColumn57.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn57.Caption = "Selección"
        Me.GridColumn57.FieldName = "Item2"
        Me.GridColumn57.Name = "GridColumn57"
        '
        'GridColumn56
        '
        Me.GridColumn56.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn56.Caption = "Selección"
        Me.GridColumn56.FieldName = "Item2"
        Me.GridColumn56.Name = "GridColumn56"
        '
        'GridColumn55
        '
        Me.GridColumn55.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn55.Caption = "Selección"
        Me.GridColumn55.FieldName = "Item2"
        Me.GridColumn55.Name = "GridColumn55"
        '
        'GridColumn54
        '
        Me.GridColumn54.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn54.Caption = "Selección"
        Me.GridColumn54.FieldName = "Item2"
        Me.GridColumn54.Name = "GridColumn54"
        '
        'GridColumn53
        '
        Me.GridColumn53.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn53.Caption = "Selección"
        Me.GridColumn53.FieldName = "Item2"
        Me.GridColumn53.Name = "GridColumn53"
        '
        'GridColumn52
        '
        Me.GridColumn52.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn52.Caption = "Selección"
        Me.GridColumn52.FieldName = "Item2"
        Me.GridColumn52.Name = "GridColumn52"
        '
        'GridColumn51
        '
        Me.GridColumn51.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn51.Caption = "Selección"
        Me.GridColumn51.FieldName = "Item2"
        Me.GridColumn51.Name = "GridColumn51"
        '
        'GridColumn50
        '
        Me.GridColumn50.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn50.Caption = "Selección"
        Me.GridColumn50.FieldName = "Item2"
        Me.GridColumn50.Name = "GridColumn50"
        '
        'GridColumn49
        '
        Me.GridColumn49.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn49.Caption = "Selección"
        Me.GridColumn49.FieldName = "Item2"
        Me.GridColumn49.Name = "GridColumn49"
        '
        'GridColumn48
        '
        Me.GridColumn48.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn48.Caption = "Selección"
        Me.GridColumn48.FieldName = "Item2"
        Me.GridColumn48.Name = "GridColumn48"
        '
        'GridColumn47
        '
        Me.GridColumn47.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn47.Caption = "Selección"
        Me.GridColumn47.FieldName = "Item2"
        Me.GridColumn47.Name = "GridColumn47"
        '
        'GridColumn46
        '
        Me.GridColumn46.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn46.Caption = "Selección"
        Me.GridColumn46.FieldName = "Item2"
        Me.GridColumn46.Name = "GridColumn46"
        '
        'GridColumn45
        '
        Me.GridColumn45.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn45.Caption = "Selección"
        Me.GridColumn45.FieldName = "Item2"
        Me.GridColumn45.Name = "GridColumn45"
        '
        'GridColumn44
        '
        Me.GridColumn44.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn44.Caption = "Selección"
        Me.GridColumn44.FieldName = "Item2"
        Me.GridColumn44.Name = "GridColumn44"
        '
        'GridColumn43
        '
        Me.GridColumn43.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn43.Caption = "Selección"
        Me.GridColumn43.FieldName = "Item2"
        Me.GridColumn43.Name = "GridColumn43"
        '
        'GridColumn42
        '
        Me.GridColumn42.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn42.Caption = "Selección"
        Me.GridColumn42.FieldName = "Item2"
        Me.GridColumn42.Name = "GridColumn42"
        '
        'GridColumn37
        '
        Me.GridColumn37.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn37.Caption = "Selección"
        Me.GridColumn37.FieldName = "Item2"
        Me.GridColumn37.Name = "GridColumn37"
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn14.Caption = "Selección"
        Me.GridColumn14.FieldName = "Item2"
        Me.GridColumn14.Name = "GridColumn14"
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
        'GridColumn8
        '
        Me.GridColumn8.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn8.Caption = "Selección"
        Me.GridColumn8.FieldName = "Item2"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.Caption = "Selección"
        Me.GridColumn7.FieldName = "Item2"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.Caption = "Selección"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.Caption = "Selección"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.Caption = "Selección"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.Caption = "Selección"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.Caption = "Selección"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.Caption = "Selección"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'INDGcExportExcell
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcExportExcell, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcExportExcell, Nothing)
        Me.INDGcExportExcell.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcExportExcell, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcExportExcell, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcExportExcell, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcExportExcell, False)
        Me.INDGcExportExcell.Location = New System.Drawing.Point(334, 452)
        Me.INDGcExportExcell.MainView = Me.GridView1
        Me.INDGcExportExcell.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGcExportExcell.MenuManager = Me.DocumentViewerBarManager1
        Me.INDGcExportExcell.Name = "INDGcExportExcell"
        Me.INDGcExportExcell.Size = New System.Drawing.Size(616, 302)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcExportExcell, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcExportExcell.TabIndex = 40
        Me.INDGcExportExcell.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        Me.INDGcExportExcell.Visible = False
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn34, Me.GridColumn35, Me.GridColumn36, Me.GridColumn38, Me.GridColumn39, Me.GridColumn40, Me.GridColumn41, Me.GridColumn83, Me.GridColumn84, Me.GridColumn85, Me.GridColumn86})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.INDGcExportExcell
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "GridColumn34"
        Me.GridColumn34.MinWidth = 150
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.Visible = True
        Me.GridColumn34.VisibleIndex = 0
        Me.GridColumn34.Width = 150
        '
        'GridColumn35
        '
        Me.GridColumn35.Caption = "GridColumn35"
        Me.GridColumn35.MinWidth = 150
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Visible = True
        Me.GridColumn35.VisibleIndex = 1
        Me.GridColumn35.Width = 150
        '
        'GridColumn36
        '
        Me.GridColumn36.Caption = "GridColumn36"
        Me.GridColumn36.MinWidth = 150
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 2
        Me.GridColumn36.Width = 150
        '
        'GridColumn38
        '
        Me.GridColumn38.Caption = "GridColumn38"
        Me.GridColumn38.MinWidth = 150
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 3
        Me.GridColumn38.Width = 150
        '
        'GridColumn39
        '
        Me.GridColumn39.Caption = "GridColumn39"
        Me.GridColumn39.MinWidth = 150
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.Visible = True
        Me.GridColumn39.VisibleIndex = 4
        Me.GridColumn39.Width = 150
        '
        'GridColumn40
        '
        Me.GridColumn40.Caption = "GridColumn40"
        Me.GridColumn40.MinWidth = 150
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.Visible = True
        Me.GridColumn40.VisibleIndex = 5
        Me.GridColumn40.Width = 150
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "GridColumn41"
        Me.GridColumn41.MinWidth = 150
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.Visible = True
        Me.GridColumn41.VisibleIndex = 6
        Me.GridColumn41.Width = 150
        '
        'GridColumn83
        '
        Me.GridColumn83.Caption = "GridColumn83"
        Me.GridColumn83.MinWidth = 150
        Me.GridColumn83.Name = "GridColumn83"
        Me.GridColumn83.Visible = True
        Me.GridColumn83.VisibleIndex = 7
        Me.GridColumn83.Width = 150
        '
        'GridColumn84
        '
        Me.GridColumn84.Caption = "GridColumn84"
        Me.GridColumn84.MinWidth = 150
        Me.GridColumn84.Name = "GridColumn84"
        Me.GridColumn84.Visible = True
        Me.GridColumn84.VisibleIndex = 8
        Me.GridColumn84.Width = 150
        '
        'GridColumn85
        '
        Me.GridColumn85.Caption = "GridColumn85"
        Me.GridColumn85.MinWidth = 150
        Me.GridColumn85.Name = "GridColumn85"
        Me.GridColumn85.Visible = True
        Me.GridColumn85.VisibleIndex = 9
        Me.GridColumn85.Width = 150
        '
        'GridColumn86
        '
        Me.GridColumn86.Caption = "GridColumn86"
        Me.GridColumn86.MinWidth = 100
        Me.GridColumn86.Name = "GridColumn86"
        Me.GridColumn86.Visible = True
        Me.GridColumn86.VisibleIndex = 10
        Me.GridColumn86.Width = 100
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDLciAccountsZero
        '
        Me.INDLciAccountsZero.CustomizationFormText = "Cuentas en Cero:"
        Me.INDLciAccountsZero.Location = New System.Drawing.Point(0, 95)
        Me.INDLciAccountsZero.MaxSize = New System.Drawing.Size(360, 65)
        Me.INDLciAccountsZero.MinSize = New System.Drawing.Size(360, 65)
        Me.INDLciAccountsZero.Name = "INDLciAccountsZero"
        Me.INDLciAccountsZero.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 12, 2)
        Me.INDLciAccountsZero.Size = New System.Drawing.Size(360, 522)
        Me.INDLciAccountsZero.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAccountsZero.Text = "Cuentas en Cero:"
        Me.INDLciAccountsZero.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAccountsZero.TextSize = New System.Drawing.Size(116, 21)
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn13.Caption = "Selección"
        Me.GridColumn13.FieldName = "Item2"
        Me.GridColumn13.Name = "GridColumn13"
        '
        'INDPcDocumentViewer
        '
        Me.INDPcDocumentViewer.Controls.Add(Me.INDDvDocumentViewer)
        Me.INDPcDocumentViewer.Controls.Add(Me.INDCnBack)
        Me.INDPcDocumentViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcDocumentViewer.Location = New System.Drawing.Point(2, 9)
        Me.INDPcDocumentViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPcDocumentViewer.Name = "INDPcDocumentViewer"
        Me.INDPcDocumentViewer.Size = New System.Drawing.Size(1344, 911)
        Me.INDPcDocumentViewer.TabIndex = 2
        '
        'INDCnBack
        '
        Me.INDCnBack.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnBack.Location = New System.Drawing.Point(2, 2)
        Me.INDCnBack.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDCnBack.Name = "INDCnBack"
        Me.INDCnBack.Size = New System.Drawing.Size(82, 907)
        Me.INDCnBack.TabIndex = 0
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmReportValuedInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1348, 929)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "FrmReportValuedInventory"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "1404"
        Me.Text = "Inventario Valorizado"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeSubGroupHandlesBatchEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeSubGroupHandlesExpiryEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDsleAccountsZero.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAccountsZero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit3View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleOrderBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleValorization.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSubGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSubGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValorization, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAmountsZero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblSubGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblSourceWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDDvDocumentViewer.ResumeLayout(False)
        Me.INDDvDocumentViewer.PerformLayout()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAccountsZero, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcDocumentViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcDocumentViewer.ResumeLayout(False)
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDCncNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDGleTypeReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents GridLookUpEdit3View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDGleOrderBy As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleValorization As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgFilterRequired As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciTypeReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciOrderBy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValorization As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDLcgFilterOptional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciAccountsZero As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleAccountsZero As Presentation.Controls.CtrYesNo
    Friend WithEvents CtrYesNo2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciAmountsZero As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPcDocumentViewer As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDCnBack As Presentation.Controls.CtrNavigation
    Friend WithEvents INDDvDocumentViewer As DevExpress.XtraPrinting.Preview.DocumentViewer
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents DocumentViewerBarManager1 As DevExpress.XtraPrinting.Preview.DocumentViewerBarManager
    Friend WithEvents PreviewBar1 As DevExpress.XtraPrinting.Preview.PreviewBar
    Friend WithEvents PrintPreviewBarItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem5 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem6 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem7 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem8 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem9 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem10 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem11 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem12 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem13 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem14 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem15 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents ZoomBarEditItem1 As DevExpress.XtraPrinting.Preview.ZoomBarEditItem
    Friend WithEvents PrintPreviewRepositoryItemComboBox1 As DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox
    Friend WithEvents PrintPreviewBarItem16 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem17 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem18 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem19 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem20 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem21 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem22 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem23 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem24 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem25 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem26 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PreviewBar2 As DevExpress.XtraPrinting.Preview.PreviewBar
    Friend WithEvents PrintPreviewStaticItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents ProgressBarEditItem1 As DevExpress.XtraPrinting.Preview.ProgressBarEditItem
    Friend WithEvents RepositoryItemProgressBar1 As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents PrintPreviewBarItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PrintPreviewStaticItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem
    Friend WithEvents ZoomTrackBarEditItem1 As DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem
    Friend WithEvents RepositoryItemZoomTrackBar1 As DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar
    Friend WithEvents PrintPreviewSubItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewSubItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewSubItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewBarItem27 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem28 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents BarToolbarsListItem1 As DevExpress.XtraBars.BarToolbarsListItem
    Friend WithEvents PrintPreviewSubItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents PrintPreviewBarCheckItem1 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem2 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem4 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem5 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem6 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem7 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem8 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem9 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem10 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem11 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem12 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem13 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem14 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem15 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem16 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents PrintPreviewBarCheckItem17 As DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGrcTypeReport As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGrcOrderBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGrcValorization As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCbeSubGroupHandlesBatchEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeSubGroupHandlesExpiryEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
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
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
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
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn77 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn78 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleSubGroup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvSubGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColSubGroup_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColExpiration As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLblSubGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn79 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProduct As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProduct_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLblProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn80 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductSubGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleGroup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColGroup_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColGroupCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColGroupName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLblGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn81 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn82 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvWarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColSourceWareHouse_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSourceWarehouseCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSourceWarehouseName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLblSourceWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDsleCurrency As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn178 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn761 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn771 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyCurrency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbGenerateExcell As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn89 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcExportExcell As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn83 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn84 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn85 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn86 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn87 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn88 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn90 As DevExpress.XtraGrid.Columns.GridColumn
End Class
