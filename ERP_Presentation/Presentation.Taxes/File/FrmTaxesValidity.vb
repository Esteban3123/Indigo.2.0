Imports Presentation.Controls
Imports Presentation.Base

Public Class FrmTaxesValidity

    Private Sub FrmTaxesValidity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActionsOnControls = False
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
    End Sub


    Public WriteOnly Property ActionsOnControls() As Boolean
        Set(value As Boolean)
            ' Datos Principales
            LayoutControlGroup1.BeginUpdate()
            INDslYear.Enabled = Not value
            INDSlLegalSalarium.Enabled = value
            INDSlAccountPayableCloseAnual.Enabled = value
            INDSlAccountPayableOtherPortfolio.Enabled = value
            INDSlAccountingAccountIngressInterest.Enabled = value
            INDsleIndustryApplicationForm.Enabled = value
            INDSlRetentionApplicationForm.Enabled = value
            INDSlePropertyTaxCashReceipt.Enabled = value
            INDSleIndustryBusinessTax.Enabled = value
            INDSlValorizationCashReceipt.Enabled = value
            INDSlReteICACashReceipt.Enabled = value
            INDSlePropertyTaxPreviousPeriod.Enabled = value
            INDSleIndustryBusinessTaxPreviousPeriod.Enabled = value
            INDSlValorizationPreviousPeriod.Enabled = value
            INDSlReteICAPreviousPeriod.Enabled = value
            INDSlIterest.Enabled = value
            BarraBotones.StatusRecordVisible = value
            LayoutControlGroup1.EndUpdate()

        End Set
    End Property


End Class