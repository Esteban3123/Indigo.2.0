Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLiquidation
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLiquidation))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GdcStays = New DevExpress.XtraGrid.GridControl()
        Me.GdvStays = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColEstBed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstInitDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstTotalTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstTotalUnits = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstShowDetails = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepEstShowDetails = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.ColEstFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.MbtnUndo = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnFind = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnLiquidateAll = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnSmallPrintAll = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnViewVoidInvoices = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnPrintAll = New DevExpress.XtraBars.BarSubItem()
        Me.MbtnLargePrintAll = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnPrintDetailandParentAccount = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnLiquidateStays = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnViewFolios = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnView = New DevExpress.XtraBars.BarSubItem()
        Me.MbtnFlow = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnNavigation = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnConfirmAdmission = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnOpenAdmission = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnFacturarIngreso = New DevExpress.XtraBars.BarButtonItem()
        Me.MBtnModifyAuthorization = New DevExpress.XtraBars.BarButtonItem()
        Me.MBtnAnulateAllAmbulatory = New DevExpress.XtraBars.BarButtonItem()
        Me.MBtnIngresosRelacionados = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnIncomeLock = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnIncomeUnlock = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnLiquidateData = New DevExpress.XtraBars.BarButtonItem()
        Me.Mbtn = New DevExpress.XtraBars.BarButtonItem()
        Me.MbtnShowMotherAccountReport = New DevExpress.XtraBars.BarButtonItem()
        Me.LycHeader = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleAdmissionNumber2 = New Presentation.Controls.SearchLookUpEditExAdmission()
        Me.viewSearchAdmission = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TxtStatusAdmission = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.DdbMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.PpmActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.BteCountFolio = New DevExpress.XtraEditors.ButtonEdit()
        Me.LblTotalPatient = New DevExpress.XtraEditors.LabelControl()
        Me.LblTotalEntity = New DevExpress.XtraEditors.LabelControl()
        Me.TxtAdmissionDate1 = New DevExpress.XtraEditors.TextEdit()
        Me.SleBillingAuthorization = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GdvBillingAuthorization = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColBACode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColBAName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SleOperatingUnit = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GdvOperatingUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColOPCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColOPName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TxtAdmissionType1 = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtPlaceEntry1 = New DevExpress.XtraEditors.TextEdit()
        Me.LycgHeader = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LycgGeneralGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LciStatusAdmission = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdmission = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem32 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem33 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem34 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.PccAdmission = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LycPccAdmission = New DevExpress.XtraLayout.LayoutControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtContact = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientEstrato = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientEntityName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtCareGroupPatient = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientAge = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientBirth = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPatientType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtAfiliationType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtPatientCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.TxtRiskType = New DevExpress.XtraEditors.TextEdit()
        Me.TxtCareGroupAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtBedStay = New DevExpress.XtraEditors.TextEdit()
        Me.TxtResponsiblePhone = New DevExpress.XtraEditors.TextEdit()
        Me.TxtResponsibleName = New DevExpress.XtraEditors.TextEdit()
        Me.TxtEntityNameAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAtentionCenter = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAuthorization = New DevExpress.XtraEditors.TextEdit()
        Me.TxtPlaceEntry = New DevExpress.XtraEditors.TextEdit()
        Me.TxtFunctionalUnitAdmission = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAdmissionDate = New DevExpress.XtraEditors.TextEdit()
        Me.TxtAdmissionType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtLiquidationType = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TxtAdmissionCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.GdcStaysLiquidated = New DevExpress.XtraGrid.GridControl()
        Me.GdvStaysLiquidated = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColEstLiqBed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqInitDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqTotalTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqTotalDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqLiqType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqFolio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepEstLiqShowFolios = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.PccStayFolios = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.GdcStayFolios = New DevExpress.XtraGrid.GridControl()
        Me.GdvStayFolios = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColStayFoliosNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColStayFoliosValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem38 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.ColEstLiqShowDetails = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepEstLiqShowDetails = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LycgPccAdmission = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem25 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LiCareGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem29 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem26 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LiEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TxtContacto = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LycgAdmissionGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem27 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem39 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LycgStays = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup2 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LycgStaysDontLiquidated = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem36 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LycgStaysLiquidated = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem35 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem31 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PnlBodySearch = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl9 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGCExportExcel = New DevExpress.XtraGrid.GridControl()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSubtotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDiscount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNetValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup21 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem30 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PccStayDetails = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.GdcStayDetails = New DevExpress.XtraGrid.GridControl()
        Me.GdvStayDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColEstLiqDetDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqDetCups = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEstLiqDetTotalUnits = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem37 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNoData1 = New Presentation.Billing.CtrNoData()
        Me.PnlProgressPanel = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.PnlBodyDashboard = New Presentation.Controls.CtrContainerControl()
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.PnlNotifications = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.GdcNotifications = New DevExpress.XtraGrid.GridControl()
        Me.GdvNotifications = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColNotiIcon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNotiTitle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColNotiMessage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PnlCtrlHeader = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl3 = New DevExpress.XtraLayout.LayoutControl()
        Me.MarqueeProgressBarControl1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem6 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem7 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem13 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem12 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.BarButtonItem14 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem9 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem11 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarSubItem2 = New DevExpress.XtraBars.BarSubItem()
        Me.BarButtonItem15 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem16 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem10 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem8 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem17 = New DevExpress.XtraBars.BarButtonItem()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LyciBusyIndicator = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DocumentManager = New DevExpress.XtraBars.Docking2010.DocumentManager(Me.components)
        Me.WidgetView = New DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView(Me.components)
        Me.StackGroup1 = New DevExpress.XtraBars.Docking2010.Views.Widget.StackGroup(Me.components)
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl4 = New DevExpress.XtraLayout.LayoutControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup9 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PopupContainerControl2 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl5 = New DevExpress.XtraLayout.LayoutControl()
        Me.GridControl2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup10 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup11 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem28 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PopupContainerControl3 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl6 = New DevExpress.XtraLayout.LayoutControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit2 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit3 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit4 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit5 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit6 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit7 = New DevExpress.XtraEditors.TextEdit()
        Me.ImageComboBoxEdit1 = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ImageComboBoxEdit2 = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ButtonEdit1 = New DevExpress.XtraEditors.ButtonEdit()
        Me.GridControl3 = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.TextEdit8 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit9 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit10 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit11 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit12 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit13 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit14 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit15 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit16 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit17 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit18 = New DevExpress.XtraEditors.TextEdit()
        Me.ImageComboBoxEdit3 = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ImageComboBoxEdit4 = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ButtonEdit2 = New DevExpress.XtraEditors.ButtonEdit()
        Me.GridControl4 = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControlGroup12 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup3 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup13 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem40 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem41 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem42 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem43 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem44 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem45 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem46 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem47 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem48 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem49 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup14 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem50 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem51 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem52 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem53 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem54 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem55 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem56 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem57 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem58 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem59 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem60 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem61 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem62 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem63 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup15 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.TabbedControlGroup4 = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup16 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem64 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup17 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem65 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem66 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.PanelControl5 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl7 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup18 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControl8 = New DevExpress.XtraLayout.LayoutControl()
        Me.SearchLookUpEditExAdmission1 = New Presentation.Controls.SearchLookUpEditExAdmission()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl6 = New DevExpress.XtraEditors.PanelControl()
        Me.DropDownButton1 = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.ButtonEdit3 = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.TextEdit19 = New DevExpress.XtraEditors.TextEdit()
        Me.GridLookUpEdit1 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridLookUpEdit2 = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ImageComboBoxEdit5 = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.TextEdit20 = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup19 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup20 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem67 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem68 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem69 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem70 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem71 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem72 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem73 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem74 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem75 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem76 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem77 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem78 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem79 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.CtrContainerControl1 = New Presentation.Controls.CtrContainerControl()
        Me.AutoHideContainer1 = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.ControlContainer1 = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.GridControl5 = New DevExpress.XtraGrid.GridControl()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GdcStays, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GdvStays, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepEstShowDetails, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycHeader, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LycHeader.SuspendLayout
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.viewSearchAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PpmActions, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BteCountFolio.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAdmissionDate1.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SleBillingAuthorization.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GdvBillingAuthorization, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SleOperatingUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GdvOperatingUnit, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAdmissionType1.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPlaceEntry1.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgHeader, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgGeneralGroup, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LciStatusAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem32, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem33, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem34, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PccAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PccAdmission.SuspendLayout
        CType(Me.LycPccAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LycPccAdmission.SuspendLayout
        CType(Me.TxtContact.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientEstrato.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientEntityName.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtCareGroupPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientAge.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientBirth.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientName.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAfiliationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPatientCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtRiskType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtCareGroupAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtBedStay.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtEntityNameAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAtentionCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAuthorization.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtPlaceEntry.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtFunctionalUnitAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GdcStaysLiquidated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GdvStaysLiquidated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepEstLiqShowFolios, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PccStayFolios, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PccStayFolios.SuspendLayout
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl2.SuspendLayout
        CType(Me.GdcStayFolios, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GdvStayFolios, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem38, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepEstLiqShowDetails, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgPccAdmission, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LiCareGroup, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LiEntity, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TxtContacto, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgAdmissionGroup, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgStays, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TabbedControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgStaysDontLiquidated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem36, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LycgStaysLiquidated, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem35, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PnlBodySearch, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PnlBodySearch.SuspendLayout
        CType(Me.LayoutControl9, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl9.SuspendLayout
        CType(Me.INDGCExportExcel, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup21, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem30, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PccStayDetails, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PccStayDetails.SuspendLayout
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl1.SuspendLayout
        CType(Me.GdcStayDetails, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GdvStayDetails, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem37, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PnlBodyDashboard.SuspendLayout
        Me.hideContainerBottom.SuspendLayout
        Me.PnlNotifications.SuspendLayout
        Me.DockPanel1_Container.SuspendLayout
        CType(Me.GdcNotifications, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GdvNotifications, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PnlCtrlHeader, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PnlCtrlHeader.SuspendLayout
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PanelControl2.SuspendLayout
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl3.SuspendLayout
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LyciBusyIndicator, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DocumentManager, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.WidgetView, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.StackGroup1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PanelControl3.SuspendLayout
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PopupContainerControl1.SuspendLayout
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl4.SuspendLayout
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PopupContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PopupContainerControl2.SuspendLayout
        CType(Me.LayoutControl5, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl5.SuspendLayout
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup10, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem28, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PopupContainerControl3, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PopupContainerControl3.SuspendLayout
        CType(Me.LayoutControl6, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl6.SuspendLayout
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit4.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit5.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit6.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit7.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ImageComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ImageComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ButtonEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridControl3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit8.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit9.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit10.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit11.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit12.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit13.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit14.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit15.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit16.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit17.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit18.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ImageComboBoxEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ImageComboBoxEdit4.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ButtonEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridControl4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup12, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TabbedControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup13, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem40, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem41, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem42, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem43, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem44, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem45, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem46, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem47, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem48, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem49, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup14, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem50, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem51, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem52, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem53, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem54, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem55, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem56, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem57, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem58, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem59, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem60, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem61, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem62, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem63, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup15, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TabbedControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup16, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem64, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup17, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem65, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem66, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PanelControl4.SuspendLayout
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).BeginInit
        Me.PanelControl5.SuspendLayout
        CType(Me.LayoutControl7, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup18, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControl8, System.ComponentModel.ISupportInitialize).BeginInit
        Me.LayoutControl8.SuspendLayout
        CType(Me.SearchLookUpEditExAdmission1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ButtonEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit19.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridLookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridLookUpEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ImageComboBoxEdit5.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.TextEdit20.Properties, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup19, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup20, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem67, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem68, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem69, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem70, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem71, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem72, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem73, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem74, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem75, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem76, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem77, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem78, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlItem79, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit
        Me.CtrContainerControl1.SuspendLayout
        Me.AutoHideContainer1.SuspendLayout
        Me.DockPanel1.SuspendLayout
        Me.ControlContainer1.SuspendLayout
        CType(Me.GridControl5, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'GdcStays
        '
        Me.GdcStays.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GdcStays, "GdcStays")
        Me.GdcStays.MainView = Me.GdvStays
        Me.GdcStays.MenuManager = Me.BarManager
        Me.GdcStays.Name = "GdcStays"
        Me.GdcStays.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepEstShowDetails})
        Me.GdcStays.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvStays})
        '
        'GdvStays
        '
        Me.GdvStays.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvStays.Appearance.GroupRow.Font = CType(resources.GetObject("GdvStays.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GdvStays.Appearance.GroupRow.Options.UseFont = True
        Me.GdvStays.Appearance.HeaderPanel.Font = CType(resources.GetObject("GdvStays.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GdvStays.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvStays.Appearance.Row.Font = CType(resources.GetObject("GdvStays.Appearance.Row.Font"), System.Drawing.Font)
        Me.GdvStays.Appearance.Row.Options.UseFont = True
        Me.GdvStays.Appearance.ViewCaption.Font = CType(resources.GetObject("GdvStays.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GdvStays.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvStays.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColEstBed, Me.ColEstInitDate, Me.ColEstEndDate, Me.ColEstType, Me.ColEstTotalTime, Me.ColEstTotalUnits, Me.ColEstShowDetails, Me.ColEstFunctionalUnit})
        Me.GdvStays.GridControl = Me.GdcStays
        Me.GdvStays.Name = "GdvStays"
        Me.GdvStays.OptionsDetail.EnableMasterViewMode = False
        Me.GdvStays.OptionsDetail.ShowDetailTabs = False
        Me.GdvStays.OptionsDetail.SmartDetailExpand = False
        Me.GdvStays.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvStays.OptionsView.EnableAppearanceOddRow = True
        Me.GdvStays.OptionsView.ShowGroupPanel = False
        Me.GdvStays.OptionsView.ShowIndicator = False
        '
        'ColEstBed
        '
        Me.ColEstBed.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstBed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstBed.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstBed, "ColEstBed")
        Me.ColEstBed.FieldName = "CHCAMASHO.NUMCAMHOS"
        Me.ColEstBed.Name = "ColEstBed"
        Me.ColEstBed.OptionsColumn.AllowEdit = False
        Me.ColEstBed.OptionsColumn.AllowFocus = False
        Me.ColEstBed.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstBed.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstBed.OptionsColumn.AllowMove = False
        Me.ColEstBed.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstBed.OptionsColumn.ReadOnly = True
        Me.ColEstBed.OptionsFilter.AllowAutoFilter = False
        Me.ColEstBed.OptionsFilter.AllowFilter = False
        '
        'ColEstInitDate
        '
        Me.ColEstInitDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstInitDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstInitDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstInitDate, "ColEstInitDate")
        Me.ColEstInitDate.FieldName = "FECINIEST"
        Me.ColEstInitDate.Name = "ColEstInitDate"
        Me.ColEstInitDate.OptionsColumn.AllowEdit = False
        Me.ColEstInitDate.OptionsColumn.AllowFocus = False
        Me.ColEstInitDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstInitDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstInitDate.OptionsColumn.AllowMove = False
        Me.ColEstInitDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstInitDate.OptionsColumn.ReadOnly = True
        Me.ColEstInitDate.OptionsFilter.AllowAutoFilter = False
        Me.ColEstInitDate.OptionsFilter.AllowFilter = False
        '
        'ColEstEndDate
        '
        Me.ColEstEndDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstEndDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstEndDate, "ColEstEndDate")
        Me.ColEstEndDate.FieldName = "FECFINEST"
        Me.ColEstEndDate.Name = "ColEstEndDate"
        Me.ColEstEndDate.OptionsColumn.AllowEdit = False
        Me.ColEstEndDate.OptionsColumn.AllowFocus = False
        Me.ColEstEndDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstEndDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstEndDate.OptionsColumn.AllowMove = False
        Me.ColEstEndDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstEndDate.OptionsColumn.ReadOnly = True
        Me.ColEstEndDate.OptionsFilter.AllowAutoFilter = False
        Me.ColEstEndDate.OptionsFilter.AllowFilter = False
        '
        'ColEstType
        '
        Me.ColEstType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstType, "ColEstType")
        Me.ColEstType.FieldName = "CHTIPESTA.DESTIPEST"
        Me.ColEstType.Name = "ColEstType"
        Me.ColEstType.OptionsColumn.AllowEdit = False
        Me.ColEstType.OptionsColumn.AllowFocus = False
        Me.ColEstType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstType.OptionsColumn.AllowMove = False
        Me.ColEstType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstType.OptionsColumn.ReadOnly = True
        Me.ColEstType.OptionsFilter.AllowAutoFilter = False
        Me.ColEstType.OptionsFilter.AllowFilter = False
        '
        'ColEstTotalTime
        '
        Me.ColEstTotalTime.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstTotalTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstTotalTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstTotalTime, "ColEstTotalTime")
        Me.ColEstTotalTime.FieldName = "TotalTime"
        Me.ColEstTotalTime.Name = "ColEstTotalTime"
        Me.ColEstTotalTime.OptionsColumn.AllowEdit = False
        Me.ColEstTotalTime.OptionsColumn.AllowFocus = False
        Me.ColEstTotalTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstTotalTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstTotalTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstTotalTime.OptionsColumn.ReadOnly = True
        Me.ColEstTotalTime.OptionsFilter.AllowAutoFilter = False
        Me.ColEstTotalTime.OptionsFilter.AllowFilter = False
        '
        'ColEstTotalUnits
        '
        Me.ColEstTotalUnits.AppearanceCell.Options.UseTextOptions = True
        Me.ColEstTotalUnits.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstTotalUnits.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColEstTotalUnits.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstTotalUnits.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstTotalUnits.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstTotalUnits, "ColEstTotalUnits")
        Me.ColEstTotalUnits.FieldName = "TotalUnits"
        Me.ColEstTotalUnits.Name = "ColEstTotalUnits"
        Me.ColEstTotalUnits.OptionsColumn.AllowEdit = False
        Me.ColEstTotalUnits.OptionsColumn.AllowFocus = False
        Me.ColEstTotalUnits.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstTotalUnits.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstTotalUnits.OptionsColumn.AllowMove = False
        Me.ColEstTotalUnits.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstTotalUnits.OptionsColumn.ReadOnly = True
        Me.ColEstTotalUnits.OptionsFilter.AllowAutoFilter = False
        Me.ColEstTotalUnits.OptionsFilter.AllowFilter = False
        '
        'ColEstShowDetails
        '
        Me.ColEstShowDetails.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstShowDetails.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstShowDetails.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstShowDetails, "ColEstShowDetails")
        Me.ColEstShowDetails.ColumnEdit = Me.RepEstShowDetails
        Me.ColEstShowDetails.Name = "ColEstShowDetails"
        Me.ColEstShowDetails.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstShowDetails.OptionsColumn.FixedWidth = True
        Me.ColEstShowDetails.OptionsFilter.AllowAutoFilter = False
        Me.ColEstShowDetails.OptionsFilter.AllowFilter = False
        '
        'RepEstShowDetails
        '
        resources.ApplyResources(Me.RepEstShowDetails, "RepEstShowDetails")
        Me.RepEstShowDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepEstShowDetails.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepEstShowDetails.Name = "RepEstShowDetails"
        Me.RepEstShowDetails.PopupFormMinSize = New System.Drawing.Size(453, 181)
        Me.RepEstShowDetails.PopupFormSize = New System.Drawing.Size(453, 181)
        Me.RepEstShowDetails.ShowPopupCloseButton = False
        Me.RepEstShowDetails.ShowPopupShadow = False
        Me.RepEstShowDetails.Tag = "T"
        Me.RepEstShowDetails.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'ColEstFunctionalUnit
        '
        resources.ApplyResources(Me.ColEstFunctionalUnit, "ColEstFunctionalUnit")
        Me.ColEstFunctionalUnit.FieldName = "FunctionalUnitCodeName"
        Me.ColEstFunctionalUnit.Name = "ColEstFunctionalUnit"
        Me.ColEstFunctionalUnit.OptionsColumn.AllowEdit = False
        Me.ColEstFunctionalUnit.OptionsColumn.AllowFocus = False
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MbtnUndo, Me.MbtnFind, Me.MbtnRefresh, Me.MbtnLiquidateAll, Me.MbtnSmallPrintAll, Me.MbtnViewVoidInvoices, Me.MbtnPrintAll, Me.MbtnLargePrintAll, Me.MbtnLiquidateStays, Me.MbtnViewFolios, Me.MbtnView, Me.MbtnFlow, Me.MbtnNavigation, Me.MbtnConfirmAdmission, Me.MbtnOpenAdmission, Me.MbtnFacturarIngreso, Me.MBtnModifyAuthorization, Me.MBtnAnulateAllAmbulatory, Me.MBtnIngresosRelacionados, Me.MbtnIncomeLock, Me.MbtnIncomeUnlock, Me.MbtnLiquidateData, Me.Mbtn, Me.MbtnShowMotherAccountReport, Me.MbtnPrintDetailandParentAccount})
        Me.BarManager.MaxItemId = 32
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        Me.barDockControlTop.Manager = Me.BarManager
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        Me.barDockControlBottom.Manager = Me.BarManager
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        Me.barDockControlLeft.Manager = Me.BarManager
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        Me.barDockControlRight.Manager = Me.BarManager
        '
        'MbtnUndo
        '
        resources.ApplyResources(Me.MbtnUndo, "MbtnUndo")
        Me.MbtnUndo.Id = 0
        Me.MbtnUndo.ImageOptions.Image = CType(resources.GetObject("MbtnUndo.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnUndo.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnUndo.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnUndo.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnUndo.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnUndo.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnUndo.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnUndo.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnUndo.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnUndo.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnUndo.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnUndo.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnUndo.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnUndo.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnUndo.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnUndo.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnUndo.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnUndo.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnUndo.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnUndo.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnUndo.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnUndo.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnUndo.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnUndo.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnUndo.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnUndo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
        Me.MbtnUndo.Name = "MbtnUndo"
        '
        'MbtnFind
        '
        resources.ApplyResources(Me.MbtnFind, "MbtnFind")
        Me.MbtnFind.Id = 1
        Me.MbtnFind.ImageOptions.Image = CType(resources.GetObject("MbtnFind.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnFind.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnFind.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnFind.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnFind.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnFind.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnFind.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnFind.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnFind.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnFind.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnFind.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnFind.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnFind.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnFind.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnFind.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnFind.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnFind.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnFind.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnFind.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnFind.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnFind.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnFind.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnFind.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnFind.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnFind.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnFind.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B))
        Me.MbtnFind.Name = "MbtnFind"
        '
        'MbtnRefresh
        '
        resources.ApplyResources(Me.MbtnRefresh, "MbtnRefresh")
        Me.MbtnRefresh.Id = 2
        Me.MbtnRefresh.ImageOptions.Image = CType(resources.GetObject("MbtnRefresh.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnRefresh.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnRefresh.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnRefresh.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnRefresh.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnRefresh.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnRefresh.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnRefresh.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnRefresh.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnRefresh.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnRefresh.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnRefresh.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnRefresh.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnRefresh.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnRefresh.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnRefresh.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnRefresh.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnRefresh.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnRefresh.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnRefresh.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnRefresh.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnRefresh.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnRefresh.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnRefresh.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnRefresh.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
        Me.MbtnRefresh.Name = "MbtnRefresh"
        '
        'MbtnLiquidateAll
        '
        resources.ApplyResources(Me.MbtnLiquidateAll, "MbtnLiquidateAll")
        Me.MbtnLiquidateAll.Id = 3
        Me.MbtnLiquidateAll.ImageOptions.Image = CType(resources.GetObject("MbtnLiquidateAll.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnLiquidateAll.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnLiquidateAll.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnLiquidateAll.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnLiquidateAll.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnLiquidateAll.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnLiquidateAll.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnLiquidateAll.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnLiquidateAll.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnLiquidateAll.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnLiquidateAll.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnLiquidateAll.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnLiquidateAll.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnLiquidateAll.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnLiquidateAll.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnLiquidateAll.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnLiquidateAll.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnLiquidateAll.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnLiquidateAll.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnLiquidateAll.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnLiquidateAll.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnLiquidateAll.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnLiquidateAll.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnLiquidateAll.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnLiquidateAll.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnLiquidateAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
        Me.MbtnLiquidateAll.Name = "MbtnLiquidateAll"
        '
        'MbtnSmallPrintAll
        '
        resources.ApplyResources(Me.MbtnSmallPrintAll, "MbtnSmallPrintAll")
        Me.MbtnSmallPrintAll.Id = 4
        Me.MbtnSmallPrintAll.ImageOptions.Image = CType(resources.GetObject("MbtnSmallPrintAll.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnSmallPrintAll.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnSmallPrintAll.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnSmallPrintAll.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnSmallPrintAll.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnSmallPrintAll.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnSmallPrintAll.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnSmallPrintAll.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnSmallPrintAll.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnSmallPrintAll.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnSmallPrintAll.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnSmallPrintAll.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnSmallPrintAll.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnSmallPrintAll.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnSmallPrintAll.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnSmallPrintAll.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnSmallPrintAll.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnSmallPrintAll.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnSmallPrintAll.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnSmallPrintAll.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnSmallPrintAll.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnSmallPrintAll.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnSmallPrintAll.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnSmallPrintAll.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnSmallPrintAll.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnSmallPrintAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), (System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.MbtnSmallPrintAll.Name = "MbtnSmallPrintAll"
        '
        'MbtnViewVoidInvoices
        '
        resources.ApplyResources(Me.MbtnViewVoidInvoices, "MbtnViewVoidInvoices")
        Me.MbtnViewVoidInvoices.Id = 5
        Me.MbtnViewVoidInvoices.ImageOptions.Image = CType(resources.GetObject("MbtnViewVoidInvoices.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnViewVoidInvoices.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnViewVoidInvoices.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnViewVoidInvoices.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnViewVoidInvoices.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnViewVoidInvoices.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnViewVoidInvoices.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnViewVoidInvoices.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnViewVoidInvoices.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnViewVoidInvoices.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnViewVoidInvoices.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnViewVoidInvoices.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnViewVoidInvoices.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnViewVoidInvoices.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnViewVoidInvoices.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnViewVoidInvoices.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnViewVoidInvoices.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnViewVoidInvoices.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnViewVoidInvoices.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnViewVoidInvoices.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnViewVoidInvoices.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnViewVoidInvoices.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnViewVoidInvoices.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnViewVoidInvoices.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnViewVoidInvoices.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnViewVoidInvoices.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.MbtnViewVoidInvoices.Name = "MbtnViewVoidInvoices"
        '
        'MbtnPrintAll
        '
        resources.ApplyResources(Me.MbtnPrintAll, "MbtnPrintAll")
        Me.MbtnPrintAll.Id = 7
        Me.MbtnPrintAll.ImageOptions.Image = CType(resources.GetObject("MbtnPrintAll.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnPrintAll.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnPrintAll.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnPrintAll.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnPrintAll.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnPrintAll.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnPrintAll.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnPrintAll.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnPrintAll.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnPrintAll.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnPrintAll.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnPrintAll.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnPrintAll.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnPrintAll.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnPrintAll.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnPrintAll.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnPrintAll.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnPrintAll.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnPrintAll.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnPrintAll.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnPrintAll.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnPrintAll.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnPrintAll.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnPrintAll.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnPrintAll.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnPrintAll.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, Me.MbtnSmallPrintAll, "Imprimir Tirilla"), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnLargePrintAll), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnPrintDetailandParentAccount)})
        Me.MbtnPrintAll.Name = "MbtnPrintAll"
        '
        'MbtnLargePrintAll
        '
        resources.ApplyResources(Me.MbtnLargePrintAll, "MbtnLargePrintAll")
        Me.MbtnLargePrintAll.Id = 8
        Me.MbtnLargePrintAll.ImageOptions.Image = CType(resources.GetObject("MbtnLargePrintAll.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnLargePrintAll.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnLargePrintAll.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnLargePrintAll.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnLargePrintAll.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnLargePrintAll.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnLargePrintAll.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnLargePrintAll.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnLargePrintAll.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnLargePrintAll.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnLargePrintAll.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnLargePrintAll.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnLargePrintAll.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnLargePrintAll.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnLargePrintAll.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnLargePrintAll.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnLargePrintAll.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnLargePrintAll.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnLargePrintAll.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnLargePrintAll.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnLargePrintAll.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnLargePrintAll.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnLargePrintAll.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnLargePrintAll.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnLargePrintAll.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnLargePrintAll.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), (System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
        Me.MbtnLargePrintAll.Name = "MbtnLargePrintAll"
        '
        'MbtnPrintDetailandParentAccount
        '
        resources.ApplyResources(Me.MbtnPrintDetailandParentAccount, "MbtnPrintDetailandParentAccount")
        Me.MbtnPrintDetailandParentAccount.Id = 31
        Me.MbtnPrintDetailandParentAccount.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.Print_24x24_blue
        Me.MbtnPrintDetailandParentAccount.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnPrintDetailandParentAccount.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnPrintDetailandParentAccount.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnPrintDetailandParentAccount.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnPrintDetailandParentAccount.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnPrintDetailandParentAccount.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnPrintDetailandParentAccount.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnPrintDetailandParentAccount.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnPrintDetailandParentAccount.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnPrintDetailandParentAccount.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnPrintDetailandParentAccount.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnPrintDetailandParentAccount.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnPrintDetailandParentAccount.Name = "MbtnPrintDetailandParentAccount"
        '
        'MbtnLiquidateStays
        '
        resources.ApplyResources(Me.MbtnLiquidateStays, "MbtnLiquidateStays")
        Me.MbtnLiquidateStays.Id = 9
        Me.MbtnLiquidateStays.ImageOptions.Image = CType(resources.GetObject("MbtnLiquidateStays.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnLiquidateStays.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnLiquidateStays.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnLiquidateStays.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnLiquidateStays.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnLiquidateStays.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnLiquidateStays.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnLiquidateStays.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnLiquidateStays.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnLiquidateStays.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnLiquidateStays.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnLiquidateStays.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnLiquidateStays.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnLiquidateStays.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnLiquidateStays.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnLiquidateStays.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnLiquidateStays.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnLiquidateStays.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnLiquidateStays.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnLiquidateStays.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnLiquidateStays.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnLiquidateStays.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnLiquidateStays.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnLiquidateStays.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnLiquidateStays.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnLiquidateStays.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.MbtnLiquidateStays.Name = "MbtnLiquidateStays"
        '
        'MbtnViewFolios
        '
        resources.ApplyResources(Me.MbtnViewFolios, "MbtnViewFolios")
        Me.MbtnViewFolios.Id = 11
        Me.MbtnViewFolios.ImageOptions.Image = CType(resources.GetObject("MbtnViewFolios.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnViewFolios.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnViewFolios.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnViewFolios.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnViewFolios.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnViewFolios.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnViewFolios.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnViewFolios.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnViewFolios.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnViewFolios.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnViewFolios.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnViewFolios.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnViewFolios.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnViewFolios.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnViewFolios.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnViewFolios.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnViewFolios.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnViewFolios.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnViewFolios.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnViewFolios.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnViewFolios.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnViewFolios.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnViewFolios.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnViewFolios.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnViewFolios.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnViewFolios.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
        Me.MbtnViewFolios.Name = "MbtnViewFolios"
        '
        'MbtnView
        '
        resources.ApplyResources(Me.MbtnView, "MbtnView")
        Me.MbtnView.Id = 14
        Me.MbtnView.ImageOptions.Image = CType(resources.GetObject("MbtnView.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnView.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnView.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnView.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnView.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnView.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnView.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnView.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnView.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnView.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnView.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnView.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnView.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnView.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnView.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnView.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnView.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnView.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnView.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnView.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnView.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnView.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnView.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnView.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnView.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnView.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnFlow), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnNavigation)})
        Me.MbtnView.Name = "MbtnView"
        '
        'MbtnFlow
        '
        resources.ApplyResources(Me.MbtnFlow, "MbtnFlow")
        Me.MbtnFlow.Id = 15
        Me.MbtnFlow.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnFlow.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnFlow.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnFlow.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnFlow.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnFlow.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnFlow.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnFlow.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnFlow.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnFlow.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnFlow.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnFlow.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnFlow.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnFlow.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnFlow.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnFlow.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnFlow.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnFlow.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnFlow.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnFlow.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnFlow.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnFlow.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnFlow.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnFlow.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnFlow.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.F))
        Me.MbtnFlow.Name = "MbtnFlow"
        '
        'MbtnNavigation
        '
        resources.ApplyResources(Me.MbtnNavigation, "MbtnNavigation")
        Me.MbtnNavigation.Id = 16
        Me.MbtnNavigation.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnNavigation.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnNavigation.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnNavigation.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnNavigation.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnNavigation.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnNavigation.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnNavigation.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnNavigation.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnNavigation.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnNavigation.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnNavigation.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnNavigation.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnNavigation.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnNavigation.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnNavigation.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnNavigation.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnNavigation.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnNavigation.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnNavigation.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnNavigation.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnNavigation.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnNavigation.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnNavigation.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnNavigation.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.N))
        Me.MbtnNavigation.Name = "MbtnNavigation"
        '
        'MbtnConfirmAdmission
        '
        resources.ApplyResources(Me.MbtnConfirmAdmission, "MbtnConfirmAdmission")
        Me.MbtnConfirmAdmission.Id = 17
        Me.MbtnConfirmAdmission.ImageOptions.Image = CType(resources.GetObject("MbtnConfirmAdmission.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnConfirmAdmission.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnConfirmAdmission.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnConfirmAdmission.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnConfirmAdmission.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnConfirmAdmission.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnConfirmAdmission.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnConfirmAdmission.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnConfirmAdmission.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnConfirmAdmission.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnConfirmAdmission.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnConfirmAdmission.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnConfirmAdmission.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnConfirmAdmission.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.F))
        Me.MbtnConfirmAdmission.Name = "MbtnConfirmAdmission"
        '
        'MbtnOpenAdmission
        '
        resources.ApplyResources(Me.MbtnOpenAdmission, "MbtnOpenAdmission")
        Me.MbtnOpenAdmission.Id = 18
        Me.MbtnOpenAdmission.ImageOptions.Image = CType(resources.GetObject("MbtnOpenAdmission.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnOpenAdmission.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnOpenAdmission.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnOpenAdmission.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnOpenAdmission.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnOpenAdmission.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnOpenAdmission.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnOpenAdmission.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnOpenAdmission.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnOpenAdmission.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnOpenAdmission.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnOpenAdmission.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnOpenAdmission.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnOpenAdmission.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.A))
        Me.MbtnOpenAdmission.Name = "MbtnOpenAdmission"
        '
        'MbtnFacturarIngreso
        '
        resources.ApplyResources(Me.MbtnFacturarIngreso, "MbtnFacturarIngreso")
        Me.MbtnFacturarIngreso.Id = 19
        Me.MbtnFacturarIngreso.ImageOptions.Image = CType(resources.GetObject("MbtnFacturarIngreso.ImageOptions.Image"), System.Drawing.Image)
        Me.MbtnFacturarIngreso.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnFacturarIngreso.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnFacturarIngreso.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnFacturarIngreso.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnFacturarIngreso.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnFacturarIngreso.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnFacturarIngreso.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnFacturarIngreso.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnFacturarIngreso.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnFacturarIngreso.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnFacturarIngreso.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnFacturarIngreso.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnFacturarIngreso.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnFacturarIngreso.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnFacturarIngreso.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnFacturarIngreso.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnFacturarIngreso.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnFacturarIngreso.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnFacturarIngreso.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnFacturarIngreso.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnFacturarIngreso.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnFacturarIngreso.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnFacturarIngreso.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnFacturarIngreso.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnFacturarIngreso.Name = "MbtnFacturarIngreso"
        '
        'MBtnModifyAuthorization
        '
        resources.ApplyResources(Me.MBtnModifyAuthorization, "MBtnModifyAuthorization")
        Me.MBtnModifyAuthorization.Id = 20
        Me.MBtnModifyAuthorization.ImageOptions.Image = CType(resources.GetObject("MBtnModifyAuthorization.ImageOptions.Image"), System.Drawing.Image)
        Me.MBtnModifyAuthorization.ItemAppearance.Disabled.Font = CType(resources.GetObject("MBtnModifyAuthorization.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MBtnModifyAuthorization.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnModifyAuthorization.ItemAppearance.Hovered.Font = CType(resources.GetObject("MBtnModifyAuthorization.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MBtnModifyAuthorization.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnModifyAuthorization.ItemAppearance.Normal.Font = CType(resources.GetObject("MBtnModifyAuthorization.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MBtnModifyAuthorization.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnModifyAuthorization.ItemAppearance.Pressed.Font = CType(resources.GetObject("MBtnModifyAuthorization.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MBtnModifyAuthorization.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnModifyAuthorization.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MBtnModifyAuthorization.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MBtnModifyAuthorization.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnModifyAuthorization.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MBtnModifyAuthorization.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MBtnModifyAuthorization.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnModifyAuthorization.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MBtnModifyAuthorization.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MBtnModifyAuthorization.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnModifyAuthorization.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MBtnModifyAuthorization.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MBtnModifyAuthorization.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnModifyAuthorization.Name = "MBtnModifyAuthorization"
        '
        'MBtnAnulateAllAmbulatory
        '
        resources.ApplyResources(Me.MBtnAnulateAllAmbulatory, "MBtnAnulateAllAmbulatory")
        Me.MBtnAnulateAllAmbulatory.Id = 21
        Me.MBtnAnulateAllAmbulatory.ImageOptions.Image = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ImageOptions.Image"), System.Drawing.Image)
        Me.MBtnAnulateAllAmbulatory.ItemAppearance.Disabled.Font = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MBtnAnulateAllAmbulatory.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnAnulateAllAmbulatory.ItemAppearance.Hovered.Font = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MBtnAnulateAllAmbulatory.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnAnulateAllAmbulatory.ItemAppearance.Normal.Font = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MBtnAnulateAllAmbulatory.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnAnulateAllAmbulatory.ItemAppearance.Pressed.Font = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MBtnAnulateAllAmbulatory.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MBtnAnulateAllAmbulatory.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnAnulateAllAmbulatory.Name = "MBtnAnulateAllAmbulatory"
        '
        'MBtnIngresosRelacionados
        '
        resources.ApplyResources(Me.MBtnIngresosRelacionados, "MBtnIngresosRelacionados")
        Me.MBtnIngresosRelacionados.Id = 22
        Me.MBtnIngresosRelacionados.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.MoreInfo_24x24_blue
        Me.MBtnIngresosRelacionados.ItemAppearance.Disabled.Font = CType(resources.GetObject("MBtnIngresosRelacionados.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MBtnIngresosRelacionados.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnIngresosRelacionados.ItemAppearance.Hovered.Font = CType(resources.GetObject("MBtnIngresosRelacionados.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MBtnIngresosRelacionados.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnIngresosRelacionados.ItemAppearance.Normal.Font = CType(resources.GetObject("MBtnIngresosRelacionados.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MBtnIngresosRelacionados.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnIngresosRelacionados.ItemAppearance.Pressed.Font = CType(resources.GetObject("MBtnIngresosRelacionados.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MBtnIngresosRelacionados.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnIngresosRelacionados.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MBtnIngresosRelacionados.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MBtnIngresosRelacionados.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnIngresosRelacionados.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MBtnIngresosRelacionados.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MBtnIngresosRelacionados.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnIngresosRelacionados.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MBtnIngresosRelacionados.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MBtnIngresosRelacionados.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnIngresosRelacionados.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MBtnIngresosRelacionados.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MBtnIngresosRelacionados.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnIngresosRelacionados.Name = "MBtnIngresosRelacionados"
        '
        'MbtnIncomeLock
        '
        resources.ApplyResources(Me.MbtnIncomeLock, "MbtnIncomeLock")
        Me.MbtnIncomeLock.Id = 25
        Me.MbtnIncomeLock.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.BlockFolio_24x24_Blue
        Me.MbtnIncomeLock.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnIncomeLock.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnIncomeLock.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnIncomeLock.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnIncomeLock.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnIncomeLock.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnIncomeLock.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnIncomeLock.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnIncomeLock.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnIncomeLock.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnIncomeLock.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnIncomeLock.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnIncomeLock.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnIncomeLock.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnIncomeLock.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnIncomeLock.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnIncomeLock.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnIncomeLock.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnIncomeLock.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnIncomeLock.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnIncomeLock.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnIncomeLock.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnIncomeLock.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnIncomeLock.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnIncomeLock.Name = "MbtnIncomeLock"
        '
        'MbtnIncomeUnlock
        '
        resources.ApplyResources(Me.MbtnIncomeUnlock, "MbtnIncomeUnlock")
        Me.MbtnIncomeUnlock.Id = 26
        Me.MbtnIncomeUnlock.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.UnblockFolio_24x24_Blue
        Me.MbtnIncomeUnlock.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnIncomeUnlock.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnIncomeUnlock.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnIncomeUnlock.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnIncomeUnlock.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnIncomeUnlock.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnIncomeUnlock.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnIncomeUnlock.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnIncomeUnlock.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnIncomeUnlock.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnIncomeUnlock.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnIncomeUnlock.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnIncomeUnlock.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("MbtnIncomeUnlock.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnIncomeUnlock.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MbtnIncomeUnlock.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("MbtnIncomeUnlock.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnIncomeUnlock.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MbtnIncomeUnlock.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("MbtnIncomeUnlock.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnIncomeUnlock.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MbtnIncomeUnlock.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("MbtnIncomeUnlock.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnIncomeUnlock.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MbtnIncomeUnlock.Name = "MbtnIncomeUnlock"
        '
        'MbtnLiquidateData
        '
        resources.ApplyResources(Me.MbtnLiquidateData, "MbtnLiquidateData")
        Me.MbtnLiquidateData.Id = 28
        Me.MbtnLiquidateData.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnLiquidateData.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnLiquidateData.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnLiquidateData.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnLiquidateData.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnLiquidateData.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnLiquidateData.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnLiquidateData.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnLiquidateData.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnLiquidateData.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnLiquidateData.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnLiquidateData.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnLiquidateData.Name = "MbtnLiquidateData"
        '
        'Mbtn
        '
        resources.ApplyResources(Me.Mbtn, "Mbtn")
        Me.Mbtn.Id = 29
        Me.Mbtn.Name = "Mbtn"
        '
        'MbtnShowMotherAccountReport
        '
        resources.ApplyResources(Me.MbtnShowMotherAccountReport, "MbtnShowMotherAccountReport")
        Me.MbtnShowMotherAccountReport.Id = 30
        Me.MbtnShowMotherAccountReport.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.Acceptance
        Me.MbtnShowMotherAccountReport.ItemAppearance.Disabled.Font = CType(resources.GetObject("MbtnShowMotherAccountReport.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.MbtnShowMotherAccountReport.ItemAppearance.Disabled.Options.UseFont = True
        Me.MbtnShowMotherAccountReport.ItemAppearance.Hovered.Font = CType(resources.GetObject("MbtnShowMotherAccountReport.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.MbtnShowMotherAccountReport.ItemAppearance.Hovered.Options.UseFont = True
        Me.MbtnShowMotherAccountReport.ItemAppearance.Normal.Font = CType(resources.GetObject("MbtnShowMotherAccountReport.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.MbtnShowMotherAccountReport.ItemAppearance.Normal.Options.UseFont = True
        Me.MbtnShowMotherAccountReport.ItemAppearance.Pressed.Font = CType(resources.GetObject("MbtnShowMotherAccountReport.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.MbtnShowMotherAccountReport.ItemAppearance.Pressed.Options.UseFont = True
        Me.MbtnShowMotherAccountReport.Name = "MbtnShowMotherAccountReport"
        '
        'LycHeader
        '
        Me.LycHeader.AllowCustomization = False
        Me.LycHeader.Controls.Add(Me.INDSleAdmissionNumber2)
        Me.LycHeader.Controls.Add(Me.TxtStatusAdmission)
        Me.LycHeader.Controls.Add(Me.LabelControl4)
        Me.LycHeader.Controls.Add(Me.DdbMenu)
        Me.LycHeader.Controls.Add(Me.BteCountFolio)
        Me.LycHeader.Controls.Add(Me.LblTotalPatient)
        Me.LycHeader.Controls.Add(Me.LblTotalEntity)
        Me.LycHeader.Controls.Add(Me.TxtAdmissionDate1)
        Me.LycHeader.Controls.Add(Me.SleBillingAuthorization)
        Me.LycHeader.Controls.Add(Me.SleOperatingUnit)
        Me.LycHeader.Controls.Add(Me.TxtAdmissionType1)
        Me.LycHeader.Controls.Add(Me.TxtPlaceEntry1)
        resources.ApplyResources(Me.LycHeader, "LycHeader")
        Me.LycHeader.Name = "LycHeader"
        Me.LycHeader.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(-1237, 204, 800, 450)
        Me.LycHeader.Root = Me.LycgHeader
        '
        'INDSleAdmissionNumber2
        '
        Me.INDSleAdmissionNumber2.AllowQueryOne = True
        Me.INDSleAdmissionNumber2.Datasource = Nothing
        Me.INDSleAdmissionNumber2.DisplayMember = "{FullNameAdmission}"
        Me.INDSleAdmissionNumber2.DisplayNullText = ""
        Me.INDSleAdmissionNumber2.EditValue = Nothing
        Me.INDSleAdmissionNumber2.EnterMoveNextControl = True
        Me.INDSleAdmissionNumber2.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleAdmissionNumber2.IdOpenForm = 0
        Me.INDSleAdmissionNumber2.IsReadOnly = False
        resources.ApplyResources(Me.INDSleAdmissionNumber2, "INDSleAdmissionNumber2")
        Me.INDSleAdmissionNumber2.Name = "INDSleAdmissionNumber2"
        Me.INDSleAdmissionNumber2.PopupContainerControl = Nothing
        Me.INDSleAdmissionNumber2.PopUpFormSize = New System.Drawing.Size(1000, 300)
        Me.INDSleAdmissionNumber2.ValueMember = "AdmissionCode"
        Me.INDSleAdmissionNumber2.View = Me.viewSearchAdmission
        '
        'viewSearchAdmission
        '
        Me.viewSearchAdmission.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewSearchAdmission.Appearance.FocusedRow.Font = CType(resources.GetObject("viewSearchAdmission.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.viewSearchAdmission.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewSearchAdmission.Appearance.FocusedRow.Options.UseFont = True
        Me.viewSearchAdmission.Appearance.GroupRow.Font = CType(resources.GetObject("viewSearchAdmission.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.viewSearchAdmission.Appearance.GroupRow.Options.UseFont = True
        Me.viewSearchAdmission.Appearance.HeaderPanel.Font = CType(resources.GetObject("viewSearchAdmission.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.viewSearchAdmission.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewSearchAdmission.Appearance.Row.Font = CType(resources.GetObject("viewSearchAdmission.Appearance.Row.Font"), System.Drawing.Font)
        Me.viewSearchAdmission.Appearance.Row.Options.UseFont = True
        Me.viewSearchAdmission.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.viewSearchAdmission.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn19, Me.GridColumn20, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23, Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27})
        Me.viewSearchAdmission.Name = "viewSearchAdmission"
        Me.viewSearchAdmission.OptionsView.EnableAppearanceEvenRow = True
        Me.viewSearchAdmission.OptionsView.EnableAppearanceOddRow = True
        Me.viewSearchAdmission.OptionsView.ShowAutoFilterRow = True
        Me.viewSearchAdmission.OptionsView.ShowGroupPanel = False
        '
        'GridColumn19
        '
        resources.ApplyResources(Me.GridColumn19, "GridColumn19")
        Me.GridColumn19.FieldName = "AdmissionCode"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.AllowEdit = False
        Me.GridColumn19.OptionsColumn.AllowFocus = False
        '
        'GridColumn20
        '
        resources.ApplyResources(Me.GridColumn20, "GridColumn20")
        Me.GridColumn20.FieldName = "PatientCode"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.OptionsColumn.AllowEdit = False
        Me.GridColumn20.OptionsColumn.AllowFocus = False
        '
        'GridColumn21
        '
        resources.ApplyResources(Me.GridColumn21, "GridColumn21")
        Me.GridColumn21.FieldName = "PatientName"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.OptionsColumn.AllowFocus = False
        '
        'GridColumn22
        '
        resources.ApplyResources(Me.GridColumn22, "GridColumn22")
        Me.GridColumn22.FieldName = "AdmissionTypeName"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.OptionsColumn.AllowFocus = False
        '
        'GridColumn23
        '
        resources.ApplyResources(Me.GridColumn23, "GridColumn23")
        Me.GridColumn23.DisplayFormat.FormatString = "MM/dd/yyyy HH:mm:ss"
        Me.GridColumn23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn23.FieldName = "AdmissionDate"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.OptionsColumn.AllowFocus = False
        '
        'GridColumn24
        '
        resources.ApplyResources(Me.GridColumn24, "GridColumn24")
        Me.GridColumn24.FieldName = "BedStay"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsColumn.AllowFocus = False
        '
        'GridColumn25
        '
        resources.ApplyResources(Me.GridColumn25, "GridColumn25")
        Me.GridColumn25.FieldName = "LiquidationTypeNameInGrid"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.OptionsColumn.AllowFocus = False
        '
        'GridColumn26
        '
        resources.ApplyResources(Me.GridColumn26, "GridColumn26")
        Me.GridColumn26.FieldName = "FunctionalUnitCodeName"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.OptionsColumn.AllowEdit = False
        Me.GridColumn26.OptionsColumn.AllowFocus = False
        '
        'GridColumn27
        '
        resources.ApplyResources(Me.GridColumn27, "GridColumn27")
        Me.GridColumn27.FieldName = "StatusName"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.OptionsColumn.AllowEdit = False
        Me.GridColumn27.OptionsColumn.AllowFocus = False
        '
        'TxtStatusAdmission
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.TxtStatusAdmission, True)
        Me.TxtStatusAdmission.Appearance.Font = CType(resources.GetObject("TxtStatusAdmission.Appearance.Font"), System.Drawing.Font)
        Me.TxtStatusAdmission.Appearance.ForeColor = System.Drawing.Color.White
        Me.TxtStatusAdmission.Appearance.Options.UseFont = True
        Me.TxtStatusAdmission.Appearance.Options.UseForeColor = True
        Me.TxtStatusAdmission.Appearance.Options.UseTextOptions = True
        Me.TxtStatusAdmission.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.TxtStatusAdmission, "TxtStatusAdmission")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.TxtStatusAdmission, False)
        Me.TxtStatusAdmission.Name = "TxtStatusAdmission"
        Me.TxtStatusAdmission.StyleController = Me.LycHeader
        '
        'LabelControl4
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl4, True)
        Me.LabelControl4.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl4.Appearance.Font = CType(resources.GetObject("LabelControl4.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl4.Appearance.Options.UseBackColor = True
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.AutoEllipsis = True
        resources.ApplyResources(Me.LabelControl4, "LabelControl4")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl4, False)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.StyleController = Me.LycHeader
        '
        'DdbMenu
        '
        Me.DdbMenu.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.DdbMenu.Appearance.Options.UseBackColor = True
        Me.DdbMenu.Cursor = System.Windows.Forms.Cursors.Default
        Me.DdbMenu.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.DdbMenu.DropDownControl = Me.PpmActions
        Me.DdbMenu.ImageOptions.Image = Global.Presentation.Billing.My.Resources.Resources.Mmenu_de_acciones
        Me.DdbMenu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.DdbMenu, "DdbMenu")
        Me.DdbMenu.Name = "DdbMenu"
        Me.DdbMenu.StyleController = Me.LycHeader
        '
        'PpmActions
        '
        Me.PpmActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.Caption Or DevExpress.XtraBars.BarLinkUserDefines.PaintStyle), DevExpress.XtraBars.BarLinkUserDefines), Me.MbtnFind, "Buscar", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.Standard), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnLiquidateData, True), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnShowMotherAccountReport), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnOpenAdmission), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnConfirmAdmission), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnFacturarIngreso), New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnIngresosRelacionados), New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.Caption Or DevExpress.XtraBars.BarLinkUserDefines.PaintStyle), DevExpress.XtraBars.BarLinkUserDefines), Me.MbtnUndo, "Deshacer", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnRefresh), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnLiquidateAll, True), New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnAnulateAllAmbulatory), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnLiquidateStays), New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnModifyAuthorization), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnViewFolios, True), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnViewVoidInvoices), New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.Caption Or DevExpress.XtraBars.BarLinkUserDefines.PaintStyle), DevExpress.XtraBars.BarLinkUserDefines), Me.MbtnPrintAll, "Imprimir Todo", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.Standard), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnView, True), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnIncomeLock, True), New DevExpress.XtraBars.LinkPersistInfo(Me.MbtnIncomeUnlock)})
        Me.PpmActions.Manager = Me.BarManager
        Me.PpmActions.Name = "PpmActions"
        '
        'BteCountFolio
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.BteCountFolio, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.BteCountFolio, False)
        resources.ApplyResources(Me.BteCountFolio, "BteCountFolio")
        Me.IndigoTextEdit1.SetMascara(Me.BteCountFolio, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.BteCountFolio.Name = "BteCountFolio"
        Me.BteCountFolio.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.BteCountFolio.Properties.Appearance.Font = CType(resources.GetObject("BteCountFolio.Properties.Appearance.Font"), System.Drawing.Font)
        Me.BteCountFolio.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.BteCountFolio.Properties.Appearance.Options.UseBackColor = True
        Me.BteCountFolio.Properties.Appearance.Options.UseFont = True
        Me.BteCountFolio.Properties.Appearance.Options.UseForeColor = True
        Me.BteCountFolio.Properties.Appearance.Options.UseTextOptions = True
        Me.BteCountFolio.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BteCountFolio.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.BteCountFolio.Properties.AppearanceFocused.Font = CType(resources.GetObject("BteCountFolio.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.BteCountFolio.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.BteCountFolio.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.BteCountFolio.Properties.AppearanceFocused.Options.UseFont = True
        Me.BteCountFolio.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.BteCountFolio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("BteCountFolio.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("BteCountFolio.Properties.Buttons1"), CType(resources.GetObject("BteCountFolio.Properties.Buttons2"), Integer), CType(resources.GetObject("BteCountFolio.Properties.Buttons3"), Boolean), CType(resources.GetObject("BteCountFolio.Properties.Buttons4"), Boolean), CType(resources.GetObject("BteCountFolio.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("BteCountFolio.Properties.Buttons6"), CType(resources.GetObject("BteCountFolio.Properties.Buttons7"), Object), CType(resources.GetObject("BteCountFolio.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("BteCountFolio.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.BteCountFolio.Properties.ReadOnly = True
        Me.BteCountFolio.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.BteCountFolio.StyleController = Me.LycHeader
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.BteCountFolio, 0)
        '
        'LblTotalPatient
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LblTotalPatient, True)
        Me.LblTotalPatient.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LblTotalPatient.Appearance.Font = CType(resources.GetObject("LblTotalPatient.Appearance.Font"), System.Drawing.Font)
        Me.LblTotalPatient.Appearance.Options.UseBackColor = True
        Me.LblTotalPatient.Appearance.Options.UseFont = True
        Me.LblTotalPatient.Appearance.Options.UseTextOptions = True
        Me.LblTotalPatient.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblTotalPatient.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LblTotalPatient, "LblTotalPatient")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LblTotalPatient, False)
        Me.LblTotalPatient.Name = "LblTotalPatient"
        Me.LblTotalPatient.StyleController = Me.LycHeader
        '
        'LblTotalEntity
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LblTotalEntity, True)
        Me.LblTotalEntity.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LblTotalEntity.Appearance.Font = CType(resources.GetObject("LblTotalEntity.Appearance.Font"), System.Drawing.Font)
        Me.LblTotalEntity.Appearance.Options.UseBackColor = True
        Me.LblTotalEntity.Appearance.Options.UseFont = True
        Me.LblTotalEntity.Appearance.Options.UseTextOptions = True
        Me.LblTotalEntity.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LblTotalEntity.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LblTotalEntity, "LblTotalEntity")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LblTotalEntity, False)
        Me.LblTotalEntity.Name = "LblTotalEntity"
        Me.LblTotalEntity.StyleController = Me.LycHeader
        '
        'TxtAdmissionDate1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionDate1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionDate1, False)
        resources.ApplyResources(Me.TxtAdmissionDate1, "TxtAdmissionDate1")
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionDate1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAdmissionDate1.Name = "TxtAdmissionDate1"
        Me.TxtAdmissionDate1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.TxtAdmissionDate1.Properties.Appearance.Font = CType(resources.GetObject("TxtAdmissionDate1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAdmissionDate1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.TxtAdmissionDate1.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionDate1.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionDate1.Properties.Appearance.Options.UseForeColor = True
        Me.TxtAdmissionDate1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.TxtAdmissionDate1.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtAdmissionDate1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtAdmissionDate1.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.TxtAdmissionDate1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionDate1.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionDate1.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.TxtAdmissionDate1.Properties.ReadOnly = True
        Me.TxtAdmissionDate1.StyleController = Me.LycHeader
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionDate1, 0)
        '
        'SleBillingAuthorization
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.SleBillingAuthorization, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.SleBillingAuthorization, False)
        Me.SleBillingAuthorization.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.SleBillingAuthorization, "SleBillingAuthorization")
        Me.IndigoTextEdit1.SetMascara(Me.SleBillingAuthorization, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.SleBillingAuthorization.MenuManager = Me.BarManager
        Me.SleBillingAuthorization.Name = "SleBillingAuthorization"
        Me.SleBillingAuthorization.Properties.AllowFocused = False
        Me.SleBillingAuthorization.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SleBillingAuthorization.Properties.Appearance.Font = CType(resources.GetObject("SleBillingAuthorization.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SleBillingAuthorization.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.SleBillingAuthorization.Properties.Appearance.Options.UseBackColor = True
        Me.SleBillingAuthorization.Properties.Appearance.Options.UseFont = True
        Me.SleBillingAuthorization.Properties.Appearance.Options.UseForeColor = True
        Me.SleBillingAuthorization.Properties.Appearance.Options.UseTextOptions = True
        Me.SleBillingAuthorization.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleBillingAuthorization.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleBillingAuthorization.Properties.AppearanceDisabled.Font = CType(resources.GetObject("SleBillingAuthorization.Properties.AppearanceDisabled.Font"), System.Drawing.Font)
        Me.SleBillingAuthorization.Properties.AppearanceDisabled.Options.UseFont = True
        Me.SleBillingAuthorization.Properties.AppearanceDisabled.Options.UseTextOptions = True
        Me.SleBillingAuthorization.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleBillingAuthorization.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleBillingAuthorization.Properties.AppearanceDropDown.Font = CType(resources.GetObject("SleBillingAuthorization.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.SleBillingAuthorization.Properties.AppearanceDropDown.Options.UseFont = True
        Me.SleBillingAuthorization.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.SleBillingAuthorization.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleBillingAuthorization.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleBillingAuthorization.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SleBillingAuthorization.Properties.AppearanceFocused.Font = CType(resources.GetObject("SleBillingAuthorization.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.SleBillingAuthorization.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.SleBillingAuthorization.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.SleBillingAuthorization.Properties.AppearanceFocused.Options.UseFont = True
        Me.SleBillingAuthorization.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.SleBillingAuthorization.Properties.AppearanceFocused.Options.UseTextOptions = True
        Me.SleBillingAuthorization.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleBillingAuthorization.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleBillingAuthorization.Properties.AppearanceReadOnly.Font = CType(resources.GetObject("SleBillingAuthorization.Properties.AppearanceReadOnly.Font"), System.Drawing.Font)
        Me.SleBillingAuthorization.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.SleBillingAuthorization.Properties.AppearanceReadOnly.Options.UseTextOptions = True
        Me.SleBillingAuthorization.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleBillingAuthorization.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleBillingAuthorization.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SleBillingAuthorization.Properties.DisplayMember = "Name"
        Me.SleBillingAuthorization.Properties.NullText = resources.GetString("SleBillingAuthorization.Properties.NullText")
        Me.SleBillingAuthorization.Properties.PopupFormMinSize = New System.Drawing.Size(230, 120)
        Me.SleBillingAuthorization.Properties.PopupFormSize = New System.Drawing.Size(230, 120)
        Me.SleBillingAuthorization.Properties.PopupSizeable = False
        Me.SleBillingAuthorization.Properties.PopupView = Me.GdvBillingAuthorization
        Me.SleBillingAuthorization.Properties.ShowFooter = False
        Me.SleBillingAuthorization.Properties.ShowPopupShadow = False
        Me.SleBillingAuthorization.Properties.ValueMember = "Id"
        Me.SleBillingAuthorization.StyleController = Me.LycHeader
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.SleBillingAuthorization, 0)
        '
        'GdvBillingAuthorization
        '
        Me.GdvBillingAuthorization.Appearance.GroupRow.Font = CType(resources.GetObject("GdvBillingAuthorization.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GdvBillingAuthorization.Appearance.GroupRow.Options.UseFont = True
        Me.GdvBillingAuthorization.Appearance.HeaderPanel.Font = CType(resources.GetObject("GdvBillingAuthorization.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GdvBillingAuthorization.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvBillingAuthorization.Appearance.Row.Font = CType(resources.GetObject("GdvBillingAuthorization.Appearance.Row.Font"), System.Drawing.Font)
        Me.GdvBillingAuthorization.Appearance.Row.Options.UseFont = True
        Me.GdvBillingAuthorization.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColBACode, Me.ColBAName})
        Me.GdvBillingAuthorization.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GdvBillingAuthorization.Name = "GdvBillingAuthorization"
        Me.GdvBillingAuthorization.OptionsMenu.EnableColumnMenu = False
        Me.GdvBillingAuthorization.OptionsMenu.EnableFooterMenu = False
        Me.GdvBillingAuthorization.OptionsMenu.EnableGroupPanelMenu = False
        Me.GdvBillingAuthorization.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
        Me.GdvBillingAuthorization.OptionsMenu.ShowAutoFilterRowItem = False
        Me.GdvBillingAuthorization.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GdvBillingAuthorization.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GdvBillingAuthorization.OptionsMenu.ShowSplitItem = False
        Me.GdvBillingAuthorization.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GdvBillingAuthorization.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvBillingAuthorization.OptionsView.EnableAppearanceOddRow = True
        Me.GdvBillingAuthorization.OptionsView.ShowGroupPanel = False
        '
        'ColBACode
        '
        Me.ColBACode.AppearanceCell.Font = CType(resources.GetObject("ColBACode.AppearanceCell.Font"), System.Drawing.Font)
        Me.ColBACode.AppearanceCell.Options.UseFont = True
        Me.ColBACode.AppearanceHeader.Font = CType(resources.GetObject("ColBACode.AppearanceHeader.Font"), System.Drawing.Font)
        Me.ColBACode.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.ColBACode, "ColBACode")
        Me.ColBACode.FieldName = "Code"
        Me.ColBACode.Name = "ColBACode"
        '
        'ColBAName
        '
        Me.ColBAName.AppearanceCell.Font = CType(resources.GetObject("ColBAName.AppearanceCell.Font"), System.Drawing.Font)
        Me.ColBAName.AppearanceCell.Options.UseFont = True
        Me.ColBAName.AppearanceHeader.Font = CType(resources.GetObject("ColBAName.AppearanceHeader.Font"), System.Drawing.Font)
        Me.ColBAName.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.ColBAName, "ColBAName")
        Me.ColBAName.FieldName = "Name"
        Me.ColBAName.Name = "ColBAName"
        '
        'SleOperatingUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.SleOperatingUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.SleOperatingUnit, False)
        Me.SleOperatingUnit.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.SleOperatingUnit, "SleOperatingUnit")
        Me.IndigoTextEdit1.SetMascara(Me.SleOperatingUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.SleOperatingUnit.MenuManager = Me.BarManager
        Me.SleOperatingUnit.Name = "SleOperatingUnit"
        Me.SleOperatingUnit.Properties.AllowFocused = False
        Me.SleOperatingUnit.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SleOperatingUnit.Properties.Appearance.Font = CType(resources.GetObject("SleOperatingUnit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.SleOperatingUnit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.SleOperatingUnit.Properties.Appearance.Options.UseBackColor = True
        Me.SleOperatingUnit.Properties.Appearance.Options.UseFont = True
        Me.SleOperatingUnit.Properties.Appearance.Options.UseForeColor = True
        Me.SleOperatingUnit.Properties.Appearance.Options.UseTextOptions = True
        Me.SleOperatingUnit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleOperatingUnit.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleOperatingUnit.Properties.AppearanceDisabled.Font = CType(resources.GetObject("SleOperatingUnit.Properties.AppearanceDisabled.Font"), System.Drawing.Font)
        Me.SleOperatingUnit.Properties.AppearanceDisabled.Options.UseFont = True
        Me.SleOperatingUnit.Properties.AppearanceDisabled.Options.UseTextOptions = True
        Me.SleOperatingUnit.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleOperatingUnit.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleOperatingUnit.Properties.AppearanceDropDown.Font = CType(resources.GetObject("SleOperatingUnit.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.SleOperatingUnit.Properties.AppearanceDropDown.Options.UseFont = True
        Me.SleOperatingUnit.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.SleOperatingUnit.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleOperatingUnit.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleOperatingUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SleOperatingUnit.Properties.AppearanceFocused.Font = CType(resources.GetObject("SleOperatingUnit.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.SleOperatingUnit.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.SleOperatingUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.SleOperatingUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.SleOperatingUnit.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.SleOperatingUnit.Properties.AppearanceFocused.Options.UseTextOptions = True
        Me.SleOperatingUnit.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleOperatingUnit.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleOperatingUnit.Properties.AppearanceReadOnly.Font = CType(resources.GetObject("SleOperatingUnit.Properties.AppearanceReadOnly.Font"), System.Drawing.Font)
        Me.SleOperatingUnit.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.SleOperatingUnit.Properties.AppearanceReadOnly.Options.UseTextOptions = True
        Me.SleOperatingUnit.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.SleOperatingUnit.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.SleOperatingUnit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SleOperatingUnit.Properties.DisplayMember = "UnitName"
        Me.SleOperatingUnit.Properties.NullText = resources.GetString("SleOperatingUnit.Properties.NullText")
        Me.SleOperatingUnit.Properties.PopupFormMinSize = New System.Drawing.Size(230, 120)
        Me.SleOperatingUnit.Properties.PopupFormSize = New System.Drawing.Size(230, 120)
        Me.SleOperatingUnit.Properties.PopupSizeable = False
        Me.SleOperatingUnit.Properties.PopupView = Me.GdvOperatingUnit
        Me.SleOperatingUnit.Properties.ShowFooter = False
        Me.SleOperatingUnit.Properties.ShowPopupShadow = False
        Me.SleOperatingUnit.Properties.ValueMember = "Id"
        Me.SleOperatingUnit.StyleController = Me.LycHeader
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.SleOperatingUnit, 0)
        '
        'GdvOperatingUnit
        '
        Me.GdvOperatingUnit.Appearance.GroupRow.Font = CType(resources.GetObject("GdvOperatingUnit.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GdvOperatingUnit.Appearance.GroupRow.Options.UseFont = True
        Me.GdvOperatingUnit.Appearance.HeaderPanel.Font = CType(resources.GetObject("GdvOperatingUnit.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GdvOperatingUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvOperatingUnit.Appearance.Row.Font = CType(resources.GetObject("GdvOperatingUnit.Appearance.Row.Font"), System.Drawing.Font)
        Me.GdvOperatingUnit.Appearance.Row.Options.UseFont = True
        Me.GdvOperatingUnit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColOPCode, Me.ColOPName})
        Me.GdvOperatingUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GdvOperatingUnit.Name = "GdvOperatingUnit"
        Me.GdvOperatingUnit.OptionsMenu.EnableColumnMenu = False
        Me.GdvOperatingUnit.OptionsMenu.EnableFooterMenu = False
        Me.GdvOperatingUnit.OptionsMenu.EnableGroupPanelMenu = False
        Me.GdvOperatingUnit.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
        Me.GdvOperatingUnit.OptionsMenu.ShowAutoFilterRowItem = False
        Me.GdvOperatingUnit.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GdvOperatingUnit.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GdvOperatingUnit.OptionsMenu.ShowSplitItem = False
        Me.GdvOperatingUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GdvOperatingUnit.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvOperatingUnit.OptionsView.EnableAppearanceOddRow = True
        Me.GdvOperatingUnit.OptionsView.ShowGroupPanel = False
        '
        'ColOPCode
        '
        Me.ColOPCode.AppearanceCell.Font = CType(resources.GetObject("ColOPCode.AppearanceCell.Font"), System.Drawing.Font)
        Me.ColOPCode.AppearanceCell.Options.UseFont = True
        Me.ColOPCode.AppearanceHeader.Font = CType(resources.GetObject("ColOPCode.AppearanceHeader.Font"), System.Drawing.Font)
        Me.ColOPCode.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.ColOPCode, "ColOPCode")
        Me.ColOPCode.FieldName = "UnitCode"
        Me.ColOPCode.Name = "ColOPCode"
        '
        'ColOPName
        '
        Me.ColOPName.AppearanceCell.Font = CType(resources.GetObject("ColOPName.AppearanceCell.Font"), System.Drawing.Font)
        Me.ColOPName.AppearanceCell.Options.UseFont = True
        Me.ColOPName.AppearanceHeader.Font = CType(resources.GetObject("ColOPName.AppearanceHeader.Font"), System.Drawing.Font)
        Me.ColOPName.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.ColOPName, "ColOPName")
        Me.ColOPName.FieldName = "UnitName"
        Me.ColOPName.Name = "ColOPName"
        '
        'TxtAdmissionType1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionType1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionType1, False)
        resources.ApplyResources(Me.TxtAdmissionType1, "TxtAdmissionType1")
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionType1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAdmissionType1.Name = "TxtAdmissionType1"
        Me.TxtAdmissionType1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.TxtAdmissionType1.Properties.Appearance.Font = CType(resources.GetObject("TxtAdmissionType1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAdmissionType1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.TxtAdmissionType1.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionType1.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionType1.Properties.Appearance.Options.UseForeColor = True
        Me.TxtAdmissionType1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.TxtAdmissionType1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAdmissionType1.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtAdmissionType1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtAdmissionType1.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.TxtAdmissionType1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionType1.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAdmissionType1.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionType1.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.TxtAdmissionType1.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAdmissionType1.Properties.Items"), CType(resources.GetObject("TxtAdmissionType1.Properties.Items1"), Object), CType(resources.GetObject("TxtAdmissionType1.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAdmissionType1.Properties.Items3"), CType(resources.GetObject("TxtAdmissionType1.Properties.Items4"), Object), CType(resources.GetObject("TxtAdmissionType1.Properties.Items5"), Integer))})
        Me.TxtAdmissionType1.Properties.ReadOnly = True
        Me.TxtAdmissionType1.StyleController = Me.LycHeader
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionType1, 0)
        '
        'TxtPlaceEntry1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPlaceEntry1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPlaceEntry1, False)
        resources.ApplyResources(Me.TxtPlaceEntry1, "TxtPlaceEntry1")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPlaceEntry1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPlaceEntry1.Name = "TxtPlaceEntry1"
        Me.TxtPlaceEntry1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.TxtPlaceEntry1.Properties.Appearance.Font = CType(resources.GetObject("TxtPlaceEntry1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPlaceEntry1.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.TxtPlaceEntry1.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPlaceEntry1.Properties.Appearance.Options.UseFont = True
        Me.TxtPlaceEntry1.Properties.Appearance.Options.UseForeColor = True
        Me.TxtPlaceEntry1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.TxtPlaceEntry1.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtPlaceEntry1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtPlaceEntry1.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.TxtPlaceEntry1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtPlaceEntry1.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPlaceEntry1.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.TxtPlaceEntry1.Properties.ReadOnly = True
        Me.TxtPlaceEntry1.StyleController = Me.LycHeader
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPlaceEntry1, 0)
        '
        'LycgHeader
        '
        Me.LycgHeader.AllowHide = False
        Me.LycgHeader.AppearanceGroup.Font = CType(resources.GetObject("LycgHeader.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LycgHeader.AppearanceGroup.Options.UseFont = True
        Me.LycgHeader.AppearanceItemCaption.Font = CType(resources.GetObject("LycgHeader.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LycgHeader.AppearanceItemCaption.Options.UseFont = True
        Me.LycgHeader.AppearanceTabPage.Header.Font = CType(resources.GetObject("LycgHeader.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LycgHeader.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgHeader.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LycgHeader.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LycgHeader.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgHeader.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LycgHeader.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LycgHeader.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgHeader.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LycgHeader.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LycgHeader.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgHeader.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LycgHeader.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LycgHeader.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LycgHeader, "LycgHeader")
        Me.LycgHeader.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LycgHeader.GroupBordersVisible = False
        Me.LycgHeader.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LycgGeneralGroup, Me.LayoutControlItem32, Me.LayoutControlItem33, Me.LayoutControlItem34, Me.EmptySpaceItem3})
        Me.LycgHeader.Name = "Root"
        Me.LycgHeader.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LycgHeader.ShowInCustomizationForm = False
        Me.LycgHeader.Size = New System.Drawing.Size(1177, 135)
        Me.LycgHeader.TextVisible = False
        '
        'LycgGeneralGroup
        '
        Me.LycgGeneralGroup.AllowHide = False
        Me.LycgGeneralGroup.AppearanceGroup.Font = CType(resources.GetObject("LycgGeneralGroup.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LycgGeneralGroup.AppearanceGroup.Options.UseFont = True
        Me.LycgGeneralGroup.AppearanceItemCaption.Font = CType(resources.GetObject("LycgGeneralGroup.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LycgGeneralGroup.AppearanceItemCaption.Options.UseFont = True
        Me.LycgGeneralGroup.AppearanceTabPage.Header.Font = CType(resources.GetObject("LycgGeneralGroup.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LycgGeneralGroup.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgGeneralGroup.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LycgGeneralGroup.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LycgGeneralGroup.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgGeneralGroup.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LycgGeneralGroup.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LycgGeneralGroup.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgGeneralGroup.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LycgGeneralGroup.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LycgGeneralGroup.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgGeneralGroup.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LycgGeneralGroup.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LycgGeneralGroup.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LycgGeneralGroup, "LycgGeneralGroup")
        Me.LycgGeneralGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LycgGeneralGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.LayoutControlItem2, Me.LayoutControlItem6, Me.LayoutControlItem21, Me.EmptySpaceItem1, Me.LayoutControlItem22, Me.LciStatusAdmission, Me.INDLciAdmission, Me.EmptySpaceItem5})
        Me.LycgGeneralGroup.Location = New System.Drawing.Point(0, 40)
        Me.LycgGeneralGroup.Name = "LycgGeneralGroup"
        Me.LycgGeneralGroup.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 0, 2, 9)
        Me.LycgGeneralGroup.Size = New System.Drawing.Size(1177, 95)
        Me.LycgGeneralGroup.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LycgGeneralGroup.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.TxtPlaceEntry1
        resources.ApplyResources(Me.LayoutControlItem4, "LayoutControlItem4")
        Me.LayoutControlItem4.Location = New System.Drawing.Point(291, 36)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(50, 25)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2)
        Me.LayoutControlItem4.Size = New System.Drawing.Size(92, 36)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(25, 21)
        Me.LayoutControlItem4.TextToControlDistance = 6
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.TxtAdmissionType1
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Location = New System.Drawing.Point(150, 36)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(50, 25)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(141, 36)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(35, 21)
        Me.LayoutControlItem3.TextToControlDistance = 6
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.TxtAdmissionDate1
        resources.ApplyResources(Me.LayoutControlItem5, "LayoutControlItem5")
        Me.LayoutControlItem5.Location = New System.Drawing.Point(383, 36)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(183, 36)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2)
        Me.LayoutControlItem5.Size = New System.Drawing.Size(253, 36)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(40, 21)
        Me.LayoutControlItem5.TextToControlDistance = 6
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem2.Control = Me.LblTotalEntity
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Location = New System.Drawing.Point(916, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(125, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(125, 1)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 6)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(125, 72)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(133, 22)
        Me.LayoutControlItem2.TextToControlDistance = 2
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem6.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlItem6.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem6.Control = Me.LblTotalPatient
        resources.ApplyResources(Me.LayoutControlItem6, "LayoutControlItem6")
        Me.LayoutControlItem6.Location = New System.Drawing.Point(1041, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(125, 0)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(125, 1)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 6)
        Me.LayoutControlItem6.Size = New System.Drawing.Size(125, 72)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(135, 22)
        Me.LayoutControlItem6.TextToControlDistance = 2
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.BteCountFolio
        resources.ApplyResources(Me.LayoutControlItem21, "LayoutControlItem21")
        Me.LayoutControlItem21.Location = New System.Drawing.Point(636, 36)
        Me.LayoutControlItem21.MaxSize = New System.Drawing.Size(114, 36)
        Me.LayoutControlItem21.MinSize = New System.Drawing.Size(114, 36)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem21.Size = New System.Drawing.Size(114, 36)
        Me.LayoutControlItem21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem21.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(40, 21)
        Me.LayoutControlItem21.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(814, 0)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(0, 10)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(1, 1)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(102, 72)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.Control = Me.DdbMenu
        resources.ApplyResources(Me.LayoutControlItem22, "LayoutControlItem22")
        Me.LayoutControlItem22.Location = New System.Drawing.Point(750, 0)
        Me.LayoutControlItem22.MaxSize = New System.Drawing.Size(64, 66)
        Me.LayoutControlItem22.MinSize = New System.Drawing.Size(64, 66)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 2, 0)
        Me.LayoutControlItem22.Size = New System.Drawing.Size(64, 72)
        Me.LayoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem22.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem22.TextToControlDistance = 0
        Me.LayoutControlItem22.TextVisible = False
        '
        'LciStatusAdmission
        '
        Me.LciStatusAdmission.Control = Me.TxtStatusAdmission
        Me.LciStatusAdmission.Location = New System.Drawing.Point(0, 36)
        Me.LciStatusAdmission.MaxSize = New System.Drawing.Size(0, 60)
        Me.LciStatusAdmission.MinSize = New System.Drawing.Size(150, 36)
        Me.LciStatusAdmission.Name = "LciStatusAdmission"
        Me.LciStatusAdmission.Size = New System.Drawing.Size(150, 36)
        Me.LciStatusAdmission.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.LciStatusAdmission, "LciStatusAdmission")
        Me.LciStatusAdmission.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LciStatusAdmission.TextSize = New System.Drawing.Size(50, 21)
        Me.LciStatusAdmission.TextToControlDistance = 12
        '
        'INDLciAdmission
        '
        Me.INDLciAdmission.Control = Me.INDSleAdmissionNumber2
        Me.INDLciAdmission.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAdmission.MaxSize = New System.Drawing.Size(750, 36)
        Me.INDLciAdmission.MinSize = New System.Drawing.Size(750, 36)
        Me.INDLciAdmission.Name = "INDLciAdmission"
        Me.INDLciAdmission.Size = New System.Drawing.Size(750, 36)
        Me.INDLciAdmission.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDLciAdmission, "INDLciAdmission")
        Me.INDLciAdmission.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdmission.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciAdmission.TextToControlDistance = 12
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.AllowHotTrack = False
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(0, 72)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(1166, 10)
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem32
        '
        Me.LayoutControlItem32.Control = Me.LabelControl4
        resources.ApplyResources(Me.LayoutControlItem32, "LayoutControlItem32")
        Me.LayoutControlItem32.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem32.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem32.MinSize = New System.Drawing.Size(10, 40)
        Me.LayoutControlItem32.Name = "LayoutControlItem32"
        Me.LayoutControlItem32.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.LayoutControlItem32.Size = New System.Drawing.Size(925, 40)
        Me.LayoutControlItem32.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem32.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem32.TextVisible = False
        '
        'LayoutControlItem33
        '
        Me.LayoutControlItem33.Control = Me.SleBillingAuthorization
        resources.ApplyResources(Me.LayoutControlItem33, "LayoutControlItem33")
        Me.LayoutControlItem33.Location = New System.Drawing.Point(925, 15)
        Me.LayoutControlItem33.MaxSize = New System.Drawing.Size(126, 0)
        Me.LayoutControlItem33.MinSize = New System.Drawing.Size(126, 1)
        Me.LayoutControlItem33.Name = "LayoutControlItem33"
        Me.LayoutControlItem33.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem33.Size = New System.Drawing.Size(126, 25)
        Me.LayoutControlItem33.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem33.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem33.TextVisible = False
        '
        'LayoutControlItem34
        '
        Me.LayoutControlItem34.Control = Me.SleOperatingUnit
        resources.ApplyResources(Me.LayoutControlItem34, "LayoutControlItem34")
        Me.LayoutControlItem34.Location = New System.Drawing.Point(1051, 15)
        Me.LayoutControlItem34.MaxSize = New System.Drawing.Size(126, 0)
        Me.LayoutControlItem34.MinSize = New System.Drawing.Size(126, 1)
        Me.LayoutControlItem34.Name = "LayoutControlItem34"
        Me.LayoutControlItem34.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem34.Size = New System.Drawing.Size(126, 25)
        Me.LayoutControlItem34.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem34.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem34.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem3, "EmptySpaceItem3")
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(925, 0)
        Me.EmptySpaceItem3.MaxSize = New System.Drawing.Size(0, 15)
        Me.EmptySpaceItem3.MinSize = New System.Drawing.Size(1, 15)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(252, 15)
        Me.EmptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'PccAdmission
        '
        Me.PccAdmission.Controls.Add(Me.LycPccAdmission)
        resources.ApplyResources(Me.PccAdmission, "PccAdmission")
        Me.PccAdmission.Name = "PccAdmission"
        '
        'LycPccAdmission
        '
        Me.LycPccAdmission.AllowCustomization = False
        Me.LycPccAdmission.Controls.Add(Me.LabelControl3)
        Me.LycPccAdmission.Controls.Add(Me.TxtContact)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientEstrato)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientEntityName)
        Me.LycPccAdmission.Controls.Add(Me.TxtCareGroupPatient)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientAge)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientBirth)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientName)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientType)
        Me.LycPccAdmission.Controls.Add(Me.TxtAfiliationType)
        Me.LycPccAdmission.Controls.Add(Me.TxtPatientCode)
        Me.LycPccAdmission.Controls.Add(Me.GdcStays)
        Me.LycPccAdmission.Controls.Add(Me.TxtRiskType)
        Me.LycPccAdmission.Controls.Add(Me.TxtCareGroupAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtBedStay)
        Me.LycPccAdmission.Controls.Add(Me.TxtResponsiblePhone)
        Me.LycPccAdmission.Controls.Add(Me.TxtResponsibleName)
        Me.LycPccAdmission.Controls.Add(Me.TxtEntityNameAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtAtentionCenter)
        Me.LycPccAdmission.Controls.Add(Me.TxtAuthorization)
        Me.LycPccAdmission.Controls.Add(Me.TxtPlaceEntry)
        Me.LycPccAdmission.Controls.Add(Me.TxtFunctionalUnitAdmission)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionDate)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionType)
        Me.LycPccAdmission.Controls.Add(Me.TxtLiquidationType)
        Me.LycPccAdmission.Controls.Add(Me.TxtAdmissionCode)
        Me.LycPccAdmission.Controls.Add(Me.GdcStaysLiquidated)
        resources.ApplyResources(Me.LycPccAdmission, "LycPccAdmission")
        Me.LycPccAdmission.Name = "LycPccAdmission"
        Me.LycPccAdmission.Root = Me.LycgPccAdmission
        '
        'LabelControl3
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl3, True)
        Me.LabelControl3.Appearance.Font = CType(resources.GetObject("LabelControl3.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl3.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.LabelControl3, "LabelControl3")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl3, False)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.StyleController = Me.LycPccAdmission
        '
        'TxtContact
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtContact, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtContact, False)
        resources.ApplyResources(Me.TxtContact, "TxtContact")
        Me.IndigoTextEdit1.SetMascara(Me.TxtContact, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtContact.MenuManager = Me.BarManager
        Me.TxtContact.Name = "TxtContact"
        Me.TxtContact.Properties.Appearance.Font = CType(resources.GetObject("TxtContact.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtContact.Properties.Appearance.Options.UseFont = True
        Me.TxtContact.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtContact.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtContact.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtContact.Properties.ReadOnly = True
        Me.TxtContact.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtContact, 0)
        '
        'TxtPatientEstrato
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientEstrato, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientEstrato, False)
        resources.ApplyResources(Me.TxtPatientEstrato, "TxtPatientEstrato")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientEstrato, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientEstrato.MenuManager = Me.BarManager
        Me.TxtPatientEstrato.Name = "TxtPatientEstrato"
        Me.TxtPatientEstrato.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientEstrato.Properties.Appearance.Font = CType(resources.GetObject("TxtPatientEstrato.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPatientEstrato.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientEstrato.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientEstrato.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtPatientEstrato.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtPatientEstrato.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientEstrato.Properties.ReadOnly = True
        Me.TxtPatientEstrato.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientEstrato, 0)
        '
        'TxtPatientEntityName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientEntityName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientEntityName, False)
        resources.ApplyResources(Me.TxtPatientEntityName, "TxtPatientEntityName")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientEntityName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientEntityName.MenuManager = Me.BarManager
        Me.TxtPatientEntityName.Name = "TxtPatientEntityName"
        Me.TxtPatientEntityName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientEntityName.Properties.Appearance.Font = CType(resources.GetObject("TxtPatientEntityName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPatientEntityName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientEntityName.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientEntityName.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtPatientEntityName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtPatientEntityName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientEntityName.Properties.ReadOnly = True
        Me.TxtPatientEntityName.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientEntityName, 0)
        '
        'TxtCareGroupPatient
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtCareGroupPatient, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtCareGroupPatient, False)
        resources.ApplyResources(Me.TxtCareGroupPatient, "TxtCareGroupPatient")
        Me.IndigoTextEdit1.SetMascara(Me.TxtCareGroupPatient, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtCareGroupPatient.MenuManager = Me.BarManager
        Me.TxtCareGroupPatient.Name = "TxtCareGroupPatient"
        Me.TxtCareGroupPatient.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtCareGroupPatient.Properties.Appearance.Font = CType(resources.GetObject("TxtCareGroupPatient.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtCareGroupPatient.Properties.Appearance.Options.UseBackColor = True
        Me.TxtCareGroupPatient.Properties.Appearance.Options.UseFont = True
        Me.TxtCareGroupPatient.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtCareGroupPatient.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtCareGroupPatient.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtCareGroupPatient.Properties.ReadOnly = True
        Me.TxtCareGroupPatient.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtCareGroupPatient, 0)
        '
        'TxtPatientAge
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientAge, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientAge, False)
        resources.ApplyResources(Me.TxtPatientAge, "TxtPatientAge")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientAge, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientAge.MenuManager = Me.BarManager
        Me.TxtPatientAge.Name = "TxtPatientAge"
        Me.TxtPatientAge.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientAge.Properties.Appearance.Font = CType(resources.GetObject("TxtPatientAge.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPatientAge.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientAge.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientAge.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtPatientAge.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtPatientAge.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientAge.Properties.ReadOnly = True
        Me.TxtPatientAge.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientAge, 0)
        '
        'TxtPatientBirth
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientBirth, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientBirth, False)
        resources.ApplyResources(Me.TxtPatientBirth, "TxtPatientBirth")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientBirth, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientBirth.MenuManager = Me.BarManager
        Me.TxtPatientBirth.Name = "TxtPatientBirth"
        Me.TxtPatientBirth.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientBirth.Properties.Appearance.Font = CType(resources.GetObject("TxtPatientBirth.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPatientBirth.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientBirth.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientBirth.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtPatientBirth.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtPatientBirth.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientBirth.Properties.ReadOnly = True
        Me.TxtPatientBirth.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientBirth, 0)
        '
        'TxtPatientName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientName, False)
        resources.ApplyResources(Me.TxtPatientName, "TxtPatientName")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientName.MenuManager = Me.BarManager
        Me.TxtPatientName.Name = "TxtPatientName"
        Me.TxtPatientName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientName.Properties.Appearance.Font = CType(resources.GetObject("TxtPatientName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPatientName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientName.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientName.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtPatientName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtPatientName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientName.Properties.ReadOnly = True
        Me.TxtPatientName.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientName, 0)
        '
        'TxtPatientType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientType, False)
        resources.ApplyResources(Me.TxtPatientType, "TxtPatientType")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientType.MenuManager = Me.BarManager
        Me.TxtPatientType.Name = "TxtPatientType"
        Me.TxtPatientType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientType.Properties.Appearance.Font = CType(resources.GetObject("TxtPatientType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPatientType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientType.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtPatientType.Properties.Items"), CType(resources.GetObject("TxtPatientType.Properties.Items1"), Object), CType(resources.GetObject("TxtPatientType.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtPatientType.Properties.Items3"), CType(resources.GetObject("TxtPatientType.Properties.Items4"), Object), CType(resources.GetObject("TxtPatientType.Properties.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtPatientType.Properties.Items6"), CType(resources.GetObject("TxtPatientType.Properties.Items7"), Object), CType(resources.GetObject("TxtPatientType.Properties.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtPatientType.Properties.Items9"), CType(resources.GetObject("TxtPatientType.Properties.Items10"), Object), CType(resources.GetObject("TxtPatientType.Properties.Items11"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtPatientType.Properties.Items12"), CType(resources.GetObject("TxtPatientType.Properties.Items13"), Object), CType(resources.GetObject("TxtPatientType.Properties.Items14"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtPatientType.Properties.Items15"), CType(resources.GetObject("TxtPatientType.Properties.Items16"), Object), CType(resources.GetObject("TxtPatientType.Properties.Items17"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtPatientType.Properties.Items18"), CType(resources.GetObject("TxtPatientType.Properties.Items19"), Object), CType(resources.GetObject("TxtPatientType.Properties.Items20"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtPatientType.Properties.Items21"), CType(resources.GetObject("TxtPatientType.Properties.Items22"), Object), CType(resources.GetObject("TxtPatientType.Properties.Items23"), Integer))})
        Me.TxtPatientType.Properties.ReadOnly = True
        Me.TxtPatientType.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientType, 0)
        '
        'TxtAfiliationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAfiliationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAfiliationType, False)
        resources.ApplyResources(Me.TxtAfiliationType, "TxtAfiliationType")
        Me.IndigoTextEdit1.SetMascara(Me.TxtAfiliationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAfiliationType.MenuManager = Me.BarManager
        Me.TxtAfiliationType.Name = "TxtAfiliationType"
        Me.TxtAfiliationType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAfiliationType.Properties.Appearance.Font = CType(resources.GetObject("TxtAfiliationType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAfiliationType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAfiliationType.Properties.Appearance.Options.UseFont = True
        Me.TxtAfiliationType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAfiliationType.Properties.Items"), CType(resources.GetObject("TxtAfiliationType.Properties.Items1"), Object), CType(resources.GetObject("TxtAfiliationType.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAfiliationType.Properties.Items3"), CType(resources.GetObject("TxtAfiliationType.Properties.Items4"), Object), CType(resources.GetObject("TxtAfiliationType.Properties.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAfiliationType.Properties.Items6"), CType(resources.GetObject("TxtAfiliationType.Properties.Items7"), Object), CType(resources.GetObject("TxtAfiliationType.Properties.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAfiliationType.Properties.Items9"), CType(resources.GetObject("TxtAfiliationType.Properties.Items10"), Object), CType(resources.GetObject("TxtAfiliationType.Properties.Items11"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAfiliationType.Properties.Items12"), CType(resources.GetObject("TxtAfiliationType.Properties.Items13"), Object), CType(resources.GetObject("TxtAfiliationType.Properties.Items14"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAfiliationType.Properties.Items15"), CType(resources.GetObject("TxtAfiliationType.Properties.Items16"), Object), CType(resources.GetObject("TxtAfiliationType.Properties.Items17"), Integer))})
        Me.TxtAfiliationType.Properties.ReadOnly = True
        Me.TxtAfiliationType.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAfiliationType, 0)
        '
        'TxtPatientCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPatientCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPatientCode, False)
        resources.ApplyResources(Me.TxtPatientCode, "TxtPatientCode")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPatientCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPatientCode.MenuManager = Me.BarManager
        Me.TxtPatientCode.Name = "TxtPatientCode"
        Me.TxtPatientCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPatientCode.Properties.Appearance.Font = CType(resources.GetObject("TxtPatientCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPatientCode.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPatientCode.Properties.Appearance.Options.UseFont = True
        Me.TxtPatientCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtPatientCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtPatientCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPatientCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("TxtPatientCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TxtPatientCode.Properties.ReadOnly = True
        Me.TxtPatientCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.TxtPatientCode.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPatientCode, 0)
        '
        'TxtRiskType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtRiskType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtRiskType, False)
        resources.ApplyResources(Me.TxtRiskType, "TxtRiskType")
        Me.IndigoTextEdit1.SetMascara(Me.TxtRiskType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtRiskType.MenuManager = Me.BarManager
        Me.TxtRiskType.Name = "TxtRiskType"
        Me.TxtRiskType.Properties.Appearance.Font = CType(resources.GetObject("TxtRiskType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtRiskType.Properties.Appearance.Options.UseFont = True
        Me.TxtRiskType.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtRiskType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtRiskType.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtRiskType.Properties.ReadOnly = True
        Me.TxtRiskType.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtRiskType, 0)
        '
        'TxtCareGroupAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtCareGroupAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtCareGroupAdmission, False)
        resources.ApplyResources(Me.TxtCareGroupAdmission, "TxtCareGroupAdmission")
        Me.IndigoTextEdit1.SetMascara(Me.TxtCareGroupAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtCareGroupAdmission.MenuManager = Me.BarManager
        Me.TxtCareGroupAdmission.Name = "TxtCareGroupAdmission"
        Me.TxtCareGroupAdmission.Properties.Appearance.Font = CType(resources.GetObject("TxtCareGroupAdmission.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtCareGroupAdmission.Properties.Appearance.Options.UseFont = True
        Me.TxtCareGroupAdmission.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtCareGroupAdmission.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtCareGroupAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtCareGroupAdmission.Properties.ReadOnly = True
        Me.TxtCareGroupAdmission.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtCareGroupAdmission, 0)
        '
        'TxtBedStay
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtBedStay, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtBedStay, False)
        resources.ApplyResources(Me.TxtBedStay, "TxtBedStay")
        Me.IndigoTextEdit1.SetMascara(Me.TxtBedStay, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtBedStay.Name = "TxtBedStay"
        Me.TxtBedStay.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtBedStay.Properties.Appearance.Font = CType(resources.GetObject("TxtBedStay.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtBedStay.Properties.Appearance.Options.UseBackColor = True
        Me.TxtBedStay.Properties.Appearance.Options.UseFont = True
        Me.TxtBedStay.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtBedStay.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtBedStay.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtBedStay.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtBedStay.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtBedStay.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtBedStay.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtBedStay.Properties.ReadOnly = True
        Me.TxtBedStay.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtBedStay, 0)
        '
        'TxtResponsiblePhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtResponsiblePhone, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtResponsiblePhone, False)
        resources.ApplyResources(Me.TxtResponsiblePhone, "TxtResponsiblePhone")
        Me.IndigoTextEdit1.SetMascara(Me.TxtResponsiblePhone, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtResponsiblePhone.Name = "TxtResponsiblePhone"
        Me.TxtResponsiblePhone.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtResponsiblePhone.Properties.Appearance.Font = CType(resources.GetObject("TxtResponsiblePhone.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtResponsiblePhone.Properties.Appearance.Options.UseBackColor = True
        Me.TxtResponsiblePhone.Properties.Appearance.Options.UseFont = True
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtResponsiblePhone.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtResponsiblePhone.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtResponsiblePhone.Properties.ReadOnly = True
        Me.TxtResponsiblePhone.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtResponsiblePhone, 0)
        '
        'TxtResponsibleName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtResponsibleName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtResponsibleName, False)
        resources.ApplyResources(Me.TxtResponsibleName, "TxtResponsibleName")
        Me.IndigoTextEdit1.SetMascara(Me.TxtResponsibleName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtResponsibleName.Name = "TxtResponsibleName"
        Me.TxtResponsibleName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtResponsibleName.Properties.Appearance.Font = CType(resources.GetObject("TxtResponsibleName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtResponsibleName.Properties.Appearance.Options.UseBackColor = True
        Me.TxtResponsibleName.Properties.Appearance.Options.UseFont = True
        Me.TxtResponsibleName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtResponsibleName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtResponsibleName.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtResponsibleName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtResponsibleName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtResponsibleName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtResponsibleName.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtResponsibleName.Properties.ReadOnly = True
        Me.TxtResponsibleName.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtResponsibleName, 0)
        '
        'TxtEntityNameAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtEntityNameAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtEntityNameAdmission, False)
        resources.ApplyResources(Me.TxtEntityNameAdmission, "TxtEntityNameAdmission")
        Me.IndigoTextEdit1.SetMascara(Me.TxtEntityNameAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtEntityNameAdmission.Name = "TxtEntityNameAdmission"
        Me.TxtEntityNameAdmission.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtEntityNameAdmission.Properties.Appearance.Font = CType(resources.GetObject("TxtEntityNameAdmission.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtEntityNameAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.TxtEntityNameAdmission.Properties.Appearance.Options.UseFont = True
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtEntityNameAdmission.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtEntityNameAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtEntityNameAdmission.Properties.ReadOnly = True
        Me.TxtEntityNameAdmission.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtEntityNameAdmission, 0)
        '
        'TxtAtentionCenter
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAtentionCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAtentionCenter, False)
        resources.ApplyResources(Me.TxtAtentionCenter, "TxtAtentionCenter")
        Me.IndigoTextEdit1.SetMascara(Me.TxtAtentionCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAtentionCenter.Name = "TxtAtentionCenter"
        Me.TxtAtentionCenter.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAtentionCenter.Properties.Appearance.Font = CType(resources.GetObject("TxtAtentionCenter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAtentionCenter.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAtentionCenter.Properties.Appearance.Options.UseFont = True
        Me.TxtAtentionCenter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAtentionCenter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAtentionCenter.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtAtentionCenter.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtAtentionCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAtentionCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAtentionCenter.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAtentionCenter.Properties.ReadOnly = True
        Me.TxtAtentionCenter.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAtentionCenter, 0)
        '
        'TxtAuthorization
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAuthorization, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAuthorization, False)
        resources.ApplyResources(Me.TxtAuthorization, "TxtAuthorization")
        Me.IndigoTextEdit1.SetMascara(Me.TxtAuthorization, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAuthorization.Name = "TxtAuthorization"
        Me.TxtAuthorization.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAuthorization.Properties.Appearance.Font = CType(resources.GetObject("TxtAuthorization.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAuthorization.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAuthorization.Properties.Appearance.Options.UseFont = True
        Me.TxtAuthorization.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAuthorization.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAuthorization.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtAuthorization.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtAuthorization.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAuthorization.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAuthorization.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAuthorization.Properties.ReadOnly = True
        Me.TxtAuthorization.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAuthorization, 0)
        '
        'TxtPlaceEntry
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtPlaceEntry, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtPlaceEntry, False)
        resources.ApplyResources(Me.TxtPlaceEntry, "TxtPlaceEntry")
        Me.IndigoTextEdit1.SetMascara(Me.TxtPlaceEntry, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtPlaceEntry.Name = "TxtPlaceEntry"
        Me.TxtPlaceEntry.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtPlaceEntry.Properties.Appearance.Font = CType(resources.GetObject("TxtPlaceEntry.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtPlaceEntry.Properties.Appearance.Options.UseBackColor = True
        Me.TxtPlaceEntry.Properties.Appearance.Options.UseFont = True
        Me.TxtPlaceEntry.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPlaceEntry.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtPlaceEntry.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtPlaceEntry.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtPlaceEntry.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtPlaceEntry.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtPlaceEntry.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtPlaceEntry.Properties.ReadOnly = True
        Me.TxtPlaceEntry.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtPlaceEntry, 0)
        '
        'TxtFunctionalUnitAdmission
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtFunctionalUnitAdmission, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtFunctionalUnitAdmission, False)
        resources.ApplyResources(Me.TxtFunctionalUnitAdmission, "TxtFunctionalUnitAdmission")
        Me.IndigoTextEdit1.SetMascara(Me.TxtFunctionalUnitAdmission, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtFunctionalUnitAdmission.Name = "TxtFunctionalUnitAdmission"
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.Font = CType(resources.GetObject("TxtFunctionalUnitAdmission.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.TxtFunctionalUnitAdmission.Properties.Appearance.Options.UseFont = True
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtFunctionalUnitAdmission.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtFunctionalUnitAdmission.Properties.ReadOnly = True
        Me.TxtFunctionalUnitAdmission.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtFunctionalUnitAdmission, 0)
        '
        'TxtAdmissionDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionDate, False)
        resources.ApplyResources(Me.TxtAdmissionDate, "TxtAdmissionDate")
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAdmissionDate.Name = "TxtAdmissionDate"
        Me.TxtAdmissionDate.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAdmissionDate.Properties.Appearance.Font = CType(resources.GetObject("TxtAdmissionDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAdmissionDate.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionDate.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAdmissionDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAdmissionDate.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtAdmissionDate.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtAdmissionDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAdmissionDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionDate.Properties.ReadOnly = True
        Me.TxtAdmissionDate.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionDate, 0)
        '
        'TxtAdmissionType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionType, False)
        resources.ApplyResources(Me.TxtAdmissionType, "TxtAdmissionType")
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAdmissionType.Name = "TxtAdmissionType"
        Me.TxtAdmissionType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAdmissionType.Properties.Appearance.Font = CType(resources.GetObject("TxtAdmissionType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAdmissionType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionType.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAdmissionType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAdmissionType.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtAdmissionType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtAdmissionType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAdmissionType.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAdmissionType.Properties.Items"), CType(resources.GetObject("TxtAdmissionType.Properties.Items1"), Object), CType(resources.GetObject("TxtAdmissionType.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtAdmissionType.Properties.Items3"), CType(resources.GetObject("TxtAdmissionType.Properties.Items4"), Object), CType(resources.GetObject("TxtAdmissionType.Properties.Items5"), Integer))})
        Me.TxtAdmissionType.Properties.ReadOnly = True
        Me.TxtAdmissionType.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionType, 0)
        '
        'TxtLiquidationType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtLiquidationType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtLiquidationType, False)
        resources.ApplyResources(Me.TxtLiquidationType, "TxtLiquidationType")
        Me.IndigoTextEdit1.SetMascara(Me.TxtLiquidationType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtLiquidationType.Name = "TxtLiquidationType"
        Me.TxtLiquidationType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtLiquidationType.Properties.Appearance.Font = CType(resources.GetObject("TxtLiquidationType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtLiquidationType.Properties.Appearance.Options.UseBackColor = True
        Me.TxtLiquidationType.Properties.Appearance.Options.UseFont = True
        Me.TxtLiquidationType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtLiquidationType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtLiquidationType.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtLiquidationType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtLiquidationType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtLiquidationType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtLiquidationType.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtLiquidationType.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtLiquidationType.Properties.Items"), CType(resources.GetObject("TxtLiquidationType.Properties.Items1"), Object), CType(resources.GetObject("TxtLiquidationType.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtLiquidationType.Properties.Items3"), CType(resources.GetObject("TxtLiquidationType.Properties.Items4"), Object), CType(resources.GetObject("TxtLiquidationType.Properties.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtLiquidationType.Properties.Items6"), CType(resources.GetObject("TxtLiquidationType.Properties.Items7"), Object), CType(resources.GetObject("TxtLiquidationType.Properties.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtLiquidationType.Properties.Items9"), CType(resources.GetObject("TxtLiquidationType.Properties.Items10"), Object), CType(resources.GetObject("TxtLiquidationType.Properties.Items11"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtLiquidationType.Properties.Items12"), CType(resources.GetObject("TxtLiquidationType.Properties.Items13"), Object), CType(resources.GetObject("TxtLiquidationType.Properties.Items14"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("TxtLiquidationType.Properties.Items15"), CType(resources.GetObject("TxtLiquidationType.Properties.Items16"), Object), CType(resources.GetObject("TxtLiquidationType.Properties.Items17"), Integer))})
        Me.TxtLiquidationType.Properties.ReadOnly = True
        Me.TxtLiquidationType.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtLiquidationType, 0)
        '
        'TxtAdmissionCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TxtAdmissionCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TxtAdmissionCode, False)
        resources.ApplyResources(Me.TxtAdmissionCode, "TxtAdmissionCode")
        Me.IndigoTextEdit1.SetMascara(Me.TxtAdmissionCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TxtAdmissionCode.Name = "TxtAdmissionCode"
        Me.TxtAdmissionCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TxtAdmissionCode.Properties.Appearance.Font = CType(resources.GetObject("TxtAdmissionCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TxtAdmissionCode.Properties.Appearance.Options.UseBackColor = True
        Me.TxtAdmissionCode.Properties.Appearance.Options.UseFont = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtAdmissionCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("TxtAdmissionCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TxtAdmissionCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.TxtAdmissionCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("TxtAdmissionCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.TxtAdmissionCode.Properties.ReadOnly = True
        Me.TxtAdmissionCode.StyleController = Me.LycPccAdmission
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TxtAdmissionCode, 0)
        '
        'GdcStaysLiquidated
        '
        Me.GdcStaysLiquidated.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GdcStaysLiquidated, "GdcStaysLiquidated")
        Me.GdcStaysLiquidated.MainView = Me.GdvStaysLiquidated
        Me.GdcStaysLiquidated.MenuManager = Me.BarManager
        Me.GdcStaysLiquidated.Name = "GdcStaysLiquidated"
        Me.GdcStaysLiquidated.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepEstLiqShowDetails, Me.RepEstLiqShowFolios})
        Me.GdcStaysLiquidated.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvStaysLiquidated})
        '
        'GdvStaysLiquidated
        '
        Me.GdvStaysLiquidated.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvStaysLiquidated.Appearance.GroupRow.Font = CType(resources.GetObject("GdvStaysLiquidated.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GdvStaysLiquidated.Appearance.GroupRow.Options.UseFont = True
        Me.GdvStaysLiquidated.Appearance.HeaderPanel.Font = CType(resources.GetObject("GdvStaysLiquidated.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GdvStaysLiquidated.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvStaysLiquidated.Appearance.Row.Font = CType(resources.GetObject("GdvStaysLiquidated.Appearance.Row.Font"), System.Drawing.Font)
        Me.GdvStaysLiquidated.Appearance.Row.Options.UseFont = True
        Me.GdvStaysLiquidated.Appearance.ViewCaption.Font = CType(resources.GetObject("GdvStaysLiquidated.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GdvStaysLiquidated.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvStaysLiquidated.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColEstLiqBed, Me.ColEstLiqInitDate, Me.ColEstLiqEndDate, Me.ColEstLiqType, Me.ColEstLiqTotalTime, Me.ColEstLiqTotalDays, Me.ColEstLiqLiqType, Me.ColEstLiqFolio, Me.ColEstLiqShowDetails})
        Me.GdvStaysLiquidated.GridControl = Me.GdcStaysLiquidated
        Me.GdvStaysLiquidated.Name = "GdvStaysLiquidated"
        Me.GdvStaysLiquidated.OptionsDetail.EnableMasterViewMode = False
        Me.GdvStaysLiquidated.OptionsDetail.ShowDetailTabs = False
        Me.GdvStaysLiquidated.OptionsDetail.SmartDetailExpand = False
        Me.GdvStaysLiquidated.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvStaysLiquidated.OptionsView.EnableAppearanceOddRow = True
        Me.GdvStaysLiquidated.OptionsView.ShowGroupPanel = False
        Me.GdvStaysLiquidated.OptionsView.ShowIndicator = False
        '
        'ColEstLiqBed
        '
        Me.ColEstLiqBed.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqBed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqBed.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqBed, "ColEstLiqBed")
        Me.ColEstLiqBed.FieldName = "CHCAMASHO.NUMCAMHOS"
        Me.ColEstLiqBed.Name = "ColEstLiqBed"
        Me.ColEstLiqBed.OptionsColumn.AllowEdit = False
        Me.ColEstLiqBed.OptionsColumn.AllowFocus = False
        Me.ColEstLiqBed.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqBed.OptionsColumn.AllowMove = False
        Me.ColEstLiqBed.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqBed.OptionsColumn.ReadOnly = True
        Me.ColEstLiqBed.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqBed.OptionsFilter.AllowFilter = False
        '
        'ColEstLiqInitDate
        '
        Me.ColEstLiqInitDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqInitDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqInitDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqInitDate, "ColEstLiqInitDate")
        Me.ColEstLiqInitDate.FieldName = "FECINIEST"
        Me.ColEstLiqInitDate.Name = "ColEstLiqInitDate"
        Me.ColEstLiqInitDate.OptionsColumn.AllowEdit = False
        Me.ColEstLiqInitDate.OptionsColumn.AllowFocus = False
        Me.ColEstLiqInitDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqInitDate.OptionsColumn.AllowMove = False
        Me.ColEstLiqInitDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqInitDate.OptionsColumn.ReadOnly = True
        Me.ColEstLiqInitDate.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqInitDate.OptionsFilter.AllowFilter = False
        '
        'ColEstLiqEndDate
        '
        Me.ColEstLiqEndDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqEndDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqEndDate, "ColEstLiqEndDate")
        Me.ColEstLiqEndDate.FieldName = "FECFINEST"
        Me.ColEstLiqEndDate.Name = "ColEstLiqEndDate"
        Me.ColEstLiqEndDate.OptionsColumn.AllowEdit = False
        Me.ColEstLiqEndDate.OptionsColumn.AllowFocus = False
        Me.ColEstLiqEndDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqEndDate.OptionsColumn.AllowMove = False
        Me.ColEstLiqEndDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqEndDate.OptionsColumn.ReadOnly = True
        Me.ColEstLiqEndDate.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqEndDate.OptionsFilter.AllowFilter = False
        '
        'ColEstLiqType
        '
        Me.ColEstLiqType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqType, "ColEstLiqType")
        Me.ColEstLiqType.FieldName = "CHTIPESTA.DESTIPEST"
        Me.ColEstLiqType.Name = "ColEstLiqType"
        Me.ColEstLiqType.OptionsColumn.AllowEdit = False
        Me.ColEstLiqType.OptionsColumn.AllowFocus = False
        Me.ColEstLiqType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqType.OptionsColumn.AllowMove = False
        Me.ColEstLiqType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqType.OptionsColumn.ReadOnly = True
        Me.ColEstLiqType.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqType.OptionsFilter.AllowFilter = False
        '
        'ColEstLiqTotalTime
        '
        Me.ColEstLiqTotalTime.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqTotalTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqTotalTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqTotalTime, "ColEstLiqTotalTime")
        Me.ColEstLiqTotalTime.FieldName = "TotalTime"
        Me.ColEstLiqTotalTime.Name = "ColEstLiqTotalTime"
        Me.ColEstLiqTotalTime.OptionsColumn.AllowEdit = False
        Me.ColEstLiqTotalTime.OptionsColumn.AllowFocus = False
        Me.ColEstLiqTotalTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqTotalTime.OptionsColumn.AllowMove = False
        Me.ColEstLiqTotalTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqTotalTime.OptionsColumn.ReadOnly = True
        '
        'ColEstLiqTotalDays
        '
        Me.ColEstLiqTotalDays.AppearanceCell.Options.UseTextOptions = True
        Me.ColEstLiqTotalDays.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqTotalDays.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColEstLiqTotalDays.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqTotalDays.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqTotalDays.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqTotalDays, "ColEstLiqTotalDays")
        Me.ColEstLiqTotalDays.FieldName = "TotalUnits"
        Me.ColEstLiqTotalDays.Name = "ColEstLiqTotalDays"
        Me.ColEstLiqTotalDays.OptionsColumn.AllowEdit = False
        Me.ColEstLiqTotalDays.OptionsColumn.AllowFocus = False
        Me.ColEstLiqTotalDays.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqTotalDays.OptionsColumn.AllowMove = False
        Me.ColEstLiqTotalDays.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqTotalDays.OptionsColumn.ReadOnly = True
        Me.ColEstLiqTotalDays.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqTotalDays.OptionsFilter.AllowFilter = False
        '
        'ColEstLiqLiqType
        '
        Me.ColEstLiqLiqType.AppearanceCell.Options.UseTextOptions = True
        Me.ColEstLiqLiqType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.ColEstLiqLiqType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColEstLiqLiqType.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqLiqType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqLiqType.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqLiqType, "ColEstLiqLiqType")
        Me.ColEstLiqLiqType.FieldName = "GENESTLIQ"
        Me.ColEstLiqLiqType.Name = "ColEstLiqLiqType"
        Me.ColEstLiqLiqType.OptionsColumn.AllowEdit = False
        Me.ColEstLiqLiqType.OptionsColumn.AllowFocus = False
        Me.ColEstLiqLiqType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqLiqType.OptionsColumn.AllowMove = False
        Me.ColEstLiqLiqType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqLiqType.OptionsColumn.ReadOnly = True
        Me.ColEstLiqLiqType.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqLiqType.OptionsFilter.AllowFilter = False
        '
        'ColEstLiqFolio
        '
        Me.ColEstLiqFolio.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqFolio.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqFolio.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqFolio, "ColEstLiqFolio")
        Me.ColEstLiqFolio.ColumnEdit = Me.RepEstLiqShowFolios
        Me.ColEstLiqFolio.Name = "ColEstLiqFolio"
        Me.ColEstLiqFolio.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqFolio.OptionsColumn.FixedWidth = True
        Me.ColEstLiqFolio.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqFolio.OptionsFilter.AllowFilter = False
        '
        'RepEstLiqShowFolios
        '
        resources.ApplyResources(Me.RepEstLiqShowFolios, "RepEstLiqShowFolios")
        Me.RepEstLiqShowFolios.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepEstLiqShowFolios.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepEstLiqShowFolios.Name = "RepEstLiqShowFolios"
        Me.RepEstLiqShowFolios.PopupControl = Me.PccStayFolios
        Me.RepEstLiqShowFolios.PopupFormMinSize = New System.Drawing.Size(250, 181)
        Me.RepEstLiqShowFolios.PopupFormSize = New System.Drawing.Size(250, 181)
        Me.RepEstLiqShowFolios.ShowPopupCloseButton = False
        Me.RepEstLiqShowFolios.ShowPopupShadow = False
        Me.RepEstLiqShowFolios.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'PccStayFolios
        '
        Me.PccStayFolios.Controls.Add(Me.LayoutControl2)
        resources.ApplyResources(Me.PccStayFolios, "PccStayFolios")
        Me.PccStayFolios.Name = "PccStayFolios"
        '
        'LayoutControl2
        '
        Me.LayoutControl2.AllowCustomization = False
        Me.LayoutControl2.Controls.Add(Me.GdcStayFolios)
        resources.ApplyResources(Me.LayoutControl2, "LayoutControl2")
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup4
        '
        'GdcStayFolios
        '
        Me.GdcStayFolios.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GdcStayFolios, "GdcStayFolios")
        Me.GdcStayFolios.MainView = Me.GdvStayFolios
        Me.GdcStayFolios.MenuManager = Me.BarManager
        Me.GdcStayFolios.Name = "GdcStayFolios"
        Me.GdcStayFolios.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvStayFolios})
        '
        'GdvStayFolios
        '
        Me.GdvStayFolios.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvStayFolios.Appearance.GroupRow.Font = CType(resources.GetObject("GdvStayFolios.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GdvStayFolios.Appearance.GroupRow.Options.UseFont = True
        Me.GdvStayFolios.Appearance.HeaderPanel.Font = CType(resources.GetObject("GdvStayFolios.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GdvStayFolios.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvStayFolios.Appearance.Row.Font = CType(resources.GetObject("GdvStayFolios.Appearance.Row.Font"), System.Drawing.Font)
        Me.GdvStayFolios.Appearance.Row.Options.UseFont = True
        Me.GdvStayFolios.Appearance.ViewCaption.Font = CType(resources.GetObject("GdvStayFolios.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GdvStayFolios.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvStayFolios.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColStayFoliosNumber, Me.ColStayFoliosValue})
        Me.GdvStayFolios.GridControl = Me.GdcStayFolios
        Me.GdvStayFolios.Name = "GdvStayFolios"
        Me.GdvStayFolios.OptionsDetail.ShowDetailTabs = False
        Me.GdvStayFolios.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvStayFolios.OptionsView.EnableAppearanceOddRow = True
        Me.GdvStayFolios.OptionsView.ShowDetailButtons = False
        Me.GdvStayFolios.OptionsView.ShowGroupPanel = False
        Me.GdvStayFolios.OptionsView.ShowIndicator = False
        '
        'ColStayFoliosNumber
        '
        Me.ColStayFoliosNumber.AppearanceCell.Options.UseTextOptions = True
        Me.ColStayFoliosNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColStayFoliosNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColStayFoliosNumber.AppearanceHeader.Options.UseTextOptions = True
        Me.ColStayFoliosNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColStayFoliosNumber.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColStayFoliosNumber, "ColStayFoliosNumber")
        Me.ColStayFoliosNumber.FieldName = "NumberFolio"
        Me.ColStayFoliosNumber.Name = "ColStayFoliosNumber"
        Me.ColStayFoliosNumber.OptionsColumn.AllowEdit = False
        Me.ColStayFoliosNumber.OptionsColumn.AllowFocus = False
        Me.ColStayFoliosNumber.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColStayFoliosNumber.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColStayFoliosNumber.OptionsColumn.FixedWidth = True
        Me.ColStayFoliosNumber.OptionsColumn.ReadOnly = True
        Me.ColStayFoliosNumber.OptionsFilter.AllowAutoFilter = False
        Me.ColStayFoliosNumber.OptionsFilter.AllowFilter = False
        '
        'ColStayFoliosValue
        '
        Me.ColStayFoliosValue.AppearanceCell.Options.UseTextOptions = True
        Me.ColStayFoliosValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColStayFoliosValue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColStayFoliosValue.AppearanceHeader.Options.UseTextOptions = True
        Me.ColStayFoliosValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColStayFoliosValue.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColStayFoliosValue, "ColStayFoliosValue")
        Me.ColStayFoliosValue.DisplayFormat.FormatString = "C2"
        Me.ColStayFoliosValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColStayFoliosValue.FieldName = "ValueFolio"
        Me.ColStayFoliosValue.Name = "ColStayFoliosValue"
        Me.ColStayFoliosValue.OptionsColumn.AllowEdit = False
        Me.ColStayFoliosValue.OptionsColumn.AllowFocus = False
        Me.ColStayFoliosValue.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColStayFoliosValue.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColStayFoliosValue.OptionsColumn.ReadOnly = True
        Me.ColStayFoliosValue.OptionsFilter.AllowAutoFilter = False
        Me.ColStayFoliosValue.OptionsFilter.AllowFilter = False
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
        resources.ApplyResources(Me.LayoutControlGroup4, "LayoutControlGroup4")
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup5})
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(250, 181)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup5.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup5.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup5.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup5.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup5.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup5.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup5, "LayoutControlGroup5")
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem38})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(250, 181)
        '
        'LayoutControlItem38
        '
        Me.LayoutControlItem38.Control = Me.GdcStayFolios
        resources.ApplyResources(Me.LayoutControlItem38, "LayoutControlItem38")
        Me.LayoutControlItem38.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem38.Name = "LayoutControlItem38"
        Me.LayoutControlItem38.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem38.Size = New System.Drawing.Size(244, 153)
        Me.LayoutControlItem38.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem38.TextVisible = False
        '
        'ColEstLiqShowDetails
        '
        Me.ColEstLiqShowDetails.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqShowDetails.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqShowDetails.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqShowDetails, "ColEstLiqShowDetails")
        Me.ColEstLiqShowDetails.ColumnEdit = Me.RepEstLiqShowDetails
        Me.ColEstLiqShowDetails.Name = "ColEstLiqShowDetails"
        Me.ColEstLiqShowDetails.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqShowDetails.OptionsColumn.FixedWidth = True
        Me.ColEstLiqShowDetails.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqShowDetails.OptionsFilter.AllowFilter = False
        '
        'RepEstLiqShowDetails
        '
        resources.ApplyResources(Me.RepEstLiqShowDetails, "RepEstLiqShowDetails")
        Me.RepEstLiqShowDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepEstLiqShowDetails.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepEstLiqShowDetails.Name = "RepEstLiqShowDetails"
        Me.RepEstLiqShowDetails.PopupFormMinSize = New System.Drawing.Size(453, 181)
        Me.RepEstLiqShowDetails.PopupFormSize = New System.Drawing.Size(453, 181)
        Me.RepEstLiqShowDetails.ShowPopupCloseButton = False
        Me.RepEstLiqShowDetails.ShowPopupShadow = False
        Me.RepEstLiqShowDetails.Tag = "L"
        Me.RepEstLiqShowDetails.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'LycgPccAdmission
        '
        Me.LycgPccAdmission.AppearanceGroup.Font = CType(resources.GetObject("LycgPccAdmission.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LycgPccAdmission.AppearanceGroup.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceItemCaption.Font = CType(resources.GetObject("LycgPccAdmission.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LycgPccAdmission.AppearanceItemCaption.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.Header.Font = CType(resources.GetObject("LycgPccAdmission.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LycgPccAdmission.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LycgPccAdmission.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LycgPccAdmission.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LycgPccAdmission.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LycgPccAdmission.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LycgPccAdmission.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LycgPccAdmission.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgPccAdmission.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LycgPccAdmission.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LycgPccAdmission.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LycgPccAdmission, "LycgPccAdmission")
        Me.LycgPccAdmission.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LycgPccAdmission.GroupBordersVisible = False
        Me.LycgPccAdmission.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup1, Me.LayoutControlItem31})
        Me.LycgPccAdmission.Name = "LycgPccAdmission"
        Me.LycgPccAdmission.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LycgPccAdmission.Size = New System.Drawing.Size(799, 305)
        Me.LycgPccAdmission.TextVisible = False
        '
        'TabbedControlGroup1
        '
        resources.ApplyResources(Me.TabbedControlGroup1, "TabbedControlGroup1")
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 40)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LayoutControlGroup1
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(799, 265)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LycgAdmissionGroup, Me.LycgStays})
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
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem23, Me.LayoutControlItem25, Me.LiCareGroup, Me.LayoutControlItem29, Me.LayoutControlItem26, Me.LiEntity, Me.LayoutControlItem24, Me.LayoutControlItem18, Me.LayoutControlItem17, Me.TxtContacto})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(775, 214)
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem23.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem23.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem23.Control = Me.TxtPatientCode
        resources.ApplyResources(Me.LayoutControlItem23, "LayoutControlItem23")
        Me.LayoutControlItem23.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem23.MinSize = New System.Drawing.Size(50, 25)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem23.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem23.TextToControlDistance = 12
        '
        'LayoutControlItem25
        '
        Me.LayoutControlItem25.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem25.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem25.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem25.Control = Me.TxtPatientBirth
        resources.ApplyResources(Me.LayoutControlItem25, "LayoutControlItem25")
        Me.LayoutControlItem25.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem25.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem25.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem25.Name = "LayoutControlItem25"
        Me.LayoutControlItem25.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem25.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem25.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem25.TextToControlDistance = 12
        '
        'LiCareGroup
        '
        Me.LiCareGroup.AppearanceItemCaption.Font = CType(resources.GetObject("LiCareGroup.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LiCareGroup.AppearanceItemCaption.Options.UseFont = True
        Me.LiCareGroup.Control = Me.TxtCareGroupPatient
        resources.ApplyResources(Me.LiCareGroup, "LiCareGroup")
        Me.LiCareGroup.Location = New System.Drawing.Point(0, 90)
        Me.LiCareGroup.MaxSize = New System.Drawing.Size(300, 30)
        Me.LiCareGroup.MinSize = New System.Drawing.Size(300, 30)
        Me.LiCareGroup.Name = "LiCareGroup"
        Me.LiCareGroup.Size = New System.Drawing.Size(300, 30)
        Me.LiCareGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiCareGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiCareGroup.TextSize = New System.Drawing.Size(115, 21)
        Me.LiCareGroup.TextToControlDistance = 12
        '
        'LayoutControlItem29
        '
        Me.LayoutControlItem29.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem29.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem29.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem29.Control = Me.TxtPatientEstrato
        resources.ApplyResources(Me.LayoutControlItem29, "LayoutControlItem29")
        Me.LayoutControlItem29.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem29.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem29.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem29.Name = "LayoutControlItem29"
        Me.LayoutControlItem29.Size = New System.Drawing.Size(300, 94)
        Me.LayoutControlItem29.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem29.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem29.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem29.TextToControlDistance = 12
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem26.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem26.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem26.Control = Me.TxtPatientAge
        resources.ApplyResources(Me.LayoutControlItem26, "LayoutControlItem26")
        Me.LayoutControlItem26.Location = New System.Drawing.Point(300, 30)
        Me.LayoutControlItem26.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem26.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem26.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem26.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem26.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem26.TextToControlDistance = 12
        '
        'LiEntity
        '
        Me.LiEntity.AppearanceItemCaption.Font = CType(resources.GetObject("LiEntity.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LiEntity.AppearanceItemCaption.Options.UseFont = True
        Me.LiEntity.Control = Me.TxtPatientEntityName
        resources.ApplyResources(Me.LiEntity, "LiEntity")
        Me.LiEntity.Location = New System.Drawing.Point(300, 90)
        Me.LiEntity.MaxSize = New System.Drawing.Size(340, 30)
        Me.LiEntity.MinSize = New System.Drawing.Size(340, 30)
        Me.LiEntity.Name = "LiEntity"
        Me.LiEntity.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LiEntity.Size = New System.Drawing.Size(475, 30)
        Me.LiEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LiEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LiEntity.TextSize = New System.Drawing.Size(115, 21)
        Me.LiEntity.TextToControlDistance = 12
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem24.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem24.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem24.Control = Me.TxtPatientName
        resources.ApplyResources(Me.LayoutControlItem24, "LayoutControlItem24")
        Me.LayoutControlItem24.Location = New System.Drawing.Point(300, 0)
        Me.LayoutControlItem24.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem24.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem24.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem24.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem24.TextToControlDistance = 12
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem18.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem18.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem18.Control = Me.TxtAfiliationType
        resources.ApplyResources(Me.LayoutControlItem18, "LayoutControlItem18")
        Me.LayoutControlItem18.Location = New System.Drawing.Point(300, 60)
        Me.LayoutControlItem18.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem18.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem18.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem18.TextToControlDistance = 12
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem17.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem17.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem17.Control = Me.TxtPatientType
        resources.ApplyResources(Me.LayoutControlItem17, "LayoutControlItem17")
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem17.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem17.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem17.TextToControlDistance = 12
        '
        'TxtContacto
        '
        Me.TxtContacto.AppearanceItemCaption.Font = CType(resources.GetObject("TxtContacto.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.TxtContacto.AppearanceItemCaption.Options.UseFont = True
        Me.TxtContacto.Control = Me.TxtContact
        resources.ApplyResources(Me.TxtContacto, "TxtContacto")
        Me.TxtContacto.Location = New System.Drawing.Point(300, 120)
        Me.TxtContacto.MaxSize = New System.Drawing.Size(340, 30)
        Me.TxtContacto.MinSize = New System.Drawing.Size(340, 30)
        Me.TxtContacto.Name = "TxtContacto"
        Me.TxtContacto.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.TxtContacto.Size = New System.Drawing.Size(475, 94)
        Me.TxtContacto.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.TxtContacto.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.TxtContacto.TextSize = New System.Drawing.Size(115, 20)
        Me.TxtContacto.TextToControlDistance = 12
        '
        'LycgAdmissionGroup
        '
        Me.LycgAdmissionGroup.AppearanceGroup.Font = CType(resources.GetObject("LycgAdmissionGroup.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LycgAdmissionGroup.AppearanceGroup.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceItemCaption.Font = CType(resources.GetObject("LycgAdmissionGroup.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LycgAdmissionGroup.AppearanceItemCaption.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.Header.Font = CType(resources.GetObject("LycgAdmissionGroup.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LycgAdmissionGroup.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LycgAdmissionGroup.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LycgAdmissionGroup.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LycgAdmissionGroup.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LycgAdmissionGroup.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgAdmissionGroup.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LycgAdmissionGroup.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LycgAdmissionGroup.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LycgAdmissionGroup, "LycgAdmissionGroup")
        Me.LycgAdmissionGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem11, Me.LayoutControlItem13, Me.LayoutControlItem14, Me.LayoutControlItem15, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem27, Me.LayoutControlItem16, Me.LayoutControlItem39, Me.LayoutControlItem12, Me.LayoutControlItem10, Me.LayoutControlItem19, Me.LayoutControlItem20})
        Me.LycgAdmissionGroup.Location = New System.Drawing.Point(0, 0)
        Me.LycgAdmissionGroup.Name = "LycgAdmissionGroup"
        Me.LycgAdmissionGroup.Size = New System.Drawing.Size(775, 214)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem7.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.Control = Me.TxtAdmissionCode
        resources.ApplyResources(Me.LayoutControlItem7, "LayoutControlItem7")
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem7.TextToControlDistance = 12
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem11.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem11.Control = Me.TxtAdmissionType
        resources.ApplyResources(Me.LayoutControlItem11, "LayoutControlItem11")
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem11.TextToControlDistance = 12
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem13.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem13.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem13.Control = Me.TxtLiquidationType
        resources.ApplyResources(Me.LayoutControlItem13, "LayoutControlItem13")
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem13.TextToControlDistance = 12
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem14.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem14.Control = Me.TxtAuthorization
        resources.ApplyResources(Me.LayoutControlItem14, "LayoutControlItem14")
        Me.LayoutControlItem14.Location = New System.Drawing.Point(300, 120)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem14.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem14.TextToControlDistance = 12
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem15.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem15.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem15.Control = Me.TxtAtentionCenter
        resources.ApplyResources(Me.LayoutControlItem15, "LayoutControlItem15")
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 150)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem15.TextToControlDistance = 12
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem8.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem8.Control = Me.TxtBedStay
        resources.ApplyResources(Me.LayoutControlItem8, "LayoutControlItem8")
        Me.LayoutControlItem8.Location = New System.Drawing.Point(300, 90)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem8.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem8.TextToControlDistance = 12
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem9.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem9.Control = Me.TxtAdmissionDate
        resources.ApplyResources(Me.LayoutControlItem9, "LayoutControlItem9")
        Me.LayoutControlItem9.Location = New System.Drawing.Point(300, 0)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem9.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem9.TextToControlDistance = 12
        '
        'LayoutControlItem27
        '
        Me.LayoutControlItem27.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem27.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem27.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem27.Control = Me.TxtCareGroupAdmission
        resources.ApplyResources(Me.LayoutControlItem27, "LayoutControlItem27")
        Me.LayoutControlItem27.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem27.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem27.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem27.Name = "LayoutControlItem27"
        Me.LayoutControlItem27.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem27.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem27.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem27.TextSize = New System.Drawing.Size(115, 20)
        Me.LayoutControlItem27.TextToControlDistance = 12
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem16.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem16.Control = Me.TxtEntityNameAdmission
        resources.ApplyResources(Me.LayoutControlItem16, "LayoutControlItem16")
        Me.LayoutControlItem16.Location = New System.Drawing.Point(300, 30)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem16.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem16.TextToControlDistance = 12
        '
        'LayoutControlItem39
        '
        Me.LayoutControlItem39.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem39.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem39.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem39.Control = Me.TxtRiskType
        resources.ApplyResources(Me.LayoutControlItem39, "LayoutControlItem39")
        Me.LayoutControlItem39.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem39.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem39.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem39.Name = "LayoutControlItem39"
        Me.LayoutControlItem39.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem39.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem39.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem39.TextSize = New System.Drawing.Size(115, 20)
        Me.LayoutControlItem39.TextToControlDistance = 12
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem12.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem12.Control = Me.TxtPlaceEntry
        resources.ApplyResources(Me.LayoutControlItem12, "LayoutControlItem12")
        Me.LayoutControlItem12.Location = New System.Drawing.Point(300, 60)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem12.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem12.TextToControlDistance = 12
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem10.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem10.Control = Me.TxtFunctionalUnitAdmission
        resources.ApplyResources(Me.LayoutControlItem10, "LayoutControlItem10")
        Me.LayoutControlItem10.Location = New System.Drawing.Point(300, 150)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem10.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem10.TextToControlDistance = 12
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem19.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem19.Control = Me.TxtResponsibleName
        resources.ApplyResources(Me.LayoutControlItem19, "LayoutControlItem19")
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(300, 34)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem19.TextToControlDistance = 12
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem20.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem20.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem20.Control = Me.TxtResponsiblePhone
        resources.ApplyResources(Me.LayoutControlItem20, "LayoutControlItem20")
        Me.LayoutControlItem20.Location = New System.Drawing.Point(300, 180)
        Me.LayoutControlItem20.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem20.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem20.Size = New System.Drawing.Size(475, 34)
        Me.LayoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem20.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem20.TextToControlDistance = 12
        '
        'LycgStays
        '
        Me.LycgStays.AppearanceGroup.Font = CType(resources.GetObject("LycgStays.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LycgStays.AppearanceGroup.Options.UseFont = True
        Me.LycgStays.AppearanceItemCaption.Font = CType(resources.GetObject("LycgStays.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LycgStays.AppearanceItemCaption.Options.UseFont = True
        Me.LycgStays.AppearanceTabPage.Header.Font = CType(resources.GetObject("LycgStays.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LycgStays.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgStays.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LycgStays.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LycgStays.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgStays.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LycgStays.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LycgStays.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgStays.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LycgStays.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LycgStays.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgStays.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LycgStays.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LycgStays.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LycgStays, "LycgStays")
        Me.LycgStays.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup2})
        Me.LycgStays.Location = New System.Drawing.Point(0, 0)
        Me.LycgStays.Name = "LycgStays"
        Me.LycgStays.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LycgStays.Size = New System.Drawing.Size(775, 214)
        Me.LycgStays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'TabbedControlGroup2
        '
        resources.ApplyResources(Me.TabbedControlGroup2, "TabbedControlGroup2")
        Me.TabbedControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.TabbedControlGroup2.Name = "TabbedControlGroup2"
        Me.TabbedControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.TabbedControlGroup2.SelectedTabPage = Me.LycgStaysDontLiquidated
        Me.TabbedControlGroup2.Size = New System.Drawing.Size(775, 214)
        Me.TabbedControlGroup2.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LycgStaysDontLiquidated, Me.LycgStaysLiquidated})
        '
        'LycgStaysDontLiquidated
        '
        Me.LycgStaysDontLiquidated.AppearanceGroup.Font = CType(resources.GetObject("LycgStaysDontLiquidated.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LycgStaysDontLiquidated.AppearanceGroup.Options.UseFont = True
        Me.LycgStaysDontLiquidated.AppearanceItemCaption.Font = CType(resources.GetObject("LycgStaysDontLiquidated.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LycgStaysDontLiquidated.AppearanceItemCaption.Options.UseFont = True
        Me.LycgStaysDontLiquidated.AppearanceTabPage.Header.Font = CType(resources.GetObject("LycgStaysDontLiquidated.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LycgStaysDontLiquidated.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgStaysDontLiquidated.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LycgStaysDontLiquidated.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LycgStaysDontLiquidated.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgStaysDontLiquidated.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LycgStaysDontLiquidated.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LycgStaysDontLiquidated.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgStaysDontLiquidated.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LycgStaysDontLiquidated.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LycgStaysDontLiquidated.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgStaysDontLiquidated.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LycgStaysDontLiquidated.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LycgStaysDontLiquidated.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LycgStaysDontLiquidated, "LycgStaysDontLiquidated")
        Me.LycgStaysDontLiquidated.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem36})
        Me.LycgStaysDontLiquidated.Location = New System.Drawing.Point(0, 0)
        Me.LycgStaysDontLiquidated.Name = "LycgStaysDontLiquidated"
        Me.LycgStaysDontLiquidated.Size = New System.Drawing.Size(769, 181)
        Me.LycgStaysDontLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem36
        '
        Me.LayoutControlItem36.Control = Me.GdcStays
        resources.ApplyResources(Me.LayoutControlItem36, "LayoutControlItem36")
        Me.LayoutControlItem36.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem36.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem36.Name = "LayoutControlItem36"
        Me.LayoutControlItem36.Size = New System.Drawing.Size(769, 181)
        Me.LayoutControlItem36.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem36.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem36.TextVisible = False
        '
        'LycgStaysLiquidated
        '
        Me.LycgStaysLiquidated.AppearanceGroup.Font = CType(resources.GetObject("LycgStaysLiquidated.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LycgStaysLiquidated.AppearanceGroup.Options.UseFont = True
        Me.LycgStaysLiquidated.AppearanceItemCaption.Font = CType(resources.GetObject("LycgStaysLiquidated.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LycgStaysLiquidated.AppearanceItemCaption.Options.UseFont = True
        Me.LycgStaysLiquidated.AppearanceTabPage.Header.Font = CType(resources.GetObject("LycgStaysLiquidated.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LycgStaysLiquidated.AppearanceTabPage.Header.Options.UseFont = True
        Me.LycgStaysLiquidated.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LycgStaysLiquidated.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LycgStaysLiquidated.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LycgStaysLiquidated.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LycgStaysLiquidated.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LycgStaysLiquidated.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LycgStaysLiquidated.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LycgStaysLiquidated.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LycgStaysLiquidated.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LycgStaysLiquidated.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LycgStaysLiquidated.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LycgStaysLiquidated.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LycgStaysLiquidated, "LycgStaysLiquidated")
        Me.LycgStaysLiquidated.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem35})
        Me.LycgStaysLiquidated.Location = New System.Drawing.Point(0, 0)
        Me.LycgStaysLiquidated.Name = "LycgStaysLiquidated"
        Me.LycgStaysLiquidated.Size = New System.Drawing.Size(769, 181)
        Me.LycgStaysLiquidated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem35
        '
        Me.LayoutControlItem35.Control = Me.GdcStaysLiquidated
        resources.ApplyResources(Me.LayoutControlItem35, "LayoutControlItem35")
        Me.LayoutControlItem35.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem35.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem35.Name = "LayoutControlItem35"
        Me.LayoutControlItem35.Size = New System.Drawing.Size(769, 181)
        Me.LayoutControlItem35.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem35.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem35.TextVisible = False
        '
        'LayoutControlItem31
        '
        Me.LayoutControlItem31.Control = Me.LabelControl3
        resources.ApplyResources(Me.LayoutControlItem31, "LayoutControlItem31")
        Me.LayoutControlItem31.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem31.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem31.MinSize = New System.Drawing.Size(1, 40)
        Me.LayoutControlItem31.Name = "LayoutControlItem31"
        Me.LayoutControlItem31.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem31.Size = New System.Drawing.Size(799, 40)
        Me.LayoutControlItem31.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem31.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem31.TextVisible = False
        '
        'PnlBodySearch
        '
        Me.PnlBodySearch.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PnlBodySearch.Appearance.Options.UseBackColor = True
        Me.PnlBodySearch.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlBodySearch.Controls.Add(Me.LayoutControl9)
        Me.PnlBodySearch.Controls.Add(Me.PccStayFolios)
        Me.PnlBodySearch.Controls.Add(Me.PccStayDetails)
        Me.PnlBodySearch.Controls.Add(Me.PccAdmission)
        Me.PnlBodySearch.Controls.Add(Me.CtrNoData1)
        Me.PnlBodySearch.Controls.Add(Me.PnlProgressPanel)
        resources.ApplyResources(Me.PnlBodySearch, "PnlBodySearch")
        Me.PnlBodySearch.Name = "PnlBodySearch"
        '
        'LayoutControl9
        '
        Me.LayoutControl9.AllowCustomization = False
        Me.LayoutControl9.Controls.Add(Me.INDGCExportExcel)
        resources.ApplyResources(Me.LayoutControl9, "LayoutControl9")
        Me.LayoutControl9.Name = "LayoutControl9"
        Me.LayoutControl9.Root = Me.LayoutControlGroup7
        '
        'INDGCExportExcel
        '
        Me.INDGCExportExcel.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.INDGCExportExcel, "INDGCExportExcel")
        Me.INDGCExportExcel.MainView = Me.GridView9
        Me.INDGCExportExcel.MenuManager = Me.BarManager
        Me.INDGCExportExcel.Name = "INDGCExportExcel"
        Me.INDGCExportExcel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView9})
        '
        'GridView9
        '
        Me.GridView9.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView9.Appearance.GroupRow.Font = CType(resources.GetObject("GridView9.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView9.Appearance.GroupRow.Options.UseFont = True
        Me.GridView9.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView9.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView9.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView9.Appearance.Row.Font = CType(resources.GetObject("GridView9.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView9.Appearance.Row.Options.UseFont = True
        Me.GridView9.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView9.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView9.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView9.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn50, Me.GridColumn51, Me.GridColumn52, Me.GridColumn53, Me.GridColumn54, Me.GridColumn55, Me.INDColSubtotal, Me.INDColDiscount, Me.INDColNetValue, Me.GridColumn56, Me.GridColumn57, Me.GridColumn58})
        Me.GridView9.GridControl = Me.INDGCExportExcel
        Me.GridView9.Name = "GridView9"
        Me.GridView9.OptionsDetail.ShowDetailTabs = False
        Me.GridView9.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView9.OptionsView.EnableAppearanceOddRow = True
        Me.GridView9.OptionsView.ShowDetailButtons = False
        Me.GridView9.OptionsView.ShowGroupPanel = False
        Me.GridView9.OptionsView.ShowIndicator = False
        '
        'GridColumn50
        '
        resources.ApplyResources(Me.GridColumn50, "GridColumn50")
        Me.GridColumn50.FieldName = "ServiceCode"
        Me.GridColumn50.MinWidth = 21
        Me.GridColumn50.Name = "GridColumn50"
        '
        'GridColumn51
        '
        resources.ApplyResources(Me.GridColumn51, "GridColumn51")
        Me.GridColumn51.FieldName = "ServiceName"
        Me.GridColumn51.MinWidth = 21
        Me.GridColumn51.Name = "GridColumn51"
        '
        'GridColumn52
        '
        resources.ApplyResources(Me.GridColumn52, "GridColumn52")
        Me.GridColumn52.FieldName = "ServiceDate"
        Me.GridColumn52.MinWidth = 21
        Me.GridColumn52.Name = "GridColumn52"
        '
        'GridColumn53
        '
        resources.ApplyResources(Me.GridColumn53, "GridColumn53")
        Me.GridColumn53.FieldName = "InvoiceQuantity"
        Me.GridColumn53.MinWidth = 21
        Me.GridColumn53.Name = "GridColumn53"
        '
        'GridColumn54
        '
        resources.ApplyResources(Me.GridColumn54, "GridColumn54")
        Me.GridColumn54.DisplayFormat.FormatString = "n2"
        Me.GridColumn54.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn54.FieldName = "GrossValue"
        Me.GridColumn54.MinWidth = 21
        Me.GridColumn54.Name = "GridColumn54"
        '
        'GridColumn55
        '
        resources.ApplyResources(Me.GridColumn55, "GridColumn55")
        Me.GridColumn55.DisplayFormat.FormatString = "n2"
        Me.GridColumn55.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn55.FieldName = "IvaPercentage"
        Me.GridColumn55.MinWidth = 21
        Me.GridColumn55.Name = "GridColumn55"
        '
        'INDColSubtotal
        '
        resources.ApplyResources(Me.INDColSubtotal, "INDColSubtotal")
        Me.INDColSubtotal.DisplayFormat.FormatString = "n2"
        Me.INDColSubtotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColSubtotal.FieldName = "SubTotalSalesPrice"
        Me.INDColSubtotal.MinWidth = 21
        Me.INDColSubtotal.Name = "INDColSubtotal"
        '
        'INDColDiscount
        '
        resources.ApplyResources(Me.INDColDiscount, "INDColDiscount")
        Me.INDColDiscount.DisplayFormat.FormatString = "n2"
        Me.INDColDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColDiscount.FieldName = "GrandTotalDiscount"
        Me.INDColDiscount.MinWidth = 21
        Me.INDColDiscount.Name = "INDColDiscount"
        '
        'INDColNetValue
        '
        resources.ApplyResources(Me.INDColNetValue, "INDColNetValue")
        Me.INDColNetValue.DisplayFormat.FormatString = "n2"
        Me.INDColNetValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColNetValue.FieldName = "INDColNetValue"
        Me.INDColNetValue.MinWidth = 21
        Me.INDColNetValue.Name = "INDColNetValue"
        Me.INDColNetValue.UnboundExpression = "[SubTotalSalesPrice] - [GrandTotalDiscount]"
        Me.INDColNetValue.UnboundType = DevExpress.Data.UnboundColumnType.[Decimal]
        '
        'GridColumn56
        '
        resources.ApplyResources(Me.GridColumn56, "GridColumn56")
        Me.GridColumn56.DisplayFormat.FormatString = "n2"
        Me.GridColumn56.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn56.FieldName = "GrandTotalTaxes"
        Me.GridColumn56.MinWidth = 21
        Me.GridColumn56.Name = "GridColumn56"
        '
        'GridColumn57
        '
        resources.ApplyResources(Me.GridColumn57, "GridColumn57")
        Me.GridColumn57.DisplayFormat.FormatString = "n2"
        Me.GridColumn57.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn57.FieldName = "GrandTotalSalesPrice"
        Me.GridColumn57.MinWidth = 21
        Me.GridColumn57.Name = "GridColumn57"
        '
        'GridColumn58
        '
        resources.ApplyResources(Me.GridColumn58, "GridColumn58")
        Me.GridColumn58.DisplayFormat.FormatString = "n2"
        Me.GridColumn58.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn58.FieldName = "SubTotalPatientSalesPrice"
        Me.GridColumn58.MinWidth = 21
        Me.GridColumn58.Name = "GridColumn58"
        '
        'LayoutControlGroup7
        '
        Me.LayoutControlGroup7.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup7.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup7.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup7.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup7.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup7.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup7.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup7.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup7.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup7.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup7.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup7, "LayoutControlGroup7")
        Me.LayoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup7.GroupBordersVisible = False
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup21})
        Me.LayoutControlGroup7.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup7.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(799, 305)
        Me.LayoutControlGroup7.TextVisible = False
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
        resources.ApplyResources(Me.LayoutControlGroup21, "LayoutControlGroup21")
        Me.LayoutControlGroup21.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem30})
        Me.LayoutControlGroup21.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup21.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup21.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup21.Size = New System.Drawing.Size(799, 305)
        Me.LayoutControlGroup21.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem30
        '
        Me.LayoutControlItem30.Control = Me.INDGCExportExcel
        resources.ApplyResources(Me.LayoutControlItem30, "LayoutControlItem30")
        Me.LayoutControlItem30.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem30.Name = "LayoutControlItem38"
        Me.LayoutControlItem30.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem30.Size = New System.Drawing.Size(793, 277)
        Me.LayoutControlItem30.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem30.TextVisible = False
        '
        'PccStayDetails
        '
        Me.PccStayDetails.Controls.Add(Me.LayoutControl1)
        resources.ApplyResources(Me.PccStayDetails, "PccStayDetails")
        Me.PccStayDetails.Name = "PccStayDetails"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomization = False
        Me.LayoutControl1.Controls.Add(Me.GdcStayDetails)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup2
        '
        'GdcStayDetails
        '
        Me.GdcStayDetails.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GdcStayDetails, "GdcStayDetails")
        Me.GdcStayDetails.MainView = Me.GdvStayDetails
        Me.GdcStayDetails.MenuManager = Me.BarManager
        Me.GdcStayDetails.Name = "GdcStayDetails"
        Me.GdcStayDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvStayDetails})
        '
        'GdvStayDetails
        '
        Me.GdvStayDetails.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvStayDetails.Appearance.GroupRow.Font = CType(resources.GetObject("GdvStayDetails.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GdvStayDetails.Appearance.GroupRow.Options.UseFont = True
        Me.GdvStayDetails.Appearance.HeaderPanel.Font = CType(resources.GetObject("GdvStayDetails.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GdvStayDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvStayDetails.Appearance.Row.Font = CType(resources.GetObject("GdvStayDetails.Appearance.Row.Font"), System.Drawing.Font)
        Me.GdvStayDetails.Appearance.Row.Options.UseFont = True
        Me.GdvStayDetails.Appearance.ViewCaption.Font = CType(resources.GetObject("GdvStayDetails.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GdvStayDetails.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvStayDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColEstLiqDetDate, Me.ColEstLiqDetCups, Me.ColEstLiqDetTotalUnits})
        Me.GdvStayDetails.GridControl = Me.GdcStayDetails
        Me.GdvStayDetails.Name = "GdvStayDetails"
        Me.GdvStayDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvStayDetails.OptionsView.EnableAppearanceOddRow = True
        Me.GdvStayDetails.OptionsView.ShowGroupPanel = False
        Me.GdvStayDetails.OptionsView.ShowIndicator = False
        '
        'ColEstLiqDetDate
        '
        Me.ColEstLiqDetDate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqDetDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqDetDate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqDetDate, "ColEstLiqDetDate")
        Me.ColEstLiqDetDate.FieldName = "GENLIQUIDA"
        Me.ColEstLiqDetDate.Name = "ColEstLiqDetDate"
        Me.ColEstLiqDetDate.OptionsColumn.AllowEdit = False
        Me.ColEstLiqDetDate.OptionsColumn.AllowFocus = False
        Me.ColEstLiqDetDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqDetDate.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqDetDate.OptionsFilter.AllowFilter = False
        '
        'ColEstLiqDetCups
        '
        Me.ColEstLiqDetCups.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqDetCups.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqDetCups.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqDetCups, "ColEstLiqDetCups")
        Me.ColEstLiqDetCups.FieldName = "CodeNameCups"
        Me.ColEstLiqDetCups.Name = "ColEstLiqDetCups"
        Me.ColEstLiqDetCups.OptionsColumn.AllowEdit = False
        Me.ColEstLiqDetCups.OptionsColumn.AllowFocus = False
        Me.ColEstLiqDetCups.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqDetCups.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqDetCups.OptionsFilter.AllowFilter = False
        '
        'ColEstLiqDetTotalUnits
        '
        Me.ColEstLiqDetTotalUnits.AppearanceCell.Options.UseTextOptions = True
        Me.ColEstLiqDetTotalUnits.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqDetTotalUnits.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColEstLiqDetTotalUnits.AppearanceHeader.Options.UseTextOptions = True
        Me.ColEstLiqDetTotalUnits.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColEstLiqDetTotalUnits.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColEstLiqDetTotalUnits, "ColEstLiqDetTotalUnits")
        Me.ColEstLiqDetTotalUnits.FieldName = "CANTIDADLIQ"
        Me.ColEstLiqDetTotalUnits.Name = "ColEstLiqDetTotalUnits"
        Me.ColEstLiqDetTotalUnits.OptionsColumn.AllowEdit = False
        Me.ColEstLiqDetTotalUnits.OptionsColumn.AllowFocus = False
        Me.ColEstLiqDetTotalUnits.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEstLiqDetTotalUnits.OptionsFilter.AllowAutoFilter = False
        Me.ColEstLiqDetTotalUnits.OptionsFilter.AllowFilter = False
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
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(453, 181)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup3.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup3, "LayoutControlGroup3")
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem37})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(453, 181)
        '
        'LayoutControlItem37
        '
        Me.LayoutControlItem37.Control = Me.GdcStayDetails
        resources.ApplyResources(Me.LayoutControlItem37, "LayoutControlItem37")
        Me.LayoutControlItem37.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem37.Name = "LayoutControlItem37"
        Me.LayoutControlItem37.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem37.Size = New System.Drawing.Size(447, 153)
        Me.LayoutControlItem37.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem37.TextVisible = False
        '
        'CtrNoData1
        '
        Me.CtrNoData1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.CtrNoData1.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.CtrNoData1, "CtrNoData1")
        Me.CtrNoData1.Name = "CtrNoData1"
        '
        'PnlProgressPanel
        '
        Me.PnlProgressPanel.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PnlProgressPanel.Appearance.Font = CType(resources.GetObject("PnlProgressPanel.Appearance.Font"), System.Drawing.Font)
        Me.PnlProgressPanel.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.PnlProgressPanel.Appearance.Options.UseBackColor = True
        Me.PnlProgressPanel.Appearance.Options.UseFont = True
        Me.PnlProgressPanel.Appearance.Options.UseForeColor = True
        Me.PnlProgressPanel.AppearanceCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PnlProgressPanel.AppearanceCaption.Font = CType(resources.GetObject("resource.Font"), System.Drawing.Font)
        Me.PnlProgressPanel.AppearanceCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.PnlProgressPanel.AppearanceCaption.Options.UseBackColor = True
        Me.PnlProgressPanel.AppearanceCaption.Options.UseFont = True
        Me.PnlProgressPanel.AppearanceCaption.Options.UseForeColor = True
        Me.PnlProgressPanel.AppearanceDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PnlProgressPanel.AppearanceDescription.Font = CType(resources.GetObject("resource.Font1"), System.Drawing.Font)
        Me.PnlProgressPanel.AppearanceDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.PnlProgressPanel.AppearanceDescription.Options.UseBackColor = True
        Me.PnlProgressPanel.AppearanceDescription.Options.UseFont = True
        Me.PnlProgressPanel.AppearanceDescription.Options.UseForeColor = True
        Me.PnlProgressPanel.AutoHeight = True
        Me.PnlProgressPanel.AutoWidth = True
        resources.ApplyResources(Me.PnlProgressPanel, "PnlProgressPanel")
        Me.PnlProgressPanel.ImageHorzOffset = 200
        Me.PnlProgressPanel.Name = "PnlProgressPanel"
        '
        'PnlBodyDashboard
        '
        Me.PnlBodyDashboard.Controls.Add(Me.hideContainerBottom)
        resources.ApplyResources(Me.PnlBodyDashboard, "PnlBodyDashboard")
        Me.PnlBodyDashboard.Name = "PnlBodyDashboard"
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.hideContainerBottom.Controls.Add(Me.PnlNotifications)
        resources.ApplyResources(Me.hideContainerBottom, "hideContainerBottom")
        Me.hideContainerBottom.Name = "hideContainerBottom"
        '
        'PnlNotifications
        '
        Me.PnlNotifications.Appearance.BackColor = System.Drawing.Color.White
        Me.PnlNotifications.Appearance.Font = CType(resources.GetObject("PnlNotifications.Appearance.Font"), System.Drawing.Font)
        Me.PnlNotifications.Appearance.ForeColor = System.Drawing.Color.White
        Me.PnlNotifications.Appearance.Options.UseBackColor = True
        Me.PnlNotifications.Appearance.Options.UseFont = True
        Me.PnlNotifications.Appearance.Options.UseForeColor = True
        Me.PnlNotifications.Appearance.Options.UseTextOptions = True
        Me.PnlNotifications.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PnlNotifications.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PnlNotifications.Controls.Add(Me.DockPanel1_Container)
        Me.PnlNotifications.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.PnlNotifications.ForeColor = System.Drawing.Color.White
        Me.PnlNotifications.ID = New System.Guid("dec38afa-357e-432e-a626-f95d12b5269e")
        resources.ApplyResources(Me.PnlNotifications, "PnlNotifications")
        Me.PnlNotifications.Name = "PnlNotifications"
        Me.PnlNotifications.Options.AllowDockAsTabbedDocument = False
        Me.PnlNotifications.Options.AllowDockFill = False
        Me.PnlNotifications.Options.AllowDockLeft = False
        Me.PnlNotifications.Options.AllowDockRight = False
        Me.PnlNotifications.Options.AllowDockTop = False
        Me.PnlNotifications.Options.AllowFloating = False
        Me.PnlNotifications.Options.FloatOnDblClick = False
        Me.PnlNotifications.Options.ShowCloseButton = False
        Me.PnlNotifications.Options.ShowMaximizeButton = False
        Me.PnlNotifications.OriginalSize = New System.Drawing.Size(200, 107)
        Me.PnlNotifications.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.PnlNotifications.SavedIndex = 0
        Me.PnlNotifications.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.GdcNotifications)
        resources.ApplyResources(Me.DockPanel1_Container, "DockPanel1_Container")
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        '
        'GdcNotifications
        '
        Me.GdcNotifications.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GdcNotifications, "GdcNotifications")
        Me.GdcNotifications.MainView = Me.GdvNotifications
        Me.GdcNotifications.MenuManager = Me.BarManager
        Me.GdcNotifications.Name = "GdcNotifications"
        Me.GdcNotifications.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GdvNotifications})
        '
        'GdvNotifications
        '
        Me.GdvNotifications.Appearance.EvenRow.BackColor = System.Drawing.Color.White
        Me.GdvNotifications.Appearance.EvenRow.Font = CType(resources.GetObject("GdvNotifications.Appearance.EvenRow.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.GdvNotifications.Appearance.EvenRow.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.EvenRow.Options.UseFont = True
        Me.GdvNotifications.Appearance.EvenRow.Options.UseForeColor = True
        Me.GdvNotifications.Appearance.FocusedRow.Font = CType(resources.GetObject("GdvNotifications.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.FocusedRow.Options.UseFont = True
        Me.GdvNotifications.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GdvNotifications.Appearance.GroupRow.Font = CType(resources.GetObject("GdvNotifications.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.GroupRow.Options.UseFont = True
        Me.GdvNotifications.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Gainsboro
        Me.GdvNotifications.Appearance.HeaderPanel.Font = CType(resources.GetObject("GdvNotifications.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.HeaderPanel.Options.UseFont = True
        Me.GdvNotifications.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.White
        Me.GdvNotifications.Appearance.HideSelectionRow.Font = CType(resources.GetObject("GdvNotifications.Appearance.HideSelectionRow.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black
        Me.GdvNotifications.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.HideSelectionRow.Options.UseFont = True
        Me.GdvNotifications.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.GdvNotifications.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GdvNotifications.Appearance.HorzLine.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.GdvNotifications.Appearance.OddRow.Font = CType(resources.GetObject("GdvNotifications.Appearance.OddRow.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.GdvNotifications.Appearance.OddRow.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.OddRow.Options.UseFont = True
        Me.GdvNotifications.Appearance.OddRow.Options.UseForeColor = True
        Me.GdvNotifications.Appearance.Preview.BackColor = System.Drawing.Color.White
        Me.GdvNotifications.Appearance.Preview.Font = CType(resources.GetObject("GdvNotifications.Appearance.Preview.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.Preview.ForeColor = System.Drawing.Color.Black
        Me.GdvNotifications.Appearance.Preview.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.Preview.Options.UseFont = True
        Me.GdvNotifications.Appearance.Preview.Options.UseForeColor = True
        Me.GdvNotifications.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.GdvNotifications.Appearance.Row.Font = CType(resources.GetObject("GdvNotifications.Appearance.Row.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.GdvNotifications.Appearance.Row.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.Row.Options.UseFont = True
        Me.GdvNotifications.Appearance.Row.Options.UseForeColor = True
        Me.GdvNotifications.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GdvNotifications.Appearance.VertLine.Options.UseBackColor = True
        Me.GdvNotifications.Appearance.ViewCaption.Font = CType(resources.GetObject("GdvNotifications.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GdvNotifications.Appearance.ViewCaption.Options.UseFont = True
        Me.GdvNotifications.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColNotiIcon, Me.ColNotiTitle, Me.ColNotiMessage})
        Me.GdvNotifications.GridControl = Me.GdcNotifications
        Me.GdvNotifications.Name = "GdvNotifications"
        Me.GdvNotifications.OptionsView.EnableAppearanceEvenRow = True
        Me.GdvNotifications.OptionsView.EnableAppearanceOddRow = True
        Me.GdvNotifications.OptionsView.ShowGroupPanel = False
        Me.GdvNotifications.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[True]
        Me.GdvNotifications.OptionsView.ShowIndicator = False
        Me.GdvNotifications.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'ColNotiIcon
        '
        Me.ColNotiIcon.AppearanceCell.Options.UseTextOptions = True
        Me.ColNotiIcon.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColNotiIcon.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColNotiIcon.AppearanceHeader.Options.UseTextOptions = True
        Me.ColNotiIcon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColNotiIcon.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColNotiIcon, "ColNotiIcon")
        Me.ColNotiIcon.FieldName = "Icon"
        Me.ColNotiIcon.Name = "ColNotiIcon"
        Me.ColNotiIcon.OptionsColumn.AllowEdit = False
        Me.ColNotiIcon.OptionsColumn.AllowFocus = False
        Me.ColNotiIcon.OptionsColumn.AllowMove = False
        Me.ColNotiIcon.OptionsColumn.AllowSize = False
        Me.ColNotiIcon.OptionsColumn.FixedWidth = True
        Me.ColNotiIcon.OptionsColumn.ReadOnly = True
        Me.ColNotiIcon.OptionsFilter.AllowAutoFilter = False
        Me.ColNotiIcon.OptionsFilter.AllowFilter = False
        '
        'ColNotiTitle
        '
        Me.ColNotiTitle.AppearanceHeader.Options.UseTextOptions = True
        Me.ColNotiTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColNotiTitle.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColNotiTitle, "ColNotiTitle")
        Me.ColNotiTitle.FieldName = "Title"
        Me.ColNotiTitle.MinWidth = 100
        Me.ColNotiTitle.Name = "ColNotiTitle"
        Me.ColNotiTitle.OptionsColumn.AllowEdit = False
        Me.ColNotiTitle.OptionsColumn.AllowFocus = False
        Me.ColNotiTitle.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColNotiTitle.OptionsColumn.AllowMove = False
        Me.ColNotiTitle.OptionsColumn.ReadOnly = True
        Me.ColNotiTitle.OptionsFilter.AllowAutoFilter = False
        Me.ColNotiTitle.OptionsFilter.AllowFilter = False
        '
        'ColNotiMessage
        '
        Me.ColNotiMessage.AppearanceHeader.Options.UseTextOptions = True
        Me.ColNotiMessage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColNotiMessage.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.ColNotiMessage, "ColNotiMessage")
        Me.ColNotiMessage.FieldName = "Message"
        Me.ColNotiMessage.MinWidth = 100
        Me.ColNotiMessage.Name = "ColNotiMessage"
        Me.ColNotiMessage.OptionsColumn.AllowEdit = False
        Me.ColNotiMessage.OptionsColumn.AllowFocus = False
        Me.ColNotiMessage.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColNotiMessage.OptionsColumn.AllowMove = False
        Me.ColNotiMessage.OptionsColumn.ReadOnly = True
        Me.ColNotiMessage.OptionsFilter.AllowAutoFilter = False
        Me.ColNotiMessage.OptionsFilter.AllowFilter = False
        '
        'PnlCtrlHeader
        '
        Me.PnlCtrlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PnlCtrlHeader.Controls.Add(Me.LycHeader)
        Me.PnlCtrlHeader.Controls.Add(Me.PanelControl2)
        resources.ApplyResources(Me.PnlCtrlHeader, "PnlCtrlHeader")
        Me.PnlCtrlHeader.Name = "PnlCtrlHeader"
        '
        'PanelControl2
        '
        Me.PanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl2.Controls.Add(Me.LayoutControl3)
        resources.ApplyResources(Me.PanelControl2, "PanelControl2")
        Me.PanelControl2.Name = "PanelControl2"
        '
        'LayoutControl3
        '
        Me.LayoutControl3.Controls.Add(Me.MarqueeProgressBarControl1)
        resources.ApplyResources(Me.LayoutControl3, "LayoutControl3")
        Me.LayoutControl3.Name = "LayoutControl3"
        Me.LayoutControl3.Root = Me.LayoutControlGroup6
        '
        'MarqueeProgressBarControl1
        '
        resources.ApplyResources(Me.MarqueeProgressBarControl1, "MarqueeProgressBarControl1")
        Me.MarqueeProgressBarControl1.MenuManager = Me.BarManager1
        Me.MarqueeProgressBarControl1.Name = "MarqueeProgressBarControl1"
        Me.MarqueeProgressBarControl1.StyleController = Me.LayoutControl3
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.DockControls.Add(Me.BarDockControl2)
        Me.BarManager1.DockControls.Add(Me.BarDockControl3)
        Me.BarManager1.DockControls.Add(Me.BarDockControl4)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem5, Me.BarButtonItem1, Me.BarButtonItem6, Me.BarButtonItem7, Me.BarButtonItem13, Me.BarButtonItem12, Me.BarSubItem1, Me.BarButtonItem14, Me.BarButtonItem9, Me.BarButtonItem11, Me.BarSubItem2, Me.BarButtonItem15, Me.BarButtonItem16, Me.BarButtonItem3, Me.BarButtonItem2, Me.BarButtonItem4, Me.BarButtonItem10, Me.BarButtonItem8, Me.BarButtonItem17})
        Me.BarManager1.MaxItemId = 23
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        resources.ApplyResources(Me.BarDockControl1, "BarDockControl1")
        Me.BarDockControl1.Manager = Me.BarManager1
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        resources.ApplyResources(Me.BarDockControl2, "BarDockControl2")
        Me.BarDockControl2.Manager = Me.BarManager1
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        resources.ApplyResources(Me.BarDockControl3, "BarDockControl3")
        Me.BarDockControl3.Manager = Me.BarManager1
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        resources.ApplyResources(Me.BarDockControl4, "BarDockControl4")
        Me.BarDockControl4.Manager = Me.BarManager1
        '
        'BarButtonItem5
        '
        resources.ApplyResources(Me.BarButtonItem5, "BarButtonItem5")
        Me.BarButtonItem5.Id = 0
        Me.BarButtonItem5.ImageOptions.Image = CType(resources.GetObject("BarButtonItem5.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem5.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem5.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem5.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem5.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem5.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem5.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem5.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem5.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem5.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem5.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem5.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem5.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem5.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem5.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem5.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem5.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem5.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem5.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem5.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem5.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem5.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem5.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem5.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem5.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem5.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'BarButtonItem1
        '
        resources.ApplyResources(Me.BarButtonItem1, "BarButtonItem1")
        Me.BarButtonItem1.Id = 1
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem1.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem1.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem1.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem1.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem1.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem1.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem1.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem1.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem1.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem1.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem1.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem1.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem1.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem1.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem1.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem1.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem1.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem1.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem1.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem1.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem1.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem1.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem1.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem1.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B))
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem6
        '
        resources.ApplyResources(Me.BarButtonItem6, "BarButtonItem6")
        Me.BarButtonItem6.Id = 2
        Me.BarButtonItem6.ImageOptions.Image = CType(resources.GetObject("BarButtonItem6.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem6.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem6.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem6.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem6.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem6.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem6.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem6.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem6.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem6.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem6.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem6.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem6.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem6.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem6.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem6.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem6.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem6.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem6.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem6.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem6.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem6.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem6.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem6.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem6.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem6.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
        Me.BarButtonItem6.Name = "BarButtonItem6"
        '
        'BarButtonItem7
        '
        resources.ApplyResources(Me.BarButtonItem7, "BarButtonItem7")
        Me.BarButtonItem7.Id = 3
        Me.BarButtonItem7.ImageOptions.Image = CType(resources.GetObject("BarButtonItem7.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem7.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem7.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem7.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem7.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem7.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem7.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem7.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem7.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem7.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem7.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem7.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem7.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem7.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem7.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem7.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem7.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem7.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem7.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem7.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem7.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem7.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem7.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem7.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem7.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem7.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
        Me.BarButtonItem7.Name = "BarButtonItem7"
        '
        'BarButtonItem13
        '
        resources.ApplyResources(Me.BarButtonItem13, "BarButtonItem13")
        Me.BarButtonItem13.Id = 4
        Me.BarButtonItem13.ImageOptions.Image = CType(resources.GetObject("BarButtonItem13.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem13.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem13.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem13.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem13.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem13.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem13.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem13.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem13.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem13.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem13.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem13.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem13.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem13.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem13.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem13.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem13.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem13.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem13.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem13.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem13.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem13.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem13.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem13.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem13.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem13.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), (System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.BarButtonItem13.Name = "BarButtonItem13"
        '
        'BarButtonItem12
        '
        resources.ApplyResources(Me.BarButtonItem12, "BarButtonItem12")
        Me.BarButtonItem12.Id = 5
        Me.BarButtonItem12.ImageOptions.Image = CType(resources.GetObject("BarButtonItem12.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem12.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem12.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem12.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem12.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem12.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem12.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem12.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem12.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem12.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem12.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem12.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem12.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem12.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem12.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem12.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem12.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem12.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem12.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem12.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem12.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem12.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem12.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem12.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem12.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem12.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
        Me.BarButtonItem12.Name = "BarButtonItem12"
        '
        'BarSubItem1
        '
        resources.ApplyResources(Me.BarSubItem1, "BarSubItem1")
        Me.BarSubItem1.Id = 7
        Me.BarSubItem1.ImageOptions.Image = CType(resources.GetObject("BarSubItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarSubItem1.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarSubItem1.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarSubItem1.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarSubItem1.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarSubItem1.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarSubItem1.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarSubItem1.ItemAppearance.Normal.Font = CType(resources.GetObject("BarSubItem1.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarSubItem1.ItemAppearance.Normal.Options.UseFont = True
        Me.BarSubItem1.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarSubItem1.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarSubItem1.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarSubItem1.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarSubItem1.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarSubItem1.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarSubItem1.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarSubItem1.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarSubItem1.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarSubItem1.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarSubItem1.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarSubItem1.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarSubItem1.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarSubItem1.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarSubItem1.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Caption, Me.BarButtonItem13, "Imprimir Tirilla"), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem14)})
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'BarButtonItem14
        '
        resources.ApplyResources(Me.BarButtonItem14, "BarButtonItem14")
        Me.BarButtonItem14.Id = 8
        Me.BarButtonItem14.ImageOptions.Image = CType(resources.GetObject("BarButtonItem14.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem14.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem14.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem14.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem14.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem14.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem14.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem14.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem14.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem14.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem14.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem14.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem14.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem14.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem14.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem14.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem14.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem14.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem14.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem14.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem14.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem14.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem14.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem14.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem14.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem14.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), (System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
        Me.BarButtonItem14.Name = "BarButtonItem14"
        '
        'BarButtonItem9
        '
        resources.ApplyResources(Me.BarButtonItem9, "BarButtonItem9")
        Me.BarButtonItem9.Id = 9
        Me.BarButtonItem9.ImageOptions.Image = CType(resources.GetObject("BarButtonItem9.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem9.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem9.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem9.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem9.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem9.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem9.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem9.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem9.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem9.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem9.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem9.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem9.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem9.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem9.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem9.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem9.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem9.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem9.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem9.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem9.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem9.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem9.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem9.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem9.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem9.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
        Me.BarButtonItem9.Name = "BarButtonItem9"
        '
        'BarButtonItem11
        '
        resources.ApplyResources(Me.BarButtonItem11, "BarButtonItem11")
        Me.BarButtonItem11.Id = 11
        Me.BarButtonItem11.ImageOptions.Image = CType(resources.GetObject("BarButtonItem11.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem11.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem11.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem11.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem11.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem11.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem11.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem11.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem11.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem11.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem11.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem11.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem11.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem11.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem11.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem11.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem11.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem11.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem11.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem11.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem11.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem11.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem11.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem11.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem11.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem11.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
        Me.BarButtonItem11.Name = "BarButtonItem11"
        '
        'BarSubItem2
        '
        resources.ApplyResources(Me.BarSubItem2, "BarSubItem2")
        Me.BarSubItem2.Id = 14
        Me.BarSubItem2.ImageOptions.Image = CType(resources.GetObject("BarSubItem2.ImageOptions.Image"), System.Drawing.Image)
        Me.BarSubItem2.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarSubItem2.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarSubItem2.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarSubItem2.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarSubItem2.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarSubItem2.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarSubItem2.ItemAppearance.Normal.Font = CType(resources.GetObject("BarSubItem2.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarSubItem2.ItemAppearance.Normal.Options.UseFont = True
        Me.BarSubItem2.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarSubItem2.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarSubItem2.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarSubItem2.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarSubItem2.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarSubItem2.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarSubItem2.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarSubItem2.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarSubItem2.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarSubItem2.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarSubItem2.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarSubItem2.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarSubItem2.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarSubItem2.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarSubItem2.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarSubItem2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem15), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem16)})
        Me.BarSubItem2.Name = "BarSubItem2"
        '
        'BarButtonItem15
        '
        resources.ApplyResources(Me.BarButtonItem15, "BarButtonItem15")
        Me.BarButtonItem15.Id = 15
        Me.BarButtonItem15.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem15.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem15.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem15.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem15.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem15.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem15.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem15.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem15.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem15.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem15.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem15.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem15.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem15.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem15.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem15.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem15.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem15.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem15.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem15.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem15.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem15.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem15.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem15.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem15.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.F))
        Me.BarButtonItem15.Name = "BarButtonItem15"
        '
        'BarButtonItem16
        '
        resources.ApplyResources(Me.BarButtonItem16, "BarButtonItem16")
        Me.BarButtonItem16.Id = 16
        Me.BarButtonItem16.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem16.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem16.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem16.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem16.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem16.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem16.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem16.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem16.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem16.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem16.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem16.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem16.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem16.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem16.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem16.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem16.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem16.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem16.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem16.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem16.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem16.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem16.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem16.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem16.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.N))
        Me.BarButtonItem16.Name = "BarButtonItem16"
        '
        'BarButtonItem3
        '
        resources.ApplyResources(Me.BarButtonItem3, "BarButtonItem3")
        Me.BarButtonItem3.Id = 17
        Me.BarButtonItem3.ImageOptions.Image = CType(resources.GetObject("BarButtonItem3.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem3.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem3.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem3.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem3.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem3.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem3.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem3.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem3.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem3.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem3.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem3.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem3.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem3.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.F))
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'BarButtonItem2
        '
        resources.ApplyResources(Me.BarButtonItem2, "BarButtonItem2")
        Me.BarButtonItem2.Id = 18
        Me.BarButtonItem2.ImageOptions.Image = CType(resources.GetObject("BarButtonItem2.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem2.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem2.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem2.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem2.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem2.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem2.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem2.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem2.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem2.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem2.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem2.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem2.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem2.ItemShortcut = New DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                Or System.Windows.Forms.Keys.A))
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem4
        '
        resources.ApplyResources(Me.BarButtonItem4, "BarButtonItem4")
        Me.BarButtonItem4.Id = 19
        Me.BarButtonItem4.ImageOptions.Image = CType(resources.GetObject("BarButtonItem4.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem4.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem4.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem4.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem4.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem4.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem4.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem4.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem4.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem4.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem4.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem4.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem4.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem4.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem4.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem4.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem4.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem4.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem4.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem4.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem4.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem4.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem4.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem4.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem4.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'BarButtonItem10
        '
        resources.ApplyResources(Me.BarButtonItem10, "BarButtonItem10")
        Me.BarButtonItem10.Id = 20
        Me.BarButtonItem10.ImageOptions.Image = CType(resources.GetObject("BarButtonItem10.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem10.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem10.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem10.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem10.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem10.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem10.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem10.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem10.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem10.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem10.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem10.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem10.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem10.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem10.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem10.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem10.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem10.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem10.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem10.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem10.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem10.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem10.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem10.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem10.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem10.Name = "BarButtonItem10"
        '
        'BarButtonItem8
        '
        resources.ApplyResources(Me.BarButtonItem8, "BarButtonItem8")
        Me.BarButtonItem8.Id = 21
        Me.BarButtonItem8.ImageOptions.Image = CType(resources.GetObject("BarButtonItem8.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem8.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem8.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem8.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem8.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem8.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem8.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem8.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem8.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem8.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem8.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem8.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem8.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem8.ItemInMenuAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem8.ItemInMenuAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem8.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem8.ItemInMenuAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem8.ItemInMenuAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem8.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem8.ItemInMenuAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem8.ItemInMenuAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem8.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem8.ItemInMenuAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem8.ItemInMenuAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem8.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem8.Name = "BarButtonItem8"
        '
        'BarButtonItem17
        '
        resources.ApplyResources(Me.BarButtonItem17, "BarButtonItem17")
        Me.BarButtonItem17.Id = 22
        Me.BarButtonItem17.ItemAppearance.Disabled.Font = CType(resources.GetObject("BarButtonItem17.ItemAppearance.Disabled.Font"), System.Drawing.Font)
        Me.BarButtonItem17.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem17.ItemAppearance.Hovered.Font = CType(resources.GetObject("BarButtonItem17.ItemAppearance.Hovered.Font"), System.Drawing.Font)
        Me.BarButtonItem17.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem17.ItemAppearance.Normal.Font = CType(resources.GetObject("BarButtonItem17.ItemAppearance.Normal.Font"), System.Drawing.Font)
        Me.BarButtonItem17.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem17.ItemAppearance.Pressed.Font = CType(resources.GetObject("BarButtonItem17.ItemAppearance.Pressed.Font"), System.Drawing.Font)
        Me.BarButtonItem17.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem17.Name = "BarButtonItem17"
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup6.GroupBordersVisible = False
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LyciBusyIndicator})
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(1177, 16)
        Me.LayoutControlGroup6.TextVisible = False
        '
        'LyciBusyIndicator
        '
        Me.LyciBusyIndicator.Control = Me.MarqueeProgressBarControl1
        resources.ApplyResources(Me.LyciBusyIndicator, "LyciBusyIndicator")
        Me.LyciBusyIndicator.Location = New System.Drawing.Point(0, 0)
        Me.LyciBusyIndicator.MaxSize = New System.Drawing.Size(0, 10)
        Me.LyciBusyIndicator.MinSize = New System.Drawing.Size(1, 10)
        Me.LyciBusyIndicator.Name = "LyciBusyIndicator"
        Me.LyciBusyIndicator.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LyciBusyIndicator.Size = New System.Drawing.Size(1177, 16)
        Me.LyciBusyIndicator.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LyciBusyIndicator.TextSize = New System.Drawing.Size(0, 0)
        Me.LyciBusyIndicator.TextVisible = False
        Me.LyciBusyIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'DocumentManager
        '
        Me.DocumentManager.ContainerControl = Me.PnlBodyDashboard
        Me.DocumentManager.MenuManager = Me.BarManager
        Me.DocumentManager.ShowThumbnailsInTaskBar = DevExpress.Utils.DefaultBoolean.[False]
        Me.DocumentManager.View = Me.WidgetView
        Me.DocumentManager.ViewCollection.AddRange(New DevExpress.XtraBars.Docking2010.Views.BaseView() {Me.WidgetView})
        '
        'WidgetView
        '
        Me.WidgetView.Appearance.BackColor = System.Drawing.Color.White
        Me.WidgetView.Appearance.Options.UseBackColor = True
        Me.WidgetView.AppearanceActiveDocumentCaption.Font = CType(resources.GetObject("WidgetView.AppearanceActiveDocumentCaption.Font"), System.Drawing.Font)
        Me.WidgetView.AppearanceActiveDocumentCaption.ForeColor = System.Drawing.Color.Black
        Me.WidgetView.AppearanceActiveDocumentCaption.Options.UseFont = True
        Me.WidgetView.AppearanceActiveDocumentCaption.Options.UseForeColor = True
        Me.WidgetView.AppearanceDocumentCaption.Font = CType(resources.GetObject("WidgetView.AppearanceDocumentCaption.Font"), System.Drawing.Font)
        Me.WidgetView.AppearanceDocumentCaption.ForeColor = System.Drawing.Color.Black
        Me.WidgetView.AppearanceDocumentCaption.Options.UseFont = True
        Me.WidgetView.AppearanceDocumentCaption.Options.UseForeColor = True
        Me.WidgetView.DocumentProperties.AllowResize = False
        Me.WidgetView.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.WidgetView.StackGroups.AddRange(New DevExpress.XtraBars.Docking2010.Views.Widget.StackGroup() {Me.StackGroup1})
        '
        'DockManager
        '
        Me.DockManager.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.DockManager.Form = Me.PnlBodyDashboard
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane"})
        '
        'PanelControl3
        '
        Me.PanelControl3.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PanelControl3.Appearance.Options.UseBackColor = True
        Me.PanelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl3.Controls.Add(Me.PopupContainerControl1)
        Me.PanelControl3.Controls.Add(Me.PopupContainerControl2)
        Me.PanelControl3.Controls.Add(Me.PopupContainerControl3)
        resources.ApplyResources(Me.PanelControl3, "PanelControl3")
        Me.PanelControl3.Name = "PanelControl3"
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Controls.Add(Me.LayoutControl4)
        resources.ApplyResources(Me.PopupContainerControl1, "PopupContainerControl1")
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        '
        'LayoutControl4
        '
        Me.LayoutControl4.AllowCustomization = False
        Me.LayoutControl4.Controls.Add(Me.GridControl1)
        resources.ApplyResources(Me.LayoutControl4, "LayoutControl4")
        Me.LayoutControl4.Name = "LayoutControl4"
        Me.LayoutControl4.Root = Me.LayoutControlGroup8
        '
        'GridControl1
        '
        Me.GridControl1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GridControl1, "GridControl1")
        Me.GridControl1.MainView = Me.GridView1
        Me.GridControl1.MenuManager = Me.BarManager1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView1.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsDetail.ShowDetailTabs = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowDetailButtons = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.OptionsView.ShowIndicator = False
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "NumberFolio"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.FixedWidth = True
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn1.OptionsFilter.AllowFilter = False
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.DisplayFormat.FormatString = "C2"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn2.FieldName = "ValueFolio"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn2.OptionsFilter.AllowFilter = False
        '
        'LayoutControlGroup8
        '
        Me.LayoutControlGroup8.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup8.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup8.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup8.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup8.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup8.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup8.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup8.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup8.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup8.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup8.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup8, "LayoutControlGroup8")
        Me.LayoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup8.GroupBordersVisible = False
        Me.LayoutControlGroup8.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup9})
        Me.LayoutControlGroup8.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup8.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup8.Size = New System.Drawing.Size(250, 181)
        Me.LayoutControlGroup8.TextVisible = False
        '
        'LayoutControlGroup9
        '
        Me.LayoutControlGroup9.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup9.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup9.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup9.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup9.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup9.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup9.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup9.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup9.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup9.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup9.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup9, "LayoutControlGroup9")
        Me.LayoutControlGroup9.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup9.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup9.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup9.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup9.Size = New System.Drawing.Size(250, 181)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.GridControl1
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem38"
        Me.LayoutControlItem1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem1.Size = New System.Drawing.Size(244, 153)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'PopupContainerControl2
        '
        Me.PopupContainerControl2.Controls.Add(Me.LayoutControl5)
        resources.ApplyResources(Me.PopupContainerControl2, "PopupContainerControl2")
        Me.PopupContainerControl2.Name = "PopupContainerControl2"
        '
        'LayoutControl5
        '
        Me.LayoutControl5.AllowCustomization = False
        Me.LayoutControl5.Controls.Add(Me.GridControl2)
        resources.ApplyResources(Me.LayoutControl5, "LayoutControl5")
        Me.LayoutControl5.Name = "LayoutControl5"
        Me.LayoutControl5.Root = Me.LayoutControlGroup10
        '
        'GridControl2
        '
        Me.GridControl2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GridControl2, "GridControl2")
        Me.GridControl2.MainView = Me.GridView2
        Me.GridControl2.MenuManager = Me.BarManager1
        Me.GridControl2.Name = "GridControl2"
        Me.GridControl2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView2.Appearance.GroupRow.Font = CType(resources.GetObject("GridView2.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView2.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = CType(resources.GetObject("GridView2.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView2.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.GridView2.GridControl = Me.GridControl2
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.GridView2.OptionsView.ShowIndicator = False
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "GENLIQUIDA"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn3.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn3.OptionsFilter.AllowFilter = False
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "CodeNameCups"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn4.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn4.OptionsFilter.AllowFilter = False
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.FieldName = "CANTIDADLIQ"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn5.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn5.OptionsFilter.AllowFilter = False
        '
        'LayoutControlGroup10
        '
        Me.LayoutControlGroup10.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup10.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup10.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup10.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup10.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup10.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup10.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup10.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup10.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup10.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup10.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup10, "LayoutControlGroup10")
        Me.LayoutControlGroup10.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup10.GroupBordersVisible = False
        Me.LayoutControlGroup10.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup11})
        Me.LayoutControlGroup10.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup10.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup10.Size = New System.Drawing.Size(453, 181)
        Me.LayoutControlGroup10.TextVisible = False
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
        resources.ApplyResources(Me.LayoutControlGroup11, "LayoutControlGroup11")
        Me.LayoutControlGroup11.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem28})
        Me.LayoutControlGroup11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup11.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup11.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup11.Size = New System.Drawing.Size(453, 181)
        '
        'LayoutControlItem28
        '
        Me.LayoutControlItem28.Control = Me.GridControl2
        resources.ApplyResources(Me.LayoutControlItem28, "LayoutControlItem28")
        Me.LayoutControlItem28.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem28.Name = "LayoutControlItem37"
        Me.LayoutControlItem28.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem28.Size = New System.Drawing.Size(447, 153)
        Me.LayoutControlItem28.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem28.TextVisible = False
        '
        'PopupContainerControl3
        '
        Me.PopupContainerControl3.Controls.Add(Me.LayoutControl6)
        resources.ApplyResources(Me.PopupContainerControl3, "PopupContainerControl3")
        Me.PopupContainerControl3.Name = "PopupContainerControl3"
        '
        'LayoutControl6
        '
        Me.LayoutControl6.AllowCustomization = False
        Me.LayoutControl6.Controls.Add(Me.LabelControl1)
        Me.LayoutControl6.Controls.Add(Me.TextEdit1)
        Me.LayoutControl6.Controls.Add(Me.TextEdit2)
        Me.LayoutControl6.Controls.Add(Me.TextEdit3)
        Me.LayoutControl6.Controls.Add(Me.TextEdit4)
        Me.LayoutControl6.Controls.Add(Me.TextEdit5)
        Me.LayoutControl6.Controls.Add(Me.TextEdit6)
        Me.LayoutControl6.Controls.Add(Me.TextEdit7)
        Me.LayoutControl6.Controls.Add(Me.ImageComboBoxEdit1)
        Me.LayoutControl6.Controls.Add(Me.ImageComboBoxEdit2)
        Me.LayoutControl6.Controls.Add(Me.ButtonEdit1)
        Me.LayoutControl6.Controls.Add(Me.GridControl3)
        Me.LayoutControl6.Controls.Add(Me.TextEdit8)
        Me.LayoutControl6.Controls.Add(Me.TextEdit9)
        Me.LayoutControl6.Controls.Add(Me.TextEdit10)
        Me.LayoutControl6.Controls.Add(Me.TextEdit11)
        Me.LayoutControl6.Controls.Add(Me.TextEdit12)
        Me.LayoutControl6.Controls.Add(Me.TextEdit13)
        Me.LayoutControl6.Controls.Add(Me.TextEdit14)
        Me.LayoutControl6.Controls.Add(Me.TextEdit15)
        Me.LayoutControl6.Controls.Add(Me.TextEdit16)
        Me.LayoutControl6.Controls.Add(Me.TextEdit17)
        Me.LayoutControl6.Controls.Add(Me.TextEdit18)
        Me.LayoutControl6.Controls.Add(Me.ImageComboBoxEdit3)
        Me.LayoutControl6.Controls.Add(Me.ImageComboBoxEdit4)
        Me.LayoutControl6.Controls.Add(Me.ButtonEdit2)
        Me.LayoutControl6.Controls.Add(Me.GridControl4)
        resources.ApplyResources(Me.LayoutControl6, "LayoutControl6")
        Me.LayoutControl6.Name = "LayoutControl6"
        Me.LayoutControl6.Root = Me.LayoutControlGroup12
        '
        'LabelControl1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl1, True)
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LabelControl1.Appearance.Font = CType(resources.GetObject("LabelControl1.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl1.Appearance.Options.UseBackColor = True
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me.LabelControl1, "LabelControl1")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl1, False)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.StyleController = Me.LayoutControl6
        '
        'TextEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit1, False)
        resources.ApplyResources(Me.TextEdit1, "TextEdit1")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit1.MenuManager = Me.BarManager1
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Properties.Appearance.Font = CType(resources.GetObject("TextEdit1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit1.Properties.Appearance.Options.UseFont = True
        Me.TextEdit1.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit1.Properties.ReadOnly = True
        Me.TextEdit1.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit1, 0)
        '
        'TextEdit2
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit2, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit2, False)
        resources.ApplyResources(Me.TextEdit2, "TextEdit2")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit2, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit2.MenuManager = Me.BarManager1
        Me.TextEdit2.Name = "TextEdit2"
        Me.TextEdit2.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit2.Properties.Appearance.Font = CType(resources.GetObject("TextEdit2.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit2.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit2.Properties.Appearance.Options.UseFont = True
        Me.TextEdit2.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit2.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit2.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit2.Properties.ReadOnly = True
        Me.TextEdit2.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit2, 0)
        '
        'TextEdit3
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit3, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit3, False)
        resources.ApplyResources(Me.TextEdit3, "TextEdit3")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit3, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit3.MenuManager = Me.BarManager1
        Me.TextEdit3.Name = "TextEdit3"
        Me.TextEdit3.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit3.Properties.Appearance.Font = CType(resources.GetObject("TextEdit3.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit3.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit3.Properties.Appearance.Options.UseFont = True
        Me.TextEdit3.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit3.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit3.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit3.Properties.ReadOnly = True
        Me.TextEdit3.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit3, 0)
        '
        'TextEdit4
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit4, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit4, False)
        resources.ApplyResources(Me.TextEdit4, "TextEdit4")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit4, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit4.MenuManager = Me.BarManager1
        Me.TextEdit4.Name = "TextEdit4"
        Me.TextEdit4.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit4.Properties.Appearance.Font = CType(resources.GetObject("TextEdit4.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit4.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit4.Properties.Appearance.Options.UseFont = True
        Me.TextEdit4.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit4.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit4.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit4.Properties.ReadOnly = True
        Me.TextEdit4.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit4, 0)
        '
        'TextEdit5
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit5, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit5, False)
        resources.ApplyResources(Me.TextEdit5, "TextEdit5")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit5, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit5.MenuManager = Me.BarManager1
        Me.TextEdit5.Name = "TextEdit5"
        Me.TextEdit5.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit5.Properties.Appearance.Font = CType(resources.GetObject("TextEdit5.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit5.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit5.Properties.Appearance.Options.UseFont = True
        Me.TextEdit5.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit5.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit5.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit5.Properties.ReadOnly = True
        Me.TextEdit5.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit5, 0)
        '
        'TextEdit6
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit6, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit6, False)
        resources.ApplyResources(Me.TextEdit6, "TextEdit6")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit6, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit6.MenuManager = Me.BarManager1
        Me.TextEdit6.Name = "TextEdit6"
        Me.TextEdit6.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit6.Properties.Appearance.Font = CType(resources.GetObject("TextEdit6.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit6.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit6.Properties.Appearance.Options.UseFont = True
        Me.TextEdit6.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit6.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit6.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit6.Properties.ReadOnly = True
        Me.TextEdit6.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit6, 0)
        '
        'TextEdit7
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit7, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit7, False)
        resources.ApplyResources(Me.TextEdit7, "TextEdit7")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit7, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit7.MenuManager = Me.BarManager1
        Me.TextEdit7.Name = "TextEdit7"
        Me.TextEdit7.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit7.Properties.Appearance.Font = CType(resources.GetObject("TextEdit7.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit7.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit7.Properties.Appearance.Options.UseFont = True
        Me.TextEdit7.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit7.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit7.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit7.Properties.ReadOnly = True
        Me.TextEdit7.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit7, 0)
        '
        'ImageComboBoxEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.ImageComboBoxEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ImageComboBoxEdit1, False)
        resources.ApplyResources(Me.ImageComboBoxEdit1, "ImageComboBoxEdit1")
        Me.IndigoTextEdit1.SetMascara(Me.ImageComboBoxEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ImageComboBoxEdit1.MenuManager = Me.BarManager1
        Me.ImageComboBoxEdit1.Name = "ImageComboBoxEdit1"
        Me.ImageComboBoxEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.ImageComboBoxEdit1.Properties.Appearance.Font = CType(resources.GetObject("ImageComboBoxEdit1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ImageComboBoxEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.ImageComboBoxEdit1.Properties.Appearance.Options.UseFont = True
        Me.ImageComboBoxEdit1.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit1.Properties.Items"), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items1"), Object), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit1.Properties.Items3"), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items4"), Object), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit1.Properties.Items6"), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items7"), Object), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit1.Properties.Items9"), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items10"), Object), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items11"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit1.Properties.Items12"), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items13"), Object), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items14"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit1.Properties.Items15"), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items16"), Object), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items17"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit1.Properties.Items18"), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items19"), Object), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items20"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit1.Properties.Items21"), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items22"), Object), CType(resources.GetObject("ImageComboBoxEdit1.Properties.Items23"), Integer))})
        Me.ImageComboBoxEdit1.Properties.ReadOnly = True
        Me.ImageComboBoxEdit1.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ImageComboBoxEdit1, 0)
        '
        'ImageComboBoxEdit2
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.ImageComboBoxEdit2, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ImageComboBoxEdit2, False)
        resources.ApplyResources(Me.ImageComboBoxEdit2, "ImageComboBoxEdit2")
        Me.IndigoTextEdit1.SetMascara(Me.ImageComboBoxEdit2, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ImageComboBoxEdit2.MenuManager = Me.BarManager1
        Me.ImageComboBoxEdit2.Name = "ImageComboBoxEdit2"
        Me.ImageComboBoxEdit2.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.ImageComboBoxEdit2.Properties.Appearance.Font = CType(resources.GetObject("ImageComboBoxEdit2.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ImageComboBoxEdit2.Properties.Appearance.Options.UseBackColor = True
        Me.ImageComboBoxEdit2.Properties.Appearance.Options.UseFont = True
        Me.ImageComboBoxEdit2.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit2.Properties.Items"), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items1"), Object), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit2.Properties.Items3"), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items4"), Object), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit2.Properties.Items6"), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items7"), Object), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit2.Properties.Items9"), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items10"), Object), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items11"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit2.Properties.Items12"), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items13"), Object), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items14"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit2.Properties.Items15"), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items16"), Object), CType(resources.GetObject("ImageComboBoxEdit2.Properties.Items17"), Integer))})
        Me.ImageComboBoxEdit2.Properties.ReadOnly = True
        Me.ImageComboBoxEdit2.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ImageComboBoxEdit2, 0)
        '
        'ButtonEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.ButtonEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ButtonEdit1, False)
        resources.ApplyResources(Me.ButtonEdit1, "ButtonEdit1")
        Me.IndigoTextEdit1.SetMascara(Me.ButtonEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ButtonEdit1.MenuManager = Me.BarManager1
        Me.ButtonEdit1.Name = "ButtonEdit1"
        Me.ButtonEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.ButtonEdit1.Properties.Appearance.Font = CType(resources.GetObject("ButtonEdit1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ButtonEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.ButtonEdit1.Properties.Appearance.Options.UseFont = True
        Me.ButtonEdit1.Properties.AppearanceFocused.Font = CType(resources.GetObject("ButtonEdit1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.ButtonEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.ButtonEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("ButtonEdit1.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.ButtonEdit1.Properties.ReadOnly = True
        Me.ButtonEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ButtonEdit1.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ButtonEdit1, 0)
        '
        'GridControl3
        '
        Me.GridControl3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GridControl3, "GridControl3")
        Me.GridControl3.MainView = Me.GridView3
        Me.GridControl3.MenuManager = Me.BarManager1
        Me.GridControl3.Name = "GridControl3"
        Me.GridControl3.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit1})
        Me.GridControl3.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView3})
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView3.Appearance.GroupRow.Font = CType(resources.GetObject("GridView3.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView3.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = CType(resources.GetObject("GridView3.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView3.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView3.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13})
        Me.GridView3.GridControl = Me.GridControl3
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsDetail.EnableMasterViewMode = False
        Me.GridView3.OptionsDetail.ShowDetailTabs = False
        Me.GridView3.OptionsDetail.SmartDetailExpand = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.GridView3.OptionsView.ShowIndicator = False
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn6, "GridColumn6")
        Me.GridColumn6.FieldName = "CHCAMASHO.NUMCAMHOS"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn6.OptionsColumn.AllowMove = False
        Me.GridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn6.OptionsColumn.ReadOnly = True
        Me.GridColumn6.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn6.OptionsFilter.AllowFilter = False
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn7, "GridColumn7")
        Me.GridColumn7.FieldName = "FECINIEST"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn7.OptionsColumn.AllowMove = False
        Me.GridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        Me.GridColumn7.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn7.OptionsFilter.AllowFilter = False
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn8, "GridColumn8")
        Me.GridColumn8.FieldName = "FECFINEST"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn8.OptionsColumn.AllowMove = False
        Me.GridColumn8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn8.OptionsColumn.ReadOnly = True
        Me.GridColumn8.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn8.OptionsFilter.AllowFilter = False
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn9, "GridColumn9")
        Me.GridColumn9.FieldName = "CHTIPESTA.DESTIPEST"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn9.OptionsColumn.AllowMove = False
        Me.GridColumn9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn9.OptionsColumn.ReadOnly = True
        Me.GridColumn9.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn9.OptionsFilter.AllowFilter = False
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn10.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn10, "GridColumn10")
        Me.GridColumn10.FieldName = "TotalTime"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn10.OptionsColumn.ReadOnly = True
        Me.GridColumn10.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn10.OptionsFilter.AllowFilter = False
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn11.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn11.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn11.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn11, "GridColumn11")
        Me.GridColumn11.FieldName = "TotalUnits"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.AllowMove = False
        Me.GridColumn11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.ReadOnly = True
        Me.GridColumn11.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn11.OptionsFilter.AllowFilter = False
        '
        'GridColumn12
        '
        Me.GridColumn12.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn12.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn12, "GridColumn12")
        Me.GridColumn12.ColumnEdit = Me.RepositoryItemPopupContainerEdit1
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn12.OptionsColumn.FixedWidth = True
        Me.GridColumn12.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn12.OptionsFilter.AllowFilter = False
        '
        'RepositoryItemPopupContainerEdit1
        '
        resources.ApplyResources(Me.RepositoryItemPopupContainerEdit1, "RepositoryItemPopupContainerEdit1")
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        Me.RepositoryItemPopupContainerEdit1.PopupFormMinSize = New System.Drawing.Size(453, 181)
        Me.RepositoryItemPopupContainerEdit1.PopupFormSize = New System.Drawing.Size(453, 181)
        Me.RepositoryItemPopupContainerEdit1.ShowPopupCloseButton = False
        Me.RepositoryItemPopupContainerEdit1.ShowPopupShadow = False
        Me.RepositoryItemPopupContainerEdit1.Tag = "T"
        Me.RepositoryItemPopupContainerEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'GridColumn13
        '
        resources.ApplyResources(Me.GridColumn13, "GridColumn13")
        Me.GridColumn13.FieldName = "FunctionalUnitCodeName"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        '
        'TextEdit8
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit8, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit8, False)
        resources.ApplyResources(Me.TextEdit8, "TextEdit8")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit8, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit8.MenuManager = Me.BarManager1
        Me.TextEdit8.Name = "TextEdit8"
        Me.TextEdit8.Properties.Appearance.Font = CType(resources.GetObject("TextEdit8.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit8.Properties.Appearance.Options.UseFont = True
        Me.TextEdit8.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit8.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit8.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit8.Properties.ReadOnly = True
        Me.TextEdit8.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit8, 0)
        '
        'TextEdit9
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit9, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit9, False)
        resources.ApplyResources(Me.TextEdit9, "TextEdit9")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit9, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit9.MenuManager = Me.BarManager1
        Me.TextEdit9.Name = "TextEdit9"
        Me.TextEdit9.Properties.Appearance.Font = CType(resources.GetObject("TextEdit9.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit9.Properties.Appearance.Options.UseFont = True
        Me.TextEdit9.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit9.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit9.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit9.Properties.ReadOnly = True
        Me.TextEdit9.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit9, 0)
        '
        'TextEdit10
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit10, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit10, False)
        resources.ApplyResources(Me.TextEdit10, "TextEdit10")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit10, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit10.Name = "TextEdit10"
        Me.TextEdit10.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit10.Properties.Appearance.Font = CType(resources.GetObject("TextEdit10.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit10.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit10.Properties.Appearance.Options.UseFont = True
        Me.TextEdit10.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit10.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit10.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit10.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit10.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit10.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit10.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit10.Properties.ReadOnly = True
        Me.TextEdit10.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit10, 0)
        '
        'TextEdit11
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit11, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit11, False)
        resources.ApplyResources(Me.TextEdit11, "TextEdit11")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit11, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit11.Name = "TextEdit11"
        Me.TextEdit11.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit11.Properties.Appearance.Font = CType(resources.GetObject("TextEdit11.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit11.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit11.Properties.Appearance.Options.UseFont = True
        Me.TextEdit11.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit11.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit11.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit11.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit11.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit11.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit11.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit11.Properties.ReadOnly = True
        Me.TextEdit11.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit11, 0)
        '
        'TextEdit12
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit12, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit12, False)
        resources.ApplyResources(Me.TextEdit12, "TextEdit12")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit12, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit12.Name = "TextEdit12"
        Me.TextEdit12.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit12.Properties.Appearance.Font = CType(resources.GetObject("TextEdit12.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit12.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit12.Properties.Appearance.Options.UseFont = True
        Me.TextEdit12.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit12.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit12.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit12.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit12.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit12.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit12.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit12.Properties.ReadOnly = True
        Me.TextEdit12.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit12, 0)
        '
        'TextEdit13
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit13, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit13, False)
        resources.ApplyResources(Me.TextEdit13, "TextEdit13")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit13, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit13.Name = "TextEdit13"
        Me.TextEdit13.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit13.Properties.Appearance.Font = CType(resources.GetObject("TextEdit13.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit13.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit13.Properties.Appearance.Options.UseFont = True
        Me.TextEdit13.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit13.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit13.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit13.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit13.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit13.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit13.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit13.Properties.ReadOnly = True
        Me.TextEdit13.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit13, 0)
        '
        'TextEdit14
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit14, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit14, False)
        resources.ApplyResources(Me.TextEdit14, "TextEdit14")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit14, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit14.Name = "TextEdit14"
        Me.TextEdit14.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit14.Properties.Appearance.Font = CType(resources.GetObject("TextEdit14.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit14.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit14.Properties.Appearance.Options.UseFont = True
        Me.TextEdit14.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit14.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit14.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit14.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit14.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit14.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit14.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit14.Properties.ReadOnly = True
        Me.TextEdit14.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit14, 0)
        '
        'TextEdit15
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit15, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit15, False)
        resources.ApplyResources(Me.TextEdit15, "TextEdit15")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit15, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit15.Name = "TextEdit15"
        Me.TextEdit15.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit15.Properties.Appearance.Font = CType(resources.GetObject("TextEdit15.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit15.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit15.Properties.Appearance.Options.UseFont = True
        Me.TextEdit15.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit15.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit15.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit15.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit15.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit15.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit15.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit15.Properties.ReadOnly = True
        Me.TextEdit15.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit15, 0)
        '
        'TextEdit16
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit16, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit16, False)
        resources.ApplyResources(Me.TextEdit16, "TextEdit16")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit16, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit16.Name = "TextEdit16"
        Me.TextEdit16.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit16.Properties.Appearance.Font = CType(resources.GetObject("TextEdit16.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit16.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit16.Properties.Appearance.Options.UseFont = True
        Me.TextEdit16.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit16.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit16.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit16.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit16.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit16.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit16.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit16.Properties.ReadOnly = True
        Me.TextEdit16.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit16, 0)
        '
        'TextEdit17
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit17, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit17, False)
        resources.ApplyResources(Me.TextEdit17, "TextEdit17")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit17, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit17.Name = "TextEdit17"
        Me.TextEdit17.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit17.Properties.Appearance.Font = CType(resources.GetObject("TextEdit17.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit17.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit17.Properties.Appearance.Options.UseFont = True
        Me.TextEdit17.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit17.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit17.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit17.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit17.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit17.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit17.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit17.Properties.ReadOnly = True
        Me.TextEdit17.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit17, 0)
        '
        'TextEdit18
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit18, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit18, False)
        resources.ApplyResources(Me.TextEdit18, "TextEdit18")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit18, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit18.Name = "TextEdit18"
        Me.TextEdit18.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit18.Properties.Appearance.Font = CType(resources.GetObject("TextEdit18.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit18.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit18.Properties.Appearance.Options.UseFont = True
        Me.TextEdit18.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit18.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit18.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit18.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit18.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit18.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit18.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit18.Properties.ReadOnly = True
        Me.TextEdit18.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit18, 0)
        '
        'ImageComboBoxEdit3
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.ImageComboBoxEdit3, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ImageComboBoxEdit3, False)
        resources.ApplyResources(Me.ImageComboBoxEdit3, "ImageComboBoxEdit3")
        Me.IndigoTextEdit1.SetMascara(Me.ImageComboBoxEdit3, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ImageComboBoxEdit3.Name = "ImageComboBoxEdit3"
        Me.ImageComboBoxEdit3.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.ImageComboBoxEdit3.Properties.Appearance.Font = CType(resources.GetObject("ImageComboBoxEdit3.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ImageComboBoxEdit3.Properties.Appearance.Options.UseBackColor = True
        Me.ImageComboBoxEdit3.Properties.Appearance.Options.UseFont = True
        Me.ImageComboBoxEdit3.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ImageComboBoxEdit3.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ImageComboBoxEdit3.Properties.AppearanceFocused.Font = CType(resources.GetObject("ImageComboBoxEdit3.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.ImageComboBoxEdit3.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.ImageComboBoxEdit3.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.ImageComboBoxEdit3.Properties.AppearanceFocused.Options.UseFont = True
        Me.ImageComboBoxEdit3.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit3.Properties.Items"), CType(resources.GetObject("ImageComboBoxEdit3.Properties.Items1"), Object), CType(resources.GetObject("ImageComboBoxEdit3.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit3.Properties.Items3"), CType(resources.GetObject("ImageComboBoxEdit3.Properties.Items4"), Object), CType(resources.GetObject("ImageComboBoxEdit3.Properties.Items5"), Integer))})
        Me.ImageComboBoxEdit3.Properties.ReadOnly = True
        Me.ImageComboBoxEdit3.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ImageComboBoxEdit3, 0)
        '
        'ImageComboBoxEdit4
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.ImageComboBoxEdit4, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ImageComboBoxEdit4, False)
        resources.ApplyResources(Me.ImageComboBoxEdit4, "ImageComboBoxEdit4")
        Me.IndigoTextEdit1.SetMascara(Me.ImageComboBoxEdit4, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ImageComboBoxEdit4.Name = "ImageComboBoxEdit4"
        Me.ImageComboBoxEdit4.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.ImageComboBoxEdit4.Properties.Appearance.Font = CType(resources.GetObject("ImageComboBoxEdit4.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ImageComboBoxEdit4.Properties.Appearance.Options.UseBackColor = True
        Me.ImageComboBoxEdit4.Properties.Appearance.Options.UseFont = True
        Me.ImageComboBoxEdit4.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ImageComboBoxEdit4.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ImageComboBoxEdit4.Properties.AppearanceFocused.Font = CType(resources.GetObject("ImageComboBoxEdit4.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.ImageComboBoxEdit4.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.ImageComboBoxEdit4.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.ImageComboBoxEdit4.Properties.AppearanceFocused.Options.UseFont = True
        Me.ImageComboBoxEdit4.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit4.Properties.Items"), CType(resources.GetObject("ImageComboBoxEdit4.Properties.Items1"), Object), CType(resources.GetObject("ImageComboBoxEdit4.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit4.Properties.Items3"), CType(resources.GetObject("ImageComboBoxEdit4.Properties.Items4"), Object), CType(resources.GetObject("ImageComboBoxEdit4.Properties.Items5"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit4.Properties.Items6"), CType(resources.GetObject("ImageComboBoxEdit4.Properties.Items7"), Object), CType(resources.GetObject("ImageComboBoxEdit4.Properties.Items8"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit4.Properties.Items9"), CType(resources.GetObject("ImageComboBoxEdit4.Properties.Items10"), Object), CType(resources.GetObject("ImageComboBoxEdit4.Properties.Items11"), Integer))})
        Me.ImageComboBoxEdit4.Properties.ReadOnly = True
        Me.ImageComboBoxEdit4.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ImageComboBoxEdit4, 0)
        '
        'ButtonEdit2
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.ButtonEdit2, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ButtonEdit2, False)
        resources.ApplyResources(Me.ButtonEdit2, "ButtonEdit2")
        Me.IndigoTextEdit1.SetMascara(Me.ButtonEdit2, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ButtonEdit2.Name = "ButtonEdit2"
        Me.ButtonEdit2.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.ButtonEdit2.Properties.Appearance.Font = CType(resources.GetObject("ButtonEdit2.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ButtonEdit2.Properties.Appearance.Options.UseBackColor = True
        Me.ButtonEdit2.Properties.Appearance.Options.UseFont = True
        Me.ButtonEdit2.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonEdit2.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ButtonEdit2.Properties.AppearanceFocused.Font = CType(resources.GetObject("ButtonEdit2.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.ButtonEdit2.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.ButtonEdit2.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.ButtonEdit2.Properties.AppearanceFocused.Options.UseFont = True
        Me.ButtonEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("ButtonEdit2.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.ButtonEdit2.Properties.ReadOnly = True
        Me.ButtonEdit2.StyleController = Me.LayoutControl6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ButtonEdit2, 0)
        '
        'GridControl4
        '
        Me.GridControl4.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GridControl4, "GridControl4")
        Me.GridControl4.MainView = Me.GridView4
        Me.GridControl4.MenuManager = Me.BarManager1
        Me.GridControl4.Name = "GridControl4"
        Me.GridControl4.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit3, Me.RepositoryItemPopupContainerEdit2})
        Me.GridControl4.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView4})
        '
        'GridView4
        '
        Me.GridView4.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView4.Appearance.GroupRow.Font = CType(resources.GetObject("GridView4.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView4.Appearance.GroupRow.Options.UseFont = True
        Me.GridView4.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView4.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView4.Appearance.Row.Font = CType(resources.GetObject("GridView4.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView4.Appearance.Row.Options.UseFont = True
        Me.GridView4.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView4.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView4.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18, Me.GridColumn28, Me.GridColumn29, Me.GridColumn30, Me.GridColumn31})
        Me.GridView4.GridControl = Me.GridControl4
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsDetail.EnableMasterViewMode = False
        Me.GridView4.OptionsDetail.ShowDetailTabs = False
        Me.GridView4.OptionsDetail.SmartDetailExpand = False
        Me.GridView4.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView4.OptionsView.EnableAppearanceOddRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        Me.GridView4.OptionsView.ShowIndicator = False
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn14.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn14, "GridColumn14")
        Me.GridColumn14.FieldName = "CHCAMASHO.NUMCAMHOS"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn14.OptionsColumn.AllowMove = False
        Me.GridColumn14.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn14.OptionsColumn.ReadOnly = True
        Me.GridColumn14.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn14.OptionsFilter.AllowFilter = False
        '
        'GridColumn15
        '
        Me.GridColumn15.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn15.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn15, "GridColumn15")
        Me.GridColumn15.FieldName = "FECINIEST"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn15.OptionsColumn.AllowMove = False
        Me.GridColumn15.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn15.OptionsColumn.ReadOnly = True
        Me.GridColumn15.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn15.OptionsFilter.AllowFilter = False
        '
        'GridColumn16
        '
        Me.GridColumn16.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn16.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn16, "GridColumn16")
        Me.GridColumn16.FieldName = "FECFINEST"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.OptionsColumn.AllowFocus = False
        Me.GridColumn16.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn16.OptionsColumn.AllowMove = False
        Me.GridColumn16.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn16.OptionsColumn.ReadOnly = True
        Me.GridColumn16.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn16.OptionsFilter.AllowFilter = False
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn17.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn17, "GridColumn17")
        Me.GridColumn17.FieldName = "CHTIPESTA.DESTIPEST"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowFocus = False
        Me.GridColumn17.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn17.OptionsColumn.AllowMove = False
        Me.GridColumn17.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn17.OptionsColumn.ReadOnly = True
        Me.GridColumn17.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn17.OptionsFilter.AllowFilter = False
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn18.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn18, "GridColumn18")
        Me.GridColumn18.FieldName = "TotalTime"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.AllowEdit = False
        Me.GridColumn18.OptionsColumn.AllowFocus = False
        Me.GridColumn18.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn18.OptionsColumn.AllowMove = False
        Me.GridColumn18.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn18.OptionsColumn.ReadOnly = True
        '
        'GridColumn28
        '
        Me.GridColumn28.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn28.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn28.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn28.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn28.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn28.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn28, "GridColumn28")
        Me.GridColumn28.FieldName = "TotalUnits"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.OptionsColumn.AllowEdit = False
        Me.GridColumn28.OptionsColumn.AllowFocus = False
        Me.GridColumn28.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn28.OptionsColumn.AllowMove = False
        Me.GridColumn28.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn28.OptionsColumn.ReadOnly = True
        Me.GridColumn28.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn28.OptionsFilter.AllowFilter = False
        '
        'GridColumn29
        '
        Me.GridColumn29.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn29.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn29.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn29.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn29.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn29, "GridColumn29")
        Me.GridColumn29.FieldName = "GENESTLIQ"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.OptionsColumn.AllowEdit = False
        Me.GridColumn29.OptionsColumn.AllowFocus = False
        Me.GridColumn29.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn29.OptionsColumn.AllowMove = False
        Me.GridColumn29.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn29.OptionsColumn.ReadOnly = True
        Me.GridColumn29.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn29.OptionsFilter.AllowFilter = False
        '
        'GridColumn30
        '
        Me.GridColumn30.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn30.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn30.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn30, "GridColumn30")
        Me.GridColumn30.ColumnEdit = Me.RepositoryItemPopupContainerEdit2
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn30.OptionsColumn.FixedWidth = True
        Me.GridColumn30.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn30.OptionsFilter.AllowFilter = False
        '
        'RepositoryItemPopupContainerEdit2
        '
        resources.ApplyResources(Me.RepositoryItemPopupContainerEdit2, "RepositoryItemPopupContainerEdit2")
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit2.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        Me.RepositoryItemPopupContainerEdit2.PopupControl = Me.PopupContainerControl1
        Me.RepositoryItemPopupContainerEdit2.PopupFormMinSize = New System.Drawing.Size(250, 181)
        Me.RepositoryItemPopupContainerEdit2.PopupFormSize = New System.Drawing.Size(250, 181)
        Me.RepositoryItemPopupContainerEdit2.ShowPopupCloseButton = False
        Me.RepositoryItemPopupContainerEdit2.ShowPopupShadow = False
        Me.RepositoryItemPopupContainerEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'GridColumn31
        '
        Me.GridColumn31.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn31.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn31, "GridColumn31")
        Me.GridColumn31.ColumnEdit = Me.RepositoryItemPopupContainerEdit3
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn31.OptionsColumn.FixedWidth = True
        Me.GridColumn31.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn31.OptionsFilter.AllowFilter = False
        '
        'RepositoryItemPopupContainerEdit3
        '
        resources.ApplyResources(Me.RepositoryItemPopupContainerEdit3, "RepositoryItemPopupContainerEdit3")
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit3.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        Me.RepositoryItemPopupContainerEdit3.PopupFormMinSize = New System.Drawing.Size(453, 181)
        Me.RepositoryItemPopupContainerEdit3.PopupFormSize = New System.Drawing.Size(453, 181)
        Me.RepositoryItemPopupContainerEdit3.ShowPopupCloseButton = False
        Me.RepositoryItemPopupContainerEdit3.ShowPopupShadow = False
        Me.RepositoryItemPopupContainerEdit3.Tag = "L"
        Me.RepositoryItemPopupContainerEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'LayoutControlGroup12
        '
        Me.LayoutControlGroup12.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup12.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup12.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup12.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup12.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup12.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup12.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup12.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup12.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup12.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup12.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup12, "LayoutControlGroup12")
        Me.LayoutControlGroup12.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup12.GroupBordersVisible = False
        Me.LayoutControlGroup12.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup3, Me.LayoutControlItem66})
        Me.LayoutControlGroup12.Name = "LycgPccAdmission"
        Me.LayoutControlGroup12.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup12.Size = New System.Drawing.Size(799, 305)
        Me.LayoutControlGroup12.TextVisible = False
        '
        'TabbedControlGroup3
        '
        resources.ApplyResources(Me.TabbedControlGroup3, "TabbedControlGroup3")
        Me.TabbedControlGroup3.Location = New System.Drawing.Point(0, 40)
        Me.TabbedControlGroup3.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup3.SelectedTabPage = Me.LayoutControlGroup13
        Me.TabbedControlGroup3.Size = New System.Drawing.Size(799, 265)
        Me.TabbedControlGroup3.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup13, Me.LayoutControlGroup14, Me.LayoutControlGroup15})
        '
        'LayoutControlGroup13
        '
        Me.LayoutControlGroup13.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup13.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup13.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup13.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup13.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup13.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup13.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup13.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup13.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup13.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup13.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup13.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup13.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup13.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup13.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup13.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup13.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup13.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup13.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup13.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup13.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup13, "LayoutControlGroup13")
        Me.LayoutControlGroup13.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem40, Me.LayoutControlItem41, Me.LayoutControlItem42, Me.LayoutControlItem43, Me.LayoutControlItem44, Me.LayoutControlItem45, Me.LayoutControlItem46, Me.LayoutControlItem47, Me.LayoutControlItem48, Me.LayoutControlItem49})
        Me.LayoutControlGroup13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup13.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup13.Size = New System.Drawing.Size(775, 214)
        '
        'LayoutControlItem40
        '
        Me.LayoutControlItem40.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem40.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem40.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem40.Control = Me.ButtonEdit1
        resources.ApplyResources(Me.LayoutControlItem40, "LayoutControlItem40")
        Me.LayoutControlItem40.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem40.MinSize = New System.Drawing.Size(50, 25)
        Me.LayoutControlItem40.Name = "LayoutControlItem23"
        Me.LayoutControlItem40.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem40.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem40.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem40.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem40.TextToControlDistance = 12
        '
        'LayoutControlItem41
        '
        Me.LayoutControlItem41.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem41.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem41.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem41.Control = Me.TextEdit6
        resources.ApplyResources(Me.LayoutControlItem41, "LayoutControlItem41")
        Me.LayoutControlItem41.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem41.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem41.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem41.Name = "LayoutControlItem25"
        Me.LayoutControlItem41.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem41.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem41.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem41.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem41.TextToControlDistance = 12
        '
        'LayoutControlItem42
        '
        Me.LayoutControlItem42.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem42.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem42.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem42.Control = Me.TextEdit4
        resources.ApplyResources(Me.LayoutControlItem42, "LayoutControlItem42")
        Me.LayoutControlItem42.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem42.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem42.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem42.Name = "LiCareGroup"
        Me.LayoutControlItem42.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem42.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem42.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem42.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem42.TextToControlDistance = 12
        '
        'LayoutControlItem43
        '
        Me.LayoutControlItem43.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem43.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem43.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem43.Control = Me.TextEdit2
        resources.ApplyResources(Me.LayoutControlItem43, "LayoutControlItem43")
        Me.LayoutControlItem43.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem43.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem43.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem43.Name = "LayoutControlItem29"
        Me.LayoutControlItem43.Size = New System.Drawing.Size(300, 94)
        Me.LayoutControlItem43.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem43.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem43.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem43.TextToControlDistance = 12
        '
        'LayoutControlItem44
        '
        Me.LayoutControlItem44.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem44.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem44.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem44.Control = Me.TextEdit5
        resources.ApplyResources(Me.LayoutControlItem44, "LayoutControlItem44")
        Me.LayoutControlItem44.Location = New System.Drawing.Point(300, 30)
        Me.LayoutControlItem44.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem44.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem44.Name = "LayoutControlItem26"
        Me.LayoutControlItem44.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem44.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem44.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem44.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem44.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem44.TextToControlDistance = 12
        '
        'LayoutControlItem45
        '
        Me.LayoutControlItem45.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem45.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem45.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem45.Control = Me.TextEdit3
        resources.ApplyResources(Me.LayoutControlItem45, "LayoutControlItem45")
        Me.LayoutControlItem45.Location = New System.Drawing.Point(300, 90)
        Me.LayoutControlItem45.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem45.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem45.Name = "LiEntity"
        Me.LayoutControlItem45.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem45.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem45.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem45.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem45.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem45.TextToControlDistance = 12
        '
        'LayoutControlItem46
        '
        Me.LayoutControlItem46.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem46.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem46.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem46.Control = Me.TextEdit7
        resources.ApplyResources(Me.LayoutControlItem46, "LayoutControlItem46")
        Me.LayoutControlItem46.Location = New System.Drawing.Point(300, 0)
        Me.LayoutControlItem46.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem46.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem46.Name = "LayoutControlItem24"
        Me.LayoutControlItem46.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem46.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem46.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem46.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem46.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem46.TextToControlDistance = 12
        '
        'LayoutControlItem47
        '
        Me.LayoutControlItem47.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem47.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem47.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem47.Control = Me.ImageComboBoxEdit2
        resources.ApplyResources(Me.LayoutControlItem47, "LayoutControlItem47")
        Me.LayoutControlItem47.Location = New System.Drawing.Point(300, 60)
        Me.LayoutControlItem47.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem47.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem47.Name = "LayoutControlItem18"
        Me.LayoutControlItem47.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem47.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem47.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem47.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem47.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem47.TextToControlDistance = 12
        '
        'LayoutControlItem48
        '
        Me.LayoutControlItem48.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem48.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem48.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem48.Control = Me.ImageComboBoxEdit1
        resources.ApplyResources(Me.LayoutControlItem48, "LayoutControlItem48")
        Me.LayoutControlItem48.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem48.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem48.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem48.Name = "LayoutControlItem17"
        Me.LayoutControlItem48.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem48.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem48.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem48.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem48.TextToControlDistance = 12
        '
        'LayoutControlItem49
        '
        Me.LayoutControlItem49.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem49.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem49.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem49.Control = Me.TextEdit1
        resources.ApplyResources(Me.LayoutControlItem49, "LayoutControlItem49")
        Me.LayoutControlItem49.Location = New System.Drawing.Point(300, 120)
        Me.LayoutControlItem49.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem49.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem49.Name = "TxtContacto"
        Me.LayoutControlItem49.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem49.Size = New System.Drawing.Size(475, 94)
        Me.LayoutControlItem49.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem49.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem49.TextSize = New System.Drawing.Size(115, 20)
        Me.LayoutControlItem49.TextToControlDistance = 12
        '
        'LayoutControlGroup14
        '
        Me.LayoutControlGroup14.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup14.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup14.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup14.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup14.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup14.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup14.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup14.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup14.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup14.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup14.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup14.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup14.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup14.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup14.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup14.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup14.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup14.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup14.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup14.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup14.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup14, "LayoutControlGroup14")
        Me.LayoutControlGroup14.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem50, Me.LayoutControlItem51, Me.LayoutControlItem52, Me.LayoutControlItem53, Me.LayoutControlItem54, Me.LayoutControlItem55, Me.LayoutControlItem56, Me.LayoutControlItem57, Me.LayoutControlItem58, Me.LayoutControlItem59, Me.LayoutControlItem60, Me.LayoutControlItem61, Me.LayoutControlItem62, Me.LayoutControlItem63})
        Me.LayoutControlGroup14.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup14.Name = "LycgAdmissionGroup"
        Me.LayoutControlGroup14.Size = New System.Drawing.Size(775, 214)
        '
        'LayoutControlItem50
        '
        Me.LayoutControlItem50.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem50.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem50.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem50.Control = Me.ButtonEdit2
        resources.ApplyResources(Me.LayoutControlItem50, "LayoutControlItem50")
        Me.LayoutControlItem50.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem50.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem50.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem50.Name = "LayoutControlItem7"
        Me.LayoutControlItem50.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem50.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem50.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem50.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem50.TextToControlDistance = 12
        '
        'LayoutControlItem51
        '
        Me.LayoutControlItem51.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem51.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem51.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem51.Control = Me.ImageComboBoxEdit3
        resources.ApplyResources(Me.LayoutControlItem51, "LayoutControlItem51")
        Me.LayoutControlItem51.Location = New System.Drawing.Point(0, 90)
        Me.LayoutControlItem51.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem51.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem51.Name = "LayoutControlItem11"
        Me.LayoutControlItem51.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem51.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem51.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem51.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem51.TextToControlDistance = 12
        '
        'LayoutControlItem52
        '
        Me.LayoutControlItem52.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem52.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem52.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem52.Control = Me.ImageComboBoxEdit4
        resources.ApplyResources(Me.LayoutControlItem52, "LayoutControlItem52")
        Me.LayoutControlItem52.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem52.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem52.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem52.Name = "LayoutControlItem13"
        Me.LayoutControlItem52.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem52.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem52.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem52.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem52.TextToControlDistance = 12
        '
        'LayoutControlItem53
        '
        Me.LayoutControlItem53.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem53.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem53.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem53.Control = Me.TextEdit15
        resources.ApplyResources(Me.LayoutControlItem53, "LayoutControlItem53")
        Me.LayoutControlItem53.Location = New System.Drawing.Point(300, 120)
        Me.LayoutControlItem53.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem53.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem53.Name = "LayoutControlItem14"
        Me.LayoutControlItem53.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem53.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem53.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem53.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem53.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem53.TextToControlDistance = 12
        '
        'LayoutControlItem54
        '
        Me.LayoutControlItem54.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem54.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem54.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem54.Control = Me.TextEdit14
        resources.ApplyResources(Me.LayoutControlItem54, "LayoutControlItem54")
        Me.LayoutControlItem54.Location = New System.Drawing.Point(0, 150)
        Me.LayoutControlItem54.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem54.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem54.Name = "LayoutControlItem15"
        Me.LayoutControlItem54.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem54.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem54.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem54.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem54.TextToControlDistance = 12
        '
        'LayoutControlItem55
        '
        Me.LayoutControlItem55.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem55.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem55.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem55.Control = Me.TextEdit10
        resources.ApplyResources(Me.LayoutControlItem55, "LayoutControlItem55")
        Me.LayoutControlItem55.Location = New System.Drawing.Point(300, 90)
        Me.LayoutControlItem55.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem55.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem55.Name = "LayoutControlItem8"
        Me.LayoutControlItem55.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem55.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem55.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem55.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem55.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem55.TextToControlDistance = 12
        '
        'LayoutControlItem56
        '
        Me.LayoutControlItem56.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem56.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem56.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem56.Control = Me.TextEdit18
        resources.ApplyResources(Me.LayoutControlItem56, "LayoutControlItem56")
        Me.LayoutControlItem56.Location = New System.Drawing.Point(300, 0)
        Me.LayoutControlItem56.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem56.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem56.Name = "LayoutControlItem9"
        Me.LayoutControlItem56.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem56.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem56.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem56.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem56.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem56.TextToControlDistance = 12
        '
        'LayoutControlItem57
        '
        Me.LayoutControlItem57.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem57.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem57.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem57.Control = Me.TextEdit9
        resources.ApplyResources(Me.LayoutControlItem57, "LayoutControlItem57")
        Me.LayoutControlItem57.Location = New System.Drawing.Point(0, 30)
        Me.LayoutControlItem57.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem57.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem57.Name = "LayoutControlItem27"
        Me.LayoutControlItem57.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem57.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem57.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem57.TextSize = New System.Drawing.Size(115, 20)
        Me.LayoutControlItem57.TextToControlDistance = 12
        '
        'LayoutControlItem58
        '
        Me.LayoutControlItem58.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem58.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem58.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem58.Control = Me.TextEdit13
        resources.ApplyResources(Me.LayoutControlItem58, "LayoutControlItem58")
        Me.LayoutControlItem58.Location = New System.Drawing.Point(300, 30)
        Me.LayoutControlItem58.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem58.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem58.Name = "LayoutControlItem16"
        Me.LayoutControlItem58.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem58.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem58.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem58.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem58.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem58.TextToControlDistance = 12
        '
        'LayoutControlItem59
        '
        Me.LayoutControlItem59.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem59.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem59.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem59.Control = Me.TextEdit8
        resources.ApplyResources(Me.LayoutControlItem59, "LayoutControlItem59")
        Me.LayoutControlItem59.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem59.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem59.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem59.Name = "LayoutControlItem39"
        Me.LayoutControlItem59.Size = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem59.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem59.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem59.TextSize = New System.Drawing.Size(115, 20)
        Me.LayoutControlItem59.TextToControlDistance = 12
        '
        'LayoutControlItem60
        '
        Me.LayoutControlItem60.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem60.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem60.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem60.Control = Me.TextEdit16
        resources.ApplyResources(Me.LayoutControlItem60, "LayoutControlItem60")
        Me.LayoutControlItem60.Location = New System.Drawing.Point(300, 60)
        Me.LayoutControlItem60.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem60.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem60.Name = "LayoutControlItem12"
        Me.LayoutControlItem60.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem60.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem60.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem60.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem60.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem60.TextToControlDistance = 12
        '
        'LayoutControlItem61
        '
        Me.LayoutControlItem61.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem61.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem61.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem61.Control = Me.TextEdit17
        resources.ApplyResources(Me.LayoutControlItem61, "LayoutControlItem61")
        Me.LayoutControlItem61.Location = New System.Drawing.Point(300, 150)
        Me.LayoutControlItem61.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem61.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem61.Name = "LayoutControlItem10"
        Me.LayoutControlItem61.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem61.Size = New System.Drawing.Size(475, 30)
        Me.LayoutControlItem61.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem61.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem61.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem61.TextToControlDistance = 12
        '
        'LayoutControlItem62
        '
        Me.LayoutControlItem62.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem62.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem62.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem62.Control = Me.TextEdit12
        resources.ApplyResources(Me.LayoutControlItem62, "LayoutControlItem62")
        Me.LayoutControlItem62.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem62.MaxSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem62.MinSize = New System.Drawing.Size(300, 30)
        Me.LayoutControlItem62.Name = "LayoutControlItem19"
        Me.LayoutControlItem62.Size = New System.Drawing.Size(300, 34)
        Me.LayoutControlItem62.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem62.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem62.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem62.TextToControlDistance = 12
        '
        'LayoutControlItem63
        '
        Me.LayoutControlItem63.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem63.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem63.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem63.Control = Me.TextEdit11
        resources.ApplyResources(Me.LayoutControlItem63, "LayoutControlItem63")
        Me.LayoutControlItem63.Location = New System.Drawing.Point(300, 180)
        Me.LayoutControlItem63.MaxSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem63.MinSize = New System.Drawing.Size(340, 30)
        Me.LayoutControlItem63.Name = "LayoutControlItem20"
        Me.LayoutControlItem63.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem63.Size = New System.Drawing.Size(475, 34)
        Me.LayoutControlItem63.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem63.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem63.TextSize = New System.Drawing.Size(115, 21)
        Me.LayoutControlItem63.TextToControlDistance = 12
        '
        'LayoutControlGroup15
        '
        Me.LayoutControlGroup15.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup15.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup15.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup15.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup15.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup15.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup15.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup15.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup15.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup15.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup15.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup15.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup15.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup15.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup15.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup15.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup15.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup15.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup15.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup15.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup15.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup15, "LayoutControlGroup15")
        Me.LayoutControlGroup15.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.TabbedControlGroup4})
        Me.LayoutControlGroup15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup15.Name = "LycgStays"
        Me.LayoutControlGroup15.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup15.Size = New System.Drawing.Size(775, 214)
        Me.LayoutControlGroup15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'TabbedControlGroup4
        '
        resources.ApplyResources(Me.TabbedControlGroup4, "TabbedControlGroup4")
        Me.TabbedControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.TabbedControlGroup4.Name = "TabbedControlGroup2"
        Me.TabbedControlGroup4.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.TabbedControlGroup4.SelectedTabPage = Me.LayoutControlGroup16
        Me.TabbedControlGroup4.Size = New System.Drawing.Size(775, 214)
        Me.TabbedControlGroup4.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup16, Me.LayoutControlGroup17})
        '
        'LayoutControlGroup16
        '
        Me.LayoutControlGroup16.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup16.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup16.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup16.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup16.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup16.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup16.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup16.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup16.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup16.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup16.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup16.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup16.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup16.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup16.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup16.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup16.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup16.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup16.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup16.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup16.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup16, "LayoutControlGroup16")
        Me.LayoutControlGroup16.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem64})
        Me.LayoutControlGroup16.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup16.Name = "LycgStaysDontLiquidated"
        Me.LayoutControlGroup16.Size = New System.Drawing.Size(769, 181)
        Me.LayoutControlGroup16.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem64
        '
        Me.LayoutControlItem64.Control = Me.GridControl3
        resources.ApplyResources(Me.LayoutControlItem64, "LayoutControlItem64")
        Me.LayoutControlItem64.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem64.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem64.Name = "LayoutControlItem36"
        Me.LayoutControlItem64.Size = New System.Drawing.Size(769, 181)
        Me.LayoutControlItem64.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem64.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem64.TextVisible = False
        '
        'LayoutControlGroup17
        '
        Me.LayoutControlGroup17.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup17.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup17.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup17.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup17.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup17.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup17.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup17.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup17.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup17.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup17.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup17.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup17.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup17.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup17.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup17.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup17.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup17.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup17.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup17.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup17.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup17, "LayoutControlGroup17")
        Me.LayoutControlGroup17.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem65})
        Me.LayoutControlGroup17.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup17.Name = "LycgStaysLiquidated"
        Me.LayoutControlGroup17.Size = New System.Drawing.Size(769, 181)
        Me.LayoutControlGroup17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem65
        '
        Me.LayoutControlItem65.Control = Me.GridControl4
        resources.ApplyResources(Me.LayoutControlItem65, "LayoutControlItem65")
        Me.LayoutControlItem65.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem65.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem65.Name = "LayoutControlItem35"
        Me.LayoutControlItem65.Size = New System.Drawing.Size(769, 181)
        Me.LayoutControlItem65.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem65.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem65.TextVisible = False
        '
        'LayoutControlItem66
        '
        Me.LayoutControlItem66.Control = Me.LabelControl1
        resources.ApplyResources(Me.LayoutControlItem66, "LayoutControlItem66")
        Me.LayoutControlItem66.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem66.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem66.MinSize = New System.Drawing.Size(1, 40)
        Me.LayoutControlItem66.Name = "LayoutControlItem31"
        Me.LayoutControlItem66.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem66.Size = New System.Drawing.Size(799, 40)
        Me.LayoutControlItem66.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem66.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem66.TextVisible = False
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.PanelControl5)
        Me.PanelControl4.Controls.Add(Me.LayoutControl8)
        resources.ApplyResources(Me.PanelControl4, "PanelControl4")
        Me.PanelControl4.Name = "PanelControl4"
        '
        'PanelControl5
        '
        Me.PanelControl5.Controls.Add(Me.LayoutControl7)
        resources.ApplyResources(Me.PanelControl5, "PanelControl5")
        Me.PanelControl5.Name = "PanelControl5"
        '
        'LayoutControl7
        '
        resources.ApplyResources(Me.LayoutControl7, "LayoutControl7")
        Me.LayoutControl7.Name = "LayoutControl7"
        Me.LayoutControl7.Root = Me.LayoutControlGroup18
        '
        'LayoutControlGroup18
        '
        Me.LayoutControlGroup18.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup18.GroupBordersVisible = False
        Me.LayoutControlGroup18.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup18.Size = New System.Drawing.Size(1152, 20)
        Me.LayoutControlGroup18.TextVisible = False
        '
        'LayoutControl8
        '
        Me.LayoutControl8.AllowCustomization = False
        Me.LayoutControl8.Controls.Add(Me.SearchLookUpEditExAdmission1)
        Me.LayoutControl8.Controls.Add(Me.LabelControl2)
        Me.LayoutControl8.Controls.Add(Me.LabelControl5)
        Me.LayoutControl8.Controls.Add(Me.PanelControl6)
        Me.LayoutControl8.Controls.Add(Me.DropDownButton1)
        Me.LayoutControl8.Controls.Add(Me.ButtonEdit3)
        Me.LayoutControl8.Controls.Add(Me.LabelControl6)
        Me.LayoutControl8.Controls.Add(Me.LabelControl7)
        Me.LayoutControl8.Controls.Add(Me.TextEdit19)
        Me.LayoutControl8.Controls.Add(Me.GridLookUpEdit1)
        Me.LayoutControl8.Controls.Add(Me.GridLookUpEdit2)
        Me.LayoutControl8.Controls.Add(Me.ImageComboBoxEdit5)
        Me.LayoutControl8.Controls.Add(Me.TextEdit20)
        resources.ApplyResources(Me.LayoutControl8, "LayoutControl8")
        Me.LayoutControl8.Name = "LayoutControl8"
        Me.LayoutControl8.Root = Me.LayoutControlGroup19
        '
        'SearchLookUpEditExAdmission1
        '
        Me.SearchLookUpEditExAdmission1.AllowQueryOne = True
        Me.SearchLookUpEditExAdmission1.Datasource = Nothing
        Me.SearchLookUpEditExAdmission1.DisplayMember = "{FullNameAdmission}"
        Me.SearchLookUpEditExAdmission1.DisplayNullText = ""
        Me.SearchLookUpEditExAdmission1.EditValue = Nothing
        Me.SearchLookUpEditExAdmission1.EnterMoveNextControl = True
        Me.SearchLookUpEditExAdmission1.FuncQueryOnKeyEnterPressed = Nothing
        Me.SearchLookUpEditExAdmission1.IdOpenForm = 0
        Me.SearchLookUpEditExAdmission1.IsReadOnly = False
        resources.ApplyResources(Me.SearchLookUpEditExAdmission1, "SearchLookUpEditExAdmission1")
        Me.SearchLookUpEditExAdmission1.Name = "SearchLookUpEditExAdmission1"
        Me.SearchLookUpEditExAdmission1.PopupContainerControl = Nothing
        Me.SearchLookUpEditExAdmission1.PopUpFormSize = New System.Drawing.Size(1000, 300)
        Me.SearchLookUpEditExAdmission1.ValueMember = "AdmissionCode"
        Me.SearchLookUpEditExAdmission1.View = Me.GridView5
        '
        'GridView5
        '
        Me.GridView5.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView5.Appearance.FocusedRow.Font = CType(resources.GetObject("GridView5.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridView5.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView5.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView5.Appearance.GroupRow.Font = CType(resources.GetObject("GridView5.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView5.Appearance.GroupRow.Options.UseFont = True
        Me.GridView5.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView5.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView5.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView5.Appearance.Row.Font = CType(resources.GetObject("GridView5.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView5.Appearance.Row.Options.UseFont = True
        Me.GridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.GridView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn32, Me.GridColumn33, Me.GridColumn34, Me.GridColumn35, Me.GridColumn36, Me.GridColumn37, Me.GridColumn38, Me.GridColumn39, Me.GridColumn40})
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView5.OptionsView.EnableAppearanceOddRow = True
        Me.GridView5.OptionsView.ShowAutoFilterRow = True
        '
        'GridColumn32
        '
        resources.ApplyResources(Me.GridColumn32, "GridColumn32")
        Me.GridColumn32.FieldName = "AdmissionCode"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.OptionsColumn.AllowEdit = False
        Me.GridColumn32.OptionsColumn.AllowFocus = False
        '
        'GridColumn33
        '
        resources.ApplyResources(Me.GridColumn33, "GridColumn33")
        Me.GridColumn33.FieldName = "PatientCode"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.OptionsColumn.AllowEdit = False
        Me.GridColumn33.OptionsColumn.AllowFocus = False
        '
        'GridColumn34
        '
        resources.ApplyResources(Me.GridColumn34, "GridColumn34")
        Me.GridColumn34.FieldName = "PatientName"
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.OptionsColumn.AllowEdit = False
        Me.GridColumn34.OptionsColumn.AllowFocus = False
        '
        'GridColumn35
        '
        resources.ApplyResources(Me.GridColumn35, "GridColumn35")
        Me.GridColumn35.FieldName = "AdmissionTypeNameInGrid"
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.OptionsColumn.AllowEdit = False
        Me.GridColumn35.OptionsColumn.AllowFocus = False
        '
        'GridColumn36
        '
        resources.ApplyResources(Me.GridColumn36, "GridColumn36")
        Me.GridColumn36.FieldName = "AdmissionDate"
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.OptionsColumn.AllowEdit = False
        Me.GridColumn36.OptionsColumn.AllowFocus = False
        '
        'GridColumn37
        '
        resources.ApplyResources(Me.GridColumn37, "GridColumn37")
        Me.GridColumn37.FieldName = "BedStay"
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.OptionsColumn.AllowEdit = False
        Me.GridColumn37.OptionsColumn.AllowFocus = False
        '
        'GridColumn38
        '
        resources.ApplyResources(Me.GridColumn38, "GridColumn38")
        Me.GridColumn38.FieldName = "LiquidationTypeNameInGrid"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.OptionsColumn.AllowEdit = False
        Me.GridColumn38.OptionsColumn.AllowFocus = False
        '
        'GridColumn39
        '
        resources.ApplyResources(Me.GridColumn39, "GridColumn39")
        Me.GridColumn39.FieldName = "ResponsibleName"
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.OptionsColumn.AllowEdit = False
        Me.GridColumn39.OptionsColumn.AllowFocus = False
        '
        'GridColumn40
        '
        resources.ApplyResources(Me.GridColumn40, "GridColumn40")
        Me.GridColumn40.FieldName = "StatusName"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.OptionsColumn.AllowEdit = False
        Me.GridColumn40.OptionsColumn.AllowFocus = False
        '
        'LabelControl2
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl2, True)
        Me.LabelControl2.Appearance.Font = CType(resources.GetObject("LabelControl2.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.White
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.LabelControl2, "LabelControl2")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl2, False)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.StyleController = Me.LayoutControl8
        '
        'LabelControl5
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl5, True)
        Me.LabelControl5.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl5.Appearance.Font = CType(resources.GetObject("LabelControl5.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl5.Appearance.Options.UseBackColor = True
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.AutoEllipsis = True
        resources.ApplyResources(Me.LabelControl5, "LabelControl5")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl5, False)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.StyleController = Me.LayoutControl8
        '
        'PanelControl6
        '
        Me.PanelControl6.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.PanelControl6.Appearance.Options.UseBackColor = True
        Me.PanelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.PanelControl6, "PanelControl6")
        Me.PanelControl6.Name = "PanelControl6"
        '
        'DropDownButton1
        '
        Me.DropDownButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.DropDownButton1.Appearance.Options.UseBackColor = True
        Me.DropDownButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DropDownButton1.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.DropDownButton1.DropDownControl = Me.PopupMenu1
        Me.DropDownButton1.ImageOptions.Image = CType(resources.GetObject("DropDownButton1.ImageOptions.Image"), System.Drawing.Image)
        Me.DropDownButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.DropDownButton1, "DropDownButton1")
        Me.DropDownButton1.Name = "DropDownButton1"
        Me.DropDownButton1.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[False]
        Me.DropDownButton1.StyleController = Me.LayoutControl8
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.Caption Or DevExpress.XtraBars.BarLinkUserDefines.PaintStyle), DevExpress.XtraBars.BarLinkUserDefines), Me.BarButtonItem1, "Buscar", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.Standard), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem17, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem4), New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.Caption Or DevExpress.XtraBars.BarLinkUserDefines.PaintStyle), DevExpress.XtraBars.BarLinkUserDefines), Me.BarButtonItem5, "Deshacer", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem6), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem7, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem10), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem11, True), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem12), New DevExpress.XtraBars.LinkPersistInfo(CType((DevExpress.XtraBars.BarLinkUserDefines.Caption Or DevExpress.XtraBars.BarLinkUserDefines.PaintStyle), DevExpress.XtraBars.BarLinkUserDefines), Me.BarSubItem1, "Imprimir Todo", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.Standard), New DevExpress.XtraBars.LinkPersistInfo(Me.BarSubItem2, True)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'ButtonEdit3
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.ButtonEdit3, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ButtonEdit3, False)
        resources.ApplyResources(Me.ButtonEdit3, "ButtonEdit3")
        Me.IndigoTextEdit1.SetMascara(Me.ButtonEdit3, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ButtonEdit3.Name = "ButtonEdit3"
        Me.ButtonEdit3.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.ButtonEdit3.Properties.Appearance.Font = CType(resources.GetObject("ButtonEdit3.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ButtonEdit3.Properties.Appearance.Options.UseBackColor = True
        Me.ButtonEdit3.Properties.Appearance.Options.UseFont = True
        Me.ButtonEdit3.Properties.Appearance.Options.UseTextOptions = True
        Me.ButtonEdit3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ButtonEdit3.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ButtonEdit3.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ButtonEdit3.Properties.AppearanceFocused.Font = CType(resources.GetObject("ButtonEdit3.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.ButtonEdit3.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.ButtonEdit3.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.ButtonEdit3.Properties.AppearanceFocused.Options.UseFont = True
        Me.ButtonEdit3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("ButtonEdit3.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("ButtonEdit3.Properties.Buttons1"), CType(resources.GetObject("ButtonEdit3.Properties.Buttons2"), Integer), CType(resources.GetObject("ButtonEdit3.Properties.Buttons3"), Boolean), CType(resources.GetObject("ButtonEdit3.Properties.Buttons4"), Boolean), CType(resources.GetObject("ButtonEdit3.Properties.Buttons5"), Boolean), EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, resources.GetString("ButtonEdit3.Properties.Buttons6"), CType(resources.GetObject("ButtonEdit3.Properties.Buttons7"), Object), CType(resources.GetObject("ButtonEdit3.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("ButtonEdit3.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.ButtonEdit3.Properties.ReadOnly = True
        Me.ButtonEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ButtonEdit3.StyleController = Me.LayoutControl8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ButtonEdit3, 0)
        '
        'LabelControl6
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl6, True)
        Me.LabelControl6.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl6.Appearance.Font = CType(resources.GetObject("LabelControl6.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl6.Appearance.Options.UseBackColor = True
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabelControl6, "LabelControl6")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl6, False)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.StyleController = Me.LayoutControl8
        '
        'LabelControl7
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl7, True)
        Me.LabelControl7.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl7.Appearance.Font = CType(resources.GetObject("LabelControl7.Appearance.Font"), System.Drawing.Font)
        Me.LabelControl7.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl7.Appearance.Options.UseBackColor = True
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Appearance.Options.UseForeColor = True
        Me.LabelControl7.Appearance.Options.UseTextOptions = True
        Me.LabelControl7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.LabelControl7, "LabelControl7")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl7, False)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.StyleController = Me.LayoutControl8
        '
        'TextEdit19
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit19, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit19, False)
        resources.ApplyResources(Me.TextEdit19, "TextEdit19")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit19, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit19.Name = "TextEdit19"
        Me.TextEdit19.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit19.Properties.Appearance.Font = CType(resources.GetObject("TextEdit19.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit19.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit19.Properties.Appearance.Options.UseFont = True
        Me.TextEdit19.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit19.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit19.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit19.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit19.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit19.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit19.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit19.Properties.ReadOnly = True
        Me.TextEdit19.StyleController = Me.LayoutControl8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit19, 0)
        '
        'GridLookUpEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.GridLookUpEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.GridLookUpEdit1, False)
        Me.GridLookUpEdit1.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.GridLookUpEdit1, "GridLookUpEdit1")
        Me.IndigoTextEdit1.SetMascara(Me.GridLookUpEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.GridLookUpEdit1.MenuManager = Me.BarManager1
        Me.GridLookUpEdit1.Name = "GridLookUpEdit1"
        Me.GridLookUpEdit1.Properties.AllowFocused = False
        Me.GridLookUpEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Green
        Me.GridLookUpEdit1.Properties.Appearance.Font = CType(resources.GetObject("GridLookUpEdit1.Properties.Appearance.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.GridLookUpEdit1.Properties.Appearance.Options.UseFont = True
        Me.GridLookUpEdit1.Properties.Appearance.Options.UseTextOptions = True
        Me.GridLookUpEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit1.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Green
        Me.GridLookUpEdit1.Properties.AppearanceDisabled.Font = CType(resources.GetObject("GridLookUpEdit1.Properties.AppearanceDisabled.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.GridLookUpEdit1.Properties.AppearanceDisabled.Options.UseFont = True
        Me.GridLookUpEdit1.Properties.AppearanceDisabled.Options.UseTextOptions = True
        Me.GridLookUpEdit1.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit1.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit1.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.Green
        Me.GridLookUpEdit1.Properties.AppearanceDropDown.Font = CType(resources.GetObject("GridLookUpEdit1.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.GridLookUpEdit1.Properties.AppearanceDropDown.Options.UseFont = True
        Me.GridLookUpEdit1.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.GridLookUpEdit1.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit1.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Green
        Me.GridLookUpEdit1.Properties.AppearanceFocused.Font = CType(resources.GetObject("GridLookUpEdit1.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GridLookUpEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.GridLookUpEdit1.Properties.AppearanceFocused.Options.UseTextOptions = True
        Me.GridLookUpEdit1.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit1.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit1.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Green
        Me.GridLookUpEdit1.Properties.AppearanceReadOnly.Font = CType(resources.GetObject("GridLookUpEdit1.Properties.AppearanceReadOnly.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.GridLookUpEdit1.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.GridLookUpEdit1.Properties.AppearanceReadOnly.Options.UseTextOptions = True
        Me.GridLookUpEdit1.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit1.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.GridLookUpEdit1.Properties.DisplayMember = "Name"
        Me.GridLookUpEdit1.Properties.NullText = resources.GetString("GridLookUpEdit1.Properties.NullText")
        Me.GridLookUpEdit1.Properties.PopupFormMinSize = New System.Drawing.Size(230, 120)
        Me.GridLookUpEdit1.Properties.PopupFormSize = New System.Drawing.Size(230, 120)
        Me.GridLookUpEdit1.Properties.PopupSizeable = False
        Me.GridLookUpEdit1.Properties.PopupView = Me.GridView6
        Me.GridLookUpEdit1.Properties.ShowFooter = False
        Me.GridLookUpEdit1.Properties.ShowPopupShadow = False
        Me.GridLookUpEdit1.Properties.ValueMember = "Id"
        Me.GridLookUpEdit1.StyleController = Me.LayoutControl8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.GridLookUpEdit1, 0)
        '
        'GridView6
        '
        Me.GridView6.Appearance.GroupRow.Font = CType(resources.GetObject("GridView6.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView6.Appearance.GroupRow.Options.UseFont = True
        Me.GridView6.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView6.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView6.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView6.Appearance.Row.Font = CType(resources.GetObject("GridView6.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView6.Appearance.Row.Options.UseFont = True
        Me.GridView6.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn41, Me.GridColumn42})
        Me.GridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsMenu.EnableColumnMenu = False
        Me.GridView6.OptionsMenu.EnableFooterMenu = False
        Me.GridView6.OptionsMenu.EnableGroupPanelMenu = False
        Me.GridView6.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView6.OptionsMenu.ShowAutoFilterRowItem = False
        Me.GridView6.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GridView6.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GridView6.OptionsMenu.ShowSplitItem = False
        Me.GridView6.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView6.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView6.OptionsView.EnableAppearanceOddRow = True
        Me.GridView6.OptionsView.ShowGroupPanel = False
        '
        'GridColumn41
        '
        Me.GridColumn41.AppearanceCell.Font = CType(resources.GetObject("GridColumn41.AppearanceCell.Font"), System.Drawing.Font)
        Me.GridColumn41.AppearanceCell.Options.UseFont = True
        Me.GridColumn41.AppearanceHeader.Font = CType(resources.GetObject("GridColumn41.AppearanceHeader.Font"), System.Drawing.Font)
        Me.GridColumn41.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.GridColumn41, "GridColumn41")
        Me.GridColumn41.FieldName = "Code"
        Me.GridColumn41.Name = "GridColumn41"
        '
        'GridColumn42
        '
        Me.GridColumn42.AppearanceCell.Font = CType(resources.GetObject("GridColumn42.AppearanceCell.Font"), System.Drawing.Font)
        Me.GridColumn42.AppearanceCell.Options.UseFont = True
        Me.GridColumn42.AppearanceHeader.Font = CType(resources.GetObject("GridColumn42.AppearanceHeader.Font"), System.Drawing.Font)
        Me.GridColumn42.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.GridColumn42, "GridColumn42")
        Me.GridColumn42.FieldName = "Name"
        Me.GridColumn42.Name = "GridColumn42"
        '
        'GridLookUpEdit2
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.GridLookUpEdit2, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.GridLookUpEdit2, False)
        Me.GridLookUpEdit2.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.GridLookUpEdit2, "GridLookUpEdit2")
        Me.IndigoTextEdit1.SetMascara(Me.GridLookUpEdit2, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.GridLookUpEdit2.MenuManager = Me.BarManager1
        Me.GridLookUpEdit2.Name = "GridLookUpEdit2"
        Me.GridLookUpEdit2.Properties.AllowFocused = False
        Me.GridLookUpEdit2.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit2.Properties.Appearance.Font = CType(resources.GetObject("GridLookUpEdit2.Properties.Appearance.Font"), System.Drawing.Font)
        Me.GridLookUpEdit2.Properties.Appearance.Options.UseBackColor = True
        Me.GridLookUpEdit2.Properties.Appearance.Options.UseFont = True
        Me.GridLookUpEdit2.Properties.Appearance.Options.UseTextOptions = True
        Me.GridLookUpEdit2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit2.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit2.Properties.AppearanceDisabled.Font = CType(resources.GetObject("GridLookUpEdit2.Properties.AppearanceDisabled.Font"), System.Drawing.Font)
        Me.GridLookUpEdit2.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.GridLookUpEdit2.Properties.AppearanceDisabled.Options.UseFont = True
        Me.GridLookUpEdit2.Properties.AppearanceDisabled.Options.UseTextOptions = True
        Me.GridLookUpEdit2.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit2.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit2.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit2.Properties.AppearanceDropDown.Font = CType(resources.GetObject("GridLookUpEdit2.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.GridLookUpEdit2.Properties.AppearanceDropDown.Options.UseBackColor = True
        Me.GridLookUpEdit2.Properties.AppearanceDropDown.Options.UseFont = True
        Me.GridLookUpEdit2.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.GridLookUpEdit2.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit2.Properties.AppearanceDropDown.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit2.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit2.Properties.AppearanceFocused.Font = CType(resources.GetObject("GridLookUpEdit2.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.GridLookUpEdit2.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.GridLookUpEdit2.Properties.AppearanceFocused.Options.UseFont = True
        Me.GridLookUpEdit2.Properties.AppearanceFocused.Options.UseTextOptions = True
        Me.GridLookUpEdit2.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit2.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit2.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit2.Properties.AppearanceReadOnly.Font = CType(resources.GetObject("GridLookUpEdit2.Properties.AppearanceReadOnly.Font"), System.Drawing.Font)
        Me.GridLookUpEdit2.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.GridLookUpEdit2.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.GridLookUpEdit2.Properties.AppearanceReadOnly.Options.UseTextOptions = True
        Me.GridLookUpEdit2.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit2.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridLookUpEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.GridLookUpEdit2.Properties.DisplayMember = "UnitName"
        Me.GridLookUpEdit2.Properties.NullText = resources.GetString("GridLookUpEdit2.Properties.NullText")
        Me.GridLookUpEdit2.Properties.PopupFormMinSize = New System.Drawing.Size(230, 120)
        Me.GridLookUpEdit2.Properties.PopupFormSize = New System.Drawing.Size(230, 120)
        Me.GridLookUpEdit2.Properties.PopupSizeable = False
        Me.GridLookUpEdit2.Properties.PopupView = Me.GridView7
        Me.GridLookUpEdit2.Properties.ShowFooter = False
        Me.GridLookUpEdit2.Properties.ShowPopupShadow = False
        Me.GridLookUpEdit2.Properties.ValueMember = "Id"
        Me.GridLookUpEdit2.StyleController = Me.LayoutControl8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.GridLookUpEdit2, 0)
        '
        'GridView7
        '
        Me.GridView7.Appearance.GroupRow.Font = CType(resources.GetObject("GridView7.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView7.Appearance.GroupRow.Options.UseFont = True
        Me.GridView7.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView7.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView7.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView7.Appearance.Row.Font = CType(resources.GetObject("GridView7.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView7.Appearance.Row.Options.UseFont = True
        Me.GridView7.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn43, Me.GridColumn44})
        Me.GridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsMenu.EnableColumnMenu = False
        Me.GridView7.OptionsMenu.EnableFooterMenu = False
        Me.GridView7.OptionsMenu.EnableGroupPanelMenu = False
        Me.GridView7.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridView7.OptionsMenu.ShowAutoFilterRowItem = False
        Me.GridView7.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.GridView7.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.GridView7.OptionsMenu.ShowSplitItem = False
        Me.GridView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView7.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView7.OptionsView.EnableAppearanceOddRow = True
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'GridColumn43
        '
        Me.GridColumn43.AppearanceCell.Font = CType(resources.GetObject("GridColumn43.AppearanceCell.Font"), System.Drawing.Font)
        Me.GridColumn43.AppearanceCell.Options.UseFont = True
        Me.GridColumn43.AppearanceHeader.Font = CType(resources.GetObject("GridColumn43.AppearanceHeader.Font"), System.Drawing.Font)
        Me.GridColumn43.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.GridColumn43, "GridColumn43")
        Me.GridColumn43.FieldName = "UnitCode"
        Me.GridColumn43.Name = "GridColumn43"
        '
        'GridColumn44
        '
        Me.GridColumn44.AppearanceCell.Font = CType(resources.GetObject("GridColumn44.AppearanceCell.Font"), System.Drawing.Font)
        Me.GridColumn44.AppearanceCell.Options.UseFont = True
        Me.GridColumn44.AppearanceHeader.Font = CType(resources.GetObject("GridColumn44.AppearanceHeader.Font"), System.Drawing.Font)
        Me.GridColumn44.AppearanceHeader.Options.UseFont = True
        resources.ApplyResources(Me.GridColumn44, "GridColumn44")
        Me.GridColumn44.FieldName = "UnitName"
        Me.GridColumn44.Name = "GridColumn44"
        '
        'ImageComboBoxEdit5
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.ImageComboBoxEdit5, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.ImageComboBoxEdit5, False)
        resources.ApplyResources(Me.ImageComboBoxEdit5, "ImageComboBoxEdit5")
        Me.IndigoTextEdit1.SetMascara(Me.ImageComboBoxEdit5, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.ImageComboBoxEdit5.Name = "ImageComboBoxEdit5"
        Me.ImageComboBoxEdit5.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.ImageComboBoxEdit5.Properties.Appearance.Font = CType(resources.GetObject("ImageComboBoxEdit5.Properties.Appearance.Font"), System.Drawing.Font)
        Me.ImageComboBoxEdit5.Properties.Appearance.Options.UseBackColor = True
        Me.ImageComboBoxEdit5.Properties.Appearance.Options.UseFont = True
        Me.ImageComboBoxEdit5.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ImageComboBoxEdit5.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ImageComboBoxEdit5.Properties.AppearanceFocused.Font = CType(resources.GetObject("ImageComboBoxEdit5.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.ImageComboBoxEdit5.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.ImageComboBoxEdit5.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.ImageComboBoxEdit5.Properties.AppearanceFocused.Options.UseFont = True
        Me.ImageComboBoxEdit5.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit5.Properties.Items"), CType(resources.GetObject("ImageComboBoxEdit5.Properties.Items1"), Object), CType(resources.GetObject("ImageComboBoxEdit5.Properties.Items2"), Integer)), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("ImageComboBoxEdit5.Properties.Items3"), CType(resources.GetObject("ImageComboBoxEdit5.Properties.Items4"), Object), CType(resources.GetObject("ImageComboBoxEdit5.Properties.Items5"), Integer))})
        Me.ImageComboBoxEdit5.Properties.ReadOnly = True
        Me.ImageComboBoxEdit5.StyleController = Me.LayoutControl8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.ImageComboBoxEdit5, 0)
        '
        'TextEdit20
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit20, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit20, False)
        resources.ApplyResources(Me.TextEdit20, "TextEdit20")
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit20, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit20.Name = "TextEdit20"
        Me.TextEdit20.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.TextEdit20.Properties.Appearance.Font = CType(resources.GetObject("TextEdit20.Properties.Appearance.Font"), System.Drawing.Font)
        Me.TextEdit20.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit20.Properties.Appearance.Options.UseFont = True
        Me.TextEdit20.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit20.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit20.Properties.AppearanceFocused.Font = CType(resources.GetObject("TextEdit20.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.TextEdit20.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit20.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit20.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit20.Properties.ReadOnly = True
        Me.TextEdit20.StyleController = Me.LayoutControl8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit20, 0)
        '
        'LayoutControlGroup19
        '
        Me.LayoutControlGroup19.AllowHide = False
        Me.LayoutControlGroup19.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup19.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup19.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup19.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup19.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup19.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup19.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup19.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup19.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup19.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup19.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup19.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup19.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup19.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup19.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup19.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup19.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup19.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup19.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup19.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup19.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup19, "LayoutControlGroup19")
        Me.LayoutControlGroup19.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup19.GroupBordersVisible = False
        Me.LayoutControlGroup19.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup20, Me.LayoutControlItem76, Me.LayoutControlItem77, Me.LayoutControlItem78, Me.LayoutControlItem79, Me.EmptySpaceItem4})
        Me.LayoutControlGroup19.Name = "Root"
        Me.LayoutControlGroup19.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup19.ShowInCustomizationForm = False
        Me.LayoutControlGroup19.Size = New System.Drawing.Size(1177, 146)
        Me.LayoutControlGroup19.TextVisible = False
        '
        'LayoutControlGroup20
        '
        Me.LayoutControlGroup20.AllowHide = False
        Me.LayoutControlGroup20.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup20.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup20.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup20.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup20.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup20.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup20.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup20.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup20.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup20.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup20.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup20.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup20.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup20.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup20.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup20.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup20.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup20.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup20.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup20.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup20.AppearanceTabPage.PageClient.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup20, "LayoutControlGroup20")
        Me.LayoutControlGroup20.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem67, Me.LayoutControlItem68, Me.LayoutControlItem69, Me.LayoutControlItem70, Me.LayoutControlItem71, Me.LayoutControlItem72, Me.EmptySpaceItem2, Me.LayoutControlItem73, Me.LayoutControlItem74, Me.LayoutControlItem75})
        Me.LayoutControlGroup20.Location = New System.Drawing.Point(0, 40)
        Me.LayoutControlGroup20.Name = "LycgGeneralGroup"
        Me.LayoutControlGroup20.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 0, 2, 9)
        Me.LayoutControlGroup20.Size = New System.Drawing.Size(1177, 85)
        Me.LayoutControlGroup20.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup20.TextVisible = False
        '
        'LayoutControlItem67
        '
        Me.LayoutControlItem67.Control = Me.TextEdit20
        resources.ApplyResources(Me.LayoutControlItem67, "LayoutControlItem67")
        Me.LayoutControlItem67.Location = New System.Drawing.Point(291, 36)
        Me.LayoutControlItem67.MinSize = New System.Drawing.Size(50, 25)
        Me.LayoutControlItem67.Name = "LayoutControlItem4"
        Me.LayoutControlItem67.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2)
        Me.LayoutControlItem67.Size = New System.Drawing.Size(92, 36)
        Me.LayoutControlItem67.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem67.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem67.TextSize = New System.Drawing.Size(25, 21)
        Me.LayoutControlItem67.TextToControlDistance = 6
        '
        'LayoutControlItem68
        '
        Me.LayoutControlItem68.Control = Me.ImageComboBoxEdit5
        resources.ApplyResources(Me.LayoutControlItem68, "LayoutControlItem68")
        Me.LayoutControlItem68.Location = New System.Drawing.Point(150, 36)
        Me.LayoutControlItem68.MinSize = New System.Drawing.Size(50, 25)
        Me.LayoutControlItem68.Name = "LayoutControlItem3"
        Me.LayoutControlItem68.Size = New System.Drawing.Size(141, 36)
        Me.LayoutControlItem68.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem68.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem68.TextSize = New System.Drawing.Size(35, 21)
        Me.LayoutControlItem68.TextToControlDistance = 6
        '
        'LayoutControlItem69
        '
        Me.LayoutControlItem69.Control = Me.TextEdit19
        resources.ApplyResources(Me.LayoutControlItem69, "LayoutControlItem69")
        Me.LayoutControlItem69.Location = New System.Drawing.Point(383, 36)
        Me.LayoutControlItem69.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem69.MinSize = New System.Drawing.Size(183, 36)
        Me.LayoutControlItem69.Name = "LayoutControlItem5"
        Me.LayoutControlItem69.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2)
        Me.LayoutControlItem69.Size = New System.Drawing.Size(253, 36)
        Me.LayoutControlItem69.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem69.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem69.TextSize = New System.Drawing.Size(40, 21)
        Me.LayoutControlItem69.TextToControlDistance = 6
        '
        'LayoutControlItem70
        '
        Me.LayoutControlItem70.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LayoutControlItem70.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem70.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem70.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black
        Me.LayoutControlItem70.AppearanceItemCaption.Options.UseBackColor = True
        Me.LayoutControlItem70.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem70.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem70.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem70.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlItem70.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem70.Control = Me.LabelControl7
        resources.ApplyResources(Me.LayoutControlItem70, "LayoutControlItem70")
        Me.LayoutControlItem70.Location = New System.Drawing.Point(916, 0)
        Me.LayoutControlItem70.MaxSize = New System.Drawing.Size(125, 0)
        Me.LayoutControlItem70.MinSize = New System.Drawing.Size(125, 1)
        Me.LayoutControlItem70.Name = "LayoutControlItem2"
        Me.LayoutControlItem70.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 6)
        Me.LayoutControlItem70.Size = New System.Drawing.Size(125, 72)
        Me.LayoutControlItem70.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem70.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem70.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem70.TextSize = New System.Drawing.Size(133, 22)
        Me.LayoutControlItem70.TextToControlDistance = 2
        '
        'LayoutControlItem71
        '
        Me.LayoutControlItem71.AppearanceItemCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LayoutControlItem71.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem71.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem71.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black
        Me.LayoutControlItem71.AppearanceItemCaption.Options.UseBackColor = True
        Me.LayoutControlItem71.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem71.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem71.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem71.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutControlItem71.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem71.Control = Me.LabelControl6
        resources.ApplyResources(Me.LayoutControlItem71, "LayoutControlItem71")
        Me.LayoutControlItem71.Location = New System.Drawing.Point(1041, 0)
        Me.LayoutControlItem71.MaxSize = New System.Drawing.Size(125, 0)
        Me.LayoutControlItem71.MinSize = New System.Drawing.Size(125, 1)
        Me.LayoutControlItem71.Name = "LayoutControlItem6"
        Me.LayoutControlItem71.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 6)
        Me.LayoutControlItem71.Size = New System.Drawing.Size(125, 72)
        Me.LayoutControlItem71.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem71.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem71.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem71.TextSize = New System.Drawing.Size(135, 22)
        Me.LayoutControlItem71.TextToControlDistance = 2
        '
        'LayoutControlItem72
        '
        Me.LayoutControlItem72.Control = Me.ButtonEdit3
        resources.ApplyResources(Me.LayoutControlItem72, "LayoutControlItem72")
        Me.LayoutControlItem72.Location = New System.Drawing.Point(636, 36)
        Me.LayoutControlItem72.MaxSize = New System.Drawing.Size(114, 36)
        Me.LayoutControlItem72.MinSize = New System.Drawing.Size(114, 36)
        Me.LayoutControlItem72.Name = "LayoutControlItem21"
        Me.LayoutControlItem72.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.LayoutControlItem72.Size = New System.Drawing.Size(114, 36)
        Me.LayoutControlItem72.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem72.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem72.TextSize = New System.Drawing.Size(40, 21)
        Me.LayoutControlItem72.TextToControlDistance = 12
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem2, "EmptySpaceItem2")
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(814, 0)
        Me.EmptySpaceItem2.MaxSize = New System.Drawing.Size(0, 10)
        Me.EmptySpaceItem2.MinSize = New System.Drawing.Size(1, 1)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(102, 72)
        Me.EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem73
        '
        Me.LayoutControlItem73.Control = Me.DropDownButton1
        resources.ApplyResources(Me.LayoutControlItem73, "LayoutControlItem73")
        Me.LayoutControlItem73.Location = New System.Drawing.Point(750, 0)
        Me.LayoutControlItem73.MaxSize = New System.Drawing.Size(64, 66)
        Me.LayoutControlItem73.MinSize = New System.Drawing.Size(64, 66)
        Me.LayoutControlItem73.Name = "LayoutControlItem22"
        Me.LayoutControlItem73.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 0, 2, 0)
        Me.LayoutControlItem73.Size = New System.Drawing.Size(64, 72)
        Me.LayoutControlItem73.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem73.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem73.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem73.TextToControlDistance = 0
        Me.LayoutControlItem73.TextVisible = False
        '
        'LayoutControlItem74
        '
        Me.LayoutControlItem74.Control = Me.LabelControl2
        Me.LayoutControlItem74.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem74.MaxSize = New System.Drawing.Size(0, 60)
        Me.LayoutControlItem74.MinSize = New System.Drawing.Size(150, 36)
        Me.LayoutControlItem74.Name = "LciStatusAdmission"
        Me.LayoutControlItem74.Size = New System.Drawing.Size(150, 36)
        Me.LayoutControlItem74.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.LayoutControlItem74, "LayoutControlItem74")
        Me.LayoutControlItem74.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem74.TextSize = New System.Drawing.Size(50, 21)
        Me.LayoutControlItem74.TextToControlDistance = 12
        '
        'LayoutControlItem75
        '
        Me.LayoutControlItem75.Control = Me.SearchLookUpEditExAdmission1
        Me.LayoutControlItem75.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem75.MaxSize = New System.Drawing.Size(750, 36)
        Me.LayoutControlItem75.MinSize = New System.Drawing.Size(750, 36)
        Me.LayoutControlItem75.Name = "INDLciAdmission"
        Me.LayoutControlItem75.Size = New System.Drawing.Size(750, 36)
        Me.LayoutControlItem75.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.LayoutControlItem75, "LayoutControlItem75")
        Me.LayoutControlItem75.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem75.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem75.TextToControlDistance = 12
        '
        'LayoutControlItem76
        '
        Me.LayoutControlItem76.Control = Me.PanelControl6
        resources.ApplyResources(Me.LayoutControlItem76, "LayoutControlItem76")
        Me.LayoutControlItem76.Location = New System.Drawing.Point(0, 125)
        Me.LayoutControlItem76.MaxSize = New System.Drawing.Size(0, 1)
        Me.LayoutControlItem76.MinSize = New System.Drawing.Size(1, 1)
        Me.LayoutControlItem76.Name = "LayoutControlItem30"
        Me.LayoutControlItem76.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem76.Size = New System.Drawing.Size(1177, 21)
        Me.LayoutControlItem76.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem76.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem76.TextVisible = False
        '
        'LayoutControlItem77
        '
        Me.LayoutControlItem77.Control = Me.LabelControl5
        resources.ApplyResources(Me.LayoutControlItem77, "LayoutControlItem77")
        Me.LayoutControlItem77.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem77.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem77.MinSize = New System.Drawing.Size(10, 40)
        Me.LayoutControlItem77.Name = "LayoutControlItem32"
        Me.LayoutControlItem77.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0)
        Me.LayoutControlItem77.Size = New System.Drawing.Size(925, 40)
        Me.LayoutControlItem77.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem77.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem77.TextVisible = False
        '
        'LayoutControlItem78
        '
        Me.LayoutControlItem78.Control = Me.GridLookUpEdit1
        resources.ApplyResources(Me.LayoutControlItem78, "LayoutControlItem78")
        Me.LayoutControlItem78.Location = New System.Drawing.Point(925, 15)
        Me.LayoutControlItem78.MaxSize = New System.Drawing.Size(126, 0)
        Me.LayoutControlItem78.MinSize = New System.Drawing.Size(126, 1)
        Me.LayoutControlItem78.Name = "LayoutControlItem33"
        Me.LayoutControlItem78.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem78.Size = New System.Drawing.Size(126, 25)
        Me.LayoutControlItem78.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem78.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem78.TextVisible = False
        '
        'LayoutControlItem79
        '
        Me.LayoutControlItem79.Control = Me.GridLookUpEdit2
        resources.ApplyResources(Me.LayoutControlItem79, "LayoutControlItem79")
        Me.LayoutControlItem79.Location = New System.Drawing.Point(1051, 15)
        Me.LayoutControlItem79.MaxSize = New System.Drawing.Size(126, 0)
        Me.LayoutControlItem79.MinSize = New System.Drawing.Size(126, 1)
        Me.LayoutControlItem79.Name = "LayoutControlItem34"
        Me.LayoutControlItem79.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0)
        Me.LayoutControlItem79.Size = New System.Drawing.Size(126, 25)
        Me.LayoutControlItem79.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem79.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem79.TextVisible = False
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem4, "EmptySpaceItem4")
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(925, 0)
        Me.EmptySpaceItem4.MaxSize = New System.Drawing.Size(0, 15)
        Me.EmptySpaceItem4.MinSize = New System.Drawing.Size(1, 15)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem4.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(252, 15)
        Me.EmptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'CtrContainerControl1
        '
        Me.CtrContainerControl1.Controls.Add(Me.AutoHideContainer1)
        resources.ApplyResources(Me.CtrContainerControl1, "CtrContainerControl1")
        Me.CtrContainerControl1.Name = "CtrContainerControl1"
        '
        'AutoHideContainer1
        '
        Me.AutoHideContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.AutoHideContainer1.Controls.Add(Me.DockPanel1)
        resources.ApplyResources(Me.AutoHideContainer1, "AutoHideContainer1")
        Me.AutoHideContainer1.Name = "AutoHideContainer1"
        '
        'DockPanel1
        '
        Me.DockPanel1.Appearance.BackColor = System.Drawing.Color.White
        Me.DockPanel1.Appearance.Font = CType(resources.GetObject("DockPanel1.Appearance.Font"), System.Drawing.Font)
        Me.DockPanel1.Appearance.ForeColor = System.Drawing.Color.White
        Me.DockPanel1.Appearance.Options.UseBackColor = True
        Me.DockPanel1.Appearance.Options.UseFont = True
        Me.DockPanel1.Appearance.Options.UseForeColor = True
        Me.DockPanel1.Appearance.Options.UseTextOptions = True
        Me.DockPanel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.DockPanel1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.DockPanel1.Controls.Add(Me.ControlContainer1)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ForeColor = System.Drawing.Color.White
        Me.DockPanel1.ID = New System.Guid("dec38afa-357e-432e-a626-f95d12b5269e")
        resources.ApplyResources(Me.DockPanel1, "DockPanel1")
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.Options.AllowDockAsTabbedDocument = False
        Me.DockPanel1.Options.AllowDockFill = False
        Me.DockPanel1.Options.AllowDockLeft = False
        Me.DockPanel1.Options.AllowDockRight = False
        Me.DockPanel1.Options.AllowDockTop = False
        Me.DockPanel1.Options.AllowFloating = False
        Me.DockPanel1.Options.FloatOnDblClick = False
        Me.DockPanel1.Options.ShowCloseButton = False
        Me.DockPanel1.Options.ShowMaximizeButton = False
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 107)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'ControlContainer1
        '
        Me.ControlContainer1.Controls.Add(Me.GridControl5)
        resources.ApplyResources(Me.ControlContainer1, "ControlContainer1")
        Me.ControlContainer1.Name = "ControlContainer1"
        '
        'GridControl5
        '
        Me.GridControl5.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.GridControl5, "GridControl5")
        Me.GridControl5.MainView = Me.GridView8
        Me.GridControl5.MenuManager = Me.BarManager1
        Me.GridControl5.Name = "GridControl5"
        Me.GridControl5.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView8})
        '
        'GridView8
        '
        Me.GridView8.Appearance.EvenRow.BackColor = System.Drawing.Color.White
        Me.GridView8.Appearance.EvenRow.Font = CType(resources.GetObject("GridView8.Appearance.EvenRow.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.GridView8.Appearance.EvenRow.Options.UseBackColor = True
        Me.GridView8.Appearance.EvenRow.Options.UseFont = True
        Me.GridView8.Appearance.EvenRow.Options.UseForeColor = True
        Me.GridView8.Appearance.FocusedRow.Font = CType(resources.GetObject("GridView8.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView8.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView8.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView8.Appearance.GroupRow.Font = CType(resources.GetObject("GridView8.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.GroupRow.Options.UseFont = True
        Me.GridView8.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Gainsboro
        Me.GridView8.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView8.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.GridView8.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView8.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.White
        Me.GridView8.Appearance.HideSelectionRow.Font = CType(resources.GetObject("GridView8.Appearance.HideSelectionRow.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black
        Me.GridView8.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.GridView8.Appearance.HideSelectionRow.Options.UseFont = True
        Me.GridView8.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.GridView8.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GridView8.Appearance.HorzLine.Options.UseBackColor = True
        Me.GridView8.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.GridView8.Appearance.OddRow.Font = CType(resources.GetObject("GridView8.Appearance.OddRow.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.GridView8.Appearance.OddRow.Options.UseBackColor = True
        Me.GridView8.Appearance.OddRow.Options.UseFont = True
        Me.GridView8.Appearance.OddRow.Options.UseForeColor = True
        Me.GridView8.Appearance.Preview.BackColor = System.Drawing.Color.White
        Me.GridView8.Appearance.Preview.Font = CType(resources.GetObject("GridView8.Appearance.Preview.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.Preview.ForeColor = System.Drawing.Color.Black
        Me.GridView8.Appearance.Preview.Options.UseBackColor = True
        Me.GridView8.Appearance.Preview.Options.UseFont = True
        Me.GridView8.Appearance.Preview.Options.UseForeColor = True
        Me.GridView8.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.GridView8.Appearance.Row.Font = CType(resources.GetObject("GridView8.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.GridView8.Appearance.Row.Options.UseBackColor = True
        Me.GridView8.Appearance.Row.Options.UseFont = True
        Me.GridView8.Appearance.Row.Options.UseForeColor = True
        Me.GridView8.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.GridView8.Appearance.VertLine.Options.UseBackColor = True
        Me.GridView8.Appearance.ViewCaption.Font = CType(resources.GetObject("GridView8.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.GridView8.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView8.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn45, Me.GridColumn46, Me.GridColumn47})
        Me.GridView8.GridControl = Me.GridControl5
        Me.GridView8.Name = "GridView8"
        Me.GridView8.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView8.OptionsView.EnableAppearanceOddRow = True
        Me.GridView8.OptionsView.ShowGroupPanel = False
        Me.GridView8.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridView8.OptionsView.ShowIndicator = False
        Me.GridView8.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'GridColumn45
        '
        Me.GridColumn45.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn45.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn45.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn45.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn45.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn45.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn45, "GridColumn45")
        Me.GridColumn45.FieldName = "Icon"
        Me.GridColumn45.Name = "GridColumn45"
        Me.GridColumn45.OptionsColumn.AllowEdit = False
        Me.GridColumn45.OptionsColumn.AllowFocus = False
        Me.GridColumn45.OptionsColumn.AllowMove = False
        Me.GridColumn45.OptionsColumn.AllowSize = False
        Me.GridColumn45.OptionsColumn.FixedWidth = True
        Me.GridColumn45.OptionsColumn.ReadOnly = True
        Me.GridColumn45.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn45.OptionsFilter.AllowFilter = False
        '
        'GridColumn46
        '
        Me.GridColumn46.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn46.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn46.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn46, "GridColumn46")
        Me.GridColumn46.FieldName = "Title"
        Me.GridColumn46.MinWidth = 100
        Me.GridColumn46.Name = "GridColumn46"
        Me.GridColumn46.OptionsColumn.AllowEdit = False
        Me.GridColumn46.OptionsColumn.AllowFocus = False
        Me.GridColumn46.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn46.OptionsColumn.AllowMove = False
        Me.GridColumn46.OptionsColumn.ReadOnly = True
        Me.GridColumn46.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn46.OptionsFilter.AllowFilter = False
        '
        'GridColumn47
        '
        Me.GridColumn47.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn47.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn47.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn47, "GridColumn47")
        Me.GridColumn47.FieldName = "Message"
        Me.GridColumn47.MinWidth = 100
        Me.GridColumn47.Name = "GridColumn47"
        Me.GridColumn47.OptionsColumn.AllowEdit = False
        Me.GridColumn47.OptionsColumn.AllowFocus = False
        Me.GridColumn47.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn47.OptionsColumn.AllowMove = False
        Me.GridColumn47.OptionsColumn.ReadOnly = True
        Me.GridColumn47.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn47.OptionsFilter.AllowFilter = False
        '
        'Bar1
        '
        Me.Bar1.BarName = "Custom 2"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        resources.ApplyResources(Me.Bar1, "Bar1")
        '
        'GridColumn48
        '
        Me.GridColumn48.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn48.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn48.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn48.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn48.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn48.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn48, "GridColumn48")
        Me.GridColumn48.FieldName = "NumberFolio"
        Me.GridColumn48.MinWidth = 23
        Me.GridColumn48.Name = "GridColumn48"
        Me.GridColumn48.OptionsColumn.AllowEdit = False
        Me.GridColumn48.OptionsColumn.AllowFocus = False
        Me.GridColumn48.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn48.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn48.OptionsColumn.FixedWidth = True
        Me.GridColumn48.OptionsColumn.ReadOnly = True
        Me.GridColumn48.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn48.OptionsFilter.AllowFilter = False
        '
        'GridColumn49
        '
        Me.GridColumn49.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn49.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn49.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn49.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn49.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn49.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        resources.ApplyResources(Me.GridColumn49, "GridColumn49")
        Me.GridColumn49.DisplayFormat.FormatString = "C2"
        Me.GridColumn49.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn49.FieldName = "ValueFolio"
        Me.GridColumn49.MinWidth = 23
        Me.GridColumn49.Name = "GridColumn49"
        Me.GridColumn49.OptionsColumn.AllowEdit = False
        Me.GridColumn49.OptionsColumn.AllowFocus = False
        Me.GridColumn49.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn49.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn49.OptionsColumn.ReadOnly = True
        Me.GridColumn49.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn49.OptionsFilter.AllowFilter = False
        '
        'FrmLiquidation
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PnlBodySearch)
        Me.Controls.Add(Me.PnlCtrlHeader)
        Me.Controls.Add(Me.PnlBodyDashboard)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Name = "FrmLiquidation"
        Me.ShowInTaskbar = False
        Me.Tag = "756"
        CType(Me.GdcStays, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GdvStays, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepEstShowDetails, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycHeader, System.ComponentModel.ISupportInitialize).EndInit
        Me.LycHeader.ResumeLayout(False)
        CType(Me.INDSleAdmissionNumber2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.viewSearchAdmission, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PpmActions, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BteCountFolio.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAdmissionDate1.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SleBillingAuthorization.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GdvBillingAuthorization, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SleOperatingUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GdvOperatingUnit, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAdmissionType1.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPlaceEntry1.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgHeader, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgGeneralGroup, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LciStatusAdmission, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciAdmission, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem32, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem33, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem34, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PccAdmission, System.ComponentModel.ISupportInitialize).EndInit
        Me.PccAdmission.ResumeLayout(False)
        CType(Me.LycPccAdmission, System.ComponentModel.ISupportInitialize).EndInit
        Me.LycPccAdmission.ResumeLayout(False)
        CType(Me.TxtContact.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientEstrato.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientEntityName.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtCareGroupPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientAge.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientBirth.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientName.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAfiliationType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPatientCode.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtRiskType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtCareGroupAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtBedStay.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtResponsiblePhone.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtResponsibleName.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtEntityNameAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAtentionCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAuthorization.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtPlaceEntry.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtFunctionalUnitAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAdmissionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAdmissionType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtLiquidationType.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtAdmissionCode.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GdcStaysLiquidated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GdvStaysLiquidated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepEstLiqShowFolios, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PccStayFolios, System.ComponentModel.ISupportInitialize).EndInit
        Me.PccStayFolios.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.GdcStayFolios, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GdvStayFolios, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem38, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepEstLiqShowDetails, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgPccAdmission, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LiCareGroup, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LiEntity, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TxtContacto, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgAdmissionGroup, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgStays, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TabbedControlGroup2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgStaysDontLiquidated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem36, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LycgStaysLiquidated, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem35, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PnlBodySearch, System.ComponentModel.ISupportInitialize).EndInit
        Me.PnlBodySearch.ResumeLayout(False)
        CType(Me.LayoutControl9, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl9.ResumeLayout(False)
        CType(Me.INDGCExportExcel, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup21, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem30, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PccStayDetails, System.ComponentModel.ISupportInitialize).EndInit
        Me.PccStayDetails.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.GdcStayDetails, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GdvStayDetails, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem37, System.ComponentModel.ISupportInitialize).EndInit
        Me.PnlBodyDashboard.ResumeLayout(False)
        Me.hideContainerBottom.ResumeLayout(False)
        Me.PnlNotifications.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.GdcNotifications, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GdvNotifications, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PnlCtrlHeader, System.ComponentModel.ISupportInitialize).EndInit
        Me.PnlCtrlHeader.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl3.ResumeLayout(False)
        CType(Me.MarqueeProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LyciBusyIndicator, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DocumentManager, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.WidgetView, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.StackGroup1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl3.ResumeLayout(False)
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit
        Me.PopupContainerControl1.ResumeLayout(False)
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl4.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PopupContainerControl2, System.ComponentModel.ISupportInitialize).EndInit
        Me.PopupContainerControl2.ResumeLayout(False)
        CType(Me.LayoutControl5, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl5.ResumeLayout(False)
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup10, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem28, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PopupContainerControl3, System.ComponentModel.ISupportInitialize).EndInit
        Me.PopupContainerControl3.ResumeLayout(False)
        CType(Me.LayoutControl6, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl6.ResumeLayout(False)
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit4.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit5.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit6.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit7.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ImageComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ImageComboBoxEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ButtonEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridControl3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit8.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit9.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit10.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit11.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit12.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit13.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit14.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit15.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit16.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit17.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit18.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ImageComboBoxEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ImageComboBoxEdit4.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ButtonEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridControl4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup12, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TabbedControlGroup3, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup13, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem40, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem41, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem42, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem43, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem44, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem45, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem46, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem47, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem48, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem49, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup14, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem50, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem51, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem52, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem53, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem54, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem55, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem56, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem57, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem58, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem59, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem60, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem61, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem62, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem63, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup15, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TabbedControlGroup4, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup16, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem64, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup17, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem65, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem66, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.PanelControl5, System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl5.ResumeLayout(False)
        CType(Me.LayoutControl7, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup18, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControl8, System.ComponentModel.ISupportInitialize).EndInit
        Me.LayoutControl8.ResumeLayout(False)
        CType(Me.SearchLookUpEditExAdmission1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PanelControl6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ButtonEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit19.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridLookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridLookUpEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ImageComboBoxEdit5.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.TextEdit20.Properties, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup19, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup20, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem67, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem68, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem69, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem70, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem71, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem72, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem73, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem74, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem75, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem76, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem77, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem78, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem79, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit
        Me.CtrContainerControl1.ResumeLayout(False)
        Me.AutoHideContainer1.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.ControlContainer1.ResumeLayout(False)
        CType(Me.GridControl5, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(False)
        Me.PerformLayout

    End Sub
    Friend WithEvents LycHeader As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LycgHeader As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LycgGeneralGroup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TxtAdmissionDate1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PccAdmission As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LycPccAdmission As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LycgPccAdmission As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TxtResponsiblePhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtResponsibleName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtEntityNameAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAtentionCenter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAuthorization As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPlaceEntry As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtFunctionalUnitAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtAdmissionDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtBedStay As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LycgAdmissionGroup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LblTotalPatient As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblTotalEntity As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BteCountFolio As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DdbMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PpmActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents MbtnUndo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents MbtnFind As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnLiquidateAll As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnSmallPrintAll As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnViewVoidInvoices As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents TxtPatientEntityName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtCareGroupPatient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientAge As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientBirth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientEstrato As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PnlBodySearch As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem31 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PnlBodyDashboard As Presentation.Controls.CtrContainerControl
    Friend WithEvents DocumentManager As DevExpress.XtraBars.Docking2010.DocumentManager
    Friend WithEvents WidgetView As DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView
    Friend WithEvents StackGroup1 As DevExpress.XtraBars.Docking2010.Views.Widget.StackGroup
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem32 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SleBillingAuthorization As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GdvBillingAuthorization As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SleOperatingUnit As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GdvOperatingUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem33 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem34 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents ColOPCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColOPName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColBACode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColBAName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TxtAdmissionType1 As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtPlaceEntry1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtPatientType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtAfiliationType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtAdmissionType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TxtLiquidationType As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents MbtnPrintAll As DevExpress.XtraBars.BarSubItem
    Friend WithEvents MbtnLargePrintAll As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents LycgStays As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GdcStaysLiquidated As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvStaysLiquidated As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColEstLiqBed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstLiqInitDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstLiqEndDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstLiqTotalDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstLiqType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstLiqFolio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstLiqLiqType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GdcStays As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvStays As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColEstBed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstInitDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstEndDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstTotalUnits As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstTotalTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TabbedControlGroup2 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LycgStaysLiquidated As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem35 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PccStayDetails As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents GdcStayDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvStayDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem37 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColEstShowDetails As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepEstShowDetails As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents LycgStaysDontLiquidated As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem36 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColEstLiqShowDetails As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepEstLiqShowDetails As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents ColEstLiqDetDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstLiqDetCups As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEstLiqDetTotalUnits As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MbtnLiquidateStays As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PnlNotifications As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents GdcNotifications As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvNotifications As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColNotiIcon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNotiTitle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColNotiMessage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ColEstLiqTotalTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GdcStayFolios As DevExpress.XtraGrid.GridControl
    Friend WithEvents GdvStayFolios As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem38 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColStayFoliosNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColStayFoliosValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepEstLiqShowFolios As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents PccStayFolios As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem25 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LiCareGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem29 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem26 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LiEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TxtContact As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TxtContacto As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TxtCareGroupAdmission As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem27 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TxtRiskType As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem39 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents MbtnViewFolios As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents MbtnView As DevExpress.XtraBars.BarSubItem
    Friend WithEvents MbtnFlow As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnNavigation As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TxtPatientCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents TxtAdmissionCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ColEstFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MbtnOpenAdmission As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnConfirmAdmission As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TxtStatusAdmission As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LciStatusAdmission As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents MbtnFacturarIngreso As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MBtnModifyAuthorization As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MBtnAnulateAllAmbulatory As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDSleAdmissionNumber2 As SearchLookUpEditExAdmission
    Friend WithEvents viewSearchAdmission As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciAdmission As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PnlCtrlHeader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents MarqueeProgressBarControl1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem7 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem13 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem12 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarButtonItem14 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem9 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem11 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarSubItem2 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarButtonItem15 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem16 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem10 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem8 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents LyciBusyIndicator As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl4 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup9 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PopupContainerControl2 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl5 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents GridControl2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup10 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup11 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem28 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PopupContainerControl3 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl6 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit4 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit5 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit6 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit7 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ImageComboBoxEdit1 As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents ImageComboBoxEdit2 As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents ButtonEdit1 As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents GridControl3 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TextEdit8 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit9 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit10 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit11 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit12 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit13 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit14 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit15 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit16 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit17 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit18 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ImageComboBoxEdit3 As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents ImageComboBoxEdit4 As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents ButtonEdit2 As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents GridControl4 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents LayoutControlGroup12 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TabbedControlGroup3 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup13 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem40 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem41 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem42 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem43 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem44 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem45 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem46 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem47 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem48 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem49 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup14 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem50 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem51 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem52 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem53 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem54 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem55 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem56 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem57 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem58 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem59 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem60 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem61 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem62 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem63 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup15 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents TabbedControlGroup4 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup16 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem64 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup17 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem65 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem66 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl5 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl7 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup18 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControl8 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents SearchLookUpEditExAdmission1 As SearchLookUpEditExAdmission
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl6 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents DropDownButton1 As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents ButtonEdit3 As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TextEdit19 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GridLookUpEdit1 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridLookUpEdit2 As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ImageComboBoxEdit5 As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents TextEdit20 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup19 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup20 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem67 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem68 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem69 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem70 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem71 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem72 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem73 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem74 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem75 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem76 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem77 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem78 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem79 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents CtrContainerControl1 As CtrContainerControl
    Friend WithEvents AutoHideContainer1 As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer1 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents GridControl5 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents MBtnIngresosRelacionados As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents CtrNoData1 As CtrNoData
    Friend WithEvents PnlProgressPanel As DevExpress.XtraWaitForm.ProgressPanel
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents MbtnIncomeLock As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnIncomeUnlock As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem17 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnLiquidateData As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents LayoutControl9 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup21 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGCExportExcel As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem30 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Mbtn As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MbtnShowMotherAccountReport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColSubtotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDiscount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNetValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents MbtnPrintDetailandParentAccount As DevExpress.XtraBars.BarButtonItem
End Class
