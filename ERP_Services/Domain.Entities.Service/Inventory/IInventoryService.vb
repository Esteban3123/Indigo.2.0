Imports System.Collections.Generic
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IInventoryService
    Inherits IDisposable

#Region "Methods"

    Function SetCopyPasteOrImportFileProductRate(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of ProductRateDetail))
    Function SetCopyPasteOrImportFileProductInTransit(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of ProductInTransitDetail))



    Function LoadListHierarchyProduct(ByVal inventoryProduct As InventoryProduct) As List(Of ProductHierarchy)

    Function ValidateInventoryPeriod(documentDate As DateTime, OperatingUnit As Integer) As ActionResult(Of SettingInventory)

    Function ValidateInventoryProductSave(ByVal product As InventoryProduct) As ActionResult

    Function ValidateQuantityPhysicalInventory(ByVal product As InventoryProduct) As ActionResult

    Function LoadJournalVoucherTransferOrder(transferOrder As Domain.Entities.TransferOrder) As ActionResult(Of JournalVouchers)

    Function LoadJournalVoucherTransferOrderDevolution(transferOrderDevolution As Domain.Entities.TransferOrderDevolution) As ActionResult(Of JournalVouchers)

    Function LoadJournalVoucherDevolution(pharmaceuticalDispensingDevolution As PharmaceuticalDispensingDevolution) As ActionResult(Of JournalVouchers)

    Function ValidateStockProducts(productId As Integer, operatingUnitId As Integer, Optional warehouseId As Integer = 0, Optional quantity As Integer = 0, Optional movement As InventoryStaticServices.MovementType = 0, Optional product As InventoryProduct = Nothing, Optional settingInventory As SettingInventory = Nothing) As ActionResult(Of InventoryProduct)

    ''' <summary>
    ''' validar la devolucion del suministro 
    ''' </summary>
    ''' <param name="pharmaceuticalDispensingDevolutionDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidatePharmaceuticalDispensingDevolutionDetail(pharmaceuticalDispensingDevolutionDetail As PharmaceuticalDispensingDevolutionDetail) As ActionResult
    ''' <summary>
    ''' metodo para validar los items del archivo de excel que se esta importando
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetProductsInventoryControlImportFile(data As List(Of ImportFileRow), warehouseId As Integer, controlType As Integer, audit As AuditMessage, documentDate As Date, operatingUnitId As Integer) As ActionResult(Of List(Of InventoryControlDetail))
    ''' <summary>
    ''' metodo para validar los iteem pegados en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetProductsInventoryControlCopyPaste(data As List(Of List(Of String)), warehouseId As Integer, controlType As Integer, audit As AuditMessage) As ActionResult(Of List(Of InventoryControlDetail))

    Function SetProductsPurchaseOrderImportFile(data As List(Of ImportFileRow), operatingUnitId As Integer, audit As AuditMessage, Optional roundingType As Decimal = 0.01) As ActionResult(Of List(Of PurchaseOrderDetail))

    Function SetProductsProductInvoiceImportFile(data As List(Of ImportFileRow), warehouseId As Integer, operatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of List(Of DocumentInvoiceProductSalesDetail))
    Function GetMeasurementUnitByProductId(productId As Integer) As List(Of Tuple(Of Integer, Decimal))
    ''' <summary>
    ''' Obtiene la unidad de medida de acuerdo al ID del producto y el MSClass de la campaña.
    ''' Para NPT (MSClass = 2), siempre usa VolumeMeasureUnit.
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="msClass">Tipo de dosis unitaria (MSClass)</param>
    ''' <returns></returns>
    Function GetMeasurementUnitByProductId(productId As Integer, msClass As Integer) As List(Of Tuple(Of Integer, Decimal))
    ''' <summary>
    ''' Obtiene la unidad de medida y la concentración por id del producto
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Function GetMeasurementUnitAndConcentrationByProductId(productId As Integer) As (measurementUnitId As Integer, concentration As Decimal)
    ''' <summary>
    ''' Obtiene la unidad de medida y la concentración por id del producto
    ''' </summary>
    ''' <param name="productIds">Lista de IDs de productos</param>
    ''' <param name="msClass">Tipo de dosis unitaria (MSClass). Para NPT (MSClass = 2) usa VolumeMeasureUnit</param>
    ''' <returns></returns>
    Function GetMeasurementUnitsAndConcentrationsByProductIds(productIds As List(Of Integer), Optional msClass As Integer = 0) As List(Of (ProductValidationId As Integer, MeasurementUnitId As Integer, Concentration As Decimal))

    ''' <summary>
    ''' Valida informacion extraida de Excel para ConsignmentTransfer
    ''' </summary>
    ''' <param name="dataImport">Datos del archivo Excel</param>
    ''' <param name="sourceWarehouseId">Id del almacen origen</param>
    ''' <returns></returns>
    Function SetConsignmentTransferImportFile(dataimport As List(Of ImportFileRow), sourceWarehouseId As Integer) As ActionResult(Of List(Of ConsignmentTransferDetail))
#End Region

End Interface
