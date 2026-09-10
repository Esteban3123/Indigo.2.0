Imports System.Data
Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class InventoryServices
    Implements IInventoryService

#Region "Variables"
    Private _inventoryProductRepository As IInventoryProductRepository
    Private _productTypeRepository As IProductTypeRepository
    Private _settingInventoryrepository As ISettingInventoryRepository
    Private _productRateDetailRepository As IProductRateDetailRepository
    Private _productGroupsRepository As IProductGroupsRepository
    Private _mainAccountsRepository As IPUCRepository
    Private _functionaUnitRepository As IFunctionalUnitRepository
    Private _wareHouseRepository As IWarehouseRepository
    Private _caregroupRepository As ICareGroupRepository
    Private _contractRepository As IContractRepository
    Private _healthAdministratorRepository As IHealthAdministratorRepository
    Private _admissionRepository As IAdmissionRepository
    Private _accountPayableConceptRepository As IPaymentsConceptRepository
    Private _thirdPartyRepository As Domain.Entities.IThirdPartyRepository
    Private _revenueControlDetailRepository As IRevenueControlDetailRepository
    Private _serviceOrderRepository As IServiceOrderRepository
    Private _serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository
    Private _pharmaceuticalDispensingDetailRepositoy As IPharmaceuticalDispensingDetailRepository
    Private _adjusmentConceptRepository As IAdjustmentConceptRepository
    Private _transferOrderRepository As ITransferOrderRepository
    Private _transferOrderDetailRepository As ITransferOrderDetailRepository
    Private _transferOrderDetailBatchSerialRepository As ITransferOrderDetailBatchSerialRepository
    Private _physicalInventory As IPhysicalInventoryRepository
    Private _warehouseStockRepository As IWarehouseStockRepository
    Private _batchSerialRepository As IBatchSerialRepository
    Private _generalLedgerIVARepository As IGeneralLedgerIVARepository
    Private _atcRepository As IATCRepository
    Private _cupsEntityRepository As ICupsEntityRepository

    Private Const MODULE_NAME_INVENTORY As String = "Inventory"
#End Region

