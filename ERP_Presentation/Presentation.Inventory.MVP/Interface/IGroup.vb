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

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IGroup
    Inherits IcrudBase

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el id de la clase
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdClass As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la subAtencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAttentionSub As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de los ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IncomeAccountId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de ingresos
    ''' </summary>
    Property IncomeAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de reconocimiento de ingresos pendientes por facturar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IncomeRecognitionMainAccountId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas para el reconocimiento de ingresos pendientes por facturar
    ''' </summary>
    Property IncomeRecognitionMainAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del concepto de cxp sin retencion y de tipo especifico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InventoryAccountPayableConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InventoryAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenter As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de debito remision entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReferenceInputDebitAccountId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de debito remision entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReferenceInputDebitAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de credito remision entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReferenceInputCreditAccountId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de credito remision entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReferenceInputCreditAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de credito remision salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReferenceOutputCreditAccountId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de credito remision salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReferenceOutputCreditAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de debito remision salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReferenceOutputDebitAccountId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de debito remision salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReferenceOutputDebitAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establce el id de la cuenta debito para realizar los movimientos de inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConsignmentMerchandiseDebitAccountId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas para el inventario en consignación débito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConsignmentMerchandiseDebitAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establce el id de la cuenta crédito para realizar los movimientos de inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConsignmentMerchandiseCreditAccountId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas para el inventario en consignación crédito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConsignmentMerchandiseCreditAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establce el id de la cuenta contrapartida al costo del movimiento de inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CounterpartCostConsignedInventoryId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas para la contrapartida al costo del movimiento de inventario en consignación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CounterpartCostConsignedInventoryIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Obtiene o establece el tiempo de reposicion de los productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductReplacementTime As Integer

    ''' <summary>
    ''' Obtiene o establece el tiempo de abastecimiento de los productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductsSourcingTime As Integer

    ''' <summary>
    ''' Obtiene o establece el procentaje de seguridad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SecurityPercentage As Decimal

    ''' <summary>
    ''' Obtiene o establece el id del concepto de cxp de tipo retencion y especifico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeclarantRetentionAccountPayableConceptId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del concepto de cxp de tipo retencion y especifico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NotDeclarantRetentionAccountPayableConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeclarantRetentionAccountPayableConceptIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NotDeclarantRetentionAccountPayableConceptIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece si se realiza el proceso de costo a los productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExcludeFreightCosts As Boolean?

    Property InventoryCostMainAccountId As Integer?

    Property InventoryCostMainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAAccountId As Integer?

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingTaxAccountId As Integer?

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingTaxAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingICAAccountId As Integer?

    ''' <summary>
    ''' Establece el datasource del cuentas para iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingICAAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion de la fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReteFuenteConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion de la fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReteFuenteConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion de ica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingICAConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion para ica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingICAConceptXpo As XPInstantFeedbackSource

    Property AccounttingPackageMainAccountId As Integer?

    Property FavorableDeviationMainAccountId As Integer?

    Property VariationPVMainAccountId As Integer?

    Property AccountingPackageMainAccountIdXpo As XPInstantFeedbackSource

    Property FavorableDeviationMainAccountIdXpo As XPInstantFeedbackSource

    Property VariationPVMainAccountIdXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Datasource de actividad Economica
    ''' </summary>
    ''' <returns></returns>
    Property EconomicActivityDatasource As XPInstantFeedbackSource

#Region "Configuración por Unidad Funcional"

    ''' <summary>
    ''' Obtiene o establece el id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitId As Integer?

    ''' <summary>
    ''' Establece el datasource de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable del costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostAccountId As Integer?

    ''' <summary>
    ''' Establece el datasource de la cuenta contable del costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la cuenta contable de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesAccountId As Integer?
    ''' <summary>
    ''' Establece el datasource de la cuenta contable de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Indica si se afecta presupuesto
    ''' </summary>
    Property AffectBudget As Boolean

    ''' <summary>
    ''' Id de la entidad de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityId As Integer?

    ''' <summary>
    ''' Datasource de entidades de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityId As Integer?

    ''' <summary>
    ''' Datasource de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del rubro presupuesto para facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetId As Integer?

    ''' <summary>
    ''' Datasource del rubro presupuesto para facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la cuenta contable de descuento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DiscountAccountId As Integer?

    ''' <summary>
    ''' Establece el datasource de la cuenta contable de ventas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DiscountAccountXpo As XPInstantFeedbackSource

#End Region

End Interface
