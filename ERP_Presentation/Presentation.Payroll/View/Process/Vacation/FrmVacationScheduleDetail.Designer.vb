Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVacationScheduleDetail
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnNo = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnYes = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGridDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDgvScheduleDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolLetter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.INDcolNameTemplate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolHours = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolHorary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGroupScheduleDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyControlYes = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyControlNo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup2 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGridDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvScheduleDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupScheduleDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGridDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyControlYes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyControlNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(749, 478)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(749, 94)
        Me.ToolBars.Visible = False
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(749, 94)
        Me.BarraBotones.Visible = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDbtnNo)
        Me.LayoutControl1.Controls.Add(Me.INDbtnYes)
        Me.LayoutControl1.Controls.Add(Me.INDGridDetail)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(637, 381, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(745, 469)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDbtnNo
        '
        Me.INDbtnNo.Location = New System.Drawing.Point(375, 409)
        Me.INDbtnNo.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnNo.Name = "INDbtnNo"
        Me.INDbtnNo.Size = New System.Drawing.Size(346, 36)
        Me.INDbtnNo.StyleController = Me.LayoutControl1
        Me.INDbtnNo.TabIndex = 7
        Me.INDbtnNo.Text = "No"
        '
        'INDbtnYes
        '
        Me.INDbtnYes.Location = New System.Drawing.Point(24, 409)
        Me.INDbtnYes.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnYes.Name = "INDbtnYes"
        Me.INDbtnYes.Size = New System.Drawing.Size(347, 36)
        Me.INDbtnYes.StyleController = Me.LayoutControl1
        Me.INDbtnYes.TabIndex = 6
        Me.INDbtnYes.Text = "Si"
        '
        'INDGridDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGridDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGridDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGridDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGridDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGridDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGridDetail, False)
        Me.INDGridDetail.Location = New System.Drawing.Point(24, 60)
        Me.INDGridDetail.MainView = Me.INDgvScheduleDetail
        Me.INDGridDetail.Name = "INDGridDetail"
        Me.INDGridDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit1})
        Me.INDGridDetail.Size = New System.Drawing.Size(697, 314)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGridDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGridDetail.TabIndex = 4
        Me.INDGridDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvScheduleDetail, Me.GridView1})
        '
        'INDgvScheduleDetail
        '
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.INDgvScheduleDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvScheduleDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvScheduleDetail.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvScheduleDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgvScheduleDetail.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDgvScheduleDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvScheduleDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvScheduleDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvScheduleDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvScheduleDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolDate, Me.INDcolLetter, Me.INDcolNameTemplate, Me.INDcolFunctionalUnit, Me.INDcolHours, Me.INDcolHorary})
        Me.INDgvScheduleDetail.GridControl = Me.INDGridDetail
        Me.INDgvScheduleDetail.GroupCount = 1
        Me.INDgvScheduleDetail.Name = "INDgvScheduleDetail"
        Me.INDgvScheduleDetail.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDgvScheduleDetail.OptionsBehavior.Editable = False
        Me.INDgvScheduleDetail.OptionsCustomization.AllowColumnMoving = False
        Me.INDgvScheduleDetail.OptionsCustomization.AllowFilter = False
        Me.INDgvScheduleDetail.OptionsCustomization.AllowGroup = False
        Me.INDgvScheduleDetail.OptionsCustomization.AllowSort = False
        Me.INDgvScheduleDetail.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvScheduleDetail.OptionsDetail.ShowDetailTabs = False
        Me.INDgvScheduleDetail.OptionsMenu.EnableColumnMenu = False
        Me.INDgvScheduleDetail.OptionsMenu.EnableFooterMenu = False
        Me.INDgvScheduleDetail.OptionsMenu.EnableGroupPanelMenu = False
        Me.INDgvScheduleDetail.OptionsMenu.ShowAutoFilterRowItem = False
        Me.INDgvScheduleDetail.OptionsMenu.ShowDateTimeGroupIntervalItems = False
        Me.INDgvScheduleDetail.OptionsMenu.ShowGroupSortSummaryItems = False
        Me.INDgvScheduleDetail.OptionsMenu.ShowSplitItem = False
        Me.INDgvScheduleDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvScheduleDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvScheduleDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvScheduleDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDgvScheduleDetail.OptionsView.ShowGroupPanel = False
        Me.INDgvScheduleDetail.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDcolFunctionalUnit, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.INDgvScheduleDetail.Tag = 356
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvScheduleDetail, False)
        '
        'INDcolDate
        '
        Me.INDcolDate.AppearanceCell.Options.UseTextOptions = True
        Me.INDcolDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDcolDate.Caption = "Fecha"
        Me.INDcolDate.FieldName = "DateDetail"
        Me.INDcolDate.Name = "INDcolDate"
        Me.INDcolDate.Visible = True
        Me.INDcolDate.VisibleIndex = 0
        Me.INDcolDate.Width = 241
        '
        'INDcolLetter
        '
        Me.INDcolLetter.Caption = " "
        Me.INDcolLetter.ColumnEdit = Me.RepositoryItemPictureEdit1
        Me.INDcolLetter.FieldName = "Letter"
        Me.INDcolLetter.Name = "INDcolLetter"
        Me.INDcolLetter.OptionsColumn.ReadOnly = True
        Me.INDcolLetter.Visible = True
        Me.INDcolLetter.VisibleIndex = 1
        Me.INDcolLetter.Width = 47
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        '
        'INDcolNameTemplate
        '
        Me.INDcolNameTemplate.Caption = "Turno"
        Me.INDcolNameTemplate.FieldName = "ScheduleTemplate.Name"
        Me.INDcolNameTemplate.Name = "INDcolNameTemplate"
        Me.INDcolNameTemplate.Visible = True
        Me.INDcolNameTemplate.VisibleIndex = 2
        Me.INDcolNameTemplate.Width = 468
        '
        'INDcolFunctionalUnit
        '
        Me.INDcolFunctionalUnit.Caption = "Empleado"
        Me.INDcolFunctionalUnit.FieldName = "Employee.ThirdParty.Name"
        Me.INDcolFunctionalUnit.Name = "INDcolFunctionalUnit"
        Me.INDcolFunctionalUnit.Visible = True
        Me.INDcolFunctionalUnit.VisibleIndex = 3
        '
        'INDcolHours
        '
        Me.INDcolHours.AppearanceCell.Options.UseTextOptions = True
        Me.INDcolHours.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDcolHours.Caption = "Horas"
        Me.INDcolHours.FieldName = "TotalNumberHours"
        Me.INDcolHours.Name = "INDcolHours"
        Me.INDcolHours.Visible = True
        Me.INDcolHours.VisibleIndex = 3
        Me.INDcolHours.Width = 148
        '
        'INDcolHorary
        '
        Me.INDcolHorary.Caption = "Horario"
        Me.INDcolHorary.FieldName = "HourDescription"
        Me.INDcolHorary.Name = "INDcolHorary"
        Me.INDcolHorary.Visible = True
        Me.INDcolHorary.VisibleIndex = 4
        Me.INDcolHorary.Width = 488
        '
        'GridView1
        '
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.GridControl = Me.INDGridDetail
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup2.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGroupScheduleDetail})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(745, 469)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGroupScheduleDetail
        '
        Me.INDlyGroupScheduleDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGroupScheduleDetail.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGroupScheduleDetail.AppearanceGroup.Options.UseTextOptions = True
        Me.INDlyGroupScheduleDetail.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlyGroupScheduleDetail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupScheduleDetail.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupScheduleDetail.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup2.SetCampoObligatorio(Me.INDlyGroupScheduleDetail, False)
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupScheduleDetail, False)
        Me.INDlyGroupScheduleDetail.CustomizationFormText = "LayoutControlGroup2"
        Me.INDlyGroupScheduleDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridDetail, Me.INDlyControlYes, Me.INDlyControlNo, Me.EmptySpaceItem1})
        Me.INDlyGroupScheduleDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGroupScheduleDetail.Name = "INDlyGroupScheduleDetail"
        Me.INDlyGroupScheduleDetail.Size = New System.Drawing.Size(725, 449)
        Me.INDlyGroupScheduleDetail.Text = "¿ Esta seguro que desea sobreescribir estos turnos asignados ?"
        '
        'INDlyItemGridDetail
        '
        Me.INDlyItemGridDetail.Control = Me.INDGridDetail
        Me.INDlyItemGridDetail.CustomizationFormText = "LayoutControlItem1"
        Me.INDlyItemGridDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemGridDetail.MinSize = New System.Drawing.Size(104, 24)
        Me.INDlyItemGridDetail.Name = "INDlyItemGridDetail"
        Me.INDlyItemGridDetail.Size = New System.Drawing.Size(701, 318)
        Me.INDlyItemGridDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridDetail.Text = "INDlyItemGridDetail"
        Me.INDlyItemGridDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridDetail.TextToControlDistance = 0
        Me.INDlyItemGridDetail.TextVisible = False
        '
        'INDlyControlYes
        '
        Me.INDlyControlYes.Control = Me.INDbtnYes
        Me.INDlyControlYes.CustomizationFormText = "LayoutControlItem2"
        Me.INDlyControlYes.Location = New System.Drawing.Point(0, 349)
        Me.INDlyControlYes.MaxSize = New System.Drawing.Size(351, 40)
        Me.INDlyControlYes.MinSize = New System.Drawing.Size(351, 40)
        Me.INDlyControlYes.Name = "INDlyControlYes"
        Me.INDlyControlYes.Size = New System.Drawing.Size(351, 40)
        Me.INDlyControlYes.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyControlYes.Text = "INDlyControlYes"
        Me.INDlyControlYes.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyControlYes.TextToControlDistance = 0
        Me.INDlyControlYes.TextVisible = False
        '
        'INDlyControlNo
        '
        Me.INDlyControlNo.Control = Me.INDbtnNo
        Me.INDlyControlNo.CustomizationFormText = "LayoutControlItem3"
        Me.INDlyControlNo.Location = New System.Drawing.Point(351, 349)
        Me.INDlyControlNo.MaxSize = New System.Drawing.Size(350, 40)
        Me.INDlyControlNo.MinSize = New System.Drawing.Size(350, 40)
        Me.INDlyControlNo.Name = "INDlyControlNo"
        Me.INDlyControlNo.Size = New System.Drawing.Size(350, 40)
        Me.INDlyControlNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyControlNo.Text = "INDlyControlNo"
        Me.INDlyControlNo.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyControlNo.TextToControlDistance = 0
        Me.INDlyControlNo.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 318)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(104, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(701, 31)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmVacationScheduleDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 595)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmVacationScheduleDetail"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = ""
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGridDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvScheduleDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupScheduleDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGridDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyControlYes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyControlNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGridDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGroupScheduleDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemGridDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDbtnNo As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDbtnYes As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyControlYes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyControlNo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup2 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgvScheduleDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolLetter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolNameTemplate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolHorary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolHours As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class
