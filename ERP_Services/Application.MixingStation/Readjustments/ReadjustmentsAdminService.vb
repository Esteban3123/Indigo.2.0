'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 2022-05-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Application.Inventory.TransferOrder
Imports Application.Inventory.InventoryAdjustment
Imports Application.Inventory.Sequense
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class ReadjustmentsAdminService
    Implements IReadjustmentsAdminService, Inject


    Private ReadOnly _cmWarehouseRepository As ICMWarehouseRepository

#Region "Constants"
    Private ReadOnly AdjusmentInventoryForm As String = "318"
#End Region

#Region "Variables"
    ''' <summary>
    ''' repository
    ''' </summary>

    Private ReadOnly _readjustmentsRepository As IReadjustmentsRepository
    Private ReadOnly _transferOrderAdminService As ITransferOrderAdminService
    Private ReadOnly _inventoryProductRepository As IInventoryProductRepository
    Private ReadOnly _requestMixingStationDetailRepository As IRequestMixingStationDetailRepository
    Private ReadOnly _requestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository
    Private ReadOnly _pharmaceuticalDispensingDevolutionRepository As IPharmaceuticalDispensingDevolutionRepository

    ''' <summary>
    ''' Repositorio de parametros de central mezclas
    ''' </summary>
    Private ReadOnly _mixingStationSettingRepository As IMixingStationSettingRepository

    ''' <summary>
    ''' Repositorio a contabilidad general
    ''' </summary>
    Private ReadOnly _settingsAccountRepository As ISettingsAccountRepository

    ''' <summary>
    ''' Repositorio a inventario fisico - para obtener el batch serial
    ''' </summary>
    Private ReadOnly _physicalInventoryRepository As IPhysicalInventoryRepository

    ''' <summary>
    ''' Inyección a los servicios de ajustes de inventario
    ''' </summary>
    Private ReadOnly _inventoryAdjustmentAdminService As IInventoryAdjustmentAdminService

    ''' <summary>
    ''' Repositorio a secuencias de inventario
    ''' </summary>
    Private ReadOnly _inventorySequenceAdminService As IInventorySequenceAdminService

#End Region

#Region "Builder"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="ReadjustmentsRepository"></param>
    Public Sub New(ReadjustmentsRepository As IReadjustmentsRepository,
                   requestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository,
                   transferOrderAdminService As ITransferOrderAdminService,
                   pharmaceuticalDispensingDevolutionRepository As IPharmaceuticalDispensingDevolutionRepository,
                   inventoryProductRepository As IInventoryProductRepository,
                   requestMixingStationDetailRepository As IRequestMixingStationDetailRepository,
                   cmWarehouseRepository As ICMWarehouseRepository,
                   mixingStationSettingRepository As IMixingStationSettingRepository,
                   settingsAccountRepository As ISettingsAccountRepository,
                   inventoryAdjustmentAdminService As IInventoryAdjustmentAdminService,
                   physicalInventoryRepository As IPhysicalInventoryRepository,
                   inventorySequenceAdminService As IInventorySequenceAdminService)

        _cmWarehouseRepository = cmWarehouseRepository
        _readjustmentsRepository = ReadjustmentsRepository
        _transferOrderAdminService = transferOrderAdminService
        _inventoryProductRepository = inventoryProductRepository
        _requestMixingStationDetailRepository = requestMixingStationDetailRepository
        _pharmaceuticalDispensingDevolutionRepository = pharmaceuticalDispensingDevolutionRepository
        _requestPackageDetailStatusRepository = requestPackageDetailStatusRepository
        _mixingStationSettingRepository = mixingStationSettingRepository
        _settingsAccountRepository = settingsAccountRepository
        _inventoryAdjustmentAdminService = inventoryAdjustmentAdminService
        _physicalInventoryRepository = physicalInventoryRepository
        _inventorySequenceAdminService = inventorySequenceAdminService

    End Sub
#End Region

