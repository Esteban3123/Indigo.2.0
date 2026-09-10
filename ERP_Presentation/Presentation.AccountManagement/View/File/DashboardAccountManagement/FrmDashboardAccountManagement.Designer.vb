Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardAccountManagement
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.RepositoryItemImageComboBox4 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLcMain = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControl3 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleFilter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiPrint = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.BarEditItem11 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemRadioGroup11 = New DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup()
        Me.BarEditItem2 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BarEditItem3 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BarToggleSwitchItem11 = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.BarDockingMenuItem11 = New DevExpress.XtraBars.BarDockingMenuItem()
        Me.BarCheckItem11 = New DevExpress.XtraBars.BarCheckItem()
        Me.BarButtonItem11 = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiTransferFolio = New DevExpress.XtraBars.BarButtonItem()
        Me.INDGvFilter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCColumn01 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCColumn02 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleFilterType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvFilterType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GcFilter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbCleanFilter = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciFilterType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFilter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDGcTraceability = New DevExpress.XtraGrid.GridControl()
        Me.INDGvTraceability = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GcAdmissionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFolios = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcCareGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcPreviousManagementArea = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcPreviousUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcEventDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcAssignedUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFolioStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcAdmissionDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcPatientCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcBed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcDiagnosis = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFolioTotalValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFolioType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcCreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcModificationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcTransferStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcComments = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcInvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDPccAddAlert = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbAddNewAlert = New DevExpress.XtraEditors.SimpleButton()
        Me.INDTeNewAlert_Comment = New DevExpress.XtraEditors.TextEdit()
        Me.INDLcgNewAlert = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNewAlert_Comments = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcTransfers = New DevExpress.XtraGrid.GridControl()
        Me.INDGvTransfers = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCTransferCheck = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferAdmission = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferPatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferAlert = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiceFolioAlert = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GCTransferAdmissionDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferPatientCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferCareGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferBed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferDiagnosis = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferFolioQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferFolioValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferFolioType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferFolioStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferAssignedUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferInvoiceUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferManagementArea = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferCreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferModificationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferInvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferTransferStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDTcgAccountManagement = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgTraceability = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgTransfers = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTransfers = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LabelInformation = New DevExpress.XtraLayout.SimpleLabelItem()
        Me.PanelControl11 = New DevExpress.XtraEditors.PanelControl()
        Me.INDSleAttentionCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvSearchCareGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCAttentionCenterCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCAttentionCenterName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcMainData = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTbAutomaticReload = New DevExpress.XtraEditors.ToggleSwitch()
        Me.LayoutControlGroup21 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDTcgDashBoard = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup11 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDDdbMenuActions = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPmActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgContainer = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SkinBarSubItem11 = New DevExpress.XtraBars.SkinBarSubItem()
        Me.BarSubItem11 = New DevExpress.XtraBars.BarSubItem()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        Me.RepositoryItemImageComboBox3 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.RepositoryItemImageComboBox2 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.INDGcRequest = New DevExpress.XtraGrid.GridControl()
        Me.INDGvRequest = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox11 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn272 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciToggleButton = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPopupContainerEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGridViewCurrentAlerts = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit31 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPmRowActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDLcMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMain.SuspendLayout()
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl3.SuspendLayout()
        CType(Me.INDSleFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemRadioGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleFilterType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFilterType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFilterType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcTraceability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvTraceability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccAddAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccAddAlert.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDTeNewAlert_Comment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgNewAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNewAlert_Comments, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiceFolioAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgAccountManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgTraceability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabelInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl11.SuspendLayout()
        CType(Me.INDSleAttentionCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSearchCareGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMainData.SuspendLayout()
        CType(Me.INDTbAutomaticReload.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgDashBoard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPmActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciToggleButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridViewCurrentAlerts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPmRowActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1503, 711)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.ToolBars.Size = New System.Drawing.Size(1503, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1503, 130)
        '
        'RepositoryItemImageComboBox4
        '
        Me.RepositoryItemImageComboBox4.AutoHeight = False
        Me.RepositoryItemImageComboBox4.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tratamiento Nuevo", Global.Microsoft.VisualBasic.ChrW(49), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tratamiento Antiguo", Global.Microsoft.VisualBasic.ChrW(50), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Codigo Azul", Global.Microsoft.VisualBasic.ChrW(51), -1)})
        Me.RepositoryItemImageComboBox4.Name = "RepositoryItemImageComboBox4"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDLcMain)
        Me.LayoutControl1.Controls.Add(Me.PanelControl11)
        Me.LayoutControl1.Controls.Add(Me.INDDdbMenuActions)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 9)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(1499, 700)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLcMain
        '
        Me.INDLcMain.Controls.Add(Me.LayoutControl3)
        Me.INDLcMain.Controls.Add(Me.INDGcTraceability)
        Me.INDLcMain.Controls.Add(Me.INDPccAddAlert)
        Me.INDLcMain.Controls.Add(Me.INDGcTransfers)
        Me.INDLcMain.Location = New System.Drawing.Point(22, 102)
        Me.INDLcMain.Margin = New System.Windows.Forms.Padding(0)
        Me.INDLcMain.Name = "INDLcMain"
        Me.INDLcMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1030, 417, 650, 400)
        Me.INDLcMain.Root = Me.INDLcgMain
        Me.INDLcMain.Size = New System.Drawing.Size(1455, 576)
        Me.INDLcMain.TabIndex = 16
        Me.INDLcMain.Text = "LayoutControl2"
        '
        'LayoutControl3
        '
        Me.LayoutControl3.Controls.Add(Me.INDSleFilter)
        Me.LayoutControl3.Controls.Add(Me.INDSleFilterType)
        Me.LayoutControl3.Controls.Add(Me.INDSbCleanFilter)
        Me.LayoutControl3.Location = New System.Drawing.Point(14, 81)
        Me.LayoutControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl3.Name = "LayoutControl3"
        Me.LayoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(975, 244, 574, 569)
        Me.LayoutControl3.Root = Me.LayoutControlGroup2
        Me.LayoutControl3.Size = New System.Drawing.Size(1427, 43)
        Me.LayoutControl3.TabIndex = 30
        Me.LayoutControl3.Text = "LayoutControl3"
        '
        'INDSleFilter
        '
        Me.INDSleFilter.Location = New System.Drawing.Point(392, 6)
        Me.INDSleFilter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleFilter.MaximumSize = New System.Drawing.Size(292, 31)
        Me.INDSleFilter.MenuManager = Me.BarManager
        Me.INDSleFilter.MinimumSize = New System.Drawing.Size(292, 31)
        Me.INDSleFilter.Name = "INDSleFilter"
        Me.INDSleFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleFilter.Properties.NullText = ""
        Me.INDSleFilter.Properties.PopupView = Me.INDGvFilter
        Me.INDSleFilter.Size = New System.Drawing.Size(292, 31)
        Me.INDSleFilter.StyleController = Me.LayoutControl3
        Me.INDSleFilter.TabIndex = 5
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiPrint, Me.INDBbiRefresh, Me.BarEditItem11, Me.BarEditItem2, Me.BarEditItem3, Me.BarToggleSwitchItem11, Me.BarDockingMenuItem11, Me.BarCheckItem11, Me.BarButtonItem11, Me.INDBbiTransferFolio})
        Me.BarManager.MaxItemId = 13
        Me.BarManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemRadioGroup11, Me.RepositoryItemTextEdit11, Me.RepositoryItemTextEdit2})
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 7)
        Me.barDockControlTop.Manager = Me.BarManager
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlTop.Size = New System.Drawing.Size(1503, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 848)
        Me.barDockControlBottom.Manager = Me.BarManager
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1503, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 7)
        Me.barDockControlLeft.Manager = Me.BarManager
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 841)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1503, 7)
        Me.barDockControlRight.Manager = Me.BarManager
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 841)
        '
        'INDBbiPrint
        '
        Me.INDBbiPrint.Caption = "Imprimir"
        Me.INDBbiPrint.Id = 1
        Me.INDBbiPrint.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
        Me.INDBbiPrint.Name = "INDBbiPrint"
        Me.INDBbiPrint.ShortcutKeyDisplayString = "Ctrl+P"
        '
        'INDBbiRefresh
        '
        Me.INDBbiRefresh.Caption = "Refrescar"
        Me.INDBbiRefresh.Id = 2
        Me.INDBbiRefresh.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiRefresh.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiRefresh.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiRefresh.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiRefresh.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiRefresh.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiRefresh.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiRefresh.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiRefresh.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
        Me.INDBbiRefresh.Name = "INDBbiRefresh"
        Me.INDBbiRefresh.ShortcutKeyDisplayString = "Ctrl+R"
        '
        'BarEditItem11
        '
        Me.BarEditItem11.Caption = "Borrar"
        Me.BarEditItem11.Edit = Me.RepositoryItemRadioGroup11
        Me.BarEditItem11.Id = 3
        Me.BarEditItem11.Name = "BarEditItem11"
        '
        'RepositoryItemRadioGroup11
        '
        Me.RepositoryItemRadioGroup11.Name = "RepositoryItemRadioGroup11"
        '
        'BarEditItem2
        '
        Me.BarEditItem2.Edit = Me.RepositoryItemTextEdit11
        Me.BarEditItem2.Id = 4
        Me.BarEditItem2.Name = "BarEditItem2"
        '
        'RepositoryItemTextEdit11
        '
        Me.RepositoryItemTextEdit11.AutoHeight = False
        Me.RepositoryItemTextEdit11.Name = "RepositoryItemTextEdit11"
        '
        'BarEditItem3
        '
        Me.BarEditItem3.Edit = Me.RepositoryItemTextEdit2
        Me.BarEditItem3.Id = 5
        Me.BarEditItem3.Name = "BarEditItem3"
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        '
        'BarToggleSwitchItem11
        '
        Me.BarToggleSwitchItem11.Caption = "BarToggleSwitchItem1"
        Me.BarToggleSwitchItem11.Id = 6
        Me.BarToggleSwitchItem11.Name = "BarToggleSwitchItem11"
        '
        'BarDockingMenuItem11
        '
        Me.BarDockingMenuItem11.Caption = "BarDockingMenuItem1"
        Me.BarDockingMenuItem11.Id = 7
        Me.BarDockingMenuItem11.Name = "BarDockingMenuItem11"
        '
        'BarCheckItem11
        '
        Me.BarCheckItem11.Caption = "BarCheckItem1"
        Me.BarCheckItem11.Id = 8
        Me.BarCheckItem11.Name = "BarCheckItem11"
        '
        'BarButtonItem11
        '
        Me.BarButtonItem11.Caption = "Imprimir"
        Me.BarButtonItem11.Id = 10
        Me.BarButtonItem11.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BarButtonItem11.ItemAppearance.Disabled.Options.UseFont = True
        Me.BarButtonItem11.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BarButtonItem11.ItemAppearance.Hovered.Options.UseFont = True
        Me.BarButtonItem11.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BarButtonItem11.ItemAppearance.Normal.Options.UseFont = True
        Me.BarButtonItem11.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.BarButtonItem11.ItemAppearance.Pressed.Options.UseFont = True
        Me.BarButtonItem11.Name = "BarButtonItem11"
        '
        'INDBbiTransferFolio
        '
        Me.INDBbiTransferFolio.Caption = "Trasladar"
        Me.INDBbiTransferFolio.Id = 12
        Me.INDBbiTransferFolio.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiTransferFolio.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiTransferFolio.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiTransferFolio.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiTransferFolio.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBbiTransferFolio.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiTransferFolio.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiTransferFolio.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiTransferFolio.Name = "INDBbiTransferFolio"
        '
        'INDGvFilter
        '
        Me.INDGvFilter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFilter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFilter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFilter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFilter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFilter.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFilter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFilter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFilter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFilter.Appearance.Row.Options.UseFont = True
        Me.INDGvFilter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCColumn01, Me.GCColumn02})
        Me.INDGvFilter.DetailHeight = 431
        Me.INDGvFilter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvFilter.Name = "INDGvFilter"
        Me.INDGvFilter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvFilter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFilter.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFilter.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFilter.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewCurrentAlerts.SetTemaIndigoMetro(Me.INDGvFilter, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDGvFilter, False)
        '
        'GCColumn01
        '
        Me.GCColumn01.Caption = "GridColumn6"
        Me.GCColumn01.MinWidth = 23
        Me.GCColumn01.Name = "GCColumn01"
        Me.GCColumn01.Visible = True
        Me.GCColumn01.VisibleIndex = 0
        Me.GCColumn01.Width = 87
        '
        'GCColumn02
        '
        Me.GCColumn02.Caption = "GridColumn7"
        Me.GCColumn02.MinWidth = 23
        Me.GCColumn02.Name = "GCColumn02"
        Me.GCColumn02.Visible = True
        Me.GCColumn02.VisibleIndex = 1
        Me.GCColumn02.Width = 87
        '
        'INDSleFilterType
        '
        Me.INDSleFilterType.Location = New System.Drawing.Point(119, 6)
        Me.INDSleFilterType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleFilterType.MaximumSize = New System.Drawing.Size(157, 31)
        Me.INDSleFilterType.MenuManager = Me.BarManager
        Me.INDSleFilterType.MinimumSize = New System.Drawing.Size(157, 31)
        Me.INDSleFilterType.Name = "INDSleFilterType"
        Me.INDSleFilterType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleFilterType.Properties.DisplayMember = "Item2"
        Me.INDSleFilterType.Properties.NullText = ""
        Me.INDSleFilterType.Properties.PopupView = Me.INDGvFilterType
        Me.INDSleFilterType.Properties.ValueMember = "Item1"
        Me.INDSleFilterType.Size = New System.Drawing.Size(157, 31)
        Me.INDSleFilterType.StyleController = Me.LayoutControl3
        Me.INDSleFilterType.TabIndex = 4
        '
        'INDGvFilterType
        '
        Me.INDGvFilterType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFilterType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFilterType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFilterType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFilterType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFilterType.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFilterType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFilterType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFilterType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFilterType.Appearance.Row.Options.UseFont = True
        Me.INDGvFilterType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GcFilter})
        Me.INDGvFilterType.DetailHeight = 431
        Me.INDGvFilterType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvFilterType.Name = "INDGvFilterType"
        Me.INDGvFilterType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvFilterType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFilterType.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFilterType.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFilterType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewCurrentAlerts.SetTemaIndigoMetro(Me.INDGvFilterType, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDGvFilterType, False)
        '
        'GcFilter
        '
        Me.GcFilter.Caption = "Tipo Consulta"
        Me.GcFilter.FieldName = "Item2"
        Me.GcFilter.MinWidth = 23
        Me.GcFilter.Name = "GcFilter"
        Me.GcFilter.Visible = True
        Me.GcFilter.VisibleIndex = 0
        Me.GcFilter.Width = 87
        '
        'INDSbCleanFilter
        '
        Me.INDSbCleanFilter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSbCleanFilter.Appearance.Options.UseFont = True
        Me.INDSbCleanFilter.Location = New System.Drawing.Point(767, 5)
        Me.INDSbCleanFilter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbCleanFilter, True)
        Me.INDSbCleanFilter.Name = "INDSbCleanFilter"
        Me.INDSbCleanFilter.Size = New System.Drawing.Size(193, 33)
        Me.INDSbCleanFilter.StyleController = Me.LayoutControl3
        Me.INDSbCleanFilter.TabIndex = 31
        Me.INDSbCleanFilter.Text = "Limpiar"
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
        Me.LayoutControlGroup2.DefaultLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciFilterType, Me.INDLciFilter, Me.LayoutControlItem16, Me.EmptySpaceItem2, Me.EmptySpaceItem1, Me.EmptySpaceItem3})
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1427, 43)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLciFilterType
        '
        Me.INDLciFilterType.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDLciFilterType.Control = Me.INDSleFilterType
        Me.INDLciFilterType.Location = New System.Drawing.Point(0, 0)
        Me.INDLciFilterType.MaxSize = New System.Drawing.Size(300, 41)
        Me.INDLciFilterType.MinSize = New System.Drawing.Size(300, 41)
        Me.INDLciFilterType.Name = "INDLciFilterType"
        Me.INDLciFilterType.Size = New System.Drawing.Size(300, 43)
        Me.INDLciFilterType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFilterType.Text = "Tipo Consulta"
        Me.INDLciFilterType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDLciFilterType.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDLciFilterType.TextSize = New System.Drawing.Size(112, 23)
        Me.INDLciFilterType.TextToControlDistance = 5
        '
        'INDLciFilter
        '
        Me.INDLciFilter.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDLciFilter.Control = Me.INDSleFilter
        Me.INDLciFilter.CustomizationFormText = "Tipo Consulta"
        Me.INDLciFilter.Location = New System.Drawing.Point(323, 0)
        Me.INDLciFilter.MaxSize = New System.Drawing.Size(357, 41)
        Me.INDLciFilter.MinSize = New System.Drawing.Size(357, 41)
        Me.INDLciFilter.Name = "INDLciFilter"
        Me.INDLciFilter.Size = New System.Drawing.Size(357, 43)
        Me.INDLciFilter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFilter.Text = "Ingreso"
        Me.INDLciFilter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDLciFilter.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDLciFilter.TextSize = New System.Drawing.Size(62, 23)
        Me.INDLciFilter.TextToControlDistance = 5
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem16.Control = Me.INDSbCleanFilter
        Me.LayoutControlItem16.Location = New System.Drawing.Point(765, 0)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(197, 43)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.SupportHorzAlignment
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem16.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(300, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(23, 43)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(680, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(85, 43)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(962, 0)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(465, 43)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDGcTraceability
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcTraceability, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcTraceability, Nothing)
        Me.INDGcTraceability.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcTraceability, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcTraceability, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcTraceability, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcTraceability, False)
        Me.INDGcTraceability.Location = New System.Drawing.Point(14, 128)
        Me.INDGcTraceability.MainView = Me.INDGvTraceability
        Me.INDGcTraceability.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGcTraceability.Name = "INDGcTraceability"
        Me.INDGcTraceability.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.INDGcTraceability.Size = New System.Drawing.Size(1427, 434)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcTraceability, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcTraceability.TabIndex = 27
        Me.INDGcTraceability.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvTraceability})
        '
        'INDGvTraceability
        '
        Me.INDGvTraceability.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvTraceability.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvTraceability.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvTraceability.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvTraceability.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvTraceability.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvTraceability.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvTraceability.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvTraceability.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvTraceability.Appearance.Row.Options.UseFont = True
        Me.INDGvTraceability.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvTraceability.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvTraceability.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GcAdmissionNumber, Me.GcFolios, Me.GcCareGroup, Me.GcPreviousManagementArea, Me.GcPreviousUser, Me.GcEventDate, Me.GcAssignedUser, Me.GcFolioStatus, Me.GcPatientName, Me.GcAdmissionDate, Me.GcPatientCode, Me.GcFunctionalUnit, Me.GcBed, Me.GcDiagnosis, Me.GcFolioTotalValue, Me.GcFolioType, Me.GcCreationUser, Me.GcModificationUser, Me.GcTransferStatus, Me.GcComments, Me.GcInvoiceNumber})
        Me.INDGvTraceability.CustomizationFormBounds = New System.Drawing.Rectangle(903, 139, 294, 377)
        Me.INDGvTraceability.DetailHeight = 431
        Me.INDGvTraceability.GridControl = Me.INDGcTraceability
        Me.INDGvTraceability.Name = "INDGvTraceability"
        Me.INDGvTraceability.OptionsSelection.MultiSelect = True
        Me.INDGvTraceability.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvTraceability.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvTraceability.OptionsView.ShowAutoFilterRow = True
        Me.INDGvTraceability.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewCurrentAlerts.SetTemaIndigoMetro(Me.INDGvTraceability, True)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDGvTraceability, False)
        '
        'GcAdmissionNumber
        '
        Me.GcAdmissionNumber.Caption = "Ingreso"
        Me.GcAdmissionNumber.FieldName = "AdmissionNumber"
        Me.GcAdmissionNumber.MinWidth = 23
        Me.GcAdmissionNumber.Name = "GcAdmissionNumber"
        Me.GcAdmissionNumber.OptionsColumn.AllowEdit = False
        Me.GcAdmissionNumber.Visible = True
        Me.GcAdmissionNumber.VisibleIndex = 0
        Me.GcAdmissionNumber.Width = 94
        '
        'GcFolios
        '
        Me.GcFolios.AppearanceCell.Options.UseTextOptions = True
        Me.GcFolios.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcFolios.Caption = "Folio"
        Me.GcFolios.FieldName = "FolioNumber"
        Me.GcFolios.MinWidth = 23
        Me.GcFolios.Name = "GcFolios"
        Me.GcFolios.OptionsColumn.AllowEdit = False
        Me.GcFolios.Visible = True
        Me.GcFolios.VisibleIndex = 5
        Me.GcFolios.Width = 82
        '
        'GcCareGroup
        '
        Me.GcCareGroup.AppearanceCell.Options.UseTextOptions = True
        Me.GcCareGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcCareGroup.Caption = "Grupo de Atención"
        Me.GcCareGroup.FieldName = "CareGroupName"
        Me.GcCareGroup.MinWidth = 23
        Me.GcCareGroup.Name = "GcCareGroup"
        Me.GcCareGroup.OptionsColumn.AllowEdit = False
        Me.GcCareGroup.Visible = True
        Me.GcCareGroup.VisibleIndex = 1
        Me.GcCareGroup.Width = 94
        '
        'GcPreviousManagementArea
        '
        Me.GcPreviousManagementArea.AppearanceCell.Options.UseTextOptions = True
        Me.GcPreviousManagementArea.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcPreviousManagementArea.Caption = "Área de Gestión Origen"
        Me.GcPreviousManagementArea.FieldName = "PreviousManagementAreaName"
        Me.GcPreviousManagementArea.MinWidth = 23
        Me.GcPreviousManagementArea.Name = "GcPreviousManagementArea"
        Me.GcPreviousManagementArea.OptionsColumn.AllowEdit = False
        Me.GcPreviousManagementArea.Visible = True
        Me.GcPreviousManagementArea.VisibleIndex = 2
        Me.GcPreviousManagementArea.Width = 134
        '
        'GcPreviousUser
        '
        Me.GcPreviousUser.AppearanceCell.Options.UseTextOptions = True
        Me.GcPreviousUser.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcPreviousUser.Caption = "Usuario que Traslada"
        Me.GcPreviousUser.FieldName = "PreviousUserCodeName"
        Me.GcPreviousUser.MinWidth = 23
        Me.GcPreviousUser.Name = "GcPreviousUser"
        Me.GcPreviousUser.OptionsColumn.AllowEdit = False
        Me.GcPreviousUser.Visible = True
        Me.GcPreviousUser.VisibleIndex = 3
        Me.GcPreviousUser.Width = 191
        '
        'GcEventDate
        '
        Me.GcEventDate.Caption = "Fecha Traslado"
        Me.GcEventDate.FieldName = "EventDate"
        Me.GcEventDate.MinWidth = 23
        Me.GcEventDate.Name = "GcEventDate"
        Me.GcEventDate.OptionsColumn.AllowEdit = False
        Me.GcEventDate.Visible = True
        Me.GcEventDate.VisibleIndex = 8
        '
        'GcAssignedUser
        '
        Me.GcAssignedUser.Caption = "Asignado"
        Me.GcAssignedUser.FieldName = "CurrentUserCodeName"
        Me.GcAssignedUser.MinWidth = 23
        Me.GcAssignedUser.Name = "GcAssignedUser"
        Me.GcAssignedUser.OptionsColumn.AllowEdit = False
        Me.GcAssignedUser.Visible = True
        Me.GcAssignedUser.VisibleIndex = 6
        Me.GcAssignedUser.Width = 82
        '
        'GcFolioStatus
        '
        Me.GcFolioStatus.Caption = "Estado Folio"
        Me.GcFolioStatus.FieldName = "FolioStatusDescription"
        Me.GcFolioStatus.MinWidth = 23
        Me.GcFolioStatus.Name = "GcFolioStatus"
        Me.GcFolioStatus.OptionsColumn.AllowEdit = False
        Me.GcFolioStatus.Width = 82
        '
        'GcPatientName
        '
        Me.GcPatientName.Caption = "Paciente"
        Me.GcPatientName.FieldName = "PatientFullName"
        Me.GcPatientName.MinWidth = 23
        Me.GcPatientName.Name = "GcPatientName"
        Me.GcPatientName.OptionsColumn.AllowEdit = False
        Me.GcPatientName.Width = 62
        '
        'GcAdmissionDate
        '
        Me.GcAdmissionDate.Caption = "Fecha Ingreso"
        Me.GcAdmissionDate.FieldName = "AdmissionDate"
        Me.GcAdmissionDate.MinWidth = 23
        Me.GcAdmissionDate.Name = "GcAdmissionDate"
        Me.GcAdmissionDate.OptionsColumn.AllowEdit = False
        Me.GcAdmissionDate.Width = 62
        '
        'GcPatientCode
        '
        Me.GcPatientCode.Caption = "Identificación"
        Me.GcPatientCode.FieldName = "PatientCode"
        Me.GcPatientCode.MinWidth = 23
        Me.GcPatientCode.Name = "GcPatientCode"
        Me.GcPatientCode.OptionsColumn.AllowEdit = False
        Me.GcPatientCode.Width = 62
        '
        'GcFunctionalUnit
        '
        Me.GcFunctionalUnit.Caption = "Unidad Funcional"
        Me.GcFunctionalUnit.FieldName = "FunctionalUnitName"
        Me.GcFunctionalUnit.MinWidth = 23
        Me.GcFunctionalUnit.Name = "GcFunctionalUnit"
        Me.GcFunctionalUnit.OptionsColumn.AllowEdit = False
        Me.GcFunctionalUnit.Width = 62
        '
        'GcBed
        '
        Me.GcBed.Caption = "Cama"
        Me.GcBed.FieldName = "BedNumber"
        Me.GcBed.MinWidth = 23
        Me.GcBed.Name = "GcBed"
        Me.GcBed.OptionsColumn.AllowEdit = False
        Me.GcBed.Width = 76
        '
        'GcDiagnosis
        '
        Me.GcDiagnosis.Caption = "Diagnóstico"
        Me.GcDiagnosis.FieldName = "Diagnosis"
        Me.GcDiagnosis.MinWidth = 23
        Me.GcDiagnosis.Name = "GcDiagnosis"
        Me.GcDiagnosis.OptionsColumn.AllowEdit = False
        Me.GcDiagnosis.Width = 70
        '
        'GcFolioTotalValue
        '
        Me.GcFolioTotalValue.Caption = "Valor Folio"
        Me.GcFolioTotalValue.FieldName = "FolioTotalValue"
        Me.GcFolioTotalValue.MinWidth = 23
        Me.GcFolioTotalValue.Name = "GcFolioTotalValue"
        Me.GcFolioTotalValue.OptionsColumn.AllowEdit = False
        Me.GcFolioTotalValue.Width = 82
        '
        'GcFolioType
        '
        Me.GcFolioType.Caption = "Categoría Folio"
        Me.GcFolioType.FieldName = "FolioType"
        Me.GcFolioType.MinWidth = 23
        Me.GcFolioType.Name = "GcFolioType"
        Me.GcFolioType.OptionsColumn.AllowEdit = False
        Me.GcFolioType.Width = 82
        '
        'GcCreationUser
        '
        Me.GcCreationUser.Caption = "Usuario de Creación"
        Me.GcCreationUser.FieldName = "AdmissionCreationUser"
        Me.GcCreationUser.MinWidth = 23
        Me.GcCreationUser.Name = "GcCreationUser"
        Me.GcCreationUser.OptionsColumn.AllowEdit = False
        Me.GcCreationUser.Width = 133
        '
        'GcModificationUser
        '
        Me.GcModificationUser.Caption = "Usuario de Modificación"
        Me.GcModificationUser.FieldName = "AdmissionModificationUser"
        Me.GcModificationUser.MinWidth = 23
        Me.GcModificationUser.Name = "GcModificationUser"
        Me.GcModificationUser.OptionsColumn.AllowEdit = False
        Me.GcModificationUser.Width = 82
        '
        'GcTransferStatus
        '
        Me.GcTransferStatus.AppearanceCell.Options.UseTextOptions = True
        Me.GcTransferStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcTransferStatus.Caption = "Estado de Traslado"
        Me.GcTransferStatus.FieldName = "TransferStatus"
        Me.GcTransferStatus.MinWidth = 23
        Me.GcTransferStatus.Name = "GcTransferStatus"
        Me.GcTransferStatus.OptionsColumn.AllowEdit = False
        Me.GcTransferStatus.Visible = True
        Me.GcTransferStatus.VisibleIndex = 7
        Me.GcTransferStatus.Width = 115
        '
        'GcComments
        '
        Me.GcComments.Caption = "Comentario"
        Me.GcComments.FieldName = "Comments"
        Me.GcComments.MinWidth = 23
        Me.GcComments.Name = "GcComments"
        Me.GcComments.OptionsColumn.AllowEdit = False
        Me.GcComments.Visible = True
        Me.GcComments.VisibleIndex = 4
        Me.GcComments.Width = 133
        '
        'GcInvoiceNumber
        '
        Me.GcInvoiceNumber.Caption = "Número de Factura"
        Me.GcInvoiceNumber.FieldName = "InvoiceNumber"
        Me.GcInvoiceNumber.MinWidth = 23
        Me.GcInvoiceNumber.Name = "GcInvoiceNumber"
        Me.GcInvoiceNumber.OptionsColumn.AllowEdit = False
        Me.GcInvoiceNumber.Width = 132
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.Appearance.Options.UseImage = True
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Custom
        Me.RepositoryItemCheckEdit1.CheckBoxOptions.SvgImageSize = New System.Drawing.Size(10, 10)
        Me.RepositoryItemCheckEdit1.ImageOptions.ImageChecked = Global.Presentation.AccountManagement.My.Resources.Resources.Alerta
        Me.RepositoryItemCheckEdit1.ImageOptions.ImageUnchecked = Global.Presentation.AccountManagement.My.Resources.Resources.aceptar16x16
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'INDPccAddAlert
        '
        Me.INDPccAddAlert.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPccAddAlert.Controls.Add(Me.LayoutControl2)
        Me.INDPccAddAlert.Location = New System.Drawing.Point(987, 123)
        Me.INDPccAddAlert.Manager = Me.BarManager
        Me.INDPccAddAlert.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPccAddAlert.Name = "INDPccAddAlert"
        Me.INDPccAddAlert.Size = New System.Drawing.Size(449, 303)
        Me.INDPccAddAlert.TabIndex = 29
        Me.INDPccAddAlert.Visible = False
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDSbAddNewAlert)
        Me.LayoutControl2.Controls.Add(Me.INDTeNewAlert_Comment)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(767, 140, 764, 569)
        Me.LayoutControl2.Root = Me.INDLcgNewAlert
        Me.LayoutControl2.Size = New System.Drawing.Size(449, 303)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDSbAddNewAlert
        '
        Me.INDSbAddNewAlert.Location = New System.Drawing.Point(9, 252)
        Me.INDSbAddNewAlert.Margin = New System.Windows.Forms.Padding(0)
        Me.INDSbAddNewAlert.MaximumSize = New System.Drawing.Size(428, 37)
        Me.INDSbAddNewAlert.MinimumSize = New System.Drawing.Size(428, 37)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbAddNewAlert, False)
        Me.INDSbAddNewAlert.Name = "INDSbAddNewAlert"
        Me.INDSbAddNewAlert.Size = New System.Drawing.Size(428, 37)
        Me.INDSbAddNewAlert.StyleController = Me.LayoutControl2
        Me.INDSbAddNewAlert.TabIndex = 4
        Me.INDSbAddNewAlert.Text = "Crear Alerta"
        '
        'INDTeNewAlert_Comment
        '
        Me.INDTeNewAlert_Comment.EditValue = ""
        Me.INDTeNewAlert_Comment.Location = New System.Drawing.Point(11, 73)
        Me.INDTeNewAlert_Comment.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDTeNewAlert_Comment.MaximumSize = New System.Drawing.Size(423, 166)
        Me.INDTeNewAlert_Comment.MenuManager = Me.BarManager
        Me.INDTeNewAlert_Comment.MinimumSize = New System.Drawing.Size(423, 166)
        Me.INDTeNewAlert_Comment.Name = "INDTeNewAlert_Comment"
        Me.INDTeNewAlert_Comment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDTeNewAlert_Comment.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTeNewAlert_Comment.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDTeNewAlert_Comment.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.INDTeNewAlert_Comment.Properties.AutoHeight = False
        Me.INDTeNewAlert_Comment.Properties.MaxLength = 500
        Me.INDTeNewAlert_Comment.Size = New System.Drawing.Size(423, 166)
        Me.INDTeNewAlert_Comment.StyleController = Me.LayoutControl2
        Me.INDTeNewAlert_Comment.TabIndex = 5
        '
        'INDLcgNewAlert
        '
        Me.INDLcgNewAlert.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgNewAlert.AppearanceGroup.Options.UseFont = True
        Me.INDLcgNewAlert.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgNewAlert.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgNewAlert.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgNewAlert.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgNewAlert.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgNewAlert.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgNewAlert.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgNewAlert.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgNewAlert.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgNewAlert.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgNewAlert.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgNewAlert.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgNewAlert, False)
        Me.INDLcgNewAlert.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgNewAlert.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.INDLcgNewAlert.GroupStyle = DevExpress.Utils.GroupStyle.Title
        Me.INDLcgNewAlert.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10, Me.INDLciNewAlert_Comments})
        Me.INDLcgNewAlert.Name = "INDLcgNewAlert"
        Me.INDLcgNewAlert.Size = New System.Drawing.Size(449, 303)
        Me.INDLcgNewAlert.Text = "Nueva Alerta"
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.INDSbAddNewAlert
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 207)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(428, 37)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(428, 37)
        Me.LayoutControlItem10.Name = "LayoutControlItem4"
        Me.LayoutControlItem10.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem10.Size = New System.Drawing.Size(431, 42)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'INDLciNewAlert_Comments
        '
        Me.INDLciNewAlert_Comments.Control = Me.INDTeNewAlert_Comment
        Me.INDLciNewAlert_Comments.Location = New System.Drawing.Point(0, 0)
        Me.INDLciNewAlert_Comments.MaxSize = New System.Drawing.Size(428, 207)
        Me.INDLciNewAlert_Comments.MinSize = New System.Drawing.Size(428, 207)
        Me.INDLciNewAlert_Comments.Name = "INDLciNewAlert_Comments"
        Me.INDLciNewAlert_Comments.Size = New System.Drawing.Size(431, 207)
        Me.INDLciNewAlert_Comments.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNewAlert_Comments.Text = "Comentario"
        Me.INDLciNewAlert_Comments.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNewAlert_Comments.TextSize = New System.Drawing.Size(95, 23)
        '
        'INDGcTransfers
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcTransfers, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcTransfers, Nothing)
        Me.INDGcTransfers.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcTransfers, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcTransfers, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcTransfers, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcTransfers, False)
        Me.INDGcTransfers.Location = New System.Drawing.Point(14, 81)
        Me.INDGcTransfers.MainView = Me.INDGvTransfers
        Me.INDGcTransfers.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGcTransfers.Name = "INDGcTransfers"
        Me.INDGcTransfers.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRiceFolioAlert})
        Me.INDGcTransfers.Size = New System.Drawing.Size(1427, 481)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcTransfers, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcTransfers.TabIndex = 26
        Me.INDGcTransfers.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvTransfers})
        '
        'INDGvTransfers
        '
        Me.INDGvTransfers.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvTransfers.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvTransfers.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvTransfers.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvTransfers.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvTransfers.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvTransfers.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvTransfers.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvTransfers.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvTransfers.Appearance.Row.Options.UseFont = True
        Me.INDGvTransfers.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvTransfers.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvTransfers.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCTransferCheck, Me.GCTransferAdmission, Me.GCTransferPatient, Me.GCTransferAlert, Me.GCTransferAdmissionDate, Me.GCTransferPatientCode, Me.GCTransferFunctionalUnit, Me.GCTransferCareGroup, Me.GCTransferBed, Me.GCTransferDiagnosis, Me.GCTransferFolioQty, Me.GCTransferFolioValue, Me.GCTransferFolioType, Me.GCTransferFolioStatus, Me.GCTransferTime, Me.GCTransferAssignedUser, Me.GCTransferInvoiceUser, Me.GCTransferManagementArea, Me.GCTransferCreationUser, Me.GCTransferModificationUser, Me.GCTransferInvoiceNumber, Me.GCTransferTransferStatus})
        Me.INDGvTransfers.DetailHeight = 431
        Me.INDGvTransfers.GridControl = Me.INDGcTransfers
        Me.INDGvTransfers.Name = "INDGvTransfers"
        Me.INDGvTransfers.OptionsSelection.MultiSelect = True
        Me.INDGvTransfers.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvTransfers.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvTransfers.OptionsView.ShowAutoFilterRow = True
        Me.INDGvTransfers.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewCurrentAlerts.SetTemaIndigoMetro(Me.INDGvTransfers, True)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDGvTransfers, False)
        '
        'GCTransferCheck
        '
        Me.GCTransferCheck.FieldName = "GCTransferCheck"
        Me.GCTransferCheck.MinWidth = 23
        Me.GCTransferCheck.Name = "GCTransferCheck"
        Me.GCTransferCheck.OptionsColumn.AllowEdit = False
        Me.GCTransferCheck.OptionsColumn.AllowFocus = False
        Me.GCTransferCheck.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCTransferCheck.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCTransferCheck.OptionsColumn.AllowMove = False
        Me.GCTransferCheck.OptionsColumn.AllowShowHide = False
        Me.GCTransferCheck.OptionsColumn.AllowSize = False
        Me.GCTransferCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCTransferCheck.OptionsColumn.ShowCaption = False
        Me.GCTransferCheck.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.GCTransferCheck.Visible = True
        Me.GCTransferCheck.VisibleIndex = 0
        Me.GCTransferCheck.Width = 47
        '
        'GCTransferAdmission
        '
        Me.GCTransferAdmission.Caption = "Ingreso"
        Me.GCTransferAdmission.FieldName = "AdmissionNumber"
        Me.GCTransferAdmission.MinWidth = 23
        Me.GCTransferAdmission.Name = "GCTransferAdmission"
        Me.GCTransferAdmission.OptionsColumn.AllowEdit = False
        Me.GCTransferAdmission.Visible = True
        Me.GCTransferAdmission.VisibleIndex = 1
        Me.GCTransferAdmission.Width = 62
        '
        'GCTransferPatient
        '
        Me.GCTransferPatient.Caption = "Paciente"
        Me.GCTransferPatient.FieldName = "PatientFullName"
        Me.GCTransferPatient.MinWidth = 23
        Me.GCTransferPatient.Name = "GCTransferPatient"
        Me.GCTransferPatient.OptionsColumn.AllowEdit = False
        Me.GCTransferPatient.Visible = True
        Me.GCTransferPatient.VisibleIndex = 2
        Me.GCTransferPatient.Width = 62
        '
        'GCTransferAlert
        '
        Me.GCTransferAlert.AppearanceCell.Options.UseImage = True
        Me.GCTransferAlert.Caption = "Alerta"
        Me.GCTransferAlert.ColumnEdit = Me.INDRiceFolioAlert
        Me.GCTransferAlert.FieldName = "HasFolioAlert"
        Me.GCTransferAlert.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.GCTransferAlert.ImageOptions.Image = Global.Presentation.AccountManagement.My.Resources.Resources.Alerta
        Me.GCTransferAlert.MinWidth = 23
        Me.GCTransferAlert.Name = "GCTransferAlert"
        Me.GCTransferAlert.OptionsColumn.AllowEdit = False
        Me.GCTransferAlert.Visible = True
        Me.GCTransferAlert.VisibleIndex = 3
        Me.GCTransferAlert.Width = 62
        '
        'INDRiceFolioAlert
        '
        Me.INDRiceFolioAlert.Appearance.Options.UseImage = True
        Me.INDRiceFolioAlert.AutoHeight = False
        Me.INDRiceFolioAlert.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.Custom
        Me.INDRiceFolioAlert.CheckBoxOptions.SvgImageSize = New System.Drawing.Size(10, 10)
        Me.INDRiceFolioAlert.ImageOptions.ImageChecked = Global.Presentation.AccountManagement.My.Resources.Resources.Alerta
        Me.INDRiceFolioAlert.ImageOptions.ImageUnchecked = Global.Presentation.AccountManagement.My.Resources.Resources.aceptar16x16
        Me.INDRiceFolioAlert.Name = "INDRiceFolioAlert"
        '
        'GCTransferAdmissionDate
        '
        Me.GCTransferAdmissionDate.Caption = "Fecha Ingreso"
        Me.GCTransferAdmissionDate.FieldName = "AdmissionDate"
        Me.GCTransferAdmissionDate.MinWidth = 23
        Me.GCTransferAdmissionDate.Name = "GCTransferAdmissionDate"
        Me.GCTransferAdmissionDate.OptionsColumn.AllowEdit = False
        Me.GCTransferAdmissionDate.Visible = True
        Me.GCTransferAdmissionDate.VisibleIndex = 4
        Me.GCTransferAdmissionDate.Width = 62
        '
        'GCTransferPatientCode
        '
        Me.GCTransferPatientCode.Caption = "Identificación"
        Me.GCTransferPatientCode.FieldName = "PatientCode"
        Me.GCTransferPatientCode.MinWidth = 23
        Me.GCTransferPatientCode.Name = "GCTransferPatientCode"
        Me.GCTransferPatientCode.OptionsColumn.AllowEdit = False
        Me.GCTransferPatientCode.Visible = True
        Me.GCTransferPatientCode.VisibleIndex = 5
        Me.GCTransferPatientCode.Width = 62
        '
        'GCTransferFunctionalUnit
        '
        Me.GCTransferFunctionalUnit.Caption = "Unidad Funcional"
        Me.GCTransferFunctionalUnit.FieldName = "FunctionalUnitName"
        Me.GCTransferFunctionalUnit.MinWidth = 23
        Me.GCTransferFunctionalUnit.Name = "GCTransferFunctionalUnit"
        Me.GCTransferFunctionalUnit.OptionsColumn.AllowEdit = False
        Me.GCTransferFunctionalUnit.Visible = True
        Me.GCTransferFunctionalUnit.VisibleIndex = 6
        Me.GCTransferFunctionalUnit.Width = 62
        '
        'GCTransferCareGroup
        '
        Me.GCTransferCareGroup.Caption = "Grupo de Atención"
        Me.GCTransferCareGroup.FieldName = "CareGroupName"
        Me.GCTransferCareGroup.MinWidth = 23
        Me.GCTransferCareGroup.Name = "GCTransferCareGroup"
        Me.GCTransferCareGroup.OptionsColumn.AllowEdit = False
        Me.GCTransferCareGroup.Visible = True
        Me.GCTransferCareGroup.VisibleIndex = 7
        Me.GCTransferCareGroup.Width = 62
        '
        'GCTransferBed
        '
        Me.GCTransferBed.Caption = "Cama"
        Me.GCTransferBed.FieldName = "BedNumber"
        Me.GCTransferBed.MinWidth = 23
        Me.GCTransferBed.Name = "GCTransferBed"
        Me.GCTransferBed.OptionsColumn.AllowEdit = False
        Me.GCTransferBed.Visible = True
        Me.GCTransferBed.VisibleIndex = 8
        Me.GCTransferBed.Width = 62
        '
        'GCTransferDiagnosis
        '
        Me.GCTransferDiagnosis.Caption = "Diagnóstico"
        Me.GCTransferDiagnosis.FieldName = "Diagnosis"
        Me.GCTransferDiagnosis.MinWidth = 23
        Me.GCTransferDiagnosis.Name = "GCTransferDiagnosis"
        Me.GCTransferDiagnosis.OptionsColumn.AllowEdit = False
        Me.GCTransferDiagnosis.Visible = True
        Me.GCTransferDiagnosis.VisibleIndex = 9
        Me.GCTransferDiagnosis.Width = 62
        '
        'GCTransferFolioQty
        '
        Me.GCTransferFolioQty.Caption = "Folios"
        Me.GCTransferFolioQty.FieldName = "FolioNumber"
        Me.GCTransferFolioQty.MinWidth = 23
        Me.GCTransferFolioQty.Name = "GCTransferFolioQty"
        Me.GCTransferFolioQty.OptionsColumn.AllowEdit = False
        Me.GCTransferFolioQty.Visible = True
        Me.GCTransferFolioQty.VisibleIndex = 10
        Me.GCTransferFolioQty.Width = 62
        '
        'GCTransferFolioValue
        '
        Me.GCTransferFolioValue.Caption = "Valor Folio"
        Me.GCTransferFolioValue.FieldName = "FolioTotalValue"
        Me.GCTransferFolioValue.MinWidth = 23
        Me.GCTransferFolioValue.Name = "GCTransferFolioValue"
        Me.GCTransferFolioValue.OptionsColumn.AllowEdit = False
        Me.GCTransferFolioValue.Visible = True
        Me.GCTransferFolioValue.VisibleIndex = 11
        Me.GCTransferFolioValue.Width = 62
        '
        'GCTransferFolioType
        '
        Me.GCTransferFolioType.Caption = "Categoría Folio"
        Me.GCTransferFolioType.FieldName = "FolioType"
        Me.GCTransferFolioType.MinWidth = 23
        Me.GCTransferFolioType.Name = "GCTransferFolioType"
        Me.GCTransferFolioType.OptionsColumn.AllowEdit = False
        Me.GCTransferFolioType.Visible = True
        Me.GCTransferFolioType.VisibleIndex = 12
        Me.GCTransferFolioType.Width = 62
        '
        'GCTransferFolioStatus
        '
        Me.GCTransferFolioStatus.Caption = "Estado Folio"
        Me.GCTransferFolioStatus.FieldName = "FolioStatusDescription"
        Me.GCTransferFolioStatus.MinWidth = 23
        Me.GCTransferFolioStatus.Name = "GCTransferFolioStatus"
        Me.GCTransferFolioStatus.OptionsColumn.AllowEdit = False
        Me.GCTransferFolioStatus.Visible = True
        Me.GCTransferFolioStatus.VisibleIndex = 13
        Me.GCTransferFolioStatus.Width = 62
        '
        'GCTransferTime
        '
        Me.GCTransferTime.Caption = "Tiempo"
        Me.GCTransferTime.FieldName = "IsOnTime"
        Me.GCTransferTime.MinWidth = 23
        Me.GCTransferTime.Name = "GCTransferTime"
        Me.GCTransferTime.OptionsColumn.AllowEdit = False
        Me.GCTransferTime.Visible = True
        Me.GCTransferTime.VisibleIndex = 14
        Me.GCTransferTime.Width = 62
        '
        'GCTransferAssignedUser
        '
        Me.GCTransferAssignedUser.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GCTransferAssignedUser.Caption = "Asignado"
        Me.GCTransferAssignedUser.FieldName = "AssignedUserFullname"
        Me.GCTransferAssignedUser.MinWidth = 23
        Me.GCTransferAssignedUser.Name = "GCTransferAssignedUser"
        Me.GCTransferAssignedUser.OptionsColumn.AllowEdit = False
        Me.GCTransferAssignedUser.Visible = True
        Me.GCTransferAssignedUser.VisibleIndex = 15
        Me.GCTransferAssignedUser.Width = 62
        '
        'GCTransferInvoiceUser
        '
        Me.GCTransferInvoiceUser.Caption = "Usuario que Factura"
        Me.GCTransferInvoiceUser.FieldName = "InvoiceUser"
        Me.GCTransferInvoiceUser.MinWidth = 23
        Me.GCTransferInvoiceUser.Name = "GCTransferInvoiceUser"
        Me.GCTransferInvoiceUser.OptionsColumn.AllowEdit = False
        Me.GCTransferInvoiceUser.Visible = True
        Me.GCTransferInvoiceUser.VisibleIndex = 19
        Me.GCTransferInvoiceUser.Width = 62
        '
        'GCTransferManagementArea
        '
        Me.GCTransferManagementArea.Caption = "Area de Gestión"
        Me.GCTransferManagementArea.FieldName = "ManagementAreaName"
        Me.GCTransferManagementArea.MinWidth = 23
        Me.GCTransferManagementArea.Name = "GCTransferManagementArea"
        Me.GCTransferManagementArea.OptionsColumn.AllowEdit = False
        Me.GCTransferManagementArea.Visible = True
        Me.GCTransferManagementArea.VisibleIndex = 16
        Me.GCTransferManagementArea.Width = 62
        '
        'GCTransferCreationUser
        '
        Me.GCTransferCreationUser.Caption = "Usuario de Creación"
        Me.GCTransferCreationUser.FieldName = "AdmissionCreationUser"
        Me.GCTransferCreationUser.MinWidth = 23
        Me.GCTransferCreationUser.Name = "GCTransferCreationUser"
        Me.GCTransferCreationUser.Visible = True
        Me.GCTransferCreationUser.VisibleIndex = 17
        Me.GCTransferCreationUser.Width = 62
        '
        'GCTransferModificationUser
        '
        Me.GCTransferModificationUser.Caption = "Usuario de Modificación"
        Me.GCTransferModificationUser.FieldName = "AdmissionModificationUser"
        Me.GCTransferModificationUser.MinWidth = 23
        Me.GCTransferModificationUser.Name = "GCTransferModificationUser"
        Me.GCTransferModificationUser.OptionsColumn.AllowEdit = False
        Me.GCTransferModificationUser.Visible = True
        Me.GCTransferModificationUser.VisibleIndex = 18
        Me.GCTransferModificationUser.Width = 62
        '
        'GCTransferInvoiceNumber
        '
        Me.GCTransferInvoiceNumber.Caption = "Número de Factura"
        Me.GCTransferInvoiceNumber.FieldName = "AssociatedInvoice"
        Me.GCTransferInvoiceNumber.MinWidth = 23
        Me.GCTransferInvoiceNumber.Name = "GCTransferInvoiceNumber"
        Me.GCTransferInvoiceNumber.OptionsColumn.AllowEdit = False
        Me.GCTransferInvoiceNumber.Visible = True
        Me.GCTransferInvoiceNumber.VisibleIndex = 20
        Me.GCTransferInvoiceNumber.Width = 62
        '
        'GCTransferTransferStatus
        '
        Me.GCTransferTransferStatus.Caption = "Estado del traslado"
        Me.GCTransferTransferStatus.FieldName = "TransferStatus"
        Me.GCTransferTransferStatus.MinWidth = 23
        Me.GCTransferTransferStatus.Name = "GCTransferTransferStatus"
        Me.GCTransferTransferStatus.OptionsColumn.AllowEdit = False
        Me.GCTransferTransferStatus.Visible = True
        Me.GCTransferTransferStatus.VisibleIndex = 21
        Me.GCTransferTransferStatus.Width = 101
        '
        'INDLcgMain
        '
        Me.INDLcgMain.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMain.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMain.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMain.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMain.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMain, False)
        Me.INDLcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgMain.GroupBordersVisible = False
        Me.INDLcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDTcgAccountManagement, Me.LabelInformation})
        Me.INDLcgMain.Name = "Root"
        Me.INDLcgMain.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLcgMain.Size = New System.Drawing.Size(1455, 576)
        Me.INDLcgMain.TextVisible = False
        '
        'INDTcgAccountManagement
        '
        Me.INDTcgAccountManagement.CustomizationFormText = "INDTcgAccountManagement"
        Me.INDTcgAccountManagement.Location = New System.Drawing.Point(0, 27)
        Me.INDTcgAccountManagement.Name = "INDTcgAccountManagement"
        Me.INDTcgAccountManagement.SelectedTabPage = Me.INDLcgTraceability
        Me.INDTcgAccountManagement.Size = New System.Drawing.Size(1455, 549)
        Me.INDTcgAccountManagement.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgTransfers, Me.INDLcgTraceability})
        '
        'INDLcgTraceability
        '
        Me.INDLcgTraceability.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgTraceability.AppearanceGroup.Options.UseFont = True
        Me.INDLcgTraceability.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgTraceability.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgTraceability.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTraceability.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgTraceability.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgTraceability.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgTraceability.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTraceability.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgTraceability.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTraceability.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgTraceability.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTraceability.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgTraceability, False)
        Me.INDLcgTraceability.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12, Me.LayoutControlItem13})
        Me.INDLcgTraceability.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgTraceability.Name = "INDLcgTraceability"
        Me.INDLcgTraceability.Padding = New DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5)
        Me.INDLcgTraceability.Size = New System.Drawing.Size(1431, 485)
        Me.INDLcgTraceability.Text = "Trazabilidad"
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.INDGcTraceability
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 47)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(1431, 438)
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.LayoutControl3
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(28, 30)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(1431, 47)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextVisible = False
        '
        'INDLcgTransfers
        '
        Me.INDLcgTransfers.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgTransfers.AppearanceGroup.Options.UseFont = True
        Me.INDLcgTransfers.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgTransfers.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgTransfers.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTransfers.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgTransfers.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgTransfers.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgTransfers.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTransfers.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgTransfers.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTransfers.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgTransfers.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTransfers.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgTransfers, False)
        Me.INDLcgTransfers.CustomizationFormText = "Traslados"
        Me.INDLcgTransfers.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTransfers})
        Me.INDLcgTransfers.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgTransfers.Name = "INDLcgTransfers"
        Me.INDLcgTransfers.Size = New System.Drawing.Size(1431, 485)
        Me.INDLcgTransfers.Text = "Traslados"
        '
        'INDLciTransfers
        '
        Me.INDLciTransfers.Control = Me.INDGcTransfers
        Me.INDLciTransfers.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTransfers.CustomizationFormText = "LayoutControlItem7"
        Me.INDLciTransfers.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTransfers.Name = "INDLciTransfers"
        Me.INDLciTransfers.Size = New System.Drawing.Size(1431, 485)
        Me.INDLciTransfers.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciTransfers.TextVisible = False
        '
        'LabelInformation
        '
        Me.LabelInformation.AllowHotTrack = False
        Me.LabelInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelInformation.AppearanceItemCaption.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText
        Me.LabelInformation.AppearanceItemCaption.Options.UseFont = True
        Me.LabelInformation.AppearanceItemCaption.Options.UseForeColor = True
        Me.LabelInformation.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LabelInformation.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelInformation.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelInformation.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LabelInformation.Location = New System.Drawing.Point(0, 0)
        Me.LabelInformation.MaxSize = New System.Drawing.Size(287, 27)
        Me.LabelInformation.MinSize = New System.Drawing.Size(287, 27)
        Me.LabelInformation.Name = "LabelInformation"
        Me.LabelInformation.Size = New System.Drawing.Size(1455, 27)
        Me.LabelInformation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LabelInformation.Text = "Los ingresos listados ya tienen folio"
        Me.LabelInformation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LabelInformation.TextSize = New System.Drawing.Size(283, 23)
        '
        'PanelControl11
        '
        Me.PanelControl11.Controls.Add(Me.INDSleAttentionCenter)
        Me.PanelControl11.Location = New System.Drawing.Point(24, 24)
        Me.PanelControl11.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl11.Name = "PanelControl11"
        Me.PanelControl11.Size = New System.Drawing.Size(1339, 76)
        Me.PanelControl11.TabIndex = 15
        '
        'INDSleAttentionCenter
        '
        Me.INDSleAttentionCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDSleAttentionCenter.Location = New System.Drawing.Point(2, 2)
        Me.INDSleAttentionCenter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleAttentionCenter.MaximumSize = New System.Drawing.Size(0, 74)
        Me.INDSleAttentionCenter.MinimumSize = New System.Drawing.Size(0, 74)
        Me.INDSleAttentionCenter.Name = "INDSleAttentionCenter"
        Me.INDSleAttentionCenter.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleAttentionCenter.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.INDSleAttentionCenter.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDSleAttentionCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleAttentionCenter.Properties.Appearance.ForeColor = System.Drawing.Color.Transparent
        Me.INDSleAttentionCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleAttentionCenter.Properties.Appearance.Options.UseBorderColor = True
        Me.INDSleAttentionCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSleAttentionCenter.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleAttentionCenter.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleAttentionCenter.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDSleAttentionCenter.Properties.DisplayMember = "CodeName"
        Me.INDSleAttentionCenter.Properties.NullText = "Seleccione un Centro de Atención"
        Me.INDSleAttentionCenter.Properties.PopupSizeable = False
        Me.INDSleAttentionCenter.Properties.PopupView = Me.INDGvSearchCareGroup
        Me.INDSleAttentionCenter.Properties.ShowClearButton = False
        Me.INDSleAttentionCenter.Properties.ShowFooter = False
        Me.INDSleAttentionCenter.Properties.ValueMember = "CODCENATE"
        Me.INDSleAttentionCenter.Size = New System.Drawing.Size(1335, 74)
        Me.INDSleAttentionCenter.StyleController = Me.INDLcMainData
        Me.INDSleAttentionCenter.TabIndex = 6
        '
        'INDGvSearchCareGroup
        '
        Me.INDGvSearchCareGroup.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSearchCareGroup.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSearchCareGroup.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSearchCareGroup.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSearchCareGroup.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvSearchCareGroup.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSearchCareGroup.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSearchCareGroup.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSearchCareGroup.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSearchCareGroup.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSearchCareGroup.Appearance.Row.Options.UseFont = True
        Me.INDGvSearchCareGroup.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCAttentionCenterCode, Me.GCAttentionCenterName})
        Me.INDGvSearchCareGroup.DetailHeight = 431
        Me.INDGvSearchCareGroup.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvSearchCareGroup.Name = "INDGvSearchCareGroup"
        Me.INDGvSearchCareGroup.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvSearchCareGroup.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSearchCareGroup.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSearchCareGroup.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSearchCareGroup.OptionsView.ShowGroupPanel = False
        Me.IndigoGridViewCurrentAlerts.SetTemaIndigoMetro(Me.INDGvSearchCareGroup, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDGvSearchCareGroup, False)
        '
        'GCAttentionCenterCode
        '
        Me.GCAttentionCenterCode.Caption = "Código"
        Me.GCAttentionCenterCode.FieldName = "CODCENATE"
        Me.GCAttentionCenterCode.MinWidth = 23
        Me.GCAttentionCenterCode.Name = "GCAttentionCenterCode"
        Me.GCAttentionCenterCode.Visible = True
        Me.GCAttentionCenterCode.VisibleIndex = 0
        Me.GCAttentionCenterCode.Width = 167
        '
        'GCAttentionCenterName
        '
        Me.GCAttentionCenterName.Caption = "Nombre"
        Me.GCAttentionCenterName.FieldName = "NOMCENATE"
        Me.GCAttentionCenterName.MinWidth = 23
        Me.GCAttentionCenterName.Name = "GCAttentionCenterName"
        Me.GCAttentionCenterName.Visible = True
        Me.GCAttentionCenterName.VisibleIndex = 1
        Me.GCAttentionCenterName.Width = 323
        '
        'INDLcMainData
        '
        Me.INDLcMainData.AllowCustomization = False
        Me.INDLcMainData.Controls.Add(Me.INDTbAutomaticReload)
        Me.INDLcMainData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLcMainData, False)
        Me.INDLcMainData.Location = New System.Drawing.Point(2, 8)
        Me.INDLcMainData.Name = "INDLcMainData"
        Me.INDLcMainData.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(374, 111, 574, 569)
        Me.INDLcMainData.Root = Me.LayoutControlGroup21
        Me.INDLcMainData.Size = New System.Drawing.Size(1294, 541)
        Me.INDLcMainData.TabIndex = 10
        Me.INDLcMainData.Text = "LayoutControl2"
        '
        'INDTbAutomaticReload
        '
        Me.INDTbAutomaticReload.EnterMoveNextControl = True
        Me.INDTbAutomaticReload.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.INDTbAutomaticReload.Location = New System.Drawing.Point(820, 120)
        Me.INDTbAutomaticReload.Name = "INDTbAutomaticReload"
        Me.INDTbAutomaticReload.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.INDTbAutomaticReload.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDTbAutomaticReload.Properties.OffText = "Off"
        Me.INDTbAutomaticReload.Properties.OnText = "On"
        Me.INDTbAutomaticReload.Properties.ShowText = False
        Me.INDTbAutomaticReload.Size = New System.Drawing.Size(50, 24)
        Me.INDTbAutomaticReload.StyleController = Me.INDLcMainData
        Me.INDTbAutomaticReload.TabIndex = 24
        Me.INDTbAutomaticReload.ToolTip = "Refrescado automático"
        Me.INDTbAutomaticReload.Visible = False
        '
        'LayoutControlGroup21
        '
        Me.LayoutControlGroup21.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup21.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup21.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup21.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup21.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup21.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup21.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup21, False)
        Me.LayoutControlGroup21.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup21.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup21.GroupBordersVisible = False
        Me.LayoutControlGroup21.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDTcgDashBoard, Me.LayoutControlGroup11})
        Me.LayoutControlGroup21.Name = "Root"
        Me.LayoutControlGroup21.Size = New System.Drawing.Size(1294, 541)
        Me.LayoutControlGroup21.TextVisible = False
        '
        'INDTcgDashBoard
        '
        Me.INDTcgDashBoard.Location = New System.Drawing.Point(0, 0)
        Me.INDTcgDashBoard.Name = "INDTcgDashBoard"
        Me.INDTcgDashBoard.SelectedTabPage = Nothing
        Me.INDTcgDashBoard.Size = New System.Drawing.Size(1274, 49)
        '
        'LayoutControlGroup11
        '
        Me.LayoutControlGroup11.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup11.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup11.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup11.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup11.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup11.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup11.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup11, False)
        Me.LayoutControlGroup11.Location = New System.Drawing.Point(0, 49)
        Me.LayoutControlGroup11.Name = "LayoutControlGroup11"
        Me.LayoutControlGroup11.Size = New System.Drawing.Size(1274, 472)
        Me.LayoutControlGroup11.TextVisible = False
        '
        'INDDdbMenuActions
        '
        Me.INDDdbMenuActions.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDdbMenuActions.DropDownControl = Me.INDPmActions
        Me.INDDdbMenuActions.ImageOptions.Image = Global.Presentation.AccountManagement.My.Resources.Resources.Mmenu_de_acciones
        Me.INDDdbMenuActions.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDDdbMenuActions.Location = New System.Drawing.Point(1367, 27)
        Me.INDDdbMenuActions.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDDdbMenuActions.MenuManager = Me.BarManager
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDDdbMenuActions, False)
        Me.INDDdbMenuActions.Name = "INDDdbMenuActions"
        Me.INDDdbMenuActions.Size = New System.Drawing.Size(108, 73)
        Me.INDDdbMenuActions.StyleController = Me.LayoutControl1
        Me.INDDdbMenuActions.TabIndex = 8
        Me.INDDdbMenuActions.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'INDPmActions
        '
        Me.INDPmActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiRefresh)})
        Me.INDPmActions.Manager = Me.BarManager
        Me.INDPmActions.Name = "INDPmActions"
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgContainer})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1499, 700)
        Me.Root.TextVisible = False
        '
        'INDLcgContainer
        '
        Me.INDLcgContainer.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgContainer.AppearanceGroup.Options.UseFont = True
        Me.INDLcgContainer.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgContainer.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgContainer.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgContainer.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgContainer.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgContainer.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgContainer.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgContainer.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgContainer.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgContainer.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgContainer.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgContainer.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgContainer, False)
        Me.INDLcgContainer.CustomizationFormText = "LayoutControlGroup1"
        Me.INDLcgContainer.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem6, Me.LayoutControlItem3})
        Me.INDLcgContainer.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgContainer.Name = "INDLcgContainer"
        Me.INDLcgContainer.Size = New System.Drawing.Size(1479, 680)
        Me.INDLcgContainer.Text = "LayoutControlGroup1"
        Me.INDLcgContainer.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.PanelControl11
        Me.LayoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(1, 1)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1343, 80)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "LayoutControlItem4"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDDdbMenuActions
        Me.LayoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.BottomCenter
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(1343, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(0, 80)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(58, 80)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2)
        Me.LayoutControlItem6.Size = New System.Drawing.Size(112, 80)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "LayoutControlItem3"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDLcMain
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 80)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem3.ShowInCustomizationForm = False
        Me.LayoutControlItem3.Size = New System.Drawing.Size(1455, 576)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'SkinBarSubItem11
        '
        Me.SkinBarSubItem11.AllowSerializeChildren = DevExpress.Utils.DefaultBoolean.[False]
        Me.SkinBarSubItem11.Caption = "SkinBarSubItem1"
        Me.SkinBarSubItem11.Id = 0
        Me.SkinBarSubItem11.Name = "SkinBarSubItem11"
        '
        'BarSubItem11
        '
        Me.BarSubItem11.Caption = "BarSubItem1"
        Me.BarSubItem11.Id = 9
        Me.BarSubItem11.Name = "BarSubItem11"
        '
        'PanelControl3
        '
        Me.PanelControl3.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(200, 100)
        Me.PanelControl3.TabIndex = 0
        '
        'RepositoryItemImageComboBox3
        '
        Me.RepositoryItemImageComboBox3.AutoHeight = False
        Me.RepositoryItemImageComboBox3.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tratamiento Nuevo", Global.Microsoft.VisualBasic.ChrW(49), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tratamiento Antiguo", Global.Microsoft.VisualBasic.ChrW(50), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Codigo Azul", Global.Microsoft.VisualBasic.ChrW(51), -1)})
        Me.RepositoryItemImageComboBox3.Name = "RepositoryItemImageComboBox3"
        '
        'RepositoryItemImageComboBox2
        '
        Me.RepositoryItemImageComboBox2.AutoHeight = False
        Me.RepositoryItemImageComboBox2.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tratamiento Nuevo", Global.Microsoft.VisualBasic.ChrW(49), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tratamiento Antiguo", Global.Microsoft.VisualBasic.ChrW(50), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Codigo Azul", Global.Microsoft.VisualBasic.ChrW(51), -1)})
        Me.RepositoryItemImageComboBox2.Name = "RepositoryItemImageComboBox2"
        '
        'PanelControl2
        '
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(200, 100)
        Me.PanelControl2.TabIndex = 0
        '
        'INDGcRequest
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcRequest, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcRequest, Nothing)
        Me.INDGcRequest.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcRequest, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcRequest, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcRequest, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcRequest, False)
        Me.INDGcRequest.Location = New System.Drawing.Point(24, 204)
        Me.INDGcRequest.MainView = Me.INDGvRequest
        Me.INDGcRequest.Name = "INDGcRequest"
        Me.INDGcRequest.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox11})
        Me.INDGcRequest.Size = New System.Drawing.Size(846, 313)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcRequest, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcRequest.TabIndex = 6
        Me.INDGcRequest.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvRequest})
        Me.INDGcRequest.Visible = False
        '
        'INDGvRequest
        '
        Me.INDGvRequest.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvRequest.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvRequest.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRequest.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvRequest.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRequest.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvRequest.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvRequest.Appearance.Row.Options.UseFont = True
        Me.INDGvRequest.Appearance.SelectedRow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.INDGvRequest.Appearance.SelectedRow.Options.UseBorderColor = True
        Me.INDGvRequest.Appearance.SelectedRow.Options.UseFont = True
        Me.INDGvRequest.Appearance.SelectedRow.Options.UseForeColor = True
        Me.INDGvRequest.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvRequest.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvRequest.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.INDGvRequest.CustomizationFormBounds = New System.Drawing.Rectangle(1070, 406, 210, 200)
        Me.INDGvRequest.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvRequest.GridControl = Me.INDGcRequest
        Me.INDGvRequest.GroupCount = 1
        Me.INDGvRequest.Name = "INDGvRequest"
        Me.INDGvRequest.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDGvRequest.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp
        Me.INDGvRequest.OptionsSelection.MultiSelect = True
        Me.INDGvRequest.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDGvRequest.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRequest.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRequest.OptionsView.ShowAutoFilterRow = True
        Me.INDGvRequest.OptionsView.ShowDetailButtons = False
        Me.INDGvRequest.OptionsView.ShowFooter = True
        Me.INDGvRequest.OptionsView.ShowGroupPanel = False
        Me.INDGvRequest.OptionsView.ShowIndicator = False
        Me.INDGvRequest.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn4, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridViewCurrentAlerts.SetTemaIndigoMetro(Me.INDGvRequest, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDGvRequest, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tipo"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemImageComboBox11
        Me.GridColumn4.FieldName = "Tipo"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'RepositoryItemImageComboBox11
        '
        Me.RepositoryItemImageComboBox11.AutoHeight = False
        Me.RepositoryItemImageComboBox11.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tratamiento Nuevo", Global.Microsoft.VisualBasic.ChrW(49), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Tratamiento Antiguo", Global.Microsoft.VisualBasic.ChrW(50), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Según necesidad", Global.Microsoft.VisualBasic.ChrW(51), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("STAT(Inmediatamente)", Global.Microsoft.VisualBasic.ChrW(52), -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Codigo Azul", Global.Microsoft.VisualBasic.ChrW(53), -1)})
        Me.RepositoryItemImageComboBox11.Name = "RepositoryItemImageComboBox11"
        '
        'GridColumn272
        '
        Me.GridColumn272.Caption = "Tipo"
        Me.GridColumn272.FieldName = "Item2"
        Me.GridColumn272.Name = "GridColumn272"
        Me.GridColumn272.Visible = True
        Me.GridColumn272.VisibleIndex = 1
        Me.GridColumn272.Width = 987
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(50, 20)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(50, 20)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(50, 28)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(50, 20)
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
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(50, 28)
        '
        'INDLciToggleButton
        '
        Me.INDLciToggleButton.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDLciToggleButton.Control = Me.INDTbAutomaticReload
        Me.INDLciToggleButton.Location = New System.Drawing.Point(796, 0)
        Me.INDLciToggleButton.MaxSize = New System.Drawing.Size(0, 22)
        Me.INDLciToggleButton.MinSize = New System.Drawing.Size(54, 22)
        Me.INDLciToggleButton.Name = "INDLciToggleButton"
        Me.INDLciToggleButton.Size = New System.Drawing.Size(54, 36)
        Me.INDLciToggleButton.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciToggleButton.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciToggleButton.TextVisible = False
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'RepositoryItemPopupContainerEdit6
        '
        Me.RepositoryItemPopupContainerEdit6.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit6.Name = "RepositoryItemPopupContainerEdit6"
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'RepositoryItemPopupContainerEdit5
        '
        Me.RepositoryItemPopupContainerEdit5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit5.Name = "RepositoryItemPopupContainerEdit5"
        '
        'RepositoryItemPopupContainerEdit4
        '
        Me.RepositoryItemPopupContainerEdit4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit4.Name = "RepositoryItemPopupContainerEdit4"
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemPopupContainerEdit7
        '
        Me.RepositoryItemPopupContainerEdit7.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit7.Name = "RepositoryItemPopupContainerEdit7"
        '
        'IndigoGridViewCurrentAlerts
        '
        Me.IndigoGridViewCurrentAlerts.RaiseMenuPopUp = True
        Me.IndigoGridViewCurrentAlerts.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        Me.IndigoGridView11.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit31
        '
        'RepositoryItemPopupContainerEdit31
        '
        Me.RepositoryItemPopupContainerEdit31.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit31.Name = "RepositoryItemPopupContainerEdit31"
        '
        'INDPmRowActions
        '
        Me.INDPmRowActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiTransferFolio)})
        Me.INDPmRowActions.Manager = Me.BarManager
        Me.INDPmRowActions.Name = "INDPmRowActions"
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
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(350, 125)
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup5, False)
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(350, 125)
        '
        'FrmDashboardAccountManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1503, 848)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "FrmDashboardAccountManagement"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "2854"
        Me.Text = "Dashboard Gestión de Cuentas"
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
        CType(Me.RepositoryItemImageComboBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDLcMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMain.ResumeLayout(False)
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl3.ResumeLayout(False)
        CType(Me.INDSleFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemRadioGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleFilterType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFilterType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFilterType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcTraceability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvTraceability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccAddAlert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccAddAlert.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDTeNewAlert_Comment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgNewAlert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNewAlert_Comments, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiceFolioAlert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgAccountManagement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgTraceability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabelInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl11.ResumeLayout(False)
        CType(Me.INDSleAttentionCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSearchCareGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMainData.ResumeLayout(False)
        CType(Me.INDTbAutomaticReload.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgDashBoard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPmActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgContainer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciToggleButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridViewCurrentAlerts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPmRowActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl11 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDSleAttentionCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvSearchCareGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCAttentionCenterCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCAttentionCenterName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemPopupContainerEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDLcMainData As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTbAutomaticReload As DevExpress.XtraEditors.ToggleSwitch
    Friend WithEvents PcContainerReportPhamacyNotes As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDPcReportViewerPharmacyNotes As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnCloseReportViewerPharmacyNote As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgcMixingStation As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDviewMixingStationDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn73 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn77 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn78 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn79 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDviewMixingStation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn62 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBirthDayMS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox4 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDgcChemotherapy As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewChemoterapy As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn67 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn69 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBirthDayQ As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox3 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents SkinBarSubItem11 As DevExpress.XtraBars.SkinBarSubItem
    Friend WithEvents INDBbiPrint As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarEditItem11 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemRadioGroup11 As DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup
    Friend WithEvents BarEditItem2 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTextEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents BarEditItem3 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents BarToggleSwitchItem11 As DevExpress.XtraBars.BarToggleSwitchItem
    Friend WithEvents BarDockingMenuItem11 As DevExpress.XtraBars.BarDockingMenuItem
    Friend WithEvents BarCheckItem11 As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents BarSubItem11 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarButtonItem11 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemImageComboBox2 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents RepositoryItemCheckEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDPcContainer As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDPcReportViewer As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDRptCheActivateDevolution As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDGcRequest As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvRequest As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageComboBox11 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptCheActivate As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDSleTypeFilter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvSearchType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn272 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup21 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTcgDashBoard As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlcgMixingStation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyMixingStation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgRequest As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciRequest As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygExtramural As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgReturn As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemChemotherapy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup11 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciReportViewer As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLycReportPhamacyNotes As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLycTypeFilter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyTypeFilter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciToggleButton As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDdbMenuActions As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDPmActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDLcgContainer As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridViewCurrentAlerts As IndigoGridView
    Friend WithEvents INDLcMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcTransfers As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvTransfers As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView11 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit31 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDTcgAccountManagement As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgTransfers As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciTransfers As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GCTransferCheck As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferAdmission As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferPatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferAlert As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferAdmissionDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferPatientCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferCareGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferBed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferDiagnosis As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferFolioQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferFolioValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferFolioType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferFolioStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferAssignedUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferManagementArea As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferCreationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferModificationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferInvoiceUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferInvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferTransferStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBbiTransferFolio As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPmRowActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents INDRiceFolioAlert As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPccAddAlert As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSbAddNewAlert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDTeNewAlert_Comment As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcgNewAlert As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciNewAlert_Comments As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgTraceability As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcTraceability As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvTraceability As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GcAdmissionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GcAdmissionDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcPatientCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcCareGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcBed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcDiagnosis As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFolios As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFolioTotalValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFolioType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFolioStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcAssignedUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcCreationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcModificationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GcTransferStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcPreviousUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcPreviousManagementArea As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcComments As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcInvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSleFilter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvFilter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleFilterType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvFilterType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSbCleanFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciFilterType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFilter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GcFilter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCColumn01 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCColumn02 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LabelInformation As DevExpress.XtraLayout.SimpleLabelItem
    Friend WithEvents GcEventDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
End Class
