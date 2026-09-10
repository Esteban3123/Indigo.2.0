Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardAccountAssignment
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
        Me.INDPcUserReassignment = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.INDSleUsers = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciUsers = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.BtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.SkinBarSubItem11 = New DevExpress.XtraBars.SkinBarSubItem()
        Me.INDBbiPrint = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.BarEditItem11 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemRadioGroup11 = New DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup()
        Me.INDBiRefresh = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BarEditItem3 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BarToggleSwitchItem11 = New DevExpress.XtraBars.BarToggleSwitchItem()
        Me.BarDockingMenuItem11 = New DevExpress.XtraBars.BarDockingMenuItem()
        Me.BarCheckItem11 = New DevExpress.XtraBars.BarCheckItem()
        Me.BarSubItem11 = New DevExpress.XtraBars.BarSubItem()
        Me.BarButtonItem11 = New DevExpress.XtraBars.BarButtonItem()
        Me.INDGcPendingAssignment = New DevExpress.XtraGrid.GridControl()
        Me.INDGvPendingAssignment = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCTransferCheck = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRiceCheck = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GCTransferAdmission = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferPatient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferAdmissionDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferPatientCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferCareGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferBed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferDiagnosis = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferFolioQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferAssignedUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCIngressType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferCreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCTransferModificationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgMain = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDTcgAccountManagement = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgTransfers = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTransfers = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PanelControl11 = New DevExpress.XtraEditors.PanelControl()
        Me.INDSleCareCenter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvSearchOperativeUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GCOperativeUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GCOperativeUnitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcMainData = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTbAutomaticReload = New DevExpress.XtraEditors.ToggleSwitch()
        Me.LayoutControlGroup21 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDTcgDashBoard = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup11 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDDdbMenuActions = New DevExpress.XtraEditors.DropDownButton()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgContainer = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
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
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit31 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
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
        CType(Me.INDPcUserReassignment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcUserReassignment.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDSleUsers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemRadioGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcPendingAssignment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPendingAssignment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRiceCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgAccountManagement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl11.SuspendLayout()
        CType(Me.INDSleCareCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSearchOperativeUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMainData.SuspendLayout()
        CType(Me.INDTbAutomaticReload.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgDashBoard, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1288, 553)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1288, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1288, 130)
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
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 8)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.Root
        Me.LayoutControl1.Size = New System.Drawing.Size(1284, 543)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLcMain
        '
        Me.INDLcMain.Controls.Add(Me.INDPcUserReassignment)
        Me.INDLcMain.Controls.Add(Me.INDGcPendingAssignment)
        Me.INDLcMain.Location = New System.Drawing.Point(20, 83)
        Me.INDLcMain.Margin = New System.Windows.Forms.Padding(0)
        Me.INDLcMain.Name = "INDLcMain"
        Me.INDLcMain.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1030, 417, 650, 400)
        Me.INDLcMain.Root = Me.INDLcgMain
        Me.INDLcMain.Size = New System.Drawing.Size(1244, 442)
        Me.INDLcMain.TabIndex = 16
        Me.INDLcMain.Text = "LayoutControl2"
        '
        'INDPcUserReassignment
        '
        Me.INDPcUserReassignment.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcUserReassignment.Controls.Add(Me.PanelControl1)
        Me.INDPcUserReassignment.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.INDPcUserReassignment.Location = New System.Drawing.Point(163, 150)
        Me.INDPcUserReassignment.Manager = Me.BarManager
        Me.INDPcUserReassignment.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPcUserReassignment.Name = "INDPcUserReassignment"
        Me.INDPcUserReassignment.Size = New System.Drawing.Size(431, 153)
        Me.INDPcUserReassignment.TabIndex = 57
        Me.INDPcUserReassignment.Visible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LayoutControl2)
        Me.PanelControl1.Controls.Add(Me.PanelControl4)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(431, 153)
        Me.PanelControl1.TabIndex = 0
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.Label1)
        Me.LayoutControl2.Controls.Add(Me.INDSleUsers)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(2, 2)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(427, 112)
        Me.LayoutControl2.TabIndex = 1
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(11, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(405, 32)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Reasignación de Usuario"
        '
        'INDSleUsers
        '
        Me.INDSleUsers.EnterMoveNextControl = True
        Me.INDSleUsers.Location = New System.Drawing.Point(11, 72)
        Me.INDSleUsers.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDSleUsers.Name = "INDSleUsers"
        Me.INDSleUsers.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleUsers.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleUsers.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleUsers.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleUsers.Properties.Appearance.Options.UseFont = True
        Me.INDSleUsers.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleUsers.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleUsers.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDSleUsers.Properties.AppearanceDisabled.Options.UseTextOptions = True
        Me.INDSleUsers.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDSleUsers.Properties.AppearanceDropDown.Options.UseTextOptions = True
        Me.INDSleUsers.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDSleUsers.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleUsers.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleUsers.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleUsers.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleUsers.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleUsers.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleUsers.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleUsers.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleUsers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
        Me.INDSleUsers.Properties.DisplayMember = "FullName"
        Me.INDSleUsers.Properties.NullText = ""
        Me.INDSleUsers.Properties.PopupFormMinSize = New System.Drawing.Size(501, 0)
        Me.INDSleUsers.Properties.PopupSizeable = False
        Me.INDSleUsers.Properties.PopupView = Me.GridView1
        Me.INDSleUsers.Properties.ShowClearButton = False
        Me.INDSleUsers.Properties.ShowFooter = False
        Me.INDSleUsers.Properties.ValueMember = "Id"
        Me.INDSleUsers.Size = New System.Drawing.Size(386, 28)
        Me.INDSleUsers.StyleController = Me.LayoutControl2
        Me.INDSleUsers.TabIndex = 0
        '
        'GridView1
        '
        Me.GridView1.Appearance.ColumnFilterButton.Options.UseTextOptions = True
        Me.GridView1.Appearance.ColumnFilterButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Options.UseTextOptions = True
        Me.GridView1.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsFind.FindFilterColumns = "AccountReceivableId.InvoiceNumber"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "UserCode"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "FullName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
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
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.INDLciUsers})
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(427, 112)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.Label1
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem5"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(409, 36)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'INDLciUsers
        '
        Me.INDLciUsers.AllowHide = False
        Me.INDLciUsers.Control = Me.INDSleUsers
        Me.INDLciUsers.CustomizationFormText = "Concepto de Conciliación"
        Me.INDLciUsers.Location = New System.Drawing.Point(0, 36)
        Me.INDLciUsers.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciUsers.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciUsers.Name = "INDLciUsers"
        Me.INDLciUsers.Size = New System.Drawing.Size(409, 60)
        Me.INDLciUsers.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciUsers.Text = "Usuarios"
        Me.INDLciUsers.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciUsers.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciUsers.TextSize = New System.Drawing.Size(99, 21)
        Me.INDLciUsers.TextToControlDistance = 5
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.BtnAdd)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl4.Location = New System.Drawing.Point(2, 114)
        Me.PanelControl4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(427, 37)
        Me.PanelControl4.TabIndex = 2
        '
        'BtnAdd
        '
        Me.BtnAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BtnAdd.Location = New System.Drawing.Point(2, 2)
        Me.BtnAdd.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(423, 33)
        Me.BtnAdd.TabIndex = 7
        Me.BtnAdd.Text = "Aceptar"
        '
        'BarManager
        '
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.SkinBarSubItem11, Me.INDBbiPrint, Me.INDBbiRefresh, Me.BarEditItem11, Me.INDBiRefresh, Me.BarEditItem3, Me.BarToggleSwitchItem11, Me.BarDockingMenuItem11, Me.BarCheckItem11, Me.BarSubItem11, Me.BarButtonItem11})
        Me.BarManager.MaxItemId = 12
        Me.BarManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemRadioGroup11, Me.RepositoryItemTextEdit11, Me.RepositoryItemTextEdit2})
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlTop.Manager = Me.BarManager
        Me.barDockControlTop.Size = New System.Drawing.Size(1288, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 689)
        Me.barDockControlBottom.Manager = Me.BarManager
        Me.barDockControlBottom.Size = New System.Drawing.Size(1288, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 6)
        Me.barDockControlLeft.Manager = Me.BarManager
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 683)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1288, 6)
        Me.barDockControlRight.Manager = Me.BarManager
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 683)
        '
        'SkinBarSubItem11
        '
        Me.SkinBarSubItem11.AllowSerializeChildren = DevExpress.Utils.DefaultBoolean.[False]
        Me.SkinBarSubItem11.Caption = "SkinBarSubItem1"
        Me.SkinBarSubItem11.Id = 0
        Me.SkinBarSubItem11.Name = "SkinBarSubItem11"
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
        'INDBiRefresh
        '
        Me.INDBiRefresh.Edit = Me.RepositoryItemTextEdit11
        Me.INDBiRefresh.Id = 4
        Me.INDBiRefresh.Name = "INDBiRefresh"
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
        'BarSubItem11
        '
        Me.BarSubItem11.Caption = "BarSubItem1"
        Me.BarSubItem11.Id = 9
        Me.BarSubItem11.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiRefresh)})
        Me.BarSubItem11.Name = "BarSubItem11"
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
        'INDGcPendingAssignment
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcPendingAssignment, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcPendingAssignment, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcPendingAssignment, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcPendingAssignment, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcPendingAssignment, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcPendingAssignment, False)
        Me.INDGcPendingAssignment.Location = New System.Drawing.Point(13, 35)
        Me.INDGcPendingAssignment.MainView = Me.INDGvPendingAssignment
        Me.INDGcPendingAssignment.Name = "INDGcPendingAssignment"
        Me.INDGcPendingAssignment.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRiceCheck})
        Me.INDGcPendingAssignment.Size = New System.Drawing.Size(1218, 395)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcPendingAssignment, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcPendingAssignment.TabIndex = 26
        Me.INDGcPendingAssignment.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvPendingAssignment})
        '
        'INDGvPendingAssignment
        '
        Me.INDGvPendingAssignment.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvPendingAssignment.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvPendingAssignment.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvPendingAssignment.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvPendingAssignment.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPendingAssignment.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvPendingAssignment.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvPendingAssignment.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvPendingAssignment.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvPendingAssignment.Appearance.Row.Options.UseFont = True
        Me.INDGvPendingAssignment.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvPendingAssignment.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvPendingAssignment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCTransferCheck, Me.GCTransferAdmission, Me.GCTransferPatient, Me.GCTransferAdmissionDate, Me.GCTransferPatientCode, Me.GCTransferFunctionalUnit, Me.GCTransferCareGroup, Me.GCTransferBed, Me.GCTransferDiagnosis, Me.GCTransferFolioQty, Me.GCTransferAssignedUser, Me.GCIngressType, Me.GCTransferCreationUser, Me.GCTransferModificationUser})
        Me.INDGvPendingAssignment.GridControl = Me.INDGcPendingAssignment
        Me.INDGvPendingAssignment.Name = "INDGvPendingAssignment"
        Me.INDGvPendingAssignment.OptionsSelection.MultiSelect = True
        Me.INDGvPendingAssignment.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvPendingAssignment.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvPendingAssignment.OptionsView.ShowAutoFilterRow = True
        Me.INDGvPendingAssignment.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPendingAssignment, False)
        '
        'GCTransferCheck
        '
        Me.GCTransferCheck.Caption = "Sel."
        Me.GCTransferCheck.ColumnEdit = Me.INDRiceCheck
        Me.GCTransferCheck.FieldName = "Check"
        Me.GCTransferCheck.Name = "GCTransferCheck"
        Me.GCTransferCheck.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCTransferCheck.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCTransferCheck.OptionsColumn.AllowMove = False
        Me.GCTransferCheck.OptionsColumn.AllowShowHide = False
        Me.GCTransferCheck.OptionsColumn.AllowSize = False
        Me.GCTransferCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GCTransferCheck.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.GCTransferCheck.Visible = True
        Me.GCTransferCheck.VisibleIndex = 0
        Me.GCTransferCheck.Width = 40
        '
        'INDRiceCheck
        '
        Me.INDRiceCheck.AutoHeight = False
        Me.INDRiceCheck.Name = "INDRiceCheck"
        '
        'GCTransferAdmission
        '
        Me.GCTransferAdmission.Caption = "Ingreso"
        Me.GCTransferAdmission.FieldName = "AdmissionNumber"
        Me.GCTransferAdmission.Name = "GCTransferAdmission"
        Me.GCTransferAdmission.OptionsColumn.AllowEdit = False
        Me.GCTransferAdmission.Visible = True
        Me.GCTransferAdmission.VisibleIndex = 1
        Me.GCTransferAdmission.Width = 53
        '
        'GCTransferPatient
        '
        Me.GCTransferPatient.Caption = "Paciente"
        Me.GCTransferPatient.FieldName = "PatientFullName"
        Me.GCTransferPatient.Name = "GCTransferPatient"
        Me.GCTransferPatient.OptionsColumn.AllowEdit = False
        Me.GCTransferPatient.Visible = True
        Me.GCTransferPatient.VisibleIndex = 2
        Me.GCTransferPatient.Width = 53
        '
        'GCTransferAdmissionDate
        '
        Me.GCTransferAdmissionDate.Caption = "Fecha Ingreso"
        Me.GCTransferAdmissionDate.FieldName = "AdmissionDate"
        Me.GCTransferAdmissionDate.Name = "GCTransferAdmissionDate"
        Me.GCTransferAdmissionDate.OptionsColumn.AllowEdit = False
        Me.GCTransferAdmissionDate.Visible = True
        Me.GCTransferAdmissionDate.VisibleIndex = 3
        Me.GCTransferAdmissionDate.Width = 53
        '
        'GCTransferPatientCode
        '
        Me.GCTransferPatientCode.Caption = "Identificación"
        Me.GCTransferPatientCode.FieldName = "Nit"
        Me.GCTransferPatientCode.Name = "GCTransferPatientCode"
        Me.GCTransferPatientCode.OptionsColumn.AllowEdit = False
        Me.GCTransferPatientCode.Visible = True
        Me.GCTransferPatientCode.VisibleIndex = 4
        Me.GCTransferPatientCode.Width = 53
        '
        'GCTransferFunctionalUnit
        '
        Me.GCTransferFunctionalUnit.Caption = "Unidad Funcional"
        Me.GCTransferFunctionalUnit.FieldName = "FunctionalUnitCodeName"
        Me.GCTransferFunctionalUnit.Name = "GCTransferFunctionalUnit"
        Me.GCTransferFunctionalUnit.OptionsColumn.AllowEdit = False
        Me.GCTransferFunctionalUnit.Visible = True
        Me.GCTransferFunctionalUnit.VisibleIndex = 5
        Me.GCTransferFunctionalUnit.Width = 53
        '
        'GCTransferCareGroup
        '
        Me.GCTransferCareGroup.Caption = "Grupo de Atención"
        Me.GCTransferCareGroup.FieldName = "CareGroup"
        Me.GCTransferCareGroup.Name = "GCTransferCareGroup"
        Me.GCTransferCareGroup.OptionsColumn.AllowEdit = False
        Me.GCTransferCareGroup.Visible = True
        Me.GCTransferCareGroup.VisibleIndex = 6
        Me.GCTransferCareGroup.Width = 53
        '
        'GCTransferBed
        '
        Me.GCTransferBed.Caption = "Cama"
        Me.GCTransferBed.FieldName = "Bed"
        Me.GCTransferBed.Name = "GCTransferBed"
        Me.GCTransferBed.OptionsColumn.AllowEdit = False
        Me.GCTransferBed.Visible = True
        Me.GCTransferBed.VisibleIndex = 7
        Me.GCTransferBed.Width = 53
        '
        'GCTransferDiagnosis
        '
        Me.GCTransferDiagnosis.Caption = "Diagnóstico"
        Me.GCTransferDiagnosis.FieldName = "Diagnosis"
        Me.GCTransferDiagnosis.Name = "GCTransferDiagnosis"
        Me.GCTransferDiagnosis.OptionsColumn.AllowEdit = False
        Me.GCTransferDiagnosis.Visible = True
        Me.GCTransferDiagnosis.VisibleIndex = 8
        Me.GCTransferDiagnosis.Width = 53
        '
        'GCTransferFolioQty
        '
        Me.GCTransferFolioQty.Caption = "Folio"
        Me.GCTransferFolioQty.FieldName = "Folio"
        Me.GCTransferFolioQty.Name = "GCTransferFolioQty"
        Me.GCTransferFolioQty.OptionsColumn.AllowEdit = False
        Me.GCTransferFolioQty.Visible = True
        Me.GCTransferFolioQty.VisibleIndex = 9
        Me.GCTransferFolioQty.Width = 53
        '
        'GCTransferAssignedUser
        '
        Me.GCTransferAssignedUser.Caption = "Asignado"
        Me.GCTransferAssignedUser.Name = "GCTransferAssignedUser"
        Me.GCTransferAssignedUser.OptionsColumn.AllowEdit = False
        Me.GCTransferAssignedUser.Visible = True
        Me.GCTransferAssignedUser.VisibleIndex = 10
        Me.GCTransferAssignedUser.Width = 53
        '
        'GCIngressType
        '
        Me.GCIngressType.Caption = "Tipo de ingreso"
        Me.GCIngressType.FieldName = "TypeIncomeName"
        Me.GCIngressType.Name = "GCIngressType"
        Me.GCIngressType.Visible = True
        Me.GCIngressType.VisibleIndex = 11
        '
        'GCTransferCreationUser
        '
        Me.GCTransferCreationUser.Caption = "Usuario de Creación"
        Me.GCTransferCreationUser.FieldName = "UserCreation"
        Me.GCTransferCreationUser.Name = "GCTransferCreationUser"
        Me.GCTransferCreationUser.Width = 53
        '
        'GCTransferModificationUser
        '
        Me.GCTransferModificationUser.Caption = "Usuario de Modificación"
        Me.GCTransferModificationUser.FieldName = "UserModificacion"
        Me.GCTransferModificationUser.Name = "GCTransferModificationUser"
        Me.GCTransferModificationUser.OptionsColumn.AllowEdit = False
        Me.GCTransferModificationUser.Width = 53
        '
        'INDLcgMain
        '
        Me.INDLcgMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgMain.GroupBordersVisible = False
        Me.INDLcgMain.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDTcgAccountManagement})
        Me.INDLcgMain.Name = "Root"
        Me.INDLcgMain.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLcgMain.Size = New System.Drawing.Size(1244, 442)
        Me.INDLcgMain.TextVisible = False
        '
        'INDTcgAccountManagement
        '
        Me.INDTcgAccountManagement.CustomizationFormText = "INDTcgAccountManagement"
        Me.INDTcgAccountManagement.Location = New System.Drawing.Point(0, 0)
        Me.INDTcgAccountManagement.Name = "INDTcgAccountManagement"
        Me.INDTcgAccountManagement.SelectedTabPage = Me.INDLcgTransfers
        Me.INDTcgAccountManagement.Size = New System.Drawing.Size(1244, 442)
        Me.INDTcgAccountManagement.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgTransfers})
        '
        'INDLcgTransfers
        '
        Me.INDLcgTransfers.CustomizationFormText = "Traslados"
        Me.INDLcgTransfers.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTransfers})
        Me.INDLcgTransfers.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgTransfers.Name = "INDLcgTransfers"
        Me.INDLcgTransfers.Size = New System.Drawing.Size(1222, 399)
        Me.INDLcgTransfers.Text = "Pendiente por Asignar"
        '
        'INDLciTransfers
        '
        Me.INDLciTransfers.Control = Me.INDGcPendingAssignment
        Me.INDLciTransfers.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciTransfers.CustomizationFormText = "LayoutControlItem7"
        Me.INDLciTransfers.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTransfers.Name = "INDLciTransfers"
        Me.INDLciTransfers.Size = New System.Drawing.Size(1222, 399)
        Me.INDLciTransfers.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciTransfers.TextVisible = False
        '
        'PanelControl11
        '
        Me.PanelControl11.Controls.Add(Me.INDSleCareCenter)
        Me.PanelControl11.Location = New System.Drawing.Point(22, 20)
        Me.PanelControl11.Name = "PanelControl11"
        Me.PanelControl11.Size = New System.Drawing.Size(1144, 61)
        Me.PanelControl11.TabIndex = 15
        '
        'INDSleCareCenter
        '
        Me.INDSleCareCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDSleCareCenter.EditValue = ""
        Me.INDSleCareCenter.Location = New System.Drawing.Point(2, 2)
        Me.INDSleCareCenter.MaximumSize = New System.Drawing.Size(0, 60)
        Me.INDSleCareCenter.MinimumSize = New System.Drawing.Size(0, 60)
        Me.INDSleCareCenter.Name = "INDSleCareCenter"
        Me.INDSleCareCenter.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleCareCenter.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCareCenter.Properties.Appearance.ForeColor = System.Drawing.Color.Transparent
        Me.INDSleCareCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseBorderColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseFont = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCareCenter.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSleCareCenter.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDSleCareCenter.Properties.DisplayMember = "CodeName"
        Me.INDSleCareCenter.Properties.NullText = "Seleccione un Centro de Atención"
        Me.INDSleCareCenter.Properties.PopupSizeable = False
        Me.INDSleCareCenter.Properties.PopupView = Me.INDGvSearchOperativeUnit
        Me.INDSleCareCenter.Properties.ShowClearButton = False
        Me.INDSleCareCenter.Properties.ShowFooter = False
        Me.INDSleCareCenter.Properties.ValueMember = "CODCENATE"
        Me.INDSleCareCenter.Size = New System.Drawing.Size(1140, 60)
        Me.INDSleCareCenter.StyleController = Me.INDLcMainData
        Me.INDSleCareCenter.TabIndex = 6
        '
        'INDGvSearchOperativeUnit
        '
        Me.INDGvSearchOperativeUnit.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSearchOperativeUnit.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSearchOperativeUnit.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSearchOperativeUnit.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSearchOperativeUnit.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvSearchOperativeUnit.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSearchOperativeUnit.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSearchOperativeUnit.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSearchOperativeUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSearchOperativeUnit.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSearchOperativeUnit.Appearance.Row.Options.UseFont = True
        Me.INDGvSearchOperativeUnit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GCOperativeUnitCode, Me.GCOperativeUnitName})
        Me.INDGvSearchOperativeUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvSearchOperativeUnit.Name = "INDGvSearchOperativeUnit"
        Me.INDGvSearchOperativeUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvSearchOperativeUnit.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSearchOperativeUnit.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSearchOperativeUnit.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSearchOperativeUnit.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSearchOperativeUnit, False)
        '
        'GCOperativeUnitCode
        '
        Me.GCOperativeUnitCode.Caption = "Código"
        Me.GCOperativeUnitCode.FieldName = "CODCENATE"
        Me.GCOperativeUnitCode.Name = "GCOperativeUnitCode"
        Me.GCOperativeUnitCode.Visible = True
        Me.GCOperativeUnitCode.VisibleIndex = 0
        Me.GCOperativeUnitCode.Width = 143
        '
        'GCOperativeUnitName
        '
        Me.GCOperativeUnitName.Caption = "Nombre"
        Me.GCOperativeUnitName.FieldName = "NOMCENATE"
        Me.GCOperativeUnitName.Name = "GCOperativeUnitName"
        Me.GCOperativeUnitName.Visible = True
        Me.GCOperativeUnitName.VisibleIndex = 1
        Me.GCOperativeUnitName.Width = 277
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
        Me.LayoutControlGroup21.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup21.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup21.GroupBordersVisible = False
        Me.LayoutControlGroup21.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDTcgDashBoard, Me.LayoutControlGroup11})
        Me.LayoutControlGroup21.Name = "LayoutControlGroup21"
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
        Me.LayoutControlGroup11.Location = New System.Drawing.Point(0, 49)
        Me.LayoutControlGroup11.Name = "LayoutControlGroup11"
        Me.LayoutControlGroup11.Size = New System.Drawing.Size(1274, 472)
        Me.LayoutControlGroup11.TextVisible = False
        '
        'INDDdbMenuActions
        '
        Me.INDDdbMenuActions.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDDdbMenuActions.DropDownControl = Me.PopupMenu1
        Me.INDDdbMenuActions.ImageOptions.Image = Global.Presentation.AccountManagement.My.Resources.Resources.Mmenu_de_acciones
        Me.INDDdbMenuActions.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDDdbMenuActions.Location = New System.Drawing.Point(1170, 22)
        Me.INDDdbMenuActions.MenuManager = Me.BarManager
        Me.INDDdbMenuActions.Name = "INDDdbMenuActions"
        Me.INDDdbMenuActions.Size = New System.Drawing.Size(92, 59)
        Me.INDDdbMenuActions.StyleController = Me.LayoutControl1
        Me.INDDdbMenuActions.TabIndex = 8
        Me.INDDdbMenuActions.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiRefresh)})
        Me.PopupMenu1.Manager = Me.BarManager
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'Root
        '
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgContainer})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1284, 543)
        Me.Root.TextVisible = False
        '
        'INDLcgContainer
        '
        Me.INDLcgContainer.CustomizationFormText = "LayoutControlGroup1"
        Me.INDLcgContainer.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem6, Me.LayoutControlItem3})
        Me.INDLcgContainer.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgContainer.Name = "INDLcgContainer"
        Me.INDLcgContainer.Size = New System.Drawing.Size(1266, 527)
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
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1148, 65)
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
        Me.LayoutControlItem6.Location = New System.Drawing.Point(1148, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(0, 65)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(50, 65)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 2)
        Me.LayoutControlItem6.Size = New System.Drawing.Size(96, 65)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "LayoutControlItem3"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDLcMain
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 65)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem3.ShowInCustomizationForm = False
        Me.LayoutControlItem3.Size = New System.Drawing.Size(1244, 442)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
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
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRequest, False)
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
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(50, 25)
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
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(50, 25)
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
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'RepositoryItemPopupContainerEdit31
        '
        Me.RepositoryItemPopupContainerEdit31.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit31.Name = "RepositoryItemPopupContainerEdit31"
        '
        'FrmDashboardAccountAssignment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1288, 689)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmDashboardAccountAssignment"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "2853"
        Me.Text = "Dashboard Asignación de Cuentas"
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
        CType(Me.INDPcUserReassignment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcUserReassignment.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDSleUsers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciUsers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemRadioGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcPendingAssignment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPendingAssignment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRiceCheck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgAccountManagement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl11.ResumeLayout(False)
        CType(Me.INDSleCareCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSearchOperativeUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMainData.ResumeLayout(False)
        CType(Me.INDTbAutomaticReload.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgDashBoard, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit31, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl11 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDSleCareCenter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvSearchOperativeUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GCOperativeUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCOperativeUnitName As DevExpress.XtraGrid.Columns.GridColumn
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
    Friend WithEvents INDBiRefresh As DevExpress.XtraBars.BarEditItem
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
    Friend WithEvents INDLcgContainer As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents INDLcMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcPendingAssignment As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvPendingAssignment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemPopupContainerEdit31 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDTcgAccountManagement As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgTransfers As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciTransfers As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GCTransferCheck As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferAdmission As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferPatient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferAdmissionDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferPatientCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferCareGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferBed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferDiagnosis As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferFolioQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferAssignedUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferCreationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCTransferModificationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCIngressType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDPcUserReassignment As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents INDSleUsers As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciUsers As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDRiceCheck As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
