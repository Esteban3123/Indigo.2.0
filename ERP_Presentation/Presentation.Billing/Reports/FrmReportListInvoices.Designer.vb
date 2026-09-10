Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportListInvoices
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReportListInvoices))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDCncReport = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBaseHome = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcGenerateExcel = New DevExpress.XtraGrid.GridControl()
        Me.INDGvGenerateExcel = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
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
        Me.INDDvReport = New DevExpress.XtraPrinting.Preview.DocumentViewer()
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
        Me.INDSleBranchOffice = New Presentation.Controls.SearchLookUpEditEx()
        Me.INDGvBranchOffice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolBranchOfficeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBranchOfficeDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleFinalInvoice = New Presentation.Controls.SearchLookUpEditEx()
        Me.INDGvFinalInvoice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColFinalInvoicePatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFinalInvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFinalInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFinalInvoiceAdmissionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFinalInvoiceCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColFinalInvoiceIdentification = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleRadicated = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView13 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDRadicatedConsecutive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDCustomerNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDCustomerName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRadicatedDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleOrder = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleStatus = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleInitialInvoice = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleUser = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleAdmissionNumber = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleTypeInvoice = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCategories = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeInvoiceCategories = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameInvoiceCategories = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleHealthAdministrator = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeHealthAdministrator = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameHealthAdministrator = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbGenerateReport = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleThirdParty = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDNitThirdParty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameThirdParty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleGroup = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleTypeReport = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSlePatient = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodePatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNamePatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSegNamePatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDFirstApellidoPatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSecApellidoPatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleCurrency = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn178 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn761 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn771 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgBaseHome = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgCriteria = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciDateStart = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDteEnd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTypeReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciInitialInvoice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTypeInvoice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciOrder = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFinalInvoice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCurrency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgFilters = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciThirdParty = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenerateReport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPatient = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciHealthAdministrator = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCategories = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAdmissionNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciUser = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRadicated = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBranchOffice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExportExcel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPcReport = New DevExpress.XtraEditors.PanelControl()
        Me.INDCnReturn = New Presentation.Controls.CtrNavigation()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoDocumentViewer1 = New Presentation.Controls.IndigoDocumentViewer()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCncReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBaseHome, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBaseHome.SuspendLayout()
        CType(Me.INDGcGenerateExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvGenerateExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDDvReport.SuspendLayout()
        CType(Me.INDSleBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleFinalInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFinalInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleRadicated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleOrder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleInitialInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleAdmissionNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleTypeInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCategories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleHealthAdministrator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlePatient, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBaseHome, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDteEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInitialInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTypeInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFinalInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCurrency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPatient, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciHealthAdministrator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCategories, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAdmissionNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRadicated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcReport.SuspendLayout()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBaseHome)
        Me.INDPanelControlBase.Controls.Add(Me.INDCncReport)
        Me.INDPanelControlBase.Controls.Add(Me.INDPcReport)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 5)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 724)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.Dock = System.Windows.Forms.DockStyle.None
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 75)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDCncReport
        '
        Me.INDCncReport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCncReport.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCncReport.LayoutControl = Me.INDLcBaseHome
        Me.INDCncReport.Location = New System.Drawing.Point(2, 7)
        Me.INDCncReport.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCncReport.Name = "INDCncReport"
        Me.INDCncReport.Size = New System.Drawing.Size(200, 715)
        Me.INDCncReport.TabIndex = 0
        Me.INDCncReport.UseDisabledStatePainter = False
        '
        'INDLcBaseHome
        '
        Me.INDLcBaseHome.Controls.Add(Me.INDGcGenerateExcel)
        Me.INDLcBaseHome.Controls.Add(Me.INDSbExportExcel)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleBranchOffice)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleFinalInvoice)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleRadicated)
        Me.INDLcBaseHome.Controls.Add(Me.INDGleOrder)
        Me.INDLcBaseHome.Controls.Add(Me.INDGleStatus)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleInitialInvoice)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleUser)
        Me.INDLcBaseHome.Controls.Add(Me.INDsleAdmissionNumber)
        Me.INDLcBaseHome.Controls.Add(Me.INDGleTypeInvoice)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleCategories)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleHealthAdministrator)
        Me.INDLcBaseHome.Controls.Add(Me.INDSbGenerateReport)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleThirdParty)
        Me.INDLcBaseHome.Controls.Add(Me.INDSleGroup)
        Me.INDLcBaseHome.Controls.Add(Me.INDGleTypeReport)
        Me.INDLcBaseHome.Controls.Add(Me.INDSlePatient)
        Me.INDLcBaseHome.Controls.Add(Me.INDDateEnd)
        Me.INDLcBaseHome.Controls.Add(Me.INDDateStart)
        Me.INDLcBaseHome.Controls.Add(Me.INDsleCurrency)
        Me.INDLcBaseHome.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBaseHome.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBaseHome.Name = "INDLcBaseHome"
        Me.INDLcBaseHome.Root = Me.INDLcgBaseHome
        Me.INDLcBaseHome.Size = New System.Drawing.Size(804, 715)
        Me.INDLcBaseHome.TabIndex = 1
        Me.INDLcBaseHome.Text = "LayoutControl1"
        '
        'INDGcGenerateExcel
        '
        Me.INDGcGenerateExcel.Location = New System.Drawing.Point(408, 597)
        Me.INDGcGenerateExcel.MainView = Me.INDGvGenerateExcel
        Me.INDGcGenerateExcel.MenuManager = Me.DocumentViewerBarManager1
        Me.INDGcGenerateExcel.Name = "INDGcGenerateExcel"
        Me.INDGcGenerateExcel.Size = New System.Drawing.Size(331, 94)
        Me.INDGcGenerateExcel.TabIndex = 19
        Me.INDGcGenerateExcel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvGenerateExcel})
        '
        'INDGvGenerateExcel
        '
        Me.INDGvGenerateExcel.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvGenerateExcel.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvGenerateExcel.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvGenerateExcel.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvGenerateExcel.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvGenerateExcel.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvGenerateExcel.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvGenerateExcel.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvGenerateExcel.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvGenerateExcel.Appearance.Row.Options.UseFont = True
        Me.INDGvGenerateExcel.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn33, Me.GridColumn20, Me.GridColumn22, Me.GridColumn21, Me.GridColumn30, Me.GridColumn23, Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27, Me.GridColumn28, Me.GridColumn29, Me.GridColumn31, Me.GridColumn32})
        Me.INDGvGenerateExcel.GridControl = Me.INDGcGenerateExcel
        Me.INDGvGenerateExcel.Name = "INDGvGenerateExcel"
        Me.INDGvGenerateExcel.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvGenerateExcel.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvGenerateExcel.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvGenerateExcel, False)
        '
        'GridColumn33
        '
        Me.GridColumn33.Caption = "Código"
        Me.GridColumn33.FieldName = "Code"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.Visible = True
        Me.GridColumn33.VisibleIndex = 0
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Factura"
        Me.GridColumn20.FieldName = "InvoiceNumber"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 1
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Estado"
        Me.GridColumn22.FieldName = "Status"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 2
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Fecha"
        Me.GridColumn21.FieldName = "DocumentDate"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 3
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Detalle"
        Me.GridColumn30.FieldName = "Description"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.Visible = True
        Me.GridColumn30.VisibleIndex = 4
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Nit Cliente"
        Me.GridColumn23.FieldName = "ThirdPartyNit"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 5
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Cliente"
        Me.GridColumn24.FieldName = "ThirdPartyName"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 6
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Sucursal"
        Me.GridColumn25.FieldName = "SucursalName"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 7
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Ciudad"
        Me.GridColumn26.FieldName = "CityName"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 8
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Subtotal"
        Me.GridColumn27.FieldName = "Subtotal"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 9
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "IVA"
        Me.GridColumn28.FieldName = "ValueTax"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Visible = True
        Me.GridColumn28.VisibleIndex = 10
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Valor Facturado"
        Me.GridColumn29.FieldName = "TotalValue"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 11
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Usuario Creación"
        Me.GridColumn31.FieldName = "CreationUser"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 12
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "Usuario Modificación"
        Me.GridColumn32.FieldName = "ConfirmationUser"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 13
        '
        'DocumentViewerBarManager1
        '
        Me.DocumentViewerBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.PreviewBar1, Me.PreviewBar2})
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlTop)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.DocumentViewerBarManager1.DockControls.Add(Me.barDockControlRight)
        Me.DocumentViewerBarManager1.DocumentViewer = Me.INDDvReport
        Me.DocumentViewerBarManager1.Form = Me.INDDvReport
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
        Me.barDockControlTop.Size = New System.Drawing.Size(930, 24)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 689)
        Me.barDockControlBottom.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(930, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 24)
        Me.barDockControlLeft.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 665)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(930, 24)
        Me.barDockControlRight.Manager = Me.DocumentViewerBarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 665)
        '
        'INDDvReport
        '
        Me.INDDvReport.Controls.Add(Me.barDockControlLeft)
        Me.INDDvReport.Controls.Add(Me.barDockControlRight)
        Me.INDDvReport.Controls.Add(Me.barDockControlBottom)
        Me.INDDvReport.Controls.Add(Me.barDockControlTop)
        Me.INDDvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoDocumentViewer1.SetExtendProperties(Me.INDDvReport, True)
        Me.INDDvReport.IsMetric = True
        Me.INDDvReport.Location = New System.Drawing.Point(72, 2)
        Me.INDDvReport.Name = "INDDvReport"
        Me.INDDvReport.Size = New System.Drawing.Size(930, 711)
        Me.INDDvReport.TabIndex = 1
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
        Me.INDSbExportExcel.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.ICONO_EXCEL_02
        Me.INDSbExportExcel.Location = New System.Drawing.Point(741, 565)
        Me.INDSbExportExcel.MaximumSize = New System.Drawing.Size(40, 32)
        Me.INDSbExportExcel.MinimumSize = New System.Drawing.Size(40, 32)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbExportExcel, False)
        Me.INDSbExportExcel.Name = "INDSbExportExcel"
        Me.INDSbExportExcel.Size = New System.Drawing.Size(40, 32)
        Me.INDSbExportExcel.StyleController = Me.INDLcBaseHome
        Me.INDSbExportExcel.TabIndex = 18
        '
        'INDSleBranchOffice
        '
        Me.INDSleBranchOffice.AllowQueryOne = True
        Me.INDSleBranchOffice.Datasource = Nothing
        Me.INDSleBranchOffice.DisplayMember = "{CodeName}"
        Me.INDSleBranchOffice.DisplayNullText = ""
        Me.INDSleBranchOffice.EditValue = Nothing
        Me.INDSleBranchOffice.EnterMoveNextControl = True
        Me.INDSleBranchOffice.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleBranchOffice.IdOpenForm = 0
        Me.INDSleBranchOffice.IsReadOnly = False
        Me.INDSleBranchOffice.Location = New System.Drawing.Point(408, 355)
        Me.INDSleBranchOffice.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleBranchOffice.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleBranchOffice.Name = "INDSleBranchOffice"
        Me.INDSleBranchOffice.PopUpFormSize = New System.Drawing.Size(200, 300)
        Me.INDSleBranchOffice.Size = New System.Drawing.Size(371, 28)
        Me.INDSleBranchOffice.TabIndex = 13
        Me.INDSleBranchOffice.Tag = "Name"
        Me.INDSleBranchOffice.ValueMember = "Id"
        Me.INDSleBranchOffice.View = Me.INDGvBranchOffice
        '
        'INDGvBranchOffice
        '
        Me.INDGvBranchOffice.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvBranchOffice.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvBranchOffice.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvBranchOffice.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvBranchOffice.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBranchOffice.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvBranchOffice.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBranchOffice.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvBranchOffice.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvBranchOffice.Appearance.Row.Options.UseFont = True
        Me.INDGvBranchOffice.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDGvBranchOffice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolBranchOfficeCode, Me.INDColBranchOfficeDescription})
        Me.INDGvBranchOffice.Name = "INDGvBranchOffice"
        Me.INDGvBranchOffice.OptionsCustomization.AllowGroup = False
        Me.INDGvBranchOffice.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvBranchOffice.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvBranchOffice.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvBranchOffice.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvBranchOffice, False)
        '
        'INDcolBranchOfficeCode
        '
        Me.INDcolBranchOfficeCode.Caption = "Código"
        Me.INDcolBranchOfficeCode.FieldName = "Codigo"
        Me.INDcolBranchOfficeCode.Name = "INDcolBranchOfficeCode"
        Me.INDcolBranchOfficeCode.OptionsColumn.AllowEdit = False
        Me.INDcolBranchOfficeCode.Visible = True
        Me.INDcolBranchOfficeCode.VisibleIndex = 0
        '
        'INDColBranchOfficeDescription
        '
        Me.INDColBranchOfficeDescription.Caption = "Descripción"
        Me.INDColBranchOfficeDescription.FieldName = "Descripcion"
        Me.INDColBranchOfficeDescription.Name = "INDColBranchOfficeDescription"
        Me.INDColBranchOfficeDescription.OptionsColumn.AllowEdit = False
        Me.INDColBranchOfficeDescription.Visible = True
        Me.INDColBranchOfficeDescription.VisibleIndex = 1
        '
        'INDSleFinalInvoice
        '
        Me.INDSleFinalInvoice.AllowQueryOne = True
        Me.INDSleFinalInvoice.Datasource = Nothing
        Me.INDSleFinalInvoice.DisplayMember = "{InvoiceNumber}"
        Me.INDSleFinalInvoice.DisplayNullText = ""
        Me.INDSleFinalInvoice.EditValue = Nothing
        Me.INDSleFinalInvoice.EnterMoveNextControl = True
        Me.INDSleFinalInvoice.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleFinalInvoice.IdOpenForm = 0
        Me.INDSleFinalInvoice.IsReadOnly = False
        Me.INDSleFinalInvoice.Location = New System.Drawing.Point(24, 411)
        Me.INDSleFinalInvoice.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleFinalInvoice.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleFinalInvoice.Name = "INDSleFinalInvoice"
        Me.INDSleFinalInvoice.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDSleFinalInvoice.Size = New System.Drawing.Size(356, 28)
        Me.INDSleFinalInvoice.TabIndex = 6
        Me.INDSleFinalInvoice.Tag = "InvoiceNumber"
        Me.INDSleFinalInvoice.ValueMember = "InvoiceNumber"
        Me.INDSleFinalInvoice.View = Me.INDGvFinalInvoice
        '
        'INDGvFinalInvoice
        '
        Me.INDGvFinalInvoice.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFinalInvoice.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFinalInvoice.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFinalInvoice.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFinalInvoice.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFinalInvoice.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFinalInvoice.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFinalInvoice.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFinalInvoice.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFinalInvoice.Appearance.Row.Options.UseFont = True
        Me.INDGvFinalInvoice.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDGvFinalInvoice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColFinalInvoicePatient, Me.INDColFinalInvoiceNumber, Me.INDColFinalInvoiceDate, Me.INDColFinalInvoiceAdmissionNumber, Me.INDColFinalInvoiceCategory, Me.INDColFinalInvoiceIdentification})
        Me.INDGvFinalInvoice.Name = "INDGvFinalInvoice"
        Me.INDGvFinalInvoice.OptionsCustomization.AllowGroup = False
        Me.INDGvFinalInvoice.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvFinalInvoice.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFinalInvoice.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFinalInvoice.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFinalInvoice.OptionsView.ShowDetailButtons = False
        Me.INDGvFinalInvoice.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvFinalInvoice, False)
        '
        'INDColFinalInvoicePatient
        '
        Me.INDColFinalInvoicePatient.Caption = "Paciente"
        Me.INDColFinalInvoicePatient.FieldName = "PatientName"
        Me.INDColFinalInvoicePatient.Name = "INDColFinalInvoicePatient"
        Me.INDColFinalInvoicePatient.OptionsColumn.AllowEdit = False
        Me.INDColFinalInvoicePatient.Visible = True
        Me.INDColFinalInvoicePatient.VisibleIndex = 0
        '
        'INDColFinalInvoiceNumber
        '
        Me.INDColFinalInvoiceNumber.Caption = "No. Factura"
        Me.INDColFinalInvoiceNumber.FieldName = "InvoiceNumber"
        Me.INDColFinalInvoiceNumber.Name = "INDColFinalInvoiceNumber"
        Me.INDColFinalInvoiceNumber.OptionsColumn.AllowEdit = False
        Me.INDColFinalInvoiceNumber.Visible = True
        Me.INDColFinalInvoiceNumber.VisibleIndex = 1
        '
        'INDColFinalInvoiceDate
        '
        Me.INDColFinalInvoiceDate.Caption = "Fecha Documento"
        Me.INDColFinalInvoiceDate.FieldName = "InvoiceDate"
        Me.INDColFinalInvoiceDate.Name = "INDColFinalInvoiceDate"
        Me.INDColFinalInvoiceDate.OptionsColumn.AllowEdit = False
        Me.INDColFinalInvoiceDate.Visible = True
        Me.INDColFinalInvoiceDate.VisibleIndex = 2
        '
        'INDColFinalInvoiceAdmissionNumber
        '
        Me.INDColFinalInvoiceAdmissionNumber.Caption = "No. Ingreso"
        Me.INDColFinalInvoiceAdmissionNumber.FieldName = "AdmissionNumber"
        Me.INDColFinalInvoiceAdmissionNumber.Name = "INDColFinalInvoiceAdmissionNumber"
        Me.INDColFinalInvoiceAdmissionNumber.OptionsColumn.AllowEdit = False
        Me.INDColFinalInvoiceAdmissionNumber.Visible = True
        Me.INDColFinalInvoiceAdmissionNumber.VisibleIndex = 3
        '
        'INDColFinalInvoiceCategory
        '
        Me.INDColFinalInvoiceCategory.Caption = "Categoria"
        Me.INDColFinalInvoiceCategory.FieldName = "InvoiceCategory"
        Me.INDColFinalInvoiceCategory.Name = "INDColFinalInvoiceCategory"
        Me.INDColFinalInvoiceCategory.OptionsColumn.AllowEdit = False
        Me.INDColFinalInvoiceCategory.Visible = True
        Me.INDColFinalInvoiceCategory.VisibleIndex = 4
        '
        'INDColFinalInvoiceIdentification
        '
        Me.INDColFinalInvoiceIdentification.Caption = "Identificación"
        Me.INDColFinalInvoiceIdentification.FieldName = "PatientCode"
        Me.INDColFinalInvoiceIdentification.Name = "INDColFinalInvoiceIdentification"
        Me.INDColFinalInvoiceIdentification.OptionsColumn.AllowEdit = False
        Me.INDColFinalInvoiceIdentification.Visible = True
        Me.INDColFinalInvoiceIdentification.VisibleIndex = 5
        '
        'INDSleRadicated
        '
        Me.INDSleRadicated.AllowQueryOne = True
        Me.INDSleRadicated.Datasource = Nothing
        Me.INDSleRadicated.DisplayMember = "{RadicatedConsecutive} - {RadicatedDate}"
        Me.INDSleRadicated.DisplayNullText = ""
        Me.INDSleRadicated.EditValue = Nothing
        Me.INDSleRadicated.EnterMoveNextControl = True
        Me.INDSleRadicated.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleRadicated.IdOpenForm = 0
        Me.INDSleRadicated.IsReadOnly = False
        Me.INDSleRadicated.Location = New System.Drawing.Point(408, 465)
        Me.INDSleRadicated.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleRadicated.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleRadicated.Name = "INDSleRadicated"
        Me.INDSleRadicated.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDSleRadicated.Size = New System.Drawing.Size(372, 28)
        Me.INDSleRadicated.TabIndex = 15
        Me.INDSleRadicated.Tag = "RadicatedDate"
        Me.INDSleRadicated.ValueMember = "Id"
        Me.INDSleRadicated.View = Me.SearchLookUpEditExView13
        '
        'SearchLookUpEditExView13
        '
        Me.SearchLookUpEditExView13.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView13.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView13.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView13.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView13.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView13.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView13.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView13.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView13.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView13.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView13.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView13.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView13.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDRadicatedConsecutive, Me.INDCustomerNit, Me.INDCustomerName, Me.INDRadicatedDate, Me.INDDocumentDate, Me.INDState})
        Me.SearchLookUpEditExView13.Name = "SearchLookUpEditExView13"
        Me.SearchLookUpEditExView13.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView13.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView13.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView13.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView13.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView13.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView13.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView13, False)
        '
        'INDRadicatedConsecutive
        '
        Me.INDRadicatedConsecutive.Caption = "Consecutivo"
        Me.INDRadicatedConsecutive.FieldName = "RadicatedConsecutive"
        Me.INDRadicatedConsecutive.Name = "INDRadicatedConsecutive"
        Me.INDRadicatedConsecutive.OptionsColumn.AllowEdit = False
        Me.INDRadicatedConsecutive.OptionsColumn.AllowFocus = False
        Me.INDRadicatedConsecutive.Visible = True
        Me.INDRadicatedConsecutive.VisibleIndex = 0
        '
        'INDCustomerNit
        '
        Me.INDCustomerNit.Caption = "Nit Cliente"
        Me.INDCustomerNit.FieldName = "CustomerId.Nit"
        Me.INDCustomerNit.Name = "INDCustomerNit"
        Me.INDCustomerNit.OptionsColumn.AllowEdit = False
        Me.INDCustomerNit.OptionsColumn.AllowFocus = False
        Me.INDCustomerNit.Visible = True
        Me.INDCustomerNit.VisibleIndex = 1
        '
        'INDCustomerName
        '
        Me.INDCustomerName.Caption = "Nombre Cliente"
        Me.INDCustomerName.FieldName = "CustomerId.Name"
        Me.INDCustomerName.Name = "INDCustomerName"
        Me.INDCustomerName.OptionsColumn.AllowEdit = False
        Me.INDCustomerName.OptionsColumn.AllowFocus = False
        Me.INDCustomerName.Visible = True
        Me.INDCustomerName.VisibleIndex = 2
        '
        'INDRadicatedDate
        '
        Me.INDRadicatedDate.Caption = "Fecha Radicado"
        Me.INDRadicatedDate.FieldName = "RadicatedDate"
        Me.INDRadicatedDate.Name = "INDRadicatedDate"
        Me.INDRadicatedDate.OptionsColumn.AllowEdit = False
        Me.INDRadicatedDate.OptionsColumn.AllowFocus = False
        Me.INDRadicatedDate.Visible = True
        Me.INDRadicatedDate.VisibleIndex = 3
        '
        'INDDocumentDate
        '
        Me.INDDocumentDate.Caption = "Fecha Documento"
        Me.INDDocumentDate.FieldName = "DocumentDate"
        Me.INDDocumentDate.Name = "INDDocumentDate"
        Me.INDDocumentDate.OptionsColumn.AllowEdit = False
        Me.INDDocumentDate.OptionsColumn.AllowFocus = False
        Me.INDDocumentDate.Visible = True
        Me.INDDocumentDate.VisibleIndex = 4
        '
        'INDState
        '
        Me.INDState.Caption = "Estado"
        Me.INDState.FieldName = "StateName"
        Me.INDState.Name = "INDState"
        Me.INDState.OptionsColumn.AllowEdit = False
        Me.INDState.OptionsColumn.AllowFocus = False
        Me.INDState.Visible = True
        Me.INDState.VisibleIndex = 5
        '
        'INDGleOrder
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleOrder, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleOrder, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleOrder, False)
        Me.INDGleOrder.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleOrder, True)
        Me.INDGleOrder.Location = New System.Drawing.Point(24, 465)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleOrder, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleOrder.MenuManager = Me.DocumentViewerBarManager1
        Me.INDGleOrder.Name = "INDGleOrder"
        Me.INDGleOrder.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleOrder.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleOrder.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleOrder.Properties.Appearance.Options.UseFont = True
        Me.INDGleOrder.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleOrder.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleOrder.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleOrder.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleOrder.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleOrder.Properties.DisplayMember = "Item2"
        Me.INDGleOrder.Properties.ImmediatePopup = True
        Me.INDGleOrder.Properties.NullText = ""
        Me.INDGleOrder.Properties.PopupView = Me.GridView3
        Me.INDGleOrder.Properties.ValueMember = "Item1"
        Me.INDGleOrder.Size = New System.Drawing.Size(356, 28)
        Me.INDGleOrder.StyleController = Me.INDLcBaseHome
        Me.INDGleOrder.TabIndex = 7
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleOrder, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleOrder, 0)
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
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDOrder})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'INDOrder
        '
        Me.INDOrder.Caption = "Ordenar Por"
        Me.INDOrder.FieldName = "Item2"
        Me.INDOrder.Name = "INDOrder"
        Me.INDOrder.Visible = True
        Me.INDOrder.VisibleIndex = 0
        '
        'INDGleStatus
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleStatus, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleStatus, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleStatus, False)
        Me.INDGleStatus.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleStatus, True)
        Me.INDGleStatus.Location = New System.Drawing.Point(24, 297)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleStatus, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleStatus.MenuManager = Me.DocumentViewerBarManager1
        Me.INDGleStatus.Name = "INDGleStatus"
        Me.INDGleStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleStatus.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleStatus.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleStatus.Properties.Appearance.Options.UseFont = True
        Me.INDGleStatus.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleStatus.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleStatus.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleStatus.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleStatus.Properties.DisplayMember = "Item2"
        Me.INDGleStatus.Properties.ImmediatePopup = True
        Me.INDGleStatus.Properties.NullText = ""
        Me.INDGleStatus.Properties.PopupView = Me.GridView2
        Me.INDGleStatus.Properties.ValueMember = "Item1"
        Me.INDGleStatus.Size = New System.Drawing.Size(356, 28)
        Me.INDGleStatus.StyleController = Me.INDLcBaseHome
        Me.INDGleStatus.TabIndex = 4
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleStatus, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleStatus, 0)
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
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn18})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Estado"
        Me.GridColumn18.FieldName = "Item2"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 0
        '
        'INDSleInitialInvoice
        '
        Me.INDSleInitialInvoice.AllowQueryOne = True
        Me.INDSleInitialInvoice.Datasource = Nothing
        Me.INDSleInitialInvoice.DisplayMember = "{InvoiceNumber}"
        Me.INDSleInitialInvoice.DisplayNullText = ""
        Me.INDSleInitialInvoice.EditValue = Nothing
        Me.INDSleInitialInvoice.EnterMoveNextControl = True
        Me.INDSleInitialInvoice.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleInitialInvoice.IdOpenForm = 0
        Me.INDSleInitialInvoice.IsReadOnly = False
        Me.INDSleInitialInvoice.Location = New System.Drawing.Point(24, 355)
        Me.INDSleInitialInvoice.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleInitialInvoice.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleInitialInvoice.Name = "INDSleInitialInvoice"
        Me.INDSleInitialInvoice.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDSleInitialInvoice.Size = New System.Drawing.Size(356, 28)
        Me.INDSleInitialInvoice.TabIndex = 5
        Me.INDSleInitialInvoice.Tag = "InvoiceNumber"
        Me.INDSleInitialInvoice.ValueMember = "InvoiceNumber"
        Me.INDSleInitialInvoice.View = Me.SearchLookUpEditExView10
        '
        'SearchLookUpEditExView10
        '
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView10.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView10.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView10.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView10.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView10.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView10.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView10.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView10.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView10.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn17, Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn19})
        Me.SearchLookUpEditExView10.Name = "SearchLookUpEditExView10"
        Me.SearchLookUpEditExView10.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView10.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView10.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView10.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView10.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView10.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView10.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView10, False)
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Paciente"
        Me.GridColumn17.FieldName = "PatientName"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 0
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "No. Factura"
        Me.GridColumn13.FieldName = "InvoiceNumber"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 1
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Fecha Documento"
        Me.GridColumn14.FieldName = "InvoiceDate"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 2
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "No. Ingreso"
        Me.GridColumn15.FieldName = "AdmissionNumber"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 3
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Categoria"
        Me.GridColumn16.FieldName = "InvoiceCategory"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 4
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Identificación"
        Me.GridColumn19.FieldName = "PatientCode"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 5
        '
        'INDSleUser
        '
        Me.INDSleUser.AllowQueryOne = True
        Me.INDSleUser.Datasource = Nothing
        Me.INDSleUser.DisplayMember = "{UserCode}"
        Me.INDSleUser.DisplayNullText = ""
        Me.INDSleUser.EditValue = Nothing
        Me.INDSleUser.EnterMoveNextControl = True
        Me.INDSleUser.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleUser.IdOpenForm = 0
        Me.INDSleUser.IsReadOnly = False
        Me.INDSleUser.Location = New System.Drawing.Point(408, 521)
        Me.INDSleUser.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleUser.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleUser.Name = "INDSleUser"
        Me.INDSleUser.PopUpFormSize = New System.Drawing.Size(200, 300)
        Me.INDSleUser.Size = New System.Drawing.Size(372, 28)
        Me.INDSleUser.TabIndex = 16
        Me.INDSleUser.Tag = "UserCode"
        Me.INDSleUser.ValueMember = "UserCode"
        Me.INDSleUser.View = Me.SearchLookUpEditExView9
        '
        'SearchLookUpEditExView9
        '
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView9.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView9.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView9.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView9.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView9.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView9.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView9.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView9.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn12})
        Me.SearchLookUpEditExView9.Name = "SearchLookUpEditExView9"
        Me.SearchLookUpEditExView9.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView9.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView9.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEditExView9.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView9.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView9.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView9, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Código"
        Me.GridColumn11.FieldName = "UserCode"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Nombre"
        Me.GridColumn12.FieldName = "IdPerson.Fullname"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 1
        '
        'INDsleAdmissionNumber
        '
        Me.INDsleAdmissionNumber.AllowQueryOne = True
        Me.INDsleAdmissionNumber.Datasource = Nothing
        Me.INDsleAdmissionNumber.DisplayMember = "{AdmissionCode}"
        Me.INDsleAdmissionNumber.DisplayNullText = ""
        Me.INDsleAdmissionNumber.EditValue = Nothing
        Me.INDsleAdmissionNumber.EnterMoveNextControl = True
        Me.INDsleAdmissionNumber.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDsleAdmissionNumber.IdOpenForm = 0
        Me.INDsleAdmissionNumber.IsReadOnly = False
        Me.INDsleAdmissionNumber.Location = New System.Drawing.Point(408, 409)
        Me.INDsleAdmissionNumber.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDsleAdmissionNumber.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDsleAdmissionNumber.Name = "INDsleAdmissionNumber"
        Me.INDsleAdmissionNumber.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDsleAdmissionNumber.Size = New System.Drawing.Size(372, 28)
        Me.INDsleAdmissionNumber.TabIndex = 14
        Me.INDsleAdmissionNumber.Tag = "AdmissionCode"
        Me.INDsleAdmissionNumber.ValueMember = "AdmissionCode"
        Me.INDsleAdmissionNumber.View = Me.SearchLookUpEditExView8
        '
        'SearchLookUpEditExView8
        '
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView8.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView8.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView8.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView8.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView8.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView8.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn3, Me.GridColumn8, Me.GridColumn9, Me.GridColumn4, Me.GridColumn7, Me.GridColumn6, Me.GridColumn10})
        Me.SearchLookUpEditExView8.Name = "SearchLookUpEditExView8"
        Me.SearchLookUpEditExView8.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView8.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView8.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEditExView8.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView8.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView8.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView8.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView8.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView8, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Paciente"
        Me.GridColumn5.FieldName = "PatientName"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Numero Ingreso"
        Me.GridColumn3.FieldName = "AdmissionCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Estancia Cama"
        Me.GridColumn8.FieldName = "BedStay"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 2
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "T. Liquidación"
        Me.GridColumn9.FieldName = "LiquidationTypeName"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 3
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Identificación"
        Me.GridColumn4.FieldName = "PatientCode"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "T. Ingreso"
        Me.GridColumn7.FieldName = "AdmissionTypeName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Fecha"
        Me.GridColumn6.FieldName = "AdmissionDate"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Acudiente"
        Me.GridColumn10.FieldName = "ResponsibleName"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 7
        '
        'INDGleTypeInvoice
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleTypeInvoice, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleTypeInvoice, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleTypeInvoice, False)
        Me.INDGleTypeInvoice.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleTypeInvoice, True)
        Me.INDGleTypeInvoice.Location = New System.Drawing.Point(24, 241)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleTypeInvoice, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleTypeInvoice.MenuManager = Me.DocumentViewerBarManager1
        Me.INDGleTypeInvoice.Name = "INDGleTypeInvoice"
        Me.INDGleTypeInvoice.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleTypeInvoice.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTypeInvoice.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleTypeInvoice.Properties.Appearance.Options.UseFont = True
        Me.INDGleTypeInvoice.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeInvoice.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleTypeInvoice.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleTypeInvoice.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleTypeInvoice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleTypeInvoice.Properties.DisplayMember = "Item2"
        Me.INDGleTypeInvoice.Properties.ImmediatePopup = True
        Me.INDGleTypeInvoice.Properties.NullText = ""
        Me.INDGleTypeInvoice.Properties.PopupView = Me.GridView1
        Me.INDGleTypeInvoice.Properties.ValueMember = "Item1"
        Me.INDGleTypeInvoice.Size = New System.Drawing.Size(356, 28)
        Me.INDGleTypeInvoice.StyleController = Me.INDLcBaseHome
        Me.INDGleTypeInvoice.TabIndex = 3
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleTypeInvoice, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleTypeInvoice, 0)
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
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
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
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo Factura"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'INDSleCategories
        '
        Me.INDSleCategories.AllowQueryOne = True
        Me.INDSleCategories.Datasource = Nothing
        Me.INDSleCategories.DisplayMember = "{Code} - {Name}"
        Me.INDSleCategories.DisplayNullText = ""
        Me.INDSleCategories.EditValue = Nothing
        Me.INDSleCategories.EnterMoveNextControl = True
        Me.INDSleCategories.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleCategories.IdOpenForm = 0
        Me.INDSleCategories.IsReadOnly = False
        Me.INDSleCategories.Location = New System.Drawing.Point(408, 243)
        Me.INDSleCategories.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleCategories.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleCategories.Name = "INDSleCategories"
        Me.INDSleCategories.PopUpFormSize = New System.Drawing.Size(200, 300)
        Me.INDSleCategories.Size = New System.Drawing.Size(371, 28)
        Me.INDSleCategories.TabIndex = 11
        Me.INDSleCategories.Tag = "Name"
        Me.INDSleCategories.ValueMember = "Id"
        Me.INDSleCategories.View = Me.SearchLookUpEditExView6
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
        Me.SearchLookUpEditExView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeInvoiceCategories, Me.INDNameInvoiceCategories})
        Me.SearchLookUpEditExView6.Name = "SearchLookUpEditExView6"
        Me.SearchLookUpEditExView6.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView6.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView6.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView6.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView6.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView6.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView6.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView6, False)
        '
        'INDCodeInvoiceCategories
        '
        Me.INDCodeInvoiceCategories.Caption = "Codigo"
        Me.INDCodeInvoiceCategories.FieldName = "Code"
        Me.INDCodeInvoiceCategories.Name = "INDCodeInvoiceCategories"
        Me.INDCodeInvoiceCategories.OptionsColumn.AllowEdit = False
        Me.INDCodeInvoiceCategories.Visible = True
        Me.INDCodeInvoiceCategories.VisibleIndex = 0
        '
        'INDNameInvoiceCategories
        '
        Me.INDNameInvoiceCategories.Caption = "Nombre"
        Me.INDNameInvoiceCategories.FieldName = "Name"
        Me.INDNameInvoiceCategories.Name = "INDNameInvoiceCategories"
        Me.INDNameInvoiceCategories.OptionsColumn.AllowEdit = False
        Me.INDNameInvoiceCategories.Visible = True
        Me.INDNameInvoiceCategories.VisibleIndex = 1
        '
        'INDSleHealthAdministrator
        '
        Me.INDSleHealthAdministrator.AllowQueryOne = True
        Me.INDSleHealthAdministrator.Datasource = Nothing
        Me.INDSleHealthAdministrator.DisplayMember = "{Code} - {Name}"
        Me.INDSleHealthAdministrator.DisplayNullText = ""
        Me.INDSleHealthAdministrator.EditValue = Nothing
        Me.INDSleHealthAdministrator.EnterMoveNextControl = True
        Me.INDSleHealthAdministrator.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleHealthAdministrator.IdOpenForm = 0
        Me.INDSleHealthAdministrator.IsReadOnly = False
        Me.INDSleHealthAdministrator.Location = New System.Drawing.Point(408, 75)
        Me.INDSleHealthAdministrator.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleHealthAdministrator.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleHealthAdministrator.Name = "INDSleHealthAdministrator"
        Me.INDSleHealthAdministrator.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDSleHealthAdministrator.Size = New System.Drawing.Size(371, 28)
        Me.INDSleHealthAdministrator.TabIndex = 8
        Me.INDSleHealthAdministrator.Tag = "Name"
        Me.INDSleHealthAdministrator.ValueMember = "Id"
        Me.INDSleHealthAdministrator.View = Me.SearchLookUpEditExView2
        '
        'SearchLookUpEditExView2
        '
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView2.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView2.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeHealthAdministrator, Me.INDNameHealthAdministrator})
        Me.SearchLookUpEditExView2.Name = "SearchLookUpEditExView2"
        Me.SearchLookUpEditExView2.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView2.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView2.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView2.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView2.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView2, False)
        '
        'INDCodeHealthAdministrator
        '
        Me.INDCodeHealthAdministrator.Caption = "Codigo"
        Me.INDCodeHealthAdministrator.FieldName = "Code"
        Me.INDCodeHealthAdministrator.Name = "INDCodeHealthAdministrator"
        Me.INDCodeHealthAdministrator.OptionsColumn.AllowEdit = False
        Me.INDCodeHealthAdministrator.Visible = True
        Me.INDCodeHealthAdministrator.VisibleIndex = 0
        '
        'INDNameHealthAdministrator
        '
        Me.INDNameHealthAdministrator.Caption = "Nombre"
        Me.INDNameHealthAdministrator.FieldName = "Name"
        Me.INDNameHealthAdministrator.Name = "INDNameHealthAdministrator"
        Me.INDNameHealthAdministrator.OptionsColumn.AllowEdit = False
        Me.INDNameHealthAdministrator.Visible = True
        Me.INDNameHealthAdministrator.VisibleIndex = 1
        '
        'INDSbGenerateReport
        '
        Me.INDSbGenerateReport.Location = New System.Drawing.Point(408, 565)
        Me.INDSbGenerateReport.MaximumSize = New System.Drawing.Size(331, 32)
        Me.INDSbGenerateReport.MinimumSize = New System.Drawing.Size(331, 32)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateReport, False)
        Me.INDSbGenerateReport.Name = "INDSbGenerateReport"
        Me.INDSbGenerateReport.Size = New System.Drawing.Size(331, 32)
        Me.INDSbGenerateReport.StyleController = Me.INDLcBaseHome
        Me.INDSbGenerateReport.TabIndex = 17
        Me.INDSbGenerateReport.Text = "Generar Reporte"
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
        Me.INDSleThirdParty.IdOpenForm = 0
        Me.INDSleThirdParty.IsReadOnly = False
        Me.INDSleThirdParty.Location = New System.Drawing.Point(408, 299)
        Me.INDSleThirdParty.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleThirdParty.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleThirdParty.Name = "INDSleThirdParty"
        Me.INDSleThirdParty.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDSleThirdParty.Size = New System.Drawing.Size(371, 28)
        Me.INDSleThirdParty.TabIndex = 12
        Me.INDSleThirdParty.Tag = "Name"
        Me.INDSleThirdParty.ValueMember = "Id"
        Me.INDSleThirdParty.View = Me.SearchLookUpEditExView5
        '
        'SearchLookUpEditExView5
        '
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView5.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView5.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView5.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView5.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView5.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView5.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDNitThirdParty, Me.INDNameThirdParty})
        Me.SearchLookUpEditExView5.Name = "SearchLookUpEditExView5"
        Me.SearchLookUpEditExView5.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView5.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView5.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView5.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView5.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView5.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView5.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView5, False)
        '
        'INDNitThirdParty
        '
        Me.INDNitThirdParty.Caption = "Nit"
        Me.INDNitThirdParty.FieldName = "Nit"
        Me.INDNitThirdParty.Name = "INDNitThirdParty"
        Me.INDNitThirdParty.OptionsColumn.AllowEdit = False
        Me.INDNitThirdParty.Visible = True
        Me.INDNitThirdParty.VisibleIndex = 0
        '
        'INDNameThirdParty
        '
        Me.INDNameThirdParty.Caption = "Nombre"
        Me.INDNameThirdParty.FieldName = "Name"
        Me.INDNameThirdParty.Name = "INDNameThirdParty"
        Me.INDNameThirdParty.OptionsColumn.AllowEdit = False
        Me.INDNameThirdParty.Visible = True
        Me.INDNameThirdParty.VisibleIndex = 1
        '
        'INDSleGroup
        '
        Me.INDSleGroup.AllowQueryOne = True
        Me.INDSleGroup.Datasource = Nothing
        Me.INDSleGroup.DisplayMember = "{Code} - {Name}"
        Me.INDSleGroup.DisplayNullText = ""
        Me.INDSleGroup.EditValue = Nothing
        Me.INDSleGroup.EnterMoveNextControl = True
        Me.INDSleGroup.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleGroup.IdOpenForm = 0
        Me.INDSleGroup.IsReadOnly = False
        Me.INDSleGroup.Location = New System.Drawing.Point(408, 187)
        Me.INDSleGroup.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleGroup.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleGroup.Name = "INDSleGroup"
        Me.INDSleGroup.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDSleGroup.Size = New System.Drawing.Size(371, 28)
        Me.INDSleGroup.TabIndex = 10
        Me.INDSleGroup.Tag = "Name"
        Me.INDSleGroup.ValueMember = "Id"
        Me.INDSleGroup.View = Me.SearchLookUpEditExView4
        '
        'SearchLookUpEditExView4
        '
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView4.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView4.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeGroup, Me.INDNameGroup})
        Me.SearchLookUpEditExView4.Name = "SearchLookUpEditExView4"
        Me.SearchLookUpEditExView4.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView4.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView4.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView4.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView4.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView4, False)
        '
        'INDCodeGroup
        '
        Me.INDCodeGroup.Caption = "Codigo"
        Me.INDCodeGroup.FieldName = "Code"
        Me.INDCodeGroup.Name = "INDCodeGroup"
        Me.INDCodeGroup.OptionsColumn.AllowEdit = False
        Me.INDCodeGroup.Visible = True
        Me.INDCodeGroup.VisibleIndex = 0
        '
        'INDNameGroup
        '
        Me.INDNameGroup.Caption = "Nombre"
        Me.INDNameGroup.FieldName = "Name"
        Me.INDNameGroup.Name = "INDNameGroup"
        Me.INDNameGroup.OptionsColumn.AllowEdit = False
        Me.INDNameGroup.Visible = True
        Me.INDNameGroup.VisibleIndex = 1
        '
        'INDGleTypeReport
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleTypeReport, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleTypeReport, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleTypeReport, False)
        Me.INDGleTypeReport.Location = New System.Drawing.Point(24, 185)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleTypeReport, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleTypeReport.MenuManager = Me.DocumentViewerBarManager1
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
        Me.INDGleTypeReport.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleTypeReport.Properties.ValueMember = "Item1"
        Me.INDGleTypeReport.Size = New System.Drawing.Size(356, 28)
        Me.INDGleTypeReport.StyleController = Me.INDLcBaseHome
        Me.INDGleTypeReport.TabIndex = 2
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleTypeReport, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleTypeReport, 0)
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
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.GridColumn1.Caption = "Tipo de Reporte"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDSlePatient
        '
        Me.INDSlePatient.AllowQueryOne = True
        Me.INDSlePatient.Datasource = Nothing
        Me.INDSlePatient.DisplayMember = "{IPCODPACI} - {IPNOMCOMP}"
        Me.INDSlePatient.DisplayNullText = ""
        Me.INDSlePatient.EditValue = Nothing
        Me.INDSlePatient.EnterMoveNextControl = True
        Me.INDSlePatient.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSlePatient.IdOpenForm = 0
        Me.INDSlePatient.IsReadOnly = False
        Me.INDSlePatient.Location = New System.Drawing.Point(408, 131)
        Me.INDSlePatient.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSlePatient.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSlePatient.Name = "INDSlePatient"
        Me.INDSlePatient.PopUpFormSize = New System.Drawing.Size(800, 400)
        Me.INDSlePatient.Size = New System.Drawing.Size(371, 28)
        Me.INDSlePatient.TabIndex = 9
        Me.INDSlePatient.Tag = "IPNOMCOMP"
        Me.INDSlePatient.ValueMember = "IPCODPACI"
        Me.INDSlePatient.View = Me.SearchLookUpEditExView1
        '
        'SearchLookUpEditExView1
        '
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodePatient, Me.INDNamePatient, Me.INDSegNamePatient, Me.INDFirstApellidoPatient, Me.INDSecApellidoPatient})
        Me.SearchLookUpEditExView1.Name = "SearchLookUpEditExView1"
        Me.SearchLookUpEditExView1.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView1.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView1, False)
        '
        'INDCodePatient
        '
        Me.INDCodePatient.Caption = "Codigo"
        Me.INDCodePatient.FieldName = "IPCODPACI"
        Me.INDCodePatient.Name = "INDCodePatient"
        Me.INDCodePatient.OptionsColumn.AllowEdit = False
        Me.INDCodePatient.Visible = True
        Me.INDCodePatient.VisibleIndex = 0
        '
        'INDNamePatient
        '
        Me.INDNamePatient.Caption = "Primer Nombre"
        Me.INDNamePatient.FieldName = "IPPRINOMB"
        Me.INDNamePatient.Name = "INDNamePatient"
        Me.INDNamePatient.OptionsColumn.AllowEdit = False
        Me.INDNamePatient.Visible = True
        Me.INDNamePatient.VisibleIndex = 1
        '
        'INDSegNamePatient
        '
        Me.INDSegNamePatient.Caption = "Segundo Nombre"
        Me.INDSegNamePatient.FieldName = "IPSEGNOMB"
        Me.INDSegNamePatient.Name = "INDSegNamePatient"
        Me.INDSegNamePatient.OptionsColumn.AllowEdit = False
        Me.INDSegNamePatient.Visible = True
        Me.INDSegNamePatient.VisibleIndex = 2
        '
        'INDFirstApellidoPatient
        '
        Me.INDFirstApellidoPatient.Caption = "Primer Apellido"
        Me.INDFirstApellidoPatient.FieldName = "IPPRIAPEL"
        Me.INDFirstApellidoPatient.Name = "INDFirstApellidoPatient"
        Me.INDFirstApellidoPatient.OptionsColumn.AllowEdit = False
        Me.INDFirstApellidoPatient.Visible = True
        Me.INDFirstApellidoPatient.VisibleIndex = 3
        '
        'INDSecApellidoPatient
        '
        Me.INDSecApellidoPatient.Caption = "Segundo Apellido"
        Me.INDSecApellidoPatient.FieldName = "IPSEGAPEL"
        Me.INDSecApellidoPatient.Name = "INDSecApellidoPatient"
        Me.INDSecApellidoPatient.OptionsColumn.AllowEdit = False
        Me.INDSecApellidoPatient.Visible = True
        Me.INDSecApellidoPatient.VisibleIndex = 4
        '
        'INDDateEnd
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDateEnd, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateEnd, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDateEnd, False)
        Me.INDDateEnd.EditValue = Nothing
        Me.INDDateEnd.EnterMoveNextControl = True
        Me.INDDateEnd.Location = New System.Drawing.Point(24, 129)
        Me.IndigoTextEdit1.SetMascara(Me.INDDateEnd, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateEnd, Presentation.Controls.IndigoDate.EMask.FechaHora)
        Me.INDDateEnd.MenuManager = Me.DocumentViewerBarManager1
        Me.INDDateEnd.Name = "INDDateEnd"
        Me.INDDateEnd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDateEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd.Properties.Appearance.Options.UseBackColor = True
        Me.INDDateEnd.Properties.Appearance.Options.UseFont = True
        Me.INDDateEnd.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateEnd.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateEnd.Properties.Mask.EditMask = "dd/MM/yyyy hh:mm tt"
        Me.INDDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateEnd.Size = New System.Drawing.Size(356, 28)
        Me.INDDateEnd.StyleController = Me.INDLcBaseHome
        Me.INDDateEnd.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDateEnd, 0)
        '
        'INDDateStart
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDateStart, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDateStart, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDateStart, False)
        Me.INDDateStart.EditValue = Nothing
        Me.INDDateStart.EnterMoveNextControl = True
        Me.INDDateStart.Location = New System.Drawing.Point(24, 73)
        Me.IndigoTextEdit1.SetMascara(Me.INDDateStart, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDateStart, Presentation.Controls.IndigoDate.EMask.FechaHora)
        Me.INDDateStart.MenuManager = Me.DocumentViewerBarManager1
        Me.INDDateStart.Name = "INDDateStart"
        Me.INDDateStart.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDateStart.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart.Properties.Appearance.Options.UseBackColor = True
        Me.INDDateStart.Properties.Appearance.Options.UseFont = True
        Me.INDDateStart.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDateStart.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDateStart.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDateStart.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDateStart.Properties.Mask.EditMask = "dd/MM/yyyy hh:mm tt"
        Me.INDDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDateStart.Size = New System.Drawing.Size(356, 28)
        Me.INDDateStart.StyleController = Me.INDLcBaseHome
        Me.INDDateStart.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDateStart, 0)
        '
        'INDsleCurrency
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCurrency, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCurrency, AppearanceObject2)
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
        Me.INDsleCurrency.Location = New System.Drawing.Point(24, 523)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCurrency, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDsleCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCurrency.Properties.DisplayMember = "CurrencyName"
        Me.INDsleCurrency.Properties.NullText = ""
        Me.INDsleCurrency.Properties.PopupSizeable = False
        Me.INDsleCurrency.Properties.PopupView = Me.SearchLookUpEdit1View1
        Me.INDsleCurrency.Properties.ShowClearButton = False
        Me.INDsleCurrency.Properties.ShowFooter = False
        Me.INDsleCurrency.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCurrency, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCurrency, False)
        Me.INDsleCurrency.Size = New System.Drawing.Size(356, 28)
        Me.INDsleCurrency.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCurrency, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCurrency, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCurrency, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCurrency, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCurrency, False)
        '
        'SearchLookUpEdit1View1
        '
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View1.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View1.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View1.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn178, Me.GridColumn761, Me.GridColumn771})
        Me.SearchLookUpEdit1View1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View1.Name = "SearchLookUpEdit1View1"
        Me.SearchLookUpEdit1View1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View1.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View1, False)
        '
        'GridColumn178
        '
        Me.GridColumn178.Caption = "Código"
        Me.GridColumn178.FieldName = "Codigo"
        Me.GridColumn178.Name = "GridColumn178"
        Me.GridColumn178.Visible = True
        Me.GridColumn178.VisibleIndex = 0
        '
        'GridColumn761
        '
        Me.GridColumn761.Caption = "Nombre"
        Me.GridColumn761.FieldName = "CurrencyName"
        Me.GridColumn761.Name = "GridColumn761"
        Me.GridColumn761.Visible = True
        Me.GridColumn761.VisibleIndex = 1
        '
        'GridColumn771
        '
        Me.GridColumn771.Caption = "Abreviación"
        Me.GridColumn771.FieldName = "Abbreviation"
        Me.GridColumn771.Name = "GridColumn771"
        Me.GridColumn771.Visible = True
        Me.GridColumn771.VisibleIndex = 2
        '
        'INDLcgBaseHome
        '
        Me.INDLcgBaseHome.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBaseHome.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBaseHome.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBaseHome.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBaseHome.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBaseHome.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBaseHome.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBaseHome.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBaseHome.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBaseHome.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBaseHome.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBaseHome.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBaseHome.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBaseHome.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBaseHome, False)
        Me.INDLcgBaseHome.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBaseHome.GroupBordersVisible = False
        Me.INDLcgBaseHome.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgCriteria, Me.INDLcgFilters})
        Me.INDLcgBaseHome.Name = "Root"
        Me.INDLcgBaseHome.Size = New System.Drawing.Size(804, 715)
        Me.INDLcgBaseHome.TextVisible = False
        '
        'INDLcgCriteria
        '
        Me.INDLcgCriteria.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCriteria.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCriteria.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCriteria, False)
        Me.INDLcgCriteria.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciDateStart, Me.INDLciDteEnd, Me.INDLciTypeReport, Me.INDLciInitialInvoice, Me.INDLciTypeInvoice, Me.INDLciStatus, Me.INDLciOrder, Me.INDLciFinalInvoice, Me.INDlyCurrency})
        Me.INDLcgCriteria.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgCriteria.Name = "INDLcgCriteria"
        Me.INDLcgCriteria.Size = New System.Drawing.Size(384, 695)
        Me.INDLcgCriteria.Text = "Criterios"
        '
        'INDLciDateStart
        '
        Me.INDLciDateStart.Control = Me.INDDateStart
        Me.INDLciDateStart.Location = New System.Drawing.Point(0, 0)
        Me.INDLciDateStart.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.Name = "INDLciDateStart"
        Me.INDLciDateStart.Size = New System.Drawing.Size(360, 56)
        Me.INDLciDateStart.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDateStart.Text = "Fecha Inicial:"
        Me.INDLciDateStart.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDateStart.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciDteEnd
        '
        Me.INDLciDteEnd.Control = Me.INDDateEnd
        Me.INDLciDteEnd.Location = New System.Drawing.Point(0, 56)
        Me.INDLciDteEnd.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciDteEnd.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciDteEnd.Name = "INDLciDteEnd"
        Me.INDLciDteEnd.Size = New System.Drawing.Size(360, 56)
        Me.INDLciDteEnd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDteEnd.Text = "Fecha Final:"
        Me.INDLciDteEnd.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDteEnd.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciTypeReport
        '
        Me.INDLciTypeReport.Control = Me.INDGleTypeReport
        Me.INDLciTypeReport.Location = New System.Drawing.Point(0, 112)
        Me.INDLciTypeReport.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciTypeReport.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciTypeReport.Name = "INDLciTypeReport"
        Me.INDLciTypeReport.Size = New System.Drawing.Size(360, 56)
        Me.INDLciTypeReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTypeReport.Text = "Tipo de Reporte:"
        Me.INDLciTypeReport.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTypeReport.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciInitialInvoice
        '
        Me.INDLciInitialInvoice.Control = Me.INDSleInitialInvoice
        Me.INDLciInitialInvoice.CustomizationFormText = "Factura Inicial:"
        Me.INDLciInitialInvoice.Location = New System.Drawing.Point(0, 280)
        Me.INDLciInitialInvoice.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciInitialInvoice.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciInitialInvoice.Name = "INDLciInitialInvoice"
        Me.INDLciInitialInvoice.Size = New System.Drawing.Size(360, 56)
        Me.INDLciInitialInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInitialInvoice.Text = "Factura Inicial:"
        Me.INDLciInitialInvoice.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciInitialInvoice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciInitialInvoice.TextSize = New System.Drawing.Size(128, 17)
        Me.INDLciInitialInvoice.TextToControlDistance = 5
        '
        'INDLciTypeInvoice
        '
        Me.INDLciTypeInvoice.Control = Me.INDGleTypeInvoice
        Me.INDLciTypeInvoice.Location = New System.Drawing.Point(0, 168)
        Me.INDLciTypeInvoice.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciTypeInvoice.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciTypeInvoice.Name = "INDLciTypeInvoice"
        Me.INDLciTypeInvoice.Size = New System.Drawing.Size(360, 56)
        Me.INDLciTypeInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTypeInvoice.Text = "Tipo Factura:"
        Me.INDLciTypeInvoice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTypeInvoice.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciStatus
        '
        Me.INDLciStatus.Control = Me.INDGleStatus
        Me.INDLciStatus.Location = New System.Drawing.Point(0, 224)
        Me.INDLciStatus.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciStatus.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciStatus.Name = "INDLciStatus"
        Me.INDLciStatus.Size = New System.Drawing.Size(360, 56)
        Me.INDLciStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStatus.Text = "Estado:"
        Me.INDLciStatus.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciStatus.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciOrder
        '
        Me.INDLciOrder.Control = Me.INDGleOrder
        Me.INDLciOrder.Location = New System.Drawing.Point(0, 392)
        Me.INDLciOrder.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciOrder.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciOrder.Name = "INDLciOrder"
        Me.INDLciOrder.Size = New System.Drawing.Size(360, 56)
        Me.INDLciOrder.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciOrder.Text = "Ordenar Por:"
        Me.INDLciOrder.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciOrder.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciFinalInvoice
        '
        Me.INDLciFinalInvoice.Control = Me.INDSleFinalInvoice
        Me.INDLciFinalInvoice.CustomizationFormText = "Factura Final:"
        Me.INDLciFinalInvoice.Location = New System.Drawing.Point(0, 336)
        Me.INDLciFinalInvoice.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDLciFinalInvoice.MinSize = New System.Drawing.Size(360, 56)
        Me.INDLciFinalInvoice.Name = "INDLciFinalInvoice"
        Me.INDLciFinalInvoice.Size = New System.Drawing.Size(360, 56)
        Me.INDLciFinalInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFinalInvoice.Text = "Factura Final:"
        Me.INDLciFinalInvoice.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFinalInvoice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFinalInvoice.TextSize = New System.Drawing.Size(128, 17)
        Me.INDLciFinalInvoice.TextToControlDistance = 5
        '
        'INDlyCurrency
        '
        Me.INDlyCurrency.Control = Me.INDsleCurrency
        Me.INDlyCurrency.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyCurrency.CustomizationFormText = "Moneda de reporte:"
        Me.INDlyCurrency.Location = New System.Drawing.Point(0, 448)
        Me.INDlyCurrency.MaxSize = New System.Drawing.Size(360, 56)
        Me.INDlyCurrency.MinSize = New System.Drawing.Size(360, 56)
        Me.INDlyCurrency.Name = "INDlyCurrency"
        Me.INDlyCurrency.Size = New System.Drawing.Size(360, 194)
        Me.INDlyCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyCurrency.Text = "Moneda de reporte:"
        Me.INDlyCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyCurrency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyCurrency.TextSize = New System.Drawing.Size(157, 17)
        Me.INDlyCurrency.TextToControlDistance = 5
        '
        'INDLcgFilters
        '
        Me.INDLcgFilters.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilters.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilters.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilters.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFilters.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilters.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgFilters, False)
        Me.INDLcgFilters.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciGroup, Me.INDLciThirdParty, Me.INDLciGenerateReport, Me.INDLciPatient, Me.INDLciHealthAdministrator, Me.INDLciCategories, Me.INDlciAdmissionNumber, Me.INDLciUser, Me.INDLciRadicated, Me.INDLciBranchOffice, Me.INDLciExportExcel, Me.LayoutControlItem1})
        Me.INDLcgFilters.Location = New System.Drawing.Point(384, 0)
        Me.INDLcgFilters.Name = "INDLcgFilters"
        Me.INDLcgFilters.Size = New System.Drawing.Size(400, 695)
        Me.INDLcgFilters.Text = "Filtros"
        '
        'INDLciGroup
        '
        Me.INDLciGroup.Control = Me.INDSleGroup
        Me.INDLciGroup.Location = New System.Drawing.Point(0, 112)
        Me.INDLciGroup.MaxSize = New System.Drawing.Size(375, 56)
        Me.INDLciGroup.MinSize = New System.Drawing.Size(375, 56)
        Me.INDLciGroup.Name = "INDLciGroup"
        Me.INDLciGroup.Size = New System.Drawing.Size(376, 56)
        Me.INDLciGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGroup.Text = "Grupo:"
        Me.INDLciGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciGroup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciGroup.TextSize = New System.Drawing.Size(128, 17)
        Me.INDLciGroup.TextToControlDistance = 5
        '
        'INDLciThirdParty
        '
        Me.INDLciThirdParty.Control = Me.INDSleThirdParty
        Me.INDLciThirdParty.Location = New System.Drawing.Point(0, 224)
        Me.INDLciThirdParty.MaxSize = New System.Drawing.Size(375, 56)
        Me.INDLciThirdParty.MinSize = New System.Drawing.Size(375, 56)
        Me.INDLciThirdParty.Name = "INDLciThirdParty"
        Me.INDLciThirdParty.Size = New System.Drawing.Size(376, 56)
        Me.INDLciThirdParty.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciThirdParty.Text = "Tercero:"
        Me.INDLciThirdParty.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciThirdParty.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciThirdParty.TextSize = New System.Drawing.Size(128, 17)
        Me.INDLciThirdParty.TextToControlDistance = 5
        '
        'INDLciGenerateReport
        '
        Me.INDLciGenerateReport.Control = Me.INDSbGenerateReport
        Me.INDLciGenerateReport.Location = New System.Drawing.Point(0, 504)
        Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(335, 40)
        Me.INDLciGenerateReport.MinSize = New System.Drawing.Size(335, 40)
        Me.INDLciGenerateReport.Name = "INDLciGenerateReport"
        Me.INDLciGenerateReport.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 2)
        Me.INDLciGenerateReport.Size = New System.Drawing.Size(335, 40)
        Me.INDLciGenerateReport.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenerateReport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateReport.TextVisible = False
        '
        'INDLciPatient
        '
        Me.INDLciPatient.Control = Me.INDSlePatient
        Me.INDLciPatient.Location = New System.Drawing.Point(0, 56)
        Me.INDLciPatient.MaxSize = New System.Drawing.Size(375, 56)
        Me.INDLciPatient.MinSize = New System.Drawing.Size(375, 56)
        Me.INDLciPatient.Name = "INDLciPatient"
        Me.INDLciPatient.Size = New System.Drawing.Size(376, 56)
        Me.INDLciPatient.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPatient.Text = "Paciente:"
        Me.INDLciPatient.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPatient.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPatient.TextSize = New System.Drawing.Size(128, 17)
        Me.INDLciPatient.TextToControlDistance = 5
        '
        'INDLciHealthAdministrator
        '
        Me.INDLciHealthAdministrator.Control = Me.INDSleHealthAdministrator
        Me.INDLciHealthAdministrator.Location = New System.Drawing.Point(0, 0)
        Me.INDLciHealthAdministrator.MaxSize = New System.Drawing.Size(375, 56)
        Me.INDLciHealthAdministrator.MinSize = New System.Drawing.Size(375, 56)
        Me.INDLciHealthAdministrator.Name = "INDLciHealthAdministrator"
        Me.INDLciHealthAdministrator.Size = New System.Drawing.Size(376, 56)
        Me.INDLciHealthAdministrator.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciHealthAdministrator.Text = "Entidad:"
        Me.INDLciHealthAdministrator.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciHealthAdministrator.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciHealthAdministrator.TextSize = New System.Drawing.Size(128, 17)
        Me.INDLciHealthAdministrator.TextToControlDistance = 5
        '
        'INDLciCategories
        '
        Me.INDLciCategories.Control = Me.INDSleCategories
        Me.INDLciCategories.Location = New System.Drawing.Point(0, 168)
        Me.INDLciCategories.MaxSize = New System.Drawing.Size(375, 56)
        Me.INDLciCategories.MinSize = New System.Drawing.Size(375, 56)
        Me.INDLciCategories.Name = "INDLciCategories"
        Me.INDLciCategories.Size = New System.Drawing.Size(376, 56)
        Me.INDLciCategories.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCategories.Text = "Categoria:"
        Me.INDLciCategories.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCategories.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCategories.TextSize = New System.Drawing.Size(128, 17)
        Me.INDLciCategories.TextToControlDistance = 5
        '
        'INDlciAdmissionNumber
        '
        Me.INDlciAdmissionNumber.Control = Me.INDsleAdmissionNumber
        Me.INDlciAdmissionNumber.Location = New System.Drawing.Point(0, 336)
        Me.INDlciAdmissionNumber.MaxSize = New System.Drawing.Size(376, 56)
        Me.INDlciAdmissionNumber.MinSize = New System.Drawing.Size(376, 56)
        Me.INDlciAdmissionNumber.Name = "INDlciAdmissionNumber"
        Me.INDlciAdmissionNumber.Size = New System.Drawing.Size(376, 56)
        Me.INDlciAdmissionNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAdmissionNumber.Text = "Numero de Ingreso:"
        Me.INDlciAdmissionNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciAdmissionNumber.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciUser
        '
        Me.INDLciUser.Control = Me.INDSleUser
        Me.INDLciUser.Location = New System.Drawing.Point(0, 448)
        Me.INDLciUser.MaxSize = New System.Drawing.Size(376, 56)
        Me.INDLciUser.MinSize = New System.Drawing.Size(376, 56)
        Me.INDLciUser.Name = "INDLciUser"
        Me.INDLciUser.Size = New System.Drawing.Size(376, 56)
        Me.INDLciUser.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciUser.Text = "Usuario:"
        Me.INDLciUser.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciUser.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciRadicated
        '
        Me.INDLciRadicated.Control = Me.INDSleRadicated
        Me.INDLciRadicated.Location = New System.Drawing.Point(0, 392)
        Me.INDLciRadicated.MaxSize = New System.Drawing.Size(376, 56)
        Me.INDLciRadicated.MinSize = New System.Drawing.Size(376, 56)
        Me.INDLciRadicated.Name = "INDLciRadicated"
        Me.INDLciRadicated.Size = New System.Drawing.Size(376, 56)
        Me.INDLciRadicated.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRadicated.Text = "Radicado:"
        Me.INDLciRadicated.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRadicated.TextSize = New System.Drawing.Size(128, 17)
        '
        'INDLciBranchOffice
        '
        Me.INDLciBranchOffice.Control = Me.INDSleBranchOffice
        Me.INDLciBranchOffice.CustomizationFormText = "Sucursal:"
        Me.INDLciBranchOffice.Location = New System.Drawing.Point(0, 280)
        Me.INDLciBranchOffice.MaxSize = New System.Drawing.Size(375, 56)
        Me.INDLciBranchOffice.MinSize = New System.Drawing.Size(375, 56)
        Me.INDLciBranchOffice.Name = "INDLciBranchOffice"
        Me.INDLciBranchOffice.Size = New System.Drawing.Size(376, 56)
        Me.INDLciBranchOffice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBranchOffice.Text = "Sucursal:"
        Me.INDLciBranchOffice.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBranchOffice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBranchOffice.TextSize = New System.Drawing.Size(128, 17)
        Me.INDLciBranchOffice.TextToControlDistance = 5
        Me.INDLciBranchOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciExportExcel
        '
        Me.INDLciExportExcel.Control = Me.INDSbExportExcel
        Me.INDLciExportExcel.Location = New System.Drawing.Point(335, 504)
        Me.INDLciExportExcel.MaxSize = New System.Drawing.Size(38, 51)
        Me.INDLciExportExcel.MinSize = New System.Drawing.Size(38, 51)
        Me.INDLciExportExcel.Name = "INDLciExportExcel"
        Me.INDLciExportExcel.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 2, 10, 2)
        Me.INDLciExportExcel.Size = New System.Drawing.Size(41, 138)
        Me.INDLciExportExcel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciExportExcel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciExportExcel.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcGenerateExcel
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 544)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(335, 98)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        Me.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDPcReport
        '
        Me.INDPcReport.Controls.Add(Me.INDDvReport)
        Me.INDPcReport.Controls.Add(Me.INDCnReturn)
        Me.INDPcReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcReport.Location = New System.Drawing.Point(2, 7)
        Me.INDPcReport.Name = "INDPcReport"
        Me.INDPcReport.Size = New System.Drawing.Size(1004, 715)
        Me.INDPcReport.TabIndex = 2
        '
        'INDCnReturn
        '
        Me.INDCnReturn.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnReturn.Location = New System.Drawing.Point(2, 2)
        Me.INDCnReturn.Name = "INDCnReturn"
        Me.INDCnReturn.Size = New System.Drawing.Size(70, 711)
        Me.INDCnReturn.TabIndex = 0
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
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
        'FrmReportListInvoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmReportListInvoices"
        Me.Opacity = 1.0R
        Me.Tag = "1683"
        Me.Text = "Informe de Lista de Facturas"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCncReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBaseHome, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBaseHome.ResumeLayout(False)
        CType(Me.INDGcGenerateExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvGenerateExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DocumentViewerBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PrintPreviewRepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemProgressBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemZoomTrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDDvReport.ResumeLayout(False)
        Me.INDDvReport.PerformLayout()
        CType(Me.INDSleBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleFinalInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFinalInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleRadicated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleOrder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleInitialInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleAdmissionNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleTypeInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCategories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleHealthAdministrator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleTypeReport.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlePatient, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCurrency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBaseHome, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCriteria, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDateStart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDteEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInitialInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTypeInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFinalInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCurrency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPatient, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciHealthAdministrator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCategories, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAdmissionNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRadicated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcReport.ResumeLayout(False)
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDocumentViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCncReport As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDPcReport As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDLcBaseHome As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBaseHome As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDDvReport As DevExpress.XtraPrinting.Preview.DocumentViewer
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDCnReturn As Presentation.Controls.CtrNavigation
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
    Friend WithEvents INDDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgCriteria As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciDateStart As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciDteEnd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlePatient As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SearchLookUpEditExView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDLcgFilters As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciPatient As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDGleTypeReport As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciTypeReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleGroup As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleThirdParty As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciThirdParty As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDCodePatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNamePatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCodeGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNitThirdParty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameThirdParty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCodeHealthAdministrator As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameHealthAdministrator As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSbGenerateReport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGenerateReport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSegNamePatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDFirstApellidoPatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSecApellidoPatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleHealthAdministrator As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents INDLciHealthAdministrator As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleCategories As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDCodeInvoiceCategories As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameInvoiceCategories As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciCategories As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDGleTypeInvoice As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciTypeInvoice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleAdmissionNumber As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciAdmissionNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleUser As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciUser As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleInitialInvoice As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciInitialInvoice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleStatus As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciStatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleOrder As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciOrder As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleRadicated As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView13 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciRadicated As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRadicatedConsecutive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCustomerNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCustomerName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRadicatedDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDocumentViewer1 As Presentation.Controls.IndigoDocumentViewer
    Friend WithEvents INDSleFinalInvoice As SearchLookUpEditEx
    Friend WithEvents INDGvFinalInvoice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciFinalInvoice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColFinalInvoicePatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFinalInvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFinalInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFinalInvoiceAdmissionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFinalInvoiceCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColFinalInvoiceIdentification As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleBranchOffice As SearchLookUpEditEx
    Friend WithEvents INDGvBranchOffice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciBranchOffice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolBranchOfficeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBranchOfficeDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSbExportExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciExportExcel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcGenerateExcel As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvGenerateExcel As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleCurrency As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn178 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn761 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn771 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyCurrency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
