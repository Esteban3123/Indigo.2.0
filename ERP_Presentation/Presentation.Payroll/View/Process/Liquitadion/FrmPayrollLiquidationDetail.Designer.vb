Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPayrollLiquidationDetail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPayrollLiquidationDetail))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGroupConcept = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGrContractType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyItemUndefined = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDGcGroup = New DevExpress.XtraGrid.GridControl()
        Me.INDGvGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ChkConcept = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDColGroupCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGroupName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGroupLastDateLiquidation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGroupNextDateLiquidation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGroupLiquidationView = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.IndColVisualizarLiquidacion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepButtonEditVisualizar = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDBarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.INDGcDetailLiquidation = New DevExpress.XtraGrid.GridControl()
        Me.INDGvLiquitadionDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColConceptCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColConceptName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAccrued = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDeducted = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Acciones = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPopupContainerControl1 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnLiquidator = New System.Windows.Forms.Button()
        Me.INDteResultFormula = New DevExpress.XtraEditors.TextEdit()
        Me.INDteReplaceFormula = New System.Windows.Forms.TextBox()
        Me.INDteUsedFormula = New System.Windows.Forms.TextBox()
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyfUsedFormula1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyfReplaceFormula1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyfResultFormula1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcDetailLiquidationPatrono = New DevExpress.XtraGrid.GridControl()
        Me.INDGvLiquitadionDetailParafiscales = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColParafiscalConceptCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColParafiscalConceptName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColParafiscalConceptValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AccionesParafiscales = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPopupContainerControl2 = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl3 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDteResultFormulaPatrono = New DevExpress.XtraEditors.TextEdit()
        Me.INDteReplaceFormulaPatrono = New System.Windows.Forms.TextBox()
        Me.INDteUsedFormulaPatrono = New System.Windows.Forms.TextBox()
        Me.LayoutControlGroup9 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup11 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyfUsedFormula2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyfReplaceFormula2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyfResultFormula2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcMessage = New DevExpress.XtraGrid.GridControl()
        Me.INDGvFormulasConceptos = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColErrorType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImageCollection1 = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDColErrorDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyCGLiquidacion = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyBteIdNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDbteIdNumber = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLblEmployeeName = New System.Windows.Forms.Label()
        Me.CtrNavigation1 = New Presentation.Controls.CtrNavigation()
        Me.INDPnlResultLiquidation = New DevExpress.XtraEditors.PanelControl()
        Me.INDlycResultLiquidation = New DevExpress.XtraLayout.LayoutControl()
        Me.INDteIncentivePaymentIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteVacationIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteHealthDays = New System.Windows.Forms.TextBox()
        Me.INDteICBFIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteLicensesDays = New System.Windows.Forms.TextBox()
        Me.INDteCompensationFundIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteWorkDays = New System.Windows.Forms.TextBox()
        Me.INDteSENAIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteBasicSalary = New DevExpress.XtraEditors.TextEdit()
        Me.INDteUnemploymentIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteRTFIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteDailyBasicSalary = New DevExpress.XtraEditors.TextEdit()
        Me.INDteARLIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteTotalAccrued = New DevExpress.XtraEditors.TextEdit()
        Me.INDteHealthIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteTotalDeducted = New DevExpress.XtraEditors.TextEdit()
        Me.INDtePensionIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDteTotalPaid = New DevExpress.XtraEditors.TextEdit()
        Me.INDtePeriodIBC = New DevExpress.XtraEditors.TextEdit()
        Me.INDtePensionDays = New System.Windows.Forms.TextBox()
        Me.INDteSanctionDays = New System.Windows.Forms.TextBox()
        Me.INDteARLDays = New System.Windows.Forms.TextBox()
        Me.INDteProvisionDays = New System.Windows.Forms.TextBox()
        Me.INDteInabilitiesDays = New System.Windows.Forms.TextBox()
        Me.INDteVacationDays = New System.Windows.Forms.TextBox()
        Me.INDtePayrollDays = New System.Windows.Forms.TextBox()
        Me.INDteUnpaidLicensesDays = New System.Windows.Forms.TextBox()
        Me.INDtePensionIBCCPM = New DevExpress.XtraEditors.TextEdit()
        Me.INDtePensionIBCACCA = New DevExpress.XtraEditors.TextEdit()
        Me.INDlyGResultLiquidation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemControlPayroll = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemBasicSalary = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDailyBasicSalary = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTotalAccrued = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPensionDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSanctionsDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemARLDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemWorkDay = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPayrollDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTotalDeducted = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTotalPaid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemProvisionDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInabilitiesDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemVacationDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLicensesDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemUnpaidLicensesDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemHealthDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIBCControl = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPeriodIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPensionIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemHealthIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemARLIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRTFIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemUnemploymentIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSENAIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCompensationFundIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemICBFIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemVacationIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemIncentivePaymentIBC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPensionIBCCPM = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPensionIBCACCAI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCGLiquidacionDetalleDesprendible = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGcDetailLiquidation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCGDetalleNominaPatronales = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGcDetailLiquidationPatrono = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyMensajes = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGcMessage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyGcGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCtrNavigation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyLblEmployeeName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup10 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup12 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyCGMensajes = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGroupInability = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGroupNoveltyPending = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.SplitterItem1 = New DevExpress.XtraLayout.SplitterItem()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoTextEdit12 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoTextEdit111 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGroupConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemUndefined, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepButtonEditVisualizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDetailLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvLiquitadionDetail, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcDetailLiquidationPatrono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvLiquitadionDetailParafiscales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopupContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPopupContainerControl2.SuspendLayout()
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl3.SuspendLayout()
        CType(Me.INDteResultFormulaPatrono.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyfUsedFormula2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyfReplaceFormula2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyfResultFormula2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFormulasConceptos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCGLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyBteIdNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteIdNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDPnlResultLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPnlResultLiquidation.SuspendLayout()
        CType(Me.INDlycResultLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycResultLiquidation.SuspendLayout()
        CType(Me.INDteIncentivePaymentIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteVacationIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteICBFIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteCompensationFundIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteSENAIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteBasicSalary.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteUnemploymentIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteRTFIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteDailyBasicSalary.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteARLIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteTotalAccrued.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteHealthIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteTotalDeducted.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtePensionIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteTotalPaid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtePeriodIBC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtePensionIBCCPM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtePensionIBCACCA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGResultLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemControlPayroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBasicSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDailyBasicSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalAccrued, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPensionDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSanctionsDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemARLDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemWorkDay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPayrollDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalDeducted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalPaid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemProvisionDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInabilitiesDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemVacationDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLicensesDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUnpaidLicensesDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHealthDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIBCControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPeriodIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPensionIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHealthIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemARLIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRTFIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUnemploymentIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSENAIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCompensationFundIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemICBFIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemVacationIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemIncentivePaymentIBC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPensionIBCCPM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPensionIBCACCAI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCGLiquidacionDetalleDesprendible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGcDetailLiquidation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCGDetalleNominaPatronales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGcDetailLiquidationPatrono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyMensajes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGcMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGcGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtrNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyLblEmployeeName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCGMensajes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupInability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGroupNoveltyPending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitterItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit111, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1348, 578)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.ToolBars.Size = New System.Drawing.Size(1348, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.BarraBotones.Size = New System.Drawing.Size(1348, 130)
        '
        'RepositoryItemPopupContainerEdit5
        '
        Me.RepositoryItemPopupContainerEdit5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit5.Name = "RepositoryItemPopupContainerEdit5"
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
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1004, 608)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDLyGroupConcept
        '
        Me.INDLyGroupConcept.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGroupConcept.AppearanceGroup.Options.UseFont = True
        Me.INDLyGroupConcept.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGroupConcept.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGroupConcept.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGroupConcept.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGroupConcept.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyGroupConcept.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGroupConcept.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGroupConcept.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGroupConcept.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGroupConcept.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGroupConcept.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGroupConcept.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGroupConcept, False)
        Me.INDLyGroupConcept.CustomizationFormText = "LayoutControlGroup2"
        Me.INDLyGroupConcept.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGroupConcept.Name = "INDLyGroupConcept"
        Me.INDLyGroupConcept.Size = New System.Drawing.Size(574, 204)
        Me.INDLyGroupConcept.Text = "Información del Concepto"
        '
        'INDLyGrContractType
        '
        Me.INDLyGrContractType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGrContractType.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrContractType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGrContractType.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrContractType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrContractType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrContractType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyGrContractType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrContractType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrContractType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrContractType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrContractType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrContractType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrContractType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrContractType, False)
        Me.INDLyGrContractType.CustomizationFormText = "LayoutControlGroup2"
        Me.INDLyGrContractType.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGrContractType.Name = "INDLyGrContractType"
        Me.INDLyGrContractType.Size = New System.Drawing.Size(710, 320)
        Me.INDLyGrContractType.Text = "Tipo De Contrato"
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
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "INDLyGrContractType"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(710, 320)
        Me.LayoutControlGroup3.Text = "Tipo De Contrato"
        '
        'INDLyItemUndefined
        '
        Me.INDLyItemUndefined.CustomizationFormText = "LayoutControlItem1"
        Me.INDLyItemUndefined.Location = New System.Drawing.Point(0, 144)
        Me.INDLyItemUndefined.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyItemUndefined.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyItemUndefined.Name = "INDLyItemUndefined"
        Me.INDLyItemUndefined.Size = New System.Drawing.Size(686, 36)
        Me.INDLyItemUndefined.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemUndefined.Text = "Termino Indefinido"
        Me.INDLyItemUndefined.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLyItemUndefined.TextSize = New System.Drawing.Size(165, 21)
        Me.INDLyItemUndefined.TextToControlDistance = 12
        '
        'INDGcGroup
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcGroup, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcGroup, Nothing)
        Me.INDGcGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDGcGroup.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcGroup, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcGroup, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcGroup, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcGroup, False)
        Me.INDGcGroup.Location = New System.Drawing.Point(20, 60)
        Me.INDGcGroup.MainView = Me.INDGvGroup
        Me.INDGcGroup.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDGcGroup.MenuManager = Me.INDBarManager
        Me.INDGcGroup.Name = "INDGcGroup"
        Me.INDGcGroup.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemButtonEdit2, Me.INDRepButtonEditVisualizar})
        Me.INDGcGroup.Size = New System.Drawing.Size(1304, 22)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcGroup, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcGroup.TabIndex = 4
        Me.INDGcGroup.Tag = 0
        Me.INDGcGroup.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvGroup})
        '
        'INDGvGroup
        '
        Me.INDGvGroup.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvGroup.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvGroup.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvGroup.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvGroup.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvGroup.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvGroup.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvGroup.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvGroup.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvGroup.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvGroup.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvGroup.Appearance.Row.Options.UseFont = True
        Me.INDGvGroup.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvGroup.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvGroup.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ChkConcept, Me.INDColGroupCode, Me.INDColGroupName, Me.INDColGroupLastDateLiquidation, Me.INDColGroupNextDateLiquidation, Me.INDColGroupLiquidationView, Me.IndColVisualizarLiquidacion})
        Me.INDGvGroup.GridControl = Me.INDGcGroup
        Me.INDGvGroup.Name = "INDGvGroup"
        Me.INDGvGroup.OptionsCustomization.AllowColumnMoving = False
        Me.INDGvGroup.OptionsCustomization.AllowSort = False
        Me.INDGvGroup.OptionsDetail.EnableMasterViewMode = False
        Me.INDGvGroup.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvGroup.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvGroup.OptionsView.ShowAutoFilterRow = True
        Me.INDGvGroup.OptionsView.ShowDetailButtons = False
        Me.INDGvGroup.OptionsView.ShowGroupPanel = False
        Me.INDGvGroup.Tag = 496
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvGroup, False)
        '
        'ChkConcept
        '
        Me.ChkConcept.Caption = "Seleccione"
        Me.ChkConcept.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.ChkConcept.FieldName = "Apply"
        Me.ChkConcept.MinWidth = 17
        Me.ChkConcept.Name = "ChkConcept"
        Me.ChkConcept.Visible = True
        Me.ChkConcept.VisibleIndex = 0
        Me.ChkConcept.Width = 72
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'INDColGroupCode
        '
        Me.INDColGroupCode.Caption = "Código"
        Me.INDColGroupCode.FieldName = "Code"
        Me.INDColGroupCode.MinWidth = 17
        Me.INDColGroupCode.Name = "INDColGroupCode"
        Me.INDColGroupCode.OptionsColumn.AllowEdit = False
        Me.INDColGroupCode.Visible = True
        Me.INDColGroupCode.VisibleIndex = 1
        Me.INDColGroupCode.Width = 72
        '
        'INDColGroupName
        '
        Me.INDColGroupName.Caption = "Nombre"
        Me.INDColGroupName.FieldName = "Name"
        Me.INDColGroupName.MinWidth = 17
        Me.INDColGroupName.Name = "INDColGroupName"
        Me.INDColGroupName.OptionsColumn.AllowEdit = False
        Me.INDColGroupName.Visible = True
        Me.INDColGroupName.VisibleIndex = 2
        Me.INDColGroupName.Width = 249
        '
        'INDColGroupLastDateLiquidation
        '
        Me.INDColGroupLastDateLiquidation.Caption = "Últ. Fecha Liquidación"
        Me.INDColGroupLastDateLiquidation.FieldName = "LastDateLiquidation"
        Me.INDColGroupLastDateLiquidation.MinWidth = 17
        Me.INDColGroupLastDateLiquidation.Name = "INDColGroupLastDateLiquidation"
        Me.INDColGroupLastDateLiquidation.OptionsColumn.AllowEdit = False
        Me.INDColGroupLastDateLiquidation.Visible = True
        Me.INDColGroupLastDateLiquidation.VisibleIndex = 3
        Me.INDColGroupLastDateLiquidation.Width = 146
        '
        'INDColGroupNextDateLiquidation
        '
        Me.INDColGroupNextDateLiquidation.Caption = "Próx. Fecha Liquidación"
        Me.INDColGroupNextDateLiquidation.FieldName = "NextDateLiquidation"
        Me.INDColGroupNextDateLiquidation.MinWidth = 17
        Me.INDColGroupNextDateLiquidation.Name = "INDColGroupNextDateLiquidation"
        Me.INDColGroupNextDateLiquidation.OptionsColumn.AllowEdit = False
        Me.INDColGroupNextDateLiquidation.Visible = True
        Me.INDColGroupNextDateLiquidation.VisibleIndex = 4
        Me.INDColGroupNextDateLiquidation.Width = 150
        '
        'INDColGroupLiquidationView
        '
        Me.INDColGroupLiquidationView.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.25!, System.Drawing.FontStyle.Underline)
        Me.INDColGroupLiquidationView.AppearanceCell.Options.UseFont = True
        Me.INDColGroupLiquidationView.AppearanceCell.Options.UseTextOptions = True
        Me.INDColGroupLiquidationView.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColGroupLiquidationView.Caption = "Liquidación"
        Me.INDColGroupLiquidationView.ColumnEdit = Me.RepositoryItemButtonEdit2
        Me.INDColGroupLiquidationView.MinWidth = 17
        Me.INDColGroupLiquidationView.Name = "INDColGroupLiquidationView"
        Me.INDColGroupLiquidationView.Width = 62
        '
        'RepositoryItemButtonEdit2
        '
        Me.RepositoryItemButtonEdit2.AutoHeight = False
        Me.RepositoryItemButtonEdit2.Name = "RepositoryItemButtonEdit2"
        Me.RepositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'IndColVisualizarLiquidacion
        '
        Me.IndColVisualizarLiquidacion.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Symbol", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IndColVisualizarLiquidacion.AppearanceCell.Options.UseFont = True
        Me.IndColVisualizarLiquidacion.Caption = "Liquidación"
        Me.IndColVisualizarLiquidacion.ColumnEdit = Me.INDRepButtonEditVisualizar
        Me.IndColVisualizarLiquidacion.MinWidth = 17
        Me.IndColVisualizarLiquidacion.Name = "IndColVisualizarLiquidacion"
        Me.IndColVisualizarLiquidacion.Width = 62
        '
        'INDRepButtonEditVisualizar
        '
        Me.INDRepButtonEditVisualizar.AutoHeight = False
        Me.INDRepButtonEditVisualizar.Name = "INDRepButtonEditVisualizar"
        Me.INDRepButtonEditVisualizar.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'INDBarManager
        '
        Me.INDBarManager.DockControls.Add(Me.BarDockControl1)
        Me.INDBarManager.DockControls.Add(Me.BarDockControl2)
        Me.INDBarManager.DockControls.Add(Me.BarDockControl3)
        Me.INDBarManager.DockControls.Add(Me.BarDockControl4)
        Me.INDBarManager.Form = Me
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl1.Manager = Me.INDBarManager
        Me.BarDockControl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.BarDockControl1.Size = New System.Drawing.Size(1348, 0)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 713)
        Me.BarDockControl2.Manager = Me.INDBarManager
        Me.BarDockControl2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.BarDockControl2.Size = New System.Drawing.Size(1348, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl3.Manager = Me.INDBarManager
        Me.BarDockControl3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 708)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1348, 5)
        Me.BarDockControl4.Manager = Me.INDBarManager
        Me.BarDockControl4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 708)
        '
        'INDGcDetailLiquidation
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDetailLiquidation, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDetailLiquidation, Nothing)
        Me.INDGcDetailLiquidation.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDetailLiquidation, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDetailLiquidation, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDetailLiquidation, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDetailLiquidation, False)
        Me.INDGcDetailLiquidation.Location = New System.Drawing.Point(184, 53)
        Me.INDGcDetailLiquidation.MainView = Me.INDGvLiquitadionDetail
        Me.INDGcDetailLiquidation.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDGcDetailLiquidation.MenuManager = Me.INDBarManager
        Me.INDGcDetailLiquidation.Name = "INDGcDetailLiquidation"
        Me.INDGcDetailLiquidation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit1})
        Me.INDGcDetailLiquidation.Size = New System.Drawing.Size(583, 386)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDetailLiquidation, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcDetailLiquidation.TabIndex = 8
        Me.INDGcDetailLiquidation.Tag = 2766
        Me.INDGcDetailLiquidation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvLiquitadionDetail})
        '
        'INDGvLiquitadionDetail
        '
        Me.INDGvLiquitadionDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvLiquitadionDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvLiquitadionDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvLiquitadionDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvLiquitadionDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvLiquitadionDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvLiquitadionDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvLiquitadionDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvLiquitadionDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvLiquitadionDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvLiquitadionDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvLiquitadionDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvLiquitadionDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvLiquitadionDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvLiquitadionDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColConceptCode, Me.INDColConceptName, Me.INDColQuantity, Me.INDColAccrued, Me.INDColDeducted, Me.Acciones})
        Me.INDGvLiquitadionDetail.GridControl = Me.INDGcDetailLiquidation
        Me.INDGvLiquitadionDetail.Name = "INDGvLiquitadionDetail"
        Me.INDGvLiquitadionDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvLiquitadionDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvLiquitadionDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvLiquitadionDetail.OptionsView.ShowGroupPanel = False
        Me.INDGvLiquitadionDetail.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColConceptCode, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.INDGvLiquitadionDetail.Tag = 491
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvLiquitadionDetail, False)
        '
        'INDColConceptCode
        '
        Me.INDColConceptCode.Caption = "Código"
        Me.INDColConceptCode.FieldName = "ConceptCode"
        Me.INDColConceptCode.MinWidth = 17
        Me.INDColConceptCode.Name = "INDColConceptCode"
        Me.INDColConceptCode.OptionsColumn.AllowEdit = False
        Me.INDColConceptCode.Visible = True
        Me.INDColConceptCode.VisibleIndex = 0
        Me.INDColConceptCode.Width = 77
        '
        'INDColConceptName
        '
        Me.INDColConceptName.Caption = "Nombre"
        Me.INDColConceptName.FieldName = "ConceptDetail"
        Me.INDColConceptName.MinWidth = 17
        Me.INDColConceptName.Name = "INDColConceptName"
        Me.INDColConceptName.OptionsColumn.AllowEdit = False
        Me.INDColConceptName.Visible = True
        Me.INDColConceptName.VisibleIndex = 1
        Me.INDColConceptName.Width = 144
        '
        'INDColQuantity
        '
        Me.INDColQuantity.Caption = "Cantidad"
        Me.INDColQuantity.DisplayFormat.FormatString = "0.00"
        Me.INDColQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDColQuantity.FieldName = "Quantity"
        Me.INDColQuantity.MinWidth = 17
        Me.INDColQuantity.Name = "INDColQuantity"
        Me.INDColQuantity.OptionsColumn.AllowEdit = False
        Me.INDColQuantity.Visible = True
        Me.INDColQuantity.VisibleIndex = 2
        Me.INDColQuantity.Width = 61
        '
        'INDColAccrued
        '
        Me.INDColAccrued.Caption = "Devengado"
        Me.INDColAccrued.DisplayFormat.FormatString = "C2"
        Me.INDColAccrued.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColAccrued.FieldName = "AccruedValue"
        Me.INDColAccrued.MinWidth = 17
        Me.INDColAccrued.Name = "INDColAccrued"
        Me.INDColAccrued.OptionsColumn.AllowEdit = False
        Me.INDColAccrued.Visible = True
        Me.INDColAccrued.VisibleIndex = 3
        Me.INDColAccrued.Width = 96
        '
        'INDColDeducted
        '
        Me.INDColDeducted.Caption = "Deducido"
        Me.INDColDeducted.DisplayFormat.FormatString = "C2"
        Me.INDColDeducted.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColDeducted.FieldName = "DeductedValue"
        Me.INDColDeducted.MinWidth = 17
        Me.INDColDeducted.Name = "INDColDeducted"
        Me.INDColDeducted.OptionsColumn.AllowEdit = False
        Me.INDColDeducted.Visible = True
        Me.INDColDeducted.VisibleIndex = 4
        Me.INDColDeducted.Width = 87
        '
        'Acciones
        '
        Me.Acciones.Caption = "Acción"
        Me.Acciones.ColumnEdit = Me.RepositoryItemPopupContainerEdit1
        Me.Acciones.MinWidth = 17
        Me.Acciones.Name = "Acciones"
        Me.Acciones.Visible = True
        Me.Acciones.VisibleIndex = 5
        Me.Acciones.Width = 97
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
        Me.INDPopupContainerControl1.Location = New System.Drawing.Point(984, 88)
        Me.INDPopupContainerControl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPopupContainerControl1.Name = "INDPopupContainerControl1"
        Me.INDPopupContainerControl1.Size = New System.Drawing.Size(351, 313)
        Me.INDPopupContainerControl1.TabIndex = 54
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDBtnLiquidator)
        Me.LayoutControl2.Controls.Add(Me.INDteResultFormula)
        Me.LayoutControl2.Controls.Add(Me.INDteReplaceFormula)
        Me.LayoutControl2.Controls.Add(Me.INDteUsedFormula)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(481, 202, 650, 400)
        Me.LayoutControl2.Root = Me.LayoutControlGroup7
        Me.LayoutControl2.Size = New System.Drawing.Size(351, 313)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDBtnLiquidator
        '
        Me.INDBtnLiquidator.BackColor = System.Drawing.Color.Transparent
        Me.INDBtnLiquidator.FlatAppearance.BorderSize = 0
        Me.INDBtnLiquidator.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.INDBtnLiquidator.Image = Global.Presentation.Payroll.My.Resources.Resources.Calculadora_16x16_013
        Me.INDBtnLiquidator.Location = New System.Drawing.Point(298, 53)
        Me.INDBtnLiquidator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDBtnLiquidator.MaximumSize = New System.Drawing.Size(33, 30)
        Me.INDBtnLiquidator.MinimumSize = New System.Drawing.Size(33, 30)
        Me.INDBtnLiquidator.Name = "INDBtnLiquidator"
        Me.INDBtnLiquidator.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.INDBtnLiquidator.Size = New System.Drawing.Size(33, 30)
        Me.INDBtnLiquidator.TabIndex = 4
        Me.INDBtnLiquidator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.INDBtnLiquidator.UseVisualStyleBackColor = False
        Me.INDBtnLiquidator.Visible = False
        '
        'INDteResultFormula
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteResultFormula, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteResultFormula, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteResultFormula, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteResultFormula, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteResultFormula, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteResultFormula, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteResultFormula, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteResultFormula, False)
        Me.INDteResultFormula.Location = New System.Drawing.Point(106, 261)
        Me.INDteResultFormula.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteResultFormula, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteResultFormula, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteResultFormula, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteResultFormula, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteResultFormula.MaximumSize = New System.Drawing.Size(202, 28)
        Me.INDteResultFormula.MenuManager = Me.INDBarManager
        Me.INDteResultFormula.MinimumSize = New System.Drawing.Size(202, 28)
        Me.INDteResultFormula.Name = "INDteResultFormula"
        Me.INDteResultFormula.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteResultFormula.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteResultFormula.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteResultFormula.Properties.Appearance.Options.UseBackColor = True
        Me.INDteResultFormula.Properties.Appearance.Options.UseFont = True
        Me.INDteResultFormula.Properties.Appearance.Options.UseForeColor = True
        Me.INDteResultFormula.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteResultFormula.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteResultFormula.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteResultFormula.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteResultFormula.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteResultFormula.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteResultFormula.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteResultFormula.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteResultFormula.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteResultFormula.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteResultFormula.Properties.Mask.EditMask = "c2"
        Me.INDteResultFormula.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteResultFormula.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteResultFormula.Properties.ReadOnly = True
        Me.INDteResultFormula.Size = New System.Drawing.Size(202, 28)
        Me.INDteResultFormula.StyleController = Me.LayoutControl2
        Me.INDteResultFormula.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteResultFormula, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteResultFormula, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteResultFormula, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteResultFormula, 0)
        '
        'INDteReplaceFormula
        '
        Me.INDteReplaceFormula.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.INDteReplaceFormula.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDteReplaceFormula.Location = New System.Drawing.Point(106, 153)
        Me.INDteReplaceFormula.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteReplaceFormula.MaximumSize = New System.Drawing.Size(203, 101)
        Me.INDteReplaceFormula.MinimumSize = New System.Drawing.Size(203, 101)
        Me.INDteReplaceFormula.Multiline = True
        Me.INDteReplaceFormula.Name = "INDteReplaceFormula"
        Me.INDteReplaceFormula.ReadOnly = True
        Me.INDteReplaceFormula.Size = New System.Drawing.Size(203, 101)
        Me.INDteReplaceFormula.TabIndex = 1
        '
        'INDteUsedFormula
        '
        Me.INDteUsedFormula.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.INDteUsedFormula.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDteUsedFormula.Location = New System.Drawing.Point(106, 53)
        Me.INDteUsedFormula.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteUsedFormula.MaximumSize = New System.Drawing.Size(203, 96)
        Me.INDteUsedFormula.MinimumSize = New System.Drawing.Size(203, 96)
        Me.INDteUsedFormula.Multiline = True
        Me.INDteUsedFormula.Name = "INDteUsedFormula"
        Me.INDteUsedFormula.ReadOnly = True
        Me.INDteUsedFormula.Size = New System.Drawing.Size(203, 96)
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
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(351, 313)
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
        Me.LayoutControlGroup8.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyfUsedFormula1, Me.INDlyfReplaceFormula1, Me.INDlyfResultFormula1, Me.LayoutControlItem1})
        Me.LayoutControlGroup8.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup8.Name = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Size = New System.Drawing.Size(335, 293)
        Me.LayoutControlGroup8.Text = "Fórmulas"
        '
        'INDlyfUsedFormula1
        '
        Me.INDlyfUsedFormula1.Control = Me.INDteUsedFormula
        Me.INDlyfUsedFormula1.CustomizationFormText = "Usada"
        Me.INDlyfUsedFormula1.Location = New System.Drawing.Point(0, 0)
        Me.INDlyfUsedFormula1.MinSize = New System.Drawing.Size(77, 100)
        Me.INDlyfUsedFormula1.Name = "INDlyfUsedFormula1"
        Me.INDlyfUsedFormula1.Size = New System.Drawing.Size(278, 100)
        Me.INDlyfUsedFormula1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyfUsedFormula1.Text = "Usada"
        Me.INDlyfUsedFormula1.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDlyfReplaceFormula1
        '
        Me.INDlyfReplaceFormula1.Control = Me.INDteReplaceFormula
        Me.INDlyfReplaceFormula1.CustomizationFormText = "Reemplazada"
        Me.INDlyfReplaceFormula1.Location = New System.Drawing.Point(0, 100)
        Me.INDlyfReplaceFormula1.MinSize = New System.Drawing.Size(77, 100)
        Me.INDlyfReplaceFormula1.Name = "INDlyfReplaceFormula1"
        Me.INDlyfReplaceFormula1.Size = New System.Drawing.Size(315, 108)
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
        Me.INDlyfResultFormula1.Size = New System.Drawing.Size(315, 32)
        Me.INDlyfResultFormula1.Text = "Resultado"
        Me.INDlyfResultFormula1.TextSize = New System.Drawing.Size(84, 17)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDBtnLiquidator
        Me.LayoutControlItem1.Location = New System.Drawing.Point(278, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(37, 100)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        Me.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDGcDetailLiquidationPatrono
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcDetailLiquidationPatrono, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcDetailLiquidationPatrono, Nothing)
        Me.INDGcDetailLiquidationPatrono.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcDetailLiquidationPatrono, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcDetailLiquidationPatrono, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcDetailLiquidationPatrono, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcDetailLiquidationPatrono, False)
        Me.INDGcDetailLiquidationPatrono.Location = New System.Drawing.Point(791, 53)
        Me.INDGcDetailLiquidationPatrono.MainView = Me.INDGvLiquitadionDetailParafiscales
        Me.INDGcDetailLiquidationPatrono.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDGcDetailLiquidationPatrono.MenuManager = Me.INDBarManager
        Me.INDGcDetailLiquidationPatrono.Name = "INDGcDetailLiquidationPatrono"
        Me.INDGcDetailLiquidationPatrono.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit3})
        Me.INDGcDetailLiquidationPatrono.Size = New System.Drawing.Size(579, 386)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcDetailLiquidationPatrono, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcDetailLiquidationPatrono.TabIndex = 42
        Me.INDGcDetailLiquidationPatrono.Tag = 3340
        Me.INDGcDetailLiquidationPatrono.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvLiquitadionDetailParafiscales})
        '
        'INDGvLiquitadionDetailParafiscales
        '
        Me.INDGvLiquitadionDetailParafiscales.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvLiquitadionDetailParafiscales.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvLiquitadionDetailParafiscales.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvLiquitadionDetailParafiscales.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvLiquitadionDetailParafiscales.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvLiquitadionDetailParafiscales.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvLiquitadionDetailParafiscales.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvLiquitadionDetailParafiscales.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvLiquitadionDetailParafiscales.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvLiquitadionDetailParafiscales.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvLiquitadionDetailParafiscales.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvLiquitadionDetailParafiscales.Appearance.Row.Options.UseFont = True
        Me.INDGvLiquitadionDetailParafiscales.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvLiquitadionDetailParafiscales.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvLiquitadionDetailParafiscales.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColParafiscalConceptCode, Me.INDColParafiscalConceptName, Me.INDColParafiscalConceptValue, Me.AccionesParafiscales})
        Me.INDGvLiquitadionDetailParafiscales.GridControl = Me.INDGcDetailLiquidationPatrono
        Me.INDGvLiquitadionDetailParafiscales.Name = "INDGvLiquitadionDetailParafiscales"
        Me.INDGvLiquitadionDetailParafiscales.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvLiquitadionDetailParafiscales.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvLiquitadionDetailParafiscales.OptionsView.ShowAutoFilterRow = True
        Me.INDGvLiquitadionDetailParafiscales.OptionsView.ShowGroupPanel = False
        Me.INDGvLiquitadionDetailParafiscales.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDColParafiscalConceptCode, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.INDGvLiquitadionDetailParafiscales.Tag = 491
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvLiquitadionDetailParafiscales, False)
        '
        'INDColParafiscalConceptCode
        '
        Me.INDColParafiscalConceptCode.Caption = "Código"
        Me.INDColParafiscalConceptCode.FieldName = "ConceptCode"
        Me.INDColParafiscalConceptCode.MinWidth = 17
        Me.INDColParafiscalConceptCode.Name = "INDColParafiscalConceptCode"
        Me.INDColParafiscalConceptCode.OptionsColumn.AllowEdit = False
        Me.INDColParafiscalConceptCode.Visible = True
        Me.INDColParafiscalConceptCode.VisibleIndex = 0
        Me.INDColParafiscalConceptCode.Width = 61
        '
        'INDColParafiscalConceptName
        '
        Me.INDColParafiscalConceptName.Caption = "Nombre"
        Me.INDColParafiscalConceptName.FieldName = "ConceptDetail"
        Me.INDColParafiscalConceptName.MinWidth = 17
        Me.INDColParafiscalConceptName.Name = "INDColParafiscalConceptName"
        Me.INDColParafiscalConceptName.OptionsColumn.AllowEdit = False
        Me.INDColParafiscalConceptName.Visible = True
        Me.INDColParafiscalConceptName.VisibleIndex = 1
        Me.INDColParafiscalConceptName.Width = 248
        '
        'INDColParafiscalConceptValue
        '
        Me.INDColParafiscalConceptValue.Caption = "Valor"
        Me.INDColParafiscalConceptValue.DisplayFormat.FormatString = "C2"
        Me.INDColParafiscalConceptValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColParafiscalConceptValue.FieldName = "ConceptTotalValue"
        Me.INDColParafiscalConceptValue.MinWidth = 17
        Me.INDColParafiscalConceptValue.Name = "INDColParafiscalConceptValue"
        Me.INDColParafiscalConceptValue.OptionsColumn.AllowEdit = False
        Me.INDColParafiscalConceptValue.Visible = True
        Me.INDColParafiscalConceptValue.VisibleIndex = 2
        Me.INDColParafiscalConceptValue.Width = 89
        '
        'AccionesParafiscales
        '
        Me.AccionesParafiscales.Caption = "Acción"
        Me.AccionesParafiscales.ColumnEdit = Me.RepositoryItemPopupContainerEdit3
        Me.AccionesParafiscales.MinWidth = 17
        Me.AccionesParafiscales.Name = "AccionesParafiscales"
        Me.AccionesParafiscales.Visible = True
        Me.AccionesParafiscales.VisibleIndex = 3
        Me.AccionesParafiscales.Width = 62
        '
        'RepositoryItemPopupContainerEdit3
        '
        Me.RepositoryItemPopupContainerEdit3.AutoHeight = False
        Me.RepositoryItemPopupContainerEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit3.Name = "RepositoryItemPopupContainerEdit3"
        Me.RepositoryItemPopupContainerEdit3.PopupControl = Me.INDPopupContainerControl2
        Me.RepositoryItemPopupContainerEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPopupContainerControl2
        '
        Me.INDPopupContainerControl2.Controls.Add(Me.LayoutControl3)
        Me.INDPopupContainerControl2.Location = New System.Drawing.Point(997, 413)
        Me.INDPopupContainerControl2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPopupContainerControl2.Name = "INDPopupContainerControl2"
        Me.INDPopupContainerControl2.Size = New System.Drawing.Size(351, 313)
        Me.INDPopupContainerControl2.TabIndex = 55
        '
        'LayoutControl3
        '
        Me.LayoutControl3.Controls.Add(Me.INDteResultFormulaPatrono)
        Me.LayoutControl3.Controls.Add(Me.INDteReplaceFormulaPatrono)
        Me.LayoutControl3.Controls.Add(Me.INDteUsedFormulaPatrono)
        Me.LayoutControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LayoutControl3.Name = "LayoutControl3"
        Me.LayoutControl3.Root = Me.LayoutControlGroup9
        Me.LayoutControl3.Size = New System.Drawing.Size(351, 313)
        Me.LayoutControl3.TabIndex = 0
        Me.LayoutControl3.Text = "LayoutControl3"
        '
        'INDteResultFormulaPatrono
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteResultFormulaPatrono, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteResultFormulaPatrono, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteResultFormulaPatrono, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteResultFormulaPatrono, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteResultFormulaPatrono, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteResultFormulaPatrono, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteResultFormulaPatrono, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteResultFormulaPatrono, False)
        Me.INDteResultFormulaPatrono.Location = New System.Drawing.Point(106, 261)
        Me.INDteResultFormulaPatrono.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteResultFormulaPatrono, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteResultFormulaPatrono, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteResultFormulaPatrono, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteResultFormulaPatrono, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteResultFormulaPatrono.MenuManager = Me.INDBarManager
        Me.INDteResultFormulaPatrono.Name = "INDteResultFormulaPatrono"
        Me.INDteResultFormulaPatrono.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteResultFormulaPatrono.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteResultFormulaPatrono.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteResultFormulaPatrono.Properties.Appearance.Options.UseBackColor = True
        Me.INDteResultFormulaPatrono.Properties.Appearance.Options.UseFont = True
        Me.INDteResultFormulaPatrono.Properties.Appearance.Options.UseForeColor = True
        Me.INDteResultFormulaPatrono.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteResultFormulaPatrono.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteResultFormulaPatrono.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteResultFormulaPatrono.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteResultFormulaPatrono.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteResultFormulaPatrono.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteResultFormulaPatrono.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteResultFormulaPatrono.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteResultFormulaPatrono.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteResultFormulaPatrono.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteResultFormulaPatrono.Properties.Mask.EditMask = "c2"
        Me.INDteResultFormulaPatrono.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteResultFormulaPatrono.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteResultFormulaPatrono.Properties.ReadOnly = True
        Me.INDteResultFormulaPatrono.Size = New System.Drawing.Size(225, 28)
        Me.INDteResultFormulaPatrono.StyleController = Me.LayoutControl3
        Me.INDteResultFormulaPatrono.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteResultFormulaPatrono, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteResultFormulaPatrono, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteResultFormulaPatrono, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteResultFormulaPatrono, 0)
        '
        'INDteReplaceFormulaPatrono
        '
        Me.INDteReplaceFormulaPatrono.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.INDteReplaceFormulaPatrono.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDteReplaceFormulaPatrono.Location = New System.Drawing.Point(106, 157)
        Me.INDteReplaceFormulaPatrono.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteReplaceFormulaPatrono.Multiline = True
        Me.INDteReplaceFormulaPatrono.Name = "INDteReplaceFormulaPatrono"
        Me.INDteReplaceFormulaPatrono.ReadOnly = True
        Me.INDteReplaceFormulaPatrono.Size = New System.Drawing.Size(225, 100)
        Me.INDteReplaceFormulaPatrono.TabIndex = 1
        '
        'INDteUsedFormulaPatrono
        '
        Me.INDteUsedFormulaPatrono.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.INDteUsedFormulaPatrono.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDteUsedFormulaPatrono.Location = New System.Drawing.Point(106, 53)
        Me.INDteUsedFormulaPatrono.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteUsedFormulaPatrono.Multiline = True
        Me.INDteUsedFormulaPatrono.Name = "INDteUsedFormulaPatrono"
        Me.INDteUsedFormulaPatrono.ReadOnly = True
        Me.INDteUsedFormulaPatrono.Size = New System.Drawing.Size(225, 100)
        Me.INDteUsedFormulaPatrono.TabIndex = 0
        '
        'LayoutControlGroup9
        '
        Me.LayoutControlGroup9.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup9.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup9.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup9.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup9.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup9.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup9.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup9, False)
        Me.LayoutControlGroup9.CustomizationFormText = "LayoutControlGroup9"
        Me.LayoutControlGroup9.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup9.GroupBordersVisible = False
        Me.LayoutControlGroup9.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup11})
        Me.LayoutControlGroup9.Name = "LayoutControlGroup9"
        Me.LayoutControlGroup9.Size = New System.Drawing.Size(351, 313)
        Me.LayoutControlGroup9.TextVisible = False
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
        Me.LayoutControlGroup11.CustomizationFormText = "Fórmulas"
        Me.LayoutControlGroup11.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyfUsedFormula2, Me.INDlyfReplaceFormula2, Me.INDlyfResultFormula2})
        Me.LayoutControlGroup11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup11.Name = "LayoutControlGroup11"
        Me.LayoutControlGroup11.Size = New System.Drawing.Size(335, 293)
        Me.LayoutControlGroup11.Text = "Fórmulas"
        '
        'INDlyfUsedFormula2
        '
        Me.INDlyfUsedFormula2.Control = Me.INDteUsedFormulaPatrono
        Me.INDlyfUsedFormula2.CustomizationFormText = "Usada"
        Me.INDlyfUsedFormula2.Location = New System.Drawing.Point(0, 0)
        Me.INDlyfUsedFormula2.MaxSize = New System.Drawing.Size(0, 104)
        Me.INDlyfUsedFormula2.MinSize = New System.Drawing.Size(77, 104)
        Me.INDlyfUsedFormula2.Name = "INDlyfUsedFormula2"
        Me.INDlyfUsedFormula2.Size = New System.Drawing.Size(315, 104)
        Me.INDlyfUsedFormula2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyfUsedFormula2.Text = "Usada"
        Me.INDlyfUsedFormula2.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDlyfReplaceFormula2
        '
        Me.INDlyfReplaceFormula2.Control = Me.INDteReplaceFormulaPatrono
        Me.INDlyfReplaceFormula2.CustomizationFormText = "Reemplazada"
        Me.INDlyfReplaceFormula2.Location = New System.Drawing.Point(0, 104)
        Me.INDlyfReplaceFormula2.MinSize = New System.Drawing.Size(77, 104)
        Me.INDlyfReplaceFormula2.Name = "INDlyfReplaceFormula2"
        Me.INDlyfReplaceFormula2.Size = New System.Drawing.Size(315, 104)
        Me.INDlyfReplaceFormula2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyfReplaceFormula2.Text = "Reemplazada"
        Me.INDlyfReplaceFormula2.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDlyfResultFormula2
        '
        Me.INDlyfResultFormula2.Control = Me.INDteResultFormulaPatrono
        Me.INDlyfResultFormula2.CustomizationFormText = "Resultado"
        Me.INDlyfResultFormula2.Location = New System.Drawing.Point(0, 208)
        Me.INDlyfResultFormula2.Name = "INDlyfResultFormula2"
        Me.INDlyfResultFormula2.Size = New System.Drawing.Size(315, 32)
        Me.INDlyfResultFormula2.Text = "Resultado"
        Me.INDlyfResultFormula2.TextSize = New System.Drawing.Size(84, 17)
        '
        'INDGcMessage
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcMessage, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcMessage, Nothing)
        Me.INDGcMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.INDGcMessage.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcMessage, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcMessage, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcMessage, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcMessage, False)
        Me.INDGcMessage.Location = New System.Drawing.Point(1394, 53)
        Me.INDGcMessage.MainView = Me.INDGvFormulasConceptos
        Me.INDGcMessage.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDGcMessage.MenuManager = Me.INDBarManager
        Me.INDGcMessage.MinimumSize = New System.Drawing.Size(583, 0)
        Me.INDGcMessage.Name = "INDGcMessage"
        Me.INDGcMessage.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemImageComboBox1})
        Me.INDGcMessage.Size = New System.Drawing.Size(583, 386)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcMessage, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcMessage.TabIndex = 54
        Me.INDGcMessage.Tag = 3864
        Me.INDGcMessage.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvFormulasConceptos})
        '
        'INDGvFormulasConceptos
        '
        Me.INDGvFormulasConceptos.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFormulasConceptos.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFormulasConceptos.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvFormulasConceptos.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFormulasConceptos.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFormulasConceptos.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvFormulasConceptos.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFormulasConceptos.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFormulasConceptos.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFormulasConceptos.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFormulasConceptos.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFormulasConceptos.Appearance.Row.Options.UseFont = True
        Me.INDGvFormulasConceptos.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvFormulasConceptos.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvFormulasConceptos.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColErrorType, Me.INDColErrorDescription})
        Me.INDGvFormulasConceptos.GridControl = Me.INDGcMessage
        Me.INDGvFormulasConceptos.Name = "INDGvFormulasConceptos"
        Me.INDGvFormulasConceptos.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFormulasConceptos.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFormulasConceptos.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFormulasConceptos.OptionsView.ShowGroupPanel = False
        Me.INDGvFormulasConceptos.Tag = 491
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvFormulasConceptos, False)
        '
        'INDColErrorType
        '
        Me.INDColErrorType.AppearanceCell.Options.UseTextOptions = True
        Me.INDColErrorType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColErrorType.Caption = "Info"
        Me.INDColErrorType.ColumnEdit = Me.RepositoryItemImageComboBox1
        Me.INDColErrorType.FieldName = "Error"
        Me.INDColErrorType.MaxWidth = 42
        Me.INDColErrorType.MinWidth = 42
        Me.INDColErrorType.Name = "INDColErrorType"
        Me.INDColErrorType.OptionsColumn.AllowEdit = False
        Me.INDColErrorType.OptionsColumn.AllowFocus = False
        Me.INDColErrorType.OptionsColumn.ReadOnly = True
        Me.INDColErrorType.Visible = True
        Me.INDColErrorType.VisibleIndex = 0
        Me.INDColErrorType.Width = 42
        '
        'RepositoryItemImageComboBox1
        '
        Me.RepositoryItemImageComboBox1.AutoHeight = False
        Me.RepositoryItemImageComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)})
        Me.RepositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Grave", True, 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Sugerencia", False, 1)})
        Me.RepositoryItemImageComboBox1.LargeImages = Me.ImageCollection1
        Me.RepositoryItemImageComboBox1.Name = "RepositoryItemImageComboBox1"
        '
        'ImageCollection1
        '
        Me.ImageCollection1.ImageStream = CType(resources.GetObject("ImageCollection1.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.ImageCollection1.Images.SetKeyName(0, "rojo_16x16.png")
        Me.ImageCollection1.Images.SetKeyName(1, "verde_16x16.png")
        '
        'INDColErrorDescription
        '
        Me.INDColErrorDescription.Caption = "Descripción"
        Me.INDColErrorDescription.FieldName = "Description"
        Me.INDColErrorDescription.MinWidth = 17
        Me.INDColErrorDescription.Name = "INDColErrorDescription"
        Me.INDColErrorDescription.OptionsColumn.AllowEdit = False
        Me.INDColErrorDescription.Visible = True
        Me.INDColErrorDescription.VisibleIndex = 1
        Me.INDColErrorDescription.Width = 527
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit5
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
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup4"
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(909, 203)
        Me.LayoutControlGroup5.Text = "Grupos"
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
        Me.Root.CustomizationFormText = "Root"
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Location = New System.Drawing.Point(0, 0)
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(3664, 583)
        Me.Root.TextVisible = False
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
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyCGLiquidacion})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1344, 569)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyCGLiquidacion
        '
        Me.INDlyCGLiquidacion.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCGLiquidacion.AppearanceGroup.Options.UseFont = True
        Me.INDlyCGLiquidacion.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCGLiquidacion.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyCGLiquidacion.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGLiquidacion.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyCGLiquidacion.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyCGLiquidacion.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyCGLiquidacion.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGLiquidacion.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyCGLiquidacion.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGLiquidacion.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyCGLiquidacion.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGLiquidacion.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyCGLiquidacion, False)
        Me.INDlyCGLiquidacion.CustomizationFormText = "Liquidación"
        Me.INDlyCGLiquidacion.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyBteIdNumber, Me.INDlyGcGroup, Me.LayoutControlItem10, Me.INDlyCtrNavigation, Me.INDlyLblEmployeeName})
        Me.INDlyCGLiquidacion.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCGLiquidacion.Name = "INDlyCGLiquidacion"
        Me.INDlyCGLiquidacion.Size = New System.Drawing.Size(1328, 549)
        Me.INDlyCGLiquidacion.Text = "Liquidación"
        Me.INDlyCGLiquidacion.TextVisible = False
        '
        'INDlyBteIdNumber
        '
        Me.INDlyBteIdNumber.Control = Me.INDbteIdNumber
        Me.INDlyBteIdNumber.CustomizationFormText = "Empleado:"
        Me.INDlyBteIdNumber.Location = New System.Drawing.Point(0, 0)
        Me.INDlyBteIdNumber.MaxSize = New System.Drawing.Size(267, 36)
        Me.INDlyBteIdNumber.MinSize = New System.Drawing.Size(267, 36)
        Me.INDlyBteIdNumber.Name = "INDlyBteIdNumber"
        Me.INDlyBteIdNumber.Size = New System.Drawing.Size(1308, 36)
        Me.INDlyBteIdNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyBteIdNumber.Text = "Empleado:"
        Me.INDlyBteIdNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyBteIdNumber.TextSize = New System.Drawing.Size(125, 21)
        Me.INDlyBteIdNumber.TextToControlDistance = 3
        '
        'INDbteIdNumber
        '
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDbteIdNumber, False)
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDbteIdNumber, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDbteIdNumber, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteIdNumber, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDbteIdNumber, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDbteIdNumber, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDbteIdNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteIdNumber, True)
        Me.INDbteIdNumber.Location = New System.Drawing.Point(148, 24)
        Me.INDbteIdNumber.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDbteIdNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteIdNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDbteIdNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDbteIdNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteIdNumber.MaximumSize = New System.Drawing.Size(129, 36)
        Me.INDbteIdNumber.MinimumSize = New System.Drawing.Size(129, 28)
        Me.INDbteIdNumber.Name = "INDbteIdNumber"
        Me.INDbteIdNumber.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteIdNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteIdNumber.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDbteIdNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteIdNumber.Properties.Appearance.Options.UseFont = True
        Me.INDbteIdNumber.Properties.Appearance.Options.UseForeColor = True
        Me.INDbteIdNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteIdNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteIdNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteIdNumber.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDbteIdNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteIdNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteIdNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteIdNumber.Properties.AppearanceFocused.Options.UseForeColor = True
        EditorButtonImageOptions1.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDbteIdNumber.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbteIdNumber.Properties.Mask.EditMask = "[0-9]+"
        Me.INDbteIdNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbteIdNumber.Size = New System.Drawing.Size(129, 28)
        Me.INDbteIdNumber.StyleController = Me.LayoutControl1
        Me.INDbteIdNumber.TabIndex = 13
        Me.INDbteIdNumber.Tag = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteIdNumber, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDbteIdNumber, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDbteIdNumber, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDbteIdNumber, 0)
        Me.INDbteIdNumber.ToolTip = "Este Campo es Necesario"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDPopupContainerControl1)
        Me.LayoutControl1.Controls.Add(Me.INDLblEmployeeName)
        Me.LayoutControl1.Controls.Add(Me.CtrNavigation1)
        Me.LayoutControl1.Controls.Add(Me.INDPnlResultLiquidation)
        Me.LayoutControl1.Controls.Add(Me.INDPopupContainerControl2)
        Me.LayoutControl1.Controls.Add(Me.INDbteIdNumber)
        Me.LayoutControl1.Controls.Add(Me.INDGcGroup)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(8, 69, 516, 830)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1344, 569)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDLblEmployeeName
        '
        Me.INDLblEmployeeName.Font = New System.Drawing.Font("Segoe UI Light", 15.0!, System.Drawing.FontStyle.Bold)
        Me.INDLblEmployeeName.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.INDLblEmployeeName.Location = New System.Drawing.Point(100, 86)
        Me.INDLblEmployeeName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.INDLblEmployeeName.Name = "INDLblEmployeeName"
        Me.INDLblEmployeeName.Size = New System.Drawing.Size(1224, 60)
        Me.INDLblEmployeeName.TabIndex = 76
        Me.INDLblEmployeeName.Text = "INDLblEmployeeName"
        Me.INDLblEmployeeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CtrNavigation1
        '
        Me.CtrNavigation1.HideGroupContent = False
        Me.CtrNavigation1.Location = New System.Drawing.Point(20, 86)
        Me.CtrNavigation1.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.CtrNavigation1.MaximumSize = New System.Drawing.Size(0, 58)
        Me.CtrNavigation1.MinimumSize = New System.Drawing.Size(0, 50)
        Me.CtrNavigation1.Name = "CtrNavigation1"
        Me.CtrNavigation1.Size = New System.Drawing.Size(76, 58)
        Me.CtrNavigation1.TabIndex = 73
        '
        'INDPnlResultLiquidation
        '
        Me.INDPnlResultLiquidation.Controls.Add(Me.INDlycResultLiquidation)
        Me.INDPnlResultLiquidation.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPnlResultLiquidation.Location = New System.Drawing.Point(20, 150)
        Me.INDPnlResultLiquidation.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPnlResultLiquidation.Name = "INDPnlResultLiquidation"
        Me.INDPnlResultLiquidation.Size = New System.Drawing.Size(1304, 395)
        Me.INDPnlResultLiquidation.TabIndex = 72
        '
        'INDlycResultLiquidation
        '
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteIncentivePaymentIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteVacationIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDGcMessage)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteHealthDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteICBFIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteLicensesDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteCompensationFundIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteWorkDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteSENAIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteBasicSalary)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteUnemploymentIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteRTFIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteDailyBasicSalary)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteARLIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteTotalAccrued)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteHealthIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteTotalDeducted)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDtePensionIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteTotalPaid)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDtePeriodIBC)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDtePensionDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteSanctionDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteARLDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteProvisionDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDGcDetailLiquidation)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDGcDetailLiquidationPatrono)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteInabilitiesDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteVacationDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDtePayrollDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDteUnpaidLicensesDays)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDtePensionIBCCPM)
        Me.INDlycResultLiquidation.Controls.Add(Me.INDtePensionIBCACCA)
        Me.INDlycResultLiquidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycResultLiquidation.Location = New System.Drawing.Point(202, 2)
        Me.INDlycResultLiquidation.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDlycResultLiquidation.Name = "INDlycResultLiquidation"
        Me.INDlycResultLiquidation.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(591, 56, 526, 780)
        Me.INDlycResultLiquidation.Root = Me.INDlyGResultLiquidation
        Me.INDlycResultLiquidation.Size = New System.Drawing.Size(1100, 391)
        Me.INDlycResultLiquidation.TabIndex = 1
        Me.INDlycResultLiquidation.Text = "LayoutControl4"
        '
        'INDteIncentivePaymentIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteIncentivePaymentIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteIncentivePaymentIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteIncentivePaymentIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteIncentivePaymentIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteIncentivePaymentIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteIncentivePaymentIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteIncentivePaymentIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteIncentivePaymentIBC, False)
        Me.INDteIncentivePaymentIBC.Location = New System.Drawing.Point(1, 413)
        Me.INDteIncentivePaymentIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteIncentivePaymentIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteIncentivePaymentIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteIncentivePaymentIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteIncentivePaymentIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteIncentivePaymentIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteIncentivePaymentIBC.MenuManager = Me.INDBarManager
        Me.INDteIncentivePaymentIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteIncentivePaymentIBC.Name = "INDteIncentivePaymentIBC"
        Me.INDteIncentivePaymentIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteIncentivePaymentIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteIncentivePaymentIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteIncentivePaymentIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteIncentivePaymentIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteIncentivePaymentIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteIncentivePaymentIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteIncentivePaymentIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteIncentivePaymentIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteIncentivePaymentIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteIncentivePaymentIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteIncentivePaymentIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteIncentivePaymentIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteIncentivePaymentIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteIncentivePaymentIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteIncentivePaymentIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteIncentivePaymentIBC.Properties.Mask.EditMask = "c2"
        Me.INDteIncentivePaymentIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteIncentivePaymentIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteIncentivePaymentIBC.Properties.ReadOnly = True
        Me.INDteIncentivePaymentIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteIncentivePaymentIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteIncentivePaymentIBC.TabIndex = 74
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteIncentivePaymentIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteIncentivePaymentIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteIncentivePaymentIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteIncentivePaymentIBC, 0)
        '
        'INDteVacationIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteVacationIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteVacationIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteVacationIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteVacationIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteVacationIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteVacationIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteVacationIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteVacationIBC, False)
        Me.INDteVacationIBC.Location = New System.Drawing.Point(1, 383)
        Me.INDteVacationIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteVacationIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteVacationIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteVacationIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteVacationIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteVacationIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteVacationIBC.MenuManager = Me.INDBarManager
        Me.INDteVacationIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteVacationIBC.Name = "INDteVacationIBC"
        Me.INDteVacationIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteVacationIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteVacationIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteVacationIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteVacationIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteVacationIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteVacationIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteVacationIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteVacationIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteVacationIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteVacationIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteVacationIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteVacationIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteVacationIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteVacationIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteVacationIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteVacationIBC.Properties.Mask.EditMask = "c2"
        Me.INDteVacationIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteVacationIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteVacationIBC.Properties.ReadOnly = True
        Me.INDteVacationIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteVacationIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteVacationIBC.TabIndex = 73
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteVacationIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteVacationIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteVacationIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteVacationIBC, 0)
        '
        'INDteHealthDays
        '
        Me.INDteHealthDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteHealthDays.Location = New System.Drawing.Point(-204, 115)
        Me.INDteHealthDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteHealthDays.Name = "INDteHealthDays"
        Me.INDteHealthDays.ReadOnly = True
        Me.INDteHealthDays.Size = New System.Drawing.Size(19, 26)
        Me.INDteHealthDays.TabIndex = 64
        '
        'INDteICBFIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteICBFIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteICBFIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteICBFIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteICBFIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteICBFIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteICBFIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteICBFIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteICBFIBC, False)
        Me.INDteICBFIBC.Location = New System.Drawing.Point(1, 353)
        Me.INDteICBFIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteICBFIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteICBFIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteICBFIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteICBFIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteICBFIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteICBFIBC.MenuManager = Me.INDBarManager
        Me.INDteICBFIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteICBFIBC.Name = "INDteICBFIBC"
        Me.INDteICBFIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteICBFIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteICBFIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteICBFIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteICBFIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteICBFIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteICBFIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteICBFIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteICBFIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteICBFIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteICBFIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteICBFIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteICBFIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteICBFIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteICBFIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteICBFIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteICBFIBC.Properties.Mask.EditMask = "c2"
        Me.INDteICBFIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteICBFIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteICBFIBC.Properties.ReadOnly = True
        Me.INDteICBFIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteICBFIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteICBFIBC.TabIndex = 70
        Me.INDteICBFIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteICBFIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteICBFIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteICBFIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteICBFIBC, 0)
        '
        'INDteLicensesDays
        '
        Me.INDteLicensesDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteLicensesDays.Location = New System.Drawing.Point(-204, 329)
        Me.INDteLicensesDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteLicensesDays.Name = "INDteLicensesDays"
        Me.INDteLicensesDays.ReadOnly = True
        Me.INDteLicensesDays.Size = New System.Drawing.Size(19, 26)
        Me.INDteLicensesDays.TabIndex = 63
        '
        'INDteCompensationFundIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteCompensationFundIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteCompensationFundIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteCompensationFundIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteCompensationFundIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteCompensationFundIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteCompensationFundIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteCompensationFundIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteCompensationFundIBC, False)
        Me.INDteCompensationFundIBC.Location = New System.Drawing.Point(1, 323)
        Me.INDteCompensationFundIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteCompensationFundIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteCompensationFundIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteCompensationFundIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteCompensationFundIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteCompensationFundIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteCompensationFundIBC.MenuManager = Me.INDBarManager
        Me.INDteCompensationFundIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteCompensationFundIBC.Name = "INDteCompensationFundIBC"
        Me.INDteCompensationFundIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteCompensationFundIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteCompensationFundIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteCompensationFundIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteCompensationFundIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteCompensationFundIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteCompensationFundIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteCompensationFundIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteCompensationFundIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteCompensationFundIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteCompensationFundIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteCompensationFundIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteCompensationFundIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteCompensationFundIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteCompensationFundIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteCompensationFundIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteCompensationFundIBC.Properties.Mask.EditMask = "c2"
        Me.INDteCompensationFundIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteCompensationFundIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteCompensationFundIBC.Properties.ReadOnly = True
        Me.INDteCompensationFundIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteCompensationFundIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteCompensationFundIBC.TabIndex = 69
        Me.INDteCompensationFundIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteCompensationFundIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteCompensationFundIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteCompensationFundIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteCompensationFundIBC, 0)
        '
        'INDteWorkDays
        '
        Me.INDteWorkDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteWorkDays.Location = New System.Drawing.Point(-204, 85)
        Me.INDteWorkDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteWorkDays.Name = "INDteWorkDays"
        Me.INDteWorkDays.ReadOnly = True
        Me.INDteWorkDays.Size = New System.Drawing.Size(19, 26)
        Me.INDteWorkDays.TabIndex = 62
        '
        'INDteSENAIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteSENAIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteSENAIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteSENAIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteSENAIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteSENAIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteSENAIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteSENAIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteSENAIBC, False)
        Me.INDteSENAIBC.Location = New System.Drawing.Point(1, 293)
        Me.INDteSENAIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteSENAIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteSENAIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteSENAIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteSENAIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteSENAIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteSENAIBC.MenuManager = Me.INDBarManager
        Me.INDteSENAIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteSENAIBC.Name = "INDteSENAIBC"
        Me.INDteSENAIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteSENAIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteSENAIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteSENAIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteSENAIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteSENAIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteSENAIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteSENAIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteSENAIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteSENAIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteSENAIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteSENAIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteSENAIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteSENAIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteSENAIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteSENAIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteSENAIBC.Properties.Mask.EditMask = "c2"
        Me.INDteSENAIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteSENAIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteSENAIBC.Properties.ReadOnly = True
        Me.INDteSENAIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteSENAIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteSENAIBC.TabIndex = 68
        Me.INDteSENAIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteSENAIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteSENAIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteSENAIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteSENAIBC, 0)
        '
        'INDteBasicSalary
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteBasicSalary, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteBasicSalary, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteBasicSalary, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteBasicSalary, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteBasicSalary, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteBasicSalary, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteBasicSalary, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteBasicSalary, False)
        Me.INDteBasicSalary.Location = New System.Drawing.Point(-479, 53)
        Me.INDteBasicSalary.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteBasicSalary, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteBasicSalary, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteBasicSalary, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteBasicSalary, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteBasicSalary.MaximumSize = New System.Drawing.Size(0, 26)
        Me.INDteBasicSalary.MenuManager = Me.INDBarManager
        Me.INDteBasicSalary.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteBasicSalary.Name = "INDteBasicSalary"
        Me.INDteBasicSalary.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteBasicSalary.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteBasicSalary.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteBasicSalary.Properties.Appearance.Options.UseBackColor = True
        Me.INDteBasicSalary.Properties.Appearance.Options.UseFont = True
        Me.INDteBasicSalary.Properties.Appearance.Options.UseForeColor = True
        Me.INDteBasicSalary.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteBasicSalary.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteBasicSalary.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteBasicSalary.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteBasicSalary.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteBasicSalary.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteBasicSalary.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteBasicSalary.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteBasicSalary.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteBasicSalary.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteBasicSalary.Properties.Mask.EditMask = "c2"
        Me.INDteBasicSalary.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteBasicSalary.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteBasicSalary.Properties.ReadOnly = True
        Me.INDteBasicSalary.Size = New System.Drawing.Size(125, 26)
        Me.INDteBasicSalary.StyleController = Me.INDlycResultLiquidation
        Me.INDteBasicSalary.TabIndex = 57
        Me.INDteBasicSalary.Tag = 1898
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteBasicSalary, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteBasicSalary, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteBasicSalary, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteBasicSalary, 0)
        '
        'INDteUnemploymentIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteUnemploymentIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteUnemploymentIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteUnemploymentIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteUnemploymentIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteUnemploymentIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteUnemploymentIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteUnemploymentIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteUnemploymentIBC, False)
        Me.INDteUnemploymentIBC.Location = New System.Drawing.Point(1, 263)
        Me.INDteUnemploymentIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteUnemploymentIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteUnemploymentIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteUnemploymentIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteUnemploymentIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteUnemploymentIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteUnemploymentIBC.MenuManager = Me.INDBarManager
        Me.INDteUnemploymentIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteUnemploymentIBC.Name = "INDteUnemploymentIBC"
        Me.INDteUnemploymentIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteUnemploymentIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteUnemploymentIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteUnemploymentIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteUnemploymentIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteUnemploymentIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteUnemploymentIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteUnemploymentIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteUnemploymentIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteUnemploymentIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteUnemploymentIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteUnemploymentIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteUnemploymentIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteUnemploymentIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteUnemploymentIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteUnemploymentIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteUnemploymentIBC.Properties.Mask.EditMask = "c2"
        Me.INDteUnemploymentIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteUnemploymentIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteUnemploymentIBC.Properties.ReadOnly = True
        Me.INDteUnemploymentIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteUnemploymentIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteUnemploymentIBC.TabIndex = 67
        Me.INDteUnemploymentIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteUnemploymentIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteUnemploymentIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteUnemploymentIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteUnemploymentIBC, 0)
        '
        'INDteRTFIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteRTFIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteRTFIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteRTFIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteRTFIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteRTFIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteRTFIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteRTFIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteRTFIBC, False)
        Me.INDteRTFIBC.Location = New System.Drawing.Point(1, 233)
        Me.INDteRTFIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteRTFIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteRTFIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteRTFIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteRTFIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteRTFIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteRTFIBC.MenuManager = Me.INDBarManager
        Me.INDteRTFIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteRTFIBC.Name = "INDteRTFIBC"
        Me.INDteRTFIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteRTFIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteRTFIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteRTFIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteRTFIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteRTFIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteRTFIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteRTFIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteRTFIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteRTFIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteRTFIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteRTFIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteRTFIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteRTFIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteRTFIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteRTFIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteRTFIBC.Properties.Mask.EditMask = "c2"
        Me.INDteRTFIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteRTFIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteRTFIBC.Properties.ReadOnly = True
        Me.INDteRTFIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteRTFIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteRTFIBC.TabIndex = 66
        Me.INDteRTFIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteRTFIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteRTFIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteRTFIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteRTFIBC, 0)
        '
        'INDteDailyBasicSalary
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteDailyBasicSalary, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteDailyBasicSalary, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteDailyBasicSalary, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteDailyBasicSalary, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteDailyBasicSalary, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteDailyBasicSalary, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteDailyBasicSalary, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteDailyBasicSalary, False)
        Me.INDteDailyBasicSalary.Location = New System.Drawing.Point(-479, 85)
        Me.INDteDailyBasicSalary.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteDailyBasicSalary, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteDailyBasicSalary, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteDailyBasicSalary, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteDailyBasicSalary, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteDailyBasicSalary.MaximumSize = New System.Drawing.Size(0, 26)
        Me.INDteDailyBasicSalary.MenuManager = Me.INDBarManager
        Me.INDteDailyBasicSalary.MinimumSize = New System.Drawing.Size(0, 26)
        Me.INDteDailyBasicSalary.Name = "INDteDailyBasicSalary"
        Me.INDteDailyBasicSalary.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteDailyBasicSalary.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDailyBasicSalary.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteDailyBasicSalary.Properties.Appearance.Options.UseBackColor = True
        Me.INDteDailyBasicSalary.Properties.Appearance.Options.UseFont = True
        Me.INDteDailyBasicSalary.Properties.Appearance.Options.UseForeColor = True
        Me.INDteDailyBasicSalary.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteDailyBasicSalary.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteDailyBasicSalary.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteDailyBasicSalary.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteDailyBasicSalary.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteDailyBasicSalary.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteDailyBasicSalary.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteDailyBasicSalary.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteDailyBasicSalary.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteDailyBasicSalary.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteDailyBasicSalary.Properties.Mask.EditMask = "c2"
        Me.INDteDailyBasicSalary.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteDailyBasicSalary.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteDailyBasicSalary.Properties.ReadOnly = True
        Me.INDteDailyBasicSalary.Size = New System.Drawing.Size(102, 26)
        Me.INDteDailyBasicSalary.StyleController = Me.INDlycResultLiquidation
        Me.INDteDailyBasicSalary.TabIndex = 58
        Me.INDteDailyBasicSalary.Tag = 1898
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteDailyBasicSalary, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteDailyBasicSalary, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteDailyBasicSalary, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteDailyBasicSalary, 0)
        '
        'INDteARLIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteARLIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteARLIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteARLIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteARLIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteARLIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteARLIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteARLIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteARLIBC, False)
        Me.INDteARLIBC.Location = New System.Drawing.Point(1, 203)
        Me.INDteARLIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteARLIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteARLIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteARLIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteARLIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteARLIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteARLIBC.MenuManager = Me.INDBarManager
        Me.INDteARLIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteARLIBC.Name = "INDteARLIBC"
        Me.INDteARLIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteARLIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteARLIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteARLIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteARLIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteARLIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteARLIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteARLIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteARLIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteARLIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteARLIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteARLIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteARLIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteARLIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteARLIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteARLIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteARLIBC.Properties.Mask.EditMask = "c2"
        Me.INDteARLIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteARLIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteARLIBC.Properties.ReadOnly = True
        Me.INDteARLIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteARLIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteARLIBC.TabIndex = 65
        Me.INDteARLIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteARLIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteARLIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteARLIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteARLIBC, 0)
        '
        'INDteTotalAccrued
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteTotalAccrued, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteTotalAccrued, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteTotalAccrued, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteTotalAccrued, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteTotalAccrued, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteTotalAccrued, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteTotalAccrued, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteTotalAccrued, False)
        Me.INDteTotalAccrued.Location = New System.Drawing.Point(-479, 117)
        Me.INDteTotalAccrued.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteTotalAccrued, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteTotalAccrued, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteTotalAccrued, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteTotalAccrued, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteTotalAccrued.MaximumSize = New System.Drawing.Size(0, 26)
        Me.INDteTotalAccrued.MenuManager = Me.INDBarManager
        Me.INDteTotalAccrued.MinimumSize = New System.Drawing.Size(0, 26)
        Me.INDteTotalAccrued.Name = "INDteTotalAccrued"
        Me.INDteTotalAccrued.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteTotalAccrued.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteTotalAccrued.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteTotalAccrued.Properties.Appearance.Options.UseBackColor = True
        Me.INDteTotalAccrued.Properties.Appearance.Options.UseFont = True
        Me.INDteTotalAccrued.Properties.Appearance.Options.UseForeColor = True
        Me.INDteTotalAccrued.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteTotalAccrued.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteTotalAccrued.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteTotalAccrued.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteTotalAccrued.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteTotalAccrued.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteTotalAccrued.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteTotalAccrued.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteTotalAccrued.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteTotalAccrued.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteTotalAccrued.Properties.Mask.EditMask = "c2"
        Me.INDteTotalAccrued.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteTotalAccrued.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteTotalAccrued.Properties.ReadOnly = True
        Me.INDteTotalAccrued.Size = New System.Drawing.Size(102, 26)
        Me.INDteTotalAccrued.StyleController = Me.INDlycResultLiquidation
        Me.INDteTotalAccrued.TabIndex = 59
        Me.INDteTotalAccrued.Tag = 1898
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteTotalAccrued, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteTotalAccrued, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteTotalAccrued, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteTotalAccrued, 0)
        '
        'INDteHealthIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteHealthIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteHealthIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteHealthIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteHealthIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteHealthIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteHealthIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteHealthIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteHealthIBC, False)
        Me.INDteHealthIBC.Location = New System.Drawing.Point(1, 173)
        Me.INDteHealthIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteHealthIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteHealthIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteHealthIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteHealthIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteHealthIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDteHealthIBC.MenuManager = Me.INDBarManager
        Me.INDteHealthIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDteHealthIBC.Name = "INDteHealthIBC"
        Me.INDteHealthIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteHealthIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteHealthIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteHealthIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDteHealthIBC.Properties.Appearance.Options.UseFont = True
        Me.INDteHealthIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDteHealthIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteHealthIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteHealthIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteHealthIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteHealthIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteHealthIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteHealthIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteHealthIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteHealthIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteHealthIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteHealthIBC.Properties.Mask.EditMask = "c2"
        Me.INDteHealthIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteHealthIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteHealthIBC.Properties.ReadOnly = True
        Me.INDteHealthIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDteHealthIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDteHealthIBC.TabIndex = 64
        Me.INDteHealthIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteHealthIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteHealthIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteHealthIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteHealthIBC, 0)
        '
        'INDteTotalDeducted
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteTotalDeducted, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteTotalDeducted, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteTotalDeducted, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteTotalDeducted, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteTotalDeducted, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteTotalDeducted, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteTotalDeducted, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteTotalDeducted, False)
        Me.INDteTotalDeducted.Location = New System.Drawing.Point(-479, 149)
        Me.INDteTotalDeducted.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteTotalDeducted, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteTotalDeducted, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteTotalDeducted, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteTotalDeducted, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteTotalDeducted.MaximumSize = New System.Drawing.Size(0, 26)
        Me.INDteTotalDeducted.MenuManager = Me.INDBarManager
        Me.INDteTotalDeducted.MinimumSize = New System.Drawing.Size(0, 26)
        Me.INDteTotalDeducted.Name = "INDteTotalDeducted"
        Me.INDteTotalDeducted.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteTotalDeducted.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteTotalDeducted.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteTotalDeducted.Properties.Appearance.Options.UseBackColor = True
        Me.INDteTotalDeducted.Properties.Appearance.Options.UseFont = True
        Me.INDteTotalDeducted.Properties.Appearance.Options.UseForeColor = True
        Me.INDteTotalDeducted.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteTotalDeducted.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteTotalDeducted.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteTotalDeducted.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteTotalDeducted.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteTotalDeducted.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteTotalDeducted.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteTotalDeducted.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteTotalDeducted.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteTotalDeducted.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteTotalDeducted.Properties.Mask.EditMask = "c2"
        Me.INDteTotalDeducted.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteTotalDeducted.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteTotalDeducted.Properties.ReadOnly = True
        Me.INDteTotalDeducted.Size = New System.Drawing.Size(102, 26)
        Me.INDteTotalDeducted.StyleController = Me.INDlycResultLiquidation
        Me.INDteTotalDeducted.TabIndex = 60
        Me.INDteTotalDeducted.Tag = 1898
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteTotalDeducted, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteTotalDeducted, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteTotalDeducted, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteTotalDeducted, 0)
        '
        'INDtePensionIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDtePensionIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDtePensionIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtePensionIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtePensionIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtePensionIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtePensionIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDtePensionIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDtePensionIBC, False)
        Me.INDtePensionIBC.Location = New System.Drawing.Point(1, 83)
        Me.INDtePensionIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDtePensionIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDtePensionIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDtePensionIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDtePensionIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtePensionIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBC.MenuManager = Me.INDBarManager
        Me.INDtePensionIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBC.Name = "INDtePensionIBC"
        Me.INDtePensionIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtePensionIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePensionIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtePensionIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDtePensionIBC.Properties.Appearance.Options.UseFont = True
        Me.INDtePensionIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDtePensionIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtePensionIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtePensionIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtePensionIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtePensionIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePensionIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtePensionIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtePensionIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtePensionIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtePensionIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtePensionIBC.Properties.Mask.EditMask = "c2"
        Me.INDtePensionIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtePensionIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtePensionIBC.Properties.ReadOnly = True
        Me.INDtePensionIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDtePensionIBC.TabIndex = 63
        Me.INDtePensionIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtePensionIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtePensionIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDtePensionIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDtePensionIBC, 0)
        '
        'INDteTotalPaid
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDteTotalPaid, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDteTotalPaid, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDteTotalPaid, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteTotalPaid, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDteTotalPaid, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteTotalPaid, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDteTotalPaid, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDteTotalPaid, False)
        Me.INDteTotalPaid.Location = New System.Drawing.Point(-479, 181)
        Me.INDteTotalPaid.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDteTotalPaid, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDteTotalPaid, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDteTotalPaid, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDteTotalPaid, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDteTotalPaid.MaximumSize = New System.Drawing.Size(124, 0)
        Me.INDteTotalPaid.MenuManager = Me.INDBarManager
        Me.INDteTotalPaid.MinimumSize = New System.Drawing.Size(124, 0)
        Me.INDteTotalPaid.Name = "INDteTotalPaid"
        Me.INDteTotalPaid.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteTotalPaid.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteTotalPaid.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteTotalPaid.Properties.Appearance.Options.UseBackColor = True
        Me.INDteTotalPaid.Properties.Appearance.Options.UseFont = True
        Me.INDteTotalPaid.Properties.Appearance.Options.UseForeColor = True
        Me.INDteTotalPaid.Properties.Appearance.Options.UseTextOptions = True
        Me.INDteTotalPaid.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDteTotalPaid.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDteTotalPaid.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDteTotalPaid.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteTotalPaid.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDteTotalPaid.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDteTotalPaid.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDteTotalPaid.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteTotalPaid.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDteTotalPaid.Properties.Mask.EditMask = "c2"
        Me.INDteTotalPaid.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDteTotalPaid.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteTotalPaid.Properties.ReadOnly = True
        Me.INDteTotalPaid.Size = New System.Drawing.Size(124, 28)
        Me.INDteTotalPaid.StyleController = Me.INDlycResultLiquidation
        Me.INDteTotalPaid.TabIndex = 61
        Me.INDteTotalPaid.Tag = 1898
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteTotalPaid, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDteTotalPaid, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDteTotalPaid, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDteTotalPaid, 0)
        '
        'INDtePeriodIBC
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDtePeriodIBC, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDtePeriodIBC, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtePeriodIBC, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtePeriodIBC, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtePeriodIBC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtePeriodIBC, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDtePeriodIBC, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDtePeriodIBC, False)
        Me.INDtePeriodIBC.Location = New System.Drawing.Point(1, 53)
        Me.INDtePeriodIBC.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDtePeriodIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDtePeriodIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDtePeriodIBC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDtePeriodIBC, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDtePeriodIBC.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDtePeriodIBC.MenuManager = Me.INDBarManager
        Me.INDtePeriodIBC.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDtePeriodIBC.Name = "INDtePeriodIBC"
        Me.INDtePeriodIBC.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtePeriodIBC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePeriodIBC.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtePeriodIBC.Properties.Appearance.Options.UseBackColor = True
        Me.INDtePeriodIBC.Properties.Appearance.Options.UseFont = True
        Me.INDtePeriodIBC.Properties.Appearance.Options.UseForeColor = True
        Me.INDtePeriodIBC.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtePeriodIBC.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtePeriodIBC.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtePeriodIBC.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtePeriodIBC.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePeriodIBC.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtePeriodIBC.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtePeriodIBC.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtePeriodIBC.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtePeriodIBC.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtePeriodIBC.Properties.Mask.EditMask = "c2"
        Me.INDtePeriodIBC.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtePeriodIBC.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtePeriodIBC.Properties.ReadOnly = True
        Me.INDtePeriodIBC.Size = New System.Drawing.Size(125, 26)
        Me.INDtePeriodIBC.StyleController = Me.INDlycResultLiquidation
        Me.INDtePeriodIBC.TabIndex = 62
        Me.INDtePeriodIBC.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtePeriodIBC, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtePeriodIBC, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDtePeriodIBC, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDtePeriodIBC, 0)
        '
        'INDtePensionDays
        '
        Me.INDtePensionDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePensionDays.Location = New System.Drawing.Point(-204, 149)
        Me.INDtePensionDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDtePensionDays.Name = "INDtePensionDays"
        Me.INDtePensionDays.ReadOnly = True
        Me.INDtePensionDays.Size = New System.Drawing.Size(19, 26)
        Me.INDtePensionDays.TabIndex = 22
        Me.INDtePensionDays.Tag = 1898
        '
        'INDteSanctionDays
        '
        Me.INDteSanctionDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteSanctionDays.Location = New System.Drawing.Point(-204, 179)
        Me.INDteSanctionDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteSanctionDays.Name = "INDteSanctionDays"
        Me.INDteSanctionDays.ReadOnly = True
        Me.INDteSanctionDays.Size = New System.Drawing.Size(19, 26)
        Me.INDteSanctionDays.TabIndex = 25
        Me.INDteSanctionDays.Tag = 1898
        '
        'INDteARLDays
        '
        Me.INDteARLDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteARLDays.Location = New System.Drawing.Point(-204, 209)
        Me.INDteARLDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteARLDays.Name = "INDteARLDays"
        Me.INDteARLDays.ReadOnly = True
        Me.INDteARLDays.Size = New System.Drawing.Size(19, 26)
        Me.INDteARLDays.TabIndex = 24
        Me.INDteARLDays.Tag = 1898
        '
        'INDteProvisionDays
        '
        Me.INDteProvisionDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteProvisionDays.Location = New System.Drawing.Point(-204, 239)
        Me.INDteProvisionDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteProvisionDays.Name = "INDteProvisionDays"
        Me.INDteProvisionDays.ReadOnly = True
        Me.INDteProvisionDays.Size = New System.Drawing.Size(19, 26)
        Me.INDteProvisionDays.TabIndex = 16
        Me.INDteProvisionDays.Tag = 1898
        '
        'INDteInabilitiesDays
        '
        Me.INDteInabilitiesDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteInabilitiesDays.Location = New System.Drawing.Point(-204, 269)
        Me.INDteInabilitiesDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteInabilitiesDays.Name = "INDteInabilitiesDays"
        Me.INDteInabilitiesDays.ReadOnly = True
        Me.INDteInabilitiesDays.Size = New System.Drawing.Size(19, 26)
        Me.INDteInabilitiesDays.TabIndex = 27
        Me.INDteInabilitiesDays.Tag = 1898
        '
        'INDteVacationDays
        '
        Me.INDteVacationDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteVacationDays.Location = New System.Drawing.Point(-204, 299)
        Me.INDteVacationDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteVacationDays.Name = "INDteVacationDays"
        Me.INDteVacationDays.ReadOnly = True
        Me.INDteVacationDays.Size = New System.Drawing.Size(19, 26)
        Me.INDteVacationDays.TabIndex = 23
        Me.INDteVacationDays.Tag = 1898
        '
        'INDtePayrollDays
        '
        Me.INDtePayrollDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePayrollDays.Location = New System.Drawing.Point(-204, 53)
        Me.INDtePayrollDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDtePayrollDays.Name = "INDtePayrollDays"
        Me.INDtePayrollDays.ReadOnly = True
        Me.INDtePayrollDays.Size = New System.Drawing.Size(19, 26)
        Me.INDtePayrollDays.TabIndex = 14
        Me.INDtePayrollDays.Tag = 1898
        '
        'INDteUnpaidLicensesDays
        '
        Me.INDteUnpaidLicensesDays.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteUnpaidLicensesDays.Location = New System.Drawing.Point(-204, 359)
        Me.INDteUnpaidLicensesDays.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDteUnpaidLicensesDays.Name = "INDteUnpaidLicensesDays"
        Me.INDteUnpaidLicensesDays.ReadOnly = True
        Me.INDteUnpaidLicensesDays.Size = New System.Drawing.Size(19, 28)
        Me.INDteUnpaidLicensesDays.TabIndex = 28
        Me.INDteUnpaidLicensesDays.Tag = 1898
        '
        'INDtePensionIBCCPM
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDtePensionIBCCPM, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDtePensionIBCCPM, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtePensionIBCCPM, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtePensionIBCCPM, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtePensionIBCCPM, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtePensionIBCCPM, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDtePensionIBCCPM, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDtePensionIBCCPM, False)
        Me.INDtePensionIBCCPM.Location = New System.Drawing.Point(1, 113)
        Me.INDtePensionIBCCPM.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDtePensionIBCCPM, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit111.SetMascara(Me.INDtePensionIBCCPM, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDtePensionIBCCPM, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.IndigoTextEdit1.SetMascara(Me.INDtePensionIBCCPM, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtePensionIBCCPM.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBCCPM.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBCCPM.Name = "INDtePensionIBCCPM"
        Me.INDtePensionIBCCPM.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtePensionIBCCPM.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePensionIBCCPM.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtePensionIBCCPM.Properties.Appearance.Options.UseBackColor = True
        Me.INDtePensionIBCCPM.Properties.Appearance.Options.UseFont = True
        Me.INDtePensionIBCCPM.Properties.Appearance.Options.UseForeColor = True
        Me.INDtePensionIBCCPM.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtePensionIBCCPM.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtePensionIBCCPM.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtePensionIBCCPM.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtePensionIBCCPM.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePensionIBCCPM.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtePensionIBCCPM.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtePensionIBCCPM.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtePensionIBCCPM.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtePensionIBCCPM.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtePensionIBCCPM.Properties.Mask.EditMask = "c2"
        Me.INDtePensionIBCCPM.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtePensionIBCCPM.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtePensionIBCCPM.Properties.ReadOnly = True
        Me.INDtePensionIBCCPM.Size = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBCCPM.StyleController = Me.INDlycResultLiquidation
        Me.INDtePensionIBCCPM.TabIndex = 63
        Me.INDtePensionIBCCPM.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtePensionIBCCPM, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtePensionIBCCPM, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDtePensionIBCCPM, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDtePensionIBCCPM, 0)
        '
        'INDtePensionIBCACCA
        '
        Me.IndigoTextEdit12.SetApplyStyle(Me.INDtePensionIBCACCA, False)
        Me.IndigoTextEdit111.SetApplyStyle(Me.INDtePensionIBCACCA, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtePensionIBCACCA, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtePensionIBCACCA, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtePensionIBCACCA, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtePensionIBCACCA, False)
        Me.IndigoTextEdit111.SetCampoObligatorio(Me.INDtePensionIBCACCA, False)
        Me.IndigoTextEdit12.SetCampoObligatorio(Me.INDtePensionIBCACCA, False)
        Me.INDtePensionIBCACCA.Location = New System.Drawing.Point(1, 143)
        Me.INDtePensionIBCACCA.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IndigoTextEdit12.SetMascara(Me.INDtePensionIBCACCA, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.IndigoTextEdit111.SetMascara(Me.INDtePensionIBCACCA, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDtePensionIBCACCA, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDtePensionIBCACCA, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtePensionIBCACCA.MaximumSize = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBCACCA.MinimumSize = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBCACCA.Name = "INDtePensionIBCACCA"
        Me.INDtePensionIBCACCA.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtePensionIBCACCA.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePensionIBCACCA.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtePensionIBCACCA.Properties.Appearance.Options.UseBackColor = True
        Me.INDtePensionIBCACCA.Properties.Appearance.Options.UseFont = True
        Me.INDtePensionIBCACCA.Properties.Appearance.Options.UseForeColor = True
        Me.INDtePensionIBCACCA.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtePensionIBCACCA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtePensionIBCACCA.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtePensionIBCACCA.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtePensionIBCACCA.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtePensionIBCACCA.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.INDtePensionIBCACCA.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtePensionIBCACCA.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtePensionIBCACCA.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtePensionIBCACCA.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtePensionIBCACCA.Properties.Mask.EditMask = "c2"
        Me.INDtePensionIBCACCA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtePensionIBCACCA.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtePensionIBCACCA.Properties.ReadOnly = True
        Me.INDtePensionIBCACCA.Size = New System.Drawing.Size(125, 26)
        Me.INDtePensionIBCACCA.StyleController = Me.INDlycResultLiquidation
        Me.INDtePensionIBCACCA.TabIndex = 63
        Me.INDtePensionIBCACCA.Tag = 2442
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtePensionIBCACCA, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtePensionIBCACCA, 0)
        Me.IndigoTextEdit111.SetTamañoMinimoString(Me.INDtePensionIBCACCA, 0)
        Me.IndigoTextEdit12.SetTamañoMinimoString(Me.INDtePensionIBCACCA, 0)
        '
        'INDlyGResultLiquidation
        '
        Me.INDlyGResultLiquidation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGResultLiquidation.AppearanceGroup.Options.UseFont = True
        Me.INDlyGResultLiquidation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGResultLiquidation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGResultLiquidation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGResultLiquidation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGResultLiquidation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGResultLiquidation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGResultLiquidation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGResultLiquidation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGResultLiquidation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGResultLiquidation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGResultLiquidation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGResultLiquidation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGResultLiquidation, False)
        Me.INDlyGResultLiquidation.CustomizationFormText = "LayoutControlGroup13"
        Me.INDlyGResultLiquidation.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlyGResultLiquidation.GroupBordersVisible = False
        Me.INDlyGResultLiquidation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemControlPayroll, Me.INDlyItemIBCControl, Me.INDlyCGLiquidacionDetalleDesprendible, Me.INDlyCGDetalleNominaPatronales, Me.INDlyMensajes})
        Me.INDlyGResultLiquidation.Name = "Root"
        Me.INDlyGResultLiquidation.Size = New System.Drawing.Size(2665, 454)
        Me.INDlyGResultLiquidation.TextVisible = False
        Me.INDlyGResultLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemControlPayroll
        '
        Me.INDlyItemControlPayroll.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyItemControlPayroll.AppearanceGroup.Options.UseFont = True
        Me.INDlyItemControlPayroll.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyItemControlPayroll.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemControlPayroll.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyItemControlPayroll.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyItemControlPayroll.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyItemControlPayroll.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyItemControlPayroll.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyItemControlPayroll.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyItemControlPayroll.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyItemControlPayroll.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyItemControlPayroll.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyItemControlPayroll.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyItemControlPayroll, False)
        Me.INDlyItemControlPayroll.CustomizationFormText = "LayoutControlGroup14"
        Me.INDlyItemControlPayroll.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemBasicSalary, Me.INDlyItemDailyBasicSalary, Me.INDlyItemTotalAccrued, Me.INDlyItemPensionDays, Me.INDlyItemSanctionsDays, Me.INDlyItemARLDays, Me.INDlyItemWorkDay, Me.INDlyItemPayrollDays, Me.INDlyItemTotalDeducted, Me.INDlyItemTotalPaid, Me.INDlyItemProvisionDays, Me.INDlyItemInabilitiesDays, Me.INDlyItemVacationDays, Me.INDlyItemLicensesDays, Me.INDlyItemUnpaidLicensesDays, Me.INDlyItemHealthDays})
        Me.INDlyItemControlPayroll.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemControlPayroll.Name = "INDlyItemControlPayroll"
        Me.INDlyItemControlPayroll.Padding = New DevExpress.XtraLayout.Utils.Padding(7, 7, 9, 0)
        Me.INDlyItemControlPayroll.Size = New System.Drawing.Size(487, 434)
        Me.INDlyItemControlPayroll.Text = "Nómina Control"
        '
        'INDlyItemBasicSalary
        '
        Me.INDlyItemBasicSalary.Control = Me.INDteBasicSalary
        Me.INDlyItemBasicSalary.CustomizationFormText = "Salario Básico"
        Me.INDlyItemBasicSalary.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemBasicSalary.MinSize = New System.Drawing.Size(275, 32)
        Me.INDlyItemBasicSalary.Name = "INDlyItemBasicSalary"
        Me.INDlyItemBasicSalary.Size = New System.Drawing.Size(275, 32)
        Me.INDlyItemBasicSalary.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBasicSalary.Text = "Salario Básico"
        Me.INDlyItemBasicSalary.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemDailyBasicSalary
        '
        Me.INDlyItemDailyBasicSalary.Control = Me.INDteDailyBasicSalary
        Me.INDlyItemDailyBasicSalary.CustomizationFormText = "Salario Básico Diario"
        Me.INDlyItemDailyBasicSalary.Location = New System.Drawing.Point(0, 32)
        Me.INDlyItemDailyBasicSalary.Name = "INDlyItemDailyBasicSalary"
        Me.INDlyItemDailyBasicSalary.Size = New System.Drawing.Size(275, 32)
        Me.INDlyItemDailyBasicSalary.Text = "Salario Básico Diario"
        Me.INDlyItemDailyBasicSalary.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemTotalAccrued
        '
        Me.INDlyItemTotalAccrued.Control = Me.INDteTotalAccrued
        Me.INDlyItemTotalAccrued.CustomizationFormText = "Total Devengado"
        Me.INDlyItemTotalAccrued.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemTotalAccrued.MaxSize = New System.Drawing.Size(0, 32)
        Me.INDlyItemTotalAccrued.MinSize = New System.Drawing.Size(152, 32)
        Me.INDlyItemTotalAccrued.Name = "INDlyItemTotalAccrued"
        Me.INDlyItemTotalAccrued.Size = New System.Drawing.Size(275, 32)
        Me.INDlyItemTotalAccrued.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalAccrued.Text = "Total Devengado"
        Me.INDlyItemTotalAccrued.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemPensionDays
        '
        Me.INDlyItemPensionDays.Control = Me.INDtePensionDays
        Me.INDlyItemPensionDays.CustomizationFormText = "Días Pensión"
        Me.INDlyItemPensionDays.Location = New System.Drawing.Point(275, 96)
        Me.INDlyItemPensionDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemPensionDays.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemPensionDays.Name = "INDlyItemPensionDays"
        Me.INDlyItemPensionDays.Size = New System.Drawing.Size(192, 30)
        Me.INDlyItemPensionDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPensionDays.Text = "Días Pensión"
        Me.INDlyItemPensionDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemSanctionsDays
        '
        Me.INDlyItemSanctionsDays.Control = Me.INDteSanctionDays
        Me.INDlyItemSanctionsDays.CustomizationFormText = "Días Sanción"
        Me.INDlyItemSanctionsDays.Location = New System.Drawing.Point(275, 126)
        Me.INDlyItemSanctionsDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemSanctionsDays.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemSanctionsDays.Name = "INDlyItemSanctionsDays"
        Me.INDlyItemSanctionsDays.Size = New System.Drawing.Size(192, 30)
        Me.INDlyItemSanctionsDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSanctionsDays.Text = "Días Sanción"
        Me.INDlyItemSanctionsDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemARLDays
        '
        Me.INDlyItemARLDays.Control = Me.INDteARLDays
        Me.INDlyItemARLDays.CustomizationFormText = "LayoutControlItem11"
        Me.INDlyItemARLDays.Location = New System.Drawing.Point(275, 156)
        Me.INDlyItemARLDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemARLDays.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemARLDays.Name = "INDlyItemARLDays"
        Me.INDlyItemARLDays.Size = New System.Drawing.Size(192, 30)
        Me.INDlyItemARLDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemARLDays.Text = "Días ARL"
        Me.INDlyItemARLDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemWorkDay
        '
        Me.INDlyItemWorkDay.Control = Me.INDteWorkDays
        Me.INDlyItemWorkDay.CustomizationFormText = "Días Trabajados"
        Me.INDlyItemWorkDay.Location = New System.Drawing.Point(275, 32)
        Me.INDlyItemWorkDay.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemWorkDay.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemWorkDay.Name = "INDlyItemWorkDay"
        Me.INDlyItemWorkDay.Size = New System.Drawing.Size(192, 30)
        Me.INDlyItemWorkDay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemWorkDay.Text = "Días Trabajados"
        Me.INDlyItemWorkDay.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemPayrollDays
        '
        Me.INDlyItemPayrollDays.Control = Me.INDtePayrollDays
        Me.INDlyItemPayrollDays.CustomizationFormText = "INDlyItemPayrollDays"
        Me.INDlyItemPayrollDays.Location = New System.Drawing.Point(275, 0)
        Me.INDlyItemPayrollDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemPayrollDays.MinSize = New System.Drawing.Size(192, 30)
        Me.INDlyItemPayrollDays.Name = "INDlyItemPayrollDays"
        Me.INDlyItemPayrollDays.Size = New System.Drawing.Size(192, 32)
        Me.INDlyItemPayrollDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPayrollDays.Text = "Días Nómina"
        Me.INDlyItemPayrollDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemTotalDeducted
        '
        Me.INDlyItemTotalDeducted.Control = Me.INDteTotalDeducted
        Me.INDlyItemTotalDeducted.CustomizationFormText = "Total Deducido"
        Me.INDlyItemTotalDeducted.Location = New System.Drawing.Point(0, 96)
        Me.INDlyItemTotalDeducted.Name = "INDlyItemTotalDeducted"
        Me.INDlyItemTotalDeducted.Size = New System.Drawing.Size(275, 32)
        Me.INDlyItemTotalDeducted.Text = "Total Deducido"
        Me.INDlyItemTotalDeducted.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemTotalPaid
        '
        Me.INDlyItemTotalPaid.Control = Me.INDteTotalPaid
        Me.INDlyItemTotalPaid.CustomizationFormText = "Total Pagado"
        Me.INDlyItemTotalPaid.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemTotalPaid.MaxSize = New System.Drawing.Size(0, 32)
        Me.INDlyItemTotalPaid.MinSize = New System.Drawing.Size(192, 32)
        Me.INDlyItemTotalPaid.Name = "INDlyItemTotalPaid"
        Me.INDlyItemTotalPaid.Size = New System.Drawing.Size(275, 262)
        Me.INDlyItemTotalPaid.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalPaid.Text = "Total Pagado"
        Me.INDlyItemTotalPaid.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemProvisionDays
        '
        Me.INDlyItemProvisionDays.Control = Me.INDteProvisionDays
        Me.INDlyItemProvisionDays.CustomizationFormText = "Días Provisión"
        Me.INDlyItemProvisionDays.Location = New System.Drawing.Point(275, 186)
        Me.INDlyItemProvisionDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemProvisionDays.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemProvisionDays.Name = "INDlyItemProvisionDays"
        Me.INDlyItemProvisionDays.Size = New System.Drawing.Size(192, 30)
        Me.INDlyItemProvisionDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemProvisionDays.Text = "Días Provisión"
        Me.INDlyItemProvisionDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemInabilitiesDays
        '
        Me.INDlyItemInabilitiesDays.Control = Me.INDteInabilitiesDays
        Me.INDlyItemInabilitiesDays.CustomizationFormText = "Días Incapacidad"
        Me.INDlyItemInabilitiesDays.Location = New System.Drawing.Point(275, 216)
        Me.INDlyItemInabilitiesDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemInabilitiesDays.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemInabilitiesDays.Name = "INDlyItemInabilitiesDays"
        Me.INDlyItemInabilitiesDays.Size = New System.Drawing.Size(192, 30)
        Me.INDlyItemInabilitiesDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInabilitiesDays.Text = "Días Incapacidad"
        Me.INDlyItemInabilitiesDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemVacationDays
        '
        Me.INDlyItemVacationDays.Control = Me.INDteVacationDays
        Me.INDlyItemVacationDays.CustomizationFormText = "Días Vacaciones"
        Me.INDlyItemVacationDays.Location = New System.Drawing.Point(275, 246)
        Me.INDlyItemVacationDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemVacationDays.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemVacationDays.Name = "INDlyItemVacationDays"
        Me.INDlyItemVacationDays.Size = New System.Drawing.Size(192, 30)
        Me.INDlyItemVacationDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemVacationDays.Text = "Días Vacaciones"
        Me.INDlyItemVacationDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemLicensesDays
        '
        Me.INDlyItemLicensesDays.Control = Me.INDteLicensesDays
        Me.INDlyItemLicensesDays.CustomizationFormText = "Días Licencias"
        Me.INDlyItemLicensesDays.Location = New System.Drawing.Point(275, 276)
        Me.INDlyItemLicensesDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemLicensesDays.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemLicensesDays.Name = "INDlyItemLicensesDays"
        Me.INDlyItemLicensesDays.Size = New System.Drawing.Size(192, 30)
        Me.INDlyItemLicensesDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLicensesDays.Text = "Días Licencias"
        Me.INDlyItemLicensesDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemUnpaidLicensesDays
        '
        Me.INDlyItemUnpaidLicensesDays.Control = Me.INDteUnpaidLicensesDays
        Me.INDlyItemUnpaidLicensesDays.CustomizationFormText = "Días Lic. No Remuneradas"
        Me.INDlyItemUnpaidLicensesDays.Location = New System.Drawing.Point(275, 306)
        Me.INDlyItemUnpaidLicensesDays.MaxSize = New System.Drawing.Size(0, 32)
        Me.INDlyItemUnpaidLicensesDays.MinSize = New System.Drawing.Size(167, 32)
        Me.INDlyItemUnpaidLicensesDays.Name = "INDlyItemUnpaidLicensesDays"
        Me.INDlyItemUnpaidLicensesDays.Size = New System.Drawing.Size(192, 84)
        Me.INDlyItemUnpaidLicensesDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemUnpaidLicensesDays.Text = "Días Lic. No Remuneradas"
        Me.INDlyItemUnpaidLicensesDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemHealthDays
        '
        Me.INDlyItemHealthDays.Control = Me.INDteHealthDays
        Me.INDlyItemHealthDays.CustomizationFormText = "Días Salud"
        Me.INDlyItemHealthDays.Location = New System.Drawing.Point(275, 62)
        Me.INDlyItemHealthDays.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemHealthDays.MinSize = New System.Drawing.Size(167, 30)
        Me.INDlyItemHealthDays.Name = "INDlyItemHealthDays"
        Me.INDlyItemHealthDays.Size = New System.Drawing.Size(192, 34)
        Me.INDlyItemHealthDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemHealthDays.Text = "Días Salud"
        Me.INDlyItemHealthDays.TextSize = New System.Drawing.Size(167, 17)
        '
        'INDlyItemIBCControl
        '
        Me.INDlyItemIBCControl.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyItemIBCControl.AppearanceGroup.Options.UseFont = True
        Me.INDlyItemIBCControl.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyItemIBCControl.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemIBCControl.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyItemIBCControl.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyItemIBCControl.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyItemIBCControl.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyItemIBCControl.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyItemIBCControl.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyItemIBCControl.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyItemIBCControl.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyItemIBCControl.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyItemIBCControl.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyItemIBCControl, False)
        Me.INDlyItemIBCControl.CustomizationFormText = "IBC - Control Nómina"
        Me.INDlyItemIBCControl.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPeriodIBC, Me.INDlyItemPensionIBC, Me.INDlyItemHealthIBC, Me.INDlyItemARLIBC, Me.INDlyItemRTFIBC, Me.INDlyItemUnemploymentIBC, Me.INDlyItemSENAIBC, Me.INDlyItemCompensationFundIBC, Me.INDlyItemICBFIBC, Me.INDlyItemVacationIBC, Me.INDlyItemIncentivePaymentIBC, Me.INDlyItemPensionIBCCPM, Me.INDlyItemPensionIBCACCAI})
        Me.INDlyItemIBCControl.Location = New System.Drawing.Point(487, 0)
        Me.INDlyItemIBCControl.Name = "INDlyItemIBCControl"
        Me.INDlyItemIBCControl.Padding = New DevExpress.XtraLayout.Utils.Padding(7, 7, 9, 0)
        Me.INDlyItemIBCControl.Size = New System.Drawing.Size(345, 434)
        Me.INDlyItemIBCControl.Text = "IBC - Control Nómina"
        '
        'INDlyItemPeriodIBC
        '
        Me.INDlyItemPeriodIBC.Control = Me.INDtePeriodIBC
        Me.INDlyItemPeriodIBC.CustomizationFormText = "IBC Periodo"
        Me.INDlyItemPeriodIBC.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPeriodIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemPeriodIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemPeriodIBC.Name = "INDlyItemPeriodIBC"
        Me.INDlyItemPeriodIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemPeriodIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPeriodIBC.Text = "IBC Periodo"
        Me.INDlyItemPeriodIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPeriodIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemPeriodIBC.TextToControlDistance = 12
        '
        'INDlyItemPensionIBC
        '
        Me.INDlyItemPensionIBC.Control = Me.INDtePensionIBC
        Me.INDlyItemPensionIBC.CustomizationFormText = "IBC Pensión"
        Me.INDlyItemPensionIBC.Location = New System.Drawing.Point(0, 30)
        Me.INDlyItemPensionIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBC.Name = "INDlyItemPensionIBC"
        Me.INDlyItemPensionIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPensionIBC.Text = "IBC Pensión"
        Me.INDlyItemPensionIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPensionIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemPensionIBC.TextToControlDistance = 12
        '
        'INDlyItemHealthIBC
        '
        Me.INDlyItemHealthIBC.Control = Me.INDteHealthIBC
        Me.INDlyItemHealthIBC.CustomizationFormText = "IBC Salud"
        Me.INDlyItemHealthIBC.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemHealthIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemHealthIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemHealthIBC.Name = "INDlyItemHealthIBC"
        Me.INDlyItemHealthIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemHealthIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemHealthIBC.Text = "IBC Salud"
        Me.INDlyItemHealthIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemHealthIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemHealthIBC.TextToControlDistance = 12
        '
        'INDlyItemARLIBC
        '
        Me.INDlyItemARLIBC.Control = Me.INDteARLIBC
        Me.INDlyItemARLIBC.CustomizationFormText = "IBC ARL"
        Me.INDlyItemARLIBC.Location = New System.Drawing.Point(0, 150)
        Me.INDlyItemARLIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemARLIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemARLIBC.Name = "INDlyItemARLIBC"
        Me.INDlyItemARLIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemARLIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemARLIBC.Text = "IBC ARL"
        Me.INDlyItemARLIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemARLIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemARLIBC.TextToControlDistance = 12
        '
        'INDlyItemRTFIBC
        '
        Me.INDlyItemRTFIBC.Control = Me.INDteRTFIBC
        Me.INDlyItemRTFIBC.CustomizationFormText = "IBC RTF"
        Me.INDlyItemRTFIBC.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemRTFIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemRTFIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemRTFIBC.Name = "INDlyItemRTFIBC"
        Me.INDlyItemRTFIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemRTFIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRTFIBC.Text = "IBC RTF"
        Me.INDlyItemRTFIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRTFIBC.TextSize = New System.Drawing.Size(150, 10)
        Me.INDlyItemRTFIBC.TextToControlDistance = 12
        '
        'INDlyItemUnemploymentIBC
        '
        Me.INDlyItemUnemploymentIBC.Control = Me.INDteUnemploymentIBC
        Me.INDlyItemUnemploymentIBC.CustomizationFormText = "LayoutControlItem14"
        Me.INDlyItemUnemploymentIBC.Location = New System.Drawing.Point(0, 210)
        Me.INDlyItemUnemploymentIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemUnemploymentIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemUnemploymentIBC.Name = "INDlyItemUnemploymentIBC"
        Me.INDlyItemUnemploymentIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemUnemploymentIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemUnemploymentIBC.Text = "IBC Cesantías"
        Me.INDlyItemUnemploymentIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemUnemploymentIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemUnemploymentIBC.TextToControlDistance = 12
        '
        'INDlyItemSENAIBC
        '
        Me.INDlyItemSENAIBC.Control = Me.INDteSENAIBC
        Me.INDlyItemSENAIBC.CustomizationFormText = "IBC Sena"
        Me.INDlyItemSENAIBC.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemSENAIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemSENAIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemSENAIBC.Name = "INDlyItemSENAIBC"
        Me.INDlyItemSENAIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemSENAIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSENAIBC.Text = "IBC Sena"
        Me.INDlyItemSENAIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSENAIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemSENAIBC.TextToControlDistance = 12
        '
        'INDlyItemCompensationFundIBC
        '
        Me.INDlyItemCompensationFundIBC.Control = Me.INDteCompensationFundIBC
        Me.INDlyItemCompensationFundIBC.CustomizationFormText = "IBC Caja"
        Me.INDlyItemCompensationFundIBC.Location = New System.Drawing.Point(0, 270)
        Me.INDlyItemCompensationFundIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemCompensationFundIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemCompensationFundIBC.Name = "INDlyItemCompensationFundIBC"
        Me.INDlyItemCompensationFundIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemCompensationFundIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCompensationFundIBC.Text = "IBC Caja"
        Me.INDlyItemCompensationFundIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCompensationFundIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemCompensationFundIBC.TextToControlDistance = 12
        '
        'INDlyItemICBFIBC
        '
        Me.INDlyItemICBFIBC.Control = Me.INDteICBFIBC
        Me.INDlyItemICBFIBC.CustomizationFormText = "IBC ICBF"
        Me.INDlyItemICBFIBC.Location = New System.Drawing.Point(0, 300)
        Me.INDlyItemICBFIBC.MaxSize = New System.Drawing.Size(0, 30)
        Me.INDlyItemICBFIBC.MinSize = New System.Drawing.Size(288, 30)
        Me.INDlyItemICBFIBC.Name = "INDlyItemICBFIBC"
        Me.INDlyItemICBFIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemICBFIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemICBFIBC.Text = "IBC ICBF"
        Me.INDlyItemICBFIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemICBFIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemICBFIBC.TextToControlDistance = 12
        '
        'INDlyItemVacationIBC
        '
        Me.INDlyItemVacationIBC.Control = Me.INDteVacationIBC
        Me.INDlyItemVacationIBC.CustomizationFormText = "IBC Vacaciones"
        Me.INDlyItemVacationIBC.Location = New System.Drawing.Point(0, 330)
        Me.INDlyItemVacationIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemVacationIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemVacationIBC.Name = "INDlyItemVacationIBC"
        Me.INDlyItemVacationIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemVacationIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemVacationIBC.Text = "IBC Vacaciones"
        Me.INDlyItemVacationIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemVacationIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemVacationIBC.TextToControlDistance = 12
        '
        'INDlyItemIncentivePaymentIBC
        '
        Me.INDlyItemIncentivePaymentIBC.Control = Me.INDteIncentivePaymentIBC
        Me.INDlyItemIncentivePaymentIBC.CustomizationFormText = "IBC Primas"
        Me.INDlyItemIncentivePaymentIBC.Location = New System.Drawing.Point(0, 360)
        Me.INDlyItemIncentivePaymentIBC.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemIncentivePaymentIBC.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemIncentivePaymentIBC.Name = "INDlyItemIncentivePaymentIBC"
        Me.INDlyItemIncentivePaymentIBC.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemIncentivePaymentIBC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemIncentivePaymentIBC.Text = "IBC Primas"
        Me.INDlyItemIncentivePaymentIBC.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemIncentivePaymentIBC.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemIncentivePaymentIBC.TextToControlDistance = 12
        '
        'INDlyItemPensionIBCCPM
        '
        Me.INDlyItemPensionIBCCPM.Control = Me.INDtePensionIBCCPM
        Me.INDlyItemPensionIBCCPM.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemPensionIBCCPM.CustomizationFormText = "IBC Pensión - CPM"
        Me.INDlyItemPensionIBCCPM.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemPensionIBCCPM.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBCCPM.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBCCPM.Name = "INDlyItemPensionIBCCPM"
        Me.INDlyItemPensionIBCCPM.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBCCPM.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPensionIBCCPM.Text = "IBC Pensión - CPM"
        Me.INDlyItemPensionIBCCPM.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPensionIBCCPM.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemPensionIBCCPM.TextToControlDistance = 12
        '
        'INDlyItemPensionIBCACCAI
        '
        Me.INDlyItemPensionIBCACCAI.Control = Me.INDtePensionIBCACCA
        Me.INDlyItemPensionIBCACCAI.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlyItemPensionIBCACCAI.CustomizationFormText = "IBC Pensión - ACCAI"
        Me.INDlyItemPensionIBCACCAI.Location = New System.Drawing.Point(0, 90)
        Me.INDlyItemPensionIBCACCAI.MaxSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBCACCAI.MinSize = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBCACCAI.Name = "INDlyItemPensionIBCACCAI"
        Me.INDlyItemPensionIBCACCAI.Size = New System.Drawing.Size(325, 30)
        Me.INDlyItemPensionIBCACCAI.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPensionIBCACCAI.Text = "IBC Pensión - ACCAI"
        Me.INDlyItemPensionIBCACCAI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPensionIBCACCAI.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyItemPensionIBCACCAI.TextToControlDistance = 12
        '
        'INDlyCGLiquidacionDetalleDesprendible
        '
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceGroup.Options.UseFont = True
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGLiquidacionDetalleDesprendible.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyCGLiquidacionDetalleDesprendible, False)
        Me.INDlyCGLiquidacionDetalleDesprendible.CustomizationFormText = "LayoutControlGroup12"
        Me.INDlyCGLiquidacionDetalleDesprendible.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGcDetailLiquidation})
        Me.INDlyCGLiquidacionDetalleDesprendible.Location = New System.Drawing.Point(832, 0)
        Me.INDlyCGLiquidacionDetalleDesprendible.Name = "LayoutControlGroup12"
        Me.INDlyCGLiquidacionDetalleDesprendible.Padding = New DevExpress.XtraLayout.Utils.Padding(7, 7, 9, 0)
        Me.INDlyCGLiquidacionDetalleDesprendible.Size = New System.Drawing.Size(607, 434)
        Me.INDlyCGLiquidacionDetalleDesprendible.Text = "Detalle Nómina - Desprendible"
        '
        'INDlyGcDetailLiquidation
        '
        Me.INDlyGcDetailLiquidation.Control = Me.INDGcDetailLiquidation
        Me.INDlyGcDetailLiquidation.CustomizationFormText = "LayoutControlItem13"
        Me.INDlyGcDetailLiquidation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGcDetailLiquidation.MinSize = New System.Drawing.Size(587, 1)
        Me.INDlyGcDetailLiquidation.Name = "INDlyGcDetailLiquidation"
        Me.INDlyGcDetailLiquidation.Size = New System.Drawing.Size(587, 390)
        Me.INDlyGcDetailLiquidation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyGcDetailLiquidation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyGcDetailLiquidation.TextVisible = False
        '
        'INDlyCGDetalleNominaPatronales
        '
        Me.INDlyCGDetalleNominaPatronales.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCGDetalleNominaPatronales.AppearanceGroup.Options.UseFont = True
        Me.INDlyCGDetalleNominaPatronales.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCGDetalleNominaPatronales.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGDetalleNominaPatronales.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyCGDetalleNominaPatronales, False)
        Me.INDlyCGDetalleNominaPatronales.CustomizationFormText = "LayoutControlGroup14"
        Me.INDlyCGDetalleNominaPatronales.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGcDetailLiquidationPatrono})
        Me.INDlyCGDetalleNominaPatronales.Location = New System.Drawing.Point(1439, 0)
        Me.INDlyCGDetalleNominaPatronales.Name = "INDlyCGDetalleNominaPatronales"
        Me.INDlyCGDetalleNominaPatronales.Padding = New DevExpress.XtraLayout.Utils.Padding(7, 7, 9, 0)
        Me.INDlyCGDetalleNominaPatronales.Size = New System.Drawing.Size(603, 434)
        Me.INDlyCGDetalleNominaPatronales.Text = "Detalle Nómina - Patronales"
        '
        'INDlyGcDetailLiquidationPatrono
        '
        Me.INDlyGcDetailLiquidationPatrono.Control = Me.INDGcDetailLiquidationPatrono
        Me.INDlyGcDetailLiquidationPatrono.CustomizationFormText = "LayoutControlItem9"
        Me.INDlyGcDetailLiquidationPatrono.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGcDetailLiquidationPatrono.MaxSize = New System.Drawing.Size(583, 5000)
        Me.INDlyGcDetailLiquidationPatrono.MinSize = New System.Drawing.Size(583, 1)
        Me.INDlyGcDetailLiquidationPatrono.Name = "INDlyGcDetailLiquidationPatrono"
        Me.INDlyGcDetailLiquidationPatrono.Size = New System.Drawing.Size(583, 390)
        Me.INDlyGcDetailLiquidationPatrono.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyGcDetailLiquidationPatrono.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyGcDetailLiquidationPatrono.TextVisible = False
        '
        'INDlyMensajes
        '
        Me.INDlyMensajes.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyMensajes.AppearanceGroup.Options.UseFont = True
        Me.INDlyMensajes.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyMensajes.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyMensajes.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyMensajes.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyMensajes.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyMensajes.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyMensajes.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyMensajes.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyMensajes.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyMensajes.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyMensajes.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyMensajes.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyMensajes, False)
        Me.INDlyMensajes.CustomizationFormText = "LayoutControlGroup14"
        Me.INDlyMensajes.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGcMessage})
        Me.INDlyMensajes.Location = New System.Drawing.Point(2042, 0)
        Me.INDlyMensajes.Name = "INDlyMensajes"
        Me.INDlyMensajes.Padding = New DevExpress.XtraLayout.Utils.Padding(7, 7, 9, 0)
        Me.INDlyMensajes.Size = New System.Drawing.Size(607, 434)
        Me.INDlyMensajes.Text = "Mensajes"
        '
        'INDlyGcMessage
        '
        Me.INDlyGcMessage.Control = Me.INDGcMessage
        Me.INDlyGcMessage.CustomizationFormText = "LayoutControlItem4"
        Me.INDlyGcMessage.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGcMessage.Name = "INDlyGcMessage"
        Me.INDlyGcMessage.Size = New System.Drawing.Size(587, 390)
        Me.INDlyGcMessage.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyGcMessage.TextVisible = False
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlycResultLiquidation
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 2)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 391)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyGcGroup
        '
        Me.INDlyGcGroup.Control = Me.INDGcGroup
        Me.INDlyGcGroup.CustomizationFormText = "Liquidacion"
        Me.INDlyGcGroup.Location = New System.Drawing.Point(0, 36)
        Me.INDlyGcGroup.Name = "INDlyGcGroup"
        Me.INDlyGcGroup.Size = New System.Drawing.Size(1308, 26)
        Me.INDlyGcGroup.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyGcGroup.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.INDPnlResultLiquidation
        Me.LayoutControlItem10.CustomizationFormText = "Datos Empleado"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 126)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(87, 300)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(1308, 399)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        Me.LayoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyCtrNavigation
        '
        Me.INDlyCtrNavigation.Control = Me.CtrNavigation1
        Me.INDlyCtrNavigation.CustomizationFormText = "Boton Atras"
        Me.INDlyCtrNavigation.Location = New System.Drawing.Point(0, 62)
        Me.INDlyCtrNavigation.MaxSize = New System.Drawing.Size(0, 74)
        Me.INDlyCtrNavigation.MinSize = New System.Drawing.Size(48, 58)
        Me.INDlyCtrNavigation.Name = "INDlyCtrNavigation"
        Me.INDlyCtrNavigation.Size = New System.Drawing.Size(80, 64)
        Me.INDlyCtrNavigation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyCtrNavigation.Text = "Hola"
        Me.INDlyCtrNavigation.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyCtrNavigation.TextVisible = False
        Me.INDlyCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyLblEmployeeName
        '
        Me.INDlyLblEmployeeName.Control = Me.INDLblEmployeeName
        Me.INDlyLblEmployeeName.CustomizationFormText = "Nombre Empleado"
        Me.INDlyLblEmployeeName.Location = New System.Drawing.Point(80, 62)
        Me.INDlyLblEmployeeName.MaxSize = New System.Drawing.Size(0, 64)
        Me.INDlyLblEmployeeName.MinSize = New System.Drawing.Size(20, 64)
        Me.INDlyLblEmployeeName.Name = "INDlyLblEmployeeName"
        Me.INDlyLblEmployeeName.Size = New System.Drawing.Size(1228, 64)
        Me.INDlyLblEmployeeName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyLblEmployeeName.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyLblEmployeeName.TextVisible = False
        Me.INDlyLblEmployeeName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup6, False)
        Me.LayoutControlGroup6.CustomizationFormText = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(3376, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(304, 563)
        '
        'LayoutControlGroup10
        '
        Me.LayoutControlGroup10.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup10.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup10, False)
        Me.LayoutControlGroup10.CustomizationFormText = "LayoutControlGroup10"
        Me.LayoutControlGroup10.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlGroup10.Name = "LayoutControlGroup10"
        Me.LayoutControlGroup10.Size = New System.Drawing.Size(369, 210)
        '
        'LayoutControlGroup12
        '
        Me.LayoutControlGroup12.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup12.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup12, False)
        Me.LayoutControlGroup12.CustomizationFormText = "LayoutControlGroup12"
        Me.LayoutControlGroup12.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5})
        Me.LayoutControlGroup12.Location = New System.Drawing.Point(789, 0)
        Me.LayoutControlGroup12.Name = "LayoutControlGroup12"
        Me.LayoutControlGroup12.Size = New System.Drawing.Size(718, 572)
        Me.LayoutControlGroup12.Text = "Detalla Nómina - Desprendible"
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDGcDetailLiquidation
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(710, 547)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'INDlyCGMensajes
        '
        Me.INDlyCGMensajes.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCGMensajes.AppearanceGroup.Options.UseFont = True
        Me.INDlyCGMensajes.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyCGMensajes.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyCGMensajes.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGMensajes.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyCGMensajes.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyCGMensajes.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyCGMensajes.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGMensajes.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyCGMensajes.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGMensajes.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyCGMensajes.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyCGMensajes.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyCGMensajes, False)
        Me.INDlyCGMensajes.CustomizationFormText = "Mensajes"
        Me.INDlyCGMensajes.Location = New System.Drawing.Point(710, 0)
        Me.INDlyCGMensajes.Name = "INDlyCGMensajes"
        Me.INDlyCGMensajes.Size = New System.Drawing.Size(728, 555)
        Me.INDlyCGMensajes.Text = "Mensajes"
        '
        'INDlyGroupInability
        '
        Me.INDlyGroupInability.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupInability.AppearanceGroup.Options.UseFont = True
        Me.INDlyGroupInability.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupInability.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupInability.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupInability.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupInability.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGroupInability.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGroupInability.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupInability.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupInability.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupInability.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGroupInability.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupInability.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupInability, False)
        Me.INDlyGroupInability.CustomizationFormText = "INDlyGroupInability"
        Me.INDlyGroupInability.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGroupInability.Name = "INDlyGroupInability"
        Me.INDlyGroupInability.Size = New System.Drawing.Size(50, 25)
        '
        'INDlyGroupNoveltyPending
        '
        Me.INDlyGroupNoveltyPending.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupNoveltyPending.AppearanceGroup.Options.UseFont = True
        Me.INDlyGroupNoveltyPending.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGroupNoveltyPending.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGroupNoveltyPending.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGroupNoveltyPending, False)
        Me.INDlyGroupNoveltyPending.CustomizationFormText = "INDlyGroupNoveltyPending"
        Me.INDlyGroupNoveltyPending.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGroupNoveltyPending.Name = "INDlyGroupNoveltyPending"
        Me.INDlyGroupNoveltyPending.Size = New System.Drawing.Size(50, 25)
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
        Me.LayoutControlGroup4.CustomizationFormText = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(50, 25)
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.AutoHeight = False
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(160, 216)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(2006, 313)
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(124, 13)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.INDtePeriodIBC
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(256, 529)
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(173, 21)
        '
        'SplitterItem1
        '
        Me.SplitterItem1.AllowHotTrack = True
        Me.SplitterItem1.Location = New System.Drawing.Point(0, 266)
        Me.SplitterItem1.Name = "SplitterItem1"
        Me.SplitterItem1.Size = New System.Drawing.Size(361, 10)
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(24, 267)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(357, 22)
        Me.SimpleButton1.TabIndex = 5
        Me.SimpleButton1.Text = "SimpleButton1"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.SimpleButton1
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 266)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(361, 26)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'FrmPayrollLiquidationDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(5.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1348, 713)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.Name = "FrmPayrollLiquidationDetail"
        Me.Opacity = 1.0R
        Me.Tag = "564"
        Me.Text = "Liquidación de Nómina"
        Me.ViewModeEditHold = True
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
        CType(Me.RepositoryItemPopupContainerEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGroupConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrContractType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemUndefined, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepButtonEditVisualizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDetailLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvLiquitadionDetail, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcDetailLiquidationPatrono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvLiquitadionDetailParafiscales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopupContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPopupContainerControl2.ResumeLayout(False)
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl3.ResumeLayout(False)
        CType(Me.INDteResultFormulaPatrono.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyfUsedFormula2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyfReplaceFormula2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyfResultFormula2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFormulasConceptos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImageCollection1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCGLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyBteIdNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteIdNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDPnlResultLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPnlResultLiquidation.ResumeLayout(False)
        CType(Me.INDlycResultLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycResultLiquidation.ResumeLayout(False)
        CType(Me.INDteIncentivePaymentIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteVacationIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteICBFIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteCompensationFundIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteSENAIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteBasicSalary.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteUnemploymentIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteRTFIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteDailyBasicSalary.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteARLIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteTotalAccrued.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteHealthIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteTotalDeducted.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtePensionIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteTotalPaid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtePeriodIBC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtePensionIBCCPM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtePensionIBCACCA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGResultLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemControlPayroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBasicSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDailyBasicSalary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalAccrued, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPensionDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSanctionsDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemARLDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemWorkDay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPayrollDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalDeducted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalPaid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemProvisionDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInabilitiesDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemVacationDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLicensesDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUnpaidLicensesDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHealthDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIBCControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPeriodIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPensionIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHealthIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemARLIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRTFIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUnemploymentIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSENAIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCompensationFundIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemICBFIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemVacationIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemIncentivePaymentIBC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPensionIBCCPM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPensionIBCACCAI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCGLiquidacionDetalleDesprendible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGcDetailLiquidation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCGDetalleNominaPatronales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGcDetailLiquidationPatrono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyMensajes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGcMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGcGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtrNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyLblEmployeeName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCGMensajes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupInability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGroupNoveltyPending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitterItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit111, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyGroupConcept As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyGrContractType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLyItemUndefined As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGroupInability As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDlyGroupNoveltyPending As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcDetailLiquidation As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvLiquitadionDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcGroup As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColGroupCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColGroupName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColGroupLastDateLiquidation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColGroupNextDateLiquidation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDbteIdNumber As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDteProvisionDays As System.Windows.Forms.TextBox
    Friend WithEvents INDtePayrollDays As System.Windows.Forms.TextBox
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtePensionDays As System.Windows.Forms.TextBox
    Friend WithEvents INDteARLDays As System.Windows.Forms.TextBox
    Friend WithEvents INDteVacationDays As System.Windows.Forms.TextBox
    Friend WithEvents INDteSanctionDays As System.Windows.Forms.TextBox
    Friend WithEvents INDteUnpaidLicensesDays As System.Windows.Forms.TextBox
    Friend WithEvents INDteInabilitiesDays As System.Windows.Forms.TextBox
    Friend WithEvents INDGcDetailLiquidationPatrono As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvLiquitadionDetailParafiscales As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColConceptCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColConceptName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAccrued As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDeducted As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColParafiscalConceptCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColParafiscalConceptName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColParafiscalConceptValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents ChkConcept As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents Acciones As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents AccionesParafiscales As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDPopupContainerControl1 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDteUsedFormula As System.Windows.Forms.TextBox
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyfUsedFormula1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDteReplaceFormula As System.Windows.Forms.TextBox
    Friend WithEvents INDlyfReplaceFormula1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPopupContainerControl2 As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDteReplaceFormulaPatrono As System.Windows.Forms.TextBox
    Friend WithEvents INDteUsedFormulaPatrono As System.Windows.Forms.TextBox
    Friend WithEvents LayoutControlGroup9 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup11 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyfUsedFormula2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyfReplaceFormula2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup10 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents RepositoryItemPopupContainerEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDteBasicSalary As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteDailyBasicSalary As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteTotalAccrued As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteTotalDeducted As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteTotalPaid As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteResultFormula As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyfResultFormula1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDteResultFormulaPatrono As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyfResultFormula2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtePeriodIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtePensionIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteHealthIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteARLIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteRTFIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteUnemploymentIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteSENAIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteCompensationFundIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDteICBFIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyCGLiquidacion As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGcGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyBteIdNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDPnlResultLiquidation As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlycResultLiquidation As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlyGResultLiquidation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemControlPayroll As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemBasicSalary As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDailyBasicSalary As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTotalAccrued As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyItemTotalDeducted As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemTotalPaid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPensionDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSanctionsDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemARLDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemProvisionDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemInabilitiesDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemVacationDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPayrollDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemUnpaidLicensesDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDteWorkDays As System.Windows.Forms.TextBox
    Friend WithEvents INDlyItemWorkDay As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDteLicensesDays As System.Windows.Forms.TextBox
    Friend WithEvents INDlyItemLicensesDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDteHealthDays As System.Windows.Forms.TextBox
    Friend WithEvents INDlyItemHealthDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup12 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyCGLiquidacionDetalleDesprendible As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGcDetailLiquidation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyCGDetalleNominaPatronales As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGcDetailLiquidationPatrono As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyMensajes As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyCGMensajes As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcMessage As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvFormulasConceptos As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColErrorType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColErrorDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyGcMessage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPeriodIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPensionIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemHealthIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemRTFIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemARLIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSENAIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemUnemploymentIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCompensationFundIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemICBFIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigation1 As Presentation.Controls.CtrNavigation
    Friend WithEvents INDlyCtrNavigation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLblEmployeeName As System.Windows.Forms.Label
    Friend WithEvents INDlyLblEmployeeName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemIBCControl As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDteVacationIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemVacationIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDteIncentivePaymentIBC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemIncentivePaymentIBC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColGroupLiquidationView As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents IndColVisualizarLiquidacion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepButtonEditVisualizar As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents RepositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImageCollection1 As DevExpress.Utils.ImageCollection
    Friend WithEvents INDBtnLiquidator As System.Windows.Forms.Button
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents SplitterItem1 As DevExpress.XtraLayout.SplitterItem
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents IndigoTextEdit11 As IndigoTextEdit
    Friend WithEvents INDtePensionIBCCPM As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemPensionIBCCPM As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit111 As IndigoTextEdit
    Friend WithEvents IndigoTextEdit12 As IndigoTextEdit
    Friend WithEvents INDtePensionIBCACCA As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemPensionIBCACCAI As DevExpress.XtraLayout.LayoutControlItem
End Class
