Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTaxesValidity
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTaxesValidity))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSlRetentionApplicationForm = New Presentation.Controls.CtrYesNo()
        Me.GridView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleIndustryApplicationForm = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSlIterest = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit6View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlReteICAPreviousPeriod = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlValorizationPreviousPeriod = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSleIndustryBusinessTaxPreviousPeriod = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlePropertyTaxPreviousPeriod = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlReteICACashReceipt = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlValorizationCashReceipt = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSleIndustryBusinessTax = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlePropertyTaxCashReceipt = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlAccountPayableCloseAnual = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit5View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlAccountPayableOtherPortfolio = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlAccountingAccountIngressInterest = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSlLegalSalarium = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDslYear = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgValidity = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAccountingAccountIngressInterest = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAccountPayableOtherPortfolio = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAccountPayableCloseAnual = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciYear = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSlLegalMinimunSalary = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIndustryApplicationForm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRetentionApplicationForm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCashReceipt = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciPropertyTaxCashReceipt = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIndustryBusinessTaxCashReceipt = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValorizationCashReceipt = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReteICACashReceipt = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgPreviousVigency = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciPropertyTaxPreviousPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciIndustryBusinessTaxPreviousPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValorizationPreviousPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciReteICAPreviousPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciInterest = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDSlRetentionApplicationForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlRetentionApplicationForm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleIndustryApplicationForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleIndustryApplicationForm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlIterest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit6View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlReteICAPreviousPeriod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlValorizationPreviousPeriod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleIndustryBusinessTaxPreviousPeriod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlePropertyTaxPreviousPeriod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlReteICACashReceipt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlValorizationCashReceipt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleIndustryBusinessTax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlePropertyTaxCashReceipt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlAccountPayableCloseAnual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit5View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlAccountPayableOtherPortfolio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlAccountingAccountIngressInterest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlLegalSalarium.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDslYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAccountingAccountIngressInterest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAccountPayableOtherPortfolio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAccountPayableCloseAnual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlLegalMinimunSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIndustryApplicationForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRetentionApplicationForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCashReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPropertyTaxCashReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIndustryBusinessTaxCashReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValorizationCashReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReteICACashReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPreviousVigency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPropertyTaxPreviousPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIndustryBusinessTaxPreviousPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValorizationPreviousPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciReteICAPreviousPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciInterest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LayoutControl1)
        Me.PanelControl1.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(2, 7)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1461, 574)
        Me.PanelControl1.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSlRetentionApplicationForm)
        Me.LayoutControl1.Controls.Add(Me.INDsleIndustryApplicationForm)
        Me.LayoutControl1.Controls.Add(Me.INDSlIterest)
        Me.LayoutControl1.Controls.Add(Me.INDSlReteICAPreviousPeriod)
        Me.LayoutControl1.Controls.Add(Me.INDSlValorizationPreviousPeriod)
        Me.LayoutControl1.Controls.Add(Me.INDSleIndustryBusinessTaxPreviousPeriod)
        Me.LayoutControl1.Controls.Add(Me.INDSlePropertyTaxPreviousPeriod)
        Me.LayoutControl1.Controls.Add(Me.INDSlReteICACashReceipt)
        Me.LayoutControl1.Controls.Add(Me.INDSlValorizationCashReceipt)
        Me.LayoutControl1.Controls.Add(Me.INDSleIndustryBusinessTax)
        Me.LayoutControl1.Controls.Add(Me.INDSlePropertyTaxCashReceipt)
        Me.LayoutControl1.Controls.Add(Me.INDSlAccountPayableCloseAnual)
        Me.LayoutControl1.Controls.Add(Me.INDSlAccountPayableOtherPortfolio)
        Me.LayoutControl1.Controls.Add(Me.INDSlAccountingAccountIngressInterest)
        Me.LayoutControl1.Controls.Add(Me.INDSlLegalSalarium)
        Me.LayoutControl1.Controls.Add(Me.INDslYear)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 2)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1257, 570)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDSlRetentionApplicationForm
        '
        Me.INDSlRetentionApplicationForm.EnterMoveNextControl = True
        Me.INDSlRetentionApplicationForm.Location = New System.Drawing.Point(19, 443)
        Me.INDSlRetentionApplicationForm.Name = "INDSlRetentionApplicationForm"
        Me.INDSlRetentionApplicationForm.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSlRetentionApplicationForm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlRetentionApplicationForm.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlRetentionApplicationForm.Properties.Appearance.Options.UseFont = True
        Me.INDSlRetentionApplicationForm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSlRetentionApplicationForm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSlRetentionApplicationForm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlRetentionApplicationForm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSlRetentionApplicationForm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSlRetentionApplicationForm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSlRetentionApplicationForm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlRetentionApplicationForm.Properties.DataSource = CType(resources.GetObject("INDSlRetentionApplicationForm.Properties.DataSource"), Object)
        Me.INDSlRetentionApplicationForm.Properties.DisplayMember = "Item2"
        Me.INDSlRetentionApplicationForm.Properties.ImmediatePopup = True
        Me.INDSlRetentionApplicationForm.Properties.NullText = ""
        Me.INDSlRetentionApplicationForm.Properties.ValueMember = "Item1"
        Me.INDSlRetentionApplicationForm.Properties.View = Me.GridView10
        Me.INDSlRetentionApplicationForm.Size = New System.Drawing.Size(386, 28)
        Me.INDSlRetentionApplicationForm.StyleController = Me.LayoutControl1
        ToolTipItem1.Text = "Obligación única grupo CxP y CxP."
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.INDSlRetentionApplicationForm.SuperTip = SuperToolTip1
        Me.INDSlRetentionApplicationForm.TabIndex = 20
        Me.INDSlRetentionApplicationForm.ToolTip = "Este Campo es Necesario"
        '
        'GridView10
        '
        Me.GridView10.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView10.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView10.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView10.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView10.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView10.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.GridView10.Appearance.GroupRow.Options.UseFont = True
        Me.GridView10.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GridView10.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView10.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView10.Appearance.Row.Options.UseFont = True
        Me.GridView10.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.GridView10.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView10.Name = "GridView10"
        Me.GridView10.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView10.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView10.OptionsView.EnableAppearanceOddRow = True
        Me.GridView10.OptionsView.ShowAutoFilterRow = True
        Me.GridView10.OptionsView.ShowDetailButtons = False
        Me.GridView10.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.Caption = "Selección"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.Caption = "Selección"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'INDsleIndustryApplicationForm
        '
        Me.INDsleIndustryApplicationForm.EnterMoveNextControl = True
        Me.INDsleIndustryApplicationForm.Location = New System.Drawing.Point(19, 383)
        Me.INDsleIndustryApplicationForm.Name = "INDsleIndustryApplicationForm"
        Me.INDsleIndustryApplicationForm.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleIndustryApplicationForm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleIndustryApplicationForm.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleIndustryApplicationForm.Properties.Appearance.Options.UseFont = True
        Me.INDsleIndustryApplicationForm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleIndustryApplicationForm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleIndustryApplicationForm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleIndustryApplicationForm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleIndustryApplicationForm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleIndustryApplicationForm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleIndustryApplicationForm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleIndustryApplicationForm.Properties.DataSource = CType(resources.GetObject("INDsleIndustryApplicationForm.Properties.DataSource"), Object)
        Me.INDsleIndustryApplicationForm.Properties.DisplayMember = "Item2"
        Me.INDsleIndustryApplicationForm.Properties.ImmediatePopup = True
        Me.INDsleIndustryApplicationForm.Properties.NullText = ""
        Me.INDsleIndustryApplicationForm.Properties.ValueMember = "Item1"
        Me.INDsleIndustryApplicationForm.Properties.View = Me.CtrYesNo4View
        Me.INDsleIndustryApplicationForm.Size = New System.Drawing.Size(386, 28)
        Me.INDsleIndustryApplicationForm.StyleController = Me.LayoutControl1
        ToolTipItem2.Text = "Obligación única grupo CxP y CxP."
        SuperToolTip2.Items.Add(ToolTipItem2)
        Me.INDsleIndustryApplicationForm.SuperTip = SuperToolTip2
        Me.INDsleIndustryApplicationForm.TabIndex = 19
        Me.INDsleIndustryApplicationForm.ToolTip = "Este Campo es Necesario"
        '
        'CtrYesNo4View
        '
        Me.CtrYesNo4View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CtrYesNo4View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.CtrYesNo4View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.CtrYesNo4View.Appearance.GroupRow.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo4View.Appearance.HeaderPanel.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.CtrYesNo4View.Appearance.Row.Options.UseFont = True
        Me.CtrYesNo4View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7})
        Me.CtrYesNo4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo4View.Name = "CtrYesNo4View"
        Me.CtrYesNo4View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo4View.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo4View.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo4View.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo4View.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo4View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.Caption = "Selección"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.Caption = "Selección"
        Me.GridColumn7.FieldName = "Item2"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        '
        'INDSlIterest
        '
        Me.INDSlIterest.Location = New System.Drawing.Point(847, 323)
        Me.INDSlIterest.Name = "INDSlIterest"
        Me.INDSlIterest.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlIterest.Properties.Appearance.Options.UseFont = True
        Me.INDSlIterest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlIterest.Properties.NullText = ""
        Me.INDSlIterest.Properties.View = Me.SearchLookUpEdit6View
        Me.INDSlIterest.Size = New System.Drawing.Size(386, 28)
        Me.INDSlIterest.StyleController = Me.LayoutControl1
        Me.INDSlIterest.TabIndex = 18
        '
        'SearchLookUpEdit6View
        '
        Me.SearchLookUpEdit6View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit6View.Name = "SearchLookUpEdit6View"
        Me.SearchLookUpEdit6View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit6View.OptionsView.ShowGroupPanel = False
        '
        'INDSlReteICAPreviousPeriod
        '
        Me.INDSlReteICAPreviousPeriod.Location = New System.Drawing.Point(847, 263)
        Me.INDSlReteICAPreviousPeriod.Name = "INDSlReteICAPreviousPeriod"
        Me.INDSlReteICAPreviousPeriod.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlReteICAPreviousPeriod.Properties.Appearance.Options.UseFont = True
        Me.INDSlReteICAPreviousPeriod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlReteICAPreviousPeriod.Properties.NullText = ""
        Me.INDSlReteICAPreviousPeriod.Properties.View = Me.GridView9
        Me.INDSlReteICAPreviousPeriod.Size = New System.Drawing.Size(386, 28)
        Me.INDSlReteICAPreviousPeriod.StyleController = Me.LayoutControl1
        Me.INDSlReteICAPreviousPeriod.TabIndex = 17
        '
        'GridView9
        '
        Me.GridView9.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView9.Name = "GridView9"
        Me.GridView9.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView9.OptionsView.ShowGroupPanel = False
        '
        'INDSlValorizationPreviousPeriod
        '
        Me.INDSlValorizationPreviousPeriod.Location = New System.Drawing.Point(847, 203)
        Me.INDSlValorizationPreviousPeriod.Name = "INDSlValorizationPreviousPeriod"
        Me.INDSlValorizationPreviousPeriod.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlValorizationPreviousPeriod.Properties.Appearance.Options.UseFont = True
        Me.INDSlValorizationPreviousPeriod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlValorizationPreviousPeriod.Properties.NullText = ""
        Me.INDSlValorizationPreviousPeriod.Properties.View = Me.GridView8
        Me.INDSlValorizationPreviousPeriod.Size = New System.Drawing.Size(386, 28)
        Me.INDSlValorizationPreviousPeriod.StyleController = Me.LayoutControl1
        Me.INDSlValorizationPreviousPeriod.TabIndex = 16
        '
        'GridView8
        '
        Me.GridView8.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView8.Name = "GridView8"
        Me.GridView8.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView8.OptionsView.ShowGroupPanel = False
        '
        'INDSleIndustryBusinessTaxPreviousPeriod
        '
        Me.INDSleIndustryBusinessTaxPreviousPeriod.Location = New System.Drawing.Point(847, 143)
        Me.INDSleIndustryBusinessTaxPreviousPeriod.Name = "INDSleIndustryBusinessTaxPreviousPeriod"
        Me.INDSleIndustryBusinessTaxPreviousPeriod.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleIndustryBusinessTaxPreviousPeriod.Properties.Appearance.Options.UseFont = True
        Me.INDSleIndustryBusinessTaxPreviousPeriod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleIndustryBusinessTaxPreviousPeriod.Properties.NullText = ""
        Me.INDSleIndustryBusinessTaxPreviousPeriod.Properties.View = Me.GridView7
        Me.INDSleIndustryBusinessTaxPreviousPeriod.Size = New System.Drawing.Size(386, 28)
        Me.INDSleIndustryBusinessTaxPreviousPeriod.StyleController = Me.LayoutControl1
        Me.INDSleIndustryBusinessTaxPreviousPeriod.TabIndex = 15
        '
        'GridView7
        '
        Me.GridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView7.Name = "GridView7"
        Me.GridView7.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView7.OptionsView.ShowGroupPanel = False
        '
        'INDSlePropertyTaxPreviousPeriod
        '
        Me.INDSlePropertyTaxPreviousPeriod.Location = New System.Drawing.Point(847, 83)
        Me.INDSlePropertyTaxPreviousPeriod.Name = "INDSlePropertyTaxPreviousPeriod"
        Me.INDSlePropertyTaxPreviousPeriod.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlePropertyTaxPreviousPeriod.Properties.Appearance.Options.UseFont = True
        Me.INDSlePropertyTaxPreviousPeriod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlePropertyTaxPreviousPeriod.Properties.NullText = ""
        Me.INDSlePropertyTaxPreviousPeriod.Properties.View = Me.GridView6
        Me.INDSlePropertyTaxPreviousPeriod.Size = New System.Drawing.Size(386, 28)
        Me.INDSlePropertyTaxPreviousPeriod.StyleController = Me.LayoutControl1
        Me.INDSlePropertyTaxPreviousPeriod.TabIndex = 14
        '
        'GridView6
        '
        Me.GridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView6.OptionsView.ShowGroupPanel = False
        '
        'INDSlReteICACashReceipt
        '
        Me.INDSlReteICACashReceipt.Location = New System.Drawing.Point(433, 263)
        Me.INDSlReteICACashReceipt.Name = "INDSlReteICACashReceipt"
        Me.INDSlReteICACashReceipt.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlReteICACashReceipt.Properties.Appearance.Options.UseFont = True
        Me.INDSlReteICACashReceipt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlReteICACashReceipt.Properties.NullText = ""
        Me.INDSlReteICACashReceipt.Properties.View = Me.GridView5
        Me.INDSlReteICACashReceipt.Size = New System.Drawing.Size(386, 28)
        Me.INDSlReteICACashReceipt.StyleController = Me.LayoutControl1
        Me.INDSlReteICACashReceipt.TabIndex = 13
        '
        'GridView5
        '
        Me.GridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView5.OptionsView.ShowGroupPanel = False
        '
        'INDSlValorizationCashReceipt
        '
        Me.INDSlValorizationCashReceipt.Location = New System.Drawing.Point(433, 203)
        Me.INDSlValorizationCashReceipt.Name = "INDSlValorizationCashReceipt"
        Me.INDSlValorizationCashReceipt.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlValorizationCashReceipt.Properties.Appearance.Options.UseFont = True
        Me.INDSlValorizationCashReceipt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlValorizationCashReceipt.Properties.NullText = ""
        Me.INDSlValorizationCashReceipt.Properties.View = Me.GridView4
        Me.INDSlValorizationCashReceipt.Size = New System.Drawing.Size(386, 28)
        Me.INDSlValorizationCashReceipt.StyleController = Me.LayoutControl1
        Me.INDSlValorizationCashReceipt.TabIndex = 12
        '
        'GridView4
        '
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.ShowGroupPanel = False
        '
        'INDSleIndustryBusinessTax
        '
        Me.INDSleIndustryBusinessTax.Location = New System.Drawing.Point(433, 143)
        Me.INDSleIndustryBusinessTax.Name = "INDSleIndustryBusinessTax"
        Me.INDSleIndustryBusinessTax.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleIndustryBusinessTax.Properties.Appearance.Options.UseFont = True
        Me.INDSleIndustryBusinessTax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleIndustryBusinessTax.Properties.NullText = ""
        Me.INDSleIndustryBusinessTax.Properties.View = Me.GridView3
        Me.INDSleIndustryBusinessTax.Size = New System.Drawing.Size(386, 28)
        Me.INDSleIndustryBusinessTax.StyleController = Me.LayoutControl1
        Me.INDSleIndustryBusinessTax.TabIndex = 11
        '
        'GridView3
        '
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'INDSlePropertyTaxCashReceipt
        '
        Me.INDSlePropertyTaxCashReceipt.EditValue = ""
        Me.INDSlePropertyTaxCashReceipt.Location = New System.Drawing.Point(433, 83)
        Me.INDSlePropertyTaxCashReceipt.Name = "INDSlePropertyTaxCashReceipt"
        Me.INDSlePropertyTaxCashReceipt.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlePropertyTaxCashReceipt.Properties.Appearance.Options.UseFont = True
        Me.INDSlePropertyTaxCashReceipt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlePropertyTaxCashReceipt.Properties.NullText = ""
        Me.INDSlePropertyTaxCashReceipt.Properties.View = Me.GridView2
        Me.INDSlePropertyTaxCashReceipt.Size = New System.Drawing.Size(386, 28)
        Me.INDSlePropertyTaxCashReceipt.StyleController = Me.LayoutControl1
        Me.INDSlePropertyTaxCashReceipt.TabIndex = 10
        '
        'GridView2
        '
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'INDSlAccountPayableCloseAnual
        '
        Me.INDSlAccountPayableCloseAnual.Location = New System.Drawing.Point(19, 203)
        Me.INDSlAccountPayableCloseAnual.Name = "INDSlAccountPayableCloseAnual"
        Me.INDSlAccountPayableCloseAnual.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlAccountPayableCloseAnual.Properties.Appearance.Options.UseFont = True
        Me.INDSlAccountPayableCloseAnual.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlAccountPayableCloseAnual.Properties.NullText = ""
        Me.INDSlAccountPayableCloseAnual.Properties.View = Me.SearchLookUpEdit5View
        Me.INDSlAccountPayableCloseAnual.Size = New System.Drawing.Size(386, 28)
        Me.INDSlAccountPayableCloseAnual.StyleController = Me.LayoutControl1
        Me.INDSlAccountPayableCloseAnual.TabIndex = 9
        '
        'SearchLookUpEdit5View
        '
        Me.SearchLookUpEdit5View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit5View.Name = "SearchLookUpEdit5View"
        Me.SearchLookUpEdit5View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit5View.OptionsView.ShowGroupPanel = False
        '
        'INDSlAccountPayableOtherPortfolio
        '
        Me.INDSlAccountPayableOtherPortfolio.Location = New System.Drawing.Point(19, 263)
        Me.INDSlAccountPayableOtherPortfolio.Name = "INDSlAccountPayableOtherPortfolio"
        Me.INDSlAccountPayableOtherPortfolio.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlAccountPayableOtherPortfolio.Properties.Appearance.Options.UseFont = True
        Me.INDSlAccountPayableOtherPortfolio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlAccountPayableOtherPortfolio.Properties.NullText = ""
        Me.INDSlAccountPayableOtherPortfolio.Properties.View = Me.SearchLookUpEdit4View
        Me.INDSlAccountPayableOtherPortfolio.Size = New System.Drawing.Size(386, 28)
        Me.INDSlAccountPayableOtherPortfolio.StyleController = Me.LayoutControl1
        Me.INDSlAccountPayableOtherPortfolio.TabIndex = 8
        '
        'SearchLookUpEdit4View
        '
        Me.SearchLookUpEdit4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit4View.Name = "SearchLookUpEdit4View"
        Me.SearchLookUpEdit4View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit4View.OptionsView.ShowGroupPanel = False
        '
        'INDSlAccountingAccountIngressInterest
        '
        Me.INDSlAccountingAccountIngressInterest.Location = New System.Drawing.Point(19, 323)
        Me.INDSlAccountingAccountIngressInterest.Name = "INDSlAccountingAccountIngressInterest"
        Me.INDSlAccountingAccountIngressInterest.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlAccountingAccountIngressInterest.Properties.Appearance.Options.UseFont = True
        Me.INDSlAccountingAccountIngressInterest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlAccountingAccountIngressInterest.Properties.NullText = ""
        Me.INDSlAccountingAccountIngressInterest.Properties.View = Me.SearchLookUpEdit2View
        Me.INDSlAccountingAccountIngressInterest.Size = New System.Drawing.Size(386, 28)
        Me.INDSlAccountingAccountIngressInterest.StyleController = Me.LayoutControl1
        Me.INDSlAccountingAccountIngressInterest.TabIndex = 6
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        '
        'INDSlLegalSalarium
        '
        Me.INDSlLegalSalarium.Location = New System.Drawing.Point(19, 143)
        Me.INDSlLegalSalarium.Name = "INDSlLegalSalarium"
        Me.INDSlLegalSalarium.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlLegalSalarium.Properties.Appearance.Options.UseFont = True
        Me.INDSlLegalSalarium.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlLegalSalarium.Properties.NullText = ""
        Me.INDSlLegalSalarium.Properties.View = Me.GridView1
        Me.INDSlLegalSalarium.Size = New System.Drawing.Size(386, 28)
        Me.INDSlLegalSalarium.StyleController = Me.LayoutControl1
        Me.INDSlLegalSalarium.TabIndex = 5
        '
        'GridView1
        '
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'INDslYear
        '
        Me.INDslYear.Location = New System.Drawing.Point(19, 83)
        Me.INDslYear.Name = "INDslYear"
        Me.INDslYear.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDslYear.Properties.Appearance.Options.UseFont = True
        Me.INDslYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDslYear.Properties.NullText = ""
        Me.INDslYear.Properties.View = Me.SearchLookUpEdit1View
        Me.INDslYear.Size = New System.Drawing.Size(386, 28)
        Me.INDslYear.StyleController = Me.LayoutControl1
        Me.INDslYear.TabIndex = 4
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgValidity, Me.INDLcgCashReceipt, Me.INDLcgPreviousVigency})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(-5, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1262, 553)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgValidity
        '
        Me.INDLcgValidity.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgValidity.AppearanceGroup.Options.UseFont = True
        Me.INDLcgValidity.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgValidity.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgValidity.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgValidity.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgValidity.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgValidity.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgValidity.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgValidity.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgValidity.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgValidity.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgValidity.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgValidity.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgValidity, False)
        Me.INDLcgValidity.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAccountingAccountIngressInterest, Me.INDLciAccountPayableOtherPortfolio, Me.INDLciAccountPayableCloseAnual, Me.INDLciYear, Me.INDSlLegalMinimunSalary, Me.INDLciIndustryApplicationForm, Me.INDLciRetentionApplicationForm})
        Me.INDLcgValidity.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgValidity.Name = "INDLcgValidity"
        Me.INDLcgValidity.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgValidity.Text = "Vigencia"
        '
        'INDLciAccountingAccountIngressInterest
        '
        Me.INDLciAccountingAccountIngressInterest.Control = Me.INDSlAccountingAccountIngressInterest
        Me.INDLciAccountingAccountIngressInterest.Location = New System.Drawing.Point(0, 240)
        Me.INDLciAccountingAccountIngressInterest.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciAccountingAccountIngressInterest.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciAccountingAccountIngressInterest.Name = "INDLciAccountingAccountIngressInterest"
        Me.INDLciAccountingAccountIngressInterest.Size = New System.Drawing.Size(390, 60)
        Me.INDLciAccountingAccountIngressInterest.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAccountingAccountIngressInterest.Text = "Cuenta Contable Ingreso Interés Otras Carteras"
        Me.INDLciAccountingAccountIngressInterest.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAccountingAccountIngressInterest.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciAccountPayableOtherPortfolio
        '
        Me.INDLciAccountPayableOtherPortfolio.Control = Me.INDSlAccountPayableOtherPortfolio
        Me.INDLciAccountPayableOtherPortfolio.Location = New System.Drawing.Point(0, 180)
        Me.INDLciAccountPayableOtherPortfolio.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciAccountPayableOtherPortfolio.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciAccountPayableOtherPortfolio.Name = "INDLciAccountPayableOtherPortfolio"
        Me.INDLciAccountPayableOtherPortfolio.Size = New System.Drawing.Size(390, 60)
        Me.INDLciAccountPayableOtherPortfolio.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAccountPayableOtherPortfolio.Text = "Concepto Recibo Caja Recaudo Otras Carteras"
        Me.INDLciAccountPayableOtherPortfolio.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAccountPayableOtherPortfolio.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciAccountPayableCloseAnual
        '
        Me.INDLciAccountPayableCloseAnual.Control = Me.INDSlAccountPayableCloseAnual
        Me.INDLciAccountPayableCloseAnual.Location = New System.Drawing.Point(0, 120)
        Me.INDLciAccountPayableCloseAnual.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciAccountPayableCloseAnual.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciAccountPayableCloseAnual.Name = "INDLciAccountPayableCloseAnual"
        Me.INDLciAccountPayableCloseAnual.Size = New System.Drawing.Size(390, 60)
        Me.INDLciAccountPayableCloseAnual.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAccountPayableCloseAnual.Text = "Tipo Comprobante Contable Cierre Anual"
        Me.INDLciAccountPayableCloseAnual.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAccountPayableCloseAnual.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciYear
        '
        Me.INDLciYear.Control = Me.INDslYear
        Me.INDLciYear.Location = New System.Drawing.Point(0, 0)
        Me.INDLciYear.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciYear.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciYear.Name = "INDLciYear"
        Me.INDLciYear.Size = New System.Drawing.Size(390, 60)
        Me.INDLciYear.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciYear.Text = "Año"
        Me.INDLciYear.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciYear.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDSlLegalMinimunSalary
        '
        Me.INDSlLegalMinimunSalary.Control = Me.INDSlLegalSalarium
        Me.INDSlLegalMinimunSalary.Location = New System.Drawing.Point(0, 60)
        Me.INDSlLegalMinimunSalary.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDSlLegalMinimunSalary.MinSize = New System.Drawing.Size(390, 60)
        Me.INDSlLegalMinimunSalary.Name = "INDSlLegalMinimunSalary"
        Me.INDSlLegalMinimunSalary.Size = New System.Drawing.Size(390, 60)
        Me.INDSlLegalMinimunSalary.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDSlLegalMinimunSalary.Text = "Salario Mínimo"
        Me.INDSlLegalMinimunSalary.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDSlLegalMinimunSalary.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciIndustryApplicationForm
        '
        Me.INDLciIndustryApplicationForm.Control = Me.INDsleIndustryApplicationForm
        Me.INDLciIndustryApplicationForm.Location = New System.Drawing.Point(0, 300)
        Me.INDLciIndustryApplicationForm.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryApplicationForm.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryApplicationForm.Name = "INDLciIndustryApplicationForm"
        Me.INDLciIndustryApplicationForm.Size = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryApplicationForm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIndustryApplicationForm.Text = "Permitir Formularios Industria y Comercio con Errores"
        Me.INDLciIndustryApplicationForm.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIndustryApplicationForm.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciRetentionApplicationForm
        '
        Me.INDLciRetentionApplicationForm.Control = Me.INDSlRetentionApplicationForm
        Me.INDLciRetentionApplicationForm.Location = New System.Drawing.Point(0, 360)
        Me.INDLciRetentionApplicationForm.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciRetentionApplicationForm.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciRetentionApplicationForm.Name = "INDLciRetentionApplicationForm"
        Me.INDLciRetentionApplicationForm.Size = New System.Drawing.Size(390, 114)
        Me.INDLciRetentionApplicationForm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRetentionApplicationForm.Text = "Permitir Formulario Ret. Industria y Comercio con Errores"
        Me.INDLciRetentionApplicationForm.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRetentionApplicationForm.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLcgCashReceipt
        '
        Me.INDLcgCashReceipt.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCashReceipt.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCashReceipt.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCashReceipt.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCashReceipt.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCashReceipt.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCashReceipt.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCashReceipt.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCashReceipt.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCashReceipt.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCashReceipt.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCashReceipt.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCashReceipt.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCashReceipt.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCashReceipt, False)
        Me.INDLcgCashReceipt.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciPropertyTaxCashReceipt, Me.INDLciIndustryBusinessTaxCashReceipt, Me.INDLciValorizationCashReceipt, Me.INDLciReteICACashReceipt})
        Me.INDLcgCashReceipt.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgCashReceipt.Name = "INDLcgCashReceipt"
        Me.INDLcgCashReceipt.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgCashReceipt.Text = "Anticipos - Conceptos de Recibos de Caja"
        '
        'INDLciPropertyTaxCashReceipt
        '
        Me.INDLciPropertyTaxCashReceipt.Control = Me.INDSlePropertyTaxCashReceipt
        Me.INDLciPropertyTaxCashReceipt.Location = New System.Drawing.Point(0, 0)
        Me.INDLciPropertyTaxCashReceipt.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciPropertyTaxCashReceipt.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPropertyTaxCashReceipt.Name = "INDLciPropertyTaxCashReceipt"
        Me.INDLciPropertyTaxCashReceipt.Size = New System.Drawing.Size(390, 60)
        Me.INDLciPropertyTaxCashReceipt.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPropertyTaxCashReceipt.Text = "Predial"
        Me.INDLciPropertyTaxCashReceipt.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPropertyTaxCashReceipt.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciIndustryBusinessTaxCashReceipt
        '
        Me.INDLciIndustryBusinessTaxCashReceipt.Control = Me.INDSleIndustryBusinessTax
        Me.INDLciIndustryBusinessTaxCashReceipt.Location = New System.Drawing.Point(0, 60)
        Me.INDLciIndustryBusinessTaxCashReceipt.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryBusinessTaxCashReceipt.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryBusinessTaxCashReceipt.Name = "INDLciIndustryBusinessTaxCashReceipt"
        Me.INDLciIndustryBusinessTaxCashReceipt.Size = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryBusinessTaxCashReceipt.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIndustryBusinessTaxCashReceipt.Text = "Industria y Comercio"
        Me.INDLciIndustryBusinessTaxCashReceipt.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIndustryBusinessTaxCashReceipt.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciValorizationCashReceipt
        '
        Me.INDLciValorizationCashReceipt.Control = Me.INDSlValorizationCashReceipt
        Me.INDLciValorizationCashReceipt.Location = New System.Drawing.Point(0, 120)
        Me.INDLciValorizationCashReceipt.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciValorizationCashReceipt.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciValorizationCashReceipt.Name = "INDLciValorizationCashReceipt"
        Me.INDLciValorizationCashReceipt.Size = New System.Drawing.Size(390, 60)
        Me.INDLciValorizationCashReceipt.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValorizationCashReceipt.Text = "Valorización"
        Me.INDLciValorizationCashReceipt.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValorizationCashReceipt.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciReteICACashReceipt
        '
        Me.INDLciReteICACashReceipt.Control = Me.INDSlReteICACashReceipt
        Me.INDLciReteICACashReceipt.Location = New System.Drawing.Point(0, 180)
        Me.INDLciReteICACashReceipt.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciReteICACashReceipt.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciReteICACashReceipt.Name = "INDLciReteICACashReceipt"
        Me.INDLciReteICACashReceipt.Size = New System.Drawing.Size(390, 294)
        Me.INDLciReteICACashReceipt.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReteICACashReceipt.Text = "Rete ICA"
        Me.INDLciReteICACashReceipt.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReteICACashReceipt.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLcgPreviousVigency
        '
        Me.INDLcgPreviousVigency.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPreviousVigency.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPreviousVigency.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPreviousVigency.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPreviousVigency.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreviousVigency.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPreviousVigency.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPreviousVigency.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPreviousVigency.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreviousVigency.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPreviousVigency.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreviousVigency.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPreviousVigency.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPreviousVigency.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPreviousVigency, False)
        Me.INDLcgPreviousVigency.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciPropertyTaxPreviousPeriod, Me.INDLciIndustryBusinessTaxPreviousPeriod, Me.INDLciValorizationPreviousPeriod, Me.INDLciReteICAPreviousPeriod, Me.INDLciInterest})
        Me.INDLcgPreviousVigency.Location = New System.Drawing.Point(828, 0)
        Me.INDLcgPreviousVigency.Name = "INDLcgPreviousVigency"
        Me.INDLcgPreviousVigency.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgPreviousVigency.Text = "Vigen. Anteriores - Conc. Recibos de Caja"
        '
        'INDLciPropertyTaxPreviousPeriod
        '
        Me.INDLciPropertyTaxPreviousPeriod.Control = Me.INDSlePropertyTaxPreviousPeriod
        Me.INDLciPropertyTaxPreviousPeriod.Location = New System.Drawing.Point(0, 0)
        Me.INDLciPropertyTaxPreviousPeriod.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciPropertyTaxPreviousPeriod.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPropertyTaxPreviousPeriod.Name = "INDLciPropertyTaxPreviousPeriod"
        Me.INDLciPropertyTaxPreviousPeriod.Size = New System.Drawing.Size(390, 60)
        Me.INDLciPropertyTaxPreviousPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPropertyTaxPreviousPeriod.Text = "Predial"
        Me.INDLciPropertyTaxPreviousPeriod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPropertyTaxPreviousPeriod.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciIndustryBusinessTaxPreviousPeriod
        '
        Me.INDLciIndustryBusinessTaxPreviousPeriod.Control = Me.INDSleIndustryBusinessTaxPreviousPeriod
        Me.INDLciIndustryBusinessTaxPreviousPeriod.Location = New System.Drawing.Point(0, 60)
        Me.INDLciIndustryBusinessTaxPreviousPeriod.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryBusinessTaxPreviousPeriod.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryBusinessTaxPreviousPeriod.Name = "INDLciIndustryBusinessTaxPreviousPeriod"
        Me.INDLciIndustryBusinessTaxPreviousPeriod.Size = New System.Drawing.Size(390, 60)
        Me.INDLciIndustryBusinessTaxPreviousPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIndustryBusinessTaxPreviousPeriod.Text = "Industria y Comercio"
        Me.INDLciIndustryBusinessTaxPreviousPeriod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIndustryBusinessTaxPreviousPeriod.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciValorizationPreviousPeriod
        '
        Me.INDLciValorizationPreviousPeriod.Control = Me.INDSlValorizationPreviousPeriod
        Me.INDLciValorizationPreviousPeriod.Location = New System.Drawing.Point(0, 120)
        Me.INDLciValorizationPreviousPeriod.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciValorizationPreviousPeriod.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciValorizationPreviousPeriod.Name = "INDLciValorizationPreviousPeriod"
        Me.INDLciValorizationPreviousPeriod.Size = New System.Drawing.Size(390, 60)
        Me.INDLciValorizationPreviousPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValorizationPreviousPeriod.Text = "Valorización"
        Me.INDLciValorizationPreviousPeriod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValorizationPreviousPeriod.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciReteICAPreviousPeriod
        '
        Me.INDLciReteICAPreviousPeriod.Control = Me.INDSlReteICAPreviousPeriod
        Me.INDLciReteICAPreviousPeriod.Location = New System.Drawing.Point(0, 180)
        Me.INDLciReteICAPreviousPeriod.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciReteICAPreviousPeriod.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciReteICAPreviousPeriod.Name = "INDLciReteICAPreviousPeriod"
        Me.INDLciReteICAPreviousPeriod.Size = New System.Drawing.Size(390, 60)
        Me.INDLciReteICAPreviousPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciReteICAPreviousPeriod.Text = "Rete ICA"
        Me.INDLciReteICAPreviousPeriod.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciReteICAPreviousPeriod.TextSize = New System.Drawing.Size(376, 21)
        '
        'INDLciInterest
        '
        Me.INDLciInterest.Control = Me.INDSlIterest
        Me.INDLciInterest.Location = New System.Drawing.Point(0, 240)
        Me.INDLciInterest.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciInterest.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciInterest.Name = "INDLciInterest"
        Me.INDLciInterest.Size = New System.Drawing.Size(390, 234)
        Me.INDLciInterest.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciInterest.Text = "Interés"
        Me.INDLciInterest.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciInterest.TextSize = New System.Drawing.Size(376, 21)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 2)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.Caption = "Selección"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.Caption = "Selección"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn17.Caption = "Selección"
        Me.GridColumn17.FieldName = "Item2"
        Me.GridColumn17.Name = "GridColumn17"
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.Caption = "Selección"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'FrmTaxesValidity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmTaxesValidity"
        Me.Opacity = 1.0R
        Me.Tag = "1827"
        Me.Text = "FrmTaxesValidity"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDSlRetentionApplicationForm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlRetentionApplicationForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleIndustryApplicationForm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleIndustryApplicationForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlIterest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit6View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlReteICAPreviousPeriod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlValorizationPreviousPeriod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleIndustryBusinessTaxPreviousPeriod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlePropertyTaxPreviousPeriod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlReteICACashReceipt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlValorizationCashReceipt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleIndustryBusinessTax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlePropertyTaxCashReceipt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlAccountPayableCloseAnual.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit5View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlAccountPayableOtherPortfolio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlAccountingAccountIngressInterest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlLegalSalarium.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDslYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAccountingAccountIngressInterest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAccountPayableOtherPortfolio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAccountPayableCloseAnual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlLegalMinimunSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIndustryApplicationForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRetentionApplicationForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCashReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPropertyTaxCashReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIndustryBusinessTaxCashReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValorizationCashReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReteICACashReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPreviousVigency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPropertyTaxPreviousPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIndustryBusinessTaxPreviousPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValorizationPreviousPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciReteICAPreviousPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciInterest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDslYear As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLcgValidity As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciYear As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlAccountPayableCloseAnual As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit5View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlAccountPayableOtherPortfolio As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlAccountingAccountIngressInterest As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlLegalSalarium As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlLegalMinimunSalary As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAccountPayableOtherPortfolio As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAccountPayableCloseAnual As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAccountingAccountIngressInterest As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlReteICACashReceipt As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlValorizationCashReceipt As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleIndustryBusinessTax As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlePropertyTaxCashReceipt As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgCashReceipt As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciPropertyTaxCashReceipt As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciIndustryBusinessTaxCashReceipt As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValorizationCashReceipt As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciReteICACashReceipt As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlePropertyTaxPreviousPeriod As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgPreviousVigency As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciPropertyTaxPreviousPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlIterest As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit6View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlReteICAPreviousPeriod As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSlValorizationPreviousPeriod As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleIndustryBusinessTaxPreviousPeriod As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciIndustryBusinessTaxPreviousPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValorizationPreviousPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciReteICAPreviousPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciInterest As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSlRetentionApplicationForm As Presentation.Controls.CtrYesNo
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleIndustryApplicationForm As Presentation.Controls.CtrYesNo
    Friend WithEvents CtrYesNo4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciIndustryApplicationForm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciRetentionApplicationForm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