#Region "Functions"

    Public Function CreateTransferOrder(readjustments As List(Of Readjustments), operativeUnitId As Long, audit As AuditMessage) As ActionResult
        Dim sb As New StringBuilder()
        Dim sbs As New List(Of String)()

        For Each item In readjustments
            Dim devolution = _pharmaceuticalDispensingDevolutionRepository _
                .FirstOrDefault(Function(m) m.Id = item.EntityId, tracking:=False, includes:={"PharmaceuticalDispensingDevolutionDetail.PharmaceuticalDispensingDetailBatchSerial.PhysicalInventory.BatchSerial"})
            Dim requesPackageDetailStatus = _requestPackageDetailStatusRepository.FirstOrDefault(Function(m) m.Id = item.RequestPackageDetailStatusId, False, includes:={"RequestMixingStationDetail"})
            Dim cmWarehouseStock = _cmWarehouseRepository.GetByTypeAndCampaignDetailId(requesPackageDetailStatus.RequestMixingStationDetail.CampaignDetailId, 4)

            Dim transferOrder As New TransferOrder With {
                .Code = "",
                .OperatingUnitId = operativeUnitId,
                .DocumentDate = Date.Now,
                .OrderType = 1,
                .DispatchTo = 1,
                .SourceWarehouseId = devolution.WarehouseId,
                .TargetWarehouseId = cmWarehouseStock.IdWarehouse,
                .Description = "Traslado para análisis de readecuación",
                .TransitWarehouseId = Nothing,
                .Status = 2,
                .CreationUser = audit.CodeUser,
                .CreationDate = Date.Now
            }

            For Each detail As IGrouping(Of Integer, PharmaceuticalDispensingDevolutionDetail) In devolution.PharmaceuticalDispensingDevolutionDetail _
                .Where(Function(m) m.PharmaceuticalDispensingDetailBatchSerial.PhysicalInventory.BatchSerial.BatchCode = item.BatchCode) _
                .GroupBy(Function(m) m.PharmaceuticalDispensingDetailBatchSerial.PhysicalInventory.ProductId).ToList()

                Dim transferOrderDetail As New TransferOrderDetail()
                Dim inventoryQuantity As Integer = 0
                Dim product = _inventoryProductRepository.FirstOrDefault(Function(m) m.Id = detail.Key, False)

                For Each pdevolutionDetail As PharmaceuticalDispensingDevolutionDetail In detail
                    Dim physicalInventoryDispensing = pdevolutionDetail.PharmaceuticalDispensingDetailBatchSerial.PhysicalInventory
                    Dim physicalInventory As PhysicalInventory

                    If physicalInventoryDispensing.BatchSerialId.HasValue Then
                        physicalInventory = _physicalInventoryRepository.GetPhysicalInventoryByBatchSerial(physicalInventoryDispensing.ProductId, devolution.WarehouseId, physicalInventoryDispensing.BatchSerialId)
                    Else
                        physicalInventory = _physicalInventoryRepository.GetPhysicalInventory(physicalInventoryDispensing.ProductId, devolution.WarehouseId)
                    End If

                    Dim batchSerial = createTransferBatchSerialDetail(physicalInventory.Id, pdevolutionDetail.Quantity)
                    transferOrderDetail.TransferOrderDetailBatchSerial.Add(batchSerial)

                    inventoryQuantity += physicalInventory.Quantity
                Next

                With transferOrderDetail
                    .ProductId = detail.Key
                    .InventoryQuantity = inventoryQuantity
                    .Quantity = detail.Sum(Function(m) m.Quantity)
                    .Description = $"{product.Code} - {product.Name}"
                    .Value = product.ProductCost
                    .ConsumptionUnit = product.PackingUnitDescription
                    .CostProduct = product.ProductCost
                End With

                transferOrder.TransferOrderDetail.Add(transferOrderDetail)
            Next

            Dim res = _transferOrderAdminService.SaveTrasnferOrder(transferOrder, audit)
            If res.StateResult Then
                sbs.Add(res.ObjectEmbbeded.Code)
            Else
                sb.AppendLine(res.Message)
            End If
        Next

        If sb.Length > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = sb.ToString()}
        End If

        Return New ActionResult With {.StateResult = True, .Message = String.Join(", ", sbs)}
    End Function

    Private Function createTransferBatchSerialDetail(physicalInventoryId As Integer, quantity As Integer) As Object
        Dim DetailBatchserial As New TransferOrderDetailBatchSerial
        With DetailBatchserial
            .PhysicalInventoryId = physicalInventoryId
            .Quantity = quantity
            .OutstandingQuantity = quantity
        End With

        Return DetailBatchserial
    End Function

    ''' <summary>
    ''' Guarda o actualiza
    ''' </summary>
    ''' <param name="listReadjustments"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveReadjustmentsRepository(listReadjustments As List(Of Readjustments), operativeUnitId As Integer, audit As AuditMessage) As ActionResult Implements IReadjustmentsAdminService.SaveReadjustmentsRepository

        If listReadjustments Is Nothing OrElse Not listReadjustments.Any() Then
            Throw New Exception("El objeto a guardar viene vacio")
        End If

        Dim uow As IUnitWork = Me._readjustmentsRepository.UnitWork

        Try
            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                })

                Dim ListOfMessage = New List(Of String)

                listReadjustments.ForEach(Sub(x)
                                              Dim auxObjEntity As Readjustments = Nothing
                                              Dim auditProcess As IndigoAuditSimpleEntity(Of Readjustments)
                                              Dim status As Integer

                                              If x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                                                  x.CreationUser = audit.CodeUser
                                                  x.CreationDate = Date.Now
                                                  status = Infrastructure.CrossCutting.Audit.Actions.Insert
                                              Else
                                                  auxObjEntity = x.OriginalValue
                                                  x.ModificationUser = audit.CodeUser
                                                  x.ModificationDate = Date.Now
                                                  status = Infrastructure.CrossCutting.Audit.Actions.Update
                                              End If

                                              _readjustmentsRepository.SaveEntity(x)
                                              auditProcess = New IndigoAuditSimpleEntity(Of Readjustments)(x, audit, status, auxObjEntity)
                                              auditProcess.Execute()
                                              ListOfMessage.Add(IIf(status = Infrastructure.CrossCutting.Audit.Actions.Insert, ResourceManager.GetString("SaveMessage"), ResourceManager.GetString("UpdateMessage")))
                                          End Sub)

                Dim res = CreateTransferOrder(listReadjustments, operativeUnitId, audit)

                If Not res.StateResult Then
                    scope.Dispose()
                    Throw New IndigoValidationException(res.Message)
                End If

                uow.Commit()
                scope.Complete()

                Return New ActionResult With {.StateResult = True, .MessageResult = ListOfMessage, .Message = $"Se ha generado las siguientes Órdenes de Traslado: {res.Message}"}
            End Using
        Catch ex As OptimisticConcurrencyException
            uow.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As IndigoValidationException
            uow.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            uow.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function SaveReadjustmentsByTechnicalConcept(Readjustment As Readjustments, audit As AuditMessage) As ActionResult Implements IReadjustmentsAdminService.SaveReadjustmentsByTechnicalConcept
        If Readjustment Is Nothing Then
            Throw New Exception("El Objeto viene vacio")
        End If
        'UnitWork
        Dim readjustmentsUnitWork As IUnitWork = Me._readjustmentsRepository.UnitWork

        Try
            Dim auxObjEntity As Readjustments = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of Readjustments)
            Dim info = _readjustmentsRepository.GetReadjustmentsByRequestPackageStatus(Readjustment.RequestPackageDetailStatusId, True)

            'Valido que no haya un registro en estado aceptado o readecuada
            If info.Where(Function(r) r.Status = 1 And r.IsReadjustment = True).Count() > 0 Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"Ya existe una readecuación en estado Aceptado. Debe asignarla a una nueva solicitud"}.ToList()}
            End If

            Dim FirstRegister = info.Where(Function(r) r.IsReadjustment = False).FirstOrDefault()

            Dim maxReadjustments As Integer
            If FirstRegister.RequestPackageDetailStatus.PackagePersonalizedId IsNot Nothing Then
                maxReadjustments = FirstRegister.RequestPackageDetailStatus.PackagePersonalized.Package.Readjustments
            Else
                maxReadjustments = FirstRegister.RequestPackageDetailStatus.Package.Readjustments
            End If

            If maxReadjustments <= info.Count() - 1 Then
                Return New ActionResult With {.StateResult = False, .MessageResult = {"La adecuación verificada alcanzo el tope máximo de readecuaciones"}.ToList()}
            End If

            '-----Modifico el primer registro de la adecuación
            FirstRegister.Status = 1
            FirstRegister.TechnicalConceptDate = Readjustment.TechnicalConceptDate
            FirstRegister.ExpiratedDate = Readjustment.ExpiratedDate
            FirstRegister.Temperature = Readjustment.Temperature
            FirstRegister.TechnicalConcept = Readjustment.TechnicalConcept
            '-----

            Dim status As Integer
            If Readjustment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Readjustment.CreationUser = audit.CodeUser
                Readjustment.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert

                Readjustment.Number = info.Count()
                Readjustment.BatchCode = String.Format("{0}-R{1}", info.FirstOrDefault().BatchCode, info.Count())

            Else
                auxObjEntity = Readjustment.OriginalValue
                Readjustment.ModificationUser = audit.CodeUser
                Readjustment.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            '----- Actualizo el lote del paquete
            FirstRegister.RequestPackageDetailStatus.BatchCode = Readjustment.BatchCode

            _readjustmentsRepository.SaveEntity(Readjustment)
            auditProcess = New IndigoAuditSimpleEntity(Of Readjustments)(Readjustment, audit, status, auxObjEntity)
            auditProcess.Execute()
            readjustmentsUnitWork.Commit()
            Return New ActionResult With {.StateResult = True, .MessageResult = {"OK"}.ToList()}
        Catch ex As Exception
            readjustmentsUnitWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

    Public Function GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId As Integer, audit As AuditMessage, Optional tracking As Boolean = False) As List(Of Readjustments) Implements IReadjustmentsAdminService.GetReadjustmentsByRequestPackageStatus
        Try
            Return _readjustmentsRepository.GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Readjustments)
        End Try
    End Function


    Public Async Function GenerateInventoryAjustmenByReadjusments(ListReadjustmentsIds As List(Of Integer), operatingUnitId As Integer, audit As AuditMessage) As Task(Of ActionResult) Implements IReadjustmentsAdminService.GenerateInventoryAjustmenByReadjusments
        If ListReadjustmentsIds Is Nothing OrElse Not ListReadjustmentsIds.Any() Then
            Throw New Exception("la lista de objetos viene vacio")
        End If
        'Unitworks
        Dim readjustmentsRepositoryUnitWork As IUnitWork = Me._readjustmentsRepository.UnitWork

        Dim listMessage As New List(Of String)
        Dim messagesOK As String = String.Empty

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled)
            Try
                Dim mixingStationSettings = _mixingStationSettingRepository.GetMixingStationSettingByOperativeUnitId(operatingUnitId)
                'Recorro los que no estén eliminados
                For Each ReadjustmentsId In ListReadjustmentsIds

                    Dim readjusment = _readjustmentsRepository.GetReadjustmentsById(ReadjustmentsId)

                    If readjusment.Status = 3 Then
                        Continue For
                    End If

                    Dim inventoryAdjustment = Me.GenerateInventoryAdjustment(mixingStationSettings, readjusment, operatingUnitId)

                    If inventoryAdjustment.StateResult Then
                        Dim sequenceId = _inventorySequenceAdminService.GetCurrentSequenceByIdForm(Me.AdjusmentInventoryForm, operatingUnitId)
                        'Guardo el ajuste de inventario
                        Dim resultConfirmInventoryAdjustment = Await _inventoryAdjustmentAdminService.SaveAndConfirmbInventoryAdjustment(inventoryAdjustment.ObjectEmbbeded, audit, operatingUnitId, sequenceId)
                        If resultConfirmInventoryAdjustment.StateResult AndAlso resultConfirmInventoryAdjustment.StateResultAux Then
                            'Si ya se genero el ajuste, cambio el estado de la readecuación
                            Dim listReadJusment = _readjustmentsRepository.GetReadjustmentsByRequestPackageStatus(readjusment.RequestPackageDetailStatusId)
                            For Each eDelete In listReadJusment
                                eDelete.Status = 2
                                eDelete.ModificationDate = DateTime.Now
                                eDelete.ModificationUser = audit.CodeUser
                                eDelete.MarkAsModified()
                            Next
                            messagesOK = resultConfirmInventoryAdjustment.Message
                        Else
                            'Ocurrio un error guardando del ajuste
                            listMessage.Add(String.Format("{0}-", resultConfirmInventoryAdjustment.Message))
                        End If
                    Else
                        'Ocurrio un error armando los datos del ajuste
                        listMessage.Add(String.Format("{0}-", inventoryAdjustment.Message))
                    End If
                Next

                If listMessage.Count > 0 Then
                    readjustmentsRepositoryUnitWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .MessageResult = listMessage}
                End If
                readjustmentsRepositoryUnitWork.Commit()
                transaction.Complete()
                Return New ActionResult With {.StateResult = True, .Message = messagesOK}
            Catch ex As Exception
                readjustmentsRepositoryUnitWork.RollbackChanges()
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function


    Private Function GenerateInventoryAdjustment(settingsMixingStation As MixingStationSetting, readjusments As Readjustments, operatingUnitId As Integer) As ActionResult(Of InventoryAdjustment)
        Dim NewInventoryAdjustment As New InventoryAdjustment
        Dim errors As New StringBuilder

        'Obtención del almacen
        Dim requestPackageDetailStatus = _requestPackageDetailStatusRepository.GetPackageDetailStatusRequestInformation(readjusments.RequestPackageDetailStatusId)

        'Obtengo el tercero por parametros de contabilidad
        Dim settingAccount As GeneralLedgerSettings = _settingsAccountRepository.GetSettingAccountSimple(operatingUnitId)

        If requestPackageDetailStatus.InventoryAdjustmentWarehouseId = 0 Then
            errors.Append(String.Format("No se encontro un almacen de tipo terminado en la central de mezclas {0} - {1}", requestPackageDetailStatus.CodeMixingStation, requestPackageDetailStatus.NameMixingStation))
        End If

        If settingsMixingStation.InventoryAdjustmentConceptOutputId = 0 Then
            errors.Append("No se encontro un concepto de ajuste tipo salida en parámetros de central mezclas")
        End If

        If settingAccount Is Nothing Then
            errors.Append("No se encontro parámetros de contabilidad")
        End If

        If errors.Length > 0 Then
            Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = errors.ToString()}
        End If

        NewInventoryAdjustment.OperatingUnitId = operatingUnitId
        NewInventoryAdjustment.DocumentDate = DateTime.Now
        NewInventoryAdjustment.AdjustmentType = 2 'Salida
        NewInventoryAdjustment.AdjustmentConceptId = settingsMixingStation.InventoryAdjustmentConceptOutputId
        NewInventoryAdjustment.WarehouseId = requestPackageDetailStatus.InventoryAdjustmentWarehouseId
        NewInventoryAdjustment.ThirdPartyId = settingAccount.IdDian
        NewInventoryAdjustment.Description = "Readecuación No Aceptada desde dashboard control calidad"
        NewInventoryAdjustment.Status = 2

        'Detalles
        Dim productId As Integer
        Dim productCost As Decimal
        If requestPackageDetailStatus.PackagePersonalizedId IsNot Nothing Then
            productId = requestPackageDetailStatus.PackagePersonalized.Package.ProductId
            productCost = requestPackageDetailStatus.PackagePersonalized.Package.InventoryProduct.ProductCost
        Else
            productId = requestPackageDetailStatus.Package.ProductId
            productCost = requestPackageDetailStatus.Package.InventoryProduct.ProductCost
        End If

        Dim NewInventoryAdjustmentDetail As New InventoryAdjustmentDetail
        NewInventoryAdjustmentDetail.ProductId = productId
        NewInventoryAdjustmentDetail.Quantity = 1
        NewInventoryAdjustmentDetail.UnitValue = productCost

        Dim detailBatchSerial = _physicalInventoryRepository.GetPhysicalInventoryById(requestPackageDetailStatus.PhysicalInventoryId)
        If detailBatchSerial IsNot Nothing Then
            Dim NewInventoryAdjustmentDetailBatchSerial As New InventoryAdjustmentDetailBatchSerial
            NewInventoryAdjustmentDetailBatchSerial.Quantity = 1
            NewInventoryAdjustmentDetailBatchSerial.BatchSerialId = detailBatchSerial.BatchSerialId
            NewInventoryAdjustmentDetail.InventoryAdjustmentDetailBatchSerial.Add(NewInventoryAdjustmentDetailBatchSerial)
        End If

        NewInventoryAdjustment.InventoryAdjustmentDetail.Add(NewInventoryAdjustmentDetail)

        Return New ActionResult(Of InventoryAdjustment) With {.StateResult = True, .ObjectEmbbeded = NewInventoryAdjustment}
    End Function

    ''' <summary>
    ''' obtiene el detalle de la solicitud para asignar una readecuacion, se valida que tenga paquete y campos que se necesitan para el match con la readecuacion
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRequestMSDToReadjustmentById(id As Integer) As ActionResult(Of RequestMixingStationDetail) Implements IReadjustmentsAdminService.GetRequestMSDToReadjustmentById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim RequestMixingStationDetail As RequestMixingStationDetail = Me._requestMixingStationDetailRepository.GetRequestMixingStationDetailById(id)

            If RequestMixingStationDetail Is Nothing OrElse RequestMixingStationDetail?.Id = 0 Then
                Throw New Exception("No se encontró la solictud a buscar")
            End If

            Dim errors = New StringBuilder

            If RequestMixingStationDetail?.PackageId Is Nothing Then
                errors.AppendLine($"No tiene Paquete estándar")
            End If

            If RequestMixingStationDetail?.PackagePersonalizedId IsNot Nothing AndAlso (RequestMixingStationDetail.UnitDoseType.MSClass = 3) Then

                If RequestMixingStationDetail?.PackagePersonalizedId IsNot Nothing AndAlso (RequestMixingStationDetail?.PackagePersonalized?.ConcentrationAntibiotic Is Nothing _
                    OrElse RequestMixingStationDetail?.PackagePersonalized?.VolumeTotalPrepared Is Nothing _
                    OrElse RequestMixingStationDetail?.PackagePersonalized?.MeasurementPreparedId Is Nothing _
                    OrElse Not RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.Any(Function(d) d.MainMedicine AndAlso d.AtcId IsNot Nothing) _
                    OrElse Not RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.Any(Function(d) d.Vehicle AndAlso d.AtcId IsNot Nothing)) Then
                    errors.AppendLine("El paquete personalizado de la solicitud no esta correctamente creado")
                End If

            Else
                If RequestMixingStationDetail?.PackagePersonalizedId IsNot Nothing AndAlso (RequestMixingStationDetail?.PackagePersonalized?.Concentration Is Nothing _
                OrElse RequestMixingStationDetail?.PackagePersonalized?.ConcentrationMeasurementUnitId Is Nothing OrElse RequestMixingStationDetail?.PackagePersonalized?.VolumeTotalOrder Is Nothing _
                OrElse RequestMixingStationDetail?.PackagePersonalized?.VolumeTotalOrderMeasurementUnitId Is Nothing OrElse Not RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.Any(Function(d) d.MainMedicine AndAlso d.AtcId IsNot Nothing) _
                OrElse Not RequestMixingStationDetail?.PackagePersonalized?.PackagePersonalizedDetail?.Any(Function(d) d.Vehicle AndAlso d.AtcId IsNot Nothing)) Then

                    errors.AppendLine("El paquete personalizado de la solicitud no esta correctamente creado")

                ElseIf RequestMixingStationDetail?.PackagePersonalizedId Is Nothing AndAlso (RequestMixingStationDetail?.Package?.Concentration Is Nothing _
                        OrElse RequestMixingStationDetail?.Package?.ConcentrationMeasurementUnitId Is Nothing OrElse RequestMixingStationDetail?.Package?.VolumeTotalOrder Is Nothing _
                        OrElse RequestMixingStationDetail?.Package?.VolumeTotalOrderMeasurementUnitId Is Nothing OrElse Not RequestMixingStationDetail?.Package?.PackageDetail?.Any(Function(d) d.MainMedicine AndAlso d.AtcId IsNot Nothing) _
                        OrElse Not RequestMixingStationDetail?.Package?.PackageDetail?.Any(Function(d) d.Vehicle AndAlso d.AtcId IsNot Nothing)) Then

                    errors.AppendLine("El paquete estándar de la solicitud no esta correctamente creado")
                End If
            End If

            If errors.Length > 0 Then
                Return New ActionResult(Of RequestMixingStationDetail) With {.StateResult = False, .Message = $"La solicitud no paso las siguientes validaciones: {errors}"}
            End If

            Return New ActionResult(Of RequestMixingStationDetail) With {.StateResult = True, .ObjectEmbbeded = RequestMixingStationDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestMixingStationDetail) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

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

End Class
