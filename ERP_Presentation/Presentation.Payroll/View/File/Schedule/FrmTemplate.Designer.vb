<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTemplate
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
        Dim TimeRuler1 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler2 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTemplate))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.SchedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDScheduleControl = New DevExpress.XtraScheduler.SchedulerControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDLyGrTemplateToUse = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGrLocation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGleFunctionalUnit = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColFUName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBOName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColComName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.CtrCalendarMini = New Presentation.Controls.CtrCalendarMini()
        Me.INDDDBAddHours = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPccAddHours = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDLyControlPopUpNewHours = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPopupRgInitialNextDay = New DevExpress.XtraEditors.RadioGroup()
        Me.INDPopupTeEndingTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDPopupTeInitialTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDSbAddHours = New DevExpress.XtraEditors.SimpleButton()
        Me.INDPopupRgNextDay = New DevExpress.XtraEditors.RadioGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemNextDay = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemSaveHours = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInitialNextDay = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemInitialTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemEndingTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcScheduleMates = New DevExpress.XtraEditors.LabelControl()
        Me.INDLcEmployee = New DevExpress.XtraEditors.LabelControl()
        Me.INDGcDetailHours = New DevExpress.XtraGrid.GridControl()
        Me.INDGvDetailHours = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColInitialTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepTimeEdit = New DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit()
        Me.INDColEndingTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNextDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEvent = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepRgEvent = New DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup()
        Me.INDColEditing = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPccEditHours = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGPopupTeEndingTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDGPopupTeInitialTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDGSbAddHours = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGPopupRgEvent = New DevExpress.XtraEditors.RadioGroup()
        Me.INDGPopupRgNextDay = New DevExpress.XtraEditors.RadioGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGLyItemNextDay = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGLyItemEvent1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGLyItemInitialTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGLyItemEndingTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.RepCheckEditNextDay = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDRgEvent = New DevExpress.XtraEditors.RadioGroup()
        Me.INDGleScheduleTemplate = New Presentation.Controls.GridLookUpMultiFilter()
        Me.CustomGridView1 = New Presentation.Controls.CustomGridView()
        Me.INDTemplateCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTemplateName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDCbeFrequency = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.INDDeEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDChkWeekDays = New DevExpress.XtraScheduler.UI.WeekDaysCheckEdit()
        Me.INDTxtRepeatDays = New DevExpress.XtraEditors.SpinEdit()
        Me.INDCLBCEmployee = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.Days = New DevExpress.XtraEditors.LabelControl()
        Me.INDRgHoliday = New DevExpress.XtraEditors.RadioGroup()
        Me.INDLyGrFrequency = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyRepeatDays1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyHoliday = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyRepeatDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyWeekDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyFrequency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyMonthDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGrRepeatInterval = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyStarDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyControlScheduleTemplate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGrEvent = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemEvent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemDetailHours = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemAddHours = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemEmployeeSelected = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemScheduleMatch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDColTemplateCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTemplateDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDScheduleControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrTemplateToUse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDPccAddHours, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccAddHours.SuspendLayout()
        CType(Me.INDLyControlPopUpNewHours, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyControlPopUpNewHours.SuspendLayout()
        CType(Me.INDPopupRgInitialNextDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupTeEndingTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupTeInitialTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupRgNextDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemNextDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemSaveHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInitialNextDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemInitialTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemEndingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDetailHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvDetailHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepTimeEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepRgEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepPopupContainerEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccEditHours, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccEditHours.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDGPopupTeEndingTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGPopupTeInitialTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGPopupRgEvent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGPopupRgNextDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGLyItemNextDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGLyItemEvent1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGLyItemInitialTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGLyItemEndingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCheckEditNextDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRgEvent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleScheduleTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeFrequency.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkWeekDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtRepeatDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCLBCEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRgHoliday.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyRepeatDays1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyRepeatDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyWeekDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyMonthDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrRepeatInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyStarDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyControlScheduleTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemEvent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemAddHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemEmployeeSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemScheduleMatch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
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
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
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
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLyGrTemplateToUse, Me.INDLyItemEmployee, Me.INDLyItemEmployeeSelected, Me.INDLyItemScheduleMatch})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 10, 0, 10)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(987, 950)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDScheduleControl
        resources.ApplyResources(Me.LayoutControlItem1, "LayoutControlItem1")
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 370)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(265, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(265, 25)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(265, 570)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDScheduleControl
        '
        Me.INDScheduleControl.DataStorage = Me.SchedulerStorage1
        resources.ApplyResources(Me.INDScheduleControl, "INDScheduleControl")
        Me.INDScheduleControl.MenuManager = Me.BarManager1
        Me.INDScheduleControl.Name = "INDScheduleControl"
        Me.INDScheduleControl.OptionsBehavior.RecurrentAppointmentDeleteAction = DevExpress.XtraScheduler.RecurrentAppointmentAction.Cancel
        Me.INDScheduleControl.OptionsBehavior.RecurrentAppointmentEditAction = DevExpress.XtraScheduler.RecurrentAppointmentAction.Cancel
        Me.INDScheduleControl.OptionsBehavior.ShowRemindersForm = False
        Me.INDScheduleControl.OptionsCustomization.AllowAppointmentCreate = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDScheduleControl.OptionsCustomization.AllowAppointmentDelete = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDScheduleControl.OptionsCustomization.AllowAppointmentDrag = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDScheduleControl.OptionsCustomization.AllowAppointmentEdit = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDScheduleControl.OptionsRangeControl.AllowChangeActiveView = False
        Me.INDScheduleControl.ResourceNavigator.Visibility = DevExpress.XtraScheduler.ResourceNavigatorVisibility.Never
        Me.INDScheduleControl.Start = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.INDScheduleControl.Views.DayView.DayCount = 2
        TimeRuler1.TimeZoneId = "SA Pacific Standard Time"
        TimeRuler1.UseClientTimeZone = False
        Me.INDScheduleControl.Views.DayView.TimeRulers.Add(TimeRuler1)
        Me.INDScheduleControl.Views.DayView.TimeScale = System.TimeSpan.Parse("01:00:00")
        Me.INDScheduleControl.Views.GanttView.Enabled = False
        Me.INDScheduleControl.Views.MonthView.Enabled = False
        Me.INDScheduleControl.Views.TimelineView.Enabled = False
        Me.INDScheduleControl.Views.WeekView.Enabled = False
        Me.INDScheduleControl.Views.WorkWeekView.Enabled = False
        TimeRuler2.TimeZoneId = "SA Pacific Standard Time"
        TimeRuler2.UseClientTimeZone = False
        Me.INDScheduleControl.Views.WorkWeekView.TimeRulers.Add(TimeRuler2)
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.DockControls.Add(Me.BarDockControl2)
        Me.BarManager1.DockControls.Add(Me.BarDockControl3)
        Me.BarManager1.DockControls.Add(Me.BarDockControl4)
        Me.BarManager1.Form = Me
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
        'INDLyGrTemplateToUse
        '
        Me.INDLyGrTemplateToUse.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrTemplateToUse.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLyGrTemplateToUse.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrTemplateToUse.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrTemplateToUse.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrTemplateToUse.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrTemplateToUse.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrTemplateToUse.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrTemplateToUse.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrTemplateToUse.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrTemplateToUse.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLyGrTemplateToUse.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrTemplateToUse.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrTemplateToUse.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrTemplateToUse.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrTemplateToUse.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrTemplateToUse.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLyGrTemplateToUse.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrTemplateToUse.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrTemplateToUse.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrTemplateToUse.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrTemplateToUse, False)
        resources.ApplyResources(Me.INDLyGrTemplateToUse, "INDLyGrTemplateToUse")
        Me.INDLyGrTemplateToUse.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGrLocation, Me.INDLyGrFrequency, Me.INDLyGrRepeatInterval, Me.INDLyControlScheduleTemplate, Me.INDLyGrEvent})
        Me.INDLyGrTemplateToUse.Location = New System.Drawing.Point(265, 38)
        Me.INDLyGrTemplateToUse.Name = "INDLyGrTemplateToUse"
        Me.INDLyGrTemplateToUse.Size = New System.Drawing.Size(702, 902)
        '
        'INDLyGrLocation
        '
        Me.INDLyGrLocation.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrLocation.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLyGrLocation.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrLocation.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrLocation.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrLocation.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrLocation.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrLocation.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrLocation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrLocation.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrLocation.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLyGrLocation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrLocation.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrLocation.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrLocation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrLocation.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrLocation.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLyGrLocation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrLocation.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrLocation.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrLocation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrLocation, False)
        resources.ApplyResources(Me.INDLyGrLocation, "INDLyGrLocation")
        Me.INDLyGrLocation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemFunctionalUnit})
        Me.INDLyGrLocation.Location = New System.Drawing.Point(0, 760)
        Me.INDLyGrLocation.Name = "INDLyGrLocation"
        Me.INDLyGrLocation.Size = New System.Drawing.Size(678, 89)
        '
        'INDLyItemFunctionalUnit
        '
        Me.INDLyItemFunctionalUnit.Control = Me.INDGleFunctionalUnit
        resources.ApplyResources(Me.INDLyItemFunctionalUnit, "INDLyItemFunctionalUnit")
        Me.INDLyItemFunctionalUnit.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemFunctionalUnit.MaxSize = New System.Drawing.Size(645, 36)
        Me.INDLyItemFunctionalUnit.MinSize = New System.Drawing.Size(645, 36)
        Me.INDLyItemFunctionalUnit.Name = "INDLyItemFunctionalUnit"
        Me.INDLyItemFunctionalUnit.Size = New System.Drawing.Size(654, 36)
        Me.INDLyItemFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemFunctionalUnit.TextSize = New System.Drawing.Size(166, 21)
        Me.INDLyItemFunctionalUnit.TextToControlDistance = 12
        '
        'INDGleFunctionalUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleFunctionalUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleFunctionalUnit, False)
        resources.ApplyResources(Me.INDGleFunctionalUnit, "INDGleFunctionalUnit")
        Me.IndigoTextEdit1.SetMascara(Me.INDGleFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleFunctionalUnit.Name = "INDGleFunctionalUnit"
        Me.INDGleFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleFunctionalUnit.Properties.Appearance.Font = CType(resources.GetObject("INDGleFunctionalUnit.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDGleFunctionalUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleFunctionalUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleFunctionalUnit.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGleFunctionalUnit.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGleFunctionalUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleFunctionalUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleFunctionalUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleFunctionalUnit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGleFunctionalUnit.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGleFunctionalUnit.Properties.DisplayMember = "Descripcion"
        Me.INDGleFunctionalUnit.Properties.NullText = resources.GetString("INDGleFunctionalUnit.Properties.NullText")
        Me.INDGleFunctionalUnit.Properties.PopupView = Me.GridView1
        Me.INDGleFunctionalUnit.Properties.ValueMember = "Codigo"
        Me.INDGleFunctionalUnit.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleFunctionalUnit, 0)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = CType(resources.GetObject("GridView1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColFUName, Me.INDColBOName, Me.INDColComName})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDColFUName
        '
        resources.ApplyResources(Me.INDColFUName, "INDColFUName")
        Me.INDColFUName.FieldName = "Descripcion"
        Me.INDColFUName.Name = "INDColFUName"
        '
        'INDColBOName
        '
        resources.ApplyResources(Me.INDColBOName, "INDColBOName")
        Me.INDColBOName.FieldName = "BranchOfficeId.Descripcion"
        Me.INDColBOName.Name = "INDColBOName"
        '
        'INDColComName
        '
        resources.ApplyResources(Me.INDColComName, "INDColComName")
        Me.INDColComName.FieldName = "BranchOfficeId.CompanyId.Descripcion"
        Me.INDColComName.Name = "INDColComName"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.CtrCalendarMini)
        Me.LayoutControl1.Controls.Add(Me.INDDDBAddHours)
        Me.LayoutControl1.Controls.Add(Me.INDLcScheduleMates)
        Me.LayoutControl1.Controls.Add(Me.INDLcEmployee)
        Me.LayoutControl1.Controls.Add(Me.INDGcDetailHours)
        Me.LayoutControl1.Controls.Add(Me.INDRgEvent)
        Me.LayoutControl1.Controls.Add(Me.INDGleScheduleTemplate)
        Me.LayoutControl1.Controls.Add(Me.INDCbeFrequency)
        Me.LayoutControl1.Controls.Add(Me.INDDeEndDate)
        Me.LayoutControl1.Controls.Add(Me.INDDeStartDate)
        Me.LayoutControl1.Controls.Add(Me.INDGleFunctionalUnit)
        Me.LayoutControl1.Controls.Add(Me.INDChkWeekDays)
        Me.LayoutControl1.Controls.Add(Me.INDTxtRepeatDays)
        Me.LayoutControl1.Controls.Add(Me.INDCLBCEmployee)
        Me.LayoutControl1.Controls.Add(Me.Days)
        Me.LayoutControl1.Controls.Add(Me.INDRgHoliday)
        Me.LayoutControl1.Controls.Add(Me.INDScheduleControl)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(54, 152, 476, 573)
        Me.LayoutControl1.OptionsFocus.EnableAutoTabOrder = False
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'CtrCalendarMini
        '
        Me.CtrCalendarMini.ListDaysToDelete = CType(resources.GetObject("CtrCalendarMini.ListDaysToDelete"), System.Collections.Generic.List(Of Integer))
        resources.ApplyResources(Me.CtrCalendarMini, "CtrCalendarMini")
        Me.CtrCalendarMini.MonthControl = 3
        Me.CtrCalendarMini.Name = "CtrCalendarMini"
        Me.CtrCalendarMini.YearControl = 2014
        '
        'INDDDBAddHours
        '
        Me.INDDDBAddHours.Appearance.Font = CType(resources.GetObject("INDDDBAddHours.Appearance.Font"), System.Drawing.Font)
        Me.INDDDBAddHours.Appearance.Options.UseFont = True
        Me.INDDDBAddHours.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDDBAddHours.DropDownControl = Me.INDPccAddHours
        resources.ApplyResources(Me.INDDDBAddHours, "INDDDBAddHours")
        Me.INDDDBAddHours.Name = "INDDDBAddHours"
        Me.INDDDBAddHours.StyleController = Me.LayoutControl1
        '
        'INDPccAddHours
        '
        Me.INDPccAddHours.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPccAddHours.Appearance.Options.UseBackColor = True
        Me.INDPccAddHours.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPccAddHours.Controls.Add(Me.INDLyControlPopUpNewHours)
        resources.ApplyResources(Me.INDPccAddHours, "INDPccAddHours")
        Me.INDPccAddHours.Manager = Me.BarManager1
        Me.INDPccAddHours.Name = "INDPccAddHours"
        '
        'INDLyControlPopUpNewHours
        '
        Me.INDLyControlPopUpNewHours.Controls.Add(Me.INDPopupRgInitialNextDay)
        Me.INDLyControlPopUpNewHours.Controls.Add(Me.INDPopupTeEndingTime)
        Me.INDLyControlPopUpNewHours.Controls.Add(Me.INDPopupTeInitialTime)
        Me.INDLyControlPopUpNewHours.Controls.Add(Me.INDSbAddHours)
        Me.INDLyControlPopUpNewHours.Controls.Add(Me.INDPopupRgNextDay)
        resources.ApplyResources(Me.INDLyControlPopUpNewHours, "INDLyControlPopUpNewHours")
        Me.INDLyControlPopUpNewHours.Name = "INDLyControlPopUpNewHours"
        Me.INDLyControlPopUpNewHours.Root = Me.LayoutControlGroup2
        '
        'INDPopupRgInitialNextDay
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDPopupRgInitialNextDay, False)
        resources.ApplyResources(Me.INDPopupRgInitialNextDay, "INDPopupRgInitialNextDay")
        Me.INDPopupRgInitialNextDay.MenuManager = Me.BarManager1
        Me.INDPopupRgInitialNextDay.Name = "INDPopupRgInitialNextDay"
        Me.INDPopupRgInitialNextDay.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPopupRgInitialNextDay.Properties.Appearance.Font = CType(resources.GetObject("INDPopupRgInitialNextDay.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupRgInitialNextDay.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopupRgInitialNextDay.Properties.Appearance.Options.UseFont = True
        Me.INDPopupRgInitialNextDay.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopupRgInitialNextDay.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopupRgInitialNextDay.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDPopupRgInitialNextDay.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDPopupRgInitialNextDay.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopupRgInitialNextDay.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopupRgInitialNextDay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopupRgInitialNextDay.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDPopupRgInitialNextDay.Properties.Items"), Object), resources.GetString("INDPopupRgInitialNextDay.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDPopupRgInitialNextDay.Properties.Items2"), Object), resources.GetString("INDPopupRgInitialNextDay.Properties.Items3"))})
        Me.INDPopupRgInitialNextDay.StyleController = Me.INDLyControlPopUpNewHours
        '
        'INDPopupTeEndingTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopupTeEndingTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopupTeEndingTime, False)
        resources.ApplyResources(Me.INDPopupTeEndingTime, "INDPopupTeEndingTime")
        Me.INDPopupTeEndingTime.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDPopupTeEndingTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDPopupTeEndingTime.MenuManager = Me.BarManager1
        Me.INDPopupTeEndingTime.Name = "INDPopupTeEndingTime"
        Me.INDPopupTeEndingTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopupTeEndingTime.Properties.Appearance.Font = CType(resources.GetObject("INDPopupTeEndingTime.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupTeEndingTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopupTeEndingTime.Properties.Appearance.Options.UseFont = True
        Me.INDPopupTeEndingTime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopupTeEndingTime.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopupTeEndingTime.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDPopupTeEndingTime.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDPopupTeEndingTime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopupTeEndingTime.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopupTeEndingTime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopupTeEndingTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDPopupTeEndingTime.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDPopupTeEndingTime.StyleController = Me.INDLyControlPopUpNewHours
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopupTeEndingTime, 0)
        '
        'INDPopupTeInitialTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDPopupTeInitialTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDPopupTeInitialTime, False)
        resources.ApplyResources(Me.INDPopupTeInitialTime, "INDPopupTeInitialTime")
        Me.INDPopupTeInitialTime.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDPopupTeInitialTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDPopupTeInitialTime.MenuManager = Me.BarManager1
        Me.INDPopupTeInitialTime.Name = "INDPopupTeInitialTime"
        Me.INDPopupTeInitialTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPopupTeInitialTime.Properties.Appearance.Font = CType(resources.GetObject("INDPopupTeInitialTime.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupTeInitialTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopupTeInitialTime.Properties.Appearance.Options.UseFont = True
        Me.INDPopupTeInitialTime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopupTeInitialTime.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopupTeInitialTime.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDPopupTeInitialTime.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDPopupTeInitialTime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopupTeInitialTime.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopupTeInitialTime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopupTeInitialTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDPopupTeInitialTime.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDPopupTeInitialTime.StyleController = Me.INDLyControlPopUpNewHours
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDPopupTeInitialTime, 0)
        '
        'INDSbAddHours
        '
        resources.ApplyResources(Me.INDSbAddHours, "INDSbAddHours")
        Me.INDSbAddHours.Name = "INDSbAddHours"
        Me.INDSbAddHours.StyleController = Me.INDLyControlPopUpNewHours
        '
        'INDPopupRgNextDay
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDPopupRgNextDay, False)
        resources.ApplyResources(Me.INDPopupRgNextDay, "INDPopupRgNextDay")
        Me.INDPopupRgNextDay.EnterMoveNextControl = True
        Me.INDPopupRgNextDay.MenuManager = Me.BarManager1
        Me.INDPopupRgNextDay.Name = "INDPopupRgNextDay"
        Me.INDPopupRgNextDay.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPopupRgNextDay.Properties.Appearance.Font = CType(resources.GetObject("INDPopupRgNextDay.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDPopupRgNextDay.Properties.Appearance.Options.UseBackColor = True
        Me.INDPopupRgNextDay.Properties.Appearance.Options.UseFont = True
        Me.INDPopupRgNextDay.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDPopupRgNextDay.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPopupRgNextDay.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDPopupRgNextDay.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDPopupRgNextDay.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDPopupRgNextDay.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDPopupRgNextDay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDPopupRgNextDay.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDPopupRgNextDay.Properties.Items"), Object), resources.GetString("INDPopupRgNextDay.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDPopupRgNextDay.Properties.Items2"), Object), resources.GetString("INDPopupRgNextDay.Properties.Items3"))})
        Me.INDPopupRgNextDay.StyleController = Me.INDLyControlPopUpNewHours
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemNextDay, Me.INDLyItemSaveHours, Me.INDlyItemInitialNextDay, Me.INDLyItemInitialTime, Me.INDLyItemEndingTime})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(620, 185)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLyItemNextDay
        '
        Me.INDLyItemNextDay.Control = Me.INDPopupRgNextDay
        resources.ApplyResources(Me.INDLyItemNextDay, "INDLyItemNextDay")
        Me.INDLyItemNextDay.Location = New System.Drawing.Point(0, 72)
        Me.INDLyItemNextDay.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemNextDay.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemNextDay.Name = "INDLyItemNextDay"
        Me.INDLyItemNextDay.Size = New System.Drawing.Size(600, 36)
        Me.INDLyItemNextDay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemNextDay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemNextDay.TextSize = New System.Drawing.Size(126, 13)
        Me.INDLyItemNextDay.TextToControlDistance = 12
        '
        'INDLyItemSaveHours
        '
        Me.INDLyItemSaveHours.Control = Me.INDSbAddHours
        resources.ApplyResources(Me.INDLyItemSaveHours, "INDLyItemSaveHours")
        Me.INDLyItemSaveHours.Location = New System.Drawing.Point(0, 108)
        Me.INDLyItemSaveHours.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDLyItemSaveHours.MinSize = New System.Drawing.Size(183, 36)
        Me.INDLyItemSaveHours.Name = "INDLyItemSaveHours"
        Me.INDLyItemSaveHours.Size = New System.Drawing.Size(600, 57)
        Me.INDLyItemSaveHours.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemSaveHours.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemSaveHours.TextVisible = False
        '
        'INDlyItemInitialNextDay
        '
        Me.INDlyItemInitialNextDay.Control = Me.INDPopupRgInitialNextDay
        resources.ApplyResources(Me.INDlyItemInitialNextDay, "INDlyItemInitialNextDay")
        Me.INDlyItemInitialNextDay.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemInitialNextDay.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemInitialNextDay.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemInitialNextDay.Name = "INDlyItemInitialNextDay"
        Me.INDlyItemInitialNextDay.Size = New System.Drawing.Size(600, 36)
        Me.INDlyItemInitialNextDay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitialNextDay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitialNextDay.TextSize = New System.Drawing.Size(126, 13)
        Me.INDlyItemInitialNextDay.TextToControlDistance = 12
        Me.INDlyItemInitialNextDay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyItemInitialTime
        '
        Me.INDLyItemInitialTime.Control = Me.INDPopupTeInitialTime
        resources.ApplyResources(Me.INDLyItemInitialTime, "INDLyItemInitialTime")
        Me.INDLyItemInitialTime.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemInitialTime.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemInitialTime.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemInitialTime.Name = "INDLyItemInitialTime"
        Me.INDLyItemInitialTime.Size = New System.Drawing.Size(300, 36)
        Me.INDLyItemInitialTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemInitialTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemInitialTime.TextSize = New System.Drawing.Size(126, 13)
        Me.INDLyItemInitialTime.TextToControlDistance = 12
        '
        'INDLyItemEndingTime
        '
        Me.INDLyItemEndingTime.Control = Me.INDPopupTeEndingTime
        resources.ApplyResources(Me.INDLyItemEndingTime, "INDLyItemEndingTime")
        Me.INDLyItemEndingTime.Location = New System.Drawing.Point(300, 36)
        Me.INDLyItemEndingTime.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemEndingTime.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLyItemEndingTime.Name = "INDLyItemEndingTime"
        Me.INDLyItemEndingTime.Size = New System.Drawing.Size(300, 36)
        Me.INDLyItemEndingTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemEndingTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemEndingTime.TextSize = New System.Drawing.Size(126, 13)
        Me.INDLyItemEndingTime.TextToControlDistance = 12
        '
        'INDLcScheduleMates
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcScheduleMates, True)
        Me.INDLcScheduleMates.Appearance.Font = CType(resources.GetObject("INDLcScheduleMates.Appearance.Font"), System.Drawing.Font)
        Me.INDLcScheduleMates.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDLcScheduleMates.Appearance.Options.UseFont = True
        Me.INDLcScheduleMates.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcScheduleMates, False)
        Me.INDLcScheduleMates.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.INDLcScheduleMates, "INDLcScheduleMates")
        Me.INDLcScheduleMates.Name = "INDLcScheduleMates"
        Me.INDLcScheduleMates.StyleController = Me.LayoutControl1
        '
        'INDLcEmployee
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcEmployee, True)
        Me.INDLcEmployee.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDLcEmployee.Appearance.Font = CType(resources.GetObject("INDLcEmployee.Appearance.Font"), System.Drawing.Font)
        Me.INDLcEmployee.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDLcEmployee.Appearance.Options.UseBackColor = True
        Me.INDLcEmployee.Appearance.Options.UseFont = True
        Me.INDLcEmployee.Appearance.Options.UseForeColor = True
        Me.INDLcEmployee.Appearance.Options.UseTextOptions = True
        Me.INDLcEmployee.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        resources.ApplyResources(Me.INDLcEmployee, "INDLcEmployee")
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcEmployee, False)
        Me.INDLcEmployee.Name = "INDLcEmployee"
        Me.INDLcEmployee.StyleController = Me.LayoutControl1
        '
        'INDGcDetailHours
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDetailHours, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDetailHours, Nothing)
        Me.INDGcDetailHours.EmbeddedNavigator.Appearance.Font = CType(resources.GetObject("INDGcDetailHours.EmbeddedNavigator.Appearance.Font"), System.Drawing.Font)
        Me.INDGcDetailHours.EmbeddedNavigator.Appearance.Options.UseFont = True
        Me.INDGcDetailHours.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.INDGcDetailHours.EmbeddedNavigator.Buttons.First.Visible = False
        Me.INDGcDetailHours.EmbeddedNavigator.Buttons.Last.Visible = False
        Me.INDGcDetailHours.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.INDGcDetailHours.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.INDGcDetailHours.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDetailHours, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDetailHours, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDetailHours, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDetailHours, False)
        resources.ApplyResources(Me.INDGcDetailHours, "INDGcDetailHours")
        Me.INDGcDetailHours.MainView = Me.INDGvDetailHours
        Me.INDGcDetailHours.Name = "INDGcDetailHours"
        Me.INDGcDetailHours.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepRgEvent, Me.RepCheckEditNextDay, Me.RepTimeEdit, Me.RepPopupContainerEdit})
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDetailHours, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcDetailHours.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvDetailHours})
        '
        'INDGvDetailHours
        '
        Me.INDGvDetailHours.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvDetailHours.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvDetailHours.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvDetailHours.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvDetailHours.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvDetailHours.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvDetailHours.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvDetailHours.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvDetailHours.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvDetailHours.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvDetailHours.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvDetailHours.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvDetailHours.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvDetailHours.Appearance.Row.Font = CType(resources.GetObject("INDGvDetailHours.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDGvDetailHours.Appearance.Row.Options.UseFont = True
        Me.INDGvDetailHours.Appearance.ViewCaption.Font = CType(resources.GetObject("INDGvDetailHours.Appearance.ViewCaption.Font"), System.Drawing.Font)
        Me.INDGvDetailHours.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvDetailHours.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColInitialTime, Me.INDColEndingTime, Me.INDColNextDay, Me.INDColEvent, Me.INDColEditing})
        Me.INDGvDetailHours.GridControl = Me.INDGcDetailHours
        Me.INDGvDetailHours.Name = "INDGvDetailHours"
        Me.INDGvDetailHours.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvDetailHours.OptionsCustomization.AllowColumnMoving = False
        Me.INDGvDetailHours.OptionsCustomization.AllowColumnResizing = False
        Me.INDGvDetailHours.OptionsCustomization.AllowFilter = False
        Me.INDGvDetailHours.OptionsCustomization.AllowGroup = False
        Me.INDGvDetailHours.OptionsCustomization.AllowQuickHideColumns = False
        Me.INDGvDetailHours.OptionsCustomization.AllowSort = False
        Me.INDGvDetailHours.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvDetailHours.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvDetailHours.OptionsView.ShowAutoFilterRow = True
        Me.INDGvDetailHours.OptionsView.ShowDetailButtons = False
        Me.INDGvDetailHours.OptionsView.ShowGroupExpandCollapseButtons = False
        Me.INDGvDetailHours.OptionsView.ShowGroupPanel = False
        Me.INDGvDetailHours.Tag = 150
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvDetailHours, False)
        '
        'INDColInitialTime
        '
        resources.ApplyResources(Me.INDColInitialTime, "INDColInitialTime")
        Me.INDColInitialTime.ColumnEdit = Me.RepTimeEdit
        Me.INDColInitialTime.DisplayFormat.FormatString = "HH:mm"
        Me.INDColInitialTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColInitialTime.FieldName = "DateTimeInitial"
        Me.INDColInitialTime.Name = "INDColInitialTime"
        Me.INDColInitialTime.OptionsColumn.AllowEdit = False
        '
        'RepTimeEdit
        '
        resources.ApplyResources(Me.RepTimeEdit, "RepTimeEdit")
        Me.RepTimeEdit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepTimeEdit.Name = "RepTimeEdit"
        '
        'INDColEndingTime
        '
        resources.ApplyResources(Me.INDColEndingTime, "INDColEndingTime")
        Me.INDColEndingTime.ColumnEdit = Me.RepTimeEdit
        Me.INDColEndingTime.DisplayFormat.FormatString = "HH:mm"
        Me.INDColEndingTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDColEndingTime.FieldName = "DateTimeEnding"
        Me.INDColEndingTime.Name = "INDColEndingTime"
        Me.INDColEndingTime.OptionsColumn.AllowEdit = False
        '
        'INDColNextDay
        '
        resources.ApplyResources(Me.INDColNextDay, "INDColNextDay")
        Me.INDColNextDay.FieldName = "NextDay"
        Me.INDColNextDay.Name = "INDColNextDay"
        Me.INDColNextDay.OptionsColumn.AllowEdit = False
        '
        'INDColEvent
        '
        resources.ApplyResources(Me.INDColEvent, "INDColEvent")
        Me.INDColEvent.ColumnEdit = Me.RepRgEvent
        Me.INDColEvent.FieldName = "Event"
        Me.INDColEvent.Name = "INDColEvent"
        Me.INDColEvent.OptionsColumn.AllowEdit = False
        '
        'RepRgEvent
        '
        Me.RepRgEvent.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RepRgEvent.Items"), Object), resources.GetString("RepRgEvent.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("RepRgEvent.Items2"), Object), resources.GetString("RepRgEvent.Items3"))})
        Me.RepRgEvent.Name = "RepRgEvent"
        '
        'INDColEditing
        '
        resources.ApplyResources(Me.INDColEditing, "INDColEditing")
        Me.INDColEditing.ColumnEdit = Me.RepPopupContainerEdit
        Me.INDColEditing.Name = "INDColEditing"
        '
        'RepPopupContainerEdit
        '
        Me.RepPopupContainerEdit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepPopupContainerEdit.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepPopupContainerEdit.Name = "RepPopupContainerEdit"
        Me.RepPopupContainerEdit.PopupControl = Me.INDPccEditHours
        Me.RepPopupContainerEdit.PopupSizeable = False
        Me.RepPopupContainerEdit.ShowPopupCloseButton = False
        Me.RepPopupContainerEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPccEditHours
        '
        Me.INDPccEditHours.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPccEditHours.Appearance.Options.UseBackColor = True
        Me.INDPccEditHours.Controls.Add(Me.LayoutControl2)
        resources.ApplyResources(Me.INDPccEditHours, "INDPccEditHours")
        Me.INDPccEditHours.Name = "INDPccEditHours"
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDGPopupTeEndingTime)
        Me.LayoutControl2.Controls.Add(Me.INDGPopupTeInitialTime)
        Me.LayoutControl2.Controls.Add(Me.INDGSbAddHours)
        Me.LayoutControl2.Controls.Add(Me.INDGPopupRgEvent)
        Me.LayoutControl2.Controls.Add(Me.INDGPopupRgNextDay)
        resources.ApplyResources(Me.LayoutControl2, "LayoutControl2")
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup3
        '
        'INDGPopupTeEndingTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGPopupTeEndingTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGPopupTeEndingTime, False)
        resources.ApplyResources(Me.INDGPopupTeEndingTime, "INDGPopupTeEndingTime")
        Me.INDGPopupTeEndingTime.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDGPopupTeEndingTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGPopupTeEndingTime.MenuManager = Me.BarManager1
        Me.INDGPopupTeEndingTime.Name = "INDGPopupTeEndingTime"
        Me.INDGPopupTeEndingTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGPopupTeEndingTime.Properties.Appearance.Font = CType(resources.GetObject("INDGPopupTeEndingTime.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGPopupTeEndingTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDGPopupTeEndingTime.Properties.Appearance.Options.UseFont = True
        Me.INDGPopupTeEndingTime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGPopupTeEndingTime.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGPopupTeEndingTime.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGPopupTeEndingTime.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGPopupTeEndingTime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGPopupTeEndingTime.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGPopupTeEndingTime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGPopupTeEndingTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGPopupTeEndingTime.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGPopupTeEndingTime.StyleController = Me.LayoutControl2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGPopupTeEndingTime, 0)
        '
        'INDGPopupTeInitialTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGPopupTeInitialTime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGPopupTeInitialTime, False)
        resources.ApplyResources(Me.INDGPopupTeInitialTime, "INDGPopupTeInitialTime")
        Me.INDGPopupTeInitialTime.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDGPopupTeInitialTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGPopupTeInitialTime.MenuManager = Me.BarManager1
        Me.INDGPopupTeInitialTime.Name = "INDGPopupTeInitialTime"
        Me.INDGPopupTeInitialTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGPopupTeInitialTime.Properties.Appearance.Font = CType(resources.GetObject("INDGPopupTeInitialTime.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGPopupTeInitialTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDGPopupTeInitialTime.Properties.Appearance.Options.UseFont = True
        Me.INDGPopupTeInitialTime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGPopupTeInitialTime.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGPopupTeInitialTime.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGPopupTeInitialTime.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGPopupTeInitialTime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGPopupTeInitialTime.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGPopupTeInitialTime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGPopupTeInitialTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGPopupTeInitialTime.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGPopupTeInitialTime.StyleController = Me.LayoutControl2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGPopupTeInitialTime, 0)
        '
        'INDGSbAddHours
        '
        resources.ApplyResources(Me.INDGSbAddHours, "INDGSbAddHours")
        Me.INDGSbAddHours.Name = "INDGSbAddHours"
        Me.INDGSbAddHours.StyleController = Me.LayoutControl2
        '
        'INDGPopupRgEvent
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDGPopupRgEvent, False)
        resources.ApplyResources(Me.INDGPopupRgEvent, "INDGPopupRgEvent")
        Me.INDGPopupRgEvent.EnterMoveNextControl = True
        Me.INDGPopupRgEvent.MenuManager = Me.BarManager1
        Me.INDGPopupRgEvent.Name = "INDGPopupRgEvent"
        Me.INDGPopupRgEvent.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDGPopupRgEvent.Properties.Appearance.Font = CType(resources.GetObject("INDGPopupRgEvent.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGPopupRgEvent.Properties.Appearance.Options.UseBackColor = True
        Me.INDGPopupRgEvent.Properties.Appearance.Options.UseFont = True
        Me.INDGPopupRgEvent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGPopupRgEvent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGPopupRgEvent.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGPopupRgEvent.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGPopupRgEvent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGPopupRgEvent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGPopupRgEvent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGPopupRgEvent.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDGPopupRgEvent.Properties.Items"), Object), resources.GetString("INDGPopupRgEvent.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDGPopupRgEvent.Properties.Items2"), Object), resources.GetString("INDGPopupRgEvent.Properties.Items3"))})
        Me.INDGPopupRgEvent.StyleController = Me.LayoutControl2
        '
        'INDGPopupRgNextDay
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDGPopupRgNextDay, False)
        resources.ApplyResources(Me.INDGPopupRgNextDay, "INDGPopupRgNextDay")
        Me.INDGPopupRgNextDay.EnterMoveNextControl = True
        Me.INDGPopupRgNextDay.MenuManager = Me.BarManager1
        Me.INDGPopupRgNextDay.Name = "INDGPopupRgNextDay"
        Me.INDGPopupRgNextDay.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDGPopupRgNextDay.Properties.Appearance.Font = CType(resources.GetObject("INDGPopupRgNextDay.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGPopupRgNextDay.Properties.Appearance.Options.UseBackColor = True
        Me.INDGPopupRgNextDay.Properties.Appearance.Options.UseFont = True
        Me.INDGPopupRgNextDay.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGPopupRgNextDay.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGPopupRgNextDay.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGPopupRgNextDay.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGPopupRgNextDay.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGPopupRgNextDay.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGPopupRgNextDay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGPopupRgNextDay.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDGPopupRgNextDay.Properties.Items"), Object), resources.GetString("INDGPopupRgNextDay.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDGPopupRgNextDay.Properties.Items2"), Object), resources.GetString("INDGPopupRgNextDay.Properties.Items3"))})
        Me.INDGPopupRgNextDay.StyleController = Me.LayoutControl2
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        resources.ApplyResources(Me.LayoutControlGroup3, "LayoutControlGroup3")
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGLyItemNextDay, Me.INDGLyItemEvent1, Me.LayoutControlItem5, Me.INDGLyItemInitialTime, Me.INDGLyItemEndingTime})
        Me.LayoutControlGroup3.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(300, 200)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'INDGLyItemNextDay
        '
        Me.INDGLyItemNextDay.Control = Me.INDGPopupRgNextDay
        resources.ApplyResources(Me.INDGLyItemNextDay, "INDGLyItemNextDay")
        Me.INDGLyItemNextDay.Location = New System.Drawing.Point(0, 72)
        Me.INDGLyItemNextDay.MaxSize = New System.Drawing.Size(280, 36)
        Me.INDGLyItemNextDay.MinSize = New System.Drawing.Size(280, 36)
        Me.INDGLyItemNextDay.Name = "INDGLyItemNextDay"
        Me.INDGLyItemNextDay.Size = New System.Drawing.Size(280, 36)
        Me.INDGLyItemNextDay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDGLyItemNextDay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDGLyItemNextDay.TextSize = New System.Drawing.Size(106, 13)
        Me.INDGLyItemNextDay.TextToControlDistance = 12
        '
        'INDGLyItemEvent1
        '
        Me.INDGLyItemEvent1.Control = Me.INDGPopupRgEvent
        resources.ApplyResources(Me.INDGLyItemEvent1, "INDGLyItemEvent1")
        Me.INDGLyItemEvent1.Location = New System.Drawing.Point(0, 108)
        Me.INDGLyItemEvent1.MaxSize = New System.Drawing.Size(280, 36)
        Me.INDGLyItemEvent1.MinSize = New System.Drawing.Size(280, 36)
        Me.INDGLyItemEvent1.Name = "INDGLyItemEvent1"
        Me.INDGLyItemEvent1.Size = New System.Drawing.Size(280, 36)
        Me.INDGLyItemEvent1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDGLyItemEvent1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDGLyItemEvent1.TextSize = New System.Drawing.Size(106, 13)
        Me.INDGLyItemEvent1.TextToControlDistance = 12
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDGSbAddHours
        resources.ApplyResources(Me.LayoutControlItem5, "LayoutControlItem5")
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(183, 36)
        Me.LayoutControlItem5.Name = "INDLyItemSaveHours"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(280, 36)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'INDGLyItemInitialTime
        '
        Me.INDGLyItemInitialTime.Control = Me.INDGPopupTeInitialTime
        resources.ApplyResources(Me.INDGLyItemInitialTime, "INDGLyItemInitialTime")
        Me.INDGLyItemInitialTime.Location = New System.Drawing.Point(0, 0)
        Me.INDGLyItemInitialTime.MaxSize = New System.Drawing.Size(280, 36)
        Me.INDGLyItemInitialTime.MinSize = New System.Drawing.Size(280, 36)
        Me.INDGLyItemInitialTime.Name = "INDGLyItemInitialTime"
        Me.INDGLyItemInitialTime.Size = New System.Drawing.Size(280, 36)
        Me.INDGLyItemInitialTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDGLyItemInitialTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDGLyItemInitialTime.TextSize = New System.Drawing.Size(106, 13)
        Me.INDGLyItemInitialTime.TextToControlDistance = 12
        '
        'INDGLyItemEndingTime
        '
        Me.INDGLyItemEndingTime.Control = Me.INDGPopupTeEndingTime
        resources.ApplyResources(Me.INDGLyItemEndingTime, "INDGLyItemEndingTime")
        Me.INDGLyItemEndingTime.Location = New System.Drawing.Point(0, 36)
        Me.INDGLyItemEndingTime.MaxSize = New System.Drawing.Size(280, 36)
        Me.INDGLyItemEndingTime.MinSize = New System.Drawing.Size(280, 36)
        Me.INDGLyItemEndingTime.Name = "INDGLyItemEndingTime"
        Me.INDGLyItemEndingTime.Size = New System.Drawing.Size(280, 36)
        Me.INDGLyItemEndingTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDGLyItemEndingTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDGLyItemEndingTime.TextSize = New System.Drawing.Size(106, 13)
        Me.INDGLyItemEndingTime.TextToControlDistance = 12
        '
        'RepCheckEditNextDay
        '
        resources.ApplyResources(Me.RepCheckEditNextDay, "RepCheckEditNextDay")
        Me.RepCheckEditNextDay.Name = "RepCheckEditNextDay"
        Me.RepCheckEditNextDay.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDRgEvent
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgEvent, False)
        resources.ApplyResources(Me.INDRgEvent, "INDRgEvent")
        Me.INDRgEvent.Name = "INDRgEvent"
        Me.INDRgEvent.Properties.Appearance.Font = CType(resources.GetObject("INDRgEvent.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDRgEvent.Properties.Appearance.Options.UseFont = True
        Me.INDRgEvent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgEvent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgEvent.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDRgEvent.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDRgEvent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgEvent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgEvent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgEvent.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDRgEvent.Properties.Items"), Object), resources.GetString("INDRgEvent.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDRgEvent.Properties.Items2"), Object), resources.GetString("INDRgEvent.Properties.Items3"))})
        Me.INDRgEvent.StyleController = Me.LayoutControl1
        '
        'INDGleScheduleTemplate
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleScheduleTemplate, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleScheduleTemplate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleScheduleTemplate, False)
        Me.INDGleScheduleTemplate.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleScheduleTemplate, False)
        resources.ApplyResources(Me.INDGleScheduleTemplate, "INDGleScheduleTemplate")
        Me.IndigoTextEdit1.SetMascara(Me.INDGleScheduleTemplate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleScheduleTemplate.Name = "INDGleScheduleTemplate"
        Me.INDGleScheduleTemplate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleScheduleTemplate.Properties.Appearance.Font = CType(resources.GetObject("INDGleScheduleTemplate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleScheduleTemplate.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleScheduleTemplate.Properties.Appearance.Options.UseFont = True
        Me.INDGleScheduleTemplate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleScheduleTemplate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleScheduleTemplate.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGleScheduleTemplate.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGleScheduleTemplate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleScheduleTemplate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleScheduleTemplate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleScheduleTemplate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGleScheduleTemplate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGleScheduleTemplate.Properties.DisplayMember = "Name"
        Me.INDGleScheduleTemplate.Properties.ImmediatePopup = True
        Me.INDGleScheduleTemplate.Properties.NullText = resources.GetString("INDGleScheduleTemplate.Properties.NullText")
        Me.INDGleScheduleTemplate.Properties.PopupView = Me.CustomGridView1
        Me.INDGleScheduleTemplate.Properties.SearchMode = DevExpress.XtraEditors.Repository.GridLookUpSearchMode.None
        Me.INDGleScheduleTemplate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDGleScheduleTemplate.Properties.ValueMember = "Id"
        Me.INDGleScheduleTemplate.StyleController = Me.LayoutControl1
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleScheduleTemplate, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleScheduleTemplate, 0)
        '
        'CustomGridView1
        '
        Me.CustomGridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CustomGridView1.Appearance.FocusedRow.Font = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.CustomGridView1.Appearance.GroupRow.Font = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.GroupRow.Options.UseFont = True
        Me.CustomGridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.CustomGridView1.Appearance.Row.Font = CType(resources.GetObject("CustomGridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.Row.Options.UseFont = True
        Me.CustomGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDTemplateCode, Me.INDTemplateName})
        Me.CustomGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CustomGridView1.Name = "CustomGridView1"
        Me.CustomGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CustomGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.CustomGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.CustomGridView1.OptionsView.ShowAutoFilterRow = True
        Me.CustomGridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CustomGridView1, False)
        '
        'INDTemplateCode
        '
        resources.ApplyResources(Me.INDTemplateCode, "INDTemplateCode")
        Me.INDTemplateCode.FieldName = "Code"
        Me.INDTemplateCode.Name = "INDTemplateCode"
        '
        'INDTemplateName
        '
        resources.ApplyResources(Me.INDTemplateName, "INDTemplateName")
        Me.INDTemplateName.FieldName = "Name"
        Me.INDTemplateName.Name = "INDTemplateName"
        '
        'INDCbeFrequency
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCbeFrequency, False)
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDCbeFrequency, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCbeFrequency, False)
        Me.INDCbeFrequency.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDCbeFrequency, "INDCbeFrequency")
        Me.IndigoTextEdit1.SetMascara(Me.INDCbeFrequency, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDCbeFrequency.Name = "INDCbeFrequency"
        Me.INDCbeFrequency.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDCbeFrequency.Properties.Appearance.Font = CType(resources.GetObject("INDCbeFrequency.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDCbeFrequency.Properties.Appearance.Options.UseBackColor = True
        Me.INDCbeFrequency.Properties.Appearance.Options.UseFont = True
        Me.INDCbeFrequency.Properties.AppearanceDropDown.Font = CType(resources.GetObject("INDCbeFrequency.Properties.AppearanceDropDown.Font"), System.Drawing.Font)
        Me.INDCbeFrequency.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDCbeFrequency.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDCbeFrequency.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDCbeFrequency.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDCbeFrequency.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDCbeFrequency.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDCbeFrequency.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDCbeFrequency.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCbeFrequency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDCbeFrequency.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDCbeFrequency.Properties.ImmediatePopup = True
        Me.INDCbeFrequency.Properties.Items.AddRange(New Object() {resources.GetString("INDCbeFrequency.Properties.Items"), resources.GetString("INDCbeFrequency.Properties.Items1"), resources.GetString("INDCbeFrequency.Properties.Items2"), resources.GetString("INDCbeFrequency.Properties.Items3")})
        Me.INDCbeFrequency.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.INDCbeFrequency.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCbeFrequency, 0)
        '
        'INDDeEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeEndDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeEndDate, False)
        resources.ApplyResources(Me.INDDeEndDate, "INDDeEndDate")
        Me.INDDeEndDate.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDDeEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeEndDate.Name = "INDDeEndDate"
        Me.INDDeEndDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeEndDate.Properties.Appearance.Font = CType(resources.GetObject("INDDeEndDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDDeEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeEndDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDeEndDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDeEndDate.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDDeEndDate.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDDeEndDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDeEndDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDeEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDDeEndDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDDeEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDDeEndDate.Properties.Mask.EditMask = resources.GetString("INDDeEndDate.Properties.Mask.EditMask")
        Me.INDDeEndDate.Properties.Mask.MaskType = CType(resources.GetObject("INDDeEndDate.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDDeEndDate.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDDeEndDate.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDDeEndDate.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeEndDate, 0)
        '
        'INDDeStartDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeStartDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeStartDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeStartDate, False)
        resources.ApplyResources(Me.INDDeStartDate, "INDDeStartDate")
        Me.INDDeStartDate.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDDeStartDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeStartDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeStartDate.Name = "INDDeStartDate"
        Me.INDDeStartDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeStartDate.Properties.Appearance.Font = CType(resources.GetObject("INDDeStartDate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDDeStartDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeStartDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeStartDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDeStartDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDeStartDate.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDDeStartDate.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDDeStartDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDeStartDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDeStartDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDDeStartDate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDDeStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDDeStartDate.Properties.Mask.EditMask = resources.GetString("INDDeStartDate.Properties.Mask.EditMask")
        Me.INDDeStartDate.Properties.Mask.MaskType = CType(resources.GetObject("INDDeStartDate.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDDeStartDate.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDDeStartDate.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDDeStartDate.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeStartDate, 0)
        '
        'INDChkWeekDays
        '
        Me.INDChkWeekDays.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDChkWeekDays.Appearance.Font = CType(resources.GetObject("INDChkWeekDays.Appearance.Font"), System.Drawing.Font)
        Me.INDChkWeekDays.Appearance.Options.UseBackColor = True
        Me.INDChkWeekDays.Appearance.Options.UseFont = True
        Me.INDChkWeekDays.FirstDayOfWeek = DevExpress.XtraScheduler.FirstDayOfWeek.Sunday
        resources.ApplyResources(Me.INDChkWeekDays, "INDChkWeekDays")
        Me.INDChkWeekDays.Name = "INDChkWeekDays"
        '
        'INDTxtRepeatDays
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtRepeatDays, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtRepeatDays, False)
        resources.ApplyResources(Me.INDTxtRepeatDays, "INDTxtRepeatDays")
        Me.INDTxtRepeatDays.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtRepeatDays, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtRepeatDays.Name = "INDTxtRepeatDays"
        Me.INDTxtRepeatDays.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtRepeatDays.Properties.Appearance.Font = CType(resources.GetObject("INDTxtRepeatDays.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtRepeatDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtRepeatDays.Properties.Appearance.Options.UseFont = True
        Me.INDTxtRepeatDays.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtRepeatDays.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtRepeatDays.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtRepeatDays.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtRepeatDays.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtRepeatDays.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtRepeatDays.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtRepeatDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDTxtRepeatDays.Properties.Mask.EditMask = resources.GetString("INDTxtRepeatDays.Properties.Mask.EditMask")
        Me.INDTxtRepeatDays.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtRepeatDays.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtRepeatDays.Properties.MaxValue = New Decimal(New Integer() {30, 0, 0, 0})
        Me.INDTxtRepeatDays.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.INDTxtRepeatDays.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtRepeatDays, 0)
        '
        'INDCLBCEmployee
        '
        Me.INDCLBCEmployee.Appearance.Font = CType(resources.GetObject("INDCLBCEmployee.Appearance.Font"), System.Drawing.Font)
        Me.INDCLBCEmployee.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDCLBCEmployee.Appearance.Options.UseFont = True
        Me.INDCLBCEmployee.Appearance.Options.UseForeColor = True
        Me.INDCLBCEmployee.CheckOnClick = True
        resources.ApplyResources(Me.INDCLBCEmployee, "INDCLBCEmployee")
        Me.INDCLBCEmployee.Name = "INDCLBCEmployee"
        Me.INDCLBCEmployee.StyleController = Me.LayoutControl1
        '
        'Days
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.Days, True)
        Me.Days.Appearance.Font = CType(resources.GetObject("Days.Appearance.Font"), System.Drawing.Font)
        Me.Days.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.Days, False)
        resources.ApplyResources(Me.Days, "Days")
        Me.Days.Name = "Days"
        Me.Days.StyleController = Me.LayoutControl1
        '
        'INDRgHoliday
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDRgHoliday, False)
        Me.INDRgHoliday.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDRgHoliday, "INDRgHoliday")
        Me.INDRgHoliday.Name = "INDRgHoliday"
        Me.INDRgHoliday.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDRgHoliday.Properties.Appearance.Font = CType(resources.GetObject("INDRgHoliday.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDRgHoliday.Properties.Appearance.Options.UseBackColor = True
        Me.INDRgHoliday.Properties.Appearance.Options.UseFont = True
        Me.INDRgHoliday.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDRgHoliday.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDRgHoliday.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDRgHoliday.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDRgHoliday.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDRgHoliday.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDRgHoliday.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDRgHoliday.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDRgHoliday.Properties.Items"), Object), resources.GetString("INDRgHoliday.Properties.Items1")), New DevExpress.XtraEditors.Controls.RadioGroupItem(CType(resources.GetObject("INDRgHoliday.Properties.Items2"), Object), resources.GetString("INDRgHoliday.Properties.Items3"))})
        Me.INDRgHoliday.StyleController = Me.LayoutControl1
        '
        'INDLyGrFrequency
        '
        Me.INDLyGrFrequency.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrFrequency.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLyGrFrequency.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrFrequency.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrFrequency.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrFrequency.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrFrequency.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrFrequency.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrFrequency.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrFrequency.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrFrequency.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLyGrFrequency.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrFrequency.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrFrequency.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrFrequency.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrFrequency.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrFrequency.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLyGrFrequency.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrFrequency.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrFrequency.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrFrequency.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrFrequency, False)
        resources.ApplyResources(Me.INDLyGrFrequency, "INDLyGrFrequency")
        Me.INDLyGrFrequency.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyRepeatDays1, Me.INDLyHoliday, Me.INDLyRepeatDays, Me.INDLyWeekDays, Me.INDLyFrequency, Me.INDLyMonthDays})
        Me.INDLyGrFrequency.Location = New System.Drawing.Point(0, 36)
        Me.INDLyGrFrequency.Name = "INDLyGrFrequency"
        Me.INDLyGrFrequency.Size = New System.Drawing.Size(678, 396)
        '
        'INDLyRepeatDays1
        '
        Me.INDLyRepeatDays1.Control = Me.Days
        resources.ApplyResources(Me.INDLyRepeatDays1, "INDLyRepeatDays1")
        Me.INDLyRepeatDays1.Location = New System.Drawing.Point(280, 36)
        Me.INDLyRepeatDays1.MinSize = New System.Drawing.Size(32, 25)
        Me.INDLyRepeatDays1.Name = "INDLyRepeatDays1"
        Me.INDLyRepeatDays1.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2)
        Me.INDLyRepeatDays1.Size = New System.Drawing.Size(374, 36)
        Me.INDLyRepeatDays1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyRepeatDays1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyRepeatDays1.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyRepeatDays1.TextToControlDistance = 0
        Me.INDLyRepeatDays1.TextVisible = False
        '
        'INDLyHoliday
        '
        Me.INDLyHoliday.Control = Me.INDRgHoliday
        resources.ApplyResources(Me.INDLyHoliday, "INDLyHoliday")
        Me.INDLyHoliday.Location = New System.Drawing.Point(0, 307)
        Me.INDLyHoliday.MaxSize = New System.Drawing.Size(340, 36)
        Me.INDLyHoliday.MinSize = New System.Drawing.Size(340, 36)
        Me.INDLyHoliday.Name = "INDLyHoliday"
        Me.INDLyHoliday.Size = New System.Drawing.Size(654, 36)
        Me.INDLyHoliday.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyHoliday.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyHoliday.TextSize = New System.Drawing.Size(220, 21)
        Me.INDLyHoliday.TextToControlDistance = 3
        '
        'INDLyRepeatDays
        '
        Me.INDLyRepeatDays.Control = Me.INDTxtRepeatDays
        resources.ApplyResources(Me.INDLyRepeatDays, "INDLyRepeatDays")
        Me.INDLyRepeatDays.Location = New System.Drawing.Point(0, 36)
        Me.INDLyRepeatDays.MaxSize = New System.Drawing.Size(280, 36)
        Me.INDLyRepeatDays.MinSize = New System.Drawing.Size(280, 36)
        Me.INDLyRepeatDays.Name = "INDLyRepeatDays"
        Me.INDLyRepeatDays.Size = New System.Drawing.Size(280, 36)
        Me.INDLyRepeatDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyRepeatDays.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyRepeatDays.TextSize = New System.Drawing.Size(220, 21)
        Me.INDLyRepeatDays.TextToControlDistance = 5
        '
        'INDLyWeekDays
        '
        Me.INDLyWeekDays.Control = Me.INDChkWeekDays
        resources.ApplyResources(Me.INDLyWeekDays, "INDLyWeekDays")
        Me.INDLyWeekDays.Location = New System.Drawing.Point(0, 72)
        Me.INDLyWeekDays.MaxSize = New System.Drawing.Size(0, 60)
        Me.INDLyWeekDays.MinSize = New System.Drawing.Size(385, 60)
        Me.INDLyWeekDays.Name = "INDLyWeekDays"
        Me.INDLyWeekDays.Size = New System.Drawing.Size(654, 60)
        Me.INDLyWeekDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyWeekDays.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyWeekDays.TextVisible = False
        '
        'INDLyFrequency
        '
        Me.INDLyFrequency.Control = Me.INDCbeFrequency
        resources.ApplyResources(Me.INDLyFrequency, "INDLyFrequency")
        Me.INDLyFrequency.Location = New System.Drawing.Point(0, 0)
        Me.INDLyFrequency.MaxSize = New System.Drawing.Size(640, 36)
        Me.INDLyFrequency.MinSize = New System.Drawing.Size(640, 36)
        Me.INDLyFrequency.Name = "INDLyFrequency"
        Me.INDLyFrequency.Size = New System.Drawing.Size(654, 36)
        Me.INDLyFrequency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyFrequency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyFrequency.TextSize = New System.Drawing.Size(165, 21)
        Me.INDLyFrequency.TextToControlDistance = 12
        '
        'INDLyMonthDays
        '
        Me.INDLyMonthDays.Control = Me.CtrCalendarMini
        resources.ApplyResources(Me.INDLyMonthDays, "INDLyMonthDays")
        Me.INDLyMonthDays.Location = New System.Drawing.Point(0, 132)
        Me.INDLyMonthDays.MaxSize = New System.Drawing.Size(654, 175)
        Me.INDLyMonthDays.MinSize = New System.Drawing.Size(654, 175)
        Me.INDLyMonthDays.Name = "INDLyMonthDays"
        Me.INDLyMonthDays.Size = New System.Drawing.Size(654, 175)
        Me.INDLyMonthDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyMonthDays.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyMonthDays.TextSize = New System.Drawing.Size(165, 21)
        Me.INDLyMonthDays.TextToControlDistance = 12
        '
        'INDLyGrRepeatInterval
        '
        Me.INDLyGrRepeatInterval.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrRepeatInterval.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLyGrRepeatInterval.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrRepeatInterval.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrRepeatInterval.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrRepeatInterval.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrRepeatInterval.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrRepeatInterval.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrRepeatInterval.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrRepeatInterval.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrRepeatInterval.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLyGrRepeatInterval.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrRepeatInterval.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrRepeatInterval.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrRepeatInterval.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrRepeatInterval.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrRepeatInterval.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLyGrRepeatInterval.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrRepeatInterval.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrRepeatInterval.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrRepeatInterval.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrRepeatInterval, False)
        resources.ApplyResources(Me.INDLyGrRepeatInterval, "INDLyGrRepeatInterval")
        Me.INDLyGrRepeatInterval.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyStarDate, Me.INDLyEndDate})
        Me.INDLyGrRepeatInterval.Location = New System.Drawing.Point(0, 432)
        Me.INDLyGrRepeatInterval.Name = "INDLyGrRepeatInterval"
        Me.INDLyGrRepeatInterval.Size = New System.Drawing.Size(678, 89)
        '
        'INDLyStarDate
        '
        Me.INDLyStarDate.Control = Me.INDDeStartDate
        resources.ApplyResources(Me.INDLyStarDate, "INDLyStarDate")
        Me.INDLyStarDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLyStarDate.MaxSize = New System.Drawing.Size(370, 36)
        Me.INDLyStarDate.MinSize = New System.Drawing.Size(370, 36)
        Me.INDLyStarDate.Name = "INDLyStarDate"
        Me.INDLyStarDate.Size = New System.Drawing.Size(370, 36)
        Me.INDLyStarDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyStarDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyStarDate.TextSize = New System.Drawing.Size(170, 21)
        Me.INDLyStarDate.TextToControlDistance = 5
        '
        'INDLyEndDate
        '
        Me.INDLyEndDate.Control = Me.INDDeEndDate
        resources.ApplyResources(Me.INDLyEndDate, "INDLyEndDate")
        Me.INDLyEndDate.Location = New System.Drawing.Point(370, 0)
        Me.INDLyEndDate.MaxSize = New System.Drawing.Size(275, 36)
        Me.INDLyEndDate.MinSize = New System.Drawing.Size(275, 36)
        Me.INDLyEndDate.Name = "INDLyEndDate"
        Me.INDLyEndDate.Size = New System.Drawing.Size(284, 36)
        Me.INDLyEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyEndDate.TextSize = New System.Drawing.Size(66, 21)
        Me.INDLyEndDate.TextToControlDistance = 5
        '
        'INDLyControlScheduleTemplate
        '
        Me.INDLyControlScheduleTemplate.Control = Me.INDGleScheduleTemplate
        resources.ApplyResources(Me.INDLyControlScheduleTemplate, "INDLyControlScheduleTemplate")
        Me.INDLyControlScheduleTemplate.Location = New System.Drawing.Point(0, 0)
        Me.INDLyControlScheduleTemplate.MaxSize = New System.Drawing.Size(650, 36)
        Me.INDLyControlScheduleTemplate.MinSize = New System.Drawing.Size(650, 36)
        Me.INDLyControlScheduleTemplate.Name = "INDLyControlScheduleTemplate"
        Me.INDLyControlScheduleTemplate.Size = New System.Drawing.Size(678, 36)
        Me.INDLyControlScheduleTemplate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyControlScheduleTemplate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyControlScheduleTemplate.TextSize = New System.Drawing.Size(175, 21)
        Me.INDLyControlScheduleTemplate.TextToControlDistance = 12
        '
        'INDLyGrEvent
        '
        Me.INDLyGrEvent.AppearanceGroup.Font = CType(resources.GetObject("INDLyGrEvent.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLyGrEvent.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrEvent.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyGrEvent.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyGrEvent.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrEvent.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLyGrEvent.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLyGrEvent.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrEvent.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLyGrEvent.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLyGrEvent.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrEvent.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLyGrEvent.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLyGrEvent.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrEvent.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLyGrEvent.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLyGrEvent.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrEvent.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLyGrEvent.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLyGrEvent.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrEvent, False)
        resources.ApplyResources(Me.INDLyGrEvent, "INDLyGrEvent")
        Me.INDLyGrEvent.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemEvent, Me.INDLyItemDetailHours, Me.INDLyItemAddHours})
        Me.INDLyGrEvent.Location = New System.Drawing.Point(0, 521)
        Me.INDLyGrEvent.Name = "INDLyGrEvent"
        Me.INDLyGrEvent.Size = New System.Drawing.Size(678, 239)
        Me.INDLyGrEvent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyItemEvent
        '
        Me.INDLyItemEvent.Control = Me.INDRgEvent
        resources.ApplyResources(Me.INDLyItemEvent, "INDLyItemEvent")
        Me.INDLyItemEvent.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemEvent.MaxSize = New System.Drawing.Size(350, 36)
        Me.INDLyItemEvent.MinSize = New System.Drawing.Size(350, 36)
        Me.INDLyItemEvent.Name = "INDLyItemEvent"
        Me.INDLyItemEvent.Size = New System.Drawing.Size(350, 36)
        Me.INDLyItemEvent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemEvent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemEvent.TextSize = New System.Drawing.Size(170, 21)
        Me.INDLyItemEvent.TextToControlDistance = 5
        '
        'INDLyItemDetailHours
        '
        Me.INDLyItemDetailHours.Control = Me.INDGcDetailHours
        resources.ApplyResources(Me.INDLyItemDetailHours, "INDLyItemDetailHours")
        Me.INDLyItemDetailHours.Location = New System.Drawing.Point(0, 36)
        Me.INDLyItemDetailHours.MaxSize = New System.Drawing.Size(654, 150)
        Me.INDLyItemDetailHours.MinSize = New System.Drawing.Size(654, 150)
        Me.INDLyItemDetailHours.Name = "INDLyItemDetailHours"
        Me.INDLyItemDetailHours.Size = New System.Drawing.Size(654, 150)
        Me.INDLyItemDetailHours.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailHours.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemDetailHours.TextVisible = False
        Me.INDLyItemDetailHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyItemAddHours
        '
        Me.INDLyItemAddHours.Control = Me.INDDDBAddHours
        resources.ApplyResources(Me.INDLyItemAddHours, "INDLyItemAddHours")
        Me.INDLyItemAddHours.Location = New System.Drawing.Point(350, 0)
        Me.INDLyItemAddHours.MinSize = New System.Drawing.Size(117, 26)
        Me.INDLyItemAddHours.Name = "INDLyItemAddHours"
        Me.INDLyItemAddHours.Size = New System.Drawing.Size(304, 36)
        Me.INDLyItemAddHours.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemAddHours.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemAddHours.TextVisible = False
        Me.INDLyItemAddHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyItemEmployee
        '
        Me.INDLyItemEmployee.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemEmployee.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLyItemEmployee.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDLyItemEmployee.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemEmployee.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemEmployee.Control = Me.INDCLBCEmployee
        resources.ApplyResources(Me.INDLyItemEmployee, "INDLyItemEmployee")
        Me.INDLyItemEmployee.Location = New System.Drawing.Point(0, 76)
        Me.INDLyItemEmployee.MaxSize = New System.Drawing.Size(265, 0)
        Me.INDLyItemEmployee.MinSize = New System.Drawing.Size(265, 37)
        Me.INDLyItemEmployee.Name = "INDLyItemEmployee"
        Me.INDLyItemEmployee.Size = New System.Drawing.Size(265, 294)
        Me.INDLyItemEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemEmployee.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyItemEmployee.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemEmployee.TextVisible = False
        Me.INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLyItemEmployeeSelected
        '
        Me.INDLyItemEmployeeSelected.Control = Me.INDLcEmployee
        resources.ApplyResources(Me.INDLyItemEmployeeSelected, "INDLyItemEmployeeSelected")
        Me.INDLyItemEmployeeSelected.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemEmployeeSelected.MaxSize = New System.Drawing.Size(967, 38)
        Me.INDLyItemEmployeeSelected.MinSize = New System.Drawing.Size(967, 38)
        Me.INDLyItemEmployeeSelected.Name = "INDLyItemEmployeeSelected"
        Me.INDLyItemEmployeeSelected.Size = New System.Drawing.Size(967, 38)
        Me.INDLyItemEmployeeSelected.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemEmployeeSelected.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemEmployeeSelected.TextVisible = False
        '
        'INDLyItemScheduleMatch
        '
        Me.INDLyItemScheduleMatch.Control = Me.INDLcScheduleMates
        resources.ApplyResources(Me.INDLyItemScheduleMatch, "INDLyItemScheduleMatch")
        Me.INDLyItemScheduleMatch.Location = New System.Drawing.Point(0, 38)
        Me.INDLyItemScheduleMatch.MaxSize = New System.Drawing.Size(265, 38)
        Me.INDLyItemScheduleMatch.MinSize = New System.Drawing.Size(265, 38)
        Me.INDLyItemScheduleMatch.Name = "INDLyItemScheduleMatch"
        Me.INDLyItemScheduleMatch.Size = New System.Drawing.Size(265, 38)
        Me.INDLyItemScheduleMatch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemScheduleMatch.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemScheduleMatch.TextVisible = False
        '
        'INDColTemplateCode
        '
        resources.ApplyResources(Me.INDColTemplateCode, "INDColTemplateCode")
        Me.INDColTemplateCode.FieldName = "Code"
        Me.INDColTemplateCode.Name = "INDColTemplateCode"
        '
        'INDColTemplateDescription
        '
        resources.ApplyResources(Me.INDColTemplateDescription, "INDColTemplateDescription")
        Me.INDColTemplateDescription.FieldName = "Name"
        Me.INDColTemplateDescription.Name = "INDColTemplateDescription"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDDeStartDate
        resources.ApplyResources(Me.LayoutControlItem3, "LayoutControlItem3")
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem3.Name = "LayoutControlItem2"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(370, 25)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(189, 21)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmTemplate
        '
        Me.Appearance.Options.UseFont = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDPccAddHours)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.Icon = CType(resources.GetObject("FrmTemplate.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmTemplate"
        Me.Opacity = 1.0R
        Me.Tag = "554"
        Me.ViewModeEditHold = True
        Me.Controls.SetChildIndex(Me.BarDockControl1, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl2, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl4, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl3, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDPccAddHours, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDScheduleControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrTemplateToUse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDPccAddHours, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccAddHours.ResumeLayout(False)
        CType(Me.INDLyControlPopUpNewHours, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyControlPopUpNewHours.ResumeLayout(False)
        CType(Me.INDPopupRgInitialNextDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupTeEndingTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupTeInitialTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupRgNextDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemNextDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemSaveHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInitialNextDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemInitialTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemEndingTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDetailHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvDetailHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepTimeEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepRgEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepPopupContainerEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccEditHours, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccEditHours.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDGPopupTeEndingTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGPopupTeInitialTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGPopupRgEvent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGPopupRgNextDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGLyItemNextDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGLyItemEvent1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGLyItemInitialTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGLyItemEndingTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCheckEditNextDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRgEvent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleScheduleTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeFrequency.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkWeekDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtRepeatDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCLBCEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRgHoliday.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyRepeatDays1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyRepeatDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyWeekDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyMonthDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrRepeatInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyStarDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyControlScheduleTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemEvent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemAddHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemEmployeeSelected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemScheduleMatch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents SchedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDChkWeekDays As DevExpress.XtraScheduler.UI.WeekDaysCheckEdit
    Friend WithEvents INDTxtRepeatDays As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDCLBCEmployee As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents Days As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDRgHoliday As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDScheduleControl As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGrTemplateToUse As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyGrLocation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyGrFrequency As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyRepeatDays1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyHoliday As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyRepeatDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyWeekDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGrRepeatInterval As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleFunctionalUnit As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLyItemFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDeEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDeStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDColFUName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBOName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColComName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLyStarDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCbeFrequency As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents INDLyFrequency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleScheduleTemplate As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents CustomGridView1 As Presentation.Controls.CustomGridView
    Friend WithEvents INDLyControlScheduleTemplate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColTemplateCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTemplateDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRgEvent As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDLyGrEvent As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemEvent As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcDetailHours As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvDetailHours As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLyItemDetailHours As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDColInitialTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEndingTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepRgEvent As DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup
    Friend WithEvents INDColNextDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepCheckEditNextDay As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepTimeEdit As DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit
    Friend WithEvents INDLcEmployee As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLyItemEmployeeSelected As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcScheduleMates As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDTemplateCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTemplateName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLyItemScheduleMatch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDDBAddHours As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDLyItemAddHours As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDPccAddHours As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDLyControlPopUpNewHours As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSbAddHours As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDPopupRgNextDay As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemNextDay As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemSaveHours As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupTeEndingTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDPopupTeInitialTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDLyItemInitialTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemEndingTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColEditing As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepPopupContainerEdit As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColEvent As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPccEditHours As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGPopupTeEndingTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDGPopupTeInitialTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDGSbAddHours As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDGPopupRgEvent As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDGPopupRgNextDay As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGLyItemNextDay As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGLyItemEvent1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGLyItemInitialTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGLyItemEndingTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrCalendarMini As Presentation.Controls.CtrCalendarMini
    Friend WithEvents INDLyMonthDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupRgInitialNextDay As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDlyItemInitialNextDay As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