#Region "Builders"
    Public Sub New(inventoryProductRepository As IInventoryProductRepository, productTypeRepository As IProductTypeRepository, settingInventoryrepository As ISettingInventoryRepository,
                   productRateDetailRepository As IProductRateDetailRepository, productGroupsRepository As IProductGroupsRepository, mainAccountsRepository As IPUCRepository,
                   functionaUnitRepository As IFunctionalUnitRepository, wareHouseRepository As IWarehouseRepository, caregroupRepository As ICareGroupRepository,
                   contractRepository As IContractRepository, healthAdministratorRepository As IHealthAdministratorRepository, admissionRepository As IAdmissionRepository,
                   accountPayableConceptRepository As IPaymentsConceptRepository, thirdPartyRepository As Domain.Entities.IThirdPartyRepository,
                   revenueControlDetailRepository As IRevenueControlDetailRepository, serviceOrderRepository As IServiceOrderRepository, serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository,
                   pharmaceuticalDispensingDetailRepositoy As IPharmaceuticalDispensingDetailRepository, adjusmentConceptRepository As IAdjustmentConceptRepository,
                   transferOrderRepository As ITransferOrderRepository, transferOrderDetailRepository As ITransferOrderDetailRepository, transferOrderDetailBatchSerialRepository As ITransferOrderDetailBatchSerialRepository,
                   physicalInventory As IPhysicalInventoryRepository, warehouseStockRepository As IWarehouseStockRepository, batchSerialRepository As IBatchSerialRepository,
                   generalLedgerIVARepository As IGeneralLedgerIVARepository, CupsEntityRepository As ICupsEntityRepository, atcRepository As IATCRepository)
        _generalLedgerIVARepository = generalLedgerIVARepository
        _inventoryProductRepository = inventoryProductRepository
        _productTypeRepository = productTypeRepository
        _settingInventoryrepository = settingInventoryrepository
        _productRateDetailRepository = productRateDetailRepository
        _productGroupsRepository = productGroupsRepository
        _mainAccountsRepository = mainAccountsRepository
        _functionaUnitRepository = functionaUnitRepository
        _wareHouseRepository = wareHouseRepository
        _caregroupRepository = caregroupRepository
        _contractRepository = contractRepository
        _healthAdministratorRepository = healthAdministratorRepository
        _admissionRepository = admissionRepository
        _accountPayableConceptRepository = accountPayableConceptRepository
        _thirdPartyRepository = thirdPartyRepository
        _revenueControlDetailRepository = revenueControlDetailRepository
        _serviceOrderRepository = serviceOrderRepository
        _serviceOrderDetailDistributionRepository = serviceOrderDetailDistributionRepository
        _pharmaceuticalDispensingDetailRepositoy = pharmaceuticalDispensingDetailRepositoy
        _adjusmentConceptRepository = adjusmentConceptRepository
        _transferOrderRepository = transferOrderRepository
        _transferOrderDetailRepository = transferOrderDetailRepository
        _transferOrderDetailBatchSerialRepository = transferOrderDetailBatchSerialRepository
        _physicalInventory = physicalInventory
        _warehouseStockRepository = warehouseStockRepository
        _batchSerialRepository = batchSerialRepository
        _cupsEntityRepository = CupsEntityRepository
        _atcRepository = atcRepository
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="InventoryServices"/> class.
    ''' </summary>
    Public Sub New(ByVal inventoryProductRepository As IInventoryProductRepository)
        _inventoryProductRepository = inventoryProductRepository
    End Sub

    Public Sub New(physicalInventory As IPhysicalInventoryRepository)
        _physicalInventory = physicalInventory
    End Sub

    ''' <summary>
    ''' constructor para validar el periodo abierto de inventario
    ''' </summary>
    ''' <param name="settingInventoryrepository"></param>
    ''' <remarks></remarks>
    Public Sub New(settingInventoryrepository As ISettingInventoryRepository)
        _settingInventoryrepository = settingInventoryrepository
    End Sub

    Public Sub New(inventoryProductRepository As IInventoryProductRepository, atcRepository As IATCRepository)
        _inventoryProductRepository = inventoryProductRepository
        _atcRepository = atcRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Llena el listado de Jerarquías de un producto
    ''' </summary>
    Public Function LoadListHierarchyProduct(ByVal inventoryProduct As InventoryProduct) As List(Of ProductHierarchy) Implements IInventoryService.LoadListHierarchyProduct
        Dim _listProductHierarchy = New List(Of ProductHierarchy)()
        Dim _inventoryProduct As InventoryProduct = inventoryProduct
        While _inventoryProduct.ProductHierarchy IsNot Nothing AndAlso _inventoryProduct.ProductHierarchy.Count > 0
            _inventoryProduct = _inventoryProductRepository.GetInventoryProductById(_inventoryProduct.ProductHierarchy.ElementAt(0).ParentProductId)
            If _inventoryProduct Is Nothing OrElse _inventoryProduct.Id = 0 OrElse _inventoryProduct.ProductHierarchy.Count = 0 Then
                Exit While
            End If
            Dim _hierachyProduct As ProductHierarchy = _inventoryProduct.ProductHierarchy.FirstOrDefault()
            _inventoryProduct.ProductHierarchy.Add(_hierachyProduct)
            '_inventoryProduct.ProductHierarchy1.Add(_listProductHierarchy.ElementAt(_inventoryProduct.ProductHierarchy.Count - 1))
            _listProductHierarchy.Add(_hierachyProduct)
        End While

        Return _listProductHierarchy
    End Function
    ''' <summary>
    ''' valida que la fecha del documento se encuentre en el periodo abierto de inventario
    ''' </summary>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateInventoryPeriod(documentDate As DateTime, OperatingUnit As Integer) As ActionResult(Of SettingInventory) Implements IInventoryService.ValidateInventoryPeriod
        Dim settingInventory = _settingInventoryrepository.GetSettingInventoryByOperatingUnitId(OperatingUnit)
        If settingInventory Is Nothing Then
            Return New ActionResult(Of SettingInventory) With {.StateResult = False, .Message = "No se ha creado una configuración para el modulo de inventarios"}
        Else
            If settingInventory.Year <> documentDate.Year OrElse settingInventory.Month <> documentDate.Month Then
                Return New ActionResult(Of SettingInventory) With {.StateResult = False, .Message = "El periodo actual de inventario no coincide con la fecha del documento, Periodo Actual de Inventario: " + settingInventory.Year.ToString() + "-" + If(settingInventory.Month < 10, "0" + settingInventory.Month.ToString(), settingInventory.Month.ToString())}
            End If
        End If
        Return New ActionResult(Of SettingInventory) With {.StateResult = True, .ObjectEmbbeded = settingInventory}
    End Function

    ''' <summary>
    ''' valida informacion extraida del Excel para ConsignmentTransfer
    ''' </summary>
    Public Function SetConsignmentTransferImportFile(dataimport As List(Of ImportFileRow), sourceWarehouseId As Integer) As ActionResult(Of List(Of ConsignmentTransferDetail)) Implements IInventoryService.SetConsignmentTransferImportFile
        Return New ActionResult(Of List(Of ConsignmentTransferDetail)) With {
        .StateResult = True,
        .ObjectEmbbeded = New List(Of ConsignmentTransferDetail)
    }
    End Function

#Region "Validate Stock"

    ''' <summary>
    ''' Funcion para validar el stoc del producto
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateStockProducts(productId As Integer, operatingUnitId As Integer, Optional warehouseId As Integer = 0, Optional quantity As Integer = 0, Optional movement As InventoryStaticServices.MovementType = 0, Optional product As InventoryProduct = Nothing, Optional settingInventory As SettingInventory = Nothing) As ActionResult(Of InventoryProduct) Implements IInventoryService.ValidateStockProducts
        If settingInventory Is Nothing OrElse settingInventory.Id = 0 Then
            settingInventory = _settingInventoryrepository.GetSettingInventoryByOperatingUnitId(operatingUnitId)
        End If
        If settingInventory Is Nothing OrElse settingInventory.Id = 0 Then
            Return New ActionResult(Of InventoryProduct) With {.StateResult = False, .Message = {ResourceManager.GetString("SettingParameter", "Inventory")}.ToString()}
        End If

        If product Is Nothing Then
            product = _inventoryProductRepository.GetInventoryProductByIdSimple(productId, False)
        End If

        Dim result As ActionResult(Of InventoryProduct) = New ActionResult(Of InventoryProduct)
        result.StateResult = True
        result.ObjectEmbbeded = product

        If settingInventory.StockControl = 1 Then
            Dim quantityPhysicalByProduct = _physicalInventory.GetQuantityByProduct(productId)
            If quantity <> 0 Then
                If movement = InventoryStaticServices.MovementType.Input Then
                    quantityPhysicalByProduct = quantityPhysicalByProduct + quantity
                ElseIf movement = InventoryStaticServices.MovementType.OutPut Then
                    quantityPhysicalByProduct = quantityPhysicalByProduct - quantity
                End If
            End If

            If product.MinimumStock > quantityPhysicalByProduct Then
                result.StateResult = False
                result.Message = "El Producto " & product.Code & " - " & product.Name & " alcanzo su stock mínimo"
            ElseIf product.MaximumStock < quantityPhysicalByProduct Then
                result.StateResult = False
                result.Message = "El Producto " & product.Code & " - " & product.Name & " alcanzo su stock máximo"
            End If
        Else
            Dim quantityPhysicalByProductAndWarehouse = _physicalInventory.GetQuantityByProductWarehouse(productId, warehouseId)
            If quantity <> 0 Then
                If movement = InventoryStaticServices.MovementType.Input Then
                    quantityPhysicalByProductAndWarehouse = quantityPhysicalByProductAndWarehouse + quantity
                ElseIf movement = InventoryStaticServices.MovementType.OutPut Then
                    quantityPhysicalByProductAndWarehouse = quantityPhysicalByProductAndWarehouse - quantity
                End If
            End If

            Dim warehouseStock = _warehouseStockRepository.GetWarehouseStockByWarehouseIdAndProductId(warehouseId, productId)
            Dim warehouse = _wareHouseRepository.GetWarehouseById(warehouseId)
            If warehouseStock IsNot Nothing AndAlso warehouseStock.Id <> 0 Then
                If (warehouseStock.MinimumStock > quantityPhysicalByProductAndWarehouse) Then
                    result.StateResult = False
                    result.Message = "El Producto " & product.Code & " - " & product.Name & " en el almacen " & warehouse.Code & " - " & warehouse.Name & " alcanzo su stock mínimo"
                ElseIf (warehouseStock.MaximumStock < quantityPhysicalByProductAndWarehouse) Then
                    result.StateResult = False
                    result.Message = "El Producto " & product.Code & " - " & product.Name & " en el almacen " & warehouse.Code & " - " & warehouse.Name & " alcanzo su stock máximo"
                End If
            End If
        End If

        Return result
    End Function

#End Region

#End Region

#Region "TransferOrder"

    ''' <summary>
    ''' Llena el objeto de comprobante contable para la confirmación de la orden de traslado
    ''' </summary>
    ''' <param name="transferOrder"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadJournalVoucherTransferOrder(transferOrder As Domain.Entities.TransferOrder) As ActionResult(Of JournalVouchers) Implements IInventoryService.LoadJournalVoucherTransferOrder
        Try
            Dim errorList As List(Of String) = New List(Of String)()
            Dim setting As Domain.Entities.SettingInventory = _settingInventoryrepository.GetSettingInventoryByOperatingUnitId(transferOrder.OperatingUnitId)
            Dim journalVoucher As New JournalVouchers()
            Dim journalVoucherDetailCR As JournalVoucherDetails
            Dim journalVoucherDetailDB As JournalVoucherDetails
            Dim product As Domain.Entities.InventoryProduct
            'Dim productGroup As Domain.Entities.ProductGroup
            Dim mainAccount As MainAccounts
            Dim concepMovement As AdjustmentConcept
            Dim accountPayableConcept As AccountPayableConcepts
            Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit

            If setting Is Nothing OrElse setting.Id = 0 Then
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .MessageResult = {ResourceManager.GetString("SettingParameter", "Inventory")}.ToList()}
            End If

            With journalVoucher
                .IdJournalVoucher = setting.OrderDispatchJournalVoucherTypeId 'PENDIENTE
                .VoucherDate = transferOrder.DocumentDate
                .Status = 2
                .Detail = transferOrder.Description
                .EntityCode = transferOrder.Code
                .EntityId = transferOrder.Id
                .EntityName = GetType(Domain.Entities.TransferOrder).Name
                .IsClosedYear = False
                .CodeNameJournalVoucherType = setting.OrderDispatchJournalVoucherTypeDescription

                Dim dictionaryMainAccount As Dictionary(Of Integer, MainAccounts) = New Dictionary(Of Integer, MainAccounts)
                For Each transferOrderDetail In transferOrder.TransferOrderDetail
                    product = _inventoryProductRepository.GetInventoryProductByIdSimple(transferOrderDetail.ProductId, False)
                    'productGroup = _productGroupsRepository.GetProductGroupById(product.ProductGroupId)

                    '****Detalle Acreditado*******
                    If Not dictionaryMainAccount.ContainsKey(product.ProductGroup.InventoryAccountPayableConceptId) Then
                        accountPayableConcept = _accountPayableConceptRepository.GetPaymentConceptById(product.ProductGroup.InventoryAccountPayableConceptId, False)
                        Dim mainAccountTemp = _mainAccountsRepository.GetAccountById(accountPayableConcept.IdAccount, False) 'CAMBIAR PORQUE ES EL CONCEPTO
                        dictionaryMainAccount.Add(product.ProductGroup.InventoryAccountPayableConceptId, mainAccountTemp)
                    End If
                    mainAccount = dictionaryMainAccount(product.ProductGroup.InventoryAccountPayableConceptId)
                    journalVoucherDetailCR = New JournalVoucherDetails()
                    With journalVoucherDetailCR
                        .IdMainAccount = mainAccount.Id
                        If mainAccount.HandlesThirdParty Then
                            'si tipo de orden de traslado es igual a "Consumo" obtenemos el tercero de la cabecera de orden de traslado
                            If transferOrder.OrderType = 1 Then
                                .IdThirdParty = setting.TransferOrderThirdPartyId

                                'si tipo de orden de traslado es igual a "Consumo" obtenemos el tercero de la cabecera de orden de traslado
                            Else
                                If setting.TakeTransferOrderThirdParty = 1 Then 'tercero del documento
                                    .IdThirdParty = transferOrder.ThirdPartyId
                                Else 'tercero del parametro
                                    .IdThirdParty = setting.TransferOrderThirdPartyId
                                End If
                            End If
                        Else
                            .IdThirdParty = Nothing
                        End If
                        If mainAccount.HandlesCostCenter Then
                            Dim wareHouse As Warehouse = _wareHouseRepository.GetWarehouseById(transferOrder.SourceWarehouseId)
                            .IdCostCenter = wareHouse.CostCenterId
                        Else
                            .IdCostCenter = Nothing
                        End If
                        .CreditValue = Infrastructure.CrossCutting.Base.Utils.RoundValue((product.ProductCost * transferOrderDetail.Quantity), Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit)
                        .DebitValue = 0
                        .Detail = ""
                        .IdRetention = Nothing
                        .RetentionRate = Nothing
                    End With
                    .JournalVoucherDetails.Add(journalVoucherDetailCR)


                    '*******Detalle debitado (Costo)
                    journalVoucherDetailDB = New JournalVoucherDetails()
                    If transferOrder.OrderType = 2 Then
                        concepMovement = _adjusmentConceptRepository.GetAdjustmentConceptById(transferOrder.AdjustmentConceptId)
                        mainAccount = _mainAccountsRepository.GetAccountById(concepMovement.AdjustmentAccountId, False)
                    End If
                    With journalVoucherDetailDB
                        .IdMainAccount = mainAccount.Id
                        If mainAccount.HandlesThirdParty Then
                            'si tipo de orden de traslado es igual a "Consumo" obtenemos el tercero de la cabecera de orden de traslado
                            If transferOrder.OrderType = 1 Then
                                .IdThirdParty = setting.TransferOrderThirdPartyId

                                'si tipo de orden de traslado es igual a "Consumo" obtenemos el tercero de la cabecera de orden de traslado
                            Else
                                If setting.TakeTransferOrderThirdParty = 1 Then 'tercero del documento
                                    .IdThirdParty = transferOrder.ThirdPartyId
                                Else 'tercero del parametro
                                    .IdThirdParty = setting.TransferOrderThirdPartyId
                                End If

                            End If
                        Else
                            .IdThirdParty = Nothing
                        End If
                        If mainAccount.HandlesCostCenter Then
                            'si tipo de orden de traslado es "Traslado" o "Consumo" y despacha a alamacen obtenemos el centro de costo del alamcen de destino
                            If transferOrder.OrderType = 1 OrElse transferOrder.OrderType = 2 AndAlso transferOrder.DispatchTo = 1 Then
                                Dim wareHouse As Warehouse = _wareHouseRepository.GetWarehouseById(transferOrder.TargetWarehouseId)
                                .IdCostCenter = wareHouse.CostCenterId

                                'si tipo de orden de traslado es "Consumo" y despachamos a una unidad funcional obtenemos el centro de costo de la unidad funcional
                            ElseIf transferOrder.OrderType = 2 AndAlso transferOrder.DispatchTo = 2 Then
                                functionalUnit = _functionaUnitRepository.GetFunctionalUnitById(transferOrder.TargetFunctionalUnitId)
                                .IdCostCenter = functionalUnit.CostCenterId
                            End If
                        Else
                            .IdCostCenter = Nothing
                        End If
                        .CreditValue = 0
                        .DebitValue = Infrastructure.CrossCutting.Base.Utils.RoundValue((product.ProductCost * transferOrderDetail.Quantity), Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit)
                        .Detail = ""
                        .IdRetention = Nothing
                        .RetentionRate = Nothing
                    End With
                    .JournalVoucherDetails.Add(journalVoucherDetailDB)
                Next
            End With

            If errorList.Count > 0 Then
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = String.Join(vbCrLf, errorList.Distinct())}
            End If
            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = journalVoucher}
        Catch ex As Exception
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#End Region

#Region "TransferOrderDevolution"

    ''' <summary>
    ''' Llena el objeto de comprobante contable para la confirmación de la orden de traslado
    ''' </summary>
    ''' <param name="transferOrderDevolution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadJournalVoucherTransferOrderDevolution(transferOrderDevolution As Domain.Entities.TransferOrderDevolution) As ActionResult(Of JournalVouchers) Implements IInventoryService.LoadJournalVoucherTransferOrderDevolution
        Try
            Dim errorList As List(Of String) = New List(Of String)()
            Dim setting As Domain.Entities.SettingInventory = _settingInventoryrepository.GetSettingInventoryByOperatingUnitId(transferOrderDevolution.OperatingUnitId)
            Dim journalVoucher As New JournalVouchers()
            Dim journalVoucherDetailCR As JournalVoucherDetails
            Dim journalVoucherDetailDB As JournalVoucherDetails
            Dim product As Domain.Entities.InventoryProduct
            'Dim productGroup As Domain.Entities.ProductGroup
            Dim mainAccount As MainAccounts
            Dim concepMovement As AdjustmentConcept
            Dim accountPayableConcept As AccountPayableConcepts
            Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit
            Dim transferOrderDetail As TransferOrderDetail
            Dim transferOrderDetailBatchSerial As TransferOrderDetailBatchSerial
            Dim transferOrder As TransferOrder

            If setting Is Nothing OrElse setting.Id = 0 Then
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .MessageResult = {ResourceManager.GetString("SettingParameter", "Inventory")}.ToList()}
            End If

            With journalVoucher
                .IdJournalVoucher = setting.OrderDispatchReturnJournalVoucherTypeId 'PENDIENTE
                .VoucherDate = transferOrderDevolution.DocumentDate
                .Status = 2
                .Detail = ""
                .EntityCode = transferOrderDevolution.Code
                .EntityId = transferOrderDevolution.Id
                .EntityName = GetType(Domain.Entities.TransferOrderDevolution).Name
                .IsClosedYear = False
                .CodeNameJournalVoucherType = setting.OrderDispatchJournalVoucherTypeDescription

                transferOrder = _transferOrderRepository.GetTransferOrderById(transferOrderDevolution.TransferOrderId)
                Dim dictionaryMainAccount As Dictionary(Of Integer, MainAccounts) = New Dictionary(Of Integer, MainAccounts)

                For Each transferOrderDevolutionDetail As TransferOrderDevolutionDetail In transferOrderDevolution.TransferOrderDevolutionDetail
                    transferOrderDetailBatchSerial = _transferOrderDetailBatchSerialRepository.GetTransferOrderDetailBatchSerialById(Convert.ToInt32(transferOrderDevolutionDetail.TransferOrderDetailBatchSerialId))
                    transferOrderDetail = _transferOrderDetailRepository.GetTransferOrderDetailById(transferOrderDetailBatchSerial.TransferOrderDetailId)
                    product = _inventoryProductRepository.GetInventoryProductByIdSimple(transferOrderDetail.ProductId, False)

                    '****Detalle debitado (Costo) 
                    If Not dictionaryMainAccount.ContainsKey(product.ProductGroup.InventoryAccountPayableConceptId) Then
                        accountPayableConcept = _accountPayableConceptRepository.GetPaymentConceptById(product.ProductGroup.InventoryAccountPayableConceptId, False)
                        Dim mainAccountTemp = _mainAccountsRepository.GetAccountById(accountPayableConcept.IdAccount, False) 'CAMBIAR PORQUE ES EL CONCEPTO
                        dictionaryMainAccount.Add(product.ProductGroup.InventoryAccountPayableConceptId, mainAccountTemp)
                    End If
                    mainAccount = dictionaryMainAccount(product.ProductGroup.InventoryAccountPayableConceptId)

                    'accountPayableConcept = _accountPayableConceptRepository.GetPaymentConceptById(product.ProductGroup.InventoryAccountPayableConceptId, False)
                    'mainAccount = _mainAccountsRepository.GetAccountById(accountPayableConcept.IdAccount, False) 'CAMBIAR PORQUE ES EL CONCEPTO
                    journalVoucherDetailDB = New JournalVoucherDetails()
                    With journalVoucherDetailDB
                        .IdMainAccount = mainAccount.Id
                        If mainAccount.HandlesThirdParty Then
                            'si tipo de orden de traslado es igual a "Traslado" obtenemos el tercero de parametros de inventario
                            If transferOrder.OrderType = 1 Then
                                .IdThirdParty = setting.TransferOrderThirdPartyId

                                'si tipo de orden de traslado es igual a "Consumo" obtenemos el tercero de la cabecera de orden de traslado
                            Else
                                .IdThirdParty = transferOrder.ThirdPartyId
                            End If
                        Else
                            .IdThirdParty = Nothing
                        End If
                        If mainAccount.HandlesCostCenter Then
                            Dim wareHouse As Warehouse = _wareHouseRepository.GetWarehouseById(transferOrder.SourceWarehouseId)
                            .IdCostCenter = wareHouse.CostCenterId
                        Else
                            .IdCostCenter = Nothing
                        End If
                        .CreditValue = 0
                        .DebitValue = Infrastructure.CrossCutting.Base.Utils.RoundValue((product.ProductCost * transferOrderDevolutionDetail.Quantity), Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit)
                        .Detail = ""
                        .IdRetention = Nothing
                        .RetentionRate = Nothing
                    End With
                    .JournalVoucherDetails.Add(journalVoucherDetailDB)


                    '*******Detalle Acreditado*******
                    journalVoucherDetailCR = New JournalVoucherDetails()
                    If transferOrder.OrderType = 2 Then
                        concepMovement = _adjusmentConceptRepository.GetAdjustmentConceptById(transferOrder.AdjustmentConceptId)
                        mainAccount = _mainAccountsRepository.GetAccountById(concepMovement.AdjustmentAccountId, False)
                    End If
                    With journalVoucherDetailCR
                        .IdMainAccount = mainAccount.Id
                        If mainAccount.HandlesThirdParty Then
                            'si tipo de orden de traslado es igual a "Traslado" obtenemos el tercero de parametros de inventario
                            If transferOrder.OrderType = 1 Then
                                .IdThirdParty = setting.TransferOrderThirdPartyId

                                'si tipo de orden de traslado es igual a "Consumo" obtenemos el tercero de la cabecera de orden de traslado
                            Else
                                .IdThirdParty = transferOrder.ThirdPartyId
                            End If
                        Else
                            .IdThirdParty = Nothing
                        End If
                        If mainAccount.HandlesCostCenter Then
                            'si tipo de orden de traslado es "Traslado" o "Consumo" y despacha a alamacen obtenemos el centro de costo del alamcen de destino
                            If transferOrder.OrderType = 1 OrElse transferOrder.OrderType = 2 AndAlso transferOrder.DispatchTo = 1 Then
                                Dim wareHouse As Warehouse = _wareHouseRepository.GetWarehouseById(transferOrder.TargetWarehouseId)
                                .IdCostCenter = wareHouse.CostCenterId

                                'si tipo de orden de traslado es "Consumo" y despachamos a una unidad funcional obtenemos el centro de costo de la unidad funcional
                            ElseIf transferOrder.OrderType = 2 AndAlso transferOrder.DispatchTo = 2 Then
                                functionalUnit = _functionaUnitRepository.GetFunctionalUnitById(transferOrder.TargetFunctionalUnitId)
                                .IdCostCenter = functionalUnit.CostCenterId
                            End If
                        Else
                            .IdCostCenter = Nothing
                        End If
                        .CreditValue = Infrastructure.CrossCutting.Base.Utils.RoundValue((product.ProductCost * transferOrderDevolutionDetail.Quantity), Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit)
                        .DebitValue = 0
                        .Detail = ""
                        .IdRetention = Nothing
                        .RetentionRate = Nothing
                    End With
                    .JournalVoucherDetails.Add(journalVoucherDetailCR)
                Next
            End With

            If errorList.Count > 0 Then
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = String.Join(vbCrLf, errorList.Distinct())}
            End If
            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = journalVoucher}
        Catch ex As Exception
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#End Region

#Region "Pharmaceutica Dispensing Devolution"
    Public Function LoadJournalVoucherDevolution(pharmaceuticalDispensingDevolution As PharmaceuticalDispensingDevolution) As ActionResult(Of JournalVouchers) Implements IInventoryService.LoadJournalVoucherDevolution
        Try
            Dim errorList As New StringBuilder
            Dim setting As Domain.Entities.SettingInventory = _settingInventoryrepository.GetSettingInventory(pharmaceuticalDispensingDevolution.OperatingUnitId)
            Dim admision As Object = _admissionRepository.GetAdmissionByServiceOrder(pharmaceuticalDispensingDevolution.AdmissionNumber)
            Dim journalVoucher As New JournalVouchers()
            Dim journalVoucherDetailCR As JournalVoucherDetails
            Dim journalVoucherDetailDB As JournalVoucherDetails
            Dim journalVoucherDetailDiscount As JournalVoucherDetails
            Dim product As Domain.Entities.InventoryProduct
            Dim productGroup As Domain.Entities.ProductGroup
            Dim accountPayableConcept As AccountPayableConcepts
            Dim mainAccount As MainAccounts
            Dim thirdParty As Domain.Entities.ThirdParty
            Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit
            Dim caregroup As CareGroup

            With journalVoucher
                .IdJournalVoucher = setting.SalesReturnJournalVoucherTypeId  'PENDIENTE
                .VoucherDate = pharmaceuticalDispensingDevolution.DocumentDate
                .Status = 2
                .Detail = ""
                .EntityCode = pharmaceuticalDispensingDevolution.Code
                .EntityId = pharmaceuticalDispensingDevolution.Id
                .EntityName = GetType(Domain.Entities.PharmaceuticalDispensingDevolution).Name
                .IsClosedYear = False

                For Each pharmaceuticalDetailDevolution In pharmaceuticalDispensingDevolution.PharmaceuticalDispensingDevolutionDetail

                    Dim pharmaceuticalDetail = _pharmaceuticalDispensingDetailRepositoy.GetPharmaceuticalDispensingDetailById(pharmaceuticalDetailDevolution.PharmaceuticalDispensingDetailId)


                    product = _inventoryProductRepository.GetInventoryProductById(pharmaceuticalDetail.ProductId, False)
                    If product.ProductGroupId = 0 Then
                        errorList.AppendLine("El producto " + product.Code + " - " + product.Name + " no tiene un grupo asociado")
                        Continue For
                    End If
                    productGroup = _productGroupsRepository.GetProductGroupById(product.ProductGroupId)
                    functionalUnit = _functionaUnitRepository.GetFunctionalUnitById(pharmaceuticalDetail.FunctionalUnitId.ToString())

                    '****Detalle debitado*******
                    accountPayableConcept = _accountPayableConceptRepository.GetPaymentConceptById(productGroup.InventoryAccountPayableConceptId, False)
                    mainAccount = _mainAccountsRepository.GetAccountById(accountPayableConcept.IdAccount, False) 'CAMBIAR PORQUE ES EL CONCEPTO

                    Dim thirdPartyPharmaceuticalDispensing = 0

                    'obtengo de donde debo tomar el tercero
                    If mainAccount.HandlesThirdParty Then
                        If setting.PharmaceuticalDispensingGetThirdParty = 1 Then '1 -  Tomar Tercero del paciente
                            'Tomar el nit del paciente del ingreso
                            Dim patientNit As String = admision.PatientNit.ToString().TrimStart("0").Trim()
                            thirdParty = _thirdPartyRepository.GetThirdPartyByNit(patientNit, False)
                            If thirdParty Is Nothing OrElse thirdParty.Id = 0 Then
                                errorList.AppendLine(String.Format(ResourceManager.GetString("ThirdPatientCrystalNotHomologated"), patientNit))
                            Else
                                thirdPartyPharmaceuticalDispensing = thirdParty.Id
                            End If
                        Else '2 -  Tercero Especifico
                            thirdPartyPharmaceuticalDispensing = setting.PharmaceuticalDispensingThirdPartyId
                        End If

                    End If

                    journalVoucherDetailCR = New JournalVoucherDetails()
                    With journalVoucherDetailCR
                        .IdMainAccount = accountPayableConcept.IdAccount
                        .IdThirdParty = thirdPartyPharmaceuticalDispensing
                        If mainAccount.HandlesCostCenter Then
                            If setting.AssociateCostCenter = 1 Then
                                Dim wareHouse As Warehouse = _wareHouseRepository.GetWarehouseById(pharmaceuticalDetail.WarehouseId)
                                .IdCostCenter = wareHouse.CostCenterId
                            ElseIf setting.AssociateCostCenter = 2 Then
                                'Tomar centro Costo del Grupo del Producto
                                .IdCostCenter = productGroup.CostCenterId
                            ElseIf setting.AssociateCostCenter = 3 Then
                                'Tomar Centro costo del Almacén
                                Dim wareHouse As Warehouse = _wareHouseRepository.GetWarehouseById(pharmaceuticalDetail.WarehouseId)
                                .IdCostCenter = wareHouse.CostCenterId
                            End If
                        Else
                            .IdCostCenter = Nothing
                        End If
                        .CreditValue = 0
                        .DebitValue = Infrastructure.CrossCutting.Base.Utils.RoundValue(pharmaceuticalDetail.SalePrice * pharmaceuticalDetail.Quantity, Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit)
                        .Detail = ""
                        .IdRetention = Nothing
                        .RetentionRate = Nothing
                    End With
                    .JournalVoucherDetails.Add(journalVoucherDetailCR)


                    '*******Detalle acreditado (Costo)
                    journalVoucherDetailDB = New JournalVoucherDetails()
                    If setting.SettingInventoryFunctionalUnit.Where(Function(x) x.FunctionalUnitId = pharmaceuticalDetail.FunctionalUnitId).ToList().Count = 0 Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("MainAccountNotFoundInventorySetting"), String.Concat(functionalUnit.Code, " - ", functionalUnit.Name)))
                        'Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = String.Format("No se encontró cuenta contable para la unidad funcional {0} en los parámetos de inventario", String.Concat(functionalUnit.Code, " - ", functionalUnit.Name))}
                    Else
                        Dim IdMainAccount As Integer = setting.SettingInventoryFunctionalUnit.Where(Function(x) x.FunctionalUnitId = pharmaceuticalDetail.FunctionalUnitId).FirstOrDefault().CostAccountId
                        mainAccount = _mainAccountsRepository.GetAccountById(IdMainAccount, False)
                        With journalVoucherDetailDB
                            .IdMainAccount = IdMainAccount
                            If mainAccount.HandlesThirdParty Then
                                caregroup = _caregroupRepository.GetCareGroupById(pharmaceuticalDetail.CareGroupId)
                                Select Case caregroup.CareGroupType
                                    Case 1 'EAPB Con contrato
                                        Dim contract As Contract = _contractRepository.GetContractById(caregroup.ContractId)
                                        Dim healthAdministrator As HealthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(contract.HealthAdministratorId)
                                        .IdThirdParty = healthAdministrator.ThirdPartyId
                                    Case 2, 4 '2 - EAPB Sin Contrato, 4 - Aseguradoras
                                        'Tomar el nit de la entidad del ingreso
                                        thirdParty = _healthAdministratorRepository.GetThirdPartyHealthAdministratorById(admision.HealthAdministratorId, False)
                                        If thirdParty Is Nothing OrElse thirdParty.Id = 0 Then
                                            errorList.AppendLine(String.Format(ResourceManager.GetString("ThirdEntityCrystalNotHomologated"), admision.EntityNit))
                                        End If
                                        .IdThirdParty = thirdParty.Id
                                    Case 3 'Particulares
                                        'Tomar el nit del paciente del ingreso
                                        Dim patientNit As String = admision.PatientNit.ToString().TrimStart("0").Trim()
                                        thirdParty = _thirdPartyRepository.GetThirdPartyByNit(patientNit, False)
                                        If thirdParty Is Nothing OrElse thirdParty.Id = 0 Then
                                            errorList.AppendLine(String.Format(ResourceManager.GetString("ThirdPatientCrystalNotHomologated"), patientNit))
                                        End If
                                        .IdThirdParty = thirdParty.Id
                                End Select
                            Else
                                .IdThirdParty = Nothing
                            End If
                            If mainAccount.HandlesCostCenter Then
                                If setting.AssociateCostCenter = 1 Then
                                    .IdCostCenter = functionalUnit.CostCenterId
                                ElseIf setting.AssociateCostCenter = 2 Then
                                    'Tomar centro Costo del grupo del producto
                                    .IdCostCenter = productGroup.CostCenterId
                                ElseIf setting.AssociateCostCenter = 3 Then
                                    'Tomar Centro costo del almacen
                                    Dim wareHouse As Warehouse = _wareHouseRepository.GetWarehouseById(pharmaceuticalDetail.WarehouseId)
                                    .IdCostCenter = wareHouse.CostCenterId
                                End If
                            Else
                                .IdCostCenter = Nothing
                            End If
                            .CreditValue = Infrastructure.CrossCutting.Base.Utils.RoundValue(pharmaceuticalDetail.GrandTotalSalesPrice, Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit)
                            .DebitValue = 0
                            .Detail = ""
                            .IdRetention = Nothing
                            .RetentionRate = Nothing
                        End With
                        .JournalVoucherDetails.Add(journalVoucherDetailDB)
                    End If

                    '******Detalle Descuento*******
                    If pharmaceuticalDetail.DiscountValue > 0 Then
                        journalVoucherDetailDiscount = New JournalVoucherDetails()
                        With journalVoucherDetailDiscount
                            .IdMainAccount = setting.DiscountSalesMainAccountId 'CUENTA DE LOS PARÁMETROS DE INVENTARIO PARA DESCUENTOS
                            mainAccount = _mainAccountsRepository.GetAccountById(.IdMainAccount, False)

                            If mainAccount.HandlesThirdParty Then
                                caregroup = _caregroupRepository.GetCareGroupById(pharmaceuticalDetail.CareGroupId)
                                Select Case caregroup.CareGroupType
                                    Case 1 'EAPB Con contrato
                                        Dim contract As Contract = _contractRepository.GetContractById(caregroup.ContractId)
                                        Dim healthAdministrator As HealthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(contract.HealthAdministratorId)
                                        .IdThirdParty = healthAdministrator.ThirdPartyId
                                    Case 2, 4 '2 - EAPB Sin Contrato, 4 - Aseguradoras
                                        'Tomar el nit de la entidad del ingreso
                                        thirdParty = _healthAdministratorRepository.GetThirdPartyHealthAdministratorById(admision.HealthAdministratorId, False)
                                        If thirdParty Is Nothing OrElse thirdParty.Id = 0 Then
                                            errorList.AppendLine(String.Format(ResourceManager.GetString("ThirdEntityCrystalNotHomologated"), admision.EntityNit))
                                        End If
                                    Case 3 'Particulares
                                        'Tomar el nit del paciente del ingreso
                                        Dim patientNit As String = admision.PatientNit.ToString().TrimStart("0").Trim()
                                        thirdParty = _thirdPartyRepository.GetThirdPartyByNit(patientNit, False)
                                        If thirdParty Is Nothing OrElse thirdParty.Id = 0 Then
                                            errorList.AppendLine(String.Format(ResourceManager.GetString("ThirdPatientCrystalNotHomologated"), patientNit))
                                        End If
                                End Select
                            Else
                                .IdThirdParty = Nothing
                            End If
                            If mainAccount.HandlesCostCenter Then
                                If setting.AssociateCostCenter = 1 Then
                                    .IdCostCenter = functionalUnit.CostCenterId
                                ElseIf setting.AssociateCostCenter = 2 Then
                                    'Tomar centro Costo del grupo del producto
                                    .IdCostCenter = productGroup.CostCenterId
                                ElseIf setting.AssociateCostCenter = 3 Then
                                    'Tomar Centro costo del almacen
                                    Dim wareHouse As Warehouse = _wareHouseRepository.GetWarehouseById(pharmaceuticalDetail.WarehouseId)
                                    .IdCostCenter = wareHouse.CostCenterId
                                End If
                            Else
                                .IdCostCenter = Nothing
                            End If
                            .CreditValue = Infrastructure.CrossCutting.Base.Utils.RoundValue(pharmaceuticalDetail.DiscountValue * pharmaceuticalDetailDevolution.Quantity, Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit)
                            .DebitValue = 0
                            .Detail = ""
                            .IdRetention = Nothing
                            .RetentionRate = Nothing
                        End With
                        .JournalVoucherDetails.Add(journalVoucherDetailDiscount)
                    End If
                Next
            End With
            If errorList.Length > 0 Then
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = errorList.ToString()}
            End If
            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = journalVoucher}
        Catch ex As Exception
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function


    Public Function ValidatePharmaceuticalDispensingDevolutionDetail(pharmaceuticalDispensingDevolutionDetail As PharmaceuticalDispensingDevolutionDetail) As ActionResult Implements IInventoryService.ValidatePharmaceuticalDispensingDevolutionDetail
        If pharmaceuticalDispensingDevolutionDetail.CodePharmaceuticalDispensing Is Nothing Then
            pharmaceuticalDispensingDevolutionDetail.CodePharmaceuticalDispensing = _pharmaceuticalDispensingDetailRepositoy.GetPharmaceuticalDispensingDetailAndHeadById(pharmaceuticalDispensingDevolutionDetail.PharmaceuticalDispensingDetailId, False).PharmaceuticalDispensing.Code
        End If
        Dim serviceOrder = _serviceOrderRepository.GetServiceOrderByEntityCode(pharmaceuticalDispensingDevolutionDetail.CodePharmaceuticalDispensing)

        If serviceOrder.Status = 3 Then
            Return New ActionResult With {.StateResult = False, .Message = "No se puede hacer la devolución porque la orden de servicio " + serviceOrder.Code + " esta anulada"}
        End If


        Dim quantityDevolution = pharmaceuticalDispensingDevolutionDetail.Quantity
        For Each itemServiceOrderDetail In serviceOrder.ServiceOrderDetail.Where(Function(x) x.ProductId = pharmaceuticalDispensingDevolutionDetail.ProductId).ToList()
            'Ya no es necesario validar que este empaquetado ya que se analizo con jose y si se podria dar esta opcion
            ''valido que el producto no este empaquetado
            'If itemServiceOrderDetail.Packaging Then
            '    Return New ActionResult With {.StateResult = False, .Message = "El producto " + pharmaceuticalDispensingDevolutionDetail.CodeNameProduct + " se encuentra empaquetado"}
            'End If
            'If itemServiceOrderDetail.InvoicedQuantity < pharmaceuticalDispensingDevolutionDetail.Quantity Then
            '    Return New ActionResult With {.StateResult = False, .Message = "La cantidad en la orden de servicio del producto " + pharmaceuticalDispensingDevolutionDetail.CodeNameProduct + " es menor a la cantidad a devolver"}
            'End If

            'valido que el item no este distribuido
            Dim result = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByServideOrderDetailId(itemServiceOrderDetail.Id)
            If result Is Nothing OrElse result.Count = 0 Then
                Continue For
            End If
            Dim serviceOrderDetailDistribution = result.ElementAt(0)
            If serviceOrderDetailDistribution.DistributionType = 2 Then
                'Return New ActionResult With {.StateResult = False, .Message = "El producto " + pharmaceuticalDispensingDevolutionDetail.CodeNameProduct + " se encuentra distribuido"}
                'Realizo un next ya que este producto no se puede tener en cuente
                Continue For
            End If
            'valido que el producto no este facturado o bloqueado
            Dim revenueControlDetail = _revenueControlDetailRepository.GetRevenueControlDetailById(serviceOrderDetailDistribution.RevenueControlDetailId)
            If revenueControlDetail.Status > 1 Then
                If revenueControlDetail.Status = 2 Then
                    'Return New ActionResult With {.StateResult = False, .Message = "El producto " + pharmaceuticalDispensingDevolutionDetail.CodeNameProduct + " se encuentra facturado"}
                    Continue For
                Else
                    'Return New ActionResult With {.StateResult = False, .Message = "El producto " + pharmaceuticalDispensingDevolutionDetail.CodeNameProduct + " se encuentra bloqueado"}
                    Continue For
                End If
            End If
            If itemServiceOrderDetail.InvoicedQuantity > 0 And itemServiceOrderDetail.InvoicedQuantity < quantityDevolution Then
                quantityDevolution -= itemServiceOrderDetail.InvoicedQuantity
            ElseIf itemServiceOrderDetail.InvoicedQuantity > 0 Then
                quantityDevolution = 0
                Exit For
            End If
        Next
        If quantityDevolution > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = "La cantidad en la orden de servicio del producto " + pharmaceuticalDispensingDevolutionDetail.CodeNameProduct + " es menor a la cantidad a devolver, recuerde que los productos que se encuentran distribuidos, facturados o bloqueados no se pueden devolver"}
        End If
        Return New ActionResult With {.StateResult = True}
    End Function
#End Region
#Region "Product In Transit"
    Public Function SetCopyPasteOrImportFileProductInTransit(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of ProductInTransitDetail)) Implements IInventoryService.SetCopyPasteOrImportFileProductInTransit
        If dataImportFile IsNot Nothing Then
            Return SetImportFileProductInTransit(dataImportFile)
        Else
            Return SetCopyPasteProductInTransit(dataCopyPaste)
        End If
    End Function

    Private Function SetImportFileProductInTransit(data As List(Of ImportFileRow)) As ActionResult(Of List(Of ProductInTransitDetail))
        Dim listErrors As New List(Of String)
        Dim listResult As New List(Of ProductInTransitDetail)

        For Each row In data
            Dim indexRow = row.IndexRow

            'valido los campos
            If row.Row.All(Function(x) String.IsNullOrEmpty(x)) Then
                Continue For
            End If

            If row.Row.Item(0) Is Nothing OrElse row.Row.Item(0) Is String.Empty Then
                listErrors.Add(String.Format("El código del producto del item esta vacio", (indexRow).ToString()))
                Continue For
            End If

            If row.Row.Item(1) Is Nothing OrElse row.Row.Item(1) Is String.Empty Then
                listErrors.Add(String.Format("El nombre producto del item {0} esta vacio", (indexRow).ToString()))
                Continue For
            End If

            If row.Row.Item(2) Is Nothing OrElse Not IsNumeric(row.Row.Item(2)) OrElse row.Row.Item(2) < 0 Then
                listErrors.Add(String.Format("El costo del producto del item {0} no es valido", (indexRow).ToString()))
                Continue For
            End If

            If row.Row.Item(3) Is Nothing OrElse Not IsNumeric(row.Row.Item(3)) OrElse row.Row.Item(3) < 0 Then
                listErrors.Add(String.Format("La cantidad de producto(s) del item {0} no es valido", (indexRow).ToString()))
                Continue For
            End If

            If row.Row.Item(4) IsNot Nothing AndAlso (Not IsNumeric(row.Row.Item(4)) OrElse row.Row.Item(4) < 0) Then
                listErrors.Add(String.Format("La ultima columna del item {0} no es valida", (indexRow).ToString()))
                Continue For
            End If

            Dim productInTransitDetail As New ProductInTransitDetail()
            With productInTransitDetail
                .ProductCode = row.Row.Item(0)
                .ProductName = row.Row.Item(1)
                .UnitValue = row.Row.Item(2)
                .Quantity = row.Row.Item(3)
                .SubTotalValue = Math.Round((.UnitValue * .Quantity), 2)
                .IvaPercentage = 0
                .IvaValue = 0
                .TotalValue = .SubTotalValue
                .GrossUnitValue = .UnitValue
                .DiscountPercentage = 0
                .NetDiscount = .SubTotalValue
                .CodeAlternative = row.Row.Item(4)
            End With
            listResult.Add(productInTransitDetail)
        Next

        Return New ActionResult(Of List(Of ProductInTransitDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
    End Function

    Private Function SetCopyPasteProductInTransit(data As List(Of List(Of String))) As ActionResult(Of List(Of ProductInTransitDetail))
        Dim listErrors As New List(Of String)
        Dim listResult As New List(Of ProductInTransitDetail)


        For i As Integer = 0 To data.Count - 1 Step 1


            If data.Item(i).Item(0) Is Nothing OrElse data.Item(i).Item(0) Is String.Empty Then
                listErrors.Add(String.Format("El código del producto del item {0} esta vacio", ((i + 1)).ToString()))
                Continue For
            End If

            If data.Item(i).Item(1) Is Nothing OrElse data.Item(i).Item(1) Is String.Empty Then
                listErrors.Add(String.Format("El nombre producto del item {0} esta vacio", ((i + 1)).ToString()))
                Continue For
            End If

            If data.Item(i).Item(2) Is Nothing OrElse Not IsNumeric(data.Item(i).Item(2)) Then
                listErrors.Add(String.Format("El costo del producto del item {0} no es valido", ((i + 1)).ToString()))
                Continue For
            End If

            If data.Item(i).Item(3) Is Nothing OrElse Not IsNumeric(data.Item(i).Item(3)) Then
                listErrors.Add(String.Format("La cantidad de producto(s) del item {0} no es valido", ((i + 1)).ToString()))
                Continue For
            End If

            If data?.Item(i)?.Item(4) IsNot Nothing AndAlso Not IsNumeric(data.Item(i).Item(4)) Then
                listErrors.Add(String.Format("La ultima columna del item {0} no es valida", ((i + 1)).ToString()))
                Continue For
            End If


            Dim ProductInTransitDetail As New ProductInTransitDetail()
            With ProductInTransitDetail
                .ProductCode = data.Item(i).Item(0).ToString()
                .ProductName = data.Item(i).Item(1).ToString()
                .UnitValue = CDec(data.Item(i).Item(2).ToString())
                .Quantity = Int32.Parse(data.Item(i).Item(3).ToString())
                .SubTotalValue = Math.Round((.UnitValue * .Quantity), 2)
                .IvaPercentage = 0
                .IvaValue = 0
                .TotalValue = .SubTotalValue
                .GrossUnitValue = .UnitValue
                .DiscountPercentage = 0
                .NetDiscount = .SubTotalValue
                .CodeAlternative = data?.Item(i)?.Item(4)?.ToString()
            End With
            listResult.Add(ProductInTransitDetail)
        Next

        Return New ActionResult(Of List(Of ProductInTransitDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
    End Function
#End Region

#Region "Inventory Product"

    Public Sub New(ByVal productTypeRepository As IProductTypeRepository, atcRepository As IATCRepository, productRepository As IInventoryProductRepository)
        _productTypeRepository = productTypeRepository
        _atcRepository = atcRepository
        _inventoryProductRepository = productRepository
    End Sub

    ''' <summary>
    ''' Valida el guardar de los productos
    ''' </summary>
    Public Function ValidateInventoryProductSave(ByVal product As InventoryProduct) As ActionResult Implements IInventoryService.ValidateInventoryProductSave
        Dim errorList As New StringBuilder()
        With product
            If .ProductTypeId = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tipo de producto"))
                Return New ActionResult With {.StateResult = False, .Message = errorList.ToString()}
            End If
            Dim _productType As ProductType = _productTypeRepository.GetProductTypeById(.ProductTypeId)
            If _productType IsNot Nothing AndAlso _productType.Id > 0 Then

                If String.IsNullOrEmpty(.Name) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Nombre"))
                End If
                If .PackagingUnitId = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Unidad de Empaque"))
                End If

                If _productType.Class = eProductTypeClass.Grupo Then
                    If errorList.Length > 0 Then
                        Return New ActionResult With {.StateResult = False, .Message = errorList.ToString()}
                    Else
                        Return New ActionResult With {.StateResult = True}
                    End If
                End If

                If String.IsNullOrEmpty(.Presentation) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Presentación"))
                End If
                If String.IsNullOrEmpty(.CodeCUM) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Código CUM"))
                End If
                If String.IsNullOrEmpty(.CodeAlternative) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Código Alternativo"))
                End If
                If String.IsNullOrEmpty(.CodeAlternativeTwo) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Código Alternativo dos"))
                End If
                If String.IsNullOrEmpty(.Description) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Descripción"))
                End If
                If .ProductGroupId = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Grupo de Producto"))
                End If
                If .ProductSubGroupId = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "SubGrupo de Producto"))
                End If
                If .ManufacturerId Is Nothing OrElse .ManufacturerId = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fabricante"))
                End If
                If .IVAId Is Nothing OrElse .IVAId = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Código Iva"))
                End If
                If String.IsNullOrEmpty(.CodeSICE) Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Código SICE"))
                End If
                If .HandlesSerial Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Maneja Serial"))
                End If
                If .HandlesHealthRegistration Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Maneja registro sanitario"))
                ElseIf .HandlesHealthRegistration Then
                    If String.IsNullOrEmpty(.HealthRegistration) Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Registro sanitario"))
                    End If
                    If .ExpirationDate Is Nothing Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha de vencimiento"))
                    End If
                End If
                If .BillingGroupId Is Nothing OrElse .BillingGroupId = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Grupo de Facturación"))
                End If
                If .ProductControl Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Control del Producto"))
                End If
                If .ProductWithPriceControl Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Producto con control de precio"))
                End If
                If .POSProduct Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "El producto esta en el POS"))
                End If
                If .AuthorizationByOrderNumber Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Número de autorizaciones por pedido"))
                End If
                If .ExpirationDay Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Días de expiración"))
                End If
                If .MaximumControlPeriod Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Control máximo por periodo"))
                ElseIf .MaximumControlPeriod = True Then
                    If .ControlDays Is Nothing Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Días de control"))
                    End If
                End If
                If .ControlOrderQuantity Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Control de cantidad por órden"))
                ElseIf .ControlOrderQuantity = True Then
                    If .ProductOrderAmount Is Nothing Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cantidad de producto por órden"))
                    End If
                End If
                If .LastPurchase Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Última compra del producto"))
                End If
                If .LastSale Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Última venta del producto"))
                End If
                If .ProductOrigin Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Origen del producto"))
                End If
                If .MinimumStock Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Stock minimo"))
                End If
                If .MaximumStock Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Stock máximo"))
                End If
                If .CommissionPercentage Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Porcentaje de comisión"))
                End If
                If .RepositionPoint Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Punto de reposición"))
                End If
                If .ResetTime Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tiempo de reposición"))
                End If
                If .CurrencyType Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tipo de moneda"))
                End If
                If .ProductCost = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Costo del producto"))
                End If
                If .FinalProductCost Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Último costo del producto"))
                End If
                If .SellingPrice Is Nothing Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Precio de venta"))
                End If
                If .POSProduct = True Then
                    If .AllPOSPathologies Is Nothing Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Aplica a todas las patologías POS"))
                    End If
                End If
                If .Status = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Estado"))
                End If

                Select Case _productType.Class
                    Case eProductTypeClass.ItemMedicamento
                        If .ATCId Is Nothing OrElse .ATCId = 0 Then
                            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "ATC"))
                        End If
                End Select

            Else
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tipo de Producto"))
            End If

        End With

        If errorList.Length > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = errorList.ToString()}
        Else
            Return New ActionResult With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' valida que un producto no se pueda inactivar si esté tiene cantidades > 0 en el inventario físico
    ''' </summary>
    ''' <param name="Product"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateQuantityPhysicalInventory(ByVal Product As InventoryProduct) As ActionResult Implements IInventoryService.ValidateQuantityPhysicalInventory
        Dim a As List(Of PhysicalInventory) = _physicalInventory.GetListPhysicalInventoryByProduct(Product.Id)
        If a.Any(Function(x) x.Quantity > 0) Then
            Return New ActionResult With {.StateResult = False, .Message = "El producto no se puede inactivar porque tiene cantidades en el inventario físico"}
        Else
            Return New ActionResult With {.StateResult = True}
        End If
    End Function

    ''' <summary>
    ''' Lista la unidad de medida del producto segun la clase y el tipo de formulacion del ATC
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Function GetMeasurementUnitByProductId(productId As Integer) As List(Of Tuple(Of Integer, Decimal)) Implements IInventoryService.GetMeasurementUnitByProductId
        Dim listProduct As List(Of Tuple(Of Integer, Decimal)) = Nothing
        Dim product = _inventoryProductRepository.FirstOrDefault(Function(m) m.Id = productId, True, {"ProductType"})

        Dim measurementUnitId = product.MeasurementUnitId
        Dim WeighVolume As Decimal? = 1
        listProduct = New List(Of Tuple(Of Integer, Decimal))

        If product.ProductType.Class = 2 Then
            Dim atc = _atcRepository.FirstOrDefault(Function(m) m.Id = product.ATCId.Value, True)

            If atc.FormulationType = 2 Then
                measurementUnitId = atc.VolumeMeasureUnit
                WeighVolume = atc.Volume
            ElseIf atc.FormulationType = 4 Then
                WeighVolume = atc.ConcentrationQuantity
                measurementUnitId = atc.ConcentrationMeasureUnitId
            Else
                measurementUnitId = atc.WeightMeasureUnit
                WeighVolume = atc.Weight
            End If
        End If

        If Not measurementUnitId.HasValue Then Throw New IndigoValidationException("Unidad de medida no encontrada")

        listProduct.Add(New Tuple(Of Integer, Decimal)(measurementUnitId, WeighVolume))
        Return listProduct
    End Function

    ''' <summary>
    ''' Lista la unidad de medida del producto segun la clase y el tipo de formulacion del ATC.
    ''' Para NPT (MSClass = 2), siempre usa VolumeMeasureUnit independientemente del FormulationType.
    ''' </summary>
    ''' <param name="productId">Id del producto</param>
    ''' <param name="msClass">Tipo de dosis unitaria (MSClass). Para NPT (MSClass = 2) usa VolumeMeasureUnit</param>
    ''' <returns></returns>
    Public Function GetMeasurementUnitByProductId(productId As Integer, msClass As Integer) As List(Of Tuple(Of Integer, Decimal)) Implements IInventoryService.GetMeasurementUnitByProductId
        Dim listProduct As List(Of Tuple(Of Integer, Decimal)) = Nothing
        Dim product = _inventoryProductRepository.FirstOrDefault(Function(m) m.Id = productId, True, {"ProductType"})

        Dim measurementUnitId = product.MeasurementUnitId
        Dim WeighVolume As Decimal? = 1
        listProduct = New List(Of Tuple(Of Integer, Decimal))

        If product.ProductType.Class = 2 Then
            Dim atc = _atcRepository.FirstOrDefault(Function(m) m.Id = product.ATCId.Value, True)

            ' Para NPT (MSClass = 2), siempre usar la unidad de medida del volumen
            If msClass = 2 Then
                measurementUnitId = If(atc.VolumeMeasureUnit, atc.WeightMeasureUnit)
                WeighVolume = If(atc.Volume, atc.Weight)
            ElseIf atc.FormulationType = 2 Then
                measurementUnitId = atc.VolumeMeasureUnit
                WeighVolume = atc.Volume
            ElseIf atc.FormulationType = 4 Then
                WeighVolume = atc.ConcentrationQuantity
                measurementUnitId = atc.ConcentrationMeasureUnitId
            Else
                measurementUnitId = atc.WeightMeasureUnit
                WeighVolume = atc.Weight
            End If
        End If

        If Not measurementUnitId.HasValue Then Throw New IndigoValidationException("Unidad de medida no encontrada")

        listProduct.Add(New Tuple(Of Integer, Decimal)(measurementUnitId, WeighVolume))
        Return listProduct
    End Function

    ''' <summary>
    ''' Lista la unidad de medida del producto segun la clase y el tipo de formulacion del ATC
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Function GetMeasurementUnitAndConcentrationByProductId(productId As Integer) As (measurementUnitId As Integer, concentration As Decimal) Implements IInventoryService.GetMeasurementUnitAndConcentrationByProductId
        Dim product = _inventoryProductRepository.FirstOrDefault(Function(m) m.Id = productId, True, {"ProductType"})

        Dim measurementUnitId = product.MeasurementUnitId
        Dim WeighVolume As Decimal? = 1

        If product.ProductType.Class = 2 Then
            Dim atc = _atcRepository.FirstOrDefault(Function(m) m.Id = product.ATCId.Value, True)

            If atc.FormulationType = 2 Then
                measurementUnitId = atc.VolumeMeasureUnit
                WeighVolume = atc.Volume
            ElseIf atc.FormulationType = 4 Then
                WeighVolume = atc.ConcentrationQuantity
                measurementUnitId = atc.ConcentrationMeasureUnitId
            Else
                measurementUnitId = atc.WeightMeasureUnit
                WeighVolume = atc.Weight
            End If
        End If

        Return (measurementUnitId, WeighVolume)
    End Function

    ''' <summary>
    ''' Lista la unidad de medida del producto segun la clase y el tipo de formulacion del ATC.
    ''' Para NPT (msClass = 2), siempre usa VolumeMeasureUnit.
    ''' </summary>
    ''' <param name="productIds">Lista de IDs de productos</param>
    ''' <param name="msClass">Tipo de dosis unitaria (MSClass). Para NPT (MSClass = 2) usa VolumeMeasureUnit</param>
    ''' <returns></returns>
    Public Function GetMeasurementUnitsAndConcentrationsByProductIds(productIds As List(Of Integer), Optional msClass As Integer = 0) As List(Of (ProductValidationId As Integer, measurementUnitId As Integer, concentration As Decimal)) Implements IInventoryService.GetMeasurementUnitsAndConcentrationsByProductIds
        Dim results As New List(Of (ProductValidationId As Integer, measurementUnitId As Integer, concentration As Decimal))
        Dim products = _inventoryProductRepository.Query(Function(m) productIds.Contains(m.Id), True, {"ProductType"}).ToList
        Dim atcIdValues = products.Where(Function(m) m.ATCId IsNot Nothing) _
                                  .Select(Function(m) m.ATCId.Value) _
                                  .ToList()

        Dim atcs = _atcRepository.Query(Function(m) atcIdValues.Contains(m.Id), True).ToList()

        For Each product In products
            Dim measurementUnitId = product.MeasurementUnitId
            Dim WeighVolume As Decimal? = 1

            If product.ProductType.Class = 2 AndAlso product.ATCId.HasValue Then
                Dim atc = atcs.FirstOrDefault(Function(m) m.Id = product.ATCId.Value)

                If atc IsNot Nothing Then
                    ' Para NPT (MSClass = 2), siempre usar la unidad de medida del volumen
                    If msClass = 2 Then
                        measurementUnitId = If(atc.VolumeMeasureUnit, atc.WeightMeasureUnit)
                        WeighVolume = If(atc.Volume, atc.Weight)
                    ElseIf atc.FormulationType = 2 Then
                        measurementUnitId = atc.VolumeMeasureUnit
                        WeighVolume = atc.Volume
                    ElseIf atc.FormulationType = 4 Then
                        WeighVolume = atc.ConcentrationQuantity
                        measurementUnitId = atc.ConcentrationMeasureUnitId
                    Else
                        measurementUnitId = atc.WeightMeasureUnit
                        WeighVolume = atc.Weight
                    End If
                End If
            End If

            results.Add((ProductValidationId:=product.Id, measurementUnitId:=measurementUnitId, concentration:=WeighVolume))
        Next

        Return results
    End Function

