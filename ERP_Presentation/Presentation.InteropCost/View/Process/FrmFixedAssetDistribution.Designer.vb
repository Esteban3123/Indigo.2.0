Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFixedAssetDistribution
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetDistribution))
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpopupExport = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyExport = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcExport = New DevExpress.XtraGrid.GridControl()
        Me.INDviewExport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepValue = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.MBtnAddProductionCenter = New DevExpress.XtraBars.BarButtonItem()
        Me.MBtnRemoveProductionCenter = New DevExpress.XtraBars.BarButtonItem()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemExport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.DdbActions = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupMenuProductionCenter = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDpccAddProductionCenter = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtMaximumAmount = New DevExpress.XtraEditors.TextEdit()
        Me.INDsbAddProductionCenter = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleProductionCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliProductionCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAmount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpceAddDetail = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDgcDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDgvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrpsleProductionCenter = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrpspnValue = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleFixetAsset = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvFixedAsset = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliFixetAsset = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcRoot.SuspendLayout()
        CType(Me.INDpopupExport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupExport.SuspendLayout()
        CType(Me.INDlyExport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyExport.SuspendLayout()
        CType(Me.INDgcExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenuProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccAddProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccAddProductionCenter.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtxtMaximumAmount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleProductionCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceAddDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrpsleProductionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrpspnValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleFixetAsset.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvFixedAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliFixetAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1112, 456)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        Me.ToolBars.Size = New System.Drawing.Size(1112, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1112, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 447)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcRoot
        '
        Me.INDlcRoot.Controls.Add(Me.INDpopupExport)
        Me.INDlcRoot.Controls.Add(Me.DdbActions)
        Me.INDlcRoot.Controls.Add(Me.INDpccAddProductionCenter)
        Me.INDlcRoot.Controls.Add(Me.INDpceAddDetail)
        Me.INDlcRoot.Controls.Add(Me.INDgcDetail)
        Me.INDlcRoot.Controls.Add(Me.INDsleFixetAsset)
        Me.INDlcRoot.Controls.Add(Me.INDtxtDescription)
        Me.INDlcRoot.Controls.Add(Me.INDbteCode)
        Me.INDlcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlcRoot.Name = "INDlcRoot"
        Me.INDlcRoot.Root = Me.INDlcgRoot
        Me.INDlcRoot.Size = New System.Drawing.Size(908, 447)
        Me.INDlcRoot.TabIndex = 1
        Me.INDlcRoot.Text = "LayoutControl1"
        '
        'INDpopupExport
        '
        Me.INDpopupExport.Controls.Add(Me.INDlyExport)
        Me.INDpopupExport.Location = New System.Drawing.Point(582, 263)
        Me.INDpopupExport.Name = "INDpopupExport"
        Me.INDpopupExport.Size = New System.Drawing.Size(475, 216)
        Me.INDpopupExport.TabIndex = 19
        '
        'INDlyExport
        '
        Me.INDlyExport.Controls.Add(Me.INDgcExport)
        Me.INDlyExport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyExport.Location = New System.Drawing.Point(0, 0)
        Me.INDlyExport.Name = "INDlyExport"
        Me.INDlyExport.Root = Me.LayoutControlGroup4
        Me.INDlyExport.Size = New System.Drawing.Size(475, 216)
        Me.INDlyExport.TabIndex = 0
        Me.INDlyExport.Text = "LayoutControl1"
        '
        'INDgcExport
        '
        Me.INDgcExport.Location = New System.Drawing.Point(12, 12)
        Me.INDgcExport.MainView = Me.INDviewExport
        Me.INDgcExport.MenuManager = Me.BarManager1
        Me.INDgcExport.Name = "INDgcExport"
        Me.INDgcExport.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepValue})
        Me.INDgcExport.Size = New System.Drawing.Size(451, 192)
        Me.INDgcExport.TabIndex = 4
        Me.INDgcExport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewExport})
        '
        'INDviewExport
        '
        Me.INDviewExport.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewExport.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewExport.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewExport.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewExport.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewExport.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewExport.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewExport.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewExport.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewExport.Appearance.Row.Options.UseFont = True
        Me.INDviewExport.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewExport.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewExport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13})
        Me.INDviewExport.GridControl = Me.INDgcExport
        Me.INDviewExport.Name = "INDviewExport"
        Me.INDviewExport.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewExport.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewExport.OptionsView.ShowAutoFilterRow = True
        Me.INDviewExport.OptionsView.ShowDetailButtons = False
        Me.INDviewExport.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewExport, False)
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Activo"
        Me.GridColumn10.FieldName = "Activo"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Valor"
        Me.GridColumn11.ColumnEdit = Me.INDrepValue
        Me.GridColumn11.FieldName = "Valor"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        '
        'INDrepValue
        '
        Me.INDrepValue.AutoHeight = False
        Me.INDrepValue.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepValue.Mask.EditMask = "C0"
        Me.INDrepValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrepValue.Name = "INDrepValue"
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Centro Costo"
        Me.GridColumn12.FieldName = "CentroCosto"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 2
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Centro Producción"
        Me.GridColumn13.FieldName = "CentroProduccion"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 3
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.MBtnAddProductionCenter, Me.MBtnRemoveProductionCenter})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Size = New System.Drawing.Size(1112, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 574)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1112, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 569)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1112, 5)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 569)
        '
        'MBtnAddProductionCenter
        '
        Me.MBtnAddProductionCenter.Caption = "Agregar Centros"
        Me.MBtnAddProductionCenter.Glyph = Global.Presentation.InteropCost.My.Resources.Resources.Iconos_Crystal_24x24_Usuarios___12_
        Me.MBtnAddProductionCenter.Id = 0
        Me.MBtnAddProductionCenter.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddProductionCenter.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnAddProductionCenter.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddProductionCenter.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnAddProductionCenter.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddProductionCenter.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnAddProductionCenter.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddProductionCenter.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnAddProductionCenter.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddProductionCenter.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnAddProductionCenter.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddProductionCenter.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnAddProductionCenter.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddProductionCenter.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnAddProductionCenter.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnAddProductionCenter.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnAddProductionCenter.Name = "MBtnAddProductionCenter"
        '
        'MBtnRemoveProductionCenter
        '
        Me.MBtnRemoveProductionCenter.Caption = "Remover Centros"
        Me.MBtnRemoveProductionCenter.Glyph = Global.Presentation.InteropCost.My.Resources.Resources.Iconos_Crystal_24x24_Usuarios___10_
        Me.MBtnRemoveProductionCenter.Id = 1
        Me.MBtnRemoveProductionCenter.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveProductionCenter.ItemAppearance.Disabled.Options.UseFont = True
        Me.MBtnRemoveProductionCenter.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveProductionCenter.ItemAppearance.Hovered.Options.UseFont = True
        Me.MBtnRemoveProductionCenter.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveProductionCenter.ItemAppearance.Normal.Options.UseFont = True
        Me.MBtnRemoveProductionCenter.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveProductionCenter.ItemAppearance.Pressed.Options.UseFont = True
        Me.MBtnRemoveProductionCenter.ItemInMenuAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveProductionCenter.ItemInMenuAppearance.Disabled.Options.UseFont = True
        Me.MBtnRemoveProductionCenter.ItemInMenuAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveProductionCenter.ItemInMenuAppearance.Hovered.Options.UseFont = True
        Me.MBtnRemoveProductionCenter.ItemInMenuAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveProductionCenter.ItemInMenuAppearance.Normal.Options.UseFont = True
        Me.MBtnRemoveProductionCenter.ItemInMenuAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 10.25!)
        Me.MBtnRemoveProductionCenter.ItemInMenuAppearance.Pressed.Options.UseFont = True
        Me.MBtnRemoveProductionCenter.Name = "MBtnRemoveProductionCenter"
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemExport})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(475, 216)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'INDlyItemExport
        '
        Me.INDlyItemExport.Control = Me.INDgcExport
        Me.INDlyItemExport.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemExport.Name = "INDlyItemExport"
        Me.INDlyItemExport.Size = New System.Drawing.Size(455, 196)
        Me.INDlyItemExport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemExport.TextVisible = False
        '
        'DdbActions
        '
        Me.DdbActions.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.DdbActions.Appearance.Options.UseBackColor = True
        Me.DdbActions.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DdbActions.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.DdbActions.DropDownControl = Me.PopupMenuProductionCenter
        Me.DdbActions.Image = CType(resources.GetObject("DdbActions.Image"), System.Drawing.Image)
        Me.DdbActions.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.DdbActions.Location = New System.Drawing.Point(1218, 59)
        Me.DdbActions.Margin = New System.Windows.Forms.Padding(0)
        Me.DdbActions.Name = "DdbActions"
        Me.DdbActions.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[False]
        Me.DdbActions.Size = New System.Drawing.Size(44, 34)
        Me.DdbActions.StyleController = Me.INDlcRoot
        Me.DdbActions.TabIndex = 16
        Me.DdbActions.ToolTip = "Click para desplegar el menú de acciones"
        Me.DdbActions.ToolTipTitle = "Menú de Acciones"
        '
        'PopupMenuProductionCenter
        '
        Me.PopupMenuProductionCenter.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnAddProductionCenter), New DevExpress.XtraBars.LinkPersistInfo(Me.MBtnRemoveProductionCenter)})
        Me.PopupMenuProductionCenter.Manager = Me.BarManager1
        Me.PopupMenuProductionCenter.Name = "PopupMenuProductionCenter"
        '
        'INDpccAddProductionCenter
        '
        Me.INDpccAddProductionCenter.Controls.Add(Me.LayoutControl1)
        Me.INDpccAddProductionCenter.Location = New System.Drawing.Point(122, 224)
        Me.INDpccAddProductionCenter.Name = "INDpccAddProductionCenter"
        Me.INDpccAddProductionCenter.Size = New System.Drawing.Size(433, 179)
        Me.INDpccAddProductionCenter.TabIndex = 11
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDtxtMaximumAmount)
        Me.LayoutControl1.Controls.Add(Me.INDsbAddProductionCenter)
        Me.LayoutControl1.Controls.Add(Me.INDsleProductionCenter)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup3
        Me.LayoutControl1.Size = New System.Drawing.Size(433, 179)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDtxtMaximumAmount
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtMaximumAmount, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtMaximumAmount, False)
        Me.INDtxtMaximumAmount.EditValue = "0"
        Me.INDtxtMaximumAmount.EnterMoveNextControl = True
        Me.INDtxtMaximumAmount.Location = New System.Drawing.Point(12, 97)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtMaximumAmount, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDtxtMaximumAmount.Name = "INDtxtMaximumAmount"
        Me.INDtxtMaximumAmount.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtMaximumAmount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtMaximumAmount.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtMaximumAmount.Properties.Appearance.Options.UseFont = True
        Me.INDtxtMaximumAmount.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtMaximumAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtMaximumAmount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtMaximumAmount.Properties.Mask.EditMask = "P"
        Me.INDtxtMaximumAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtMaximumAmount.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtMaximumAmount.Size = New System.Drawing.Size(406, 28)
        Me.INDtxtMaximumAmount.StyleController = Me.LayoutControl1
        Me.INDtxtMaximumAmount.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtMaximumAmount, 0)
        '
        'INDsbAddProductionCenter
        '
        Me.INDsbAddProductionCenter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDsbAddProductionCenter.Appearance.Options.UseFont = True
        Me.INDsbAddProductionCenter.Location = New System.Drawing.Point(12, 132)
        Me.INDsbAddProductionCenter.Name = "INDsbAddProductionCenter"
        Me.INDsbAddProductionCenter.Size = New System.Drawing.Size(406, 32)
        Me.INDsbAddProductionCenter.StyleController = Me.LayoutControl1
        Me.INDsbAddProductionCenter.TabIndex = 5
        Me.INDsbAddProductionCenter.Text = "Agregar"
        '
        'INDsleProductionCenter
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleProductionCenter, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleProductionCenter, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleProductionCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleProductionCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleProductionCenter.Name = "INDsleProductionCenter"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleProductionCenter, False)
        Me.INDsleProductionCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleProductionCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleProductionCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleProductionCenter.Properties.Appearance.Options.UseFont = True
        Me.INDsleProductionCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleProductionCenter.Properties.DisplayMember = "CodeName"
        Me.INDsleProductionCenter.Properties.NullText = ""
        Me.INDsleProductionCenter.Properties.PopupSizeable = False
        Me.INDsleProductionCenter.Properties.ShowFooter = False
        Me.INDsleProductionCenter.Properties.ValueMember = "Id"
        Me.INDsleProductionCenter.Properties.View = Me.GridView2
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleProductionCenter, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleProductionCenter, True)
        Me.INDsleProductionCenter.Size = New System.Drawing.Size(406, 28)
        Me.INDsleProductionCenter.StyleController = Me.LayoutControl1
        Me.INDsleProductionCenter.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleProductionCenter, "1201")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleProductionCenter, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleProductionCenter, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleProductionCenter, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleProductionCenter, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn6})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "Code"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        Me.GridColumn5.Width = 359
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Nombre"
        Me.GridColumn6.FieldName = "Name"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 1
        Me.GridColumn6.Width = 1033
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliProductionCenter, Me.LayoutControlItem2, Me.INDliAmount})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(433, 179)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'INDliProductionCenter
        '
        Me.INDliProductionCenter.Control = Me.INDsleProductionCenter
        Me.INDliProductionCenter.CustomizationFormText = "LayoutControlItem1"
        Me.INDliProductionCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDliProductionCenter.MaxSize = New System.Drawing.Size(410, 60)
        Me.INDliProductionCenter.MinSize = New System.Drawing.Size(410, 60)
        Me.INDliProductionCenter.Name = "INDliProductionCenter"
        Me.INDliProductionCenter.Size = New System.Drawing.Size(413, 60)
        Me.INDliProductionCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliProductionCenter.Text = "Centro de Producción"
        Me.INDliProductionCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliProductionCenter.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliProductionCenter.TextSize = New System.Drawing.Size(155, 21)
        Me.INDliProductionCenter.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsbAddProductionCenter
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(413, 39)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDliAmount
        '
        Me.INDliAmount.Control = Me.INDtxtMaximumAmount
        Me.INDliAmount.CustomizationFormText = "LayoutControlItem1"
        Me.INDliAmount.Location = New System.Drawing.Point(0, 60)
        Me.INDliAmount.MaxSize = New System.Drawing.Size(410, 60)
        Me.INDliAmount.MinSize = New System.Drawing.Size(410, 60)
        Me.INDliAmount.Name = "INDliAmount"
        Me.INDliAmount.Size = New System.Drawing.Size(413, 60)
        Me.INDliAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAmount.Text = "Valor Máximo a Distribuir"
        Me.INDliAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAmount.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliAmount.TextSize = New System.Drawing.Size(155, 20)
        Me.INDliAmount.TextToControlDistance = 5
        '
        'INDpceAddDetail
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceAddDetail, True)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceAddDetail, Nothing)
        Me.INDpceAddDetail.Location = New System.Drawing.Point(438, 59)
        Me.INDpceAddDetail.MaximumSize = New System.Drawing.Size(776, 0)
        Me.INDpceAddDetail.MinimumSize = New System.Drawing.Size(776, 32)
        Me.INDpceAddDetail.Name = "INDpceAddDetail"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceAddDetail, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceAddDetail, False)
        Me.INDpceAddDetail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceAddDetail.Properties.Appearance.Options.UseFont = True
        Me.INDpceAddDetail.Properties.AutoHeight = False
        Me.INDpceAddDetail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpceAddDetail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("INDpceAddDetail.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.INDpceAddDetail.Properties.CloseUpKey = New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None)
        Me.INDpceAddDetail.Properties.PopupControl = Me.INDpccAddProductionCenter
        Me.INDpceAddDetail.Properties.PopupSizeable = False
        Me.INDpceAddDetail.Properties.ShowPopupCloseButton = False
        Me.INDpceAddDetail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceAddDetail.Size = New System.Drawing.Size(776, 34)
        Me.INDpceAddDetail.StyleController = Me.INDlcRoot
        Me.INDpceAddDetail.TabIndex = 8
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceAddDetail, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceAddDetail, Nothing)
        '
        'INDgcDetail
        '
        Me.INDgcDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDgcDetail.Location = New System.Drawing.Point(438, 97)
        Me.INDgcDetail.MainView = Me.INDgvDetail
        Me.INDgcDetail.Name = "INDgcDetail"
        Me.INDgcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrpsleProductionCenter, Me.INDrpspnValue})
        Me.INDgcDetail.Size = New System.Drawing.Size(824, 309)
        Me.INDgcDetail.TabIndex = 7
        Me.INDgcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvDetail})
        '
        'INDgvDetail
        '
        Me.INDgvDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn9})
        Me.INDgvDetail.GridControl = Me.INDgcDetail
        Me.INDgvDetail.Name = "INDgvDetail"
        Me.INDgvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvDetail.OptionsView.ShowDetailButtons = False
        Me.INDgvDetail.OptionsView.ShowFooter = True
        Me.INDgvDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvDetail, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Centro de Producción"
        Me.GridColumn1.ColumnEdit = Me.INDrpsleProductionCenter
        Me.GridColumn1.FieldName = "ProductionCenterId"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 695
        '
        'INDrpsleProductionCenter
        '
        Me.INDrpsleProductionCenter.AutoHeight = False
        Me.INDrpsleProductionCenter.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrpsleProductionCenter.DisplayMember = "CodeName"
        Me.INDrpsleProductionCenter.Name = "INDrpsleProductionCenter"
        Me.INDrpsleProductionCenter.NullText = ""
        Me.INDrpsleProductionCenter.ValueMember = "Id"
        Me.INDrpsleProductionCenter.View = Me.RepositoryItemSearchLookUpEdit1View
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 393
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 999
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Proporción"
        Me.GridColumn2.ColumnEdit = Me.INDrpspnValue
        Me.GridColumn2.DisplayFormat.FormatString = "P2"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn2.FieldName = "SetProportion"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SetProportion", "Total: {0:P2}")})
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 385
        '
        'INDrpspnValue
        '
        Me.INDrpspnValue.AutoHeight = False
        Me.INDrpspnValue.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrpspnValue.Mask.EditMask = "P2"
        Me.INDrpspnValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrpspnValue.Name = "INDrpspnValue"
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Valor Distribuido"
        Me.GridColumn9.DisplayFormat.FormatString = "C0"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "DepreciationValue"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DepreciationValue", "Total: {0:C0}")})
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        Me.GridColumn9.Width = 312
        '
        'INDsleFixetAsset
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleFixetAsset, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleFixetAsset, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleFixetAsset, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleFixetAsset, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleFixetAsset, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleFixetAsset, False)
        Me.INDsleFixetAsset.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleFixetAsset, False)
        Me.INDsleFixetAsset.Location = New System.Drawing.Point(24, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleFixetAsset, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleFixetAsset.Name = "INDsleFixetAsset"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleFixetAsset, False)
        Me.INDsleFixetAsset.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleFixetAsset.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFixetAsset.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFixetAsset.Properties.Appearance.Options.UseFont = True
        Me.INDsleFixetAsset.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleFixetAsset.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleFixetAsset.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFixetAsset.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleFixetAsset.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleFixetAsset.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleFixetAsset.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleFixetAsset.Properties.DisplayMember = "AFNACTIVO.CodeProduct"
        Me.INDsleFixetAsset.Properties.NullText = ""
        Me.INDsleFixetAsset.Properties.PopupSizeable = False
        Me.INDsleFixetAsset.Properties.ShowFooter = False
        Me.INDsleFixetAsset.Properties.ValueMember = "AFNACTIVO.OID"
        Me.INDsleFixetAsset.Properties.View = Me.INDgvFixedAsset
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleFixetAsset, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleFixetAsset, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleFixetAsset, True)
        Me.INDsleFixetAsset.Size = New System.Drawing.Size(386, 28)
        Me.INDsleFixetAsset.StyleController = Me.INDlcRoot
        Me.INDsleFixetAsset.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleFixetAsset, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleFixetAsset, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleFixetAsset, "{0} - {1}")
        Me.INDsleFixetAsset.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleFixetAsset, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleFixetAsset, False)
        '
        'INDgvFixedAsset
        '
        Me.INDgvFixedAsset.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvFixedAsset.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvFixedAsset.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvFixedAsset.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvFixedAsset.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvFixedAsset.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvFixedAsset.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvFixedAsset.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvFixedAsset.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvFixedAsset.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvFixedAsset.Appearance.Row.Options.UseFont = True
        Me.INDgvFixedAsset.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8})
        Me.INDgvFixedAsset.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvFixedAsset.Name = "INDgvFixedAsset"
        Me.INDgvFixedAsset.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvFixedAsset.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvFixedAsset.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvFixedAsset.OptionsView.ShowAutoFilterRow = True
        Me.INDgvFixedAsset.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvFixedAsset, False)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Código"
        Me.GridColumn7.FieldName = "AFNACTIVO.AACCODACT"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 442
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Producto"
        Me.GridColumn8.FieldName = "AFNACTIVO.AFNPRODUC.APRNOMBRE"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 950
        '
        'INDtxtDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDescription, True)
        Me.INDtxtDescription.EnterMoveNextControl = True
        Me.INDtxtDescription.Location = New System.Drawing.Point(24, 147)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDescription.Name = "INDtxtDescription"
        Me.INDtxtDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDescription.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtDescription.StyleController = Me.INDlcRoot
        Me.INDtxtDescription.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDescription, 0)
        Me.INDtxtDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.INDbteCode.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.InteropCost.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.INDbteCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbteCode.StyleController = Me.INDlcRoot
        Me.INDbteCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlcgRoot
        '
        Me.INDlcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgRoot, False)
        Me.INDlcgRoot.CustomizationFormText = "INDlcgRoot"
        Me.INDlcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgRoot.GroupBordersVisible = False
        Me.INDlcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LayoutControlGroup2})
        Me.INDlcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgRoot.Name = "INDlcgRoot"
        Me.INDlcgRoot.Size = New System.Drawing.Size(1286, 430)
        Me.INDlcgRoot.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliCode, Me.INDliDescription, Me.INDliFixetAsset})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(414, 410)
        Me.LayoutControlGroup1.Text = "Información General"
        '
        'INDliCode
        '
        Me.INDliCode.AllowHide = False
        Me.INDliCode.Control = Me.INDbteCode
        Me.INDliCode.CustomizationFormText = "Código"
        Me.INDliCode.Location = New System.Drawing.Point(0, 0)
        Me.INDliCode.MaxSize = New System.Drawing.Size(390, 62)
        Me.INDliCode.MinSize = New System.Drawing.Size(390, 62)
        Me.INDliCode.Name = "INDliCode"
        Me.INDliCode.ShowInCustomizationForm = False
        Me.INDliCode.Size = New System.Drawing.Size(390, 62)
        Me.INDliCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCode.Text = "Código"
        Me.INDliCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCode.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliCode.TextToControlDistance = 5
        '
        'INDliDescription
        '
        Me.INDliDescription.AllowHide = False
        Me.INDliDescription.Control = Me.INDtxtDescription
        Me.INDliDescription.CustomizationFormText = "Descripción"
        Me.INDliDescription.Location = New System.Drawing.Point(0, 62)
        Me.INDliDescription.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliDescription.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliDescription.Name = "INDliDescription"
        Me.INDliDescription.ShowInCustomizationForm = False
        Me.INDliDescription.Size = New System.Drawing.Size(390, 60)
        Me.INDliDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDescription.Text = "Descripción"
        Me.INDliDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliDescription.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliDescription.TextToControlDistance = 5
        '
        'INDliFixetAsset
        '
        Me.INDliFixetAsset.AllowHide = False
        Me.INDliFixetAsset.Control = Me.INDsleFixetAsset
        Me.INDliFixetAsset.CustomizationFormText = "Activo Fijo"
        Me.INDliFixetAsset.Location = New System.Drawing.Point(0, 122)
        Me.INDliFixetAsset.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliFixetAsset.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliFixetAsset.Name = "INDliFixetAsset"
        Me.INDliFixetAsset.ShowInCustomizationForm = False
        Me.INDliFixetAsset.Size = New System.Drawing.Size(390, 229)
        Me.INDliFixetAsset.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliFixetAsset.Text = "Activo Fijo"
        Me.INDliFixetAsset.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliFixetAsset.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliFixetAsset.TextSize = New System.Drawing.Size(96, 21)
        Me.INDliFixetAsset.TextToControlDistance = 5
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliDetail, Me.INDliAddDetail, Me.LayoutControlItem1})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(852, 410)
        Me.LayoutControlGroup2.Text = "Información Detallada"
        '
        'INDliDetail
        '
        Me.INDliDetail.Control = Me.INDgcDetail
        Me.INDliDetail.CustomizationFormText = "Distribucion Activos Fijos Detalle"
        Me.INDliDetail.Location = New System.Drawing.Point(0, 38)
        Me.INDliDetail.MaxSize = New System.Drawing.Size(828, 0)
        Me.INDliDetail.MinSize = New System.Drawing.Size(828, 24)
        Me.INDliDetail.Name = "INDliDetail"
        Me.INDliDetail.Size = New System.Drawing.Size(828, 313)
        Me.INDliDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliDetail.TextToControlDistance = 0
        Me.INDliDetail.TextVisible = False
        '
        'INDliAddDetail
        '
        Me.INDliAddDetail.Control = Me.INDpceAddDetail
        Me.INDliAddDetail.CustomizationFormText = "Agregar Detalle"
        Me.INDliAddDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDliAddDetail.MaxSize = New System.Drawing.Size(780, 38)
        Me.INDliAddDetail.MinSize = New System.Drawing.Size(780, 38)
        Me.INDliAddDetail.Name = "INDliAddDetail"
        Me.INDliAddDetail.Size = New System.Drawing.Size(780, 38)
        Me.INDliAddDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAddDetail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAddDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAddDetail.TextToControlDistance = 0
        Me.INDliAddDetail.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.DdbActions
        Me.LayoutControlItem1.Location = New System.Drawing.Point(780, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(48, 38)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(48, 38)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(48, 38)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmFixedAssetDistribution
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1112, 574)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "FrmFixedAssetDistribution"
        Me.Opacity = 1.0R
        Me.Tag = "1206"
        Me.Text = "Distribución Activos Fijos"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcRoot.ResumeLayout(False)
        CType(Me.INDpopupExport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupExport.ResumeLayout(False)
        CType(Me.INDlyExport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyExport.ResumeLayout(False)
        CType(Me.INDgcExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenuProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccAddProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccAddProductionCenter.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtxtMaximumAmount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleProductionCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceAddDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrpsleProductionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrpspnValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleFixetAsset.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvFixedAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliFixetAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDliCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleFixetAsset As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvFixedAsset As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtxtDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDliDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliFixetAsset As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceAddDetail As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDliAddDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDpccAddProductionCenter As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtMaximumAmount As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDsbAddProductionCenter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDsleProductionCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliProductionCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliAmount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrpsleProductionCenter As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrpspnValue As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DdbActions As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PopupMenuProductionCenter As DevExpress.XtraBars.PopupMenu
    Friend WithEvents MBtnAddProductionCenter As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents MBtnRemoveProductionCenter As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpopupExport As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyExport As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcExport As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewExport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemExport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepValue As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
End Class
