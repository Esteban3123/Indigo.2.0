'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports System.Data.Entity.Core
Imports System.Text
Imports System.Transactions
Imports Application.Inventory.InventoryAdjustment
Imports Application.Inventory.InventoryProduct
Imports Application.Inventory.Sequense
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.AzureBlobStorage.Factory
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class CampaignAdminService
    Implements ICampaignAdminService, Inject

    ''' <summary>
    ''' Variables tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _CampaignRepository As ICampaignRepository
    Private ReadOnly _inventoryService As IInventoryService
    Private _campaingDetailRepository As ICampaignDetailRepository
    Private ReadOnly _RequestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository
    Private _requestMixingStationDetailRepository As IRequestMixingStationDetailRepository
    Private ReadOnly _mixingStationSetting As IMixingStationSettingRepository
    Private ReadOnly _campaignDetailPickingRepository As ICampaignDetailPickingRepository
    Private ReadOnly _CampaignDetailValidationRepository As ICampaignDetailValidationRepository
    Private ReadOnly _campaignDetailItemRepository As ICampaignItemsRepository
    Private ReadOnly _inventoryProductRepository As IInventoryProductRepository
    Private ReadOnly _inventorySupplieRepository As IInventorySupplieRepository
    Private ReadOnly _requestMixingStationRepository As IRequestMixingStationRepository
    Private ReadOnly _packageDetailRepository As IPackageDetailRepository
    Private ReadOnly _packagePersonalizedDetailRepository As IPackagePersonalizedDetailRepository
    Private ReadOnly _rawMaterialDevolutionRepository As IRawMaterialDevolutionRepository
    Private ReadOnly _campaignKardexRepository As ICampaignKardexRepository
    Private ReadOnly _CampaignKardex As ICampaignKardexAdminService
    Private ReadOnly _cmWarehouseRepository As ICMWarehouseRepository
    Private ReadOnly _thirdPartyRepository As IThirdPartyRepository
    Private ReadOnly _inventoryAdjustmentAdminService As IInventoryAdjustmentAdminService
    Private ReadOnly _inventorySequenceAdminService As IInventorySequenceAdminService
    Private ReadOnly _packagePersonalizedRepository As IPackagePersonalizedRepository
    Private ReadOnly _packageRepository As IPackageRepository
    Private ReadOnly _inventoryProductAdminService As IInventoryProductAdminService
    Private ReadOnly _campaignRawMaterialRepository As ICampaignRawMaterialRepository
    Private ReadOnly _campaignDetailBasketRepository As ICampaignDetailBasketDetailRepository
    Private ReadOnly _batchSerialRepository As IBatchSerialRepository
    Private ReadOnly _measurementUnitRepository As IMeasureUnitRepository
    Private ReadOnly _releaseLineRepository As IReleaseLineRepository
    Private ReadOnly _physicalInventoryRepository As IPhysicalInventoryRepository
    Private ReadOnly _atcRepository As IATCRepository
    Private ReadOnly _inventoryWarehouseRepository As IWarehouseRepository
    Private ReadOnly _confirmationUnitDoseRepository As IConfirmationUnitDoseRepository
    Private ReadOnly _campaignDetailWitnessFileRepository As ICampaignDetailWitnessFileRepository
    Private ReadOnly _storateFactory As IFactoryStorage
    Private ReadOnly _blobStorateService As Infrastructure.CrossCutting.AzureBlobStorage.IStorage
    Private ReadOnly _ReadjustmentRepository As IReadjustmentsRepository
    Private ReadOnly _unitDoseTypeRepository As IUnitDoseTypeRepository
    Private ReadOnly _ReasonscancellationNPTRepository As IReasonscancellationNPTRepository
    Private ReadOnly _pharmaDoseRepository As IPharmaDoseRepository
    Private Const MIXING_STATION_BLOB_CONTAINER As String = "mixing-station"

    Public Sub New(CampaignRepository As ICampaignRepository,
                   inventorySupplieRepository As IInventorySupplieRepository,
                   RequestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository,
                   RequestMixingStationDetailRepository As IRequestMixingStationDetailRepository,
                   campaignDetailPickingRepository As ICampaignDetailPickingRepository,
                   campaignDetailValidationRepository As ICampaignDetailValidationRepository,
                   campaignDetailItemRepository As ICampaignItemsRepository,
                   productRepository As IInventoryProductRepository,
                   CampaingDetailRepository As ICampaignDetailRepository,
                   requestMixingStationRepository As IRequestMixingStationRepository,
                   packageDetailRepository As IPackageDetailRepository,
                   inventoryProductRepository As IInventoryProductRepository,
                   mixingStationSetting As IMixingStationSettingRepository,
                   atcRepository As IATCRepository,
                   cmWarehouseRepository As ICMWarehouseRepository,
                   CampaignKardex As ICampaignKardexAdminService,
                   packagePersonalizedDetailRepository As IPackagePersonalizedDetailRepository,
                   rawMaterialDevolutionRepository As IRawMaterialDevolutionRepository,
                   campaignKardexRepository As ICampaignKardexRepository,
                   thirdPartyRepository As IThirdPartyRepository,
                   inventoryAdjustmentAdminService As IInventoryAdjustmentAdminService,
                   inventorySequenceAdminService As IInventorySequenceAdminService,
                   packageRepository As IPackageRepository,
                   inventoryProductAdminService As IInventoryProductAdminService,
                   packagePersonalizedRepository As IPackagePersonalizedRepository,
                   campaignRawMaterialRepository As ICampaignRawMaterialRepository,
                   campaignDetailBasketRepository As ICampaignDetailBasketDetailRepository,
                   batchSerialRepository As IBatchSerialRepository,
                   measurementUnitRepository As IMeasureUnitRepository,
                   releaseLineRepository As IReleaseLineRepository,
                   physicalInventoryRepository As IPhysicalInventoryRepository,
                   inventoryWarehouseRepository As IWarehouseRepository,
                   confirmationUnitDoseRepository As IConfirmationUnitDoseRepository,
                   campaignDetailWitnessFileRepository As ICampaignDetailWitnessFileRepository,
                   storateFactory As IFactoryStorage,
                   ReadjustmentRepository As IReadjustmentsRepository,
                   unitDoseTypeRepository As IUnitDoseTypeRepository,
                   ReasonscancellationNPTRepository As IReasonscancellationNPTRepository,
                   pharmadoseRepository As IPharmaDoseRepository
    )
        If CampaignRepository Is Nothing Then
            Throw New ArgumentNullException("CampaignRepository vacío")
        End If
        If RequestMixingStationDetailRepository Is Nothing Then
            Throw New ArgumentNullException("RequestMixingStationDetailRepository vacío")
        End If

        _inventoryProductRepository = productRepository
        If CampaingDetailRepository Is Nothing Then
            Throw New ArgumentNullException("CampaingDetailRepository vacío")
        End If

        _inventoryService = New InventoryServices(productRepository, atcRepository)
        _ReadjustmentRepository = ReadjustmentRepository
        _storateFactory = storateFactory
        _blobStorateService = _storateFactory.CreateStorageControl(System.Configuration.ConfigurationManager.AppSettings.Get("AzureBlobConnectionString"), MIXING_STATION_BLOB_CONTAINER)
        _packageRepository = packageRepository
        _CampaignRepository = CampaignRepository
        _thirdPartyRepository = thirdPartyRepository
        _mixingStationSetting = mixingStationSetting
        _cmWarehouseRepository = cmWarehouseRepository
        _batchSerialRepository = batchSerialRepository
        _measurementUnitRepository = measurementUnitRepository
        _physicalInventoryRepository = physicalInventoryRepository
        _campaignDetailBasketRepository = campaignDetailBasketRepository
        _campaignDetailItemRepository = campaignDetailItemRepository
        _campaignDetailPickingRepository = campaignDetailPickingRepository
        _CampaignDetailValidationRepository = campaignDetailValidationRepository
        _RequestPackageDetailStatusRepository = RequestPackageDetailStatusRepository
        _requestMixingStationDetailRepository = RequestMixingStationDetailRepository
        _ReasonscancellationNPTRepository = ReasonscancellationNPTRepository
        _campaingDetailRepository = CampaingDetailRepository
        _requestMixingStationRepository = requestMixingStationRepository
        _packageDetailRepository = packageDetailRepository
        _packagePersonalizedDetailRepository = packagePersonalizedDetailRepository
        _campaignKardexRepository = campaignKardexRepository
        _confirmationUnitDoseRepository = confirmationUnitDoseRepository
        _rawMaterialDevolutionRepository = rawMaterialDevolutionRepository
        _inventorySupplieRepository = inventorySupplieRepository
        _inventoryProductRepository = inventoryProductRepository
        _CampaignKardex = CampaignKardex
        _atcRepository = atcRepository
        _packagePersonalizedRepository = packagePersonalizedRepository
        _inventorySequenceAdminService = inventorySequenceAdminService
        _inventoryAdjustmentAdminService = inventoryAdjustmentAdminService
        _inventoryProductAdminService = inventoryProductAdminService
        _campaignRawMaterialRepository = campaignRawMaterialRepository
        _releaseLineRepository = releaseLineRepository
        _inventoryWarehouseRepository = inventoryWarehouseRepository
        _campaignDetailWitnessFileRepository = campaignDetailWitnessFileRepository
        _unitDoseTypeRepository = unitDoseTypeRepository
        _pharmaDoseRepository = pharmadoseRepository
    End Sub

    ''' <summary>
    ''' Registra el motivo de anulacion de una adecuación NPT
    ''' </summary>
    ''' <returns></returns>
    Public Function CancellationadjustmentsNPT(MixingstationDetailId As Integer, arguments As String, audit As AuditMessage) As ActionResult Implements ICampaignAdminService.CancellationadjustmentsNPT
        Try
            If MixingstationDetailId = 0 Then
                Throw New ArgumentNullException("MixingstationDetailId")
            End If

            If String.IsNullOrEmpty(arguments) Then
                Throw New ArgumentNullException("arguments")
            End If
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim rpds As IUnitWork = _RequestPackageDetailStatusRepository.UnitWork
                Dim rc As IUnitWork = _ReasonscancellationNPTRepository.UnitWork

                Dim dataRequestPackageDetailStatus As Object = Utils.DeserializeJsonToObject(arguments)
                Dim RequestPackageDetailStatus As RequestPackageDetailStatus = _RequestPackageDetailStatusRepository.
                                                                                GetByFilter(Function(x) x.RequestMixingStationDetailId.Equals(MixingstationDetailId) _
                                                                                , False,
                                                                                {"RequestMixingStationDetail"}).FirstOrDefault()

                If RequestPackageDetailStatus Is Nothing Then
                    Throw New ArgumentException("RequestPackageDetailStatus")
                    scope.Dispose()
                End If

                RequestPackageDetailStatus.Status = dataRequestPackageDetailStatus.status

                RequestPackageDetailStatus.MarkAsModified()
                _RequestPackageDetailStatusRepository.SaveEntity(RequestPackageDetailStatus)
                rpds.Commit()

                Dim CauseRejection As New ReasonscancellationNPT With {.CauseReprocessingRejectionId = dataRequestPackageDetailStatus.info.RejectCause,
                                                                        .Observations = dataRequestPackageDetailStatus.info.Observation,
                                                                        .RequestPackageDetailStatusId = RequestPackageDetailStatus.Id,
                                                                        .CreationDate = Date.Now(),
                                                                        .CreationUser = audit.CodeUser}
                CauseRejection.MarkAsAdded()
                _ReasonscancellationNPTRepository.SaveEntity(CauseRejection)
                rc.Commit()

                Dim RequestMixingStationDetails As List(Of RequestMixingStationDetail) = _requestMixingStationDetailRepository.
                                                                                            GetByFilter(Function(x) _
                                                                                            x.CampaignDetailId = RequestPackageDetailStatus.RequestMixingStationDetail.CampaignDetailId).ToList()

                If RequestMixingStationDetails.Count = 1 Then
                    Dim Result = RequestMixingStationDetails.FirstOrDefault()
                    Dim cd As IUnitWork = _campaingDetailRepository.UnitWork
                    Dim CampaignDetail As CampaignDetail = _campaingDetailRepository.GetByFilter(Function(x) x.Id = Result.CampaignDetailId).FirstOrDefault()

                    CampaignDetail.CampaignStatus = 4
                    CampaignDetail.ModificationUser = audit.CodeUser
                    CampaignDetail.ModificationDate = Date.Now()

                    CampaignDetail.MarkAsModified()
                    _campaingDetailRepository.SaveEntity(CampaignDetail)
                    cd.Commit()

                    scope.Complete()
                    Return New ActionResult With {.StateResult = True, .Message = $"La campaña #{CampaignDetail.CampaignNumber} cambio a estado anulada"}
                End If

                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = "El proceso se completo correctamente"}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetCampaignDetailWitnessFile(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailWitnessFile)) Implements ICampaignAdminService.GetCampaignDetailWitnessFile
        Try
            Dim files = _campaignDetailWitnessFileRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId, includes:={"CampaignDetail"}).ToList()

            If files IsNot Nothing AndAlso files.Any() Then
                Dim connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("AzureBlobConnectionString")
                Dim campaigDetailId = files(0).CampaignDetailId
                Dim campaignData = _campaingDetailRepository.Query(Function(m) m.Id = campaigDetailId).Select(Function(m) New With {Key m.CampaignNumber, Key m.CampaignId}).FirstOrDefault()

                For Each file In files
                    file.File = _blobStorateService.ReadFile($"{campaignData.CampaignId}/{campaignData.CampaignNumber}", file.Name)
                Next
            End If

            Return New ActionResult(Of List(Of CampaignDetailWitnessFile)) With {.StateResult = True, .ObjectEmbbeded = files}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CampaignDetailWitnessFile)) With {.StateResult = False, .Message = ex.ToDetailString()}
        End Try
    End Function


    Public Function SaveCampaignDetailWitnessFile(files As List(Of CampaignDetailWitnessFile), audit As AuditMessage) As ActionResult Implements ICampaignAdminService.SaveCampaignDetailWitnessFile
        Try
            If files IsNot Nothing AndAlso files.Any() Then

                Dim campaigDetailId = files(0).CampaignDetailId
                Dim campaignData = _campaingDetailRepository.Query(Function(m) m.Id = campaigDetailId).Select(Function(m) New With {Key m.CampaignNumber, Key m.CampaignId}).FirstOrDefault()
                Using scope As New TransactionScope(TransactionScopeOption.Required,
                                                    New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                    For Each file In files
                        If file.ChangeTracker.State = ObjectState.Deleted Then
                            _blobStorateService.DeleteFile($"{campaignData.CampaignId}/{campaignData.CampaignNumber}", $"{file.Name}")
                        Else
                            _blobStorateService.WriteFile($"{campaignData.CampaignId}/{campaignData.CampaignNumber}", $"{file.Name}", file.File)

                            file.CreationDate = Date.Now
                            file.CreationUser = audit.CodeUser
                        End If

                        _campaignDetailWitnessFileRepository.SaveEntity(file)
                    Next

                    _campaignDetailWitnessFileRepository.UnitWork.Commit()
                    scope.Complete()
                End Using
            End If

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.ToDetailString()}
        End Try
    End Function

    ''' <summary>
    ''' Proceso producto terminado
    ''' </summary>
    ''' <param name="requestMixingStationDetailIds"></param>
    ''' <returns></returns>
    Public Async Function ProcessFinishedProductAsync(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer)))) Implements ICampaignAdminService.ProcessFinishedProductAsync
        Dim listErrors As New List(Of Tuple(Of String, Integer))()

        Try
            ' Obtener solicitudes
            Dim requests = _requestMixingStationDetailRepository.Query(
            Function(m) requestMixingStationDetailIds.Contains(m.Id) AndAlso m.Status <> 3,
            False,
            {"CampaignDetail", "RequestMixingStation"}).ToList()

            ValidateRequest(requests)

            Using processScope As New TransactionScope(TransactionScopeOption.RequiresNew,
                                                   New TransactionOptions With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted},
                                                   TransactionScopeAsyncFlowOption.Enabled)

                AcquireProcessFinishedProductLocks(requests)

                ' Obtener detalles pendientes despues de tomar el bloqueo de campaña.
                Dim activeRequestIds = requests.Select(Function(m) m.Id).ToList()
                Dim requestPackageDetailStatus = _RequestPackageDetailStatusRepository.Query(
                Function(m) activeRequestIds.Contains(m.RequestMixingStationDetailId) AndAlso {1, 4}.Contains(m.Status),
                False,
                includes:={"CampaignRawMaterial"}
            ).ToList()

                If Not requestPackageDetailStatus.Any() Then
                    Throw New IndigoValidationException("No se encontraron detalles de solicitud pendientes a ser procesados.")
                End If

                ' Procesar cada solicitud
                For Each request In requests
                    Try
                        Dim relatedStatus = requestPackageDetailStatus.Where(Function(x) x.RequestMixingStationDetailId = request.Id).ToList()

                        If Not relatedStatus.Any() Then
                            Dim msg = $"La Solicitud {request.RequestMixingStation.Code} ya fue terminada los productos"
                            listErrors.Add(Tuple.Create(msg, 1))
                            My.Application.Log.WriteEntry(msg)
                            Continue For
                        End If

                        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew,
                                                   New TransactionOptions With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted},
                                                   TransactionScopeAsyncFlowOption.Enabled)

                            ' Procesar productos
                            Dim productsToProcess = relatedStatus.Where(Function(x) x.Status <> 4).ToList()
                            If productsToProcess.Any() Then
                                Dim AutomaticRawMaterial = Await AutomaticManageRawMaterial(productsToProcess, request, audit)

                                If Not AutomaticRawMaterial.StateResult Then
                                    Throw New IndigoValidationException(AutomaticRawMaterial.Message)
                                End If
                            End If

                            ' Asignar campos y guardar
                            AssignFieldsToRequestStatus(relatedStatus, audit)
                            Dim resSaveStatus = Await SavePackageDetailStatusAsync(audit, Nothing, relatedStatus)

                            If Not resSaveStatus.StateResult Then
                                Throw New IndigoValidationException(resSaveStatus.Message)
                            End If

                            scope.Complete()

                            Dim msgSuccess = $"La Solicitud {request.RequestMixingStation.Code} ejecutada correctamente"
                            listErrors.Add(Tuple.Create(msgSuccess, 1))
                            My.Application.Log.WriteEntry(msgSuccess)
                        End Using

                    Catch ex As IndigoValidationException
                        Dim msg = $"Solicitud {request.RequestMixingStation.Code}: {ex.Message}"
                        listErrors.Add(Tuple.Create(msg, 2))
                        My.Application.Log.WriteEntry(msg)
                    Catch ex As Exception
                        Dim msg = $"Solicitud {request.RequestMixingStation.Code}: {ex.Message}"
                        listErrors.Add(Tuple.Create(msg, 2))
                        IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    End Try
                Next

                processScope.Complete()
            End Using

            Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {
                .ObjectEmbbeded = listErrors,
                .StateResult = True
            }

        Catch ex As IndigoValidationException
            Dim msg = $"Error: {ex.Message}"
            listErrors.Add(Tuple.Create(msg, 3))
            My.Application.Log.WriteEntry(msg)
            Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {
                .ObjectEmbbeded = listErrors,
                .StateResult = False,
                .Message = ex.Message
            }

        Catch ex As Exception
            Dim msg = $"Error: {ex.Message}"
            listErrors.Add(Tuple.Create(msg, 2))
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {
                .ObjectEmbbeded = listErrors,
                .StateResult = False,
                .Message = Utils.GetInnerExceptionMessageToString(ex)
            }
        End Try
    End Function

    ''' <summary>
    ''' Toma un bloqueo exclusivo de aplicacion por cada campaña que se va a procesar como producto terminado.
    ''' </summary>
    ''' <param name="requests">Solicitudes de mezcla que pertenecen al proceso de producto terminado.</param>
    ''' <remarks>
    ''' El bloqueo se asocia a la transaccion activa. Mientras esa transaccion no termine, otra ejecucion
    ''' concurrente no puede procesar la misma campaña y se evita generar materias primas duplicadas.
    ''' </remarks>
    Private Sub AcquireProcessFinishedProductLocks(requests As List(Of RequestMixingStationDetail))
        Dim campaignDetailIds = requests _
            .Where(Function(m) m.CampaignDetailId.HasValue) _
            .Select(Function(m) m.CampaignDetailId.Value) _
            .Distinct() _
            .OrderBy(Function(m) m) _
            .ToList()

        For Each campaignDetailId In campaignDetailIds
            _RequestPackageDetailStatusRepository.ExecuteNonQuery("
            DECLARE @Result int;
            EXEC @Result = sys.sp_getapplock
                @Resource = {0},
                @LockMode = 'Exclusive',
                @LockOwner = 'Transaction',
                @LockTimeout = 0;
            IF @Result < 0
                RAISERROR('El proceso de producto terminado de esta campaña ya se encuentra en ejecución.', 16, 1);",
                            $"MixingStation.ProcessFinishedProduct.CampaignDetail:{campaignDetailId}")
        Next
    End Sub

    ''' <summary>
    ''' Asigna campos al status
    ''' </summary>
    ''' <param name="requestPackageDetailStatus"></param>
    ''' <param name="audit"></param>
    Private Sub AssignFieldsToRequestStatus(ByRef requestPackageDetailStatus As List(Of RequestPackageDetailStatus), ByVal audit As AuditMessage)
        requestPackageDetailStatus _
            .ForEach(Sub(status)
                         With status
                             .Status = 2

                             If status.VerificationTagUser Is Nothing And status.VerificationTagDate Is Nothing Then
                                 .VerificationTagUser = audit.CodeUser
                                 .VerificationTagDate = Date.Now()
                             End If
                         End With
                         status.MarkAsModified()
                     End Sub)
    End Sub

    ''' <summary>
    ''' Valida los datos de la solicitud
    ''' </summary>
    ''' <param name="request"></param>
    Private Sub ValidateRequest(request As List(Of RequestMixingStationDetail))
        If request Is Nothing OrElse Not request.Any() Then
            Throw New IndigoValidationException("Solicitud no encontrada")
        End If

        If request?.Any(Function(x) x.CampaignDetail.Status <> 4) Then
            Throw New IndigoValidationException("La validación de lotes debe haber culminado con por lo menos una orden de traslado en estado confirmado")
        End If

        If request?.Any(Function(x) x.CampaignDetail.CampaignStatus <> 5) Then
            Throw New IndigoValidationException("La campaña seleccionada no se encuentra en estado procesado")
        End If

    End Sub

    ''' <summary>
    ''' Automatic manege raw material
    ''' </summary>
    ''' <param name="requestPackageDetailStatus"></param>
    Private Async Function AutomaticManageRawMaterial(requestPackageDetailStatus As List(Of RequestPackageDetailStatus), request As RequestMixingStationDetail, audit As AuditMessage) As Task(Of ActionResult)
        Try
            Using scope As New TransactionScope(TransactionScopeOption.RequiresNew,
                                            New TransactionOptions() With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted
                                            }, TransactionScopeAsyncFlowOption.Enabled)

                ' Filtrar solo los que no tienen CampaignRawMaterial asociados
                Dim requestPackageDetailStatusIds = requestPackageDetailStatus _
                .Where(Function(m) Not m.CampaignRawMaterial.Any()) _
                .Select(Function(m) m.Id) _
                .ToList()

                If Not requestPackageDetailStatusIds.Any() Then
                    ' No hay registros pendientes, salir temprano sin error
                    scope.Complete()
                    Return New ActionResult With {.StateResult = True}
                End If

                ' Obtener detalle de productos
                Dim quantity = requestPackageDetailStatusIds.Count
                Dim res = GetProductDetailByCampaignId(requestPackageDetailStatusIds, Tuple.Create(quantity, request.Id))

                If Not res.StateResult Then
                    Throw New IndigoValidationException("No se encontraron registros para poder continuar")
                End If

                ' Guardar materiales
                Dim resultSave = Await SaveManageCampaignRawMaterialsAsync(res.Data,
                                                                       productPackageIds:=requestPackageDetailStatusIds,
                                                                       RequestMixingStationDetailId:=request.Id,
                                                                       audit:=audit)

                If Not resultSave.StateResult Then
                    Throw New IndigoValidationException(resultSave.Message)
                End If

                scope.Complete()
                Return New ActionResult With {.StateResult = True}

            End Using
        Catch ex As Exception
            Return New ActionResult With {
                .StateResult = False,
                .Message = Utils.GetInnerExceptionMessageToString(ex)
            }
        End Try
    End Function

    Public Function GetLastReleaseLineUsedByWorkingAreaId(releaseLineId As Integer, workingAreaId As Integer) As ReleaseLine Implements ICampaignAdminService.GetLastReleaseLineUsedByWorkingAreaId
        Try
            Return _releaseLineRepository.Query(Function(m) m.WorkingAreaId = workingAreaId AndAlso m.Id <> releaseLineId, tracking:=False, includes:={"CampaignDetail"}).OrderByDescending(Function(m) m.Id).FirstOrDefault()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Liberación de línea
    ''' </summary>
    ''' <param name="releaseLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveReleaseLine(releaseLine As ReleaseLine, audit As AuditMessage) As ActionResult Implements ICampaignAdminService.SaveReleaseLine
        Try
            Dim lastLine = _releaseLineRepository.FirstOrDefault(Function(m) m.WorkingAreaId = releaseLine.WorkingAreaId AndAlso m.Id <> releaseLine.Id AndAlso m.CampaignDetail.CampaignStatus <> 6, False, {"CampaignDetail"})

            If lastLine IsNot Nothing Then
                Throw New IndigoValidationException($"La Línea se encuentra en producción en la campaña No. ({lastLine.CampaignDetail.CampaignNumber})")
            End If

            If releaseLine.Id = 0 Then
                releaseLine.CreationUser = audit.CodeUser
                releaseLine.CreationDate = Date.Now
            Else
                releaseLine.ModificationUser = audit.CodeUser
                releaseLine.ModificationDate = Date.Now
            End If

            _releaseLineRepository.SaveEntity(releaseLine)
            _releaseLineRepository.UnitWork.Commit()

            Return New ActionResult With {.StateResult = True}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una liberacion de linea
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetReleaseLine(campaignDetailId As Integer) As ReleaseLine Implements ICampaignAdminService.GetReleaseLine
        Try
            Dim releaseLine = _releaseLineRepository.GetReleaseLineByCampaignDetailId(campaignDetailId)
            Return releaseLine
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function AnnulatePatients(objParams As String, audit As AuditMessage) As ActionResult(Of SP_ProcessMixingStation_Result) Implements ICampaignAdminService.AnnulatePatients
        Dim args As Object = Utils.DeserializeJsonToObject(objParams)
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertAnnularToXml(args)

                Dim result = _CampaignRepository.SP_ProcessMixingStation(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of SP_ProcessMixingStation_Result) With {.StateResult = False, .Message = result.Message}
                End If

                scope.Complete()
                Return New ActionResult(Of SP_ProcessMixingStation_Result) With {.StateResult = True, .ObjectEmbbeded = result, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of SP_ProcessMixingStation_Result) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ConvertAnnularToXml(args As Object) As String
        Dim builder As New StringBuilder

        For Each item In CType(args.Details, List(Of Object)).ToList()
            builder.Append("<Data>")

            builder.Append(String.Format("<RequestPackageDetailStatusId>{0}</RequestPackageDetailStatusId>", item.RequestPackageDetailStatusId))
            builder.Append(String.Format("<RequestMixingStationDetailPatientsId>{0}</RequestMixingStationDetailPatientsId>", item.RequestMixingStationDetailPatientsId))
            builder.Append(String.Format("<RequestMixingStationDetailId>{0}</RequestMixingStationDetailId>", item.RequestMixingStationDetailId))
            builder.Append(String.Format("<Status>{0}</Status>", item.Status))

            builder.Append("</Data>")
        Next

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Valida la campaña para terminacion
    ''' </summary>
    ''' <param name="campaignDetailid"></param>
    ''' <returns></returns>
    Public Function ValidateCampaignToEnd(campaignDetailid As Integer) As ActionResult Implements ICampaignAdminService.ValidateCampaignToEnd
        Try
            Dim Result = LoadAndValidateCampaign(campaignDetailid)
            If Not Result.StateResult Then
                Return New ActionResult With {.StateResult = False, .Message = Result.Message}
            End If

            Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Finaliza una campaña
    ''' </summary>
    ''' <returns></returns>
    Public Async Function EndCampaignAsync(
        campaignDetailId As Integer,
        observations As String,
        operativeUnitId As Integer,
        companyNit As String,
        audit As AuditMessage) As Task(Of ActionResult) Implements ICampaignAdminService.EndCampaignAsync

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required,
            New TransactionOptions With {
            .Timeout = TransactionManager.MaximumTimeout,
            .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)

                Dim campaign = LoadAndValidateCampaign(campaignDetailId)
                If Not campaign.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = campaign.Message}
                End If

                Dim costModels = CalculatePackageCostsFull(campaign.ObjectEmbbeded)
                If Not costModels.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = costModels.Message}
                End If

                Dim inputCostModel = costModels.ObjectEmbbeded.Item1
                Dim outputCostModel = costModels.ObjectEmbbeded.Item2

                Dim mixingStationSetting = _mixingStationSetting.FirstOrDefault(Function(m) m.OperativeUnitId = operativeUnitId)
                If mixingStationSetting Is Nothing Then
                    Throw New IndigoValidationException("No se encontró configuración de mezclas para la unidad operativa.")
                End If

                Dim companyThirdParty = _thirdPartyRepository.FirstOrDefault(Function(t) t.Nit = companyNit.Trim(), False)
                If companyThirdParty Is Nothing Then
                    Throw New IndigoValidationException("No se encontró un tercero con el NIT de la empresa.")
                End If

                Dim resultOut = Await GenerateInventoryAdjustmentOutputAsync(campaign.ObjectEmbbeded, outputCostModel, operativeUnitId, mixingStationSetting, companyThirdParty, audit)
                If Not resultOut.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = resultOut.Message}
                End If

                Dim newProducts = Await GeneratePackageProductAsync(campaign.ObjectEmbbeded, mixingStationSetting, audit)
                If Not newProducts.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = newProducts.Message}
                End If

                Dim resultIn = Await GenerateInventoryAdjustmentInputAsync(campaign.ObjectEmbbeded, mixingStationSetting, companyThirdParty.Id, operativeUnitId, inputCostModel, audit)
                If Not resultIn.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = resultIn.Message}
                End If

                Dim Result = Await UpdatePhysicalInventoryToRequestDetailStatusAsync(inputCostModel, campaign.ObjectEmbbeded)
                If Not Result.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = Result.Message}
                End If

                Dim UpdateCampaign = Await FinalizeCampaignState(campaign.ObjectEmbbeded, observations, audit)
                If Not UpdateCampaign.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = UpdateCampaign.Message}
                End If

                scope.Complete()
                Return BuildSuccessResult(resultOut.Message, resultIn.Message, newProducts.ObjectEmbbeded)
            End Using
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Construye el resultado exitoso del cierre de campaña, incluyendo los mensajes de entrada, salida y nuevos productos.
    ''' </summary>
    ''' <param name="resultOutMsg">Mensaje generado por el ajuste de inventario de salida.</param>
    ''' <param name="resultInMsg">Mensaje generado por el ajuste de inventario de entrada.</param>
    ''' <param name="newProducts">Lista de productos nuevos creados durante el proceso.</param>
    ''' <returns>Instancia de ActionResult con mensaje consolidado.</returns>
    Private Function BuildSuccessResult(resultOutMsg As String, resultInMsg As String, newProducts As List(Of String)) As ActionResult
        Dim sb As New StringBuilder()
        sb.AppendLine("La Campaña ha sido Terminada correctamente generando:")
        sb.AppendLine()
        sb.AppendLine("* Ajuste de inventario de salida con el siguiente detalle:")
        sb.AppendLine(resultOutMsg)

        If newProducts.Any() Then
            sb.AppendLine()
            sb.AppendLine("* Se ha creado los siguientes productos:")
            sb.AppendLine(String.Join(", ", newProducts))
        End If

        sb.AppendLine()
        sb.AppendLine("* Ajuste de inventario de entrada con el siguiente detalle:")
        sb.AppendLine(resultInMsg)

        Debug.WriteLine("OK: Success Result")
        Return New ActionResult With {
        .StateResult = True,
        .StatusCode = eStatusResult.SUCCESS,
        .Message = sb.ToString(),
        .MessageResult = newProducts}
    End Function

    ''' <summary>
    ''' Finaliza una campaña marcándola como terminada y actualizando campos de auditoría.
    ''' </summary>
    ''' <param name="campaign">La campaña a actualizar.</param>
    ''' <param name="observations">Observaciones registradas por el usuario.</param>
    ''' <param name="audit">Objeto de auditoría con información del usuario actual.</param>
    Private Async Function FinalizeCampaignState(campaign As CampaignDetail, observations As String, audit As AuditMessage) As Task(Of ActionResult)
        Try
            campaign.Observations = observations
            campaign.CampaignStatus = 6 ' Terminada
            campaign.ModificationUser = audit.CodeUser
            campaign.ModificationDate = Date.Now
            campaign.FinishDate = Date.Now
            _campaingDetailRepository.SaveEntity(campaign)
            Await _campaingDetailRepository.UnitWork.CommitAsync()

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .MessageResult = Utils.GetInnerExceptionMessages(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza las referencias al inventario físico dentro de los estados de detalle del paquete.
    ''' </summary>
    ''' <param name="packageCostModel">Lista de modelos de costo que contienen IDs de productos y lotes generados.</param>
    ''' <param name="campaign">Detalle de campaña correspondiente.</param>
    Private Async Function UpdatePhysicalInventoryToRequestDetailStatusAsync(
        packageCostModel As List(Of PackageCostModel),
        campaign As CampaignDetail) As Task(Of ActionResult)

        Try
            Dim productPackageIds = packageCostModel.Select(Function(p) p.ProductPackageId).Distinct().ToHashSet()
            Dim batchSerialIds = packageCostModel.Select(Function(p) p.BatchSerialGenerated.Id).Distinct().ToHashSet()
            Dim detailStatusIds = packageCostModel.Select(Function(p) p.RequestPackageDetailStatusId).Distinct().ToHashSet()

            ' MEJORA: Las llamadas a la BD ahora son asíncronas con 'Await' y 'ToListAsync'.
            Dim physicalInventories = Await _physicalInventoryRepository.Query(
            Function(m) productPackageIds.Contains(m.ProductId) AndAlso batchSerialIds.Contains(m.BatchSerialId),
            False,
            includes:={"InventoryProduct.ATC"}).ToListAsync()

            Dim detailStatuses = Await _RequestPackageDetailStatusRepository.Query(
            Function(m) detailStatusIds.Contains(m.Id),
            False).ToListAsync()

            ' Los diccionarios para acceso rápido se mantienen, son una excelente optimización.
            Dim physicalDict = physicalInventories.ToDictionary(Function(m) (m.ProductId, m.BatchSerialId))
            Dim statusDict = detailStatuses.ToDictionary(Function(m) m.Id)

            Dim toUpdate As New List(Of RequestPackageDetailStatus)()
            Dim isMagistral = GetUnitTypeDoseClassByCampaignDetail(campaign) = EUnitDoseTypeClass.Magistral

            For Each item In packageCostModel
                Dim physical As PhysicalInventory = Nothing
                Dim status As RequestPackageDetailStatus = Nothing

                If physicalDict.TryGetValue((item.ProductPackageId, item.BatchSerialGenerated.Id), physical) AndAlso
                    statusDict.TryGetValue(item.RequestPackageDetailStatusId, status) Then

                    status.PhysicalInventoryId = physical.Id
                    status.ProductId = physical.ProductId
                    status.ProductCost = physical.InventoryProduct.ProductCost

                    ' La lógica de negocio condicional se conserva intacta.
                    If isMagistral Then
                        Dim mainDrugCodeResult = UpdateMainDrugCodeByNPTandMagistral(physical.InventoryProduct.ATC.Code, status.GroupingCodeDose)
                        If Not mainDrugCodeResult.StateResult Then
                            Return mainDrugCodeResult
                        End If
                    End If

                    status.MarkAsModified()
                    toUpdate.Add(status)
                End If
            Next

            If toUpdate.Any() Then
                Await _RequestPackageDetailStatusRepository.SaveEntityMassiveAsync(toUpdate)
            End If

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualizamos el MainDrugCode para la campaña de tipo 'NPT' o 'Magistrales' que se esta terminando
    ''' </summary>
    Private Function UpdateMainDrugCodeByNPTandMagistral(_MainDrugCode As String, _GroupingCodeDose As Guid) As ActionResult
        Using cnx As New System.Data.SqlClient.SqlConnection(Utils.GetEntityConnectionString(ConfigurationFile.CONX_GENESIS, String.Empty, ServerSessionValues.Current.CurrentContainer, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try
                Dim CodeSusceptibleMixingStation = _pharmaDoseRepository.Query(
                    Function(x) _GroupingCodeDose = x.GroupingCodeDose,
                    tracking:=False).
                    Select(Function(x) x.CodeSusceptibleMixingStation).
                    FirstOrDefault()

                If CodeSusceptibleMixingStation = Guid.Empty Then
                    Throw New IndigoValidationException("No se encontró la dosis farmacéutica asociada al grupo de dosificación.")
                End If

                'Se actualizan las solicitudes
                command.CommandText = "update MedicalHistory.ProductSusceptibleMixingStation
                                        set MainDrugCode = @MainDrugCode
                                        where CodeSusceptibleMixingStation = @CodeSusceptibleMixingStation"
                command.Parameters.Add("@MainDrugCode", System.Data.SqlDbType.VarChar, 20).Value = _MainDrugCode
                command.Parameters.Add("@CodeSusceptibleMixingStation", System.Data.SqlDbType.UniqueIdentifier).Value = CodeSusceptibleMixingStation

                command.ExecuteNonQuery()
                tx.Commit()
                Return New ActionResult With {.StateResult = True}
            Catch ex As Exception
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Genera un producto por cada paquete si no tiene asociado uno, y lo registra automáticamente si es válido.
    ''' </summary>
    ''' <param name="campaign">Campaña con detalles de solicitudes</param>
    ''' <param name="setting">Configuración de mezclas</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>Lista de productos nuevos creados</returns>
    Private Async Function GeneratePackageProductAsync(
        campaign As CampaignDetail,
        setting As MixingStationSetting,
        audit As AuditMessage
    ) As Task(Of ActionResult(Of List(Of String)))
        Dim newProducts As New List(Of String)()
        Dim errors As New List(Of String)()

        For Each req In campaign.RequestMixingStationDetail
            If Not req.PackageId.HasValue Then
                If Not {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(GetUnitTypeDoseClassByCampaignDetail(campaign)) Then
                    errors.Add("El producto no tiene un paquete asociado")
                End If

                Continue For
            End If

            Dim result As ActionResult = Nothing

            If req.PackagePersonalizedId.HasValue Then
                result = Await CreateProductByPackagePersonalizedAsync(req, setting, audit)
            ElseIf req.PackageId.HasValue Then
                result = Await CreateProductByPackageAsync(req, setting, audit)
            End If

            If result Is Nothing Then
                Continue For
            ElseIf result.StateResult Then
                newProducts.Add(result.Message)
            Else
                errors.Add(result.Message)
            End If
        Next

        If errors.Any() Then
            Return New ActionResult(Of List(Of String)) With {.StateResult = False, .Message = String.Join(Environment.NewLine, errors.Distinct())}
        End If

        Return New ActionResult(Of List(Of String)) With {.ObjectEmbbeded = newProducts, .StateResult = True}
    End Function

    Private Async Function CreateProductByPackageAsync(
        requestMixingStationDetail As RequestMixingStationDetail,
        setting As MixingStationSetting,
        audit As AuditMessage
    ) As Task(Of ActionResult)
        ' Obtener el paquete
        Dim package = _packageRepository.FirstOrDefault(Function(p) p.Id = requestMixingStationDetail.PackageId.Value,
                                                            includes:={"UnitDoseType", "PackageDetail"})

        ' Si ya tiene producto asociado
        If package.ProductId.HasValue Then Return Nothing

        If Not package.PackageDetail.Any(Function(m) m.MainMedicine) Then
            Return New ActionResult With {.StateResult = False, .Message = $"El paquete ({package.Code} - {package.Name}) no posee un componente principal"}
        End If

        Dim atcId As Integer
        If package.UnitDoseType.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            If package.MainDrugId Is Nothing Then
                Return New ActionResult With {.StateResult = False, .Message = $"El paquete ({package.Code} - {package.Name}) no posee un medicamento de referencia NPT"}
            End If
            atcId = package.MainDrugId.Value
        Else
            Dim ItemMain As PackageDetail = package.PackageDetail.FirstOrDefault(Function(m) m.MainMedicine)

            Select Case ItemMain.ComponentType
                Case 1
                    atcId = ItemMain.AtcId.Value
                Case 2
                    atcId = _inventoryProductRepository.Query(Function(m) m.SupplieId = ItemMain.SupplieId.Value).Select(Function(m) m.ATCId).FirstOrDefault()
                Case 3
                    atcId = _inventoryProductRepository.Query(Function(m) m.Id = ItemMain.ProductId.Value).Select(Function(m) m.ATCId).FirstOrDefault()
            End Select
        End If

        ' Creacion del producto finalizado
        Dim finishedProduct As New FinishedProduct With {
            .Type = EFinishedProductType.Standard,
            .Code = package.Code,
            .Name = package.Name,
            .Description = package.Description,
            .AtcId = atcId,
            .UnitDoseType = package.UnitDoseType,
            .Setting = setting
        }

        Dim res = Await Me.CreateFinishedProductAsync(finishedProduct, audit)
        If Not res.StateResult Then
            Return New ActionResult With {.StateResult = False, .Message = res.Message}
        End If

        package.ProductId = res.ObjectEmbbeded.Id
        package.ModificationUser = audit.CodeUser
        package.ModificationDate = Date.Now
        package.MarkAsModified()

        _packageRepository.SaveEntity(package)
        Await _packageRepository.UnitWork.CommitAsync()

        Return New ActionResult With {.StateResult = True, .Message = res.ObjectEmbbeded.CodeName}
    End Function

    Private Async Function CreateProductByPackagePersonalizedAsync(
        requestMixingStationDetail As RequestMixingStationDetail,
        setting As MixingStationSetting,
        audit As AuditMessage
    ) As Task(Of ActionResult)
        ' Obtener el paquete personalizado
        Dim packagePersonalized = _packagePersonalizedRepository.FirstOrDefault(Function(p) p.Id = requestMixingStationDetail.PackagePersonalizedId.Value,
                                                            includes:={"UnitDoseType", "PackagePersonalizedDetail"})

        ' Si ya tiene producto asociado
        If packagePersonalized.ProductId.HasValue Then Return Nothing

        If Not packagePersonalized.PackagePersonalizedDetail.Any(Function(m) m.MainMedicine) Then
            Return New ActionResult With {.StateResult = False, .Message = $"El paquete personalizado ({packagePersonalized.Code} - {packagePersonalized.Name}) no posee un componente principal"}
        End If

        Dim atcId As Integer
        If packagePersonalized.UnitDoseType.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            atcId = requestMixingStationDetail.Package.MainDrugId
        Else
            Dim itemMain = packagePersonalized.PackagePersonalizedDetail.FirstOrDefault(Function(m) m.MainMedicine)

            Select Case itemMain.ComponentType
                Case 1
                    atcId = itemMain.AtcId.Value
                Case 2
                    atcId = _inventoryProductRepository.Query(Function(m) m.SupplieId = itemMain.SupplieId.Value).Select(Function(m) m.ATCId).FirstOrDefault()
                Case 3
                    atcId = _inventoryProductRepository.Query(Function(m) m.Id = itemMain.ProductId.Value).Select(Function(m) m.ATCId).FirstOrDefault()
            End Select
        End If

        ' Creacion del producto finalizado
        Dim finishedProduct As New FinishedProduct With {
            .Type = EFinishedProductType.Custom,
            .Code = packagePersonalized.Code,
            .Name = packagePersonalized.Name,
            .Description = packagePersonalized.Description,
            .AtcId = atcId,
            .UnitDoseType = packagePersonalized.UnitDoseType,
            .Setting = setting
        }

        Dim res = Await Me.CreateFinishedProductAsync(finishedProduct, audit)
        If Not res.StateResult Then
            Return New ActionResult With {.StateResult = False, .Message = res.Message}
        End If

        packagePersonalized.ProductId = res.ObjectEmbbeded.Id
        packagePersonalized.ModificationUser = audit.CodeUser
        packagePersonalized.ModificationDate = Date.Now
        packagePersonalized.MarkAsModified()

        _packagePersonalizedRepository.SaveEntity(packagePersonalized)
        Await _packagePersonalizedRepository.UnitWork.CommitAsync()

        Return New ActionResult With {.StateResult = True, .Message = res.ObjectEmbbeded.CodeName}
    End Function


    Private Async Function CreateFinishedProductAsync(
        finishedProduct As FinishedProduct,
        audit As AuditMessage
    ) As Task(Of ActionResult(Of InventoryProduct))
        Dim productCode = finishedProduct.ProductCode

        ' Verificar si ya existe producto con mismo código
        If _inventoryProductRepository.Any(Function(p) p.Code = productCode) Then
            Return New ActionResult(Of InventoryProduct) With {.StateResult = False, .Message = $"El producto ya existe. Asocie de manera manual un código al paquete ({finishedProduct.ProductCode} - {finishedProduct.ProductName})"}
        End If

        ' Crear nuevo producto y asociarlo
        If Not finishedProduct.UnitDoseType.InventoryGroupId.HasValue Then
            Return New ActionResult(Of InventoryProduct) With {.StateResult = False, .Message = $"No se encontró un Grupo de producto asociado al tipo de dosis del paquete ({finishedProduct.ProductCode} - {finishedProduct.ProductName})"}
        End If

        If Not finishedProduct.UnitDoseType.InventorySubGroupId.HasValue Then
            Return New ActionResult(Of InventoryProduct) With {.StateResult = False, .Message = $"No se encontró un SubGrupo de producto asociado al tipo de dosis del paquete ({finishedProduct.ProductCode} - {finishedProduct.ProductName})"}
        End If

        If Not finishedProduct.UnitDoseType.IVACodeId.HasValue Then
            Return New ActionResult(Of InventoryProduct) With {.StateResult = False, .Message = $"No se encontró un Código de IVA asociado al tipo de dosis del paquete ({finishedProduct.ProductCode} - {finishedProduct.ProductName})"}
        End If

        If Not finishedProduct.UnitDoseType.BillingGroupId.HasValue Then
            Return New ActionResult(Of InventoryProduct) With {.StateResult = False, .Message = $"No se encontró un Grupo de Facturación asociado al tipo de dosis del paquete ({finishedProduct.ProductCode} - {finishedProduct.ProductName})"}
        End If

        Dim product As New InventoryProduct With {
            .Id = 0,
            .Code = finishedProduct.ProductCode(),
            .Name = finishedProduct.ProductName(),
            .ProductTypeId = finishedProduct.Setting.ProductTypeId,
            .ATCId = finishedProduct.AtcId,
            .CodeAlternative = finishedProduct.Code,
            .Description = finishedProduct.Description.ToUpper(),
            .Abbreviation = finishedProduct.ProductAbbreviation(),
            .ProductGroupId = finishedProduct.UnitDoseType.InventoryGroupId,
            .ProductSubGroupId = finishedProduct.UnitDoseType.InventorySubGroupId,
            .MeasurementUnitId = finishedProduct.Setting.MeasurementUnitId,
            .PackagingUnitId = finishedProduct.Setting.PackageUnitId,
            .ManufacturerId = finishedProduct.Setting.ManufacturerId,
            .IVAId = finishedProduct.UnitDoseType.IVACodeId,
            .TaxedProduct = IIf(.IVAId Is Nothing, 0, 1),
            .BillingGroupId = finishedProduct.UnitDoseType.BillingGroupId,
            .ExpirationDay = finishedProduct.Setting.ExpirationDays,
            .LastPurchase = Date.Now,
            .LastSale = Date.Now,
            .ProductOrigin = 1, ' Nacional
            .MinimumStock = 0,
            .MaximumStock = 0,
            .CurrencyType = 1, 'Pesos colombianos
            .ControlCostPercentage = 0,
            .ProductCost = 0,
            .FinalProductCost = 0,
            .SellingPrice = 0,
            .Status = True
        }
        'TODO: Revisar 
        'Presentation de donde se puede obtener
        'Temperatura minima y maxima
        'SupplieId (deberia ser la central de mezclas?)
        'Storage va relacionado con la temperatura
        'Osmolarity

        Dim res = Await _inventoryProductAdminService.SaveInventoryProductAsync(product, audit)

        If Not res.StateResult Then
            Return New ActionResult(Of InventoryProduct) With {.StateResult = False, .Message = res.Message}
        End If

        product = _inventoryProductRepository.FirstOrDefault(Function(m) m.Code = res.ObjectEmbbeded.Code)

        Return New ActionResult(Of InventoryProduct) With {.StateResult = True, .ObjectEmbbeded = product}
    End Function

    ''' <summary>
    ''' Genera un ajuste de inventario de salida con base en la configuración y validaciones de la campaña.
    ''' </summary>
    Private Async Function GenerateInventoryAdjustmentOutputAsync(
        campaign As CampaignDetail,
        packageCostModel As List(Of PackageCostModel),
        operativeUnitId As Integer,
        _mixingStationSetting As MixingStationSetting,
        _companyThirdParty As ThirdParty,
        audit As AuditMessage) As Task(Of ActionResult(Of InventoryAdjustment))

        Try
            Dim sequenceId = _inventorySequenceAdminService.GetCurrentSequenceByIdForm("318", operativeUnitId)
            Dim sb As New StringBuilder()

            Dim validationsControl = campaign.CampaignDetailValidation.Where(Function(v) v.Warehouse?.ControlStore).ToList()

            If validationsControl.Any() Then
                Dim controlWarehouse = _cmWarehouseRepository.FirstOrDefault(Function(w) w.StateWH AndAlso
            w.WarehouseType = 5 AndAlso
            w.IdMixingStation = campaign.Campaign.CMConfigurationId)

                If controlWarehouse Is Nothing Then
                    Throw New IndigoValidationException("No se encontró almacén de control para la campaña.")
                End If

                Dim resultControl = Await ConfirmInventoryAdjustmentOutput(
                controlWarehouse.IdWarehouse,
                _mixingStationSetting.InventoryAdjustmentConceptOutputId,
                _companyThirdParty.Id,
                campaign.CampaignNumber,
                operativeUnitId,
                validationsControl,
                Nothing,
                sequenceId,
                audit)

                sb.AppendLine(resultControl.Message)
            End If

            Dim validationsProduction = campaign.CampaignDetailValidation.Where(Function(v) Not v.Warehouse?.ControlStore).ToList()

            Dim productionWarehouse = _cmWarehouseRepository.FirstOrDefault(Function(w) w.StateWH AndAlso
            w.WarehouseType = 3 AndAlso
            w.IdMixingStation = campaign.Campaign.CMConfigurationId)

            If productionWarehouse Is Nothing Then
                Throw New IndigoValidationException("No se encontró almacén de producción para la campaña.")
            End If

            Dim resultProduction = Await ConfirmInventoryAdjustmentOutput(
                productionWarehouse.IdWarehouse,
                _mixingStationSetting.InventoryAdjustmentConceptOutputId,
                _companyThirdParty.Id,
                campaign.CampaignNumber,
                operativeUnitId,
                validationsProduction,
                packageCostModel,
                sequenceId,
                audit)

            If Not resultProduction.StateResult Then
                Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = resultProduction.Message}
            End If

            sb.AppendLine(resultProduction.Message)

            Return New ActionResult(Of InventoryAdjustment) With {.StateResult = True, .Message = sb.ToString()}
        Catch ex As Exception
            Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Crea y confirma un ajuste de inventario de salida
    ''' </summary>
    ''' <param name="wareHouseId"></param>
    ''' <param name="inventoryAdjustmentConceptOutputId"></param>
    ''' <param name="thirdPartyId"></param>
    ''' <param name="campaignNumber"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="campaignValidationItems"></param>
    ''' <param name="packageCostModel"></param>
    ''' <param name="sequenceId"></param>
    ''' <param name="audit"></param>
    Private Async Function ConfirmInventoryAdjustmentOutput(
        wareHouseId As Integer,
        inventoryAdjustmentConceptOutputId As Integer,
        thirdPartyId As Integer,
        campaignNumber As Integer,
        operativeUnitId As Integer,
        campaignValidationItems As List(Of CampaignDetailValidation),
        packageCostModel As List(Of PackageCostModel),
        sequenceId As Integer,
        audit As AuditMessage) As Task(Of ActionResult(Of InventoryAdjustment))

        Try
            If campaignValidationItems Is Nothing OrElse Not campaignValidationItems.Any() Then
                Throw New IndigoValidationException("No se proporcionaron validaciones de campaña")
            End If

            Dim productIds = campaignValidationItems.Select(Function(v) v.ProductId).Distinct().ToList()
            Dim productsById = _inventoryProductRepository.GetByFilter(Function(p) productIds.Contains(p.Id)) _
                                                .ToDictionary(Function(p) p.Id)

            Dim adjustmentControl As New InventoryAdjustment With {
                .DocumentDate = Date.Now,
                .AdjustmentType = 2, ' Salida
                .ThirdPartyId = thirdPartyId,
                .Description = $"MATERIA PRIMA NETA ASOCIADA A LA CAMPAÑA {campaignNumber}",
                .AdjustmentConceptId = inventoryAdjustmentConceptOutputId,
                .WarehouseId = wareHouseId,
                .OperatingUnitId = operativeUnitId,
                .Status = 2
            }

            For Each productGroup In campaignValidationItems.GroupBy(Function(v) v.ProductId)

                Dim quantity As Integer = productGroup.Sum(Function(v) v.DeliveredQuantity - v.DevolutionQuantity)
                If quantity <= 0 Then Continue For

                If Not productsById.ContainsKey(productGroup.Key) Then
                    Throw New IndigoValidationException($"Producto no encontrado con Id: {productGroup.Key}")
                End If

                Dim product = productsById(productGroup.Key)

                Dim adjustmentDetail As New InventoryAdjustmentDetail With {
                    .Id = 0,
                    .ProductId = product.Id,
                    .Quantity = quantity,
                    .UnitValue = product.ProductCost
                }

                For Each batchGroup In productGroup.GroupBy(Function(v) v.BatchSerialId)

                    Dim batchQuantity = batchGroup.Sum(Function(v) v.DeliveredQuantity - v.DevolutionQuantity)
                    If batchQuantity <= 0 Then Continue For

                    Dim batchDetail As New InventoryAdjustmentDetailBatchSerial With {
                        .Id = 0,
                        .Quantity = batchQuantity,
                        .BatchSerialId = batchGroup.Key
                    }
                    adjustmentDetail.InventoryAdjustmentDetailBatchSerial.Add(batchDetail)
                Next

                adjustmentControl.InventoryAdjustmentDetail.Add(adjustmentDetail)
            Next

            If Not adjustmentControl.InventoryAdjustmentDetail.Any() Then
                Throw New IndigoValidationException("No se encontraron detalles con cantidades a ajustar")
            End If

            If packageCostModel IsNot Nothing AndAlso packageCostModel.Any() Then
                Dim totalOutput = Math.Round(adjustmentControl.InventoryAdjustmentDetail _
                .Sum(Function(d) d.Quantity * d.UnitValue), 2)

                Dim totalInput = Math.Round(packageCostModel.Sum(Function(m) m.Cost), 2)

                If totalOutput <> totalInput Then
                    Dim difference = Math.Abs(totalOutput - totalInput)
                    Throw New IndigoValidationException($"El valor total de Movimiento de Entrada es diferente al valor Total de ajuste de Salida. Salida: {totalOutput:N2}. Entrada: {totalInput:N2}. Diferencia: {difference:N2}")
                End If
            End If

            Dim resultConfirm = Await _inventoryAdjustmentAdminService.SaveAndConfirmbInventoryAdjustment(
            InventoryAdjustment:=adjustmentControl,
            audit:=audit,
            OperatingUnitId:=operativeUnitId,
            idSequence:=sequenceId)

            If Not resultConfirm.StateResult OrElse Not resultConfirm.StateResultAux Then
                If resultConfirm.StateResult AndAlso resultConfirm.Message.Contains("pero no se confirmo por") Then
                    Dim message = resultConfirm.Message.Split(New String() {"pero no se confirmo por"}, StringSplitOptions.None)(1).Trim()
                    Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = message}
                End If

                Dim messageFinal = If(Not String.IsNullOrEmpty(resultConfirm.Message),
                              resultConfirm.Message,
                              String.Join(vbCrLf, resultConfirm.MessageResult))
                Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = messageFinal}
            End If

            Return resultConfirm
        Catch ex As Exception
            Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Agrega al inventario los nuevos productos de tipo paquete
    ''' </summary>
    ''' <param name="campaign"></param>
    ''' <param name="packageCostModels"></param>
    ''' <returns></returns>
    Private Async Function GenerateInventoryAdjustmentInputAsync(
        campaign As CampaignDetail,
        mixingStationSetting As MixingStationSetting,
        companyThirdPartyId As Integer,
        operativeUnitId As Integer,
        packageCostModels As List(Of PackageCostModel),
        audit As AuditMessage) As Task(Of ActionResult(Of InventoryAdjustment))

        Try
            ' Validaciones iniciales
            If mixingStationSetting Is Nothing Then
                Throw New IndigoValidationException("No se encontró parámetros de Mezclas")
            End If

            Dim cmWarehouse = _cmWarehouseRepository.FirstOrDefault(Function(m) m.StateWH AndAlso m.WarehouseType = 4 AndAlso m.IdMixingStation = campaign.Campaign.CMConfigurationId)
            If cmWarehouse Is Nothing Then
                Throw New IndigoValidationException("No se encontró almacén de producción")
            End If

            ' Precarga
            Dim requiredPackageIds = packageCostModels.Select(Function(m) m.PackageId).Distinct().ToList()
            Dim packagesById = _packageRepository.GetByFilter(Function(p) requiredPackageIds.Contains(p.Id)).ToDictionary(Function(p) p.Id)

            Dim requiredPackagePersonalizedIds = packageCostModels.Select(Function(m) m.PackagePersonalizedId).Distinct().ToList()
            Dim packagesPersonalizedById = _packagePersonalizedRepository.GetByFilter(Function(p) requiredPackagePersonalizedIds.Contains(p.Id)).ToDictionary(Function(p) p.Id)

            Dim requiredStatusIds = packageCostModels.Select(Function(m) m.RequestPackageDetailStatusId).Distinct().ToList()
            Dim statusesById = _RequestPackageDetailStatusRepository.GetByFilter(Function(s) requiredStatusIds.Contains(s.Id)).ToDictionary(Function(s) s.Id)

            ' Ajuste principal
            Dim adjustment As New InventoryAdjustment With {
                    .DocumentDate = Date.Now,
                    .AdjustmentType = 1,
                    .ThirdPartyId = companyThirdPartyId,
                    .Description = $"Ingreso de productos transformados campaña No.{campaign.CampaignNumber}",
                    .AdjustmentConceptId = mixingStationSetting.InventoryAdjustmentConceptInputId,
                    .WarehouseId = cmWarehouse.IdWarehouse,
                    .OperatingUnitId = operativeUnitId,
                    .Status = 2
                }

            Dim unitDoseTypeClass = GetUnitTypeDoseClassByCampaignDetail(campaign)

            For Each packageGroup In packageCostModels.GroupBy(Function(m) New With {Key m.PackageId, Key m.PackagePersonalizedId, Key m.ProductId})
                Dim groupItems = packageGroup.ToList()
                Dim quantity As Integer = groupItems.Count()
                Dim totalCost = groupItems.Sum(Function(m) m.Cost)

                ' Determinar producto asociado
                Dim productId = packageGroup.Key.ProductId
                If Not {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(unitDoseTypeClass) Then
                    If packageGroup.Key.PackagePersonalizedId > 0 Then
                        If packagesPersonalizedById.ContainsKey(packageGroup.Key.PackagePersonalizedId) Then
                            productId = packagesPersonalizedById(packageGroup.Key.PackagePersonalizedId).ProductId
                        Else
                            Throw New IndigoValidationException($"Paquete personalizado no encontrado con Id: {packageGroup.Key.PackagePersonalizedId}")
                        End If
                    Else
                        If packagesById.ContainsKey(packageGroup.Key.PackageId) Then
                            productId = packagesById(packageGroup.Key.PackageId).ProductId
                        Else
                            Throw New IndigoValidationException($"Paquete no encontrado con Id: {packageGroup.Key.PackageId}")
                        End If
                    End If
                End If

                groupItems.ForEach(Sub(m) m.ProductPackageId = productId)

                Dim adjustmentDetail As New InventoryAdjustmentDetail With {
                        .Id = 0,
                        .ProductId = productId,
                        .Quantity = quantity,
                        .UnitValue = If(quantity > 0, totalCost / quantity, 0)
                    }

                If unitDoseTypeClass = EUnitDoseTypeClass.Repackaging Then
                    ' Un solo lote para el grupo
                    Dim firstStatus = statusesById(groupItems.First().RequestPackageDetailStatusId)
                    Dim batchCode As New BatchSerial With {
                            .Type = 1,
                            .ProductId = productId,
                            .BatchCode = firstStatus.BatchCode,
                            .ExpirationDate = firstStatus.BatchExpirationDate,
                            .CreationUser = audit.CodeUser,
                            .CreationDate = Date.Now
                        }

                    ' Asignar por navegación, no por ID
                    adjustmentDetail.InventoryAdjustmentDetailBatchSerial.Add(
                        New InventoryAdjustmentDetailBatchSerial With {
                            .Id = 0,
                            .Quantity = quantity,
                            .BatchSerial = batchCode
                        })

                    groupItems.ForEach(Sub(m) m.BatchSerialGenerated = batchCode)
                Else
                    ' Un lote por cada item
                    For Each item In groupItems
                        Dim status = statusesById(item.RequestPackageDetailStatusId)

                        ' Busca en los registros ya agregados si ya existe uno con el mismo BatchCode
                        Dim existingDetail = adjustmentDetail.InventoryAdjustmentDetailBatchSerial _
                                                    .FirstOrDefault(Function(d) d.BatchSerial IsNot Nothing AndAlso
                                                    d.BatchSerial.BatchCode = status.BatchCode)

                        If existingDetail IsNot Nothing Then
                            existingDetail.Quantity += 1
                            item.BatchSerialGenerated = existingDetail.BatchSerial
                        Else
                            ' No existe, creamos un nuevo BatchSerial y lo agregamos
                            Dim batchCode As New BatchSerial With {
                                    .Type = 1,
                                    .ProductId = productId,
                                    .BatchCode = status.BatchCode,
                                    .ExpirationDate = status.BatchExpirationDate,
                                    .CreationUser = audit.CodeUser,
                                    .CreationDate = Date.Now
                                }

                            Dim newDetail As New InventoryAdjustmentDetailBatchSerial With {
                                    .Id = 0,
                                    .Quantity = 1,
                                    .BatchSerial = batchCode
                                }

                            adjustmentDetail.InventoryAdjustmentDetailBatchSerial.Add(newDetail)
                            item.BatchSerialGenerated = batchCode
                        End If
                    Next
                End If

                adjustment.InventoryAdjustmentDetail.Add(adjustmentDetail)
            Next

            If Not adjustment.InventoryAdjustmentDetail.Any() Then
                Throw New IndigoValidationException("No se encontraron detalles con cantidades a ajustar")
            End If

            Dim sequenceId = _inventorySequenceAdminService.GetCurrentSequenceByIdForm("318", operativeUnitId)

            ' Commit total al final (persistencia de todos los objetos relacionados)
            Dim resultConfirm = Await _inventoryAdjustmentAdminService.SaveAndConfirmbInventoryAdjustment(
                InventoryAdjustment:=adjustment,
                audit:=audit,
                OperatingUnitId:=operativeUnitId,
                idSequence:=sequenceId,
                Type:=1)

            If Not resultConfirm.StateResult OrElse Not resultConfirm.StateResultAux Then
                If resultConfirm.StateResult AndAlso resultConfirm.Message.Contains("pero no se confirmo por") Then
                    Dim message = resultConfirm.Message.Split(New String() {"pero no se confirmo por"}, StringSplitOptions.None)(1).Trim()
                    Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = message}
                End If

                Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = resultConfirm.Message}
            End If

            Return resultConfirm
        Catch ex As Exception
            Return New ActionResult(Of InventoryAdjustment) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function GetUnitTypeDoseClassByCampaignDetail(CampaignDetail As CampaignDetail) As Integer
        If CampaignDetail.MSClass > 0 Then
            Return CampaignDetail.MSClass
        End If

        Dim UnitTypeDoseCampaign = _unitDoseTypeRepository.Query(Function(m) m.Id = CampaignDetail.UnitDoseTypeId).FirstOrDefault()
        CampaignDetail.MSClass = UnitTypeDoseCampaign.MSClass
        Return CampaignDetail.MSClass
    End Function

    ''' <summary>
    ''' Calcula los modelos de costo para una campaña, separando los costos de entrada y de salida.
    ''' </summary>
    ''' <param name="campaign">Campaña sobre la cual se realiza el cálculo.</param>
    ''' <returns>
    ''' Una tupla con:
    ''' - Item1: Lista de PackageCostModel para el inventario de entrada (excluye rechazados).
    ''' - Item2: Lista de PackageCostModel para el inventario de salida (incluye rechazados).
    ''' </returns>
    Private Function CalculatePackageCostsFull(campaign As CampaignDetail) As ActionResult(Of (List(Of PackageCostModel), List(Of PackageCostModel)))
        Dim inputCost = CalculatePackageCosts(campaign, takeIntoAccountRejected:=False)
        If Not inputCost.StateResult Then
            Return New ActionResult(Of (List(Of PackageCostModel), List(Of PackageCostModel))) With {.StateResult = False, .Message = inputCost.Message}
        End If

        Dim outputCost = CalculatePackageCosts(campaign, takeIntoAccountRejected:=True)
        If Not outputCost.StateResult Then
            Return New ActionResult(Of (List(Of PackageCostModel), List(Of PackageCostModel))) With {.StateResult = False, .Message = outputCost.Message}
        End If

        Return New ActionResult(Of (List(Of PackageCostModel), List(Of PackageCostModel))) With {.StateResult = True, .ObjectEmbbeded = (inputCost.ObjectEmbbeded, outputCost.ObjectEmbbeded)}
    End Function

    ''' <summary>
    ''' Calcula el valor de los paquetes
    ''' </summary>
    ''' <param name="campaignDetail"></param>
    ''' <returns></returns>
    Private Function CalculatePackageCosts(campaignDetail As CampaignDetail, Optional takeIntoAccountRejected As Boolean = False) As ActionResult(Of List(Of PackageCostModel))
        Try
            Dim packageCosts As New List(Of PackageCostModel)
            Dim campaignValidations = campaignDetail.CampaignDetailValidation.ToList()
            Dim statusToValidate As New List(Of Integer)({1})
            Dim readjustmentIds As List(Of Integer) = New List(Of Integer)

            If takeIntoAccountRejected Then
                statusToValidate.Add(2)
                readjustmentIds = _ReadjustmentRepository.GetAll().Select(Function(x) x.RequestPackageDetailStatusId).ToList()
            End If

            ' 1. Filtrado de RequestPackageDetailStatusId válidos
            Dim requestStatusIds = campaignDetail.RequestMixingStationDetail.
                SelectMany(Function(r) r.RequestPackageDetailStatus).
                Where(Function(s) statusToValidate.Contains(s.QualityStatus) AndAlso Not readjustmentIds.Contains(s.Id)).
                Select(Function(s) s.Id).ToList()

            ' 2. Carga de datos principales (materias primas, productos, unidades de medida)
            Dim rawMaterials = _campaignRawMaterialRepository.GetByFilter(Function(m) requestStatusIds.Contains(m.RequestPackageDetailStatusId)).ToList()
            Dim productValidationIds = rawMaterials.Select(Function(m) m.ProductValidationId).Distinct().ToList()
            Dim inventoryProducts = _inventoryProductRepository.GetByFilter(Function(p) productValidationIds.Contains(p.Id)).ToDictionary(Function(p) p.Id)
            Dim msClass = GetUnitTypeDoseClassByCampaignDetail(campaignDetail)
            Dim unitConversions = _inventoryService.GetMeasurementUnitsAndConcentrationsByProductIds(productValidationIds, msClass).ToDictionary(Function(x) x.ProductValidationId)

            ' 3. Construcción de diccionarios para unidades de medida
            Dim unitIds = rawMaterials.Select(Function(m) m.MeasurementUnitId).Distinct().ToList()
            Dim units = _measurementUnitRepository.GetByFilter(Function(u) unitIds.Contains(u.Id)).ToDictionary(Function(u) u.Id)

            ' 4. Procesamiento de materias primas directas
            For Each rmd In campaignDetail.RequestMixingStationDetail
                For Each rmds In rmd.RequestPackageDetailStatus
                    If Not statusToValidate.Contains(rmds.QualityStatus) OrElse readjustmentIds.Contains(rmds.Id) Then Continue For

                    Dim rmdMaterials = rawMaterials.Where(Function(m) m.RequestPackageDetailStatusId = rmds.Id).ToList()
                    Dim packageCost As Decimal = 0

                    For Each rm In rmdMaterials
                        If rm.ExpendQuantity <= 0 Then Continue For

                        Dim qVals = campaignValidations.Where(Function(v) v.ProductId = rm.ProductValidationId).ToList()
                        Dim qYes = qVals.Where(Function(v) Not v.Warehouse.ControlStore).Sum(Function(v) v.DeliveredQuantity - v.DevolutionQuantity)
                        Dim qAll = qVals.Sum(Function(v) v.DeliveredQuantity - v.DevolutionQuantity)

                        Dim pYes = If(qAll > 0, qYes / qAll, 0)
                        If pYes = 0 Then Continue For

                        Dim prod = inventoryProducts(rm.ProductValidationId)
                        Dim conv = unitConversions(rm.ProductValidationId)
                        Dim unitFrom = units(rm.MeasurementUnitId).Abbreviation
                        Dim unitTo = units(conv.MeasurementUnitId).Abbreviation

                        Dim adjustedQty = CDec(Utils.MeasureUnitConvert(unitFrom.ToLower(), unitTo.ToLower()) * rm.ExpendQuantity * pYes / conv.Concentration)
                        packageCost += prod.ProductCost * adjustedQty
                    Next

                    If rmd.PackagePersonalizedId IsNot Nothing Then
                        packageCosts.Add(New PackageCostModel With {.RequestPackageDetailStatusId = rmds.Id, .PackageId = rmd.PackageId.Value, .PackagePersonalizedId = rmd.PackagePersonalizedId, .ProductId = 0, .Cost = packageCost})
                    ElseIf rmd.PackageId IsNot Nothing Then
                        packageCosts.Add(New PackageCostModel With {.RequestPackageDetailStatusId = rmds.Id, .PackageId = rmd.PackageId.Value, .PackagePersonalizedId = 0, .ProductId = 0, .Cost = packageCost})
                    Else
                        Dim doseType = CType(GetUnitTypeDoseClassByCampaignDetail(campaignDetail), EUnitDoseTypeClass)
                        If Not {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(doseType) Then
                            Throw New IndigoValidationException("El producto no tiene un paquete asociado")
                        End If

                        If rmdMaterials.Any() Then
                            packageCosts.Add(New PackageCostModel With {
                        .RequestPackageDetailStatusId = rmds.Id,
                            .PackageId = 0,
                            .PackagePersonalizedId = 0,
                            .ProductId = rmdMaterials.First().ProductValidationId,
                            .Cost = packageCost
                        })
                        End If
                    End If
                Next
            Next

            ' 5. Cálculo de porcentajes de costo
            Dim totalCost = packageCosts.Sum(Function(m) m.Cost)
            If totalCost > 0 Then
                Dim sobrante As Decimal = 0
                For i = 0 To packageCosts.Count - 1
                    Dim pc = packageCosts(i)
                    Dim p = pc.Cost / totalCost
                    pc.Percentage = Math.Round(p, 4)
                    sobrante += p - pc.Percentage
                    If i = packageCosts.Count - 1 Then pc.Percentage += Math.Round(sobrante, 10)
                Next
            End If

            ' 6. Agregación de materia prima indirecta
            packageCosts = AddIndirectRawMaterialCosts(packageCosts, campaignDetail.Id, campaignValidations, msClass)

            Return New ActionResult(Of List(Of PackageCostModel)) With {.StateResult = True, .ObjectEmbbeded = packageCosts}
        Catch ex As Exception
            Return New ActionResult(Of List(Of PackageCostModel)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Suma el valor de la materia prima indirecta (MP Indirecta) al costo de los paquetes existentes,
    ''' distribuyéndolo proporcionalmente con base en el porcentaje de cada paquete.
    ''' </summary>
    ''' <param name="packageCosts">Lista actual de modelos de costo de paquete</param>
    ''' <param name="campaignDetailId">ID del detalle de campaña asociado</param>
    ''' <param name="campaignValidations">Lista de validaciones de la campaña</param>
    ''' <param name="msClass">Tipo de dosis unitaria (MSClass). Para NPT (MSClass = 2) usa VolumeMeasureUnit</param>
    ''' <returns>Lista actualizada de modelos de costo de paquete con MP indirecta incluida</returns>
    Private Function AddIndirectRawMaterialCosts(
    packageCosts As List(Of PackageCostModel),
    campaignDetailId As Integer,
    campaignValidations As List(Of CampaignDetailValidation),
    msClass As Integer) As List(Of PackageCostModel)

        ' --- 1. Obtener los movimientos de materia prima indirecta ---
        Dim kardexEntries = _campaignKardexRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId AndAlso
        (m.EntityName = GetType(IndirectMPQuantity).Name OrElse
         m.EntityName = GetType(Harnessed).Name OrElse
         m.EntityName = GetType(QuantityRemaining).Name)).
         Select(Function(m) New With {m.ProductId, m.BatchSerialId, m.MovementType, m.Quantity}).ToList()

        If Not kardexEntries.Any() Then
            Return packageCosts ' No hay nada que sumar
        End If

        ' --- 2. Obtener productos únicos y sus costos ---
        Dim indirectProductIds = kardexEntries.Select(Function(k) k.ProductId).Distinct().ToList()
        Dim productCosts = _inventoryProductRepository.GetByFilter(Function(p) indirectProductIds.Contains(p.Id)).
        ToDictionary(Function(p) p.Id, Function(p) p.ProductCost)

        ' --- 3. Obtener unidades de medida y concentraciones ---
        Dim unitConversions = _inventoryService.GetMeasurementUnitsAndConcentrationsByProductIds(indirectProductIds, msClass).
        ToDictionary(Function(c) c.ProductValidationId)

        ' --- 4. Agrupación por producto/lote y cálculo de valor total indirecto ---
        Dim totalIndirectCost As Decimal = 0

        For Each group In kardexEntries.GroupBy(Function(k) New With {k.ProductId, k.BatchSerialId})
            Dim productId = group.Key.ProductId
            Dim batchId = group.Key.BatchSerialId

            ' Saltar si pertenece a un almacén de control
            Dim validation = campaignValidations.FirstOrDefault(Function(v) CBool(v.ProductId = productId AndAlso v.BatchSerialId = batchId))
            If validation IsNot Nothing AndAlso validation.Warehouse.ControlStore Then Continue For

            If Not productCosts.ContainsKey(productId) OrElse Not unitConversions.ContainsKey(productId) Then Continue For

            Dim cost = productCosts(productId)
            Dim concentration = unitConversions(productId).Concentration

            ' Cálculo neto de cantidad ajustada (tipo movimiento: 1 = salida negativa, 2 = entrada positiva)
            Dim quantity = group.Sum(Function(k) If(k.MovementType = 1, -k.Quantity, k.Quantity))
            Dim costComponent = cost * quantity / concentration
            totalIndirectCost += costComponent
        Next

        ' --- 5. Distribuir el costo proporcionalmente a los paquetes existentes ---
        If totalIndirectCost > 0 Then
            For Each pc In packageCosts
                pc.Cost += Math.Round(pc.Percentage * totalIndirectCost, 4)
            Next
        End If

        Return packageCosts
    End Function

    ''' <summary>
    ''' Validaciones para terminar la campaña optimizada
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    Public Function LoadAndValidateCampaign(campaignDetailId As Integer) As ActionResult(Of CampaignDetail)
        Try
            Dim campaign = _campaingDetailRepository.FirstOrDefault(
            Function(m) m.Id = campaignDetailId,
            True,
            {"Campaign", "CampaignDetailValidation.Warehouse", "RequestMixingStationDetail.RequestPackageDetailStatus", "RequestMixingStationDetail.Package"})

            If campaign Is Nothing Then
                Throw New IndigoValidationException("Campaña no encontrada")
            End If

            ' 2. Validar estado procesado
            If campaign.CampaignStatus <> 5 Then
                Throw New IndigoValidationException("La Campaña no se encuentra procesada")
            End If

            ' 3. Validar existencia de solicitudes
            If Not campaign.RequestMixingStationDetail.Any() Then
                Throw New IndigoValidationException("La Campaña no tiene solicitudes")
            End If

            ' 4. Validar que todas las solicitudes tengan al menos un estado
            If campaign.RequestMixingStationDetail.Any(Function(m) Not m.RequestPackageDetailStatus.Any()) Then
                Throw New IndigoValidationException("Existen productos que no tienen un estado")
            End If

            ' 5. Validar estados permitidos en todas las solicitudes
            Dim allowedStatuses = New HashSet(Of Byte)({3, 4, 5, 6})
            If campaign.RequestMixingStationDetail.Any(Function(m) Not m.RequestPackageDetailStatus.All(Function(o) allowedStatuses.Contains(o.Status))) Then
                Throw New IndigoValidationException("La campaña no puede ser terminada, existen productos en estado no finalizado")
            End If

            ' 6. Validar liberación de línea de producción
            If Not _releaseLineRepository.Any(Function(m) m.CampaignDetailId = campaign.Id) Then
                Throw New IndigoValidationException("La línea de producción no se encuentra liberada aún")
            End If

            ' 7. Validar devoluciones pendientes
            Dim devolucionesPendientes = _rawMaterialDevolutionRepository.Query(
            Function(m) m.CampaignDetailId = campaign.Id AndAlso m.State = 1, False).Select(Function(m) m.Code).ToList()

            If devolucionesPendientes.Any() Then
                Throw New IndigoValidationException($"La Campaña #{campaign.CampaignNumber} tiene devoluciones pendientes por Confirmar: {String.Join(",", devolucionesPendientes)}")
            End If

            ' 8. Validar que no existan saldos de materia prima
            Dim haySaldo = HasRawMaterialInventoryBalance(campaign.Id)

            If haySaldo Then
                Throw New IndigoValidationException($"La Campaña #{campaign.CampaignNumber} Tiene saldos de Inventario de Materia Prima.")
            End If

            Return New ActionResult(Of CampaignDetail) With {.StateResult = True, .ObjectEmbbeded = campaign}
        Catch ex As Exception
            Return New ActionResult(Of CampaignDetail) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Consulta materia prima teniendo en cuenta el balacen entre adecuaciones activas y anuladas
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Private Function HasRawMaterialInventoryBalance(campaignDetailId As Integer) As Boolean
        Dim kardexRows = _campaignKardexRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId, False)
        If kardexRows Is Nothing Then Return False

        Dim kardex = kardexRows.ToList()
        If Not kardex.Any() Then Return False

        Dim rawMaterialEntityName = GetType(CampaignRawMaterial).Name
        Dim activeRawMaterialRows = _campaignRawMaterialRepository.Query(
            Function(m) m.RequestPackageDetailStatus.RequestMixingStationDetail.CampaignDetailId = campaignDetailId _
                        AndAlso m.RequestPackageDetailStatus.RequestMixingStationDetail.Status <> 3 _
                        AndAlso m.RequestPackageDetailStatus.Status <> 6,
            False
        ).Select(Function(m) New With {
            .ProductId = m.ProductValidationId,
            .Quantity = m.ExpendQuantity
        }).ToList()

        Dim activeRawMaterialByProduct = activeRawMaterialRows _
            .GroupBy(Function(m) m.ProductId) _
            .ToDictionary(Function(g) g.Key, Function(g) g.Sum(Function(m) m.Quantity))

        Dim productIds = kardex.Select(Function(m) m.ProductId) _
            .Union(activeRawMaterialByProduct.Keys) _
            .Distinct() _
            .ToList()

        For Each productId In productIds
            Dim inputQuantity = kardex _
                .Where(Function(m) m.ProductId = productId AndAlso m.MovementType = CInt(eMovementType.Input)) _
                .Sum(Function(m) m.Quantity)
            Dim outputQuantity = kardex _
                .Where(Function(m) m.ProductId = productId _
                                   AndAlso m.MovementType = CInt(eMovementType.Output) _
                                   AndAlso m.EntityName <> rawMaterialEntityName) _
                .Sum(Function(m) m.Quantity)
            Dim activeRawMaterialQuantity = If(activeRawMaterialByProduct.ContainsKey(productId), activeRawMaterialByProduct(productId), 0D)

            If inputQuantity - outputQuantity - activeRawMaterialQuantity <> 0D Then
                Return True
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' Guarda la campaña y los items RequestMixingStationDetail
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="listPackageDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveCampaignAndMixingStationDetailAsync(objParams As String, listPackageDetail As List(Of RequestMixingStationDetail), audit As AuditMessage) As Task(Of ActionResult(Of SP_SaveCampaign_Result)) Implements ICampaignAdminService.SaveCampaignAndMixingStationDetailAsync
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                            New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted},
                                            TransactionScopeAsyncFlowOption.Enabled)

                ' Guardar detalles de paquetes
                Dim resMix = Await SavePackageDetailStatusAsync(audit, listPackageDetail)
                If Not resMix.StateResult Then
                    Return New ActionResult(Of SP_SaveCampaign_Result) With {.StateResult = False, .Message = resMix.Message}
                End If

                ' Guardar campaña
                Dim res = SaveCampaign(objParams, audit)
                If Not res.StateResult Then
                    Return New ActionResult(Of SP_SaveCampaign_Result) With {.StateResult = False, .Message = res.Message}
                End If

                ' Confirmar transacción
                scope.Complete()
                Return res
            End Using

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_SaveCampaign_Result) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveCampaign(objParams As String, audit As AuditMessage) As ActionResult(Of SP_SaveCampaign_Result) Implements ICampaignAdminService.SaveCampaign
        Dim args As Object = Utils.DeserializeJsonToObject(objParams)
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(args)
                If ValidationCampaingStatus(args) Then
                    scope.Dispose()
                    Return New ActionResult(Of SP_SaveCampaign_Result) With {.StateResult = False, .Message = " Refresque el Dasboard por favor, la Campaña Podria haber cambiado de estado; o esta intentando mezclar solicitudes Internas con externas"}
                End If

                Dim result = _CampaignRepository.SP_SaveCampaign(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope.Dispose()
                    Return New ActionResult(Of SP_SaveCampaign_Result) With {.StateResult = False, .Message = result.Message}
                End If

                scope.Complete()
                Return New ActionResult(Of SP_SaveCampaign_Result) With {.StateResult = True, .ObjectEmbbeded = result, .Message = result.Message}
            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of SP_SaveCampaign_Result) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    Private Function ValidationCampaingStatus(args As Object) As Boolean
        Dim listCampaingDetailId As New List(Of Integer)
        Dim CampainStatus As Byte = Nothing
        Dim CampaingDetailOriginId = CType(args.Details, List(Of Object)).FirstOrDefault.CampaingDetailOriginId
        Dim RequestMixingStationDetailId = CType(args.Details, List(Of Object)).FirstOrDefault.StringIds
        Dim Flag As Boolean = False
        Dim RequestMixingStationDetail As New RequestMixingStationDetail
        Dim ListRequestMixingStationDetail As New List(Of RequestMixingStationDetail)

        If CampaingDetailOriginId IsNot Nothing Then
            listCampaingDetailId.Add(CampaingDetailOriginId)
            Flag = True
        End If
        If args.CampaignDetailId > 0 Then
            listCampaingDetailId.Add(args.CampaignDetailId)
            For Each Item In listCampaingDetailId
                CampainStatus = _campaingDetailRepository.GetCampaignDetailById(Item).CampaignStatus
                If CampainStatus <> 1 Then
                    Return True
                End If
            Next
        End If
        If listCampaingDetailId IsNot Nothing AndAlso listCampaingDetailId.Count > 0 Then
            If Flag = False Then
                RequestMixingStationDetail = _requestMixingStationDetailRepository.GetRequestMixingStationDetailById(RequestMixingStationDetailId)
            End If

            Dim listInt As List(Of Integer) = New List(Of Integer) From {1, 4}
            Dim ListExt As List(Of Integer) = New List(Of Integer) From {2, 3}

            ListRequestMixingStationDetail = _requestMixingStationDetailRepository.GetRequestMixingStationDetailByCampaignDetailId(listCampaingDetailId)

            If RequestMixingStationDetail IsNot Nothing AndAlso RequestMixingStationDetail.Id > 0 Then
                ListRequestMixingStationDetail.Add(RequestMixingStationDetail)
            End If

            If (From i In ListRequestMixingStationDetail Where listInt.Contains(i.Source) Select i).Count > 0 And (From x In ListRequestMixingStationDetail Where ListExt.Contains(x.Source) Select x).Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function ConvertEntityToXml(args As Object) As String
        Dim builder As New StringBuilder()

        builder.Append("<Data>")
        builder.Append("<Id>" & args.Id & "</Id>")
        builder.Append("<CMConfigurationId>" & args.CMConfigurationId & "</CMConfigurationId>")
        builder.Append("<ProductionLineId>" & args.ProductionLineId & "</ProductionLineId>")
        builder.Append("<UnitDoseTypeId>" & args.UnitDoseTypeId & "</UnitDoseTypeId>")
        builder.Append("<CampaignDetailId>" & args.CampaignDetailId & "</CampaignDetailId>")

        For Each item In CType(args.Details, List(Of Object))
            builder.Append("<Details>")
            builder.Append("<SourceType>" & item.SourceType & "</SourceType>")
            builder.Append("<StringIds>" & item.StringIds & "</StringIds>")
            builder.Append("<RequestMixingStationDetailId>" & item.RequestMixingStationDetailId & "</RequestMixingStationDetailId>")
            builder.Append("</Details>")
        Next

        builder.Append("</Data>")

        Return builder.ToString()
    End Function

    Public Function GetCampaignById(id As Integer) As ActionResult(Of Campaign) Implements ICampaignAdminService.GetCampaignById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim Campaign As Campaign = Me._CampaignRepository.GetCampaignById(id)
            Return New ActionResult(Of Campaign) With {.StateResult = True, .ObjectEmbbeded = Campaign}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Campaign) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetCampaignDetailById(id As Integer, Optional ValidateProcessRawMaterial As Boolean = False) As ActionResult(Of CampaignDetail) Implements ICampaignAdminService.GetCampaignDetailById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim CampaignDetail As CampaignDetail = Me._campaingDetailRepository.GetCampaignDetailById(id)
            If ValidateProcessRawMaterial Then
                If Not {2, 5}.Contains(CampaignDetail.CampaignStatus) Then
                    Return New ActionResult(Of CampaignDetail) With {.StateResult = False, .Message = "La campaña debe estar en estado Cerrada o Procesada para Procesar Materia Prima"}
                End If
                If CampaignDetail.RequestMixingStationDetail.All(Function(x) x.Status = 3) Then
                    Return New ActionResult(Of CampaignDetail) With {.StateResult = False, .Message = "No se puede Procesar Materia Prima, debido a que la solicitud esta Anulada"}
                End If
            End If
            Return New ActionResult(Of CampaignDetail) With {.StateResult = True, .ObjectEmbbeded = CampaignDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CampaignDetail) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina la campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="TransactionalContainer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteCampaign(campaignDetailId As Integer, TransactionalContainer As String, audit As AuditMessage) As ActionResult Implements ICampaignAdminService.DeleteCampaign
        If campaignDetailId = 0 Then
            Throw New ArgumentNullException("campaignDetailId")
        End If
        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, TransactionalContainer, False))
        cnx.Open()
        Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
        Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
        command.CommandTimeout = 30000
        command.CommandType = CommandType.Text

        Try
            'Se actualizan las solicitudes
            command.CommandText = "update MixingStation.RequestMixingStationDetail set CampaignDetailId = null where CampaignDetailId = " + campaignDetailId.ToString()
            command.ExecuteNonQuery()

            'Se actualizan las solicitudes
            command.CommandText = "update MixingStation.RequestMixingStationDetailPatients set Status = 1, CampaignDetailId = null where CampaignDetailId = " + campaignDetailId.ToString()
            command.ExecuteNonQuery()

            'Se elimina la campaña
            command.CommandText = "delete from MixingStation.CampaignDetail where Id = " + campaignDetailId.ToString()
            command.ExecuteNonQuery()

            tx.Commit()
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            tx.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Finally
            cnx.Close()
        End Try
    End Function

    ''' <summary>
    ''' retorna un listado de status
    ''' </summary>
    ''' <param name="ids"></param>
    ''' <returns></returns>
    Public Function GetRequestMixingDetailStatusByIds(ids As List(Of Integer)) As List(Of RequestPackageDetailStatus) Implements ICampaignAdminService.GetRequestMixingDetailStatusByIds
        Return _RequestPackageDetailStatusRepository.GetByFilter(Function(m) ids.Contains(m.Id)).ToList()
    End Function


    ''' <summary>
    ''' Guardo los paquetes de forma indivdual por cantidad. 
    ''' </summary>
    ''' <param name="listPackageDetail"></param>
    ''' <returns></returns>
    Public Async Function SavePackageDetailStatusAsync(
        audit As AuditMessage,
        Optional listPackageDetail As List(Of RequestMixingStationDetail) = Nothing,
        Optional PackageDetailStatus As List(Of RequestPackageDetailStatus) = Nothing
    ) As Task(Of ActionResult) Implements ICampaignAdminService.SavePackageDetailStatusAsync
        Try
            ' Validación inicial: si viene lista de estados, validar duplicados en estado PT
            If PackageDetailStatus?.Any() Then
                Const blockSize As Integer = 5000
                Dim ids = PackageDetailStatus.Select(Function(x) x.Id).ToList()
                Dim validation = New List(Of RequestPackageDetailStatus)()

                For i As Integer = 0 To ids.Count - 1 Step blockSize
                    Dim subIds = ids.Skip(i).Take(blockSize).ToList()
                    Dim subList = _RequestPackageDetailStatusRepository _
                                .Query(Function(m) subIds.Contains(m.Id) AndAlso m.Status = 2) _
                                .ToList()
                    validation.AddRange(subList)
                Next

                If validation.Any() Then
                    Return New ActionResult With {
                    .StateResult = False,
                    .Message = "Uno de los productos seleccionados ya se encuentra en estado PT. Por favor cierre y abra nuevamente la rejilla."
                }
                End If
            End If

            ' Transacción
            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                           New TransactionOptions With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted},
                                           TransactionScopeAsyncFlowOption.Enabled)

                ' Si no hay estados, crear detalles nuevos
                If PackageDetailStatus Is Nothing OrElse Not PackageDetailStatus.Any() Then
                    If listPackageDetail Is Nothing OrElse Not listPackageDetail.Any() Then
                        Throw New ArgumentNullException(NameOf(listPackageDetail))
                    End If

                    Dim result = Await createPackageDetailStatus(listPackageDetail)
                    If Not result.StateResult Then
                        Throw New IndigoValidationException(result.Message)
                    End If

                Else ' Si hay estados, validarlos y guardarlos
                    Dim ids = PackageDetailStatus.Select(Function(m) m.RequestMixingStationDetailId).Distinct().ToList()

                    Dim exists = _RequestPackageDetailStatusRepository.Any(Function(m) ids.Contains(m.RequestMixingStationDetailId) AndAlso
                                                                      m.Status = 2 AndAlso
                                                                      Not m.RequestMixingStationDetail.LabelType.HasValue)

                    If exists Then
                        Throw New IndigoValidationException("Hay paquetes a los cuales no se les ha gestionado la etiqueta.")
                    End If

                    Await _RequestPackageDetailStatusRepository.SaveEntityMassiveAsync(PackageDetailStatus)
                End If

                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using

        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}

        Catch ex As OptimisticConcurrencyException
            Return New ActionResult With {
                .StateResult = False,
                .MessageResult = {ex.Message}.ToList(),
                .Message = ResourceManager.GetString("ErrorConcurrence")
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {
                .StateResult = False,
                .MessageResult = {ex.Message}.ToList(),
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

    Public Function GetCampaignDetailPickingByCampaignDetailId(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailPicking)) Implements ICampaignAdminService.GetCampaignDetailPickingByCampaignDetailId
        Try
            Dim data = _campaignDetailPickingRepository.GetCampaignDetailPickingByCampaignDetailId(campaignDetailId, False)

            Return New ActionResult(Of List(Of CampaignDetailPicking)) With {.StateResult = True, .ObjectEmbbeded = data}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CampaignDetailPicking)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetCampaignDetailValidationByCampaignDetailId(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailValidation)) Implements ICampaignAdminService.GetCampaignDetailValidationByCampaignDetailId
        Try
            Dim data = _CampaignDetailValidationRepository.GetCampaignDetailValidationByCampaignDetailId(campaignDetailId)

            Return New ActionResult(Of List(Of CampaignDetailValidation)) With {.StateResult = True, .ObjectEmbbeded = data}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CampaignDetailValidation)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GetCampaignDetailValidationForDevolution(campaignDetailId As Integer) As ActionResult(Of List(Of CampaignDetailValidation)) Implements ICampaignAdminService.GetCampaignDetailValidationForDevolution
        Try
            Dim data = _CampaignDetailValidationRepository.GetCampaignDetailValidationForDevolution(campaignDetailId)

            Return New ActionResult(Of List(Of CampaignDetailValidation)) With {.StateResult = True, .ObjectEmbbeded = data}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CampaignDetailValidation)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Guarda los items de picking
    ''' </summary>
    ''' <param name="campaignDetailItem"></param>
    ''' <param name="campaignDetailItemsPicking"></param>
    ''' <param name="stockWareHouseId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCampaignDetailItem(campaignDetailItem As CampaignDetailItems, campaignDetailItemsPicking As List(Of CampaignDetailPicking), stockWareHouseId As Integer, audit As AuditMessage) As ActionResult Implements ICampaignAdminService.SaveCampaignDetailPickingList
        Try
            If campaignDetailItemsPicking Is Nothing OrElse Not campaignDetailItemsPicking.Any() Then
                Throw New ArgumentNullException("campaignDetailItems")
            End If

            If campaignDetailItem Is Nothing Then
                Throw New Exception("Detalle no enviado")
            End If

            If campaignDetailItemsPicking.GroupBy(Function(m) m.CampaignDetailId).Count() > 1 Then
                Throw New Exception("Se ha enviado datos de más de un detalle")
            End If

            Dim uow = _campaignDetailItemRepository.UnitWork

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim campaignDetailId = campaignDetailItem.CampaignDetailId

                For Each picking In campaignDetailItemsPicking
                    If picking.Id > 0 Then
                        picking.MarkAsModified()
                        picking.ModificationUser = audit.CodeUser
                        picking.ModificationDate = Date.Now
                    Else
                        picking.MarkAsAdded()
                        picking.CreationUser = audit.CodeUser
                        picking.CreationDate = Date.Now
                        picking.ModificationUser = audit.CodeUser
                        picking.ModificationDate = Date.Now
                    End If

                    _campaignDetailPickingRepository.SaveEntity(picking)
                Next

                Dim cmWarehouseStock = _cmWarehouseRepository.GetByTypeAndCampaignDetailId(campaignDetailId, 1)
                Dim cmWarehouseWarehouse = _cmWarehouseRepository.GetByTypeAndCampaignDetailId(campaignDetailId, 2)

                campaignDetailItem.QuantityWarehouse = 0
                campaignDetailItem.QuantityStock = 0

                Dim productIds As List(Of Integer) = Nothing

                If campaignDetailItem.AtcId.HasValue Then
                    Dim products = _inventoryProductRepository.GetByFilter(Function(m) m.ATCId = campaignDetailItem.AtcId)
                    productIds = products?.Select(Function(m) m.Id).ToList()
                ElseIf campaignDetailItem.SupplyId.HasValue Then
                    Dim products = _inventoryProductRepository.GetByFilter(Function(m) m.SupplieId = campaignDetailItem.SupplyId)
                    productIds = products?.Select(Function(m) m.Id).ToList()
                Else
                    productIds = {campaignDetailItem.ProductId.Value}.ToList()
                End If

                If productIds IsNot Nothing Then
                    campaignDetailItem.QuantityWarehouse = campaignDetailItemsPicking _
                        .Where(Function(m) productIds.Contains(m.ProductId) AndAlso m.WarehouseId = cmWarehouseWarehouse.IdWarehouse) _
                        .Sum(Function(m) m.Quantity)

                    campaignDetailItem.QuantityStock = campaignDetailItemsPicking _
                        .Where(Function(m) productIds.Contains(m.ProductId) AndAlso m.WarehouseId = cmWarehouseStock.IdWarehouse) _
                        .Sum(Function(m) m.Quantity)
                End If

                _campaignDetailItemRepository.SaveEntity(campaignDetailItem)
                uow.Commit()

                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Creo los Detalles del Paquete según la cantidad solicitada.
    ''' </summary>
    ''' <param name="listPackageDetail"></param>
    ''' <returns></returns>
    Public Async Function createPackageDetailStatus(listPackageDetail As List(Of RequestMixingStationDetail)) As Task(Of ActionResult(Of RequestPackageDetailStatus))
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                            New TransactionOptions With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted},
                                            TransactionScopeAsyncFlowOption.Enabled)

                Dim requestPackageDetailStatusList As New List(Of RequestPackageDetailStatus)()

                ' IDs para consultas
                Dim mixingDetailIds = listPackageDetail.Select(Function(x) x.Id).ToList()

                ' Carga datos relacionados
                Dim mixingDetails = _requestMixingStationDetailRepository.Query(Function(m) mixingDetailIds.Contains(m.Id), tracking:=False).ToList()
                Dim entityIds = mixingDetails.Select(Function(x) x.EntityId).ToList()
                Dim packageStatuses = _RequestPackageDetailStatusRepository.Query(Function(m) entityIds.Contains(m.Id), tracking:=False).ToList()

                For Each item In listPackageDetail
                    Dim confirmation = _confirmationUnitDoseRepository.FirstOrDefault(Function(m) m.RequestMixingStationDetailId = item.Id, tracking:=False)
                    Dim relatedDetail = mixingDetails.First(Function(r) r.Id = item.Id)

                    For i As Integer = 1 To item.Quantity
                        Dim packageDetail = New RequestPackageDetailStatus With {
                        .RequestMixingStationDetailId = item.Id,
                        .PackageId = item.PackageId,
                        .PackagePersonalizedId = item.PackagePersonalizedId,
                        .Status = 1 ' PP
                    }

                        ' Asignar código de agrupación de dosis si aplica
                        If confirmation IsNot Nothing Then
                            packageDetail.GroupingCodeDose = confirmation.GroupingCodeDose
                        ElseIf relatedDetail?.EntityName = NameOf(RequestPackageDetailStatus) Then
                            packageDetail.GroupingCodeDose = packageStatuses.FirstOrDefault(Function(m) m.Id = relatedDetail.EntityId)?.GroupingCodeDose
                        End If

                        requestPackageDetailStatusList.Add(packageDetail)
                    Next
                Next

                ' Guardar si hay registros
                If requestPackageDetailStatusList.Any() Then
                    Await _RequestPackageDetailStatusRepository.SaveEntityMassiveAsync(requestPackageDetailStatusList)
                End If

                scope.Complete()
                Return New ActionResult(Of RequestPackageDetailStatus) With {.StateResult = True}

            End Using

        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of RequestPackageDetailStatus) With {
                .StateResult = False,
                .MessageResult = {ex.Message}.ToList(),
                .Message = ResourceManager.GetString("ErrorConcurrence")
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestPackageDetailStatus) With {
                .StateResult = False,
                .MessageResult = {ex.Message}.ToList(),
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

    ''' <summary>
    ''' Guarda los items de validacion de lotes
    ''' </summary>
    ''' <param name="campaignDetailItem"></param>
    ''' <param name="campaignDetailValidation"></param>
    ''' <param name="stockWareHouseId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCampaignDetailValidationList(campaignDetailItem As CampaignDetailItems, campaignDetailValidation As List(Of CampaignDetailValidation), stockWareHouseId As Integer, audit As AuditMessage) As ActionResult Implements ICampaignAdminService.SaveCampaignDetailValidationList
        Try
            If campaignDetailValidation Is Nothing OrElse Not campaignDetailValidation.Any() Then
                Throw New ArgumentNullException("campaignDetailItems")
            End If

            If campaignDetailItem Is Nothing Then
                Throw New Exception("Detalle no enviado")
            End If

            If campaignDetailValidation.GroupBy(Function(m) m.CampaignDetailId).Count() > 1 Then
                Throw New Exception("Se ha enviado datos de más de un detalle")
            End If

            Dim campaignDetailId As Integer = campaignDetailValidation(0).CampaignDetailId
            Dim ProductId As Integer = campaignDetailValidation(0).ProductId

            ' Ajustes de Cantidades: la suma de la cantidades confirmadas y las cantidades enviadas no deben ser mayor a la cantidad requeridad.
            Dim detailValidation = _CampaignDetailValidationRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetailId AndAlso m.ProductId = ProductId, False)
            If detailValidation.GroupBy(Function(m) m.ProductId) _
                                               .Any(Function(m) m.Sum(Function(o) o.TransferOrderQuantity) + campaignDetailValidation.Sum(Function(d) d.DeliveredQuantity) > campaignDetailItem.RequestQuantity) Then
                Throw New IndigoValidationException("La cantidad a entregar por todos los productos supera la cantidad solicitada")
            End If

            Dim uow = _campaingDetailRepository.UnitWork
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted})

                For Each ItemValidation In campaignDetailValidation
                    If ItemValidation.Id > 0 Then
                        ItemValidation.DeliveredQuantity += ItemValidation.TransferOrderQuantity
                        ItemValidation.MarkAsModified()
                        ItemValidation.ModificationUser = audit.CodeUser
                        ItemValidation.ModificationDate = Date.Now
                    Else
                        ItemValidation.MarkAsAdded()
                        ItemValidation.CreationUser = audit.CodeUser
                        ItemValidation.CreationDate = Date.Now
                        ItemValidation.ModificationUser = audit.CodeUser
                        ItemValidation.ModificationDate = Date.Now
                    End If

                    _CampaignDetailValidationRepository.SaveEntity(ItemValidation)
                Next

                uow.Commit()
                scope.Complete()

                Return New ActionResult With {.StateResult = True}
            End Using

        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Confirma el etiquetado de los items
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="mixingLabelItems"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ConfirmLabelItems(campaignDetailId As Integer, mixingLabelItems As List(Of MixingLabelModel), audit As AuditMessage) As ActionResult Implements ICampaignAdminService.ConfirmLabelItems
        Try
            If mixingLabelItems Is Nothing OrElse Not mixingLabelItems.Any() Then
                Throw New ArgumentNullException("mixingLabelItems")
            End If

            Dim campaign = _campaingDetailRepository.FindById(campaignDetailId)

            If campaign Is Nothing Then
                Throw New ArgumentException("Campaña no encontrada")
            End If

            If campaign.LabelConfirmationDate IsNot Nothing Then
                Throw New ArgumentException("La Campaña ya se encuentra etiquetada")
            End If

            If campaign.CampaignStatus <> 5 Then
                Throw New ArgumentException("La Campaña no se encuentra en proceso")
            End If

            Dim uow = _requestMixingStationDetailRepository.UnitWork

            Using scope As New TransactionScope(
                TransactionScopeOption.Required, New TransactionOptions() With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                })


                For Each requestMixingStationModel In mixingLabelItems
                    Dim requestDetail = _requestMixingStationDetailRepository.FindById(requestMixingStationModel.RequestMixingStationDetailId)

                    If requestDetail IsNot Nothing Then
                        requestDetail.LabelType = requestMixingStationModel.LabelType
                        _requestMixingStationDetailRepository.SaveEntity(requestDetail)
                    End If
                Next

                campaign.LabelConfirmationDate = DateTime.Now
                campaign.ModificationUser = audit.CodeUser
                campaign.ModificationDate = DateTime.Now
                _campaingDetailRepository.SaveEntity(campaign)

                uow.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valido los almacenes para procesos fuera de la Central de Mezclas
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <returns></returns>
    Public Function ValidateWarehouseMS(objParams As String) As ActionResult Implements ICampaignAdminService.ValidateWarehouseMS
        Dim args As Object = Utils.DeserializeJsonToObject(objParams)
        Try

            If Not objParams.Any() Then Throw New ArgumentNullException("Data")

            Dim sourceWarehouseId As Integer = args.SourceWarehouseId
            Dim sourceTypeName As String = args.SourceTypeName
            Dim targetWarehouseId As Integer = args.TargetWarehouseId
            Dim formModule As String = args.FormModule

            Dim sbErrors As New StringBuilder()

            'Valido el almacén de Origen
            Dim sourceWarehouse As Warehouse = _inventoryWarehouseRepository _
                .FirstOrDefault(Function(m) m.Id = sourceWarehouseId)

            If sourceWarehouse Is Nothing Then Throw New IndigoValidationException($"El Almacén de Origen: {sourceTypeName} no existe.")
            If sourceWarehouse.Status = False Then Throw New IndigoValidationException($"El Almacén de Origen: {sourceTypeName} Se encuentra inactivo")
            If sourceWarehouse.VirtualStore = True Then Throw New IndigoValidationException($"El Almacén de Origen: {sourceTypeName} Es un Almacén Virtual")
            If sourceWarehouse.WarehouseConsignment = True Then Throw New IndigoValidationException($"El Almacén de Origen: {sourceTypeName} Es un Almacén de Consignación ")
            If sourceWarehouse.CustodyStore = True Then Throw New IndigoValidationException($"El Almacén de Origen: {sourceTypeName} Es un Almacén de Custodia ")
            If sourceWarehouse.TransitStore = True Then Throw New IndigoValidationException($"El Almacén de Origen: {sourceTypeName} Es un Almacén de tránsito ")
            If sourceWarehouse.ControlStore = True Then Throw New IndigoValidationException($"El Almacén de Origen: {sourceTypeName} Es un Almacén de control ")


            'Valido el almacén de Destino
            Dim targetWarehouse As Warehouse = _inventoryWarehouseRepository _
                .FirstOrDefault(Function(m) m.Id = targetWarehouseId)

            If formModule <> "FrmDashboardQualityControl" Then
                Dim targetTypeName As String = args.TargetTypeName
                If targetWarehouse Is Nothing Then Throw New IndigoValidationException($"El Almacén de Destino: {targetTypeName} no existe.")
                If targetWarehouse.Status = False Then Throw New IndigoValidationException($"El Almacén de Destino: {targetTypeName} Se encuentra inactivo")
                If targetWarehouse.VirtualStore = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetTypeName} Es un Almacén Virtual")
                If targetWarehouse.WarehouseConsignment = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetTypeName} Es un Almacén de Consignación ")
                If targetWarehouse.CustodyStore = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetTypeName} Es un Almacén de Custodia ")
                If targetWarehouse.TransitStore = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetTypeName} Es un Almacén de tránsito ")
                If targetWarehouse.ControlStore = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetTypeName} Es un Almacén de control ")
            Else
                If targetWarehouse Is Nothing Then Throw New IndigoValidationException($"El Almacén de Destino: {targetWarehouse.Name} no existe.")
                If targetWarehouse.Status = False Then Throw New IndigoValidationException($"El Almacén de Destino: {targetWarehouse.Name} Se encuentra inactivo")
                If targetWarehouse.VirtualStore = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetWarehouse.Name} Es un Almacén Virtual")
                If targetWarehouse.WarehouseConsignment = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetWarehouse.Name} Es un Almacén de Consignación ")
                If targetWarehouse.CustodyStore = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetWarehouse.Name} Es un Almacén de Custodia ")
                If targetWarehouse.TransitStore = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetWarehouse.Name} Es un Almacén de tránsito ")
                If targetWarehouse.ControlStore = True Then Throw New IndigoValidationException($"El Almacén de Destino: {targetWarehouse.Name} Es un Almacén de control ")
            End If

            'Valido el almacén de Transito
            If formModule = "FrmDashboardQualityControl" Then
                Dim transitWarehouseId As Integer = args.TransitWarehouseId

                Dim transitWarehouse As Warehouse = _inventoryWarehouseRepository _
                    .FirstOrDefault(Function(m) m.Id = transitWarehouseId)

                If targetWarehouse Is Nothing Then Throw New IndigoValidationException($"El Almacén de Tránsito: {transitWarehouse.Name} de los Parámetros la Central de Mezclas no existe.")
                If targetWarehouse.Status = False Then Throw New IndigoValidationException($"El Almacén de Tránsito: {transitWarehouse.Name} de los Parámetros la Central de Mezclas se encuentra inactivo")
                If targetWarehouse.VirtualStore = True Then Throw New IndigoValidationException($"El Almacén de Tránsito: {transitWarehouse.Name} de los Parámetros la Central de Mezclas es un Almacén Virtual")
                If targetWarehouse.WarehouseConsignment = True Then Throw New IndigoValidationException($"El Almacén de Tránsito: {transitWarehouse.Name} de los Parámetros la Central de Mezclas es un Almacén de Consignación ")
                If targetWarehouse.CustodyStore = True Then Throw New IndigoValidationException($"El Almacén de Tránsito: {transitWarehouse.Name} de los Parámetros la Central de Mezclas es un Almacén de Custodia ")
            End If
            Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "FrmManageRawMaterial"

