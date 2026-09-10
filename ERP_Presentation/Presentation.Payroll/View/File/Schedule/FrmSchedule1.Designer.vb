<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSchedule1
    Inherits Presentation.Controls.FormBase

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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSchedule1))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDCalendar = New Presentation.Controls.CtrCalendar()
        Me.INDPcCDetail = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcDetHours = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetHours = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEvent = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNextDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColApproval = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBarNewScheduleDetail = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBarMarkAsDelete = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBarDeleteAll = New DevExpress.XtraBars.BarButtonItem()
        Me.BarCheckItem1 = New DevExpress.XtraBars.BarCheckItem()
        Me.INDBarMarkEmployee = New DevExpress.XtraBars.BarCheckItem()
        Me.INDBarRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBarEmployee = New DevExpress.XtraBars.BarButtonItem()
        Me.INDPopupLcTxtTemplateHoursNumber = New DevExpress.XtraEditors.LabelControl()
        Me.INDPopupSBEdit = New DevExpress.XtraEditors.SimpleButton()
        Me.INDPcCMore = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.LayoutControl4 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPopupMoreLcTxtOtherSchHoursNumber = New DevExpress.XtraEditors.LabelControl()
        Me.INDPopupMoreGcHoursDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDPopupMoreGvHoursDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FunctionalUnitMore = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Template = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPopupMoreLcNumberDay = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemOtherSchDet = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPopupMoreLcEmployee = New DevExpress.XtraEditors.LabelControl()
        Me.INDPopupGcMatches = New DevExpress.XtraGrid.GridControl()
        Me.INDPopupGvMatches = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCOlEmployeeNameDetailMates = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPopupLcTxtTemplate = New DevExpress.XtraEditors.LabelControl()
        Me.INDPopupLcNumberDay = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemDetailDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemDetailTemplate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemDetailEdit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTcgDetailsSchedule = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlyGrDetHours = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCtrGrMatchPartnerts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemDetailScheduleMates = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemDetailTemplateHoursNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPopupLcEmployee = New DevExpress.XtraEditors.LabelControl()
        Me.INDLySchedule = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSlPosition = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cargoName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDChkLbcEmployee = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.INDPcCHours = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.LayoutControl3 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPopupHoursGcHoursPendingDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDPopupHoursGvHoursPendingDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPopupHoursWorkedPercentage = New DevExpress.XtraEditors.LabelControl()
        Me.INDPopupHoursMinNumber = New DevExpress.XtraEditors.LabelControl()
        Me.INDPopupHoursMaxNumber = New DevExpress.XtraEditors.LabelControl()
        Me.INDPopupHoursGcHoursDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDPopupHoursGvHoursDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColConceptName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColHoursNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPopupHoursWorkedNumber = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemWorkedHours = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemMaxHours = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemMinHours = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTcgHours = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLyGrHourLiquidation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemHoursGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGrPendingEvents = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemHoursPendingGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemWorkedPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPopupHoursLcEmployee = New DevExpress.XtraEditors.LabelControl()
        Me.INDPcScheduleDetailComplete = New DevExpress.XtraEditors.PanelControl()
        Me.INDPcDetailEm1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDLcTotalHoursNumber1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDLcTotalHours1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDDDBEmployee1 = New DevExpress.XtraEditors.DropDownButton()
        Me.INDLcEmployee1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDLcEmployee2 = New DevExpress.XtraEditors.LabelControl()
        Me.INDPcDetailEm2 = New DevExpress.XtraEditors.PanelControl()
        Me.INDLcTotalHoursNumber2 = New DevExpress.XtraEditors.LabelControl()
        Me.INDLcTotalHours2 = New DevExpress.XtraEditors.LabelControl()
        Me.INDDDBEmployee2 = New DevExpress.XtraEditors.DropDownButton()
        Me.INDLyDelete = New DevExpress.XtraLayout.LayoutControl()
        Me.INDDelSBCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDDelSBFinish = New DevExpress.XtraEditors.SimpleButton()
        Me.INDLcSelectSchedule = New DevExpress.XtraEditors.LabelControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemTxtDeleteInformation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemButtonFinishDelete = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemButtonCancelDelete = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDDDBOptionsMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPopMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDLcEmployees = New DevExpress.XtraEditors.LabelControl()
        Me.INDGcScheduleDetail1 = New DevExpress.XtraGrid.GridControl()
        Me.INDGvScheduleDetail1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CtrDateNavigator1 = New Presentation.Controls.CtrDateNavigator()
        Me.INDPcMainControls = New DevExpress.XtraEditors.PanelControl()
        Me.INDSleFunctionalUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvFunctionalUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBOName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCompanyDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDDBFunctionalUnit = New DevExpress.XtraEditors.DropDownButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemCalendar = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGrFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSpace2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDSpace1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDLyItemEmployeeList = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemScheduleDetailGrid1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemTxtEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemOptionMenu = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDLyItemLayoutDelete = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemScheduleDetailPanelComplete = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPosition = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcCDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcCDetail.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDgcDetHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDetHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcCMore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcCMore.SuspendLayout()
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl4.SuspendLayout()
        CType(Me.INDPopupMoreGcHoursDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupMoreGvHoursDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemOtherSchDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupGcMatches, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupGvMatches, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgDetailsSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrDetHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtrGrMatchPartnerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailScheduleMates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailTemplateHoursNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLySchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLySchedule.SuspendLayout()
        CType(Me.INDSlPosition.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkLbcEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcCHours, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcCHours.SuspendLayout()
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl3.SuspendLayout()
        CType(Me.INDPopupHoursGcHoursPendingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupHoursGvHoursPendingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupHoursGcHoursDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupHoursGvHoursDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemWorkedHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemMaxHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemMinHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrHourLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemHoursGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrPendingEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemHoursPendingGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemWorkedPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcScheduleDetailComplete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcScheduleDetailComplete.SuspendLayout()
        CType(Me.INDPcDetailEm1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcDetailEm1.SuspendLayout()
        CType(Me.INDPcDetailEm2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcDetailEm2.SuspendLayout()
        CType(Me.INDLyDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyDelete.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemTxtDeleteInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemButtonFinishDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemButtonCancelDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcScheduleDetail1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvScheduleDetail1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcMainControls, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcMainControls.SuspendLayout()
        CType(Me.INDSleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemCalendar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpace2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpace1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemEmployeeList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemScheduleDetailGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemTxtEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemOptionMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemLayoutDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemScheduleDetailPanelComplete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLySchedule)
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
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDCalendar
        '
        Me.INDCalendar.DaySelected = 0
        Me.INDCalendar.IncrementX = 119
        Me.INDCalendar.IncrementY = 79
        Me.INDCalendar.ListDaysToDelete = CType(resources.GetObject("INDCalendar.ListDaysToDelete"), System.Collections.Generic.List(Of Integer))
        Me.INDCalendar.ListHoliday = Nothing
        resources.ApplyResources(Me.INDCalendar, "INDCalendar")
        Me.INDCalendar.LocationX = 0
        Me.INDCalendar.LocationY = 35
        Me.INDCalendar.ModeDelete = False
        Me.INDCalendar.ModeDeleteColor = System.Drawing.Color.MistyRose
        Me.INDCalendar.MonthControl = 9
        Me.INDCalendar.Name = "INDCalendar"
        Me.INDCalendar.PopUpControlToShowDetail = Me.INDPcCDetail
        Me.INDCalendar.PopUpControlToShowMore = Me.INDPcCMore
        Me.INDCalendar.RegColor1 = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.INDCalendar.RegColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer))
        Me.INDCalendar.SizeChilds = New System.Drawing.Size(120, 80)
        Me.INDCalendar.YearControl = 2013
        '
        'INDPcCDetail
        '
        Me.INDPcCDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcCDetail.Controls.Add(Me.LayoutControl2)
        Me.INDPcCDetail.Controls.Add(Me.INDPopupLcEmployee)
        resources.ApplyResources(Me.INDPcCDetail, "INDPcCDetail")
        Me.INDPcCDetail.Manager = Me.BarManager1
        Me.INDPcCDetail.Name = "INDPcCDetail"
        '
        'LayoutControl2
        '
        Me.LayoutControl2.BackColor = System.Drawing.Color.White
        Me.LayoutControl2.Controls.Add(Me.INDgcDetHours)
        Me.LayoutControl2.Controls.Add(Me.INDPopupLcTxtTemplateHoursNumber)
        Me.LayoutControl2.Controls.Add(Me.INDPopupSBEdit)
        Me.LayoutControl2.Controls.Add(Me.INDPcCMore)
        Me.LayoutControl2.Controls.Add(Me.INDPopupGcMatches)
        Me.LayoutControl2.Controls.Add(Me.INDPopupLcTxtTemplate)
        Me.LayoutControl2.Controls.Add(Me.INDPopupLcNumberDay)
        resources.ApplyResources(Me.LayoutControl2, "LayoutControl2")
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        '
        'INDgcDetHours
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetHours, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetHours, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetHours, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetHours, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetHours, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetHours, False)
        resources.ApplyResources(Me.INDgcDetHours, "INDgcDetHours")
        Me.INDgcDetHours.MainView = Me.INDgvDetHours
        Me.INDgcDetHours.MenuManager = Me.BarManager1
        Me.INDgcDetHours.Name = "INDgcDetHours"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetHours, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDetHours.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetHours})
        '
        'INDgvDetHours
        '
        Me.INDgvDetHours.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetHours.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgvDetHours.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgvDetHours.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDetHours.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDetHours.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDetHours.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvDetHours.Appearance.GroupRow.Font = CType(resources.GetObject("INDgvDetHours.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgvDetHours.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDetHours.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgvDetHours.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgvDetHours.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDetHours.Appearance.Row.Font = CType(resources.GetObject("INDgvDetHours.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgvDetHours.Appearance.Row.Options.UseFont = True
        Me.INDgvDetHours.Appearance.ViewCaption.Font = CType(resources.GetObject("INDgvDetHours.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDgvDetHours.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDetHours.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColStart, Me.INDColEnd, Me.INDColEvent, Me.INDColNextDay, Me.INDColApproval})
        Me.INDgvDetHours.GridControl = Me.INDgcDetHours
        Me.INDgvDetHours.Name = "INDgvDetHours"
        Me.INDgvDetHours.OptionsCustomization.AllowColumnMoving = False
        Me.INDgvDetHours.OptionsCustomization.AllowFilter = False
        Me.INDgvDetHours.OptionsCustomization.AllowGroup = False
        Me.INDgvDetHours.OptionsMenu.EnableColumnMenu = False
        Me.INDgvDetHours.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDetHours.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDetHours.OptionsView.ShowAutoFilterRow = True
        Me.INDgvDetHours.OptionsView.ShowDetailButtons = False
        Me.INDgvDetHours.OptionsView.ShowGroupPanel = False
        Me.INDgvDetHours.OptionsView.ShowIndicator = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDetHours, False)
        '
        'INDColStart
        '
        resources.ApplyResources(Me.INDColStart, "INDColStart")
        Me.INDColStart.Name = "INDColStart"
        Me.INDColStart.OptionsColumn.AllowEdit = False
        '
        'INDColEnd
        '
        resources.ApplyResources(Me.INDColEnd, "INDColEnd")
        Me.INDColEnd.Name = "INDColEnd"
        Me.INDColEnd.OptionsColumn.AllowEdit = False
        '
        'INDColEvent
        '
        resources.ApplyResources(Me.INDColEvent, "INDColEvent")
        Me.INDColEvent.FieldName = "Event"
        Me.INDColEvent.Name = "INDColEvent"
        Me.INDColEvent.OptionsColumn.AllowEdit = False
        '
        'INDColNextDay
        '
        resources.ApplyResources(Me.INDColNextDay, "INDColNextDay")
        Me.INDColNextDay.FieldName = "NextDay"
        Me.INDColNextDay.Name = "INDColNextDay"
        Me.INDColNextDay.OptionsColumn.AllowEdit = False
        '
        'INDColApproval
        '
        resources.ApplyResources(Me.INDColApproval, "INDColApproval")
        Me.INDColApproval.FieldName = "Approved"
        Me.INDColApproval.Name = "INDColApproval"
        Me.INDColApproval.OptionsColumn.AllowEdit = False
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.DockControls.Add(Me.BarDockControl2)
        Me.BarManager1.DockControls.Add(Me.BarDockControl3)
        Me.BarManager1.DockControls.Add(Me.BarDockControl4)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBarNewScheduleDetail, Me.INDBarMarkAsDelete, Me.INDBarDeleteAll, Me.BarCheckItem1, Me.INDBarMarkEmployee, Me.INDBarRefresh, Me.INDBarEmployee})
        Me.BarManager1.MaxItemId = 7
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
        'INDBarNewScheduleDetail
        '
        resources.ApplyResources(Me.INDBarNewScheduleDetail, "INDBarNewScheduleDetail")
        Me.INDBarNewScheduleDetail.Id = 0
        Me.INDBarNewScheduleDetail.Name = "INDBarNewScheduleDetail"
        '
        'INDBarMarkAsDelete
        '
        resources.ApplyResources(Me.INDBarMarkAsDelete, "INDBarMarkAsDelete")
        Me.INDBarMarkAsDelete.Id = 1
        Me.INDBarMarkAsDelete.Name = "INDBarMarkAsDelete"
        Me.INDBarMarkAsDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'INDBarDeleteAll
        '
        resources.ApplyResources(Me.INDBarDeleteAll, "INDBarDeleteAll")
        Me.INDBarDeleteAll.Id = 2
        Me.INDBarDeleteAll.Name = "INDBarDeleteAll"
        Me.INDBarDeleteAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'BarCheckItem1
        '
        resources.ApplyResources(Me.BarCheckItem1, "BarCheckItem1")
        Me.BarCheckItem1.Id = 3
        Me.BarCheckItem1.Name = "BarCheckItem1"
        '
        'INDBarMarkEmployee
        '
        resources.ApplyResources(Me.INDBarMarkEmployee, "INDBarMarkEmployee")
        Me.INDBarMarkEmployee.Id = 4
        Me.INDBarMarkEmployee.Name = "INDBarMarkEmployee"
        '
        'INDBarRefresh
        '
        resources.ApplyResources(Me.INDBarRefresh, "INDBarRefresh")
        Me.INDBarRefresh.Id = 5
        Me.INDBarRefresh.Name = "INDBarRefresh"
        '
        'INDBarEmployee
        '
        resources.ApplyResources(Me.INDBarEmployee, "INDBarEmployee")
        Me.INDBarEmployee.Id = 6
        Me.INDBarEmployee.Name = "INDBarEmployee"
        '
        'INDPopupLcTxtTemplateHoursNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupLcTxtTemplateHoursNumber, True)
        Me.INDPopupLcTxtTemplateHoursNumber.Appearance.Font = CType(resources.GetObject("INDPopupLcTxtTemplateHoursNumber.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupLcTxtTemplateHoursNumber.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDPopupLcTxtTemplateHoursNumber.Appearance.Options.UseFont = True
        Me.INDPopupLcTxtTemplateHoursNumber.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupLcTxtTemplateHoursNumber, False)
        resources.ApplyResources(Me.INDPopupLcTxtTemplateHoursNumber, "INDPopupLcTxtTemplateHoursNumber")
        Me.INDPopupLcTxtTemplateHoursNumber.Name = "INDPopupLcTxtTemplateHoursNumber"
        Me.INDPopupLcTxtTemplateHoursNumber.StyleController = Me.LayoutControl2
        '
        'INDPopupSBEdit
        '
        Me.INDPopupSBEdit.Appearance.Font = CType(resources.GetObject("INDPopupSBEdit.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupSBEdit.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDPopupSBEdit, "INDPopupSBEdit")
        Me.INDPopupSBEdit.Name = "INDPopupSBEdit"
        Me.INDPopupSBEdit.StyleController = Me.LayoutControl2
        '
        'INDPcCMore
        '
        Me.INDPcCMore.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcCMore.Controls.Add(Me.LayoutControl4)
        Me.INDPcCMore.Controls.Add(Me.INDPopupMoreLcEmployee)
        resources.ApplyResources(Me.INDPcCMore, "INDPcCMore")
        Me.INDPcCMore.Manager = Me.BarManager1
        Me.INDPcCMore.Name = "INDPcCMore"
        '
        'LayoutControl4
        '
        Me.LayoutControl4.BackColor = System.Drawing.Color.White
        Me.LayoutControl4.Controls.Add(Me.INDPopupMoreLcTxtOtherSchHoursNumber)
        Me.LayoutControl4.Controls.Add(Me.INDPopupMoreGcHoursDetail)
        Me.LayoutControl4.Controls.Add(Me.INDPopupMoreLcNumberDay)
        resources.ApplyResources(Me.LayoutControl4, "LayoutControl4")
        Me.LayoutControl4.Name = "LayoutControl4"
        Me.LayoutControl4.Root = Me.LayoutControlGroup4
        '
        'INDPopupMoreLcTxtOtherSchHoursNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupMoreLcTxtOtherSchHoursNumber, True)
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.Font = CType(resources.GetObject("INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.Options.UseFont = True
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupMoreLcTxtOtherSchHoursNumber, False)
        resources.ApplyResources(Me.INDPopupMoreLcTxtOtherSchHoursNumber, "INDPopupMoreLcTxtOtherSchHoursNumber")
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Name = "INDPopupMoreLcTxtOtherSchHoursNumber"
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.StyleController = Me.LayoutControl4
        '
        'INDPopupMoreGcHoursDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDPopupMoreGcHoursDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDPopupMoreGcHoursDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDPopupMoreGcHoursDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDPopupMoreGcHoursDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDPopupMoreGcHoursDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDPopupMoreGcHoursDetail, False)
        resources.ApplyResources(Me.INDPopupMoreGcHoursDetail, "INDPopupMoreGcHoursDetail")
        Me.INDPopupMoreGcHoursDetail.MainView = Me.INDPopupMoreGvHoursDetail
        Me.INDPopupMoreGcHoursDetail.MenuManager = Me.BarManager1
        Me.INDPopupMoreGcHoursDetail.Name = "INDPopupMoreGcHoursDetail"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDPopupMoreGcHoursDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDPopupMoreGcHoursDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDPopupMoreGvHoursDetail})
        '
        'INDPopupMoreGvHoursDetail
        '
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Font = CType(resources.GetObject("INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDPopupMoreGvHoursDetail.Appearance.GroupRow.Font = CType(resources.GetObject("INDPopupMoreGvHoursDetail.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDPopupMoreGvHoursDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDPopupMoreGvHoursDetail.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDPopupMoreGvHoursDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Appearance.Row.Font = CType(resources.GetObject("INDPopupMoreGvHoursDetail.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDPopupMoreGvHoursDetail.Appearance.Row.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Appearance.ViewCaption.Font = CType(resources.GetObject("INDPopupMoreGvHoursDetail.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDPopupMoreGvHoursDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FunctionalUnitMore, Me.Template})
        Me.INDPopupMoreGvHoursDetail.GridControl = Me.INDPopupMoreGcHoursDetail
        Me.INDPopupMoreGvHoursDetail.Name = "INDPopupMoreGvHoursDetail"
        Me.INDPopupMoreGvHoursDetail.OptionsBehavior.Editable = False
        Me.INDPopupMoreGvHoursDetail.OptionsCustomization.AllowColumnMoving = False
        Me.INDPopupMoreGvHoursDetail.OptionsCustomization.AllowFilter = False
        Me.INDPopupMoreGvHoursDetail.OptionsCustomization.AllowGroup = False
        Me.INDPopupMoreGvHoursDetail.OptionsMenu.EnableColumnMenu = False
        Me.INDPopupMoreGvHoursDetail.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDPopupMoreGvHoursDetail.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDPopupMoreGvHoursDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDPopupMoreGvHoursDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDPopupMoreGvHoursDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDPopupMoreGvHoursDetail.OptionsView.ShowDetailButtons = False
        Me.INDPopupMoreGvHoursDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDPopupMoreGvHoursDetail.OptionsView.ShowGroupPanel = False
        Me.INDPopupMoreGvHoursDetail.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDPopupMoreGvHoursDetail.OptionsView.ShowIndicator = False
        Me.INDPopupMoreGvHoursDetail.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDPopupMoreGvHoursDetail.Tag = 140
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDPopupMoreGvHoursDetail, False)
        '
        'FunctionalUnitMore
        '
        resources.ApplyResources(Me.FunctionalUnitMore, "FunctionalUnitMore")
        Me.FunctionalUnitMore.FieldName = "DetMoreFU"
        Me.FunctionalUnitMore.Name = "FunctionalUnitMore"
        '
        'Template
        '
        resources.ApplyResources(Me.Template, "Template")
        Me.Template.FieldName = "DetMoreT"
        Me.Template.Name = "Template"
        '
        'INDPopupMoreLcNumberDay
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupMoreLcNumberDay, True)
        Me.INDPopupMoreLcNumberDay.Appearance.Font = CType(resources.GetObject("INDPopupMoreLcNumberDay.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupMoreLcNumberDay.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDPopupMoreLcNumberDay.Appearance.Options.UseFont = True
        Me.INDPopupMoreLcNumberDay.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupMoreLcNumberDay, False)
        resources.ApplyResources(Me.INDPopupMoreLcNumberDay, "INDPopupMoreLcNumberDay")
        Me.INDPopupMoreLcNumberDay.Name = "INDPopupMoreLcNumberDay"
        Me.INDPopupMoreLcNumberDay.StyleController = Me.LayoutControl4
        '
        'LayoutControlGroup4
        '
        resources.ApplyResources(Me.LayoutControlGroup4, "LayoutControlGroup4")
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDLyItemOtherSchDet, Me.LayoutControlItem6})
        Me.LayoutControlGroup4.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(328, 210)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDPopupMoreLcNumberDay
        resources.ApplyResources(Me.LayoutControlItem2, "LayoutControlItem2")
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(102, 25)
        Me.LayoutControlItem2.Name = "INDLyItemDetailDate"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(308, 25)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLyItemOtherSchDet
        '
        Me.INDLyItemOtherSchDet.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemOtherSchDet.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemOtherSchDet.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.INDLyItemOtherSchDet.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemOtherSchDet.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemOtherSchDet.Control = Me.INDPopupMoreGcHoursDetail
        resources.ApplyResources(Me.INDLyItemOtherSchDet, "INDLyItemOtherSchDet")
        Me.INDLyItemOtherSchDet.Location = New System.Drawing.Point(0, 50)
        Me.INDLyItemOtherSchDet.MinSize = New System.Drawing.Size(156, 48)
        Me.INDLyItemOtherSchDet.Name = "INDLyItemOtherSchDet"
        Me.INDLyItemOtherSchDet.Size = New System.Drawing.Size(308, 140)
        Me.INDLyItemOtherSchDet.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemOtherSchDet.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyItemOtherSchDet.TextSize = New System.Drawing.Size(266, 21)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlItem6.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlItem6.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem6.Control = Me.INDPopupMoreLcTxtOtherSchHoursNumber
        resources.ApplyResources(Me.LayoutControlItem6, "LayoutControlItem6")
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 25)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(96, 25)
        Me.LayoutControlItem6.Name = "INDLyItemDetailTemplateHoursNumber"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(308, 25)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(80, 21)
        Me.LayoutControlItem6.TextToControlDistance = 12
        '
        'INDPopupMoreLcEmployee
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupMoreLcEmployee, True)
        Me.INDPopupMoreLcEmployee.Appearance.Font = CType(resources.GetObject("INDPopupMoreLcEmployee.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupMoreLcEmployee.Appearance.Options.UseFont = True
        Me.INDPopupMoreLcEmployee.Appearance.Options.UseTextOptions = True
        Me.INDPopupMoreLcEmployee.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.INDPopupMoreLcEmployee, "INDPopupMoreLcEmployee")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupMoreLcEmployee, False)
        Me.INDPopupMoreLcEmployee.Name = "INDPopupMoreLcEmployee"
        '
        'INDPopupGcMatches
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDPopupGcMatches, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDPopupGcMatches, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDPopupGcMatches, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDPopupGcMatches, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDPopupGcMatches, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDPopupGcMatches, False)
        resources.ApplyResources(Me.INDPopupGcMatches, "INDPopupGcMatches")
        Me.INDPopupGcMatches.MainView = Me.INDPopupGvMatches
        Me.INDPopupGcMatches.MenuManager = Me.BarManager1
        Me.INDPopupGcMatches.Name = "INDPopupGcMatches"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDPopupGcMatches, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDPopupGcMatches.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDPopupGvMatches})
        '
        'INDPopupGvMatches
        '
        Me.INDPopupGvMatches.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDPopupGvMatches.Appearance.FocusedRow.Font = CType(resources.GetObject("INDPopupGvMatches.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDPopupGvMatches.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDPopupGvMatches.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDPopupGvMatches.Appearance.FocusedRow.Options.UseFont = True
        Me.INDPopupGvMatches.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDPopupGvMatches.Appearance.GroupRow.Font = CType(resources.GetObject("INDPopupGvMatches.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDPopupGvMatches.Appearance.GroupRow.Options.UseFont = True
        Me.INDPopupGvMatches.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDPopupGvMatches.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDPopupGvMatches.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDPopupGvMatches.Appearance.Row.Font = CType(resources.GetObject("INDPopupGvMatches.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDPopupGvMatches.Appearance.Row.Options.UseFont = True
        Me.INDPopupGvMatches.Appearance.ViewCaption.Font = CType(resources.GetObject("INDPopupGvMatches.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDPopupGvMatches.Appearance.ViewCaption.Options.UseFont = True
        Me.INDPopupGvMatches.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCOlEmployeeNameDetailMates})
        Me.INDPopupGvMatches.GridControl = Me.INDPopupGcMatches
        Me.INDPopupGvMatches.Name = "INDPopupGvMatches"
        Me.INDPopupGvMatches.OptionsBehavior.Editable = False
        Me.INDPopupGvMatches.OptionsCustomization.AllowColumnMoving = False
        Me.INDPopupGvMatches.OptionsCustomization.AllowColumnResizing = False
        Me.INDPopupGvMatches.OptionsMenu.EnableColumnMenu = False
        Me.INDPopupGvMatches.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDPopupGvMatches.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDPopupGvMatches.OptionsView.EnableAppearanceEvenRow = True
        Me.INDPopupGvMatches.OptionsView.EnableAppearanceOddRow = True
        Me.INDPopupGvMatches.OptionsView.ShowAutoFilterRow = True
        Me.INDPopupGvMatches.OptionsView.ShowColumnHeaders = False
        Me.INDPopupGvMatches.OptionsView.ShowDetailButtons = False
        Me.INDPopupGvMatches.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDPopupGvMatches.OptionsView.ShowGroupPanel = False
        Me.INDPopupGvMatches.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDPopupGvMatches.OptionsView.ShowIndicator = False
        Me.INDPopupGvMatches.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDPopupGvMatches.Tag = 90
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDPopupGvMatches, False)
        '
        'INDCOlEmployeeNameDetailMates
        '
        resources.ApplyResources(Me.INDCOlEmployeeNameDetailMates, "INDCOlEmployeeNameDetailMates")
        Me.INDCOlEmployeeNameDetailMates.FieldName = "Employee.ThirdParty.Name"
        Me.INDCOlEmployeeNameDetailMates.Name = "INDCOlEmployeeNameDetailMates"
        '
        'INDPopupLcTxtTemplate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupLcTxtTemplate, True)
        Me.INDPopupLcTxtTemplate.Appearance.Font = CType(resources.GetObject("INDPopupLcTxtTemplate.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupLcTxtTemplate.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDPopupLcTxtTemplate.Appearance.Options.UseFont = True
        Me.INDPopupLcTxtTemplate.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupLcTxtTemplate, False)
        resources.ApplyResources(Me.INDPopupLcTxtTemplate, "INDPopupLcTxtTemplate")
        Me.INDPopupLcTxtTemplate.Name = "INDPopupLcTxtTemplate"
        Me.INDPopupLcTxtTemplate.StyleController = Me.LayoutControl2
        '
        'INDPopupLcNumberDay
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupLcNumberDay, True)
        Me.INDPopupLcNumberDay.Appearance.Font = CType(resources.GetObject("INDPopupLcNumberDay.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupLcNumberDay.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupLcNumberDay, False)
        resources.ApplyResources(Me.INDPopupLcNumberDay, "INDPopupLcNumberDay")
        Me.INDPopupLcNumberDay.Name = "INDPopupLcNumberDay"
        Me.INDPopupLcNumberDay.StyleController = Me.LayoutControl2
        '
        'LayoutControlGroup2
        '
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemDetailDate, Me.INDLyItemDetailTemplate, Me.INDLyItemDetailEdit, Me.INDTcgDetailsSchedule, Me.INDLyItemDetailTemplateHoursNumber})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(332, 241)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLyItemDetailDate
        '
        Me.INDLyItemDetailDate.Control = Me.INDPopupLcNumberDay
        resources.ApplyResources(Me.INDLyItemDetailDate, "INDLyItemDetailDate")
        Me.INDLyItemDetailDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemDetailDate.MinSize = New System.Drawing.Size(102, 25)
        Me.INDLyItemDetailDate.Name = "INDLyItemDetailDate"
        Me.INDLyItemDetailDate.Size = New System.Drawing.Size(312, 25)
        Me.INDLyItemDetailDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailDate.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemDetailDate.TextVisible = False
        '
        'INDLyItemDetailTemplate
        '
        Me.INDLyItemDetailTemplate.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemDetailTemplate.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemDetailTemplate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.INDLyItemDetailTemplate.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemDetailTemplate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemDetailTemplate.Control = Me.INDPopupLcTxtTemplate
        resources.ApplyResources(Me.INDLyItemDetailTemplate, "INDLyItemDetailTemplate")
        Me.INDLyItemDetailTemplate.Location = New System.Drawing.Point(0, 25)
        Me.INDLyItemDetailTemplate.MinSize = New System.Drawing.Size(76, 25)
        Me.INDLyItemDetailTemplate.Name = "INDLyItemDetailTemplate"
        Me.INDLyItemDetailTemplate.Size = New System.Drawing.Size(207, 25)
        Me.INDLyItemDetailTemplate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailTemplate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemDetailTemplate.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLyItemDetailTemplate.TextToControlDistance = 12
        '
        'INDLyItemDetailEdit
        '
        Me.INDLyItemDetailEdit.Control = Me.INDPopupSBEdit
        resources.ApplyResources(Me.INDLyItemDetailEdit, "INDLyItemDetailEdit")
        Me.INDLyItemDetailEdit.Location = New System.Drawing.Point(0, 185)
        Me.INDLyItemDetailEdit.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLyItemDetailEdit.MinSize = New System.Drawing.Size(247, 36)
        Me.INDLyItemDetailEdit.Name = "INDLyItemDetailEdit"
        Me.INDLyItemDetailEdit.Size = New System.Drawing.Size(312, 36)
        Me.INDLyItemDetailEdit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailEdit.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemDetailEdit.TextVisible = False
        '
        'INDTcgDetailsSchedule
        '
        Me.INDTcgDetailsSchedule.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDTcgDetailsSchedule.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDTcgDetailsSchedule.AppearanceTabPage.Header.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDTcgDetailsSchedule.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDTcgDetailsSchedule.AppearanceTabPage.Header.Options.UseForeColor = True
        Me.INDTcgDetailsSchedule.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDTcgDetailsSchedule.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDTcgDetailsSchedule.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDTcgDetailsSchedule.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDTcgDetailsSchedule.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDTcgDetailsSchedule.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        resources.ApplyResources(Me.INDTcgDetailsSchedule, "INDTcgDetailsSchedule")
        Me.INDTcgDetailsSchedule.Location = New System.Drawing.Point(0, 50)
        Me.INDTcgDetailsSchedule.Name = "INDTcgDetailsSchedule"
        Me.INDTcgDetailsSchedule.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 9, 2, 9)
        Me.INDTcgDetailsSchedule.SelectedTabPage = Me.INDlyGrDetHours
        Me.INDTcgDetailsSchedule.Size = New System.Drawing.Size(312, 135)
        Me.INDTcgDetailsSchedule.Spacing = New DevExpress.XtraLayout.Utils.Padding(2, -2, 10, -2)
        Me.INDTcgDetailsSchedule.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrDetHours, Me.INDlyCtrGrMatchPartnerts})
        '
        'INDlyGrDetHours
        '
        resources.ApplyResources(Me.INDlyGrDetHours, "INDlyGrDetHours")
        Me.INDlyGrDetHours.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.INDlyGrDetHours.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrDetHours.Name = "INDlyGrDetHours"
        Me.INDlyGrDetHours.Size = New System.Drawing.Size(292, 83)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDgcDetHours
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(292, 83)
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'INDlyCtrGrMatchPartnerts
        '
        resources.ApplyResources(Me.INDlyCtrGrMatchPartnerts, "INDlyCtrGrMatchPartnerts")
        Me.INDlyCtrGrMatchPartnerts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemDetailScheduleMates})
        Me.INDlyCtrGrMatchPartnerts.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCtrGrMatchPartnerts.Name = "INDlyCtrGrMatchPartnerts"
        Me.INDlyCtrGrMatchPartnerts.Size = New System.Drawing.Size(292, 83)
        '
        'INDLyItemDetailScheduleMates
        '
        Me.INDLyItemDetailScheduleMates.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemDetailScheduleMates.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemDetailScheduleMates.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.INDLyItemDetailScheduleMates.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemDetailScheduleMates.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemDetailScheduleMates.Control = Me.INDPopupGcMatches
        resources.ApplyResources(Me.INDLyItemDetailScheduleMates, "INDLyItemDetailScheduleMates")
        Me.INDLyItemDetailScheduleMates.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemDetailScheduleMates.MinSize = New System.Drawing.Size(156, 48)
        Me.INDLyItemDetailScheduleMates.Name = "INDLyItemDetailScheduleMates"
        Me.INDLyItemDetailScheduleMates.Size = New System.Drawing.Size(292, 83)
        Me.INDLyItemDetailScheduleMates.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailScheduleMates.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyItemDetailScheduleMates.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemDetailScheduleMates.TextVisible = False
        '
        'INDLyItemDetailTemplateHoursNumber
        '
        Me.INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemDetailTemplateHoursNumber.Control = Me.INDPopupLcTxtTemplateHoursNumber
        resources.ApplyResources(Me.INDLyItemDetailTemplateHoursNumber, "INDLyItemDetailTemplateHoursNumber")
        Me.INDLyItemDetailTemplateHoursNumber.Location = New System.Drawing.Point(207, 25)
        Me.INDLyItemDetailTemplateHoursNumber.MinSize = New System.Drawing.Size(96, 25)
        Me.INDLyItemDetailTemplateHoursNumber.Name = "INDLyItemDetailTemplateHoursNumber"
        Me.INDLyItemDetailTemplateHoursNumber.Size = New System.Drawing.Size(105, 25)
        Me.INDLyItemDetailTemplateHoursNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailTemplateHoursNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemDetailTemplateHoursNumber.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLyItemDetailTemplateHoursNumber.TextToControlDistance = 12
        '
        'INDPopupLcEmployee
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupLcEmployee, True)
        Me.INDPopupLcEmployee.Appearance.Font = CType(resources.GetObject("INDPopupLcEmployee.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupLcEmployee.Appearance.Options.UseFont = True
        Me.INDPopupLcEmployee.Appearance.Options.UseTextOptions = True
        Me.INDPopupLcEmployee.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.INDPopupLcEmployee, "INDPopupLcEmployee")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupLcEmployee, False)
        Me.INDPopupLcEmployee.Name = "INDPopupLcEmployee"
        '
        'INDLySchedule
        '
        Me.INDLySchedule.AllowCustomization = False
        Me.INDLySchedule.BackColor = System.Drawing.Color.Transparent
        Me.INDLySchedule.Controls.Add(Me.INDSlPosition)
        Me.INDLySchedule.Controls.Add(Me.INDChkLbcEmployee)
        Me.INDLySchedule.Controls.Add(Me.INDPcCHours)
        Me.INDLySchedule.Controls.Add(Me.INDPcCDetail)
        Me.INDLySchedule.Controls.Add(Me.INDPcScheduleDetailComplete)
        Me.INDLySchedule.Controls.Add(Me.INDLyDelete)
        Me.INDLySchedule.Controls.Add(Me.INDDDBOptionsMenu)
        Me.INDLySchedule.Controls.Add(Me.INDLcEmployees)
        Me.INDLySchedule.Controls.Add(Me.INDGcScheduleDetail1)
        Me.INDLySchedule.Controls.Add(Me.CtrDateNavigator1)
        Me.INDLySchedule.Controls.Add(Me.INDPcMainControls)
        Me.INDLySchedule.Controls.Add(Me.INDCalendar)
        resources.ApplyResources(Me.INDLySchedule, "INDLySchedule")
        Me.LayoutControls.SetIsCustomizable(Me.INDLySchedule, False)
        Me.INDLySchedule.Name = "INDLySchedule"
        Me.INDLySchedule.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(8, 489, 352, 350)
        Me.INDLySchedule.Root = Me.LayoutControlGroup1
        '
        'INDSlPosition
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlPosition, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlPosition, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlPosition, False)
        resources.ApplyResources(Me.INDSlPosition, "INDSlPosition")
        Me.INDSlPosition.MenuManager = Me.BarManager1
        Me.INDSlPosition.Name = "INDSlPosition"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlPosition, False)
        Me.INDSlPosition.Properties.Appearance.Font = CType(resources.GetObject("INDSlPosition.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSlPosition.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSlPosition.Properties.Appearance.Options.UseFont = True
        Me.INDSlPosition.Properties.Appearance.Options.UseForeColor = True
        Me.INDSlPosition.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSlPosition.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)), New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSlPosition.Properties.Buttons1"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDSlPosition.Properties.Buttons2"), CType(resources.GetObject("INDSlPosition.Properties.Buttons3"), Integer), CType(resources.GetObject("INDSlPosition.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDSlPosition.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDSlPosition.Properties.Buttons6"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDSlPosition.Properties.Buttons7"), CType(resources.GetObject("INDSlPosition.Properties.Buttons8"), Object), CType(resources.GetObject("INDSlPosition.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDSlPosition.Properties.Buttons10"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDSlPosition.Properties.DisplayMember = "Name"
        Me.INDSlPosition.Properties.NullText = resources.GetString("INDSlPosition.Properties.NullText")
        Me.INDSlPosition.Properties.PopupSizeable = False
        Me.INDSlPosition.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSlPosition.Properties.ShowFooter = False
        Me.INDSlPosition.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlPosition, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlPosition, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlPosition, True)
        Me.INDSlPosition.StyleController = Me.INDLySchedule
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlPosition, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlPosition, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlPosition, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlPosition, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.code, Me.cargoName})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'code
        '
        resources.ApplyResources(Me.code, "code")
        Me.code.FieldName = "Code"
        Me.code.MinWidth = 10
        Me.code.Name = "code"
        '
        'cargoName
        '
        resources.ApplyResources(Me.cargoName, "cargoName")
        Me.cargoName.FieldName = "Name"
        Me.cargoName.Name = "cargoName"
        '
        'INDChkLbcEmployee
        '
        Me.INDChkLbcEmployee.Appearance.Font = CType(resources.GetObject("INDChkLbcEmployee.Appearance.Font"), System.Drawing.Font)
        Me.INDChkLbcEmployee.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDChkLbcEmployee.Appearance.Options.UseFont = True
        Me.INDChkLbcEmployee.Appearance.Options.UseForeColor = True
        Me.INDChkLbcEmployee.CheckOnClick = True
        Me.INDChkLbcEmployee.ItemHeight = 30
        resources.ApplyResources(Me.INDChkLbcEmployee, "INDChkLbcEmployee")
        Me.INDChkLbcEmployee.Name = "INDChkLbcEmployee"
        Me.INDChkLbcEmployee.StyleController = Me.INDLySchedule
        '
        'INDPcCHours
        '
        Me.INDPcCHours.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcCHours.Controls.Add(Me.LayoutControl3)
        Me.INDPcCHours.Controls.Add(Me.INDPopupHoursLcEmployee)
        resources.ApplyResources(Me.INDPcCHours, "INDPcCHours")
        Me.INDPcCHours.Manager = Me.BarManager1
        Me.INDPcCHours.Name = "INDPcCHours"
        '
        'LayoutControl3
        '
        Me.LayoutControl3.BackColor = System.Drawing.Color.White
        Me.LayoutControl3.Controls.Add(Me.INDPopupHoursGcHoursPendingDetail)
        Me.LayoutControl3.Controls.Add(Me.INDPopupHoursWorkedPercentage)
        Me.LayoutControl3.Controls.Add(Me.INDPopupHoursMinNumber)
        Me.LayoutControl3.Controls.Add(Me.INDPopupHoursMaxNumber)
        Me.LayoutControl3.Controls.Add(Me.INDPopupHoursGcHoursDetail)
        Me.LayoutControl3.Controls.Add(Me.INDPopupHoursWorkedNumber)
        resources.ApplyResources(Me.LayoutControl3, "LayoutControl3")
        Me.LayoutControl3.Name = "LayoutControl3"
        Me.LayoutControl3.Root = Me.LayoutControlGroup3
        '
        'INDPopupHoursGcHoursPendingDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDPopupHoursGcHoursPendingDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDPopupHoursGcHoursPendingDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDPopupHoursGcHoursPendingDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDPopupHoursGcHoursPendingDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDPopupHoursGcHoursPendingDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDPopupHoursGcHoursPendingDetail, False)
        resources.ApplyResources(Me.INDPopupHoursGcHoursPendingDetail, "INDPopupHoursGcHoursPendingDetail")
        Me.INDPopupHoursGcHoursPendingDetail.MainView = Me.INDPopupHoursGvHoursPendingDetail
        Me.INDPopupHoursGcHoursPendingDetail.MenuManager = Me.BarManager1
        Me.INDPopupHoursGcHoursPendingDetail.Name = "INDPopupHoursGcHoursPendingDetail"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDPopupHoursGcHoursPendingDetail, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDPopupHoursGcHoursPendingDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDPopupHoursGvHoursPendingDetail})
        '
        'INDPopupHoursGvHoursPendingDetail
        '
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.FocusedRow.Font = CType(resources.GetObject("INDPopupHoursGvHoursPendingDetail.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.GroupRow.Font = CType(resources.GetObject("INDPopupHoursGvHoursPendingDetail.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDPopupHoursGvHoursPendingDetail.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.Row.Font = CType(resources.GetObject("INDPopupHoursGvHoursPendingDetail.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.Row.Options.UseFont = True
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.ViewCaption.Font = CType(resources.GetObject("INDPopupHoursGvHoursPendingDetail.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursPendingDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDPopupHoursGvHoursPendingDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDPopupHoursGvHoursPendingDetail.GridControl = Me.INDPopupHoursGcHoursPendingDetail
        Me.INDPopupHoursGvHoursPendingDetail.Name = "INDPopupHoursGvHoursPendingDetail"
        Me.INDPopupHoursGvHoursPendingDetail.OptionsBehavior.Editable = False
        Me.INDPopupHoursGvHoursPendingDetail.OptionsCustomization.AllowColumnMoving = False
        Me.INDPopupHoursGvHoursPendingDetail.OptionsCustomization.AllowFilter = False
        Me.INDPopupHoursGvHoursPendingDetail.OptionsMenu.EnableColumnMenu = False
        Me.INDPopupHoursGvHoursPendingDetail.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDPopupHoursGvHoursPendingDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDPopupHoursGvHoursPendingDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDPopupHoursGvHoursPendingDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDPopupHoursGvHoursPendingDetail.OptionsView.ShowDetailButtons = False
        Me.INDPopupHoursGvHoursPendingDetail.OptionsView.ShowGroupPanel = False
        Me.INDPopupHoursGvHoursPendingDetail.OptionsView.ShowIndicator = False
        Me.INDPopupHoursGvHoursPendingDetail.Tag = 121
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDPopupHoursGvHoursPendingDetail, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "ConceptName"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "HoursNumber"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'INDPopupHoursWorkedPercentage
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupHoursWorkedPercentage, True)
        Me.INDPopupHoursWorkedPercentage.Appearance.Font = CType(resources.GetObject("INDPopupHoursWorkedPercentage.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupHoursWorkedPercentage.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupHoursWorkedPercentage, False)
        resources.ApplyResources(Me.INDPopupHoursWorkedPercentage, "INDPopupHoursWorkedPercentage")
        Me.INDPopupHoursWorkedPercentage.Name = "INDPopupHoursWorkedPercentage"
        Me.INDPopupHoursWorkedPercentage.StyleController = Me.LayoutControl3
        '
        'INDPopupHoursMinNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupHoursMinNumber, True)
        Me.INDPopupHoursMinNumber.Appearance.Font = CType(resources.GetObject("INDPopupHoursMinNumber.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupHoursMinNumber.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupHoursMinNumber, False)
        resources.ApplyResources(Me.INDPopupHoursMinNumber, "INDPopupHoursMinNumber")
        Me.INDPopupHoursMinNumber.Name = "INDPopupHoursMinNumber"
        Me.INDPopupHoursMinNumber.StyleController = Me.LayoutControl3
        '
        'INDPopupHoursMaxNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupHoursMaxNumber, True)
        Me.INDPopupHoursMaxNumber.Appearance.Font = CType(resources.GetObject("INDPopupHoursMaxNumber.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupHoursMaxNumber.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupHoursMaxNumber, False)
        resources.ApplyResources(Me.INDPopupHoursMaxNumber, "INDPopupHoursMaxNumber")
        Me.INDPopupHoursMaxNumber.Name = "INDPopupHoursMaxNumber"
        Me.INDPopupHoursMaxNumber.StyleController = Me.LayoutControl3
        '
        'INDPopupHoursGcHoursDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDPopupHoursGcHoursDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDPopupHoursGcHoursDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDPopupHoursGcHoursDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDPopupHoursGcHoursDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDPopupHoursGcHoursDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDPopupHoursGcHoursDetail, False)
        resources.ApplyResources(Me.INDPopupHoursGcHoursDetail, "INDPopupHoursGcHoursDetail")
        Me.INDPopupHoursGcHoursDetail.MainView = Me.INDPopupHoursGvHoursDetail
        Me.INDPopupHoursGcHoursDetail.MenuManager = Me.BarManager1
        Me.INDPopupHoursGcHoursDetail.Name = "INDPopupHoursGcHoursDetail"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDPopupHoursGcHoursDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDPopupHoursGcHoursDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDPopupHoursGvHoursDetail})
        '
        'INDPopupHoursGvHoursDetail
        '
        Me.INDPopupHoursGvHoursDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDPopupHoursGvHoursDetail.Appearance.FocusedRow.Font = CType(resources.GetObject("INDPopupHoursGvHoursDetail.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDPopupHoursGvHoursDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDPopupHoursGvHoursDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDPopupHoursGvHoursDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDPopupHoursGvHoursDetail.Appearance.GroupRow.Font = CType(resources.GetObject("INDPopupHoursGvHoursDetail.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDPopupHoursGvHoursDetail.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDPopupHoursGvHoursDetail.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDPopupHoursGvHoursDetail.Appearance.Row.Font = CType(resources.GetObject("INDPopupHoursGvHoursDetail.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursDetail.Appearance.Row.Options.UseFont = True
        Me.INDPopupHoursGvHoursDetail.Appearance.ViewCaption.Font = CType(resources.GetObject("INDPopupHoursGvHoursDetail.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDPopupHoursGvHoursDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDPopupHoursGvHoursDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColConceptName, Me.INDColHoursNumber})
        Me.INDPopupHoursGvHoursDetail.GridControl = Me.INDPopupHoursGcHoursDetail
        Me.INDPopupHoursGvHoursDetail.Name = "INDPopupHoursGvHoursDetail"
        Me.INDPopupHoursGvHoursDetail.OptionsBehavior.Editable = False
        Me.INDPopupHoursGvHoursDetail.OptionsCustomization.AllowColumnMoving = False
        Me.INDPopupHoursGvHoursDetail.OptionsCustomization.AllowFilter = False
        Me.INDPopupHoursGvHoursDetail.OptionsMenu.EnableColumnMenu = False
        Me.INDPopupHoursGvHoursDetail.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDPopupHoursGvHoursDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDPopupHoursGvHoursDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDPopupHoursGvHoursDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDPopupHoursGvHoursDetail.OptionsView.ShowDetailButtons = False
        Me.INDPopupHoursGvHoursDetail.OptionsView.ShowGroupPanel = False
        Me.INDPopupHoursGvHoursDetail.OptionsView.ShowIndicator = False
        Me.INDPopupHoursGvHoursDetail.Tag = 121
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDPopupHoursGvHoursDetail, False)
        '
        'INDColConceptName
        '
        resources.ApplyResources(Me.INDColConceptName, "INDColConceptName")
        Me.INDColConceptName.FieldName = "ConceptName"
        Me.INDColConceptName.Name = "INDColConceptName"
        '
        'INDColHoursNumber
        '
        resources.ApplyResources(Me.INDColHoursNumber, "INDColHoursNumber")
        Me.INDColHoursNumber.FieldName = "HoursNumber"
        Me.INDColHoursNumber.Name = "INDColHoursNumber"
        '
        'INDPopupHoursWorkedNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupHoursWorkedNumber, True)
        Me.INDPopupHoursWorkedNumber.Appearance.Font = CType(resources.GetObject("INDPopupHoursWorkedNumber.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupHoursWorkedNumber.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupHoursWorkedNumber, False)
        resources.ApplyResources(Me.INDPopupHoursWorkedNumber, "INDPopupHoursWorkedNumber")
        Me.INDPopupHoursWorkedNumber.Name = "INDPopupHoursWorkedNumber"
        Me.INDPopupHoursWorkedNumber.StyleController = Me.LayoutControl3
        '
        'LayoutControlGroup3
        '
        resources.ApplyResources(Me.LayoutControlGroup3, "LayoutControlGroup3")
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemWorkedHours, Me.INDLyItemMaxHours, Me.INDLyItemMinHours, Me.INDTcgHours, Me.INDLyItemWorkedPercentage})
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(350, 241)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'INDLyItemWorkedHours
        '
        Me.INDLyItemWorkedHours.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemWorkedHours.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemWorkedHours.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.INDLyItemWorkedHours.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemWorkedHours.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemWorkedHours.Control = Me.INDPopupHoursWorkedNumber
        resources.ApplyResources(Me.INDLyItemWorkedHours, "INDLyItemWorkedHours")
        Me.INDLyItemWorkedHours.Location = New System.Drawing.Point(0, 27)
        Me.INDLyItemWorkedHours.MaxSize = New System.Drawing.Size(140, 27)
        Me.INDLyItemWorkedHours.MinSize = New System.Drawing.Size(140, 27)
        Me.INDLyItemWorkedHours.Name = "INDLyItemWorkedHours"
        Me.INDLyItemWorkedHours.Size = New System.Drawing.Size(140, 27)
        Me.INDLyItemWorkedHours.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemWorkedHours.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemWorkedHours.TextSize = New System.Drawing.Size(100, 23)
        Me.INDLyItemWorkedHours.TextToControlDistance = 12
        '
        'INDLyItemMaxHours
        '
        Me.INDLyItemMaxHours.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemMaxHours.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemMaxHours.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.INDLyItemMaxHours.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemMaxHours.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemMaxHours.Control = Me.INDPopupHoursMaxNumber
        resources.ApplyResources(Me.INDLyItemMaxHours, "INDLyItemMaxHours")
        Me.INDLyItemMaxHours.Location = New System.Drawing.Point(140, 0)
        Me.INDLyItemMaxHours.Name = "INDLyItemMaxHours"
        Me.INDLyItemMaxHours.Size = New System.Drawing.Size(190, 27)
        Me.INDLyItemMaxHours.Spacing = New DevExpress.XtraLayout.Utils.Padding(15, 0, 0, 0)
        Me.INDLyItemMaxHours.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemMaxHours.TextSize = New System.Drawing.Size(100, 23)
        Me.INDLyItemMaxHours.TextToControlDistance = 12
        '
        'INDLyItemMinHours
        '
        Me.INDLyItemMinHours.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemMinHours.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemMinHours.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.INDLyItemMinHours.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemMinHours.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemMinHours.Control = Me.INDPopupHoursMinNumber
        resources.ApplyResources(Me.INDLyItemMinHours, "INDLyItemMinHours")
        Me.INDLyItemMinHours.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemMinHours.Name = "INDLyItemMinHours"
        Me.INDLyItemMinHours.Size = New System.Drawing.Size(140, 27)
        Me.INDLyItemMinHours.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemMinHours.TextSize = New System.Drawing.Size(100, 23)
        Me.INDLyItemMinHours.TextToControlDistance = 12
        '
        'INDTcgHours
        '
        Me.INDTcgHours.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDTcgHours.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDTcgHours.AppearanceTabPage.Header.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDTcgHours.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDTcgHours.AppearanceTabPage.Header.Options.UseForeColor = True
        Me.INDTcgHours.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDTcgHours.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDTcgHours.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDTcgHours.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDTcgHours.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDTcgHours.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        resources.ApplyResources(Me.INDTcgHours, "INDTcgHours")
        Me.INDTcgHours.Location = New System.Drawing.Point(0, 54)
        Me.INDTcgHours.Name = "INDTcgHours"
        Me.INDTcgHours.SelectedTabPage = Me.INDLyGrHourLiquidation
        Me.INDTcgHours.Size = New System.Drawing.Size(330, 167)
        Me.INDTcgHours.Spacing = New DevExpress.XtraLayout.Utils.Padding(2, -2, 5, -2)
        Me.INDTcgHours.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGrHourLiquidation, Me.INDLyGrPendingEvents})
        '
        'INDLyGrHourLiquidation
        '
        resources.ApplyResources(Me.INDLyGrHourLiquidation, "INDLyGrHourLiquidation")
        Me.INDLyGrHourLiquidation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemHoursGrid})
        Me.INDLyGrHourLiquidation.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGrHourLiquidation.Name = "INDLyGrHourLiquidation"
        Me.INDLyGrHourLiquidation.Size = New System.Drawing.Size(310, 113)
        '
        'INDLyItemHoursGrid
        '
        Me.INDLyItemHoursGrid.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemHoursGrid.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemHoursGrid.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDLyItemHoursGrid.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemHoursGrid.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemHoursGrid.Control = Me.INDPopupHoursGcHoursDetail
        resources.ApplyResources(Me.INDLyItemHoursGrid, "INDLyItemHoursGrid")
        Me.INDLyItemHoursGrid.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemHoursGrid.MinSize = New System.Drawing.Size(248, 48)
        Me.INDLyItemHoursGrid.Name = "INDLyItemHoursGrid"
        Me.INDLyItemHoursGrid.Size = New System.Drawing.Size(310, 113)
        Me.INDLyItemHoursGrid.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemHoursGrid.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyItemHoursGrid.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemHoursGrid.TextVisible = False
        '
        'INDLyGrPendingEvents
        '
        resources.ApplyResources(Me.INDLyGrPendingEvents, "INDLyGrPendingEvents")
        Me.INDLyGrPendingEvents.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemHoursPendingGrid})
        Me.INDLyGrPendingEvents.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGrPendingEvents.Name = "INDLyGrPendingEvents"
        Me.INDLyGrPendingEvents.Size = New System.Drawing.Size(310, 113)
        '
        'INDLyItemHoursPendingGrid
        '
        Me.INDLyItemHoursPendingGrid.Control = Me.INDPopupHoursGcHoursPendingDetail
        resources.ApplyResources(Me.INDLyItemHoursPendingGrid, "INDLyItemHoursPendingGrid")
        Me.INDLyItemHoursPendingGrid.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemHoursPendingGrid.Name = "INDLyItemHoursPendingGrid"
        Me.INDLyItemHoursPendingGrid.Size = New System.Drawing.Size(310, 113)
        Me.INDLyItemHoursPendingGrid.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemHoursPendingGrid.TextVisible = False
        '
        'INDLyItemWorkedPercentage
        '
        Me.INDLyItemWorkedPercentage.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemWorkedPercentage.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemWorkedPercentage.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.INDLyItemWorkedPercentage.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemWorkedPercentage.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemWorkedPercentage.Control = Me.INDPopupHoursWorkedPercentage
        resources.ApplyResources(Me.INDLyItemWorkedPercentage, "INDLyItemWorkedPercentage")
        Me.INDLyItemWorkedPercentage.Location = New System.Drawing.Point(140, 27)
        Me.INDLyItemWorkedPercentage.Name = "INDLyItemWorkedPercentage"
        Me.INDLyItemWorkedPercentage.Size = New System.Drawing.Size(190, 27)
        Me.INDLyItemWorkedPercentage.Spacing = New DevExpress.XtraLayout.Utils.Padding(15, 0, 0, 0)
        Me.INDLyItemWorkedPercentage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemWorkedPercentage.TextSize = New System.Drawing.Size(100, 23)
        Me.INDLyItemWorkedPercentage.TextToControlDistance = 12
        '
        'INDPopupHoursLcEmployee
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupHoursLcEmployee, True)
        Me.INDPopupHoursLcEmployee.Appearance.Font = CType(resources.GetObject("INDPopupHoursLcEmployee.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupHoursLcEmployee.Appearance.Options.UseFont = True
        Me.INDPopupHoursLcEmployee.Appearance.Options.UseTextOptions = True
        Me.INDPopupHoursLcEmployee.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.INDPopupHoursLcEmployee, "INDPopupHoursLcEmployee")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupHoursLcEmployee, False)
        Me.INDPopupHoursLcEmployee.Name = "INDPopupHoursLcEmployee"
        '
        'INDPcScheduleDetailComplete
        '
        Me.INDPcScheduleDetailComplete.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcScheduleDetailComplete.Controls.Add(Me.INDPcDetailEm1)
        Me.INDPcScheduleDetailComplete.Controls.Add(Me.INDLcEmployee1)
        Me.INDPcScheduleDetailComplete.Controls.Add(Me.INDLcEmployee2)
        Me.INDPcScheduleDetailComplete.Controls.Add(Me.INDPcDetailEm2)
        resources.ApplyResources(Me.INDPcScheduleDetailComplete, "INDPcScheduleDetailComplete")
        Me.INDPcScheduleDetailComplete.Name = "INDPcScheduleDetailComplete"
        '
        'INDPcDetailEm1
        '
        Me.INDPcDetailEm1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcDetailEm1.Controls.Add(Me.INDLcTotalHoursNumber1)
        Me.INDPcDetailEm1.Controls.Add(Me.INDLcTotalHours1)
        Me.INDPcDetailEm1.Controls.Add(Me.INDDDBEmployee1)
        resources.ApplyResources(Me.INDPcDetailEm1, "INDPcDetailEm1")
        Me.INDPcDetailEm1.Name = "INDPcDetailEm1"
        '
        'INDLcTotalHoursNumber1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcTotalHoursNumber1, True)
        Me.INDLcTotalHoursNumber1.Appearance.BackColor = System.Drawing.Color.Gray
        Me.INDLcTotalHoursNumber1.Appearance.Font = CType(resources.GetObject("INDLcTotalHoursNumber1.Appearance.Font"), System.Drawing.Font)
        Me.INDLcTotalHoursNumber1.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDLcTotalHoursNumber1.Appearance.Options.UseBackColor = True
        Me.INDLcTotalHoursNumber1.Appearance.Options.UseFont = True
        Me.INDLcTotalHoursNumber1.Appearance.Options.UseForeColor = True
        Me.INDLcTotalHoursNumber1.Appearance.Options.UseTextOptions = True
        Me.INDLcTotalHoursNumber1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.INDLcTotalHoursNumber1, "INDLcTotalHoursNumber1")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcTotalHoursNumber1, False)
        Me.INDLcTotalHoursNumber1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDLcTotalHoursNumber1.Name = "INDLcTotalHoursNumber1"
        Me.INDLcTotalHoursNumber1.Tag = "1"
        '
        'INDLcTotalHours1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcTotalHours1, True)
        Me.INDLcTotalHours1.Appearance.BackColor = System.Drawing.Color.White
        Me.INDLcTotalHours1.Appearance.BorderColor = System.Drawing.Color.Gray
        Me.INDLcTotalHours1.Appearance.Font = CType(resources.GetObject("INDLcTotalHours1.Appearance.Font"), System.Drawing.Font)
        Me.INDLcTotalHours1.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.INDLcTotalHours1.Appearance.Options.UseBackColor = True
        Me.INDLcTotalHours1.Appearance.Options.UseBorderColor = True
        Me.INDLcTotalHours1.Appearance.Options.UseFont = True
        Me.INDLcTotalHours1.Appearance.Options.UseForeColor = True
        Me.INDLcTotalHours1.Appearance.Options.UseTextOptions = True
        Me.INDLcTotalHours1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLcTotalHours1.AutoEllipsis = True
        resources.ApplyResources(Me.INDLcTotalHours1, "INDLcTotalHours1")
        Me.INDLcTotalHours1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcTotalHours1, False)
        Me.INDLcTotalHours1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDLcTotalHours1.Name = "INDLcTotalHours1"
        Me.INDLcTotalHours1.Tag = "1"
        '
        'INDDDBEmployee1
        '
        Me.INDDDBEmployee1.DropDownControl = Me.INDPcCHours
        resources.ApplyResources(Me.INDDDBEmployee1, "INDDDBEmployee1")
        Me.INDDDBEmployee1.MenuManager = Me.BarManager1
        Me.INDDDBEmployee1.Name = "INDDDBEmployee1"
        '
        'INDLcEmployee1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcEmployee1, True)
        Me.INDLcEmployee1.Appearance.Font = CType(resources.GetObject("INDLcEmployee1.Appearance.Font"), System.Drawing.Font)
        Me.INDLcEmployee1.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.INDLcEmployee1.Appearance.Options.UseFont = True
        Me.INDLcEmployee1.Appearance.Options.UseForeColor = True
        Me.INDLcEmployee1.Appearance.Options.UseTextOptions = True
        Me.INDLcEmployee1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        resources.ApplyResources(Me.INDLcEmployee1, "INDLcEmployee1")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcEmployee1, False)
        Me.INDLcEmployee1.Name = "INDLcEmployee1"
        '
        'INDLcEmployee2
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcEmployee2, True)
        Me.INDLcEmployee2.Appearance.Font = CType(resources.GetObject("INDLcEmployee2.Appearance.Font"), System.Drawing.Font)
        Me.INDLcEmployee2.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.INDLcEmployee2.Appearance.Options.UseFont = True
        Me.INDLcEmployee2.Appearance.Options.UseForeColor = True
        Me.INDLcEmployee2.Appearance.Options.UseTextOptions = True
        Me.INDLcEmployee2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        resources.ApplyResources(Me.INDLcEmployee2, "INDLcEmployee2")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcEmployee2, False)
        Me.INDLcEmployee2.Name = "INDLcEmployee2"
        '
        'INDPcDetailEm2
        '
        Me.INDPcDetailEm2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcDetailEm2.Controls.Add(Me.INDLcTotalHoursNumber2)
        Me.INDPcDetailEm2.Controls.Add(Me.INDLcTotalHours2)
        Me.INDPcDetailEm2.Controls.Add(Me.INDDDBEmployee2)
        resources.ApplyResources(Me.INDPcDetailEm2, "INDPcDetailEm2")
        Me.INDPcDetailEm2.Name = "INDPcDetailEm2"
        '
        'INDLcTotalHoursNumber2
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcTotalHoursNumber2, True)
        Me.INDLcTotalHoursNumber2.Appearance.BackColor = System.Drawing.Color.Gray
        Me.INDLcTotalHoursNumber2.Appearance.Font = CType(resources.GetObject("INDLcTotalHoursNumber2.Appearance.Font"), System.Drawing.Font)
        Me.INDLcTotalHoursNumber2.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDLcTotalHoursNumber2.Appearance.Options.UseBackColor = True
        Me.INDLcTotalHoursNumber2.Appearance.Options.UseFont = True
        Me.INDLcTotalHoursNumber2.Appearance.Options.UseForeColor = True
        Me.INDLcTotalHoursNumber2.Appearance.Options.UseTextOptions = True
        Me.INDLcTotalHoursNumber2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.INDLcTotalHoursNumber2, "INDLcTotalHoursNumber2")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcTotalHoursNumber2, False)
        Me.INDLcTotalHoursNumber2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDLcTotalHoursNumber2.Name = "INDLcTotalHoursNumber2"
        Me.INDLcTotalHoursNumber2.Tag = "2"
        '
        'INDLcTotalHours2
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcTotalHours2, True)
        Me.INDLcTotalHours2.Appearance.BackColor = System.Drawing.Color.White
        Me.INDLcTotalHours2.Appearance.BorderColor = System.Drawing.Color.Gray
        Me.INDLcTotalHours2.Appearance.Font = CType(resources.GetObject("INDLcTotalHours2.Appearance.Font"), System.Drawing.Font)
        Me.INDLcTotalHours2.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.INDLcTotalHours2.Appearance.Options.UseBackColor = True
        Me.INDLcTotalHours2.Appearance.Options.UseBorderColor = True
        Me.INDLcTotalHours2.Appearance.Options.UseFont = True
        Me.INDLcTotalHours2.Appearance.Options.UseForeColor = True
        Me.INDLcTotalHours2.Appearance.Options.UseTextOptions = True
        Me.INDLcTotalHours2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLcTotalHours2.AutoEllipsis = True
        resources.ApplyResources(Me.INDLcTotalHours2, "INDLcTotalHours2")
        Me.INDLcTotalHours2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcTotalHours2, False)
        Me.INDLcTotalHours2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDLcTotalHours2.Name = "INDLcTotalHours2"
        Me.INDLcTotalHours2.Tag = "2"
        '
        'INDDDBEmployee2
        '
        Me.INDDDBEmployee2.DropDownControl = Me.INDPcCHours
        resources.ApplyResources(Me.INDDDBEmployee2, "INDDDBEmployee2")
        Me.INDDDBEmployee2.MenuManager = Me.BarManager1
        Me.INDDDBEmployee2.Name = "INDDDBEmployee2"
        '
        'INDLyDelete
        '
        Me.INDLyDelete.Controls.Add(Me.INDDelSBCancel)
        Me.INDLyDelete.Controls.Add(Me.INDDelSBFinish)
        Me.INDLyDelete.Controls.Add(Me.INDLcSelectSchedule)
        resources.ApplyResources(Me.INDLyDelete, "INDLyDelete")
        Me.INDLyDelete.Name = "INDLyDelete"
        Me.INDLyDelete.Root = Me.Root
        '
        'INDDelSBCancel
        '
        Me.INDDelSBCancel.Appearance.Font = CType(resources.GetObject("INDDelSBCancel.Appearance.Font"), System.Drawing.Font)
        Me.INDDelSBCancel.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDDelSBCancel, "INDDelSBCancel")
        Me.INDDelSBCancel.Name = "INDDelSBCancel"
        Me.INDDelSBCancel.StyleController = Me.INDLyDelete
        '
        'INDDelSBFinish
        '
        Me.INDDelSBFinish.Appearance.Font = CType(resources.GetObject("INDDelSBFinish.Appearance.Font"), System.Drawing.Font)
        Me.INDDelSBFinish.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.INDDelSBFinish, "INDDelSBFinish")
        Me.INDDelSBFinish.Name = "INDDelSBFinish"
        Me.INDDelSBFinish.StyleController = Me.INDLyDelete
        '
        'INDLcSelectSchedule
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcSelectSchedule, False)
        Me.INDLcSelectSchedule.Appearance.Font = CType(resources.GetObject("INDLcSelectSchedule.Appearance.Font"), System.Drawing.Font)
        Me.INDLcSelectSchedule.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDLcSelectSchedule.Appearance.Options.UseFont = True
        Me.INDLcSelectSchedule.Appearance.Options.UseForeColor = True
        Me.INDLcSelectSchedule.Appearance.Options.UseTextOptions = True
        Me.INDLcSelectSchedule.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        resources.ApplyResources(Me.INDLcSelectSchedule, "INDLcSelectSchedule")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcSelectSchedule, False)
        Me.INDLcSelectSchedule.Name = "INDLcSelectSchedule"
        Me.INDLcSelectSchedule.StyleController = Me.INDLyDelete
        '
        'Root
        '
        resources.ApplyResources(Me.Root, "Root")
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemTxtDeleteInformation, Me.INDLyItemButtonFinishDelete, Me.INDLyItemButtonCancelDelete})
        Me.Root.Name = "Root"
        Me.Root.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 10, 0, 0)
        Me.Root.Size = New System.Drawing.Size(839, 36)
        Me.Root.TextVisible = False
        '
        'INDLyItemTxtDeleteInformation
        '
        Me.INDLyItemTxtDeleteInformation.Control = Me.INDLcSelectSchedule
        resources.ApplyResources(Me.INDLyItemTxtDeleteInformation, "INDLyItemTxtDeleteInformation")
        Me.INDLyItemTxtDeleteInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemTxtDeleteInformation.Name = "INDLyItemTxtDeleteInformation"
        Me.INDLyItemTxtDeleteInformation.Size = New System.Drawing.Size(409, 36)
        Me.INDLyItemTxtDeleteInformation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemTxtDeleteInformation.TextVisible = False
        '
        'INDLyItemButtonFinishDelete
        '
        Me.INDLyItemButtonFinishDelete.Control = Me.INDDelSBFinish
        resources.ApplyResources(Me.INDLyItemButtonFinishDelete, "INDLyItemButtonFinishDelete")
        Me.INDLyItemButtonFinishDelete.Location = New System.Drawing.Point(409, 0)
        Me.INDLyItemButtonFinishDelete.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLyItemButtonFinishDelete.MinSize = New System.Drawing.Size(83, 36)
        Me.INDLyItemButtonFinishDelete.Name = "INDLyItemButtonFinishDelete"
        Me.INDLyItemButtonFinishDelete.Size = New System.Drawing.Size(206, 36)
        Me.INDLyItemButtonFinishDelete.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemButtonFinishDelete.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemButtonFinishDelete.TextVisible = False
        '
        'INDLyItemButtonCancelDelete
        '
        Me.INDLyItemButtonCancelDelete.Control = Me.INDDelSBCancel
        resources.ApplyResources(Me.INDLyItemButtonCancelDelete, "INDLyItemButtonCancelDelete")
        Me.INDLyItemButtonCancelDelete.Location = New System.Drawing.Point(615, 0)
        Me.INDLyItemButtonCancelDelete.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLyItemButtonCancelDelete.MinSize = New System.Drawing.Size(83, 36)
        Me.INDLyItemButtonCancelDelete.Name = "INDLyItemButtonCancelDelete"
        Me.INDLyItemButtonCancelDelete.Size = New System.Drawing.Size(204, 36)
        Me.INDLyItemButtonCancelDelete.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemButtonCancelDelete.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemButtonCancelDelete.TextVisible = False
        '
        'INDDDBOptionsMenu
        '
        Me.INDDDBOptionsMenu.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.INDDDBOptionsMenu.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDDBOptionsMenu.DropDownControl = Me.INDPopMenuActions
        Me.INDDDBOptionsMenu.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.Mmenu_de_acciones
        Me.INDDDBOptionsMenu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        resources.ApplyResources(Me.INDDDBOptionsMenu, "INDDDBOptionsMenu")
        Me.INDDDBOptionsMenu.MenuManager = Me.BarManager1
        Me.INDDDBOptionsMenu.Name = "INDDDBOptionsMenu"
        Me.INDDDBOptionsMenu.StyleController = Me.INDLySchedule
        '
        'INDPopMenuActions
        '
        Me.INDPopMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarRefresh), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarMarkEmployee), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarDeleteAll), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarMarkAsDelete), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarEmployee)})
        Me.INDPopMenuActions.Manager = Me.BarManager1
        Me.INDPopMenuActions.Name = "INDPopMenuActions"
        '
        'INDLcEmployees
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcEmployees, False)
        Me.INDLcEmployees.Appearance.Font = CType(resources.GetObject("INDLcEmployees.Appearance.Font"), System.Drawing.Font)
        Me.INDLcEmployees.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcEmployees, False)
        resources.ApplyResources(Me.INDLcEmployees, "INDLcEmployees")
        Me.INDLcEmployees.Name = "INDLcEmployees"
        Me.INDLcEmployees.StyleController = Me.INDLySchedule
        '
        'INDGcScheduleDetail1
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcScheduleDetail1, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcScheduleDetail1, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcScheduleDetail1, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcScheduleDetail1, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcScheduleDetail1, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcScheduleDetail1, False)
        resources.ApplyResources(Me.INDGcScheduleDetail1, "INDGcScheduleDetail1")
        Me.INDGcScheduleDetail1.MainView = Me.INDGvScheduleDetail1
        Me.INDGcScheduleDetail1.Name = "INDGcScheduleDetail1"
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcScheduleDetail1, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcScheduleDetail1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvScheduleDetail1})
        '
        'INDGvScheduleDetail1
        '
        Me.INDGvScheduleDetail1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvScheduleDetail1.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvScheduleDetail1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvScheduleDetail1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvScheduleDetail1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvScheduleDetail1.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvScheduleDetail1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvScheduleDetail1.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvScheduleDetail1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvScheduleDetail1.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvScheduleDetail1.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvScheduleDetail1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvScheduleDetail1.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvScheduleDetail1.Appearance.Row.Font = CType(resources.GetObject("INDGvScheduleDetail1.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvScheduleDetail1.Appearance.Row.Options.UseFont = True
        Me.INDGvScheduleDetail1.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvScheduleDetail1.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvScheduleDetail1.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvScheduleDetail1.ColumnPanelRowHeight = 60
        Me.INDGvScheduleDetail1.GridControl = Me.INDGcScheduleDetail1
        Me.INDGvScheduleDetail1.Name = "INDGvScheduleDetail1"
        Me.INDGvScheduleDetail1.OptionsCustomization.AllowColumnMoving = False
        Me.INDGvScheduleDetail1.OptionsCustomization.AllowColumnResizing = False
        Me.INDGvScheduleDetail1.OptionsCustomization.AllowFilter = False
        Me.INDGvScheduleDetail1.OptionsMenu.EnableColumnMenu = False
        Me.INDGvScheduleDetail1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvScheduleDetail1.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvScheduleDetail1.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvScheduleDetail1.OptionsView.ShowAutoFilterRow = True
        Me.INDGvScheduleDetail1.OptionsView.ShowGroupPanel = False
        Me.INDGvScheduleDetail1.OptionsView.ShowIndicator = False
        Me.INDGvScheduleDetail1.RowHeight = 30
        Me.INDGvScheduleDetail1.Tag = 644
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvScheduleDetail1, False)
        '
        'CtrDateNavigator1
        '
        Me.CtrDateNavigator1.CtrCalendar = Me.INDCalendar
        Me.CtrDateNavigator1.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        resources.ApplyResources(Me.CtrDateNavigator1, "CtrDateNavigator1")
        Me.CtrDateNavigator1.Name = "CtrDateNavigator1"
        Me.CtrDateNavigator1.WithEvent = True
        '
        'INDPcMainControls
        '
        Me.INDPcMainControls.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPcMainControls.Appearance.Options.UseBackColor = True
        Me.INDPcMainControls.Controls.Add(Me.INDSleFunctionalUnit)
        Me.INDPcMainControls.Controls.Add(Me.INDDDBFunctionalUnit)
        resources.ApplyResources(Me.INDPcMainControls, "INDPcMainControls")
        Me.INDPcMainControls.Name = "INDPcMainControls"
        '
        'INDSleFunctionalUnit
        '
        AppearanceObject3.Options.UseFont = True
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleFunctionalUnit, AppearanceObject3)
        AppearanceObject4.Options.UseFont = True
        AppearanceObject4.Options.UseForeColor = True
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleFunctionalUnit, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleFunctionalUnit, False)
        resources.ApplyResources(Me.INDSleFunctionalUnit, "INDSleFunctionalUnit")
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.INDSleFunctionalUnit.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.INDSleFunctionalUnit.Name = "INDSleFunctionalUnit"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.INDSleFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleFunctionalUnit.Properties.Appearance.BackColor2 = CType(resources.GetObject("INDSleFunctionalUnit.Properties.Appearance.BackColor2"), System.Drawing.Color)
        Me.INDSleFunctionalUnit.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDSleFunctionalUnit.Properties.Appearance.Font = CType(resources.GetObject("INDSleFunctionalUnit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleFunctionalUnit.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleFunctionalUnit.Properties.Appearance.Options.UseBorderColor = True
        Me.INDSleFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDSleFunctionalUnit.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleFunctionalUnit.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleFunctionalUnit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDSleFunctionalUnit.Properties.DisplayMember = "Descripcion"
        Me.INDSleFunctionalUnit.Properties.NullText = resources.GetString("INDSleFunctionalUnit.Properties.NullText")
        Me.INDSleFunctionalUnit.Properties.PopupSizeable = False
        Me.INDSleFunctionalUnit.Properties.PopupView = Me.INDGvFunctionalUnit
        Me.INDSleFunctionalUnit.Properties.ShowClearButton = False
        Me.INDSleFunctionalUnit.Properties.ShowFooter = False
        Me.INDSleFunctionalUnit.Properties.ValueMember = "Codigo"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleFunctionalUnit, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleFunctionalUnit, True)
        Me.INDSleFunctionalUnit.StyleController = Me.INDLySchedule
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleFunctionalUnit, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleFunctionalUnit, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleFunctionalUnit, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleFunctionalUnit, False)
        '
        'INDGvFunctionalUnit
        '
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvFunctionalUnit.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvFunctionalUnit.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvFunctionalUnit.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvFunctionalUnit.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvFunctionalUnit.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvFunctionalUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFunctionalUnit.Appearance.Row.Font = CType(resources.GetObject("INDGvFunctionalUnit.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvFunctionalUnit.Appearance.Row.Options.UseFont = True
        Me.INDGvFunctionalUnit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColDescription, Me.INDColBOName, Me.INDColCompanyDescription})
        Me.INDGvFunctionalUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvFunctionalUnit.Name = "INDGvFunctionalUnit"
        Me.INDGvFunctionalUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvFunctionalUnit.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFunctionalUnit.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFunctionalUnit.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFunctionalUnit.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvFunctionalUnit, False)
        '
        'INDColDescription
        '
        resources.ApplyResources(Me.INDColDescription, "INDColDescription")
        Me.INDColDescription.FieldName = "Descripcion"
        Me.INDColDescription.Name = "INDColDescription"
        '
        'INDColBOName
        '
        resources.ApplyResources(Me.INDColBOName, "INDColBOName")
        Me.INDColBOName.FieldName = "BranchOfficeId.Descripcion"
        Me.INDColBOName.Name = "INDColBOName"
        '
        'INDColCompanyDescription
        '
        resources.ApplyResources(Me.INDColCompanyDescription, "INDColCompanyDescription")
        Me.INDColCompanyDescription.FieldName = "BranchOfficeId.CompanyId.Descripcion"
        Me.INDColCompanyDescription.Name = "INDColCompanyDescription"
        '
        'INDDDBFunctionalUnit
        '
        Me.INDDDBFunctionalUnit.Appearance.Font = CType(resources.GetObject("INDDDBFunctionalUnit.Appearance.Font"), System.Drawing.Font)
        Me.INDDDBFunctionalUnit.Appearance.Options.UseFont = True
        Me.INDDDBFunctionalUnit.AutoWidthInLayoutControl = True
        resources.ApplyResources(Me.INDDDBFunctionalUnit, "INDDDBFunctionalUnit")
        Me.INDDDBFunctionalUnit.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDDBFunctionalUnit.Name = "INDDDBFunctionalUnit"
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemCalendar, Me.INDLyGrFunctionalUnit, Me.INDSpace2, Me.INDSpace1, Me.INDLyItemEmployeeList, Me.INDLyItemScheduleDetailGrid1, Me.INDLyItemTxtEmployee, Me.INDLyItemOptionMenu, Me.LayoutControlItem1, Me.EmptySpaceItem3, Me.INDLyItemLayoutDelete, Me.INDLyItemScheduleDetailPanelComplete, Me.INDLciPosition})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 10, 0, 10)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1531, 711)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLyItemCalendar
        '
        Me.INDLyItemCalendar.Control = Me.INDCalendar
        resources.ApplyResources(Me.INDLyItemCalendar, "INDLyItemCalendar")
        Me.INDLyItemCalendar.Location = New System.Drawing.Point(274, 114)
        Me.INDLyItemCalendar.MaxSize = New System.Drawing.Size(843, 519)
        Me.INDLyItemCalendar.MinSize = New System.Drawing.Size(843, 519)
        Me.INDLyItemCalendar.Name = "INDLyItemCalendar"
        Me.INDLyItemCalendar.Size = New System.Drawing.Size(843, 519)
        Me.INDLyItemCalendar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemCalendar.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemCalendar.TextVisible = False
        '
        'INDLyGrFunctionalUnit
        '
        Me.INDLyGrFunctionalUnit.Control = Me.INDPcMainControls
        resources.ApplyResources(Me.INDLyGrFunctionalUnit, "INDLyGrFunctionalUnit")
        Me.INDLyGrFunctionalUnit.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGrFunctionalUnit.MaxSize = New System.Drawing.Size(0, 69)
        Me.INDLyGrFunctionalUnit.MinSize = New System.Drawing.Size(763, 69)
        Me.INDLyGrFunctionalUnit.Name = "INDLyGrFunctionalUnit"
        Me.INDLyGrFunctionalUnit.Size = New System.Drawing.Size(1156, 69)
        Me.INDLyGrFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGrFunctionalUnit.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyGrFunctionalUnit.TextVisible = False
        '
        'INDSpace2
        '
        Me.INDSpace2.AllowHotTrack = False
        resources.ApplyResources(Me.INDSpace2, "INDSpace2")
        Me.INDSpace2.Location = New System.Drawing.Point(1117, 74)
        Me.INDSpace2.MinSize = New System.Drawing.Size(104, 24)
        Me.INDSpace2.Name = "INDSpace2"
        Me.INDSpace2.Size = New System.Drawing.Size(108, 627)
        Me.INDSpace2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDSpace2.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDSpace1
        '
        Me.INDSpace1.AllowHotTrack = False
        resources.ApplyResources(Me.INDSpace1, "INDSpace1")
        Me.INDSpace1.Location = New System.Drawing.Point(0, 74)
        Me.INDSpace1.Name = "INDSpace1"
        Me.INDSpace1.Size = New System.Drawing.Size(173, 627)
        Me.INDSpace1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDLyItemEmployeeList
        '
        Me.INDLyItemEmployeeList.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemEmployeeList.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemEmployeeList.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDLyItemEmployeeList.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemEmployeeList.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemEmployeeList.Control = Me.INDChkLbcEmployee
        resources.ApplyResources(Me.INDLyItemEmployeeList, "INDLyItemEmployeeList")
        Me.INDLyItemEmployeeList.Location = New System.Drawing.Point(1225, 170)
        Me.INDLyItemEmployeeList.MaxSize = New System.Drawing.Size(286, 0)
        Me.INDLyItemEmployeeList.MinSize = New System.Drawing.Size(286, 1)
        Me.INDLyItemEmployeeList.Name = "INDLyItemEmployeeList"
        Me.INDLyItemEmployeeList.Size = New System.Drawing.Size(286, 531)
        Me.INDLyItemEmployeeList.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemEmployeeList.Spacing = New DevExpress.XtraLayout.Utils.Padding(-2, 0, 0, 0)
        Me.INDLyItemEmployeeList.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyItemEmployeeList.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemEmployeeList.TextVisible = False
        '
        'INDLyItemScheduleDetailGrid1
        '
        Me.INDLyItemScheduleDetailGrid1.Control = Me.INDGcScheduleDetail1
        resources.ApplyResources(Me.INDLyItemScheduleDetailGrid1, "INDLyItemScheduleDetailGrid1")
        Me.INDLyItemScheduleDetailGrid1.Location = New System.Drawing.Point(173, 74)
        Me.INDLyItemScheduleDetailGrid1.MinSize = New System.Drawing.Size(101, 24)
        Me.INDLyItemScheduleDetailGrid1.Name = "INDLyItemScheduleDetailGrid1"
        Me.INDLyItemScheduleDetailGrid1.Size = New System.Drawing.Size(101, 627)
        Me.INDLyItemScheduleDetailGrid1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemScheduleDetailGrid1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, -3, 0, 0)
        Me.INDLyItemScheduleDetailGrid1.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemScheduleDetailGrid1.TextVisible = False
        Me.INDLyItemScheduleDetailGrid1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyItemTxtEmployee
        '
        Me.INDLyItemTxtEmployee.Control = Me.INDLcEmployees
        resources.ApplyResources(Me.INDLyItemTxtEmployee, "INDLyItemTxtEmployee")
        Me.INDLyItemTxtEmployee.Location = New System.Drawing.Point(1225, 74)
        Me.INDLyItemTxtEmployee.MaxSize = New System.Drawing.Size(261, 60)
        Me.INDLyItemTxtEmployee.MinSize = New System.Drawing.Size(261, 60)
        Me.INDLyItemTxtEmployee.Name = "INDLyItemTxtEmployee"
        Me.INDLyItemTxtEmployee.Size = New System.Drawing.Size(286, 60)
        Me.INDLyItemTxtEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemTxtEmployee.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemTxtEmployee.TextVisible = False
        '
        'INDLyItemOptionMenu
        '
        Me.INDLyItemOptionMenu.Control = Me.INDDDBOptionsMenu
        resources.ApplyResources(Me.INDLyItemOptionMenu, "INDLyItemOptionMenu")
        Me.INDLyItemOptionMenu.Location = New System.Drawing.Point(1156, 0)
        Me.INDLyItemOptionMenu.MaxSize = New System.Drawing.Size(69, 69)
        Me.INDLyItemOptionMenu.MinSize = New System.Drawing.Size(69, 69)
        Me.INDLyItemOptionMenu.Name = "INDLyItemOptionMenu"
        Me.INDLyItemOptionMenu.Size = New System.Drawing.Size(69, 69)
        Me.INDLyItemOptionMenu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemOptionMenu.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemOptionMenu.TextVisible = False
        Me.INDLyItemOptionMenu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.CtrDateNavigator1
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(1225, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(286, 69)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(286, 69)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(286, 69)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem3, "EmptySpaceItem3")
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 69)
        Me.EmptySpaceItem3.MaxSize = New System.Drawing.Size(0, 5)
        Me.EmptySpaceItem3.MinSize = New System.Drawing.Size(10, 5)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(1511, 5)
        Me.EmptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDLyItemLayoutDelete
        '
        Me.INDLyItemLayoutDelete.Control = Me.INDLyDelete
        resources.ApplyResources(Me.INDLyItemLayoutDelete, "INDLyItemLayoutDelete")
        Me.INDLyItemLayoutDelete.Location = New System.Drawing.Point(274, 74)
        Me.INDLyItemLayoutDelete.MaxSize = New System.Drawing.Size(843, 40)
        Me.INDLyItemLayoutDelete.MinSize = New System.Drawing.Size(843, 40)
        Me.INDLyItemLayoutDelete.Name = "INDLyItemLayoutDelete"
        Me.INDLyItemLayoutDelete.Size = New System.Drawing.Size(843, 40)
        Me.INDLyItemLayoutDelete.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemLayoutDelete.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemLayoutDelete.TextVisible = False
        Me.INDLyItemLayoutDelete.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyItemScheduleDetailPanelComplete
        '
        Me.INDLyItemScheduleDetailPanelComplete.Control = Me.INDPcScheduleDetailComplete
        resources.ApplyResources(Me.INDLyItemScheduleDetailPanelComplete, "INDLyItemScheduleDetailPanelComplete")
        Me.INDLyItemScheduleDetailPanelComplete.Location = New System.Drawing.Point(274, 633)
        Me.INDLyItemScheduleDetailPanelComplete.MaxSize = New System.Drawing.Size(843, 68)
        Me.INDLyItemScheduleDetailPanelComplete.MinSize = New System.Drawing.Size(843, 68)
        Me.INDLyItemScheduleDetailPanelComplete.Name = "INDLyItemScheduleDetailPanelComplete"
        Me.INDLyItemScheduleDetailPanelComplete.Size = New System.Drawing.Size(843, 68)
        Me.INDLyItemScheduleDetailPanelComplete.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemScheduleDetailPanelComplete.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemScheduleDetailPanelComplete.TextVisible = False
        '
        'INDLciPosition
        '
        Me.INDLciPosition.Control = Me.INDSlPosition
        Me.INDLciPosition.Location = New System.Drawing.Point(1225, 134)
        Me.INDLciPosition.MaxSize = New System.Drawing.Size(286, 36)
        Me.INDLciPosition.MinSize = New System.Drawing.Size(286, 36)
        Me.INDLciPosition.Name = "INDLciPosition"
        Me.INDLciPosition.Size = New System.Drawing.Size(286, 36)
        Me.INDLciPosition.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDLciPosition, "INDLciPosition")
        Me.INDLciPosition.TextSize = New System.Drawing.Size(47, 21)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmSchedule1
        '
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmSchedule1"
        Me.Opacity = 1.0R
        Me.Tag = "554"
        Me.ViewModeEditHold = True
        Me.Controls.SetChildIndex(Me.BarDockControl1, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl2, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl4, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl3, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcCDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcCDetail.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDgcDetHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDetHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcCMore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcCMore.ResumeLayout(False)
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl4.ResumeLayout(False)
        CType(Me.INDPopupMoreGcHoursDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupMoreGvHoursDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemOtherSchDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupGcMatches, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupGvMatches, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgDetailsSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrDetHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtrGrMatchPartnerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailScheduleMates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailTemplateHoursNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLySchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLySchedule.ResumeLayout(False)
        CType(Me.INDSlPosition.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkLbcEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcCHours, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcCHours.ResumeLayout(False)
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl3.ResumeLayout(False)
        CType(Me.INDPopupHoursGcHoursPendingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupHoursGvHoursPendingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupHoursGcHoursDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupHoursGvHoursDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemWorkedHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemMaxHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemMinHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrHourLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemHoursGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrPendingEvents, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemHoursPendingGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemWorkedPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcScheduleDetailComplete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcScheduleDetailComplete.ResumeLayout(False)
        CType(Me.INDPcDetailEm1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcDetailEm1.ResumeLayout(False)
        CType(Me.INDPcDetailEm2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcDetailEm2.ResumeLayout(False)
        CType(Me.INDLyDelete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyDelete.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemTxtDeleteInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemButtonFinishDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemButtonCancelDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcScheduleDetail1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvScheduleDetail1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcMainControls, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcMainControls.ResumeLayout(False)
        CType(Me.INDSleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemCalendar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpace2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpace1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemEmployeeList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemScheduleDetailGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemTxtEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemOptionMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemLayoutDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemScheduleDetailPanelComplete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDCalendar As Presentation.Controls.CtrCalendar
    Friend WithEvents INDLySchedule As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemCalendar As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrDateNavigator1 As Presentation.Controls.CtrDateNavigator
    Friend WithEvents INDPcScheduleDetailComplete As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDSleFunctionalUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvFunctionalUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDColCodigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBranchOfficeDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBOName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCompanyDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDChkLbcEmployee As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents INDLcTotalHoursNumber1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLcTotalHours1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLcTotalHoursNumber2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLcTotalHours2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLcEmployee2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLcEmployee1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDDDBFunctionalUnit As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDPcMainControls As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDLyGrFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemEmployeeList As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSpace2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSpace1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDGcScheduleDetail1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvScheduleDetail1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLyItemScheduleDetailGrid1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPcCDetail As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDPopupLcEmployee As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDPopupLcTxtTemplate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDPopupLcNumberDay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemDetailDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemDetailTemplate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupGcMatches As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDPopupGvMatches As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDPopupSBEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLyItemDetailEdit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCOlEmployeeNameDetailMates As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPcCHours As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPopupHoursLcEmployee As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDPopupHoursWorkedNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLyItemWorkedHours As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupHoursGcHoursDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDPopupHoursGvHoursDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDPopupHoursMinNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDPopupHoursMaxNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLyItemMaxHours As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemMinHours As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColConceptName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColHoursNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDDBEmployee2 As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDDDBEmployee1 As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDPopupHoursWorkedPercentage As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLyItemWorkedPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupLcTxtTemplateHoursNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLyItemDetailTemplateHoursNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcEmployees As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLyItemTxtEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDDBOptionsMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDLyItemOptionMenu As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPcDetailEm2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDPcDetailEm1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDLyDelete As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemLayoutDelete As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDelSBCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDDelSBFinish As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcSelectSchedule As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLyItemTxtDeleteInformation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemButtonFinishDelete As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemButtonCancelDelete As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBarNewScheduleDetail As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBarMarkAsDelete As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBarDeleteAll As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents BarCheckItem1 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents INDBarMarkEmployee As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents INDPcCMore As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents LayoutControl4 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDPopupMoreLcTxtOtherSchHoursNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDPopupMoreGcHoursDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDPopupMoreGvHoursDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FunctionalUnitMore As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Template As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPopupMoreLcNumberDay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemOtherSchDet As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupMoreLcEmployee As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDBarRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDLyItemScheduleDetailPanelComplete As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcDetHours As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDetHours As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDTcgDetailsSchedule As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlyGrDetHours As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyCtrGrMatchPartnerts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemDetailScheduleMates As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEvent As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNextDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColApproval As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTcgHours As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLyGrHourLiquidation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemHoursGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGrPendingEvents As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPopupHoursGcHoursPendingDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDPopupHoursGvHoursPendingDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLyItemHoursPendingGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBarEmployee As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDSlPosition As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciPosition As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cargoName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
End Class
