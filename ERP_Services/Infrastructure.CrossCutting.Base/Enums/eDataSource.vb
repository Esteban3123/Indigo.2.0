''' <summary>
''' Enumeracion para las consultas del frontal de busquedas
''' </summary>
Public Enum eDataSource

    ListBranchOffice

    ListLogisticsProductionCenterRecord

    ''' <summary>
    ''' Obtiene los parametros de inventario por id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    GetSettingInventoryByOperatingUnit
    ''' <summary>
    ''' lista todos los detalles de la orden de compra por proveedor, linea de distribuccion y codigo de usuario que tenga permiso a los almacenes
    ''' </summary>
    ''' <remarks></remarks>
    ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineIdAndCodeUser
    ''' <summary>
    ''' lista todos los subdetalles de la remision de entrada por proveedor, linea de distribuccion y codigo de usuario que tenga permiso a los almacenes
    ''' </summary>
    ''' <remarks></remarks>
    ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineIdAndCodeUser

    ListAllInventoryPurchaserOrderDevolution
    ''' <summary>
    ''' lista las boletas de salida con el nit y nombre del paciente
    ''' </summary>
    ''' <remarks></remarks>
    ListSlipOutAndAdmissionNumber
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetInputRemission

    ''' <summary>
    '''  
    ''' </summary>
    ListPaymentSupportPaymentSuppliers

    ''' <summary>
    ''' Lista los grupos de autorizaciones
    ''' </summary>
    ListAuthorizationGroup

    ''' <summary>
    ''' Lista las autorizaciones de servicios tercerizados
    ''' </summary>
    ListAuthorizationOutsourcedServices

    ''' <summary>
    ''' Lista los portafolios de autorizaciones
    ''' </summary>
    ListAuthorizationPortfolio

    ''' <summary>
    ''' Lista los motivos de cancelación
    ''' </summary>
    ListCancellationReasons

    ''' <summary>
    ''' Lista los rechazos de autorización
    ''' </summary>
    ListAuthorizationRejection

    ''' <summary>
    ''' Lista los origenes de autorización
    ''' </summary>
    ListAuthorizationSource

    ''' <summary>
    ''' Lista los turnos de autorización
    ''' </summary>
    ListAuthorizationScheduleTemplate

    ''' <summary>
    ''' Lista las devoluciones parciales de venta
    ''' </summary>
    ListDocumentInvoiceProductSalesDevolution

    ''' <summary>
    ''' Lista las cotizaciones
    ''' </summary>
    ListQuotation

    ''' <summary>
    ''' Lista todas las formulas medicas registradas
    ''' </summary>
    ListAllMedicalFormula

    ''' <summary>
    ''' Lista las estructuras presupuestales
    ''' </summary>
    ListPrivateBudgetItemsStructure

    ''' <summary>
    ''' Lista las sucursales con compañia nulas
    ''' </summary>
    ListBranchOfficeByCompanyIsNull

    ''' <summary>
    ''' Listado de fondo de caja menor
    ''' </summary>
    ''' <remarks></remarks>
    ListConstitutionCashSmaller

    ''' <summary>
    ''' Lista la distribución secundaria de costos nativo
    ''' </summary>
    ''' <remarks></remarks>
    ListCostDirectDistributionSecondary

    ''' <summary>
    ''' Listado de producción de centros logísticos
    ''' </summary>
    ''' <remarks></remarks>
    ListCostLogisticsProductionCenterRecord

    ''' <summary>
    ''' Lista las categorias de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    ListCostGeneralExpenseCategory

    ''' <summary>
    ''' Lista costo estándar promedio
    ''' </summary>
    ListStandarCost

    ''' <summary>
    ''' Lista las categorias de elementos del costo
    ''' </summary>
    ''' <remarks></remarks>
    ListGeneralExpenseCategory

    ''' <summary>
    ''' Lista todas las provisiones
    ''' </summary>
    ''' <remarks></remarks>
    ListPortfolioProvision

    ''' <summary>
    ''' Listado de distribuciones de activos fijos en costos
    ''' </summary>
    ''' <remarks></remarks>
    ListCostDistributionFixedAsset

    ''' <summary>
    ''' Devoluciones de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    ListRequestDevolution
    ''' <summary>
    ''' Lista todas las estructuras contables de contratos
    ''' </summary>
    ''' <remarks></remarks>
    ListContractAccountingStructure

    ''' <summary>
    ''' Lista todos los predios
    ''' </summary>
    ''' <remarks></remarks>
    ListTaxesProperty
    ''' <summary>
    ''' Lista las devoluciones de ingreso de activos
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetEntryDevolution
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetPolizaType
    ''' <summary>
    ''' Cambio de placa
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetChangePlate
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ListPharmaceuticalDispensingByFilter
    ''' <summary>
    ''' lista los rubros por filter
    ''' </summary>
    ''' <remarks></remarks>
    ListCategoryByFilter
    ''' <summary>
    ''' lista las ordenes de traslados por un filtro
    ''' </summary>
    ''' <remarks></remarks>
    ListTransferOrderByFilter
    ''' <summary>
    ''' lista los radicados de cartera
    ''' </summary>
    ''' <remarks></remarks>
    ListRadicateInvoice
    ''' <summary>
    ''' lista los radicados Reporte
    ''' </summary>
    ''' <remarks></remarks>
    ListRadicateInvoiceReport
    ''' <summary>
    ''' lista las facturas
    ''' </summary>
    ''' <remarks></remarks>
    ListViewListInvoiceAndPatient
    ''' <summary>
    ''' lista las facturas
    ''' </summary>
    ''' <remarks></remarks>
    ListInvoice
    ''' <summary>
    ''' lista las boletas de salida
    ''' </summary>
    ''' <remarks></remarks>
    ListSlipOut
    ''' <summary>
    ''' lista los ingresos por id de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    ListAdmissionsToReportById
    ''' <summary>
    ''' lista los ingresos por id de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    ListAdmissionsToReportSlipOutById
    ''' <summary>
    ''' lista los ingresos con fecha de salida
    ''' </summary>
    ''' <remarks></remarks>
    ListAdmissionsToReportSlipOut
    ''' <summary>
    ''' lista los ingresos
    ''' </summary>
    ''' <remarks></remarks>
    ListAdmissionsToLiquidation
    ''' <summary>
    ''' Lista las facturas a entidades capitadas
    ''' </summary>
    ListInvoiceEntityCapitated
    ''' <summary>
    ''' The list sequence patterns
    ''' </summary>
    ListSequencePatterns
    ''' <summary>
    ''' Lista todos los comprobantes contables
    ''' </summary>
    ''' <remarks></remarks>
    ListAllInventoryGeneralLedgerJournalVoucherTypes
    ''' <summary>
    ''' lista las devoluciones de remisiones
    ''' </summary>
    ''' <remarks></remarks>
    ListAllRemissionDevolution
    ''' <summary>
    ''' lista las remisiones de salida
    ''' </summary>
    ''' <remarks></remarks>
    ListAllRemissionOutput
    ''' <summary>
    ''' Lista todas las Órdenes de Compra
    ''' </summary>
    ''' <remarks></remarks>
    ListAllInventoryPurchaserOrder
    ''' <summary>
    ''' lista las remisionde de entrada
    ''' </summary>
    ''' <remarks></remarks>
    ListAllRemissionEntrance
    ''' <summary>
    ''' lista las remisionde de entrada
    ''' </summary>
    ''' <remarks></remarks>
    ListAllProductInTransit
    ''' <summary>
    ''' lista las remision de inventario en consignacion
    ''' </summary>
    ''' <remarks></remarks>
    ListAllConsignmentInventoryRemission
    ''' <summary>
    ''' lista las devoluciones de suministros
    ''' </summary>
    ''' <remarks></remarks>
    ListAllPharmaceuticalDispensingDevolution
    ''' <summary>
    ''' Lista todos los items de control de inventario
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryControl
    ''' <summary>
    ''' Lista todos los items de control de inventario por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryControlByStatus
    ''' <summary>
    ''' Lista todos los items de ajuste de inventario
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryAdjustment
    ''' <summary>
    ''' Lista todos los items de ajuste de inventario por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryAdjustmentByStatus
    ''' <summary>
    ''' Lista todos los comprobantes de entrada
    ''' </summary>
    ''' <remarks></remarks>
    ListEntranceVoucher
    ''' <summary>
    ''' Lista todos los comprobantes de entrada por stado
    ''' </summary>
    ''' <remarks></remarks>
    ListEntranceVoucherByStatus
    ''' <summary>
    ''' Lista los ajustes de inventario
    ''' </summary>
    ''' <remarks></remarks>
    ListInventaryAdjustments
    ''' <summary>
    ''' Lista los ajustes de inventario por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListInventaryAdjustmentsByStatus
    ''' <summary>
    ''' Lista todos los comprobantes de entrada
    ''' </summary>
    ''' <remarks></remarks>
    ListEntranceVoucherDevolution
    ''' <summary>
    ''' Lista todos los comprobantes de entrada por stado
    ''' </summary>
    ''' <remarks></remarks>
    ListEntranceVoucherDevolutionByStatus
    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <remarks></remarks>
    ListOperatingUnit
    ''' <summary>
    ''' Lista todas las unidades operativas XPCollection
    ''' </summary>
    ''' <remarks></remarks>
    ListOperatingUnitTreeList
    ''' <summary>
    ''' lista los Contratos
    ''' </summary>
    ''' <remarks></remarks>
    ListAllInventoryContracts
    ''' <summary>
    ''' lista los Contratos
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryContractByStatus
    ''' <summary>
    ''' lista las ordenes de servicio
    ''' </summary>
    ''' <remarks></remarks>
    ListAllServiceOrder
    ''' <summary>
    ''' lista los traslados de cartera
    ''' </summary>
    ''' <remarks></remarks>
    ListPortfolioTransfers
    ''' <summary>
    ''' DataSource de Usuarios para el frontal de CRUD
    ''' </summary>
    ''' <return>Consulta de Codigo, y Nombre de usuario, con XPO</return>
    UsersCRUD

    ''' <summary>
    ''' DataSource de Grupos para el frontal de CRUD
    ''' </summary>
    ''' <return>Consulta de Codigo, y Nombre del grupo, con XPO</return>
    GroupsCRUD

    ''' <summary>
    ''' DataSource de Roles para el frontal de CRUD
    ''' </summary>
    ''' <return>Consulta de Codigo, y Nombre del rol, con XPO</return>
    RolesCRUD

    ''' <summary>
    ''' DataSource de Conceptos Generales para el frontal de Terceros
    ''' </summary>
    Customers

    ''' <summary>
    ''' DataSource de Conceptos Generales para el frontal de Conceptos detallados
    ''' </summary>
    DetailedConcepts

    ''' <summary>
    ''' DataSource de Conceptos Generales para el frontal de Conceptos generales
    ''' </summary>
    GeneralConcepts

    ''' <summary>
    ''' DataSource de Conceptos Generales para el frontal de Responsables
    ''' </summary>
    Responsible

    ''' <summary>
    ''' DataSource de Conceptos de Jerarquía de Aceptaciones de glosas
    ''' </summary>
    ''' <remarks></remarks>
    ResponseHierarchy

    ''' <summary>
    ''' DataSource de Conceptos Generales para el frontal de Conceptos especificos
    ''' </summary>
    SpecificConcepts

    ''' <summary>
    ''' DataSource de empresas para el frontal de empresas
    ''' </summary>
    Company

    ''' <summary>
    ''' DataSource de Niveles de cargo para el frontal de Niveles de cargo
    ''' </summary>
    PositionLevel

    ''' <summary>
    ''' DataSource de objeciones de recepcion  para el frontal de objeciones 
    ''' </summary>
    ObjectionsReceptionC

    ''' <summary>
    ''' DataSource de paises parael frontal paises
    ''' </summary>
    ''' <remarks></remarks>
    Country

    ''' <summary>
    ''' DataSource de departamentos para el frontal departamentos
    ''' </summary>
    ''' <remarks></remarks>
    Department

    ''' <summary>
    ''' DataSource de ciudades para el frontal ciudades
    ''' </summary>
    ''' <remarks></remarks>
    City

    ''' <summary>
    ''' DataSource de centros de atención para el frontal de centros de atención
    ''' </summary>
    ''' <remarks></remarks>
    HealthCenter

    ''' <summary>
    ''' DataSource de Niveles de educacion para el forntal de niveles de educacion
    ''' </summary>
    ''' <remarks></remarks>
    EducationLevels

    ''' <summary>
    ''' DataSource de Professions para el frontal de profesiones
    ''' </summary>
    ''' <remarks></remarks>
    Professions

    ''' <summary>
    ''' data source para listar las cuentas contables del nivel 5
    ''' </summary>
    ListAccountsByLevelReport

    ''' <summary>
    ''' data source para listar las cuentas contables del nivel 5 y que manejen terceros
    ''' </summary>
    ListAccountsByLevelHandlesThirdReport

    ''' <summary>
    ''' data source para listar las cuentas contables del nivel 5 y de patrimonio
    ''' </summary>
    ListAccountsByLevelPatrimonial

    ''' <summary>
    ''' Listado de rubros presupuestales por id de la vigencia y el tipo
    ''' </summary>
    ''' <remarks></remarks>
    ListCategoryByBudgetaryValidityIdAndItemType

    ''' <summary>
    ''' data source para listar las cuentas contables del nivel 5
    ''' </summary>
    ListTypeVoucherRepor

    ''' <summary>
    ''' data source para listar todos los comprobantes
    ''' </summary>
    ListVoucherReport

    ''' <summary>
    ''' data source para listar todos los comprobantes con filtro
    ''' </summary>
    ListVoucherReportFilter

    ''' <summary>
    ''' data source para listar las cuentas bancarias
    ''' </summary>
    ListEntityBankAccountsReport

    ListAllEntityBankAccount

    ListAllCashReceiptConcept

    ''' <summary>
    ''' data source para listar las cajas
    ''' </summary>
    ListCashRegistersEntity

    ''' <summary>
    ''' Lista todos los registros de cajas
    ''' </summary>
    ListAllCashRegister

    ''' <summary>
    ''' data source para listar todos los Centros de costos
    ''' </summary>
    ListCostcenterReport
    ''' <summary>
    ''' Lista todas las actividades economicas
    ''' </summary>
    ''' <remarks></remarks>
    ListEconomicActivity
    ''' <summary>
    ''' Lista las actividades economicas por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListEconomicActivityByStatus

    ''' <summary>
    ''' Lista todas las exoneraciones tributarias
    ''' </summary>
    ''' <remarks></remarks>
    ListTaxExemptions

    ''' <summary>
    ''' Retorna la fecha del ultimo cierre contable
    ''' </summary>
    GetLastClosingDate
    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <remarks></remarks>
    ListAllThirdParty
    ''' <summary>
    ''' lista la distribucion de activos fijos
    ''' </summary>
    ListDistributionFixedAsset
    ''' <summary>
    ''' data source para listar todos los terceros
    ''' </summary>
    ListThirdPartyReport
    ''' <summary>
    ''' Lista los elementos de distribución secundaria
    ''' </summary>
    ListCostDistributionSecondary
    ListCostIntermediatetDistribution
    ''' <summary>
    ''' Lista la distribucion secundaria por año y mes
    ''' </summary>
    ListDistributionSecondaryByYearMonth
    ''' <summary>
    ''' data source para listar todos las Cuentas de Cruce
    ''' </summary>
    ListCrossingAccountReportTreasury
    ''' <summary>
    ''' data source para listar todos las Cuentas de Cruce por filtro
    ''' </summary>
    ListCrossingAccountReportTreasuryFilter
    ''' <summary>
    ''' data source para listar todos los terceros
    ''' </summary>
    ListThirdPartyReportTreasury
    ''' <summary>
    ''' data source para listar todos los terceros
    ''' </summary>
    ListThirdPartyReportPayments
    ''' <summary>
    ''' data source para listar todos los Clientes
    ''' </summary>
    ListCustomerReportPortfolio
    ''' <summary>
    ''' data source para listar todos los Vendedores
    ''' </summary>
    ListSellerReportPortfolio
    ''' <summary>
    ''' data source para listar todas las Facturas
    ''' </summary>
    ListInvoiceReportPortfolio
    ''' <summary>
    ''' data source para listar todas las Facturas por filtro
    ''' </summary>
    ListInvoiceReportPortfolioFilter
    ''' <summary>
    ''' data source para listar todos anticipos por filtro
    ''' </summary>
    ListAdvancesReportPortfolioFilter
    ''' <summary>
    ''' data source para listar todos los grupos de atención
    ''' </summary>
    ListCareGroupReportPortfolio
    ''' <summary>
    ''' data source para listar todas las cuentas contables
    ''' </summary>
    ListMainAccountsReportPortfolio
    ''' <summary>
    ''' obtiene un listado de los documentos reclasificados
    ''' </summary>
    ListPortfolioReclassification
    ''' <summary>
    ''' data source para listar todas las cuentas contables por filtro
    ''' </summary>
    ListMainAccountsReportByFilter
    ''' <summary>
    ''' data source para listar todos los traslados
    ''' </summary>
    ListTransfersReportPayments
    ''' <summary>
    ''' data source para listar todos los traslados por filtro
    ''' </summary>
    ListTransfersReportPaymentsFilter
    ''' <summary>
    ''' data source para listar todos los comprobantes de transacción
    ''' </summary>
    ListVoucherTranscationReportTreasury
    ''' <summary>
    ''' data source para listar todos los comprobantes de transacción por filtro
    ''' </summary>
    ListVoucherTranscationReportTreasuryFilter
    ''' <summary>
    ''' data source para listar todos los usuarios de creación
    ''' </summary>
    ListCreationUsersReportTreasury
    ''' <summary>
    ''' data source para listar todos los recibos de caja
    ''' </summary>
    ListCashReceiptReport
    ''' <summary>
    ''' data source para listar todos los recibos de caja
    ''' </summary>
    ListNoteReportTreasury
    ''' <summary>
    ''' data source para listar todos los recibos de caja con filtro
    ''' </summary>
    ListNoteReportTreasuryByFilter
    ''' <summary>
    ''' data source para listar todas las ubicaciones con filtro
    ''' </summary>
    ListFixedAssetLocationByFilter
    ''' <summary>
    ''' data source para listar todos las consignacions y transferencias de tesoreria
    ''' </summary>
    ListVReportConsignmentTransferTreasury
    ''' <summary>
    ''' data source para listar todos las consignacions y transferencias de tesoreria con filtro
    ''' </summary>
    ListVReportConsignmentTransferTreasuryByFilter
    ''' <summary>
    ''' data source para listar todos los reembolsos de tesoreria
    ''' </summary>
    ListRefundsReportTreasury
    ''' <summary>
    ''' data source para listar todos los reembolsos de tesoreria con filtro
    ''' </summary>
    ListRefundsReportTreasuryByFilter
    ''' <summary>
    ''' data source para listar todos los cheques cancelados de tesoreria
    ''' </summary>
    ListCancellationChecksReportTreasury
    ''' <summary>
    ''' data source para listar todos los conceptos de los comprobantes de egreso
    ''' </summary>
    ListExpensesConceptsReport
    ''' <summary>
    ''' data source para listar todos los conceptos de los recibos de caja
    ''' </summary>
    ListReceiptConceptsReport
    ''' <summary>
    ''' data source para listar todos los comprobantes de egreso que se paguen con cheques
    ''' </summary>
    ListVoucherTranscationCheckReportTreasury
    ''' <summary>
    ''' data source para listar todos los comprobantes de egreso que se paguen con cheques por filtro
    ''' </summary>
    ListVoucherTranscationCheckReportTreasuryByFilter
    ''' <summary>
    ''' data source para listar todos las cuentas bancarias
    ''' </summary>
    ListEntityBankReportTreasury
    ''' <summary>
    ''' data source para listar todos los proveedores
    ''' </summary>
    ListSupplierReportTreasury
    ''' <summary>
    ''' data source para listar todos los proveedores
    ''' </summary>
    ListPaymentsAdvancesReportTreasury
    ''' <summary>
    ''' Lista todas las definiciones de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    ListDefinitionRate
    ''' <summary>
    ''' Lista todas las definiciones de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    ListDefinitionRateByStatus
    ''' <summary>
    ''' Lista los traslados de presupuesto por tipo
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetTransferByType
    ''' <summary>
    ''' Lista las modificaciones de obligaciones por vigencia
    ''' </summary>
    ''' <remarks></remarks>
    ListObligationModificationByValidityId
    ''' <summary>
    ''' Lista los reconocimientos
    ''' </summary>
    ''' <remarks></remarks>
    ListRecognition
    ''' <summary>
    ''' Lista las disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    ListAvailability
    ''' <summary>
    ''' Lista los proveedores por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListSupplierByStatus
    ''' <summary>
    ''' data source para listar todos los terceros
    ''' </summary>
    ListThirdPartyReportPortfolio
    ''' <summary>
    ''' data source para listar todos los terceros
    ''' </summary>
    ListAccountBankReportPortfolio
    ''' <summary>
    ''' data source para listar todos los traslados
    ''' </summary>
    ListPortfolioTransferReport
    ''' <summary>
    ''' data source para listar todos los traslados por filtro
    ''' </summary>
    ListPortfolioTransferReportFilter
    ''' <summary>
    ''' data source para listar todas las notas
    ''' </summary>
    ListNoteReportPortfolio
    ''' <summary>
    ''' data source para listar todas las notas por filtro
    ''' </summary>
    ListNoteReportPortfolioByFilter
    ''' <summary>
    ''' data source para listar todos los productos 
    ''' </summary>
    ListProductsReport
    ''' <summary>
    ''' data source para listar todos los Almacenes 
    ''' </summary>
    ListWarehouseReport
    ''' <summary>
    ''' data source para listar todos los Almacenes XP 
    ''' </summary>
    ListWarehouseReportXP
    ''' <summary>
    ''' data source para listar todos los productos 
    ''' </summary>
    ListBatchSerialReport
    ''' <summary>
    ''' data source para listar todos los grupos 
    ''' </summary>
    ListGroupReport
    ''' <summary>
    ''' data source para listar todos los subgrupos 
    ''' </summary>
    ListSubGroupReport
    ''' <summary>
    ''' data source para listar todos los subgrupos 
    ''' </summary>
    ListProductRateByIdName
    ''' <summary>
    ''' Lista todas las tarifas de los productos mostrando Id y Name
    ''' </summary>
    ListPharmaceuticalDispensing
    ''' <summary>
    ''' data source para listar todos los proveedores
    ''' </summary>
    ListSupplierInventoryReport
    ''' <summary>
    ''' data source para listar todos los clientes 
    ''' </summary>
    ListCustomerInventoryReport
    ''' <summary>
    ''' data source para listar todas las ordenes de servicio
    ''' </summary>
    ListPurchaseOrderReport
    ''' <summary>
    ''' data source para listar todas las ordenes de servicio con un filtro
    ''' </summary>
    ListPurchaseOrderReportByFilter
    ''' <summary>
    ''' data source para listar todas las remisiones de entrada
    ''' </summary>
    ListRemissionEntranceReport
    ''' <summary>
    ''' data source para listar todas las remisiones de entrada por filtro
    ''' </summary>
    ListRemissionEntranceReportFilter
    ''' <summary>
    ''' data source para listar todos comprobantes de entrada inventario
    ''' </summary>
    ListEntranceVoucherReport
    ''' <summary>
    ''' data source para listar todos comprobantes de entrada inventario por filtro
    ''' </summary>
    ListEntranceVoucherReportByFilter
    ''' <summary>
    ''' data source para listar todas las devoluciones de remision por tipo 
    ''' </summary>
    ListRemissionDevolutionByTypeReport

    ''' <summary>
    ''' data source para listar todos los subdetalles de la remision de entrada por proveedor y linea de distribuccion
    ''' </summary>
    ListRemissionEntranceDetailBatchSerialBySupplierIdAndSupplierDistributionLineId

    ''' <summary>
    ''' data source para listar todos los detalles del contrato por proveedor y linea de distribuccion
    ''' </summary>
    ListInventoryContractDetailBySupplierIdAndSupplierDistributionLineId

    ''' <summary>
    ''' data source para listar todos los detalles de las ordenes de compra por proveedor y linea de distribuccion
    ''' </summary>
    ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId

    ''' <summary>
    ''' data source para listar todos los detalles de las solicitudes por tipo de orden, despachado a y almacen o unidad funcional dependiendo a cual se despacho
    ''' </summary>
    ListRequestDetailByFilterFunctionalUnitWarehouseAndOrderTypeAndDispatchTo

    ''' <summary>
    ''' DataSource de CostCenter para el frontal de CostCenter
    ''' </summary>
    ''' <remarks></remarks>
    CostCenter

    ''' <summary>
    ''' DataSource de AuthorizationHealthCenter    
    ''' </summary>
    ''' <remarks></remarks>
    AuthorizationHealthCenter

    ''' <summary>
    ''' DataSource de razones de retiro para el mismo frontal
    ''' </summary>
    RetirementReason

    ''' <summary>
    ''' DataSource de Corporaciones para el Frontal 
    ''' </summary>
    ''' <remarks></remarks>
    Corporation

    ''' <summary>
    ''' DataSource de Fondos para el Frontal
    ''' </summary>
    ''' <remarks></remarks>
    Funds

    ''' <summary>
    ''' DataSource de Fondos por tipo de fondo
    ''' </summary>
    ''' <remarks></remarks>
    FundsByTypeFunds

    ''' <summary>
    ''' DataSource de Grupos de Nómina para el Frontal
    ''' </summary>
    ''' <remarks></remarks>
    GroupsPayroll

    ''' <summary>
    ''' Datasource de sindicatos de nomina para el frontal
    ''' </summary>
    ''' <remarks></remarks>
    ListTradeUnion

    ''' <summary>
    ''' Datasource de sindicatos de nomina por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListTradeUnionByStatus

    ''' <summary>
    ''' DataSource de BusinessUnit para el frontal de Unidad de negocio
    ''' </summary>
    ''' <remarks></remarks>
    BusinessUnit

    ''' <summary>
    ''' DataSource de FunctionalUnit para el mismo frontal de FunctionalUnit
    ''' </summary>
    ''' <remarks></remarks>
    FunctionalUnit

    ''' <summary>
    ''' DataSource de idiomas para el frontal de idiomas
    ''' </summary>
    ''' <remarks></remarks>
    Language

    ''' <summary>
    ''' DataSource de Company para Payroll
    ''' </summary>
    ''' <remarks></remarks>
    CompanyPayroll

    ''' <summary>
    ''' DataSource de parentescos para el modulo de parentesco
    ''' </summary>
    ''' <remarks></remarks>
    Kinship
    ''' <summary>
    ''' DataSource de parentescos para el modulo de tipo de telefono
    ''' </summary>
    ''' <remarks></remarks>
    PhoneType

    ''' <summary>
    ''' DataSource de Registro de Objecion para el frontal de Registro de Objeción
    ''' </summary>
    ''' <remarks></remarks>
    RegisterObjection


    ''' <summary>
    ''' DataSource de Talento Humano para el Frontal de human Talent
    ''' </summary>
    ''' <remarks></remarks>
    HumanTalent
    ''' <summary>
    ''' Lista la distribucion secundaria
    ''' </summary>
    ListDistributionSecondary

    ''' <summary>
    ''' DataSource de Conciliacion Cabecera para el Frontal de Conciliacion
    ''' </summary>
    ''' <remarks></remarks>
    ConciliationC
    ''' <summary>
    ''' DataSource de Cartera Glosa para el Frontal de Conciliacion
    ''' </summary>
    ''' <remarks></remarks>
    PortFolioGlosa
    ''' <summary>
    ''' Lista los gastos generales de distribución directa
    ''' </summary>
    ListGeneralExpensesWithDirectDistribution
    ''' <summary>
    ''' Lista los gastos generales de distribución directa por año y mes
    ''' </summary>
    ListGeneralExpensesWithDirectDistributionByYearMonth
    ''' <summary>
    ''' DataSource de Devolución Cabecera para el Frontal de Devolución
    ''' </summary>
    ''' <remarks></remarks>
    DevolucionC
    ''' <summary>
    ''' Lista la distribución por mano de obra
    ''' </summary>
    ListDistributionManpower
    ''' <summary>
    ''' DataSource de sucursales para el frontal de sucursales
    ''' </summary>
    ''' <remarks></remarks>
    BranchOffice
    ''' <summary>
    ''' DataSource de bancospara el frontal de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Bank

    ''' <summary>
    ''' DataSource de bancospara el frontal de bancos por estado
    ''' </summary>
    ''' <remarks></remarks>
    BankByStatus

    ''' <summary>
    ''' DataSource de Libreta Militar para el Frontal de Libreta Militar
    ''' </summary>
    ''' <remarks></remarks>
    MilitaryCard

    ''' <summary>
    ''' DataSource de Tipos de Pensionados para el Frontal de Tipos de Pensionados
    ''' </summary>
    ''' <remarks></remarks>
    PensionaryType

    ''' <summary>
    ''' DataSource de Centros de Trabajo para el Frontal de Centros de Trabajo
    ''' </summary>
    ''' <remarks></remarks>
    WorkCenter
    ''' <summary>
    ''' DataSource de tipos de estudio  para el frontal de tipos de estudio
    ''' </summary>
    ''' <remarks></remarks>
    StudyType
    ''' <summary>
    ''' DataSource de terceros para el frontal de terceros
    ''' </summary>
    ''' <remarks></remarks>
    ThirdParty
    ''' <summary>
    ''' DataSource de ciudades para el frontal de Centros de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    AllCity
    ''' <summary>
    ''' DataSource de grupos de contratos para el frontal de grupos de contratos
    ''' </summary>
    ''' <remarks></remarks>
    ContractGroup

    ''' <summary>
    ''' DataSource de Riesgos Profesionales
    ''' </summary>
    ''' <remarks></remarks>
    ProfessionalRisk
    ''' <summary>
    ''' DataSource de discapacidades para el frontal de discapacidades
    ''' </summary>
    ''' <remarks></remarks>
    Disability
    ''' <summary>
    ''' Lista las cuentas del erp q hace interfaz
    ''' </summary>
    ListMainAccountSupply
    ''' <summary>
    ''' lista la distribucion de activos fijos por periodo
    ''' </summary>
    ListDistributionFixedAssetByYearMonth
    ListDepreciationByYearMonth
    ''' <summary>
    ''' The list main account erp
    ''' </summary>
    ListMainAccountErp
    ''' <summary>
    ''' DataSource de Centros de Estudio para el frontal de Centros de Estudio
    ''' </summary>
    ''' <remarks></remarks>
    CenterStudy
    ''' <summary>
    ''' DataSource de terceros por persona para el frontal de terceros
    ''' </summary>
    ''' <remarks></remarks>
    ThirdPartyByPersonId
    ''' <summary>
    ''' DataSource de sucursales por tercero para el frontal de sucursales
    ''' </summary>
    ''' <remarks></remarks>
    BranchOfficeByThirdPartyId
    ''' <summary>
    ''' Lista todas las consignaciones
    ''' </summary>
    ListConsignment
    ''' <summary>
    ''' Lista las cuentas contables del erp con que se interfaza por clase
    ''' </summary>
    ListMainAccountErpByClass
    ''' <summary>
    ''' Lista los centros de costo provenientes de dinamica
    ''' </summary>
    ListCostCenterDinamic
    ''' <summary>
    ''' Lista los datos de liquidacion que pertenezcan a un numero de ingreso
    ''' </summary>
    ListLiquidationDataByAdmissionNumber
    ''' <summary>
    ''' Lista las liquidaciones que pertenezcan a un periodo
    ''' </summary>
    ListLiquidationByYearMont
    ''' <summary>
    ''' Lista las liquidaciones de costos de un periodo
    ''' </summary>
    ListCostLiquidationByYearMont
    ''' <summary>
    ''' DataSource de Tipos de Contrato 
    ''' </summary>
    ''' <remarks></remarks>
    ContractType
    ''' <summary>
    ''' DataSource de tipo contribuyente para el formulario Tipo contribuyente
    ''' </summary>
    ''' <remarks></remarks>
    ContributorType

    ''' <summary>
    ''' DataSource de Plantillas de Contrato para el formulario Plantillas de Contrato
    ''' </summary>
    ''' <remarks></remarks>
    ContractTemplate
    ''' <summary>
    ''' Lista las cuentas contables por depreciacion
    ''' </summary>
    ListMainAccountDeprecation
    ''' <summary>
    ''' Lista la distribucion intermedia por año y mes
    ''' </summary>
    ListDistributionIntermediateByYearMonth
    ListCostDistributionIntermediateByYearMonth
    ''' <summary>
    ''' DataSource de Retenciones para el formulario de Retenciones
    ''' </summary>
    ''' <remarks></remarks>
    Retention
    ''' <summary>
    ''' DataSource de cargos para el formulario de cargos
    ''' </summary>
    ''' <remarks></remarks>
    Position

    ListPositionByStatus

    ''' <summary>
    ''' DataSource de Incapacidades para el formulario de Incapacidades
    ''' </summary>
    ''' <remarks></remarks>
    Inability
    ''' <summary>
    ''' DataSource de persona para el formulario de terceros
    ''' </summary>
    ''' <remarks></remarks>
    Person

    ''' <summary>
    ''' DataSource de Conceptos para el formulario de Conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Concept
    ''' <summary>
    ''' Lista todos los empleados con el contrato activo
    ''' </summary>
    ListEmployeeWithActiveContract
    ''' <summary>
    ''' DataSource de Tipos de Vinculación Laboral para el formulario de Tipos de Vinculación Laboral
    ''' </summary>
    ''' <remarks></remarks>
    JobBondingType
    ''' <summary>
    ''' Lista la estructura organizacional
    ''' </summary>
    ListOrganizationalStructure
    ''' <summary>
    ''' DataSource de Unidades de Tiempo para el formulario de Unidades de Tiempo
    ''' </summary>
    ''' <remarks></remarks>
    TimeUnit
    ''' <summary>
    ''' Lista todos los registros de distribucion de gastos directos
    ''' </summary>
    ListDistributionDirectCost
    ListCostDistributionDirectCost
    ''' <summary>
    ''' DataSource de grupos por compañia para el frontal de grupos
    ''' </summary>
    ''' <remarks></remarks>
    GroupByCompany
    ''' <summary>
    ''' DataSource de sucursales por empresa para el frontal de sucursales
    ''' </summary>
    ''' <remarks></remarks>
    BranchOfficeByCompany
    ''' <summary>
    ''' DataSource de unidad functional por sucursales para el frontal de unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    FunctionalUnitByBranchOffice
    ''' <summary>
    ''' Lista los gastos generales
    ''' </summary>
    ListGeneralExpenses
    ListCostGeneralExpenses
    ''' <summary>
    ''' DataSource de plantillas de turnos para el mismo frontal
    ''' </summary>
    ''' <remarks></remarks>
    ScheduleTemplate
    ''' <summary>
    ''' DataSource de Parametrización de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    PayrollParameter

    ''' <summary>
    ''' Lista los activos fijos del erp que se hace interfaz
    ''' </summary>
    ListFixedAssetInterfaceErp

    ''' <summary>
    ''' DataSource de Lista Consecutivo Frontal de Recepcion de objecions
    ''' </summary>
    ''' <remarks></remarks>
    ObjectionsReceptionD


    ''' <summary>
    ''' DataSource de Frontal de Plantillas de Justificación
    ''' </summary>
    ''' <remarks></remarks>
    JustificationTemplate
    ''' <summary>
    ''' DataSource de Frontal de aseguradoras
    ''' </summary>
    ''' <remarks></remarks>
    Insurance
    ''' <summary>
    ''' DataSource de Frontal de fabricantes
    ''' </summary>
    ''' <remarks></remarks>
    ListSupplierReport
    ''' <summary>
    ''' DataSource de Frontal de Notas de Pago
    ''' </summary>
    ''' <remarks></remarks>
    ListPaymentsNotesReport
    ''' <summary>
    ''' Lista las notas de pagos por filtro
    ''' </summary>
    ''' <remarks></remarks>
    ListPaymentsNotesReportFilter
    ''' <summary>
    ''' DataSource de Facturas
    ''' </summary>
    ''' <remarks></remarks>
    ListPaymentsAccountPayableReport
    ''' <summary>
    ''' DataSource de Facturas por filtro
    ''' </summary>
    ''' <remarks></remarks>
    ListPaymentsAccountPayableReportByFilter
    ''' <summary>
    ''' Lista los diagnosticos
    ''' </summary>
    ListDiagnostic
    ''' <summary>
    ''' DataSource de Frontal de fabricantes
    ''' </summary>
    ''' <remarks></remarks>
    Supplier
    ''' <summary>
    ''' Lista los tipos de proveedores
    ''' </summary>
    ''' <remarks></remarks>
    ListSupplierType
    ''' <summary>
    ''' Lista los tipo de proveedores por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListSupplierTypeByStatus
    ''' <summary>
    ''' Lista los tipos de proveedor por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListAllSupplierTypeByStatus
    ''' <summary>
    ''' Lista los tipos de proveedores
    ''' </summary>
    ''' <remarks></remarks>
    ListSupplierTypeData
    ''' <summary>
    ''' Lista los tipos de proveedores por estado para el treeList
    ''' </summary>
    ''' <remarks></remarks>
    ListSupplierTypeByStatusTreeList
    ''' <summary>
    ''' Lista los tipos de proveedores que tiene asociado el proveedor por estado para el treeList
    ''' </summary>
    ''' <remarks></remarks>
    ListSupplierTypeByStatusTreeList2
    ''' <summary>
    ''' DataSource de Frontal de Proveedores de Mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    SupplierMaintenance
    ''' <summary>
    ''' DataSource de Frontal de polizas
    ''' </summary>
    ''' <remarks></remarks>
    Poliza
    ''' <summary>
    ''' DataSource de Frontal de tipos de poliza
    ''' </summary>
    ''' <remarks></remarks>
    PolizaType
    ''' <summary>
    ''' DataSource de Frontal de tipos de equipos
    ''' </summary>
    ''' <remarks></remarks>
    EquipamentType
    ''' <summary>
    ''' DataSource de Frontal de tipos de inventario
    ''' </summary>
    ''' <remarks></remarks>
    InventoryType
    ''' <summary>
    ''' Lista las areas de servicio de dinamica
    ''' </summary>
    ListServiceArea
    ''' <summary>
    ''' Datasource de empleados para el frontal de incapacidades
    ''' </summary>
    ''' <remarks></remarks>
    Employee
    ''' <summary>
    ''' DataSource de incapacidades para el frontal de incapacidades
    ''' </summary>
    ''' <remarks></remarks>
    InabilityLiquidate
    ''' <summary>
    ''' DataSource de  accesorios
    ''' </summary>
    ''' <remarks></remarks>
    Accessories
    ''' <summary>
    ''' DataSource de  Consumibles
    ''' </summary>
    ''' <remarks></remarks>
    Consumables
    ''' <summary>
    ''' DataSource de torres
    ''' </summary>
    ''' <remarks></remarks>
    Tower
    ''' <summary>
    ''' DataSource de Pisos
    ''' </summary>
    ''' <remarks></remarks>
    Floor
    ''' <summary>
    ''' DataSource areas
    ''' </summary>
    ''' <remarks></remarks>
    Area
    ''' <summary>
    ''' DataSource habitaciones y oficinas
    ''' </summary>
    ''' <remarks></remarks>
    Room
    ''' <summary>
    ''' DataSource responsables de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    Responsibles
    ''' <summary>
    ''' Lista los numeros de cuenta
    ''' </summary>
    ListMainAccountNumber
    ''' <summary>
    ''' DataSource equipos de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    Equipment
    ''' <summary>
    ''' Lista los centros de produccion
    ''' </summary>
    ListProductionCenter
    ListCostProductionCenter
    ''' <summary>
    ''' Lista las Actividades
    ''' </summary>
    ListCostActivities
    ''' <summary>
    ''' Lista las Actividades
    ''' </summary>
    ListCostInventoryGroups
    ''' <summary>
    ''' DataSource partes de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    Part
    ''' <summary>
    ''' datasource para listar las plantillas de tipo de equipamiento
    ''' </summary>
    ''' <remarks></remarks>
    TemplateEquipmentType
    ''' <summary>
    ''' DataSource ingreso de equipos
    ''' </summary>
    ''' <remarks></remarks>
    EquipmentReception
    ''' <summary>
    ''' DataSource unidad de medida
    ''' </summary>
    ''' <remarks></remarks>
    UnitMeasure
    ''' <summary>
    ''' DataSource registro tecnico
    ''' </summary>
    ''' <remarks></remarks>
    TechnicalLog
    ''' <summary>
    ''' DataSource sucursales
    ''' </summary>
    ''' <remarks></remarks>
    Branch
    ''' <summary>
    ''' Lista todos los cambios de cheque
    ''' </summary>
    ListCheckCashing
    ''' <summary>
    ''' Listado de conceptos de tesoreria por comportamiento
    ''' </summary>
    ''' <remarks></remarks>
    ListExpenseConceptsByBehavior
    ''' <summary>
    ''' The list inventory product by no class type
    ''' </summary>
    ListInventoryProductByNoClassType
    ''' <summary>
    ''' Lista los centros de produccion por estado
    ''' </summary>
    ListProductionCenterByStatus
    ''' <summary>
    ''' Listado de conceptos de tesoreria por comportamiento y cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    ListExpenseConceptsByBehaviorAndIdMainAccount
    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ListAllBank
    ''' <summary>
    ''' Lista todos los productos
    ''' </summary>
    ListInventoryProduct
    ''' <summary>
    ''' Lista todos los rangos de temperatura
    ''' </summary>
    ListStorageTemperature
    ''' <summary>
    ''' Lista todos los anticipos de cartera pagados por comprobante de egreso
    ''' </summary>
    ListVoucherTransactionAdvance
    ''' <summary>
    ''' Lista todos los conceptos de egreso
    ''' </summary>
    ListAllVoucherTransaction
    ''' <summary>
    ''' Lista todos los conceptos de egreso by status
    ''' </summary>
    ListAllVoucherTransactionByStatus
    InitializeOrganizationalStructureWithOut
    ''' <summary>
    ''' Lista todas las cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountPayable
    ''' <summary>
    ''' Lista las facturas por el id del proveedor y el estado
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountPayableByIdSupplierAndState
    ''' <summary>
    ''' Lista los traslados de facturas
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountPayableTransfer
    ''' <summary>
    ''' Lista las cuotas con la cabecera de facturas segun corresponda
    ''' </summary>
    ''' <remarks></remarks>
    ListSharesWithAccountPayable
    ''' <summary>
    ''' Lista los anticipos del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    ListAdvancePayments
    ''' <summary>
    ''' Lista las notas debito/credito
    ''' </summary>
    ''' <remarks></remarks>
    ListPaymentNotes
    ''' <summary>
    ''' Lista todas las patologias POS
    ''' </summary>
    LostPOSPathologies
    ''' <summary>
    ''' Lista las lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    ListDistributionLine
    ''' <summary>
    ''' lista las cuentas por pagar por numero de factura
    ''' </summary>
    ListAccountPayableByBillNumber
    ''' <summary>
    ''' Lista de proveedores con sus lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    ListSuppliersDistributionLines
    ''' <summary>
    ''' Obtiene la linea de distribucion proveedor por id
    ''' </summary>
    ''' <remarks></remarks>
    ListSuppliersDistributionLinesById
    ''' <summary>
    ''' lista todas las cxc por tercero y estado
    ''' </summary>
    ListAccountRecivableAccountByThirdIdState
    ''' <summary>
    ''' Lista los saldos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    ListInitialBalance
    ''' <summary>
    ''' The list organizational structure data
    ''' </summary>
    ListOrganizationalStructureData
    ''' <summary>
    ''' Lista los traslados
    ''' </summary>
    ''' <remarks></remarks>
    ListPaymentTransfer
    ''' <summary>
    ''' Lista las unidades de medida por tipo
    ''' </summary>
    ListMeasureUnitByType
    ''' <summary>
    ''' Lista las cuentas contables para mano de obra
    ''' </summary>
    ListMainAccountLabor
    ''' <summary>
    ''' The list atc
    ''' </summary>
    ListATC
    ''' <summary>
    ''' The list general ledger iva
    ''' </summary>
    ListGeneralLedgerIva
    ''' <summary>
    ''' The list packaging unit
    ''' </summary>
    ListPackagingUnit
    ''' <summary>
    ''' lista las cuentas contables para el formulario de busqueda
    ''' </summary>
    ListAccountsForSearch
    ''' <summary>
    ''' obtiene una cuota de factura por id
    ''' </summary>
    GetAccountPayableXpoById
    ''' <summary>
    ''' DataSource para tipo de empleado
    ''' </summary>
    ''' <remarks></remarks>
    EmployeeType
    ''' <summary>
    ''' DataSource para razones de otro si
    ''' </summary>
    ''' <remarks></remarks>
    ContractModificationReason
    ''' <summary>
    ''' DataSource para listar los grupos filtrados por clase de contrato
    ''' </summary>
    ''' <remarks></remarks>
    GroupByContractClass
    ''' <summary>
    ''' DataSource para listar las unidades funcionales por empresa :P
    ''' </summary>
    ''' <remarks></remarks>
    FunctionalUnitByCompany
    ''' <summary>
    ''' Datasource para llenar los tipos de contrato dependiendo del tipo de contrato
    ''' </summary>
    ''' <remarks></remarks>
    ContractTypeByContractClass
    ''' <summary>
    ''' Data source para listar todos loe empleados sin tener en cuenta el contrato
    ''' </summary>
    ''' <remarks></remarks>
    AllEmployees
    ''' <summary>
    ''' Lista todos los cruces de cuentas
    ''' </summary>
    ListCrossingAccount
    ''' <summary>
    ''' Lista las notas de tesoreria
    ''' </summary>
    ListTreasuryNote
    ''' <summary>
    ''' obtiene los anticipos de cartera por id del tercero
    ''' </summary>
    GetAllPortfolioAdvanceByThirdId

    ''' <summary>
    ''' Data source para listar todos los empleados que tenga contrato para ser liquidado
    ''' </summary>
    AllEmployeesWithContractToLiquidate
    ''' <summary>
    ''' Lista todos los detalles de egresos por id del comprobante de egreso detalle
    ''' </summary>
    ListDischargeBillByIdVoucherTransactionDXpo

    ''' <summary>
    ''' Data source para listar todos los traslados cobro jurídico
    ''' </summary>
    ''' <remarks></remarks>
    TransferJuridicalDebt
    ''' <summary>
    ''' Lista los productos por clase del tipo de producto
    ''' </summary>
    ListInventoryProductByProductType

    ''' <summary>
    ''' DataSource para listar las unidades funcionales por empresa
    ''' </summary>
    ''' <remarks></remarks>
    ResponsibleMaintenance

    ''' <summary>
    ''' DataSource para listar las clases de convenios
    ''' </summary>
    ''' <remarks></remarks>
    ListKindsAgreements
    ''' <summary>
    ''' Datasource para listar terceros mediante un Linq
    ''' </summary>
    ''' <remarks></remarks>
    ListThird
    ''' <summary>
    ''' DataSource para listar convenios
    ''' </summary>
    ''' <remarks></remarks>
    ListAgreements
    ''' <summary>
    ''' Datasource para Listar Embargos
    ''' </summary>
    ListForeclousure
    ''' <summary>
    ''' DataSource para listar parametros de interface
    ''' </summary>
    ''' <remarks></remarks>
    ListparametersInterface

    ''' <summary>
    ''' Lista todos los tipos de documentos
    ''' </summary>
    ListDocumentTypes
    ''' <summary>
    ''' The list product template
    ''' </summary>
    ListProductTemplate
    ''' <summary>
    ''' Lista todos los grupos de producto
    ''' </summary>
    ''' <remarks></remarks>
    ListProductGroup
    ''' <summary>
    ''' Lista todos los subgrupos de producto
    ''' </summary>
    ''' <remarks></remarks>
    ListProductSubGroup
    ''' <summary>
    ''' Lista todas las unidades de medida
    ''' </summary>
    ''' <remarks></remarks>
    ListMeasureUnit
    ''' <summary>
    ''' Lista todos los almacenes
    ''' </summary>
    ''' <remarks></remarks>
    ListWarehouse
    ''' <summary>
    ''' Lista todas las ordenes de traslado
    ''' </summary>
    ''' <remarks></remarks>
    ListTransferOrder
    ''' <summary>
    ''' Lista todas las ordenes de traslado por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListTransferOrderByStatus
    ''' <summary>
    ''' Lista todas las devoluciones de ordenes de traslado
    ''' </summary>
    ''' <remarks></remarks>
    ListTransferOrderDevolution
    ''' <summary>
    ''' Lista los inventarios fisicos filtrados por id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    ListPhysicalInventoryByWarehouseIdFrmStock
    ''' <summary>
    ''' Lista todos los productos
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryProductFrmStock
    ''' <summary>
    ''' Lista todos las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryRequest
    ''' <summary>
    ''' Lista todos los grupos farmacologicos
    ''' </summary>
    ''' <remarks></remarks>
    ListPharmacologicalGroup
    ''' <summary>
    ''' Lista todos los conceptos de ajuste
    ''' </summary>
    ''' <remarks></remarks>
    ListAdjustmentConcept
    ''' <summary>
    ''' Listado todos los fabricantes
    ''' </summary>
    ''' <remarks></remarks>
    ListManufacturers
    ''' <summary>
    ''' Lista otras deducciones o retenciones
    ''' </summary>
    ''' <remarks></remarks>
    ListOtherWitholdingDeductions
    ''' <summary>
    ''' Lista todos los tipos de producto
    ''' </summary>
    ''' <remarks></remarks>
    ListProductType
    ''' <summary>
    ''' Lista todos los tipos de producto por el estado
    ''' </summary>
    ''' <remarks></remarks>
    ListProductTypeByStatus
    ''' <summary>
    ''' Lista todos los conceptos de ajuste de inventario por tipo
    ''' </summary>
    ''' <remarks></remarks>
    ListAdjustmentConceptByConceptType
    ''' <summary>
    ''' Lista los atributos del tipo de producto
    ''' </summary>
    ''' <remarks></remarks>
    ListAttributeProductType
    ''' <summary>
    ''' Lista todos los DCI
    ''' </summary>
    ''' <remarks></remarks>
    ListDCI
    ''' <summary>
    ''' Lista los DCI por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListDCIByStatus
    ''' <summary>
    ''' Lista todas las formas farmceuticas
    ''' </summary>
    ''' <remarks></remarks>
    ListPharmaceuticalForm
    ''' <summary>
    ''' Lista los comprobantes de egreso que se hayan pagado con cheque
    ''' </summary>
    ListAllVoucherTransactionWithVoucherTypeCheck
    ''' <summary>
    ''' Lista las formas farmaceuticas por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListPharmaceuticalFormByStatus
    ''' <summary>
    ''' Lista todas las vias de administracion
    ''' </summary>
    ''' <remarks></remarks>
    ListAdministrationRoute
    ''' <summary>
    ''' Lista todas las ATC
    ''' </summary>
    ListATCEntity
    ''' <summary>
    ''' Lista todos los tipos de documentos
    ''' </summary>
    ListAccountClass

    ''' <summary>
    ''' Lista todos los libros
    ''' </summary>
    ListAllBook

    ''' <summary>
    ''' Lista todos los libros en los cuales se puede realizar movimientos
    ''' </summary>
    ListBook

    ''' <summary>
    ''' Lista todos los libros por estado
    ''' </summary>
    ListAllBookByStatus

    ''' <summary>
    ''' Lista todos libros en los cuales se puede realizar movimientos por estado
    ''' </summary>
    ListBookByStatus

    ''' <summary>
    ''' Lista todos los libros por estado
    ''' </summary>
    ListAllBookByStatusXpCollection

    ''' <summary>
    ''' Lista todos libros en los cuales se puede realizar movimientos por estado
    ''' </summary>
    ListBookByStatusXpCollection

    ''' <summary>
    ''' lista las cuentas contables por estado y por id del libro
    ''' </summary>
    ListMainAccountsByStatusAndBookId

    ''' <summary>
    ''' lista las cuentas contables por estado y por id del libro de Activos fijos
    ''' </summary>
    ListMainAccountsFixedAssetByStatusAndBookId

    ''' <summary>
    ''' Lista todos los tipos de documentos
    ''' </summary>
    ListAccountLevel
    ''' <summary>
    ''' Lista todos las entidades 
    ''' </summary>
    ListCompanyType

    ''' <summary>
    ''' Lista todos los grupos de cups
    ''' </summary>
    ''' <remarks></remarks>
    ListCupsGroup
    ''' <summary>
    ''' Lista los grupos de cups por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListCupsGroupByStatus
    ''' <summary>
    ''' Lista todas las entidades de contratos
    ''' </summary>
    ''' <remarks></remarks>
    ListContractEntity
    ''' <summary>
    ''' Lista las entidades de contratos por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListContractEntityByStatus
    ListProductTemplateInventory
    ''' <summary>
    ''' Lista las plantillas de productos por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListProductTemplateByStatus
    ''' <summary>
    ''' Lista todos los rangos uvr
    ''' </summary>
    ''' <remarks></remarks>
    ListUVRRange
    ''' <summary>
    ''' Lista los rangos uvr por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListUVRRangeByStatus
    ''' <summary>
    ''' Lista todos los subgrupos de cups
    ''' </summary>
    ''' <remarks></remarks>
    ListCupsSubGroup
    ''' <summary>
    ''' Lista los subgrupos cups por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListCupsSubGroupByStatus
    ''' <summary>
    ''' Lista todas las entidades CUPS
    ''' </summary>
    ''' <remarks></remarks>
    ListCupsEntity
    ''' <summary>
    ''' Lista las entidades cups por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListCupsEntityByStatus
    ''' <summary>
    ''' Lista todos los contratos
    ''' </summary>
    ''' <remarks></remarks>
    ListContract
    ''' <summary>
    ''' Lista los contratos por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListContractByStatus
    ''' <summary>
    ''' Lista las unidades de mercadeo
    ''' </summary>
    ''' <remarks></remarks>
    ListMarketingUnit
    ''' <summary>
    ''' Lista las descripciones del módulo de contratos
    ''' </summary>
    ListContractDescriptions
    ''' <summary>
    ''' Lista los tipos de decuento del módulo de contratos
    ''' </summary>
    ListDiscountTypes
    ''' <summary>
    ''' Lista las entidades administradoras de salud
    ''' </summary>
    ''' <remarks></remarks>
    ListHealthAdministrator
    ''' <summary>
    ''' Lista las entidades administradoras de salud por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListHealthAdministratorByStatus
    ''' <summary>
    ''' Lista todos los contratos profesionales de salud
    ''' </summary>
    ''' <remarks></remarks>
    ListMedicalFeesContract
    ''' <summary>
    ''' Consulta un contrato por id
    ''' </summary>
    ''' <remarks></remarks>
    GetMedicalFeesContractById
    ''' <summary>
    ''' Lista todos los contratos profesionales de salud por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListMedicalFeesContractByStatus
    ''' <summary>
    ''' Lista todas las causaciones por contrato profesional de la salud
    ''' </summary>
    ''' <remarks></remarks>
    ListMedicalFeesCausationByMedicalFeesContractId
    ''' <summary>
    ''' Lista todas las causaciones por contrato profesional de la salud
    ''' </summary>
    ''' <remarks></remarks>
    ListCausationByMedicalFeesContractId
    ''' <summary>
    ''' Lista todas las causaciones por contrato profesional de la salud para listar las deducciones
    ''' </summary>
    ''' <remarks></remarks>
    ListCausationByMedicalFeesContractIdForDeductions
    ''' <summary>
    ''' Lista todas las causaciones por contrato profesional de la salud para listar las glosas
    ''' </summary>
    ''' <remarks></remarks>
    ListCausationByMedicalFeesContractIdForGlosas
    ''' <summary>
    ''' Lista todas las liquidaciones de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    ListMedicalFeesLiquidation
    ''' <summary>
    ''' Lista los ipsService por presentacion y clase
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServicesByPresentationAndServiceClass
    ''' <summary>
    ''' Lista los ipService por manual servicio y clase
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServicesByServiceManualAndServiceClass
    ''' <summary>
    ''' Lista los servicios ips por clase, tipo manual y presentacion
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServiceByServiceClassAndServiceManualAndPresentation
    ''' <summary>
    ''' Lista todos los manuales tarifarios
    ''' </summary>
    ''' <remarks></remarks>
    ListRateManual
    ''' <summary>
    ''' Lista los conceptos de nota por tipo de nota
    ''' </summary>
    ''' <remarks></remarks>
    ListPortfolioNoteConceptByNoteType
    ''' <summary>
    ''' Lista los manuales tarifarios por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListRateManualByStatus
    ''' <summary>
    ''' Consulta manual tarifario por id
    ''' </summary>
    ''' <remarks></remarks>
    RateManualById
    ''' <summary>
    ''' Lista todos los manuales de servicio
    ''' </summary>
    ''' <remarks></remarks>
    ListRateManualDetail
    ''' <summary>
    ''' Lista los manuales de servicio por id de manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    ListRateManualDetailByRateManualId
    ''' <summary>
    ''' Lista los manuales de servicio por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListRateManualDetailByStatus
    ''' <summary>
    ''' Lista la asociacion entre procedureCups y marketingUnitCUps
    ''' </summary>
    ''' <remarks></remarks>
    ListVProcedureMarketingUnitCUPS
    ''' <summary>
    ''' Lista las facturas por numero de factura
    ''' </summary>
    ''' <remarks></remarks>
    ListViewNoSurgicalByInvoiceId
    ''' <summary>
    ''' Lista la vista de los detalles de las ordenes de servicio
    ''' </summary>
    ''' <remarks></remarks>
    ListViewSurgicalAndPackageByInvoiceId
    ''' <summary>
    ''' Lista todos los grupos quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    ListSurgicalGroup
    ''' <summary>
    ''' Lista los grupos quirurgicos por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListSurgicalGroupByStatus

    ''' <summary>
    ''' Lista todos los tipos de documentos
    ''' </summary>
    ListRetentionConcept
    ''' <summary>
    ''' lista los conceptos de retencion por tipo de retention
    ''' </summary>
    ''' <remarks></remarks>
    ListRetentionConceptByTypeRetention
    ''' <summary>
    ''' DataSource para listar los contenedores de archivo
    ''' </summary>
    FileContainerDocumentalSystem

    ''' <summary>
    ''' Datasource para listar los conceptos manuales
    ''' </summary>
    ''' <remarks></remarks>
    ListManualConcepts
    ''' <summary>
    ''' Lista las programaciones de pagos
    ''' </summary>
    ListSchedulePayment
    ''' <summary>
    ''' Datasource para listar los indicadores economicos
    ''' </summary>
    EconomicIndicator

    ''' <summary>
    ''' Datasource para listar los conceptos de cuentas por cobrar
    ''' </summary>
    PortfolioConcept

    ''' <summary>
    ''' Lista todas las tarjetas
    ''' </summary>
    ListCard
    ''' <summary>
    ''' Lista todos los grupos de atencion
    ''' </summary>
    ''' <remarks></remarks>
    ListCareGroup
    ''' <summary>
    ''' Lista los medicos que tengan asociados el contrato que pasa como parametro
    ''' </summary>
    ''' <remarks></remarks>
    ListHealthProfessionalCodeByMedicalFeesContractId
    ''' <summary>
    ''' lista todos los reembolsos
    ''' </summary>
    ListRefund
    ''' <summary>
    ''' Lista todos los conceptos de recibo de caja
    ''' </summary>
    ListCashReceiptConcept
    ''' <summary>
    ''' lista los conceptos de recibo de caja que la cuenta contable no maneja centro de costo
    ''' </summary>
    ''' <remarks></remarks>
    ListCashReceiptConceptWithOutCostCenter

    ''' <summary>
    ''' Datasource para listar las fuentes de financiacion
    ''' </summary>
    ''' <remarks></remarks>
    ListFinancialSource
    ''' <summary>
    ''' Datasource para listar los conceptos de notas
    ''' </summary>
    PortfolioNoteConcept
    ''' <summary>
    ''' Datasource para listar las dependencias
    ''' </summary>
    ''' <remarks></remarks>
    ListDependencies

    ''' <summary>
    ''' Lista las entidades presupuestales server collection source
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetEntitiesXPSCS

    GetSchedulePayment



    ''' <summary>
    ''' Lista las entidades presupuestales instant feed back s
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetEntitiesXPIFS

    ''' <summary>
    ''' Datasource para listar los conceptos de pago
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsAccountsPayable
    ''' <summary>
    ''' Lista los conceptos de pagos por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsAccountsPayableByStatus
    ''' <summary>
    ''' Lista los conceptos cxp por estado y si maneja retencion
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsAccountsPayableByStatusAndHandlesRetention
    ''' <summary>
    ''' Lista los conceptos cxp por estado y si maneja retencion pero se saca de common xpo
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsAccountsPayableByStatusAndHandlesRetentionOfCommon
    ''' <summary>
    ''' Lista los motivos de rechazo de factura
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountPayableRejectionReason
    ''' <summary>
    ''' Lista todas las unidades de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    ListFilingUnit
    ''' <summary>
    ''' Lista todas las Ubicaciones
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetLocationData
    ''' <summary>
    ''' Lista todas los tipos de ubicación
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetLocationType
    ''' <summary>
    ''' Lista todas los tipos de ubicación mayores a los del padre(ubicación)
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetLocationTypeParent
    ''' <summary>
    ''' Lista todas los centros de costo
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetCostCenter
    ''' <summary>
    ''' Lista todas las cuentas de pago
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetMainAccount
    ''' <summary>
    ''' Lista las unidades de radicacion por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListFilingUnitByStatus
    ''' <summary>
    ''' Lista las unidades de radicacion para el treelist
    ''' </summary>
    ''' <remarks></remarks>
    ListFilingUnitByStatusCollection
    ''' <summary>
    ''' Lista las unidades de radicacion para el treeList del Formulario
    ''' </summary>
    ''' <remarks></remarks>
    ListFilingUnitData
    ''' <summary>
    ''' Lista las unidades de radicacion para el treeList del Formulario
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetLocation
    ''' <summary>
    ''' Lista la propiedad física para el treeList del Formulario
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetPhysicalAsset
    ''' <summary>
    ''' Lista la propiedad física para el treeList del Formulario
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetVPhysicalAssetMainAccount
    ''' <summary>
    ''' Lista el estado de los activos para el treeList del Formulario
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetStatusAsset
    ''' <summary>
    ''' Lista las ubicaciones de Activos Fijos
    ''' </summary>
    ''' <remarks></remarks>
    ListFunctionalUnit
    ''' <summary>
    ''' Lista las ubicaciones de Activos Fijos
    ''' </summary>
    ''' <remarks></remarks>
    ListFunctionalUnitByUser
    ''' <summary>
    ''' Lista los conceptos de nomina
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsPayroll
    ''' <summary>
    ''' Lista los conceptos de cxp por la bandera de si maneja retencion
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountPayableConceptByHandlesRetention
    ''' <summary>
    ''' Lista los conceptos de cxp pr la bandera de si maneja retencion y tipos de concepto
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountPayableConceptByHandlesRetentionAndConceptType
    ''' <summary>
    ''' Lista los conceptos de cxp por el tipo de concepto
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountPayableConceptByConceptType
    ''' <summary>
    ''' Datasource para listar los conceptos de notas
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsNotes

    ''' <summary>
    ''' Listado de abogados
    ''' </summary>
    ListLawyer

    ''' <summary>
    ''' Lista los conceptos de notas por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsNotesByStatus
    ''' <summary>
    ''' Lista los conceptos de notas por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsNotesByConcepType


    ''' <summary>
    ''' The list cash register
    ''' </summary>
    ListCashRegister
    ''' <summary>
    ''' Lista todos los conceptos de egresos
    ''' </summary>
    ListExpenseConcept

    ''' <summary>
    ''' The address
    ''' </summary>
    Address

    ''' <summary>
    ''' The area fixed assets
    ''' </summary>
    AreaFixedAssets

    ''' <summary>
    ''' The list note concept
    ''' </summary>
    ListNoteConcept


    ''' <summary>
    ''' The list note concept
    ''' </summary>
    ListAllNoteConcept

    ''' <summary>
    ''' The deduction
    ''' </summary>
    Deduction

    ListThridPartyBankAccount

    ''' <summary>
    ''' The iva
    ''' </summary>
    Iva
    ''' <summary>
    ''' Lista los conceptos de traslados
    ''' </summary>
    ''' <remarks></remarks>
    ListConceptsShuttle

    ''' <summary>
    ''' Listado de las vigencias
    ''' </summary>
    ''' <remarks></remarks>
    ListValidityByEntity
    ''' <summary>
    ''' Datasource de ajuste de inflacion
    ''' </summary>
    ListInflationAdjustment

    ''' <summary>
    ''' The group fixed asset
    ''' </summary>
    GroupFixedAsset

    ''' <summary>
    ''' Lista los tipos de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    ListEarningsType

    ''' <summary>
    ''' The product fixed asset
    ''' </summary>
    ProductFixedAsset

    ''' <summary>
    ''' The list provision ranges
    ''' </summary>
    ListProvisionRanges
    ''' <summary>
    ''' datasoruce para listar todos los Anexos de entarda
    ''' </summary>
    StatementFolio
    ''' <summary>
    ''' Lista todas las cuentas de entidades
    ''' </summary>
    ListEntityBankAccount
    ''' <summary>
    ''' Lista todas las cuentas de entidades
    ''' </summary>
    ListEntityBankAccountByBank
    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ListPatrimonialPart

    ''' <summary>
    ''' Lista todos los tipos de gasto
    ''' </summary>
    ''' <remarks></remarks>
    ListExpenseType

    ''' <summary>
    ''' Lista todas las dependencias de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetDependency

    ''' <summary>
    ''' lista todos los conceptos de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetConcept
    ''' <summary>
    ''' Lista todos las rutas
    ''' </summary>
    ''' <remarks></remarks>
    ListRoute
    ''' <summary>
    ''' lista todos grupos de activos fijos por estado
    ''' </summary>
    ''' <remarks></remarks>
    GetAllGroupByStateFixedAsset
    ''' <summary>
    ''' Datasource facturas radicadas 
    ''' </summary>
    ''' <remarks></remarks>
    ListInvoiceRadicate
    ''' <summary>
    ''' Datasource facturas radicadas confirmadas
    ''' </summary>
    ''' <remarks></remarks>
    ListInvoiceRadicateConfirm

    ''' <summary>
    ''' Datasource linqinstan de facturas pagos parciales
    ''' </summary>
    ''' <remarks></remarks>
    ListInvoicePartialPayments

    ''' <summary>
    ''' Datasource facturas por evaluar de responsable 
    ''' </summary>
    ''' <remarks></remarks>
    ListEvaluationInvoiceByResponsible
    ''' <summary>
    ''' Datasource de los rubros presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetItems

    ''' <summary>
    ''' Listar las modificaciones de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetModifications

    ''' <summary>
    ''' Listar los traslados de pac por vigencia
    ''' </summary>
    ''' <remarks></remarks>
    ListAnnualizedCashFlowTransferByValidityId

    ''' <summary>
    ''' Consulta el presupuesto inicial con el valor Programado
    ''' </summary>
    ''' <remarks></remarks>
    ListViewListAnnualizedCashFlow

    ''' <summary>
    ''' data source para listar todas las cuentas
    ''' </summary>
    ListAccounts

    ''' <summary>
    ''' data source para listar todo el plan de cuentas 
    ''' </summary>
    ListAccountsReport

    ''' <summary>
    ''' Lista todos los conceptos de pago
    ''' </summary>
    ListAllPaymentConcept
    ''' <summary>
    ''' The list accounts by level
    ''' </summary>
    ListAccountsByLevel
    ''' <summary>
    ''' lista las cuentas contables que sea de tipo retencion en la fuente y que sea de categoria trabajadores independientes
    ''' </summary>
    ListAccountsByRetentionAndFreelancerCategory
    ''' <summary>
    ''' The list accounts by retention
    ''' </summary>
    ListAccountsByRetention
    ''' <summary>
    ''' lista los  conceptos de retenmcion segun el estadp
    ''' </summary>
    ListRetentionConceptByStatus
    ''' <summary>
    ''' Lista todas los cheques cancelados
    ''' </summary>
    ListAllCancellationCheck
    ''' <summary>
    ''' Lista todas las ciudades
    ''' </summary>
    ''' <remarks></remarks>
    ListAllCities
    ''' <summary>
    ''' Lista todas las cajas por tipo
    ''' </summary>
    ListCashRegisterByType
    ''' <summary>
    ''' Lista todos los conceptos por id de caja
    ''' </summary>
    ListExpenseConceptCashRegisterByCash
    ''' <summary>
    ''' Lista todas las cajas distintas a la elegida
    ''' </summary>
    ListExpenseConceptCashRegisterByNotCash

    ''' <summary>
    ''' The list journal voucher by state
    ''' </summary>
    ListJournalVoucherByState

    ''' <summary>
    ''' The get cost center by state
    ''' </summary>
    GetCostCenterByState
    ''' <summary>
    ''' Lista los conceptos que tenga autorizada una caja
    ''' </summary>
    ListExpenseConceptByCash
    ''' <summary>
    ''' Lista todos los conceptos de egreso que no tengan comportamiento de caja
    ''' </summary>
    ListExpenseConceptByNotCash
    ''' <summary>
    ''' lista los conceptos de egreso que sean de tipo endoso de facturas
    ''' </summary>
    ListExpenseConceptEndorsement
    ''' <summary>
    ''' lista las cajas por usuario
    ''' </summary>
    ''' <remarks></remarks>
    ListCashRegisterByUser
    ''' <summary>
    ''' Lista todos los conceptos de pago por estado
    ''' </summary>
    ListAllPaymentConceptByState

    ''' <summary>
    ''' funcion para obtener las clasificaciones por estado
    ''' </summary>
    ''' <remarks></remarks>
    GetAllClassificationByState
    ''' <summary>
    ''' funcion para obtener las clasificacioanes
    ''' </summary>
    ''' <remarks></remarks>
    GetAllClassification

    ''' <summary>
    ''' consulta todos los documentos contable
    ''' </summary>
    ''' <remarks></remarks>
    GetAllJournalVourchers

    ''' <summary>
    ''' consulta todos los meses 
    ''' </summary>
    ''' <remarks></remarks>
    ListAllMonth
    ''' <summary>
    ''' consulta los recibos de caja
    ''' </summary>
    ''' <remarks></remarks>
    ListCashReceipts

    ''' <summary>
    ''' Listar los empleados por unidad funcional
    ''' </summary>
    ''' <remarks></remarks>
    ListEmployeeByFunctionalUnitId

    ''' <summary>
    ''' Listar los empleados por grupo
    ''' </summary>
    ''' <remarks></remarks>
    ListEmployeeByGroupId
    ''' <summary>
    ''' Lista las cuentas bancarias autorizadas por codigo de usuario
    ''' </summary>
    ListEntityBankAccountByUser
    ''' <summary>
    ''' Lista todas las cuotas
    ''' </summary>
    ListAccountPayableShares
    ''' <summary>
    ''' Lista todas las cuotas de facturas por tercero, cuenta contable y estado
    ''' </summary>
    ListAccountPayableSharesByIdThirdIdAccountAndState
    ''' <summary>
    ''' Lista todas las cuotas de facturas por proveedor y estado
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountPayableSharesByIdSupplierAndState
    ''' <summary>
    ''' Lista todas las cuotas de facturas por proveedor y estado
    ''' </summary>
    ListAccountPayableSharesBySupplierIdAndState
    ''' <summary>
    ''' Lista las ubicaciones de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListLocationMaintenance

    ''' <summary>
    ''' Lista los Planes de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListMaintenancePlan

    ''' <summary>
    ''' Lista los tipos de equipo de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListEquipmentTypeMaintenance
    ''' <summary>
    ''' lista los conceptos de recibo de caja por tipo de retention
    ''' </summary>
    ''' <remarks></remarks>
    ListCashReceiptConceptsByRetentionType
    ''' <summary>
    ''' lista las cuentas contables por centro de costo o por retecion
    ''' </summary>
    ''' <remarks></remarks>
    ListMainAccountsHandlesCostCenterAndRetention

    ''' <summary>
    ''' Estructura Contable de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    AccountingStructure
    ''' <summary>
    ''' lista las facturas que se usan en recibos de caja
    ''' </summary>
    ''' <remarks></remarks>
    ListPortfolioAccountReceivableByCashReceipt
    ''' <summary>
    ''' lista las cuotas de la cuenta por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    ListPortfolioAccountReceivableValidation
    ''' <summary>
    ''' lista los anticipos para el recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    ListAdvancePaymentsCashReceipt
    ''' <summary>
    ''' lista los clientes por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListCustomerByStatus
    ''' <summary>
    ''' dataSource para las facuras que se usan en las notas de cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    ListBillsPortfolioNote
    ''' <summary>
    ''' dataSource para los conceptos de cuentas x cobrar por estado
    ''' </summary>
    ''' <remarks></remarks>
    GetAllAccountReceivableConceptByStatus
    ''' <summary>
    ''' dataSource para los documentos de cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    GetAllAccountReceivableDocument
    ''' <summary>
    ''' dataSource para las facuras que se usan en las notas de cuentas por cobrar filtrado por tercero
    ''' </summary>
    ''' <remarks></remarks>
    ListBillsPortfolioNoteByThridParty
    ''' <summary>
    ''' dataSource para los anticipos que se usan en las notas de cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    ListPortfolioAdvancePortfolioNote
    ''' <summary>
    ''' datasource para obtener los conceptos de nota por estado
    ''' </summary>
    ''' <remarks></remarks>
    GetAllPortfolioNoteConceptByStatus
    ''' <summary>
    ''' datasource para obtener las notas de cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    ListPortfolioNote


    ''' <summary>
    ''' Lista el grupo de facturacion por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListBillingGroupByStatus
    ''' <summary>
    ''' Lista el grupo de facturacion 
    ''' </summary>
    ''' <remarks></remarks>
    ListBillingGroup

    ''' <summary>
    ''' Lista las autorizaciones de facturación
    ''' </summary>
    ''' <remarks></remarks>
    ListBillingAuthotization

    ''' <summary>
    ''' Lista las condiciones de venta
    ''' </summary>
    ''' <remarks></remarks>
    ListConditionSales

    ''' <summary>
    ''' Lista las Areas de gestion
    ''' </summary>
    ''' <remarks></remarks>
    ListManagementAreas

    ''' <summary>
    ''' The list schedule payment confirm
    ''' </summary>
    ListSchedulePaymentConfirm
    ''' <summary>
    ''' listado de los saldos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    GetAllInitialBalance
    ''' <summary>
    ''' lista los anticipos del cliente para traslados
    ''' </summary>
    ''' <remarks></remarks>
    ListAdvanceTransfers
    ''' <summary>
    ''' lista las facturas para anticipos
    ''' </summary>
    ''' <remarks></remarks>
    ListBillsTransfers
    ''' <summary>
    ''' lista todos los servicios IPS
    ''' </summary>
    ''' <remarks></remarks>
    ListBillingConcept
    ''' <summary>
    ''' Lista los grupos de servicios ips por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServicesGroupByStatus
    ''' <summary>
    ''' lista todos los grupos servicios IPS
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServices
    ''' <summary>
    ''' Lista los servicios ips por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServicesByStatus
    ''' <summary>
    ''' lista los servicios IPS no quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServicesNoSurgical
    ''' <summary>
    ''' Lista los servicios IPS por Servicio
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServicesByService
    ''' <summary>
    ''' Lista los servicios IPS por servicio y presentacion
    ''' </summary>
    ''' <remarks></remarks>
    ListIPSServicesByServiceAndPresentation
    ''' <summary>
    ''' lista las plantillas de procedimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListProcedureTemplate
    ''' <summary>
    ''' lista las plantillas de procedimiento por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListProcedureTemplateByStatus
    ''' <summary>
    ''' lista las plantillas de requerimientos
    ''' </summary>
    ''' <remarks></remarks>
    ListRequirementTemplate
    ''' <summary>
    ''' lista las plantillas de requerimientos por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListRequirementTemplateByStatus

    ''' <summary>
    ''' Lista las marcas de los equipos de Mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListTrademark

    ''' <summary>
    ''' Lista los catalogos de bienes y servicios
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetCatalogOfPropertyandServices

    ''' <summary>
    ''' Lista de Partes, Accesorios y Consumibles
    ''' </summary>
    ''' <remarks></remarks>
    ListPartsAccesoriesConsumables
    ''' <summary>
    ''' lista todos los salarios 
    ''' </summary>
    ''' <remarks></remarks>
    ListContractMinimumWage
    ''' <summary>
    ''' lista los salarios minimos por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListContractMinimumWageByStatus

    ''' <summary>
    ''' Lista los Centros de Costo de Mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListCostCenterMaintenance

    ''' <summary>
    ''' Lista todos los niveles de riesgo
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryRiskLevel

    ''' <summary>
    ''' Lista todos los niveles de riesgo
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryDCI

    ''' <summary>
    ''' Lista todos los Contract Type
    ''' </summary>
    ''' <remarks></remarks>
    ListContractType

    ''' <summary>
    ''' Lista los Contract Type por estado
    ''' </summary>
    ''' <remarks></remarks>
    ListContractTypeByStatus

    ''' <summary>
    ''' Lista los Contract Type por estado (inventario)
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryContractType

    ''' <summary>
    ''' Lista todos los profesioanales
    ''' </summary>
    ''' <remarks></remarks>
    ListActiveProfessionals
    ''' <summary>
    ''' Lista todos los profesioanales
    ''' </summary>
    ''' <remarks></remarks>
    ListAllProfessionals

    ''' <summary>
    ''' Lista Todos Los pacientes
    ''' </summary>
    ''' <remarks></remarks>
    ListAllPatients

    ''' <summary>
    ''' lista las  solicitud de prestamo 
    ''' </summary>
    ''' <remarks></remarks>
    ListAllLoanMerchandise
    ''' <summary>
    ''' lista las devoluciones de préstamo
    ''' </summary>
    ''' <remarks></remarks>
    ListAllLoanMerchandiseDevolutions
    ''' <summary>
    ''' Lista las razones de anulacion
    ''' </summary>
    ListReversalReason
    ''' <summary>
    ''' lista de factura en cartera
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountReceivable
    ''' <summary>
    ''' Lista las cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    ListAccountReceivableByStatus
    ''' <summary>
    ''' lista los rubros por estado, vigencia y tipo 
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetCategoryByStatusAndBudgetaryValidityIdAndItemType
    ''' <summary>
    ''' lista los rubros por estado, vigencia y tipo 
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId
    ''' <summary>
    ''' Lista los tipos de presupuesto por estado, vigencia y tipo
    ''' </summary>
    ''' <remarks></remarks>
    ListRevenueTypeByBudgetValidityIdAndTypeAndStatus
    ''' <summary>
    ''' lista los items de presupuesto inicial por id de la vigencia y con saldo mayor a 0
    ''' </summary>
    ''' <remarks></remarks>
    ListBudgetByBudgetValidityId
    ''' <summary>
    ''' lista los items de pac inicial por id de la vigencia 
    ''' </summary>
    ''' <remarks></remarks>
    ListAnnualizedCashFlowValidityId
    ''' <summary>
    ''' lista las modificaciones del pac
    ''' </summary>
    ''' <remarks></remarks>
    ListPACModification
    ''' <summary>
    ''' lista los reconocimientos por vigencia y estado
    ''' </summary>
    ''' <remarks></remarks>
    ListRecognitionByValidityIdAndStatus
    ''' <summary>
    ''' lista las disponibilidades por vigencia y estado
    ''' </summary>
    ''' <remarks></remarks>
    ListAvailabilityByValidityIdAndStatus
    ''' <summary>
    ''' lista las modificacion de reocnocimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListRecognitionModificationByValidityId
    ''' <summary>
    ''' lista las modificacion de disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    ListAvailabilityModificationByValidityId
    ''' <summary>
    ''' lista los compromisos por vigencia 
    ''' </summary>
    ''' <remarks></remarks>
    ListCommitmentByValidityId
    ''' <summary>
    ''' lista los items de disponibilidad por id de la vigencia y estado
    ''' </summary>
    ''' <remarks></remarks>
    ListAvailabilityDetailByValidityIdAndStatus
    ''' <summary>
    ''' lista las modificacion de compromisos
    ''' </summary>
    ''' <remarks></remarks>
    ListCommitmentModificationByValidityId
    ''' <summary>
    ''' lista los comrpomisos por vigencia y estado
    ''' </summary>
    ''' <remarks></remarks>
    ListCommitmentByValidityIdAndStatus

    ListCollection

    ListCollectionModification

    ListObligation

    ''' <summary>
    ''' lista los items de obligación por id de la vigencia y estado
    ''' </summary>
    ''' <remarks></remarks>
    ListObligationDetailByValidityIdAndStatus
    ''' <summary>
    ''' lista las ordenes de pago por id de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    ListPaymentOrderByValidityId
    ''' <summary>
    ''' lista las ordenes de pago por vigencia y estado
    ''' </summary>
    ''' <remarks></remarks>
    ListPaymentOrderByValidityIdAndStatus
    ''' <summary>
    ''' lista los reintegros
    ''' </summary>
    ''' <remarks></remarks>
    ListReimbursementResourceByValidityId
    ''' <summary>
    ''' lista las suspenciones de presupuesto por id de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    ListSuspensionByValidityId
    ''' <summary>
    ''' lista las suspenciones de presupuesto por id de la vigencia ypor estado
    ''' </summary>
    ''' <remarks></remarks>
    ListSuspensionByValidityIdAndStatus
    ''' <summary>
    ''' lista los levantamientos de suspenciones de presupuesto por id de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    ListSuspensionCancellationByValidityId

    ListDocumentInvoiceProductSales

    ''' <summary>
    ''' Lista todas las categorías de facturas
    ''' </summary>
    ListInvoiceCategories

    ListFixedAssetTrademark

    ListFixedAssetPolicyType

    ListFixedAsetPoliza

    ListFixedAssetItemCatalog

    ListFixedAssetInsurance
    ListFixedAssetEquipmentCatalog
    ListFixedAssetEquipmentType
    ListFixedAssetEquipment
    ListFixedAssetEquipmentByStatus
    ListFixedAssetEquipmentByEquipmentType

    ''' <summary>
    ''' Lista todos los saldos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetInitialBalance

    ListFixedAssetInventoryType

    ListFixedAssetItemType

    ListFixedAssetResponsibleType

    ListFixedAssetVinculationType

    ListFixedAssetResponsible

    ListFixedAssetItem

    ListFixedAssetFixedAssetRemissionEntrance

    ListFixedAssetPartsAccesoriesConsumibles

    ListFixedAssetEntry

    ListFixedAssetTransfer

    ListFixedAssetRetirementTypes

    ListFixedAssetActiveOutput

    ListFixedAssetFixedAssetRemissionEntranceByStatus

    ListFixedAssetPurchaseOrder

    ListConceptsPayrollAll

    ListPurchaseOrderDetailBySupplierDistributionLineId

    ListFixedAssetItemByEquipmentType

    ListFixedAssetTransaction

    ListHardCollection

    ListLowTaxesLiquidation

    ListTaxesPropertyReport

    ListDirectDistributionSecondary

    ''' <summary>
    ''' Lista todos los centros de costo por estado y tipo de centro para los reportes
    ''' </summary>
    ''' <remarks></remarks>
    ListProductionCenterByStatusCenterTypeReport

    ''' <summary>
    ''' Lista la estructura Organizacional teniendo en cuenta el Centro de Producción.
    ''' </summary>
    ListOrganizationalStructureWithProductionCenter

    ''' <summary>
    ''' Lista la estructura Organizacional de costo nativo teniendo en cuenta el Centro de Producción.
    ''' </summary>
    ListCostOrganizationalStructureWithProductionCenter
    ''' <summary>
    ''' Lista las cuentas por que manejen tercero
    ''' </summary>
    ListMainAccountsByStatusAndBookIdHandledThirdParty

    ListGroupers

    ListGroupersByStatus

    GetAllAGACTIMED

    ''' <summary>
    ''' Lista todos los empleados que aun no han sido liquidados
    ''' </summary>
    AllEmployeesNoLiquidated

    ''' <summary>
    ''' Lista las cuentas del activo por ingreso o depreciacion y por libro
    ''' </summary>
    ListFixedAssetMainAccountsByTypeAndByBook

    ListAllEmployee

    ''' <summary>
    ''' Lista todas las Reclasificaciones de Activos
    ''' </summary>
    ''' <remarks></remarks>
    ListFixedAssetReclassification

    ''' <summary>
    ''' Lista todas los Indices de Deterioro
    ''' </summary>
    ''' <remarks></remarks>
    ListDeteriorationIndications
    ListMaintenanceProtocol

    ''' <summary>
    ''' Lista todas los Deportes Practicados
    ''' </summary>
    ''' <remarks></remarks>
    ListSportPractice

    ''' <summary>
    ''' DataSource de Deportes Practicados para el frontal talento humano
    ''' </summary>
    ''' <remarks></remarks>
    SportPractice

    ''' <summary>
    ''' Lista todas las Actividades en Tiempo Libre
    ''' </summary>
    ''' <remarks></remarks>
    ListFreeTimeUse

    ''' <summary>
    ''' DataSource de actividades en tiempo libre para el frontal talento humano
    ''' </summary>
    ''' <remarks></remarks>
    FreeTimeUse

    ''' <summary>
    ''' Lista todas las Enfermedades Diagnosticadas
    ''' </summary>
    ''' <remarks></remarks>
    ListDiagnosedDisease

    ''' <summary>
    ''' DataSource de enfermedades diagnosticadas para el frontal talento humano
    ''' </summary>
    ''' <remarks></remarks>
    DiagnosedDisease

    ''' <summary>
    ''' Lista todos las facturaciones basicas
    ''' </summary>
    ''' <remarks></remarks>
    ListBasicBillings

    ''' <summary>
    ''' Lista todos los archivos de planos de bancos
    ''' </summary>
    ''' <remarks></remarks>
    ListBankFiles

    ''' <summary>
    ''' Lista todas los distribuciones de facturas monto fijo
    ''' </summary>
    ''' <remarks></remarks>
    ListInvoiceEntityCapitatedDistributions

    ''' <summary>
    ''' Monedas
    ''' </summary>
    Currency

    ''' <summary>
    ''' Niveles de Cuentas Contables
    ''' </summary>
    ListMainAccountLevels

    ''' <summary>
    ''' Cotizacion
    ''' </summary>
    Quotation

    ''' <summary>
    ''' Lista todos los turnos
    ''' </summary>
    ListTurn

    ''' <summary>
    ''' Solicitud de compra
    ''' </summary>
    ListPurchaseRequest

    ''' <summary>
    ''' Lista todos los tipos de dosis unitaria
    ''' </summary>
    ListUnitDoseType

    ''' <summary>
    ''' Lista todos los tipos de dosis unitaria por estado
    ''' </summary>
    ListUnitDoseTypeByStatus

    ''' <summary>
    ''' Lista todas las tablas de estabilidad
    ''' </summary>
    ListStabilityTable

    ''' <summary>
    ''' Lista todas las solicitudes
    ''' </summary>
    ListRequestUnitDoseInventory

    ''' <summary>
    ''' Lista todas las solicitudes
    ''' </summary>
    ListRequestUnitDoseExternalCareCenter

    ''' <summary>
    ''' Lista todas las tablas de estabilidad
    ''' </summary>
    ListProductionBaskets

    ''' <summary>
    ''' Lista todas los trasnportes
    ''' </summary>
    ListTransportation

    ''' <summary>
    ''' Lista todas los auxiliares de trasporte
    ''' </summary>
    ListTransportationAssistant

    ''' <summary>
    ''' Lista todas los contratos de clientes externos
    ''' </summary>
    ListContractExternalClients

    ''' <summary>
    ''' Lista todas los contratos de clientes externos
    ''' </summary>
    ListExternalCareCenter

    ''' <summary>
    ''' Lista todas las causas de reproceso/rechazo
    ''' </summary>
    ListCauseReprocessingRejection

    ''' <summary>
    ''' Lista todos los defectos 
    ''' </summary>
    ListCategoryDefects
    ''' <summary>
    ''' Lista todos los pacientes centro atención externos
    ''' </summary>
    ListPatientExternalCareCenter

    ''' <summary>
    ''' Lista todos los tipos de estante
    ''' </summary>
    ListShelfType

    ''' <summary>
    ''' Lista todos los paquetes (productos) de central de mezclas
    ''' </summary>
    ListPackage

    ''' <summary>
    ''' Lista todas las cargas masivas de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    ListLoadMassive

    ''' <summary>
    ''' Lista todas las centrales de mezclas
    ''' </summary>
    ''' <remarks></remarks>
    ListMixingCenter

    ''' <summary>
    ''' Lista todas las lineas de Producción
    ''' </summary>
    ''' <remarks></remarks>
    ListProductionLine

    ''' <summary>
    ''' Lista todos los criterios de despeje de linea
    ''' </summary>
    ''' <remarks></remarks>
    ListLineClearanceCriteria

    ''' <summary>
    ''' Lista todos los estados de demanda
    ''' </summary>
    ListDemandStatus

    ''' <summary>
    ''' Lista todos los estatus de medicamentos
    ''' </summary>
    ListMedicationStatus

    ''' <summary>
    ''' Lista todas las funciones de equipos
    ''' </summary>
    ListEquipmentFunction
    ''' <summary>
    ''' Lista las distribuciones de mano de obra de un periodo
    ''' </summary>
    ListCostDistributionManpower
    ''' <summary>
    ''' Lista las categorias de centros de produccion
    ''' </summary>
    ListCostProductionCenterCategory
    ''' <summary>
    '''Lista las notas debito/credito de cuentas por pagar con valor de la nota.
    ''' </summary>
    ListLoadPaymentNotesDebitCredit

    ListReligiousBeliefs
    ReligiousBeliefs

    ListEthnicGroups
    EthnicGroups

    ''' <summary>
    ''' Lista controles de cheque
    ''' </summary>
    ListCheckCashingControl

    ''' <summary>
    ''' Lista conceptos de flujo de efectivo
    ''' </summary>
    ListCashFlowConcept

    ''' <summary>
    ''' Lista cuentas de ultimo nivel activas y de un libro 
    ''' </summary>
    ListMainAccountsLastLevel

    ''' <summary>
    ''' Lista centros de atencion
    ''' </summary>
    ListAttentionCenter

    ''' <summary>
    ''' Lista reclasificación de concepto de flujo de efectivo
    ''' </summary>
    ListCashFlowReclassification

    ''' <summary>
    ''' Lista insumos de inventario
    ''' </summary>
    ListInventorySupplie

    ''' <summary>
    ''' Lista de formatos de exógena
    ''' </summary>
    ListExogenousFormat

    ''' <summary>
    ''' Lista las conciliaciones bancarias
    ''' </summary>
    ListBankReconciliation

    ''' <summary>
    ''' lista las cesiones de contrato
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryContractAssignment

    ''' <summary>
    ''' lista los otro si de contrato
    ''' </summary>
    ''' <remarks></remarks>
    ListInventoryContractModification

    ''' <summary>
    ''' lista las Causa Devolución de Mercancía
    ''' </summary>
    ''' <remarks></remarks>
    ListDevolutionCause

    ''' <summary>
    ''' lista las Traslado de Devolución por Ingreso
    ''' </summary>
    ''' <remarks></remarks>
    ListPharmaceuticalDispensingTransfer

    ''' <summary>
    ''' Lista de Turnos de Empleados
    ''' </summary>
    ListEmployeeScheduleC

    ''' <summary>
    ''' Lista los motivos de postergación
    ''' </summary>
    ListPostponementReasons

    ''' <summary>
    ''' lista de CCPET
    ''' </summary>
    ListCCPET

    ''' <summary>
    ''' lista de CPCCatalog
    ''' </summary>
    ListCPCCatalog

    ''' <summary>
    ''' Lista las autorizaciones de documentos
    ''' </summary>
    ''' <remarks></remarks>
    ListDocumentSupportAuthorization

    ''' <summary>
    ''' Lista catalogo de articulos
    ''' </summary>
    ''' <remarks></remarks>
    ListItemCatalog


    ''' <summary>
    ''' Lista usuarios por container
    ''' </summary>
    ListUserContainer

    ''' <summary>
    ''' Lista concepto de conciliacion 
    ''' </summary>
    ''' <remarks></remarks>
    ListConciliationConcepts

    ''' <summary>
    ''' Lista fallas de equipos
    ''' </summary>
    ''' <remarks></remarks>
    ListMaintenanceFailureRequest

    ''' <summary>
    ''' Lista de contratos de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListMaintenanceContract

    ''' <summary>
    ''' Lista de herramientas
    ''' </summary>
    ''' <remarks></remarks>
    ListMaintenanceTools

    ''' <summary>
    ''' Lista de fabricantes de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    ListMaintenanceManufacturers

    ''' <summary>
    ''' lista de conciliaciones de cartera
    ''' </summary>
    ListPortfolioConciliation

    ''' <summary>
    ''' Lista de conceptos de conciliación bancaria
    ''' </summary>    
    ListBankConciliationConcepts

    ''' <summary>
    ''' Lista de convenios de redencion de productos
    ''' </summary>    
    ListAgreementsRedemptionPoints

    ''' <summary>
    ''' lista las causas de importunidad
    ''' </summary>
    ListImportunityCauses

    ''' <summary>
    ''' lista los conceptos de glosas de honorarios medicos
    ''' </summary>
    ListGlosaMedicalFeesConcepts

    ''' <summary>
    ''' lista las glosas de honorarios medicos
    ''' </summary>
    ListGlosaMedicalFees

    ''' <summary>
    ''' Lista los Conceptos Causas de Estado Folio
    ''' </summary>
    ListConceptsCausesStatusFolio
    ''' <summary>
    ''' lista los extractos bancarios
    ''' </summary>
    ListUploadBankStatements

    ''' <summary>
    ''' Lista las conciliaciones bancarias automaticas
    ''' </summary>
    ListBankReconciliationAutomatic

    ''' <summary>
    ''' Lista las parametrizaciones de los formatos de superSalud
    ''' </summary>
    ListHealthSuperParameters

    ''' <summary>
    ''' Lista las ordenes de trabajo
    ''' </summary>
    ListWorkOrder

    ''' <summary>
    ''' lista de Paquetes
    ''' </summary>
    ListContractPackages

    ''' <summary>
    ''' lista los servicios IPS por estado y el campo Presentacion
    ''' </summary>
    ListIPSServiceByStatusAndPresentation

    ''' <summary>
    ''' Lista los productos por estado activo
    ''' </summary>
    ListInventoryProductByStatus

    ''' <summary>
    ''' Lista las vigencias de manuales de tarifas
    ''' </summary>
    ListRateManualValidity

    ''' <summary>
    ''' Lista las devoluciones de materia prima
    ''' </summary>
    ListRawMaterialDevolution

    ''' <summary>
    ''' Lista de notas tecnicas
    ''' </summary>
    ListTechnicalNote

    ''' <summary>
    ''' Lista las políticas públicas
    ''' </summary>
    ListPublicPolicy

    ''' <summary>
    ''' Lista los Productos Catalogos
    ''' </summary>
    ListProductCatalog

    ''' <summary>
    ''' Lista los Modulos del menu
    ''' </summary>
    ListModule

    ''' <summary>
    ''' Lista los conceptos de glosas
    ''' </summary>
    ListGlosaConcept

    ''' <summary>
    ''' Lista los Formularios
    ''' </summary>
    ListForm

    ''' <summary>
    ''' Lista los Titulos
    ''' </summary>
    ListTitle

    ''' <summary>
    ''' Lista de documento soporte electronico
    ''' </summary>
    ListElectronicSupportDocument

    ''' <summary>
    ''' Lista de justificacion control de cuentas hospitalarias
    ''' </summary>
    ListBillingJustificationControl

    ''' <summary>
    ''' Lista de documento numeracion de autorizacion
    ''' </summary>
    ListDocumentNumberingAuthorization

    ''' <summary>
    ''' Lista de defectos de central de mezclas
    ''' </summary>
    ListDefects

    ListElectronicSupportDocumentAdjustmentNote

    ''' <summary>
    ''' Lista de los factores de dilucion
    ''' </summary>
    ListDilutionFactors

    ''' <summary>
    ''' Lista de los salarios minimos
    ''' </summary>
    ListMinimumSalary

    ''' <summary>
    ''' Lista de los subtipos de cotizantes
    ''' </summary>
    ListContributorSubtype

    ''' <summary>
    ''' Listado de parámetros de solicitures
    ''' </summary>
    ListRequestParam

    ''' <summary>
    ''' Listado de los ejecutivos de ventas
    ''' </summary>
    ListSalesExecutive

    ''' <summary>
    ''' Listado de consignaciones en traslado
    ''' </summary>
    ListInventoryConsignmentTransfer

    ''' <summary>
    ''' Listado de tarifas de productos y servicios
    ''' </summary>
    ListProductAndServicesFee

    ''' <summary>
    ''' Listado de conceptos de licencia
    ''' </summary>
    ListLicensingConcepts

    ''' <summary>
    ''' Lista que traera los conceptos de licencia con filtro 
    ''' </summary>
    ListLicensingConceptsNovelty

    ''' <summary>
    ''' Lista de Detalle de radicado de factruras
    ''' </summary>
    ListInvoiceRadicateDetail

    ''' <summary>
    ''' Lista que traerá los grupos de servicios RIPS
    ''' </summary>
    ListRIPSServiceGroups

    ''' <summary>
    ''' Lista los servicios No facturables
    ''' </summary>
    ListBillingItemsRestriction

    ''' <summary>
    ''' Lista que traerá los servicios RIPS
    ''' </summary>
    ListRIPSServices

    ''' <summary>
    ''' Lista que trae el estado civil
    ''' </summary>
    ListMaritalStatus

    ''' <summary>
    ''' Lista que trae las Unidades UPR
    ''' </summary>
    ListUPRUnits

    ''' <summary>
    ''' Lista todas las formas farmcéuticas
    ''' </summary>
    ''' <remarks></remarks>
    ListPharmaceuticalFormGrouping

    ''' <summary>
    ''' Obtiene una programación de pagos por codigo 
    ''' </summary>
    ''' <remarks></remarks>
    GetSchedulePaymentTreasuryByCode

    ''' <summary>
    ''' Lista los registros de clasificación deterioro de cartera 
    ''' </summary>
    ListPortfolioDeteriorationClassification

    ''' <summary>
    ''' Lista los registros de tipo de medicamentos
    ''' </summary>
    ListMedicationType

    ''' <summary>
    ''' Lista los motivos de rechazo
    ''' </summary>
    ListRejectionReason

    ''' <summary>
    ''' Lista de los conceptos de nómina electrónica
    ''' </summary>
    ElectronicPayrollConcepts

    ''' <summary>
    ''' Lista los profesionales de la salud int o ext
    ''' </summary>
    ListAllHealthProfessional

    ''' <summary>
    ''' Lista todos los registros de soporte RIPS
    ''' </summary>
    ListAllRIPSSupportRecords

End Enum