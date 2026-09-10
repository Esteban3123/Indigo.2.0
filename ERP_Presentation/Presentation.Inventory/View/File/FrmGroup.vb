'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Accounting
Imports Presentation.Inventory.MVP
Imports Presentation.Common
Imports Presentation.Payments
Imports Presentation.Payments.MVP
Imports DevExpress.Xpo
Imports System.Text
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Accounting.MVP
Imports System.Windows.Forms
Imports System.Drawing

#End Region

Public Class FrmGroup
    Implements IGroup, ICustomizableForm

#Region "Properties"

    Public Property AffectBudget As Boolean Implements IGroup.AffectBudget
        Get
            Return INDsleAffectBudget.EditValue
        End Get
        Set(value As Boolean)
            INDsleAffectBudget.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityId As Integer? Implements IGroup.BudgetaryEntityId
        Get
            Return INDsleBudgetaryEntityId.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As XPInstantFeedbackSource Implements IGroup.BudgetaryEntityXpo
        Get
            Return INDsleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer? Implements IGroup.BudgetaryValidityId
        Get
            Return INDsleBudgetaryValidityId.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPInstantFeedbackSource Implements IGroup.BudgetaryValidityXpo
        Get
            Return INDsleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetId As Integer? Implements IGroup.BudgetId
        Get
            Return INDsleBudget.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudget.EditValue = value
        End Set
    End Property

    Public Property BudgetXpo As XPInstantFeedbackSource Implements IGroup.BudgetXpo
        Get
            Return INDsleBudget.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudget.Properties.DataSource = value
        End Set
    End Property

    Public Property InventoryCostMainAccountId As Integer? Implements IGroup.InventoryCostMainAccountId
        Get
            Return INDsleInventoryCostMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleInventoryCostMainAccount.EditValue = value
        End Set
    End Property

    Public Property InventoryCostMainAccountXpo As XPInstantFeedbackSource Implements IGroup.InventoryCostMainAccountXpo
        Get
            Return INDsleInventoryCostMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleInventoryCostMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAAccountId As Integer? Implements IGroup.IVAAccountId
        Get
            Return INDPucIVAAccount.EditValue
        End Get
        Set(value As Integer?)
            INDPucIVAAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IVAAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.IVAAccountXpo
        Get
            Return INDPucIVAAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDPucIVAAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTaxAccountId As Integer? Implements IGroup.WithholdingTaxAccountId
        Get
            Return INDPucWithholdingTaxAccount.EditValue
        End Get
        Set(value As Integer?)
            INDPucWithholdingTaxAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingTaxAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.WithholdingTaxAccountXpo
        Get
            Return INDPucWithholdingTaxAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDPucWithholdingTaxAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICAAccountId As Integer? Implements IGroup.WithholdingICAAccountId
        Get
            Return INDPucWithholdingICAAccount.EditValue
        End Get
        Set(value As Integer?)
            INDPucWithholdingICAAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICAAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.WithholdingICAAccountXpo
        Get
            Return INDPucWithholdingICAAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDPucWithholdingICAAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion de la fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReteFuenteConceptId As Integer? Implements IGroup.ReteFuenteConceptId
        Get
            Return INDsleReteFuenteConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleReteFuenteConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion de la fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReteFuenteConceptXpo As XPInstantFeedbackSource Implements IGroup.ReteFuenteConceptXpo
        Get
            Return INDsleReteFuenteConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReteFuenteConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion de ica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICAConceptId As Integer? Implements IGroup.WithholdingICAConceptId
        Get
            Return INDSleWithholdingICAConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleWithholdingICAConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion para ica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WithholdingICAConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.WithholdingICAConceptXpo
        Get
            Return INDSleWithholdingICAConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleWithholdingICAConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si se realiza el proceso de costo a los productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExcludeFreightCosts As Boolean? Implements IGroup.ExcludeFreightCosts
        Get
            Return INDsleExcludeFreightCosts.EditValue
        End Get
        Set(value As Boolean?)
            INDsleExcludeFreightCosts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de cxp 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeclarantRetentionAccountPayableConceptId As Integer? Implements IGroup.DeclarantRetentionAccountPayableConceptId
        Get
            Return INDSleDeclarantRetentionAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDSleDeclarantRetentionAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de cxp 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NotDeclarantRetentionAccountPayableConceptId As Integer? Implements IGroup.NotDeclarantRetentionAccountPayableConceptId
        Get
            Return INDSleNotDeclarantRetentionAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDSleNotDeclarantRetentionAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeclarantRetentionAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.DeclarantRetentionAccountPayableConceptIdXpo
        Get
            Return INDSleDeclarantRetentionAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleDeclarantRetentionAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NotDeclarantRetentionAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.NotDeclarantRetentionAccountPayableConceptIdXpo
        Get
            Return INDSleNotDeclarantRetentionAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleNotDeclarantRetentionAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InventoryAccountPayableConceptId As Integer? Implements IGroup.InventoryAccountPayableConceptId
        Get
            Return INDsleInventoryAccountPayableConceptId.EditValue
        End Get
        Set(value As Integer?)
            INDsleInventoryAccountPayableConceptId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InventoryAccountPayableConceptIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.InventoryAccountPayableConceptIdXpo
        Get
            Return INDsleInventoryAccountPayableConceptId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleInventoryAccountPayableConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IGroup.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de seguridad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SecurityPercentage As Decimal Implements IGroup.SecurityPercentage
        Get
            Return INDseSecurityPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDseSecurityPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tiempo de abstecimiento de los productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductsSourcingTime As Integer Implements IGroup.ProductsSourcingTime
        Get
            Return INDseProductsSourcingTime.EditValue
        End Get
        Set(value As Integer)
            INDseProductsSourcingTime.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tiempo de reposicion de los productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductReplacementTime As Integer Implements IGroup.ProductReplacementTime
        Get
            Return INDseProductReplacementTime.EditValue
        End Get
        Set(value As Integer)
            INDseProductReplacementTime.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequence As InventorySequence Implements IGroup.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IGroup.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IGroup.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IGroup.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.CostCenterXpo
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IGroup.Description
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establce el id del codigo de subAtencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAttentionSub As Integer? Implements IGroup.IdAttentionSub
        Get
            Return INDsleSubClassCode.EditValue
        End Get
        Set(value As Integer?)
            INDsleSubClassCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la clase
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdClass As Integer? Implements IGroup.IdClass
        Get
            Return INDsleGroupClass.EditValue
        End Get
        Set(value As Integer?)
            INDsleGroupClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenter As Integer? Implements IGroup.IdCostCenter
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IncomeAccountId As Integer? Implements IGroup.IncomeAccountId
        Get
            Return INDsleIncomeAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleIncomeAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Cuenta contable paquetes
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountingPackageMainAccountId As Integer? Implements IGroup.AccounttingPackageMainAccountId
        Get
            Return INDsleAccountPackage.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountPackage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Cuenta contable cuenta desviación favorable
    ''' </summary>
    ''' <returns></returns>
    Public Property FavorableDeviationMainAccountId As Integer? Implements IGroup.FavorableDeviationMainAccountId
        Get
            Return INDsleAccountFavorableVariation.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountFavorableVariation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Cuenta contable variación de precio
    ''' </summary>
    ''' <returns></returns>
    Public Property VariationPVMainAccountId As Integer? Implements IGroup.VariationPVMainAccountId
        Get
            Return INDsleAccountPriceVariation.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountPriceVariation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de reconocimiento de ingresos pendientes por facturar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IncomeRecognitionMainAccountId As Integer? Implements IGroup.IncomeRecognitionMainAccountId
        Get
            Return INDsleIncomeRecognitionMainAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleIncomeRecognitionMainAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de credito remision entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferenceInputCreditAccountId As Integer? Implements IGroup.ReferenceInputCreditAccountId
        Get
            Return INDsleReferenceInputCreditAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleReferenceInputCreditAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de debito remision entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferenceInputDebitAccountId As Integer? Implements IGroup.ReferenceInputDebitAccountId
        Get
            Return INDsleReferenceInputDebitAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleReferenceInputDebitAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de credito remision salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferenceOutputCreditAccountId As Integer? Implements IGroup.ReferenceOutputCreditAccountId
        Get
            Return INDsleReferenceOutputCreditAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleReferenceOutputCreditAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establce el id de debito remision salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferenceOutputDebitAccountId As Integer? Implements IGroup.ReferenceOutputDebitAccountId
        Get
            Return INDsleReferenceOutputDebitAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleReferenceOutputDebitAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establce el id de la cuenta debito para realizar los movimientos de inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentMerchandiseDebitAccountId As Integer? Implements IGroup.ConsignmentMerchandiseDebitAccountId
        Get
            Return INDsleConsignmentMerchandiseDebitAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleConsignmentMerchandiseDebitAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta credito para realizar los movimientos de inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentMerchandiseCreditAccountId As Integer? Implements IGroup.ConsignmentMerchandiseCreditAccountId
        Get
            Return INDsleConsignmentMerchandiseCreditAccountId.EditValue
        End Get
        Set(value As Integer?)
            INDsleConsignmentMerchandiseCreditAccountId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contrapartida al costo del movimiento de inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CounterpartCostConsignedInventoryId As Integer? Implements IGroup.CounterpartCostConsignedInventoryId
        Get
            Return INDsleCounterpartCostConsignedInventoryId.EditValue
        End Get
        Set(value As Integer?)
            INDsleCounterpartCostConsignedInventoryId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivity As Integer?
        Get
            Return INDsleEconomicActivity.EditValue
        End Get
        Set(value As Integer?)
            INDsleEconomicActivity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de la actividad economica
    ''' </summary>
    ''' <returns></returns>
    Public Property EconomicActivityDatasource As XPInstantFeedbackSource Implements IGroup.EconomicActivityDatasource
        Get
            Return CType(INDsleEconomicActivity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEconomicActivity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IncomeAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.IncomeAccountIdXpo
        Get
            Return INDsleIncomeAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIncomeAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas para el reconocimiento de ingresos pendientes por facturar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IncomeRecognitionMainAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.IncomeRecognitionMainAccountIdXpo
        Get
            Return INDsleIncomeRecognitionMainAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIncomeRecognitionMainAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de credito remision entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferenceInputCreditAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.ReferenceInputCreditAccountIdXpo
        Get
            Return INDsleReferenceInputCreditAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleReferenceInputCreditAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de debito remision entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferenceInputDebitAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.ReferenceInputDebitAccountIdXpo
        Get
            Return INDsleReferenceInputDebitAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleReferenceInputDebitAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de credito remision salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferenceOutputCreditAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.ReferenceOutputCreditAccountIdXpo
        Get
            Return INDsleReferenceOutputCreditAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleReferenceOutputCreditAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de debito remision salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReferenceOutputDebitAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.ReferenceOutputDebitAccountIdXpo
        Get
            Return INDsleReferenceOutputDebitAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleReferenceOutputDebitAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas para el inventario en consignación débito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentMerchandiseDebitAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.ConsignmentMerchandiseDebitAccountIdXpo
        Get
            Return INDsleConsignmentMerchandiseDebitAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConsignmentMerchandiseDebitAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas para el inventario en consignación crédito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConsignmentMerchandiseCreditAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.ConsignmentMerchandiseCreditAccountIdXpo
        Get
            Return INDsleConsignmentMerchandiseCreditAccountId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleConsignmentMerchandiseCreditAccountId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas para la contrapartida al costo del movimiento de inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CounterpartCostConsignedInventoryIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.CounterpartCostConsignedInventoryIdXpo
        Get
            Return INDsleCounterpartCostConsignedInventoryId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCounterpartCostConsignedInventoryId.Properties.DataSource = value
        End Set
    End Property

    Public Property AccountingPackageMainAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.AccountingPackageMainAccountIdXpo
        Get
            Return INDsleAccountPackage.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountPackage.Properties.DataSource = value
        End Set
    End Property

    Public Property FavorableDeviationMainAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.FavorableDeviationMainAccountIdXpo
        Get
            Return INDsleAccountFavorableVariation.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountFavorableVariation.Properties.DataSource = value
        End Set
    End Property

    Public Property VariationPVMainAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.VariationPVMainAccountIdXpo
        Get
            Return INDsleAccountPriceVariation.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountPriceVariation.Properties.DataSource = value
        End Set
    End Property

#Region "Configuración por Unidad Funcional"

    ''' <summary>
    ''' Obtiene o establece el id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitId As Integer? Implements IGroup.FunctionalUnitId
        Get
            Return INDsleFunctionalUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleFunctionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.FunctionalUnitIdXpo
        Get
            Return INDsleFunctionalUnit.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable del costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostAccountId As Integer? Implements IGroup.CostAccountId
        Get
            Return INDsleCostAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable del costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.CostAccountIdXpo
        Get
            Return INDsleCostAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesAccountId As Integer? Implements IGroup.SalesAccountId
        Get
            Return INDsleSalesAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleSalesAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SalesAccountIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.SalesAccountIdXpo
        Get
            Return INDsleSalesAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSalesAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable de descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IGroup.DiscountAccountXpo
        Get
            Return INDsleDiscountAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleDiscountAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable de descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DiscountAccountId As Integer? Implements IGroup.DiscountAccountId
        Get
            Return INDsleDiscountAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleDiscountAccount.EditValue = value
        End Set
    End Property

#End Region

#End Region

#Region "Variables"

    ''' <summary>
    ''' bandera para indicar que se está cargando un registro
    ''' </summary>
    Private _isLoading As Boolean

    ''' <summary>
    ''' Listado que establece si o no
    ''' </summary>
    Private ListYesNot As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PGroup

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim InventoryClass As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim InventoryAttentionSub As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Parametros de Inventario
    ''' </summary>
    Private _parameterInventory As SettingInventory

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Representa la entidad de grupo de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim productGroup As ProductGroup

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Listado del detalle de la configuración contable por unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListProductGroupFuntionalUnit As List(Of ProductGroupFunctionalUnit)

    ''' <summary>
    ''' Listado de eliminados del detalle de la configuración contable por unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteProductGroupFuntionalUnit As List(Of ProductGroupFunctionalUnit)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        ' Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me.productGroup IsNot Nothing AndAlso Me.productGroup.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MGroup(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteProductGroup(Me.productGroup)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MGroup(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ProductGroup) = Await Model.SaveProductGroup(Me.productGroup, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If productGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.productGroup = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewGroup()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Clase", .FieldName = "GroupClassName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Sub Clase", .FieldName = "SubclassCodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListProductGroup
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PGroup(Me)
        Presenter.GetSequense()
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Await Me.LoadParameters()

        InitializeSearch()
        INDsleGroupClass.Properties.Buttons.Item(1).Visible = False
        INDsleSubClassCode.Properties.Buttons.Item(1).Visible = False
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewGridControl, ListActions)
        Deshacer()
        LoadStatus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        InventoryClass = Nothing
        InventoryAttentionSub = Nothing
        _parameterInventory = Nothing
        _sequence = Nothing
        productGroup = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetaryEntityId.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            Presenter.InitializeBudgetaryEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetaryValidityId.QueryPopUp
        If BudgetaryValidityXpo Is Nothing Then
            Presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rubro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudget_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudget.QueryPopUp
        If BudgetXpo Is Nothing Then
            Presenter.InitializeBudget(BudgetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable costo inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleInventoryCostMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleInventoryCostMainAccount.QueryPopUp
        If InventoryCostMainAccountXpo Is Nothing Then
            Presenter.InitializeInventoryCostMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReteFuenteConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReteFuenteConcept.QueryPopUp
        If ReteFuenteConceptXpo Is Nothing Then
            Presenter.InitializeReteFuenteConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de ingresos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIncomeAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIncomeAccountId.QueryPopUp
        If INDsleIncomeAccountId.Properties.DataSource Is Nothing Then
            Presenter.InitializeIncomeAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de reconocimiento de ingresos pendientes por facturar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleItemIncomeRecognitionMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIncomeRecognitionMainAccountId.QueryPopUp
        If INDsleIncomeRecognitionMainAccountId.Properties.DataSource Is Nothing Then
            Presenter.InitializeIncomeRecognitionMainAccountId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de conceptos de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRetentionAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDeclarantRetentionAccountPayableConceptId.QueryPopUp
        If DeclarantRetentionAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeDeclarantRetentionAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de debito remision entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRemissionInputDebit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReferenceInputDebitAccountId.QueryPopUp
        If INDsleReferenceInputDebitAccountId.Properties.DataSource Is Nothing Then
            Presenter.InitializeRemissionInputDebit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de credito remision entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRemissionInputCredit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReferenceInputCreditAccountId.QueryPopUp
        If INDsleReferenceInputCreditAccountId.Properties.DataSource Is Nothing Then
            Presenter.InitializeRemissionInputCredit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de credito remision salida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRemissionOutputCredit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReferenceOutputCreditAccountId.QueryPopUp
        If INDsleReferenceOutputCreditAccountId.Properties.DataSource Is Nothing Then
            Presenter.InitializeRemissionOutputCredit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de debito remision salida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRemissionOutputDebit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReferenceOutputDebitAccountId.QueryPopUp
        If INDsleReferenceOutputDebitAccountId.Properties.DataSource Is Nothing Then
            Presenter.InitializeRemissionOutputDebit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control cuenta debito de mercancía en consignación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConsignmentMerchandiseDebitAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConsignmentMerchandiseDebitAccountId.QueryPopUp
        If INDsleConsignmentMerchandiseDebitAccountId.Properties.DataSource Is Nothing Then
            Presenter.InitializeConsignmentMerchandiseDebit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control cuenta credito de mercancía en consignación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConsignmentMerchandiseCreditAccountId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConsignmentMerchandiseCreditAccountId.QueryPopUp
        If INDsleConsignmentMerchandiseCreditAccountId.Properties.DataSource Is Nothing Then
            Presenter.InitializeConsignmentMerchandiseCredit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control cuenta credito de mercancía en consignación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCounterpartCostConsignedInventoryId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCounterpartCostConsignedInventoryId.QueryPopUp
        If INDsleCounterpartCostConsignedInventoryId.Properties.DataSource Is Nothing Then
            Presenter.InitializeCounterpartCostConsignedInventory()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInventoryAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleInventoryAccountPayableConceptId.QueryPopUp
        If InventoryAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeInventoryAccountPayableConceptId()
        End If
    End Sub

    Private Sub INDSleNotDeclarantRetentionAccountPayableConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleNotDeclarantRetentionAccountPayableConceptId.QueryPopUp
        If NotDeclarantRetentionAccountPayableConceptIdXpo Is Nothing Then
            Presenter.InitializeNotDeclarantRetentionAccountPayableConceptId()
        End If
    End Sub

    Private Sub INDPucIVAAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPucIVAAccount.QueryPopUp
        If IVAAccountXpo Is Nothing Then
            Presenter.InitializeIVAAccount()
        End If
    End Sub

    Private Sub INDPucWithholdingTaxAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPucWithholdingTaxAccount.QueryPopUp
        If WithholdingTaxAccountXpo Is Nothing Then
            Presenter.InitializeWithholdingTaxAccount()
        End If
    End Sub

    Private Sub INDPucWithholdingICAAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPucWithholdingICAAccount.QueryPopUp
        If WithholdingICAAccountXpo Is Nothing Then
            Presenter.InitializeWithholdingICAAccount()
        End If
    End Sub

    Private Sub INDSleWithholdingICAConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleWithholdingICAConcept.QueryPopUp
        If WithholdingICAConceptXpo Is Nothing Then
            Presenter.InitializeWithholdingICAConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If FunctionalUnitIdXpo Is Nothing Then
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostAccount.QueryPopUp
        If CostAccountIdXpo Is Nothing Then
            Presenter.InitializeCostAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSalesAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSalesAccount.QueryPopUp
        If SalesAccountIdXpo Is Nothing Then
            Presenter.InitializeSalesAccount()
        End If
    End Sub

    Private Sub INDsleAccountPackage_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountPackage.QueryPopUp
        If AccountingPackageMainAccountIdXpo Is Nothing Then
            Presenter.InitializeAccountingPackage()
        End If
    End Sub

    Private Sub INDsleAccountFavorableVariation_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountFavorableVariation.QueryPopUp
        If FavorableDeviationMainAccountIdXpo Is Nothing Then
            Presenter.InitializeAccountFavorableDeviation()
        End If
    End Sub

    Private Sub INDsleAccountPriceVariation_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAccountPriceVariation.QueryPopUp
        If VariationPVMainAccountIdXpo Is Nothing Then
            Presenter.InitializeAccountVariationPV()
        End If
    End Sub

    ''' <summary>
    ''' evento que carga las cuentas contable para descuento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDiscountAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDiscountAccount.QueryPopUp
        If DiscountAccountXpo Is Nothing Then
            Presenter.InitializeDiscountAccount()
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmGroup_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewGroup()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 sobre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpce_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpce.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpce.ShowPopup()
            INDsleFunctionalUnit.Focus()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmGroup_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleInventoryCostMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleInventoryCostMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeInventoryCostMainAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReteFuenteConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleReteFuenteConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(605, Nothing, True)
            Presenter.InitializeReteFuenteConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de concepto de cxp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInventoryAccountPayableConceptId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleInventoryAccountPayableConceptId.ButtonClick, INDSleDeclarantRetentionAccountPayableConceptId.ButtonClick, INDSleNotDeclarantRetentionAccountPayableConceptId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmConceptsAccountsPayable With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeConceptCxP()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de cuentas contables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIncomeAccountId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIncomeAccountId.ButtonClick, INDsleIncomeRecognitionMainAccountId.ButtonClick, INDslePurchasesAccountId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeMainAccounts()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presional el boton del control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRetentionConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRetentionConcept With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeDeclarantRetentionAccountPayableConceptId()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFunctionalUnit_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleFunctionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Payroll.FrmFunctionalUnit With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleCostAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCostAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSalesAccount_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleSalesAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSalesAccount()
        End If
    End Sub
    ''' <summary>
    ''' Metodo para abrir el frm de actividades economicas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEconomicActivity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEconomicActivity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEconomicActivity
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False

                Dim proportionWidth As Double = 0.6 ' 60% del ancho de la pantalla
                Dim proportionHeight As Double = 0.8 ' 80% de la altura de la pantalla

                ' Calcula el tamaño proporcional
                Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width 'Calculamos el ancho de la pantalla actual
                Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height 'Obtenemos la altura de la pantalla actual
                'Nuevas medidas de nuestro popup
                Dim popUpWidth As Integer = CInt(screenWidth * proportionWidth)
                Dim popUpHeight As Integer = CInt(screenHeight * proportionHeight)

                ' Asignamos el nuevo tamaño al formulario de actividades Economicas
                Formulario.Size = New Size(popUpWidth, popUpHeight)
                Formulario.StartPosition = FormStartPosition.CenterParent 'Centramos el frm

                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de si afecta presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAffectBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAffectBudget.EditValueChanged
        INDlyItemBudgetaryEntity.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBudgetaryEntity.AllowHide = Not AffectBudget
        INDlyItemBudgetaryEntity.ShowInCustomizationForm = Not AffectBudget

        INDlyItemBudgetaryValidity.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBudgetaryValidity.AllowHide = Not AffectBudget
        INDlyItemBudgetaryValidity.ShowInCustomizationForm = Not AffectBudget

        INDlyItemBudget.Visibility = If(AffectBudget, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemBudget.AllowHide = Not AffectBudget
        INDlyItemBudget.ShowInCustomizationForm = Not AffectBudget

        If Not AffectBudget Then
            CleanBudgetInterface(0)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetaryEntityId.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        CleanBudgetInterface(1)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetaryValidityId.EditValueChanged
        If _isLoading Then
            Exit Sub
        End If

        CleanBudgetInterface(2)
    End Sub

    Private Sub INDsleInventoryAccountPayableConceptId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInventoryAccountPayableConceptId.EditValueChanged
        If INDsleInventoryAccountPayableConceptId.EditValue IsNot Nothing Then
            Using model As New MConceptsAccountsPayable(MyTag)
                Dim conceptTmp = model.GetPaymentConceptById(INDsleInventoryAccountPayableConceptId.EditValue).ObjectEmbbeded
                INDslePurchasesAccountId.Properties.NullText = $"{conceptTmp?.MainAccounts?.Number} - {conceptTmp?.MainAccounts?.Name}"
            End Using
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddDetail()
    End Sub

#End Region

#Region "ContextMenu"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteDetail()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteDetail()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpia los controles de presupuesto
    ''' </summary>
    ''' <param name="level"></param>
    Private Sub CleanBudgetInterface(level As Integer)
        If level < 1 Then
            BudgetaryEntityId = Nothing
            INDsleBudgetaryEntityId.Properties.NullText = String.Empty
        End If
        If level < 2 Then
            BudgetaryValidityId = Nothing
            INDsleBudgetaryValidityId.Properties.NullText = String.Empty
            BudgetaryValidityXpo = Nothing
        End If
        If level < 3 Then
            BudgetId = Nothing
            INDsleBudget.Properties.NullText = String.Empty
            BudgetXpo = Nothing
        End If
    End Sub

    Private Async Function LoadParameters() As Task
        Try
            AsyncLoader(True)
            Using model As New Presentation.Inventory.MVP.MSettingInventory(MyTag)
                Me._parameterInventory = (Await model.GetInventorySettingsRegister(Me._idOperativeUnit)).ObjectEmbbeded
                If Me._parameterInventory Is Nothing OrElse Me._parameterInventory.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Inventario para la unidad operativa seleccionada"
                    Exit Function
                End If

                If Me._parameterInventory.AssociateCostMainAccount = 1 Then
                    INDlygProductGroupFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Else
                    INDlygProductGroupFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End Using

            Using model As New Presentation.Payments.MVP.MParameters(MyTag)
                Dim budgetInterface As Boolean = False
                Dim _parameter = Await model.GetSettingPaymentsByIdOperatingUnit(_idOperativeUnit)
                If _parameter IsNot Nothing AndAlso _parameter.ObjectEmbbeded IsNot Nothing AndAlso _parameter.StateResult Then
                    budgetInterface = _parameter.ObjectEmbbeded.BudgetInterface
                End If

                INDlyItemAffectBudget.AllowHide = Not budgetInterface
                INDlyItemBudget.ShowInCustomizationForm = Not budgetInterface
                INDlygBudgetInterface.Visibility = If(budgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            End Using

            Using model As New Presentation.Billing.MVP.MSettingBilling(MyTag)
                Dim result = Await model.GetSettingsBillingByIdUnitOperative(Me._idOperativeUnit)
                If Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoExistenParametrosFacturacion", "Facturacion")
                    Exit Function
                End If
                Dim settingBilling = result.ObjectEmbbeded
                If settingBilling.AccountingPackage Then
                    INDlygAccountingPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End Using
            Using model As New MCompanySettings(Tag)
                Dim _companySettings = Await model.GetCompanySettings()
                If _companySettings IsNot Nothing Then
                    If _companySettings.TransactionEconomicActivity Then
                        Presenter.InitializeEconomicActivity()
                        INDliEconomicActivity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    End If
                Else
                    Mensaje(EeventViewerImages.MensajeError) = "No se encontró campo parametrizado de Actividad Económica"

                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Se presentó un error al cargar los datos de la unidad operativa"
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IGroup.ActionsOnControls
        Set(value As Boolean)
            INDlyGroup.BeginUpdate()

            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleGroupClass.Enabled = value
            INDsleSubClassCode.Enabled = value
            INDsleIncomeRecognitionMainAccountId.Enabled = value
            INDsleInventoryAccountPayableConceptId.Enabled = value
            INDslePurchasesAccountId.Enabled = value
            INDSleDeclarantRetentionAccountPayableConceptId.Enabled = value
            INDSleNotDeclarantRetentionAccountPayableConceptId.Enabled = value
            INDsleCostCenter.Enabled = value
            INDsleReferenceInputDebitAccountId.Enabled = value
            INDsleReferenceInputCreditAccountId.Enabled = value
            INDsleReferenceOutputDebitAccountId.Enabled = value
            INDsleReferenceOutputCreditAccountId.Enabled = value
            INDsleConsignmentMerchandiseDebitAccountId.Enabled = value
            INDsleConsignmentMerchandiseCreditAccountId.Enabled = value
            INDsleCounterpartCostConsignedInventoryId.Enabled = value
            INDsleExcludeFreightCosts.Enabled = value
            INDseSecurityPercentage.Enabled = value
            INDseProductsSourcingTime.Enabled = value
            INDseProductReplacementTime.Enabled = value

            INDsleIncomeAccountId.Enabled = value
            INDPucIVAAccount.Enabled = value
            INDsleInventoryCostMainAccount.Enabled = value
            INDPucWithholdingTaxAccount.Enabled = value
            INDPucWithholdingICAAccount.Enabled = value
            INDsleReteFuenteConcept.Enabled = value
            INDSleWithholdingICAConcept.Enabled = value

            INDsleAffectBudget.Enabled = value
            INDsleBudgetaryEntityId.Enabled = value
            INDsleBudgetaryValidityId.Enabled = value
            INDsleBudget.Enabled = value

            INDsleAccountPackage.Enabled = value
            INDsleAccountPriceVariation.Enabled = value
            INDsleAccountFavorableVariation.Enabled = value
            INDsleEconomicActivity.Enabled = value

            INDpce.Enabled = value
            INDgcDetail.Enabled = value

            INDlyGroup.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.productGroup IsNot Nothing AndAlso Me.productGroup.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Llena el control con el listado de la naturaleza de la cuenta
    ''' </summary>
    Private Sub InitializeSearch()
        InventoryClass = New List(Of Tuple(Of Integer, String))
        InventoryClass.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("Product")))
        InventoryClass.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Service")))
        INDsleGroupClass.Properties.DataSource = InventoryClass.ToList

        InventoryAttentionSub = New List(Of Tuple(Of Integer, String))
        InventoryAttentionSub.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("HospitalSupply")))
        InventoryAttentionSub.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("SurgicalSupply")))
        InventoryAttentionSub.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("Medications")))
        INDsleSubClassCode.Properties.DataSource = InventoryAttentionSub.ToList

        ListYesNot = New List(Of Tuple(Of Boolean, String))
        ListYesNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleAffectBudget.Properties.DataSource = ListYesNot
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.productGroup.Code, Me.productGroup.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.productGroup.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.productGroup.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.productGroup.Code, Me.productGroup.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.productGroup.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyGroup.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        Code = String.Empty
        Description = String.Empty
        IdClass = Nothing
        IdAttentionSub = Nothing

        IncomeRecognitionMainAccountId = Nothing
        INDsleIncomeRecognitionMainAccountId.Properties.NullText = String.Empty

        InventoryAccountPayableConceptId = Nothing
        INDsleInventoryAccountPayableConceptId.Properties.NullText = String.Empty


        INDslePurchasesAccountId.Properties.NullText = String.Empty

        DeclarantRetentionAccountPayableConceptId = Nothing
        INDSleDeclarantRetentionAccountPayableConceptId.Properties.NullText = String.Empty

        NotDeclarantRetentionAccountPayableConceptId = Nothing
        INDSleNotDeclarantRetentionAccountPayableConceptId.Properties.NullText = String.Empty

        IdCostCenter = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty

        ReferenceInputDebitAccountId = Nothing
        INDsleReferenceInputDebitAccountId.Properties.NullText = String.Empty

        ReferenceInputCreditAccountId = Nothing
        INDsleReferenceInputCreditAccountId.Properties.NullText = String.Empty

        ReferenceOutputCreditAccountId = Nothing
        INDsleReferenceOutputCreditAccountId.Properties.NullText = String.Empty

        ReferenceOutputDebitAccountId = Nothing
        INDsleReferenceOutputDebitAccountId.Properties.NullText = String.Empty

        ConsignmentMerchandiseDebitAccountId = Nothing
        INDsleConsignmentMerchandiseDebitAccountId.Properties.NullText = String.Empty

        ConsignmentMerchandiseCreditAccountId = Nothing
        INDsleConsignmentMerchandiseCreditAccountId.Properties.NullText = String.Empty

        CounterpartCostConsignedInventoryId = Nothing
        INDsleCounterpartCostConsignedInventoryId.Properties.NullText = String.Empty

        ExcludeFreightCosts = Nothing
        ProductReplacementTime = Nothing
        ProductsSourcingTime = Nothing
        SecurityPercentage = Nothing
        'Limpiar controles
        Me.productGroup = Nothing

        IncomeAccountId = Nothing
        INDsleIncomeAccountId.Properties.NullText = String.Empty

        IVAAccountId = Nothing
        INDPucIVAAccount.Properties.NullText = String.Empty

        InventoryCostMainAccountId = Nothing
        INDsleInventoryCostMainAccount.Properties.NullText = String.Empty

        WithholdingTaxAccountId = Nothing
        INDPucWithholdingTaxAccount.Properties.NullText = String.Empty

        WithholdingICAAccountId = Nothing
        INDPucWithholdingICAAccount.Properties.NullText = String.Empty

        ReteFuenteConceptId = Nothing
        INDsleReteFuenteConcept.Properties.NullText = String.Empty

        WithholdingICAConceptId = Nothing
        INDSleWithholdingICAConcept.Properties.NullText = String.Empty

        AffectBudget = Nothing
        BudgetaryEntityId = Nothing
        INDsleBudgetaryEntityId.Properties.NullText = String.Empty
        BudgetaryValidityId = Nothing
        INDsleBudgetaryValidityId.Properties.NullText = String.Empty
        BudgetId = Nothing
        INDsleBudget.Properties.NullText = String.Empty
        ListProductGroupFuntionalUnit = Nothing
        ListDeleteProductGroupFuntionalUnit = Nothing
        INDgcDetail.DataSource = Nothing
        EconomicActivity = Nothing

        AccountingPackageMainAccountId = Nothing
        FavorableDeviationMainAccountId = Nothing
        VariationPVMainAccountId = Nothing
        Me.CleanControlsPopup()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyGroup.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Asigna valores a la entidad.
    ''' </summary>
    Private Sub AssigningValues()
        With productGroup
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Description
            .GroupClass = IdClass
            .SubclassCode = IdAttentionSub
            .IncomeRecognitionMainAccountId = IncomeRecognitionMainAccountId

            .IncomeAccountId = IncomeAccountId
            .IVAAccountId = IVAAccountId
            .InventoryCostMainAccountId = InventoryCostMainAccountId
            .WithholdingTaxAccountId = WithholdingTaxAccountId
            .WithholdingICAAccountId = WithholdingICAAccountId
            .ReteFuenteConceptId = ReteFuenteConceptId
            .WithholdingICAConceptId = WithholdingICAConceptId

            .InventoryAccountPayableConceptId = InventoryAccountPayableConceptId
            .DeclarantRetentionAccountPayableConceptId = DeclarantRetentionAccountPayableConceptId
            .NotDeclarantRetentionAccountPayableConceptId = NotDeclarantRetentionAccountPayableConceptId
            .CostCenterId = IdCostCenter
            .ReferenceInputDebitAccountId = ReferenceInputDebitAccountId
            .ReferenceInputCreditAccountId = ReferenceInputCreditAccountId
            .ReferenceOutputCreditAccountId = ReferenceOutputCreditAccountId
            .ReferenceOutputDebitAccountId = ReferenceOutputDebitAccountId
            .ConsignmentMerchandiseDebitAccountId = ConsignmentMerchandiseDebitAccountId
            .ConsignmentMerchandiseCreditAccountId = ConsignmentMerchandiseCreditAccountId
            .CounterpartCostConsignedInventoryId = CounterpartCostConsignedInventoryId
            .ExcludeFreightCosts = ExcludeFreightCosts
            .ProductReplacementTime = ProductReplacementTime
            .ProductsSourcingTime = ProductsSourcingTime
            .SecurityPercentage = SecurityPercentage
            .EconomicActivityId = EconomicActivity

            .AccountingPackageMainAccountId = AccountingPackageMainAccountId
            .FavorableDeviationMainAccountId = FavorableDeviationMainAccountId
            .VariationPVMainAccountId = VariationPVMainAccountId

            .AffectBudget = AffectBudget
            .BudgetId = BudgetId

            If ListProductGroupFuntionalUnit IsNot Nothing Then
                For Each itemDetail As ProductGroupFunctionalUnit In ListProductGroupFuntionalUnit
                    .ProductGroupFunctionalUnit.Add(itemDetail)
                Next
            End If

            If ListDeleteProductGroupFuntionalUnit IsNot Nothing Then
                For Each itemDeleteDetail As ProductGroupFunctionalUnit In ListDeleteProductGroupFuntionalUnit
                    .ProductGroupFunctionalUnit.Add(itemDeleteDetail)
                Next
            End If
        End With
    End Sub

#Region "Configuracion de Unidades Funcionales"

    ''' <summary>
    ''' Metodo que agrega los detalles a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetail()
        Dim listErrors As String = ValidateControlsPopup()
        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors
            INDsleFunctionalUnit.Focus()
            Exit Sub
        End If
        If ListProductGroupFuntionalUnit Is Nothing Then
            ListProductGroupFuntionalUnit = New List(Of ProductGroupFunctionalUnit)
        Else
            Dim cont As Integer = ListProductGroupFuntionalUnit.FindAll(Function(item) item.FunctionalUnitId = FunctionalUnitId).Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ExistFuntionalUnit", NAME_MODULE)
                INDsleFunctionalUnit.Focus()
                Exit Sub
            End If
        End If
        Dim ProductGroupFunctionalUnit As New ProductGroupFunctionalUnit
        With ProductGroupFunctionalUnit
            .FunctionalUnitId = FunctionalUnitId
            .CostAccountId = CostAccountId
            .SalesAccountId = SalesAccountId
            .DiscountAccountId = Me.DiscountAccountId

            .FunctionalUnitDescription = INDsleFunctionalUnit.Text
            .CostAccountDescription = INDsleCostAccount.Text
            .SalesAccountDescription = INDsleSalesAccount.Text
            .DiscountAccountDescription = INDsleDiscountAccount.Text
        End With
        ListProductGroupFuntionalUnit.Add(ProductGroupFunctionalUnit)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListProductGroupFuntionalUnit
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("DetailAgregatedSatisfactory", NAME_MODULE)
        CleanControlsPopup()
        INDsleFunctionalUnit.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim sifu As ProductGroupFunctionalUnit = CType(viewGridControl.GetFocusedRow, ProductGroupFunctionalUnit)
        If sifu.Id <> 0 Then
            If ListDeleteProductGroupFuntionalUnit Is Nothing Then
                ListDeleteProductGroupFuntionalUnit = New List(Of ProductGroupFunctionalUnit)
            End If
            sifu.MarkAsDeleted()
            ListDeleteProductGroupFuntionalUnit.Add(sifu)
        End If
        ListProductGroupFuntionalUnit.Remove(sifu)
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListProductGroupFuntionalUnit
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If FunctionalUnitId Is Nothing Then
            listErrors.Append(ResourceManager.GetString("SelectFuntionalUnit", NAME_MODULE))
        End If
        If CostAccountId Is Nothing Then
            listErrors.Append(ResourceManager.GetString("SelectCostAccount", NAME_MODULE))
        End If
        If SalesAccountId Is Nothing Then
            listErrors.Append(ResourceManager.GetString("SelectSalesAccount", NAME_MODULE))
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        FunctionalUnitId = Nothing
        CostAccountId = Nothing
        SalesAccountId = Nothing
        Me.DiscountAccountId = Nothing
    End Sub

#End Region

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            _isLoading = True
            Try
                Using Model As New MGroup(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetProductGroup(INDbtnCode.Text.Trim)
                    INDlyGroup.BeginUpdate()
                    productGroup = resultOperation.ObjectEmbbeded
                    If productGroup IsNot Nothing AndAlso productGroup.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(productGroup.Id))
                            With productGroup
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Code = .Code
                                Description = .Name
                                IdClass = .GroupClass
                                IdAttentionSub = .SubclassCode

                                IncomeAccountId = .IncomeAccountId
                                INDsleIncomeAccountId.Properties.NullText = .IncomeAccountDescription

                                InventoryAccountPayableConceptId = .InventoryAccountPayableConceptId
                                INDsleInventoryAccountPayableConceptId.Properties.NullText = .InventoryAccountPayableConceptDescription

                                DeclarantRetentionAccountPayableConceptId = .DeclarantRetentionAccountPayableConceptId
                                INDSleDeclarantRetentionAccountPayableConceptId.Properties.NullText = .DeclarantRetentionAccountPayableConceptDescription

                                NotDeclarantRetentionAccountPayableConceptId = .NotDeclarantRetentionAccountPayableConceptId
                                INDSleNotDeclarantRetentionAccountPayableConceptId.Properties.NullText = .NotDeclarantRetentionAccountPayableConceptDescription

                                IdCostCenter = .CostCenterId
                                INDsleCostCenter.Properties.NullText = .CostCenterDescription

                                IncomeRecognitionMainAccountId = .IncomeRecognitionMainAccountId
                                INDsleIncomeRecognitionMainAccountId.Properties.NullText = .IncomeRecognitionMainAccountDescription

                                IVAAccountId = .IVAAccountId
                                INDPucIVAAccount.Properties.NullText = .CodeNameIVAAccount

                                InventoryCostMainAccountId = .InventoryCostMainAccountId
                                INDsleInventoryCostMainAccount.Properties.NullText = .InventoryCostMainAccountDescription

                                WithholdingTaxAccountId = .WithholdingTaxAccountId
                                INDPucWithholdingTaxAccount.Properties.NullText = .CodeNameWithholdingTaxAccount

                                WithholdingICAAccountId = .WithholdingICAAccountId
                                INDPucWithholdingICAAccount.Properties.NullText = .CodeNameWithholdingICAAccount

                                ReteFuenteConceptId = .ReteFuenteConceptId
                                INDsleReteFuenteConcept.Properties.NullText = .ReteFuenteConceptDescription

                                WithholdingICAConceptId = .WithholdingICAConceptId
                                INDSleWithholdingICAConcept.Properties.NullText = .CodeNameWithholdingICAConcept

                                ReferenceInputDebitAccountId = .ReferenceInputDebitAccountId
                                INDsleReferenceInputDebitAccountId.Properties.NullText = .RemissionInputDebitDescription

                                ReferenceInputCreditAccountId = .ReferenceInputCreditAccountId
                                INDsleReferenceInputCreditAccountId.Properties.NullText = .RemissionInputCreditDescription

                                ReferenceOutputCreditAccountId = .ReferenceOutputCreditAccountId
                                INDsleReferenceOutputCreditAccountId.Properties.NullText = .RemissionOutputCreditDescription

                                ReferenceOutputDebitAccountId = .ReferenceOutputDebitAccountId
                                INDsleReferenceOutputDebitAccountId.Properties.NullText = .RemissionOutputDebitDescription

                                ConsignmentMerchandiseDebitAccountId = .ConsignmentMerchandiseDebitAccountId
                                INDsleConsignmentMerchandiseDebitAccountId.Properties.NullText = .ConsignmentMerchandiseDebitDescription

                                ConsignmentMerchandiseCreditAccountId = .ConsignmentMerchandiseCreditAccountId
                                INDsleConsignmentMerchandiseCreditAccountId.Properties.NullText = .ConsignmentMerchandiseCreditDescription

                                CounterpartCostConsignedInventoryId = .CounterpartCostConsignedInventoryId
                                INDsleCounterpartCostConsignedInventoryId.Properties.NullText = .CounterpartCostConsignedInventoryDescription

                                ExcludeFreightCosts = .ExcludeFreightCosts
                                ProductReplacementTime = .ProductReplacementTime
                                ProductsSourcingTime = .ProductsSourcingTime
                                SecurityPercentage = .SecurityPercentage
                                Status = .Status
                                If .EconomicActivityId IsNot Nothing Then
                                    EconomicActivity = .EconomicActivityId
                                End If

                                ListProductGroupFuntionalUnit = .ProductGroupFunctionalUnit.ToList
                                INDgcDetail.DataSource = Nothing
                                INDgcDetail.DataSource = ListProductGroupFuntionalUnit

                                AffectBudget = .AffectBudget
                                If AffectBudget Then
                                    BudgetaryEntityId = .BudgetaryEntityId
                                    INDsleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                    BudgetaryValidityId = .BudgetaryValidityId
                                    INDsleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                    BudgetId = .BudgetId
                                    INDsleBudget.Properties.NullText = .BudgetDescription
                                End If

                                AccountingPackageMainAccountId = .AccountingPackageMainAccountId
                                FavorableDeviationMainAccountId = .FavorableDeviationMainAccountId
                                VariationPVMainAccountId = .VariationPVMainAccountId
                                'Control
                                INDsleAccountPackage.Properties.NullText = .CodeNameAccountingPackageMainAccount
                                INDsleAccountFavorableVariation.Properties.NullText = .CodeNameFavorableDeviationMainAccount
                                INDsleAccountPriceVariation.Properties.NullText = .CodeNameVariationPVMainAccount

                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.productGroup.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = productGroup.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(productGroup.Id, Me.Tag.ToString(), Nothing, GetType(ProductGroup).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewGroup()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyGroup.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
        _isLoading = False
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewGroup() As Task
        If Me._parameterInventory Is Nothing OrElse Me._parameterInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Inventario para la unidad operativa seleccionada"
            Exit Function
        End If

        productGroup = New ProductGroup() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.productGroup.Code) Then
            Try
                Using model As New MGroup(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not productGroup.Status
                    Dim result As ActionResult(Of ProductGroup) = Await model.ChangeState(Me.productGroup.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.productGroup = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class