<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopupPermissionsTenant
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
        Me.components = New System.ComponentModel.Container()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PopupPermissionsTenant))
        Me.INDgcvActionPermission = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolAction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolValor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrbgActionValue = New DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup()
        Me.INDgcPermission = New DevExpress.XtraGrid.GridControl()
        Me.INDgcvPermission = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolModule = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolMenu = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolMenuCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolActions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDpceActions = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLycPermissionsTenant = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSmbCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSmbAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnRemoveAllPermissions = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnGiveAllPermissions = New DevExpress.XtraEditors.SimpleButton()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciGiveAllPermissions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRemoveAllPermissions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGcPermission = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCancel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDbarButtonSelectAll = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbarButtonUnSelectAll = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.INDgcvActionPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrbgActionValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcvPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycPermissionsTenant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLycPermissionsTenant.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGiveAllPermissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRemoveAllPermissions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGcPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDgcvActionPermission
        '
        Me.INDgcvActionPermission.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgcvActionPermission.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgcvActionPermission.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgcvActionPermission.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgcvActionPermission.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDgcvActionPermission.Appearance.GroupRow.Options.UseFont = True
        Me.INDgcvActionPermission.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDgcvActionPermission.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcvActionPermission.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgcvActionPermission.Appearance.Row.Options.UseFont = True
        Me.INDgcvActionPermission.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolAction, Me.INDcolValor})
        Me.INDgcvActionPermission.GridControl = Me.INDgcPermission
        Me.INDgcvActionPermission.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.INDgcvActionPermission.Name = "INDgcvActionPermission"
        Me.INDgcvActionPermission.OptionsSelection.MultiSelect = True
        Me.INDgcvActionPermission.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcvActionPermission.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcvActionPermission.OptionsView.ShowAutoFilterRow = True
        Me.INDgcvActionPermission.OptionsView.ShowColumnHeaders = False
        Me.INDgcvActionPermission.OptionsView.ShowGroupPanel = False
        '
        'INDcolAction
        '
        Me.INDcolAction.FieldName = "Name"
        Me.INDcolAction.MaxWidth = 300
        Me.INDcolAction.MinWidth = 300
        Me.INDcolAction.Name = "INDcolAction"
        Me.INDcolAction.OptionsColumn.AllowEdit = False
        Me.INDcolAction.OptionsColumn.AllowFocus = False
        Me.INDcolAction.Visible = True
        Me.INDcolAction.VisibleIndex = 0
        '
        'INDcolValor
        '
        Me.INDcolValor.ColumnEdit = Me.INDrbgActionValue
        Me.INDcolValor.FieldName = "Value"
        Me.INDcolValor.MaxWidth = 100
        Me.INDcolValor.MinWidth = 100
        Me.INDcolValor.Name = "INDcolValor"
        Me.INDcolValor.Visible = True
        Me.INDcolValor.VisibleIndex = 1
        '
        'INDrbgActionValue
        '
        Me.INDrbgActionValue.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDrbgActionValue.Name = "INDrbgActionValue"
        '
        'INDgcPermission
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcPermission, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcPermission, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcPermission, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcPermission, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcPermission, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcPermission, False)
        GridLevelNode1.LevelTemplate = Me.INDgcvActionPermission
        GridLevelNode1.RelationName = "Permissions"
        Me.INDgcPermission.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.INDgcPermission.Location = New System.Drawing.Point(12, 48)
        Me.INDgcPermission.MainView = Me.INDgcvPermission
        Me.INDgcPermission.Name = "INDgcPermission"
        Me.INDgcPermission.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrbgActionValue, Me.INDpceActions})
        Me.INDgcPermission.ShowOnlyPredefinedDetails = True
        Me.INDgcPermission.Size = New System.Drawing.Size(928, 735)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcPermission, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcPermission.TabIndex = 27
        Me.INDgcPermission.Tag = 1242
        Me.INDgcPermission.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcvPermission, Me.INDgcvActionPermission})
        '
        'INDgcvPermission
        '
        Me.INDgcvPermission.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgcvPermission.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgcvPermission.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgcvPermission.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgcvPermission.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgcvPermission.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDgcvPermission.Appearance.GroupRow.Options.UseFont = True
        Me.INDgcvPermission.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.INDgcvPermission.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcvPermission.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgcvPermission.Appearance.Row.Options.UseFont = True
        Me.INDgcvPermission.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgcvPermission.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgcvPermission.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.INDcolModule, Me.GridColumn7, Me.INDcolMenu, Me.INDcolMenuCode, Me.INDcolActions})
        Me.INDgcvPermission.GridControl = Me.INDgcPermission
        Me.INDgcvPermission.GroupCount = 3
        Me.INDgcvPermission.Name = "INDgcvPermission"
        Me.INDgcvPermission.OptionsDetail.ShowDetailTabs = False
        Me.INDgcvPermission.OptionsSelection.MultiSelect = True
        Me.INDgcvPermission.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcvPermission.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcvPermission.OptionsView.ShowAutoFilterRow = True
        Me.INDgcvPermission.OptionsView.ShowGroupPanel = False
        Me.INDgcvPermission.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn1, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDcolModule, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn7, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.INDgcvPermission.Tag = 442
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Grupo"
        Me.GridColumn1.FieldName = "GroupForms"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDcolModule
        '
        Me.INDcolModule.Caption = "Modulo"
        Me.INDcolModule.FieldName = "Module.Name"
        Me.INDcolModule.Name = "INDcolModule"
        Me.INDcolModule.OptionsColumn.AllowEdit = False
        Me.INDcolModule.OptionsColumn.AllowFocus = False
        Me.INDcolModule.Visible = True
        Me.INDcolModule.VisibleIndex = 1
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tipo"
        Me.GridColumn7.FieldName = "TypeName"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        '
        'INDcolMenu
        '
        Me.INDcolMenu.Caption = "Menu"
        Me.INDcolMenu.FieldName = "Name"
        Me.INDcolMenu.Name = "INDcolMenu"
        Me.INDcolMenu.OptionsColumn.AllowEdit = False
        Me.INDcolMenu.OptionsColumn.AllowFocus = False
        Me.INDcolMenu.Visible = True
        Me.INDcolMenu.VisibleIndex = 0
        '
        'INDcolMenuCode
        '
        Me.INDcolMenuCode.AppearanceCell.Options.UseTextOptions = True
        Me.INDcolMenuCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDcolMenuCode.Caption = "Código"
        Me.INDcolMenuCode.FieldName = "Id"
        Me.INDcolMenuCode.Name = "INDcolMenuCode"
        Me.INDcolMenuCode.OptionsColumn.AllowEdit = False
        Me.INDcolMenuCode.OptionsColumn.AllowFocus = False
        Me.INDcolMenuCode.Visible = True
        Me.INDcolMenuCode.VisibleIndex = 1
        '
        'INDcolActions
        '
        Me.INDcolActions.ColumnEdit = Me.INDpceActions
        Me.INDcolActions.MaxWidth = 75
        Me.INDcolActions.MinWidth = 75
        Me.INDcolActions.Name = "INDcolActions"
        '
        'INDpceActions
        '
        Me.INDpceActions.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDpceActions.Name = "INDpceActions"
        Me.INDpceActions.PopupSizeable = False
        Me.INDpceActions.ShowPopupCloseButton = False
        '
        'INDLycPermissionsTenant
        '
        Me.INDLycPermissionsTenant.Controls.Add(Me.INDSmbCancel)
        Me.INDLycPermissionsTenant.Controls.Add(Me.INDSmbAccept)
        Me.INDLycPermissionsTenant.Controls.Add(Me.INDgcPermission)
        Me.INDLycPermissionsTenant.Controls.Add(Me.INDbtnRemoveAllPermissions)
        Me.INDLycPermissionsTenant.Controls.Add(Me.INDbtnGiveAllPermissions)
        Me.INDLycPermissionsTenant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLycPermissionsTenant.Location = New System.Drawing.Point(0, 0)
        Me.INDLycPermissionsTenant.Name = "INDLycPermissionsTenant"
        Me.INDLycPermissionsTenant.Root = Me.Root
        Me.INDLycPermissionsTenant.Size = New System.Drawing.Size(952, 841)
        Me.INDLycPermissionsTenant.TabIndex = 0
        Me.INDLycPermissionsTenant.Text = "LayoutControl1"
        '
        'INDSmbCancel
        '
        Me.INDSmbCancel.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSmbCancel.Appearance.Options.UseFont = True
        Me.INDSmbCancel.Location = New System.Drawing.Point(443, 795)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbCancel, False)
        Me.INDSmbCancel.Name = "INDSmbCancel"
        Me.INDSmbCancel.Size = New System.Drawing.Size(196, 34)
        Me.INDSmbCancel.StyleController = Me.INDLycPermissionsTenant
        Me.INDSmbCancel.TabIndex = 29
        Me.INDSmbCancel.Text = "Cancelar"
        '
        'INDSmbAccept
        '
        Me.INDSmbAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSmbAccept.Appearance.Options.UseFont = True
        Me.INDSmbAccept.Location = New System.Drawing.Point(243, 795)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbAccept, False)
        Me.INDSmbAccept.Name = "INDSmbAccept"
        Me.INDSmbAccept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDSmbAccept.Size = New System.Drawing.Size(196, 34)
        Me.INDSmbAccept.StyleController = Me.INDLycPermissionsTenant
        Me.INDSmbAccept.TabIndex = 28
        Me.INDSmbAccept.Text = "Aceptar"
        '
        'INDbtnRemoveAllPermissions
        '
        Me.INDbtnRemoveAllPermissions.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnRemoveAllPermissions.Appearance.Options.UseFont = True
        Me.INDbtnRemoveAllPermissions.Location = New System.Drawing.Point(220, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnRemoveAllPermissions, False)
        Me.INDbtnRemoveAllPermissions.Name = "INDbtnRemoveAllPermissions"
        Me.INDbtnRemoveAllPermissions.Size = New System.Drawing.Size(188, 32)
        Me.INDbtnRemoveAllPermissions.StyleController = Me.INDLycPermissionsTenant
        Me.INDbtnRemoveAllPermissions.TabIndex = 26
        Me.INDbtnRemoveAllPermissions.Tag = 1242
        Me.INDbtnRemoveAllPermissions.Text = "Quitar todos los permisos"
        '
        'INDbtnGiveAllPermissions
        '
        Me.INDbtnGiveAllPermissions.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnGiveAllPermissions.Appearance.Options.UseFont = True
        Me.INDbtnGiveAllPermissions.Appearance.Options.UseTextOptions = True
        Me.INDbtnGiveAllPermissions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDbtnGiveAllPermissions.Location = New System.Drawing.Point(12, 12)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnGiveAllPermissions, False)
        Me.INDbtnGiveAllPermissions.Name = "INDbtnGiveAllPermissions"
        Me.INDbtnGiveAllPermissions.Size = New System.Drawing.Size(196, 32)
        Me.INDbtnGiveAllPermissions.StyleController = Me.INDLycPermissionsTenant
        Me.INDbtnGiveAllPermissions.TabIndex = 25
        Me.INDbtnGiveAllPermissions.Tag = 1242
        Me.INDbtnGiveAllPermissions.Text = "Dar todos los permisos"
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciGiveAllPermissions, Me.INDLciRemoveAllPermissions, Me.INDLciGcPermission, Me.INDLciAcept, Me.INDLciCancel, Me.EmptySpaceItem1})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(952, 841)
        Me.Root.TextVisible = False
        '
        'INDLciGiveAllPermissions
        '
        Me.INDLciGiveAllPermissions.Control = Me.INDbtnGiveAllPermissions
        Me.INDLciGiveAllPermissions.Location = New System.Drawing.Point(0, 0)
        Me.INDLciGiveAllPermissions.MaxSize = New System.Drawing.Size(200, 36)
        Me.INDLciGiveAllPermissions.MinSize = New System.Drawing.Size(200, 36)
        Me.INDLciGiveAllPermissions.Name = "INDLciGiveAllPermissions"
        Me.INDLciGiveAllPermissions.Size = New System.Drawing.Size(200, 36)
        Me.INDLciGiveAllPermissions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGiveAllPermissions.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGiveAllPermissions.TextVisible = False
        '
        'INDLciRemoveAllPermissions
        '
        Me.INDLciRemoveAllPermissions.Control = Me.INDbtnRemoveAllPermissions
        Me.INDLciRemoveAllPermissions.Location = New System.Drawing.Point(200, 0)
        Me.INDLciRemoveAllPermissions.MaxSize = New System.Drawing.Size(200, 36)
        Me.INDLciRemoveAllPermissions.MinSize = New System.Drawing.Size(200, 36)
        Me.INDLciRemoveAllPermissions.Name = "INDLciRemoveAllPermissions"
        Me.INDLciRemoveAllPermissions.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.INDLciRemoveAllPermissions.Size = New System.Drawing.Size(732, 36)
        Me.INDLciRemoveAllPermissions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRemoveAllPermissions.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciRemoveAllPermissions.TextVisible = False
        '
        'INDLciGcPermission
        '
        Me.INDLciGcPermission.Control = Me.INDgcPermission
        Me.INDLciGcPermission.Location = New System.Drawing.Point(0, 36)
        Me.INDLciGcPermission.Name = "INDLciGcPermission"
        Me.INDLciGcPermission.Size = New System.Drawing.Size(932, 739)
        Me.INDLciGcPermission.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGcPermission.TextVisible = False
        '
        'INDLciAcept
        '
        Me.INDLciAcept.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDLciAcept.Control = Me.INDSmbAccept
        Me.INDLciAcept.Location = New System.Drawing.Point(231, 775)
        Me.INDLciAcept.MaxSize = New System.Drawing.Size(200, 46)
        Me.INDLciAcept.MinSize = New System.Drawing.Size(200, 46)
        Me.INDLciAcept.Name = "INDLciAcept"
        Me.INDLciAcept.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 2)
        Me.INDLciAcept.Size = New System.Drawing.Size(200, 46)
        Me.INDLciAcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAcept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAcept.TextVisible = False
        '
        'INDLciCancel
        '
        Me.INDLciCancel.Control = Me.INDSmbCancel
        Me.INDLciCancel.Location = New System.Drawing.Point(431, 775)
        Me.INDLciCancel.MaxSize = New System.Drawing.Size(200, 46)
        Me.INDLciCancel.MinSize = New System.Drawing.Size(200, 46)
        Me.INDLciCancel.Name = "INDLciCancel"
        Me.INDLciCancel.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 10, 2)
        Me.INDLciCancel.Size = New System.Drawing.Size(501, 46)
        Me.INDLciCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCancel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciCancel.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 775)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(231, 0)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(231, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(231, 46)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDbarButtonSelectAll, Me.INDbarButtonUnSelectAll})
        Me.BarManager.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager
        Me.barDockControlTop.Size = New System.Drawing.Size(952, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 841)
        Me.barDockControlBottom.Manager = Me.BarManager
        Me.barDockControlBottom.Size = New System.Drawing.Size(952, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 841)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(952, 0)
        Me.barDockControlRight.Manager = Me.BarManager
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 841)
        '
        'INDbarButtonSelectAll
        '
        Me.INDbarButtonSelectAll.Caption = "Seleccionar Grupo"
        Me.INDbarButtonSelectAll.Id = 0
        Me.INDbarButtonSelectAll.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectAll.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonSelectAll.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectAll.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonSelectAll.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectAll.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonSelectAll.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectAll.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonSelectAll.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectAll.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonSelectAll.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectAll.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonSelectAll.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectAll.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonSelectAll.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonSelectAll.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonSelectAll.Name = "INDbarButtonSelectAll"
        '
        'INDbarButtonUnSelectAll
        '
        Me.INDbarButtonUnSelectAll.Caption = "Quitar Selección Grupo"
        Me.INDbarButtonUnSelectAll.Id = 1
        Me.INDbarButtonUnSelectAll.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectAll.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonUnSelectAll.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectAll.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonUnSelectAll.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectAll.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonUnSelectAll.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectAll.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonUnSelectAll.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectAll.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonUnSelectAll.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectAll.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonUnSelectAll.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectAll.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonUnSelectAll.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonUnSelectAll.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonUnSelectAll.Name = "INDbarButtonUnSelectAll"
        '
        'PopupMenuActions
        '
        Me.PopupMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonSelectAll), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonUnSelectAll), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonSelectAll), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonUnSelectAll)})
        Me.PopupMenuActions.Manager = Me.BarManager
        Me.PopupMenuActions.Name = "PopupMenuActions"
        '
        'PopupPermissionsTenant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(952, 841)
        Me.ControlBox = False
        Me.Controls.Add(Me.INDLycPermissionsTenant)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.Image = CType(resources.GetObject("PopupPermissionsTenant.IconOptions.Image"), System.Drawing.Image)
        Me.Name = "PopupPermissionsTenant"
        Me.Text = "Permisos por tenant"
        CType(Me.INDgcvActionPermission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrbgActionValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcPermission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcvPermission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycPermissionsTenant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLycPermissionsTenant.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGiveAllPermissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRemoveAllPermissions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGcPermission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDLycPermissionsTenant As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnGiveAllPermissions As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGiveAllPermissions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnRemoveAllPermissions As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciRemoveAllPermissions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcPermission As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcvActionPermission As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolAction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolValor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrbgActionValue As DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup
    Friend WithEvents INDgcvPermission As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDcolModule As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolMenu As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolMenuCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolActions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceActions As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDLciGcPermission As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSmbCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents INDSmbAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciAcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCancel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDbarButtonSelectAll As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbarButtonUnSelectAll As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupMenuActions As DevExpress.XtraBars.PopupMenu
End Class