#Region "Get"
    ''' <summary>
    ''' Gestiona la Materia Prima segun la cantidad Solicitada
    ''' </summary>
    ''' <param name="requestMixingStationDetailIdAndQuantity"></param>
    ''' <returns></returns>
    Public Function GetProductDetailByCampaignId(packageDetailStatusIds As List(Of Integer), requestMixingStationDetailIdAndQuantity As Tuple(Of Integer, Integer)) As ActionResult(Of ManageRawMaterialModel) Implements ICampaignAdminService.GetProductDetailByCampaignId
        Try
            If requestMixingStationDetailIdAndQuantity Is Nothing Then Throw New ArgumentNullException("Data")

            Dim requestMixingStationDetail As RequestMixingStationDetail = _requestMixingStationDetailRepository.GetRequestMixingStationDetailByIdAsNoTracking(requestMixingStationDetailIdAndQuantity.Item2)

            If requestMixingStationDetail Is Nothing Then Throw New ArgumentException("Solicitud no encontrada")

            Dim CampaignValidation As List(Of CampaignDetailValidation) = _CampaignDetailValidationRepository.GetCampaignDetailValidationByCampaignDetailId(requestMixingStationDetail.CampaignDetailId.Value)

            If Not CampaignValidation.Any() Then Throw New ArgumentException("Productos No Encontrados")

            Dim campaignValidationItems = (From so In CampaignValidation
                                           Group so By so.ProductId, so.BatchSerialId Into Group
                                           Select New ProductTotalDelivered With {
                                               .ProductId = Group(0).ProductId,
                                               .BatchSerialId = Group(0).BatchSerialId,
                                               .TotaledDelivered = Group.Sum(Function(m) m.DeliveredQuantity)
                                            }).ToList()

            Dim productItems As List(Of ProductTotalDelivered) = GetAtcIdOrSuppliedId(campaignValidationItems)
            'Obtengo el paquete de la Solicitud
            Dim productDetailList = CreateRawMaterialProductDetail(
                requestMixingStationDetail:=requestMixingStationDetail,
                packageDetailStatusIds:=packageDetailStatusIds,
                quantity:=requestMixingStationDetailIdAndQuantity.Item1,
                ProductList:=productItems,
                requestMixingStationDetailId:=requestMixingStationDetailIdAndQuantity.Item2,
                totalPackages:=requestMixingStationDetail.Quantity.Value)

            Return New ActionResult(Of ManageRawMaterialModel) With {.StateResult = True, .Data = productDetailList}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ManageRawMaterialModel) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para pasar un PackagePersonalizedDetail a PackageDetail
    ''' </summary>
    ''' <returns></returns>
    Private Function ConvertPackage(PackagePersonalizedDetail As List(Of PackagePersonalizedDetail)) As List(Of PackageDetail)
        Return PackagePersonalizedDetail.Select(Function(ls As PackagePersonalizedDetail)
                                                    Dim package As New PackageDetail
                                                    With package
                                                        .Id = ls.Id
                                                        .PackageId = ls.PackagePersonalizedId
                                                        .ProductId = ls.ProductId
                                                        .Quantity = ls.Quantity
                                                        .MeasurementUnitId = ls.MeasurementUnitId
                                                        .Volume = ls.Volume
                                                        .VolumeMeasureUnit = ls.VolumeMeasureUnit
                                                        .AtcId = ls.AtcId
                                                        .SupplieId = ls.SupplieId
                                                        .ComponentType = ls.ComponentType
                                                        '.ATC = ls.ATC
                                                        '.InventoryProduct = ls.InventoryProduct
                                                        '.InventorySupplie = ls.InventorySupplie
                                                    End With
                                                    package.MarkAsUnchanged()
                                                    package.StopTracking()
                                                    Return package
                                                End Function).ToList()
    End Function

    ''' <summary>
    ''' Consulto el inventoryProduct por Id para validar luego validar con el Paquete (ATCId o el SuplliedId).
    ''' </summary>
    ''' <param name="campaignValidationItems"></param>
    ''' <returns></returns>
    Private Function GetAtcIdOrSuppliedId(campaignValidationItems As List(Of ProductTotalDelivered)) As List(Of ProductTotalDelivered)
        campaignValidationItems.ForEach(Sub(ls As ProductTotalDelivered)
                                            Dim product = _inventoryProductRepository.FirstOrDefault(Function(m) m.Id = ls.ProductId, includes:={
                                                    "ATC.ATCEntity.ATC",
                                                    "ATC.InventoryMeasurementUnit",
                                                    "ATC.InventoryMeasurementUnit1",
                                                    "ATC.InventoryMeasurementUnit2",
                                                    "ProductType"
                                                })
                                            With ls
                                                If product.ATCId IsNot Nothing Then
                                                    .AtcId = product.ATCId
                                                    .AtcEntityId = product.ATC.ATCEntityId
                                                    .FormulationType = product.ATC.FormulationType
                                                    .Weight = product.ATC.Weight
                                                    .WeightMeasureUnit = product.ATC.WeightMeasureUnit
                                                    .Volume = product.ATC.Volume
                                                    .VolumeMeasureUnit = product.ATC.VolumeMeasureUnit

                                                    .MeasureUnitAbreviation = If(product.ATC.InventoryMeasurementUnit IsNot Nothing, product.ATC.InventoryMeasurementUnit.Abbreviation,
                                                                                If(product.ATC.InventoryMeasurementUnit1 IsNot Nothing, product.ATC.InventoryMeasurementUnit1.Abbreviation,
                                                                                                                                        product.ATC.InventoryMeasurementUnit2.Abbreviation))
                                                Else
                                                    .SuppliedId = product.SupplieId
                                                    .MeasureUnitAbreviation = product.InventoryMeasurementUnit?.Abbreviation
                                                End If

                                                .MeasureUnitId = If(product.ATC?.FormulationType = 4, product.ATC.ConcentrationMeasureUnitId, product.MeasurementUnitId)
                                                .ProducType = product.ProductType.Class
                                                .ProductFullName = product.Name
                                                .ProductCode = product.Code
                                            End With
                                        End Sub)
        Return campaignValidationItems
    End Function

    ''' <summary>
    ''' Funcion para crear el paquete con sus Detalles
    ''' </summary>
    ''' <param name="requestMixingStationDetail"></param>
    ''' <param name="quantity"></param>
    ''' <returns></returns>
    Private Function CreateRawMaterialProductDetail(
        requestMixingStationDetail As RequestMixingStationDetail,
        packageDetailStatusIds As List(Of Integer),
        quantity As Integer,
        ProductList As List(Of ProductTotalDelivered),
        requestMixingStationDetailId As Integer,
        totalPackages As Integer) As List(Of ManageRawMaterialModel)

        Dim packageDetailList As New List(Of ManageRawMaterialModel)()
        Dim packageDetail As New List(Of PackageDetail)

        If requestMixingStationDetail.PackagePersonalizedId.HasValue Then
            Dim packagePersonalizedDetail As List(Of PackagePersonalizedDetail) = (From e In requestMixingStationDetail.PackagePersonalized.PackagePersonalizedDetail Select e).ToList()
            packageDetail = ConvertPackage(packagePersonalizedDetail)
        Else
            If {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(CType(requestMixingStationDetail.UnitDoseType.MSClass, EUnitDoseTypeClass)) Then 'Reempaque o Reenvase
                Dim atc = _atcRepository.Query(Function(m) m.Id = requestMixingStationDetail.ATCId.Value).FirstOrDefault()

                packageDetail.Add(New PackageDetail With {
                    .AtcId = requestMixingStationDetail.ATCId,
                    .Quantity = CDec((requestMixingStationDetail.Quantity / packageDetailStatusIds.Count) * If(atc.Weight, atc.Volume)),
                    .MeasurementUnitId = requestMixingStationDetail.ATC.WeightMeasureUnit
                })
            Else
                packageDetail = (From e In requestMixingStationDetail.Package.PackageDetail Select e).ToList()
            End If
        End If

        Dim ListMeasurementUnit = New List(Of InventoryMeasurementUnit)

        If packageDetail?.Any(Function(s) s.MeasurementUnitId IsNot Nothing) Then
            Dim ids = packageDetail _
            .Where(Function(x) x.MeasurementUnitId IsNot Nothing OrElse x.VolumeMeasureUnit IsNot Nothing) _
            .SelectMany(Function(x) New List(Of Integer?) From {x.MeasurementUnitId, x.VolumeMeasureUnit}) _
            .Where(Function(id) id IsNot Nothing) _
            .Distinct() _
            .ToList()

            ListMeasurementUnit = _measurementUnitRepository? _
                                    .GetByFilter(Function(m) ids.Contains(m.Id), False).ToList()
        End If

        Dim listProduct = New List(Of InventoryProduct)
        If packageDetail?.Any(Function(x) x.ProductId IsNot Nothing) Then
            Dim ids = packageDetail.FindAll(Function(x) x.ProductId IsNot Nothing).Select(Function(e) e.ProductId)?.ToList()
            listProduct = _inventoryProductRepository.GetByFilter(Function(m) ids.Contains(m.Id), False).ToList()
        End If

        Dim listATC = New List(Of ATC)
        If packageDetail?.Any(Function(x) x.AtcId IsNot Nothing) Then
            Dim ids = packageDetail.FindAll(Function(x) x.AtcId IsNot Nothing).Select(Function(e) e.AtcId)?.ToList()
            listATC = _atcRepository.GetByFilter(Function(m) ids.Contains(m.Id), False).ToList()
        End If

        Dim listSupply = New List(Of InventorySupplie)
        If packageDetail?.Any(Function(x) x.SupplieId IsNot Nothing) Then
            Dim ids = packageDetail.FindAll(Function(x) x.SupplieId IsNot Nothing).Select(Function(e) e.SupplieId)?.ToList()
            listSupply = _inventorySupplieRepository.GetByFilter(Function(m) ids.Contains(m.Id), False).ToList()
        End If

        packageDetail.ForEach(Sub(pd As PackageDetail)
                                  Dim measurementUnit = If(pd.MeasurementUnitId IsNot Nothing,
                                                            ListMeasurementUnit.FirstOrDefault(Function(m) m.Id = pd.MeasurementUnitId.Value),
                                                            ListMeasurementUnit.FirstOrDefault(Function(m) m.Id = pd.VolumeMeasureUnit.Value))

                                  Dim validationDetail As List(Of ProductTotalDelivered) = Nothing
                                  Dim productDetails As New ManageRawMaterialModel()
                                  With productDetails
                                      .Id = pd.Id
                                      .RequestMixingStationDetailId = requestMixingStationDetailId
                                      .ProductId = pd.ProductId
                                      .SuppliedId = pd.SupplieId
                                      .AtcId = pd.AtcId
                                      .TotalProducts = totalPackages
                                      .TotalProductsProcess = quantity

                                      If pd.AtcId.HasValue Then
                                          Dim atc = listATC.FirstOrDefault(Function(m) m.Id = pd.AtcId.Value)

                                          If atc IsNot Nothing Then
                                              .ProductFullName = String.Format("{0} - {1}", atc.Code, atc.Name)
                                              If ProductList IsNot Nothing Then
                                                  validationDetail = (From so In ProductList Where so.AtcId = pd.AtcId Select so).ToList()
                                              End If
                                          End If
                                      End If

                                      If pd.ProductId.HasValue Then
                                          Dim product = listProduct.FirstOrDefault(Function(m) m.Id = pd.ProductId.Value)

                                          If product IsNot Nothing Then
                                              .ProductFullName = String.Format("{0} - {1}", product.Code, product.Name)
                                              If ProductList IsNot Nothing Then
                                                  validationDetail = (From so In ProductList Where so.ProductId = pd.ProductId Select so).ToList()
                                              End If
                                          End If
                                      End If

                                      If pd.SupplieId.HasValue Then
                                          Dim supply = listSupply.FirstOrDefault(Function(m) m.Id = pd.SupplieId.Value)

                                          If supply IsNot Nothing Then
                                              .ProductFullName = String.Format("{0} - {1}", supply.Code, supply.SupplieName)
                                              If ProductList IsNot Nothing Then
                                                  validationDetail = (From so In ProductList Where so.SuppliedId = pd.SupplieId Select so).ToList()
                                              End If
                                          End If
                                      End If

                                      .RequiredQuantity = pd.Quantity * packageDetailStatusIds.Count
                                      .MeasureUnitId = measurementUnit.Id
                                      .MeasureUnitAbreviation = measurementUnit?.Abbreviation

                                      If validationDetail IsNot Nothing AndAlso validationDetail.Count > 0 Then 'Agrego los Detalles
                                          Dim msClassValue As Integer = If(requestMixingStationDetail.UnitDoseType?.MSClass, 0)
                                          Dim campaignValidationDetail = CreateConversionUnit(
                                            campaignDetailId:=requestMixingStationDetail.CampaignDetailId.Value,
                                            requestMeasurementUnitId:=measurementUnit.Id,
                                            validationDetail:=validationDetail,
                                            quantity:=quantity,
                                            totalPackages:=totalPackages,
                                            packageQuantity:= .RequiredQuantity.Value,
                                            msClass:=msClassValue
                                          )

                                          .CampaignQuantity = campaignValidationDetail.Sum(Function(m) m.CampaignQuantity)
                                          .CampaignBalanceQuantity = campaignValidationDetail.Sum(Function(m) m.CampaignBalanceQuantity)
                                          .UsedQuantity = campaignValidationDetail.Sum(Function(m) m.UsedQuantity)
                                          .PendingQuantity = .RequiredQuantity - campaignValidationDetail.Sum(Function(m) m.UsedQuantity)
                                          .totalQuantityRequired = campaignValidationDetail(0).TotalRequiredQuantity
                                          .CampaignDetailValidation = campaignValidationDetail
                                      Else
                                          .UsedQuantity = 0
                                          .CampaignQuantity = 0
                                          .CampaignBalanceQuantity = 0
                                          .PendingQuantity = 0
                                          .totalQuantityRequired = 0
                                          .CampaignDetailValidation = Nothing
                                      End If
                                  End With
                                  packageDetailList.Add(productDetails)
                              End Sub)
        Return packageDetailList
    End Function

    ''' <summary>
    '''  Unidad de Medidad del Deatalle a la Solicitud  
    ''' </summary>
    ''' <param name="validationDetail"></param>
    ''' <param name="msClass">Tipo de dosis unitaria (MSClass). Para NPT (MSClass = 2) se usa la unidad de medida del volumen.</param>
    ''' <returns></returns>
    Private Function CreateConversionUnit(
        campaignDetailId As Integer,
        validationDetail As List(Of ProductTotalDelivered),
        requestMeasurementUnitId As Integer?,
        quantity As Integer,
        totalPackages As Integer,
        packageQuantity As Decimal,
        Optional msClass As Integer = 0
    ) As List(Of ProductTotalDelivered)
        Return validationDetail.Select(Function(ls As ProductTotalDelivered)
                                           Dim newConversion = New ProductTotalDelivered
                                           Dim measurementUnitRequest = _measurementUnitRepository.FirstOrDefault(Function(m) m.Id = requestMeasurementUnitId.Value)
                                           ' Para NPT (MSClass = 2), usar primero la unidad de medida del volumen
                                           Dim measurementUnitUsedId = If(msClass = 2,
                                               If(If(ls.VolumeMeasureUnit, ls.WeightMeasureUnit), ls.MeasureUnitId),
                                               If(If(ls.WeightMeasureUnit, ls.VolumeMeasureUnit), ls.MeasureUnitId))
                                           Dim measurementUnitUsed = _measurementUnitRepository.FirstOrDefault(Function(m) m.Id = measurementUnitUsedId.Value)
                                           Dim quantityConverted = Utils.MeasureUnitConvert(measurementUnitUsed.Abbreviation, measurementUnitRequest.Abbreviation)
                                           Dim expendQuantitiesL = _campaignRawMaterialRepository _
                                            .Query(Function(m) m.ProductValidationId = ls.ProductId _
                                                   AndAlso ((ls.BatchSerialId.HasValue AndAlso m.BatchSerialId = ls.BatchSerialId) _
                                                            OrElse (Not ls.BatchSerialId.HasValue AndAlso Not m.BatchSerialId.HasValue)) _
                                                   AndAlso m.RequestPackageDetailStatus.RequestMixingStationDetail.CampaignDetailId.Value = campaignDetailId) _
                                            .Select(Function(m) m.ExpendQuantity)?.ToList()
                                           Dim expendQuantities As Decimal = 0

                                           If expendQuantitiesL.Any() Then expendQuantities = quantityConverted * expendQuantitiesL.Sum(Function(m) m)

                                           Dim weightVolumeConverted As Decimal = 0

                                           If ls.ProducType = 3 Then 'Insumos Medicos
                                               weightVolumeConverted = 1
                                           End If

                                           ' Para NPT (MSClass = 2), siempre usar Volume independientemente del FormulationType
                                           If msClass = 2 Then
                                               If ls.Volume.HasValue AndAlso ls.Volume.Value > 0 Then
                                                   weightVolumeConverted = ls.Volume.Value * quantityConverted
                                               ElseIf ls.Weight.HasValue Then
                                                   weightVolumeConverted = ls.Weight.Value * quantityConverted
                                               End If
                                           Else
                                               Select Case ls.FormulationType
                                                   Case 1, 3
                                                       weightVolumeConverted = ls.Weight.Value * quantityConverted
                                                   Case 2
                                                       weightVolumeConverted = ls.Volume.Value * quantityConverted
                                                   Case 4
                                                       weightVolumeConverted = 1
                                               End Select
                                           End If

                                           With newConversion
                                               .FormulationType = ls.FormulationType
                                               .BatchSerialId = ls.BatchSerialId
                                               .ProductId = ls.ProductId
                                               .ProductCode = ls.ProductCode
                                               .ProductFullName = ls.ProductFullName
                                               .DeliveredQuantity = weightVolumeConverted * ls.TotaledDelivered
                                               .CampaignQuantity = .DeliveredQuantity - expendQuantities

                                               Dim usedQuantity = .CampaignQuantity

                                               If packageQuantity > usedQuantity And ls.FormulationType <> 4 Then
                                                   .UsedQuantity = usedQuantity
                                                   packageQuantity -= usedQuantity.Value
                                               Else
                                                   .UsedQuantity = packageQuantity
                                                   packageQuantity = 0
                                               End If
                                               Dim QuantityRequired = If(weightVolumeConverted = 0, 0, packageQuantity / weightVolumeConverted)
                                               .CampaignBalanceQuantity = If(ls.FormulationType <> 4, .CampaignQuantity - .UsedQuantity, 0)
                                               .MeasureUnitAbreviation = If(ls.FormulationType = 4, measurementUnitUsed.Abbreviation, ls.MeasureUnitAbreviation)
                                               .VolumeMeasureUnit = ls.VolumeMeasureUnit
                                               .WeightMeasureUnit = ls.WeightMeasureUnit
                                               .MeasureUnitId = measurementUnitUsedId
                                               .TotalRequiredQuantity = QuantityRequired ''packageQuantity / weightVolumeConverted
                                           End With

                                           Return newConversion
                                       End Function)?.ToList()
    End Function
#End Region

#Region "Save"
    ''' <summary>
    ''' Guarda la gestion de la Materia Prima
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial"></param>
    ''' <param name="productPackageIds"></param>
    ''' <param name="RequestMixingStationDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveManageCampaignRawMaterialsAsync(myListCampaignRawMaterial As List(Of ManageRawMaterialModel),
                                                          productPackageIds As List(Of Integer),
                                                          RequestMixingStationDetailId As Integer,
                                                          audit As AuditMessage) As Task(Of ActionResult) Implements ICampaignAdminService.SaveManageCampaignRawMaterialsAsync
        Try
            ' Validaciones iniciales claras
            If myListCampaignRawMaterial Is Nothing OrElse Not myListCampaignRawMaterial.Any() Then
                Throw New IndigoValidationException("listCampaignRawMaterial")
            End If
            If productPackageIds Is Nothing OrElse Not productPackageIds.Any() Then
                Throw New IndigoValidationException("Productos no encontrados")
            End If
            If RequestMixingStationDetailId = 0 Then
                Throw New IndigoValidationException("Codigo Solicitud no encontrado")
            End If

            ' Obtener solicitud
            Dim RequestMixingStationDetail = _requestMixingStationDetailRepository.GetRequestMixingStationDetailByIdAsNoTracking(RequestMixingStationDetailId)
            If RequestMixingStationDetail Is Nothing Then
                Throw New IndigoValidationException("Solicitud no encontrada")
            End If

            ' Obtener estados de paquete por lotes de 5000
            Dim RequestPackageDetailStatus = New List(Of RequestPackageDetailStatus)
            For i = 0 To productPackageIds.Count - 1 Step 5000
                Dim batch = productPackageIds.Skip(i).Take(5000).ToList()
                Dim batchData = _RequestPackageDetailStatusRepository.GetListPackageDetailStatus(batch, Nothing)
                RequestPackageDetailStatus.AddRange(batchData)
            Next

            If Not RequestPackageDetailStatus.Any() Then
                Throw New IndigoValidationException("RequestPackageDetailStatus")
            End If

            ValidateRawMaterialAssignment(myListCampaignRawMaterial, RequestPackageDetailStatus.Count)

            ' Crear entidades a guardar
            Dim CampaingRawMaterial = CreateCampaingRawMaterial(myListCampaignRawMaterial, RequestPackageDetailStatus, audit)
            ValidateGeneratedRawMaterialAssignment(CampaingRawMaterial, myListCampaignRawMaterial, RequestPackageDetailStatus)

            Dim CampaignKardex = CreateCampaignKardex(myListCampaignRawMaterial, RequestPackageDetailStatus, RequestMixingStationDetail.CampaignDetailId, audit)

            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                            New TransactionOptions With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted
                                            },
                                            TransactionScopeAsyncFlowOption.Enabled)

                ' Guardar CampaignRawMaterial
                If CampaingRawMaterial IsNot Nothing AndAlso CampaingRawMaterial.Any() Then
                    Await _campaignRawMaterialRepository.SaveEntityMassiveAsync(CampaingRawMaterial)
                End If

                ' Guardar Kardex
                If CampaignKardex IsNot Nothing AndAlso CampaignKardex.Any() Then
                    For Each kardex In CampaignKardex
                        _CampaignKardex.Savekardex(Of CampaignRawMaterial)(
                        kardex.CampaignDetailId,
                        eMovementType.Output,
                        kardex.ProductId,
                        kardex.BatchSerialId,
                        kardex.Quantity,
                        kardex.MeasurementUnitId,
                        "Gestion de Materia Prima",
                        audit)
                    Next
                End If

                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using

        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valida que la materia prima disponible cubra todos los componentes requeridos por las preparaciones seleccionadas.
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial">Componentes de materia prima calculados para la gestion de la solicitud.</param>
    ''' <param name="requestStatusCount">Cantidad de preparaciones que se intentan gestionar.</param>
    Private Sub ValidateRawMaterialAssignment(myListCampaignRawMaterial As List(Of ManageRawMaterialModel), requestStatusCount As Integer)
        Dim errors As New StringBuilder()
        Const tolerance As Decimal = 0.000001D

        For Each item In myListCampaignRawMaterial.Where(Function(m) m.RequiredQuantity.HasValue AndAlso m.RequiredQuantity.Value > 0D)
            Dim componentName = If(String.IsNullOrWhiteSpace(item.ProductFullName), GetRawMaterialComponentName(item), item.ProductFullName)
            Dim usedQuantity = item.UsedQuantity.GetValueOrDefault(0D)
            Dim pendingQuantity = item.RequiredQuantity.Value - usedQuantity

            If item.CampaignDetailValidation Is Nothing OrElse Not item.CampaignDetailValidation.Any() Then
                errors.AppendLine($"No se encontró materia prima validada para el componente {componentName}. Cantidad requerida: {item.RequiredQuantity.Value}.")
                Continue For
            End If

            If usedQuantity <= tolerance OrElse pendingQuantity > tolerance Then
                errors.AppendLine($"La materia prima validada para el componente {componentName} no cubre las {requestStatusCount} preparación(es) seleccionada(s). Requerido: {item.RequiredQuantity.Value}, disponible para asignar: {usedQuantity}.")
            End If
        Next

        If errors.Length > 0 Then
            Throw New IndigoValidationException($"No se puede guardar la gestión de materia prima porque no es congruente con los componentes del paquete.{Environment.NewLine}{errors.ToString().Trim()}")
        End If
    End Sub

    ''' <summary>
    ''' Obtiene un nombre descriptivo de respaldo para el componente de materia prima cuando no se cargo el nombre completo.
    ''' </summary>
    ''' <param name="item">Componente de materia prima a describir.</param>
    ''' <returns>Nombre descriptivo del componente.</returns>
    Private Function GetRawMaterialComponentName(item As ManageRawMaterialModel) As String
        If item.AtcId.HasValue Then Return $"ATC {item.AtcId.Value}"
        If item.SuppliedId.HasValue Then Return $"Insumo {item.SuppliedId.Value}"
        If item.ProductId.HasValue Then Return $"Producto {item.ProductId.Value}"
        Return "sin identificar"
    End Function

    ''' <summary>
    ''' Valida que la asignacion generada incluya todos los componentes requeridos para cada preparacion.
    ''' </summary>
    ''' <param name="campaignRawMaterials">Registros de materia prima generados antes de guardar.</param>
    ''' <param name="myListCampaignRawMaterial">Componentes requeridos por el paquete gestionado.</param>
    ''' <param name="requestPackageDetailStatus">Preparaciones seleccionadas para la gestion de materia prima.</param>
    Private Sub ValidateGeneratedRawMaterialAssignment(campaignRawMaterials As List(Of CampaignRawMaterial),
                                                       myListCampaignRawMaterial As List(Of ManageRawMaterialModel),
                                                       requestPackageDetailStatus As List(Of RequestPackageDetailStatus))
        Dim errors As New StringBuilder()
        Dim requiredComponents = myListCampaignRawMaterial _
            .Where(Function(m) m.RequiredQuantity.HasValue AndAlso m.RequiredQuantity.Value > 0D) _
            .ToList()

        For Each status In requestPackageDetailStatus
            For Each item In requiredComponents
                Dim existsRawMaterial = campaignRawMaterials.Any(Function(m) m.RequestPackageDetailStatusId = status.Id AndAlso
                                                                     m.ExpendQuantity > 0D AndAlso
                                                                     HasSameRawMaterialComponent(m, item))

                If Not existsRawMaterial Then
                    Dim componentName = If(String.IsNullOrWhiteSpace(item.ProductFullName), GetRawMaterialComponentName(item), item.ProductFullName)
                    errors.AppendLine($"La preparación {status.BatchCode} no tiene materia prima asignada para el componente {componentName}.")
                End If
            Next
        Next

        If errors.Length > 0 Then
            Throw New IndigoValidationException($"No se puede guardar la gestión de materia prima porque la asignación generada no coincide con los componentes del paquete.{Environment.NewLine}{errors.ToString().Trim()}")
        End If
    End Sub

    ''' <summary>
    ''' Determina si un registro generado corresponde al mismo componente requerido por el paquete.
    ''' </summary>
    ''' <param name="rawMaterial">Registro de materia prima generado.</param>
    ''' <param name="item">Componente requerido por el paquete.</param>
    ''' <returns>True si ambos representan el mismo ATC, insumo o producto.</returns>
    Private Function HasSameRawMaterialComponent(rawMaterial As CampaignRawMaterial, item As ManageRawMaterialModel) As Boolean
        If item.AtcId.HasValue Then
            Return rawMaterial.AtcId.HasValue AndAlso rawMaterial.AtcId.Value = item.AtcId.Value
        End If

        If item.SuppliedId.HasValue Then
            Return rawMaterial.SupplieId.HasValue AndAlso rawMaterial.SupplieId.Value = item.SuppliedId.Value
        End If

        If item.ProductId.HasValue Then
            Return rawMaterial.ProductId.HasValue AndAlso rawMaterial.ProductId.Value = item.ProductId.Value
        End If

        Return False
    End Function

    ''' <summary>
    '''  Asignamos los valores a la Entidad  CampaingRawMaterial
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial"></param>
    ''' <returns></returns>
    Private Function CreateCampaingRawMaterial(myListCampaignRawMaterial As List(Of ManageRawMaterialModel),
                                           RequestPackageDetailStatus As List(Of RequestPackageDetailStatus),
                                           audit As AuditMessage) As List(Of CampaignRawMaterial)

        Dim campaignRawMaterials As New List(Of CampaignRawMaterial)()
        Dim measurementUnitCache As New Dictionary(Of Integer, InventoryMeasurementUnit)()

        For Each status In RequestPackageDetailStatus

            For Each model In myListCampaignRawMaterial.Where(Function(m) m.CampaignDetailValidation IsNot Nothing AndAlso m.CampaignDetailValidation.Any())

                For Each itemValidation In model.CampaignDetailValidation

                    ' Cargar y cachear unidades de medida
                    If Not measurementUnitCache.ContainsKey(itemValidation.MeasureUnitId) Then
                        Dim idsToLoad = model.CampaignDetailValidation _
                                    .Where(Function(x) Not measurementUnitCache.ContainsKey(x.MeasureUnitId)) _
                                    .Select(Function(x) x.MeasureUnitId).Distinct().ToList()

                        Dim loadedUnits = _measurementUnitRepository.Query(Function(m) idsToLoad.Contains(m.Id), False).ToList()
                        For Each unit In loadedUnits
                            measurementUnitCache(unit.Id) = unit
                        Next
                    End If

                    Dim measurementUnit = measurementUnitCache(itemValidation.MeasureUnitId)
                    ' Los insumos medicos se gestionan por cantidad unitaria del suplido, no por conversion farmacologica.
                    Dim conversion = If(model.SuppliedId.HasValue, 1D, Utils.MeasureUnitConvert(model.MeasureUnitAbreviation, measurementUnit.Abbreviation))

                    itemValidation.totalProductQuantity = model.TotalProducts
                    Dim quantityPerItem = model.RequiredQuantity / model.TotalProductsProcess

                    Dim expendQuantity As Decimal

                    Dim remainingQty = itemValidation.UsedQuantity - itemValidation.TotalUsed
                    Dim isLastStatus = (RequestPackageDetailStatus.IndexOf(status) = RequestPackageDetailStatus.Count - 1)

                    If isLastStatus AndAlso remainingQty > 0 Then
                        expendQuantity = If(itemValidation.FormulationType = 4, remainingQty, (conversion * remainingQty))
                        itemValidation.TotalUsed = itemValidation.UsedQuantity
                    ElseIf remainingQty > quantityPerItem Then
                        expendQuantity = conversion * quantityPerItem
                        itemValidation.TotalUsed += quantityPerItem
                    Else
                        If remainingQty < 0 Then
                            itemValidation.TotalUsed = itemValidation.UsedQuantity
                        End If

                        Dim usedQuantity As Decimal = If(itemValidation.UsedQuantity, 0D)
                        Dim remainingQuantity As Decimal = usedQuantity - itemValidation.TotalUsed
                        expendQuantity = conversion * Math.Max(0D, remainingQuantity)
                        itemValidation.TotalUsed += itemValidation.UsedQuantity
                    End If

                    itemValidation.Conversion = conversion

                    If expendQuantity <= 0D Then Continue For

                    campaignRawMaterials.Add(New CampaignRawMaterial With {
                        .RequestPackageDetailStatusId = status.Id,
                        .ProductId = model.ProductId,
                        .AtcId = model.AtcId,
                        .SupplieId = model.SuppliedId,
                        .MeasurementUnitId = itemValidation.MeasureUnitId,
                        .ProductValidationId = itemValidation.ProductId,
                        .BatchSerialId = itemValidation.BatchSerialId,
                        .ExpendQuantity = expendQuantity,
                        .CreationUser = audit.CodeUser,
                        .CreationDate = Date.Now
                    })

                Next
            Next
        Next

        Return campaignRawMaterials
    End Function

    ''' <summary>
    ''' Asignamos los valores a la Entidad de CampaignKardex
    ''' </summary>
    ''' <param name="myListCampaignRawMaterial"></param>
    ''' <param name="RequestPackageDetailStatus"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function CreateCampaignKardex(myListCampaignRawMaterial As List(Of ManageRawMaterialModel), RequestPackageDetailStatus As List(Of RequestPackageDetailStatus), campaingDetailId As Integer, audit As AuditMessage) As List(Of CampaignKardex)
        Dim CampaignKardex = New List(Of CampaignKardex)()

        For Each ls In myListCampaignRawMaterial.FindAll(Function(m) m.CampaignDetailValidation IsNot Nothing AndAlso m.CampaignDetailValidation.Any())
            For Each ItemValidation In ls.CampaignDetailValidation
                Dim newCampaignKardexDetail = New CampaignKardex()
                With newCampaignKardexDetail
                    .CampaignDetailId = campaingDetailId
                    .MovementType = 2
                    .MovementDate = DateTime.Now
                    .Description = "Gestion de Materia Prima"
                    .EntityName = "CampaignRawMaterial"
                    .MeasurementUnitId = ItemValidation.MeasureUnitId 'ls.MeasureUnitId
                    .ProductId = ItemValidation.ProductId
                    .BatchSerialId = ItemValidation.BatchSerialId
                    .Quantity = ItemValidation.UsedQuantity * ItemValidation.Conversion
                    .CreationUser = audit.CodeUser
                    .CreationDate = DateTime.Now
                End With
                CampaignKardex.Add(newCampaignKardexDetail)
            Next
        Next
        Return CampaignKardex.FindAll(Function(m) m.Quantity > 0)
    End Function

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _CampaignRepository = Nothing
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
