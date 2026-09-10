<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrSearching
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
            _model.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrSearching))
        Dim WindowsUIButtonImageOptions1 As DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions = New DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions()
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccFilterMenu = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnContentDocuments = New System.Windows.Forms.Button()
        Me.INDbtnNoFilter = New System.Windows.Forms.Button()
        Me.INDbtnToday = New System.Windows.Forms.Button()
        Me.INDbtnWeek = New System.Windows.Forms.Button()
        Me.INDbtnMonth = New System.Windows.Forms.Button()
        Me.INDbtnYear = New System.Windows.Forms.Button()
        Me.INDbtnLastHour = New System.Windows.Forms.Button()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpnlTitle = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnBack = New DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel()
        Me.INDlblTitle = New DevExpress.XtraEditors.LabelControl()
        Me.INDpnlSearch = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblFilters = New DevExpress.XtraEditors.LabelControl()
        Me.INDpnlSeparate2 = New DevExpress.XtraEditors.PanelControl()
        Me.INDpnlSeparate1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblSearchTime = New DevExpress.XtraEditors.LabelControl()
        Me.INDpnlSubSearch = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblSearch = New DevExpress.XtraEditors.LabelControl()
        Me.INDbtnSearch = New System.Windows.Forms.Button()
        Me.INDtxtSearch = New DevExpress.XtraEditors.TextEdit()
        Me.INDpnlDocsResults = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblDocsTotal = New DevExpress.XtraEditors.LabelControl()
        Me.INDpnlDocsTitle = New DevExpress.XtraEditors.PanelControl()
        Me.INDlnDocsSelector = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblDocsTitle = New DevExpress.XtraEditors.LabelControl()
        Me.INDgdcDocs = New DevExpress.XtraGrid.GridControl()
        Me.INDgdvDocs = New DevExpress.XtraGrid.Views.Layout.LayoutView()
        Me.ColDocsTile = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.RepimlTileDocs = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDimcTiles = New DevExpress.Utils.ImageCollection(Me.components)
        Me.layoutViewField_ColDocsTile = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColDocsTitle = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColDocsTitle = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColDocsNameForm = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColDocsNameForm = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColDocsAuthor = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColDocsAuthor = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColDocsUpdate = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColDocsUpdate = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColDocsContent = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.RepmemDocsContent = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.layoutViewField_ColDocsContent = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.LayoutViewCard1 = New DevExpress.XtraGrid.Views.Layout.LayoutViewCard()
        Me.INDpnlRegsResults = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblRegsTotal = New DevExpress.XtraEditors.LabelControl()
        Me.INDpnlRegsTitle = New DevExpress.XtraEditors.PanelControl()
        Me.INDlnRegsSelector = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblRegsTitle = New DevExpress.XtraEditors.LabelControl()
        Me.INDgdcRegs = New DevExpress.XtraGrid.GridControl()
        Me.INDgdvRegs = New DevExpress.XtraGrid.Views.Layout.LayoutView()
        Me.ColRegsTile = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.RepimlTileRegs = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.layoutViewField_ColRegsTile = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColRegsTitle = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColRegsTitle = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColRegsNameForm = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColRegsNameForm = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColRegsAuthor = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColRegsAuthor = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColRegsUpdate = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColRegsUpdate = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColRegsContent = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.RepmemRegsContent = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.layoutViewField_ColRegsContent = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.LayoutViewCard2 = New DevExpress.XtraGrid.Views.Layout.LayoutViewCard()
        Me.INDpnlMenuResults = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblMenuTotal = New DevExpress.XtraEditors.LabelControl()
        Me.INDpnlMenuTitle = New DevExpress.XtraEditors.PanelControl()
        Me.INDlnMenuSelector = New DevExpress.XtraEditors.PanelControl()
        Me.INDlblMenuTitle = New DevExpress.XtraEditors.LabelControl()
        Me.INDgdcMenu = New DevExpress.XtraGrid.GridControl()
        Me.INDgdvMenu = New DevExpress.XtraGrid.Views.Layout.LayoutView()
        Me.ColMenuTile = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.RepimlTileMenu = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.layoutViewField_ColMenuTile = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColMenuNameForm = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColMenuNameForm = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColMenuNameModule = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColMenuNameModule = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.ColMenuNameGroup = New DevExpress.XtraGrid.Columns.LayoutViewColumn()
        Me.layoutViewField_ColMenuNameGroup = New DevExpress.XtraGrid.Views.Layout.LayoutViewField()
        Me.LayoutViewCard3 = New DevExpress.XtraGrid.Views.Layout.LayoutViewCard()
        Me.INDlycgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyciSearch = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlycgResults = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyciMenuResults = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyciDocsResults = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyciRegsResults = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyciTitle = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDimcButtonSearch = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDimcMenuFilter = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDtmrRelaySearch = New System.Windows.Forms.Timer(Me.components)
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit(Me.components)
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycRoot.SuspendLayout()
        CType(Me.INDpccFilterMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccFilterMenu.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpnlTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlTitle.SuspendLayout()
        CType(Me.INDpnlSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlSearch.SuspendLayout()
        CType(Me.INDpnlSeparate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpnlSeparate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpnlSubSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlSubSearch.SuspendLayout()
        CType(Me.INDtxtSearch.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpnlDocsResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlDocsResults.SuspendLayout()
        CType(Me.INDpnlDocsTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlDocsTitle.SuspendLayout()
        CType(Me.INDlnDocsSelector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgdcDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgdvDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepimlTileDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDimcTiles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColDocsTile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColDocsTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColDocsNameForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColDocsAuthor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColDocsUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepmemDocsContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColDocsContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutViewCard1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpnlRegsResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlRegsResults.SuspendLayout()
        CType(Me.INDpnlRegsTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlRegsTitle.SuspendLayout()
        CType(Me.INDlnRegsSelector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgdcRegs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgdvRegs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepimlTileRegs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColRegsTile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColRegsTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColRegsNameForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColRegsAuthor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColRegsUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepmemRegsContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColRegsContent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutViewCard2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpnlMenuResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlMenuResults.SuspendLayout()
        CType(Me.INDpnlMenuTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpnlMenuTitle.SuspendLayout()
        CType(Me.INDlnMenuSelector, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgdcMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgdvMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepimlTileMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColMenuTile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColMenuNameForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColMenuNameModule, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutViewField_ColMenuNameGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutViewCard3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycgResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciMenuResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciDocsResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciRegsResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyciTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDimcButtonSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDimcMenuFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlycRoot
        '
        Me.INDlycRoot.AllowCustomization = False
        Me.INDlycRoot.AllowDrop = True
        Me.INDlycRoot.Controls.Add(Me.INDpccFilterMenu)
        Me.INDlycRoot.Controls.Add(Me.INDpnlTitle)
        Me.INDlycRoot.Controls.Add(Me.INDpnlSearch)
        Me.INDlycRoot.Controls.Add(Me.INDpnlDocsResults)
        Me.INDlycRoot.Controls.Add(Me.INDpnlRegsResults)
        Me.INDlycRoot.Controls.Add(Me.INDpnlMenuResults)
        Me.INDlycRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDlycRoot.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlycRoot.Name = "INDlycRoot"
        Me.INDlycRoot.Root = Me.INDlycgRoot
        Me.INDlycRoot.Size = New System.Drawing.Size(1001, 700)
        Me.INDlycRoot.TabIndex = 0
        Me.INDlycRoot.Text = "LayoutControl1"
        '
        'INDpccFilterMenu
        '
        Me.INDpccFilterMenu.Appearance.BorderColor = System.Drawing.Color.LightGray
        Me.INDpccFilterMenu.Appearance.Options.UseBorderColor = True
        Me.INDpccFilterMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.INDpccFilterMenu.Controls.Add(Me.LayoutControl1)
        Me.INDpccFilterMenu.Location = New System.Drawing.Point(749, 207)
        Me.INDpccFilterMenu.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDpccFilterMenu.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpccFilterMenu.Name = "INDpccFilterMenu"
        Me.INDpccFilterMenu.Size = New System.Drawing.Size(230, 257)
        Me.INDpccFilterMenu.TabIndex = 23
        Me.INDpccFilterMenu.Tag = "0"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.PanelControl1)
        Me.LayoutControl1.Controls.Add(Me.INDbtnContentDocuments)
        Me.LayoutControl1.Controls.Add(Me.INDbtnNoFilter)
        Me.LayoutControl1.Controls.Add(Me.INDbtnToday)
        Me.LayoutControl1.Controls.Add(Me.INDbtnWeek)
        Me.LayoutControl1.Controls.Add(Me.INDbtnMonth)
        Me.LayoutControl1.Controls.Add(Me.INDbtnYear)
        Me.LayoutControl1.Controls.Add(Me.INDbtnLastHour)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 2)
        Me.LayoutControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup5
        Me.LayoutControl1.Size = New System.Drawing.Size(226, 253)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Location = New System.Drawing.Point(0, 216)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(226, 1)
        Me.PanelControl1.TabIndex = 11
        '
        'INDbtnContentDocuments
        '
        Me.INDbtnContentDocuments.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnContentDocuments.FlatAppearance.BorderSize = 0
        Me.INDbtnContentDocuments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.INDbtnContentDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDbtnContentDocuments.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDbtnContentDocuments.ForeColor = System.Drawing.Color.Gray
        Me.INDbtnContentDocuments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnContentDocuments.Location = New System.Drawing.Point(0, 217)
        Me.INDbtnContentDocuments.Margin = New System.Windows.Forms.Padding(0)
        Me.INDbtnContentDocuments.Name = "INDbtnContentDocuments"
        Me.INDbtnContentDocuments.Size = New System.Drawing.Size(226, 36)
        Me.INDbtnContentDocuments.TabIndex = 10
        Me.INDbtnContentDocuments.Tag = "0"
        Me.INDbtnContentDocuments.Text = "   Incluir contenido documentos"
        Me.INDbtnContentDocuments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnContentDocuments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.INDbtnContentDocuments.UseVisualStyleBackColor = True
        '
        'INDbtnNoFilter
        '
        Me.INDbtnNoFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnNoFilter.FlatAppearance.BorderSize = 0
        Me.INDbtnNoFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.INDbtnNoFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDbtnNoFilter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDbtnNoFilter.ForeColor = System.Drawing.Color.Gray
        Me.INDbtnNoFilter.Image = CType(resources.GetObject("INDbtnNoFilter.Image"), System.Drawing.Image)
        Me.INDbtnNoFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnNoFilter.Location = New System.Drawing.Point(0, 0)
        Me.INDbtnNoFilter.Margin = New System.Windows.Forms.Padding(0)
        Me.INDbtnNoFilter.Name = "INDbtnNoFilter"
        Me.INDbtnNoFilter.Size = New System.Drawing.Size(226, 36)
        Me.INDbtnNoFilter.TabIndex = 0
        Me.INDbtnNoFilter.Tag = "1"
        Me.INDbtnNoFilter.Text = "   Sin filtro"
        Me.INDbtnNoFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnNoFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.INDbtnNoFilter.UseVisualStyleBackColor = True
        '
        'INDbtnToday
        '
        Me.INDbtnToday.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnToday.FlatAppearance.BorderSize = 0
        Me.INDbtnToday.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.INDbtnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDbtnToday.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDbtnToday.ForeColor = System.Drawing.Color.Gray
        Me.INDbtnToday.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnToday.Location = New System.Drawing.Point(0, 72)
        Me.INDbtnToday.Margin = New System.Windows.Forms.Padding(0)
        Me.INDbtnToday.Name = "INDbtnToday"
        Me.INDbtnToday.Size = New System.Drawing.Size(226, 36)
        Me.INDbtnToday.TabIndex = 8
        Me.INDbtnToday.Tag = "0"
        Me.INDbtnToday.Text = "   Hoy"
        Me.INDbtnToday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnToday.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.INDbtnToday.UseVisualStyleBackColor = True
        '
        'INDbtnWeek
        '
        Me.INDbtnWeek.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnWeek.FlatAppearance.BorderSize = 0
        Me.INDbtnWeek.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.INDbtnWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDbtnWeek.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDbtnWeek.ForeColor = System.Drawing.Color.Gray
        Me.INDbtnWeek.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnWeek.Location = New System.Drawing.Point(0, 108)
        Me.INDbtnWeek.Margin = New System.Windows.Forms.Padding(0)
        Me.INDbtnWeek.Name = "INDbtnWeek"
        Me.INDbtnWeek.Size = New System.Drawing.Size(226, 36)
        Me.INDbtnWeek.TabIndex = 7
        Me.INDbtnWeek.Tag = "0"
        Me.INDbtnWeek.Text = "   Ésta semana"
        Me.INDbtnWeek.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnWeek.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.INDbtnWeek.UseVisualStyleBackColor = True
        '
        'INDbtnMonth
        '
        Me.INDbtnMonth.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnMonth.FlatAppearance.BorderSize = 0
        Me.INDbtnMonth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.INDbtnMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDbtnMonth.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDbtnMonth.ForeColor = System.Drawing.Color.Gray
        Me.INDbtnMonth.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnMonth.Location = New System.Drawing.Point(0, 144)
        Me.INDbtnMonth.Margin = New System.Windows.Forms.Padding(0)
        Me.INDbtnMonth.Name = "INDbtnMonth"
        Me.INDbtnMonth.Size = New System.Drawing.Size(226, 36)
        Me.INDbtnMonth.TabIndex = 6
        Me.INDbtnMonth.Tag = "0"
        Me.INDbtnMonth.Text = "   Éste mes"
        Me.INDbtnMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnMonth.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.INDbtnMonth.UseVisualStyleBackColor = True
        '
        'INDbtnYear
        '
        Me.INDbtnYear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnYear.FlatAppearance.BorderSize = 0
        Me.INDbtnYear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.INDbtnYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDbtnYear.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDbtnYear.ForeColor = System.Drawing.Color.Gray
        Me.INDbtnYear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnYear.Location = New System.Drawing.Point(0, 180)
        Me.INDbtnYear.Margin = New System.Windows.Forms.Padding(0)
        Me.INDbtnYear.Name = "INDbtnYear"
        Me.INDbtnYear.Size = New System.Drawing.Size(226, 36)
        Me.INDbtnYear.TabIndex = 5
        Me.INDbtnYear.Tag = "0"
        Me.INDbtnYear.Text = "   Éste año"
        Me.INDbtnYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnYear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.INDbtnYear.UseVisualStyleBackColor = True
        '
        'INDbtnLastHour
        '
        Me.INDbtnLastHour.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnLastHour.FlatAppearance.BorderSize = 0
        Me.INDbtnLastHour.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.INDbtnLastHour.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDbtnLastHour.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDbtnLastHour.ForeColor = System.Drawing.Color.Gray
        Me.INDbtnLastHour.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnLastHour.Location = New System.Drawing.Point(0, 36)
        Me.INDbtnLastHour.Margin = New System.Windows.Forms.Padding(0)
        Me.INDbtnLastHour.Name = "INDbtnLastHour"
        Me.INDbtnLastHour.Size = New System.Drawing.Size(226, 36)
        Me.INDbtnLastHour.TabIndex = 4
        Me.INDbtnLastHour.Tag = "0"
        Me.INDbtnLastHour.Text = "   Última hora"
        Me.INDbtnLastHour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.INDbtnLastHour.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.INDbtnLastHour.UseVisualStyleBackColor = True
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup5.GroupBordersVisible = False
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem17, Me.LayoutControlItem18, Me.LayoutControlItem19, Me.LayoutControlItem20, Me.LayoutControlItem21, Me.LayoutControlItem22, Me.LayoutControlItem23, Me.LayoutControlItem24})
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(226, 253)
        Me.LayoutControlGroup5.TextVisible = False
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.INDbtnLastHour
        Me.LayoutControlItem17.CustomizationFormText = "LayoutControlItem17"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem17.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem17.MinSize = New System.Drawing.Size(1, 36)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem17.Size = New System.Drawing.Size(226, 36)
        Me.LayoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem17.TextVisible = False
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.INDbtnYear
        Me.LayoutControlItem18.CustomizationFormText = "LayoutControlItem18"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem18.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem18.MinSize = New System.Drawing.Size(1, 36)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem18.Size = New System.Drawing.Size(226, 36)
        Me.LayoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem18.TextVisible = False
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.INDbtnMonth
        Me.LayoutControlItem19.CustomizationFormText = "LayoutControlItem19"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(1, 36)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem19.Size = New System.Drawing.Size(226, 36)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem19.TextVisible = False
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.INDbtnWeek
        Me.LayoutControlItem20.CustomizationFormText = "LayoutControlItem20"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(0, 108)
        Me.LayoutControlItem20.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem20.MinSize = New System.Drawing.Size(1, 36)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem20.Size = New System.Drawing.Size(226, 36)
        Me.LayoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem20.TextVisible = False
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.INDbtnToday
        Me.LayoutControlItem21.CustomizationFormText = "LayoutControlItem21"
        Me.LayoutControlItem21.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem21.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem21.MinSize = New System.Drawing.Size(1, 36)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem21.Size = New System.Drawing.Size(226, 36)
        Me.LayoutControlItem21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem21.TextVisible = False
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.Control = Me.INDbtnNoFilter
        Me.LayoutControlItem22.CustomizationFormText = "LayoutControlItem22"
        Me.LayoutControlItem22.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem22.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem22.MinSize = New System.Drawing.Size(1, 36)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem22.Size = New System.Drawing.Size(226, 36)
        Me.LayoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem22.TextVisible = False
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.Control = Me.INDbtnContentDocuments
        Me.LayoutControlItem23.CustomizationFormText = "LayoutControlItem23"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(0, 217)
        Me.LayoutControlItem23.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem23.MinSize = New System.Drawing.Size(1, 36)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem23.Size = New System.Drawing.Size(226, 36)
        Me.LayoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem23.TextVisible = False
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.Control = Me.PanelControl1
        Me.LayoutControlItem24.CustomizationFormText = "LayoutControlItem24"
        Me.LayoutControlItem24.Location = New System.Drawing.Point(0, 216)
        Me.LayoutControlItem24.MaxSize = New System.Drawing.Size(0, 1)
        Me.LayoutControlItem24.MinSize = New System.Drawing.Size(1, 1)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem24.Size = New System.Drawing.Size(226, 1)
        Me.LayoutControlItem24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem24.TextVisible = False
        '
        'INDpnlTitle
        '
        Me.INDpnlTitle.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpnlTitle.Appearance.Options.UseBackColor = True
        Me.INDpnlTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlTitle.Controls.Add(Me.INDbtnBack)
        Me.INDpnlTitle.Controls.Add(Me.INDlblTitle)
        Me.INDpnlTitle.Location = New System.Drawing.Point(2, 142)
        Me.INDpnlTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlTitle.Name = "INDpnlTitle"
        Me.INDpnlTitle.Size = New System.Drawing.Size(999, 50)
        Me.INDpnlTitle.TabIndex = 0
        '
        'INDbtnBack
        '
        WindowsUIButtonImageOptions1.Image = CType(resources.GetObject("WindowsUIButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDbtnBack.Buttons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.XtraBars.Docking2010.WindowsUIButton("", False, WindowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, True, Nothing, True, False, True, Nothing, -1, False)})
        Me.INDbtnBack.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnBack.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDbtnBack.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDbtnBack.Location = New System.Drawing.Point(0, 0)
        Me.INDbtnBack.Name = "INDbtnBack"
        Me.INDbtnBack.Size = New System.Drawing.Size(70, 50)
        Me.INDbtnBack.TabIndex = 1
        Me.INDbtnBack.TabStop = False
        Me.INDbtnBack.Visible = False
        '
        'INDlblTitle
        '
        Me.INDlblTitle.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDlblTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 28.0!)
        Me.INDlblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlblTitle.Appearance.Options.UseBackColor = True
        Me.INDlblTitle.Appearance.Options.UseFont = True
        Me.INDlblTitle.Appearance.Options.UseForeColor = True
        Me.INDlblTitle.AutoEllipsis = True
        Me.INDlblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblTitle.Location = New System.Drawing.Point(70, 0)
        Me.INDlblTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblTitle.MaximumSize = New System.Drawing.Size(0, 50)
        Me.INDlblTitle.MinimumSize = New System.Drawing.Size(900, 50)
        Me.INDlblTitle.Name = "INDlblTitle"
        Me.INDlblTitle.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.INDlblTitle.Size = New System.Drawing.Size(900, 50)
        Me.INDlblTitle.TabIndex = 0
        '
        'INDpnlSearch
        '
        Me.INDpnlSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDpnlSearch.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDpnlSearch.Appearance.Options.UseFont = True
        Me.INDpnlSearch.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlSearch.Controls.Add(Me.INDlblFilters)
        Me.INDpnlSearch.Controls.Add(Me.INDpnlSeparate2)
        Me.INDpnlSearch.Controls.Add(Me.INDpnlSeparate1)
        Me.INDpnlSearch.Controls.Add(Me.INDlblSearchTime)
        Me.INDpnlSearch.Controls.Add(Me.INDpnlSubSearch)
        Me.INDpnlSearch.Location = New System.Drawing.Point(0, 60)
        Me.INDpnlSearch.MaximumSize = New System.Drawing.Size(0, 70)
        Me.INDpnlSearch.MinimumSize = New System.Drawing.Size(1024, 70)
        Me.INDpnlSearch.Name = "INDpnlSearch"
        Me.INDpnlSearch.Size = New System.Drawing.Size(1024, 70)
        Me.INDpnlSearch.TabIndex = 1
        '
        'INDlblFilters
        '
        Me.INDlblFilters.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDlblFilters.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDlblFilters.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.INDlblFilters.Appearance.Options.UseBackColor = True
        Me.INDlblFilters.Appearance.Options.UseFont = True
        Me.INDlblFilters.Appearance.Options.UseForeColor = True
        Me.INDlblFilters.Appearance.Options.UseTextOptions = True
        Me.INDlblFilters.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblFilters.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblFilters.AutoEllipsis = True
        Me.INDlblFilters.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblFilters.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlblFilters.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDlblFilters.Location = New System.Drawing.Point(700, 43)
        Me.INDlblFilters.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblFilters.MaximumSize = New System.Drawing.Size(100, 26)
        Me.INDlblFilters.MinimumSize = New System.Drawing.Size(100, 26)
        Me.INDlblFilters.Name = "INDlblFilters"
        Me.INDlblFilters.Size = New System.Drawing.Size(100, 26)
        Me.INDlblFilters.TabIndex = 0
        Me.INDlblFilters.Tag = "{0}  ▼"
        Me.INDlblFilters.Text = "Sin filtro  ▼"
        '
        'INDpnlSeparate2
        '
        Me.INDpnlSeparate2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDpnlSeparate2.Appearance.BackColor = System.Drawing.Color.Silver
        Me.INDpnlSeparate2.Appearance.Options.UseBackColor = True
        Me.INDpnlSeparate2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlSeparate2.Location = New System.Drawing.Point(0, 69)
        Me.INDpnlSeparate2.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlSeparate2.MaximumSize = New System.Drawing.Size(0, 1)
        Me.INDpnlSeparate2.MinimumSize = New System.Drawing.Size(1024, 1)
        Me.INDpnlSeparate2.Name = "INDpnlSeparate2"
        Me.INDpnlSeparate2.Size = New System.Drawing.Size(1024, 1)
        Me.INDpnlSeparate2.TabIndex = 5
        '
        'INDpnlSeparate1
        '
        Me.INDpnlSeparate1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDpnlSeparate1.Appearance.BackColor = System.Drawing.Color.Silver
        Me.INDpnlSeparate1.Appearance.Options.UseBackColor = True
        Me.INDpnlSeparate1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlSeparate1.Location = New System.Drawing.Point(0, 42)
        Me.INDpnlSeparate1.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlSeparate1.MaximumSize = New System.Drawing.Size(0, 1)
        Me.INDpnlSeparate1.MinimumSize = New System.Drawing.Size(1024, 1)
        Me.INDpnlSeparate1.Name = "INDpnlSeparate1"
        Me.INDpnlSeparate1.Size = New System.Drawing.Size(1024, 1)
        Me.INDpnlSeparate1.TabIndex = 4
        '
        'INDlblSearchTime
        '
        Me.INDlblSearchTime.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDlblSearchTime.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblSearchTime.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.INDlblSearchTime.Appearance.Options.UseBackColor = True
        Me.INDlblSearchTime.Appearance.Options.UseFont = True
        Me.INDlblSearchTime.Appearance.Options.UseForeColor = True
        Me.INDlblSearchTime.Appearance.Options.UseTextOptions = True
        Me.INDlblSearchTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblSearchTime.AutoEllipsis = True
        Me.INDlblSearchTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblSearchTime.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlblSearchTime.LineColor = System.Drawing.Color.Gray
        Me.INDlblSearchTime.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom
        Me.INDlblSearchTime.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal
        Me.INDlblSearchTime.Location = New System.Drawing.Point(0, 43)
        Me.INDlblSearchTime.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDlblSearchTime.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblSearchTime.MaximumSize = New System.Drawing.Size(0, 26)
        Me.INDlblSearchTime.MinimumSize = New System.Drawing.Size(700, 26)
        Me.INDlblSearchTime.Name = "INDlblSearchTime"
        Me.INDlblSearchTime.Padding = New System.Windows.Forms.Padding(82, 0, 0, 0)
        Me.INDlblSearchTime.Size = New System.Drawing.Size(700, 26)
        Me.INDlblSearchTime.TabIndex = 0
        Me.INDlblSearchTime.Tag = "Cerca de {0} resultados ({1} segundos)"
        '
        'INDpnlSubSearch
        '
        Me.INDpnlSubSearch.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDpnlSubSearch.Appearance.Options.UseBackColor = True
        Me.INDpnlSubSearch.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlSubSearch.Controls.Add(Me.INDlblSearch)
        Me.INDpnlSubSearch.Controls.Add(Me.INDbtnSearch)
        Me.INDpnlSubSearch.Controls.Add(Me.INDtxtSearch)
        Me.INDpnlSubSearch.Location = New System.Drawing.Point(0, 0)
        Me.INDpnlSubSearch.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlSubSearch.Name = "INDpnlSubSearch"
        Me.INDpnlSubSearch.Size = New System.Drawing.Size(800, 70)
        Me.INDpnlSubSearch.TabIndex = 3
        '
        'INDlblSearch
        '
        Me.INDlblSearch.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDlblSearch.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblSearch.Appearance.Options.UseBackColor = True
        Me.INDlblSearch.Appearance.Options.UseFont = True
        Me.INDlblSearch.Appearance.Options.UseTextOptions = True
        Me.INDlblSearch.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblSearch.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblSearch.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblSearch.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlblSearch.Location = New System.Drawing.Point(13, 6)
        Me.INDlblSearch.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDlblSearch.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblSearch.Name = "INDlblSearch"
        Me.INDlblSearch.Size = New System.Drawing.Size(146, 32)
        Me.INDlblSearch.TabIndex = 2
        Me.INDlblSearch.Tag = "Texto a buscar|Resultados para"
        Me.INDlblSearch.Text = "Texto a buscar"
        '
        'INDbtnSearch
        '
        Me.INDbtnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDbtnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDbtnSearch.FlatAppearance.BorderSize = 0
        Me.INDbtnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke
        Me.INDbtnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.INDbtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDbtnSearch.Image = CType(resources.GetObject("INDbtnSearch.Image"), System.Drawing.Image)
        Me.INDbtnSearch.Location = New System.Drawing.Point(734, 0)
        Me.INDbtnSearch.Name = "INDbtnSearch"
        Me.INDbtnSearch.Size = New System.Drawing.Size(66, 42)
        Me.INDbtnSearch.TabIndex = 1
        Me.INDbtnSearch.TabStop = False
        Me.INDbtnSearch.UseVisualStyleBackColor = False
        Me.INDbtnSearch.Visible = False
        '
        'INDtxtSearch
        '
        Me.INDtxtSearch.EditValue = ""
        Me.INDtxtSearch.Location = New System.Drawing.Point(162, 6)
        Me.INDtxtSearch.Margin = New System.Windows.Forms.Padding(0)
        Me.INDtxtSearch.Name = "INDtxtSearch"
        Me.INDtxtSearch.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSearch.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtxtSearch.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSearch.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSearch.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSearch.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDtxtSearch.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDtxtSearch.Properties.AppearanceFocused.BackColor = System.Drawing.Color.WhiteSmoke
        Me.INDtxtSearch.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.WhiteSmoke
        Me.INDtxtSearch.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 14.25!)
        Me.INDtxtSearch.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDtxtSearch.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSearch.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtSearch.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSearch.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtSearch.Properties.AutoHeight = False
        Me.INDtxtSearch.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDtxtSearch.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.INDtxtSearch.Properties.MaxLength = 255
        Me.INDtxtSearch.Properties.NullValuePrompt = "Escriba el texto a buscar..."
        Me.INDtxtSearch.Size = New System.Drawing.Size(563, 32)
        Me.INDtxtSearch.TabIndex = 0
        '
        'INDpnlDocsResults
        '
        Me.INDpnlDocsResults.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlDocsResults.Controls.Add(Me.INDlblDocsTotal)
        Me.INDpnlDocsResults.Controls.Add(Me.INDpnlDocsTitle)
        Me.INDpnlDocsResults.Controls.Add(Me.INDgdcDocs)
        Me.INDpnlDocsResults.Location = New System.Drawing.Point(668, 192)
        Me.INDpnlDocsResults.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlDocsResults.MinimumSize = New System.Drawing.Size(333, 450)
        Me.INDpnlDocsResults.Name = "INDpnlDocsResults"
        Me.INDpnlDocsResults.Size = New System.Drawing.Size(333, 488)
        Me.INDpnlDocsResults.TabIndex = 3
        '
        'INDlblDocsTotal
        '
        Me.INDlblDocsTotal.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 16.0!)
        Me.INDlblDocsTotal.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlblDocsTotal.Appearance.Options.UseFont = True
        Me.INDlblDocsTotal.Appearance.Options.UseForeColor = True
        Me.INDlblDocsTotal.Appearance.Options.UseTextOptions = True
        Me.INDlblDocsTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblDocsTotal.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblDocsTotal.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblDocsTotal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblDocsTotal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDlblDocsTotal.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDlblDocsTotal.Location = New System.Drawing.Point(0, 438)
        Me.INDlblDocsTotal.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblDocsTotal.Name = "INDlblDocsTotal"
        Me.INDlblDocsTotal.Size = New System.Drawing.Size(333, 50)
        Me.INDlblDocsTotal.TabIndex = 3
        Me.INDlblDocsTotal.Tag = "Total:  {0}  -  Ver más  >"
        '
        'INDpnlDocsTitle
        '
        Me.INDpnlDocsTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlDocsTitle.Controls.Add(Me.INDlnDocsSelector)
        Me.INDpnlDocsTitle.Controls.Add(Me.INDlblDocsTitle)
        Me.INDpnlDocsTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpnlDocsTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDpnlDocsTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlDocsTitle.Name = "INDpnlDocsTitle"
        Me.INDpnlDocsTitle.Size = New System.Drawing.Size(333, 50)
        Me.INDpnlDocsTitle.TabIndex = 2
        '
        'INDlnDocsSelector
        '
        Me.INDlnDocsSelector.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.INDlnDocsSelector.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlnDocsSelector.Appearance.Options.UseBackColor = True
        Me.INDlnDocsSelector.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlnDocsSelector.Location = New System.Drawing.Point(99, 12)
        Me.INDlnDocsSelector.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlnDocsSelector.Name = "INDlnDocsSelector"
        Me.INDlnDocsSelector.Size = New System.Drawing.Size(136, 3)
        Me.INDlnDocsSelector.TabIndex = 5
        Me.INDlnDocsSelector.Visible = False
        '
        'INDlblDocsTitle
        '
        Me.INDlblDocsTitle.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDlblDocsTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblDocsTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlblDocsTitle.Appearance.Options.UseBackColor = True
        Me.INDlblDocsTitle.Appearance.Options.UseFont = True
        Me.INDlblDocsTitle.Appearance.Options.UseForeColor = True
        Me.INDlblDocsTitle.Appearance.Options.UseTextOptions = True
        Me.INDlblDocsTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblDocsTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblDocsTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblDocsTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlblDocsTitle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDlblDocsTitle.Location = New System.Drawing.Point(0, 15)
        Me.INDlblDocsTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblDocsTitle.Name = "INDlblDocsTitle"
        Me.INDlblDocsTitle.Size = New System.Drawing.Size(333, 35)
        Me.INDlblDocsTitle.TabIndex = 1
        Me.INDlblDocsTitle.Text = "Documentos"
        '
        'INDgdcDocs
        '
        Me.INDgdcDocs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDgdcDocs.Location = New System.Drawing.Point(0, 50)
        Me.INDgdcDocs.MainView = Me.INDgdvDocs
        Me.INDgdcDocs.Margin = New System.Windows.Forms.Padding(0)
        Me.INDgdcDocs.Name = "INDgdcDocs"
        Me.INDgdcDocs.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepimlTileDocs, Me.RepmemDocsContent})
        Me.INDgdcDocs.Size = New System.Drawing.Size(333, 384)
        Me.INDgdcDocs.TabIndex = 0
        Me.INDgdcDocs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgdvDocs})
        '
        'INDgdvDocs
        '
        Me.INDgdvDocs.Appearance.ViewCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.INDgdvDocs.Appearance.ViewCaption.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.INDgdvDocs.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 15.75!)
        Me.INDgdvDocs.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Gray
        Me.INDgdvDocs.Appearance.ViewCaption.Options.UseBackColor = True
        Me.INDgdvDocs.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgdvDocs.Appearance.ViewCaption.Options.UseForeColor = True
        Me.INDgdvDocs.Appearance.ViewCaption.Options.UseTextOptions = True
        Me.INDgdvDocs.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDgdvDocs.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDgdvDocs.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDgdvDocs.CardMinSize = New System.Drawing.Size(500, 200)
        Me.INDgdvDocs.Columns.AddRange(New DevExpress.XtraGrid.Columns.LayoutViewColumn() {Me.ColDocsTile, Me.ColDocsTitle, Me.ColDocsNameForm, Me.ColDocsAuthor, Me.ColDocsUpdate, Me.ColDocsContent})
        Me.INDgdvDocs.GridControl = Me.INDgdcDocs
        Me.INDgdvDocs.Name = "INDgdvDocs"
        Me.INDgdvDocs.OptionsBehavior.AutoFocusCardOnScrolling = True
        Me.INDgdvDocs.OptionsBehavior.Editable = False
        Me.INDgdvDocs.OptionsBehavior.ReadOnly = True
        Me.INDgdvDocs.OptionsBehavior.ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.[Auto]
        Me.INDgdvDocs.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Horizontal
        Me.INDgdvDocs.OptionsMultiRecordMode.StretchCardToViewWidth = True
        Me.INDgdvDocs.OptionsView.AllowHotTrackFields = False
        Me.INDgdvDocs.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        Me.INDgdvDocs.OptionsView.CardArrangeRule = DevExpress.XtraGrid.Views.Layout.LayoutCardArrangeRule.AllowPartialCards
        Me.INDgdvDocs.OptionsView.CardsAlignment = DevExpress.XtraGrid.Views.Layout.CardsAlignment.Near
        Me.INDgdvDocs.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDgdvDocs.OptionsView.ShowCardBorderIfCaptionHidden = False
        Me.INDgdvDocs.OptionsView.ShowCardCaption = False
        Me.INDgdvDocs.OptionsView.ShowCardExpandButton = False
        Me.INDgdvDocs.OptionsView.ShowCardLines = False
        Me.INDgdvDocs.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDgdvDocs.OptionsView.ShowHeaderPanel = False
        Me.INDgdvDocs.OptionsView.ShowViewCaption = True
        Me.INDgdvDocs.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Column
        Me.INDgdvDocs.PaintStyleName = "Skin"
        Me.INDgdvDocs.TemplateCard = Me.LayoutViewCard1
        Me.INDgdvDocs.ViewCaption = "Sin resultados"
        '
        'ColDocsTile
        '
        Me.ColDocsTile.ColumnEdit = Me.RepimlTileDocs
        Me.ColDocsTile.FieldName = "Extension"
        Me.ColDocsTile.LayoutViewField = Me.layoutViewField_ColDocsTile
        Me.ColDocsTile.Name = "ColDocsTile"
        Me.ColDocsTile.OptionsColumn.AllowEdit = False
        Me.ColDocsTile.OptionsColumn.AllowFocus = False
        Me.ColDocsTile.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsTile.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsTile.OptionsColumn.AllowMove = False
        Me.ColDocsTile.OptionsColumn.AllowShowHide = False
        Me.ColDocsTile.OptionsColumn.AllowSize = False
        Me.ColDocsTile.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsTile.OptionsColumn.ReadOnly = True
        Me.ColDocsTile.OptionsColumn.ShowCaption = False
        Me.ColDocsTile.OptionsColumn.TabStop = False
        Me.ColDocsTile.OptionsFilter.AllowAutoFilter = False
        Me.ColDocsTile.OptionsFilter.AllowFilter = False
        Me.ColDocsTile.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'RepimlTileDocs
        '
        Me.RepimlTileDocs.AllowFocused = False
        Me.RepimlTileDocs.AutoHeight = False
        Me.RepimlTileDocs.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepimlTileDocs.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".amr", 3), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".mp3", 4), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".wav", 5), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".wma", 6), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".7z", 7), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".doc", 8), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".docx", 9), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".htm", 10), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".html", 11), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".log", 12), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".mdb", 13), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".pdf", 14), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".pps", 15), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".ppsx", 16), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".ppt", 17), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".pptx", 18), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".rar", 19), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".rtf", 20), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".txt", 21), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".xls", 22), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".xlsx", 23), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".xml", 24), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".xps", 25), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".zip", 26), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".bmp", 27), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".gif", 28), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".ico", 29), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".jpeg", 30), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".jpg", 31), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".png", 32), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".tif", 33), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".avi", 34), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".mkv", 35), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".mp4", 36), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".mpeg", 37), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".mpg", 38), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".vob", 39), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".wmv", 40), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ".otro", 41)})
        Me.RepimlTileDocs.LargeImages = Me.INDimcTiles
        Me.RepimlTileDocs.Name = "RepimlTileDocs"
        Me.RepimlTileDocs.ReadOnly = True
        '
        'INDimcTiles
        '
        Me.INDimcTiles.ImageSize = New System.Drawing.Size(62, 62)
        Me.INDimcTiles.ImageStream = CType(resources.GetObject("INDimcTiles.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDimcTiles.Images.SetKeyName(0, "menu.png")
        Me.INDimcTiles.Images.SetKeyName(1, "archivo.png")
        Me.INDimcTiles.Images.SetKeyName(2, "procesos.png")
        Me.INDimcTiles.Images.SetKeyName(3, "amr.png")
        Me.INDimcTiles.Images.SetKeyName(4, "mp3.png")
        Me.INDimcTiles.Images.SetKeyName(5, "wav.png")
        Me.INDimcTiles.Images.SetKeyName(6, "wmaa.png")
        Me.INDimcTiles.Images.SetKeyName(7, "7z.png")
        Me.INDimcTiles.Images.SetKeyName(8, "doc.png")
        Me.INDimcTiles.Images.SetKeyName(9, "docx.png")
        Me.INDimcTiles.Images.SetKeyName(10, "htm.png")
        Me.INDimcTiles.Images.SetKeyName(11, "html.png")
        Me.INDimcTiles.Images.SetKeyName(12, "log.png")
        Me.INDimcTiles.Images.SetKeyName(13, "mdb.png")
        Me.INDimcTiles.Images.SetKeyName(14, "pdf.png")
        Me.INDimcTiles.Images.SetKeyName(15, "pps.png")
        Me.INDimcTiles.Images.SetKeyName(16, "ppsx.png")
        Me.INDimcTiles.Images.SetKeyName(17, "ppt.png")
        Me.INDimcTiles.Images.SetKeyName(18, "pptx.png")
        Me.INDimcTiles.Images.SetKeyName(19, "rar.png")
        Me.INDimcTiles.Images.SetKeyName(20, "rtf.png")
        Me.INDimcTiles.Images.SetKeyName(21, "txt.png")
        Me.INDimcTiles.Images.SetKeyName(22, "xls.png")
        Me.INDimcTiles.Images.SetKeyName(23, "xlsx.png")
        Me.INDimcTiles.Images.SetKeyName(24, "xml.png")
        Me.INDimcTiles.Images.SetKeyName(25, "xps.png")
        Me.INDimcTiles.Images.SetKeyName(26, "zip.png")
        Me.INDimcTiles.Images.SetKeyName(27, "bmp.png")
        Me.INDimcTiles.Images.SetKeyName(28, "gif.png")
        Me.INDimcTiles.Images.SetKeyName(29, "Ico.png")
        Me.INDimcTiles.Images.SetKeyName(30, "jpeg.png")
        Me.INDimcTiles.Images.SetKeyName(31, "jpg.png")
        Me.INDimcTiles.Images.SetKeyName(32, "png.png")
        Me.INDimcTiles.Images.SetKeyName(33, "tif.png")
        Me.INDimcTiles.Images.SetKeyName(34, "avi.png")
        Me.INDimcTiles.Images.SetKeyName(35, "mkv.png")
        Me.INDimcTiles.Images.SetKeyName(36, "mp4.png")
        Me.INDimcTiles.Images.SetKeyName(37, "mpeg.png")
        Me.INDimcTiles.Images.SetKeyName(38, "mpg.png")
        Me.INDimcTiles.Images.SetKeyName(39, "vob.png")
        Me.INDimcTiles.Images.SetKeyName(40, "wmv.png")
        Me.INDimcTiles.Images.SetKeyName(41, "otro.png")
        '
        'layoutViewField_ColDocsTile
        '
        Me.layoutViewField_ColDocsTile.AllowHtmlStringInCaption = True
        Me.layoutViewField_ColDocsTile.EditorPreferredWidth = 63
        Me.layoutViewField_ColDocsTile.Location = New System.Drawing.Point(0, 0)
        Me.layoutViewField_ColDocsTile.MaxSize = New System.Drawing.Size(63, 63)
        Me.layoutViewField_ColDocsTile.MinSize = New System.Drawing.Size(63, 63)
        Me.layoutViewField_ColDocsTile.Name = "layoutViewField_ColDocsTile"
        Me.layoutViewField_ColDocsTile.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColDocsTile.Size = New System.Drawing.Size(63, 192)
        Me.layoutViewField_ColDocsTile.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColDocsTile.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColDocsTile.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutViewField_ColDocsTile.TextToControlDistance = 0
        Me.layoutViewField_ColDocsTile.TextVisible = False
        '
        'ColDocsTitle
        '
        Me.ColDocsTitle.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro
        Me.ColDocsTitle.AppearanceCell.BackColor2 = System.Drawing.Color.Gainsboro
        Me.ColDocsTitle.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 15.75!, System.Drawing.FontStyle.Underline)
        Me.ColDocsTitle.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.ColDocsTitle.AppearanceCell.Options.UseBackColor = True
        Me.ColDocsTitle.AppearanceCell.Options.UseFont = True
        Me.ColDocsTitle.AppearanceCell.Options.UseForeColor = True
        Me.ColDocsTitle.AppearanceCell.Options.UseTextOptions = True
        Me.ColDocsTitle.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColDocsTitle.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDocsTitle.FieldName = "Title"
        Me.ColDocsTitle.LayoutViewField = Me.layoutViewField_ColDocsTitle
        Me.ColDocsTitle.Name = "ColDocsTitle"
        Me.ColDocsTitle.OptionsColumn.AllowEdit = False
        Me.ColDocsTitle.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.ColDocsTitle.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsTitle.OptionsColumn.AllowMove = False
        Me.ColDocsTitle.OptionsColumn.AllowShowHide = False
        Me.ColDocsTitle.OptionsColumn.AllowSize = False
        Me.ColDocsTitle.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsTitle.OptionsColumn.ReadOnly = True
        Me.ColDocsTitle.OptionsColumn.ShowCaption = False
        Me.ColDocsTitle.OptionsColumn.ShowInCustomizationForm = False
        Me.ColDocsTitle.OptionsFilter.AllowAutoFilter = False
        Me.ColDocsTitle.OptionsFilter.AllowFilter = False
        Me.ColDocsTitle.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColDocsTitle
        '
        Me.layoutViewField_ColDocsTitle.EditorPreferredWidth = 414
        Me.layoutViewField_ColDocsTitle.Location = New System.Drawing.Point(63, 0)
        Me.layoutViewField_ColDocsTitle.MaxSize = New System.Drawing.Size(0, 36)
        Me.layoutViewField_ColDocsTitle.MinSize = New System.Drawing.Size(30, 36)
        Me.layoutViewField_ColDocsTitle.Name = "layoutViewField_ColDocsTitle"
        Me.layoutViewField_ColDocsTitle.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColDocsTitle.Size = New System.Drawing.Size(417, 36)
        Me.layoutViewField_ColDocsTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColDocsTitle.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColDocsTitle.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutViewField_ColDocsTitle.TextVisible = False
        '
        'ColDocsNameForm
        '
        Me.ColDocsNameForm.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColDocsNameForm.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColDocsNameForm.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColDocsNameForm.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColDocsNameForm.AppearanceCell.Options.UseBackColor = True
        Me.ColDocsNameForm.AppearanceCell.Options.UseFont = True
        Me.ColDocsNameForm.AppearanceCell.Options.UseForeColor = True
        Me.ColDocsNameForm.AppearanceCell.Options.UseTextOptions = True
        Me.ColDocsNameForm.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColDocsNameForm.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDocsNameForm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColDocsNameForm.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColDocsNameForm.AppearanceHeader.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColDocsNameForm.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColDocsNameForm.AppearanceHeader.ForeColor = System.Drawing.Color.Black
        Me.ColDocsNameForm.AppearanceHeader.Options.UseBackColor = True
        Me.ColDocsNameForm.AppearanceHeader.Options.UseFont = True
        Me.ColDocsNameForm.AppearanceHeader.Options.UseForeColor = True
        Me.ColDocsNameForm.AppearanceHeader.Options.UseTextOptions = True
        Me.ColDocsNameForm.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColDocsNameForm.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDocsNameForm.Caption = "Funcional"
        Me.ColDocsNameForm.FieldName = "NameForm"
        Me.ColDocsNameForm.LayoutViewField = Me.layoutViewField_ColDocsNameForm
        Me.ColDocsNameForm.Name = "ColDocsNameForm"
        Me.ColDocsNameForm.OptionsColumn.AllowEdit = False
        Me.ColDocsNameForm.OptionsColumn.AllowFocus = False
        Me.ColDocsNameForm.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsNameForm.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsNameForm.OptionsColumn.AllowMove = False
        Me.ColDocsNameForm.OptionsColumn.AllowShowHide = False
        Me.ColDocsNameForm.OptionsColumn.AllowSize = False
        Me.ColDocsNameForm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsNameForm.OptionsColumn.ReadOnly = True
        Me.ColDocsNameForm.OptionsColumn.ShowCaption = False
        Me.ColDocsNameForm.OptionsColumn.TabStop = False
        Me.ColDocsNameForm.OptionsFilter.AllowAutoFilter = False
        Me.ColDocsNameForm.OptionsFilter.AllowFilter = False
        Me.ColDocsNameForm.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColDocsNameForm
        '
        Me.layoutViewField_ColDocsNameForm.EditorPreferredWidth = 344
        Me.layoutViewField_ColDocsNameForm.Location = New System.Drawing.Point(63, 36)
        Me.layoutViewField_ColDocsNameForm.MaxSize = New System.Drawing.Size(0, 26)
        Me.layoutViewField_ColDocsNameForm.MinSize = New System.Drawing.Size(77, 26)
        Me.layoutViewField_ColDocsNameForm.Name = "layoutViewField_ColDocsNameForm"
        Me.layoutViewField_ColDocsNameForm.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColDocsNameForm.Size = New System.Drawing.Size(417, 26)
        Me.layoutViewField_ColDocsNameForm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColDocsNameForm.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColDocsNameForm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColDocsNameForm.TextSize = New System.Drawing.Size(70, 13)
        Me.layoutViewField_ColDocsNameForm.TextToControlDistance = 0
        '
        'ColDocsAuthor
        '
        Me.ColDocsAuthor.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColDocsAuthor.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColDocsAuthor.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColDocsAuthor.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColDocsAuthor.AppearanceCell.Options.UseBackColor = True
        Me.ColDocsAuthor.AppearanceCell.Options.UseFont = True
        Me.ColDocsAuthor.AppearanceCell.Options.UseForeColor = True
        Me.ColDocsAuthor.AppearanceCell.Options.UseTextOptions = True
        Me.ColDocsAuthor.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColDocsAuthor.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDocsAuthor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColDocsAuthor.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColDocsAuthor.AppearanceHeader.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColDocsAuthor.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColDocsAuthor.AppearanceHeader.ForeColor = System.Drawing.Color.Black
        Me.ColDocsAuthor.AppearanceHeader.Options.UseBackColor = True
        Me.ColDocsAuthor.AppearanceHeader.Options.UseFont = True
        Me.ColDocsAuthor.AppearanceHeader.Options.UseForeColor = True
        Me.ColDocsAuthor.AppearanceHeader.Options.UseTextOptions = True
        Me.ColDocsAuthor.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColDocsAuthor.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDocsAuthor.Caption = "Autor"
        Me.ColDocsAuthor.FieldName = "CreationUser"
        Me.ColDocsAuthor.LayoutViewField = Me.layoutViewField_ColDocsAuthor
        Me.ColDocsAuthor.Name = "ColDocsAuthor"
        Me.ColDocsAuthor.OptionsColumn.AllowEdit = False
        Me.ColDocsAuthor.OptionsColumn.AllowFocus = False
        Me.ColDocsAuthor.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsAuthor.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsAuthor.OptionsColumn.AllowMove = False
        Me.ColDocsAuthor.OptionsColumn.AllowShowHide = False
        Me.ColDocsAuthor.OptionsColumn.AllowSize = False
        Me.ColDocsAuthor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsAuthor.OptionsColumn.ReadOnly = True
        Me.ColDocsAuthor.OptionsColumn.ShowCaption = False
        Me.ColDocsAuthor.OptionsColumn.TabStop = False
        Me.ColDocsAuthor.OptionsFilter.AllowAutoFilter = False
        Me.ColDocsAuthor.OptionsFilter.AllowFilter = False
        Me.ColDocsAuthor.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColDocsAuthor
        '
        Me.layoutViewField_ColDocsAuthor.EditorPreferredWidth = 344
        Me.layoutViewField_ColDocsAuthor.Location = New System.Drawing.Point(63, 62)
        Me.layoutViewField_ColDocsAuthor.MaxSize = New System.Drawing.Size(0, 26)
        Me.layoutViewField_ColDocsAuthor.MinSize = New System.Drawing.Size(77, 26)
        Me.layoutViewField_ColDocsAuthor.Name = "layoutViewField_ColDocsAuthor"
        Me.layoutViewField_ColDocsAuthor.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColDocsAuthor.Size = New System.Drawing.Size(417, 26)
        Me.layoutViewField_ColDocsAuthor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColDocsAuthor.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColDocsAuthor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColDocsAuthor.TextSize = New System.Drawing.Size(70, 13)
        Me.layoutViewField_ColDocsAuthor.TextToControlDistance = 0
        '
        'ColDocsUpdate
        '
        Me.ColDocsUpdate.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColDocsUpdate.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColDocsUpdate.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColDocsUpdate.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColDocsUpdate.AppearanceCell.Options.UseBackColor = True
        Me.ColDocsUpdate.AppearanceCell.Options.UseFont = True
        Me.ColDocsUpdate.AppearanceCell.Options.UseForeColor = True
        Me.ColDocsUpdate.AppearanceCell.Options.UseTextOptions = True
        Me.ColDocsUpdate.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColDocsUpdate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDocsUpdate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColDocsUpdate.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColDocsUpdate.AppearanceHeader.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColDocsUpdate.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColDocsUpdate.AppearanceHeader.ForeColor = System.Drawing.Color.Black
        Me.ColDocsUpdate.AppearanceHeader.Options.UseBackColor = True
        Me.ColDocsUpdate.AppearanceHeader.Options.UseFont = True
        Me.ColDocsUpdate.AppearanceHeader.Options.UseForeColor = True
        Me.ColDocsUpdate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColDocsUpdate.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColDocsUpdate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColDocsUpdate.Caption = "Modificado"
        Me.ColDocsUpdate.DisplayFormat.FormatString = "G"
        Me.ColDocsUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColDocsUpdate.FieldName = "Update"
        Me.ColDocsUpdate.LayoutViewField = Me.layoutViewField_ColDocsUpdate
        Me.ColDocsUpdate.Name = "ColDocsUpdate"
        Me.ColDocsUpdate.OptionsColumn.AllowEdit = False
        Me.ColDocsUpdate.OptionsColumn.AllowFocus = False
        Me.ColDocsUpdate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsUpdate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsUpdate.OptionsColumn.AllowMove = False
        Me.ColDocsUpdate.OptionsColumn.AllowShowHide = False
        Me.ColDocsUpdate.OptionsColumn.AllowSize = False
        Me.ColDocsUpdate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsUpdate.OptionsColumn.ReadOnly = True
        Me.ColDocsUpdate.OptionsColumn.ShowCaption = False
        Me.ColDocsUpdate.OptionsColumn.TabStop = False
        Me.ColDocsUpdate.OptionsFilter.AllowAutoFilter = False
        Me.ColDocsUpdate.OptionsFilter.AllowFilter = False
        Me.ColDocsUpdate.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColDocsUpdate
        '
        Me.layoutViewField_ColDocsUpdate.EditorPreferredWidth = 344
        Me.layoutViewField_ColDocsUpdate.Location = New System.Drawing.Point(63, 88)
        Me.layoutViewField_ColDocsUpdate.MaxSize = New System.Drawing.Size(0, 26)
        Me.layoutViewField_ColDocsUpdate.MinSize = New System.Drawing.Size(77, 26)
        Me.layoutViewField_ColDocsUpdate.Name = "layoutViewField_ColDocsUpdate"
        Me.layoutViewField_ColDocsUpdate.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColDocsUpdate.Size = New System.Drawing.Size(417, 26)
        Me.layoutViewField_ColDocsUpdate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColDocsUpdate.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColDocsUpdate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColDocsUpdate.TextSize = New System.Drawing.Size(70, 13)
        Me.layoutViewField_ColDocsUpdate.TextToControlDistance = 0
        '
        'ColDocsContent
        '
        Me.ColDocsContent.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColDocsContent.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColDocsContent.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColDocsContent.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColDocsContent.AppearanceCell.Options.UseBackColor = True
        Me.ColDocsContent.AppearanceCell.Options.UseFont = True
        Me.ColDocsContent.AppearanceCell.Options.UseForeColor = True
        Me.ColDocsContent.AppearanceCell.Options.UseTextOptions = True
        Me.ColDocsContent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.ColDocsContent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColDocsContent.ColumnEdit = Me.RepmemDocsContent
        Me.ColDocsContent.FieldName = "Content"
        Me.ColDocsContent.LayoutViewField = Me.layoutViewField_ColDocsContent
        Me.ColDocsContent.Name = "ColDocsContent"
        Me.ColDocsContent.OptionsColumn.AllowEdit = False
        Me.ColDocsContent.OptionsColumn.AllowFocus = False
        Me.ColDocsContent.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsContent.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsContent.OptionsColumn.AllowMove = False
        Me.ColDocsContent.OptionsColumn.AllowShowHide = False
        Me.ColDocsContent.OptionsColumn.AllowSize = False
        Me.ColDocsContent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDocsContent.OptionsColumn.ReadOnly = True
        Me.ColDocsContent.OptionsColumn.ShowCaption = False
        Me.ColDocsContent.OptionsColumn.TabStop = False
        Me.ColDocsContent.OptionsFilter.AllowAutoFilter = False
        Me.ColDocsContent.OptionsFilter.AllowFilter = False
        Me.ColDocsContent.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'RepmemDocsContent
        '
        Me.RepmemDocsContent.AllowFocused = False
        Me.RepmemDocsContent.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepmemDocsContent.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.RepmemDocsContent.Appearance.Options.UseFont = True
        Me.RepmemDocsContent.Appearance.Options.UseForeColor = True
        Me.RepmemDocsContent.Appearance.Options.UseTextOptions = True
        Me.RepmemDocsContent.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.RepmemDocsContent.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.RepmemDocsContent.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.RepmemDocsContent.Name = "RepmemDocsContent"
        Me.RepmemDocsContent.ReadOnly = True
        Me.RepmemDocsContent.ScrollBars = System.Windows.Forms.ScrollBars.None
        '
        'layoutViewField_ColDocsContent
        '
        Me.layoutViewField_ColDocsContent.EditorPreferredWidth = 414
        Me.layoutViewField_ColDocsContent.Location = New System.Drawing.Point(63, 114)
        Me.layoutViewField_ColDocsContent.MaxSize = New System.Drawing.Size(0, 78)
        Me.layoutViewField_ColDocsContent.MinSize = New System.Drawing.Size(99, 78)
        Me.layoutViewField_ColDocsContent.Name = "layoutViewField_ColDocsContent"
        Me.layoutViewField_ColDocsContent.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColDocsContent.Size = New System.Drawing.Size(417, 78)
        Me.layoutViewField_ColDocsContent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColDocsContent.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColDocsContent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColDocsContent.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutViewField_ColDocsContent.TextToControlDistance = 0
        Me.layoutViewField_ColDocsContent.TextVisible = False
        '
        'LayoutViewCard1
        '
        Me.LayoutViewCard1.CustomizationFormText = "TemplateCard"
        Me.LayoutViewCard1.GroupBordersVisible = False
        Me.LayoutViewCard1.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.LayoutViewCard1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutViewField_ColDocsTitle, Me.layoutViewField_ColDocsNameForm, Me.layoutViewField_ColDocsAuthor, Me.layoutViewField_ColDocsUpdate, Me.layoutViewField_ColDocsContent, Me.layoutViewField_ColDocsTile})
        Me.LayoutViewCard1.Name = "LayoutViewCard1"
        Me.LayoutViewCard1.OptionsItemText.TextToControlDistance = 5
        Me.LayoutViewCard1.Text = "TemplateCard"
        '
        'INDpnlRegsResults
        '
        Me.INDpnlRegsResults.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlRegsResults.Controls.Add(Me.INDlblRegsTotal)
        Me.INDpnlRegsResults.Controls.Add(Me.INDpnlRegsTitle)
        Me.INDpnlRegsResults.Controls.Add(Me.INDgdcRegs)
        Me.INDpnlRegsResults.Location = New System.Drawing.Point(335, 192)
        Me.INDpnlRegsResults.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlRegsResults.MinimumSize = New System.Drawing.Size(333, 450)
        Me.INDpnlRegsResults.Name = "INDpnlRegsResults"
        Me.INDpnlRegsResults.Size = New System.Drawing.Size(333, 488)
        Me.INDpnlRegsResults.TabIndex = 3
        Me.INDpnlRegsResults.Visible = False
        '
        'INDlblRegsTotal
        '
        Me.INDlblRegsTotal.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 16.0!)
        Me.INDlblRegsTotal.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlblRegsTotal.Appearance.Options.UseFont = True
        Me.INDlblRegsTotal.Appearance.Options.UseForeColor = True
        Me.INDlblRegsTotal.Appearance.Options.UseTextOptions = True
        Me.INDlblRegsTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblRegsTotal.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblRegsTotal.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblRegsTotal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblRegsTotal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDlblRegsTotal.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDlblRegsTotal.Location = New System.Drawing.Point(0, 438)
        Me.INDlblRegsTotal.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblRegsTotal.Name = "INDlblRegsTotal"
        Me.INDlblRegsTotal.Size = New System.Drawing.Size(333, 50)
        Me.INDlblRegsTotal.TabIndex = 3
        Me.INDlblRegsTotal.Tag = "Total:  {0}  -  Ver más  >"
        '
        'INDpnlRegsTitle
        '
        Me.INDpnlRegsTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlRegsTitle.Controls.Add(Me.INDlnRegsSelector)
        Me.INDpnlRegsTitle.Controls.Add(Me.INDlblRegsTitle)
        Me.INDpnlRegsTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpnlRegsTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDpnlRegsTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlRegsTitle.Name = "INDpnlRegsTitle"
        Me.INDpnlRegsTitle.Size = New System.Drawing.Size(333, 50)
        Me.INDpnlRegsTitle.TabIndex = 2
        '
        'INDlnRegsSelector
        '
        Me.INDlnRegsSelector.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.INDlnRegsSelector.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlnRegsSelector.Appearance.Options.UseBackColor = True
        Me.INDlnRegsSelector.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlnRegsSelector.Location = New System.Drawing.Point(118, 12)
        Me.INDlnRegsSelector.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlnRegsSelector.Name = "INDlnRegsSelector"
        Me.INDlnRegsSelector.Size = New System.Drawing.Size(99, 3)
        Me.INDlnRegsSelector.TabIndex = 4
        Me.INDlnRegsSelector.Visible = False
        '
        'INDlblRegsTitle
        '
        Me.INDlblRegsTitle.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDlblRegsTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblRegsTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlblRegsTitle.Appearance.Options.UseBackColor = True
        Me.INDlblRegsTitle.Appearance.Options.UseFont = True
        Me.INDlblRegsTitle.Appearance.Options.UseForeColor = True
        Me.INDlblRegsTitle.Appearance.Options.UseTextOptions = True
        Me.INDlblRegsTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblRegsTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblRegsTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblRegsTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlblRegsTitle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDlblRegsTitle.Location = New System.Drawing.Point(0, 15)
        Me.INDlblRegsTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblRegsTitle.Name = "INDlblRegsTitle"
        Me.INDlblRegsTitle.Size = New System.Drawing.Size(333, 35)
        Me.INDlblRegsTitle.TabIndex = 1
        Me.INDlblRegsTitle.Text = "Registros"
        '
        'INDgdcRegs
        '
        Me.INDgdcRegs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDgdcRegs.Location = New System.Drawing.Point(0, 50)
        Me.INDgdcRegs.MainView = Me.INDgdvRegs
        Me.INDgdcRegs.Margin = New System.Windows.Forms.Padding(0)
        Me.INDgdcRegs.Name = "INDgdcRegs"
        Me.INDgdcRegs.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepimlTileRegs, Me.RepmemRegsContent})
        Me.INDgdcRegs.Size = New System.Drawing.Size(333, 384)
        Me.INDgdcRegs.TabIndex = 0
        Me.INDgdcRegs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgdvRegs})
        '
        'INDgdvRegs
        '
        Me.INDgdvRegs.Appearance.ViewCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.INDgdvRegs.Appearance.ViewCaption.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.INDgdvRegs.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 15.75!)
        Me.INDgdvRegs.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Gray
        Me.INDgdvRegs.Appearance.ViewCaption.Options.UseBackColor = True
        Me.INDgdvRegs.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgdvRegs.Appearance.ViewCaption.Options.UseForeColor = True
        Me.INDgdvRegs.Appearance.ViewCaption.Options.UseTextOptions = True
        Me.INDgdvRegs.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDgdvRegs.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDgdvRegs.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDgdvRegs.CardMinSize = New System.Drawing.Size(503, 234)
        Me.INDgdvRegs.Columns.AddRange(New DevExpress.XtraGrid.Columns.LayoutViewColumn() {Me.ColRegsTile, Me.ColRegsTitle, Me.ColRegsNameForm, Me.ColRegsAuthor, Me.ColRegsUpdate, Me.ColRegsContent})
        Me.INDgdvRegs.GridControl = Me.INDgdcRegs
        Me.INDgdvRegs.Name = "INDgdvRegs"
        Me.INDgdvRegs.OptionsBehavior.AutoFocusCardOnScrolling = True
        Me.INDgdvRegs.OptionsBehavior.Editable = False
        Me.INDgdvRegs.OptionsBehavior.ReadOnly = True
        Me.INDgdvRegs.OptionsBehavior.ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.[Auto]
        Me.INDgdvRegs.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Horizontal
        Me.INDgdvRegs.OptionsMultiRecordMode.StretchCardToViewWidth = True
        Me.INDgdvRegs.OptionsView.AllowHotTrackFields = False
        Me.INDgdvRegs.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        Me.INDgdvRegs.OptionsView.CardArrangeRule = DevExpress.XtraGrid.Views.Layout.LayoutCardArrangeRule.AllowPartialCards
        Me.INDgdvRegs.OptionsView.CardsAlignment = DevExpress.XtraGrid.Views.Layout.CardsAlignment.Near
        Me.INDgdvRegs.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDgdvRegs.OptionsView.ShowCardBorderIfCaptionHidden = False
        Me.INDgdvRegs.OptionsView.ShowCardCaption = False
        Me.INDgdvRegs.OptionsView.ShowCardExpandButton = False
        Me.INDgdvRegs.OptionsView.ShowCardLines = False
        Me.INDgdvRegs.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDgdvRegs.OptionsView.ShowHeaderPanel = False
        Me.INDgdvRegs.OptionsView.ShowViewCaption = True
        Me.INDgdvRegs.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Column
        Me.INDgdvRegs.PaintStyleName = "Skin"
        Me.INDgdvRegs.TemplateCard = Me.LayoutViewCard2
        Me.INDgdvRegs.ViewCaption = "Sin resultados"
        '
        'ColRegsTile
        '
        Me.ColRegsTile.ColumnEdit = Me.RepimlTileRegs
        Me.ColRegsTile.FieldName = "JournalVoucher"
        Me.ColRegsTile.LayoutViewField = Me.layoutViewField_ColRegsTile
        Me.ColRegsTile.Name = "ColRegsTile"
        Me.ColRegsTile.OptionsColumn.AllowEdit = False
        Me.ColRegsTile.OptionsColumn.AllowFocus = False
        Me.ColRegsTile.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsTile.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsTile.OptionsColumn.AllowMove = False
        Me.ColRegsTile.OptionsColumn.AllowShowHide = False
        Me.ColRegsTile.OptionsColumn.AllowSize = False
        Me.ColRegsTile.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsTile.OptionsColumn.ReadOnly = True
        Me.ColRegsTile.OptionsColumn.ShowCaption = False
        Me.ColRegsTile.OptionsColumn.TabStop = False
        Me.ColRegsTile.OptionsFilter.AllowAutoFilter = False
        Me.ColRegsTile.OptionsFilter.AllowFilter = False
        Me.ColRegsTile.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'RepimlTileRegs
        '
        Me.RepimlTileRegs.AllowFocused = False
        Me.RepimlTileRegs.AutoHeight = False
        Me.RepimlTileRegs.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepimlTileRegs.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2)})
        Me.RepimlTileRegs.LargeImages = Me.INDimcTiles
        Me.RepimlTileRegs.Name = "RepimlTileRegs"
        Me.RepimlTileRegs.ReadOnly = True
        '
        'layoutViewField_ColRegsTile
        '
        Me.layoutViewField_ColRegsTile.AllowHtmlStringInCaption = True
        Me.layoutViewField_ColRegsTile.EditorPreferredWidth = 63
        Me.layoutViewField_ColRegsTile.Location = New System.Drawing.Point(0, 0)
        Me.layoutViewField_ColRegsTile.MaxSize = New System.Drawing.Size(63, 63)
        Me.layoutViewField_ColRegsTile.MinSize = New System.Drawing.Size(63, 63)
        Me.layoutViewField_ColRegsTile.Name = "layoutViewField_ColRegsTile"
        Me.layoutViewField_ColRegsTile.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColRegsTile.Size = New System.Drawing.Size(63, 214)
        Me.layoutViewField_ColRegsTile.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColRegsTile.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColRegsTile.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutViewField_ColRegsTile.TextToControlDistance = 0
        Me.layoutViewField_ColRegsTile.TextVisible = False
        '
        'ColRegsTitle
        '
        Me.ColRegsTitle.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro
        Me.ColRegsTitle.AppearanceCell.BackColor2 = System.Drawing.Color.Gainsboro
        Me.ColRegsTitle.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 15.75!, System.Drawing.FontStyle.Underline)
        Me.ColRegsTitle.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.ColRegsTitle.AppearanceCell.Options.UseBackColor = True
        Me.ColRegsTitle.AppearanceCell.Options.UseFont = True
        Me.ColRegsTitle.AppearanceCell.Options.UseForeColor = True
        Me.ColRegsTitle.AppearanceCell.Options.UseTextOptions = True
        Me.ColRegsTitle.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColRegsTitle.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRegsTitle.FieldName = "Title"
        Me.ColRegsTitle.LayoutViewField = Me.layoutViewField_ColRegsTitle
        Me.ColRegsTitle.Name = "ColRegsTitle"
        Me.ColRegsTitle.OptionsColumn.AllowEdit = False
        Me.ColRegsTitle.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.ColRegsTitle.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsTitle.OptionsColumn.AllowMove = False
        Me.ColRegsTitle.OptionsColumn.AllowShowHide = False
        Me.ColRegsTitle.OptionsColumn.AllowSize = False
        Me.ColRegsTitle.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsTitle.OptionsColumn.ReadOnly = True
        Me.ColRegsTitle.OptionsColumn.ShowCaption = False
        Me.ColRegsTitle.OptionsColumn.ShowInCustomizationForm = False
        Me.ColRegsTitle.OptionsFilter.AllowAutoFilter = False
        Me.ColRegsTitle.OptionsFilter.AllowFilter = False
        Me.ColRegsTitle.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColRegsTitle
        '
        Me.layoutViewField_ColRegsTitle.EditorPreferredWidth = 417
        Me.layoutViewField_ColRegsTitle.Location = New System.Drawing.Point(63, 0)
        Me.layoutViewField_ColRegsTitle.MaxSize = New System.Drawing.Size(0, 36)
        Me.layoutViewField_ColRegsTitle.MinSize = New System.Drawing.Size(30, 36)
        Me.layoutViewField_ColRegsTitle.Name = "layoutViewField_ColRegsTitle"
        Me.layoutViewField_ColRegsTitle.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColRegsTitle.Size = New System.Drawing.Size(420, 36)
        Me.layoutViewField_ColRegsTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColRegsTitle.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColRegsTitle.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutViewField_ColRegsTitle.TextVisible = False
        '
        'ColRegsNameForm
        '
        Me.ColRegsNameForm.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColRegsNameForm.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColRegsNameForm.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColRegsNameForm.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColRegsNameForm.AppearanceCell.Options.UseBackColor = True
        Me.ColRegsNameForm.AppearanceCell.Options.UseFont = True
        Me.ColRegsNameForm.AppearanceCell.Options.UseForeColor = True
        Me.ColRegsNameForm.AppearanceCell.Options.UseTextOptions = True
        Me.ColRegsNameForm.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColRegsNameForm.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRegsNameForm.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColRegsNameForm.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColRegsNameForm.AppearanceHeader.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColRegsNameForm.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColRegsNameForm.AppearanceHeader.ForeColor = System.Drawing.Color.Black
        Me.ColRegsNameForm.AppearanceHeader.Options.UseBackColor = True
        Me.ColRegsNameForm.AppearanceHeader.Options.UseFont = True
        Me.ColRegsNameForm.AppearanceHeader.Options.UseForeColor = True
        Me.ColRegsNameForm.AppearanceHeader.Options.UseTextOptions = True
        Me.ColRegsNameForm.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColRegsNameForm.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRegsNameForm.Caption = "Funcional"
        Me.ColRegsNameForm.FieldName = "NameForm"
        Me.ColRegsNameForm.LayoutViewField = Me.layoutViewField_ColRegsNameForm
        Me.ColRegsNameForm.Name = "ColRegsNameForm"
        Me.ColRegsNameForm.OptionsColumn.AllowEdit = False
        Me.ColRegsNameForm.OptionsColumn.AllowFocus = False
        Me.ColRegsNameForm.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsNameForm.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsNameForm.OptionsColumn.AllowMove = False
        Me.ColRegsNameForm.OptionsColumn.AllowShowHide = False
        Me.ColRegsNameForm.OptionsColumn.AllowSize = False
        Me.ColRegsNameForm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsNameForm.OptionsColumn.ReadOnly = True
        Me.ColRegsNameForm.OptionsColumn.ShowCaption = False
        Me.ColRegsNameForm.OptionsColumn.TabStop = False
        Me.ColRegsNameForm.OptionsFilter.AllowAutoFilter = False
        Me.ColRegsNameForm.OptionsFilter.AllowFilter = False
        Me.ColRegsNameForm.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColRegsNameForm
        '
        Me.layoutViewField_ColRegsNameForm.EditorPreferredWidth = 333
        Me.layoutViewField_ColRegsNameForm.Location = New System.Drawing.Point(63, 36)
        Me.layoutViewField_ColRegsNameForm.MaxSize = New System.Drawing.Size(0, 26)
        Me.layoutViewField_ColRegsNameForm.MinSize = New System.Drawing.Size(77, 26)
        Me.layoutViewField_ColRegsNameForm.Name = "layoutViewField_ColRegsNameForm"
        Me.layoutViewField_ColRegsNameForm.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColRegsNameForm.Size = New System.Drawing.Size(420, 26)
        Me.layoutViewField_ColRegsNameForm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColRegsNameForm.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColRegsNameForm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColRegsNameForm.TextSize = New System.Drawing.Size(79, 20)
        Me.layoutViewField_ColRegsNameForm.TextToControlDistance = 5
        '
        'ColRegsAuthor
        '
        Me.ColRegsAuthor.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColRegsAuthor.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColRegsAuthor.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColRegsAuthor.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColRegsAuthor.AppearanceCell.Options.UseBackColor = True
        Me.ColRegsAuthor.AppearanceCell.Options.UseFont = True
        Me.ColRegsAuthor.AppearanceCell.Options.UseForeColor = True
        Me.ColRegsAuthor.AppearanceCell.Options.UseTextOptions = True
        Me.ColRegsAuthor.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColRegsAuthor.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRegsAuthor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColRegsAuthor.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColRegsAuthor.AppearanceHeader.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColRegsAuthor.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColRegsAuthor.AppearanceHeader.ForeColor = System.Drawing.Color.Black
        Me.ColRegsAuthor.AppearanceHeader.Options.UseBackColor = True
        Me.ColRegsAuthor.AppearanceHeader.Options.UseFont = True
        Me.ColRegsAuthor.AppearanceHeader.Options.UseForeColor = True
        Me.ColRegsAuthor.AppearanceHeader.Options.UseTextOptions = True
        Me.ColRegsAuthor.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColRegsAuthor.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRegsAuthor.Caption = "Autor"
        Me.ColRegsAuthor.FieldName = "CreationUser"
        Me.ColRegsAuthor.LayoutViewField = Me.layoutViewField_ColRegsAuthor
        Me.ColRegsAuthor.Name = "ColRegsAuthor"
        Me.ColRegsAuthor.OptionsColumn.AllowEdit = False
        Me.ColRegsAuthor.OptionsColumn.AllowFocus = False
        Me.ColRegsAuthor.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsAuthor.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsAuthor.OptionsColumn.AllowMove = False
        Me.ColRegsAuthor.OptionsColumn.AllowShowHide = False
        Me.ColRegsAuthor.OptionsColumn.AllowSize = False
        Me.ColRegsAuthor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsAuthor.OptionsColumn.ReadOnly = True
        Me.ColRegsAuthor.OptionsColumn.ShowCaption = False
        Me.ColRegsAuthor.OptionsColumn.TabStop = False
        Me.ColRegsAuthor.OptionsFilter.AllowAutoFilter = False
        Me.ColRegsAuthor.OptionsFilter.AllowFilter = False
        Me.ColRegsAuthor.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColRegsAuthor
        '
        Me.layoutViewField_ColRegsAuthor.EditorPreferredWidth = 333
        Me.layoutViewField_ColRegsAuthor.Location = New System.Drawing.Point(63, 62)
        Me.layoutViewField_ColRegsAuthor.MaxSize = New System.Drawing.Size(0, 26)
        Me.layoutViewField_ColRegsAuthor.MinSize = New System.Drawing.Size(77, 26)
        Me.layoutViewField_ColRegsAuthor.Name = "layoutViewField_ColRegsAuthor"
        Me.layoutViewField_ColRegsAuthor.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColRegsAuthor.Size = New System.Drawing.Size(420, 26)
        Me.layoutViewField_ColRegsAuthor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColRegsAuthor.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColRegsAuthor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColRegsAuthor.TextSize = New System.Drawing.Size(79, 20)
        Me.layoutViewField_ColRegsAuthor.TextToControlDistance = 5
        '
        'ColRegsUpdate
        '
        Me.ColRegsUpdate.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColRegsUpdate.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColRegsUpdate.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColRegsUpdate.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColRegsUpdate.AppearanceCell.Options.UseBackColor = True
        Me.ColRegsUpdate.AppearanceCell.Options.UseFont = True
        Me.ColRegsUpdate.AppearanceCell.Options.UseForeColor = True
        Me.ColRegsUpdate.AppearanceCell.Options.UseTextOptions = True
        Me.ColRegsUpdate.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColRegsUpdate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRegsUpdate.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColRegsUpdate.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColRegsUpdate.AppearanceHeader.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColRegsUpdate.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColRegsUpdate.AppearanceHeader.ForeColor = System.Drawing.Color.Black
        Me.ColRegsUpdate.AppearanceHeader.Options.UseBackColor = True
        Me.ColRegsUpdate.AppearanceHeader.Options.UseFont = True
        Me.ColRegsUpdate.AppearanceHeader.Options.UseForeColor = True
        Me.ColRegsUpdate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColRegsUpdate.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColRegsUpdate.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColRegsUpdate.Caption = "Modificado"
        Me.ColRegsUpdate.DisplayFormat.FormatString = "G"
        Me.ColRegsUpdate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ColRegsUpdate.FieldName = "Update"
        Me.ColRegsUpdate.LayoutViewField = Me.layoutViewField_ColRegsUpdate
        Me.ColRegsUpdate.Name = "ColRegsUpdate"
        Me.ColRegsUpdate.OptionsColumn.AllowEdit = False
        Me.ColRegsUpdate.OptionsColumn.AllowFocus = False
        Me.ColRegsUpdate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsUpdate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsUpdate.OptionsColumn.AllowMove = False
        Me.ColRegsUpdate.OptionsColumn.AllowShowHide = False
        Me.ColRegsUpdate.OptionsColumn.AllowSize = False
        Me.ColRegsUpdate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsUpdate.OptionsColumn.ReadOnly = True
        Me.ColRegsUpdate.OptionsColumn.ShowCaption = False
        Me.ColRegsUpdate.OptionsColumn.TabStop = False
        Me.ColRegsUpdate.OptionsFilter.AllowAutoFilter = False
        Me.ColRegsUpdate.OptionsFilter.AllowFilter = False
        Me.ColRegsUpdate.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColRegsUpdate
        '
        Me.layoutViewField_ColRegsUpdate.EditorPreferredWidth = 333
        Me.layoutViewField_ColRegsUpdate.Location = New System.Drawing.Point(63, 88)
        Me.layoutViewField_ColRegsUpdate.MaxSize = New System.Drawing.Size(0, 26)
        Me.layoutViewField_ColRegsUpdate.MinSize = New System.Drawing.Size(77, 26)
        Me.layoutViewField_ColRegsUpdate.Name = "layoutViewField_ColRegsUpdate"
        Me.layoutViewField_ColRegsUpdate.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColRegsUpdate.Size = New System.Drawing.Size(420, 26)
        Me.layoutViewField_ColRegsUpdate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColRegsUpdate.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColRegsUpdate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColRegsUpdate.TextSize = New System.Drawing.Size(79, 20)
        Me.layoutViewField_ColRegsUpdate.TextToControlDistance = 5
        '
        'ColRegsContent
        '
        Me.ColRegsContent.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColRegsContent.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColRegsContent.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColRegsContent.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColRegsContent.AppearanceCell.Options.UseBackColor = True
        Me.ColRegsContent.AppearanceCell.Options.UseFont = True
        Me.ColRegsContent.AppearanceCell.Options.UseForeColor = True
        Me.ColRegsContent.AppearanceCell.Options.UseTextOptions = True
        Me.ColRegsContent.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.ColRegsContent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColRegsContent.ColumnEdit = Me.RepmemRegsContent
        Me.ColRegsContent.FieldName = "Content"
        Me.ColRegsContent.LayoutViewField = Me.layoutViewField_ColRegsContent
        Me.ColRegsContent.Name = "ColRegsContent"
        Me.ColRegsContent.OptionsColumn.AllowEdit = False
        Me.ColRegsContent.OptionsColumn.AllowFocus = False
        Me.ColRegsContent.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsContent.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsContent.OptionsColumn.AllowMove = False
        Me.ColRegsContent.OptionsColumn.AllowShowHide = False
        Me.ColRegsContent.OptionsColumn.AllowSize = False
        Me.ColRegsContent.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColRegsContent.OptionsColumn.ReadOnly = True
        Me.ColRegsContent.OptionsColumn.ShowCaption = False
        Me.ColRegsContent.OptionsColumn.TabStop = False
        Me.ColRegsContent.OptionsFilter.AllowAutoFilter = False
        Me.ColRegsContent.OptionsFilter.AllowFilter = False
        Me.ColRegsContent.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'RepmemRegsContent
        '
        Me.RepmemRegsContent.AllowFocused = False
        Me.RepmemRegsContent.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepmemRegsContent.Appearance.ForeColor = System.Drawing.Color.Gray
        Me.RepmemRegsContent.Appearance.Options.UseFont = True
        Me.RepmemRegsContent.Appearance.Options.UseForeColor = True
        Me.RepmemRegsContent.Appearance.Options.UseTextOptions = True
        Me.RepmemRegsContent.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.RepmemRegsContent.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.RepmemRegsContent.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.RepmemRegsContent.Name = "RepmemRegsContent"
        Me.RepmemRegsContent.ReadOnly = True
        Me.RepmemRegsContent.ScrollBars = System.Windows.Forms.ScrollBars.None
        '
        'layoutViewField_ColRegsContent
        '
        Me.layoutViewField_ColRegsContent.EditorPreferredWidth = 417
        Me.layoutViewField_ColRegsContent.Location = New System.Drawing.Point(63, 114)
        Me.layoutViewField_ColRegsContent.MaxSize = New System.Drawing.Size(0, 98)
        Me.layoutViewField_ColRegsContent.MinSize = New System.Drawing.Size(99, 98)
        Me.layoutViewField_ColRegsContent.Name = "layoutViewField_ColRegsContent"
        Me.layoutViewField_ColRegsContent.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColRegsContent.Size = New System.Drawing.Size(420, 100)
        Me.layoutViewField_ColRegsContent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColRegsContent.Spacing = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColRegsContent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColRegsContent.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutViewField_ColRegsContent.TextToControlDistance = 0
        Me.layoutViewField_ColRegsContent.TextVisible = False
        '
        'LayoutViewCard2
        '
        Me.LayoutViewCard2.CustomizationFormText = "TemplateCard"
        Me.LayoutViewCard2.GroupBordersVisible = False
        Me.LayoutViewCard2.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.LayoutViewCard2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutViewField_ColRegsTitle, Me.layoutViewField_ColRegsNameForm, Me.layoutViewField_ColRegsAuthor, Me.layoutViewField_ColRegsUpdate, Me.layoutViewField_ColRegsContent, Me.layoutViewField_ColRegsTile})
        Me.LayoutViewCard2.Name = "LayoutViewCard2"
        Me.LayoutViewCard2.OptionsItemText.TextToControlDistance = 5
        Me.LayoutViewCard2.Text = "TemplateCard"
        '
        'INDpnlMenuResults
        '
        Me.INDpnlMenuResults.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlMenuResults.Controls.Add(Me.INDlblMenuTotal)
        Me.INDpnlMenuResults.Controls.Add(Me.INDpnlMenuTitle)
        Me.INDpnlMenuResults.Controls.Add(Me.INDgdcMenu)
        Me.INDpnlMenuResults.Location = New System.Drawing.Point(2, 192)
        Me.INDpnlMenuResults.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlMenuResults.MinimumSize = New System.Drawing.Size(333, 450)
        Me.INDpnlMenuResults.Name = "INDpnlMenuResults"
        Me.INDpnlMenuResults.Size = New System.Drawing.Size(333, 488)
        Me.INDpnlMenuResults.TabIndex = 2
        '
        'INDlblMenuTotal
        '
        Me.INDlblMenuTotal.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 16.0!)
        Me.INDlblMenuTotal.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlblMenuTotal.Appearance.Options.UseFont = True
        Me.INDlblMenuTotal.Appearance.Options.UseForeColor = True
        Me.INDlblMenuTotal.Appearance.Options.UseTextOptions = True
        Me.INDlblMenuTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblMenuTotal.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblMenuTotal.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblMenuTotal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblMenuTotal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDlblMenuTotal.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDlblMenuTotal.Location = New System.Drawing.Point(0, 438)
        Me.INDlblMenuTotal.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblMenuTotal.Name = "INDlblMenuTotal"
        Me.INDlblMenuTotal.Size = New System.Drawing.Size(333, 50)
        Me.INDlblMenuTotal.TabIndex = 3
        Me.INDlblMenuTotal.Tag = "Total:  {0}  -  Ver más  >"
        '
        'INDpnlMenuTitle
        '
        Me.INDpnlMenuTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpnlMenuTitle.Controls.Add(Me.INDlnMenuSelector)
        Me.INDpnlMenuTitle.Controls.Add(Me.INDlblMenuTitle)
        Me.INDpnlMenuTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDpnlMenuTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDpnlMenuTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.INDpnlMenuTitle.Name = "INDpnlMenuTitle"
        Me.INDpnlMenuTitle.Size = New System.Drawing.Size(333, 50)
        Me.INDpnlMenuTitle.TabIndex = 2
        '
        'INDlnMenuSelector
        '
        Me.INDlnMenuSelector.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.INDlnMenuSelector.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlnMenuSelector.Appearance.Options.UseBackColor = True
        Me.INDlnMenuSelector.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlnMenuSelector.Location = New System.Drawing.Point(132, 12)
        Me.INDlnMenuSelector.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlnMenuSelector.Name = "INDlnMenuSelector"
        Me.INDlnMenuSelector.Size = New System.Drawing.Size(69, 3)
        Me.INDlnMenuSelector.TabIndex = 3
        Me.INDlnMenuSelector.Visible = False
        '
        'INDlblMenuTitle
        '
        Me.INDlblMenuTitle.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDlblMenuTitle.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblMenuTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.INDlblMenuTitle.Appearance.Options.UseBackColor = True
        Me.INDlblMenuTitle.Appearance.Options.UseFont = True
        Me.INDlblMenuTitle.Appearance.Options.UseForeColor = True
        Me.INDlblMenuTitle.Appearance.Options.UseTextOptions = True
        Me.INDlblMenuTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDlblMenuTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDlblMenuTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.INDlblMenuTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDlblMenuTitle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.INDlblMenuTitle.Location = New System.Drawing.Point(0, 15)
        Me.INDlblMenuTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.INDlblMenuTitle.Name = "INDlblMenuTitle"
        Me.INDlblMenuTitle.Size = New System.Drawing.Size(333, 35)
        Me.INDlblMenuTitle.TabIndex = 1
        Me.INDlblMenuTitle.Text = "Menú"
        '
        'INDgdcMenu
        '
        Me.INDgdcMenu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.INDgdcMenu.Location = New System.Drawing.Point(0, 50)
        Me.INDgdcMenu.MainView = Me.INDgdvMenu
        Me.INDgdcMenu.Margin = New System.Windows.Forms.Padding(0)
        Me.INDgdcMenu.Name = "INDgdcMenu"
        Me.INDgdcMenu.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepimlTileMenu})
        Me.INDgdcMenu.Size = New System.Drawing.Size(333, 384)
        Me.INDgdcMenu.TabIndex = 0
        Me.INDgdcMenu.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgdvMenu})
        '
        'INDgdvMenu
        '
        Me.INDgdvMenu.Appearance.CardCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.INDgdvMenu.Appearance.CardCaption.ForeColor = System.Drawing.Color.Gray
        Me.INDgdvMenu.Appearance.CardCaption.Options.UseFont = True
        Me.INDgdvMenu.Appearance.CardCaption.Options.UseForeColor = True
        Me.INDgdvMenu.Appearance.CardCaption.Options.UseTextOptions = True
        Me.INDgdvMenu.Appearance.CardCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDgdvMenu.Appearance.CardCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDgdvMenu.Appearance.FocusedCardCaption.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.INDgdvMenu.Appearance.FocusedCardCaption.ForeColor = System.Drawing.Color.Black
        Me.INDgdvMenu.Appearance.FocusedCardCaption.Options.UseFont = True
        Me.INDgdvMenu.Appearance.FocusedCardCaption.Options.UseForeColor = True
        Me.INDgdvMenu.Appearance.FocusedCardCaption.Options.UseTextOptions = True
        Me.INDgdvMenu.Appearance.FocusedCardCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDgdvMenu.Appearance.FocusedCardCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDgdvMenu.Appearance.SelectedCardCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold)
        Me.INDgdvMenu.Appearance.SelectedCardCaption.ForeColor = System.Drawing.Color.Black
        Me.INDgdvMenu.Appearance.SelectedCardCaption.Options.UseFont = True
        Me.INDgdvMenu.Appearance.SelectedCardCaption.Options.UseForeColor = True
        Me.INDgdvMenu.Appearance.SelectedCardCaption.Options.UseTextOptions = True
        Me.INDgdvMenu.Appearance.SelectedCardCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDgdvMenu.Appearance.SelectedCardCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDgdvMenu.Appearance.ViewCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.INDgdvMenu.Appearance.ViewCaption.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.INDgdvMenu.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 15.75!)
        Me.INDgdvMenu.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Gray
        Me.INDgdvMenu.Appearance.ViewCaption.Options.UseBackColor = True
        Me.INDgdvMenu.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgdvMenu.Appearance.ViewCaption.Options.UseForeColor = True
        Me.INDgdvMenu.Appearance.ViewCaption.Options.UseTextOptions = True
        Me.INDgdvMenu.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDgdvMenu.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDgdvMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDgdvMenu.CardMinSize = New System.Drawing.Size(450, 115)
        Me.INDgdvMenu.Columns.AddRange(New DevExpress.XtraGrid.Columns.LayoutViewColumn() {Me.ColMenuTile, Me.ColMenuNameForm, Me.ColMenuNameModule, Me.ColMenuNameGroup})
        Me.INDgdvMenu.GridControl = Me.INDgdcMenu
        Me.INDgdvMenu.Name = "INDgdvMenu"
        Me.INDgdvMenu.OptionsBehavior.AutoFocusCardOnScrolling = True
        Me.INDgdvMenu.OptionsBehavior.Editable = False
        Me.INDgdvMenu.OptionsBehavior.ReadOnly = True
        Me.INDgdvMenu.OptionsBehavior.ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.[Auto]
        Me.INDgdvMenu.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Horizontal
        Me.INDgdvMenu.OptionsMultiRecordMode.StretchCardToViewWidth = True
        Me.INDgdvMenu.OptionsView.AllowHotTrackFields = False
        Me.INDgdvMenu.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        Me.INDgdvMenu.OptionsView.CardArrangeRule = DevExpress.XtraGrid.Views.Layout.LayoutCardArrangeRule.AllowPartialCards
        Me.INDgdvMenu.OptionsView.CardsAlignment = DevExpress.XtraGrid.Views.Layout.CardsAlignment.Near
        Me.INDgdvMenu.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDgdvMenu.OptionsView.ShowCardBorderIfCaptionHidden = False
        Me.INDgdvMenu.OptionsView.ShowCardCaption = False
        Me.INDgdvMenu.OptionsView.ShowCardExpandButton = False
        Me.INDgdvMenu.OptionsView.ShowCardLines = False
        Me.INDgdvMenu.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.INDgdvMenu.OptionsView.ShowHeaderPanel = False
        Me.INDgdvMenu.OptionsView.ShowViewCaption = True
        Me.INDgdvMenu.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Column
        Me.INDgdvMenu.PaintStyleName = "Skin"
        Me.INDgdvMenu.TemplateCard = Me.LayoutViewCard3
        Me.INDgdvMenu.ViewCaption = "Sin resultados"
        '
        'ColMenuTile
        '
        Me.ColMenuTile.ColumnEdit = Me.RepimlTileMenu
        Me.ColMenuTile.FieldName = "EnableEmbedded"
        Me.ColMenuTile.LayoutViewField = Me.layoutViewField_ColMenuTile
        Me.ColMenuTile.Name = "ColMenuTile"
        Me.ColMenuTile.OptionsColumn.AllowEdit = False
        Me.ColMenuTile.OptionsColumn.AllowFocus = False
        Me.ColMenuTile.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuTile.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuTile.OptionsColumn.AllowMove = False
        Me.ColMenuTile.OptionsColumn.AllowShowHide = False
        Me.ColMenuTile.OptionsColumn.AllowSize = False
        Me.ColMenuTile.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuTile.OptionsColumn.ReadOnly = True
        Me.ColMenuTile.OptionsColumn.ShowCaption = False
        Me.ColMenuTile.OptionsColumn.ShowInExpressionEditor = False
        Me.ColMenuTile.OptionsColumn.TabStop = False
        Me.ColMenuTile.OptionsFilter.AllowAutoFilter = False
        Me.ColMenuTile.OptionsFilter.AllowFilter = False
        Me.ColMenuTile.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'RepimlTileMenu
        '
        Me.RepimlTileMenu.AllowFocused = False
        Me.RepimlTileMenu.AutoHeight = False
        Me.RepimlTileMenu.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepimlTileMenu.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", False, 0)})
        Me.RepimlTileMenu.LargeImages = Me.INDimcTiles
        Me.RepimlTileMenu.Name = "RepimlTileMenu"
        Me.RepimlTileMenu.ReadOnly = True
        '
        'layoutViewField_ColMenuTile
        '
        Me.layoutViewField_ColMenuTile.EditorPreferredWidth = 63
        Me.layoutViewField_ColMenuTile.Location = New System.Drawing.Point(0, 0)
        Me.layoutViewField_ColMenuTile.MaxSize = New System.Drawing.Size(63, 63)
        Me.layoutViewField_ColMenuTile.MinSize = New System.Drawing.Size(63, 63)
        Me.layoutViewField_ColMenuTile.Name = "layoutViewField_ColMenuTile"
        Me.layoutViewField_ColMenuTile.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.layoutViewField_ColMenuTile.Size = New System.Drawing.Size(63, 151)
        Me.layoutViewField_ColMenuTile.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColMenuTile.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutViewField_ColMenuTile.TextVisible = False
        '
        'ColMenuNameForm
        '
        Me.ColMenuNameForm.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro
        Me.ColMenuNameForm.AppearanceCell.BackColor2 = System.Drawing.Color.Gainsboro
        Me.ColMenuNameForm.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 15.75!, System.Drawing.FontStyle.Underline)
        Me.ColMenuNameForm.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(103, Byte), Integer))
        Me.ColMenuNameForm.AppearanceCell.Options.UseBackColor = True
        Me.ColMenuNameForm.AppearanceCell.Options.UseFont = True
        Me.ColMenuNameForm.AppearanceCell.Options.UseForeColor = True
        Me.ColMenuNameForm.AppearanceCell.Options.UseTextOptions = True
        Me.ColMenuNameForm.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColMenuNameForm.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColMenuNameForm.FieldName = "NameForm"
        Me.ColMenuNameForm.LayoutViewField = Me.layoutViewField_ColMenuNameForm
        Me.ColMenuNameForm.Name = "ColMenuNameForm"
        Me.ColMenuNameForm.OptionsColumn.AllowEdit = False
        Me.ColMenuNameForm.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.ColMenuNameForm.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuNameForm.OptionsColumn.AllowMove = False
        Me.ColMenuNameForm.OptionsColumn.AllowShowHide = False
        Me.ColMenuNameForm.OptionsColumn.AllowSize = False
        Me.ColMenuNameForm.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuNameForm.OptionsColumn.ReadOnly = True
        Me.ColMenuNameForm.OptionsColumn.ShowCaption = False
        Me.ColMenuNameForm.OptionsColumn.ShowInCustomizationForm = False
        Me.ColMenuNameForm.OptionsFilter.AllowAutoFilter = False
        Me.ColMenuNameForm.OptionsFilter.AllowFilter = False
        Me.ColMenuNameForm.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColMenuNameForm
        '
        Me.layoutViewField_ColMenuNameForm.EditorPreferredWidth = 364
        Me.layoutViewField_ColMenuNameForm.ImageOptions.ImageToTextDistance = 0
        Me.layoutViewField_ColMenuNameForm.Location = New System.Drawing.Point(63, 0)
        Me.layoutViewField_ColMenuNameForm.MaxSize = New System.Drawing.Size(0, 36)
        Me.layoutViewField_ColMenuNameForm.MinSize = New System.Drawing.Size(30, 36)
        Me.layoutViewField_ColMenuNameForm.Name = "layoutViewField_ColMenuNameForm"
        Me.layoutViewField_ColMenuNameForm.Padding = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColMenuNameForm.Size = New System.Drawing.Size(367, 36)
        Me.layoutViewField_ColMenuNameForm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColMenuNameForm.TextSize = New System.Drawing.Size(0, 0)
        Me.layoutViewField_ColMenuNameForm.TextVisible = False
        '
        'ColMenuNameModule
        '
        Me.ColMenuNameModule.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColMenuNameModule.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColMenuNameModule.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColMenuNameModule.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColMenuNameModule.AppearanceCell.Options.UseBackColor = True
        Me.ColMenuNameModule.AppearanceCell.Options.UseFont = True
        Me.ColMenuNameModule.AppearanceCell.Options.UseForeColor = True
        Me.ColMenuNameModule.AppearanceCell.Options.UseTextOptions = True
        Me.ColMenuNameModule.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColMenuNameModule.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColMenuNameModule.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColMenuNameModule.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColMenuNameModule.AppearanceHeader.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColMenuNameModule.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColMenuNameModule.AppearanceHeader.ForeColor = System.Drawing.Color.Black
        Me.ColMenuNameModule.AppearanceHeader.Options.UseBackColor = True
        Me.ColMenuNameModule.AppearanceHeader.Options.UseFont = True
        Me.ColMenuNameModule.AppearanceHeader.Options.UseForeColor = True
        Me.ColMenuNameModule.AppearanceHeader.Options.UseTextOptions = True
        Me.ColMenuNameModule.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColMenuNameModule.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColMenuNameModule.Caption = "Módulo"
        Me.ColMenuNameModule.FieldName = "NameModule"
        Me.ColMenuNameModule.LayoutViewField = Me.layoutViewField_ColMenuNameModule
        Me.ColMenuNameModule.Name = "ColMenuNameModule"
        Me.ColMenuNameModule.OptionsColumn.AllowEdit = False
        Me.ColMenuNameModule.OptionsColumn.AllowFocus = False
        Me.ColMenuNameModule.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuNameModule.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuNameModule.OptionsColumn.AllowMove = False
        Me.ColMenuNameModule.OptionsColumn.AllowShowHide = False
        Me.ColMenuNameModule.OptionsColumn.AllowSize = False
        Me.ColMenuNameModule.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuNameModule.OptionsColumn.ReadOnly = True
        Me.ColMenuNameModule.OptionsColumn.TabStop = False
        Me.ColMenuNameModule.OptionsFilter.AllowAutoFilter = False
        Me.ColMenuNameModule.OptionsFilter.AllowFilter = False
        Me.ColMenuNameModule.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColMenuNameModule
        '
        Me.layoutViewField_ColMenuNameModule.EditorPreferredWidth = 270
        Me.layoutViewField_ColMenuNameModule.Location = New System.Drawing.Point(63, 36)
        Me.layoutViewField_ColMenuNameModule.MaxSize = New System.Drawing.Size(0, 26)
        Me.layoutViewField_ColMenuNameModule.MinSize = New System.Drawing.Size(77, 26)
        Me.layoutViewField_ColMenuNameModule.Name = "layoutViewField_ColMenuNameModule"
        Me.layoutViewField_ColMenuNameModule.Padding = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColMenuNameModule.Size = New System.Drawing.Size(367, 26)
        Me.layoutViewField_ColMenuNameModule.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColMenuNameModule.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColMenuNameModule.TextSize = New System.Drawing.Size(89, 20)
        Me.layoutViewField_ColMenuNameModule.TextToControlDistance = 5
        '
        'ColMenuNameGroup
        '
        Me.ColMenuNameGroup.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColMenuNameGroup.AppearanceCell.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColMenuNameGroup.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColMenuNameGroup.AppearanceCell.ForeColor = System.Drawing.Color.Gray
        Me.ColMenuNameGroup.AppearanceCell.Options.UseBackColor = True
        Me.ColMenuNameGroup.AppearanceCell.Options.UseFont = True
        Me.ColMenuNameGroup.AppearanceCell.Options.UseForeColor = True
        Me.ColMenuNameGroup.AppearanceCell.Options.UseTextOptions = True
        Me.ColMenuNameGroup.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColMenuNameGroup.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColMenuNameGroup.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.ColMenuNameGroup.AppearanceHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColMenuNameGroup.AppearanceHeader.BackColor2 = System.Drawing.Color.WhiteSmoke
        Me.ColMenuNameGroup.AppearanceHeader.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.ColMenuNameGroup.AppearanceHeader.ForeColor = System.Drawing.Color.Black
        Me.ColMenuNameGroup.AppearanceHeader.Options.UseBackColor = True
        Me.ColMenuNameGroup.AppearanceHeader.Options.UseFont = True
        Me.ColMenuNameGroup.AppearanceHeader.Options.UseForeColor = True
        Me.ColMenuNameGroup.AppearanceHeader.Options.UseTextOptions = True
        Me.ColMenuNameGroup.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.ColMenuNameGroup.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ColMenuNameGroup.Caption = "Grupo"
        Me.ColMenuNameGroup.FieldName = "NameGroup"
        Me.ColMenuNameGroup.LayoutViewField = Me.layoutViewField_ColMenuNameGroup
        Me.ColMenuNameGroup.Name = "ColMenuNameGroup"
        Me.ColMenuNameGroup.OptionsColumn.AllowEdit = False
        Me.ColMenuNameGroup.OptionsColumn.AllowFocus = False
        Me.ColMenuNameGroup.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuNameGroup.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuNameGroup.OptionsColumn.AllowMove = False
        Me.ColMenuNameGroup.OptionsColumn.AllowShowHide = False
        Me.ColMenuNameGroup.OptionsColumn.AllowSize = False
        Me.ColMenuNameGroup.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColMenuNameGroup.OptionsColumn.ReadOnly = True
        Me.ColMenuNameGroup.OptionsColumn.TabStop = False
        Me.ColMenuNameGroup.OptionsFilter.AllowAutoFilter = False
        Me.ColMenuNameGroup.OptionsFilter.AllowFilter = False
        Me.ColMenuNameGroup.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.[False]
        '
        'layoutViewField_ColMenuNameGroup
        '
        Me.layoutViewField_ColMenuNameGroup.EditorPreferredWidth = 270
        Me.layoutViewField_ColMenuNameGroup.Location = New System.Drawing.Point(63, 62)
        Me.layoutViewField_ColMenuNameGroup.MaxSize = New System.Drawing.Size(0, 26)
        Me.layoutViewField_ColMenuNameGroup.MinSize = New System.Drawing.Size(77, 26)
        Me.layoutViewField_ColMenuNameGroup.Name = "layoutViewField_ColMenuNameGroup"
        Me.layoutViewField_ColMenuNameGroup.Padding = New DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 0)
        Me.layoutViewField_ColMenuNameGroup.Size = New System.Drawing.Size(367, 89)
        Me.layoutViewField_ColMenuNameGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.layoutViewField_ColMenuNameGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.layoutViewField_ColMenuNameGroup.TextSize = New System.Drawing.Size(89, 20)
        Me.layoutViewField_ColMenuNameGroup.TextToControlDistance = 5
        '
        'LayoutViewCard3
        '
        Me.LayoutViewCard3.CustomizationFormText = "TemplateCard"
        Me.LayoutViewCard3.GroupBordersVisible = False
        Me.LayoutViewCard3.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.LayoutViewCard3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutViewField_ColMenuNameForm, Me.layoutViewField_ColMenuNameModule, Me.layoutViewField_ColMenuNameGroup, Me.layoutViewField_ColMenuTile})
        Me.LayoutViewCard3.Name = "LayoutViewCard3"
        Me.LayoutViewCard3.OptionsItemText.TextToControlDistance = 5
        Me.LayoutViewCard3.Text = "TemplateCard"
        '
        'INDlycgRoot
        '
        Me.INDlycgRoot.AllowHide = False
        Me.INDlycgRoot.CustomizationFormText = "INDlycgRoot"
        Me.INDlycgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycgRoot.GroupBordersVisible = False
        Me.INDlycgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyciSearch, Me.INDlycgResults})
        Me.INDlycgRoot.Name = "INDlycgRoot"
        Me.INDlycgRoot.ShowInCustomizationForm = False
        Me.INDlycgRoot.Size = New System.Drawing.Size(1003, 683)
        Me.INDlycgRoot.TextVisible = False
        '
        'INDlyciSearch
        '
        Me.INDlyciSearch.Control = Me.INDpnlSearch
        Me.INDlyciSearch.CustomizationFormText = "LayoutControlItem1"
        Me.INDlyciSearch.Location = New System.Drawing.Point(0, 0)
        Me.INDlyciSearch.MaxSize = New System.Drawing.Size(0, 130)
        Me.INDlyciSearch.MinSize = New System.Drawing.Size(1000, 130)
        Me.INDlyciSearch.Name = "INDlyciSearch"
        Me.INDlyciSearch.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 60, 0)
        Me.INDlyciSearch.Size = New System.Drawing.Size(1003, 130)
        Me.INDlyciSearch.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciSearch.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyciSearch.TextVisible = False
        '
        'INDlycgResults
        '
        Me.INDlycgResults.CustomizationFormText = "INDlycgResults"
        Me.INDlycgResults.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyciMenuResults, Me.INDlyciDocsResults, Me.INDlyciRegsResults, Me.INDlyciTitle})
        Me.INDlycgResults.Location = New System.Drawing.Point(0, 130)
        Me.INDlycgResults.Name = "INDlycgResults"
        Me.INDlycgResults.Size = New System.Drawing.Size(1003, 553)
        Me.INDlycgResults.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlycgResults.TextVisible = False
        '
        'INDlyciMenuResults
        '
        Me.INDlyciMenuResults.Control = Me.INDpnlMenuResults
        Me.INDlyciMenuResults.CustomizationFormText = "INDlyciMenuResults"
        Me.INDlyciMenuResults.Location = New System.Drawing.Point(0, 60)
        Me.INDlyciMenuResults.MinSize = New System.Drawing.Size(333, 450)
        Me.INDlyciMenuResults.Name = "INDlyciMenuResults"
        Me.INDlyciMenuResults.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlyciMenuResults.Size = New System.Drawing.Size(333, 488)
        Me.INDlyciMenuResults.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciMenuResults.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyciMenuResults.TextVisible = False
        '
        'INDlyciDocsResults
        '
        Me.INDlyciDocsResults.Control = Me.INDpnlDocsResults
        Me.INDlyciDocsResults.CustomizationFormText = "INDlyciDocsResults"
        Me.INDlyciDocsResults.Location = New System.Drawing.Point(666, 60)
        Me.INDlyciDocsResults.MinSize = New System.Drawing.Size(333, 450)
        Me.INDlyciDocsResults.Name = "INDlyciDocsResults"
        Me.INDlyciDocsResults.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlyciDocsResults.Size = New System.Drawing.Size(333, 488)
        Me.INDlyciDocsResults.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciDocsResults.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyciDocsResults.TextVisible = False
        '
        'INDlyciRegsResults
        '
        Me.INDlyciRegsResults.Control = Me.INDpnlRegsResults
        Me.INDlyciRegsResults.CustomizationFormText = "INDlyciRegsResults"
        Me.INDlyciRegsResults.Location = New System.Drawing.Point(333, 60)
        Me.INDlyciRegsResults.MinSize = New System.Drawing.Size(333, 450)
        Me.INDlyciRegsResults.Name = "INDlyciRegsResults"
        Me.INDlyciRegsResults.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlyciRegsResults.Size = New System.Drawing.Size(333, 488)
        Me.INDlyciRegsResults.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciRegsResults.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyciRegsResults.TextVisible = False
        '
        'INDlyciTitle
        '
        Me.INDlyciTitle.Control = Me.INDpnlTitle
        Me.INDlyciTitle.CustomizationFormText = "INDlyciTitle"
        Me.INDlyciTitle.Location = New System.Drawing.Point(0, 0)
        Me.INDlyciTitle.MaxSize = New System.Drawing.Size(0, 60)
        Me.INDlyciTitle.MinSize = New System.Drawing.Size(990, 60)
        Me.INDlyciTitle.Name = "INDlyciTitle"
        Me.INDlyciTitle.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 0)
        Me.INDlyciTitle.Size = New System.Drawing.Size(999, 60)
        Me.INDlyciTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyciTitle.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyciTitle.TextVisible = False
        '
        'INDimcButtonSearch
        '
        Me.INDimcButtonSearch.ImageSize = New System.Drawing.Size(32, 32)
        Me.INDimcButtonSearch.ImageStream = CType(resources.GetObject("INDimcButtonSearch.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDimcButtonSearch.Images.SetKeyName(0, "Search32.png")
        Me.INDimcButtonSearch.Images.SetKeyName(1, "SearchGray32.png")
        '
        'INDimcMenuFilter
        '
        Me.INDimcMenuFilter.ImageStream = CType(resources.GetObject("INDimcMenuFilter.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDimcMenuFilter.TransparentColor = System.Drawing.Color.White
        Me.INDimcMenuFilter.Images.SetKeyName(0, "Blank16.png")
        Me.INDimcMenuFilter.Images.SetKeyName(1, "chek.png")
        '
        'INDtmrRelaySearch
        '
        Me.INDtmrRelaySearch.Interval = 1000
        '
        'CtrSearching
        '
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlycRoot)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MinimumSize = New System.Drawing.Size(1001, 700)
        Me.Name = "CtrSearching"
        Me.Size = New System.Drawing.Size(1001, 700)
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycRoot.ResumeLayout(False)
        CType(Me.INDpccFilterMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccFilterMenu.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpnlTitle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlTitle.ResumeLayout(False)
        CType(Me.INDpnlSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlSearch.ResumeLayout(False)
        CType(Me.INDpnlSeparate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpnlSeparate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpnlSubSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlSubSearch.ResumeLayout(False)
        CType(Me.INDtxtSearch.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpnlDocsResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlDocsResults.ResumeLayout(False)
        CType(Me.INDpnlDocsTitle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlDocsTitle.ResumeLayout(False)
        CType(Me.INDlnDocsSelector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgdcDocs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgdvDocs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepimlTileDocs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDimcTiles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColDocsTile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColDocsTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColDocsNameForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColDocsAuthor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColDocsUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepmemDocsContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColDocsContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutViewCard1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpnlRegsResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlRegsResults.ResumeLayout(False)
        CType(Me.INDpnlRegsTitle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlRegsTitle.ResumeLayout(False)
        CType(Me.INDlnRegsSelector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgdcRegs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgdvRegs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepimlTileRegs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColRegsTile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColRegsTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColRegsNameForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColRegsAuthor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColRegsUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepmemRegsContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColRegsContent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutViewCard2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpnlMenuResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlMenuResults.ResumeLayout(False)
        CType(Me.INDpnlMenuTitle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpnlMenuTitle.ResumeLayout(False)
        CType(Me.INDlnMenuSelector, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgdcMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgdvMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepimlTileMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColMenuTile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColMenuNameForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColMenuNameModule, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutViewField_ColMenuNameGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutViewCard3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycgResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciMenuResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciDocsResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciRegsResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyciTitle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDimcButtonSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDimcMenuFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgdcMenu As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDpnlSearch As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyciSearch As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblSearchTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDgdvMenu As DevExpress.XtraGrid.Views.Layout.LayoutView
    Friend WithEvents ColMenuNameForm As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColMenuNameModule As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColMenuNameGroup As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents INDpnlTitle As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyciTitle As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDbtnBack As DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel
    Friend WithEvents INDtxtSearch As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDpnlSubSearch As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpnlSeparate2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDpnlSeparate1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnSearch As System.Windows.Forms.Button
    Friend WithEvents INDimcButtonSearch As DevExpress.Utils.ImageCollection
    Friend WithEvents INDlblFilters As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpccFilterMenu As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnYear As System.Windows.Forms.Button
    Friend WithEvents INDbtnLastHour As System.Windows.Forms.Button
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnToday As System.Windows.Forms.Button
    Friend WithEvents INDbtnWeek As System.Windows.Forms.Button
    Friend WithEvents INDbtnMonth As System.Windows.Forms.Button
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDimcMenuFilter As DevExpress.Utils.ImageCollection
    Friend WithEvents INDbtnNoFilter As System.Windows.Forms.Button
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnContentDocuments As System.Windows.Forms.Button
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtmrRelaySearch As System.Windows.Forms.Timer
    Friend WithEvents INDpnlMenuResults As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblMenuTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpnlMenuTitle As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblMenuTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlycgResults As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyciMenuResults As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpnlRegsResults As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblRegsTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpnlRegsTitle As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblRegsTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDgdcRegs As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgdvRegs As DevExpress.XtraGrid.Views.Layout.LayoutView
    Friend WithEvents INDpnlDocsResults As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblDocsTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDpnlDocsTitle As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblDocsTitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDgdcDocs As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgdvDocs As DevExpress.XtraGrid.Views.Layout.LayoutView
    Friend WithEvents INDlyciDocsResults As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyciRegsResults As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDimcTiles As DevExpress.Utils.ImageCollection
    Friend WithEvents RepimlTileMenu As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ColMenuTile As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents RepimlTileRegs As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ColDocsTile As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents RepimlTileDocs As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ColDocsTitle As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColDocsNameForm As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColDocsAuthor As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColDocsUpdate As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColDocsContent As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColRegsTile As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColRegsTitle As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColRegsNameForm As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColRegsAuthor As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColRegsUpdate As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents ColRegsContent As DevExpress.XtraGrid.Columns.LayoutViewColumn
    Friend WithEvents RepmemDocsContent As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents RepmemRegsContent As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents INDlnMenuSelector As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlnDocsSelector As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlnRegsSelector As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlblSearch As DevExpress.XtraEditors.LabelControl
    Friend WithEvents layoutViewField_ColDocsTile As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColDocsTitle As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColDocsNameForm As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColDocsAuthor As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColDocsUpdate As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColDocsContent As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents LayoutViewCard1 As DevExpress.XtraGrid.Views.Layout.LayoutViewCard
    Friend WithEvents layoutViewField_ColRegsTile As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColRegsTitle As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColRegsNameForm As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColRegsAuthor As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColRegsUpdate As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColRegsContent As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents LayoutViewCard2 As DevExpress.XtraGrid.Views.Layout.LayoutViewCard
    Friend WithEvents layoutViewField_ColMenuTile As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColMenuNameForm As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColMenuNameModule As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents layoutViewField_ColMenuNameGroup As DevExpress.XtraGrid.Views.Layout.LayoutViewField
    Friend WithEvents LayoutViewCard3 As DevExpress.XtraGrid.Views.Layout.LayoutViewCard
    Friend WithEvents IndigoCheckEdit1 As IndigoCheckEdit
End Class
