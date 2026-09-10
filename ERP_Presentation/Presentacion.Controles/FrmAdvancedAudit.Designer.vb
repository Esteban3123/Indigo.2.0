<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdvancedAudit
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDAuditDetalGc = New Presentation.Controls.GridControlXpoErrors()
        Me.INDGridViewDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlbZoom = New System.Windows.Forms.Label()
        Me.CtrNavigation1 = New Presentation.Controls.CtrNavigation()
        Me.INDAuditCGc = New Presentation.Controls.GridControlXpoErrors()
        Me.INDAuditCGv = New Presentation.Controls.GridViewXpoErrors()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ActionCCol = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepositoryDetailBte = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRecordDeleteSmb = New DevExpress.XtraEditors.SimpleButton()
        Me.INDRecordCreateUpdateSmb = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlycGroupAuditDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlycGroupAuditAdvanced = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDAuditDetalGc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGridViewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDAuditCGc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDAuditCGv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepositoryDetailBte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycGroupAuditDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycGroupAuditAdvanced, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.INDAuditDetalGc
        Me.GridView1.Name = "GridView1"
        '
        'INDAuditDetalGc
        '
        Me.INDAuditDetalGc.Location = New System.Drawing.Point(1012, 142)
        Me.INDAuditDetalGc.MainView = Me.INDGridViewDetail
        Me.INDAuditDetalGc.Name = "INDAuditDetalGc"
        Me.INDAuditDetalGc.Size = New System.Drawing.Size(936, 550)
        Me.INDAuditDetalGc.TabIndex = 9
        Me.INDAuditDetalGc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGridViewDetail, Me.GridView2, Me.GridView1})
        '
        'INDGridViewDetail
        '
        Me.INDGridViewDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGridViewDetail.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGridViewDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGridViewDetail.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDGridViewDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGridViewDetail.Appearance.Row.Options.UseFont = True
        Me.INDGridViewDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGridViewDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGridViewDetail.GridControl = Me.INDAuditDetalGc
        Me.INDGridViewDetail.Name = "INDGridViewDetail"
        Me.INDGridViewDetail.OptionsBehavior.Editable = False
        Me.INDGridViewDetail.OptionsCustomization.AllowColumnMoving = False
        Me.INDGridViewDetail.OptionsCustomization.AllowColumnResizing = False
        Me.INDGridViewDetail.OptionsCustomization.AllowFilter = False
        Me.INDGridViewDetail.OptionsCustomization.AllowGroup = False
        Me.INDGridViewDetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.INDGridViewDetail.OptionsCustomization.AllowSort = False
        Me.INDGridViewDetail.OptionsFilter.AllowColumnMRUFilterList = False
        Me.INDGridViewDetail.OptionsFilter.AllowFilterEditor = False
        Me.INDGridViewDetail.OptionsFilter.AllowFilterIncrementalSearch = False
        Me.INDGridViewDetail.OptionsMenu.EnableColumnMenu = False
        Me.INDGridViewDetail.OptionsMenu.EnableFooterMenu = False
        Me.INDGridViewDetail.OptionsMenu.EnableGroupPanelMenu = False
        Me.INDGridViewDetail.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.INDAuditDetalGc
        Me.GridView2.Name = "GridView2"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomizationMenu = False
        Me.LayoutControl1.Controls.Add(Me.INDlbZoom)
        Me.LayoutControl1.Controls.Add(Me.CtrNavigation1)
        Me.LayoutControl1.Controls.Add(Me.INDAuditDetalGc)
        Me.LayoutControl1.Controls.Add(Me.INDAuditCGc)
        Me.LayoutControl1.Controls.Add(Me.INDRecordDeleteSmb)
        Me.LayoutControl1.Controls.Add(Me.INDRecordCreateUpdateSmb)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1032, 733)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDlbZoom
        '
        Me.INDlbZoom.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.INDlbZoom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlbZoom.Location = New System.Drawing.Point(1116, 60)
        Me.INDlbZoom.Name = "INDlbZoom"
        Me.INDlbZoom.Size = New System.Drawing.Size(832, 78)
        Me.INDlbZoom.TabIndex = 11
        Me.INDlbZoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CtrNavigation1
        '
        Me.CtrNavigation1.Location = New System.Drawing.Point(1012, 60)
        Me.CtrNavigation1.Name = "CtrNavigation1"
        Me.CtrNavigation1.Size = New System.Drawing.Size(100, 78)
        Me.CtrNavigation1.TabIndex = 10
        '
        'INDAuditCGc
        '
        Me.INDAuditCGc.Location = New System.Drawing.Point(36, 212)
        Me.INDAuditCGc.MainView = Me.INDAuditCGv
        Me.INDAuditCGc.Name = "INDAuditCGc"
        Me.INDAuditCGc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRepositoryDetailBte})
        Me.INDAuditCGc.Size = New System.Drawing.Size(936, 468)
        Me.INDAuditCGc.TabIndex = 8
        Me.INDAuditCGc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDAuditCGv})
        '
        'INDAuditCGv
        '
        Me.INDAuditCGv.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDAuditCGv.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDAuditCGv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDAuditCGv.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDAuditCGv.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDAuditCGv.Appearance.Row.Options.UseFont = True
        Me.INDAuditCGv.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDAuditCGv.Appearance.ViewCaption.Options.UseFont = True
        Me.INDAuditCGv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.ActionCCol, Me.GridColumn5, Me.GridColumn3})
        Me.INDAuditCGv.GridControl = Me.INDAuditCGc
        Me.INDAuditCGv.GroupCount = 2
        Me.INDAuditCGv.Name = "INDAuditCGv"
        Me.INDAuditCGv.OptionsCustomization.AllowColumnMoving = False
        Me.INDAuditCGv.OptionsCustomization.AllowColumnResizing = False
        Me.INDAuditCGv.OptionsCustomization.AllowFilter = False
        Me.INDAuditCGv.OptionsCustomization.AllowGroup = False
        Me.INDAuditCGv.OptionsCustomization.AllowQuickHideColumns = False
        Me.INDAuditCGv.OptionsCustomization.AllowSort = False
        Me.INDAuditCGv.OptionsDetail.AutoZoomDetail = True
        Me.INDAuditCGv.OptionsFilter.AllowColumnMRUFilterList = False
        Me.INDAuditCGv.OptionsFilter.AllowFilterEditor = False
        Me.INDAuditCGv.OptionsFind.AllowFindPanel = False
        Me.INDAuditCGv.OptionsMenu.EnableColumnMenu = False
        Me.INDAuditCGv.OptionsMenu.EnableFooterMenu = False
        Me.INDAuditCGv.OptionsMenu.EnableGroupPanelMenu = False
        Me.INDAuditCGv.OptionsView.ShowGroupPanel = False
        Me.INDAuditCGv.OptionsView.ShowIndicator = False
        Me.INDAuditCGv.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.ActionCCol, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Usuario"
        Me.GridColumn1.FieldName = "Usuario"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Fecha"
        Me.GridColumn2.DisplayFormat.FormatString = "g"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn2.FieldName = "Fecha"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'ActionCCol
        '
        Me.ActionCCol.Caption = "Operación"
        Me.ActionCCol.FieldName = "Accion"
        Me.ActionCCol.Name = "ActionCCol"
        Me.ActionCCol.OptionsColumn.AllowEdit = False
        Me.ActionCCol.OptionsColumn.AllowFocus = False
        Me.ActionCCol.Visible = True
        Me.ActionCCol.VisibleIndex = 2
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Ver Detalle"
        Me.GridColumn5.ColumnEdit = Me.INDRepositoryDetailBte
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.FixedWidth = True
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'INDRepositoryDetailBte
        '
        Me.INDRepositoryDetailBte.AutoHeight = False
        Me.INDRepositoryDetailBte.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
        Me.INDRepositoryDetailBte.Name = "INDRepositoryDetailBte"
        Me.INDRepositoryDetailBte.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Entidad"
        Me.GridColumn3.FieldName = "Entidad"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'INDRecordDeleteSmb
        '
        Me.INDRecordDeleteSmb.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDRecordDeleteSmb.Appearance.Options.UseFont = True
        Me.INDRecordDeleteSmb.Appearance.Options.UseTextOptions = True
        Me.INDRecordDeleteSmb.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDRecordDeleteSmb.Location = New System.Drawing.Point(36, 112)
        Me.INDRecordDeleteSmb.MaximumSize = New System.Drawing.Size(0, 36)
        Me.INDRecordDeleteSmb.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDRecordDeleteSmb.Name = "INDRecordDeleteSmb"
        Me.INDRecordDeleteSmb.Size = New System.Drawing.Size(936, 36)
        Me.INDRecordDeleteSmb.StyleController = Me.LayoutControl1
        Me.INDRecordDeleteSmb.TabIndex = 7
        Me.INDRecordDeleteSmb.Text = "Registros de Eliminación   ▼"
        '
        'INDRecordCreateUpdateSmb
        '
        Me.INDRecordCreateUpdateSmb.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDRecordCreateUpdateSmb.Appearance.Options.UseFont = True
        Me.INDRecordCreateUpdateSmb.Appearance.Options.UseTextOptions = True
        Me.INDRecordCreateUpdateSmb.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDRecordCreateUpdateSmb.Location = New System.Drawing.Point(36, 72)
        Me.INDRecordCreateUpdateSmb.MaximumSize = New System.Drawing.Size(0, 36)
        Me.INDRecordCreateUpdateSmb.MinimumSize = New System.Drawing.Size(0, 36)
        Me.INDRecordCreateUpdateSmb.Name = "INDRecordCreateUpdateSmb"
        Me.INDRecordCreateUpdateSmb.Size = New System.Drawing.Size(936, 36)
        Me.INDRecordCreateUpdateSmb.StyleController = Me.LayoutControl1
        Me.INDRecordCreateUpdateSmb.TabIndex = 6
        Me.INDRecordCreateUpdateSmb.Text = "Registros de Creación y Modificación   ▼"
        Me.INDRecordCreateUpdateSmb.Visible = False
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlycGroupAuditDetail, Me.INDlycGroupAuditAdvanced})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1972, 716)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlycGroupAuditDetail
        '
        Me.INDlycGroupAuditDetail.CustomizationFormText = "Detalle Auditoria Avanzada"
        Me.INDlycGroupAuditDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem5, Me.LayoutControlItem6})
        Me.INDlycGroupAuditDetail.Location = New System.Drawing.Point(988, 0)
        Me.INDlycGroupAuditDetail.Name = "INDlycGroupAuditDetail"
        Me.INDlycGroupAuditDetail.Size = New System.Drawing.Size(964, 696)
        Me.INDlycGroupAuditDetail.Text = "Detalle Auditoria Avanzada"
        Me.INDlycGroupAuditDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDAuditDetalGc
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 82)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(940, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(940, 554)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.CtrNavigation1
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(104, 82)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDlbZoom
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(104, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(836, 82)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'INDlycGroupAuditAdvanced
        '
        Me.INDlycGroupAuditAdvanced.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycGroupAuditAdvanced.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycGroupAuditAdvanced.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycGroupAuditAdvanced.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycGroupAuditAdvanced.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlycGroupAuditAdvanced.CustomizationFormText = "LayoutControlGroup4"
        Me.INDlycGroupAuditAdvanced.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3, Me.LayoutControlGroup2})
        Me.INDlycGroupAuditAdvanced.Location = New System.Drawing.Point(0, 0)
        Me.INDlycGroupAuditAdvanced.Name = "INDlycGroupAuditAdvanced"
        Me.INDlycGroupAuditAdvanced.Size = New System.Drawing.Size(988, 696)
        Me.INDlycGroupAuditAdvanced.Text = "INDlycGroupAuditAdvanced"
        Me.INDlycGroupAuditAdvanced.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 140)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(964, 532)
        Me.LayoutControlGroup3.Text = "Registros de Creación y Modificación"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDAuditCGc
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(940, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(940, 472)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup2.CustomizationFormText = "REGISTROS DE AUDITORIA AVANZADA"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem3})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(964, 140)
        Me.LayoutControlGroup2.Text = "Registros de Auditoria Avanzada"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDRecordDeleteSmb
        Me.LayoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 40)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(1, 40)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(940, 40)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDRecordCreateUpdateSmb
        Me.LayoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(1, 40)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(940, 40)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        Me.LayoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'FrmAdvancedAudit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1032, 733)
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAdvancedAudit"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Auditoria Avanzada"
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDAuditDetalGc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGridViewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDAuditCGc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDAuditCGv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepositoryDetailBte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycGroupAuditDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycGroupAuditAdvanced, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDRecordDeleteSmb As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDRecordCreateUpdateSmb As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlycGroupAuditAdvanced As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDAuditCGc As Presentation.Controls.GridControlXpoErrors
    Friend WithEvents INDAuditCGv As Presentation.Controls.GridViewXpoErrors
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ActionCCol As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDAuditDetalGc As Presentation.Controls.GridControlXpoErrors
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDRepositoryDetailBte As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CtrNavigation1 As Presentation.Controls.CtrNavigation
    Friend WithEvents INDlycGroupAuditDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGridViewDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlbZoom As System.Windows.Forms.Label
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
End Class
