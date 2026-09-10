'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region

Public Interface IEquipmentCatalog

    Inherits ICrudBase

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' codigo del Catálogo del Equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Permite saber si maneja iva deducible para bienes de capital
    ''' </summary>
    ''' <returns></returns>
    Property IVADeductible As Boolean

    ''' <summary>
    ''' detalle del Catálogo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Id de la Cuenta de Ingreso de Leasing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IngressLeasingAccountId As Integer?

    ''' <summary>
    ''' Id de la cuenta comodato Débito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LoanDebitAccountId As Integer?

    ''' <summary>
    ''' Id de la cuenta comodato Crédito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LoanCreditAccountId As Integer?

    ''' <summary>
    ''' Id de la Cuenta de Depreciación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciationAccountId As Integer?

    Property ValorizationDebitAccountId As Integer?

    Property ValorizationCreditAccountId As Integer?

    Property DevaluationDebitAccountId As Integer?

    Property DevaluationCreditAccountId As Integer?

    Property CatalogActive As Boolean

    Property IncomeAccountPayableConceptId As Integer?

    Property DeclarantRetentionAccountPayableConceptId As Integer?

    Property NotDeclarantRetentionAccountPayableConceptId As Integer?

    Property NetIncomeAccountId As Integer?

    Property LossMainAccountId As Integer?

    Property ReplacementCreditMainAccountId As Integer?

    Property WarehouseAssetsMainAccountId As Integer?

    Property MaintenanceAssetsMainAccountId As Integer?

    Property DepreciationLeasingAccountId As Integer?

    Property IncomeAccountId As Integer

    Property CreditEquipmentPlantAcumulatedAccountId As Integer?

    Property AccumulatedDeteriorationAccountId As Integer?

    Property LoanLeasingAccountId As Integer?

    Property FinancialRentingAccountId As Integer?

    ''' <summary>
    ''' Estado del registro
    ''' </summary>
    ''' <returns></returns>
    Property Status As Boolean

    ''' <summary>
    ''' Tipo de adquisición
    ''' </summary>
    ''' <returns></returns>
    Property AdquisitionType As Integer

    ''' <summary>
    ''' Id del libro
    ''' </summary>
    ''' <returns></returns>
    Property LegalBookId As Integer

    ''' <summary>
    ''' Datasource del libro
    ''' </summary>
    ''' <returns></returns>
    Property LegalBookXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Property MainAccountId As Integer

    ''' <summary>
    ''' Datasource de la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Property MainAccountXpo As XPInstantFeedbackSource

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
    Property WithholdingTaxConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource de los conceptos de retencion de la fuente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingTaxConceptXpo As XPInstantFeedbackSource

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
    ''' Clasificacion de Activo Fijo e Intangibles
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Classification As Byte


End Interface