#End Region

#Region "Product Rate"
    Public Function SetCopyPasteOrImportFileProductRate(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of ProductRateDetail)) Implements IInventoryService.SetCopyPasteOrImportFileProductRate
        If dataImportFile IsNot Nothing Then
            Return SetImportFileProductRate(dataImportFile)
        Else
            Return SetCopyPaste(dataCopyPaste)
        End If
    End Function

    Private Function SetImportFileProductRate(data As List(Of ImportFileRow)) As ActionResult(Of List(Of ProductRateDetail))
        Dim listErrors As New List(Of String)
        Dim listResult As New List(Of ProductRateDetail)

        Dim CodeProducts = (From x In data Where Not String.IsNullOrEmpty(x.Row.Item(0)) Select TryCast(x.Row.Item(0).ToString(), String)).ToList()

        If Not CodeProducts.Any() Then
            listErrors.Add("La columna de código de producto esta vacía")
            Return New ActionResult(Of List(Of ProductRateDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
        End If

        Dim _products = _inventoryProductRepository.Query(Function(m) CodeProducts.Contains(m.Code), False).Select(Function(m) New With {m.Id, m.Code, m.Name, m.ProductControl, m.ProductWithPriceControl}).ToList()

        If Not _products.Any() Then
            listErrors.Add("Ninguno de los productos importar existe")
            Return New ActionResult(Of List(Of ProductRateDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
        End If

        Dim Product = New With {.Id = New Integer, .Code = "", .Name = "", .ProductControl = New Boolean?, .ProductWithPriceControl = New Boolean?}

        For Each row In data
            Dim indexRow = row.IndexRow

            'valido los campos
            If row.Row.All(Function(x) String.IsNullOrEmpty(x)) Then
                Continue For
            End If

            If row.Row.Item(0) Is Nothing OrElse row.Row.Item(0) Is String.Empty Then
                listErrors.Add(String.Format("El producto del item {0} esta vacio", (indexRow).ToString()))
                Continue For
            End If

            If Not _products.Any(Function(f) f.Code = row.Row.Item(0).ToString()) Then
                listErrors.Add(String.Format("El producto {0} No existe", row.Row.Item(0).ToString()))
                Continue For
            Else
                Product = _products.Where(Function(x) x.Code = row.Row.Item(0).ToString()).FirstOrDefault
            End If

            If row.Row.Item(1) Is Nothing OrElse Not IsNumeric(row.Row.Item(1)) OrElse Not {1, 2, 3}.Contains(row.Row.Item(1)) Then
                listErrors.Add(String.Format("El tipo de liquidacion {0} no es valido", (indexRow).ToString()))
                Continue For
            End If

            If row.Row.Item(1) <> 2 Then
                If row.Row.Item(2) Is Nothing OrElse Not IsNumeric(row.Row.Item(2)) OrElse Not {1, 2}.Contains(row.Row.Item(2)) Then
                    listErrors.Add(String.Format("El tipo de Tarifa {0} no es valido", (indexRow).ToString()))
                    Continue For
                End If
            End If

            If row.Row.Item(2) IsNot Nothing AndAlso row.Row.Item(2) = 2 Then
                If row.Row.Item(3) Is Nothing OrElse Not IsNumeric(row.Row.Item(3)) OrElse Not {1, 2}.Contains(row.Row.Item(3)) Then
                    listErrors.Add(String.Format("El tipo de Porcentaje del item {0} no es valido", (indexRow).ToString()))
                    Continue For
                End If
            End If

            If row.Row.Item(1) IsNot Nothing AndAlso row.Row.Item(1) <> 1 Then
                If row.Row.Item(4) Is Nothing Then
                    listErrors.Add("El Codigo CUPS no puede estar vacio si el tipo de liquidación incluye servicio")
                    Continue For
                End If
            End If

            If row.Row.Item(6) Is Nothing OrElse Not IsDate(row.Row.Item(6)) Then
                listErrors.Add(String.Format("La fecha inicial del item {0} no es valida", (indexRow).ToString()))
                Continue For
            End If

            If row.Row.Item(7) Is Nothing OrElse Not IsDate(row.Row.Item(7)) Then
                listErrors.Add(String.Format("La fecha final del item {0} no es valida", (indexRow).ToString()))
                Continue For
            End If

            If row.Row.Item(2) IsNot Nothing AndAlso row.Row.Item(2) = 2 Then
                If row.Row.Item(8) Is Nothing OrElse Not IsNumeric(row.Row.Item(8)) Then
                    listErrors.Add(String.Format("El Porcentaje del item {0} no es valido", (indexRow).ToString()))
                    Continue For
                End If
            End If

            If row.Row.Item(2) IsNot Nothing AndAlso row.Row.Item(2) = 1 Then
                If row.Row.Item(9) Is Nothing OrElse Not IsNumeric(row.Row.Item(9)) Then
                    listErrors.Add(String.Format("El precio de venta del item {0} no es valido", (indexRow).ToString()))
                    Continue For
                End If
                If row.Row.Item(10) Is Nothing OrElse Not IsNumeric(row.Row.Item(10)) Then
                    listErrors.Add(String.Format("El precio con recargo del item {0} no es valido", (indexRow).ToString()))
                    Continue For
                End If
            End If

            If Not IsNumeric(row.Row.Item(11)) OrElse row.Row.Item(11) < 0 OrElse row.Row.Item(11) > 1 Then
                listErrors.Add(String.Format("La asignación del campo Contratado del item {0} no es valida", (indexRow).ToString()))
                Continue For
            End If

            If Not IsNumeric(row.Row.Item(12)) OrElse row.Row.Item(12) < 0 OrElse row.Row.Item(12) > 1 Then
                listErrors.Add(String.Format("La asignación del campo Cotizado del item {0} no es valida", (indexRow).ToString()))
                Continue For
            End If

            If (row.Row.Item(0).ToString().Trim.Length > 20) Then
                listErrors.Add(String.Format("El producto {0} del item {1} exede la cantidad de caracteres permitida", row.Row.Item(0), indexRow.ToString()))
                Continue For
            End If

            Dim productCode As String = row.Row.Item(0)

            Dim LiquidationType As Byte = row.Row.Item(1)

            Dim RateType As Byte
            If LiquidationType <> 2 Then
                RateType = row.Row.Item(2)
            Else
                RateType = 0
            End If

            Dim PercentageBasedOn As Byte
            Dim Percentage As Decimal
            If RateType = 2 Then
                PercentageBasedOn = row.Row.Item(3)
                Percentage = row.Row.Item(8)
            Else
                PercentageBasedOn = 0
                Percentage = Nothing
            End If

            Dim CupsCode As String
            Dim ContracDescCode As String
            Dim CupsEntity As CUPSEntity = Nothing
            Dim ContractDescription = New CUPSEntityContractDescriptions

            If LiquidationType <> 1 Then
                CupsCode = row.Row.Item(4)
                ContracDescCode = IIf(String.IsNullOrEmpty(row.Row.Item(5)), Nothing, row.Row.Item(5))

                CupsEntity = _cupsEntityRepository.FirstOrDefault(Function(x) x.Code = CupsCode, False, {"CUPSEntityContractDescriptions"})

                If CupsEntity Is Nothing OrElse CupsEntity.Id = 0 Then
                    listErrors.Add(String.Format("El Cups del item {0} no existe", (indexRow).ToString()))
                    Continue For
                End If

                ' Valido si el cups tiene descripciones relacionadas
                If CupsEntity.CUPSEntityContractDescriptions IsNot Nothing AndAlso CupsEntity.CUPSEntityContractDescriptions.Count > 0 AndAlso String.IsNullOrEmpty(ContracDescCode) Then
                    listErrors.Add(String.Format("El CUPS tiene Descripciones relacionadas pero el campo de Descripcion esta vacio del item {0} ", (indexRow).ToString()))
                    Continue For
                End If

                If ContracDescCode IsNot Nothing Then
                    ContractDescription = _cupsEntityRepository.GetListCupsEntityContractDescription(CupsCode, ContracDescCode).FirstOrDefault
                    If ContractDescription Is Nothing OrElse ContractDescription.ContractDescriptionId = 0 Then
                        listErrors.Add(String.Format("El Descripcion relacionada No pertence al CUPS del item {0} ", (indexRow).ToString()))
                        Continue For
                    End If
                End If
            Else
                CupsCode = Nothing
                ContracDescCode = Nothing
            End If

            Dim initialDate As DateTime = row.Row.Item(6)
            Dim endDate As DateTime = row.Row.Item(7)

            endDate = endDate.AddDays(1).AddSeconds(-1)

            Dim value As Decimal
            Dim surchargeValue As Decimal
            If RateType = 1 Then
                value = row.Row.Item(9)
                surchargeValue = row.Row.Item(10)
                If value = 0 Then
                    listErrors.Add($"El precio de venta del item {(indexRow).ToString()} no puede ser 0")
                    Continue For
                End If
                If surchargeValue = 0 Then
                    listErrors.Add($"El precio con recargo del item {(indexRow).ToString()} no puede ser 0")
                    Continue For
                End If
            End If

            Dim contracted As Boolean = row.Row.Item(11)
            Dim quoted As Boolean = row.Row.Item(12)
            Dim observations As String = row.Row.Item(13)

            If initialDate > endDate Then
                listErrors.Add(String.Format("La fecha inicial del item {0} debe ser menor a la fecha final ", (indexRow).ToString()))
                Continue For
            End If

            If ValidateProductWithDates(initialDate, endDate, Product.Id, listResult) > 0 Then
                listErrors.Add(String.Format("El producto {0} ya existe en el listado con las fechas {1} y {2}", Product.Code, initialDate.ToString, endDate.ToString))
                Continue For
            End If

            Dim productRateDetail As New ProductRateDetail()
            With productRateDetail
                .RateClass = 1
                .ProductId = Product.Id
                .ProductCodeName = Product.Code & " - " & Product.Name
                .ProductCode = Product.Code
                .ProductName = Product.Name
                .ProductControl = IIf(Product.ProductControl Is Nothing, False, Product.ProductControl)
                .ProductWithPriceControl = IIf(Product.ProductWithPriceControl Is Nothing, False, Product.ProductWithPriceControl)
                .LiquidationType = LiquidationType
                .RateType = RateType
                .PercentageBasedOn = PercentageBasedOn
                .Percentage = Percentage
                If CupsEntity IsNot Nothing Then
                    .CupsId = CupsEntity.Id
                    .CupsCodeName = String.Format("{0} - {1}", CupsEntity.Code, CupsEntity.Description)
                Else
                    .CupsId = Nothing
                    .CupsCodeName = String.Empty
                End If
                If ContractDescription.ContractDescriptionId = 0 Then
                    .ContractDescriptionId = Nothing
                    .ContractDesCodeName = String.Empty
                Else
                    .ContractDescriptionId = ContractDescription.ContractDescriptionId
                    .ContractDesCodeName = $"{ContractDescription.ContractDescriptions.Code} - {ContractDescription.ContractDescriptions.Name}"
                End If
                .InitialDate = initialDate
                .EndDate = endDate
                .SalesValue = value
                .SalesValueWithSurcharge = surchargeValue
                .Contracted = contracted
                .Quoted = quoted
                .Observations = observations
                .Status = 1
            End With
            listResult.Add(productRateDetail)
        Next


        Return New ActionResult(Of List(Of ProductRateDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
    End Function

    Private Function SetCopyPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of ProductRateDetail))
        Dim listErrors As New List(Of String)
        Dim listResult As New List(Of ProductRateDetail)

        Dim CodeProducts = (From x In data Where Not String.IsNullOrEmpty(x.Item(0)) Select x.Item(0)).ToList()

        If Not CodeProducts.Any() Then
            listErrors.Add("La columna de código de producto esta vacía")
            Return New ActionResult(Of List(Of ProductRateDetail)) With {.StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
        End If

        Dim _products = _inventoryProductRepository.Query(Function(m) CodeProducts.Contains(m.Code), False).Select(Function(m) New With {m.Id, m.Code, m.Name, m.ProductControl, m.ProductWithPriceControl}).ToList()

        If Not _products.Any() Then
            listErrors.Add("Ninguno de los productos importar existe")
            Return New ActionResult(Of List(Of ProductRateDetail)) With {.StatusCode = eStatusResult.EXCEPTION, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
        End If

        Dim Product = New With {.Id = New Integer, .Code = "", .Name = "", .ProductControl = New Boolean?, .ProductWithPriceControl = New Boolean?}

        For i As Integer = 0 To data.Count - 1 Step 1
            'valido la estructura
            If data.Item(i).Count <> 14 Then
                If {"0", "1"}.Contains(data.Item(i)(data.Item(i).Count - 1).ToString) Then
                    data.Item(i).Add("")
                End If
            End If
            If data.Item(i).Count <> 14 Then
                listErrors.Add(String.Format("La estructura del item {0} es incorrecta", (i + 1).ToString()))
                Continue For
            End If
            'valido los campos
            If data.Item(i).Item(0) Is String.Empty Then
                listErrors.Add(String.Format("El producto del item {0} esta vacio", (i + 1).ToString()))
                Continue For
            End If

            If Not _products.Any(Function(f) f.Code = data.Item(i).Item(0)) Then
                listErrors.Add(String.Format("El producto {0} No existe", data.Item(i).Item(0).ToString()))
                Continue For
            Else
                Product = _products.Where(Function(x) x.Code = data.Item(i).Item(0)).FirstOrDefault
            End If

            If data.Item(i).Item(1) Is String.Empty OrElse Not IsNumeric(data.Item(i).Item(1)) OrElse Not {1, 2, 3}.Contains(data.Item(i).Item(1)) Then
                listErrors.Add(String.Format("El tipo de liquidacion del item {0} esta vacio o No tiene un formato valido", (i + 1).ToString()))
                Continue For
            End If

            If data.Item(i).Item(1) <> 2 Then
                If data.Item(i).Item(2) Is String.Empty OrElse Not IsNumeric(data.Item(i).Item(2)) OrElse Not {1, 2}.Contains(data.Item(i).Item(2)) Then
                    listErrors.Add(String.Format("El tipo de tarifa del item {0} esta vacio o No tiene un formato valido", (i + 1).ToString()))
                    Continue For
                End If
            End If

            If data.Item(i).Item(2) = 2 Then
                If data.Item(i).Item(3) Is String.Empty OrElse Not IsNumeric(data.Item(i).Item(3)) OrElse Not {1, 2}.Contains(data.Item(i).Item(3)) Then
                    listErrors.Add(String.Format("El tipo de Porcentaje del item {0} esta vacio o No tiene un formato valido", (i + 1).ToString()))
                    Continue For
                End If
            End If

            If data.Item(i).Item(1) <> 1 Then
                If data.Item(i).Item(4) Is String.Empty Then
                    listErrors.Add(String.Format("El Cups del item {0} esta vacio o No tiene un formato valido", (i + 1).ToString()))
                    Continue For
                End If
            End If

            If Not IsDate(data.Item(i).Item(6)) Then
                listErrors.Add("La fecha inicial del item " & (i + 1).ToString() & " no es valida")
                Continue For
            End If
            If Not IsDate(data.Item(i).Item(7)) Then
                listErrors.Add("La fecha final del item " & (i + 1).ToString() & " no es valida")
                Continue For
            End If

            If data.Item(i).Item(2) = 2 Then
                If data.Item(i).Item(8) Is String.Empty OrElse Not IsNumeric(data.Item(i).Item(8)) Then
                    listErrors.Add(String.Format("El Porcentaje del item {0} esta vacio o No tiene un formato valido", (i + 1).ToString()))
                    Continue For
                End If
            End If

            If data.Item(i).Item(2) = 1 Then
                If Not IsNumeric(data.Item(i).Item(9)) Then
                    listErrors.Add(String.Format("El precio de venta del item {0} no es valido", (i + 1).ToString()))
                    Continue For
                End If
                If Not IsNumeric(data.Item(i).Item(10)) Then
                    listErrors.Add(String.Format("El precio con recargo del item {0} no es valido", (i + 1).ToString()))
                    Continue For
                End If
            End If

            If Not IsNumeric(data.Item(i).Item(11)) OrElse data.Item(i).Item(11) < 0 OrElse data.Item(i).Item(11) > 1 Then
                listErrors.Add(String.Format("La asignación del campo Contratado del item {0} no es valida", (i + 1).ToString()))
                Continue For
            End If
            If Not IsNumeric(data.Item(i).Item(12)) OrElse data.Item(i).Item(12) < 0 OrElse data.Item(i).Item(12) > 1 Then
                listErrors.Add(String.Format("La asignación del campo Al cotizar del item {0} no es valida", (i + 1).ToString()))
                Continue For
            End If

            Dim productCode As String = data.Item(i).Item(0)
            Dim LiquidationType As Byte = data.Item(i).Item(1)

            Dim RateType As Byte
            If LiquidationType <> 2 Then
                RateType = data.Item(i).Item(2)
            Else
                RateType = 0
            End If

            Dim PercentageBasedOn As Byte
            Dim Percentage As Decimal?
            If RateType = 2 Then
                PercentageBasedOn = data.Item(i).Item(3)
                Percentage = data.Item(i).Item(8)
            Else
                PercentageBasedOn = 0
                Percentage = Nothing
            End If

            Dim CupsCode As String
            Dim ContracDescCode As String
            Dim CupsEntity As CUPSEntity = Nothing
            Dim ContractDescription = New CUPSEntityContractDescriptions

            If LiquidationType <> 1 Then
                CupsCode = data.Item(i).Item(4)
                ContracDescCode = IIf(data.Item(i).Item(5) Is String.Empty, Nothing, data.Item(i).Item(5))

                CupsEntity = _cupsEntityRepository.FirstOrDefault(Function(x) x.Code = CupsCode, False, {"CUPSEntityContractDescriptions"})

                If CupsEntity Is Nothing OrElse CupsEntity.Id = 0 Then
                    listErrors.Add(String.Format("El Cups del item {0} no existe", (i + 1).ToString()))
                    Continue For
                End If

                ' Valido si el cups tiene descripciones relacionadas
                If CupsEntity.CUPSEntityContractDescriptions IsNot Nothing AndAlso CupsEntity.CUPSEntityContractDescriptions.Count > 0 AndAlso String.IsNullOrEmpty(ContracDescCode) Then
                    listErrors.Add(String.Format("El CUPS tiene Descripciones relacionadas pero el campo de Descripcion esta vacio del item {0} ", (i + 1).ToString()))
                    Continue For
                End If

                If ContracDescCode IsNot Nothing Then
                    ContractDescription = _cupsEntityRepository.GetListCupsEntityContractDescription(CupsCode, ContracDescCode).FirstOrDefault
                    If ContractDescription Is Nothing OrElse ContractDescription.ContractDescriptionId = 0 Then
                        listErrors.Add(String.Format("El Descripcion relacionada No pertence al CUPS del item {0} ", (i + 1).ToString()))
                        Continue For
                    End If
                End If
            Else
                CupsCode = Nothing
                ContracDescCode = Nothing
            End If

            Dim initialDate As DateTime = data.Item(i).Item(6)
            Dim endDate As DateTime = data.Item(i).Item(7)

            Dim value As Decimal
            Dim surchargeValue As Decimal
            If RateType = 1 Then
                value = data.Item(i).Item(9)
                surchargeValue = data.Item(i).Item(10)
                If value = 0 Then
                    listErrors.Add($"El precio de venta del item {(i + 1).ToString()} no puede ser 0")
                    Continue For
                End If
                If surchargeValue = 0 Then
                    listErrors.Add($"El precio con recargo del item {(i + 1).ToString()} no puede ser 0")
                    Continue For
                End If
            End If

            Dim contracted As Boolean = data.Item(i).Item(11)
            Dim quoted As Boolean = data.Item(i).Item(12)
            Dim observations As String = data.Item(i).Item(13)

            If initialDate > endDate Then
                listErrors.Add(String.Format("La fecha inicial del item {0} debe ser menor a la fecha final ", (i + 1).ToString()))
                Continue For
            End If

            If ValidateProductWithDates(initialDate, endDate, Product.Id, listResult) > 0 Then
                listErrors.Add($"El producto {Product.Code} ya existe en el listado con las fechas {initialDate.ToString} y {endDate.ToString}")
                Continue For
            End If

            Dim productRateDetail As New ProductRateDetail()
            With productRateDetail
                .RateClass = 1
                .ProductId = Product.Id
                .ProductCodeName = $"{Product.Code} - {Product.Name}"
                .ProductCode = Product.Code
                .ProductName = Product.Name
                .ProductControl = IIf(Product.ProductControl Is Nothing, False, Product.ProductControl)
                .ProductWithPriceControl = IIf(Product.ProductWithPriceControl Is Nothing, False, Product.ProductWithPriceControl)
                .LiquidationType = LiquidationType
                .RateType = RateType
                .PercentageBasedOn = PercentageBasedOn
                .Percentage = Percentage
                If CupsEntity IsNot Nothing Then
                    .CupsId = CupsEntity.Id
                    .CupsCodeName = String.Format("{0} - {1}", CupsEntity.Code, CupsEntity.Description)
                Else
                    .CupsId = Nothing
                    .CupsCodeName = String.Empty
                End If
                If ContractDescription.ContractDescriptionId = 0 Then
                    .ContractDescriptionId = Nothing
                    .ContractDesCodeName = String.Empty
                Else
                    .ContractDescriptionId = ContractDescription.ContractDescriptionId
                    .ContractDesCodeName = $"{ContractDescription.ContractDescriptions.Code} - {ContractDescription.ContractDescriptions.Name}"
                End If
                .InitialDate = initialDate
                .EndDate = endDate
                .SalesValue = value
                .SalesValueWithSurcharge = surchargeValue
                .Contracted = contracted
                .Quoted = quoted
                .Observations = observations
                .Status = 1
            End With
            listResult.Add(productRateDetail)
        Next

        Return New ActionResult(Of List(Of ProductRateDetail)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
    End Function

    Private Function ValidateProductWithDates(_initialDate As DateTime, _endDate As DateTime, _productId As Integer, listCompare As List(Of ProductRateDetail)) As Integer
        Dim list As List(Of ProductRateDetail) = listCompare.FindAll(Function(item) ((_initialDate >= item.InitialDate AndAlso _initialDate <= item.EndDate) OrElse (_endDate >= item.InitialDate AndAlso _endDate <= item.EndDate) OrElse (_initialDate < item.InitialDate) AndAlso (_endDate > item.EndDate)) AndAlso (_productId = item.ProductId))
        Return list.Count
    End Function
#End Region

#Region "Inventory Control"
    ''' <summary>
    ''' mnetodo para validar los items pegados en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="warehouseId"></param>
    ''' <param name="controlType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetProductsInventoryControlCopyPaste(data As List(Of List(Of String)), warehouseId As Integer, controlType As Integer, audit As AuditMessage) As ActionResult(Of List(Of InventoryControlDetail)) Implements IInventoryService.SetProductsInventoryControlCopyPaste
        Return Me.SetCopyPasteOrImportInventoryControlDetails(Nothing, data, warehouseId, controlType, audit)
    End Function

    ''' <summary>
    ''' metodo para validar los items del archivo de excel que se esta importando
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetProductsInventoryControlImportFile(data As List(Of ImportFileRow), warehouseId As Integer, controlType As Integer, audit As AuditMessage, documentDate As Date, operatingUnitId As Integer) As ActionResult(Of List(Of InventoryControlDetail)) Implements IInventoryService.SetProductsInventoryControlImportFile
        Return Me.SetCopyPasteOrImportInventoryControlDetails(data, Nothing, warehouseId, controlType, audit)
    End Function

    Private Function SetCopyPasteOrImportInventoryControlDetails(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), warehouseId As Integer, controlType As Integer, audit As AuditMessage) As ActionResult(Of List(Of InventoryControlDetail))
        If Not (dataImportFile IsNot Nothing AndAlso dataImportFile.Count > 0) AndAlso Not (dataCopyPaste IsNot Nothing AndAlso dataCopyPaste.Count > 0) Then
            Throw New ArgumentNullException("data")
        End If

        Try
            Dim listErrors As New List(Of String)
            Dim listInventoryControlDetail As New List(Of InventoryControlDetail)
            Dim xmlObject As String = String.Empty
            If dataImportFile IsNot Nothing Then
                xmlObject = ConvertToXmlImportInventoryControlDetail(dataImportFile)
            Else
                xmlObject = ConvertToXmlCopyPasteInventoryControlDetail(dataCopyPaste)
            End If
            Dim ds = Me.SP_CopyAndPasteInventoryControlDetail(warehouseId, xmlObject, audit.CodeUser)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    If CInt(row("StatusField")) = 0 Then
                        Dim inventoryControlDetail = listInventoryControlDetail.Where(Function(icd) icd.ProductId = CInt(row("ProductId"))).FirstOrDefault()
                        If inventoryControlDetail Is Nothing Then
                            inventoryControlDetail = New InventoryControlDetail
                            inventoryControlDetail.ProductId = CInt(row("ProductId"))
                            inventoryControlDetail.ProductCodeName = String.Format("{0} - {1}", row("ProductCode").ToString(), row("ProductName").ToString())
                            inventoryControlDetail.Quantity = CInt(row("Quantity"))
                            listInventoryControlDetail.Add(inventoryControlDetail)
                        Else
                            inventoryControlDetail.Quantity += CInt(row("Quantity"))
                        End If

                        Dim batchSerialId As Integer? = If(IsDBNull(row("BatchSerialId")), Nothing, row("BatchSerialId"))
                        Dim inventoryControlDetailBatchSerial = inventoryControlDetail.InventoryControlDetailBatchSerial.Where(Function(icdbs) icdbs.BatchSerialId.Equals(batchSerialId)).FirstOrDefault()
                        If inventoryControlDetailBatchSerial Is Nothing Then
                            inventoryControlDetailBatchSerial = New InventoryControlDetailBatchSerial
                            inventoryControlDetailBatchSerial.BatchSerialId = batchSerialId
                            inventoryControlDetailBatchSerial.BatchSerialCode = row("BatchSerialCode").ToString()
                            inventoryControlDetailBatchSerial.Quantity = CInt(row("Quantity"))
                            inventoryControlDetailBatchSerial.InventoryQuantity = CInt(row("InventoryQuantity"))
                            inventoryControlDetail.InventoryControlDetailBatchSerial.Add(inventoryControlDetailBatchSerial)
                        Else
                            inventoryControlDetailBatchSerial.Quantity += CInt(row("Quantity"))
                        End If

                        If controlType = 1 Or controlType = 3 Then
                            inventoryControlDetailBatchSerial.Status = 0
                        Else
                            inventoryControlDetailBatchSerial.Status = If(inventoryControlDetailBatchSerial.Quantity <> inventoryControlDetailBatchSerial.InventoryQuantity, 1, 2)
                        End If
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(row("MessageField").ToString())
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of InventoryControlDetail)) With {.StateResult = True, .ObjectEmbbeded = listInventoryControlDetail, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of InventoryControlDetail)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of InventoryControlDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of InventoryControlDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SetProductsPurchaseOrderImportFile(data As List(Of ImportFileRow), operatingUnitId As Integer, audit As AuditMessage, Optional roundingType As Decimal = 0.01) As ActionResult(Of List(Of PurchaseOrderDetail)) Implements IInventoryService.SetProductsPurchaseOrderImportFile
        Try
            Dim listPurchaseOrderDetail As New List(Of PurchaseOrderDetail)
            Dim purchaseOrderDetail As PurchaseOrderDetail = Nothing
            Dim listErrors As New List(Of String)

            For Each row In data
                Dim indexRow = row.IndexRow
                If row.Row.Item(0) Is Nothing Then
                    listErrors.Add("El producto del item " + indexRow.ToString() + " esta vacío")
                    Continue For
                End If
                If (row.Row.Item(0).ToString().Trim.Length > 20) Then
                    listErrors.Add("El producto " + row.Row.Item(0) + " del item " + indexRow.ToString() + " exede la cantidad de caracteres permitida")
                    Continue For
                End If
                Dim productCode As String
                productCode = row.Row.Item(0).ToString()
                'valido el producto
                Dim product = _inventoryProductRepository.GetInventoryProductByCodeWithProducGroup(productCode, False)
                If product Is Nothing OrElse product.Id = 0 Then
                    listErrors.Add("El producto " + productCode + " del item " + indexRow.ToString() + " no existe")
                    Continue For
                End If
                'valido la cantidad
                If row.Row.Item(1) Is Nothing Then
                    listErrors.Add("La cantidad del item " + indexRow.ToString() + " esta vacía")
                    Continue For
                End If
                If Not IsNumeric(row.Row.Item(1)) Then
                    listErrors.Add("La cantidad del item " + indexRow.ToString() + " no es numerica")
                    Continue For
                End If
                If CInt(row.Row.Item(1)) <= 0 Then
                    listErrors.Add("La cantidad del item " + indexRow.ToString() + " debe ser mayor a 0")
                    Continue For
                End If
                'Valido el Valor Unitario
                If row.Row.Item(2) Is Nothing Then
                    listErrors.Add("El Valor Unitario del item " + indexRow.ToString() + " esta vacío")
                    Continue For
                End If
                If Not IsNumeric(row.Row.Item(2)) Then
                    listErrors.Add("El Valor Unitario del item " + indexRow.ToString() + " no es numerico")
                    Continue For
                End If
                'Valido el porcentaje de descuento
                If row.Row.Item(3) Is Nothing Then
                    listErrors.Add("El porcentaje de descuento del item " + indexRow.ToString() + " esta vacío")
                    Continue For
                End If
                If Not IsNumeric(row.Row.Item(3)) Then
                    listErrors.Add("El porcentaje de descuento del item " + indexRow.ToString() + " no es numerico")
                    Continue For
                End If

                'busco si existe un detalle con el mismo producto
                purchaseOrderDetail = listPurchaseOrderDetail.Find(Function(x) x.ProductId = product.Id)
                If purchaseOrderDetail Is Nothing Then
                    purchaseOrderDetail = New PurchaseOrderDetail()
                    purchaseOrderDetail.InventoryProduct = product
                    purchaseOrderDetail.ProductId = product.Id
                    purchaseOrderDetail.ProductCode = product.Code
                    purchaseOrderDetail.ProductName = product.Name
                    purchaseOrderDetail.CodeNameProduct = String.Format("{0} - {1}", product.Code, product.Name)
                    purchaseOrderDetail.Quantity = CInt(row.Row.Item(1))
                    purchaseOrderDetail.OutstandingQuantity = CInt(row.Row.Item(1))
                    purchaseOrderDetail.CancelledQuantity = 0
                    purchaseOrderDetail.DiscountPercentage = CDbl(row.Row.Item(3))

                    purchaseOrderDetail.Value = CDbl(row.Row.Item(2))

                    ''variable de redondeo para iva y descuento
                    Dim Rounding As Decimal = 0.01
                    If product.IVAId IsNot Nothing Then
                        Dim generalLedgerIva As GeneralLedgerIVA = _generalLedgerIVARepository.GetGeneralLedgerIVAById(product.IVAId.Value)
                        If generalLedgerIva IsNot Nothing Then
                            'purchaseOrderDetail.Value = product.FinalProductCost / (1 + generalLedgerIva.Percentage / 100)
                            purchaseOrderDetail.IvaPercentage = generalLedgerIva.Percentage
                            purchaseOrderDetail.DiscountValue = CDec((purchaseOrderDetail.Quantity * purchaseOrderDetail.Value) * (purchaseOrderDetail.DiscountPercentage / 100))
                            purchaseOrderDetail.IvaValue = Utils.RoundValue(CDec(((purchaseOrderDetail.Quantity * purchaseOrderDetail.Value) - purchaseOrderDetail.DiscountValue) _
                                                                                 * (purchaseOrderDetail.IvaPercentage / 100)), Rounding)
                            purchaseOrderDetail.TotalIva = Utils.RoundValue((purchaseOrderDetail.Quantity * purchaseOrderDetail.Value) * (purchaseOrderDetail.IvaPercentage / 100), Rounding)
                        Else
                            listErrors.Add($"No se encontró iva para el producto {productCode} en la fila {indexRow}")
                            Continue For
                        End If
                    Else
                        'purchaseOrderDetail.Value = product.FinalProductCost
                        purchaseOrderDetail.IvaPercentage = 0
                        purchaseOrderDetail.IvaValue = 0
                        purchaseOrderDetail.DiscountValue = Utils.RoundValue(CDec((purchaseOrderDetail.Quantity * purchaseOrderDetail.Value) _
                                                                             * (purchaseOrderDetail.DiscountPercentage / 100)), Rounding)
                        purchaseOrderDetail.TotalIva = Utils.RoundValue(purchaseOrderDetail.SubTotalValue * (purchaseOrderDetail.IvaPercentage / 100), Rounding)
                    End If

                    purchaseOrderDetail.SubTotalValue = Utils.RoundValue((purchaseOrderDetail.Quantity * purchaseOrderDetail.Value), roundingType)
                    purchaseOrderDetail.TotalValue = Utils.RoundValue((purchaseOrderDetail.SubTotalValue - purchaseOrderDetail.DiscountValue + purchaseOrderDetail.IvaValue), roundingType)
                    listPurchaseOrderDetail.Add(purchaseOrderDetail)
                End If
            Next
            Return New ActionResult(Of List(Of PurchaseOrderDetail)) With {.ObjectEmbbeded = listPurchaseOrderDetail, .MessageResult = listErrors}
        Catch ex As Exception
            Return New ActionResult(Of List(Of PurchaseOrderDetail)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    Public Function SetProductsProductInvoiceImportFile(data As List(Of ImportFileRow), warehouseId As Integer, operatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of List(Of DocumentInvoiceProductSalesDetail)) Implements IInventoryService.SetProductsProductInvoiceImportFile
        Dim listDocumentInvoiceProductSalesDetail As New List(Of DocumentInvoiceProductSalesDetail)
        Dim documentInvoiceSalesDetail As DocumentInvoiceProductSalesDetail = Nothing
        Dim listErrors As New List(Of String)
        Dim inventorySetting As SettingInventory = _settingInventoryrepository.GetSettingInventoryByOperatingUnitId(operatingUnitId)
        If inventorySetting Is Nothing OrElse inventorySetting.Id = 0 Then
            listErrors.Add("No se encontraron parámetros de inventarios")
            Return New ActionResult(Of List(Of DocumentInvoiceProductSalesDetail)) With {.ObjectEmbbeded = listDocumentInvoiceProductSalesDetail, .MessageResult = listErrors}
        End If
        For Each row As ImportFileRow In data

            If row.Row.All(Function(x) String.IsNullOrEmpty(x)) Then
                Continue For
            End If

            'valido la estructura
            If row.Row.Count <> 6 Then
                listErrors.Add(String.Format(ResourceManager.GetString("IncorrectStructure", "Treasury"), (data.IndexOf(row)).ToString()))
                Continue For
            End If
            Dim indexRow = row.IndexRow
            If row.Row.Item(0) Is Nothing Then
                listErrors.Add($"El producto del item {indexRow.ToString()} esta vacío")
                Continue For
            End If
            If (row.Row.Item(0).ToString().Trim.Length > 20) Then
                listErrors.Add("El producto " + row.Row.Item(0) + " del item " + indexRow.ToString() + " excede la cantidad de caracteres permitida")
                Continue For
            End If
            Dim productCode As String
            If IsNumeric(row.Row.Item(0)) Then
                productCode = CDec(row.Row.Item(0)).ToString()
            Else
                productCode = row.Row.Item(0)
            End If
            'valido el producto
            Dim product = _inventoryProductRepository.GetInventoryProductByCodeWithProducGroup(productCode, False)
            If product Is Nothing OrElse product.Id = 0 Then
                listErrors.Add($"El producto {productCode} del item {indexRow.ToString()} no existe")
                Continue For
            ElseIf product.ProductSubGroupId = 0 Then
                listErrors.Add($"El producto {productCode} del item {indexRow.ToString()} no tiene un subgrupo asociado")
                Continue For
            End If
            'valido el lote
            Dim batchCode As String = String.Empty
            If product.ProductSubGroup.HandlesBatch Then
                If row.Row.Item(1) Is Nothing Then
                    listErrors.Add($"No se asigno lote para el producto {productCode} del item {indexRow.ToString()}")
                    Continue For
                End If
                batchCode = row.Row.Item(1).ToString()
            End If
            Dim batchSerial As BatchSerial = Nothing
            If batchCode IsNot String.Empty Then
                If batchCode.Length > 50 Then
                    listErrors.Add($"El codigo del lote {batchCode} del item {indexRow.ToString()} tiene una longitud mayor a 50")
                    Continue For
                End If

                Dim expirationDate = CDate(row.Row.Item(2))
                batchSerial = _batchSerialRepository.FirstOrDefault(Function(m) m.ProductId = product.Id AndAlso m.BatchCode = batchCode AndAlso m.ExpirationDate = expirationDate)

                If batchSerial IsNot Nothing AndAlso batchSerial.Id = 0 Then
                    'creo el lote para poder asignarlo
                    If row.Row.Item(2) Is String.Empty Then
                        listErrors.Add($"La fecha del item {indexRow.ToString()} esta vacía")
                        Continue For
                    End If
                    If Not IsDate(row.Row.Item(2)) Then
                        listErrors.Add($"La fecha del item {indexRow.ToString()} no tiene un formato válido")
                        Continue For
                    End If
                    batchSerial = New BatchSerial()
                    With batchSerial
                        .ProductId = product.Id
                        .Type = 2
                        .BatchCode = batchCode
                        .ExpirationDate = expirationDate
                        .CreationDate = DateTime.Now
                        .CreationUser = audit.CodeUser
                    End With
                    _batchSerialRepository.SaveEntity(batchSerial)
                    _batchSerialRepository.UnitWork.Commit()
                    listErrors.Add($"El lote {batchCode} del item {indexRow.ToString()} fue creado")
                    Continue For
                End If
            End If
            'valido la cantidad
            If row.Row.Item(3) Is Nothing Then
                listErrors.Add($"La cantidad del item {indexRow.ToString()} esta vacía")
                Continue For
            End If
            If Not IsNumeric(row.Row.Item(3)) Then
                listErrors.Add($"La cantidad del item {indexRow.ToString()} no es numérica")
                Continue For
            End If
            If CInt(row.Row.Item(3)) <= 0 Then
                listErrors.Add($"La cantidad del item {indexRow.ToString()} debe ser mayor a 0")
                Continue For
            End If
            'Valor de Venta
            If row.Row.Item(4) Is Nothing Then
                listErrors.Add($"El Valor Unitario del item {indexRow.ToString()} esta vacío")
                Continue For
            End If
            If Not IsNumeric(row.Row.Item(4)) Then
                listErrors.Add($"El Valor Unitario del item {indexRow.ToString()} no es numérico")
                Continue For
            End If
            If CInt(row.Row.Item(4)) < 0 Then
                listErrors.Add($"El Valor Unitario del item {indexRow.ToString()} debe ser mayor o igual a 0")
                Continue For
            End If
            'Valido el porcentaje de descuento
            If row.Row.Item(5) Is Nothing Then
                listErrors.Add($"El porcentaje de descuento del item {indexRow.ToString()} esta vacío")
                Continue For
            End If
            If Not IsNumeric(row.Row.Item(5)) Then
                listErrors.Add($"El porcentaje de descuento del item {indexRow.ToString()} no es numérico")
                Continue For
            End If
            'busco si existe un detalle con el mismo producto
            documentInvoiceSalesDetail = listDocumentInvoiceProductSalesDetail.Find(Function(x) x.ProductId = product.Id)
            If documentInvoiceSalesDetail Is Nothing Then
                documentInvoiceSalesDetail = New DocumentInvoiceProductSalesDetail()
                documentInvoiceSalesDetail.ProductId = product.Id
                documentInvoiceSalesDetail.CodeNameProduct = $"{product.Code} - {product.Name}"
                documentInvoiceSalesDetail.Quantity = CInt(row.Row.Item(3))
                Dim physicalInventory As PhysicalInventory = Nothing
                Dim documentInvoiceProductSalesDetailBatchSerial As New DocumentInvoiceProductSalesDetailBatchSerial()

                If batchSerial IsNot Nothing AndAlso batchSerial.Id > 0 Then
                    physicalInventory = _physicalInventory.GetPhysicalInventoryByBatchSerial(product.Id, warehouseId, batchSerial.Id)
                    documentInvoiceProductSalesDetailBatchSerial.BatchCode = batchSerial.BatchCode
                Else
                    physicalInventory = _physicalInventory.GetPhysicalInventory(product.Id, warehouseId)
                End If

                If physicalInventory Is Nothing OrElse physicalInventory.Id = 0 Then
                    listErrors.Add($"El producto {productCode} en el item {indexRow.ToString()} no se encuentra en el Inventario físico.")
                    Continue For
                End If
                If physicalInventory.Quantity < documentInvoiceSalesDetail.Quantity Then
                    listErrors.Add($"El producto {productCode} en el item {indexRow.ToString()} no cuenta con la cantidad suficiente en el almacen seleccionado.")
                    Continue For
                End If

                documentInvoiceProductSalesDetailBatchSerial.Quantity = CInt(row.Row.Item(3))
                documentInvoiceProductSalesDetailBatchSerial.PhysicalInventoryId = physicalInventory.Id
                documentInvoiceSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial.Add(documentInvoiceProductSalesDetailBatchSerial)
                listDocumentInvoiceProductSalesDetail.Add(documentInvoiceSalesDetail)
            Else
                Dim documentInvoiceProductSalesDetailBatchSerial As New DocumentInvoiceProductSalesDetailBatchSerial
                If batchSerial IsNot Nothing AndAlso batchSerial.Id > 0 Then
                    'valido que el lote que se va agregar no exista en el listado
                    If documentInvoiceSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial _
                            .Any(Function(m) m.BatchCode.Equals(batchSerial.BatchCode)) Then
                        listErrors.Add($"El lote {batchCode} ya se encuentra agregardo al producto {productCode}")
                        Continue For
                    End If
                    Dim physicalInventory As PhysicalInventory = _physicalInventory.GetPhysicalInventoryByBatchSerial(product.Id, warehouseId, batchSerial.Id)
                    If physicalInventory Is Nothing OrElse physicalInventory.Id = 0 Then
                        listErrors.Add($"El producto {productCode} en el item {indexRow.ToString()} no se encuentra en el Inventario físico.")
                        Continue For
                    End If
                    documentInvoiceProductSalesDetailBatchSerial.PhysicalInventoryId = physicalInventory.Id
                    documentInvoiceProductSalesDetailBatchSerial.BatchCode = batchSerial.BatchCode
                    documentInvoiceProductSalesDetailBatchSerial.Quantity = CInt(row.Row.Item(3))
                Else
                    Dim physicalInventory As PhysicalInventory = _physicalInventory.GetPhysicalInventory(product.Id, warehouseId)
                    If physicalInventory Is Nothing OrElse physicalInventory.Id = 0 Then
                        listErrors.Add($"El producto {productCode} en el item {indexRow.ToString()} no se encuentra en el Inventario físico.")
                        Continue For
                    End If
                    documentInvoiceProductSalesDetailBatchSerial.PhysicalInventoryId = physicalInventory.Id
                    documentInvoiceProductSalesDetailBatchSerial.Quantity = CInt(row.Row.Item(3))
                End If
                documentInvoiceSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial.Add(documentInvoiceProductSalesDetailBatchSerial)
                documentInvoiceSalesDetail.Quantity = documentInvoiceSalesDetail.DocumentInvoiceProductSalesDetailBatchSerial.Sum(Function(x) x.Quantity)
            End If
            Dim ivaPercentage As Decimal = 0
            If product.IVAId IsNot Nothing Then
                Dim generalLedgerIva As GeneralLedgerIVA = _generalLedgerIVARepository.GetGeneralLedgerIVAById(product.IVAId.Value)
                If generalLedgerIva IsNot Nothing Then
                    ivaPercentage = generalLedgerIva.Percentage
                Else
                    listErrors.Add($"No se encontró iva para el producto {productCode} en la fila {indexRow}")
                    Continue For
                End If
            End If
            With documentInvoiceSalesDetail
                .HandlesBatch = product.ProductSubGroup.HandlesBatch
                .SalePrice = Utils.RoundValue(CDec(row.Row.Item(4)), Utils.RoundLevel.Unit)
                .SubTotalValue = .SalePrice * .Quantity
                .DiscountPercentage = CDbl(row.Row.Item(5))
                .DiscountValue = .SubTotalValue * .DiscountPercentage / 100
                .IvaPercentage = ivaPercentage
                .IvaValue = .SubTotalValue * .IvaPercentage / 100
                .TotalValue = .SubTotalValue - .DiscountValue + .IvaValue
            End With
        Next
        Return New ActionResult(Of List(Of DocumentInvoiceProductSalesDetail)) With {.ObjectEmbbeded = listDocumentInvoiceProductSalesDetail, .MessageResult = listErrors}
    End Function
#End Region

#Region "Datatables"

    Private Function ConvertToXmlImportInventoryControlDetail(dataImportFile As List(Of ImportFileRow))
        Dim builder As StringBuilder = New StringBuilder()

        For Each importFile In dataImportFile
            builder.Append("<Data>")

            builder.Append("<ProductCode>" & If(importFile.Row.Count > 0, importFile.Row.Item(0), String.Empty) & "</ProductCode>")
            builder.Append("<BatchSerialCode>" & If(importFile.Row.Count > 1, importFile.Row.Item(1), String.Empty) & "</BatchSerialCode>")
            builder.Append("<BatchSerialExpirationDate>" & If(importFile.Row.Count > 2, importFile.Row.Item(2), String.Empty) & "</BatchSerialExpirationDate>")
            builder.Append("<Quantity>" & If(importFile.Row.Count > 3 AndAlso importFile.Row.Item(3) IsNot Nothing, importFile.Row.Item(3).ToString.Replace(",", "."), 0) & "</Quantity>")

            builder.Append("</Data>")
        Next

        Return builder.ToString
    End Function

    Private Function ConvertToXmlCopyPasteInventoryControlDetail(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()

        For Each item In data
            builder.Append("<Data>")

            builder.Append("<ProductCode>" & If(item.Count > 0, item(0), String.Empty) & "</ProductCode>")
            builder.Append("<BatchSerialCode>" & If(item.Count > 1, item(1), String.Empty) & "</BatchSerialCode>")
            builder.Append("<BatchSerialExpirationDate>" & If(item.Count > 2, item(2), String.Empty) & "</BatchSerialExpirationDate>")
            builder.Append("<Quantity>" & If(item.Count > 3 AndAlso item(3) IsNot Nothing, item(3).ToString.Replace(",", "."), 0) & "</Quantity>")

            builder.Append("</Data>")
        Next

        Return builder.ToString
    End Function

    Private Function SP_CopyAndPasteInventoryControlDetail(WarehouseId As Integer, XmlObject As String, UserCode As String) As DataSet
        Try
            Dim ds As New DataSet
            Dim query As String = String.Format("EXEC [Inventory].[SP_CopyAndPasteInventoryControlDetail] {0}, '{1}', '{2}'", WarehouseId, XmlObject, UserCode)
            Dim dt = Me.GetDatatable(query, "CopyAndPasteInventoryControlDetail")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetDatatable(ByVal Comando As String, nameDt As String) As System.Data.DataTable
        Dim conexion As New SqlClient.SqlConnection
        Try
            If conexion.State = ConnectionState.Closed Then
                conexion = New SqlClient.SqlConnection(Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ServerSessionValues.Current.CurrentContainer, False))
                conexion.Open()
            End If
            Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(Comando, conexion)
            da.SelectCommand.CommandTimeout = 36000
            Dim ds As New DataSet
            da.Fill(ds, nameDt)
            GetDatatable = ds.Tables(nameDt)
            da = Nothing
            ds = Nothing
            conexion.Close()
            Return GetDatatable
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        Finally
            conexion.Close()
        End Try
    End Function

#End Region

#Region "Enums"
    Public Enum eProductTypeClass
        Grupo = 1
        ItemMedicamento = 2
        ItemInsumo = 3
        ItemOtro = 4
    End Enum
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _generalLedgerIVARepository = Nothing
            _inventoryProductRepository = Nothing
            _productTypeRepository = Nothing
            _settingInventoryrepository = Nothing
            _productRateDetailRepository = Nothing
            _productGroupsRepository = Nothing
            _mainAccountsRepository = Nothing
            _functionaUnitRepository = Nothing
            _wareHouseRepository = Nothing
            _caregroupRepository = Nothing
            _contractRepository = Nothing
            _healthAdministratorRepository = Nothing
            _admissionRepository = Nothing
            _accountPayableConceptRepository = Nothing
            _thirdPartyRepository = Nothing
            _revenueControlDetailRepository = Nothing
            _serviceOrderRepository = Nothing
            _serviceOrderDetailDistributionRepository = Nothing
            _pharmaceuticalDispensingDetailRepositoy = Nothing
            _adjusmentConceptRepository = Nothing
            _transferOrderRepository = Nothing
            _transferOrderDetailRepository = Nothing
            _transferOrderDetailBatchSerialRepository = Nothing
            _physicalInventory = Nothing
            _warehouseStockRepository = Nothing
            _batchSerialRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
