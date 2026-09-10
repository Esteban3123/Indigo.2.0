Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmIntegrationErrorLogs
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
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmIntegrationErrorLogs))
        Me.INDviewMixingStationDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn76 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn77 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn78 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn79 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcMixingStation = New DevExpress.XtraGrid.GridControl()
        Me.INDviewMixingStation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn62 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBirthDayMS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox4 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcMainData = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccIntegrationTransactionDetails = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcIntegrationTransactionDetails = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcIntegrationTransactionDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDGvIntegrationTransactionDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvIntegrationTransactionDetails_LogMessage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIntegrationTransactionDetails_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIntegrationTransactionDetails_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgIntegrationTransactionDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciIntegrationTransactionDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcIntegrationInvalid = New DevExpress.XtraGrid.GridControl()
        Me.INDGvIntegrationTransactionsInvalid = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvIntegrationErrors_Source = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIntegrationErrors_MessageId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIntegrationErrors_Transmitter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIntegrationErrors_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIntegrationErrors_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvIntegrationErrors_Details = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPceTransactionDetailsInvalid = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGcIntegrationValid = New DevExpress.XtraGrid.GridControl()
        Me.INDGvIntegrationTransactionsValid = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDTransactionLog_Source = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTransactionLog_MessageId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTransactionLog_Transmitter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTransactionLog_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTransactionLog_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTransactionLog_Details = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPceTransactionDetailValid = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDDdbMenuActions = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPmActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDBbiRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBtsAutomaticRefresh = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDTcgTransactionMessages = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgInvalidTransactions = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTransactionInvalid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgValidTransactions = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDLcMenuActions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPceDetailsValid = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup21 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDTcgDashBoard = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlcgMixingStation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyMixingStation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgRequest = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciRequest = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcRequest = New DevExpress.XtraGrid.GridControl()
        Me.INDGvRequest = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox11 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColConsecutiveCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBithDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptCheActivate = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDlygExtramural = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcExtramural = New DevExpress.XtraGrid.GridControl()
        Me.INDGvExtramural = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColConsecutiveCod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBirthDayEx = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox2 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDLcgReturn = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcReturn = New DevExpress.XtraGrid.GridControl()
        Me.INDGvReturn = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColConsDevCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBirthDayDev = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptCheActivateDevolution = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.LayoutControlGroup31 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcPaquetes = New DevExpress.XtraGrid.GridControl()
        Me.INDgvPaquetes = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBirthDaySurg = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemChemotherapy = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDgcChemotherapy = New DevExpress.XtraGrid.GridControl()
        Me.INDviewChemoterapy = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn67 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBirthDayQ = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox3 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup11 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl11 = New DevExpress.XtraEditors.PanelControl()
        Me.INDSleCareCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewSearchCareCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcMainData1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTbAutomaticReload = New DevExpress.XtraEditors.ToggleSwitch()
        Me.PcContainerReportPhamacyNotes = New DevExpress.XtraEditors.PanelControl()
        Me.INDPcReportViewerPharmacyNotes = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnCloseReportViewerPharmacyNote = New DevExpress.XtraEditors.SimpleButton()
        Me.INDPcContainer = New DevExpress.XtraEditors.PanelControl()
        Me.INDPcReportViewer = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnCloseReportViewer = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleTypeFilter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvSearchType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn272 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLciReportViewer = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLycReportPhamacyNotes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLycTypeFilter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyTypeFilter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciToggleButton = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.RepositoryItemPopupContainerEdit31 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewMixingStationDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcMixingStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewMixingStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMainData.SuspendLayout()
        CType(Me.INDPccIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccIntegrationTransactionDetails.SuspendLayout()
        CType(Me.INDLcIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcIntegrationTransactionDetails.SuspendLayout()
        CType(Me.INDGcIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcIntegrationInvalid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvIntegrationTransactionsInvalid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceTransactionDetailsInvalid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcIntegrationValid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvIntegrationTransactionsValid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceTransactionDetailValid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPmActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgTransactionMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgInvalidTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTransactionInvalid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgValidTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceDetailsValid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgDashBoard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMixingStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyMixingStation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptCheActivate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygExtramural, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcExtramural, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvExtramural, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptCheActivateDevolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcPaquetes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvPaquetes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemChemotherapy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcChemotherapy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewChemoterapy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl11.SuspendLayout()
        CType(Me.INDSleCareCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewSearchCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMainData1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMainData1.SuspendLayout()
        CType(Me.INDTbAutomaticReload.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PcContainerReportPhamacyNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PcContainerReportPhamacyNotes.SuspendLayout()
        CType(Me.INDPcReportViewerPharmacyNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.INDPcContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcContainer.SuspendLayout()
        CType(Me.INDPcReportViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.INDSleTypeFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvSearchType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReportViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycReportPhamacyNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycTypeFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTypeFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciToggleButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcMainData)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDviewMixingStationDetail
        '
        Me.INDviewMixingStationDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewMixingStationDetail.Appearance.FocusedRow.Font = CType(resources.GetObject("INDviewMixingStationDetail.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDviewMixingStationDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewMixingStationDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewMixingStationDetail.Appearance.GroupRow.Font = CType(resources.GetObject("INDviewMixingStationDetail.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDviewMixingStationDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewMixingStationDetail.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDviewMixingStationDetail.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDviewMixingStationDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewMixingStationDetail.Appearance.Row.Font = CType(resources.GetObject("INDviewMixingStationDetail.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDviewMixingStationDetail.Appearance.Row.Options.UseFont = True
        Me.INDviewMixingStationDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn71, Me.GridColumn73, Me.GridColumn74, Me.GridColumn75, Me.GridColumn76, Me.GridColumn77, Me.GridColumn78, Me.GridColumn79})
        Me.INDviewMixingStationDetail.GridControl = Me.INDgcMixingStation
        Me.INDviewMixingStationDetail.Name = "INDviewMixingStationDetail"
        Me.INDviewMixingStationDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewMixingStationDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewMixingStationDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDviewMixingStationDetail.OptionsView.ShowDetailButtons = False
        Me.INDviewMixingStationDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewMixingStationDetail, False)
        '
        'GridColumn71
        '
        resources.ApplyResources(Me.GridColumn71, "GridColumn71")
        Me.GridColumn71.FieldName = "Row"
        Me.GridColumn71.Name = "GridColumn71"
        Me.GridColumn71.OptionsColumn.AllowEdit = False
        Me.GridColumn71.OptionsColumn.AllowFocus = False
        '
        'GridColumn73
        '
        resources.ApplyResources(Me.GridColumn73, "GridColumn73")
        Me.GridColumn73.FieldName = "FechaOrden"
        Me.GridColumn73.Name = "GridColumn73"
        Me.GridColumn73.OptionsColumn.AllowEdit = False
        Me.GridColumn73.OptionsColumn.AllowFocus = False
        '
        'GridColumn74
        '
        resources.ApplyResources(Me.GridColumn74, "GridColumn74")
        Me.GridColumn74.FieldName = "CodigoCama"
        Me.GridColumn74.Name = "GridColumn74"
        Me.GridColumn74.OptionsColumn.AllowEdit = False
        Me.GridColumn74.OptionsColumn.AllowFocus = False
        '
        'GridColumn75
        '
        resources.ApplyResources(Me.GridColumn75, "GridColumn75")
        Me.GridColumn75.FieldName = "Profesion"
        Me.GridColumn75.Name = "GridColumn75"
        Me.GridColumn75.OptionsColumn.AllowEdit = False
        Me.GridColumn75.OptionsColumn.AllowFocus = False
        '
        'GridColumn76
        '
        resources.ApplyResources(Me.GridColumn76, "GridColumn76")
        Me.GridColumn76.FieldName = "NombreMedico"
        Me.GridColumn76.Name = "GridColumn76"
        Me.GridColumn76.OptionsColumn.AllowEdit = False
        Me.GridColumn76.OptionsColumn.AllowFocus = False
        '
        'GridColumn77
        '
        resources.ApplyResources(Me.GridColumn77, "GridColumn77")
        Me.GridColumn77.FieldName = "UnidadFuncional"
        Me.GridColumn77.Name = "GridColumn77"
        Me.GridColumn77.OptionsColumn.AllowEdit = False
        Me.GridColumn77.OptionsColumn.AllowFocus = False
        '
        'GridColumn78
        '
        resources.ApplyResources(Me.GridColumn78, "GridColumn78")
        Me.GridColumn78.FieldName = "CareCenterDescription"
        Me.GridColumn78.Name = "GridColumn78"
        Me.GridColumn78.OptionsColumn.AllowEdit = False
        Me.GridColumn78.OptionsColumn.AllowFocus = False
        '
        'GridColumn79
        '
        resources.ApplyResources(Me.GridColumn79, "GridColumn79")
        Me.GridColumn79.Name = "GridColumn79"
        Me.GridColumn79.OptionsColumn.AllowEdit = False
        Me.GridColumn79.OptionsColumn.AllowFocus = False
        '
        'INDgcMixingStation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcMixingStation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcMixingStation, Nothing)
        Me.INDgcMixingStation.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcMixingStation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcMixingStation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcMixingStation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcMixingStation, False)
        GridLevelNode2.LevelTemplate = Me.INDviewMixingStationDetail
        GridLevelNode2.RelationName = "Solicitudes pendientes"
        Me.INDgcMixingStation.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        resources.ApplyResources(Me.INDgcMixingStation, "INDgcMixingStation")
        Me.INDgcMixingStation.MainView = Me.INDviewMixingStation
        Me.INDgcMixingStation.Name = "INDgcMixingStation"
        Me.INDgcMixingStation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox4, Me.RepositoryItemCheckEdit2})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcMixingStation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcMixingStation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewMixingStation, Me.INDviewMixingStationDetail})
        '
        'INDviewMixingStation
        '
        Me.INDviewMixingStation.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewMixingStation.Appearance.FocusedRow.Font = CType(resources.GetObject("INDviewMixingStation.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDviewMixingStation.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewMixingStation.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewMixingStation.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewMixingStation.Appearance.GroupRow.Font = CType(resources.GetObject("INDviewMixingStation.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDviewMixingStation.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewMixingStation.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDviewMixingStation.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDviewMixingStation.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewMixingStation.Appearance.Row.Font = CType(resources.GetObject("INDviewMixingStation.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDviewMixingStation.Appearance.Row.Options.UseFont = True
        Me.INDviewMixingStation.Appearance.ViewCaption.Font = CType(resources.GetObject("INDviewMixingStation.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDviewMixingStation.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewMixingStation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn57, Me.GridColumn58, Me.GridColumn61, Me.GridColumn62, Me.INDColBirthDayMS})
        Me.INDviewMixingStation.CustomizationFormBounds = New System.Drawing.Rectangle(1070, 406, 210, 200)
        Me.INDviewMixingStation.GridControl = Me.INDgcMixingStation
        Me.INDviewMixingStation.GroupCount = 1
        Me.INDviewMixingStation.Name = "INDviewMixingStation"
        Me.INDviewMixingStation.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewMixingStation.OptionsDetail.AllowExpandEmptyDetails = True
        Me.INDviewMixingStation.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewMixingStation.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewMixingStation.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Standard
        Me.INDviewMixingStation.OptionsView.ShowAutoFilterRow = True
        Me.INDviewMixingStation.OptionsView.ShowGroupPanel = False
        Me.INDviewMixingStation.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn57, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewMixingStation, False)
        '
        'GridColumn57
        '
        resources.ApplyResources(Me.GridColumn57, "GridColumn57")
        Me.GridColumn57.FieldName = "Tipo"
        Me.GridColumn57.Name = "GridColumn57"
        Me.GridColumn57.OptionsColumn.AllowEdit = False
        Me.GridColumn57.OptionsColumn.AllowFocus = False
        '
        'GridColumn58
        '
        resources.ApplyResources(Me.GridColumn58, "GridColumn58")
        Me.GridColumn58.FieldName = "Ingreso"
        Me.GridColumn58.Name = "GridColumn58"
        Me.GridColumn58.OptionsColumn.AllowEdit = False
        Me.GridColumn58.OptionsColumn.AllowFocus = False
        '
        'GridColumn61
        '
        resources.ApplyResources(Me.GridColumn61, "GridColumn61")
        Me.GridColumn61.FieldName = "CodigoPaciente"
        Me.GridColumn61.Name = "GridColumn61"
        Me.GridColumn61.OptionsColumn.AllowEdit = False
        Me.GridColumn61.OptionsColumn.AllowFocus = False
        '
        'GridColumn62
        '
        resources.ApplyResources(Me.GridColumn62, "GridColumn62")
        Me.GridColumn62.FieldName = "NombrePaciente"
        Me.GridColumn62.Name = "GridColumn62"
        Me.GridColumn62.OptionsColumn.AllowEdit = False
        Me.GridColumn62.OptionsColumn.AllowFocus = False
        '
        'INDColBirthDayMS
        '
        resources.ApplyResources(Me.INDColBirthDayMS, "INDColBirthDayMS")
        Me.INDColBirthDayMS.DisplayFormat.FormatString = "d"
        Me.INDColBirthDayMS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColBirthDayMS.FieldName = "BirthDay"
        Me.INDColBirthDayMS.Name = "INDColBirthDayMS"
        Me.INDColBirthDayMS.OptionsColumn.AllowEdit = False
        Me.INDColBirthDayMS.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemImageComboBox4
        '
        resources.ApplyResources(Me.RepositoryItemImageComboBox4, "RepositoryItemImageComboBox4")
        Me.RepositoryItemImageComboBox4.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox4.Items"), CType(resources.GetObject("RepositoryItemImageComboBox4.Items1"), Object), CType(resources.GetObject("RepositoryItemImageComboBox4.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox4.Items3"), CType(resources.GetObject("RepositoryItemImageComboBox4.Items4"), Object), CType(resources.GetObject("RepositoryItemImageComboBox4.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox4.Items6"), CType(resources.GetObject("RepositoryItemImageComboBox4.Items7"), Object), CType(resources.GetObject("RepositoryItemImageComboBox4.Items8"), Integer))})
        Me.RepositoryItemImageComboBox4.Name = "RepositoryItemImageComboBox4"
        '
        'RepositoryItemCheckEdit2
        '
        resources.ApplyResources(Me.RepositoryItemCheckEdit2, "RepositoryItemCheckEdit2")
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit3.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'INDLcMainData
        '
        Me.INDLcMainData.AllowCustomization = False
        Me.INDLcMainData.Controls.Add(Me.INDPccIntegrationTransactionDetails)
        Me.INDLcMainData.Controls.Add(Me.INDGcIntegrationInvalid)
        Me.INDLcMainData.Controls.Add(Me.INDGcIntegrationValid)
        Me.INDLcMainData.Controls.Add(Me.INDDdbMenuActions)
        resources.ApplyResources(Me.INDLcMainData, "INDLcMainData")
        Me.LayoutControls.SetIsCustomizable(Me.INDLcMainData, False)
        Me.INDLcMainData.Name = "INDLcMainData"
        Me.INDLcMainData.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(378, 169, 1245, 542)
        Me.INDLcMainData.Root = Me.LayoutControlGroup2
        '
        'INDPccIntegrationTransactionDetails
        '
        Me.INDPccIntegrationTransactionDetails.Controls.Add(Me.INDLcIntegrationTransactionDetails)
        resources.ApplyResources(Me.INDPccIntegrationTransactionDetails, "INDPccIntegrationTransactionDetails")
        Me.INDPccIntegrationTransactionDetails.Name = "INDPccIntegrationTransactionDetails"
        '
        'INDLcIntegrationTransactionDetails
        '
        Me.INDLcIntegrationTransactionDetails.Controls.Add(Me.INDGcIntegrationTransactionDetails)
        resources.ApplyResources(Me.INDLcIntegrationTransactionDetails, "INDLcIntegrationTransactionDetails")
        Me.INDLcIntegrationTransactionDetails.Name = "INDLcIntegrationTransactionDetails"
        Me.INDLcIntegrationTransactionDetails.Root = Me.INDLcgIntegrationTransactionDetails
        '
        'INDGcIntegrationTransactionDetails
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcIntegrationTransactionDetails, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcIntegrationTransactionDetails, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcIntegrationTransactionDetails, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcIntegrationTransactionDetails, False)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcIntegrationTransactionDetails, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcIntegrationTransactionDetails, False)
        resources.ApplyResources(Me.INDGcIntegrationTransactionDetails, "INDGcIntegrationTransactionDetails")
        Me.INDGcIntegrationTransactionDetails.MainView = Me.INDGvIntegrationTransactionDetails
        Me.INDGcIntegrationTransactionDetails.Name = "INDGcIntegrationTransactionDetails"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcIntegrationTransactionDetails, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcIntegrationTransactionDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvIntegrationTransactionDetails})
        '
        'INDGvIntegrationTransactionDetails
        '
        Me.INDGvIntegrationTransactionDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvIntegrationTransactionDetails.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvIntegrationTransactionDetails.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionDetails.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvIntegrationTransactionDetails.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvIntegrationTransactionDetails.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvIntegrationTransactionDetails.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionDetails.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvIntegrationTransactionDetails.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvIntegrationTransactionDetails.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvIntegrationTransactionDetails.Appearance.Row.Font = CType(resources.GetObject("INDGvIntegrationTransactionDetails.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionDetails.Appearance.Row.Options.UseFont = True
        Me.INDGvIntegrationTransactionDetails.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvIntegrationTransactionDetails.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionDetails.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvIntegrationTransactionDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvIntegrationTransactionDetails_LogMessage, Me.INDGvIntegrationTransactionDetails_Status, Me.INDGvIntegrationTransactionDetails_CreationDate})
        Me.INDGvIntegrationTransactionDetails.GridControl = Me.INDGcIntegrationTransactionDetails
        Me.INDGvIntegrationTransactionDetails.Name = "INDGvIntegrationTransactionDetails"
        Me.INDGvIntegrationTransactionDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvIntegrationTransactionDetails.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvIntegrationTransactionDetails.OptionsView.ShowAutoFilterRow = True
        Me.INDGvIntegrationTransactionDetails.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvIntegrationTransactionDetails, False)
        '
        'INDGvIntegrationTransactionDetails_LogMessage
        '
        resources.ApplyResources(Me.INDGvIntegrationTransactionDetails_LogMessage, "INDGvIntegrationTransactionDetails_LogMessage")
        Me.INDGvIntegrationTransactionDetails_LogMessage.FieldName = "LogMessage"
        Me.INDGvIntegrationTransactionDetails_LogMessage.Name = "INDGvIntegrationTransactionDetails_LogMessage"
        Me.INDGvIntegrationTransactionDetails_LogMessage.OptionsColumn.AllowEdit = False
        Me.INDGvIntegrationTransactionDetails_LogMessage.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvIntegrationTransactionDetails_LogMessage.OptionsColumn.AllowShowHide = False
        Me.INDGvIntegrationTransactionDetails_LogMessage.OptionsColumn.FixedWidth = True
        '
        'INDGvIntegrationTransactionDetails_Status
        '
        resources.ApplyResources(Me.INDGvIntegrationTransactionDetails_Status, "INDGvIntegrationTransactionDetails_Status")
        Me.INDGvIntegrationTransactionDetails_Status.FieldName = "StatusDescription"
        Me.INDGvIntegrationTransactionDetails_Status.Name = "INDGvIntegrationTransactionDetails_Status"
        Me.INDGvIntegrationTransactionDetails_Status.OptionsColumn.AllowEdit = False
        Me.INDGvIntegrationTransactionDetails_Status.OptionsColumn.AllowShowHide = False
        Me.INDGvIntegrationTransactionDetails_Status.OptionsColumn.FixedWidth = True
        '
        'INDGvIntegrationTransactionDetails_CreationDate
        '
        resources.ApplyResources(Me.INDGvIntegrationTransactionDetails_CreationDate, "INDGvIntegrationTransactionDetails_CreationDate")
        Me.INDGvIntegrationTransactionDetails_CreationDate.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt"
        Me.INDGvIntegrationTransactionDetails_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvIntegrationTransactionDetails_CreationDate.FieldName = "CreationDate"
        Me.INDGvIntegrationTransactionDetails_CreationDate.Name = "INDGvIntegrationTransactionDetails_CreationDate"
        Me.INDGvIntegrationTransactionDetails_CreationDate.OptionsColumn.AllowEdit = False
        '
        'INDLcgIntegrationTransactionDetails
        '
        Me.INDLcgIntegrationTransactionDetails.AppearanceGroup.Font = CType(resources.GetObject("INDLcgIntegrationTransactionDetails.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgIntegrationTransactionDetails.AppearanceGroup.Options.UseFont = True
        Me.INDLcgIntegrationTransactionDetails.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgIntegrationTransactionDetails.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgIntegrationTransactionDetails.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgIntegrationTransactionDetails.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgIntegrationTransactionDetails.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgIntegrationTransactionDetails.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgIntegrationTransactionDetails, False)
        Me.INDLcgIntegrationTransactionDetails.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgIntegrationTransactionDetails.GroupBordersVisible = False
        Me.INDLcgIntegrationTransactionDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciIntegrationTransactionDetails})
        Me.INDLcgIntegrationTransactionDetails.Name = "INDLcgIntegrationTransactionDetails"
        Me.INDLcgIntegrationTransactionDetails.Size = New System.Drawing.Size(861, 149)
        Me.INDLcgIntegrationTransactionDetails.TextVisible = False
        '
        'INDLciIntegrationTransactionDetails
        '
        Me.INDLciIntegrationTransactionDetails.Control = Me.INDGcIntegrationTransactionDetails
        Me.INDLciIntegrationTransactionDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDLciIntegrationTransactionDetails.Name = "INDLciIntegrationTransactionDetails"
        Me.INDLciIntegrationTransactionDetails.Size = New System.Drawing.Size(841, 129)
        Me.INDLciIntegrationTransactionDetails.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciIntegrationTransactionDetails.TextVisible = False
        '
        'INDGcIntegrationInvalid
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcIntegrationInvalid, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcIntegrationInvalid, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcIntegrationInvalid, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcIntegrationInvalid, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcIntegrationInvalid, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcIntegrationInvalid, False)
        resources.ApplyResources(Me.INDGcIntegrationInvalid, "INDGcIntegrationInvalid")
        Me.INDGcIntegrationInvalid.MainView = Me.INDGvIntegrationTransactionsInvalid
        Me.INDGcIntegrationInvalid.Name = "INDGcIntegrationInvalid"
        Me.INDGcIntegrationInvalid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDPceTransactionDetailsInvalid})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcIntegrationInvalid, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcIntegrationInvalid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvIntegrationTransactionsInvalid})
        '
        'INDGvIntegrationTransactionsInvalid
        '
        Me.INDGvIntegrationTransactionsInvalid.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvIntegrationTransactionsInvalid.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvIntegrationTransactionsInvalid.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsInvalid.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvIntegrationTransactionsInvalid.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvIntegrationTransactionsInvalid.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvIntegrationTransactionsInvalid.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsInvalid.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvIntegrationTransactionsInvalid.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvIntegrationTransactionsInvalid.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsInvalid.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvIntegrationTransactionsInvalid.Appearance.Row.Font = CType(resources.GetObject("INDGvIntegrationTransactionsInvalid.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsInvalid.Appearance.Row.Options.UseFont = True
        Me.INDGvIntegrationTransactionsInvalid.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvIntegrationTransactionsInvalid.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsInvalid.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvIntegrationTransactionsInvalid.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvIntegrationErrors_Source, Me.INDGvIntegrationErrors_MessageId, Me.INDGvIntegrationErrors_Transmitter, Me.INDGvIntegrationErrors_Status, Me.INDGvIntegrationErrors_CreationDate, Me.INDGvIntegrationErrors_Details})
        Me.INDGvIntegrationTransactionsInvalid.GridControl = Me.INDGcIntegrationInvalid
        Me.INDGvIntegrationTransactionsInvalid.Name = "INDGvIntegrationTransactionsInvalid"
        Me.INDGvIntegrationTransactionsInvalid.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvIntegrationTransactionsInvalid.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvIntegrationTransactionsInvalid.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvIntegrationTransactionsInvalid.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvIntegrationTransactionsInvalid.OptionsView.ShowAutoFilterRow = True
        Me.INDGvIntegrationTransactionsInvalid.OptionsView.ShowFooter = True
        Me.INDGvIntegrationTransactionsInvalid.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvIntegrationTransactionsInvalid, False)
        '
        'INDGvIntegrationErrors_Source
        '
        resources.ApplyResources(Me.INDGvIntegrationErrors_Source, "INDGvIntegrationErrors_Source")
        Me.INDGvIntegrationErrors_Source.FieldName = "SourceName"
        Me.INDGvIntegrationErrors_Source.Name = "INDGvIntegrationErrors_Source"
        Me.INDGvIntegrationErrors_Source.OptionsColumn.AllowEdit = False
        Me.INDGvIntegrationErrors_Source.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvIntegrationErrors_Source.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvIntegrationErrors_Source.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        '
        'INDGvIntegrationErrors_MessageId
        '
        resources.ApplyResources(Me.INDGvIntegrationErrors_MessageId, "INDGvIntegrationErrors_MessageId")
        Me.INDGvIntegrationErrors_MessageId.FieldName = "InternalBodyId"
        Me.INDGvIntegrationErrors_MessageId.Name = "INDGvIntegrationErrors_MessageId"
        Me.INDGvIntegrationErrors_MessageId.OptionsColumn.AllowEdit = False
        Me.INDGvIntegrationErrors_MessageId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvIntegrationErrors_MessageId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        '
        'INDGvIntegrationErrors_Transmitter
        '
        resources.ApplyResources(Me.INDGvIntegrationErrors_Transmitter, "INDGvIntegrationErrors_Transmitter")
        Me.INDGvIntegrationErrors_Transmitter.FieldName = "Transmitter"
        Me.INDGvIntegrationErrors_Transmitter.Name = "INDGvIntegrationErrors_Transmitter"
        Me.INDGvIntegrationErrors_Transmitter.OptionsColumn.AllowEdit = False
        Me.INDGvIntegrationErrors_Transmitter.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        '
        'INDGvIntegrationErrors_Status
        '
        resources.ApplyResources(Me.INDGvIntegrationErrors_Status, "INDGvIntegrationErrors_Status")
        Me.INDGvIntegrationErrors_Status.FieldName = "StatusDescription"
        Me.INDGvIntegrationErrors_Status.Name = "INDGvIntegrationErrors_Status"
        Me.INDGvIntegrationErrors_Status.OptionsColumn.AllowEdit = False
        '
        'INDGvIntegrationErrors_CreationDate
        '
        resources.ApplyResources(Me.INDGvIntegrationErrors_CreationDate, "INDGvIntegrationErrors_CreationDate")
        Me.INDGvIntegrationErrors_CreationDate.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt"
        Me.INDGvIntegrationErrors_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvIntegrationErrors_CreationDate.FieldName = "CreationDate"
        Me.INDGvIntegrationErrors_CreationDate.Name = "INDGvIntegrationErrors_CreationDate"
        Me.INDGvIntegrationErrors_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDGvIntegrationErrors_CreationDate.OptionsColumn.ReadOnly = True
        '
        'INDGvIntegrationErrors_Details
        '
        resources.ApplyResources(Me.INDGvIntegrationErrors_Details, "INDGvIntegrationErrors_Details")
        Me.INDGvIntegrationErrors_Details.ColumnEdit = Me.INDPceTransactionDetailsInvalid
        Me.INDGvIntegrationErrors_Details.FieldName = "MessageDetails"
        Me.INDGvIntegrationErrors_Details.Name = "INDGvIntegrationErrors_Details"
        Me.INDGvIntegrationErrors_Details.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        '
        'INDPceTransactionDetailsInvalid
        '
        resources.ApplyResources(Me.INDPceTransactionDetailsInvalid, "INDPceTransactionDetailsInvalid")
        Me.INDPceTransactionDetailsInvalid.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDPceTransactionDetailsInvalid.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDPceTransactionDetailsInvalid.Name = "INDPceTransactionDetailsInvalid"
        Me.INDPceTransactionDetailsInvalid.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGcIntegrationValid
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcIntegrationValid, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcIntegrationValid, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcIntegrationValid, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcIntegrationValid, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcIntegrationValid, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcIntegrationValid, False)
        resources.ApplyResources(Me.INDGcIntegrationValid, "INDGcIntegrationValid")
        Me.INDGcIntegrationValid.MainView = Me.INDGvIntegrationTransactionsValid
        Me.INDGcIntegrationValid.Name = "INDGcIntegrationValid"
        Me.INDGcIntegrationValid.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDPceTransactionDetailValid})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcIntegrationValid, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcIntegrationValid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvIntegrationTransactionsValid})
        '
        'INDGvIntegrationTransactionsValid
        '
        Me.INDGvIntegrationTransactionsValid.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvIntegrationTransactionsValid.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvIntegrationTransactionsValid.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsValid.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvIntegrationTransactionsValid.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvIntegrationTransactionsValid.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvIntegrationTransactionsValid.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsValid.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvIntegrationTransactionsValid.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvIntegrationTransactionsValid.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsValid.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvIntegrationTransactionsValid.Appearance.Row.Font = CType(resources.GetObject("INDGvIntegrationTransactionsValid.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsValid.Appearance.Row.Options.UseFont = True
        Me.INDGvIntegrationTransactionsValid.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvIntegrationTransactionsValid.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvIntegrationTransactionsValid.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvIntegrationTransactionsValid.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDTransactionLog_Source, Me.INDTransactionLog_MessageId, Me.INDTransactionLog_Transmitter, Me.INDTransactionLog_Status, Me.INDTransactionLog_CreationDate, Me.INDTransactionLog_Details})
        Me.INDGvIntegrationTransactionsValid.GridControl = Me.INDGcIntegrationValid
        Me.INDGvIntegrationTransactionsValid.Name = "INDGvIntegrationTransactionsValid"
        Me.INDGvIntegrationTransactionsValid.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvIntegrationTransactionsValid.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvIntegrationTransactionsValid.OptionsView.ShowAutoFilterRow = True
        Me.INDGvIntegrationTransactionsValid.OptionsView.ShowFooter = True
        Me.INDGvIntegrationTransactionsValid.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvIntegrationTransactionsValid, False)
        '
        'INDTransactionLog_Source
        '
        resources.ApplyResources(Me.INDTransactionLog_Source, "INDTransactionLog_Source")
        Me.INDTransactionLog_Source.FieldName = "SourceName"
        Me.INDTransactionLog_Source.Name = "INDTransactionLog_Source"
        Me.INDTransactionLog_Source.OptionsColumn.AllowEdit = False
        '
        'INDTransactionLog_MessageId
        '
        resources.ApplyResources(Me.INDTransactionLog_MessageId, "INDTransactionLog_MessageId")
        Me.INDTransactionLog_MessageId.FieldName = "InternalBodyId"
        Me.INDTransactionLog_MessageId.Name = "INDTransactionLog_MessageId"
        Me.INDTransactionLog_MessageId.OptionsColumn.AllowEdit = False
        '
        'INDTransactionLog_Transmitter
        '
        resources.ApplyResources(Me.INDTransactionLog_Transmitter, "INDTransactionLog_Transmitter")
        Me.INDTransactionLog_Transmitter.FieldName = "Transmitter"
        Me.INDTransactionLog_Transmitter.Name = "INDTransactionLog_Transmitter"
        Me.INDTransactionLog_Transmitter.OptionsColumn.AllowEdit = False
        '
        'INDTransactionLog_Status
        '
        resources.ApplyResources(Me.INDTransactionLog_Status, "INDTransactionLog_Status")
        Me.INDTransactionLog_Status.FieldName = "StatusDescription"
        Me.INDTransactionLog_Status.Name = "INDTransactionLog_Status"
        Me.INDTransactionLog_Status.OptionsColumn.AllowEdit = False
        '
        'INDTransactionLog_CreationDate
        '
        resources.ApplyResources(Me.INDTransactionLog_CreationDate, "INDTransactionLog_CreationDate")
        Me.INDTransactionLog_CreationDate.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt"
        Me.INDTransactionLog_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDTransactionLog_CreationDate.FieldName = "CreationDate"
        Me.INDTransactionLog_CreationDate.Name = "INDTransactionLog_CreationDate"
        Me.INDTransactionLog_CreationDate.OptionsColumn.AllowEdit = False
        '
        'INDTransactionLog_Details
        '
        resources.ApplyResources(Me.INDTransactionLog_Details, "INDTransactionLog_Details")
        Me.INDTransactionLog_Details.ColumnEdit = Me.INDPceTransactionDetailValid
        Me.INDTransactionLog_Details.Name = "INDTransactionLog_Details"
        '
        'INDPceTransactionDetailValid
        '
        resources.ApplyResources(Me.INDPceTransactionDetailValid, "INDPceTransactionDetailValid")
        Me.INDPceTransactionDetailValid.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDPceTransactionDetailValid.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDPceTransactionDetailValid.Name = "INDPceTransactionDetailValid"
        Me.INDPceTransactionDetailValid.PopupControl = Me.INDPccIntegrationTransactionDetails
        Me.INDPceTransactionDetailValid.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDDdbMenuActions
        '
        Me.INDDdbMenuActions.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDdbMenuActions.DropDownControl = Me.INDPmActions
        Me.INDDdbMenuActions.ImageOptions.Image = Global.Presentation.Inventory.My.Resources.Resources.Mmenu_de_acciones
        Me.INDDdbMenuActions.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.INDDdbMenuActions, "INDDdbMenuActions")
        Me.INDDdbMenuActions.Name = "INDDdbMenuActions"
        Me.INDDdbMenuActions.StyleController = Me.INDLcMainData
        '
        'INDPmActions
        '
        Me.INDPmActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiRefresh), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBtsAutomaticRefresh)})
        Me.INDPmActions.Manager = Me.BarManager1
        Me.INDPmActions.Name = "INDPmActions"
        '
        'INDBbiRefresh
        '
        resources.ApplyResources(Me.INDBbiRefresh, "INDBbiRefresh")
        Me.INDBbiRefresh.Id = 0
        Me.INDBbiRefresh.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDBbiRefresh.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDBbiRefresh.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiRefresh.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDBbiRefresh.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDBbiRefresh.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiRefresh.ItemAppearance.Normal.Font = CType(resources.GetObject("INDBbiRefresh.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDBbiRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiRefresh.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDBbiRefresh.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDBbiRefresh.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiRefresh.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
        Me.INDBbiRefresh.Name = "INDBbiRefresh"
        '
        'INDBtsAutomaticRefresh
        '
        resources.ApplyResources(Me.INDBtsAutomaticRefresh, "INDBtsAutomaticRefresh")
        Me.INDBtsAutomaticRefresh.Id = 1
        Me.INDBtsAutomaticRefresh.ItemAppearance.Disabled.Font = CType(resources.GetObject("INDBtsAutomaticRefresh.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDBtsAutomaticRefresh.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBtsAutomaticRefresh.ItemAppearance.Hovered.Font = CType(resources.GetObject("INDBtsAutomaticRefresh.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDBtsAutomaticRefresh.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBtsAutomaticRefresh.ItemAppearance.Normal.Font = CType(resources.GetObject("INDBtsAutomaticRefresh.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDBtsAutomaticRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBtsAutomaticRefresh.ItemAppearance.Pressed.Font = CType(resources.GetObject("INDBtsAutomaticRefresh.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDBtsAutomaticRefresh.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBtsAutomaticRefresh.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("INDBtsAutomaticRefresh.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.INDBtsAutomaticRefresh.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDBtsAutomaticRefresh.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("INDBtsAutomaticRefresh.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.INDBtsAutomaticRefresh.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.INDBtsAutomaticRefresh.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("INDBtsAutomaticRefresh.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.INDBtsAutomaticRefresh.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDBtsAutomaticRefresh.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("INDBtsAutomaticRefresh.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.INDBtsAutomaticRefresh.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDBtsAutomaticRefresh.Name = "INDBtsAutomaticRefresh"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiRefresh, Me.INDBtsAutomaticRefresh})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        Me.barDockControlTop.Manager = Me.BarManager1
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        Me.barDockControlBottom.Manager = Me.BarManager1
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        Me.barDockControlLeft.Manager = Me.BarManager1
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        Me.barDockControlRight.Manager = Me.BarManager1
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDTcgTransactionMessages, Me.LayoutControlGroup1})
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1064, 574)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDTcgTransactionMessages
        '
        Me.INDTcgTransactionMessages.Location = New System.Drawing.Point(0, 89)
        Me.INDTcgTransactionMessages.Name = "INDTcgTransactionMessages"
        Me.INDTcgTransactionMessages.SelectedTabPage = Me.INDLcgValidTransactions
        Me.INDTcgTransactionMessages.Size = New System.Drawing.Size(1044, 465)
        Me.INDTcgTransactionMessages.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgInvalidTransactions, Me.INDLcgValidTransactions})
        '
        'INDLcgInvalidTransactions
        '
        Me.INDLcgInvalidTransactions.AppearanceGroup.Font = CType(resources.GetObject("INDLcgInvalidTransactions.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgInvalidTransactions.AppearanceGroup.Options.UseFont = True
        Me.INDLcgInvalidTransactions.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgInvalidTransactions.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgInvalidTransactions.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgInvalidTransactions.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgInvalidTransactions.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgInvalidTransactions.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgInvalidTransactions.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgInvalidTransactions.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgInvalidTransactions.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgInvalidTransactions.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgInvalidTransactions.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgInvalidTransactions.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgInvalidTransactions.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgInvalidTransactions.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgInvalidTransactions.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgInvalidTransactions.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgInvalidTransactions.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgInvalidTransactions.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgInvalidTransactions, False)
        resources.ApplyResources(Me.INDLcgInvalidTransactions, "INDLcgInvalidTransactions")
        Me.INDLcgInvalidTransactions.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTransactionInvalid})
        Me.INDLcgInvalidTransactions.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgInvalidTransactions.Name = "INDLcgInvalidTransactions"
        Me.INDLcgInvalidTransactions.Size = New System.Drawing.Size(1020, 410)
        '
        'INDLciTransactionInvalid
        '
        Me.INDLciTransactionInvalid.Control = Me.INDGcIntegrationInvalid
        Me.INDLciTransactionInvalid.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTransactionInvalid.Name = "INDLciTransactionInvalid"
        Me.INDLciTransactionInvalid.Size = New System.Drawing.Size(1020, 410)
        Me.INDLciTransactionInvalid.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciTransactionInvalid.TextVisible = False
        '
        'INDLcgValidTransactions
        '
        Me.INDLcgValidTransactions.AppearanceGroup.Font = CType(resources.GetObject("INDLcgValidTransactions.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgValidTransactions.AppearanceGroup.Options.UseFont = True
        Me.INDLcgValidTransactions.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgValidTransactions.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgValidTransactions.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgValidTransactions.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgValidTransactions.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgValidTransactions.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgValidTransactions.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgValidTransactions.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgValidTransactions.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgValidTransactions.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgValidTransactions.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgValidTransactions.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgValidTransactions.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgValidTransactions.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgValidTransactions.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgValidTransactions.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgValidTransactions.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgValidTransactions.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgValidTransactions, False)
        Me.INDLcgValidTransactions.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDLcgValidTransactions.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgValidTransactions.Name = "INDLcgValidTransactions"
        Me.INDLcgValidTransactions.Size = New System.Drawing.Size(1020, 410)
        resources.ApplyResources(Me.INDLcgValidTransactions, "INDLcgValidTransactions")
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcIntegrationValid
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1020, 410)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.INDLcMenuActions})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1044, 89)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(0, 65)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(950, 65)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(950, 65)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDLcMenuActions
        '
        Me.INDLcMenuActions.Control = Me.INDDdbMenuActions
        resources.ApplyResources(Me.INDLcMenuActions, "INDLcMenuActions")
        Me.INDLcMenuActions.Location = New System.Drawing.Point(950, 0)
        Me.INDLcMenuActions.MaxSize = New System.Drawing.Size(70, 65)
        Me.INDLcMenuActions.MinSize = New System.Drawing.Size(70, 65)
        Me.INDLcMenuActions.Name = "INDLcMenuActions"
        Me.INDLcMenuActions.OptionsTableLayoutItem.ColumnIndex = 1
        Me.INDLcMenuActions.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 2)
        Me.INDLcMenuActions.Size = New System.Drawing.Size(70, 65)
        Me.INDLcMenuActions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLcMenuActions.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLcMenuActions.TextVisible = False
        '
        'INDPceDetailsValid
        '
        Me.INDPceDetailsValid.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        resources.ApplyResources(Me.INDPceDetailsValid, "INDPceDetailsValid")
        Me.INDPceDetailsValid.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDPceDetailsValid.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDPceDetailsValid.Name = "INDPceDetailsValid"
        Me.INDPceDetailsValid.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(50, 20)
        '
        'LayoutControlGroup21
        '
        Me.LayoutControlGroup21.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup21.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup21.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup21.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup21.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup21.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup21.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup21.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup21.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup21.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup21.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup21.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup21, False)
        resources.ApplyResources(Me.LayoutControlGroup21, "LayoutControlGroup21")
        Me.LayoutControlGroup21.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup21.GroupBordersVisible = False
        Me.LayoutControlGroup21.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDTcgDashBoard, Me.LayoutControlGroup11, Me.INDLciReportViewer, Me.INDLycReportPhamacyNotes, Me.INDLycTypeFilter})
        Me.LayoutControlGroup21.Name = "LayoutControlGroup21"
        Me.LayoutControlGroup21.Size = New System.Drawing.Size(1294, 541)
        Me.LayoutControlGroup21.TextVisible = False
        '
        'INDTcgDashBoard
        '
        resources.ApplyResources(Me.INDTcgDashBoard, "INDTcgDashBoard")
        Me.INDTcgDashBoard.Location = New System.Drawing.Point(0, 149)
        Me.INDTcgDashBoard.Name = "INDTcgDashBoard"
        Me.INDTcgDashBoard.SelectedTabPage = Me.INDlcgMixingStation
        Me.INDTcgDashBoard.Size = New System.Drawing.Size(874, 372)
        Me.INDTcgDashBoard.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgRequest, Me.INDlygExtramural, Me.INDLcgReturn, Me.LayoutControlGroup31, Me.LayoutControlGroup4, Me.INDlcgMixingStation})
        '
        'INDlcgMixingStation
        '
        Me.INDlcgMixingStation.AppearanceGroup.Font = CType(resources.GetObject("INDlcgMixingStation.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlcgMixingStation.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMixingStation.AppearanceItemCaption.Font = CType(resources.GetObject("INDlcgMixingStation.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlcgMixingStation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMixingStation.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlcgMixingStation.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlcgMixingStation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMixingStation.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlcgMixingStation.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlcgMixingStation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMixingStation.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlcgMixingStation.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlcgMixingStation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMixingStation.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlcgMixingStation.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlcgMixingStation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMixingStation.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlcgMixingStation.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlcgMixingStation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMixingStation, False)
        Me.INDlcgMixingStation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyMixingStation})
        Me.INDlcgMixingStation.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMixingStation.Name = "INDlcgMixingStation"
        Me.INDlcgMixingStation.Size = New System.Drawing.Size(850, 317)
        resources.ApplyResources(Me.INDlcgMixingStation, "INDlcgMixingStation")
        '
        'INDlyMixingStation
        '
        Me.INDlyMixingStation.Control = Me.INDgcMixingStation
        Me.INDlyMixingStation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyMixingStation.Name = "INDlyMixingStation"
        Me.INDlyMixingStation.Size = New System.Drawing.Size(850, 317)
        Me.INDlyMixingStation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyMixingStation.TextVisible = False
        '
        'INDLcgRequest
        '
        Me.INDLcgRequest.AppearanceGroup.Font = CType(resources.GetObject("INDLcgRequest.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgRequest.AppearanceGroup.Options.UseFont = True
        Me.INDLcgRequest.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgRequest.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgRequest.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgRequest.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgRequest.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgRequest.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgRequest.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgRequest.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgRequest.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgRequest.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgRequest.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgRequest.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgRequest.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgRequest.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgRequest.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgRequest.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgRequest.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgRequest.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgRequest, False)
        resources.ApplyResources(Me.INDLcgRequest, "INDLcgRequest")
        Me.INDLcgRequest.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciRequest})
        Me.INDLcgRequest.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgRequest.Name = "INDLcgRequest"
        Me.INDLcgRequest.Size = New System.Drawing.Size(850, 317)
        '
        'INDLciRequest
        '
        Me.INDLciRequest.Control = Me.INDGcRequest
        resources.ApplyResources(Me.INDLciRequest, "INDLciRequest")
        Me.INDLciRequest.Location = New System.Drawing.Point(0, 0)
        Me.INDLciRequest.MinSize = New System.Drawing.Size(1, 1)
        Me.INDLciRequest.Name = "INDLciRequest"
        Me.INDLciRequest.Size = New System.Drawing.Size(850, 317)
        Me.INDLciRequest.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRequest.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciRequest.TextVisible = False
        '
        'INDGcRequest
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcRequest, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcRequest, Nothing)
        Me.INDGcRequest.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcRequest, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcRequest, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcRequest, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcRequest, False)
        resources.ApplyResources(Me.INDGcRequest, "INDGcRequest")
        Me.INDGcRequest.MainView = Me.INDGvRequest
        Me.INDGcRequest.Name = "INDGcRequest"
        Me.INDGcRequest.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox11, Me.INDRptCheActivate})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcRequest, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcRequest.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvRequest})
        '
        'INDGvRequest
        '
        Me.INDGvRequest.Appearance.Empty.Font = CType(resources.GetObject("INDGvRequest.Appearance.Empty.Font"), System.Drawing.Font)
        Me.INDGvRequest.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvRequest.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvRequest.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvRequest.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvRequest.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvRequest.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvRequest.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvRequest.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvRequest.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvRequest.Appearance.Row.Font = CType(resources.GetObject("INDGvRequest.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvRequest.Appearance.Row.Options.UseFont = True
        Me.INDGvRequest.Appearance.SelectedRow.Font = CType(resources.GetObject("INDGvRequest.Appearance.SelectedRow.Font"), System.Drawing.Font)
        Me.INDGvRequest.Appearance.SelectedRow.Options.UseBorderColor = True
        Me.INDGvRequest.Appearance.SelectedRow.Options.UseFont = True
        Me.INDGvRequest.Appearance.SelectedRow.Options.UseForeColor = True
        Me.INDGvRequest.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvRequest.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvRequest.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvRequest.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.INDColConsecutiveCode, Me.GridColumn7, Me.GridColumn33, Me.GridColumn8, Me.INDColBithDay, Me.GridColumn9, Me.GridColumn52, Me.GridColumn10, Me.GridColumn3, Me.GridColumn39})
        Me.INDGvRequest.CustomizationFormBounds = New System.Drawing.Rectangle(1070, 406, 210, 200)
        Me.INDGvRequest.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvRequest.GridControl = Me.INDGcRequest
        Me.INDGvRequest.GroupCount = 1
        Me.INDGvRequest.Name = "INDGvRequest"
        Me.INDGvRequest.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvRequest.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp
        Me.INDGvRequest.OptionsSelection.MultiSelect = True
        Me.INDGvRequest.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDGvRequest.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRequest.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRequest.OptionsView.ShowAutoFilterRow = True
        Me.INDGvRequest.OptionsView.ShowDetailButtons = False
        Me.INDGvRequest.OptionsView.ShowFooter = True
        Me.INDGvRequest.OptionsView.ShowGroupPanel = False
        Me.INDGvRequest.OptionsView.ShowIndicator = False
        Me.INDGvRequest.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn4, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRequest, False)
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemImageComboBox11
        Me.GridColumn4.FieldName = "Tipo"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemImageComboBox11
        '
        resources.ApplyResources(Me.RepositoryItemImageComboBox11, "RepositoryItemImageComboBox11")
        Me.RepositoryItemImageComboBox11.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox11.Items"), CType(resources.GetObject("RepositoryItemImageComboBox11.Items1"), Object), CType(resources.GetObject("RepositoryItemImageComboBox11.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox11.Items3"), CType(resources.GetObject("RepositoryItemImageComboBox11.Items4"), Object), CType(resources.GetObject("RepositoryItemImageComboBox11.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox11.Items6"), CType(resources.GetObject("RepositoryItemImageComboBox11.Items7"), Object), CType(resources.GetObject("RepositoryItemImageComboBox11.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox11.Items9"), CType(resources.GetObject("RepositoryItemImageComboBox11.Items10"), Object), CType(resources.GetObject("RepositoryItemImageComboBox11.Items11"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox11.Items12"), CType(resources.GetObject("RepositoryItemImageComboBox11.Items13"), Object), CType(resources.GetObject("RepositoryItemImageComboBox11.Items14"), Integer))})
        Me.RepositoryItemImageComboBox11.Name = "RepositoryItemImageComboBox11"
        '
        'GridColumn5
        '
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.FieldName = "Ingreso"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        '
        'INDColConsecutiveCode
        '
        resources.ApplyResources(Me.INDColConsecutiveCode, "INDColConsecutiveCode")
        Me.INDColConsecutiveCode.FieldName = "Row"
        Me.INDColConsecutiveCode.Name = "INDColConsecutiveCode"
        Me.INDColConsecutiveCode.OptionsColumn.AllowEdit = False
        Me.INDColConsecutiveCode.OptionsColumn.AllowFocus = False
        '
        'GridColumn7
        '
        resources.ApplyResources(Me.GridColumn7, "GridColumn7")
        Me.GridColumn7.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss tt"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn7.FieldName = "FechaOrden"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        '
        'GridColumn33
        '
        resources.ApplyResources(Me.GridColumn33, "GridColumn33")
        Me.GridColumn33.FieldName = "CodigoCama"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.OptionsColumn.AllowEdit = False
        Me.GridColumn33.OptionsColumn.AllowFocus = False
        '
        'GridColumn8
        '
        resources.ApplyResources(Me.GridColumn8, "GridColumn8")
        Me.GridColumn8.FieldName = "CodigoPaciente"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        '
        'INDColBithDay
        '
        resources.ApplyResources(Me.INDColBithDay, "INDColBithDay")
        Me.INDColBithDay.DisplayFormat.FormatString = "d"
        Me.INDColBithDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColBithDay.FieldName = "BirthDay"
        Me.INDColBithDay.Name = "INDColBithDay"
        Me.INDColBithDay.OptionsColumn.AllowEdit = False
        Me.INDColBithDay.OptionsColumn.AllowFocus = False
        '
        'GridColumn9
        '
        resources.ApplyResources(Me.GridColumn9, "GridColumn9")
        Me.GridColumn9.FieldName = "NombrePaciente"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        '
        'GridColumn52
        '
        resources.ApplyResources(Me.GridColumn52, "GridColumn52")
        Me.GridColumn52.FieldName = "Profesion"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.OptionsColumn.AllowEdit = False
        Me.GridColumn52.OptionsColumn.AllowFocus = False
        '
        'GridColumn10
        '
        resources.ApplyResources(Me.GridColumn10, "GridColumn10")
        Me.GridColumn10.FieldName = "UnidadFuncional"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "NombreMedico"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        '
        'GridColumn39
        '
        resources.ApplyResources(Me.GridColumn39, "GridColumn39")
        Me.GridColumn39.FieldName = "CareCenterDescription"
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.OptionsColumn.AllowEdit = False
        Me.GridColumn39.OptionsColumn.AllowFocus = False
        '
        'INDRptCheActivate
        '
        resources.ApplyResources(Me.INDRptCheActivate, "INDRptCheActivate")
        Me.INDRptCheActivate.Name = "INDRptCheActivate"
        '
        'INDlygExtramural
        '
        Me.INDlygExtramural.AppearanceGroup.Font = CType(resources.GetObject("INDlygExtramural.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlygExtramural.AppearanceGroup.Options.UseFont = True
        Me.INDlygExtramural.AppearanceItemCaption.Font = CType(resources.GetObject("INDlygExtramural.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlygExtramural.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygExtramural.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlygExtramural.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlygExtramural.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygExtramural.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlygExtramural.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlygExtramural.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygExtramural.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlygExtramural.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlygExtramural.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygExtramural.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlygExtramural.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlygExtramural.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygExtramural.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlygExtramural.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlygExtramural.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygExtramural, False)
        Me.INDlygExtramural.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5})
        Me.INDlygExtramural.Location = New System.Drawing.Point(0, 0)
        Me.INDlygExtramural.Name = "INDlygExtramural"
        Me.INDlygExtramural.Size = New System.Drawing.Size(850, 317)
        resources.ApplyResources(Me.INDlygExtramural, "INDlygExtramural")
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDGcExtramural
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(850, 317)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'INDGcExtramural
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcExtramural, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcExtramural, Nothing)
        Me.INDGcExtramural.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcExtramural, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcExtramural, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcExtramural, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcExtramural, False)
        resources.ApplyResources(Me.INDGcExtramural, "INDGcExtramural")
        Me.INDGcExtramural.MainView = Me.INDGvExtramural
        Me.INDGcExtramural.Name = "INDGcExtramural"
        Me.INDGcExtramural.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox2, Me.RepositoryItemCheckEdit11})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcExtramural, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcExtramural.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvExtramural})
        '
        'INDGvExtramural
        '
        Me.INDGvExtramural.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvExtramural.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvExtramural.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvExtramural.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvExtramural.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvExtramural.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvExtramural.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvExtramural.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvExtramural.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvExtramural.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvExtramural.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvExtramural.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvExtramural.Appearance.Row.Font = CType(resources.GetObject("INDGvExtramural.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvExtramural.Appearance.Row.Options.UseFont = True
        Me.INDGvExtramural.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvExtramural.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvExtramural.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvExtramural.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn26, Me.INDColConsecutiveCod, Me.GridColumn27, Me.GridColumn28, Me.INDColBirthDayEx, Me.GridColumn29, Me.GridColumn53, Me.GridColumn30, Me.GridColumn31, Me.GridColumn32, Me.GridColumn45})
        Me.INDGvExtramural.GridControl = Me.INDGcExtramural
        Me.INDGvExtramural.GroupCount = 1
        Me.INDGvExtramural.Name = "INDGvExtramural"
        Me.INDGvExtramural.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvExtramural.OptionsSelection.MultiSelect = True
        Me.INDGvExtramural.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDGvExtramural.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvExtramural.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvExtramural.OptionsView.ShowAutoFilterRow = True
        Me.INDGvExtramural.OptionsView.ShowDetailButtons = False
        Me.INDGvExtramural.OptionsView.ShowFooter = True
        Me.INDGvExtramural.OptionsView.ShowGroupPanel = False
        Me.INDGvExtramural.OptionsView.ShowIndicator = False
        Me.INDGvExtramural.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn32, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvExtramural, False)
        '
        'GridColumn26
        '
        resources.ApplyResources(Me.GridColumn26, "GridColumn26")
        Me.GridColumn26.FieldName = "Ingreso"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.OptionsColumn.AllowEdit = False
        Me.GridColumn26.OptionsColumn.AllowFocus = False
        '
        'INDColConsecutiveCod
        '
        resources.ApplyResources(Me.INDColConsecutiveCod, "INDColConsecutiveCod")
        Me.INDColConsecutiveCod.FieldName = "Row"
        Me.INDColConsecutiveCod.Name = "INDColConsecutiveCod"
        Me.INDColConsecutiveCod.OptionsColumn.AllowEdit = False
        Me.INDColConsecutiveCod.OptionsColumn.AllowFocus = False
        '
        'GridColumn27
        '
        resources.ApplyResources(Me.GridColumn27, "GridColumn27")
        Me.GridColumn27.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss tt"
        Me.GridColumn27.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn27.FieldName = "FechaOrden"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.OptionsColumn.AllowEdit = False
        Me.GridColumn27.OptionsColumn.AllowFocus = False
        '
        'GridColumn28
        '
        resources.ApplyResources(Me.GridColumn28, "GridColumn28")
        Me.GridColumn28.FieldName = "CodigoPaciente"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.OptionsColumn.AllowEdit = False
        Me.GridColumn28.OptionsColumn.AllowFocus = False
        '
        'INDColBirthDayEx
        '
        resources.ApplyResources(Me.INDColBirthDayEx, "INDColBirthDayEx")
        Me.INDColBirthDayEx.DisplayFormat.FormatString = "d"
        Me.INDColBirthDayEx.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColBirthDayEx.FieldName = "BirthDay"
        Me.INDColBirthDayEx.Name = "INDColBirthDayEx"
        Me.INDColBirthDayEx.OptionsColumn.AllowEdit = False
        Me.INDColBirthDayEx.OptionsColumn.AllowFocus = False
        '
        'GridColumn29
        '
        resources.ApplyResources(Me.GridColumn29, "GridColumn29")
        Me.GridColumn29.FieldName = "NombrePaciente"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.OptionsColumn.AllowEdit = False
        Me.GridColumn29.OptionsColumn.AllowFocus = False
        '
        'GridColumn53
        '
        resources.ApplyResources(Me.GridColumn53, "GridColumn53")
        Me.GridColumn53.Name = "GridColumn53"
        Me.GridColumn53.OptionsColumn.AllowEdit = False
        Me.GridColumn53.OptionsColumn.AllowFocus = False
        '
        'GridColumn30
        '
        resources.ApplyResources(Me.GridColumn30, "GridColumn30")
        Me.GridColumn30.FieldName = "UnidadFuncional"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.OptionsColumn.AllowEdit = False
        Me.GridColumn30.OptionsColumn.AllowFocus = False
        '
        'GridColumn31
        '
        resources.ApplyResources(Me.GridColumn31, "GridColumn31")
        Me.GridColumn31.FieldName = "NombreMedico"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.OptionsColumn.AllowFocus = False
        '
        'GridColumn32
        '
        resources.ApplyResources(Me.GridColumn32, "GridColumn32")
        Me.GridColumn32.ColumnEdit = Me.RepositoryItemImageComboBox2
        Me.GridColumn32.FieldName = "Tipo"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.OptionsColumn.AllowEdit = False
        Me.GridColumn32.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemImageComboBox2
        '
        resources.ApplyResources(Me.RepositoryItemImageComboBox2, "RepositoryItemImageComboBox2")
        Me.RepositoryItemImageComboBox2.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox2.Items"), CType(resources.GetObject("RepositoryItemImageComboBox2.Items1"), Object), CType(resources.GetObject("RepositoryItemImageComboBox2.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox2.Items3"), CType(resources.GetObject("RepositoryItemImageComboBox2.Items4"), Object), CType(resources.GetObject("RepositoryItemImageComboBox2.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox2.Items6"), CType(resources.GetObject("RepositoryItemImageComboBox2.Items7"), Object), CType(resources.GetObject("RepositoryItemImageComboBox2.Items8"), Integer))})
        Me.RepositoryItemImageComboBox2.Name = "RepositoryItemImageComboBox2"
        '
        'GridColumn45
        '
        resources.ApplyResources(Me.GridColumn45, "GridColumn45")
        Me.GridColumn45.FieldName = "CareCenterDescription"
        Me.GridColumn45.Name = "GridColumn45"
        Me.GridColumn45.OptionsColumn.AllowEdit = False
        Me.GridColumn45.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemCheckEdit11
        '
        resources.ApplyResources(Me.RepositoryItemCheckEdit11, "RepositoryItemCheckEdit11")
        Me.RepositoryItemCheckEdit11.Name = "RepositoryItemCheckEdit11"
        '
        'INDLcgReturn
        '
        Me.INDLcgReturn.AppearanceGroup.Font = CType(resources.GetObject("INDLcgReturn.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgReturn.AppearanceGroup.Options.UseFont = True
        Me.INDLcgReturn.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgReturn.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgReturn.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgReturn.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgReturn.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgReturn.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgReturn.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgReturn.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgReturn.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgReturn.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgReturn.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgReturn.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgReturn.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgReturn.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgReturn.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgReturn.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgReturn.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgReturn.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgReturn, False)
        resources.ApplyResources(Me.INDLcgReturn, "INDLcgReturn")
        Me.INDLcgReturn.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem11})
        Me.INDLcgReturn.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgReturn.Name = "INDLcgReturn"
        Me.INDLcgReturn.Size = New System.Drawing.Size(850, 317)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.INDGcReturn
        resources.ApplyResources(Me.LayoutControlItem11, "LayoutControlItem11")
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(850, 317)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextVisible = False
        '
        'INDGcReturn
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcReturn, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcReturn, Nothing)
        Me.INDGcReturn.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcReturn, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcReturn, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcReturn, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcReturn, False)
        resources.ApplyResources(Me.INDGcReturn, "INDGcReturn")
        Me.INDGcReturn.MainView = Me.INDGvReturn
        Me.INDGcReturn.Name = "INDGcReturn"
        Me.INDGcReturn.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptCheActivateDevolution})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcReturn, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcReturn.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvReturn})
        '
        'INDGvReturn
        '
        Me.INDGvReturn.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvReturn.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvReturn.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvReturn.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvReturn.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvReturn.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvReturn.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvReturn.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvReturn.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvReturn.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvReturn.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvReturn.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvReturn.Appearance.Row.Font = CType(resources.GetObject("INDGvReturn.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvReturn.Appearance.Row.Options.UseFont = True
        Me.INDGvReturn.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvReturn.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvReturn.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvReturn.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.INDColConsDevCode, Me.GridColumn111, Me.GridColumn34, Me.GridColumn12, Me.INDColBirthDayDev, Me.GridColumn13, Me.GridColumn54, Me.GridColumn14, Me.GridColumn16, Me.GridColumn15, Me.GridColumn46})
        Me.INDGvReturn.GridControl = Me.INDGcReturn
        Me.INDGvReturn.GroupCount = 1
        Me.INDGvReturn.Name = "INDGvReturn"
        Me.INDGvReturn.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvReturn.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.INDGvReturn.OptionsSelection.MultiSelect = True
        Me.INDGvReturn.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDGvReturn.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvReturn.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvReturn.OptionsView.ShowAutoFilterRow = True
        Me.INDGvReturn.OptionsView.ShowDetailButtons = False
        Me.INDGvReturn.OptionsView.ShowFooter = True
        Me.INDGvReturn.OptionsView.ShowGroupPanel = False
        Me.INDGvReturn.OptionsView.ShowIndicator = False
        Me.INDGvReturn.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn15, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvReturn, False)
        '
        'GridColumn6
        '
        resources.ApplyResources(Me.GridColumn6, "GridColumn6")
        Me.GridColumn6.FieldName = "Ingreso"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        '
        'INDColConsDevCode
        '
        resources.ApplyResources(Me.INDColConsDevCode, "INDColConsDevCode")
        Me.INDColConsDevCode.FieldName = "Row"
        Me.INDColConsDevCode.Name = "INDColConsDevCode"
        Me.INDColConsDevCode.OptionsColumn.AllowEdit = False
        Me.INDColConsDevCode.OptionsColumn.AllowFocus = False
        '
        'GridColumn111
        '
        resources.ApplyResources(Me.GridColumn111, "GridColumn111")
        Me.GridColumn111.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss tt"
        Me.GridColumn111.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn111.FieldName = "FechaDevolucion"
        Me.GridColumn111.Name = "GridColumn111"
        Me.GridColumn111.OptionsColumn.AllowEdit = False
        Me.GridColumn111.OptionsColumn.AllowFocus = False
        '
        'GridColumn34
        '
        resources.ApplyResources(Me.GridColumn34, "GridColumn34")
        Me.GridColumn34.FieldName = "CodigoCama"
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.OptionsColumn.ReadOnly = True
        '
        'GridColumn12
        '
        resources.ApplyResources(Me.GridColumn12, "GridColumn12")
        Me.GridColumn12.FieldName = "CodigoPaciente"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        '
        'INDColBirthDayDev
        '
        resources.ApplyResources(Me.INDColBirthDayDev, "INDColBirthDayDev")
        Me.INDColBirthDayDev.DisplayFormat.FormatString = "d"
        Me.INDColBirthDayDev.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColBirthDayDev.FieldName = "BirthDay"
        Me.INDColBirthDayDev.Name = "INDColBirthDayDev"
        Me.INDColBirthDayDev.OptionsColumn.AllowEdit = False
        Me.INDColBirthDayDev.OptionsColumn.AllowFocus = False
        '
        'GridColumn13
        '
        resources.ApplyResources(Me.GridColumn13, "GridColumn13")
        Me.GridColumn13.FieldName = "NombrePaciente"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        '
        'GridColumn54
        '
        resources.ApplyResources(Me.GridColumn54, "GridColumn54")
        Me.GridColumn54.FieldName = "Profesion"
        Me.GridColumn54.Name = "GridColumn54"
        Me.GridColumn54.OptionsColumn.AllowEdit = False
        Me.GridColumn54.OptionsColumn.AllowFocus = False
        '
        'GridColumn14
        '
        resources.ApplyResources(Me.GridColumn14, "GridColumn14")
        Me.GridColumn14.FieldName = "UnidadFuncional"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        '
        'GridColumn16
        '
        resources.ApplyResources(Me.GridColumn16, "GridColumn16")
        Me.GridColumn16.FieldName = "NombreMedico"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        '
        'GridColumn15
        '
        resources.ApplyResources(Me.GridColumn15, "GridColumn15")
        Me.GridColumn15.FieldName = "OrigenDevolutivo"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        '
        'GridColumn46
        '
        resources.ApplyResources(Me.GridColumn46, "GridColumn46")
        Me.GridColumn46.FieldName = "CareCenterDescription"
        Me.GridColumn46.Name = "GridColumn46"
        Me.GridColumn46.OptionsColumn.AllowEdit = False
        Me.GridColumn46.OptionsColumn.AllowFocus = False
        '
        'INDRptCheActivateDevolution
        '
        resources.ApplyResources(Me.INDRptCheActivateDevolution, "INDRptCheActivateDevolution")
        Me.INDRptCheActivateDevolution.Name = "INDRptCheActivateDevolution"
        '
        'LayoutControlGroup31
        '
        Me.LayoutControlGroup31.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup31.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup31.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup31.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup31.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup31.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup31.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup31.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup31.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup31.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup31.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup31.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup31.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup31.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup31.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup31.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup31.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup31.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup31.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup31.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup31.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup31, False)
        Me.LayoutControlGroup31.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem21})
        Me.LayoutControlGroup31.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup31.Name = "LayoutControlGroup31"
        Me.LayoutControlGroup31.Size = New System.Drawing.Size(850, 317)
        resources.ApplyResources(Me.LayoutControlGroup31, "LayoutControlGroup31")
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.INDgcPaquetes
        Me.LayoutControlItem21.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(850, 317)
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem21.TextVisible = False
        '
        'INDgcPaquetes
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcPaquetes, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcPaquetes, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcPaquetes, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcPaquetes, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcPaquetes, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcPaquetes, False)
        resources.ApplyResources(Me.INDgcPaquetes, "INDgcPaquetes")
        Me.INDgcPaquetes.MainView = Me.INDgvPaquetes
        Me.INDgcPaquetes.Name = "INDgcPaquetes"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcPaquetes, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcPaquetes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvPaquetes})
        '
        'INDgvPaquetes
        '
        Me.INDgvPaquetes.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvPaquetes.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgvPaquetes.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgvPaquetes.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvPaquetes.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvPaquetes.Appearance.GroupRow.Font = CType(resources.GetObject("INDgvPaquetes.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgvPaquetes.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvPaquetes.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgvPaquetes.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgvPaquetes.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvPaquetes.Appearance.Row.Font = CType(resources.GetObject("INDgvPaquetes.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgvPaquetes.Appearance.Row.Options.UseFont = True
        Me.INDgvPaquetes.Appearance.ViewCaption.Font = CType(resources.GetObject("INDgvPaquetes.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDgvPaquetes.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvPaquetes.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn68, Me.GridColumn35, Me.GridColumn17, Me.GridColumn18, Me.INDColBirthDaySurg, Me.GridColumn19, Me.GridColumn55, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23, Me.GridColumn24, Me.GridColumn25, Me.GridColumn36, Me.GridColumn20, Me.GridColumn47})
        Me.INDgvPaquetes.GridControl = Me.INDgcPaquetes
        Me.INDgvPaquetes.GroupCount = 1
        Me.INDgvPaquetes.Name = "INDgvPaquetes"
        Me.INDgvPaquetes.OptionsSelection.MultiSelect = True
        Me.INDgvPaquetes.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDgvPaquetes.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvPaquetes.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvPaquetes.OptionsView.ShowAutoFilterRow = True
        Me.INDgvPaquetes.OptionsView.ShowFooter = True
        Me.INDgvPaquetes.OptionsView.ShowGroupPanel = False
        Me.INDgvPaquetes.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn24, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvPaquetes, False)
        '
        'GridColumn68
        '
        resources.ApplyResources(Me.GridColumn68, "GridColumn68")
        Me.GridColumn68.FieldName = "Row"
        Me.GridColumn68.Name = "GridColumn68"
        Me.GridColumn68.OptionsColumn.AllowEdit = False
        Me.GridColumn68.OptionsColumn.AllowFocus = False
        '
        'GridColumn35
        '
        resources.ApplyResources(Me.GridColumn35, "GridColumn35")
        Me.GridColumn35.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss tt"
        Me.GridColumn35.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn35.FieldName = "FechaCirugia"
        Me.GridColumn35.MinWidth = 110
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.OptionsColumn.AllowEdit = False
        Me.GridColumn35.OptionsColumn.ReadOnly = True
        '
        'GridColumn17
        '
        resources.ApplyResources(Me.GridColumn17, "GridColumn17")
        Me.GridColumn17.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss tt"
        Me.GridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn17.FieldName = "FechaOrden"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.OptionsColumn.ReadOnly = True
        '
        'GridColumn18
        '
        resources.ApplyResources(Me.GridColumn18, "GridColumn18")
        Me.GridColumn18.FieldName = "CodigoPaciente"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.OptionsColumn.ReadOnly = True
        '
        'INDColBirthDaySurg
        '
        resources.ApplyResources(Me.INDColBirthDaySurg, "INDColBirthDaySurg")
        Me.INDColBirthDaySurg.DisplayFormat.FormatString = "d"
        Me.INDColBirthDaySurg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColBirthDaySurg.FieldName = "BirthDay"
        Me.INDColBirthDaySurg.Name = "INDColBirthDaySurg"
        Me.INDColBirthDaySurg.OptionsColumn.AllowEdit = False
        Me.INDColBirthDaySurg.OptionsColumn.AllowFocus = False
        '
        'GridColumn19
        '
        resources.ApplyResources(Me.GridColumn19, "GridColumn19")
        Me.GridColumn19.FieldName = "NombrePaciente"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        Me.GridColumn19.OptionsColumn.ReadOnly = True
        '
        'GridColumn55
        '
        resources.ApplyResources(Me.GridColumn55, "GridColumn55")
        Me.GridColumn55.FieldName = "Profesion"
        Me.GridColumn55.Name = "GridColumn55"
        Me.GridColumn55.OptionsColumn.AllowEdit = False
        Me.GridColumn55.OptionsColumn.AllowFocus = False
        '
        'GridColumn21
        '
        resources.ApplyResources(Me.GridColumn21, "GridColumn21")
        Me.GridColumn21.FieldName = "Procedimiento"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        Me.GridColumn21.OptionsColumn.ReadOnly = True
        '
        'GridColumn22
        '
        resources.ApplyResources(Me.GridColumn22, "GridColumn22")
        Me.GridColumn22.FieldName = "NombreMedico"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.OptionsColumn.AllowFocus = False
        Me.GridColumn22.OptionsColumn.ReadOnly = True
        '
        'GridColumn23
        '
        resources.ApplyResources(Me.GridColumn23, "GridColumn23")
        Me.GridColumn23.FieldName = "Especialidad"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.OptionsColumn.AllowFocus = False
        Me.GridColumn23.OptionsColumn.ReadOnly = True
        '
        'GridColumn24
        '
        resources.ApplyResources(Me.GridColumn24, "GridColumn24")
        Me.GridColumn24.FieldName = "OrigenQXDescripcion"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsColumn.AllowFocus = False
        Me.GridColumn24.OptionsColumn.ReadOnly = True
        '
        'GridColumn25
        '
        resources.ApplyResources(Me.GridColumn25, "GridColumn25")
        Me.GridColumn25.FieldName = "Sala"
        Me.GridColumn25.MinWidth = 100
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.OptionsColumn.ReadOnly = True
        '
        'GridColumn36
        '
        resources.ApplyResources(Me.GridColumn36, "GridColumn36")
        Me.GridColumn36.FieldName = "TipoSolicitud"
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.OptionsColumn.AllowEdit = False
        Me.GridColumn36.OptionsColumn.ReadOnly = True
        '
        'GridColumn20
        '
        resources.ApplyResources(Me.GridColumn20, "GridColumn20")
        Me.GridColumn20.FieldName = "UnidadFuncional"
        Me.GridColumn20.MinWidth = 100
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.OptionsColumn.AllowEdit = False
        Me.GridColumn20.OptionsColumn.ReadOnly = True
        '
        'GridColumn47
        '
        resources.ApplyResources(Me.GridColumn47, "GridColumn47")
        Me.GridColumn47.FieldName = "CareCenterDescription"
        Me.GridColumn47.Name = "GridColumn47"
        Me.GridColumn47.OptionsColumn.AllowEdit = False
        Me.GridColumn47.OptionsColumn.AllowFocus = False
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup4.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup4.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup4.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup4.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup4.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup4, False)
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemChemotherapy})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(850, 317)
        resources.ApplyResources(Me.LayoutControlGroup4, "LayoutControlGroup4")
        '
        'INDlyItemChemotherapy
        '
        Me.INDlyItemChemotherapy.Control = Me.INDgcChemotherapy
        Me.INDlyItemChemotherapy.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemChemotherapy.Name = "INDlyItemChemotherapy"
        Me.INDlyItemChemotherapy.Size = New System.Drawing.Size(850, 317)
        Me.INDlyItemChemotherapy.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemChemotherapy.TextVisible = False
        '
        'INDgcChemotherapy
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcChemotherapy, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcChemotherapy, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcChemotherapy, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcChemotherapy, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcChemotherapy, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcChemotherapy, False)
        resources.ApplyResources(Me.INDgcChemotherapy, "INDgcChemotherapy")
        Me.INDgcChemotherapy.MainView = Me.INDviewChemoterapy
        Me.INDgcChemotherapy.Name = "INDgcChemotherapy"
        Me.INDgcChemotherapy.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox3})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcChemotherapy, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcChemotherapy.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewChemoterapy})
        '
        'INDviewChemoterapy
        '
        Me.INDviewChemoterapy.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewChemoterapy.Appearance.FocusedRow.Font = CType(resources.GetObject("INDviewChemoterapy.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDviewChemoterapy.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewChemoterapy.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewChemoterapy.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewChemoterapy.Appearance.GroupRow.Font = CType(resources.GetObject("INDviewChemoterapy.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDviewChemoterapy.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewChemoterapy.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDviewChemoterapy.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDviewChemoterapy.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewChemoterapy.Appearance.Row.Font = CType(resources.GetObject("INDviewChemoterapy.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDviewChemoterapy.Appearance.Row.Options.UseFont = True
        Me.INDviewChemoterapy.Appearance.ViewCaption.Font = CType(resources.GetObject("INDviewChemoterapy.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDviewChemoterapy.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewChemoterapy.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn37, Me.GridColumn67, Me.GridColumn69, Me.GridColumn38, Me.GridColumn49, Me.GridColumn50, Me.GridColumn51, Me.GridColumn40, Me.INDColBirthDayQ, Me.GridColumn41, Me.GridColumn56, Me.GridColumn42, Me.GridColumn43, Me.GridColumn44, Me.GridColumn48})
        Me.INDviewChemoterapy.GridControl = Me.INDgcChemotherapy
        Me.INDviewChemoterapy.GroupCount = 1
        Me.INDviewChemoterapy.Name = "INDviewChemoterapy"
        Me.INDviewChemoterapy.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewChemoterapy.OptionsSelection.MultiSelect = True
        Me.INDviewChemoterapy.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDviewChemoterapy.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewChemoterapy.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewChemoterapy.OptionsView.ShowAutoFilterRow = True
        Me.INDviewChemoterapy.OptionsView.ShowDetailButtons = False
        Me.INDviewChemoterapy.OptionsView.ShowFooter = True
        Me.INDviewChemoterapy.OptionsView.ShowGroupPanel = False
        Me.INDviewChemoterapy.OptionsView.ShowIndicator = False
        Me.INDviewChemoterapy.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn44, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewChemoterapy, False)
        '
        'GridColumn37
        '
        resources.ApplyResources(Me.GridColumn37, "GridColumn37")
        Me.GridColumn37.FieldName = "Ingreso"
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.OptionsColumn.AllowEdit = False
        Me.GridColumn37.OptionsColumn.AllowFocus = False
        '
        'GridColumn67
        '
        resources.ApplyResources(Me.GridColumn67, "GridColumn67")
        Me.GridColumn67.FieldName = "AuthorizationNumber"
        Me.GridColumn67.Name = "GridColumn67"
        Me.GridColumn67.OptionsColumn.AllowEdit = False
        Me.GridColumn67.OptionsColumn.AllowFocus = False
        '
        'GridColumn69
        '
        resources.ApplyResources(Me.GridColumn69, "GridColumn69")
        Me.GridColumn69.FieldName = "Row"
        Me.GridColumn69.Name = "GridColumn69"
        Me.GridColumn69.OptionsColumn.AllowEdit = False
        Me.GridColumn69.OptionsColumn.AllowFocus = False
        '
        'GridColumn38
        '
        resources.ApplyResources(Me.GridColumn38, "GridColumn38")
        Me.GridColumn38.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss tt"
        Me.GridColumn38.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn38.FieldName = "FechaOrden"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.OptionsColumn.AllowEdit = False
        Me.GridColumn38.OptionsColumn.AllowFocus = False
        '
        'GridColumn49
        '
        resources.ApplyResources(Me.GridColumn49, "GridColumn49")
        Me.GridColumn49.FieldName = "DIA"
        Me.GridColumn49.Name = "GridColumn49"
        Me.GridColumn49.OptionsColumn.AllowEdit = False
        Me.GridColumn49.OptionsColumn.AllowFocus = False
        '
        'GridColumn50
        '
        resources.ApplyResources(Me.GridColumn50, "GridColumn50")
        Me.GridColumn50.FieldName = "FechaCita"
        Me.GridColumn50.Name = "GridColumn50"
        Me.GridColumn50.OptionsColumn.AllowEdit = False
        Me.GridColumn50.OptionsColumn.AllowFocus = False
        '
        'GridColumn51
        '
        resources.ApplyResources(Me.GridColumn51, "GridColumn51")
        Me.GridColumn51.FieldName = "EstadoCita"
        Me.GridColumn51.Name = "GridColumn51"
        Me.GridColumn51.OptionsColumn.AllowEdit = False
        Me.GridColumn51.OptionsColumn.AllowFocus = False
        '
        'GridColumn40
        '
        resources.ApplyResources(Me.GridColumn40, "GridColumn40")
        Me.GridColumn40.FieldName = "CodigoPaciente"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.OptionsColumn.AllowEdit = False
        Me.GridColumn40.OptionsColumn.AllowFocus = False
        '
        'INDColBirthDayQ
        '
        resources.ApplyResources(Me.INDColBirthDayQ, "INDColBirthDayQ")
        Me.INDColBirthDayQ.DisplayFormat.FormatString = "d"
        Me.INDColBirthDayQ.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColBirthDayQ.FieldName = "BirthDay"
        Me.INDColBirthDayQ.Name = "INDColBirthDayQ"
        Me.INDColBirthDayQ.OptionsColumn.AllowEdit = False
        Me.INDColBirthDayQ.OptionsColumn.AllowFocus = False
        '
        'GridColumn41
        '
        resources.ApplyResources(Me.GridColumn41, "GridColumn41")
        Me.GridColumn41.FieldName = "NombrePaciente"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.OptionsColumn.AllowEdit = False
        Me.GridColumn41.OptionsColumn.AllowFocus = False
        '
        'GridColumn56
        '
        resources.ApplyResources(Me.GridColumn56, "GridColumn56")
        Me.GridColumn56.FieldName = "Profesion"
        Me.GridColumn56.Name = "GridColumn56"
        Me.GridColumn56.OptionsColumn.AllowEdit = False
        Me.GridColumn56.OptionsColumn.AllowFocus = False
        '
        'GridColumn42
        '
        resources.ApplyResources(Me.GridColumn42, "GridColumn42")
        Me.GridColumn42.FieldName = "UnidadFuncional"
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.OptionsColumn.AllowEdit = False
        Me.GridColumn42.OptionsColumn.AllowFocus = False
        '
        'GridColumn43
        '
        resources.ApplyResources(Me.GridColumn43, "GridColumn43")
        Me.GridColumn43.FieldName = "NombreMedico"
        Me.GridColumn43.Name = "GridColumn43"
        Me.GridColumn43.OptionsColumn.AllowEdit = False
        Me.GridColumn43.OptionsColumn.AllowFocus = False
        '
        'GridColumn44
        '
        resources.ApplyResources(Me.GridColumn44, "GridColumn44")
        Me.GridColumn44.ColumnEdit = Me.RepositoryItemImageComboBox3
        Me.GridColumn44.FieldName = "Tipo"
        Me.GridColumn44.Name = "GridColumn44"
        Me.GridColumn44.OptionsColumn.AllowEdit = False
        Me.GridColumn44.OptionsColumn.AllowFocus = False
        '
        'RepositoryItemImageComboBox3
        '
        resources.ApplyResources(Me.RepositoryItemImageComboBox3, "RepositoryItemImageComboBox3")
        Me.RepositoryItemImageComboBox3.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox3.Items"), CType(resources.GetObject("RepositoryItemImageComboBox3.Items1"), Object), CType(resources.GetObject("RepositoryItemImageComboBox3.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox3.Items3"), CType(resources.GetObject("RepositoryItemImageComboBox3.Items4"), Object), CType(resources.GetObject("RepositoryItemImageComboBox3.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("RepositoryItemImageComboBox3.Items6"), CType(resources.GetObject("RepositoryItemImageComboBox3.Items7"), Object), CType(resources.GetObject("RepositoryItemImageComboBox3.Items8"), Integer))})
        Me.RepositoryItemImageComboBox3.Name = "RepositoryItemImageComboBox3"
        '
        'GridColumn48
        '
        resources.ApplyResources(Me.GridColumn48, "GridColumn48")
        Me.GridColumn48.FieldName = "CareCenterDescription"
        Me.GridColumn48.Name = "GridColumn48"
        Me.GridColumn48.OptionsColumn.AllowEdit = False
        Me.GridColumn48.OptionsColumn.AllowFocus = False
        '
        'LayoutControlGroup11
        '
        Me.LayoutControlGroup11.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup11.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup11.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup11.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup11.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup11.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup11.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup11.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup11.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup11.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup11.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup11, False)
        Me.LayoutControlGroup11.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutControlGroup11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup11.Name = "LayoutControlGroup11"
        Me.LayoutControlGroup11.Size = New System.Drawing.Size(874, 89)
        Me.LayoutControlGroup11.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.PanelControl11
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(1, 1)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(850, 65)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'PanelControl11
        '
        Me.PanelControl11.Controls.Add(Me.INDSleCareCenter)
        resources.ApplyResources(Me.PanelControl11, "PanelControl11")
        Me.PanelControl11.Name = "PanelControl11"
        '
        'INDSleCareCenter
        '
        resources.ApplyResources(Me.INDSleCareCenter, "INDSleCareCenter")
        Me.INDSleCareCenter.Name = "INDSleCareCenter"
        Me.INDSleCareCenter.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleCareCenter.Properties.Appearance.BackColor2 = CType(resources.GetObject("INDSleCareCenter.Properties.Appearance.BackColor2"), System.Drawing.Color)
        Me.INDSleCareCenter.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.Font = CType(resources.GetObject("INDSleCareCenter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleCareCenter.Properties.Appearance.ForeColor = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseBorderColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleCareCenter.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDSleCareCenter.Properties.DisplayMember = "CodeName"
        Me.INDSleCareCenter.Properties.NullText = resources.GetString("INDSleCareCenter.Properties.NullText")
        Me.INDSleCareCenter.Properties.PopupSizeable = False
        Me.INDSleCareCenter.Properties.PopupView = Me.INDviewSearchCareCenter
        Me.INDSleCareCenter.Properties.ShowClearButton = False
        Me.INDSleCareCenter.Properties.ShowFooter = False
        Me.INDSleCareCenter.Properties.ValueMember = "CODCENATE"
        Me.INDSleCareCenter.StyleController = Me.INDLcMainData1
        '
        'INDviewSearchCareCenter
        '
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.Font = CType(resources.GetObject("INDviewSearchCareCenter.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewSearchCareCenter.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewSearchCareCenter.Appearance.GroupRow.Font = CType(resources.GetObject("INDviewSearchCareCenter.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDviewSearchCareCenter.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewSearchCareCenter.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDviewSearchCareCenter.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDviewSearchCareCenter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewSearchCareCenter.Appearance.Row.Font = CType(resources.GetObject("INDviewSearchCareCenter.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDviewSearchCareCenter.Appearance.Row.Options.UseFont = True
        Me.INDviewSearchCareCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn2})
        Me.INDviewSearchCareCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewSearchCareCenter.Name = "INDviewSearchCareCenter"
        Me.INDviewSearchCareCenter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewSearchCareCenter.OptionsSelection.MultiSelect = True
        Me.INDviewSearchCareCenter.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDviewSearchCareCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewSearchCareCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewSearchCareCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDviewSearchCareCenter.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewSearchCareCenter, False)
        '
        'GridColumn11
        '
        resources.ApplyResources(Me.GridColumn11, "GridColumn11")
        Me.GridColumn11.FieldName = "CODCENATE"
        Me.GridColumn11.Name = "GridColumn11"
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "NOMCENATE"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'INDLcMainData1
        '
        Me.INDLcMainData1.AllowCustomization = False
        Me.INDLcMainData1.Controls.Add(Me.INDTbAutomaticReload)
        Me.INDLcMainData1.Controls.Add(Me.PcContainerReportPhamacyNotes)
        Me.INDLcMainData1.Controls.Add(Me.INDgcMixingStation)
        Me.INDLcMainData1.Controls.Add(Me.INDgcChemotherapy)
        Me.INDLcMainData1.Controls.Add(Me.INDGcExtramural)
        Me.INDLcMainData1.Controls.Add(Me.INDgcPaquetes)
        Me.INDLcMainData1.Controls.Add(Me.INDPcContainer)
        Me.INDLcMainData1.Controls.Add(Me.PanelControl11)
        Me.INDLcMainData1.Controls.Add(Me.INDGcReturn)
        Me.INDLcMainData1.Controls.Add(Me.INDGcRequest)
        Me.INDLcMainData1.Controls.Add(Me.INDSleTypeFilter)
        resources.ApplyResources(Me.INDLcMainData1, "INDLcMainData1")
        Me.LayoutControls.SetIsCustomizable(Me.INDLcMainData1, False)
        Me.INDLcMainData1.Name = "INDLcMainData1"
        Me.INDLcMainData1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(374, 111, 574, 569)
        Me.INDLcMainData1.Root = Me.LayoutControlGroup21
        '
        'INDTbAutomaticReload
        '
        Me.INDTbAutomaticReload.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTbAutomaticReload, "INDTbAutomaticReload")
        Me.INDTbAutomaticReload.Name = "INDTbAutomaticReload"
        Me.INDTbAutomaticReload.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.INDTbAutomaticReload.Properties.GlyphAlignment = CType(resources.GetObject("INDTbAutomaticReload.Properties.GlyphAlignment"), DevExpress.Utils.HorzAlignment)
        Me.INDTbAutomaticReload.Properties.OffText = resources.GetString("INDTbAutomaticReload.Properties.OffText")
        Me.INDTbAutomaticReload.Properties.OnText = resources.GetString("INDTbAutomaticReload.Properties.OnText")
        Me.INDTbAutomaticReload.Properties.ShowText = False
        Me.INDTbAutomaticReload.StyleController = Me.INDLcMainData1
        '
        'PcContainerReportPhamacyNotes
        '
        Me.PcContainerReportPhamacyNotes.Controls.Add(Me.INDPcReportViewerPharmacyNotes)
        Me.PcContainerReportPhamacyNotes.Controls.Add(Me.PanelControl3)
        resources.ApplyResources(Me.PcContainerReportPhamacyNotes, "PcContainerReportPhamacyNotes")
        Me.PcContainerReportPhamacyNotes.Name = "PcContainerReportPhamacyNotes"
        '
        'INDPcReportViewerPharmacyNotes
        '
        resources.ApplyResources(Me.INDPcReportViewerPharmacyNotes, "INDPcReportViewerPharmacyNotes")
        Me.INDPcReportViewerPharmacyNotes.Name = "INDPcReportViewerPharmacyNotes"
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.INDBtnCloseReportViewerPharmacyNote)
        resources.ApplyResources(Me.PanelControl3, "PanelControl3")
        Me.PanelControl3.Name = "PanelControl3"
        '
        'INDBtnCloseReportViewerPharmacyNote
        '
        resources.ApplyResources(Me.INDBtnCloseReportViewerPharmacyNote, "INDBtnCloseReportViewerPharmacyNote")
        Me.INDBtnCloseReportViewerPharmacyNote.Name = "INDBtnCloseReportViewerPharmacyNote"
        '
        'INDPcContainer
        '
        Me.INDPcContainer.Controls.Add(Me.INDPcReportViewer)
        Me.INDPcContainer.Controls.Add(Me.PanelControl2)
        resources.ApplyResources(Me.INDPcContainer, "INDPcContainer")
        Me.INDPcContainer.Name = "INDPcContainer"
        '
        'INDPcReportViewer
        '
        resources.ApplyResources(Me.INDPcReportViewer, "INDPcReportViewer")
        Me.INDPcReportViewer.Name = "INDPcReportViewer"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.INDBtnCloseReportViewer)
        resources.ApplyResources(Me.PanelControl2, "PanelControl2")
        Me.PanelControl2.Name = "PanelControl2"
        '
        'INDBtnCloseReportViewer
        '
        resources.ApplyResources(Me.INDBtnCloseReportViewer, "INDBtnCloseReportViewer")
        Me.INDBtnCloseReportViewer.Name = "INDBtnCloseReportViewer"
        '
        'INDSleTypeFilter
        '
        resources.ApplyResources(Me.INDSleTypeFilter, "INDSleTypeFilter")
        Me.INDSleTypeFilter.Name = "INDSleTypeFilter"
        Me.INDSleTypeFilter.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleTypeFilter.Properties.Appearance.Font = CType(resources.GetObject("INDSleTypeFilter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleTypeFilter.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleTypeFilter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleTypeFilter.Properties.Appearance.Options.UseFont = True
        Me.INDSleTypeFilter.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleTypeFilter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleTypeFilter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleTypeFilter.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDSleTypeFilter.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDSleTypeFilter.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleTypeFilter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleTypeFilter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleTypeFilter.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleTypeFilter.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleTypeFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSleTypeFilter.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDSleTypeFilter.Properties.DisplayMember = "Item2"
        Me.INDSleTypeFilter.Properties.NullText = resources.GetString("INDSleTypeFilter.Properties.NullText")
        Me.INDSleTypeFilter.Properties.PopupSizeable = False
        Me.INDSleTypeFilter.Properties.PopupView = Me.INDgvSearchType
        Me.INDSleTypeFilter.Properties.ShowClearButton = False
        Me.INDSleTypeFilter.Properties.ShowFooter = False
        Me.INDSleTypeFilter.Properties.ValueMember = "Item1"
        Me.INDSleTypeFilter.StyleController = Me.INDLcMainData1
        '
        'INDgvSearchType
        '
        Me.INDgvSearchType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvSearchType.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgvSearchType.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgvSearchType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvSearchType.Appearance.GroupRow.Font = CType(resources.GetObject("INDgvSearchType.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgvSearchType.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvSearchType.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgvSearchType.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgvSearchType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvSearchType.Appearance.Row.Font = CType(resources.GetObject("INDgvSearchType.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgvSearchType.Appearance.Row.Options.UseFont = True
        Me.INDgvSearchType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn272})
        Me.INDgvSearchType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvSearchType.Name = "INDgvSearchType"
        Me.INDgvSearchType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvSearchType.OptionsSelection.MultiSelect = True
        Me.INDgvSearchType.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDgvSearchType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvSearchType.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvSearchType.OptionsView.ShowAutoFilterRow = True
        Me.INDgvSearchType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvSearchType, False)
        '
        'GridColumn272
        '
        resources.ApplyResources(Me.GridColumn272, "GridColumn272")
        Me.GridColumn272.FieldName = "Item2"
        Me.GridColumn272.Name = "GridColumn272"
        '
        'INDLciReportViewer
        '
        Me.INDLciReportViewer.Control = Me.INDPcContainer
        Me.INDLciReportViewer.Location = New System.Drawing.Point(874, 0)
        Me.INDLciReportViewer.MaxSize = New System.Drawing.Size(400, 0)
        Me.INDLciReportViewer.MinSize = New System.Drawing.Size(400, 25)
        Me.INDLciReportViewer.Name = "INDLciReportViewer"
        Me.INDLciReportViewer.Size = New System.Drawing.Size(400, 296)
        Me.INDLciReportViewer.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReportViewer.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciReportViewer.TextVisible = False
        Me.INDLciReportViewer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLycReportPhamacyNotes
        '
        Me.INDLycReportPhamacyNotes.Control = Me.PcContainerReportPhamacyNotes
        Me.INDLycReportPhamacyNotes.Location = New System.Drawing.Point(874, 296)
        Me.INDLycReportPhamacyNotes.Name = "INDLycReportPhamacyNotes"
        Me.INDLycReportPhamacyNotes.Size = New System.Drawing.Size(400, 225)
        Me.INDLycReportPhamacyNotes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLycReportPhamacyNotes.TextVisible = False
        Me.INDLycReportPhamacyNotes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLycTypeFilter
        '
        Me.INDLycTypeFilter.AppearanceGroup.Font = CType(resources.GetObject("INDLycTypeFilter.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLycTypeFilter.AppearanceGroup.Options.UseFont = True
        Me.INDLycTypeFilter.AppearanceItemCaption.Font = CType(resources.GetObject("INDLycTypeFilter.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLycTypeFilter.AppearanceItemCaption.Options.UseFont = True
        Me.INDLycTypeFilter.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLycTypeFilter.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLycTypeFilter.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLycTypeFilter.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLycTypeFilter.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLycTypeFilter.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLycTypeFilter.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLycTypeFilter.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLycTypeFilter.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLycTypeFilter.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLycTypeFilter.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLycTypeFilter.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLycTypeFilter.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLycTypeFilter.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLycTypeFilter.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLycTypeFilter, False)
        resources.ApplyResources(Me.INDLycTypeFilter, "INDLycTypeFilter")
        Me.INDLycTypeFilter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyTypeFilter, Me.INDLciToggleButton})
        Me.INDLycTypeFilter.Location = New System.Drawing.Point(0, 89)
        Me.INDLycTypeFilter.Name = "INDLycTypeFilter"
        Me.INDLycTypeFilter.Size = New System.Drawing.Size(874, 60)
        Me.INDLycTypeFilter.TextVisible = False
        Me.INDLycTypeFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyTypeFilter
        '
        Me.INDLyTypeFilter.Control = Me.INDSleTypeFilter
        resources.ApplyResources(Me.INDLyTypeFilter, "INDLyTypeFilter")
        Me.INDLyTypeFilter.Location = New System.Drawing.Point(0, 0)
        Me.INDLyTypeFilter.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLyTypeFilter.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTypeFilter.Name = "INDLyTypeFilter"
        Me.INDLyTypeFilter.Size = New System.Drawing.Size(796, 36)
        Me.INDLyTypeFilter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTypeFilter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyTypeFilter.TextSize = New System.Drawing.Size(40, 21)
        Me.INDLyTypeFilter.TextToControlDistance = 5
        '
        'INDLciToggleButton
        '
        Me.INDLciToggleButton.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDLciToggleButton.Control = Me.INDTbAutomaticReload
        Me.INDLciToggleButton.Location = New System.Drawing.Point(796, 0)
        Me.INDLciToggleButton.MaxSize = New System.Drawing.Size(0, 22)
        Me.INDLciToggleButton.MinSize = New System.Drawing.Size(54, 22)
        Me.INDLciToggleButton.Name = "INDLciToggleButton"
        Me.INDLciToggleButton.Size = New System.Drawing.Size(54, 36)
        Me.INDLciToggleButton.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciToggleButton.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciToggleButton.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'RepositoryItemPopupContainerEdit31
        '
        Me.RepositoryItemPopupContainerEdit31.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit31.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit31.Name = "RepositoryItemPopupContainerEdit31"
        '
        'RepositoryItemPopupContainerEdit6
        '
        Me.RepositoryItemPopupContainerEdit6.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit6.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit6.Name = "RepositoryItemPopupContainerEdit6"
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit11.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'RepositoryItemPopupContainerEdit5
        '
        Me.RepositoryItemPopupContainerEdit5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit5.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit5.Name = "RepositoryItemPopupContainerEdit5"
        '
        'RepositoryItemPopupContainerEdit4
        '
        Me.RepositoryItemPopupContainerEdit4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit4.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit4.Name = "RepositoryItemPopupContainerEdit4"
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit2.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemPopupContainerEdit7
        '
        Me.RepositoryItemPopupContainerEdit7.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit7.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit7.Name = "RepositoryItemPopupContainerEdit7"
        '
        'FrmIntegrationErrorLogs
        '
        Me.AdditionalControlPanel = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmIntegrationErrorLogs"
        Me.Opacity = 1.0R
        Me.Tag = "2039"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewMixingStationDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcMixingStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewMixingStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMainData.ResumeLayout(False)
        CType(Me.INDPccIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccIntegrationTransactionDetails.ResumeLayout(False)
        CType(Me.INDLcIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcIntegrationTransactionDetails.ResumeLayout(False)
        CType(Me.INDGcIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIntegrationTransactionDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcIntegrationInvalid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvIntegrationTransactionsInvalid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceTransactionDetailsInvalid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcIntegrationValid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvIntegrationTransactionsValid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceTransactionDetailValid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPmActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgTransactionMessages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgInvalidTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTransactionInvalid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgValidTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceDetailsValid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgDashBoard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMixingStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyMixingStation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptCheActivate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygExtramural, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcExtramural, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvExtramural, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvReturn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptCheActivateDevolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcPaquetes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvPaquetes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemChemotherapy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcChemotherapy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewChemoterapy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl11.ResumeLayout(False)
        CType(Me.INDSleCareCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewSearchCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMainData1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMainData1.ResumeLayout(False)
        CType(Me.INDTbAutomaticReload.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PcContainerReportPhamacyNotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PcContainerReportPhamacyNotes.ResumeLayout(False)
        CType(Me.INDPcReportViewerPharmacyNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.INDPcContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcContainer.ResumeLayout(False)
        CType(Me.INDPcReportViewer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.INDSleTypeFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvSearchType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReportViewer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycReportPhamacyNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycTypeFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTypeFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciToggleButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDLcMainData As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDTcgTransactionMessages As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgValidTransactions As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcIntegrationInvalid As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvIntegrationTransactionsInvalid As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvIntegrationErrors_MessageId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIntegrationErrors_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPccIntegrationTransactionDetails As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcIntegrationTransactionDetails As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgIntegrationTransactionDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcIntegrationTransactionDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvIntegrationTransactionDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvIntegrationTransactionDetails_LogMessage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIntegrationTransactionDetails_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIntegrationTransactionDetails_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIntegrationErrors_Details As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIntegrationErrors_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvIntegrationErrors_Source As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPceDetailsValid As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvIntegrationErrors_Transmitter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDLciIntegrationTransactionDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcIntegrationValid As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvIntegrationTransactionsValid As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTransactionLog_Source As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTransactionLog_MessageId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTransactionLog_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTransactionLog_Transmitter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTransactionLog_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTransactionLog_Details As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPceTransactionDetailsInvalid As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDPceTransactionDetailValid As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents RepositoryItemPopupContainerEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit31 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDDdbMenuActions As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDPmActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDLcMainData1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTbAutomaticReload As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents PcContainerReportPhamacyNotes As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDPcReportViewerPharmacyNotes As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnCloseReportViewerPharmacyNote As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgcMixingStation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewMixingStationDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn73 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn77 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn78 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn79 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewMixingStation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn62 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBirthDayMS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox4 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDgcChemotherapy As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewChemoterapy As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn67 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn69 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBirthDayQ As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox3 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcExtramural As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvExtramural As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColConsecutiveCod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBirthDayEx As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox2 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDgcPaquetes As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvPaquetes As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn68 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBirthDaySurg As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPcContainer As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDPcReportViewer As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnCloseReportViewer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl11 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDSleCareCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewSearchCareCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcReturn As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvReturn As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColConsDevCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBirthDayDev As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptCheActivateDevolution As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDGcRequest As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvRequest As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox11 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColConsecutiveCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBithDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptCheActivate As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDSleTypeFilter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvSearchType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn272 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup21 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTcgDashBoard As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlcgMixingStation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyMixingStation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgRequest As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciRequest As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygExtramural As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgReturn As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup31 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemChemotherapy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup11 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciReportViewer As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLycReportPhamacyNotes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLycTypeFilter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyTypeFilter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciToggleButton As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgInvalidTransactions As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciTransactionInvalid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcMenuActions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBbiRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBtsAutomaticRefresh As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
End Class
