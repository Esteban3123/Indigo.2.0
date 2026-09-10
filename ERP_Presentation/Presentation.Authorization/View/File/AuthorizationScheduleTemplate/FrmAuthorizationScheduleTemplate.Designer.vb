Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAuthorizationScheduleTemplate
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
        Dim TimeRuler1 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler2 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeRuler3 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAuthorizationScheduleTemplate))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDscScheduler = New DevExpress.XtraScheduler.SchedulerControl()
        Me.SchedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
        Me.INDsleSchedule = New DevExpress.XtraEditors.ImageComboBoxEdit()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleUsers = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.viewUserSearch = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcUsers = New DevExpress.XtraGrid.GridControl()
        Me.viewUsersGrid = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbtnAddUser = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSchedule = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygScheduler = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemScheduler = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygAuthorization = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemUsers = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddUser = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDscScheduler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleSchedule.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleUsers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewUserSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewUsersGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygScheduler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemScheduler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAuthorization, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1186, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1186, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1186, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 598)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.AllowCustomization = False
        Me.INDlyRoot.Controls.Add(Me.INDscScheduler)
        Me.INDlyRoot.Controls.Add(Me.INDsleSchedule)
        Me.INDlyRoot.Controls.Add(Me.INDtxtName)
        Me.INDlyRoot.Controls.Add(Me.INDbtnCode)
        Me.INDlyRoot.Controls.Add(Me.INDsleUsers)
        Me.INDlyRoot.Controls.Add(Me.INDgcUsers)
        Me.INDlyRoot.Controls.Add(Me.INDbtnAddUser)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, False)
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(982, 598)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDscScheduler
        '
        Me.INDscScheduler.DataStorage = Me.SchedulerStorage1
        Me.INDscScheduler.DateNavigationBar.Visible = False
        Me.INDscScheduler.Enabled = False
        Me.INDscScheduler.Location = New System.Drawing.Point(438, 59)
        Me.INDscScheduler.Name = "INDscScheduler"
        Me.INDscScheduler.OptionsCustomization.AllowAppointmentDrag = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDscScheduler.OptionsCustomization.AllowAppointmentResize = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDscScheduler.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.INDscScheduler.OptionsPrint.PrintStyle = DevExpress.XtraScheduler.Printing.SchedulerPrintStyleKind.Daily
        Me.INDscScheduler.OptionsRangeControl.AllowChangeActiveView = False
        Me.INDscScheduler.OptionsView.NavigationButtons.Visibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never
        Me.INDscScheduler.Size = New System.Drawing.Size(496, 498)
        Me.INDscScheduler.Start = New Date(2020, 3, 6, 0, 0, 0, 0)
        Me.INDscScheduler.TabIndex = 3
        Me.INDscScheduler.Text = "SchedulerControl1"
        Me.INDscScheduler.Views.DayView.AllDayAreaScrollBarVisible = True
        Me.INDscScheduler.Views.DayView.DayCount = 2
        Me.INDscScheduler.Views.DayView.DisplayName = "Day"
        Me.INDscScheduler.Views.DayView.TimeRulers.Add(TimeRuler1)
        Me.INDscScheduler.Views.FullWeekView.TimeRulers.Add(TimeRuler2)
        Me.INDscScheduler.Views.WorkWeekView.TimeRulers.Add(TimeRuler3)
        '
        'SchedulerStorage1
        '
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.SystemColors.Window, "None", "&None")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(190, Byte), Integer)), "Important", "&Important")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(168, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(255, Byte), Integer)), "Business", "&Business")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(156, Byte), Integer)), "Personal", "&Personal")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(199, Byte), Integer)), "Vacation", "&Vacation")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(147, Byte), Integer)), "Must Attend", "Must &Attend")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(199, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(255, Byte), Integer)), "Travel Required", "&Travel Required")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(152, Byte), Integer)), "Needs Preparation", "&Needs Preparation")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(233, Byte), Integer)), "Birthday", "&Birthday")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(223, Byte), Integer)), "Anniversary", "&Anniversary")
        Me.SchedulerStorage1.Appointments.Labels.Add(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(165, Byte), Integer)), "Telefono", "Phone &Call")
        '
        'INDsleSchedule
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleSchedule, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleSchedule, False)
        Me.INDsleSchedule.EnterMoveNextControl = True
        Me.INDsleSchedule.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleSchedule, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleSchedule.Name = "INDsleSchedule"
        Me.INDsleSchedule.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleSchedule.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSchedule.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleSchedule.Properties.Appearance.Options.UseFont = True
        Me.INDsleSchedule.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSchedule.Properties.AppearanceDisabled.Options.UseFont = True
        Me.INDsleSchedule.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSchedule.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDsleSchedule.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSchedule.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleSchedule.Properties.AppearanceItemDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSchedule.Properties.AppearanceItemDisabled.Options.UseFont = True
        Me.INDsleSchedule.Properties.AppearanceItemHighlight.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSchedule.Properties.AppearanceItemHighlight.Options.UseFont = True
        Me.INDsleSchedule.Properties.AppearanceItemSelected.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSchedule.Properties.AppearanceItemSelected.Options.UseFont = True
        Me.INDsleSchedule.Properties.AppearanceReadOnly.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleSchedule.Properties.AppearanceReadOnly.Options.UseFont = True
        Me.INDsleSchedule.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleSchedule.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Mañana", 1, 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tarde", 2, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Noche", 3, 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Mañana - Tarde", 4, 3), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tarde - Noche", 5, 4), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Mañana - Noche", 6, 5)})
        Me.INDsleSchedule.Properties.LargeImages = Me.ImageList1
        Me.INDsleSchedule.Properties.SmallImages = Me.ImageList1
        Me.INDsleSchedule.Size = New System.Drawing.Size(386, 28)
        Me.INDsleSchedule.StyleController = Me.INDlyRoot
        Me.INDsleSchedule.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleSchedule, 0)
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "manana.png")
        Me.ImageList1.Images.SetKeyName(1, "Tarde.png")
        Me.ImageList1.Images.SetKeyName(2, "noche.png")
        Me.ImageList1.Images.SetKeyName(3, "manana-tarde.png")
        Me.ImageList1.Images.SetKeyName(4, "tarde-noche.png")
        Me.ImageList1.Images.SetKeyName(5, "manana-noche.png")
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlyRoot
        Me.INDtxtName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, False)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Authorization.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlyRoot
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        '
        'INDsleUsers
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleUsers, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleUsers, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleUsers, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.Location = New System.Drawing.Point(1082, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleUsers, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleUsers.Name = "INDsleUsers"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleUsers, False)
        Me.INDsleUsers.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleUsers.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleUsers.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleUsers.Properties.Appearance.Options.UseFont = True
        Me.INDsleUsers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleUsers.Properties.DisplayMember = "CodeName"
        Me.INDsleUsers.Properties.NullText = ""
        Me.INDsleUsers.Properties.PopupSizeable = False
        Me.INDsleUsers.Properties.PopupView = Me.viewUserSearch
        Me.INDsleUsers.Properties.ShowFooter = False
        Me.INDsleUsers.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleUsers, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleUsers, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleUsers, True)
        Me.INDsleUsers.Size = New System.Drawing.Size(474, 28)
        Me.INDsleUsers.StyleController = Me.INDlyRoot
        Me.INDsleUsers.TabIndex = 4
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
        Me.viewUserSearch.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn2})
        Me.viewUserSearch.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.viewUserSearch.Name = "viewUserSearch"
        Me.viewUserSearch.OptionsFind.AlwaysVisible = True
        Me.viewUserSearch.OptionsFind.FindFilterColumns = "UserCode"
        Me.viewUserSearch.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.viewUserSearch.OptionsView.EnableAppearanceEvenRow = True
        Me.viewUserSearch.OptionsView.EnableAppearanceOddRow = True
        Me.viewUserSearch.OptionsView.ShowAutoFilterRow = True
        Me.viewUserSearch.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewUserSearch, False)
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
        Me.GridColumn11.Width = 333
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "IdPerson.Fullname"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 1049
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
        Me.INDgcUsers.Location = New System.Drawing.Point(962, 95)
        Me.INDgcUsers.MainView = Me.viewUsersGrid
        Me.INDgcUsers.Name = "INDgcUsers"
        Me.INDgcUsers.Size = New System.Drawing.Size(824, 462)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcUsers, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcUsers.TabIndex = 6
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
        Me.viewUsersGrid.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
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
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewUsersGrid, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código "
        Me.GridColumn3.FieldName = "UserCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 354
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripción"
        Me.GridColumn4.FieldName = "FullNameUser"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 1038
        '
        'INDbtnAddUser
        '
        Me.INDbtnAddUser.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddUser.Appearance.Options.UseFont = True
        Me.INDbtnAddUser.Location = New System.Drawing.Point(1560, 59)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddUser, True)
        Me.INDbtnAddUser.Name = "INDbtnAddUser"
        Me.INDbtnAddUser.Size = New System.Drawing.Size(226, 29)
        Me.INDbtnAddUser.StyleController = Me.INDlyRoot
        Me.INDbtnAddUser.TabIndex = 5
        Me.INDbtnAddUser.Text = "Agregar"
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
        Me.LayoutControlGroup1.CustomizationFormText = "Grupos de Facturación"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.INDlygScheduler, Me.INDlygAuthorization})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1810, 581)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "Datos Principales"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemSchedule})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 561)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.CustomizationFormText = "Nombre"
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemSchedule
        '
        Me.INDlyItemSchedule.Control = Me.INDsleSchedule
        Me.INDlyItemSchedule.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemSchedule.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSchedule.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSchedule.Name = "INDlyItemSchedule"
        Me.INDlyItemSchedule.ShowInCustomizationForm = False
        Me.INDlyItemSchedule.Size = New System.Drawing.Size(390, 382)
        Me.INDlyItemSchedule.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSchedule.Text = "Horario"
        Me.INDlyItemSchedule.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSchedule.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSchedule.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSchedule.TextToControlDistance = 5
        '
        'INDlygScheduler
        '
        Me.INDlygScheduler.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygScheduler.AppearanceGroup.Options.UseFont = True
        Me.INDlygScheduler.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygScheduler.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygScheduler.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygScheduler.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygScheduler.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygScheduler.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygScheduler.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygScheduler.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygScheduler.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygScheduler.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygScheduler.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygScheduler.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygScheduler, False)
        Me.INDlygScheduler.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemScheduler})
        Me.INDlygScheduler.Location = New System.Drawing.Point(414, 0)
        Me.INDlygScheduler.Name = "INDlygScheduler"
        Me.INDlygScheduler.Size = New System.Drawing.Size(524, 561)
        Me.INDlygScheduler.Text = "Turnos"
        '
        'INDlyItemScheduler
        '
        Me.INDlyItemScheduler.Control = Me.INDscScheduler
        Me.INDlyItemScheduler.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemScheduler.MaxSize = New System.Drawing.Size(500, 0)
        Me.INDlyItemScheduler.MinSize = New System.Drawing.Size(500, 1)
        Me.INDlyItemScheduler.Name = "INDlyItemScheduler"
        Me.INDlyItemScheduler.Size = New System.Drawing.Size(500, 502)
        Me.INDlyItemScheduler.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemScheduler.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemScheduler.TextVisible = False
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
        Me.INDlygAuthorization.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemUsers, Me.LayoutControlItem1, Me.INDlyItemAddUser})
        Me.INDlygAuthorization.Location = New System.Drawing.Point(938, 0)
        Me.INDlygAuthorization.Name = "INDlygAuthorization"
        Me.INDlygAuthorization.Size = New System.Drawing.Size(852, 561)
        Me.INDlygAuthorization.Text = "Asignación de Usuarios"
        '
        'INDlyItemUsers
        '
        Me.INDlyItemUsers.Control = Me.INDsleUsers
        Me.INDlyItemUsers.CustomizationFormText = "Usuarios"
        Me.INDlyItemUsers.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemUsers.MaxSize = New System.Drawing.Size(598, 36)
        Me.INDlyItemUsers.MinSize = New System.Drawing.Size(598, 36)
        Me.INDlyItemUsers.Name = "INDlyItemUsers"
        Me.INDlyItemUsers.Size = New System.Drawing.Size(598, 36)
        Me.INDlyItemUsers.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemUsers.Text = "Usuarios"
        Me.INDlyItemUsers.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemUsers.TextSize = New System.Drawing.Size(120, 21)
        Me.INDlyItemUsers.TextToControlDistance = 0
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcUsers
        Me.LayoutControlItem1.CustomizationFormText = "Usuarios"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(828, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(828, 1)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 466)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDlyItemAddUser
        '
        Me.INDlyItemAddUser.Control = Me.INDbtnAddUser
        Me.INDlyItemAddUser.CustomizationFormText = "Agregar"
        Me.INDlyItemAddUser.Location = New System.Drawing.Point(598, 0)
        Me.INDlyItemAddUser.MaxSize = New System.Drawing.Size(230, 33)
        Me.INDlyItemAddUser.MinSize = New System.Drawing.Size(230, 33)
        Me.INDlyItemAddUser.Name = "INDlyItemAddUser"
        Me.INDlyItemAddUser.Size = New System.Drawing.Size(230, 36)
        Me.INDlyItemAddUser.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddUser.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddUser.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmAuthorizationScheduleTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1186, 729)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAuthorizationScheduleTemplate"
        Me.Opacity = 1.0R
        Me.Tag = "2135"
        Me.Text = "Plantilla de Turnos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDscScheduler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleSchedule.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleUsers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewUserSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewUsersGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygScheduler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemScheduler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAuthorization, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDsleUsers As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents viewUserSearch As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcUsers As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewUsersGrid As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDbtnAddUser As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlygAuthorization As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemUsers As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAddUser As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents INDsleSchedule As DevExpress.XtraEditors.ImageComboBoxEdit
    Friend WithEvents INDlyItemSchedule As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDscScheduler As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents INDlygScheduler As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemScheduler As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SchedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
End Class
