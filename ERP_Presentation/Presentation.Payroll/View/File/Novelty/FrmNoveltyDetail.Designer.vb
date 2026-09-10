Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNoveltyDetail
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlbTitleNoveltyExisting = New DevExpress.XtraEditors.LabelControl()
        Me.INDbtnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGridDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDgvScheduleDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolTypeNovelty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolReason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolRealDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolEndDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGroupScheduleDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGridDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyControlCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyControlNoveltyExisting = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup2 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
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
        CType(Me.INDlyControlCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyControlNoveltyExisting, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.LayoutControl1.Controls.Add(Me.INDlbTitleNoveltyExisting)
        Me.LayoutControl1.Controls.Add(Me.INDbtnCancel)
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
        'INDlbTitleNoveltyExisting
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlbTitleNoveltyExisting, False)
        Me.INDlbTitleNoveltyExisting.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 18.0!, System.Drawing.FontStyle.Bold)
        Me.INDlbTitleNoveltyExisting.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlbTitleNoveltyExisting.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlbTitleNoveltyExisting.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlbTitleNoveltyExisting, False)
        Me.INDlbTitleNoveltyExisting.Location = New System.Drawing.Point(12, 12)
        Me.INDlbTitleNoveltyExisting.Name = "INDlbTitleNoveltyExisting"
        Me.INDlbTitleNoveltyExisting.Size = New System.Drawing.Size(721, 32)
        Me.INDlbTitleNoveltyExisting.StyleController = Me.LayoutControl1
        Me.INDlbTitleNoveltyExisting.TabIndex = 9
        Me.INDlbTitleNoveltyExisting.Text = "Ya existe novedades en el rango de fechas seleccionada"
        '
        'INDbtnCancel
        '
        Me.INDbtnCancel.Location = New System.Drawing.Point(24, 349)
        Me.INDbtnCancel.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDbtnCancel.Name = "INDbtnCancel"
        Me.INDbtnCancel.Size = New System.Drawing.Size(697, 36)
        Me.INDbtnCancel.StyleController = Me.LayoutControl1
        Me.INDbtnCancel.TabIndex = 8
        Me.INDbtnCancel.Text = "Cancelar"
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
        Me.INDGridDetail.Size = New System.Drawing.Size(697, 285)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGridDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.IndigoGridControl1.SetSizeLayoutItem(Me.INDGridDetail, New System.Drawing.Size(701, 0))
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
        Me.INDgvScheduleDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolTypeNovelty, Me.INDcolReason, Me.INDcolRealDate, Me.INDcolEndDate})
        Me.INDgvScheduleDetail.GridControl = Me.INDGridDetail
        Me.INDgvScheduleDetail.Name = "INDgvScheduleDetail"
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
        Me.INDgvScheduleDetail.Tag = 356
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvScheduleDetail, False)
        '
        'INDcolTypeNovelty
        '
        Me.INDcolTypeNovelty.Caption = "Tipo Novedad"
        Me.INDcolTypeNovelty.FieldName = "TypeNovelty"
        Me.INDcolTypeNovelty.Name = "INDcolTypeNovelty"
        Me.INDcolTypeNovelty.Visible = True
        Me.INDcolTypeNovelty.VisibleIndex = 0
        Me.INDcolTypeNovelty.Width = 245
        '
        'INDcolReason
        '
        Me.INDcolReason.Caption = "Descripción"
        Me.INDcolReason.FieldName = "Reason"
        Me.INDcolReason.Name = "INDcolReason"
        Me.INDcolReason.Visible = True
        Me.INDcolReason.VisibleIndex = 1
        Me.INDcolReason.Width = 446
        '
        'INDcolRealDate
        '
        Me.INDcolRealDate.Caption = "Fecha Inicio"
        Me.INDcolRealDate.FieldName = "RealDate"
        Me.INDcolRealDate.Name = "INDcolRealDate"
        Me.INDcolRealDate.Visible = True
        Me.INDcolRealDate.VisibleIndex = 2
        Me.INDcolRealDate.Width = 381
        '
        'INDcolEndDate
        '
        Me.INDcolEndDate.Caption = "Fecha Fin"
        Me.INDcolEndDate.FieldName = "EndDate"
        Me.INDcolEndDate.Name = "INDcolEndDate"
        Me.INDcolEndDate.Visible = True
        Me.INDcolEndDate.VisibleIndex = 3
        Me.INDcolEndDate.Width = 320
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGroupScheduleDetail, Me.INDlyControlNoveltyExisting})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(745, 469)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGroupScheduleDetail
        '
        Me.INDlyGroupScheduleDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlyGroupScheduleDetail.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
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
        Me.INDlyGroupScheduleDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGridDetail, Me.INDlyControlCancel})
        Me.INDlyGroupScheduleDetail.Location = New System.Drawing.Point(0, 36)
        Me.INDlyGroupScheduleDetail.Name = "INDlyGroupScheduleDetail"
        Me.INDlyGroupScheduleDetail.Size = New System.Drawing.Size(725, 413)
        Me.INDlyGroupScheduleDetail.Text = "Turnos Existentes"
        Me.INDlyGroupScheduleDetail.TextVisible = False
        '
        'INDlyItemGridDetail
        '
        Me.INDlyItemGridDetail.Control = Me.INDGridDetail
        Me.INDlyItemGridDetail.CustomizationFormText = "LayoutControlItem1"
        Me.INDlyItemGridDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemGridDetail.MaxSize = New System.Drawing.Size(701, 289)
        Me.INDlyItemGridDetail.MinSize = New System.Drawing.Size(701, 289)
        Me.INDlyItemGridDetail.Name = "INDlyItemGridDetail"
        Me.INDlyItemGridDetail.Size = New System.Drawing.Size(701, 289)
        Me.INDlyItemGridDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGridDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGridDetail.TextVisible = False
        '
        'INDlyControlCancel
        '
        Me.INDlyControlCancel.Control = Me.INDbtnCancel
        Me.INDlyControlCancel.CustomizationFormText = "LayoutControlItem4"
        Me.INDlyControlCancel.Location = New System.Drawing.Point(0, 289)
        Me.INDlyControlCancel.Name = "INDlyControlCancel"
        Me.INDlyControlCancel.Size = New System.Drawing.Size(701, 100)
        Me.INDlyControlCancel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyControlCancel.TextVisible = False
        '
        'INDlyControlNoveltyExisting
        '
        Me.INDlyControlNoveltyExisting.Control = Me.INDlbTitleNoveltyExisting
        Me.INDlyControlNoveltyExisting.CustomizationFormText = "LayoutControlItem5"
        Me.INDlyControlNoveltyExisting.Location = New System.Drawing.Point(0, 0)
        Me.INDlyControlNoveltyExisting.Name = "INDlyControlNoveltyExisting"
        Me.INDlyControlNoveltyExisting.Size = New System.Drawing.Size(725, 36)
        Me.INDlyControlNoveltyExisting.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyControlNoveltyExisting.TextVisible = False
        '
        'FrmNoveltyDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 595)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmNoveltyDetail"
        Me.Opacity = 1.0R
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
        CType(Me.INDlyControlCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyControlNoveltyExisting, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents INDbtnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyControlCancel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlbTitleNoveltyExisting As DevExpress.XtraEditors.LabelControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDlyControlNoveltyExisting As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup2 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgvScheduleDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolTypeNovelty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolReason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolRealDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolEndDate As DevExpress.XtraGrid.Columns.GridColumn
End Class
