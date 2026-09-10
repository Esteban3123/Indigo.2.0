'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 30-01-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

Public Interface ISettingInventory
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna el año del periodo actual de invetarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ReadOnly Property Year As Integer

    ''' <summary>
    ''' Obtiene o asigna el mes del  periodo actual de inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ReadOnly Property Month As Integer

    ''' <summary>
    ''' Especifica como se va a llevar el control del stock
    ''' </summary>
    ''' <value>1.General 2.Por Almacen</value>
    ''' <returns></returns>
    Property StockControl As Integer?

    ''' <summary>
    ''' Especifica el tipo de comprobante para las compras de inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Property PurchaseJournalVoucherTypeId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes contables para las compras
    ''' </summary>
    ''' <value></value>
    Property PurchaseJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el tipo de comprobante contable que se va a crear cuando se realice un ajuste de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InventoryAdjustmentJournalVoucherTypeId As Integer

    ''' <summary>
    ''' Propiedad que contiene el listado de los tipos de comprobantes para un ajuste de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InventoryAdjustmentJournalVoucherTypeXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el tipo de comprobante contable que se va a crear cuando se realice un ajuste en el cierre mensual de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InventoryCloseAdjustmentJournalVoucherTypeId As Integer

    ''' <summary>
    ''' Propiedad que contiene el listado de los tipos de comprobantes para un ajuste en el cierre mensual de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InventoryCloseAdjustmentJournalVoucherTypeXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el tipo de comprobante contable para cuando se realice una venta
    ''' </summary>
    ''' <value></value>
    Property SalesJournalVoucherTypeId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes contables para cuando se realicen ventas
    ''' </summary>
    ''' <value></value>
    Property SalesJournalVoucherTypeIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Especifica el id del tipo del comprobante para cuando se ejecuta una remision de entrada
    ''' </summary>
    ''' <value></value>
    Property RemissionEntranceJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de compronates contables para cuando se ejecuta una remision de entrada
    ''' </summary>
    ''' <value></value>
    Property RemissionEntranceJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el id del tipo del comprobante para cuando se ejecuta una remision de entrada devolution
    ''' </summary>
    ''' <value></value>
    Property RemissionEntranceDevolutionJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de compronates contables para cuando se ejecuta una remision de entrada devolution
    ''' </summary>
    ''' <value></value>
    Property RemissionEntranceDevolutionJournalVoucherTypeIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Especifica el id del tipo del comprobante para cuando se realiza remisiones de salida
    ''' </summary>
    ''' <value></value>
    Property RemissionOutputJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado el tipo de comprobantes contables para cuando se realizan remisiones de salida
    ''' </summary>
    ''' <value></value>
    Property RemissionOutputJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el id del tipo del comprobante para cuando se realiza remisiones de salida devolution
    ''' </summary>
    ''' <value></value>
    Property RemissionOutputDevolutionJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado el tipo de comprobantes contables para cuando se realizan remisiones de salida devolution
    ''' </summary>
    ''' <value></value>
    Property RemissionOutputDevolutionJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el id del tipo del comprobante para cuando se realiza reclasificación de remisiones
    ''' </summary>
    ''' <value></value>
    Property ReclassificationRemissionJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado el tipo de comprobantes contables para cuando se realizan reclasificación de remisiones
    ''' </summary>
    ''' <value></value>
    Property ReclassificationRemissionJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el id del tipo del comprobante que se debe generar al crear una devolucion prestamo de mercancia
    ''' </summary>
    ''' <value></value>
    Property LoanReturnJournalVoucherTypeId As Integer
    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes que se deben generar al crear una devolucion prestamo de mercancia
    ''' </summary>
    ''' <value></value>
    Property LoanReturnJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes
    ''' </summary>
    ''' <value></value>
    Property PartialReturnSalesJournalVoucherTypeXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el id del tipo del comprobante que se debe generar al crear un prestamo de mercancia
    ''' </summary>
    ''' <value></value>
    Property LoanJournalVoucherTypeId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes que se deben generar al crear un prestamo de mercancia
    ''' </summary>
    ''' <value></value>
    Property LoanJournalVoucherTypeIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Especifica el id del tipo del comprobante que se debe generar al hacer una devolucion en ventas
    ''' </summary>
    ''' <value></value>
    Property SalesReturnJournalVoucherTypeId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes contables que se deben generar al hacer una devolucion en ventas
    ''' </summary>
    ''' <value></value>
    Property SalesReturnJournalVoucherTypeIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Especifica el id del tipo del comprobante que se debe generar al hacer una devolucion en compras
    ''' </summary>
    ''' <value></value>
    Property PurchaseReturnJournalVoucherTypeId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes contables que se deben generar al hacer una devolucion en compras
    ''' </summary>
    ''' <value></value>
    Property PurchaseReturnJournalVoucherTypeIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del tipo del comprobante contable que se genera al hacer una orden de Traslado
    ''' </summary>
    ''' <value></value>
    Property OrderDispatchJournalVoucherTypeId As Integer
    ''' <summary>
    ''' Propiedad que contiene el listado de comprobantes contables que se generan al hacer una orden de Traslado
    ''' </summary>
    ''' <value></value>
    Property OrderDispatchJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del tipo del comprobante contable que se genera al hacer un registro de mercancia en consignación
    ''' </summary>
    ''' <value></value>
    Property ConsignmentMerchandiseJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de comprobantes contables que se generan al hacer un registro de mercancia en consignación
    ''' </summary>
    ''' <value></value>
    Property ConsignmentMerchandiseJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del tipo del comprobante contable que se genera al hacer un registro de devolución de mercancia en consignación
    ''' </summary>
    ''' <value></value>
    Property ConsignmentMerchandiseReturnJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de comprobantes contables que se generan al hacer un registro de devolución de mercancia en consignación
    ''' </summary>
    ''' <value></value>
    Property ConsignmentMerchandiseReturnJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del tipo del comprobante contable que se genera al hacer un registro de uso de mercancia en consignación
    ''' </summary>
    ''' <value></value>
    Property ConsignmentInventoryUseJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de comprobantes contables que se generan al hacer un registro de uso de mercancia en consignación
    ''' </summary>
    ''' <value></value>
    Property ConsignmentInventoryUseJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del tipo del comprobante contable que se genera al hacer un registro de devolución de uso de mercancia en consignación
    ''' </summary>
    ''' <value></value>
    Property ConsignmentInventoryUseDevolutionJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de comprobantes contables que se generan al hacer un registro de devolución de uso de mercancia en consignación
    ''' </summary>
    ''' <value></value>
    Property ConsignmentInventoryUseDevolutionJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del tipo del comprobante contable que se genera al hacer una devolucion de una orden de despacho
    ''' </summary>
    ''' <value></value>
    Property OrderDispatchReturnJournalVoucherTypeId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de comprobantes contables que se generan al hacer una devolucion de una orden de despacho
    ''' </summary>
    ''' <value></value>
    Property OrderDispatchReturnJournalVoucherTypeIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del concepto de cuentas por pagar par a el porcentaje de IVA del flete, Este concepto de cuenta por pagar debe Manejar Retencion y debe ser de tipo Especifico
    ''' </summary>
    ''' <value></value>
    Property IVAFreightAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de conceptos por pagar a el porcentaje de IVA del flete
    ''' </summary>
    ''' <value></value>
    Property IVAFreightAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del concepto de cuentas por pagar para el flete, Este concepto de cuenta por pagar NO debe Manejar Retencion y debe ser de tipo Especifico
    ''' </summary>
    ''' <value></value>
    Property FreightAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas por pagar para el flete
    ''' </summary>
    ''' <value></value>
    Property FreightAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del concepto de cuenta por pagar para el impuesto distrital de prodesarrollo, Este concepto debe Manejar Retencion y debe ser de tipo Especifico
    ''' </summary>
    ''' <value></value>
    Property ProDevelopmentAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Listados de conceptos de cuenta por pagar para el impuesto distrital de prodesarrollo
    ''' </summary>
    ''' <value></value>
    Property ProDevelopmentAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del concepto de cuenta por pagar para el impuestro distrital de proElectrificacion, Este concepto debe Manejar Retencion y debe ser de tipo Especifico
    ''' </summary>
    ''' <value></value>
    Property ProElectrificationAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Listado de conceptos de cuenta por pagar para el impuesto distrital de proElectrifricacion
    ''' </summary>
    ''' <value></value>
    Property ProElectrificationAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del concepto de cuenta por pagar para el impuesto distrital de procultura, Este concepto debe Manejar Retencion y debe ser de tipo Especifico
    ''' </summary>
    ''' <value></value>
    Property ProCultureAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Listado de conceptos de cuenta por pafar para el impuesto distrital de proCultura
    ''' </summary>
    ''' <value></value>
    Property ProCultureAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del concepto de cuenta por pagar para el impuesto distrital de pro Hospital, Este concepto debe Manejar Retencion y debe ser de tipo Especifico
    ''' </summary>
    ''' <value></value>
    Property ProHospitalAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Listado de conceptos de cuenta por pagar para el impuesto distrital de proHospital
    ''' </summary>
    ''' <value></value>
    Property ProHospitalAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del concepto de cuenta por pagar para el impuesto distrital de pro Juego, Este concepto debe Manejar Retencion y debe ser de tipo Especifico
    ''' </summary>
    ''' <value></value>
    Property ProGameAccountPayableConceptId1 As Integer?
    ''' <summary>
    ''' Listado de conceptos de cuenta por pagar para el impuesto distrital proJuegos
    ''' </summary>
    ''' <value></value>
    Property ProGameAccountPayableConceptId1Xpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Especifica de donde se va a sacar la retencion del iva
    ''' </summary>
    ''' <value>1-Tercero 2-Concepto</value>
    Property IVARetention As Integer?

    ''' <summary>
    ''' Especifica de donde se va a sacar la retencion del iva
    ''' </summary>
    ''' <value>1-Unidad Funcional 2-Concepto de facturación</value>
    Property PharmacySuppliesCostCenter As Integer

    ''' <summary>
    ''' Id del concepto de pagos para la retencion del IVA, 
    ''' este concepto debe de ser de tipo Especifico 
    ''' y debe manejar Retencion, Solo se llena cuando 
    ''' la rentencion del iva se obtiene del concepto (IVARetention - 2)
    ''' </summary>
    ''' <value></value>
    Property IVARetentionAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Listado de conceptos de pagos para la retencion del IVA
    ''' </summary>
    ''' <value></value>
    Property IVARetentionAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Property FilingUnitId As Integer?
    ''' <summary>
    ''' Propiedad que contiene la lista de unidades de radicacion
    ''' </summary>
    ''' <value></value>
    Property FilingUnitIdXpo As XPCollection


    ''' <summary>
    ''' Id del concepto de pagos para el IVA, este concepto debe de ser de tipo Especifico y NO debe manejar Retencion, 
    ''' Este concepto es utilizado cuando se confirma el comprobante de entrada y genera la cuenta por pagar
    ''' este representaria al iva descontable
    ''' </summary>
    ''' <value></value>
    Property IVAAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Propiedad que contiene la lista de iva que es de tipo especifico y no maneja retencion
    ''' </summary>
    ''' <value></value>
    Property IVAAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Especifica el id de la cuenta contable para almacenar el iva GENERADO
    ''' </summary>
    ''' <value></value>
    Property IVAGeneratedMainAccountId As Integer?
    ''' <summary>
    ''' listado de cuentas contables para almacenar el IVA generado
    ''' </summary>
    ''' <value></value>
    Property IVAGeneratedMainAccountIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Id del concepto de la cuenta por pagar para el ajuste de redondeo, El concepto de ajuste debe ser de Tipo Especifico y NO debe manejar retencion
    ''' </summary>
    ''' <value></value>
    Property AdjustmentAccountPayableConceptId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de la cuentas por pagar para el ajuste de redondeo
    ''' </summary>
    ''' <value></value>
    Property AdjustmentAccountPayableConceptIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Especifica de donde se va tomar el centro de costo para cuando se hacen los comprobantes contables
    '''1 - Inventarios(Sin CC), Costo(Unidad Funcional)
    '''2 - Inventarios(Grupo), Costo(Grupo)
    '''3 - Inventarios(Almacen), Costo(Almacen)
    ''' </summary>
    ''' <value></value>
    Property AssociateCostCenter As Integer?


    ''' <summary>
    ''' Especifica de donde se va tomar la cuenta contable de costo cuando se hacen los comprobantes contables
    '''1 - Parámetros de Inventario
    '''2 - Grupos del Producto
    ''' </summary>
    ''' <value></value>
    Property AssociateCostMainAccount As Integer?


    ''' <summary>
    ''' Especifica la cuenta contable del descuento para ventas, En esta cuenta se cargan los descuento realizados a través de la dispensacion farmaceutica
    ''' </summary>
    ''' <value></value>
    Property DiscountSalesMainAccountId As Integer?
    ''' <summary>
    ''' Propiedad que contiene la lista de los decuentos realizados a travez de la dispenzacion farmaceutica
    ''' </summary>
    ''' <value></value>
    Property DiscountSalesMainAccountIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica el id del concepto de nota de pagos que se va usar cuando se haga una devolucion del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    Property RefundAccountPayableConceptNoteId As Integer?
    ''' <summary>
    ''' Propiedad que contiene la lista de los concepto de nota de pagos que se va usar cuando se haga una devolucion del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    Property RefundAccountPayableConceptNoteXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del fucnional</value>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

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
    ''' obtiene o establece el id del concepto de ajuste de inventario de tipo entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InputAdjustmentConceptId As Integer

    ''' <summary>
    ''' Establece el datasource de los conceptos de ajuste de inventarios de tipo entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InputAdjustmentConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece el id del concepto de ajuste de inventario de tipo salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OutputAdjustmentConceptId As Integer

    ''' <summary>
    ''' Establece el datasource de los conceptos de ajuste de inventarios de tipo salida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OutputAdjustmentConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de iva
    ''' </summary>
    Property IVADatasource As XPInstantFeedbackSource


    ''' <summary>
    ''' Obtiene permiso de bar code
    ''' </summary>
    ''' <returns></returns>
    Property Nomanualbarcodepermission As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property AutomaticPharmacyRequest As Boolean?

    ''' <summary>
    ''' Validar productos próximos a vencer
    ''' </summary>
    ''' <returns></returns>
    Property ValidateBatchSerialExpiredDate As Boolean?

    ''' <summary>
    ''' Indica si se valida patologías pos en medicamentos
    ''' </summary>
    ''' <returns></returns>
    Property ValidatePOSPathologies As Boolean?

    ''' <summary>
    ''' Reporte a usar en el dashboard de farmacia
    ''' </summary>
    ''' <returns></returns>
    Property PharmacyDashboardReport As Byte?

    ''' <summary>
    ''' Dispensación de paquetes qx
    ''' </summary>
    ''' <returns></returns>
    Property PackageDispensingMethod As Boolean?

    ''' <summary>
    ''' Indica si se valida dispensaciones de farmacia por unidad funcional
    ''' </summary>
    ''' <returns></returns>
    Property PharmacyDashboardFromRequestWarehouse As Boolean?

    ''' <summary>
    ''' registro de IVA
    ''' </summary>
    ''' <returns></returns>
    Property TaxRegistration As Byte

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para Traslado entre almacenes en consignación
    ''' </summary>
    Property TransferBetweenWarehousesConsignmentId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id de tipo de comprobante contable para Valorización por lista de precios-consignación
    ''' </summary>
    Property ValuationConsignmentPriceJournalVoucherTypesId As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes
    ''' </summary>
    ''' <value></value>
    Property TransferBetweenWarehousesConsignmentXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Propiedad que contiene el listado de tipos de comprobantes
    ''' </summary>
    ''' <value></value>
    Property ValuationConsignmentPriceJournalVoucherTypesIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el Id del almacén principal
    ''' </summary>
    Property MainWarehouse As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseXpo As XPInstantFeedbackSource
#End Region

End Interface
