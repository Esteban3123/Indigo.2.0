Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLiquidator
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
        Me.INDlyLiquidator = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbPrevoiusRetentions = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbEditExemptIncomeAndDeductions = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbEditExemptIncome = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbInfoExemptIncomeAndDeductionsControl = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSbInfoExemptIncomeControl = New DevExpress.XtraEditors.SimpleButton()
        Me.INDpopupTotalValues = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDlyPopupControl = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtPopUpNoConstitutivosRenta = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtMenosRents = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtBasePopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtSubTotalBPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtSubTotalAPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtDeductionsPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtRentExentPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPaymentMonthPopup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtSubTotal3Popup = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtMaxDeductionsAndRentExentsPopup = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPaymentMonthPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRentExentPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemBasePopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemMenosRents = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INCLciPopUpNoConstitutivosRenta = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSubTotalAPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDeductionsPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSubTotalBPopup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSubTotal3Popup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSubTotal4Popup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDspAccountContributions = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspVoluntaryContributions = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSPPaymentHealthObligatory = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspSolidarityPension = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspPensionContribution = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspRiskWork = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspDependent = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtFeeCommissionServiceData = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtTotalIncomeExent = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtTotalDeductions = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtTotalNoConstitutive = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtTotalIncomeMonthlyData = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPreparedHealth = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtInterests = New DevExpress.XtraEditors.SpinEdit()
        Me.INDspPensionByIndividualSavingsRegime = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtExemptIncomeControl = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxExemptIncome25Percent = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtExemptIncomeAndDeductions = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtControlExemptIncomeAndDeductions = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtTaxBase = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtSubtotal = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtAccumulatedIncome = New DevExpress.XtraEditors.TextEdit()
        Me.INDSbInfoIncomeByMonth = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtRetArt383 = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPreviousDeductions = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtWithholdingApplied = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPaymentsMonthly = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemTotalIncomeMonthlyData = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAccumulatedIncome = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlygRentsExempts = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemTotalIncomeExent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPensionByIndividualSavingsRegime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygDeductions = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPreparedHealth = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTotalDeductionsPartial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInterests = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciVoluntaryContributions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlcgSubtotal = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciExemptIncomeControl = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciExemptIncome25Percent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciExemptIncomeAndDeductions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciTaxBase = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciControlExemptIncomeAndDeductions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciSubtotal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgAppyRetentios = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciRetArt383 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPreviousDeductions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciWithholdingApplied = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDbtnAddRetention = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyLiquidator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyLiquidator.SuspendLayout()
        CType(Me.INDpopupTotalValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpopupTotalValues.SuspendLayout()
        CType(Me.INDlyPopupControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPopupControl.SuspendLayout()
        CType(Me.INDTxtPopUpNoConstitutivosRenta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtMenosRents.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtBasePopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSubTotalBPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSubTotalAPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDeductionsPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtRentExentPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPaymentMonthPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSubTotal3Popup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPaymentMonthPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRentExentPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemBasePopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemMenosRents, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INCLciPopUpNoConstitutivosRenta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSubTotalAPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDeductionsPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSubTotalBPopup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSubTotal3Popup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSubTotal4Popup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspAccountContributions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspVoluntaryContributions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSPPaymentHealthObligatory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspSolidarityPension.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspPensionContribution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspRiskWork.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspDependent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtFeeCommissionServiceData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTotalIncomeExent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTotalDeductions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTotalNoConstitutive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTotalIncomeMonthlyData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPreparedHealth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtInterests.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDspPensionByIndividualSavingsRegime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtExemptIncomeControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxExemptIncome25Percent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtExemptIncomeAndDeductions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtControlExemptIncomeAndDeductions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtTaxBase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSubtotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtAccumulatedIncome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtRetArt383.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPreviousDeductions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtWithholdingApplied.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPaymentsMonthly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalIncomeMonthlyData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAccumulatedIncome, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygRentsExempts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalIncomeExent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPensionByIndividualSavingsRegime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygDeductions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPreparedHealth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTotalDeductionsPartial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInterests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciVoluntaryContributions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgSubtotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciExemptIncomeControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciExemptIncome25Percent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciExemptIncomeAndDeductions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciTaxBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciControlExemptIncomeAndDeductions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciSubtotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgAppyRetentios, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRetArt383, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPreviousDeductions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciWithholdingApplied, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyLiquidator)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(577, 615)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(577, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(577, 130)
        '
        'INDlyLiquidator
        '
        Me.INDlyLiquidator.Controls.Add(Me.INDSbPrevoiusRetentions)
        Me.INDlyLiquidator.Controls.Add(Me.INDSbEditExemptIncomeAndDeductions)
        Me.INDlyLiquidator.Controls.Add(Me.INDSbEditExemptIncome)
        Me.INDlyLiquidator.Controls.Add(Me.INDSbInfoExemptIncomeAndDeductionsControl)
        Me.INDlyLiquidator.Controls.Add(Me.INDSbInfoExemptIncomeControl)
        Me.INDlyLiquidator.Controls.Add(Me.INDpopupTotalValues)
        Me.INDlyLiquidator.Controls.Add(Me.INDspAccountContributions)
        Me.INDlyLiquidator.Controls.Add(Me.INDspVoluntaryContributions)
        Me.INDlyLiquidator.Controls.Add(Me.INDSPPaymentHealthObligatory)
        Me.INDlyLiquidator.Controls.Add(Me.INDspSolidarityPension)
        Me.INDlyLiquidator.Controls.Add(Me.INDspPensionContribution)
        Me.INDlyLiquidator.Controls.Add(Me.INDspRiskWork)
        Me.INDlyLiquidator.Controls.Add(Me.INDspDependent)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtFeeCommissionServiceData)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtTotalIncomeExent)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtTotalDeductions)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtTotalNoConstitutive)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtTotalIncomeMonthlyData)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtPreparedHealth)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtInterests)
        Me.INDlyLiquidator.Controls.Add(Me.INDspPensionByIndividualSavingsRegime)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtExemptIncomeControl)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxExemptIncome25Percent)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtExemptIncomeAndDeductions)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtControlExemptIncomeAndDeductions)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtTaxBase)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtSubtotal)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtAccumulatedIncome)
        Me.INDlyLiquidator.Controls.Add(Me.INDSbInfoIncomeByMonth)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtRetArt383)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtPreviousDeductions)
        Me.INDlyLiquidator.Controls.Add(Me.INDtxtWithholdingApplied)
        Me.INDlyLiquidator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyLiquidator.Location = New System.Drawing.Point(2, 8)
        Me.INDlyLiquidator.Name = "INDlyLiquidator"
        Me.INDlyLiquidator.Root = Me.LayoutControlGroup1
        Me.INDlyLiquidator.Size = New System.Drawing.Size(573, 565)
        Me.INDlyLiquidator.TabIndex = 1
        Me.INDlyLiquidator.Text = "LayoutControl1"
        '
        'INDSbPrevoiusRetentions
        '
        Me.INDSbPrevoiusRetentions.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.MoreInfo_24x24_blue
        Me.INDSbPrevoiusRetentions.Location = New System.Drawing.Point(426, 409)
        Me.INDSbPrevoiusRetentions.MaximumSize = New System.Drawing.Size(30, 28)
        Me.INDSbPrevoiusRetentions.MinimumSize = New System.Drawing.Size(30, 28)
        Me.INDSbPrevoiusRetentions.Name = "INDSbPrevoiusRetentions"
        Me.INDSbPrevoiusRetentions.Size = New System.Drawing.Size(30, 28)
        Me.INDSbPrevoiusRetentions.StyleController = Me.INDlyLiquidator
        Me.INDSbPrevoiusRetentions.TabIndex = 49
        Me.INDSbPrevoiusRetentions.Text = "SimpleButton1"
        Me.INDSbPrevoiusRetentions.ToolTip = "Ver detalle"
        '
        'INDSbEditExemptIncomeAndDeductions
        '
        Me.INDSbEditExemptIncomeAndDeductions.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.IconoEditar_
        Me.INDSbEditExemptIncomeAndDeductions.Location = New System.Drawing.Point(414, 176)
        Me.INDSbEditExemptIncomeAndDeductions.MaximumSize = New System.Drawing.Size(30, 28)
        Me.INDSbEditExemptIncomeAndDeductions.MinimumSize = New System.Drawing.Size(30, 28)
        Me.INDSbEditExemptIncomeAndDeductions.Name = "INDSbEditExemptIncomeAndDeductions"
        Me.INDSbEditExemptIncomeAndDeductions.Size = New System.Drawing.Size(30, 28)
        Me.INDSbEditExemptIncomeAndDeductions.StyleController = Me.INDlyLiquidator
        Me.INDSbEditExemptIncomeAndDeductions.TabIndex = 42
        Me.INDSbEditExemptIncomeAndDeductions.ToolTip = "Editar total de rentas exentas y deducciones"
        '
        'INDSbEditExemptIncome
        '
        Me.INDSbEditExemptIncome.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.IconoEditar_
        Me.INDSbEditExemptIncome.Location = New System.Drawing.Point(414, 48)
        Me.INDSbEditExemptIncome.MaximumSize = New System.Drawing.Size(30, 28)
        Me.INDSbEditExemptIncome.MinimumSize = New System.Drawing.Size(30, 28)
        Me.INDSbEditExemptIncome.Name = "INDSbEditExemptIncome"
        Me.INDSbEditExemptIncome.Size = New System.Drawing.Size(30, 28)
        Me.INDSbEditExemptIncome.StyleController = Me.INDlyLiquidator
        Me.INDSbEditExemptIncome.TabIndex = 40
        Me.INDSbEditExemptIncome.ToolTip = "Editar renta exenta 25%"
        '
        'INDSbInfoExemptIncomeAndDeductionsControl
        '
        Me.INDSbInfoExemptIncomeAndDeductionsControl.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.MoreInfo_24x24_blue
        Me.INDSbInfoExemptIncomeAndDeductionsControl.Location = New System.Drawing.Point(414, 112)
        Me.INDSbInfoExemptIncomeAndDeductionsControl.MaximumSize = New System.Drawing.Size(30, 28)
        Me.INDSbInfoExemptIncomeAndDeductionsControl.MinimumSize = New System.Drawing.Size(30, 28)
        Me.INDSbInfoExemptIncomeAndDeductionsControl.Name = "INDSbInfoExemptIncomeAndDeductionsControl"
        Me.INDSbInfoExemptIncomeAndDeductionsControl.Size = New System.Drawing.Size(30, 28)
        Me.INDSbInfoExemptIncomeAndDeductionsControl.StyleController = Me.INDlyLiquidator
        Me.INDSbInfoExemptIncomeAndDeductionsControl.TabIndex = 39
        Me.INDSbInfoExemptIncomeAndDeductionsControl.Text = "SimpleButton1"
        Me.INDSbInfoExemptIncomeAndDeductionsControl.ToolTip = "Ver detalle"
        '
        'INDSbInfoExemptIncomeControl
        '
        Me.INDSbInfoExemptIncomeControl.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.MoreInfo_24x24_blue
        Me.INDSbInfoExemptIncomeControl.Location = New System.Drawing.Point(414, -16)
        Me.INDSbInfoExemptIncomeControl.MaximumSize = New System.Drawing.Size(30, 28)
        Me.INDSbInfoExemptIncomeControl.MinimumSize = New System.Drawing.Size(30, 28)
        Me.INDSbInfoExemptIncomeControl.Name = "INDSbInfoExemptIncomeControl"
        Me.INDSbInfoExemptIncomeControl.Size = New System.Drawing.Size(30, 28)
        Me.INDSbInfoExemptIncomeControl.StyleController = Me.INDlyLiquidator
        Me.INDSbInfoExemptIncomeControl.TabIndex = 38
        Me.INDSbInfoExemptIncomeControl.ToolTip = "Ver detalle"
        '
        'INDpopupTotalValues
        '
        Me.INDpopupTotalValues.Controls.Add(Me.INDlyPopupControl)
        Me.INDpopupTotalValues.Location = New System.Drawing.Point(15, 494)
        Me.INDpopupTotalValues.Name = "INDpopupTotalValues"
        Me.INDpopupTotalValues.Size = New System.Drawing.Size(299, 522)
        Me.INDpopupTotalValues.TabIndex = 27
        '
        'INDlyPopupControl
        '
        Me.INDlyPopupControl.Controls.Add(Me.INDTxtPopUpNoConstitutivosRenta)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtMenosRents)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtBasePopup)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtSubTotalBPopup)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtSubTotalAPopup)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtDeductionsPopup)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtRentExentPopup)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtPaymentMonthPopup)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtSubTotal3Popup)
        Me.INDlyPopupControl.Controls.Add(Me.INDtxtMaxDeductionsAndRentExentsPopup)
        Me.INDlyPopupControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyPopupControl.Location = New System.Drawing.Point(0, 0)
        Me.INDlyPopupControl.Name = "INDlyPopupControl"
        Me.INDlyPopupControl.Root = Me.LayoutControlGroup2
        Me.INDlyPopupControl.Size = New System.Drawing.Size(299, 522)
        Me.INDlyPopupControl.TabIndex = 0
        Me.INDlyPopupControl.Text = "LayoutControl1"
        '
        'INDTxtPopUpNoConstitutivosRenta
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPopUpNoConstitutivosRenta, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPopUpNoConstitutivosRenta, False)
        Me.INDTxtPopUpNoConstitutivosRenta.Location = New System.Drawing.Point(12, 88)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPopUpNoConstitutivosRenta, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtPopUpNoConstitutivosRenta.Name = "INDTxtPopUpNoConstitutivosRenta"
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Mask.EditMask = "c0"
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.NullText = "$0"
        Me.INDTxtPopUpNoConstitutivosRenta.Properties.ReadOnly = True
        Me.INDTxtPopUpNoConstitutivosRenta.Size = New System.Drawing.Size(275, 28)
        Me.INDTxtPopUpNoConstitutivosRenta.StyleController = Me.INDlyPopupControl
        Me.INDTxtPopUpNoConstitutivosRenta.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPopUpNoConstitutivosRenta, 0)
        '
        'INDtxtMenosRents
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtMenosRents, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtMenosRents, False)
        Me.INDtxtMenosRents.Location = New System.Drawing.Point(12, 338)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtMenosRents, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtMenosRents.Name = "INDtxtMenosRents"
        Me.INDtxtMenosRents.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtMenosRents.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtMenosRents.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtMenosRents.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtMenosRents.Properties.Appearance.Options.UseFont = True
        Me.INDtxtMenosRents.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtMenosRents.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtMenosRents.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtMenosRents.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtMenosRents.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtMenosRents.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtMenosRents.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtMenosRents.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtMenosRents.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtMenosRents.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtMenosRents.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtMenosRents.Properties.Mask.EditMask = "c0"
        Me.INDtxtMenosRents.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtMenosRents.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtMenosRents.Properties.NullText = "$0"
        Me.INDtxtMenosRents.Properties.ReadOnly = True
        Me.INDtxtMenosRents.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtMenosRents.StyleController = Me.INDlyPopupControl
        Me.INDtxtMenosRents.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtMenosRents, 0)
        '
        'INDtxtBasePopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtBasePopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtBasePopup, False)
        Me.INDtxtBasePopup.Location = New System.Drawing.Point(12, 488)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtBasePopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtBasePopup.Name = "INDtxtBasePopup"
        Me.INDtxtBasePopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtBasePopup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtBasePopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtBasePopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtBasePopup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtBasePopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtBasePopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtBasePopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtBasePopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtBasePopup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtBasePopup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtBasePopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtBasePopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtBasePopup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtBasePopup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtBasePopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtBasePopup.Properties.Mask.EditMask = "c0"
        Me.INDtxtBasePopup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtBasePopup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtBasePopup.Properties.NullText = "$0"
        Me.INDtxtBasePopup.Properties.ReadOnly = True
        Me.INDtxtBasePopup.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtBasePopup.StyleController = Me.INDlyPopupControl
        Me.INDtxtBasePopup.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtBasePopup, 0)
        '
        'INDtxtSubTotalBPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSubTotalBPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSubTotalBPopup, False)
        Me.INDtxtSubTotalBPopup.Location = New System.Drawing.Point(12, 288)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSubTotalBPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtSubTotalBPopup.Name = "INDtxtSubTotalBPopup"
        Me.INDtxtSubTotalBPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtSubTotalBPopup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotalBPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtSubTotalBPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSubTotalBPopup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSubTotalBPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtSubTotalBPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSubTotalBPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSubTotalBPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtSubTotalBPopup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtSubTotalBPopup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotalBPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtSubTotalBPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSubTotalBPopup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtSubTotalBPopup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSubTotalBPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtSubTotalBPopup.Properties.Mask.EditMask = "c0"
        Me.INDtxtSubTotalBPopup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSubTotalBPopup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSubTotalBPopup.Properties.NullText = "$0"
        Me.INDtxtSubTotalBPopup.Properties.ReadOnly = True
        Me.INDtxtSubTotalBPopup.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtSubTotalBPopup.StyleController = Me.INDlyPopupControl
        Me.INDtxtSubTotalBPopup.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSubTotalBPopup, 0)
        '
        'INDtxtSubTotalAPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSubTotalAPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSubTotalAPopup, False)
        Me.INDtxtSubTotalAPopup.Location = New System.Drawing.Point(12, 138)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSubTotalAPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtSubTotalAPopup.Name = "INDtxtSubTotalAPopup"
        Me.INDtxtSubTotalAPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtSubTotalAPopup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotalAPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtSubTotalAPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSubTotalAPopup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSubTotalAPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtSubTotalAPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSubTotalAPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSubTotalAPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtSubTotalAPopup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtSubTotalAPopup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotalAPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtSubTotalAPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSubTotalAPopup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtSubTotalAPopup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSubTotalAPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtSubTotalAPopup.Properties.Mask.EditMask = "c0"
        Me.INDtxtSubTotalAPopup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSubTotalAPopup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSubTotalAPopup.Properties.NullText = "$0"
        Me.INDtxtSubTotalAPopup.Properties.ReadOnly = True
        Me.INDtxtSubTotalAPopup.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtSubTotalAPopup.StyleController = Me.INDlyPopupControl
        Me.INDtxtSubTotalAPopup.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSubTotalAPopup, 0)
        '
        'INDtxtDeductionsPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDeductionsPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDeductionsPopup, False)
        Me.INDtxtDeductionsPopup.Location = New System.Drawing.Point(12, 188)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDeductionsPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDeductionsPopup.Name = "INDtxtDeductionsPopup"
        Me.INDtxtDeductionsPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtDeductionsPopup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDeductionsPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtDeductionsPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDeductionsPopup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDeductionsPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtDeductionsPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtDeductionsPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtDeductionsPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtDeductionsPopup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtDeductionsPopup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDeductionsPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtDeductionsPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDeductionsPopup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDeductionsPopup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDeductionsPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtDeductionsPopup.Properties.Mask.EditMask = "c0"
        Me.INDtxtDeductionsPopup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtDeductionsPopup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtDeductionsPopup.Properties.NullText = "$0"
        Me.INDtxtDeductionsPopup.Properties.ReadOnly = True
        Me.INDtxtDeductionsPopup.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtDeductionsPopup.StyleController = Me.INDlyPopupControl
        Me.INDtxtDeductionsPopup.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDeductionsPopup, 0)
        '
        'INDtxtRentExentPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtRentExentPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtRentExentPopup, False)
        Me.INDtxtRentExentPopup.Location = New System.Drawing.Point(12, 238)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtRentExentPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtRentExentPopup.Name = "INDtxtRentExentPopup"
        Me.INDtxtRentExentPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtRentExentPopup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRentExentPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtRentExentPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtRentExentPopup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtRentExentPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtRentExentPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtRentExentPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtRentExentPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtRentExentPopup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtRentExentPopup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRentExentPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtRentExentPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtRentExentPopup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtRentExentPopup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtRentExentPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtRentExentPopup.Properties.Mask.EditMask = "c0"
        Me.INDtxtRentExentPopup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtRentExentPopup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtRentExentPopup.Properties.NullText = "$0"
        Me.INDtxtRentExentPopup.Properties.ReadOnly = True
        Me.INDtxtRentExentPopup.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtRentExentPopup.StyleController = Me.INDlyPopupControl
        Me.INDtxtRentExentPopup.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtRentExentPopup, 0)
        '
        'INDtxtPaymentMonthPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPaymentMonthPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPaymentMonthPopup, False)
        Me.INDtxtPaymentMonthPopup.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPaymentMonthPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPaymentMonthPopup.Name = "INDtxtPaymentMonthPopup"
        Me.INDtxtPaymentMonthPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtPaymentMonthPopup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPaymentMonthPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtPaymentMonthPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPaymentMonthPopup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPaymentMonthPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtPaymentMonthPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtPaymentMonthPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtPaymentMonthPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtPaymentMonthPopup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtPaymentMonthPopup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPaymentMonthPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtPaymentMonthPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPaymentMonthPopup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtPaymentMonthPopup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPaymentMonthPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtPaymentMonthPopup.Properties.Mask.EditMask = "c0"
        Me.INDtxtPaymentMonthPopup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtPaymentMonthPopup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtPaymentMonthPopup.Properties.NullText = "$0"
        Me.INDtxtPaymentMonthPopup.Properties.ReadOnly = True
        Me.INDtxtPaymentMonthPopup.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtPaymentMonthPopup.StyleController = Me.INDlyPopupControl
        Me.INDtxtPaymentMonthPopup.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPaymentMonthPopup, 0)
        '
        'INDtxtSubTotal3Popup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSubTotal3Popup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSubTotal3Popup, False)
        Me.INDtxtSubTotal3Popup.EnterMoveNextControl = True
        Me.INDtxtSubTotal3Popup.Location = New System.Drawing.Point(12, 388)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSubTotal3Popup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtSubTotal3Popup.Name = "INDtxtSubTotal3Popup"
        Me.INDtxtSubTotal3Popup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtSubTotal3Popup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotal3Popup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtSubTotal3Popup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSubTotal3Popup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSubTotal3Popup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtSubTotal3Popup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSubTotal3Popup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSubTotal3Popup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtSubTotal3Popup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtSubTotal3Popup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubTotal3Popup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtSubTotal3Popup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSubTotal3Popup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtSubTotal3Popup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSubTotal3Popup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtSubTotal3Popup.Properties.Mask.EditMask = "c0"
        Me.INDtxtSubTotal3Popup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSubTotal3Popup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSubTotal3Popup.Properties.NullText = "$0"
        Me.INDtxtSubTotal3Popup.Properties.ReadOnly = True
        Me.INDtxtSubTotal3Popup.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtSubTotal3Popup.StyleController = Me.INDlyPopupControl
        Me.INDtxtSubTotal3Popup.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSubTotal3Popup, 0)
        '
        'INDtxtMaxDeductionsAndRentExentsPopup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtMaxDeductionsAndRentExentsPopup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtMaxDeductionsAndRentExentsPopup, False)
        Me.INDtxtMaxDeductionsAndRentExentsPopup.EnterMoveNextControl = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Location = New System.Drawing.Point(12, 438)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtMaxDeductionsAndRentExentsPopup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Name = "INDtxtMaxDeductionsAndRentExentsPopup"
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Appearance.Options.UseFont = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Mask.EditMask = "c0"
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.NullText = "$0"
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties.ReadOnly = True
        Me.INDtxtMaxDeductionsAndRentExentsPopup.Size = New System.Drawing.Size(275, 28)
        Me.INDtxtMaxDeductionsAndRentExentsPopup.StyleController = Me.INDlyPopupControl
        Me.INDtxtMaxDeductionsAndRentExentsPopup.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtMaxDeductionsAndRentExentsPopup, 0)
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
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPaymentMonthPopup, Me.INDlyItemRentExentPopup, Me.INDlyItemBasePopup, Me.INDlyItemMenosRents, Me.INCLciPopUpNoConstitutivosRenta, Me.INDlyItemSubTotalAPopup, Me.INDlyItemDeductionsPopup, Me.INDlyItemSubTotalBPopup, Me.INDlyItemSubTotal3Popup, Me.INDlyItemSubTotal4Popup})
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(299, 522)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlyItemPaymentMonthPopup
        '
        Me.INDlyItemPaymentMonthPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemPaymentMonthPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemPaymentMonthPopup.Control = Me.INDtxtPaymentMonthPopup
        Me.INDlyItemPaymentMonthPopup.CustomizationFormText = "Total Pagos Mes"
        Me.INDlyItemPaymentMonthPopup.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPaymentMonthPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemPaymentMonthPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemPaymentMonthPopup.Name = "INDlyItemPaymentMonthPopup"
        Me.INDlyItemPaymentMonthPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemPaymentMonthPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPaymentMonthPopup.Text = "Total Pagos Mes"
        Me.INDlyItemPaymentMonthPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPaymentMonthPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPaymentMonthPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPaymentMonthPopup.TextToControlDistance = 5
        '
        'INDlyItemRentExentPopup
        '
        Me.INDlyItemRentExentPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemRentExentPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemRentExentPopup.Control = Me.INDtxtRentExentPopup
        Me.INDlyItemRentExentPopup.CustomizationFormText = "Rentas Exentas"
        Me.INDlyItemRentExentPopup.Location = New System.Drawing.Point(0, 200)
        Me.INDlyItemRentExentPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemRentExentPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemRentExentPopup.Name = "INDlyItemRentExentPopup"
        Me.INDlyItemRentExentPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemRentExentPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRentExentPopup.Text = "Rentas Exentas"
        Me.INDlyItemRentExentPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRentExentPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemRentExentPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRentExentPopup.TextToControlDistance = 5
        '
        'INDlyItemBasePopup
        '
        Me.INDlyItemBasePopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemBasePopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemBasePopup.Control = Me.INDtxtBasePopup
        Me.INDlyItemBasePopup.CustomizationFormText = "Base Gravable"
        Me.INDlyItemBasePopup.Location = New System.Drawing.Point(0, 450)
        Me.INDlyItemBasePopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemBasePopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemBasePopup.Name = "INDlyItemBasePopup"
        Me.INDlyItemBasePopup.Size = New System.Drawing.Size(279, 52)
        Me.INDlyItemBasePopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemBasePopup.Text = "Base Gravable"
        Me.INDlyItemBasePopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemBasePopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemBasePopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemBasePopup.TextToControlDistance = 5
        '
        'INDlyItemMenosRents
        '
        Me.INDlyItemMenosRents.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemMenosRents.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemMenosRents.Control = Me.INDtxtMenosRents
        Me.INDlyItemMenosRents.CustomizationFormText = "Renta Exenta 25%"
        Me.INDlyItemMenosRents.Location = New System.Drawing.Point(0, 300)
        Me.INDlyItemMenosRents.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemMenosRents.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemMenosRents.Name = "INDlyItemMenosRents"
        Me.INDlyItemMenosRents.OptionsToolTip.ToolTip = "25% del SubTotal 2 con Tope de 790 UVT"
        Me.INDlyItemMenosRents.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemMenosRents.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemMenosRents.Text = "Renta Exenta 25%"
        Me.INDlyItemMenosRents.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemMenosRents.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemMenosRents.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemMenosRents.TextToControlDistance = 5
        '
        'INCLciPopUpNoConstitutivosRenta
        '
        Me.INCLciPopUpNoConstitutivosRenta.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INCLciPopUpNoConstitutivosRenta.AppearanceItemCaption.Options.UseFont = True
        Me.INCLciPopUpNoConstitutivosRenta.Control = Me.INDTxtPopUpNoConstitutivosRenta
        Me.INCLciPopUpNoConstitutivosRenta.Location = New System.Drawing.Point(0, 50)
        Me.INCLciPopUpNoConstitutivosRenta.MaxSize = New System.Drawing.Size(390, 50)
        Me.INCLciPopUpNoConstitutivosRenta.MinSize = New System.Drawing.Size(140, 50)
        Me.INCLciPopUpNoConstitutivosRenta.Name = "INCLciPopUpNoConstitutivosRenta"
        Me.INCLciPopUpNoConstitutivosRenta.Size = New System.Drawing.Size(279, 50)
        Me.INCLciPopUpNoConstitutivosRenta.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INCLciPopUpNoConstitutivosRenta.Text = "Ingresos No Constitutivos de Renta"
        Me.INCLciPopUpNoConstitutivosRenta.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INCLciPopUpNoConstitutivosRenta.TextLocation = DevExpress.Utils.Locations.Top
        Me.INCLciPopUpNoConstitutivosRenta.TextSize = New System.Drawing.Size(135, 21)
        Me.INCLciPopUpNoConstitutivosRenta.TextToControlDistance = 5
        '
        'INDlyItemSubTotalAPopup
        '
        Me.INDlyItemSubTotalAPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemSubTotalAPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemSubTotalAPopup.Control = Me.INDtxtSubTotalAPopup
        Me.INDlyItemSubTotalAPopup.CustomizationFormText = "SubTotal"
        Me.INDlyItemSubTotalAPopup.Location = New System.Drawing.Point(0, 100)
        Me.INDlyItemSubTotalAPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemSubTotalAPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemSubTotalAPopup.Name = "INDlyItemSubTotalAPopup"
        Me.INDlyItemSubTotalAPopup.OptionsToolTip.ToolTip = "Total Pagos Mes - Ingresos No Constitutivos de Renta"
        Me.INDlyItemSubTotalAPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemSubTotalAPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSubTotalAPopup.Text = "SubTotal 1"
        Me.INDlyItemSubTotalAPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSubTotalAPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSubTotalAPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSubTotalAPopup.TextToControlDistance = 5
        '
        'INDlyItemDeductionsPopup
        '
        Me.INDlyItemDeductionsPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemDeductionsPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemDeductionsPopup.Control = Me.INDtxtDeductionsPopup
        Me.INDlyItemDeductionsPopup.CustomizationFormText = "Deducciones"
        Me.INDlyItemDeductionsPopup.Location = New System.Drawing.Point(0, 150)
        Me.INDlyItemDeductionsPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemDeductionsPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemDeductionsPopup.Name = "INDlyItemDeductionsPopup"
        Me.INDlyItemDeductionsPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemDeductionsPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDeductionsPopup.Text = "Deducciones"
        Me.INDlyItemDeductionsPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDeductionsPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDeductionsPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemDeductionsPopup.TextToControlDistance = 5
        '
        'INDlyItemSubTotalBPopup
        '
        Me.INDlyItemSubTotalBPopup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemSubTotalBPopup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemSubTotalBPopup.Control = Me.INDtxtSubTotalBPopup
        Me.INDlyItemSubTotalBPopup.CustomizationFormText = "SubTotal 2"
        Me.INDlyItemSubTotalBPopup.Location = New System.Drawing.Point(0, 250)
        Me.INDlyItemSubTotalBPopup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemSubTotalBPopup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemSubTotalBPopup.Name = "INDlyItemSubTotalBPopup"
        Me.INDlyItemSubTotalBPopup.OptionsToolTip.ToolTip = "SubTotal 1 - Deducciones - Rentas Exentas"
        Me.INDlyItemSubTotalBPopup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemSubTotalBPopup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSubTotalBPopup.Text = "SubTotal 2"
        Me.INDlyItemSubTotalBPopup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSubTotalBPopup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSubTotalBPopup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSubTotalBPopup.TextToControlDistance = 5
        '
        'INDlyItemSubTotal3Popup
        '
        Me.INDlyItemSubTotal3Popup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemSubTotal3Popup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemSubTotal3Popup.Control = Me.INDtxtSubTotal3Popup
        Me.INDlyItemSubTotal3Popup.CustomizationFormText = "SubTotal 3"
        Me.INDlyItemSubTotal3Popup.Location = New System.Drawing.Point(0, 350)
        Me.INDlyItemSubTotal3Popup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemSubTotal3Popup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemSubTotal3Popup.Name = "INDlyItemSubTotal3Popup"
        Me.INDlyItemSubTotal3Popup.OptionsToolTip.ToolTip = "Deducciones + Rentas Exentas(Incluyendo Renta Exenta 25%)"
        Me.INDlyItemSubTotal3Popup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemSubTotal3Popup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSubTotal3Popup.Text = "SubTotal 3"
        Me.INDlyItemSubTotal3Popup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSubTotal3Popup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSubTotal3Popup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSubTotal3Popup.TextToControlDistance = 5
        '
        'INDlyItemSubTotal4Popup
        '
        Me.INDlyItemSubTotal4Popup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDlyItemSubTotal4Popup.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemSubTotal4Popup.Control = Me.INDtxtMaxDeductionsAndRentExentsPopup
        Me.INDlyItemSubTotal4Popup.CustomizationFormText = "Tope Máximo de Deducciones y Rentas Exentas"
        Me.INDlyItemSubTotal4Popup.Location = New System.Drawing.Point(0, 400)
        Me.INDlyItemSubTotal4Popup.MaxSize = New System.Drawing.Size(390, 50)
        Me.INDlyItemSubTotal4Popup.MinSize = New System.Drawing.Size(140, 50)
        Me.INDlyItemSubTotal4Popup.Name = "INDlyItemSubTotal4Popup"
        Me.INDlyItemSubTotal4Popup.OptionsToolTip.ToolTip = "40% del SubTotal 1 con Tope de 1340 UVT Anuales"
        Me.INDlyItemSubTotal4Popup.Size = New System.Drawing.Size(279, 50)
        Me.INDlyItemSubTotal4Popup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSubTotal4Popup.Text = "Tope Máximo de Deducciones y Rentas Exentas"
        Me.INDlyItemSubTotal4Popup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSubTotal4Popup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSubTotal4Popup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSubTotal4Popup.TextToControlDistance = 5
        '
        'INDspAccountContributions
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspAccountContributions, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspAccountContributions, False)
        Me.INDspAccountContributions.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspAccountContributions.EnterMoveNextControl = True
        Me.INDspAccountContributions.Location = New System.Drawing.Point(24, -262)
        Me.IndigoTextEdit1.SetMascara(Me.INDspAccountContributions, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspAccountContributions.Name = "INDspAccountContributions"
        Me.INDspAccountContributions.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspAccountContributions.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspAccountContributions.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDspAccountContributions.Properties.Appearance.Options.UseBackColor = True
        Me.INDspAccountContributions.Properties.Appearance.Options.UseFont = True
        Me.INDspAccountContributions.Properties.Appearance.Options.UseForeColor = True
        Me.INDspAccountContributions.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDspAccountContributions.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDspAccountContributions.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspAccountContributions.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDspAccountContributions.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspAccountContributions.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspAccountContributions.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspAccountContributions.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspAccountContributions.Properties.Mask.EditMask = "c0"
        Me.INDspAccountContributions.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspAccountContributions.Properties.MaxLength = 18
        Me.INDspAccountContributions.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDspAccountContributions.Size = New System.Drawing.Size(386, 28)
        Me.INDspAccountContributions.StyleController = Me.INDlyLiquidator
        Me.INDspAccountContributions.TabIndex = 36
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspAccountContributions, 0)
        '
        'INDspVoluntaryContributions
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspVoluntaryContributions, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspVoluntaryContributions, False)
        Me.INDspVoluntaryContributions.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspVoluntaryContributions.EnterMoveNextControl = True
        Me.INDspVoluntaryContributions.Location = New System.Drawing.Point(24, -326)
        Me.IndigoTextEdit1.SetMascara(Me.INDspVoluntaryContributions, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspVoluntaryContributions.Name = "INDspVoluntaryContributions"
        Me.INDspVoluntaryContributions.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspVoluntaryContributions.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspVoluntaryContributions.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDspVoluntaryContributions.Properties.Appearance.Options.UseBackColor = True
        Me.INDspVoluntaryContributions.Properties.Appearance.Options.UseFont = True
        Me.INDspVoluntaryContributions.Properties.Appearance.Options.UseForeColor = True
        Me.INDspVoluntaryContributions.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDspVoluntaryContributions.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDspVoluntaryContributions.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspVoluntaryContributions.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDspVoluntaryContributions.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspVoluntaryContributions.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspVoluntaryContributions.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspVoluntaryContributions.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspVoluntaryContributions.Properties.Mask.EditMask = "c0"
        Me.INDspVoluntaryContributions.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspVoluntaryContributions.Properties.MaxLength = 18
        Me.INDspVoluntaryContributions.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDspVoluntaryContributions.Size = New System.Drawing.Size(386, 28)
        Me.INDspVoluntaryContributions.StyleController = Me.INDlyLiquidator
        Me.INDspVoluntaryContributions.TabIndex = 35
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspVoluntaryContributions, 0)
        '
        'INDSPPaymentHealthObligatory
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSPPaymentHealthObligatory, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSPPaymentHealthObligatory, False)
        Me.INDSPPaymentHealthObligatory.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSPPaymentHealthObligatory.EnterMoveNextControl = True
        Me.INDSPPaymentHealthObligatory.Location = New System.Drawing.Point(24, -1008)
        Me.IndigoTextEdit1.SetMascara(Me.INDSPPaymentHealthObligatory, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSPPaymentHealthObligatory.Name = "INDSPPaymentHealthObligatory"
        Me.INDSPPaymentHealthObligatory.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSPPaymentHealthObligatory.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSPPaymentHealthObligatory.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDSPPaymentHealthObligatory.Properties.Appearance.Options.UseBackColor = True
        Me.INDSPPaymentHealthObligatory.Properties.Appearance.Options.UseFont = True
        Me.INDSPPaymentHealthObligatory.Properties.Appearance.Options.UseForeColor = True
        Me.INDSPPaymentHealthObligatory.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDSPPaymentHealthObligatory.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDSPPaymentHealthObligatory.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSPPaymentHealthObligatory.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDSPPaymentHealthObligatory.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSPPaymentHealthObligatory.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSPPaymentHealthObligatory.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSPPaymentHealthObligatory.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSPPaymentHealthObligatory.Properties.Mask.EditMask = "c0"
        Me.INDSPPaymentHealthObligatory.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSPPaymentHealthObligatory.Properties.MaxLength = 18
        Me.INDSPPaymentHealthObligatory.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDSPPaymentHealthObligatory.Properties.NullText = "$0"
        Me.INDSPPaymentHealthObligatory.Size = New System.Drawing.Size(386, 28)
        Me.INDSPPaymentHealthObligatory.StyleController = Me.INDlyLiquidator
        Me.INDSPPaymentHealthObligatory.TabIndex = 34
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSPPaymentHealthObligatory, 0)
        '
        'INDspSolidarityPension
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspSolidarityPension, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspSolidarityPension, False)
        Me.INDspSolidarityPension.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspSolidarityPension.EnterMoveNextControl = True
        Me.INDspSolidarityPension.Location = New System.Drawing.Point(24, -944)
        Me.IndigoTextEdit1.SetMascara(Me.INDspSolidarityPension, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspSolidarityPension.Name = "INDspSolidarityPension"
        Me.INDspSolidarityPension.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspSolidarityPension.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspSolidarityPension.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDspSolidarityPension.Properties.Appearance.Options.UseBackColor = True
        Me.INDspSolidarityPension.Properties.Appearance.Options.UseFont = True
        Me.INDspSolidarityPension.Properties.Appearance.Options.UseForeColor = True
        Me.INDspSolidarityPension.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDspSolidarityPension.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDspSolidarityPension.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspSolidarityPension.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDspSolidarityPension.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspSolidarityPension.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspSolidarityPension.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspSolidarityPension.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspSolidarityPension.Properties.Mask.EditMask = "c0"
        Me.INDspSolidarityPension.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspSolidarityPension.Properties.MaxLength = 18
        Me.INDspSolidarityPension.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDspSolidarityPension.Properties.NullText = "$0"
        Me.INDspSolidarityPension.Size = New System.Drawing.Size(386, 28)
        Me.INDspSolidarityPension.StyleController = Me.INDlyLiquidator
        Me.INDspSolidarityPension.TabIndex = 33
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspSolidarityPension, 0)
        '
        'INDspPensionContribution
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspPensionContribution, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspPensionContribution, False)
        Me.INDspPensionContribution.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspPensionContribution.EnterMoveNextControl = True
        Me.INDspPensionContribution.Location = New System.Drawing.Point(24, -1072)
        Me.IndigoTextEdit1.SetMascara(Me.INDspPensionContribution, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspPensionContribution.Name = "INDspPensionContribution"
        Me.INDspPensionContribution.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspPensionContribution.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspPensionContribution.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDspPensionContribution.Properties.Appearance.Options.UseBackColor = True
        Me.INDspPensionContribution.Properties.Appearance.Options.UseFont = True
        Me.INDspPensionContribution.Properties.Appearance.Options.UseForeColor = True
        Me.INDspPensionContribution.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDspPensionContribution.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDspPensionContribution.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspPensionContribution.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDspPensionContribution.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspPensionContribution.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspPensionContribution.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspPensionContribution.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspPensionContribution.Properties.Mask.EditMask = "c0"
        Me.INDspPensionContribution.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspPensionContribution.Properties.MaxLength = 18
        Me.INDspPensionContribution.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDspPensionContribution.Properties.NullText = "$0"
        Me.INDspPensionContribution.Size = New System.Drawing.Size(386, 28)
        Me.INDspPensionContribution.StyleController = Me.INDlyLiquidator
        Me.INDspPensionContribution.TabIndex = 32
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspPensionContribution, 0)
        '
        'INDspRiskWork
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspRiskWork, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspRiskWork, False)
        Me.INDspRiskWork.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspRiskWork.EnterMoveNextControl = True
        Me.INDspRiskWork.Location = New System.Drawing.Point(24, -507)
        Me.IndigoTextEdit1.SetMascara(Me.INDspRiskWork, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspRiskWork.Name = "INDspRiskWork"
        Me.INDspRiskWork.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspRiskWork.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspRiskWork.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDspRiskWork.Properties.Appearance.Options.UseBackColor = True
        Me.INDspRiskWork.Properties.Appearance.Options.UseFont = True
        Me.INDspRiskWork.Properties.Appearance.Options.UseForeColor = True
        Me.INDspRiskWork.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDspRiskWork.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDspRiskWork.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspRiskWork.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDspRiskWork.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspRiskWork.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspRiskWork.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspRiskWork.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspRiskWork.Properties.Mask.EditMask = "c0"
        Me.INDspRiskWork.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspRiskWork.Properties.MaxLength = 18
        Me.INDspRiskWork.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDspRiskWork.Properties.NullText = "$0"
        Me.INDspRiskWork.Size = New System.Drawing.Size(386, 28)
        Me.INDspRiskWork.StyleController = Me.INDlyLiquidator
        Me.INDspRiskWork.TabIndex = 31
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspRiskWork, 0)
        '
        'INDspDependent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspDependent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspDependent, False)
        Me.INDspDependent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspDependent.EnterMoveNextControl = True
        Me.INDspDependent.Location = New System.Drawing.Point(24, -635)
        Me.IndigoTextEdit1.SetMascara(Me.INDspDependent, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspDependent.Name = "INDspDependent"
        Me.INDspDependent.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspDependent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspDependent.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDspDependent.Properties.Appearance.Options.UseBackColor = True
        Me.INDspDependent.Properties.Appearance.Options.UseFont = True
        Me.INDspDependent.Properties.Appearance.Options.UseForeColor = True
        Me.INDspDependent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDspDependent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDspDependent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspDependent.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDspDependent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspDependent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspDependent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspDependent.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspDependent.Properties.Mask.EditMask = "c0"
        Me.INDspDependent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspDependent.Properties.MaxLength = 18
        Me.INDspDependent.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDspDependent.Properties.NullText = "$ 0"
        Me.INDspDependent.Size = New System.Drawing.Size(386, 28)
        Me.INDspDependent.StyleController = Me.INDlyLiquidator
        Me.INDspDependent.TabIndex = 30
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspDependent, 0)
        '
        'INDtxtFeeCommissionServiceData
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtFeeCommissionServiceData, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtFeeCommissionServiceData, False)
        Me.INDtxtFeeCommissionServiceData.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtFeeCommissionServiceData.EnterMoveNextControl = True
        Me.INDtxtFeeCommissionServiceData.Location = New System.Drawing.Point(24, -1317)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtFeeCommissionServiceData, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtFeeCommissionServiceData.Name = "INDtxtFeeCommissionServiceData"
        Me.INDtxtFeeCommissionServiceData.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtFeeCommissionServiceData.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFeeCommissionServiceData.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtFeeCommissionServiceData.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtFeeCommissionServiceData.Properties.Appearance.Options.UseFont = True
        Me.INDtxtFeeCommissionServiceData.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtFeeCommissionServiceData.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDtxtFeeCommissionServiceData.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtFeeCommissionServiceData.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtFeeCommissionServiceData.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDtxtFeeCommissionServiceData.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtFeeCommissionServiceData.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtFeeCommissionServiceData.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtFeeCommissionServiceData.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtFeeCommissionServiceData.Properties.Mask.EditMask = "c0"
        Me.INDtxtFeeCommissionServiceData.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtFeeCommissionServiceData.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDtxtFeeCommissionServiceData.Properties.NullText = "$0"
        Me.INDtxtFeeCommissionServiceData.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtFeeCommissionServiceData.StyleController = Me.INDlyLiquidator
        Me.INDtxtFeeCommissionServiceData.TabIndex = 29
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtFeeCommissionServiceData, 0)
        Me.INDtxtFeeCommissionServiceData.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.INDtxtFeeCommissionServiceData.ToolTipTitle = "Atención"
        '
        'INDtxtTotalIncomeExent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalIncomeExent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalIncomeExent, False)
        Me.INDtxtTotalIncomeExent.EnterMoveNextControl = True
        Me.INDtxtTotalIncomeExent.Location = New System.Drawing.Point(24, -198)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalIncomeExent, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTotalIncomeExent.Name = "INDtxtTotalIncomeExent"
        Me.INDtxtTotalIncomeExent.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalIncomeExent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalIncomeExent.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTotalIncomeExent.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalIncomeExent.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTotalIncomeExent.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtTotalIncomeExent.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalIncomeExent.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalIncomeExent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalIncomeExent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalIncomeExent.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTotalIncomeExent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalIncomeExent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtTotalIncomeExent.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtTotalIncomeExent.Properties.Mask.EditMask = "c0"
        Me.INDtxtTotalIncomeExent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalIncomeExent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalIncomeExent.Properties.NullText = "$0"
        Me.INDtxtTotalIncomeExent.Properties.ReadOnly = True
        Me.INDtxtTotalIncomeExent.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtTotalIncomeExent.StyleController = Me.INDlyLiquidator
        Me.INDtxtTotalIncomeExent.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalIncomeExent, 0)
        '
        'INDtxtTotalDeductions
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalDeductions, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalDeductions, False)
        Me.INDtxtTotalDeductions.EnterMoveNextControl = True
        Me.INDtxtTotalDeductions.Location = New System.Drawing.Point(24, -437)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalDeductions, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTotalDeductions.Name = "INDtxtTotalDeductions"
        Me.INDtxtTotalDeductions.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalDeductions.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalDeductions.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTotalDeductions.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalDeductions.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTotalDeductions.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtTotalDeductions.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalDeductions.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalDeductions.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalDeductions.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalDeductions.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTotalDeductions.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalDeductions.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtTotalDeductions.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtTotalDeductions.Properties.Mask.EditMask = "c0"
        Me.INDtxtTotalDeductions.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalDeductions.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalDeductions.Properties.NullText = "$0"
        Me.INDtxtTotalDeductions.Properties.ReadOnly = True
        Me.INDtxtTotalDeductions.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtTotalDeductions.StyleController = Me.INDlyLiquidator
        Me.INDtxtTotalDeductions.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalDeductions, 0)
        '
        'INDtxtTotalNoConstitutive
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalNoConstitutive, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalNoConstitutive, False)
        Me.INDtxtTotalNoConstitutive.EnterMoveNextControl = True
        Me.INDtxtTotalNoConstitutive.Location = New System.Drawing.Point(24, -810)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalNoConstitutive, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTotalNoConstitutive.Name = "INDtxtTotalNoConstitutive"
        Me.INDtxtTotalNoConstitutive.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalNoConstitutive.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalNoConstitutive.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTotalNoConstitutive.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalNoConstitutive.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTotalNoConstitutive.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtTotalNoConstitutive.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalNoConstitutive.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalNoConstitutive.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalNoConstitutive.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalNoConstitutive.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTotalNoConstitutive.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalNoConstitutive.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtTotalNoConstitutive.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtTotalNoConstitutive.Properties.Mask.EditMask = "c0"
        Me.INDtxtTotalNoConstitutive.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalNoConstitutive.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalNoConstitutive.Properties.NullText = "$0"
        Me.INDtxtTotalNoConstitutive.Properties.ReadOnly = True
        Me.INDtxtTotalNoConstitutive.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtTotalNoConstitutive.StyleController = Me.INDlyLiquidator
        Me.INDtxtTotalNoConstitutive.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalNoConstitutive, 0)
        '
        'INDtxtTotalIncomeMonthlyData
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTotalIncomeMonthlyData, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTotalIncomeMonthlyData, False)
        Me.INDtxtTotalIncomeMonthlyData.EnterMoveNextControl = True
        Me.INDtxtTotalIncomeMonthlyData.Location = New System.Drawing.Point(24, -1183)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTotalIncomeMonthlyData, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTotalIncomeMonthlyData.Name = "INDtxtTotalIncomeMonthlyData"
        Me.INDtxtTotalIncomeMonthlyData.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalIncomeMonthlyData.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalIncomeMonthlyData.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTotalIncomeMonthlyData.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTotalIncomeMonthlyData.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTotalIncomeMonthlyData.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtTotalIncomeMonthlyData.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTotalIncomeMonthlyData.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTotalIncomeMonthlyData.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTotalIncomeMonthlyData.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTotalIncomeMonthlyData.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTotalIncomeMonthlyData.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTotalIncomeMonthlyData.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtTotalIncomeMonthlyData.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtTotalIncomeMonthlyData.Properties.Mask.EditMask = "c0"
        Me.INDtxtTotalIncomeMonthlyData.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTotalIncomeMonthlyData.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTotalIncomeMonthlyData.Properties.NullText = "$0"
        Me.INDtxtTotalIncomeMonthlyData.Properties.ReadOnly = True
        Me.INDtxtTotalIncomeMonthlyData.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtTotalIncomeMonthlyData.StyleController = Me.INDlyLiquidator
        Me.INDtxtTotalIncomeMonthlyData.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTotalIncomeMonthlyData, 0)
        '
        'INDtxtPreparedHealth
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPreparedHealth, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPreparedHealth, False)
        Me.INDtxtPreparedHealth.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtPreparedHealth.EnterMoveNextControl = True
        Me.INDtxtPreparedHealth.Location = New System.Drawing.Point(24, -565)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPreparedHealth, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPreparedHealth.Name = "INDtxtPreparedHealth"
        Me.INDtxtPreparedHealth.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtPreparedHealth.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPreparedHealth.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtPreparedHealth.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPreparedHealth.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPreparedHealth.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtPreparedHealth.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtPreparedHealth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtPreparedHealth.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDtxtPreparedHealth.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtPreparedHealth.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPreparedHealth.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDtxtPreparedHealth.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPreparedHealth.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtPreparedHealth.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPreparedHealth.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtPreparedHealth.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtPreparedHealth.Properties.Mask.EditMask = "c0"
        Me.INDtxtPreparedHealth.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtPreparedHealth.Properties.MaxLength = 18
        Me.INDtxtPreparedHealth.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDtxtPreparedHealth.Properties.NullText = "$0"
        Me.INDtxtPreparedHealth.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPreparedHealth.StyleController = Me.INDlyLiquidator
        Me.INDtxtPreparedHealth.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPreparedHealth, 0)
        '
        'INDtxtInterests
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtInterests, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtInterests, False)
        Me.INDtxtInterests.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtInterests.EnterMoveNextControl = True
        Me.INDtxtInterests.Location = New System.Drawing.Point(24, -693)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtInterests, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtInterests.Name = "INDtxtInterests"
        Me.INDtxtInterests.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtInterests.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtInterests.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtInterests.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtInterests.Properties.Appearance.Options.UseFont = True
        Me.INDtxtInterests.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtInterests.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtInterests.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtInterests.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDtxtInterests.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDtxtInterests.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtInterests.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDtxtInterests.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtInterests.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtInterests.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtInterests.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtInterests.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtInterests.Properties.Mask.EditMask = "c0"
        Me.INDtxtInterests.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtInterests.Properties.MaxLength = 18
        Me.INDtxtInterests.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDtxtInterests.Properties.NullText = "$0"
        Me.INDtxtInterests.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtInterests.StyleController = Me.INDlyLiquidator
        Me.INDtxtInterests.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtInterests, 0)
        '
        'INDspPensionByIndividualSavingsRegime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDspPensionByIndividualSavingsRegime, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDspPensionByIndividualSavingsRegime, False)
        Me.INDspPensionByIndividualSavingsRegime.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDspPensionByIndividualSavingsRegime.EnterMoveNextControl = True
        Me.INDspPensionByIndividualSavingsRegime.Location = New System.Drawing.Point(24, -880)
        Me.IndigoTextEdit1.SetMascara(Me.INDspPensionByIndividualSavingsRegime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDspPensionByIndividualSavingsRegime.Name = "INDspPensionByIndividualSavingsRegime"
        Me.INDspPensionByIndividualSavingsRegime.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDspPensionByIndividualSavingsRegime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspPensionByIndividualSavingsRegime.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDspPensionByIndividualSavingsRegime.Properties.Appearance.Options.UseBackColor = True
        Me.INDspPensionByIndividualSavingsRegime.Properties.Appearance.Options.UseFont = True
        Me.INDspPensionByIndividualSavingsRegime.Properties.Appearance.Options.UseForeColor = True
        Me.INDspPensionByIndividualSavingsRegime.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White
        Me.INDspPensionByIndividualSavingsRegime.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.INDspPensionByIndividualSavingsRegime.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDspPensionByIndividualSavingsRegime.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black
        Me.INDspPensionByIndividualSavingsRegime.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDspPensionByIndividualSavingsRegime.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDspPensionByIndividualSavingsRegime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDspPensionByIndividualSavingsRegime.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDspPensionByIndividualSavingsRegime.Properties.Mask.EditMask = "c0"
        Me.INDspPensionByIndividualSavingsRegime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDspPensionByIndividualSavingsRegime.Properties.MaxLength = 18
        Me.INDspPensionByIndividualSavingsRegime.Properties.MaxValue = New Decimal(New Integer() {-1486618625, 232830643, 0, 0})
        Me.INDspPensionByIndividualSavingsRegime.Size = New System.Drawing.Size(386, 28)
        Me.INDspPensionByIndividualSavingsRegime.StyleController = Me.INDlyLiquidator
        Me.INDspPensionByIndividualSavingsRegime.TabIndex = 37
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDspPensionByIndividualSavingsRegime, 0)
        '
        'INDtxtExemptIncomeControl
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtExemptIncomeControl, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtExemptIncomeControl, False)
        Me.INDtxtExemptIncomeControl.EnterMoveNextControl = True
        Me.INDtxtExemptIncomeControl.Location = New System.Drawing.Point(24, -17)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtExemptIncomeControl, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtExemptIncomeControl.Name = "INDtxtExemptIncomeControl"
        Me.INDtxtExemptIncomeControl.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtExemptIncomeControl.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExemptIncomeControl.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtExemptIncomeControl.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtExemptIncomeControl.Properties.Appearance.Options.UseFont = True
        Me.INDtxtExemptIncomeControl.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtExemptIncomeControl.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtExemptIncomeControl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtExemptIncomeControl.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtExemptIncomeControl.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExemptIncomeControl.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtExemptIncomeControl.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtExemptIncomeControl.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtExemptIncomeControl.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtExemptIncomeControl.Properties.Mask.EditMask = "c0"
        Me.INDtxtExemptIncomeControl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtExemptIncomeControl.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtExemptIncomeControl.Properties.NullText = "$0"
        Me.INDtxtExemptIncomeControl.Properties.ReadOnly = True
        Me.INDtxtExemptIncomeControl.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtExemptIncomeControl.StyleController = Me.INDlyLiquidator
        Me.INDtxtExemptIncomeControl.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtExemptIncomeControl, 0)
        '
        'INDtxExemptIncome25Percent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxExemptIncome25Percent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxExemptIncome25Percent, False)
        Me.INDtxExemptIncome25Percent.EnterMoveNextControl = True
        Me.INDtxExemptIncome25Percent.Location = New System.Drawing.Point(24, 47)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxExemptIncome25Percent, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxExemptIncome25Percent.Name = "INDtxExemptIncome25Percent"
        Me.INDtxExemptIncome25Percent.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxExemptIncome25Percent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxExemptIncome25Percent.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxExemptIncome25Percent.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxExemptIncome25Percent.Properties.Appearance.Options.UseFont = True
        Me.INDtxExemptIncome25Percent.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxExemptIncome25Percent.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxExemptIncome25Percent.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxExemptIncome25Percent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxExemptIncome25Percent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxExemptIncome25Percent.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxExemptIncome25Percent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxExemptIncome25Percent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxExemptIncome25Percent.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxExemptIncome25Percent.Properties.Mask.EditMask = "c0"
        Me.INDtxExemptIncome25Percent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxExemptIncome25Percent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxExemptIncome25Percent.Properties.NullText = "$0"
        Me.INDtxExemptIncome25Percent.Properties.ReadOnly = True
        Me.INDtxExemptIncome25Percent.Size = New System.Drawing.Size(386, 28)
        Me.INDtxExemptIncome25Percent.StyleController = Me.INDlyLiquidator
        Me.INDtxExemptIncome25Percent.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxExemptIncome25Percent, 0)
        '
        'INDtxtExemptIncomeAndDeductions
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtExemptIncomeAndDeductions, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtExemptIncomeAndDeductions, False)
        Me.INDtxtExemptIncomeAndDeductions.EnterMoveNextControl = True
        Me.INDtxtExemptIncomeAndDeductions.Location = New System.Drawing.Point(24, 175)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtExemptIncomeAndDeductions, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtExemptIncomeAndDeductions.Name = "INDtxtExemptIncomeAndDeductions"
        Me.INDtxtExemptIncomeAndDeductions.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtExemptIncomeAndDeductions.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExemptIncomeAndDeductions.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtExemptIncomeAndDeductions.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtExemptIncomeAndDeductions.Properties.Appearance.Options.UseFont = True
        Me.INDtxtExemptIncomeAndDeductions.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtExemptIncomeAndDeductions.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtExemptIncomeAndDeductions.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtExemptIncomeAndDeductions.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtExemptIncomeAndDeductions.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExemptIncomeAndDeductions.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtExemptIncomeAndDeductions.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtExemptIncomeAndDeductions.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtExemptIncomeAndDeductions.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtExemptIncomeAndDeductions.Properties.Mask.EditMask = "c0"
        Me.INDtxtExemptIncomeAndDeductions.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtExemptIncomeAndDeductions.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtExemptIncomeAndDeductions.Properties.NullText = "$0"
        Me.INDtxtExemptIncomeAndDeductions.Properties.ReadOnly = True
        Me.INDtxtExemptIncomeAndDeductions.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtExemptIncomeAndDeductions.StyleController = Me.INDlyLiquidator
        Me.INDtxtExemptIncomeAndDeductions.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtExemptIncomeAndDeductions, 0)
        '
        'INDtxtControlExemptIncomeAndDeductions
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtControlExemptIncomeAndDeductions, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtControlExemptIncomeAndDeductions, False)
        Me.INDtxtControlExemptIncomeAndDeductions.EnterMoveNextControl = True
        Me.INDtxtControlExemptIncomeAndDeductions.Location = New System.Drawing.Point(24, 111)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtControlExemptIncomeAndDeductions, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtControlExemptIncomeAndDeductions.Name = "INDtxtControlExemptIncomeAndDeductions"
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Appearance.Options.UseFont = True
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Mask.EditMask = "c0"
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.NullText = "$0"
        Me.INDtxtControlExemptIncomeAndDeductions.Properties.ReadOnly = True
        Me.INDtxtControlExemptIncomeAndDeductions.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtControlExemptIncomeAndDeductions.StyleController = Me.INDlyLiquidator
        Me.INDtxtControlExemptIncomeAndDeductions.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtControlExemptIncomeAndDeductions, 0)
        '
        'INDtxtTaxBase
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtTaxBase, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtTaxBase, False)
        Me.INDtxtTaxBase.EnterMoveNextControl = True
        Me.INDtxtTaxBase.Location = New System.Drawing.Point(24, 239)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtTaxBase, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtTaxBase.Name = "INDtxtTaxBase"
        Me.INDtxtTaxBase.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTaxBase.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTaxBase.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTaxBase.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtTaxBase.Properties.Appearance.Options.UseFont = True
        Me.INDtxtTaxBase.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtTaxBase.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtTaxBase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtTaxBase.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtTaxBase.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtTaxBase.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtTaxBase.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtTaxBase.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtTaxBase.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtTaxBase.Properties.Mask.EditMask = "c0"
        Me.INDtxtTaxBase.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtTaxBase.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtTaxBase.Properties.NullText = "$0"
        Me.INDtxtTaxBase.Properties.ReadOnly = True
        Me.INDtxtTaxBase.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtTaxBase.StyleController = Me.INDlyLiquidator
        Me.INDtxtTaxBase.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtTaxBase, 0)
        '
        'INDtxtSubtotal
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSubtotal, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSubtotal, False)
        Me.INDtxtSubtotal.EnterMoveNextControl = True
        Me.INDtxtSubtotal.Location = New System.Drawing.Point(24, -81)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSubtotal, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtSubtotal.Name = "INDtxtSubtotal"
        Me.INDtxtSubtotal.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtSubtotal.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubtotal.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtSubtotal.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSubtotal.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSubtotal.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtSubtotal.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtSubtotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtSubtotal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtSubtotal.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSubtotal.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtSubtotal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtSubtotal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSubtotal.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtSubtotal.Properties.Mask.EditMask = "c0"
        Me.INDtxtSubtotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtSubtotal.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtSubtotal.Properties.NullText = "$0"
        Me.INDtxtSubtotal.Properties.ReadOnly = True
        Me.INDtxtSubtotal.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtSubtotal.StyleController = Me.INDlyLiquidator
        Me.INDtxtSubtotal.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSubtotal, 0)
        '
        'INDtxtAccumulatedIncome
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtAccumulatedIncome, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtAccumulatedIncome, False)
        Me.INDtxtAccumulatedIncome.EditValue = "$0"
        Me.INDtxtAccumulatedIncome.EnterMoveNextControl = True
        Me.INDtxtAccumulatedIncome.Location = New System.Drawing.Point(24, -1253)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtAccumulatedIncome, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtAccumulatedIncome.Name = "INDtxtAccumulatedIncome"
        Me.INDtxtAccumulatedIncome.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtAccumulatedIncome.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtxtAccumulatedIncome.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtAccumulatedIncome.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtAccumulatedIncome.Properties.Appearance.Options.UseFont = True
        Me.INDtxtAccumulatedIncome.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtAccumulatedIncome.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtAccumulatedIncome.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtAccumulatedIncome.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtAccumulatedIncome.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtAccumulatedIncome.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtAccumulatedIncome.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtAccumulatedIncome.Properties.Mask.EditMask = "c0"
        Me.INDtxtAccumulatedIncome.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtAccumulatedIncome.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtAccumulatedIncome.Properties.ReadOnly = True
        Me.INDtxtAccumulatedIncome.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtAccumulatedIncome.StyleController = Me.INDlyLiquidator
        Me.INDtxtAccumulatedIncome.TabIndex = 45
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtAccumulatedIncome, 0)
        '
        'INDSbInfoIncomeByMonth
        '
        Me.INDSbInfoIncomeByMonth.ImageOptions.Image = Global.Presentation.Controls.My.Resources.Resources.MoreInfo_24x24_blue
        Me.INDSbInfoIncomeByMonth.Location = New System.Drawing.Point(414, -1252)
        Me.INDSbInfoIncomeByMonth.MaximumSize = New System.Drawing.Size(30, 28)
        Me.INDSbInfoIncomeByMonth.MinimumSize = New System.Drawing.Size(30, 28)
        Me.INDSbInfoIncomeByMonth.Name = "INDSbInfoIncomeByMonth"
        Me.INDSbInfoIncomeByMonth.Size = New System.Drawing.Size(30, 28)
        Me.INDSbInfoIncomeByMonth.StyleController = Me.INDlyLiquidator
        Me.INDSbInfoIncomeByMonth.TabIndex = 38
        Me.INDSbInfoIncomeByMonth.ToolTip = "Ver detalle"
        '
        'INDtxtRetArt383
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtRetArt383, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtRetArt383, False)
        Me.INDtxtRetArt383.EditValue = "$0"
        Me.INDtxtRetArt383.Location = New System.Drawing.Point(36, 344)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtRetArt383, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtRetArt383.Name = "INDtxtRetArt383"
        Me.INDtxtRetArt383.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtRetArt383.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtxtRetArt383.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtRetArt383.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtRetArt383.Properties.Appearance.Options.UseFont = True
        Me.INDtxtRetArt383.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtRetArt383.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtRetArt383.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtRetArt383.Properties.AppearanceDisabled.Options.UseTextOptions = True
        Me.INDtxtRetArt383.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtRetArt383.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtRetArt383.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtRetArt383.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtRetArt383.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtRetArt383.Properties.Mask.EditMask = "c0"
        Me.INDtxtRetArt383.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtRetArt383.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtRetArt383.Properties.ReadOnly = True
        Me.INDtxtRetArt383.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtRetArt383.StyleController = Me.INDlyLiquidator
        Me.INDtxtRetArt383.TabIndex = 46
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtRetArt383, 0)
        '
        'INDtxtPreviousDeductions
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPreviousDeductions, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPreviousDeductions, False)
        Me.INDtxtPreviousDeductions.EditValue = "$0"
        Me.INDtxtPreviousDeductions.Location = New System.Drawing.Point(36, 408)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPreviousDeductions, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPreviousDeductions.Name = "INDtxtPreviousDeductions"
        Me.INDtxtPreviousDeductions.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtPreviousDeductions.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtxtPreviousDeductions.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtPreviousDeductions.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPreviousDeductions.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPreviousDeductions.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtPreviousDeductions.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtPreviousDeductions.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtPreviousDeductions.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtPreviousDeductions.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtPreviousDeductions.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPreviousDeductions.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtPreviousDeductions.Properties.Mask.EditMask = "c0"
        Me.INDtxtPreviousDeductions.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtPreviousDeductions.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtPreviousDeductions.Properties.ReadOnly = True
        Me.INDtxtPreviousDeductions.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPreviousDeductions.StyleController = Me.INDlyLiquidator
        Me.INDtxtPreviousDeductions.TabIndex = 47
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPreviousDeductions, 0)
        '
        'INDtxtWithholdingApplied
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtWithholdingApplied, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtWithholdingApplied, False)
        Me.INDtxtWithholdingApplied.EditValue = "$0"
        Me.INDtxtWithholdingApplied.Location = New System.Drawing.Point(36, 472)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtWithholdingApplied, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtWithholdingApplied.Name = "INDtxtWithholdingApplied"
        Me.INDtxtWithholdingApplied.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtWithholdingApplied.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDtxtWithholdingApplied.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtWithholdingApplied.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtWithholdingApplied.Properties.Appearance.Options.UseFont = True
        Me.INDtxtWithholdingApplied.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtWithholdingApplied.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtWithholdingApplied.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtWithholdingApplied.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDtxtWithholdingApplied.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.INDtxtWithholdingApplied.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtWithholdingApplied.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtWithholdingApplied.Properties.Mask.EditMask = "c0"
        Me.INDtxtWithholdingApplied.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtWithholdingApplied.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtWithholdingApplied.Properties.ReadOnly = True
        Me.INDtxtWithholdingApplied.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtWithholdingApplied.StyleController = Me.INDlyLiquidator
        Me.INDtxtWithholdingApplied.TabIndex = 48
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtWithholdingApplied, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPaymentsMonthly, Me.INDlygRentsExempts, Me.INDlygDeductions, Me.LayoutControlGroup3, Me.INDlcgSubtotal})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(564, 1938)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygPaymentsMonthly
        '
        Me.INDlygPaymentsMonthly.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPaymentsMonthly.AppearanceGroup.Options.UseFont = True
        Me.INDlygPaymentsMonthly.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPaymentsMonthly.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPaymentsMonthly.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPaymentsMonthly.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPaymentsMonthly.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPaymentsMonthly.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPaymentsMonthly.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPaymentsMonthly.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPaymentsMonthly.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPaymentsMonthly.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPaymentsMonthly.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPaymentsMonthly.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPaymentsMonthly, False)
        Me.INDlygPaymentsMonthly.CustomizationFormText = "2. Total pagos del mes"
        Me.INDlygPaymentsMonthly.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemTotalIncomeMonthlyData, Me.LayoutControlItem2, Me.INDLciAccumulatedIncome, Me.LayoutControlItem14, Me.EmptySpaceItem1})
        Me.INDlygPaymentsMonthly.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPaymentsMonthly.Name = "INDlygPaymentsMonthly"
        Me.INDlygPaymentsMonthly.Size = New System.Drawing.Size(544, 245)
        Me.INDlygPaymentsMonthly.Text = "Retención de Salarios"
        '
        'INDlyItemTotalIncomeMonthlyData
        '
        Me.INDlyItemTotalIncomeMonthlyData.Control = Me.INDtxtTotalIncomeMonthlyData
        Me.INDlyItemTotalIncomeMonthlyData.CustomizationFormText = "Total Ingresos Mes"
        Me.INDlyItemTotalIncomeMonthlyData.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemTotalIncomeMonthlyData.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemTotalIncomeMonthlyData.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemTotalIncomeMonthlyData.Name = "INDlyItemTotalIncomeMonthlyData"
        Me.INDlyItemTotalIncomeMonthlyData.Size = New System.Drawing.Size(520, 64)
        Me.INDlyItemTotalIncomeMonthlyData.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalIncomeMonthlyData.Text = "Total Ingresos Mes"
        Me.INDlyItemTotalIncomeMonthlyData.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTotalIncomeMonthlyData.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTotalIncomeMonthlyData.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTotalIncomeMonthlyData.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDtxtFeeCommissionServiceData
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(520, 64)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Honorarios, Comisiones o Servicios"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDLciAccumulatedIncome
        '
        Me.INDLciAccumulatedIncome.Control = Me.INDtxtAccumulatedIncome
        Me.INDLciAccumulatedIncome.Location = New System.Drawing.Point(0, 64)
        Me.INDLciAccumulatedIncome.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAccumulatedIncome.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAccumulatedIncome.Name = "INDLciAccumulatedIncome"
        Me.INDLciAccumulatedIncome.Size = New System.Drawing.Size(390, 64)
        Me.INDLciAccumulatedIncome.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAccumulatedIncome.Text = "Ingresos Acumulados Del Mes"
        Me.INDLciAccumulatedIncome.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAccumulatedIncome.TextSize = New System.Drawing.Size(364, 17)
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.INDSbInfoIncomeByMonth
        Me.LayoutControlItem14.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(390, 85)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.OptionsPrint.AppearanceItem.Options.UseTextOptions = True
        Me.LayoutControlItem14.OptionsPrint.AppearanceItem.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem14.OptionsPrint.AppearanceItemControl.Options.UseTextOptions = True
        Me.LayoutControlItem14.OptionsPrint.AppearanceItemControl.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutControlItem14.Size = New System.Drawing.Size(130, 43)
        Me.LayoutControlItem14.Text = "LayoutControlItem8"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem14.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(390, 64)
        Me.EmptySpaceItem1.MaxSize = New System.Drawing.Size(0, 21)
        Me.EmptySpaceItem1.MinSize = New System.Drawing.Size(1, 21)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(130, 21)
        Me.EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlygRentsExempts
        '
        Me.INDlygRentsExempts.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygRentsExempts.AppearanceGroup.Options.UseFont = True
        Me.INDlygRentsExempts.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygRentsExempts.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygRentsExempts.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygRentsExempts.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygRentsExempts.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygRentsExempts.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygRentsExempts.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygRentsExempts.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygRentsExempts.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygRentsExempts.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygRentsExempts.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygRentsExempts.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygRentsExempts, False)
        Me.INDlygRentsExempts.CustomizationFormText = "3. Menos rentas exentas"
        Me.INDlygRentsExempts.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemTotalIncomeExent, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.INDlciPensionByIndividualSavingsRegime})
        Me.INDlygRentsExempts.Location = New System.Drawing.Point(0, 245)
        Me.INDlygRentsExempts.Name = "INDlygRentsExempts"
        Me.INDlygRentsExempts.Size = New System.Drawing.Size(544, 373)
        Me.INDlygRentsExempts.Text = "Ingresos No Constitutivos de Renta"
        '
        'INDlyItemTotalIncomeExent
        '
        Me.INDlyItemTotalIncomeExent.Control = Me.INDtxtTotalNoConstitutive
        Me.INDlyItemTotalIncomeExent.CustomizationFormText = "Total Renta Exentas"
        Me.INDlyItemTotalIncomeExent.Location = New System.Drawing.Point(0, 256)
        Me.INDlyItemTotalIncomeExent.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemTotalIncomeExent.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemTotalIncomeExent.Name = "INDlyItemTotalIncomeExent"
        Me.INDlyItemTotalIncomeExent.Size = New System.Drawing.Size(520, 64)
        Me.INDlyItemTotalIncomeExent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalIncomeExent.Text = "Total Ingresos No Constitutivos"
        Me.INDlyItemTotalIncomeExent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTotalIncomeExent.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTotalIncomeExent.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTotalIncomeExent.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDspPensionContribution
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(520, 64)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Aportes Obligatorios Fondo Pensiones"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(364, 17)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDspSolidarityPension
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 128)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(520, 64)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "Fondo Solidaridad Pensional"
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(364, 17)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDSPPaymentHealthObligatory
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(520, 64)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "Aportes Obligatorios a Salud"
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlciPensionByIndividualSavingsRegime
        '
        Me.INDlciPensionByIndividualSavingsRegime.Control = Me.INDspPensionByIndividualSavingsRegime
        Me.INDlciPensionByIndividualSavingsRegime.Location = New System.Drawing.Point(0, 192)
        Me.INDlciPensionByIndividualSavingsRegime.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciPensionByIndividualSavingsRegime.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciPensionByIndividualSavingsRegime.Name = "INDlciPensionByIndividualSavingsRegime"
        Me.INDlciPensionByIndividualSavingsRegime.OptionsToolTip.ToolTip = "Cotizaciones voluntarias al régimen de ahorro individual con solidaridad del sist" &
    "ema general de pensiones"
        Me.INDlciPensionByIndividualSavingsRegime.Size = New System.Drawing.Size(520, 64)
        Me.INDlciPensionByIndividualSavingsRegime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPensionByIndividualSavingsRegime.Text = "Aportes Voluntarios a Pensión régimen ahorro individual"
        Me.INDlciPensionByIndividualSavingsRegime.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciPensionByIndividualSavingsRegime.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlygDeductions
        '
        Me.INDlygDeductions.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDeductions.AppearanceGroup.Options.UseFont = True
        Me.INDlygDeductions.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygDeductions.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygDeductions.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDeductions.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygDeductions.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygDeductions.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygDeductions.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDeductions.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygDeductions.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDeductions.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygDeductions.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygDeductions.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygDeductions, False)
        Me.INDlygDeductions.CustomizationFormText = "3. Menos deducciones"
        Me.INDlygDeductions.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPreparedHealth, Me.INDlyItemTotalDeductionsPartial, Me.INDlyItemInterests, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.INDlygDeductions.Location = New System.Drawing.Point(0, 618)
        Me.INDlygDeductions.Name = "INDlygDeductions"
        Me.INDlygDeductions.Size = New System.Drawing.Size(544, 373)
        Me.INDlygDeductions.Text = "Deducciones"
        '
        'INDlyItemPreparedHealth
        '
        Me.INDlyItemPreparedHealth.Control = Me.INDtxtPreparedHealth
        Me.INDlyItemPreparedHealth.CustomizationFormText = "Pagos ARL, Medicina Prepagada, Planes Adicionales"
        Me.INDlyItemPreparedHealth.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemPreparedHealth.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemPreparedHealth.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemPreparedHealth.Name = "INDlyItemPreparedHealth"
        Me.INDlyItemPreparedHealth.Size = New System.Drawing.Size(520, 64)
        Me.INDlyItemPreparedHealth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPreparedHealth.Text = "Pagos por Salud"
        Me.INDlyItemPreparedHealth.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPreparedHealth.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPreparedHealth.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPreparedHealth.TextToControlDistance = 5
        '
        'INDlyItemTotalDeductionsPartial
        '
        Me.INDlyItemTotalDeductionsPartial.Control = Me.INDtxtTotalDeductions
        Me.INDlyItemTotalDeductionsPartial.CustomizationFormText = "LayoutControlItem1"
        Me.INDlyItemTotalDeductionsPartial.Location = New System.Drawing.Point(0, 256)
        Me.INDlyItemTotalDeductionsPartial.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemTotalDeductionsPartial.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemTotalDeductionsPartial.Name = "INDlyItemTotalDeductionsPartial"
        Me.INDlyItemTotalDeductionsPartial.Size = New System.Drawing.Size(520, 64)
        Me.INDlyItemTotalDeductionsPartial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTotalDeductionsPartial.Text = "Total Deducciones"
        Me.INDlyItemTotalDeductionsPartial.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTotalDeductionsPartial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTotalDeductionsPartial.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemTotalDeductionsPartial.TextToControlDistance = 5
        '
        'INDlyItemInterests
        '
        Me.INDlyItemInterests.Control = Me.INDtxtInterests
        Me.INDlyItemInterests.CustomizationFormText = "Intereses Prestamos Vivienda"
        Me.INDlyItemInterests.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemInterests.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemInterests.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemInterests.Name = "INDlyItemInterests"
        Me.INDlyItemInterests.Size = New System.Drawing.Size(520, 64)
        Me.INDlyItemInterests.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInterests.Text = "Intereses Prestamos Vivienda"
        Me.INDlyItemInterests.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInterests.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInterests.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInterests.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDspDependent
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(520, 64)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Por Dependientes"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(364, 17)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDspRiskWork
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 192)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(520, 64)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Aportes Riesgos Laborales"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(364, 17)
        Me.LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDlciVoluntaryContributions, Me.LayoutControlItem9})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 991)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(544, 245)
        Me.LayoutControlGroup3.Text = "Rentas Exentas"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDtxtTotalIncomeExent
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 128)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(520, 64)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Total Rentas Exentas"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlciVoluntaryContributions
        '
        Me.INDlciVoluntaryContributions.Control = Me.INDspVoluntaryContributions
        Me.INDlciVoluntaryContributions.Location = New System.Drawing.Point(0, 0)
        Me.INDlciVoluntaryContributions.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciVoluntaryContributions.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciVoluntaryContributions.Name = "INDlciVoluntaryContributions"
        Me.INDlciVoluntaryContributions.Size = New System.Drawing.Size(520, 64)
        Me.INDlciVoluntaryContributions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciVoluntaryContributions.Text = "Aportes Fondo Pensiones Voluntaria"
        Me.INDlciVoluntaryContributions.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciVoluntaryContributions.TextSize = New System.Drawing.Size(364, 17)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDspAccountContributions
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(390, 64)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(520, 64)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "Aportes Destino a Cuentas AFC"
        Me.LayoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlcgSubtotal
        '
        Me.INDlcgSubtotal.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgSubtotal.AppearanceGroup.Options.UseFont = True
        Me.INDlcgSubtotal.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgSubtotal.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgSubtotal.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgSubtotal.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgSubtotal.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgSubtotal.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgSubtotal.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgSubtotal.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgSubtotal.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgSubtotal.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgSubtotal.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgSubtotal.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgSubtotal, False)
        Me.INDlcgSubtotal.CustomizationFormText = "Subtotal"
        Me.INDlcgSubtotal.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciExemptIncomeControl, Me.INDlciExemptIncome25Percent, Me.INDlciExemptIncomeAndDeductions, Me.INDlciTaxBase, Me.INDlciControlExemptIncomeAndDeductions, Me.INDlciSubtotal, Me.LayoutControlItem10, Me.EmptySpaceItem2, Me.EmptySpaceItem3, Me.LayoutControlItem11, Me.EmptySpaceItem4, Me.LayoutControlItem13, Me.EmptySpaceItem5, Me.LayoutControlItem8, Me.INDLcgAppyRetentios})
        Me.INDlcgSubtotal.Location = New System.Drawing.Point(0, 1236)
        Me.INDlcgSubtotal.Name = "INDlcgSubtotal"
        Me.INDlcgSubtotal.Size = New System.Drawing.Size(544, 682)
        Me.INDlcgSubtotal.Text = "Subtotal"
        '
        'INDlciExemptIncomeControl
        '
        Me.INDlciExemptIncomeControl.Control = Me.INDtxtExemptIncomeControl
        Me.INDlciExemptIncomeControl.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciExemptIncomeControl.CustomizationFormText = "Control Renta Exenta 25%"
        Me.INDlciExemptIncomeControl.Location = New System.Drawing.Point(0, 64)
        Me.INDlciExemptIncomeControl.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncomeControl.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncomeControl.Name = "INDlciExemptIncomeControl"
        Me.INDlciExemptIncomeControl.OptionsToolTip.ToolTip = "Límite 790 UVT anuales"
        Me.INDlciExemptIncomeControl.ShowInCustomizationForm = False
        Me.INDlciExemptIncomeControl.Size = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncomeControl.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciExemptIncomeControl.Text = "Control Renta Exenta 25%"
        Me.INDlciExemptIncomeControl.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciExemptIncomeControl.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlciExemptIncome25Percent
        '
        Me.INDlciExemptIncome25Percent.Control = Me.INDtxExemptIncome25Percent
        Me.INDlciExemptIncome25Percent.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciExemptIncome25Percent.CustomizationFormText = "Renta Exenta 25%"
        Me.INDlciExemptIncome25Percent.Location = New System.Drawing.Point(0, 128)
        Me.INDlciExemptIncome25Percent.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncome25Percent.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncome25Percent.Name = "INDlciExemptIncome25Percent"
        Me.INDlciExemptIncome25Percent.ShowInCustomizationForm = False
        Me.INDlciExemptIncome25Percent.Size = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncome25Percent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciExemptIncome25Percent.Text = "Renta Exenta 25%"
        Me.INDlciExemptIncome25Percent.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciExemptIncome25Percent.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlciExemptIncomeAndDeductions
        '
        Me.INDlciExemptIncomeAndDeductions.Control = Me.INDtxtExemptIncomeAndDeductions
        Me.INDlciExemptIncomeAndDeductions.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciExemptIncomeAndDeductions.CustomizationFormText = "Total Rentas Exentas y Deducciones"
        Me.INDlciExemptIncomeAndDeductions.Location = New System.Drawing.Point(0, 256)
        Me.INDlciExemptIncomeAndDeductions.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncomeAndDeductions.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncomeAndDeductions.Name = "INDlciExemptIncomeAndDeductions"
        Me.INDlciExemptIncomeAndDeductions.ShowInCustomizationForm = False
        Me.INDlciExemptIncomeAndDeductions.Size = New System.Drawing.Size(390, 64)
        Me.INDlciExemptIncomeAndDeductions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciExemptIncomeAndDeductions.Text = "Total Rentas Exentas y Deducciones"
        Me.INDlciExemptIncomeAndDeductions.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciExemptIncomeAndDeductions.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlciTaxBase
        '
        Me.INDlciTaxBase.Control = Me.INDtxtTaxBase
        Me.INDlciTaxBase.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciTaxBase.CustomizationFormText = "Base Gravable"
        Me.INDlciTaxBase.Location = New System.Drawing.Point(0, 320)
        Me.INDlciTaxBase.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciTaxBase.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciTaxBase.Name = "INDlciTaxBase"
        Me.INDlciTaxBase.ShowInCustomizationForm = False
        Me.INDlciTaxBase.Size = New System.Drawing.Size(520, 64)
        Me.INDlciTaxBase.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciTaxBase.Text = "Base Gravable"
        Me.INDlciTaxBase.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciTaxBase.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlciControlExemptIncomeAndDeductions
        '
        Me.INDlciControlExemptIncomeAndDeductions.Control = Me.INDtxtControlExemptIncomeAndDeductions
        Me.INDlciControlExemptIncomeAndDeductions.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciControlExemptIncomeAndDeductions.CustomizationFormText = "Control Rentas Exentas y Deducciones"
        Me.INDlciControlExemptIncomeAndDeductions.Location = New System.Drawing.Point(0, 192)
        Me.INDlciControlExemptIncomeAndDeductions.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciControlExemptIncomeAndDeductions.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciControlExemptIncomeAndDeductions.Name = "INDlciControlExemptIncomeAndDeductions"
        Me.INDlciControlExemptIncomeAndDeductions.OptionsToolTip.ToolTip = "Límite 1340 UVT anuales"
        Me.INDlciControlExemptIncomeAndDeductions.ShowInCustomizationForm = False
        Me.INDlciControlExemptIncomeAndDeductions.Size = New System.Drawing.Size(390, 64)
        Me.INDlciControlExemptIncomeAndDeductions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciControlExemptIncomeAndDeductions.Text = "Control Rentas Exentas y Deducciones"
        Me.INDlciControlExemptIncomeAndDeductions.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciControlExemptIncomeAndDeductions.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDlciSubtotal
        '
        Me.INDlciSubtotal.Control = Me.INDtxtSubtotal
        Me.INDlciSubtotal.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDlciSubtotal.CustomizationFormText = "Subtotal"
        Me.INDlciSubtotal.Location = New System.Drawing.Point(0, 0)
        Me.INDlciSubtotal.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciSubtotal.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciSubtotal.Name = "INDlciSubtotal"
        Me.INDlciSubtotal.ShowInCustomizationForm = False
        Me.INDlciSubtotal.Size = New System.Drawing.Size(520, 64)
        Me.INDlciSubtotal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciSubtotal.Text = "Subtotal"
        Me.INDlciSubtotal.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciSubtotal.TextSize = New System.Drawing.Size(364, 17)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.INDSbInfoExemptIncomeAndDeductionsControl
        Me.LayoutControlItem10.Location = New System.Drawing.Point(390, 213)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(130, 43)
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(390, 192)
        Me.EmptySpaceItem2.MaxSize = New System.Drawing.Size(0, 21)
        Me.EmptySpaceItem2.MinSize = New System.Drawing.Size(1, 21)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(130, 21)
        Me.EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(390, 128)
        Me.EmptySpaceItem3.MaxSize = New System.Drawing.Size(0, 21)
        Me.EmptySpaceItem3.MinSize = New System.Drawing.Size(1, 21)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(130, 21)
        Me.EmptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.INDSbEditExemptIncome
        Me.LayoutControlItem11.Location = New System.Drawing.Point(390, 149)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(130, 43)
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextVisible = False
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(390, 256)
        Me.EmptySpaceItem4.MaxSize = New System.Drawing.Size(0, 21)
        Me.EmptySpaceItem4.MinSize = New System.Drawing.Size(1, 21)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(130, 21)
        Me.EmptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.INDSbEditExemptIncomeAndDeductions
        Me.LayoutControlItem13.Location = New System.Drawing.Point(390, 277)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(130, 43)
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextVisible = False
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.AllowHotTrack = False
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(390, 64)
        Me.EmptySpaceItem5.MaxSize = New System.Drawing.Size(122, 21)
        Me.EmptySpaceItem5.MinSize = New System.Drawing.Size(122, 21)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(130, 21)
        Me.EmptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDSbInfoExemptIncomeControl
        Me.LayoutControlItem8.Location = New System.Drawing.Point(390, 85)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(130, 43)
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'INDLcgAppyRetentios
        '
        Me.INDLcgAppyRetentios.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAppyRetentios.AppearanceGroup.Options.UseFont = True
        Me.INDLcgAppyRetentios.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAppyRetentios.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgAppyRetentios.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAppyRetentios.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgAppyRetentios.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgAppyRetentios.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgAppyRetentios.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAppyRetentios.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgAppyRetentios.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAppyRetentios.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgAppyRetentios.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAppyRetentios.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgAppyRetentios, False)
        Me.INDLcgAppyRetentios.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciRetArt383, Me.INDLciPreviousDeductions, Me.INDLciWithholdingApplied, Me.LayoutControlItem12, Me.EmptySpaceItem6})
        Me.INDLcgAppyRetentios.Location = New System.Drawing.Point(0, 384)
        Me.INDLcgAppyRetentios.Name = "INDLcgAppyRetentios"
        Me.INDLcgAppyRetentios.Size = New System.Drawing.Size(520, 245)
        Me.INDLcgAppyRetentios.Text = "Retenciones Aplicadas"
        '
        'INDLciRetArt383
        '
        Me.INDLciRetArt383.Control = Me.INDtxtRetArt383
        Me.INDLciRetArt383.Location = New System.Drawing.Point(0, 0)
        Me.INDLciRetArt383.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciRetArt383.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciRetArt383.Name = "INDLciRetArt383"
        Me.INDLciRetArt383.Size = New System.Drawing.Size(496, 64)
        Me.INDLciRetArt383.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRetArt383.Text = "Retención Art. 383"
        Me.INDLciRetArt383.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRetArt383.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDLciPreviousDeductions
        '
        Me.INDLciPreviousDeductions.Control = Me.INDtxtPreviousDeductions
        Me.INDLciPreviousDeductions.Location = New System.Drawing.Point(0, 64)
        Me.INDLciPreviousDeductions.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciPreviousDeductions.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciPreviousDeductions.Name = "INDLciPreviousDeductions"
        Me.INDLciPreviousDeductions.Size = New System.Drawing.Size(390, 64)
        Me.INDLciPreviousDeductions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPreviousDeductions.Text = "Deducción por otras Retenciones en el Mes"
        Me.INDLciPreviousDeductions.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPreviousDeductions.TextSize = New System.Drawing.Size(364, 17)
        '
        'INDLciWithholdingApplied
        '
        Me.INDLciWithholdingApplied.Control = Me.INDtxtWithholdingApplied
        Me.INDLciWithholdingApplied.Location = New System.Drawing.Point(0, 128)
        Me.INDLciWithholdingApplied.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciWithholdingApplied.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciWithholdingApplied.Name = "INDLciWithholdingApplied"
        Me.INDLciWithholdingApplied.Size = New System.Drawing.Size(496, 64)
        Me.INDLciWithholdingApplied.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciWithholdingApplied.Text = "Retención Aplicada"
        Me.INDLciWithholdingApplied.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciWithholdingApplied.TextSize = New System.Drawing.Size(364, 17)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.INDSbPrevoiusRetentions
        Me.LayoutControlItem12.Location = New System.Drawing.Point(390, 85)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(106, 43)
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextVisible = False
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.AllowHotTrack = False
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(390, 64)
        Me.EmptySpaceItem6.MaxSize = New System.Drawing.Size(0, 21)
        Me.EmptySpaceItem6.MinSize = New System.Drawing.Size(1, 21)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(106, 21)
        Me.EmptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddRetention)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 573)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(573, 40)
        Me.PanelControl1.TabIndex = 2
        '
        'INDbtnAddRetention
        '
        Me.INDbtnAddRetention.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddRetention.Location = New System.Drawing.Point(2, 2)
        Me.INDbtnAddRetention.Name = "INDbtnAddRetention"
        Me.INDbtnAddRetention.Size = New System.Drawing.Size(569, 36)
        Me.INDbtnAddRetention.TabIndex = 0
        Me.INDbtnAddRetention.Text = "Agregar"
        '
        'FrmLiquidator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 751)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmLiquidator"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "9999"
        Me.Text = "Liquidador"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyLiquidator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyLiquidator.ResumeLayout(False)
        CType(Me.INDpopupTotalValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpopupTotalValues.ResumeLayout(False)
        CType(Me.INDlyPopupControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPopupControl.ResumeLayout(False)
        CType(Me.INDTxtPopUpNoConstitutivosRenta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtMenosRents.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtBasePopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSubTotalBPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSubTotalAPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDeductionsPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtRentExentPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPaymentMonthPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSubTotal3Popup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtMaxDeductionsAndRentExentsPopup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPaymentMonthPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRentExentPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemBasePopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemMenosRents, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INCLciPopUpNoConstitutivosRenta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSubTotalAPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDeductionsPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSubTotalBPopup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSubTotal3Popup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSubTotal4Popup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspAccountContributions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspVoluntaryContributions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSPPaymentHealthObligatory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspSolidarityPension.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspPensionContribution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspRiskWork.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspDependent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtFeeCommissionServiceData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTotalIncomeExent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTotalDeductions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTotalNoConstitutive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTotalIncomeMonthlyData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPreparedHealth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtInterests.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDspPensionByIndividualSavingsRegime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtExemptIncomeControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxExemptIncome25Percent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtExemptIncomeAndDeductions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtControlExemptIncomeAndDeductions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtTaxBase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSubtotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtAccumulatedIncome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtRetArt383.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPreviousDeductions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtWithholdingApplied.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPaymentsMonthly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalIncomeMonthlyData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAccumulatedIncome, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygRentsExempts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalIncomeExent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPensionByIndividualSavingsRegime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygDeductions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPreparedHealth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTotalDeductionsPartial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInterests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciVoluntaryContributions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgSubtotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciExemptIncomeControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciExemptIncome25Percent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciExemptIncomeAndDeductions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciTaxBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciControlExemptIncomeAndDeductions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciSubtotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgAppyRetentios, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRetArt383, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPreviousDeductions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciWithholdingApplied, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyLiquidator As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDlygPaymentsMonthly As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtTotalIncomeMonthlyData As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemTotalIncomeMonthlyData As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygRentsExempts As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtTotalNoConstitutive As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemTotalIncomeExent As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygDeductions As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemPreparedHealth As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemInterests As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddRetention As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDpopupTotalValues As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDlyPopupControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtPaymentMonthPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemPaymentMonthPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtRentExentPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemRentExentPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDeductionsPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemDeductionsPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtSubTotalAPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemSubTotalAPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtSubTotalBPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemSubTotalBPopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtBasePopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemBasePopup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtMenosRents As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemMenosRents As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtTotalDeductions As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemTotalDeductionsPartial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtPreparedHealth As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDtxtInterests As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtPopUpNoConstitutivosRenta As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INCLciPopUpNoConstitutivosRenta As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtTotalIncomeExent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtSubTotal3Popup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemSubTotal3Popup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtMaxDeductionsAndRentExentsPopup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemSubTotal4Popup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtFeeCommissionServiceData As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspDependent As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspRiskWork As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspPensionContribution As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspSolidarityPension As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSPPaymentHealthObligatory As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspVoluntaryContributions As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciVoluntaryContributions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspAccountContributions As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDspPensionByIndividualSavingsRegime As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlciPensionByIndividualSavingsRegime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtExemptIncomeControl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlcgSubtotal As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciExemptIncomeControl As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxExemptIncome25Percent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciExemptIncome25Percent As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtExemptIncomeAndDeductions As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtControlExemptIncomeAndDeductions As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtTaxBase As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciExemptIncomeAndDeductions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciControlExemptIncomeAndDeductions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciTaxBase As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtSubtotal As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciSubtotal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbInfoExemptIncomeAndDeductionsControl As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSbInfoExemptIncomeControl As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDSbEditExemptIncome As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbEditExemptIncomeAndDeductions As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtAccumulatedIncome As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciAccumulatedIncome As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbInfoIncomeByMonth As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDtxtRetArt383 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcgAppyRetentios As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciRetArt383 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtPreviousDeductions As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciPreviousDeductions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtWithholdingApplied As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciWithholdingApplied As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbPrevoiusRetentions As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
End Class
