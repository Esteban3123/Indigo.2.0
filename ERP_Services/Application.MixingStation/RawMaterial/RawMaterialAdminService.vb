'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Juan David Patiño Cabrera
' Created          : 13/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Application.Inventory.InventoryRequest
Imports Application.Inventory.TransferOrder
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.ProductMixingStation
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region

Partial Public Class RawMaterialAdminService
    Implements IRawMaterialAdminService, Inject

#Region "Properties"
    ''' <summary>
    ''' Variables tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _CampaignRepository As ICampaignRepository
    Private ReadOnly _CampaignDetailItemsRepository As ICampaignItemsRepository
    Private ReadOnly _PickingRepository As IPickingRepository
    Private ReadOnly _CampaignDetailRepository As ICampaignDetailRepository
    Private ReadOnly _CMConfigRepository As ICMConfigRepository
    Private ReadOnly _cmWarehouseRepository As ICMWarehouseRepository
    Private ReadOnly _transferOrderAdminService As ITransferOrderAdminService
    Private ReadOnly _InventoryRequestAdminService As IInventoryRequestAdminService
    Private ReadOnly _campaignDetailValidationRepository As ICampaignDetailValidationRepository
    Private _campaignReportsRepository As ICampaignReportsRepository
    Private _campaignReports As ICampaignReportsAdminService
    Private Const FORM_NAME As String = "FrmRawMaterial"
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _RawMaterialRepository As IRawMaterialRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _ProductionBasketsRepository As IProductionBasketsRepository
    Private ReadOnly _physicalInventoryRepository As IPhysicalInventoryRepository
    Private ReadOnly _wareHouseRepository As IWarehouseRepository
    Private ReadOnly _contractExternalClientsDetail As IContractExternalClientsDetailRepository
    Private ReadOnly _campaignDetailBasketDetailRepository As ICampaignDetailBasketDetailRepository
    Private ReadOnly _campaignKardex As ICampaignKardexAdminService
    Private ReadOnly _inventoryService As IInventoryService
#End Region

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(
                  ByVal RawMaterialRepository As IRawMaterialRepository,
                  ByVal RawMaterialsecuenseDetailRepository As IMixingStationSequenceDetailRepository,
                  ByVal ProductionBasketsRepository As IProductionBasketsRepository,
                  ByVal CampaignRepository As ICampaignRepository,
                  ByVal CampaignDetailItemsRepository As ICampaignItemsRepository,
                  ByVal PickingRepository As IPickingRepository,
                  ByVal CampaignDetailRepository As ICampaignDetailRepository,
                  physicalInventoryRepository As IPhysicalInventoryRepository,
                  wareHouseRepository As IWarehouseRepository,
                  cmWarehouseRepository As ICMWarehouseRepository,
                  campaignKardex As ICampaignKardexAdminService,
                  inventoryService As IInventoryService,
                  ByVal CMConfigurationRepository As ICMConfigRepository,
                  ByVal transferOrderAdminService As ITransferOrderAdminService,
                  ByVal InventoryRequestAdminService As IInventoryRequestAdminService,
                  ByVal campaignDetailValidationRepository As ICampaignDetailValidationRepository,
                  ByVal campaignReports As ICampaignReportsAdminService,
                  ByVal campaignReportsRepository As ICampaignReportsRepository,
                  contractExternalClientsDetail As IContractExternalClientsDetailRepository,
                  campaignDetailBasketDetailRepository As ICampaignDetailBasketDetailRepository)
        If RawMaterialRepository Is Nothing Then
            Throw New ArgumentNullException("Materia Prima Vacio")
        End If

        _campaignKardex = campaignKardex
        _inventoryService = inventoryService
        _wareHouseRepository = wareHouseRepository
        _RawMaterialRepository = RawMaterialRepository
        _physicalInventoryRepository = physicalInventoryRepository
        _secuenseDetailRepository = RawMaterialsecuenseDetailRepository
        _ProductionBasketsRepository = ProductionBasketsRepository
        _CampaignRepository = CampaignRepository
        _cmWarehouseRepository = cmWarehouseRepository
        _CampaignDetailItemsRepository = CampaignDetailItemsRepository
        _campaignDetailValidationRepository = campaignDetailValidationRepository
        _PickingRepository = PickingRepository
        _CampaignDetailRepository = CampaignDetailRepository
        _CMConfigRepository = CMConfigurationRepository
        _transferOrderAdminService = transferOrderAdminService
        _InventoryRequestAdminService = InventoryRequestAdminService
        _campaignReportsRepository = campaignReportsRepository
        _campaignReports = campaignReports
        _contractExternalClientsDetail = contractExternalClientsDetail
        _campaignDetailBasketDetailRepository = campaignDetailBasketDetailRepository
    End Sub

#Region "Functions"

