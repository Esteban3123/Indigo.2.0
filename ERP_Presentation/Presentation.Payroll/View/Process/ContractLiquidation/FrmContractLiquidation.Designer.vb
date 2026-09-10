Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmContractLiquidation
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode3 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode4 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim GridLevelNode5 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.INDgrvContract = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcInitialContractNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcContractNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcContractCompany = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcContractGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcContractPosition = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcBasicSalary = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcContractInitialDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcContractEndingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrdHumanTalent = New DevExpress.XtraGrid.GridControl()
        Me.INDgrvHumanTalent = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcEmloyeeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcEmployeeNames = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcRetirementDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepRetirementDate = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.INDgrcRetirementReason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepRetirementReason = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcRRCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcRRName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcActions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepDeleteAction = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDrepConfirm = New DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup()
        Me.INDgrvLiquidations = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcLiquidationPeriod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcLiquidationTotalAccrued = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcLiquidationTotalDeducted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcLiquidationTotalPaid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcLiquidationPeriodIBC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrvNovelties = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcNoveltiesPeriod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcPayrollDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcWorkedDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcDisabilityDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcVacationDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcSanctionDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcPermissionDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcLicenseDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcQuoteHealthDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDlyCtrContractLiquidation = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPopUpContainerControl2 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDteResultFormulaEmployer = New DevExpress.XtraEditors.TextEdit()
        Me.INDteReplaceFormulaEmployer = New System.Windows.Forms.TextBox()
        Me.INDteUsedFormulaEmployer = New System.Windows.Forms.TextBox()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDbtnAddEmployee = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsleEmployees = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgrvSleEmployees = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcEmployeeNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcEmployeeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDlyControlEmployeeLiquidation = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcEmployerLiquidation = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGcNameConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColActions2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDDeResolutionDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDtxtResolutionNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDgcMessage = New DevExpress.XtraGrid.GridControl()
        Me.INDgvMessage = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolInfoMessage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.INDcolDescriptionMessage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtRetirementReason = New DevExpress.XtraEditors.TextEdit()
        Me.INDgcLiquidationDetail = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColumnDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnInitialDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumnEndingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColumAccrued = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolDeducted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Acciones = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDteResultFormula = New DevExpress.XtraEditors.TextEdit()
        Me.INDteReplaceFormula = New System.Windows.Forms.TextBox()
        Me.INDteUsedFormula = New System.Windows.Forms.TextBox()
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyfUsedFormula1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyfReplaceFormula1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyfResultFormula1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDtxtTotalPaid = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPosition = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtSalaryBase = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtEndDate = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtInitialDate = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtEmployee = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtGroup = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGroupGeneralImformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemTotalPaid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEndDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPosition = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSalaryBase = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRetirementReason = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemResolutionNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemResolutionDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGroupLiquidationDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemLiquidationDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlyGroupMessageLiquidation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemMessageLiquidation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlbEmployeeName = New System.Windows.Forms.Label()
        Me.CtrNavigation1 = New Presentation.Controls.CtrNavigation()
        Me.INDlcgContractLiquidation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEmployeeBack = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPanelLiquidation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEmployeeName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGroupHumanTalent = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEmployees = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemHumanTalent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgHumanTalent = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrdHumanTalent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvHumanTalent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepRetirementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepRetirementDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepRetirementReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepDeleteAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvLiquidations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvNovelties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtrContractLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtrContractLiquidation.SuspendLayout()
        CType(Me.INDPopUpContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPopUpContainerControl2.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDteResultFormulaEmployer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEmployees.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvSleEmployees, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.INDlyControlEmployeeLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyControlEmployeeLiquidation.SuspendLayout()
        CType(Me.INDGcEmployerLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeResolutionDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeResolutionDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtResolutionNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtRetirementReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcLiquidationDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPopupContainerControl1.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDteResultFormula.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyfUsedFormula1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyfReplaceFormula1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyfResultFormula1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTotalPaid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPosition.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSalaryBase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtEmployee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupGeneralImformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalPaid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInitialDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSalaryBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRetirementReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemResolutionNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemResolutionDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupLiquidationDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLiquidationDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupMessageLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemMessageLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgContractLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEmployeeBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPanelLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupHumanTalent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEmployees, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHumanTalent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgHumanTalent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCtrContractLiquidation)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1154, 605)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Controls.Add(Me.INDPopupContainerControl1)
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1154, 130)
        Me.ToolBars.Controls.SetChildIndex(Me.BarraBotones, 0)
        Me.ToolBars.Controls.SetChildIndex(Me.INDPopupContainerControl1, 0)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1154, 130)
        '
        'INDgrvContract
        '
        Me.INDgrvContract.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvContract.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvContract.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvContract.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvContract.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvContract.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvContract.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvContract.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvContract.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvContract.Appearance.Row.Options.UseFont = True
        Me.INDgrvContract.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 15.0!)
        Me.INDgrvContract.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgrvContract.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcInitialContractNumber, Me.INDgrcContractNumber, Me.INDgrcContractCompany, Me.INDgrcContractGroup, Me.INDgrcFunctionalUnit, Me.INDgrcContractPosition, Me.INDgrcBasicSalary, Me.INDgrcContractInitialDate, Me.INDgrcContractEndingDate})
        Me.INDgrvContract.GridControl = Me.INDgrdHumanTalent
        Me.INDgrvContract.GroupCount = 1
        Me.INDgrvContract.Name = "INDgrvContract"
        Me.INDgrvContract.OptionsView.BestFitMaxRowCount = 100
        Me.INDgrvContract.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvContract.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvContract.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvContract.OptionsView.ShowGroupPanel = False
        Me.INDgrvContract.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDgrcInitialContractNumber, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDgrcContractNumber, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvContract, False)
        Me.INDgrvContract.ViewCaption = "Contratos"
        '
        'INDgrcInitialContractNumber
        '
        Me.INDgrcInitialContractNumber.Caption = "Contrato Inicial"
        Me.INDgrcInitialContractNumber.FieldName = "InitialContractNumber"
        Me.INDgrcInitialContractNumber.Name = "INDgrcInitialContractNumber"
        Me.INDgrcInitialContractNumber.OptionsColumn.AllowEdit = False
        Me.INDgrcInitialContractNumber.OptionsColumn.AllowMove = False
        Me.INDgrcInitialContractNumber.Visible = True
        Me.INDgrcInitialContractNumber.VisibleIndex = 0
        '
        'INDgrcContractNumber
        '
        Me.INDgrcContractNumber.Caption = "No. Contrato"
        Me.INDgrcContractNumber.FieldName = "Id"
        Me.INDgrcContractNumber.Name = "INDgrcContractNumber"
        Me.INDgrcContractNumber.OptionsColumn.AllowEdit = False
        Me.INDgrcContractNumber.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.INDgrcContractNumber.Visible = True
        Me.INDgrcContractNumber.VisibleIndex = 0
        Me.INDgrcContractNumber.Width = 112
        '
        'INDgrcContractCompany
        '
        Me.INDgrcContractCompany.Caption = "Empresa"
        Me.INDgrcContractCompany.FieldName = "Group.Company.Name"
        Me.INDgrcContractCompany.Name = "INDgrcContractCompany"
        Me.INDgrcContractCompany.OptionsColumn.AllowEdit = False
        Me.INDgrcContractCompany.Visible = True
        Me.INDgrcContractCompany.VisibleIndex = 1
        Me.INDgrcContractCompany.Width = 218
        '
        'INDgrcContractGroup
        '
        Me.INDgrcContractGroup.Caption = "Grupo"
        Me.INDgrcContractGroup.FieldName = "Group.Name"
        Me.INDgrcContractGroup.Name = "INDgrcContractGroup"
        Me.INDgrcContractGroup.OptionsColumn.AllowEdit = False
        Me.INDgrcContractGroup.Visible = True
        Me.INDgrcContractGroup.VisibleIndex = 2
        Me.INDgrcContractGroup.Width = 133
        '
        'INDgrcFunctionalUnit
        '
        Me.INDgrcFunctionalUnit.Caption = "Unidad Funcional"
        Me.INDgrcFunctionalUnit.FieldName = "FunctionalUnit.Name"
        Me.INDgrcFunctionalUnit.Name = "INDgrcFunctionalUnit"
        Me.INDgrcFunctionalUnit.OptionsColumn.AllowEdit = False
        Me.INDgrcFunctionalUnit.Visible = True
        Me.INDgrcFunctionalUnit.VisibleIndex = 3
        Me.INDgrcFunctionalUnit.Width = 165
        '
        'INDgrcContractPosition
        '
        Me.INDgrcContractPosition.Caption = "Cargo"
        Me.INDgrcContractPosition.FieldName = "Position.Name"
        Me.INDgrcContractPosition.Name = "INDgrcContractPosition"
        Me.INDgrcContractPosition.OptionsColumn.AllowEdit = False
        Me.INDgrcContractPosition.Visible = True
        Me.INDgrcContractPosition.VisibleIndex = 4
        Me.INDgrcContractPosition.Width = 208
        '
        'INDgrcBasicSalary
        '
        Me.INDgrcBasicSalary.Caption = "Salario Básico"
        Me.INDgrcBasicSalary.DisplayFormat.FormatString = "C2"
        Me.INDgrcBasicSalary.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcBasicSalary.FieldName = "BasicSalary"
        Me.INDgrcBasicSalary.Name = "INDgrcBasicSalary"
        Me.INDgrcBasicSalary.OptionsColumn.AllowEdit = False
        Me.INDgrcBasicSalary.Visible = True
        Me.INDgrcBasicSalary.VisibleIndex = 5
        Me.INDgrcBasicSalary.Width = 154
        '
        'INDgrcContractInitialDate
        '
        Me.INDgrcContractInitialDate.Caption = "Fecha Inicial"
        Me.INDgrcContractInitialDate.DisplayFormat.FormatString = "MMMM dd / yyyy"
        Me.INDgrcContractInitialDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcContractInitialDate.FieldName = "ContractInitialDate"
        Me.INDgrcContractInitialDate.Name = "INDgrcContractInitialDate"
        Me.INDgrcContractInitialDate.OptionsColumn.AllowEdit = False
        Me.INDgrcContractInitialDate.Visible = True
        Me.INDgrcContractInitialDate.VisibleIndex = 6
        Me.INDgrcContractInitialDate.Width = 117
        '
        'INDgrcContractEndingDate
        '
        Me.INDgrcContractEndingDate.Caption = "Fecha Final"
        Me.INDgrcContractEndingDate.DisplayFormat.FormatString = "MMMM dd / yyyy"
        Me.INDgrcContractEndingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcContractEndingDate.FieldName = "ContractEndingDate"
        Me.INDgrcContractEndingDate.Name = "INDgrcContractEndingDate"
        Me.INDgrcContractEndingDate.OptionsColumn.AllowEdit = False
        Me.INDgrcContractEndingDate.Visible = True
        Me.INDgrcContractEndingDate.VisibleIndex = 7
        Me.INDgrcContractEndingDate.Width = 143
        '
        'INDgrdHumanTalent
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgrdHumanTalent, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgrdHumanTalent, Nothing)
        Me.INDgrdHumanTalent.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgrdHumanTalent, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgrdHumanTalent, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgrdHumanTalent, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgrdHumanTalent, False)
        GridLevelNode1.LevelTemplate = Me.INDgrvContract
        GridLevelNode2.LevelTemplate = Me.INDgrvLiquidations
        GridLevelNode2.RelationName = "Liquidations"
        GridLevelNode3.LevelTemplate = Me.INDgrvNovelties
        GridLevelNode3.RelationName = "Novelties"
        GridLevelNode1.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2, GridLevelNode3})
        GridLevelNode1.RelationName = "Contract"
        GridLevelNode4.RelationName = "Totals"
        Me.INDgrdHumanTalent.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1, GridLevelNode4})
        Me.INDgrdHumanTalent.Location = New System.Drawing.Point(24, 89)
        Me.INDgrdHumanTalent.MainView = Me.INDgrvHumanTalent
        Me.INDgrdHumanTalent.Name = "INDgrdHumanTalent"
        Me.INDgrdHumanTalent.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepRetirementDate, Me.INDrepDeleteAction, Me.INDrepConfirm, Me.INDrepRetirementReason})
        Me.INDgrdHumanTalent.Size = New System.Drawing.Size(1102, 180)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgrdHumanTalent, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgrdHumanTalent.TabIndex = 19
        Me.INDgrdHumanTalent.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgrvHumanTalent, Me.INDgrvLiquidations, Me.INDgrvNovelties, Me.INDgrvContract})
        '
        'INDgrvHumanTalent
        '
        Me.INDgrvHumanTalent.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvHumanTalent.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvHumanTalent.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgrvHumanTalent.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvHumanTalent.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvHumanTalent.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvHumanTalent.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvHumanTalent.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvHumanTalent.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvHumanTalent.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvHumanTalent.Appearance.Row.Options.UseFont = True
        Me.INDgrvHumanTalent.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgrvHumanTalent.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgrvHumanTalent.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcEmloyeeId, Me.INDgrcEmployeeNames, Me.INDgrcRetirementDate, Me.INDgrcRetirementReason, Me.INDgrcActions})
        Me.INDgrvHumanTalent.GridControl = Me.INDgrdHumanTalent
        Me.INDgrvHumanTalent.Name = "INDgrvHumanTalent"
        Me.INDgrvHumanTalent.OptionsView.BestFitMaxRowCount = 100
        Me.INDgrvHumanTalent.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvHumanTalent.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvHumanTalent.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvHumanTalent.OptionsView.ShowGroupPanel = False
        Me.INDgrvHumanTalent.Tag = 451
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvHumanTalent, False)
        '
        'INDgrcEmloyeeId
        '
        Me.INDgrcEmloyeeId.Caption = "Identificación"
        Me.INDgrcEmloyeeId.FieldName = "ThirdParty.Nit"
        Me.INDgrcEmloyeeId.Name = "INDgrcEmloyeeId"
        Me.INDgrcEmloyeeId.OptionsColumn.AllowEdit = False
        Me.INDgrcEmloyeeId.Visible = True
        Me.INDgrcEmloyeeId.VisibleIndex = 0
        Me.INDgrcEmloyeeId.Width = 126
        '
        'INDgrcEmployeeNames
        '
        Me.INDgrcEmployeeNames.Caption = "Nombre"
        Me.INDgrcEmployeeNames.FieldName = "ThirdParty.Name"
        Me.INDgrcEmployeeNames.Name = "INDgrcEmployeeNames"
        Me.INDgrcEmployeeNames.OptionsColumn.AllowEdit = False
        Me.INDgrcEmployeeNames.Visible = True
        Me.INDgrcEmployeeNames.VisibleIndex = 1
        Me.INDgrcEmployeeNames.Width = 250
        '
        'INDgrcRetirementDate
        '
        Me.INDgrcRetirementDate.Caption = "Fecha de Retiro"
        Me.INDgrcRetirementDate.ColumnEdit = Me.INDrepRetirementDate
        Me.INDgrcRetirementDate.FieldName = "None"
        Me.INDgrcRetirementDate.Name = "INDgrcRetirementDate"
        Me.INDgrcRetirementDate.UnboundType = DevExpress.Data.UnboundColumnType.[String]
        Me.INDgrcRetirementDate.Visible = True
        Me.INDgrcRetirementDate.VisibleIndex = 2
        Me.INDgrcRetirementDate.Width = 209
        '
        'INDrepRetirementDate
        '
        Me.INDrepRetirementDate.AutoHeight = False
        Me.INDrepRetirementDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepRetirementDate.Name = "INDrepRetirementDate"
        Me.INDrepRetirementDate.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'INDgrcRetirementReason
        '
        Me.INDgrcRetirementReason.Caption = "Motivo de Retiro"
        Me.INDgrcRetirementReason.ColumnEdit = Me.INDrepRetirementReason
        Me.INDgrcRetirementReason.FieldName = "RetirementReason"
        Me.INDgrcRetirementReason.Name = "INDgrcRetirementReason"
        Me.INDgrcRetirementReason.UnboundType = DevExpress.Data.UnboundColumnType.[Integer]
        Me.INDgrcRetirementReason.Visible = True
        Me.INDgrcRetirementReason.VisibleIndex = 3
        Me.INDgrcRetirementReason.Width = 360
        '
        'INDrepRetirementReason
        '
        Me.INDrepRetirementReason.AutoHeight = False
        Me.INDrepRetirementReason.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrepRetirementReason.DisplayMember = "Name"
        Me.INDrepRetirementReason.Name = "INDrepRetirementReason"
        Me.INDrepRetirementReason.NullText = ""
        Me.INDrepRetirementReason.PopupView = Me.RepositoryItemGridLookUpEdit1View
        Me.INDrepRetirementReason.ValueMember = "Id"
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemGridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RepositoryItemGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemGridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcRRCode, Me.INDgrcRRName})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemGridLookUpEdit1View, False)
        '
        'INDgrcRRCode
        '
        Me.INDgrcRRCode.Caption = "Código"
        Me.INDgrcRRCode.FieldName = "Code"
        Me.INDgrcRRCode.Name = "INDgrcRRCode"
        Me.INDgrcRRCode.Visible = True
        Me.INDgrcRRCode.VisibleIndex = 0
        '
        'INDgrcRRName
        '
        Me.INDgrcRRName.Caption = "Descripción"
        Me.INDgrcRRName.FieldName = "Name"
        Me.INDgrcRRName.Name = "INDgrcRRName"
        Me.INDgrcRRName.Visible = True
        Me.INDgrcRRName.VisibleIndex = 1
        '
        'INDgrcActions
        '
        Me.INDgrcActions.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Underline)
        Me.INDgrcActions.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgrcActions.AppearanceCell.Options.UseFont = True
        Me.INDgrcActions.AppearanceCell.Options.UseForeColor = True
        Me.INDgrcActions.AppearanceCell.Options.UseTextOptions = True
        Me.INDgrcActions.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDgrcActions.Caption = "Acciones"
        Me.INDgrcActions.ColumnEdit = Me.INDrepDeleteAction
        Me.INDgrcActions.Name = "INDgrcActions"
        Me.INDgrcActions.Visible = True
        Me.INDgrcActions.VisibleIndex = 4
        Me.INDgrcActions.Width = 146
        '
        'INDrepDeleteAction
        '
        Me.INDrepDeleteAction.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Underline)
        Me.INDrepDeleteAction.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDrepDeleteAction.Appearance.Options.UseFont = True
        Me.INDrepDeleteAction.Appearance.Options.UseForeColor = True
        Me.INDrepDeleteAction.Appearance.Options.UseTextOptions = True
        Me.INDrepDeleteAction.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepDeleteAction.AutoHeight = False
        Me.INDrepDeleteAction.Name = "INDrepDeleteAction"
        Me.INDrepDeleteAction.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'INDrepConfirm
        '
        Me.INDrepConfirm.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Si"), New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No")})
        Me.INDrepConfirm.Name = "INDrepConfirm"
        '
        'INDgrvLiquidations
        '
        Me.INDgrvLiquidations.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvLiquidations.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvLiquidations.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvLiquidations.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvLiquidations.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvLiquidations.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvLiquidations.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvLiquidations.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvLiquidations.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvLiquidations.Appearance.Row.Options.UseFont = True
        Me.INDgrvLiquidations.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 15.0!)
        Me.INDgrvLiquidations.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgrvLiquidations.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcLiquidationPeriod, Me.INDgrcLiquidationTotalAccrued, Me.INDgrcLiquidationTotalDeducted, Me.INDgrcLiquidationTotalPaid, Me.INDgrcLiquidationPeriodIBC})
        Me.INDgrvLiquidations.GridControl = Me.INDgrdHumanTalent
        Me.INDgrvLiquidations.Name = "INDgrvLiquidations"
        Me.INDgrvLiquidations.OptionsView.BestFitMaxRowCount = 100
        Me.INDgrvLiquidations.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvLiquidations.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvLiquidations.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvLiquidations.OptionsView.ShowDetailButtons = False
        Me.INDgrvLiquidations.OptionsView.ShowFooter = True
        Me.INDgrvLiquidations.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvLiquidations, False)
        Me.INDgrvLiquidations.ViewCaption = "Nominas"
        '
        'INDgrcLiquidationPeriod
        '
        Me.INDgrcLiquidationPeriod.Caption = "Periodo"
        Me.INDgrcLiquidationPeriod.DisplayFormat.FormatString = "yyyy - MMMM"
        Me.INDgrcLiquidationPeriod.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcLiquidationPeriod.FieldName = "PayrollDateLiquidated"
        Me.INDgrcLiquidationPeriod.Name = "INDgrcLiquidationPeriod"
        Me.INDgrcLiquidationPeriod.OptionsColumn.AllowEdit = False
        Me.INDgrcLiquidationPeriod.Visible = True
        Me.INDgrcLiquidationPeriod.VisibleIndex = 0
        '
        'INDgrcLiquidationTotalAccrued
        '
        Me.INDgrcLiquidationTotalAccrued.Caption = "Total Devengado"
        Me.INDgrcLiquidationTotalAccrued.DisplayFormat.FormatString = "C2"
        Me.INDgrcLiquidationTotalAccrued.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcLiquidationTotalAccrued.FieldName = "TotalAccrued"
        Me.INDgrcLiquidationTotalAccrued.Name = "INDgrcLiquidationTotalAccrued"
        Me.INDgrcLiquidationTotalAccrued.OptionsColumn.AllowEdit = False
        Me.INDgrcLiquidationTotalAccrued.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalAccrued", "{0:C2}")})
        Me.INDgrcLiquidationTotalAccrued.Visible = True
        Me.INDgrcLiquidationTotalAccrued.VisibleIndex = 1
        '
        'INDgrcLiquidationTotalDeducted
        '
        Me.INDgrcLiquidationTotalDeducted.Caption = "Total Deducido"
        Me.INDgrcLiquidationTotalDeducted.DisplayFormat.FormatString = "C2"
        Me.INDgrcLiquidationTotalDeducted.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcLiquidationTotalDeducted.FieldName = "TotalDeducted"
        Me.INDgrcLiquidationTotalDeducted.Name = "INDgrcLiquidationTotalDeducted"
        Me.INDgrcLiquidationTotalDeducted.OptionsColumn.AllowEdit = False
        Me.INDgrcLiquidationTotalDeducted.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalDeducted", "{0:C2}")})
        Me.INDgrcLiquidationTotalDeducted.Visible = True
        Me.INDgrcLiquidationTotalDeducted.VisibleIndex = 2
        '
        'INDgrcLiquidationTotalPaid
        '
        Me.INDgrcLiquidationTotalPaid.Caption = "Total Pagado"
        Me.INDgrcLiquidationTotalPaid.DisplayFormat.FormatString = "C2"
        Me.INDgrcLiquidationTotalPaid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcLiquidationTotalPaid.FieldName = "TotalPaid"
        Me.INDgrcLiquidationTotalPaid.Name = "INDgrcLiquidationTotalPaid"
        Me.INDgrcLiquidationTotalPaid.OptionsColumn.AllowEdit = False
        Me.INDgrcLiquidationTotalPaid.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPaid", "{0:C2}")})
        Me.INDgrcLiquidationTotalPaid.Visible = True
        Me.INDgrcLiquidationTotalPaid.VisibleIndex = 3
        '
        'INDgrcLiquidationPeriodIBC
        '
        Me.INDgrcLiquidationPeriodIBC.Caption = "IBC Periodo"
        Me.INDgrcLiquidationPeriodIBC.DisplayFormat.FormatString = "C2"
        Me.INDgrcLiquidationPeriodIBC.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcLiquidationPeriodIBC.FieldName = "PeriodJCB"
        Me.INDgrcLiquidationPeriodIBC.Name = "INDgrcLiquidationPeriodIBC"
        Me.INDgrcLiquidationPeriodIBC.OptionsColumn.AllowEdit = False
        Me.INDgrcLiquidationPeriodIBC.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PeriodJCB", "{0:C2}")})
        Me.INDgrcLiquidationPeriodIBC.Visible = True
        Me.INDgrcLiquidationPeriodIBC.VisibleIndex = 4
        '
        'INDgrvNovelties
        '
        Me.INDgrvNovelties.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvNovelties.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvNovelties.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvNovelties.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvNovelties.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvNovelties.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvNovelties.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvNovelties.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvNovelties.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvNovelties.Appearance.Row.Options.UseFont = True
        Me.INDgrvNovelties.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 15.0!)
        Me.INDgrvNovelties.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgrvNovelties.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcNoveltiesPeriod, Me.INDgrcPayrollDays, Me.INDgrcWorkedDays, Me.INDgrcDisabilityDays, Me.INDgrcVacationDays, Me.INDgrcSanctionDays, Me.INDgrcPermissionDays, Me.INDgrcLicenseDays, Me.INDgrcQuoteHealthDays})
        Me.INDgrvNovelties.GridControl = Me.INDgrdHumanTalent
        Me.INDgrvNovelties.Name = "INDgrvNovelties"
        Me.INDgrvNovelties.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvNovelties.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvNovelties.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvNovelties.OptionsView.ShowDetailButtons = False
        Me.INDgrvNovelties.OptionsView.ShowFooter = True
        Me.INDgrvNovelties.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvNovelties, False)
        Me.INDgrvNovelties.ViewCaption = "Novedades"
        '
        'INDgrcNoveltiesPeriod
        '
        Me.INDgrcNoveltiesPeriod.Caption = "Periodo"
        Me.INDgrcNoveltiesPeriod.DisplayFormat.FormatString = "yyyy - MMMM"
        Me.INDgrcNoveltiesPeriod.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcNoveltiesPeriod.FieldName = "PayrollDateLiquidated"
        Me.INDgrcNoveltiesPeriod.Name = "INDgrcNoveltiesPeriod"
        Me.INDgrcNoveltiesPeriod.OptionsColumn.AllowEdit = False
        Me.INDgrcNoveltiesPeriod.Visible = True
        Me.INDgrcNoveltiesPeriod.VisibleIndex = 0
        '
        'INDgrcPayrollDays
        '
        Me.INDgrcPayrollDays.Caption = "Días Nomina"
        Me.INDgrcPayrollDays.FieldName = "PayrollDays"
        Me.INDgrcPayrollDays.Name = "INDgrcPayrollDays"
        Me.INDgrcPayrollDays.OptionsColumn.AllowEdit = False
        Me.INDgrcPayrollDays.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PayrollDays", "{0:0 Día(s)}")})
        Me.INDgrcPayrollDays.Visible = True
        Me.INDgrcPayrollDays.VisibleIndex = 1
        '
        'INDgrcWorkedDays
        '
        Me.INDgrcWorkedDays.Caption = "Días Trabajados"
        Me.INDgrcWorkedDays.FieldName = "DaysWorked"
        Me.INDgrcWorkedDays.Name = "INDgrcWorkedDays"
        Me.INDgrcWorkedDays.OptionsColumn.AllowEdit = False
        Me.INDgrcWorkedDays.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DaysWorked", "{0:0 Día(s)}")})
        Me.INDgrcWorkedDays.Visible = True
        Me.INDgrcWorkedDays.VisibleIndex = 2
        '
        'INDgrcDisabilityDays
        '
        Me.INDgrcDisabilityDays.Caption = "Días de Incapacidad"
        Me.INDgrcDisabilityDays.FieldName = "DisabilityDays"
        Me.INDgrcDisabilityDays.Name = "INDgrcDisabilityDays"
        Me.INDgrcDisabilityDays.OptionsColumn.AllowEdit = False
        Me.INDgrcDisabilityDays.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DisabilityDays", "{0:0 Día(s)}")})
        Me.INDgrcDisabilityDays.Visible = True
        Me.INDgrcDisabilityDays.VisibleIndex = 3
        '
        'INDgrcVacationDays
        '
        Me.INDgrcVacationDays.Caption = "Días de Vacaciones"
        Me.INDgrcVacationDays.FieldName = "VacationDays"
        Me.INDgrcVacationDays.Name = "INDgrcVacationDays"
        Me.INDgrcVacationDays.OptionsColumn.AllowEdit = False
        Me.INDgrcVacationDays.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "VacationDays", "{0:0 Día(s)}")})
        Me.INDgrcVacationDays.Visible = True
        Me.INDgrcVacationDays.VisibleIndex = 4
        '
        'INDgrcSanctionDays
        '
        Me.INDgrcSanctionDays.Caption = "Días de Sanción"
        Me.INDgrcSanctionDays.FieldName = "SanctionDays"
        Me.INDgrcSanctionDays.Name = "INDgrcSanctionDays"
        Me.INDgrcSanctionDays.OptionsColumn.AllowEdit = False
        Me.INDgrcSanctionDays.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SanctionDays", "{0:0 Día(s)}")})
        Me.INDgrcSanctionDays.Visible = True
        Me.INDgrcSanctionDays.VisibleIndex = 5
        '
        'INDgrcPermissionDays
        '
        Me.INDgrcPermissionDays.Caption = "Diás de Permiso"
        Me.INDgrcPermissionDays.FieldName = "PermissionDays"
        Me.INDgrcPermissionDays.Name = "INDgrcPermissionDays"
        Me.INDgrcPermissionDays.OptionsColumn.AllowEdit = False
        Me.INDgrcPermissionDays.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PermissionDays", "{0:0 Día(s)}")})
        Me.INDgrcPermissionDays.Visible = True
        Me.INDgrcPermissionDays.VisibleIndex = 6
        '
        'INDgrcLicenseDays
        '
        Me.INDgrcLicenseDays.Caption = "Días de Licencia"
        Me.INDgrcLicenseDays.FieldName = "LicenseDays"
        Me.INDgrcLicenseDays.Name = "INDgrcLicenseDays"
        Me.INDgrcLicenseDays.OptionsColumn.AllowEdit = False
        Me.INDgrcLicenseDays.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LicenseDays", "{0:0 Día(s)}")})
        Me.INDgrcLicenseDays.Visible = True
        Me.INDgrcLicenseDays.VisibleIndex = 7
        '
        'INDgrcQuoteHealthDays
        '
        Me.INDgrcQuoteHealthDays.Caption = "Días de Salud"
        Me.INDgrcQuoteHealthDays.FieldName = "QuoteHealthDays"
        Me.INDgrcQuoteHealthDays.Name = "INDgrcQuoteHealthDays"
        Me.INDgrcQuoteHealthDays.OptionsColumn.AllowEdit = False
        Me.INDgrcQuoteHealthDays.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QuoteHealthDays", "{0:0 Día(s)}")})
        Me.INDgrcQuoteHealthDays.Visible = True
        Me.INDgrcQuoteHealthDays.VisibleIndex = 8
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        '
        'INDlyCtrContractLiquidation
        '
        Me.INDlyCtrContractLiquidation.Controls.Add(Me.INDPopUpContainerControl2)
        Me.INDlyCtrContractLiquidation.Controls.Add(Me.PopupContainerControl1)
        Me.INDlyCtrContractLiquidation.Controls.Add(Me.INDgrdHumanTalent)
        Me.INDlyCtrContractLiquidation.Controls.Add(Me.INDbtnAddEmployee)
        Me.INDlyCtrContractLiquidation.Controls.Add(Me.INDsleEmployees)
        Me.INDlyCtrContractLiquidation.Controls.Add(Me.PanelControl1)
        Me.INDlyCtrContractLiquidation.Controls.Add(Me.INDlbEmployeeName)
        Me.INDlyCtrContractLiquidation.Controls.Add(Me.CtrNavigation1)
        Me.INDlyCtrContractLiquidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyCtrContractLiquidation.Location = New System.Drawing.Point(2, 8)
        Me.INDlyCtrContractLiquidation.Name = "INDlyCtrContractLiquidation"
        Me.INDlyCtrContractLiquidation.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(480, 503, 250, 350)
        Me.INDlyCtrContractLiquidation.Root = Me.INDlcgContractLiquidation
        Me.INDlyCtrContractLiquidation.Size = New System.Drawing.Size(1150, 595)
        Me.INDlyCtrContractLiquidation.TabIndex = 1
        Me.INDlyCtrContractLiquidation.Text = "LayoutControl1"
        '
        'INDPopUpContainerControl2
        '
        Me.INDPopUpContainerControl2.Controls.Add(Me.LayoutControl1)
        Me.INDPopUpContainerControl2.Location = New System.Drawing.Point(653, 140)
        Me.INDPopUpContainerControl2.Name = "INDPopUpContainerControl2"
        Me.INDPopUpContainerControl2.Size = New System.Drawing.Size(422, 313)
        Me.INDPopUpContainerControl2.TabIndex = 56
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDteResultFormulaEmployer)
        Me.LayoutControl1.Controls.Add(Me.INDteReplaceFormulaEmployer)
        Me.LayoutControl1.Controls.Add(Me.INDteUsedFormulaEmployer)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup4
        Me.LayoutControl1.Size = New System.Drawing.Size(422, 313)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDteResultFormulaEmployer
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteResultFormulaEmployer, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteResultFormulaEmployer, False)
        Me.INDteResultFormulaEmployer.Location = New System.Drawing.Point(111, 261)
        Me.IndigoTextEdit1.SetMascara(Me.INDteResultFormulaEmployer, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteResultFormulaEmployer.Name = "INDteResultFormulaEmployer"
        Me.INDteResultFormulaEmployer.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteResultFormulaEmployer.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteResultFormulaEmployer.Properties.Appearance.Options.UseBackColor = True
        Me.INDteResultFormulaEmployer.Properties.Appearance.Options.UseFont = True
        Me.INDteResultFormulaEmployer.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteResultFormulaEmployer.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteResultFormulaEmployer.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDteResultFormulaEmployer.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteResultFormulaEmployer.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteResultFormulaEmployer.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteResultFormulaEmployer.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteResultFormulaEmployer.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteResultFormulaEmployer.Properties.Mask.EditMask = "c2"
        Me.INDteResultFormulaEmployer.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteResultFormulaEmployer.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteResultFormulaEmployer.Properties.ReadOnly = True
        Me.INDteResultFormulaEmployer.Size = New System.Drawing.Size(287, 28)
        Me.INDteResultFormulaEmployer.StyleController = Me.LayoutControl1
        Me.INDteResultFormulaEmployer.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteResultFormulaEmployer, 0)
        '
        'INDteReplaceFormulaEmployer
        '
        Me.INDteReplaceFormulaEmployer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.INDteReplaceFormulaEmployer.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDteReplaceFormulaEmployer.Location = New System.Drawing.Point(111, 157)
        Me.INDteReplaceFormulaEmployer.Multiline = True
        Me.INDteReplaceFormulaEmployer.Name = "INDteReplaceFormulaEmployer"
        Me.INDteReplaceFormulaEmployer.ReadOnly = True
        Me.INDteReplaceFormulaEmployer.Size = New System.Drawing.Size(287, 100)
        Me.INDteReplaceFormulaEmployer.TabIndex = 1
        '
        'INDteUsedFormulaEmployer
        '
        Me.INDteUsedFormulaEmployer.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.INDteUsedFormulaEmployer.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDteUsedFormulaEmployer.Location = New System.Drawing.Point(111, 53)
        Me.INDteUsedFormulaEmployer.Multiline = True
        Me.INDteUsedFormulaEmployer.Name = "INDteUsedFormulaEmployer"
        Me.INDteUsedFormulaEmployer.ReadOnly = True
        Me.INDteUsedFormulaEmployer.Size = New System.Drawing.Size(287, 100)
        Me.INDteUsedFormulaEmployer.TabIndex = 0
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
        Me.LayoutControlGroup4.CustomizationFormText = "LayoutControlGroup7"
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup5})
        Me.LayoutControlGroup4.Name = "Root"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(422, 313)
        Me.LayoutControlGroup4.TextVisible = False
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
        Me.LayoutControlGroup5.CustomizationFormText = "Fórmulas"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup8"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(402, 293)
        Me.LayoutControlGroup5.Text = "Fórmulas"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDteUsedFormulaEmployer
        Me.LayoutControlItem2.CustomizationFormText = "Usada"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(92, 100)
        Me.LayoutControlItem2.Name = "INDlyfUsedFormula1"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(378, 104)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Usada"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(84, 17)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDteReplaceFormulaEmployer
        Me.LayoutControlItem3.CustomizationFormText = "Reemplazada"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 104)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(92, 100)
        Me.LayoutControlItem3.Name = "INDlyfReplaceFormula1"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(378, 104)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Reemplazada"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(84, 17)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDteResultFormulaEmployer
        Me.LayoutControlItem4.CustomizationFormText = "Resultado"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 208)
        Me.LayoutControlItem4.Name = "INDlyfResultFormula1"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(378, 32)
        Me.LayoutControlItem4.Text = "Resultado"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(84, 17)
        '
        'PopupContainerControl1
        '
        Me.PopupContainerControl1.Location = New System.Drawing.Point(1141, 375)
        Me.PopupContainerControl1.Name = "PopupContainerControl1"
        Me.PopupContainerControl1.Size = New System.Drawing.Size(200, 100)
        Me.PopupContainerControl1.TabIndex = 79
        '
        'INDbtnAddEmployee
        '
        Me.INDbtnAddEmployee.Location = New System.Drawing.Point(414, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddEmployee, False)
        Me.INDbtnAddEmployee.Name = "INDbtnAddEmployee"
        Me.INDbtnAddEmployee.Size = New System.Drawing.Size(196, 28)
        Me.INDbtnAddEmployee.StyleController = Me.INDlyCtrContractLiquidation
        Me.INDbtnAddEmployee.TabIndex = 18
        Me.INDbtnAddEmployee.Text = "Agregar Empleado"
        '
        'INDsleEmployees
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEmployees, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEmployees, False)
        Me.INDsleEmployees.EnterMoveNextControl = True
        Me.INDsleEmployees.Location = New System.Drawing.Point(171, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEmployees, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEmployees.Name = "INDsleEmployees"
        Me.INDsleEmployees.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEmployees.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEmployees.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEmployees.Properties.Appearance.Options.UseFont = True
        Me.INDsleEmployees.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleEmployees.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleEmployees.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEmployees.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleEmployees.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleEmployees.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEmployees.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEmployees.Properties.DisplayMember = "Name"
        Me.INDsleEmployees.Properties.NullText = ""
        Me.INDsleEmployees.Properties.PopupFormMinSize = New System.Drawing.Size(700, 0)
        Me.INDsleEmployees.Properties.PopupView = Me.INDgrvSleEmployees
        Me.INDsleEmployees.Properties.ValueMember = "Id"
        Me.INDsleEmployees.Size = New System.Drawing.Size(239, 28)
        Me.INDsleEmployees.StyleController = Me.INDlyCtrContractLiquidation
        Me.INDsleEmployees.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEmployees, 0)
        '
        'INDgrvSleEmployees
        '
        Me.INDgrvSleEmployees.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvSleEmployees.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvSleEmployees.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvSleEmployees.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvSleEmployees.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvSleEmployees.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvSleEmployees.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvSleEmployees.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvSleEmployees.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvSleEmployees.Appearance.Row.Options.UseFont = True
        Me.INDgrvSleEmployees.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcEmployeeNit, Me.INDgrcEmployeeName, Me.INDgrcEmployee})
        Me.INDgrvSleEmployees.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgrvSleEmployees.Name = "INDgrvSleEmployees"
        Me.INDgrvSleEmployees.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgrvSleEmployees.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvSleEmployees.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvSleEmployees.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvSleEmployees.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvSleEmployees, False)
        '
        'INDgrcEmployeeNit
        '
        Me.INDgrcEmployeeNit.Caption = "Identificación"
        Me.INDgrcEmployeeNit.FieldName = "Nit"
        Me.INDgrcEmployeeNit.Name = "INDgrcEmployeeNit"
        Me.INDgrcEmployeeNit.Visible = True
        Me.INDgrcEmployeeNit.VisibleIndex = 0
        Me.INDgrcEmployeeNit.Width = 188
        '
        'INDgrcEmployeeName
        '
        Me.INDgrcEmployeeName.Caption = "Nombre"
        Me.INDgrcEmployeeName.FieldName = "Name"
        Me.INDgrcEmployeeName.Name = "INDgrcEmployeeName"
        Me.INDgrcEmployeeName.Visible = True
        Me.INDgrcEmployeeName.VisibleIndex = 1
        Me.INDgrcEmployeeName.Width = 334
        '
        'INDgrcEmployee
        '
        Me.INDgrcEmployee.Caption = "Grupo"
        Me.INDgrcEmployee.FieldName = "Descripcion"
        Me.INDgrcEmployee.Name = "INDgrcEmployee"
        Me.INDgrcEmployee.Visible = True
        Me.INDgrcEmployee.VisibleIndex = 2
        Me.INDgrcEmployee.Width = 340
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDlyControlEmployeeLiquidation)
        Me.PanelControl1.Controls.Add(Me.CtrNavigationControl1)
        Me.PanelControl1.Location = New System.Drawing.Point(12, 349)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1126, 234)
        Me.PanelControl1.TabIndex = 78
        '
        'INDlyControlEmployeeLiquidation
        '
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDGcEmployerLiquidation)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDDeResolutionDate)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtResolutionNumber)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDgcMessage)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtRetirementReason)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDgcLiquidationDetail)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtTotalPaid)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtPosition)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtSalaryBase)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtEndDate)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtInitialDate)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtEmployee)
        Me.INDlyControlEmployeeLiquidation.Controls.Add(Me.INDtxtGroup)
        Me.INDlyControlEmployeeLiquidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyControlEmployeeLiquidation.Location = New System.Drawing.Point(202, 2)
        Me.INDlyControlEmployeeLiquidation.Name = "INDlyControlEmployeeLiquidation"
        Me.INDlyControlEmployeeLiquidation.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(758, 467, 250, 350)
        Me.INDlyControlEmployeeLiquidation.Root = Me.LayoutControlGroup1
        Me.INDlyControlEmployeeLiquidation.Size = New System.Drawing.Size(922, 230)
        Me.INDlyControlEmployeeLiquidation.TabIndex = 1
        Me.INDlyControlEmployeeLiquidation.Text = "LayoutControl1"
        '
        'INDGcEmployerLiquidation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcEmployerLiquidation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcEmployerLiquidation, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcEmployerLiquidation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcEmployerLiquidation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcEmployerLiquidation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcEmployerLiquidation, False)
        Me.INDGcEmployerLiquidation.Location = New System.Drawing.Point(1172, -163)
        Me.INDGcEmployerLiquidation.MainView = Me.GridView2
        Me.INDGcEmployerLiquidation.Name = "INDGcEmployerLiquidation"
        Me.INDGcEmployerLiquidation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit2})
        Me.INDGcEmployerLiquidation.Size = New System.Drawing.Size(696, 352)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcEmployerLiquidation, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcEmployerLiquidation.TabIndex = 16
        Me.INDGcEmployerLiquidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView2})
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView2.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGcNameConcept, Me.INDGcValue, Me.INDColActions2})
        Me.GridView2.GridControl = Me.INDGcEmployerLiquidation
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'INDGcNameConcept
        '
        Me.INDGcNameConcept.Caption = "Descripción"
        Me.INDGcNameConcept.FieldName = "Description"
        Me.INDGcNameConcept.Name = "INDGcNameConcept"
        Me.INDGcNameConcept.OptionsColumn.AllowEdit = False
        Me.INDGcNameConcept.Visible = True
        Me.INDGcNameConcept.VisibleIndex = 0
        Me.INDGcNameConcept.Width = 304
        '
        'INDGcValue
        '
        Me.INDGcValue.Caption = "Valor"
        Me.INDGcValue.DisplayFormat.FormatString = "C2"
        Me.INDGcValue.FieldName = "Accrued"
        Me.INDGcValue.Name = "INDGcValue"
        Me.INDGcValue.OptionsColumn.AllowEdit = False
        Me.INDGcValue.Visible = True
        Me.INDGcValue.VisibleIndex = 1
        Me.INDGcValue.Width = 70
        '
        'INDColActions2
        '
        Me.INDColActions2.Caption = "Acción"
        Me.INDColActions2.ColumnEdit = Me.RepositoryItemPopupContainerEdit2
        Me.INDColActions2.Name = "INDColActions2"
        Me.INDColActions2.Visible = True
        Me.INDColActions2.VisibleIndex = 2
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.AutoHeight = False
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        Me.RepositoryItemPopupContainerEdit2.PopupControl = Me.INDPopUpContainerControl2
        Me.RepositoryItemPopupContainerEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDDeResolutionDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeResolutionDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeResolutionDate, False)
        Me.INDDeResolutionDate.EditValue = Nothing
        Me.INDDeResolutionDate.Location = New System.Drawing.Point(171, 157)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeResolutionDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDDeResolutionDate.Name = "INDDeResolutionDate"
        Me.INDDeResolutionDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeResolutionDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeResolutionDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeResolutionDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeResolutionDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeResolutionDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeResolutionDate.Size = New System.Drawing.Size(239, 28)
        Me.INDDeResolutionDate.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDDeResolutionDate.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeResolutionDate, 0)
        '
        'INDtxtResolutionNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtResolutionNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtResolutionNumber, False)
        Me.INDtxtResolutionNumber.Location = New System.Drawing.Point(171, 121)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtResolutionNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtResolutionNumber.Name = "INDtxtResolutionNumber"
        Me.INDtxtResolutionNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtResolutionNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtResolutionNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtResolutionNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtResolutionNumber.Properties.MaxLength = 50
        Me.INDtxtResolutionNumber.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtResolutionNumber.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtResolutionNumber.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtResolutionNumber, 0)
        '
        'INDgcMessage
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcMessage, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcMessage, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcMessage, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcMessage, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcMessage, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcMessage, False)
        Me.INDgcMessage.Location = New System.Drawing.Point(1896, -163)
        Me.INDgcMessage.MainView = Me.INDgvMessage
        Me.INDgcMessage.Name = "INDgcMessage"
        Me.INDgcMessage.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit1})
        Me.INDgcMessage.Size = New System.Drawing.Size(596, 352)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcMessage, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcMessage.TabIndex = 13
        Me.INDgcMessage.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvMessage})
        '
        'INDgvMessage
        '
        Me.INDgvMessage.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvMessage.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvMessage.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvMessage.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvMessage.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvMessage.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMessage.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvMessage.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMessage.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvMessage.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvMessage.Appearance.Row.Options.UseFont = True
        Me.INDgvMessage.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvMessage.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvMessage.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolInfoMessage, Me.INDcolDescriptionMessage})
        Me.INDgvMessage.GridControl = Me.INDgcMessage
        Me.INDgvMessage.Name = "INDgvMessage"
        Me.INDgvMessage.OptionsBehavior.Editable = False
        Me.INDgvMessage.OptionsCustomization.AllowGroup = False
        Me.INDgvMessage.OptionsCustomization.AllowSort = False
        Me.INDgvMessage.OptionsMenu.EnableColumnMenu = False
        Me.INDgvMessage.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvMessage.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvMessage.OptionsView.ShowAutoFilterRow = True
        Me.INDgvMessage.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvMessage, False)
        '
        'INDcolInfoMessage
        '
        Me.INDcolInfoMessage.Caption = "Info"
        Me.INDcolInfoMessage.ColumnEdit = Me.RepositoryItemPictureEdit1
        Me.INDcolInfoMessage.FieldName = "Error"
        Me.INDcolInfoMessage.Name = "INDcolInfoMessage"
        Me.INDcolInfoMessage.Visible = True
        Me.INDcolInfoMessage.VisibleIndex = 0
        Me.INDcolInfoMessage.Width = 49
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        '
        'INDcolDescriptionMessage
        '
        Me.INDcolDescriptionMessage.Caption = "Descripción"
        Me.INDcolDescriptionMessage.FieldName = "Description"
        Me.INDcolDescriptionMessage.Name = "INDcolDescriptionMessage"
        Me.INDcolDescriptionMessage.Visible = True
        Me.INDcolDescriptionMessage.VisibleIndex = 1
        Me.INDcolDescriptionMessage.Width = 529
        '
        'INDtxtRetirementReason
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtRetirementReason, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtRetirementReason, False)
        Me.INDtxtRetirementReason.Location = New System.Drawing.Point(171, -19)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtRetirementReason, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtRetirementReason.Name = "INDtxtRetirementReason"
        Me.INDtxtRetirementReason.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtRetirementReason.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRetirementReason.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtRetirementReason.Properties.Appearance.Options.UseFont = True
        Me.INDtxtRetirementReason.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtRetirementReason.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtRetirementReason.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRetirementReason.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtRetirementReason.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtRetirementReason.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtRetirementReason.Properties.ReadOnly = True
        Me.INDtxtRetirementReason.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtRetirementReason.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtRetirementReason.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtRetirementReason, 0)
        '
        'INDgcLiquidationDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLiquidationDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLiquidationDetail, Nothing)
        Me.INDgcLiquidationDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLiquidationDetail, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLiquidationDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLiquidationDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLiquidationDetail, False)
        GridLevelNode5.RelationName = "Level1"
        Me.INDgcLiquidationDetail.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode5})
        Me.INDgcLiquidationDetail.Location = New System.Drawing.Point(438, -163)
        Me.INDgcLiquidationDetail.MainView = Me.GridView1
        Me.INDgcLiquidationDetail.Name = "INDgcLiquidationDetail"
        Me.INDgcLiquidationDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit1})
        Me.INDgcLiquidationDetail.Size = New System.Drawing.Size(696, 352)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLiquidationDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcLiquidationDetail.TabIndex = 11
        Me.INDgcLiquidationDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColumnDescription, Me.INDColumnInitialDate, Me.INDColumnEndingDate, Me.INDColumAccrued, Me.INDcolDeducted, Me.Acciones})
        Me.GridView1.GridControl = Me.INDgcLiquidationDetail
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowSort = False
        Me.GridView1.OptionsMenu.EnableColumnMenu = False
        Me.GridView1.OptionsMenu.EnableGroupPanelMenu = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDColumnDescription
        '
        Me.INDColumnDescription.Caption = "Descripción"
        Me.INDColumnDescription.FieldName = "Description"
        Me.INDColumnDescription.Name = "INDColumnDescription"
        Me.INDColumnDescription.OptionsColumn.AllowEdit = False
        Me.INDColumnDescription.Visible = True
        Me.INDColumnDescription.VisibleIndex = 0
        Me.INDColumnDescription.Width = 288
        '
        'INDColumnInitialDate
        '
        Me.INDColumnInitialDate.Caption = "F. Inicio"
        Me.INDColumnInitialDate.FieldName = "InitialDate"
        Me.INDColumnInitialDate.Name = "INDColumnInitialDate"
        Me.INDColumnInitialDate.OptionsColumn.AllowEdit = False
        Me.INDColumnInitialDate.Visible = True
        Me.INDColumnInitialDate.VisibleIndex = 1
        Me.INDColumnInitialDate.Width = 133
        '
        'INDColumnEndingDate
        '
        Me.INDColumnEndingDate.Caption = "F. Final"
        Me.INDColumnEndingDate.FieldName = "EndingDate"
        Me.INDColumnEndingDate.Name = "INDColumnEndingDate"
        Me.INDColumnEndingDate.OptionsColumn.AllowEdit = False
        Me.INDColumnEndingDate.Visible = True
        Me.INDColumnEndingDate.VisibleIndex = 2
        Me.INDColumnEndingDate.Width = 134
        '
        'INDColumAccrued
        '
        Me.INDColumAccrued.Caption = "Devengado"
        Me.INDColumAccrued.DisplayFormat.FormatString = "C2"
        Me.INDColumAccrued.FieldName = "Accrued"
        Me.INDColumAccrued.Name = "INDColumAccrued"
        Me.INDColumAccrued.Visible = True
        Me.INDColumAccrued.VisibleIndex = 3
        Me.INDColumAccrued.Width = 123
        '
        'INDcolDeducted
        '
        Me.INDcolDeducted.Caption = "Deducido"
        Me.INDcolDeducted.DisplayFormat.FormatString = "C2"
        Me.INDcolDeducted.FieldName = "Deducted"
        Me.INDcolDeducted.Name = "INDcolDeducted"
        Me.INDcolDeducted.Visible = True
        Me.INDcolDeducted.VisibleIndex = 4
        '
        'Acciones
        '
        Me.Acciones.Caption = "Acción"
        Me.Acciones.ColumnEdit = Me.RepositoryItemPopupContainerEdit1
        Me.Acciones.Name = "Acciones"
        Me.Acciones.Visible = True
        Me.Acciones.VisibleIndex = 5
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.RepositoryItemPopupContainerEdit1.AutoHeight = False
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        Me.RepositoryItemPopupContainerEdit1.PopupControl = Me.INDPopupContainerControl1
        Me.RepositoryItemPopupContainerEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPopupContainerControl1
        '
        Me.INDPopupContainerControl1.Controls.Add(Me.LayoutControl2)
        Me.INDPopupContainerControl1.Location = New System.Drawing.Point(366, 20)
        Me.INDPopupContainerControl1.Name = "INDPopupContainerControl1"
        Me.INDPopupContainerControl1.Size = New System.Drawing.Size(422, 313)
        Me.INDPopupContainerControl1.TabIndex = 55
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDteResultFormula)
        Me.LayoutControl2.Controls.Add(Me.INDteReplaceFormula)
        Me.LayoutControl2.Controls.Add(Me.INDteUsedFormula)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup7
        Me.LayoutControl2.Size = New System.Drawing.Size(422, 313)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDteResultFormula
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteResultFormula, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteResultFormula, False)
        Me.INDteResultFormula.Location = New System.Drawing.Point(111, 261)
        Me.IndigoTextEdit1.SetMascara(Me.INDteResultFormula, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteResultFormula.Name = "INDteResultFormula"
        Me.INDteResultFormula.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteResultFormula.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteResultFormula.Properties.Appearance.Options.UseBackColor = True
        Me.INDteResultFormula.Properties.Appearance.Options.UseFont = True
        Me.INDteResultFormula.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteResultFormula.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteResultFormula.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDteResultFormula.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteResultFormula.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteResultFormula.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteResultFormula.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteResultFormula.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteResultFormula.Properties.Mask.EditMask = "c2"
        Me.INDteResultFormula.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteResultFormula.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteResultFormula.Properties.ReadOnly = True
        Me.INDteResultFormula.Size = New System.Drawing.Size(287, 28)
        Me.INDteResultFormula.StyleController = Me.LayoutControl2
        Me.INDteResultFormula.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteResultFormula, 0)
        '
        'INDteReplaceFormula
        '
        Me.INDteReplaceFormula.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.INDteReplaceFormula.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDteReplaceFormula.Location = New System.Drawing.Point(111, 157)
        Me.INDteReplaceFormula.Multiline = True
        Me.INDteReplaceFormula.Name = "INDteReplaceFormula"
        Me.INDteReplaceFormula.ReadOnly = True
        Me.INDteReplaceFormula.Size = New System.Drawing.Size(287, 100)
        Me.INDteReplaceFormula.TabIndex = 1
        '
        'INDteUsedFormula
        '
        Me.INDteUsedFormula.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.INDteUsedFormula.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDteUsedFormula.Location = New System.Drawing.Point(111, 53)
        Me.INDteUsedFormula.Multiline = True
        Me.INDteUsedFormula.Name = "INDteUsedFormula"
        Me.INDteUsedFormula.ReadOnly = True
        Me.INDteUsedFormula.Size = New System.Drawing.Size(287, 100)
        Me.INDteUsedFormula.TabIndex = 0
        '
        'LayoutControlGroup7
        '
        Me.LayoutControlGroup7.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup7.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup7.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup7.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup7.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup7.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup7.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup7, False)
        Me.LayoutControlGroup7.CustomizationFormText = "LayoutControlGroup7"
        Me.LayoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup7.GroupBordersVisible = False
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup8})
        Me.LayoutControlGroup7.Name = "Root"
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(422, 313)
        Me.LayoutControlGroup7.TextVisible = False
        '
        'LayoutControlGroup8
        '
        Me.LayoutControlGroup8.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup8.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup8.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup8.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup8.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup8, False)
        Me.LayoutControlGroup8.CustomizationFormText = "Fórmulas"
        Me.LayoutControlGroup8.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyfUsedFormula1, Me.INDlyfReplaceFormula1, Me.INDlyfResultFormula1})
        Me.LayoutControlGroup8.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup8.Name = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Size = New System.Drawing.Size(402, 293)
        Me.LayoutControlGroup8.Text = "Fórmulas"
        '
        'INDlyfUsedFormula1
        '
        Me.INDlyfUsedFormula1.Control = Me.INDteUsedFormula
        Me.INDlyfUsedFormula1.CustomizationFormText = "Usada"
        Me.INDlyfUsedFormula1.Location = New System.Drawing.Point(0, 0)
        Me.INDlyfUsedFormula1.MinSize = New System.Drawing.Size(92, 100)
        Me.INDlyfUsedFormula1.Name = "INDlyfUsedFormula1"
        Me.INDlyfUsedFormula1.Size = New System.Drawing.Size(378, 104)
        Me.INDlyfUsedFormula1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyfUsedFormula1.Text = "Usada"
        Me.INDlyfUsedFormula1.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDlyfReplaceFormula1
        '
        Me.INDlyfReplaceFormula1.Control = Me.INDteReplaceFormula
        Me.INDlyfReplaceFormula1.CustomizationFormText = "Reemplazada"
        Me.INDlyfReplaceFormula1.Location = New System.Drawing.Point(0, 104)
        Me.INDlyfReplaceFormula1.MinSize = New System.Drawing.Size(92, 100)
        Me.INDlyfReplaceFormula1.Name = "INDlyfReplaceFormula1"
        Me.INDlyfReplaceFormula1.Size = New System.Drawing.Size(378, 104)
        Me.INDlyfReplaceFormula1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyfReplaceFormula1.Text = "Reemplazada"
        Me.INDlyfReplaceFormula1.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDlyfResultFormula1
        '
        Me.INDlyfResultFormula1.Control = Me.INDteResultFormula
        Me.INDlyfResultFormula1.CustomizationFormText = "Resultado"
        Me.INDlyfResultFormula1.Location = New System.Drawing.Point(0, 208)
        Me.INDlyfResultFormula1.Name = "INDlyfResultFormula1"
        Me.INDlyfResultFormula1.Size = New System.Drawing.Size(378, 32)
        Me.INDlyfResultFormula1.Text = "Resultado"
        Me.INDlyfResultFormula1.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDtxtTotalPaid
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalPaid, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalPaid, False)
        Me.INDtxtTotalPaid.Location = New System.Drawing.Point(171, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalPaid, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTotalPaid.Name = "INDtxtTotalPaid"
        Me.INDtxtTotalPaid.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtTotalPaid.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalPaid.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalPaid.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTotalPaid.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalPaid.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalPaid.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtTotalPaid.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtTotalPaid.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalPaid.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalPaid.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtTotalPaid.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtTotalPaid.Properties.Mask.EditMask = "c"
        Me.INDtxtTotalPaid.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalPaid.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalPaid.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtTotalPaid.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtTotalPaid.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalPaid, 0)
        '
        'INDtxtPosition
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPosition, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPosition, False)
        Me.INDtxtPosition.Location = New System.Drawing.Point(171, 17)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPosition, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPosition.Name = "INDtxtPosition"
        Me.INDtxtPosition.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtPosition.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPosition.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPosition.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPosition.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtPosition.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtPosition.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPosition.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPosition.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtPosition.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPosition.Properties.ReadOnly = True
        Me.INDtxtPosition.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtPosition.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtPosition.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPosition, 0)
        '
        'INDtxtSalaryBase
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSalaryBase, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSalaryBase, False)
        Me.INDtxtSalaryBase.Location = New System.Drawing.Point(171, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSalaryBase, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtxtSalaryBase.Name = "INDtxtSalaryBase"
        Me.INDtxtSalaryBase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSalaryBase.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSalaryBase.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSalaryBase.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSalaryBase.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSalaryBase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSalaryBase.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtSalaryBase.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtSalaryBase.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSalaryBase.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSalaryBase.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtSalaryBase.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSalaryBase.Properties.Mask.EditMask = "c0"
        Me.INDtxtSalaryBase.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSalaryBase.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSalaryBase.Properties.ReadOnly = True
        Me.INDtxtSalaryBase.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtSalaryBase.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtSalaryBase.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSalaryBase, 0)
        '
        'INDtxtEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtEndDate, False)
        Me.INDtxtEndDate.Location = New System.Drawing.Point(171, -55)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtEndDate.Name = "INDtxtEndDate"
        Me.INDtxtEndDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDtxtEndDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtEndDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEndDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtEndDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtEndDate.Properties.ReadOnly = True
        Me.INDtxtEndDate.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtEndDate.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtEndDate.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtEndDate, 0)
        '
        'INDtxtInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtInitialDate, False)
        Me.INDtxtInitialDate.Location = New System.Drawing.Point(171, -91)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtInitialDate.Name = "INDtxtInitialDate"
        Me.INDtxtInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDtxtInitialDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtInitialDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtInitialDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtInitialDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtInitialDate.Properties.ReadOnly = True
        Me.INDtxtInitialDate.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtInitialDate.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtInitialDate.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtInitialDate, 0)
        '
        'INDtxtEmployee
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtEmployee, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtEmployee, False)
        Me.INDtxtEmployee.Location = New System.Drawing.Point(171, -127)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtEmployee.Name = "INDtxtEmployee"
        Me.INDtxtEmployee.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtEmployee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEmployee.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtEmployee.Properties.Appearance.Options.UseFont = True
        Me.INDtxtEmployee.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtEmployee.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtEmployee.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEmployee.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtEmployee.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtEmployee.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtEmployee.Properties.ReadOnly = True
        Me.INDtxtEmployee.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtEmployee.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtEmployee.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtEmployee, 0)
        '
        'INDtxtGroup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtGroup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtGroup, False)
        Me.INDtxtGroup.Location = New System.Drawing.Point(171, -163)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtGroup.Name = "INDtxtGroup"
        Me.INDtxtGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtGroup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtGroup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtGroup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtGroup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtGroup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtGroup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtGroup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtGroup.Properties.ReadOnly = True
        Me.INDtxtGroup.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtGroup.StyleController = Me.INDlyControlEmployeeLiquidation
        Me.INDtxtGroup.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtGroup, 0)
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGroupGeneralImformation, Me.INDlyGroupLiquidationDetail, Me.INDlyGroupMessageLiquidation, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2516, 429)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGroupGeneralImformation
        '
        Me.INDlyGroupGeneralImformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupGeneralImformation.AppearanceGroup.Options.UseFont = True
        Me.INDlyGroupGeneralImformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupGeneralImformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupGeneralImformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupGeneralImformation, False)
        Me.INDlyGroupGeneralImformation.CustomizationFormText = "LayoutControlGroup2"
        Me.INDlyGroupGeneralImformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemTotalPaid, Me.INDlyItemGroup, Me.INDlyItemEmployee, Me.INDlyItemInitialDate, Me.INDlyItemEndDate, Me.INDlyItemPosition, Me.INDlyItemSalaryBase, Me.INDlyItemRetirementReason, Me.INDlyItemResolutionNumber, Me.INDlyItemResolutionDate})
        Me.INDlyGroupGeneralImformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGroupGeneralImformation.Name = "INDlyGroupGeneralImformation"
        Me.INDlyGroupGeneralImformation.Size = New System.Drawing.Size(414, 409)
        Me.INDlyGroupGeneralImformation.Text = "Información General"
        '
        'INDlyItemTotalPaid
        '
        Me.INDlyItemTotalPaid.Control = Me.INDtxtTotalPaid
        Me.INDlyItemTotalPaid.CustomizationFormText = "Total a Pagar"
        Me.INDlyItemTotalPaid.Location = New System.Drawing.Point(0, 248)
        Me.INDlyItemTotalPaid.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTotalPaid.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemTotalPaid.Name = "INDlyItemTotalPaid"
        Me.INDlyItemTotalPaid.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemTotalPaid.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalPaid.Text = "Total a Pagar"
        Me.INDlyItemTotalPaid.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTotalPaid.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTotalPaid.TextToControlDistance = 12
        '
        'INDlyItemGroup
        '
        Me.INDlyItemGroup.Control = Me.INDtxtGroup
        Me.INDlyItemGroup.CustomizationFormText = "Grupo"
        Me.INDlyItemGroup.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemGroup.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemGroup.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemGroup.Name = "INDlyItemGroup"
        Me.INDlyItemGroup.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGroup.Text = "Grupo"
        Me.INDlyItemGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemGroup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemGroup.TextToControlDistance = 12
        '
        'INDlyItemEmployee
        '
        Me.INDlyItemEmployee.Control = Me.INDtxtEmployee
        Me.INDlyItemEmployee.CustomizationFormText = "Empleado"
        Me.INDlyItemEmployee.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemEmployee.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemEmployee.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemEmployee.Name = "INDlyItemEmployee"
        Me.INDlyItemEmployee.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEmployee.Text = "Empleado"
        Me.INDlyItemEmployee.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEmployee.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEmployee.TextToControlDistance = 12
        '
        'INDlyItemInitialDate
        '
        Me.INDlyItemInitialDate.Control = Me.INDtxtInitialDate
        Me.INDlyItemInitialDate.CustomizationFormText = "Fecha Inicio"
        Me.INDlyItemInitialDate.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemInitialDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemInitialDate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemInitialDate.Name = "INDlyItemInitialDate"
        Me.INDlyItemInitialDate.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitialDate.Text = "Fecha Inicio"
        Me.INDlyItemInitialDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitialDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInitialDate.TextToControlDistance = 12
        '
        'INDlyItemEndDate
        '
        Me.INDlyItemEndDate.Control = Me.INDtxtEndDate
        Me.INDlyItemEndDate.CustomizationFormText = "Fecha Retiro"
        Me.INDlyItemEndDate.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemEndDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemEndDate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemEndDate.Name = "INDlyItemEndDate"
        Me.INDlyItemEndDate.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemEndDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEndDate.Text = "Fecha Retiro"
        Me.INDlyItemEndDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEndDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEndDate.TextToControlDistance = 12
        '
        'INDlyItemPosition
        '
        Me.INDlyItemPosition.Control = Me.INDtxtPosition
        Me.INDlyItemPosition.CustomizationFormText = "Cargo"
        Me.INDlyItemPosition.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemPosition.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemPosition.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemPosition.Name = "INDlyItemPosition"
        Me.INDlyItemPosition.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemPosition.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPosition.Text = "Cargo"
        Me.INDlyItemPosition.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPosition.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPosition.TextToControlDistance = 12
        '
        'INDlyItemSalaryBase
        '
        Me.INDlyItemSalaryBase.Control = Me.INDtxtSalaryBase
        Me.INDlyItemSalaryBase.CustomizationFormText = "Salario Base"
        Me.INDlyItemSalaryBase.Location = New System.Drawing.Point(0, 216)
        Me.INDlyItemSalaryBase.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSalaryBase.MinSize = New System.Drawing.Size(390, 32)
        Me.INDlyItemSalaryBase.Name = "INDlyItemSalaryBase"
        Me.INDlyItemSalaryBase.Size = New System.Drawing.Size(390, 32)
        Me.INDlyItemSalaryBase.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSalaryBase.Text = "Salario Base"
        Me.INDlyItemSalaryBase.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSalaryBase.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSalaryBase.TextToControlDistance = 12
        '
        'INDlyItemRetirementReason
        '
        Me.INDlyItemRetirementReason.Control = Me.INDtxtRetirementReason
        Me.INDlyItemRetirementReason.CustomizationFormText = "Motivo de Retiro"
        Me.INDlyItemRetirementReason.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemRetirementReason.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemRetirementReason.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemRetirementReason.Name = "INDlyItemRetirementReason"
        Me.INDlyItemRetirementReason.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemRetirementReason.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRetirementReason.Text = "Motivo de Retiro"
        Me.INDlyItemRetirementReason.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRetirementReason.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRetirementReason.TextToControlDistance = 12
        '
        'INDlyItemResolutionNumber
        '
        Me.INDlyItemResolutionNumber.Control = Me.INDtxtResolutionNumber
        Me.INDlyItemResolutionNumber.CustomizationFormText = "# Resolución"
        Me.INDlyItemResolutionNumber.Location = New System.Drawing.Point(0, 284)
        Me.INDlyItemResolutionNumber.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemResolutionNumber.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemResolutionNumber.Name = "INDlyItemResolutionNumber"
        Me.INDlyItemResolutionNumber.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemResolutionNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemResolutionNumber.Text = "# Resolución"
        Me.INDlyItemResolutionNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemResolutionNumber.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemResolutionNumber.TextToControlDistance = 12
        Me.INDlyItemResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemResolutionDate
        '
        Me.INDlyItemResolutionDate.Control = Me.INDDeResolutionDate
        Me.INDlyItemResolutionDate.CustomizationFormText = "Fecha Resolución"
        Me.INDlyItemResolutionDate.Location = New System.Drawing.Point(0, 320)
        Me.INDlyItemResolutionDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemResolutionDate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemResolutionDate.Name = "INDlyItemResolutionDate"
        Me.INDlyItemResolutionDate.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemResolutionDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemResolutionDate.Text = "Fecha Resolución"
        Me.INDlyItemResolutionDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemResolutionDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemResolutionDate.TextToControlDistance = 12
        Me.INDlyItemResolutionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyGroupLiquidationDetail
        '
        Me.INDlyGroupLiquidationDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupLiquidationDetail.AppearanceGroup.Options.UseFont = True
        Me.INDlyGroupLiquidationDetail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupLiquidationDetail.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupLiquidationDetail.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupLiquidationDetail, False)
        Me.INDlyGroupLiquidationDetail.CustomizationFormText = "Detalle Liquidación"
        Me.INDlyGroupLiquidationDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemLiquidationDetail, Me.EmptySpaceItem1})
        Me.INDlyGroupLiquidationDetail.Location = New System.Drawing.Point(414, 0)
        Me.INDlyGroupLiquidationDetail.Name = "INDlyGroupLiquidationDetail"
        Me.INDlyGroupLiquidationDetail.Size = New System.Drawing.Size(734, 409)
        Me.INDlyGroupLiquidationDetail.Text = "Detalle Liquidación"
        '
        'INDlyItemLiquidationDetail
        '
        Me.INDlyItemLiquidationDetail.Control = Me.INDgcLiquidationDetail
        Me.INDlyItemLiquidationDetail.CustomizationFormText = "Detalle Liquidación"
        Me.INDlyItemLiquidationDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemLiquidationDetail.MaxSize = New System.Drawing.Size(700, 0)
        Me.INDlyItemLiquidationDetail.MinSize = New System.Drawing.Size(700, 24)
        Me.INDlyItemLiquidationDetail.Name = "INDlyItemLiquidationDetail"
        Me.INDlyItemLiquidationDetail.Size = New System.Drawing.Size(700, 356)
        Me.INDlyItemLiquidationDetail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLiquidationDetail.Text = "Liquidación Detalle"
        Me.INDlyItemLiquidationDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemLiquidationDetail.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(700, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(10, 356)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlyGroupMessageLiquidation
        '
        Me.INDlyGroupMessageLiquidation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupMessageLiquidation.AppearanceGroup.Options.UseFont = True
        Me.INDlyGroupMessageLiquidation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupMessageLiquidation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupMessageLiquidation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupMessageLiquidation, False)
        Me.INDlyGroupMessageLiquidation.CustomizationFormText = "Mensajes de Liquidación"
        Me.INDlyGroupMessageLiquidation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemMessageLiquidation})
        Me.INDlyGroupMessageLiquidation.Location = New System.Drawing.Point(1872, 0)
        Me.INDlyGroupMessageLiquidation.Name = "INDlyGroupMessageLiquidation"
        Me.INDlyGroupMessageLiquidation.Size = New System.Drawing.Size(624, 409)
        Me.INDlyGroupMessageLiquidation.Text = "Mensajes de Liquidación"
        '
        'INDlyItemMessageLiquidation
        '
        Me.INDlyItemMessageLiquidation.Control = Me.INDgcMessage
        Me.INDlyItemMessageLiquidation.CustomizationFormText = "Regilla Mensajes "
        Me.INDlyItemMessageLiquidation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemMessageLiquidation.MaxSize = New System.Drawing.Size(600, 0)
        Me.INDlyItemMessageLiquidation.MinSize = New System.Drawing.Size(600, 24)
        Me.INDlyItemMessageLiquidation.Name = "INDlyItemMessageLiquidation"
        Me.INDlyItemMessageLiquidation.Size = New System.Drawing.Size(600, 356)
        Me.INDlyItemMessageLiquidation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemMessageLiquidation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemMessageLiquidation.TextVisible = False
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(1148, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(724, 409)
        Me.LayoutControlGroup3.Text = "Conceptos Patronales"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcEmployerLiquidation
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(700, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(700, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(700, 356)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyControlEmployeeLiquidation
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 2)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 230)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlbEmployeeName
        '
        Me.INDlbEmployeeName.Font = New System.Drawing.Font("Segoe UI Light", 14.0!, System.Drawing.FontStyle.Bold)
        Me.INDlbEmployeeName.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.INDlbEmployeeName.Location = New System.Drawing.Point(65, 285)
        Me.INDlbEmployeeName.Name = "INDlbEmployeeName"
        Me.INDlbEmployeeName.Size = New System.Drawing.Size(1073, 60)
        Me.INDlbEmployeeName.TabIndex = 77
        Me.INDlbEmployeeName.Text = "INDLblEmployeeName"
        Me.INDlbEmployeeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CtrNavigation1
        '
        Me.CtrNavigation1.HideGroupContent = True
        Me.CtrNavigation1.Location = New System.Drawing.Point(10, 283)
        Me.CtrNavigation1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CtrNavigation1.MaximumSize = New System.Drawing.Size(0, 58)
        Me.CtrNavigation1.MinimumSize = New System.Drawing.Size(0, 50)
        Me.CtrNavigation1.Name = "CtrNavigation1"
        Me.CtrNavigation1.Size = New System.Drawing.Size(53, 58)
        Me.CtrNavigation1.TabIndex = 20
        '
        'INDlcgContractLiquidation
        '
        Me.INDlcgContractLiquidation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractLiquidation.AppearanceGroup.Options.UseFont = True
        Me.INDlcgContractLiquidation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractLiquidation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgContractLiquidation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractLiquidation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgContractLiquidation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgContractLiquidation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgContractLiquidation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractLiquidation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgContractLiquidation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractLiquidation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgContractLiquidation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractLiquidation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgContractLiquidation, False)
        Me.INDlcgContractLiquidation.CustomizationFormText = "Root"
        Me.INDlcgContractLiquidation.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgContractLiquidation.GroupBordersVisible = False
        Me.INDlcgContractLiquidation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEmployeeBack, Me.INDlyItemPanelLiquidation, Me.INDlyItemEmployeeName, Me.INDlyGroupHumanTalent})
        Me.INDlcgContractLiquidation.Name = "Root"
        Me.INDlcgContractLiquidation.Size = New System.Drawing.Size(1150, 595)
        Me.INDlcgContractLiquidation.TextVisible = False
        '
        'INDlyItemEmployeeBack
        '
        Me.INDlyItemEmployeeBack.Control = Me.CtrNavigation1
        Me.INDlyItemEmployeeBack.CustomizationFormText = "Boton Atras"
        Me.INDlyItemEmployeeBack.Location = New System.Drawing.Point(0, 273)
        Me.INDlyItemEmployeeBack.MaxSize = New System.Drawing.Size(53, 64)
        Me.INDlyItemEmployeeBack.MinSize = New System.Drawing.Size(53, 64)
        Me.INDlyItemEmployeeBack.Name = "INDlyItemEmployeeBack"
        Me.INDlyItemEmployeeBack.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlyItemEmployeeBack.Size = New System.Drawing.Size(53, 64)
        Me.INDlyItemEmployeeBack.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEmployeeBack.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemEmployeeBack.TextVisible = False
        Me.INDlyItemEmployeeBack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemPanelLiquidation
        '
        Me.INDlyItemPanelLiquidation.Control = Me.PanelControl1
        Me.INDlyItemPanelLiquidation.CustomizationFormText = "Panel de Liquidacion"
        Me.INDlyItemPanelLiquidation.Location = New System.Drawing.Point(0, 337)
        Me.INDlyItemPanelLiquidation.MinSize = New System.Drawing.Size(104, 24)
        Me.INDlyItemPanelLiquidation.Name = "INDlyItemPanelLiquidation"
        Me.INDlyItemPanelLiquidation.Size = New System.Drawing.Size(1130, 238)
        Me.INDlyItemPanelLiquidation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPanelLiquidation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemPanelLiquidation.TextVisible = False
        Me.INDlyItemPanelLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemEmployeeName
        '
        Me.INDlyItemEmployeeName.Control = Me.INDlbEmployeeName
        Me.INDlyItemEmployeeName.CustomizationFormText = "Nombre Empleado"
        Me.INDlyItemEmployeeName.Location = New System.Drawing.Point(53, 273)
        Me.INDlyItemEmployeeName.MaxSize = New System.Drawing.Size(0, 64)
        Me.INDlyItemEmployeeName.MinSize = New System.Drawing.Size(24, 64)
        Me.INDlyItemEmployeeName.Name = "INDlyItemEmployeeName"
        Me.INDlyItemEmployeeName.Size = New System.Drawing.Size(1077, 64)
        Me.INDlyItemEmployeeName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEmployeeName.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemEmployeeName.TextVisible = False
        Me.INDlyItemEmployeeName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyGroupHumanTalent
        '
        Me.INDlyGroupHumanTalent.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupHumanTalent.AppearanceGroup.Options.UseFont = True
        Me.INDlyGroupHumanTalent.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupHumanTalent.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupHumanTalent.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupHumanTalent.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupHumanTalent.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGroupHumanTalent.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGroupHumanTalent.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupHumanTalent.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupHumanTalent.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupHumanTalent.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGroupHumanTalent.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupHumanTalent.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupHumanTalent, False)
        Me.INDlyGroupHumanTalent.CustomizationFormText = "Liquidación Contrato"
        Me.INDlyGroupHumanTalent.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEmployees, Me.INDlyItemHumanTalent, Me.INDlyItemAddEmployee})
        Me.INDlyGroupHumanTalent.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGroupHumanTalent.Name = "INDlcgHumanTalent"
        Me.INDlyGroupHumanTalent.Size = New System.Drawing.Size(1130, 273)
        Me.INDlyGroupHumanTalent.Text = "Liquidación de Contrato"
        '
        'INDlyItemEmployees
        '
        Me.INDlyItemEmployees.Control = Me.INDsleEmployees
        Me.INDlyItemEmployees.CustomizationFormText = "Empleados"
        Me.INDlyItemEmployees.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemEmployees.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemEmployees.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemEmployees.Name = "INDlyItemEmployees"
        Me.INDlyItemEmployees.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemEmployees.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEmployees.Text = "Empleados"
        Me.INDlyItemEmployees.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEmployees.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEmployees.TextToControlDistance = 12
        '
        'INDlyItemHumanTalent
        '
        Me.INDlyItemHumanTalent.Control = Me.INDgrdHumanTalent
        Me.INDlyItemHumanTalent.CustomizationFormText = "Talento Humano"
        Me.INDlyItemHumanTalent.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemHumanTalent.Name = "INDlyItemHumanTalent"
        Me.INDlyItemHumanTalent.Size = New System.Drawing.Size(1106, 184)
        Me.INDlyItemHumanTalent.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemHumanTalent.TextVisible = False
        '
        'INDlyItemAddEmployee
        '
        Me.INDlyItemAddEmployee.Control = Me.INDbtnAddEmployee
        Me.INDlyItemAddEmployee.CustomizationFormText = "Agregar Empleado"
        Me.INDlyItemAddEmployee.Location = New System.Drawing.Point(390, 0)
        Me.INDlyItemAddEmployee.MaxSize = New System.Drawing.Size(200, 32)
        Me.INDlyItemAddEmployee.MinSize = New System.Drawing.Size(200, 32)
        Me.INDlyItemAddEmployee.Name = "INDlyItemAddEmployee"
        Me.INDlyItemAddEmployee.Size = New System.Drawing.Size(716, 36)
        Me.INDlyItemAddEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddEmployee.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemAddEmployee.TextVisible = False
        '
        'INDlcgHumanTalent
        '
        Me.INDlcgHumanTalent.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgHumanTalent.AppearanceGroup.Options.UseFont = True
        Me.INDlcgHumanTalent.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgHumanTalent.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgHumanTalent.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgHumanTalent.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgHumanTalent.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgHumanTalent.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgHumanTalent.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgHumanTalent.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgHumanTalent.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgHumanTalent.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgHumanTalent.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgHumanTalent.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgHumanTalent, False)
        Me.INDlcgHumanTalent.CustomizationFormText = "Talento Humano"
        Me.INDlcgHumanTalent.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgHumanTalent.Name = "INDlcgHumanTalent"
        Me.INDlcgHumanTalent.Size = New System.Drawing.Size(1042, 325)
        Me.INDlcgHumanTalent.Text = "Talento Humano"
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
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 325)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(82, 117)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit3
        '
        'FrmContractLiquidation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1154, 741)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "FrmContractLiquidation"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "596"
        Me.Text = "Liquidación de Contrato"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrdHumanTalent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvHumanTalent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepRetirementDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepRetirementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepRetirementReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepDeleteAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvLiquidations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvNovelties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtrContractLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtrContractLiquidation.ResumeLayout(False)
        CType(Me.INDPopUpContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPopUpContainerControl2.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDteResultFormulaEmployer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEmployees.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvSleEmployees, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.INDlyControlEmployeeLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyControlEmployeeLiquidation.ResumeLayout(False)
        CType(Me.INDGcEmployerLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeResolutionDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeResolutionDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtResolutionNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtRetirementReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcLiquidationDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPopupContainerControl1.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDteResultFormula.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyfUsedFormula1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyfReplaceFormula1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyfResultFormula1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTotalPaid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPosition.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSalaryBase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtEmployee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupGeneralImformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalPaid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInitialDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSalaryBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRetirementReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemResolutionNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemResolutionDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupLiquidationDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLiquidationDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupMessageLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemMessageLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgContractLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEmployeeBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPanelLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupHumanTalent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEmployees, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHumanTalent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgHumanTalent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyCtrContractLiquidation As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgContractLiquidation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDlcgHumanTalent As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleEmployees As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgrvSleEmployees As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDbtnAddEmployee As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDgrcEmployeeNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcEmployeeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrdHumanTalent As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgrvContract As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgrcBasicSalary As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcContractInitialDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcContractEndingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrvHumanTalent As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgrcEmloyeeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcEmployeeNames As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrvLiquidations As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgrcContractNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcLiquidationPeriod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcLiquidationTotalAccrued As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcLiquidationTotalDeducted As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcLiquidationTotalPaid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcLiquidationPeriodIBC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrvNovelties As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgrcNoveltiesPeriod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcPayrollDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcWorkedDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcDisabilityDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcVacationDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcSanctionDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcPermissionDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcLicenseDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcQuoteHealthDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcContractCompany As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcContractGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcContractPosition As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcRetirementDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepRetirementDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents INDgrcActions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepDeleteAction As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents INDrepConfirm As DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup
    Friend WithEvents INDgrcInitialContractNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcRetirementReason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrepRetirementReason As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgrcRRCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcRRName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrNavigation1 As Presentation.Controls.CtrNavigation
    Friend WithEvents INDlbEmployeeName As System.Windows.Forms.Label
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDlyItemPanelLiquidation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyControlEmployeeLiquidation As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtTotalPaid As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtPosition As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtSalaryBase As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtEndDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtInitialDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtEmployee As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtGroup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGroupGeneralImformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemTotalPaid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemEndDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPosition As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSalaryBase As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDgcLiquidationDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyGroupLiquidationDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemLiquidationDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGroupHumanTalent As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemEmployeeBack As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemEmployeeName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDlyItemEmployees As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemHumanTalent As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAddEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtRetirementReason As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemRetirementReason As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColumnDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnInitialDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumnEndingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColumAccrued As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDgcMessage As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvMessage As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemMessageLiquidation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGroupMessageLiquidation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDcolInfoMessage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolDescriptionMessage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents INDcolDeducted As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDeResolutionDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDtxtResolutionNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemResolutionNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemResolutionDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Acciones As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents PopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents INDPopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDteResultFormula As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteReplaceFormula As System.Windows.Forms.TextBox
    Friend WithEvents INDteUsedFormula As System.Windows.Forms.TextBox
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyfUsedFormula1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyfReplaceFormula1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyfResultFormula1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcEmployerLiquidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcNameConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGcValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDColActions2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDPopUpContainerControl2 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDteResultFormulaEmployer As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteReplaceFormulaEmployer As System.Windows.Forms.TextBox
    Friend WithEvents INDteUsedFormulaEmployer As System.Windows.Forms.TextBox
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
