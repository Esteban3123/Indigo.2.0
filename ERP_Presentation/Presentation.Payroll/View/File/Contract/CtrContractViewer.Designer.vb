Imports System.Drawing

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrContractViewer
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim StyleFormatCondition1 As DevExpress.XtraGrid.StyleFormatCondition = New DevExpress.XtraGrid.StyleFormatCondition()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDlyCtlContractViewer = New DevExpress.XtraLayout.LayoutControl()
        Me.INDlblResolutionDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblPosesionNumber = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblResolutionNumber = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblPosesionDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblJobBondingDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDpccModifyContract = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDCtrContractModify = New Presentation.Payroll.CtrContractModify()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDbbiAddNewContract = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbbiRenewContract = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbbiFinishContract = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbbiDeleteContract = New DevExpress.XtraBars.BarButtonItem()
        Me.INDbbiActivateContract = New DevExpress.XtraBars.BarButtonItem()
        Me.RepositoryItemButtonEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDpccFinishContract = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDlyCtlFinishContract = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbtnConfirmRetirement = New DevExpress.XtraEditors.SimpleButton()
        Me.INDdteActualRetirementDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleActualRetirementReason = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgrvSleActualRetirementReason = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcRetirementReasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcRetirementReasonDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlcgFinishContract = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciConfirmRetirement = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgFinishActualContract = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciActualRetirementDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciActualRetirementReason = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDpccContractEdit = New DevExpress.XtraBars.PopupControlContainer(Me.components)
        Me.INDctrContractEdit = New Presentation.Payroll.CtrContractEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDpccPositionInfo = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDpcePosition = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDlblRowType = New DevExpress.XtraEditors.LabelControl()
        Me.INDgrdContractHistory = New DevExpress.XtraGrid.GridControl()
        Me.INDgrvGrdContractHistory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcInitialContract = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcRowType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepRowType = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDgrcContractNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcPosition = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcInitialDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcEndingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcAction = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrepContractHistoryMoreActionPop = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDpccOldContractViewer = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyCtlOldContractViewer = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgrdContractCompare = New DevExpress.XtraGrid.GridControl()
        Me.INDgrvGrdContractCompare = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcSelectedContract = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcActualContract = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlblCompareAction = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldPaymentType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldSalaryType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldPaymentPeriod = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldRetirementDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldRetirementReason = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldBasicSalary = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldContractEndingDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldContractInitialDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldJobBondingDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldJobBondingType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldContractType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldFunctionalUnit = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldPosition = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblOldRowType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlcgOldContractViewer = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCompareAction = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgOldContractInfo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciOldPaymentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldPaymentPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldRetirementReason = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldBasicSalary = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldContractInitialDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldJobBondingType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldContractType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldPosition = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldRowType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldJobBondingDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldContractEndingDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldRetirementDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciOldSalaryType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgContractCompare = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciContractCompare = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDrepContractHistoryMoreAction = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDgrdHistoryChanges = New DevExpress.XtraGrid.GridControl()
        Me.INDgrvHistoryChanges = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDgrcHistGroupLevel1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcHistGroupLevel2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcHistType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcHistValueOld = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcHistValueNew = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcHistUserCodeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgrcHistDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlblPaymentType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblPaymentPeriod = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblSalaryType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblRetirementDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblRetirementReason = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblJobBondingType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblContractType = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblShowContractHistory = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblShowMoreInfo = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblEndingDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblStartDate = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblCostCenter = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblFunctionalUnit = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblGroup = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblSalary = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblPosition = New DevExpress.XtraEditors.LabelControl()
        Me.INDlblContractNo = New DevExpress.XtraEditors.LabelControl()
        Me.INDbtnContractViewerActions = New DevExpress.XtraEditors.DropDownButton()
        Me.INDppmContractViewerActions = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.INDlcgContractViewer = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciContractViewerActions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciContractNo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPositionOld = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCostCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgContractMoreInfo = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciContractType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciRetirementReason = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciRetirementDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPaymentPeriod = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPaymentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciJobBondingType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciShowMoreInfo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciShowContractHistory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciSalaryType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgContractHistory = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciContractHistory = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgHistoryChanges = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciHistoryChanges = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciRowType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciStartDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciEndingDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciSalary = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPosition = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlciJobBondingDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciResolutionNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCertificateOfficeNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPosesionDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciResolutionDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtlContractViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtlContractViewer.SuspendLayout()
        CType(Me.INDpccModifyContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccModifyContract.SuspendLayout()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccFinishContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccFinishContract.SuspendLayout()
        CType(Me.INDlyCtlFinishContract, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtlFinishContract.SuspendLayout()
        CType(Me.INDdteActualRetirementDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteActualRetirementDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleActualRetirementReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvSleActualRetirementReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgFinishContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciConfirmRetirement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgFinishActualContract, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciActualRetirementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciActualRetirementReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccContractEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccContractEdit.SuspendLayout()
        CType(Me.INDpccPositionInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpcePosition.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrdContractHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvGrdContractHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepRowType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepContractHistoryMoreActionPop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccOldContractViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccOldContractViewer.SuspendLayout()
        CType(Me.INDlyCtlOldContractViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtlOldContractViewer.SuspendLayout()
        CType(Me.INDgrdContractCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvGrdContractCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgOldContractViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCompareAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgOldContractInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldPaymentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldPaymentPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldRetirementReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldBasicSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldContractInitialDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldJobBondingType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldRowType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldJobBondingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldContractEndingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldRetirementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciOldSalaryType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgContractCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciContractCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrepContractHistoryMoreAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrdHistoryChanges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgrvHistoryChanges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDppmContractViewerActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgContractViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciContractViewerActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciContractNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPositionOld, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgContractMoreInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciRetirementReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciRetirementDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPaymentPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPaymentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciJobBondingType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciShowMoreInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciShowContractHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciSalaryType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgContractHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciContractHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgHistoryChanges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciHistoryChanges, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciRowType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEndingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPosition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciJobBondingDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciResolutionNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCertificateOfficeNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPosesionDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciResolutionDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'INDlyCtlContractViewer
        '
        Me.INDlyCtlContractViewer.AllowCustomization = False
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblResolutionDate)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblPosesionNumber)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblResolutionNumber)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblPosesionDate)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblJobBondingDate)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDpccModifyContract)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDpccFinishContract)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDpccContractEdit)
        Me.INDlyCtlContractViewer.Controls.Add(Me.LabelControl1)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDpccPositionInfo)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDpcePosition)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblRowType)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDgrdContractHistory)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDgrdHistoryChanges)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblPaymentType)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblPaymentPeriod)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblSalaryType)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblRetirementDate)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblRetirementReason)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblJobBondingType)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblContractType)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblShowContractHistory)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblShowMoreInfo)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblEndingDate)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblStartDate)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblCostCenter)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblFunctionalUnit)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblGroup)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblSalary)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblPosition)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDlblContractNo)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDbtnContractViewerActions)
        Me.INDlyCtlContractViewer.Controls.Add(Me.INDpccOldContractViewer)
        Me.INDlyCtlContractViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyCtlContractViewer.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCtlContractViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlyCtlContractViewer.Name = "INDlyCtlContractViewer"
        Me.INDlyCtlContractViewer.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(561, 143, 861, 568)
        Me.INDlyCtlContractViewer.Root = Me.INDlcgContractViewer
        Me.INDlyCtlContractViewer.Size = New System.Drawing.Size(800, 700)
        Me.INDlyCtlContractViewer.TabIndex = 0
        Me.INDlyCtlContractViewer.Text = "LayoutControl1"
        '
        'INDlblResolutionDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblResolutionDate, True)
        Me.INDlblResolutionDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblResolutionDate.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblResolutionDate, False)
        Me.INDlblResolutionDate.Location = New System.Drawing.Point(564, 38)
        Me.INDlblResolutionDate.Name = "INDlblResolutionDate"
        Me.INDlblResolutionDate.Size = New System.Drawing.Size(214, 26)
        Me.INDlblResolutionDate.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblResolutionDate.TabIndex = 47
        Me.INDlblResolutionDate.Text = "LabelControl3"
        '
        'INDlblPosesionNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblPosesionNumber, True)
        Me.INDlblPosesionNumber.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblPosesionNumber.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblPosesionNumber, False)
        Me.INDlblPosesionNumber.Location = New System.Drawing.Point(174, 68)
        Me.INDlblPosesionNumber.Name = "INDlblPosesionNumber"
        Me.INDlblPosesionNumber.Size = New System.Drawing.Size(214, 26)
        Me.INDlblPosesionNumber.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblPosesionNumber.TabIndex = 46
        Me.INDlblPosesionNumber.Text = "LabelControl2"
        '
        'INDlblResolutionNumber
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblResolutionNumber, True)
        Me.INDlblResolutionNumber.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblResolutionNumber.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblResolutionNumber, False)
        Me.INDlblResolutionNumber.Location = New System.Drawing.Point(174, 38)
        Me.INDlblResolutionNumber.Name = "INDlblResolutionNumber"
        Me.INDlblResolutionNumber.Size = New System.Drawing.Size(214, 26)
        Me.INDlblResolutionNumber.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblResolutionNumber.TabIndex = 45
        Me.INDlblResolutionNumber.Text = "LabelControl2"
        '
        'INDlblPosesionDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblPosesionDate, True)
        Me.INDlblPosesionDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblPosesionDate.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblPosesionDate, False)
        Me.INDlblPosesionDate.Location = New System.Drawing.Point(564, 68)
        Me.INDlblPosesionDate.Name = "INDlblPosesionDate"
        Me.INDlblPosesionDate.Size = New System.Drawing.Size(214, 26)
        Me.INDlblPosesionDate.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblPosesionDate.TabIndex = 44
        Me.INDlblPosesionDate.Text = "LabelControl2"
        '
        'INDlblJobBondingDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblJobBondingDate, True)
        Me.INDlblJobBondingDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblJobBondingDate.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblJobBondingDate, False)
        Me.INDlblJobBondingDate.Location = New System.Drawing.Point(564, 98)
        Me.INDlblJobBondingDate.Name = "INDlblJobBondingDate"
        Me.INDlblJobBondingDate.Size = New System.Drawing.Size(214, 26)
        Me.INDlblJobBondingDate.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblJobBondingDate.TabIndex = 43
        Me.INDlblJobBondingDate.Text = "LabelControl2"
        '
        'INDpccModifyContract
        '
        Me.INDpccModifyContract.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpccModifyContract.CloseOnLostFocus = False
        Me.INDpccModifyContract.Controls.Add(Me.INDCtrContractModify)
        Me.INDpccModifyContract.Location = New System.Drawing.Point(797, 33)
        Me.INDpccModifyContract.Manager = Me.BarManager1
        Me.INDpccModifyContract.Name = "INDpccModifyContract"
        Me.INDpccModifyContract.Size = New System.Drawing.Size(690, 499)
        Me.INDpccModifyContract.TabIndex = 42
        Me.INDpccModifyContract.Visible = False
        '
        'INDCtrContractModify
        '
        Me.INDCtrContractModify.Contingency = Nothing
        Me.INDCtrContractModify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDCtrContractModify.Location = New System.Drawing.Point(0, 0)
        Me.INDCtrContractModify.Name = "INDCtrContractModify"
        Me.INDCtrContractModify.Size = New System.Drawing.Size(690, 499)
        Me.INDCtrContractModify.TabIndex = 0
        '
        'BarManager1
        '
        Me.BarManager1.AllowCustomization = False
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDbbiAddNewContract, Me.INDbbiRenewContract, Me.INDbbiFinishContract, Me.INDbbiDeleteContract, Me.INDbbiActivateContract})
        Me.BarManager1.MaxItemId = 9
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonEdit1})
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(800, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 700)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(800, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 700)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(800, 0)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 700)
        '
        'INDbbiAddNewContract
        '
        Me.INDbbiAddNewContract.Caption = "Nueva Contratación"
        Me.INDbbiAddNewContract.Id = 0
        Me.INDbbiAddNewContract.Name = "INDbbiAddNewContract"
        '
        'INDbbiRenewContract
        '
        Me.INDbbiRenewContract.Caption = "Modificar Contratación"
        Me.INDbbiRenewContract.Id = 3
        Me.INDbbiRenewContract.Name = "INDbbiRenewContract"
        '
        'INDbbiFinishContract
        '
        Me.INDbbiFinishContract.Caption = "Terminar Contratación"
        Me.INDbbiFinishContract.Id = 4
        Me.INDbbiFinishContract.Name = "INDbbiFinishContract"
        '
        'INDbbiDeleteContract
        '
        Me.INDbbiDeleteContract.Caption = "Eliminar Contrato"
        Me.INDbbiDeleteContract.Id = 5
        Me.INDbbiDeleteContract.Name = "INDbbiDeleteContract"
        '
        'INDbbiActivateContract
        '
        Me.INDbbiActivateContract.Caption = "Activar Contrato"
        Me.INDbbiActivateContract.Id = 8
        Me.INDbbiActivateContract.Name = "INDbbiActivateContract"
        '
        'RepositoryItemButtonEdit1
        '
        Me.RepositoryItemButtonEdit1.AutoHeight = False
        Me.RepositoryItemButtonEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemButtonEdit1.Name = "RepositoryItemButtonEdit1"
        '
        'INDpccFinishContract
        '
        Me.INDpccFinishContract.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpccFinishContract.CloseOnLostFocus = False
        Me.INDpccFinishContract.Controls.Add(Me.INDlyCtlFinishContract)
        Me.INDpccFinishContract.Location = New System.Drawing.Point(116, 58)
        Me.INDpccFinishContract.Manager = Me.BarManager1
        Me.INDpccFinishContract.Name = "INDpccFinishContract"
        Me.INDpccFinishContract.Size = New System.Drawing.Size(440, 232)
        Me.INDpccFinishContract.TabIndex = 41
        Me.INDpccFinishContract.Visible = False
        '
        'INDlyCtlFinishContract
        '
        Me.INDlyCtlFinishContract.AllowCustomization = False
        Me.INDlyCtlFinishContract.Controls.Add(Me.INDbtnConfirmRetirement)
        Me.INDlyCtlFinishContract.Controls.Add(Me.INDdteActualRetirementDate)
        Me.INDlyCtlFinishContract.Controls.Add(Me.INDsleActualRetirementReason)
        Me.INDlyCtlFinishContract.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyCtlFinishContract.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCtlFinishContract.Name = "INDlyCtlFinishContract"
        Me.INDlyCtlFinishContract.Root = Me.INDlcgFinishContract
        Me.INDlyCtlFinishContract.Size = New System.Drawing.Size(440, 232)
        Me.INDlyCtlFinishContract.TabIndex = 0
        Me.INDlyCtlFinishContract.Text = "LayoutControl1"
        '
        'INDbtnConfirmRetirement
        '
        Me.INDbtnConfirmRetirement.Location = New System.Drawing.Point(12, 188)
        Me.INDbtnConfirmRetirement.Name = "INDbtnConfirmRetirement"
        Me.INDbtnConfirmRetirement.Size = New System.Drawing.Size(416, 32)
        Me.INDbtnConfirmRetirement.StyleController = Me.INDlyCtlFinishContract
        Me.INDbtnConfirmRetirement.TabIndex = 6
        Me.INDbtnConfirmRetirement.Text = "Terminar Contratación"
        '
        'INDdteActualRetirementDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteActualRetirementDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteActualRetirementDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteActualRetirementDate, False)
        Me.INDdteActualRetirementDate.EditValue = Nothing
        Me.INDdteActualRetirementDate.EnterMoveNextControl = True
        Me.INDdteActualRetirementDate.Location = New System.Drawing.Point(216, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteActualRetirementDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteActualRetirementDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteActualRetirementDate.MenuManager = Me.BarManager1
        Me.INDdteActualRetirementDate.Name = "INDdteActualRetirementDate"
        Me.INDdteActualRetirementDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteActualRetirementDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteActualRetirementDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteActualRetirementDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteActualRetirementDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdteActualRetirementDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdteActualRetirementDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteActualRetirementDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdteActualRetirementDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdteActualRetirementDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteActualRetirementDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteActualRetirementDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteActualRetirementDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdteActualRetirementDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteActualRetirementDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteActualRetirementDate.Size = New System.Drawing.Size(194, 28)
        Me.INDdteActualRetirementDate.StyleController = Me.INDlyCtlFinishContract
        Me.INDdteActualRetirementDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteActualRetirementDate, 0)
        '
        'INDsleActualRetirementReason
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleActualRetirementReason, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleActualRetirementReason, False)
        Me.INDsleActualRetirementReason.EnterMoveNextControl = True
        Me.INDsleActualRetirementReason.Location = New System.Drawing.Point(216, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleActualRetirementReason, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleActualRetirementReason.MenuManager = Me.BarManager1
        Me.INDsleActualRetirementReason.Name = "INDsleActualRetirementReason"
        Me.INDsleActualRetirementReason.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleActualRetirementReason.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleActualRetirementReason.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleActualRetirementReason.Properties.Appearance.Options.UseFont = True
        Me.INDsleActualRetirementReason.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleActualRetirementReason.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleActualRetirementReason.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleActualRetirementReason.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleActualRetirementReason.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleActualRetirementReason.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleActualRetirementReason.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleActualRetirementReason.Properties.DisplayMember = "Descripcion"
        Me.INDsleActualRetirementReason.Properties.NullText = ""
        Me.INDsleActualRetirementReason.Properties.PopupView = Me.INDgrvSleActualRetirementReason
        Me.INDsleActualRetirementReason.Properties.ValueMember = "Id"
        Me.INDsleActualRetirementReason.Size = New System.Drawing.Size(194, 28)
        Me.INDsleActualRetirementReason.StyleController = Me.INDlyCtlFinishContract
        Me.INDsleActualRetirementReason.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleActualRetirementReason, 0)
        '
        'INDgrvSleActualRetirementReason
        '
        Me.INDgrvSleActualRetirementReason.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvSleActualRetirementReason.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvSleActualRetirementReason.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvSleActualRetirementReason.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvSleActualRetirementReason.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvSleActualRetirementReason.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvSleActualRetirementReason.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvSleActualRetirementReason.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvSleActualRetirementReason.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvSleActualRetirementReason.Appearance.Row.Options.UseFont = True
        Me.INDgrvSleActualRetirementReason.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcRetirementReasonCode, Me.INDgrcRetirementReasonDescription})
        Me.INDgrvSleActualRetirementReason.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgrvSleActualRetirementReason.Name = "INDgrvSleActualRetirementReason"
        Me.INDgrvSleActualRetirementReason.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgrvSleActualRetirementReason.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvSleActualRetirementReason.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvSleActualRetirementReason.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvSleActualRetirementReason.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvSleActualRetirementReason, False)
        '
        'INDgrcRetirementReasonCode
        '
        Me.INDgrcRetirementReasonCode.Caption = "Código"
        Me.INDgrcRetirementReasonCode.FieldName = "Codigo"
        Me.INDgrcRetirementReasonCode.Name = "INDgrcRetirementReasonCode"
        Me.INDgrcRetirementReasonCode.Visible = True
        Me.INDgrcRetirementReasonCode.VisibleIndex = 0
        Me.INDgrcRetirementReasonCode.Width = 182
        '
        'INDgrcRetirementReasonDescription
        '
        Me.INDgrcRetirementReasonDescription.Caption = "Descripción"
        Me.INDgrcRetirementReasonDescription.FieldName = "Descripcion"
        Me.INDgrcRetirementReasonDescription.Name = "INDgrcRetirementReasonDescription"
        Me.INDgrcRetirementReasonDescription.Visible = True
        Me.INDgrcRetirementReasonDescription.VisibleIndex = 1
        Me.INDgrcRetirementReasonDescription.Width = 514
        '
        'INDlcgFinishContract
        '
        Me.INDlcgFinishContract.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgFinishContract.AppearanceGroup.Options.UseFont = True
        Me.INDlcgFinishContract.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgFinishContract.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgFinishContract.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFinishContract.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgFinishContract.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgFinishContract.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgFinishContract.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFinishContract.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgFinishContract.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFinishContract.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgFinishContract.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFinishContract.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgFinishContract, False)
        Me.INDlcgFinishContract.CustomizationFormText = "INDlcgFinishContract"
        Me.INDlcgFinishContract.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgFinishContract.GroupBordersVisible = False
        Me.INDlcgFinishContract.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciConfirmRetirement, Me.INDlcgFinishActualContract, Me.EmptySpaceItem2})
        Me.INDlcgFinishContract.Name = "INDlcgFinishContract"
        Me.INDlcgFinishContract.Size = New System.Drawing.Size(440, 232)
        Me.INDlcgFinishContract.TextVisible = False
        '
        'INDlciConfirmRetirement
        '
        Me.INDlciConfirmRetirement.Control = Me.INDbtnConfirmRetirement
        Me.INDlciConfirmRetirement.CustomizationFormText = "Terminar Contratación"
        Me.INDlciConfirmRetirement.Location = New System.Drawing.Point(0, 176)
        Me.INDlciConfirmRetirement.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlciConfirmRetirement.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciConfirmRetirement.Name = "INDlciConfirmRetirement"
        Me.INDlciConfirmRetirement.Size = New System.Drawing.Size(420, 36)
        Me.INDlciConfirmRetirement.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciConfirmRetirement.Text = "Terminar Contratación"
        Me.INDlciConfirmRetirement.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciConfirmRetirement.TextVisible = False
        '
        'INDlcgFinishActualContract
        '
        Me.INDlcgFinishActualContract.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgFinishActualContract.AppearanceGroup.Options.UseFont = True
        Me.INDlcgFinishActualContract.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgFinishActualContract.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgFinishActualContract.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFinishActualContract.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgFinishActualContract.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgFinishActualContract.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgFinishActualContract.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFinishActualContract.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgFinishActualContract.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFinishActualContract.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgFinishActualContract.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgFinishActualContract.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgFinishActualContract, False)
        Me.INDlcgFinishActualContract.CustomizationFormText = "Terminar Contratación"
        Me.INDlcgFinishActualContract.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciActualRetirementDate, Me.INDlciActualRetirementReason})
        Me.INDlcgFinishActualContract.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgFinishActualContract.Name = "INDlcgFinishActualContract"
        Me.INDlcgFinishActualContract.Size = New System.Drawing.Size(420, 125)
        Me.INDlcgFinishActualContract.Text = "Terminar Contratación"
        '
        'INDlciActualRetirementDate
        '
        Me.INDlciActualRetirementDate.Control = Me.INDdteActualRetirementDate
        Me.INDlciActualRetirementDate.CustomizationFormText = "Fecha de Retiro"
        Me.INDlciActualRetirementDate.Location = New System.Drawing.Point(0, 36)
        Me.INDlciActualRetirementDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciActualRetirementDate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciActualRetirementDate.Name = "INDlciActualRetirementDate"
        Me.INDlciActualRetirementDate.Size = New System.Drawing.Size(396, 36)
        Me.INDlciActualRetirementDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciActualRetirementDate.Text = "Fecha de Retiro"
        Me.INDlciActualRetirementDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciActualRetirementDate.TextSize = New System.Drawing.Size(180, 12)
        Me.INDlciActualRetirementDate.TextToControlDistance = 12
        '
        'INDlciActualRetirementReason
        '
        Me.INDlciActualRetirementReason.Control = Me.INDsleActualRetirementReason
        Me.INDlciActualRetirementReason.CustomizationFormText = "Razón de Retiro"
        Me.INDlciActualRetirementReason.Location = New System.Drawing.Point(0, 0)
        Me.INDlciActualRetirementReason.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciActualRetirementReason.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciActualRetirementReason.Name = "INDlciActualRetirementReason"
        Me.INDlciActualRetirementReason.Size = New System.Drawing.Size(396, 36)
        Me.INDlciActualRetirementReason.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciActualRetirementReason.Text = "Razón de Retiro"
        Me.INDlciActualRetirementReason.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciActualRetirementReason.TextSize = New System.Drawing.Size(180, 12)
        Me.INDlciActualRetirementReason.TextToControlDistance = 12
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 125)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(420, 51)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDpccContractEdit
        '
        Me.INDpccContractEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpccContractEdit.CloseOnLostFocus = False
        Me.INDpccContractEdit.Controls.Add(Me.INDctrContractEdit)
        Me.INDpccContractEdit.Location = New System.Drawing.Point(783, 450)
        Me.INDpccContractEdit.Manager = Me.BarManager1
        Me.INDpccContractEdit.Name = "INDpccContractEdit"
        Me.INDpccContractEdit.Size = New System.Drawing.Size(700, 480)
        Me.INDpccContractEdit.TabIndex = 40
        Me.INDpccContractEdit.Visible = False
        '
        'INDctrContractEdit
        '
        Me.INDctrContractEdit.Contingency = CType(0, Byte)
        Me.INDctrContractEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDctrContractEdit.Location = New System.Drawing.Point(0, 0)
        Me.INDctrContractEdit.Name = "INDctrContractEdit"
        Me.INDctrContractEdit.Size = New System.Drawing.Size(700, 480)
        Me.INDctrContractEdit.TabIndex = 0
        '
        'LabelControl1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl1, False)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl1, False)
        Me.LabelControl1.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(246, 32)
        Me.LabelControl1.StyleController = Me.INDlyCtlContractViewer
        Me.LabelControl1.TabIndex = 38
        Me.LabelControl1.Text = "Información de Contratos"
        '
        'INDpccPositionInfo
        '
        Me.INDpccPositionInfo.Location = New System.Drawing.Point(3, 697)
        Me.INDpccPositionInfo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDpccPositionInfo.Name = "INDpccPositionInfo"
        Me.INDpccPositionInfo.Size = New System.Drawing.Size(581, 349)
        Me.INDpccPositionInfo.TabIndex = 37
        '
        'INDpcePosition
        '
        Me.INDpcePosition.EditValue = "Prueba"
        Me.INDpcePosition.Location = New System.Drawing.Point(174, 158)
        Me.INDpcePosition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDpcePosition.Name = "INDpcePosition"
        Me.INDpcePosition.Properties.AllowFocused = False
        Me.INDpcePosition.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDpcePosition.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.INDpcePosition.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpcePosition.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpcePosition.Properties.Appearance.Options.UseBackColor = True
        Me.INDpcePosition.Properties.Appearance.Options.UseBorderColor = True
        Me.INDpcePosition.Properties.Appearance.Options.UseFont = True
        Me.INDpcePosition.Properties.Appearance.Options.UseForeColor = True
        Me.INDpcePosition.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDpcePosition.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpcePosition.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpcePosition.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDpcePosition.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDpcePosition.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDpcePosition.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDpcePosition.Properties.PopupControl = Me.INDpccPositionInfo
        Me.INDpcePosition.Properties.PopupSizeable = False
        Me.INDpcePosition.Properties.ShowPopupCloseButton = False
        Me.INDpcePosition.Size = New System.Drawing.Size(214, 26)
        Me.INDpcePosition.StyleController = Me.INDlyCtlContractViewer
        Me.INDpcePosition.TabIndex = 36
        '
        'INDlblRowType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblRowType, False)
        Me.INDlblRowType.Appearance.BackColor = System.Drawing.Color.LightGray
        Me.INDlblRowType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblRowType.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDlblRowType.Appearance.Options.UseBackColor = True
        Me.INDlblRowType.Appearance.Options.UseFont = True
        Me.INDlblRowType.Appearance.Options.UseForeColor = True
        Me.INDlblRowType.Appearance.Options.UseTextOptions = True
        Me.INDlblRowType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblRowType, False)
        Me.INDlblRowType.Location = New System.Drawing.Point(250, 0)
        Me.INDlblRowType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblRowType.Name = "INDlblRowType"
        Me.INDlblRowType.Size = New System.Drawing.Size(470, 36)
        Me.INDlblRowType.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblRowType.TabIndex = 17
        Me.INDlblRowType.Text = "LabelControl1"
        '
        'INDgrdContractHistory
        '
        Me.INDgrdContractHistory.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgrdContractHistory.Location = New System.Drawing.Point(3, 460)
        Me.INDgrdContractHistory.MainView = Me.INDgrvGrdContractHistory
        Me.INDgrdContractHistory.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgrdContractHistory.Name = "INDgrdContractHistory"
        Me.INDgrdContractHistory.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrepRowType, Me.INDrepContractHistoryMoreAction, Me.INDrepContractHistoryMoreActionPop})
        Me.INDgrdContractHistory.Size = New System.Drawing.Size(794, 105)
        Me.INDgrdContractHistory.TabIndex = 34
        Me.INDgrdContractHistory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgrvGrdContractHistory})
        '
        'INDgrvGrdContractHistory
        '
        Me.INDgrvGrdContractHistory.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvGrdContractHistory.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvGrdContractHistory.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvGrdContractHistory.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvGrdContractHistory.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvGrdContractHistory.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvGrdContractHistory.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvGrdContractHistory.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvGrdContractHistory.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvGrdContractHistory.Appearance.Row.Options.UseFont = True
        Me.INDgrvGrdContractHistory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcInitialContract, Me.INDgrcRowType, Me.INDgrcContractNo, Me.INDgrcPosition, Me.INDgrcInitialDate, Me.INDgrcEndingDate, Me.INDgrcAction})
        Me.INDgrvGrdContractHistory.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgrvGrdContractHistory.GridControl = Me.INDgrdContractHistory
        Me.INDgrvGrdContractHistory.GroupCount = 1
        Me.INDgrvGrdContractHistory.Name = "INDgrvGrdContractHistory"
        Me.INDgrvGrdContractHistory.OptionsDetail.EnableMasterViewMode = False
        Me.INDgrvGrdContractHistory.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgrvGrdContractHistory.OptionsSelection.UseIndicatorForSelection = False
        Me.INDgrvGrdContractHistory.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvGrdContractHistory.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvGrdContractHistory.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvGrdContractHistory.OptionsView.ShowGroupPanel = False
        Me.INDgrvGrdContractHistory.OptionsView.ShowIndicator = False
        Me.INDgrvGrdContractHistory.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDgrcInitialContract, DevExpress.Data.ColumnSortOrder.Descending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDgrcContractNo, DevExpress.Data.ColumnSortOrder.Descending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvGrdContractHistory, False)
        '
        'INDgrcInitialContract
        '
        Me.INDgrcInitialContract.Caption = "Contrato Inicial"
        Me.INDgrcInitialContract.FieldName = "InitialContractNumber"
        Me.INDgrcInitialContract.Name = "INDgrcInitialContract"
        Me.INDgrcInitialContract.OptionsColumn.AllowEdit = False
        Me.INDgrcInitialContract.OptionsColumn.AllowMove = False
        Me.INDgrcInitialContract.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDgrcInitialContract.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.INDgrcInitialContract.Visible = True
        Me.INDgrcInitialContract.VisibleIndex = 0
        '
        'INDgrcRowType
        '
        Me.INDgrcRowType.Caption = "Tipo de Registro"
        Me.INDgrcRowType.ColumnEdit = Me.INDrepRowType
        Me.INDgrcRowType.FieldName = "RowType"
        Me.INDgrcRowType.Name = "INDgrcRowType"
        Me.INDgrcRowType.OptionsColumn.AllowEdit = False
        Me.INDgrcRowType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcRowType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcRowType.Visible = True
        Me.INDgrcRowType.VisibleIndex = 0
        Me.INDgrcRowType.Width = 202
        '
        'INDrepRowType
        '
        Me.INDrepRowType.AutoHeight = False
        Me.INDrepRowType.Name = "INDrepRowType"
        '
        'INDgrcContractNo
        '
        Me.INDgrcContractNo.Caption = "Contrato No."
        Me.INDgrcContractNo.FieldName = "Id"
        Me.INDgrcContractNo.Name = "INDgrcContractNo"
        Me.INDgrcContractNo.OptionsColumn.AllowEdit = False
        Me.INDgrcContractNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcContractNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcContractNo.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.INDgrcContractNo.Visible = True
        Me.INDgrcContractNo.VisibleIndex = 1
        Me.INDgrcContractNo.Width = 233
        '
        'INDgrcPosition
        '
        Me.INDgrcPosition.Caption = "Cargo"
        Me.INDgrcPosition.FieldName = "Position.Name"
        Me.INDgrcPosition.Name = "INDgrcPosition"
        Me.INDgrcPosition.OptionsColumn.AllowEdit = False
        Me.INDgrcPosition.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcPosition.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcPosition.Visible = True
        Me.INDgrcPosition.VisibleIndex = 2
        Me.INDgrcPosition.Width = 264
        '
        'INDgrcInitialDate
        '
        Me.INDgrcInitialDate.Caption = "Fecha Inicial"
        Me.INDgrcInitialDate.DisplayFormat.FormatString = "{0:dd \de MMMM \de yyyy}"
        Me.INDgrcInitialDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcInitialDate.FieldName = "ContractInitialDate"
        Me.INDgrcInitialDate.Name = "INDgrcInitialDate"
        Me.INDgrcInitialDate.OptionsColumn.AllowEdit = False
        Me.INDgrcInitialDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcInitialDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcInitialDate.Visible = True
        Me.INDgrcInitialDate.VisibleIndex = 3
        Me.INDgrcInitialDate.Width = 264
        '
        'INDgrcEndingDate
        '
        Me.INDgrcEndingDate.Caption = "Fecha Final"
        Me.INDgrcEndingDate.DisplayFormat.FormatString = "{0:dd \de MMMM \de yyyy}"
        Me.INDgrcEndingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDgrcEndingDate.FieldName = "ContractEndingDate"
        Me.INDgrcEndingDate.Name = "INDgrcEndingDate"
        Me.INDgrcEndingDate.OptionsColumn.AllowEdit = False
        Me.INDgrcEndingDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcEndingDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDgrcEndingDate.Visible = True
        Me.INDgrcEndingDate.VisibleIndex = 4
        Me.INDgrcEndingDate.Width = 353
        '
        'INDgrcAction
        '
        Me.INDgrcAction.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrcAction.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgrcAction.AppearanceCell.Options.UseFont = True
        Me.INDgrcAction.AppearanceCell.Options.UseForeColor = True
        Me.INDgrcAction.AppearanceCell.Options.UseTextOptions = True
        Me.INDgrcAction.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDgrcAction.Caption = "Acciones"
        Me.INDgrcAction.ColumnEdit = Me.INDrepContractHistoryMoreActionPop
        Me.INDgrcAction.Name = "INDgrcAction"
        Me.INDgrcAction.Visible = True
        Me.INDgrcAction.VisibleIndex = 5
        Me.INDgrcAction.Width = 178
        '
        'INDrepContractHistoryMoreActionPop
        '
        Me.INDrepContractHistoryMoreActionPop.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepContractHistoryMoreActionPop.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDrepContractHistoryMoreActionPop.Appearance.Options.UseFont = True
        Me.INDrepContractHistoryMoreActionPop.Appearance.Options.UseForeColor = True
        Me.INDrepContractHistoryMoreActionPop.Appearance.Options.UseTextOptions = True
        Me.INDrepContractHistoryMoreActionPop.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepContractHistoryMoreActionPop.AutoHeight = False
        Me.INDrepContractHistoryMoreActionPop.Name = "INDrepContractHistoryMoreActionPop"
        Me.INDrepContractHistoryMoreActionPop.PopupControl = Me.INDpccOldContractViewer
        Me.INDrepContractHistoryMoreActionPop.PopupFormSize = New System.Drawing.Size(830, 520)
        Me.INDrepContractHistoryMoreActionPop.PopupSizeable = False
        Me.INDrepContractHistoryMoreActionPop.ShowPopupCloseButton = False
        '
        'INDpccOldContractViewer
        '
        Me.INDpccOldContractViewer.Controls.Add(Me.INDlyCtlOldContractViewer)
        Me.INDpccOldContractViewer.Location = New System.Drawing.Point(349, 474)
        Me.INDpccOldContractViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDpccOldContractViewer.Name = "INDpccOldContractViewer"
        Me.INDpccOldContractViewer.Size = New System.Drawing.Size(700, 645)
        Me.INDpccOldContractViewer.TabIndex = 35
        '
        'INDlyCtlOldContractViewer
        '
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDgrdContractCompare)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblCompareAction)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldPaymentType)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldSalaryType)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldPaymentPeriod)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldRetirementDate)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldRetirementReason)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldBasicSalary)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldContractEndingDate)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldContractInitialDate)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldJobBondingDate)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldJobBondingType)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldContractType)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldFunctionalUnit)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldPosition)
        Me.INDlyCtlOldContractViewer.Controls.Add(Me.INDlblOldRowType)
        Me.INDlyCtlOldContractViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyCtlOldContractViewer.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCtlOldContractViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlyCtlOldContractViewer.Name = "INDlyCtlOldContractViewer"
        Me.INDlyCtlOldContractViewer.Root = Me.INDlcgOldContractViewer
        Me.INDlyCtlOldContractViewer.Size = New System.Drawing.Size(700, 645)
        Me.INDlyCtlOldContractViewer.TabIndex = 0
        Me.INDlyCtlOldContractViewer.Text = "LayoutControl1"
        '
        'INDgrdContractCompare
        '
        Me.INDgrdContractCompare.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgrdContractCompare.Location = New System.Drawing.Point(24, 526)
        Me.INDgrdContractCompare.MainView = Me.INDgrvGrdContractCompare
        Me.INDgrdContractCompare.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgrdContractCompare.Name = "INDgrdContractCompare"
        Me.INDgrdContractCompare.Size = New System.Drawing.Size(635, 96)
        Me.INDgrdContractCompare.TabIndex = 19
        Me.INDgrdContractCompare.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgrvGrdContractCompare})
        '
        'INDgrvGrdContractCompare
        '
        Me.INDgrvGrdContractCompare.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvGrdContractCompare.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvGrdContractCompare.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvGrdContractCompare.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvGrdContractCompare.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvGrdContractCompare.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvGrdContractCompare.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvGrdContractCompare.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvGrdContractCompare.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvGrdContractCompare.Appearance.Row.Options.UseFont = True
        Me.INDgrvGrdContractCompare.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcConcept, Me.INDgrcSelectedContract, Me.INDgrcActualContract})
        StyleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        StyleFormatCondition1.Appearance.BackColor2 = System.Drawing.Color.White
        StyleFormatCondition1.Appearance.Options.UseBackColor = True
        StyleFormatCondition1.ApplyToRow = True
        StyleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression
        StyleFormatCondition1.Expression = "[Item3] != [Item2]"
        Me.INDgrvGrdContractCompare.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {StyleFormatCondition1})
        Me.INDgrvGrdContractCompare.GridControl = Me.INDgrdContractCompare
        Me.INDgrvGrdContractCompare.Name = "INDgrvGrdContractCompare"
        Me.INDgrvGrdContractCompare.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvGrdContractCompare.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvGrdContractCompare.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvGrdContractCompare.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvGrdContractCompare, False)
        '
        'INDgrcConcept
        '
        Me.INDgrcConcept.Caption = "Concepto"
        Me.INDgrcConcept.FieldName = "Item1"
        Me.INDgrcConcept.Name = "INDgrcConcept"
        Me.INDgrcConcept.OptionsColumn.AllowEdit = False
        Me.INDgrcConcept.Visible = True
        Me.INDgrcConcept.VisibleIndex = 0
        '
        'INDgrcSelectedContract
        '
        Me.INDgrcSelectedContract.Caption = "Contrato Seleccionado"
        Me.INDgrcSelectedContract.FieldName = "Item2"
        Me.INDgrcSelectedContract.Name = "INDgrcSelectedContract"
        Me.INDgrcSelectedContract.OptionsColumn.AllowEdit = False
        Me.INDgrcSelectedContract.Visible = True
        Me.INDgrcSelectedContract.VisibleIndex = 1
        '
        'INDgrcActualContract
        '
        Me.INDgrcActualContract.Caption = "Contrato Vigente/Último"
        Me.INDgrcActualContract.FieldName = "Item3"
        Me.INDgrcActualContract.Name = "INDgrcActualContract"
        Me.INDgrcActualContract.OptionsColumn.AllowEdit = False
        Me.INDgrcActualContract.Visible = True
        Me.INDgrcActualContract.VisibleIndex = 2
        '
        'INDlblCompareAction
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblCompareAction, False)
        Me.INDlblCompareAction.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Underline)
        Me.INDlblCompareAction.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlblCompareAction.Appearance.Options.UseFont = True
        Me.INDlblCompareAction.Appearance.Options.UseForeColor = True
        Me.INDlblCompareAction.Appearance.Options.UseTextOptions = True
        Me.INDlblCompareAction.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblCompareAction, False)
        Me.INDlblCompareAction.Location = New System.Drawing.Point(12, 638)
        Me.INDlblCompareAction.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblCompareAction.Name = "INDlblCompareAction"
        Me.INDlblCompareAction.Size = New System.Drawing.Size(659, 32)
        Me.INDlblCompareAction.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblCompareAction.TabIndex = 18
        Me.INDlblCompareAction.Text = "Ver/Ocultar Comparación"
        '
        'INDlblOldPaymentType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldPaymentType, False)
        Me.INDlblOldPaymentType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldPaymentType.Appearance.Options.UseFont = True
        Me.INDlblOldPaymentType.Appearance.Options.UseTextOptions = True
        Me.INDlblOldPaymentType.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldPaymentType.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldPaymentType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldPaymentType, False)
        Me.INDlblOldPaymentType.Location = New System.Drawing.Point(196, 413)
        Me.INDlblOldPaymentType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldPaymentType.Name = "INDlblOldPaymentType"
        Me.INDlblOldPaymentType.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldPaymentType.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldPaymentType.TabIndex = 17
        Me.INDlblOldPaymentType.Text = "LabelControl14"
        '
        'INDlblOldSalaryType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldSalaryType, False)
        Me.INDlblOldSalaryType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldSalaryType.Appearance.Options.UseFont = True
        Me.INDlblOldSalaryType.Appearance.Options.UseTextOptions = True
        Me.INDlblOldSalaryType.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldSalaryType.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldSalaryType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldSalaryType, False)
        Me.INDlblOldSalaryType.Location = New System.Drawing.Point(196, 443)
        Me.INDlblOldSalaryType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldSalaryType.Name = "INDlblOldSalaryType"
        Me.INDlblOldSalaryType.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldSalaryType.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldSalaryType.TabIndex = 16
        Me.INDlblOldSalaryType.Text = "LabelControl13"
        '
        'INDlblOldPaymentPeriod
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldPaymentPeriod, False)
        Me.INDlblOldPaymentPeriod.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldPaymentPeriod.Appearance.Options.UseFont = True
        Me.INDlblOldPaymentPeriod.Appearance.Options.UseTextOptions = True
        Me.INDlblOldPaymentPeriod.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldPaymentPeriod.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldPaymentPeriod.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldPaymentPeriod, False)
        Me.INDlblOldPaymentPeriod.Location = New System.Drawing.Point(196, 383)
        Me.INDlblOldPaymentPeriod.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldPaymentPeriod.Name = "INDlblOldPaymentPeriod"
        Me.INDlblOldPaymentPeriod.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldPaymentPeriod.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldPaymentPeriod.TabIndex = 15
        Me.INDlblOldPaymentPeriod.Text = "LabelControl12"
        '
        'INDlblOldRetirementDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldRetirementDate, False)
        Me.INDlblOldRetirementDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldRetirementDate.Appearance.Options.UseFont = True
        Me.INDlblOldRetirementDate.Appearance.Options.UseTextOptions = True
        Me.INDlblOldRetirementDate.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldRetirementDate.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldRetirementDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldRetirementDate, False)
        Me.INDlblOldRetirementDate.Location = New System.Drawing.Point(196, 353)
        Me.INDlblOldRetirementDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldRetirementDate.Name = "INDlblOldRetirementDate"
        Me.INDlblOldRetirementDate.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldRetirementDate.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldRetirementDate.TabIndex = 14
        Me.INDlblOldRetirementDate.Text = "LabelControl11"
        '
        'INDlblOldRetirementReason
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldRetirementReason, False)
        Me.INDlblOldRetirementReason.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldRetirementReason.Appearance.Options.UseFont = True
        Me.INDlblOldRetirementReason.Appearance.Options.UseTextOptions = True
        Me.INDlblOldRetirementReason.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldRetirementReason.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldRetirementReason.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldRetirementReason, False)
        Me.INDlblOldRetirementReason.Location = New System.Drawing.Point(196, 323)
        Me.INDlblOldRetirementReason.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldRetirementReason.Name = "INDlblOldRetirementReason"
        Me.INDlblOldRetirementReason.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldRetirementReason.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldRetirementReason.TabIndex = 13
        Me.INDlblOldRetirementReason.Text = "LabelControl10"
        '
        'INDlblOldBasicSalary
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldBasicSalary, False)
        Me.INDlblOldBasicSalary.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldBasicSalary.Appearance.Options.UseFont = True
        Me.INDlblOldBasicSalary.Appearance.Options.UseTextOptions = True
        Me.INDlblOldBasicSalary.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldBasicSalary.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldBasicSalary.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldBasicSalary, False)
        Me.INDlblOldBasicSalary.Location = New System.Drawing.Point(196, 293)
        Me.INDlblOldBasicSalary.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldBasicSalary.Name = "INDlblOldBasicSalary"
        Me.INDlblOldBasicSalary.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldBasicSalary.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldBasicSalary.TabIndex = 12
        Me.INDlblOldBasicSalary.Text = "LabelControl9"
        '
        'INDlblOldContractEndingDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldContractEndingDate, False)
        Me.INDlblOldContractEndingDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldContractEndingDate.Appearance.Options.UseFont = True
        Me.INDlblOldContractEndingDate.Appearance.Options.UseTextOptions = True
        Me.INDlblOldContractEndingDate.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldContractEndingDate.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldContractEndingDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldContractEndingDate, False)
        Me.INDlblOldContractEndingDate.Location = New System.Drawing.Point(196, 263)
        Me.INDlblOldContractEndingDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldContractEndingDate.Name = "INDlblOldContractEndingDate"
        Me.INDlblOldContractEndingDate.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldContractEndingDate.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldContractEndingDate.TabIndex = 11
        Me.INDlblOldContractEndingDate.Text = "LabelControl8"
        '
        'INDlblOldContractInitialDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldContractInitialDate, False)
        Me.INDlblOldContractInitialDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldContractInitialDate.Appearance.Options.UseFont = True
        Me.INDlblOldContractInitialDate.Appearance.Options.UseTextOptions = True
        Me.INDlblOldContractInitialDate.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldContractInitialDate.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldContractInitialDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldContractInitialDate, False)
        Me.INDlblOldContractInitialDate.Location = New System.Drawing.Point(196, 233)
        Me.INDlblOldContractInitialDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldContractInitialDate.Name = "INDlblOldContractInitialDate"
        Me.INDlblOldContractInitialDate.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldContractInitialDate.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldContractInitialDate.TabIndex = 10
        Me.INDlblOldContractInitialDate.Text = "LabelControl7"
        '
        'INDlblOldJobBondingDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldJobBondingDate, False)
        Me.INDlblOldJobBondingDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldJobBondingDate.Appearance.Options.UseFont = True
        Me.INDlblOldJobBondingDate.Appearance.Options.UseTextOptions = True
        Me.INDlblOldJobBondingDate.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldJobBondingDate.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldJobBondingDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldJobBondingDate, False)
        Me.INDlblOldJobBondingDate.Location = New System.Drawing.Point(196, 203)
        Me.INDlblOldJobBondingDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldJobBondingDate.Name = "INDlblOldJobBondingDate"
        Me.INDlblOldJobBondingDate.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldJobBondingDate.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldJobBondingDate.TabIndex = 9
        Me.INDlblOldJobBondingDate.Text = "LabelControl6"
        '
        'INDlblOldJobBondingType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldJobBondingType, False)
        Me.INDlblOldJobBondingType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldJobBondingType.Appearance.Options.UseFont = True
        Me.INDlblOldJobBondingType.Appearance.Options.UseTextOptions = True
        Me.INDlblOldJobBondingType.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldJobBondingType.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldJobBondingType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldJobBondingType, False)
        Me.INDlblOldJobBondingType.Location = New System.Drawing.Point(196, 173)
        Me.INDlblOldJobBondingType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldJobBondingType.Name = "INDlblOldJobBondingType"
        Me.INDlblOldJobBondingType.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldJobBondingType.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldJobBondingType.TabIndex = 8
        Me.INDlblOldJobBondingType.Text = "LabelControl5"
        '
        'INDlblOldContractType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldContractType, False)
        Me.INDlblOldContractType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldContractType.Appearance.Options.UseFont = True
        Me.INDlblOldContractType.Appearance.Options.UseTextOptions = True
        Me.INDlblOldContractType.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldContractType.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldContractType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldContractType, False)
        Me.INDlblOldContractType.Location = New System.Drawing.Point(196, 143)
        Me.INDlblOldContractType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldContractType.Name = "INDlblOldContractType"
        Me.INDlblOldContractType.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldContractType.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldContractType.TabIndex = 7
        Me.INDlblOldContractType.Text = "LabelControl4"
        '
        'INDlblOldFunctionalUnit
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldFunctionalUnit, False)
        Me.INDlblOldFunctionalUnit.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldFunctionalUnit.Appearance.Options.UseFont = True
        Me.INDlblOldFunctionalUnit.Appearance.Options.UseTextOptions = True
        Me.INDlblOldFunctionalUnit.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldFunctionalUnit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldFunctionalUnit.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldFunctionalUnit, False)
        Me.INDlblOldFunctionalUnit.Location = New System.Drawing.Point(196, 113)
        Me.INDlblOldFunctionalUnit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldFunctionalUnit.Name = "INDlblOldFunctionalUnit"
        Me.INDlblOldFunctionalUnit.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldFunctionalUnit.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldFunctionalUnit.TabIndex = 6
        Me.INDlblOldFunctionalUnit.Text = "LabelControl3"
        '
        'INDlblOldPosition
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldPosition, False)
        Me.INDlblOldPosition.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldPosition.Appearance.Options.UseFont = True
        Me.INDlblOldPosition.Appearance.Options.UseTextOptions = True
        Me.INDlblOldPosition.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblOldPosition.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblOldPosition.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldPosition, False)
        Me.INDlblOldPosition.Location = New System.Drawing.Point(196, 83)
        Me.INDlblOldPosition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldPosition.Name = "INDlblOldPosition"
        Me.INDlblOldPosition.Size = New System.Drawing.Size(463, 26)
        Me.INDlblOldPosition.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldPosition.TabIndex = 5
        Me.INDlblOldPosition.Text = "LabelControl2"
        '
        'INDlblOldRowType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblOldRowType, False)
        Me.INDlblOldRowType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblOldRowType.Appearance.Options.UseFont = True
        Me.INDlblOldRowType.Appearance.Options.UseTextOptions = True
        Me.INDlblOldRowType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblOldRowType, False)
        Me.INDlblOldRowType.Location = New System.Drawing.Point(24, 53)
        Me.INDlblOldRowType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblOldRowType.Name = "INDlblOldRowType"
        Me.INDlblOldRowType.Size = New System.Drawing.Size(635, 26)
        Me.INDlblOldRowType.StyleController = Me.INDlyCtlOldContractViewer
        Me.INDlblOldRowType.TabIndex = 4
        Me.INDlblOldRowType.Text = "LabelControl1"
        '
        'INDlcgOldContractViewer
        '
        Me.INDlcgOldContractViewer.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOldContractViewer.AppearanceGroup.Options.UseFont = True
        Me.INDlcgOldContractViewer.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOldContractViewer.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgOldContractViewer.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOldContractViewer.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgOldContractViewer.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgOldContractViewer.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgOldContractViewer.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOldContractViewer.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgOldContractViewer.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOldContractViewer.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgOldContractViewer.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOldContractViewer.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgOldContractViewer, False)
        Me.INDlcgOldContractViewer.CustomizationFormText = "INDlcgOldContractViewer"
        Me.INDlcgOldContractViewer.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgOldContractViewer.GroupBordersVisible = False
        Me.INDlcgOldContractViewer.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciCompareAction, Me.INDlcgOldContractInfo, Me.INDlcgContractCompare})
        Me.INDlcgOldContractViewer.Name = "INDlcgOldContractViewer"
        Me.INDlcgOldContractViewer.Size = New System.Drawing.Size(683, 682)
        Me.INDlcgOldContractViewer.TextVisible = False
        '
        'INDlciCompareAction
        '
        Me.INDlciCompareAction.Control = Me.INDlblCompareAction
        Me.INDlciCompareAction.ControlAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.INDlciCompareAction.CustomizationFormText = "Ver Comparación"
        Me.INDlciCompareAction.Location = New System.Drawing.Point(0, 626)
        Me.INDlciCompareAction.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlciCompareAction.MinSize = New System.Drawing.Size(1, 36)
        Me.INDlciCompareAction.Name = "INDlciCompareAction"
        Me.INDlciCompareAction.Size = New System.Drawing.Size(663, 36)
        Me.INDlciCompareAction.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCompareAction.Text = "Ver Comparación"
        Me.INDlciCompareAction.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciCompareAction.TextVisible = False
        '
        'INDlcgOldContractInfo
        '
        Me.INDlcgOldContractInfo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOldContractInfo.AppearanceGroup.Options.UseFont = True
        Me.INDlcgOldContractInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgOldContractInfo.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgOldContractInfo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOldContractInfo.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgOldContractInfo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgOldContractInfo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgOldContractInfo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOldContractInfo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgOldContractInfo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOldContractInfo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgOldContractInfo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgOldContractInfo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgOldContractInfo, False)
        Me.INDlcgOldContractInfo.CustomizationFormText = "Información del Contrato"
        Me.INDlcgOldContractInfo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciOldPaymentType, Me.INDlciOldPaymentPeriod, Me.INDlciOldRetirementReason, Me.INDlciOldBasicSalary, Me.INDlciOldContractInitialDate, Me.INDlciOldJobBondingType, Me.INDlciOldContractType, Me.INDlciOldFunctionalUnit, Me.INDlciOldPosition, Me.INDlciOldRowType, Me.INDlciOldJobBondingDate, Me.INDlciOldContractEndingDate, Me.INDlciOldRetirementDate, Me.INDlciOldSalaryType})
        Me.INDlcgOldContractInfo.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgOldContractInfo.Name = "INDlcgOldContractInfo"
        Me.INDlcgOldContractInfo.Size = New System.Drawing.Size(663, 473)
        Me.INDlcgOldContractInfo.Text = "Información del Contrato"
        '
        'INDlciOldPaymentType
        '
        Me.INDlciOldPaymentType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldPaymentType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldPaymentType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldPaymentType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldPaymentType.Control = Me.INDlblOldPaymentType
        Me.INDlciOldPaymentType.CustomizationFormText = "Tipo de Pago"
        Me.INDlciOldPaymentType.Location = New System.Drawing.Point(0, 360)
        Me.INDlciOldPaymentType.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldPaymentType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldPaymentType.Name = "INDlciOldPaymentType"
        Me.INDlciOldPaymentType.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldPaymentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldPaymentType.Text = "Tipo de Pago"
        Me.INDlciOldPaymentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldPaymentType.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldPaymentType.TextToControlDistance = 12
        '
        'INDlciOldPaymentPeriod
        '
        Me.INDlciOldPaymentPeriod.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldPaymentPeriod.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldPaymentPeriod.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldPaymentPeriod.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldPaymentPeriod.Control = Me.INDlblOldPaymentPeriod
        Me.INDlciOldPaymentPeriod.CustomizationFormText = "Período de Pago"
        Me.INDlciOldPaymentPeriod.Location = New System.Drawing.Point(0, 330)
        Me.INDlciOldPaymentPeriod.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldPaymentPeriod.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldPaymentPeriod.Name = "INDlciOldPaymentPeriod"
        Me.INDlciOldPaymentPeriod.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldPaymentPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldPaymentPeriod.Text = "Período de Pago"
        Me.INDlciOldPaymentPeriod.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldPaymentPeriod.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldPaymentPeriod.TextToControlDistance = 12
        '
        'INDlciOldRetirementReason
        '
        Me.INDlciOldRetirementReason.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldRetirementReason.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldRetirementReason.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldRetirementReason.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldRetirementReason.Control = Me.INDlblOldRetirementReason
        Me.INDlciOldRetirementReason.CustomizationFormText = "Razón de Retiro"
        Me.INDlciOldRetirementReason.Location = New System.Drawing.Point(0, 270)
        Me.INDlciOldRetirementReason.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldRetirementReason.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldRetirementReason.Name = "INDlciOldRetirementReason"
        Me.INDlciOldRetirementReason.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldRetirementReason.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldRetirementReason.Text = "Razón de Retiro"
        Me.INDlciOldRetirementReason.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldRetirementReason.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldRetirementReason.TextToControlDistance = 12
        '
        'INDlciOldBasicSalary
        '
        Me.INDlciOldBasicSalary.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldBasicSalary.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldBasicSalary.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldBasicSalary.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldBasicSalary.Control = Me.INDlblOldBasicSalary
        Me.INDlciOldBasicSalary.CustomizationFormText = "Salario"
        Me.INDlciOldBasicSalary.Location = New System.Drawing.Point(0, 240)
        Me.INDlciOldBasicSalary.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldBasicSalary.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldBasicSalary.Name = "INDlciOldBasicSalary"
        Me.INDlciOldBasicSalary.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldBasicSalary.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldBasicSalary.Text = "Salario"
        Me.INDlciOldBasicSalary.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldBasicSalary.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldBasicSalary.TextToControlDistance = 12
        '
        'INDlciOldContractInitialDate
        '
        Me.INDlciOldContractInitialDate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldContractInitialDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldContractInitialDate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldContractInitialDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldContractInitialDate.Control = Me.INDlblOldContractInitialDate
        Me.INDlciOldContractInitialDate.CustomizationFormText = "Fecha de Inicio"
        Me.INDlciOldContractInitialDate.Location = New System.Drawing.Point(0, 180)
        Me.INDlciOldContractInitialDate.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldContractInitialDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldContractInitialDate.Name = "INDlciOldContractInitialDate"
        Me.INDlciOldContractInitialDate.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldContractInitialDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldContractInitialDate.Text = "Fecha de Inicio"
        Me.INDlciOldContractInitialDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldContractInitialDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldContractInitialDate.TextToControlDistance = 12
        '
        'INDlciOldJobBondingType
        '
        Me.INDlciOldJobBondingType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldJobBondingType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldJobBondingType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldJobBondingType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldJobBondingType.Control = Me.INDlblOldJobBondingType
        Me.INDlciOldJobBondingType.CustomizationFormText = "Tipo de Vinculación"
        Me.INDlciOldJobBondingType.Location = New System.Drawing.Point(0, 120)
        Me.INDlciOldJobBondingType.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldJobBondingType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldJobBondingType.Name = "INDlciOldJobBondingType"
        Me.INDlciOldJobBondingType.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldJobBondingType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldJobBondingType.Text = "Tipo de Vinculación"
        Me.INDlciOldJobBondingType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldJobBondingType.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldJobBondingType.TextToControlDistance = 12
        '
        'INDlciOldContractType
        '
        Me.INDlciOldContractType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldContractType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldContractType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldContractType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldContractType.Control = Me.INDlblOldContractType
        Me.INDlciOldContractType.CustomizationFormText = "Tipo de Contrato"
        Me.INDlciOldContractType.Location = New System.Drawing.Point(0, 90)
        Me.INDlciOldContractType.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldContractType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldContractType.Name = "INDlciOldContractType"
        Me.INDlciOldContractType.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldContractType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldContractType.Text = "Tipo de Contrato"
        Me.INDlciOldContractType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldContractType.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldContractType.TextToControlDistance = 12
        '
        'INDlciOldFunctionalUnit
        '
        Me.INDlciOldFunctionalUnit.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldFunctionalUnit.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldFunctionalUnit.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldFunctionalUnit.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldFunctionalUnit.Control = Me.INDlblOldFunctionalUnit
        Me.INDlciOldFunctionalUnit.CustomizationFormText = "Unidad Funcional"
        Me.INDlciOldFunctionalUnit.Location = New System.Drawing.Point(0, 60)
        Me.INDlciOldFunctionalUnit.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldFunctionalUnit.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldFunctionalUnit.Name = "INDlciOldFunctionalUnit"
        Me.INDlciOldFunctionalUnit.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldFunctionalUnit.Text = "Unidad Funcional"
        Me.INDlciOldFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldFunctionalUnit.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldFunctionalUnit.TextToControlDistance = 12
        '
        'INDlciOldPosition
        '
        Me.INDlciOldPosition.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldPosition.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldPosition.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldPosition.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldPosition.Control = Me.INDlblOldPosition
        Me.INDlciOldPosition.CustomizationFormText = "Cargo"
        Me.INDlciOldPosition.Location = New System.Drawing.Point(0, 30)
        Me.INDlciOldPosition.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldPosition.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldPosition.Name = "INDlciOldPosition"
        Me.INDlciOldPosition.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldPosition.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldPosition.Text = "Cargo"
        Me.INDlciOldPosition.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldPosition.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldPosition.TextToControlDistance = 12
        '
        'INDlciOldRowType
        '
        Me.INDlciOldRowType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldRowType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldRowType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldRowType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldRowType.Control = Me.INDlblOldRowType
        Me.INDlciOldRowType.CustomizationFormText = "Tipo de Registro"
        Me.INDlciOldRowType.Location = New System.Drawing.Point(0, 0)
        Me.INDlciOldRowType.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldRowType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldRowType.Name = "INDlciOldRowType"
        Me.INDlciOldRowType.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldRowType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldRowType.Text = "Tipo de Registro"
        Me.INDlciOldRowType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldRowType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciOldRowType.TextToControlDistance = 0
        Me.INDlciOldRowType.TextVisible = False
        '
        'INDlciOldJobBondingDate
        '
        Me.INDlciOldJobBondingDate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldJobBondingDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldJobBondingDate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldJobBondingDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldJobBondingDate.Control = Me.INDlblOldJobBondingDate
        Me.INDlciOldJobBondingDate.CustomizationFormText = "Fecha de Vinculación"
        Me.INDlciOldJobBondingDate.Location = New System.Drawing.Point(0, 150)
        Me.INDlciOldJobBondingDate.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldJobBondingDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldJobBondingDate.Name = "INDlciOldJobBondingDate"
        Me.INDlciOldJobBondingDate.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldJobBondingDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldJobBondingDate.Text = "Fecha de Vinculación"
        Me.INDlciOldJobBondingDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldJobBondingDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldJobBondingDate.TextToControlDistance = 12
        '
        'INDlciOldContractEndingDate
        '
        Me.INDlciOldContractEndingDate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldContractEndingDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldContractEndingDate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldContractEndingDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldContractEndingDate.Control = Me.INDlblOldContractEndingDate
        Me.INDlciOldContractEndingDate.CustomizationFormText = "Fecha de Terminación"
        Me.INDlciOldContractEndingDate.Location = New System.Drawing.Point(0, 210)
        Me.INDlciOldContractEndingDate.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldContractEndingDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldContractEndingDate.Name = "INDlciOldContractEndingDate"
        Me.INDlciOldContractEndingDate.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldContractEndingDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldContractEndingDate.Text = "Fecha de Terminación"
        Me.INDlciOldContractEndingDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldContractEndingDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldContractEndingDate.TextToControlDistance = 12
        '
        'INDlciOldRetirementDate
        '
        Me.INDlciOldRetirementDate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldRetirementDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldRetirementDate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldRetirementDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldRetirementDate.Control = Me.INDlblOldRetirementDate
        Me.INDlciOldRetirementDate.CustomizationFormText = "Fecha de Retiro"
        Me.INDlciOldRetirementDate.Location = New System.Drawing.Point(0, 300)
        Me.INDlciOldRetirementDate.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldRetirementDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldRetirementDate.Name = "INDlciOldRetirementDate"
        Me.INDlciOldRetirementDate.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldRetirementDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldRetirementDate.Text = "Fecha de Retiro"
        Me.INDlciOldRetirementDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldRetirementDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldRetirementDate.TextToControlDistance = 12
        '
        'INDlciOldSalaryType
        '
        Me.INDlciOldSalaryType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciOldSalaryType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciOldSalaryType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciOldSalaryType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciOldSalaryType.Control = Me.INDlblOldSalaryType
        Me.INDlciOldSalaryType.CustomizationFormText = "Tipo de Salario"
        Me.INDlciOldSalaryType.Location = New System.Drawing.Point(0, 390)
        Me.INDlciOldSalaryType.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlciOldSalaryType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciOldSalaryType.Name = "INDlciOldSalaryType"
        Me.INDlciOldSalaryType.Size = New System.Drawing.Size(639, 30)
        Me.INDlciOldSalaryType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciOldSalaryType.Text = "Tipo de Salario"
        Me.INDlciOldSalaryType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciOldSalaryType.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciOldSalaryType.TextToControlDistance = 12
        '
        'INDlcgContractCompare
        '
        Me.INDlcgContractCompare.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractCompare.AppearanceGroup.Options.UseFont = True
        Me.INDlcgContractCompare.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractCompare.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgContractCompare.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractCompare.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgContractCompare.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgContractCompare.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgContractCompare.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractCompare.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgContractCompare.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractCompare.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgContractCompare.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractCompare.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgContractCompare, False)
        Me.INDlcgContractCompare.CustomizationFormText = "Información de Comparación"
        Me.INDlcgContractCompare.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciContractCompare})
        Me.INDlcgContractCompare.Location = New System.Drawing.Point(0, 473)
        Me.INDlcgContractCompare.Name = "INDlcgContractCompare"
        Me.INDlcgContractCompare.Size = New System.Drawing.Size(663, 153)
        Me.INDlcgContractCompare.Text = "Información de Comparación"
        Me.INDlcgContractCompare.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciContractCompare
        '
        Me.INDlciContractCompare.Control = Me.INDgrdContractCompare
        Me.INDlciContractCompare.CustomizationFormText = "Comparación de Contratos"
        Me.INDlciContractCompare.Location = New System.Drawing.Point(0, 0)
        Me.INDlciContractCompare.MinSize = New System.Drawing.Size(500, 100)
        Me.INDlciContractCompare.Name = "INDlciContractCompare"
        Me.INDlciContractCompare.Size = New System.Drawing.Size(639, 100)
        Me.INDlciContractCompare.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciContractCompare.Text = "Comparación de Contratos"
        Me.INDlciContractCompare.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciContractCompare.TextVisible = False
        '
        'INDrepContractHistoryMoreAction
        '
        Me.INDrepContractHistoryMoreAction.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDrepContractHistoryMoreAction.Appearance.ForeColor = System.Drawing.Color.White
        Me.INDrepContractHistoryMoreAction.Appearance.Options.UseFont = True
        Me.INDrepContractHistoryMoreAction.Appearance.Options.UseForeColor = True
        Me.INDrepContractHistoryMoreAction.Appearance.Options.UseTextOptions = True
        Me.INDrepContractHistoryMoreAction.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDrepContractHistoryMoreAction.AutoHeight = False
        Me.INDrepContractHistoryMoreAction.Name = "INDrepContractHistoryMoreAction"
        Me.INDrepContractHistoryMoreAction.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'INDgrdHistoryChanges
        '
        Me.INDgrdHistoryChanges.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgrdHistoryChanges.Location = New System.Drawing.Point(3, 600)
        Me.INDgrdHistoryChanges.MainView = Me.INDgrvHistoryChanges
        Me.INDgrdHistoryChanges.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDgrdHistoryChanges.MaximumSize = New System.Drawing.Size(0, 200)
        Me.INDgrdHistoryChanges.MinimumSize = New System.Drawing.Size(0, 200)
        Me.INDgrdHistoryChanges.Name = "INDgrdHistoryChanges"
        Me.INDgrdHistoryChanges.Size = New System.Drawing.Size(794, 200)
        Me.INDgrdHistoryChanges.TabIndex = 35
        Me.INDgrdHistoryChanges.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgrvHistoryChanges})
        '
        'INDgrvHistoryChanges
        '
        Me.INDgrvHistoryChanges.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgrvHistoryChanges.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgrvHistoryChanges.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgrvHistoryChanges.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgrvHistoryChanges.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvHistoryChanges.Appearance.GroupRow.Options.UseFont = True
        Me.INDgrvHistoryChanges.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgrvHistoryChanges.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgrvHistoryChanges.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgrvHistoryChanges.Appearance.Row.Options.UseFont = True
        Me.INDgrvHistoryChanges.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDgrcHistGroupLevel1, Me.INDgrcHistGroupLevel2, Me.INDgrcHistType, Me.INDgrcHistValueOld, Me.INDgrcHistValueNew, Me.INDgrcHistUserCodeName, Me.INDgrcHistDate})
        Me.INDgrvHistoryChanges.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgrvHistoryChanges.GridControl = Me.INDgrdHistoryChanges
        Me.INDgrvHistoryChanges.GroupCount = 2
        Me.INDgrvHistoryChanges.Name = "INDgrvHistoryChanges"
        Me.INDgrvHistoryChanges.OptionsDetail.EnableMasterViewMode = False
        Me.INDgrvHistoryChanges.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgrvHistoryChanges.OptionsSelection.UseIndicatorForSelection = False
        Me.INDgrvHistoryChanges.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgrvHistoryChanges.OptionsView.EnableAppearanceOddRow = True
        Me.INDgrvHistoryChanges.OptionsView.ShowAutoFilterRow = True
        Me.INDgrvHistoryChanges.OptionsView.ShowGroupPanel = False
        Me.INDgrvHistoryChanges.OptionsView.ShowIndicator = False
        Me.INDgrvHistoryChanges.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDgrcHistGroupLevel1, DevExpress.Data.ColumnSortOrder.Descending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDgrcHistGroupLevel2, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgrvHistoryChanges, False)
        '
        'INDgrcHistGroupLevel1
        '
        Me.INDgrcHistGroupLevel1.Caption = "Contrato"
        Me.INDgrcHistGroupLevel1.FieldName = "ContractStatus"
        Me.INDgrcHistGroupLevel1.Name = "INDgrcHistGroupLevel1"
        Me.INDgrcHistGroupLevel1.OptionsColumn.AllowEdit = False
        Me.INDgrcHistGroupLevel1.OptionsColumn.AllowMove = False
        Me.INDgrcHistGroupLevel1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDgrcHistGroupLevel1.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.INDgrcHistGroupLevel1.Visible = True
        Me.INDgrcHistGroupLevel1.VisibleIndex = 0
        Me.INDgrcHistGroupLevel1.Width = 150
        '
        'INDgrcHistGroupLevel2
        '
        Me.INDgrcHistGroupLevel2.Caption = "Contrato"
        Me.INDgrcHistGroupLevel2.FieldName = "ContractId"
        Me.INDgrcHistGroupLevel2.Name = "INDgrcHistGroupLevel2"
        Me.INDgrcHistGroupLevel2.OptionsColumn.AllowEdit = False
        Me.INDgrcHistGroupLevel2.OptionsColumn.AllowMove = False
        Me.INDgrcHistGroupLevel2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDgrcHistGroupLevel2.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.INDgrcHistGroupLevel2.Visible = True
        Me.INDgrcHistGroupLevel2.VisibleIndex = 1
        Me.INDgrcHistGroupLevel2.Width = 150
        '
        'INDgrcHistType
        '
        Me.INDgrcHistType.Caption = "Campo Modificado"
        Me.INDgrcHistType.FieldName = "Type"
        Me.INDgrcHistType.Name = "INDgrcHistType"
        Me.INDgrcHistType.OptionsColumn.AllowEdit = False
        Me.INDgrcHistType.Visible = True
        Me.INDgrcHistType.VisibleIndex = 0
        Me.INDgrcHistType.Width = 150
        '
        'INDgrcHistValueOld
        '
        Me.INDgrcHistValueOld.Caption = "Campo Anterior"
        Me.INDgrcHistValueOld.FieldName = "ValueOld"
        Me.INDgrcHistValueOld.Name = "INDgrcHistValueOld"
        Me.INDgrcHistValueOld.OptionsColumn.AllowEdit = False
        Me.INDgrcHistValueOld.Visible = True
        Me.INDgrcHistValueOld.VisibleIndex = 1
        Me.INDgrcHistValueOld.Width = 150
        '
        'INDgrcHistValueNew
        '
        Me.INDgrcHistValueNew.Caption = "Campo Nuevo"
        Me.INDgrcHistValueNew.FieldName = "ValueNew"
        Me.INDgrcHistValueNew.Name = "INDgrcHistValueNew"
        Me.INDgrcHistValueNew.OptionsColumn.AllowEdit = False
        Me.INDgrcHistValueNew.Visible = True
        Me.INDgrcHistValueNew.VisibleIndex = 2
        Me.INDgrcHistValueNew.Width = 150
        '
        'INDgrcHistUserCodeName
        '
        Me.INDgrcHistUserCodeName.Caption = "Usuario"
        Me.INDgrcHistUserCodeName.FieldName = "UserCodeName"
        Me.INDgrcHistUserCodeName.Name = "INDgrcHistUserCodeName"
        Me.INDgrcHistUserCodeName.OptionsColumn.AllowEdit = False
        Me.INDgrcHistUserCodeName.Visible = True
        Me.INDgrcHistUserCodeName.VisibleIndex = 4
        Me.INDgrcHistUserCodeName.Width = 150
        '
        'INDgrcHistDate
        '
        Me.INDgrcHistDate.Caption = "Fecha"
        Me.INDgrcHistDate.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss tt"
        Me.INDgrcHistDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDgrcHistDate.FieldName = "Date"
        Me.INDgrcHistDate.Name = "INDgrcHistDate"
        Me.INDgrcHistDate.OptionsColumn.AllowEdit = False
        Me.INDgrcHistDate.Visible = True
        Me.INDgrcHistDate.VisibleIndex = 3
        Me.INDgrcHistDate.Width = 200
        '
        'INDlblPaymentType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblPaymentType, False)
        Me.INDlblPaymentType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblPaymentType.Appearance.Options.UseFont = True
        Me.INDlblPaymentType.Appearance.Options.UseTextOptions = True
        Me.INDlblPaymentType.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblPaymentType.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblPaymentType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblPaymentType, False)
        Me.INDlblPaymentType.Location = New System.Drawing.Point(565, 399)
        Me.INDlblPaymentType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblPaymentType.Name = "INDlblPaymentType"
        Me.INDlblPaymentType.Size = New System.Drawing.Size(214, 26)
        Me.INDlblPaymentType.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblPaymentType.TabIndex = 33
        Me.INDlblPaymentType.Text = "LabelControl11"
        '
        'INDlblPaymentPeriod
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblPaymentPeriod, False)
        Me.INDlblPaymentPeriod.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblPaymentPeriod.Appearance.Options.UseFont = True
        Me.INDlblPaymentPeriod.Appearance.Options.UseTextOptions = True
        Me.INDlblPaymentPeriod.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblPaymentPeriod.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblPaymentPeriod.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblPaymentPeriod, False)
        Me.INDlblPaymentPeriod.Location = New System.Drawing.Point(175, 399)
        Me.INDlblPaymentPeriod.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblPaymentPeriod.Name = "INDlblPaymentPeriod"
        Me.INDlblPaymentPeriod.Size = New System.Drawing.Size(214, 26)
        Me.INDlblPaymentPeriod.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblPaymentPeriod.TabIndex = 32
        Me.INDlblPaymentPeriod.Text = "LabelControl10"
        '
        'INDlblSalaryType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblSalaryType, False)
        Me.INDlblSalaryType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblSalaryType.Appearance.Options.UseFont = True
        Me.INDlblSalaryType.Appearance.Options.UseTextOptions = True
        Me.INDlblSalaryType.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblSalaryType.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblSalaryType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblSalaryType, False)
        Me.INDlblSalaryType.Location = New System.Drawing.Point(174, 218)
        Me.INDlblSalaryType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblSalaryType.Name = "INDlblSalaryType"
        Me.INDlblSalaryType.Size = New System.Drawing.Size(214, 26)
        Me.INDlblSalaryType.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblSalaryType.TabIndex = 31
        Me.INDlblSalaryType.Text = "LabelControl10"
        '
        'INDlblRetirementDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblRetirementDate, False)
        Me.INDlblRetirementDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblRetirementDate.Appearance.Options.UseFont = True
        Me.INDlblRetirementDate.Appearance.Options.UseTextOptions = True
        Me.INDlblRetirementDate.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblRetirementDate.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblRetirementDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblRetirementDate, False)
        Me.INDlblRetirementDate.Location = New System.Drawing.Point(565, 369)
        Me.INDlblRetirementDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblRetirementDate.Name = "INDlblRetirementDate"
        Me.INDlblRetirementDate.Size = New System.Drawing.Size(214, 26)
        Me.INDlblRetirementDate.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblRetirementDate.TabIndex = 30
        Me.INDlblRetirementDate.Text = "LabelControl9"
        '
        'INDlblRetirementReason
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblRetirementReason, False)
        Me.INDlblRetirementReason.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblRetirementReason.Appearance.Options.UseFont = True
        Me.INDlblRetirementReason.Appearance.Options.UseTextOptions = True
        Me.INDlblRetirementReason.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblRetirementReason.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblRetirementReason.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblRetirementReason, False)
        Me.INDlblRetirementReason.Location = New System.Drawing.Point(175, 369)
        Me.INDlblRetirementReason.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblRetirementReason.Name = "INDlblRetirementReason"
        Me.INDlblRetirementReason.Size = New System.Drawing.Size(214, 26)
        Me.INDlblRetirementReason.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblRetirementReason.TabIndex = 29
        Me.INDlblRetirementReason.Text = "LabelControl8"
        '
        'INDlblJobBondingType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblJobBondingType, False)
        Me.INDlblJobBondingType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblJobBondingType.Appearance.Options.UseFont = True
        Me.INDlblJobBondingType.Appearance.Options.UseTextOptions = True
        Me.INDlblJobBondingType.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblJobBondingType.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblJobBondingType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblJobBondingType, False)
        Me.INDlblJobBondingType.Location = New System.Drawing.Point(565, 339)
        Me.INDlblJobBondingType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblJobBondingType.Name = "INDlblJobBondingType"
        Me.INDlblJobBondingType.Size = New System.Drawing.Size(214, 26)
        Me.INDlblJobBondingType.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblJobBondingType.TabIndex = 28
        Me.INDlblJobBondingType.Text = "LabelControl8"
        '
        'INDlblContractType
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblContractType, False)
        Me.INDlblContractType.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblContractType.Appearance.Options.UseFont = True
        Me.INDlblContractType.Appearance.Options.UseTextOptions = True
        Me.INDlblContractType.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblContractType.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblContractType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblContractType, False)
        Me.INDlblContractType.Location = New System.Drawing.Point(175, 339)
        Me.INDlblContractType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblContractType.Name = "INDlblContractType"
        Me.INDlblContractType.Size = New System.Drawing.Size(214, 26)
        Me.INDlblContractType.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblContractType.TabIndex = 27
        Me.INDlblContractType.Text = "LabelControl8"
        '
        'INDlblShowContractHistory
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblShowContractHistory, False)
        Me.INDlblShowContractHistory.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblShowContractHistory.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlblShowContractHistory.Appearance.Options.UseFont = True
        Me.INDlblShowContractHistory.Appearance.Options.UseForeColor = True
        Me.INDlblShowContractHistory.Appearance.Options.UseTextOptions = True
        Me.INDlblShowContractHistory.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblShowContractHistory, False)
        Me.INDlblShowContractHistory.Location = New System.Drawing.Point(607, 308)
        Me.INDlblShowContractHistory.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblShowContractHistory.Name = "INDlblShowContractHistory"
        Me.INDlblShowContractHistory.Size = New System.Drawing.Size(191, 26)
        Me.INDlblShowContractHistory.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblShowContractHistory.TabIndex = 26
        Me.INDlblShowContractHistory.Text = "Histórico de Contratos"
        '
        'INDlblShowMoreInfo
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblShowMoreInfo, False)
        Me.INDlblShowMoreInfo.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblShowMoreInfo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlblShowMoreInfo.Appearance.Options.UseFont = True
        Me.INDlblShowMoreInfo.Appearance.Options.UseForeColor = True
        Me.INDlblShowMoreInfo.Appearance.Options.UseTextOptions = True
        Me.INDlblShowMoreInfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblShowMoreInfo, False)
        Me.INDlblShowMoreInfo.Location = New System.Drawing.Point(412, 308)
        Me.INDlblShowMoreInfo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblShowMoreInfo.Name = "INDlblShowMoreInfo"
        Me.INDlblShowMoreInfo.Size = New System.Drawing.Size(191, 26)
        Me.INDlblShowMoreInfo.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblShowMoreInfo.TabIndex = 25
        Me.INDlblShowMoreInfo.Text = "Más Información"
        '
        'INDlblEndingDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblEndingDate, False)
        Me.INDlblEndingDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblEndingDate.Appearance.Options.UseFont = True
        Me.INDlblEndingDate.Appearance.Options.UseTextOptions = True
        Me.INDlblEndingDate.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblEndingDate.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblEndingDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblEndingDate, False)
        Me.INDlblEndingDate.Location = New System.Drawing.Point(564, 188)
        Me.INDlblEndingDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblEndingDate.Name = "INDlblEndingDate"
        Me.INDlblEndingDate.Size = New System.Drawing.Size(214, 26)
        Me.INDlblEndingDate.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblEndingDate.TabIndex = 24
        Me.INDlblEndingDate.Text = "LabelControl5"
        '
        'INDlblStartDate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblStartDate, False)
        Me.INDlblStartDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblStartDate.Appearance.Options.UseFont = True
        Me.INDlblStartDate.Appearance.Options.UseTextOptions = True
        Me.INDlblStartDate.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblStartDate.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblStartDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblStartDate, False)
        Me.INDlblStartDate.Location = New System.Drawing.Point(174, 188)
        Me.INDlblStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblStartDate.Name = "INDlblStartDate"
        Me.INDlblStartDate.Size = New System.Drawing.Size(214, 26)
        Me.INDlblStartDate.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblStartDate.TabIndex = 23
        Me.INDlblStartDate.Text = "LabelControl1"
        '
        'INDlblCostCenter
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblCostCenter, False)
        Me.INDlblCostCenter.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblCostCenter.Appearance.Options.UseFont = True
        Me.INDlblCostCenter.Appearance.Options.UseTextOptions = True
        Me.INDlblCostCenter.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblCostCenter.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblCostCenter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblCostCenter, False)
        Me.INDlblCostCenter.Location = New System.Drawing.Point(174, 278)
        Me.INDlblCostCenter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblCostCenter.Name = "INDlblCostCenter"
        Me.INDlblCostCenter.Size = New System.Drawing.Size(214, 26)
        Me.INDlblCostCenter.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblCostCenter.TabIndex = 22
        Me.INDlblCostCenter.Text = "LabelControl4"
        '
        'INDlblFunctionalUnit
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblFunctionalUnit, False)
        Me.INDlblFunctionalUnit.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblFunctionalUnit.Appearance.Options.UseFont = True
        Me.INDlblFunctionalUnit.Appearance.Options.UseTextOptions = True
        Me.INDlblFunctionalUnit.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblFunctionalUnit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblFunctionalUnit.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblFunctionalUnit, False)
        Me.INDlblFunctionalUnit.Location = New System.Drawing.Point(564, 248)
        Me.INDlblFunctionalUnit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblFunctionalUnit.Name = "INDlblFunctionalUnit"
        Me.INDlblFunctionalUnit.Size = New System.Drawing.Size(214, 26)
        Me.INDlblFunctionalUnit.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblFunctionalUnit.TabIndex = 21
        Me.INDlblFunctionalUnit.Text = "LabelControl3"
        '
        'INDlblGroup
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblGroup, False)
        Me.INDlblGroup.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblGroup.Appearance.Options.UseFont = True
        Me.INDlblGroup.Appearance.Options.UseTextOptions = True
        Me.INDlblGroup.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblGroup.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblGroup.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblGroup, False)
        Me.INDlblGroup.Location = New System.Drawing.Point(174, 248)
        Me.INDlblGroup.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblGroup.Name = "INDlblGroup"
        Me.INDlblGroup.Size = New System.Drawing.Size(214, 26)
        Me.INDlblGroup.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblGroup.TabIndex = 20
        Me.INDlblGroup.Text = "LabelControl2"
        '
        'INDlblSalary
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblSalary, False)
        Me.INDlblSalary.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblSalary.Appearance.Options.UseFont = True
        Me.INDlblSalary.Appearance.Options.UseTextOptions = True
        Me.INDlblSalary.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblSalary.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblSalary.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblSalary, False)
        Me.INDlblSalary.Location = New System.Drawing.Point(564, 218)
        Me.INDlblSalary.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblSalary.Name = "INDlblSalary"
        Me.INDlblSalary.Size = New System.Drawing.Size(214, 26)
        Me.INDlblSalary.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblSalary.TabIndex = 19
        Me.INDlblSalary.Text = "LabelControl1"
        '
        'INDlblPosition
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblPosition, False)
        Me.INDlblPosition.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblPosition.Appearance.Options.UseFont = True
        Me.INDlblPosition.Appearance.Options.UseTextOptions = True
        Me.INDlblPosition.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.INDlblPosition.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap
        Me.INDlblPosition.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblPosition, False)
        Me.INDlblPosition.Location = New System.Drawing.Point(174, 128)
        Me.INDlblPosition.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblPosition.Name = "INDlblPosition"
        Me.INDlblPosition.Size = New System.Drawing.Size(214, 26)
        Me.INDlblPosition.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblPosition.TabIndex = 18
        Me.INDlblPosition.Text = "LabelControl1"
        '
        'INDlblContractNo
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblContractNo, False)
        Me.INDlblContractNo.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlblContractNo.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblContractNo, False)
        Me.INDlblContractNo.Location = New System.Drawing.Point(174, 98)
        Me.INDlblContractNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDlblContractNo.Name = "INDlblContractNo"
        Me.INDlblContractNo.Size = New System.Drawing.Size(214, 26)
        Me.INDlblContractNo.StyleController = Me.INDlyCtlContractViewer
        Me.INDlblContractNo.TabIndex = 5
        Me.INDlblContractNo.Text = "0"
        '
        'INDbtnContractViewerActions
        '
        Me.INDbtnContractViewerActions.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Hide
        Me.INDbtnContractViewerActions.DropDownControl = Me.INDppmContractViewerActions
        Me.INDbtnContractViewerActions.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.Mmenu_de_acciones
        Me.INDbtnContractViewerActions.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDbtnContractViewerActions.Location = New System.Drawing.Point(720, 0)
        Me.INDbtnContractViewerActions.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDbtnContractViewerActions.MenuManager = Me.BarManager1
        Me.INDbtnContractViewerActions.Name = "INDbtnContractViewerActions"
        Me.INDbtnContractViewerActions.Size = New System.Drawing.Size(80, 36)
        Me.INDbtnContractViewerActions.StyleController = Me.INDlyCtlContractViewer
        Me.INDbtnContractViewerActions.TabIndex = 4
        '
        'INDppmContractViewerActions
        '
        Me.INDppmContractViewerActions.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbiAddNewContract), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.INDbbiRenewContract, DevExpress.XtraBars.BarItemPaintStyle.Standard), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbiFinishContract), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbiDeleteContract), New DevExpress.XtraBars.LinkPersistInfo(Me.INDbbiActivateContract)})
        Me.INDppmContractViewerActions.Manager = Me.BarManager1
        Me.INDppmContractViewerActions.Name = "INDppmContractViewerActions"
        '
        'INDlcgContractViewer
        '
        Me.INDlcgContractViewer.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractViewer.AppearanceGroup.Options.UseFont = True
        Me.INDlcgContractViewer.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractViewer.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgContractViewer.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractViewer.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgContractViewer.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgContractViewer.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgContractViewer.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractViewer.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgContractViewer.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractViewer.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgContractViewer.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractViewer.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgContractViewer, False)
        Me.INDlcgContractViewer.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgContractViewer.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgContractViewer.GroupBordersVisible = False
        Me.INDlcgContractViewer.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciContractViewerActions, Me.INDlciContractNo, Me.INDlciPositionOld, Me.INDlciGroup, Me.INDlciCostCenter, Me.INDlcgContractMoreInfo, Me.INDlciShowMoreInfo, Me.INDlciShowContractHistory, Me.INDlciSalaryType, Me.INDlcgContractHistory, Me.INDlcgHistoryChanges, Me.INDlciRowType, Me.INDlciStartDate, Me.INDlciEndingDate, Me.INDlciSalary, Me.INDlciFunctionalUnit, Me.INDlciPosition, Me.LayoutControlItem1, Me.EmptySpaceItem1, Me.INDlciJobBondingDate, Me.INDlciResolutionNumber, Me.INDlciCertificateOfficeNumber, Me.INDlciPosesionDate, Me.INDlciResolutionDate})
        Me.INDlcgContractViewer.Name = "Root"
        Me.INDlcgContractViewer.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlcgContractViewer.Size = New System.Drawing.Size(800, 700)
        Me.INDlcgContractViewer.TextVisible = False
        '
        'INDlciContractViewerActions
        '
        Me.INDlciContractViewerActions.Control = Me.INDbtnContractViewerActions
        Me.INDlciContractViewerActions.ControlAlignment = System.Drawing.ContentAlignment.TopRight
        Me.INDlciContractViewerActions.CustomizationFormText = "Acciones"
        Me.INDlciContractViewerActions.Location = New System.Drawing.Point(720, 0)
        Me.INDlciContractViewerActions.MaxSize = New System.Drawing.Size(80, 36)
        Me.INDlciContractViewerActions.MinSize = New System.Drawing.Size(80, 36)
        Me.INDlciContractViewerActions.Name = "INDlciContractViewerActions"
        Me.INDlciContractViewerActions.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlciContractViewerActions.Size = New System.Drawing.Size(80, 36)
        Me.INDlciContractViewerActions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciContractViewerActions.Text = "Acciones"
        Me.INDlciContractViewerActions.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciContractViewerActions.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciContractViewerActions.TextToControlDistance = 0
        Me.INDlciContractViewerActions.TextVisible = False
        '
        'INDlciContractNo
        '
        Me.INDlciContractNo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciContractNo.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciContractNo.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciContractNo.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciContractNo.Control = Me.INDlblContractNo
        Me.INDlciContractNo.CustomizationFormText = "Número de Contrato"
        Me.INDlciContractNo.Location = New System.Drawing.Point(0, 96)
        Me.INDlciContractNo.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciContractNo.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciContractNo.Name = "INDlciContractNo"
        Me.INDlciContractNo.Size = New System.Drawing.Size(390, 30)
        Me.INDlciContractNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciContractNo.Text = "Número de Contrato"
        Me.INDlciContractNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciContractNo.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciContractNo.TextToControlDistance = 12
        '
        'INDlciPositionOld
        '
        Me.INDlciPositionOld.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciPositionOld.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciPositionOld.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciPositionOld.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciPositionOld.Control = Me.INDlblPosition
        Me.INDlciPositionOld.CustomizationFormText = "Cargo"
        Me.INDlciPositionOld.Location = New System.Drawing.Point(0, 126)
        Me.INDlciPositionOld.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciPositionOld.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciPositionOld.Name = "INDlciPositionOld"
        Me.INDlciPositionOld.Size = New System.Drawing.Size(800, 30)
        Me.INDlciPositionOld.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPositionOld.Text = "Cargo"
        Me.INDlciPositionOld.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciPositionOld.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciPositionOld.TextToControlDistance = 12
        '
        'INDlciGroup
        '
        Me.INDlciGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciGroup.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciGroup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciGroup.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciGroup.Control = Me.INDlblGroup
        Me.INDlciGroup.CustomizationFormText = "Grupo"
        Me.INDlciGroup.Location = New System.Drawing.Point(0, 246)
        Me.INDlciGroup.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciGroup.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciGroup.Name = "INDlciGroup"
        Me.INDlciGroup.Size = New System.Drawing.Size(390, 30)
        Me.INDlciGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciGroup.Text = "Grupo"
        Me.INDlciGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciGroup.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciGroup.TextToControlDistance = 12
        '
        'INDlciCostCenter
        '
        Me.INDlciCostCenter.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciCostCenter.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciCostCenter.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciCostCenter.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciCostCenter.Control = Me.INDlblCostCenter
        Me.INDlciCostCenter.CustomizationFormText = "Centro de Costo"
        Me.INDlciCostCenter.Location = New System.Drawing.Point(0, 276)
        Me.INDlciCostCenter.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciCostCenter.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciCostCenter.Name = "INDlciCostCenter"
        Me.INDlciCostCenter.Size = New System.Drawing.Size(800, 30)
        Me.INDlciCostCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCostCenter.Text = "Centro de Costo"
        Me.INDlciCostCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCostCenter.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciCostCenter.TextToControlDistance = 12
        '
        'INDlcgContractMoreInfo
        '
        Me.INDlcgContractMoreInfo.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractMoreInfo.AppearanceGroup.Options.UseFont = True
        Me.INDlcgContractMoreInfo.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractMoreInfo.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgContractMoreInfo.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractMoreInfo.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgContractMoreInfo.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgContractMoreInfo.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgContractMoreInfo.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractMoreInfo.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgContractMoreInfo.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractMoreInfo.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgContractMoreInfo.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractMoreInfo.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgContractMoreInfo, False)
        Me.INDlcgContractMoreInfo.CustomizationFormText = "LayoutControlGroup1"
        Me.INDlcgContractMoreInfo.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciContractType, Me.INDlciRetirementReason, Me.INDlciRetirementDate, Me.INDlciPaymentPeriod, Me.INDlciPaymentType, Me.INDlciJobBondingType})
        Me.INDlcgContractMoreInfo.Location = New System.Drawing.Point(0, 336)
        Me.INDlcgContractMoreInfo.Name = "INDlcgContractMoreInfo"
        Me.INDlcgContractMoreInfo.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlcgContractMoreInfo.Size = New System.Drawing.Size(800, 92)
        Me.INDlcgContractMoreInfo.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlcgContractMoreInfo.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDlcgContractMoreInfo.TextVisible = False
        Me.INDlcgContractMoreInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciContractType
        '
        Me.INDlciContractType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciContractType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciContractType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciContractType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciContractType.Control = Me.INDlblContractType
        Me.INDlciContractType.CustomizationFormText = "Tipo de Contrato"
        Me.INDlciContractType.Location = New System.Drawing.Point(0, 0)
        Me.INDlciContractType.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciContractType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciContractType.Name = "INDlciContractType"
        Me.INDlciContractType.Size = New System.Drawing.Size(390, 30)
        Me.INDlciContractType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciContractType.Text = "Tipo de Contrato"
        Me.INDlciContractType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciContractType.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciContractType.TextToControlDistance = 12
        '
        'INDlciRetirementReason
        '
        Me.INDlciRetirementReason.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciRetirementReason.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciRetirementReason.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciRetirementReason.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciRetirementReason.Control = Me.INDlblRetirementReason
        Me.INDlciRetirementReason.CustomizationFormText = "Razón de Retíro"
        Me.INDlciRetirementReason.Location = New System.Drawing.Point(0, 30)
        Me.INDlciRetirementReason.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciRetirementReason.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciRetirementReason.Name = "INDlciRetirementReason"
        Me.INDlciRetirementReason.Size = New System.Drawing.Size(390, 30)
        Me.INDlciRetirementReason.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciRetirementReason.Text = "Razón de Retíro"
        Me.INDlciRetirementReason.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciRetirementReason.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciRetirementReason.TextToControlDistance = 12
        '
        'INDlciRetirementDate
        '
        Me.INDlciRetirementDate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciRetirementDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciRetirementDate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciRetirementDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciRetirementDate.Control = Me.INDlblRetirementDate
        Me.INDlciRetirementDate.CustomizationFormText = "Fecha de Retíro"
        Me.INDlciRetirementDate.Location = New System.Drawing.Point(390, 30)
        Me.INDlciRetirementDate.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciRetirementDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciRetirementDate.Name = "INDlciRetirementDate"
        Me.INDlciRetirementDate.Size = New System.Drawing.Size(408, 30)
        Me.INDlciRetirementDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciRetirementDate.Text = "Fecha de Retíro"
        Me.INDlciRetirementDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciRetirementDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciRetirementDate.TextToControlDistance = 12
        '
        'INDlciPaymentPeriod
        '
        Me.INDlciPaymentPeriod.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciPaymentPeriod.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciPaymentPeriod.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciPaymentPeriod.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciPaymentPeriod.Control = Me.INDlblPaymentPeriod
        Me.INDlciPaymentPeriod.CustomizationFormText = "Período de Pago"
        Me.INDlciPaymentPeriod.Location = New System.Drawing.Point(0, 60)
        Me.INDlciPaymentPeriod.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciPaymentPeriod.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciPaymentPeriod.Name = "INDlciPaymentPeriod"
        Me.INDlciPaymentPeriod.Size = New System.Drawing.Size(390, 30)
        Me.INDlciPaymentPeriod.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPaymentPeriod.Text = "Período de Pago"
        Me.INDlciPaymentPeriod.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciPaymentPeriod.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciPaymentPeriod.TextToControlDistance = 12
        '
        'INDlciPaymentType
        '
        Me.INDlciPaymentType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciPaymentType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciPaymentType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciPaymentType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciPaymentType.Control = Me.INDlblPaymentType
        Me.INDlciPaymentType.CustomizationFormText = "Tipo de Pago"
        Me.INDlciPaymentType.Location = New System.Drawing.Point(390, 60)
        Me.INDlciPaymentType.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciPaymentType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciPaymentType.Name = "INDlciPaymentType"
        Me.INDlciPaymentType.Size = New System.Drawing.Size(408, 30)
        Me.INDlciPaymentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPaymentType.Text = "Tipo de Pago"
        Me.INDlciPaymentType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciPaymentType.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciPaymentType.TextToControlDistance = 12
        '
        'INDlciJobBondingType
        '
        Me.INDlciJobBondingType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciJobBondingType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciJobBondingType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciJobBondingType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciJobBondingType.Control = Me.INDlblJobBondingType
        Me.INDlciJobBondingType.CustomizationFormText = "Tipo de Vinculación"
        Me.INDlciJobBondingType.Location = New System.Drawing.Point(390, 0)
        Me.INDlciJobBondingType.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciJobBondingType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciJobBondingType.Name = "INDlciJobBondingType"
        Me.INDlciJobBondingType.Size = New System.Drawing.Size(408, 30)
        Me.INDlciJobBondingType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciJobBondingType.Text = "Tipo de Vinculación"
        Me.INDlciJobBondingType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciJobBondingType.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciJobBondingType.TextToControlDistance = 12
        '
        'INDlciShowMoreInfo
        '
        Me.INDlciShowMoreInfo.Control = Me.INDlblShowMoreInfo
        Me.INDlciShowMoreInfo.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.INDlciShowMoreInfo.CustomizationFormText = "Mostrar Más Información"
        Me.INDlciShowMoreInfo.Location = New System.Drawing.Point(410, 306)
        Me.INDlciShowMoreInfo.MaxSize = New System.Drawing.Size(195, 30)
        Me.INDlciShowMoreInfo.MinSize = New System.Drawing.Size(195, 30)
        Me.INDlciShowMoreInfo.Name = "INDlciShowMoreInfo"
        Me.INDlciShowMoreInfo.Size = New System.Drawing.Size(195, 30)
        Me.INDlciShowMoreInfo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciShowMoreInfo.Text = "Mostrar Más Información"
        Me.INDlciShowMoreInfo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciShowMoreInfo.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciShowMoreInfo.TextToControlDistance = 0
        Me.INDlciShowMoreInfo.TextVisible = False
        '
        'INDlciShowContractHistory
        '
        Me.INDlciShowContractHistory.Control = Me.INDlblShowContractHistory
        Me.INDlciShowContractHistory.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.INDlciShowContractHistory.CustomizationFormText = "Mostrar Histórico de Contratos"
        Me.INDlciShowContractHistory.Location = New System.Drawing.Point(605, 306)
        Me.INDlciShowContractHistory.MaxSize = New System.Drawing.Size(195, 30)
        Me.INDlciShowContractHistory.MinSize = New System.Drawing.Size(195, 30)
        Me.INDlciShowContractHistory.Name = "INDlciShowContractHistory"
        Me.INDlciShowContractHistory.Size = New System.Drawing.Size(195, 30)
        Me.INDlciShowContractHistory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciShowContractHistory.Text = "Mostrar Histórico de Contratos"
        Me.INDlciShowContractHistory.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciShowContractHistory.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciShowContractHistory.TextToControlDistance = 0
        Me.INDlciShowContractHistory.TextVisible = False
        '
        'INDlciSalaryType
        '
        Me.INDlciSalaryType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciSalaryType.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciSalaryType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciSalaryType.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciSalaryType.Control = Me.INDlblSalaryType
        Me.INDlciSalaryType.CustomizationFormText = "Tipo de Salario"
        Me.INDlciSalaryType.Location = New System.Drawing.Point(0, 216)
        Me.INDlciSalaryType.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciSalaryType.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciSalaryType.Name = "INDlciSalaryType"
        Me.INDlciSalaryType.Size = New System.Drawing.Size(390, 30)
        Me.INDlciSalaryType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciSalaryType.Text = "Tipo de Salario"
        Me.INDlciSalaryType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciSalaryType.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciSalaryType.TextToControlDistance = 12
        '
        'INDlcgContractHistory
        '
        Me.INDlcgContractHistory.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractHistory.AppearanceGroup.Options.UseFont = True
        Me.INDlcgContractHistory.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractHistory.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgContractHistory.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractHistory.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgContractHistory.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgContractHistory.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgContractHistory.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractHistory.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgContractHistory.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractHistory.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgContractHistory.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractHistory.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgContractHistory, False)
        Me.INDlcgContractHistory.CustomizationFormText = "INDlcgContractHistory"
        Me.INDlcgContractHistory.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgContractHistory.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciContractHistory})
        Me.INDlcgContractHistory.Location = New System.Drawing.Point(0, 428)
        Me.INDlcgContractHistory.Name = "INDlcgContractHistory"
        Me.INDlcgContractHistory.OptionsItemText.TextToControlDistance = 0
        Me.INDlcgContractHistory.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlcgContractHistory.Size = New System.Drawing.Size(800, 140)
        Me.INDlcgContractHistory.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlcgContractHistory.Text = "Histórico de cambios contractuales"
        Me.INDlcgContractHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciContractHistory
        '
        Me.INDlciContractHistory.Control = Me.INDgrdContractHistory
        Me.INDlciContractHistory.CustomizationFormText = "INDlciContractHistory"
        Me.INDlciContractHistory.Location = New System.Drawing.Point(0, 0)
        Me.INDlciContractHistory.MinSize = New System.Drawing.Size(1, 100)
        Me.INDlciContractHistory.Name = "INDlciContractHistory"
        Me.INDlciContractHistory.Size = New System.Drawing.Size(798, 109)
        Me.INDlciContractHistory.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciContractHistory.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciContractHistory.TextVisible = False
        '
        'INDlcgHistoryChanges
        '
        Me.INDlcgHistoryChanges.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgHistoryChanges.AppearanceGroup.Options.UseFont = True
        Me.INDlcgHistoryChanges.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgHistoryChanges.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgHistoryChanges.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgHistoryChanges.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgHistoryChanges.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgHistoryChanges.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgHistoryChanges.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgHistoryChanges.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgHistoryChanges.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgHistoryChanges.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgHistoryChanges.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgHistoryChanges.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgHistoryChanges, False)
        Me.INDlcgHistoryChanges.CustomizationFormText = "INDlcgHistoryChanges"
        Me.INDlcgHistoryChanges.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgHistoryChanges.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciHistoryChanges})
        Me.INDlcgHistoryChanges.Location = New System.Drawing.Point(0, 568)
        Me.INDlcgHistoryChanges.Name = "INDlcgHistoryChanges"
        Me.INDlcgHistoryChanges.OptionsItemText.TextToControlDistance = 0
        Me.INDlcgHistoryChanges.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlcgHistoryChanges.Size = New System.Drawing.Size(800, 132)
        Me.INDlcgHistoryChanges.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlcgHistoryChanges.Text = "Histórico de cambios no contractuales"
        Me.INDlcgHistoryChanges.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciHistoryChanges
        '
        Me.INDlciHistoryChanges.Control = Me.INDgrdHistoryChanges
        Me.INDlciHistoryChanges.CustomizationFormText = "INDlciHistoryChanges"
        Me.INDlciHistoryChanges.Location = New System.Drawing.Point(0, 0)
        Me.INDlciHistoryChanges.MinSize = New System.Drawing.Size(1, 100)
        Me.INDlciHistoryChanges.Name = "INDlciHistoryChanges"
        Me.INDlciHistoryChanges.Size = New System.Drawing.Size(798, 101)
        Me.INDlciHistoryChanges.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciHistoryChanges.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciHistoryChanges.TextVisible = False
        '
        'INDlciRowType
        '
        Me.INDlciRowType.Control = Me.INDlblRowType
        Me.INDlciRowType.CustomizationFormText = "Tipo de Registro"
        Me.INDlciRowType.Location = New System.Drawing.Point(250, 0)
        Me.INDlciRowType.MaxSize = New System.Drawing.Size(0, 36)
        Me.INDlciRowType.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciRowType.Name = "INDlciRowType"
        Me.INDlciRowType.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDlciRowType.Size = New System.Drawing.Size(470, 36)
        Me.INDlciRowType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciRowType.Text = "Tipo de Registro"
        Me.INDlciRowType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciRowType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlciRowType.TextToControlDistance = 0
        Me.INDlciRowType.TextVisible = False
        '
        'INDlciStartDate
        '
        Me.INDlciStartDate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciStartDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciStartDate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciStartDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciStartDate.Control = Me.INDlblStartDate
        Me.INDlciStartDate.CustomizationFormText = "Fecha de Inicio"
        Me.INDlciStartDate.Location = New System.Drawing.Point(0, 186)
        Me.INDlciStartDate.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciStartDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciStartDate.Name = "INDlciStartDate"
        Me.INDlciStartDate.Size = New System.Drawing.Size(390, 30)
        Me.INDlciStartDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciStartDate.Text = "Fecha de Inicio"
        Me.INDlciStartDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciStartDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciStartDate.TextToControlDistance = 12
        '
        'INDlciEndingDate
        '
        Me.INDlciEndingDate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciEndingDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciEndingDate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciEndingDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciEndingDate.Control = Me.INDlblEndingDate
        Me.INDlciEndingDate.CustomizationFormText = "Fecha de Terminación"
        Me.INDlciEndingDate.Location = New System.Drawing.Point(390, 186)
        Me.INDlciEndingDate.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciEndingDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciEndingDate.Name = "INDlciEndingDate"
        Me.INDlciEndingDate.Size = New System.Drawing.Size(410, 30)
        Me.INDlciEndingDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEndingDate.Text = "Fecha de Terminación"
        Me.INDlciEndingDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciEndingDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciEndingDate.TextToControlDistance = 12
        '
        'INDlciSalary
        '
        Me.INDlciSalary.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciSalary.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciSalary.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciSalary.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciSalary.Control = Me.INDlblSalary
        Me.INDlciSalary.CustomizationFormText = "Salario"
        Me.INDlciSalary.Location = New System.Drawing.Point(390, 216)
        Me.INDlciSalary.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciSalary.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciSalary.Name = "INDlciSalary"
        Me.INDlciSalary.Size = New System.Drawing.Size(410, 30)
        Me.INDlciSalary.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciSalary.Text = "Salario"
        Me.INDlciSalary.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciSalary.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciSalary.TextToControlDistance = 12
        '
        'INDlciFunctionalUnit
        '
        Me.INDlciFunctionalUnit.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciFunctionalUnit.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciFunctionalUnit.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciFunctionalUnit.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciFunctionalUnit.Control = Me.INDlblFunctionalUnit
        Me.INDlciFunctionalUnit.CustomizationFormText = "Unidad Funcional"
        Me.INDlciFunctionalUnit.Location = New System.Drawing.Point(390, 246)
        Me.INDlciFunctionalUnit.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciFunctionalUnit.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciFunctionalUnit.Name = "INDlciFunctionalUnit"
        Me.INDlciFunctionalUnit.Size = New System.Drawing.Size(410, 30)
        Me.INDlciFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciFunctionalUnit.Text = "Unidad Funcional"
        Me.INDlciFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciFunctionalUnit.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciFunctionalUnit.TextToControlDistance = 12
        '
        'INDlciPosition
        '
        Me.INDlciPosition.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciPosition.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciPosition.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciPosition.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciPosition.Control = Me.INDpcePosition
        Me.INDlciPosition.CustomizationFormText = "Cargo"
        Me.INDlciPosition.Location = New System.Drawing.Point(0, 156)
        Me.INDlciPosition.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciPosition.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciPosition.Name = "INDlciPosition"
        Me.INDlciPosition.Size = New System.Drawing.Size(800, 30)
        Me.INDlciPosition.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPosition.Text = "Cargo"
        Me.INDlciPosition.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciPosition.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciPosition.TextToControlDistance = 12
        Me.INDlciPosition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.LabelControl1
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(250, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(250, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(250, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 306)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(0, 30)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(390, 30)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(410, 30)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlciJobBondingDate
        '
        Me.INDlciJobBondingDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciJobBondingDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciJobBondingDate.Control = Me.INDlblJobBondingDate
        Me.INDlciJobBondingDate.CustomizationFormText = "Fecha Contratación"
        Me.INDlciJobBondingDate.Location = New System.Drawing.Point(390, 96)
        Me.INDlciJobBondingDate.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciJobBondingDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciJobBondingDate.Name = "INDlciJobBondingDate"
        Me.INDlciJobBondingDate.Size = New System.Drawing.Size(410, 30)
        Me.INDlciJobBondingDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciJobBondingDate.Text = "Fecha Contratación"
        Me.INDlciJobBondingDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciJobBondingDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciJobBondingDate.TextToControlDistance = 12
        '
        'INDlciResolutionNumber
        '
        Me.INDlciResolutionNumber.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciResolutionNumber.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciResolutionNumber.Control = Me.INDlblResolutionNumber
        Me.INDlciResolutionNumber.CustomizationFormText = "Número de Decreto"
        Me.INDlciResolutionNumber.Location = New System.Drawing.Point(0, 36)
        Me.INDlciResolutionNumber.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciResolutionNumber.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciResolutionNumber.Name = "INDlciResolutionNumber"
        Me.INDlciResolutionNumber.Size = New System.Drawing.Size(390, 30)
        Me.INDlciResolutionNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciResolutionNumber.Text = "# Resolución"
        Me.INDlciResolutionNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciResolutionNumber.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciResolutionNumber.TextToControlDistance = 12
        Me.INDlciResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciCertificateOfficeNumber
        '
        Me.INDlciCertificateOfficeNumber.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciCertificateOfficeNumber.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciCertificateOfficeNumber.Control = Me.INDlblPosesionNumber
        Me.INDlciCertificateOfficeNumber.CustomizationFormText = "# Acta Posesión"
        Me.INDlciCertificateOfficeNumber.Location = New System.Drawing.Point(0, 66)
        Me.INDlciCertificateOfficeNumber.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciCertificateOfficeNumber.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciCertificateOfficeNumber.Name = "INDlciCertificateOfficeNumber"
        Me.INDlciCertificateOfficeNumber.Size = New System.Drawing.Size(390, 30)
        Me.INDlciCertificateOfficeNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCertificateOfficeNumber.Text = "# Acta Posesión"
        Me.INDlciCertificateOfficeNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCertificateOfficeNumber.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciCertificateOfficeNumber.TextToControlDistance = 12
        Me.INDlciCertificateOfficeNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciPosesionDate
        '
        Me.INDlciPosesionDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciPosesionDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciPosesionDate.Control = Me.INDlblPosesionDate
        Me.INDlciPosesionDate.CustomizationFormText = "Fecha de Posesión"
        Me.INDlciPosesionDate.Location = New System.Drawing.Point(390, 66)
        Me.INDlciPosesionDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciPosesionDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciPosesionDate.Name = "INDlciPosesionDate"
        Me.INDlciPosesionDate.Size = New System.Drawing.Size(410, 30)
        Me.INDlciPosesionDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPosesionDate.Text = "Fecha de Posesión"
        Me.INDlciPosesionDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciPosesionDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciPosesionDate.TextToControlDistance = 12
        Me.INDlciPosesionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlciResolutionDate
        '
        Me.INDlciResolutionDate.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkGray
        Me.INDlciResolutionDate.AppearanceItemCaption.Options.UseForeColor = True
        Me.INDlciResolutionDate.Control = Me.INDlblResolutionDate
        Me.INDlciResolutionDate.CustomizationFormText = "Fecha Resolución"
        Me.INDlciResolutionDate.Location = New System.Drawing.Point(390, 36)
        Me.INDlciResolutionDate.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDlciResolutionDate.MinSize = New System.Drawing.Size(390, 30)
        Me.INDlciResolutionDate.Name = "INDlciResolutionDate"
        Me.INDlciResolutionDate.Size = New System.Drawing.Size(410, 30)
        Me.INDlciResolutionDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciResolutionDate.Text = "Fecha Resolución"
        Me.INDlciResolutionDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciResolutionDate.TextSize = New System.Drawing.Size(160, 20)
        Me.INDlciResolutionDate.TextToControlDistance = 12
        Me.INDlciResolutionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit2
        '
        'CtrContractViewer
        '
        Me.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDlyCtlContractViewer)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MinimumSize = New System.Drawing.Size(500, 350)
        Me.Name = "CtrContractViewer"
        Me.Size = New System.Drawing.Size(800, 700)
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtlContractViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtlContractViewer.ResumeLayout(False)
        CType(Me.INDpccModifyContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccModifyContract.ResumeLayout(False)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccFinishContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccFinishContract.ResumeLayout(False)
        CType(Me.INDlyCtlFinishContract, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtlFinishContract.ResumeLayout(False)
        CType(Me.INDdteActualRetirementDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteActualRetirementDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleActualRetirementReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvSleActualRetirementReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgFinishContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciConfirmRetirement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgFinishActualContract, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciActualRetirementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciActualRetirementReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccContractEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccContractEdit.ResumeLayout(False)
        CType(Me.INDpccPositionInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpcePosition.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrdContractHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvGrdContractHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepRowType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepContractHistoryMoreActionPop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccOldContractViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccOldContractViewer.ResumeLayout(False)
        CType(Me.INDlyCtlOldContractViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtlOldContractViewer.ResumeLayout(False)
        CType(Me.INDgrdContractCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvGrdContractCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgOldContractViewer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCompareAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgOldContractInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldPaymentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldPaymentPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldRetirementReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldBasicSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldContractInitialDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldJobBondingType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldContractType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldRowType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldJobBondingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldContractEndingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldRetirementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciOldSalaryType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgContractCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciContractCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrepContractHistoryMoreAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrdHistoryChanges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgrvHistoryChanges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDppmContractViewerActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgContractViewer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciContractViewerActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciContractNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPositionOld, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgContractMoreInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciContractType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciRetirementReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciRetirementDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPaymentPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPaymentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciJobBondingType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciShowMoreInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciShowContractHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciSalaryType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgContractHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciContractHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgHistoryChanges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciHistoryChanges, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciRowType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEndingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPosition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciJobBondingDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciResolutionNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCertificateOfficeNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPosesionDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciResolutionDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Private WithEvents INDlblRowType As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblContractNo As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblPosition As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblCostCenter As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblFunctionalUnit As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblGroup As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblSalary As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblEndingDate As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblStartDate As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblShowMoreInfo As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblShowContractHistory As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblRetirementDate As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblRetirementReason As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblJobBondingType As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblContractType As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblSalaryType As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDgrdContractHistory As DevExpress.XtraGrid.GridControl
    Private WithEvents INDlblPaymentType As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlblPaymentPeriod As DevExpress.XtraEditors.LabelControl
    Private WithEvents INDlyCtlContractViewer As DevExpress.XtraLayout.LayoutControl
    Private WithEvents INDlcgContractViewer As DevExpress.XtraLayout.LayoutControlGroup
    Private WithEvents INDlciContractNo As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciRowType As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciPositionOld As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciSalary As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciGroup As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciCostCenter As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciStartDate As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciEndingDate As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciShowMoreInfo As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciShowContractHistory As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciContractType As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciJobBondingType As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciRetirementReason As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciRetirementDate As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciSalaryType As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciPaymentPeriod As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlciPaymentType As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents INDlcgContractHistory As DevExpress.XtraLayout.LayoutControlGroup
    Private WithEvents INDlcgHistoryChanges As DevExpress.XtraLayout.LayoutControlGroup
    Private WithEvents INDlciHistoryChanges As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Private WithEvents INDbtnContractViewerActions As DevExpress.XtraEditors.DropDownButton
    Private WithEvents INDlciContractViewerActions As DevExpress.XtraLayout.LayoutControlItem
    Private WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Private WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Private WithEvents INDgrvGrdContractHistory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgrcHistGroupLevel1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcHistGroupLevel2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcHistType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcHistValueOld As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcHistValueNew As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcHistUserCodeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcHistDate As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents INDlcgContractMoreInfo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgrcRowType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcContractNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcPosition As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcInitialDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcEndingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcAction As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDrepRowType As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDrepContractHistoryMoreAction As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents INDpccOldContractViewer As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDrepContractHistoryMoreActionPop As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDlyCtlOldContractViewer As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgOldContractViewer As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlblOldRetirementDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldRetirementReason As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldBasicSalary As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldContractEndingDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldContractInitialDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldJobBondingDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldJobBondingType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldContractType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldFunctionalUnit As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldPosition As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldRowType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlciOldRowType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldPosition As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldContractType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldJobBondingType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldJobBondingDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldContractInitialDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldContractEndingDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldBasicSalary As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldRetirementReason As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldRetirementDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciContractHistory As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblOldPaymentType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldSalaryType As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblOldPaymentPeriod As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlciOldPaymentPeriod As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldPaymentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciOldSalaryType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblCompareAction As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlciCompareAction As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgrdContractCompare As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgrvGrdContractCompare As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDgrdHistoryChanges As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgrvHistoryChanges As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciContractCompare As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgOldContractInfo As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlcgContractCompare As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgrcConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcSelectedContract As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcActualContract As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcInitialContract As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpcePosition As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlciPosition As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpccPositionInfo As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDpccContractEdit As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDctrContractEdit As Presentation.Payroll.CtrContractEdit
    Friend WithEvents INDpccFinishContract As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDlyCtlFinishContract As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDbtnConfirmRetirement As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDdteActualRetirementDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDsleActualRetirementReason As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgrvSleActualRetirementReason As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlcgFinishContract As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciConfirmRetirement As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlcgFinishActualContract As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciActualRetirementDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciActualRetirementReason As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDgrcRetirementReasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgrcRetirementReasonDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpccModifyContract As DevExpress.XtraBars.PopupControlContainer
    Friend WithEvents INDCtrContractModify As Presentation.Payroll.CtrContractModify
    Friend WithEvents INDppmContractViewerActions As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDbbiAddNewContract As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemButtonEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents INDbbiRenewContract As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbbiFinishContract As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDbbiDeleteContract As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDlblJobBondingDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlciJobBondingDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblPosesionDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlciPosesionDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblResolutionNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlciResolutionNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblResolutionDate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlblPosesionNumber As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlciCertificateOfficeNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciResolutionDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbbiActivateContract As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
