'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-09
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Application.Inventory.TransferOrder
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities.Service
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class RawMaterialDevolutionAdminService
    Implements IRawMaterialDevolutionAdminService, Inject

    Private Const FORM_NAME As String = "FrmRawMaterialDevolution"

    ''' <summary>
    ''' Atc repository
    ''' </summary>
    Private ReadOnly _atcRepository As IATCRepository

    Private ReadOnly _inventoryService As IInventoryService

    ''' <summary>
    ''' campaignRepository
    ''' </summary>
    Private ReadOnly _campaignDetailRepository As ICampaignDetailRepository

    ''' <summary>
    ''' picking repository
    ''' </summary>
    Private ReadOnly _pickingRepository As IPickingRepository

    ''' <summary>
    ''' cm config repository
    ''' </summary>
    Private ReadOnly _cMConfigRepository As ICMConfigRepository

    ''' <summary>
    ''' repositorio de tipos de producto
    ''' </summary>
    Private ReadOnly _productTypeRepository As IProductTypeRepository

    ''' <summary>
    ''' warehouse mixing station
    ''' </summary>
    Private ReadOnly _cMWarehouseRepository As ICMWarehouseRepository

    ''' <summary>
    ''' Servicio de orden de traslado
    ''' </summary>
    Private ReadOnly _transferOrderAdminService As ITransferOrderAdminService

    ''' <summary>
    ''' repositorio de productos
    ''' </summary>
    Private ReadOnly _inventoryProductRepository As IInventoryProductRepository

    ''' <summary>
    ''' campaign kardex service
    ''' </summary>
    Private ReadOnly _campaignKardexAdminService As ICampaignKardexAdminService

    ''' <summary>
    ''' physical inventory repository
    ''' </summary>
    Private ReadOnly _physicalInventoryRepository As IPhysicalInventoryRepository

    ''' <summary>
    ''' Sequence repository
    ''' </summary>
    Private ReadOnly _secuenseDetailRepository As IMixingStationSequenceDetailRepository
    ''' <summary>
    ''' Repositorio
    ''' </summary>
    Private ReadOnly _rawMaterialDevolutionRepository As IRawMaterialDevolutionRepository

    ''' <summary>
    ''' Campaign detail validation
    ''' </summary>
    Private ReadOnly _campaignDetailValidationRepository As ICampaignDetailValidationRepository

    ''' <summary>
    ''' detail repository
    ''' </summary>
    Private ReadOnly _rawMaterialDevolutionDetailRepository As IRawMaterialDevolutionDetailRepository

    Private ReadOnly _contractExternalClientsDetailRepository As IContractExternalClientsDetailRepository

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(
        atcRepository As IATCRepository,
        inventoryService As IInventoryService,
        pickingRepository As IPickingRepository,
        cMConfigRepository As ICMConfigRepository,
        cMWarehouseRepository As ICMWarehouseRepository,
        productTypeRepository As IProductTypeRepository,
        campaignDetailRepository As ICampaignDetailRepository,
        transferOrderAdminService As ITransferOrderAdminService,
        inventoryProductRepository As IInventoryProductRepository,
        campaignKardexAdminService As ICampaignKardexAdminService,
        physicalInventoryRepository As IPhysicalInventoryRepository,
        rawMaterialDevolutionRepository As IRawMaterialDevolutionRepository,
        secuenseDetailRepository As IMixingStationSequenceDetailRepository,
        campaignDetailValidationRepository As ICampaignDetailValidationRepository,
        rawMaterialDevolutionDetailRepository As IRawMaterialDevolutionDetailRepository,
        contractExternalClientsDetailRepository As IContractExternalClientsDetailRepository)

        _atcRepository = atcRepository
        _inventoryService = inventoryService
        _pickingRepository = pickingRepository
        _cMConfigRepository = cMConfigRepository
        _cMWarehouseRepository = cMWarehouseRepository
        _productTypeRepository = productTypeRepository
        _campaignDetailRepository = campaignDetailRepository
        _secuenseDetailRepository = secuenseDetailRepository
        _transferOrderAdminService = transferOrderAdminService
        _inventoryProductRepository = inventoryProductRepository
        _campaignKardexAdminService = campaignKardexAdminService
        _physicalInventoryRepository = physicalInventoryRepository
        _rawMaterialDevolutionRepository = rawMaterialDevolutionRepository
        _campaignDetailValidationRepository = campaignDetailValidationRepository
        _rawMaterialDevolutionDetailRepository = rawMaterialDevolutionDetailRepository
        _contractExternalClientsDetailRepository = contractExternalClientsDetailRepository
    End Sub

    ''' <summary>
    ''' Consulta una devolucion de materia prima por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRawMaterialDevolutionByCode(code As String, audit As AuditMessage) As ActionResult(Of RawMaterialDevolution) Implements IRawMaterialDevolutionAdminService.GetRawMaterialDevolutionByCode
        Try
            If code Is String.Empty Then
                Throw New ArgumentNullException("code")
            End If
            If audit Is Nothing Then
                Throw New ArgumentNullException("audit")
            End If

            Dim rawMaterialDevolution As RawMaterialDevolution = _rawMaterialDevolutionRepository.GetRawMaterialDevolutionByCode(code, False)

            If rawMaterialDevolution IsNot Nothing AndAlso rawMaterialDevolution.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RawMaterialDevolution)(rawMaterialDevolution, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = True, .ObjectEmbbeded = rawMaterialDevolution}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los detalles de devolucion de materia prima
    ''' </summary>
    ''' <param name="rawMatertialDevolutionId"></param>
    ''' <returns></returns>
    Public Function GetRawMaterialDevolutionDetailByRawMaterialDevolutionId(rawMatertialDevolutionId As Integer) As List(Of RawMaterialDevolutionDetail) Implements IRawMaterialDevolutionAdminService.GetRawMaterialDevolutionDetailByRawMaterialDevolutionId
        Try
            Dim rawMaterialDetailDevolutions = _rawMaterialDevolutionDetailRepository.GetRawMaterialDevolutionDetailByRawMaterialDevolutionId(rawMatertialDevolutionId, False)
            Dim validations = _campaignDetailValidationRepository.GetCampaignDetailValidationByCampaignDetailId(rawMaterialDetailDevolutions(0).RawMaterialDevolution.CampaignDetailId)

            If validations.Any() Then
                For Each v In validations
                    If Not rawMaterialDetailDevolutions.Any(Function(m) m.CampaignDetailValidationId = v.Id) Then
                        rawMaterialDetailDevolutions.Add(New RawMaterialDevolutionDetail With {
                            .RawMaterialDevolutionId = rawMatertialDevolutionId,
                            .CampaignDetailValidationId = v.Id,
                            .ProductId = v.ProductId,
                            .ProductCodeName = v.ProductFullName,
                            .BatchSerialId = v.BatchSerialId,
                            .BatchSerialCode = v.BatchSerialCode,
                            .DeliveredQuantity = v.DeliveredQuantity,
                            .DevolutionQuantity = v.DevolutionQuantity,
                            .Quantity = 0
                        })
                    End If
                Next
            End If

            Return rawMaterialDetailDevolutions
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Confirma una devolucion de materia prima
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ConfirmRawMaterialDevolution(rawMaterialDevolution As RawMaterialDevolution, operatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of RawMaterialDevolution) Implements IRawMaterialDevolutionAdminService.ConfirmRawMaterialDevolution
        If rawMaterialDevolution Is Nothing Then
            Throw New ArgumentNullException("rawMaterialDevolution")
        End If

        Dim uow As IUnitWork = Me._campaignDetailValidationRepository.UnitWork

        Try
            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                })

                Dim resultKardex = SaveCampaignKardex(rawMaterialDevolution.CampaignDetailId, rawMaterialDevolution.RawMaterialDevolutionDetail.ToList(), audit)

                If Not resultKardex.StateResult Then
                    scope.Dispose()
                    Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultKardex.Message}
                End If

                For Each i In rawMaterialDevolution.RawMaterialDevolutionDetail
                    _campaignDetailValidationRepository.FirstOrDefault(Function(m) m.Id = i.CampaignDetailValidationId, includes:={"Warehouse"})
                Next

                Dim itemsDevolutionControl = (From d In rawMaterialDevolution.RawMaterialDevolutionDetail Where d.CampaignDetailValidation.Warehouse.ControlStore Select d).ToList()

                Dim transferOrderCodes As New List(Of String)()

                If itemsDevolutionControl IsNot Nothing AndAlso itemsDevolutionControl.Any() Then
                    ' Obtenemos el almacén de control configurado en la central de mezclas
                    Dim campaignDetail = _campaignDetailRepository.FirstOrDefault(Function(m) m.Id = rawMaterialDevolution.CampaignDetailId, includes:={"Campaign"})
                    Dim cMConfiguration = _cMConfigRepository.FirstOrDefault(Function(m) m.Id = campaignDetail.Campaign.CMConfigurationId, includes:={"CMWarehouse"})
                    Dim wareHouseControlMixingStation = cMConfiguration.CMWarehouse.FirstOrDefault(Function(m) m.WarehouseType = 5)

                    If wareHouseControlMixingStation Is Nothing Then Throw New IndigoValidationException("No se encontró un almacén de Control en la central de mezclas")

                    ' buscamos el almacén de maquila que está configurado en el contrato del centro de atencion externo
                    Dim contract = _contractExternalClientsDetailRepository.GetContractExternalClientByCampaignDetailId(rawMaterialDevolution.CampaignDetailId)

                    ' Generar orden de traslado del almacen de producción al del stock
                    Dim tOrderControl = GetTransferOrder(
                        rawMaterialDevolutionCode:=rawMaterialDevolution.Code,
                        sourceWareHouseId:=wareHouseControlMixingStation.IdWarehouse,
                        targetWarehouseId:=contract.WarehouseId,
                        rawMaterialDevolutionDetails:=itemsDevolutionControl,
                        operatingUnitId:=operatingUnitId,
                        audit:=audit
                    )

                    Dim resultTOrderControl = _transferOrderAdminService.SaveTrasnferOrder(tOrderControl, audit)

                    If Not resultTOrderControl.StateResult Then
                        scope.Dispose()
                        Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultTOrderControl.Message}
                    End If

                    transferOrderCodes.Add(tOrderControl.Code)
                End If

                Dim itemsDevolution = (From d In rawMaterialDevolution.RawMaterialDevolutionDetail Where Not d.CampaignDetailValidation.Warehouse.ControlStore Select d).ToList()

                If itemsDevolution.Any() Then
                    ' Generar orden de traslado del almacen de producción al del stock
                    Dim tOrder = GetTransferOrder(
                        rawMaterialDevolutionCode:=rawMaterialDevolution.Code,
                        sourceWareHouseId:=rawMaterialDevolution.ProductionWarehouseId,
                        targetWarehouseId:=rawMaterialDevolution.StockWarehouseId,
                        rawMaterialDevolutionDetails:=itemsDevolution,
                        operatingUnitId:=operatingUnitId,
                        audit:=audit
                    )

                    Dim resultTOrder = _transferOrderAdminService.SaveTrasnferOrder(tOrder, audit)

                    If Not resultTOrder.StateResult Then
                        scope.Dispose()
                        Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultTOrder.Message}
                    End If

                    transferOrderCodes.Add(tOrder.Code)
                End If

                'Actualizamos las cantidades devueltas
                For Each ddetail In rawMaterialDevolution.RawMaterialDevolutionDetail
                    Dim campaignDetailValidation = ddetail.CampaignDetailValidation
                    campaignDetailValidation.DevolutionQuantity += ddetail.Quantity
                    campaignDetailValidation.ModificationUser = audit.CodeUser
                    campaignDetailValidation.ModificationDate = Date.Now

                    _campaignDetailValidationRepository.SaveEntity(campaignDetailValidation)
                Next
                uow.Commit()

                rawMaterialDevolution.State = 2 'Confirmado
                rawMaterialDevolution.ConfirmationUser = audit.CodeUser
                rawMaterialDevolution.ConfirmationDate = Date.Now
                rawMaterialDevolution.MarkAsModified()
                _rawMaterialDevolutionRepository.SaveEntity(rawMaterialDevolution)
                uow.Commit()

                scope.Complete()
                Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = String.Join(vbCrLf, transferOrderCodes), .MessageResult = transferOrderCodes}
            End Using
        Catch ex As OptimisticConcurrencyException
            uow.RollbackChanges()
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As IndigoValidationException
            uow.RollbackChanges()
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            uow.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Guarda y confirma un documento
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function AnnulateRawMaterialDevolution(code As String, audit As AuditMessage) As ActionResult(Of RawMaterialDevolution) Implements IRawMaterialDevolutionAdminService.AnnulateRawMaterialDevolution
        Try
            Dim rawMaterialDevolution = _rawMaterialDevolutionRepository.FirstOrDefault(Function(m) m.Code = code)

            If rawMaterialDevolution Is Nothing Then
                Throw New IndigoValidationException($"Devolución con código ({code}) no encontrado")
            End If

            rawMaterialDevolution.State = 3
            rawMaterialDevolution.AnnulmentUser = audit.CodeUser
            rawMaterialDevolution.AnnulmentDate = Date.Now
            rawMaterialDevolution.ModificationUser = audit.CodeUser
            rawMaterialDevolution.ModificationDate = Date.Now

            _rawMaterialDevolutionRepository.SaveEntity(rawMaterialDevolution)
            _rawMaterialDevolutionRepository.UnitWork.Commit()

            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = "Documento anulado correctamente"}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Guarda y confirma un documento
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSecuence"></param>
    ''' <returns></returns>
    Public Function SaveAndConfirmRawMaterialDevolution(rawMaterialDevolution As RawMaterialDevolution, operatingUnitId As Integer, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RawMaterialDevolution) Implements IRawMaterialDevolutionAdminService.SaveAndConfirmRawMaterialDevolution
        Try
            Dim resSave = SaveRawMaterialDevolution(rawMaterialDevolution, audit, idSecuence)

            If resSave.StateResult Then
                Dim resConfirmation = ConfirmRawMaterialDevolution(resSave.ObjectEmbbeded, operatingUnitId, audit)

                If resConfirmation.StateResult Then
                    Dim msg = $"Se guardó y confirmó con código ({resSave.ObjectEmbbeded.Code})"

                    If resConfirmation.MessageResult IsNot Nothing AndAlso resConfirmation.MessageResult.Count() = 2 Then
                        msg += $" y generando dos Órdenes de Traslado con códigos ({String.Join(", ", resConfirmation.MessageResult)})"
                    Else
                        msg += $" y generando una Orden de Traslado con código ({resConfirmation.Message})"
                    End If

                    Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = msg, .ObjectEmbbeded = rawMaterialDevolution}
                Else
                    Dim sb As New StringBuilder()
                    sb.AppendLine($"Se guardó con código ({resSave.ObjectEmbbeded.Code}) pero no se confirmó por:")
                    sb.AppendLine(resConfirmation.Message)
                    Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = True, .StatusCode = eStatusResult.WARNING, .Message = sb.ToString(), .ObjectEmbbeded = rawMaterialDevolution}
                End If
            Else
                Return resSave
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Guarda la devolucion de material prima
    ''' </summary>
    ''' <param name="rawMaterialDevolution"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveRawMaterialDevolution(rawMaterialDevolution As RawMaterialDevolution, audit As AuditMessage, Optional idSecuence As Long = 0) As ActionResult(Of RawMaterialDevolution) Implements IRawMaterialDevolutionAdminService.SaveRawMaterialDevolution
        If rawMaterialDevolution Is Nothing Then
            Throw New ArgumentNullException("rawMaterialDevolution")
        End If

        Dim uow As IUnitWork = Me._rawMaterialDevolutionRepository.UnitWork

        Try
            'Validamos los detalles
            ValidateRawMaterialDevolutionDetails(rawMaterialDevolution)

            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {
                    .Timeout = TransactionManager.MaximumTimeout,
                    .IsolationLevel = IsolationLevel.ReadCommitted
                })

                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(rawMaterialDevolution.Code) Then
                    Dim seq As MixingStationSequenceDetail = Me._secuenseDetailRepository.GetSequenceDById(idSecuence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            rawMaterialDevolution.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of RawMaterialDevolution) With {
                                .StatusCode = eStatusResult.WARNING,
                                .StateResult = False,
                                .MessageResult = {"_Seq02_"}.ToList(),
                                .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)
                            }
                        End If
                        MessageResult = If(seq.MixingStationSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), rawMaterialDevolution.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of RawMaterialDevolution) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As RawMaterialDevolution = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of RawMaterialDevolution)
                Dim status As Integer

                If rawMaterialDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    rawMaterialDevolution.CreationUser = audit.CodeUser
                    rawMaterialDevolution.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = rawMaterialDevolution.OriginalValue
                    rawMaterialDevolution.ModificationUser = audit.CodeUser
                    rawMaterialDevolution.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                rawMaterialDevolution.RawMaterialDevolutionDetail _
                    .ToList().ForEach(Sub(item)
                                          If item.Id > 0 Then
                                              item.ModificationUser = audit.CodeUser
                                              item.ModificationDate = DateTime.Now
                                          Else
                                              item.CreationUser = audit.CodeUser
                                              item.CreationDate = DateTime.Now
                                          End If
                                      End Sub)

                _rawMaterialDevolutionRepository.SaveEntity(rawMaterialDevolution)
                uow.Commit()

                auditProcess = New IndigoAuditSimpleEntity(Of RawMaterialDevolution)(rawMaterialDevolution, audit, status, auxObjEntity)
                auditProcess.Execute()
                rawMaterialDevolution.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = MessageResult, .ObjectEmbbeded = rawMaterialDevolution}
            End Using
        Catch ex As OptimisticConcurrencyException
            uow.RollbackChanges()
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As IndigoValidationException
            uow.RollbackChanges()
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            uow.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RawMaterialDevolution) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "Privates"
    ''' <summary>
    ''' Validamos los detalles de devolución
    ''' </summary>
    Private Sub ValidateRawMaterialDevolutionDetails(rawMaterialDevolution As RawMaterialDevolution)

        If rawMaterialDevolution.RawMaterialDevolutionDetail Is Nothing OrElse Not rawMaterialDevolution.RawMaterialDevolutionDetail.Any() Then
            Throw New IndigoValidationException("No se ha enviado detalles de devolución")
        End If

        For i As Integer = rawMaterialDevolution.RawMaterialDevolutionDetail.Count - 1 To 0 Step -1
            If rawMaterialDevolution.RawMaterialDevolutionDetail(i).Quantity = 0 Then
                rawMaterialDevolution.RawMaterialDevolutionDetail(i).MarkAsDeleted()
            End If
        Next

        rawMaterialDevolution.ChangeTracker.ObjectsRemovedFromCollectionProperties.Clear()

        If Not rawMaterialDevolution.RawMaterialDevolutionDetail.Any() Then
            Throw New IndigoValidationException("No se ha enviado detalles de devolución con cantidades a devolver")
        End If

        Dim errors As New StringBuilder()
        For Each dev In rawMaterialDevolution.RawMaterialDevolutionDetail
            If dev.Quantity + dev.DevolutionQuantity > dev.DeliveredQuantity Then
                errors.AppendFormat("La cantidad a devolver del producto ({0}) supera la cantidad máxima permitida.", dev.ProductCodeName)
            ElseIf dev.Quantity < 0 Then
                errors.AppendFormat("El producto ({0}) contiene una cantidad a devolver no permitida.", dev.ProductCodeName)
            End If
        Next

        If errors.Length > 0 Then Throw New IndigoValidationException(errors.ToString())
    End Sub

    ''' <summary>
    ''' Genera una Orden de traslado
    ''' </summary>
    ''' <param name="sourceWareHouseId"></param>
    ''' <param name="targetWarehouseId"></param>
    ''' <param name="rawMaterialDevolutionDetails"></param>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function GetTransferOrder(
        rawMaterialDevolutionCode As String,
        sourceWareHouseId As Integer,
        targetWarehouseId As Integer,
        rawMaterialDevolutionDetails As List(Of RawMaterialDevolutionDetail),
        operatingUnitId As Integer,
        audit As AuditMessage
    ) As TransferOrder
        Dim order As New TransferOrder
        With order
            .Code = ""
            .OperatingUnitId = operatingUnitId
            .DocumentDate = Date.Now
            .OrderType = 1
            .DispatchTo = 1
            .SourceWarehouseId = sourceWareHouseId
            .TargetWarehouseId = targetWarehouseId
            .Description = String.Format("Confirmación de devolución de materia prima Central de Mezclas : {0}", rawMaterialDevolutionCode)
            .Status = 2
            .CreationUser = audit.CodeUser
            .CreationDate = Date.Now
        End With

        Dim details = CreateOrderTransferDetail(sourceWareHouseId, rawMaterialDevolutionDetails)
        details.ForEach(Sub(item) order.TransferOrderDetail.Add(item))

        Return order
    End Function

    ''' <summary>
    ''' Guarda los movimientos en el kardex
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="rawMaterialDevolutionDetails"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function SaveCampaignKardex(campaignDetailId As Integer, rawMaterialDevolutionDetails As List(Of RawMaterialDevolutionDetail), audit As AuditMessage) As ActionResult
        ' Registrar movimiento de salida del kardex  de mezclas
        Dim msClass As Integer = _campaignDetailRepository _
            .Query(Function(m) m.Id = campaignDetailId, includes:={"UnitDoseType"}) _
            .Select(Function(m) m.UnitDoseType.MSClass) _
            .FirstOrDefault()

        For Each devolutionDetail In rawMaterialDevolutionDetails
            ' Consultar el validation
            Dim campaignDetailValidation = devolutionDetail.CampaignDetailValidation

            If campaignDetailValidation Is Nothing Then
                campaignDetailValidation = _campaignDetailValidationRepository _
                    .FirstOrDefault(Function(m) m.Id = devolutionDetail.CampaignDetailValidationId, True, {"InventoryProduct.ProductType"}.ToList())
            End If

            If campaignDetailValidation.InventoryProduct Is Nothing Then
                Dim product As InventoryProduct = _inventoryProductRepository.FirstOrDefault(Function(m) m.Id = campaignDetailValidation.ProductId, True, {"ProductType"}.ToList())
                campaignDetailValidation.InventoryProduct = product
            End If

            If campaignDetailValidation.InventoryProduct.ProductType Is Nothing Then
                Dim productType = _productTypeRepository.FirstOrDefault(Function(m) m.Id = campaignDetailValidation.InventoryProduct.ProductTypeId, True)
                campaignDetailValidation.InventoryProduct.ProductType = productType ' Esto ni siquiera es necesario porque al consultarlo con tracking se agrega al producto
            End If

            Dim measurementUnitId = _inventoryService.GetMeasurementUnitByProductId(campaignDetailValidation.ProductId, msClass)

            Dim reskardex = _campaignKardexAdminService.Savekardex(Of RawMaterialDevolution)(
                        campaignDetailId:=campaignDetailId,
                        movementType:=eMovementType.Output,
                        productId:=devolutionDetail.ProductId,
                        batchSerialId:=devolutionDetail.BatchSerialId,
                        quantity:=devolutionDetail.Quantity * measurementUnitId.FirstOrDefault.Item2,
                        measurementUnitId:=measurementUnitId.FirstOrDefault.Item1,
                        description:="Devolución de Materia Prima",
                        audit:=audit
                    )

            If Not reskardex.StateResult Then
                Return New ActionResult With {.StateResult = False, .Message = reskardex.Message}
            End If
        Next

        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Funcion Crear el Detalle de la Orden de traslado
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateOrderTransferDetail(warehouseId As Integer, rawMaterialDevolutionDetails As List(Of RawMaterialDevolutionDetail)) As List(Of TransferOrderDetail)
        Dim transferOrderDetail As New List(Of TransferOrderDetail)()

        rawMaterialDevolutionDetails.GroupBy(Function(m) m.ProductId).ToList() _
            .ForEach(Sub(grmd As IGrouping(Of Integer, RawMaterialDevolutionDetail))

                         Dim orderDetail As New TransferOrderDetail()
                         Dim inventoryQuantity As Integer = 0
                         Dim product As InventoryProduct = _inventoryProductRepository.FirstOrDefault(Function(m) m.Id = grmd.Key, True)

                         For Each rmdd In grmd
                             Dim physicalInventory As PhysicalInventory = Nothing

                             If rmdd.BatchSerialId.HasValue Then
                                 physicalInventory = _physicalInventoryRepository.GetPhysicalInventoryByBatchSerial(rmdd.ProductId, warehouseId, rmdd.BatchSerialId.Value)

                                 If physicalInventory Is Nothing Then Throw New IndigoValidationException($"El producto {rmdd.ProductCodeName} no se encontró en el inventario")

                                 Dim transferOrderDetailBatchSerial = createTransferBatchSerialDetail(physicalInventory.Id, rmdd.Quantity)
                                 orderDetail.TransferOrderDetailBatchSerial.Add(transferOrderDetailBatchSerial)
                             Else
                                 physicalInventory = _physicalInventoryRepository.GetPhysicalInventory(rmdd.ProductId, warehouseId)
                             End If

                             inventoryQuantity += physicalInventory.Quantity
                         Next

                         With orderDetail
                             .ProductId = grmd.Key
                             .InventoryQuantity = inventoryQuantity
                             .Quantity = grmd.Sum(Function(m) m.Quantity)
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
