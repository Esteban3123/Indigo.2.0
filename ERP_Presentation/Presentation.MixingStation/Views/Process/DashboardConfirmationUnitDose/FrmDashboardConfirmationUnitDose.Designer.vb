Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardConfirmationUnitDose
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
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule2 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue2 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim GridFormatRule3 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue3 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBarRefresh = New DevExpress.XtraBars.BarButtonItem()
        Me.INDddbOptionsMenu = New DevExpress.XtraEditors.DropDownButton()
        Me.INDPopMenuActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TablePanel1 = New DevExpress.Utils.Layout.TablePanel()
        Me.LayoutControl3 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLblTotal = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLblTotalVisualizations = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLblTotalAdecuations = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSbRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcConfirmUnitDose = New DevExpress.XtraGrid.GridControl()
        Me.INDviewConfirmUnitDose = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColIconStateHis = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptPic = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColPackageName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColRequestDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptSleSafeStatus = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Seguridad = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptIcbHist = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        Me.INDsleCM = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDviewSearchCM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemOptionsMenu = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCareCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDtcgInformation = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDlcgConfirmUnitDose = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemConfirmUnitDose = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpopMenuType = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDBarIntraHospitable = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBarAmbulatory = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.TablePanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TablePanel1.SuspendLayout()
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl3.SuspendLayout()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcConfirmUnitDose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewConfirmUnitDose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptSleSafeStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptIcbHist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewSearchCM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemOptionsMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtcgInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgConfirmUnitDose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemConfirmUnitDose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpopMenuType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 140)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1944, 919)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 10)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(9, 9, 9, 9)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.DockControls.Add(Me.BarDockControl2)
        Me.BarManager1.DockControls.Add(Me.BarDockControl3)
        Me.BarManager1.DockControls.Add(Me.BarDockControl4)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBarRefresh})
        Me.BarManager1.MaxItemId = 8
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 10)
        Me.BarDockControl1.Manager = Me.BarManager1
        Me.BarDockControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BarDockControl1.Size = New System.Drawing.Size(1944, 0)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 1059)
        Me.BarDockControl2.Manager = Me.BarManager1
        Me.BarDockControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BarDockControl2.Size = New System.Drawing.Size(1944, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 10)
        Me.BarDockControl3.Manager = Me.BarManager1
        Me.BarDockControl3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 1049)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1944, 10)
        Me.BarDockControl4.Manager = Me.BarManager1
        Me.BarDockControl4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 1049)
        '
        'INDBarRefresh
        '
        Me.INDBarRefresh.Caption = "Refrescar"
        Me.INDBarRefresh.Id = 5
        Me.INDBarRefresh.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Refresh_16x16_blue
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
        'INDddbOptionsMenu
        '
        Me.INDddbOptionsMenu.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.INDddbOptionsMenu.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDddbOptionsMenu.DropDownControl = Me.INDPopMenuActions
        Me.INDddbOptionsMenu.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Mmenu_de_acciones
        Me.INDddbOptionsMenu.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDddbOptionsMenu.Location = New System.Drawing.Point(1832, 18)
        Me.INDddbOptionsMenu.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDddbOptionsMenu.MenuManager = Me.BarManager1
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDddbOptionsMenu, False)
        Me.INDddbOptionsMenu.Name = "INDddbOptionsMenu"
        Me.INDddbOptionsMenu.Size = New System.Drawing.Size(90, 88)
        Me.INDddbOptionsMenu.StyleController = Me.INDlyRoot
        Me.INDddbOptionsMenu.TabIndex = 9
        '
        'INDPopMenuActions
        '
        Me.INDPopMenuActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarRefresh)})
        Me.INDPopMenuActions.Manager = Me.BarManager1
        Me.INDPopMenuActions.Name = "INDPopMenuActions"
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.Panel1)
        Me.INDlyRoot.Controls.Add(Me.INDSbRefresh)
        Me.INDlyRoot.Controls.Add(Me.INDgcConfirmUnitDose)
        Me.INDlyRoot.Controls.Add(Me.INDsleCM)
        Me.INDlyRoot.Controls.Add(Me.INDddbOptionsMenu)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 12)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1940, 905)
        Me.INDlyRoot.TabIndex = 0
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TablePanel1)
        Me.Panel1.Location = New System.Drawing.Point(35, 820)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1870, 50)
        Me.Panel1.TabIndex = 13
        '
        'TablePanel1
        '
        Me.TablePanel1.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 34.6!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 38.68!), New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 36.72!)})
        Me.TablePanel1.Controls.Add(Me.LayoutControl3)
        Me.TablePanel1.Controls.Add(Me.LayoutControl2)
        Me.TablePanel1.Controls.Add(Me.LayoutControl1)
        Me.TablePanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablePanel1.Location = New System.Drawing.Point(0, 0)
        Me.TablePanel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TablePanel1.Name = "TablePanel1"
        Me.TablePanel1.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 43.0!)})
        Me.TablePanel1.Size = New System.Drawing.Size(1870, 50)
        Me.TablePanel1.TabIndex = 0
        '
        'LayoutControl3
        '
        Me.TablePanel1.SetColumn(Me.LayoutControl3, 2)
        Me.LayoutControl3.Controls.Add(Me.INDLblTotal)
        Me.LayoutControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl3.Location = New System.Drawing.Point(1250, 4)
        Me.LayoutControl3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LayoutControl3.Name = "LayoutControl3"
        Me.LayoutControl3.Root = Me.LayoutControlGroup3
        Me.TablePanel1.SetRow(Me.LayoutControl3, 0)
        Me.LayoutControl3.Size = New System.Drawing.Size(616, 42)
        Me.LayoutControl3.TabIndex = 2
        Me.LayoutControl3.Text = "LayoutControl3"
        '
        'INDLblTotal
        '
        Me.INDLblTotal.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotal.Appearance.Options.UseFont = True
        Me.INDLblTotal.AppearanceDisabled.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotal.AppearanceDisabled.Options.UseFont = True
        Me.INDLblTotal.AppearanceHovered.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotal.AppearanceHovered.Options.UseFont = True
        Me.INDLblTotal.AppearancePressed.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotal.AppearancePressed.Options.UseFont = True
        Me.INDLblTotal.Location = New System.Drawing.Point(278, 3)
        Me.INDLblTotal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDLblTotal.Name = "INDLblTotal"
        Me.INDLblTotal.Size = New System.Drawing.Size(10, 28)
        Me.INDLblTotal.StyleController = Me.LayoutControl3
        Me.INDLblTotal.TabIndex = 4
        Me.INDLblTotal.Text = "0"
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
        Me.LayoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup3.GroupBordersVisible = False
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5})
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(616, 42)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem5.Control = Me.INDLblTotal
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(616, 42)
        Me.LayoutControlItem5.Text = "Adecuaciones seleccionadas:"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(271, 28)
        '
        'LayoutControl2
        '
        Me.TablePanel1.SetColumn(Me.LayoutControl2, 1)
        Me.LayoutControl2.Controls.Add(Me.INDLblTotalVisualizations)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(592, 4)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.TablePanel1.SetRow(Me.LayoutControl2, 0)
        Me.LayoutControl2.Size = New System.Drawing.Size(650, 42)
        Me.LayoutControl2.TabIndex = 1
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDLblTotalVisualizations
        '
        Me.INDLblTotalVisualizations.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLblTotalVisualizations.Appearance.Options.UseFont = True
        Me.INDLblTotalVisualizations.AppearanceDisabled.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotalVisualizations.AppearanceDisabled.Options.UseFont = True
        Me.INDLblTotalVisualizations.AppearanceHovered.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotalVisualizations.AppearanceHovered.Options.UseFont = True
        Me.INDLblTotalVisualizations.AppearancePressed.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotalVisualizations.AppearancePressed.Options.UseFont = True
        Me.INDLblTotalVisualizations.Location = New System.Drawing.Point(474, 3)
        Me.INDLblTotalVisualizations.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDLblTotalVisualizations.Name = "INDLblTotalVisualizations"
        Me.INDLblTotalVisualizations.Size = New System.Drawing.Size(10, 28)
        Me.INDLblTotalVisualizations.StyleController = Me.LayoutControl2
        Me.INDLblTotalVisualizations.TabIndex = 4
        Me.INDLblTotalVisualizations.Text = "0"
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
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(650, 42)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem4.Control = Me.INDLblTotalVisualizations
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(650, 42)
        Me.LayoutControlItem4.Text = "Adecuaciones visualizadas (con filtros aplicados):"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(467, 28)
        '
        'LayoutControl1
        '
        Me.TablePanel1.SetColumn(Me.LayoutControl1, 0)
        Me.LayoutControl1.Controls.Add(Me.INDLblTotalAdecuations)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(4, 4)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.TablePanel1.SetRow(Me.LayoutControl1, 0)
        Me.LayoutControl1.Size = New System.Drawing.Size(580, 42)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLblTotalAdecuations
        '
        Me.INDLblTotalAdecuations.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotalAdecuations.Appearance.Options.UseFont = True
        Me.INDLblTotalAdecuations.AppearanceDisabled.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotalAdecuations.AppearanceDisabled.Options.UseFont = True
        Me.INDLblTotalAdecuations.AppearanceHovered.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotalAdecuations.AppearanceHovered.Options.UseFont = True
        Me.INDLblTotalAdecuations.AppearancePressed.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDLblTotalAdecuations.AppearancePressed.Options.UseFont = True
        Me.INDLblTotalAdecuations.Location = New System.Drawing.Point(223, 3)
        Me.INDLblTotalAdecuations.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDLblTotalAdecuations.Name = "INDLblTotalAdecuations"
        Me.INDLblTotalAdecuations.Size = New System.Drawing.Size(10, 28)
        Me.INDLblTotalAdecuations.StyleController = Me.LayoutControl1
        Me.INDLblTotalAdecuations.TabIndex = 4
        Me.INDLblTotalAdecuations.Text = "0"
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(580, 42)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem3.Control = Me.INDLblTotalAdecuations
        Me.LayoutControlItem3.CustomizationFormText = "Total de adecuaciones:"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(580, 42)
        Me.LayoutControlItem3.Text = "Total de adecuaciones:"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(216, 28)
        '
        'INDSbRefresh
        '
        Me.INDSbRefresh.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Refresh_24x24_blue
        Me.INDSbRefresh.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDSbRefresh.Location = New System.Drawing.Point(1736, 18)
        Me.INDSbRefresh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbRefresh, False)
        Me.INDSbRefresh.Name = "INDSbRefresh"
        Me.INDSbRefresh.Size = New System.Drawing.Size(90, 88)
        Me.INDSbRefresh.StyleController = Me.INDlyRoot
        Me.INDSbRefresh.TabIndex = 12
        '
        'INDgcConfirmUnitDose
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcConfirmUnitDose, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcConfirmUnitDose, Nothing)
        Me.INDgcConfirmUnitDose.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcConfirmUnitDose, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcConfirmUnitDose, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcConfirmUnitDose, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcConfirmUnitDose, False)
        Me.INDgcConfirmUnitDose.Location = New System.Drawing.Point(35, 175)
        Me.INDgcConfirmUnitDose.MainView = Me.INDviewConfirmUnitDose
        Me.INDgcConfirmUnitDose.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDgcConfirmUnitDose.MenuManager = Me.BarManager1
        Me.INDgcConfirmUnitDose.Name = "INDgcConfirmUnitDose"
        Me.INDgcConfirmUnitDose.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptIcbHist, Me.INDRptPic, Me.INDRptSleSafeStatus})
        Me.INDgcConfirmUnitDose.Size = New System.Drawing.Size(1870, 639)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcConfirmUnitDose, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcConfirmUnitDose.TabIndex = 11
        Me.INDgcConfirmUnitDose.ToolTipController = Me.ToolTipController1
        Me.INDgcConfirmUnitDose.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewConfirmUnitDose})
        '
        'INDviewConfirmUnitDose
        '
        Me.INDviewConfirmUnitDose.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewConfirmUnitDose.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewConfirmUnitDose.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewConfirmUnitDose.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewConfirmUnitDose.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewConfirmUnitDose.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewConfirmUnitDose.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewConfirmUnitDose.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewConfirmUnitDose.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewConfirmUnitDose.Appearance.Row.Options.UseFont = True
        Me.INDviewConfirmUnitDose.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewConfirmUnitDose.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewConfirmUnitDose.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColIconStateHis, Me.GridColumn10, Me.GridColumn12, Me.GridColumn1, Me.GridColumn2, Me.GridColumn9, Me.INDColPackageName, Me.GridColumn7, Me.GridColumn8, Me.GridColumn14, Me.GridColumn13, Me.GridColumn15, Me.INDColRequestDate, Me.GridColumn5})
        Me.INDviewConfirmUnitDose.DetailHeight = 512
        Me.INDviewConfirmUnitDose.FixedLineWidth = 3
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.Red
        FormatConditionRuleValue1.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue1.Appearance.ForeColor = System.Drawing.Color.Red
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue1.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue1.Expression = "[ColorRequest] = 3"
        FormatConditionRuleValue1.Value1 = 3
        GridFormatRule1.Rule = FormatConditionRuleValue1
        GridFormatRule2.Name = "Format1"
        FormatConditionRuleValue2.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        FormatConditionRuleValue2.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue2.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        FormatConditionRuleValue2.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue2.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue2.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue2.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue2.Expression = "[ColorRequest] = 2"
        FormatConditionRuleValue2.Value1 = 2
        GridFormatRule2.Rule = FormatConditionRuleValue2
        GridFormatRule3.Name = "Format2"
        FormatConditionRuleValue3.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        FormatConditionRuleValue3.Appearance.BackColor2 = System.Drawing.Color.White
        FormatConditionRuleValue3.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        FormatConditionRuleValue3.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue3.Appearance.Options.UseForeColor = True
        FormatConditionRuleValue3.Appearance.Options.UseTextOptions = True
        FormatConditionRuleValue3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        FormatConditionRuleValue3.Condition = DevExpress.XtraEditors.FormatCondition.Equal
        FormatConditionRuleValue3.Expression = "[ColorRequest] = 1"
        FormatConditionRuleValue3.Value1 = 1
        GridFormatRule3.Rule = FormatConditionRuleValue3
        Me.INDviewConfirmUnitDose.FormatRules.Add(GridFormatRule1)
        Me.INDviewConfirmUnitDose.FormatRules.Add(GridFormatRule2)
        Me.INDviewConfirmUnitDose.FormatRules.Add(GridFormatRule3)
        Me.INDviewConfirmUnitDose.GridControl = Me.INDgcConfirmUnitDose
        Me.INDviewConfirmUnitDose.GroupCount = 1
        Me.INDviewConfirmUnitDose.Name = "INDviewConfirmUnitDose"
        Me.INDviewConfirmUnitDose.OptionsBehavior.AutoExpandAllGroups = True
        Me.INDviewConfirmUnitDose.OptionsSelection.CheckBoxSelectorColumnWidth = 45
        Me.INDviewConfirmUnitDose.OptionsSelection.MultiSelect = True
        Me.INDviewConfirmUnitDose.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect
        Me.INDviewConfirmUnitDose.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        Me.INDviewConfirmUnitDose.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewConfirmUnitDose.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewConfirmUnitDose.OptionsView.ShowAutoFilterRow = True
        Me.INDviewConfirmUnitDose.OptionsView.ShowDetailButtons = False
        Me.INDviewConfirmUnitDose.OptionsView.ShowGroupPanel = False
        Me.INDviewConfirmUnitDose.OptionsView.ShowIndicator = False
        Me.INDviewConfirmUnitDose.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn10, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewConfirmUnitDose, False)
        '
        'ColIconStateHis
        '
        Me.ColIconStateHis.Caption = " "
        Me.ColIconStateHis.ColumnEdit = Me.INDRptPic
        Me.ColIconStateHis.FieldName = "GridColumn16"
        Me.ColIconStateHis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.ColIconStateHis.MinWidth = 30
        Me.ColIconStateHis.Name = "ColIconStateHis"
        Me.ColIconStateHis.OptionsColumn.AllowEdit = False
        Me.ColIconStateHis.OptionsColumn.AllowFocus = False
        Me.ColIconStateHis.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColIconStateHis.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColIconStateHis.OptionsColumn.AllowMove = False
        Me.ColIconStateHis.OptionsColumn.AllowSize = False
        Me.ColIconStateHis.OptionsColumn.FixedWidth = True
        Me.ColIconStateHis.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.ColIconStateHis.Visible = True
        Me.ColIconStateHis.VisibleIndex = 1
        Me.ColIconStateHis.Width = 46
        '
        'INDRptPic
        '
        Me.INDRptPic.Name = "INDRptPic"
        Me.INDRptPic.NullText = " "
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Línea producción"
        Me.GridColumn10.FieldName = "ProductionLineCodeName"
        Me.GridColumn10.MinWidth = 30
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowFocus = False
        Me.GridColumn10.OptionsColumn.FixedWidth = True
        Me.GridColumn10.OptionsColumn.ShowCaption = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 7
        Me.GridColumn10.Width = 112
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Paciente"
        Me.GridColumn12.FieldName = "PatientCodeName"
        Me.GridColumn12.MinWidth = 30
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.AllowEdit = False
        Me.GridColumn12.OptionsColumn.AllowFocus = False
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 3
        Me.GridColumn12.Width = 87
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Medicamento/adecuaciones solicitadas"
        Me.GridColumn1.FieldName = "ServiceDescription"
        Me.GridColumn1.MinWidth = 30
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 477
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Centro atención"
        Me.GridColumn2.FieldName = "CareCenterDescription"
        Me.GridColumn2.MinWidth = 30
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 4
        Me.GridColumn2.Width = 291
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Total preparación"
        Me.GridColumn9.FieldName = "DosageMeasurementUnitName"
        Me.GridColumn9.MinWidth = 30
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowFocus = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 5
        Me.GridColumn9.Width = 87
        '
        'INDColPackageName
        '
        Me.INDColPackageName.Caption = "Paquete asignado"
        Me.INDColPackageName.FieldName = "INDColPackageName"
        Me.INDColPackageName.MinWidth = 30
        Me.INDColPackageName.Name = "INDColPackageName"
        Me.INDColPackageName.OptionsColumn.AllowEdit = False
        Me.INDColPackageName.OptionsColumn.AllowFocus = False
        Me.INDColPackageName.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDColPackageName.Visible = True
        Me.INDColPackageName.VisibleIndex = 6
        Me.INDColPackageName.Width = 184
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tipo dosis unitaria"
        Me.GridColumn7.FieldName = "UnitDoseTypeDescription"
        Me.GridColumn7.MinWidth = 30
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 7
        Me.GridColumn7.Width = 184
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Origen"
        Me.GridColumn8.FieldName = "SourceName"
        Me.GridColumn8.MinWidth = 30
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowFocus = False
        Me.GridColumn8.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, DevExpress.Data.SummaryMode.Selection, "SourceName", "{0}")})
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 8
        Me.GridColumn8.Width = 168
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Cama"
        Me.GridColumn14.FieldName = "Bed"
        Me.GridColumn14.MinWidth = 30
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.OptionsColumn.AllowFocus = False
        Me.GridColumn14.Width = 112
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Unidad funcional"
        Me.GridColumn13.FieldName = "FuncionalUnitCodeName"
        Me.GridColumn13.MinWidth = 30
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.OptionsColumn.AllowFocus = False
        Me.GridColumn13.Width = 112
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Vía administración"
        Me.GridColumn15.FieldName = "AdministrationRoute"
        Me.GridColumn15.MinWidth = 30
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowFocus = False
        Me.GridColumn15.Width = 112
        '
        'INDColRequestDate
        '
        Me.INDColRequestDate.Caption = "Fecha solicitud"
        Me.INDColRequestDate.FieldName = "RequestDate"
        Me.INDColRequestDate.MinWidth = 30
        Me.INDColRequestDate.Name = "INDColRequestDate"
        Me.INDColRequestDate.OptionsColumn.AllowEdit = False
        Me.INDColRequestDate.OptionsColumn.AllowFocus = False
        Me.INDColRequestDate.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "RequestDate", "{0}")})
        Me.INDColRequestDate.Visible = True
        Me.INDColRequestDate.VisibleIndex = 9
        Me.INDColRequestDate.Width = 201
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Seguridad"
        Me.GridColumn5.ColumnEdit = Me.INDRptSleSafeStatus
        Me.GridColumn5.FieldName = "SafeStatus"
        Me.GridColumn5.MinWidth = 30
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 10
        Me.GridColumn5.Width = 112
        '
        'INDRptSleSafeStatus
        '
        Me.INDRptSleSafeStatus.AutoHeight = False
        Me.INDRptSleSafeStatus.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptSleSafeStatus.DisplayMember = "Item2"
        Me.INDRptSleSafeStatus.Name = "INDRptSleSafeStatus"
        Me.INDRptSleSafeStatus.PopupView = Me.RepositoryItemSearchLookUpEdit1View
        Me.INDRptSleSafeStatus.ValueMember = "Item1"
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemSearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemSearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Seguridad})
        Me.RepositoryItemSearchLookUpEdit1View.DetailHeight = 512
        Me.RepositoryItemSearchLookUpEdit1View.FixedLineWidth = 3
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemSearchLookUpEdit1View, False)
        '
        'Seguridad
        '
        Me.Seguridad.Caption = "Seguridad"
        Me.Seguridad.FieldName = "Item2"
        Me.Seguridad.MinWidth = 30
        Me.Seguridad.Name = "Seguridad"
        Me.Seguridad.Visible = True
        Me.Seguridad.VisibleIndex = 0
        Me.Seguridad.Width = 112
        '
        'INDRptIcbHist
        '
        Me.INDRptIcbHist.AutoHeight = False
        EditorButtonImageOptions1.Image = Global.Presentation.MixingStation.My.Resources.Resources.Alerta
        Me.INDRptIcbHist.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDRptIcbHist.Name = "INDRptIcbHist"
        '
        'ToolTipController1
        '
        '
        'INDsleCM
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCM, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCM, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCM, False)
        Me.INDsleCM.Location = New System.Drawing.Point(18, 18)
        Me.INDsleCM.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.INDsleCM.MaximumSize = New System.Drawing.Size(0, 88)
        Me.INDsleCM.MinimumSize = New System.Drawing.Size(0, 88)
        Me.INDsleCM.Name = "INDsleCM"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDsleCM, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCM, False)
        Me.INDsleCM.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCM.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleCM.Properties.Appearance.Options.UseFont = True
        Me.INDsleCM.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleCM.Properties.Appearance.Options.UseTextOptions = True
        Me.INDsleCM.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDsleCM.Properties.DisplayMember = "CodeName"
        Me.INDsleCM.Properties.NullText = "Seleccione una central de mezclas"
        Me.INDsleCM.Properties.PopupSizeable = False
        Me.INDsleCM.Properties.PopupView = Me.INDviewSearchCM
        Me.INDsleCM.Properties.ShowClearButton = False
        Me.INDsleCM.Properties.ShowFooter = False
        Me.INDsleCM.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCM, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCM, True)
        Me.INDsleCM.Size = New System.Drawing.Size(1712, 88)
        Me.INDsleCM.StyleController = Me.INDlyRoot
        Me.INDsleCM.TabIndex = 10
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCM, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCM, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCM, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCM, False)
        '
        'INDviewSearchCM
        '
        Me.INDviewSearchCM.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewSearchCM.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewSearchCM.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewSearchCM.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewSearchCM.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDviewSearchCM.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchCM.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewSearchCM.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewSearchCM.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewSearchCM.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewSearchCM.Appearance.Row.Options.UseFont = True
        Me.INDviewSearchCM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.INDviewSearchCM.DetailHeight = 512
        Me.INDviewSearchCM.FixedLineWidth = 3
        Me.INDviewSearchCM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDviewSearchCM.Name = "INDviewSearchCM"
        Me.INDviewSearchCM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDviewSearchCM.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewSearchCM.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewSearchCM.OptionsView.ShowAutoFilterRow = True
        Me.INDviewSearchCM.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewSearchCM, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.MinWidth = 30
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 529
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.MinWidth = 30
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 1543
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemOptionsMenu, Me.INDlyItemCareCenter, Me.INDtcgInformation, Me.LayoutControlItem1})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(1940, 905)
        Me.Root.TextVisible = False
        '
        'INDlyItemOptionsMenu
        '
        Me.INDlyItemOptionsMenu.Control = Me.INDddbOptionsMenu
        Me.INDlyItemOptionsMenu.Location = New System.Drawing.Point(1814, 0)
        Me.INDlyItemOptionsMenu.MaxSize = New System.Drawing.Size(96, 94)
        Me.INDlyItemOptionsMenu.MinSize = New System.Drawing.Size(96, 94)
        Me.INDlyItemOptionsMenu.Name = "INDlyItemOptionsMenu"
        Me.INDlyItemOptionsMenu.Size = New System.Drawing.Size(96, 94)
        Me.INDlyItemOptionsMenu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemOptionsMenu.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemOptionsMenu.TextVisible = False
        Me.INDlyItemOptionsMenu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemCareCenter
        '
        Me.INDlyItemCareCenter.Control = Me.INDsleCM
        Me.INDlyItemCareCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCareCenter.MaxSize = New System.Drawing.Size(0, 94)
        Me.INDlyItemCareCenter.MinSize = New System.Drawing.Size(1, 94)
        Me.INDlyItemCareCenter.Name = "INDlyItemCareCenter"
        Me.INDlyItemCareCenter.Size = New System.Drawing.Size(1718, 94)
        Me.INDlyItemCareCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCareCenter.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemCareCenter.TextVisible = False
        '
        'INDtcgInformation
        '
        Me.INDtcgInformation.Location = New System.Drawing.Point(0, 94)
        Me.INDtcgInformation.Name = "INDtcgInformation"
        Me.INDtcgInformation.SelectedTabPage = Me.INDlcgConfirmUnitDose
        Me.INDtcgInformation.Size = New System.Drawing.Size(1910, 781)
        Me.INDtcgInformation.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgConfirmUnitDose})
        '
        'INDlcgConfirmUnitDose
        '
        Me.INDlcgConfirmUnitDose.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgConfirmUnitDose.AppearanceGroup.Options.UseFont = True
        Me.INDlcgConfirmUnitDose.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgConfirmUnitDose.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgConfirmUnitDose.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgConfirmUnitDose, False)
        Me.INDlcgConfirmUnitDose.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemConfirmUnitDose, Me.LayoutControlItem2})
        Me.INDlcgConfirmUnitDose.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgConfirmUnitDose.Name = "INDlcgConfirmUnitDose"
        Me.INDlcgConfirmUnitDose.Size = New System.Drawing.Size(1876, 701)
        Me.INDlcgConfirmUnitDose.Text = "Confirmación dosis unitarias"
        '
        'INDlyItemConfirmUnitDose
        '
        Me.INDlyItemConfirmUnitDose.Control = Me.INDgcConfirmUnitDose
        Me.INDlyItemConfirmUnitDose.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemConfirmUnitDose.Name = "INDlyItemConfirmUnitDose"
        Me.INDlyItemConfirmUnitDose.Size = New System.Drawing.Size(1876, 645)
        Me.INDlyItemConfirmUnitDose.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemConfirmUnitDose.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.Panel1
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 645)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 56)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(156, 56)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1876, 56)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSbRefresh
        Me.LayoutControlItem1.Location = New System.Drawing.Point(1718, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(96, 94)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(96, 94)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(96, 94)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDpopMenuType
        '
        Me.INDpopMenuType.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarIntraHospitable), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBarAmbulatory)})
        Me.INDpopMenuType.Manager = Me.BarManager2
        Me.INDpopMenuType.Name = "INDpopMenuType"
        '
        'INDBarIntraHospitable
        '
        Me.INDBarIntraHospitable.Caption = "Intrahospitalario"
        Me.INDBarIntraHospitable.Id = 8
        Me.INDBarIntraHospitable.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Add_24x24_blue
        Me.INDBarIntraHospitable.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarIntraHospitable.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBarIntraHospitable.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarIntraHospitable.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBarIntraHospitable.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarIntraHospitable.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBarIntraHospitable.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarIntraHospitable.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBarIntraHospitable.Name = "INDBarIntraHospitable"
        '
        'INDBarAmbulatory
        '
        Me.INDBarAmbulatory.Caption = "Ambulatorio"
        Me.INDBarAmbulatory.Id = 9
        Me.INDBarAmbulatory.ImageOptions.Image = Global.Presentation.MixingStation.My.Resources.Resources.Add_24x24_blue
        Me.INDBarAmbulatory.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarAmbulatory.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBarAmbulatory.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarAmbulatory.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBarAmbulatory.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarAmbulatory.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBarAmbulatory.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBarAmbulatory.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBarAmbulatory.Name = "INDBarAmbulatory"
        '
        'BarManager2
        '
        Me.BarManager2.DockControls.Add(Me.BarDockControl5)
        Me.BarManager2.DockControls.Add(Me.BarDockControl6)
        Me.BarManager2.DockControls.Add(Me.BarDockControl7)
        Me.BarManager2.DockControls.Add(Me.BarDockControl8)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBarIntraHospitable, Me.INDBarAmbulatory})
        Me.BarManager2.MaxItemId = 10
        '
        'BarDockControl5
        '
        Me.BarDockControl5.CausesValidation = False
        Me.BarDockControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl5.Location = New System.Drawing.Point(0, 10)
        Me.BarDockControl5.Manager = Me.BarManager2
        Me.BarDockControl5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BarDockControl5.Size = New System.Drawing.Size(1944, 0)
        '
        'BarDockControl6
        '
        Me.BarDockControl6.CausesValidation = False
        Me.BarDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl6.Location = New System.Drawing.Point(0, 1059)
        Me.BarDockControl6.Manager = Me.BarManager2
        Me.BarDockControl6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BarDockControl6.Size = New System.Drawing.Size(1944, 0)
        '
        'BarDockControl7
        '
        Me.BarDockControl7.CausesValidation = False
        Me.BarDockControl7.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl7.Location = New System.Drawing.Point(0, 10)
        Me.BarDockControl7.Manager = Me.BarManager2
        Me.BarDockControl7.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BarDockControl7.Size = New System.Drawing.Size(0, 1049)
        '
        'BarDockControl8
        '
        Me.BarDockControl8.CausesValidation = False
        Me.BarDockControl8.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl8.Location = New System.Drawing.Point(1944, 10)
        Me.BarDockControl8.Manager = Me.BarManager2
        Me.BarDockControl8.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BarDockControl8.Size = New System.Drawing.Size(0, 1049)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'FrmDashboardConfirmationUnitDose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1944, 1059)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.BarDockControl7)
        Me.Controls.Add(Me.BarDockControl8)
        Me.Controls.Add(Me.BarDockControl6)
        Me.Controls.Add(Me.BarDockControl5)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDashboardConfirmationUnitDose"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Tag = "2194"
        Me.Text = "Dashboard confirmación dosis unitarias"
        Me.Controls.SetChildIndex(Me.BarDockControl5, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl6, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl8, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl7, 0)
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
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.TablePanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TablePanel1.ResumeLayout(False)
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl3.ResumeLayout(False)
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcConfirmUnitDose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewConfirmUnitDose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptSleSafeStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptIcbHist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewSearchCM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemOptionsMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtcgInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgConfirmUnitDose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemConfirmUnitDose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpopMenuType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDddbOptionsMenu As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents INDlyItemOptionsMenu As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBarRefresh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDsleCM As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDviewSearchCM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemCareCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcConfirmUnitDose As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewConfirmUnitDose As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDpopMenuType As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBarIntraHospitable As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBarAmbulatory As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDtcgInformation As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDlcgConfirmUnitDose As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemConfirmUnitDose As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColPackageName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSbRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColIconStateHis As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptIcbHist As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDRptPic As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents INDColRequestDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TablePanel1 As DevExpress.Utils.Layout.TablePanel
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLblTotal As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLblTotalVisualizations As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLblTotalAdecuations As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptSleSafeStatus As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Seguridad As DevExpress.XtraGrid.Columns.GridColumn
End Class
