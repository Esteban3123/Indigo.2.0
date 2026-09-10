Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBudgetAllocationInitialBalances
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcBills = New DevExpress.XtraGrid.GridControl()
        Me.INDGvBills = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager = New DevExpress.XtraBars.BarManager()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDbarButtonBudget = New DevExpress.XtraBars.BarButtonItem()
        Me.INDDdbMenuActions = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPmActions = New DevExpress.XtraBars.PopupMenu()
        Me.INDBbiProcess = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiUndo = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.SkinBarSubItem1 = New DevExpress.XtraBars.SkinBarSubItem()
        Me.INDSleInitialBalance = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.PopupMenuActions = New DevExpress.XtraBars.PopupMenu()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDGcBills, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvBills, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPmActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleInitialBalance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1256, 512)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1256, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1256, 130)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDGcBills)
        Me.LayoutControl1.Controls.Add(Me.INDDdbMenuActions)
        Me.LayoutControl1.Controls.Add(Me.INDSleInitialBalance)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1252, 503)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDGcBills
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcBills, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcBills, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcBills, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcBills, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcBills, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcBills, False)
        Me.INDGcBills.Location = New System.Drawing.Point(12, 76)
        Me.INDGcBills.MainView = Me.INDGvBills
        Me.INDGcBills.MenuManager = Me.BarManager
        Me.INDGcBills.Name = "INDGcBills"
        Me.INDGcBills.Size = New System.Drawing.Size(1228, 415)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcBills, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcBills.TabIndex = 10
        Me.INDGcBills.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvBills})
        '
        'INDGvBills
        '
        Me.INDGvBills.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvBills.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvBills.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvBills.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvBills.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvBills.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBills.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvBills.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBills.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvBills.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvBills.Appearance.Row.Options.UseFont = True
        Me.INDGvBills.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvBills.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvBills.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn4, Me.GridColumn5, Me.GridColumn7, Me.GridColumn8})
        Me.INDGvBills.GridControl = Me.INDGcBills
        Me.INDGvBills.Name = "INDGvBills"
        Me.INDGvBills.OptionsSelection.MultiSelect = True
        Me.INDGvBills.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvBills.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvBills.OptionsView.ShowAutoFilterRow = True
        Me.INDGvBills.OptionsView.ShowDetailButtons = False
        Me.INDGvBills.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvBills, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tercero"
        Me.GridColumn6.FieldName = "NitNameThird"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        Me.GridColumn6.Width = 325
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Factura"
        Me.GridColumn4.FieldName = "InvoiceNumber"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Fecha"
        Me.GridColumn5.FieldName = "AccountReceivableDate"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Cuenta Contable"
        Me.GridColumn7.FieldName = "NumberNameAccount"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 363
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Valor"
        Me.GridColumn8.DisplayFormat.FormatString = "c0"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "Value"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 4
        Me.GridColumn8.Width = 139
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.BarDockControl1)
        Me.BarManager.DockControls.Add(Me.BarDockControl2)
        Me.BarManager.DockControls.Add(Me.BarDockControl3)
        Me.BarManager.DockControls.Add(Me.BarDockControl4)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDbarButtonBudget})
        Me.BarManager.MaxItemId = 2
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl1.Manager = Me.BarManager
        Me.BarDockControl1.Size = New System.Drawing.Size(1256, 0)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 647)
        Me.BarDockControl2.Manager = Me.BarManager
        Me.BarDockControl2.Size = New System.Drawing.Size(1256, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl3.Manager = Me.BarManager
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 642)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1256, 5)
        Me.BarDockControl4.Manager = Me.BarManager
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 642)
        '
        'INDbarButtonBudget
        '
        Me.INDbarButtonBudget.Caption = "Asignar Presupuesto"
        Me.INDbarButtonBudget.Id = 0
        Me.INDbarButtonBudget.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonBudget.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonBudget.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonBudget.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonBudget.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonBudget.ItemAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonBudget.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonBudget.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonBudget.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonBudget.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.INDbarButtonBudget.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonBudget.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.INDbarButtonBudget.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonBudget.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.INDbarButtonBudget.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 11.25!)
        Me.INDbarButtonBudget.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.INDbarButtonBudget.Name = "INDbarButtonBudget"
        '
        'INDDdbMenuActions
        '
        Me.INDDdbMenuActions.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDdbMenuActions.DropDownControl = Me.INDPmActions
        Me.INDDdbMenuActions.ImageOptions.Image = Global.Presentation.Portfolio.My.Resources.Resources.Mmenu_de_acciones
        Me.INDDdbMenuActions.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDDdbMenuActions.Location = New System.Drawing.Point(1175, 12)
        Me.INDDdbMenuActions.MenuManager = Me.BarManager1
        Me.INDDdbMenuActions.Name = "INDDdbMenuActions"
        Me.INDDdbMenuActions.Size = New System.Drawing.Size(65, 60)
        Me.INDDdbMenuActions.StyleController = Me.LayoutControl1
        Me.INDDdbMenuActions.TabIndex = 9
        Me.INDDdbMenuActions.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'INDPmActions
        '
        Me.INDPmActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.INDBbiProcess, DevExpress.XtraBars.BarItemPaintStyle.Standard), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiUndo)})
        Me.INDPmActions.Manager = Me.BarManager1
        Me.INDPmActions.Name = "INDPmActions"
        '
        'INDBbiProcess
        '
        Me.INDBbiProcess.Caption = "Procesar"
        Me.INDBbiProcess.Id = 1
        Me.INDBbiProcess.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
        Me.INDBbiProcess.Name = "INDBbiProcess"
        Me.INDBbiProcess.ShortcutKeyDisplayString = "Ctrl+S"
        '
        'INDBbiUndo
        '
        Me.INDBbiUndo.Caption = "Deshacer"
        Me.INDBbiUndo.Id = 2
        Me.INDBbiUndo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
        Me.INDBbiUndo.Name = "INDBbiUndo"
        Me.INDBbiUndo.ShortcutKeyDisplayString = "Ctrl+U"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.SkinBarSubItem1, Me.INDBbiProcess, Me.INDBbiUndo})
        Me.BarManager1.MaxItemId = 3
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1256, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 647)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1256, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 642)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1256, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 642)
        '
        'SkinBarSubItem1
        '
        Me.SkinBarSubItem1.Caption = "SkinBarSubItem1"
        Me.SkinBarSubItem1.Id = 0
        Me.SkinBarSubItem1.Name = "SkinBarSubItem1"
        '
        'INDSleInitialBalance
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleInitialBalance, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleInitialBalance, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleInitialBalance, False)
        Me.INDSleInitialBalance.Location = New System.Drawing.Point(12, 12)
        Me.INDSleInitialBalance.MaximumSize = New System.Drawing.Size(0, 60)
        Me.INDSleInitialBalance.MinimumSize = New System.Drawing.Size(0, 60)
        Me.INDSleInitialBalance.Name = "INDSleInitialBalance"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleInitialBalance, False)
        Me.INDSleInitialBalance.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleInitialBalance.Properties.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleInitialBalance.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleInitialBalance.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 28.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleInitialBalance.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleInitialBalance.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleInitialBalance.Properties.Appearance.Options.UseBorderColor = True
        Me.INDSleInitialBalance.Properties.Appearance.Options.UseFont = True
        Me.INDSleInitialBalance.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleInitialBalance.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleInitialBalance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDSleInitialBalance.Properties.DisplayMember = "Code"
        Me.INDSleInitialBalance.Properties.NullText = "Seleccione un Saldo Inicial"
        Me.INDSleInitialBalance.Properties.PopupSizeable = False
        Me.INDSleInitialBalance.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleInitialBalance.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDSleInitialBalance.Properties.ShowClearButton = False
        Me.INDSleInitialBalance.Properties.ShowFooter = False
        Me.INDSleInitialBalance.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleInitialBalance, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleInitialBalance, True)
        Me.INDSleInitialBalance.Size = New System.Drawing.Size(1159, 60)
        Me.INDSleInitialBalance.StyleController = Me.LayoutControl1
        Me.INDSleInitialBalance.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleInitialBalance, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleInitialBalance, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleInitialBalance, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleInitialBalance, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 470
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Fecha"
        Me.GridColumn2.FieldName = "DocumentDate"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 553
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Presupuesto Asignado a Facturas"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.GridColumn3.FieldName = "AllBudgetAssigned"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 369
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Si", CType(1, Byte), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("No", CType(0, Byte), -1)})
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1252, 503)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSleInitialBalance
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1163, 64)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDDdbMenuActions
        Me.LayoutControlItem2.Location = New System.Drawing.Point(1163, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(69, 64)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(69, 64)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(69, 64)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDGcBills
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(1232, 419)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PopupMenuActions
        '
        Me.PopupMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbarButtonBudget)})
        Me.PopupMenuActions.Manager = Me.BarManager
        Me.PopupMenuActions.Name = "PopupMenuActions"
        '
        'FrmBudgetAllocationInitialBalances
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1256, 647)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmBudgetAllocationInitialBalances"
        Me.Opacity = 1.0R
        Me.Tag = "1675"
        Me.Text = "Asignación Rubro Saldos Iniciales"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
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
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDGcBills, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvBills, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPmActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleInitialBalance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleInitialBalance As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPmActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiProcess As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiUndo As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents SkinBarSubItem1 As DevExpress.XtraBars.SkinBarSubItem
    Friend WithEvents INDDdbMenuActions As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcBills As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvBills As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDbarButtonBudget As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenuActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
End Class
