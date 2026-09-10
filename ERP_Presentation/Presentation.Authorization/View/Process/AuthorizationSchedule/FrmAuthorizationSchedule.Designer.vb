Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAuthorizationSchedule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAuthorizationSchedule))
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.INDPopupLcNumberDay = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcDetHours = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetHours = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNextDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEvent = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepIcbeType = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
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
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemOtherSchDet = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPopupMoreLcEmployee = New DevExpress.XtraEditors.LabelControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBarRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.INDPopupGcMatches = New DevExpress.XtraGrid.GridControl()
        Me.INDPopupGvMatches = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCOlEmployeeNameDetailMates = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPopupLcTxtTemplate = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemDetailDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemDetailTemplate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemDetailEdit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTcgDetailsSchedule = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlyGrDetHours = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCtrGrMatchPartnerts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemDetailScheduleMates = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemDetailTemplateHoursNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPopupLcEmployee = New DevExpress.XtraEditors.LabelControl()
        Me.INDLcTotalHoursNumber1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDLcTotalHours1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDDDBOptionsMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPopMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPcScheduleDetailComplete = New DevExpress.XtraEditors.PanelControl()
        Me.INDPcDetailEm1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDDDBEmployee1 = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPcCDetail = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDclbcUsers = New DevExpress.XtraEditors.CheckedListBoxControl()
        Me.INDCalendar = New Presentation.Controls.CtrCalendar()
        Me.CtrDateNavigator1 = New Presentation.Controls.CtrDateNavigator()
        Me.INDsleScheduleTemplate = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewSearchPortfolio = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemScheduleTemplate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDgcDetHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDetHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepIcbeType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcCMore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcCMore.SuspendLayout()
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl4.SuspendLayout()
        CType(Me.INDPopupMoreGcHoursDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupMoreGvHoursDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemOtherSchDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupGcMatches, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupGvMatches, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgDetailsSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrDetHours, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtrGrMatchPartnerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailScheduleMates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemDetailTemplateHoursNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDPcScheduleDetailComplete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcScheduleDetailComplete.SuspendLayout()
        CType(Me.INDPcDetailEm1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcDetailEm1.SuspendLayout()
        CType(Me.INDPcCDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcCDetail.SuspendLayout()
        CType(Me.INDclbcUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleScheduleTemplate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewSearchPortfolio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemScheduleTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1370, 673)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1370, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1370, 130)
        '
        'INDPopupLcNumberDay
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupLcNumberDay, True)
        Me.INDPopupLcNumberDay.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPopupLcNumberDay.Appearance.ForeColor = System.Drawing.SystemColors.Control
        Me.INDPopupLcNumberDay.Appearance.Options.UseFont = True
        Me.INDPopupLcNumberDay.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupLcNumberDay, False)
        Me.INDPopupLcNumberDay.Location = New System.Drawing.Point(12, 12)
        Me.INDPopupLcNumberDay.Name = "INDPopupLcNumberDay"
        Me.INDPopupLcNumberDay.Size = New System.Drawing.Size(308, 25)
        Me.INDPopupLcNumberDay.StyleController = Me.LayoutControl2
        Me.INDPopupLcNumberDay.TabIndex = 4
        Me.INDPopupLcNumberDay.Text = " "
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
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 33)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(332, 274)
        Me.LayoutControl2.TabIndex = 2
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDgcDetHours
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetHours, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetHours, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetHours, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetHours, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetHours, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetHours, False)
        Me.INDgcDetHours.Location = New System.Drawing.Point(24, 115)
        Me.INDgcDetHours.MainView = Me.INDgvDetHours
        Me.INDgcDetHours.Name = "INDgcDetHours"
        Me.INDgcDetHours.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepIcbeType})
        Me.INDgcDetHours.Size = New System.Drawing.Size(288, 103)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetHours, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDetHours.TabIndex = 10
        Me.INDgcDetHours.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetHours})
        '
        'INDgvDetHours
        '
        Me.INDgvDetHours.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetHours.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDetHours.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDetHours.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDetHours.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDetHours.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvDetHours.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetHours.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDetHours.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetHours.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDetHours.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDetHours.Appearance.Row.Options.UseFont = True
        Me.INDgvDetHours.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDetHours.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDetHours.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColStart, Me.INDColEnd, Me.INDColNextDay, Me.INDColEvent})
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
        Me.INDColStart.Caption = "H. Ini"
        Me.INDColStart.FieldName = "InitialTime"
        Me.INDColStart.Name = "INDColStart"
        Me.INDColStart.OptionsColumn.AllowEdit = False
        Me.INDColStart.OptionsColumn.AllowFocus = False
        Me.INDColStart.Visible = True
        Me.INDColStart.VisibleIndex = 0
        '
        'INDColEnd
        '
        Me.INDColEnd.Caption = "H. Fin"
        Me.INDColEnd.FieldName = "EndingTime"
        Me.INDColEnd.Name = "INDColEnd"
        Me.INDColEnd.OptionsColumn.AllowEdit = False
        Me.INDColEnd.OptionsColumn.AllowFocus = False
        Me.INDColEnd.Visible = True
        Me.INDColEnd.VisibleIndex = 1
        '
        'INDColNextDay
        '
        Me.INDColNextDay.Caption = "Sig Día"
        Me.INDColNextDay.FieldName = "NextDay"
        Me.INDColNextDay.Name = "INDColNextDay"
        Me.INDColNextDay.OptionsColumn.AllowEdit = False
        Me.INDColNextDay.OptionsColumn.AllowFocus = False
        Me.INDColNextDay.Visible = True
        Me.INDColNextDay.VisibleIndex = 2
        '
        'INDColEvent
        '
        Me.INDColEvent.Caption = "Tipo"
        Me.INDColEvent.ColumnEdit = Me.INDrepIcbeType
        Me.INDColEvent.FieldName = "Type"
        Me.INDColEvent.Name = "INDColEvent"
        Me.INDColEvent.OptionsColumn.AllowEdit = False
        Me.INDColEvent.OptionsColumn.AllowFocus = False
        Me.INDColEvent.Visible = True
        Me.INDColEvent.VisibleIndex = 3
        '
        'INDrepIcbeType
        '
        Me.INDrepIcbeType.AutoHeight = False
        Me.INDrepIcbeType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepIcbeType.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Turno", 1, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Evento", 2, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Novedad", 3, -1)})
        Me.INDrepIcbeType.Name = "INDrepIcbeType"
        '
        'INDPopupLcTxtTemplateHoursNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupLcTxtTemplateHoursNumber, True)
        Me.INDPopupLcTxtTemplateHoursNumber.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPopupLcTxtTemplateHoursNumber.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDPopupLcTxtTemplateHoursNumber.Appearance.Options.UseFont = True
        Me.INDPopupLcTxtTemplateHoursNumber.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupLcTxtTemplateHoursNumber, False)
        Me.INDPopupLcTxtTemplateHoursNumber.Location = New System.Drawing.Point(291, 41)
        Me.INDPopupLcTxtTemplateHoursNumber.Name = "INDPopupLcTxtTemplateHoursNumber"
        Me.INDPopupLcTxtTemplateHoursNumber.Size = New System.Drawing.Size(29, 26)
        Me.INDPopupLcTxtTemplateHoursNumber.StyleController = Me.LayoutControl2
        Me.INDPopupLcTxtTemplateHoursNumber.TabIndex = 9
        '
        'INDPopupSBEdit
        '
        Me.INDPopupSBEdit.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDPopupSBEdit.Appearance.Options.UseFont = True
        Me.INDPopupSBEdit.Location = New System.Drawing.Point(12, 230)
        Me.INDPopupSBEdit.Name = "INDPopupSBEdit"
        Me.INDPopupSBEdit.Size = New System.Drawing.Size(308, 32)
        Me.INDPopupSBEdit.StyleController = Me.LayoutControl2
        Me.INDPopupSBEdit.TabIndex = 8
        Me.INDPopupSBEdit.Text = "Editar"
        '
        'INDPcCMore
        '
        Me.INDPcCMore.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcCMore.Controls.Add(Me.LayoutControl4)
        Me.INDPcCMore.Controls.Add(Me.INDPopupMoreLcEmployee)
        Me.INDPcCMore.Location = New System.Drawing.Point(105, 216)
        Me.INDPcCMore.Manager = Me.BarManager1
        Me.INDPcCMore.Name = "INDPcCMore"
        Me.INDPcCMore.Size = New System.Drawing.Size(328, 243)
        Me.INDPcCMore.TabIndex = 19
        Me.INDPcCMore.Visible = False
        '
        'LayoutControl4
        '
        Me.LayoutControl4.BackColor = System.Drawing.Color.White
        Me.LayoutControl4.Controls.Add(Me.INDPopupMoreLcTxtOtherSchHoursNumber)
        Me.LayoutControl4.Controls.Add(Me.INDPopupMoreGcHoursDetail)
        Me.LayoutControl4.Controls.Add(Me.INDPopupMoreLcNumberDay)
        Me.LayoutControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl4.Location = New System.Drawing.Point(0, 33)
        Me.LayoutControl4.Name = "LayoutControl4"
        Me.LayoutControl4.Root = Me.LayoutControlGroup4
        Me.LayoutControl4.Size = New System.Drawing.Size(328, 210)
        Me.LayoutControl4.TabIndex = 2
        Me.LayoutControl4.Text = "LayoutControl4"
        '
        'INDPopupMoreLcTxtOtherSchHoursNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupMoreLcTxtOtherSchHoursNumber, True)
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.Options.UseFont = True
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupMoreLcTxtOtherSchHoursNumber, False)
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Location = New System.Drawing.Point(104, 37)
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Name = "INDPopupMoreLcTxtOtherSchHoursNumber"
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.Size = New System.Drawing.Size(212, 21)
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.StyleController = Me.LayoutControl4
        Me.INDPopupMoreLcTxtOtherSchHoursNumber.TabIndex = 9
        '
        'INDPopupMoreGcHoursDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDPopupMoreGcHoursDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDPopupMoreGcHoursDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDPopupMoreGcHoursDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDPopupMoreGcHoursDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDPopupMoreGcHoursDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDPopupMoreGcHoursDetail, False)
        Me.INDPopupMoreGcHoursDetail.Location = New System.Drawing.Point(12, 86)
        Me.INDPopupMoreGcHoursDetail.MainView = Me.INDPopupMoreGvHoursDetail
        Me.INDPopupMoreGcHoursDetail.Name = "INDPopupMoreGcHoursDetail"
        Me.INDPopupMoreGcHoursDetail.Size = New System.Drawing.Size(304, 112)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDPopupMoreGcHoursDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDPopupMoreGcHoursDetail.TabIndex = 7
        Me.INDPopupMoreGcHoursDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDPopupMoreGvHoursDetail})
        '
        'INDPopupMoreGvHoursDetail
        '
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDPopupMoreGvHoursDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDPopupMoreGvHoursDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDPopupMoreGvHoursDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDPopupMoreGvHoursDetail.Appearance.Row.Options.UseFont = True
        Me.INDPopupMoreGvHoursDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
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
        Me.FunctionalUnitMore.Caption = "U Funcional"
        Me.FunctionalUnitMore.FieldName = "DetMoreFU"
        Me.FunctionalUnitMore.Name = "FunctionalUnitMore"
        Me.FunctionalUnitMore.Visible = True
        Me.FunctionalUnitMore.VisibleIndex = 0
        Me.FunctionalUnitMore.Width = 170
        '
        'Template
        '
        Me.Template.Caption = "Jornada"
        Me.Template.FieldName = "DetMoreT"
        Me.Template.Name = "Template"
        Me.Template.Visible = True
        Me.Template.VisibleIndex = 1
        Me.Template.Width = 115
        '
        'INDPopupMoreLcNumberDay
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupMoreLcNumberDay, True)
        Me.INDPopupMoreLcNumberDay.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPopupMoreLcNumberDay.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDPopupMoreLcNumberDay.Appearance.Options.UseFont = True
        Me.INDPopupMoreLcNumberDay.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupMoreLcNumberDay, False)
        Me.INDPopupMoreLcNumberDay.Location = New System.Drawing.Point(12, 12)
        Me.INDPopupMoreLcNumberDay.Name = "INDPopupMoreLcNumberDay"
        Me.INDPopupMoreLcNumberDay.Size = New System.Drawing.Size(304, 21)
        Me.INDPopupMoreLcNumberDay.StyleController = Me.LayoutControl4
        Me.INDPopupMoreLcNumberDay.TabIndex = 4
        Me.INDPopupMoreLcNumberDay.Text = " "
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup4, False)
        Me.LayoutControlGroup4.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6, Me.INDLyItemOtherSchDet, Me.LayoutControlItem7})
        Me.LayoutControlGroup4.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(328, 210)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDPopupMoreLcNumberDay
        Me.LayoutControlItem6.CustomizationFormText = "INDLyItemDetailDate"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(102, 25)
        Me.LayoutControlItem6.Name = "INDLyItemDetailDate"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(308, 25)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'INDLyItemOtherSchDet
        '
        Me.INDLyItemOtherSchDet.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyItemOtherSchDet.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.INDLyItemOtherSchDet.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemOtherSchDet.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemOtherSchDet.Control = Me.INDPopupMoreGcHoursDetail
        Me.INDLyItemOtherSchDet.CustomizationFormText = "INDLyItemDetailScheduleMates"
        Me.INDLyItemOtherSchDet.Location = New System.Drawing.Point(0, 50)
        Me.INDLyItemOtherSchDet.MinSize = New System.Drawing.Size(156, 48)
        Me.INDLyItemOtherSchDet.Name = "INDLyItemOtherSchDet"
        Me.INDLyItemOtherSchDet.Size = New System.Drawing.Size(308, 140)
        Me.INDLyItemOtherSchDet.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemOtherSchDet.Text = "Turnos En Otras Unidades Funcionales"
        Me.INDLyItemOtherSchDet.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyItemOtherSchDet.TextSize = New System.Drawing.Size(266, 21)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlItem7.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseForeColor = True
        Me.LayoutControlItem7.Control = Me.INDPopupMoreLcTxtOtherSchHoursNumber
        Me.LayoutControlItem7.CustomizationFormText = "INDLyItemDetailTemplateHoursNumber"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 25)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(96, 25)
        Me.LayoutControlItem7.Name = "INDLyItemDetailTemplateHoursNumber"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(308, 25)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "N Horas"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(80, 21)
        Me.LayoutControlItem7.TextToControlDistance = 12
        '
        'INDPopupMoreLcEmployee
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupMoreLcEmployee, True)
        Me.INDPopupMoreLcEmployee.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPopupMoreLcEmployee.Appearance.ForeColor = System.Drawing.SystemColors.Control
        Me.INDPopupMoreLcEmployee.Appearance.Options.UseFont = True
        Me.INDPopupMoreLcEmployee.Appearance.Options.UseForeColor = True
        Me.INDPopupMoreLcEmployee.Appearance.Options.UseTextOptions = True
        Me.INDPopupMoreLcEmployee.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDPopupMoreLcEmployee.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupMoreLcEmployee, False)
        Me.INDPopupMoreLcEmployee.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDPopupMoreLcEmployee.Location = New System.Drawing.Point(0, 0)
        Me.INDPopupMoreLcEmployee.Name = "INDPopupMoreLcEmployee"
        Me.INDPopupMoreLcEmployee.Size = New System.Drawing.Size(328, 33)
        Me.INDPopupMoreLcEmployee.TabIndex = 1
        Me.INDPopupMoreLcEmployee.Text = " "
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.DockControls.Add(Me.BarDockControl2)
        Me.BarManager1.DockControls.Add(Me.BarDockControl3)
        Me.BarManager1.DockControls.Add(Me.BarDockControl4)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBarRefresh})
        Me.BarManager1.MaxItemId = 7
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl1.Manager = Me.BarManager1
        Me.BarDockControl1.Size = New System.Drawing.Size(1370, 0)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 808)
        Me.BarDockControl2.Manager = Me.BarManager1
        Me.BarDockControl2.Size = New System.Drawing.Size(1370, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl3.Manager = Me.BarManager1
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 803)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1370, 5)
        Me.BarDockControl4.Manager = Me.BarManager1
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 803)
        '
        'INDBarRefresh
        '
        Me.INDBarRefresh.Caption = "Refrescar"
        Me.INDBarRefresh.Id = 5
        Me.INDBarRefresh.ImageOptions.Image = Global.Presentation.Authorization.My.Resources.Resources.Refresh_16x16_blue
        Me.INDBarRefresh.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarRefresh.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBarRefresh.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarRefresh.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBarRefresh.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBarRefresh.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarRefresh.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBarRefresh.Name = "INDBarRefresh"
        '
        'INDPopupGcMatches
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDPopupGcMatches, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDPopupGcMatches, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDPopupGcMatches, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDPopupGcMatches, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDPopupGcMatches, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDPopupGcMatches, False)
        Me.INDPopupGcMatches.Location = New System.Drawing.Point(24, 115)
        Me.INDPopupGcMatches.MainView = Me.INDPopupGvMatches
        Me.INDPopupGcMatches.Name = "INDPopupGcMatches"
        Me.INDPopupGcMatches.Size = New System.Drawing.Size(288, 103)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDPopupGcMatches, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDPopupGcMatches.TabIndex = 7
        Me.INDPopupGcMatches.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDPopupGvMatches})
        '
        'INDPopupGvMatches
        '
        Me.INDPopupGvMatches.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDPopupGvMatches.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDPopupGvMatches.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDPopupGvMatches.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDPopupGvMatches.Appearance.FocusedRow.Options.UseFont = True
        Me.INDPopupGvMatches.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDPopupGvMatches.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDPopupGvMatches.Appearance.GroupRow.Options.UseFont = True
        Me.INDPopupGvMatches.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDPopupGvMatches.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDPopupGvMatches.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDPopupGvMatches.Appearance.Row.Options.UseFont = True
        Me.INDPopupGvMatches.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
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
        Me.INDCOlEmployeeNameDetailMates.Caption = "Employee"
        Me.INDCOlEmployeeNameDetailMates.FieldName = "Employee.ThirdParty.Name"
        Me.INDCOlEmployeeNameDetailMates.Name = "INDCOlEmployeeNameDetailMates"
        Me.INDCOlEmployeeNameDetailMates.Visible = True
        Me.INDCOlEmployeeNameDetailMates.VisibleIndex = 0
        '
        'INDPopupLcTxtTemplate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupLcTxtTemplate, True)
        Me.INDPopupLcTxtTemplate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPopupLcTxtTemplate.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDPopupLcTxtTemplate.Appearance.Options.UseFont = True
        Me.INDPopupLcTxtTemplate.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupLcTxtTemplate, False)
        Me.INDPopupLcTxtTemplate.Location = New System.Drawing.Point(84, 41)
        Me.INDPopupLcTxtTemplate.Name = "INDPopupLcTxtTemplate"
        Me.INDPopupLcTxtTemplate.Size = New System.Drawing.Size(131, 26)
        Me.INDPopupLcTxtTemplate.StyleController = Me.LayoutControl2
        Me.INDPopupLcTxtTemplate.TabIndex = 5
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
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemDetailDate, Me.INDLyItemDetailTemplate, Me.INDLyItemDetailEdit, Me.INDTcgDetailsSchedule, Me.INDLyItemDetailTemplateHoursNumber})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(332, 274)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLyItemDetailDate
        '
        Me.INDLyItemDetailDate.Control = Me.INDPopupLcNumberDay
        Me.INDLyItemDetailDate.CustomizationFormText = "INDLyItemDetailDate"
        Me.INDLyItemDetailDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemDetailDate.MinSize = New System.Drawing.Size(102, 25)
        Me.INDLyItemDetailDate.Name = "INDLyItemDetailDate"
        Me.INDLyItemDetailDate.Size = New System.Drawing.Size(312, 29)
        Me.INDLyItemDetailDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailDate.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemDetailDate.TextVisible = False
        '
        'INDLyItemDetailTemplate
        '
        Me.INDLyItemDetailTemplate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyItemDetailTemplate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.INDLyItemDetailTemplate.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemDetailTemplate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemDetailTemplate.Control = Me.INDPopupLcTxtTemplate
        Me.INDLyItemDetailTemplate.CustomizationFormText = "INDLyItemDetailTemplate"
        Me.INDLyItemDetailTemplate.Location = New System.Drawing.Point(0, 29)
        Me.INDLyItemDetailTemplate.MinSize = New System.Drawing.Size(76, 25)
        Me.INDLyItemDetailTemplate.Name = "INDLyItemDetailTemplate"
        Me.INDLyItemDetailTemplate.Size = New System.Drawing.Size(207, 30)
        Me.INDLyItemDetailTemplate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailTemplate.Text = "Jornada"
        Me.INDLyItemDetailTemplate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemDetailTemplate.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLyItemDetailTemplate.TextToControlDistance = 12
        '
        'INDLyItemDetailEdit
        '
        Me.INDLyItemDetailEdit.Control = Me.INDPopupSBEdit
        Me.INDLyItemDetailEdit.CustomizationFormText = "INDLyItemDetailEdit"
        Me.INDLyItemDetailEdit.Location = New System.Drawing.Point(0, 218)
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
        Me.INDTcgDetailsSchedule.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Semilight", 12.0!)
        Me.INDTcgDetailsSchedule.AppearanceTabPage.Header.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.INDTcgDetailsSchedule.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDTcgDetailsSchedule.AppearanceTabPage.Header.Options.UseForeColor = True
        Me.INDTcgDetailsSchedule.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDTcgDetailsSchedule.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDTcgDetailsSchedule.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDTcgDetailsSchedule.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDTcgDetailsSchedule.CustomizationFormText = "Pestañas Detalles de Horas"
        Me.INDTcgDetailsSchedule.Location = New System.Drawing.Point(0, 59)
        Me.INDTcgDetailsSchedule.Name = "INDTcgDetailsSchedule"
        Me.INDTcgDetailsSchedule.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 9, 2, 9)
        Me.INDTcgDetailsSchedule.SelectedTabPage = Me.INDlyGrDetHours
        Me.INDTcgDetailsSchedule.Size = New System.Drawing.Size(312, 159)
        Me.INDTcgDetailsSchedule.Spacing = New DevExpress.XtraLayout.Utils.Padding(2, -2, 10, -2)
        Me.INDTcgDetailsSchedule.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrDetHours, Me.INDlyCtrGrMatchPartnerts})
        Me.INDTcgDetailsSchedule.Text = " "
        '
        'INDlyGrDetHours
        '
        Me.INDlyGrDetHours.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrDetHours.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrDetHours.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrDetHours.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrDetHours.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrDetHours.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrDetHours.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrDetHours.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrDetHours.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrDetHours.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrDetHours.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrDetHours.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrDetHours.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrDetHours.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrDetHours, False)
        Me.INDlyGrDetHours.CustomizationFormText = "Detalle Horas"
        Me.INDlyGrDetHours.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5})
        Me.INDlyGrDetHours.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrDetHours.Name = "INDlyGrDetHours"
        Me.INDlyGrDetHours.Size = New System.Drawing.Size(292, 107)
        Me.INDlyGrDetHours.Text = "Det De Horas"
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDgcDetHours
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem3"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(292, 107)
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'INDlyCtrGrMatchPartnerts
        '
        Me.INDlyCtrGrMatchPartnerts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCtrGrMatchPartnerts.AppearanceGroup.Options.UseFont = True
        Me.INDlyCtrGrMatchPartnerts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCtrGrMatchPartnerts.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCtrGrMatchPartnerts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyCtrGrMatchPartnerts, False)
        Me.INDlyCtrGrMatchPartnerts.CustomizationFormText = "Compañeros"
        Me.INDlyCtrGrMatchPartnerts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyItemDetailScheduleMates})
        Me.INDlyCtrGrMatchPartnerts.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCtrGrMatchPartnerts.Name = "INDlyCtrGrMatchPartnerts"
        Me.INDlyCtrGrMatchPartnerts.Size = New System.Drawing.Size(292, 107)
        Me.INDlyCtrGrMatchPartnerts.Text = "Compañeros"
        '
        'INDLyItemDetailScheduleMates
        '
        Me.INDLyItemDetailScheduleMates.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyItemDetailScheduleMates.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.INDLyItemDetailScheduleMates.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemDetailScheduleMates.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemDetailScheduleMates.Control = Me.INDPopupGcMatches
        Me.INDLyItemDetailScheduleMates.CustomizationFormText = "INDLyItemDetailScheduleMates"
        Me.INDLyItemDetailScheduleMates.Location = New System.Drawing.Point(0, 0)
        Me.INDLyItemDetailScheduleMates.MinSize = New System.Drawing.Size(156, 48)
        Me.INDLyItemDetailScheduleMates.Name = "INDLyItemDetailScheduleMates"
        Me.INDLyItemDetailScheduleMates.Size = New System.Drawing.Size(292, 107)
        Me.INDLyItemDetailScheduleMates.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailScheduleMates.Text = "Compañeros de turno"
        Me.INDLyItemDetailScheduleMates.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLyItemDetailScheduleMates.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemDetailScheduleMates.TextVisible = False
        '
        'INDLyItemDetailTemplateHoursNumber
        '
        Me.INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.ForeColor = System.Drawing.Color.DimGray
        Me.INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyItemDetailTemplateHoursNumber.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDLyItemDetailTemplateHoursNumber.Control = Me.INDPopupLcTxtTemplateHoursNumber
        Me.INDLyItemDetailTemplateHoursNumber.CustomizationFormText = "INDLyItemDetailTemplateHoursNumber"
        Me.INDLyItemDetailTemplateHoursNumber.Location = New System.Drawing.Point(207, 29)
        Me.INDLyItemDetailTemplateHoursNumber.MinSize = New System.Drawing.Size(96, 25)
        Me.INDLyItemDetailTemplateHoursNumber.Name = "INDLyItemDetailTemplateHoursNumber"
        Me.INDLyItemDetailTemplateHoursNumber.Size = New System.Drawing.Size(105, 30)
        Me.INDLyItemDetailTemplateHoursNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemDetailTemplateHoursNumber.Text = "N Horas"
        Me.INDLyItemDetailTemplateHoursNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemDetailTemplateHoursNumber.TextSize = New System.Drawing.Size(60, 21)
        Me.INDLyItemDetailTemplateHoursNumber.TextToControlDistance = 12
        '
        'INDPopupLcEmployee
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDPopupLcEmployee, True)
        Me.INDPopupLcEmployee.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPopupLcEmployee.Appearance.ForeColor = System.Drawing.SystemColors.Control
        Me.INDPopupLcEmployee.Appearance.Options.UseFont = True
        Me.INDPopupLcEmployee.Appearance.Options.UseForeColor = True
        Me.INDPopupLcEmployee.Appearance.Options.UseTextOptions = True
        Me.INDPopupLcEmployee.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDPopupLcEmployee.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDPopupLcEmployee, False)
        Me.INDPopupLcEmployee.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDPopupLcEmployee.Location = New System.Drawing.Point(0, 0)
        Me.INDPopupLcEmployee.Name = "INDPopupLcEmployee"
        Me.INDPopupLcEmployee.Size = New System.Drawing.Size(332, 33)
        Me.INDPopupLcEmployee.TabIndex = 1
        Me.INDPopupLcEmployee.Text = " "
        '
        'INDLcTotalHoursNumber1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcTotalHoursNumber1, True)
        Me.INDLcTotalHoursNumber1.Appearance.BackColor = System.Drawing.Color.Gray
        Me.INDLcTotalHoursNumber1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcTotalHoursNumber1.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDLcTotalHoursNumber1.Appearance.Options.UseBackColor = True
        Me.INDLcTotalHoursNumber1.Appearance.Options.UseFont = True
        Me.INDLcTotalHoursNumber1.Appearance.Options.UseForeColor = True
        Me.INDLcTotalHoursNumber1.Appearance.Options.UseTextOptions = True
        Me.INDLcTotalHoursNumber1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLcTotalHoursNumber1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcTotalHoursNumber1, False)
        Me.INDLcTotalHoursNumber1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDLcTotalHoursNumber1.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDLcTotalHoursNumber1.Location = New System.Drawing.Point(0, 0)
        Me.INDLcTotalHoursNumber1.Name = "INDLcTotalHoursNumber1"
        Me.INDLcTotalHoursNumber1.Size = New System.Drawing.Size(98, 42)
        Me.INDLcTotalHoursNumber1.TabIndex = 1
        Me.INDLcTotalHoursNumber1.Tag = "1"
        Me.INDLcTotalHoursNumber1.Text = "0"
        '
        'INDLcTotalHours1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDLcTotalHours1, True)
        Me.INDLcTotalHours1.Appearance.BackColor = System.Drawing.Color.White
        Me.INDLcTotalHours1.Appearance.BorderColor = System.Drawing.Color.Gray
        Me.INDLcTotalHours1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcTotalHours1.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.INDLcTotalHours1.Appearance.Options.UseBackColor = True
        Me.INDLcTotalHours1.Appearance.Options.UseBorderColor = True
        Me.INDLcTotalHours1.Appearance.Options.UseFont = True
        Me.INDLcTotalHours1.Appearance.Options.UseForeColor = True
        Me.INDLcTotalHours1.Appearance.Options.UseTextOptions = True
        Me.INDLcTotalHours1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLcTotalHours1.AutoEllipsis = True
        Me.INDLcTotalHours1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDLcTotalHours1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDLcTotalHours1, False)
        Me.INDLcTotalHours1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDLcTotalHours1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDLcTotalHours1.Location = New System.Drawing.Point(0, 39)
        Me.INDLcTotalHours1.Name = "INDLcTotalHours1"
        Me.INDLcTotalHours1.Size = New System.Drawing.Size(98, 18)
        Me.INDLcTotalHours1.TabIndex = 0
        Me.INDLcTotalHours1.Tag = "1"
        Me.INDLcTotalHours1.Text = "Det Horas"
        '
        'INDDDBOptionsMenu
        '
        Me.INDDDBOptionsMenu.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.INDDDBOptionsMenu.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDDBOptionsMenu.DropDownControl = Me.INDPopMenuActions
        Me.INDDDBOptionsMenu.ImageOptions.Image = CType(resources.GetObject("INDDDBOptionsMenu.ImageOptions.Image"), System.Drawing.Image)
        Me.INDDDBOptionsMenu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDDDBOptionsMenu.Location = New System.Drawing.Point(1026, 12)
        Me.INDDDBOptionsMenu.MenuManager = Me.BarManager1
        Me.INDDDBOptionsMenu.Name = "INDDDBOptionsMenu"
        Me.INDDDBOptionsMenu.Size = New System.Drawing.Size(65, 65)
        Me.INDDDBOptionsMenu.StyleController = Me.INDlyRoot
        Me.INDDDBOptionsMenu.TabIndex = 9
        '
        'INDPopMenuActions
        '
        Me.INDPopMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarRefresh)})
        Me.INDPopMenuActions.Manager = Me.BarManager1
        Me.INDPopMenuActions.Name = "INDPopMenuActions"
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDPcScheduleDetailComplete)
        Me.INDlyRoot.Controls.Add(Me.INDPcCDetail)
        Me.INDlyRoot.Controls.Add(Me.INDclbcUsers)
        Me.INDlyRoot.Controls.Add(Me.INDCalendar)
        Me.INDlyRoot.Controls.Add(Me.CtrDateNavigator1)
        Me.INDlyRoot.Controls.Add(Me.INDDDBOptionsMenu)
        Me.INDlyRoot.Controls.Add(Me.INDsleScheduleTemplate)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1366, 664)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDPcScheduleDetailComplete
        '
        Me.INDPcScheduleDetailComplete.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcScheduleDetailComplete.Controls.Add(Me.INDPcDetailEm1)
        Me.INDPcScheduleDetailComplete.Location = New System.Drawing.Point(242, 571)
        Me.INDPcScheduleDetailComplete.Name = "INDPcScheduleDetailComplete"
        Me.INDPcScheduleDetailComplete.Size = New System.Drawing.Size(839, 64)
        Me.INDPcScheduleDetailComplete.TabIndex = 20
        '
        'INDPcDetailEm1
        '
        Me.INDPcDetailEm1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcDetailEm1.Controls.Add(Me.INDLcTotalHoursNumber1)
        Me.INDPcDetailEm1.Controls.Add(Me.INDLcTotalHours1)
        Me.INDPcDetailEm1.Controls.Add(Me.INDDDBEmployee1)
        Me.INDPcDetailEm1.Location = New System.Drawing.Point(738, 3)
        Me.INDPcDetailEm1.Name = "INDPcDetailEm1"
        Me.INDPcDetailEm1.Size = New System.Drawing.Size(98, 57)
        Me.INDPcDetailEm1.TabIndex = 7
        '
        'INDDDBEmployee1
        '
        Me.INDDDBEmployee1.Location = New System.Drawing.Point(-3, 1)
        Me.INDDDBEmployee1.MenuManager = Me.BarManager1
        Me.INDDDBEmployee1.Name = "INDDDBEmployee1"
        Me.INDDDBEmployee1.Size = New System.Drawing.Size(1, 60)
        Me.INDDDBEmployee1.TabIndex = 5
        Me.INDDDBEmployee1.Text = "DropDownButton1"
        '
        'INDPcCDetail
        '
        Me.INDPcCDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcCDetail.Controls.Add(Me.LayoutControl2)
        Me.INDPcCDetail.Controls.Add(Me.INDPopupLcEmployee)
        Me.INDPcCDetail.Location = New System.Drawing.Point(477, 274)
        Me.INDPcCDetail.Manager = Me.BarManager1
        Me.INDPcCDetail.Name = "INDPcCDetail"
        Me.INDPcCDetail.Size = New System.Drawing.Size(332, 307)
        Me.INDPcCDetail.TabIndex = 19
        Me.INDPcCDetail.Visible = False
        '
        'INDclbcUsers
        '
        Me.INDclbcUsers.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDclbcUsers.Appearance.ForeColor = System.Drawing.Color.DimGray
        Me.INDclbcUsers.Appearance.Options.UseFont = True
        Me.INDclbcUsers.Appearance.Options.UseForeColor = True
        Me.INDclbcUsers.CheckOnClick = True
        Me.INDclbcUsers.ItemHeight = 30
        Me.INDclbcUsers.Location = New System.Drawing.Point(1095, 146)
        Me.INDclbcUsers.Name = "INDclbcUsers"
        Me.INDclbcUsers.Size = New System.Drawing.Size(282, 489)
        Me.INDclbcUsers.StyleController = Me.INDlyRoot
        Me.INDclbcUsers.TabIndex = 12
        '
        'INDCalendar
        '
        Me.INDCalendar.DaySelected = 0
        Me.INDCalendar.IncrementX = 119
        Me.INDCalendar.IncrementY = 79
        Me.INDCalendar.ListDaysToDelete = CType(resources.GetObject("INDCalendar.ListDaysToDelete"), System.Collections.Generic.List(Of Integer))
        Me.INDCalendar.ListHoliday = Nothing
        Me.INDCalendar.Location = New System.Drawing.Point(242, 81)
        Me.INDCalendar.LocationX = 0
        Me.INDCalendar.LocationY = 35
        Me.INDCalendar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDCalendar.ModeDelete = False
        Me.INDCalendar.ModeDeleteColor = System.Drawing.Color.MistyRose
        Me.INDCalendar.MonthControl = 9
        Me.INDCalendar.Name = "INDCalendar"
        Me.INDCalendar.PopUpControlToShowDetail = Me.INDPcCDetail
        Me.INDCalendar.PopUpControlToShowMore = Nothing
        Me.INDCalendar.RegColor1 = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(155, Byte), Integer))
        Me.INDCalendar.RegColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(171, Byte), Integer))
        Me.INDCalendar.Size = New System.Drawing.Size(839, 486)
        Me.INDCalendar.SizeChilds = New System.Drawing.Size(120, 80)
        Me.INDCalendar.TabIndex = 11
        Me.INDCalendar.YearControl = 2013
        '
        'CtrDateNavigator1
        '
        Me.CtrDateNavigator1.CtrCalendar = Me.INDCalendar
        Me.CtrDateNavigator1.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.CtrDateNavigator1.Location = New System.Drawing.Point(1095, 12)
        Me.CtrDateNavigator1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CtrDateNavigator1.Name = "CtrDateNavigator1"
        Me.CtrDateNavigator1.Size = New System.Drawing.Size(282, 65)
        Me.CtrDateNavigator1.TabIndex = 10
        Me.CtrDateNavigator1.WithEvent = True
        '
        'INDsleScheduleTemplate
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleScheduleTemplate, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleScheduleTemplate, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.INDsleScheduleTemplate.Location = New System.Drawing.Point(12, 12)
        Me.INDsleScheduleTemplate.MaximumSize = New System.Drawing.Size(0, 67)
        Me.INDsleScheduleTemplate.MinimumSize = New System.Drawing.Size(0, 67)
        Me.INDsleScheduleTemplate.Name = "INDsleScheduleTemplate"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.INDsleScheduleTemplate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleScheduleTemplate.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.INDsleScheduleTemplate.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDsleScheduleTemplate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 28.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleScheduleTemplate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleScheduleTemplate.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleScheduleTemplate.Properties.Appearance.Options.UseBorderColor = True
        Me.INDsleScheduleTemplate.Properties.Appearance.Options.UseFont = True
        Me.INDsleScheduleTemplate.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleScheduleTemplate.Properties.Appearance.Options.UseTextOptions = True
        Me.INDsleScheduleTemplate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDsleScheduleTemplate.Properties.DisplayMember = "CodeName"
        Me.INDsleScheduleTemplate.Properties.NullText = "Seleccione una Plantilla de Turnos"
        Me.INDsleScheduleTemplate.Properties.PopupSizeable = False
        Me.INDsleScheduleTemplate.Properties.PopupView = Me.INDviewSearchPortfolio
        Me.INDsleScheduleTemplate.Properties.ShowClearButton = False
        Me.INDsleScheduleTemplate.Properties.ShowFooter = False
        Me.INDsleScheduleTemplate.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleScheduleTemplate, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleScheduleTemplate, True)
        Me.INDsleScheduleTemplate.Size = New System.Drawing.Size(1010, 67)
        Me.INDsleScheduleTemplate.StyleController = Me.INDlyRoot
        Me.INDsleScheduleTemplate.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleScheduleTemplate, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleScheduleTemplate, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleScheduleTemplate, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleScheduleTemplate, False)
        '
        'INDviewSearchPortfolio
        '
        Me.INDviewSearchPortfolio.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewSearchPortfolio.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewSearchPortfolio.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewSearchPortfolio.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewSearchPortfolio.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewSearchPortfolio.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchPortfolio.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewSearchPortfolio.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchPortfolio.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewSearchPortfolio.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewSearchPortfolio.Appearance.Row.Options.UseFont = True
        Me.INDviewSearchPortfolio.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDviewSearchPortfolio.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewSearchPortfolio.Name = "INDviewSearchPortfolio"
        Me.INDviewSearchPortfolio.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewSearchPortfolio.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewSearchPortfolio.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewSearchPortfolio.OptionsView.ShowAutoFilterRow = True
        Me.INDviewSearchPortfolio.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewSearchPortfolio, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 381
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 926
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemScheduleTemplate, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem8, Me.EmptySpaceItem2, Me.EmptySpaceItem1})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1389, 647)
        Me.Root.TextVisible = False
        '
        'INDlyItemScheduleTemplate
        '
        Me.INDlyItemScheduleTemplate.Control = Me.INDsleScheduleTemplate
        Me.INDlyItemScheduleTemplate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemScheduleTemplate.MaxSize = New System.Drawing.Size(0, 69)
        Me.INDlyItemScheduleTemplate.MinSize = New System.Drawing.Size(1, 69)
        Me.INDlyItemScheduleTemplate.Name = "INDlyItemScheduleTemplate"
        Me.INDlyItemScheduleTemplate.Size = New System.Drawing.Size(1014, 69)
        Me.INDlyItemScheduleTemplate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemScheduleTemplate.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemScheduleTemplate.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDDDBOptionsMenu
        Me.LayoutControlItem1.Location = New System.Drawing.Point(1014, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(69, 69)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(69, 69)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(69, 69)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.CtrDateNavigator1
        Me.LayoutControlItem2.Location = New System.Drawing.Point(1083, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(286, 69)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(286, 69)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(286, 69)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDCalendar
        Me.LayoutControlItem3.Location = New System.Drawing.Point(230, 69)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(843, 490)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.Control = Me.INDclbcUsers
        Me.LayoutControlItem4.Location = New System.Drawing.Point(1083, 69)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(286, 0)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(286, 1)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(286, 558)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Usuarios"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(135, 60)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDPcScheduleDetailComplete
        Me.LayoutControlItem8.Location = New System.Drawing.Point(230, 559)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(843, 68)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(843, 68)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(843, 68)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 69)
        Me.EmptySpaceItem2.MaxSize = New System.Drawing.Size(230, 0)
        Me.EmptySpaceItem2.MinSize = New System.Drawing.Size(230, 1)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(230, 558)
        Me.EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(1073, 69)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(10, 558)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmAuthorizationSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1370, 808)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAuthorizationSchedule"
        Me.Opacity = 1.0R
        Me.Tag = "2174"
        Me.Text = "Asignación de Turnos"
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
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDgcDetHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDetHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepIcbeType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcCMore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcCMore.ResumeLayout(False)
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl4.ResumeLayout(False)
        CType(Me.INDPopupMoreGcHoursDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupMoreGvHoursDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemOtherSchDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupGcMatches, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupGvMatches, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgDetailsSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrDetHours, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtrGrMatchPartnerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailScheduleMates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemDetailTemplateHoursNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDPcScheduleDetailComplete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcScheduleDetailComplete.ResumeLayout(False)
        CType(Me.INDPcDetailEm1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcDetailEm1.ResumeLayout(False)
        CType(Me.INDPcCDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcCDetail.ResumeLayout(False)
        CType(Me.INDclbcUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleScheduleTemplate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewSearchPortfolio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemScheduleTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleScheduleTemplate As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewSearchPortfolio As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemScheduleTemplate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDDBOptionsMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrDateNavigator1 As CtrDateNavigator
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCalendar As CtrCalendar
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDclbcUsers As DevExpress.XtraEditors.CheckedListBoxControl
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDPcCDetail As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcDetHours As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDetHours As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEvent As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNextDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPopupLcTxtTemplateHoursNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDPopupSBEdit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDPcCMore As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents LayoutControl4 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDPopupMoreLcTxtOtherSchHoursNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDPopupMoreGcHoursDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDPopupMoreGvHoursDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FunctionalUnitMore As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Template As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPopupMoreLcNumberDay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemOtherSchDet As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupMoreLcEmployee As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDPopupGcMatches As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDPopupGvMatches As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDCOlEmployeeNameDetailMates As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPopupLcTxtTemplate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDPopupLcNumberDay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemDetailDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemDetailTemplate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemDetailEdit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTcgDetailsSchedule As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlyGrDetHours As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyCtrGrMatchPartnerts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemDetailScheduleMates As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemDetailTemplateHoursNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPopupLcEmployee As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBarRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDrepIcbeType As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDPcScheduleDetailComplete As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDPcDetailEm1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDLcTotalHoursNumber1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLcTotalHours1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDDDBEmployee1 As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
End Class
