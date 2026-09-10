Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEditAutoliquidation
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDDeEndDateIRL = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeInitialIRLInability = New DevExpress.XtraEditors.DateEdit()
        Me.INDMemoObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDDeEndDateSanction = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeInitialDateSanction = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeEndDateVacation = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeInitialDateVacation = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeMaternityEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeMaternityInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeInabilityEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeInabilityInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDTxtICBFValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDSpICBFPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtValueSena = New DevExpress.XtraEditors.TextEdit()
        Me.INDSpSenaPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtValueCompensation = New DevExpress.XtraEditors.TextEdit()
        Me.INDSpCompensationFundPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpProfessionalRiskPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtProfessionalValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtValueHealth = New DevExpress.XtraEditors.TextEdit()
        Me.INDSpHealthPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtValuePension = New DevExpress.XtraEditors.TextEdit()
        Me.INDSpPensionPercentage = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtIBCCompensationFund = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtibcProfessionalRisk = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtIBCHealth = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtIBCPension = New DevExpress.XtraEditors.TextEdit()
        Me.INDSpCompensationFundDays = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpProfessionalRiskDays = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpHealthDays = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpPensionDays = New DevExpress.XtraEditors.SpinEdit()
        Me.INDTxtNameEmployee = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtNitEmployee = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciNit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciNameEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciPensionDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciHealthDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProfessionalRiskDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCompensationFundDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPensionPercentage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValuePension = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciIBCHealth = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LciIBCProfessionalRisk = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProfessionalValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciPercentageProfessional = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciIBCCompensationFund = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgInabilities = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgMaternity = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgVacation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgSanction = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgIRL = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDDeEndDateIRL.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDateIRL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialIRLInability.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialIRLInability.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMemoObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDateSanction.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDateSanction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialDateSanction.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialDateSanction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDateVacation.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDateVacation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialDateVacation.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialDateVacation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeMaternityEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeMaternityEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeMaternityInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeMaternityInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInabilityEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInabilityEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInabilityInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInabilityInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtICBFValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpICBFPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValueSena.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpSenaPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValueCompensation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpCompensationFundPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpProfessionalRiskPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtProfessionalValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValueHealth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpHealthPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValuePension.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpPensionPercentage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtIBCCompensationFund.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtibcProfessionalRisk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtIBCHealth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtIBCPension.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpCompensationFundDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpProfessionalRiskDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpHealthDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpPensionDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtNameEmployee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtNitEmployee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciNameEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPensionDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciHealthDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProfessionalRiskDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCompensationFundDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPensionPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValuePension, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIBCHealth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LciIBCProfessionalRisk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProfessionalValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPercentageProfessional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciIBCCompensationFund, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgInabilities, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMaternity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgVacation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgSanction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgIRL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
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
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDDeEndDateIRL)
        Me.LayoutControl1.Controls.Add(Me.INDDeInitialIRLInability)
        Me.LayoutControl1.Controls.Add(Me.INDMemoObservation)
        Me.LayoutControl1.Controls.Add(Me.INDDeEndDateSanction)
        Me.LayoutControl1.Controls.Add(Me.INDDeInitialDateSanction)
        Me.LayoutControl1.Controls.Add(Me.INDDeEndDateVacation)
        Me.LayoutControl1.Controls.Add(Me.INDDeInitialDateVacation)
        Me.LayoutControl1.Controls.Add(Me.INDDeMaternityEndDate)
        Me.LayoutControl1.Controls.Add(Me.INDDeMaternityInitialDate)
        Me.LayoutControl1.Controls.Add(Me.INDDeInabilityEndDate)
        Me.LayoutControl1.Controls.Add(Me.INDDeInabilityInitialDate)
        Me.LayoutControl1.Controls.Add(Me.INDTxtICBFValue)
        Me.LayoutControl1.Controls.Add(Me.INDSpICBFPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDTxtValueSena)
        Me.LayoutControl1.Controls.Add(Me.INDSpSenaPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDTxtValueCompensation)
        Me.LayoutControl1.Controls.Add(Me.INDSpCompensationFundPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDSpProfessionalRiskPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDTxtProfessionalValue)
        Me.LayoutControl1.Controls.Add(Me.INDTxtValueHealth)
        Me.LayoutControl1.Controls.Add(Me.INDSpHealthPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDTxtValuePension)
        Me.LayoutControl1.Controls.Add(Me.INDSpPensionPercentage)
        Me.LayoutControl1.Controls.Add(Me.INDTxtIBCCompensationFund)
        Me.LayoutControl1.Controls.Add(Me.INDTxtibcProfessionalRisk)
        Me.LayoutControl1.Controls.Add(Me.INDTxtIBCHealth)
        Me.LayoutControl1.Controls.Add(Me.INDTxtIBCPension)
        Me.LayoutControl1.Controls.Add(Me.INDSpCompensationFundDays)
        Me.LayoutControl1.Controls.Add(Me.INDSpProfessionalRiskDays)
        Me.LayoutControl1.Controls.Add(Me.INDSpHealthDays)
        Me.LayoutControl1.Controls.Add(Me.INDSpPensionDays)
        Me.LayoutControl1.Controls.Add(Me.INDTxtNameEmployee)
        Me.LayoutControl1.Controls.Add(Me.INDTxtNitEmployee)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1261, 570)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDDeEndDateIRL
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeEndDateIRL, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeEndDateIRL, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeEndDateIRL, False)
        Me.INDDeEndDateIRL.EditValue = Nothing
        Me.INDDeEndDateIRL.EnterMoveNextControl = True
        Me.INDDeEndDateIRL.Location = New System.Drawing.Point(3862, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeEndDateIRL, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeEndDateIRL, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeEndDateIRL.Name = "INDDeEndDateIRL"
        Me.INDDeEndDateIRL.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeEndDateIRL.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDateIRL.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeEndDateIRL.Properties.Appearance.Options.UseFont = True
        Me.INDDeEndDateIRL.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDateIRL.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeEndDateIRL.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDateIRL.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDateIRL.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeEndDateIRL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeEndDateIRL.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeEndDateIRL.Size = New System.Drawing.Size(386, 28)
        Me.INDDeEndDateIRL.StyleController = Me.LayoutControl1
        Me.INDDeEndDateIRL.TabIndex = 37
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeEndDateIRL, 0)
        '
        'INDDeInitialIRLInability
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeInitialIRLInability, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeInitialIRLInability, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeInitialIRLInability, False)
        Me.INDDeInitialIRLInability.EditValue = Nothing
        Me.INDDeInitialIRLInability.EnterMoveNextControl = True
        Me.INDDeInitialIRLInability.Location = New System.Drawing.Point(3862, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeInitialIRLInability, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeInitialIRLInability, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeInitialIRLInability.Name = "INDDeInitialIRLInability"
        Me.INDDeInitialIRLInability.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeInitialIRLInability.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInitialIRLInability.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeInitialIRLInability.Properties.Appearance.Options.UseFont = True
        Me.INDDeInitialIRLInability.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInitialIRLInability.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeInitialIRLInability.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialIRLInability.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialIRLInability.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeInitialIRLInability.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeInitialIRLInability.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeInitialIRLInability.Size = New System.Drawing.Size(386, 28)
        Me.INDDeInitialIRLInability.StyleController = Me.LayoutControl1
        Me.INDDeInitialIRLInability.TabIndex = 36
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeInitialIRLInability, 0)
        '
        'INDMemoObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMemoObservation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMemoObservation, False)
        Me.INDMemoObservation.EnterMoveNextControl = True
        Me.INDMemoObservation.Location = New System.Drawing.Point(-188, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDMemoObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMemoObservation.Name = "INDMemoObservation"
        Me.INDMemoObservation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMemoObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMemoObservation.Size = New System.Drawing.Size(386, 156)
        Me.INDMemoObservation.StyleController = Me.LayoutControl1
        Me.INDMemoObservation.TabIndex = 35
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMemoObservation, 0)
        '
        'INDDeEndDateSanction
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeEndDateSanction, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeEndDateSanction, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeEndDateSanction, False)
        Me.INDDeEndDateSanction.EditValue = Nothing
        Me.INDDeEndDateSanction.EnterMoveNextControl = True
        Me.INDDeEndDateSanction.Location = New System.Drawing.Point(3448, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeEndDateSanction, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeEndDateSanction, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeEndDateSanction.Name = "INDDeEndDateSanction"
        Me.INDDeEndDateSanction.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeEndDateSanction.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDateSanction.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeEndDateSanction.Properties.Appearance.Options.UseFont = True
        Me.INDDeEndDateSanction.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDateSanction.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeEndDateSanction.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDateSanction.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDateSanction.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeEndDateSanction.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeEndDateSanction.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeEndDateSanction.Size = New System.Drawing.Size(386, 28)
        Me.INDDeEndDateSanction.StyleController = Me.LayoutControl1
        Me.INDDeEndDateSanction.TabIndex = 34
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeEndDateSanction, 0)
        '
        'INDDeInitialDateSanction
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeInitialDateSanction, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeInitialDateSanction, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeInitialDateSanction, False)
        Me.INDDeInitialDateSanction.EditValue = Nothing
        Me.INDDeInitialDateSanction.EnterMoveNextControl = True
        Me.INDDeInitialDateSanction.Location = New System.Drawing.Point(3448, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeInitialDateSanction, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeInitialDateSanction, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeInitialDateSanction.Name = "INDDeInitialDateSanction"
        Me.INDDeInitialDateSanction.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeInitialDateSanction.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInitialDateSanction.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeInitialDateSanction.Properties.Appearance.Options.UseFont = True
        Me.INDDeInitialDateSanction.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInitialDateSanction.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeInitialDateSanction.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialDateSanction.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialDateSanction.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeInitialDateSanction.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeInitialDateSanction.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeInitialDateSanction.Size = New System.Drawing.Size(386, 28)
        Me.INDDeInitialDateSanction.StyleController = Me.LayoutControl1
        Me.INDDeInitialDateSanction.TabIndex = 33
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeInitialDateSanction, 0)
        '
        'INDDeEndDateVacation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeEndDateVacation, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeEndDateVacation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeEndDateVacation, False)
        Me.INDDeEndDateVacation.EditValue = Nothing
        Me.INDDeEndDateVacation.EnterMoveNextControl = True
        Me.INDDeEndDateVacation.Location = New System.Drawing.Point(3034, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeEndDateVacation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeEndDateVacation, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeEndDateVacation.Name = "INDDeEndDateVacation"
        Me.INDDeEndDateVacation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeEndDateVacation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDateVacation.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeEndDateVacation.Properties.Appearance.Options.UseFont = True
        Me.INDDeEndDateVacation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeEndDateVacation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeEndDateVacation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDateVacation.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDateVacation.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeEndDateVacation.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeEndDateVacation.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeEndDateVacation.Size = New System.Drawing.Size(386, 28)
        Me.INDDeEndDateVacation.StyleController = Me.LayoutControl1
        Me.INDDeEndDateVacation.TabIndex = 32
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeEndDateVacation, 0)
        '
        'INDDeInitialDateVacation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeInitialDateVacation, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeInitialDateVacation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeInitialDateVacation, False)
        Me.INDDeInitialDateVacation.EditValue = Nothing
        Me.INDDeInitialDateVacation.EnterMoveNextControl = True
        Me.INDDeInitialDateVacation.Location = New System.Drawing.Point(3034, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeInitialDateVacation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeInitialDateVacation, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeInitialDateVacation.Name = "INDDeInitialDateVacation"
        Me.INDDeInitialDateVacation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeInitialDateVacation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInitialDateVacation.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeInitialDateVacation.Properties.Appearance.Options.UseFont = True
        Me.INDDeInitialDateVacation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInitialDateVacation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeInitialDateVacation.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialDateVacation.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialDateVacation.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeInitialDateVacation.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeInitialDateVacation.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeInitialDateVacation.Size = New System.Drawing.Size(386, 28)
        Me.INDDeInitialDateVacation.StyleController = Me.LayoutControl1
        Me.INDDeInitialDateVacation.TabIndex = 31
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeInitialDateVacation, 0)
        '
        'INDDeMaternityEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeMaternityEndDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeMaternityEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeMaternityEndDate, False)
        Me.INDDeMaternityEndDate.EditValue = Nothing
        Me.INDDeMaternityEndDate.EnterMoveNextControl = True
        Me.INDDeMaternityEndDate.Location = New System.Drawing.Point(2620, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeMaternityEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeMaternityEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeMaternityEndDate.Name = "INDDeMaternityEndDate"
        Me.INDDeMaternityEndDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeMaternityEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeMaternityEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeMaternityEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeMaternityEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeMaternityEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeMaternityEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeMaternityEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeMaternityEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeMaternityEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeMaternityEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeMaternityEndDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeMaternityEndDate.StyleController = Me.LayoutControl1
        Me.INDDeMaternityEndDate.TabIndex = 30
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeMaternityEndDate, 0)
        '
        'INDDeMaternityInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeMaternityInitialDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeMaternityInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeMaternityInitialDate, False)
        Me.INDDeMaternityInitialDate.EditValue = Nothing
        Me.INDDeMaternityInitialDate.EnterMoveNextControl = True
        Me.INDDeMaternityInitialDate.Location = New System.Drawing.Point(2620, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeMaternityInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeMaternityInitialDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeMaternityInitialDate.Name = "INDDeMaternityInitialDate"
        Me.INDDeMaternityInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeMaternityInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeMaternityInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeMaternityInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeMaternityInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeMaternityInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeMaternityInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeMaternityInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeMaternityInitialDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeMaternityInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeMaternityInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeMaternityInitialDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeMaternityInitialDate.StyleController = Me.LayoutControl1
        Me.INDDeMaternityInitialDate.TabIndex = 29
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeMaternityInitialDate, 0)
        '
        'INDDeInabilityEndDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeInabilityEndDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeInabilityEndDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeInabilityEndDate, False)
        Me.INDDeInabilityEndDate.EditValue = Nothing
        Me.INDDeInabilityEndDate.EnterMoveNextControl = True
        Me.INDDeInabilityEndDate.Location = New System.Drawing.Point(2206, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeInabilityEndDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeInabilityEndDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeInabilityEndDate.Name = "INDDeInabilityEndDate"
        Me.INDDeInabilityEndDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeInabilityEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInabilityEndDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeInabilityEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeInabilityEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInabilityEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeInabilityEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInabilityEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInabilityEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeInabilityEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeInabilityEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeInabilityEndDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeInabilityEndDate.StyleController = Me.LayoutControl1
        Me.INDDeInabilityEndDate.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeInabilityEndDate, 0)
        '
        'INDDeInabilityInitialDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeInabilityInitialDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeInabilityInitialDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeInabilityInitialDate, False)
        Me.INDDeInabilityInitialDate.EditValue = Nothing
        Me.INDDeInabilityInitialDate.EnterMoveNextControl = True
        Me.INDDeInabilityInitialDate.Location = New System.Drawing.Point(2206, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeInabilityInitialDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeInabilityInitialDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeInabilityInitialDate.Name = "INDDeInabilityInitialDate"
        Me.INDDeInabilityInitialDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeInabilityInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInabilityInitialDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeInabilityInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeInabilityInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeInabilityInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeInabilityInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInabilityInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInabilityInitialDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeInabilityInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeInabilityInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeInabilityInitialDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeInabilityInitialDate.StyleController = Me.LayoutControl1
        Me.INDDeInabilityInitialDate.TabIndex = 27
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeInabilityInitialDate, 0)
        '
        'INDTxtICBFValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtICBFValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtICBFValue, False)
        Me.INDTxtICBFValue.EnterMoveNextControl = True
        Me.INDTxtICBFValue.Location = New System.Drawing.Point(1792, 439)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtICBFValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtICBFValue.Name = "INDTxtICBFValue"
        Me.INDTxtICBFValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtICBFValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtICBFValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtICBFValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtICBFValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtICBFValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtICBFValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtICBFValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtICBFValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtICBFValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtICBFValue.StyleController = Me.LayoutControl1
        Me.INDTxtICBFValue.TabIndex = 26
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtICBFValue, 0)
        '
        'INDSpICBFPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpICBFPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpICBFPercentage, False)
        Me.INDSpICBFPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpICBFPercentage.EnterMoveNextControl = True
        Me.INDSpICBFPercentage.Location = New System.Drawing.Point(1792, 379)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpICBFPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpICBFPercentage.Name = "INDSpICBFPercentage"
        Me.INDSpICBFPercentage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpICBFPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpICBFPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpICBFPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpICBFPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpICBFPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpICBFPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpICBFPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDSpICBFPercentage.StyleController = Me.LayoutControl1
        Me.INDSpICBFPercentage.TabIndex = 25
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpICBFPercentage, 0)
        '
        'INDTxtValueSena
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValueSena, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValueSena, False)
        Me.INDTxtValueSena.EnterMoveNextControl = True
        Me.INDTxtValueSena.Location = New System.Drawing.Point(1792, 319)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValueSena, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtValueSena.Name = "INDTxtValueSena"
        Me.INDTxtValueSena.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValueSena.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValueSena.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValueSena.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValueSena.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValueSena.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValueSena.Properties.Mask.EditMask = "c0"
        Me.INDTxtValueSena.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtValueSena.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtValueSena.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtValueSena.StyleController = Me.LayoutControl1
        Me.INDTxtValueSena.TabIndex = 24
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValueSena, 0)
        '
        'INDSpSenaPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpSenaPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpSenaPercentage, False)
        Me.INDSpSenaPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpSenaPercentage.EnterMoveNextControl = True
        Me.INDSpSenaPercentage.Location = New System.Drawing.Point(1792, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpSenaPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpSenaPercentage.Name = "INDSpSenaPercentage"
        Me.INDSpSenaPercentage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpSenaPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpSenaPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpSenaPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpSenaPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpSenaPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpSenaPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpSenaPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDSpSenaPercentage.StyleController = Me.LayoutControl1
        Me.INDSpSenaPercentage.TabIndex = 23
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpSenaPercentage, 0)
        '
        'INDTxtValueCompensation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValueCompensation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValueCompensation, False)
        Me.INDTxtValueCompensation.EnterMoveNextControl = True
        Me.INDTxtValueCompensation.Location = New System.Drawing.Point(1792, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValueCompensation, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtValueCompensation.Name = "INDTxtValueCompensation"
        Me.INDTxtValueCompensation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValueCompensation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValueCompensation.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValueCompensation.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValueCompensation.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValueCompensation.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValueCompensation.Properties.Mask.EditMask = "c0"
        Me.INDTxtValueCompensation.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtValueCompensation.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtValueCompensation.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtValueCompensation.StyleController = Me.LayoutControl1
        Me.INDTxtValueCompensation.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValueCompensation, 0)
        '
        'INDSpCompensationFundPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpCompensationFundPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpCompensationFundPercentage, False)
        Me.INDSpCompensationFundPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpCompensationFundPercentage.EnterMoveNextControl = True
        Me.INDSpCompensationFundPercentage.Location = New System.Drawing.Point(1792, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpCompensationFundPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpCompensationFundPercentage.Name = "INDSpCompensationFundPercentage"
        Me.INDSpCompensationFundPercentage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpCompensationFundPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpCompensationFundPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpCompensationFundPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpCompensationFundPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpCompensationFundPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpCompensationFundPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpCompensationFundPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDSpCompensationFundPercentage.StyleController = Me.LayoutControl1
        Me.INDSpCompensationFundPercentage.TabIndex = 21
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpCompensationFundPercentage, 0)
        '
        'INDSpProfessionalRiskPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpProfessionalRiskPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpProfessionalRiskPercentage, False)
        Me.INDSpProfessionalRiskPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpProfessionalRiskPercentage.EnterMoveNextControl = True
        Me.INDSpProfessionalRiskPercentage.Location = New System.Drawing.Point(1378, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpProfessionalRiskPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpProfessionalRiskPercentage.Name = "INDSpProfessionalRiskPercentage"
        Me.INDSpProfessionalRiskPercentage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpProfessionalRiskPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpProfessionalRiskPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpProfessionalRiskPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpProfessionalRiskPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpProfessionalRiskPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpProfessionalRiskPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpProfessionalRiskPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDSpProfessionalRiskPercentage.StyleController = Me.LayoutControl1
        Me.INDSpProfessionalRiskPercentage.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpProfessionalRiskPercentage, 0)
        '
        'INDTxtProfessionalValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtProfessionalValue, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtProfessionalValue, False)
        Me.INDTxtProfessionalValue.EnterMoveNextControl = True
        Me.INDTxtProfessionalValue.Location = New System.Drawing.Point(1378, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtProfessionalValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtProfessionalValue.Name = "INDTxtProfessionalValue"
        Me.INDTxtProfessionalValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtProfessionalValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtProfessionalValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtProfessionalValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxtProfessionalValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtProfessionalValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtProfessionalValue.Properties.Mask.EditMask = "c0"
        Me.INDTxtProfessionalValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtProfessionalValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtProfessionalValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtProfessionalValue.StyleController = Me.LayoutControl1
        Me.INDTxtProfessionalValue.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtProfessionalValue, 0)
        '
        'INDTxtValueHealth
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValueHealth, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValueHealth, False)
        Me.INDTxtValueHealth.EnterMoveNextControl = True
        Me.INDTxtValueHealth.Location = New System.Drawing.Point(964, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValueHealth, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtValueHealth.Name = "INDTxtValueHealth"
        Me.INDTxtValueHealth.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValueHealth.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValueHealth.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValueHealth.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValueHealth.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValueHealth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValueHealth.Properties.Mask.EditMask = "c0"
        Me.INDTxtValueHealth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtValueHealth.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtValueHealth.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtValueHealth.StyleController = Me.LayoutControl1
        Me.INDTxtValueHealth.TabIndex = 18
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValueHealth, 0)
        '
        'INDSpHealthPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpHealthPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpHealthPercentage, False)
        Me.INDSpHealthPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpHealthPercentage.EnterMoveNextControl = True
        Me.INDSpHealthPercentage.Location = New System.Drawing.Point(964, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpHealthPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpHealthPercentage.Name = "INDSpHealthPercentage"
        Me.INDSpHealthPercentage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpHealthPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpHealthPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpHealthPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpHealthPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpHealthPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpHealthPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpHealthPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDSpHealthPercentage.StyleController = Me.LayoutControl1
        Me.INDSpHealthPercentage.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpHealthPercentage, 0)
        '
        'INDTxtValuePension
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValuePension, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValuePension, False)
        Me.INDTxtValuePension.EnterMoveNextControl = True
        Me.INDTxtValuePension.Location = New System.Drawing.Point(550, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValuePension, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtValuePension.Name = "INDTxtValuePension"
        Me.INDTxtValuePension.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValuePension.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtValuePension.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValuePension.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValuePension.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValuePension.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValuePension.Properties.Mask.EditMask = "c0"
        Me.INDTxtValuePension.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtValuePension.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtValuePension.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtValuePension.StyleController = Me.LayoutControl1
        Me.INDTxtValuePension.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValuePension, 0)
        '
        'INDSpPensionPercentage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpPensionPercentage, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpPensionPercentage, False)
        Me.INDSpPensionPercentage.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpPensionPercentage.EnterMoveNextControl = True
        Me.INDSpPensionPercentage.Location = New System.Drawing.Point(550, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpPensionPercentage, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDSpPensionPercentage.Name = "INDSpPensionPercentage"
        Me.INDSpPensionPercentage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpPensionPercentage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpPensionPercentage.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpPensionPercentage.Properties.Appearance.Options.UseFont = True
        Me.INDSpPensionPercentage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpPensionPercentage.Properties.Mask.EditMask = "P"
        Me.INDSpPensionPercentage.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpPensionPercentage.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.INDSpPensionPercentage.Size = New System.Drawing.Size(386, 28)
        Me.INDSpPensionPercentage.StyleController = Me.LayoutControl1
        Me.INDSpPensionPercentage.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpPensionPercentage, 0)
        '
        'INDTxtIBCCompensationFund
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtIBCCompensationFund, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtIBCCompensationFund, False)
        Me.INDTxtIBCCompensationFund.EnterMoveNextControl = True
        Me.INDTxtIBCCompensationFund.Location = New System.Drawing.Point(1792, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtIBCCompensationFund, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtIBCCompensationFund.Name = "INDTxtIBCCompensationFund"
        Me.INDTxtIBCCompensationFund.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtIBCCompensationFund.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtIBCCompensationFund.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtIBCCompensationFund.Properties.Appearance.Options.UseFont = True
        Me.INDTxtIBCCompensationFund.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtIBCCompensationFund.StyleController = Me.LayoutControl1
        Me.INDTxtIBCCompensationFund.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtIBCCompensationFund, 0)
        '
        'INDTxtibcProfessionalRisk
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtibcProfessionalRisk, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtibcProfessionalRisk, False)
        Me.INDTxtibcProfessionalRisk.EnterMoveNextControl = True
        Me.INDTxtibcProfessionalRisk.Location = New System.Drawing.Point(1378, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtibcProfessionalRisk, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtibcProfessionalRisk.Name = "INDTxtibcProfessionalRisk"
        Me.INDTxtibcProfessionalRisk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtibcProfessionalRisk.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtibcProfessionalRisk.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtibcProfessionalRisk.Properties.Appearance.Options.UseFont = True
        Me.INDTxtibcProfessionalRisk.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtibcProfessionalRisk.StyleController = Me.LayoutControl1
        Me.INDTxtibcProfessionalRisk.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtibcProfessionalRisk, 0)
        '
        'INDTxtIBCHealth
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtIBCHealth, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtIBCHealth, False)
        Me.INDTxtIBCHealth.EnterMoveNextControl = True
        Me.INDTxtIBCHealth.Location = New System.Drawing.Point(964, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtIBCHealth, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtIBCHealth.Name = "INDTxtIBCHealth"
        Me.INDTxtIBCHealth.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtIBCHealth.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtIBCHealth.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtIBCHealth.Properties.Appearance.Options.UseFont = True
        Me.INDTxtIBCHealth.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtIBCHealth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtIBCHealth.Properties.Mask.EditMask = "c0"
        Me.INDTxtIBCHealth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtIBCHealth.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtIBCHealth.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtIBCHealth.StyleController = Me.LayoutControl1
        Me.INDTxtIBCHealth.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtIBCHealth, 0)
        '
        'INDTxtIBCPension
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtIBCPension, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtIBCPension, False)
        Me.INDTxtIBCPension.EnterMoveNextControl = True
        Me.INDTxtIBCPension.Location = New System.Drawing.Point(550, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtIBCPension, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDTxtIBCPension.Name = "INDTxtIBCPension"
        Me.INDTxtIBCPension.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtIBCPension.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtIBCPension.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtIBCPension.Properties.Appearance.Options.UseFont = True
        Me.INDTxtIBCPension.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtIBCPension.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtIBCPension.Properties.Mask.EditMask = "c0"
        Me.INDTxtIBCPension.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxtIBCPension.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxtIBCPension.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtIBCPension.StyleController = Me.LayoutControl1
        Me.INDTxtIBCPension.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtIBCPension, 0)
        '
        'INDSpCompensationFundDays
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpCompensationFundDays, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpCompensationFundDays, False)
        Me.INDSpCompensationFundDays.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpCompensationFundDays.EnterMoveNextControl = True
        Me.INDSpCompensationFundDays.Location = New System.Drawing.Point(226, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpCompensationFundDays, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSpCompensationFundDays.Name = "INDSpCompensationFundDays"
        Me.INDSpCompensationFundDays.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpCompensationFundDays.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpCompensationFundDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpCompensationFundDays.Properties.Appearance.Options.UseFont = True
        Me.INDSpCompensationFundDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpCompensationFundDays.Size = New System.Drawing.Size(296, 28)
        Me.INDSpCompensationFundDays.StyleController = Me.LayoutControl1
        Me.INDSpCompensationFundDays.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpCompensationFundDays, 0)
        '
        'INDSpProfessionalRiskDays
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpProfessionalRiskDays, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpProfessionalRiskDays, False)
        Me.INDSpProfessionalRiskDays.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpProfessionalRiskDays.EnterMoveNextControl = True
        Me.INDSpProfessionalRiskDays.Location = New System.Drawing.Point(226, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpProfessionalRiskDays, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSpProfessionalRiskDays.Name = "INDSpProfessionalRiskDays"
        Me.INDSpProfessionalRiskDays.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpProfessionalRiskDays.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpProfessionalRiskDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpProfessionalRiskDays.Properties.Appearance.Options.UseFont = True
        Me.INDSpProfessionalRiskDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpProfessionalRiskDays.Size = New System.Drawing.Size(296, 28)
        Me.INDSpProfessionalRiskDays.StyleController = Me.LayoutControl1
        Me.INDSpProfessionalRiskDays.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpProfessionalRiskDays, 0)
        '
        'INDSpHealthDays
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpHealthDays, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpHealthDays, False)
        Me.INDSpHealthDays.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpHealthDays.EnterMoveNextControl = True
        Me.INDSpHealthDays.Location = New System.Drawing.Point(226, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpHealthDays, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSpHealthDays.Name = "INDSpHealthDays"
        Me.INDSpHealthDays.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpHealthDays.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpHealthDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpHealthDays.Properties.Appearance.Options.UseFont = True
        Me.INDSpHealthDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpHealthDays.Properties.MaxValue = New Decimal(New Integer() {30, 0, 0, 0})
        Me.INDSpHealthDays.Size = New System.Drawing.Size(296, 28)
        Me.INDSpHealthDays.StyleController = Me.LayoutControl1
        Me.INDSpHealthDays.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpHealthDays, 0)
        '
        'INDSpPensionDays
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpPensionDays, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpPensionDays, False)
        Me.INDSpPensionDays.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpPensionDays.EnterMoveNextControl = True
        Me.INDSpPensionDays.Location = New System.Drawing.Point(226, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpPensionDays, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSpPensionDays.Name = "INDSpPensionDays"
        Me.INDSpPensionDays.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpPensionDays.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpPensionDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpPensionDays.Properties.Appearance.Options.UseFont = True
        Me.INDSpPensionDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpPensionDays.Properties.MaxValue = New Decimal(New Integer() {30, 0, 0, 0})
        Me.INDSpPensionDays.Size = New System.Drawing.Size(296, 28)
        Me.INDSpPensionDays.StyleController = Me.LayoutControl1
        Me.INDSpPensionDays.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpPensionDays, 0)
        '
        'INDTxtNameEmployee
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtNameEmployee, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtNameEmployee, False)
        Me.INDTxtNameEmployee.Location = New System.Drawing.Point(-188, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtNameEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtNameEmployee.Name = "INDTxtNameEmployee"
        Me.INDTxtNameEmployee.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtNameEmployee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtNameEmployee.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtNameEmployee.Properties.Appearance.Options.UseFont = True
        Me.INDTxtNameEmployee.Properties.ReadOnly = True
        Me.INDTxtNameEmployee.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtNameEmployee.StyleController = Me.LayoutControl1
        Me.INDTxtNameEmployee.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtNameEmployee, 0)
        '
        'INDTxtNitEmployee
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtNitEmployee, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtNitEmployee, False)
        Me.INDTxtNitEmployee.Location = New System.Drawing.Point(-188, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtNitEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtNitEmployee.Name = "INDTxtNitEmployee"
        Me.INDTxtNitEmployee.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtNitEmployee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtNitEmployee.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtNitEmployee.Properties.Appearance.Options.UseFont = True
        Me.INDTxtNitEmployee.Properties.ReadOnly = True
        Me.INDTxtNitEmployee.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtNitEmployee.StyleController = Me.LayoutControl1
        Me.INDTxtNitEmployee.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtNitEmployee, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3, Me.LayoutControlGroup4, Me.LayoutControlGroup5, Me.LayoutControlGroup6, Me.LayoutControlGroup7, Me.INDLcgInabilities, Me.INDLcgMaternity, Me.INDLcgVacation, Me.INDLcgSanction, Me.INDLcgIRL})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(-212, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(4484, 553)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciNit, Me.INDLciNameEmployee, Me.INDLciObservation})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(414, 533)
        Me.LayoutControlGroup2.Text = "Datos Básicos"
        '
        'INDLciNit
        '
        Me.INDLciNit.Control = Me.INDTxtNitEmployee
        Me.INDLciNit.Location = New System.Drawing.Point(0, 0)
        Me.INDLciNit.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciNit.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciNit.Name = "INDLciNit"
        Me.INDLciNit.Size = New System.Drawing.Size(390, 60)
        Me.INDLciNit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNit.Text = "Cédula"
        Me.INDLciNit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNit.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciNameEmployee
        '
        Me.INDLciNameEmployee.Control = Me.INDTxtNameEmployee
        Me.INDLciNameEmployee.Location = New System.Drawing.Point(0, 60)
        Me.INDLciNameEmployee.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciNameEmployee.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciNameEmployee.Name = "INDLciNameEmployee"
        Me.INDLciNameEmployee.Size = New System.Drawing.Size(390, 60)
        Me.INDLciNameEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciNameEmployee.Text = "Nombre"
        Me.INDLciNameEmployee.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciNameEmployee.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciObservation
        '
        Me.INDLciObservation.Control = Me.INDMemoObservation
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 120)
        Me.INDLciObservation.MaxSize = New System.Drawing.Size(390, 180)
        Me.INDLciObservation.MinSize = New System.Drawing.Size(390, 180)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Size = New System.Drawing.Size(390, 354)
        Me.INDLciObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservation.Text = "Observaciones"
        Me.INDLciObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservation.TextSize = New System.Drawing.Size(211, 17)
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
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciPensionDays, Me.INDLciHealthDays, Me.INDLciProfessionalRiskDays, Me.INDLciCompensationFundDays})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(324, 533)
        Me.LayoutControlGroup3.Text = "Días"
        '
        'INDLciPensionDays
        '
        Me.INDLciPensionDays.Control = Me.INDSpPensionDays
        Me.INDLciPensionDays.Location = New System.Drawing.Point(0, 0)
        Me.INDLciPensionDays.MaxSize = New System.Drawing.Size(300, 60)
        Me.INDLciPensionDays.MinSize = New System.Drawing.Size(300, 60)
        Me.INDLciPensionDays.Name = "INDLciPensionDays"
        Me.INDLciPensionDays.Size = New System.Drawing.Size(300, 60)
        Me.INDLciPensionDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPensionDays.Text = "Días Pensión"
        Me.INDLciPensionDays.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPensionDays.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciHealthDays
        '
        Me.INDLciHealthDays.Control = Me.INDSpHealthDays
        Me.INDLciHealthDays.Location = New System.Drawing.Point(0, 60)
        Me.INDLciHealthDays.MaxSize = New System.Drawing.Size(300, 60)
        Me.INDLciHealthDays.MinSize = New System.Drawing.Size(300, 60)
        Me.INDLciHealthDays.Name = "INDLciHealthDays"
        Me.INDLciHealthDays.Size = New System.Drawing.Size(300, 60)
        Me.INDLciHealthDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciHealthDays.Text = "Días Salud"
        Me.INDLciHealthDays.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciHealthDays.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciProfessionalRiskDays
        '
        Me.INDLciProfessionalRiskDays.Control = Me.INDSpProfessionalRiskDays
        Me.INDLciProfessionalRiskDays.Location = New System.Drawing.Point(0, 120)
        Me.INDLciProfessionalRiskDays.MaxSize = New System.Drawing.Size(300, 60)
        Me.INDLciProfessionalRiskDays.MinSize = New System.Drawing.Size(300, 60)
        Me.INDLciProfessionalRiskDays.Name = "INDLciProfessionalRiskDays"
        Me.INDLciProfessionalRiskDays.Size = New System.Drawing.Size(300, 60)
        Me.INDLciProfessionalRiskDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProfessionalRiskDays.Text = "Días Riesgos Profesionales"
        Me.INDLciProfessionalRiskDays.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProfessionalRiskDays.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciCompensationFundDays
        '
        Me.INDLciCompensationFundDays.Control = Me.INDSpCompensationFundDays
        Me.INDLciCompensationFundDays.Location = New System.Drawing.Point(0, 180)
        Me.INDLciCompensationFundDays.MaxSize = New System.Drawing.Size(300, 60)
        Me.INDLciCompensationFundDays.MinSize = New System.Drawing.Size(300, 60)
        Me.INDLciCompensationFundDays.Name = "INDLciCompensationFundDays"
        Me.INDLciCompensationFundDays.Size = New System.Drawing.Size(300, 294)
        Me.INDLciCompensationFundDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCompensationFundDays.Text = "Días Cajas Compensación"
        Me.INDLciCompensationFundDays.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCompensationFundDays.TextSize = New System.Drawing.Size(211, 17)
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
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDLciPensionPercentage, Me.INDLciValuePension})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(738, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(414, 533)
        Me.LayoutControlGroup4.Text = "Pensión"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDTxtIBCPension
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "IBC Pensión"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciPensionPercentage
        '
        Me.INDLciPensionPercentage.Control = Me.INDSpPensionPercentage
        Me.INDLciPensionPercentage.Location = New System.Drawing.Point(0, 60)
        Me.INDLciPensionPercentage.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciPensionPercentage.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPensionPercentage.Name = "INDLciPensionPercentage"
        Me.INDLciPensionPercentage.Size = New System.Drawing.Size(390, 60)
        Me.INDLciPensionPercentage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPensionPercentage.Text = "% Pensión"
        Me.INDLciPensionPercentage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPensionPercentage.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciValuePension
        '
        Me.INDLciValuePension.Control = Me.INDTxtValuePension
        Me.INDLciValuePension.Location = New System.Drawing.Point(0, 120)
        Me.INDLciValuePension.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciValuePension.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciValuePension.Name = "INDLciValuePension"
        Me.INDLciValuePension.Size = New System.Drawing.Size(390, 354)
        Me.INDLciValuePension.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValuePension.Text = "Valor Pensión"
        Me.INDLciValuePension.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValuePension.TextSize = New System.Drawing.Size(211, 17)
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
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciIBCHealth, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(1152, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(414, 533)
        Me.LayoutControlGroup5.Text = "Salud"
        '
        'INDLciIBCHealth
        '
        Me.INDLciIBCHealth.Control = Me.INDTxtIBCHealth
        Me.INDLciIBCHealth.Location = New System.Drawing.Point(0, 0)
        Me.INDLciIBCHealth.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciIBCHealth.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciIBCHealth.Name = "INDLciIBCHealth"
        Me.INDLciIBCHealth.Size = New System.Drawing.Size(390, 60)
        Me.INDLciIBCHealth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIBCHealth.Text = "IBC Salud"
        Me.INDLciIBCHealth.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIBCHealth.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSpHealthPercentage
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "% Salud"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDTxtValueHealth
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(390, 354)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Valor Salud"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(211, 17)
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
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LciIBCProfessionalRisk, Me.INDLciProfessionalValue, Me.INDLciPercentageProfessional})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(1566, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(414, 533)
        Me.LayoutControlGroup6.Text = "Riesgos"
        '
        'LciIBCProfessionalRisk
        '
        Me.LciIBCProfessionalRisk.Control = Me.INDTxtibcProfessionalRisk
        Me.LciIBCProfessionalRisk.Location = New System.Drawing.Point(0, 0)
        Me.LciIBCProfessionalRisk.MaxSize = New System.Drawing.Size(390, 60)
        Me.LciIBCProfessionalRisk.MinSize = New System.Drawing.Size(390, 60)
        Me.LciIBCProfessionalRisk.Name = "LciIBCProfessionalRisk"
        Me.LciIBCProfessionalRisk.Size = New System.Drawing.Size(390, 60)
        Me.LciIBCProfessionalRisk.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LciIBCProfessionalRisk.Text = "IBC Riesgos Profesionales"
        Me.LciIBCProfessionalRisk.TextLocation = DevExpress.Utils.Locations.Top
        Me.LciIBCProfessionalRisk.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciProfessionalValue
        '
        Me.INDLciProfessionalValue.Control = Me.INDTxtProfessionalValue
        Me.INDLciProfessionalValue.Location = New System.Drawing.Point(0, 120)
        Me.INDLciProfessionalValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciProfessionalValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciProfessionalValue.Name = "INDLciProfessionalValue"
        Me.INDLciProfessionalValue.Size = New System.Drawing.Size(390, 354)
        Me.INDLciProfessionalValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProfessionalValue.Text = "Valor Riesgos Profesionales"
        Me.INDLciProfessionalValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProfessionalValue.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLciPercentageProfessional
        '
        Me.INDLciPercentageProfessional.Control = Me.INDSpProfessionalRiskPercentage
        Me.INDLciPercentageProfessional.Location = New System.Drawing.Point(0, 60)
        Me.INDLciPercentageProfessional.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciPercentageProfessional.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciPercentageProfessional.Name = "INDLciPercentageProfessional"
        Me.INDLciPercentageProfessional.Size = New System.Drawing.Size(390, 60)
        Me.INDLciPercentageProfessional.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPercentageProfessional.Text = "% Riesgos Profesionales"
        Me.INDLciPercentageProfessional.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPercentageProfessional.TextSize = New System.Drawing.Size(211, 17)
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
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciIBCCompensationFund, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem9})
        Me.LayoutControlGroup7.Location = New System.Drawing.Point(1980, 0)
        Me.LayoutControlGroup7.Name = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(414, 533)
        Me.LayoutControlGroup7.Text = "Caja de Compensación, SENA e ICBF"
        '
        'INDLciIBCCompensationFund
        '
        Me.INDLciIBCCompensationFund.Control = Me.INDTxtIBCCompensationFund
        Me.INDLciIBCCompensationFund.Location = New System.Drawing.Point(0, 0)
        Me.INDLciIBCCompensationFund.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciIBCCompensationFund.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciIBCCompensationFund.Name = "INDLciIBCCompensationFund"
        Me.INDLciIBCCompensationFund.Size = New System.Drawing.Size(390, 60)
        Me.INDLciIBCCompensationFund.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciIBCCompensationFund.Text = "IBC Caja de Compensación"
        Me.INDLciIBCCompensationFund.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciIBCCompensationFund.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.INDSpCompensationFundPercentage
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "% Caja de Compensación"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDTxtValueCompensation
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.Text = "Valor Caja de Compensación"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDSpSenaPercentage
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 180)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "% SENA"
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.INDTxtValueSena
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 240)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.Text = "Valor SENA"
        Me.LayoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDSpICBFPercentage
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 300)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "% ICBF"
        Me.LayoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDTxtICBFValue
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 360)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(390, 114)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "Valor ICBF"
        Me.LayoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLcgInabilities
        '
        Me.INDLcgInabilities.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgInabilities.AppearanceGroup.Options.UseFont = True
        Me.INDLcgInabilities.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgInabilities.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgInabilities.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgInabilities.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgInabilities.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgInabilities.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgInabilities.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgInabilities.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgInabilities.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgInabilities.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgInabilities.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgInabilities.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgInabilities, False)
        Me.INDLcgInabilities.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10, Me.LayoutControlItem11})
        Me.INDLcgInabilities.Location = New System.Drawing.Point(2394, 0)
        Me.INDLcgInabilities.Name = "INDLcgInabilities"
        Me.INDLcgInabilities.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgInabilities.Text = "Incapacidad General"
        Me.INDLcgInabilities.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.INDDeInabilityInitialDate
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.Text = "Fecha Inicio Incapacidad"
        Me.LayoutControlItem10.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.INDDeInabilityEndDate
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(390, 414)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.Text = "Fecha Fin Incapacidad"
        Me.LayoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLcgMaternity
        '
        Me.INDLcgMaternity.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMaternity.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMaternity.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMaternity.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMaternity.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMaternity.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMaternity.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMaternity.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMaternity.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMaternity.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMaternity.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMaternity.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMaternity.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMaternity.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMaternity, False)
        Me.INDLcgMaternity.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12, Me.LayoutControlItem13})
        Me.INDLcgMaternity.Location = New System.Drawing.Point(2808, 0)
        Me.INDLcgMaternity.Name = "INDLcgMaternity"
        Me.INDLcgMaternity.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgMaternity.Text = "Maternidad"
        Me.INDLcgMaternity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.INDDeMaternityInitialDate
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem12.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem12.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem12.Text = "Fecha Inicio Licencia Maternidad"
        Me.LayoutControlItem12.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.INDDeMaternityEndDate
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem13.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem13.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(390, 414)
        Me.LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem13.Text = "Fecha Fin Licencia Maternidad"
        Me.LayoutControlItem13.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLcgVacation
        '
        Me.INDLcgVacation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgVacation.AppearanceGroup.Options.UseFont = True
        Me.INDLcgVacation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgVacation.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgVacation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgVacation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgVacation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgVacation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgVacation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgVacation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgVacation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgVacation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgVacation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgVacation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgVacation, False)
        Me.INDLcgVacation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem14, Me.LayoutControlItem15})
        Me.INDLcgVacation.Location = New System.Drawing.Point(3222, 0)
        Me.INDLcgVacation.Name = "INDLcgVacation"
        Me.INDLcgVacation.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgVacation.Text = "Vacaciones"
        Me.INDLcgVacation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.INDDeInitialDateVacation
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem14.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem14.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem14.Text = "Fecha Inicio Vacaciones"
        Me.LayoutControlItem14.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.INDDeEndDateVacation
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(390, 414)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.Text = "Fecha Fin Vacaciones"
        Me.LayoutControlItem15.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLcgSanction
        '
        Me.INDLcgSanction.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgSanction.AppearanceGroup.Options.UseFont = True
        Me.INDLcgSanction.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgSanction.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgSanction.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgSanction.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgSanction.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgSanction.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgSanction.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgSanction.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgSanction.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgSanction.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgSanction.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgSanction.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgSanction, False)
        Me.INDLcgSanction.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem16, Me.LayoutControlItem17})
        Me.INDLcgSanction.Location = New System.Drawing.Point(3636, 0)
        Me.INDLcgSanction.Name = "INDLcgSanction"
        Me.INDLcgSanction.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgSanction.Text = "Sanción"
        Me.INDLcgSanction.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.INDDeInitialDateSanction
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.Text = "Fecha Inicio Sanción"
        Me.LayoutControlItem16.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.INDDeEndDateSanction
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem17.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem17.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(390, 414)
        Me.LayoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem17.Text = "Fecha Fin Sanción"
        Me.LayoutControlItem17.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(211, 17)
        '
        'INDLcgIRL
        '
        Me.INDLcgIRL.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgIRL.AppearanceGroup.Options.UseFont = True
        Me.INDLcgIRL.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgIRL.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgIRL.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgIRL.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgIRL.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgIRL.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgIRL.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgIRL.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgIRL.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgIRL.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgIRL.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgIRL.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgIRL, False)
        Me.INDLcgIRL.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem18, Me.LayoutControlItem19})
        Me.INDLcgIRL.Location = New System.Drawing.Point(4050, 0)
        Me.INDLcgIRL.Name = "INDLcgIRL"
        Me.INDLcgIRL.Size = New System.Drawing.Size(414, 533)
        Me.INDLcgIRL.Text = "Incapacidad Riesgos Laborales"
        Me.INDLcgIRL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.INDDeInitialIRLInability
        Me.LayoutControlItem18.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem18.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem18.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem18.Text = "Fecha Inicio Incapacidad Riesgos"
        Me.LayoutControlItem18.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(211, 17)
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.INDDeEndDateIRL
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(390, 414)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.Text = "Fecha Fin Incapacidad Riesgos"
        Me.LayoutControlItem19.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(211, 17)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControlPanel1.TabIndex = 1
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmEditAutoliquidation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmEditAutoliquidation"
        Me.Opacity = 1.0R
        Me.Text = "Editar Autoliquidación"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDDeEndDateIRL.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDateIRL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialIRLInability.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialIRLInability.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMemoObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDateSanction.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDateSanction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialDateSanction.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialDateSanction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDateVacation.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDateVacation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialDateVacation.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialDateVacation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeMaternityEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeMaternityEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeMaternityInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeMaternityInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInabilityEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInabilityEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInabilityInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInabilityInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtICBFValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpICBFPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValueSena.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpSenaPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValueCompensation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpCompensationFundPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpProfessionalRiskPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtProfessionalValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValueHealth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpHealthPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValuePension.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpPensionPercentage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtIBCCompensationFund.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtibcProfessionalRisk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtIBCHealth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtIBCPension.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpCompensationFundDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpProfessionalRiskDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpHealthDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpPensionDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtNameEmployee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtNitEmployee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciNameEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPensionDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciHealthDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProfessionalRiskDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCompensationFundDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPensionPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValuePension, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIBCHealth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LciIBCProfessionalRisk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProfessionalValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPercentageProfessional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciIBCCompensationFund, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgInabilities, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMaternity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgVacation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgSanction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgIRL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtNameEmployee As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents INDTxtNitEmployee As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciNit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciNameEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDTxtValueHealth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSpHealthPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtValuePension As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSpPensionPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtIBCCompensationFund As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtibcProfessionalRisk As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtIBCHealth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtIBCPension As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSpCompensationFundDays As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpProfessionalRiskDays As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpHealthDays As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDSpPensionDays As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciPensionDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciHealthDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciProfessionalRiskDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCompensationFundDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciPensionPercentage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValuePension As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciIBCHealth As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LciIBCProfessionalRisk As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciIBCCompensationFund As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSpProfessionalRiskPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtProfessionalValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciProfessionalValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciPercentageProfessional As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDeInabilityEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDeInabilityInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDTxtICBFValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSpICBFPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtValueSena As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSpSenaPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDTxtValueCompensation As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDSpCompensationFundPercentage As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgInabilities As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDeMaternityEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDDeMaternityInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgMaternity As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDeEndDateVacation As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDeInitialDateVacation As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgVacation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDeEndDateSanction As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDeInitialDateSanction As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLcgSanction As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMemoObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDDeInitialIRLInability As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgIRL As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDDeEndDateIRL As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
End Class
