<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmHoliday
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmHoliday))
        Dim TimeRuler1 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler2 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDScHoliday = New DevExpress.XtraScheduler.SchedulerControl()
        Me.INDScStorage = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
        Me.INDDateNavigator = New DevExpress.XtraScheduler.DateNavigator()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemDateNavigator = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemScHoliday = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDScHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDScStorage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDateNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDateNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemScHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomizationMenu = False
        Me.LayoutControl1.Controls.Add(Me.INDScHoliday)
        Me.LayoutControl1.Controls.Add(Me.INDDateNavigator)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, False)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'INDScHoliday
        '
        Me.INDScHoliday.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month
        Me.INDScHoliday.Appearance.HeaderCaption.Font = CType(resources.GetObject("INDScHoliday.Appearance.HeaderCaption.Font"), System.Drawing.Font)
        Me.INDScHoliday.Appearance.HeaderCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDScHoliday, "INDScHoliday")
        Me.INDScHoliday.ForeColor = System.Drawing.Color.White
        Me.INDScHoliday.Name = "INDScHoliday"
        Me.INDScHoliday.Start = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.INDScHoliday.Storage = Me.INDScStorage
        Me.INDScHoliday.Views.DayView.Enabled = False
        TimeRuler1.TimeZoneId = "SA Pacific Standard Time"
        TimeRuler1.UseClientTimeZone = False
        Me.INDScHoliday.Views.DayView.TimeRulers.Add(TimeRuler1)
        Me.INDScHoliday.Views.GanttView.Enabled = False
        Me.INDScHoliday.Views.MonthView.Appearance.Appointment.BackColor = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.Appointment.BackColor"), System.Drawing.Color)
        Me.INDScHoliday.Views.MonthView.Appearance.Appointment.Font = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.Appointment.Font"), System.Drawing.Font)
        Me.INDScHoliday.Views.MonthView.Appearance.Appointment.ForeColor = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.Appointment.ForeColor"), System.Drawing.Color)
        Me.INDScHoliday.Views.MonthView.Appearance.Appointment.Options.UseBackColor = True
        Me.INDScHoliday.Views.MonthView.Appearance.Appointment.Options.UseFont = True
        Me.INDScHoliday.Views.MonthView.Appearance.Appointment.Options.UseForeColor = True
        Me.INDScHoliday.Views.MonthView.Appearance.Appointment.Options.UseTextOptions = True
        Me.INDScHoliday.Views.MonthView.Appearance.CellHeaderCaption.Font = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.CellHeaderCaption.Font"), System.Drawing.Font)
        Me.INDScHoliday.Views.MonthView.Appearance.CellHeaderCaption.ForeColor = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.CellHeaderCaption.ForeColor"), System.Drawing.Color)
        Me.INDScHoliday.Views.MonthView.Appearance.CellHeaderCaption.Options.UseFont = True
        Me.INDScHoliday.Views.MonthView.Appearance.CellHeaderCaption.Options.UseForeColor = True
        Me.INDScHoliday.Views.MonthView.Appearance.CellHeaderCaptionLine.Font = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.CellHeaderCaptionLine.Font"), System.Drawing.Font)
        Me.INDScHoliday.Views.MonthView.Appearance.CellHeaderCaptionLine.ForeColor = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.CellHeaderCaptionLine.ForeColor"), System.Drawing.Color)
        Me.INDScHoliday.Views.MonthView.Appearance.CellHeaderCaptionLine.Options.UseFont = True
        Me.INDScHoliday.Views.MonthView.Appearance.CellHeaderCaptionLine.Options.UseForeColor = True
        Me.INDScHoliday.Views.MonthView.Appearance.HeaderCaption.Font = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.HeaderCaption.Font"), System.Drawing.Font)
        Me.INDScHoliday.Views.MonthView.Appearance.HeaderCaption.ForeColor = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.HeaderCaption.ForeColor"), System.Drawing.Color)
        Me.INDScHoliday.Views.MonthView.Appearance.HeaderCaption.Options.UseFont = True
        Me.INDScHoliday.Views.MonthView.Appearance.HeaderCaption.Options.UseForeColor = True
        Me.INDScHoliday.Views.MonthView.Appearance.HeaderCaptionLine.Font = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.HeaderCaptionLine.Font"), System.Drawing.Font)
        Me.INDScHoliday.Views.MonthView.Appearance.HeaderCaptionLine.ForeColor = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.HeaderCaptionLine.ForeColor"), System.Drawing.Color)
        Me.INDScHoliday.Views.MonthView.Appearance.HeaderCaptionLine.Options.UseFont = True
        Me.INDScHoliday.Views.MonthView.Appearance.HeaderCaptionLine.Options.UseForeColor = True
        Me.INDScHoliday.Views.MonthView.Appearance.Selection.Font = CType(resources.GetObject("INDScHoliday.Views.MonthView.Appearance.Selection.Font"), System.Drawing.Font)
        Me.INDScHoliday.Views.MonthView.Appearance.Selection.Options.UseFont = True
        Me.INDScHoliday.Views.MonthView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.INDScHoliday.Views.MonthView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.INDScHoliday.Views.MonthView.WeekCount = 4
        Me.INDScHoliday.Views.TimelineView.Enabled = False
        Me.INDScHoliday.Views.WeekView.Enabled = False
        Me.INDScHoliday.Views.WorkWeekView.Enabled = False
        TimeRuler2.TimeZoneId = "SA Pacific Standard Time"
        TimeRuler2.UseClientTimeZone = False
        Me.INDScHoliday.Views.WorkWeekView.TimeRulers.Add(TimeRuler2)
        '
        'INDScStorage
        '
        Me.INDScStorage.Appointments.CustomFieldMappings.Add(New DevExpress.XtraScheduler.AppointmentCustomFieldMapping("Holidate", "", DevExpress.XtraScheduler.FieldValueType.[Integer]))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel("Ninguno", "&Ninguno"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(190, Byte), Integer)), "Importante", "&Importante"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(168, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer)), "Trabajo", "&Trabajo"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(156, Byte), Integer)), "Particular", "Partic&ular"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(199, Byte), Integer)), "Vacaciones", "&Vacaciones"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(147, Byte), Integer)), "Requiere asistencia", "&Requiere asistencia"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer)), "Requiere viajar", "Requiere via&jar"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(152, Byte), Integer)), "Requiere preparación", "Requiere &preparación"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(233, Byte), Integer)), "Cumpleaños", "&Cumpleaños"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer)), "Aniversario", "&Aniversario"))
        Me.INDScStorage.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(165, Byte), Integer)), "Llamada telefónica", "Llamada telefón&ica"))
        '
        'INDDateNavigator
        '
        Me.INDDateNavigator.AppearanceCalendar.Font = CType(resources.GetObject("INDDateNavigator.AppearanceCalendar.Font"), System.Drawing.Font)
        Me.INDDateNavigator.AppearanceCalendar.Options.UseFont = True
        Me.INDDateNavigator.AppearanceHeader.Font = CType(resources.GetObject("INDDateNavigator.AppearanceHeader.Font"), System.Drawing.Font)
        Me.INDDateNavigator.AppearanceHeader.Options.UseFont = True
        Me.INDDateNavigator.AppearanceWeekNumber.Font = CType(resources.GetObject("INDDateNavigator.AppearanceWeekNumber.Font"), System.Drawing.Font)
        Me.INDDateNavigator.AppearanceWeekNumber.Options.UseFont = True
        resources.ApplyResources(Me.INDDateNavigator, "INDDateNavigator")
        Me.INDDateNavigator.HotDate = Nothing
        Me.INDDateNavigator.Name = "INDDateNavigator"
        Me.INDDateNavigator.SchedulerControl = Me.INDScHoliday
        '
        'LayoutControlGroup1
        '
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemDateNavigator, Me.INDLyItemScHoliday})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(753, 387)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLyItemDateNavigator
        '
        Me.INDLyItemDateNavigator.Control = Me.INDDateNavigator
        resources.ApplyResources(Me.INDLyItemDateNavigator, "INDLyItemDateNavigator")
        Me.INDLyItemDateNavigator.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemDateNavigator.MaxSize = New System.Drawing.Size(200, 0)
        Me.INDLyItemDateNavigator.MinSize = New System.Drawing.Size(200, 320)
        Me.INDLyItemDateNavigator.Name = "INDLyItemDateNavigator"
        Me.INDLyItemDateNavigator.Size = New System.Drawing.Size(200, 367)
        Me.INDLyItemDateNavigator.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDateNavigator.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemDateNavigator.TextToControlDistance = 0
        Me.INDLyItemDateNavigator.TextVisible = False
        '
        'INDLyItemScHoliday
        '
        Me.INDLyItemScHoliday.Control = Me.INDScHoliday
        resources.ApplyResources(Me.INDLyItemScHoliday, "INDLyItemScHoliday")
        Me.INDLyItemScHoliday.Location = New System.Drawing.Point(200, 0)
        Me.INDLyItemScHoliday.Name = "INDLyItemScHoliday"
        Me.INDLyItemScHoliday.Size = New System.Drawing.Size(533, 367)
        Me.INDLyItemScHoliday.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemScHoliday.TextToControlDistance = 0
        Me.INDLyItemScHoliday.TextVisible = False
        '
        'FrmHoliday
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmHoliday"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Tag = "553"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDScHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDScStorage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDateNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDateNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemScHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDScHoliday As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents INDScStorage As DevExpress.XtraScheduler.SchedulerStorage
    Friend WithEvents INDDateNavigator As DevExpress.XtraScheduler.DateNavigator
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemDateNavigator As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemScHoliday As DevExpress.XtraLayout.LayoutControlItem
End Class
