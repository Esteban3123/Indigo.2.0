Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportConsumtionAverage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportConsumtionAverage))
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject11 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject12 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject13 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject14 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions4 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject13 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject14 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject15 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject16 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDCncNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDDateEnd1 = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleWarehouse = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvWarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColWareHouse_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWarehouseCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWarehouseName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateStart1 = New DevExpress.XtraEditors.DateEdit()
        Me.INDGcExportExcel = New DevExpress.XtraGrid.GridControl()
        Me.INDGvExportExcel = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Producto = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Almacen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Proveedor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AÑO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ENE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FEB = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MAR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ABR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MAY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.JUN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.JUL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AGO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SEP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.OCT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.NOV = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DIC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Promedio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.NombreProducto = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.PrintPreviewBarItem16 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.ZoomBarEditItem1 = New DevExpress.XtraPrinting.Preview.ZoomBarEditItem()
        Me.PrintPreviewRepositoryItemComboBox1 = New DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox()
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
        Me.PrintPreviewBarItem27 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
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
        Me.PrintPreviewBarItem28 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
        Me.PrintPreviewBarItem29 = New DevExpress.XtraPrinting.Preview.PrintPreviewBarItem()
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
        Me.INDSbExportExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGleReportType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGleState = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleSupplier = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INGvSupplier = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColSupplier_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplierDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSupplierStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvProduct = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProducto_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductoCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductoName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilterRequired = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciReportType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGcExportExcel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciOrderType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgFilterOptional = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblWarehouse = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblSupplier = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLblProducto = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDCbeSubGroupHandlesBatchEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeSubGroupHandlesExpiryEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbePersonTypeEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeContributionTypeEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeRetentionTypeEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeThirdPartyStatusEnd = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDGvProduct1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProduct_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvProduct2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProduct_UnboundSelection1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductCode1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductName1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvProduct3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProduct_UnboundSelection2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductCode2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductName2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvProduct4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColProduct_UnboundSelection3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductCode3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColProductName3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.INDSleProduct1 = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSleProduct2 = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSleProduct3 = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDSleProduct4 = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDPcDocumentViewer = New DevExpress.XtraEditors.PanelControl()
        Me.INDCnBack = New Presentation.Controls.CtrNavigation()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.INDCbePersonTypeEnd1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeContributionTypeEnd1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeRetentionTypeEnd1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDCbeThirdPartyStatusEnd1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.INDLblProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProduct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDestinationStore = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDDateEnd1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDDvDocumentViewer.SuspendLayout()
        CType(Me.INDGleReportType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleState.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleSupplier.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INGvSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGcExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciOrderType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeSubGroupHandlesBatchEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeSubGroupHandlesExpiryEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbePersonTypeEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeContributionTypeEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeRetentionTypeEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeThirdPartyStatusEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProduct4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProduct1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProduct2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProduct3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProduct4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcDocumentViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcDocumentViewer.SuspendLayout()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbePersonTypeEnd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeContributionTypeEnd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeRetentionTypeEnd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeThirdPartyStatusEnd1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLblProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDestinationStore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCncNavigation)
        Me.INDPanelControlBase.Controls.Add(Me.INDPcDocumentViewer)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 10)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1512, 1055)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(9, 9, 9, 9)
        Me.BarraBotones.Size = New System.Drawing.Size(2176, 130)
        '
        'INDCncNavigation
        '
        Me.INDCncNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCncNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCncNavigation.LayoutControl = Me.INDLcBase
        Me.INDCncNavigation.Location = New System.Drawing.Point(2, 12)
        Me.INDCncNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCncNavigation.Name = "INDCncNavigation"
        Me.INDCncNavigation.Size = New System.Drawing.Size(200, 1041)
        Me.INDCncNavigation.TabIndex = 0
        Me.INDCncNavigation.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDDateEnd1)
        Me.INDLcBase.Controls.Add(Me.INDSleWarehouse)
        Me.INDLcBase.Controls.Add(Me.INDDateStart1)
        Me.INDLcBase.Controls.Add(Me.INDGcExportExcel)
        Me.INDLcBase.Controls.Add(Me.INDSbExportExcel)
        Me.INDLcBase.Controls.Add(Me.INDGleReportType)
        Me.INDLcBase.Controls.Add(Me.INDSbGenerateReport)
        Me.INDLcBase.Controls.Add(Me.INDGleState)
        Me.INDLcBase.Controls.Add(Me.INDSleSupplier)
        Me.INDLcBase.Controls.Add(Me.INDSleProduct)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 12)
        Me.INDLcBase.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.LayoutControlGroup1
        Me.INDLcBase.Size = New System.Drawing.Size(1308, 1041)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDDateEnd1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDateEnd1, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateEnd1, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDateEnd1, True)
        Me.INDDateEnd1.EditValue = Nothing
        Me.INDDateEnd1.EnterMoveNextControl = True
        Me.INDDateEnd1.Location = New System.Drawing.Point(35, 193)
        Me.INDDateEnd1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDDateEnd1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateEnd1, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDateEnd1.MaximumSize = New System.Drawing.Size(534, 41)
        Me.INDDateEnd1.MinimumSize = New System.Drawing.Size(534, 41)
        Me.INDDateEnd1.Name = "INDDateEnd1"
        Me.INDDateEnd1.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDateEnd1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd1.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDDateEnd1.Properties.Appearance.Options.UseBackColor = True
        Me.INDDateEnd1.Properties.Appearance.Options.UseFont = True
        Me.INDDateEnd1.Properties.Appearance.Options.UseForeColor = True
        Me.INDDateEnd1.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd1.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateEnd1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd1.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDateEnd1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEnd1.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEnd1.Properties.MaxValue = New Date(9999, 12, 31, 23, 59, 0, 0)
        Me.INDDateEnd1.Size = New System.Drawing.Size(534, 38)
        Me.INDDateEnd1.StyleController = Me.INDLcBase
        Me.INDDateEnd1.TabIndex = 27
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDateEnd1, 0)
        Me.INDDateEnd1.ToolTip = "Este Campo es Necesario"
        '
        'INDSleWarehouse
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleWarehouse, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleWarehouse, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleWarehouse, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleWarehouse, False)
        Me.INDSleWarehouse.Location = New System.Drawing.Point(609, 111)
        Me.INDSleWarehouse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleWarehouse, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleWarehouse.Name = "INDSleWarehouse"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleWarehouse, False)
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
        Me.INDSleWarehouse.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleWarehouse, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleWarehouse, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleWarehouse, True)
        Me.INDSleWarehouse.Size = New System.Drawing.Size(558, 38)
        Me.INDSleWarehouse.StyleController = Me.INDLcBase
        Me.INDSleWarehouse.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleWarehouse, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleWarehouse, 0)
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
        Me.INDGvWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColWareHouse_UnboundSelection, Me.INDColWarehouseCode, Me.INDColWarehouseName})
        Me.INDGvWarehouse.DetailHeight = 512
        Me.INDGvWarehouse.FixedLineWidth = 3
        Me.INDGvWarehouse.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvWarehouse.Name = "INDGvWarehouse"
        Me.INDGvWarehouse.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvWarehouse.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvWarehouse.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvWarehouse.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvWarehouse.OptionsView.ShowAutoFilterRow = True
        Me.INDGvWarehouse.OptionsView.ShowDetailButtons = False
        Me.INDGvWarehouse.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvWarehouse, False)
        '
        'INDColWareHouse_UnboundSelection
        '
        Me.INDColWareHouse_UnboundSelection.Caption = " "
        Me.INDColWareHouse_UnboundSelection.FieldName = "INDColWareHouse_UnboundSelection"
        Me.INDColWareHouse_UnboundSelection.MinWidth = 30
        Me.INDColWareHouse_UnboundSelection.Name = "INDColWareHouse_UnboundSelection"
        Me.INDColWareHouse_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColWareHouse_UnboundSelection.Visible = True
        Me.INDColWareHouse_UnboundSelection.VisibleIndex = 0
        Me.INDColWareHouse_UnboundSelection.Width = 30
        '
        'INDColWarehouseCode
        '
        Me.INDColWarehouseCode.Caption = "Código"
        Me.INDColWarehouseCode.FieldName = "Code"
        Me.INDColWarehouseCode.MinWidth = 30
        Me.INDColWarehouseCode.Name = "INDColWarehouseCode"
        Me.INDColWarehouseCode.OptionsColumn.AllowEdit = False
        Me.INDColWarehouseCode.OptionsColumn.AllowFocus = False
        Me.INDColWarehouseCode.Visible = True
        Me.INDColWarehouseCode.VisibleIndex = 1
        Me.INDColWarehouseCode.Width = 112
        '
        'INDColWarehouseName
        '
        Me.INDColWarehouseName.Caption = "Nombre"
        Me.INDColWarehouseName.FieldName = "Name"
        Me.INDColWarehouseName.MinWidth = 30
        Me.INDColWarehouseName.Name = "INDColWarehouseName"
        Me.INDColWarehouseName.OptionsColumn.AllowEdit = False
        Me.INDColWarehouseName.OptionsColumn.AllowFocus = False
        Me.INDColWarehouseName.Visible = True
        Me.INDColWarehouseName.VisibleIndex = 2
        Me.INDColWarehouseName.Width = 112
        '
        'INDDateStart1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDateStart1, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateStart1, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDateStart1, True)
        Me.INDDateStart1.EditValue = Nothing
        Me.INDDateStart1.EnterMoveNextControl = True
        Me.INDDateStart1.Location = New System.Drawing.Point(35, 111)
        Me.INDDateStart1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDDateStart1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateStart1, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDateStart1.MaximumSize = New System.Drawing.Size(534, 41)
        Me.INDDateStart1.MinimumSize = New System.Drawing.Size(534, 41)
        Me.INDDateStart1.Name = "INDDateStart1"
        Me.INDDateStart1.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDateStart1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart1.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDDateStart1.Properties.Appearance.Options.UseBackColor = True
        Me.INDDateStart1.Properties.Appearance.Options.UseFont = True
        Me.INDDateStart1.Properties.Appearance.Options.UseForeColor = True
        Me.INDDateStart1.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart1.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateStart1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart1.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDDateStart1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateStart1.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateStart1.Properties.MaxValue = New Date(9999, 12, 31, 23, 59, 0, 0)
        Me.INDDateStart1.Size = New System.Drawing.Size(534, 38)
        Me.INDDateStart1.StyleController = Me.INDLcBase
        Me.INDDateStart1.TabIndex = 24
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDateStart1, 0)
        Me.INDDateStart1.ToolTip = "Este Campo es Necesario"
        '
        'INDGcExportExcel
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcExportExcel, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcExportExcel, Nothing)
        Me.INDGcExportExcel.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcExportExcel, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcExportExcel, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcExportExcel, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcExportExcel, False)
        Me.INDGcExportExcel.Location = New System.Drawing.Point(35, 404)
        Me.INDGcExportExcel.MainView = Me.INDGvExportExcel
        Me.INDGcExportExcel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDGcExportExcel.MenuManager = Me.DocumentViewerBarManager1
        Me.INDGcExportExcel.Name = "INDGcExportExcel"
        Me.INDGcExportExcel.Size = New System.Drawing.Size(534, 602)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcExportExcel, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcExportExcel.TabIndex = 22
        Me.INDGcExportExcel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvExportExcel})
        '
        'INDGvExportExcel
        '
        Me.INDGvExportExcel.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvExportExcel.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvExportExcel.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvExportExcel.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvExportExcel.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvExportExcel.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvExportExcel.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvExportExcel.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvExportExcel.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvExportExcel.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvExportExcel.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvExportExcel.Appearance.Row.Options.UseFont = True
        Me.INDGvExportExcel.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvExportExcel.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvExportExcel.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Producto, Me.Almacen, Me.Proveedor, Me.AÑO, Me.ENE, Me.FEB, Me.MAR, Me.ABR, Me.MAY, Me.JUN, Me.JUL, Me.AGO, Me.SEP, Me.OCT, Me.NOV, Me.DIC, Me.Promedio, Me.NombreProducto})
        Me.INDGvExportExcel.DetailHeight = 512
        Me.INDGvExportExcel.FixedLineWidth = 3
        Me.INDGvExportExcel.GridControl = Me.INDGcExportExcel
        Me.INDGvExportExcel.GroupCount = 2
        Me.INDGvExportExcel.Name = "INDGvExportExcel"
        Me.INDGvExportExcel.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvExportExcel.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvExportExcel.OptionsView.ShowAutoFilterRow = True
        Me.INDGvExportExcel.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.Producto, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.Almacen, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvExportExcel, False)
        '
        'Producto
        '
        Me.Producto.Caption = "Producto"
        Me.Producto.FieldName = "ProductId"
        Me.Producto.MinWidth = 30
        Me.Producto.Name = "Producto"
        Me.Producto.Tag = "0"
        Me.Producto.Visible = True
        Me.Producto.VisibleIndex = 3
        Me.Producto.Width = 112
        '
        'Almacen
        '
        Me.Almacen.Caption = "Almacen"
        Me.Almacen.FieldName = "WarehouseName"
        Me.Almacen.MinWidth = 30
        Me.Almacen.Name = "Almacen"
        Me.Almacen.Tag = "1"
        Me.Almacen.Visible = True
        Me.Almacen.VisibleIndex = 0
        Me.Almacen.Width = 112
        '
        'Proveedor
        '
        Me.Proveedor.Caption = "Proveedor"
        Me.Proveedor.FieldName = "SupplierName"
        Me.Proveedor.MinWidth = 30
        Me.Proveedor.Name = "Proveedor"
        Me.Proveedor.Tag = "2"
        Me.Proveedor.Visible = True
        Me.Proveedor.VisibleIndex = 1
        Me.Proveedor.Width = 30
        '
        'AÑO
        '
        Me.AÑO.Caption = "AÑO"
        Me.AÑO.FieldName = "ANIO"
        Me.AÑO.MinWidth = 30
        Me.AÑO.Name = "AÑO"
        Me.AÑO.Tag = "3"
        Me.AÑO.Visible = True
        Me.AÑO.VisibleIndex = 2
        Me.AÑO.Width = 30
        '
        'ENE
        '
        Me.ENE.Caption = "ENE"
        Me.ENE.FieldName = "ENE"
        Me.ENE.MinWidth = 30
        Me.ENE.Name = "ENE"
        Me.ENE.Visible = True
        Me.ENE.VisibleIndex = 3
        Me.ENE.Width = 30
        '
        'FEB
        '
        Me.FEB.Caption = "FEB"
        Me.FEB.FieldName = "FEB"
        Me.FEB.MinWidth = 30
        Me.FEB.Name = "FEB"
        Me.FEB.Visible = True
        Me.FEB.VisibleIndex = 5
        Me.FEB.Width = 30
        '
        'MAR
        '
        Me.MAR.Caption = "MAR"
        Me.MAR.FieldName = "MAR"
        Me.MAR.MinWidth = 30
        Me.MAR.Name = "MAR"
        Me.MAR.Visible = True
        Me.MAR.VisibleIndex = 4
        Me.MAR.Width = 30
        '
        'ABR
        '
        Me.ABR.Caption = "ABR"
        Me.ABR.FieldName = "ABR"
        Me.ABR.MinWidth = 30
        Me.ABR.Name = "ABR"
        Me.ABR.Visible = True
        Me.ABR.VisibleIndex = 6
        Me.ABR.Width = 30
        '
        'MAY
        '
        Me.MAY.Caption = "MAY"
        Me.MAY.FieldName = "MAY"
        Me.MAY.MinWidth = 30
        Me.MAY.Name = "MAY"
        Me.MAY.Visible = True
        Me.MAY.VisibleIndex = 7
        Me.MAY.Width = 30
        '
        'JUN
        '
        Me.JUN.Caption = "JUN"
        Me.JUN.FieldName = "JUN"
        Me.JUN.MinWidth = 30
        Me.JUN.Name = "JUN"
        Me.JUN.Visible = True
        Me.JUN.VisibleIndex = 8
        Me.JUN.Width = 30
        '
        'JUL
        '
        Me.JUL.Caption = "JUL"
        Me.JUL.FieldName = "JUL"
        Me.JUL.MinWidth = 30
        Me.JUL.Name = "JUL"
        Me.JUL.Visible = True
        Me.JUL.VisibleIndex = 9
        Me.JUL.Width = 30
        '
        'AGO
        '
        Me.AGO.Caption = "AGO"
        Me.AGO.FieldName = "AGO"
        Me.AGO.MinWidth = 30
        Me.AGO.Name = "AGO"
        Me.AGO.Visible = True
        Me.AGO.VisibleIndex = 10
        Me.AGO.Width = 30
        '
        'SEP
        '
        Me.SEP.Caption = "SEP"
        Me.SEP.FieldName = "SEP"
        Me.SEP.MinWidth = 30
        Me.SEP.Name = "SEP"
        Me.SEP.Visible = True
        Me.SEP.VisibleIndex = 11
        Me.SEP.Width = 30
        '
        'OCT
        '
        Me.OCT.Caption = "OCT"
        Me.OCT.FieldName = "OCT"
        Me.OCT.MinWidth = 30
        Me.OCT.Name = "OCT"
        Me.OCT.Visible = True
        Me.OCT.VisibleIndex = 12
        Me.OCT.Width = 30
        '
        'NOV
        '
        Me.NOV.Caption = "NOV"
        Me.NOV.FieldName = "NOV"
        Me.NOV.MinWidth = 30
        Me.NOV.Name = "NOV"
        Me.NOV.Visible = True
        Me.NOV.VisibleIndex = 13
        Me.NOV.Width = 30
        '
        'DIC
        '
        Me.DIC.Caption = "DIC"
        Me.DIC.FieldName = "DIC"
        Me.DIC.MinWidth = 30
        Me.DIC.Name = "DIC"
        Me.DIC.Visible = True
        Me.DIC.VisibleIndex = 14
        Me.DIC.Width = 30
        '
        'Promedio
        '
        Me.Promedio.Caption = "Promedio"
        Me.Promedio.FieldName = "Average"
        Me.Promedio.MinWidth = 30
        Me.Promedio.Name = "Promedio"
        Me.Promedio.Visible = True
        Me.Promedio.VisibleIndex = 15
        Me.Promedio.Width = 30
        '
        'NombreProducto
        '
        Me.NombreProducto.Caption = "NombreProducto"
        Me.NombreProducto.FieldName = "ProductName"
        Me.NombreProducto.MinWidth = 30
        Me.NombreProducto.Name = "NombreProducto"
        Me.NombreProducto.Visible = True
        Me.NombreProducto.VisibleIndex = 0
        Me.NombreProducto.Width = 189
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
        Me.DocumentViewerBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.PrintPreviewStaticItem1, Me.BarStaticItem1, Me.ProgressBarEditItem1, Me.PrintPreviewBarItem1, Me.BarButtonItem1, Me.PrintPreviewStaticItem2, Me.ZoomTrackBarEditItem1, Me.PrintPreviewBarItem2, Me.PrintPreviewBarItem3, Me.PrintPreviewBarItem4, Me.PrintPreviewBarItem5, Me.PrintPreviewBarItem6, Me.PrintPreviewBarItem7, Me.PrintPreviewBarItem8, Me.PrintPreviewBarItem9, Me.PrintPreviewBarItem10, Me.PrintPreviewBarItem11, Me.PrintPreviewBarItem12, Me.PrintPreviewBarItem13, Me.PrintPreviewBarItem14, Me.PrintPreviewBarItem15, Me.PrintPreviewBarItem16, Me.ZoomBarEditItem1, Me.PrintPreviewBarItem17, Me.PrintPreviewBarItem18, Me.PrintPreviewBarItem19, Me.PrintPreviewBarItem20, Me.PrintPreviewBarItem21, Me.PrintPreviewBarItem22, Me.PrintPreviewBarItem23, Me.PrintPreviewBarItem24, Me.PrintPreviewBarItem25, Me.PrintPreviewBarItem26, Me.PrintPreviewBarItem27, Me.PrintPreviewSubItem1, Me.PrintPreviewSubItem2, Me.PrintPreviewSubItem3, Me.PrintPreviewSubItem4, Me.PrintPreviewBarItem28, Me.PrintPreviewBarItem29, Me.BarToolbarsListItem1, Me.PrintPreviewBarCheckItem1, Me.PrintPreviewBarCheckItem2, Me.PrintPreviewBarCheckItem3, Me.PrintPreviewBarCheckItem4, Me.PrintPreviewBarCheckItem5, Me.PrintPreviewBarCheckItem6, Me.PrintPreviewBarCheckItem7, Me.PrintPreviewBarCheckItem8, Me.PrintPreviewBarCheckItem9, Me.PrintPreviewBarCheckItem10, Me.PrintPreviewBarCheckItem11, Me.PrintPreviewBarCheckItem12, Me.PrintPreviewBarCheckItem13, Me.PrintPreviewBarCheckItem14, Me.PrintPreviewBarCheckItem15, Me.PrintPreviewBarCheckItem16, Me.PrintPreviewBarCheckItem17})
        Me.DocumentViewerBarManager1.MaxItemId = 58
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
        Me.PreviewBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem4), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem5), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem6, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem7, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem12), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem13), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem14, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem15), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem16, True), New DevExpress.XtraBars.LinkPersistInfo(Me.ZoomBarEditItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem17), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem18, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem19), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem20), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem21), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem22, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem27, True)})
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
        Me.PrintPreviewBarItem4.Caption = "Thumbnails"
        Me.PrintPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Thumbnails
        Me.PrintPreviewBarItem4.Enabled = False
        Me.PrintPreviewBarItem4.Hint = "Thumbnails"
        Me.PrintPreviewBarItem4.Id = 9
        Me.PrintPreviewBarItem4.ImageOptions.ImageIndex = 23
        Me.PrintPreviewBarItem4.Name = "PrintPreviewBarItem4"
        '
        'PrintPreviewBarItem5
        '
        Me.PrintPreviewBarItem5.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem5.Caption = "Search"
        Me.PrintPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find
        Me.PrintPreviewBarItem5.Enabled = False
        Me.PrintPreviewBarItem5.Hint = "Search"
        Me.PrintPreviewBarItem5.Id = 10
        Me.PrintPreviewBarItem5.ImageOptions.ImageIndex = 20
        Me.PrintPreviewBarItem5.Name = "PrintPreviewBarItem5"
        '
        'PrintPreviewBarItem6
        '
        Me.PrintPreviewBarItem6.Caption = "Customize"
        Me.PrintPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize
        Me.PrintPreviewBarItem6.Enabled = False
        Me.PrintPreviewBarItem6.Hint = "Customize"
        Me.PrintPreviewBarItem6.Id = 11
        Me.PrintPreviewBarItem6.ImageOptions.ImageIndex = 14
        Me.PrintPreviewBarItem6.Name = "PrintPreviewBarItem6"
        '
        'PrintPreviewBarItem7
        '
        Me.PrintPreviewBarItem7.Caption = "Open"
        Me.PrintPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open
        Me.PrintPreviewBarItem7.Enabled = False
        Me.PrintPreviewBarItem7.Hint = "Open a document"
        Me.PrintPreviewBarItem7.Id = 12
        Me.PrintPreviewBarItem7.ImageOptions.ImageIndex = 24
        Me.PrintPreviewBarItem7.Name = "PrintPreviewBarItem7"
        '
        'PrintPreviewBarItem8
        '
        Me.PrintPreviewBarItem8.Caption = "Save"
        Me.PrintPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save
        Me.PrintPreviewBarItem8.Enabled = False
        Me.PrintPreviewBarItem8.Hint = "Save the document"
        Me.PrintPreviewBarItem8.Id = 13
        Me.PrintPreviewBarItem8.ImageOptions.ImageIndex = 25
        Me.PrintPreviewBarItem8.Name = "PrintPreviewBarItem8"
        '
        'PrintPreviewBarItem9
        '
        Me.PrintPreviewBarItem9.Caption = "&Print..."
        Me.PrintPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print
        Me.PrintPreviewBarItem9.Enabled = False
        Me.PrintPreviewBarItem9.Hint = "Print"
        Me.PrintPreviewBarItem9.Id = 14
        Me.PrintPreviewBarItem9.ImageOptions.ImageIndex = 0
        Me.PrintPreviewBarItem9.Name = "PrintPreviewBarItem9"
        '
        'PrintPreviewBarItem10
        '
        Me.PrintPreviewBarItem10.Caption = "P&rint"
        Me.PrintPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect
        Me.PrintPreviewBarItem10.Enabled = False
        Me.PrintPreviewBarItem10.Hint = "Quick Print"
        Me.PrintPreviewBarItem10.Id = 15
        Me.PrintPreviewBarItem10.ImageOptions.ImageIndex = 1
        Me.PrintPreviewBarItem10.Name = "PrintPreviewBarItem10"
        '
        'PrintPreviewBarItem11
        '
        Me.PrintPreviewBarItem11.Caption = "Page Set&up..."
        Me.PrintPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup
        Me.PrintPreviewBarItem11.Enabled = False
        Me.PrintPreviewBarItem11.Hint = "Page Setup"
        Me.PrintPreviewBarItem11.Id = 16
        Me.PrintPreviewBarItem11.ImageOptions.ImageIndex = 2
        Me.PrintPreviewBarItem11.Name = "PrintPreviewBarItem11"
        '
        'PrintPreviewBarItem12
        '
        Me.PrintPreviewBarItem12.Caption = "Header And Footer"
        Me.PrintPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF
        Me.PrintPreviewBarItem12.Enabled = False
        Me.PrintPreviewBarItem12.Hint = "Header And Footer"
        Me.PrintPreviewBarItem12.Id = 17
        Me.PrintPreviewBarItem12.ImageOptions.ImageIndex = 15
        Me.PrintPreviewBarItem12.Name = "PrintPreviewBarItem12"
        '
        'PrintPreviewBarItem13
        '
        Me.PrintPreviewBarItem13.ActAsDropDown = True
        Me.PrintPreviewBarItem13.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem13.Caption = "Scale"
        Me.PrintPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale
        Me.PrintPreviewBarItem13.Enabled = False
        Me.PrintPreviewBarItem13.Hint = "Scale"
        Me.PrintPreviewBarItem13.Id = 18
        Me.PrintPreviewBarItem13.ImageOptions.ImageIndex = 26
        Me.PrintPreviewBarItem13.Name = "PrintPreviewBarItem13"
        '
        'PrintPreviewBarItem14
        '
        Me.PrintPreviewBarItem14.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem14.Caption = "Hand Tool"
        Me.PrintPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool
        Me.PrintPreviewBarItem14.Enabled = False
        Me.PrintPreviewBarItem14.Hint = "Hand Tool"
        Me.PrintPreviewBarItem14.Id = 19
        Me.PrintPreviewBarItem14.ImageOptions.ImageIndex = 16
        Me.PrintPreviewBarItem14.Name = "PrintPreviewBarItem14"
        '
        'PrintPreviewBarItem15
        '
        Me.PrintPreviewBarItem15.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem15.Caption = "Magnifier"
        Me.PrintPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier
        Me.PrintPreviewBarItem15.Enabled = False
        Me.PrintPreviewBarItem15.Hint = "Magnifier"
        Me.PrintPreviewBarItem15.Id = 20
        Me.PrintPreviewBarItem15.ImageOptions.ImageIndex = 3
        Me.PrintPreviewBarItem15.Name = "PrintPreviewBarItem15"
        '
        'PrintPreviewBarItem16
        '
        Me.PrintPreviewBarItem16.Caption = "Zoom Out"
        Me.PrintPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut
        Me.PrintPreviewBarItem16.Enabled = False
        Me.PrintPreviewBarItem16.Hint = "Zoom Out"
        Me.PrintPreviewBarItem16.Id = 21
        Me.PrintPreviewBarItem16.ImageOptions.ImageIndex = 5
        Me.PrintPreviewBarItem16.Name = "PrintPreviewBarItem16"
        '
        'ZoomBarEditItem1
        '
        Me.ZoomBarEditItem1.Caption = "Zoom"
        Me.ZoomBarEditItem1.Edit = Me.PrintPreviewRepositoryItemComboBox1
        Me.ZoomBarEditItem1.EditValue = "100%"
        Me.ZoomBarEditItem1.EditWidth = 70
        Me.ZoomBarEditItem1.Enabled = False
        Me.ZoomBarEditItem1.Hint = "Zoom"
        Me.ZoomBarEditItem1.Id = 22
        Me.ZoomBarEditItem1.Name = "ZoomBarEditItem1"
        '
        'PrintPreviewRepositoryItemComboBox1
        '
        Me.PrintPreviewRepositoryItemComboBox1.AutoComplete = False
        Me.PrintPreviewRepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.PrintPreviewRepositoryItemComboBox1.DropDownRows = 11
        Me.PrintPreviewRepositoryItemComboBox1.Name = "PrintPreviewRepositoryItemComboBox1"
        '
        'PrintPreviewBarItem17
        '
        Me.PrintPreviewBarItem17.Caption = "Zoom In"
        Me.PrintPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn
        Me.PrintPreviewBarItem17.Enabled = False
        Me.PrintPreviewBarItem17.Hint = "Zoom In"
        Me.PrintPreviewBarItem17.Id = 23
        Me.PrintPreviewBarItem17.ImageOptions.ImageIndex = 4
        Me.PrintPreviewBarItem17.Name = "PrintPreviewBarItem17"
        '
        'PrintPreviewBarItem18
        '
        Me.PrintPreviewBarItem18.Caption = "First Page"
        Me.PrintPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage
        Me.PrintPreviewBarItem18.Enabled = False
        Me.PrintPreviewBarItem18.Hint = "First Page"
        Me.PrintPreviewBarItem18.Id = 24
        Me.PrintPreviewBarItem18.ImageOptions.ImageIndex = 7
        Me.PrintPreviewBarItem18.Name = "PrintPreviewBarItem18"
        '
        'PrintPreviewBarItem19
        '
        Me.PrintPreviewBarItem19.Caption = "Previous Page"
        Me.PrintPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage
        Me.PrintPreviewBarItem19.Enabled = False
        Me.PrintPreviewBarItem19.Hint = "Previous Page"
        Me.PrintPreviewBarItem19.Id = 25
        Me.PrintPreviewBarItem19.ImageOptions.ImageIndex = 8
        Me.PrintPreviewBarItem19.Name = "PrintPreviewBarItem19"
        '
        'PrintPreviewBarItem20
        '
        Me.PrintPreviewBarItem20.Caption = "Next Page"
        Me.PrintPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage
        Me.PrintPreviewBarItem20.Enabled = False
        Me.PrintPreviewBarItem20.Hint = "Next Page"
        Me.PrintPreviewBarItem20.Id = 26
        Me.PrintPreviewBarItem20.ImageOptions.ImageIndex = 9
        Me.PrintPreviewBarItem20.Name = "PrintPreviewBarItem20"
        '
        'PrintPreviewBarItem21
        '
        Me.PrintPreviewBarItem21.Caption = "Last Page"
        Me.PrintPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage
        Me.PrintPreviewBarItem21.Enabled = False
        Me.PrintPreviewBarItem21.Hint = "Last Page"
        Me.PrintPreviewBarItem21.Id = 27
        Me.PrintPreviewBarItem21.ImageOptions.ImageIndex = 10
        Me.PrintPreviewBarItem21.Name = "PrintPreviewBarItem21"
        '
        'PrintPreviewBarItem22
        '
        Me.PrintPreviewBarItem22.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem22.Caption = "Multiple Pages"
        Me.PrintPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages
        Me.PrintPreviewBarItem22.Enabled = False
        Me.PrintPreviewBarItem22.Hint = "Multiple Pages"
        Me.PrintPreviewBarItem22.Id = 28
        Me.PrintPreviewBarItem22.ImageOptions.ImageIndex = 11
        Me.PrintPreviewBarItem22.Name = "PrintPreviewBarItem22"
        '
        'PrintPreviewBarItem23
        '
        Me.PrintPreviewBarItem23.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem23.Caption = "&Color..."
        Me.PrintPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground
        Me.PrintPreviewBarItem23.Enabled = False
        Me.PrintPreviewBarItem23.Hint = "Background"
        Me.PrintPreviewBarItem23.Id = 29
        Me.PrintPreviewBarItem23.ImageOptions.ImageIndex = 12
        Me.PrintPreviewBarItem23.Name = "PrintPreviewBarItem23"
        '
        'PrintPreviewBarItem24
        '
        Me.PrintPreviewBarItem24.Caption = "&Watermark..."
        Me.PrintPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark
        Me.PrintPreviewBarItem24.Enabled = False
        Me.PrintPreviewBarItem24.Hint = "Watermark"
        Me.PrintPreviewBarItem24.Id = 30
        Me.PrintPreviewBarItem24.ImageOptions.ImageIndex = 21
        Me.PrintPreviewBarItem24.Name = "PrintPreviewBarItem24"
        '
        'PrintPreviewBarItem25
        '
        Me.PrintPreviewBarItem25.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem25.Caption = "Export Document..."
        Me.PrintPreviewBarItem25.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile
        Me.PrintPreviewBarItem25.Enabled = False
        Me.PrintPreviewBarItem25.Hint = "Export Document..."
        Me.PrintPreviewBarItem25.Id = 31
        Me.PrintPreviewBarItem25.ImageOptions.ImageIndex = 18
        Me.PrintPreviewBarItem25.Name = "PrintPreviewBarItem25"
        '
        'PrintPreviewBarItem26
        '
        Me.PrintPreviewBarItem26.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown
        Me.PrintPreviewBarItem26.Caption = "Send via E-Mail..."
        Me.PrintPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile
        Me.PrintPreviewBarItem26.Enabled = False
        Me.PrintPreviewBarItem26.Hint = "Send via E-Mail..."
        Me.PrintPreviewBarItem26.Id = 32
        Me.PrintPreviewBarItem26.ImageOptions.ImageIndex = 17
        Me.PrintPreviewBarItem26.Name = "PrintPreviewBarItem26"
        '
        'PrintPreviewBarItem27
        '
        Me.PrintPreviewBarItem27.Caption = "E&xit"
        Me.PrintPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview
        Me.PrintPreviewBarItem27.Enabled = False
        Me.PrintPreviewBarItem27.Hint = "Close Preview"
        Me.PrintPreviewBarItem27.Id = 33
        Me.PrintPreviewBarItem27.ImageOptions.ImageIndex = 13
        Me.PrintPreviewBarItem27.Name = "PrintPreviewBarItem27"
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
        Me.PrintPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Far
        Me.PrintPreviewStaticItem2.Type = "ZoomFactor"
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
        Me.barDockControlTop.Size = New System.Drawing.Size(1399, 34)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 1007)
        Me.barDockControlBottom.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1399, 30)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 34)
        Me.barDockControlLeft.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 973)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1399, 34)
        Me.barDockControlRight.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 973)
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
        Me.INDDvDocumentViewer.Location = New System.Drawing.Point(107, 2)
        Me.INDDvDocumentViewer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDDvDocumentViewer.Name = "INDDvDocumentViewer"
        Me.INDDvDocumentViewer.Size = New System.Drawing.Size(1399, 1037)
        Me.INDDvDocumentViewer.TabIndex = 1
        '
        'PrintPreviewSubItem1
        '
        Me.PrintPreviewSubItem1.Caption = "&File"
        Me.PrintPreviewSubItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.File
        Me.PrintPreviewSubItem1.Id = 34
        Me.PrintPreviewSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem11), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem25, True), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem26), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem27, True)})
        Me.PrintPreviewSubItem1.Name = "PrintPreviewSubItem1"
        '
        'PrintPreviewSubItem2
        '
        Me.PrintPreviewSubItem2.Caption = "&View"
        Me.PrintPreviewSubItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.View
        Me.PrintPreviewSubItem2.Id = 35
        Me.PrintPreviewSubItem2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewSubItem4, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarToolbarsListItem1, True)})
        Me.PrintPreviewSubItem2.Name = "PrintPreviewSubItem2"
        '
        'PrintPreviewSubItem4
        '
        Me.PrintPreviewSubItem4.Caption = "&Page Layout"
        Me.PrintPreviewSubItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayout
        Me.PrintPreviewSubItem4.Id = 37
        Me.PrintPreviewSubItem4.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem28), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem29)})
        Me.PrintPreviewSubItem4.Name = "PrintPreviewSubItem4"
        '
        'PrintPreviewBarItem28
        '
        Me.PrintPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem28.Caption = "&Facing"
        Me.PrintPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutFacing
        Me.PrintPreviewBarItem28.Enabled = False
        Me.PrintPreviewBarItem28.GroupIndex = 100
        Me.PrintPreviewBarItem28.Id = 38
        Me.PrintPreviewBarItem28.Name = "PrintPreviewBarItem28"
        '
        'PrintPreviewBarItem29
        '
        Me.PrintPreviewBarItem29.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
        Me.PrintPreviewBarItem29.Caption = "&Continuous"
        Me.PrintPreviewBarItem29.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutContinuous
        Me.PrintPreviewBarItem29.Enabled = False
        Me.PrintPreviewBarItem29.GroupIndex = 100
        Me.PrintPreviewBarItem29.Id = 39
        Me.PrintPreviewBarItem29.Name = "PrintPreviewBarItem29"
        '
        'BarToolbarsListItem1
        '
        Me.BarToolbarsListItem1.Caption = "Bars"
        Me.BarToolbarsListItem1.Id = 40
        Me.BarToolbarsListItem1.Name = "BarToolbarsListItem1"
        '
        'PrintPreviewSubItem3
        '
        Me.PrintPreviewSubItem3.Caption = "&Background"
        Me.PrintPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background
        Me.PrintPreviewSubItem3.Id = 36
        Me.PrintPreviewSubItem3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem23), New DevExpress.XtraBars.LinkPersistInfo(Me.PrintPreviewBarItem24)})
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
        Me.PrintPreviewBarCheckItem1.Id = 41
        Me.PrintPreviewBarCheckItem1.Name = "PrintPreviewBarCheckItem1"
        '
        'PrintPreviewBarCheckItem2
        '
        Me.PrintPreviewBarCheckItem2.Caption = "HTML File"
        Me.PrintPreviewBarCheckItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm
        Me.PrintPreviewBarCheckItem2.Enabled = False
        Me.PrintPreviewBarCheckItem2.GroupIndex = 2
        Me.PrintPreviewBarCheckItem2.Hint = "HTML File"
        Me.PrintPreviewBarCheckItem2.Id = 42
        Me.PrintPreviewBarCheckItem2.Name = "PrintPreviewBarCheckItem2"
        '
        'PrintPreviewBarCheckItem3
        '
        Me.PrintPreviewBarCheckItem3.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht
        Me.PrintPreviewBarCheckItem3.Enabled = False
        Me.PrintPreviewBarCheckItem3.GroupIndex = 2
        Me.PrintPreviewBarCheckItem3.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem3.Id = 43
        Me.PrintPreviewBarCheckItem3.Name = "PrintPreviewBarCheckItem3"
        '
        'PrintPreviewBarCheckItem4
        '
        Me.PrintPreviewBarCheckItem4.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf
        Me.PrintPreviewBarCheckItem4.Enabled = False
        Me.PrintPreviewBarCheckItem4.GroupIndex = 2
        Me.PrintPreviewBarCheckItem4.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem4.Id = 44
        Me.PrintPreviewBarCheckItem4.Name = "PrintPreviewBarCheckItem4"
        '
        'PrintPreviewBarCheckItem5
        '
        Me.PrintPreviewBarCheckItem5.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls
        Me.PrintPreviewBarCheckItem5.Enabled = False
        Me.PrintPreviewBarCheckItem5.GroupIndex = 2
        Me.PrintPreviewBarCheckItem5.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem5.Id = 45
        Me.PrintPreviewBarCheckItem5.Name = "PrintPreviewBarCheckItem5"
        '
        'PrintPreviewBarCheckItem6
        '
        Me.PrintPreviewBarCheckItem6.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx
        Me.PrintPreviewBarCheckItem6.Enabled = False
        Me.PrintPreviewBarCheckItem6.GroupIndex = 2
        Me.PrintPreviewBarCheckItem6.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem6.Id = 46
        Me.PrintPreviewBarCheckItem6.Name = "PrintPreviewBarCheckItem6"
        '
        'PrintPreviewBarCheckItem7
        '
        Me.PrintPreviewBarCheckItem7.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv
        Me.PrintPreviewBarCheckItem7.Enabled = False
        Me.PrintPreviewBarCheckItem7.GroupIndex = 2
        Me.PrintPreviewBarCheckItem7.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem7.Id = 47
        Me.PrintPreviewBarCheckItem7.Name = "PrintPreviewBarCheckItem7"
        '
        'PrintPreviewBarCheckItem8
        '
        Me.PrintPreviewBarCheckItem8.Caption = "Text File"
        Me.PrintPreviewBarCheckItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt
        Me.PrintPreviewBarCheckItem8.Enabled = False
        Me.PrintPreviewBarCheckItem8.GroupIndex = 2
        Me.PrintPreviewBarCheckItem8.Hint = "Text File"
        Me.PrintPreviewBarCheckItem8.Id = 48
        Me.PrintPreviewBarCheckItem8.Name = "PrintPreviewBarCheckItem8"
        '
        'PrintPreviewBarCheckItem9
        '
        Me.PrintPreviewBarCheckItem9.Caption = "Image File"
        Me.PrintPreviewBarCheckItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic
        Me.PrintPreviewBarCheckItem9.Enabled = False
        Me.PrintPreviewBarCheckItem9.GroupIndex = 2
        Me.PrintPreviewBarCheckItem9.Hint = "Image File"
        Me.PrintPreviewBarCheckItem9.Id = 49
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
        Me.PrintPreviewBarCheckItem10.Id = 50
        Me.PrintPreviewBarCheckItem10.Name = "PrintPreviewBarCheckItem10"
        '
        'PrintPreviewBarCheckItem11
        '
        Me.PrintPreviewBarCheckItem11.Caption = "MHT File"
        Me.PrintPreviewBarCheckItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht
        Me.PrintPreviewBarCheckItem11.Enabled = False
        Me.PrintPreviewBarCheckItem11.GroupIndex = 1
        Me.PrintPreviewBarCheckItem11.Hint = "MHT File"
        Me.PrintPreviewBarCheckItem11.Id = 51
        Me.PrintPreviewBarCheckItem11.Name = "PrintPreviewBarCheckItem11"
        '
        'PrintPreviewBarCheckItem12
        '
        Me.PrintPreviewBarCheckItem12.Caption = "RTF File"
        Me.PrintPreviewBarCheckItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf
        Me.PrintPreviewBarCheckItem12.Enabled = False
        Me.PrintPreviewBarCheckItem12.GroupIndex = 1
        Me.PrintPreviewBarCheckItem12.Hint = "RTF File"
        Me.PrintPreviewBarCheckItem12.Id = 52
        Me.PrintPreviewBarCheckItem12.Name = "PrintPreviewBarCheckItem12"
        '
        'PrintPreviewBarCheckItem13
        '
        Me.PrintPreviewBarCheckItem13.Caption = "XLS File"
        Me.PrintPreviewBarCheckItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls
        Me.PrintPreviewBarCheckItem13.Enabled = False
        Me.PrintPreviewBarCheckItem13.GroupIndex = 1
        Me.PrintPreviewBarCheckItem13.Hint = "XLS File"
        Me.PrintPreviewBarCheckItem13.Id = 53
        Me.PrintPreviewBarCheckItem13.Name = "PrintPreviewBarCheckItem13"
        '
        'PrintPreviewBarCheckItem14
        '
        Me.PrintPreviewBarCheckItem14.Caption = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx
        Me.PrintPreviewBarCheckItem14.Enabled = False
        Me.PrintPreviewBarCheckItem14.GroupIndex = 1
        Me.PrintPreviewBarCheckItem14.Hint = "XLSX File"
        Me.PrintPreviewBarCheckItem14.Id = 54
        Me.PrintPreviewBarCheckItem14.Name = "PrintPreviewBarCheckItem14"
        '
        'PrintPreviewBarCheckItem15
        '
        Me.PrintPreviewBarCheckItem15.Caption = "CSV File"
        Me.PrintPreviewBarCheckItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv
        Me.PrintPreviewBarCheckItem15.Enabled = False
        Me.PrintPreviewBarCheckItem15.GroupIndex = 1
        Me.PrintPreviewBarCheckItem15.Hint = "CSV File"
        Me.PrintPreviewBarCheckItem15.Id = 55
        Me.PrintPreviewBarCheckItem15.Name = "PrintPreviewBarCheckItem15"
        '
        'PrintPreviewBarCheckItem16
        '
        Me.PrintPreviewBarCheckItem16.Caption = "Text File"
        Me.PrintPreviewBarCheckItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt
        Me.PrintPreviewBarCheckItem16.Enabled = False
        Me.PrintPreviewBarCheckItem16.GroupIndex = 1
        Me.PrintPreviewBarCheckItem16.Hint = "Text File"
        Me.PrintPreviewBarCheckItem16.Id = 56
        Me.PrintPreviewBarCheckItem16.Name = "PrintPreviewBarCheckItem16"
        '
        'PrintPreviewBarCheckItem17
        '
        Me.PrintPreviewBarCheckItem17.Caption = "Image File"
        Me.PrintPreviewBarCheckItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic
        Me.PrintPreviewBarCheckItem17.Enabled = False
        Me.PrintPreviewBarCheckItem17.GroupIndex = 1
        Me.PrintPreviewBarCheckItem17.Hint = "Image File"
        Me.PrintPreviewBarCheckItem17.Id = 57
        Me.PrintPreviewBarCheckItem17.Name = "PrintPreviewBarCheckItem17"
        '
        'INDSbExportExcel
        '
        Me.INDSbExportExcel.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.ICONO_EXCEL_02
        Me.INDSbExportExcel.Location = New System.Drawing.Point(1110, 340)
        Me.INDSbExportExcel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbExportExcel, False)
        Me.INDSbExportExcel.Name = "INDSbExportExcel"
        Me.INDSbExportExcel.Size = New System.Drawing.Size(60, 47)
        Me.INDSbExportExcel.StyleController = Me.INDLcBase
        Me.INDSbExportExcel.TabIndex = 10
        '
        'INDGleReportType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleReportType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleReportType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleReportType, True)
        Me.INDGleReportType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleReportType, True)
        Me.INDGleReportType.Location = New System.Drawing.Point(35, 354)
        Me.INDGleReportType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleReportType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleReportType.Name = "INDGleReportType"
        Me.INDGleReportType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleReportType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleReportType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleReportType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleReportType.Properties.Appearance.Options.UseFont = True
        Me.INDGleReportType.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleReportType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleReportType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleReportType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleReportType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleReportType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleReportType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleReportType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleReportType.Properties.DisplayMember = "Item2"
        Me.INDGleReportType.Properties.ImmediatePopup = True
        Me.INDGleReportType.Properties.NullText = ""
        Me.INDGleReportType.Properties.PopupView = Me.GridView1
        Me.INDGleReportType.Properties.ValueMember = "Item1"
        Me.INDGleReportType.Size = New System.Drawing.Size(534, 38)
        Me.INDGleReportType.StyleController = Me.INDLcBase
        Me.INDGleReportType.TabIndex = 4
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleReportType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleReportType, 0)
        Me.INDGleReportType.ToolTip = "Este Campo es Necesario"
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
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn19})
        Me.GridView1.DetailHeight = 512
        Me.GridView1.FixedLineWidth = 3
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Tipo de reporte"
        Me.GridColumn19.FieldName = "Item2"
        Me.GridColumn19.MinWidth = 30
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 0
        Me.GridColumn19.Width = 112
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(609, 343)
        Me.INDSbGenerateReport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(498, 41)
        Me.INDSbGenerateReport.StyleController = Me.INDLcBase
        Me.INDSbGenerateReport.TabIndex = 9
        Me.INDSbGenerateReport.Text = "Generar Reporte"
        '
        'INDGleState
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleState, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleState, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleState, True)
        Me.INDGleState.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleState, True)
        Me.INDGleState.Location = New System.Drawing.Point(35, 272)
        Me.INDGleState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleState, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleState.Name = "INDGleState"
        Me.INDGleState.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleState.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleState.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleState.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleState.Properties.Appearance.Options.UseFont = True
        Me.INDGleState.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleState.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleState.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleState.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleState.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleState.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleState.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleState.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleState.Properties.DisplayMember = "Item2"
        Me.INDGleState.Properties.ImmediatePopup = True
        Me.INDGleState.Properties.NullText = ""
        Me.INDGleState.Properties.PopupView = Me.GridLookUpEdit2View
        Me.INDGleState.Properties.ValueMember = "Item1"
        Me.INDGleState.Size = New System.Drawing.Size(534, 38)
        Me.INDGleState.StyleController = Me.INDLcBase
        Me.INDGleState.TabIndex = 2
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleState, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleState, 0)
        Me.INDGleState.ToolTip = "Este Campo es Necesario"
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
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.GridLookUpEdit2View.DetailHeight = 512
        Me.GridLookUpEdit2View.FixedLineWidth = 3
        Me.GridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit2View.Name = "GridLookUpEdit2View"
        Me.GridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit2View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo Filtro"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.MinWidth = 30
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 112
        '
        'INDSleSupplier
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleSupplier, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleSupplier, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleSupplier, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.Location = New System.Drawing.Point(609, 199)
        Me.INDSleSupplier.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleSupplier, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleSupplier.Name = "INDSleSupplier"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleSupplier, False)
        Me.INDSleSupplier.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleSupplier.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleSupplier.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleSupplier.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleSupplier.Properties.Appearance.Options.UseFont = True
        Me.INDSleSupplier.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleSupplier.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleSupplier.Properties.NullText = ""
        Me.INDSleSupplier.Properties.PopupSizeable = False
        Me.INDSleSupplier.Properties.PopupView = Me.INGvSupplier
        Me.INDSleSupplier.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleSupplier, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleSupplier, True)
        Me.INDSleSupplier.Size = New System.Drawing.Size(558, 38)
        Me.INDSleSupplier.StyleController = Me.INDLcBase
        Me.INDSleSupplier.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleSupplier, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleSupplier, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleSupplier, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleSupplier, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleSupplier, False)
        '
        'INGvSupplier
        '
        Me.INGvSupplier.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INGvSupplier.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INGvSupplier.Appearance.FocusedRow.Options.UseFont = True
        Me.INGvSupplier.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INGvSupplier.Appearance.GroupRow.Options.UseFont = True
        Me.INGvSupplier.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INGvSupplier.Appearance.HeaderPanel.Options.UseFont = True
        Me.INGvSupplier.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INGvSupplier.Appearance.Row.Options.UseFont = True
        Me.INGvSupplier.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColSupplier_UnboundSelection, Me.INDColSupplier, Me.INDColSupplierDescription, Me.INDColSupplierStatus})
        Me.INGvSupplier.DetailHeight = 512
        Me.INGvSupplier.FixedLineWidth = 3
        Me.INGvSupplier.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INGvSupplier.Name = "INGvSupplier"
        Me.INGvSupplier.OptionsFind.ShowClearButton = False
        Me.INGvSupplier.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INGvSupplier.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INGvSupplier.OptionsSelection.MultiSelect = True
        Me.INGvSupplier.OptionsView.EnableAppearanceEvenRow = True
        Me.INGvSupplier.OptionsView.EnableAppearanceOddRow = True
        Me.INGvSupplier.OptionsView.ShowAutoFilterRow = True
        Me.INGvSupplier.OptionsView.ShowDetailButtons = False
        Me.INGvSupplier.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INGvSupplier, False)
        '
        'INDColSupplier_UnboundSelection
        '
        Me.INDColSupplier_UnboundSelection.Caption = " "
        Me.INDColSupplier_UnboundSelection.FieldName = "INDColSupplier_UnboundSelection"
        Me.INDColSupplier_UnboundSelection.MinWidth = 30
        Me.INDColSupplier_UnboundSelection.Name = "INDColSupplier_UnboundSelection"
        Me.INDColSupplier_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColSupplier_UnboundSelection.Visible = True
        Me.INDColSupplier_UnboundSelection.VisibleIndex = 0
        Me.INDColSupplier_UnboundSelection.Width = 30
        '
        'INDColSupplier
        '
        Me.INDColSupplier.Caption = "Nit"
        Me.INDColSupplier.FieldName = "ThirdPartyNit"
        Me.INDColSupplier.MinWidth = 30
        Me.INDColSupplier.Name = "INDColSupplier"
        Me.INDColSupplier.OptionsColumn.AllowEdit = False
        Me.INDColSupplier.OptionsColumn.AllowFocus = False
        Me.INDColSupplier.Visible = True
        Me.INDColSupplier.VisibleIndex = 1
        Me.INDColSupplier.Width = 112
        '
        'INDColSupplierDescription
        '
        Me.INDColSupplierDescription.Caption = "Nombre"
        Me.INDColSupplierDescription.FieldName = "Name"
        Me.INDColSupplierDescription.MinWidth = 30
        Me.INDColSupplierDescription.Name = "INDColSupplierDescription"
        Me.INDColSupplierDescription.OptionsColumn.AllowEdit = False
        Me.INDColSupplierDescription.OptionsColumn.AllowFocus = False
        Me.INDColSupplierDescription.Visible = True
        Me.INDColSupplierDescription.VisibleIndex = 2
        Me.INDColSupplierDescription.Width = 112
        '
        'INDColSupplierStatus
        '
        Me.INDColSupplierStatus.Caption = "Estado"
        Me.INDColSupplierStatus.FieldName = "StatusName"
        Me.INDColSupplierStatus.MinWidth = 30
        Me.INDColSupplierStatus.Name = "INDColSupplierStatus"
        Me.INDColSupplierStatus.OptionsColumn.AllowEdit = False
        Me.INDColSupplierStatus.OptionsColumn.AllowFocus = False
        Me.INDColSupplierStatus.Visible = True
        Me.INDColSupplierStatus.VisibleIndex = 3
        Me.INDColSupplierStatus.Width = 112
        '
        'INDSleProduct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleProduct, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct, False)
        Me.INDSleProduct.Location = New System.Drawing.Point(609, 287)
        Me.INDSleProduct.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProduct.Name = "INDSleProduct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProduct, False)
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
        Me.INDSleProduct.Properties.PopupSizeable = False
        Me.INDSleProduct.Properties.PopupView = Me.INDGvProduct
        Me.INDSleProduct.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProduct, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct, True)
        Me.INDSleProduct.Size = New System.Drawing.Size(558, 38)
        Me.INDSleProduct.StyleController = Me.INDLcBase
        Me.INDSleProduct.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProduct, 0)
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
        Me.INDGvProduct.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProducto_UnboundSelection, Me.INDColProductoCode, Me.INDColProductoName})
        Me.INDGvProduct.DetailHeight = 512
        Me.INDGvProduct.FixedLineWidth = 3
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
        'INDColProducto_UnboundSelection
        '
        Me.INDColProducto_UnboundSelection.Caption = " "
        Me.INDColProducto_UnboundSelection.FieldName = "INDColProducto_UnboundSelection"
        Me.INDColProducto_UnboundSelection.MinWidth = 30
        Me.INDColProducto_UnboundSelection.Name = "INDColProducto_UnboundSelection"
        Me.INDColProducto_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColProducto_UnboundSelection.Visible = True
        Me.INDColProducto_UnboundSelection.VisibleIndex = 0
        Me.INDColProducto_UnboundSelection.Width = 30
        '
        'INDColProductoCode
        '
        Me.INDColProductoCode.Caption = "Código"
        Me.INDColProductoCode.FieldName = "Code"
        Me.INDColProductoCode.MinWidth = 30
        Me.INDColProductoCode.Name = "INDColProductoCode"
        Me.INDColProductoCode.OptionsColumn.AllowEdit = False
        Me.INDColProductoCode.OptionsColumn.AllowFocus = False
        Me.INDColProductoCode.Visible = True
        Me.INDColProductoCode.VisibleIndex = 1
        Me.INDColProductoCode.Width = 112
        '
        'INDColProductoName
        '
        Me.INDColProductoName.Caption = "Nombre"
        Me.INDColProductoName.FieldName = "Name"
        Me.INDColProductoName.MinWidth = 30
        Me.INDColProductoName.Name = "INDColProductoName"
        Me.INDColProductoName.OptionsColumn.AllowEdit = False
        Me.INDColProductoName.OptionsColumn.AllowFocus = False
        Me.INDColProductoName.Visible = True
        Me.INDColProductoName.VisibleIndex = 2
        Me.INDColProductoName.Width = 112
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilterRequired, Me.INDLcgFilterOptional})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1308, 1041)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.INDLcgFilterRequired.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciReportType, Me.INDLciGcExportExcel, Me.INDLciOrderType, Me.LayoutControlItem2, Me.LayoutControlItem4})
        Me.INDLcgFilterRequired.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilterRequired.Name = "INDLcgFilterRequired"
        Me.INDLcgFilterRequired.Size = New System.Drawing.Size(574, 1011)
        Me.INDLcgFilterRequired.Text = "Criterios"
        '
        'INDLciReportType
        '
        Me.INDLciReportType.Control = Me.INDGleReportType
        Me.INDLciReportType.Location = New System.Drawing.Point(0, 243)
        Me.INDLciReportType.MaxSize = New System.Drawing.Size(540, 82)
        Me.INDLciReportType.MinSize = New System.Drawing.Size(540, 82)
        Me.INDLciReportType.Name = "INDLciReportType"
        Me.INDLciReportType.Size = New System.Drawing.Size(540, 82)
        Me.INDLciReportType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReportType.Text = "Tipo Reporte:"
        Me.INDLciReportType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReportType.TextSize = New System.Drawing.Size(124, 28)
        '
        'INDLciGcExportExcel
        '
        Me.INDLciGcExportExcel.Control = Me.INDGcExportExcel
        Me.INDLciGcExportExcel.Location = New System.Drawing.Point(0, 325)
        Me.INDLciGcExportExcel.Name = "INDLciGcExportExcel"
        Me.INDLciGcExportExcel.Size = New System.Drawing.Size(540, 608)
        Me.INDLciGcExportExcel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGcExportExcel.TextVisible = False
        Me.INDLciGcExportExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciOrderType
        '
        Me.INDLciOrderType.Control = Me.INDGleState
        Me.INDLciOrderType.Location = New System.Drawing.Point(0, 161)
        Me.INDLciOrderType.MaxSize = New System.Drawing.Size(540, 82)
        Me.INDLciOrderType.MinSize = New System.Drawing.Size(540, 82)
        Me.INDLciOrderType.Name = "INDLciOrderType"
        Me.INDLciOrderType.Size = New System.Drawing.Size(540, 82)
        Me.INDLciOrderType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciOrderType.Text = "Estado:"
        Me.INDLciOrderType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciOrderType.TextSize = New System.Drawing.Size(124, 28)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDDateStart1
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(540, 82)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(540, 82)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(540, 82)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Fecha inicial:"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(124, 28)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDDateEnd1
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 82)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(540, 79)
        Me.LayoutControlItem4.Text = "Fecha final:"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(124, 28)
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
        Me.INDLcgFilterOptional.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciGenerateReport, Me.LayoutControlItem6, Me.INDLblWarehouse, Me.INDLblSupplier, Me.INDLblProducto})
        Me.INDLcgFilterOptional.Location = New System.Drawing.Point(574, 0)
        Me.INDLcgFilterOptional.Name = "INDLcgFilterOptional"
        Me.INDLcgFilterOptional.Size = New System.Drawing.Size(704, 1011)
        Me.INDLcgFilterOptional.Text = "Filtros"
        '
        'INDLciGenerateReport
        '
        Me.INDLciGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciGenerateReport.Location = New System.Drawing.Point(0, 264)
        Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(504, 47)
        Me.INDLciGenerateReport.MinSize = New System.Drawing.Size(504, 47)
        Me.INDLciGenerateReport.Name = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Size = New System.Drawing.Size(504, 669)
        Me.INDLciGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateReport.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDSbExportExcel
        Me.LayoutControlItem6.Location = New System.Drawing.Point(504, 264)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(60, 47)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(60, 47)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem6.Size = New System.Drawing.Size(166, 669)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'INDLblWarehouse
        '
        Me.INDLblWarehouse.Control = Me.INDSleWarehouse
        Me.INDLblWarehouse.Location = New System.Drawing.Point(0, 0)
        Me.INDLblWarehouse.MaxSize = New System.Drawing.Size(564, 88)
        Me.INDLblWarehouse.MinSize = New System.Drawing.Size(564, 88)
        Me.INDLblWarehouse.Name = "INDLblWarehouse"
        Me.INDLblWarehouse.Size = New System.Drawing.Size(670, 88)
        Me.INDLblWarehouse.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblWarehouse.Text = "Almacén :"
        Me.INDLblWarehouse.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblWarehouse.TextSize = New System.Drawing.Size(124, 28)
        '
        'INDLblSupplier
        '
        Me.INDLblSupplier.Control = Me.INDSleSupplier
        Me.INDLblSupplier.CustomizationFormText = "Almacén"
        Me.INDLblSupplier.Location = New System.Drawing.Point(0, 88)
        Me.INDLblSupplier.MaxSize = New System.Drawing.Size(564, 88)
        Me.INDLblSupplier.MinSize = New System.Drawing.Size(564, 88)
        Me.INDLblSupplier.Name = "INDLblSupplier"
        Me.INDLblSupplier.Size = New System.Drawing.Size(670, 88)
        Me.INDLblSupplier.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblSupplier.Text = "Proveedor :"
        Me.INDLblSupplier.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblSupplier.TextSize = New System.Drawing.Size(124, 28)
        '
        'INDLblProducto
        '
        Me.INDLblProducto.Control = Me.INDSleProduct
        Me.INDLblProducto.CustomizationFormText = "Almacén"
        Me.INDLblProducto.Location = New System.Drawing.Point(0, 176)
        Me.INDLblProducto.MaxSize = New System.Drawing.Size(564, 88)
        Me.INDLblProducto.MinSize = New System.Drawing.Size(564, 88)
        Me.INDLblProducto.Name = "INDLblProducto"
        Me.INDLblProducto.Size = New System.Drawing.Size(670, 88)
        Me.INDLblProducto.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblProducto.Text = "Producto :"
        Me.INDLblProducto.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblProducto.TextSize = New System.Drawing.Size(124, 28)
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
        'INDCbePersonTypeEnd
        '
        Me.INDCbePersonTypeEnd.AutoHeight = False
        Me.INDCbePersonTypeEnd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbePersonTypeEnd.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Natural", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Jurídico", CType(2, Byte), -1)})
        Me.INDCbePersonTypeEnd.Name = "INDCbePersonTypeEnd"
        '
        'INDCbeContributionTypeEnd
        '
        Me.INDCbeContributionTypeEnd.AutoHeight = False
        Me.INDCbeContributionTypeEnd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeContributionTypeEnd.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No responsable de Iva", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Responsables de Iva", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Empresa Estatal", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Gran Contribuyente", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Régimen simple", CType(4, Byte), -1)})
        Me.INDCbeContributionTypeEnd.Name = "INDCbeContributionTypeEnd"
        '
        'INDCbeRetentionTypeEnd
        '
        Me.INDCbeRetentionTypeEnd.AutoHeight = False
        Me.INDCbeRetentionTypeEnd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeRetentionTypeEnd.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguna", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Exento de Retención", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hace Retención", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Autoretenedor", CType(3, Byte), -1)})
        Me.INDCbeRetentionTypeEnd.Name = "INDCbeRetentionTypeEnd"
        '
        'INDCbeThirdPartyStatusEnd
        '
        Me.INDCbeThirdPartyStatusEnd.AutoHeight = False
        Me.INDCbeThirdPartyStatusEnd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeThirdPartyStatusEnd.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Activo", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inactivo", CType(0, Byte), -1)})
        Me.INDCbeThirdPartyStatusEnd.Name = "INDCbeThirdPartyStatusEnd"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'INDGvProduct1
        '
        Me.INDGvProduct1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProduct1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProduct1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProduct1.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProduct1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct1.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProduct1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct1.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProduct1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProduct1.Appearance.Row.Options.UseFont = True
        Me.INDGvProduct1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProduct_UnboundSelection, Me.INDColProductCode, Me.INDColProductName})
        Me.INDGvProduct1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProduct1.Name = "INDGvProduct1"
        Me.INDGvProduct1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProduct1.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvProduct1.OptionsSelection.MultiSelect = True
        Me.INDGvProduct1.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProduct1.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProduct1.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProduct1.OptionsView.ShowDetailButtons = False
        Me.INDGvProduct1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProduct1, False)
        '
        'INDColProduct_UnboundSelection
        '
        Me.INDColProduct_UnboundSelection.Caption = " "
        Me.INDColProduct_UnboundSelection.CustomizationCaption = " "
        Me.INDColProduct_UnboundSelection.FieldName = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection.Name = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColProduct_UnboundSelection.Visible = True
        Me.INDColProduct_UnboundSelection.VisibleIndex = 0
        Me.INDColProduct_UnboundSelection.Width = 20
        '
        'INDColProductCode
        '
        Me.INDColProductCode.Caption = " Código"
        Me.INDColProductCode.FieldName = "Code"
        Me.INDColProductCode.Name = "INDColProductCode"
        Me.INDColProductCode.OptionsColumn.AllowEdit = False
        Me.INDColProductCode.OptionsColumn.AllowFocus = False
        Me.INDColProductCode.Visible = True
        Me.INDColProductCode.VisibleIndex = 1
        '
        'INDColProductName
        '
        Me.INDColProductName.Caption = "Nombre"
        Me.INDColProductName.FieldName = "Name"
        Me.INDColProductName.Name = "INDColProductName"
        Me.INDColProductName.OptionsColumn.AllowEdit = False
        Me.INDColProductName.OptionsColumn.AllowFocus = False
        Me.INDColProductName.Visible = True
        Me.INDColProductName.VisibleIndex = 2
        '
        'INDGvProduct2
        '
        Me.INDGvProduct2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProduct2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProduct2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProduct2.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProduct2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct2.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProduct2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct2.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProduct2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProduct2.Appearance.Row.Options.UseFont = True
        Me.INDGvProduct2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProduct_UnboundSelection1, Me.INDColProductCode1, Me.INDColProductName1})
        Me.INDGvProduct2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProduct2.Name = "INDGvProduct2"
        Me.INDGvProduct2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProduct2.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvProduct2.OptionsSelection.MultiSelect = True
        Me.INDGvProduct2.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProduct2.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProduct2.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProduct2.OptionsView.ShowDetailButtons = False
        Me.INDGvProduct2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProduct2, False)
        '
        'INDColProduct_UnboundSelection1
        '
        Me.INDColProduct_UnboundSelection1.Caption = " "
        Me.INDColProduct_UnboundSelection1.CustomizationCaption = " "
        Me.INDColProduct_UnboundSelection1.FieldName = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection1.Name = "INDColProduct_UnboundSelection1"
        Me.INDColProduct_UnboundSelection1.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColProduct_UnboundSelection1.Visible = True
        Me.INDColProduct_UnboundSelection1.VisibleIndex = 0
        Me.INDColProduct_UnboundSelection1.Width = 20
        '
        'INDColProductCode1
        '
        Me.INDColProductCode1.Caption = " Código"
        Me.INDColProductCode1.FieldName = "Code"
        Me.INDColProductCode1.Name = "INDColProductCode1"
        Me.INDColProductCode1.OptionsColumn.AllowEdit = False
        Me.INDColProductCode1.OptionsColumn.AllowFocus = False
        Me.INDColProductCode1.Visible = True
        Me.INDColProductCode1.VisibleIndex = 1
        '
        'INDColProductName1
        '
        Me.INDColProductName1.Caption = "Nombre"
        Me.INDColProductName1.FieldName = "Name"
        Me.INDColProductName1.Name = "INDColProductName1"
        Me.INDColProductName1.OptionsColumn.AllowEdit = False
        Me.INDColProductName1.OptionsColumn.AllowFocus = False
        Me.INDColProductName1.Visible = True
        Me.INDColProductName1.VisibleIndex = 2
        '
        'INDGvProduct3
        '
        Me.INDGvProduct3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProduct3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProduct3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProduct3.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProduct3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct3.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProduct3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct3.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProduct3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProduct3.Appearance.Row.Options.UseFont = True
        Me.INDGvProduct3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProduct_UnboundSelection2, Me.INDColProductCode2, Me.INDColProductName2})
        Me.INDGvProduct3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProduct3.Name = "INDGvProduct3"
        Me.INDGvProduct3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProduct3.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvProduct3.OptionsSelection.MultiSelect = True
        Me.INDGvProduct3.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProduct3.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProduct3.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProduct3.OptionsView.ShowDetailButtons = False
        Me.INDGvProduct3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProduct3, False)
        '
        'INDColProduct_UnboundSelection2
        '
        Me.INDColProduct_UnboundSelection2.Caption = " "
        Me.INDColProduct_UnboundSelection2.CustomizationCaption = " "
        Me.INDColProduct_UnboundSelection2.FieldName = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection2.Name = "INDColProduct_UnboundSelection2"
        Me.INDColProduct_UnboundSelection2.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColProduct_UnboundSelection2.Visible = True
        Me.INDColProduct_UnboundSelection2.VisibleIndex = 0
        Me.INDColProduct_UnboundSelection2.Width = 20
        '
        'INDColProductCode2
        '
        Me.INDColProductCode2.Caption = " Código"
        Me.INDColProductCode2.FieldName = "Code"
        Me.INDColProductCode2.Name = "INDColProductCode2"
        Me.INDColProductCode2.OptionsColumn.AllowEdit = False
        Me.INDColProductCode2.OptionsColumn.AllowFocus = False
        Me.INDColProductCode2.Visible = True
        Me.INDColProductCode2.VisibleIndex = 1
        '
        'INDColProductName2
        '
        Me.INDColProductName2.Caption = "Nombre"
        Me.INDColProductName2.FieldName = "Name"
        Me.INDColProductName2.Name = "INDColProductName2"
        Me.INDColProductName2.OptionsColumn.AllowEdit = False
        Me.INDColProductName2.OptionsColumn.AllowFocus = False
        Me.INDColProductName2.Visible = True
        Me.INDColProductName2.VisibleIndex = 2
        '
        'INDGvProduct4
        '
        Me.INDGvProduct4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProduct4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProduct4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProduct4.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProduct4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct4.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProduct4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProduct4.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProduct4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProduct4.Appearance.Row.Options.UseFont = True
        Me.INDGvProduct4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColProduct_UnboundSelection3, Me.INDColProductCode3, Me.INDColProductName3})
        Me.INDGvProduct4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProduct4.Name = "INDGvProduct4"
        Me.INDGvProduct4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProduct4.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvProduct4.OptionsSelection.MultiSelect = True
        Me.INDGvProduct4.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProduct4.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProduct4.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProduct4.OptionsView.ShowDetailButtons = False
        Me.INDGvProduct4.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProduct4, False)
        '
        'INDColProduct_UnboundSelection3
        '
        Me.INDColProduct_UnboundSelection3.Caption = " "
        Me.INDColProduct_UnboundSelection3.CustomizationCaption = " "
        Me.INDColProduct_UnboundSelection3.FieldName = "INDColProduct_UnboundSelection"
        Me.INDColProduct_UnboundSelection3.Name = "INDColProduct_UnboundSelection3"
        Me.INDColProduct_UnboundSelection3.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDColProduct_UnboundSelection3.Visible = True
        Me.INDColProduct_UnboundSelection3.VisibleIndex = 0
        Me.INDColProduct_UnboundSelection3.Width = 20
        '
        'INDColProductCode3
        '
        Me.INDColProductCode3.Caption = " Código"
        Me.INDColProductCode3.FieldName = "Code"
        Me.INDColProductCode3.Name = "INDColProductCode3"
        Me.INDColProductCode3.OptionsColumn.AllowEdit = False
        Me.INDColProductCode3.OptionsColumn.AllowFocus = False
        Me.INDColProductCode3.Visible = True
        Me.INDColProductCode3.VisibleIndex = 1
        '
        'INDColProductName3
        '
        Me.INDColProductName3.Caption = "Nombre"
        Me.INDColProductName3.FieldName = "Name"
        Me.INDColProductName3.Name = "INDColProductName3"
        Me.INDColProductName3.OptionsColumn.AllowEdit = False
        Me.INDColProductName3.OptionsColumn.AllowFocus = False
        Me.INDColProductName3.Visible = True
        Me.INDColProductName3.VisibleIndex = 2
        '
        'INDSleProduct1
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct1, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct1, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct1, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleProduct1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct1, False)
        Me.INDSleProduct1.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct1, False)
        Me.INDSleProduct1.Location = New System.Drawing.Point(12, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProduct1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProduct1.Name = "INDSleProduct1"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProduct1, False)
        Me.INDSleProduct1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleProduct1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProduct1.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProduct1.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleProduct1.Properties.NullText = ""
        Me.INDSleProduct1.Properties.PopupSizeable = False
        Me.INDSleProduct1.Properties.PopupView = Me.INDGvProduct1
        Me.INDSleProduct1.Properties.ShowClearButton = False
        Me.INDSleProduct1.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct1, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProduct1, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct1, True)
        Me.INDSleProduct1.Size = New System.Drawing.Size(376, 38)
        Me.INDSleProduct1.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct1, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProduct1, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProduct1, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProduct1, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProduct1, False)
        '
        'INDSleProduct2
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct2, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct2, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct2, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleProduct2, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct2, False)
        Me.INDSleProduct2.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct2, False)
        Me.INDSleProduct2.Location = New System.Drawing.Point(12, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProduct2, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProduct2.Name = "INDSleProduct2"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProduct2, False)
        Me.INDSleProduct2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleProduct2.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProduct2.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProduct2.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleProduct2.Properties.NullText = ""
        Me.INDSleProduct2.Properties.PopupSizeable = False
        Me.INDSleProduct2.Properties.PopupView = Me.INDGvProduct2
        Me.INDSleProduct2.Properties.ShowClearButton = False
        Me.INDSleProduct2.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct2, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProduct2, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct2, True)
        Me.INDSleProduct2.Size = New System.Drawing.Size(376, 38)
        Me.INDSleProduct2.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct2, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProduct2, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProduct2, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProduct2, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProduct2, False)
        '
        'INDSleProduct3
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct3, AppearanceObject11)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct3, AppearanceObject12)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct3, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleProduct3, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct3, False)
        Me.INDSleProduct3.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct3, False)
        Me.INDSleProduct3.Location = New System.Drawing.Point(12, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProduct3, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProduct3.Name = "INDSleProduct3"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProduct3, False)
        Me.INDSleProduct3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleProduct3.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProduct3.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProduct3.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleProduct3.Properties.NullText = ""
        Me.INDSleProduct3.Properties.PopupSizeable = False
        Me.INDSleProduct3.Properties.PopupView = Me.INDGvProduct3
        Me.INDSleProduct3.Properties.ShowClearButton = False
        Me.INDSleProduct3.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct3, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProduct3, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct3, True)
        Me.INDSleProduct3.Size = New System.Drawing.Size(376, 38)
        Me.INDSleProduct3.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct3, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProduct3, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProduct3, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProduct3, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProduct3, False)
        '
        'INDSleProduct4
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProduct4, AppearanceObject13)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProduct4, AppearanceObject14)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProduct4, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleProduct4, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProduct4, False)
        Me.INDSleProduct4.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProduct4, False)
        Me.INDSleProduct4.Location = New System.Drawing.Point(12, 335)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProduct4, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProduct4.Name = "INDSleProduct4"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProduct4, False)
        Me.INDSleProduct4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleProduct4.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProduct4.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProduct4.Properties.Appearance.Options.UseFont = True
        Me.INDSleProduct4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions4, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject13, SerializableAppearanceObject14, SerializableAppearanceObject15, SerializableAppearanceObject16, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleProduct4.Properties.NullText = ""
        Me.INDSleProduct4.Properties.PopupSizeable = False
        Me.INDSleProduct4.Properties.PopupView = Me.INDGvProduct4
        Me.INDSleProduct4.Properties.ShowClearButton = False
        Me.INDSleProduct4.Properties.ShowFooter = False
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProduct4, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProduct4, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProduct4, True)
        Me.INDSleProduct4.Size = New System.Drawing.Size(376, 38)
        Me.INDSleProduct4.TabIndex = 26
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProduct4, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProduct4, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProduct4, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProduct4, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProduct4, False)
        '
        'INDPcDocumentViewer
        '
        Me.INDPcDocumentViewer.Controls.Add(Me.INDDvDocumentViewer)
        Me.INDPcDocumentViewer.Controls.Add(Me.INDCnBack)
        Me.INDPcDocumentViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcDocumentViewer.Location = New System.Drawing.Point(2, 12)
        Me.INDPcDocumentViewer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDPcDocumentViewer.Name = "INDPcDocumentViewer"
        Me.INDPcDocumentViewer.Size = New System.Drawing.Size(1508, 1041)
        Me.INDPcDocumentViewer.TabIndex = 2
        '
        'INDCnBack
        '
        Me.INDCnBack.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnBack.Location = New System.Drawing.Point(2, 2)
        Me.INDCnBack.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.INDCnBack.Name = "INDCnBack"
        Me.INDCnBack.Size = New System.Drawing.Size(105, 1037)
        Me.INDCnBack.TabIndex = 0
        '
        'IndigoDocumentViewer1
        '
        Me.IndigoDocumentViewer1.ParentForm = Nothing
        Me.IndigoDocumentViewer1.Permissions = Nothing
        '
        'INDCbePersonTypeEnd1
        '
        Me.INDCbePersonTypeEnd1.AutoHeight = False
        Me.INDCbePersonTypeEnd1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbePersonTypeEnd1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Natural", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Jurídico", CType(2, Byte), -1)})
        Me.INDCbePersonTypeEnd1.Name = "INDCbePersonTypeEnd1"
        '
        'INDCbeContributionTypeEnd1
        '
        Me.INDCbeContributionTypeEnd1.AutoHeight = False
        Me.INDCbeContributionTypeEnd1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeContributionTypeEnd1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No responsable de Iva", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Responsables de Iva", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Empresa Estatal", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Gran Contribuyente", CType(3, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Régimen simple", CType(4, Byte), -1)})
        Me.INDCbeContributionTypeEnd1.Name = "INDCbeContributionTypeEnd1"
        '
        'INDCbeRetentionTypeEnd1
        '
        Me.INDCbeRetentionTypeEnd1.AutoHeight = False
        Me.INDCbeRetentionTypeEnd1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeRetentionTypeEnd1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Ninguna", CType(0, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Exento de Retención", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Hace Retención", CType(2, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Autoretenedor", CType(3, Byte), -1)})
        Me.INDCbeRetentionTypeEnd1.Name = "INDCbeRetentionTypeEnd1"
        '
        'INDCbeThirdPartyStatusEnd1
        '
        Me.INDCbeThirdPartyStatusEnd1.AutoHeight = False
        Me.INDCbeThirdPartyStatusEnd1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeThirdPartyStatusEnd1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Activo", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inactivo", CType(0, Byte), -1)})
        Me.INDCbeThirdPartyStatusEnd1.Name = "INDCbeThirdPartyStatusEnd1"
        '
        'INDLblProduct
        '
        Me.INDLblProduct.Control = Me.INDSleProduct1
        Me.INDLblProduct.CustomizationFormText = "Producto :"
        Me.INDLblProduct.Location = New System.Drawing.Point(0, 307)
        Me.INDLblProduct.MaxSize = New System.Drawing.Size(380, 60)
        Me.INDLblProduct.MinSize = New System.Drawing.Size(370, 60)
        Me.INDLblProduct.Name = "INDLblProduct"
        Me.INDLblProduct.Size = New System.Drawing.Size(784, 369)
        Me.INDLblProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLblProduct.Text = "Producto :"
        Me.INDLblProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLblProduct.TextSize = New System.Drawing.Size(87, 17)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 264)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(376, 52)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(376, 52)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(416, 52)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Proveedor"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(87, 17)
        '
        'INDLciProduct
        '
        Me.INDLciProduct.Location = New System.Drawing.Point(0, 316)
        Me.INDLciProduct.MaxSize = New System.Drawing.Size(376, 52)
        Me.INDLciProduct.MinSize = New System.Drawing.Size(376, 52)
        Me.INDLciProduct.Name = "INDLciProduct"
        Me.INDLciProduct.Size = New System.Drawing.Size(416, 284)
        Me.INDLciProduct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProduct.Text = "Producto:"
        Me.INDLciProduct.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProduct.TextSize = New System.Drawing.Size(87, 17)
        '
        'INDLciDestinationStore
        '
        Me.INDLciDestinationStore.Location = New System.Drawing.Point(0, 212)
        Me.INDLciDestinationStore.MaxSize = New System.Drawing.Size(376, 52)
        Me.INDLciDestinationStore.MinSize = New System.Drawing.Size(376, 52)
        Me.INDLciDestinationStore.Name = "INDLciDestinationStore"
        Me.INDLciDestinationStore.Size = New System.Drawing.Size(416, 52)
        Me.INDLciDestinationStore.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDestinationStore.Text = "Almacén Destino:"
        Me.INDLciDestinationStore.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDestinationStore.TextSize = New System.Drawing.Size(87, 17)
        '
        'FrmReportConsumtionAverage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1512, 1065)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Name = "FrmReportConsumtionAverage"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Tag = "1717"
        Me.Text = "Listado Ordenes de Traslado"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDDateEnd1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleWarehouse.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDDvDocumentViewer.ResumeLayout(False)
        Me.INDDvDocumentViewer.PerformLayout()
        CType(Me.INDGleReportType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleState.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleSupplier.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INGvSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterRequired, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGcExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciOrderType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilterOptional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblProducto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeSubGroupHandlesBatchEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeSubGroupHandlesExpiryEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbePersonTypeEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeContributionTypeEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeRetentionTypeEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeThirdPartyStatusEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProduct4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProduct1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProduct2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProduct3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProduct4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcDocumentViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcDocumentViewer.ResumeLayout(False)
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbePersonTypeEnd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeContributionTypeEnd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeRetentionTypeEnd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeThirdPartyStatusEnd1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLblProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDestinationStore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDCncNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDPcDocumentViewer As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDDvDocumentViewer As DevExpress.XtraPrinting.Preview.DocumentViewer
    Friend WithEvents INDCnBack As Presentation.Controls.CtrNavigation
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
    Friend WithEvents PrintPreviewBarItem16 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents ZoomBarEditItem1 As DevExpress.XtraPrinting.Preview.ZoomBarEditItem
    Friend WithEvents PrintPreviewRepositoryItemComboBox1 As DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox
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
    Friend WithEvents PrintPreviewBarItem27 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
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
    Friend WithEvents PrintPreviewBarItem28 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents PrintPreviewBarItem29 As DevExpress.XtraPrinting.Preview.PrintPreviewBarItem
    Friend WithEvents BarToolbarsListItem1 As DevExpress.XtraBars.BarToolbarsListItem
    Friend WithEvents PrintPreviewSubItem3 As DevExpress.XtraPrinting.Preview.PrintPreviewSubItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
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
    Friend WithEvents INDLcgFilterRequired As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgFilterOptional As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGleState As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciOrderType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCbePersonTypeEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeContributionTypeEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeRetentionTypeEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeThirdPartyStatusEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleReportType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciReportType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCbeSubGroupHandlesBatchEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeSubGroupHandlesExpiryEnd As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDSbExportExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcExportExcel As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvExportExcel As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciGcExportExcel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
    Friend WithEvents INDCbePersonTypeEnd1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeContributionTypeEnd1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeRetentionTypeEnd1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDCbeThirdPartyStatusEnd1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDDateStart1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Producto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Almacen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Proveedor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AÑO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ENE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FEB As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MAR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ABR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MAY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents JUN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents JUL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AGO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SEP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents OCT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents NOV As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DIC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Promedio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents NombreProducto As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleProduct1 As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProduct1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProduct_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLblProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleProduct2 As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProduct2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProduct_UnboundSelection1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductCode1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductName1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleProduct3 As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProduct3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProduct_UnboundSelection2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductCode2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductName2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleProduct4 As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProduct4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColProduct_UnboundSelection3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductCode3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductName3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleWarehouse As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvWarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLblWarehouse As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleSupplier As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INGvSupplier As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLblSupplier As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProduct As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLblProducto As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciProduct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDestinationStore As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColWareHouse_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColWarehouseCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColWarehouseName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplier_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplierDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProducto_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductoCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColProductoName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSupplierStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDateEnd As SearchLookUpEditEx
    Friend WithEvents INDDateEnd1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
