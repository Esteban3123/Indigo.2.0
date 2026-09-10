Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFixedAssetDepreciation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFixedAssetDepreciation))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyDepreciation = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpopupDetailCost = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyDetailCost = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcDetailCost = New DevExpress.XtraGrid.GridControl()
        Me.INDviewDetailCost = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDetailCost = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrDateNavigator1 = New Presentation.Controls.CtrDateNavigator()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgPeriod = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDepreciation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygAmortization = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDPopupAmortizationDetailCost = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyAmortizationDetailCost = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgcAmortizationDetailCost = New DevExpress.XtraGrid.GridControl()
        Me.INDviewAmortizationDetailCost = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColMainAccount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColThirdParty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColLocation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCostCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAmortizedDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAmortizedValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemAmortizationDetailCost = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyDepreciation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyDepreciation.SuspendLayout()
        CType(Me.INDpopupDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupDetailCost.SuspendLayout()
        CType(Me.INDlyDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyDetailCost.SuspendLayout()
        CType(Me.INDgcDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDepreciation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAmortization, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupAmortizationDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPopupAmortizationDetailCost.SuspendLayout()
        CType(Me.INDlyAmortizationDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyAmortizationDetailCost.SuspendLayout()
        CType(Me.INDgcAmortizationDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDviewAmortizationDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAmortizationDetailCost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyDepreciation)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(2011, 566)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(2011, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(2011, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.Appearance.BackColor = System.Drawing.Color.White
        Me.CtrNavigationControlPanel1.Appearance.Image = CType(resources.GetObject("CtrNavigationControlPanel1.Appearance.Image"), System.Drawing.Image)
        Me.CtrNavigationControlPanel1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControlPanel1.Appearance.Options.UseImage = True
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyDepreciation
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 557)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyDepreciation
        '
        Me.INDlyDepreciation.Controls.Add(Me.INDPopupAmortizationDetailCost)
        Me.INDlyDepreciation.Controls.Add(Me.INDpopupDetailCost)
        Me.INDlyDepreciation.Controls.Add(Me.CtrDateNavigator1)
        Me.INDlyDepreciation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyDepreciation.Location = New System.Drawing.Point(202, 7)
        Me.INDlyDepreciation.Name = "INDlyDepreciation"
        Me.INDlyDepreciation.Root = Me.LayoutControlGroup1
        Me.INDlyDepreciation.Size = New System.Drawing.Size(1807, 557)
        Me.INDlyDepreciation.TabIndex = 1
        Me.INDlyDepreciation.Text = "LayoutControl1"
        '
        'INDpopupDetailCost
        '
        Me.INDpopupDetailCost.Controls.Add(Me.INDlyDetailCost)
        Me.INDpopupDetailCost.Location = New System.Drawing.Point(63, 285)
        Me.INDpopupDetailCost.Name = "INDpopupDetailCost"
        Me.INDpopupDetailCost.Size = New System.Drawing.Size(874, 256)
        Me.INDpopupDetailCost.TabIndex = 5
        '
        'INDlyDetailCost
        '
        Me.INDlyDetailCost.Controls.Add(Me.INDgcDetailCost)
        Me.INDlyDetailCost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyDetailCost.Location = New System.Drawing.Point(0, 0)
        Me.INDlyDetailCost.Name = "INDlyDetailCost"
        Me.INDlyDetailCost.Root = Me.LayoutControlGroup2
        Me.INDlyDetailCost.Size = New System.Drawing.Size(874, 256)
        Me.INDlyDetailCost.TabIndex = 0
        Me.INDlyDetailCost.Text = "LayoutControl1"
        '
        'INDgcDetailCost
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcDetailCost, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcDetailCost, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcDetailCost, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcDetailCost, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcDetailCost, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcDetailCost, False)
        Me.INDgcDetailCost.Location = New System.Drawing.Point(12, 12)
        Me.INDgcDetailCost.MainView = Me.INDviewDetailCost
        Me.INDgcDetailCost.Name = "INDgcDetailCost"
        Me.INDgcDetailCost.Size = New System.Drawing.Size(850, 232)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcDetailCost, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcDetailCost.TabIndex = 4
        Me.INDgcDetailCost.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewDetailCost})
        '
        'INDviewDetailCost
        '
        Me.INDviewDetailCost.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewDetailCost.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewDetailCost.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewDetailCost.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewDetailCost.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetailCost.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewDetailCost.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewDetailCost.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewDetailCost.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewDetailCost.Appearance.Row.Options.UseFont = True
        Me.INDviewDetailCost.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewDetailCost.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewDetailCost.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.INDviewDetailCost.GridControl = Me.INDgcDetailCost
        Me.INDviewDetailCost.Name = "INDviewDetailCost"
        Me.INDviewDetailCost.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewDetailCost.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewDetailCost.OptionsView.ShowAutoFilterRow = True
        Me.INDviewDetailCost.OptionsView.ShowDetailButtons = False
        Me.INDviewDetailCost.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewDetailCost, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Cuenta"
        Me.GridColumn1.FieldName = "MainAccountId.NumberName"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Responsable"
        Me.GridColumn2.FieldName = "ResponsibleId.CodeNitName"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Localización"
        Me.GridColumn3.FieldName = "LocationId.CodeName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Centro Costo"
        Me.GridColumn4.FieldName = "CostCenterId.CodeName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Días Depreciados"
        Me.GridColumn5.FieldName = "DepreciatedDays"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Valor Depreciado"
        Me.GridColumn6.DisplayFormat.FormatString = "c2"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "DepreciationValue"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.AllowFocus = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDetailCost})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(874, 256)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemDetailCost
        '
        Me.INDlyItemDetailCost.Control = Me.INDgcDetailCost
        Me.INDlyItemDetailCost.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemDetailCost.Name = "INDlyItemDetailCost"
        Me.INDlyItemDetailCost.Size = New System.Drawing.Size(854, 236)
        Me.INDlyItemDetailCost.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemDetailCost.TextVisible = False
        '
        'CtrDateNavigator1
        '
        Me.CtrDateNavigator1.CtrCalendar = Nothing
        Me.CtrDateNavigator1.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.Both
        Me.CtrDateNavigator1.Location = New System.Drawing.Point(24, 53)
        Me.CtrDateNavigator1.Name = "CtrDateNavigator1"
        Me.CtrDateNavigator1.Size = New System.Drawing.Size(460, 65)
        Me.CtrDateNavigator1.TabIndex = 0
        Me.CtrDateNavigator1.WithEvent = True
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgPeriod, Me.INDlygDepreciation, Me.INDlygAmortization})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1807, 557)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgPeriod
        '
        Me.INDLcgPeriod.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPeriod.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPeriod.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPeriod.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPeriod.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPeriod.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPeriod.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPeriod.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPeriod.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPeriod.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPeriod.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPeriod.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPeriod.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPeriod.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPeriod, False)
        Me.INDLcgPeriod.CustomizationFormText = "Depreciación"
        Me.INDLcgPeriod.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.INDLcgPeriod.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgPeriod.Name = "INDLcgPeriod"
        Me.INDLcgPeriod.Size = New System.Drawing.Size(488, 537)
        Me.INDLcgPeriod.Text = "Periodo a Depreciar / Amortizar"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.CtrDateNavigator1
        Me.LayoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(464, 69)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(464, 69)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(464, 484)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDlygDepreciation
        '
        Me.INDlygDepreciation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDepreciation.AppearanceGroup.Options.UseFont = True
        Me.INDlygDepreciation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDepreciation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDepreciation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDepreciation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDepreciation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDepreciation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDepreciation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDepreciation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDepreciation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDepreciation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDepreciation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDepreciation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDepreciation, False)
        Me.INDlygDepreciation.Location = New System.Drawing.Point(488, 0)
        Me.INDlygDepreciation.Name = "INDlygDepreciation"
        Me.INDlygDepreciation.Size = New System.Drawing.Size(975, 537)
        Me.INDlygDepreciation.Text = "Depreciación"
        '
        'INDlygAmortization
        '
        Me.INDlygAmortization.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAmortization.AppearanceGroup.Options.UseFont = True
        Me.INDlygAmortization.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAmortization.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygAmortization.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAmortization.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygAmortization.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygAmortization.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygAmortization.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAmortization.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygAmortization.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAmortization.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygAmortization.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAmortization.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygAmortization, False)
        Me.INDlygAmortization.CustomizationFormText = "Amortización"
        Me.INDlygAmortization.Location = New System.Drawing.Point(1463, 0)
        Me.INDlygAmortization.Name = "INDlygAmortization"
        Me.INDlygAmortization.Size = New System.Drawing.Size(324, 537)
        Me.INDlygAmortization.Text = "Amortización"
        '
        'IndigoGridControl1
        '
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'INDPopupAmortizationDetailCost
        '
        Me.INDPopupAmortizationDetailCost.Controls.Add(Me.INDlyAmortizationDetailCost)
        Me.INDPopupAmortizationDetailCost.Location = New System.Drawing.Point(68, 15)
        Me.INDPopupAmortizationDetailCost.Name = "INDPopupAmortizationDetailCost"
        Me.INDPopupAmortizationDetailCost.Size = New System.Drawing.Size(874, 256)
        Me.INDPopupAmortizationDetailCost.TabIndex = 6
        '
        'INDlyAmortizationDetailCost
        '
        Me.INDlyAmortizationDetailCost.Controls.Add(Me.INDgcAmortizationDetailCost)
        Me.INDlyAmortizationDetailCost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyAmortizationDetailCost.Location = New System.Drawing.Point(0, 0)
        Me.INDlyAmortizationDetailCost.Name = "INDlyAmortizationDetailCost"
        Me.INDlyAmortizationDetailCost.Root = Me.LayoutControlGroup3
        Me.INDlyAmortizationDetailCost.Size = New System.Drawing.Size(874, 256)
        Me.INDlyAmortizationDetailCost.TabIndex = 0
        Me.INDlyAmortizationDetailCost.Text = "LayoutControl1"
        '
        'INDgcAmortizationDetailCost
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcAmortizationDetailCost, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcAmortizationDetailCost, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcAmortizationDetailCost, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcAmortizationDetailCost, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcAmortizationDetailCost, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcAmortizationDetailCost, False)
        Me.INDgcAmortizationDetailCost.Location = New System.Drawing.Point(12, 12)
        Me.INDgcAmortizationDetailCost.MainView = Me.INDviewAmortizationDetailCost
        Me.INDgcAmortizationDetailCost.Name = "INDgcAmortizationDetailCost"
        Me.INDgcAmortizationDetailCost.Size = New System.Drawing.Size(850, 232)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcAmortizationDetailCost, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcAmortizationDetailCost.TabIndex = 0
        Me.INDgcAmortizationDetailCost.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDviewAmortizationDetailCost})
        '
        'INDviewAmortizationDetailCost
        '
        Me.INDviewAmortizationDetailCost.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDviewAmortizationDetailCost.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDviewAmortizationDetailCost.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDviewAmortizationDetailCost.Appearance.FocusedRow.Options.UseFont = True
        Me.INDviewAmortizationDetailCost.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAmortizationDetailCost.Appearance.GroupRow.Options.UseFont = True
        Me.INDviewAmortizationDetailCost.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDviewAmortizationDetailCost.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDviewAmortizationDetailCost.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDviewAmortizationDetailCost.Appearance.Row.Options.UseFont = True
        Me.INDviewAmortizationDetailCost.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDviewAmortizationDetailCost.Appearance.ViewCaption.Options.UseFont = True
        Me.INDviewAmortizationDetailCost.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColMainAccount, Me.INDColThirdParty, Me.INDColLocation, Me.INDColCostCenter, Me.INDColAmortizedDays, Me.INDColAmortizedValue})
        Me.INDviewAmortizationDetailCost.GridControl = Me.INDgcAmortizationDetailCost
        Me.INDviewAmortizationDetailCost.Name = "INDviewAmortizationDetailCost"
        Me.INDviewAmortizationDetailCost.OptionsView.EnableAppearanceEvenRow = True
        Me.INDviewAmortizationDetailCost.OptionsView.EnableAppearanceOddRow = True
        Me.INDviewAmortizationDetailCost.OptionsView.ShowAutoFilterRow = True
        Me.INDviewAmortizationDetailCost.OptionsView.ShowDetailButtons = False
        Me.INDviewAmortizationDetailCost.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDviewAmortizationDetailCost, False)
        '
        'INDColMainAccount
        '
        Me.INDColMainAccount.Caption = "Cuenta"
        Me.INDColMainAccount.FieldName = "MainAccountId.NumberName"
        Me.INDColMainAccount.Name = "INDColMainAccount"
        Me.INDColMainAccount.OptionsColumn.AllowEdit = False
        Me.INDColMainAccount.OptionsColumn.AllowFocus = False
        Me.INDColMainAccount.Visible = True
        Me.INDColMainAccount.VisibleIndex = 0
        '
        'INDColThirdParty
        '
        Me.INDColThirdParty.Caption = "Tercero"
        Me.INDColThirdParty.FieldName = "ThirdPartyId.NitName"
        Me.INDColThirdParty.Name = "INDColThirdParty"
        Me.INDColThirdParty.OptionsColumn.AllowEdit = False
        Me.INDColThirdParty.OptionsColumn.AllowFocus = False
        Me.INDColThirdParty.Visible = True
        Me.INDColThirdParty.VisibleIndex = 1
        '
        'INDColLocation
        '
        Me.INDColLocation.Caption = "Localización"
        Me.INDColLocation.FieldName = "LocationId.CodeName"
        Me.INDColLocation.Name = "INDColLocation"
        Me.INDColLocation.OptionsColumn.AllowEdit = False
        Me.INDColLocation.OptionsColumn.AllowFocus = False
        Me.INDColLocation.Visible = True
        Me.INDColLocation.VisibleIndex = 2
        '
        'INDColCostCenter
        '
        Me.INDColCostCenter.Caption = "Centro Costo"
        Me.INDColCostCenter.FieldName = "CostCenterId.CodeName"
        Me.INDColCostCenter.Name = "INDColCostCenter"
        Me.INDColCostCenter.OptionsColumn.AllowEdit = False
        Me.INDColCostCenter.OptionsColumn.AllowFocus = False
        Me.INDColCostCenter.Visible = True
        Me.INDColCostCenter.VisibleIndex = 3
        '
        'INDColAmortizedDays
        '
        Me.INDColAmortizedDays.Caption = "Días Amortizados"
        Me.INDColAmortizedDays.FieldName = "AmortizedDays"
        Me.INDColAmortizedDays.Name = "INDColAmortizedDays"
        Me.INDColAmortizedDays.OptionsColumn.AllowEdit = False
        Me.INDColAmortizedDays.OptionsColumn.AllowFocus = False
        Me.INDColAmortizedDays.Visible = True
        Me.INDColAmortizedDays.VisibleIndex = 4
        '
        'INDColAmortizedValue
        '
        Me.INDColAmortizedValue.Caption = "Valor Amortizado"
        Me.INDColAmortizedValue.DisplayFormat.FormatString = "c2"
        Me.INDColAmortizedValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColAmortizedValue.FieldName = "AmortizedValue"
        Me.INDColAmortizedValue.Name = "INDColAmortizedValue"
        Me.INDColAmortizedValue.OptionsColumn.AllowEdit = False
        Me.INDColAmortizedValue.OptionsColumn.AllowFocus = False
        Me.INDColAmortizedValue.Visible = True
        Me.INDColAmortizedValue.VisibleIndex = 5
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemAmortizationDetailCost})
        Me.LayoutControlGroup3.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(874, 256)
        Me.LayoutControlGroup3.TextVisible = False
        '
        'INDlyItemAmortizationDetailCost
        '
        Me.INDlyItemAmortizationDetailCost.Control = Me.INDgcAmortizationDetailCost
        Me.INDlyItemAmortizationDetailCost.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemAmortizationDetailCost.Name = "INDlyItemAmortizationDetailCost"
        Me.INDlyItemAmortizationDetailCost.Size = New System.Drawing.Size(854, 236)
        Me.INDlyItemAmortizationDetailCost.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAmortizationDetailCost.TextVisible = False
        '
        'FrmFixedAssetDepreciation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2011, 701)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFixedAssetDepreciation"
        Me.Opacity = 1.0R
        Me.Tag = "1122"
        Me.Text = "Depreciación"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyDepreciation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyDepreciation.ResumeLayout(False)
        CType(Me.INDpopupDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupDetailCost.ResumeLayout(False)
        CType(Me.INDlyDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyDetailCost.ResumeLayout(False)
        CType(Me.INDgcDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDepreciation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAmortization, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupAmortizationDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPopupAmortizationDetailCost.ResumeLayout(False)
        CType(Me.INDlyAmortizationDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyAmortizationDetailCost.ResumeLayout(False)
        CType(Me.INDgcAmortizationDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDviewAmortizationDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAmortizationDetailCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyDepreciation As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDlygDepreciation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDpopupDetailCost As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyDetailCost As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgcDetailCost As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewDetailCost As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemDetailCost As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDlygAmortization As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrDateNavigator1 As CtrDateNavigator
    Friend WithEvents INDLcgPeriod As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDPopupAmortizationDetailCost As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyAmortizationDetailCost As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDgcAmortizationDetailCost As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDviewAmortizationDetailCost As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColMainAccount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColThirdParty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColLocation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCostCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAmortizedDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAmortizedValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemAmortizationDetailCost As DevExpress.XtraLayout.LayoutControlItem
End Class
