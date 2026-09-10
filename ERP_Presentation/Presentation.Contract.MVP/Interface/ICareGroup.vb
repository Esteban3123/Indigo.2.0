'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/11/2014
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

Public Interface ICareGroup
    Inherits ICrudBase

    ''' <summary>
    ''' Esta propiedad contiene el centro de costo
    ''' </summary>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property CostCenterXpo As XPInstantFeedbackSource

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
    ''' Obtiene o establece el consecutivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NameCG As String

    ''' <summary>
    ''' Obtiene o establece el tipo de grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CareGroupType As Byte?

    ''' <summary>
    ''' Obtiene o establece el id del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractId As Integer?

    ''' <summary>
    ''' Establece el datasource de contratos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidationType As Integer?

    ''' <summary>
    ''' Obtiene o establece el periodo de facturacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingPeriod As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidación del oxigeno
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TypeLiquidationOxygen As Integer

    ''' <summary>
    ''' Obtiene o establece el tope maximo de facturacion individual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MaximumIndividualBilling As Decimal

    ''' <summary>
    ''' Obtiene o establece el tope maximo de la facturacion por periodo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PeriodMaximumBilling As Decimal

    ''' <summary>
    ''' Obtiene o establece el id de la plantilla de requerimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RequirementsTemplateId As Integer?

    ''' <summary>
    ''' Establece el datasource de la plantilla de requerimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RequirementsTemplateXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el plazo de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceDeadlines As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la plantilla de procedimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProcedureTemplateId As Integer?

    ''' <summary>
    ''' Establece el datasource de la plantilla de procedimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProcedureTemplateXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la plantilla de productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductRateId As Integer?

    ''' <summary>
    ''' Establece el datasource de la plantilla de productos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProductTemplateXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el concepto a facturar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConceptToBill As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntityType As Byte?

    ''' <summary>
    ''' obtiene parametro de si se dispensa extramural o no
    ''' </summary>
    ''' <returns></returns>
    Property ExtramuralPharmaceuticalDispensing As Boolean?

    ''' <summary>
    ''' obtiene o establece el porcentaje de descuento especifico al cliente
    ''' </summary>
    ''' <returns></returns>
    Property DiscountContractedCustomer As Decimal

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Obtiene o establece el tipo de liquidacion para estancias de urgencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TypeLiquidationEmergencyStays As Integer?

    ''' <summary>
    ''' Obtiene o establece si liquida dia de egreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidateDay As Boolean

    ''' <summary>
    ''' Obtiene o establece las horas minimas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MinimumObservationTime As Int16

    ''' <summary>
    ''' Obtiene o establece las horas maximas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MaximumObservationTime As Int16

    ''' <summary>
    ''' Obtiene o establece las horas de recuperacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HoursOfRecoveryIncluded As Int16

    ''' <summary>
    ''' Obtiene o establece el id de la definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DefinitionRateId As Integer

    ''' <summary>
    ''' Establece el datasource del control de definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DefinitionRateXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la fecha inicial 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EndDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece el id del servicio no facturable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingItemsRestrictionId As Integer

    ''' <summary>
    ''' Establece el datasource de los servicios n facturables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingItemsRestrictionXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la estructura contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractAccountingStructureId As Integer?

    ''' <summary>
    ''' Datasource de la estructura contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ContractAccountingStructureXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Permite saber si requiere autorización
    ''' </summary>
    ''' <returns></returns>
    Property AuthorizationRequired As Boolean

    ''' <summary>
    ''' Obtiene o establece el id de la nota tecnica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TechnicalNoteId As Integer?

    ''' <summary>
    ''' Establece el datasource de notas tecnicas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TechnicalNoteXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece o obitiene los tipos de entidad
    ''' </summary>
    ''' <returns></returns>
    Property EntityTypeXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Método Reporte Recaudo Monto Fijo
    ''' </summary>
    ''' <returns></returns>
    Property CollectionMethod As Integer

#Region "Budget Interface"

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
    Property BillingBudgetId As Integer?

    ''' <summary>
    ''' Datasource del rubro presupuesto para facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillingBudgetXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del rubro de presupuesto para pagarés
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PromissoryNoteBudgetId As Integer?

    ''' <summary>
    ''' Datasource del rubro de presupuesto para pagarés
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PromissoryNoteBudgetXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del rubro de presupuesto para pago de facturas de la vigencia inmediatamente anterior
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountReceivablePreviousValidityBudgetId As Integer?

    ''' <summary>
    ''' Datasource del rubro de presupuesto para pago de facturas de la vigencia inmediatamente anterior
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountReceivablePreviousValidityBudgetXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del rubro de presupuesto para pago de facturas de periodos mucho más antiguos que la anterior vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PortfolioRecoveryBudgetId As Integer?

    ''' <summary>
    ''' Datasource del rubro de presupuesto para pago de facturas de periodos mucho más antiguos que la anterior vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PortfolioRecoveryBudgetXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' DataSource de los paquetes para asociar al grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    Property ContractPackageXpo As XPInstantFeedbackSource

#End Region

End Interface