#Region "Frm de Materia Prima (FrmRawMaterial)"
    ''' <summary>
    ''' Función para listar Productos a partir de un 
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="stockId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function ListPhysicalInventoryByCode(aTCNumber As String, warehouseId As Integer, stockId As Integer, maquilaId As Integer?, session As SessionValues) As List(Of PhysicalInventory) Implements IRawMaterialAdminService.ListPhysicalInventoryByCode
        Try
            Dim res As List(Of PhysicalInventory) = _RawMaterialRepository.SP_ListPhysicalInventoryByCodeMaterialRaw(aTCNumber, warehouseId, stockId, maquilaId)

            Return res
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para listar Productos a partir de un 
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="stockId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function ListPhysicalInventoryByATCSupplyProduct(campaignDetailId As Integer, atcid As Integer?, supplyId As Integer?, productId As Integer?, warehouseId As Integer, stockId As Integer, maquilaId As Integer?, session As SessionValues) As List(Of PhysicalInventory) Implements IRawMaterialAdminService.ListPhysicalInventoryByATCSupplyProduct
        Try
            Dim exceptions = _contractExternalClientsDetail.GetRawMaterialExceptionByCampaignDetailIdAndItemId(campaignDetailId, atcid, supplyId, productId)
            Dim parameters As IEnumerable(Of (String, Object)) = Nothing
            If exceptions IsNot Nothing Then
                If exceptions.SuppliedBy = 1 Then
                    parameters = {
                        ("@ATCId", atcid), ("@SupplyId", supplyId), ("@ProductId", productId), ("@WarehouseId", Nothing), ("@StockId", Nothing), ("@MaquilaId", maquilaId)
                    }
                Else
                    parameters = {
                        ("@ATCId", atcid), ("@SupplyId", supplyId), ("@ProductId", productId), ("@WarehouseId", warehouseId), ("@StockId", stockId), ("@MaquilaId", maquilaId)
                    }
                End If
            Else
                parameters = {
                    ("@ATCId", atcid), ("@SupplyId", supplyId), ("@ProductId", productId), ("@WarehouseId", warehouseId), ("@StockId", stockId), ("@MaquilaId", Nothing)
                }
            End If

            Dim res = _RawMaterialRepository.ExecuteStoredProcedure(Of PhysicalInventory)("[MixingStation].[SP_ListPhysicalInventoryByATCSupplyProduct]", parameters)
            res?.ToList().ForEach(Sub(m) m.MarkAsUnchanged())
            Return res
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Dispensación manual
    ''' </summary>
    ''' <param name="campaignDetailItems"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Async Function ManualDeliveryAsync(
        campaignDetailItems As List(Of CampaignDetailItems),
        session As SessionValues) As Task(Of ActionResult) Implements IRawMaterialAdminService.ManualDeliveryAsync

        Try
            If campaignDetailItems Is Nothing OrElse Not campaignDetailItems.Any() Then
                Throw New IndigoValidationException("No se ha enviado algún dato")
            End If

            Dim sbErrors As New StringBuilder()
            Dim sbManualDeliveryErrors As New StringBuilder()
            Dim sbInsufficientInventoryErrors As New StringBuilder()
            Dim sbDeliverSuccess As New StringBuilder()

            Dim campaignDetailId As Integer = campaignDetailItems(0).CampaignDetailId
            Dim cmWarehouseStock = _cmWarehouseRepository.GetByTypeAndCampaignDetailId(campaignDetailId, 1)
            Dim contract = _contractExternalClientsDetail.GetContractExternalClientByCampaignDetailId(campaignDetailId)
            Dim wareHouseMaquila As Integer? = If(contract IsNot Nothing AndAlso contract.ManagesMaquila, contract.WarehouseId, Nothing)
            Dim campaignDetailValidations = _campaignDetailValidationRepository.Query(Function(m) m.CampaignDetailId = campaignDetailId).ToList()
            Dim unitDoseClassCampaign = _CampaignDetailRepository.Query(Function(m) m.Id = campaignDetailId).Select(Function(m) m.UnitDoseType.MSClass).FirstOrDefault()

            campaignDetailValidations.ForEach(Sub(i) i.DeliveredQuantityTmp = i.DeliveredQuantity)
            Dim campaignValidationToSave As New List(Of CampaignDetailValidation)()

            Dim getItemCode = Function(item As CampaignDetailItems)
                                  If item.AtcId.HasValue Then Return item.AtcCodeName
                                  If item.ProductId.HasValue Then Return item.ProductCodeName
                                  If item.SupplyId.HasValue Then Return item.SupplyCodeName
                                  Return Nothing
                              End Function

            Using scope As New TransactionScope(
            TransactionScopeOption.Required,
            New TransactionOptions With {
                .Timeout = TransactionManager.MaximumTimeout,
                .IsolationLevel = IsolationLevel.ReadCommitted
            },
            TransactionScopeAsyncFlowOption.Enabled
        )

                For Each item In campaignDetailItems
                    Dim outstandingQuantity = item.RequestQuantity - item.DeliveredQuantity
                    If outstandingQuantity <= 0 Then Continue For

                    Dim itemName = getItemCode(item)
                    Dim physicalInventory = ListPhysicalInventoryByATCSupplyProduct(campaignDetailId, item.AtcId, item.SupplyId, item.ProductId, warehouseId:=Nothing, stockId:=cmWarehouseStock.IdWarehouse, maquilaId:=wareHouseMaquila, session:=session)

                    If physicalInventory Is Nothing OrElse Not physicalInventory.Any() Then
                        sbErrors.AppendLine($"- {itemName}")
                        Continue For
                    End If

                    physicalInventory = physicalInventory _
                    .OrderBy(Function(m) If(m.WarehouseId = cmWarehouseStock.IdWarehouse, 0, 1)) _
                    .ThenBy(Function(m) If(m.Covered, 0, 1)) _
                    .ThenBy(Function(m) If(m.BatchSerialExpiredDate, Date.MaxValue)) _
                    .ToList()

                    For Each physical In physicalInventory
                        Dim finded = campaignDetailValidations.Find(Function(m) _
                        m.ProductId = physical.ProductId _
                        AndAlso m.BatchSerialId.HasValue AndAlso physical.BatchSerialId.HasValue _
                        AndAlso m.BatchSerialId.Value = physical.BatchSerialId.Value _
                        AndAlso m.WarehouseId = physical.WarehouseId _
                        AndAlso m.ItemType = item.ItemType)

                        If finded Is Nothing Then
                            finded = New CampaignDetailValidation With {
                                .CampaignDetailId = campaignDetailId,
                                .ProductId = physical.ProductId,
                                .BatchSerialId = physical.BatchSerialId,
                                .WarehouseId = physical.WarehouseId,
                                .TypeProcess = 1,
                                .ItemType = item.ItemType,
                                .CreationUser = session.AuditMessageWcf.CodeUser,
                                .CreationDate = Date.Now
                            }
                        Else
                            finded.MarkAsModified()
                            finded.TypeProcess = 1
                            finded.ModificationUser = session.AuditMessageWcf.CodeUser
                            finded.ModificationDate = Date.Now
                        End If

                        Dim canDeliver = physical.Quantity - finded.DeliveredQuantity

                        If canDeliver >= outstandingQuantity Then
                            item.DeliveredQuantity += outstandingQuantity
                            finded.DeliveredQuantity += outstandingQuantity
                            outstandingQuantity = 0
                        Else
                            item.DeliveredQuantity += canDeliver
                            outstandingQuantity -= canDeliver
                            finded.DeliveredQuantity += canDeliver
                        End If

                        Try
                            campaignValidationToSave.Add(finded)
                        Catch ex As Exception
                            sbManualDeliveryErrors.AppendLine($"- {itemName}")
                        End Try

                        If outstandingQuantity = 0 Then Exit For
                    Next

                    If outstandingQuantity > 0 Then
                        sbInsufficientInventoryErrors.AppendLine($"- {itemName}. Cantidad pendiente: {outstandingQuantity}")
                    Else
                        sbDeliverSuccess.AppendLine($"- {itemName}")
                    End If
                Next

                If campaignValidationToSave?.Any() Then
                    Await _campaignDetailValidationRepository.SaveEntityMassiveAsync(campaignValidationToSave)
                End If

                scope.Complete()
            End Using

            Dim msg As New StringBuilder()

            If sbDeliverSuccess.Length = 0 AndAlso sbManualDeliveryErrors.Length = 0 AndAlso sbErrors.Length = 0 Then
                For Each item In campaignDetailItems
                    Dim itemName = getItemCode(item)
                    sbDeliverSuccess.AppendLine($"- {itemName}")
                Next
            End If

            If sbDeliverSuccess.Length > 0 Then
                msg.AppendLine("* Se realizo la entrega manual correctamente de los siguientes productos: ")
                msg.AppendLine(sbDeliverSuccess.ToString())
            End If

            If sbManualDeliveryErrors.Length > 0 Then
                msg.AppendLine("* No se pudo realizar la entrega manual correctamente de los siguientes productos: ")
                msg.AppendLine(sbManualDeliveryErrors.ToString())
            End If

            If sbErrors.Length > 0 Then
                msg.AppendLine("* No se encontró inventario para los siguientes productos: ")
                msg.AppendLine(sbErrors.ToString())
            End If

            If sbInsufficientInventoryErrors.Length > 0 Then
                msg.AppendLine("* El inventario disponible no cubre la cantidad requerida de los siguientes productos: ")
                msg.AppendLine(sbInsufficientInventoryErrors.ToString())
            End If

            Dim stateResult = sbManualDeliveryErrors.Length = 0 AndAlso sbErrors.Length = 0 AndAlso sbInsufficientInventoryErrors.Length = 0
            Return New ActionResult With {.StateResult = stateResult, .Message = msg.ToString()}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Listar medicamentos en el formulario de Materia Prima
    ''' </summary>
    ''' <param name="CampaignDetailId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Async Function ListProductRawMaterialAsync(CampaignDetailId As Integer, StockId As Integer, warehouseId As Integer, Type As Integer, session As SessionValues) As Task(Of List(Of ProductMixingStation)) Implements IRawMaterialAdminService.ListProductRawMaterialAsync
        Try
            Dim result As New List(Of ProductMixingStation)
            Dim unitDoseTypeClass = CType(Await GetUnitDoseTypeClass(CampaignDetailId), EUnitDoseTypeClass)
            Dim campaignDosisData = GetCampaignDosisData(CampaignDetailId, Type)
            If campaignDosisData Is Nothing OrElse Not campaignDosisData.Any() Then
                Return result
            End If

            Dim RawMaterialLeftovers = AggregateRawMaterialLeftovers(campaignDosisData)

            ' Para Type=2 (mL): usar datos de Type=1 (mg) en el cálculo de viales para que "Cantidad requerida (en unidades)" coincida
            ' Clave compuesta (DetailId, ItemId): un RequestMixingStationDetailId puede tener varios medicamentos (Principal + Otro)
            Dim lookupMgByDetailItem As Dictionary(Of (Integer?, Integer), ProductCalculateMixingStation) = Nothing
            If Type = 2 Then
                Dim campaignDosisDataMg = GetCampaignDosisData(CampaignDetailId, 1)
                lookupMgByDetailItem = (If(campaignDosisDataMg, New List(Of ProductCalculateMixingStation))).
                    Where(Function(x) x.RequestMixingStationDetailId > 0 AndAlso x.ItemId > 0).
                    GroupBy(Function(x) (x.RequestMixingStationDetailId, x.ItemId)).
                    ToDictionary(Function(g) g.Key, Function(g) g.First())
            End If

            Dim personalizedVehicleUseByDetail As Dictionary(Of Integer, Decimal) = Nothing
            If campaignDosisData IsNot Nothing AndAlso campaignDosisData.Any(Function(x) x.IsPackagePersonalized AndAlso x.HasComplementaryMedicine) Then
                Dim campaignDosisDataMl = If(Type = 2, campaignDosisData, GetCampaignDosisData(CampaignDetailId, 2))
                personalizedVehicleUseByDetail = BuildPersonalizedComplementaryVehicleUseByDetail(campaignDosisDataMl)
            End If

            ' Procesamos cada producto o componente
            For Each item As ProductCalculateMixingStation In campaignDosisData

                Dim itemMg As ProductCalculateMixingStation = Nothing
                If Type = 2 AndAlso lookupMgByDetailItem IsNot Nothing AndAlso item.RequestMixingStationDetailId > 0 AndAlso item.ItemId > 0 Then
                    lookupMgByDetailItem.TryGetValue((item.RequestMixingStationDetailId, item.ItemId), itemMg)
                End If

                If Not item.ATCEntityId.HasValue Then
                    'Son insumos ó productos (No tienen ATC)
                    If Not (From e As ProductMixingStation In result Where e.ProductCode = item.Code AndAlso e.BatchCode = item.BatchCode Select e).Any() Then
                        AddProductMixingStation(result, item, item.CantidadDosis, item.QuantityPackage, unitDoseTypeClass)
                    End If
                    Continue For
                End If

                item.Concentration = If(item.Concentration = 0D, 1D, item.Concentration) 'NPT-Maquila

                ' Los componentes de materia prima y los paquetes personalizados ya traen del SP
                ' la dosis total normalizada. Se conservan como fracciones hasta consolidar la campaña.
                If item.ItemType = 3 OrElse item.IsPackagePersonalized Then
                    Dim hasComplementaryPersonalized = item.IsPackagePersonalized AndAlso item.HasComplementaryMedicine
                    Dim personalizedVehicleUse As Decimal = 0D
                    Dim useCalculatedVehicleQuantity = hasComplementaryPersonalized AndAlso
                                                       item.Vehicle > 0 AndAlso
                                                       item.RequestMixingStationDetailId.HasValue AndAlso
                                                       personalizedVehicleUseByDetail IsNot Nothing AndAlso
                                                       personalizedVehicleUseByDetail.TryGetValue(item.RequestMixingStationDetailId.Value, personalizedVehicleUse)
                    Dim dosisForPackage As Decimal = If(hasComplementaryPersonalized,
                                                         If(useCalculatedVehicleQuantity, personalizedVehicleUse, item.DosisRequerida),
                                                         If(Type = 2, item.DosisRequerida, item.QuantityPackage))
                    Dim requiredDose = CalculatePackageRequiredDose(item, itemMg, Type)
                    AddProductMixingStation(result, item, requiredDose, dosisForPackage, unitDoseTypeClass)
                    Continue For
                End If

                ' Para Type=2: usar unidad mg en el SP de inventario y datos mg para cantidadAtc (viales)
                Dim unitCodeForInventario = If(Type = 2 AndAlso itemMg IsNot Nothing, itemMg.CodigoUnidadPeso, item.CodigoUnidadPeso)
                'Consultamos los Productos asociados por el ATCEntityId
                Dim inventario = _RawMaterialRepository _
                    .ExecuteStoredProcedure(Of ProductCalculationCampaignDetail)("MixingStation.[SP_ProductListCampaingDetails]", {
                        ("@AtcId", item.ItemId),
                        ("@ATCEntityId", item.ATCEntityId),
                        ("@StockId", StockId),
                        ("@warehouseId", warehouseId),
                        ("@PharmaceuticalFormId", item.PharmaceuticalFormId.Value),
                        ("@RequestMeasurementUnitCode", unitCodeForInventario),
                        ("@Type", Type),
                        ("@MSClass", unitDoseTypeClass)
                    })?.ToList()

                If unitDoseTypeClass = EUnitDoseTypeClass.Repackaging Then 'Reempaque
                    Dim reqDosisReempaque = If(Type = 2 AndAlso itemMg IsNot Nothing, itemMg.DosisRequerida / If(itemMg.Concentration = 0D, 1D, itemMg.Concentration), item.DosisRequerida / item.Concentration)
                    AddProductMixingStation(result, item, reqDosisReempaque, item.Concentration, unitDoseTypeClass)
                    Continue For
                End If

                'Recorremos los Hijos para bucar la mejor opción que cumpla mi dosis requerida.  
                If inventario Is Nothing OrElse Not inventario.Any() OrElse item.ItemType = 4 Then
                    Dim dosisForPackage As Decimal = item.QuantityPackage
                    Dim requiredDose As Decimal
                    If Type = 2 AndAlso itemMg IsNot Nothing Then
                        requiredDose = itemMg.DosisRequerida / If(itemMg.Concentration = 0D, 1D, itemMg.Concentration)
                    Else
                        requiredDose = item.DosisRequerida / item.Concentration
                    End If
                    AddProductMixingStation(result, item, requiredDose, dosisForPackage, unitDoseTypeClass)
                    Continue For
                End If

                ' Para Type=2: usar DosisRequerida en mg para que cantidadAtc (viales) coincida con Type=1
                Dim restante As Decimal = If(Type = 2 AndAlso itemMg IsNot Nothing, itemMg.DosisRequerida, item.DosisRequerida)

                For Each atc In inventario
                    Dim cantidadAtc = Math.Floor(restante / atc.Concentration)
                    restante -= cantidadAtc * atc.Concentration

                    If inventario.IndexOf(atc) = inventario.Count - 1 AndAlso restante > 0 Then
                        If RawMaterialLeftovers(item.ItemId) < restante Then
                            cantidadAtc += 1
                            RawMaterialLeftovers(item.ItemId) += atc.Concentration - restante
                        Else
                            RawMaterialLeftovers(item.ItemId) -= restante
                        End If

                        restante = 0
                    End If

                    ' Eliminado: estos overrides sobrescribían el cantidadAtc correcto del inventario
                    ' y mezclaban unidades (mg vs mL). La lógica base del inventario ya calcula correctamente.

                    If cantidadAtc >= 0 Then
                        result.Add(New ProductMixingStation With {
                            .ItemId = atc.Id,
                            .ProductCode = atc.ProductCode,
                            .ProductName = atc.ProductCode & " - " & atc.ProductName,
                            .ProductShortName = atc.ProductAbbreviationName,
                            .OnlyNameProduct = atc.ProductName,
                            .QuantityMaterialRaw = cantidadAtc,
                            .Dosis = item.CantidadDosis,
                            .ItemType = item.ItemType,
                            .GroupName = item.GroupName.Trim(),
                            .TypeProduct = CType(item.TypeProduct, eTypeProduct),
                            .NameTypeProduct = item.NameTypeProduct.Trim(),
                            .RequiredDosis = cantidadAtc,
                            .DosisRequeridaPaquete = item.QuantityPackage,
                            .BatchCode = item.BatchCode,
                            .MeasureUnitAbbreviation = item.MeasureUnitAbbreviation,
                            .Concentration = If(unitDoseTypeClass = 2, item.Concentration, atc.Concentration),
                            .Thinner = item.Thinner,
                            .Vehicle = item.Vehicle,
                            .FormulationType = item.FormulationType,
                            .NPTItemOrder = item.NPTItemOrder,
                            .RequestMixingStationDetailId = item.RequestMixingStationDetailId,
                            .PackageId = item.PackageId,
                            .MSClass = unitDoseTypeClass,
                            .VerifiedFor = item.VerifiedFor,
                            .ConfirmedFor = item.ConfirmedFor,
                            .HasComplementaryMedicine = item.HasComplementaryMedicine,
                            .Source = item.Source
                        })

                    End If

                    If restante = 0 Then Exit For
                Next
            Next

            ' Agrupar por ItemId (ATC) e ItemType; para solicitudes de inventario separar por preparación.
            Dim res = result.
            GroupBy(Function(r) New With {
                Key .ItemId = If(r.ItemId.HasValue, r.ItemId.Value, 0),
                Key .ItemType = r.ItemType,
                Key .Source = If(r.Source = CByte(4), r.Source, CByte(0)),
                Key .RequestMixingStationDetailId = If(r.Source = CByte(4) AndAlso r.RequestMixingStationDetailId.HasValue, r.RequestMixingStationDetailId.Value, 0)
            }).
            Select(Function(grp)
                       Dim m = grp.AsEnumerable()
                       ' BuildBatchDataByType: excluir Thinner SOLO cuando Thinner y Vehicle son el mismo medicamento (mismo ItemId).
                       ' Si son medicamentos distintos, se mantiene el comportamiento normal (sumar ambos).
                       Dim bd As List(Of BatchData) = BuildBatchDataByType(m, Type)
                       Dim isPersonalized = m(0).IsPackagePersonalized
                       Dim hasComplementaryPersonalized = isPersonalized AndAlso m.Any(Function(x) x.HasComplementaryMedicine)
                       Dim batchQuantitySum = bd.Sum(Function(o) o.Quantity)
                       Dim reqDosis = Math.Ceiling(m.Sum(Function(o) o.RequiredDosis))
                       Dim totalToUseQuantity = If(hasComplementaryPersonalized,
                                                    CustomRound(m.Sum(Function(o) o.DosisRequeridaPaquete)),
                                                    If(isPersonalized, batchQuantitySum, batchQuantitySum * reqDosis))

                       Return New ProductMixingStation With {
                            .ItemId = grp.Key.ItemId,
                            .ProductCode = m(0).ProductCode,
                            .ProductName = m(0).ProductName,
                            .ProductShortName = m(0).ProductShortName,
                            .OnlyNameProduct = m(0).OnlyNameProduct,
                            .QuantityMaterialRaw = m.Sum(Function(o) o.QuantityMaterialRaw),
                            .Dosis = If(m.Any(Function(x) x.Thinner > 0) AndAlso m.Any(Function(x) x.Vehicle > 0), m.Where(Function(x) x.Thinner = 0).Sum(Function(o) o.Dosis), m.Sum(Function(o) o.Dosis)), ' Solo excluir Thinner cuando son mismo ItemId
                            .ItemType = grp.Key.ItemType,
                            .GroupName = m(0).GroupName,
                            .TypeProduct = m(0).TypeProduct,
                            .FormulationType = m(0).FormulationType,
                            .NPTItemOrder = m(0).NPTItemOrder,
                            .NameTypeProduct = m(0).NameTypeProduct,
                            .RequiredDosis = reqDosis,
                            .BatchData = bd,
                            .TotalToUseQuantity = totalToUseQuantity, ' Para paquetes personalizados, la cantidad ya está calculada, no multiplicar por RequiredDosis
                            .MeasureUnitAbbreviation = m(0).MeasureUnitAbbreviation,
                            .Concentration =
                                 If(unitDoseTypeClass <> EUnitDoseTypeClass.ParenteralNutrition,
                                    If(Type = 2 AndAlso .ItemType = CByte(1),
                                       If(Math.Ceiling(batchQuantitySum) > m(0).Concentration,
                                          Convert.ToDecimal(m(0).Concentration),
                                          Convert.ToDecimal(Math.Ceiling(batchQuantitySum))),
                                       Convert.ToDecimal(m(0).Concentration)),
                                    Convert.ToDecimal(m(0).Concentration)),
                            .Vehicle = m(0).Vehicle,
                            .Thinner = m(0).Thinner,
                            .RequestMixingStationDetailId = m(0).RequestMixingStationDetailId,
                            .PackageId = m(0).PackageId,
                            .MSClass = unitDoseTypeClass,
                            .VerifiedFor = m(0).VerifiedFor,
                            .ConfirmedFor = m(0).ConfirmedFor,
                            .IsPackagePersonalized = isPersonalized,
                            .HasComplementaryMedicine = hasComplementaryPersonalized,
                            .Source = m(0).Source
                        }
                   End Function).
            ToList()

            ValidatePackageRawMaterialCoverage(campaignDosisData, res, Type, lookupMgByDetailItem)

            If unitDoseTypeClass = EUnitDoseTypeClass.ParenteralNutrition Then
                Return res.OrderBy(Function(x) x.NPTItemOrder).ToList()
            Else
                Return res.OrderBy(Function(x) x.OnlyNameProduct).ToList()
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Calcula las unidades fraccionarias requeridas por un componente de paquete.
    ''' La dosis ya normalizada por el SP se divide por la capacidad de la presentación del ATC.
    ''' </summary>
    ''' <param name="item">Componente calculado para la preparación.</param>
    ''' <param name="itemMg">Componente equivalente en unidad de peso cuando el documento se consulta en mL.</param>
    ''' <param name="documentType">Tipo de cálculo solicitado: peso o volumen.</param>
    ''' <returns>Unidades fraccionarias necesarias antes de consolidar y redondear la campaña.</returns>
    Private Function CalculatePackageRequiredDose(item As ProductCalculateMixingStation, itemMg As ProductCalculateMixingStation, documentType As Integer) As Decimal
        Dim dose = If(documentType = 2 AndAlso itemMg IsNot Nothing, itemMg.DosisRequerida, item.DosisRequerida)
        Dim concentration = If(documentType = 2 AndAlso itemMg IsNot Nothing, itemMg.Concentration, item.Concentration)
        Return dose / If(concentration = 0D, 1D, concentration)
    End Function

    ''' <summary>
    ''' Verifica que la consolidación final cubra la dosis completa de todas las preparaciones de la campaña.
    ''' </summary>
    ''' <param name="campaignDosisData">Componentes de todas las preparaciones retornados por el SP.</param>
    ''' <param name="calculatedProducts">Productos consolidados que serán mostrados y enviados al picking.</param>
    ''' <param name="documentType">Tipo de cálculo solicitado: peso o volumen.</param>
    ''' <param name="lookupMgByDetailItem">Equivalencias en peso utilizadas para consultas en mL.</param>
    Private Sub ValidatePackageRawMaterialCoverage(campaignDosisData As IEnumerable(Of ProductCalculateMixingStation), calculatedProducts As IEnumerable(Of ProductMixingStation), documentType As Integer, lookupMgByDetailItem As Dictionary(Of (Integer?, Integer), ProductCalculateMixingStation))
        Dim requirements = campaignDosisData.
            Where(Function(x) x.ItemType = 3 OrElse x.IsPackagePersonalized).
            GroupBy(Function(x) New With {Key .ItemId = x.ItemId, Key .ItemType = x.ItemType})

        For Each requirement In requirements
            Dim requiredUnits = Math.Ceiling(requirement.Sum(Function(item)
                                                                 Dim itemMg As ProductCalculateMixingStation = Nothing
                                                                 If documentType = 2 AndAlso lookupMgByDetailItem IsNot Nothing Then
                                                                     lookupMgByDetailItem.TryGetValue((item.RequestMixingStationDetailId, item.ItemId), itemMg)
                                                                 End If
                                                                 Return CalculatePackageRequiredDose(item, itemMg, documentType)
                                                             End Function))
            Dim calculatedUnits = calculatedProducts.
                Where(Function(x) x.ItemId = requirement.Key.ItemId AndAlso x.ItemType = requirement.Key.ItemType).
                Sum(Function(x) x.RequiredDosis)

            If calculatedUnits < requiredUnits Then
                Dim component = requirement.First()
                Throw New IndigoValidationException($"La cantidad calculada para {component.Code} - {component.Name} no cubre la dosis total requerida por las preparaciones de la campaña. Requerida: {requiredUnits}; calculada: {calculatedUnits}.")
            End If
        Next
    End Sub

    ''' <summary>
    ''' Calcula BatchData por lote
    ''' </summary>
    ''' <returns>Lista de Lotes con cantidad y unidad de medida</returns>
    Private Function BuildBatchDataByType(m As IEnumerable(Of ProductMixingStation), docType As Integer) As List(Of BatchData)
        Dim list As New List(Of BatchData)
        Dim mList = m.AsEnumerable().ToList()

        ' SOLO cuando Thinner y Vehicle son el mismo medicamento (mismo ItemId en el grupo): usar solo Vehicle.
        ' Si son medicamentos distintos, hasThinner AndAlso hasVehicle no se cumple y se mantiene comportamiento normal.
        Dim hasThinner As Boolean = mList.Any(Function(x) x.Thinner > 0)
        Dim hasVehicle As Boolean = mList.Any(Function(x) x.Vehicle > 0)

        ' Obtener todos los BatchCodes y separarlos si vienen concatenados con comas
        Dim allBatchCodes As New HashSet(Of String)()

        For Each item In mList
            ' Separar los lotes si vienen concatenados con comas (STRING_AGG del SP)
            Dim batchCodesArray = If(item.BatchCode, "").Split(New String() {", ", ","}, StringSplitOptions.RemoveEmptyEntries)
            For Each Batch In batchCodesArray
                allBatchCodes.Add(Batch.Trim()) 'HashSet auto-elimina duplicados
            Next
        Next

        ' Si no hay códigos de lote, agregar una entrada vacía
        If Not allBatchCodes.Any() Then
            allBatchCodes.Add(String.Empty)
        End If

        Dim itemBatches = mList.ToDictionary(
            Function(x) x,
            Function(x) x.BatchCode?.Split(New String() {", ", ","}, StringSplitOptions.RemoveEmptyEntries).Select(Function(b) b.Trim()).ToArray())

        ' Procesar cada lote individualmente
        For Each Batch In allBatchCodes
            Dim matches As IEnumerable(Of ProductMixingStation)

            If String.IsNullOrEmpty(Batch) Then
                matches = mList.Where(Function(x) String.IsNullOrEmpty(x.BatchCode))
            Else
                matches = itemBatches.Where(Function(kvp) kvp.Value IsNot Nothing AndAlso kvp.Value.Contains(Batch)).Select(Function(x) x.Key)
            End If

            ' Solo cuando Thinner+Vehicle mismo ItemId: excluir Thinner. Si medicamentos distintos, sumar todos.
            Dim matchesForQty = If(hasThinner AndAlso hasVehicle, matches.Where(Function(x) x.Thinner = 0), matches)

            Dim quantity As Decimal
            Dim mu As String

            ' Paquetes personalizados: usar Dosis (CantidadDosis del SP) = cantidad ya en los paquetes
            ' Otros: usar DosisRequeridaPaquete
            Dim firstItem = mList.FirstOrDefault()
            Dim isPersonalized As Boolean = firstItem IsNot Nothing AndAlso firstItem.IsPackagePersonalized
            quantity = CustomRound(If(isPersonalized, matchesForQty.Sum(Function(x) x.Dosis), matchesForQty.Sum(Function(x) x.DosisRequeridaPaquete)))
            mu = matches.Select(Function(x) x.MeasureUnitAbbreviation).FirstOrDefault()

            ' Agregar el lote solo si tiene cantidad
            If quantity > 0 OrElse String.IsNullOrEmpty(Batch) Then
                list.Add(New BatchData With {
                    .BatchCode = If(String.IsNullOrEmpty(Batch), Nothing, Batch),
                    .Quantity = quantity,
                    .MeasurementUnit = mu
                })
            End If
        Next

        Return list
    End Function

    ''' <summary>
    ''' Calcula el volumen real de vehículo a utilizar para paquetes personalizados con medicamento complementario.
    ''' El valor se obtiene restando del volumen final del vehículo el volumen en mL de los medicamentos principales y complementarios.
    ''' </summary>
    ''' <param name="campaignDosisDataMl">Datos de la campaña calculados en mL.</param>
    ''' <returns>Diccionario por RequestMixingStationDetailId con el volumen neto del vehículo a utilizar.</returns>
    Private Function BuildPersonalizedComplementaryVehicleUseByDetail(campaignDosisDataMl As List(Of ProductCalculateMixingStation)) As Dictionary(Of Integer, Decimal)
        Dim result As New Dictionary(Of Integer, Decimal)
        If campaignDosisDataMl Is Nothing Then Return result

        Dim detailGroups = campaignDosisDataMl.
            Where(Function(x) x.IsPackagePersonalized AndAlso x.HasComplementaryMedicine AndAlso x.RequestMixingStationDetailId.HasValue).
            GroupBy(Function(x) x.RequestMixingStationDetailId.Value)

        For Each detailGroup In detailGroups
            Dim vehicleItems = detailGroup.Where(Function(x) x.Vehicle > 0).ToList()
            If Not vehicleItems.Any() Then Continue For

            Dim finalVehicleVolume = vehicleItems.Max(Function(x) If(x.DosisRequerida > 0D, x.DosisRequerida, x.Concentration))
            If finalVehicleVolume <= 0D Then Continue For

            Dim medicineVolume = detailGroup.
                Where(Function(x) x.ItemType = CByte(1) AndAlso x.Vehicle = 0 AndAlso x.Thinner = 0).
                Sum(Function(x) x.CantidadDosis)

            result(detailGroup.Key) = CustomRound(Math.Max(0D, finalVehicleVolume - medicineVolume))
        Next

        Return result
    End Function

    ''' <summary>
    ''' Se encarga de obtener el tipo de dosis unitaria para la campaña
    ''' </summary>
    Private Async Function GetUnitDoseTypeClass(CampaignDetailId As Integer) As Task(Of Integer)
        Return Await _CampaignDetailRepository _
        .Query(Function(m) m.Id = CampaignDetailId, includes:={"UnidDoseType"}) _
        .Select(Function(m) m.UnitDoseType.MSClass) _
        .FirstOrDefaultAsync()
    End Function

    ''' <summary>
    ''' Se encarga de obtener los datos de la campaña por dosis
    ''' </summary>
    Private Function GetCampaignDosisData(CampaignDetailId As Integer, Type As Integer) As List(Of ProductCalculateMixingStation)
        Dim campaignDosisData = _RawMaterialRepository.ExecuteStoredProcedure(Of ProductCalculateMixingStation)("MixingStation.[SP_ProductListCampaing]", {("@CampaignDetailId", CampaignDetailId), ("@Type", Type)})
        Return campaignDosisData?.Select(Function(m)
                                             m.BatchCodes = m.BatchCode?.Split(CChar("|")).ToList()
                                             Return m
                                         End Function).ToList()
    End Function

    ''' <summary>
    ''' Se encarga de conservar los Ids de los atc de la campaña
    ''' </summary>
    Private Function AggregateRawMaterialLeftovers(campaignDosisData As List(Of ProductCalculateMixingStation)) As Dictionary(Of Integer, Decimal)
        Dim RawMaterialLeftovers As New Dictionary(Of Integer, Decimal)
        If campaignDosisData IsNot Nothing Then
            For Each item As ProductCalculateMixingStation In campaignDosisData
                If Not RawMaterialLeftovers.ContainsKey(item.ItemId) Then
                    RawMaterialLeftovers.Add(item.ItemId, 0)
                End If
            Next
        End If

        Return RawMaterialLeftovers
    End Function

    ''' <summary>
    ''' Obtiene la cantidad para el reconstituyente
    ''' </summary>
    Private Function GetThinnerQuantity(item As ProductCalculateMixingStation, Type As Integer, campaignDosisData As List(Of ProductCalculateMixingStation)) As Decimal
        If (Type = 2 And item.ItemType = 1 And item.FormulationType = 1 AndAlso item.PackageId IsNot Nothing) OrElse
       (Type = 1 And item.ItemType = 1 And item.FormulationType = 3 AndAlso item.PackageId IsNot Nothing) Then
            Dim thinnerItem = campaignDosisData.FirstOrDefault(Function(x) x.Thinner = CByte(1) AndAlso x.PackageId.Value = item.PackageId.Value)
            If thinnerItem IsNot Nothing Then
                Return thinnerItem.QuantityPackage
            End If
        End If
        Return 0
    End Function

    ''' <summary>
    ''' Crea el objeto ProductMixingStation
    ''' </summary>
    Private Sub AddProductMixingStation(ByRef result As List(Of ProductMixingStation), item As ProductCalculateMixingStation, requiredDose As Decimal, requiredDoseByPackage As Decimal, unitDoseTypeClass As Integer) ', QuantityThinner As Integer, unidDoseTypeClass As Integer)
        If result Is Nothing Then
            result = New List(Of ProductMixingStation)
        End If

        result.Add(New ProductMixingStation With {
            .ItemId = item.ItemId,
            .ProductCode = item.Code.Trim(),
            .ProductName = item.Code & " - " & item.Name.Trim(),
            .OnlyNameProduct = item.Name,
            .QuantityMaterialRaw = 0,
            .Dosis = item.CantidadDosis,
            .ItemType = item.ItemType,
            .GroupName = item.GroupName.Trim(),
            .TypeProduct = CType(item.TypeProduct, eTypeProduct),
            .NameTypeProduct = item.NameTypeProduct.Trim(),
            .RequiredDosis = requiredDose,
            .DosisRequeridaPaquete = requiredDoseByPackage,
            .BatchCode = item.BatchCode,
            .MeasureUnitAbbreviation = item.MeasureUnitAbbreviation,
            .Concentration = item.Concentration,
            .FormulationType = item.FormulationType,
            .NPTItemOrder = item.NPTItemOrder,
            .RequestMixingStationDetailId = item.RequestMixingStationDetailId,
            .MSClass = unitDoseTypeClass,
            .VerifiedFor = item.VerifiedFor,
            .ConfirmedFor = item.ConfirmedFor,
            .IsPackagePersonalized = item.IsPackagePersonalized,
            .HasComplementaryMedicine = item.HasComplementaryMedicine,
            .Thinner = item.Thinner,
            .Vehicle = item.Vehicle,
            .Source = item.Source
        })
    End Sub

    ''' <summary>
    ''' Función para redondear valores a 2 decimales si son mayores o iguales a 0.001,
    ''' y a 4 decimales si son menores a 0.001.
    ''' Ajuste realizado por PBI #22105
    ''' </summary>
    ''' <param name="Value">Valor a redondear</param>
    ''' <returns>El valor redondeado</returns>
    Function CustomRound(Value As Decimal) As Decimal
        Dim decimals As Integer = If(Value >= 0.001D, 2, 4)
        ' Calcular el factor de multiplicación basado en los decimales
        Dim factor As Decimal = CDec(Math.Pow(10, decimals))
        ' Truncar el valor a los decimales especificados
        Return Decimal.Truncate(Value * factor) / factor
        ''Return CDec(formattedValue)
    End Function
    ''' <summary>
    ''' Guardar los componentes de la canasta que seleccionan en el formulario de materia prima
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveBasketsMateriaRawAsync(
        CampaignDetailId As Integer,
        campaignId As Integer,
        ProductionBasketsId As Integer,
        audit As AuditMessage,
        session As SessionValues) As Task(Of ActionResult) Implements IRawMaterialAdminService.SaveBasketsMateriaRawAsync

        Dim uowCampaing As IUnitWork = Me._CampaignRepository.UnitWork

        Try
            Using scope As New TransactionScope(
            TransactionScopeOption.Required,
            New TransactionOptions() With {
                .Timeout = TransactionManager.MaximumTimeout,
                .IsolationLevel = IsolationLevel.ReadCommitted
            },
            TransactionScopeAsyncFlowOption.Enabled)

                ' Actualizar datos de la canasta en el detalle de campaña
                Dim campaignDetail = _CampaignDetailRepository.FirstOrDefault(Function(m) m.Id = CampaignDetailId)
                If campaignDetail Is Nothing Then
                    Return New ActionResult With {.StateResult = False, .Message = "No se encontró el detalle de campaña"}
                End If

                campaignDetail.ProductionBasketId = ProductionBasketsId
                campaignDetail.BasketUser = audit.CodeUser
                campaignDetail.BasketDate = Date.Now
                campaignDetail.MarkAsModified()

                _CampaignDetailRepository.SaveEntity(campaignDetail)
                Await uowCampaing.CommitAsync()

                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    '''  Guarda la Materia prima Adicional a la Campaña
    ''' </summary>
    ''' <param name="productCampaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveProductCampaignDetailAsync(
        productCampaignDetail As CampaignDetailItems,
        audit As AuditMessage) As Task(Of ActionResult) Implements IRawMaterialAdminService.SaveProductCampaignDetailAsync

        Try
            Using scope As New TransactionScope(
            TransactionScopeOption.Required,
            New TransactionOptions With {
                .Timeout = TransactionManager.MaximumTimeout,
                .IsolationLevel = IsolationLevel.ReadCommitted
            },
            TransactionScopeAsyncFlowOption.Enabled
        )
                ' Validación de existencia de campaña
                Dim CampaignDetailId As Integer = productCampaignDetail.CampaignDetailId
                If CampaignDetailId = 0 Then
                    Throw New IndigoValidationException("No se encontró Campaña asociada al producto")
                End If

                ' Buscar existencia previa (solo un match por tipo)
                Dim campaignDetailItem As CampaignDetailItems = Nothing
                If productCampaignDetail.AtcId.HasValue Then
                    campaignDetailItem = _CampaignDetailItemsRepository.FirstOrDefault(Function(m) m.AtcId = productCampaignDetail.AtcId And m.CampaignDetailId = CampaignDetailId And m.ItemType = 4)
                ElseIf productCampaignDetail.ProductId.HasValue Then
                    campaignDetailItem = _CampaignDetailItemsRepository.FirstOrDefault(Function(m) m.ProductId = productCampaignDetail.ProductId And m.CampaignDetailId = CampaignDetailId And m.ItemType = 4)
                ElseIf productCampaignDetail.SupplyId.HasValue Then
                    campaignDetailItem = _CampaignDetailItemsRepository.FirstOrDefault(Function(m) m.SupplyId = productCampaignDetail.SupplyId And m.CampaignDetailId = CampaignDetailId And m.ItemType = 4)
                End If

                If campaignDetailItem IsNot Nothing Then
                    campaignDetailItem.RequestQuantity += productCampaignDetail.RequestQuantity
                    campaignDetailItem.MarkAsModified()
                    _CampaignDetailItemsRepository.SaveEntity(campaignDetailItem)
                Else
                    _CampaignDetailItemsRepository.SaveEntity(productCampaignDetail)
                End If

                Await _CampaignDetailItemsRepository.UnitWork.CommitAsync()
                scope.Complete()

                Return New ActionResult With {.StateResult = True}
            End Using

        Catch ex As OptimisticConcurrencyException
            Return New ActionResult() With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {
                .StateResult = False,
                .MessageResult = {ex.Message}.ToList,
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

    ''' <summary>
    ''' Se encarga de eliminar la materia prima adicional de la campaña
    ''' </summary>
    Private Async Function DeleteProductCampaignDetailAsync(productCampaignDetail As CampaignDetailItems, audit As AuditMessage) As Task(Of ActionResult) Implements IRawMaterialAdminService.DeleteProductCampaignDetailAsync
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)

                productCampaignDetail.MarkAsDeleted()
                Me._CampaignDetailItemsRepository.SaveEntity(productCampaignDetail)
                Await _CampaignDetailItemsRepository.UnitWork.CommitAsync()

                scope.Complete()

                Return New ActionResult With {.StateResult = True, .Message = "Detalle eliminado correctamente"}
            End Using
        Catch ex As Exception
            Return New ActionResult() With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = String.Concat("No se pudo eliminar la solicitud manual", " - ", Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

#Region "Picking Dosis Unitarias"
    ''' <summary>
    ''' Calcula el picking de materia prima consultando disponibilidad en almacenes (Stock, Warehouse, Maquila).
    ''' Para tipo reempaque/reenvase con solicitud de inventario (Source=4), consulta únicamente el almacén de RequestUnitDoseInventory.
    ''' </summary>
    ''' <param name="productsMixing">Lista de productos de la central de mezclas a preparar.</param>
    ''' <param name="campaignDetailId">Id del detalle de campaña.</param>
    ''' <param name="stockWarehouseId">Id del almacén de stock de la central de mezclas.</param>
    ''' <param name="warehouseId">Id del almacén adicional (opcional).</param>
    ''' <param name="maquilaWareHouseId">Id del almacén maquila (opcional, para centros que aplican excepción).</param>
    ''' <param name="RemnantWarehouseId">Id del almacén de remanentes.</param>
    ''' <param name="audit">Información de auditoría.</param>
    ''' <param name="session">Valores de sesión del usuario.</param>
    ''' <returns>ActionResult con los detalles de campaña generados o mensaje de error.</returns>
    Private Async Function CalculatePickingAsync(
        ByVal productsMixing As List(Of ProductMixingStation),
        campaignDetailId As Integer,
        stockWarehouseId As Integer,
        warehouseId As Integer?,
        maquilaWareHouseId As Integer?,
        RemnantWarehouseId As Integer,
        audit As AuditMessage,
        session As SessionValues) As Task(Of ActionResult(Of CampaignDetailItems)) Implements IRawMaterialAdminService.CalculatePickingAsync

        Try
            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted},
                TransactionScopeAsyncFlowOption.Enabled)
                ' Validación de parámetros
                If productsMixing Is Nothing OrElse Not productsMixing.Any() Then
                    Return New ActionResult(Of CampaignDetailItems) With {
                        .StateResult = False,
                        .Message = "No se han proporcionado productos para calcular el picking"
                    }
                End If
                If audit Is Nothing Then
                    Return New ActionResult(Of CampaignDetailItems) With {
                        .StateResult = False,
                        .Message = "No se ha proporcionado la información de auditoría"
                    }
                End If

                ' Validación de almacenes críticos
                If stockWarehouseId = 0 OrElse RemnantWarehouseId = 0 Then
                    Return New ActionResult(Of CampaignDetailItems) With {
                        .StateResult = False,
                        .Message = "Debe tener parametrizado el almacen de Stock y de Remanente para llevar acabo este proceso"
                    }
                End If

                ' Limpiar detalles antiguos de campaña
                Dim oldCampaigDetails = _CampaignDetailItemsRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId)
                If oldCampaigDetails?.Any() Then _CampaignDetailItemsRepository.DeleteList(oldCampaigDetails.ToList())
                Dim oldPicking = _PickingRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId)
                If oldPicking?.Any() Then _PickingRepository.DeleteList(oldPicking.ToList())
                Dim oldCampaignDetailBasket = _campaignDetailBasketDetailRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId)
                If oldCampaignDetailBasket?.Any() Then _campaignDetailBasketDetailRepository.DeleteList(oldCampaignDetailBasket.ToList())

                Dim detailsItems As New List(Of CampaignDetailItems)()
                Dim detailsPicking As New List(Of CampaignDetailPicking)()
                Dim notfound As New StringBuilder()

                ' Agrega detalles de canastilla de producción (productos de tipo 2)
                AddProductionBasketDetails(campaignDetailId, productsMixing.Where(Function(x) x.ItemType = 2).ToList(), audit)

                ' Excepciones y unidad de dosis
                Dim exceptions = _contractExternalClientsDetail.GetRawMaterialExceptionsByCampaignDetailId(campaignDetailId)
                Dim unitDoseTypeClass = _CampaignDetailRepository.Query(
                Function(m) m.Id = campaignDetailId,
                includes:={"UnitDoseType"}).Select(Function(m) m.UnitDoseType.MSClass).FirstOrDefault()

                For Each productMixing As ProductMixingStation In productsMixing
                    Dim detailItem As New CampaignDetailItems With {
                            .CampaignDetailId = campaignDetailId,
                            .RequestQuantity = CInt(productMixing.RequiredDosis),
                            .ItemType = productMixing.ItemType
                            }
                    Dim applyExceptions As ContractExternalClientsDetail = Nothing

                    ' Set detalle según tipo de producto
                    Select Case productMixing.TypeProduct
                        Case ProductMixingStation.eTypeProduct.Atc
                            detailItem.AtcCodeName = productMixing.ProductName
                            detailItem.AtcId = productMixing.ItemId
                            applyExceptions = exceptions.Find(Function(m) m.AtcId.HasValue AndAlso m.AtcId.Value = productMixing.ItemId)
                        Case ProductMixingStation.eTypeProduct.Supply
                            detailItem.SupplyCodeName = productMixing.ProductName
                            detailItem.SupplyId = productMixing.ItemId
                            applyExceptions = exceptions.Find(Function(m) m.SupplieId.HasValue AndAlso m.SupplieId.Value = productMixing.ItemId)
                        Case ProductMixingStation.eTypeProduct.Product
                            detailItem.ProductCodeName = productMixing.ProductName
                            detailItem.ProductId = productMixing.ItemId
                            applyExceptions = exceptions.Find(Function(m) m.ProductId.HasValue AndAlso m.ProductId.Value = productMixing.ItemId)
                    End Select

                    Dim pendingQuantity = detailItem.RequestQuantity
                    Dim stockInventory As List(Of PhysicalInventory) = Nothing
                    Dim warehouseInventory As List(Of PhysicalInventory) = Nothing

                    ' Solicitud de inventario (Source=4): toma primero del stock; si no cubre la cantidad requerida,
                    ' completa las unidades restantes con el almacén asignado en la solicitud. No cae al flujo estándar.
                    If productMixing.Source = 4 Then

                        stockInventory = Await _PickingRepository.GetProductsByAtcAndWarehouseAsync(detailItem.AtcId, detailItem.SupplyId, detailItem.ProductId, stockWarehouseId)
                        Dim pickingList = processInventory(eWareHouseType.Stock, campaignDetailId, stockInventory, pendingQuantity, detailItem, productMixing.ItemType, audit.CodeUser, detailsPicking)
                        detailsPicking.AddRange(pickingList)

                        If pendingQuantity = 0 Then
                            detailsItems.Add(detailItem)
                            Continue For
                        End If

                        Dim requestMixingStationDetailId As Integer = If(productMixing.RequestMixingStationDetailId.HasValue, productMixing.RequestMixingStationDetailId.Value, 0)
                        If requestMixingStationDetailId = 0 Then
                            notfound.AppendLine($"- {productMixing.ProductName}")
                            Continue For
                        End If

                        ' Cantidades restantes: consulta el almacén asignado a esta preparación desde RequestMixingStationDetail
                        Dim warehouseRequestInventoryId As Integer = Await _PickingRepository.GetInventoryRequestWarehouseIdByMixingStationDetailAsync(requestMixingStationDetailId)

                        If warehouseRequestInventoryId > 0 Then
                            Dim solicitudWarehouse = Await _wareHouseRepository.GetWarehouseByIdAsync(warehouseRequestInventoryId)
                            Dim almacenOrigenLabel As String = String.Empty
                            If solicitudWarehouse IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(solicitudWarehouse.Name) Then
                                almacenOrigenLabel = $"{solicitudWarehouse.Code} - {solicitudWarehouse.Name}"
                            End If

                            warehouseInventory = Await _PickingRepository.GetProductsByAtcAndWarehouseAsync(detailItem.AtcId, detailItem.SupplyId, detailItem.ProductId, warehouseRequestInventoryId)
                            If warehouseInventory IsNot Nothing AndAlso warehouseInventory.Any() Then
                                Dim pickingListInv = processInventory(eWareHouseType.WareHouse, campaignDetailId, warehouseInventory, pendingQuantity, detailItem, productMixing.ItemType, audit.CodeUser, detailsPicking)
                                detailsPicking.AddRange(pickingListInv)
                                detailsItems.Add(detailItem)
                            End If

                            If pendingQuantity > 0 Then
                                notfound.AppendLine($"- {productMixing.ProductName} ({almacenOrigenLabel}).")
                            End If
                        Else
                            notfound.AppendLine($"- {productMixing.ProductName}")
                        End If
                        Continue For ' No caer al flujo estándar bajo ninguna circunstancia

                    Else

                        ' Excepción: Maquila
                        If maquilaWareHouseId.HasValue AndAlso applyExceptions IsNot Nothing Then
                            warehouseInventory = Await _PickingRepository.GetProductsByAtcAndWarehouseAsync(detailItem.AtcId, detailItem.ProductId, detailItem.SupplyId, maquilaWareHouseId)
                            Dim pickingWareList = processInventory(eWareHouseType.Maquila, campaignDetailId, warehouseInventory, pendingQuantity, detailItem, productMixing.ItemType, audit.CodeUser)
                            detailsPicking.AddRange(pickingWareList)

                            ' Si el centro maneja almacén maquila y aplica excepción
                            If applyExceptions.SuppliedBy = 1 Then
                                detailsItems.Add(detailItem)
                                If warehouseInventory Is Nothing OrElse warehouseInventory.Count = 0 Then
                                    notfound.AppendLine($"- {productMixing.ProductName}")
                                End If
                                Continue For
                            End If
                        End If

                        ' Inventario principal stock
                        stockInventory = Await _PickingRepository.GetProductsByAtcAndWarehouseAsync(detailItem.AtcId, detailItem.SupplyId, detailItem.ProductId, stockWarehouseId)
                        Dim pickingList = processInventory(eWareHouseType.Stock, campaignDetailId, stockInventory, pendingQuantity, detailItem, productMixing.ItemType, audit.CodeUser)
                        detailsPicking.AddRange(pickingList)

                        If pendingQuantity = 0 Then
                            detailsItems.Add(detailItem)
                            Continue For
                        End If

                        ' Inventario warehouse adicional
                        If warehouseId.HasValue Then
                            warehouseInventory = Await _PickingRepository.GetProductsByAtcAndWarehouseAsync(detailItem.AtcId, detailItem.SupplyId, detailItem.ProductId, warehouseId)
                            Dim pickingWareList = processInventory(eWareHouseType.WareHouse, campaignDetailId, warehouseInventory, pendingQuantity, detailItem, productMixing.ItemType, audit.CodeUser)
                            detailsPicking.AddRange(pickingWareList)
                        End If

                    End If

                    detailsItems.Add(detailItem)
                Next

                ' Guardar detalles
                If detailsItems?.Any() Then
                    Await _CampaignDetailItemsRepository.SaveEntityMassiveAsync(detailsItems)
                End If
                If detailsPicking?.Any() Then
                    Await Me._PickingRepository.SaveEntityMassiveAsync(detailsPicking)
                End If

                If notfound.Length > 0 Then
                    notfound.Insert(0, $"* La materia prima requerida no está disponible en el almacén del cliente: {vbCrLf}")
                    Return New ActionResult(Of CampaignDetailItems) With {.StateResult = False, .Message = notfound.ToString()}
                End If

                Dim UpdateCampaignDetail = Await UpdateStatusCampaignDetailAsync(campaignDetailId, 2, session)
                If Not UpdateCampaignDetail.StateResult Then
                    Return New ActionResult(Of CampaignDetailItems) With {.StateResult = False, .Message = UpdateCampaignDetail.Message}
                End If

                scope.Complete()
                Return New ActionResult(Of CampaignDetailItems) With {.StateResult = True, .Data = detailsItems}
            End Using

        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of CampaignDetailItems) With {
                .StateResult = False,
                .Message = ResourceManager.GetString("ErrorConcurrence")
            }
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of CampaignDetailItems) With {
                .StateResult = False,
                .MessageResult = {ex.Message}.ToList,
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

    ''' <summary>
    ''' agrega los detalles de la canasta de produccion
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="BasketItems"></param>
    ''' <param name="audit"></param>
    Private Sub AddProductionBasketDetails(campaignDetailId As Integer, BasketItems As List(Of ProductMixingStation), audit As AuditMessage)
        'Consultamos productos canasta de la canasta
        Dim campaignDetail = _CampaignDetailRepository.FirstOrDefault(Function(m) m.Id = campaignDetailId)

        If Not campaignDetail.ProductionBasketId.HasValue Then Return

        Dim productionBaskets As ProductionBaskets = Me._ProductionBasketsRepository.GetProductionBasketsById(campaignDetail.ProductionBasketId)
        Dim auxProductionBaskets As CampaignDetailBasketDetail = Nothing

        'Asignamos productos a la nueva Entidad.
        If productionBaskets.ProductionBasketsDetail.Any() Then
            productionBaskets.ProductionBasketsDetail.ToList() _
                .ForEach(Sub(seq As ProductionBasketsDetail)

                             If BasketItems.Any() AndAlso Not BasketItems.Exists(Function(x) x.ItemId.Value = If(seq.AtcId, If(seq.SupplieId, seq.ProductId.Value))) Then
                                 Exit Sub
                             End If

                             Dim campaignDetailBasketDetail As New CampaignDetailBasketDetail
                             With campaignDetailBasketDetail
                                 .CampaignDetailId = campaignDetailId
                                 .ComponentType = seq.ComponentType
                                 .ProductId = seq.ProductId
                                 .AtcId = seq.AtcId
                                 .SupplieId = seq.SupplieId
                                 .Quantity = seq.Quantity
                                 .MeasurementUnitId = seq.MeasurementUnitId
                             End With

                             _RawMaterialRepository.SaveEntity(campaignDetailBasketDetail)
                             Dim auditProcess2 As New IndigoAuditSimpleEntity(Of CampaignDetailBasketDetail)(campaignDetailBasketDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, auxProductionBaskets)
                             auditProcess2.Execute()
                         End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Devuele Materia prima en estado Picking 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function ListRawMaterialItems(campaignDetailId As Integer, session As SessionValues) As List(Of CampaignDetailItems) Implements IRawMaterialAdminService.ListRawMaterialItems
        Dim items = _CampaignDetailItemsRepository.GetProductsItems(campaignDetailId)
        Return items
    End Function

    ''' <summary>
    ''' Devuele Materia prima en estado Validacion 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Function ListRawMaterialItemsValidation(campaignDetailId As Integer, session As SessionValues) As List(Of CampaignDetailItems) Implements IRawMaterialAdminService.ListRawMaterialItemsValidation
        Return _CampaignDetailItemsRepository.GetProductsItemsValidation(campaignDetailId)
    End Function

    ''' <summary>
    ''' Procesa el inventario físico y genera los registros de picking para el detalle de campaña.
    ''' Distribuye las cantidades según el tipo de almacén (Stock, Warehouse, Maquila, Remnant).
    ''' </summary>
    ''' <param name="typeWarehouse">Tipo de almacén: 1-Stock, 2-Warehouse, 3-Maquila, 4-Remanentes</param>
    ''' <param name="campaignDetailId">Identificador del detalle de campaña</param>
    ''' <param name="listInventory">Lista de inventario físico disponible. Si es Nothing o vacía, retorna lista vacía</param>
    ''' <param name="pendingQuantity">Cantidad pendiente por cubrir (ByRef: se reduce conforme se asigna)</param>
    ''' <param name="detailItem">Detalle del ítem de campaña donde se actualizan las cantidades por tipo de almacén</param>
    ''' <param name="itemType">Tipo de ítem (ATC, Supply, Product)</param>
    ''' <param name="codeUser">Código del usuario que realiza el proceso</param>
    ''' <param name="currentPicking">Picking generado previamente en la misma ejecución para descontarlo del inventario disponible</param>
    ''' <returns>Lista de registros CampaignDetailPicking generados</returns>
    Private Function processInventory(
        typeWarehouse As eWareHouseType,
        campaignDetailId As Integer,
        listInventory As List(Of PhysicalInventory),
        ByRef pendingQuantity As Integer,
        ByRef detailItem As CampaignDetailItems,
        itemType As Integer,
        codeUser As String,
        Optional currentPicking As List(Of CampaignDetailPicking) = Nothing
    ) As List(Of CampaignDetailPicking)
        Dim pickingList As New List(Of CampaignDetailPicking)()

        If listInventory Is Nothing OrElse Not listInventory.Any() OrElse pendingQuantity <= 0 Then
            Return pickingList
        End If

        If detailItem Is Nothing Then
            Return pickingList
        End If

        DiscountAlreadyPickedInventory(listInventory, currentPicking)

        For Each physicalProduct As PhysicalInventory In listInventory.Where(Function(x) x.Quantity > 0)
            Dim quantity As Integer = 0

            If physicalProduct.Quantity > pendingQuantity Then
                quantity = pendingQuantity
                pendingQuantity = 0
            Else
                quantity = physicalProduct.Quantity
                pendingQuantity -= physicalProduct.Quantity
            End If

            If typeWarehouse = eWareHouseType.Stock Then
                detailItem.QuantityStock += quantity
            ElseIf typeWarehouse = eWareHouseType.WareHouse Then
                detailItem.QuantityWarehouse += quantity
            ElseIf typeWarehouse = eWareHouseType.Maquila Then
                detailItem.QuantityMaquila += quantity
            ElseIf typeWarehouse = eWareHouseType.Remnant Then
                detailItem.QuantityRemnant += quantity
            End If

            pickingList.Add(createPicking(campaignDetailId, physicalProduct, quantity, itemType, codeUser))

            If pendingQuantity = 0 Then
                Exit For
            End If
        Next

        Return pickingList
    End Function

    ''' <summary>
    ''' Descuenta del inventario disponible las cantidades ya asignadas en el picking calculado durante la ejecución actual.
    ''' </summary>
    ''' <param name="listInventory">Inventario disponible consultado para el producto, almacén y lote que se está procesando</param>
    ''' <param name="currentPicking">Picking acumulado en memoria durante el cálculo actual</param>
    Private Sub DiscountAlreadyPickedInventory(listInventory As List(Of PhysicalInventory), currentPicking As List(Of CampaignDetailPicking))
        If listInventory Is Nothing OrElse currentPicking Is Nothing OrElse Not currentPicking.Any() Then
            Return
        End If

        Dim pickedByInventory = currentPicking.
            GroupBy(Function(x) New With {Key .ProductId = x.ProductId, Key .WarehouseId = x.WarehouseId, Key .BatchSerialId = x.BatchSerialId}).
            ToDictionary(Function(g) g.Key, Function(g) g.Sum(Function(x) x.Quantity))

        For Each physicalProduct In listInventory
            Dim key = New With {Key .ProductId = physicalProduct.ProductId, Key .WarehouseId = physicalProduct.WarehouseId, Key .BatchSerialId = physicalProduct.BatchSerialId}
            Dim pickedQuantity As Integer = 0
            If pickedByInventory.TryGetValue(key, pickedQuantity) Then
                physicalProduct.Quantity = Math.Max(physicalProduct.Quantity - pickedQuantity, 0)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Funcion que Guardar los Objetos del Picking
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="physicalProduct"></param>
    ''' <param name="quantity"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Private Function createPicking(campaignDetailId As Integer, physicalProduct As PhysicalInventory, quantity As Integer, itemType As Byte, userCode As String) As CampaignDetailPicking
        Dim picking As New CampaignDetailPicking()
        picking.CampaignDetailId = campaignDetailId
        picking.ProductId = physicalProduct.ProductId
        picking.WarehouseId = physicalProduct.WarehouseId
        picking.BatchSerialId = physicalProduct.BatchSerialId
        picking.Quantity = quantity
        picking.ItemType = itemType
        picking.CreationUser = userCode
        picking.CreationDate = Date.Now
        Return picking
    End Function

#End Region

#Region "Solicitud Orden de Traslado"

    ''' <summary>
    ''' Proceso Orden de Traslado 
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="CMConfiguration"></param>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="CampaignNumber"></param>
    ''' <param name="audit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Async Function OrdertransferAsync(transferOrderlist As TransferOrder, CMConfiguration As Integer, campaignDetailId As Integer, CampaignNumber As Integer, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult(Of TransferOrder)) Implements IRawMaterialAdminService.OrdertransferAsync
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)
                Dim result As ActionResult(Of TransferOrder) = Nothing
                Dim campaignValidationTmp = _campaignDetailValidationRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId, True, {"InventoryProduct", "Warehouse"})

                If Not campaignValidationTmp.Any() OrElse campaignValidationTmp.Count = 0 Then
                    Throw New IndigoValidationException("No se encontraron productos para realizar la Orden de Traslado")
                End If

                Dim campaignValidation = (From x In campaignValidationTmp Select x Where (x.DeliveredQuantity - x.DevolutionQuantity) > x.TransferOrderQuantity).ToList()
                If Not campaignValidation.Any() OrElse campaignValidation.Count = 0 Then
                    Throw New IndigoValidationException("No se ha entregado productos para realizar la Orden de Traslado")
                End If

                For Each y As CampaignDetailValidation In campaignValidation
                    With y
                        .TransferOrderQuantityTmp = y.TransferOrderQuantity
                        .DeliveredQuantity -= y.TransferOrderQuantity
                        .TransferOrderQuantity += y.DeliveredQuantity
                    End With
                Next

                Dim GetCMConfiguration = _CMConfigRepository.FirstOrDefault(Function(m) m.Id = CMConfiguration, includes:={"CMWarehouse"})
                Dim validationItemsControl = (From v In campaignValidation Where v.Warehouse.ControlStore Select v).ToList()
                Dim generatedCodes As New List(Of String)()

                If validationItemsControl.Any() Then
                    ' Obtenemos el almacén de control configurado en la central de mezclas
                    Dim wareHouseControlMixingStation = GetCMConfiguration.CMWarehouse.FirstOrDefault(Function(m) m.WarehouseType = 5)

                    If wareHouseControlMixingStation Is Nothing Then Throw New IndigoValidationException("No se encontró un almacén de Control en la central de mezclas")

                    Dim OrderTransferDetailControl = CreateOrderTransferDetail(validationItemsControl)

                    Dim tor As New TransferOrder With {
                        .OperatingUnitId = transferOrderlist.OperatingUnitId,
                        .DocumentDate = transferOrderlist.DocumentDate,
                        .OrderType = 1,
                        .DispatchTo = 1,
                        .SourceWarehouseId = validationItemsControl(0).WarehouseId,
                        .TargetWarehouseId = wareHouseControlMixingStation.IdWarehouse,
                        .Status = transferOrderlist.Status
                    }

                    Dim OrdertransferControl = CreateOrdertransfer(tor, campaignDetailId, GetCMConfiguration, CampaignNumber, OrderTransferDetailControl, audit.CodeUser)
                    result = _transferOrderAdminService.SaveTrasnferOrder(OrdertransferControl, audit)

                    If Not result.StateResult Then Throw New IndigoValidationException(result.Message)
                    generatedCodes.Add(result.ObjectEmbbeded.Code)
                    _campaignReports.SaveReports(Of TransferOrder)(
                                  campaignDetailId,
                                   result.ObjectEmbbeded.Id)
                End If

                Dim validationItems = (From v In campaignValidation Where Not v.Warehouse.ControlStore Select v).ToList()

                If validationItems.Any() Then
                    Dim OrderTransferDetail = CreateOrderTransferDetail(validationItems)
                    Dim OrdertransferC = CreateOrdertransfer(transferOrderlist, campaignDetailId, GetCMConfiguration, CampaignNumber, OrderTransferDetail, audit.CodeUser)
                    result = _transferOrderAdminService.SaveTrasnferOrder(OrdertransferC, audit)

                    If Not result.StateResult Then
                        Return New ActionResult(Of TransferOrder) With {.StateResult = False, .Message = result.Message}
                    End If

                    generatedCodes.Add(result.ObjectEmbbeded.Code)
                    _campaignReports.SaveReports(Of TransferOrder)(
                                  campaignDetailId,
                                   result.ObjectEmbbeded.Id)
                End If

                If result.StateResult Then
                    Dim updateStatus = Await UpdateStatusCampaignDetailAsync(campaignDetailId, 4, session)
                    If Not updateStatus.StateResult Then
                        Return New ActionResult(Of TransferOrder) With {.StateResult = False, .Message = updateStatus.Message}
                    End If

                    Dim updateQuantity = Await UpdateTransferOrderQuantityAsync(campaignDetailId)
                    If Not updateQuantity.StateResult Then
                        Return New ActionResult(Of TransferOrder) With {.StateResult = False, .Message = updateQuantity.Message}
                    End If
                End If

                AddToKardex(campaignDetailId, validationItems, audit)
                scope.Complete()

                Return New ActionResult(Of TransferOrder) With {.StateResult = True, .MessageResult = generatedCodes}
            End Using
        Catch ex As IndigoValidationException
            Return New ActionResult(Of TransferOrder) With {.StateResult = False, .Message = ex.Message}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of TransferOrder) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of TransferOrder) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Private Sub AddToKardex(campaignDetailId As Integer, validations As List(Of CampaignDetailValidation), audit As AuditMessage)
        If validations Is Nothing Then Return

        ' Obtener el MSClass de la campaña para determinar si es NPT
        Dim msClass As Integer = _CampaignDetailRepository.Query(Function(m) m.Id = campaignDetailId, includes:={"UnitDoseType"}).Select(Function(m) m.UnitDoseType.MSClass).FirstOrDefault()

        For Each Item In validations
            Dim measurementUnitId = _inventoryService.GetMeasurementUnitByProductId(Item.ProductId, msClass)
            Item.DeliveredQuantityTmp = Item.TransferOrderQuantityTmp

            If Item.DeliveredQuantityTmp IsNot Nothing AndAlso Item.DeliveredQuantityTmp > 0 Then
                If Item.DeliveredQuantityTmp = Item.DeliveredQuantity Then
                    Continue For
                End If

                If Item.DeliveredQuantityTmp < Item.DeliveredQuantity Then
                    Dim QuantityKardexOutput = Item.DeliveredQuantity - Item.DeliveredQuantityTmp
                    _campaignKardex.Savekardex(Of CampaignDetailValidation)(
                                        campaignDetailId:=campaignDetailId,
                                        movementType:=eMovementType.Input,
                                        productId:=Item.ProductId,
                                        batchSerialId:=Item.BatchSerialId,
                                        quantity:=(QuantityKardexOutput * measurementUnitId.FirstOrDefault.Item2).Value,
                                        measurementUnitId:=measurementUnitId.FirstOrDefault.Item1,
                                        description:="Validación de lotes",
                                        audit:=audit
                                    )
                Else
                    Dim QuantityKardexInput = Item.DeliveredQuantityTmp - Item.DeliveredQuantity.AsDecimal
                    _campaignKardex.Savekardex(Of CampaignDetailValidation)(
                                        campaignDetailId:=campaignDetailId,
                                        movementType:=eMovementType.Output,
                                        productId:=Item.ProductId,
                                        batchSerialId:=Item.BatchSerialId,
                                        quantity:=(QuantityKardexInput * measurementUnitId.FirstOrDefault.Item2).Value,
                                        measurementUnitId:=measurementUnitId.FirstOrDefault.Item1,
                                        description:="Validación de lotes",
                                        audit:=audit
                                    )
                End If
            Else
                _campaignKardex.Savekardex(Of CampaignDetailValidation)(
                                    campaignDetailId:=campaignDetailId,
                                    movementType:=eMovementType.Input,
                                    productId:=Item.ProductId,
                                    batchSerialId:=Item.BatchSerialId,
                                    quantity:=Item.DeliveredQuantity * measurementUnitId.FirstOrDefault.Item2,
                                    measurementUnitId:=measurementUnitId.FirstOrDefault.Item1,
                                    description:="Validación de lotes",
                                    audit:=audit
                                )
            End If
        Next
    End Sub

    ''' <summary>
    ''' Funcion crear Orden de traslado
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="listcampaignDetailId"></param>
    ''' <param name="GetCMConfiguration"></param>
    ''' <param name="CampaignNumber"></param>
    ''' <param name="OrderTransferDetail"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Private Function CreateOrdertransfer(transferOrderlist As TransferOrder, listcampaignDetailId As Integer, GetCMConfiguration As CMConfiguration, CampaignNumber As Integer, OrderTransferDetail As List(Of TransferOrderDetail), userCode As String) As TransferOrder
        Dim order As New TransferOrder
        With order
            .Code = ""
            .OperatingUnitId = transferOrderlist.OperatingUnitId
            .DocumentDate = Date.Now
            .OrderType = transferOrderlist.OrderType
            .DispatchTo = transferOrderlist.DispatchTo
            .SourceWarehouseId = transferOrderlist.SourceWarehouseId
            .TargetWarehouseId = transferOrderlist.TargetWarehouseId
            .Description = String.Format("{0} {1} {2} {3}", "Confirmación de lotes materia prima Central de Mezclas ", GetCMConfiguration.Name, " Campaña #", CampaignNumber)
            .TransitWarehouseId = transferOrderlist.TransitWarehouseId
            .Status = transferOrderlist.Status
            .CreationUser = userCode
            .CreationDate = DateTime.Now

            For Each item In OrderTransferDetail
                item.TransferOrder = order
                .TransferOrderDetail.Add(item)
            Next
            If .Id > 0 Then
                .MarkAsModified()
            End If

        End With
        Return order
    End Function

    ''' <summary>
    ''' Funcion Crear el Detalle de la Orden de traslado
    ''' </summary>
    ''' <param name="campaignValidation"></param>
    ''' <returns></returns>
    Private Function CreateOrderTransferDetail(campaignValidation As List(Of CampaignDetailValidation)) As List(Of TransferOrderDetail)
        Dim transferOrderDetail As New List(Of TransferOrderDetail)()

        campaignValidation.GroupBy(Function(m) m.ProductId).ToList() _
            .ForEach(Sub(ls As IGrouping(Of Integer, CampaignDetailValidation))
                         Dim orderDetail = New TransferOrderDetail
                         Dim intentoryQuantity As Integer = 0

                         For Each cdp In ls.Where(Function(m) m.DeliveredQuantity > 0).ToList()
                             Dim physicalInventory As PhysicalInventory = _physicalInventoryRepository _
                                .FirstOrDefault(Function(m) m.ProductId = ls.Key AndAlso m.BatchSerialId = cdp.BatchSerialId AndAlso m.WarehouseId = cdp.WarehouseId, includes:={"InventoryProduct"})

                             Dim batchSerial = createTransferBatchSerialDetail(physicalInventory.Id, cdp.DeliveredQuantity)
                             orderDetail.TransferOrderDetailBatchSerial.Add(batchSerial)

                             intentoryQuantity += physicalInventory.Quantity
                         Next
                         If orderDetail.TransferOrderDetailBatchSerial.Any() Then
                             Dim res = orderDetail.TransferOrderDetailBatchSerial.GroupBy(Function(m) New With {
                                    Key .ItemId = m.PhysicalInventoryId
                                    }
                                    )?.Select(Function(m) New TransferOrderDetailBatchSerial With {
                                    .PhysicalInventoryId = m.Key.ItemId,
                                    .Quantity = m.Sum(Function(o) o.Quantity),
                                    .OutstandingQuantity = m.Sum(Function(o) o.OutstandingQuantity)
                                    })?.ToList()
                             orderDetail.TransferOrderDetailBatchSerial = Nothing
                             For Each dx As TransferOrderDetailBatchSerial In res
                                 orderDetail.TransferOrderDetailBatchSerial.Add(dx)
                             Next
                         End If
                         Dim product = ls.FirstOrDefault().InventoryProduct
                         With orderDetail
                             .ProductId = ls.Key
                             .InventoryQuantity = intentoryQuantity
                             .Quantity = ls.Sum(Function(m) m.DeliveredQuantity)
                             .Description = String.Format("{0} - {1}", product.Code, product.Name)
                             .Value = product.ProductCost
                             .ConsumptionUnit = product.PackingUnitDescription
                             .CostProduct = product.ProductCost
                         End With

                         transferOrderDetail.Add(orderDetail)
                     End Sub)

        Return transferOrderDetail
    End Function

    ''' <summary>
    ''' Funcion Crear el BatchSerial detalle de la Orden de Traslado
    ''' </summary>
    ''' <param name="physicalInventoryID"></param>
    ''' <param name="quantity"></param>
    ''' <returns></returns>
    Private Function createTransferBatchSerialDetail(physicalInventoryID As Integer, quantity As Integer) As TransferOrderDetailBatchSerial
        Dim DetailBatchserial As New TransferOrderDetailBatchSerial
        With DetailBatchserial
            .PhysicalInventoryId = physicalInventoryID
            .Quantity = quantity
            .OutstandingQuantity = quantity
        End With
        Return DetailBatchserial
    End Function

    ''' <summary>
    ''' Actualizar estado de la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="statuscampaign"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Private Async Function UpdateStatusCampaignDetailAsync(campaignDetailId As Integer, statuscampaign As Byte, session As SessionValues) As Task(Of ActionResult(Of CampaignDetail)) Implements IRawMaterialAdminService.UpdateStatusCampaignDetailAsync
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)

                Dim ObjCampignDetail As CampaignDetail = _CampaignDetailRepository.FindById(campaignDetailId)
                ObjCampignDetail.Status = statuscampaign
                _CampaignDetailRepository.SaveEntity(ObjCampignDetail)
                Await _CampaignDetailRepository.UnitWork.CommitAsync()

                scope.Complete()
                Return New ActionResult(Of CampaignDetail) With {.StateResult = True}
            End Using
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of CampaignDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of CampaignDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Función que Actualiza el Campo de Cantidad Enviada - Cantidad Anterior
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Private Async Function UpdateTransferOrderQuantityAsync(campaignDetailId As Integer) As Task(Of ActionResult)
        Try
            Dim campaignValidationUPD = _campaignDetailValidationRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId, True, {"InventoryProduct"})
            Dim campaignValidation = (From x In campaignValidationUPD Select x).ToList()

            campaignValidation.ForEach(Sub(ls As CampaignDetailValidation)
                                           With ls
                                               .DeliveredQuantity = ls.TransferOrderQuantity
                                               ls.MarkAsModified()
                                           End With
                                       End Sub)

            Await Me._campaignDetailValidationRepository.SaveEntityMassiveAsync(campaignValidation)

            Return New ActionResult() With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult() With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


#End Region

#Region "Solicitud traslado de Inventario"
    ''' <summary>
    ''' Crea y Guarda una Solicitud de Inventario.
    ''' Para preparaciones de tipo reempaque/reenvase que provienen de solicitud de inventario (Source=4),
    ''' utiliza el WarehouseId de RequestUnitDoseInventory en lugar del almacén parametrizado por la CM.
    ''' </summary>
    ''' <param name="inventoryRequest">Solicitud de inventario con SourceWarehouseId (origen), TargetWarehouseId (destino) y datos de campaña.</param>
    ''' <param name="audit">Información de auditoría.</param>
    ''' <param name="session">Valores de sesión.</param>
    ''' <returns>ActionResult con el código generado o mensaje de error.</returns>
    Private Async Function SaveInventoryRequestAsync(inventoryRequest As InventoryRequest, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult) Implements IRawMaterialAdminService.SaveInventoryRequestAsync
        Try
            If inventoryRequest.CMConfigurationId = 0 Then Throw New IndigoValidationException("Central de Mezclas no existe")

            If audit Is Nothing Then Throw New IndigoValidationException("audit")

            Dim CMConfiguration = _CMConfigRepository.GetCMDetailById(inventoryRequest.CMConfigurationId)

            If CMConfiguration Is Nothing Then Throw New IndigoValidationException("Central de Mezclas no existe")

            Dim detailsPicking = _PickingRepository.GetCampaignPickingDetail(inventoryRequest.CampaignDetailId)

            If detailsPicking Is Nothing OrElse Not detailsPicking.Any() Then Throw New IndigoValidationException("Detalles no encontrados")

            Dim itemsCampaign = _CampaignDetailItemsRepository.GetProductsItems(inventoryRequest.CampaignDetailId)

            Dim effectiveSourceWarehouseId As Integer? = inventoryRequest.SourceWarehouseId

            If Not effectiveSourceWarehouseId.HasValue Then
                Throw New IndigoValidationException("No se ha enviado el Almacén de Origen")
            End If

            inventoryRequest.SourceWarehouseId = effectiveSourceWarehouseId

            'Filtro solo tipo almacén
            Dim warehouseId = effectiveSourceWarehouseId
            Dim campaignDetailPickingByWarehouse = (From x In detailsPicking Where x.WarehouseId = warehouseId).ToList()
            Dim inventoryRequestDetail = CreateInventoryRequestDetail(campaignDetailPickingByWarehouse)

            '===========================================================================================================
            ' Obtener ítems de campaña con cantidades en 0
            Dim itemsWithoutQuantities = (From x In itemsCampaign
                                          Where x.QuantityStock = 0 AndAlso
                                              x.QuantityWarehouse = 0 AndAlso
                                              x.QuantityMaquila = 0 AndAlso
                                              x.QuantityRemnant = 0 AndAlso
                                              x.DeliveredQuantity <= x.RequestQuantity
                                          Select x).ToList()

            Dim inventoryRequestDetailOther = CreateInventoryRequestDetailOther(itemsWithoutQuantities)

            '===========================================================================================================
            Dim _idCurrentSequence = 1673 'Inventario
            Dim inventoryRequestC = CreateInventoryRequest(inventoryRequest, CMConfiguration, inventoryRequestDetail, inventoryRequestDetailOther, audit.CodeUser)
            'save 
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)
                Dim result = Await _InventoryRequestAdminService.SaveInventoryRequestAsync(inventoryRequestC, audit, _idCurrentSequence)
                If result.StateResult = True Then
                    _campaignReports.SaveReports(Of InventoryRequest)(
                                   inventoryRequest.CampaignDetailId,
                                    result.ObjectEmbbeded.Id)
                    scope.Complete()
                    Return New ActionResult With {.StateResult = True, .MessageResult = {result.ObjectEmbbeded.Code}.ToList()}
                Else
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
                End If
            End Using
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion crear la Solicitud de Inventario
    ''' </summary>
    ''' <param name="inventoryRequest"></param>
    ''' <param name="GetCMConfiguration">Info Central mezclas</param>
    ''' <param name="InventoryRequestDetail">Detalle de la Solicitud de Inventario</param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Private Function CreateInventoryRequest(inventoryRequest As InventoryRequest, GetCMConfiguration As CMConfiguration, InventoryRequestDetail As List(Of InventoryRequestDetail), inventoryRequestDetailOther As List(Of InventoryRequestDetailOther), userCode As String) As InventoryRequest
        Dim inventory As New InventoryRequest
        With inventory
            .Code = ""
            .OperatingUnitId = inventoryRequest.OperatingUnitId
            .DocumentDate = DateTime.Now
            .RequestType = inventoryRequest.RequestType
            .MovementType = inventoryRequest.MovementType
            .SourceWarehouseId = inventoryRequest.SourceWarehouseId
            .TargetWarehouseId = inventoryRequest.TargetWarehouseId
            .Observation = String.Format("{0} {1} {2} {3}", "Requisición materia prima Central de Mezclas ", GetCMConfiguration.Name, " Campaña #", inventoryRequest.CampaignNumber)
            .Status = inventoryRequest.Status
            .CreationUser = userCode
            .CreationDate = DateTime.Now
            .ConfirmationDate = DateTime.Now
            .ConfirmationUser = userCode
            For Each item In InventoryRequestDetail
                item.InventoryRequest = inventory
                .InventoryRequestDetail.Add(item)
            Next

            For Each detailOther In inventoryRequestDetailOther
                detailOther.InventoryRequest = inventory
                .InventoryRequestDetailOther.Add(detailOther)
            Next
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
        Return inventory

    End Function

    ''' <summary>
    ''' Función Crear el Detalle de la Solicitud de Inventario
    ''' </summary>
    ''' <param name="detailsPicking"></param>
    ''' <returns></returns>
    Private Function CreateInventoryRequestDetail(detailsPicking As List(Of CampaignDetailPicking)) As List(Of InventoryRequestDetail)

        Dim inventoryRequestDetail = New List(Of InventoryRequestDetail)()

        detailsPicking.ForEach(Sub(ls As CampaignDetailPicking)
                                   Dim requestDetail = New InventoryRequestDetail
                                   With requestDetail
                                       .InventoryProductId = ls.ProductId
                                       .Quantity = ls.Quantity
                                       .OutstandingQuantity = ls.Quantity
                                       .Description = String.Format("{0} - {1}", ls.InventoryProduct.Code, ls.InventoryProduct.Name)
                                       .Status = 2 'Confirmado
                                       inventoryRequestDetail.Add(requestDetail)
                                   End With
                               End Sub)
        Return inventoryRequestDetail
    End Function

    ''' <summary>
    ''' Función para crear los medicamentos que no poseen disponibilidad en inventario físico
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateInventoryRequestDetailOther(detailsOthers As List(Of CampaignDetailItems)) As List(Of InventoryRequestDetailOther)

        Dim InventoryRequestDetailOther = New List(Of InventoryRequestDetailOther)()

        detailsOthers.ForEach(Sub(ls As CampaignDetailItems)
                                  Dim requestDetailOthers = New InventoryRequestDetailOther
                                  With requestDetailOthers
                                      .ComponentType = If(ls.AtcId.HasValue, CByte(1), If(ls.SupplyId.HasValue, CByte(2), If(ls.ProductId.HasValue, CByte(3), CByte(0))))
                                      .ATCId = ls.AtcId
                                      .SupplieId = ls.SupplyId
                                      .InventoryProductId = ls.ProductId
                                      .Quantity = ls.RequestQuantity - (ls.QuantityStock - ls.QuantityWarehouse - ls.QuantityMaquila)
                                      .OutstandingQuantity = ls.RequestQuantity - (ls.QuantityStock - ls.QuantityWarehouse - ls.QuantityMaquila)
                                      .Description = $"Solicitud de central de mezclas"
                                      .Status = 2 'Confirmado
                                      .OriginalQuantity = Nothing
                                      .UserConfirmAuthorization = Nothing
                                      InventoryRequestDetailOther.Add(requestDetailOthers)
                                  End With
                              End Sub)

        Return InventoryRequestDetailOther
    End Function
#End Region

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _RawMaterialRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

    Public Enum eWareHouseType
        Stock = 1
        WareHouse
        Maquila
        Remnant
    End Enum

End Class

